﻿using Birikim.Models;
using KurumsalKaynakPlanlaması12M;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{
    public class PurchaseController : RootController
    {
        /// <summary>
        /// gm sipariş onaylama sayfası
        /// </summary>
        public ActionResult GM_Onay()
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return Redirect("/");

            ViewBag.OnayTip = MyGlobalVariables.OnayTip.GMOnay;
            ViewBag.baslik = "Satınalma Onay";
            MyGlobalVariables.Depo = "93 DP";
            MyGlobalVariables.DovizDurum = false;

            MyGlobalVariables.SipTalepList = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[wms].[SatinAlmaGMOnayList]", vUser.SirketKodu)).ToList();

            return View("GM_Onay", MyGlobalVariables.SipTalepList);
        }

        /// <summary>
        /// Satınalma Sipariş Talebi GMY Mali Onaylama Sayfası
        /// </summary>
        /// <returns></returns>
        public ActionResult SatisSiparisGMYMali_Onay()
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return Redirect("/");

            ViewBag.OnayTip = MyGlobalVariables.OnayTip.SatSipGMYMaliOnay;
            ViewBag.baslik = "Satınalma Sipariş Talebi GMY " + (vUser.UserName.ToUpper() == "TUNCE" ? "Üretim" : "Mali") + " Onay";
            MyGlobalVariables.Depo = "93 DP";
            MyGlobalVariables.DovizDurum = false;

            MyGlobalVariables.SipTalepList = db.Database.SqlQuery<SatTalep>(string.Format(@"[FINSAT6{0}].[wms].[SatinAlmaTalepGMYMaliOnayList] '{1}'", vUser.SirketKodu, vUser.UserName.ToUpper())).ToList();

            return View("GM_Onay", MyGlobalVariables.SipTalepList);
        }

        /// <summary>
        /// onaylanacak malzeme listesi
        /// </summary>
        public PartialViewResult SipGMOnayList(string HesapKodu, int SipTalepNo, string OnayTip)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return null;

            ViewBag.HesapKodu = JsonConvert.SerializeObject(HesapKodu);
            ViewBag.SipTalepNo = SipTalepNo;
            ViewBag.OnayTip = OnayTip;
            return PartialView("GMOnay_List");
        }

        /// <summary>
        /// onaylanacak fatura biilgileri
        /// </summary>
        public PartialViewResult SipGMOnayListFTD(string HesapKodu, int SipTalepNo, string OnayTip)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return null;

            ViewBag.HesapKodu = JsonConvert.SerializeObject(HesapKodu);
            ViewBag.SipTalepNo = SipTalepNo;
            ViewBag.OnayTip = OnayTip;
            return PartialView("GMOnayFTD_List");
        }

        public string SipGMOnayListData(string HesapKodu, int SipTalepNo, string OnayTip)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return null;
            MyGlobalVariables.DovizDurum = false;

            if (MyGlobalVariables.GridSource == null)
                MyGlobalVariables.GridSource = new List<KKP_SPI>();
            else
                MyGlobalVariables.GridSource.Clear();

            MyGlobalVariables.SipEvrak = new KKPEvrakSiparis(KKPSiparisTip.AlimSiparisi);

            if (OnayTip == MyGlobalVariables.OnayTip.GMOnay.ToString())
            {
                MyGlobalVariables.TalepSource = db.Database.SqlQuery<SatTalep>(string.Format(@"
                [FINSAT6{0}].[wms].[SatinAlmaGMOnayListData] @HesapKodu='{1}', @SipTalepNo={2} ", vUser.SirketKodu, HesapKodu, SipTalepNo)).ToList();
            }
            else if (OnayTip == MyGlobalVariables.OnayTip.SatSipGMYMaliOnay.ToString())
            {
                MyGlobalVariables.TalepSource = db.Database.SqlQuery<SatTalep>(string.Format(@"
                [FINSAT6{0}].[wms].[SatinAlmaTalepGMYMaliOnayListData] @HesapKodu='{1}', @SipTalepNo={2} ", vUser.SirketKodu, HesapKodu, SipTalepNo)).ToList();
            }

            if (MyGlobalVariables.TalepSource[0].SipIslemTip == null || (MyGlobalVariables.TalepSource[0].SipIslemTip != 1 && MyGlobalVariables.TalepSource[0].SipIslemTip != 2))
                return "";

            MyGlobalVariables.SipEvrak.IslemTip = (KKPIslemTipSPI)MyGlobalVariables.TalepSource[0].SipIslemTip;
            MyGlobalVariables.SipEvrak.dvzTL = (KKPDvzTL)(short)MyGlobalVariables.TalepSource[0].FTDDovizTL;
            short siraNo = 0;

            foreach (var item in MyGlobalVariables.TalepSource)
            {
                if ((short)item.DvzTL == (short)1)
                    MyGlobalVariables.DovizDurum = true;

                var spi = new KKP_SPI(KKPSiparisTip.AlimSiparisi)
                {
                    MalKodu = item.MalKodu,
                    MalAdi = item.MalAdi,
                    Birim = item.Birim,
                    BirimMiktar = item.BirimMiktar,
                    Miktar = Convert.ToDecimal(item.Miktar),
                    Fiyat = (decimal)item.BirimFiyat,
                    BirimFiyat = (decimal)item.BirimFiyat,

                    DvzTL = (short)item.DvzTL,
                    DovizCinsi = item.DvzCinsi,
                    DovizKuru = item.DvzKuru ?? 0,

                    Aciklama = item.Aciklama,
                    TeklifAciklamasi = item.TeklifAciklamasi,

                    KDVOran = item.KDVOran,
                    SiraNo = siraNo
                };

                spi.SatirHesapla();

                spi.Kod1 = item.TalepNo;
                spi.Kod2 = item.Satinalmaci;
                spi.Kod3 = item.SipTalepNo.ToString();
                spi.Kod4 = item.TeklifNo.ToString();

                spi.Kaydeden = item.Kaydeden;
                spi.Nesne3 = item.TesisKodu ?? "";
                spi.SatinAlmaci = item.Satinalmaci;

                spi.Kod11 = item.TeklifVade ?? 0; //Sırf ekranda göstermek için Kod11' e teklif de ki Vade atıyoruz. Ve kaydediyoruz. SPI.Kod11 daha sonra değiştirilip silinebilir önemli değil. (şimdilik)

                spi.Depo = MyGlobalVariables.Depo;

                spi.Operator = (short)(item.Operator != null ? item.Operator.Value : 0);
                spi.Katsayi = item.Katsayi != null ? item.Katsayi.Value : 0;

                MyGlobalVariables.SipEvrak.Ekle(spi);
                MyGlobalVariables.GridSource.Add(spi);
                siraNo++;
            }

            if (MyGlobalVariables.DovizDurum == false)
            {
                MyGlobalVariables.SipEvrak.FTDHesapla("TL", Convert.ToDecimal(0));
                MyGlobalVariables.GridFTD = null;
                MyGlobalVariables.GridFTD = MyGlobalVariables.SipEvrak.FTDList;
            }
            else
            {
                var kur = MyGlobalVariables.TalepSource[0].FTDDovizKuru ?? 0;
                if (kur > 0)
                {
                    MyGlobalVariables.SipEvrak.FTDHesapla(MyGlobalVariables.TalepSource[0].FTDDovizCinsi, kur);
                    MyGlobalVariables.GridFTD = null;
                    MyGlobalVariables.GridFTD = MyGlobalVariables.SipEvrak.FTDList;
                }
                else
                    MyGlobalVariables.GridFTD = new List<KKP_FTD>();
            }

            var json = new JavaScriptSerializer().Serialize(MyGlobalVariables.GridSource);
            return json;
        }

        public string SipGMOnayListFTDData(string HesapKodu, int SipTalepNo)
        {
            var json = new JavaScriptSerializer().Serialize(MyGlobalVariables.GridFTD);
            return json;
        }

        /// <summary>
        /// gm için sipariş oanylama
        /// </summary>
        public JsonResult SipGMOnayla(string OnayTip)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true, "İşlem Başarılı");

            if (MyGlobalVariables.TalepSource == null)
            {
                _Result.Message = "Sipariş Seçin";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            decimal limit = 0;
            if (OnayTip == MyGlobalVariables.OnayTip.SatSipGMYMaliOnay.ToString())
            {
                if (MyGlobalVariables.GridFTD.IsNotNull())
                {
                    var ftdtoplam = MyGlobalVariables.GridFTD.Where(x => x.SatirTip == (short)12).FirstOrDefault();
                    if (ftdtoplam.IsNull())
                    {
                        _Result.Message = "Toplam Hesaplanmalıdır.";
                        _Result.Status = false;
                        return Json(_Result, JsonRequestBehavior.AllowGet);
                    }
                }

                limit = db.Database.SqlQuery<decimal>(@"SELECT SiparisGMYOnayLimit FROM Kaynak.sta.GenelAyarVeParams(NOLOCK) WHERE Tip = 2").FirstOrDefault();

                if (limit.IsNull() || limit == 0)
                {
                    _Result.Message = "Satınalma Sipariş GM Onay Limit miktarı belirlenmemiş. Lütfen IT Yöneticinizle iletişime geçiniz!!";
                    _Result.Status = false;
                    return Json(_Result, JsonRequestBehavior.AllowGet);
                }
            }

            var muhsebesirketkodu = db.GetSirketMuhasebeKod(vUser.SirketKodu, fn.ToOADate()).FirstOrDefault();
            using (KKP kkp = new KKP(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu, muhsebesirketkodu))
            {
                bool siparisOnayMailGonderim = false;
                int sipTarih = 0;
                string sipEvrakNo = MyGlobalVariables.SipEvrak.EvrakNo;
                string hesapKodu = MyGlobalVariables.SipEvrak.HesapKodu;

                if (OnayTip == MyGlobalVariables.OnayTip.GMOnay.ToString())
                {
                    try
                    {
                        var evrakno = kkp.YeniEvrakNo(KKPKynkEvrakTip.AlımSiparişi, 1);
                        foreach (var item in MyGlobalVariables.TalepSource)
                        {
                            var sql = string.Format(@"UPDATE Kaynak.sta.Talep
	                                                SET GMOnaylayan=@Degistiren, GMOnayTarih=@DegisTarih, Durum=15, SipEvrakNo=@SipEvrakNo, SirketKodu='{0}', Degistiren=@Degistiren, DegisTarih=@DegisTarih, DegisSirKodu='{0}'
	                                                WHERE ID=@ID AND Durum=11 AND SipTalepNo IS NOT NULL", vUser.SirketKodu);

                            SqlParameter[] paramlist = new SqlParameter[4]
                            {
                                new SqlParameter("ID", SqlDbType.Int){ Value = item.ID},
                                new SqlParameter("Degistiren", SqlDbType.VarChar){ Value = vUser.UserName},
                                new SqlParameter("DegisTarih", SqlDbType.SmallDateTime){ Value = DateTime.Now},
                                new SqlParameter("SipEvrakNo", SqlDbType.VarChar){Value = evrakno}
                            };

                            kkp.ExecuteCommandOnUpdate(sql, true, paramlist);
                        }

                        foreach (var item in MyGlobalVariables.SipEvrak.Satirlar)
                        {
                            item.Aciklama = item.Aciklama.Length > 64 ? item.Aciklama.Substring(0, 64) : item.Aciklama;
                        }

                        kkp.SiparisEvrakList.Add(MyGlobalVariables.SipEvrak);
                        MyGlobalVariables.SipEvrak.MFK.Aciklama = "";
                        MyGlobalVariables.SipEvrak.MFK.Aciklama2 = "";
                        MyGlobalVariables.SipEvrak.MFK.Aciklama3 = "";
                        MyGlobalVariables.SipEvrak.MFK.Aciklama4 = "";
                        MyGlobalVariables.SipEvrak.MFK.Aciklama5 = "";
                        MyGlobalVariables.SipEvrak.MFK.Aciklama6 = "";
                        MyGlobalVariables.SipEvrak.EvrakNo = evrakno;
                        MyGlobalVariables.SipEvrak.HesapKodu = MyGlobalVariables.TalepSource[0].HesapKodu;
                        MyGlobalVariables.SipEvrak.TeslimYeriKodu = MyGlobalVariables.TalepSource[0].HesapKodu;
                        MyGlobalVariables.SipEvrak.Tarih = DateTime.Today;
                        MyGlobalVariables.SipEvrak.INIGuncellensin = true;
                        MyGlobalVariables.SipEvrak.TahTeslimTarihi = new DateTime(1900, 1, 1).AddDays(-2);

                        if (MyGlobalVariables.DovizDurum == false)
                        {
                            MyGlobalVariables.SipEvrak.FTDHesapla("", Convert.ToDecimal(0));
                        }
                        else
                        {
                            var kur = MyGlobalVariables.TalepSource[0].FTDDovizKuru.Value;
                            if (kur > 0)
                            {
                                MyGlobalVariables.SipEvrak.FTDHesapla(MyGlobalVariables.TalepSource[0].FTDDovizCinsi, kur);
                            }
                        }

                        kkp.UpdateChanges();
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "Approvals/Purchase/SipGMOnayla/1");
                        _Result.Message = "İşlem Sırasında Hata Oluştu.";
                        _Result.Status = false;
                        return Json(_Result, JsonRequestBehavior.AllowGet);
                    }

                    sipTarih = Convert.ToInt32(MyGlobalVariables.SipEvrak.Tarih.ToOADate());
                    sipEvrakNo = MyGlobalVariables.SipEvrak.EvrakNo;
                    hesapKodu = MyGlobalVariables.SipEvrak.HesapKodu;

                    siparisOnayMailGonderim = true;
                }
                else if (OnayTip == MyGlobalVariables.OnayTip.SatSipGMYMaliOnay.ToString())
                {
                    var ftdtoplam = MyGlobalVariables.GridFTD.Where(x => x.SatirTip == (short)12).FirstOrDefault();

                    if (ftdtoplam.Iskonto > limit && vUser.UserName.ToUpper() != "TUNCE")
                    {
                        try
                        {
                            foreach (var item in MyGlobalVariables.TalepSource)
                            {
                                //Durum 5: Teklif Bekliyor; 6: Teklif Değerlendirme; 7: Teklif Onaylandı; 8: Sipaiş Süreci, 11: Sipariş Ön Onay
                                var sql = string.Format(@"UPDATE Kaynak.sta.Talep
                                                        SET GMYMaliOnaylayan=@Degistiren, GMYMaliOnayTarih=@DegisTarih, Durum=11, Degistiren=@Degistiren, DegisTarih=@DegisTarih, DegisSirKodu={0}
                                                        WHERE ID=@ID AND Durum=8 AND SipTalepNo IS NOT NULL", vUser.SirketKodu);

                                SqlParameter[] paramlist = new SqlParameter[3]
                                {
                                    new SqlParameter("ID", SqlDbType.Int){ Value = item.ID},
                                    new SqlParameter("Degistiren", SqlDbType.VarChar){ Value = vUser.UserName},
                                    new SqlParameter("DegisTarih", SqlDbType.SmallDateTime){ Value = DateTime.Now}
                                };

                                kkp.ExecuteCommandOnUpdate(sql, true, paramlist);
                            }

                            kkp.UpdateChanges();
                        }
                        catch (Exception ex)
                        {
                            Logger(ex, "Approvals/Purchase/SatSipGMYMaliOnay/1");
                            _Result.Message = "İşlem Sırasında Hata Oluştu.";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        var mailayar = db.Database.SqlQuery<GenelAyarVeParams>(string.Format(@"
                        SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 6")).FirstOrDefault();

                        if (mailayar == null)
                        {
                            _Result.Message = "GM Onay Bilgilendirme Mail ayarları yapılandırılmamış!";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        var gmTanimlama = db.Database.SqlQuery<GenelAyarVeParams>(string.Format(@"
                        SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 2")).FirstOrDefault();

                        if (gmTanimlama == null)
                        {
                            _Result.Message = "GM Onay Bilgilendirme maili gönderilemdi. \"Genel Ayarlar ve Parametreler>Satınalma Sipariş Ayarları\" sekmesinde GM tanımlaması yapılandırılmamış!";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        var gmmail = db.Database.SqlQuery<string>(string.Format(@"
                        SELECT top(1)
                        REPLACE(
                        (
                            SELECT RTRIM(email) [data()] FROM Kaynak.usr.Users(NOLOCK)
                            WHERE UserName in (SELECT SiparisSorumlu FROM Kaynak.sta.Talep(nolock) WHERE SipEvrakNo = '{0}'))
                            FOR XML PATH('')
                        ),' ',';') as Email"
                        , sipEvrakNo)).FirstOrDefault();

                        if (string.IsNullOrEmpty(gmmail) || gmmail.Trim() == "")
                        {
                            _Result.Message = "Users tablosunda GM Kullanıcısı bulunamadı!";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        var kime = string.Format("{0};{1};{2}", gmmail, mailayar.MailTo, mailayar.MailCc);
                        var gorunenIsim = "GM Onay";
                        var konu = "Satınalma Sipariş GM Onay";
                        var icerik = string.Format("Onayınızı bekleyen bir satınalma sipariş talebi (Satınalma Sipariş Talep No:{0}) sizin ekranınızda beklemektedir.", sipEvrakNo);

                        var m = new MyMail(db, true);
                        m.MailHataMesajı = "GM'ye gönderilen Satınalma Sipariş Talebi bilgilendirme mailinde hata oluştu!! Mail Gönderilemedi!!";
                        m.MailBasariMesajı = "Satınalma Sipariş Talebi Bilgilendirme maili başarılı bir şekilde GM'ye gönderildi!!";

                        m.Gonder(kime, mailayar.MailCc, gorunenIsim, konu, icerik, null, vUser.UserName, fn.GetIPAddress());
                    }
                    else
                    {
                        try
                        {
                            var evrakno = kkp.YeniEvrakNo(KKPKynkEvrakTip.AlımSiparişi, 1);
                            foreach (var item in MyGlobalVariables.TalepSource)
                            {
                                var sql = string.Format(@"UPDATE Kaynak.sta.Talep
                                SET GMYMaliOnaylayan=@Degistiren, GMYMaliOnayTarih=@DegisTarih, Durum=15, SipEvrakNo=@SipEvrakNo, SirketKodu='{0}',
                                Degistiren=@Degistiren, DegisTarih=@DegisTarih, DegisSirKodu='{0}'
                                WHERE ID=@ID AND Durum = 8 AND SipTalepNo IS NOT NULL", vUser.SirketKodu);

                                SqlParameter[] paramlist = new SqlParameter[4]
                                {
                                new SqlParameter("ID", SqlDbType.Int){ Value = item.ID},
                                new SqlParameter("Degistiren", SqlDbType.VarChar){ Value = vUser.UserName},
                                new SqlParameter("DegisTarih", SqlDbType.SmallDateTime){ Value = DateTime.Now},
                                new SqlParameter("SipEvrakNo", SqlDbType.VarChar){Value = evrakno}
                                };

                                kkp.ExecuteCommandOnUpdate(sql, true, paramlist);
                            }

                            foreach (var item in MyGlobalVariables.SipEvrak.Satirlar)
                            {
                                item.Aciklama = item.Aciklama.Length > 64 ? item.Aciklama.Substring(0, 64) : item.Aciklama;
                            }

                            kkp.SiparisEvrakList.Add(MyGlobalVariables.SipEvrak);
                            MyGlobalVariables.SipEvrak.MFK.Aciklama = "";
                            MyGlobalVariables.SipEvrak.MFK.Aciklama2 = "";
                            MyGlobalVariables.SipEvrak.MFK.Aciklama3 = "";
                            MyGlobalVariables.SipEvrak.MFK.Aciklama4 = "";
                            MyGlobalVariables.SipEvrak.MFK.Aciklama5 = "";
                            MyGlobalVariables.SipEvrak.MFK.Aciklama6 = "";

                            MyGlobalVariables.SipEvrak.EvrakNo = evrakno;
                            MyGlobalVariables.SipEvrak.HesapKodu = MyGlobalVariables.TalepSource[0].HesapKodu;
                            MyGlobalVariables.SipEvrak.TeslimYeriKodu = MyGlobalVariables.TalepSource[0].HesapKodu;
                            MyGlobalVariables.SipEvrak.Tarih = DateTime.Today;
                            MyGlobalVariables.SipEvrak.INIGuncellensin = true;
                            MyGlobalVariables.SipEvrak.TahTeslimTarihi = new DateTime(1900, 1, 1).AddDays(-2);

                            if (MyGlobalVariables.DovizDurum == false)
                            {
                                MyGlobalVariables.SipEvrak.FTDHesapla("", Convert.ToDecimal(0));
                            }
                            else
                            {
                                var kur = MyGlobalVariables.TalepSource[0].FTDDovizKuru.Value;
                                if (kur > 0)
                                {
                                    MyGlobalVariables.SipEvrak.FTDHesapla(MyGlobalVariables.TalepSource[0].FTDDovizCinsi, kur);
                                }
                                else
                                {
                                    _Result.Message = "Döviz Kuru 0'dan büyük bir değer olmalıdır!!";
                                    _Result.Status = false;
                                    return Json(_Result, JsonRequestBehavior.AllowGet);
                                }
                            }

                            kkp.UpdateChanges();

                            sipTarih = Convert.ToInt32(MyGlobalVariables.SipEvrak.Tarih.ToOADate());
                            sipEvrakNo = MyGlobalVariables.TalepSource[0].TalepNo;
                            hesapKodu = MyGlobalVariables.SipEvrak.HesapKodu;
                        }
                        catch (Exception ex)
                        {
                            Logger(ex, "Approvals/Purchase/SatSipGMYMaliOnay/2");
                            _Result.Message = "İşlem Sırasında Hata Oluştu.";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        siparisOnayMailGonderim = true;
                    }
                }

                if (siparisOnayMailGonderim)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(sipEvrakNo) || string.IsNullOrEmpty(hesapKodu))
                            throw new ArgumentException("parametreler hatalı!");

                        var mailayar = db.Database.SqlQuery<GenelAyarVeParams>(string.Format(@"
                        SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 0")).FirstOrDefault();

                        if (mailayar == null)
                        {
                            _Result.Message = "Sipariş Onay Mail ayarları yapılandırılmamış!";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        var sorgu = string.Format("SELECT Kaynak.sta.TedarikciMail('{0}')", hesapKodu);

                        var sirketemail = db.Database.SqlQuery<string>(sorgu).FirstOrDefault();
                        if (string.IsNullOrEmpty(sirketemail) || sirketemail.Trim() == "")
                        {
                            _Result.Message = string.Format("Tedarikçiye ait mail bulunamadı! (Hesap Kodu: {0})", hesapKodu);
                            _Result.Status = false;
                        }

                        var satinalmacimail = db.Database.SqlQuery<string>(string.Format(@"
                    SELECT Email FROM usr.Users (nolock) WHERE Kod IN (SELECT TOP(1) Satinalmaci FROM Kaynak.sta.Talep(nolock) WHERE SipEvrakNo = '{0}' )", sipEvrakNo)).FirstOrDefault();

                        if ((string.IsNullOrEmpty(sirketemail) || sirketemail.Trim() == "")
                            && (string.IsNullOrEmpty(satinalmacimail) || satinalmacimail.Trim() == ""))
                        {
                            _Result.Message = "Ne Şirket Maili ne de Satınalmacı maili yapılandırılmamış. Mail gönderilemedi";
                            _Result.Status = false;
                        }

                        var sipTalep = db.Database.SqlQuery<SatTalep>(string.Format(@"
                    SELECT TOP (1) TalepNo, SipIslemTip FROM Kaynak.sta.Talep (nolock) WHERE SipEvrakNo = '{0}'", sipEvrakNo)).FirstOrDefault();

                        if (sipTalep == null)
                        {
                            _Result.Message = "Siparişin Talep ile ilişkisi bulunamadı! (Sipariş Onay Mail Gönderim)";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        if (sipTalep.SipIslemTip == null)
                        {
                            _Result.Message = "Sipariş iç/Dış Piyasa olduğu belirlenemedi!";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }
                        try
                        {
                            SatınalmaSiparisFormu sspf = new SatınalmaSiparisFormu(); ;
                            sspf.SatinalmaSiparisFormu(sipEvrakNo, hesapKodu, sipTarih, true, vUser.SirketKodu);
                        }
                        catch (Exception ex)
                        {
                            Logger(ex, "SatinalmaSiparisFormu");
                        }

                        List<string> attachList = new List<string>() { string.Format("{0}{1}.pdf", Path.GetTempPath(), sipEvrakNo) };

                        List<SatTalep> listTalep = db.Database.SqlQuery<SatTalep>(string.Format(@"
                    SELECT TalepNo, MalKodu, EkDosya FROM Kaynak.sta.Talep (nolock) WHERE SipEvrakNo ='{0}' AND HesapKodu = '{1}' AND ISNULL(EkDosya,'')<> '' ", sipEvrakNo, hesapKodu)).ToList();

                        var dosyaYolu = db.Database.SqlQuery<string>(string.Format(@"
                    SELECT top 1 DosyaYolu FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 7 and DosyaYolu IS NOT NULL")).FirstOrDefault();

                        foreach (SatTalep talep in listTalep)
                        {
                            var yol = string.Format("{0}SatTalep\\{1}\\{2}", dosyaYolu, talep.TalepNo, talep.EkDosya);
                            if (yol.FileExists()) attachList.Add(yol);
                        }

                        var kime = string.Format("{0};{1};{2}", sirketemail, satinalmacimail, mailayar.MailTo);
                        var gorunenIsim = "Sipariş Onay";
                        var konu = "Sipariş Onay";
                        var icerik = string.Format("{0} hesapkoduna ait evrak nosu {1} ve talep nosu {2} olan sipariş onaylanmıştır. Sipariş bilgileri ektedir.", hesapKodu, sipEvrakNo, sipTalep.TalepNo);
                        if (sipTalep.SipIslemTip == 2)
                        {
                            gorunenIsim = "Purchase Order Approval";
                            konu = "Purchase Order Approval";
                            icerik = "Purchase Order Items Information in Attachments";
                        }

                        var m = new MyMail(db, true)
                        {
                            MailHataMesajı = "Sipariş Onay Maili Gönderiminde hata oluştu! Mail Gönderilemedi!",
                            MailBasariMesajı = "Sipariş Onay Maili başarılı bir şekilde gönderildi!"
                        };
                        m.Gonder(kime.Replace(";;", ";"), mailayar.MailCc, gorunenIsim, konu, icerik, attachList, vUser.UserName, fn.GetIPAddress());
                        if (m.MailGonderimBasarili)
                        {
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE Kaynak.sta.Talep SET MailGonder=-1 WHERE TalepNo='{0}'", sipTalep.TalepNo));
                        }
                        else
                        {
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE Kaynak.sta.Talep SET MailGonder=0 WHERE TalepNo='{0}'", sipTalep.TalepNo));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger(ex, "Approvals/Purchase/SipGMOnayla/2");
                        _Result.Message = string.Format("Sipariş Onay Maili Gönderiminde hata oluştu! Mail Gönderilemedi!)");
                        _Result.Status = false;
                        return Json(_Result, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// gm için sipariş reddetme
        /// </summary>
        public JsonResult SipGMReddet(string redAciklama, string OnayTip)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true, "İşlem Başarılı");

            //kontrol 1
            if (redAciklama.IsNullEmpty())
            {
                _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            //kontrol 2
            if (MyGlobalVariables.GridSource == null || MyGlobalVariables.GridSource.Count == 0 || MyGlobalVariables.SipEvrak == null)
            {
                _Result.Message = "Siparis Seçmelisiniz!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            //transaction

            string sql = "";

            if (OnayTip == MyGlobalVariables.OnayTip.GMOnay.ToString())
            {
                sql = @"UPDATE Kaynak.sta.Talep
                SET GMOnaylayan='{0}', GMOnayTarih=GETDATE(), Durum=13,
                Degistiren='{0}', DegisTarih=GETDATE(), DegisSirKodu={2}, Aciklama2='{1}'
                WHERE ID={3} AND Durum=11 AND SipTalepNo IS NOT NULL";
            }
            else if (OnayTip == MyGlobalVariables.OnayTip.SatSipGMYMaliOnay.ToString())
            {
                ///Durum 5: Teklif Bekliyor; 8: Sipariş Süreci, 9: SiparisOnOnayIptal, 11: Sipariş Ön Onay, 15: Onaylı Sipariş
                sql = @"UPDATE Kaynak.sta.Talep
                SET GMYMaliOnaylayan='{0}', GMYMaliOnayTarih=GETDATE(), Durum=9,
                Degistiren='{0}', DegisTarih=GETDATE(), DegisSirKodu={2}, Aciklama2='{1}'
                WHERE ID={3} AND Durum=8 AND SipTalepNo IS NOT NULL";
            }
            string satinalmaci = "";
            string kaydeden = "";
            string aciklama2 = "";
            using (var con = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in MyGlobalVariables.TalepSource)
                    {
                        db.Database.ExecuteSqlCommand(string.Format(sql, vUser.UserName.ToString(), redAciklama, vUser.SirketKodu, item.ID));
                        satinalmaci = item.Satinalmaci;
                        kaydeden = item.Kaydeden;
                        aciklama2 = item.Aciklama2;
                    }

                    con.Commit();
                }
                catch (Exception)
                {
                    con.Rollback();
                    _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!";
                    _Result.Status = false;
                }
            }

            if (OnayTip == MyGlobalVariables.OnayTip.SatSipGMYMaliOnay.ToString())
            {
                try
                {
                    var mailayar = db.Database.SqlQuery<GenelAyarVeParams>(string.Format(@"
                                SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 3")).FirstOrDefault();

                    if (mailayar == null)
                    {
                        _Result.Message = "Talep Geri Çevirme Mail ayarları yapılandırılmamış!";
                        _Result.Status = false;
                        return Json(_Result, JsonRequestBehavior.AllowGet);
                    }

                    var satinalmacimail = db.Database.SqlQuery<string>(string.Format(@"
                    SELECT Email FROM Kaynak.usr.Users(nolock) WHERE UserName ='{0}'
                    ", satinalmaci)).FirstOrDefault();

                    if ((string.IsNullOrEmpty(satinalmacimail) || satinalmacimail.Trim() == ""))
                    {
                        _Result.Message = "Satınalmacının mail ayarları girilmemiş";
                        _Result.Status = false;
                        return Json(_Result, JsonRequestBehavior.AllowGet);
                    }

                    var TLPKaydedenMail = db.Database.SqlQuery<GenelAyarVeParams>(string.Format(@"
                    SELECT Email FROM Kaynak.usr.Users (nolock) WHERE UserName='{0}'", kaydeden)).FirstOrDefault();

                    var kime = string.Format("{0};{1};{2}", satinalmacimail, TLPKaydedenMail, mailayar.MailTo);
                    var gorunenIsim = "Sipariş Talebi";
                    var konu = "Sipariş Talebi Uygun görülmedi!";

                    var m = new MyMail(db, true)
                    {
                        MailHataMesajı = "Teklif Geri Çevirme Maili Gönderiminde hata oluştu! Mail Gönderilemedi!",
                        MailBasariMesajı = "Teklif Geri Çevirme Maili başarılı bir şekilde gönderildi!"
                    };

                    var sw = new StringWriter();
                    var wr = new HtmlTextWriter(sw);
                    wr.RenderBeginTag(HtmlTextWriterTag.P);
                    wr.Write(aciklama2);
                    wr.RenderEndTag();
                    var icerik = sw.ToString();
                    icerik += MyMail.TalepGeriCevirmeIcerik(MyGlobalVariables.TalepSource);

                    sw = new StringWriter();
                    wr = new HtmlTextWriter(sw);
                    wr.RenderBeginTag(HtmlTextWriterTag.P);
                    wr.Write("Talebi Geri Çeviren : " + vUser.UserName.ToString());
                    wr.RenderEndTag();

                    icerik += sw.ToString();
                    m.Gonder(kime.Replace(";;", ";"), mailayar.MailCc, gorunenIsim, konu, icerik, null, vUser.UserName, fn.GetIPAddress());
                }
                catch (Exception ex)
                {
                    Logger(ex, "Approvals/Purchase/GMYReddet/2");
                    _Result.Message = string.Format("Teklif Geri Çevirme Maili Gönderiminde hata oluştu! Mail Gönderilemedi!)");
                    _Result.Status = false;
                    return Json(_Result, JsonRequestBehavior.AllowGet);
                }
            }
            //_Result = new Result(true);

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public int BirimDegisimKontrol(string talepNo, string hesapKodu)
        {
            List<int> brmContList = null;
            var query = string.Format(@"
            SELECT (CASE WHEN ST.Birim = STK.Birim1 THEN 1
		        WHEN ST.Birim = STK.Birim2 THEN 1
		        WHEN ST.Birim = STK.Birim3 THEN 1
		        WHEN ST.Birim = STK.Birim4 THEN 1
		        ELSE 0 END ) AS Miktar
            FROM Kaynak.sta.Talep as ST (nolock)
            LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on ST.MalKodu=STK.MalKodu
            WHERE ST.Durum=11 AND ST.SipTalepNo={1} AND ST.HesapKodu='{2}'
            GROUP BY (CASE WHEN ST.Birim = STK.Birim1 THEN 1 WHEN ST.Birim = STK.Birim2 THEN 1 WHEN ST.Birim = STK.Birim3 THEN 1
            WHEN ST.Birim = STK.Birim4 THEN 1 ELSE 0 END )", vUser.SirketKodu, talepNo, hesapKodu);
            brmContList = db.Database.SqlQuery<int>(query).ToList();

            if (brmContList.Count == 0)
                return 1;
            else if (brmContList.Count > 1 || brmContList[0] == 0)
                return 0;
            else
                return 1;
        }

        public ActionResult SatGMContext()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            ViewBag.ContextType = id;
            return PartialView();
        }

        public PartialViewResult TeklifList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            ViewBag.id = id;
            List<SatTeklif> TL = db.Database.SqlQuery<SatTeklif>(string.Format("[FINSAT6{0}].[wms].[TeklifListesi] @MalKodu='{1}'", vUser.SirketKodu, id)).ToList();

            return PartialView(TL);
        }

        public PartialViewResult OnayliTedarikciList()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            ViewBag.id = id;
            List<SatOnayliTed> OTL = db.Database.SqlQuery<SatOnayliTed>(string.Format("[FINSAT6{0}].[wms].[OnayliTedarikciListesi] @MalKodu='{1}'", vUser.SirketKodu, id)).ToList();
            return PartialView(OTL);
        }

        public PartialViewResult SonAlimListesi()
        {
            var id = Url.RequestContext.RouteData.Values["id"];
            ViewBag.id = id;
            List<SonAlimListesi> SAL = db.Database.SqlQuery<SonAlimListesi>(string.Format("[FINSAT6{0}].[wms].[SonAlimListesi] @MalKodu='{1}'", vUser.SirketKodu, id)).ToList();

            return PartialView(SAL);
        }

        public ActionResult GMYTedarikci_Onay()
        {
            if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Reading) == false) return Redirect("/");

            ViewBag.OnayTip = MyGlobalVariables.OnayTip.GMYTedarikciOnay;
            ViewBag.baslik = "Teklif GMY Tedarikçi Onay";

            MyGlobalVariables.GMYOnayList = db.Database.SqlQuery<SatTalep>(string.Format(@"
            SELECT DISTINCT ST.KynkTalepNo AS TalepNo, (select TOP(1) Tp.Kaydeden FROM KAYNAK.sta.Talep as Tp (nolock) where Tp.TalepNo = St.KynkTalepNo) AS TalepEden
            FROM    KAYNAk.sta.Teklif as ST (nolock) LEFT JOIN 
                        FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=ST.MalKodu
            WHERE ST.Durum=2 AND  ST.KynkTalepNo<>''
            GROUP BY ST.KynkTalepNo
            ORDER BY ST.KynkTalepNo", vUser.SirketKodu)).ToList();

            return View("GMY_Onay", MyGlobalVariables.GMYOnayList);
        }

        public ActionResult GMYMali_Onay()
        {
            if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Reading) == false) return Redirect("/");

            ViewBag.OnayTip = MyGlobalVariables.OnayTip.GMYMaliOnay;
            ViewBag.baslik = "Teklif GMY Mali Onay";

            MyGlobalVariables.GMYOnayList = db.Database.SqlQuery<SatTalep>(string.Format(@"
            SELECT DISTINCT ST.KynkTalepNo AS TalepNo, (select TOP(1)Tp.Kaydeden FROM KAYNAK.sta.Talep(nolock) Tp where Tp.TalepNo = ST.KynkTalepNo) AS TalepEden
            FROM    KAYNAK.sta.Teklif as ST (nolock) LEFT JOIN 
                        FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=ST.MalKodu
            WHERE ST.Durum=4
            GROUP BY ST.KynkTalepNo
            ORDER BY ST.KynkTalepNo", vUser.SirketKodu)).ToList();

            return View("GMY_Onay", MyGlobalVariables.GMYOnayList);
        }

        public PartialViewResult GMYOnayList(string TalepNo, string OnayTip)
        {
            if (OnayTip == MyGlobalVariables.OnayTip.GMYTedarikciOnay.ToString())
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Reading) == false) return null;
                else if (OnayTip == MyGlobalVariables.OnayTip.GMYMaliOnay.ToString())
                    if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Reading) == false) return null;

            ViewBag.TalepNo = TalepNo;
            ViewBag.OnayTip = OnayTip;
            return PartialView("GMYOnay_List");
        }

        public string GMYOnayListData(string TalepNo, string OnayTip)
        {
            if (OnayTip == MyGlobalVariables.OnayTip.GMYTedarikciOnay.ToString())
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Reading) == false) return null;
                else if (OnayTip == MyGlobalVariables.OnayTip.GMYMaliOnay.ToString())
                    if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Reading) == false) return null;

            if (MyGlobalVariables.GMYSource == null)
                MyGlobalVariables.GMYSource = new List<SatTalep>();
            else
                MyGlobalVariables.GMYSource.Clear();

            if (OnayTip == MyGlobalVariables.OnayTip.GMYTedarikciOnay.ToString())
            {
                MyGlobalVariables.GMYSource = db.Database.SqlQuery<SatTalep>(string.Format(@"
                SELECT ST.ID, ST.TeklifNo, ST.Tarih, ST.HesapKodu, ST.MalKodu,
                (SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK (NOLOCK) WHERE MalKodu = ST.MalKodu) AS MalAdi, ST.Birim,
                ST.BirimFiyat, ST.TeklifMiktar, ST.Durum,
                ST.DvzTL, ST.DvzCinsi, ST.TerminSure,
                ST.MinSipMiktar,
                CONVERT(DATETIME,ST.TeklifBasTarih) AS TeklifBasTarih,
			    CONVERT(DATETIME,ST.TeklifBitTarih) AS TeklifBitTarih,
                ST.OneriDurum,
                ST.Vade, ST.TeslimYeri,
                ST.Aciklama, ST.Aciklama2, ST.Aciklama3,ST.TeklifAciklamasi, ST.Satinalmaci,

                ST.Kademe2Onaylayan, ST.Kademe2OnayTarih,
                ST.Kademe1Onaylayan, ST.Kademe1OnayTarih,
                ST.Durum2, ST.KynkTalepNo, ST.KynkTalepEkDosya,

                ST.Kaydeden, ST.KayitTarih,
                ST.Degistiren, ST.DegisTarih,

                ISNULL((select TOP(1)Tp.TesisKodu FROM KAYNAK.sta.Talep as Tp (nolock) where Tp.TalepNo = St.KynkTalepNo),'') AS TesisKodu,
                ISNULL((select TOP(1) (select MMK.HesapAd from MUHASEBE6{0}.MUHASEBE6{0}.MMK (nolock) WHERE MMK.HesapKod=Tp.TesisKodu)
                FROM KAYNAK.sta.Talep (nolock) Tp where Tp.TalepNo = St.KynkTalepNo),'') AS TesisAdi,

                STK.MalAdi, CHK.Unvan1 as Unvan,
                ISNULL(AT.AcikTalepMiktar,0) as AcikTalepMiktar,
                CASE WHEN ST.DvzCinsi='JPY' then DVZ.Kur01/100 else DVZ.Kur01 end as DvzKuru,
                (select TOP(1)Tp.Kaydeden FROM KAYNAK.sta.Talep as Tp (nolock) where Tp.TalepNo = St.KynkTalepNo) AS TLPKaydeden,
                ST.TeklifMiktar AS BirimMiktar

                FROM KAYNAK.sta.Teklif as ST (nolock)
                LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=ST.MalKodu
                LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) on CHK.HesapKodu=ST.HesapKodu
                LEFT JOIN
                (
	                SELECT MalKodu, SUM(BirimMiktar) as AcikTalepMiktar FROM KAYNAK.sta.Talep (nolock)
	                WHERE
	                --Durum NOT IN ({1}) AND
	                Durum < 15
	                GROUP BY MalKodu
                ) as AT on AT.MalKodu=ST.MalKodu
                LEFT JOIN SOLAR6.dbo.DVZ (nolock) on DVZ.DovizCinsi=ST.DvzCinsi AND DVZ.Tarih=CAST( DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))+2 AS INT)
                WHERE
                ST.KynkTalepNo='{1}' AND ST.Durum=2", vUser.SirketKodu, TalepNo)).ToList();
            }
            else if (OnayTip == MyGlobalVariables.OnayTip.GMYMaliOnay.ToString())
            {
                MyGlobalVariables.GMYSource = db.Database.SqlQuery<SatTalep>(string.Format(@"
                SELECT ST.ID, ST.TeklifNo, ST.Tarih, ST.HesapKodu,
                    ST.MalKodu, (SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK (NOLOCK) WHERE MalKodu = ST.MalKodu) AS MalAdi, ST.Birim,
                    ST.BirimFiyat, ST.TeklifMiktar, ST.Durum,
                    ST.DvzTL, ST.DvzCinsi, ST.TerminSure,
                    ST.MinSipMiktar,
                    CONVERT(DATETIME,ST.TeklifBasTarih) AS TeklifBasTarih,
			        CONVERT(DATETIME,ST.TeklifBitTarih) AS TeklifBitTarih,
                    ST.OneriDurum,
                    ST.Vade, ST.TeslimYeri,
                    ST.Aciklama, ST.Aciklama2, ST.Aciklama3,ST.TeklifAciklamasi, ST.Satinalmaci,

                    ST.Kademe2Onaylayan, ST.Kademe2OnayTarih,
                    ST.Kademe1Onaylayan, ST.Kademe1OnayTarih,
                    ST.Durum2, ST.KynkTalepNo, ST.KynkTalepEkDosya,

                    ST.Kaydeden, ST.KayitTarih,
                    ST.Degistiren, ST.DegisTarih,

                    ISNULL((select TOP(1)Tp.TesisKodu FROM KAYNAK.sta.Talep as Tp (nolock) where Tp.TalepNo = St.KynkTalepNo),'') AS TesisKodu,
                    ISNULL((select TOP(1) (select MMK.HesapAd from MUHASEBE6{0}.MUHASEBE6{0}.MMK (nolock) WHERE MMK.HesapKod=Tp.TesisKodu)
                    FROM KAYNAK.sta.Talep (nolock) Tp where Tp.TalepNo = St.KynkTalepNo),'') AS TesisAdi,

                    STK.MalAdi, CHK.Unvan1 as Unvan,
                    ISNULL(AT.AcikTalepMiktar,0) as AcikTalepMiktar,
                    CASE WHEN ST.DvzCinsi='JPY' then DVZ.Kur01/100 else DVZ.Kur01 end as DvzKuru,
                    (select TOP(1)Tp.Kaydeden FROM KAYNAK.sta.Talep as Tp (nolock) where Tp.TalepNo = St.KynkTalepNo) AS TLPKaydeden,
                    ST.TeklifMiktar AS BirimMiktar

                FROM KAYNAK.sta.Teklif as ST (nolock)
                LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=ST.MalKodu
                LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) on CHK.HesapKodu=ST.HesapKodu
                LEFT JOIN
                (
	               SELECT MalKodu, SUM(BirimMiktar) as AcikTalepMiktar FROM KAYNAK.sta.Talep (nolock)
	               WHERE Durum NOT IN (3, 4, 9, 13) AND Durum < 15
	               GROUP BY MalKodu
                 ) as AT on AT.MalKodu=ST.MalKodu
                 LEFT JOIN SOLAR6.dbo.DVZ (nolock) on DVZ.DovizCinsi=ST.DvzCinsi AND DVZ.Tarih=CAST( DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))+2 AS INT)
                WHERE ST.KynkTalepNo='{1}' AND ST.Durum=4", vUser.SirketKodu, TalepNo)).ToList();
            }

            var json = new JavaScriptSerializer().Serialize(MyGlobalVariables.GMYSource);
            return json;
        }

        /// <summary>
        /// gmy onaylama
        /// </summary>
        public JsonResult GMYOnayla(string OnayTip)
        {
            if (OnayTip == MyGlobalVariables.OnayTip.GMYTedarikciOnay.ToString())
            {
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Writing) == false)
                    return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            }
            else if (OnayTip == MyGlobalVariables.OnayTip.GMYMaliOnay.ToString())
                if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Writing) == false)
                    return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);

            var _Result = new Result(true, "İşlem Başarılı");

            if (MyGlobalVariables.GMYSource.Where(x => x.OneriDurum).Count() == 0)
            {
                _Result.Message = "Satınalmacı tarafından öneri yapılmamış. İşlem devam edemez!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            using (var con = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in MyGlobalVariables.GMYSource)
                    {
                        string sql = "";

                        if (OnayTip == MyGlobalVariables.OnayTip.GMYTedarikciOnay.ToString())
                        {
                            sql = string.Format(@"UPDATE Kaynak.sta.Teklif SET
	                        Durum=4, Aciklama2='{0}', Degistiren='{1}', DegisTarih=GETDATE(), DegisSirKodu='{3}',
                            Kademe2Onaylayan='{1}', Kademe2OnayTarih=GETDATE()
	                        WHERE ID={2} AND Durum=2", item.Aciklama2, vUser.UserName.ToString(), item.ID, vUser.SirketKodu);

                            db.Database.ExecuteSqlCommand(sql);
                        }
                        else if (OnayTip == MyGlobalVariables.OnayTip.GMYMaliOnay.ToString())
                        {
                            sql = string.Format(@"UPDATE Kaynak.sta.Teklif SET
                            Durum={0}, Aciklama3='{1}', Degistiren='{2}', DegisTarih=GETDATE(), DegisSirKodu='{4}',
                            Kademe1Onaylayan='{2}', Kademe1OnayTarih=GETDATE(), Durum2=1
                            WHERE ID={3} AND Durum=4", item.OneriDurum ? 6 : 5, item.Aciklama3, vUser.UserName.ToString(), item.ID, vUser.SirketKodu);

                            db.Database.ExecuteSqlCommand(sql);

                            if (item.OneriDurum)
                            {
                                if (!string.IsNullOrWhiteSpace(item.KynkTalepNo))
                                {
                                    ///Talep Durum 5: Teklif Bekliyor;  6: Teklif Değerlendirme; 7: Teklif Onaylandı
                                    sql = string.Format(@"UPDATE Kaynak.sta.Talep SET Durum=7, Degistiren='{0}', DegisTarih=GETDATE(), DegisSirKodu='{3}'
                                    WHERE TalepNo='{1}' AND MalKodu='{2}' AND Durum in (5,6)", vUser.UserName.ToString(), item.KynkTalepNo, item.MalKodu, vUser.SirketKodu);

                                    db.Database.ExecuteSqlCommand(sql);
                                }

                                var maxSiraNo = -1;
                                var OnayliTedSiraNo = db.Database.SqlQuery<short>(string.Format(@"
                                SELECT ISNULL(MAX(SiraNo),0) FROM Kaynak.sta.OnayliTed (nolock) WHERE MalKodu='{0}'"
                                , item.MalKodu)).FirstOrDefault();

                                if (OnayliTedSiraNo > 0)
                                    maxSiraNo = OnayliTedSiraNo;

                                sql = string.Format(@"INSERT INTO Kaynak.sta.OnayliTed
                                (TeklifNo, HesapKodu, MalKodu, SiraNo, Kaydeden, KayitTarih, KayitSirKodu, Degistiren, DegisTarih, DegisSirKodu)
                                VALUES
                                ('{0}', '{1}', '{2}', {3}, '{4}', GETDATE(), '{5}', '{4}', GETDATE(), '{5}')"
                                , item.TeklifNo, item.HesapKodu, item.MalKodu
                                , maxSiraNo == -1 ? (short)1 : (short)maxSiraNo + 1
                                , vUser.UserName.ToString(), vUser.SirketKodu);

                                db.Database.ExecuteSqlCommand(sql);
                            }
                        }
                    }
                    con.Commit();
                }
                catch (Exception)
                {
                    con.Rollback();
                    _Result.Message = "İşlem Sırasında Hata Oluştu.";
                    _Result.Status = false;
                }
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// gmy reddetme
        /// </summary>
        public JsonResult GMYReddet(string redAciklama, string OnayTip)
        {
            if (OnayTip == MyGlobalVariables.OnayTip.GMYTedarikciOnay.ToString())
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
                else if (OnayTip == MyGlobalVariables.OnayTip.GMYMaliOnay.ToString())
                    if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);

            var _Result = new Result(true, "İşlem Başarılı");

            if (MyGlobalVariables.GMYSource.Where(x => x.OneriDurum).Count() == 0)
            {
                _Result.Message = "Satınalmacı tarafından öneri yapılmamış. İşlem devam edemez!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
            string TLPKaydeden = "";
            using (var con = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in MyGlobalVariables.GMYSource)
                    {
                        string sql = "";
                        if (TLPKaydeden == "")
                            TLPKaydeden = "'" + item.TLPKaydeden + "'";
                        else
                            TLPKaydeden += ", '" + item.TLPKaydeden + "'";

                        if (OnayTip == MyGlobalVariables.OnayTip.GMYTedarikciOnay.ToString())
                        {
                            sql = string.Format(@"UPDATE Kaynak.sta.Teklif SET
                            Durum=3, Aciklama2='{0}', Kademe2Onaylayan='{1}', Kademe2OnayTarih=GETDATE(),
                            Degistiren='{1}', DegisTarih=GETDATE(), DegisSirKodu='{3}'
                            WHERE ID={2} AND Durum=2", redAciklama, vUser.UserName.ToString(), item.ID, vUser.SirketKodu);
                        }
                        else if (OnayTip == MyGlobalVariables.OnayTip.GMYMaliOnay.ToString())
                        {
                            sql = string.Format(@"UPDATE Kaynak.sta.Teklif SET
                            Durum=5, Aciklama3='{0}', Kademe1Onaylayan='{1}', Kademe1OnayTarih=GETDATE(),
                            Degistiren='{1}', DegisTarih=GETDATE(), DegisSirKodu='{3}'
                            WHERE ID={2} AND Durum=4", redAciklama, vUser.UserName.ToString(), item.ID, vUser.SirketKodu);
                        }

                        db.Database.ExecuteSqlCommand(sql);

                        try
                        {
                            var mailayar = db.Database.SqlQuery<GenelAyarVeParams>(string.Format(@"
                                SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 4")).FirstOrDefault();

                            if (mailayar == null)
                            {
                                _Result.Message = "Teklif Geri Çevirme Mail ayarları yapılandırılmamış!";
                                _Result.Status = false;
                                return Json(_Result, JsonRequestBehavior.AllowGet);
                            }

                            var satinalmacimail = db.Database.SqlQuery<string>(string.Format(@"
                                SELECT top(1)
                                REPLACE(
                                (
                                    SELECT RTRIM(email) [data()] FROM Kaynak.usr.Users(NOLOCK) WHERE UserName in ({0})
                                    FOR XML PATH('')
                                ),' ',';') as Email", TLPKaydeden)).FirstOrDefault();

                            if ((string.IsNullOrEmpty(satinalmacimail) || satinalmacimail.Trim() == ""))
                            {
                                _Result.Message = "Mail bilgileri olmadığından geri çevirme maili gönderilemedi";
                                _Result.Status = false;
                            }

                            var kime = string.Format("{0};{1}", satinalmacimail, mailayar.MailTo);
                            var gorunenIsim = "Teklif";
                            var konu = "Teklif Uygun görülmedi.";

                            var m = new MyMail(db, true)
                            {
                                MailHataMesajı = "Geri Çevirme Maili Gönderiminde hata oluştu! Mail Gönderilemedi!",
                                MailBasariMesajı = "Geri Çevirme Maili başarılı bir şekilde gönderildi!"
                            };

                            var icerik = MyMail.TeklifGeriCevirmeMailIcerik(MyGlobalVariables.GMYSource, redAciklama);
                            m.Gonder(kime.Replace(";;", ";"), mailayar.MailCc, gorunenIsim, konu, icerik, null, vUser.UserName.ToString(), fn.GetIPAddress());
                        }
                        catch (Exception ex)
                        {
                            Logger(ex, "Approvals/Purchase/GMYReddet/2");
                            _Result.Message = string.Format("Teklif Geri Çevirme Maili Gönderiminde hata oluştu! Mail Gönderilemedi!)");
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }
                    }

                    con.Commit();
                }
                catch (Exception)
                {
                    con.Rollback();
                    _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!";
                    _Result.Status = false;
                }
            }

            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SatinAlmaTalebiGMYOnay(bool? onay)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return Redirect("/");

            var sorgu = string.Format("SELECT DISTINCT ST.TalepNo,ST.ID, ST.MalKodu, ST.Tarih, ST.Birim, ST.BirimMiktar, ST.IstenenTarih, ST.Aciklama, ST.Aciklama2, ST.Aciklama3, ST.Durum, ST.EkDosya, ST.Kademe2Onaylayan, ST.Kademe2OnayTarih, ST.Kaydeden AS TalepEden, STK.MalAdi, ST.TesisKodu FROM KAYNAK.sta.Talep as ST (nolock) LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK(nolock) on ST.MalKodu = STK.MalKodu WHERE Tip = 0 AND ST.Durum in ({1}) AND ST.Kademe2Onaylayan  IN(SELECT DISTINCT Talep2KademeOnaylayan FROM Kaynak.sta.GenelAyarVeParams WHERE Tip = 0 AND Talep1KademeOnaylayan = '{2}')", vUser.SirketKodu, "1", vUser.UserName);

            ViewBag.baslik = "Satınalma Sipariş Talebi GMY Mali Onay";
            MyGlobalVariables.Depo = "93 DP";
            MyGlobalVariables.DovizDurum = false;

            if (onay == null)
            {
                MyGlobalVariables.SipTalepList = new List<SatTalep>();
            }
            else
            {
                if (!(bool)onay)
                {
                    MyGlobalVariables.SipTalepList = db.Database.SqlQuery<SatTalep>(sorgu).ToList();
                }
                else
                {
                    sorgu = string.Format("SELECT DISTINCT ST.TalepNo,ST.ID, ST.MalKodu, ST.Tarih, ST.Birim, ST.BirimMiktar, ST.IstenenTarih, ST.Aciklama, ST.Aciklama2, ST.Aciklama3, ST.Durum, ST.EkDosya, ST.Kademe2Onaylayan, ST.Kademe2OnayTarih, ST.Kaydeden AS TalepEden, STK.MalAdi, ST.TesisKodu FROM KAYNAK.sta.Talep as ST (nolock) LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK(nolock) on ST.MalKodu = STK.MalKodu WHERE Tip = 0 AND ST.Durum in ({1}) AND ST.Kademe2Onaylayan  IN(SELECT DISTINCT Talep2KademeOnaylayan FROM Kaynak.sta.GenelAyarVeParams WHERE Tip = 0 AND Talep1KademeOnaylayan = '{2}')", vUser.SirketKodu, "1,2", vUser.UserName);
                    MyGlobalVariables.SipTalepList = db.Database.SqlQuery<SatTalep>(sorgu).ToList();
                }
            }

            return View("SatinAlmaTalebiGMYOnay", MyGlobalVariables.SipTalepList);
        }

        public JsonResult SatinAlmaTalebiGMYOnayUrunDetay(string id)
        {
            var sql = string.Format(
            @"SELECT DST.MalKodu, STK.Birim1, DST.Depo, DEP.DepoAdi, DST.DvrMiktar, DST.GirMiktar, DST.CikMiktar,
            (DST.DvrMiktar+DST.GirMiktar-DST.CikMiktar) AS StokMiktar
            FROM FINSAT6{0}.FINSAT6{0}.DST (nolock)
            LEFT JOIN FINSAT6{0}.FINSAT6{0}.DEP (nolock) on DEP.Depo=DST.Depo
            LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=DST.MalKodu
            WHERE DST.MalKodu='{1}'", vUser.SirketKodu, id);

            List<SatMalListesi> sonuc = db.Database.SqlQuery<SatMalListesi>(sql).ToList();

            return Json(sonuc, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public HttpStatusCodeResult SatinAlmaTalebiGMYOnayYap(int[] id, bool durum, string aciklama = null)
        {
            int onay = durum ? 2 : 4;

            var data = db.Database.SqlQuery<SatTalep>(string.Format(@"[FINSAT6{0}].[wms].[SatinAlmaTalepGMYOnayList]", vUser.SirketKodu)).ToList();

            foreach (var item in data.Where(p => id.Contains(p.ID)))
            {
                var sorgu = string.Format(@"UPDATE Kaynak.sta.Talep SET Durum={3},Kademe2OnayTarih=GETDATE(),Kademe2Onaylayan='{0}',Degistiren='{0}',DegisTarih=GETDATE(),DegisSirKodu={1} WHERE ID={2} AND Durum=1", vUser.UserName, vUser.SirketKodu, item.ID, onay);

                if (!durum)
                {
                    sorgu = string.Format(@"UPDATE Kaynak.sta.Talep SET Durum={3},Kademe2OnayTarih=GETDATE(),Kademe2Onaylayan='{0}',Degistiren='{0}',DegisTarih=GETDATE(),DegisSirKodu='{1}',Aciklama3='{4}' WHERE ID={2} AND Durum IN (1,2)", vUser.UserName, vUser.SirketKodu, item.ID, onay, aciklama);

                    db.Database.ExecuteSqlCommand(sorgu);

                    var mailSorgu = string.Format("SELECT Email FROM Kaynak.usr.Users WHERE UserName= ( SELECT TOP (1) Kaydeden FROM Kaynak.sta.Talep WHERE ID='{0}')", item.ID);
                    var kaydedenmail = db.Database.SqlQuery<string>(mailSorgu).FirstOrDefault().ToString();

                    var mailayar = db.Database.SqlQuery<GenelAyarVeParams>(string.Format(@"
                    SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 0")).FirstOrDefault();

                    var kime = string.Format("{0};{1};{2}", vUser.Email, kaydedenmail, mailayar.MailTo);
                    var gorunenIsim = "Sipariş Onay";
                    var konu = "Sipariş Onay";
                    var icerik = string.Format("Talep nosu {0} olan {1} nedeni ile iptal edilmişir.", item.TalepNo, aciklama);

                    var m = new MyMail(db);
                    m.Gonder(kime.Replace(";;", ";"), mailayar.MailCc, gorunenIsim, konu, icerik, null, vUser.UserName, fn.GetIPAddress());

                    if (m.MailGonderimBasarili)
                    {
                        db.Database.ExecuteSqlCommand(string.Format("UPDATE Kaynak.sta.Talep SET MailGonder=-1 WHERE ID='{0}'", item.ID));
                    }
                }
            }

            return new HttpStatusCodeResult(200);
        }
    }
}