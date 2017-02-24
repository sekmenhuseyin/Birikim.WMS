using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    public class Store04
    {
        [Key]
        public int ID { get; set; }
        [DataMember]
        [Range(0, 999999, ErrorMessage = "Lütfen Raf Seçiniz !!!")]
        [Required(ErrorMessage = "Lütfen Raf Seçiniz !!!")]
        public int RafID { get; set; }
        [DataMember]
        [Required(ErrorMessage = "Lütfen Bölüm Giriniz !!!")]
        public string Bolum { get; set; }
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
