namespace Wms12m.Entity
{
    public class AksiyonSatis
    {
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string StkMaladi { get; set; }
        /// <summary> VarChar(50) (Allow Null) </summary>
        public string stkmaladi4 { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string CHKGrupKod { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string CHKTipKod { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? BirimMiktar { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? NetTutar { get; set; }

    }
}
