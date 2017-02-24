using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    public class Store05
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [Range(0, 999999, ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Koridor Seçiniz !!!")]
        public int RafID { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Kat Giriniz !!!")]
        public string Kat { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Genişlik Giriniz !!!")]
        public decimal En { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Yükseklik Giriniz !!!")]
        public decimal Boy { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Derinlik Giriniz !!!")]
        public decimal Derinlik { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Kapasite Giriniz !!!")]
        public int AgirlikKapasite { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Özellik Giriniz !!!")]
        public string Ozellik { get; set; }
        [DataMember]
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
