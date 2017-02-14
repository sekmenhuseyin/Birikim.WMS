using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    public  class Store03
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Range(0, 999999, ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        public int KoridorID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Raf Giriniz !!!")]
        public string Raf { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public bool Aktif { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public int SiraNo { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public int KayitTarih { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public string Kaydeden { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public int DegisTarih { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public string Degistiren { get; set; }
    }
}
