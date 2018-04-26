namespace Wms12m.Entity
{
    public class MySti
    {
        public string Chk { get; set; }
        public string EvrakNo { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public int Tarih { get; set; }

        public short SiraNo { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Tutar { get; set; }
        public string Depo { get; set; }

        public string Unvan { get; set; }
        public string Unvan1 { get; set; }
        public string DepoAdi { get; set; }

        public short KynkEvrakTip { get; set; }

        public decimal DepoStokMiktar { get; set; }
        /// <summary>
        /// STok Yaşlandırma Raporunda MalKodu + " "+Depo Stok mİktarını Tutuyoruz. 
        /// </summary>
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }

        /// <summary>
        /// Stok Yaşlandırma Raporunda Depo Stok Miktarını Tututyoruz.
        /// </summary>
        public decimal Kod13 { get; set; }

        public override string ToString() => string.Format("{0} {1}", MalKodu, MalAdi);
    }
}