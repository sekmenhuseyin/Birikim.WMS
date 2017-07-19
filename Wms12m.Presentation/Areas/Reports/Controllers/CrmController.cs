using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            return View("Institution");
        }
        public string InstitutionList()
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var list = db.CRM_KurumKarti().ToList();
            return json.Serialize(list);
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