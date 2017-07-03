using System;

namespace Wms12m.Entity
{
    public class RaporVadesiGelmemisCekler
    {
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string CHK { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Tutar { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string VadeTarih { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string VerildigiYer { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string VerildigiYerUnvan { get; set; }
        /// <summary> VarChar(82) (Not Null) </summary>
        public string CekiDuzenleyen { get; set; }
    }
    public class RaporCekPortfoyListesi
    {
        /// <summary> VarChar(16) (Allow Null) </summary>
        public string Evrakno { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(20) (Allow Null) </summary>
        public string CHK { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(81) (Allow Null) </summary>
        public string CekiDuzenleyen { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Tutar { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string VadeTarih { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string SonPozisyon { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string VerildigiYer { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string VerildigiYerUnvan { get; set; }
    }
    public class RaporCHKSelect
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
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
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string BaglantiTipi { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string BaglantiNo { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string SatisTemsilcisi { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string IslemTarihi { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? BirimMiktar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? NetBirimFiyat { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? NetTutar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KDV { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? GenelToplam { get; set; }
    }
    public class RaporOdemeYapmayanMusteriler
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(40) (Not Null) </summary>
        public string Unvan1 { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Bakiye { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? SonTahsilatTutari { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
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
        public Nullable<decimal> StokMiktar { get; set; }
        public decimal KritikStok { get; set; }
        public string SonFaturaNo { get; set; }
        public string SonFaturaTarihi { get; set; }
        public decimal SonSatisMiktari { get; set; }
        public string SonSatisMiktariBirim { get; set; }
    }
    public class RaporStok
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string DepoKodu { get; set; }
        public string DepoAdi { get; set; }
        public Nullable<decimal> Birim1StokMiktar { get; set; }
        public string Birim1 { get; set; }
        public Nullable<double> Birim2StokMiktar { get; set; }
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
        public Nullable<decimal> StokMiktarBirim2 { get; set; }
        public string Birim2 { get; set; }
        public Nullable<decimal> StokMiktarBirim3 { get; set; }
        public string Birim3 { get; set; }
    }
    public class RaporToplamRiskBakiyesi
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Ünvan { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Borç { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Alacak { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Bakiye { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
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
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string BaglantiNo { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Miktar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? AnaMiktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string AnaBirim { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? SevkedilenMiktar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KalanMiktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string KalanBirim { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KalanAnaMiktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string KalanAnaBirim { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? NetFiyat2 { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? Tutar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? ToplamIskonto { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KDV { get; set; }
        /// <summary> Decimal(27,6) (Allow Null) </summary>
        public decimal? KalanBirimMiktar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KDVSizTutar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KDVliTutar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KDVSizKalanTutar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? KDVliKalanTutar { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? StokMiktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string StokBirim { get; set; }
        /// <summary> Decimal(36,2) (Allow Null) </summary>
        public decimal? StokAnaMiktar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string StokAnaBirim { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string TeslimTarih { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TeslimTarihDurum { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string SatisTemsilcisi { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string BaglantiTipi { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string FiyatListeNo { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string FYTDovizCinsi { get; set; }
    }
    public class RaporCekListesi
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
        public string Tarih { get; set; }
        public string IslemTip { get; set; }
        public string VadeTarih { get; set; }
        public Nullable<decimal> Borc { get; set; }
        public Nullable<decimal> Alacak { get; set; }
        public Nullable<decimal> BorcBakiye { get; set; }
        public Nullable<decimal> AlacakBakiye { get; set; }
        public string Borc2 { get; set; }
        public string Alacak2 { get; set; }
        public string BorcBakiye2 { get; set; }
        public string AlacakBakiye2 { get; set; }
    }
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
        public Nullable<decimal> GenelTutar { get; set; }
    }
    public class ChartBekleyenSiparisUrunGrubu
    {
        public string GrupKod { get; set; }
        public Nullable<decimal> KalanMiktar { get; set; }
    }
    public class ChartSatisTemsilcisiAylikSatisAnalizi
    {
        public string Kod7 { get; set; }
        public string Aciklama { get; set; }
        public Nullable<decimal> GenelTutar { get; set; }
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
        public Nullable<decimal> Yil2015 { get; set; }
        public Nullable<decimal> Yil2016 { get; set; }
        public Nullable<decimal> Yil2017 { get; set; }
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
    public class ChartBaglantiUrunGrup
    {
        public string MalKod { get; set; }
        public Nullable<decimal> KalanTutar { get; set; }
    }
    public class ChartGunlukMDFUretimi
    {
        public Nullable<int> GrupID { get; set; }

        public string Mamul { get; set; }

        public string MalKodu { get; set; }

        public Nullable<decimal> Birim1Miktar { get; set; }

        public Nullable<decimal> Birim2Miktar { get; set; }
    }
    public class ChartBaglantiZaman
    {
        public Nullable<System.DateTime> Tarih { get; set; }

        public Nullable<decimal> KalanTutar { get; set; }
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
