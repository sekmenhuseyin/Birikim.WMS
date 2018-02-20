using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
    }
}