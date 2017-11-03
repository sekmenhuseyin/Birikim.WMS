using System;

namespace Wms12m.Entity
{
    public class EMG
    {
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EmirNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Musteri { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string SipNo { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short IcDis { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string Mamul { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short RecID { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Birim { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Miktar { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string IlkTzg { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BasTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BasSaat { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BitTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BitSaat { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int GerBsTr { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int GerBsSt { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int GerBtTr { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int GerBtSt { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string CurTzg { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short CurDurum { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short CurDurSb { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SonDurSb { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal PlDrMly { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal GrDrMly { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal PlEndMly { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal GrEndMly { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string Talimat1 { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string Talimat2 { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string Talimat3 { get; set; }
        /// <summary> VarChar(200) (Not Null) </summary>
        public string Notlar { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short PlOnay { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int PlTarih { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Planly { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int OnTarih { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Onayly { get; set; }
        /// <summary> Real (Not Null) </summary>
        public Single OrHmdBsr { get; set; }
        /// <summary> Real (Not Null) </summary>
        public Single OrZmnBsr { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod1 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod2 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod3 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod4 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod5 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Kod6 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Kod7 { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short YMUret { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short YMMly { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short YMEndMly { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short YMDepo { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short YMHmdCik { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string StiNo { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Teklif { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string TrsfrNo { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string RenkBedenKod1 { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string RenkBedenKod2 { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string RenkBedenKod3 { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string RenkBedenKod4 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string AmbKod { get; set; }
        /// <summary> VarChar(254) (Not Null) </summary>
        public string Nesne1 { get; set; }
        /// <summary> VarChar(254) (Not Null) </summary>
        public string Nesne2 { get; set; }
        /// <summary> VarChar(254) (Not Null) </summary>
        public string Nesne3 { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short KayitTuru { get; set; }
        /// <summary> VarChar(2) (Not Null) </summary>
        public string GuvenlikKod { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int KayitTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int KayitSaat { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short KayitKaynak { get; set; }
        /// <summary> VarChar(12) (Not Null) </summary>
        public string KayitSurum { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Degistiren { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DegisTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DegisSaat { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DegisKaynak { get; set; }
        /// <summary> VarChar(12) (Not Null) </summary>
        public string DegisSurum { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short CheckSum { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Row_ID { get; set; }
        /// <summary> Timestamp (Not Null) </summary>
        public byte[] timestamp { get; set; }
    }
}
