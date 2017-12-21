using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class PurchaseController : RootController
    {
        /// <summary>
        /// irsaliye sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            return View("Index", new frmIrsaliye());
        }
        /// <summary>
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult List()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0,") return null;
            string[] tmp = id.ToString().Split(',');
            if (tmp.Length != 2) return null;
            // kontrol
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false) return null;
            // get liste
            var list = Irsaliye.GetList(vUser.SirketKodu, false, tmp[1], tmp[0].ToInt32());
            ViewBag.id = id;
            ViewBag.Yetki = CheckPerm(Perms.MalKabul, PermTypes.Writing);
            ViewBag.Yetki2 = CheckPerm(Perms.MalKabul, PermTypes.Deleting);
            return PartialView("List", list);
        }
        /// <summary>
        /// irsaliye evrak no günceller
        /// </summary>
        public JsonResult Update(string EvrakNo, int ID, string HesapKodu)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(false, "Bu evrak no kullanılıyor");
            // length kontrol
            if (EvrakNo.Length > 16)
            {
                _Result.Message = "Bu evrak no çok uzun.";
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            // birikim db kontrol
            var kontrol = db.IRS.Where(m => m.IslemTur == false && m.EvrakNo == EvrakNo && m.SirketKod == vUser.SirketKodu & m.HesapKodu == HesapKodu && m.ID != ID).FirstOrDefault();
            if (kontrol != null)
                return Json(_Result, JsonRequestBehavior.AllowGet);
            // finsta db kontrol
            var sql = string.Format("SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 3) AND (Chk = '{2}')", vUser.SirketKodu, EvrakNo, HesapKodu);
            var sti = db.Database.SqlQuery<string>(sql).FirstOrDefault();
            if (sti != null)
                return Json(_Result, JsonRequestBehavior.AllowGet);
            // if all correct update
            var tbl = Irsaliye.Detail(ID);
            tbl.EvrakNo = EvrakNo;
            _Result = Irsaliye.Operation(tbl);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// seçili görewvi başlatır
        /// </summary>
        [HttpPost]
        public JsonResult Activate(int ID)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            try
            {
                db.Database.ExecuteSqlCommand(string.Format(@"UPDATE wms.IRS SET Onay = 1 WHERE (ID IN
                                                                        (SELECT IrsaliyeID FROM wms.GorevIRS WHERE (GorevID IN
                                                                                    (SELECT GorevID FROM wms.GorevIRS WHERE (IrsaliyeID = {0}))
                                                                        ))
                                                                    )
                                                            UPDATE wms.Gorev SET DurumID = 9 WHERE (ID IN
                                                                        (SELECT GorevID FROM wms.GorevIRS WHERE (IrsaliyeID = {0}))
                                                            )", ID));
                return Json(new Result(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Kayıtta hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// irs detay güncelle
        /// </summary>
        [HttpPost]
        public JsonResult UpdateList(int ID, decimal M, string mNo)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var tbl = db.IRS_Detay.Where(m => m.ID == ID).FirstOrDefault();
            tbl.Miktar = M;
            if (tbl.MakaraNo != mNo)
            {
                if (mNo == "" || mNo == null)
                {
                    var kkablo = db.Database.SqlQuery<string>(string.Format("SELECT Kod1 FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = '{1}')", vUser.SirketKodu, tbl.MalKodu)).FirstOrDefault();
                    if (kkablo == "KKABLO")
                    {
                        mNo = "Boş-" + db.SettingsMakaraNo(tbl.IR.DepoID).FirstOrDefault();
                    }
                }

                if (mNo != null && mNo != "")
                {
                    // depo stoktaki makara noları ve
                    // bu depodaki durdurulanlar hariç tüm mal kabuldeki makara noları kontrol eder
                    var makara = db.Database.SqlQuery<string>(string.Format("BIRIKIM.wms.MakaraNoKontrol @DepoID = {0} , @MakaraNo='{1}'", tbl.IR.DepoID, mNo)).FirstOrDefault();
                    if (makara != "" && makara != null)
                        return Json(new Result(false, "Bu makara no kullanılıyor"), JsonRequestBehavior.AllowGet);
                }
            }
            try
            {
                tbl.MakaraNo = mNo;
                db.SaveChanges();
                return Json(new Result(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Kayıtta hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult SiparisList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0,") return null;
            string[] tmp = id.ToString().Split(',');
            if (tmp.Length != 2) return null;
            // kontrol
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false) return null;
            // get list
            var depo = Store.Detail(tmp[0].ToInt32()).DepoKodu;
            var hesapKodu = tmp[1].ToString();
            var sql = string.Format(@"
                                SELECT  FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, 
                                        FINSAT6{0}.FINSAT6{0}.STK.MalKodu, 
                                        FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar AS AçıkMiktar, 
                                        FINSAT6{0}.FINSAT6{0}.SPI.Birim 
                                FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) 
                                     INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu 
                                     INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu 
                                WHERE (FINSAT6{0}.FINSAT6{0}.SPI.IslemTur = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 63) 
                                      AND (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Chk = '{2}') 
                                      AND FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID 
                                      NOT IN (SELECT BIRIKIM.wms.IRS_Detay.KynkSiparisID 
                                              FROM BIRIKIM.wms.IRS_Detay INNER JOIN BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID 
                                              INNER JOIN BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID 
                                              WHERE (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 15) 
                                      AND (NOT(BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL)) 
                                GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID)", vUser.SirketKodu, depo, hesapKodu);
            var list = db.Database.SqlQuery<frmSiparistenGelen>(sql).ToList();
            return PartialView("SiparisList", list);
        }
        /// <summary>
        /// siparişten malzeme ekler
        /// </summary>
        [HttpPost]
        public PartialViewResult FromSiparis(string id, string ids)
        {
            if (id == null || ids == null) return null;
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return null;
            int irsaliyeID = id.ToInt32(), eklenen = 0, sira = 0;
            // split ids into rows
            ids = ids.Left(ids.Length - 1);
            string[] tmp = ids.Split('#');
            var satir = new IRS_Detay();
            var tbl = new List<IRS_Detay>();
            foreach (var item in tmp)
            {
                if (sira == 0)
                {
                    // evrak no ekle
                    satir.KynkSiparisNo = item;
                    sira++;
                }
                else if (sira == 1)
                {
                    // malkodu ekle
                    satir.MalKodu = item;
                    sira++;
                }
                else if (sira == 2)
                {
                    // birim ekle
                    satir.Birim = item;
                    sira++;
                }
                else if (sira == 3)
                {
                    // row id ekle
                    satir.KynkSiparisID = item.ToInt32();
                    sira++;
                }
                else
                {
                    // miktar ekle
                    satir.Miktar = item.ToDecimal();
                    tbl.Add(satir);
                    satir = new IRS_Detay();
                    sira = 0;
                }
            }

            // sadece irsaliye daha onaylanmamışsa yani işlemleri bitmeişse ekle
            var irs = Irsaliye.Detail(irsaliyeID);
            if (irs.Onay == false)
            {
                foreach (var item in tbl)
                {
                    var sql = string.Format(@"
                                SELECT SPI.EvrakNo, SPI.Tarih, SPI.SiraNo, SPI.MalKodu, SPI.BirimMiktar, 
                                    (SPI.BirimMiktar - SPI.TeslimMiktar - SPI.KapatilanMiktar) AS Miktar, 
                                    SPI.Birim, SPI.DegisSaat , STK.Kod1
                                FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) 
                                     INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON SPI.MalKodu=STK.MalKodu
                                WHERE (SPI.ROW_ID = {1}) AND (SPI.IslemTur = 0) AND (SPI.KynkEvrakTip = 63) AND (SPI.SiparisDurumu = 0) 
                                AND (SPI.MalKodu = '{2}') AND (SPI.Birim = '{3}') AND (LTRIM(SPI.EvrakNo) = '{4}')", vUser.SirketKodu, item.KynkSiparisID, item.MalKodu, item.Birim, item.KynkSiparisNo.Trim());
                    try
                    {
                        var tempTbl = db.Database.SqlQuery<frmIrsaliyeMalzeme>(sql).FirstOrDefault();
                        // save details

                        var mNo = "";
                        if (tempTbl.Kod1 == "KKABLO")
                            mNo = "Boş-" + db.SettingsMakaraNo(irs.DepoID).FirstOrDefault();

                        var sti = new IRS_Detay()
                        {
                            IrsaliyeID = irsaliyeID,
                            Birim = tempTbl.Birim,
                            KynkSiparisMiktar = tempTbl.BirimMiktar,
                            KynkSiparisID = item.KynkSiparisID,
                            KynkSiparisNo = tempTbl.EvrakNo,
                            KynkSiparisSiraNo = tempTbl.SiraNo,
                            KynkSiparisTarih = tempTbl.Tarih,
                            KynkDegisSaat = tempTbl.DegisSaat,
                            MalKodu = tempTbl.MalKodu,
                            Miktar = item.Miktar > 0 ? item.Miktar : tempTbl.Miktar,
                            MakaraNo = mNo
                        };
                        var _Result = IrsaliyeDetay.Operation(sti);
                        eklenen++;
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "WMS/Purchase/FromSiparis");
                    }
                }
            }

            // get list
            var list = IrsaliyeDetay.GetList(irsaliyeID);
            ViewBag.IrsaliyeId = irsaliyeID;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = vUser.SirketKodu;
            ViewBag.Yetki = true;
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// yeni irsaliye fatura kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult New(frmIrsaliye tbl)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false)
            {
                ViewBag.message = "Burası için izniniz yok";
                return PartialView("_GridPartial", new List<IRS_Detay>());
            }

            if (tbl.EvrakNo.Length > 16)
            {
                ViewBag.message = "Bu evrak no çok uzun";
                return PartialView("_GridPartial", new List<IRS_Detay>());
            }

            var kontrol1 = DateTime.TryParse(tbl.Tarih, out DateTime tmpTarih);
            if (kontrol1 == false)
            {
                db.Logger(vUser.UserName, "", fn.GetIPAddress(), "Tarih hatası: " + tbl.Tarih, "", "WMS/Purchase/New");
                ViewBag.message = "Tarih yanlış";
                return PartialView("_GridPartial", new List<IRS_Detay>());
            }

            var tarih = tmpTarih.ToOADateInt();
            var kontrol2 = db.IRS.Where(m => m.IslemTur == false && m.EvrakNo == tbl.EvrakNo && m.SirketKod == vUser.SirketKodu & m.HesapKodu == tbl.HesapKodu).FirstOrDefault();
            // var olanı göster
            if (kontrol2 != null)
            {
                if (kontrol2.DepoID != tbl.DepoID)
                {
                    ViewBag.message = "Bu evrak no kullanılıyor";
                    return PartialView("_GridPartial", new List<IRS_Detay>());
                }

                try
                {
                    var list = IrsaliyeDetay.GetList(kontrol2.ID);
                    ViewBag.IrsaliyeId = kontrol2.ID;
                    ViewBag.Onay = kontrol2.Onay;
                    ViewBag.SirketID = vUser.SirketKodu;
                    ViewBag.Yetki = CheckPerm(Perms.MalKabul, PermTypes.Writing);
                    return PartialView("_GridPartial", list);
                }
                catch (Exception ex)
                {
                    Logger(ex, "WMS/Purchase/New-varolan");
                    return null;
                }
            }

            // kontrol
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return null;
            // yeni kayıtta evrak no spide olmayacak kontrolü
            var sql = string.Format("SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 3) AND (Chk = '{2}')", vUser.SirketKodu, tbl.EvrakNo, tbl.HesapKodu);
            var sti = db.Database.SqlQuery<string>(sql).FirstOrDefault();
            if (sti != null)
            {
                ViewBag.message = "Bu evrak no kullanılıyor";
                return PartialView("_GridPartial", new List<IRS_Detay>());
            }

            // yeni kayıt
            var gorevno = db.SettingsGorevNo(DateTime.Today.ToOADateInt(), tbl.DepoID).FirstOrDefault();
            var today = fn.ToOADate();
            var time = fn.ToOATime();
            try
            {
                var cevap = db.InsertIrsaliye(vUser.SirketKodu, tbl.DepoID, gorevno, tbl.EvrakNo, tarih, "Tedarikçi: " + tbl.Unvan, false, ComboItems.MalKabul.ToInt32(), vUser.UserName, today, time, tbl.HesapKodu, "", 0, "", "").FirstOrDefault();
                LogActions("WMS", "Purchase", "New", ComboItems.alEkle, cevap.GorevID.Value, "Tedarikçi: " + tbl.Unvan);
                // get list
                var list = IrsaliyeDetay.GetList(cevap.IrsaliyeID.Value);
                ViewBag.IrsaliyeId = cevap.IrsaliyeID;
                ViewBag.Onay = false;
                ViewBag.SirketID = vUser.SirketKodu;
                ViewBag.Yetki = CheckPerm(Perms.MalKabul, PermTypes.Writing);
                return PartialView("_GridPartial", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Purchase/New-yeni");
                return null;
            }
        }
        /// <summary>
        /// listeyi günceller
        /// </summary>
        public PartialViewResult GridPartial(int ID)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Reading) == false) return null;
            var list = IrsaliyeDetay.GetList(ID);
            var irs = Irsaliye.Detail(ID);
            ViewBag.IrsaliyeId = ID;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = vUser.SirketKodu;
            ViewBag.Yetki = CheckPerm(Perms.MalKabul, PermTypes.Writing);
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// irs detay düzenle
        /// </summary>
        public PartialViewResult EditList(int ID)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return null;
            var tbl = IrsaliyeDetay.Detail(ID);
            ViewBag.SirketID = vUser.SirketKodu;
            return PartialView("EditList", tbl);
        }
        /// <summary>
        /// yeni malzeme
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult InsertMalzeme(frmMalzeme tbl)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return null;
            // sadece irsaliye daha onaylanmamışsa yani işlemleri bitmemişse ekle
            var irs = Irsaliye.Detail(tbl.IrsaliyeId);
            if (irs.Onay == false)
            {
                if (tbl.MakaraNo == "" || tbl.MakaraNo == null)
                {
                    var kkablo = db.Database.SqlQuery<string>(string.Format("SELECT Kod1 FROM BIRIKIM.wms.GetSTK('{0}')", tbl.MalKodu)).FirstOrDefault();
                    if (kkablo == "KKABLO")
                    {
                        tbl.MakaraNo = "Boş-" + db.SettingsMakaraNo(irs.DepoID).FirstOrDefault();
                        return Json(IrsaliyeDetay.Insert(tbl, irs.DepoID), JsonRequestBehavior.AllowGet);
                    }
                }

                if (tbl.MakaraNo != null)
                {
                    // depo stoktaki makara noları ve
                    // deu depodaki durdurulanlar hariç tüm mal kabuldeki makara noları kontrol eder
                    var makara = db.Database.SqlQuery<string>(string.Format("BIRIKIM.wms.MakaraNoKontrol @DepoID = {0} , @MakaraNo='{1}'", irs.DepoID, tbl.MakaraNo)).FirstOrDefault();
                    if (makara == "" || makara == null)
                        return Json(IrsaliyeDetay.Insert(tbl, irs.DepoID), JsonRequestBehavior.AllowGet);
                    else
                        return Json(new Result(false, "Bu makara no kullanılıyor"), JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(IrsaliyeDetay.Insert(tbl, irs.DepoID), JsonRequestBehavior.AllowGet);
            }

            return Json(new Result(false, "Bu irsaliyeye ürün eklenemez"), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// malzeme autocomplete
        /// </summary>
        public JsonResult GetMalzemebyCode(string term)
        {
            var sql = string.Format("FINSAT6{0}.[wms].[getMalzemeByCodeOrName] @MalKodu = N'{1}', @MalAdi = N''", vUser.SirketKodu, term);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Purchase/getMalzemebyCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetMalzemebyName(string term)
        {
            var sql = string.Format("FINSAT6{0}.[wms].[getMalzemeByCodeOrName] @MalKodu = N'{1}', @MalAdi = N''", vUser.SirketKodu, term);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Purchase/getMalzemebyName");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// malzeme koduna göre birim getirir
        /// </summary>
        [HttpPost]
        public JsonResult GetBirim(string kod)
        {
            var sql = string.Format("SELECT Birim1, Birim2, Birim3, Birim4 FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = '{1}')", vUser.SirketKodu, kod);

            try
            {
                var list = db.Database.SqlQuery<frmBirims>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Purchase/getBirim");
                return Json(new List<frmBirims>(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// get chk codes
        /// </summary>
        public JsonResult GetChkKod(string term)
        {
            var sql = string.Format("FINSAT6{0}.[wms].[CHKSearch4] @HesapKodu = N'{1}', @Unvan = N'', @top = 200", vUser.SirketKodu, term);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Purchase/GetChkKod");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// get chk codes
        /// </summary>
        public JsonResult GetChkUnvan(string term)
        {
            var sql = string.Format("FINSAT6{0}.[wms].[CHKSearch4] @HesapKodu = N'', @Unvan = N'{1}', @top = 200", vUser.SirketKodu, term);
            // return
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Purchase/GetChkUnvan");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// yeni malzeme satırı formu
        /// </summary>
        public PartialViewResult NewMalzemeForm(int id)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return null;
            ViewBag.IrsaliyeId = id;
            return PartialView("_GridNewPartial", new frmMalzeme());
        }
        /// <summary>
        /// irsaliye sil
        /// </summary>
        public JsonResult Delete1(int ID)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = Irsaliye.Delete(ID);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// stok malzeme sil
        /// </summary>
        public JsonResult Delete2(int ID)
        {
            if (CheckPerm(Perms.MalKabul, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = IrsaliyeDetay.Delete(ID);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}