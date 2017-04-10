using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class TransferController : RootController
    {
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// transfer onaylama
        /// </summary>
        public ActionResult Approve()
        {
            return View("Approve");
        }
    }
}