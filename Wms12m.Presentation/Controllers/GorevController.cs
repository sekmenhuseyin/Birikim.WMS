using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class GorevController : RootController
    {
        // GET: Gorev
        public ActionResult Index()
        {
            return View("Index", new frmGorev());
        }
        public PartialViewResult GorevGridPartial()
        {
            var list = db.GorevListesis.ToList();
            return PartialView("_GorevGridPartial", list);
        }
    }
}