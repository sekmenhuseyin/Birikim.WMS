﻿using System;
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
        public PartialViewResult List(int DepoID, int RafID = 0, int BolumID = 0, int KatID = 0, bool Elle = false)
        {
            try
            {
                ViewBag.Manual = Elle;
                return PartialView("List", Yerlestirme.GetList(DepoID, RafID, BolumID, KatID));
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
            var list = db.Database.SqlQuery<frmStokList>(string.Format("SELECT wms.fnGetRezervStock(wms.Depo.DepoKodu,wms.Yer.MalKodu,wms.Yer.Birim) AS WmsRezerv, wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, wms.Yer.Birim, SUM(wms.Yer.Miktar) AS Miktar FROM wms.Yer INNER JOIN wms.Depo ON wms.Yer.DepoID = wms.Depo.ID WHERE (wms.Yer.MalKodu = '{0}') GROUP BY wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, wms.Yer.Birim, wms.fnGetRezervStock(wms.Depo.DepoKodu,wms.Yer.MalKodu,wms.Yer.Birim)", Id)).ToList();
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
        public PartialViewResult CableList(int DepoID, int RafID = 0, int BolumID = 0, int KatID = 0)
        {
            try
            {
                return PartialView("CableList", Yerlestirme.GetListCable(DepoID, RafID, BolumID, KatID));
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
        public PartialViewResult HistoryList(int DepoID, string MalKodu)
        {
            var sql = string.Format(@"SELECT ISNULL(wms.fnGetStockByID({1}, '{2}', ''), 0) AS WmsStok, ISNULL(wms.fnGetRezervStock({1}, '{2}', ''), 0) AS RezervStok, ISNULL(FINSAT6{0}.wms.getStockByDepo('{2}', DepoKodu), 0) AS GunesStok
                                        FROM wms.Depo
                                        WHERE (ID = 1)", vUser.SirketKodu, DepoID, MalKodu);
            var sonuc = db.Database.SqlQuery<frmStokDetay>(sql).FirstOrDefault();
            // return
            ViewBag.WmsStok = sonuc.WmsStok;
            ViewBag.WmsRezerv = sonuc.WmsRezerv;
            ViewBag.GunesStok = sonuc.GunesStok;
            var list = db.Yer_Log.Where(m => m.MalKodu == MalKodu && m.DepoID == DepoID).OrderBy(m => m.KayitTarihi).ThenBy(m => m.KayitSaati).ThenBy(m => m.ID).ToList();
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
            // yerleştirme kaydı yapılır
            if (GC == false)
            {
                if (tbl.MakaraNo == "" || tbl.MakaraNo == null)
                {
                    var kkablo = db.Database.SqlQuery<string>(string.Format("SELECT Kod1 FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = '{1}')", vUser.SirketKodu, tbl.MalKodu)).FirstOrDefault();
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
                        Yerlestirme.Insert(tmp2, vUser.Id, "Stok Elle Ekle");
                    }
                    else
                    {
                        tmp2.Miktar += tbl.Miktar;
                        Yerlestirme.Update(tmp2, vUser.Id, "Stok Elle Ekle", tbl.Miktar, false);
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
                        Yerlestirme.Insert(tmp2, vUser.Id, "Stok Elle Ekle");
                    }
                    else
                    {
                        return Json(new Result(false, "Bu makara no kullanılmaktadır"), JsonRequestBehavior.AllowGet);
                    }
                }

                // add to mysql
                if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
                {
                    var listedb = db.GetSirketDBs().ToList();
                    var sql = "";
                    foreach (var item in listedb)
                    {
                        if (sql != "") sql += " UNION ";
                        sql += string.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4 as Marka, FINSAT6{0}.FINSAT6{0}.STK.Nesne2 as Cins, FINSAT6{0}.FINSAT6{0}.STK.Kod15 as Kesit " +
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
                                    // sid bul
                                    var sid = dbx.indices.Where(m => m.cins == stks.Cins && m.kesit == stks.Kesit).FirstOrDefault();
                                    if (sid == null)
                                    {
                                        sid = new index() { cins = stks.Cins, kesit = stks.Kesit, agirlik = 0 };
                                        dbx.indices.Add(sid);
                                        dbx.SaveChanges();
                                    }

                                    // stoğa kaydet
                                    var tbls = new stok()
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
                                // return Json(new Result(false, "Kablo kaydı hariç her şey tamamlandı!"), JsonRequestBehavior.AllowGet);
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
                    Yerlestirme.Update(tmp2, vUser.Id, "Stok Elle Çıkartma", tbl.Miktar, true);
                }
                else
                {
                    var tmp2 = db.Yers.Where(m => m.KatID == tbl.KatID && m.MalKodu == tbl.MalKodu && m.Birim == tbl.Birim && m.MakaraNo == tbl.MakaraNo).FirstOrDefault();
                    if (tmp2 == null)
                        return Json(new Result(false, "Seçili yerde bu ürün bulunamadı."), JsonRequestBehavior.AllowGet);
                    if (tmp2.Miktar < tbl.Miktar)
                        return Json(new Result(false, "Seçili yerde çıkış yapılmak istenilen sayıda ürün yok"), JsonRequestBehavior.AllowGet);
                    tmp2.Miktar -= tbl.Miktar;
                    Yerlestirme.Update(tmp2, vUser.Id, "Stok Elle Çıkartma", tbl.Miktar, true);
                }
            }

            // return
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
            // burada görev oluştur
            var ilk = Yerlestirme.Detail(tbl.ID);
            if (ilk.Miktar < tbl.Miktar || tbl.Miktar < 1)
                return Json(new Result(false, "Miktar hatalı yazılmış"), JsonRequestBehavior.AllowGet);
            var today = fn.ToOADate();
            var time = fn.ToOATime();
            try
            {
                // ilk önce görevler ve irsaliye kaydedilir
                var GorevÇıkNo = db.SettingsGorevNo(today, ilk.DepoID).FirstOrDefault();
                var GorevGirNo = db.SettingsGorevNo(today, ilk.DepoID).FirstOrDefault();
                var cevapGir = db.InsertIrsaliye(vUser.SirketKodu, ilk.DepoID, GorevGirNo, GorevGirNo, today, "Yer Değiştir", true, ComboItems.TransferGiriş.ToInt32(), vUser.UserName, today, time, "Yer Değiştir", "", 0, "", "").FirstOrDefault();
                var cevapÇık = db.InsertIrsaliye(vUser.SirketKodu, ilk.DepoID, GorevÇıkNo, GorevGirNo, today, "Yer Değiştir", true, ComboItems.TransferÇıkış.ToInt32(), vUser.UserName, today, time, "Yer Değiştir", "", 0, cevapGir.GorevID.Value.ToString(), "").FirstOrDefault();
                // GorevYer tablosu - çıkış
                var cevap = TaskYer.Operation(new GorevYer() { GorevID = cevapÇık.GorevID.Value, YerID = ilk.ID, MalKodu = ilk.MalKodu, Birim = ilk.Birim, Miktar = tbl.Miktar, GC = true });
                // giriş
                var yertmp = Yerlestirme.Detail(tbl.KatID, ilk.MalKodu, ilk.Birim);
                if (yertmp == null)
                {
                    cevap = Yerlestirme.Insert(new Yer() { KatID = tbl.KatID, MalKodu = ilk.MalKodu, Birim = ilk.Birim, Miktar = 0 }, vUser.Id, "Yer Değiştir");
                    yertmp = new Yer() { ID = cevap.Id };
                }

                cevap = TaskYer.Operation(new GorevYer() { GorevID = cevapGir.GorevID.Value, YerID = yertmp.ID, MalKodu = ilk.MalKodu, Birim = ilk.Birim, Miktar = tbl.Miktar, GC = false });
                // irs detay
                cevap = IrsaliyeDetay.Operation(new IRS_Detay() { IrsaliyeID = cevapGir.IrsaliyeID.Value, MalKodu = ilk.MalKodu, Miktar = tbl.Miktar, Birim = ilk.Birim, KynkSiparisID = tbl.KatID });
                // return
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
            // listeyi getir
            var list = db.IRS.Where(m => m.ID == ID).FirstOrDefault();
            if (list == null) return null;
            return PartialView("Details", list);
        }
        /// <summary>
        /// depoya bir ürünü listeler
        /// </summary>
        [HttpPost]
        public PartialViewResult Details2(int DepoID, string MalKodu)
        {
            return PartialView("Details2", Yerlestirme.GetList(DepoID, MalKodu));
        }
        /// <summary>
        /// rezerv bilgileri
        /// </summary>
        [HttpPost]
        public PartialViewResult GetRezerv2(string MalKodu, int Depo, string Birim)
        {
            var list = db.GetStockRezerv2(Depo, MalKodu, Birim).ToList();
            if (list == null) return null;
            return PartialView("Rezervler", list);
        }
        /// <summary>
        /// stok karşılaştırma sayfası
        /// </summary>
        public ActionResult Comparison()
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "DepoKodu", "DepoAd");
            return View("Comparison");
        }
        /// <summary>
        /// stok karşılaştırma listesi
        /// </summary>
        [HttpPost]
        public PartialViewResult ComparisonList(string DepoID, string BasMalKodu, string BitMalKodu)
        {
            // generate sql
            var sql = "";
            if (BasMalKodu != "" && BitMalKodu != "")
                sql = string.Format("WHERE (MalKodu >= '{0}%') AND (MalKodu <= '{1}%')", BasMalKodu, BitMalKodu);
            else if (BasMalKodu != "")
                sql = string.Format("WHERE (MalKodu >= '{0}%')", BasMalKodu);
            else if (BitMalKodu != "")
                sql = string.Format("WHERE (MalKodu <= '{0}%')", BitMalKodu);
            sql = string.Format(@"
                        SELECT * FROM (
                            SELECT FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.Birim1 AS Birim, FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}') as GunesStok, BIRIKIM.wms.fnGetStock('{1}', STK.MalKodu, '', 0) AS WmsStok
						    FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK) {2}) t
                        WHERE (GunesStok <> WmsStok)", vUser.SirketKodu, DepoID, sql);
            // return
            var list = db.Database.SqlQuery<frmStokComparison>(sql).ToList();
            return PartialView("ComparisonList", list);
        }
    }
}