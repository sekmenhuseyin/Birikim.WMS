using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity.Models;
using Wms12m.Security;

namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class UserController : RootController
    {
        // GET: ToDo/User
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult List()
        {
            return PartialView(db.Users.ToList());
        }
        public PartialViewResult Edit(int id)
        {
            ViewBag.Role = db.Roles.ToList();
            ViewBag.ID = id;
            return PartialView(db.Users.FirstOrDefault(x => x.ID == id));
        }
        public PartialViewResult Create(int id)
        {
            ViewBag.Role = db.Roles.ToList();
            ViewBag.ID = id;
            return PartialView("Edit", new User());
        }
        public PartialViewResult Delete(int id)
        {
            ViewBag.ID = id;
            return PartialView();
        }
        [HttpPost]
        public bool Save(User u)
        {
            u.Sifre = CryptographyExtension.Sifrele(u.Sifre);
            if (u.ID != -1)
                db.Entry(u).State = EntityState.Modified;
            else
                db.Users.Add(u);
            var result = db.SaveChanges();
            if (result > 0)
                return true;
            return false;
        }
        [HttpPost]
        public bool DeleteUser(int ID)
        {
            db.Users.Remove(db.Users.FirstOrDefault(x => x.ID == ID));
            if (db.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}