using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class OnayTeminatController : RootController
    {
        public ActionResult Index()
        {

            return View();
        }
        public PartialViewResult List()
        {
            var KOD = db.Database.SqlQuery<TeminatOnaySelect>(string.Format("[FINSAT6{0}].[wms].[TeminatOnayList]", "33")).ToList();
            return PartialView("List", KOD);
        }

        public ActionResult TeminatTanim()
        {
            return View();
        }
    }
}