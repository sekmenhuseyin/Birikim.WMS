namespace Wms12m.Entity
{
    public class SatisIadeDetay
    {
        /// <summary>Parametreler : ŞirketKodu, EvrakNo, Tarih</summary>
        public static string Sorgu = @"
        SELECT STI1.STK005_EvrakSeriNo as IadeNo, CONVERT(VARCHAR, CAST(STI1.STK005_IslemTarihi - 2 AS datetime), 104) as IadeTarih,
            STI1.STK005_Kod8 as HesapKodu, CAR002_Unvan1 as Unvan, STI1.STK005_Depo as Depo,
            STI1.STK005_MalKodu as MalKodu, STK004_Aciklama as MalAdi, STI1.STK005_Miktari as Miktar, STK004_Birim1 as Birim,
            STI2.STK005_BirimFiyati as Fiyat, STI2.STK005_DovizCinsi as DovizCinsi,
            (STI1.STK005_Miktari * STI2.STK005_BirimFiyati) as Tutar, STI1.STK005_Kod9 as FaturaNo,
            CONVERT(VARCHAR, CAST(STI1.STK005_IslemTarihi - 2 AS datetime), 104) as FaturaTarih, STI1.STK005_GirenKodu as Kaydeden,
            STI1.STK005_ROW_ID AS ID
        FROM  YNS{0}.YNS{0}.STK005(NOLOCK) STI1
        LEFT JOIN YNS{0}.YNS{0}.STK005(NOLOCK) STI2 ON STI1.STK005_Kod9=STI2.STK005_EvrakSeriNo AND
            CAST(STI1.STK005_Kod11 as INT)=STI2.STK005_Row_ID AND  STI2.STK005_EvrakTipi=11 AND STI1.STK005_MalKodu=STI2.STK005_MalKodu
        LEFT JOIN YNS{0}.YNS{0}.CAR002(NOLOCK) ON STI1.STK005_Kod8=CAR002_HesapKodu
        LEFT JOIN YNS{0}.YNS{0}.STK004(NOLOCK) ON STI1.STK005_MalKodu=STK004_MalKodu
        WHERE STI1.STK005_EvrakTipi=22 AND STI1.STK005_IslemTipi=2 AND STI1.STK005_GC=0 AND STI1.STK005_Kod11>0 AND STI1.STK005_Kod9<>'' AND
            STI1.STK005_Kod10='Onay Bekliyor' AND SUBSTRING(STI1.STK005_Not5,1,8)='AndMobil' AND
            STI1.STK005_EvrakSeriNo='{1}' AND STI1.STK005_IslemTarihi='{2}'";

        /// <summary> NVarChar(5) (Allow Null) </summary>
        public string Birim { get; set; }

        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Depo { get; set; }

        /// <summary> NVarChar(3) (Allow Null) </summary>
        public string DovizCinsi { get; set; }

        /// <summary> NVarChar(20) (Allow Null) </summary>
        public string FaturaNo { get; set; }

        /// <summary> VarChar(30) (Allow Null) </summary>
        public string FaturaTarih { get; set; }

        /// <summary> Decimal(23,6) (Allow Null) </summary>
        public decimal? Fiyat { get; set; }

        /// <summary> NVarChar(20) (Allow Null) </summary>
        public string HesapKodu { get; set; }

        /// <summary> NVarChar(16) (Allow Null) </summary>
        public string IadeNo { get; set; }

        /// <summary> VarChar(30) (Allow Null) </summary>
        public string IadeTarih { get; set; }

        public int ID { get; set; }

        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Kaydeden { get; set; }

        /// <summary> NVarChar(50) (Allow Null) </summary>
        public string MalAdi { get; set; }

        /// <summary> NVarChar(30) (Allow Null) </summary>
        public string MalKodu { get; set; }

        /// <summary> Decimal(17,4) (Allow Null) </summary>
        public decimal? Miktar { get; set; }

        /// <summary> Decimal(38,7) (Allow Null) </summary>
        public decimal? Tutar { get; set; }

        /// <summary> NVarChar(40) (Allow Null) </summary>
        public string Unvan { get; set; }
    }
}