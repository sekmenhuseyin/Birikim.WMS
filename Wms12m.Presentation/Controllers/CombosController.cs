using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class CombosController : RootController
    {
        /// <summary>
        /// tüm liste
        /// </summary>
        public ActionResult Index()
        {
            return View("Index", db.Combo_Name.OrderBy(m=>m.ComboName).ToList());
        }
        /// <summary>
        /// yeni sayfası
        /// </summary>
        public PartialViewResult New()
        {
            return PartialView("New", new Combo_Name());
        }
        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult Edit(int id)
        {
            Combo_Name combo_Name = db.Combo_Name.Find(id);
            return PartialView("Edit", combo_Name);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save([Bind(Include = "ID,ComboName")] Combo_Name combo_Name)
        {
            Result _Result = new Result();
            if (ModelState.IsValid)
            {
                db.Combo_Name.Add(combo_Name);
                db.SaveChanges();
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Result _Result = new Result();
            Combo_Name combo_Name = db.Combo_Name.Find(id);
            if (combo_Name != null)
            {
                db.Combo_Name.Remove(combo_Name);
                db.SaveChanges();
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
