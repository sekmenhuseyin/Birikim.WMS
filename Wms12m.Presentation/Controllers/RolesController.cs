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
            if (CheckPerm("Gruplar", PermTypes.Reading) == false) return Redirect("/");
            var list = db.Roles.Where(m => m.RoleName != "").ToList();
            return View("Index", list);
        }

        // GET: Roles/Create
        public PartialViewResult New()
        {
            if (CheckPerm("Gruplar", PermTypes.Reading) == false) return null;
            return PartialView("Create", new Role());
        }

        // POST: Roles/Create
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RoleName")] Role role)
        {
            if (CheckPerm("Gruplar", PermTypes.Writing) == false) return Redirect("/");
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (CheckPerm("Gruplar", PermTypes.Deleting) == false) return Redirect("/");
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