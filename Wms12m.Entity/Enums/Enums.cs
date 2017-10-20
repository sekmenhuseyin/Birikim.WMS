namespace Wms12m
{
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
        PaketTipi = 12,
        TatilTipi = 13,
        DestekTipi = 14,
        MesajTipi = 15,
        FaqTopics = 16
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
        gydOluşturuldu = 27,
        gydAtandı = 28,
        gydBaşlandı = 29,
        gydOnayVer = 30,
        gydKaliteKontrol = 31,
        gydBeklemede = 32,
        gydBitti = 33,
        gydOnaylandı = 34,
        gydReddedildi = 35,
        gydDurduruldu = 36,
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
        ptPalet = 69,
        Bakır = 70,
        KüçükRaf = 71,
        Alımdanİade = 72,
        Satıştanİade = 73,
        Yıllıkİzin = 74,
        Mazaretİzni = 75,
        ResmiTatil = 76,
        İdariİzin = 77,
        ÖnemliGün = 78,
        LinkGünesDestek = 79,
        LinkTechnoDestek = 80,
        LinkYNSDestek = 81,
        SatınalmaModülüDestek = 82,
        GrupMesajı = 83,
        KişiselMesaj = 84,
        DuyuruMesajı = 85,
        UYSModülüDestek = 86,
        WMSModülüDestek = 87,
        SiparişModülüDestek = 88,
        B2BModülüDestek = 89,
        LinkCampusDestek = 90,
        LinkeDefterDestek = 91,
        LinkeFaturaDestek = 92,
        BarkodOtomasyonDestek = 93,
        BackOfficeDestek = 94,
        ElTerminaliDestek = 95,
        FaqGenel = 96,
        FaqTeknik = 97,
        FaqYönetim = 98
    }
    /// <summary>
    /// list status
    /// </summary>
    public enum GetListStatus
    {
        Refresh = 0,
        Close = 1
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
        AlimdanIade,
        SatistanIade,
        Stok,
        Transfer,
        SiparişOnaylama,
        SiparişOnaylamaSM,
        SiparişOnaylamaSPGMY,
        SiparişOnaylamaGM,
        StokOnaylama,
        FiyatOnaylama,
        FiyatTanim,
        FiyatOnaylamaSM,
        FiyatOnaylamaSPGMY,
        FiyatOnaylamaGM,
        FaturaOnaylama,
        RiskOnaylama,
        RiskTanim,
        RiskOnaylamaSM,
        RiskOnaylamaSPGMY,
        RiskOnaylamaMIGMY,
        RiskOnaylamaGM,
        TeminatOnaylama,
        TeminatTanim,
        TeminatOnay,
        ÇekOnaylama,
        ÇekOnaylamaSPGMY,
        ÇekOnaylamaMIGMY,
        ÇekOnaylamaGM,
        SatinalmaOnaylama,
        SözleşmeOnaylama,
        SözleşmeTanim,
        SözleşmeOnaylamaSM,
        SözleşmeOnaylamaSPGMY,
        SözleşmeOnaylamaGM,
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
        ChartAylikSatisAnaliziBar,
        ChartUrunGrubuSatis,
        ChartLokasyonSatis,
        ChartBakiyeRiskAnalizi,
        ChartBekleyenSiparisUrunGrubu,
        ChartSatisTemsilcisiAylikSatisAnalizi,
        ChartBaglantiUrunGrubu,
        ChartGunlukMDFUretimi,
        ChartBaglantiZamanCizelgesi,
        ChartBolgeBazliSatisAnalizi,
        TodoMüşteri,
        TodoProje,
        TodoTakvim,
        TodoÇalışma,
        TodoGörevler,
        TodoTroubleshooting,
        TerminalAlimdanİade,
        TerminalSatıştanİade        
    }
    /// <summary>
    /// aylar
    /// </summary>
    public enum Aylar
    {
        Tümü = 0,
        Ocak = 1,
        Şubat = 2,
        Mart = 3,
        Nisan = 4,
        Mayıs = 5,
        Haziran = 6,
        Temmuz = 7,
        Ağustos = 8,
        Eylül = 9,
        Ekim = 10,
        Kasım = 11,
        Aralık = 12
    }
    /// <summary>
    /// fatura tipleri
    /// </summary>
    public enum FaturaTipi
    {
        SatisFaturası = 0,
        AlimdanIadeFaturası = 1,
        SatistanIadeIrsaliyesi = 2
    }
}
