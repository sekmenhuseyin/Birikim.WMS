using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KurumsalKaynakPlanlaması12M
{

    #region KartTip_CHK

    public enum KKPKartTip_CHK
    {
        Müşteri=0,
        Satıcı=1,
        Banka=2,
        Kasa=3,
        Diğer=4,
    }

    #endregion

    #region KartTip_STK

    public enum KKPKartTip_STK
    {
        TicariMal = 0,
        Mamül = 1,
        YarıMamül = 2,
        SarfMalzemesi = 3,
        SabitKıymet = 4,
        Hizmet = 5,
        Masraf = 6,
        VadeFarkı_KurFarkı = 7,
        Diğer = 8,
    }

    #endregion

    #region KartTuru

    public enum KKPKartTuru
    {
        StokKartı = 0,
        KarmaKoli = 1,
    }

    #endregion


    #region KynkEvrakTip

    public enum KKPKynkEvrakTip
    {
        Satışİrsaliyesi = 0,
        SatışFaturası = 1,
        SatıştanİadeFaturası = 2,
        Alımİrsaliyesi = 3,
        AlımFaturası = 4,
        AlımdanİadeFaturası = 5,
        TahsilatMakbuzu = 6,
        TediyeMakbuzu = 7,
        AlacakDekontu = 8,
        BorçDekontu = 9,
        GelenHavale = 10,
        GönderilenHavale = 11,
        VirmanFişi = 12,
        AlacakSenetleri_SenetAlımBordrosu = 13,
        AlacakSenetleri_İadeEdildi = 14,
        AlacakSenetleri_Protesto = 15,
        AlacakSenetleri_KasadanTahsilat = 16,
        AlacakSenetleri_Ciro = 17,
        AlacakSenetleri_İadeAlındı = 18,
        AlacakSenetleri_TahsilataÇıkış = 19,
        AlacakSenetleri_TeminataÇıkış = 20,
        AlacakSenetleri_BankadanTahsilat = 21,
        AlacakSenetleri_Ödenmedi = 22,
        AlacakSenetleri_Kayıp = 23,
        AlacakSenetleri_İcrayaVerme = 24,
        AlınanÇekler_ÇekAlımBordrosu = 25,
        AlınanÇekler_İadeEdildi = 26,
        AlınanÇekler_Karşılıksız = 27,
        AlınanÇekler_KasadanTahsilEdilen = 28,
        AlınanÇekler_Ciro = 29,
        AlınanÇekler_İadeAlındı = 30,
        AlınanÇekler_TahsilataÇıkış = 31, //Takasa Verilen Çekler
        AlınanÇekler_TeminataÇıkış = 32,
        AlınanÇekler_BankadanTahsilat = 33,
        AlınanÇekler_Ödenmedi = 34,
        AlınanÇekler_Kayıp = 35,
        AlınanÇekler_İcrayaVerme = 36,
        BorçSenetleri_Ödeme = 37,
        BorçSenetleri_İadeAlındı = 38,
        BorçSenetleri_İadeEdildi = 39,
        BorçSenetleri_KasadanÖdeme = 40,
        BorçSenetleri_TeminataÇıkış = 41,
        BorçSenetleri_BankadanÖdeme = 42,
        BorçSenetleri_Ödenmedi = 43,
        BorçSenetleri_Protesto = 44,
        VerilenÇekler_Ödeme = 45,
        VerilenÇekler_İadeAlındı = 46,
        VerilenÇekler_İadeEdildi = 47,
        VerilenÇekler_KasadanÖdeme = 48,
        VerilenÇekler_TeminataÇıkış = 49,
        VerilenÇekler_BankadanÖdeme = 50,
        VerilenÇekler_Karşılıksız = 51,
        İçKullanımFişi = 52,
        DepolarArasıTransferFişi = 53,
        AlınanNumune_HediyeFişi = 54,
        VerilenNumune_HediyeFişi = 55,
        FireFişi = 56,
        SayımFarkıFişi = 57,
        StokGirişFişi = 58,
        StokÇıkışFişi = 59,
        KasaTahsilFişi = 60,
        KasaTediyeFişi = 61,
        SatışSiparişi = 62,
        AlımSiparişi = 63,
        PerakendeSatışFaturası = 64,
        SiparişeGöreFaturalama = 65,
        AlımRezervasyonu = 66,
        SatışRezervasyonu = 67,
        Alımdanİadeİrsaliyesi = 68,
        Satıştanİadeİrsaliyesi = 69,
        Diğer = 70,
        FiyatListesiKartı = 71,
        MamülAğacıGirişi = 72,
        ÜretimEmirFişi = 73,
        Üretimİşlemleri = 74,
        AlacakÖdemePlanı = 75,
        BorçÖdemePlanı = 76,
        Bütçeİşlemleri = 77,
        BankaBorçDekontu = 78,
        BankaGelenHavale = 79,
        BankaAlacakDekontu = 80,
        BankaGönderilenHavale = 81,
        KarmaKoli = 82,
        AçılışFişiİşlemleri = 83,
        DönemselFişİşlemleri = 84,
        SabitKıymetSatışİşlemleri = 85,
        BankaTeminatMektubu = 86,
        SabitKıymetGiderİşlemleri = 87,
        MuhasebeAçılışFişi = 88,
        MuhasebeKapanışFişi = 89,
        MuhasebeTahsilFişi = 90,
        MuhasebeTediyeFişi = 91,
        MuhasebeMahsupFişi = 92,
        TeminatMektubu = 93,
        SayımSonuçFişi = 94,
        DepoSayımSonuçFişi = 95,
        CH_Devirİşlemi = 96,
        AlınanÇekler_TransferFişi = 97,
        AlacakSenetleri_TransferFişi = 98,
        GiderPusulası = 99,
        DepoSayımFarkıFişi = 100,
        ArbitrajFişi = 101,
        DepolarArasıAltTransferFişi = 102,
        KonsinyeVerilenMalİrsaliyesi = 103,
        KonsinyeVerilenMalİadeİrsaliyesi = 104,
        KonsinyeAlınanMalİrsaliyesi = 105,
        KonsinyeAlınanMalİadeİrsaliyesi = 106,
        EnflasyonDüzeltmesineGöreAçılışFişi = 107,
        EnflasyonDüzeltmesineGöreDönemselFişi = 108,
        EnflasyonDüzeltmeFişi = 109,
        FinansmanGiderFişi = 110,
        HisseSenetleriTablosu = 111,
        TarihiDeğerlerTablosu = 112,
        DönemSonuDüzeltilmişDeğerler = 113,
        AktifleşmedenÖnceYapılanHarcamalar = 114,
        EnflasyonDüzeltmeTarsİşlemFişi = 115,
        SerbestMeslekMakbuzu = 116,
        PerakendeSatıştanİadeFaturası = 117,
        GiderÖdemeFişi = 118,
        BankaGiderÖdemeFişi = 119,
        PerakendeAlımFaturası = 120,
        GiderFaturası = 121,
        TeklifİstemeFişi = 122,
        SatışTeklifi = 123,
        SatınAlmaTeklifi = 124,
        HizmetSatışFaturası = 125,
        HizmetSatıştanİadeFaturası = 126,
        HizmetAlımFaturası = 127,
        HizmetAlımdanİadeFaturası = 128,
        HizmetSatışSiparişi = 129,
        HizmetAlımSiparişi = 130,
        SabitKıymetSatışFaturası = 131,
        SabitKıymetSatıştanİadeFaturası = 132,
        SabitKıymetAlımFaturası = 133,
        SabitKıymetAlımdanİadeFaturası = 134,
        SabitKıymetSatışSiparişi = 135,
        SabitKıymetAlımSiparişi = 136,
        MalzemeTalepFormu = 137,
        ÖnSatışTeklifi = 138,
        MasrafFişi = 139,
        MuhasebeSonuçAktarımFişi = 140,
        SatışlarİçinAlımFiyatFarkıFaturası = 141,
        AlımlarİçinKesilenFiyatFarkıFaturası = 142,
        SatışlarİçinKesilenFiyatFarkıFaturası = 143,
        AlımFiyatFarkıFaturası = 144,
        CariDevirFişi = 145,
        BankaTeminatMektubuİptal_İade = 146,
        MalzemeTeslimFişi = 147,

    }
    #endregion


    #region IslemTip_CHI

    //select * from FINSAT633.COMBO_NAME(NOLOCK) WHERE CBONAME='IslemTip'


    //SELECT * FROM FINSAT633.COMBOITEM_NAME(NOLOCK) WHERE ITEMCBOID='8'
    public enum KKPIslemTip_CHI
    {
        Devir = 0,
        Nakit = 1,
        Havale = 2,
        Virman = 3,
        Fatura = 4,
        İskonto = 5,
        KDV = 6,
        Dekont = 7,
        İadeFaturası = 8,
        İadeFaturasıİskonto = 9,
        İadeFaturasıKDV = 10,
        Senet = 11,
        ProtestoluSenet = 12,
        SenetProtestoMasrafı = 13,
        SenetEksikPul = 14,
        Senetİade = 15,
        SenetTahsilatı = 16,
        Çek = 17,
        KarşılıksızÇek = 18,
        ÇekTahsilMasrafı = 19,
        Çekİade = 20,
        ÇekTahsilatı = 21,
        BorçSenedi = 22,
        ProtestoluBorçSenedi = 23,
        BorçSenediProtestoMasrafı = 24,
        BorçSenediEksikPul = 25,
        BorçSenediİade = 26,
        BorçSenediTahsilatı = 27,
        BorçÇeki = 28,
        KarşılıksızBorçÇeki = 29,
        BorçÇekiTahsilMasrafı = 30,
        BorçÇekiİade = 31,
        BorçÇekiTahsilatı = 32,
        Diğer1 = 33,
        Diğer2 = 34,
        Diğer3 = 35,
        Teminat1 = 36,
        Teminat2 = 37,
        KDVTevkifatı = 38,
        YuvarlamaFarkı = 39,
        KrediKartı = 40,
        TeminatMektubu = 41,
        TeminatMektubuMasrafı = 42,
        ÖzelTüketimVergisi = 43,
        ÇekTransferi = 44,
        SenetTransferi = 45,
        GelirVergisi = 46,
        FonPayı = 47,
        Gider = 48,
        ÖdemeKaydediciCihazFişi = 49,
        ÖİV = 50,
        Stopaj = 51,
        BankaTeminatMektubuİptal_İadesi = 52,
        KrediKartıileÖdeme = 53,
        KrediKartıileTahsilat = 54,
        KrediKartıKomisyon_Masraf = 55,
    }

    #endregion

    #region IslemTip_STI

    //SELECT * FROM FINSAT633.COMBOITEM_NAME(NOLOCK) WHERE ITEMCBOID='57'
    public enum KKPIslemTipSPI
    {
        Devir = 0,
        İçPiyasa = 1,
        DışPiyasa = 2,
        İadeEdildi = 3,
        Üretim = 4,
        Fason = 5,
        Transfer = 6,
        Numune_Hediye = 7,
        Fire = 8,
        KonsinyeVerilen = 9,
        SayımFarkı = 10,
        İçKullanım = 11,
        Maliyet = 12,
        Diğer1 = 13,
        Diğer2 = 14,
        KarmaKoli = 15,
        KarmaKoliDetay = 16,
        SayımSonuçFişi = 17,
        DepoyaGöreSayımSonuçFişi = 18,
        KonsinyeAlınan = 19,
        DepoyaGöreSayımFarkıFişi = 20,
        FinansmanGiderFişi = 21,
        FiyatFarkı = 22,
    }

    #endregion

    #region IslemTip_SPI

    //SELECT * FROM FINSAT633.COMBOITEM_NAME(NOLOCK) WHERE ITEMCBOID='72'
    //public enum IslemTip_SPI
    //{
    //    Fatura = 0,
    //    İadeFaturası = 1,
    //    İrsaliye = 2,
    //    AlımSiparişi = 3,
    //    SatışSiparişi = 4,
    //    PerakendeSatışFaturası = 5,
    //    SiparişeGöreSatışFaturası = 6,
    //    AlımRezervasyonu = 7,
    //    SatışRezervasyonu = 8,
    //    İadeİrsaliyesi = 9,
    //    Konsinyeİrsaliyesi = 10,
    //    Konsinyeİadeİrsaliyesi = 11,
    //    SerbestMeslekMakbuzu = 12,
    //    PerakendeSatıştanİadeFaturası = 13,
    //    GiderFaturası = 14,
    //    SatışTeklifi = 15,
    //    SatınalmaTeklifi = 16,
    //    HizmetFaturası = 17,
    //    HizmetİadeFaturası = 18,
    //    HizmetAlımSiparişi = 19,
    //    HizmetSatışSiparişi = 20,
    //    SabitKıymetFaturası = 21,
    //    SabitKıymetİadeFaturası = 22,
    //    SabitKıymetAlımSiparişi = 23,
    //    SabitKıymetSatışSiparişi = 24,
    //    AlımFiyatFarkıFaturası = 25,
    //    SatışFiyatFarkıFaturası = 26,
    //    MüstahsilMakbuzu = 27,
    //}

    #endregion

    #region SonIslemTip

    public enum KKPSonIslemTip
    {
        PortföyeGiriş = 0,
        Ödeme = 1,
        Ciro = 2,
        TeminataÇıkış = 3,
        TahsilataÇıkış = 4,
        İadeEdildi = 5,
        Kayıp = 6,
        Ödenmedi = 7,
        Karşılıksız = 8,
        Protesto = 9,
        İadeAlındı = 10,
        İcrayaVerme = 11,
        BankadanTahsilEdildi = 12,
        KasadanTahsilEdildi = 13,
        BankadanÖdeme = 14,
        KasadanÖdeme = 15,
        Transfer = 16,
    }

    #endregion


    #region IslemTur

    public enum KKPIslemTur
    {
        Giriş=0,
        Çıkış = 1,

        //Alış=0,        
        //Satış=1,
    }

    #endregion

    #region SatirTip

    public enum KKPSatirTip
    {
        MalBedeli = 0,
        KDVDahilMalBedeli = 1,
        KalemlerdeOranİskontosu = 3,
        KalemlerdeTutarİskontosu = 4,
        Promosyonİskontosu = 5,
        Oranİskontosu = 6,
        Tutarİskontosu = 7,
        İskontoToplamı = 8,
        KDV = 9,
        KDVToplamı = 10,
        AraToplam = 11,
        Toplam = 12,
        Kalemdeİçİskonto = 13,
        İçİskonto = 14,
        KalemlerdeOranİskontosu_1 = 15,
        KDVTevkifatı = 16,
        YuvarlamaFarkı = 17,
        KalemlerdeOranİskontosu_2 = 18,
        KalemlerdeOranİskontosu_3 = 19,
        KalemlerdeOranİskontosu_4 = 20,
        KalemlerdeOranİskontosu_5 = 21,
        ÖzelTüketimVergisi = 22,
        KDVDahilTutarİskontosu = 23,
        ÖzelİletişimVergisi = 24,
        Stopaj = 25,
        Diğer = 26,
        SSDF = 27,
        Bağkur = 28,
        Borsa = 29,
        ORGE = 30,
    }

    #endregion

    #region AktifPasif

    public enum KKPAktifPasif
    {
        Pasif=0,
        Aktif=1,
    }

    #endregion


    #region Durumu
    public enum KKPSiparisDurumu
    {
        Açık = 0,
        Kapalı = 1,
    }
    #endregion

    #region TeklifDurumu

    public enum KKPTeklifDurumu
    {
        Açık=0,
        Kapalı=1,
    }
    #endregion

    #region Sipariş Tip    
    public enum KKPSiparisTip
    {
        SatisSiparisi = 62,
        AlimSiparisi = 63
    }
    #endregion
    public enum KKPIrsaliyeTip
    {
        SatisIrsaliyesi=0,
        AlimIrsaliyesi=3
    }


    #region STok Giriş Çıkış Tip
    
    public enum KKPStokGirisCikisTip
    {
        Giris = 58,
        Cikis = 59
    }
    #endregion

    #region DvzTL

    public enum KKPDvzTL
    {
        YerelPara = 0,
        Döviz = 1,
        YerelPara_Döviz = 2,
    }

    #endregion

    #region BA

    public enum KKPBA
    {
        Borç=0,
        Alacak=1,
    }

    #endregion

    #region FiyatTuru

    public enum KKPFiyatTuru
    {
        KoduBelirtilenMüşterilereUygulansın = 0,
        TümMüşterilereUygulansın = 1,
    }
#endregion

    #region KodTipi

    public enum KKPKodTipi
    {
        CHKodu = 0,
        GrupKodu = 1,
        ÖzelKod = 2,
        TipKodu = 3,
        BölgeKodu = 4,
        GüvenlikKodu = 5,
        Kod1 = 6,
        Kod2 = 7,
        Kod3 = 8,
        Kod4 = 9,
        Kod7 = 10,
        Kod8 = 11,
        Kod9 = 12,
        MuhasebeKodu = 13,
        FaturaCHK = 14,
    }
#endregion


    #region MusUygSekli

    public enum KKPMusUygSekli
    {
        TümHesaplaraUygulansın = 0,
        KoduBelirtilenHesaplaraUygulansın = 1,
    }
    #endregion

    #region MusKodGrup

    public enum KKPMusKodGrup
    {
        HesapKodu = 0,
        BölgeKodu = 1,
        GrupKodu = 2,
        TipKodu = 3,
        ÖzelKod = 4,
        Kod1 = 5,
        Kod2 = 6,
        Kod3 = 7,
        Kod4 = 8,
        Kod5 = 9,
        Kod6 = 10,
        Kod7 = 11,
        Kod8 = 12,
        Kod9 = 13,
        Kod10 = 14,
        Kod11 = 15,
        Kod12 = 16,
        Kod13 = 17,
        FaturaCHK = 18,
    }
    #endregion


    #region MalUygSekli

    public enum KKPMalUygSekli
    {
        TümMallaraUygulansın = 0,
        KoduBelirtilenMallaraUygulansın = 1,
    }
    #endregion

    #region MalKodGrup

    public enum KKPMalKodGrup
    {
        MalKodu = 0,
        GrupKodu = 1,
        TipKodu = 2,
        ÖzelKodu = 3,
        Kod1 = 4,
        Kod2 = 5,
        Kod3 = 6,
        Kod4 = 7,
        Kod5 = 8,
        Kod6 = 9,
        Kod7 = 10,
        Kod8 = 11,
        Kod9 = 12,
        Kod10 = 13,
        Kod11 = 14,
        Kod12 = 15,
        Kod13 = 16,
    }
    #endregion


    #region MiktarTakibi

    public enum MiktarTakibi
    {
        Miktarsız=0,
        Miktarlı=1,
    }
    #endregion

    #region BakGostSekli

    public enum KKPBakGostSekli
    {
        YTL_TL = 0,
        Döviz = 1,
        _2005öncesiTL = 2,
    }
    #endregion


    #region IrsFat

    public enum KKPIrsFat
    {
        İrsaliye = 0,
        Fatura = 1,
        Faturalananİrsaliye = 2,
        İadeEdilenFatura = 3,
        İadeEdilenİrsaliye = 4,
    }
    #endregion

    #region IrsFat2

    public enum KKPIrsFat2
    {
        İrsaliye = 0,
        Fatura = 1,
        Faturalananİrsaliye = 2,
        İadeEdilenFatura = 3,
        İadeEdilenİrsaliye = 4,
        Faturalananİadeİrsaliyesi = 5,
        SevkEdilenSipariş = 6,
        FaturalananSipariş = 7,
    }
    #endregion


    #region OnayStatus

    public enum KKPOnayStatus
    {
        OnayBeklemede = 0,
        Onaylandı = 1,
        Reddedildi = 2,
    }
    #endregion

    #region KDVDahil

    public enum KKPKDVDahil
    {
        Hayır=0,
        Evet=1,
    }
    #endregion

    #region FaizTip

    public enum KKPFaizTip
    {
        Aylık=0,
        Yıllık=1,
    }
    #endregion


    #region Cinsiyet

    public enum KKPCinsiyet
    {
        Erkek=0,
        Kadın=1,
    }
    #endregion

    #region MedHal

    public enum KKPMedHal
    {
        Bekar=0,
        Evli=1,
    }
#endregion


    #region Kayıt Kaynak
    /// <summary>
    ///   select * FROM [FINSAT616].[FINSAT616].[COMBOITEM_NAME] WHERE ITEMCBOID=2
    /// </summary>
    public enum KKPKayitKaynak
    {
        SatisIrsaliye = 71,
        AlimIrsaliye = 74,
        StokGirisFisi = 83,
        StokCikisFisi = 84,
        SatisSiparisi = 107,
        AlimSiparisi = 108
    }
    #endregion Kayıt Kaynak

}
