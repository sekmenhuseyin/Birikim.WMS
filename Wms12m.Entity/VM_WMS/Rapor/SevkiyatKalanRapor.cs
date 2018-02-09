namespace Wms12m.Entity
{
    public class SevkiyatKalanRapor
    {

        /// <summary> Char(10) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Depo { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? Miktar { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? KalanStok { get; set; }

    }
}
