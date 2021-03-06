﻿using Birikim.Helpers;
using Birikim.Models;
using Birikim.Models.UysPln;
using OnikimCore;
using OnikimCore.Model;
using System;
using System.Collections.Generic;
using Wms12m.Entity;

namespace Wms12m
{
    public class UYSF
    {
        private SqlExper SqlExper { get; set; }
        private string SirketKodu { get; set; }

        /// <summary>
        /// yeni finsat
        /// </summary>
        public UYSF(SqlExper sqlExper, string sirketKodu)
        {
            SqlExper = sqlExper;
            SirketKodu = sirketKodu;
        }

        /// <summary>
        /// depo transfer fişi
        /// </summary>
        public Result DepoTransfer(List<frmUysWaitingTransfer> tbl, EMG2 emir, bool GirisMi)
        {
            // add to list
            var DepTranList = new List<DepTran>();
            foreach (var item in tbl)
            {
                DepTranList.Add(new DepTran()
                {
                    EvrakNo = tbl[0].EvrakNo,
                    Tarih = tbl[0].Tarih.FromOaDate(),
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

            var Emir = new EMG();
            if (emir != null)
            {
                Emir.DefaultValueSet();
                Emir.EmirNo = emir.EmirNo;
                Emir.IcDis = 11;
                Emir.BasTarih = emir.BasTarih;
                Emir.BasSaat = emir.BasSaat;
                Emir.Talimat2 = emir.Talimat2;
                Emir.Kod2 = emir.Kod2;
                Emir.Kod3 = emir.Kod3;
                Emir.StiNo = emir.StiNo;
                Emir.KayitTarih = emir.KayitTarih;
                Emir.KayitSaat = emir.KayitSaat;
                Emir.BitTarih = emir.BitTarih;
                Emir.BitSaat = emir.BitSaat;
                Emir.Talimat3 = emir.Talimat3;
                Emir.TrsfrNo = emir.TrsfrNo;
                Emir.RecID = emir.RecID;
                Emir.Birim = emir.Birim;
                Emir.CurDurum = emir.CurDurum;
                Emir.CurDurSb = emir.CurDurSb;
                Emir.SonDurSb = emir.SonDurSb;
                Emir.PlOnay = emir.PlOnay;
                Emir.YMUret = emir.YMUret;
                Emir.YMMly = emir.YMMly;
                Emir.YMEndMly = emir.YMEndMly;
                Emir.YMDepo = emir.YMDepo;
                Emir.YMHmdCik = emir.YMHmdCik;
                Emir.Teklif = emir.Teklif;
                Emir.KayitTuru = emir.KayitTuru;
                // emir details
                Emir.Kaydeden = tbl[0].Kaydeden;
                Emir.KayitKaynak = 10;
                Emir.KayitSurum = "1.00";
                Emir.Degistiren = tbl[0].Kaydeden;
                Emir.DegisTarih = DateTime.Today.ToOADateInt();
                Emir.DegisSaat = DateTime.Now.ToOaTime();
                Emir.DegisKaynak = 10;
                Emir.DegisSurum = "1.00";
                Emir.CheckSum = 1542;
            }

            // save 2 db
            var StokIslem = new Stok_Islemleri(SirketKodu, SqlExper);
            return StokIslem.DepoTransfer_EMG_Kayit(DepTranList, emir == null ? null : Emir, tbl[0].Kaydeden2);
        }

        /// <summary>
        /// evrak no oluştur
        /// </summary>
        public string EvrakNoArttir(string evrakNo, string seri)
        {
            if (evrakNo == null || evrakNo == "") evrakNo = seri + "000000";
            var sonemirNo = evrakNo.RemoveFromStart(2).ToInt32();
            evrakNo = seri + ("000000" + (sonemirNo + 1)).Right(6);
            return evrakNo;
        }
    }
}