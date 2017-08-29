using System;

namespace Wms12m.Entity
{
    public class CekOnaySelect
    {
        public int ID { get; set; }
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
        public string OzgurMu { get; set; }
    }
    public class SozlesmeOnaySelect
    {
        public string ListeNo { get; set; }
        public string ListeAdi { get; set; }
        public int BasTarih { get; set; }
        public int BasSaat { get; set; }
        public int BitTarih { get; set; }
        public int YeniBitTarih { get; set; }
        public int BitSaat { get; set; }
        public short MusUygSekli { get; set; }
        public short MusKodGrup { get; set; }
        public string MusteriKod { get; set; }
        public string Unvan { get; set; }
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
        public decimal YeniBaglantiTutar { get; set; }
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
    public class BaglantiDetaySelect
    {
        public string Kod10 { get; set; }
        public string MalKodGrup { get; set; }
        public string MalKod { get; set; }
        public string MalAdi { get; set; }
        public string Kalite { get; set; }
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
        public string KartTip { get; set; }
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
        public decimal DegisimToplamBakiye { get; set; }
        public decimal IslemYapılanCHKBakiye { get; set; }
        public decimal BekleyenSiparisTutari { get; set; }
        public decimal Teminat { get; set; }
        public decimal TeminatAltBayi { get; set; }
    }

    public class KampanyaliSatisRaporu
    {
        public string Chk { get; set; }
        public string EvrakNo { get; set; }
        public string ComfortBirim { get; set; }
        public string ExculusiveBirim { get; set; }
        public string PeliNeroFloorBirim { get; set; }
        public string GoldenBirim { get; set; }
        public string LoftBirim { get; set; }
        public string VintageBirim { get; set; }
        public string WoodBirim { get; set; }
        public string SupurgelikBirim { get; set; }
        public decimal ComfortMiktar { get; set; }
        public decimal ComfortTeslimMiktar { get; set; }
        public decimal ComfortTutar { get; set; }
        public decimal ExculusiveMiktar { get; set; }
        public decimal ExculusiveTeslimMiktar { get; set; }
        public decimal ExculusiveTutar { get; set; }
        public decimal GoldenMiktar { get; set; }
        public decimal GoldenTeslimMiktar { get; set; }
        public decimal GoldenTutar { get; set; }
        public decimal LoftMiktar { get; set; }
        public decimal LoftTeslimMiktar { get; set; }
        public decimal LoftTutar { get; set; }
        public decimal VintageMiktar { get; set; }
        public decimal VintageTeslimMiktar { get; set; }
        public decimal VintageTutar { get; set; }
        public decimal PeliNeroFloorMiktar { get; set; }
        public decimal PeliNeroFloorTeslimMiktar { get; set; }
        public decimal PeliNeroFloorTutar { get; set; }
        public decimal WoodMiktar { get; set; }
        public decimal WoodTeslimMiktar { get; set; }
        public decimal WoodTutar { get; set; }
        public decimal Bedelsiz6CmSupMiktar { get; set; }
        public decimal Bedelsiz6CmSupTeslimMiktar { get; set; }
        public decimal Bedelsiz6CmSupTutar { get; set; }
        public decimal Bedelsiz8CmSupMiktar { get; set; }
        public decimal Bedelsiz8CmSupTeslimMiktar { get; set; }
        public decimal Bedelsiz8CmSupTutar { get; set; }
        public decimal Bedelli6CmSupMiktar { get; set; }
        public decimal Bedelli6CmSupTeslimMiktar { get; set; }
        public decimal Bedelli6CmSupTutar { get; set; }
        public decimal Bedelli8CmSupMiktar { get; set; }
        public decimal Bedelli8CmSupTeslimMiktar { get; set; }
        public decimal Bedelli8CmSupTutar { get; set; }
        public decimal HakKazanilan6cmSup { get; set; }
        public decimal HakKazanilan8cmSup { get; set; }
    }

    public class KampanyaSiparisDetay
    {
        public string Chk { get; set; }
        public string EvrakNo { get; set; }
        public DateTime Tarih { get; set; }
        public decimal Tutar { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal TeslimMiktar { get; set; }
        public string TeslimChk { get; set; }
        public string Durum { get; set; }
        public string GrupKod { get; set; }
        public string Koleksiyon { get; set; }
        public string SupTip { get; set; }
    }

    public class GerceklesenSevkiyatPlani
    {
        public System.Nullable<System.DateTime> Tarih { get; set; }

        public string EvrakNo { get; set; }

        public string Chk { get; set; }

        public string Unvan { get; set; }

        public string SatisTemsilcisi { get; set; }

        public string Malkodu { get; set; }

        public string MalAdi { get; set; }

        public decimal Birimmiktar { get; set; }

        public string Birim { get; set; }

        public System.Nullable<decimal> StokMiktar { get; set; }

        public string StokBirim { get; set; }
    }

    public class SatisBaglatiRapru
    {
        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public System.Nullable<decimal> Bakiye { get; set; }

        public string SozlesmeSiraNo { get; set; }

        public string BaglantiNo { get; set; }

        public string BaglantiTipi { get; set; }

        public System.Nullable<System.DateTime> BaglantiBitisTarihi { get; set; }

        public decimal BaglantiTutari { get; set; }

        public string BaglantiParaCinsi { get; set; }

        public System.Nullable<System.DateTime> BaglantiTarihi { get; set; }

        public string DevirTarih { get; set; }

        public System.Nullable<decimal> DevirTutar { get; set; }

        public System.Nullable<decimal> ToplamSevkEdilenTutar { get; set; }

        public System.Nullable<decimal> DevirdenSonrakiSevkedilen { get; set; }

        public System.Nullable<decimal> KalanTutar { get; set; }
    }

    public class SatisBaglantiHareketleri
    {

        public string SozlesmeNo { get; set; }

        public string EvrakNo { get; set; }

        public string Tarih { get; set; }

        public string KaynakSiparisNo { get; set; }

        public string KaynakSiparisTarih { get; set; }

        public string FytListeNo { get; set; }

        public string Depo { get; set; }

        public string MalKodu { get; set; }

        public decimal Miktar { get; set; }

        public string Birim { get; set; }

        public decimal BirimFiyat { get; set; }

        public decimal Tutar { get; set; }

        public decimal ToplamIskonto { get; set; }

        public float KDVOran { get; set; }

        public decimal KDV { get; set; }

        public System.Nullable<decimal> SevkedilenTutar { get; set; }
    }


    public class ToplamRiskAnaliziRaporu
    {
        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public System.Nullable<decimal> Bakiye { get; set; }

        public decimal BekleyenSiparisTutari { get; set; }

        public decimal SahsiCekRiski { get; set; }

        public System.Nullable<decimal> ToplamSahsiRisk { get; set; }

        public decimal SahsiCekLimiti { get; set; }

        public decimal MusteriCekRiski { get; set; }

        public System.Nullable<decimal> ToplamRisk { get; set; }

        public System.Nullable<decimal> ToplamCekRiski { get; set; }

        public decimal Teminat { get; set; }

        public string SatisTemsilcisi { get; set; }
    }
    public partial class CariCiroRaporuResult
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal? ToplamBorc { get; set; }
        public decimal ToplamAlacak { get; set; }
        public decimal? ToplamBakiye { get; set; }
        public decimal? ToplamBorcOD { get; set; }
        public decimal? ToplamAlacakOD { get; set; }
        public decimal? ToplamBakiyeOD { get; set; }
    }
}
