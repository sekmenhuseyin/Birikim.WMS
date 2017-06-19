using OnikimCore.GunesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TumFaturaKayit;
using Wms12m.Entity;
using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Finsat
    {
        private string ConStr { get; set; }
        private string SirketKodu { get; set; }
        /// <summary>
        /// yeni finsat
        /// </summary>
        public Finsat(string conStr, string sirketKodu)
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
        /// sayım fişi oluştur
        /// </summary>
        public Result SayımVeFarkFişi(List<Entity.STI> stiList, int EvrakSeriNo, bool EvrakNoArttır, string username)
        {
            //evrak no
            string evrakno = EvrakNo(EvrakSeriNo);
            if (evrakno == null || evrakno == "")
                return new Result(false, "Evrak Seri hatası.");
            //definitions
            Functions fn = new Functions();
            int tarih = fn.ToOADate();
            int saat = fn.ToOATime();
            SqlExper sqlexper = new SqlExper(ConStr, SirketKodu);
            //sql exper
            foreach (var item in stiList)
            {
                item.EvrakNo = evrakno;
                item.Kaydeden = username;
                item.KayitTarih = tarih;
                item.KayitSaat = saat;
                item.KayitKaynak = 153;
                item.KayitSurum = "9.01.028";
                item.Degistiren = username;
                item.DegisTarih = tarih;
                item.DegisSaat = saat;
                item.DegisKaynak = 153;
                item.DegisSurum = "9.01.028";
                item.CheckSum = 12;
                sqlexper.Insert(item);
            }
            var sonuc = sqlexper.AcceptChanges();
            if (sonuc.Status == true)
            {
                sonuc.Message = evrakno;
                //evrak no arttır
                if (EvrakNoArttır == true)
                {
                    string seri = string.Empty;
                    string noStr = string.Empty;
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
                    int no = noStr.ToInt32();
                    string evrakNoArti = Helper.EvrakNoOlustur(8, seri, no);
                    sqlexper.Komut("UPDATE FINSAT6{0}.FINSAT6{0}.INI SET MyValue={{0}} WHERE MySection = 1 AND MyEntry = {{1}}", evrakNoArti, EvrakSeriNo);
                    sqlexper.AcceptChanges();
                }
            }
            return sonuc;
        }
        /// <summary>
        /// malkabul işlemleri
        /// </summary>
        public Result MalKabul(IR irsaliye, int kulID)
        {
            DevHelper.Ayarlar.SetConStr(ConStr);
            DevHelper.Ayarlar.SirketKodu = irsaliye.SirketKod;
            List<STIBase> STIBaseList = new List<STIBase>();
            using (WMSEntities db = new WMSEntities())
            {
                string kaydeden = db.Users.Where(m => m.ID == kulID).Select(m => m.Kod).FirstOrDefault();
                string sql = String.Format("SELECT IRS.EvrakNo, IRS_Detay.IrsaliyeID, IRS_Detay.MalKodu, IRS_Detay.Miktar, IRS_Detay.Birim, ISNULL(IRS_Detay.OkutulanMiktar, 0) AS OkutulanMiktar, Depo.DepoKodu, IRS.HesapKodu, IRS.Tarih, " +
                                            "(SELECT MalAdi FROM FINSAT6{0}.FINSAT6{0}.STK WITH(NOLOCK) WHERE (MalKodu = IRS_Detay.MalKodu)) AS MalAdi," +
                                            "ISNULL(IRS_Detay.KynkSiparisNo, '') AS SiparisNo, ISNULL(IRS_Detay.KynkSiparisSiraNo, 0) AS KynkSiparisSiraNo, ISNULL(IRS_Detay.KynkSiparisTarih, 0) AS KynkSiparisTarih, ISNULL(IRS_Detay.KynkSiparisMiktar, 0) AS KynkSiparisMiktar, " +
                                            "FINSAT6{0}.FINSAT6{0}.SPI.BirimFiyat AS Fiyat, FINSAT6{0}.FINSAT6{0}.SPI.KDVOran, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran1, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran2, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran3, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran4, FINSAT6{0}.FINSAT6{0}.SPI.IskontoOran5 " +
                                            "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH (NOLOCK) RIGHT OUTER JOIN wms.Depo WITH(NOLOCK) INNER JOIN wms.IRS WITH(NOLOCK) ON wms.Depo.ID = wms.IRS.DepoID INNER JOIN wms.IRS_Detay WITH(NOLOCK) ON wms.IRS.ID = wms.IRS_Detay.IrsaliyeID ON FINSAT6{0}.FINSAT6{0}.SPI.Chk = wms.IRS.HesapKodu AND FINSAT6{0}.FINSAT6{0}.SPI.Tarih = wms.IRS_Detay.KynkSiparisTarih AND FINSAT6{0}.FINSAT6{0}.SPI.SiraNo = wms.IRS_Detay.KynkSiparisSiraNo AND FINSAT6{0}.FINSAT6{0}.SPI.EvrakNo = wms.IRS_Detay.KynkSiparisNo " +
                                            "WHERE (IRS_Detay.IrsaliyeID = {1}) AND (IRS_Detay.OkutulanMiktar IS NOT NULL) AND (IRS_Detay.OkutulanMiktar > 0)", irsaliye.SirketKod, irsaliye.ID);
                var STList = db.Database.SqlQuery<STIMax>(sql).ToList();
                foreach (STIMax stItem in STList)
                {
                    STIBase sti = new STIBase()
                    {
                        EvrakNo = stItem.EvrakNo,
                        HesapKodu = stItem.HesapKodu,
                        Tarih = stItem.Tarih.IntToDate(),
                        MalKodu = stItem.MalKodu,
                        Miktar = stItem.OkutulanMiktar,
                        Birim = stItem.Birim,
                        Depo = stItem.DepoKodu,
                        EvrakTipi = STIEvrakTipi.AlimIrsaliyesi,
                        Kaydeden = kaydeden,
                        KayitSurum = "9.01.028",
                        KayitKaynak = 74
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
            }
            Irsaliye_Islemleri IrsIslem = new Irsaliye_Islemleri(irsaliye.SirketKod);
            var Sonuc = new OnikimCore.GunesCore.IslemSonuc(false);
            try
            {
                Sonuc = IrsIslem.Irsaliye_Kayit(-1, STIBaseList);
            }
            catch (Exception ex)
            {
                Sonuc.Basarili = false;
                Sonuc.Hata = ex;
            }
            //sonuç döner
            if (Sonuc.Hata.IsNull())
                return new Result(true, Sonuc.Veri.ToString2());
            else
                return new Result(false, Sonuc.Hata.Message);
        }
        /// <summary>
        /// satış irsaliyesi ve faturası
        /// </summary>
        public Result FaturaKayıt(int irsID, string DepoKodu, bool efatKullanici, int Tarih, string CHK, string kaydeden, int IrsaliyeSeri, int FaturaSeri, int yil)
        {
            var STIBaseList = new List<ParamSti>();
            //evrak no getir
            var ftrKayit = new FaturaKayit(ConStr, SirketKodu);
            List<EvrakBilgi> evrkno;
            try
            {
                evrkno = ftrKayit.EvrakNo_Getir(efatKullanici, IrsaliyeSeri, yil);
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
            int saat = DateTime.Now.ToOaTime();
            //listeyi dön
            using (WMSEntities db = new WMSEntities())
            {
                string sql = String.Format("SELECT MalKodu, Miktar, Birim, KynkSiparisNo as EvrakNo, KynkSiparisTarih, KynkSiparisSiraNo  FROM wms.IRS_Detay WITH (NOLOCK) WHERE IrsaliyeID={0}", irsID);
                var list = db.Database.SqlQuery<STIMax>(sql).ToList();
                foreach (STIMax item in list)
                {
                    sql = string.Format("SELECT SPI.Chk, SPI.Miktar, SPI.MalKodu, SPI.Fiyat, SPI.Birim, SPI.Depo, SPI.ToplamIskonto, SPI.KDV, SPI.KDVOran, SPI.IskontoOran1, SPI.IskontoOran2, SPI.IskontoOran3, SPI.IskontoOran4, SPI.IskontoOran5, " +
                                        "SPI.EvrakNo as KaynakSiparisNo, SPI.Tarih as KaynakSiparisTarih, SPI.SiraNo as SiparisSiraNo, Miktar as SPI.SiparisMiktar, SPI.TeslimMiktar, SPI.KapatilanMiktar, SPI.FytListeNo, SPI.ValorGun, SPI.Kod1, SPI.Kod2, SPI.Kod3, SPI.Kod10, SPI.Kod13, SPI.Kod14, SPI.KayitKaynak, SPI.KayitSurum, SPI.DegisKaynak, SPI.DegisSurum," +
                                        "CHK.EFatKullanici, STK.SatislarHesabi, CHK.EArsivTeslimSekli " +
                                        "FROM FINSAT6{0}.FINSAT6{0}.SPI WITH (NOLOCK) LEFT JOIN FINSAT671.FINSAT671.CHK WITH (NOLOCK) ON SPI.Chk=CHK.HesapKodu LEFT JOIN FINSAT671.FINSAT671.STK WITH (NOLOCK) ON STK.MalKodu=SPI.MalKodu " +
                                        "WHERE (EvrakNo = '{1}') AND (Chk = '{2}') AND (Depo = '{3}') AND (Tarih = {4}) AND (SiraNo = {5}) AND (KynkEvrakTip = 62) AND (SiparisDurumu = 0) AND (Kod10 IN ('Terminal', 'Onaylandı'))", SirketKodu, item.EvrakNo, CHK, DepoKodu, item.KynkSiparisTarih, item.KynkSiparisSiraNo);
                    var finsat = db.Database.SqlQuery<ParamSti>(sql).FirstOrDefault();
                    if (finsat != null)
                    {
                        finsat.Miktar = item.Miktar;
                        finsat.EvrakNo = evrkno[0].EvrakNo;
                        finsat.KaynakIrsEvrakNo = evrkno[1].EvrakNo;
                        finsat.Tarih = Tarih;
                        finsat.Kaydeden = kaydeden;
                        finsat.KayitSurum = "9.01.028";
                        finsat.KayitKaynak = 74;
                        STIBaseList.Add(finsat);
                    }
                }
            }
            //finsat işlemleri
            try
            {
                if (STIBaseList.Count > 0)
                {
                    var sonuc = ftrKayit.FaturaKaydet(STIBaseList, efatKullanici, FaturaSeri, yil);
                    return new Result(sonuc.Basarili, sonuc.Mesaj);
                }
                else
                    return new Result(false, "Bu sipariş kapanmış");
            }
            catch (Exception ex)
            {
                return new Result(false, ex.Message);
            }
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