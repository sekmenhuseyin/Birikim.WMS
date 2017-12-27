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
        public ActionResult Step2(frmSiparisOnay tbl)
        {
            if (tbl.checkboxes.ToString2() == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.SatistanIade, PermTypes.Reading) == false) return Redirect("/");
            // şirket id ve evrak nolar bulunur
            string[] tmp = tbl.checkboxes.Split('-');
            // Depo - EvrakNo - Chk
            var depo = tmp[0];
            var evrak = tmp[1];
            var chk = tmp[2];
            // listeyi getir
            var sql = string.Format("FINSAT6{0}.wms.SatisIptalSecimList @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK = '{3}'", vUser.SirketKodu, tbl.DepoID, evrak, chk);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            // return
            ViewBag.EvrakNos = vUser.SirketKodu + "-" + evrak + "-" + chk;
            ViewBag.DepoID = depo;
            ViewBag.Evrak = tbl.EvrakNos;
            ViewBag.Tarih = tbl.Tarih;
            return View("Step2", list);
        }
        /// <summary>
        /// step 3
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisOnay tbl)
        {
            if (tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.SatistanIade, PermTypes.Writing) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var checkList = tbl.checkboxes.Split('#');
            // EvrakNo - Chk
            var evrak = tbl.EvrakNos.Split('-')[0];
            var hesapKodu = tbl.EvrakNos.Split('-')[1];
            var malkodlari = new List<string>();
            var birimler = new List<string>();
            var miktars = new List<decimal>();
            var rowID = new List<int>();
            var i = 0;

            var kontrol1 = DateTime.TryParse(tbl.Tarih, out DateTime tmpTarih);
            if (kontrol1 == false)
            {
                db.Logger(vUser.UserName, "", fn.GetIPAddress(), "Tarih hatası: " + tbl.Tarih, "", "WMS/ReturnSale/Step3");
                return Redirect("/");
            }

            var tarih = tmpTarih.ToOADateInt();
            // malkodları,miktarları,birimleri ayır
            foreach (var item in checkList)
            {
                if (item != "")
                {
                    string[] tmp2 = item.Split('!');

                    malkodlari.Add(tmp2[0]);
                    birimler.Add(tmp2[1]);
                    miktars.Add(tmp2[2].Replace(".", ",").ToDecimal());
                    rowID.Add(tmp2[4].Replace(".", ",").ToInt32());
                }
            }

            if (checkList == null)
                return RedirectToAction("Index");
            // variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime();
            int idDepo;
            if (vUser.DepoId != null)//tek bir depoya yetkisi varsa
                idDepo = vUser.DepoId.Value;
            else//tüm depolara yetkisi varsa siparişin deposuna gönder
                idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            // yeni görev
            var GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            var cevap = new InsertIadeIrsaliye_Result();
            Result _Result;
            // loop the list
            cevap = db.InsertIadeIrsaliye(vUser.SirketKodu, idDepo, GorevNo, tbl.SirketID, tarih, "Irs: " + tbl.SirketID + ", Tedarikçi: " + hesapKodu, false, ComboItems.Satıştanİade.ToInt32(), vUser.UserName, today, time, hesapKodu, "", 0, "").FirstOrDefault();
            foreach (var item in checkList)
            {
                // sti tablosu
                var sti = new IRS_Detay()
                {
                    IrsaliyeID = cevap.IrsaliyeID.Value,
                    MalKodu = malkodlari[i],
                    Birim = birimler[i],
                    Miktar = miktars[i],
                    KynkSiparisNo = evrak,
                    KynkSiparisID = rowID[i]
                };
                var op2 = new IrsaliyeDetay();
                _Result = op2.Operation(sti);
                i++;
            }

            // görev tablosu için tekrar yeni ve sade bir liste lazım
            var grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
            grv.Bilgi = "Irs: " + evrak + " Alıcı: " + hesapKodu;
            db.SaveChanges();
            // listeyi getir
            var sql = string.Format("SELECT MalKodu,miktar,Birim,BIRIKIM.wms.fnGetStock('{0}', MalKodu, Birim,0) AS Stok FROM BIRIKIM.wms.IRS_Detay WHERE IrsaliyeID = {1}", tbl.DepoID, cevap.IrsaliyeID.Value);
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
        public PartialViewResult GetSiparis(string DepoID, string CHK, string Starts, string Ends, string EvrakNo, string Tarih)
        {
            // ilk kontrol
            if (CHK == "" || EvrakNo == "" || Tarih == "") return null;
            var tarihler = DateTime.TryParse(Starts, out DateTime StartDate); if (tarihler == false) return null;
            tarihler = DateTime.TryParse(Ends, out DateTime EndDate); if (tarihler == false) return null;
            if (StartDate > EndDate) return null;
            // perm kontrol
            if (CheckPerm(Perms.SatistanIade, PermTypes.Reading) == false) return null;
            var sql = string.Format("FINSAT6{0}.wms.SatisIptalSiparisList @DepoKodu = '{1}', @CHK = '{2}', @BasTarih = {3}, @BitTarih = {4}", vUser.SirketKodu, DepoID.ToString(), CHK, StartDate.ToOADateInt(), EndDate.ToOADateInt());
            // return list
            ViewBag.Depo = DepoID;
            ViewBag.CHK = CHK;
            ViewBag.EvrakNos = EvrakNo;
            ViewBag.Tarih = Tarih;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("SiparisList", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/ReturnSale/GetSiparis");
                return PartialView("SiparisList", new List<frmSiparisler>());
            }
        }
        /// <summary>
        /// detaylar
        /// </summary>
        [HttpPost]
        public JsonResult Details(string ID)
        {
            if (CheckPerm(Perms.SatistanIade, PermTypes.Reading) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            string[] tmp = ID.Split('-');
            var sql = string.Format("FINSAT6{0}.wms.SatisIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK = '{3}'", vUser.SirketKodu, tmp[0], tmp[1], tmp[2]);
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