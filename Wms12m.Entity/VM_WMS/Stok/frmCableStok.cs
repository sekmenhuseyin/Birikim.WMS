namespace Wms12m.Entity
{
    /// <summary>
    /// kablo stok sayfası
    /// </summary>
    public partial class frmCableStok
    {
        public string Birim { get; set; }
        public string cins { get; set; }
        public int? DepoID { get; set; }
        public string HucreAd { get; set; }
        public int ID { get; set; }
        public int KatID { get; set; }
        public string kesit { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public string marka { get; set; }
        public decimal Miktar { get; set; }
        public string renk { get; set; }
    }
}