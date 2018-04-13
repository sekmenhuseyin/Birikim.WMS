namespace Wms12m.Entity
{
    /// <summary>
    /// kablo siparişi için stk sütunları
    /// </summary>
    public class frmCableStk
    {
        public string Cins { get; set; }
        public string EvrakNo { get; set; }
        public string Kesit { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public string Marka { get; set; }
        public decimal Miktar { get; set; }
        public int ROW_ID { get; set; }
        public int Saat { get; set; }
        public short SiraNo { get; set; }
        public string SirketID { get; set; }
        public decimal Stok { get; set; }
        public int Tarih { get; set; }
        public string Unvan { get; set; }
        public string Renk { get; set; }
    }
}