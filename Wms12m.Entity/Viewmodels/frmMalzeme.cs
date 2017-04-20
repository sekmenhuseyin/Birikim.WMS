using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    /// <summary>
    /// malzeme ekleme formu
    /// </summary>
    public class frmMalzeme
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int IrsaliyeId { get; set; }

        [DataMember, DisplayName("Mal Kodu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalKodu { get; set; }

        [DataMember, DisplayName("Mal Adı"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalAdi { get; set; }

        [DataMember, DisplayName("Miktar"), Required(ErrorMessage = "Boş bırakmayınız"), Range(1, Int32.MaxValue)]
        public decimal Miktar { get; set; }

        [DataMember, DisplayName("Birim"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Birim { get; set; }
    }
    /// <summary>
    /// malzeme arama
    /// </summary>
    public class frmMalzemeSearch
    {
        public string kod { get; set; }
        public string ad { get; set; }
    }
    /// <summary>
    /// sipraişten malzeme ekleme
    /// </summary>
    public class frmFromSiparis
    {
        //irsaliye id
        public string id { get; set; }
        //şirket kod
        public string kod { get; set; }
        //finsat6x.spi.chk
        public string chk { get; set; }
        //açık miktar
        public string miktar { get; set; }
    }
    /// <summary>
    /// mal kabul kayıt
    /// </summary>
    public class frmMalKabul
    {
        public int ID { get; set; }
        public decimal OkutulanMiktar { get; set; }
    }
    /// <summary>
    /// rafa yerleştir formu
    /// </summary>
    public class frmYerlesme
    {
        public int GorevID { get; set; }
        public int IrsID { get; set; }
        public int IrsDetayID { get; set; }
        public int DepoID { get; set; }
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public decimal Miktar { get; set; }
        public string RafNo { get; set; }
    }
    /// <summary>
    /// kablo siparişi için stk sütunları
    /// </summary>
    public class frmCableStk
    {
        public string MalAdi4 { get; set; }//marka
        public string Nesne2 { get; set; }//kesit
        public string Nesne3 { get; set; }//cins
        public string Kod15 { get; set; }//renk
        public decimal Miktar { get; set; }
    }

}
