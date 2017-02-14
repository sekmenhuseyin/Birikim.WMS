using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    public class Store02
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [DisplayName("Depo")]
        [Range(0, 999999, ErrorMessage = "Lütfen Depo Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Depo Seçiniz !!!")]
        public int DepoID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Koridor Giriniz !!!")]
        public string Koridor { get; set; }
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
