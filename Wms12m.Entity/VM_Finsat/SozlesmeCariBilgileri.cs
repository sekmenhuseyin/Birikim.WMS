namespace Wms12m.Entity
{
    public class SozlesmeCariBilgileri
    {
        /// <summary> Decimal(36,6) (Allow Null) </summary>
        public decimal? Bakiye { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? ToplamBakiye { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal KrediLimiti { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string AlinanBordroOrtalamaVade { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string OrtalamaVade { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? BekleyenSiparisTutar { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? AlinanBordro { get; set; }
    }
}