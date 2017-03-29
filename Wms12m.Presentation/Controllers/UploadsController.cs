using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity.Models;

namespace Wms12m.Presentation.Controllers
{
    public class UploadsController : RootController
    {
        /// <summary>
        /// irsaliye için malzeme girişi yapar
        /// </summary>
        public JsonResult irsaliye(int IrsNo, HttpPostedFileBase file)
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
            var irsaliye = db.IRS.Where(m => m.ID == IrsNo).FirstOrDefault();
            //sti listesi oluşturuyoruz. tüm bilgiyi tek seferde veritabanına kaydetçek
            List<IRS_Detay> liste = new List<IRS_Detay>();
            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
            {
                DataRow dr = result.Tables[0].Rows[i];
                //satıcı malkodundan malkodunu getir
                string malkodu = String.Format("SELECT MalKodu FROM FINSAT6{0}.FINSAT6{0}.TTY WITH(NOLOCK) WHERE (SatMalKodu = '{1}') AND (Chk = '{2}')", irsaliye.SirketKod, dr["MalKodu"].ToString(), irsaliye.HesapKodu);
                malkodu = db.Database.SqlQuery<string>(malkodu).FirstOrDefault();
                //kontrol
                if (dr["Birim"].ToString() == "") return Json(false, JsonRequestBehavior.AllowGet);
                if (dr["Miktar"].ToString2().IsNumeric() == false) return Json(false, JsonRequestBehavior.AllowGet);
                if (malkodu == "" || malkodu == null) return Json(false, JsonRequestBehavior.AllowGet);
                //add new
                try
                {
                    IRS_Detay sti = new IRS_Detay();
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
            reader.Close();
            //buraya kadar hata yoksa bunu yapar. yine de hata olursa hiçbirini kaydetmez...
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.IRS_Detay.AddRange(liste);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex){
                    db.Logger(vUser.UserName, Environment.MachineName, fn.GetIPAddress(), ex.Message, ex.InnerException.Message, "Uploads/irsaliye");
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// boyut kartı için toplu giriş yapar
        /// </summary>
        public JsonResult Olcu(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0) return Json(false, JsonRequestBehavior.AllowGet);
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
            //sti listesi oluşturuyoruz. tüm bilgiyi tek seferde veritabanına kaydetçek
            List<Olcu> liste = new List<Olcu>();
            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
            {
                DataRow dr = result.Tables[0].Rows[i];
                //kontrol
                if (dr["Mal Kodu"].ToString() == "") return Json(false, JsonRequestBehavior.AllowGet);
                if (dr["Birim"].ToString() == "") return Json(false, JsonRequestBehavior.AllowGet);
                if (dr["En"].ToString2().IsNumeric() == false) return Json(false, JsonRequestBehavior.AllowGet);
                if (dr["Boy"].ToString2().IsNumeric() == false) return Json(false, JsonRequestBehavior.AllowGet);
                if (dr["Derinlik"].ToString2().IsNumeric() == false) return Json(false, JsonRequestBehavior.AllowGet);
                if (dr["Ağırlık"].ToString2().IsNumeric() == false) return Json(false, JsonRequestBehavior.AllowGet);
                //add new
                try
                {
                    Olcu sti = new Olcu();
                    sti.MalKodu = dr["Mal Kodu"].ToString();
                    sti.Birim = dr["Birim"].ToString();
                    sti.En = dr["En"].ToDecimal();
                    sti.Boy = dr["Boy"].ToDecimal();
                    sti.Derinlik = dr["Derinlik"].ToDecimal();
                    sti.Agirlik = dr["Ağırlık"].ToDecimal();
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
                    db.Olcus.AddRange(liste);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    db.Logger(vUser.UserName, Environment.MachineName, fn.GetIPAddress(), ex.Message, ex.InnerException.Message, "Uploads/Olcu");
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            reader.Close();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}