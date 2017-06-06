using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class OrderController : RootController
    {
        public ActionResult Siparis_SM()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Siparis_SM_List()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSM]", "17")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Siparis_SPGMY()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Siparis_SPGMY_List()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSPGMY]", "17")).ToList();
            return PartialView(KOD);
        }
        public ActionResult Siparis_GM()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public PartialViewResult Siparis_GM_List()
        {
            if (CheckPerm("Sipariş Onaylama", PermTypes.Reading) == false) return null;
            var KOD = db.Database.SqlQuery<SMSiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", "17")).ToList();
            return PartialView(KOD);
        }
        public JsonResult Siparis_Onay(string Data, int OnayTip, bool OnaylandiMi)
        {

            if (CheckPerm("Sipariş Onaylama", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(true);
            JArray parameters = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(Request["Data"]);
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                foreach (string insertObj in parameters)
                {
                    if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 1)); }
                    else if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 2, 1)); }
                    else if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 1, 1)); }
                    else if (OnayTip == 3 && OnaylandiMi == false)//GMRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 3, 0)); }
                    else if (OnayTip == 2 && OnaylandiMi == false)//SPGMYRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 2, 0)); }
                    else if (OnayTip == 1 && OnaylandiMi == false)//SMRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnayTip={3},@OnaylandiMi={4}", "17", insertObj, vUser.UserName, 1, 0)); }
                }

                try
                {
                    dbContextTransaction.Commit();

                    _Result.Status = true;
                    _Result.Message = "İşlem Başarılı ";
                }
                catch (Exception)
                {

                    _Result.Status = false;
                    _Result.Message = "Hata Oluştu.";
                }
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
    }
}