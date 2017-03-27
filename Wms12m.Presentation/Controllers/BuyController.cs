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
            string[] tmp = id.ToString().Split(',');
            if (tmp.Length != 3) return null;
            //get liste
            var list = Irsaliye.GetList(tmp[0], false, tmp[2], tmp[1].ToInt32());
            return PartialView("List", list);
        }
        /// <summary>
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult SiparisList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0,0,") return null;
            string[] tmp = id.ToString().Split(',');
            if (tmp.Length != 3) return null;
            string depo = Store.Detail(tmp[1].ToInt32()).DepoKodu;
            //get list
            string sql = String.Format("SELECT FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar AS AçıkMiktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 63) AND (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Chk = '{2}') AND (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar > 0)", tmp[0], depo, tmp[2]);
            var list = db.Database.SqlQuery<frmSiparistenGelen>(sql).ToList();

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
                    string sql = String.Format("SELECT EvrakNo, MalKodu, BirimMiktar - TeslimMiktar - KapatilanMiktar AS Miktar, Birim FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) WHERE (ROW_ID = {1}) AND (IslemTur = 0) AND (KynkEvrakTip = 63) AND (BirimMiktar - TeslimMiktar - KapatilanMiktar > 0)", s, rowid);
                    var tbl = db.Database.SqlQuery<frmIrsaliyeMalzeme>(sql).FirstOrDefault();
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
            var irs = Irsaliye.Detail(irsaliyeID);
            ViewBag.IrsaliyeId = irsaliyeID;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = irs.SirketKod;
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
            ViewBag.SirketID = tbl.SirketID;
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// listeyi günceller
        /// </summary>
        public PartialViewResult GridPartial(int ID)
        {
            var list = Stok.GetList(ID);
            var irs = Irsaliye.Detail(ID);
            ViewBag.IrsaliyeId = ID;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = irs.SirketKod;
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
            var irs = Irsaliye.Detail(tbl.IrsaliyeId);
            ViewBag.IrsaliyeId = tbl.IrsaliyeId;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = irs.SirketKod;
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// malzeme autocomplete
        /// </summary>
        public JsonResult getMalzemebyCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = String.Format("SELECT TOP (20) MalKodu AS id, MalAdi AS value, MalAdi AS label FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu LIKE '{1}%')", id.ToString(), term);
            var list = db.Database.SqlQuery<frmJson>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getMalzemebyName(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = String.Format("SELECT TOP (20) MalKodu AS id, MalAdi AS value, MalAdi AS label FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalAdi LIKE '%{1}%')", id.ToString(), term);
            var list = db.Database.SqlQuery<frmJson>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// malzeme koduna göre birim getirir
        /// </summary>
        [HttpPost]
        public JsonResult getBirim(string kod, string s)
        {
            string sql = String.Format("SELECT Birim1, Birim2, Birim3, Birim4 FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = '{1}')", s, kod);
            var list = db.Database.SqlQuery<frmBirims>(sql).ToList();
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
            string sql = String.Format("SELECT HesapKodu, Unvan1 + ' ' + Unvan2 AS Unvan FROM FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) WHERE (KartTip = 0) OR (KartTip = 1) OR (KartTip = 4)", id.ToString());
            var list = db.Database.SqlQuery<frmHesapUnvan>(sql).ToList();
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