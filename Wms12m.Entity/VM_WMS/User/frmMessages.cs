using System;

namespace Wms12m.Entity
{
    /// <summary>
    /// mesaj tablosu
    /// </summary>
    public class frmMessages
    {
        public string AdSoyad { get; set; }
        public Guid ID { get; set; }
        public string Kod { get; set; }
        public string Mesaj { get; set; }
        public int MesajTipi { get; set; }
        public DateTime Tarih { get; set; }
    }
}