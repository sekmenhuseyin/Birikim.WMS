using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    [Table("STI")]
    [DataContract(IsReference = true)]
    public class STII
    {

        [Key]
        [Column(Order = 4)]
        [Required]
        [Display(Name = "Islem Tur")]
        public short IslemTur { get; set; }
        [Key]
        [Column(Order = 1)]
        [MaxLength(16)]
        [Required]
        [Display(Name = "Evrak No")]
        public string EvrakNo { get; set; }
        [Key]
        [Column(Order = 3)]
        [Required]
        [Display(Name = "Tarih")]
        public int Tarih { get; set; }
        [Key]
        [Column(Order = 2)]
        [MaxLength(20)]
        [Required]
        [Display(Name = "Chk")]
        public string Chk { get; set; }
        [Key]
        [Column(Order = 5)]
        [Required]
        [Display(Name = "Kynk Evrak Tip")]
        public short KynkEvrakTip { get; set; }
        [Key]
        [Column(Order = 6)]
        [Required]
        [Display(Name = "Sira No")]
        public short SiraNo { get; set; }
        [Required]
        [Display(Name = "Irs Fat")]
        public short IrsFat { get; set; }
        [Required]
        [Display(Name = "Islem Tip")]
        public short IslemTip { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Mal Kodu")]
        public string MalKodu { get; set; }
        [Required]
        [Display(Name = "Miktar")]
        public decimal Miktar { get; set; }
        [Required]
        [Display(Name = "Fiyat")]
        public decimal Fiyat { get; set; }
        [Required]
        [Display(Name = "Tutar")]
        public decimal Tutar { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "Doviz Cinsi")]
        public string DovizCinsi { get; set; }
        [Required]
        [Display(Name = "Doviz Kuru")]
        public decimal DovizKuru { get; set; }
        [Required]
        [Display(Name = "Doviz Tutar")]
        public decimal DovizTutar { get; set; }
        [Required]
        [Display(Name = "Dvz Birim Fiyat")]
        public decimal DvzBirimFiyat { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Birim")]
        public string Birim { get; set; }
        [Required]
        [Display(Name = "Birim Fiyat")]
        public decimal BirimFiyat { get; set; }
        [Required]
        [Display(Name = "Birim Miktar")]
        public decimal BirimMiktar { get; set; }
        [Required]
        [Display(Name = "Iskonto")]
        public decimal Iskonto { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran")]
        public Single IskontoOran { get; set; }
        [Required]
        [Display(Name = "Toplam Iskonto")]
        public decimal ToplamIskonto { get; set; }
        [Required]
        [Display(Name = "KDV")]
        public decimal KDV { get; set; }
        [Required]
        [Display(Name = "KDV Oran")]
        public Single KDVOran { get; set; }
        [Required]
        [Display(Name = "KDV Dahil Haric")]
        public short KDVDahilHaric { get; set; }
        [Required]
        [Display(Name = "Otv Dahil Haric")]
        public short OtvDahilHaric { get; set; }
        [Required]
        [Display(Name = "Otv Tutar")]
        public decimal OtvTutar { get; set; }
        [MaxLength(2)]
        [Required]
        [Display(Name = "Gtk Liste No")]
        public string GtkListeNo { get; set; }
        [MaxLength(64)]
        [Required]
        [Display(Name = "Aciklama")]
        public string Aciklama { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod1")]
        public string Kod1 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod2")]
        public string Kod2 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod3")]
        public string Kod3 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod4")]
        public string Kod4 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod5")]
        public string Kod5 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod6")]
        public string Kod6 { get; set; }
        [MaxLength(10)]
        [Required]
        [Display(Name = "Kod7")]
        public string Kod7 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod8")]
        public string Kod8 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod9")]
        public string Kod9 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod10")]
        public string Kod10 { get; set; }
        [Required]
        [Display(Name = "Kod11")]
        public short Kod11 { get; set; }
        [Required]
        [Display(Name = "Kod12")]
        public short Kod12 { get; set; }
        [Required]
        [Display(Name = "Kod13")]
        public decimal Kod13 { get; set; }
        [Required]
        [Display(Name = "Kod14")]
        public decimal Kod14 { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Depo")]
        public string Depo { get; set; }
        [MaxLength(12)]
        [Required]
        [Display(Name = "Vasita")]
        public string Vasita { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Seri No")]
        public string SeriNo { get; set; }
        [Required]
        [Display(Name = "Sevk Tarih")]
        public int SevkTarih { get; set; }
        [Required]
        [Display(Name = "Promosyon Miktar")]
        public decimal PromosyonMiktar { get; set; }
        [Required]
        [Display(Name = "Miktar2")]
        public decimal Miktar2 { get; set; }
        [Required]
        [Display(Name = "Tutar2")]
        public decimal Tutar2 { get; set; }
        [Required]
        [Display(Name = "Tarih2")]
        public int Tarih2 { get; set; }
        [Required]
        [Display(Name = "Vade Tarih")]
        public int VadeTarih { get; set; }
        [Required]
        [Display(Name = "Masraf")]
        public decimal Masraf { get; set; }
        [Required]
        [Display(Name = "Maliyet")]
        public decimal Maliyet { get; set; }
        [Required]
        [Display(Name = "Mly Yontem")]
        public short MlyYontem { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Mhs Kod")]
        public string MhsKod { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Mhs Karsi Kod")]
        public string MhsKarsiKod { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Masraf Merkezi")]
        public string MasrafMerkezi { get; set; }
        [Required]
        [Display(Name = "Mhs Durum")]
        public short MhsDurum { get; set; }
        [Required]
        [Display(Name = "Mly Mhs")]
        public short MlyMhs { get; set; }
        [Required]
        [Display(Name = "Mhs Tablo No")]
        public short MhsTabloNo { get; set; }
        [Required]
        [Display(Name = "Evrak Tarih")]
        public int EvrakTarih { get; set; }
        [Required]
        [Display(Name = "Siparis Sira No")]
        public short SiparisSiraNo { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran1")]
        public Single IskontoOran1 { get; set; }
        [Required]
        [Display(Name = "Isk Oran1 Net")]
        public short IskOran1Net { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran2")]
        public Single IskontoOran2 { get; set; }
        [Required]
        [Display(Name = "Isk Oran2 Net")]
        public short IskOran2Net { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran3")]
        public Single IskontoOran3 { get; set; }
        [Required]
        [Display(Name = "Isk Oran3 Net")]
        public short IskOran3Net { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran4")]
        public Single IskontoOran4 { get; set; }
        [Required]
        [Display(Name = "Isk Oran4 Net")]
        public short IskOran4Net { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran5")]
        public Single IskontoOran5 { get; set; }
        [Required]
        [Display(Name = "Isk Oran5 Net")]
        public short IskOran5Net { get; set; }
        [Required]
        [Display(Name = "Klm Tutar Isk")]
        public decimal KlmTutarIsk { get; set; }
        [Required]
        [Display(Name = "Klm Tutar Isk Net")]
        public short KlmTutarIskNet { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Teslim Chk")]
        public string TeslimChk { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Butce Kod")]
        public string ButceKod { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Fyt Liste No")]
        public string FytListeNo { get; set; }
        [Required]
        [Display(Name = "Fat Miktar")]
        public decimal FatMiktar { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Tes Tem Mal Kod")]
        public string TesTemMalKod { get; set; }
        [Required]
        [Display(Name = "Dvz TL")]
        public short DvzTL { get; set; }
        [MaxLength(52)]
        [Required]
        [Display(Name = "Barkod No")]
        public string BarkodNo { get; set; }
        [Required]
        [Display(Name = "Katsayi")]
        public double Katsayi { get; set; }
        [Required]
        [Display(Name = "Operator")]
        public short Operator { get; set; }
        [Required]
        [Display(Name = "Valor Gun")]
        public short ValorGun { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Kaynak Irs Evrak No")]
        public string KaynakIrsEvrakNo { get; set; }
        [Required]
        [Display(Name = "Kaynak Irs Tarih")]
        public int KaynakIrsTarih { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Kaynak IIF Evrak No")]
        public string KaynakIIFEvrakNo { get; set; }
        [Required]
        [Display(Name = "Kaynak IIF Tarih")]
        public int KaynakIIFTarih { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Kaynak Siparis No")]
        public string KaynakSiparisNo { get; set; }
        [Required]
        [Display(Name = "Kaynak Siparis Tarih")]
        public int KaynakSiparisTarih { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Erek IF Evrak No")]
        public string ErekIFEvrakNo { get; set; }
        [Required]
        [Display(Name = "Erek IFK Evrak Tip")]
        public short ErekIFKEvrakTip { get; set; }
        [Required]
        [Display(Name = "Erek IF Miktar")]
        public decimal ErekIFMiktar { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Erek IIF Evrak No")]
        public string ErekIIFEvrakNo { get; set; }
        [Required]
        [Display(Name = "Erek IIFK Evrak Tip")]
        public short ErekIIFKEvrakTip { get; set; }
        [Required]
        [Display(Name = "Erek IIF Miktar")]
        public decimal ErekIIFMiktar { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Renk Beden Kod1")]
        public string RenkBedenKod1 { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Renk Beden Kod2")]
        public string RenkBedenKod2 { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Renk Beden Kod3")]
        public string RenkBedenKod3 { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Renk Beden Kod4")]
        public string RenkBedenKod4 { get; set; }
        [Required]
        [Display(Name = "Kayit Turu")]
        public short KayitTuru { get; set; }
        [MaxLength(254)]
        [Required]
        [Display(Name = "Nesne1")]
        public string Nesne1 { get; set; }
        [MaxLength(254)]
        [Required]
        [Display(Name = "Nesne2")]
        public string Nesne2 { get; set; }
        [MaxLength(254)]
        [Required]
        [Display(Name = "Nesne3")]
        public string Nesne3 { get; set; }
        [Required]
        [Display(Name = "Irs Fat2")]
        public short IrsFat2 { get; set; }
        [Required]
        [Display(Name = "Miktar3")]
        public decimal Miktar3 { get; set; }
        [Required]
        [Display(Name = "Tutar3")]
        public decimal Tutar3 { get; set; }
        [Required]
        [Display(Name = "Sira No2")]
        public short SiraNo2 { get; set; }
        [Required]
        [Display(Name = "Kur Tarihi")]
        public int KurTarihi { get; set; }
        [Required]
        [Display(Name = "Kredi Borc Tutar")]
        public decimal KrediBorcTutar { get; set; }
        [Required]
        [Display(Name = "Aktiflesen Kredi Faizi")]
        public decimal AktiflesenKrediFaizi { get; set; }
        [Required]
        [Display(Name = "Kredi Donem Baslangic Tarih")]
        public int Kredi_Donem_BaslangicTarih { get; set; }
        [Required]
        [Display(Name = "Kredi Donem Bitis Tarih")]
        public int Kredi_Donem_BitisTarih { get; set; }
        [Required]
        [Display(Name = "Kredi Donem Vade Tarih")]
        public int Kredi_Donem_VadeTarih { get; set; }
        [Required]
        [Display(Name = "Kredi Donem Vade Farki Tutar")]
        public decimal Kredi_Donem_VadeFarkiTutar { get; set; }
        [Required]
        [Display(Name = "Reel Olmayan Finansman Maliyet")]
        public decimal ReelOlmayanFinansmanMaliyet { get; set; }
        [Required]
        [Display(Name = "Kredi Arindirma Sekli")]
        public short KrediArindirmaSekli { get; set; }
        [Required]
        [Display(Name = "Finansman Gider Turu")]
        public short FinansmanGiderTuru { get; set; }
        [Required]
        [Display(Name = "Duz Yapilan Yil")]
        public int Duz_Yapilan_Yil { get; set; }
        [Required]
        [Display(Name = "Duz Yapilan Donem")]
        public short Duz_Yapilan_Donem { get; set; }
        [Required]
        [Display(Name = "Duz Yontemi")]
        public short Duz_Yontemi { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Duz Mhs Hesap Kodu")]
        public string Duz_Mhs_Hesap_Kodu { get; set; }
        [Required]
        [Display(Name = "Duz Mhs Durumu")]
        public short Duz_Mhs_Durumu { get; set; }
        [Required]
        [Display(Name = "Duz Stok Devir Hizi")]
        public Single Duz_Stok_Devir_Hizi { get; set; }
        [Required]
        [Display(Name = "Duz Katsayisi")]
        public Single Duz_Katsayisi { get; set; }
        [Required]
        [Display(Name = "Duz Esas Tutar")]
        public decimal Duz_Esas_Tutar { get; set; }
        [Required]
        [Display(Name = "Duz Tutar")]
        public decimal Duz_Tutar { get; set; }
        [Required]
        [Display(Name = "Duz Mly Yontemi")]
        public short Duz_Mly_Yontemi { get; set; }
        [Required]
        [Display(Name = "Duz Mly Tarihi Mly Tutar")]
        public decimal Duz_Mly_Tarihi_Mly_Tutar { get; set; }
        [Required]
        [Display(Name = "Duz Mly Satilan Mal Mly Tutar")]
        public decimal Duz_Mly_Satilan_Mal_Mly_Tutar { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Duz Mly Mhs Hesap Kodu")]
        public string Duz_Mly_Mhs_Hesap_Kodu { get; set; }
        [Required]
        [Display(Name = "Duz Mly Mhs Durumu")]
        public short Duz_Mly_Mhs_Durumu { get; set; }
        [Required]
        [Display(Name = "Ana Evrak Tip")]
        public short AnaEvrakTip { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "Kart Doviz Cinsi")]
        public string KartDovizCinsi { get; set; }
        [Required]
        [Display(Name = "Kart Doviz Kuru")]
        public decimal KartDovizKuru { get; set; }
        [MaxLength(2)]
        [Required]
        [Display(Name = "Guvenlik Kod")]
        public string GuvenlikKod { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Kaydeden")]
        public string Kaydeden { get; set; }
        [Required]
        [Display(Name = "Kayit Tarih")]
        public int KayitTarih { get; set; }
        [Required]
        [Display(Name = "Kayit Saat")]
        public int KayitSaat { get; set; }
        [Required]
        [Display(Name = "Kayit Kaynak")]
        public short KayitKaynak { get; set; }
        [MaxLength(12)]
        [Required]
        [Display(Name = "Kayit Surum")]
        public string KayitSurum { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Degistiren")]
        public string Degistiren { get; set; }
        [Required]
        [Display(Name = "Degis Tarih")]
        public int DegisTarih { get; set; }
        [Required]
        [Display(Name = "Degis Saat")]
        public int DegisSaat { get; set; }
        [Required]
        [Display(Name = "Degis Kaynak")]
        public short DegisKaynak { get; set; }
        [MaxLength(12)]
        [Required]
        [Display(Name = "Degis Surum")]
        public string DegisSurum { get; set; }
        [Required]
        [Display(Name = "Check Sum")]
        public short CheckSum { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "FF Evrak No")]
        public string FFEvrakNo { get; set; }
        [Required]
        [Display(Name = "FF Tarih")]
        public int FFTarih { get; set; }
        [Required]
        [Display(Name = "Kaynak Sira No")]
        public short KaynakSiraNo { get; set; }
        [Required]
        [Display(Name = "Son Kullanim Tarihi")]
        public int SonKullanimTarihi { get; set; }
        [Required]
        [Display(Name = "Dvz Kur Cinsi")]
        public short DvzKurCinsi { get; set; }
        [Required]
        [Display(Name = "Depo Iade Edilen Miktar")]
        public decimal DepoIadeEdilenMiktar { get; set; }
        [MaxLength(7)]
        [Required]
        [Display(Name = "Tevfikat Oran")]
        public string TevfikatOran { get; set; }
        [Required]
        [Display(Name = "Tevfikat Tutar")]
        public decimal TevfikatTutar { get; set; }
        [Required]
        [Display(Name = "Masraf1")]
        public decimal Masraf1 { get; set; }
        [Required]
        [Display(Name = "Masraf2")]
        public decimal Masraf2 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Ithalat Dosya No")]
        public string IthalatDosyaNo { get; set; }
        [Required]
        [Display(Name = "Ithalat M Dag Durum")]
        public short IthalatMDagDurum { get; set; }
        [Required]
        [Display(Name = "Tarih3")]
        public int Tarih3 { get; set; }
        [Required]
        [Display(Name = "Tarih4")]
        public int Tarih4 { get; set; }
        [Required]
        [Display(Name = "Tarih5")]
        public int Tarih5 { get; set; }
        [Required]
        [Display(Name = "Tarih6")]
        public int Tarih6 { get; set; }
        [Required]
        [Display(Name = "E Fat Tip")]
        public short EFatTip { get; set; }
        [Required]
        [Display(Name = "E Fat Durum")]
        public short EFatDurum { get; set; }
        [Required]
        [Display(Name = "Toplam Klm Iskonto")]
        public decimal ToplamKlmIskonto { get; set; }
        [Required]
        [Display(Name = "Ithalat M Dag Yontem")]
        public short IthalatMDagYontem { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "KDV Muaf Ack")]
        public string KDVMuafAck { get; set; }
        [MaxLength(40)]
        [Required]
        [Display(Name = "Karsi Mal Kodu")]
        public string KarsiMalKodu { get; set; }
        [MaxLength(40)]
        [Required]
        [Display(Name = "Uretici Mal Kodu")]
        public string UreticiMalKodu { get; set; }
        [Required]
        [Display(Name = "Stopaj Oran")]
        public Single StopajOran { get; set; }
        [Required]
        [Display(Name = "Stopaj Tutar")]
        public decimal StopajTutar { get; set; }
        [Required]
        [Display(Name = "OTV Tevkifat Var Yok")]
        public short OTVTevkifatVarYok { get; set; }
        [MaxLength(64)]
        [Required]
        [Display(Name = "Aciklama2")]
        public string Aciklama2 { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "e Fat Irs Referans No")]
        public string eFatIrsReferansNo { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Ihracat Dosya No")]
        public string IhracatDosyaNo { get; set; }
        [MaxLength(7)]
        [Required]
        [Display(Name = "Tevfikat Oran Kod")]
        public string TevfikatOranKod { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "Ozel Mat KDV Kod")]
        public string OzelMatKDVKod { get; set; }
        [MaxLength(40)]
        [Required]
        [Display(Name = "Proje Kodu")]
        public string ProjeKodu { get; set; }
        [Required]
        [Display(Name = "E Arsiv Teslim Sekli")]
        public short EArsivTeslimSekli { get; set; }
        [Required]
        [Display(Name = "E Arsiv Fatura Tipi")]
        public short EArsivFaturaTipi { get; set; }
        [Required]
        [Display(Name = "E Arsiv Fatura Durum")]
        public short EArsivFaturaDurum { get; set; }
        [Required]
        [Display(Name = "Iskonto Tutar")]
        public decimal IskontoTutar { get; set; }
        [Required]
        [Display(Name = "Iskonto Tutar1")]
        public decimal IskontoTutar1 { get; set; }
        [Required]
        [Display(Name = "Iskonto Tutar2")]
        public decimal IskontoTutar2 { get; set; }
        [Required]
        [Display(Name = "Iskonto Tutar3")]
        public decimal IskontoTutar3 { get; set; }
        [Required]
        [Display(Name = "Iskonto Tutar4")]
        public decimal IskontoTutar4 { get; set; }
        [Required]
        [Display(Name = "Iskonto Tutar5")]
        public decimal IskontoTutar5 { get; set; }
        [MaxLength(128)]
        [Required]
        [Display(Name = "Not1")]
        public string Not1 { get; set; }
        [MaxLength(128)]
        [Required]
        [Display(Name = "Not2")]
        public string Not2 { get; set; }
        [MaxLength(128)]
        [Required]
        [Display(Name = "Not3")]
        public string Not3 { get; set; }
        [MaxLength(128)]
        [Required]
        [Display(Name = "Not4")]
        public string Not4 { get; set; }
        [MaxLength(128)]
        [Required]
        [Display(Name = "Not5")]
        public string Not5 { get; set; }
        [Required]
        [Display(Name = "Brut Agirlik")]
        public decimal BrutAgirlik { get; set; }
        [Required]
        [Display(Name = "Net Agirlik")]
        public decimal NetAgirlik { get; set; }
        [Required]
        [Display(Name = "Dara Agirlik")]
        public decimal DaraAgirlik { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Birim Agirlik")]
        public string BirimAgirlik { get; set; }
        [Required]
        [Display(Name = "Brut Hacim")]
        public decimal BrutHacim { get; set; }
        [Required]
        [Display(Name = "Net Hacim")]
        public decimal NetHacim { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Birim Hacim")]
        public string BirimHacim { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Kap Tipi")]
        public string KapTipi { get; set; }
        [Required]
        [Display(Name = "Kap Adet")]
        public decimal KapAdet { get; set; }
        [Required]
        [Display(Name = "Form Ba Bs Tarih")]
        public int FormBaBsTarih { get; set; }
        [Required]
        [Display(Name = "YOKCZ Rapor No")]
        public int YOKCZRaporNo { get; set; }
        [Required]
        [Display(Name = "KDV Dahil Kalem Iskonto Oran")]
        public Single KDVDahilKalemIskontoOran { get; set; }
        [Required]
        [Display(Name = "KDV Dahil Kalem Oran Iskontosu")]
        public decimal KDVDahilKalemOranIskontosu { get; set; }
        [Required]
        [Display(Name = "KDV Dahil Kalem Tutar Iskontosu")]
        public decimal KDVDahilKalemTutarIskontosu { get; set; }
        [Required]
        [Display(Name = "KDV Dahil Iskonto")]
        public decimal KDVDahilIskonto { get; set; }
        [Required]
        [Display(Name = "KDV Istisna Tutar")]
        public decimal KDVIstisnaTutar { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ROW ID")]
        public int ROW_ID { get; set; }
        [Timestamp]
        [Required]
        [Display(Name = "timestamp")]
        public byte[] timestamp { get; set; }
    }

}
