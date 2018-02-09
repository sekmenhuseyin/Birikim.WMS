namespace Wms12m.Entity
{
    /// <summary>
    /// fatura kayıt işlemleri için
    /// </summary>
    public class SepetUrun
    {
        public string KullaniciKodu { get; set; }
        public string HesapKodu { get; set; }
        public string UrunKodu { get; set; }
        public string Depo { get; set; }
        public string ParaCinsi { get; set; }
        public string Birim { get; set; }
        public string Fiyat { get; set; }
        public string Miktar { get; set; }
        public int SatirTip { get; set; }  ///1 Fatura 2 Sipariş 3 Teklif
        public int OnayaDusme { get; set; }  ///0 Normal  1 Onaya Düşecek
        public string Adres1 { get; set; }
        public string Adres2 { get; set; }
        public string Adres3 { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public string Kaydeden { get; set; }
    }
}