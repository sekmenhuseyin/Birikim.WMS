using System.Web.Mvc;
using System.Web.Routing;
using Wms12m.Business;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Presentation.Controllers
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

        public bool CheckPerm(string control, PermTypes permtype)
        {
            var sql = "SELECT " + permtype.ToString() + " FROM usr.RolePerm WHERE (RoleName = '" + vUser.RoleName + "') AND (PermName = '" + control + "')";
            var sonuc=db.Database.SqlQuery<bool>(sql).FirstOrDefaultAsync();
            return sonuc.Result;
        }
        /// <summary>
        /// user için kısayol
        /// </summary>
        protected virtual UserIdentity vUser
        {
            get
            {
                var u = HttpContext.User as CustomPrincipal;
                if (u != null)
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
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Security", action = "Login" }));
                return;
            }
            ViewBag.User = vUser.FirstName;
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
            }
            base.Dispose(disposing);
        }
    }
}