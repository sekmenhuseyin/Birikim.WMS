using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            var list = db.Database.SqlQuery<GorevCalisma>(string.Format("SELECT * FROM BIRIKIM.ong.GorevCalisma", "17")).ToList();
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