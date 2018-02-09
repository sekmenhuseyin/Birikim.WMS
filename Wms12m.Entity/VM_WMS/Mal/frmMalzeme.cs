using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    /// <summary>
    /// malzeme ekleme formu
    /// </summary>
    public class frmMalzeme
    {
        [DataMember, DisplayName("Birim"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Birim { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public int IrsaliyeId { get; set; }

        [DataMember, DisplayName("Makara No")]
        public string MakaraNo { get; set; }

        [DataMember, DisplayName("Mal Adı"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalAdi { get; set; }

        [DataMember, DisplayName("Mal Kodu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalKodu { get; set; }

        [DataMember, DisplayName("Miktar"), Required(ErrorMessage = "Boş bırakmayınız"), Range(1, int.MaxValue)]
        public decimal Miktar { get; set; }
    }
}