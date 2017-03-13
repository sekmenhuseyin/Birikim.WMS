﻿using System;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class BuyController : RootController
    {

        /// <summary>
        /// irsaliye sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            return View("Index", new frmIrsaliye());
        }
        /// <summary>
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult List()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0,0,") return null;
            //get IDs
            string[] tmp = id.ToString().Split(',');
            if (tmp.Length != 3) return null;
            string SirketKod = tmp[0];
            int DepoID = tmp[1].ToInt32();
            string HesapKodu = tmp[2];
            var list = Irsaliye.GetList(SirketKod, false, HesapKodu, DepoID);
            return PartialView("List", list);
        }
        /// <summary>
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult SiparisList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0") return null;
            string sirket = id.ToString().Left(2);
            string kod = id.ToString().Mid(2, 99);
            using (DinamikModelContext Dinamik = new DinamikModelContext(sirket))
            {
                var list = Dinamik.Context.SPIs
                    .Join(Dinamik.Context.CHKs, ti => ti.Chk, tc => tc.HesapKodu, (ti, tc) => new { ti = ti, tc = tc })
                    .Join(Dinamik.Context.STKs, ts => ts.ti.MalKodu, ti => ti.MalKodu, (ts, ti) => new { ts = ts, ti = ti })
                    .Where(m => m.ts.ti.KynkEvrakTip == 63 && m.ts.ti.SiparisDurumu == 0 && m.ts.ti.Chk == kod && m.ts.ti.BirimMiktar - m.ts.ti.TeslimMiktar - m.ts.ti.KapatilanMiktar > 0)
                    .Select(m => new frmSiparistenGelen { ID = m.ts.ti.ROW_ID, EvrakNo = m.ts.ti.EvrakNo, Tarih = m.ts.ti.Tarih, MalAdi = m.ti.MalAdi, MalKodu = m.ti.MalKodu, AçıkMiktar = m.ts.ti.BirimMiktar - m.ts.ti.TeslimMiktar - m.ts.ti.KapatilanMiktar, Birim = m.ts.ti.Birim }).ToList();
                return PartialView("SiparisList", list);
            }
        }
        /// <summary>
        /// siparişten malzeme ekler
        /// </summary>
        [HttpPost]
        public PartialViewResult FromSiparis(string s, string id, string ids)
        {
            if (s == null || id == null || ids == null) return null;
            //loop ids
            string[] tmp = ids.Split(',');
            int rowid; int irsaliyeID = id.ToInt32();
            using (DinamikModelContext Dinamik = new DinamikModelContext(s))
            {
                foreach (var item in tmp)
                {
                    if (item != "")
                    {
                        rowid = item.ToInt32();
                        var tbl = Dinamik.Context.SPIs.Where(m => m.ROW_ID == rowid && m.IslemTur == 0 && m.KynkEvrakTip == 63 && (m.BirimMiktar - m.TeslimMiktar - m.KapatilanMiktar)>0).FirstOrDefault();
                        //save details
                        IRS_Detay sti = new IRS_Detay();
                        sti.IrsaliyeID = irsaliyeID;
                        sti.Birim = tbl.Birim;
                        sti.KynkSiparisNo = tbl.EvrakNo;
                        sti.MalKodu = tbl.MalKodu;
                        sti.Miktar = tbl.BirimMiktar - tbl.TeslimMiktar - tbl.KapatilanMiktar;
                        Result _Result = Stok.Operation(sti);
                    }
                }
            }
            //get list
            var list = Stok.GetList(irsaliyeID);
            ViewBag.IrsaliyeId = irsaliyeID;
            ViewBag.Onay = Irsaliye.GetOnay(irsaliyeID);
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// yeni irsaliye fatura kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult New(frmIrsaliye tbl)
        {
            string gorevno = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
            int today = DateTime.Today.ToOADateInt();
            int time = DateTime.Now.SaatiAl();
            var cevap = db.InsertIrsaliye(tbl.SirketID, tbl.DepoID, gorevno, tbl.EvrakNo, "Irs: " + tbl.EvrakNo + ", Tedarikçi: " + tbl.Unvan, false, ComboItems.MalKabul.ToInt32(), User.Id, User.UserName, today, time, tbl.HesapKodu).FirstOrDefault();
            //get list
            var list = Stok.GetList(cevap.IrsaliyeID.Value);
            ViewBag.IrsaliyeId = cevap.IrsaliyeID;
            ViewBag.Onay = Irsaliye.GetOnay(cevap.IrsaliyeID.Value);
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// listeyi günceller
        /// </summary>
        public PartialViewResult GridPartial(int ID)
        {
            var list = Stok.GetList(ID);
            ViewBag.IrsaliyeId = ID;
            ViewBag.Onay = Irsaliye.GetOnay(ID);
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// yeni malzeme
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult InsertMalzeme(frmMalzeme tbl)
        {
            //add new
            Result _Result = Stok.Insert(tbl);
            //get list
            var list = Stok.GetList(tbl.IrsaliyeId);
            ViewBag.IrsaliyeId = tbl.IrsaliyeId;
            ViewBag.Onay = Irsaliye.GetOnay(tbl.IrsaliyeId);
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// malzeme autocomplete
        /// </summary>
        public JsonResult getMalzemebyCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(id.ToString()))
            {
                var list = Dinamik.Context.STKs.Where(m => m.MalKodu.StartsWith(term)).Select(m => new frmJson { id = m.MalKodu, value = m.MalAdi, label = m.MalAdi }).Take(20).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getMalzemebyName(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(id.ToString()))
            {
                var list = Dinamik.Context.STKs.Where(m => m.MalAdi.Contains(term)).Select(m => new frmJson { id = m.MalKodu, value = m.MalAdi, label = m.MalAdi }).Take(20).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// malzeme koduna göre birim getirir
        /// </summary>
        [HttpPost]
        public JsonResult getBirim(string kod,string s)
        {
            using (DinamikModelContext Dinamik = new DinamikModelContext(s))
            {
                var list = Dinamik.Context.STKs.Where(m => m.MalKodu == kod).Select(m => new { m.Birim1, m.Birim2, m.Birim3, m.Birim4 }).FirstOrDefault();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// anasayfadaki malzeme listesi
        /// </summary>
        public PartialViewResult GetHesapCodes()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            using (DinamikModelContext Dinamik = new DinamikModelContext(id.ToString()))
            {
                var list = Dinamik.Context.CHKs.Where(m => m.HesapKodu.StartsWith("320")).Select(m => new frmHesapUnvan { HesapKodu = m.HesapKodu, Unvan = m.Unvan1 + " " + m.Unvan2 }).ToList();
                return PartialView("_HesapGridPartial", list);
            }
        }
        /// <summary>
        /// yeni malzeme satırı formu
        /// </summary>
        public PartialViewResult NewMalzemeForm(int id)
        {
            ViewBag.IrsaliyeId = id;
            return PartialView("_GridNewPartial", new frmMalzeme());
        }
        /// <summary>
        /// irsaliye sil
        /// </summary>
        public JsonResult Delete1(int ID)
        {
            Result _Result = Irsaliye.Delete(ID);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// stok malzeme sil
        /// </summary>
        public JsonResult Delete2(int ID)
        {
            Result _Result = Stok.Delete(ID);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}