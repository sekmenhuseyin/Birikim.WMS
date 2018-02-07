using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Wms12m
{
    /// <summary>
    /// bu class ile tüm proje içinde kullanılan ortak fonksiyonları tanımlıyoruz.
    /// </summary>
    public class Functions
    {
        /// <summary>
        /// most used passwords
        /// </summary>
        private string[] mostUsedPass = { "11111", "111111", "1111111", "12345", "123456", "1234567", "12345678", "123456789", "654321", "1qaz2wsx", "password", "iloveyou", "princess", "football", "baseball", "rockyou", "abc123", "nicole", "daniel", "babygirl", "monkey", "master", "admin", "login", "jessica", "lovely", "michael", "ashley", "winter", "summer", "qwerty", "qwertyuiop", "sifre", "welcome", "welcome1", "welcome2", "welcome01", "passw0rd", "password1", "password2", "password3", "password4", "password6", "password7", "password8", "password9", "password01", "password123" };

        /// <summary>
        /// find browser name
        /// </summary>
        public string FindBrowser()
        {
            var context = HttpContext.Current;
            return context.Request.Browser.Browser;
        }

        /// <summary>
        /// find is mobile from from browser
        /// </summary>
        public bool FindisMobile()
        {
            var context = HttpContext.Current;
            return context.Request.Browser.IsMobileDevice;
        }

        /// <summary>
        /// find os name from browser
        /// </summary>
        public string FindOS()
        {
            var context = HttpContext.Current;
            var OS = context.Request.Browser.Platform;
            if (OS == "Unknown")
            {
                if (context.Request.UserAgent.IndexOf("Windows Phone") > 0) { OS = "Windows Phone"; }
                else if (context.Request.UserAgent.IndexOf("iPhone") > 0) { OS = "iPhone"; }
                else if (context.Request.UserAgent.IndexOf("iPad") > 0) { OS = "iPad"; }
                else if (context.Request.UserAgent.IndexOf("iPod") > 0) { OS = "iPod"; }
                else if (context.Request.UserAgent.IndexOf("Macintosh") > 0) { OS = "Macintosh"; }
                else if (context.Request.UserAgent.IndexOf("Android") > 0) { OS = "Android"; }
                else if (context.Request.UserAgent.IndexOf("Nokia") > 0) { OS = "Nokia"; }
                else if (context.Request.UserAgent.IndexOf("BlackBerry") > 0) { OS = "BlackBerry"; }
                else { OS = ""; }
            }
            else if (OS == "WinNT")
            {
                if (context.Request.UserAgent.IndexOf("Windows NT 10") > 0) { OS = "Windows 10"; }
                else if (context.Request.UserAgent.IndexOf("Windows NT 6.3") > 0) { OS = "Windows 8.1"; }
                else if (context.Request.UserAgent.IndexOf("Windows NT 6.2") > 0) { OS = "Windows 8"; }
                else if (context.Request.UserAgent.IndexOf("Windows NT 6.1") > 0) { OS = "Windows 7"; }
                else if (context.Request.UserAgent.IndexOf("Windows NT 6.0") > 0) { OS = "Windows Vista"; }
                else { OS = "Old Windows"; }
            }

            return OS;
        }

        /// <summary>
        /// finds user agent
        /// </summary>
        public string FindUserAgent()
        {
            var context = HttpContext.Current;
            return context.Request.UserAgent;
        }

        /// <summary>
        /// kullanıcının IP sini bulan prosedür
        /// </summary>
        public string GetIPAddress()
        {
            var context = HttpContext.Current;
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0) { return addresses[0]; }//eğer doğruysa bunu yolla
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        /// <summary>
        /// iiif in c#
        /// </summary>
        public object iif(bool expression, object truePart, object falsePart)
        { return expression ? truePart : falsePart; }

        /// <summary>
        /// isBoolean
        /// </summary>
        public bool isBoolean(bool str)
        {
            if (str == true || str == false) return true;
            else return false;
        }

        /// <summary>
        /// email mi değilmi bulan fonksiyonu
        /// </summary>
        public bool isEmail(string email)
        {
            try
            {
                var a = new System.Net.Mail.MailAddress(email);
            }
            catch { return false; }
            if (email.IndexOf("@mswork") > 0) return false;
            else if (email.IndexOf("@leeching") > 0) return false;
            else if (email.IndexOf("@extremail") > 0) return false;
            else if (email.IndexOf("@kismail") > 0) return false;
            else if (email.IndexOf("@divismail") > 0) return false;
            else if (email.IndexOf("@sharklasers") > 0) return false;
            else if (email.IndexOf("@grr") > 0) return false;
            else if (email.IndexOf("@guerrillamail") > 0) return false;
            else if (email.IndexOf("@spam") > 0) return false;
            else if (email.IndexOf("@turoid") > 0) return false;
            else if (email.IndexOf("temp") > 0) return false;
            else if (email.IndexOf("@trbvn") > 0) return false;
            else if (email.IndexOf("@thraml") > 0) return false;
            else if (email.IndexOf("@vvx") > 0) return false;
            else if (email.IndexOf("@yomail") > 0) return false;
            else if (email.IndexOf("@dropmail") > 0) return false;
            else if (email.IndexOf("@10mail") > 0) return false;
            else if (email.IndexOf("@emltmp") > 0) return false;
            else if (email.IndexOf("@cetesavaslari") > 0) return false;
            else if (email.IndexOf("@mailinator") > 0) return false;
            else return true;
        }

        /// <summary>
        /// isnumeric
        /// </summary>
        public bool isNumeric(object value)
        {
            if (value != null)
            {
                var reNum = new Regex(@"^\d+$");
                var isNumeric = reNum.Match(value.ToString()).Success;
                return isNumeric;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// şifrenin zorluğunu doğrulayan fonksiyon
        /// </summary>
        public bool isStrongPass(string pass)
        {
            var sonuc = false;
            var passComplexity = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{5,100}$";
            var re = new Regex(passComplexity);
            if (re.IsMatch(pass)) sonuc = true;
            if (Array.IndexOf(mostUsedPass, pass) == -1) sonuc = true;
            return sonuc;
        }

        /// <summary>
        /// internet adresini doğrulayan prosedür
        /// </summary>
        public bool IsValidUrl(string text)
        {
            var r = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
            return r.IsMatch(text);
        }

        /// <summary>
        /// Türkçe karakterleri silen fonksiyon
        /// </summary>
        public string RemoveNonAscii(string str)
        {
            str = str.Replace("Ç", "C").Replace("Ş", "S").Replace("Ö", "O").Replace("Ü", "U").Replace("İ", "I").Replace("Ğ", "G");
            str = str.Replace("ç", "c").Replace("ş", "s").Replace("ö", "o").Replace("ü", "u").Replace("ı", "i").Replace("ğ", "g");
            return str;
        }

        /// <summary>
        /// db için oadate oluşturur
        /// </summary>
        public int ToOADate() => DateTime.Today.ToOADateInt();

        /// <summary>
        /// db için saat oluşturur
        /// </summary>
        public int ToOATime() => DateTime.Now.ToOaTime();

        /// <summary>
        /// titlecase dönüştürme prosedürü
        /// </summary>
        public string ToTitleCase(string Text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Text);
        }

        /// <summary>
        /// check if an url exists
        /// </summary>
        protected bool CheckUrlExists(string url)
        {
            // If the url does not contain Http. Add it.
            if (!url.Contains("http://"))
                url = "http://" + url;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                    return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}