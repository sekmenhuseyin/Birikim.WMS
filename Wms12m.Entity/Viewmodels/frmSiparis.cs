using System;
using System.ComponentModel;
using System.Runtime.Serialization;
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
    }
    /// <summary>
    /// siparişlerin malzemeleri
    /// </summary>
    public class frmSiparisMalzeme
    {
        [Required]
        public string MalKodu { get; set; }
        [Required]
        public string MalAdi { get; set; }
        [Required]
        public decimal Miktar { get; set; }
        [Required]
        public string Birim { get; set; }
    }
}
