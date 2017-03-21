using System;
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
            var list = db.Database.SqlQuery<frmSiparistenGelen>("SELECT FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar AS AçıkMiktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim " +
                                                                "FROM FINSAT6{0}.FINSAT6{0}.SPI INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                                                "WHERE(FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND(FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND(FINSAT6{0}.FINSAT6{0}.SPI.Chk = '{1}') AND(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar > 0)", sirket, kod);

            return PartialView("SiparisList", list);
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
            foreach (var item in tmp)
            {
                if (item != "")
                {
                    rowid = item.ToInt32();
                    var tbl = db.Database.SqlQuery<frmIrsaliyeMalzeme>("SELECT EvrakNo, MalKodu, BirimMiktar - TeslimMiktar - KapatilanMiktar AS Miktar, Birim FROM FINSAT6{0}.FINSAT6{0}.SPI WHERE (ROW_ID = {1}) AND (IslemTur = 0) AND (KynkEvrakTip = 63) AND (BirimMiktar - TeslimMiktar - KapatilanMiktar > 0)", s, rowid).FirstOrDefault();
                    //save details
                    IRS_Detay sti = new IRS_Detay();
                    sti.IrsaliyeID = irsaliyeID;
                    sti.Birim = tbl.Birim;
                    sti.KynkSiparisNo = tbl.EvrakNo;
                    sti.MalKodu = tbl.MalKodu;
                    sti.Miktar = tbl.Miktar;
                    Result _Result = Stok.Operation(sti);
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
            var cevap = db.InsertIrsaliye(tbl.SirketID, tbl.DepoID, gorevno, tbl.EvrakNo, "Irs: " + tbl.EvrakNo + ", Tedarikçi: " + tbl.Unvan, false, ComboItems.MalKabul.ToInt32(), vUser.Id, vUser.UserName, today, time, tbl.HesapKodu).FirstOrDefault();
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
            var list = db.Database.SqlQuery<frmJson>("SELECT TOP (20) MalKodu AS id, MalAdi AS value, MalAdi AS label FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalKodu LIKE '{1}%')", id.ToString(), term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getMalzemebyName(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            var list = db.Database.SqlQuery<frmJson>("SELECT TOP (20) MalKodu AS id, MalAdi AS value, MalAdi AS label FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalAdi LIKE '%{1}%')", id.ToString(), term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// malzeme koduna göre birim getirir
        /// </summary>
        [HttpPost]
        public JsonResult getBirim(string kod, string s)
        {
            var list = db.Database.SqlQuery<frmBirims>("SELECT Birim1, Birim2, Birim3, Birim4 FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalKodu = '{1}')", s, kod);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// anasayfadaki malzeme listesi
        /// </summary>
        public PartialViewResult GetHesapCodes()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            if (id.ToString() == "0") return null;
            var list = db.Database.SqlQuery<frmHesapUnvan>("SELECT HesapKodu, Unvan1 + ' ' + Unvan2 AS Unvan FROM FINSAT6{0}.FINSAT6{0}.CHK WHERE (KartTip = 0) OR (KartTip = 1) OR (KartTip = 4)", id.ToString());
            return PartialView("_HesapGridPartial", list);
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