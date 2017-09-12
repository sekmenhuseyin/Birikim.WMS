using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Wms12m.Entity.Models;

namespace Wms12m.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class frmTaskDetails
    {
        public List<GetIrsDetayfromGorev_Result> irsdetay { get; set; }
        public Gorev grv { get; set; }
    }
    /// <summary>
    /// tüm tablo
    /// </summary>
    public class frmGorev
    {
        [Key]
        public int ID { get; set; }

        [DataMember, DisplayName("Depo"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int DepoID { get; set; }

        [DataMember, DisplayName("Görev No"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string GorevNo { get; set; }

        [DataMember, DisplayName("Görev Tipi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int GorevTipiID { get; set; }

        [DataMember, DisplayName("Görev Durumu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int DurumID { get; set; }

        [DataMember, DisplayName("Görevli"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int GorevliID { get; set; }

        [DataMember, DisplayName("Atayan"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int AtayanID { get; set; }
        public int IrsaliyeID { get; set; }

        [DataMember, DisplayName("Oluşturma Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int OlusturmaTarihi { get; set; }
        [DataMember, DisplayName("Atama Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int AtamaTarihi { get; set; }
        [DataMember, DisplayName("Bitirme Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int BitirmeTarihi { get; set; }
        [DataMember, DisplayName("Bilgi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Bilgi { get; set; }
        [DataMember, DisplayName("Açıklama"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Aciklama { get; set; }
    }
    /// <summary>
    /// görev listesi
    /// </summary>
    public class frmGorevJson
    {
        public string Gorevli { get; set; }
        public string Atayan { get; set; }
        public string Olusturan { get; set; }
        public string EvrakNo { get; set; }
        public int OlusturmaTarihi { get; set; }
        public int OlusturmaSaati { get; set; }
        public int? AtamaTarihi { get; set; }
        public int? BitisTarihi { get; set; }
        public int? BitisSaati { get; set; }
        public string Bilgi { get; set; }
        public string Aciklama { get; set; }
    }
    /// <summary>
    /// görev id ve görevli id
    /// </summary>
    public class frmGorevli
    {
        public int ID { get; set; }
        public string Gorevli { get; set; }
    }
    /// <summary>
    /// görev id ve görevli id
    /// </summary>
    public class frmStokList2
    {
        public int ID { get; set; }
        public string DepoAd { get; set; }
        public decimal Miktar { get; set; }
    }
    /// <summary>
    /// şirket
    /// </summary>
    public class frmSirkets
    {
        public string Kod { get; set; }
    }
    /// <summary>
    /// sipariş toplamadaki kontrol
    /// </summary>
    public class frmSiparisToplaKontrol
    {
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
    }
    /// <summary>
    /// sipariş toplamadaki yerleştirmeden düşme
    /// </summary>
    public class frmSiparisToplayerlestirilen
    {
        public int YerID { get; set; }
        public string MakaraNo { get; set; }
        public string MalKodu { get; set; }
        public string Birim { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
    }
    /// <summary>
    /// şirket
    /// </summary>
    public class frmPaketBarkod
    {
        public string GonderenUnvan { get; set; }
        public string tel { get; set; }
        public string fax { get; set; }
        public string GonderenAdres { get; set; }
        public string FaturaAdres { get; set; }
        public string Unvan { get; set; }
        public string TeslimAdres { get; set; }
    }
    public class frmGorevTodos
    {
        public int ID { get; set; }
        public int GorevID { get; set; }
        public string Aciklama { get; set; }
        public bool AktifPasif { get; set; }
        public bool OnayDurum { get; set; }
        public string Kaydeden { get; set; }
        public DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public DateTime? DegisTarih { get; set; }
    }

    public class frmGorevCalismas
    {
        public int ID { get; set; }
        public int GorevID { get; set; }
        public System.DateTime Tarih { get; set; }
        public int CalismaSure { get; set; }
        public string Calisma { get; set; }
        public string ToDoListID { get; set; }
        public string Kaydeden { get; set; }
        public System.DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public System.DateTime DegisTarih { get; set; }
    }
}
