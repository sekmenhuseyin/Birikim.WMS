using System;
using System.IO;
using System.Web;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace Wms12m
{
    /// <summary>
    /// Session wrapper gibi bir de cookie wrapper ekledim
    /// Böylece cookie boşsa hata vermeyecek. ilk sorguda boş olursa null yerine "" gönderecek
    /// boş mu değil mi diye kontrol etmemiz gerekmeyecek.
    /// </summary>
    public class SiteCookies
    {
        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be 32 bytes long.
        // Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("9fjr873hdkrh48s2");
        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;
        //actions
        public static void Clear()
        {
            IP = "";
            LoggedUserNo = 0;
        }
        //general cookies
        public static int WrongLogin
        {
            get
            {
                string WrongLogin = GetFromCookie("WrongLogin");
                if (WrongLogin == "")
                {
                    WrongLogin = "0";
                    SetCookie("WrongLogin", WrongLogin);
                }
                return Convert.ToInt32(WrongLogin);
            }
            set { SetCookie("WrongLogin", value.ToString()); }
        }
        public static string IP
        {
            get { return GetFromCookie("IP"); }
            set { SetCookie("IP", value); }
        }
        public static int LoggedUserNo
        {
            get
            {
                string LoggedUserNo = GetFromCookie("LoggedUserNo");
                if (LoggedUserNo == "")
                {
                    LoggedUserNo = "0";
                    SetCookie("LoggedUserNo", LoggedUserNo);
                }
                return Convert.ToInt32(LoggedUserNo);
            }
            set { SetCookie("LoggedUserNo", value.ToString()); }
        }
        ///////////////////////////////////////////////////////////////////////////////
        //bundan sonrası cookie kontrol fonksiyonları: hata vermesin diye bir kaç önlem
        //eğer cookie değeri boşsa default int/string/bool değerini gönderiyor
        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// get cookie value
        /// </summary>
        private static string GetFromCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[EncryptCookieName(key)];
            if (cookie == null) { return ""; }
            return Decrypt(cookie.Value);
        }
        /// <summary>
        /// set cookie value
        /// </summary>
        private static void SetCookie(string key, string value)
        {
            //değerler boşsa cookieleri sil
            if (value == null || value.ToString() == "" || value.ToString() == "false" || value.ToString() == "-1") { RemoveCookie(key); }
            else
            {//boş değilse ata
                HttpCookie cookie = new HttpCookie(EncryptCookieName(key), Encrypt(value.ToString())); cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now.AddDays(30); HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// delete cookie
        /// </summary>
        private static void RemoveCookie(string key)
        {
            HttpCookie cookie = new HttpCookie(EncryptCookieName(key)) { Expires = DateTime.Now.AddDays(-1) };
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        /// <summary>
        /// encrypts cookie names
        /// </summary>
        private static string EncryptCookieName(string plainText)
        {
            return Encrypt(plainText);
        }
        /// <summary>
        /// encrypts cookie value
        /// </summary>
        private static string Encrypt(string plainText)
        {
            string passPhrase = ConfigurationManager.AppSettings["ENCRYPTION_KEY"];
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// decrypts cookie value
        /// </summary>
        private static string Decrypt(string cipherText)
        {
            string passPhrase = ConfigurationManager.AppSettings["ENCRYPTION_KEY"];
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}