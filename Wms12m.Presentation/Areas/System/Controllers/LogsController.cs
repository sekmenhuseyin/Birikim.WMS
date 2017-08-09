using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.System.Controllers
{
    public class LogsController : RootController
    {
        // GET: Logs
        public ActionResult Index()
        {
            var list = db.GetELMAH_Error().ToList();
            return View("Index", list);
        }
        public ActionResult Error()
        {
            var list = db.GetErrorLog().ToList();
            return View("Error", list);
        }
        public ActionResult Login()
        {
            var list = db.GetLoginLog().ToList();
            return View("Login", list);
        }
        public ActionResult App()
        {
            var list = db.GetAppLog().ToList();
            return View("App", list);
        }
        public JsonResult Delete(string ID)
        {
            try
            {
                var tip = Url.RequestContext.RouteData.Values["id"].ToString();
                db.DeleteLog(tip, ID);
                return Json(new Result(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Result(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}