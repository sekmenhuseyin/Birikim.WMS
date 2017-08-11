using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class IzOrderController : RootController
    {
        // GET: Approvals/IzOrder
        public ActionResult Index(string onayRed)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return Redirect("/");
            if (onayRed == null)
            {
                ViewBag.OnayDurum = "OnayBekleyenler";
            }
            else
            {
                ViewBag.OnayDurum = onayRed;
            }
            return View();
        }

        public PartialViewResult IzOrderList(string Tip)
        {
            if (CheckPerm(Perms.TechnoIKOnaylama, PermTypes.Reading) == false) return null;
            ViewBag.Tip = Tip;
            return PartialView();
        }

        public string IzOrderListData(string tip)
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<TechnoList> ucretBilgi;
            try
            {
                ucretBilgi = db.Database.SqlQuery<TechnoList>(string.Format("[HR03V01].[wms].[TCH_UcretOnaySelect] @Birim='{1}', @Tip='{0}'", tip, MyGlobalVariables.Birim)).ToList();
            }
            catch (Exception ex)
            {
                ucretBilgi = new List<Entity.TechnoList>();
            }
            return json.Serialize(ucretBilgi);
        }
    }
}