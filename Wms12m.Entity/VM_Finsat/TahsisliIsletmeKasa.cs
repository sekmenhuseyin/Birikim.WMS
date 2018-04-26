namespace Wms12m.Entity
{
    public class TahsisliIsletmeKasa
    {
        /// <summary> VarChar(16) (Allow Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string OrmIslt { get; set; }
        /// <summary> VarChar(40) (Allow Null) </summary>
        public string OrmIsltUnvan { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Yil { get; set; }
        /// <summary> Int (Not Null) </summary>
        public int Hafta { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahTopMektupTutar { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? TahPesinat { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? IbreliMiktarSter { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? IbreliMiktarM3 { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? YaprakliMiktarSter { get; set; }
        /// <summary> Decimal(18,2) (Allow Null) </summary>
        public decimal? YaprakliMiktarM3 { get; set; }
    }
}