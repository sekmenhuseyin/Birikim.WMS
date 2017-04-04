using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class HomeController : RootController
    {
        // Anasayfa
        public ActionResult Index()
        {
            var ozet = db.GetHomeSummary().FirstOrDefault();
            return View("Index", ozet);
        }
    }
}