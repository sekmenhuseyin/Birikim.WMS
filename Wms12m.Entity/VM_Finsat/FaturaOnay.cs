namespace Wms12m.Entity
{
    public class FaturaOnay
    {
        /// <summary> VarChar(14) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(128) (Not Null) </summary>
        public string Not1 { get; set; }
        /// <summary> VarChar(82) (Allow Null) </summary>
        public string Unvan { get; set; }
    }
}