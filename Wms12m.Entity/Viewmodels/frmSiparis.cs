using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi
    /// </summary>
    public class frmSiparisler
    {
        public string SirketID { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public int Saat { get; set; }
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public string GrupKod { get; set; }
        public string FaturaAdres { get; set; }
        public string Aciklama { get; set; }
        public int Çeşit { get; set; }
        public decimal Miktar { get; set; }
    }
    /// <summary>
    /// sipariş listesi onay
    /// </summary>
    public class frmSiparisOnay
    {
        public string SirketID { get; set; }
        [Required]
        public string DepoID { get; set; }
        [Required]
        public string checkboxes { get; set; }
        public string EvrakNos { get; set; }
    }
    /// <summary>
    /// siparişlerin malzemeleri
    /// </summary>
    public class frmSiparisMalzeme
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public int KatID { get; set; }
        public decimal Stok { get; set; }
    }
    /// <summary>
    /// siparişlerin malzeme detay
    /// </summary>
    public class frmSiparisMalzemeDetay
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal Stok { get; set; }
    }
    /// <summary>
    /// sipariş toplama sırası
    /// </summary>
    public class frmSiparisToplama
    {
        public string EvrakNo { get; set; }
        public int YerID { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string Koridor { get; set; }
        public string Raf { get; set; }
        public string Bolum { get; set; }
        public string Kat { get; set; }
    }
}
