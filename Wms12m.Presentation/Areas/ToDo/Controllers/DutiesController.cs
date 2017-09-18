using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class DutiesController : RootController
    {
        // GET: MainProjectForm/Create
        public ActionResult Index(string aktifPasif)
        {
            if (aktifPasif == null)
            {
                ViewBag.AktifPasif = "Aktif";
            }
            else
            {
                ViewBag.AktifPasif = aktifPasif;
            }
            return View();
        }

        public PartialViewResult New()
        {
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            ViewBag.OncelikID = new SelectList(ComboSub.GetList(Combos.Öncelik.ToInt32()), "ID", "Name");
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name", "");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name", "");
            //ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje");
            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma");
            ViewBag.Sorumlu = new SelectList(db.Users.ToList(), "Kod", "AdSoyad");
            ViewBag.KaliteKontrol = new SelectList(db.Users.Where(m => m.RoleName == "Destek").ToList(), "Kod", "AdSoyad");
            ViewBag.Sorumlu2 = ViewBag.Sorumlu;
            ViewBag.Sorumlu3 = ViewBag.Sorumlu;
            return PartialView(new Gorevler());
        }

        [HttpPost]
        public PartialViewResult Duty_Details(int ID)
        {
            var gorevCalismas = db.GorevCalismas.Include(g => g.Gorevler);
            var list = gorevCalismas.Where(a => a.GorevID == ID).ToList();

            return PartialView("Duty_Details", list);

        }

        public PartialViewResult List(string Tip)
        {
            ViewBag.RoleName = vUser.RoleName;
            var tip = Tip == "Aktif" || Tip == "Onay" ? true : false;
            var list = new List<Gorevler>();
            if (vUser.RoleName == "Admin" && Tip == "Onay")
            {
                var id = Combos.GörevYönetimDurumları.ToInt32();
                var durum = db.ComboItem_Name.Where(a => a.ComboID == id && a.Name == "Onay Ver").FirstOrDefault();
                list = db.Gorevlers.Where(a => a.AktifPasif == true && a.DurumID == durum.ID).OrderBy(a => a.OncelikID).ToList();
            }
            else if (vUser.RoleName == "Admin")
            {
                list = db.Gorevlers.Where(a => a.AktifPasif == tip).OrderBy(a => a.OncelikID).ToList();
            }
            else if (Tip == "Onay")
            {
                var id = Combos.GörevYönetimDurumları.ToInt32();
                var durum = db.ComboItem_Name.Where(a => a.ComboID == id && a.Name == "Onay Ver").FirstOrDefault();
                var tipId = Combos.GörevYönetimTipleri.ToInt32();
                var gorevTipleri = db.ComboItem_Name.Where(a => a.ComboID == tipId && a.Name == "Kalite Kontrol").FirstOrDefault();
                list = db.Gorevlers.Where(a => a.AktifPasif == tip && a.DurumID == durum.ID && (((a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName) && a.GorevTipiID != gorevTipleri.ID) || (a.KaliteKontrol == vUser.UserName && a.GorevTipiID == gorevTipleri.ID))).OrderBy(a => a.OncelikID).ToList();
            }
            else
            {
                var id = Combos.GörevYönetimDurumları.ToInt32();
                var durum = db.ComboItem_Name.Where(a => a.ComboID == id && a.Name == "Onay Ver").FirstOrDefault();
                var tipId = Combos.GörevYönetimTipleri.ToInt32();
                var gorevTipleri = db.ComboItem_Name.Where(a => a.ComboID == tipId && a.Name == "Kalite Kontrol").FirstOrDefault();
                if (tip == true)
                {
                    list = db.Gorevlers.Where(a => a.AktifPasif == tip && a.DurumID != durum.ID && (((a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName) && a.GorevTipiID != gorevTipleri.ID) || (a.KaliteKontrol == vUser.UserName && a.GorevTipiID == gorevTipleri.ID))).OrderBy(a => a.OncelikID).ToList();
                }
                else
                {
                    list = db.Gorevlers.Where(a => a.AktifPasif == tip && (((a.Sorumlu == vUser.UserName || a.Sorumlu2 == vUser.UserName || a.Sorumlu3 == vUser.UserName) && a.GorevTipiID != gorevTipleri.ID) || (a.KaliteKontrol == vUser.UserName && a.GorevTipiID == gorevTipleri.ID))).OrderBy(a => a.OncelikID).ToList();
                }

            }
            return PartialView(list);
        }

        // GET: MainProjectForm/Edit/5
        public PartialViewResult Edit(int? id)
        {
            var tbl = db.Gorevlers.Find(id);
            Gorevler gorevler = db.Gorevlers.Find(id);
            ProjeForm projeForm = db.ProjeForms.Find(gorevler.ProjeFormID);

            ViewBag.MusteriID = new SelectList(db.Musteris.ToList(), "ID", "Firma", projeForm.MusteriID);
            ViewBag.Proje = new SelectList(db.ProjeForms.Where(m => m.MusteriID == projeForm.MusteriID && m.PID == null).ToList(), "ID", "Proje", projeForm.PID);
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms.Where(m => m.PID == projeForm.PID).ToList(), "ID", "Form", projeForm.ID);
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GörevYönetimDurumları.ToInt32()), "ID", "Name");
            ViewBag.OncelikID = new SelectList(ComboSub.GetList(Combos.Öncelik.ToInt32()), "ID", "Name");
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GörevYönetimTipleri.ToInt32()), "ID", "Name");
            ViewBag.DepartmanID = new SelectList(ComboSub.GetList(Combos.Departman.ToInt32()), "ID", "Name");

            ViewBag.ID = projeForm.PID;
            ViewBag.PFID = projeForm.ID;
            return PartialView("Edit", tbl);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,ProjeFormID,Sorumlu,Sorumlu2,Sorumlu3,KaliteKontrol,Gorev,Aciklama,OncelikID,DurumID,GorevTipiID,DepartmanID,TahminiBitis,BitisTarih,IslemTip,IslemSira,Kaydeden,KayitTarih,Degistiren,DegisTarih,work,todo,silinenler")] Gorevler gorevler)
        {
            if (ModelState.IsValid)
            {
                if (gorevler.ID == 0)
                {
                    var kontDur = false;
                    var onayVerID = 0;
                    var atandıID = 0;
                    var gelistirmeID = 0;
                    var bilgiTalebiID = 0;
                    var kritikHataID = 0;
                    var kaliteKontrolID = 0;
                    var tipId = Combos.GörevYönetimTipleri.ToInt32();
                    var durumId = Combos.GörevYönetimDurumları.ToInt32();
                    var gorevTipleri = db.ComboItem_Name.Where(a => a.ComboID == tipId).ToList();
                    var gorevDurumları = db.ComboItem_Name.Where(a => a.ComboID == durumId).ToList();
                    foreach (var item in gorevTipleri)
                    {
                        if (item.Name == "Kritik Hata")
                        {
                            kritikHataID = item.ID;
                        }
                        else if (item.Name == "Bilgi Talebi")
                        {
                            bilgiTalebiID = item.ID;
                        }
                        else if (item.Name == "Geliştirme")
                        {
                            gelistirmeID = item.ID;
                        }
                        else if (item.Name == "Kalite Kontrol")
                        {
                            kaliteKontrolID = item.ID;
                        }
                    }
                    foreach (var item in gorevDurumları)
                    {
                        if (item.Name == "Atandı")
                        {
                            atandıID = item.ID;
                        }
                        else if (item.Name == "Onay Ver")
                        {
                            onayVerID = item.ID;
                        }
                    }
                    var maxSira = db.Gorevlers.Where(a=>a.ID>0).ToList().Max(p => p.OncelikID);
                    gorevler.Degistiren = vUser.UserName;
                    gorevler.Kaydeden = vUser.UserName;
                    gorevler.DegisTarih = DateTime.Now;
                    gorevler.KayitTarih = gorevler.DegisTarih;
                    gorevler.IslemTip = 0;
                    gorevler.BitisTarih = null;
                    gorevler.IslemSira = null;
                    gorevler.AktifPasif = true;
                    gorevler.OncelikID = maxSira + 1;
                    if ((gorevler.GorevTipiID == bilgiTalebiID || gorevler.GorevTipiID == gelistirmeID) && vUser.RoleName != "Admin")
                    {
                        gorevler.DurumID = onayVerID;
                    }
                    else if (gorevler.GorevTipiID == kaliteKontrolID)
                    {
                        if (vUser.RoleName == "Admin")
                        {
                            gorevler.DurumID = atandıID;
                        }
                        else
                        {
                            gorevler.DurumID = onayVerID;
                        }

                        gorevler.Kontrol = true;
                        kontDur = true;
                    }
                    else
                    {
                        gorevler.DurumID = atandıID;
                    }
                    db.Gorevlers.Add(gorevler);

                    foreach (var item in gorevler.work)
                    {
                        GorevToDoList grvTDL = new GorevToDoList();
                        grvTDL.Aciklama = item;
                        grvTDL.AktifPasif = true;
                        grvTDL.DegisTarih = DateTime.Now;
                        grvTDL.Degistiren = vUser.UserName;
                        grvTDL.KayitTarih = DateTime.Now;
                        grvTDL.Kaydeden = vUser.UserName;
                        grvTDL.Gorevler = gorevler;
                        grvTDL.OnayDurum = kontDur;
                        grvTDL.Kontrol = kontDur;

                        db.GorevToDoLists.Add(grvTDL);
                    }



                }
                else
                {
                    string[] sl = new string[0];
                    if (gorevler.silinenler != null)
                    {
                        sl = gorevler.silinenler.Split(',');
                    }

                    var tbl = db.Gorevlers.Where(m => m.ID == gorevler.ID).FirstOrDefault();
                    tbl.Sorumlu = gorevler.Sorumlu;
                    tbl.Sorumlu2 = gorevler.Sorumlu2;
                    tbl.Sorumlu3 = gorevler.Sorumlu3;
                    tbl.KaliteKontrol = gorevler.KaliteKontrol;
                    tbl.Gorev = gorevler.Gorev;
                    tbl.Aciklama = gorevler.Aciklama;
                    tbl.OncelikID = gorevler.OncelikID;

                    tbl.DurumID = gorevler.DurumID;
                    tbl.GorevTipiID = gorevler.GorevTipiID;
                    tbl.DepartmanID = gorevler.DepartmanID;
                    tbl.TahminiBitis = gorevler.TahminiBitis;

                    tbl.DurumID = gorevler.DurumID;
                    tbl.GorevTipiID = gorevler.GorevTipiID;
                    tbl.DepartmanID = gorevler.DepartmanID;
                    tbl.TahminiBitis = gorevler.TahminiBitis;
                    tbl.BitisTarih = null;
                    tbl.IslemTip = 0;
                    tbl.IslemSira = null;

                    for (int j = 0; j < sl.Length - 1; j++)
                    {
                        var idd = Convert.ToInt32(sl[j]);
                        var silGrv = db.GorevToDoLists.Where(m => m.ID == idd).FirstOrDefault();
                        silGrv.AktifPasif = false;
                        silGrv.DegisTarih = DateTime.Now;
                        silGrv.Degistiren = vUser.UserName;
                    }
                    for (int i = 0; i < gorevler.work.Length; i++)
                    {
                        if (gorevler.todo[i] == 0)
                        {
                            GorevToDoList grvTDL = new GorevToDoList();
                            grvTDL.Aciklama = gorevler.work[i];
                            grvTDL.AktifPasif = true;
                            grvTDL.DegisTarih = DateTime.Now;
                            grvTDL.Degistiren = vUser.UserName;
                            grvTDL.KayitTarih = DateTime.Now;
                            grvTDL.Kaydeden = vUser.UserName;
                            grvTDL.Gorevler = tbl;

                            db.GorevToDoLists.Add(grvTDL);
                        }
                        else
                        {
                            var id2 = Convert.ToInt32(gorevler.todo[i]);
                            var grv = db.GorevToDoLists.Where(m => m.ID == id2).FirstOrDefault();
                            if (grv.Aciklama.Trim() != gorevler.work[i])
                            {
                                grv.AktifPasif = false;
                                grv.DegisTarih = DateTime.Now;
                                grv.Degistiren = vUser.UserName;

                                GorevToDoList grvTDL = new GorevToDoList();
                                grvTDL.Aciklama = gorevler.work[i];
                                grvTDL.AktifPasif = true;
                                grvTDL.DegisTarih = DateTime.Now;
                                grvTDL.Degistiren = vUser.UserName;
                                grvTDL.KayitTarih = DateTime.Now;
                                grvTDL.Kaydeden = vUser.UserName;
                                grvTDL.Gorevler = tbl;

                                db.GorevToDoLists.Add(grvTDL);
                            }
                        }
                    }

                }
                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, gorevler.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                }
            }
            ViewBag.ProjeFormID = new SelectList(db.ProjeForms, "ID", "Proje", gorevler.ProjeFormID);
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);


        }


        public JsonResult Delete(string Id)
        {
            Gorevler gorev = db.Gorevlers.Find(Id.ToInt32());
            gorev.AktifPasif = false;

            db.Database.ExecuteSqlCommand(string.Format("UPDATE [BIRIKIM].[ong].[GorevTodoList] SET AktifPasif=0 WHERE  GorevID = {0}", Id.ToInt32()));

            db.SaveChanges();

            Result _Result = new Result(true, Id.ToInt32());
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ProjeListesi()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            List<ProjeForm> Prj = db.ProjeForms.Where(m => m.MusteriID == ID && m.PID == null).ToList();
            List<SelectListItem> List = new List<SelectListItem>();
            foreach (ProjeForm item in Prj)
            {
                List.Add(new SelectListItem
                {
                    Selected = false,
                    Text = item.Proje,
                    Value = item.ID.ToString()
                });
            }
            return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);

        }

        public JsonResult FormListesi()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            var ID = id.ToInt32();
            List<ProjeForm> Prj = db.ProjeForms.Where(m => m.PID == ID).ToList();
            List<SelectListItem> List = new List<SelectListItem>();
            foreach (ProjeForm item in Prj)
            {
                List.Add(new SelectListItem
                {
                    Selected = false,
                    Text = item.Form,
                    Value = item.ID.ToString()
                });
            }
            return Json(List.Select(x => new { Value = x.Value, Text = x.Text, Selected = x.Selected }), JsonRequestBehavior.AllowGet);

        }

        public string SorumluListesi()
        {
            List<frmUserss> usr = db.Users.Where(a => a.Aktif == true).Select(m => new frmUserss { ID = m.ID, Kod = m.Kod, AdSoyad = m.AdSoyad, RoleName = m.RoleName }).ToList();
            var json = new JavaScriptSerializer().Serialize(usr);
            return json;

        }

        public string OncelikGuncelle(string Data)
        {
            if (CheckPerm(Perms.SözleşmeTanim, PermTypes.Writing) == false) return null;
            JArray parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            foreach (JObject bds in parameters)
            {
                var id = bds["ID"].ToInt32();
                List<Gorevler> grv = db.Gorevlers.ToList();
                foreach (Gorevler item in grv)
                {
                    if (item.ID == id)
                    {
                        item.OncelikID = bds["OncelikID"].ToInt32();
                    }
                }

            }
            try
            {
                var x = db.SaveChanges();
                return "OK";
            }
            catch (Exception ex)
            {
                return "NO";
            }
        }

        public string GorevReddet(int Data)
        {

            try
            {
                var durumId = Combos.GörevYönetimDurumları.ToInt32();
                var gorevDurumları = db.ComboItem_Name.Where(a => a.ComboID == durumId && a.Name == "Reddedildi").FirstOrDefault();
                db.Database.ExecuteSqlCommand(string.Format("UPDATE [BIRIKIM].[ong].[GorevTodoList] SET AktifPasif=0  WHERE  GorevID = {0}", Data));
                db.Database.ExecuteSqlCommand(string.Format("UPDATE [BIRIKIM].[ong].[Gorevler] SET AktifPasif=0 , DurumID={0} WHERE ID = {1}", gorevDurumları.ID, Data));
                db.SaveChanges();
                return "OK";
            }
            catch (Exception ex)
            {

                return "NO";
            }

        }

        public string GorevOnayla(int Data)
        {

            try
            {
                var durumId = Combos.GörevYönetimDurumları.ToInt32();
                var gorevDurumları = db.ComboItem_Name.Where(a => a.ComboID == durumId && a.Name == "Atandı").FirstOrDefault();
                db.Database.ExecuteSqlCommand(string.Format("UPDATE [BIRIKIM].[ong].[Gorevler] SET DurumID={0} WHERE  ID = {1}", gorevDurumları.ID, Data));
                db.SaveChanges();
                return "OK";
            }
            catch (Exception ex)
            {

                return "NO";
            }

        }
    }
}
