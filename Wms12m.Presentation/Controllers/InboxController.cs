using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class InboxController : RootController
    {
        // GET: Inbox
        public ActionResult Index()
        {
            return View();
        }
    }
}