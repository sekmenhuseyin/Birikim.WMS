using System;

namespace Wms12m.Entity
{
    public class RaporVadesiGelmemisCekler
    {
        public string EvrakNo { get; set; }
        public string CHK { get; set; }
        public string Unvan { get; set; }
        public string Tutar { get; set; }
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
        public string Tutar { get; set; }
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
        public string Unvan { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string IslemTarihi { get; set; }
        public string EvrakNo { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public string NetBirimFiyat { get; set; }
        public string NetTutar { get; set; }
        public string KDV { get; set; }
        public string GenelToplam { get; set; }
    }
    public class RaporOdemeYapmayanMusteriler
    {
        public string HesapKodu { get; set; }
        public string Unvan1 { get; set; }
        public string Bakiye { get; set; }
        public string SonTahsilatTutari { get; set; }
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
        public string HesapKodu { get; set; }
        public string Ünvan { get; set; }
        public string Borç { get; set; }
        public string Alacak { get; set; }
        public string Bakiye { get; set; }
        public string ToplamBakiye { get; set; }
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
        public string Tarih { get; set; }
        public string EvrakNo { get; set; }
        public string GrupKod { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public decimal AnaMiktar { get; set; }
        public string Birim { get; set; }
        public string AnaBirim { get; set; }
        public decimal SevkedilenMiktar { get; set; }
        public Nullable<decimal> KalanMiktar { get; set; }
        public string KalanBirim { get; set; }
        public Nullable<double> KalanAnaMiktar { get; set; }
        public string KalanAnaBirim { get; set; }
        public string NetFiyat2 { get; set; }
        public string Tutar { get; set; }
        public string ToplamIskonto { get; set; }
        public string KDV { get; set; }
        public Nullable<decimal> KalanBirimMiktar { get; set; }
        public string KDVSizTutar { get; set; }
        public string KDVliTutar { get; set; }
        public Nullable<double> StokMiktar { get; set; }
        public string StokBirim { get; set; }
        public Nullable<decimal> StokAnaMiktar { get; set; }
        public string StokAnaBirim { get; set; }
        public string TeslimTarih { get; set; }
        public string TeslimTarihDurum { get; set; }
        public string SatisTemsilcisi { get; set; }
    }
    public class RaporCekListesi
    {
        public string EvrakNo { get; set; }
        public string Tarih { get; set; }
        public string Chk { get; set; }
        public string Unvan1 { get; set; }
        public string Tutar { get; set; }
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
}
