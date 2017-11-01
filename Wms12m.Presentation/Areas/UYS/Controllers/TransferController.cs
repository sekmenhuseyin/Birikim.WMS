using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.UYS.Controllers
{
    public class TransferController : RootController
    {
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}