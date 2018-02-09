using System;
using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class CrmController : RootController
    {
        /// <summary>
        /// teklif analizi
        /// </summary>
        public ActionResult Bid()
        {
            var id = Url.RequestContext.RouteData.Values["id"].ToString2();
            if (id == null || id == "") id = "0";
            ViewBag.id = id;
            return View("Bid");
        }

        /// <summary>
        /// teklif analizi listesi
        /// </summary>
        public PartialViewResult BidList(string Start, string End)
        {
            var list = db.CRM_TeklifAnaliz(Start == null ? DateTime.Today.AddYears(-10) : Start.ToDatetime(), End == null ? DateTime.Today.AddYears(10) : End.ToDatetime()).ToList();
            return PartialView("BidList", list);
        }

        /// <summary>
        /// teklif analiz detay
        /// </summary>
        public PartialViewResult BidDetail(int ID)
        {
            var list = db.CRM_TeklifAnaliz_Detay(ID).ToList();
            return PartialView("BidDetail", list);
        }

        /// <summary>
        /// görüşme notları
        /// </summary>
        public ActionResult Meeting()
        {
            var id = Url.RequestContext.RouteData.Values["id"].ToString2();
            if (id == null || id == "") id = "0";
            ViewBag.id = id;
            return View("Meeting");
        }

        /// <summary>
        /// görüşme notları listesi
        /// </summary>
        public PartialViewResult MeetingList(string Start, string End)
        {
            var list = db.CRM_GorusmeNotlari(Start == null ? DateTime.Today.AddYears(-10) : Start.ToDatetime(), End == null ? DateTime.Today.AddYears(10) : End.ToDatetime()).ToList();
            return PartialView("MeetingList", list);
        }

        /// <summary>
        /// teklif analiz detay
        /// </summary>
        public JsonResult MeetingDetail(int ID)
        {
            return Json(db.CRM_GorusmeNotlariDetay(ID).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// kurum kartları
        /// </summary>
        public ActionResult Institution()
        {
            var id = Url.RequestContext.RouteData.Values["id"].ToString2();
            if (id == null || id == "") id = "0";
            ViewBag.id = id;
            return View("Institution");
        }

        /// <summary>
        /// kurum kartları listesi
        /// </summary>
        public PartialViewResult InstitutionList(string Start, string End)
        {
            var list = db.CRM_KurumKarti(Start == null ? DateTime.Today.AddYears(-10) : Start.ToDatetime(), End == null ? DateTime.Today.AddYears(10) : End.ToDatetime()).ToList();
            return PartialView("InstitutionList", list);
        }
    }
}