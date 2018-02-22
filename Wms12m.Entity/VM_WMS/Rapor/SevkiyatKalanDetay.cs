namespace Wms12m.Entity
{
    public class SevkiyatKalanDetay
    {
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Depo { get; set; }
        /// <summary> VarChar(40) (Not Null) </summary>
        public string DepoAdi { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DvrMiktar { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal GirMiktar { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal CikMiktar { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal Sevkiyat { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? StokMiktar { get; set; }

    }
}
