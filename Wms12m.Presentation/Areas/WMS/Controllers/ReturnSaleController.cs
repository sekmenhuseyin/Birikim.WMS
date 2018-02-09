using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class ReturnSaleController : RootController
    {
        /// <summary>
        /// ilk sayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.SatistanIade, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(), "DepoKodu", "DepoAd");
            return View("Index");
        }

        /// <summary>
        /// seçili malzemeler gruplanmış olarak gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmSiparisIadeSteps tbl)
        {
            //kontrol
            if (tbl.DepoID == "" || tbl.Tarih == "" || tbl.EvrakNo == "" || tbl.HesapKodu == "" || tbl.EvrakNos.Count() == 0)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.SatistanIade, PermTypes.Reading) == false) return Redirect("/");
            // listeyi getir
            var sql = string.Format("FINSAT6{0}.wms.SatisIptalSecimList @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK = '{3}'", vUser.SirketKodu, tbl.DepoID, string.Join(",", tbl.EvrakNos), tbl.HesapKodu);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            // çapraz stok kontrol
            string hataliStok = "", sifirStok = ""; var newList = new List<frmSiparisMalzemeDetay>();
            foreach (var item in list)
            {
                if (item.WmsStok == 0)
                {
                    if (sifirStok != "") sifirStok += ", ";
                    sifirStok += item.MalKodu;
                    newList.Add(item);
                }
                else if (item.GunesStok != item.WmsStok)
                {
                    if (hataliStok != "") hataliStok += ", ";
                    hataliStok += item.MalKodu;
                }
            }

            if (newList.Count > 0)
                foreach (var item in newList)
                    list.Remove(item);
            if (sifirStok != "")
                sifirStok += " için stok bulunamadı.<br />";
            if (hataliStok != "")
                hataliStok += " için stok miktarları uyuşmuyor.<br />";
            // return
            ViewBag.Hatali = sifirStok + hataliStok + "<br /><br />";
            ViewBag.hataliStok = hataliStok == "" && list.Count > 0 ? true : false;
            ViewBag.tbl = tbl;
            return View("Step2", list);
        }

        /// <summary>
        /// step 3
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisIadeSteps tbl)
        {
            if (CheckPerm(Perms.SatistanIade, PermTypes.Writing) == false) return Redirect("/");
            var kontrol1 = DateTime.TryParse(tbl.Tarih, out DateTime tmpTarih);
            if (kontrol1 == false)
            {
                db.Logger(vUser.UserName, "", fn.GetIPAddress(), "Tarih hatası: " + tbl.Tarih, "", "WMS/ReturnSale/Step3");
                return Redirect("/");
            }
            // variables and consts
            var tarih = tmpTarih.ToOADateInt();
            int today = fn.ToOADate(), time = fn.ToOATime();
            var idDepo = Store.Detail(tbl.DepoID).ID;
            // yeni görev
            var GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            var cevap = db.InsertIadeIrsaliye(vUser.SirketKodu, idDepo, GorevNo, vUser.SirketKodu, tarih, "Irs: " + tbl.EvrakNo + ", Tedarikçi: " + tbl.HesapKodu, false, ComboItems.Satıştanİade.ToInt32(), vUser.UserName, today, time, tbl.HesapKodu, "", 0, "").FirstOrDefault();
            for (int i = 0; i < tbl.MalKodus.Length; i++)
            {
                IrsaliyeDetay.Operation(new IRS_Detay()
                {
                    IrsaliyeID = cevap.IrsaliyeID.Value,
                    MalKodu = tbl.MalKodus[i],
                    Birim = tbl.Birims[i],
                    Miktar = tbl.Miktars[i],
                    KynkSiparisNo = tbl.EvrakNos[0],
                    KynkSiparisID = tbl.IDs[i]
                });
            }
            // listeyi getir
            var sql = string.Format("EXEC FINSAT6{0}.wms.getIrsaliyeDetay {1}", vUser.SirketKodu, cevap.IrsaliyeID.Value);
            var list2 = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            ViewBag.GorevID = cevap.GorevID.Value;
            ViewBag.DepoID = idDepo;
            return View("Step3", list2);
        }

        /// <summary>
        /// alımdan iade onaylandı
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(int GorevID)
        {
            if (CheckPerm(Perms.SatistanIade, PermTypes.Writing) == false) return Redirect("/");

            var ir = db.IRS.Where(a => a.ID == a.Gorevs.Where(n => n.ID == GorevID).Select(n => n.IrsaliyeID).FirstOrDefault()).FirstOrDefault();
            var grv = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            // görevi aç
            grv.DurumID = ComboItems.Açık.ToInt32();
            grv.OlusturmaTarihi = fn.ToOADate();
            grv.OlusturmaSaati = fn.ToOATime();
            ir.Onay = true;
            db.SaveChanges();
            LogActions("WMS", "ReturnSale", "Approve", ComboItems.alEkle, GorevID, "Firma: " + grv.IR.HesapKodu);
            // görevlere git
            return Redirect("/WMS/Tasks");
        }

        ///<summary>
        ///depo ve şirket seçince açık siparişler gelecek
        ///</summary>
        [HttpPost]
        public PartialViewResult List(string DepoID, string CHK, string Starts, string Ends, string EvrakNo, string Tarih)
        {
            // ilk kontrol
            if (CHK == "" || EvrakNo == "" || Tarih == "") return null;
            var tarihler = DateTime.TryParse(Starts, out DateTime StartDate); if (tarihler == false) return null;
            tarihler = DateTime.TryParse(Ends, out DateTime EndDate); if (tarihler == false) return null;
            if (StartDate > EndDate) return null;
            // perm kontrol
            var sql = string.Format("FINSAT6{0}.wms.SatisIptalSiparisList @DepoKodu = '{1}', @CHK = '{2}', @BasTarih = {3}, @BitTarih = {4}", vUser.SirketKodu, DepoID.ToString(), CHK, StartDate.ToOADateInt(), EndDate.ToOADateInt());
            // return list
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("List", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/ReturnSale/List");
                return PartialView("List", new List<frmSiparisler>());
            }
        }

        /// <summary>
        /// detaylar
        /// </summary>
        [HttpPost]
        public JsonResult Details(string EvrakNo, string CHK, string Depo)
        {
            var sql = string.Format("FINSAT6{0}.wms.SatisIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK = '{3}'", vUser.SirketKodu, Depo, EvrakNo, CHK);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// stok kontrol
        /// </summary>
        [HttpPost]
        public string StokKontrol(string EvrakNo, string CHK, string Depo)
        {
            var sql = string.Format("FINSAT6{0}.wms.SatisIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK='{3}'", vUser.SirketKodu, Depo, EvrakNo, CHK);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            var hataliStok = "##";
            foreach (var item in list)
            {
                if (item.Stok != item.WmsStok)
                {
                    if (hataliStok != "##") hataliStok += ", ";
                    hataliStok += item.MalKodu;
                }
            }

            if (hataliStok != "##")
                hataliStok += " için stok miktarları uyuşmuyor.";
            return hataliStok;
        }
    }
}