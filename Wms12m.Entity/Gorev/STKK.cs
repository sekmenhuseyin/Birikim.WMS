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
    [Table("STK")]
    [DataContract(IsReference = true)]
    public class STKK
    {
        [Key]
        [MaxLength(30)]
        [Required]
        [Display(Name = "Mal Kodu")]
        public string MalKodu { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Mal Adi")]
        public string MalAdi { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Mal Adi2")]
        public string MalAdi2 { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Mal Adi3")]
        public string MalAdi3 { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Mal Adi4")]
        public string MalAdi4 { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Mal Adi5")]
        public string MalAdi5 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Grup Kod")]
        public string GrupKod { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Sirket Web Adres")]
        public string SirketWebAdres { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Ozel Kod")]
        public string OzelKod { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Tip Kod")]
        public string TipKod { get; set; }
        [MaxLength(52)]
        [Required]
        [Display(Name = "Bar Kod1")]
        public string BarKod1 { get; set; }
        [MaxLength(52)]
        [Required]
        [Display(Name = "Bar Kod2")]
        public string BarKod2 { get; set; }
        [MaxLength(52)]
        [Required]
        [Display(Name = "Bar Kod3")]
        public string BarKod3 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod1")]
        public string Kod1 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod2")]
        public string Kod2 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod3")]
        public string Kod3 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod4")]
        public string Kod4 { get; set; }
        [Required]
        [Display(Name = "Kod5")]
        public decimal Kod5 { get; set; }
        [Required]
        [Display(Name = "Kod6")]
        public decimal Kod6 { get; set; }
        [MaxLength(20)]
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
        [Required]
        [Display(Name = "Kod10")]
        public short Kod10 { get; set; }
        [Required]
        [Display(Name = "Kod11")]
        public short Kod11 { get; set; }
        [Required]
        [Display(Name = "Kod12")]
        public decimal Kod12 { get; set; }
        [Required]
        [Display(Name = "Kod13")]
        public decimal Kod13 { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "Doviz Cinsi")]
        public string DovizCinsi { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Mal Kodu2")]
        public string MalKodu2 { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Birim1")]
        public string Birim1 { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Birim2")]
        public string Birim2 { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Birim3")]
        public string Birim3 { get; set; }
        [Required]
        [Display(Name = "Operator2")]
        public short Operator2 { get; set; }
        [Required]
        [Display(Name = "Operator3")]
        public short Operator3 { get; set; }
        [Required]
        [Display(Name = "Kat Sayi2")]
        public double KatSayi2 { get; set; }
        [Required]
        [Display(Name = "Kat Sayi3")]
        public double KatSayi3 { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Uretici Kodu")]
        public string UreticiKodu { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Fireli Mal Kodu")]
        public string FireliMalKodu { get; set; }
        [Required]
        [Display(Name = "Mly Sekli")]
        public short MlySekli { get; set; }
        [Required]
        [Display(Name = "MKDS")]
        public short MKDS { get; set; }
        [Required]
        [Display(Name = "Valor Gun")]
        public short ValorGun { get; set; }
        [Required]
        [Display(Name = "Iskonto Oran")]
        public Single IskontoOran { get; set; }
        [Required]
        [Display(Name = "KDV Oran")]
        public Single KDVOran { get; set; }
        [Required]
        [Display(Name = "Fire")]
        public Single Fire { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Temin Yeri")]
        public string TeminYeri { get; set; }
        [Required]
        [Display(Name = "Temin Suresi")]
        public short TeminSuresi { get; set; }
        [Required]
        [Display(Name = "Kritik Stok")]
        public decimal KritikStok { get; set; }
        [Required]
        [Display(Name = "Azami Stok")]
        public decimal AzamiStok { get; set; }
        [Required]
        [Display(Name = "Tahmini Stok")]
        public decimal TahminiStok { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Satislar Hesabi")]
        public string SatislarHesabi { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Alimlar Hesabi")]
        public string AlimlarHesabi { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Satistan Iade")]
        public string SatistanIade { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Alimdan Iade")]
        public string AlimdanIade { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Masraf Merkezi")]
        public string MasrafMerkezi { get; set; }
        [Required]
        [Display(Name = "Alis Fiyat1")]
        public decimal AlisFiyat1 { get; set; }
        [Required]
        [Display(Name = "Alis Fiyat2")]
        public decimal AlisFiyat2 { get; set; }
        [Required]
        [Display(Name = "Alis Fiyat3")]
        public decimal AlisFiyat3 { get; set; }
        [Required]
        [Display(Name = "Doviz Alis Fiyat1")]
        public decimal DovizAlisFiyat1 { get; set; }
        [Required]
        [Display(Name = "Doviz Alis Fiyat2")]
        public decimal DovizAlisFiyat2 { get; set; }
        [Required]
        [Display(Name = "Doviz Alis Fiyat3")]
        public decimal DovizAlisFiyat3 { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "A F1 Doviz Cinsi")]
        public string AF1DovizCinsi { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "A F2 Doviz Cinsi")]
        public string AF2DovizCinsi { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "A F3 Doviz Cinsi")]
        public string AF3DovizCinsi { get; set; }
        [Required]
        [Display(Name = "A F1 KDV")]
        public short AF1KDV { get; set; }
        [Required]
        [Display(Name = "A F2 KDV")]
        public short AF2KDV { get; set; }
        [Required]
        [Display(Name = "A F3 KDV")]
        public short AF3KDV { get; set; }
        [Required]
        [Display(Name = "Doviz A F1 KDV")]
        public short DovizAF1KDV { get; set; }
        [Required]
        [Display(Name = "Doviz A F2 KDV")]
        public short DovizAF2KDV { get; set; }
        [Required]
        [Display(Name = "Doviz A F3 KDV")]
        public short DovizAF3KDV { get; set; }
        [Required]
        [Display(Name = "A F1 Birim")]
        public short AF1Birim { get; set; }
        [Required]
        [Display(Name = "A F2 Birim")]
        public short AF2Birim { get; set; }
        [Required]
        [Display(Name = "A F3 Birim")]
        public short AF3Birim { get; set; }
        [Required]
        [Display(Name = "Doviz A F1 Birim")]
        public short DovizAF1Birim { get; set; }
        [Required]
        [Display(Name = "Doviz A F2 Birim")]
        public short DovizAF2Birim { get; set; }
        [Required]
        [Display(Name = "Doviz A F3 Birim")]
        public short DovizAF3Birim { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat1")]
        public decimal SatisFiyat1 { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat2")]
        public decimal SatisFiyat2 { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat3")]
        public decimal SatisFiyat3 { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat4")]
        public decimal SatisFiyat4 { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat5")]
        public decimal SatisFiyat5 { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat6")]
        public decimal SatisFiyat6 { get; set; }
        [Required]
        [Display(Name = "Doviz Satis Fiyat1")]
        public decimal DovizSatisFiyat1 { get; set; }
        [Required]
        [Display(Name = "Doviz Satis Fiyat2")]
        public decimal DovizSatisFiyat2 { get; set; }
        [Required]
        [Display(Name = "Doviz Satis Fiyat3")]
        public decimal DovizSatisFiyat3 { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "S F1 Doviz Cinsi")]
        public string SF1DovizCinsi { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "S F2 Doviz Cinsi")]
        public string SF2DovizCinsi { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "S F3 Doviz Cinsi")]
        public string SF3DovizCinsi { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "S F4 Doviz Cinsi")]
        public string SF4DovizCinsi { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "S F5 Doviz Cinsi")]
        public string SF5DovizCinsi { get; set; }
        [MaxLength(3)]
        [Required]
        [Display(Name = "S F6 Doviz Cinsi")]
        public string SF6DovizCinsi { get; set; }
        [Required]
        [Display(Name = "S F1 KDV")]
        public short SF1KDV { get; set; }
        [Required]
        [Display(Name = "S F2 KDV")]
        public short SF2KDV { get; set; }
        [Required]
        [Display(Name = "S F3 KDV")]
        public short SF3KDV { get; set; }
        [Required]
        [Display(Name = "S F4 KDV")]
        public short SF4KDV { get; set; }
        [Required]
        [Display(Name = "S F5 KDV")]
        public short SF5KDV { get; set; }
        [Required]
        [Display(Name = "S F6 KDV")]
        public short SF6KDV { get; set; }
        [Required]
        [Display(Name = "Doviz S F1 KDV")]
        public short DovizSF1KDV { get; set; }
        [Required]
        [Display(Name = "Doviz S F2 KDV")]
        public short DovizSF2KDV { get; set; }
        [Required]
        [Display(Name = "Doviz S F3 KDV")]
        public short DovizSF3KDV { get; set; }
        [Required]
        [Display(Name = "S F1 Birim")]
        public short SF1Birim { get; set; }
        [Required]
        [Display(Name = "S F2 Birim")]
        public short SF2Birim { get; set; }
        [Required]
        [Display(Name = "S F3 Birim")]
        public short SF3Birim { get; set; }
        [Required]
        [Display(Name = "S F4 Birim")]
        public short SF4Birim { get; set; }
        [Required]
        [Display(Name = "S F5 Birim")]
        public short SF5Birim { get; set; }
        [Required]
        [Display(Name = "S F6 Birim")]
        public short SF6Birim { get; set; }
        [Required]
        [Display(Name = "Doviz S F1 Birim")]
        public short DovizSF1Birim { get; set; }
        [Required]
        [Display(Name = "Doviz S F2 Birim")]
        public short DovizSF2Birim { get; set; }
        [Required]
        [Display(Name = "Doviz S F3 Birim")]
        public short DovizSF3Birim { get; set; }
        [Required]
        [Display(Name = "Dvr Tarih")]
        public int DvrTarih { get; set; }
        [Required]
        [Display(Name = "Dvr Miktar")]
        public decimal DvrMiktar { get; set; }
        [Required]
        [Display(Name = "Dvr Tutar")]
        public decimal DvrTutar { get; set; }
        [Required]
        [Display(Name = "Gir Miktar")]
        public decimal GirMiktar { get; set; }
        [Required]
        [Display(Name = "Gir Tutar")]
        public decimal GirTutar { get; set; }
        [Required]
        [Display(Name = "Gir Iskonto")]
        public decimal GirIskonto { get; set; }
        [Required]
        [Display(Name = "Gir Tarih")]
        public int GirTarih { get; set; }
        [Required]
        [Display(Name = "Cik Miktar")]
        public decimal CikMiktar { get; set; }
        [Required]
        [Display(Name = "Cik Tutar")]
        public decimal CikTutar { get; set; }
        [Required]
        [Display(Name = "Cik Iskonto")]
        public decimal CikIskonto { get; set; }
        [Required]
        [Display(Name = "Cik Tarih")]
        public int CikTarih { get; set; }
        [Required]
        [Display(Name = "Dvz Dvr Tutar")]
        public decimal DvzDvrTutar { get; set; }
        [Required]
        [Display(Name = "Dvz Gir Tutar")]
        public decimal DvzGirTutar { get; set; }
        [Required]
        [Display(Name = "Dvz Gir Isk Tutar")]
        public decimal DvzGirIskTutar { get; set; }
        [Required]
        [Display(Name = "Dvz Cik Tutar")]
        public decimal DvzCikTutar { get; set; }
        [Required]
        [Display(Name = "Dvz Cik Isk Tutar")]
        public decimal DvzCikIskTutar { get; set; }
        [Required]
        [Display(Name = "Son Mly Sekli")]
        public short SonMlySekli { get; set; }
        [Required]
        [Display(Name = "Son Mly Birim Fiyat")]
        public decimal SonMlyBirimFiyat { get; set; }
        [Required]
        [Display(Name = "Son Mly Tarih")]
        public int SonMlyTarih { get; set; }
        [Required]
        [Display(Name = "Son Alim Fat Tarih")]
        public int SonAlimFatTarih { get; set; }
        [MaxLength(16)]
        [Required]
        [Display(Name = "Son Alim Evrak No")]
        public string SonAlimEvrakNo { get; set; }
        [Required]
        [Display(Name = "Son Alim BF")]
        public decimal SonAlimBF { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Son Alim CHK")]
        public string SonAlimCHK { get; set; }
        [Required]
        [Display(Name = "Alim Siparis")]
        public decimal AlimSiparis { get; set; }
        [Required]
        [Display(Name = "Satis Siparis")]
        public decimal SatisSiparis { get; set; }
        [Required]
        [Display(Name = "Gumruk Fon")]
        public decimal GumrukFon { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Gumruk GTIPN")]
        public string GumrukGTIPN { get; set; }
        [Required]
        [Display(Name = "Gumruk Vergi")]
        public Single GumrukVergi { get; set; }
        [Required]
        [Display(Name = "Gir Rezervasyon")]
        public decimal GirRezervasyon { get; set; }
        [Required]
        [Display(Name = "Cik Rezervasyon")]
        public decimal CikRezervasyon { get; set; }
        [Required]
        [Display(Name = "Gir Konsinye")]
        public decimal GirKonsinye { get; set; }
        [Required]
        [Display(Name = "Cik Konsinye")]
        public decimal CikKonsinye { get; set; }
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
        [MaxLength(50)]
        [Required]
        [Display(Name = "Butce Kodu")]
        public string ButceKodu { get; set; }
        [Required]
        [Display(Name = "Kart Turu")]
        public short KartTuru { get; set; }
        [Required]
        [Display(Name = "Use Sat Rezervasyon")]
        public short UseSatRezervasyon { get; set; }
        [Required]
        [Display(Name = "Use Sat Siparis")]
        public short UseSatSiparis { get; set; }
        [Required]
        [Display(Name = "Use Sat Fat Irs")]
        public short UseSatFatIrs { get; set; }
        [Required]
        [Display(Name = "Use Cikis Islem")]
        public short UseCikisIslem { get; set; }
        [Required]
        [Display(Name = "Use Set Urun")]
        public short UseSetUrun { get; set; }
        [Required]
        [Display(Name = "Use Alim Rezervasyon")]
        public short UseAlimRezervasyon { get; set; }
        [Required]
        [Display(Name = "Use Alim Siparis")]
        public short UseAlimSiparis { get; set; }
        [Required]
        [Display(Name = "Use Alim Irs Fat")]
        public short UseAlimIrsFat { get; set; }
        [Required]
        [Display(Name = "Use Giris Islem")]
        public short UseGirisIslem { get; set; }
        [Required]
        [Display(Name = "S F1 Valor Gun")]
        public short SF1ValorGun { get; set; }
        [Required]
        [Display(Name = "S F2 Valor Gun")]
        public short SF2ValorGun { get; set; }
        [Required]
        [Display(Name = "S F3 Valor Gun")]
        public short SF3ValorGun { get; set; }
        [Required]
        [Display(Name = "S F4 Valor Gun")]
        public short SF4ValorGun { get; set; }
        [Required]
        [Display(Name = "S F5 Valor Gun")]
        public short SF5ValorGun { get; set; }
        [Required]
        [Display(Name = "S F6 Valor Gun")]
        public short SF6ValorGun { get; set; }
        [Required]
        [Display(Name = "S F1 Dvz Valor Gun")]
        public short SF1DvzValorGun { get; set; }
        [Required]
        [Display(Name = "S F2 Dvz Valor Gun")]
        public short SF2DvzValorGun { get; set; }
        [Required]
        [Display(Name = "S F3 Dvz Valor Gun")]
        public short SF3DvzValorGun { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat Tip")]
        public short SatisFiyatTip { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat Alt Limit")]
        public decimal SatisFiyatAltLimit { get; set; }
        [Required]
        [Display(Name = "Satis Fiyat Ust Limit")]
        public decimal SatisFiyatUstLimit { get; set; }
        [Required]
        [Display(Name = "Son Sayim Tarih")]
        public int SonSayimTarih { get; set; }
        [Required]
        [Display(Name = "Son Sayim Sonuc")]
        public decimal SonSayimSonuc { get; set; }
        [Required]
        [Display(Name = "Son Sayim Fark")]
        public decimal SonSayimFark { get; set; }
        [MaxLength(64)]
        [Required]
        [Display(Name = "Notlar")]
        public string Notlar { get; set; }
        [Required]
        [Display(Name = "Blk Miktar")]
        public decimal BlkMiktar { get; set; }
        [Required]
        [Display(Name = "Elek Tic Sit List")]
        public short ElekTicSitList { get; set; }
        [Required]
        [Display(Name = "Web Mag Sit List")]
        public short WebMagSitList { get; set; }
        [Required]
        [Display(Name = "Dag Zin Sit List")]
        public short DagZinSitList { get; set; }
        [Required]
        [Display(Name = "Sir Ici Sip Sit List")]
        public short SirIciSipSitList { get; set; }
        [Required]
        [Display(Name = "Elek Tic Sit List Adi")]
        public short ElekTicSitListAdi { get; set; }
        [Required]
        [Display(Name = "Web Mag Sit List Adi")]
        public short WebMagSitListAdi { get; set; }
        [Required]
        [Display(Name = "Dag Zin Sit List Adi")]
        public short DagZinSitListAdi { get; set; }
        [Required]
        [Display(Name = "Sir Ici Sip Sit List Adi")]
        public short SirIciSipSitListAdi { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Demirbas Kodu")]
        public string DemirbasKodu { get; set; }
        [Required]
        [Display(Name = "Miktar Takibi")]
        public short MiktarTakibi { get; set; }
        [Required]
        [Display(Name = "Bak Gost Sekli")]
        public short BakGostSekli { get; set; }
        [Required]
        [Display(Name = "Kart Tip")]
        public short KartTip { get; set; }
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
        [Required]
        [Display(Name = "Alim Teklif")]
        public decimal AlimTeklif { get; set; }
        [Required]
        [Display(Name = "Satis Teklif")]
        public decimal SatisTeklif { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Sat Mal Maliyet Hesap")]
        public string SatMalMaliyetHesap { get; set; }
        [Required]
        [Display(Name = "Aktif Pasif")]
        public short AktifPasif { get; set; }
        [Required]
        [Display(Name = "Tevfikat Oran")]
        public short TevfikatOran { get; set; }
        [Required]
        [Display(Name = "Son Alim Net BF")]
        public decimal SonAlimNetBF { get; set; }
        [Required]
        [Display(Name = "Son Alim Dvz BF")]
        public decimal SonAlimDvzBF { get; set; }
        [Required]
        [Display(Name = "Son Alim Dvz Net BF")]
        public decimal SonAlimDvzNetBF { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "YD Alimlar Hesabi")]
        public string YDAlimlarHesabi { get; set; }
        [MaxLength(7)]
        [Required]
        [Display(Name = "Tevkifat Alis")]
        public string TevkifatAlis { get; set; }
        [MaxLength(7)]
        [Required]
        [Display(Name = "Tevkifat Satis")]
        public string TevkifatSatis { get; set; }
        [MaxLength(5)]
        [Required]
        [Display(Name = "Tevkifat Alis Tam")]
        public string TevkifatAlisTam { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod14")]
        public string Kod14 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod15")]
        public string Kod15 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod16")]
        public string Kod16 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod17")]
        public string Kod17 { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Kod18")]
        public string Kod18 { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Birim4")]
        public string Birim4 { get; set; }
        [Required]
        [Display(Name = "Operator4")]
        public short Operator4 { get; set; }
        [Required]
        [Display(Name = "Kat Sayi4")]
        public double KatSayi4 { get; set; }
        [Required]
        [Display(Name = "En")]
        public decimal En { get; set; }
        [Required]
        [Display(Name = "Boy")]
        public decimal Boy { get; set; }
        [Required]
        [Display(Name = "Genislik")]
        public decimal Genislik { get; set; }
        [MaxLength(4)]
        [Required]
        [Display(Name = "Boyut Birim")]
        public string BoyutBirim { get; set; }
        [Required]
        [Display(Name = "Brut Agirlik")]
        public decimal BrutAgirlik { get; set; }
        [Required]
        [Display(Name = "Net Agirlik")]
        public decimal NetAgirlik { get; set; }
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
        [Required]
        [Display(Name = "YOKCPLU Gonder")]
        public short YOKCPLUGonder { get; set; }
        [Required]
        [Display(Name = "YOKC Departman ID")]
        public short YOKCDepartmanID { get; set; }
        [Required]
        [Display(Name = "YOKCPLUID")]
        public int YOKCPLUID { get; set; }
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
