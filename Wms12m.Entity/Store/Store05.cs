using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    public class Store05
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Range(0, 999999, ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        public int RafID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Kat Giriniz !!!")]
        public string Kat { get; set; }
        [DataMember]
        public int SiraNo { get; set; }
        [DataMember]
        public decimal Boy { get; set; }
        [DataMember]
        public decimal Ozellik { get; set; }
        [DataMember]
        public decimal Aciklama { get; set; }
        [DataMember]
        public bool Aktif { get; set; }
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
