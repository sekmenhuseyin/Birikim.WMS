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
        public string EvrakNoArttir(string EvrakNo, string Seri)
        {
            if (EvrakNo == null || EvrakNo == "") EvrakNo = Seri + "000000";
            var sonemirNo = EvrakNo.RemoveFirstCharacter(2).ToInt32();
            EvrakNo = Seri + ("000000" + (sonemirNo + 1)).Right(6);
            return EvrakNo;
        }
        /// <summary>
        /// depo transfer fişi
        /// </summary>
        public Result DepoTransfer(List<frmUysWaitingTransfer> tbl, Entity.EMG Emir, bool GirisMi)
        {
            //settings
            DevHelper.Ayarlar.SetConStr(ConStr);
            DevHelper.Ayarlar.SirketKodu = SirketKodu;
            //add to list
            var DepTranList = new List<DepTran>();
            foreach (var item in tbl)
            {
                DepTranList.Add(new DepTran()
                {
                    EvrakNo = tbl[0].EvrakNo,
                    Tarih = DateTime.Today,
                    MalKodu = item.MalKodu,
                    Miktar = item.Miktar,
                    Birim = item.Birim,
                    SeriNo = item.SeriNo,
                    CikisDepo = GirisMi == true ? item.AraDepo : item.CikisDepo,
                    GirisDepo = GirisMi == true ? item.GirisDepo : item.AraDepo,
                    Kaydeden = item.Kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 74
                });
            }
            //emir details
            Emir.Kaydeden = tbl[0].Kaydeden;
            Emir.KayitKaynak = 10;
            Emir.KayitSurum = "1.00";
            Emir.Degistiren = tbl[0].Kaydeden;
            Emir.DegisTarih = Emir.KayitTarih;
            Emir.DegisSaat = Emir.KayitSaat;
            Emir.DegisKaynak = 10;
            Emir.DegisSurum = "1.00";
            Emir.CheckSum = 1542;
            //save 2 db
            Stok_Islemleri StokIslem = new Stok_Islemleri(SirketKodu);
            IslemSonuc Sonuc = StokIslem.DepoTransfer_EMG_Kayit(DepTranList, Emir, tbl[0].Kaydeden2);
            //return
            var _Result = new Result()
            {
                Status = Sonuc.Basarili,
                Message = Sonuc.Hata != null ? Sonuc.Hata.Message : ""
            };
            return _Result;
        }
    }
}