namespace Wms12m.Entity
{
    public class FaturaDetaySTI
    {
        /// <summary> VarChar(30) (Not Null) </summary>
        public string MalKodu { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string MalAdi { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string BaglantiKodu { get; set; }
        /// <summary> VarChar(4) (Not Null) </summary>
        public string Birim { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string BirimMiktar { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string BirimFiyat { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string Tutar { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string Depo { get; set; }
        /// <summary> Real (Not Null) </summary>
        public float KdvOran { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string KdvMuafiyetNedeni { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string DvzTL { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizKuru { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizTutar { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran1 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran2 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran3 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran4 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string IskontoOran5 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string NetTutar { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string Nesne1 { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public short CheckSum { get; set; }
    }
}