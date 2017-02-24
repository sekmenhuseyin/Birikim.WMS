using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    public class Store01
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Depo Adı Giriniz !!!")]
        public string DepoAdi { get; set; }
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
        [DataMember]
        [Required(ErrorMessage = "Lütfen Depo Kodu Giriniz !!!")]
        public string DepoKodu { get; set; }
    }
}
