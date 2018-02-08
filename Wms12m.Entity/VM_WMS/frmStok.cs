namespace Wms12m.Entity
{
    /// <summary>
    /// ölçü tablosu
    /// </summary>
    public class frmOlcu
    {
        public decimal Agirlik { get; set; }
        public string Birim { get; set; }
        public decimal Boy { get; set; }
        public decimal Derinlik { get; set; }
        public decimal En { get; set; }
        public decimal Hacim { get; set; }
        public int ID { get; set; }
        public string Kaydeden { get; set; }
        public string MalKodu { get; set; }
    }

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

    /// <summary>
    /// stoklar
    /// </summary>
    public class frmStokComparison
    {
        public string Birim { get; set; }
        public decimal GunesStok { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal WmsStok { get; set; }
    }

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

    /// <summary>
    /// stoklar
    /// </summary>
    public class frmStokDetay
    {
        public decimal GunesStok { get; set; }
        public decimal WmsRezerv { get; set; }
        public decimal WmsStok { get; set; }
    }

    /// <summary>
    /// görev id ve görevli id
    /// </summary>
    public class frmStokList
    {
        public string DepoAd { get; set; }
        public int ID { get; set; }
        public decimal Miktar { get; set; }
        public decimal WmsRezerv { get; set; }
    }

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