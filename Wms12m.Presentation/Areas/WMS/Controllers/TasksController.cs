using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class TasksController : RootController
    {

        /// <summary>
        /// görev anasayfa
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Index");
        }
        /// <summary>
        /// listeyi gösterir
        /// </summary>
        public PartialViewResult List(int Id)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            var list = Task.GetList(Id, vUser.DepoId);
            return PartialView("List", list);
        }
        /// <summary>
        /// görev ayrıntıları
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            var list = new frmTaskDetails()
            {
                irsdetay = db.GetIrsDetayfromGorev(ID).ToList(),
                grv = Task.Detail(ID)
            };
            return PartialView("Details", list);
        }
        /// <summary>
        /// görev düzenle
        /// </summary>
        public PartialViewResult GorevDetailPartial(int id)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            var list = Task.Detail(id);
            ViewBag.GorevTipiID = new SelectList(ComboSub.GetList(Combos.GorevTipleri.ToInt32()), "ID", "Name", list.GorevTipiID);
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name", list.DurumID);
            return PartialView("_GorevDetailPartial", list);
        }
        /// <summary>
        /// görev güncelle
        /// </summary>
        public JsonResult Update(frmGorev tbl)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            //update
            var _Result = Task.Update(tbl);
            //get list
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// görevli ata
        /// </summary>
        [HttpPost]
        public PartialViewResult GorevliAta()
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            Int32 ID = Convert.ToInt32(id);
            var list = Task.Detail(ID);
            ViewBag.Gorevli = new SelectList(Persons.GetList(), "Kod", "AdSoyad", list.Gorevli);
            return PartialView("GorevliAta", list);
        }
        /// <summary>
        /// görevliyi kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult GorevliKaydet(frmGorevli tbl)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Redirect("/");
            Task tmpTable = new Task();
            Result _Result = tmpTable.UpdateGorevli(tbl);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// kontrollü sayin sayfası
        /// </summary>
        public ActionResult Count()
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Count");
        }
        /// <summary>
        /// listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult CountList(string Id)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            //id'ye göre liste döner
            var list = Task.GetList(ComboItems.KontrolSayım.ToInt32(), Id.ToInt32());
            return PartialView("CountList", list);
        }
        /// <summary>
        /// yeni kontrollü sayım görevi ekle
        /// </summary>
        [HttpPost]
        public PartialViewResult New()
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            ViewBag.DepoID = new SelectList(Store.GetList(), "ID", "DepoAd");
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            return PartialView("CountNew");
        }
        /// <summary>
        /// yeni görevi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult SaveNew(int DepoID, string SirketID)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result;
            //kontrol
            int açık = ComboItems.Açık.ToInt32();
            int sayim = ComboItems.KontrolSayım.ToInt32();
            var grv = db.Gorevs.Where(m => m.DepoID == DepoID && m.IR.SirketKod == SirketID && m.GorevTipiID == sayim && m.DurumID == açık).FirstOrDefault();
            if (grv == null)
            {
                int tarih = fn.ToOADate();
                var grvNo = db.SettingsGorevNo(tarih, DepoID).FirstOrDefault();
                var depo = Store.Detail(DepoID).DepoKodu;
                var cevap = db.InsertIrsaliye(SirketID, DepoID, grvNo, grvNo, tarih, SirketID + "-" + depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "").FirstOrDefault();
                grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                grv.DurumID = açık;
                db.SaveChanges();
                LogActions("WMS", "Tasks", "SaveNew", ComboItems.alEkle, grv.ID, "Sayım: " + grv.IR.HesapKodu);
                _Result = new Result(true);
            }
            else
            {
                if (grv.IR.SirketKod == SirketID)
                    _Result = new Result(false, "Bu görev zaten var");
                else
                {
                    int tarih = fn.ToOADate();
                    var grvNo = db.SettingsGorevNo(tarih, DepoID).FirstOrDefault();
                    var depo = Store.Detail(DepoID).DepoKodu;
                    var cevap = db.InsertIrsaliye(SirketID, DepoID, grvNo, grvNo, tarih, SirketID + "-" + depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "").FirstOrDefault();
                    grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                    grv.DurumID = açık;
                    db.SaveChanges();
                    LogActions("WMS", "Tasks", "SaveNew", ComboItems.alEkle, grv.ID, "Sayım: " + grv.IR.HesapKodu);
                    _Result = new Result(true);
                }
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// kontrollü sayima ait fark sayfası
        /// </summary>
        public PartialViewResult CountFark()
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            //split id
            string[] tmp = id.ToString().Split('-');
            //get gorev details
            int GorevID = tmp[1].ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            //create sql
            string sql = "";
            if (tmp[0] == "1")//sadece fark liste
                sql = " WHERE (WmsStok <> Miktar)";
            //eksik liste için farklı sql
            if (tmp[0] == "2")
                sql = string.Format(@"SELECT FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.Birim1 as Birim, CAST(0 as DECIMAL) as Miktar, ISNULL(FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}'), 0) AS GunesStok, 
                                                ISNULL([wms].fnGetStockByID({3}, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.Birim1), 0) AS WmsStok
                                    FROM FINSAT6{0}.FINSAT6{0}.STK INNER JOIN FINSAT6{0}.FINSAT6{0}.DST ON FINSAT6{0}.FINSAT6{0}.STK.MalKodu = FINSAT6{0}.FINSAT6{0}.DST.MalKodu
                                    WHERE        (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}') <> ISNULL([wms].fnGetStockByID({3}, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.Birim1), 0))
                                                AND FINSAT6{0}.FINSAT6{0}.STK.MalKodu NOT IN (SELECT MalKodu FROM wms.GorevYer WHERE (GorevID = {2}))

                                UNION

                                SELECT        MalKodu, '' AS MalAdi, Birim, 0 AS Miktar, ISNULL(FINSAT699.wms.getStockByDepo(MalKodu, '{1}'), 0) AS GunesStok, ISNULL(wms.fnGetStockByID({3}, MalKodu, Birim), 0) AS WmsStok
                                FROM            wms.Yer
                                GROUP BY MalKodu, Birim, ISNULL(FINSAT6{0}.wms.getStockByDepo(MalKodu, '{1}'), 0), ISNULL(wms.fnGetStockByID({3}, MalKodu, Birim), 0)
                                HAVING        (NOT (MalKodu IN (SELECT MalKodu FROM wms.GorevYer WHERE (GorevID = {2})))) 
                                            AND (ISNULL(wms.fnGetStockByID({3}, MalKodu, Birim), 0) > 0)
                                    ORDER BY FINSAT6{0}.FINSAT6{0}.STK.MalKodu", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID, mGorev.DepoID);
            else
                sql = string.Format("SELECT MalKodu, ISNULL(MalAdi, '') as MalAdi, Birim, ISNULL(Miktar, 0) as Miktar, ISNULL(WmsStok, 0) as WmsStok, ISNULL(GunesStok, 0) as GunesStok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                    "(SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi from FINSAT6{0}.FINSAT6{0}.STK WHERE FINSAT6{0}.FINSAT6{0}.STK.MalKodu = wms.GorevYer.MalKodu) as MalAdi, " +
                                                    "FINSAT6{0}.wms.getStockByDepo(wms.GorevYer.MalKodu, '{1}') as GunesStok, [wms].fnGetStockByID(wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim) as WmsStok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t{3} ORDER BY MalKodu", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID, sql);
            //run
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            //return
            ViewBag.ID = id;
            return PartialView("CountFark", list);
        }
        /// <summary>
        /// sayım fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountCreate(int GorevID, bool Tip)
        {
            //kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == true)
                return Json(new Result(false, "Sayım fişi daha önce oluşturulmuş!"), JsonRequestBehavior.AllowGet);
            //seri kontrol
            var details = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
            if (details == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri.Value < 1 || details.SayimSeri.Value > 199)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            //variables
            int tarih = fn.ToOADate();
            short sirano = 0;
            List<STI> stiList = new List<STI>();
            //loop malkods
            string sql = string.Format("SELECT MalKodu, MalAdi, Birim, Miktar, GunesStok, WmsStok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                    "(SELECT FINSAT6{0}.FINSAT6{0}.STK.MalAdi from FINSAT6{0}.FINSAT6{0}.STK WHERE FINSAT6{0}.FINSAT6{0}.STK.MalKodu = wms.GorevYer.MalKodu) as MalAdi, " +
                                                    "FINSAT6{0}.wms.getStockByDepo(wms.GorevYer.MalKodu, '{1}') as GunesStok, [wms].fnGetStockByID(wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim) as WmsStok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            foreach (var item in list)
            {
                var sti = new STI();
                sti.DefaultValueSet();
                if (item.Miktar > item.GunesStok)//olması gerekenden fazlaysa giriş yapılacak
                    sti.IslemTur = 0;
                else//eğer olması gerekenden az varsa çıkış yapılacak
                    sti.IslemTur = 1;
                sti.Tarih = tarih;
                sti.KynkEvrakTip = 95;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                sti.SiraNo = sirano;
                sti.IslemTip = 18;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                sti.MalKodu = item.MalKodu;
                sti.Miktar = item.Miktar;
                sti.Miktar2 = item.GunesStok;
                sti.Birim = item.Birim;
                sti.BirimMiktar = item.Miktar;
                sti.Depo = mGorev.Depo.DepoKodu;
                sti.VadeTarih = tarih;
                sti.EvrakTarih = tarih;
                sti.AnaEvrakTip = 95;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                stiList.Add(sti);
                sirano++;
            }
            //eğer eksik listesi de atılacaksa sayım fişine biraz daha ekle
            if (Tip == true)
            {
                sql = string.Format(@"SELECT FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.Birim1 as Birim, CAST(0 as DECIMAL) as Miktar, FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}') AS GunesStok, 
                                                ISNULL([wms].fnGetStockByID({3}, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.Birim1), 0) AS WmsStok
                                    FROM FINSAT6{0}.FINSAT6{0}.STK INNER JOIN FINSAT6{0}.FINSAT6{0}.DST ON FINSAT6{0}.FINSAT6{0}.STK.MalKodu = FINSAT6{0}.FINSAT6{0}.DST.MalKodu
                                    WHERE        (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.wms.getStockByDepo(FINSAT6{0}.FINSAT6{0}.STK.MalKodu, '{1}') <> ISNULL([wms].fnGetStockByID({3}, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.STK.Birim1), 0)) AND 
                                                FINSAT6{0}.FINSAT6{0}.STK.MalKodu NOT IN (SELECT MalKodu FROM wms.GorevYer WHERE (GorevID = {2}))", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID, mGorev.DepoID);
                list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
                foreach (var item in list)
                {
                    var sti = new STI();
                    sti.DefaultValueSet();
                    if (item.Miktar > item.GunesStok)//olması gerekenden fazlaysa giriş yapılacak
                        sti.IslemTur = 0;
                    else//eğer olması gerekenden az varsa çıkış yapılacak
                        sti.IslemTur = 1;
                    sti.Tarih = tarih;
                    sti.KynkEvrakTip = 95;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                    sti.SiraNo = sirano;
                    sti.IslemTip = 18;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                    sti.MalKodu = item.MalKodu;
                    sti.Miktar = item.Miktar;
                    sti.Miktar2 = item.GunesStok;
                    sti.Birim = item.Birim;
                    sti.BirimMiktar = item.Miktar;
                    sti.Depo = mGorev.Depo.DepoKodu;
                    sti.VadeTarih = tarih;
                    sti.EvrakTarih = tarih;
                    sti.AnaEvrakTip = 95;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                    stiList.Add(sti);
                    sirano++;
                }
            }
            //finsat tanımlama
            int EvrakSeriNo = 7100 + details.SayimSeri.Value - 1;
            Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            var sonuc = finsat.SayımVeFarkFişi(stiList, EvrakSeriNo, true, vUser.UserName);
            if (sonuc.Status == true)
            {
                mGorev.IR.EvrakNo = sonuc.Message;
                mGorev.IR.Onay = true;
                mGorev.IR.ValorGun = (short)(Tip == true ? 1 : 0);
                db.SaveChanges();
                sonuc.Message = "İşlem tamlandı!";
                LogActions("WMS", "Tasks", "CountCreate", ComboItems.alEkle, mGorev.ID, "Sayım Fişi: " + mGorev.IR.EvrakNo);
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayım fark fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountCreateDiff(int GorevID)
        {
            //kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.LinkEvrakNo != null)
                return Json(new Result(false, "Fark fişi daha önce oluşturulmuş!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == false)
                return Json(new Result(false, "Sayım fişi daha oluşturulmamış!"), JsonRequestBehavior.AllowGet);
            //seri kontrol
            var details = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
            if (details == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri.Value < 1 || details.SayimSeri.Value > 199)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            //variables
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            short sirano = 0;
            List<STI> stiList = new List<STI>();
            //loop malkods
            string sql = string.Format("SELECT IslemTur, MalKodu, Miktar, Miktar2, Birim, Depo FROM FINSAT6{0}.FINSAT6{0}.STI " +
                                        "WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 95) AND (IslemTip = 18)", mGorev.IR.SirketKod, mGorev.IR.EvrakNo);
            var list = db.Database.SqlQuery<frmGorevSayimFisi>(sql).ToList();
            foreach (var item in list)
            {
                if (item.Miktar != item.Miktar2)
                {
                    var sti = new STI();
                    sti.DefaultValueSet();
                    sti.IslemTur = item.IslemTur;
                    sti.Miktar = Math.Abs(item.Miktar - item.Miktar2);
                    sti.Tarih = tarih;
                    sti.KynkEvrakTip = 100;//"Sayım Farkı Fişi" from finsat.COMBOITEM_NAME
                    sti.SiraNo = sirano;
                    sti.IslemTip = 20;//"Sayım Farkı" from finsat.COMBOITEM_NAME
                    sti.MalKodu = item.MalKodu;
                    sti.Birim = item.Birim;
                    sti.BirimMiktar = sti.Miktar;
                    sti.Miktar2 = item.Miktar;
                    sti.Depo = mGorev.Depo.DepoKodu;
                    sti.VadeTarih = tarih;
                    sti.EvrakTarih = tarih;
                    sti.AnaEvrakTip = 100;//"Sayım Farkı Fişi" from finsat.COMBOITEM_NAME
                    sti.KaynakIrsEvrakNo = mGorev.IR.EvrakNo;
                    stiList.Add(sti);
                    sirano++;
                }
            }
            //finsat tanımlama
            int EvrakSeriNo = 7500 + details.SayimSeri.Value - 1;
            Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            var sonuc = finsat.SayımVeFarkFişi(stiList, EvrakSeriNo, true, vUser.UserName);
            if (sonuc.Status == true)
            {
                mGorev.IR.LinkEvrakNo = sonuc.Message;
                LogActions("WMS", "Tasks", "CountCreateDiff", ComboItems.alEkle, mGorev.ID, "Sayım Fark Fişi: " + mGorev.IR.LinkEvrakNo);
                db.SaveChanges();
                //finsat dst & stk update
                sql = string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STI SET KaynakIrsEvrakNo='{1}' WHERE EvrakNo = '{2}' AND KynkEvrakTip = 95;", mGorev.IR.SirketKod, sonuc.Message, mGorev.IR.EvrakNo);
                foreach (var item in list)
                {
                    if (item.IslemTur == 0)//giriş
                    {
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                            "SET GirMiktar = GirMiktar + {3}, SonGirTarih = {5}, SonSayimTarih = {5}, SonSayimFarki = {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}') AND(Depo = '{2}');", mGorev.IR.SirketKod, item.MalKodu, mGorev.Depo.DepoKodu, (item.Miktar - item.Miktar2).ToDot(), vUser.UserName, tarih, saat);
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                            "SET TahminiStok = TahminiStok + {2}, GirMiktar = GirMiktar + {2}, GirTarih = {5}, SonSayimTarih = {5}, SonSayimSonuc = {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}');", mGorev.IR.SirketKod, item.MalKodu, (item.Miktar - item.Miktar2).ToDot(), item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                    }
                    else//çıkış
                    {
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                            "SET CikMiktar = CikMiktar + {3}, SonCikTarih = {5}, SonSayimTarih = {5}, SonSayimFarki = -{3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}') AND(Depo = '{2}');", mGorev.IR.SirketKod, item.MalKodu, mGorev.Depo.DepoKodu, (item.Miktar2 - item.Miktar).ToDot(), vUser.UserName, tarih, saat);
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                            "SET TahminiStok = TahminiStok - {2}, CikMiktar = CikMiktar + {2}, CikTarih = {5}, SonSayimTarih = {5}, SonSayimSonuc = {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}');", mGorev.IR.SirketKod, item.MalKodu, (item.Miktar2 - item.Miktar).ToDot(), item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                    }
                }
                db.Database.ExecuteSqlCommand(sql);
                //son olarak bizim stoka kaydet
                //get list
                if (mGorev.IR.ValorGun == 1)
                    sql = string.Format(@"SELECT        wms.Yer.KatID, wms.Yer.MalKodu, wms.Yer.Birim, wms.Yer.Miktar AS Stok, 
                                                        ISNULL((SELECT Sum(Miktar) FROM wms.GorevYer WHERE (GorevID = {0}) AND(MalKodu = wms.Yer.MalKodu) AND (YerID = wms.Yer.ID)), 0) AS Miktar
                                        FROM wms.Yer INNER JOIN wms.Gorev ON wms.Yer.DepoID = wms.Gorev.DepoID
                                        WHERE (wms.Gorev.ID = {0})", GorevID);
                else
                     sql = string.Format(@"SELECT        wms.Yer.KatID, wms.GorevYer.MalKodu, wms.GorevYer.Birim, wms.Yer.Miktar AS Stok, wms.GorevYer.Miktar
                                        FROM            wms.GorevYer INNER JOIN
                                                                    wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID 
                                        WHERE (wms.GorevYer.GorevID  = {0})", GorevID);
               var list2 = db.Database.SqlQuery<frmSiparisToplama>(sql).ToList();
                //loop list
                foreach (var item in list2)
                {
                    //yerleştirme kaydı yapılır
                    var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, item.Birim);
                    if (tmp2 == null)
                    {
                        tmp2 = new Yer()
                        {
                            KatID = item.KatID,
                            MalKodu = item.MalKodu,
                            Birim = item.Birim,
                            Miktar = item.Miktar
                        };
                        Yerlestirme.Insert(tmp2, mGorev.IrsaliyeID.Value, vUser.Id);
                    }
                    else
                    {
                        if (item.Miktar > item.Stok)//giriş
                        {
                            tmp2.Miktar = item.Miktar;
                            Yerlestirme.Update(tmp2, mGorev.IrsaliyeID.Value, vUser.Id, false, item.Miktar - item.Stok);
                        }
                        else if (item.Miktar < item.Stok)//çıkış
                        {
                            tmp2.Miktar = item.Miktar;
                            Yerlestirme.Update(tmp2, mGorev.IrsaliyeID.Value, vUser.Id, true, item.Stok - item.Miktar);
                        }
                    }
                }
                sonuc.Message = "İşlem tamlandı!";
            }
            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayım fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult Finish(int GorevID)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Açık.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            var tbl = mGorev.GorevUsers.Where(m => m.BitisTarihi == null).FirstOrDefault();
            if (tbl != null)
                return Json(new Result(false, "Bu görev daha bitmemiş!"), JsonRequestBehavior.AllowGet);
            mGorev.DurumID = ComboItems.Tamamlanan.ToInt32();
            mGorev.BitisTarihi = fn.ToOADate();
            mGorev.BitisSaati = fn.ToOATime();
            db.SaveChanges();
            return Json(new Result(true, mGorev.ID, "İşlem tamlandı!"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayımı tekrar aktif et
        /// </summary>
        [HttpPost]
        public JsonResult ReActivate(int GorevID)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            mGorev.DurumID = ComboItems.Açık.ToInt32();
            mGorev.BitisTarihi = null;
            mGorev.BitisSaati = null;
            foreach (var item in mGorev.GorevUsers)
            {
                item.BitisTarihi = null;
            }
            db.SaveChanges();
            return Json(new Result(true, mGorev.ID, "İşlem tamlandı!"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// paketleme sonrası, sevkiyat öncesi barkod hazırlama
        /// </summary>
        public PartialViewResult Barcode()
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            var ID = id.ToInt32();
            var tbl = db.GorevPaketlers.Where(m => m.GorevID == ID).FirstOrDefault();
            if (tbl == null)
                return PartialView("Barcode", new GorevPaketler());
            ViewBag.PaketTipiID = new SelectList(ComboSub.GetList(Combos.PaketTipi.ToInt32()), "ID", "Name", tbl.PaketTipiID);
            return PartialView("Barcode", tbl);
        }
        /// <summary>
        /// paketleme sonrası, sevkiyat öncesi barkod yazdırma
        /// </summary>
        public ViewResult PrintBarcode(GorevPaketler tbl)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return null;
            var tblx = db.GorevPaketlers.Where(m => m.GorevID == tbl.GorevID).FirstOrDefault();
            tblx.SevkiyatNo = tbl.SevkiyatNo;
            tblx.PaketNo = tbl.PaketNo;
            tblx.PaketTipiID = tbl.PaketTipiID;
            tblx.Adet = tbl.Adet;
            tblx.DegisTarih = fn.ToOADate();
            tbl.Degistiren = vUser.UserName;
            db.SaveChanges();
            ViewBag.Details = db.Database.SqlQuery<frmPaketBarkod>(string.Format("EXEC [FINSAT6{0}].[wms].[getBarcodeDetails] @SirketKodu = N'{0}', @DepoKodu = N'{1}', @EvrakNo = N'{2}'", tblx.Gorev.IR.SirketKod, tblx.Gorev.Depo.DepoKodu, tblx.Gorev.IR.EvrakNo)).FirstOrDefault();
            return View("BarcodePrint", tblx);
        }
        /// <summary>
        /// sayım fişi iptal
        /// </summary>
        [HttpPost]
        public JsonResult CountBack(int GorevID)
        {
            //kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == false)
                return Json(new Result(false, "Sayım fişi bulunamadı!"), JsonRequestBehavior.AllowGet);
            //variables
            string sql = string.Format("DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 95) AND (IslemTip = 18);", mGorev.IR.SirketKod, mGorev.IR.EvrakNo);
            db.Database.ExecuteSqlCommand(sql);
            mGorev.IR.EvrakNo = mGorev.GorevNo;
            mGorev.IR.Onay = false;
            db.SaveChanges();
            LogActions("WMS", "Tasks", "CountBack", ComboItems.alSil, mGorev.ID, "Sayım Fişi İptal: " + mGorev.IR.EvrakNo);
            return Json(new Result(true, mGorev.ID, "İşlem tamlandı!"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayım fark fişi iptal
        /// </summary>
        [HttpPost]
        public JsonResult CountBackDiff(int GorevID)
        {
            //kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            int durumID = ComboItems.Tamamlanan.ToInt32();
            int tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.LinkEvrakNo == null)
                return Json(new Result(false, "Fark fişi bulunamadı!"), JsonRequestBehavior.AllowGet);

            //variables
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            List<STI> stiList = new List<STI>();
            //loop malkods
            string sql = string.Format("SELECT IslemTur, MalKodu, Miktar, Miktar2, Birim, Depo FROM FINSAT6{0}.FINSAT6{0}.STI " +
                                        "WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 100) AND (IslemTip = 20)", mGorev.IR.SirketKod, mGorev.IR.LinkEvrakNo);
            var list = db.Database.SqlQuery<frmGorevSayimFisi>(sql).ToList();
            sql = "";
            foreach (var item in list)
            {
                if (item.IslemTur == 0)//giriş
                {
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                        "SET GirMiktar = GirMiktar - {3},  SonSayimFarki = {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                        "WHERE(MalKodu = '{1}') AND(Depo = '{2}');", mGorev.IR.SirketKod, item.MalKodu, mGorev.Depo.DepoKodu, (item.Miktar - item.Miktar2).ToDot(), vUser.UserName, tarih, saat);
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                        "SET TahminiStok = TahminiStok - {2}, GirMiktar = GirMiktar - {2},  Degistiren = '{3}', DegisTarih = {4}, DegisSaat = {5} " +
                        "WHERE(MalKodu = '{1}');", mGorev.IR.SirketKod, item.MalKodu, (item.Miktar - item.Miktar2).ToDot(), vUser.UserName, tarih, saat);
                }
                else//çıkış
                {
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                        "SET CikMiktar = CikMiktar - {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                        "WHERE(MalKodu = '{1}') AND(Depo = '{2}');", mGorev.IR.SirketKod, item.MalKodu, mGorev.Depo.DepoKodu, (item.Miktar2 - item.Miktar).ToDot(), vUser.UserName, tarih, saat);
                    sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                        "SET TahminiStok = TahminiStok + {2}, CikMiktar = CikMiktar - {2}, Degistiren = '{3}', DegisTarih = {4}, DegisSaat = {5} " +
                        "WHERE(MalKodu = '{1}');", mGorev.IR.SirketKod, item.MalKodu, (item.Miktar2 - item.Miktar).ToDot(), vUser.UserName, tarih, saat);
                }
            }

            sql += string.Format("DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 100) AND (IslemTip = 20);",
            mGorev.IR.SirketKod, mGorev.IR.LinkEvrakNo);
            db.Database.ExecuteSqlCommand(sql);
            LogActions("WMS", "Tasks", "CountBackDiff", ComboItems.alSil, mGorev.ID, "Sayım Fark Fişi İptal: " + mGorev.IR.LinkEvrakNo);

            var yl = db.Yer_Log.Where(a => a.IrsaliyeID == mGorev.IR.ID).ToList();
            foreach (var item in yl)
            {
                var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, item.Birim);
                if (item.GC == true)
                {
                    tmp2.Miktar += item.Miktar;
                    Yerlestirme.Update(tmp2, mGorev.IR.ID, vUser.Id, false, item.Miktar);
                }
                else
                {
                    tmp2.Miktar -= item.Miktar;
                    Yerlestirme.Update(tmp2, mGorev.IR.ID, vUser.Id, true, item.Miktar);
                }
            }
            mGorev.IR.LinkEvrakNo = null;
            db.SaveChanges();

            return Json(new Result(true, mGorev.ID, "İşlem tamlandı!"), JsonRequestBehavior.AllowGet);
        }
    }
}