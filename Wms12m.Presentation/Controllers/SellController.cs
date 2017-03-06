using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SellController : RootController
    {
        /// <summary>
        /// sipariş planlama sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return View("Index");
        }
        /// <summary>
        /// get depo names based on şirket
        /// </summary>
        [HttpPost]
        public JsonResult GetDepo()
        {
            var ID = Url.RequestContext.RouteData.Values["id"];
            if (ID == null || ID.ToString2() == "0") return null;
            //connect
            using (DinamikModelContext Dinamik = new DinamikModelContext(ID.ToString()))
            {
                var list = Dinamik.Context.DEPs.Select(m => new { m.Depo, m.DepoAdi }).ToList();
                return Json(list.Select(x => new { Value = x.Depo, Text = x.DepoAdi, Selected = 0 }), JsonRequestBehavior.AllowGet);
            }
        }
    }
}