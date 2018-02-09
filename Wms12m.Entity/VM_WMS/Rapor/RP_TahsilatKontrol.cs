using System;

namespace Wms12m.Entity
{
    public class RP_TahsilatKontrol
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string HesapKodu { get; set; }
        /// <summary> VarChar(81) (Allow Null) </summary>
        public string Unvan { get; set; }
        /// <summary> Decimal(36,6) (Allow Null) </summary>
        public decimal? Bakiye { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string SozlesmeSiraNo { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string BaglantiNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string BaglantiTipi { get; set; }
        /// <summary> Datetime (Allow Null) </summary>
        public DateTime? BaglantiBitisTarihi { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal BaglantiTutari { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string BaglantiParaCinsi { get; set; }
        /// <summary> Datetime (Allow Null) </summary>
        public DateTime? BaglantiTarihi { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string DevirTarih { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal DevirTutar { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal ToplamSevkEdilenTutar { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal DevirdenSonrakiSevkedilen { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal KalanTutar { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal BekleyenIrsaliye { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal BekleyenSiparis { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal CekTahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal NakitTahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal KKTahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal Havaletahsilati { get; set; }
        /// <summary> Decimal(38,6) (Not Null) </summary>
        public decimal ToplamTahsilat { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? FaturaBekleyenler { get; set; }
    }
}
