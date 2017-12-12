using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class OrderController : RootController
    {
        /// <summary>
        /// SM sayfası
        /// </summary>
        public ActionResult SM()
        {
            if (CheckPerm(Perms.SiparişOnaylamaSM, PermTypes.Reading) == false) return Redirect("/");
            var kOD = db.Database.SqlQuery<SiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSM]", vUser.SirketKodu)).ToList();
            ViewBag.OnayTip = 1;
            ViewBag.baslik = "SM";
            return View("Index", kOD);
        }
        /// <summary>
        /// SPGMY sayfası
        /// </summary>
        public ActionResult SPGMY()
        {
            if (CheckPerm(Perms.SiparişOnaylamaSPGMY, PermTypes.Reading) == false) return Redirect("/");
            var KOD = db.Database.SqlQuery<SiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListSPGMY]", vUser.SirketKodu)).ToList();
            ViewBag.OnayTip = 2;
            ViewBag.baslik = "SPGMY";
            return View("Index", KOD);
        }
        /// <summary>
        /// GM sayfası
        /// </summary>
        public ActionResult GM()
        {
            if (CheckPerm(Perms.SiparişOnaylamaGM, PermTypes.Reading) == false) return Redirect("/");
            var KOD = db.Database.SqlQuery<SiparisOnaySelect>(string.Format("[FINSAT6{0}].[wms].[SiparisOnayListGM]", vUser.SirketKodu)).ToList();
            ViewBag.OnayTip = 3;
            ViewBag.baslik = "GM";
            return View("Index", KOD);
        }
        /// <summary>
        /// Onaylama
        /// </summary>
        public JsonResult Onay(string Data, int OnayTip, bool OnaylandiMi)
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true);
            var parameters = JsonConvert.DeserializeObject<JArray>(Request["Data"]);
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                foreach (string insertObj in parameters)
                {
                    var OTip = db.Database.SqlQuery<short>(string.Format("SELECT OnayTip FROM FINSAT6{0}.FINSAT6{0}.SiparisOnay WHERE EvrakNo='{1}'", vUser.SirketKodu, insertObj)).FirstOrDefault();
                    if (OnayTip == 3 && OnaylandiMi == true)//GMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnaylayanTip={3},@OnaylandiMi={4},@OnayTip={5}", vUser.SirketKodu, insertObj, vUser.UserName, 3, 1, OTip)); }
                    else if (OnayTip == 2 && OnaylandiMi == true)//SPGMYOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnaylayanTip={3},@OnaylandiMi={4},@OnayTip={5}", vUser.SirketKodu, insertObj, vUser.UserName, 2, 1, OTip)); }
                    else if (OnayTip == 1 && OnaylandiMi == true)//SMOnay
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnaylayanTip={3},@OnaylandiMi={4},@OnayTip={5}", vUser.SirketKodu, insertObj, vUser.UserName, 1, 1, OTip)); }
                    else if (OnayTip == 3 && OnaylandiMi == false)//GMRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnaylayanTip={3},@OnaylandiMi={4},@OnayTip={5}", vUser.SirketKodu, insertObj, vUser.UserName, 3, 0, OTip)); }
                    else if (OnayTip == 2 && OnaylandiMi == false)//SPGMYRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnaylayanTip={3},@OnaylandiMi={4},@OnayTip={5}", vUser.SirketKodu, insertObj, vUser.UserName, 2, 0, OTip)); }
                    else if (OnayTip == 1 && OnaylandiMi == false)//SMRet
                    { db.Database.ExecuteSqlCommand(string.Format("[FINSAT6{0}].[wms].[SP_SiparisOnay] @EvrakNo = '{1}',@Kullanici = '{2}',@OnaylayanTip={3},@OnaylandiMi={4},@OnayTip={5}", vUser.SirketKodu, insertObj, vUser.UserName, 1, 0, OTip)); }
                }

                try
                {
                    db.SaveChanges();
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
        /// <summary>
        /// bekleyenler
        /// </summary>
        public string BekleyenOnaylar()
        {
            if (CheckPerm(Perms.SiparişOnaylama, PermTypes.Reading) == false) return null;
            var BO = db.Database.SqlQuery<BekleyenOnaylar>(string.Format("[FINSAT6{0}].[wms].[DB_BekleyenOnaylar]", vUser.SirketKodu)).ToList();
            var json = new JavaScriptSerializer().Serialize(BO);
            return json;
        }
    }
}