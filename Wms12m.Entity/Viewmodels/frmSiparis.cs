using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş listesi
    /// </summary>
    public class frmSiparisler
    {
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
    }
    /// <summary>
    /// sipariş listesi onay
    /// </summary>
    public class frmSiparisOnay
    {
        [Required]
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
    }
    /// <summary>
    /// siparişlerin malzeme detay
    /// </summary>
    public class frmSiparisMalzemeDetay
    {
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal Stok { get; set; }
    }
}
