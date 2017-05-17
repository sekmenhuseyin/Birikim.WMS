using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Wms12m.Entity;
using Wms12m.Entity.Models;
using Wms12m.Entity.Mysql;

namespace Wms12m
{
    public static class DbExtensions
    {
        /// <summary>
        /// get finsat stk maladi from malkodu
        /// </summary>
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
        /// <summary>
        /// gets finsat chk unvan from hesapkodu
        /// </summary>
        public static string GetUnvan(this string value, string SirketKodu)
        {
            try
            {
                using (WMSEntities db = new WMSEntities())
                {
                    string sql = String.Format("SELECT Unvan1+' '+Unvan2 as Unvan FROM FINSAT6{0}.FINSAT6{0}.chk WHERE HesapKodu='{1}'", SirketKodu, value);
                    return db.Database.SqlQuery<String>(sql).FirstOrDefault();
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
            string sql = string.Format("select MalAdi4, Nesne2, Kod15 FROM FINSAT6{0}.FINSAT6{0}.STK WHERE MalKodu='{1}'", SirketKodu, value);
            var satir = new frmCableStk();
            decimal sonuc = 0;
            //get stk details
            using (WMSEntities db = new WMSEntities())
            {
                satir = db.Database.SqlQuery<frmCableStk>(sql).FirstOrDefault();
            }
            if (satir == null) return sonuc;
            //get stok
            using (KabloEntities dbx = new KabloEntities())
            {
                string DepoAd = dbx.depoes.Where(m => m.id == KabloDepoID).Select(m => m.depo1).FirstOrDefault();
                var stok = dbx.kblstoks.Where(m => m.marka == satir.MalAdi4 && m.cins == satir.Nesne2 && m.kesit == satir.Kod15 && m.depo == DepoAd);
                var tmp = stok.Select(m => m.miktar).Sum();
                if (tmp != null)
                    sonuc = tmp.Value;
            }
            return sonuc;
        }
    }
    /// <summary>
    /// export to excel
    /// </summary>
    public class Export
    {
        public void ToExcel(HttpResponseBase Response, object clientsList, string fileName)
        {
            var grid = new GridView();
            grid.DataSource = clientsList;
            grid.DataBind();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename="+ fileName + ".xls");
            Response.ContentType = "application/excel";
            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                grid.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}
