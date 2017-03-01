using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SizeController : RootController
    {
        /// <summary>
        /// boyutlar anasayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return View("Index");
        }
        /// <summary>
        /// şirket ID'ye göre stok listesini getirir
        /// </summary>
        public PartialViewResult GetList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            var list = db.Olcus.Where(m => m.SirketKodu == id.ToString()).OrderBy(m => m.MalKodu).ToList();
            return PartialView("_GetList", list);
        }
    }
}