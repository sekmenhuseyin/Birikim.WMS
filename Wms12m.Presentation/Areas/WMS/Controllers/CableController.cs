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
        public ActionResult Step2(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.checkboxes.ToString2() == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Reading) == false) return Redirect("/");
            // şirket id ve evrak nolar bulunur
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

            // sql oluştur
            var sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (sql != "") sql += " UNION ";
                sql += string.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.ChK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat as Saat, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim, " +
                                            "FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}') as GunesStok, wms.fnGetStock('{1}',FINSAT6{0}.FINSAT6{0}.SPI.MalKodu,FINSAT6{0}.FINSAT6{0}.SPI.Birim, 0) AS WmsStok, wms.fnGetRezervStock('{1}',FINSAT6{0}.FINSAT6{0}.SPI.MalKodu,FINSAT6{0}.FINSAT6{0}.SPI.Birim) AS WmsRezerv " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu = FINSAT6{0}.FINSAT6{0}.SPI.Chk INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND(FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND(FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN({2})) AND(FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN('Terminal', 'Onaylandı')) AND " +
                                    "FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID WHERE (BIRIKIM.wms.Gorev.DurumID = 9) AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID)", item, tbl.DepoID, evraklar[i]);
                i++;
            }

            sql += " ORDER BY SPI.MalKodu";
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
                sifirStok = sifirStok + " için stok bulunamadı.<br />";
            if (hataliStok != "")
                hataliStok += hataliStok + " için stok miktarları uyuşmuyor.<br />";
            // return
            ViewBag.EvrakNos = tbl.checkboxes;
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.KabloDepoID = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.KabloDepoID).FirstOrDefault().Value;
            ViewBag.Hatali = sifirStok + hataliStok + "<br /><br />";
            ViewBag.hataliStok = hataliStok == "" ? true : false;
            return View("Step2", list);
        }
        /// <summary>
        /// siparişler içi kablo makara listesi gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "" || tbl.EvrakNos == null || tbl.checkboxes == null)
                return RedirectToAction("Index");
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Reading) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            var ids = new List<string>();
            int i;
            // şirket id ve evrak nolar bulunur
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

            // id bulunur
            tmp = tbl.checkboxes.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                ids[i] += ",'" + tmp2[1] + "'";
            }

            // sql oluştur
            var sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (ids[i] != "0")
                {
                    if (sql != "") sql += " UNION ";
                    sql += string.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.ChK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat as Saat, FINSAT6{0}.FINSAT6{0}.SPI.SiraNo, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, wms.fnGetStock('{1}',FINSAT6{0}.FINSAT6{0}.SPI.MalKodu,FINSAT6{0}.FINSAT6{0}.SPI.Birim, 1) AS Stok " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu = FINSAT6{0}.FINSAT6{0}.SPI.Chk INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN ({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND " +
                                    "FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID WHERE (BIRIKIM.wms.Gorev.DurumID = 9) AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID)", item, tbl.DepoID, evraklar[i], ids[i]);
                }

                i++;
            }

            var list = db.Database.SqlQuery<frmCableStk>(sql).ToList();
            ViewBag.EvrakNos = tbl.EvrakNos;
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.Yetki = CheckPerm(Perms.KabloSiparişi, PermTypes.Writing);
            return View("Step3", list);
        }
        /// <summary>
        /// siparişler son adım
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step4(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Writing) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            var ids = new List<string>();
            var miktars = new List<decimal>();
            var rowids = new List<int>();
            int i;
            // şirket id ve evrak nolar bulunur
            string[] tmp = tbl.EvrakNos.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                // tmp2[0] = "SirketID";
                // tmp2[1] = "ROW_ID";
                // tmp2[2] = "MakaraNo-YerID";
                // tmp2[3] = "Miktar";
                if (sirketler.Contains(tmp2[0]) == false) { sirketler.Add(tmp2[0]); evraklar.Add("'" + tmp2[1] + "'"); ids.Add("0"); }//eğer şirket yoksa ekle
                else
                {
                    i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                    if (evraklar[i] != "") evraklar[i] += ",";
                    evraklar[i] += "'" + tmp2[1] + "'";
                }
            }

            // id bulunur
            tmp = tbl.checkboxes.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                ids[i] += ",'" + tmp2[1] + "'";
                rowids.Add(tmp2[2].ToInt32());
                miktars.Add(tmp2[3].Replace(".", ",").ToDecimal());
            }

            // sql oluştur
            var sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (ids[i] != "0")
                {
                    if (sql != "") sql += " UNION ";
                    sql += string.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID, '{0}-'+CONVERT(VARCHAR(10),FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID) as ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.DegisSaat, FINSAT6{0}.FINSAT6{0}.SPI.SiraNo, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi4, FINSAT6{0}.FINSAT6{0}.STK.Nesne2, FINSAT6{0}.FINSAT6{0}.STK.Kod15, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.Birim, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 as Unvan, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.ValorGun, FINSAT6{0}.FINSAT6{0}.SPI.TeslimChk " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                        "INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.MFK.EvrakNo=FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo  AND  FINSAT6{0}.FINSAT6{0}.MFK.KynkEvrakTip=FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip AND  FINSAT6{0}.FINSAT6{0}.MFK.HesapKod=FINSAT6{0}.FINSAT6{0}.SPI.Chk " +
                                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN ({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) ", item, tbl.DepoID, evraklar[i], ids[i]);
                }

                i++;
            }

            var list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
            if (list == null)
                return RedirectToAction("Index");
            // variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime(), valorgun = 0;
            var idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            var GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            string evraknolar = "", alıcılar = "", chk = "", teslimchk = "", aciklama = "";
            var cevap = new InsertIrsaliye_Result();
            Result _Result;
            // loop the list
            foreach (var item in list)
            {
                // irsaliye tablosu
                if (chk != item.Chk || valorgun != item.ValorGun || teslimchk != item.TeslimChk || aciklama != item.Aciklama)
                {
                    cevap = db.InsertIrsaliye(item.SirketID, idDepo, GorevNo, GorevNo, today, "", true, ComboItems.SiparişTopla.ToInt32(), vUser.UserName, today, time, item.Chk, item.TeslimChk, item.ValorGun, item.EvrakNo, item.Aciklama).FirstOrDefault();
                    // save sck
                    chk = item.Chk;
                    valorgun = item.ValorGun;
                    teslimchk = item.TeslimChk;
                    aciklama = item.Aciklama;
                    evraknolar += GorevNo + ",";
                    alıcılar += item.Unvan + ",";
                }

                // get stok
                var stokMiktari = db.GetStock(idDepo, item.MalKodu, item.Birim, true).FirstOrDefault();
                if (stokMiktari != null)
                {
                    var tyerid = rowids[Array.FindIndex(tmp, m => m.Contains(item.ROW_ID.ToString()))];
                    var yersatiri = Yerlestirme.Detail(tyerid);
                    var miktar = miktars[Array.FindIndex(tmp, m => m.Contains(item.ID))];
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
                    using (var op2 = new IrsaliyeDetay())
                        _Result = op2.Operation(sti);

                    // miktarı tabloya ekle
                    var tblyer = new GorevYer()
                    {
                        GorevID = cevap.GorevID.Value,
                        YerID = tyerid,
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
            grv.Bilgi = "Irs: " + evraknolar + " Alıcı: " + alıcılar;
            db.SaveChanges();
            // listeyi getir
            sql = string.Format("SELECT wms.Yer.HucreAd, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim, wms.Yer.Miktar AS Stok, wms.Yer.MakaraNo " +
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
            if (CheckPerm(Perms.KabloSiparişi, PermTypes.Writing) == false) return Redirect("/");
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
            var sql = string.Format("SELECT wms.Yer.HucreAd, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim,  wms.GorevYer.Sira, wms.Yer.Miktar AS Stok, wms.Yer.MakaraNo " +
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
            // loop dbs
            var sql = string.Format("SELECT * FROM FINSAT6{0}.wms.fnSiparisList('{1}', 1, 0, 0)", vUser.SirketKodu, DepoID);
            ViewBag.Depo = DepoID;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("_Siparis", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "Cable/GetSiparis");
                return PartialView("_Siparis", new List<frmSiparisler>());
            }
        }
        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult Step3Details(string id)
        {
            string[] tmp = id.Split('-');
            string depoid = tmp[0], dbname = tmp[1], rowid = tmp[2];
            ViewBag.satir = tmp[3];
            var sql = string.Format("SELECT wms.Yer.* " +
                "FROM wms.Yer INNER JOIN FINSAT6{0}.FINSAT6{0}.SPI ON wms.Yer.MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu INNER JOIN wms.Depo ON wms.Yer.DepoID = wms.Depo.ID " +
                "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID = {1}) AND (wms.Depo.DepoKodu = '{2}') AND (wms.Yer.Miktar > 0)", dbname, rowid, depoid);
            try
            {
                var list = db.Database.SqlQuery<Yer>(sql).ToList();
                return PartialView("Step3Details", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "Cable/Step3Details");
                return null;
            }
        }
    }
}