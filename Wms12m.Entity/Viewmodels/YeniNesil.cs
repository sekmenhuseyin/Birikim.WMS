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
        public string DovizCinsi { get; set; }
        public decimal? Tutar { get; set; }
        public string Tarih { get; set; }
        public string Kaydeden { get; set; }
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

    public class frmOnayTeklifList
    {
        /// <summary> VarChar(16) (Not Null) </summary>
        public string TeklifNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> Int (Allow Null) </summary>
        public int? Cesit { get; set; }
        /// <summary> Decimal(38,4) (Allow Null) </summary>
        public decimal? Miktar { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string KayitTarihi { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string TeklifTarihi { get; set; }


        public static string Sorgu = @"
        SELECT TeklifNo,HesapKodu,COUNT(MalKodu) AS Cesit,
SUM(Miktar) AS Miktar,Kaydeden,
CONVERT(VARCHAR(15), CAST(KayitTarih - 2 AS datetime), 104) AS KayitTarihi,
CONVERT(VARCHAR(15), CAST(TeklifTarihi - 2 AS datetime), 104) AS TeklifTarihi
  FROM [YNS0TEST].[YNS0TEST].[Teklif]
  GROUP BY TeklifNo,TeklifTarihi,HesapKodu,KayitTarih,Kaydeden";
    }

    public class frmOnayTeklifListDetay {
        /// <summary> VarChar(16) (Not Null) </summary>
        public string TeklifNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> NVarChar(40) (Allow Null) </summary>
        public string Unvan { get; set; }
        /// <summary> Decimal(17,4) (Not Null) </summary>
        public decimal Miktar { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> NVarChar(50) (Allow Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> Decimal(23,6) (Not Null) </summary>
        public decimal Fiyat { get; set; }
        /// <summary> Decimal(19,2) (Not Null) </summary>
        public decimal Tutar { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string KayitTarihi { get; set; }


        public static string Sorgu = @"
        SELECT TeklifNo,HesapKodu,
YNS0TEST.CAR002.CAR002_Unvan1 AS Unvan,
Miktar AS Miktar,Kaydeden,MalKodu,STK004_Aciklama AS MalAdi,
Fiyat,Tutar,DovizCinsi,
CONVERT(VARCHAR(15), CAST(KayitTarih - 2 AS datetime), 104) AS KayitTarihi
  FROM [YNS0TEST].[YNS0TEST].[Teklif] 
  INNER JOIN
  YNS0TEST.CAR002 ON HesapKodu = YNS0TEST.CAR002.CAR002_HesapKodu
  INNER JOIN
  YNS0TEST.STK004 ON MalKodu = YNS0TEST.STK004.STK004_MalKodu";
    }
}
