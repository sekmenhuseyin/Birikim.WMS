using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{
   public class KKP_CHK
   {
       #region CHK Column Names
       /// <summary>
       /// 0-Müşteri, 1-Satıcı, 2-Banka, 3-Kasa, 4-Diğer
       /// </summary>
       [DbAlan("KartTip", SqlDbType.SmallInt, 2)]
       public short KartTip { get; set; }

       [DbAlan("HesapKodu", SqlDbType.VarChar, 20, false, true, false)]
       public string HesapKodu { get; set; }

       [DbAlan("Unvan1", SqlDbType.VarChar, 40)]
       public string Unvan1 { get; set; }

       [DbAlan("Unvan2", SqlDbType.VarChar, 40)]
       public string Unvan2 { get; set; }

       [DbAlan("Yetkili1", SqlDbType.VarChar, 30)]
       public string Yetkili1 { get; set; }

       [DbAlan("Yetkili2", SqlDbType.VarChar, 30)]
       public string Yetkili2 { get; set; }

       [DbAlan("FaturaAdres1", SqlDbType.VarChar, 40)]
       public string FaturaAdres1 { get; set; }

       [DbAlan("FaturaAdres2", SqlDbType.VarChar, 40)]
       public string FaturaAdres2 { get; set; }

       [DbAlan("FaturaAdres3", SqlDbType.VarChar, 40)]
       public string FaturaAdres3 { get; set; }

       [DbAlan("TeslimAdres1", SqlDbType.VarChar, 40)]
       public string TeslimAdres1 { get; set; }

       [DbAlan("TeslimAdres2", SqlDbType.VarChar, 40)]
       public string TeslimAdres2 { get; set; }

       [DbAlan("TeslimAdres3", SqlDbType.VarChar, 40)]
       public string TeslimAdres3 { get; set; }

       [DbAlan("VergiDairesi", SqlDbType.VarChar, 6)]
       public string VergiDairesi { get; set; }

       [DbAlan("HesapNo", SqlDbType.VarChar, 20)]
       public string HesapNo { get; set; }

       [DbAlan("Telefon1", SqlDbType.VarChar, 7)]
       public string Telefon1 { get; set; }

       [DbAlan("Telefon2", SqlDbType.VarChar, 7)]
       public string Telefon2 { get; set; }

       [DbAlan("Telefon3", SqlDbType.VarChar, 7)]
       public string Telefon3 { get; set; }

       [DbAlan("Telefon4", SqlDbType.VarChar, 7)]
       public string Telefon4 { get; set; }

       [DbAlan("Fax1", SqlDbType.VarChar, 7)]
       public string Fax1 { get; set; }

       [DbAlan("Fax2", SqlDbType.VarChar, 7)]
       public string Fax2 { get; set; }

       [DbAlan("MusterekHesap", SqlDbType.VarChar, 20)]
       public string MusterekHesap { get; set; }

       [DbAlan("MutabakatTarih", SqlDbType.Int, 4)]
       public int MutabakatTarih { get; set; }

       [DbAlan("IskontoOran", SqlDbType.Real, 4)]
       public double IskontoOran { get; set; }

       [DbAlan("OpsiyonGunu", SqlDbType.SmallInt, 2)]
       public short OpsiyonGunu { get; set; }

       [DbAlan("OdemeGunu", SqlDbType.SmallInt, 2)]
       public short OdemeGunu { get; set; }

       [DbAlan("DovizCinsi", SqlDbType.VarChar, 3)]
       public string DovizCinsi { get; set; }

       [DbAlan("BolgeKod", SqlDbType.VarChar, 4)]
       public string BolgeKod { get; set; }

       [DbAlan("OzelKod", SqlDbType.VarChar, 20)]
       public string OzelKod { get; set; }

       [DbAlan("GrupKod", SqlDbType.VarChar, 20)]
       public string GrupKod { get; set; }

       [DbAlan("TipKod", SqlDbType.VarChar, 20)]
       public string TipKod { get; set; }

       [DbAlan("Kod1", SqlDbType.VarChar, 20)]
       public string Kod1 { get; set; }

       [DbAlan("Kod2", SqlDbType.VarChar, 20)]
       public string Kod2 { get; set; }

       [DbAlan("Kod3", SqlDbType.VarChar, 20)]
       public string Kod3 { get; set; }

       [DbAlan("Kod4", SqlDbType.VarChar, 20)]
       public string Kod4 { get; set; }

       [DbAlan("Kod5", SqlDbType.Decimal, 13)]
       public decimal Kod5 { get; set; }

       [DbAlan("Kod6", SqlDbType.Decimal, 13)]
       public decimal Kod6 { get; set; }

       [DbAlan("Kod7", SqlDbType.VarChar, 20)]
       public string Kod7 { get; set; }

       [DbAlan("KOD8", SqlDbType.VarChar, 20)]
       public string KOD8 { get; set; }

       [DbAlan("Kod9", SqlDbType.VarChar, 20)]
       public string Kod9 { get; set; }

       [DbAlan("Kod10", SqlDbType.SmallInt, 2)]
       public short Kod10 { get; set; }

       [DbAlan("Kod11", SqlDbType.SmallInt, 2)]
       public short Kod11 { get; set; }

       [DbAlan("Kod12", SqlDbType.Decimal, 13)]
       public decimal Kod12 { get; set; }

       [DbAlan("Kod13", SqlDbType.Decimal, 13)]
       public decimal Kod13 { get; set; }

       [DbAlan("MhsKod", SqlDbType.VarChar, 50)]
       public string MhsKod { get; set; }

       [DbAlan("MasrafMerkezi", SqlDbType.VarChar, 20)]
       public string MasrafMerkezi { get; set; }

       [DbAlan("KulSatisFiyat", SqlDbType.SmallInt, 2)]
       public short KulSatisFiyat { get; set; }

       [DbAlan("DvrTarih", SqlDbType.Int, 4)]
       public int DvrTarih { get; set; }

       [DbAlan("BorcTarih", SqlDbType.Int, 4)]
       public int BorcTarih { get; set; }

       [DbAlan("AlacakTarih", SqlDbType.Int, 4)]
       public int AlacakTarih { get; set; }

       [DbAlan("RiskBorcTarih", SqlDbType.Int, 4)]
       public int RiskBorcTarih { get; set; }

       [DbAlan("RiskAlacakTarih", SqlDbType.Int, 4)]
       public int RiskAlacakTarih { get; set; }

       [DbAlan("DvrSntRiskB", SqlDbType.Decimal, 13)]
       public decimal DvrSntRiskB { get; set; }

       [DbAlan("DvrSntRiskA", SqlDbType.Decimal, 13)]
       public decimal DvrSntRiskA { get; set; }

       [DbAlan("DvrCekRiskB", SqlDbType.Decimal, 13)]
       public decimal DvrCekRiskB { get; set; }

       [DbAlan("DvrCekRiskA", SqlDbType.Decimal, 13)]
       public decimal DvrCekRiskA { get; set; }

       [DbAlan("SntRiskB", SqlDbType.Decimal, 13)]
       public decimal SntRiskB { get; set; }

       [DbAlan("SntRiskA", SqlDbType.Decimal, 13)]
       public decimal SntRiskA { get; set; }

       [DbAlan("CekRiskB", SqlDbType.Decimal, 13)]
       public decimal CekRiskB { get; set; }

       [DbAlan("CekRiskA", SqlDbType.Decimal, 13)]
       public decimal CekRiskA { get; set; }

       [DbAlan("DvrTeminat1B", SqlDbType.Decimal, 13)]
       public decimal DvrTeminat1B { get; set; }

       [DbAlan("DvrTeminat1A", SqlDbType.Decimal, 13)]
       public decimal DvrTeminat1A { get; set; }

       [DbAlan("Teminat1B", SqlDbType.Decimal, 13)]
       public decimal Teminat1B { get; set; }

       [DbAlan("Teminat1A", SqlDbType.Decimal, 13)]
       public decimal Teminat1A { get; set; }

       [DbAlan("DvrTeminat2B", SqlDbType.Decimal, 13)]
       public decimal DvrTeminat2B { get; set; }

       [DbAlan("DvrTeminat2A", SqlDbType.Decimal, 13)]
       public decimal DvrTeminat2A { get; set; }

       [DbAlan("Teminat2B", SqlDbType.Decimal, 13)]
       public decimal Teminat2B { get; set; }

       [DbAlan("Teminat2A", SqlDbType.Decimal, 13)]
       public decimal Teminat2A { get; set; }

       [DbAlan("DvrB", SqlDbType.Decimal, 13)]
       public decimal DvrB { get; set; }

       [DbAlan("DvrA", SqlDbType.Decimal, 13)]
       public decimal DvrA { get; set; }

       [DbAlan("OdemeB", SqlDbType.Decimal, 13)]
       public decimal OdemeB { get; set; }

       [DbAlan("OdemeA", SqlDbType.Decimal, 13)]
       public decimal OdemeA { get; set; }

       [DbAlan("CiroB", SqlDbType.Decimal, 13)]
       public decimal CiroB { get; set; }

       [DbAlan("CiroA", SqlDbType.Decimal, 13)]
       public decimal CiroA { get; set; }

       [DbAlan("IadeFatB", SqlDbType.Decimal, 13)]
       public decimal IadeFatB { get; set; }

       [DbAlan("IadeFatA", SqlDbType.Decimal, 13)]
       public decimal IadeFatA { get; set; }

       [DbAlan("KDVB", SqlDbType.Decimal, 13)]
       public decimal KDVB { get; set; }

       [DbAlan("KDVA", SqlDbType.Decimal, 13)]
       public decimal KDVA { get; set; }

       [DbAlan("DigerB", SqlDbType.Decimal, 13)]
       public decimal DigerB { get; set; }

       [DbAlan("DigerA", SqlDbType.Decimal, 13)]
       public decimal DigerA { get; set; }

       [DbAlan("DvrProtSnt", SqlDbType.Decimal, 13)]
       public decimal DvrProtSnt { get; set; }

       [DbAlan("ProtSnt", SqlDbType.Decimal, 13)]
       public decimal ProtSnt { get; set; }

       [DbAlan("DvrKarsCek", SqlDbType.Decimal, 13)]
       public decimal DvrKarsCek { get; set; }

       [DbAlan("KarsCek", SqlDbType.Decimal, 13)]
       public decimal KarsCek { get; set; }

       [DbAlan("KrediLimiti", SqlDbType.Decimal, 13)]
       public decimal KrediLimiti { get; set; }

       [DbAlan("DvzKrediLimiti", SqlDbType.Decimal, 13)]
       public decimal DvzKrediLimiti { get; set; }

       [DbAlan("MtbkBak", SqlDbType.Decimal, 13)]
       public decimal MtbkBak { get; set; }

       [DbAlan("DvzMtbkBak", SqlDbType.Decimal, 13)]
       public decimal DvzMtbkBak { get; set; }

       [DbAlan("DvzDvrB", SqlDbType.Decimal, 13)]
       public decimal DvzDvrB { get; set; }

       [DbAlan("DvzDvrA", SqlDbType.Decimal, 13)]
       public decimal DvzDvrA { get; set; }

       [DbAlan("DvzDigerB", SqlDbType.Decimal, 13)]
       public decimal DvzDigerB { get; set; }

       [DbAlan("DvzDigerA", SqlDbType.Decimal, 13)]
       public decimal DvzDigerA { get; set; }

       [DbAlan("DvzOdemeB", SqlDbType.Decimal, 13)]
       public decimal DvzOdemeB { get; set; }

       [DbAlan("DvzOdemeA", SqlDbType.Decimal, 13)]
       public decimal DvzOdemeA { get; set; }

       [DbAlan("DvzCiroB", SqlDbType.Decimal, 13)]
       public decimal DvzCiroB { get; set; }

       [DbAlan("DvzCiroA", SqlDbType.Decimal, 13)]
       public decimal DvzCiroA { get; set; }

       [DbAlan("DvzIadeB", SqlDbType.Decimal, 13)]
       public decimal DvzIadeB { get; set; }

       [DbAlan("DvzIadeA", SqlDbType.Decimal, 13)]
       public decimal DvzIadeA { get; set; }

       [DbAlan("DvzKDVB", SqlDbType.Decimal, 13)]
       public decimal DvzKDVB { get; set; }

       [DbAlan("DvzKDVA", SqlDbType.Decimal, 13)]
       public decimal DvzKDVA { get; set; }

       [DbAlan("DvrDvzSntRiskB", SqlDbType.Decimal, 13)]
       public decimal DvrDvzSntRiskB { get; set; }

       [DbAlan("DvrDvzSntRiskA", SqlDbType.Decimal, 13)]
       public decimal DvrDvzSntRiskA { get; set; }

       [DbAlan("DvrDvzCekRiskB", SqlDbType.Decimal, 13)]
       public decimal DvrDvzCekRiskB { get; set; }

       [DbAlan("DvrDvzCekRiskA", SqlDbType.Decimal, 13)]
       public decimal DvrDvzCekRiskA { get; set; }

       [DbAlan("DvrDvzTeminatB", SqlDbType.Decimal, 13)]
       public decimal DvrDvzTeminatB { get; set; }

       [DbAlan("DvrDvzTeminatA", SqlDbType.Decimal, 13)]
       public decimal DvrDvzTeminatA { get; set; }

       [DbAlan("DvrDvzTeminat2B", SqlDbType.Decimal, 13)]
       public decimal DvrDvzTeminat2B { get; set; }

       [DbAlan("DvrDvzTeminat2A", SqlDbType.Decimal, 13)]
       public decimal DvrDvzTeminat2A { get; set; }

       [DbAlan("DvzTeminatB", SqlDbType.Decimal, 13)]
       public decimal DvzTeminatB { get; set; }

       [DbAlan("DvzTeminatA", SqlDbType.Decimal, 13)]
       public decimal DvzTeminatA { get; set; }

       [DbAlan("DvzTeminat2B", SqlDbType.Decimal, 13)]
       public decimal DvzTeminat2B { get; set; }

       [DbAlan("DvzTeminat2A", SqlDbType.Decimal, 13)]
       public decimal DvzTeminat2A { get; set; }

       [DbAlan("DvzSntRiskB", SqlDbType.Decimal, 13)]
       public decimal DvzSntRiskB { get; set; }

       [DbAlan("DvzSntRiskiA", SqlDbType.Decimal, 13)]
       public decimal DvzSntRiskiA { get; set; }

       [DbAlan("DvzCekRiskB", SqlDbType.Decimal, 13)]
       public decimal DvzCekRiskB { get; set; }

       [DbAlan("DvzCekRiskA", SqlDbType.Decimal, 13)]
       public decimal DvzCekRiskA { get; set; }

       [DbAlan("DvzProtSnt", SqlDbType.Decimal, 13)]
       public decimal DvzProtSnt { get; set; }

       [DbAlan("DvrDvzProtSenet", SqlDbType.Decimal, 13)]
       public decimal DvrDvzProtSenet { get; set; }

       [DbAlan("DvzKarsCek", SqlDbType.Decimal, 13)]
       public decimal DvzKarsCek { get; set; }

       [DbAlan("DvrDvzKarsCek", SqlDbType.Decimal, 13)]
       public decimal DvrDvzKarsCek { get; set; }

       [DbAlan("YasBakiye", SqlDbType.Decimal, 13)]
       public decimal YasBakiye { get; set; }

       [DbAlan("OrtalamaTarih", SqlDbType.Int, 4)]
       public int OrtalamaTarih { get; set; }

       [DbAlan("OrtalamaGun", SqlDbType.SmallInt, 2)]
       public short OrtalamaGun { get; set; }

       [DbAlan("OtvBorc", SqlDbType.Decimal, 13)]
       public decimal OtvBorc { get; set; }

       [DbAlan("OtvAlacak", SqlDbType.Decimal, 13)]
       public decimal OtvAlacak { get; set; }

       [DbAlan("OtvDvzBorc", SqlDbType.Decimal, 13)]
       public decimal OtvDvzBorc { get; set; }

       [DbAlan("OtvDvzAlacak", SqlDbType.Decimal, 13)]
       public decimal OtvDvzAlacak { get; set; }

       [DbAlan("Nesne1", SqlDbType.VarChar, 254)]
       public string Nesne1 { get; set; }

       [DbAlan("Nesne2", SqlDbType.VarChar, 254)]
       public string Nesne2 { get; set; }

       [DbAlan("Nesne3", SqlDbType.VarChar, 254)]
       public string Nesne3 { get; set; }

       [DbAlan("Notlar", SqlDbType.VarChar, 50)]
       public string Notlar { get; set; }

       [DbAlan("UygFiyatListeNo", SqlDbType.VarChar, 16)]
       public string UygFiyatListeNo { get; set; }

       [DbAlan("FatIrsDenDoganBorc", SqlDbType.Decimal, 13)]
       public decimal FatIrsDenDoganBorc { get; set; }

       [DbAlan("FatIrsDenDoganAlacak", SqlDbType.Decimal, 13)]
       public decimal FatIrsDenDoganAlacak { get; set; }

       [DbAlan("BekleyenSatisSip", SqlDbType.Decimal, 13)]
       public decimal BekleyenSatisSip { get; set; }

       [DbAlan("BekleyenAlimSip", SqlDbType.Decimal, 13)]
       public decimal BekleyenAlimSip { get; set; }

       [DbAlan("FatIrsdenDoganDovizBorc", SqlDbType.Decimal, 13)]
       public decimal FatIrsdenDoganDovizBorc { get; set; }

       [DbAlan("FatIrsdenDoganDovizAlacak", SqlDbType.Decimal, 13)]
       public decimal FatIrsdenDoganDovizAlacak { get; set; }

       [DbAlan("BekleyenSatSipDoviz", SqlDbType.Decimal, 13)]
       public decimal BekleyenSatSipDoviz { get; set; }

       [DbAlan("BekleyenAlimSipDoviz", SqlDbType.Decimal, 13)]
       public decimal BekleyenAlimSipDoviz { get; set; }

       [DbAlan("YetkiliEmail1", SqlDbType.VarChar, 50)]
       public string YetkiliEmail1 { get; set; }

       [DbAlan("YetkiliEmail2", SqlDbType.VarChar, 50)]
       public string YetkiliEmail2 { get; set; }

       [DbAlan("HavaleEdilecekBanka", SqlDbType.VarChar, 4)]
       public string HavaleEdilecekBanka { get; set; }

       [DbAlan("HavaleEdilecekBankaSubesi", SqlDbType.VarChar, 5)]
       public string HavaleEdilecekBankaSubesi { get; set; }

       [DbAlan("HavaleEdilecekHesapNo", SqlDbType.VarChar, 20)]
       public string HavaleEdilecekHesapNo { get; set; }

       [DbAlan("DovizIslemBanka", SqlDbType.VarChar, 4)]
       public string DovizIslemBanka { get; set; }

       [DbAlan("DovizIslemKurCins", SqlDbType.SmallInt, 2)]
       public short DovizIslemKurCins { get; set; }

       [DbAlan("ButceKod", SqlDbType.VarChar, 50)]
       public string ButceKod { get; set; }

       [DbAlan("FaturaChk", SqlDbType.VarChar, 20)]
       public string FaturaChk { get; set; }

       [DbAlan("FaturaAdres1Sehir", SqlDbType.VarChar, 40)]
       public string FaturaAdres1Sehir { get; set; }

       [DbAlan("FaturaAdres1Ulke", SqlDbType.VarChar, 20)]
       public string FaturaAdres1Ulke { get; set; }

       [DbAlan("FaturaAdres1PKod", SqlDbType.VarChar, 7)]
       public string FaturaAdres1PKod { get; set; }

       [DbAlan("TeslimAdres1Sehir", SqlDbType.VarChar, 40)]
       public string TeslimAdres1Sehir { get; set; }

       [DbAlan("TeslimAdres1Ulke", SqlDbType.VarChar, 20)]
       public string TeslimAdres1Ulke { get; set; }

       [DbAlan("TeslimAdres1PKod", SqlDbType.VarChar, 7)]
       public string TeslimAdres1PKod { get; set; }

       [DbAlan("Telefon1Dahili", SqlDbType.VarChar, 4)]
       public string Telefon1Dahili { get; set; }

       [DbAlan("Telefon1BolgeKod", SqlDbType.VarChar, 4)]
       public string Telefon1BolgeKod { get; set; }

       [DbAlan("Telefon1UlkeKod", SqlDbType.VarChar, 4)]
       public string Telefon1UlkeKod { get; set; }

       [DbAlan("Telefon2Dahili", SqlDbType.VarChar, 4)]
       public string Telefon2Dahili { get; set; }

       [DbAlan("Telefon2BolgeKod", SqlDbType.VarChar, 4)]
       public string Telefon2BolgeKod { get; set; }

       [DbAlan("Telefon2UlkeKod", SqlDbType.VarChar, 4)]
       public string Telefon2UlkeKod { get; set; }

       [DbAlan("Telefon3Dahili", SqlDbType.VarChar, 4)]
       public string Telefon3Dahili { get; set; }

       [DbAlan("Telefon3BolgeKod", SqlDbType.VarChar, 4)]
       public string Telefon3BolgeKod { get; set; }

       [DbAlan("Telefon3UlkeKod", SqlDbType.VarChar, 4)]
       public string Telefon3UlkeKod { get; set; }

       [DbAlan("Telefon4Dahili", SqlDbType.VarChar, 4)]
       public string Telefon4Dahili { get; set; }

       [DbAlan("Telefon4BolgeKod", SqlDbType.VarChar, 4)]
       public string Telefon4BolgeKod { get; set; }

       [DbAlan("Telefon4UlkeKod", SqlDbType.VarChar, 4)]
       public string Telefon4UlkeKod { get; set; }

       [DbAlan("Fax1BolgeKod", SqlDbType.VarChar, 4)]
       public string Fax1BolgeKod { get; set; }

       [DbAlan("Fax1UlkeKodu", SqlDbType.VarChar, 4)]
       public string Fax1UlkeKodu { get; set; }

       [DbAlan("Fax2BolgeKod", SqlDbType.VarChar, 4)]
       public string Fax2BolgeKod { get; set; }

       [DbAlan("Fax2UlkeKodu", SqlDbType.VarChar, 4)]
       public string Fax2UlkeKodu { get; set; }

       [DbAlan("BankaKod1", SqlDbType.VarChar, 3)]
       public string BankaKod1 { get; set; }

       [DbAlan("BankaIlKod1", SqlDbType.VarChar, 3)]
       public string BankaIlKod1 { get; set; }

       [DbAlan("BankaSubeKod1", SqlDbType.VarChar, 5)]
       public string BankaSubeKod1 { get; set; }

       [DbAlan("BankaHesap1", SqlDbType.VarChar, 20)]
       public string BankaHesap1 { get; set; }

       [DbAlan("BankaKod2", SqlDbType.VarChar, 3)]
       public string BankaKod2 { get; set; }

       [DbAlan("BankaIlKod2", SqlDbType.VarChar, 3)]
       public string BankaIlKod2 { get; set; }

       [DbAlan("BankaSubeKod2", SqlDbType.VarChar, 5)]
       public string BankaSubeKod2 { get; set; }

       [DbAlan("BankaHesap2", SqlDbType.VarChar, 20)]
       public string BankaHesap2 { get; set; }

       [DbAlan("BankaKod3", SqlDbType.VarChar, 3)]
       public string BankaKod3 { get; set; }

       [DbAlan("BankaIlKod3", SqlDbType.VarChar, 3)]
       public string BankaIlKod3 { get; set; }

       [DbAlan("BankaSubeKod3", SqlDbType.VarChar, 5)]
       public string BankaSubeKod3 { get; set; }

       [DbAlan("BankaHesap3", SqlDbType.VarChar, 20)]
       public string BankaHesap3 { get; set; }

       [DbAlan("BankaKod4", SqlDbType.VarChar, 3)]
       public string BankaKod4 { get; set; }

       [DbAlan("BankaIlKod4", SqlDbType.VarChar, 3)]
       public string BankaIlKod4 { get; set; }

       [DbAlan("BankaSubeKod4", SqlDbType.VarChar, 5)]
       public string BankaSubeKod4 { get; set; }

       [DbAlan("BankaHesap4", SqlDbType.VarChar, 20)]
       public string BankaHesap4 { get; set; }

       [DbAlan("SirketEMail", SqlDbType.VarChar, 50)]
       public string SirketEMail { get; set; }

       [DbAlan("SirketWebAdres", SqlDbType.VarChar, 50)]
       public string SirketWebAdres { get; set; }

       [DbAlan("TeslimatSekli", SqlDbType.SmallInt, 2)]
       public short TeslimatSekli { get; set; }

       [DbAlan("SatisKurCinsi", SqlDbType.SmallInt, 2)]
       public short SatisKurCinsi { get; set; }

       [DbAlan("AlisKurCinsi", SqlDbType.SmallInt, 2)]
       public short AlisKurCinsi { get; set; }

       [DbAlan("FaturaMalAdi", SqlDbType.SmallInt, 2)]
       public short FaturaMalAdi { get; set; }

       /// <summary>
       /// 0-YerelPara, 1-Döviz, 2-YerelPara/Döviz
       /// </summary>
       [DbAlan("DvzTL", SqlDbType.SmallInt, 2)]
       public short DvzTL { get; set; }

       [DbAlan("OdemePlani", SqlDbType.Int, 4)]
       public int OdemePlani { get; set; }

       [DbAlan("SatIslemEMail", SqlDbType.VarChar, 50)]
       public string SatIslemEMail { get; set; }

       [DbAlan("SatAlmaIslemEMail", SqlDbType.VarChar, 50)]
       public string SatAlmaIslemEMail { get; set; }

       [DbAlan("FinIslemEMail", SqlDbType.VarChar, 50)]
       public string FinIslemEMail { get; set; }

       [DbAlan("CHKodu", SqlDbType.VarChar, 20)]
       public string CHKodu { get; set; }

       [DbAlan("UlkeNumKod", SqlDbType.VarChar, 3)]
       public string UlkeNumKod { get; set; }

       [DbAlan("FormBaBsUnvan", SqlDbType.SmallInt, 2)]
       public short FormBaBsUnvan { get; set; }

       [DbAlan("OivBorc", SqlDbType.Decimal, 13)]
       public decimal OivBorc { get; set; }

       [DbAlan("OivAlacak", SqlDbType.Decimal, 13)]
       public decimal OivAlacak { get; set; }

       [DbAlan("OivDvzBorc", SqlDbType.Decimal, 13)]
       public decimal OivDvzBorc { get; set; }

       [DbAlan("OivDvzAlacak", SqlDbType.Decimal, 13)]
       public decimal OivDvzAlacak { get; set; }

       [DbAlan("BankaUlkeNumKod1", SqlDbType.VarChar, 3)]
       public string BankaUlkeNumKod1 { get; set; }

       [DbAlan("BankaUlkeNumKod2", SqlDbType.VarChar, 3)]
       public string BankaUlkeNumKod2 { get; set; }

       [DbAlan("BankaUlkeNumKod3", SqlDbType.VarChar, 3)]
       public string BankaUlkeNumKod3 { get; set; }

       [DbAlan("BankaUlkeNumKod4", SqlDbType.VarChar, 3)]
       public string BankaUlkeNumKod4 { get; set; }

       [DbAlan("IslemTipi", SqlDbType.SmallInt, 2)]
       public short IslemTipi { get; set; }

       [DbAlan("BakGostSekli", SqlDbType.SmallInt, 2)]
       public short BakGostSekli { get; set; }

       [DbAlan("HesapTuru", SqlDbType.SmallInt, 2)]
       public short HesapTuru { get; set; }

       [DbAlan("VergiDairesi2", SqlDbType.VarChar, 12)]
       public string VergiDairesi2 { get; set; }

       [DbAlan("GuvenlikKod", SqlDbType.VarChar, 2)]
       public string GuvenlikKod { get; set; }

       [DbAlan("Kaydeden", SqlDbType.VarChar, 5)]
       public string Kaydeden { get; set; }

       [DbAlan("KayitTarih", SqlDbType.Int, 4)]
       public int KayitTarih { get; set; }

       [DbAlan("KayitSaat", SqlDbType.Int, 4)]
       public int KayitSaat { get; set; }

       [DbAlan("KayitKaynak", SqlDbType.SmallInt, 2)]
       public short KayitKaynak { get; set; }

       [DbAlan("KayitSurum", SqlDbType.VarChar, 12)]
       public string KayitSurum { get; set; }

       [DbAlan("Degistiren", SqlDbType.VarChar, 5)]
       public string Degistiren { get; set; }

       [DbAlan("DegisTarih", SqlDbType.Int, 4)]
       public int DegisTarih { get; set; }

       [DbAlan("DegisSaat", SqlDbType.Int, 4)]
       public int DegisSaat { get; set; }

       [DbAlan("DegisKaynak", SqlDbType.SmallInt, 2)]
       public short DegisKaynak { get; set; }

       [DbAlan("DegisSurum", SqlDbType.VarChar, 12)]
       public string DegisSurum { get; set; }

       [DbAlan("CheckSum", SqlDbType.SmallInt, 2)]
       public short CheckSum { get; set; }

       [DbAlan("BekleyenSatisTkf", SqlDbType.Decimal, 13)]
       public decimal BekleyenSatisTkf { get; set; }

       [DbAlan("BekleyenAlimTkf", SqlDbType.Decimal, 13)]
       public decimal BekleyenAlimTkf { get; set; }

       [DbAlan("BekleyenSatTkfDoviz", SqlDbType.Decimal, 13)]
       public decimal BekleyenSatTkfDoviz { get; set; }

       [DbAlan("BekleyenAlimTkfDoviz", SqlDbType.Decimal, 13)]
       public decimal BekleyenAlimTkfDoviz { get; set; }

       [DbAlan("BankaIBAN1", SqlDbType.VarChar, 40)]
       public string BankaIBAN1 { get; set; }

       [DbAlan("BankaIBAN2", SqlDbType.VarChar, 40)]
       public string BankaIBAN2 { get; set; }

       [DbAlan("BankaIBAN3", SqlDbType.VarChar, 40)]
       public string BankaIBAN3 { get; set; }

       [DbAlan("BankaIBAN4", SqlDbType.VarChar, 40)]
       public string BankaIBAN4 { get; set; }

       [DbAlan("BankaDvzCinsi1", SqlDbType.VarChar, 3)]
       public string BankaDvzCinsi1 { get; set; }

       [DbAlan("BankaDvzCinsi2", SqlDbType.VarChar, 3)]
       public string BankaDvzCinsi2 { get; set; }

       [DbAlan("BankaDvzCinsi3", SqlDbType.VarChar, 3)]
       public string BankaDvzCinsi3 { get; set; }

       [DbAlan("BankaDvzCinsi4", SqlDbType.VarChar, 3)]
       public string BankaDvzCinsi4 { get; set; }

       /// <summary>
       /// 0-Pasif, 1-Aktif
       /// </summary>
       [DbAlan("AktifPasif", SqlDbType.SmallInt, 2)]
       public short AktifPasif { get; set; }

       [DbAlan("IrsaliyeRaporAdi", SqlDbType.VarChar, 254)]
       public string IrsaliyeRaporAdi { get; set; }

       [DbAlan("FaturaRaporAdi", SqlDbType.VarChar, 254)]
       public string FaturaRaporAdi { get; set; }

       [DbAlan("KDVTevfikatUygula", SqlDbType.SmallInt, 2)]
       public short KDVTevfikatUygula { get; set; }

       [DbAlan("Kod14", SqlDbType.VarChar, 20)]
       public string Kod14 { get; set; }

       [DbAlan("Kod15", SqlDbType.VarChar, 20)]
       public string Kod15 { get; set; }

       [DbAlan("Kod16", SqlDbType.VarChar, 20)]
       public string Kod16 { get; set; }

       [DbAlan("Kod17", SqlDbType.VarChar, 20)]
       public string Kod17 { get; set; }

       [DbAlan("Kod18", SqlDbType.VarChar, 20)]
       public string Kod18 { get; set; }

       [DbAlan("EFatAdresCadde", SqlDbType.VarChar, 40)]
       public string EFatAdresCadde { get; set; }

       [DbAlan("EFatAdresBinaAdi", SqlDbType.VarChar, 40)]
       public string EFatAdresBinaAdi { get; set; }

       [DbAlan("EFatAdresDisKapiNo", SqlDbType.VarChar, 10)]
       public string EFatAdresDisKapiNo { get; set; }

       [DbAlan("EFatAdresIcKapiNo", SqlDbType.VarChar, 10)]
       public string EFatAdresIcKapiNo { get; set; }

       [DbAlan("EFatAdresKasabaKoy", SqlDbType.VarChar, 40)]
       public string EFatAdresKasabaKoy { get; set; }

       [DbAlan("EFatAdresIlce", SqlDbType.VarChar, 40)]
       public string EFatAdresIlce { get; set; }

       [DbAlan("EFatKullanici", SqlDbType.SmallInt, 2)]
       public short EFatKullanici { get; set; }

       [DbAlan("EFatSenaryo", SqlDbType.SmallInt, 2)]
       public short EFatSenaryo { get; set; }

       [DbAlan("EFatEtiket", SqlDbType.VarChar, 128)]
       public string EFatEtiket { get; set; }

       [DbAlan("Ad", SqlDbType.VarChar, 40)]
       public string Ad { get; set; }

       [DbAlan("SoyAd", SqlDbType.VarChar, 40)]
       public string SoyAd { get; set; }

       [DbAlan("EFatMusBrmSubeTnm", SqlDbType.VarChar, 20)]
       public string EFatMusBrmSubeTnm { get; set; }

       [DbAlan("EFatMusBrmSubeTnmDeger", SqlDbType.VarChar, 20)]
       public string EFatMusBrmSubeTnmDeger { get; set; }

       [DbAlan("EFatCHKoduTnm", SqlDbType.VarChar, 20)]
       public string EFatCHKoduTnm { get; set; }

       [DbAlan("KDVMuaf", SqlDbType.SmallInt, 2)]
       public short KDVMuaf { get; set; }

       [DbAlan("KDVMuafAck", SqlDbType.VarChar, 20)]
       public string KDVMuafAck { get; set; }

       [DbAlan("ROW_ID", SqlDbType.Int, 4, true, false, false)]
       public int ROW_ID { get; set; }

       #endregion

       internal KKP_CHK()
       {

       }
    }
}
