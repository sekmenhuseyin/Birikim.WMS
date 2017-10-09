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
            var list = db.GorevlerCalismas.Where(m => m.Kaydeden == vUser.UserName).GroupBy(m => m.Tarih).Select(m => new frmGorevlerCalismalar { Tarih = m.Key, Sure = m.Sum(n => n.Sure) }).ToList();
            return View(list);
        }
    }
}