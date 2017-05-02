using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class TerminalController : RootController
    {
        /// <summary>
        /// kullanıcı sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return Redirect("/");
            return View("Index");
        }
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var list = db.UserDetails.ToList();
            return PartialView("List", list);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.UserID = new SelectList(Persons.GetListWithoutTerminal(), "ID", "AdSoyad");
            return PartialView("Editor", new UserDetail());
        }
        /// <summary>
        /// düzenler
        /// </summary>
        public PartialViewResult Edit()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            int UserID = id.ToInt32();
            //return
            var tbl = db.UserDetails.Where(m => m.UserID == UserID).FirstOrDefault();
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tbl.DepoID);
            ViewBag.ID = UserID;
            return PartialView("Editor", tbl);
        }
    }
}