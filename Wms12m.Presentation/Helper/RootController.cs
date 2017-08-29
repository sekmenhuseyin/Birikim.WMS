using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Wms12m.Business;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Presentation
{
    public class RootController : Controller
    {
        public Functions fn = new Functions();
        public WMSEntities db = new WMSEntities();
        public Combo Combo = new Combo();
        public ComboSub ComboSub = new ComboSub();
        public Corridor Corridor = new Corridor();
        public Dimension Dimension = new Dimension();
        public Floor Floor = new Floor();
        public Irsaliye Irsaliye = new Irsaliye();
        public Persons Persons = new Persons();
        public Roles Roles = new Roles();
        public Section Section = new Section();
        public Shelf Shelf = new Shelf();
        public IrsaliyeDetay IrsaliyeDetay = new IrsaliyeDetay();
        public Store Store = new Store();
        public Task Task = new Task();
        public TaskYer TaskYer = new TaskYer();
        public Yerlestirme Yerlestirme = new Yerlestirme();
        public Transfers Transfers = new Transfers();
        public PersonDetails PersonDetails = new PersonDetails();
        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(Exception ex, string page)
        {
            string inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }
            db.Logger(vUser.UserName, "", fn.GetIPAddress(), ex.Message, inner, page);
        }
        /// <summary>
        /// işlem kaydı
        /// </summary>
        public void LogActions(string area, string controller, string action, ComboItems type, int ID, string details = "", string request = "")
        {
            db.LogActions("WMS", area, controller, action, type.ToInt32(), ID, request, details, vUser.UserName, fn.GetIPAddress());
        }
        /// <summary>
        /// her bir sayfa için yetki kontrolü yapar
        /// </summary>
        public bool CheckPerm(Perms permName, PermTypes permtype)
        {
            var sonuc = db.GetPermissionFor(vUser.Id, vUser.RoleName, permName.ToString(), "WMS", permtype.ToString()).FirstOrDefault().Value;
            return sonuc;
        }
        /// <summary>
        /// user için kısayol
        /// </summary>
        protected virtual UserIdentity vUser
        {
            get
            {
                if (HttpContext.User is CustomPrincipal u)
                    return u.AppIdentity.User;
                return null;
            }
        }
        /// <summary>
        /// actiona olmadan hemen önce
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (vUser == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "Security", action = "Login" }));
                return;
            }
            ViewBag.User = vUser.FirstName;
            ViewBag.UserID = vUser.Id;
            ViewBag.settings = db.Settings.FirstOrDefault();
            ViewBag.Debug = HttpContext.IsDebuggingEnabled;
            if (ViewBag.settings.Aktif == false && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Maintenance")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "Maintenance", action = "Index" }));
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// actiondan sonra
        /// </summary>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                Combo.Dispose();
                ComboSub.Dispose();
                Corridor.Dispose();
                Dimension.Dispose();
                Floor.Dispose();
                Irsaliye.Dispose();
                Persons.Dispose();
                Section.Dispose();
                Shelf.Dispose();
                IrsaliyeDetay.Dispose();
                Store.Dispose();
                Task.Dispose();
                TaskYer.Dispose();
                Yerlestirme.Dispose();
                Transfers.Dispose();
                PersonDetails.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}