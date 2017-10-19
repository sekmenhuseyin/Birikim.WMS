using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class HelpController : RootController
    {
        // GET: Help
        public ActionResult Index()
        {
            var liste = db.FAQs.ToList();
            return View("Index", liste);
        }
    }
}