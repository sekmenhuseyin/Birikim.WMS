using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class ApprovalController : RootController
    {
        public ActionResult Cek_SPGMY()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_SPGMY_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnaySPGMY]", "33")).ToList();
            return PartialView(KOD);
        }

        public ActionResult Cek_MIGMY()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_MIGMY_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayMIGMY]", "33")).ToList();
            return PartialView(KOD);
        }

        public ActionResult Cek_GM()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Cek_GM_List()
        {
            if (CheckPerm("Çek Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<CekOnaySelect>(string.Format("[FINSAT6{0}].[wms].[CekOnayGM]", "33")).ToList();
            return PartialView(KOD);
        }
    }
}