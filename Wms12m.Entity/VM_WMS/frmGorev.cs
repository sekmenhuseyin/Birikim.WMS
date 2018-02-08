using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Wms12m.Entity.Models;

namespace Wms12m.Entity
{
    /// <summary>
    /// git json
    /// </summary>
    public class ForJson
    {
        public DateTime Date { get; set; }
        public Guid Id { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// ToDosList.cshtml
    /// </summary>
    public class ForToDosList
    {
        public DateTime checkin { get; set; }
        public string GitGuid { get; set; }
        public string Kaydeden { get; set; }
    }

    /// <summary>
    /// tüm tablo
    /// </summary>
    public class frmGorev
    {
        [DataMember, DisplayName("Açıklama"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Aciklama { get; set; }

        [DataMember, DisplayName("Atama Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int AtamaTarihi { get; set; }

        [DataMember, DisplayName("Atayan"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int AtayanID { get; set; }

        [DataMember, DisplayName("Bilgi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string Bilgi { get; set; }

        [DataMember, DisplayName("Bitirme Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int BitirmeTarihi { get; set; }

        [DataMember, DisplayName("Depo"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int DepoID { get; set; }

        [DataMember, DisplayName("Görev Durumu"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int DurumID { get; set; }

        [DataMember, DisplayName("Görevli"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int GorevliID { get; set; }

        [DataMember, DisplayName("Görev No"), Required(ErrorMessage = "Boş bırakmayınız")]
        public string GorevNo { get; set; }

        [DataMember, DisplayName("Görev Tipi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int GorevTipiID { get; set; }

        [Key]
        public int ID { get; set; }

        public int IrsaliyeID { get; set; }

        [DataMember, DisplayName("Oluşturma Tarihi"), Required(ErrorMessage = "Boş bırakmayınız")]
        public int OlusturmaTarihi { get; set; }
    }

    public class frmGorevCalismas
    {
        public string Calisma { get; set; }
        public int CalismaSure { get; set; }
        public DateTime DegisTarih { get; set; }
        public string Degistiren { get; set; }
        public int GorevID { get; set; }
        public int ID { get; set; }
        public string Kaydeden { get; set; }
        public DateTime KayitTarih { get; set; }
        public DateTime Tarih { get; set; }
        public string ToDoListID { get; set; }
    }

    /// <summary>
    /// görev çalışmaları
    /// </summary>
    public class frmGorevDestekCalisma
    {
        public string Calisma { get; set; }
        public int GorevID { get; set; }
        public int MusteriID { get; set; }
        public int Sure { get; set; }
        public DateTime Tarih { get; set; }
    }

    /// <summary>
    /// görev listesi
    /// </summary>
    public class frmGorevJson
    {
        public string Bilgi { get; set; }
        public string BitisTarihi { get; set; }
        public string DepoAd { get; set; }
        public int DurumID { get; set; }
        public string EvrakNo { get; set; }
        public string Gorevli { get; set; }
        public string GorevNo { get; set; }
        public string GorevTipi { get; set; }
        public int ID { get; set; }
        public string OlusturmaTarihi { get; set; }
        public string SiparisTarihi { get; set; }
    }

    /// <summary>
    /// görev çalışmaları
    /// </summary>
    public class frmGorevlerCalismalar
    {
        public int Sure { get; set; }
        public DateTime Tarih { get; set; }
    }

    /// <summary>
    /// görev id ve görevli id
    /// </summary>
    public class frmGorevli
    {
        public string Gorevli { get; set; }
        public int ID { get; set; }
    }

    /// <summary>
    /// sayım fişi getirir
    /// </summary>
    public class frmGorevSayimFisi
    {
        public string Birim { get; set; }
        public string Depo { get; set; }
        public short IslemTur { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public decimal Miktar2 { get; set; }
    }

    public class frmGorevTodos
    {
        public string Aciklama { get; set; }
        public bool AktifPasif { get; set; }
        public DateTime? DegisTarih { get; set; }
        public string Degistiren { get; set; }
        public int GorevID { get; set; }
        public int ID { get; set; }
        public string Kaydeden { get; set; }
        public DateTime KayitTarih { get; set; }
        public bool Kontrol { get; set; }
        public bool KontrolOnay { get; set; }
        public bool OnayDurum { get; set; }
    }

    public partial class frmIrsDetayfromGorev
    {
        public string Birim { get; set; }
        public string GorevNo { get; set; }
        public string HucreAd { get; set; }
        public int ID { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal? Miktar { get; set; }
        public int Sira { get; set; }
        public string SirketKod { get; set; }
    }

    /// <summary>
    /// şirket
    /// </summary>
    public class frmPaketBarkod
    {
        public string FaturaAdres { get; set; }
        public string fax { get; set; }
        public string GonderenAdres { get; set; }
        public string GonderenUnvan { get; set; }
        public string tel { get; set; }
        public string TeslimAdres { get; set; }
        public string Unvan { get; set; }
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
        public string Birim { get; set; }
        public string MakaraNo { get; set; }
        public string MalKodu { get; set; }
        public int YerID { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
    }

    /// <summary>
    /// şirket
    /// </summary>
    public class frmSirkets
    {
        public string Kod { get; set; }
    }

    /// <summary>
    /// görev ayrıntıları
    /// </summary>
    public class frmTaskDetails
    {
        public Gorev grv { get; set; }
        public List<frmIrsDetayfromGorev> irsdetay { get; set; }
    }

    public class frmUserss
    {
        public bool Admin { get; set; }
        public string AdSoyad { get; set; }
        public bool Aktif { get; set; }
        public string Email { get; set; }
        public int ID { get; set; }
        public string Kod { get; set; }
        public string RoleName { get; set; }
        public string Sifre { get; set; }
        public string Sirket { get; set; }
        public string Tema { get; set; }
        public short Tip { get; set; }
    }
}