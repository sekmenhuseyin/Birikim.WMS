namespace Wms12m.Entity
{
    public class BekleyenMalz
    {
        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> SmallInt (Not Null) </summary>
        public short ValorGun { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal Siparis { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Depo { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal TeslimEdilen { get; set; }
        /// <summary> Real (Not Null) </summary>
        public float IskontoOran1 { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BirimFiyat { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal ToplamIskonto { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? BekleyenNetTutar { get; set; }
        /// <summary> Decimal(27,6) (Allow Null) </summary>
        public decimal? MevcutStok { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal AlimSiparis { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal SatisFiyat3 { get; set; }
        /// <summary> Decimal(27,6) (Allow Null) </summary>
        public decimal? KalanMiktar { get; set; }

        public static string Sorgu = @"
                                    SELECT 
                                    REPLACE(CONVERT(NVARCHAR(10),DATEADD(DD,SPI.Tarih,'1899-12-30'),104),'.','-') AS Tarih,
                                    SPI.MalKodu,
                                    STK.MalAdi,
                                    SPI.EvrakNo,
                                    SPI.ValorGun,
                                    SPI.BirimMiktar AS Siparis,
                                    SPI.Depo,
                                    SPI.TeslimMiktar AS TeslimEdilen,
                                    SPI.IskontoOran1,
								    SPI.BirimFiyat,
									SPI.ToplamIskonto,
                                    ((SPI.Tutar-SPI.ToplamIskonto)/SPI.BirimMiktar)*(SPI.BirimMiktar-SPI.TeslimMiktar-SPI.KapatilanMiktar) AS BekleyenNetTutar,
                                    (STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar) AS MevcutStok,
                                    STK.AlimSiparis AS AlimSiparis,
									STK.SatisFiyat3,
									(SPI.BirimMiktar - (SPI.KapatilanMiktar + SPI.TeslimMiktar)) AS KalanMiktar
                                    FROM FINSAT6{0}.FINSAT6{0}.SPI AS SPI WITH (NOLOCK) 
                                    INNER JOIN FINSAT6{0}.FINSAT6{0}.STK AS STK WITH (NOLOCK) ON STK.MalKodu=SPI.MalKodu 
                                    WHERE SPI.Kynkevraktip=62 AND SPI.SiparisDurumu=0 
									AND SPI.Chk='{1}'
                                    ";
    }
}