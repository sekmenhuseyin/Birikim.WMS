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
            if (CheckPerm(Perms.AlimdanIade, PermTypes.Reading) == false) return Redirect("/");
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
                 sql = String.Format("FINSAT6{0}.wms.AlimIptalSecimList @DepoKodu = '{1}', @EvrakNo = {2}, @CHK={3}", item, tbl.DepoID, evraklar[i], chk);
            }  
            //listeyi getir
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            //çapraz stok kontrol
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
        public ActionResult Step3(frmSiparisOnay tbl)
        {
            if (tbl.DepoID == "0" || tbl.checkboxes == "")
                return RedirectToAction("Index");
            if (CheckPerm(Perms.AlimdanIade, PermTypes.Writing) == false) return Redirect("/");
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            var checkList = tbl.checkboxes.Split('#');
            var sirket = tbl.EvrakNos.Split('-')[0];
            var evrak = tbl.EvrakNos.Split('-')[1];
            var hesapKodu = tbl.EvrakNos.Split('-')[2];
            var malkodlari = new List<string>();
            var birimler = new List<string>();
            var miktars = new List<decimal>();
            int i=0;


            //malkodları,miktarları,birimleri ayır
            foreach (var item in checkList)
            {
                if(item!="")
                { 
                string[] tmp2 = item.Split('-');
             
                malkodlari.Add(tmp2[0]);
                birimler.Add(tmp2[1]);
                miktars.Add(tmp2[2].Replace(".", ",").ToDecimal());
                }
            }

            if (checkList == null)
                return RedirectToAction("Index");
            //variables and consts
            int today = fn.ToOADate(), time = fn.ToOATime();
            int idDepo = db.Depoes.Where(m => m.DepoKodu == tbl.DepoID).Select(m => m.ID).FirstOrDefault();
            string GorevNo = db.SettingsGorevNo(today, idDepo).FirstOrDefault();
            string evraknolar = "", alıcılar = "";
            InsertIadeIrsaliye_Result cevap = new InsertIadeIrsaliye_Result();
            Result _Result;
            //loop the list
            cevap = db.InsertIadeIrsaliye(sirket, idDepo, GorevNo, GorevNo, today, "", true, ComboItems.SiparişTopla.ToInt32(), vUser.UserName, today, time, hesapKodu , hesapKodu, 0, evrak).FirstOrDefault();
            foreach (var item in checkList)
            {

                //get stok
                var stokMiktari = db.GetStock(idDepo, malkodlari[i], birimler[i], false).FirstOrDefault();
                if (stokMiktari != null)
                {
                    //sti tablosu
                    IRS_Detay sti = new IRS_Detay()
                    {
                        IrsaliyeID = cevap.IrsaliyeID.Value,
                        MalKodu = malkodlari[i],
                        Birim = birimler[i],
                        Miktar = miktars[i] <= stokMiktari.Value ? miktars[i] : stokMiktari.Value,
                        KynkSiparisNo = evrak
                    };
                    var op2 = new IrsaliyeDetay();
                    _Result = op2.Operation(sti);

                }

                i++;
            }
            //görev tablosu için tekrar yeni ve sade bir liste lazım
            Gorev grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
            grv.Bilgi = "Irs: " + evraknolar + " Alıcı: " + alıcılar;
            db.SaveChanges();
            //get gorev details
            var sql = string.Format("SELECT wms.IRS_Detay.MalKodu, SUM(wms.IRS_Detay.Miktar) AS Miktar, wms.IRS_Detay.Birim " +
                                "FROM wms.IRS_Detay INNER JOIN wms.GorevIRS ON wms.IRS_Detay.IrsaliyeID = wms.GorevIRS.IrsaliyeID " +
                                "WHERE(wms.GorevIRS.GorevID = {0}) " +
                                "GROUP BY wms.IRS_Detay.MalKodu, wms.IRS_Detay.Birim", cevap.GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzemeOnay>(sql).ToList();
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
            return View("Step3", list2);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Step4(int GorevID, int DepoID)
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
            return View("Step4", list);
        }

        /// <summary>
        /// alımdan iade onaylandı
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
                LogActions("WMS", "ReturnSale", "Approve", ComboItems.alEkle, GorevID, "Firma: " + grv.IR.HesapKodu);
            }
            //görevlere git
            return Redirect("/WMS/Tasks");
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