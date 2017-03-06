using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Wms12m.Entity
{
    /// <summary>
    /// yeni irsaliye formu
    /// </summary>
    public class frmIrsaliye
    {
        [Key]
        public int Id { get; set; }

        [DataMember, DisplayName("Şirket"), Required(ErrorMessage = "Boş bırakmayınız"), Range(1, Int32.MaxValue)]
        public string SirketID { get; set; }

        [DataMember, DisplayName("Depo"), Required(ErrorMessage = "Boş bırakmayınız"), Range(1, Int32.MaxValue)]
        public int DepoID { get; set; }

        [DataMember, DisplayName("Hesap Kodu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string HesapKodu { get; set; }

        [DataMember, DisplayName("Ünvan"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Unvan { get; set; }

        [DataMember, DisplayName("Evrak No"), Required(ErrorMessage = "Boş bırakmayınız"), StringLength(16)]
        public string EvrakNo { get; set; }

        [DataMember, DisplayName("Tarih"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Tarih { get; set; }
        public bool Onay { get; set; }
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
    /// <summary>
    /// siparişten getir sayfası
    /// </summary>
    public class frmSiparistenGelen
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal AçıkMiktar { get; set; }
        public string Birim { get; set; }
    }
}
