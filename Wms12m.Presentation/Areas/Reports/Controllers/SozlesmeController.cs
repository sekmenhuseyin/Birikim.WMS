using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class SozlesmeController : RootController
    {

        public ActionResult MusteriKrediRiskBilgileri()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public JsonResult MusteriKrediRiskList()
        {
            var list = db.Database.SqlQuery<MusteriKrediRiskBilgileri>(string.Format("[FINSAT6{0}].[wms].[RP_Musteri_KrediRisk_Bilgileri]", vUser.SirketKodu)).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }


        public string SozDetList(int bastarih, int bittarih)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var GS = db.Database.SqlQuery<SozlesmeDetay>(string.Format("[FINSAT6{0}].[wms].[RP_SozlesmeDetayKontrol] @BasTarih = {1}, @BitTarih = {2}", vUser.SirketKodu, bastarih, bittarih)).ToList();
            return json.Serialize(GS);
        }
        public ActionResult SozlesmeDetay()
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult SozlesmeDetayList(int bastarih, int bittarih)
        {
            if (CheckPerm(Perms.Raporlar, PermTypes.Reading) == false) return null;
            //List<RaporGunlukSatis> SU;
            //try
            //{
            //    SU = db.Database.SqlQuery<RaporGunlukSatis>(string.Format("[FINSAT6{0}].[wms].[GunlukSatisRaporu] @BasTarih = {1}, @BitTarih = {2}",vUser.SirketKodu, bastarih, bittarih)).ToList();
            //}
            //catch (Exception ex)
            //{
            //    Logger(ex, "/Reports/Stock/GunlukSatisList");
            //    SU = new List<RaporGunlukSatis>();
            //}
            return PartialView("SozlesmeDetayList");


        }



    }
}