namespace Wms12m
{
    /// <summary>
    /// list status
    /// </summary>
    public enum GetListStatus
    {            
        Refresh=0,
        Close=1
    }
    /// <summary>
    /// combo items
    /// </summary>
    public enum ComboItems
    {
        MalKabul = 1,
        RafaKaldır = 2,
        SiparişTopla = 3,
        KabloSiparişi = 4,
        İrsaliyeKes = 5,
        Paketle = 6,
        Sevkiyat = 7,
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
        Normal = 18,
        TransferÇıkış = 19,
        TransferGiriş = 20,
        WMS = 21,
        EnÜstMenü = 22,
        ÜstMenü = 23,
        SolMenü = 24,
        SağMenü = 25,
        FooterMenü = 26
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
        MenuYeri = 6
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
}
