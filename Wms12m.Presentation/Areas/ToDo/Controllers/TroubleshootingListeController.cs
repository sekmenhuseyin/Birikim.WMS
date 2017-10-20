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
        public PartialViewResult List()
        {
            return PartialView("List", db.Troubleshootings.ToList());
        }

        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TodoTroubleshootingListe, PermTypes.Writing) == false) return null;
           
            return PartialView(new Troubleshooting());
        }
    }
}