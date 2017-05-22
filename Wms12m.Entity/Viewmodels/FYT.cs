using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
   public class FYT
    {
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BasTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BasSaat { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BitTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int BitSaat { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SiraNo { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string RenkBedenKod1 { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string RenkBedenKod2 { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string RenkBedenKod3 { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string RenkBedenKod4 { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string FiyatListNum { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short FiyatTuru { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod1 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod2 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod3 { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short KodTipi { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string MusteriKodu { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal AlisFiyat1 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal AlisFiyat2 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal AlisFiyat3 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DvzAlisFiyat1 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DvzAlisFiyat2 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DvzAlisFiyat3 { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string AF1DovizCinsi { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string AF2DovizCinsi { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string AF3DovizCinsi { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short AF1KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short AF2KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short AF3KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizAF1KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizAF2KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizAF3KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short AF1Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short AF2Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short AF3Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizAF1Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizAF2Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizAF3Birim { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat1 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat2 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat3 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat4 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat5 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat6 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DvzSatisFiyat1 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DvzSatisFiyat2 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DvzSatisFiyat3 { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string SF1DovizCinsi { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string SF2DovizCinsi { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string SF3DovizCinsi { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string SF4DovizCinsi { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string SF5DovizCinsi { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string SF6DovizCinsi { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF1KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF2KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF3KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF4KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF5KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF6KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizSF1KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizSF2KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizSF3KDV { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF1Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF2Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF3Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF4Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF5Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF6Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizSF1Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizSF2Birim { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DovizSF3Birim { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string FiyatListName { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyatAltLimit { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyatUstLimit { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF1ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF2ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF3ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF4ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF5ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short SF6ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DvzSF1ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DvzSF2ValorGun { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short DvzSF3ValorGun { get; set; }
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
        /// <summary> VarChar(64) (Not Null) </summary>
        public string Aciklama { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod4 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod5 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod6 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod7 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod8 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod9 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Kod10 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Kod11 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Kod12 { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SF1VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SF2VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SF3VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SF4VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SF5VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int SF6VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DvzSF1VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DvzSF2VadeTarih { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int DvzSF3VadeTarih { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short FiyatListeTipi { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int ROW_ID { get; set; }
        /// <summary> Timestamp (Not Null) </summary>
        public byte[] timestamp { get; set; }
    }
}
