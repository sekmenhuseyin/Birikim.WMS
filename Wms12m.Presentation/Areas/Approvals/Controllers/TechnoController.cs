using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class TechnoController : RootController
    {
        // GET: Approvals/Techno
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Ucret_List()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }

        public PartialViewResult Prim_List()
        {
            if (CheckPerm("Fiyat Onaylama", PermTypes.Reading) == false) return null;
            return PartialView();
        }

        public string UcretListData()
        {
            JavaScriptSerializer json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            List<TechnoUcretList> ucretBilgi;
            try
            {
                ucretBilgi = db.Database.SqlQuery<TechnoUcretList>(string.Format("[HR0312M].[dbo].[TCH_UcretOnaySelect]")).ToList();
            }
            catch (Exception ex)
            {
                ucretBilgi = new List<Entity.TechnoUcretList>();
            }
            return json.Serialize(ucretBilgi);
        }
    }
}