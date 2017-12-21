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
    /// <summary>
    /// stoklar
    /// </summary>
    public class frmStokComparison
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Birim { get; set; }
        public decimal WmsStok { get; set; }
        public decimal GunesStok { get; set; }
    }
    /// <summary>
    /// rezrve edilmiş stok listesi
    /// </summary>
    public partial class frmRezerveStok
    {
        public string SirketKod { get; set; }
        public string GorevNo { get; set; }
        public string DepoKodu { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public int OlusturmaTarihi { get; set; }
        public string Bilgi { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string MakaraNo { get; set; }
        public int? KynkSiparisID { get; set; }
        public string KynkSiparisNo { get; set; }
        public short? KynkSiparisSiraNo { get; set; }
        public int? KynkSiparisTarih { get; set; }
        public decimal? KynkSiparisMiktar { get; set; }
    }
}
