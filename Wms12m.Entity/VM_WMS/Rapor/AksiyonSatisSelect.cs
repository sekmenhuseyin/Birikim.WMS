namespace Wms12m.Entity
{
    public class AksiyonSatisSelect
    {
        public string StkMalKodu { get; set; }
        public string StkMaladi { get; set; }
        public string stkmaladi4 { get; set; }
        public string CHKGrupKod { get; set; }
        public string CHKTipKod { get; set; }
        public decimal? BirimMiktar { get; set; }
        public decimal? NetTutar { get; set; }
        public static string Sorgu= @"
            SELECT max(Stk.MalKodu)as StkMalKodu,max(Stk.Maladi)as StkMaladi ,max(stk.maladi4) as [stkmaladi4] ,CHK.GrupKod as CHKGrupKod, CHK.TipKod as CHKTipKod,
            SUM(BirimMiktar) as BirimMiktar, SUM(Tutar-ToplamIskonto) AS NetTutar   FROM FINSAT6{0}.FINSAT6{0}.SPI(NOLOCK) SPI
            INNER JOIN FINSAT6{0}.FINSAT6{0}.STK(NOLOCK)STK ON STK.Malkodu = SPI.Malkodu
            INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK(NOLOCK) CHK ON CHK.HesapKodu = SPI.Chk
            WHERE SPI.Kynkevraktip = 62 
            AND SPI.Tarih=DATEDIFF(DD,'1899-12-30',GETDATE())
            and stk.Kod13 = {1}
            GROUP BY  STK.Malkodu, CHK.GrupKod, CHK.TipKod
            ";
    }
}
