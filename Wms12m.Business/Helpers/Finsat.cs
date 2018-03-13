using Birikim.Helpers;
using Birikim.Models;
using OnikimCore;
using OnikimCore.Enums;
using OnikimCore.Model;
using OnikimCore.Operations;
using OnikimCore.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using TumFaturaKayit;
using TumFaturaKayit.Enum;
using TumFaturaKayit.Models;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Finsat
    {
        private WMSEntities Db { get; set; }
        private FaturaKayit FtrKayit { get; set; }
        private string SirketKodu { get; set; }
        private SqlExper SqlExper { get; set; }

        /// <summary>
        /// yeni finsat
        /// </summary>
        public Finsat(WMSEntities db, string sirketKodu, SqlExper sqlExper, FaturaKayit faturaKayit)
        {
            Db = db;
            SirketKodu = sirketKodu;
            SqlExper = sqlExper;
            FtrKayit = faturaKayit;
        }

        /// <summary>
        /// Alımdan Iade faturası
        /// </summary>
        public Result AlımdanIadeFaturaKayıt(int irsId, string depoKodu, bool efatKullanici, int tarih, string chk, string kaydeden, int irsaliyeSeri, int yil)
        {
            var STIBaseList = new List<ParamSti>();
            var tempEvrakNo = "";
            // evrak no getir
            List<EvrakBilgi> evrkno;
            try
            {
                evrkno = FtrKayit.EvrakNo_Getir(efatKullanici, irsaliyeSeri, yil, FaturaTipi.AlimdanIadeFaturası.ToInt32());
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }

            var saat = DateTime.Now.ToOaTime();
            // listeyi dön
            var sql = string.Format("SELECT MalKodu, Miktar, Birim, KynkSiparisNo as EvrakNo, KynkSiparisID AS Row_ID FROM wms.IRS_Detay WITH (NOLOCK) WHERE IrsaliyeID={0}", irsId);
            var list = Db.Database.SqlQuery<STIMax>(sql).ToList();
            foreach (STIMax item in list)
            {
                tempEvrakNo = item.EvrakNo;
                sql = string.Format("SELECT STI.ROW_ID,STI.IslemTur,STI.EvrakTarih,STI.MhsKod as STIMhsKod,STI.Nesne1,STI.Nesne2,STI.Nesne3, STI.Chk,STI.IslemTip,STI.Miktar,STI.Miktar - STI.ErekIIFMiktar AS ErekIIFMiktar,STI.MalKodu,STI.Fiyat,STI.Birim," +
                    "STI.Depo,STI.ToplamIskonto,STI.KDV,STI.KDVOran,STI.IskontoOran1,STI.IskontoOran2,STI.IskontoOran3,STI.IskontoOran4,STI.IskontoOran5,STI.EvrakNo as KaynakIIFEvrakNo,STI.KaynakSiparisNo," +
                    "STI.Tarih as KaynakSiparisTarih,STI.SiraNo,STI.KaynakSiraNo,STI.SiparisSiraNo," +
                    "STI.Miktar as SiparisMiktar,STI.FytListeNo,STI.ValorGun,STI.Kod1,STI.Kod2,STI.Kod3,STI.Kod4,STI.Kod5,STI.Kod6,STI.Kod7,STI.Kod8,STI.Kod9,STI.Kod10," +
                    "STI.Kod11,STI.Kod12,STI.Kod13,STI.Kod14,STI.KayitKaynak,STI.KayitSurum,STI.DegisKaynak,STI.DegisSurum,STI.SevkTarih as SevkTarih,STI.KaynakIrsTarih as KaynakIrsTarih," +
                    "STI.KaynakSiparisTarih as KaynakSiparisTarih2,STI.Kredi_Donem_VadeTarih,STI.KaynakIrsEvrakNo,STK.AlimdanIade as MhsKod,CHK.EFatKullanici, STK.SatislarHesabi," +
                    "CHK.EArsivTeslimSekli,CHK.MhsKod As CHKMhsKod,CHK.EFatSenaryo " +
                    ", MFK.Tutar AS MFKTutar, MFK.Aciklama AS MFKAciklama , MFK.Aciklama2 AS MFKAciklama2 , MFK.Aciklama3 AS MFKAciklama3 , MFK.Aciklama4 AS MFKAciklama4 , MFK.Aciklama5  AS MFKAciklama5, MFK.Aciklama6 AS MFKAciklama6 " +
                    "FROM FINSAT6{0}.FINSAT6{0}.STI WITH (NOLOCK) LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH (NOLOCK) ON STI.Chk=CHK.HesapKodu LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK WITH (NOLOCK) ON STK.MalKodu=STI.MalKodu " +
                    "LEFT JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH (NOLOCK) ON MFK.EvrakNo=STI.EvrakNo  AND  MFK.KynkEvrakTip=STI.KynkEvrakTip AND  MFK.HesapKod=STI.Chk " +
                    "WHERE (STI.EvrakNo = '{1}') AND (STI.Chk = '{2}') AND (STI.Depo = '{3}') AND (STI.Row_ID = '{4}')  AND (STI.KynkEvrakTip = 4)", SirketKodu, item.EvrakNo, chk, depoKodu, item.Row_ID);
                var finsat = Db.Database.SqlQuery<ParamSti>(sql).FirstOrDefault();
                if (finsat != null)
                {
                    finsat.Miktar = item.Miktar;
                    finsat.EvrakNo = evrkno[0].EvrakNo;
                    finsat.Tarih = finsat.KaynakSiparisTarih;
                    finsat.Kaydeden = kaydeden;
                    finsat.KayitSurum = "9.01.028";
                    finsat.KayitKaynak = 74;
                    STIBaseList.Add(finsat);
                }
            }

            // finsat işlemleri
            try
            {
                if (STIBaseList.Count > 0)
                {
                    return FtrKayit.FaturaKaydet(STIBaseList, efatKullanici, irsaliyeSeri, yil, FaturaTipi.AlimdanIadeFaturası.ToInt32(), "391", SirketKodu == "33" ? "391 000" : "391 01 001", "391 001");
                }
                else
                    return new Result(false, "Bu sipariş kapanmış. Evrak No= " + tempEvrakNo);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        /// <summary>
        /// depo transfer fişi
        /// </summary>
        public Result DepoTransfer(Transfer tblTransfer, bool girisMi, string kaydeden, int evrakserino)
        {
            // settings
            var GI = new Genel_Islemler(SqlExper);
            var evrakNo = GI.EvrakNo_Getir(2399 + evrakserino);
            // add to list
            List<DepTran> depTranList = new List<DepTran>();
            foreach (var item in tblTransfer.Transfer_Detay)
            {
                var dep = new DepTran()
                {
                    EvrakNo = evrakNo,
                    Tarih = DateTime.Today,
                    MalKodu = item.MalKodu,
                    Miktar = item.Miktar,
                    Birim = item.Birim,
                    GirisDepo = girisMi == true ? tblTransfer.Depo.DepoKodu : tblTransfer.Depo2.DepoKodu,
                    CikisDepo = girisMi == true ? tblTransfer.Depo2.DepoKodu : tblTransfer.Depo1.DepoKodu,
                    Kaydeden = kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 74,
                    SeriNo = ""
                };
                depTranList.Add(dep);
            }

            // save 2 db
            var stokIslem = new Stok_Islemleri(tblTransfer.SirketKod, SqlExper);
            var sonuc = stokIslem.DepoTransfer_Kayit(2399 + evrakserino, depTranList);
            sonuc.Data = evrakNo;
            return sonuc;
        }

        /// <summary>
        /// evrak no oluştur
        /// </summary>
        public string EvrakNo(int evrakSeriNo)
        {
            var gI = new Genel_Islemler(SqlExper);
            return gI.EvrakNo_Getir(evrakSeriNo);
        }

        /// <summary>
        /// satış irsaliyesi ve faturası
        /// </summary>
        public Result FaturaKayıt(int irsId, string depoKodu, bool efatKullanici, int tarih, string chk, string kaydeden, int irsaliyeSeri, int faturaSeri, int yil)
        {
            var stiBaseList = new List<ParamSti>();
            var tempEvrakNo = "";
            List<STIBase> stiBaseListSpi = new List<STIBase>();
            List<STIMax> stList = new List<STIMax>();

            // evrak no getir
            List<EvrakBilgi> evrkno = new List<EvrakBilgi>();
            var saat = DateTime.Now.ToOaTime();
            // listeyi dön
            var sql = string.Format(@"
                                        SELECT MalKodu, Miktar, Birim, KynkSiparisNo as EvrakNo, KynkSiparisTarih, KynkSiparisSiraNo,
                                        (SELECT IslemTip FROM FINSAT6{0}.FINSAT6{0}.SPI WITH (NOLOCK) WHERE KynkEvrakTip=62 AND SPI.EvrakNo= wms.IRS_Detay.KynkSiparisNo AND SiraNo=wms.IRS_Detay.KynkSiparisSiraNo
                                        AND Tarih=wms.IRS_Detay.KynkSiparisTarih) AS SipIslemTip
                                        FROM wms.IRS_Detay WITH (NOLOCK) WHERE IrsaliyeID={1}", SirketKodu, irsId);
            var list = Db.Database.SqlQuery<STIMax>(sql).ToList();

            if (list.Where(x => x.SipIslemTip == 1).ToList().Count > 0)
            {
                try
                {
                    evrkno = FtrKayit.EvrakNo_Getir(efatKullanici, irsaliyeSeri, yil, FaturaTipi.SatisFaturası.ToInt32());
                }
                catch (Exception ex)
                {
                    return new Result(false, ex.Message);
                }
            }

            foreach (STIMax item in list.Where(x => x.SipIslemTip == 1).ToList()) //İç Piyasa İse Satış Faturası Kaydedilir
            {
                tempEvrakNo = item.EvrakNo;
                sql = string.Format("SELECT SPI.Chk, SPI.IslemTip, SPI.Miktar, SPI.MalKodu, SPI.Fiyat, SPI.Birim, SPI.Depo, SPI.ToplamIskonto, SPI.KDV, SPI.KDVOran, SPI.IskontoOran1, SPI.IskontoOran2, SPI.IskontoOran3, SPI.IskontoOran4, SPI.IskontoOran5, " +
                                    "SPI.EvrakNo as KaynakSiparisNo,SPI.VadeTarih, SPI.Tarih as KaynakSiparisTarih, SPI.SiraNo as SiparisSiraNo, SPI.Miktar as SiparisMiktar, SPI.TeslimMiktar, SPI.KapatilanMiktar, SPI.FytListeNo, SPI.ValorGun, SPI.Kod1, SPI.Kod2, SPI.Kod3, SPI.Kod4, SPI.Kod5, SPI.Kod6, SPI.Kod7, SPI.Kod8, SPI.Kod9, SPI.Kod10, SPI.Kod11, SPI.Kod12, SPI.Kod13, SPI.Kod14, SPI.KayitKaynak, SPI.KayitSurum, SPI.DegisKaynak, SPI.DegisSurum," +
                                    "CHK.EFatKullanici, STK.SatislarHesabi, CHK.EArsivTeslimSekli, CHK.MhsKod, CHK.EFatSenaryo " +
                                    ", MFK.Tutar AS MFKTutar, MFK.Aciklama AS MFKAciklama , MFK.Aciklama2 AS MFKAciklama2 , MFK.Aciklama3 AS MFKAciklama3 , MFK.Aciklama4 AS MFKAciklama4 , MFK.Aciklama5  AS MFKAciklama5, MFK.Aciklama6 AS MFKAciklama6 " +
                                    "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH (NOLOCK) LEFT JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH (NOLOCK) ON SPI.Chk=CHK.HesapKodu LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK WITH (NOLOCK) ON STK.MalKodu=SPI.MalKodu " +
                                    "LEFT JOIN FINSAT6{0}.FINSAT6{0}.MFK WITH (NOLOCK) ON MFK.EvrakNo=SPI.EvrakNo  AND  MFK.KynkEvrakTip=SPI.KynkEvrakTip AND  MFK.HesapKod=SPI.Chk " +
                                    "WHERE (SPI.EvrakNo = '{1}') AND (SPI.Chk = '{2}') AND (SPI.Depo = '{3}') AND (SPI.Tarih = {4}) AND (SPI.SiraNo = {5}) AND (SPI.KynkEvrakTip = 62) AND (SPI.SiparisDurumu = 0) AND (SPI.Kod10 IN ('Terminal', 'Onaylandı'))", SirketKodu, item.EvrakNo, chk, depoKodu, item.KynkSiparisTarih, item.KynkSiparisSiraNo);
                var finsat = Db.Database.SqlQuery<ParamSti>(sql).FirstOrDefault();
                if (finsat != null)
                {
                    finsat.Miktar = item.Miktar;
                    finsat.EvrakNo = evrkno[0].EvrakNo;
                    finsat.KaynakIrsEvrakNo = evrkno[1].EvrakNo;
                    finsat.Tarih = finsat.KaynakSiparisTarih;
                    finsat.Kaydeden = kaydeden;
                    finsat.KayitSurum = "9.01.028";
                    finsat.KayitKaynak = 70;
                    stiBaseList.Add(finsat);
                }
            }

            if (list.Where(x => x.SipIslemTip == 2).ToList().Count > 0)
            {
                try
                {
                    evrkno = FtrKayit.EvrakNo_Getir(efatKullanici, irsaliyeSeri, yil, FaturaTipi.SatisIrsaliyesi.ToInt32());
                }
                catch (Exception ex)
                {
                    return new Result(false, ex.Message);
                }
            }

            foreach (STIMax item in list.Where(x => x.SipIslemTip == 2).ToList()) //Dış Piyasa İse Satış İrsaliyesi Kaydedilir
            {
                tempEvrakNo = item.EvrakNo;
                var sqlSPI = string.Format("SELECT IRS.EvrakNo, IRS_Detay.IrsaliyeID, IRS_Detay.MalKodu, SUM(wms.IRS_Detay.Miktar) AS Miktar, IRS_Detay.Birim, SUM(wms.IRS_Detay.Miktar) AS OkutulanMiktar, Depo.DepoKodu, IRS.HesapKodu, IRS.Tarih, " +
                                        "(SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = IRS_Detay.MalKodu)) AS MalAdi," +
                                        "ISNULL(IRS_Detay.KynkSiparisNo, '') AS SiparisNo, ISNULL(IRS_Detay.KynkSiparisSiraNo, 0) AS KynkSiparisSiraNo, ISNULL(IRS_Detay.KynkSiparisTarih, 0) AS KynkSiparisTarih, ISNULL(IRS_Detay.KynkSiparisMiktar, 0) AS KynkSiparisMiktar, " +
                                        "FINSAT6{0}.FINSAT6{0}.SPI.BirimFiyat AS Fiyat, FINSAT6{0}.FINSAT6{0}.SPI.KDVOran, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran1, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran2, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran3, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran4, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran5, FINSAT6{0}.FINSAT6{0}.SPI.IslemTip AS SipIslemTip, FINSAT6{0}.FINSAT6{0}.SPI.DovizCinsi AS DovizCinsi,FINSAT6{0}.FINSAT6{0}.CHK.EFatSenaryo,FINSAT6{0}.FINSAT6{0}.CHK.EArsivTeslimSekli,FINSAT6{0}.FINSAT6{0}.CHK.EFatEtiket AS EFatEtiketPK,ISNULL((SELECT EfatEtiketGB FROM SOLAR6.DBO.SIR(NOLOCK) INNER JOIN SOLAR6.DBO.SDK(NOLOCK) ON SIR.Kod = SDK.SirketKod WHERE SDK.Kod = '{0}' AND SDK.Tip = 0), '') AS EfatEtiketGB, FINSAT6{0}.FINSAT6{0}.SPI.ValorGun " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH (NOLOCK)" +
                                        "INNER JOIN FINSAT6{0}.FINSAT6{0}.CHK WITH (NOLOCK) ON SPI.Chk = CHK.HesapKodu RIGHT OUTER JOIN wms.Depo WITH(NOLOCK) INNER JOIN wms.IRS WITH(NOLOCK) ON wms.Depo.ID = wms.IRS.DepoID INNER JOIN wms.IRS_Detay WITH(NOLOCK) ON wms.IRS.ID = wms.IRS_Detay.IrsaliyeID ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = wms.IRS.HesapKodu AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = wms.IRS_Detay.KynkSiparisTarih AND FINSAT6{0}.FINSAT6{0}.SPI.SiraNo = wms.IRS_Detay.KynkSiparisSiraNo AND FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = wms.IRS_Detay.KynkSiparisNo " +
                                        "WHERE (IRS_Detay.IrsaliyeID = {1}) " +
                                        "GROUP BY wms.IRS.EvrakNo, wms.IRS_Detay.IrsaliyeID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Birim, wms.Depo.DepoKodu, wms.IRS.HesapKodu, wms.IRS.Tarih, ISNULL(wms.IRS_Detay.KynkSiparisNo, ''), ISNULL(wms.IRS_Detay.KynkSiparisSiraNo, 0), ISNULL(wms.IRS_Detay.KynkSiparisTarih, 0), ISNULL(wms.IRS_Detay.KynkSiparisMiktar, 0), FINSAT6{0}.FINSAT6{0}.SPI.BirimFiyat, FINSAT6{0}.FINSAT6{0}.SPI.KDVOran, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran1, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran2, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran3, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran4, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran5, FINSAT6{0}.FINSAT6{0}.SPI.IslemTip, FINSAT6{0}.FINSAT6{0}.SPI.DovizCinsi,FINSAT6{0}.FINSAT6{0}.CHK.EFatSenaryo,FINSAT6{0}.FINSAT6{0}.CHK.EArsivTeslimSekli,FINSAT6{0}.FINSAT6{0}.CHK.EFatEtiket, FINSAT6{0}.FINSAT6{0}.SPI.ValorGun", SirketKodu, irsId);
                stList = Db.Database.SqlQuery<STIMax>(sqlSPI).ToList();
            }

            foreach (STIMax stItem in stList)
            {
                var sti = new STIBase()
                {
                    EvrakNo = evrkno[0].EvrakNo,
                    HesapKodu = stItem.HesapKodu,
                    Tarih = stItem.KynkSiparisTarih == 0 ? DateTime.Today.ToOADate().ToInt32() : stItem.KynkSiparisTarih,
                    MalKodu = stItem.MalKodu,
                    Miktar = stItem.OkutulanMiktar,
                    Birim = stItem.Birim,
                    Depo = stItem.DepoKodu,
                    EvrakTipi = STIEvrakTipi.SatisIrsaliyesi,
                    Kaydeden = kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 70,
                    IslemTip = stItem.SipIslemTip,
                    DovizCinsi = stItem.DovizCinsi,
                    EFatSenaryo = stItem.EFatSenaryo,
                    EArsivTeslimSekli = stItem.EArsivTeslimSekli,
                    EFatEtiketGB = stItem.EFatEtiketGB,
                    EFatEtiketPK = stItem.EFatEtiketPK,
                    ValorGun = stItem.ValorGun
                };
                if (stItem.SiparisNo != "" && stItem.KynkSiparisMiktar > 0)
                {
                    sti.KayitTipi = STIKayitTipi.Siparisten_Irsaliye;
                    sti.KaynakSiparisNo = stItem.SiparisNo;
                    sti.KaynakSiparisTarih = stItem.KynkSiparisTarih;
                    sti.SiparisSiraNo = stItem.KynkSiparisSiraNo;
                    sti.SiparisMiktar = stItem.KynkSiparisMiktar;
                    sti.Fiyat = stItem.Fiyat;
                }
                else
                {
                    sti.KayitTipi = STIKayitTipi.Irsaliye;
                    sti.KaynakSiparisNo = "";
                    sti.KaynakSiparisTarih = 0;
                    sti.SiparisSiraNo = 0;
                    sti.SiparisMiktar = 0;
                }

                stiBaseListSpi.Add(sti);
            }

            // finsat işlemleri
            try
            {
                var result = new Result();

                if (stiBaseList.Count > 0)
                {
                    result = FtrKayit.FaturaKaydet(stiBaseList, efatKullanici, faturaSeri, yil, FaturaTipi.SatisFaturası.ToInt32(), "391", SirketKodu == "33" ? "391 000" : "391 01 001", "391 001");
                }

                if (stiBaseListSpi.Count > 0)
                {
                    var irsIslem = new Irsaliye_Islemleri(SirketKodu, SqlExper);
                    try
                    {
                        result = irsIslem.Irsaliye_Kayit(irsaliyeSeri, efatKullanici, stiBaseListSpi);
                    }
                    catch (Exception ex)
                    {
                        return new Result(false, ex.Message);
                    }
                }

                if (stiBaseList.Count <= 0 && stiBaseListSpi.Count <= 0)
                    return new Result(false, "Bu sipariş kapanmış. Evrak No= " + tempEvrakNo);
                else
                    return result;
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        /// <summary>
        /// malkabul işlemleri
        /// </summary>
        public Result MalKabul(IR irsaliye, int kulId)
        {
            List<STIBase> STIBaseList = new List<STIBase>();
            var kaydeden = Db.Users.Where(m => m.ID == kulId).Select(m => m.Kod).FirstOrDefault();
            var sql = string.Format("SELECT IRS.EvrakNo, IRS_Detay.IrsaliyeID, IRS_Detay.MalKodu, SUM(wms.IRS_Detay.Miktar) AS Miktar, IRS_Detay.Birim, ISNULL(SUM(wms.IRS_Detay.OkutulanMiktar), 0) AS OkutulanMiktar, Depo.DepoKodu, IRS.HesapKodu, IRS.Tarih, " +
                                        "(SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = IRS_Detay.MalKodu)) AS MalAdi," +
                                        "ISNULL(IRS_Detay.KynkSiparisNo, '') AS SiparisNo, ISNULL(IRS_Detay.KynkSiparisSiraNo, 0) AS KynkSiparisSiraNo, ISNULL(IRS_Detay.KynkSiparisTarih, 0) AS KynkSiparisTarih, ISNULL(IRS_Detay.KynkSiparisMiktar, 0) AS KynkSiparisMiktar, " +
                                        "ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.BirimFiyat,0) AS Fiyat, ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.KDVOran,0) AS KDVOran, ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran1,0) AS IskontoOran1, ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran2,0) As IskontoOran2, ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran3,0) As IskontoOran3 , ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran4,0) As IskontoOran4, ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran5,0) As IskontoOran5, ISNULL(FINSAT6{0}.FINSAT6{0}.SPI.ValorGun,0) As ValorGun  " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH (NOLOCK) RIGHT OUTER JOIN wms.Depo WITH(NOLOCK) INNER JOIN wms.IRS WITH(NOLOCK) ON wms.Depo.ID = wms.IRS.DepoID INNER JOIN wms.IRS_Detay WITH(NOLOCK) ON wms.IRS.ID = wms.IRS_Detay.IrsaliyeID ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = wms.IRS.HesapKodu AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = wms.IRS_Detay.KynkSiparisTarih AND FINSAT6{0}.FINSAT6{0}.SPI.SiraNo = wms.IRS_Detay.KynkSiparisSiraNo AND FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = wms.IRS_Detay.KynkSiparisNo " +
                                        "WHERE (IRS_Detay.IrsaliyeID = {1}) AND (IRS_Detay.OkutulanMiktar IS NOT NULL) AND (IRS_Detay.OkutulanMiktar > 0)" +
                                        "GROUP BY wms.IRS.EvrakNo, wms.IRS_Detay.IrsaliyeID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Birim, wms.Depo.DepoKodu, wms.IRS.HesapKodu, wms.IRS.Tarih, ISNULL(wms.IRS_Detay.KynkSiparisNo, ''), ISNULL(wms.IRS_Detay.KynkSiparisSiraNo, 0), ISNULL(wms.IRS_Detay.KynkSiparisTarih, 0), ISNULL(wms.IRS_Detay.KynkSiparisMiktar, 0), FINSAT6{0}.FINSAT6{0}.SPI.BirimFiyat, FINSAT6{0}.FINSAT6{0}.SPI.KDVOran, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran1, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran2, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran3, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran4, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran5, FINSAT6{0}.FINSAT6{0}.SPI.ValorGun ORDER BY  ISNULL(wms.IRS_Detay.KynkSiparisSiraNo, 0)", irsaliye.SirketKod, irsaliye.ID);
            var STList = Db.Database.SqlQuery<STIMax>(sql).ToList();
            foreach (STIMax stItem in STList)
            {
                var sti = new STIBase()
                {
                    EvrakNo = stItem.EvrakNo,
                    HesapKodu = stItem.HesapKodu,
                    Tarih = stItem.Tarih,
                    MalKodu = stItem.MalKodu,
                    Miktar = stItem.OkutulanMiktar,
                    Birim = stItem.Birim,
                    Depo = stItem.DepoKodu,
                    EvrakTipi = STIEvrakTipi.AlimIrsaliyesi,
                    Kaydeden = kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 74,
                    ValorGun = stItem.ValorGun
                };
                if (stItem.SiparisNo != "" && stItem.KynkSiparisMiktar > 0)
                {
                    sti.KayitTipi = STIKayitTipi.Siparisten_Irsaliye;
                    sti.KaynakSiparisNo = stItem.SiparisNo;
                    sti.KaynakSiparisTarih = stItem.KynkSiparisTarih;
                    sti.SiparisSiraNo = stItem.KynkSiparisSiraNo;
                    sti.SiparisMiktar = stItem.KynkSiparisMiktar;
                    sti.Fiyat = stItem.Fiyat;
                    sti.KdvOran = stItem.KdvOran;
                    sti.IskontoOran1 = stItem.IskontoOran1;
                    sti.IskontoOran2 = stItem.IskontoOran2;
                    sti.IskontoOran3 = stItem.IskontoOran3;
                    sti.IskontoOran4 = stItem.IskontoOran4;
                    sti.IskontoOran5 = stItem.IskontoOran5;
                }
                else
                {
                    sti.KayitTipi = STIKayitTipi.Irsaliye;
                    sti.KaynakSiparisNo = "";
                    sti.KaynakSiparisTarih = 0;
                    sti.SiparisSiraNo = 0;
                    sti.SiparisMiktar = 0;
                }

                STIBaseList.Add(sti);
            }

            var IrsIslem = new Irsaliye_Islemleri(irsaliye.SirketKod, SqlExper);
            try
            {
                return IrsIslem.Irsaliye_Kayit(-1, true, STIBaseList);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        /// <summary>
        /// satıştan iade
        /// </summary>
        public Result SatisIade(IR irsaliye, int kulID, int IrsaliyeSeri, int yil, bool efatKullanici, string depo)
        {
            List<STIBase> STIBaseList = new List<STIBase>();

            var kaydeden = Db.Users.Where(m => m.ID == kulID).Select(m => m.Kod).FirstOrDefault();
            var sql = string.Format("FINSAT6{0}.wms.SatisIadeKayitList {1}", irsaliye.SirketKod, irsaliye.ID);
            var STList = Db.Database.SqlQuery<STIMax>(sql).ToList();
            foreach (STIMax stItem in STList)
            {
                var sti = new STIBase()
                {
                    EvrakNo = irsaliye.EvrakNo,
                    HesapKodu = stItem.HesapKodu,
                    Tarih = stItem.Tarih,
                    MalKodu = stItem.MalKodu,
                    Miktar = stItem.OkutulanMiktar,
                    Birim = stItem.Birim,
                    Depo = depo,
                    EvrakTipi = STIEvrakTipi.SatistanIadeIrsaliyesi,
                    Kaydeden = kaydeden,
                    KayitSurum = "9.01.028",
                    KayitKaynak = 74,
                    ErekIIFMiktar = stItem.ErekIIFMiktar,
                    Row_ID = stItem.Row_ID,
                    SiraNo = stItem.SiraNo,
                    KaynakSiraNo = stItem.KaynakSiraNo,
                    EFatSenaryo = stItem.EFatSenaryo,
                    EArsivTeslimSekli = stItem.EArsivTeslimSekli,
                    EFatEtiketGB = stItem.EFatEtiketGB,
                    EFatEtiketPK = stItem.EFatEtiketPK,
                    IslemTip = stItem.SipIslemTip
                };
                if (stItem.SiparisNo != "")
                {
                    sti.KayitTipi = STIKayitTipi.Irsaliye;
                    sti.KaynakSiparisTarih = stItem.KaynakSiparisTarih;
                    sti.SiparisSiraNo = stItem.SiparisSiraNo;
                    sti.SiparisMiktar = stItem.KynkSiparisMiktar;
                    sti.Fiyat = stItem.Fiyat;
                    sti.KdvOran = stItem.KdvOran;
                    sti.IskontoOran1 = stItem.IskontoOran1;
                    sti.IskontoOran2 = stItem.IskontoOran2;
                    sti.IskontoOran3 = stItem.IskontoOran3;
                    sti.IskontoOran4 = stItem.IskontoOran4;
                    sti.IskontoOran5 = stItem.IskontoOran5;
                    sti.Kod1 = stItem.Kod1;
                    sti.Kod2 = stItem.Kod2;
                    sti.Kod3 = stItem.Kod3;
                    sti.Kod4 = stItem.Kod4;
                    sti.Kod5 = stItem.Kod5;
                    sti.Kod6 = stItem.Kod6;
                    sti.Kod7 = stItem.Kod7;
                    sti.Kod8 = stItem.Kod8;
                    sti.Kod9 = stItem.Kod9;
                    sti.Kod10 = stItem.Kod10;
                    sti.Kod11 = stItem.Kod11;
                    sti.Kod12 = stItem.Kod12;
                    sti.Kod13 = stItem.Kod13;
                    sti.Kod14 = stItem.Kod14;
                    sti.ValorGun = stItem.ValorGun;
                    sti.Operator = stItem.Operator;
                    sti.KaynakIrsEvrakNo = stItem.KaynakIrsEvrakNo;
                    sti.KaynakIrsTarih = stItem.KaynakIrsTarih;
                    sti.KaynakIIFEvrakNo = stItem.KaynakIIFEvrakNo;
                    sti.KaynakIIFTarih = stItem.KaynakIIFTarih;
                    sti.KaynakSiparisNo = stItem.KaynakSiparisNo;
                    sti.MFKAciklama = stItem.MFKAciklama;
                    sti.MFKTarih = stItem.MFKTarih;
                    sti.Kredi_Donem_VadeTarih = stItem.Kredi_Donem_VadeTarih;
                    sti.Nesne1 = stItem.Nesne1;
                    sti.Nesne2 = stItem.Nesne2;
                    sti.Nesne3 = stItem.Nesne3;
                    sti.EvrakTarih = stItem.EvrakTarih;
                    sti.SevkTarih = stItem.SevkTarih;
                }
                else
                {
                    sti.KayitTipi = STIKayitTipi.Irsaliye;
                    sti.KaynakSiparisNo = "";
                    sti.KaynakSiparisTarih = 0;
                    sti.SiparisSiraNo = 0;
                    sti.SiparisMiktar = 0;
                }

                STIBaseList.Add(sti);
            }

            var IrsIslem = new Irsaliye_Islemleri(irsaliye.SirketKod, SqlExper);
            try
            {
                return IrsIslem.Irsaliye_Kayit(-1, efatKullanici, STIBaseList);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
        }

        /// <summary>
        /// sayım fişi oluştur
        /// </summary>
        public Result SayımVeFarkFişi(List<Entity.STI> stiList, int EvrakSeriNo, bool EvrakNoArttır, string username)
        {
            // evrak no
            var evrakno = EvrakNo(EvrakSeriNo);
            if (evrakno == null || evrakno == "")
                return new Result(false, "Evrak Seri hatası. Lütfen terminal yetkilerinden seriyi değiştirin yada Güneşten seçili seri için bir değer verin.");
            // definitions
            var fn = new Functions();
            var tarih = fn.ToOADate();
            var saat = fn.ToOATime();
            // sql exper
            foreach (var item in stiList)
            {
                item.EvrakNo = evrakno;
                item.Kaydeden = username;
                item.KayitTarih = tarih;
                item.KayitSaat = saat;
                item.KayitKaynak = 154;
                item.KayitSurum = "9.10.017";
                item.Degistiren = username;
                item.DegisTarih = tarih;
                item.DegisSaat = saat;
                item.DegisKaynak = 154;
                item.DegisSurum = "9.10.017";
                item.CheckSum = 12;
                item.IrsFat = -1;
                item.KDVDahilHaric = -1;
                item.OtvDahilHaric = -1;
                item.MlyYontem = -1;
                item.MhsDurum = 1;
                item.MlyMhs = 1;
                item.MhsTabloNo = 1;
                item.Katsayi = 1;
                item.ErekIFMiktar = 0;
                item.ErekIIFMiktar = 0;
                item.IrsFat2 = -1;
                item.KrediArindirmaSekli = -1;
                item.FinansmanGiderTuru = -1;
                item.Duz_Yapilan_Yil = -1;
                item.Duz_Yapilan_Donem = -1;
                item.Duz_Mhs_Durumu = -1;
                item.Duz_Mly_Yontemi = -1;
                item.Duz_Mly_Mhs_Durumu = -1;
                item.DvzKurCinsi = -1;
                item.IthalatMDagDurum = -1;
                item.EFatTip = -1;
                item.EFatDurum = -1;
                item.IthalatMDagYontem = -1;
                item.OTVTevkifatVarYok = -1;
                item.EArsivTeslimSekli = -1;
                item.EArsivFaturaTipi = -1;
                item.EArsivFaturaDurum = -1;
                SqlExper.Insert(item);
            }

            var sonuc = SqlExper.AcceptChanges();
            if (sonuc.Status)
            {
                sonuc.Message = evrakno;
                // evrak no arttır
                if (EvrakNoArttır)
                {
                    var seri = string.Empty;
                    var noStr = string.Empty;
                    if (evrakno.Length == 8)
                    {
                        seri = evrakno.Substring(0, 2);
                        noStr = evrakno.Substring(2, 6);
                    }
                    else if (evrakno.Length == 7)
                    {
                        seri = evrakno.Substring(0, 1);
                        noStr = evrakno.Substring(1, 6);
                    }

                    var no = noStr.ToInt32();
                    var evrakNoArti = Helper.EvrakNoOlustur(8, seri, no);
                    SqlExper.Komut("UPDATE FINSAT6{0}.FINSAT6{0}.INI SET MyValue={{0}} WHERE MySection = 1 AND MyEntry = {{1}}", evrakNoArti, EvrakSeriNo);
                    SqlExper.AcceptChanges();
                }
            }

            return sonuc;
        }
    }
}