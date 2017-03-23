using System;
using System.Linq;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public static class DbExtensions
    {

        public static string GetMalAdi(this string value, string SirketKodu)
        {
            try
            {
                using (WMSEntities db = new WMSEntities())
                {
                    string sql = String.Format("SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.stk WHERE Malkodu='{1}'", SirketKodu, value);
                    return db.Database.SqlQuery<String>(sql).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
