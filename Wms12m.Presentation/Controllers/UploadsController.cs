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
            if (CheckPerm(Perms.MalKabul, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _result = new Result(false, 0, "Hatalı dosya!");
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
            var sonuc = new InsertIrsaliye_Result();
            var gorevno = db.SettingsGorevNo(tarih, DID).FirstOrDefault();
            string evraklar = "";
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                {
                    _result = new Result(false, 0, "Hatalı dosya");
                    DataRow dr = result.Tables[0].Rows[i];
                    //kontrol
                    try
                    {
                        if (dr["İrsaliye No"].ToString() == "") return Json(_result, JsonRequestBehavior.AllowGet);
                        if (dr["Miktar"].ToString2().IsNumeric() == false) return Json(_result, JsonRequestBehavior.AllowGet);
                        if (dr["MalKodu"].ToString() == "") return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "Uploads/Malzeme");
                        return Json(_result, JsonRequestBehavior.AllowGet);
                    }
                    //satıcı malkodundan malkodunu getir
                    string malkodu = String.Format("SELECT MalKodu FROM FINSAT6{0}.FINSAT6{0}.TTY WITH(NOLOCK) WHERE (SatMalKodu = '{1}') AND (Chk = '{2}')", SID, dr["MalKodu"].ToString(), Hesap);
                    malkodu = db.Database.SqlQuery<string>(malkodu).FirstOrDefault();
                    _result.Message = "Mal kodu yanlış";
                    if (malkodu == "" || malkodu == null) return Json(_result, JsonRequestBehavior.AllowGet);
                    //add irsaliye and gorev
                    string irsNo = dr["İrsaliye No"].ToString();
                    if (evraklar.Contains(irsNo) == false)
                    {
                        if (evraklar != "") evraklar += ",";
                        evraklar += irsNo;
                    }
                    else//daha önce eklenen bir irsaliye ise
                    {
                        var kontrol1 = db.Gorevs.Where(m => m.IR.EvrakNo == irsNo && m.IR.IslemTur == false && (m.DurumID == 9 || m.DurumID == 11)).FirstOrDefault();
                        if (kontrol1 != null)
                            return Json(new Result(false, 0, kontrol1.IR.EvrakNo + " nolu irsaliye daha önce kaydedilmiş."), JsonRequestBehavior.AllowGet);
                    }
                    sonuc = db.InsertIrsaliye(SID, DID, gorevno, irsNo, tarih, "", false, ComboItems.MalKabul.ToInt32(), vUser.UserName, tarih, fn.ToOATime(), Hesap, "", 0, "").FirstOrDefault();
                    //add detays
                    try
                    {
                        //malkodu kontrol
                        var kontrol2 = db.IRS_Detay.Where(m => m.MalKodu == malkodu && m.IR.EvrakNo == irsNo && m.IR.IslemTur == false).FirstOrDefault();
                        if (kontrol2 != null)
                            return Json(new Result(false, 0, kontrol2.IR.EvrakNo + " nolu irsaliyeye daha önce " + malkodu + " eklenmiş."), JsonRequestBehavior.AllowGet);
                        //irs detay
                        IRS_Detay sti = new IRS_Detay()
                        {
                            IrsaliyeID = sonuc.IrsaliyeID.Value,
                            MalKodu = malkodu,
                            Miktar = Convert.ToDecimal(dr["Miktar"]),
                            Birim = dr["Birim"].ToString()
                        };
                        if (dr["Kaynak Sipariş No"].ToString() != "")
                        {
                            //kaynak sipariş
                            string sql = String.Format("SELECT FINSAT6{0}.FINSAT6{0}.SPI.ROW_ID, FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo, FINSAT6{0}.FINSAT6{0}.SPI.Tarih, FINSAT6{0}.FINSAT6{0}.SPI.SiraNo, FINSAT6{0}.FINSAT6{0}.SPI.BirimMiktar, FINSAT6{0}.FINSAT6{0}.STK.Birim1 as Birim " +
                                "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH(NOLOCK) INNER JOIN FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) ON FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = FINSAT6{0}.FINSAT6{0}.STK.MalKodu " +
                                "WHERE (FINSAT6{0}.FINSAT6{0}.SPI.IslemTur = 0) AND (FINSAT6{0}.FINSAT6{0}.SPI.KynkEvrakTip = 63) AND (FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = '{1}') AND (FINSAT6{0}.FINSAT6{0}.SPI.MalKodu = '{2}') AND (FINSAT6{0}.FINSAT6{0}.SPI.Depo = '{3}')", SID, dr["Kaynak Sipariş No"].ToString(), malkodu, depo.DepoKodu);
                            var tbl = db.Database.SqlQuery<frmIrsaliyeMalzeme>(sql).FirstOrDefault();
                            if (tbl != null)
                            {
                                if (sti.Birim == "") sti.Birim = tbl.Birim;
                                sti.KynkSiparisNo = tbl.EvrakNo;
                                sti.KynkSiparisID = tbl.ROW_ID;
                                sti.KynkSiparisMiktar = tbl.BirimMiktar;
                                sti.KynkSiparisSiraNo = tbl.SiraNo;
                                sti.KynkSiparisTarih = tbl.Tarih;
                            }
                        }
                        //birim kontrol
                        if (sti.Birim == "")
                            sti.Birim = db.Database.SqlQuery<string>(string.Format("SELECT Birim1 FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalKodu = '{1}')", SID, sti.MalKodu)).FirstOrDefault();
                        if (sti.Birim == "")
                            sti.Birim = "ADET";
                        //Makara No
                        var kod1 = db.Database.SqlQuery<string>(string.Format("SELECT Kod1 FROM FINSAT6{0}.FINSAT6{0}.STK WHERE (MalKodu = '{1}')", SID, sti.MalKodu)).FirstOrDefault();
                        if (kod1 == "KKABLO")
                        {
                            if (dr["Makara No"].ToString() != "") sti.MakaraNo = dr["Makara No"].ToString();
                            else sti.MakaraNo = "Boş-" + db.SettingsMakaraNo(DID).FirstOrDefault();
                        }
                        //ekle
                        liste.Add(sti);
                    }
                    catch (Exception ex)
                    {
                        _result.Message = "Hatalı satırlar var";
                        Logger(ex, "Uploads/Malzeme");
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
                    Logger(ex, "Uploads/Malzeme");
                    _result.Message = "Hatalı satırlar var";
                    return Json(_result, JsonRequestBehavior.AllowGet);
                }
            }
            var Unvan = db.Database.SqlQuery<string>(string.Format("SELECT Unvan1+' '+Unvan2 AS Unvan FROM FINSAT6{0}.FINSAT6{0}.CHK WITH(NOLOCK) WHERE HesapKodu = '{1}'", SID, Hesap)).FirstOrDefault();
            //log
            LogActions("", "Uploads", "Malzeme", ComboItems.alYükle, sonuc.GorevID.Value, "GörevID: " + sonuc.GorevID.Value + ", Satır Sayısı: " + liste.Count);
            //update görev
            var gorev = db.Gorevs.Where(m => m.ID == sonuc.GorevID.Value).FirstOrDefault();
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
            if (CheckPerm(Perms.BoyutKartı, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(false, "Hatalı dosya!");
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
            int basarili = 0, hatali = 0, tarih = fn.ToOADate(); string hatalilar = "", birim;
            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
            {
                DataRow dr = result.Tables[0].Rows[i];
                //kontrol
                try
                {
                    if (dr["Mal Kodu"].ToString() != "" && dr["En"].ToString2() != "" && dr["Boy"].ToString2() != "" && dr["Derinlik"].ToString2() != "" && dr["Ağırlık"].ToString2() != "")
                    {
                        birim = dr["Birim"].ToString();
                        if (birim == "")
                            birim = db.Database.SqlQuery<string>(string.Format("SELECT Birim1 FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE MalKodu='{1}'", db.GetSirketDBs().FirstOrDefault(), dr["Mal Kodu"].ToString())).FirstOrDefault();
                        if (birim != "")
                        {
                            var test = db.Olcus.Where(m => m.MalKodu == dr["Mal Kodu"].ToString() && m.Birim == birim).FirstOrDefault();
                            if (test == null)
                            {
                                //add new
                                Olcu sti = new Olcu()
                                {
                                    MalKodu = dr["Mal Kodu"].ToString(),
                                    Birim = dr["Birim"].ToString(),
                                    En = dr["En"].ToDecimal(),
                                    Boy = dr["Boy"].ToDecimal(),
                                    Derinlik = dr["Derinlik"].ToDecimal(),
                                    Agirlik = dr["Ağırlık"].ToDecimal(),
                                    Kaydeden = vUser.UserName,
                                    KayitTarih = tarih,
                                    Degistiren = vUser.UserName,
                                    DegisTarih = tarih
                                };
                                //ekle
                                if (sti.En != 0 || sti.Boy != 0 || sti.Derinlik != 0 || sti.Agirlik != 0)
                                {
                                    db.Olcus.Add(sti);
                                    db.SaveChanges();
                                    basarili++;
                                }
                            }
                        }
                        else
                        {
                            hatali++;
                            if (hatalilar != "") hatalilar += ", ";
                            hatalilar += (i + 1);
                        }
                    }
                    else
                    {
                        hatali++;
                        if (hatalilar != "") hatalilar += ", ";
                        hatalilar += (i + 1);
                    }
                }
                catch (Exception ex)
                {
                    hatali++;
                    if (hatalilar != "") hatalilar += ", ";
                    hatalilar += (i + 1);
                    Logger(ex, "Uploads/Malzeme");
                }
            }
            reader.Close();
            if (basarili > 0)
            {
                _Result.Message = basarili + " adet satır eklendi";
                //log
                LogActions("", "Uploads", "Olcu", ComboItems.alYükle, 0, "Satır Sayısı: " + basarili);
            }
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
            if (CheckPerm(Perms.Stok, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            Result _result = new Result(false, 0, "Hatalı dosya!");
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
                                hatalilar += (i + 1);
                            }
                        }
                        else
                        {
                            hatali++;
                            if (hatalilar != "") hatalilar += ", ";
                            hatalilar += (i + 1);
                        }
                    }
                    catch (Exception ex)
                    {
                        hatali++;
                        if (hatalilar != "") hatalilar += ", ";
                        hatalilar += (i + 1);
                        Logger(ex, "Uploads/Stock");
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
            {
                _result.Message = basarili + " adet satır eklendi";
                //log
                LogActions("", "Uploads", "Stock", ComboItems.alYükle, 0, "Satır Sayısı: " + basarili);
            }
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
        /// <summary>
        /// boyut kartı için toplu giriş yapar
        /// </summary>
        public JsonResult Kat(HttpPostedFileBase file)
        {
            if (CheckPerm(Perms.KatKartı, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(false, "Hatalı dosya!");
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
            int basarili = 0, hatali = 0, ozelliktipi = Combos.Özellik.ToInt32(); string hatalilar = "";
            for (int i = 0; i < result.Tables[0].Rows.Count; i++)
            {
                DataRow dr = result.Tables[0].Rows[i];
                //kontrol
                try
                {
                    string tdepo = dr["Depo"].ToString();
                    string tkoridor = dr["Koridor"].ToString();
                    string traf = dr["Raf Grubu"].ToString();
                    string tbolum = dr["Bölüm"].ToString();
                    string tkat = dr["Rafın Katı"].ToString();
                    string tozellik = dr["Özellik"].ToString();
                    string taciklama = dr["Açıklama"].ToString();
                    if (tdepo != "" && tkoridor != "" && traf != "" && tbolum != "" && tkat != "" && tozellik != "" &&
                    (dr["Genişlik (mm)"].ToString2().IsNumeric() != false || dr["Genişlik (mm)"].ToString2() == "*") &&
                    (dr["Derinlik (mm)"].ToString2().IsNumeric() != false || dr["Derinlik (mm)"].ToString2() == "*") &&
                    (dr["Yükseklik (mm)"].ToString2().IsNumeric() != false || dr["Yükseklik (mm)"].ToString2() == "*") &&
                    (dr["Kapasite (kg)"].ToString2().IsNumeric() != false || dr["Kapasite (kg)"].ToString2() == "*"))
                    {
                        var dp = db.Depoes.Where(m => m.DepoKodu == tdepo).FirstOrDefault();
                        if (dp == null)
                        {
                            _Result.Message = "Önce depoyu ekleyin";
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            var kr = db.Koridors.Where(m => m.KoridorAd == tkoridor && m.DepoID == dp.ID).FirstOrDefault();
                            if (kr == null)
                            {
                                kr = new Koridor() { DepoID = dp.ID, KoridorAd = tkoridor, SiraNo = 0, Aktif = true, Kaydeden = vUser.UserName, KayitTarih = fn.ToOADate(), Degistiren = vUser.UserName, DegisTarih = fn.ToOADate() };
                                db.Koridors.Add(kr);
                                db.SaveChanges();
                            }
                            var rf = db.Rafs.Where(m => m.RafAd == traf && m.KoridorID == kr.ID).FirstOrDefault();
                            if (rf == null)
                            {
                                rf = new Raf() { KoridorID = kr.ID, RafAd = traf, SiraNo = 0, Aktif = true, Kaydeden = vUser.UserName, KayitTarih = fn.ToOADate(), Degistiren = vUser.UserName, DegisTarih = fn.ToOADate() };
                                db.Rafs.Add(rf);
                                db.SaveChanges();
                            }
                            var bl = db.Bolums.Where(m => m.BolumAd == tbolum && m.RafID == rf.ID).FirstOrDefault();
                            if (bl == null)
                            {
                                bl = new Bolum() { RafID = rf.ID, BolumAd = tbolum, SiraNo = 0, Aktif = true, Kaydeden = vUser.UserName, KayitTarih = fn.ToOADate(), Degistiren = vUser.UserName, DegisTarih = fn.ToOADate() };
                                db.Bolums.Add(bl);
                                db.SaveChanges();
                            }
                            //özellik id bul
                            var ozellik = db.Database.SqlQuery<int>("SELECT ID FROM ComboItem_Name WHERE (ComboID = 3) AND (Name like '%" + tozellik + "%')").FirstOrDefault();
                            if (ozellik == 0) ozellik = 14;
                            //kat bul
                            var kt = db.Kats.Where(m => m.KatAd == tkat && m.BolumID == bl.ID).FirstOrDefault();
                            if (kt == null)
                            {
                                kt = new Kat()
                                {
                                    BolumID = bl.ID,
                                    KatAd = tkat,
                                    Boy = dr["Yükseklik (mm)"].ToDecimal(),
                                    En = dr["Genişlik (mm)"].ToDecimal(),
                                    Derinlik = dr["Derinlik (mm)"].ToDecimal(),
                                    AgirlikKapasite = dr["Kapasite (kg)"].ToDecimal(),
                                    OzellikID = ozellik,
                                    SiraNo = 0,
                                    Aktif = true,
                                    Kaydeden = vUser.UserName,
                                    KayitTarih = fn.ToOADate(),
                                    Degistiren = vUser.UserName,
                                    DegisTarih = fn.ToOADate()
                                };
                                if (taciklama != "") kt.Aciklama = taciklama;
                                db.Kats.Add(kt);
                            }
                            else
                            {
                                kt.Boy = dr["Yükseklik (mm)"].ToDecimal();
                                kt.En = dr["Genişlik (mm)"].ToDecimal();
                                kt.Derinlik = dr["Derinlik (mm)"].ToDecimal();
                                kt.AgirlikKapasite = dr["Kapasite (kg)"].ToDecimal();
                                kt.OzellikID = ozellik;
                                kt.Degistiren = vUser.UserName;
                                kt.DegisTarih = fn.ToOADate();
                            }
                            db.SaveChanges();
                            basarili++;
                        }
                    }
                    else
                    {
                        hatali++;
                        if (hatalilar != "") hatalilar += ", ";
                        hatalilar += (i + 1);
                    }
                }
                catch (Exception ex)
                {
                    hatali++;
                    if (hatalilar != "") hatalilar += ", ";
                    hatalilar += (i + 1);
                    Logger(ex, "Uploads/Kat");
                }
            }
            reader.Close();
            if (basarili > 0)
            {
                _Result.Message = basarili + " adet satır eklendi";
                //log
                LogActions("", "Uploads", "Kat", ComboItems.alYükle, 0, "Satır Sayısı: " + basarili);
            }
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
        /// kullanıcı resmi yükle
        /// </summary>
        public JsonResult UserImage(string ID, HttpPostedFileBase file)
        {
            if (CheckPerm(Perms.Kullanıcılar, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(false, "Hatalı dosya!");
            if (file == null || file.ContentLength == 0) return Json(_Result, JsonRequestBehavior.AllowGet);
            //dosya tipini bul
            if (!file.FileName.EndsWith(".jpg") && !file.FileName.EndsWith(".png"))
                return Json(_Result, JsonRequestBehavior.AllowGet);



            _Result.Message = "";
            _Result.Status = true;
            _Result.Id = 1;
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}