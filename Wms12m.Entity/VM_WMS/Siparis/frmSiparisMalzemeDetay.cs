namespace Wms12m.Entity
{
    /// <summary>
    /// Siparişlerin malzeme detay
    /// </summary>
    public class frmSiparisMalzemeDetay
    {
        public string Birim { get; set; }
        public string EvrakNo { get; set; }
        public decimal GunesStok { get; set; }
        public int ID { get; set; }
        public string MalAdi { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public int Saat { get; set; }
        public string SirketID { get; set; }
        public int Tarih { get; set; }
        public string Unvan { get; set; }
        public decimal WmsRezerv { get; set; }
        public decimal WmsStok { get; set; }
        public decimal SayimFarki { get; set; }  // Miktar - WmsStok
        public static string SorguEksikListe = @"EXEC FINSAT6{0}.wms.getSayimEksikList @ID = {1}";
        public static string SorguTumFarkListe = @"EXEC FINSAT6{0}.wms.getSayimList @ID = {1}, @FarkMi = {2}";
        public static string SorguWmsMiktar = @"SELECT ISNULL(BIRIKIM.wms.fnGetStockByID((SELECT TOP 1 G.DepoID FROM BIRIKIM.wms.Gorev AS G WITH (NOLOCK) WHERE G.ID={0}), '{1}',''),0)";
    }
}