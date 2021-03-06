﻿using System;

namespace Wms12m.Entity
{
    public class RaporBekleyenSiparis
    {
        public string Chk { get; set; }
        public string Unvan { get; set; }
        public DateTime? Tarih { get; set; }
        public string EvrakNo { get; set; }
        public string BaglantiNo { get; set; }
        public string GrupKod { get; set; }
        public string MalKodu { get; set; }
        public string MalAdi { get; set; }
        public decimal Miktar { get; set; }
        public string Birim { get; set; }
        public decimal AnaMiktar { get; set; }
        public string AnaBirim { get; set; }
        public decimal SevkedilenMiktar { get; set; }
        public decimal? KalanMiktar { get; set; }
        public string KalanBirim { get; set; }
        public double? KalanAnaMiktar { get; set; }
        public string KalanAnaBirim { get; set; }
        public string BaglantiTipi { get; set; }
        public string FiyatListeNo { get; set; }
        public decimal? FYTFiyat { get; set; }
        public string FYTDovizCinsi { get; set; }
        public string SozlesmeSartlari { get; set; }
        public decimal? NetFiyat { get; set; }
        public decimal? NetFiyat2 { get; set; }
        public decimal Tutar { get; set; }
        public decimal ToplamIskonto { get; set; }
        public decimal KDV { get; set; }
        public decimal? KalanBirimMiktar { get; set; }
        public decimal? KDVSizTutar { get; set; }
        public decimal? KDVliTutar { get; set; }
        public decimal? KDVSizKalanTutar { get; set; }
        public decimal? KDVliKalanTutar { get; set; }
        public double? StokMiktar { get; set; }
        public string StokBirim { get; set; }
        public decimal? StokAnaMiktar { get; set; }
        public string StokAnaBirim { get; set; }
        public DateTime? TeslimTarih { get; set; }
        public string TeslimTarihDurum { get; set; }
        public string SatisTemsilcisi { get; set; }
    }
}
