namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class SatisIadeIcmal
    {
        /// <summary>Parametreler : ŞirketKodu </summary>
        public static string Sorgu = @"
        SELECT STK005_EvrakSeriNo as IadeNo, CONVERT(VARCHAR, CAST(STK005_IslemTarihi - 2 AS datetime), 104) as IadeTarih,
                STK005_Kod8 as HesapKodu, MIN(CAR002_Unvan1) as Unvan, STK005_Depo as Depo, COUNT(STK005_Row_ID) as Cesit, SUM(STK005_Miktari) as Miktar,
                STK005_Kod9 as FaturaNo, CONVERT(VARCHAR, CAST(STK005_IslemTarihi - 2 AS datetime), 104) as FaturaTarih, STK005_GirenKodu as Kaydeden,
	            STK005_EvrakSeriNo+','+CAST(STK005_IslemTarihi as varchar)+','+STK005_Kod9+','+CAST(MAX(CAST(STK005_Kod11 as INT)) as varchar) as EvrakID
        FROM  YNS{0}.YNS{0}.STK005(NOLOCK)
        LEFT JOIN YNS{0}.YNS{0}.CAR002(NOLOCK) ON STK005_Kod8=CAR002_HesapKodu
        WHERE STK005_EvrakTipi=22 AND STK005_IslemTipi=2 AND STK005_GC=0 AND STK005_Kod11>0 AND STK005_Kod9<>'' AND
                STK005_Kod10='Onay Bekliyor' AND SUBSTRING(STK005_Not5,1,8)='AndMobil'
        GROUP BY STK005_EvrakSeriNo, STK005_IslemTarihi, STK005_Kod8, STK005_Depo, STK005_Kod9, STK005_GirenKodu";

        public int Cesit { get; set; }
        public string Depo { get; set; }
        public string EvrakID { get; set; }
        public string FaturaNo { get; set; }
        public string FaturaTarih { get; set; }
        public string HesapKodu { get; set; }
        public string IadeNo { get; set; }
        public string IadeTarih { get; set; }
        public string Kaydeden { get; set; }
        public decimal Miktar { get; set; }
        public string Unvan { get; set; }
    }
}