namespace Wms12m.Entity
{

    /// <summary>
    /// görev id ve görevli id
    /// </summary>
    public class frmStokYer
    {
        public string Birim { get; set; }
        public int? DepoID { get; set; }
        public string HucreAd { get; set; }
        public int ID { get; set; }
        public int KatID { get; set; }
        public bool MakaraDurum { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
    }
}