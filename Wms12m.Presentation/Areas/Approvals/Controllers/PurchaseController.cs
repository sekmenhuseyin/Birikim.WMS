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
using Wms12m.Entity;

namespace Wms12m.Presentation.Areas.Approvals.Controllers
{

    public class PurchaseController : RootController
    {
        /// <summary>
        /// gm sipariş onaylama sayfası
        /// </summary>
        public ActionResult GMOnayHTML()
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }

        public ActionResult GM_Onay()
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return Redirect("/");
            MyGlobalVariables.Depo = "93 DP";
            MyGlobalVariables.DovizDurum = false;
            MyGlobalVariables.SipTalepList = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[wms].[SatinAlmaGMOnayList]", vUser.SirketKodu)).ToList();
            return View("GM_Onay", MyGlobalVariables.SipTalepList);
        }

        public PartialViewResult SipGMOnayList(string HesapKodu, int SipTalepNo)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return null;

            ViewBag.HesapKodu = JsonConvert.SerializeObject(HesapKodu);
            ViewBag.SipTalepNo = SipTalepNo;
            return PartialView("GMOnay_List");
        }

        public PartialViewResult SipGMOnayListFTD(string HesapKodu, int SipTalepNo)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return null;

            ViewBag.HesapKodu = JsonConvert.SerializeObject(HesapKodu);
            ViewBag.SipTalepNo = SipTalepNo;
            return PartialView("GMOnayFTD_List");
        }

        public string SipGMOnayListData(string HesapKodu, int SipTalepNo)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Reading) == false) return null;
            MyGlobalVariables.DovizDurum = false;
            if (MyGlobalVariables.GridSource == null)
                MyGlobalVariables.GridSource = new List<KKP_SPI>();
            else
                MyGlobalVariables.GridSource.Clear();

            MyGlobalVariables.SipEvrak = new KKPEvrakSiparis(KKPSiparisTip.AlimSiparisi);

            MyGlobalVariables.TalepSource = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[wms].[SatinAlmaGMOnayListData] @HesapKodu='{1}', @SipTalepNo={2} ", vUser.SirketKodu, HesapKodu, SipTalepNo)).ToList();
            if (MyGlobalVariables.TalepSource[0].SipIslemTip == null || (MyGlobalVariables.TalepSource[0].SipIslemTip != 1 && MyGlobalVariables.TalepSource[0].SipIslemTip != 2))
                return "";

            MyGlobalVariables.SipEvrak.IslemTip = (KKPIslemTipSPI)MyGlobalVariables.TalepSource[0].SipIslemTip;
            MyGlobalVariables.SipEvrak.dvzTL = (KKPDvzTL)(short)MyGlobalVariables.TalepSource[0].FTDDovizTL;
            short siraNo = 0;
            foreach (var item in MyGlobalVariables.TalepSource)
            {
                if ((short)item.DvzTL == (short)1)
                {
                    MyGlobalVariables.DovizDurum = true;
                }

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
                {
                    MyGlobalVariables.GridFTD = new List<KKP_FTD>();
                }
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
        public JsonResult SipGMOnayla()
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true, "İşlem Başarılı");
            if (MyGlobalVariables.TalepSource == null)
            {
                _Result.Message = "Sipariş Seçin";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            using (KKP kkp = new KKP(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, vUser.SirketKodu))
            {
                try
                {
                    var evrakno = kkp.YeniEvrakNo(KKPKynkEvrakTip.AlımSiparişi, 1);
                    foreach (var item in MyGlobalVariables.TalepSource)
                    {
                        var sql = string.Format(@"UPDATE Kaynak.sta.Talep
	                        SET GMOnaylayan=@Degistiren, GMOnayTarih=@DegisTarih, Durum=15, SipEvrakNo=@SipEvrakNo
	                        , SirketKodu='{0}'
	                        , Degistiren=@Degistiren, DegisTarih=@DegisTarih, DegisSirKodu='{0}'
	                        WHERE ID=@ID AND Durum=11 AND SipTalepNo IS NOT NULL", vUser.SirketKodu);

                        SqlParameter[] paramlist = new SqlParameter[4]
                        {
                            new SqlParameter("ID", SqlDbType.Int){ Value = item.ID},
                            new SqlParameter("Degistiren", SqlDbType.VarChar){ Value = vUser.UserName.ToString()},
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

                var sipTarih = Convert.ToInt32(MyGlobalVariables.SipEvrak.Tarih.ToOADate());
                var sipEvrakNo = MyGlobalVariables.SipEvrak.EvrakNo;
                var hesapKodu = MyGlobalVariables.SipEvrak.HesapKodu;
                var teklifNo = Convert.ToInt32(MyGlobalVariables.SipEvrak.Satirlar[0].Kod4);
                try
                {
                    if (string.IsNullOrEmpty(sipEvrakNo) || string.IsNullOrEmpty(hesapKodu))
                        throw new ArgumentException("parametreler hatalı!");

                    var mailayar = db.Database.SqlQuery<GenelAyarVeParams>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 0")).FirstOrDefault();
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

                    var satinalmacimail = db.Database.SqlQuery<string>(string.Format("SELECT Email FROM usr.Users (nolock) WHERE Kod IN (SELECT TOP(1) Satinalmaci FROM Kaynak.sta.Talep(nolock) WHERE SipEvrakNo ={0} )", sipEvrakNo)).FirstOrDefault();

                    if ((string.IsNullOrEmpty(sirketemail) || sirketemail.Trim() == "")
                        && (string.IsNullOrEmpty(satinalmacimail) || satinalmacimail.Trim() == ""))
                    {
                        _Result.Message = "Ne Şirket Maili ne de Satınalmacı maili yapılandırılmamış. Mail gönderilemedi";
                        _Result.Status = false;
                    }

                    var sipTalep = db.Database.SqlQuery<SatTalep>(string.Format("SELECT TOP (1) TalepNo, SipIslemTip FROM Kaynak.sta.Talep (nolock) WHERE SipEvrakNo={0}", sipEvrakNo)).FirstOrDefault();
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

                    SatınalmaSiparisFormu.SatinalmaSiparisFormu(sipEvrakNo, hesapKodu, sipTarih, true, vUser.SirketKodu);

                    List<string> attachList = new List<string>
                        {
                        string.Format("{0}{1}.pdf", ConfigurationManager.AppSettings["TeklifDosyaAdres"].ToString(), sipEvrakNo)
                        };

                    List<SatTalep> listTalep = db.Database.SqlQuery<SatTalep>(string.Format("SELECT TalepNo, MalKodu, EkDosya FROM Kaynak.sta.Talep (nolock) WHERE SipEvrakNo ='{0}' AND HesapKodu = '{1}' AND ISNULL(EkDosya,'')<> '' ", sipEvrakNo, hesapKodu)).ToList();

                    var dosyaYolu = db.Database.SqlQuery<string>(string.Format("SELECT top 1 DosyaYolu FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 7 and DosyaYolu IS NOT NULL")).FirstOrDefault();

                    foreach (SatTalep talep in listTalep)
                    {
                        var yol = string.Format("{0}{1}\\{2}", dosyaYolu, "SatTalep", talep.EkDosya);
                        if (yol.FileExists()) attachList.Add(yol);
                    }

                    var kime = string.Format("{0};{1};{2}", sirketemail, satinalmacimail, mailayar.MailTo);
                    var gorunenIsim = "Sipariş Onay";
                    var konu = "Sipariş Onay";
                    var icerik = "Sipariş Bilgileri Ektedir.";
                    if (sipTalep.SipIslemTip == (short)KKPIslemTipSPI.DışPiyasa)
                    {
                        gorunenIsim = "Purchase Order Approval";
                        konu = "Purchase Order Approval";
                        icerik = "Purchase Order Items Information in Attachments";
                    }

                    using (var m = new MyMail(false)
                    {
                        MailHataMesajı = "Sipariş Onay Maili Gönderiminde hata oluştu! Mail Gönderilemedi!",
                        MailBasariMesajı = "Sipariş Onay Maili başarılı bir şekilde gönderildi!"
                    })
                    {
                        m.Gonder(kime, mailayar.MailCc, gorunenIsim, konu, icerik, attachList, vUser.UserName, fn.GetIPAddress());
                        if (m.MailGonderimBasarili)
                        {
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE Kaynak.sta.Talep SET MailGonder=-1 WHERE TalepNo='{0}'", sipTalep.TalepNo));
                        }
                        else
                        {
                            db.Database.ExecuteSqlCommand(string.Format("UPDATE Kaynak.sta.Talep SET MailGonder=0 WHERE TalepNo='{0}'", sipTalep.TalepNo));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger(ex, "Approvals/Purchase/SipGMOnayla/2");
                    _Result.Message = string.Format("Sipariş Onay Maili Gönderiminde hata oluştu! Mail Gönderilemedi!)");
                    _Result.Status = false;
                    return Json(_Result, JsonRequestBehavior.AllowGet);
                }

                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// gm için sipariş reddetme
        /// </summary>
        public JsonResult SipGMReddet(string redAciklama)
        {
            if (CheckPerm(Perms.SatinalmaOnaylama, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
            var _Result = new Result(true, "İşlem Başarılı");

            if (MyGlobalVariables.GridSource == null || MyGlobalVariables.GridSource.Count == 0 || MyGlobalVariables.SipEvrak == null)
            {
                _Result.Message = "Siparis Seçmelisiniz!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            var con = Connection.GetConnectionWithTrans();
            try
            {
                foreach (var item in MyGlobalVariables.TalepSource)
                {
                    var sql = @"UPDATE Kaynak.sta.Talep
SET GMOnaylayan='{0}', GMOnayTarih='{1}', Durum=13
, Degistiren='{0}', DegisTarih='{1}', DegisSirKodu={3}, Aciklama2='{2}'
WHERE ID={4} AND Durum=11 AND SipTalepNo IS NOT NULL";

                    db.Database.ExecuteSqlCommand(string.Format(sql, vUser.UserName.ToString(), DateTime.Now.ToString("yyyy-dd-MM"), redAciklama, vUser.SirketKodu, item.ID));
                }

                con.Trans.Commit();
            }
            catch (Exception)
            {
                if (con.Trans != null) con.Trans.Rollback();
                _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!";
                _Result.Status = false;
            }

            con.Dispose();
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        public int BirimDegisimKontrol(string talepNo, string hesapKodu)
        {
            List<int> brmContList = null;
            var query = string.Format(
   @"SELECT (CASE WHEN ST.Birim = STK.Birim1 THEN 1
		WHEN ST.Birim = STK.Birim2 THEN 1
		WHEN ST.Birim = STK.Birim3 THEN 1
		WHEN ST.Birim = STK.Birim4 THEN 1
		ELSE 0 END ) AS Miktar

FROM Kaynak.sta.Talep as ST (nolock)
LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on ST.MalKodu=STK.MalKodu
WHERE ST.Durum=11 AND ST.SipTalepNo={1} AND ST.HesapKodu='{2}'
GROUP BY (CASE WHEN ST.Birim = STK.Birim1 THEN 1
		WHEN ST.Birim = STK.Birim2 THEN 1
		WHEN ST.Birim = STK.Birim3 THEN 1
		WHEN ST.Birim = STK.Birim4 THEN 1
		ELSE 0 END )"
      , vUser.SirketKodu
      , talepNo, hesapKodu);
            brmContList = db.Database.SqlQuery<int>(query).ToList();

            if (brmContList.Count > 1 || brmContList[0] == 0)
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

            ViewBag.OnayTip = 0;
            ViewBag.baslik = "Teklif GMY Tedarikçi Onay";

            MyGlobalVariables.GMYOnayList = db.Database.SqlQuery<SatTalep>(string.Format(@"
            SELECT DISTINCT ST.KynkTalepNo AS TalepNo,
            (select TOP(1) Tp.Kaydeden FROM KAYNAK.sta.Talep as Tp (nolock) where Tp.TalepNo = St.KynkTalepNo) AS TalepEden
            FROM KAYNAk.sta.Teklif as ST (nolock)
            LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=ST.MalKodu
            WHERE ST.Durum=2 AND  ST.KynkTalepNo<>''
            GROUP BY ST.KynkTalepNo
            ORDER BY ST.KynkTalepNo", vUser.SirketKodu)).ToList();

            return View("GMY_Onay", MyGlobalVariables.GMYOnayList);
        }

        public ActionResult GMYMali_Onay()
        {
            if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Reading) == false) return Redirect("/");

            ViewBag.OnayTip = 1;
            ViewBag.baslik = "Teklif GMY Mali Onay";

            MyGlobalVariables.GMYOnayList = db.Database.SqlQuery<SatTalep>(string.Format(@"
            SELECT DISTINCT ST.KynkTalepNo AS TalepNo,
            (select TOP(1)Tp.Kaydeden FROM KAYNAK.sta.Talep(nolock) Tp where Tp.TalepNo = ST.KynkTalepNo) AS TalepEden
            FROM KAYNAK.sta.Teklif as ST (nolock)
            LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=ST.MalKodu
            WHERE ST.Durum=4
            GROUP BY ST.KynkTalepNo
            ORDER BY ST.KynkTalepNo", vUser.SirketKodu)).ToList();

            return View("GMY_Onay", MyGlobalVariables.GMYOnayList);
        }


        public PartialViewResult GMYOnayList(string TalepNo, int OnayTip)
        {
            if (OnayTip == 0)
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Reading) == false) return null;
                else if (OnayTip == 1)
                    if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Reading) == false) return null;

            ViewBag.TalepNo = TalepNo;
            ViewBag.OnayTip = OnayTip;
            return PartialView("GMYOnay_List");
        }

        public string GMYOnayListData(string TalepNo, int OnayTip)
        {
            if (OnayTip == 0)
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Reading) == false) return null;
                else if (OnayTip == 1)
                    if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Reading) == false) return null;

            if (MyGlobalVariables.GridGMYSource == null)
                MyGlobalVariables.GridGMYSource = new List<SatTalep>();
            else
                MyGlobalVariables.GridGMYSource.Clear();

            if (OnayTip == 0)
            {
                #region Teklif GMY Tedarikçi Onay

                MyGlobalVariables.GridGMYSource = db.Database.SqlQuery<SatTalep>(string.Format(@"
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
#endregion
            }
            else if (OnayTip == 1)
            {
                #region Teklif GMY Mali Onay

                MyGlobalVariables.GridGMYSource = db.Database.SqlQuery<SatTalep>(string.Format(@"
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
                (select TOP(1)Tp.Kaydeden FROM sta.Talep as Tp (nolock) where Tp.TalepNo = St.KynkTalepNo) AS TLPKaydeden,
                ST.TeklifMiktar AS BirimMiktar

            FROM KAYNAK.sta.Teklif as ST (nolock)
            LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) on STK.MalKodu=ST.MalKodu
            LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK (nolock) on CHK.HesapKodu=ST.HesapKodu
            LEFT JOIN
            (
	           SELECT MalKodu, SUM(BirimMiktar) as AcikTalepMiktar FROM sta.Talep (nolock)
	           WHERE Durum NOT IN (3, 4, 9, 13) AND Durum < 15
	           GROUP BY MalKodu
             ) as AT on AT.MalKodu=ST.MalKodu
             LEFT JOIN SOLAR6.dbo.DVZ (nolock) on DVZ.DovizCinsi=ST.DvzCinsi AND DVZ.Tarih=CAST( DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))+2 AS INT)
            WHERE ST.KynkTalepNo='{1}' AND ST.Durum=4", vUser.SirketKodu, TalepNo)).ToList();

                #endregion
            }

            var json = new JavaScriptSerializer().Serialize(MyGlobalVariables.GridGMYSource);
            return json;
        }

        /// <summary>
        /// gmy onaylama
        /// </summary>
        public JsonResult GMYOnayla(int OnayTip)
        {
            if (OnayTip == 0)
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
                else if (OnayTip == 1)
                    if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);

            var _Result = new Result(true, "İşlem Başarılı");
            if (MyGlobalVariables.GridGMYSource.Where(x => x.OneriDurum).Count() == 0)
            {
                _Result.Message = "Satınalmacı tarafından öneri yapılmamış. İşlem devam edemez!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            var con = Connection.GetConnectionWithTrans();

            try
            {
                foreach (var item in MyGlobalVariables.GridGMYSource)
                {
                    string sql = "";

                    if (OnayTip == 0)
                    {
                        #region Teklif GMY Tedarikçi Onay

                        sql = string.Format(@"UPDATE Kaynak.sta.Teklif SET
	                        Durum=4, Aciklama2='{0}', Degistiren='{1}', DegisTarih=GETDATE(), DegisSirKodu='{3}',
                            Kademe2Onaylayan='{1}', Kademe2OnayTarih=GETDATE()
	                        WHERE ID={2} AND Durum=2", item.Aciklama2, vUser.UserName.ToString(), item.ID, vUser.SirketKodu);

                        #endregion
                    }
                    else if (OnayTip == 1)
                    {
                        #region Teklif GMY Mali Onay
                        #endregion
                    }
                        db.Database.ExecuteSqlCommand(sql);
                }
                con.Trans.Commit();
            }
            catch (Exception)
            {
                if (con.Trans != null) con.Trans.Rollback();
                _Result.Message = "İşlem Sırasında Hata Oluştu.";
                _Result.Status = false;
            }

            con.Dispose();
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// gmy reddetme
        /// </summary>
        public JsonResult GMYReddet(string redAciklama, int OnayTip)
        {
            if (OnayTip == 0)
                if (CheckPerm(Perms.SatinalmaGMYTedarikciOnay, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);
                else if (OnayTip == 1)
                    if (CheckPerm(Perms.SatinalmaGMYMaliOnay, PermTypes.Writing) == false) return Json(new Result(false, "Yetkiniz yok"), JsonRequestBehavior.AllowGet);

            var _Result = new Result(true, "İşlem Başarılı");

            if (MyGlobalVariables.GridGMYSource.Where(x => x.OneriDurum).Count() == 0)
            {
                _Result.Message = "Satınalmacı tarafından öneri yapılmamış. İşlem devam edemez!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            var con = Connection.GetConnectionWithTrans();
            try
            {
                foreach (var item in MyGlobalVariables.TalepSource)
                {
                    string sql = "";

                    if (OnayTip == 0)
                    {
                        #region Teklif GMY Tedarikçi Onay

                        sql = string.Format(@"UPDATE Kaynak.sta.Teklif SET
                                            Durum=3, Aciklama2='{0}', Degistiren='{1}', DegisTarih=GETDATE(), DegisSirKodu='{3}',
                                            Kademe2Onaylayan='{1}', Kademe2OnayTarih=GETDATE()
                                            WHERE ID={2} AND Durum=2", redAciklama, vUser.UserName.ToString(), item.ID, vUser.SirketKodu);

                        #endregion
                    }
                    else if (OnayTip == 1)
                    {
                        #region Teklif GMY Mali Onay
                        #endregion
                    }

                    db.Database.ExecuteSqlCommand(sql);
                }

                con.Trans.Commit();
            }
            catch (Exception)
            {
                if (con.Trans != null) con.Trans.Rollback();
                _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!";
                _Result.Status = false;
            }

            con.Dispose();
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}