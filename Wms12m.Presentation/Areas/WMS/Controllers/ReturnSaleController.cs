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
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
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
            //şirket id ve evrak nolar bulunur
            string[] tmp = tbl.checkboxes.Split('-');
            //SirketID - Depo - EvrakNo - Chk
            var sirket = tmp[0];
            var depo = tmp[1];
            var evrak = tmp[2];
            var chk = tmp[3];
            //listeyi getir
            string sql = String.Format("FINSAT6{0}.wms.SatisIptalSecimList @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK = '{3}'", sirket, tbl.DepoID, evrak, chk);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            //return
            ViewBag.EvrakNos = sirket + "-" + evrak + "-" + chk;
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
            //SirketID - EvrakNo - Chk
            var sirket = tbl.EvrakNos.Split('-')[0];
            var evrak = tbl.EvrakNos.Split('-')[1];
            var hesapKodu = tbl.EvrakNos.Split('-')[2];
            var malkodlari = new List<string>();
            var birimler = new List<string>();
            var miktars = new List<decimal>();
            var rowID = new List<int>();
            int i = 0;

            bool kontrol1 = DateTime.TryParse(tbl.Tarih, out DateTime tmpTarih);
            if (kontrol1 == false)
            {
                db.Logger(vUser.UserName, "", fn.GetIPAddress(), "Tarih hatası: " + tbl.Tarih, "", "WMS/ReturnSale/Step3");
                return Redirect("/");
            }
            int tarih = tmpTarih.ToOADateInt();
            //malkodları,miktarları,birimleri ayır
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
            //variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime();
            int idDepo;
            if (vUser.DepoId != null)//tek bir depoya yetkisi varsa
                idDepo = vUser.DepoId.Value;
            else//tüm depolara yetkisi varsa siparişin deposuna gönder
                idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            //yeni görev
            string GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            InsertIadeIrsaliye_Result cevap = new InsertIadeIrsaliye_Result();
            Result _Result;
            //loop the list
            cevap = db.InsertIadeIrsaliye(sirket, idDepo, GorevNo, tbl.SirketID, tarih, "Irs: " + tbl.SirketID + ", Tedarikçi: " + hesapKodu, false, ComboItems.Satıştanİade.ToInt32(), vUser.UserName, today, time, hesapKodu, "", 0, "").FirstOrDefault();
            foreach (var item in checkList)
            {
                //sti tablosu
                IRS_Detay sti = new IRS_Detay()
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
            //görev tablosu için tekrar yeni ve sade bir liste lazım
            Gorev grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
            grv.Bilgi = "Irs: " + evrak + " Alıcı: " + hesapKodu;
            db.SaveChanges();
            //listeyi getir
            var sql = string.Format("SELECT MalKodu,miktar,Birim,BIRIKIM.wms.fnGetStock('{0}', MalKodu, Birim,0) AS Stok FROM BIRIKIM.wms.IRS_Detay WHERE IrsaliyeID = {1}", tbl.DepoID, cevap.IrsaliyeID.Value);
            var list2 = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            ViewBag.GorevID = cevap.GorevID.Value;
            ViewBag.DepoID = idDepo;
            var listsirk = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in listsirk)
            {
                liste.Add(item);
            }
            ViewBag.Sirket = liste;
            return View("Step3", list2);
        }
        /// <summary>
        /// alımdan iade onaylandı
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(int GorevID)
        {
            if (CheckPerm(Perms.SatistanIade, PermTypes.Writing) == false) return Redirect("/");

            IR ir = db.IRS.Where(a => a.ID == a.Gorevs.Where(n => n.ID == GorevID).Select(n => n.IrsaliyeID).FirstOrDefault()).FirstOrDefault();
            Gorev grv = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            //görevi aç
            grv.DurumID = ComboItems.Açık.ToInt32();
            grv.OlusturmaTarihi = fn.ToOADate();
            grv.OlusturmaSaati = fn.ToOATime();
            ir.Onay = true;
            db.SaveChanges();
            LogActions("WMS", "ReturnSale", "Approve", ComboItems.alEkle, GorevID, "Firma: " + grv.IR.HesapKodu);
            //görevlere git
            return Redirect("/WMS/Tasks");
        }
        ///<summary>
        ///depo ve şirket seçince açık siparişler gelecek
        ///</summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string Sirket, string DepoID, string CHK, string Starts, string Ends, string EvrakNo, string Tarih)
        {
            //ilk kontrol
            if (Sirket == "" || CHK == "" || EvrakNo == "" || Tarih == "") return null;
            bool tarihler = DateTime.TryParse(Starts, out DateTime StartDate); if (tarihler == false) return null;
            tarihler = DateTime.TryParse(Ends, out DateTime EndDate); if (tarihler == false) return null;
            if (StartDate > EndDate) return null;
            //perm kontrol
            if (CheckPerm(Perms.SatistanIade, PermTypes.Reading) == false) return null;
            string sql = String.Format("FINSAT6{0}.wms.SatisIptalSiparisList @DepoKodu = '{1}', @CHK = '{2}', @BasTarih = {3}, @BitTarih = {4}", Sirket, DepoID.ToString(), CHK, StartDate.ToOADateInt(), EndDate.ToOADateInt());
            //return list
            ViewBag.Depo = DepoID;
            ViewBag.Sirket = Sirket;
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
            string sql = String.Format("FINSAT6{0}.wms.SatisIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK = '{3}'", tmp[0], tmp[1], tmp[2], tmp[3]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get chk codes
        /// </summary>
        public JsonResult GetChKCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = "";
            //generate sql
            sql = String.Format("FINSAT6{0}.[wms].[CHKSearch4] @HesapKodu = N'', @Unvan = N'{1}', @top = 200", id.ToString(), term);
            //return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/ReturnSale/GetChKCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// rezerv mallar
        /// </summary>
        [HttpPost]
        public PartialViewResult GetRezerv(string MalKodu, string Depo, string Birim)
        {

            if (CheckPerm(Perms.SatistanIade, PermTypes.Reading) == false) return null;
            var list = db.GetStockRezerv(Birim, MalKodu, Depo).ToList();
            if (list == null)
                return null;
            return PartialView("Rezervler", list);
        }
        /// <summary>
        /// stok kontrol
        /// </summary>
        [HttpPost]
        public string StokKontrol(string Sirket, string EvrakNo, string CHK, string Depo)
        {
            string sql = String.Format("FINSAT6{0}.wms.SatisIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK='{3}'", Sirket, Depo, EvrakNo, CHK);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            string hataliStok = "##";
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