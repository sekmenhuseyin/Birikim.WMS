using OnikimCore.GunesCore;
using System;
using System.Collections.Generic;
using Wms12m.Entity;

namespace Wms12m
{
    public class UYSF
    {
        private string ConStr { get; set; }
        private string SirketKodu { get; set; }
        /// <summary>
        /// yeni finsat
        /// </summary>
        public UYSF(string conStr, string sirketKodu)
        {
            ConStr = conStr;
            SirketKodu = sirketKodu;
        }
        /// <summary>
        /// evrak no oluştur
        /// </summary>
        public string EvrakNo(int EvrakSeriNo)
        {
            DevHelper.Ayarlar.SetConStr(ConStr);
            DevHelper.Ayarlar.SirketKodu = SirketKodu;
            return (new Genel_Islemler(SirketKodu)).EvrakNo_Getir(EvrakSeriNo);
        }
        /// <summary>
        /// depo transfer fişi
        /// </summary>
        public Result DepoTransfer(frmUysTransfer tbl, string emirno, bool GirisMi, int Evrakserino, string kaydeden, string tamAdi)
        {
            //settings
            DevHelper.Ayarlar.SetConStr(ConStr);
            DevHelper.Ayarlar.SirketKodu = SirketKodu;
            string evrakNo = (new Genel_Islemler(SirketKodu)).EvrakNo_Getir(7199 + Evrakserino);
            //add to list
            var DepTranList = new List<DepTran>();
            for (int i = 0; i < tbl.MalKodu.Length; i++)
            {
                DepTran dep = new DepTran()
                {
                    EvrakNo = evrakNo,
                    Tarih = DateTime.Today,
                    MalKodu = tbl.MalKodu[i],
                    Miktar = tbl.Miktar[i],
                    Birim = tbl.Birim[i],
                    SeriNo = tbl.SeriNo[i],
                    CikisDepo = GirisMi == true ? tbl.AraDepo : tbl.CikisDepo,
                    GirisDepo = GirisMi == true ? tbl.GirisDepo : tbl.AraDepo,
                    Kaydeden = kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 74
                };
                DepTranList.Add(dep);
            }
            //save 2 db
            Stok_Islemleri StokIslem = new Stok_Islemleri(SirketKodu);
            IslemSonuc Sonuc = StokIslem.DepoTransfer_Kayit(7199 + Evrakserino, DepTranList);
            if (Sonuc.Basarili == true)
            {
                Sonuc = StokIslem.DepoTransfer_EMG_Kayit(DepTranList[0].CikisDepo, DepTranList[0].GirisDepo, evrakNo, emirno, kaydeden, tamAdi);
            }
            //return
            var _Result = new Result()
            {
                Status = Sonuc.Basarili,
                Message = Sonuc.Hata != null ? Sonuc.Hata.Message : "",
                Data = evrakNo
            };
            return _Result;
        }
    }
}