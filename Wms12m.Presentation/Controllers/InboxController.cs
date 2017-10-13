using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class InboxController : RootController
    {
        /// <summary>
        /// gelen kutusu
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// gelen bildirimler
        /// </summary>
        public ActionResult Notifications()
        {
            return View();
        }
        /// <summary>
        /// bildiri listesi
        /// </summary>
        public JsonResult NotificationList()
        {
            return Json(new Result(false, 0, "yetki hatası"), JsonRequestBehavior.AllowGet);
        }
    }
}