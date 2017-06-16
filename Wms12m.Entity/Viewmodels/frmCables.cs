namespace Wms12m.Entity
{
    /// <summary>
    /// kablo siparişi için stk sütunları
    /// </summary>
    public class frmCableStk
    {
        public string SirketID { get; set; }
        public int ROW_ID { get; set; }
        public short SiraNo { get; set; }
        public string EvrakNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string MalAdi4 { get; set; }//marka
        public string Nesne2 { get; set; }//cins
        public string Kod15 { get; set; }//kesit
        public string MakaraNo { get; set; }//kesit
        //renk de olacak
        public decimal Miktar { get; set; }
    }
    /// <summary>
    /// kablo siparişi için stk sütunları
    /// </summary>
    public class frmCableSiparis
    {
        public string SirketID { get; set; }
        public int ROW_ID { get; set; }
        public short SiraNo { get; set; }
        public string EvrakNo { get; set; }
        public int id { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public decimal stok { get; set; }
        public string marka { get; set; }
        public string cins { get; set; }
        public string kesit { get; set; }
        public string depo { get; set; }
        public string renk { get; set; }
        public string makara { get; set; }
        public string rezerve { get; set; }
        public string satici { get; set; }
    }
}
