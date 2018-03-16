using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class PerformansController : RootController
    {
        public string MusPerList(int bastarih, int bittarih)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var GS = db.Database.SqlQuery<MusteriPerformans>(string.Format("[FINSAT6{0}].[wms].[RP_Musteri_Performans_Raporu] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            return json.Serialize(GS);
        }

        public ActionResult MusteriPerformans()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult MusteriPerformansList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            return PartialView("MusteriPerformansList");
        }

        public string MusTemPerList(int bastarih, int bittarih)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var GS = db.Database.SqlQuery<MusteriTemsilcisiPerformans>(string.Format("[FINSAT6{0}].[wms].[RP_MusteriTemsilcisi_Performans_Raporu] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            return json.Serialize(GS);
        }

        public ActionResult MusteriTemsilcisiPerformans()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public PartialViewResult MusteriTemsilcisiPerformansList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            return PartialView("MusteriTemsilcisiPerformansList");
        }
    }
}