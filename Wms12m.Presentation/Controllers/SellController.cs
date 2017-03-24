using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;

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
                if (sirketler.Contains(tmp2[0]) == false) { sirketler.Add(tmp2[0]); evraklar.Add(tmp2[1]); }//eğer şirket yoksa ekle
                else
                {
                    i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                    if (evraklar[i] != "") evraklar[i] += ",";
                    evraklar[i] += tmp2[1];
                }
            }
            //liste getirilir
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                if (sql != "") sql += " UNION ";
                sql += String.Format("SELECT wms.Yer.MalKodu, wms.Yer.Birim, wms.Yer.Miktar AS Stok, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar " +
                                    "FROM wms.Yer INNER JOIN FINSAT6{0}.FINSAT6{0}.SPI ON wms.Yer.MalKodu = FINSAT6{0}.FINSAT6{0}.SPI.MalKodu AND wms.Yer.Birim = FINSAT6{0}.FINSAT6{0}.SPI.Birim INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND(FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND(FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN('{1}')) " +
                                    "GROUP BY wms.Yer.MalKodu, wms.Yer.Birim, wms.Yer.Miktar, FINSAT6{0}.FINSAT6{0}.STK.MalAdi " +
                                    "HAVING (SUM(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) > 0)", item, evraklar[i]);
                i++;

            }
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
            //using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            //{
            //    string[] mals = tbl.checkboxes.Split('#');
            //    string[] evraks = tbl.EvrakNos.Split('#');
            //    var list = (from s in Dinamik.Context.SPIs
            //                join s2 in Dinamik.Context.STKs on s.MalKodu equals s2.MalKodu
            //                where evraks.Contains(s.EvrakNo) && mals.Contains(s.MalKodu)  && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
            //                select new { ID = s.ROW_ID, EvrakNo = s.EvrakNo, Tarih = s.Tarih, MalKodu = s.MalKodu, MalAdi = s2.MalAdi, Miktar = (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar), Birim = s.Birim }).ToList();
            //    var list2 = (from s in db.Yers
            //                where s.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.DepoID
            //                group s by new { s.Birim, s.MalKodu } into g
            //                select new { g.Key.MalKodu, g.Key.Birim, Miktar = g.Sum(m => m.Miktar) }).ToList();
            //    var list3 = (from s in list
            //                 join s2 in list2 on new { s.Birim, s.MalKodu }  equals new { s2.Birim, s2.MalKodu }
            //                 orderby s.MalKodu
            //                 select new frmSiparisMalzemeDetay { ID = s.ID, EvrakNo = s.EvrakNo, Tarih = s.Tarih, MalKodu = s.MalKodu, MalAdi = s.MalAdi, Miktar = s.Miktar, Birim = s.Birim, Stok = s2.Miktar }).ToList();
            //    ViewBag.SirketID = tbl.SirketID;
            //    ViewBag.DepoID = tbl.DepoID;
            //    ViewBag.EvrakNos = tbl.EvrakNos;
            //    return View("Step3", list3);
            //}
            return View("Step3");
        }
        /// <summary>
        /// sipariş onaylandı
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(frmSiparisOnay tbl)
        {
            //using (DinamikModelContext Dinamik = new DinamikModelContext(tbl.SirketID))
            //{
            //    string[] ids = (tbl.checkboxes + "0").Split('#');
            //    int[] myInts = Array.ConvertAll(ids, int.Parse);
            //    string[] evraks = tbl.EvrakNos.Split('#');
            //    string chk = "", evrakNo = ""; Result _Result;
            //    string evraknolar = "", alıcılar = "";
            //    int idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            //    string GorevNo = db.SettingsGorevNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
            //    int today = DateTime.Today.ToOADateInt();
            //    int time = DateTime.Now.SaatiAl();
            //    InsertIrsaliye_Result cevap = new InsertIrsaliye_Result();
            //    //listeler
            //    var list = (from s in Dinamik.Context.SPIs
            //                join s2 in Dinamik.Context.CHKs on s.Chk equals s2.HesapKodu
            //                where evraks.Contains(s.EvrakNo) && myInts.Contains(s.ROW_ID) && s.SiparisDurumu == 0 && s.KynkEvrakTip == 62 && s.Depo == tbl.DepoID && (s.BirimMiktar - s.TeslimMiktar - s.KapatilanMiktar) > 0
            //                group new { s, s2 } by new { s.EvrakNo, s.Chk, s2.Unvan1, s.MalKodu, s.Birim } into g
            //                orderby g.Key.Chk
            //                select new { g.Key.EvrakNo, g.Key.Chk, g.Key.Unvan1, g.Key.MalKodu, Miktar = g.Sum(m => m.s.BirimMiktar - m.s.TeslimMiktar - m.s.KapatilanMiktar), g.Key.Birim }).ToList();
            //    var list2 = (from s in db.Yers
            //                 where s.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.DepoID
            //                 group s by new { s.Birim, s.MalKodu } into g
            //                 select new { g.Key.MalKodu, g.Key.Birim, Miktar = g.Sum(m => m.Miktar) }).ToList();
            //    var list3 = (from s in list
            //                 join s2 in list2 on new { s.Birim, s.MalKodu } equals new { s2.Birim, s2.MalKodu }
            //                 select new { s.EvrakNo, s.Chk, s.Unvan1, s.MalKodu, Miktar = s.Miktar > s2.Miktar ? s2.Miktar : s.Miktar, s.Birim }).ToList();
            //    foreach (var item in list3)
            //    {
            //        //irsaliye tablosu
            //        if (chk != item.Chk)
            //        {
            //            evrakNo = db.SettingsIrsaliyeNo(DateTime.Today.ToOADateInt()).FirstOrDefault();
            //            cevap = db.InsertIrsaliye(tbl.SirketID, idDepo, GorevNo, evrakNo, "", true, ComboItems.SiparişTopla.ToInt32(), vUser.Id, vUser.UserName, today, time, item.Chk).FirstOrDefault();
            //            //save sck
            //            chk = item.Chk;
            //            evraknolar += evrakNo + ",";
            //            alıcılar += item.Unvan1 + ",";
            //        }
            //        //sti tablosu
            //        IRS_Detay sti = new IRS_Detay();
            //        sti.IrsaliyeID = cevap.IrsaliyeID.Value;
            //        sti.MalKodu = item.MalKodu;
            //        sti.Birim = item.Birim;
            //        sti.Miktar = item.Miktar;
            //        var op2 = new Stok();
            //        _Result = op2.Operation(sti);
            //    }
            //    //görev tablosu
            //    Gorev grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
            //    grv.Bilgi = "Irs: " + evraknolar + " Alıcı: " + alıcılar;
            //    db.SaveChanges();
            //    //get gorev yer
            //    var tablo = TaskYer.GetList(cevap.GorevID.Value);
            //    if (tablo.Count == 0)
            //    {
            //        //get gorev details
            //        var gList = db.GetIrsDetayfromGorev(cevap.GorevID.Value).ToList();
            //        foreach (var item in gList)
            //        {
            //            var tmp = db.Yers.Where(m => m.MalKodu == item.MalKodu && m.Birim == item.Birim).OrderBy(m => m.Miktar).ToList();
            //            decimal toplam = 0, miktar = 0;
            //            if (tmp != null)
            //            {
            //                foreach (var itemyer in tmp)
            //                {
            //                    if (itemyer.Miktar >= (item.Miktar - toplam))
            //                        miktar = item.Miktar.Value - toplam;
            //                    else
            //                        miktar = itemyer.Miktar;
            //                    toplam += miktar;
            //                    //miktarı tabloya ekle
            //                    GorevYer tblyer = new GorevYer();
            //                    tblyer.GorevID = item.ID;
            //                    tblyer.YerID = itemyer.ID;
            //                    tblyer.MalKodu = item.MalKodu;
            //                    tblyer.Birim = item.Birim;
            //                    tblyer.Miktar = miktar;
            //                    tblyer.GC = true;
            //                    TaskYer.Operation(tblyer);
            //                    //toplam yeterli miktardaysa
            //                    if (toplam == item.Miktar) break;
            //                }
            //            }
            //        }
            //    }
            //    //sıralama
            //    var lstKoridor = db.GetKoridorIdFromGorevId(cevap.GorevID).ToList();
            //    bool asc = false; int sira = 0;
            //    foreach (var item in lstKoridor)
            //    {
            //        var lstBolum = db.GetBolumSiralamaFromGorevId(cevap.GorevID, item.Value, asc).ToList();
            //        foreach (var item2 in lstBolum)
            //        {
            //            var tmptblyer = new GorevYer();
            //            tmptblyer.ID = item2.Value;
            //            tmptblyer.Sira = sira;
            //            sira++;
            //            TaskYer.Operation(tmptblyer);
            //        }
            //        asc = asc == false ? true : false;
            //    }
            //    return Redirect("/Gorev");
            //}
            return Redirect("/Gorev");
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
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar > 0)  AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 = 'Terminal' OR FINSAT6{0}.FINSAT6{0}.SPI.Kod10 = 'Onaylandı') " +
                                    "GROUP BY FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1, FINSAT6{0}.FINSAT6{0}.CHK.GrupKod, FINSAT6{0}.FINSAT6{0}.CHK.FaturaAdres3, FINSAT6{0}.FINSAT6{0}.MFK.Aciklama", item, ID.ToString());
            }
            ViewBag.Depo = ID;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();
                return PartialView("_Siparis", list);
            }
            catch (Exception)
            {
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
                        "WHERE(FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND(FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = '{2}') AND(FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND(FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND(FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar > 0)", tmp[0], tmp[1], tmp[2]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}