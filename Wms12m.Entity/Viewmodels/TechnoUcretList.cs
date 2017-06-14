using System;

namespace Wms12m.Entity
{
    public class TechnoUcretList
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string IsyeriAdi { get; set; }
        public string BirimAdi { get; set; }
        public string PozisyonAdi { get; set; }
        public string GecerlilikAyi { get; set; }
        public short GecerlilikYili { get; set; }
        public DateTime DegisimTarihi { get; set; }
        public string UcretTipi { get; set; }
        public string DovizTipi { get; set; }
        public decimal BirimUcret { get; set; }
        public decimal GenelAylikUcret { get; set; }
        public DateTime KayitTarih { get; set; }
        public string KaydedenKullanici { get; set; }
        public DateTime GuncellemeTarihi { get; set; }
        public string GuncelleyenKullanici { get; set; }
        public string Aciklama { get; set; }
    }
}
