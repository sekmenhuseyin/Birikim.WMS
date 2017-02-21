using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Wms12m.Entity
{
    /// <summary>
    /// yeni irsaliye formu
    /// </summary>
    public class frmIrsaliye
    {
        [Key]
        public int Id { get; set; }

        [DataMember, DisplayName("Şirket"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string SirketID { get; set; }

        [DataMember, DisplayName("Depo"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int DepoID { get; set; }

        [DataMember, DisplayName("Hesap Kodu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string HesapKodu { get; set; }

        [DataMember, DisplayName("Ünvan"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Unvan { get; set; }

        [DataMember, DisplayName("Evrak No"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string EvrakNo { get; set; }

        [DataMember, DisplayName("Tarih"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Tarih { get; set; }
    }
    /// <summary>
    /// hesap kodu ve ünvanı: şirket değişince gelen bilgiler
    /// </summary>
    public class frmHesapUnvan
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
    }
    /// <summary>
    /// ünvanı: şirket değişince gelen bilgiler
    /// </summary>
    public class frmUnvan
    {
        public string Unvan { get; set; }
    }
}
