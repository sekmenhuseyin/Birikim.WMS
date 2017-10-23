using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class HelpController : RootController
    {
        /// <summary>
        /// yardım sayfası
        /// </summary>
        public ActionResult Index()
        {
            var liste = db.FAQs.ToList();
            return View("Index", liste);
        }
        /// <summary>
        /// yardım sayfası listesi
        /// </summary>
        public PartialViewResult List()
        {
            var liste = db.FAQs.ToList();
            return PartialView("List", liste);
        }
    }
}