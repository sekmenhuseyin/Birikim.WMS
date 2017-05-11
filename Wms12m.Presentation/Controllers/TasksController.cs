using OnikimCore.GunesCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class TasksController : RootController
    {

        /// <summary>
        /// görev anasayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Index");
        }
        /// <summary>
        /// listeyi gösterir
        /// </summary>
        public PartialViewResult List(int Id)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return null;
            var list = Task.GetList(Id, vUser.DepoId);
            return PartialView("List", list);
        }
        /// <summary>
        /// görev ayrıntıları
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return null;
            var list = db.GetIrsDetayfromGorev(ID);
            return PartialView("Details", list);
        }
        /// <summary>
        /// görev düzenle
        /// </summary>
        public PartialViewResult GorevDetailPartial(int id)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return null;
            var list = Task.Detail(id);
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GorevTipleri.ToInt32()), "ID", "Name", list.GorevTipiID);
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name", list.DurumID);
            return PartialView("_GorevDetailPartial", list);
        }
        /// <summary>
        /// görev güncelle
        /// </summary>
        public JsonResult Update(frmGorev tbl)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            //update
            Task tmpTable = new Task();
            Result _Result = tmpTable.Update(tbl);
            //get list
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görev sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result();
            try
            {
                db.DeleteFromGorev(ID);
                _Result.Status = true; _Result.Id = ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Tasks/Delete");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görevli ata
        /// </summary>
        [HttpPost]
        public PartialViewResult GorevliAta()
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            Int32 ID = Convert.ToInt32(id);
            var list = Task.Detail(ID);
            ViewBag.Gorevli = new SelectList(Persons.GetList(), "Kod", "AdSoyad", list.Gorevli);
            return PartialView("GorevliAta", list);
        }
        /// <summary>
        /// görevliyi kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GorevliKaydet(frmGorevli tbl)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Redirect("/");
            Task tmpTable = new Task();
            Result _Result = tmpTable.UpdateGorevli(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// kontrollü sayin sayfası
        /// </summary>
        public ActionResult Count()
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Count");
        }
        /// <summary>
        /// listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult CountList(string Id)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return null;
            //id'ye göre liste döner
            var list = Task.GetList(ComboItems.KontrolSayım.ToInt32(), Id.ToInt32());
            return PartialView("CountList", list);
        }
        /// <summary>
        /// yeni kontrollü sayım görevi ekle
        /// </summary>
        [HttpPost]
        public PartialViewResult New()
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return null;
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            var list = db.GetSirketDBs().ToList();
            List<frmSirkets> liste = new List<frmSirkets>();
            foreach (var item in list) { liste.Add(new frmSirkets() { Kod = item }); }
            ViewBag.SirketKod = new SelectList(liste, "Kod", "Kod");
            return PartialView("CountNew");
        }
        /// <summary>
        /// yeni görevi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult SaveNew(int DepoID, string SirketKod)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result;
            //kontrol
            int açık = ComboItems.Açık.ToInt32();
            int sayim = ComboItems.KontrolSayım.ToInt32();
            var grv = db.Gorevs.Where(m => m.DepoID == DepoID && m.IR.SirketKod == SirketKod && m.GorevTipiID == sayim && m.DurumID == açık).FirstOrDefault();
            if (grv == null)
            {
                int tarih = fn.ToOADate();
                var grvNo = db.SettingsGorevNo(tarih, DepoID).FirstOrDefault();
                var depo = Store.Detail(DepoID).DepoKodu;
                var cevap = db.InsertIrsaliye(SirketKod, DepoID, grvNo, grvNo, tarih, SirketKod + "-" + depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "").FirstOrDefault();
                grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                grv.DurumID = açık;
                db.SaveChanges();
                _Result = new Result(true);
            }
            else
            {
                if (grv.IR.SirketKod == SirketKod)
                    _Result = new Result(false, "Bu görev zaten var");
                else
                {
                    int tarih = fn.ToOADate();
                    var grvNo = db.SettingsGorevNo(tarih, DepoID).FirstOrDefault();
                    var depo = Store.Detail(DepoID).DepoKodu;
                    var cevap = db.InsertIrsaliye(SirketKod, DepoID, grvNo, grvNo, tarih, SirketKod + "-" + depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "").FirstOrDefault();
                    grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                    grv.DurumID = açık;
                    db.SaveChanges();
                    _Result = new Result(true);
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kontrollü sayima ait fark sayfası
        /// </summary>
        public PartialViewResult CountFark()
        {
            if (CheckPerm("Görev Listesi", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string[] tmp = id.ToString().Split('-');
            string sql = "";
            if (tmp[0] != "1")//sadece fark liste
                sql = " WHERE (Stok <> Miktar)";
            int durumID = ComboItems.Açık.ToInt32();
            int GorevID = tmp[1].ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            sql = string.Format("SELECT MalKodu, Birim, Miktar, Stok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                "ISNULL((SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                                                "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = wms.GorevYer.MalKodu) AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)),0) AS Stok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t{3}", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID, sql);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            return PartialView("CountFark", list);
        }
        /// <summary>
        /// sayım fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountCreate(int GorevID)
        {
            //kontrols
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Açık.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            //evrak no bul
            DevHelper.Ayarlar.SetConStr(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString);
            DevHelper.Ayarlar.SirketKodu = mGorev.IR.SirketKod;
            Genel_Islemler GI = new Genel_Islemler(mGorev.IR.SirketKod);
            string evrakNo = GI.EvrakNo_Getir(7000);
            List<STI> stiList = new List<STI>();
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            short sirano = 0;
            //loop malkods
            var list = mGorev.GorevYers.GroupBy(m => new { m.MalKodu, m.Birim }).Select(m => new { m.Key.MalKodu, m.Key.Birim, Miktar = m.Sum(n => n.Miktar) }).ToList();
            foreach (var item in list)
            {
                var sti = new STI();
                sti.DefaultValueSet();
                sti.IslemTur = 1;
                sti.EvrakNo = evrakNo;
                sti.Tarih = tarih;
                sti.KynkEvrakTip = 94;
                sti.SiraNo = sirano;
                sti.IslemTip = 17;
                sti.MalKodu = item.MalKodu;
                sti.Miktar = item.Miktar;
                sti.Birim = item.Birim;
                sti.BirimMiktar = item.Miktar;
                sti.Depo = mGorev.Depo.DepoKodu;
                sti.VadeTarih = tarih;
                sti.EvrakTarih = tarih;
                sti.AnaEvrakTip = 94;
                sti.Kaydeden = vUser.UserName;
                sti.KayitTarih = tarih;
                sti.KayitSaat = saat;
                sti.KayitKaynak = 153;
                sti.KayitSurum = "9.01.028";
                sti.Degistiren = vUser.UserName;
                sti.DegisTarih = tarih;
                sti.DegisSaat = saat;
                sti.DegisKaynak = 153;
                sti.DegisSurum = "9.01.028";
                sti.CheckSum = 12;
                stiList.Add(sti);
                sirano++;
            }
            //sqlexper loop
            SqlExper sqlexper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            foreach (var item in stiList)
            {
                sqlexper.Insert(item);
            }
            var sonuc = sqlexper.AcceptChanges();
            if (sonuc.Status == true)
            {
                mGorev.IR.EvrakNo = evrakNo;
                db.SaveChanges();
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayım fark fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountCreateDiff(int GorevID)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);

            return Json(new Result(true), JsonRequestBehavior.AllowGet);
        }
    }
}