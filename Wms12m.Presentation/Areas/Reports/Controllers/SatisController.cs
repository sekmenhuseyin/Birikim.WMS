using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class SatisController : RootController
    {
        public string UGBSList(int bastarih, int bittarih)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var GS = db.Database.SqlQuery<UrunGrubuBazindaSatis>(string.Format("[FINSAT6{0}].[wms].[RP_UrunGrubu_BazindaSatis] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            return json.Serialize(GS);
        }
        public ActionResult UrunGrubuBazindaSatis()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult UrunGrubuBazindaSatisList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            return PartialView("UrunGrubuBazindaSatisList");
        }

        public string MSSKList(int bastarih, int bittarih)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var GS = db.Database.SqlQuery<MusteriSozlesmeSatisKontrol>(string.Format("[FINSAT6{0}].[wms].[RP_Musteri_SozlesmeSatis_Kontrol] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            return json.Serialize(GS);
        }
        public ActionResult MusteriSozlesmeSatisKontrol()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult MusteriSozlesmeSatisKontrolList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            return PartialView("MusteriSozlesmeSatisKontrolList");
        }
    }
}