using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    /// <summary>
    /// yeni irsaliye formu
    /// </summary>
    public class frmIrsaliye
    {
        [DataMember, DisplayName("Depo"), Required(ErrorMessage = "Boş bırakmayınız"), Range(1, int.MaxValue)]
        public int DepoID { get; set; }

        [DataMember, DisplayName("Evrak No"), Required(ErrorMessage = "Boş bırakmayınız"), StringLength(16)]
        public string EvrakNo { get; set; }

        [DataMember, DisplayName("Hesap Kodu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string HesapKodu { get; set; }

        [Key]
        public int Id { get; set; }

        public bool Onay { get; set; }

        [DataMember, DisplayName("Şirket"), Required(ErrorMessage = "Boş bırakmayınız"), Range(1, int.MaxValue)]
        public string SirketID { get; set; }

        [DataMember, DisplayName("Tarih"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Tarih { get; set; }

        [DataMember, DisplayName("Ünvan"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Unvan { get; set; }
    }
}