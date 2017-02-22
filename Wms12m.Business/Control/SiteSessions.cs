using System.Web;

namespace Wms12m
{
    /// <summary>
    /// This wrapper provides a safe typed access from all over the application, one place to define all objects that might be stored in the session
    /// www.dev102.com/2008/05/07/why-should-you-wrap-your-aspnet-session-object/
    ///
    /// Type Safety: Session contains objects, so you need to cast.
    /// Null reference: You must check, it might be null
    /// Variable name: don’t use hard coded strings
    /// </summary>
    public class SiteSessions
    {
        //actions
        public static void Extend() {
            HttpContext.Current.Session.Timeout = 20;
        }
        public static void Clear()
        {
            LoggedRealName = "";
            LoggedUserName = "";
            LoggedUserNo = 0;
        }
        //user sessions
        public static string LoggedUserName
        {
            get { return GetFromSession<string>("LoggedUserName"); }
            set { SetInSession<string>("LoggedUserName", value); }
        }
        public static string LoggedRealName
        {
            get { return GetFromSession<string>("LoggedRealName"); }
            set { SetInSession<string>("LoggedRealName", value); }
        }
        public static int LoggedUserNo
        {
            get { return GetFromSession<int>("LoggedUserNo"); }
            set { SetInSession<int>("LoggedUserNo", value); }
        }
        /////////////////////////////////////////////////////////////////////////////////
        //bundan sonrası session kontrol fonksiyonları: hata vermesin diye bir kaç önlem
        //eğer session değeri boşsa default int/string/bool değerini gönderiyor
        /// <summary>
        /// gets session value
        /// </summary>
        private static T GetFromSession<T>(string key)
        {
            object obj = HttpContext.Current.Session[key];
            if (obj == null) { return default(T); }
            return (T)obj;
        }
        /// <summary>
        /// sets session value
        /// </summary>
        private static void SetInSession<T>(string key, T value)
        {
            //eğer değer boşsa session sil
            if (value == null || value.ToString() == "" || value.ToString() == "false" || value.ToString() == "-1") { HttpContext.Current.Session.Remove(key); }
            else { HttpContext.Current.Session[key] = value; HttpContext.Current.Session.Timeout = 20; }
        }
    }
}