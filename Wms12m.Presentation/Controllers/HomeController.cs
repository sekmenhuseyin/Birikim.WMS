using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class HomeController : RootController
    {
        // Anasayfa
        public ActionResult Index()
        {
            ViewBag.user = User;
            return View("Index");
        }
    }
}