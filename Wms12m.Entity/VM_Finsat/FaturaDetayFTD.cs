namespace Wms12m.Entity
{
    public class FaturaDetayFTD
    {
        /// <summary> VarChar(64) (Not Null) </summary>
        public string Tip { get; set; }
        /// <summary> VarChar(64) (Not Null) </summary>
        public string Aciklama { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string Iskonto { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string DvzTL { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizKuru { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizTutar { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string KdvMuafiyetNedeni { get; set; }
    }
}