namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnaySiparisList
    {
        public int ID { get; set; }
        /// <summary> NVarChar(16) (Allow Null) </summary>
        public string EvrakSeriNo { get; set; }
        /// <summary> NVarChar(16) (Allow Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> NVarChar(40) (Allow Null) </summary>
        public string Unvan { get; set; }
        /// <summary> Int (Allow Null) </summary>
        public int? Cesit { get; set; }
        /// <summary> Decimal(38,4) (Allow Null) </summary>
        public decimal? Miktar { get; set; }
        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(800) (Not Null) </summary>
        public string Notlar { get; set; }
        /// <summary> NVarChar(30) (Allow Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> NVarChar(50) (Allow Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> NVarChar(10) (Allow Null) </summary>
        public string Depo { get; set; }
        /// <summary> Decimal(23,6) (Allow Null) </summary>
        public decimal? BirimFiyat { get; set; }
        /// <summary> Decimal(19,2) (Allow Null) </summary>
        public decimal? Tutar { get; set; }
        /// <summary> NVarChar(3) (Allow Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> Decimal(19,1) (Allow Null) </summary>
        public decimal? EsikFiyat { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string OnayRedTarih { get; set; }
        /// <summary> NVarChar(20) (Allow Null) </summary>
        public string OnaylayanReddeden { get; set; }



    }
}