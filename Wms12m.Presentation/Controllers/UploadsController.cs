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
        public JsonResult Malzeme(string SID, int DID, string Hesap, HttpPostedFileBase file)
        {
            if (CheckPerm("Mal Kabul", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _result = new Result(false, 0, "Hatalı dosya");
            if (file == null || file.ContentLength == 0) return Json(_result, JsonRequestBehavior.AllowGet);
            //gelen dosyayı oku
            Stream stream = file.InputStream;
            IExcelDataReader reader;
            //dosya tipini bul
            if (file.FileName.EndsWith(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else
                return Json(_result, JsonRequestBehavior.AllowGet);
            //ilk satır başlık
            reader.IsFirstRowAsColumnNames = true;
            //exceldeki bilgileri datasete aktar
            DataSet result = reader.AsDataSet();
            //kontrol
            if (result.Tables.Count == 0) return Json(_result, JsonRequestBehavior.AllowGet);
            if (result.Tables[0].Rows == null) return Json(_result, JsonRequestBehavior.AllowGet);
            //sti listesi oluşturuyoruz. tüm bilgiyi tek seferde veritabanına kaydetçek
            List<IRS_Detay> liste = new List<IRS_Detay>();
            var depo = Store.Detail(DID);
            //buraya kadar hata yoksa bunu yapar. yine de hata olursa hiçbirini kaydetmez...
            int tarih = fn.ToOADate();
            var gorevno = db.SettingsGorevNo(tarih, DID).FirstOrDefault();
            var sonuc = new InsertIrsaliye_Result();
            string evraklar = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = result.Tables[0].Rows[i];
                    //kontrol
                    try
                    {
                        if (dr["İrsaliye No"].ToString() == "") return Json(_result, JsonRequestBehavior.AllowGet);
                        if (dr["Birim"].ToString() == "") return Json(_result, JsonRequestBehavior.AllowGet);
                        if (dr["Miktar"].ToString2().IsNumeric() == false) return Json(_result, JsonRequestBehavior.AllowGet);
                        if (dr["MalKodu"].ToString() == "") return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception)
                    {
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    //satıcı malkodundan malkodunu getir
                    string malkodu = String.Format("SELECT MalKodu FROM FINSAT6{0}.FINSAT6{0}.TTY WITH(NOLOCK) WHERE (SatMalKodu = '{1}') AND (Chk = '{2}')", SID, dr["MalKodu"].ToString(), Hesap);
                    malkodu = db.Database.SqlQuery<string>(malkodu).FirstOrDefault();
                    _result.Message = "Mal kodu yanlış";
                    if (malkodu == "" || malkodu == null) return Json(_result, JsonRequestBehavior.AllowGet);
                    //add irsaliye and gorev
                    sonuc = db.InsertIrsaliye(SID, DID, gorevno, dr["İrsaliye No"].ToString(), tarih, "", false, ComboItems.MalKabul.ToInt32(), vUser.UserName, tarih, fn.ToOATime(), Hesap, "", 0, "").FirstOrDefault();
                    if (evraklar.Contains(dr["İrsaliye No"].ToString()) == false)
                    {
                        if (evraklar != "") evraklar += ",";
                        evraklar += dr["İrsaliye No"].ToString();
                    }
                    //add detays
                    try
                    {
                        IRS_Detay sti = new IRS_Detay()
                        {
                            IrsaliyeID = sonuc.IrsaliyeID.Value,
                            MalKodu = malkodu,
                            Miktar = Convert.ToDecimal(dr["Miktar"]),
                            Birim = dr["Birim"].ToString()
                        };
                        if (dr["Kaynak Sipariş No"].ToString() != "")
                        {
                            string sql = String.Format("SELECT ROW_ID, EvrakNo, Tarih, SiraNo, BirimMiktar FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) WHERE (IslemTur = 0) AND (KynkEvrakTip = 63) AND (SiparisDurumu = 0) AND (EvrakNo = '{1}') AND (MalKodu = '{2}') AND (Birim = '{3}') AND (Depo = '{4}')", SID, dr["Kaynak Sipariş No"].ToString(), malkodu, dr["Birim"].ToString(), depo.DepoKodu);
                            var tbl = db.Database.SqlQuery<frmIrsaliyeMalzeme>(sql).FirstOrDefault();
                            if (tbl != null)
                            {
                                sti.KynkSiparisNo = tbl.EvrakNo;
                                sti.KynkSiparisID = tbl.ROW_ID;
                                sti.KynkSiparisMiktar = tbl.BirimMiktar;
                                sti.KynkSiparisSiraNo = tbl.SiraNo;
                                sti.KynkSiparisTarih = tbl.Tarih;
                            }
                        }
                        //ekle
                        liste.Add(sti);
                    }
                    catch (Exception)
                    {
                        _result.Message = "Hatalı satırlar var";
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                }
                reader.Close();
                //update db
                try
                {
                    db.IRS_Detay.AddRange(liste);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Uploads/Irsaliye");
                    _result.Message = "Hatalı satırlar var";
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
            }
            var Unvan = db.Database.SqlQuery<string>(string.Format("SELECT Unvan1+' '+Unvan2 AS Unvan FROM FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) WHERE HesapKodu = '{1}'", SID, Hesap)).FirstOrDefault();
            //update görev
            var gorev = db.Gorevs.Where(m => m.ID == sonuc.GorevID.Value).FirstOrDefault();
            gorev.DurumID = ComboItems.Açık.ToInt32();
            gorev.Bilgi = "Irs: " + evraklar + ", Tedarikçi: " + Unvan;
            db.SaveChanges();
            //return
            _result.Id = 1;
            _result.Status = true;
            return Json(_result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// boyut kartı için toplu giriş yapar
        /// </summary>
        public JsonResult Olcu(HttpPostedFileBase file)
        {
            if (CheckPerm("Boyut Kartı", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(false, "Hatalı Kayıt !");
            if (file == null || file.ContentLength == 0) return Json(_Result, JsonRequestBehavior.AllowGet);
            //gelen dosyayı oku
            Stream stream = file.InputStream;
            IExcelDataReader reader;
            //dosya tipini bul
            if (file.FileName.EndsWith(".xlsx"))
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
                _Result.Message = basarili + " adet satır eklendi";
            else
                _Result.Message = "";
            if (basarili > 0 && hatali > 0)
                _Result.Message += ", ";
            if (hatali > 0)
                _Result.Message += hatali + " satır hata verdi. Hatalı satırlar: \n" + hatalilar;
            else
                _Result.Status = true;
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// stoğa excelden elle ekleme
        /// </summary>
        public JsonResult Stock(int DID, HttpPostedFileBase file)
        {
            if (CheckPerm("Stok", PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _result = new Result(false, 0, "Hatalı dosya");
            if (file == null || file.ContentLength == 0) return Json(_result, JsonRequestBehavior.AllowGet);
            //gelen dosyayı oku
            Stream stream = file.InputStream;
            IExcelDataReader reader;
            //dosya tipini bul
            if (file.FileName.EndsWith(".xlsx"))
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            else
                return Json(_result, JsonRequestBehavior.AllowGet);
            //ilk satır başlık
            reader.IsFirstRowAsColumnNames = true;
            //exceldeki bilgileri datasete aktar
            DataSet result = reader.AsDataSet();
            //kontrol
            if (result.Tables.Count == 0) return Json(_result, JsonRequestBehavior.AllowGet);
            if (result.Tables[0].Rows == null) return Json(_result, JsonRequestBehavior.AllowGet);
            //loop all list
            int basarili = 0, hatali = 0, tarih = fn.ToOADate(); string hatalilar = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                //add to irs table
                var sonuc = Irsaliye.Operation(new IR() { SirketKod = db.GetSirketDBs().FirstOrDefault(), DepoID = DID, EvrakNo = db.SettingsGorevNo(tarih, DID).FirstOrDefault(), HesapKodu = "Elle Ekleme", Tarih = tarih });
                //loop
                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = result.Tables[0].Rows[i];
                    try
                    {
                        if (dr["Hücre Adı"].ToString() != "" && dr["Mal Kodu"].ToString() != "" && dr["Birim"].ToString() != "" && dr["Miktar"].ToString2().IsNumeric() != false)
                        {
                            var katID = db.GetHucreKatID(DID, dr["Hücre Adı"].ToString()).FirstOrDefault();
                            var miktar = dr["Miktar"].ToString().ToDecimal();
                            if (katID != null)
                            {
                                var tmp2 = Yerlestirme.Detail(katID.Value, dr["Mal Kodu"].ToString(), dr["Birim"].ToString());
                                if (tmp2 == null)
                                {
                                    tmp2 = new Yer() { KatID = katID.Value, MalKodu = dr["Mal Kodu"].ToString(), Birim = dr["Birim"].ToString(), Miktar = miktar };
                                    Yerlestirme.Insert(tmp2, 0, vUser.Id);
                                }
                                else
                                {
                                    tmp2.Miktar += miktar;
                                    Yerlestirme.Update(tmp2, 0, vUser.Id, false, miktar);
                                }
                                basarili++;
                            }
                            else
                            {
                                hatali++;
                                if (hatalilar != "") hatalilar += ", ";
                                hatalilar += i;
                            }
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
                //update db
                try
                {
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Logger(ex, "Uploads/Stock");
                    _result.Message = "Hatalı satırlar var";
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
            }
            if (basarili > 0)
                _result.Message = basarili + " adet satır eklendi";
            else
                _result.Message = "";
            if (basarili > 0 && hatali > 0)
                _result.Message += ", ";
            if (hatali > 0)
                _result.Message += hatali + " satır hata verdi. Hatalı satırlar: \n" + hatalilar;
            else
                _result.Status = true;
            return Json(_result, JsonRequestBehavior.AllowGet);
        }
    }
}