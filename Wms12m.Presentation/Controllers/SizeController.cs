using System;
using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class SizeController : Controller
    {
        /// <summary>
        /// boyutlar anasayfası
        /// </summary>
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}