using System;

namespace Wms12m.Entity
{
    public class RaporGunlukSatis
    {
        public string Chk { get; set; }
        public string Chk2 { get; set; }
        public string Unvan { get; set; }
        public string BaglantiTipi { get; set; }
        public string BaglantiNo { get; set; }
        public string SatisTemsilcisi { get; set; }
        public string TeslimYeriUnvan { get; set; }
        public string KrediLimiti { get; set; }
        public string GrupKod { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public string Kalite { get; set; }
        public DateTime? IslemTarihi { get; set; }
        public string EvrakNo { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public string AnaBirim { get; set; }
        public double? AnaBirimMiktar { get; set; }
        public decimal AdetCikisMiktar { get; set; }
        public decimal M2CikisMiktar { get; set; }
        public decimal KGCikisMiktar { get; set; }
        public decimal? M3CikisMiktar { get; set; }
        public decimal MTCikisMiktar { get; set; }
        public decimal? NetBirimFiyat { get; set; }
        public decimal? NetTutar { get; set; }
        public decimal KDV { get; set; }
        public decimal? GenelToplam { get; set; }
    }
}
