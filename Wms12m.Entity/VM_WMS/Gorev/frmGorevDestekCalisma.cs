using System;

namespace Wms12m.Entity
{
    /// <summary>
    /// görev çalışmaları
    /// </summary>
    public class frmGorevDestekCalisma
    {
        public string Calisma { get; set; }
        public int GorevID { get; set; }
        public int MusteriID { get; set; }
        public int Sure { get; set; }
        public DateTime Tarih { get; set; }
    }
}