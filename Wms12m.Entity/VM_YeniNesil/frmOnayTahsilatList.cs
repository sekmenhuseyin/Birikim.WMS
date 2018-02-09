namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnayTahsilatList
    {
        public string TahsilatNo { get; set; }
        public string Tarih { get; set; }

        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string IslemTuru { get; set; }
        public string OdemeTuru { get; set; }

        public decimal Tutar { get; set; }
        public string DovizCinsi { get; set; }

        public decimal KapatilanTL { get; set; }
        public decimal KapatilanUSD { get; set; }
        public decimal KapatilanEUR { get; set; }

        public string Kaydeden { get; set; }
        public string Aciklama { get; set; }

        public decimal USDKur { get; set; }
        public decimal EURKur { get; set; }
    }
}