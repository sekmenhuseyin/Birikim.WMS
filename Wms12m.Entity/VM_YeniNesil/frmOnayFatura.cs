namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class frmOnayFatura
    {
        public string Birim { get; set; }
        public int Cesit { get; set; }
        public string DegisSaat { get; set; }
        public string DegisTarih { get; set; }
        public string Degistiren { get; set; }
        public string Depo { get; set; }
        public decimal EsikFiyat { get; set; }
        public string EvrakNo { get; set; }
        public decimal Fiyat { get; set; }
        public string HesapKodu { get; set; }
        public int ID { get; set; }
        public short IslemDurumu { get; set; }
        public string Kaydeden { get; set; }
        public string KayitSaat { get; set; }
        public string KayitTarih { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public string Notlar { get; set; }
        public string ParaCinsi { get; set; }
        public short SiraNo { get; set; }
        public string Unvan { get; set; }
    }
}