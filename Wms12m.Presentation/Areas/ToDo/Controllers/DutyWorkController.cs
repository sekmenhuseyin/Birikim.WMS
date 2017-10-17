using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class DutyWorkController : RootController
    {
        /// <summary>
        /// çalışmalar
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.Sorumlu == vUser.UserName || m.Sorumlu2 == vUser.UserName || m.Sorumlu3 == vUser.UserName || m.KontrolSorumlusu == vUser.UserName).ToList(), "ID", "Gorev");
            ViewBag.RoleName = vUser.RoleName;
            return View();
        }
        /// <summary>
        /// yeni çalışma
        /// </summary>
        public PartialViewResult New()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            var TodoItem = db.GorevlerToDoLists.Find(ID);
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.ID == TodoItem.GorevID).ToList(), "ID", "Gorev", TodoItem.GorevID);
            var tbl = new GorevlerCalisma
            {
                Calisma = TodoItem.Aciklama,
                GorevID = TodoItem.GorevID
            };
            return PartialView("New", tbl);
        }
        /// <summary>
        /// yeni çalışma
        /// </summary>
        public PartialViewResult NewAll()
        {
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan");
            ViewBag.GorevID = new SelectList(ComboSub.GetList(Combos.DestekTipi.ToInt32()), "ID", "Name", "");
            return PartialView(new GorevlerCalisma());
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(bool Tip)
        {
            var gorevCalismas = db.GorevlerCalismas.Where(m => m.ID > 0);
            var yetki = CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting);
            if (Tip == false)//bana ait
            {
                gorevCalismas = gorevCalismas.Where(m => m.Kaydeden == vUser.UserName);
            }
            else if (yetki == true)//admin ise filtre yapma hepsini gör
            {

            }
            else
            {
                gorevCalismas = gorevCalismas.Where(m => m.Kaydeden == vUser.UserName || m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName || m.Gorevler.KontrolSorumlusu == vUser.UserName || m.Gorevler.Kaydeden == vUser.UserName);
            }
            ViewBag.Yetki2 = yetki;
            return PartialView("List", gorevCalismas.OrderByDescending(m => m.Tarih).ToList());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int Id)
        {
            //TODO:
            var gorevCalisma = db.GorevlerCalismas.Find(Id);
            ViewBag.GorevID = new SelectList(db.Gorevlers.Where(m => m.ID == gorevCalisma.GorevID).ToList(), "ID", "Gorev", gorevCalisma.GorevID);
            return PartialView(gorevCalisma);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(GorevlerCalisma gorevCalisma)
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (!ModelState.IsValid)
                return Json(new Result(false, "Hata oldu. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
            if (gorevCalisma.Sure < 0 || gorevCalisma.Sure > 540)
                return Json(new Result(false, "Çalışma süresini doğru yazınız"), JsonRequestBehavior.AllowGet);
            //yeni çalışma girerken
            if (gorevCalisma.ID == 0)
            {
                //kontrol
                string sql = string.Format("SELECT ISNULL(SUM(Sure), 0) AS Expr1 FROM ong.GorevlerCalisma WHERE (Tarih = '{0}') AND (Kaydeden = '{1}')", DateTime.Now.ToString("yyyy-MM-dd"), vUser.UserName);
                var kontrol = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                if ((kontrol + gorevCalisma.Sure) > 540)
                    return Json(new Result(false, "Seçili tarih için " + (kontrol + gorevCalisma.Sure - 540) + " dakika fazla yazdınız."), JsonRequestBehavior.AllowGet);
                //update
                var grv = db.Gorevlers.Where(m => m.ID == gorevCalisma.GorevID).FirstOrDefault();
                gorevCalisma.Kaydeden = vUser.UserName;
                gorevCalisma.KayitTarih = DateTime.Now;
                gorevCalisma.Degistiren = vUser.UserName;
                gorevCalisma.DegisTarih = gorevCalisma.KayitTarih;
                //eğer 
                if (vUser.RoleName == "Developer")
                {
                    //eğer tüm maddeler onaylandıysa kontrol onay yap yoksa başlandı yap
                    var c = grv.GorevlerToDoLists.Where(m => m.Onay == false).FirstOrDefault();
                    if (c == null)
                        grv.DurumID = ComboItems.gydKaliteKontrol.ToInt32();
                    else
                        grv.DurumID = ComboItems.gydBaşlandı.ToInt32();
                }
                else if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
                {
                    //eğer admin tüm maddeleri onaylarsa durum değişsin
                    var c = grv.GorevlerToDoLists.Where(m => m.AdminOnay == false).FirstOrDefault();
                    if (c == null)
                        grv.DurumID = ComboItems.gydOnaylandı.ToInt32();
                }
                db.GorevlerCalismas.Add(gorevCalisma);
            }
            //çalışma güncelleme
            else
            {
                //kontrol
                string sql = string.Format("SELECT ISNULL(SUM(Sure), 0) AS Expr1 FROM ong.GorevlerCalisma WHERE (Tarih = '{0}') AND (Kaydeden = '{1}') AND (ID <> {2})", gorevCalisma.Tarih.ToString("yyyy-MM-dd"), vUser.UserName, gorevCalisma.ID);
                var kontrol = db.Database.SqlQuery<int>(sql).FirstOrDefault();
                if ((kontrol + gorevCalisma.Sure) > 540)
                    return Json(new Result(false, "Seçili tarih için " + (kontrol + gorevCalisma.Sure - 540) + " dakika fazla yazdınız."), JsonRequestBehavior.AllowGet);
                //update
                var tbl = db.GorevlerCalismas.Where(m => m.ID == gorevCalisma.ID).FirstOrDefault();
                tbl.Tarih = gorevCalisma.Tarih;
                tbl.Sure = gorevCalisma.Sure;
                tbl.Calisma = gorevCalisma.Calisma.ToString2();
                tbl.DegisTarih = DateTime.Now;
                tbl.Degistiren = vUser.UserName;
            }
            try
            {
                db.SaveChanges();
                return Json(new Result(true, gorevCalisma.ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/DutyWork/Save");
                return Json(new Result(false, "Kayıt hatası"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// destek çalışma kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult SaveAll(frmGorevDestekCalisma tbl)
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (!ModelState.IsValid)
                return Json(new Result(false, "Hata oldu. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
            if (tbl.Sure < 0 || tbl.Sure > 540)
                return Json(new Result(false, "Çalışma süresini doğru yazınız"), JsonRequestBehavior.AllowGet);
            //get comboitemname
            var gtip = db.ComboItem_Name.Where(m => m.ID == tbl.GorevID).Select(m => m.Name).FirstOrDefault();
            var durum = ComboItems.gydOnaylandı.ToInt32();
            //projeform
            var projeid = db.ProjeForms.Where(m => m.Proje == gtip).FirstOrDefault();
            if (projeid==null)
            {
                db.ProjeForms.Add(new ProjeForm()
                {
                    MusteriID = tbl.MusteriID,
                    Proje = gtip,
                    Form = "",
                    MesaiKontrol = false,
                    Kaydeden = vUser.UserName,
                    KayitTarih = DateTime.Now,
                    Degistiren = vUser.UserName,
                    DegisTarih = DateTime.Now,
                    Aktif = true
                });
            }
            //add görev
            var gorev = new Gorevler()
            {
                ProjeForm = projeid,
                Sorumlu = vUser.UserName,
                Gorev = gtip,
                Aciklama = gtip,
                OncelikID = 1,
                DurumID = durum,
                GorevTipiID = 52,
                DepartmanID = 49,
                Kaydeden = vUser.UserName,
                KayitTarih = DateTime.Now,
                Degistiren = vUser.UserName,
                DegisTarih = DateTime.Now

            };
            //add todolist
            var todo = new GorevlerToDoList()
            {
                Gorevler = gorev,
                Aciklama = gtip,
                Onay = true,
                KontrolOnay = true,
                AdminOnay = true,
                Onaylayan = vUser.UserName,
                KontrolEden = vUser.UserName,
                Kaydeden = vUser.UserName,
                KayitTarih = DateTime.Now,
                Degistiren = vUser.UserName,
                DegisTarih = DateTime.Now
            };
            //add çalışma
            var cal = new GorevlerCalisma()
            {
                Gorevler = gorev,
                Calisma = tbl.Calisma,
                Sure = tbl.Sure,
                Tarih = DateTime.Now,
                Kaydeden = vUser.UserName,
                KayitTarih = DateTime.Now,
                Degistiren = vUser.UserName,
                DegisTarih = DateTime.Now
            };
            //add
            db.Gorevlers.Add(gorev);
            db.GorevlerToDoLists.Add(todo);
            db.GorevlerCalismas.Add(cal);
            //save
            try
            {
                db.SaveChanges();
                return Json(new Result(true, cal.ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/DutyWork/Save");
                return Json(new Result(false, "Kayıt hatası"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            GorevlerCalisma gorevcalisma = db.GorevlerCalismas.Find(Id.ToInt32());
            db.GorevlerCalismas.Remove(gorevcalisma);
            try
            {
                db.SaveChanges();
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "/ToDo/DutyWork/Delete");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// todo lists page
        /// </summary>
        public ActionResult ToDos()
        {
            if (CheckPerm(Perms.TodoÇalışma, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Yetki = CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting);
            ViewBag.RoleName = vUser.RoleName;
            return View();
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult ToDosList(bool Tip)
        {
            var tip1 = ComboItems.gydReddedildi.ToInt32();
            var tip2 = ComboItems.gydOnaylandı.ToInt32();
            var tip3 = ComboItems.gydBeklemede.ToInt32();
            var tip4 = ComboItems.gydOnayVer.ToInt32();
            var tip5 = ComboItems.gydBitti.ToInt32();
            var tip6 = ComboItems.gydDurduruldu.ToInt32();
            var list = db.GorevlerToDoLists.Where(m => m.AdminOnay == false && m.Gorevler.DurumID != tip1 && m.Gorevler.DurumID != tip2 && m.Gorevler.DurumID != tip3 && m.Gorevler.DurumID != tip4 && m.Gorevler.DurumID != tip5 && m.Gorevler.DurumID != tip6);
            if (Tip == false)
            {
                if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
                    list = list.Where(m => (m.Onay == true && m.KontrolOnay == true) || ((m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName) && m.Onay == false) || (m.Onay == true && m.KontrolOnay == false && m.Gorevler.KontrolSorumlusu == vUser.UserName));
                else if (vUser.RoleName == "Destek")
                    list = list.Where(m => ((m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName) && m.Onay == false) || (m.Onay == true && m.KontrolOnay == false && (m.Gorevler.KontrolSorumlusu == vUser.UserName || m.Gorevler.KontrolSorumlusu == null)));
                else
                    list = list.Where(m => (m.Gorevler.Sorumlu == vUser.UserName || m.Gorevler.Sorumlu2 == vUser.UserName || m.Gorevler.Sorumlu3 == vUser.UserName) && m.Onay == false);
            }
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.Tip = Tip;
            ViewBag.RoleName = vUser.RoleName;
            return PartialView("ToDosList", list.OrderBy(m => m.Gorevler.OncelikID).ThenBy(m => m.ID).ToList());
        }
        /// <summary>
        /// ToDos onay / ret
        /// </summary>
        public JsonResult ToDosOnay(int Id, bool Onay, int Sure)
        {
            var tbl = db.GorevlerToDoLists.Where(m => m.ID == Id).FirstOrDefault();
            //kontrol
            string sql = string.Format("SELECT ISNULL(SUM(Sure), 0) AS Expr1 FROM ong.GorevlerCalisma WHERE (Tarih = '{0}') AND (Kaydeden = '{1}')", DateTime.Now.ToString("yyyy-MM-dd"), vUser.UserName);
            var kontrol = db.Database.SqlQuery<int>(sql).FirstOrDefault();
            if ((kontrol + Sure) > 540)
                return Json(new Result(false, "Seçili tarih için " + (kontrol + Sure - 540) + " dakika fazla yazdınız."), JsonRequestBehavior.AllowGet);
            //getir
            tbl.DegisTarih = DateTime.Now;
            tbl.Degistiren = vUser.UserName;
            if (Onay == true)
            {
                //onaylamalar
                if (tbl.Onay == false && (tbl.Gorevler.Sorumlu == vUser.UserName || tbl.Gorevler.Sorumlu2 == vUser.UserName || tbl.Gorevler.Sorumlu3 == vUser.UserName))
                {
                    tbl.Onay = true;
                    tbl.Onaylayan = vUser.UserName;
                    //messages
                    if (tbl.Gorevler.KontrolSorumlusu != null)
                    {
                        var mesaj = new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = tbl.Gorevler.KontrolSorumlusu,
                            Tarih = DateTime.Now,
                            Mesaj = "Onay listenize bir maddde eklendi",
                            URL = "/ToDo/DutyWork/Todos"
                        };
                        db.Messages.Add(mesaj);
                    }
                }
                else if (tbl.Onay == true && vUser.RoleName == "Destek")
                {
                    tbl.KontrolOnay = true;
                    tbl.KontrolEden = vUser.UserName;
                    //messages
                    var kullList = Persons.GetList("Admin");
                    foreach (var item in kullList)
                    {
                        db.Messages.Add(new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = item.Kod,
                            Tarih = DateTime.Now,
                            Mesaj = "Onay listenize bir maddde eklendi",
                            URL = "/ToDo/DutyWork/Todos"
                        });
                    }
                }
                else if (tbl.Onay == true && CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == true)
                {
                    tbl.KontrolOnay = true;
                    tbl.KontrolEden = vUser.UserName;
                    tbl.AdminOnay = true;
                }
                else if (tbl.Onay == true && tbl.KontrolOnay == true && CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == true)
                {
                    tbl.AdminOnay = true;
                }
                //çalışma gir
                if (Sure > 0)
                {
                    var calisma = new GorevlerCalisma()
                    {
                        GorevID = tbl.GorevID,
                        Tarih = DateTime.Now,
                        Sure = Sure,
                        Calisma = tbl.Aciklama,
                        KayitTarih = DateTime.Now,
                        Kaydeden = vUser.UserName,
                        Degistiren = vUser.UserName,
                        DegisTarih = DateTime.Now,
                    };
                    db.GorevlerCalismas.Add(calisma);
                }
                //eğer 
                if (vUser.RoleName == "Developer")
                {
                    var c = db.GorevlerToDoLists.Where(m => m.GorevID == tbl.GorevID && m.Onay == false && m.ID != Id).FirstOrDefault();
                    if (c == null)
                        tbl.Gorevler.DurumID = ComboItems.gydKaliteKontrol.ToInt32();
                    else
                        tbl.Gorevler.DurumID = ComboItems.gydBaşlandı.ToInt32();
                }
                else if (CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == true)
                {
                    var c = db.GorevlerToDoLists.Where(m => m.GorevID == tbl.GorevID && m.AdminOnay == false && m.ID != Id).FirstOrDefault();
                    if (c == null)
                        tbl.Gorevler.DurumID = ComboItems.gydOnaylandı.ToInt32();
                }
            }
            else
            {
                if ((tbl.Onay == true && tbl.KontrolOnay == true && CheckPerm(Perms.TodoÇalışma, PermTypes.Deleting) == true) || (tbl.Onay == true && (tbl.Gorevler.KontrolSorumlusu == vUser.UserName || tbl.Gorevler.KontrolSorumlusu == null)))
                {
                    tbl.AdminOnay = false;
                    tbl.KontrolOnay = false;
                    tbl.KontrolEden = null;
                    tbl.Onay = false;
                    tbl.Onaylayan = null;
                    tbl.Gorevler.DurumID = ComboItems.gydBaşlandı.ToInt32();
                    //messages
                    var mesaj = new Message()
                    {
                        MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                        Kimden = vUser.UserName,
                        Kime = tbl.Gorevler.Sorumlu,
                        Tarih = DateTime.Now,
                        Mesaj = vUser.UserName + " onay listenizdeki bir maddeyi reddetti",
                        URL = "/ToDo/DutyWork/Todos"
                    };
                    db.Messages.Add(mesaj);
                    if (tbl.Gorevler.Sorumlu2 != null)
                    {
                        var mesaj2 = new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = tbl.Gorevler.Sorumlu2,
                            Tarih = DateTime.Now,
                            Mesaj = vUser.UserName + " onay listenizdeki bir maddeyi reddetti",
                            URL = "/ToDo/DutyWork/Todos"
                        };
                        db.Messages.Add(mesaj2);
                    }
                    if (tbl.Gorevler.Sorumlu3 != null)
                    {
                        var mesaj3 = new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = tbl.Gorevler.Sorumlu3,
                            Tarih = DateTime.Now,
                            Mesaj = vUser.UserName + " onay listenizdeki bir maddeyi reddetti",
                            URL = "/ToDo/DutyWork/Todos"
                        };
                        db.Messages.Add(mesaj3);
                    }
                }
            }
            //kaydet
            try
            {
                db.SaveChanges();
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/DutyWork/ToDosOnay");
                return Json(new Result(false, "Kayıt hatası"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// ayrıntı
        /// </summary>
        public PartialViewResult ToDosDetails(int ID)
        {
            return PartialView("ToDosDetails", db.Gorevlers.Where(m => m.ID == ID).FirstOrDefault());
        }
    }
}