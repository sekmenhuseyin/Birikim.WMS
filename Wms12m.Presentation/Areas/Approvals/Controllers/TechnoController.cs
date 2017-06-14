using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class TechnoController : RootController
    {
        // GET: Approvals/Techno
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult TechnoUcretPartial()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }

        public PartialViewResult TechnoPrimPartial()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }
    }
}