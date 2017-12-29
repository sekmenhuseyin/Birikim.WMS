using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class MyAccountController : RootController
    {
        /// <summary>
        /// hesabım sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.sirket = db.GetSirkets().FirstOrDefault();
            return View("Index", Persons.Detail(vUser.Id));
        }

        /// <summary>
        /// resim sil
        /// </summary>
        public JsonResult RemoveImage(string ID)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            // if exists delete
            if (System.IO.File.Exists(Server.MapPath("/Content/Uploads/" + ID + ".jpg")) == true) System.IO.File.Delete(Server.MapPath("/Content/Uploads/" + ID + ".jpg"));
            // return
            return Json(new Result(true, 1), JsonRequestBehavior.AllowGet);
        }
    }
}