using System;
using System.Linq;
using Wms12m.Entity.Models;

namespace Wms12m
{
    /// <summary>
    /// TODO: bu silinecek
    /// </summary>
    public static class DbExtensions
    {
        /// <summary>
        /// TODO: bu silinecek
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
        /// TODO: bu silinecek
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
        /// TODO: bu silinecek
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
        /// TODO: bu silinecek
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
    }
}