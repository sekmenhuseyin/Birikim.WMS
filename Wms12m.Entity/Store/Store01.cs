using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    public class Store01
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Depo Adı Giriniz !!!")]
        public string DepoAdi { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public int SiraNo { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        public bool Aktif { get; set; }
        [DataMember]
        public int KayitTarih { get; set; }
        [DataMember]
        public string Kaydeden { get; set; }
        [DataMember]
        public int DegisTarih { get; set; }
        [DataMember]
        public string Degistiren { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Depo Kodu Giriniz !!!")]
        public string DepoKodu { get; set; }
    }
}
