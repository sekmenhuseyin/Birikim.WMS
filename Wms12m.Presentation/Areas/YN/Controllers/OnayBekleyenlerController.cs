using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Areas.YN.Controllers
{
    public class OnayBekleyenlerController : RootController
    {
        // GET: YN/OnayBekleyenler
        public ActionResult Index()
        {
            return View("Index");
        }
        /// <summary>
        /// stok malzeme sil
        /// </summary>
        public string SiparisOnay_List()
        {
            using (YNSEntities dby = new YNSEntities())
            {
                var list = dby.Database.SqlQuery<frmOnaySiparisList>(@"SELECT        YNS0TEST.STK002.STK002_Row_ID AS ID, YNS0TEST.STK002.STK002_MalKodu AS MalKodu, YNS0TEST.STK002.STK002_CariHesapKodu AS HesapKodu, YNS0TEST.CAR002.CAR002_Unvan1 AS Unvan, 
                                                                                            YNS0TEST.STK002.STK002_EvrakSeriNo AS EvrakSeriNo, YNS0TEST.STK002.STK002_Depo AS Depo, YNS0TEST.STK002.STK002_Miktari AS Miktar, YNS0TEST.STK002.STK002_BirimFiyati AS BirimFiyat, 
                                                                                            YNS0TEST.STK002.STK002_Tutari AS Tutar, YNS0TEST.STK002.STK002_DovizCinsi AS DovizCinsi, YNS0TEST.STK002.STK002_GirenKodu AS Kaydeden, CONVERT(VARCHAR(15), CAST(YNS0TEST.STK002.STK002_GirenTarih - 2 AS datetime), 104) AS Tarih
                                                                FROM            YNS0TEST.STK002 INNER JOIN
                                                                                            YNS0TEST.CAR002 ON YNS0TEST.STK002.STK002_CariHesapKodu = YNS0TEST.CAR002.CAR002_HesapKodu
                                                                WHERE        (YNS0TEST.STK002.STK002_GC = 1) AND (YNS0TEST.STK002.STK002_SipDurumu = 0)").ToList();
                var json = new JavaScriptSerializer().Serialize(list);
                return json;
            }
        }
    }
}