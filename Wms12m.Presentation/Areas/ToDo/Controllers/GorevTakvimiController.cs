using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class GorevTakvimiController : RootController
    {
        /// <summary>
        /// görevtakvimi sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoTakvim, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.UserName = vUser.UserName;
            return View("Index");
        }
        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(string UserName)
        {
            if (vUser.RoleName != "Admin" && vUser.RoleName != " " && UserName != vUser.UserName)
                UserName = vUser.UserName;
            ViewBag.UserName = UserName;
            ViewBag.Yetki = vUser.RoleName;
            if (vUser.RoleName == "Admin" || vUser.RoleName == " ")
                ViewBag.UserID = new SelectList(Persons.GetList(), "Kod", "AdSoyad");
            var list = db.GorevlerCalismas.Where(m => m.Kaydeden == UserName).GroupBy(m => m.Tarih).Select(m => new frmGorevlerCalismalar { Tarih = m.Key, Sure = m.Sum(n => n.Sure) }).ToList();
            ViewBag.Tatil = db.Tatils.ToList();
            return PartialView("List", list);
        }
        /// <summary>
        /// tatil liste
        /// </summary>
        public PartialViewResult ListTatil(string UserName)
        {
            var list = db.Tatils.OrderByDescending(m => m.Tarih).ToList();
            return PartialView("ListTatil", list);
        }
        /// <summary>
        /// ayrıntılar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID, string User)
        {
            var Tarih = ID.FromOaDate();
            var list = db.GorevlerCalismas.Where(m => m.Tarih == Tarih && m.Kaydeden == User).OrderByDescending(m => m.ID).ToList();
            return PartialView("Details", list);
        }
    }
}