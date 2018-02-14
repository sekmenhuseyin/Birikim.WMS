using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class CableController : RootController
    {
        /// <summary>
        /// kablo sipariş ana sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetListCable(vUser.DepoId), "DepoKodu", "DepoAd");
            return View("Index");
        }

        /// <summary>
        /// seçili malzemeler gruplanmış olarak gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmSiparisSteps tbl)
        {
            //kontrol
            if (tbl.DepoID == "" || tbl.EvrakNos.Count() == 0)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Redirect("/");
            // sql oluştur
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparis2ListStep2 @DepoKodu = '{1}', @EvrakNos = '{2}'", vUser.SirketKodu, tbl.DepoID, string.Join(",", tbl.EvrakNos));
            // listeyi getir
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
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.EvrakNos = tbl.EvrakNos;
            ViewBag.Hatali = sifirStok + hataliStok + "<br /><br />";
            ViewBag.hataliStok = hataliStok == "" && list.Count > 0 ? true : false;
            return View("Step2", list);
        }

        /// <summary>
        /// siparişler içi kablo makara listesi gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisSteps tbl)
        {
            if (tbl.DepoID == "" || tbl.EvrakNos.Count() == 0 || tbl.IDs.Count() == 0)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Reading) == false) return Redirect("/");
            // sql oluştur
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparis2ListStep3 @DepoKodu = '{1}', @EvrakNos = '{2}', @IDs = '{3}'", vUser.SirketKodu, tbl.DepoID, string.Join(",", tbl.EvrakNos), string.Join(",", tbl.IDs));
            // listeyi getir
            var list = db.Database.SqlQuery<frmCableStk>(sql).ToList();
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.Yetki = CheckPerm(Perms.KabloSiparişi, PermTypes.Writing);
            return View("Step3", list);
        }

        /// <summary>
        /// siparişler son adım
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step4(frmSiparisSteps tbl)
        {
            if (tbl.DepoID == "" || tbl.EvrakNos.Count() == 0 || tbl.Miktars.Count() == 0 || tbl.IDs.Count() == 0)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Writing) == false) return Redirect("/");
            // sql oluştur
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparis2ListStep4 @DepoKodu = '{1}', @EvrakNos = '{2}', @IDs = '{3}'", vUser.SirketKodu, tbl.DepoID, string.Join(",", tbl.EvrakNos), string.Join(",", tbl.IDs));
            var list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
            if (list == null)
                return RedirectToAction("Index");
            // variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime(), valorgun = 0;
            var idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            var GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            string alıcılar = "", chk = "", teslimchk = "", aciklama = "";
            var cevap = new InsertIrsaliye_Result();
            Result _Result;
            // loop the list
            foreach (var item in list)
            {
                // irsaliye tablosu
                if (chk != item.Chk || valorgun != item.ValorGun || teslimchk != item.TeslimChk || aciklama != item.Aciklama)
                {
                    cevap = db.InsertIrsaliye(vUser.SirketKodu, idDepo, GorevNo, GorevNo, today, "", true, ComboItems.SiparişTopla.ToInt32(), vUser.UserName, today, time, item.Chk, item.TeslimChk, item.ValorGun, item.EvrakNo, item.Aciklama).FirstOrDefault();
                    // save sck
                    chk = item.Chk;
                    valorgun = item.ValorGun;
                    teslimchk = item.TeslimChk;
                    aciklama = item.Aciklama;
                    alıcılar += item.Unvan + ",";
                }

                // get stok
                var stokMiktari = db.GetStock(idDepo, item.MalKodu, item.Birim, true).FirstOrDefault();
                if (stokMiktari != null)
                {
                    var tyerid = tbl.MalKodus[Array.FindIndex(tbl.IDs, m => m == item.ROW_ID)];
                    var yersatiri = Yerlestirme.Detail(tyerid.ToInt32());
                    var miktar = tbl.Miktars[Array.FindIndex(tbl.IDs, m => m == item.ROW_ID)];
                    // sti tablosu
                    var sti = new IRS_Detay()
                    {
                        IrsaliyeID = cevap.IrsaliyeID.Value,
                        MalKodu = item.MalKodu,
                        Birim = item.Birim,
                        Miktar = miktar <= stokMiktari.Value ? miktar : stokMiktari.Value,
                        MakaraNo = yersatiri.MakaraNo,
                        KynkSiparisID = item.ROW_ID,
                        KynkSiparisNo = item.EvrakNo,
                        KynkSiparisSiraNo = item.SiraNo,
                        KynkSiparisTarih = item.Tarih,
                        KynkSiparisMiktar = item.BirimMiktar,
                        KynkDegisSaat = item.DegisSaat
                    };
                    _Result = IrsaliyeDetay.Operation(sti);

                    // miktarı tabloya ekle
                    var tblyer = new GorevYer()
                    {
                        GorevID = cevap.GorevID.Value,
                        YerID = tyerid.ToInt32(),
                        MalKodu = item.MalKodu,
                        Birim = item.Birim,
                        Miktar = miktar,
                        MakaraNo = yersatiri.MakaraNo,
                        GC = true
                    };
                    TaskYer.Operation(tblyer);
                }
            }
            // görev tablosu için tekrar yeni ve sade bir liste lazım
            var grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
            grv.Bilgi = "Alıcı: " + alıcılar;
            db.SaveChanges();
            // sıralama
            var lstKoridor = db.GetKoridorIdFromGorevId(cevap.GorevID.Value).ToList();
            var asc = false; var sira = 1;
            foreach (var item in lstKoridor)
            {
                var lstBolum = db.GetBolumSiralamaFromGorevId(cevap.GorevID.Value, item.Value, asc).ToList();
                foreach (var item2 in lstBolum)
                {
                    var tmptblyer = new GorevYer()
                    {
                        ID = item2.Value,
                        Sira = sira
                    };
                    sira++;
                    TaskYer.Operation(tmptblyer);
                }

                asc = asc == false ? true : false;
            }
            // listeyi getir
            sql = string.Format("EXEC FINSAT6{0}.wms.getSiparisListStep42 {1}", vUser.SirketKodu, cevap.GorevID);
            var list2 = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            ViewBag.GorevID = cevap.GorevID.Value;
            return View("Step4", list2);
        }

        /// <summary>
        /// sipariş onaylandı
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(int GorevID)
        {
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Writing) == false) return Redirect("/");
            var grv = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (grv.DurumID == ComboItems.Başlamamış.ToInt32())
            {
                // görevi aç
                grv.DurumID = ComboItems.Açık.ToInt32();
                grv.OlusturmaTarihi = fn.ToOADate();
                grv.OlusturmaSaati = fn.ToOATime();
                LogActions("WMS", "Cable", "Approve", ComboItems.alEkle, GorevID, "Firma: " + grv.IR.HesapKodu);
                db.SaveChanges();
            }

            // görevlere git
            return Redirect("/WMS/Tasks");
        }

        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string DepoID)
        {
            if (DepoID == "0") return null;
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparisList @DepoKodu = '{1}', @isKable = 1, @BasTarih = 0, @BitTarih = 0", vUser.SirketKodu, DepoID);
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("../Sales/_Siparis", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "Cable/GetSiparis");
                return PartialView("../Sales/_Siparis", new List<frmSiparisler>());
            }
        }

        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult Step3Details(string Depo, string MalKodu, string Satir)
        {
            ViewBag.satir = Satir;
            var depoID = Store.Detail(Depo).ID;
            var list = Yerlestirme.GetList(depoID, MalKodu);
            return PartialView("Step3Details", list);
        }
    }
}