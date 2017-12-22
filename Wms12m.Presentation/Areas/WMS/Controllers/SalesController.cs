using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class SalesController : RootController
    {
        /// <summary>
        /// sipariş planlama sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "DepoKodu", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// seçili malzemeler gruplanmış olarak gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmSiparisSteps tbl)
        {
            //kontrol
            if (tbl.DepoID == "0" || tbl.EvrakNos.Count() == 0)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Redirect("/");
            // sql oluştur
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparisListStep2 @DepoKodu = '{1}', @EvrakNos = '{2}'", vUser.SirketKodu, tbl.DepoID, string.Join(",", tbl.EvrakNos));
            // listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            // çapraz stok kontrol
            string hataliStok = "", sifirStok = ""; var newList = new List<frmSiparisMalzeme>();
            foreach (var item in list)
            {
                if (item.WmsStok == 0)
                {
                    if (sifirStok != "") sifirStok += ", ";
                    sifirStok += item.MalKodu;
                    newList.Add(item);
                }
                else if (item.Stok != item.WmsStok)
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
        /// siparişleri seçince, siparişlerdeki tüm malzemeler gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisSteps tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos.Count() == 0 || tbl.MalKodus.Count() == 0)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Redirect("/");
            // sql oluştur
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparisListStep3 @DepoKodu = '{1}', @EvrakNos = '{2}', @MalKodus = '{3}'", vUser.SirketKodu, tbl.DepoID, string.Join(",", tbl.EvrakNos), string.Join(",", tbl.MalKodus));
            // listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.Yetki = CheckPerm(Perms.GenelSipariş, PermTypes.Writing);
            return View("Step3", list);
        }
        /// <summary>
        /// adım 4: yerleştirme kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step4(frmSiparisSteps tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos.Count() == 0 || tbl.MalKodus.Count() == 0 || tbl.Miktars.Count() == 0 || tbl.IDs.Count() == 0)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Writing) == false) return Redirect("/");
            // sql oluştur
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparisListStep4 @DepoKodu = '{1}', @EvrakNos = '{2}', @MalKodus = '{3}', @IDs = '{4}'", vUser.SirketKodu, tbl.DepoID, string.Join(",", tbl.EvrakNos), string.Join(",", tbl.MalKodus), string.Join(",", tbl.IDs));
            var list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
            // listeyi getir
            if (list == null)
                return RedirectToAction("Index");
            // variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime(), valorgun = 0;
            var idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            var GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            string evraknolar = "", alıcılar = "", chk = "", teslimchk = "", aciklama = "", srkt = "";
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
                    srkt = vUser.SirketKodu;
                    valorgun = item.ValorGun;
                    teslimchk = item.TeslimChk;
                    aciklama = item.Aciklama;
                    evraknolar += GorevNo + ",";
                    alıcılar += item.Unvan + ",";
                }

                // get stok
                var stokMiktari = db.GetStock(idDepo, item.MalKodu, item.Birim, false).FirstOrDefault();
                if (stokMiktari != null)
                {
                    var miktar = tbl.Miktars[Array.FindIndex(tbl.IDs, m => m == item.ROW_ID)];
                    // sti tablosu
                    var sti = new IRS_Detay()
                    {
                        IrsaliyeID = cevap.IrsaliyeID.Value,
                        MalKodu = item.MalKodu,
                        Birim = item.Birim,
                        Miktar = miktar <= stokMiktari.Value ? miktar : stokMiktari.Value,
                        KynkSiparisID = item.ROW_ID,
                        KynkSiparisNo = item.EvrakNo,
                        KynkSiparisSiraNo = item.SiraNo,
                        KynkSiparisTarih = item.Tarih,
                        KynkSiparisMiktar = item.BirimMiktar,
                        KynkDegisSaat = item.DegisSaat
                    };
                    var op2 = new IrsaliyeDetay();
                    _Result = op2.Operation(sti);
                }
            }

            // görev tablosu için tekrar yeni ve sade bir liste lazım
            var grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
            grv.Bilgi = "Alıcı: " + alıcılar;
            db.SaveChanges();
            // get gorev details
            sql = string.Format("SELECT wms.IRS_Detay.MalKodu, SUM(wms.IRS_Detay.Miktar) AS Miktar, wms.IRS_Detay.Birim " +
                                "FROM wms.IRS_Detay INNER JOIN wms.GorevIRS ON wms.IRS_Detay.IrsaliyeID = wms.GorevIRS.IrsaliyeID " +
                                "WHERE(wms.GorevIRS.GorevID = {0}) " +
                                "GROUP BY wms.IRS_Detay.MalKodu, wms.IRS_Detay.Birim", cevap.GorevID);
            list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
            foreach (var item in list)
            {
                var tmpYer = db.Yers.Where(m => m.MalKodu == item.MalKodu && m.Birim == item.Birim && m.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.DepoID && m.Miktar > 0).OrderBy(m => m.Miktar).ToList();
                decimal toplam = 0, miktar = 0;
                if (tmpYer != null)
                {
                    foreach (var itemyer in tmpYer)
                    {
                        if (itemyer.Miktar >= (item.Miktar - toplam))
                            miktar = item.Miktar - toplam;
                        else
                            miktar = itemyer.Miktar;
                        toplam += miktar;
                        // miktarı tabloya ekle
                        var tblyer = new GorevYer()
                        {
                            GorevID = cevap.GorevID.Value,
                            YerID = itemyer.ID,
                            MalKodu = item.MalKodu,
                            Birim = item.Birim,
                            Miktar = miktar,
                            GC = true
                        };
                        TaskYer.Operation(tblyer);
                        // toplam yeterli miktardaysa
                        if (toplam == item.Miktar) break;
                    }
                }
            }

            // listeyi getir
            sql = string.Format("SELECT wms.Yer.HucreAd, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim, wms.Yer.Miktar AS Stok " +
                                "FROM wms.GorevYer INNER JOIN wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID " +
                                "WHERE (wms.GorevYer.GorevID = {1})", idDepo, cevap.GorevID.Value);
            var list2 = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            ViewBag.GorevID = cevap.GorevID.Value;
            ViewBag.DepoID = idDepo;
            var listsirk = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in listsirk)
                liste.Add(item);

            ViewBag.Sirket = liste;
            return View("Step4", list2);
        }
        /// <summary>
        /// rota adımlarını gör
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step5(int GorevID, int DepoID)
        {
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Writing) == false) return Redirect("/");
            // sıralama
            var lstKoridor = db.GetKoridorIdFromGorevId(GorevID).ToList();
            var asc = false; var sira = 1;
            foreach (var item in lstKoridor)
            {
                var lstBolum = db.GetBolumSiralamaFromGorevId(GorevID, item.Value, asc).ToList();
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
            var sql = string.Format("SELECT wms.Yer.HucreAd, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim,  wms.GorevYer.Sira, wms.Yer.Miktar AS Stok " +
                                "FROM wms.GorevYer INNER JOIN wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID " +
                                "WHERE (wms.GorevYer.GorevID = {1}) ORDER BY  wms.GorevYer.Sira", DepoID, GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            ViewBag.GorevID = GorevID;
            var listsirk = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in listsirk)
                liste.Add(item);

            ViewBag.Sirket = liste;
            return View("Step5", list);
        }
        /// <summary>
        /// sipariş onaylandı
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(int GorevID)
        {
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Writing) == false) return Redirect("/");
            var grv = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (grv.DurumID == ComboItems.Başlamamış.ToInt32())
            {
                // görevi aç
                grv.DurumID = ComboItems.Açık.ToInt32();
                grv.OlusturmaTarihi = fn.ToOADate();
                grv.OlusturmaSaati = fn.ToOATime();
                db.SaveChanges();
                LogActions("WMS", "Sales", "Approve", ComboItems.alEkle, GorevID, "Firma: " + grv.IR.HesapKodu);
            }

            // görevlere git
            return Redirect("/WMS/Tasks");
        }
        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string DepoID, string Starts, string Ends)
        {
            // kontrol
            if (DepoID == "0") return null;
            string[] tmp = Starts.Split('.');
            var StartDate = new DateTime(tmp[2].ToInt32(), tmp[1].ToInt32(), tmp[0].ToInt32());
            tmp = Ends.Split('.');
            var EndDate = new DateTime(tmp[2].ToInt32(), tmp[1].ToInt32(), tmp[0].ToInt32());
            if (StartDate > EndDate) return null;
            // return list
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparisList @DepoKodu = '{1}', @isKable = 0, @BasTarih = {2}, @BitTarih = {3}", vUser.SirketKodu, DepoID.ToString(), StartDate.ToOADateInt(), EndDate.ToOADateInt());
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("_Siparis", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "Sales/GetSiparis");
                return PartialView("_Siparis", new List<frmSiparisler>());
            }
        }
        /// <summary>
        /// evrak noya ait mallar
        /// </summary>
        [HttpPost]
        public JsonResult Details(string ID)
        {
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSiparisDetail @EvrakNo = '{1}'", vUser.SirketKodu, ID);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}