using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using KurumsalKaynakPlanlaması12M;
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
    public static class MyGlobalVariables
    {
        public static KKPEvrakSiparis SipEvrak { get; set; }
        public static List<MMK> TesisList { get; set; }
        public static List<SatTalep> SipTalepList { get; set; }
        public static List<SatTalep> TalepSource { get; set; }
        public static List<KKP_SPI> GridSource { get; set; }
        public static List<KKP_FTD> GridFTD { get; set; }
        public static bool DovizDurum { get; set; }
        public static string Birim { get; set; }

    }

    public class PurchaseController : RootController
    {
        public ActionResult GMOnayHTML()
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return Redirect("/");
            return View();
        }
        public ActionResult Satınalma_GM_Onay()
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return Redirect("/");
            MyGlobalVariables.DovizDurum = false;
            MyGlobalVariables.SipTalepList = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[wms].[SatinAlmaGMOnayList]", "17")).ToList();
            return View("GM_Onay", MyGlobalVariables.SipTalepList);
        }

        public PartialViewResult SipGMOnayList(string HesapKodu, int SipTalepNo)
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return null;

            ViewBag.HesapKodu = HesapKodu;
            ViewBag.SipTalepNo = SipTalepNo;
            return PartialView("SatinalmaSipGMOnay_List");
        }
        public PartialViewResult SipGMOnayListFTD(string HesapKodu, int SipTalepNo)
        {
            if (CheckPerm(Perms.FiyatOnaylama, PermTypes.Reading) == false) return null;

            ViewBag.HesapKodu = HesapKodu;
            ViewBag.SipTalepNo = SipTalepNo;
            return PartialView("SatinalmaSipGMOnayFTD_List");
        }
        public string SipGMOnayListData(string HesapKodu, int SipTalepNo)
        {
            if (MyGlobalVariables.GridSource == null)
                MyGlobalVariables.GridSource = new List<KKP_SPI>();
            else
                MyGlobalVariables.GridSource.Clear();

            MyGlobalVariables.SipEvrak = new KKPEvrakSiparis(KKPSiparisTip.AlimSiparisi);

            MyGlobalVariables.TalepSource = db.Database.SqlQuery<SatTalep>(string.Format("[FINSAT6{0}].[wms].[SatinAlmaGMOnayListData] @HesapKodu='{1}', @SipTalepNo={2} ", "17", HesapKodu, SipTalepNo)).ToList();
            if (MyGlobalVariables.TalepSource[0].SipIslemTip == null || (MyGlobalVariables.TalepSource[0].SipIslemTip != 1 && MyGlobalVariables.TalepSource[0].SipIslemTip != 2))
                return "";

            //SipEvrak.dvzTL = (KKPDvzTL)(short)TalepSource[0].DvzTL;
            //SipEvrak.DovizCinsi = TalepSource[0].DvzCinsi;
            //SipEvrak.DovizKuru = TalepSource[0].DvzKuru ?? 0;
            MyGlobalVariables.SipEvrak.IslemTip = (KKPIslemTipSPI)MyGlobalVariables.TalepSource[0].SipIslemTip;
            MyGlobalVariables.SipEvrak.dvzTL = (KKPDvzTL)(short)MyGlobalVariables.TalepSource[0].FTDDovizTL;
            short siraNo = 0;
            foreach (var item in MyGlobalVariables.TalepSource)
            {
                if ((short)item.DvzTL == (short)1)
                {
                    MyGlobalVariables.DovizDurum = true;
                }
                KKP_SPI spi = new KKP_SPI(KKPSiparisTip.AlimSiparisi)
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

                //spi.Depo = Degiskenler.SiparisDepo;

                spi.Operator = (short)item.Operator.Value;
                spi.Katsayi = item.Katsayi.Value;

                MyGlobalVariables.SipEvrak.Ekle(spi);
                MyGlobalVariables.GridSource.Add(spi);
                siraNo++;
            }

            if (MyGlobalVariables.DovizDurum == false)
            {
                MyGlobalVariables.SipEvrak.FTDHesapla("TL", Convert.ToDecimal(0));
                MyGlobalVariables.GridFTD = MyGlobalVariables.SipEvrak.FTDList;
            }
            else
            {
                decimal kur = MyGlobalVariables.TalepSource[0].FTDDovizKuru.Value;
                if (kur > 0)
                {
                    MyGlobalVariables.SipEvrak.FTDHesapla(MyGlobalVariables.TalepSource[0].FTDDovizCinsi, kur);
                    MyGlobalVariables.GridFTD = MyGlobalVariables.SipEvrak.FTDList;
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

        public JsonResult SipGMOnayla()
        {
            Result _Result = new Result(true, "İşlem Başarılı");
            if (MyGlobalVariables.TalepSource == null)
            {
                _Result.Message = "Sipariş Seçin";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }


            using (KKP kkp = new KKP(ConfigurationManager.ConnectionStrings["WMSConnection"].ConnectionString, "17"))
            {
                try
                {
                    string evrakno = kkp.YeniEvrakNo(KKPKynkEvrakTip.AlımSiparişi, 1);
                    foreach (var item in MyGlobalVariables.TalepSource)
                    {
                        string sql = string.Format(@"UPDATE Kaynak.sta.Talep 
    SET GMOnaylayan=@Degistiren, GMOnayTarih=@DegisTarih, Durum=15, SipEvrakNo=@SipEvrakNo
    , SirketKodu='{0}'
    , Degistiren=@Degistiren, DegisTarih=@DegisTarih, DegisSirKodu='{0}'
    WHERE ID=@ID AND Durum=11 AND SipTalepNo IS NOT NULL", "17");


                        SqlParameter[] paramlist = new SqlParameter[4]
                        {
                            new SqlParameter("ID", SqlDbType.Int){ Value = item.ID},
                            new SqlParameter("Degistiren", SqlDbType.VarChar){ Value = vUser.UserName.ToString()},
                            new SqlParameter("DegisTarih", SqlDbType.SmallDateTime){ Value = DateTime.Now},
                            new SqlParameter("SipEvrakNo", SqlDbType.VarChar){Value = evrakno}
                        };

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
                        decimal kur = MyGlobalVariables.TalepSource[0].FTDDovizKuru.Value;
                        if (kur > 0)
                        {
                            MyGlobalVariables.SipEvrak.FTDHesapla(MyGlobalVariables.TalepSource[0].FTDDovizCinsi, kur);
                        }
                    }

                    int sipTarih = Convert.ToInt32(MyGlobalVariables.SipEvrak.Tarih.ToOADate());
                    var sipEvrakNo = MyGlobalVariables.SipEvrak.EvrakNo;
                    var hesapKodu = MyGlobalVariables.SipEvrak.HesapKodu;
                    var teklifNo = Convert.ToInt32(MyGlobalVariables.SipEvrak.Satirlar[0].Kod4);

                    try
                    {
                        #region Kayıt ve Mail
                        if (string.IsNullOrEmpty(sipEvrakNo) || string.IsNullOrEmpty(hesapKodu))
                            throw new ArgumentException("parametreler hatalı!!");

                        GenelAyarVeParams mailayar = db.Database.SqlQuery<GenelAyarVeParams>(string.Format("SELECT * FROM [Kaynak].[sta].[GenelAyarVeParams]  where Tip = 4 and Tip2 = 0")).FirstOrDefault();
                        if (mailayar == null)
                        {
                            _Result.Message = "Sipariş Onay Mail ayarları yapılandırılmamış!!";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }


                        string sorgu = string.Format("SELECT Kaynak.sta.TedarikciMail('{0}')", hesapKodu);

                        string sirketemail = db.Database.SqlQuery<string>(sorgu).FirstOrDefault();
                        if (string.IsNullOrEmpty(sirketemail) || sirketemail.Trim() == "")
                        {
                            //if (Degiskenler.FromWinServis == false)
                            _Result.Message = string.Format("Tedarikçiye ait mail bulunamadı!! (Hesap Kodu: {0})", hesapKodu);
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        string satinalmacimail = db.Database.SqlQuery<string>(string.Format("SELECT Email FROM usr.Users (nolock) WHERE Kod IN (SELECT TOP(1) Satinalmaci FROM Kaynak.sta.Talep(nolock) WHERE SipEvrakNo ={0} )", sipEvrakNo)).FirstOrDefault();

                        if ((string.IsNullOrEmpty(sirketemail) || sirketemail.Trim() == "")
                            && (string.IsNullOrEmpty(satinalmacimail) || satinalmacimail.Trim() == ""))
                        {
                            _Result.Message = "Ne Şirket Maili nede Satınalmacı maili yapılandırılmamış. Mail gönderilemedi";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }

                        SatTalep sipTalep = db.Database.SqlQuery<SatTalep>(string.Format("SELECT TOP (1) TalepNo, SipIslemTip FROM Kaynak.sta.Talep (nolock) WHERE SipEvrakNo={0}", sipEvrakNo)).FirstOrDefault();
                        if (sipTalep == null)
                        {
                            _Result.Message = "Siparişin Talep ile ilişkisi bulunamadı!! (Sipariş Onay Mail Gönderim)";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }
                        if (sipTalep.SipIslemTip == null)
                        {
                            _Result.Message = "Sipariş iç/Dış Piyasa olduğu belirlenemedi!!";
                            _Result.Status = false;
                            return Json(_Result, JsonRequestBehavior.AllowGet);
                        }


                        List<SatTalep> listTalep = db.Database.SqlQuery<SatTalep>(string.Format("SELECT TalepNo, MalKodu, EkDosya FROM Kaynak.sta.Talep (nolock) WHERE SipEvrakNo ='{0}' AND HesapKodu = '{1}' AND ISNULL(EkDosya,'')<> '' ", sipEvrakNo, hesapKodu)).ToList();



                        string kime = String.Format("{0};{1};{2}", sirketemail, satinalmacimail, mailayar.MailTo);
                        string gorunenIsim = "Sipariş Onay";
                        string konu = "Sipariş Onay";
                        string icerik = "Sipariş Bilgileri Ektedir.";
                        if (sipTalep.SipIslemTip == (short)KKPIslemTipSPI.DışPiyasa)
                        {
                            gorunenIsim = "Purchase Order Approval";
                            konu = "Purchase Order Approval";
                            icerik = "Purchase Order Items Information in Attachments";
                        }

                        #endregion

                        #region Rapor oluşturma

                        string deger = "aaaaaaaa";
                        var x = @"<style>
    .a {
    border: 1px solid black;
    }

    th {
        border: 2px dotted black;
        text-align: center;
    }

    td {
        border: 2px dotted black;
        text-align: center;
    }
    p{
        line-height:1px;
        font-weight:bold;
        font-size:12px;
    }
    </style>
    <div class='m-10' style='height: 29.7cm;width: 21cm;'>
        <div class='header-logo' style='width:100%;'>
            <img src='/Content/images/GMOnayHeader.png' alt='Mountain View' style='width:100%;height:100%;'>
        </div>
        <div style='width:100%;height:20px;text-align:center;margin-top:10px;'>
            <span style='font-weight:bold;'>SATINALMA SİPARİŞ FORMU</span>
        </div>
        <div class='row col-md-12' style='font-size:12px;'>
            <div class='col-md-9'>
                <div class='row bold m-l-5'>Tedarikçi Bilgileri</div>
                <div class='row'>
                    <div class='a col-md-3'>Ünvanı</div>
                    <div class='a col-md-9'>" + deger + @"</div>
                </div>
                <div class='row'>
                    <div class='a col-md-3'>Adresi</div>
                    <div class='a col-md-9'>" + deger + @"</div>
                </div>
                <div class='row'>
                    <div class='a col-md-3'>Tel</div>
                    <div class='a col-md-9'>" + deger + @"</div>
                </div>
                <div class='row'>
                    <div class='a col-md-3'>Fax</div>
                    <div class='a col-md-9'>" + deger + @"</div>
                </div>
                <div class='row'>
                    <div class='a col-md-3'>Email</div>
                    <div class='a col-md-9'>" + deger + @"</div>
                </div>
            </div>
            <div class='col-md-3'>
                <div>
                    <div style='width:50%;float:left;height:20px;'><span style='font-weight:bold;width:120px;'>Tarih</span></div>
                    <div style='width:50%;float:left;height:20px;text-align:right;'><span style='width:120px;margin-left:8px;'>" + deger + @"</span></div>
                </div>
                <div>
                    <div style='width:50%;float:left;height:20px;'><span style='font-weight:bold;width:120px;'>Sipariş No</span></div>
                    <div style='width:50%;float:left;height:20px;text-align:right;'><span style='width:120px;margin-left:8px;'>" + deger + @"</span></div>
                </div>
                <div>
                    <div style='width:50%;float:left;height:20px;'><span style='font-weight:bold;width:120px;'>Sipariş Tarihi</span></div>
                    <div style='width:50%;float:left;height:20px;text-align:right;'><span style='width:120px;margin-left:8px;'>" + deger + @"</span></div>
                </div>
            </div>
        </div>
        <div class='row col-md-12 m-t-10' >
            <table>
                <thead>
                    <tr>
                        <th>No</th>
                        <th>Mal Kodu</th>
                        <th>Nal Adı</th>
                        <th>Açıklamalar</th>
                        <th>İstenen Tarih</th>
                        <th>Onaylanan Tarih</th>
                        <th>Miktar</th>
                        <th>Birim</th>
                        <th>Fiyat</th>
                        <th>Tutar</th>
                        <th>Döviz Cinsi</th>
                        <th>Vade</th>
                        <th>Teslim Yeri</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                        <td>" + deger + @"</td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class='row col-md-12 m-t-10'>
            <div class='col-md-4' style='float:right;'>
                <div class='row'>
                    <div class='a col-md-6' style='border:none;'>Ara Toplam</div>
                    <div class='a col-md-4' style='border:none;'>" + deger + @"</div>
                    <div class='a col-md-2' style='border:none;'>" + deger + @"</div>
                </div>
                <div class='row'>
                    <div class='a col-md-6' style='border:none;'>KDV</div>
                    <div class='a col-md-4' style='border:none;'>" + deger + @"</div>
                    <div class='a col-md-2' style='border:none;'>" + deger + @"</div>
                </div>
                <div class='row'>
                    <div class='a col-md-6' style='border:none;'>Toplam Tutar</div>
                    <div class='a col-md-4' style='border:none;'>" + deger + @"</div>
                    <div class='a col-md-2' style='border:none;'>" + deger + @"</div>
                </div>
            </div>
        </div>

        <div class='row col-md-12 m-t-10'>
            <p>LÜTFEN SİPARİŞLERİMİZİ KESİN TESLİM TARİHİ İLE BİRLİKTE TEYİT EDİNİZ.</p>
            <p>İRSALİYE VE FATURA ÜZERİNE SİPARİŞ NUMARASININ MUTLAKA YAZILMASINI RİCA EDERİZ.</p>
            <p>FATURANIZDA BANKA BİLGİLERİNİZİN IBAN NO KULLANILARAK BELİRTİLMESİ HUSUSUNU RİCA EDERİZ.</p>
            <p>İRSALİYESİZ MAL KABULÜ KESİNLİKLE YAPILMAYACAKTIR.</p>
            <p>FİRMAMIZ ADINA KESİLEN FATURALARIN İVEDİLİKLE TARAFIMIZA ULAŞTIRILMASI HUSUSUNU RİCA EDERİZ.</p>
            <p>ÜCRET ALICI GÖNDERİLERİNİZİN AKSİ BELİRTİLMEDİĞİ SÜRECE ARAS KARGO İLE GÖNDERİLMESİNİ RİCA EDERİZ.</p>
        </div>

        <div class='row col-md-12 m-t-10'>
            <div class='row'>
                <div class='a col-md-4' style='text-align:center;border:none;font-weight:bold;'>Satınalma Uzmanı</div>
                <div class='a col-md-4' style='text-align:center;border:none;font-weight:bold;'>Genel Müdür Yardımcısı</div>
                <div class='a col-md-4' style='text-align:center;border:none;font-weight:bold;'>Genel Müdür</div>
            </div>
            <div class='row'>
                <div class='a col-md-4' style='text-align:center;border:none;font-weight:bold;'>Çetin Ün</div>
                <div class='a col-md-4' style='text-align:center;border:none;'></div>
                <div class='a col-md-4' style='text-align:center;border:none;'></div>
            </div>

        </div>
    </div>";
                        var html = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                 <!DOCTYPE html 
                     PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN""
                    ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">
                 <html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en"">
                    <head>
                        <title>Minimal XHTML 1.0 Document with W3C DTD</title>
                    </head>
                  <body>
                    " + x + "</body></html>";

                        var bytes = System.Text.Encoding.UTF8.GetBytes(html);

                        using (var input = new MemoryStream(bytes))
                        {
                            var output = new MemoryStream(); // this MemoryStream is closed by FileStreamResult

                            var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.LETTER, 50, 50, 50, 50);
                            var writer = PdfWriter.GetInstance(document, output);
                            writer.CloseStream = false;
                            document.Open();

                            var xmlWorker = XMLWorkerHelper.GetInstance();

                            //xmlWorker.ParseXHtml(writer, document);
                            document.Close();
                            output.Position = 0;

                            new FileStreamResult(output, "application/pdf");
                        }
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        _Result.Message = string.Format("Sipariş Onay Maili Gönderiminde hata oluştu!! Mail Gönderilelemedi!!)");
                        _Result.Status = false;
                        return Json(_Result, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    _Result.Message = "İşlem Sırasında Hata Oluştu.";
                    _Result.Status = false;

                }
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SipGMReddet(string redAciklama)
        {
            Result _Result = new Result(true, "İşlem Başarılı");

            if (MyGlobalVariables.GridSource == null || MyGlobalVariables.GridSource.Count == 0 || MyGlobalVariables.SipEvrak == null)
            {
                _Result.Message = "Siparis Seçmelisiniz!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }


            if (redAciklama.Trim() == "")
            {
                _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!!";
                _Result.Status = false;
                return Json(_Result, JsonRequestBehavior.AllowGet);
            }

            Connection con = Connection.GetConnectionWithTrans();
            try
            {
                foreach (var item in MyGlobalVariables.TalepSource)
                {
                    string sql = @"UPDATE Kaynak.sta.Talep 
SET GMOnaylayan='{0}', GMOnayTarih={1}, Durum=13
, Degistiren='{0}', DegisTarih={1}, DegisSirKodu={3}, Aciklama2='{2}'
WHERE ID={4} AND Durum=11 AND SipTalepNo IS NOT NULL";

                    db.Database.ExecuteSqlCommand(string.Format(sql, vUser.UserName.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), redAciklama, "17", item.ID));

                }

                con.Trans.Commit();

            }
            catch (Exception ex)
            {
                if (con.Trans != null)
                    con.Trans.Rollback();
                _Result.Message = "Geri Çevirme açıklamasını girmek zorundasınız!!";
                _Result.Status = false;

            }
            con.Dispose();
            return Json(_Result, JsonRequestBehavior.AllowGet);
        }
    }
}