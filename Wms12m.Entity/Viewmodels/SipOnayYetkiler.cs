using System;

namespace Wms12m.Entity
{
    public class SipOnayYetkiler
    {
        public int UserID { get; set; }
        public string Kod { get; set; }
        public string AdSoyad { get; set; }
        public string GosterilecekSirket { get; set; }
        public string GostCHKKodAlani { get; set; }
        public string GostSTKDeger { get; set; }
        public Nullable<decimal> GostRiskDeger { get; set; }
        public Nullable<decimal> GostKod3OrtBakiye { get; set; }
    }
}
