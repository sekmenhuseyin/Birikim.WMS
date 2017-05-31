using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wms12m
{
    public enum MailType
    {
        SiparisOnay = 0,
        SiparisTermin = 1,
        TeklifGonder = 2,
        TalepGeriCevirMe = 3,
        TeklifGeriCevirme = 4,
        IrsaliyeGiris = 5,
        GMOnayBilgilendirme = 6
    }

    public class MyMail
    {

        public Exception ExcHata { get; set; }
        public string MailHataMesajı { get; set; }
        public string MailBasariMesajı { get; set; }

        public bool IsBodyHtml { get; set; }
        public MyMail(bool isBodyHtml = false)
        {
            IsBodyHtml = isBodyHtml;
        }

        /// <summary>
        /// Mail Adresi geçerli mi değilmi bunu regexle kontrol eder. Geçerli ise true döner.
        /// </summary>
        public static bool MailGecerlimi(string emailaddress)
        {
            return Regex.IsMatch(emailaddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public bool MailGonderimBasarili { get; private set; }


        SmtpClient smtp;
        MailMessage message;
        public void Gonder(string kime, string cc, string gorunenIsim, string konu, string mesaj, List<string> dosyaList,
             string mailOturumAdi, string mailSifre, string serverAdi, bool enableSsl, int smtpPort)
        {

            //if (Degiskenler.TestRun)
            //    return;

            message = new MailMessage();
            MailAddress fromAddress = new MailAddress(mailOturumAdi, gorunenIsim);

            message.From = fromAddress;

            foreach (string mail in kime.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(mail))
                    continue;

                if (!MailGecerlimi(mail.Trim()))
                    
                    //throw new MyException(string.Format("Mail gönderimi başarısız!! Mail Formatı hatalı (mail: {0})", mail)) { Deger = mail };

                message.To.Add(mail);
            }

            foreach (string mail in cc.Split(';'))
            {
                if (string.IsNullOrWhiteSpace(mail))
                    continue;

                if (!MailGecerlimi(mail.Trim()))
                    //throw new MyException(string.Format("Mail gönderimi başarısız!! Mail Formatı hatalı (mail: {0})", mail)) { Deger = mail };

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

            smtp = new SmtpClient();
            smtp.Port = smtpPort;
            smtp.Host = serverAdi;
            smtp.EnableSsl = enableSsl;
            smtp.Credentials = new System.Net.NetworkCredential(mailOturumAdi, mailSifre);
            smtp.SendCompleted += smtp_SendCompleted;
            smtp.SendAsync(message, message);



            //message.Dispose();
            //smtp.Dispose();

            MailGonderimBasarili = true;

        }

        void smtp_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            smtp.Dispose();
            message.Dispose();
        }

        public void SendMail(string kime, string cc, string gorunenIsım, string konu, string mesaj, List<string> dosyaList
            , string mailOturumAdi, string mailSifre, string serverAdi, bool enableSsl, int smtpPort, bool msgBoxVisible = false)
        {

            if (Degiskenler.TestRun)
                return;

            try
            {
                Gonder(kime, cc, gorunenIsım, konu, mesaj, dosyaList, mailOturumAdi, mailSifre, serverAdi, enableSsl, smtpPort);

                if (msgBoxVisible && Degiskenler.FromWinServis == false)
                {
                    if (string.IsNullOrWhiteSpace(MailBasariMesajı))
                        Mesaj.Basari("Mail Gönderimi Başarılı.");
                    else
                        Mesaj.Basari(MailBasariMesajı);
                }
            }
            catch (Exception ex)
            {
                ExcHata = ex;
                if (Degiskenler.FromWinServis == false)
                {
                    if (ex is MyException)
                        Mesaj.Uyari(ex.Message);
                    else
                    {
                        if (string.IsNullOrWhiteSpace(MailHataMesajı))
                            Mesaj.BilgiOzelPencere("Mail gönderimi Başarısız!!", ex.Message);
                        else
                            Mesaj.BilgiOzelPencere(MailHataMesajı, ex.Message, System.Drawing.SystemIcons.Warning);
                    }
                }
            }

        }

    }
}
