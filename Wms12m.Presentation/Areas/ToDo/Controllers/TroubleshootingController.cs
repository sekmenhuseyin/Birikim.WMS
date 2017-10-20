using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;
namespace Wms12m.Presentation.Areas.ToDo.Controllers
{
    public class TroubleshootingController : RootController
    {
        public ActionResult Index()
        {
            if (CheckPerm(Perms.TodoTroubleshooting, PermTypes.Reading) == false) return Redirect("/");
            return View("Index", new Troubleshooting());
        }

        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult List(string search)
        {
            object list = new object();

            if (search.IsNotNull())
                list = db.Troubleshootings.Where(x => x.Konu.Contains(search) || x.Aciklama.Contains(search)).ToList();
            else
                list = db.Troubleshootings.ToList();

            return PartialView("List", list);          
        }

        public PartialViewResult New()
        {
            if (CheckPerm(Perms.TodoTroubleshooting, PermTypes.Writing) == false) return null;
           
            return PartialView(new Troubleshooting());
        }




        /// <summary>
        /// liste
        /// </summary>
        public PartialViewResult FormList()
        {
            return PartialView("FormList", db.Troubleshootings.ToList());
        }

        /// <summary>
        /// düzenle
        /// </summary>
        public PartialViewResult FormEdit(int? id)
        {
            Troubleshooting troubleshooting = db.Troubleshootings.Find(id);

            return PartialView(troubleshooting);
        }

        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(Troubleshooting troubleshooting)
        {
            if (ModelState.IsValid)
            {
                if (troubleshooting.ID == 0)
                {
                    troubleshooting.Degistiren = vUser.UserName;
                    troubleshooting.Kaydeden = vUser.UserName;
                    troubleshooting.DegisTarih = DateTime.Now;
                    troubleshooting.KayitTarih = troubleshooting.DegisTarih;
                    db.Troubleshootings.Add(troubleshooting);
                }
                else
                {
                    var tbl = db.Troubleshootings.Where(m => m.ID == troubleshooting.ID).FirstOrDefault();
                    tbl.Konu = troubleshooting.Konu;
                    tbl.Aciklama = troubleshooting.Aciklama;
                    tbl.Degistiren = vUser.UserName;
                    tbl.DegisTarih = DateTime.Now;
                }
                try
                {
                    db.SaveChanges();
                    LogActions("ToDo", "Troubleshooting", "Save", ComboItems.alEkle, troubleshooting.ID, "Konu: " + troubleshooting.Konu);
                    return Json(new Result(true, troubleshooting.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                }
            }
            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            Troubleshooting troubleshooting = db.Troubleshootings.Find(Id.ToInt32());
            db.Troubleshootings.Remove(troubleshooting);
            try
            {
                db.SaveChanges();
                return Json(new Result(true, Id.ToInt32()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Silme işlemi gerçekleştirilemedi."), JsonRequestBehavior.AllowGet);
            }
        }

    }
}