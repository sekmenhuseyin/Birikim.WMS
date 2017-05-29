﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class TerminalController : RootController
    {
        /// <summary>
        /// kullanıcı sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return Redirect("/");
            return View("Index");
        }
        /// <summary>
        /// kullanıcılar
        /// </summary>
        public PartialViewResult List()
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
            var list = PersonPerms.GetList();
            return PartialView("List", list);
        }
        /// <summary>
        /// yeni form
        /// </summary>
        public PartialViewResult New()
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.UserID = new SelectList(Persons.GetListWithoutTerminal(), "ID", "AdSoyad");
            return PartialView("Editor", new UserDetail());
        }
        /// <summary>
        /// düzenler
        /// </summary>
        public PartialViewResult Edit()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
            //return
            var tbl = PersonPerms.Detail(id.ToInt32());
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd", tbl.DepoID);
            ViewBag.UserID = new SelectList(db.Users.Where(m => m.ID == tbl.UserID).ToList(), "ID", "AdSoyad");
            return PartialView("Editor", tbl);
        }
        /// <summary>
        /// düzenler
        /// </summary>
        public PartialViewResult Barcode()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Reading) == false) return null;
            //return
            var tbl = Persons.Detail(id.ToInt32());
            return PartialView("Barcode", tbl);
        }
        /// <summary>
        /// kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(UserDetail tbl)
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Writing) == false) return Redirect("/");
            Result _Result = PersonPerms.Operation(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// sil
        /// </summary>
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (CheckPerm("Terminal için Yetkilendirme", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = PersonPerms.Delete(Id);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}
namespace Wms12m
{
    public class IDAtomationBarCode
    {
        public static string BarcodeImageGenerator(string Code)
        {
            byte[] BarCode;
            string BarCodeImage;
            Bitmap objBitmap = new Bitmap(Code.Length * 28, 100);
            using (Graphics graphic = Graphics.FromImage(objBitmap))
            {
                Font newFont = new Font("IDAutomationHC39M Free Version", 18, FontStyle.Regular);
                PointF point = new PointF(2, 2);
                SolidBrush balck = new SolidBrush(Color.Black);
                SolidBrush white = new SolidBrush(Color.White);
                graphic.FillRectangle(white, 0, 0, objBitmap.Width, objBitmap.Height);
                graphic.DrawString("*" + Code + "*", newFont, balck, point);
            }
            using (MemoryStream Mmst = new MemoryStream())
            {
                objBitmap.Save(Mmst, ImageFormat.Png);
                BarCode = Mmst.GetBuffer();
                BarCodeImage = BarCode != null ? "data:image/jpg;base64," + Convert.ToBase64String((byte[])BarCode) : "";
                return BarCodeImage;
            }
        }
    }
}
