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
        public ActionResult Step2(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.checkboxes.ToString2() == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Redirect("/");
            //şirket id ve evrak nolar bulunur
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            string[] tmp = tbl.checkboxes.Split('#');
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            int i;
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                if (sirketler.Contains(tmp2[0]) == false) { sirketler.Add(tmp2[0]); evraklar.Add("'" + tmp2[1] + "'"); }//eğer şirket yoksa ekle
                else
                {
                    i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                    if (evraklar[i] != "") evraklar[i] += ",";
                    evraklar[i] += "'" + tmp2[1] + "'";
                }
            }
            //sql oluştur
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (sql != "") sql += " UNION ";
                sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim, " +
                                            "FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}') as Stok, wms.fnGetStock('{1}',FINSAT6{0}.FINSAT6{0}.SPI.MalKodu,FINSAT6{0}.FINSAT6{0}.SPI.Birim, 0) AS WmsStok, wms.fnGetRezervStock('{1}',FINSAT6{0}.FINSAT6{0}.SPI.MalKodu,FINSAT6{0}.FINSAT6{0}.SPI.Birim) AS WmsRezerv " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN " +
                                            "FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu LEFT OUTER JOIN " +
                                            "FINSAT6{0}.FINSAT6{0}.DST WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.STK.MalKodu =FINSAT6{0}. FINSAT6{0}.DST.MalKodu AND FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}' " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND(FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND(FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN({2})) AND(FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN('Terminal', 'Onaylandı')) AND " +
                                    "FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID WHERE (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 11) AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID)", item, tbl.DepoID, evraklar[i]);
                i++;
            }
            sql = "SELECT MIN(SirketID) as SirketID, MalKodu, SUM(Miktar) AS Miktar, Birim, Stok, WmsStok, WmsRezerv FROM (" + sql + ") AS t1 GROUP BY MalKodu, Birim, Stok, WmsStok, WmsRezerv ORDER BY MalKodu";
            //listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            //çapraz stok kontrol
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
            //return
            ViewBag.EvrakNos = tbl.checkboxes;
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.Hatali = sifirStok + hataliStok + "<br /><br />";
            ViewBag.hataliStok = hataliStok == "" && list.Count>0 ? true : false;
            return View("Step2", list);
        }
        /// <summary>
        /// siparişleri seçince, siparişlerdeki tüm malzemeler gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Redirect("/");
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            int i;
            //şirket id ve evrak nolar bulunur
            string malkodlari = "'" + (tbl.checkboxes + "0").Replace("#", "','") + "'";
            string[] tmp = tbl.EvrakNos.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                if (sirketler.Contains(tmp2[0]) == false) { sirketler.Add(tmp2[0]); evraklar.Add("'" + tmp2[1] + "'"); }//eğer şirket yoksa ekle
                else
                {
                    i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                    if (evraklar[i] != "") evraklar[i] += ",";
                    evraklar[i] += "'" + tmp2[1] + "'";
                }
            }
            //sql oluştur
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (sql != "") sql += " UNION ";
                sql += String.Format("SELECT '{0}' AS SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim, " +
                                    "wms.fnGetStock('{1}',FINSAT6{0}.FINSAT6{0}.SPI.MalKodu,FINSAT6{0}.FINSAT6{0}.SPI.Birim, 1) AS WmsStok " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND( FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.MalKodu IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN('Terminal', 'Onaylandı')) AND " +
                                    "((SELECT SUM(Miktar) AS Expr1 FROM wms.Yer AS Yer_2 WITH (NOLOCK) WHERE (MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AND (Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim)) IS NOT NULL) AND " +
                                    "FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID WHERE (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 11) AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID)", item, tbl.DepoID, evraklar[i], malkodlari);
                i++;
            }
            sql += " ORDER BY MalKodu";
            //listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            ViewBag.EvrakNos = tbl.EvrakNos;
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.Yetki = CheckPerm(Perms.GenelSipariş, PermTypes.Writing);
            return View("Step3", list);
        }
        /// <summary>
        /// adım 4: yerleştirme kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step4(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Writing) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            var ids = new List<string>();
            var miktars = new List<decimal>();
            int i;
            //şirket id ve evrak nolar bulunur
            string[] tmp = tbl.EvrakNos.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                if (sirketler.Contains(tmp2[0]) == false) { sirketler.Add(tmp2[0]); evraklar.Add("'" + tmp2[1] + "'"); ids.Add("0"); }//eğer şirket yoksa ekle
                else
                {
                    i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                    if (evraklar[i] != "") evraklar[i] += ",";
                    evraklar[i] += "'" + tmp2[1] + "'";
                }
            }
            //id bulunur
            tmp = tbl.checkboxes.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                ids[i] += ",'" + tmp2[1] + "'";
                miktars.Add(tmp2[2].Replace(".", ",").ToDecimal());
            }
            //sql oluştur
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (ids[i] != "0")
                {
                    if (sql != "") sql += " UNION ";
                    sql += String.Format("SELECT '{0}' as SirketID,FINSAT6{0}.FINSAT6{0}.MFK.Aciklama, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID, '{0}-'+CONVERT(VARCHAR(10),FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID) as ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat as Saat, FINSAT6{0}.FINSAT6{0}.SPI.DegisSaat, FINSAT6{0}.FINSAT6{0}.SPI.SiraNo, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.Birim, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 as Unvan, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.ValorGun, FINSAT6{0}.FINSAT6{0}.SPI.TeslimChk " +
                                        
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                        "INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.MFK.EvrakNo=FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo  AND  FINSAT6{0}.FINSAT6{0}.MFK.KynkEvrakTip=FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip AND  FINSAT6{0}.FINSAT6{0}.MFK.HesapKod=FINSAT6{0}.FINSAT6{0}.SPI.Chk " +
                                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN ({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) ", item, tbl.DepoID, evraklar[i], ids[i]);
                }
                i++;
            }
            sql += "ORDER BY Tarih,Saat";
            var list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
            if (list == null)
                return RedirectToAction("Index");
            //variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime(), valorgun = 0;
            int idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            string GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            string evraknolar = "", alıcılar = "", chk = "", teslimchk = "",aciklama="";
            InsertIrsaliye_Result cevap = new InsertIrsaliye_Result();
            Result _Result;
            //loop the list
            foreach (var item in list)
            {
                //irsaliye tablosu
                if (chk != item.Chk || valorgun != item.ValorGun || teslimchk != item.TeslimChk || aciklama != item.Aciklama)
                {
                    cevap = db.InsertIrsaliye(item.SirketID, idDepo, GorevNo, GorevNo, today, "", true, ComboItems.SiparişTopla.ToInt32(), vUser.UserName, today, time, item.Chk, item.TeslimChk, item.ValorGun, item.EvrakNo, item.Aciklama).FirstOrDefault();
                    //save sck
                    chk = item.Chk;
                    valorgun = item.ValorGun;
                    teslimchk = item.TeslimChk;
                    aciklama = item.Aciklama;
                    evraknolar += GorevNo + ",";
                    alıcılar += item.Unvan + ",";
                }
                //get stok
                var stokMiktari = db.GetStock(idDepo, item.MalKodu, item.Birim, false).FirstOrDefault();
                if (stokMiktari != null)
                {
                    var miktar = miktars[Array.FindIndex(tmp, m => m.Contains(item.ID))];
                    //sti tablosu
                    IRS_Detay sti = new IRS_Detay()
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
            //görev tablosu için tekrar yeni ve sade bir liste lazım
            Gorev grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
            grv.Bilgi = "Irs: " + evraknolar + " Alıcı: " + alıcılar;
            db.SaveChanges();
            //get gorev details
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
                        //miktarı tabloya ekle
                        GorevYer tblyer = new GorevYer()
                        {
                            GorevID = cevap.GorevID.Value,
                            YerID = itemyer.ID,
                            MalKodu = item.MalKodu,
                            Birim = item.Birim,
                            Miktar = miktar,
                            GC = true
                        };
                        TaskYer.Operation(tblyer);
                        //toplam yeterli miktardaysa
                        if (toplam == item.Miktar) break;
                    }
                }
            }
            //listeyi getir
            sql = string.Format("SELECT wms.Yer.HucreAd, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim, wms.Yer.Miktar AS Stok " +
                                "FROM wms.GorevYer INNER JOIN wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID " +
                                "WHERE (wms.GorevYer.GorevID = {1})", idDepo, cevap.GorevID.Value);
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
            return View("Step4", list2);
        }
        /// <summary>
        /// rota adımlarını gör
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step5(int GorevID, int DepoID)
        {
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Writing) == false) return Redirect("/");
            //sıralama
            var lstKoridor = db.GetKoridorIdFromGorevId(GorevID).ToList();
            bool asc = false; int sira = 1;
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
            //listeyi getir
            string sql = string.Format("SELECT wms.Yer.HucreAd, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim,  wms.GorevYer.Sira, wms.Yer.Miktar AS Stok " +
                                "FROM wms.GorevYer INNER JOIN wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID " +
                                "WHERE (wms.GorevYer.GorevID = {1}) ORDER BY  wms.GorevYer.Sira", DepoID, GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            ViewBag.GorevID = GorevID;
            var listsirk = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in listsirk)
            {
                liste.Add(item);
            }
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
            Gorev grv = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (grv.DurumID == ComboItems.Başlamamış.ToInt32())
            {
                //görevi aç
                grv.DurumID = ComboItems.Açık.ToInt32();
                grv.OlusturmaTarihi = fn.ToOADate();
                grv.OlusturmaSaati = fn.ToOATime();
                db.SaveChanges();
                LogActions("WMS", "Sales", "Approve", ComboItems.alEkle, GorevID, "Firma: " + grv.IR.HesapKodu);
            }
            //görevlere git
            return Redirect("/WMS/Tasks");
        }
        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string DepoID, string Starts, string Ends)
        {
            //ilk kontrol
            if (DepoID == "0") return null;
            string[] tmp = Starts.Split('.');
            DateTime StartDate = new DateTime(tmp[2].ToInt32(), tmp[1].ToInt32(), tmp[0].ToInt32());
            tmp = Ends.Split('.');
            DateTime EndDate = new DateTime(tmp[2].ToInt32(), tmp[1].ToInt32(), tmp[0].ToInt32());
            if (StartDate > EndDate) return null;
            //perm kontrol
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return null;
            string sql = "";
            //loop dbs
            var tmps = db.GetSirketDBs().ToList();
            foreach (var item in tmps)
            {
                if (sql != "") sql += " union ";
                sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3 AS FaturaAdres, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama, COUNT(FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AS Çeşit, SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, MIN(FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat) as Saat " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = FINSAT6{0}.FINSAT6{0}.MFK.EvrakNo AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = FINSAT6{0}.FINSAT6{0}.MFK.EvrakTarih AND FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.MFK.HesapKod AND FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = FINSAT6{0}.FINSAT6{0}.MFK.KynkEvrakTip INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Tarih >= '{2}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Tarih <= '{3}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND (FINSAT6{0}.FINSAT6{0}.STK.Kod1 <> 'KKABLO') AND " +
                                    "(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) > 0 AND " +
                                    "FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID WHERE (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 11) AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID) " +
                                    "GROUP BY FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama", item, DepoID.ToString(), StartDate.ToOADateInt(), EndDate.ToOADateInt());
            }
            //return list
            ViewBag.Depo = DepoID;
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
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            string[] tmp = ID.Split('-');
            string sql = String.Format("FINSAT6{0}.wms.getSiparisDetail @DepoKodu = '{1}', @EvrakNo = '{2}'", tmp[0], tmp[1], tmp[2]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// get rezerv
        /// </summary>
        [HttpPost]
        public PartialViewResult GetRezerv(string MalKodu, string Depo, string Birim)
        {

            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return null;

            var list = db.GetStockRezerv(Birim, MalKodu, Depo).ToList();

            if (list == null)
                return null;
            return PartialView("Rezervler", list);
        }
    }
}