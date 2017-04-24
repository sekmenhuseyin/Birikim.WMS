using System;
using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class HomeController : RootController
    {
        /// <summary>
        /// Anasayfa
        /// </summary>
        public ActionResult Index()
        {
            var ozet = db.GetHomeSummary().FirstOrDefault();
            return View("Index", ozet);
        }
        /// <summary>
        /// child view for menu
        /// </summary>
        /// <param name="mYeri">menu yeri no</param>
        /// <param name="mUstNo">üst menu no</param>
        /// <param name="url">current url</param>
        [ChildActionOnly]
        public PartialViewResult Menu(byte mYeri, short mUstNo, string url)
        {
            var uri = new Uri(Request.Url.AbsoluteUri);
            url = url.Replace(uri.Scheme + "://" + uri.Host + "/", ""); if (url == "") url = "/";
            ViewBag.ustMenu = mUstNo;
            ViewBag.aktifNo = db.MenuFindAktif(ComboItems.WMS.ToInt32(), mYeri, "Admin", mUstNo, url).FirstOrDefault();
            var tablo = db.MenuGetirici(ComboItems.WMS.ToInt32(), mYeri, "Admin", mUstNo).ToList();
            return PartialView("../Shared/_MenuList", tablo);
        }
    }
}