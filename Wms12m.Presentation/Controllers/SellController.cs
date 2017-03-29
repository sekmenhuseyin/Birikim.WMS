using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class SellController : RootController
    {
        /// <summary>
        /// sipariş planlama sayfası
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DepoID = new SelectList(Store.GetList(), "DepoKodu", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// seçili malzemeler gruplanmış olarak gelecek
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step2(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.checkboxes == "")
                return RedirectToAction("Index");
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
                sql += String.Format("SELECT FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim, (SELECT SUM(Miktar) AS Expr1 FROM wms.Yer WITH (NOLOCK) WHERE (MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AND (Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim)) as Stok " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND(FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND(FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN({2})) AND(FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN('Terminal', 'Onaylandı')) " +
                                    "GROUP BY FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.Birim " +
                                    "HAVING ((SELECT SUM(Miktar) AS Expr1 FROM wms.Yer AS Yer_2 WITH(NOLOCK) WHERE(MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AND(Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim)) IS NOT NULL)", item, tbl.DepoID, evraklar[i]);
                i++;
            }
            //listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            ViewBag.EvrakNos = tbl.checkboxes;
            ViewBag.DepoID = tbl.DepoID;
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
            var sirketler = new List<string>();
            var evraklar = new List<string>();
            int i;
            //şirket id ve evrak nolar bulunur
            string malkodlari = "'" + (tbl.checkboxes + "0").Replace(",", "','") + "'";
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
                sql += String.Format("SELECT '{0}' AS SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim, (SELECT SUM(Miktar) AS Expr1 FROM wms.Yer WITH(NOLOCK) WHERE(MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AND(Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim)) AS Stok " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND( FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.MalKodu IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN('Terminal', 'Onaylandı')) AND " +
                                    "((SELECT SUM(Miktar) AS Expr1 FROM wms.Yer AS Yer_2 WITH (NOLOCK) WHERE (MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AND (Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim)) IS NOT NULL)", item, tbl.DepoID, evraklar[i], malkodlari);
                i++;
            }
            //listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            ViewBag.EvrakNos = tbl.EvrakNos;
            ViewBag.DepoID = tbl.DepoID;
            return View("Step3", list);
        }
        /// <summary>
        /// sipariş onaylandı
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "")
                return RedirectToAction("Index");
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
                if (sirketler.Contains(tmp2[0]) == false) { sirketler.Add(tmp2[0]); evraklar.Add("'" + tmp2[1] + "'"); ids.Add(""); }//eğer şirket yoksa ekle
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
                if (ids[i] != "") ids[i] += ",";
                ids[i] += "'" + tmp2[1] + "'";
            }
            //sql oluştur
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (sql != "") sql += " UNION ";
                sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.Birim, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 as Unvan, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, (SELECT SUM(Miktar) AS Expr1 FROM wms.Yer WITH(NOLOCK) WHERE(MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AND(Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim)) AS Stok " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN ({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID IN ({3}))  AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı'))", item, tbl.DepoID, evraklar[i], ids[i]);
                i++;
            }
            var list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
            if (list != null)
            {
                //variables and consts
                int idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
                string GorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                int today = DateTime.Today.ToOADateInt();
                int time = DateTime.Now.SaatiAl();
                InsertIrsaliye_Result cevap = new InsertIrsaliye_Result();
                string evraknolar = "", alıcılar = "", chk = "", evrakNo = "";
                Result _Result;
                //loop the list
                foreach (var item in list)
                {
                    //irsaliye tablosu
                    if (chk != item.Chk)
                    {
                        evrakNo = db.SettingsIrsaliyeNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
                        cevap = db.InsertIrsaliye(item.SirketID, idDepo, GorevNo, evrakNo, "", true, ComboItems.SiparişTopla.ToInt32(), vUser.Id, vUser.UserName, today, time, item.Chk).FirstOrDefault();
                        //save sck
                        chk = item.Chk;
                        evraknolar += evrakNo + ",";
                        alıcılar += item.Unvan + ",";
                    }
                    //sti tablosu
                    IRS_Detay sti = new IRS_Detay();
                    sti.IrsaliyeID = cevap.IrsaliyeID.Value;
                    sti.MalKodu = item.MalKodu;
                    sti.Birim = item.Birim;
                    sti.Miktar = item.Miktar <= item.Stok ? item.Miktar : item.Stok;
                    var op2 = new Stok();
                    _Result = op2.Operation(sti);
                }
                //görev tablosu
                Gorev grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                grv.Bilgi = "Irs: " + evraknolar + " Alıcı: " + alıcılar;
                db.SaveChanges();
                //get gorev yer
                var tablo = TaskYer.GetList(cevap.GorevID.Value);
                if (tablo.Count == 0)
                {
                    //get gorev details
                    var gList = db.GetIrsDetayfromGorev(cevap.GorevID.Value).ToList();
                    foreach (var item in gList)
                    {
                        var tmpYer = db.Yers.Where(m => m.MalKodu == item.MalKodu && m.Birim == item.Birim).OrderBy(m => m.Miktar).ToList();
                        decimal toplam = 0, miktar = 0;
                        if (tmpYer != null)
                        {
                            foreach (var itemyer in tmpYer)
                            {
                                if (itemyer.Miktar >= (item.Miktar - toplam))
                                    miktar = item.Miktar.Value - toplam;
                                else
                                    miktar = itemyer.Miktar;
                                toplam += miktar;
                                //miktarı tabloya ekle
                                GorevYer tblyer = new GorevYer();
                                tblyer.GorevID = item.ID;
                                tblyer.YerID = itemyer.ID;
                                tblyer.MalKodu = item.MalKodu;
                                tblyer.Birim = item.Birim;
                                tblyer.Miktar = miktar;
                                tblyer.GC = true;
                                TaskYer.Operation(tblyer);
                                //toplam yeterli miktardaysa
                                if (toplam == item.Miktar) break;
                            }
                        }
                    }
                }
                //sıralama
                var lstKoridor = db.GetKoridorIdFromGorevId(cevap.GorevID).ToList();
                bool asc = false; int sira = 0;
                foreach (var item in lstKoridor)
                {
                    var lstBolum = db.GetBolumSiralamaFromGorevId(cevap.GorevID, item.Value, asc).ToList();
                    foreach (var item2 in lstBolum)
                    {
                        var tmptblyer = new GorevYer();
                        tmptblyer.ID = item2.Value;
                        tmptblyer.Sira = sira;
                        sira++;
                        TaskYer.Operation(tmptblyer);
                    }
                    asc = asc == false ? true : false;
                }
                return Redirect("/Lists/Topla/" + cevap.GorevID);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// depo ve şirket seçince açık siparişler gelecek
        /// </summary>
        [HttpPost]
        public PartialViewResult GetSiparis()
        {
            var ID = Url.RequestContext.RouteData.Values["id"];
            if (ID == null || ID.ToString2() == "0") return null;
            string sql = "";
            var tmp = db.GetSirketDBs().ToList();
            foreach (var item in tmp)
            {
                if (sql != "") sql += " union ";
                sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 AS Unvan, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3 AS FaturaAdres, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama, COUNT(FINSAT6{0}.FINSAT6{0}.SPI.MalKodu) AS Çeşit, SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, MIN(FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat) as Saat " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = FINSAT6{0}.FINSAT6{0}.MFK.EvrakNo AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = FINSAT6{0}.FINSAT6{0}.MFK.EvrakTarih AND FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.MFK.HesapKod AND FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = FINSAT6{0}.FINSAT6{0}.MFK.KynkEvrakTip INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) " +
                                    "GROUP BY FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama", item, ID.ToString());
            }
            ViewBag.Depo = ID;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("_Siparis", list);
            }
            catch (Exception ex)
            {
                db.Logger(vUser.UserName, Environment.MachineName, fn.GetIPAddress(), ex.Message, ex.InnerException.Message, "Sell/GetSiparis");
                return null;
            }
        }
        /// <summary>
        /// evrak noya ait mallar
        /// </summary>
        [HttpPost]
        public JsonResult Details(string ID)
        {
            string[] tmp = ID.Split('-');
            string sql = String.Format("SELECT FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim " +
                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = '{2}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı'))", tmp[0], tmp[1], tmp[2]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}