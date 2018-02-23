using Birikim.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class HelpController : RootController
    {
        /// <summary>
        /// yardım sayfası
        /// </summary>
        public ActionResult Index() => View("Index");

        /// <summary>
        /// yardım sayfası listesi
        /// </summary>
        public PartialViewResult List(string search)
        {
            ViewBag.Topics = db.FAQs.Where(m => m.Active).GroupBy(m => new frmGorevli { Gorevli = m.ComboItem_Name.Name, ID = m.TopicTypeID }).Select(m => m.Key).ToList();
            var liste = db.FAQs.Where(m => m.Active);
            if (!string.IsNullOrEmpty(search))
                liste = liste.Where(m => m.Title.Contains(search) || m.Detail.Contains(search));
            return PartialView("List", liste.OrderBy(m => m.TopicTypeID).ToList());
        }

        /// <summary>
        /// yeni sayfası
        /// </summary>
        public PartialViewResult New()
        {
            ViewBag.TopicTypeID = new SelectList(ComboSub.GetList(Combos.FaqTopics.ToInt32()), "ID", "Name");
            return PartialView(new FAQ());
        }

        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Save(FAQ satir)
        {
            if (ModelState.IsValid)
            {
                if (satir.ID == 0)
                {
                    db.FAQs.Add(satir);
                }
                else
                {
                    var tbl = db.FAQs.FirstOrDefault(m => m.ID == satir.ID);
                    if (tbl != null)
                    {
                        tbl.TopicTypeID = satir.TopicTypeID;
                        tbl.Title = satir.Title;
                        tbl.Detail = satir.Detail;
                    }
                }

                try
                {
                    db.SaveChanges();
                    return Json(new Result(true, satir.ID), JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return Json(new Result(false, "Hata oldu"), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// sil
        /// </summary>
        public JsonResult Delete(string Id)
        {
            var tbl = db.FAQs.Find(Id.ToInt32());
            if (tbl != null) db.FAQs.Remove(tbl);
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

        /// <summary>
        /// düzenleme listesi
        /// </summary>
        public PartialViewResult FormList() => PartialView("FormList", db.FAQs.ToList());

        /// <summary>
        /// düzenleme
        /// </summary>
        public PartialViewResult FormEdit(int? id)
        {
            var tbl = db.FAQs.Find(id);
            ViewBag.TopicTypeID = new SelectList(ComboSub.GetList(Combos.FaqTopics.ToInt32()), "ID", "Name", tbl?.TopicTypeID ?? 0);
            return PartialView("FormEdit", tbl);
        }
    }
}