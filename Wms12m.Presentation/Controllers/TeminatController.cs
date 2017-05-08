using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class TeminatController : RootController
    {
        public ActionResult TeminatOnay()
        {

            return View();
        }
        public PartialViewResult PartialTeminatOnay()
        {
            List<TeminatOnaySelect> KOD = db.Database.SqlQuery<TeminatOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[TeminatOnayList]", "17")).ToList();
            return PartialView("_PartialTeminatOnay", KOD);
        }
    }
}