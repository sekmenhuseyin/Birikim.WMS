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
            var list = db.GetIrsDetayfromGorev(ID);
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
        /// görev sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result();
            try
            {
                db.DeleteFromGorev(ID);
                _Result.Status = true; _Result.Id = ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "Tasks/Delete");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }
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
            string[] tmp = id.ToString().Split('-');
            string sql = "";
            if (tmp[0] != "1")//sadece fark liste
                sql = " WHERE (Stok <> Miktar)";
            int GorevID = tmp[1].ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID).FirstOrDefault();
            sql = string.Format("SELECT MalKodu, Birim, Miktar, Stok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                "ISNULL((SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                                                "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = wms.GorevYer.MalKodu) AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)),0) AS Stok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t{3}", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID, sql);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            ViewBag.ID = id;
            return PartialView("CountFark", list);
        }
        /// <summary>
        /// sayım fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountCreate(int GorevID)
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
            string sql = string.Format("SELECT MalKodu, Birim, Miktar, Stok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                "ISNULL((SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                                                "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = wms.GorevYer.MalKodu) AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)),0) AS Stok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            foreach (var item in list)
            {
                //TODO: burada hata var
                var sti = new STI();
                sti.DefaultValueSet();
                if (item.Miktar > item.Stok)//olması gerekenden fazlaysa giriş yapılacak
                    sti.IslemTur = 0;
                else//eğer olması gerekenden az varsa çıkış yapılacak
                    sti.IslemTur = 1;
                sti.Tarih = tarih;
                sti.KynkEvrakTip = 95;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                sti.SiraNo = sirano;
                sti.IslemTip = 18;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                sti.MalKodu = item.MalKodu;
                sti.Miktar = item.Miktar;
                sti.Miktar2 = item.Stok;
                sti.Birim = item.Birim;
                sti.BirimMiktar = item.Miktar;
                sti.Depo = mGorev.Depo.DepoKodu;
                sti.VadeTarih = tarih;
                sti.EvrakTarih = tarih;
                sti.AnaEvrakTip = 95;//"Sayım Sonuç Fişi" from finsat.COMBOITEM_NAME
                stiList.Add(sti);
                sirano++;
            }
            //finsat tanımlama
            int EvrakSeriNo = 7000 + details.SayimSeri.Value - 1;
            Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            var sonuc = finsat.SayımVeFarkFişi(stiList, EvrakSeriNo, true, vUser.UserName);
            if (sonuc.Status == true)
            {
                mGorev.IR.EvrakNo = sonuc.Message;
                mGorev.IR.Onay = true;
                db.SaveChanges();
                sonuc.Message = "İşlem tamlandı!";
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
            string sql = string.Format("SELECT MalKodu, Birim, Miktar, Stok FROM (" +
                                            "SELECT wms.GorevYer.MalKodu, wms.GorevYer.Birim, SUM(wms.GorevYer.Miktar) AS Miktar, " +
                                                "ISNULL((SELECT FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar AS stok " +
                                                "FROM FINSAT6{0}.FINSAT6{0}.DST INNER JOIN FINSAT6{0}.FINSAT6{0}.STK ON FINSAT6{0}.FINSAT6{0}.DST.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                                "WHERE (FINSAT6{0}.FINSAT6{0}.DST.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.DST.MalKodu = wms.GorevYer.MalKodu) AND (FINSAT6{0}.FINSAT6{0}.DST.DvrMiktar + FINSAT6{0}.FINSAT6{0}.DST.GirMiktar - FINSAT6{0}.FINSAT6{0}.DST.CikMiktar > 0)),0) AS Stok " +
                                            "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID " +
                                            "WHERE (wms.Gorev.ID = {2}) GROUP BY wms.Gorev.DepoID, wms.GorevYer.MalKodu, wms.GorevYer.Birim" +
                                        ") AS t WHERE (Stok <> Miktar)", mGorev.IR.SirketKod, mGorev.Depo.DepoKodu, GorevID);
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            foreach (var item in list)
            {
                //TODO: burada hata var
                var sti = new STI();
                sti.DefaultValueSet();
                if (item.Miktar > item.Stok)//fazla mal varsa giriş
                    sti.IslemTur = 0;
                else
                    sti.IslemTur = 1;
                sti.Miktar = Math.Abs(item.Miktar - item.Stok);
                sti.Tarih = tarih;
                sti.KynkEvrakTip = 100;//"Sayım Farkı Fişi" from finsat.COMBOITEM_NAME
                sti.SiraNo = sirano;
                sti.IslemTip = 20;//"Sayım Farkı" from finsat.COMBOITEM_NAME
                sti.MalKodu = item.MalKodu;
                sti.Birim = item.Birim;
                sti.BirimMiktar = sti.Miktar;
                sti.Miktar2 = item.Stok;
                sti.Depo = mGorev.Depo.DepoKodu;
                sti.VadeTarih = tarih;
                sti.EvrakTarih = tarih;
                sti.AnaEvrakTip = 100;//"Sayım Farkı Fişi" from finsat.COMBOITEM_NAME
                sti.KaynakIrsEvrakNo = mGorev.IR.EvrakNo;
                stiList.Add(sti);
                sirano++;
            }
            //finsat tanımlama
            int EvrakSeriNo = 2600 + details.SayimSeri.Value - 1;
            Finsat finsat = new Finsat(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, mGorev.IR.SirketKod);
            var sonuc = finsat.SayımVeFarkFişi(stiList, EvrakSeriNo, true, vUser.UserName);
            if (sonuc.Status == true)
            {
                mGorev.IR.LinkEvrakNo = sonuc.Message;
                db.SaveChanges();
                //finsat dst & stk update
                sql = string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STI SET KaynakIrsEvrakNo='{1}' WHERE EvrakNo = '{2}' AND KynkEvrakTip = 94;", mGorev.IR.SirketKod, sonuc.Message, mGorev.IR.EvrakNo);
                foreach (var item in list)
                {
                    if (item.Miktar > item.Stok)//giriş
                    {
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                            "SET GirMiktar = GirMiktar + {3}, SonGirTarih = {5}, SonSayimTarih = {5}, SonSayimFarki = {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}') AND(Depo = '{2}');", mGorev.IR.SirketKod, item.MalKodu, mGorev.Depo.DepoKodu, (item.Miktar - item.Stok).ToDot(), vUser.UserName, tarih, saat);
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                            "SET TahminiStok = TahminiStok + {2}, GirMiktar = GirMiktar + {2}, GirTarih = {5}, SonSayimTarih = {5}, SonSayimSonuc = {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}');", mGorev.IR.SirketKod, item.MalKodu, (item.Miktar - item.Stok).ToDot(), item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                    }
                    else//çıkış
                    {
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.DST " +
                            "SET CikMiktar = CikMiktar + {3}, SonCikTarih = {5}, SonSayimTarih = {5}, SonSayimFarki = -{3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}') AND(Depo = '{2}');", mGorev.IR.SirketKod, item.MalKodu, mGorev.Depo.DepoKodu, (item.Stok - item.Miktar).ToDot(), vUser.UserName, tarih, saat);
                        sql += string.Format("UPDATE FINSAT6{0}.FINSAT6{0}.STK " +
                            "SET TahminiStok = TahminiStok - {2}, CikMiktar = CikMiktar + {2}, CikTarih = {5}, SonSayimTarih = {5}, SonSayimSonuc = {3}, Degistiren = '{4}', DegisTarih = {5}, DegisSaat = {6} " +
                            "WHERE(MalKodu = '{1}');", mGorev.IR.SirketKod, item.MalKodu, (item.Stok - item.Miktar).ToDot(), item.Miktar.ToDot(), vUser.UserName, tarih, saat);
                    }
                }
                db.Database.ExecuteSqlCommand(sql);
                //son olarak bizim stoka kaydet
                //get list
                sql = string.Format("SELECT wms.Yer.KatID, wms.GorevYer.MalKodu, wms.GorevYer.Birim, wms.GorevYer.Miktar, " +
                                        "ISNULL((SELECT Miktar FROM wms.Yer AS Yer2 WHERE (KatID = wms.Yer.KatID) AND (MalKodu = wms.Yer.MalKodu) AND (Birim = wms.Yer.Birim)), 0) as Stok " +
                                        "FROM wms.Gorev WITH(NOLOCK) INNER JOIN wms.GorevYer WITH(NOLOCK) ON wms.Gorev.ID = wms.GorevYer.GorevID INNER JOIN wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID " +
                                        "WHERE (wms.Gorev.ID = {0})", GorevID);
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
            ViewBag.Details = db.Database.SqlQuery<frmPaketBarkod>(string.Format("EXEC [FINSAT6{0}].[wms].[getBarcodeDetails] @SirketKodu = N'{0}', @DepoKodu = N'{1}', @EvrakNo = N'{2}'", tblx.Gorev.IR.SirketKod, tblx.Gorev.Depo.DepoKodu, tblx.Gorev.IR.LinkEvrakNo)).FirstOrDefault();
            return View("BarcodePrint", tblx);
        }
    }
}