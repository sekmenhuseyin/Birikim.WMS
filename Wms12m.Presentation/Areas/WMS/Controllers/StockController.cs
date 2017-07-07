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
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return null;
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
        public PartialViewResult CableList(int Id)
        {
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return null;
            using (KabloEntities dbx = new KabloEntities())
            {
                var kblDepoID = Store.Detail(Id).KabloDepoID;
                var depo = dbx.depoes.Where(m => m.id == kblDepoID).Select(m => m.depo1).FirstOrDefault();
                var list = dbx.kblstoks.Where(m => m.depo == depo).ToList();
                return PartialView("CableList", list);
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
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return null;
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
            var list = db.Yer_Log.Where(m => m.MalKodu == kod && m.DepoID == depoID).OrderBy(m => m.KayitTarihi).ThenBy(m => m.KayitSaati).ToList();
            ViewBag.Stok = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
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
                var tmp2 = Yerlestirme.Detail(tbl.KatID, tbl.MalKodu, tbl.Birim);
                if (tmp2 == null)
                {
                    tmp2 = new Yer()
                    {
                        KatID = tbl.KatID,
                        MalKodu = tbl.MalKodu,
                        Birim = tbl.Birim,
                        Miktar = tbl.Miktar
                    };
                    if (tbl.MakaraNo != "") tmp2.MakaraNo = tbl.MakaraNo;
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
                var tmp2 = Yerlestirme.Detail(tbl.KatID, tbl.MalKodu, tbl.Birim);
                if (tmp2 == null)
                    return Json(new Result(false, "Seçili yerde bu ürün bulunamadı."), JsonRequestBehavior.AllowGet);
                tmp2.Miktar -= tbl.Miktar;
                Yerlestirme.Update(tmp2, 0, vUser.Id, true, tbl.Miktar);
            }
            //add to mysql
            if (db.Settings.FirstOrDefault().KabloSiparisMySql == true)
            {
                var listedb = db.GetSirketDBs().ToList();
                string sql = "";
                foreach (var item in listedb)
                {
                    if (sql != "") sql += " UNION ";
                    sql += String.Format("SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi4, FINSAT6{0}.FINSAT6{0}.STK.Nesne2, FINSAT6{0}.FINSAT6{0}.STK.Kod15 " +
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
                                var sid = dbx.indices.Where(m => m.cins == stks.Nesne2 && m.kesit == stks.Kod15).FirstOrDefault();
                                if (sid == null)
                                {
                                    sid = new index() { cins = stks.Nesne2, kesit = stks.Kod15, agirlik = 0 };
                                    dbx.indices.Add(sid);
                                    dbx.SaveChanges();
                                }
                                //stoğa kaydet
                                stok tbls = new stok()
                                {
                                    marka = stks.MalAdi4,
                                    cins = stks.Nesne2,
                                    kesit = stks.Kod15,
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
                            return Json(new Result(false, "Kablo kaydı hariç her şey tamamlandı!"), JsonRequestBehavior.AllowGet);
                        }
                    }
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
                var cevapGir = db.InsertIrsaliye(db.GetSirketDBs().FirstOrDefault(), ilk.DepoID, GorevGirNo, GorevGirNo, today, "Yer Değiştir", true, ComboItems.TransferGiriş.ToInt32(), vUser.UserName, today, time, "Yer Değiştir", "", 0, "").FirstOrDefault();
                var cevapÇık = db.InsertIrsaliye(db.GetSirketDBs().FirstOrDefault(), ilk.DepoID, GorevÇıkNo, GorevGirNo, today, "Yer Değiştir", true, ComboItems.TransferÇıkış.ToInt32(), vUser.UserName, today, time, "Yer Değiştir", "", 0, cevapGir.GorevID.Value.ToString()).FirstOrDefault();
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
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return null;
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
            if (CheckPerm(Perms.Stok, PermTypes.Reading) == false) return null;
            //listeyi getir
            var list = Irsaliye.Detail(id.ToInt32());
            if (list == null)
                return null;
            return PartialView("Details", list);
        }
    }
}