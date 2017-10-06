using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class RefundController : RootController
    {
        // GET: WMS/Refund
        public ActionResult Index()
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "DepoKodu", "DepoAd");
            //ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
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
            var chk = "";
            int i;
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                if (sirketler.Contains(tmp2[0]) == false) { sirketler.Add(tmp2[0]); evraklar.Add("'" + tmp2[1] + "'"); chk = "'" + tmp2[2] + "'"; }//eğer şirket yoksa ekle
                else
                {
                    i = sirketler.FindIndex(m => m.Contains(tmp2[0]));
                    if (evraklar[i] != "") evraklar[i] += ",";
                    evraklar[i] += "'" + tmp2[1] + "'";
                    chk = "'" + tmp2[1] + "'";
                }
            }
            //sql oluştur
            string sql = ""; i = 0;
            foreach (var item in sirketler)
            {
                 sql = String.Format("FINSAT6{0}.wms.AlimIptalDetail @DepoKodu = '{1}', @EvrakNo = {2}, @CHK={3}", item, tbl.DepoID, evraklar[i], chk);
            }  
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
            ViewBag.hataliStok = hataliStok == "" ? true : false;
            return View("Step2", list);
        }

        /// <summary>
        /// adım 4: yerleştirme kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step4(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.EvrakNos == "" || tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.AlimdanIade, PermTypes.Writing) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var sirket = new List<string>();
            var evraklar = new List<string>();
            var ids = new List<string>();
            var miktars = new List<decimal>();
            int i;
            //şirket id ve evrak nolar bulunur
            string[] tmp = tbl.EvrakNos.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                if (sirket.Contains(tmp2[0]) == false) { evraklar.Add("'" + tmp2[1] + "'"); ids.Add("0"); }//eğer şirket yoksa ekle
                else
                {
                    i = sirket.FindIndex(m => m.Contains(tmp2[0]));
                    if (evraklar[i] != "") evraklar[i] += ",";
                    evraklar[i] += "'" + tmp2[1] + "'";
                }
            }
            //id bulunur
            tmp = tbl.checkboxes.Split('#');
            foreach (var item in tmp)
            {
                string[] tmp2 = item.Split('-');
                i = sirket.FindIndex(m => m.Contains(tmp2[0]));
                ids[i] += ",'" + tmp2[1] + "'";
                miktars.Add(tmp2[2].Replace(".", ",").ToDecimal());
            }
            //sql oluştur
            string sql = ""; i = 0;

            if (ids[i] != "0")
            {
                sql += String.Format("SELECT '{0}' as SirketID, FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID, '{0}-'+CONVERT(VARCHAR(10),FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID) as ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.KayitSaat as Saat, FINSAT6{0}.FINSAT6{0}.SPI.DegisSaat, FINSAT6{0}.FINSAT6{0}.SPI.SiraNo, FINSAT6{0}.FINSAT6{0}.SPI.Chk, FINSAT6{0}.FINSAT6{0}.SPI.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.Birim, FINSAT6{0}.FINSAT6{0}.CHK.Unvan1 as Unvan, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar, (FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar) AS Miktar, FINSAT6{0}.FINSAT6{0}.SPI.ValorGun, FINSAT6{0}.FINSAT6{0}.SPI.TeslimChk " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                    "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 62) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo IN ({2})) AND (FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID IN ({3})) AND (FINSAT6{0}.FINSAT6{0}.SPI.Kod10 IN ('Terminal', 'Onaylandı')) ORDER BY Tarih, Saat", sirket, tbl.DepoID, evraklar[i], ids[i]);
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
                Logger(ex, "Reports/Financial/GetChKCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }

        ///<summary>
        ///depo ve şirket seçince açık siparişler gelecek
        ///</summary>
        [HttpPost]
        public PartialViewResult GetSiparis(string Sirket, string DepoID, string CHK, string Starts, string Ends)
        {
            //ilk kontrol
            if (DepoID == "0" || Sirket == "0" || CHK == "") return null;
            bool tarihler = DateTime.TryParse(Starts, out DateTime StartDate); if (tarihler == false) return null;
            tarihler = DateTime.TryParse(Ends, out DateTime EndDate); if (tarihler == false) return null;
            if (StartDate > EndDate) return null;
            //perm kontrol
            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return null;
            string sql = String.Format("FINSAT6{0}.wms.AlimIptalSiparisList @DepoKodu = '{1}', @CHK='{2}', @BasTarih = {3}, @BitTarih= {4}", Sirket, DepoID.ToString(), CHK, StartDate.ToOADateInt(), EndDate.ToOADateInt());
          
            //return list
            ViewBag.Depo = DepoID;
            ViewBag.Sirket = Sirket;
            ViewBag.CHK = CHK;
            try
            {
                var list = db.Database.SqlQuery<frmSiparisler>(sql).ToList();

                return PartialView("SiparisList", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "Refund/GetSiparis");
                return PartialView("SiparisList", new List<frmSiparisler>());
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
            string sql = String.Format("FINSAT6{0}.wms.AlimIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK='{3}'", tmp[0], tmp[1], tmp[2], tmp[3]);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public PartialViewResult GetRezerv(string MalKodu, string Depo, string Birim)
        {

            if (CheckPerm(Perms.GenelSipariş, PermTypes.Reading) == false) return null;

            var list = db.GetStockRezerv(Birim, MalKodu, Depo).ToList();

            if (list == null)
                return null;
            return PartialView("Rezervler", list);
        }

        [HttpPost]
        public string StokKontrol(string Sirket, string EvrakNo, string CHK, string Depo)
        {
            string sql = String.Format("FINSAT6{0}.wms.AlimIptalDetail @DepoKodu = '{1}', @EvrakNo = '{2}', @CHK='{3}'", Sirket, Depo, EvrakNo, CHK);
            var list = db.Database.SqlQuery<frmSiparisMalzeme>(sql).ToList();
            string hataliStok = "##", sifirStok = ""; 
            foreach (var item in list)
            {
                if (item.WmsStok == 0)
                {
                    if (sifirStok != "") sifirStok += ", ";
                    sifirStok += item.MalKodu;
                }
                else if (item.Stok != item.WmsStok)
                {
                    if (hataliStok != "##") hataliStok += ", ";
                    hataliStok += item.MalKodu;
                }
            }
            if (sifirStok != "")
                sifirStok += " için stok bulunamadı.";
            if (hataliStok != "##")
                hataliStok += " için stok miktarları uyuşmuyor.";
            return sifirStok + hataliStok;
        }
    }


}