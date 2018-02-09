namespace Wms12m.Entity
{
    /// <summary>
    /// kablo siparişi için stk sütunları
    /// </summary>
    public class frmCableSiparis
    {
        public string cins { get; set; }
        public string depo { get; set; }
        public string EvrakNo { get; set; }
        public int id { get; set; }
        public string kesit { get; set; }
        public string makara { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public string marka { get; set; }
        public decimal Miktar { get; set; }
        public string renk { get; set; }
        public string rezerve { get; set; }
        public int ROW_ID { get; set; }
        public int Saat { get; set; }
        public string satici { get; set; }
        public short SiraNo { get; set; }
        public string SirketID { get; set; }
        public decimal stok { get; set; }
        public int Tarih { get; set; }
        public string Unvan { get; set; }
    }
}