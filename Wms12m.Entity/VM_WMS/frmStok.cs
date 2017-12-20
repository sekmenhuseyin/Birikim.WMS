namespace Wms12m.Entity
{
    /// <summary>
    /// görev id ve görevli id
    /// </summary>
    public class frmStokList
    {
        public int ID { get; set; }
        public string DepoAd { get; set; }
        public decimal Miktar { get; set; }
        public decimal WmsRezerv { get; set; }
    }
    /// <summary>
    /// görev id ve görevli id
    /// </summary>
    public class frmStokYer
    {
        public int ID { get; set; }
        public int KatID { get; set; }
        public int? DepoID { get; set; }
        public string HucreAd { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string MakaraNo { get; set; }
        public bool MakaraDurum { get; set; }
    }
    /// <summary>
    /// stoklar
    /// </summary>
    public class frmStokDetay
    {
        public decimal WmsStok { get; set; }
        public decimal WmsRezerv { get; set; }
        public decimal GunesStok { get; set; }
    }
}
