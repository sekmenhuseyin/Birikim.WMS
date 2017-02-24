using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    public class Store02
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [Range(0, 999999, ErrorMessage = "Lütfen Depo Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Depo Seçiniz !!!")]
        public int DepoID { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Koridor Giriniz !!!")]
        public string Koridor { get; set; }
        [DataMember]
        public bool Aktif { get; set; }
        [DataMember]
        public int SiraNo { get; set; }
        [DataMember]
        public int KayitTarih { get; set; }
        [DataMember]
        public string Kaydeden { get; set; }
        [DataMember]
        public int DegisTarih { get; set; }
        [DataMember]
        public string Degistiren { get; set; }

    }
}
