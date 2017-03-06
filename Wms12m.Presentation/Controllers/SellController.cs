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
        public JsonResult GetDepo(string ID)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(ID))
            {
                var list = Dinamik.Context.DEPs.Select(m => new { value = m.Depo, text = m.DepoAdi }).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
    }
}