using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class UsersController : RootController
    {
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public ActionResult Index()
        {
            var list = db.Users.ToList();
            return View("Index", list);
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        public PartialViewResult Details(int id)
        {
            var tbl = Persons.Detail(id);
            return PartialView("Details", tbl);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            ViewBag.Sirket = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName");
            return PartialView("New", new User());
        }
        /// <summary>
        /// düzenle form
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            var tbl = Persons.Detail(id);
            ViewBag.Sirket = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad", tbl.Sirket);
            ViewBag.RoleName = new SelectList(Roles.GetList(), "RoleName", "RoleName", tbl.RoleName);
            return PartialView("Edit", tbl);
        }
        /// <summary>
        /// depo yetkilerini ayarlar
        /// </summary>
        public PartialViewResult Depo()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            var tbl = db.YetkiDepo(id.ToInt32()).ToList();
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.ID = id;
            return PartialView("Depo", tbl);
        }
        /// <summary>
        /// şifre değiştirme ekranı
        /// </summary>
        public PartialViewResult Pass()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            ViewBag.ID = id;
            return PartialView("Pass", new frmUserChangePass());
        }
        /// <summary>
        /// depo yetkisini kaydet
        /// </summary>
        [HttpPost]
        public PartialViewResult DepoSet(int UserID, int DepoID)
        {
            db.YetkiDepoSet(DepoID, UserID, true);
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
            string[] ids = Id.ToString().Split('-');
            db.YetkiDepoSet(ids[1].ToInt32(), ids[0].ToInt32(), false);
            Result _Result = new Result();
            _Result.Id = ids[1].ToInt32();
            _Result.Message = "İşlem Başarılı !!!";
            _Result.Status = true;
            return Json(_Result, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(User tbl)
        {
            Result _Result = Persons.Operation(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// şifreyi değiştir
        /// </summary>
        [HttpPost]
        public JsonResult ChangePass(frmUserChangePass tmp)
        {
            Result _Result = new Result();
            if (tmp.Password==tmp.Password2)
            {
                User tbl = new User();
                tbl.ID = tmp.ID;
                tbl.Sifre = tmp.Password;
                _Result = Persons.ChangePass(tbl);
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            Result _Result = Persons.Delete(Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
