using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
    class KKP_DST
    {
        #region DST_Column Names
        
        [DbAlan("MalKodu", SqlDbType.VarChar, 30, false, true, false)]
        public string MalKodu { get; set; }

        [DbAlan("BasTarih", SqlDbType.Int, 4, false, true, false)]
        public int BasTarih { get; set; }

        [DbAlan("BasSaat", SqlDbType.Int, 4, false, true, false)]
        public int BasSaat { get; set; }

        [DbAlan("BitTarih", SqlDbType.Int, 4)]
        public int BitTarih { get; set; }

        [DbAlan("BitSaat", SqlDbType.Int, 4)]
        public int BitSaat { get; set; }

        [DbAlan("SiraNo", SqlDbType.SmallInt, 2, false, true, false)]
        public short SiraNo { get; set; }

        [DbAlan("RenkBedenKod1", SqlDbType.VarChar, 16)]
        public string RenkBedenKod1 { get; set; }

        [DbAlan("RenkBedenKod2", SqlDbType.VarChar, 16)]
        public string RenkBedenKod2 { get; set; }

        [DbAlan("RenkBedenKod3", SqlDbType.VarChar, 16)]
        public string RenkBedenKod3 { get; set; }

        [DbAlan("RenkBedenKod4", SqlDbType.VarChar, 16)]
        public string RenkBedenKod4 { get; set; }

        [DbAlan("FiyatListNum", SqlDbType.VarChar, 16, false, true, false)]
        public string FiyatListNum { get; set; }

        [DbAlan("FiyatTuru", SqlDbType.SmallInt, 2)]
        public short FiyatTuru { get; set; }

        [DbAlan("Kod1", SqlDbType.VarChar, 10)]
        public string Kod1 { get; set; }

        [DbAlan("Kod2", SqlDbType.VarChar, 10)]
        public string Kod2 { get; set; }

        [DbAlan("Kod3", SqlDbType.VarChar, 10)]
        public string Kod3 { get; set; }

        [DbAlan("KodTipi", SqlDbType.SmallInt, 2)]
        public short KodTipi { get; set; }

        [DbAlan("MusteriKodu", SqlDbType.VarChar, 20, false, true, false)]
        public string MusteriKodu { get; set; }

        [DbAlan("AlisFiyat1", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat1 { get; set; }

        [DbAlan("AlisFiyat2", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat2 { get; set; }

        [DbAlan("AlisFiyat3", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat3 { get; set; }

        [DbAlan("DvzAlisFiyat1", SqlDbType.Decimal, 13)]
        public decimal DvzAlisFiyat1 { get; set; }

        [DbAlan("DvzAlisFiyat2", SqlDbType.Decimal, 13)]
        public decimal DvzAlisFiyat2 { get; set; }

        [DbAlan("DvzAlisFiyat3", SqlDbType.Decimal, 13)]
        public decimal DvzAlisFiyat3 { get; set; }

        [DbAlan("AF1DovizCinsi", SqlDbType.VarChar, 3)]
        public string AF1DovizCinsi { get; set; }

        [DbAlan("AF2DovizCinsi", SqlDbType.VarChar, 3)]
        public string AF2DovizCinsi { get; set; }

        [DbAlan("AF3DovizCinsi", SqlDbType.VarChar, 3)]
        public string AF3DovizCinsi { get; set; }

        [DbAlan("AF1KDV", SqlDbType.SmallInt, 2)]
        public short AF1KDV { get; set; }

        [DbAlan("AF2KDV", SqlDbType.SmallInt, 2)]
        public short AF2KDV { get; set; }

        [DbAlan("AF3KDV", SqlDbType.SmallInt, 2)]
        public short AF3KDV { get; set; }

        [DbAlan("DovizAF1KDV", SqlDbType.SmallInt, 2)]
        public short DovizAF1KDV { get; set; }

        [DbAlan("DovizAF2KDV", SqlDbType.SmallInt, 2)]
        public short DovizAF2KDV { get; set; }

        [DbAlan("DovizAF3KDV", SqlDbType.SmallInt, 2)]
        public short DovizAF3KDV { get; set; }

        [DbAlan("AF1Birim", SqlDbType.SmallInt, 2)]
        public short AF1Birim { get; set; }

        [DbAlan("AF2Birim", SqlDbType.SmallInt, 2)]
        public short AF2Birim { get; set; }

        [DbAlan("AF3Birim", SqlDbType.SmallInt, 2)]
        public short AF3Birim { get; set; }

        [DbAlan("DovizAF1Birim", SqlDbType.SmallInt, 2)]
        public short DovizAF1Birim { get; set; }

        [DbAlan("DovizAF2Birim", SqlDbType.SmallInt, 2)]
        public short DovizAF2Birim { get; set; }

        [DbAlan("DovizAF3Birim", SqlDbType.SmallInt, 2)]
        public short DovizAF3Birim { get; set; }

        [DbAlan("SatisFiyat1", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat1 { get; set; }

        [DbAlan("SatisFiyat2", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat2 { get; set; }

        [DbAlan("SatisFiyat3", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat3 { get; set; }

        [DbAlan("SatisFiyat4", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat4 { get; set; }

        [DbAlan("SatisFiyat5", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat5 { get; set; }

        [DbAlan("SatisFiyat6", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat6 { get; set; }

        [DbAlan("DvzSatisFiyat1", SqlDbType.Decimal, 13)]
        public decimal DvzSatisFiyat1 { get; set; }

        [DbAlan("DvzSatisFiyat2", SqlDbType.Decimal, 13)]
        public decimal DvzSatisFiyat2 { get; set; }

        [DbAlan("DvzSatisFiyat3", SqlDbType.Decimal, 13)]
        public decimal DvzSatisFiyat3 { get; set; }

        [DbAlan("SF1DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF1DovizCinsi { get; set; }

        [DbAlan("SF2DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF2DovizCinsi { get; set; }

        [DbAlan("SF3DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF3DovizCinsi { get; set; }

        [DbAlan("SF4DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF4DovizCinsi { get; set; }

        [DbAlan("SF5DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF5DovizCinsi { get; set; }

        [DbAlan("SF6DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF6DovizCinsi { get; set; }

        [DbAlan("SF1KDV", SqlDbType.SmallInt, 2)]
        public short SF1KDV { get; set; }

        [DbAlan("SF2KDV", SqlDbType.SmallInt, 2)]
        public short SF2KDV { get; set; }

        [DbAlan("SF3KDV", SqlDbType.SmallInt, 2)]
        public short SF3KDV { get; set; }

        [DbAlan("SF4KDV", SqlDbType.SmallInt, 2)]
        public short SF4KDV { get; set; }

        [DbAlan("SF5KDV", SqlDbType.SmallInt, 2)]
        public short SF5KDV { get; set; }

        [DbAlan("SF6KDV", SqlDbType.SmallInt, 2)]
        public short SF6KDV { get; set; }

        [DbAlan("DovizSF1KDV", SqlDbType.SmallInt, 2)]
        public short DovizSF1KDV { get; set; }

        [DbAlan("DovizSF2KDV", SqlDbType.SmallInt, 2)]
        public short DovizSF2KDV { get; set; }

        [DbAlan("DovizSF3KDV", SqlDbType.SmallInt, 2)]
        public short DovizSF3KDV { get; set; }

        [DbAlan("SF1Birim", SqlDbType.SmallInt, 2)]
        public short SF1Birim { get; set; }

        [DbAlan("SF2Birim", SqlDbType.SmallInt, 2)]
        public short SF2Birim { get; set; }

        [DbAlan("SF3Birim", SqlDbType.SmallInt, 2)]
        public short SF3Birim { get; set; }

        [DbAlan("SF4Birim", SqlDbType.SmallInt, 2)]
        public short SF4Birim { get; set; }

        [DbAlan("SF5Birim", SqlDbType.SmallInt, 2)]
        public short SF5Birim { get; set; }

        [DbAlan("SF6Birim", SqlDbType.SmallInt, 2)]
        public short SF6Birim { get; set; }

        [DbAlan("DovizSF1Birim", SqlDbType.SmallInt, 2)]
        public short DovizSF1Birim { get; set; }

        [DbAlan("DovizSF2Birim", SqlDbType.SmallInt, 2)]
        public short DovizSF2Birim { get; set; }

        [DbAlan("DovizSF3Birim", SqlDbType.SmallInt, 2)]
        public short DovizSF3Birim { get; set; }

        [DbAlan("FiyatListName", SqlDbType.VarChar, 30)]
        public string FiyatListName { get; set; }

        [DbAlan("SatisFiyatAltLimit", SqlDbType.Decimal, 13)]
        public decimal SatisFiyatAltLimit { get; set; }

        [DbAlan("SatisFiyatUstLimit", SqlDbType.Decimal, 13)]
        public decimal SatisFiyatUstLimit { get; set; }

        [DbAlan("SF1ValorGun", SqlDbType.SmallInt, 2)]
        public short SF1ValorGun { get; set; }

        [DbAlan("SF2ValorGun", SqlDbType.SmallInt, 2)]
        public short SF2ValorGun { get; set; }

        [DbAlan("SF3ValorGun", SqlDbType.SmallInt, 2)]
        public short SF3ValorGun { get; set; }

        [DbAlan("SF4ValorGun", SqlDbType.SmallInt, 2)]
        public short SF4ValorGun { get; set; }

        [DbAlan("SF5ValorGun", SqlDbType.SmallInt, 2)]
        public short SF5ValorGun { get; set; }

        [DbAlan("SF6ValorGun", SqlDbType.SmallInt, 2)]
        public short SF6ValorGun { get; set; }

        [DbAlan("DvzSF1ValorGun", SqlDbType.SmallInt, 2)]
        public short DvzSF1ValorGun { get; set; }

        [DbAlan("DvzSF2ValorGun", SqlDbType.SmallInt, 2)]
        public short DvzSF2ValorGun { get; set; }

        [DbAlan("DvzSF3ValorGun", SqlDbType.SmallInt, 2)]
        public short DvzSF3ValorGun { get; set; }

        [DbAlan("KayitTuru", SqlDbType.SmallInt, 2)]
        public short KayitTuru { get; set; }

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

        [DbAlan("Aciklama", SqlDbType.VarChar, 64)]
        public string Aciklama { get; set; }

        [DbAlan("Kod4", SqlDbType.VarChar, 10)]
        public string Kod4 { get; set; }

        [DbAlan("Kod5", SqlDbType.VarChar, 10)]
        public string Kod5 { get; set; }

        [DbAlan("Kod6", SqlDbType.VarChar, 10)]
        public string Kod6 { get; set; }

        [DbAlan("Kod7", SqlDbType.VarChar, 10)]
        public string Kod7 { get; set; }

        [DbAlan("Kod8", SqlDbType.VarChar, 20)]
        public string Kod8 { get; set; }

        [DbAlan("Kod9", SqlDbType.VarChar, 20)]
        public string Kod9 { get; set; }

        [DbAlan("Kod10", SqlDbType.VarChar, 20)]
        public string Kod10 { get; set; }

        [DbAlan("Kod11", SqlDbType.Decimal, 13)]
        public decimal Kod11 { get; set; }

        [DbAlan("Kod12", SqlDbType.Decimal, 13)]
        public decimal Kod12 { get; set; }

        [DbAlan("Row_ID", SqlDbType.Int, 4, true, false, false)]
        public int Row_ID { get; set; }

        #endregion DST_Column Names

        internal KKP_DST()
        {

        }
    }
}
