using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.System.Controllers
{
    public class LogsController : RootController
    {
        // GET: Logs
        public ActionResult Index()
        {
            using (var dbx = new LOGEntities())
            {
                var list = dbx.ELMAH_Error.ToList();
                return View("Index", list);
            }
        }
        public ActionResult Error()
        {
            using (var dbx = new LOGEntities())
            {
                var list = dbx.ErrorLogs.ToList();
                return View("Error", list);
            }
        }
        public ActionResult Login()
        {
            using (var dbx = new LOGEntities())
            {
                var list = dbx.LoginLogs.ToList();
                return View("Login", list);
            }
        }
        public ActionResult App()
        {
            using (var dbx = new LOGEntities())
            {
                var list = dbx.AppLogs.ToList();
                return View("App", list);
            }
        }
        public JsonResult Delete(string ID)
        {
            try
            {
                var tip = Url.RequestContext.RouteData.Values["id"].ToString();
                db.DeleteLog(tip, ID);
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Result(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}