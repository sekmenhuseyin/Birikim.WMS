using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class ListsController : RootController
    {
        /// <summary>
        /// listeler
        /// </summary>
        public ActionResult Index()
        {
            return View("Index");
        }
        /// <summary>
        /// siparişler
        /// </summary>
        public ActionResult Siparis()
        {
            ViewBag.Siparis = new SelectList(db.IRS.Where(m => m.IslemTur == true && m.Onay == false).ToList(), "ID", "EvrakNo");
            return View("Siparis");
        }
        /// <summary>
        /// siparişi seçince gelen liste
        /// </summary>
        public PartialViewResult GetSiparisDetails()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0") return null;
            int ID = id.ToInt32();
            var list = db.IRS_Detay.Where(m => m.IrsaliyeID == ID).ToList();
            return PartialView("_SiparisDetails", list);
        }
        /// <summary>
        /// raf kaldırmalar
        /// </summary>
        public ActionResult Raf()
        {
            ViewBag.Siparis = new SelectList(db.IRS.Where(m => m.IslemTur == false && m.Onay == false).ToList(), "ID", "EvrakNo");
            return View("Raf");
        }
        /// <summary>
        /// siparişi seçince gelen liste
        /// </summary>
        public PartialViewResult GetRafDetails()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0") return null;
            int ID = id.ToInt32();
            var list = db.IRS_Detay.Where(m => m.IrsaliyeID == ID).ToList();
            return PartialView("_RafDetails", list);
        }
    }
}