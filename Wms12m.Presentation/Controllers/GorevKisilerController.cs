using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class GorevKisilerController : RootController
    {
        // GET: GorevKisiler
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GorevKisiListesiPartial()
        {
            return PartialView();
        }

        public string GorevKisiListesiData()
        {
            var KL = db.Database.SqlQuery<Kullanici>(string.Format("[OnikimGorev].[ong].[Kullanici]")).ToList();
            var json = new JavaScriptSerializer().Serialize(KL);
            return json;
        }
    }
}