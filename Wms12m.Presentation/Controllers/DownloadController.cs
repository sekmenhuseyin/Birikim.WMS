using System.Linq;
using System.Web.UI.WebControls;
using Wms12m.Entity;

namespace Wms12m.Presentation.Controllers
{
    public class DownloadController : RootController
    {
        /// <summary>
        /// stok hareketlerini excel aktar
        /// </summary>
        public void StockHistory(string malkodu, int depoID)
        {
            //define
            string depoKodu = Store.Detail(depoID).DepoKodu;
            //listeyi getir
            string sql = string.Format("SELECT CASE WHEN GC = 0 THEN 'Girdi' ELSE 'Çıktı' END AS islem, wms.fnFormatDateFromInt(wms.Yer_Log.KayitTarihi) AS Tarih, wms.fnFormatTimeFromInt(wms.Yer_Log.KayitSaati) AS Saat, " +
                "wms.IRS.EvrakNo, wms.Yer_Log.HucreAd, wms.Yer_Log.MalKodu, wms.Yer_Log.Birim, FORMAT(wms.Yer_Log.Miktar, 'N', 'tr-TR') AS Miktar, FORMAT(wms.fnGetStock('{0}', wms.Yer_Log.MalKodu, wms.Yer_Log.Birim), 'N', 'tr-TR') AS Stok " +
                "FROM wms.Yer_Log LEFT OUTER JOIN wms.IRS ON wms.Yer_Log.IrsaliyeID = wms.IRS.ID " +
                "WHERE (wms.Yer_Log.DepoID = {1}) AND (wms.Yer_Log.MalKodu = '{2}') " +
                "ORDER BY wms.Yer_Log.KayitTarihi, wms.Yer_Log.KayitSaati", depoKodu, depoID, malkodu);
            var list = db.Database.SqlQuery<downStockHistory>(sql).ToList();
            //export
            Export export = new Export();
            export.ToExcel(Response, list, depoKodu + " depoda " + malkodu + "için Stok Hareketleri");
        }
        /// <summary>
        /// stoğu aktar
        /// </summary>
        public void Stock(int KatID)
        {
            //listeyi getir
            string sql = string.Format("SELECT HucreAd, MalKodu, Birim, FORMAT(Miktar, 'N', 'tr-TR') AS Miktar FROM wms.Yer WHERE (KatID = {0})", KatID);
            var list = db.Database.SqlQuery<downStock>(sql).ToList();
            //export
            Export export = new Export();
            export.ToExcel(Response, list, "Stok");
        }
    }
}