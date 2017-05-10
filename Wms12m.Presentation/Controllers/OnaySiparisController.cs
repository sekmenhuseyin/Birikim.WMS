using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
namespace Wms12m.Presentation.Controllers
{
    public class OnaySiparisController : RootController
    {
        public ActionResult SM()
        {

            return View();
        }
        public PartialViewResult PartialSM()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSM]", "17")).ToList();
            return PartialView("_PartialSM", KOD);
        }

        public ActionResult SPGMY()
        {

            return View();
        }
        public PartialViewResult PartialSPGMY()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListSPGMY]", "17")).ToList();
            return PartialView("_PartialSPGMY", KOD);
        }

        public ActionResult GM()
        {

            return View();
        }
        public PartialViewResult PartialGM()
        {
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[dbo].[SiparisOnayListGM]", "17")).ToList();
            return PartialView("_PartialGM", KOD);
        }

        public bool SiparisOnay(string EvrakNo, int OnayTip, bool OnaylandiMi) {

            bool Result = true;

            try
            {
                if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
                { db.Database.ExecuteSqlCommand(string.Format(@"[FINSAT6{0}].[dbo].[SP_SiparisOnay] @EvrakNo='{1}', @Kullanici ='{2}', @OnayTip = {3}, @OnaylandiMi= {4}", "17", EvrakNo, vUser.UserName, 3, 1)); }
                else if (OnayTip == 3 && OnaylandiMi == false)//GMRet
                { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SP_SiparisOnay] @EvrakNo='{1}', @Kullanici ='{2}', @OnayTip = {3}, @OnaylandiMi= {4}", "17", EvrakNo, vUser.UserName, 3, 0)); }
                else if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
                { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SP_SiparisOnay] @EvrakNo='{1}', @Kullanici ='{2}', @OnayTip = {3}, @OnaylandiMi= {4}", "17", EvrakNo, vUser.UserName, 2, 1)); }
                else if (OnayTip == 2 && OnaylandiMi == false)//SPGMYRet
                { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SP_SiparisOnay] @EvrakNo='{1}', @Kullanici ='{2}', @OnayTip = {3}, @OnaylandiMi= {4}", "17", EvrakNo, vUser.UserName, 2, 0)); }
                else if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
                { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SP_SiparisOnay] @EvrakNo='{1}', @Kullanici ='{2}', @OnayTip = {3}, @OnaylandiMi= {4}", "17", EvrakNo, vUser.UserName, 1, 1)); }
                /********************************************************/
                else if (OnayTip == 1 && OnaylandiMi == false)//SMRet
                { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[dbo].[SP_SiparisOnay] @EvrakNo='{1}', @Kullanici ='{2}', @OnayTip = {3}, @OnaylandiMi= {4}", "17", EvrakNo, vUser.UserName, 1, 0)); }
            }
            catch (System.Exception)
            {

                Result = false;
            }

            return Result;
        }

    }
}