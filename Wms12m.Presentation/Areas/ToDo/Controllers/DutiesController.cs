using Birikim.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class DutiesController : RootController
    {
        /// <summary>
        /// görevler sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.RoleName = vUser.RoleName;
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            return View();
        }

        /// <summary>
        /// yeni görev sayfası
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return null;
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", "");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", "");
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan");
            ViewBag.Sorumlu = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
            ViewBag.KontrolSorumlusu = new SelectList(Persons.GetList(new string[] { "Destek", "Admin" }), "Kod", "AdSoyad");
            ViewBag.Sorumlu2 = ViewBag.Sorumlu;
            ViewBag.Sorumlu3 = ViewBag.Sorumlu;
            return PartialView(new Gorevler());
        }

        /// <summary>
        /// ayrıntılar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            var list = db.GorevlerToDoLists.Where(a => a.GorevID == ID).ToList();
            ViewBag.ID = ID;
            return PartialView("Details", list);
        }

        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(int Tip)
        {
            var list = db.Gorevlers.Where(m => m.ID > 0);
            if (Tip == 0)
            {
                var tip1 = ComboItems.gydReddedildi.ToInt32();
                var tip2 = ComboItems.gydOnaylandı.ToInt32();
                var tip3 = ComboItems.gydBeklemede.ToInt32();
                var tip4 = ComboItems.gydOnayVer.ToInt32();
                var tip5 = ComboItems.gydBitti.ToInt32();
                var tip6 = ComboItems.gydDurduruldu.ToInt32();
                list = list.Where(m => m.DurumID != tip1 && m.DurumID != tip2 && m.DurumID != tip3 && m.DurumID != tip4 && m.DurumID != tip5 && m.DurumID != tip6);
            }
            else
                list = list.Where(m => m.DurumID == Tip);
            ViewBag.Yetki2 = CheckPerm(Perms.TodoGörevler, PermTypes.Deleting);
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.Tip = Tip;
            return PartialView(list.ToList());
        }

        /// <summary>
        /// sadece onay listesi
        /// </summary>
        public PartialViewResult ListOnay()
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false) return null;
            var Tip = ComboItems.gydOnayVer.ToInt32();
            var list = db.Gorevlers.Where(m => m.DurumID == Tip);
            ViewBag.Yetki = CheckPerm(Perms.TodoGörevler, PermTypes.Deleting);
            ViewBag.Yetki1 = CheckPerm(Perms.TodoGörevler, PermTypes.Writing);
            ViewBag.Tip = Tip;
            return PartialView(list.ToList());
        }

        /// <summary>
        /// düzenleme sayfası
        /// </summary>
        public PartialViewResult Edit(int? id)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return null;
            var tbl = db.Gorevlers.Find(id);
            var list = db.Database.SqlQuery<SelectListItem>(string.Format("EXEC BIRIKIM.ong.GetProjeFormFromMusteri {0}", tbl.ProjeForm.MusteriID)).ToList();
            ViewBag.Proje = new SelectList(list, "Value", "Text", tbl.ProjeForm.PID);
            ViewBag.MusteriID = new SelectList(db.Musteris.OrderBy(m => m.Unvan).ToList(), "ID", "Unvan", tbl.ProjeForm.MusteriID);
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms.Where(m => m.PID == tbl.ProjeForm.PID).ToList(), "ID", "Form", tbl.ProjeForm.ID);
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", tbl.GorevTipiID);
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", tbl.DepartmanID);
            ViewBag.Sorumlu = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Sorumlu);
            ViewBag.Sorumlu2 = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Sorumlu2);
            ViewBag.Sorumlu3 = new SelectList(Persons.GetList(), "Kod", "AdSoyad", tbl.Sorumlu3);
            ViewBag.KontrolSorumlusu = new SelectList(Persons.GetList(new string[] { "Destek", "Admin" }), "Kod", "AdSoyad", tbl.KontrolSorumlusu);
            return PartialView("Edit", tbl);
        }

        /// <summary>
        /// görev onay / ret / durdur
        /// </summary>
        public JsonResult GorevOnay(int Id, int Tip)
        {
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);//sadece admin onaylayabilsin diye silme yetkisine bakıyorum
            try
            {
                var satir = db.Gorevlers.FirstOrDefault(m => m.ID == Id);
                satir.DurumID = Tip == 0 ? ComboItems.gydAtandı.ToInt32() : (Tip == 1 ? ComboItems.gydReddedildi.ToInt32() : (Tip == 2 ? ComboItems.gydDurduruldu.ToInt32() : ComboItems.gydBaşlandı.ToInt32()));
                satir.Degistiren = vUser.UserName;
                satir.DegisTarih = DateTime.Now;
                // messages
                if (satir.DurumID == ComboItems.gydAtandı.ToInt32())
                {
                    var mesaj = new Message()
                    {
                        MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                        Kimden = vUser.UserName,
                        Kime = satir.Sorumlu,
                        Tarih = DateTime.Now,
                        Mesaj = "Size yeni bir görev açıldı: " + satir.Gorev,
                        URL = "/ToDo/Duties"
                    };
                    db.Messages.Add(mesaj);
                    if (satir.Sorumlu2 != null)
                    {
                        var mesaj2 = new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = satir.Sorumlu2,
                            Tarih = DateTime.Now,
                            Mesaj = "Size yeni bir görev açıldı: " + satir.Gorev,
                            URL = "/ToDo/Duties"
                        };
                        db.Messages.Add(mesaj2);
                    }

                    if (satir.Sorumlu3 != null)
                    {
                        var mesaj3 = new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = satir.Sorumlu3,
                            Tarih = DateTime.Now,
                            Mesaj = "Size yeni bir görev açıldı: " + satir.Gorev,
                            URL = "/ToDo/Duties"
                        };
                        db.Messages.Add(mesaj3);
                    }
                }
                else if (satir.DurumID == ComboItems.gydReddedildi.ToInt32())
                {
                    var mesaj = new Message()
                    {
                        MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                        Kimden = vUser.UserName,
                        Kime = satir.Kaydeden,
                        Tarih = DateTime.Now,
                        Mesaj = "Açtığınız görev reddedildi: " + satir.Gorev,
                        URL = "/ToDo/Duties"
                    };
                    db.Messages.Add(mesaj);
                }

                // kaydet ve return
                db.SaveChanges();
                return Json(new Result(true, Id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/GorevOnay");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// kaydetme
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]//GorevTodo todo
        public JsonResult Save(Gorevler gorevler, string[] work, string[] tahminiBitis, string silinenler, string[] todo)
        {

            if (CheckPerm(Perms.TodoGörevler, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (!ModelState.IsValid)
                return Json(new Result(false, "Form hatalı. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
            // yeni görev ekle
            if (gorevler.ID == 0)
            {
                if (work == null)
                    return Json(new Result(false, "Lütfen bir madde yazınız!"), JsonRequestBehavior.AllowGet);
                if (work[0] == null || work[0] == "")
                    return Json(new Result(false, "Lütfen bir madde yazınız!"), JsonRequestBehavior.AllowGet);
                // set
                gorevler.Aciklama = gorevler.Aciklama ?? "";
                gorevler.Degistiren = vUser.UserName;
                gorevler.DegisTarih = DateTime.Now;
                gorevler.Kaydeden = vUser.UserName;
                gorevler.KayitTarih = gorevler.DegisTarih;
                gorevler.OncelikID = 0;

                if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false && gorevler.GorevTipiID == ComboItems.gytGeliştirme.ToInt32())
                    gorevler.DurumID = ComboItems.gydOnayVer.ToInt32();
                else
                    gorevler.DurumID = ComboItems.gydAtandı.ToInt32();
                db.Gorevlers.Add(gorevler);
                // lists
                var sontarih = DateTime.Parse(tahminiBitis[0]);
                for (int i = 0; i < work.Length; i++)
                {
                    //yeni maddeyi ekle
                    var grvTdl = new GorevlerToDoList
                    {
                        Aciklama = work[i],
                        DegisTarih = DateTime.Now,
                        Degistiren = vUser.UserName,
                        KayitTarih = DateTime.Now,
                        Kaydeden = vUser.UserName,
                        Gorevler = gorevler,
                        TahminiBitis = DateTime.Parse(tahminiBitis[i])
                    };

                    if (grvTdl.Aciklama.Trim() != "")
                    {
                        db.GorevlerToDoLists.Add(grvTdl);
                        //görevin tahmini bitiş tarihini hesala
                        if (DateTime.Parse(tahminiBitis[i]) > sontarih)
                            sontarih = DateTime.Parse(tahminiBitis[i]);
                    }
                }

                gorevler.TahminiBitis = sontarih;


                // messages
                if (gorevler.DurumID == ComboItems.gydAtandı.ToInt32())
                {
                    var mesaj = new Message()
                    {
                        MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                        Kimden = vUser.UserName,
                        Kime = gorevler.Sorumlu,
                        Tarih = DateTime.Now,
                        Mesaj = "Size yeni bir görev açıldı: " + gorevler.Gorev,
                        URL = "/ToDo/Duties"
                    };
                    db.Messages.Add(mesaj);
                    if (gorevler.Sorumlu2 != null)
                    {
                        var mesaj2 = new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = gorevler.Sorumlu2,
                            Tarih = DateTime.Now,
                            Mesaj = "Size yeni bir görev açıldı: " + gorevler.Gorev,
                            URL = "/ToDo/Duties"
                        };
                        db.Messages.Add(mesaj2);
                    }

                    if (gorevler.Sorumlu3 != null)
                    {
                        var mesaj3 = new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = gorevler.Sorumlu3,
                            Tarih = DateTime.Now,
                            Mesaj = "Size yeni bir görev açıldı: " + gorevler.Gorev,
                            URL = "/ToDo/Duties"
                        };
                        db.Messages.Add(mesaj3);
                    }
                }
                else if (gorevler.DurumID == ComboItems.gydOnayVer.ToInt32())
                {
                    var kullList = Persons.GetList("Admin");
                    foreach (var item in kullList)
                    {
                        db.Messages.Add(new Message()
                        {
                            MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                            Kimden = vUser.UserName,
                            Kime = item.Kod,
                            Tarih = DateTime.Now,
                            Mesaj = "Onayınıza bir görev düştü: " + gorevler.Gorev,
                            URL = "/ToDo/Duties"
                        });
                    }
                }
            }
            // görev güncelle
            else
            {
                // sil
                string[] sl = new string[0];
                if (silinenler != null && silinenler != "")
                {
                    sl = silinenler.Split(',');
                }

                for (int j = 0; j < sl.Length - 1; j++)
                {
                    var tmpId = Convert.ToInt32(sl[j]);
                    var silGrv = db.GorevlerToDoLists.FirstOrDefault(m => m.ID == tmpId);
                    db.GorevlerToDoLists.Remove(silGrv);
                }

                var sontarih = new DateTime();
                // görevi bul ve değiştir
                var tbl = db.Gorevlers.FirstOrDefault(m => m.ID == gorevler.ID);
                tbl.Sorumlu = gorevler.Sorumlu;
                tbl.Sorumlu2 = gorevler.Sorumlu2;
                tbl.Sorumlu3 = gorevler.Sorumlu3;
                tbl.KontrolSorumlusu = gorevler.KontrolSorumlusu;
                tbl.Gorev = gorevler.Gorev;
                tbl.Aciklama = gorevler.Aciklama ?? "";
                tbl.GorevTipiID = gorevler.GorevTipiID;
                tbl.DepartmanID = gorevler.DepartmanID;
                tbl.Degistiren = vUser.UserName;
                tbl.DegisTarih = DateTime.Now;
                if (work != null)
                {
                    for (int i = 0; i < work.Length; i++)
                    {
                        // yeni madde ekle
                        if (todo[i] == "0")
                        {
                            if (work[i].Trim() != "")
                            {
                                if (tbl.DurumID != ComboItems.gydBaşlandı.ToInt32() || tbl.DurumID != ComboItems.gydAtandı.ToInt32()) { tbl.DurumID = ComboItems.gydBaşlandı.ToInt32(); }
                                var grvTdl = new GorevlerToDoList
                                {
                                    Aciklama = work[i],
                                    Kaydeden = vUser.UserName,
                                    KayitTarih = DateTime.Now,
                                    Degistiren = vUser.UserName,
                                    DegisTarih = DateTime.Now,
                                    TahminiBitis = DateTime.Parse(tahminiBitis[i]),
                                    Gorevler = tbl
                                };
                                db.GorevlerToDoLists.Add(grvTdl);
                                //görevin tahmini bitiş tarihini hesala
                                if (DateTime.Parse(tahminiBitis[i]) > sontarih)
                                    sontarih = DateTime.Parse(tahminiBitis[i]);
                                //messages
                                var mesaj = new Message()
                                {
                                    MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                                    Kimden = vUser.UserName,
                                    Kime = tbl.Sorumlu,
                                    Tarih = DateTime.Now,
                                    Mesaj = "Onay listenize bir maddde eklendi: " + work[i],
                                    URL = "/ToDo/DutyWork/Todos"
                                };
                                db.Messages.Add(mesaj);
                                if (tbl.Sorumlu2 != null)
                                {
                                    var mesaj2 = new Message()
                                    {
                                        MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                                        Kimden = vUser.UserName,
                                        Kime = tbl.Sorumlu2,
                                        Tarih = DateTime.Now,
                                        Mesaj = "Onay listenize bir maddde eklendi: " + work[i],
                                        URL = "/ToDo/DutyWork/Todos"
                                    };
                                    db.Messages.Add(mesaj2);
                                }

                                if (tbl.Sorumlu3 != null)
                                {
                                    var mesaj3 = new Message()
                                    {
                                        MesajTipi = ComboItems.DuyuruMesajı.ToInt32(),
                                        Kimden = vUser.UserName,
                                        Kime = tbl.Sorumlu3,
                                        Tarih = DateTime.Now,
                                        Mesaj = "Onay listenize bir maddde eklendi: " + work[i],
                                        URL = "/ToDo/DutyWork/Todos"
                                    };
                                    db.Messages.Add(mesaj3);
                                }
                            }
                        }
                        // maddeyi güncelle
                        else
                        {
                            var id2 = Convert.ToInt32(todo[i]);
                            var grv = db.GorevlerToDoLists.FirstOrDefault(m => m.ID == id2);
                            if (grv.Onay == false)
                            {
                                grv.Aciklama = work[i].ToString2();
                                grv.DegisTarih = DateTime.Now;
                                grv.Degistiren = vUser.UserName;
                                grv.TahminiBitis = DateTime.Parse(tahminiBitis[i]);
                            }
                            //görevin tahmini bitiş tarihini hesala
                            if (DateTime.Parse(tahminiBitis[i]) > sontarih)
                                sontarih = DateTime.Parse(tahminiBitis[i]);
                        }
                    }
                    tbl.TahminiBitis = sontarih;
                }
            }

            try
            {
                db.SaveChanges();
                LogActions("ToDo", "Duties", "Save", ComboItems.alEkle, gorevler.ID, "Gorev: " + gorevler.Gorev);
                return Json(new Result(true, gorevler.ID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/Save");
                return Json(new Result(false, "Kayıt hatası. Sayfayı yenileyin"), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            var gorev = db.Gorevlers.Find(Id.ToInt32());
            if (CheckPerm(Perms.TodoGörevler, PermTypes.Deleting) == false && gorev.Kaydeden != vUser.UserName) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (gorev.GorevlerCalismas.FirstOrDefault() != null)
                return Json(new Result(false, "Bu göreve çalışma kaydedildiği için silinemez"), JsonRequestBehavior.AllowGet);
            try
            {
                db.GorevlerToDoLists.RemoveRange(gorev.GorevlerToDoLists.ToList());
                db.Gorevlers.Remove(gorev);
                db.SaveChanges();
                LogActions("ToDo", "Duties", "Delete", ComboItems.alSil, Id.ToInt32());
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "ToDo/Duties/Delete");
                return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
    }
}