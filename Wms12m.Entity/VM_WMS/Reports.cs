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
        public float Oran { get; set; }
        public float Oran1 { get; set; }
        public float Oran2 { get; set; }
        public float Oran3 { get; set; }
        public float Oran4 { get; set; }
        public float Oran5 { get; set; }
        public decimal MikAralik1 { get; set; }
        public float MikYuzde1 { get; set; }
        public decimal MikAralik2 { get; set; }
        public float MikYuzde2 { get; set; }
        public decimal MikAralik3 { get; set; }
        public float MikYuzde3 { get; set; }
        public decimal MikAralik4 { get; set; }
        public float MikYuzde4 { get; set; }
        public decimal MikAralik5 { get; set; }
        public float MikYuzde5 { get; set; }
        public decimal MikAralik6 { get; set; }
        public float MikYuzde6 { get; set; }
        public decimal MikAralik7 { get; set; }
        public float MikYuzde7 { get; set; }
        public decimal MikAralik8 { get; set; }
        public float MikYuzde8 { get; set; }
        public decimal TutarAralik1 { get; set; }
        public float TutarYuzde1 { get; set; }
        public decimal TutarAralik2 { get; set; }
        public float TutarYuzde2 { get; set; }
        public decimal TutarAralik3 { get; set; }
        public float TutarYuzde3 { get; set; }
        public decimal TutarAralik4 { get; set; }
        public float TutarYuzde4 { get; set; }
        public decimal TutarAralik5 { get; set; }
        public float TutarYuzde5 { get; set; }
        public decimal TutarAralik6 { get; set; }
        public float TutarYuzde6 { get; set; }
        public decimal TutarAralik7 { get; set; }
        public float TutarYuzde7 { get; set; }
        public decimal TutarAralik8 { get; set; }
        public float TutarYuzde8 { get; set; }
        public decimal OdemeAralik1 { get; set; }
        public float OdemeYuzde1 { get; set; }
        public decimal OdemeAralik2 { get; set; }
        public float OdemeYuzde2 { get; set; }
        public decimal OdemeAralik3 { get; set; }
        public float OdemeYuzde3 { get; set; }
        public decimal OdemeAralik4 { get; set; }
        public float OdemeYuzde4 { get; set; }
        public decimal OdemeAralik5 { get; set; }
        public float OdemeYuzde5 { get; set; }
        public decimal OdemeAralik6 { get; set; }
        public float OdemeYuzde6 { get; set; }
        public decimal OdemeAralik7 { get; set; }
        public float OdemeYuzde7 { get; set; }
        public decimal OdemeAralik8 { get; set; }
        public float OdemeYuzde8 { get; set; }
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
        public float Oran1 { get; set; }
        public float Oran2 { get; set; }
        public float Oran3 { get; set; }
        public float Oran4 { get; set; }
        public float Oran5 { get; set; }
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
    public class SiparisOnaySelect
    {
        public string BaglantiNo { get; set; }
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

    public class CariHesapOnaySelect
    {
        public bool? Onay { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan1 { get; set; }
        public string Unvan2 { get; set; }
        public string FaturaAdres1 { get; set; }
        public string FaturaAdres2 { get; set; }
        public string FaturaAdres3 { get; set; }
        public string VergiDairesi { get; set; }
        public string HesapNo { get; set; }
        public Int16 OpsiyonGunu { get; set; }
        public string BolgeKod { get; set; }
        public string OzelKod { get; set; }
        public string GrupKod { get; set; }
        public string TipKod { get; set; }
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public string Kod4 { get; set; }
        public decimal Kod5 { get; set; }
        public decimal Kod6 { get; set; }
        public string Kod7 { get; set; }
        public string Kod8 { get; set; }
        public string Kod9 { get; set; }
        public Int16 Kod10 { get; set; }
        public Int16 Kod11 { get; set; }
        public decimal Kod12 { get; set; }
        public decimal Kod13 { get; set; }
        public string Kod14 { get; set; }
        public string Kod15 { get; set; }
        public string Kod16 { get; set; }
        public string Kod17 { get; set; }
        public string Kod18 { get; set; }
        public string MhsKod { get; set; }
        public decimal KrediLimiti { get; set; }
        public Int16 EFatKullanici { get; set; }
        public Int16 EFatSenaryo { get; set; }
        public string EFatEtiket { get; set; }
        public string Ad { get; set; }
        public string SoyAd { get; set; }
    }
    public class MuhasebeOnaySelect
    {
        public string YeniHesapKod { get; set; }
        public string YeniHesapAd { get; set; }
        public string EskiHesapKod { get; set; }
        public string EskiHesapAd { get; set; }
    }


    public class KampanyaliSatisRaporu
    {
        public string Chk { get; set; }
        public string Unvan { get; set; }
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
        public string Unvan { get; set; }
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
        public DateTime? Tarih { get; set; }
        public string EvrakNo { get; set; }
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public string SatisTemsilcisi { get; set; }
        public string Malkodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Birimmiktar { get; set; }
        public string Birim { get; set; }
        public decimal? StokMiktar { get; set; }
        public string StokBirim { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }
    }

    public class SatisBaglatiRapru
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal? Bakiye { get; set; }
        public string SozlesmeSiraNo { get; set; }
        public string BaglantiNo { get; set; }
        public string BaglantiTipi { get; set; }
        public DateTime? BaglantiBitisTarihi { get; set; }
        public decimal BaglantiTutari { get; set; }
        public string BaglantiParaCinsi { get; set; }
        public DateTime? BaglantiTarihi { get; set; }
        public string DevirTarih { get; set; }
        public decimal? DevirTutar { get; set; }
        public decimal? ToplamSevkEdilenTutar { get; set; }
        public decimal? DevirdenSonrakiSevkedilen { get; set; }
        public decimal? KalanTutar { get; set; }
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
        public decimal? SevkedilenTutar { get; set; }
    }
    public class TechnoList
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string IsyeriAdi { get; set; }
        public string BirimAdi { get; set; }
        public string PozisyonAdi { get; set; }
        public string GecerlilikAyi { get; set; }
        public short GecerlilikYili { get; set; }
        public DateTime DegisimTarihi { get; set; }
        public string UcretTipi { get; set; }
        public string DovizTipi { get; set; }
        public decimal BirimUcret { get; set; }
        public decimal GenelAylikUcret { get; set; }
        public DateTime KayitTarih { get; set; }
        public string KaydedenKullanici { get; set; }
        public DateTime GuncellemeTarihi { get; set; }
        public string GuncelleyenKullanici { get; set; }
        public string Aciklama { get; set; }
        public decimal TutarIsci { get; set; }
        public decimal TutarMemur { get; set; }
        public string DovizTipIsci { get; set; }
        public string DovizTipMemur { get; set; }
        public string OdenekKodu { get; set; }
        public byte Ocak { get; set; }
        public byte Subat { get; set; }
        public byte Mart { get; set; }
        public byte Nisan { get; set; }
        public byte Mayis { get; set; }
        public byte Haziran { get; set; }
        public byte Temmuz { get; set; }
        public byte Agustos { get; set; }
        public byte Eylul { get; set; }
        public byte Ekim { get; set; }
        public byte Kasim { get; set; }
        public byte Aralik { get; set; }
        public short Yil { get; set; }
        public int PERSONELID { get; set; }
        public string BrutNet { get; set; }
        public int ID { get; set; }
        public string RedNedeni { get; set; }
        public string Reddeden { get; set; }
        public int DSKALAID { get; set; }
        public int DBUTUCRETID { get; set; }
        public short IslemTipi { get; set; }
        public int DSKALAANAID { get; set; }
        public int DBIRIMID { get; set; }
        public string OnayindaBekleyen { get; set; }
    }

    public class ToplamRiskAnaliziRaporu
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal? Bakiye { get; set; }
        public decimal BekleyenSiparisTutari { get; set; }
        public decimal SahsiCekRiski { get; set; }
        public decimal? ToplamSahsiRisk { get; set; }
        public decimal SahsiCekLimiti { get; set; }
        public decimal MusteriCekRiski { get; set; }
        public decimal? ToplamRisk { get; set; }
        public decimal? ToplamCekRiski { get; set; }
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