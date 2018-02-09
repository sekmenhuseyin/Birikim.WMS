namespace Wms12m.Entity
{
    /// <summary>
    /// stok detay
    /// </summary>
    public class frmStokdetail
    {
        public string Birim { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public bool IslemTur { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public string SirketKod { get; set; }
        public int Tarih { get; set; }
        public string Unvan { get; set; }
    }
}