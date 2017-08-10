using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class GorevTakvimiController : RootController
    {
        // GET: ToDo/GorevTakvimi
        public ActionResult GorevTakvimi()
        {
            List<GorevCalisma> aa = db.Database.SqlQuery<GorevCalisma>(string.Format("SELECT * FROM BIRIKIM.ong.GorevCalisma", "17")).ToList();
            return View(aa);
        }

        public string UserSelect()
        {
            var FHKS = db.Database.SqlQuery<User>(string.Format("SELECT * FROM BIRIKIM.usr.Users where ID IN (4,5)", "17")).ToList();
            var json = new JavaScriptSerializer().Serialize(FHKS);
            return json;
        }
    }
}