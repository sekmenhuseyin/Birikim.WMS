using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

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
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("CountNew");
        }
        /// <summary>
        /// yeni görevi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult SaveNew(int DepoID, string SirketID)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result;
            //kontrol
            int açık = ComboItems.Açık.ToInt32();
            int sayim = ComboItems.KontrolSayım.ToInt32();
            var grv = db.Gorevs.Where(m => m.DepoID == DepoID && m.IR.SirketKod == SirketID && m.GorevTipiID == sayim && m.DurumID == açık).FirstOrDefault();
            if (grv == null)
            {
                int tarih = fn.ToOADate();
                var grvNo = db.SettingsGorevNo(tarih, DepoID).FirstOrDefault();
                var depo = Store.Detail(DepoID).DepoKodu;
                var cevap = db.InsertIrsaliye(SirketID, DepoID, grvNo, grvNo, tarih, SirketID + "-" + depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "").FirstOrDefault();
                grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                grv.DurumID = açık;
                db.SaveChanges();
                _Result = new Result(true);
            }
            else
            {
                if (grv.IR.SirketKod == SirketID)
                    _Result = new Result(false, "Bu görev zaten var");
                else
                {
                    int tarih = fn.ToOADate();
                    var grvNo = db.SettingsGorevNo(tarih, DepoID).FirstOrDefault();
                    var depo = Store.Detail(DepoID).DepoKodu;
                    var cevap = db.InsertIrsaliye(SirketID, DepoID, grvNo, grvNo, tarih, SirketID + "-" + depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "").FirstOrDefault();
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
            int GorevID = tmp[1].ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
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
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == true)
                return Json(new Result(false, "Sayım fişi daha önce oluşturulmuş!"), JsonRequestBehavior.AllowGet);
            //seri kontrol
            var details = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
            if (details == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri.Value < 1 || details.SayimSeri.Value > 199)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            //variables
            int tarih = fn.ToOADate();
            short sirano = 0;
            List<STI> stiList = new List<STI>();
            //loop malkods
            string sql = string.Format("SELECT MalKodu, Birim, Miktar, Stok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                "ISNULL((SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                                                "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = wms.GorevYer.MalKodu) AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)),0) AS Stok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            foreach (var item in list)
            {
                //TODO: burada hata var
                var sti = new STI();
                sti.DefaultValueSet();
                if (item.Miktar < item.Stok)//eğer olması gerekenden az varsa çıkış yapılacak
                    sti.IslemTur = 0;
                else//olması gerekenden fazlaysa giriş yapılacak
                    sti.IslemTur = 1;
                sti.Tarih = tarih;
                sti.KynkEvrakTip = 94;
                sti.SiraNo = sirano;
                sti.IslemTip = 17;
                sti.MalKodu = item.MalKodu;
                sti.Miktar = item.Miktar;
                sti.Miktar2 = item.Stok;
                sti.Birim = item.Birim;
                sti.BirimMiktar = item.Miktar;
                sti.Depo = mGorev.Depo.DepoKodu;
                sti.VadeTarih = tarih;
                sti.EvrakTarih = tarih;
                sti.AnaEvrakTip = 94;
                stiList.Add(sti);
                sirano++;
            }
            //finsat tanımlama
            int EvrakSeriNo = 7000 + details.SayimSeri.Value - 1;
            Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            var sonuc = finsat.SayımVeFarkFişi(stiList, EvrakSeriNo, true, vUser.UserName);
            if (sonuc.Status == true)
            {
                mGorev.IR.EvrakNo = sonuc.Message;
                mGorev.IR.Onay = true;
                db.SaveChanges();
                sonuc.Message = "İşlem tamlandı!";
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayım fark fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountCreateDiff(int GorevID)
        {
            //kontrols
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.LinkEvrakNo != null)
                return Json(new Result(false, "Fark fişi daha önce oluşturulmuş!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == false)
                return Json(new Result(false, "Sayım fişi daha oluşturulmamış!"), JsonRequestBehavior.AllowGet);
            //seri kontrol
            var details = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
            if (details == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri.Value < 1 || details.SayimSeri.Value > 199)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            //variables
            int tarih = fn.ToOADate();
            short sirano = 0;
            List<STI> stiList = new List<STI>();
            //loop malkods
            string sql = string.Format("SELECT MalKodu, Birim, Miktar, Stok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                "ISNULL((SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                                                "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = wms.GorevYer.MalKodu) AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)),0) AS Stok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t WHERE (Stok <> Miktar)", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            foreach (var item in list)
            {
                //TODO: burada hata var
                var sti = new STI();
                sti.DefaultValueSet();
                if (item.Miktar < item.Stok)//eğer olması gerekenden az varsa çıkış yapılacak
                    sti.Miktar = item.Stok - item.Miktar;//fark
                else//olması gerekenden fazlaysa giriş yapılacak
                    sti.Miktar = item.Miktar - item.Stok;//fark
                sti.IslemTur = 0;
                sti.Tarih = tarih;
                sti.KynkEvrakTip = 57;
                sti.SiraNo = sirano;
                sti.IslemTip = 10;
                sti.MalKodu = item.MalKodu;
                sti.Birim = item.Birim;
                sti.BirimMiktar = sti.Miktar;
                sti.Miktar2 = item.Stok;
                sti.Depo = mGorev.Depo.DepoKodu;
                sti.VadeTarih = tarih;
                sti.EvrakTarih = tarih;
                sti.AnaEvrakTip = 57;
                sti.KaynakIrsEvrakNo = mGorev.IR.EvrakNo;
                stiList.Add(sti);
                sirano++;
            }
            //finsat tanımlama
            int EvrakSeriNo = 2600 + details.SayimSeri.Value - 1;
            Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            var sonuc = finsat.SayımVeFarkFişi(stiList, EvrakSeriNo, true, vUser.UserName);
            if (sonuc.Status == true)
            {
                mGorev.IR.LinkEvrakNo = sonuc.Message;
                db.SaveChanges();
                db.Database.ExecuteSqlCommand(string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STI SET KaynakIrsEvrakNo='{1}' WHERE EvrakNo = '{2}' AND KynkEvrakTip = 94", mGorev.IR.SirketKod, sonuc.Message, mGorev.IR.EvrakNo));
                sonuc.Message = "İşlem tamlandı!";
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kontrollü sayım sonrası farkı yer tablosuna kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountSave(int GorevID)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            //get list
            string sql = string.Format("SELECT wms.Yer.KatID, wms.GorevYer.MalKodu, wms.GorevYer.Birim, wms.GorevYer.Miktar, " +
                                    "ISNULL((SELECT Miktar FROM wms.Yer AS Yer2 WHERE (KatID = wms.Yer.KatID) AND (MalKodu = wms.Yer.MalKodu) AND (Birim = wms.Yer.Birim)), 0) as Stok " +
                                    "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID INNER JOIN wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID " +
                                    "WHERE (wms.Gorev.ID = {0})", GorevID);
            var list = db.Database.SqlQuery<frmSiparisToplama>(sql).ToList();
            //loop list
            foreach (var item in list)
            {
                //yerleştirme kaydı yapılır
                var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, item.Birim);
                if (tmp2 == null)
                {
                    tmp2 = new Yer()
                    {
                        KatID = item.KatID,
                        MalKodu = item.MalKodu,
                        Birim = item.Birim,
                        Miktar = item.Miktar
                    };
                    Yerlestirme.Insert(tmp2, 0, vUser.Id);
                }
                else
                {
                    if (item.Miktar != item.Stok)
                    {
                        tmp2.Miktar = 0;
                        Yerlestirme.Update(tmp2, 0, vUser.Id, true, tmp2.Miktar);
                        tmp2.Miktar = item.Miktar;
                        Yerlestirme.Update(tmp2, 0, vUser.Id, false, item.Miktar);
                    }
                }
            }
            return Json(new Result(true, "Kayıt İşlemi tamamlandı"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayım fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult Finish(int GorevID)
        {
            if (CheckPerm("Görev Listesi", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Açık.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            mGorev.DurumID = ComboItems.Tamamlanan.ToInt32();
            db.SaveChanges();
            return Json(new Result(true, mGorev.ID, "İşlem tamlandı!"), JsonRequestBehavior.AllowGet);
        }
    }
}