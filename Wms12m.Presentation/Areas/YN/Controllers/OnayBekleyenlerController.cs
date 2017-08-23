using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.YN.Controllers
{
    public class OnayBekleyenlerController : RootController
    {
        /// <summary>
        /// sipariş onayı bekleyenler sayfası
        /// </summary>
        public ActionResult Siparis()
        {
            return View("Siparis");
        }
        /// <summary>
        /// sipariş onayı bekleyenler listesi
        /// </summary>
        public string Siparis_List()
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnaySiparisList>(string.Format(@"SELECT        YNS{0}.STK002.STK002_EvrakSeriNo AS EvrakSeriNo, YNS{0}.STK002.STK002_CariHesapKodu AS HesapKodu, YNS{0}.CAR002.CAR002_Unvan1 AS Unvan, COUNT(YNS{0}.STK002.STK002_MalKodu) 
                                                                                             AS Cesit, SUM(YNS{0}.STK002.STK002_Miktari) AS Miktar, YNS{0}.STK002.STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), CAST(YNS{0}.STK002.STK002_GirenTarih - 2 AS datetime), 104) 
                                                                                             AS Tarih
                                                                    FROM            YNS{0}.STK002 INNER JOIN
                                                                                             YNS{0}.CAR002 ON YNS{0}.STK002.STK002_CariHesapKodu = YNS{0}.CAR002.CAR002_HesapKodu
                                                                    WHERE        (YNS{0}.STK002.STK002_GC = 1) AND (YNS{0}.STK002.STK002_SipDurumu = 0)
                                                                    GROUP BY YNS{0}.STK002.STK002_CariHesapKodu, YNS{0}.CAR002.CAR002_Unvan1, YNS{0}.STK002.STK002_EvrakSeriNo, YNS{0}.STK002.STK002_GirenKodu, CONVERT(VARCHAR(15), 
                                                                                             CAST(YNS{0}.STK002.STK002_GirenTarih - 2 AS datetime), 104)", "0TEST")).ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public PartialViewResult Siparis_Details(string ID)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnaySiparisList>(string.Format(@"SELECT        YNS{0}.STK002.STK002_MalKodu AS MalKodu, YNS{0}.STK004.STK004_Aciklama AS MalAdi, YNS{0}.STK002.STK002_CariHesapKodu AS HesapKodu, YNS{0}.CAR002.CAR002_Unvan1 AS Unvan, 
                                                                                            YNS{0}.STK002.STK002_EvrakSeriNo AS EvrakSeriNo, YNS{0}.STK002.STK002_Depo AS Depo, YNS{0}.STK002.STK002_Miktari AS Miktar, YNS{0}.STK002.STK002_BirimFiyati AS BirimFiyat, 
                                                                                            YNS{0}.STK002.STK002_Tutari AS Tutar, YNS{0}.STK002.STK002_DovizCinsi AS DovizCinsi, YNS{0}.STK002.STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), CAST(YNS{0}.STK002.STK002_GirenTarih - 2 AS datetime), 104) AS Tarih
                                                                FROM            YNS{0}.STK002 INNER JOIN
                                                                                            YNS{0}.CAR002 ON YNS{0}.STK002.STK002_CariHesapKodu = YNS{0}.CAR002.CAR002_HesapKodu INNER JOIN
                                                                                            YNS{0}.STK004 ON YNS{0}.STK002.STK002_MalKodu = YNS{0}.STK004.STK004_MalKodu
                                                                WHERE        (YNS{0}.STK002.STK002_GC = 1) AND (YNS{0}.STK002.STK002_SipDurumu = 0) AND YNS{0}.STK002.STK002_EvrakSeriNo = '{1}'", "0TEST", ID)).ToList();
                return PartialView("Siparis_Details", list);
            }
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Siparis_Onay(string ID, bool Onay)
        {
            ID = ID.RemoveLastCharacter();
            ID = "'" + ID.Replace("#", "','") + "'";
            try
            {
                using (YNSEntities dby = new YNSEntities())
                {
                    if (Onay == true)
                    {

                    }
                    else
                    {

                    }
                }
                return Json(new Result(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Result(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// transfer onayı bekleyenler sayfası
        /// </summary>
        public ActionResult Transfer()
        {
            return View("Transfer");
        }
        /// <summary>
        /// transfer onayı bekleyenler listesi
        /// </summary>
        public string Transfer_List()
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayTransferList>(string.Format(@"SELECT        TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104) AS Tarih
                                                                    FROM            YNS{0}.TransferDepo
                                                                    WHERE        (OnayDurumu = 0)
                                                                    GROUP BY TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104)", "0TEST")).ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public JsonResult Transfer_Details(string ID)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnayTransferList>(string.Format(@"SELECT        YNS{0}.TransferDepo.ID, YNS{0}.TransferDepo.TransferNo, YNS{0}.TransferDepo.TransferTarihi, YNS{0}.TransferDepo.SiraNo, YNS{0}.TransferDepo.MalKodu, YNS{0}.STK004.STK004_Aciklama AS MalAdi,
                                                                                                  YNS{0}.TransferDepo.Miktar, YNS{0}.TransferDepo.Birim, YNS{0}.TransferDepo.CikisDepo, YNS{0}.TransferDepo.GirisDepo, YNS{0}.TransferDepo.TalepEden, YNS{0}.TransferDepo.OnayDurumu, 
                                                                                                 YNS{0}.TransferDepo.Kaydeden, YNS{0}.TransferDepo.KayitTarih
                                                                        FROM            YNS{0}.TransferDepo INNER JOIN
                                                                                                 YNS{0}.STK004 ON YNS{0}.TransferDepo.MalKodu = YNS{0}.STK004.STK004_MalKodu
                                                                        WHERE        YNS{0}.TransferDepo.TransferNo = '{1}'", "0TEST", ID)).ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Transfer_Onay(string ID, bool Onay)
        {
            ID = ID.RemoveLastCharacter();
            ID = "'" + ID.Replace("#", "','") + "'";
            try
            {
                using (YNSEntities dby = new YNSEntities())
                {
                    if (Onay == true)
                    {
                        dby.Database.ExecuteSqlCommand(string.Format(@"UPDATE YNS{0}.TransferDepo SET OnayDurumu = 1 WHERE (TransferNo IN ({1}))", "0TEST", ID));
                    }
                    else
                    {
                        dby.Database.ExecuteSqlCommand(string.Format(@"UPDATE YNS{0}.TransferDepo SET OnayDurumu = 2 WHERE (TransferNo IN ({1}))", "0TEST", ID));
                    }
                }
                return Json(new Result(true), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new Result(false, ex.Message), JsonRequestBehavior.AllowGet);
            }
        }
    }
}