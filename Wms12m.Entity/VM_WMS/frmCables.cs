using System;

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
    }

    /// <summary>
    /// kablo stok sayfası
    /// </summary>
    public partial class frmCableStok
    {
        public string Birim { get; set; }
        public string cins { get; set; }
        public int? DepoID { get; set; }
        public string HucreAd { get; set; }
        public int ID { get; set; }
        public int KatID { get; set; }
        public string kesit { get; set; }
        public string MakaraNo { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public string marka { get; set; }
        public decimal Miktar { get; set; }
        public string renk { get; set; }
    }

    /// <summary>
    /// Sipariş Planlama Görev Yer Düzenleme İçin
    /// </summary>
    public partial class frmTempGorevYer
    {
        public bool Aktif { get; set; }
        public string Birim { get; set; }
        public bool GC { get; set; }
        public int GorevID { get; set; }
        public int ID { get; set; }
        public int IrsaliyeID { get; set; }
        public Nullable<bool> IU { get; set; }
        public string MakaraNo { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public Nullable<int> PID { get; set; }
        public Nullable<int> Sira { get; set; }
        public int YerID { get; set; }
        public Nullable<decimal> YerlestirmeMiktari { get; set; }
    }
}