using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wms12m.Presentation.Areas.Reports.Controllers
{
    public class OrdersController : RootController
    {
        // GET: Reports/Orders
        public ActionResult Index()
        {
            return View();
        }
    }
}