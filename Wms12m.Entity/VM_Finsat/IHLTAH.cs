﻿using System;

namespace Wms12m.Entity
{
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
}