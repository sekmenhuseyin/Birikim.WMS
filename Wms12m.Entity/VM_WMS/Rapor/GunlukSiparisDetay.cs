namespace Wms12m.Entity
{
    public class GunlukSiparisDetay
    {

        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BirimMiktar { get; set; }
        /// <summary> Decimal(10,2) (Allow Null) </summary>
        public decimal? IskontoOran1 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat3 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BirimFiyat { get; set; }
        /// <summary> Decimal(26,6) (Allow Null) </summary>
        public decimal? NetTutar { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short Valorgun { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal AlisFiyat1 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi4 { get; set; }


        public static string Sorgu = @"
        SELECT
          SPI.MalKodu,
          STK.MalAdi,
          SPI.BirimMiktar,
          CONVERT(DECIMAL(10,2),SPI.IskontoOran1) AS IskontoOran1,
          STK.SatisFiyat3,
          SPI.BirimFiyat,
          (SPI.Tutar - SPI.ToplamIskonto) AS NetTutar,
          SPI.Valorgun,
          STK.AlisFiyat1,
          CHK.TipKod,
          CHK.GrupKod,
          SPI.EvrakNo,
          SPI.Chk AS HesapKodu,
          CONCAT(CHK.Unvan1,SPACE(1),CHK.Unvan2) AS Unvan,
          STK.MalAdi4
          FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
          INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SPI.MALKODU 
          INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.CHK
          WHERE SPI.KynkEvrakTip=62 
          AND SPI.EvrakNo = LTRIM('{1}')
          ";


    }
}
