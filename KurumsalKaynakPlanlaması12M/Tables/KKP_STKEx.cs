using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
    public class KKP_STKEx 
    {
        #region STK_Column Names

        [DbAlan("MalKodu", SqlDbType.VarChar, 30, false, true, false)]
        public string MalKodu { get; set; }

        [DbAlan("MalAdi", SqlDbType.VarChar, 50)]
        public string MalAdi { get; set; }

        [DbAlan("MalAdi2", SqlDbType.VarChar, 50)]
        public string MalAdi2 { get; set; }

        [DbAlan("MalAdi3", SqlDbType.VarChar, 50)]
        public string MalAdi3 { get; set; }

        [DbAlan("MalAdi4", SqlDbType.VarChar, 50)]
        public string MalAdi4 { get; set; }

        [DbAlan("MalAdi5", SqlDbType.VarChar, 50)]
        public string MalAdi5 { get; set; }

        [DbAlan("GrupKod", SqlDbType.VarChar, 20)]
        public string GrupKod { get; set; }

        [DbAlan("SirketWebAdres", SqlDbType.VarChar, 50)]
        public string SirketWebAdres { get; set; }

        [DbAlan("OzelKod", SqlDbType.VarChar, 20)]
        public string OzelKod { get; set; }

        [DbAlan("TipKod", SqlDbType.VarChar, 20)]
        public string TipKod { get; set; }

        [DbAlan("BarKod1", SqlDbType.VarChar, 52)]
        public string BarKod1 { get; set; }

        [DbAlan("BarKod2", SqlDbType.VarChar, 52)]
        public string BarKod2 { get; set; }

        [DbAlan("BarKod3", SqlDbType.VarChar, 52)]
        public string BarKod3 { get; set; }

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

        [DbAlan("Kod8", SqlDbType.VarChar, 20)]
        public string Kod8 { get; set; }

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

        [DbAlan("DovizCinsi", SqlDbType.VarChar, 3)]
        public string DovizCinsi { get; set; }

        [DbAlan("MalKodu2", SqlDbType.VarChar, 30)]
        public string MalKodu2 { get; set; }

        [DbAlan("Birim1", SqlDbType.VarChar, 4)]
        public string Birim1 { get; set; }

        [DbAlan("Birim2", SqlDbType.VarChar, 4)]
        public string Birim2 { get; set; }

        [DbAlan("Birim3", SqlDbType.VarChar, 4)]
        public string Birim3 { get; set; }

        [DbAlan("Operator2", SqlDbType.SmallInt, 2)]
        public short Operator2 { get; set; }

        [DbAlan("Operator3", SqlDbType.SmallInt, 2)]
        public short Operator3 { get; set; }

        [DbAlan("KatSayi2", SqlDbType.Float, 8)]
        public float KatSayi2 { get; set; }

        [DbAlan("KatSayi3", SqlDbType.Float, 8)]
        public float KatSayi3 { get; set; }

        [DbAlan("UreticiKodu", SqlDbType.VarChar, 30)]
        public string UreticiKodu { get; set; }

        [DbAlan("FireliMalKodu", SqlDbType.VarChar, 30)]
        public string FireliMalKodu { get; set; }

        [DbAlan("MlySekli", SqlDbType.SmallInt, 2)]
        public short MlySekli { get; set; }

        [DbAlan("MKDS", SqlDbType.SmallInt, 2)]
        public short MKDS { get; set; }

        [DbAlan("ValorGun", SqlDbType.SmallInt, 2)]
        public short ValorGun { get; set; }

        [DbAlan("IskontoOran", SqlDbType.Real, 4)]
        public double IskontoOran { get; set; }

        [DbAlan("KDVOran", SqlDbType.Real, 4)]
        public double KDVOran { get; set; }

        [DbAlan("Fire", SqlDbType.Real, 4)]
        public double Fire { get; set; }

        [DbAlan("TeminYeri", SqlDbType.VarChar, 20)]
        public string TeminYeri { get; set; }

        [DbAlan("TeminSuresi", SqlDbType.SmallInt, 2)]
        public short TeminSuresi { get; set; }

        [DbAlan("KritikStok", SqlDbType.Decimal, 13)]
        public decimal KritikStok { get; set; }

        [DbAlan("AzamiStok", SqlDbType.Decimal, 13)]
        public decimal AzamiStok { get; set; }

        [DbAlan("TahminiStok", SqlDbType.Decimal, 13)]
        public decimal TahminiStok { get; set; }

        [DbAlan("SatislarHesabi", SqlDbType.VarChar, 50)]
        public string SatislarHesabi { get; set; }

        [DbAlan("AlimlarHesabi", SqlDbType.VarChar, 50)]
        public string AlimlarHesabi { get; set; }

        [DbAlan("SatistanIade", SqlDbType.VarChar, 50)]
        public string SatistanIade { get; set; }

        [DbAlan("AlimdanIade", SqlDbType.VarChar, 50)]
        public string AlimdanIade { get; set; }

        [DbAlan("MasrafMerkezi", SqlDbType.VarChar, 20)]
        public string MasrafMerkezi { get; set; }

        [DbAlan("AlisFiyat1", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat1 { get; set; }

        [DbAlan("AlisFiyat2", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat2 { get; set; }

        [DbAlan("AlisFiyat3", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat3 { get; set; }

        [DbAlan("DovizAlisFiyat1", SqlDbType.Decimal, 13)]
        public decimal DovizAlisFiyat1 { get; set; }

        [DbAlan("DovizAlisFiyat2", SqlDbType.Decimal, 13)]
        public decimal DovizAlisFiyat2 { get; set; }

        [DbAlan("DovizAlisFiyat3", SqlDbType.Decimal, 13)]
        public decimal DovizAlisFiyat3 { get; set; }

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

        [DbAlan("DovizSatisFiyat1", SqlDbType.Decimal, 13)]
        public decimal DovizSatisFiyat1 { get; set; }

        [DbAlan("DovizSatisFiyat2", SqlDbType.Decimal, 13)]
        public decimal DovizSatisFiyat2 { get; set; }

        [DbAlan("DovizSatisFiyat3", SqlDbType.Decimal, 13)]
        public decimal DovizSatisFiyat3 { get; set; }

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

        [DbAlan("DvrTarih", SqlDbType.Int, 4)]
        public int DvrTarih { get; set; }

        [DbAlan("DvrMiktar", SqlDbType.Decimal, 13)]
        public decimal DvrMiktar { get; set; }

        [DbAlan("DvrTutar", SqlDbType.Decimal, 13)]
        public decimal DvrTutar { get; set; }

        [DbAlan("GirMiktar", SqlDbType.Decimal, 13)]
        public decimal GirMiktar { get; set; }

        [DbAlan("GirTutar", SqlDbType.Decimal, 13)]
        public decimal GirTutar { get; set; }

        [DbAlan("GirIskonto", SqlDbType.Decimal, 13)]
        public decimal GirIskonto { get; set; }

        [DbAlan("GirTarih", SqlDbType.Int, 4)]
        public int GirTarih { get; set; }

        [DbAlan("CikMiktar", SqlDbType.Decimal, 13)]
        public decimal CikMiktar { get; set; }

        [DbAlan("CikTutar", SqlDbType.Decimal, 13)]
        public decimal CikTutar { get; set; }

        [DbAlan("CikIskonto", SqlDbType.Decimal, 13)]
        public decimal CikIskonto { get; set; }

        [DbAlan("CikTarih", SqlDbType.Int, 4)]
        public int CikTarih { get; set; }

        [DbAlan("DvzDvrTutar", SqlDbType.Decimal, 13)]
        public decimal DvzDvrTutar { get; set; }

        [DbAlan("DvzGirTutar", SqlDbType.Decimal, 13)]
        public decimal DvzGirTutar { get; set; }

        [DbAlan("DvzGirIskTutar", SqlDbType.Decimal, 13)]
        public decimal DvzGirIskTutar { get; set; }

        [DbAlan("DvzCikTutar", SqlDbType.Decimal, 13)]
        public decimal DvzCikTutar { get; set; }

        [DbAlan("DvzCikIskTutar", SqlDbType.Decimal, 13)]
        public decimal DvzCikIskTutar { get; set; }

        [DbAlan("SonMlySekli", SqlDbType.SmallInt, 2)]
        public short SonMlySekli { get; set; }

        [DbAlan("SonMlyBirimFiyat", SqlDbType.Decimal, 13)]
        public decimal SonMlyBirimFiyat { get; set; }

        [DbAlan("SonMlyTarih", SqlDbType.Int, 4)]
        public int SonMlyTarih { get; set; }

        [DbAlan("SonAlimFatTarih", SqlDbType.Int, 4)]
        public int SonAlimFatTarih { get; set; }

        [DbAlan("SonAlimEvrakNo", SqlDbType.VarChar, 16)]
        public string SonAlimEvrakNo { get; set; }

        [DbAlan("SonAlimBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimBF { get; set; }

        [DbAlan("SonAlimCHK", SqlDbType.VarChar, 20)]
        public string SonAlimCHK { get; set; }

        [DbAlan("AlimSiparis", SqlDbType.Decimal, 13)]
        public decimal AlimSiparis { get; set; }

        [DbAlan("SatisSiparis", SqlDbType.Decimal, 13)]
        public decimal SatisSiparis { get; set; }

        [DbAlan("GumrukFon", SqlDbType.Decimal, 13)]
        public decimal GumrukFon { get; set; }

        [DbAlan("GumrukGTIPN", SqlDbType.VarChar, 30)]
        public string GumrukGTIPN { get; set; }

        [DbAlan("GumrukVergi", SqlDbType.Real, 4)]
        public double GumrukVergi { get; set; }

        [DbAlan("GirRezervasyon", SqlDbType.Decimal, 13)]
        public decimal GirRezervasyon { get; set; }

        [DbAlan("CikRezervasyon", SqlDbType.Decimal, 13)]
        public decimal CikRezervasyon { get; set; }

        [DbAlan("GirKonsinye", SqlDbType.Decimal, 13)]
        public decimal GirKonsinye { get; set; }

        [DbAlan("CikKonsinye", SqlDbType.Decimal, 13)]
        public decimal CikKonsinye { get; set; }

        [DbAlan("Nesne1", SqlDbType.VarChar, 254)]
        public string Nesne1 { get; set; }

        [DbAlan("Nesne2", SqlDbType.VarChar, 254)]
        public string Nesne2 { get; set; }

        [DbAlan("Nesne3", SqlDbType.VarChar, 254)]
        public string Nesne3 { get; set; }

        [DbAlan("ButceKodu", SqlDbType.VarChar, 50)]
        public string ButceKodu { get; set; }

        /// <summary>
        ///   0-StokKartı, 1-KarmaKoli
        /// </summary>
        [DbAlan("KartTuru", SqlDbType.SmallInt, 2)]
        public short KartTuru { get; set; }

        [DbAlan("UseSatRezervasyon", SqlDbType.SmallInt, 2)]
        public short UseSatRezervasyon { get; set; }

        [DbAlan("UseSatSiparis", SqlDbType.SmallInt, 2)]
        public short UseSatSiparis { get; set; }

        [DbAlan("UseSatFatIrs", SqlDbType.SmallInt, 2)]
        public short UseSatFatIrs { get; set; }

        [DbAlan("UseCikisIslem", SqlDbType.SmallInt, 2)]
        public short UseCikisIslem { get; set; }

        [DbAlan("UseSetUrun", SqlDbType.SmallInt, 2)]
        public short UseSetUrun { get; set; }

        [DbAlan("UseAlimRezervasyon", SqlDbType.SmallInt, 2)]
        public short UseAlimRezervasyon { get; set; }

        [DbAlan("UseAlimSiparis", SqlDbType.SmallInt, 2)]
        public short UseAlimSiparis { get; set; }

        [DbAlan("UseAlimIrsFat", SqlDbType.SmallInt, 2)]
        public short UseAlimIrsFat { get; set; }

        [DbAlan("UseGirisIslem", SqlDbType.SmallInt, 2)]
        public short UseGirisIslem { get; set; }

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

        [DbAlan("SF1DvzValorGun", SqlDbType.SmallInt, 2)]
        public short SF1DvzValorGun { get; set; }

        [DbAlan("SF2DvzValorGun", SqlDbType.SmallInt, 2)]
        public short SF2DvzValorGun { get; set; }

        [DbAlan("SF3DvzValorGun", SqlDbType.SmallInt, 2)]
        public short SF3DvzValorGun { get; set; }

        [DbAlan("SatisFiyatTip", SqlDbType.SmallInt, 2)]
        public short SatisFiyatTip { get; set; }

        [DbAlan("SatisFiyatAltLimit", SqlDbType.Decimal, 13)]
        public decimal SatisFiyatAltLimit { get; set; }

        [DbAlan("SatisFiyatUstLimit", SqlDbType.Decimal, 13)]
        public decimal SatisFiyatUstLimit { get; set; }

        [DbAlan("SonSayimTarih", SqlDbType.Int, 4)]
        public int SonSayimTarih { get; set; }

        [DbAlan("SonSayimSonuc", SqlDbType.Decimal, 13)]
        public decimal SonSayimSonuc { get; set; }

        [DbAlan("SonSayimFark", SqlDbType.Decimal, 13)]
        public decimal SonSayimFark { get; set; }

        [DbAlan("Notlar", SqlDbType.VarChar, 64)]
        public string Notlar { get; set; }

        [DbAlan("BlkMiktar", SqlDbType.Decimal, 13)]
        public decimal BlkMiktar { get; set; }

        [DbAlan("ElekTicSitList", SqlDbType.SmallInt, 2)]
        public short ElekTicSitList { get; set; }

        [DbAlan("WebMagSitList", SqlDbType.SmallInt, 2)]
        public short WebMagSitList { get; set; }

        [DbAlan("DagZinSitList", SqlDbType.SmallInt, 2)]
        public short DagZinSitList { get; set; }

        [DbAlan("SirIciSipSitList", SqlDbType.SmallInt, 2)]
        public short SirIciSipSitList { get; set; }

        [DbAlan("ElekTicSitListAdi", SqlDbType.SmallInt, 2)]
        public short ElekTicSitListAdi { get; set; }

        [DbAlan("WebMagSitListAdi", SqlDbType.SmallInt, 2)]
        public short WebMagSitListAdi { get; set; }

        [DbAlan("DagZinSitListAdi", SqlDbType.SmallInt, 2)]
        public short DagZinSitListAdi { get; set; }

        [DbAlan("SirIciSipSitListAdi", SqlDbType.SmallInt, 2)]
        public short SirIciSipSitListAdi { get; set; }

        [DbAlan("DemirbasKodu", SqlDbType.VarChar, 20)]
        public string DemirbasKodu { get; set; }

        /// <summary>
        ///    0-Miktarsız, 1-Miktarlı
        /// </summary>
        [DbAlan("MiktarTakibi", SqlDbType.SmallInt, 2)]
        public short MiktarTakibi { get; set; }

        /// <summary>
        ///        0-YTL/TL, 1-Döviz, 2-2005 Öncesi TL
        /// </summary>
        [DbAlan("BakGostSekli", SqlDbType.SmallInt, 2)]
        public short BakGostSekli { get; set; }

        /// <summary>
        /// 0-Ticari Mal, 1-Mamül, 2-Yarı Mamül, 3-Sarf Malzemesi, 4-Sabit Kıymet,
        /// 5-Hizmet, 6-Masraf, 7-Vade Farkı/ Kur Farkı, 8-Diğer
        /// </summary>
        [DbAlan("KartTip", SqlDbType.SmallInt, 2)]
        public short KartTip { get; set; }

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

        [DbAlan("AlimTeklif", SqlDbType.Decimal, 13)]
        public decimal AlimTeklif { get; set; }

        [DbAlan("SatisTeklif", SqlDbType.Decimal, 13)]
        public decimal SatisTeklif { get; set; }

        [DbAlan("SatMalMaliyetHesap", SqlDbType.VarChar, 50)]
        public string SatMalMaliyetHesap { get; set; }

        /// <summary>
        /// 0-Pasif, 1-Aktif
        /// </summary>
        [DbAlan("AktifPasif", SqlDbType.SmallInt, 2)]
        public short AktifPasif { get; set; }

        [DbAlan("TevfikatOran", SqlDbType.SmallInt, 2)]
        public short TevfikatOran { get; set; }

        [DbAlan("SonAlimNetBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimNetBF { get; set; }

        [DbAlan("SonAlimDvzBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimDvzBF { get; set; }

        [DbAlan("SonAlimDvzNetBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimDvzNetBF { get; set; }

        [DbAlan("YDAlimlarHesabi", SqlDbType.VarChar, 50)]
        public string YDAlimlarHesabi { get; set; }

        [DbAlan("TevkifatAlis", SqlDbType.VarChar, 7)]
        public string TevkifatAlis { get; set; }

        [DbAlan("TevkifatSatis", SqlDbType.VarChar, 7)]
        public string TevkifatSatis { get; set; }

        [DbAlan("TevkifatAlisTam", SqlDbType.VarChar, 5)]
        public string TevkifatAlisTam { get; set; }

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

        [DbAlan("Row_ID", SqlDbType.Int, 4, true, false, false)]
        public int Row_ID { get; set; }
        #endregion

        internal KKP_STKEx()
        {

        }

    }

}
