using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
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
            ViewBag.Sirket = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName");
            return PartialView("New", new UserDetail());
        }
        /// <summary>
        /// depo yetkilerini ayarlar
        /// </summary>
        public PartialViewResult Depo()
        {
            //kontrol
            if (CheckPerm("Users", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            //return
            var tbl = db.YetkiDepo(id.ToInt32()).ToList();
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.ID = id;
            return PartialView("Depo", tbl);
        }
        /// <summary>
        /// depo yetkisini kaydet
        /// </summary>
        [HttpPost]
        public PartialViewResult DepoSet(int UserID, int DepoID)
        {
            if (CheckPerm("Users", PermTypes.Writing) == false) return null;
            try
            {
                db.YetkiDepoSet(DepoID, UserID, true);
            }
            catch (System.Exception) { }
            var tbl = db.YetkiDepo(UserID).ToList();
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.ID = UserID;
            return PartialView("Depo", tbl);
        }
        /// <summary>
        /// depo yetkisi sil
        /// </summary>
        public JsonResult DepoDelete(string Id)
        {
            if (CheckPerm("Users", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            string[] ids = Id.ToString().Split('-');
            db.YetkiDepoSet(ids[1].ToInt32(), ids[0].ToInt32(), false);
            Result _Result = new Result()
            {
                Id = ids[1].ToInt32(),
                Message = "İşlem Başarılı !!!",
                Status = true
            };
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
    }
}