using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.System.Controllers
{
    public class SettingsController : RootController
    {
        /// <summary>
        /// ayarlar
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.Menü, PermTypes.Reading) == false) return Redirect("/");
            return View("Index", db.Settings.FirstOrDefault());
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Index(Setting tbl)
        {
            if (CheckPerm(Perms.Menü, PermTypes.Writing) == false) return Redirect("/");
            if (ModelState.IsValid)
            {
                //db.Entry(tbl).State = EntityState.Modified;
                //db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}