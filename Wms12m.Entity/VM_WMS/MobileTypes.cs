namespace Wms12m.Entity
{
    /// <summary>
    /// terminal durum tablosu
    /// </summary>
    public class Durum
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// terminal görev özetleri
    /// </summary>
    public class GorevOzet
    {
        public int ID { get; set; }
        public string Ad { get; set; }
        public int Sayi { get; set; }
    }
    /// <summary>
    /// mobil ürün listesi
    /// </summary>
    public class frmUrunler
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string GrupKodu { get; set; }
        public string Birim1 { get; set; }
        public string Birim2 { get; set; }
        public decimal Fiyat { get; set; }
    }
    /// <summary>
    /// mobil müşteri listesi
    /// </summary>
    public class frmMusteriler
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string CariTipi { get; set; }
    }
    /// <summary>
    /// Gorev Paketler
    /// </summary>
    public class frmGorevPaket
    {
        public string SevkiyatNo { get; set; }
        public string PaketNo { get; set; }
        public decimal Adet { get; set; }
        public int PaketTipiID { get; set; }
        public decimal Agirlik { get; set; }
        public bool HepsiVar { get; set; }
    }
    public class Tip_STI
    {
        public int ID { get; set; }
        public int irsID { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string MakaraNo { get; set; }
        public string Barkod { get; set; }
        public int SiraNo { get; set; }
        public string Kaydeden { get; set; }
        public bool AktarimDurumu { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
        public decimal? YerMiktar { get; set; }
        public string Raf { get; set; }
    }

    public class Tip_STI2
    {
        public int ID { get; set; }
        public int irsID { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string MakaraNo { get; set; }
        public string Barkod { get; set; }
        public int SiraNo { get; set; }
        public string Kaydeden { get; set; }
        public bool AktarimDurumu { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public decimal YerlestirmeMiktari { get; set; }
        public decimal? YerMiktar { get; set; }
        public string Raf { get; set; }
        public string KynkSiparisNo { get; set; }
        public string IrsaliyeNo { get; set; }
        public short? KynkSiparisSiraNo { get; set; }
    }

    public class STIMax
    {
        public string SirketKod { get; set; }
        public int IrsaliyeID { get; set; }
        public string Chk { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string TeslimChk { get; set; }
        public string DepoKodu { get; set; }
        public int Tarih { get; set; }
        public short ValorGun { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal OkutulanMiktar { get; set; }
        public string SiparisNo { get; set; }
        public short KynkSiparisSiraNo { get; set; }
        public int KynkSiparisTarih { get; set; }
        public decimal KynkSiparisMiktar { get; set; }
        public decimal TeslimMiktar { get; set; }
        public decimal KapatilanMiktar { get; set; }
        public decimal KDV { get; set; }
        public string FytListeNo { get; set; }
        public decimal ToplamIskonto { get; set; }
        public float? IskontoOran5 { get; set; }
        public float? IskontoOran4 { get; set; }
        public float? IskontoOran3 { get; set; }
        public float? IskontoOran2 { get; set; }
        public float? IskontoOran1 { get; set; }
        public float? KdvOran { get; set; }
        public decimal? Fiyat { get; set; }
        public decimal? ErekIIFMiktar { get; set; }
        public int? Row_ID { get; set; }
    }


    public class Tip_IRS
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public string HesapKodu { get; set; }
        public string TeslimCHK { get; set; }
        public string DepoID { get; set; }
        public string Kaydeden { get; set; }
        public string Tarih { get; set; }
        public string SirketKod { get; set; }
        public string Unvan { get; set; }
    }

    public class Tip_GOREV
    {
        public int ID { get; set; }
        public int IrsaliyeID { get; set; }
        public string EvrakNo { get; set; }
        public string Bilgi { get; set; }
        public string Atayan { get; set; }
        public string DepoID { get; set; }
        public string OlusturmaTarihi { get; set; }
        public string Gorevli { get; set; }
        public string Aciklama { get; set; }
        public string GorevNo { get; set; }
        public string Durum { get; set; }
    }
    public class Tip_Malzeme
    {
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Birim { get; set; }
        public string Barkod { get; set; }
        public string Kod1 { get; set; }
    }
}
