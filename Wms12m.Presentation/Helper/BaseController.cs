using Birikim.Helpers;
using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Presentation
{
    public class RootController : Controller
    {
        public Functions fn = new Functions();
        public WMSEntities db = new WMSEntities();
        public LOGEntities dbl = new LOGEntities();
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
        public SqlExper SqlExper;

        /// <summary>
        /// hata kaydını tek yerden kontrol etmek için
        /// </summary>
        public void Logger(Exception ex, string page, string detay = "")
        {
            var inner = "";
            if (ex.InnerException != null)
            {
                inner = ex.InnerException == null ? "" : ex.InnerException.Message;
                if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
            }

            db.Logger(vUser.UserName, "", fn.GetIPAddress(), ex.Message + ": " + detay, inner, page);
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
            db.Database.CommandTimeout = 2000;
            // culture
            var culture = CultureInfo.GetCultureInfo("tr");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
            // login
            if (vUser == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "Security", action = "Login" }));
                return;
            }
            else if (vUser.SirketKodu == "" || vUser.SirketKodu == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "Security", action = "Login" }));
                return;
            }

            // Maintenance
            ViewBag.settings = db.Settings.FirstOrDefault();
            if (ViewBag.settings.Aktif == false && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName != "Maintenance")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "", controller = "Maintenance", action = "Index" }));
                return;
            }
            // developer ise çalışma kontrol
            if (vUser.RoleName == "Developer" && ViewBag.settings.GorevProjesi == true)
                ViewBag.ÇalışmaSüresi = db.Database.SqlQuery<int>(string.Format(@"EXEC BIRIKIM.ong.GetCalismaSure @Username = {0}, @Tarih = '{1}'", vUser.UserName, DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd") : DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3).ToString("yyyy-MM-dd") : DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"))).FirstOrDefault();
            else
                ViewBag.ÇalışmaSüresi = 1000;
            // get assembly version
            var ver = System.IO.File.GetLastWriteTime(typeof(RootController).Assembly.Location).ToString("yyMMdd");
            ViewBag.Version = ver.Substring(0, 1) + "." + ver.Substring(1, 2) + "." + ver.Substring(3);
            // ViewBags
            ViewBag.vUser = vUser;
            ViewBag.Debug = HttpContext.IsDebuggingEnabled;
            ViewBag.UnreadMessages = db.Messages.Where(m => m.MesajTipi == 85 && m.Kime == vUser.UserName && m.Okundu == false).OrderByDescending(m => m.Tarih).ToList();//sadece genel uyarılar
            //sql exper
            if (SqlExper == null)
                SqlExper = new SqlExper(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu);
            // end
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// dispose override
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                dbl.Dispose();
                Combo.Dispose();
                ComboSub.Dispose();
                Corridor.Dispose();
                Dimension.Dispose();
                Floor.Dispose();
                Irsaliye.Dispose();
                Persons.Dispose();
                Roles.Dispose();
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