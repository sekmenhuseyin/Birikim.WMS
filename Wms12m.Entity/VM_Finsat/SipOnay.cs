namespace Wms12m.Entity
{
    public class SipOnay
    {
        public bool Secim { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string TipKodu { get; set; }
        public decimal KrediLimiti { get; set; }
        public decimal Bakiye { get; set; }
        public decimal PRTBakiye { get; set; }
        public decimal Risk { get; set; }
        public decimal SCek { get; set; }
        public decimal TCek { get; set; }
        public string Kod2 { get; set; }
        public decimal OrtGun { get; set; }
        public decimal Kod3OrtGun { get; set; }
        public decimal Kod3OrtBakiye { get; set; }
        public decimal SicakSiparis { get; set; }
        public decimal SogukSiparis { get; set; }
        public decimal GunIciSiparis { get; set; }
        public string SiparisTuru { get; set; }
        public string EvrakNo { get; set; }
        public string OnayDurumu { get; set; }
        public string Tarih { get; set; }
        public string Firma { get; set; }
        public string Onaylayan { get; set; }
        public decimal RiskBakiyesi { get; set; }
    }
}