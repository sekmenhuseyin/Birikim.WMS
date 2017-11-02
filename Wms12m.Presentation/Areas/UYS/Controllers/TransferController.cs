using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.UYS.Controllers
{
    public class TransferController : RootController
    {
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            var liste = db.Database.SqlQuery<frmDepoList>(string.Format("SELECT Depo, DepoAdi FROM FINSAT6{0}.FINSAT6{0}.DEP ORDER BY DepoAdi", db.GetSirketDBs().FirstOrDefault())).ToList();
            ViewBag.GirisDepo = new SelectList(liste, "Depo", "DepoAdi");
            ViewBag.CikisDepo = ViewBag.GirisDepo;
            return View("Index");
        }
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
        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult WaitingList(string Id)
        {
            return PartialView("WaitingList");
        }
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
        /// bekleyen transferi onayla
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(int ID)
        {
            //log
            LogActions("UYS", "Transfer", "Approve", ComboItems.alOnayla, ID);
            //return
            return RedirectToAction("List");
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
                Logger(ex, "Tasks/Delete");
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
                Logger(ex, "Tasks/Delete2");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}