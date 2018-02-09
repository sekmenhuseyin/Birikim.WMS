namespace Wms12m.Entity
{
    /// <summary>
    /// rafa yerleştir formu
    /// </summary>
    public class frmYerlesme
    {
        public string Birim { get; set; }
        public int DepoID { get; set; }
        public int GorevID { get; set; }
        public int IrsDetayID { get; set; }
        public int IrsID { get; set; }
        public string MakaraNo { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public string RafNo { get; set; }
    }
}