using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class RecordsController : RootController
    {
        /// <summary>
        /// App Logs
        /// </summary>
        public ActionResult App()
        {
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.İşlemKayıtTipi.ToInt32()), "ID", "Name");
            return View("App");
        }

        public JsonResult AppList([DataSourceRequest]DataSourceRequest request, int Id, int Tarih = 0)
        {
            var trh = Tarih.FromOaDate();
            return Json(dbl.AppLogs.Where(m => m.Type == Id && m.Tarih >= trh).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Error logs
        /// </summary>
        public ActionResult Error()
        {
            return View("Error");
        }

        public JsonResult ErrorList([DataSourceRequest]DataSourceRequest request, int Tarih = 0)
        {
            var trh = Tarih.FromOaDate();
            return Json(dbl.ErrorLogs.Where(m => m.DateTime >= trh).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Elmah Logs
        /// </summary>
        public ActionResult Index()
        {
            return View("Index");
        }

        public JsonResult List([DataSourceRequest]DataSourceRequest request, int Tarih = 0)
        {
            var trh = Tarih.FromOaDate();
            return Json(dbl.ELMAH_Error.Where(m => m.TimeUtc >= trh).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Login Logs
        /// </summary>
        public ActionResult Login()
        {
            return View("Login");
        }

        public JsonResult LoginList([DataSourceRequest]DataSourceRequest request, int Tarih = 0)
        {
            var trh = Tarih.FromOaDate();
            return Json(dbl.LoginLogs.Where(m => m.DateTime >= trh).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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