using Excel;
using System;
using System.IO;
using System.Web;
using System.Data;
using System.Web.Mvc;
using Wms12m.Entity.Models;
using System.Collections.Generic;


namespace Wms12m.Presentation.Controllers
{
    public class UploadsController : RootController
    {
        /// <summary>
        /// irsaliye için malzeme girişi yapar
        /// </summary>
        public JsonResult Irsaliye(int IrsNo,HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                List<WMS_STI> list_sti = new List<WMS_STI>();
                Stream stream = file.InputStream;
                IExcelDataReader reader = null;
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
                try
                {
                    if (result.Tables[0].Rows != null)
                    {
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = result.Tables[0].Rows[0];
                            var sti = new WMS_STI();
                            sti.IrsaliyeID = IrsNo;
                            sti.MalKodu = dr["MalKodu"].ToString();
                            sti.Miktar = Convert.ToDecimal(dr["Miktar"]);
                            sti.Birim = dr["Birim"].ToString();
                            db.WMS_STI.Add(sti);
                            db.SaveChanges();
                            //list_sti.Add(sti);
                        }
                        //db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                reader.Close();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}