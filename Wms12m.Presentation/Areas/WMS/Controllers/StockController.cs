using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class StockController : RootController
    {
        /// <summary>
        /// stok ana sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            return View("Index");
        }
        /// <summary>
        /// listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult List(string Id)
        {
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            ViewBag.Manual = false;
            //id'ye göre liste döner
            string[] ids = Id.Split('#');
            try
            {
                if (ids[2] != "0" && ids[2] != "null" && ids[2].ToString2() != "") //bir kattaki ait malzemeler
                {
                    ViewBag.Manual = ids[3].ToBool();

                    return PartialView("List", Yerlestirme.GetList(ids[2].ToInt32()));
                }
                else if (ids[1] != "0" && ids[1] != "null" && ids[1].ToString2() != "") //bir raftaki ait malzemeler
                    return PartialView("List", Yerlestirme.GetListFromRaf(ids[1].ToInt32()));
                else// if (ids[0] != "0") //tüm depoya ait malzemeler: burada timeout verebilir
                    return PartialView("List", Yerlestirme.GetListFromDepo(ids[0].ToInt32()));
            }
            catch (Exception ex)
            {
                Logger(ex, "Stock/List");
                return PartialView("List", new List<Yer>());
            }
        }
        /// <summary>
        /// ürün bazlı liste
        /// </summary>
        [HttpPost]
        public PartialViewResult List2(string Id)
        {
            var list = db.Database.SqlQuery<frmStokList2>(string.Format("SELECT wms.fnGetRezervStock(wms.Depo.DepoKodu,wms.Yer.MalKodu,wms.Yer.Birim) AS WmsRezerv, wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, wms.Yer.Birim, SUM(wms.Yer.Miktar) AS Miktar FROM wms.Yer INNER JOIN wms.Depo ON wms.Yer.DepoID = wms.Depo.ID WHERE (wms.Yer.MalKodu = '{0}') GROUP BY wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, wms.Yer.Birim, wms.fnGetRezervStock(wms.Depo.DepoKodu,wms.Yer.MalKodu,wms.Yer.Birim)", Id)).ToList();
            return PartialView("List2", list);
        }
        /// <summary>
        /// kablo stok ana sayfası
        /// </summary>
        public ActionResult Cable()
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetListCable(vUser.DepoId), "ID", "DepoAd");
            return View("Cable");
        }
        /// <summary>
        /// kablo stoğunu getirir
        /// </summary>
        [HttpPost]
        public PartialViewResult CableList(string Id)
        {
            //dbler tempe aktarılıyor
            var list = db.GetSirketDBs();
            List<string> liste = new List<string>();
            foreach (var item in list) { liste.Add(item); }
            ViewBag.Sirket = liste;
            ViewBag.Manual = false;
            //id'ye göre liste döner
            string[] ids = Id.Split('#');
            string sirketkodu = db.GetSirketDBs().FirstOrDefault();
            string sql; string var;
            if (ids[2] != "0" && ids[2] != "null" && ids[2].ToString2() != "") //bir kattaki ait malzemeler
            {
                sql = "KatID";
                var = ids[2].ToString2();
            }
            else if (ids[1] != "0" && ids[1] != "null" && ids[1].ToString2() != "") //bir raftaki ait malzemeler
            {
                sql = "RafID";
                var = ids[1].ToString2();
            }
            else// if (ids[0] != "0")
            {
                sql = "wms.Yer.DepoID";
                var = ids[0].ToString2();
            }
            try
            {
                sql = string.Format("SELECT wms.Yer.ID, wms.Yer.KatID, wms.Yer.DepoID, wms.Yer.HucreAd, wms.Yer.MalKodu, wms.Yer.Miktar, wms.Yer.Birim, wms.Yer.MakaraNo, FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit " +
                    "FROM wms.Yer WITH(NOLOCK) INNER JOIN wms.Kat WITH(NOLOCK) ON wms.Yer.KatID = wms.Kat.ID INNER JOIN wms.Bolum WITH(NOLOCK) ON wms.Kat.BolumID = wms.Bolum.ID INNER JOIN wms.Raf WITH(NOLOCK) ON wms.Bolum.RafID = wms.Raf.ID INNER JOIN wms.Koridor WITH(NOLOCK) ON wms.Raf.KoridorID = wms.Koridor.ID INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON wms.Yer.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                    "WHERE (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO') AND {1} = {2}", sirketkodu, sql, var);
                var lst = db.Database.SqlQuery<frmCableStok>(sql).ToList();
                return PartialView("CableList", lst);
            }
            catch (Exception ex)
            {
                Logger(ex, "Stock/CableList");
                return PartialView("CableList", new List<frmCableStok>());
            }
        }
        /// <summary>
        /// evrak noya ait mallar
        /// </summary>
        [HttpPost]
        public JsonResult CableMovements(int ID)
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return null;
            using (KabloEntities dbx = new KabloEntities())
            {
                var list = dbx.harekets.Where(m => m.id == ID).OrderBy(m => m.tarih).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// mal hareketleri
        /// </summary>
        public ActionResult History()
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            return View("History");
        }
        /// <summary>
        /// hareketler alt sayfa
        /// </summary>
        [HttpPost]
        public PartialViewResult HistoryList(string Id)
        {
            if (Id.Contains("#") == false) return null;
            var ids = Id.Split('#');
            var depoID = ids[1].ToInt32();
            var depoKodu = Store.Detail(depoID).DepoKodu;
            string kod = ids[0];
            string sql = "";
            //get stok from finsat
            var tmps = db.GetSirketDBs();
            foreach (var item in tmps)
            {
                if (sql != "") sql += " UNION ";
                sql += string.Format("SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                    "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                    "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = '{2}') AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)", item, depoKodu, kod);
            }
            sql = "SELECT ISNULL(SUM(Stok),0) as Stok from (" + sql + ")t";
            //return
            var list = db.Yer_Log.Where(m => m.MalKodu == kod && m.DepoID == depoID).OrderBy(m => m.KayitTarihi).ThenBy(m => m.KayitSaati).ThenBy(m => m.ID).ToList();
            ViewBag.Stok = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            if (list.Count != 0)
            {
                sql = string.Format("SELECT wms.fnGetRezervStock('{0}','{1}','{2}')", depoKodu, kod, list[0].Birim);
                ViewBag.RezervMiktar = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            }
            else
            {
                ViewBag.RezervMiktar = 0;
            }
            return PartialView("HistoryList", list);
        }
        /// <summary>
        /// manual ekle
        /// </summary>
        public ActionResult ManualPlacement()
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
            ViewBag.BolumID = new SelectList(Section.GetList(0), "ID", "BolumAd");
            ViewBag.KatID = new SelectList(Floor.GetList(0), "ID", "KatAd");
            return View("ManualPlacement", new Yer());
        }
        /// <summary>
        /// manual ekle
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ManualPlacement(Yer tbl, bool GC)
        {
            if (CheckPerm(Perms.Stok, PermTypes.Writing) == false || tbl.Miktar < 0) return Json(false, JsonRequestBehavior.AllowGet);
            //yerleştirme kaydı yapılır
            if (GC == false)
            {
                if (tbl.MakaraNo == "" || tbl.MakaraNo == null)
                {
                    var kkablo = db.Database.SqlQuery<string>(string.Format("SELECT Kod1 FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = '{1}')", db.GetSirketDBs().FirstOrDefault(), tbl.MalKodu)).FirstOrDefault();
                    if (kkablo == "KKABLO")
                    {
                        tbl.MakaraNo = "Boş-" + db.SettingsMakaraNo(tbl.DepoID).FirstOrDefault();
                    }
                }
                if (tbl.MakaraNo == "" || tbl.MakaraNo == null)
                {
                    var tmp2 = Yerlestirme.Detail(tbl.KatID, tbl.MalKodu, tbl.Birim);
                    if (tmp2 == null)
                    {
                        tmp2 = new Yer()
                        {
                            KatID = tbl.KatID,
                            MalKodu = tbl.MalKodu,
                            Birim = tbl.Birim,
                            Miktar = tbl.Miktar,
                            MakaraDurum = true
                        };
                        Yerlestirme.Insert(tmp2, 0, vUser.Id);
                    }
                    else
                    {
                        tmp2.Miktar += tbl.Miktar;
                        Yerlestirme.Update(tmp2, 0, vUser.Id, false, tbl.Miktar);
                    }
                }
                else
                {
                    var tmp2 = db.Yers.Where(m => m.DepoID == tbl.DepoID && m.MakaraNo == tbl.MakaraNo).FirstOrDefault();
                    if (tmp2 == null)
                    {
                        tmp2 = new Yer()
                        {
                            KatID = tbl.KatID,
                            MalKodu = tbl.MalKodu,
                            MakaraNo = tbl.MakaraNo,
                            Birim = tbl.Birim,
                            Miktar = tbl.Miktar,
                            MakaraDurum = true
                        };
                        Yerlestirme.Insert(tmp2, 0, vUser.Id);
                    }
                    else
                    {
                        return Json(new Result(false, "Bu makara no kullanılmaktadır"), JsonRequestBehavior.AllowGet);
                    }
                }
                //add to mysql
                if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                {
                    var listedb = db.GetSirketDBs().ToList();
                    string sql = "";
                    foreach (var item in listedb)
                    {
                        if (sql != "") sql += " UNION ";
                        sql += String.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit " +
                                            "FROM FINSAT6{0}.FINSAT6{0}.STK " +
                                            "WHERE (FINSAT6{0}.FINSAT6{0}.STK.Kod1 = 'KKABLO') AND (FINSAT6{0}.FINSAT6{0}.STK.MalKodu = '{1}')", item, tbl.MalKodu);
                    }
                    sql = "SELECT * from (" + sql + ") t";
                    var stks = db.Database.SqlQuery<frmCableStk>(sql).FirstOrDefault();
                    if (stks != null)
                    {
                        using (KabloEntities dbx = new KabloEntities())
                        {
                            string depo;
                            var kbldepoID = db.Depoes.Where(m => m.ID == vUser.DepoId).Select(m => m.KabloDepoID).FirstOrDefault();
                            if (kbldepoID == null) depo = dbx.depoes.Select(m => m.depo1).FirstOrDefault();
                            else depo = dbx.depoes.Where(m => m.id == kbldepoID).Select(m => m.depo1).FirstOrDefault();
                            try
                            {
                                if (GC == false)
                                {
                                    //sid bul
                                    var sid = dbx.indices.Where(m => m.cins == stks.Cins && m.kesit == stks.Kesit).FirstOrDefault();
                                    if (sid == null)
                                    {
                                        sid = new index() { cins = stks.Cins, kesit = stks.Kesit, agirlik = 0 };
                                        dbx.indices.Add(sid);
                                        dbx.SaveChanges();
                                    }
                                    //stoğa kaydet
                                    stok tbls = new stok()
                                    {
                                        marka = stks.Marka,
                                        cins = stks.Cins,
                                        kesit = stks.Kesit,
                                        sid = sid.id,
                                        depo = depo,
                                        renk = "",
                                        makara = "KAPALI",
                                        rezerve = "0",
                                        sure = new TimeSpan(),
                                        tarih = DateTime.Now,
                                        tip = "",
                                        rmiktar = 0,
                                        miktar = tbl.Miktar,
                                        makarano = tbl.MakaraNo
                                    };
                                    dbx.stoks.Add(tbls);
                                }
                                else
                                {
                                }
                                dbx.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Logger(ex, "Stock/ManualPlacement");
                                //return Json(new Result(false, "Kablo kaydı hariç her şey tamamlandı!"), JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            else
            {
                if (tbl.MakaraNo == "" || tbl.MakaraNo == null)
                {
                    var tmp2 = Yerlestirme.Detail(tbl.KatID, tbl.MalKodu, tbl.Birim);
                    if (tmp2 == null)
                        return Json(new Result(false, "Seçili yerde bu ürün bulunamadı."), JsonRequestBehavior.AllowGet);
                    if (tmp2.Miktar < tbl.Miktar)
                        return Json(new Result(false, "Seçili yerde çıkış yapılmak istenilen sayıda ürün yok"), JsonRequestBehavior.AllowGet);
                    tmp2.Miktar -= tbl.Miktar;
                    Yerlestirme.Update(tmp2, 0, vUser.Id, true, tbl.Miktar);
                }
                else
                {
                    var tmp2 = db.Yers.Where(m => m.KatID == tbl.KatID && m.MalKodu == tbl.MalKodu && m.Birim == tbl.Birim && m.MakaraNo == tbl.MakaraNo).FirstOrDefault();
                    if (tmp2 == null)
                        return Json(new Result(false, "Seçili yerde bu ürün bulunamadı."), JsonRequestBehavior.AllowGet);
                    if (tmp2.Miktar < tbl.Miktar)
                        return Json(new Result(false, "Seçili yerde çıkış yapılmak istenilen sayıda ürün yok"), JsonRequestBehavior.AllowGet);
                    tmp2.Miktar -= tbl.Miktar;
                    Yerlestirme.Update(tmp2, 0, vUser.Id, true, tbl.Miktar);
                }
            }
            //return
            return Json(new Result(true), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// manual yer değiştir
        /// </summary>
        public ActionResult ManualMovement()
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            ViewBag.RafID = new SelectList(Shelf.GetList(0), "ID", "RafAd");
            ViewBag.BolumID = new SelectList(Section.GetList(0), "ID", "BolumAd");
            ViewBag.KatID = new SelectList(Floor.GetList(0), "ID", "KatAd");
            return View("ManualMovement", new Yer());
        }
        /// <summary>
        /// elle yer değiştirmeyi kaydet
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult ManualMovement(Yer tbl)
        {
            if (CheckPerm(Perms.Stok, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (tbl.Miktar < 1) return Json(new Result(false, "Miktar hatalı"), JsonRequestBehavior.AllowGet);
            //burada görev oluştur
            var ilk = Yerlestirme.Detail(tbl.ID);
            if (ilk.Miktar < tbl.Miktar || tbl.Miktar < 1)
                return Json(new Result(false, "Miktar hatalı yazılmış"), JsonRequestBehavior.AllowGet);
            int today = fn.ToOADate();
            int time = fn.ToOATime();
            try
            {
                //ilk önce görevler ve irsaliye kaydedilir
                string GorevÇıkNo = db.SettingsGorevNo(today, ilk.DepoID).FirstOrDefault();
                string GorevGirNo = db.SettingsGorevNo(today, ilk.DepoID).FirstOrDefault();
                var cevapGir = db.InsertIrsaliye(db.GetSirketDBs().FirstOrDefault(), ilk.DepoID, GorevGirNo, GorevGirNo, today, "Yer Değiştir", true, ComboItems.TransferGiriş.ToInt32(), vUser.UserName, today, time, "Yer Değiştir", "", 0, "", "").FirstOrDefault();
                var cevapÇık = db.InsertIrsaliye(db.GetSirketDBs().FirstOrDefault(), ilk.DepoID, GorevÇıkNo, GorevGirNo, today, "Yer Değiştir", true, ComboItems.TransferÇıkış.ToInt32(), vUser.UserName, today, time, "Yer Değiştir", "", 0, cevapGir.GorevID.Value.ToString(), "").FirstOrDefault();
                //GorevYer tablosu - çıkış
                var cevap = TaskYer.Operation(new GorevYer() { GorevID = cevapÇık.GorevID.Value, YerID = ilk.ID, MalKodu = ilk.MalKodu, Birim = ilk.Birim, Miktar = tbl.Miktar, GC = true });
                //giriş
                var yertmp = Yerlestirme.Detail(tbl.KatID, tbl.MalKodu, tbl.Birim);
                if (yertmp == null)
                {
                    cevap = Yerlestirme.Insert(new Yer() { KatID = tbl.KatID, MalKodu = ilk.MalKodu, Birim = ilk.Birim, Miktar = 0 }, 0, vUser.Id);
                    yertmp = new Yer() { ID = cevap.Id };
                }
                cevap = TaskYer.Operation(new GorevYer() { GorevID = cevapGir.GorevID.Value, YerID = yertmp.ID, MalKodu = ilk.MalKodu, Birim = ilk.Birim, Miktar = tbl.Miktar, GC = false });
                //irs detay
                cevap = IrsaliyeDetay.Operation(new IRS_Detay() { IrsaliyeID = cevapGir.IrsaliyeID.Value, MalKodu = ilk.MalKodu, Miktar = tbl.Miktar, Birim = ilk.Birim, KynkSiparisID = tbl.KatID });
                //return
                return Json(new Result(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Stock/ManualMovementSave");
                return Json(new Result(false, "Kayıt hatası"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// elle yerleştirmede yeni yeri belirle
        /// </summary>
        [HttpPost]
        public PartialViewResult ManualNewPlace(int Id)
        {
            var tbl = Yerlestirme.Detail(Id);
            ViewBag.RafID = new SelectList(Shelf.GetListByDepo(tbl.DepoID.Value), "ID", "RafAd");
            ViewBag.BolumID = new SelectList(Section.GetList(0), "ID", "BolumAd");
            ViewBag.KatID = new SelectList(Floor.GetList(0), "ID", "KatAd");
            ViewBag.Miktar = tbl.Miktar;
            ViewBag.Id = Id;
            return PartialView("ManualNewPlace", new Yer());
        }
        /// <summary>
        /// elle yerleştirmede yeni yeri belirle
        /// </summary>
        [HttpPost]
        public PartialViewResult Details()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "") return null;
            var ID = id.ToInt32();
            //listeyi getir
            var list = db.IRS.Where(m => m.ID == ID).FirstOrDefault();
            if (list == null)
                return null;
            return PartialView("Details", list);
        }
        /// <summary>
        /// depoya bir ürünü listeler
        /// </summary>
        [HttpPost]
        public PartialViewResult Details2(int DepoID, string MalKodu)
        {
            return PartialView("Details2", Yerlestirme.GetMalListFromDepo(DepoID, MalKodu));
        }
        /// <summary>
        /// rezerv bilgileri
        /// </summary>
        [HttpPost]
        public PartialViewResult GetRezerv2(string MalKodu, int Depo, string Birim)
        {
            var list = db.GetStockRezerv2(Depo, MalKodu, Birim).ToList();
            if (list == null)
                return null;
            return PartialView("Rezervler", list);
        }
        /// <summary>
        /// stok karşılaştırma sayfası
        /// </summary>
        public ActionResult Comparison()
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            return View("Comparison");
        }
        /// <summary>
        /// stok karşılaştırma listesi
        /// </summary>
        [HttpPost]
        public PartialViewResult ComparisonList(string Id)
        {
            var ids = Id.Split('#');
            string BasMalKodu = ids[0];
            string BitMalKodu = ids[1];
            var depoID = ids[2].ToInt32();
            var depoKodu = Store.Detail(depoID).DepoKodu;
            //generate sql
            string sql = "";
            var listsirk = db.GetSirketDBs();
            string Sart = "";

            foreach (var item in listsirk)
            {
                if (item == "33")
                    Sart = " (MalKodu BETWEEN 'a' AND 'z') AND ";
                else if (item == "71")
                    Sart = " (MalKodu BETWEEN '1' AND '9') AND ";
                else
                    Sart = "";

                if (sql != "") sql += " UNION ";
                sql += String.Format(@"SELECT FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.Birim1 AS Birim, FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}') as Stok
										FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK) WHERE {4} (MalKodu BETWEEN '{2}' AND '{3}') ", item, depoKodu, BasMalKodu, BitMalKodu, Sart);
            }
            sql = string.Format(@"SELECT MalKodu, MalAdi, Birim, SUM(Stok) AS GunesStok, BIRIKIM.wms.fnGetStock('{0}', t1.MalKodu, t1.Birim, 0) AS WmsStok 
									FROM (" + sql + ") AS t1 GROUP BY MalKodu, Birim", depoKodu);
            sql = "SELECT * FROM ( " + sql + " ) AS t2 WHERE t2.GunesStok<>t2.WmsStok ORDER BY MalKodu";
            //return
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            return PartialView("ComparisonList", list);
        }
    }
}