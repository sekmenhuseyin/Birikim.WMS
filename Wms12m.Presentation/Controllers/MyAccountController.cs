using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class MyAccountController : RootController
    {
        // GET: MyAccount
        public ActionResult Index()
        {
            return View("Index", Persons.Detail(vUser.Id));
        }
    }
}