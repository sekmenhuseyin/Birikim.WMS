using OnikimCore.GunesCore;
using System;
using System.Collections.Generic;
using Wms12m.Entity;
using Wms12m.Entity.Models;

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
            Genel_Islemler GI = new Genel_Islemler(SirketKodu);
            return GI.EvrakNo_Getir(EvrakSeriNo);
        }
        /// <summary>
        /// depo transfer fişi
        /// </summary>
        public Result DepoTransfer(Transfer tblTransfer, bool GirisMi, string kaydeden, int Evrakserino)
        {
            //settings
            DevHelper.Ayarlar.SetConStr(ConStr);
            DevHelper.Ayarlar.SirketKodu = SirketKodu;
            Genel_Islemler GI = new Genel_Islemler(SirketKodu);
            string evrakNo = GI.EvrakNo_Getir(7199 + Evrakserino);
            //add to list
            List<DepTran> DepTranList = new List<DepTran>();
            foreach (var item in tblTransfer.Transfer_Detay)
            {
                DepTran dep = new DepTran()
                {
                    EvrakNo = evrakNo,
                    Tarih = DateTime.Today,
                    MalKodu = item.MalKodu,
                    Miktar = item.Miktar,
                    Birim = item.Birim,
                    GirisDepo = GirisMi == true ? tblTransfer.Depo.DepoKodu : tblTransfer.Depo2.DepoKodu,
                    CikisDepo = GirisMi == true ? tblTransfer.Depo2.DepoKodu : tblTransfer.Depo1.DepoKodu,
                    Kaydeden = kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 74
                };
                DepTranList.Add(dep);
            }
            //save 2 db
            Stok_Islemleri StokIslem = new Stok_Islemleri(tblTransfer.SirketKod);
            OnikimCore.GunesCore.IslemSonuc Sonuc = StokIslem.DepoTransfer_Kayit(7199 + Evrakserino, DepTranList);
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