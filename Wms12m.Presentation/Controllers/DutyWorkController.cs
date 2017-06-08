using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class DutyWorkController : Controller
    {
        private WMSEntities db = new WMSEntities();

        // GET: DutyWork
        public ActionResult Index()
        {
            var gorevCalismas = db.GorevCalismas.Include(g => g.Gorevler);
            return View(gorevCalismas.ToList());
        }

        // GET: DutyWork/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GorevCalisma gorevCalisma = db.GorevCalismas.Find(id);
            if (gorevCalisma == null)
            {
                return HttpNotFound();
            }
            return View(gorevCalisma);
        }

        // GET: DutyWork/Create
        public ActionResult Create()
        {
            ViewBag.GorevID = new SelectList(db.Gorevlers, "ID", "Sorumlu");
            return View();
        }

        // POST: DutyWork/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GorevID,Tarih,CalismaSure,Calisma,Kaydeden,KayitTarih,Degistiren,DegisTarih")] GorevCalisma gorevCalisma)
        {
            if (ModelState.IsValid)
            {
                db.GorevCalismas.Add(gorevCalisma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GorevID = new SelectList(db.Gorevlers, "ID", "Sorumlu", gorevCalisma.GorevID);
            return View(gorevCalisma);
        }

        // GET: DutyWork/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GorevCalisma gorevCalisma = db.GorevCalismas.Find(id);
            if (gorevCalisma == null)
            {
                return HttpNotFound();
            }
            ViewBag.GorevID = new SelectList(db.Gorevlers, "ID", "Sorumlu", gorevCalisma.GorevID);
            return View(gorevCalisma);
        }

        // POST: DutyWork/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GorevID,Tarih,CalismaSure,Calisma,Kaydeden,KayitTarih,Degistiren,DegisTarih")] GorevCalisma gorevCalisma)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(gorevCalisma).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.GorevID = new SelectList(db.Gorevlers, "ID", "Sorumlu", gorevCalisma.GorevID);
            //return View(gorevCalisma);
            return View();
        }

        // GET: DutyWork/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GorevCalisma gorevCalisma = db.GorevCalismas.Find(id);
            if (gorevCalisma == null)
            {
                return HttpNotFound();
            }
            return View(gorevCalisma);
        }

        // POST: DutyWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GorevCalisma gorevCalisma = db.GorevCalismas.Find(id);
            db.GorevCalismas.Remove(gorevCalisma);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
