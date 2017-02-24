using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    public  class Store03
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [Range(0, 999999, ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        public int KoridorID { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Raf Giriniz !!!")]
        public string Raf { get; set; }
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
