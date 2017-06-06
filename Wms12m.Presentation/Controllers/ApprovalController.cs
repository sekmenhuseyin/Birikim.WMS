using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{

    public class ApprovalController : RootController
    {
        public ActionResult GMOnayHTML()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

    }
}