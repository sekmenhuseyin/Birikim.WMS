using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class TasksController : RootController
    {

        /// <summary>
        /// görev anasayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Index");
        }
        /// <summary>
        /// listeyi gösterir
        /// </summary>
        public PartialViewResult List(int Id)
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var list = Task.GetList(Id, vUser.DepoId);
            return PartialView("List", list);
        }
        /// <summary>
        /// görev ayrıntıları
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var list = db.GetIrsDetayfromGorev(ID);
            return PartialView("Details", list);
        }
        /// <summary>
        /// görev düzenle
        /// </summary>
        public PartialViewResult GorevDetailPartial(int id)
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var list = Task.Detail(id);
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GorevTipleri.ToInt32()), "ID", "Name", list.GorevTipiID);
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name", list.DurumID);
            return PartialView("_GorevDetailPartial", list);
        }
        /// <summary>
        /// görev güncelle
        /// </summary>
        public JsonResult Update(frmGorev tbl)
        {
            if (CheckPerm("Tasks", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            //update
            Task tmpTable = new Task();
            Result _Result = tmpTable.Update(tbl);
            //get list
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görev sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            if (CheckPerm("Tasks", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result();
            try
            {
                db.DeleteFromGorev(ID);
                _Result.Status = true; _Result.Id = ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Tasks/Delete");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görevli ata
        /// </summary>
        [HttpPost]
        public PartialViewResult GorevliAta()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            Int32 ID = Convert.ToInt32(id);
            var list = Task.Detail(ID);
            ViewBag.Gorevli = new SelectList(Persons.GetList(), "Kod", "AdSoyad", list.Gorevli);
            return PartialView("GorevliAta", list);
        }
        /// <summary>
        /// görevliyi kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GorevliKaydet(frmGorevli tbl)
        {
            if (CheckPerm("Tasks", PermTypes.Writing) == false) return Redirect("/");
            Task tmpTable = new Task();
            Result _Result = tmpTable.UpdateGorevli(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// kontrollü sayin sayfası
        /// </summary>
        public ActionResult Count()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Count");
        }
        /// <summary>
        /// listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult CountList(string Id)
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            //id'ye göre liste döner
            var list = Task.GetList(ComboItems.KontrolSayım.ToInt32(), Id.ToInt32());
            return PartialView("CountList", list);
        }
        /// <summary>
        /// yeni kontrollü sayım görevi ekle
        /// </summary>
        [HttpPost]
        public PartialViewResult New()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return null;
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return PartialView("CountNew");
        }
        /// <summary>
        /// yeni görevi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult SaveNew(int DepoID)
        {
            if (CheckPerm("Tasks", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result;
            //kontrol
            int açık = ComboItems.Açık.ToInt32();
            int sayim = ComboItems.KontrolSayım.ToInt32();
            var grv = db.Gorevs.Where(m => m.DepoID == DepoID && m.GorevTipiID == sayim && m.DurumID == açık).FirstOrDefault();
            if (grv == null)
            {
                int tarih = fn.ToOADate();
                var grvNo = db.SettingsGorevNo(tarih).FirstOrDefault();
                var irsNo = db.SettingsIrsaliyeNo(tarih).FirstOrDefault();
                var sirketkodu = db.GetSirketDBs().FirstOrDefault();
                var depo = Store.Detail(DepoID).DepoKodu;
                var cevap = db.InsertIrsaliye(sirketkodu, DepoID, grvNo, irsNo, tarih, depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "").FirstOrDefault();
                grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                grv.DurumID = açık;
                db.SaveChanges();
                _Result = new Result(true);
            }
            else
                _Result = new Result(false, "Bu görev zaten var");
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kontrollü sayima ait fark sayfası
        /// </summary>
        public ActionResult CountFark()
        {
            if (CheckPerm("Tasks", PermTypes.Reading) == false) return Redirect("/");
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = string.Format("SELECT MalKodu, Birim, Miktar, Stok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                            "(SELECT SUM(Miktar) AS Expr1 FROM wms.Yer WITH(NOLOCK) WHERE (DepoID = wms.Gorev.DepoID) AND (MalKodu = wms.GorevYer.MalKodu) AND (Birim = wms.GorevYer.Birim)) AS Stok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {0}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t WHERE (Stok <> Miktar)", id);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            return View("CountFark", list);
        }
    }
}