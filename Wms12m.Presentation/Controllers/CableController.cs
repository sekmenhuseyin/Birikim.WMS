using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m.Presentation.Controllers
{
    public class CableController : RootController
    {
        /// <summary>
        /// kablo sipariş ana sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Kablo Siparişi", PermTypes.Reading) == false) return Redirect("/");
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
            if (CheckPerm("Kablo Siparişi", PermTypes.Reading) == false) return Redirect("/");
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
                sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND(FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND(FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN({2})) AND(FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN('Terminal', 'Onaylandı')) AND " +
                                    "((SELECT SUM(Miktar) AS Expr1 FROM wms.Yer AS Yer_2 WITH (NOLOCK) WHERE (MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AND (Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim)) IS NOT NULL)", item, tbl.DepoID, evraklar[i]);
                i++;
            }
            sql += " ORDER BY SPI.MalKodu";
            //listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            ViewBag.EvrakNos = tbl.checkboxes;
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.KabloDepoID = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.KabloDepoID).FirstOrDefault().Value;
            return View("Step2", list);
        }
        /// <summary>
        /// siparişler içi kablo makara listesi gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step3(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm("Kablo Siparişi", PermTypes.Reading) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            var ids = new List<string>();
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
            }
            //sql oluştur
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (ids[i] != "0")
                {
                    if (sql != "") sql += " UNION ";
                    sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.SiraNo, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.MalAdi4, FINSAT6{0}.FINSAT6{0}.STK.Nesne2, FINSAT6{0}.FINSAT6{0}.STK.Kod15, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN ({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) ", item, tbl.DepoID, evraklar[i], ids[i]);
                }
                i++;
            }
            var list = db.Database.SqlQuery<frmCableStk>(sql).ToList();
            if (list == null)
                return RedirectToAction("Index");
            //get kablodepoid
            var KabloDepoID = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.KabloDepoID).FirstOrDefault().Value;
            //mysql için yeni sorgu
            sql = "";
            foreach (var item in list)
            {
                if (sql != "") sql += " UNION ";
                sql += string.Format("SELECT kblstok.id, marka, cins, kesit, miktar as stok, kblStok.depo, renk, makara, rezerve, satici, sure, tarih, {4} as ROW_ID, {5} as SiraNo, '{6}' as EvrakNo, '{7}' as MalKodu, {8} as miktar, '{9}' as SirketID, '{10}' as MalAdi " +
                                    "FROM kblStok INNER JOIN depo ON kblStok.depo = depo.depo " +
                                    "WHERE depo.id = {0} AND (marka = '{1}') AND (cins = '{2}') AND (kesit = '{3}')", KabloDepoID, item.MalAdi4, item.Nesne2, item.Kod15, item.ROW_ID, item.SiraNo, item.EvrakNo, item.MalKodu, item.Miktar.ToInt32(), item.SirketID, item.MalAdi);
            }
            sql += " ORDER BY MalKodu";
            //exec sql
            ViewBag.EvrakNos = tbl.EvrakNos;
            ViewBag.DepoID = tbl.DepoID;
            ViewBag.Yetki = CheckPerm("Kablo Siparişi", PermTypes.Writing);
            using (KabloEntities dbx = new KabloEntities())
            {
                var listx = dbx.Database.SqlQuery<frmCableSiparis>(sql).ToList();
                return View("Step3", listx);
            }
        }
        /// <summary>
        /// siparişler son adım
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step4(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm("Kablo Siparişi", PermTypes.Writing) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            var ids = new List<string>();
            var miktars = new List<decimal>();
            var rowids = new List<int>();
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
                rowids.Add(tmp2[2].ToInt32());
                miktars.Add(tmp2[3].Replace(".", ",").ToDecimal());
            }
            //sql oluştur
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (ids[i] != "0")
                {
                    if (sql != "") sql += " UNION ";
                    sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID, '{0}-'+CONVERT(VARCHAR(10),FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID) as ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.SiraNo, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi4, FINSAT6{0}.FINSAT6{0}.STK.Nesne2, FINSAT6{0}.FINSAT6{0}.STK.Kod15, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.Birim, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 as Unvan, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.ValorGun, FINSAT6{0}.FINSAT6{0}.SPI.TeslimChk " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN ({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) ", item, tbl.DepoID, evraklar[i], ids[i]);
                }
                i++;
            }
            var list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
            if (list == null)
                return RedirectToAction("Index");
            //variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime(), valorgun = 0;
            int idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            string GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            string evraknolar = "", alıcılar = "", chk = "", teslimchk = "";
            InsertIrsaliye_Result cevap = new InsertIrsaliye_Result();
            Result _Result;
            //loop the list
            foreach (var item in list)
            {
                //irsaliye tablosu
                if (chk != item.Chk || valorgun != item.ValorGun || teslimchk != item.TeslimChk)
                {
                    cevap = db.InsertIrsaliye(item.SirketID, idDepo, GorevNo, GorevNo, today, "", true, ComboItems.SiparişTopla.ToInt32(), vUser.UserName, today, time, item.Chk, item.TeslimChk, item.ValorGun, item.EvrakNo).FirstOrDefault();
                    //save sck
                    chk = item.Chk;
                    valorgun = item.ValorGun;
                    teslimchk = item.TeslimChk;
                    evraknolar += GorevNo + ",";
                    alıcılar += item.Unvan + ",";
                }
                //get stok
                var stokMiktari = db.GetStock(idDepo, item.MalKodu, item.Birim, true).FirstOrDefault();
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
                        KynkSiparisMiktar = item.BirimMiktar
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
            if (CheckPerm("Kablo Siparişi", PermTypes.Writing) == false) return Redirect("/");
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
            if (CheckPerm("Kablo Siparişi", PermTypes.Writing) == false) return Redirect("/");
            Gorev grv = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            if (grv.DurumID == ComboItems.Başlamamış.ToInt32())
            {
                //görevi aç
                grv.DurumID = ComboItems.Açık.ToInt32();
                grv.OlusturmaTarihi = fn.ToOADate();
                grv.OlusturmaSaati = fn.ToOATime();
                db.SaveChanges();
            }
            //görevlere git
            return Redirect("/Tasks");
        }
        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string DepoID)
        {
            if (DepoID == "0") return null;
            if (CheckPerm("Kablo Siparişi", PermTypes.Reading) == false) return null;
            string sql = "";
            var item = db.GetSirketDBs().FirstOrDefault();
            sql = String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3 AS FaturaAdres, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama, COUNT(FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AS Çeşit, SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, MIN(FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat) as Saat " +
                                "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = FINSAT6{0}.FINSAT6{0}.MFK.EvrakNo AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = FINSAT6{0}.FINSAT6{0}.MFK.EvrakTarih AND FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.MFK.HesapKod AND FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = FINSAT6{0}.FINSAT6{0}.MFK.KynkEvrakTip INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO') AND " +
                                "(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) > 0 AND " +
                                "FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID WHERE (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 11) AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID) " +
                                "GROUP BY FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama, FINSAT6{0}.FINSAT6{0}.STK.Kod1", item, DepoID.ToString());
            ViewBag.Depo = DepoID;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("_Siparis", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "Sell/GetSiparis");
                return null;
            }
        }
        /// <summary>
        /// evrak noya ait mallar
        /// </summary>
        [HttpPost]
        public JsonResult Details(string ID)
        {
            if (CheckPerm("Kablo Siparişi", PermTypes.Reading) == false) return null;
            string[] tmp = ID.Split('-');
            string sql = String.Format("FINSAT6{0}.wms.getSiparisDetail @DepoKodu = '{1}', @EvrakNo = '{2}'", tmp[0], tmp[1], tmp[2]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}