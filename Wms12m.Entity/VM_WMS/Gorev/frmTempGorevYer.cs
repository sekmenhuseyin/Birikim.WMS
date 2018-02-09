using System;

namespace Wms12m.Entity
{

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