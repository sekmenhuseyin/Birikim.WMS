using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    public class frmSiparisOnay
    {
        [Required]
        public string checkboxes { get; set; }

        [Required]
        public string DepoID { get; set; }

        public string[] EvrakNo { get; set; }
        public string EvrakNos { get; set; }
        public string SirketID { get; set; }
        public string Tarih { get; set; } = "";
    }
}