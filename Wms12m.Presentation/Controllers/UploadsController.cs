using Excel;
using System;
using System.IO;
using System.Web;
using System.Data;
using System.Linq;
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
        public JsonResult Irsaliye(int IrsNo, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0 || IrsNo==0) return Json(false, JsonRequestBehavior.AllowGet);
            //gelen dosyayı oku
            Stream stream = file.InputStream;
            IExcelDataReader reader;
            //dosya tipini bul
            if (file.FileName.EndsWith(".xls"))
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            else if (file.FileName.EndsWith(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
            //ilk satır başlık
            reader.IsFirstRowAsColumnNames = true;
            //exceldeki bilgileri datasete aktar
            DataSet result = reader.AsDataSet();
            //kontorl
            if (result.Tables.Count == 0) return Json(false, JsonRequestBehavior.AllowGet);
            if (result.Tables[0].Rows == null) return Json(false, JsonRequestBehavior.AllowGet);
            //irsaliye no bul
            var irsaliye = db.WMS_IRS.Where(m => m.ID == IrsNo).FirstOrDefault();
            //sti listesi oluşturuyoruz. tüm bilgiyi tek seferde veritabanına kaydetçek
            List<WMS_STI> liste = new List<WMS_STI>();
            //finsat işlemleri
            using (DinamikModelContext Dinamik = new DinamikModelContext(irsaliye.SirketKod))
            {
                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = result.Tables[0].Rows[i];
                    string tmp = dr["MalKodu"].ToString();
                    //satıcı malkodundan malkodunu getir
                    string malkodu = Dinamik.Context.TTies.Where(m => m.SatMalKodu == tmp && m.Chk == irsaliye.HesapKodu).Select(m => m.MalKodu).FirstOrDefault();
                    //kontrol
                    if (dr["Birim"].ToString() == "") return Json(false, JsonRequestBehavior.AllowGet);
                    if (dr["Miktar"].ToString2().IsNumeric() == false) return Json(false, JsonRequestBehavior.AllowGet);
                    if (malkodu == "" || malkodu == null) return Json(false, JsonRequestBehavior.AllowGet);
                    //add new
                    try
                    {
                        WMS_STI sti = new WMS_STI();
                        sti.IrsaliyeID = IrsNo;
                        sti.MalKodu = malkodu;
                        sti.Miktar = Convert.ToDecimal(dr["Miktar"]);
                        sti.Birim = dr["Birim"].ToString();
                        //ekle
                        liste.Add(sti);
                    }
                    catch (Exception)
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                //buraya kadar hata yoksa bunu yapar. yine de hata olursa hiçbirini kaydetmez...
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.WMS_STI.AddRange(liste);
                        db.SaveChanges();
                        dbContextTransaction.Commit();
                    }catch (Exception){}
                }
            }
            reader.Close();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}