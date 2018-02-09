namespace Wms12m.Entity
{
    /// <summary>
    /// sipariş onay formu
    /// </summary>
    public class frmSiparisMalzemeOnay
    {
        public string Aciklama { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public string Chk { get; set; }
        public int DegisSaat { get; set; }
        public string EvrakNo { get; set; }
        public string ID { get; set; }
        public string Kod15 { get; set; }
        public string MalAdi { get; set; }
        public string MalAdi4 { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }

        //marka
        public string Nesne2 { get; set; }

        public int ROW_ID { get; set; }
        public short SiraNo { get; set; }
        public string SirketID { get; set; }
        public decimal Stok { get; set; }
        public int Tarih { get; set; }
        public string TeslimChk { get; set; }
        public string Unvan { get; set; }
        public short ValorGun { get; set; }
        //cins
        //kesit
    }
}