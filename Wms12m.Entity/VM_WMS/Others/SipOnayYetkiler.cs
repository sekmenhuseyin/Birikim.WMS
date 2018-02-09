namespace Wms12m.Entity
{

    /// <summary>
    /// sipariş onay
    /// </summary>
    public class SipOnayYetkiler
    {
        public string AdSoyad { get; set; }
        public string GostCHKKodAlani { get; set; }
        public string GosterilecekSirket { get; set; }
        public string GostKod3OrtBakiye { get; set; }
        public string GostRiskDeger { get; set; }
        public string GostSTKDeger { get; set; }
        public string Kod { get; set; }
        public string RoleName { get; set; }
        public int UserID { get; set; }
    }
}