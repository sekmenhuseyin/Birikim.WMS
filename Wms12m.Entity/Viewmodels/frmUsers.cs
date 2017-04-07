namespace Wms12m.Entity
{
    /// <summary>
    /// şifre değiştir
    /// </summary>
    public class frmUserChangePass
    {
        public int ID { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
    }
    /// <summary>
    /// giriş sonrası gelen veri
    /// </summary>
    public class Login
    {
        public int ID { get; set; }
        public string Kod { get; set; }
        public string AdSoyad { get; set; }
        public string DepoKodu { get; set; }
        public int DepoID { get; set; }
    }
}
