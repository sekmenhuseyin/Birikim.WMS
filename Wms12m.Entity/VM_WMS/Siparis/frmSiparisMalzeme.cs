namespace Wms12m.Entity
{
    /// <summary>
    /// siparişlerin malzemeleri
    /// </summary>
    public class frmSiparisMalzeme
    {
        public string Birim { get; set; }
        public string EvrakNo { get; set; }
        public string HucreAd { get; set; }
        public int KatID { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public int? Sira { get; set; }
        public string SirketID { get; set; }
        public decimal Stok { get; set; }
        public decimal WmsRezerv { get; set; }
        public decimal WmsStok { get; set; }
    }
}