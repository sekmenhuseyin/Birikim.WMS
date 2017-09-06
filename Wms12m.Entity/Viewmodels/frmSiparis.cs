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
        public string SirketID { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string HucreAd { get; set; }
        public string MakaraNo { get; set; }
        public int KatID { get; set; }
        public int Sira { get; set; }
        public decimal Stok { get; set; }
        public decimal WmsStok { get; set; }
    }
    /// <summary>
    /// siparişlerin malzeme detay
    /// </summary>
    public class frmSiparisMalzemeDetay
    {
        public string SirketID { get; set; }
        public int ID { get; set; }
        public string Unvan { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public int Saat { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal WmsStok { get; set; }
        public decimal GunesStok { get; set; }
    }
    /// <summary>
    /// sipariş onay formu
    /// </summary>
    public class frmSiparisMalzemeOnay
    {
        public string ID { get; set; }
        public int DegisSaat { get; set; }
        public int ROW_ID { get; set; }
        public string SirketID { get; set; }
        public string EvrakNo { get; set; }
        public string Chk { get; set; }
        public string TeslimChk { get; set; }
        public string Unvan { get; set; }
        public string MalKodu { get; set; }
        public decimal BirimMiktar { get; set; }
        public decimal Miktar { get; set; }
        public decimal Stok { get; set; }
        public string Birim { get; set; }
        public string Aciklama { get; set; }
        public short ValorGun { get; set; }
        public int Tarih { get; set; }
        public short SiraNo { get; set; }
        public string MalAdi4 { get; set; }//marka
        public string Nesne2 { get; set; }//cins
        public string Kod15 { get; set; }//kesit
    }
    /// <summary>
    /// sipariş toplama sırası
    /// </summary>
    public class frmSiparisToplama
    {
        public string EvrakNo { get; set; }
        public int KatID { get; set; }
        public int YerID { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string Koridor { get; set; }
        public string Raf { get; set; }
        public string Bolum { get; set; }
        public string Kat { get; set; }
        public decimal Stok { get; set; }
    }
}
