namespace Wms12m.Entity
{
    /// <summary>
    /// rezrve edilmiş stok listesi
    /// </summary>
    public class frmRezerveStok
    {
        public string Bilgi { get; set; }
        public string Birim { get; set; }
        public string DepoKodu { get; set; }
        public string EvrakNo { get; set; }
        public string GorevNo { get; set; }
        public string HesapKodu { get; set; }
        public int? KynkSiparisID { get; set; }
        public decimal? KynkSiparisMiktar { get; set; }
        public string KynkSiparisNo { get; set; }
        public short? KynkSiparisSiraNo { get; set; }
        public int? KynkSiparisTarih { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public int OlusturmaTarihi { get; set; }
        public string SirketKod { get; set; }
        public string Unvan { get; set; }
    }
}