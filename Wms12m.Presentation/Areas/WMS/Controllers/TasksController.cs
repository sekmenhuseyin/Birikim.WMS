using Birikim.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TumFaturaKayit;
using Wms12m.Business;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class TasksController : RootController
    {
        public ActionResult Index2()
        {
            ViewBag.DurumID = new SelectList(ComboSub.GetList(Combos.GorevDurum.ToInt32()), "ID", "Name");
            return View("Index2");
        }

        public JsonResult List2([DataSourceRequest]DataSourceRequest request, int Id, int Tarih = 0)
        {
            return Json(db.GetTaskList(Id, vUser.DepoId ?? 0, Tarih).ToList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

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
        public PartialViewResult List(int Id, int Tarih = 0)
        {
            return base.PartialView("List", db.GetTaskList(Id, vUser.DepoId ?? 0, Tarih).ToList());
        }

        /// <summary>
        /// görev ayrıntıları
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            var sql = string.Format("EXEC FINSAT6{0}.wms.GetIrsDetayfromGorev {1}", vUser.SirketKodu, ID);
            var list = new frmTaskDetails()
            {
                irsdetay = db.Database.SqlQuery<frmIrsDetayfromGorev>(sql).ToList(),
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
            // update
            var _Result = Task.Update(tbl);
            // get list
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
            var ID = Convert.ToInt32(id);
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
            var _Result = Task.UpdateGorevli(tbl);
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
            return PartialView("CountNew");
        }

        /// <summary>
        /// yeni görevi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult SaveNew(int DepoID)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(false, "Bu görev zaten var");
            // kontrol
            var açık = ComboItems.Açık.ToInt32();
            var sayim = ComboItems.KontrolSayım.ToInt32();
            var grv = db.Gorevs.Where(m => m.DepoID == DepoID && m.GorevTipiID == sayim && m.DurumID == açık).FirstOrDefault();
            if (grv == null)
            {
                var tarih = fn.ToOADate();
                var grvNo = db.SettingsGorevNo(tarih, DepoID).FirstOrDefault();
                var depo = Store.Detail(DepoID).DepoKodu;
                var cevap = db.InsertIrsaliye(vUser.SirketKodu, DepoID, grvNo, grvNo, tarih, depo + " Kontrollü Sayım", false, sayim, vUser.UserName, tarih, fn.ToOATime(), depo, "", 0, "", "").FirstOrDefault();
                grv = db.Gorevs.Where(m => m.ID == cevap.GorevID).FirstOrDefault();
                grv.DurumID = açık;
                db.SaveChanges();
                LogActions("WMS", "Tasks", "SaveNew", ComboItems.alEkle, grv.ID, "Sayım: " + grv.IR.HesapKodu);
                _Result = new Result(true);
            }
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// kontrollü sayima ait fark sayfası
        /// </summary>
        public PartialViewResult CountFark(int ID, string Tip)
        {
            if (CheckPerm(Perms.GörevListesi, PermTypes.Reading) == false) return null;
            // create sql
            var sql = "";
            // eksik liste için farklı sql
            if (Tip == "2")
                sql = string.Format("EXEC FINSAT6{0}.wms.getSayimEksikList @ID = {1}", vUser.SirketKodu, ID);
            else
                sql = string.Format("EXEC FINSAT6{0}.wms.getSayimList @ID = {1}, @FarkMi = {2}", vUser.SirketKodu, ID, Tip == "1" ? 1 : 0);
            // run
            var list = db.Database.SqlQuery<frmSiparisMalzemeDetay>(sql).ToList();
            // return
            ViewBag.Tip = Tip;
            ViewBag.ID = ID;
            return PartialView("CountFark", list);
        }

        /// <summary>
        /// sayım fişi kaydeder
        /// </summary>
        [HttpPost]
        public JsonResult CountCreate(int GorevID, bool Tip)
        {
            // kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var durumID = ComboItems.Tamamlanan.ToInt32();
            var tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == true)
                return Json(new Result(false, "Sayım fişi daha önce oluşturulmuş!"), JsonRequestBehavior.AllowGet);
            // seri kontrol
            var details = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
            if (details == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri.Value < 1 || details.SayimSeri.Value > 199)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            // variables
            var tarih = fn.ToOADate();
            short sirano = 0;
            List<STI> stiList = new List<STI>();
            // loop malkods
            var sql = string.Format("EXEC FINSAT6{0}.wms.getSayimFisiListesi1 @ID = {1}", vUser.SirketKodu, GorevID);
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

            // eğer eksik listesi de atılacaksa sayım fişine biraz daha ekle
            if (Tip == true)
            {
                sql = string.Format(@"EXEC FINSAT6{0}.wms.getSayimFisiListesi2 @ID = {1}, @DepoKodu = '{2}'", vUser.SirketKodu, GorevID, mGorev.Depo.DepoKodu);
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

            // finsat tanımlama
            var evrakSeriNo = 7100 + details.SayimSeri.Value - 1;
            var finsat = new Finsat(db, mGorev.IR.SirketKod, SqlExper, new FaturaKayit(SqlExper, mGorev.IR.SirketKod, SqlExper, mGorev.IR.SirketKod));
            var sonuc = finsat.SayımVeFarkFişi(stiList, evrakSeriNo, true, vUser.UserName);
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
            // kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var durumID = ComboItems.Tamamlanan.ToInt32();
            var tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.LinkEvrakNo != null)
                return Json(new Result(false, "Fark fişi daha önce oluşturulmuş!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == false)
                return Json(new Result(false, "Sayım fişi daha oluşturulmamış!"), JsonRequestBehavior.AllowGet);
            // seri kontrol
            var details = db.UserDetails.Where(m => m.UserID == vUser.Id).FirstOrDefault();
            if (details == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri == null)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            if (details.SayimSeri.Value < 1 || details.SayimSeri.Value > 199)
                return Json(new Result(false, "Seri hatası!"), JsonRequestBehavior.AllowGet);
            // variables
            var tarih = fn.ToOADate();
            var saat = fn.ToOATime();
            short sirano = 0;
            var stiList = new List<STI>();
            // loop malkods
            var sql = string.Format("SELECT IslemTur, MalKodu, Miktar, Miktar2, Birim, Depo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 95) AND (IslemTip = 18)", mGorev.IR.SirketKod, mGorev.IR.EvrakNo);
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
            if (sirano == 0)
            {
                // son olarak bizim stoka kaydet
                sql = string.Format(@"EXEC BIRIKIM.wms.GetSayimFarki {0}, {1}", GorevID, mGorev.IR.ValorGun);
                var list2 = db.Database.SqlQuery<frmSiparisToplama>(sql).ToList();
                // loop list
                short siranok = 0;
                foreach (var item in list2)
                {
                    siranok += 1;

                    // yerleştirme kaydı yapılır
                    var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, "", item.MakaraNo);
                    if (tmp2 == null)
                    {
                        tmp2 = new Yer()
                        {
                            KatID = item.KatID,
                            MalKodu = item.MalKodu,
                            Birim = item.Birim,
                            Miktar = item.Miktar,
                            MakaraNo = item.MakaraNo
                        };
                        Yerlestirme.Insert(tmp2, vUser.Id, "Sayım Farkı Fişi", mGorev.IrsaliyeID.Value);
                    }
                    else
                    {
                        if (item.Miktar > item.Stok)//giriş
                        {
                            tmp2.Miktar = item.Miktar;
                            Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi", item.Miktar - item.Stok, false, mGorev.IrsaliyeID.Value);
                        }
                        else if (item.Miktar < item.Stok)//çıkış
                        {
                            tmp2.Miktar = item.Miktar;
                            Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi", item.Stok - item.Miktar, true, mGorev.IrsaliyeID.Value);
                        }
                    }
                }
                //fark fişi tuşu kaybolsun diye boş kayıt atıyoruz
                mGorev.IR.LinkEvrakNo = "";
                db.SaveChanges();
                //return result
                if (siranok > 0)
                    return Json(new Result(true, "Güneş Fark fişine gerek yok, WMS fark fişi hareketi atıldı."), JsonRequestBehavior.AllowGet);
                else
                    return Json(new Result(false, "Fark fişine gerek yok."), JsonRequestBehavior.AllowGet);
            }
            // finsat tanımlama
            var EvrakSeriNo = 7500 + details.SayimSeri.Value - 1;
            var finsat = new Finsat(db, mGorev.IR.SirketKod, SqlExper, new FaturaKayit(SqlExper, mGorev.IR.SirketKod, SqlExper, mGorev.IR.SirketKod));
            var sonuc = finsat.SayımVeFarkFişi(stiList, EvrakSeriNo, true, vUser.UserName);
            if (sonuc.Status == true)
            {
                mGorev.IR.LinkEvrakNo = sonuc.Message;
                LogActions("WMS", "Tasks", "CountCreateDiff", ComboItems.alEkle, mGorev.ID, "Sayım Fark Fişi: " + mGorev.IR.LinkEvrakNo);
                db.SaveChanges();
                // finsat dst & stk update
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
                // son olarak bizim stoka kaydet
                sql = string.Format(@"EXEC BIRIKIM.wms.GetSayimFarki {0}, {1}", GorevID, mGorev.IR.ValorGun);
                var list2 = db.Database.SqlQuery<frmSiparisToplama>(sql).ToList();
                // loop list
                foreach (var item in list2)
                {
                    // yerleştirme kaydı yapılır
                    var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, "", item.MakaraNo);
                    if (tmp2 == null)
                    {
                        tmp2 = new Yer()
                        {
                            KatID = item.KatID,
                            MalKodu = item.MalKodu,
                            Birim = item.Birim,
                            Miktar = item.Miktar,
                            MakaraNo = item.MakaraNo
                        };
                        Yerlestirme.Insert(tmp2, vUser.Id, "Sayım Farkı Fişi", mGorev.IrsaliyeID.Value);
                    }
                    else
                    {
                        if (item.Miktar > item.Stok)//giriş
                        {
                            tmp2.Miktar = item.Miktar;
                            Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi", item.Miktar - item.Stok, false, mGorev.IrsaliyeID.Value);
                        }
                        else if (item.Miktar < item.Stok)//çıkış
                        {
                            tmp2.Miktar = item.Miktar;
                            Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi", item.Stok - item.Miktar, true, mGorev.IrsaliyeID.Value);
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
            var durumID = ComboItems.Açık.ToInt32();
            var tipID = ComboItems.KontrolSayım.ToInt32();
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
            var durumID = ComboItems.Tamamlanan.ToInt32();
            var tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            mGorev.DurumID = ComboItems.Açık.ToInt32();
            mGorev.BitisTarihi = null;
            mGorev.BitisSaati = null;
            foreach (var item in mGorev.GorevUsers)
                item.BitisTarihi = null;

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
            // kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var durumID = ComboItems.Tamamlanan.ToInt32();
            var tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.Onay == false)
                return Json(new Result(false, "Sayım fişi bulunamadı!"), JsonRequestBehavior.AllowGet);
            // variables
            var sql = string.Format("DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 95) AND (IslemTip = 18);", mGorev.IR.SirketKod, mGorev.IR.EvrakNo);
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
            // kontrols
            if (CheckPerm(Perms.GörevListesi, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var durumID = ComboItems.Tamamlanan.ToInt32();
            var tipID = ComboItems.KontrolSayım.ToInt32();
            var mGorev = db.Gorevs.Where(m => m.ID == GorevID && m.GorevTipiID == tipID && m.DurumID == durumID).FirstOrDefault();
            if (mGorev.IsNull())
                return Json(new Result(false, "Görev bulunamadı!"), JsonRequestBehavior.AllowGet);
            if (mGorev.IR.LinkEvrakNo == null)
                return Json(new Result(false, "Fark fişi bulunamadı!"), JsonRequestBehavior.AllowGet);

            // variables
            var tarih = fn.ToOADate();
            var saat = fn.ToOATime();
            List<STI> stiList = new List<STI>();
            // loop malkods
            var sql = string.Format("SELECT IslemTur, MalKodu, Miktar, Miktar2, Birim, Depo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 100) AND (IslemTip = 20)", mGorev.IR.SirketKod, mGorev.IR.LinkEvrakNo);
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
                var tmp2 = Yerlestirme.Detail(item.KatID, item.MalKodu, "", item.MakaraNo);
                if (item.GC == true)
                {
                    tmp2.Miktar += item.Miktar;
                    Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi İptal", item.Miktar, false, mGorev.IR.ID);
                }
                else
                {
                    tmp2.Miktar -= item.Miktar;
                    Yerlestirme.Update(tmp2, vUser.Id, "Sayım Farkı Fişi İptal", item.Miktar, true, mGorev.IR.ID);
                }
            }

            mGorev.IR.LinkEvrakNo = null;
            db.SaveChanges();

            return Json(new Result(true, mGorev.ID, "İşlem tamlandı!"), JsonRequestBehavior.AllowGet);
        }
    }
}