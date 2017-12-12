using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Genel Hatalar
        /// </summary>
        public ActionResult Index() => View("Index");

        /// <summary>
        /// 404 sayfa bulunamadı
        /// </summary>
        public ActionResult Lost()
        {
            Response.StatusCode = 200;
            return View("Lost");
        }
    }
}