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
}
