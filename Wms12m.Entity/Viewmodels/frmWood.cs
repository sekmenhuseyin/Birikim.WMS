using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class TahsisOnayOdun { 
    /// <summary> Int (Not Null) </summary>
    public int ID { get; set; }
    /// <summary> SmallInt (Not Null) </summary>
    public short Tip { get; set; }
    /// <summary> Int (Not Null) </summary>
    public int Yil { get; set; }
    /// <summary> Int (Not Null) </summary>
    public int Hafta { get; set; }
    /// <summary> Int (Not Null) </summary>
    public int Tarih { get; set; }
    /// <summary> SmallInt (Not Null) </summary>
    public short SiraNo { get; set; }
    /// <summary> VarChar(20) (Not Null) </summary>
    public string OrmIslt { get; set; }
    /// <summary> VarChar(18) (Allow Null) </summary>
    public string TipAck { get; set; }
    /// <summary> VarChar(30) (Allow Null) </summary>
    public string EmvalCinsi { get; set; }
    /// <summary> Decimal(18,2) (Not Null) </summary>
    public decimal Miktar { get; set; }
    /// <summary> VarChar(4) (Allow Null) </summary>
    public string Birim { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? MuhBdl1 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? MuhBdl2 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? IbreliMiktarSter { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? IbreliMiktarM3 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? YaprakliMiktarSter { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? YaprakliMiktarM3 { get; set; }
    /// <summary> VarChar(100) (Allow Null) </summary>
    public string NrdKull { get; set; }
    /// <summary> VarChar(100) (Allow Null) </summary>
    public string StkDrm { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? NakFiyati { get; set; }
    /// <summary> VarChar(250) (Allow Null) </summary>
    public string Aciklama { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TavanMiktar { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TavanFiyat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Ortalama { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? VezirOrt { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Vezir { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? KstEnt { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Camsan { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Terme { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? SFC { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Piyasa { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? Pazarlik { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahBirimFiyat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahMalBedeli { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahAgFn { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahVadeFarki { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahKdv18 { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahToplam { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahTeminat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahMektupFaizi { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahTopMektupTutar { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahPesinat { get; set; }
    /// <summary> Decimal(18,2) (Allow Null) </summary>
    public decimal? TahGenelTop { get; set; }
    /// <summary> VarChar(100) (Allow Null) </summary>
    public string TahOncelik { get; set; }
    /// <summary> Bit (Not Null) </summary>
    public bool Onay { get; set; }
    /// <summary> VarChar(5) (Allow Null) </summary>
    public string Onaylayan { get; set; }
    /// <summary> SmallDatetime (Allow Null) </summary>
    public DateTime? OnayTarih { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanIbreliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanIbreliM3 { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanYaprakliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal PlanYaprakliM3 { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulIbreliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulYaprakliSter { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulIbreliM3 { get; set; }
    /// <summary> Decimal(4,4) (Not Null) </summary>
    public decimal KulYaprakliM3 { get; set; }
    /// <summary> VarChar(81) (Allow Null) </summary>
    public string OrmIsltUnvan { get; set; }
    /// <summary> VarChar(50) (Allow Null) </summary>
    public string EmvalCinsiAdi { get; set; }
    /// <summary> SmallDatetime (Allow Null) </summary>
    public DateTime? Tarih2 { get; set; }
    /// <summary> Bit (Allow Null) </summary>
    public bool? Secim { get; set; }
    }
    public class IHLTAH
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string value { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string name { get; set; }

        /// <summary> Int (Not Null) </summary>
        public int ID { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Tip { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Yil { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Hafta { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Tarih { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SiraNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string OrmIslt { get; set; }
        /// <summary> VarChar(18) (Allow Null) </summary>
        public string TipAck { get; set; }
        /// <summary> VarChar(30) (Allow Null) </summary>
        public string EmvalCinsi { get; set; }
        /// <summary> Decimal(18,2) (Not Null) </summary>
        public decimal Miktar { get; set; }
        /// <summary> VarChar(4) (Allow Null) </summary>
        public string Birim { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? MuhBdl1 { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? MuhBdl2 { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? IbreliMiktarSter { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? IbreliMiktarM3 { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? YaprakliMiktarSter { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? YaprakliMiktarM3 { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string NrdKull { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string StkDrm { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? NakFiyati { get; set; }
        /// <summary> VarChar(250) (Allow Null) </summary>
        public string Aciklama { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TavanMiktar { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TavanFiyat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? Ortalama { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? VezirOrt { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? Vezir { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? KstEnt { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? Camsan { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? Terme { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? SFC { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? Piyasa { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? Pazarlik { get; set; }
        /// <summary> VarChar(5) (Allow Null) </summary>
        public string TebDepo { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TebPartiNo { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebMiktar { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebBirimFiyat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebVadeliTeminat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebVadeliPesinat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebKampTeminat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebKampPesinat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebPesinTutar { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TebTemMektupDosya { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TebPesDekontDosya { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TebFaturaDosya { get; set; }
        /// <summary> Date (Allow Null) </summary>
        public DateTime? TebTarih { get; set; }
        /// <summary> Date (Allow Null) </summary>
        public DateTime? TebSonSatisTarih { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebKrediKartOdeme { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TebHavaleOdeme { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahBirimFiyat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahMalBedeli { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahAgFn { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahVadeFarki { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahKdv18 { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahToplam { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahTeminat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahMektupFaizi { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahTopMektupTutar { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahPesinat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahGenelTop { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string TahOncelik { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahTemMektupBankaAd { get; set; }
        /// <summary> Date (Allow Null) </summary>
        public DateTime? TahTemMektupTarih { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahTemMektupNo { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahTemMektupDosya { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahTemMektupKargo { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahTemMektupKargoNo { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahPesDekontBankaAd { get; set; }
        /// <summary> Date (Allow Null) </summary>
        public DateTime? TahPesDekontTarih { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahPesDekontDosya { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string TahFaturaDosya { get; set; }
        /// <summary> VarChar(16) (Allow Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> Bit (Not Null) </summary>
        public bool Onay { get; set; }
        /// <summary> VarChar(5) (Allow Null) </summary>
        public string Onaylayan { get; set; }
        /// <summary> SmallDatetime (Allow Null) </summary>
        public DateTime? OnayTarih { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string Kod1 { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string Kod2 { get; set; }
        /// <summary> Int (Allow Null) </summary>
        public int? Kod3 { get; set; }
        /// <summary> Int (Allow Null) </summary>
        public int? Kod4 { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? Kod5 { get; set; }
        /// <summary> VarChar(5) (Allow Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> SmallDatetime (Not Null) </summary>
        public DateTime KayitTarih { get; set; }
        /// <summary> VarChar(5) (Allow Null) </summary>
        public string Degistiren { get; set; }
        /// <summary> SmallDatetime (Not Null) </summary>
        public DateTime DegisTarih { get; set; }

    }
    public class IHLTAHKayitResult {
        public int  ID{get;set;}

        public short  Tip{get;set;}

        public int  Yil{get;set;}

        public int  Hafta{get;set;}

        public int  Tarih{get;set;}

        public short  SiraNo{get;set;}

        public string  OrmIslt{get;set;}

        public string  TipAck{get;set;}

        public string  EmvalCinsi{get;set;}

        public decimal  Miktar{get;set;}

        public string  Birim{get;set;}

        public System.Nullable<decimal>  MuhBdl1{get;set;}

        public System.Nullable<decimal>  MuhBdl2{get;set;}

        public System.Nullable<decimal>  IbreliMiktarSter{get;set;}

        public System.Nullable<decimal>  IbreliMiktarM3{get;set;}

        public System.Nullable<decimal>  YaprakliMiktarSter{get;set;}

        public System.Nullable<decimal>  YaprakliMiktarM3{get;set;}

        public string  NrdKull{get;set;}

        public string  StkDrm{get;set;}

        public System.Nullable<decimal>  NakFiyati{get;set;}

        public string  Aciklama{get;set;}

        public System.Nullable<decimal>  TavanMiktar{get;set;}

        public System.Nullable<decimal>  TavanFiyat{get;set;}

        public System.Nullable<decimal>  Ortalama{get;set;}

        public System.Nullable<decimal>  VezirOrt{get;set;}

        public System.Nullable<decimal>  Vezir{get;set;}

        public System.Nullable<decimal>  KstEnt{get;set;}

        public System.Nullable<decimal>  Camsan{get;set;}

        public System.Nullable<decimal>  Terme{get;set;}

        public System.Nullable<decimal>  SFC{get;set;}

        public System.Nullable<decimal>  Piyasa{get;set;}

        public System.Nullable<decimal>  Pazarlik{get;set;}

        public string  TebDepo{get;set;}

        public string  TebPartiNo{get;set;}

        public System.Nullable<decimal>  TebMiktar{get;set;}

        public System.Nullable<decimal>  TebBirimFiyat{get;set;}

        public System.Nullable<decimal>  TebVadeliTeminat{get;set;}

        public System.Nullable<decimal>  TebVadeliPesinat{get;set;}

        public System.Nullable<decimal>  TebKampTeminat{get;set;}

        public System.Nullable<decimal>  TebKampPesinat{get;set;}

        public System.Nullable<decimal>  TebPesinTutar{get;set;}

        public string  TebTemMektupDosya{get;set;}

        public string  TebPesDekontDosya{get;set;}

        public string  TebFaturaDosya{get;set;}

        public System.Nullable<System.DateTime>  TebTarih{get;set;}

        public System.Nullable<System.DateTime>  TebSonSatisTarih{get;set;}

        public System.Nullable<decimal>  TebKrediKartOdeme{get;set;}

        public System.Nullable<decimal>  TebHavaleOdeme{get;set;}

        public System.Nullable<decimal>  TahBirimFiyat{get;set;}

        public System.Nullable<decimal>  TahMalBedeli{get;set;}

        public System.Nullable<decimal>  TahAgFn{get;set;}

        public System.Nullable<decimal>  TahVadeFarki{get;set;}

        public System.Nullable<decimal>  TahKdv18{get;set;}

        public System.Nullable<decimal>  TahToplam{get;set;}

        public System.Nullable<decimal>  TahTeminat{get;set;}

        public System.Nullable<decimal>  TahMektupFaizi{get;set;}

        public System.Nullable<decimal>  TahTopMektupTutar{get;set;}

        public System.Nullable<decimal>  TahPesinat{get;set;}

        public System.Nullable<decimal>  TahGenelTop{get;set;}

        public string  TahOncelik{get;set;}

        public string  TahTemMektupBankaAd{get;set;}

        public System.Nullable<System.DateTime>  TahTemMektupTarih{get;set;}

        public string  TahTemMektupNo{get;set;}

        public string  TahTemMektupDosya{get;set;}

        public string  TahTemMektupKargo{get;set;}

        public string  TahTemMektupKargoNo{get;set;}

        public string  TahPesDekontBankaAd{get;set;}

        public System.Nullable<System.DateTime>  TahPesDekontTarih{get;set;}

        public string  TahPesDekontDosya{get;set;}

        public string  TahFaturaDosya{get;set;}

        public string  EvrakNo{get;set;}

        public bool  Onay{get;set;}

        public string  Onaylayan{get;set;}

        public System.Nullable<System.DateTime>  OnayTarih{get;set;}

        public string  Kod1{get;set;}

        public string  Kod2{get;set;}

        public System.Nullable<int>  Kod3{get;set;}

        public System.Nullable<int>  Kod4{get;set;}

        public System.Nullable<decimal>  Kod5{get;set;}

        public System.DateTime  KayitTarih{get;set;}

        public System.DateTime  DegisTarih{get;set;}

        public string  AlimFatura{get;set;}

        public decimal  PlanIbreliSter{get;set;}

        public decimal  PlanIbreliM3{get;set;}

        public decimal  PlanYaprakliSter{get;set;}

        public decimal  PlanYaprakliM3{get;set;}

        public decimal  KulIbreliSter{get;set;}

        public decimal  KulYaprakliSter{get;set;}

        public decimal  KulIbreliM3{get;set;}

        public decimal  KulYaprakliM3{get;set;}

        public string  OrmIsltUnvan{get;set;}

        public string  EmvalCinsiAdi{get;set;}

        public System.Nullable<System.DateTime>  Tarih2{get;set;}
    }
    public class CHKSelect1Result {
        public string HesapKodu { get; set; }

        public string Unvan { get; set; }
    }
    public class TahsisliIsletmeKasa {
        /// <summary> VarChar(16) (Allow Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string OrmIslt { get; set; }
        /// <summary> VarChar(40) (Allow Null) </summary>
        public string OrmIsltUnvan { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Yil { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Hafta { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahTopMektupTutar { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahPesinat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? IbreliMiktarSter { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? IbreliMiktarM3 { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? YaprakliMiktarSter { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? YaprakliMiktarM3 { get; set; }

    }

    public class MyChi
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }

        /// <summary>
        /// Fatura No
        /// </summary>
        public string EvrakNo { get; set; }
        /// <summary>
        /// Fatura Tarih
        /// </summary>
        public DateTime Tarih { get; set; }
        /// <summary>
        /// Tahsis Evrak No
        /// </summary>
        public string EvrakNo2 { get; set; }

        /// <summary>
        /// Mektup Tutar
        /// </summary>
        public decimal Kod13 { get; set; }
        /// <summary>
        /// Peşinat Tutar
        /// </summary>
        public decimal Kod14 { get; set; }

        public override string ToString()
        {
            return Unvan;
        }

        public List<MySti> FaturaDetay { get; set; }

    }
    public class MySti
    {
        public string Chk { get; set; }
        public string EvrakNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public int Tarih { get; set; }

        public short SiraNo { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
        public string Depo { get; set; }

        public string Unvan { get; set; }
        public string DepoAdi { get; set; }

        public short KynkEvrakTip { get; set; }


        /// <summary>
        /// STok Yaşlandırma Raporunda MalKodu + " "+Depo Stok mİktarını Tutuyoruz. 
        /// </summary>
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }

        /// <summary>
        /// Stok Yaşlandırma Raporunda Depo Stok Miktarını Tututyoruz.
        /// </summary>
        public decimal Kod13 { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}", MalKodu, MalAdi);
        }
    }
}
