using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m
{
    /// <summary>
    /// statik extensions that require db usage
    /// </summary>
    public static class DbExtensions
    {
        /// <summary>
        /// görev listesindeki kaynak evrak noları gösterir
        /// </summary>
        public static string GetEvrakNosForGorev(this int gorevID)
        {
            using (WMSEntities db = new WMSEntities())
            {
                var sonuc = db.GetKynkEvrakNosForGorev(gorevID).FirstOrDefault();
                if (sonuc == null || sonuc == "") sonuc = "";
                else sonuc = sonuc = sonuc.Substring(0, sonuc.Length - 1);
                return sonuc;
            }
        }
        /// <summary>
        /// görev listesindeki kaynak evrak tarihlerini gösterir
        /// </summary>
        public static string GetEvrakTarihsForGorev(this int GorevID)
        {
            using (WMSEntities db = new WMSEntities())
            {
                var sonuc = db.GetKynkTarihsForGorev(GorevID).FirstOrDefault();
                if (sonuc == null || sonuc == "") sonuc = "";
                else sonuc = sonuc = sonuc.Substring(0, sonuc.Length - 1);
                return sonuc;
            }
        }
        /// <summary>
        /// get finsat stk maladi from malkodu
        /// </summary>
        public static string GetMalAdi(this string value, string SirketKodu)
        {
            try
            {
                using (WMSEntities db = new WMSEntities())
                {
                    var sql = string.Format("SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.stk WHERE Malkodu='{1}'", SirketKodu, value);
                    return db.Database.SqlQuery<string>(sql).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// gets finsat chk unvan from hesapkodu
        /// </summary>
        public static string GetUnvan(this string value, string SirketKodu)
        {
            try
            {
                using (WMSEntities db = new WMSEntities())
                {
                    var sql = string.Format("SELECT Unvan1+' '+Unvan2 as Unvan FROM FINSAT6{0}.FINSAT6{0}.chk WHERE HesapKodu='{1}'", SirketKodu, value);
                    return db.Database.SqlQuery<string>(sql).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// malkoduna göre kablo stoğunu getirir
        /// </summary>
        public static decimal GetKabloStok(this string value, string SirketKodu, int KabloDepoID)
        {
            var sql = string.Format("select MalAdi4 as Marka, Nesne2 as Cins, Kod15 as Kesit FROM FINSAT6{0}.FINSAT6{0}.STK WHERE MalKodu='{1}'", SirketKodu, value);
            var satir = new frmCableStk();
            decimal sonuc = 0;
            // get stk details
            using (WMSEntities db = new WMSEntities())
                satir = db.Database.SqlQuery<frmCableStk>(sql).FirstOrDefault();


            if (satir == null) return sonuc;
            // get stok
            using (KabloEntities dbx = new KabloEntities())
            {
                var DepoAd = dbx.depoes.Where(m => m.id == KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                var stok = dbx.kblstoks.Where(m => m.marka == satir.Marka && m.cins == satir.Cins && m.kesit == satir.Kesit && m.depo == DepoAd);
                var tmp = stok.Select(m => m.miktar).Sum();
                if (tmp != null) sonuc = tmp.Value;
            }

            return sonuc;
        }
        /// <summary>
        /// gets finsat chk unvan from hesapkodu
        /// </summary>
        public static string GetDepoKod(this int? value, string SirketKodu)
        {
            try
            {
                using (WMSEntities db = new WMSEntities())
                {
                    var sql = string.Format("SELECT DepoKodu FROM BIRIKIM.wms.Depo WHERE ID='{0}'", value);
                    return db.Database.SqlQuery<string>(sql).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}