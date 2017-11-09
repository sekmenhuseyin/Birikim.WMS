namespace Wms12m.Entity
{
    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnaySiparisList
    {
        public int ID { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string EvrakSeriNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Depo { get; set; }
        public int Cesit { get; set; }
        public decimal? Miktar { get; set; }
        public decimal? BirimFiyat { get; set; }
        public string DovizCinsi { get; set; }
        public decimal? Tutar { get; set; }
        public string Tarih { get; set; }
        public string Kaydeden { get; set; }
    }
    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class frmOnayTransferList
    {
        public int ID { get; set; }
        public string TransferNo { get; set; }
        public int TransferTarihi { get; set; }
        public string Tarih { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public string CikisDepo { get; set; }
        public string GirisDepo { get; set; }
        public string Kaydeden { get; set; }
        public int KayitTarih { get; set; }
    }
    /// <summary>
    /// teklif onay
    /// </summary>
    public class frmOnayTeklifList
    {
        public string TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public int? Cesit { get; set; }
        public decimal? Miktar { get; set; }
        public string Kaydeden { get; set; }
        public string KayitTarihi { get; set; }
        public string TeklifTarihi { get; set; }
    }
    /// <summary>
    /// teklif detay
    /// </summary>
    public class frmOnayTeklifListDetay
    {
        public string TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public decimal Miktar { get; set; }
        public string Kaydeden { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }
        public string DovizCinsi { get; set; }
        public string KayitTarihi { get; set; }
    }
    /// <summary>
    /// onay bekleyen transfer listesi
    /// </summary>
    public class frmOnayFatura
    {
        public int ID { get; set; }
        public string EvrakNo { get; set; }
        public short SiraNo { get; set; }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Depo { get; set; }
        public int Cesit { get; set; }
        public decimal Miktar { get; set; }
        public decimal Fiyat { get; set; }
        public string ParaCinsi { get; set; }
        public string Birim { get; set; }
        public short IslemDurumu { get; set; }
        public string Kaydeden { get; set; }
        public string KayitTarih { get; set; }
        public string KayitSaat { get; set; }
        public string Degistiren { get; set; }
        public string DegisTarih { get; set; }
        public string DegisSaat { get; set; }
    }

    /// <summary>
    /// onay bekleyen sipariş listesi
    /// </summary>
    public class frmOnayTahsilatList
    {
        public string TahsilatNo { get; set; }
        public string Tarih { get; set; }

        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string IslemTuru { get; set; }
        public string OdemeTuru { get; set; }
      
        public decimal Tutar { get; set; }
        public string DovizCinsi { get; set; }

        public decimal KapatilanTL { get; set; }
        public decimal KapatilanUSD { get; set; }
        public decimal KapatilanEUR { get; set; }

        public string Kaydeden { get; set; }
        public string Aciklama { get; set; }
       
    }
    /// <summary>
    /// fatura kayıt işlemleri için
    /// </summary>
    public class SepetUrun
    {
        public string KullaniciKodu { get; set; }
        public string HesapKodu { get; set; }
        public string UrunKodu { get; set; }
        public string Depo { get; set; }
        public string ParaCinsi { get; set; }
        public string Birim { get; set; }
        public string Fiyat { get; set; }
        public string Miktar { get; set; }
        public int SatirTip { get; set; }  ///1 Fatura 2 Sipariş 3 Teklif
        public int OnayaDusme { get; set; }  ///0 Normal  1 Onaya Düşecek
        public string Adres1 { get; set; }
        public string Adres2 { get; set; }
        public string Adres3 { get; set; }
        public string Aciklama1 { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }

        public string Kaydeden { get; set; }
    }

    public class DepoTran
    {
        public string KullaniciKodu { get; set; }
        public string MalKodu { get; set; }
        public string GirisDepo { get; set; }
        public string CikisDepo { get; set; }       
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public int MiatTarih { get; set; }      
    }


    public class HesapItem
    {
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }
        public string BankaHesapKodu { get; set; }
        public string ParaBirimi { get; set; }
        public static string Sorgu = @"
        SELECT CAR002_HesapKodu as HesapKodu, CAR002_Unvan1+' '+CAR002_Unvan2 as Unvan,
               CAR002_BankaHesapKodu as BankaHesapKodu, CAR002_ParaBirimi as ParaBirimi   
        FROM  YNS{{0}}.YNS{{0}}.CAR002(NOLOCK)
        WHERE CAR002_AktifFlag=1 AND CAR002_BankaHesapKodu='{0}'
        ";
    }
    public class STIEvrakBilgi
    {
        public int Tip { get; set; }
        public string EvrakNo { get; set; }
        public int SiraNo { get; set; }
    }
}
