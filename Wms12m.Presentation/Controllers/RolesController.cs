using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class RolesController : RootController
    {
        // GET: Roles
        public ActionResult Index()
        {
            var list = db.Roles.Where(m => m.RoleName != "").ToList();
            return View("Index", list);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View("Editor");
        }

        // POST: Roles/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
                return RedirectToAction("Index");
            return View("Editor", role);
        }

        // POST: Roles/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RoleName")] Role role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (System.Exception)
                {
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                Role role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
            }
            catch (System.Exception)
            {
            }
            return RedirectToAction("Index");
        }
    }
}