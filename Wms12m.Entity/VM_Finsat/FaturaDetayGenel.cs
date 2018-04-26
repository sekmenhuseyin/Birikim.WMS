namespace Wms12m.Entity
{
    public class FaturaDetayGenel
    {
        /// <summary> VarChar(20) (Not Null) </summary>
        public string Chk { get; set; }
        /// <summary> VarChar(81) (Not Null) </summary>
        public string Unvan { get; set; }
        /// <summary> VarChar(20) (Not Null) </summary>
        public string TeslimYeriKodu { get; set; }
        public string TeslimYeriUnvan { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Kaydeden { get; set; }
        /// <summary> VarChar(5) (Not Null) </summary>
        public string Degistiren { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama2 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama3 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama4 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama5 { get; set; }
        /// <summary> VarChar(120) (Not Null) </summary>
        public string Aciklama6 { get; set; }
        /// <summary> VarChar(16) (Not Null) </summary>
        public string EvrakNo { get; set; }
        /// <summary> VarChar(10) (Allow Null) </summary>
        public string Tarih { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string IslemTip { get; set; }
        /// <summary> VarChar(10) (Allow Null) </summary>
        public string VadeTarih { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public string DvzTL { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string DovizCinsi { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string DovizKuru { get; set; }
        /// <summary> VarChar(100) (Not Null) </summary>
        public string EFatTip { get; set; }

        /// <summary> VarChar(16) (Not Null) </summary>
        public string EFatNot { get; set; }
        /// <summary> VarChar(10) (Allow Null) </summary>
        public string TeslimSarti { get; set; }
        /// <summary> VarChar(100) (Allow Null) </summary>
        public string GonderimSekli { get; set; }
        /// <summary> VarChar(10) (Allow Null) </summary>
        public decimal BrutKilo { get; set; }
        /// <summary> VarChar(10) (Not Null) </summary>
        public decimal NetKilo { get; set; }
        /// <summary> VarChar(3) (Not Null) </summary>
        public string OdemeNotu { get; set; }
        /// <summary> VarChar(8000) (Allow Null) </summary>
        public string OdemeSekli { get; set; }
    }
}