using System;

namespace Wms12m.Entity
{
    public class CekOnaySelect
    {
        public string EvrakNo { get; set; }
        public string Veren { get; set; }
        public string Unvan { get; set; }
        public string Borclu { get; set; }
        public string BorcluUnvan { get; set; }
        public string VadeTarihi { get; set; }
        public decimal? Tutar { get; set; }
        public int VadeTarih { get; set; }
        public decimal BaglantiTutar { get; set; }
        public DateTime? BaglantininOrtalamaVadesi { get; set; }

    }
    public class FiyatOnayGMSelect
    {
        public int ID { get; set; }
        public string FiyatListNum { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string HesapKodu { get; set; }
        public int BasTarih { get; set; }
        public int BitTarih { get; set; }
        public string Unvan { get; set; }
        public decimal SatisFiyat1 { get; set; }
        public string SatisFiyat1Birim { get; set; }
        public int SatisFiyat1BirimInt { get; set; }
        public decimal DovizSatisFiyat1 { get; set; }
        public string DovizSF1Birim { get; set; }
        public int DovizSF1BirimInt { get; set; }
        public string DovizCinsi { get; set; }
        public int ROW_ID { get; set; }
        public bool Onay { get; set; }
        public string Durum { get; set; }
    }
    //public class RiskTanim
    //{
    //    public int ID { get; set; }
    //    public string HesapKodu { get; set; }
    //    public string Unvan { get; set; }
    //    public decimal SahsiCekLimiti { get; set; }
    //    public decimal MusteriCekLimiti { get; set; }
    //    public bool? SMOnay { get; set; }
    //    public string SMOnaylayan { get; set; }
    //    public DateTime? SMOnayTarih { get; set; }
    //    public bool? SPGMYOnay { get; set; }
    //    public string SPGMYOnaylayan { get; set; }
    //    public DateTime? SPGMYOnayTarih { get; set; }
    //    public bool? MIGMYOnay { get; set; }
    //    public string MIGMYOnaylayan { get; set; }
    //    public DateTime? MIGMYOnayTarih { get; set; }
    //    public bool? GMOnay { get; set; }
    //    public string GMOnaylayan { get; set; }
    //    public DateTime? GMOnayTarih { get; set; }
    //    public short? OnayTip { get; set; }
    //    public bool? Durum { get; set; }
    //}
    public class SozlesmeOnaySelect
    {
        public string ListeNo { get; set; }
        public string ListeAdi { get; set; }
        public int BasTarih { get; set; }
        public int BasSaat { get; set; }
        public int BitTarih { get; set; }
        public int BitSaat { get; set; }
        public short MusUygSekli { get; set; }
        public short MusKodGrup { get; set; }
        public string MusteriKod { get; set; }
        public short MalUygSekli { get; set; }
        public short MalKodGrup { get; set; }
        public string MalKod { get; set; }
        public string SiraNo { get; set; }
        public Single Oran { get; set; }
        public Single Oran1 { get; set; }
        public Single Oran2 { get; set; }
        public Single Oran3 { get; set; }
        public Single Oran4 { get; set; }
        public Single Oran5 { get; set; }
        public decimal MikAralik1 { get; set; }
        public Single MikYuzde1 { get; set; }
        public decimal MikAralik2 { get; set; }
        public Single MikYuzde2 { get; set; }
        public decimal MikAralik3 { get; set; }
        public Single MikYuzde3 { get; set; }
        public decimal MikAralik4 { get; set; }
        public Single MikYuzde4 { get; set; }
        public decimal MikAralik5 { get; set; }
        public Single MikYuzde5 { get; set; }
        public decimal MikAralik6 { get; set; }
        public Single MikYuzde6 { get; set; }
        public decimal MikAralik7 { get; set; }
        public Single MikYuzde7 { get; set; }
        public decimal MikAralik8 { get; set; }
        public Single MikYuzde8 { get; set; }
        public decimal TutarAralik1 { get; set; }
        public Single TutarYuzde1 { get; set; }
        public decimal TutarAralik2 { get; set; }
        public Single TutarYuzde2 { get; set; }
        public decimal TutarAralik3 { get; set; }
        public Single TutarYuzde3 { get; set; }
        public decimal TutarAralik4 { get; set; }
        public Single TutarYuzde4 { get; set; }
        public decimal TutarAralik5 { get; set; }
        public Single TutarYuzde5 { get; set; }
        public decimal TutarAralik6 { get; set; }
        public Single TutarYuzde6 { get; set; }
        public decimal TutarAralik7 { get; set; }
        public Single TutarYuzde7 { get; set; }
        public decimal TutarAralik8 { get; set; }
        public Single TutarYuzde8 { get; set; }
        public decimal OdemeAralik1 { get; set; }
        public Single OdemeYuzde1 { get; set; }
        public decimal OdemeAralik2 { get; set; }
        public Single OdemeYuzde2 { get; set; }
        public decimal OdemeAralik3 { get; set; }
        public Single OdemeYuzde3 { get; set; }
        public decimal OdemeAralik4 { get; set; }
        public Single OdemeYuzde4 { get; set; }
        public decimal OdemeAralik5 { get; set; }
        public Single OdemeYuzde5 { get; set; }
        public decimal OdemeAralik6 { get; set; }
        public Single OdemeYuzde6 { get; set; }
        public decimal OdemeAralik7 { get; set; }
        public Single OdemeYuzde7 { get; set; }
        public decimal OdemeAralik8 { get; set; }
        public Single OdemeYuzde8 { get; set; }
        public short KayitTuru { get; set; }
        public string GuvenlikKod { get; set; }
        public string Kaydeden { get; set; }
        public int KayitTarih { get; set; }
        public int KayitSaat { get; set; }
        public short KayitKaynak { get; set; }
        public string KayitSurum { get; set; }
        public string Degistiren { get; set; }
        public int DegisTarih { get; set; }
        public int DegisSaat { get; set; }
        public short DegisKaynak { get; set; }
        public string DegisSurum { get; set; }
        public short CheckSum { get; set; }
        public string Aciklama { get; set; }
        public string BaglantiKodu { get; set; }
        public string Kod3 { get; set; }
        public string Kod4 { get; set; }
        public string Kod5 { get; set; }
        public string Kod6 { get; set; }
        public string Kod7 { get; set; }
        public string Kod8 { get; set; }
        public string Kod9 { get; set; }
        public string Kod10 { get; set; }
        public decimal BaglantiTutar { get; set; }
        public decimal Kod12 { get; set; }
        public int? DevirTarih { get; set; }
        public decimal? DevirTutar { get; set; }
        public short OnayTip { get; set; }
        public bool? SatisMuduruOnay { get; set; }
        public bool? FinansmanMuduruOnay { get; set; }
        public bool? GenelMudurOnay { get; set; }
        public string OnaylayanSatisMuduru { get; set; }
        public DateTime? OnayTarihSatisMuduru { get; set; }
        public string OnaylayanFinansmanMuduru { get; set; }
        public DateTime? OnayTarihFinansmanMuduru { get; set; }
        public string OnaylayanGenelMudur { get; set; }
        public DateTime? OnayTarihGenelMudur { get; set; }
        public decimal? CekTutari { get; set; }
        public DateTime? CekOrtalamaVadeTarih { get; set; }
        public decimal? NakitTutari { get; set; }
        public string BaglantiParaCinsi { get; set; }
        public bool AktifPasif { get; set; }
        public int? VadeTarihi { get; set; }
        public int? ValorGun { get; set; }
        public int Row_ID { get; set; }
        public byte[] timestamp { get; set; }
        public decimal? Bakiye { get; set; }
        public decimal? ToplamBakiye { get; set; }
        public decimal? KrediLimiti { get; set; }
        public string AlinanBordroOrtalamaVade { get; set; }
        public decimal? AlinanBordro { get; set; }
        public string OrtalamaVade { get; set; }
        public decimal? BekleyenSiparisTutar { get; set; }
    }
    public class BaglantiDetaySelect1
    {
        public string Kod10 { get; set; }
        public string MalKodGrup { get; set; }
        public string MalKod { get; set; }
        public string MalAdi { get; set; }
        public Single Oran1 { get; set; }
        public Single Oran2 { get; set; }
        public Single Oran3 { get; set; }
        public Single Oran4 { get; set; }
        public Single Oran5 { get; set; }
    }
    public class StokOnaySelect
    {
        public bool? Onay { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string MalAdi2 { get; set; }
        public string Birim1 { get; set; }
        public string Birim2 { get; set; }
        public string GrupKod { get; set; }
        public string TipKod { get; set; }
        public string OzelKod { get; set; }
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public string Kod4 { get; set; }
        public string Kod8 { get; set; }
        public double KatSayi2 { get; set; }
        public double KatSayi3 { get; set; }
        public short Operator2 { get; set; }
        public short Operator3 { get; set; }
        public int? AktifPasif { get; set; }
    }
    public class TeminatOnaySelect
    {
        public bool? Onay { get; set; }
        public int ID { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string AltBayi { get; set; }
        public string Cins { get; set; }
        public decimal Tutar { get; set; }
        public bool SureliSuresiz { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime? VadeTarih { get; set; }
        public decimal TeminatTutar { get; set; }
    }
    public class SMSiparisOnaySelect
    {

        public bool? Onay { get; set; }
        public string EvrakNo { get; set; }
        public string Tarih { get; set; }
        public string HesapNo { get; set; }
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public string TeslimYeriKodu { get; set; }
        public string TeslimYeriUnvan { get; set; }
        public decimal? SiparisTutari { get; set; }
        public decimal? Bakiye { get; set; }
        public decimal RiskBakiyesi { get; set; }
        public decimal? ToplamBakiye { get; set; }
        public decimal? SahsiCekLimiti { get; set; }
        public decimal SahsiCekRiski { get; set; }
        public decimal? MusteriCekLimiti { get; set; }
        public decimal MusteriCekRiski { get; set; }
        public decimal BekleyenSiparisTutari { get; set; }
        public decimal Teminat { get; set; }
        public decimal TeminatAltBayi { get; set; }
    }
}
