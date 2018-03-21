using Birikim.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class MyMail
    {
        public bool IsBodyHtml { get; set; }
        public string MailBasariMesajı { get; set; }
        public bool MailGonderimBasarili { get; private set; }
        public string MailHataMesajı { get; set; }
        private WMSEntities Db { get; }

        public MyMail(WMSEntities db, bool isBodyHtml = false)
        {
            IsBodyHtml = isBodyHtml;
            Db = db;
        }

        /// <summary>
        /// Mail Adresi geçerli mi değilmi bunu regexle kontrol eder. Geçerli ise true döner.
        /// </summary>
        public static bool MailGecerlimi(string emailaddress)
        {
            return Regex.IsMatch(emailaddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public Result Gonder(string kime, string cc, string gorunenIsim, string konu, string mesaj, List<string> dosyaList, string UserName, string IP)
        {
            var tbl = Db.Settings.FirstOrDefault();
            if (tbl == null)
                return new Result(false, "Mail ayarları kaydedilmemiş");
            if (tbl.SmtpEmail == null || tbl.SmtpPass == null || tbl.SmtpHost == null || tbl.SmtpPort == null)
                return new Result(false, "Mail ayarları kaydedilmemiş");

            var message = new MailMessage();
            var fromAddress = new MailAddress(tbl.SmtpEmail, gorunenIsim);
            message.From = fromAddress;
            foreach (string mail in kime.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(mail)) continue;

                if (!MailGecerlimi(mail.Trim()))
                    return new Result(false, string.Format("Mail gönderimi başarısız! Mail Formatı hatalı (mail: {0})", mail));

                message.To.Add(mail);
            }

            foreach (string mail in cc.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(mail)) continue;

                if (!MailGecerlimi(mail.Trim()))
                    return new Result(false, string.Format("Mail gönderimi başarısız! Mail Formatı hatalı (mail: {0})", mail));

                message.CC.Add(mail);
            }

            if (dosyaList != null)
            {
                foreach (string filepath in dosyaList)
                {
                    if (string.IsNullOrEmpty(filepath) || filepath.Length < 2)
                        continue;
                    if (!File.Exists(filepath))
                        continue;
                    message.Attachments.Add(new Attachment(filepath));
                }
            }

            message.SubjectEncoding = Encoding.UTF8;
            message.Subject = konu;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = mesaj;
            message.IsBodyHtml = IsBodyHtml;
            using (var smtp = new SmtpClient()
            {
                Port = tbl.SmtpPort.Value,
                Host = tbl.SmtpHost,
                EnableSsl = tbl.SmtpSSL,
                Credentials = new System.Net.NetworkCredential(tbl.SmtpEmail, tbl.SmtpPass)
            })
                try
                {
                    smtp.Send(message);
                    MailGonderimBasarili = true;
                    var tmp = string.Format("Konu: {0}{1}", konu, dosyaList != null ? ", Ek Dosya: " + string.Join(", ", dosyaList) : "");
                    Db.LogActions("WMS", "Business", "MyMail", "Gonder", (int)ComboItems.alMailGönder, 0, kime, tmp, UserName, IP);
                    return new Result(true);
                }
                catch (Exception ex)
                {
                    //log
                    var inner = "";
                    if (ex.InnerException != null)
                    {
                        inner = ex.InnerException?.Message ?? "";
                        if (ex.InnerException.InnerException != null) inner += ": " + ex.InnerException.InnerException.Message;
                    }
                    Db.Logger(UserName, "", IP, ex.Message, inner, "Business/MyMail/Gonder");
                    //return
                    MailGonderimBasarili = false;
                    return new Result(false, ex.Message);
                }
        }

        public static string TeklifGeriCevirmeMailIcerik(List<SatTalep> list, string geriCevirmeAciklamasi)
        {
            var sw = new StringWriter();
            var wr = new HtmlTextWriter(sw);

            wr.AddAttribute(HtmlTextWriterAttribute.Border, "1");
            wr.AddAttribute(HtmlTextWriterAttribute.Bordercolor, "gray");
            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
            wr.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "4");
            wr.RenderBeginTag(HtmlTextWriterTag.Table);
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "1");

            #region Üst Bilgi

            wr.RenderBeginTag(HtmlTextWriterTag.Tr); //başlık tr aç

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Teklif No");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Hesap Kodu");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Ünvan");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Mal Kodu");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Mal Adı");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Birim");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Birim Fiyat");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Döviz Cinsi");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Geri Çevirme Açıklaması");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Önerilen");
            wr.RenderEndTag();

            wr.RenderEndTag(); //BAŞLIK TR KAPA

            #endregion

            #region Detay

            foreach (var item in list)
            {
                wr.RenderBeginTag(HtmlTextWriterTag.Tr); //satır tr aç

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.TeklifNo);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.HesapKodu);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.Unvan);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.MalKodu);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.MalAdi);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.Birim);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.BirimFiyat.ToDecimal().ToString("N4"));
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.DvzCinsi);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(geriCevirmeAciklamasi);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(item.OneriDurum ? "X" : "");
                wr.RenderEndTag();

                wr.RenderEndTag(); //satır TR KAPA
            }

            #endregion

            wr.RenderEndTag();  //table tag kapa

            return sw.ToString();
        }

        public static string TalepGeriCevirmeIcerik(List<SatTalep> list)
        {
            var sw = new StringWriter();
            var wr = new HtmlTextWriter(sw);

            wr.AddAttribute(HtmlTextWriterAttribute.Border, "1");
            wr.AddAttribute(HtmlTextWriterAttribute.Bordercolor, "gray");
            wr.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "1");
            wr.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "4");
            wr.RenderBeginTag(HtmlTextWriterTag.Table);
            wr.AddAttribute(HtmlTextWriterAttribute.Border, "1");

            wr.RenderBeginTag(HtmlTextWriterTag.Tr); //başlık tr aç

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Talep No");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Mal Kodu");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Mal Adı");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("Birim");
            wr.RenderEndTag();

            wr.RenderBeginTag(HtmlTextWriterTag.Td);
            wr.Write("İstenen Miktar");
            wr.RenderEndTag();

            wr.RenderEndTag();//başlık tr kapa
            foreach (SatTalep talep in list)
            {
                wr.RenderBeginTag(HtmlTextWriterTag.Tr); //tr tag aç

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(talep.TalepNo);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(talep.MalKodu);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(talep.MalAdi);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(talep.Birim);
                wr.RenderEndTag();

                wr.RenderBeginTag(HtmlTextWriterTag.Td);
                wr.Write(talep.BirimMiktar.ToString("N2"));
                wr.RenderEndTag();


                wr.RenderEndTag(); //tr tag kapa
            }

            wr.RenderEndTag();  //table tag kapa

            return sw.ToString();
        }
    }
}