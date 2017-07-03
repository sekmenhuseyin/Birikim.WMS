namespace Wms12m
{
    /// <summary>
    /// list status
    /// </summary>
    public enum GetListStatus
    {
        Refresh = 0,
        Close = 1
    }
    /// <summary>
    /// combo items
    /// </summary>
    public enum ComboItems
    {
        MalKabul = 1,
        RafaKaldır = 2,
        SiparişTopla = 3,
        BarkodHazırla = 4,
        İrsaliyeKes = 5,
        Paketle = 6,
        Sevket = 7,
        KontrolSayım = 8,
        Açık = 9,
        Durdurulan = 10,
        Tamamlanan = 11,
        Bobin = 12,
        KabloKanalı = 13,
        MuhtelifKoli = 14,
        Başlamamış = 15,
        EArşiv = 16,
        EFatura = 17,
        NormalFatura = 18,
        TransferÇıkış = 19,
        TransferGiriş = 20,
        WMS = 21,
        EnÜstMenü = 22,
        ÜstMenü = 23,
        SolMenü = 24,
        SağMenü = 25,
        FooterMenü = 26,
        Oluşturuldu = 27,
        Atandı = 28,
        Başlandı = 29,
        OnayVer = 30,
        KaliteKontrol = 31,
        Beklemede = 32,
        Bitti = 33,
        Onaylandı = 34,
        Reddedildi = 35,
        Durduruldu = 36,
        DüşükÖncelik = 37,
        NormalÖncelik = 38,
        AcilÖncelik = 39,
        depMuhasebe = 40,
        depSatış = 41,
        depSatınalma = 42,
        depIK = 43,
        depIT = 44,
        depSevkiyat = 45,
        depDepo = 46,
        depYönetim = 47,
        depFinans = 48,
        depGenel = 49,
        gytKritikHata = 50,
        gytBilgiTalebi = 51,
        gytStandart = 52,
        gytGeliştirme = 53,
        gytKaliteKontrol = 54,
        alEkle = 55,
        alDüzenle = 56,
        alSil = 57,
        alZiyaret = 58,
        alİndir = 59,
        alYükle = 60,
        alOnayla = 61,
        alRed = 62,
        ptMakara = 63,
        ptKoli = 64,
        ptKangal = 65,
        ptÇuval = 66,
        ptBağ = 67,
        ptPoşetPaket = 68,
        ptPalet = 69
    }
    /// <summary>
    /// combo item names
    /// </summary>
    public enum Combos
    {
        GorevTipleri = 1,
        GorevDurum = 2,
        Özellik = 3,
        EvrakSeriTipi = 4,
        SiteTipi = 5,
        MenuYeri = 6,
        GörevYönetimDurumları = 7,
        Öncelik = 8,
        Departman = 9,
        GörevYönetimTipleri = 10,
        İşlemKayıtTipi = 11,
        PaketTipi = 12
    }
    /// <summary>
    /// permission types
    /// </summary>
    public enum PermTypes
    {
        Reading,
        Writing,
        Updating,
        Deleting
    }
    /// <summary>
    /// yetkiler
    /// </summary>
    public enum Perms
    {
        Gruplar,
        GrupYetkileri,
        Kullanıcılar,
        TerminalİçinYetkilendirme,
        DepoKartı,
        KoridorKartı,
        RafKartı,
        BölümKartı,
        KatKartı,
        BoyutKartı,
        GörevListesi,
        MalKabul,
        GenelSipariş,
        KabloSiparişi,
        KontrollüSayım,
        Stok,
        Transfer,
        SiparişOnaylama,
        StokOnaylama,
        FiyatOnaylama,
        FaturaOnaylama,
        RiskOnaylama,
        TeminatOnaylama,
        ÇekOnaylama,
        SatinalmaOnaylama,
        SözleşmeOnaylama,
        Raporlar,
        TechnoIKOnaylama,
        TerminalMalKabul,
        TerminalRafaKaldır,
        TerminalPaketle,
        TerminalSiparişTopla,
        TerminalKontrollüSayım,
        TerminalTransferÇıkış,
        TerminalTransferGiriş,
        Menu,
        Menü,
        ChartGunlukSatis,
        ChartGunlukSatisPie,
        ChartGunlukSatisYearToDay,
        ChartGunlukSatisYearToDayPie,
        ChartGunlukSatisDoubleKriter,
        ChartGunlukSatisDoubleKriterPie,
        ChartAylikSatisAnaliziBar,
        ChartAylikSatisCHKAnaliziBar,
        ChartAylikSatisAnaliziKodTipDovizBar,
        ChartUrunGrubuSatis,
        ChartUrunGrubuSatisKriter,
        ChartLokasyonSatis,
        ChartLokasyonSatisKriter,
        ChartBakiyeRiskAnalizi,
        ChartBekleyenSiparisUrunGrubu,
        ChartBekleyenSiparisUrunGrubuMiktar,
        ChartBekleyenSiparisUrunGrubuMiktarPie,
        ChartBekleyenSiparisUrunGrubuMiktarKriter,
        ChartBekleyenSiparisUrunGrubuMiktarKriterPie,
        ChartBekleyenSiparisMusteriAnalizi,
        ChartSatisTemsilcisiAylikSatisAnalizi,
        ChartBaglantiUrunGrubu,
        ChartBaglantiUrunGrubuPie,
        ChartGunlukMDFUretimi,
        ChartGunlukMDFUretimiPie,
        ChartBaglantiZamanCizelgesi,
        ChartBolgeBazliSatisAnalizi,
        ChartBolgeBazliSatisAnaliziPie
    }
}
