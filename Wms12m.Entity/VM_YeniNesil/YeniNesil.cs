namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnaySiparisList
    {
        public int ID { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string EvrakSeriNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Depo { get; set; }
        public int Cesit { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? BirimFiyat { get; set; }
        public decimal? EsikFiyat { get; set; }
        public string DovizCinsi { get; set; }
        public decimal? Tutar { get; set; }
        public string Tarih { get; set; }
        public string Kaydeden { get; set; }

        public string Notlar { get; set; }
    }
    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class frmOnayTransferList
    {
        public int ID { get; set; }
        public string TransferNo { get; set; }
        public int TransferTarihi { get; set; }
        public string Tarih { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string CikisDepo { get; set; }
        public string GirisDepo { get; set; }
        public string Kaydeden { get; set; }
        public int KayitTarih { get; set; }
    }
    /// <summary>
    /// teklif onay
    /// </summary>
    public class frmOnayTeklifList
    {
        public string TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public int? Cesit { get; set; }
        public decimal? Miktar { get; set; }
        public string Kaydeden { get; set; }
        public string KayitTarihi { get; set; }
        public string TeklifTarihi { get; set; }
    }
    /// <summary>
    /// teklif detay
    /// </summary>
    public class frmOnayTeklifListDetay
    {
        public string TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal Miktar { get; set; }
        public string Kaydeden { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Fiyat { get; set; }
        public decimal EsikFiyat { get; set; }
        public decimal Tutar { get; set; }
        public string DovizCinsi { get; set; }
        public string KayitTarihi { get; set; }
    }
    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class frmOnayFatura
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public short SiraNo { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Depo { get; set; }
        public int Cesit { get; set; }
        public decimal Miktar { get; set; }
        public decimal Fiyat { get; set; }
        public decimal EsikFiyat { get; set; }
        public string ParaCinsi { get; set; }
        public string Birim { get; set; }
        public short IslemDurumu { get; set; }
        public string Kaydeden { get; set; }
        public string KayitTarih { get; set; }
        public string KayitSaat { get; set; }
        public string Degistiren { get; set; }
        public string DegisTarih { get; set; }
        public string DegisSaat { get; set; }

        public string Notlar { get; set; }
    }

    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class SatisIadeIcmal
    {
        public string IadeNo { get; set; }
        public string IadeTarih { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string Depo { get; set; }
        public int Cesit { get; set; }
        public decimal Miktar { get; set; }
        public string FaturaNo { get; set; }
        public string FaturaTarih { get; set; }
        public string Kaydeden { get; set; }
        public string EvrakID { get; set; }

        /// <summary>Parametreler : ŞirketKodu </summary>
        public static string Sorgu = @"
        SELECT STK005_EvrakSeriNo as IadeNo, CONVERT(VARCHAR, CAST(STK005_IslemTarihi - 2 AS datetime), 104) as IadeTarih,
                STK005_Kod8 as HesapKodu, MIN(CAR002_Unvan1) as Unvan, STK005_Depo as Depo, COUNT(STK005_Row_ID) as Cesit, SUM(STK005_Miktari) as Miktar, 
                STK005_Kod9 as FaturaNo, CONVERT(VARCHAR, CAST(STK005_Kod11 - 2 AS datetime), 104) as FaturaTarih, STK005_GirenKodu as Kaydeden,
	            STK005_EvrakSeriNo+','+CAST(STK005_IslemTarihi as varchar)+','+STK005_Kod9+','+CAST(CAST(STK005_Kod11 as INT) as varchar) as EvrakID
        FROM  YNS{0}.YNS{0}.STK005(NOLOCK)
        LEFT JOIN YNS{0}.YNS{0}.CAR002(NOLOCK) ON STK005_Kod8=CAR002_HesapKodu 
        WHERE STK005_EvrakTipi=99 AND STK005_IslemTipi=2 AND STK005_GC=0 AND STK005_Kod11>0 AND STK005_Kod9<>'' AND
                STK005_Kod10='Onay Bekliyor' AND SUBSTRING(STK005_Not5,1,8)='AndMobil'
        GROUP BY STK005_EvrakSeriNo, STK005_IslemTarihi, STK005_Kod8, STK005_Depo, STK005_Kod9, STK005_Kod11, STK005_GirenKodu";
    }

    public class SatisIadeOnay
    {
        public bool Onay { get; set; }
        public string IadeNo { get; set; }
        public string IadeTarih { get; set; }
        public string Kaydeden { get; set; }
    }

    #region SatisIadeDetay Class
    public class SatisIadeDetay
    {
        /// <summary> NVarChar(16) (Allow Null) </summary>
        public string IadeNo { get; set; }
        /// <summary> VarChar(30) (Allow Null) </summary>
        public string IadeTarih { get; set; }
        /// <summary> NVarChar(20) (Allow Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> NVarChar(40) (Allow Null) </summary>
        public string Unvan { get; set; }
        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Depo { get; set; }
        /// <summary> NVarChar(30) (Allow Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> NVarChar(50) (Allow Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> Decimal(17,4) (Allow Null) </summary>
        public decimal? Miktar { get; set; }
        /// <summary> NVarChar(5) (Allow Null) </summary>
        public string Birim { get; set; }
        /// <summary> Decimal(23,6) (Allow Null) </summary>
        public decimal? Fiyat { get; set; }
        /// <summary> NVarChar(3) (Allow Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> Decimal(38,7) (Allow Null) </summary>
        public decimal? Tutar { get; set; }
        /// <summary> NVarChar(20) (Allow Null) </summary>
        public string FaturaNo { get; set; }
        /// <summary> VarChar(30) (Allow Null) </summary>
        public string FaturaTarih { get; set; }
        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Kaydeden { get; set; }

        /// <summary>Parametreler : ŞirketKodu, EvrakNo, Tarih</summary>
        public static string Sorgu = @"
        SELECT STI1.STK005_EvrakSeriNo as IadeNo, CONVERT(VARCHAR, CAST(STI1.STK005_IslemTarihi - 2 AS datetime), 104) as IadeTarih,
            STI1.STK005_Kod8 as HesapKodu, CAR002_Unvan1 as Unvan, STI1.STK005_Depo as Depo, 
            STI1.STK005_MalKodu as MalKodu, STK004_Aciklama as MalAdi, STI1.STK005_Miktari as Miktar, STK004_Birim1 as Birim,
            STI2.STK005_BirimFiyati as Fiyat, STI2.STK005_DovizCinsi as DovizCinsi,
            (STI1.STK005_Miktari * STI2.STK005_BirimFiyati) as Tutar, STI1.STK005_Kod9 as FaturaNo, 
            CONVERT(VARCHAR, CAST(STI1.STK005_Kod11 - 2 AS datetime), 104) as FaturaTarih, STI1.STK005_GirenKodu as Kaydeden
        FROM  YNS{0}.YNS{0}.STK005(NOLOCK) STI1
        LEFT JOIN YNS{0}.YNS{0}.STK005(NOLOCK) STI2 ON STI1.STK005_Kod9=STI2.STK005_EvrakSeriNo AND 
            CAST(STI1.STK005_Kod11 as INT)=STI2.STK005_Row_ID AND  STI2.STK005_EvrakTipi=11 AND STI1.STK005_MalKodu=STI2.STK005_MalKodu
        LEFT JOIN YNS{0}.YNS{0}.CAR002(NOLOCK) ON STI1.STK005_Kod8=CAR002_HesapKodu
        LEFT JOIN YNS{0}.YNS{0}.STK004(NOLOCK) ON STI1.STK005_MalKodu=STK004_MalKodu 
        WHERE STI1.STK005_EvrakTipi=99 AND STI1.STK005_IslemTipi=2 AND STI1.STK005_GC=0 AND STI1.STK005_Kod11>0 AND STI1.STK005_Kod9<>'' AND
            STI1.STK005_Kod10='Onay Bekliyor' AND SUBSTRING(STI1.STK005_Not5,1,8)='AndMobil' AND 
            STI1.STK005_EvrakSeriNo='{1}' AND STI1.STK005_IslemTarihi='{2}'";
    }
    #endregion /// SatisIadeDetay Class

    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnayTahsilatList
    {
        public string TahsilatNo { get; set; }
        public string Tarih { get; set; }

        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string IslemTuru { get; set; }
        public string OdemeTuru { get; set; }

        public decimal Tutar { get; set; }
        public string DovizCinsi { get; set; }

        public decimal KapatilanTL { get; set; }
        public decimal KapatilanUSD { get; set; }
        public decimal KapatilanEUR { get; set; }

        public string Kaydeden { get; set; }
        public string Aciklama { get; set; }
    }
    /// <summary>
    /// fatura kayıt işlemleri için
    /// </summary>
    public class SepetUrun
    {
        public string KullaniciKodu { get; set; }
        public string HesapKodu { get; set; }
        public string UrunKodu { get; set; }
        public string Depo { get; set; }
        public string ParaCinsi { get; set; }
        public string Birim { get; set; }
        public string Fiyat { get; set; }
        public string Miktar { get; set; }
        public int SatirTip { get; set; }  ///1 Fatura 2 Sipariş 3 Teklif
        public int OnayaDusme { get; set; }  ///0 Normal  1 Onaya Düşecek
        public string Adres1 { get; set; }
        public string Adres2 { get; set; }
        public string Adres3 { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }

        public string Kaydeden { get; set; }
    }

    public class DepoTran
    {
        public string KullaniciKodu { get; set; }
        public string MalKodu { get; set; }
        public string GirisDepo { get; set; }
        public string CikisDepo { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public int MiatTarih { get; set; }
    }

    public class HesapItem
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string BankaHesapKodu { get; set; }
        public string ParaBirimi { get; set; }
        public static string Sorgu = @"
        SELECT CAR002_HesapKodu as HesapKodu, CAR002_Unvan1+' '+CAR002_Unvan2 as Unvan,
               CAR002_BankaHesapKodu as BankaHesapKodu, CAR002_ParaBirimi as ParaBirimi   
        FROM  YNS{{0}}.YNS{{0}}.CAR002(NOLOCK)
        WHERE CAR002_AktifFlag=1 AND CAR002_BankaHesapKodu='{0}'
        ";
    }
    public class STIEvrakBilgi
    {
        public int Tip { get; set; }
        public string EvrakNo { get; set; }
        public int SiraNo { get; set; }
    }
}