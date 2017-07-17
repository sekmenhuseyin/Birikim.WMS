using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class CrmController : RootController
    {
        /// <summary>
        /// görüşme notları
        /// </summary>
        public ActionResult Meeting()
        {
            var list = db.CRM_GorusmeNotlari().ToList();
            return View("Meeting", list);
        }
        /// <summary>
        /// kurum kartları
        /// </summary>
        public ActionResult Institution()
        {
            var list = db.CRM_KurumKarti().ToList();
            return View("Institution", list);
        }
        /// <summary>
        /// teklif analiz detay
        /// </summary>
        public PartialViewResult InstitutionDetail(string term)
        {
            var tbl = db.CRM_KurumKartiSearch(term).FirstOrDefault();
            return PartialView("InstitutionDetail", tbl);
        }
        /// <summary>
        /// kurum adı arama
        /// </summary>
        public JsonResult GetKurumbyName(string term)
        {
            string sql = "SELECT KODU + ' ' + ADI as value, KODU + ' ' + ADI as lable, KODU as id FROM CAMPUSLNK1.dbo.TBLKURUM WHERE (ADI like '%" + term+ "%') OR (KODU like '%" + term + "%')";
            //return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Reports/CRM/InstitutionDetail");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// teklif analizi
        /// </summary>
        public ActionResult Bid()
        {
            var list = db.CRM_TeklifAnaliz().ToList();
            return View("Bid", list);
        }
        /// <summary>
        /// teklif analiz detay
        /// </summary>
        public PartialViewResult BidDetail(int ID)
        {
            var list = db.CRM_TeklifAnaliz_Detay(ID).ToList();
            return PartialView("BidDetail", list);
        }
    }
}