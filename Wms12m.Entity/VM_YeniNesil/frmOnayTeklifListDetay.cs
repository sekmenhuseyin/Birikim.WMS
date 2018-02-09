namespace Wms12m.Entity
{
    /// <summary>
    /// teklif detay
    /// </summary>
    public class frmOnayTeklifListDetay
    {
        public string TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal Miktar { get; set; }
        public string Kaydeden { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Fiyat { get; set; }
        public decimal EsikFiyat { get; set; }
        public decimal Tutar { get; set; }
        public string DovizCinsi { get; set; }
        public string KayitTarihi { get; set; }
    }
}