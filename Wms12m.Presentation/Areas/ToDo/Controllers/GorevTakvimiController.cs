using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

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
            var list = db.GorevlerCalismas.Select(m => new frmGorevlerCalismalar { ID = m.ID, GorevID = m.GorevID, Gorev = m.Gorevler.Gorev, Calisma = m.Calisma, Sure = m.Sure, Tarih = m.Tarih, Kaydeden = m.Kaydeden }).ToList();
            return View(list);
        }
        /// <summary>
        /// kullanıcı seç
        /// </summary>
        public string UserSelect()
        {
            var list = db.Database.SqlQuery<User>(string.Format("SELECT * FROM BIRIKIM.usr.Users where ID IN (4,5)", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(list);
            return json;
        }
    }
}