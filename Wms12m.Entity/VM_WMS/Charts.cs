using System;

namespace Wms12m.Entity
{
    public class BekleyenOnaylar
    {
        public int SiparisOnay_SM { get; set; }
        public int SiparisOnay_SPGMY { get; set; }
        public int SiparisOnay_GM { get; set; }
        public int StokOnay { get; set; }
        public int SozlesmeOnay_SM { get; set; }
        public int SozlesmeOnay_SPGMY { get; set; }
        public int SozlesmeOnay_GM { get; set; }
        public int RiskLimitOnay_SM { get; set; }
        public int RiskLimitOnay_SPGMY { get; set; }
        public int RiskLimitOnay_MIGMY { get; set; }
        public int RiskLimitOnay_GM { get; set; }
        public int TeminatOnay { get; set; }
        public int FiyatOnay_SM { get; set; }
        public int FiyatOnay_SPGMY { get; set; }
        public int FiyatOnay_GM { get; set; }
        public int CekOnay_SPGMY { get; set; }
        public int CekOnay_MIGMY { get; set; }
        public int CekOnay_GM { get; set; }
        public int KurumKartlari { get; set; }
        public int KurumKartlariBuHafta { get; set; }
        public int KurumKartlariBugun { get; set; }
        public int GorusmeNotlari { get; set; }
        public int GorusmeNotlariBuHafta { get; set; }
        public int GorusmeNotlariBugun { get; set; }
        public int TeklifAnalizi { get; set; }
        public int TeklifAnaliziBuHafta { get; set; }
        public int TeklifAnaliziBugun { get; set; }
        public int YeniIhaleAdet { get; set; }
        public int YeniTahsisAdet { get; set; }
        public int NakliyeFiyatAdet { get; set; }
        public int SatinalmaSipTalepGMAdet { get; set; }
        public int TechnoIKOnay_IK { get; set; }
        public int TechnoIKOnay_IKGM { get; set; }
        public int TechnoIKOnay_GM { get; set; }
        public int TechnoIKOnay_SPGMY { get; set; }
        public int TechnoIKOnay_USGMY { get; set; }
        public int TechnoIKOnay_MIGMY { get; set; }
        public int TechnoIKOnay_YKU { get; set; }
        public decimal MevcudBanka { get; set; }
        public decimal MevcudCek { get; set; }
        public decimal MevcudKasa { get; set; }
        public decimal MevcudPOS { get; set; }
    }
    /// <summary>
    /// görev projesi
    /// </summary>
    public class chartGorevCalisma
    {
        public string Proje { get; set; }
        public int Sure { get; set; }
    }
    public class chartGorevProje1
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int Sure { get; set; }
    }
    public class chartGorevProje
    {
        public string Ay { get; set; }
        public int GecenYil { get; set; }
        public int BuYil { get; set; }
    }
    public class chartGorevCalismaAnaliz
    {
        public string Unvan { get; set; }
        public string Proje { get; set; }
        public string Gorev { get; set; }
        public string Aciklama { get; set; }
        public string Kaydeden { get; set; }
        public int Sure { get; set; }
        public DateTime Tarih { get; set; }
        public string GitGuid { get; set; }
    }
    /// <summary>
    /// raporlar
    /// </summary>
    public class RaporVadesiGelmemisCekler
    {
        public string EvrakNo { get; set; }
        public string CHK { get; set; }
        public string Unvan { get; set; }
        public decimal? Tutar { get; set; }
        public string Tarih { get; set; }
        public string VadeTarih { get; set; }
        public string VerildigiYer { get; set; }
        public string VerildigiYerUnvan { get; set; }
        public string CekiDuzenleyen { get; set; }
    }
    public class RaporCekPortfoyListesi
    {
        public string Evrakno { get; set; }
        public string Tarih { get; set; }
        public string CHK { get; set; }
        public string Unvan { get; set; }
        public string CekiDuzenleyen { get; set; }
        public decimal? Tutar { get; set; }
        public string VadeTarih { get; set; }
        public string SonPozisyon { get; set; }
        public string VerildigiYer { get; set; }
        public string VerildigiYerUnvan { get; set; }
    }
    public class RaporCHKSelect
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
    }
    public class RaporTaoSelect
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string TipKod { get; set; }
        public string GrupKod { get; set; }
    }
    public class RaporDepoSelect
    {
        public string Depo { get; set; }
        public string DepoAdi { get; set; }
    }
    public class RaporGetKod
    {
        public string Kod { get; set; }
        public string Tip { get; set; }
    }
    public class RaporGunlukSatis
    {
        public string Chk { get; set; }
        public string Chk2 { get; set; }
        public string Unvan { get; set; }
        public string BaglantiTipi { get; set; }
        public string BaglantiNo { get; set; }
        public string SatisTemsilcisi { get; set; }
        public string TeslimYeriUnvan { get; set; }
        public string KrediLimiti { get; set; }
        public string GrupKod { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Kalite { get; set; }
        public DateTime? IslemTarihi { get; set; }
        public string EvrakNo { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public string AnaBirim { get; set; }
        public double? AnaBirimMiktar { get; set; }
        public decimal AdetCikisMiktar { get; set; }
        public decimal M2CikisMiktar { get; set; }
        public decimal KGCikisMiktar { get; set; }
        public decimal? M3CikisMiktar { get; set; }
        public decimal MTCikisMiktar { get; set; }
        public decimal? NetBirimFiyat { get; set; }
        public decimal? NetTutar { get; set; }
        public decimal KDV { get; set; }
        public decimal? GenelToplam { get; set; }
    }
    public class RaporGunlukSatis2
    {
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public string BaglantiTipi { get; set; }
        public string BaglantiNo { get; set; }
        public string SatisTemsilcisi { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string IslemTarihi { get; set; }
        public string EvrakNo { get; set; }
        public string Birim { get; set; }
        public decimal? BirimMiktar { get; set; }
        public decimal? NetBirimFiyat { get; set; }
        public decimal? NetTutar { get; set; }
        public decimal? KDV { get; set; }
        public decimal? GenelToplam { get; set; }
    }
    public class RaporOdemeYapmayanMusteriler
    {
        public string HesapKodu { get; set; }
        public string Unvan1 { get; set; }
        public decimal? Bakiye { get; set; }
        public decimal? SonTahsilatTutari { get; set; }
        public string SonTahsilatTarihi { get; set; }
    }
    public class RaporPozisyonCekSenet
    {
        public int ID { get; set; }
        public string ITEMNAME { get; set; }
    }
    public class RaporSatilmayanUrunler
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Birim { get; set; }
        public decimal? StokMiktar { get; set; }
        public decimal KritikStok { get; set; }
        public string SonFaturaNo { get; set; }
        public string SonFaturaTarihi { get; set; }
        public string SonUretimTarihi { get; set; }
        public decimal SonSatisMiktari { get; set; }
        public string SonSatisMiktariBirim { get; set; }
    }
    public class RaporStok
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string DepoKodu { get; set; }
        public string DepoAdi { get; set; }
        public decimal? Birim1StokMiktar { get; set; }
        public string Birim1 { get; set; }
        public double? Birim2StokMiktar { get; set; }
        public string Birim2 { get; set; }
        public string SatMalKodu { get; set; }
        public string StokAdresi { get; set; }
        public string OnayliTedarikci { get; set; }
        public string OnayliTedarikciUnvan { get; set; }
    }
    public class RaporStokKodCase
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string GrupKod { get; set; }
        public string TipKod { get; set; }
        public string OzelKod { get; set; }
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public decimal GirMiktar { get; set; }
        public decimal CikMiktar { get; set; }
        public decimal StokMiktar { get; set; }
        public string Birim1 { get; set; }
        public decimal? StokMiktarBirim2 { get; set; }
        public string Birim2 { get; set; }
        public decimal? StokMiktarBirim3 { get; set; }
        public string Birim3 { get; set; }
    }
    public class RaporToplamRiskBakiyesi
    {
        public string HesapKodu { get; set; }
        public string Ünvan { get; set; }
        public decimal? Borç { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Bakiye { get; set; }
        public decimal? ToplamBakiye { get; set; }
    }
    public class RaporBakiye
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal Borc { get; set; }
        public decimal Alacak { get; set; }
        public decimal Bakiye { get; set; }
    }
    public class RaporBekleyenSiparis
    {
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public DateTime? Tarih { get; set; }
        public string EvrakNo { get; set; }
        public string BaglantiNo { get; set; }
        public string GrupKod { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal AnaMiktar { get; set; }
        public string AnaBirim { get; set; }
        public decimal SevkedilenMiktar { get; set; }
        public decimal? KalanMiktar { get; set; }
        public string KalanBirim { get; set; }
        public double? KalanAnaMiktar { get; set; }
        public string KalanAnaBirim { get; set; }
        public string BaglantiTipi { get; set; }
        public string FiyatListeNo { get; set; }
        public decimal? FYTFiyat { get; set; }
        public string FYTDovizCinsi { get; set; }
        public string SozlesmeSartlari { get; set; }
        public decimal? NetFiyat { get; set; }
        public decimal? NetFiyat2 { get; set; }
        public decimal Tutar { get; set; }
        public decimal ToplamIskonto { get; set; }
        public decimal KDV { get; set; }
        public decimal? KalanBirimMiktar { get; set; }
        public decimal? KDVSizTutar { get; set; }
        public decimal? KDVliTutar { get; set; }
        public decimal? KDVSizKalanTutar { get; set; }
        public decimal? KDVliKalanTutar { get; set; }
        public double? StokMiktar { get; set; }
        public string StokBirim { get; set; }
        public decimal? StokAnaMiktar { get; set; }
        public string StokAnaBirim { get; set; }
        public DateTime? TeslimTarih { get; set; }
        public string TeslimTarihDurum { get; set; }
        public string SatisTemsilcisi { get; set; }
    }
    public class RaporBekleyenSiparis2
    {
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public string Tarih { get; set; }
        public string EvrakNo { get; set; }
        public string GrupKod { get; set; }
        public string BaglantiNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? AnaMiktar { get; set; }
        public string Birim { get; set; }
        public string AnaBirim { get; set; }
        public decimal? SevkedilenMiktar { get; set; }
        public decimal? KalanMiktar { get; set; }
        public string KalanBirim { get; set; }
        public decimal? KalanAnaMiktar { get; set; }
        public string KalanAnaBirim { get; set; }
        public decimal? NetFiyat2 { get; set; }
        public decimal? Tutar { get; set; }
        public decimal? ToplamIskonto { get; set; }
        public decimal? KDV { get; set; }
        public decimal? KalanBirimMiktar { get; set; }
        public decimal? KDVSizTutar { get; set; }
        public decimal? KDVliTutar { get; set; }
        public decimal? KDVSizKalanTutar { get; set; }
        public decimal? KDVliKalanTutar { get; set; }
        public decimal? StokMiktar { get; set; }
        public string StokBirim { get; set; }
        public decimal? StokAnaMiktar { get; set; }
        public string StokAnaBirim { get; set; }
        public string TeslimTarih { get; set; }
        public string TeslimTarihDurum { get; set; }
        public string SatisTemsilcisi { get; set; }
        public string BaglantiTipi { get; set; }
        public string FiyatListeNo { get; set; }
        public string FYTDovizCinsi { get; set; }
    }
    public class RaporCekListesi
    {
        public string EvrakNo { get; set; }
        public DateTime? Tarih { get; set; }
        public string Chk { get; set; }
        public string Unvan1 { get; set; }
        public decimal Tutar { get; set; }
        public DateTime? VadeTarih { get; set; }
        public DateTime? KayitTarih { get; set; }
        public string Pozisyon { get; set; }
        public string BorcluUnvan1 { get; set; }
        public string SonPozisyon { get; set; }
        public string VerildigiYer { get; set; }
        public string VerildigiYerUnvan { get; set; }
    }
    public class RaporCekListesi1
    {
        public string EvrakNo { get; set; }
        public string Tarih { get; set; }
        public string Chk { get; set; }
        public string Unvan1 { get; set; }
        public decimal Tutar { get; set; }
        public string VadeTarih { get; set; }
        public string KayitTarih { get; set; }
        public string Pozisyon { get; set; }
        public string BorcluUnvan1 { get; set; }
        public string SonPozisyon { get; set; }
        public string VerildigiYer { get; set; }
        public string VerildigiYerUnvan { get; set; }
    }
    public class RaporCariEkstre
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public DateTime? Tarih { get; set; }
        public string IslemTip { get; set; }
        public DateTime? VadeTarih { get; set; }
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? BorcBakiye { get; set; }
        public decimal? AlacakBakiye { get; set; }
    }


    public class RP_TahsilatKontrol
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Allow Null) </summary>
        public string Unvan { get; set; }
        /// <summary> Decimal(36,6) (Allow Null) </summary>
        public decimal? Bakiye { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string SozlesmeSiraNo { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string BaglantiNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string BaglantiTipi { get; set; }
        /// <summary> Datetime (Allow Null) </summary>
        public DateTime? BaglantiBitisTarihi { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BaglantiTutari { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string BaglantiParaCinsi { get; set; }
        /// <summary> Datetime (Allow Null) </summary>
        public DateTime? BaglantiTarihi { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string DevirTarih { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DevirTutar { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal ToplamSevkEdilenTutar { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal DevirdenSonrakiSevkedilen { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal KalanTutar { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal BekleyenIrsaliye { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal BekleyenSiparis { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal CekTahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal NakitTahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal KKTahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal Havaletahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal ToplamTahsilat { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? FaturaBekleyenler { get; set; }
    }



    public class RaporCariEkstreCek
    {
        public string BorcluUnvan { get; set; }
        public string Tutar { get; set; }
        public string VadeTarihi { get; set; }
        public string Sehir { get; set; }
        public string Banka { get; set; }
        public string Sube { get; set; }
        public string CekNo { get; set; }
    }
    public class RaporCariEkstreFatura
    {
        public string EvrakNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Miktar { get; set; }
        public string Birim { get; set; }
        public string BirimFiyat { get; set; }
        public string Tutar { get; set; }
        public string IskontoOran1 { get; set; }
        public string IskontoOran2 { get; set; }
        public string IskontoOran3 { get; set; }
        public string IskontoOran4 { get; set; }
        public string IskontoOran5 { get; set; }
        public string ToplamIskonto { get; set; }
        public string NetTutar { get; set; }
    }
    public class RaporCariEkstreDiger
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string EvrakNo { get; set; }
        public string KarsiHesapKodu { get; set; }
        public string KarsiEvrakNo { get; set; }
        public string Aciklama { get; set; }
        public string Tutar { get; set; }
        public string VadeTarihi { get; set; }
    }
    public class RaporCariEkstre1
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public string Tarih { get; set; }
        public string IslemTip { get; set; }
        public string VadeTarih { get; set; }
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? BorcBakiye { get; set; }
        public decimal? AlacakBakiye { get; set; }
        public string Borc2 { get; set; }
        public string Alacak2 { get; set; }
        public string BorcBakiye2 { get; set; }
        public string AlacakBakiye2 { get; set; }
    }
    public class GMSiparisOnaySelect
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
    /// <summary>
    /// charts
    /// </summary>
    public class ChartUrunGrubuSatisAnalizi
    {
        public string GrupKod { get; set; }
        public decimal Yil2016 { get; set; }
        public decimal Yil2017 { get; set; }
        public decimal Toplam { get; set; }
    }
    public class ChartGunlukSatisAnalizi
    {
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public decimal? GenelTutar { get; set; }
    }
    public class ChartBekleyenSiparisUrunGrubu
    {
        public string GrupKod { get; set; }
        public decimal? KalanMiktar { get; set; }
    }
    public class ChartSatisTemsilcisiAylikSatisAnalizi
    {
        public string Kod7 { get; set; }
        public string Aciklama { get; set; }
        public decimal? GenelTutar { get; set; }
    }
    public class ChartBakiyeRiskAnalizi
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal Borc { get; set; }
        public decimal Alacak { get; set; }
        public decimal Bakiye { get; set; }
    }
    public class ChartAylikSatisAnalizi
    {
        public string Ay { get; set; }
        public decimal? Yil2015 { get; set; }
        public decimal? Yil2016 { get; set; }
        public decimal? Yil2017 { get; set; }
    }
    public class ChartBaglantiUrunGrup
    {
        public string MalKod { get; set; }
        public decimal? KalanTutar { get; set; }
    }
    public class ChartGunlukMDFUretimi
    {
        public int? GrupID { get; set; }
        public string Mamul { get; set; }
        public string MalKodu { get; set; }
        public decimal? Birim1Miktar { get; set; }
        public decimal? Birim2Miktar { get; set; }
    }
    public class ChartBaglantiZaman
    {
        public DateTime? Tarih { get; set; }
        public decimal? KalanTutar { get; set; }
    }
    public class ChartBolgeBazliSatisAnalizi
    {
        public string Ay { get; set; }
        public string Lokasyon { get; set; }
        public decimal GenelTutar { get; set; }
    }
    public class ChartBolgeBazliSatisAnaliziKriter
    {
        public string Kriter { get; set; }
        public string Flag { get; set; }
    }
}
