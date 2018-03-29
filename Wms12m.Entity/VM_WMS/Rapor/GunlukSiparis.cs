namespace Wms12m.Entity
{
    public class GunlukSiparis
    {
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EVRAKNO { get; set; }
        public decimal Tutar { get; set; }
        /// <summary> Int (Not Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string CHK { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TipKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string GrupKod { get; set; }

        public static string Sorgu = @"
                          SELECT    SPI.EVRAKNO,
SPI.Tutar as Tutar,
 CONVERT(VARCHAR(10),CONVERT(datetime,SPI.Tarih-2),104)as Tarih,
SPI.CHK,(CHK.Unvan1+' '+CHK.Unvan2) as Unvan,CHK.TipKod,CHK.GrupKod
          FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK)
          INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MALKODU = SPI.MALKODU 
          INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.CHK
          WHERE SPI.KynkEvrakTip=62
		  AND SPI.Tarih BETWEEN {1} AND {2}
          Group By SPI.EvrakNo,SPI.Tutar,SPI.Tarih, SPI.Chk, (CHK.Unvan1+' '+CHK.Unvan2), CHK.TipKod,CHK.GrupKod

                                   ";
    }
}