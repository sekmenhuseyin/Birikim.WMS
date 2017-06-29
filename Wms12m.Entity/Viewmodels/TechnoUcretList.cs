using System;

namespace Wms12m.Entity
{
    public class TechnoList
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
        public decimal TutarIsci { get; set; }
        public decimal TutarMemur { get; set; }
        public string DovizTipIsci { get; set; }
        public string DovizTipMemur { get; set; }
        public string OdenekKodu { get; set; }
        public byte Ocak { get; set; }
        public byte Subat { get; set; }
        public byte Mart { get; set; }
        public byte Nisan { get; set; }
        public byte Mayis { get; set; }
        public byte Haziran { get; set; }
        public byte Temmuz { get; set; }
        public byte Agustos { get; set; }
        public byte Eylul { get; set; }
        public byte Ekim { get; set; }
        public byte Kasim { get; set; }
        public byte Aralik { get; set; }
        public short Yil { get; set; }
        public int PERSONELID { get; set; }
        public string BrutNet { get; set; }
        public int ID { get; set; }
        public string RedNedeni { get; set; }
        public string Reddeden { get; set; }
        public int DSKALAID { get; set; }
        public int DBUTUCRETID { get; set; }
        public short IslemTipi { get; set; }
        public int DSKALAANAID { get; set; }
    }
}
