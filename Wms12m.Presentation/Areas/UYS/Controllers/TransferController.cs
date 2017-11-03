using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.UYS.Controllers
{
    public class TransferController : RootController
    {
        #region planlama
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            var liste = db.Database.SqlQuery<frmDepoList>(string.Format("SELECT Depo, DepoAdi + ' [' + Depo + ']' as DepoAdi FROM FINSAT6{0}.FINSAT6{0}.DEP ORDER BY DepoAdi", db.GetSirketDBs().FirstOrDefault())).ToList();
            ViewBag.GirisDepo = new SelectList(liste, "Depo", "DepoAdi");
            ViewBag.CikisDepo = ViewBag.GirisDepo;
            return View("Index");
        }
        /// <summary>
        /// planlamadaki 1. adımdaki malzeme listesi
        /// </summary>
        public PartialViewResult List(string Id)
        {
            try
            {
                return PartialView("List");
            }
            catch (Exception ex)
            {
                Logger(ex, "UYS/Transfer/List");
                return PartialView("List", new List<frmTransferMalzemeler>());
            }

        }
        #endregion
        #region onaylama
        /// <summary>
        /// onay bekleyen transfer sayfası
        /// </summary>
        public ActionResult Waiting()
        {
            var liste = db.Database.SqlQuery<frmWaitingList>(string.Format(@"SELECT StiNo, Kod2 + ' => ' + Kod3 + ' (' + BIRIKIM.wms.fnFormatDateFromInt(BasTarih) + ')' as Depo FROM UYSPLN6{0}.UYSPLN6{0}.EMG WHERE(StiNo NOT IN
                                                                                    (SELECT StiNo FROM UYSPLN6{0}.UYSPLN6{0}.EMG AS EMG_1 WHERE (TrsfrNo <> '') GROUP BY StiNo))
                                                                            ORDER BY BasTarih, Kod2", db.GetSirketDBs().FirstOrDefault())).ToList();
            ViewBag.DurumID = new SelectList(liste, "StiNo", "Depo");
            return View("Waiting");
        }
        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult WaitingList(string Id)
        {
            return PartialView("WaitingList");
        }
        #endregion
        #region ortak
        /// <summary>
        /// transfere ait mallar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            //return
            var result = db.Transfer_Detay.Where(m => m.TransferID == ID).Select(m => new frmMalKoduMiktar { MalKodu = m.MalKodu, Miktar = m.Miktar, Birim = m.Birim }).ToList();
            return PartialView("Details", result);
        }
        /// <summary>
        /// transfer sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            Result _Result = new Result();
            try
            {

                _Result.Status = true; _Result.Id = ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Transfer/Delete");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// transfer detay sil
        /// </summary>
        public JsonResult Delete2(int ID)
        {
            Result _Result = new Result();
            try
            {
                db.SaveChanges();
                _Result = new Result(true, ID);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Transfer/Delete2");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// ürün stoğu bul
        /// </summary>
        public JsonResult GetStock(string MalKodu, int Tarih, string SeriNo, string Depo)
        {
            if (SeriNo != "") SeriNo = ("000000" + SeriNo).Right(6); else SeriNo = "      ";
            var sql = string.Format(@"SELECT '{1}' as MalKodu, isnull(sum(case when IslemTur = 0 then Miktar else -Miktar end), 0) as Miktar, (SELECT Birim1 FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalKodu = '{1}')) as Birim
                                    FROM FINSAT6{0}.FINSAT6{0}.STI WITH (nolock) left join FINSAT6{0}.FINSAT6{0}.STK WITH (nolock) ON STK.MalKodu = STI.MalKodu 
                                    where STI.MalKodu = '{1}' and Tarih <= {2} and SeriNo = '  {3}' and Depo ='{4}'  and IrsFat <> 2 and KynkEvrakTip <> 95 and KynkEvrakTip not in (141,142,143,144) and not (KynkEvrakTip in (68,69) and ErekIIFKEvrakTip in (5,2) and IrsFat = 3)
                                    GROUP by STI.MalKodu, Depo,SeriNo", db.GetSirketDBs().FirstOrDefault(), MalKodu, Tarih, SeriNo, Depo);
            return Json(db.Database.SqlQuery<frmMalKoduMiktar>(sql).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// transfer kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(frmUysTransfer tbl)
        {
            tbl.AraDepo = "TD";
            //send to db
            var uysf = new UYSF(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, db.GetSirketDBs().FirstOrDefault());
            var sonuc = uysf.DepoTransfer(tbl, false, vUser.UserName, 1);
            if (sonuc.Status == true)
            {
                //sonuc.Data = evrakNo;
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// bekleyen transferi onayla
        /// </summary>
        [HttpPost]
        public JsonResult Approve(int ID)
        {
            Result _Result = new Result();
            try
            {

                _Result.Status = true; _Result.Id = 1;
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Transfer/Approve");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}