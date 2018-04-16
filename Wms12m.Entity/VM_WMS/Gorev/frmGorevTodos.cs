using System;

namespace Wms12m.Entity
{
    public class frmGorevTodos
    {
        public string Aciklama { get; set; }
        public bool AktifPasif { get; set; }
        public DateTime? DegisTarih { get; set; }
        public string Degistiren { get; set; }
        public int GorevID { get; set; }
        public int ID { get; set; }
        public string Kaydeden { get; set; }
        public DateTime KayitTarih { get; set; }
        public string TahminiBitis { get; set; }
        public bool Kontrol { get; set; }
        public bool KontrolOnay { get; set; }
        public bool Onay { get; set; }
    }
}