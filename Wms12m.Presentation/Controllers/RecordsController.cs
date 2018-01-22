using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class RecordsController : RootController
    {
        /// <summary>
        /// Elmah Logs
        /// </summary>
        public ActionResult Index()
        {
            var list = dbl.ELMAH_Error.ToList();
            return View("Index", list);
        }
        /// <summary>
        /// Error logs
        /// </summary>
        public ActionResult Error()
        {
            var list = dbl.ErrorLogs.ToList();
            return View("Error", list);
        }
        /// <summary>
        /// Login Logs
        /// </summary>
        public ActionResult Login()
        {
            var list = dbl.LoginLogs.ToList();
            return View("Login", list);
        }
        /// <summary>
        /// App Logs
        /// </summary>
        public ActionResult App()
        {
            var list = dbl.AppLogs.ToList();
            return View("App", list);
        }
        /// <summary>
        /// Delete
        /// </summary>
        public JsonResult Delete(string ID)
        {
            var tip = Url.RequestContext.RouteData.Values["id"].ToString();
            if (tip == "Elmah")
            {
                var guid = new Guid(ID);
                dbl.ELMAH_Error.Remove(dbl.ELMAH_Error.Where(m => m.ErrorId == guid).FirstOrDefault());
            }
            else
            {
                var id = ID.ToInt32();
                if (tip == "Error")
                    dbl.ErrorLogs.Remove(dbl.ErrorLogs.Where(m => m.ID == id).FirstOrDefault());
                else if (tip == "Login")
                    dbl.LoginLogs.Remove(dbl.LoginLogs.Where(m => m.ID == id).FirstOrDefault());
                else if (tip == "App")
                    dbl.AppLogs.Remove(dbl.AppLogs.Where(m => m.ID == id).FirstOrDefault());
            }
            try
            {
                dbl.SaveChanges();
                return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Result(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}