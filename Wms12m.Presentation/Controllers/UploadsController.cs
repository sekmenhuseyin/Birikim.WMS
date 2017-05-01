using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wms12m.Entity;
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
            if (file == null || file.ContentLength == 0 || IrsNo == 0) return Json(false, JsonRequestBehavior.AllowGet);
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
                    IRS_Detay sti = new IRS_Detay()
                    {
                        IrsaliyeID = IrsNo,
                        MalKodu = malkodu,
                        Miktar = Convert.ToDecimal(dr["Miktar"]),
                        Birim = dr["Birim"].ToString()
                    };
                    if (dr["Kaynak Sipariş No"].ToString() != "") sti.KynkSiparisNo = dr["Kaynak Sipariş No"].ToString();
                    if (dr["Kaynak Sipariş Sıra No"].ToString() != "") sti.KynkSiparisSiraNo = dr["Kaynak Sipariş Sıra No"].ToString().ToShort();
                    if (dr["Kaynak Sipariş Tarih"].ToString() != "") sti.KynkSiparisTarih = dr["Kaynak Sipariş Tarih"].ToString().ToInt32();
                    if (dr["Kaynak Sipariş Miktar"].ToString() != "") sti.KynkSiparisMiktar = dr["Kaynak Sipariş Miktar"].ToString().ToDecimal();
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
                catch (Exception ex)
                {
                    Logger(ex, "Uploads/irsaliye");
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
            var _Result = new Result(false, "Hatalı Kayıt !");
            if (file == null || file.ContentLength == 0) return Json(_Result, JsonRequestBehavior.AllowGet);
            //gelen dosyayı oku
            Stream stream = file.InputStream;
            IExcelDataReader reader;
            //dosya tipini bul
            if (file.FileName.EndsWith(".xls"))
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            else if (file.FileName.EndsWith(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else
                return Json(_Result, JsonRequestBehavior.AllowGet);
            //ilk satır başlık
            reader.IsFirstRowAsColumnNames = true;
            //exceldeki bilgileri datasete aktar
            DataSet result = reader.AsDataSet();
            //kontrol
            if (result.Tables.Count == 0) return Json(_Result, JsonRequestBehavior.AllowGet);
            if (result.Tables[0].Rows == null) return Json(_Result, JsonRequestBehavior.AllowGet);
            //her satırı tek tek kaydet
            int basarili = 0, hatali = 0; string hatalilar = "";
            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
            {
                DataRow dr = result.Tables[0].Rows[i];
                //kontrol
                try
                {
                    if (dr["Mal Kodu"].ToString() != "" && dr["Birim"].ToString() != "" && dr["En"].ToString2().IsNumeric() != false && dr["Boy"].ToString2().IsNumeric() != false && dr["Derinlik"].ToString2().IsNumeric() != false && dr["Ağırlık"].ToString2().IsNumeric() != false)
                    {
                        //add new
                        Olcu sti = new Olcu()
                        {
                            MalKodu = dr["Mal Kodu"].ToString(),
                            Birim = dr["Birim"].ToString(),
                            En = dr["En"].ToDecimal(),
                            Boy = dr["Boy"].ToDecimal(),
                            Derinlik = dr["Derinlik"].ToDecimal(),
                            Agirlik = dr["Ağırlık"].ToDecimal()
                        };
                        //ekle
                        db.Olcus.Add(sti);
                        db.SaveChanges();
                        basarili++;
                    }
                    else
                    {
                        hatali++;
                        if (hatalilar != "") hatalilar += ", ";
                        hatalilar += i;
                    }
                }
                catch (Exception)
                {
                    hatali++;
                    if (hatalilar != "") hatalilar += ", ";
                    hatalilar += i;
                }
            }
            reader.Close();
            if (basarili > 0)
                _Result.Message = basarili + " kart eklendi";
            else
                _Result.Message = "";
            if (basarili > 0 && hatali > 0)
                _Result.Message += ", ";
            if (hatali > 0)
                _Result.Message += hatali + " kart hata verdi. Hatalı satırlar: \n" + hatalilar;
            else
                _Result.Status = true;
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}