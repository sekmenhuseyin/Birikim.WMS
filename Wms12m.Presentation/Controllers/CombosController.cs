using System;
using System.Linq;
using System.Web.Mvc;

namespace Wms12m.Presentation.Controllers
{
    public class CombosController : Controller
    {
        // GET: Combos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Combos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Combos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Combos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Combos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Combos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Combos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
