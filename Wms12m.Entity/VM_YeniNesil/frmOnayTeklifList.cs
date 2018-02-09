namespace Wms12m.Entity
{
    /// <summary>
    /// teklif onay
    /// </summary>
    public class frmOnayTeklifList
    {
        public string TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public int? Cesit { get; set; }
        public decimal? Miktar { get; set; }
        public string Kaydeden { get; set; }
        public string KayitTarihi { get; set; }
        public string TeklifTarihi { get; set; }
    }
}