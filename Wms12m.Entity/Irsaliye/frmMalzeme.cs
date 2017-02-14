using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    public class frmMalzeme
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IrsaliyeId { get; set; }

        [DataMember, DisplayName("Mal Kodu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalKodu { get; set; }

        [DataMember, DisplayName("Mal Adı"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalAdi { get; set; }

        [DataMember, DisplayName("Miktar"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int Miktar { get; set; }

        [DataMember, DisplayName("Birim"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Birim { get; set; }

        [DataMember, DisplayName("Depo"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Depo { get; set; }
    }
}
