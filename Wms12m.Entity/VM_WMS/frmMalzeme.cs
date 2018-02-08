using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    /// <summary>
    /// json result birim
    /// </summary>
    public class frmBirims
    {
        public string Birim1 { get; set; }
        public string Birim2 { get; set; }
        public string Birim3 { get; set; }
        public string Birim4 { get; set; }
    }

    /// <summary>
    /// sipraişten malzeme ekleme
    /// </summary>
    public class frmFromSiparis
    {
        // finsat6x.spi.chk
        public string chk { get; set; }

        // irsaliye id
        public string id { get; set; }

        // şirket kod
        public string kod { get; set; }

        // açık miktar
        public string miktar { get; set; }
    }

    /// <summary>
    /// json result
    /// </summary>
    public class frmJson
    {
        public string id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
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
    /// malzeme ekleme formu
    /// </summary>
    public class frmMalzeme
    {
        [DataMember, DisplayName("Birim"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Birim { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public int IrsaliyeId { get; set; }

        [DataMember, DisplayName("Makara No")]
        public string MakaraNo { get; set; }

        [DataMember, DisplayName("Mal Adı"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalAdi { get; set; }

        [DataMember, DisplayName("Mal Kodu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string MalKodu { get; set; }

        [DataMember, DisplayName("Miktar"), Required(ErrorMessage = "Boş bırakmayınız"), Range(1, int.MaxValue)]
        public decimal Miktar { get; set; }
    }

    /// <summary>
    /// malzeme arama
    /// </summary>
    public class frmMalzemeSearch
    {
        public string ad { get; set; }
        public string kod { get; set; }
    }

    /// <summary>
    /// stok sayfası 1
    /// </summary>
    public class frmStok1
    {
        public string Birim { get; set; }
        public string HucreAd { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
    }

    /// <summary>
    /// rafa yerleştir formu
    /// </summary>
    public class frmYerlesme
    {
        public string Birim { get; set; }
        public int DepoID { get; set; }
        public int GorevID { get; set; }
        public int IrsDetayID { get; set; }
        public int IrsID { get; set; }
        public string MakaraNo { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public string RafNo { get; set; }
    }
}