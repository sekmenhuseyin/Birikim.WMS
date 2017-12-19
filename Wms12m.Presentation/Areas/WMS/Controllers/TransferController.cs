using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.WMS.Controllers
{
    public class TransferController : RootController
    {
        #region Planlama
        /// <summary>
        /// transfer planlama
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm(Perms.Transfer, PermTypes.Reading) == false) return Redirect("/");
            ViewBag.GirisDepo = new SelectList(Store.GetList(vUser.DepoId), "DepoKodu", "DepoAd");
            ViewBag.CikisDepo = new SelectList(Store.GetList(), "DepoKodu", "DepoAd");
            ViewBag.AraDepo = ViewBag.CikisDepo;
            return View("Index");
        }
        /// <summary>
        /// planlamadaki 1. adımdaki malzeme listesi
        /// </summary>
        public PartialViewResult List(string Id)
        {
            var parameters = JsonConvert.DeserializeObject<JObject>(Id);
            string GirisDepo = parameters["GirisDepo"].ToString(), CikisDepo = parameters["CikisDepo"].ToString(), AraDepo = parameters["AraDepo"].ToString(), listType = parameters["listType"].ToString();
            if (GirisDepo == CikisDepo || GirisDepo == AraDepo || CikisDepo == AraDepo || CikisDepo == "" || GirisDepo == "" || AraDepo == "")
                return PartialView("List", new List<frmTransferMalzemeler>());
            ViewBag.SirketID = vUser.SirketKodu;
            ViewBag.GirisDepo = GirisDepo;
            ViewBag.CikisDepo = CikisDepo;
            var sql = listType == "below" ? "AND (ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0) - ISNULL(DST.KritikStok, 0)) < 0  " : "";
            sql = string.Format("SELECT STK.MalKodu, STK.MalAdi, STK.Birim1 as Birim, " +
                            "(ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) as Depo1StokMiktar, ISNULL(DST.KritikStok, 0) as Depo1KritikMiktar, ((ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)) - ISNULL(DST.KritikStok, 0)) as Depo1GerekenMiktar, ISNULL(DST.AlSiparis, 0) as AlSiparis, ISNULL(DST.SatSiparis, 0) as SatSiparis, " +
                            "BIRIKIM.[wms].[fnGetStock]('{2}',STK.MalKodu,STK.Birim1, 0) as Depo2WmsStok, (ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0)) as Depo2GunesStok, isnull(DST2.KritikStok, 0) as Depo2KritikMiktar, ((ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0)) - isnull(DST2.KritikStok, 0)) as Depo2GerekenMiktar, CAST(0 AS DECIMAL) Depo2Miktar " +
                            "FROM FINSAT6{0}.FINSAT6{0}.STK(NOLOCK) STK LEFT join FINSAT6{0}.FINSAT6{0}.DST(NOLOCK) DST ON STK.MalKodu = DST.MalKodu and DST.Depo = '{1}' LEFT JOIN FINSAT6{0}.FINSAT6{0}.DST(NOLOCK) DST2 ON STK.MalKodu = DST2.MalKodu AND DST2.Depo = '{2}' LEFT JOIN(SELECT MalKodu, SUM(Miktar-TeslimMiktar) Miktar FROM  FINSAT6{0}.FINSAT6{0}.DTF(NOLOCK) WHERE GirDepo = '{1}' AND Durum = 0 GROUP BY MalKodu) DTF ON DTF.MalKodu = STK.MalKodu " +
                            "WHERE ((ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0)) - (SELECT isnull(SUM(Miktar - TeslimMiktar), 0) Miktar FROM  FINSAT6{0}.FINSAT6{0}.DTF(NOLOCK) WHERE CikDepo = '{2}' AND Durum = 0 and MalKodu = DST2.MalKodu))>0 " + sql + "ORDER BY DST.MalKodu asc", vUser.SirketKodu, GirisDepo, CikisDepo);
            try
            {
                var list = db.Database.SqlQuery<frmTransferMalzemeler>(sql).ToList();
                return PartialView("List", list);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Transfer/List");
                return PartialView("List", new List<frmTransferMalzemeler>());
            }
        }
        /// <summary>
        /// ilk sayfada seçtiklerini gösterip onaylatan bir sayfa
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult Summary(frmTransferMalzemeApprove tbl)
        {
            ViewBag.Result = new Result(false, "Yetkiniz yok.");
            if (CheckPerm(Perms.Transfer, PermTypes.Writing) == false) return PartialView("Summary");
            ViewBag.Result = new Result(false, "Eksik bilgi girdiniz.");
            if (vUser.SirketKodu == "" || tbl.GirisDepo == "" || tbl.AraDepo == "" || tbl.CikisDepo == "" || tbl.checkboxes.ToString2() == "")
                return PartialView("Summary");
            ViewBag.Result = new Result(false, "Aynı depoları seçemezsiniz.");
            if (tbl.GirisDepo == tbl.AraDepo || tbl.CikisDepo == tbl.AraDepo || tbl.CikisDepo == tbl.GirisDepo)
                return PartialView("Summary");
            // liste oluştur
            tbl.checkboxes = tbl.checkboxes.Left(tbl.checkboxes.Length - 1);
            string[] tmp = tbl.checkboxes.Split('#');
            var malkodlari = ""; var sira = 0;
            var mallar = new Transfer_Detay();
            var mallistesi = new List<Transfer_Detay>();
            foreach (var item in tmp)
            {
                if (sira == 0)
                {
                    // malkodunu ve transfer id ekle
                    mallar.MalKodu = item;
                    if (malkodlari != "") malkodlari += ",";
                    malkodlari += "'" + item + "'";
                    sira++;
                }
                else if (sira == 1)
                {
                    // birim ekle
                    mallar.Birim = item;
                    sira++;
                }
                else
                {
                    // miktar ekle
                    mallar.Miktar = item.ToDecimal();
                    mallistesi.Add(mallar);
                    mallar = new Transfer_Detay();
                    sira = 0;
                }
            }

            // stok kontrol
            var varmi = false;
            foreach (var item in mallistesi)
            {
                var sayi = db.Yers.Where(m => m.MalKodu == item.MalKodu && m.Miktar > 0 && m.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.CikisDepo).FirstOrDefault();
                if (sayi != null) { varmi = true; break; }
            }

            ViewBag.Result = new Result(false, "Seçtiğiniz hiç bir ürün stokta kayıtlı değil.");
            if (varmi == false) return PartialView("Summary");
            // çapraz stok kontrol
            var sql = string.Format(@"SELECT STK.MalKodu, wms.fnGetStock('{2}', STK.MalKodu, STK.Birim1, 0) AS Depo2WmsStok, 
                                                            ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0) AS Depo2GunesStok
                                        FROM FINSAT6{0}.FINSAT6{0}.STK AS STK WITH(NOLOCK) LEFT OUTER JOIN
                                                                    FINSAT6{0}.FINSAT6{0}.DST AS DST WITH(NOLOCK) ON STK.MalKodu = DST.MalKodu AND DST.Depo = '{2}'
                                        WHERE(STK.MalKodu IN({1}))", vUser.SirketKodu, malkodlari, tbl.CikisDepo);
            var list1 = db.Database.SqlQuery<frmTransferMalzemeler>(sql).ToList();
            malkodlari = "";
            foreach (var item in list1)
            {
                if (item.Depo2GunesStok != item.Depo2WmsStok)
                {
                    if (malkodlari != "") malkodlari += ", ";
                    malkodlari += item.MalKodu;
                }
            }

            // add to list
            var aDepoID = Store.Detail(tbl.AraDepo).ID;
            var cDepoID = Store.Detail(tbl.CikisDepo);
            var gDepoID = Store.Detail(tbl.GirisDepo);
            int today = fn.ToOADate(), time = fn.ToOATime();
            // kontrol
            var kontrol = db.Transfers.Where(m => m.Onay == false && m.Gorev.DurumID == 15 && m.SirketKod == vUser.SirketKodu && m.GirisDepoID == gDepoID.ID && m.AraDepoID == aDepoID && m.CikisDepoID == cDepoID.ID && m.Gorev.OlusturmaTarihi == today).FirstOrDefault();
            if (kontrol != null)
            {
                db.DeleteTransfer(kontrol.GorevID);
            }

            // yeni bir görev eklenir
            var GorevNo = db.SettingsGorevNo(today, cDepoID.ID).FirstOrDefault();
            var cevap = db.InsertIrsaliye(vUser.SirketKodu, cDepoID.ID, GorevNo, GorevNo, today, "Giriş: " + tbl.GirisDepo + ", Çıkış: " + tbl.CikisDepo, true, ComboItems.TransferÇıkış.ToInt32(), vUser.UserName, today, time, cDepoID.DepoAd, "", 0, "", "").FirstOrDefault();
            // yeni transfer eklenir
            var sonuc = Transfers.Operation(new Transfer() { SirketKod = vUser.SirketKodu, GirisDepoID = gDepoID.ID, CikisDepoID = cDepoID.ID, AraDepoID = aDepoID, GorevID = cevap.GorevID.Value });
            ViewBag.Result = new Result(false, "Kayıtta hata oldu. Lütfen tekrar deneyin.");
            if (sonuc.Status == false)
                return PartialView("Summary");
            // find detays
            var eksikler = ""; var TransferID = sonuc.Id;
            foreach (var item in mallistesi)
            {
                // stok kontrol
                var tmpYer = db.Yers.Where(m => m.MalKodu == item.MalKodu && m.Birim == item.Birim && m.Kat.Bolum.Raf.Koridor.Depo.DepoKodu == tbl.CikisDepo && m.Miktar > 0).OrderByDescending(m => m.Miktar).ToList();
                decimal toplam = 0, miktar = 0;
                if (tmpYer.Count > 0)
                {
                    foreach (var itemyer in tmpYer)
                    {
                        if (itemyer.Miktar >= (item.Miktar - toplam))
                            miktar = item.Miktar - toplam;
                        else
                            miktar = itemyer.Miktar;
                        toplam += miktar;
                        // miktarı tabloya ekle
                        var tblyer = new GorevYer()
                        {
                            GorevID = cevap.GorevID.Value,
                            YerID = itemyer.ID,
                            MalKodu = item.MalKodu,
                            Birim = item.Birim,
                            Miktar = miktar,
                            MakaraNo = itemyer.MakaraNo,
                            GC = true
                        };
                        if (miktar > 0) TaskYer.Operation(tblyer);
                        // toplam yeterli miktardaysa
                        if (toplam == item.Miktar) break;
                    }

                    item.Miktar = toplam;
                    item.TransferID = TransferID;
                    // hepsi eklenince detayı db'ye ekle
                    if (item.Miktar > 0) { sonuc = Transfers.AddDetay(item); }
                    if (item.Miktar > 0) IrsaliyeDetay.Operation(new IRS_Detay() { IrsaliyeID = cevap.IrsaliyeID.Value, MalKodu = item.MalKodu, Miktar = miktar, Birim = item.Birim, KynkSiparisID = sonuc.Id, KynkSiparisTarih = TransferID });
                }
                else
                {
                    if (eksikler != "") eksikler += ", ";
                    eksikler += item.MalKodu;
                }
            }

            // dbler tempe aktarılıyor
            List<string> liste = new List<string>();
            liste.Add(vUser.SirketKodu);

            ViewBag.Sirket = liste;
            ViewBag.IrsaliyeId = cevap.IrsaliyeID.Value;
            if (eksikler == "" && malkodlari != "")
                eksikler = malkodlari + " için stok miktarları uyuşmuyor.";
            else if (eksikler != "" && malkodlari != "")
                eksikler += " için stok bulunamadı.<br />Ayrıca " + malkodlari + " için stok miktarları uyuşmuyor.";
            ViewBag.Result = new Result(true, eksikler);
            // return
            var list = db.Transfers.Where(m => m.ID == TransferID).FirstOrDefault();
            return PartialView("Summary", list);
        }
        /// <summary>
        /// özet sayfasındaki listeyi yeniler
        /// </summary>
        [HttpPost]
        public PartialViewResult SummaryList(int ID)
        {
            // dbler tempe aktarılıyor
            string malkodlari = "", eksikler = "";
            List<string> liste = new List<string>();
            liste.Add(vUser.SirketKodu);

            // get transfer
            var transfer = db.Transfers.Where(m => m.ID == ID).FirstOrDefault();
            // delete gorev yer
            var gorevyerleri = db.GorevYers.Where(m => m.GorevID == transfer.GorevID).ToList();
            db.GorevYers.RemoveRange(gorevyerleri);
            db.SaveChanges();
            // add gorev yer
            foreach (var item in transfer.Transfer_Detay)
            {
                // çapraz stok kontrol
                var sql = string.Format(@"SELECT STK.MalKodu, wms.fnGetStock('{2}', STK.MalKodu, STK.Birim1, 0) AS Depo2WmsStok, 
                                                            ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0)  AS Depo2GunesStok
                                        FROM FINSAT6{0}.FINSAT6{0}.STK AS STK WITH(NOLOCK) LEFT OUTER JOIN
                                                                    FINSAT6{0}.FINSAT6{0}.DST AS DST WITH(NOLOCK) ON STK.MalKodu = DST.MalKodu AND DST.Depo = '{2}'
                                        WHERE (STK.MalKodu = '{1}')", vUser.SirketKodu, item.MalKodu, item.Transfer.Depo1.DepoKodu);
                var list1 = db.Database.SqlQuery<frmTransferMalzemeler>(sql).ToList();
                foreach (var item2 in list1)
                {
                    if (item2.Depo2GunesStok != item2.Depo2WmsStok)
                    {
                        if (malkodlari != "") malkodlari += ", ";
                        malkodlari += item2.MalKodu;
                    }
                }

                // stok kontrol
                var tmpYer = db.Yers.Where(m => m.MalKodu == item.MalKodu && m.Birim == item.Birim && m.Kat.Bolum.Raf.Koridor.DepoID == transfer.CikisDepoID && m.Miktar > 0).OrderByDescending(m => m.Miktar).ToList();
                decimal toplam = 0, miktar = 0;
                if (tmpYer.Count > 0)
                {
                    foreach (var itemyer in tmpYer)
                    {
                        if (itemyer.Miktar >= (item.Miktar - toplam))
                            miktar = item.Miktar - toplam;
                        else
                            miktar = itemyer.Miktar;
                        toplam += miktar;
                        // miktarı tabloya ekle
                        var tblyer = new GorevYer()
                        {
                            GorevID = transfer.GorevID,
                            YerID = itemyer.ID,
                            MalKodu = item.MalKodu,
                            Birim = item.Birim,
                            Miktar = miktar,
                            GC = true
                        };
                        if (miktar > 0) TaskYer.Operation(tblyer);
                        // toplam yeterli miktardaysa
                        if (toplam == item.Miktar) break;
                    }
                }
                else
                {
                    if (eksikler != "") eksikler += ", ";
                    eksikler += item.MalKodu;
                }
            }

            if (eksikler == "" && malkodlari != "")
                eksikler = malkodlari + " için stok miktarları uyuşmuyor.";
            else if (eksikler != "" && malkodlari != "")
                eksikler += " için stok bulunamadı.<br />Ayrıca " + malkodlari + " için stok miktarları uyuşmuyor.";
            // return
            ViewBag.IrsaliyeId = transfer.Gorev.IrsaliyeID;
            ViewBag.Sirket = liste;
            ViewBag.Result = new Result(true, eksikler);
            return PartialView("SummaryList", transfer);
        }
        /// <summary>
        /// bekleyen transferi onayla
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Approve(int ID)
        {
            if (CheckPerm(Perms.Transfer, PermTypes.Writing) == false) return Redirect("/");
            // get and set transfer details
            var tbl = Transfers.Detail(ID);
            tbl.Onay = true;
            Transfers.Operation(tbl);
            var tbl2 = db.Gorevs.Where(m => m.ID == tbl.GorevID).FirstOrDefault();
            tbl2.DurumID = ComboItems.Açık.ToInt32();
            // sıralama
            var lstKoridor = db.GetKoridorIdFromGorevId(tbl.GorevID).ToList();
            var asc = false; var sira = 1;
            foreach (var item in lstKoridor)
            {
                var lstBolum = db.GetBolumSiralamaFromGorevId(tbl.GorevID, item.Value, asc).ToList();
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

            db.SaveChanges();
            // log
            LogActions("WMS", "Transfer", "Approve", ComboItems.alOnayla, ID);
            // return
            return RedirectToAction("Waiting");
        }
        /// <summary>
        /// transfer detay ekle
        /// </summary>
        public JsonResult AddDetail(frmMalzeme tbl)
        {
            if (CheckPerm(Perms.Transfer, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            if (tbl.MalKodu == "" || tbl.Birim == "" || tbl.Miktar < 1) return Json(new Result(false, "Eksik bilgi yazılmış"), JsonRequestBehavior.AllowGet);
            // ekle
            var item = new Transfer_Detay() { TransferID = tbl.Id, MalKodu = tbl.MalKodu, Miktar = tbl.Miktar, Birim = tbl.Birim };
            var _Result = Transfers.AddDetay(item);
            _Result = IrsaliyeDetay.Operation(new IRS_Detay() { IrsaliyeID = tbl.IrsaliyeId, MalKodu = item.MalKodu, Miktar = item.Miktar, Birim = item.Birim, KynkSiparisID = _Result.Id, KynkSiparisTarih = tbl.Id });
            try
            {
                db.SaveChanges();
                _Result.Id = tbl.Id;
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Transfer/AddDetail");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// transfer detay düzenle
        /// </summary>
        public PartialViewResult SummaryEdit(int ID)
        {
            var tbl = Transfers.SubDetail(ID);
            return PartialView("SummaryEdit", tbl);
        }
        /// <summary>
        /// transfer detay güncelle
        /// </summary>
        [HttpPost]
        public JsonResult UpdateList(int ID, decimal M)
        {
            if (CheckPerm(Perms.Transfer, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var tbl = db.Transfer_Detay.Where(m => m.ID == ID).FirstOrDefault();
            tbl.Miktar = M;
            try
            {
                db.SaveChanges();
                return Json(new Result(true, tbl.TransferID), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new Result(false, "Kayıtta hata oldu"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Onaylama
        /// <summary>
        /// onay bekleyen transfer sayfası
        /// </summary>
        public ActionResult Waiting()
        {
            if (CheckPerm(Perms.Transfer, PermTypes.Reading) == false) return Redirect("/");
            return View("Waiting");
        }
        /// <summary>
        /// onay bekleyen transfer listesi
        /// </summary>
        public PartialViewResult WaitingList(bool Id)
        {
            var list = Transfers.GetList(Id, vUser.DepoId);
            return PartialView("WaitingList", list);
        }
        /// <summary>
        /// transfere ait mallar
        /// </summary>
        [HttpPost]
        public PartialViewResult Details(int ID)
        {
            List<string> liste = new List<string>();
            liste.Add(vUser.SirketKodu);

            ViewBag.Sirket = liste;
            // return
            var result = db.Transfer_Detay.Where(m => m.TransferID == ID).Select(m => new frmMalKoduMiktar { MalKodu = m.MalKodu, Miktar = m.Miktar, Birim = m.Birim }).ToList();
            return PartialView("Details", result);
        }
        #endregion
        [HttpPost]
        public string TransferDetay(int ID)
        {
            var json = new JavaScriptSerializer()
            {
                MaxJsonLength = int.MaxValue
            };
            var result = db.Database.SqlQuery<frmDepoTransferDetay>(string.Format("FINSAT6{0}.wms.TransferDetayList @TransferID = '{1}'", vUser.SirketKodu, ID)).ToList();
            return json.Serialize(result);
        }
        #region Delete
        /// <summary>
        /// transfer sil
        /// </summary>
        public JsonResult Delete(int ID)
        {
            if (CheckPerm(Perms.Transfer, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result();
            try
            {
                db.DeleteTransfer(ID);
                _Result.Status = true; _Result.Id = ID;
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Transfer/Delete");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// transfer detay sil
        /// </summary>
        public JsonResult Delete2(int ID)
        {
            if (CheckPerm(Perms.Transfer, PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result();
            var satir1 = db.Transfer_Detay.Where(m => m.ID == ID).FirstOrDefault();
            var satir2 = db.IRS_Detay.Where(m => m.KynkSiparisID == ID && m.KynkSiparisTarih == satir1.TransferID).FirstOrDefault();
            var satir3 = db.GorevYers.Where(m => m.GorevID == satir1.Transfer.GorevID).ToList();
            db.Transfer_Detay.Remove(satir1);
            db.IRS_Detay.Remove(satir2);
            db.GorevYers.RemoveRange(satir3);
            try
            {
                db.SaveChanges();
                _Result = new Result(true, ID);
            }
            catch (Exception ex)
            {
                Logger(ex, "WMS/Transfer/Delete2");
                _Result.Status = false;
                _Result.Message = ex.Message;
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}