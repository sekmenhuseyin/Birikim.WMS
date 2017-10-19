using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class HelpController : RootController
    {
        // GET: Help
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}