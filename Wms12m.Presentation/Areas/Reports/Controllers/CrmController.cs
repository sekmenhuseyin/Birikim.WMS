using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class CrmController : RootController
    {
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
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult MeetingList()
        {
            var list = db.CRM_GorusmeNotlari(null, null).ToList();
            return PartialView("MeetingList", list);
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
        /// teklif analiz detay
        /// </summary>
        public PartialViewResult BidDetail(int ID)
        {
            var list = db.CRM_TeklifAnaliz_Detay(ID).ToList();
            return PartialView("BidDetail", list);
        }
    }
}