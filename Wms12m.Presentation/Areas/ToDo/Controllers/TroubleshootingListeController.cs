using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;
namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class TroubleshootingListeController : RootController
    {
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoTroubleshootingListe, PermTypes.Reading) == false) return Redirect("/");
            return View("Index", new Troubleshooting());
        }

        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(string search)
        {
            object list = new object();

            if (search.IsNotNull())
                list = db.Troubleshootings.Where(x => x.Konu.Contains(search) || x.Aciklama.Contains(search)).ToList();
            else
                list = db.Troubleshootings.ToList();

            return PartialView("List", list);          
        }

        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TodoTroubleshootingListe, PermTypes.Writing) == false) return null;
           
            return PartialView(new Troubleshooting());
        }
    }
}