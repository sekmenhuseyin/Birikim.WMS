﻿using System.Linq;
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
                var list = dby.Database.SqlQuery<frmOnaySiparisList>(@"SELECT        YNS0TEST.STK002.STK002_EvrakSeriNo AS EvrakSeriNo, YNS0TEST.STK002.STK002_CariHesapKodu AS HesapKodu, YNS0TEST.CAR002.CAR002_Unvan1 AS Unvan, COUNT(YNS0TEST.STK002.STK002_MalKodu) 
                                                                                             AS Cesit, SUM(YNS0TEST.STK002.STK002_Miktari) AS Miktar, YNS0TEST.STK002.STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), CAST(YNS0TEST.STK002.STK002_GirenTarih - 2 AS datetime), 104) 
                                                                                             AS Tarih
                                                                    FROM            YNS0TEST.STK002 INNER JOIN
                                                                                             YNS0TEST.CAR002 ON YNS0TEST.STK002.STK002_CariHesapKodu = YNS0TEST.CAR002.CAR002_HesapKodu
                                                                    WHERE        (YNS0TEST.STK002.STK002_GC = 1) AND (YNS0TEST.STK002.STK002_SipDurumu = 0)
                                                                    GROUP BY YNS0TEST.STK002.STK002_CariHesapKodu, YNS0TEST.CAR002.CAR002_Unvan1, YNS0TEST.STK002.STK002_EvrakSeriNo, YNS0TEST.STK002.STK002_GirenKodu, CONVERT(VARCHAR(15), 
                                                                                             CAST(YNS0TEST.STK002.STK002_GirenTarih - 2 AS datetime), 104)").ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
        /// <summary>
        /// liste detayı
        /// </summary>
        [HttpPost]
        public JsonResult Siparis_Details(string ID)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnaySiparisList>(@"SELECT        YNS0TEST.STK002.STK002_MalKodu AS MalKodu, YNS0TEST.STK002.STK002_CariHesapKodu AS HesapKodu, YNS0TEST.CAR002.CAR002_Unvan1 AS Unvan, 
                                                                                            YNS0TEST.STK002.STK002_EvrakSeriNo AS EvrakSeriNo, YNS0TEST.STK002.STK002_Depo AS Depo, YNS0TEST.STK002.STK002_Miktari AS Miktar, YNS0TEST.STK002.STK002_BirimFiyati AS BirimFiyat, 
                                                                                            YNS0TEST.STK002.STK002_Tutari AS Tutar, YNS0TEST.STK002.STK002_DovizCinsi AS DovizCinsi, YNS0TEST.STK002.STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), CAST(YNS0TEST.STK002.STK002_GirenTarih - 2 AS datetime), 104) AS Tarih
                                                                FROM            YNS0TEST.STK002 INNER JOIN
                                                                                            YNS0TEST.CAR002 ON YNS0TEST.STK002.STK002_CariHesapKodu = YNS0TEST.CAR002.CAR002_HesapKodu
                                                                WHERE        (YNS0TEST.STK002.STK002_GC = 1) AND (YNS0TEST.STK002.STK002_SipDurumu = 0) AND YNS0TEST.STK002.STK002_EvrakSeriNo = '" + ID + "'").ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Siparis_Onay(string ID, bool Onay)
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
                var list = dby.Database.SqlQuery<frmOnayTransferList>(@"SELECT        TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104) AS Tarih
                                                                    FROM            YNS0TEST.TransferDepo
                                                                    WHERE        (OnayDurumu = 0)
                                                                    GROUP BY TransferNo, Kaydeden, CONVERT(VARCHAR(15), CAST(TransferTarihi - 2 AS datetime), 104)").ToList();
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
                var list = dby.Database.SqlQuery<frmOnayTransferList>(@"SELECT        ID, TransferNo, TransferTarihi, SiraNo, MalKodu, Miktar, Birim, CikisDepo, GirisDepo, TalepEden, OnayDurumu, Kaydeden, KayitTarih
                                                                    FROM            YNS0TEST.TransferDepo
                                                                    WHERE        TransferNo = '" + ID + "'").ToList();
                return Json(list, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// onayla veya reddet
        /// </summary>
        [HttpPost]
        public JsonResult Transfer_Onay(string ID, bool Onay)
        {
            using (YNSEntities dby = new YNSEntities())
            {
                if (Onay == true)
                {
                    db.Database.ExecuteSqlCommand(@"UPDATE YNS0TEST.TransferDepo SET OnayDurumu = 1 WHERE (TransferNo = '" + ID + "')");
                }
                else
                {
                    db.Database.ExecuteSqlCommand(@"UPDATE YNS0TEST.TransferDepo SET OnayDurumu = 2 WHERE (TransferNo = '" + ID + "')");
                }
            }
            return Json(new Result(true), JsonRequestBehavior.AllowGet);
        }
    }
}