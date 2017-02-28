using Excel;
using System;
using System.IO;
using System.Web;
using System.Data;
using System.Linq;
using Wms12m.Entity;
using System.Web.Mvc;
using Wms12m.Business;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class UploadsController : RootController
    {
        /// <summary>
        /// irsaliye için malzeme girişi yapar
        /// </summary>
        public JsonResult Irsaliye(int IrsNo, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0) return null;
            Stream stream = file.InputStream;
            IExcelDataReader reader;
            Result _Result = null;
            //dosya tipini bul
            if (file.FileName.EndsWith(".xls"))
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            else if (file.FileName.EndsWith(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else
                return null;
            //ilk satır başlık
            reader.IsFirstRowAsColumnNames = true;
            DataSet result = reader.AsDataSet();
            if (result.Tables[0].Rows != null)
            {
                var irsaliye = db.WMS_IRS.Where(m => m.ID == IrsNo).FirstOrDefault();
                using (DinamikModelContext Dinamik = new DinamikModelContext(irsaliye.SirketKod))
                {
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = result.Tables[0].Rows[0];
                        string malkodu = Dinamik.Context.TTies.Where(m => m.SatMalKodu == dr["MalKodu"].ToString() && m.Chk == irsaliye.HesapKodu).Select(m => m.MalKodu).FirstOrDefault();
                        //add new
                        try
                        {
                            WMS_STI sti = new WMS_STI();
                            sti.IrsaliyeID = IrsNo;
                            sti.MalKodu = malkodu;
                            sti.Miktar = Convert.ToDecimal(dr["Miktar"]);
                            sti.Birim = dr["Birim"].ToString();
                            Stok tmpTable = new Stok();
                            _Result = tmpTable.Operation(sti);
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
            }
            reader.Close();
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}