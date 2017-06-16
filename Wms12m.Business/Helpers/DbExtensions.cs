using System;
using System.Drawing;
using System.Drawing.Imaging;
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
    /// <summary>
    /// statik extensions that require db usage
    /// </summary>
    public static class DbExtensions
    {
        /// <summary>
        /// görev listesindeki kaynak evrak noları gösterir
        /// </summary>
        public static string GetEvrakNosForGorev(this int GorevID)
        {
            using (WMSEntities db = new WMSEntities())
            {
                var sonuc = db.GetKynkEvrakNosForGorev(GorevID).FirstOrDefault();
                if (sonuc == null) sonuc = "";
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
                if (sonuc == null) sonuc = "";
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
        /// <summary>
        /// Generates Barcode Image
        /// </summary>
        public static string GenerateBarcode(this string Code)
        {
            byte[] barcode;
            string BarCodeImage;
            Bitmap objBitmap = new Bitmap(Code.Length * 28, 100);
            using (Graphics graphic = Graphics.FromImage(objBitmap))
            {
                Font newFont = new Font("IDAutomationHC39M Free Version", 18, FontStyle.Regular);
                PointF point = new PointF(2, 2);
                SolidBrush balck = new SolidBrush(Color.Black);
                SolidBrush white = new SolidBrush(Color.White);
                graphic.FillRectangle(white, 0, 0, objBitmap.Width, objBitmap.Height);
                graphic.DrawString("*" + Code + "*", newFont, balck, point);
            }
            using (MemoryStream Mmst = new MemoryStream())
            {
                objBitmap.Save(Mmst, ImageFormat.Png);
                barcode = Mmst.GetBuffer();
                BarCodeImage = barcode != null ? "data:image/jpg;base64," + Convert.ToBase64String((byte[])barcode) : "";
                return BarCodeImage;
            }
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
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
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
