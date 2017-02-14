using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    public class Store06
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Range(0, 999999, ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        public int KatID { get; set; }
        [DataMember]
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Genişlik Giriniz !!!")]
        public int Genislik { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Yükseklik Giriniz !!!")]
        public int Yukseklik { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Derinlik Giriniz !!!")]
        public int Derinlik { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Kapasite Giriniz !!!")]
        public int Kapasite { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Özellik Giriniz !!!")]
        public string Ozellik { get; set; }
        [DisplayName("Durum")]
        [Required(ErrorMessage = "Lütfen Açıklama Giriniz !!!")]
        public string Aciklama { get; set; }
        [DataMember]
        public int SiraNo { get; set; }
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
