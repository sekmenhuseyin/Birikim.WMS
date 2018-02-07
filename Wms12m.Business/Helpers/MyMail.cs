using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class MyMail : IDisposable
    {
        private MailMessage message;
        private SmtpClient smtp;

        public MyMail(bool isBodyHtml = false)
        {
            IsBodyHtml = isBodyHtml;
        }

        public bool IsBodyHtml { get; set; }
        public string MailBasariMesajı { get; set; }
        public bool MailGonderimBasarili { get; private set; }
        public string MailHataMesajı { get; set; }

        /// <summary>
        /// Mail Adresi geçerli mi değilmi bunu regexle kontrol eder. Geçerli ise true döner.
        /// </summary>
        public static bool MailGecerlimi(string emailaddress)
        {
            return Regex.IsMatch(emailaddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public void Dispose()
        {
            if (smtp != null)
                smtp.Dispose();
            if (message != null)
                message.Dispose();
        }

        public Result Gonder(string kime, string cc, string gorunenIsim, string konu, string mesaj, List<string> dosyaList)
        {
            string smtpEmail, smtpPass, smtpHost; int smtpPort; bool smtpSSL;
            using (WMSEntities db = new WMSEntities())
            {
                var tbl = db.Settings.FirstOrDefault();
                if (tbl.SmtpEmail == null || tbl.SmtpPass == null || tbl.SmtpHost == null || tbl.SmtpPort == null)
                    return new Result(false, "Mail ayarları kaydedilmemiş");
                smtpEmail = tbl.SmtpEmail;
                smtpPass = tbl.SmtpPass;
                smtpHost = tbl.SmtpHost;
                smtpPort = tbl.SmtpPort.Value;
                smtpSSL = tbl.SmtpSSL;
            }

            message = new MailMessage();
            var fromAddress = new MailAddress(smtpEmail, gorunenIsim);
            message.From = fromAddress;
            foreach (string mail in kime.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(mail)) continue;

                if (!MailGecerlimi(mail.Trim()))
                    return new Result(false, string.Format("Mail gönderimi başarısız!! Mail Formatı hatalı (mail: {0})", mail));

                message.To.Add(mail);
            }

            foreach (string mail in cc.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(mail)) continue;

                if (!MailGecerlimi(mail.Trim()))
                    return new Result(false, string.Format("Mail gönderimi başarısız!! Mail Formatı hatalı (mail: {0})", mail));

                message.CC.Add(mail);
            }

            if (dosyaList != null)
            {
                foreach (string filepath in dosyaList)
                {
                    if (string.IsNullOrEmpty(filepath) || filepath.Length < 2)
                        continue;
                    message.Attachments.Add(new Attachment(filepath));
                }
            }

            message.SubjectEncoding = Encoding.UTF8;
            message.Subject = konu;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = mesaj;
            message.IsBodyHtml = IsBodyHtml;
            smtp = new SmtpClient()
            {
                Port = smtpPort,
                Host = smtpHost,
                EnableSsl = smtpSSL,
                Credentials = new System.Net.NetworkCredential(smtpEmail, smtpPass)
            };
            smtp.SendCompleted += smtp_SendCompleted;
            try
            {
                smtp.Send(message);
                MailGonderimBasarili = true;
                return new Result(true);
            }
            catch (Exception ex)
            {
                MailGonderimBasarili = false;
                return new Result(false, ex.Message);
            }
        }

        private void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            smtp.Dispose();
            message.Dispose();
            smtp = null;
            message = null;
        }
    }
}