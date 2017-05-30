using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class BuyController : RootController
    {
        /// <summary>
        /// irsaliye sayfası
        /// </summary>
        public ActionResult Index()
        {
            if (CheckPerm("Mal Kabul", PermTypes.Reading) == false) return Redirect("/");
            ViewBag.SirketID = new SelectList(db.GetSirkets().ToList(), "Kod", "Ad");
            ViewBag.DepoID = new SelectList(Store.GetList(vUser.DepoId), "ID", "DepoAd");
            return View("Index", new frmIrsaliye());
        }
        /// <summary>
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult List()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0,0,") return null;
            string[] tmp = id.ToString().Split(',');
            if (tmp.Length != 3) return null;
            //kontrol
            if (CheckPerm("Mal Kabul", PermTypes.Reading) == false) return null;
            //get liste
            var list = Irsaliye.GetList(tmp[0], false, tmp[2], tmp[1].ToInt32());
            ViewBag.id = id;
            ViewBag.Yetki = CheckPerm("Mal Kabul", PermTypes.Writing);
            ViewBag.Yetki2 = CheckPerm("Mal Kabul", PermTypes.Deleting);
            return PartialView("List", list);
        }
        /// <summary>
        /// irsaliye evrak no günceller
        /// </summary>
        public JsonResult Update(string EvrakNo, int ID,string SirketID, string HesapKodu)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = new Result(false, "Bu evrak no kullanılıyor");
            //length kontrol
            if (EvrakNo.Length > 16)
            {
                _Result.Message = "Bu evrak no çok uzun.";
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            //birikim db kontrol
            var kontrol = db.IRS.Where(m => m.IslemTur == false && m.EvrakNo == EvrakNo && m.SirketKod == SirketID & m.HesapKodu == HesapKodu && m.ID != ID).FirstOrDefault();
            if (kontrol != null)
                return Json(_Result, JsonRequestBehavior.AllowGet);
            //finsta db kontrol
            string sql = string.Format("SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 3) AND (Chk = {2})", SirketID, EvrakNo, HesapKodu);
            var sti = db.Database.SqlQuery<string>(sql).FirstOrDefault();
            if (sti != null)
                return Json(_Result, JsonRequestBehavior.AllowGet);
            //if all correct update
            var tbl = Irsaliye.Detail(ID);
            tbl.EvrakNo = EvrakNo;
            _Result = Irsaliye.Operation(tbl);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// irsaliye listesi
        /// </summary>
        public PartialViewResult SiparisList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null || id.ToString2() == "0,0,") return null;
            string[] tmp = id.ToString().Split(',');
            if (tmp.Length != 3) return null;
            //kontrol
            if (CheckPerm("Mal Kabul", PermTypes.Reading) == false) return null;
            //get list
            string depo = Store.Detail(tmp[1].ToInt32()).DepoKodu;
            string sql = String.Format("SELECT FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID AS ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.STK.MalAdi, FINSAT6{0}.FINSAT6{0}.STK.MalKodu, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.TeslimMiktar - FINSAT6{0}.FINSAT6{0}.SPI.KapatilanMiktar AS AçıkMiktar, FINSAT6{0}.FINSAT6{0}.SPI.Birim " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = FINSAT6{0}.FINSAT6{0}.CHK.HesapKodu " +
                                        "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.IslemTur = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.SiparisDurumu = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 63) AND (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Chk = '{2}') " +
                                        "AND FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID NOT IN (SELECT ISNULL(KynkSiparisID,0) FROM wms.IRS_Detay GROUP BY ISNULL(KynkSiparisID,0))", tmp[0], depo, tmp[2]);
            var list = db.Database.SqlQuery<frmSiparistenGelen>(sql).ToList();
            return PartialView("SiparisList", list);
        }
        /// <summary>
        /// siparişten malzeme ekler
        /// </summary>
        [HttpPost]
        public PartialViewResult FromSiparis(string s, string id, string ids)
        {
            if (s == null || id == null || ids == null) return null;
            if (CheckPerm("Mal Kabul", PermTypes.Writing) == false) return null;
            int irsaliyeID = id.ToInt32(), eklenen = 0, sira = 0;
            //split ids into rows
            ids = ids.Left(ids.Length - 1);
            string[] tmp = ids.Split('#');
            var satir = new IRS_Detay();
            var tbl = new List<IRS_Detay>();
            foreach (var item in tmp)
            {
                if (sira == 0)
                {
                    //evrak no ekle
                    satir.KynkSiparisNo = item;
                    sira++;
                }
                else if (sira == 1)
                {
                    //malkodu ekle
                    satir.MalKodu = item;
                    sira++;
                }
                else if (sira == 2)
                {
                    //birim ekle
                    satir.Birim = item;
                    sira++;
                }
                else if (sira == 3)
                {
                    //row id ekle
                    satir.KynkSiparisID = item.ToInt32();
                    sira++;
                }
                else
                {
                    //miktar ekle
                    satir.Miktar = item.ToDecimal();
                    tbl.Add(satir);
                    satir = new IRS_Detay();
                    sira = 0;
                }
            }
            //sadece irsaliye daha onaylanmamışsa yani işlemleri bitmeişse ekle
            var irs = Irsaliye.Detail(irsaliyeID);
            if (irs.Onay == false)
            {
                foreach (var item in tbl)
                {
                    string sql = String.Format("SELECT EvrakNo, Tarih, SiraNo, MalKodu, BirimMiktar, (BirimMiktar - TeslimMiktar - KapatilanMiktar) AS Miktar, Birim FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) WHERE (ROW_ID = {1}) AND (IslemTur = 0) AND (KynkEvrakTip = 63) AND (SiparisDurumu = 0) AND (MalKodu = '{2}') AND (Birim = '{3}') AND (EvrakNo = '{4}')", s, item.KynkSiparisID, item.MalKodu, item.Birim, item.KynkSiparisNo);
                    try
                    {
                        var tempTbl = db.Database.SqlQuery<frmIrsaliyeMalzeme>(sql).FirstOrDefault();
                        //save details
                        IRS_Detay sti = new IRS_Detay()
                        {
                            IrsaliyeID = irsaliyeID,
                            Birim = tempTbl.Birim,
                            KynkSiparisMiktar = tempTbl.BirimMiktar,
                            KynkSiparisID = item.KynkSiparisID,
                            KynkSiparisNo = tempTbl.EvrakNo,
                            KynkSiparisSiraNo = tempTbl.SiraNo,
                            KynkSiparisTarih = tempTbl.Tarih,
                            MalKodu = tempTbl.MalKodu,
                            Miktar = item.Miktar > 0 ? item.Miktar : tempTbl.Miktar
                        };
                        Result _Result = IrsaliyeDetay.Operation(sti);
                        eklenen++;
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "Buy/FromSiparis");
                    }
                }
                if (eklenen > 0)
                {
                    //set aktif
                    var grv = db.Gorevs.Where(m => m.IrsaliyeID == irsaliyeID).FirstOrDefault();
                    if (grv.DurumID == ComboItems.Başlamamış.ToInt32())
                    {
                        grv.DurumID = ComboItems.Açık.ToInt32();
                        grv.OlusturmaTarihi = fn.ToOADate();
                        grv.OlusturmaSaati = fn.ToOATime();
                        db.SaveChanges();
                    }
                }
            }
            //get list
            var list = IrsaliyeDetay.GetList(irsaliyeID);
            ViewBag.IrsaliyeId = irsaliyeID;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = irs.SirketKod;
            ViewBag.Yetki = true;
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// yeni irsaliye fatura kaydeder
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult New(frmIrsaliye tbl)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Reading) == false)
            {
                ViewBag.message = "Burası için izniniz yok";
                return PartialView("_GridPartial", new List<IRS_Detay>());
            }
            if (tbl.EvrakNo.Length > 16)
            {
                ViewBag.message = "Bu evrak no çok uzun";
                return PartialView("_GridPartial", new List<IRS_Detay>());
            }
            //bool kontrol1 = DateTime.TryParse(tbl.Tarih, out DateTime tmpTarih);
            //if (kontrol1 == false)
            //{
            //    db.Logger(vUser.UserName, "", fn.GetIPAddress(), "Tarih hatası: " + tbl.Tarih, "", "Buy/New");
            //    ViewBag.message = "Tarih yanlış";
            //    return PartialView("_GridPartial", new List<IRS_Detay>());
            //}
            //int tarih = tmpTarih.ToOADateInt();
            int tarih = DateTime.Today.ToOADateInt();
            var kontrol2 = db.IRS.Where(m => m.IslemTur == false && m.EvrakNo == tbl.EvrakNo && m.SirketKod == tbl.SirketID & m.HesapKodu == tbl.HesapKodu).FirstOrDefault();
            //var olanı göster
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
                    ViewBag.SirketID = tbl.SirketID;
                    ViewBag.Yetki = CheckPerm("Mal Kabul", PermTypes.Writing);
                    return PartialView("_GridPartial", list);
                }
                catch (Exception ex)
                {
                    Logger(ex, "Buy/New-varolan");
                    return null;
                }
            }

            //kontrol
            if (CheckPerm("Mal Kabul", PermTypes.Writing) == false) return null;
            //yeni kayıtta evrak no spide olmayacak kontrolü
            string sql = string.Format("SELECT EvrakNo FROM FINSAT6{0}.FINSAT6{0}.STI WHERE (EvrakNo = '{1}') AND (KynkEvrakTip = 3) AND (Chk = {2})", tbl.SirketID, tbl.EvrakNo, tbl.HesapKodu);
            var sti = db.Database.SqlQuery<string>(sql).FirstOrDefault();
            if (sti != null)
            {
                ViewBag.message = "Bu evrak no kullanılıyor";
                return PartialView("_GridPartial", new List<IRS_Detay>());
            }
            //yeni kayıt
            string gorevno = db.SettingsGorevNo(DateTime.Today.ToOADateInt(), tbl.DepoID).FirstOrDefault();
            int today = fn.ToOADate();
            int time = fn.ToOATime();
            try
            {
                var cevap = db.InsertIrsaliye(tbl.SirketID, tbl.DepoID, gorevno, tbl.EvrakNo, tarih, "Irs: " + tbl.EvrakNo + ", Tedarikçi: " + tbl.Unvan, false, ComboItems.MalKabul.ToInt32(), vUser.UserName, today, time, tbl.HesapKodu, "", 0, "").FirstOrDefault();
                //get list
                var list = IrsaliyeDetay.GetList(cevap.IrsaliyeID.Value);
                ViewBag.IrsaliyeId = cevap.IrsaliyeID;
                ViewBag.Onay = false;
                ViewBag.SirketID = tbl.SirketID;
                ViewBag.Yetki = CheckPerm("Mal Kabul", PermTypes.Writing);
                return PartialView("_GridPartial", list);

            }
            catch (Exception ex)
            {
                Logger(ex, "Buy/New-yeni");
                return null;
            }
        }
        /// <summary>
        /// listeyi günceller
        /// </summary>
        public PartialViewResult GridPartial(int ID)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Reading) == false) return null;
            var list = IrsaliyeDetay.GetList(ID);
            var irs = Irsaliye.Detail(ID);
            ViewBag.IrsaliyeId = ID;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = irs.SirketKod;
            ViewBag.Yetki = CheckPerm("Mal Kabul", PermTypes.Writing);
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// yeni malzeme
        /// </summary>
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult InsertMalzeme(frmMalzeme tbl)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Writing) == false) return null;
            //sadece irsaliye daha onaylanmamışsa yani işlemleri bitmeişse ekle
            var irs = Irsaliye.Detail(tbl.IrsaliyeId);
            if (irs.Onay == false)
            {
                //add new
                Result _Result = IrsaliyeDetay.Insert(tbl);
                //set aktif
                var grv = db.Gorevs.Where(m => m.IrsaliyeID == tbl.IrsaliyeId).FirstOrDefault();
                if (grv.DurumID == ComboItems.Başlamamış.ToInt32())
                {
                    grv.DurumID = ComboItems.Açık.ToInt32();
                    grv.OlusturmaTarihi = fn.ToOADate();
                    grv.OlusturmaSaati = fn.ToOATime();
                    db.SaveChanges();
                }
            }
            //get list
            var list = IrsaliyeDetay.GetList(tbl.IrsaliyeId);
            ViewBag.IrsaliyeId = tbl.IrsaliyeId;
            ViewBag.Onay = irs.Onay;
            ViewBag.SirketID = irs.SirketKod;
            ViewBag.Yetki = true;
            return PartialView("_GridPartial", list);
        }
        /// <summary>
        /// malzeme autocomplete
        /// </summary>
        public JsonResult getMalzemebyCode(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = String.Format("FINSAT6{0}.[wms].[getMalzemeByCodeOrName] @MalKodu = N'{1}', @MalAdi = N''", id.ToString(), term);
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Buy/getMalzemebyCode");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult getMalzemebyName(string term)
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            if (id == null) return null;
            string sql = String.Format("FINSAT6{0}.[wms].[getMalzemeByCodeOrName] @MalKodu = N'', @MalAdi = N'{1}'", id.ToString(), term);
            try
            {
                var list = db.Database.SqlQuery<frmJson>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Buy/getMalzemebyName");
                return Json(new List<frmJson>(), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// malzeme koduna göre birim getirir
        /// </summary>
        [HttpPost]
        public JsonResult getBirim(string kod, string s)
        {
            string sql = String.Format("SELECT Birim1, Birim2, Birim3, Birim4 FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = '{1}')", s, kod);
            try
            {
                var list = db.Database.SqlQuery<frmBirims>(sql).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger(ex, "Buy/getBirim");
                return Json(new List<frmBirims>(), JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// anasayfadaki malzeme listesi
        /// </summary>
        public ActionResult GetHesapCodes(IDataTablesRequest request)
        {
            string SirketID = "";
            // Check additional parameters. You may perform custom actions with them.
            if (request.AdditionalParameters != null)
                foreach (var additionalParameter in request.AdditionalParameters)
                {
                    if (additionalParameter.Key == "SirketID") SirketID = additionalParameter.Value.ToString();
                }
            if (SirketID == "") SirketID = db.GetSirketDBs().FirstOrDefault();
            string sql = String.Format("FINSAT6{0}.[wms].[CHKSelectKartTip]", SirketID);
            var data = db.Database.SqlQuery<frmHesapUnvan>(sql).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item => _item.Unvan.Contains(request.Search.Value) || _item.HesapKodu.ToLower().Contains(request.Search.Value.ToLower()));

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// yeni malzeme satırı formu
        /// </summary>
        public PartialViewResult NewMalzemeForm(int id)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Writing) == false) return null;
            ViewBag.IrsaliyeId = id;
            return PartialView("_GridNewPartial", new frmMalzeme());
        }
        /// <summary>
        /// irsaliye sil
        /// </summary>
        public JsonResult Delete1(int ID)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = Irsaliye.Delete(ID);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// stok malzeme sil
        /// </summary>
        public JsonResult Delete2(int ID)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Deleting) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _Result = IrsaliyeDetay.Delete(ID);
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}