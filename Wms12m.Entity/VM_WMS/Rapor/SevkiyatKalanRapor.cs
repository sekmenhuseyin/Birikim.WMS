namespace Wms12m.Entity
{
    public class SevkiyatKalanRapor
    {

        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Not Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim1 { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? DvrMiktar { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? GirMiktar { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? CikMiktar { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? Sevkiyat { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? StokMiktar { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BekleyenSiparis { get; set; }


    }
}
