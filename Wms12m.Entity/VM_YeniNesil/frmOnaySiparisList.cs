namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnaySiparisList
    {
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
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string OnayRedTarih { get; set; }
        /// <summary> NVarChar(20) (Allow Null) </summary>
        public string OnaylayanReddeden { get; set; }
    }
}