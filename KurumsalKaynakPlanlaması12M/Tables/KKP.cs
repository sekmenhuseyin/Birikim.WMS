using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace KurumsalKaynakPlanlaması12M
{
    public class KKPSorguSonuc
    {
        public string TableName { get; set; }
        public string Sorgu { get; set; }
        public bool UpdateKontrol { get; set; }
        public List<SqlParameter> Parameters { get; set; }
        
        public KKPSorguSonuc() 
            :this("","", new List<SqlParameter>())
        {
            
        }

        public KKPSorguSonuc(string tableName, string sorgu, List<SqlParameter> parameters)
        {
            TableName = tableName;
            Sorgu = sorgu;
            Parameters = parameters;
        }

        public KKPSorguSonuc(string tableName, string sorgu, List<SqlParameter> parameters, bool updateKontrol)
            :this(tableName, sorgu, parameters)
        {
            UpdateKontrol = updateKontrol;
        }

        public override string ToString()
        {
            return TableName;
        }
    }

    public class PropertyValue
    {
        public string PropertiName { get; set; }
        public object OldValue { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", PropertiName, OldValue);
        }
    }

    public class KKPCon : IDisposable
    {
        public SqlConnection SqlCon { get; private set; }
        public SqlCommand SqlCmd { get; private set; }
        public SqlDataReader SqlDr { get; private set; }

        public string CommandText { get { return SqlCmd.CommandText; } set { SqlCmd.CommandText = value; } }

        internal KKPCon(string kkpConStr)
        {
            SqlCon = new SqlConnection(kkpConStr);
            SqlCmd = new SqlCommand("", SqlCon);
        }

        public SqlDataReader ExecuteReader()
        {
            if (SqlCon.State != ConnectionState.Open)
                SqlCon.Open();

            SqlDr = SqlCmd.ExecuteReader();
            return SqlDr;
        }

        public int ExecuteNonQuery()
        {
            if (SqlCon.State != ConnectionState.Open)
                SqlCon.Open();
            return SqlCmd.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            if (SqlCon.State != ConnectionState.Open)
                SqlCon.Open();
            return SqlCmd.ExecuteScalar();
        }


        public void Dispose()
        {
            if (SqlCmd != null)
                SqlCmd.Dispose();

            if (SqlDr != null)
                SqlDr.Dispose();

            if (SqlCon != null)
                SqlCon.Dispose();
        }
    }

    
    public class KKP : IDisposable
    {
        public const string LinkSurum = "9.01.017";//"8.20.204";

        List<KKPSorguSonuc> SqlSorguParam { get; set; }

        #region Variable

        public string ConStr { get; private set; }

        public string SirketKodu { get; private set; }

        public Exception Exc { get; private set; }

        public KKPCon MyCon { get; private set; }

        #region Table

        public STKTable STKs { get; private set; }
        public CHKTable CHKs { get; private set; }
        public DEPTable DEPs { get; private set; }
        public FYTTable FYTs { get; private set; }
        public SPITable SPIs { get; private set; }
        public MFKTable MFKs { get; private set; }
        public FTDTable FTDs { get; private set; }

        #endregion

        #region EVRAK

        public List<KKPEvrakSiparis> SiparisEvrakList { get; set; }
        public List<KKPEvrakOzet> SiparisEvrakDeleteList { get; set; }

        public List<KKPEvrakStokGirisCikis> StokGirisCikisEvrakList { get; set; }
        public List<KKPEvrakOzet> StokGirisCikisEvrakDeleteList { get; set; }

        public List<KKPEvrakMuhFis> MuhasebeFisEvrakList { get; set; }

        //public List<KKPEvrakYNSSiparis> SiparisEvrakListYeniNesil { get; set; }

        public List<KKPEvrakIrsaliye> IrsaliyeEvrakList { get; set; }
        public List<KKPEvrakOzet> IrsaliyeEvrakDeleteList { get; set; }


        #endregion


        #endregion

        TakvimeBagliKisit.TBKIslem Takvim { get; set; }

        public KKP(string conStr, string sirketKod)
        {
            SqlSorguParam = new List<KKPSorguSonuc>();

            STKs = new STKTable();
            CHKs = new CHKTable();
            DEPs = new DEPTable();
            FYTs = new FYTTable();
            SPIs = new SPITable();
            MFKs = new MFKTable();
            FTDs = new FTDTable();


            SiparisEvrakList = new List<KKPEvrakSiparis>();
            SiparisEvrakDeleteList = new List<KKPEvrakOzet>();

            StokGirisCikisEvrakList = new List<KKPEvrakStokGirisCikis>();
            StokGirisCikisEvrakDeleteList = new List<KKPEvrakOzet>();

            MuhasebeFisEvrakList = new List<KKPEvrakMuhFis>();


            //SiparisEvrakListYeniNesil = new List<KKPEvrakYNSSiparis>();

            IrsaliyeEvrakList = new List<KKPEvrakIrsaliye>();
            IrsaliyeEvrakDeleteList = new List<KKPEvrakOzet>();
            

            ConStr = conStr;
            SirketKodu = sirketKod;

            MyCon = new KKPCon(ConStr);

            Takvim = new TakvimeBagliKisit.TBKIslem(conStr, sirketKod);

        }


        #region Functions

        public void ClearEntities()
        {
            STKs.Clear();
            CHKs.Clear();
            DEPs.Clear();
            FYTs.Clear();
            SPIs.Clear();
            MFKs.Clear();
            FTDs.Clear();

            SiparisEvrakList.Clear();
            SiparisEvrakDeleteList.Clear();

            StokGirisCikisEvrakList.Clear();
            StokGirisCikisEvrakDeleteList.Clear();

            MuhasebeFisEvrakList.Clear();

            IrsaliyeEvrakList.Clear();
            IrsaliyeEvrakDeleteList.Clear();
        }

        public void UpdateChanges()
        {
            Exc = null;

            List<KKPSorguSonuc> listSorguSonuc = new List<KKPSorguSonuc>();

            if (SqlSorguParam.Count > 0)
                listSorguSonuc.AddRange(SqlSorguParam);


            #region SORGULARIN HAZIRLANIŞI

     

            #region STK

            foreach (KKP_STK stk in STKs.InsertList)
            {
                listSorguSonuc.Add(Data.GetSqlInsert(stk, "STK", SirketKodu, false));
            }

            foreach (KKP_STK stk in STKs.UpdateList)
            {
                listSorguSonuc.Add(Data.GetSqlUpdate(stk, "STK", SirketKodu));
            }

            foreach (KKP_STK stk in STKs.DeleteList)
            {
                listSorguSonuc.Add(Data.GetSqlDelete(stk, "STK", SirketKodu));
            }

            #endregion STK

            #region CHK

            foreach (KKP_CHK chk in CHKs.InsertList)
            {
                listSorguSonuc.Add(Data.GetSqlInsert(chk, "CHK", SirketKodu, false));
            }
            foreach (KKP_CHK chk in CHKs.UpdateList)
            {
                listSorguSonuc.Add(Data.GetSqlUpdate(chk, "CHK", SirketKodu));
            }
            foreach (KKP_CHK chk in CHKs.DeleteList)
            {
                listSorguSonuc.Add(Data.GetSqlDelete(chk, "CHK", SirketKodu));
            }
            #endregion CHK

            #region DEP

            foreach (KKP_DEP dep in DEPs.InsertList)
            {
                listSorguSonuc.Add(Data.GetSqlInsert(dep, "DEP", SirketKodu, false));
            }
            foreach (KKP_DEP dep in DEPs.UpdateList)
            {
                listSorguSonuc.Add(Data.GetSqlUpdate(dep, "DEP", SirketKodu));
            }
            foreach (KKP_DEP dep in DEPs.DeleteList)
            {
                listSorguSonuc.Add(Data.GetSqlDelete(dep, "DEP", SirketKodu));
            }
            #endregion DEP

            #region SPI

            foreach (KKP_SPI spi in SPIs.InsertList)
            {
                listSorguSonuc.Add(Data.GetSqlInsert(spi, "SPI", SirketKodu, false));
            }

            foreach (KKP_SPI spi in SPIs.UpdateList)
            {
                listSorguSonuc.Add(Data.GetSqlUpdate(spi, "SPI", SirketKodu));
            }

            foreach (KKP_SPI spi in SPIs.DeleteList)
            {
                listSorguSonuc.Add(Data.GetSqlDelete(spi, "SPI", SirketKodu));
            }

            #endregion

            #region MFK

            foreach (KKP_MFK mfk in MFKs.InsertList)
            {
                listSorguSonuc.Add(Data.GetSqlInsert(mfk, "MFK", SirketKodu, false));
            }

            foreach (KKP_MFK mfk in MFKs.UpdateList)
            {
                listSorguSonuc.Add(Data.GetSqlUpdate(mfk, "MFK", SirketKodu));
            }

            foreach (KKP_MFK mfk in MFKs.DeleteList)
            {
                listSorguSonuc.Add(Data.GetSqlDelete(mfk, "MFK", SirketKodu));
            }


            #endregion



            #region Sipariş Evrak

            foreach (KKPEvrakSiparis item in SiparisEvrakList)
            {
                listSorguSonuc.AddRange(SiparisKayit(item));
            }
            #region evrak delete
                
            foreach (KKPEvrakOzet ozet in SiparisEvrakDeleteList)
            {
                listSorguSonuc.AddRange(SiparisSil((KKPSiparisTip)ozet.KynkEvrakTip, ozet.EvrakNo, ozet.Tarih, ozet.HesapKodu));
            }
            #endregion evrak delete son

            #endregion Sipariş Evrak

            #region Stok Giriş Çıkış Evrak

            foreach (KKPEvrakStokGirisCikis fis in StokGirisCikisEvrakList)
            {
                listSorguSonuc.AddRange(StokGirisCikisFisiKayit(fis));
            }

            foreach (KKPEvrakOzet ozet in StokGirisCikisEvrakDeleteList)
            {
                listSorguSonuc.AddRange(StokGirisCikisFisiSil((KKPStokGirisCikisTip)ozet.KynkEvrakTip, ozet.EvrakNo, ozet.Tarih, ozet.MuhasebeKaydiniSil));
            }

            #endregion Stok Giriş Çıkış Evrak SON


            #region İrsaliye Evrak

            foreach (KKPEvrakIrsaliye item in IrsaliyeEvrakList)
            {
                listSorguSonuc.AddRange(IrsaliyeKayit(item));
            }

            foreach (KKPEvrakOzet ozet in IrsaliyeEvrakDeleteList)
            {
                listSorguSonuc.AddRange(IrsaliyeSil((KKPIrsaliyeTip)ozet.KynkEvrakTip, ozet.EvrakNo, ozet.Tarih, ozet.HesapKodu));
            }


            #endregion

            #region Muhasebe Fiş

            foreach (KKPEvrakMuhFis fis in MuhasebeFisEvrakList)
            {
                listSorguSonuc.AddRange(MuhFisKayit(fis));
            }

            #endregion


            #endregion SORGULARIN HAZIRLANIŞI-SON


            using (TransactionScope ts = new TransactionScope())
            {

                using (SqlConnection con = new SqlConnection(ConStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("", con);

                    foreach (KKPSorguSonuc srg in listSorguSonuc)
                    {
                        if (srg.Sorgu == "-")  //getsqlupdate fonksiyonunda update ediecek bir alan yoksa "-" atılıyor.
                            continue;
                        cmd.CommandText = srg.Sorgu;
                        cmd.Parameters.AddRange(srg.Parameters.ToArray());
                        int kayitsay = cmd.ExecuteNonQuery();
                        if (kayitsay <= 0 && srg.UpdateKontrol)
                            throw new Exception("Kayıt bulunamadı!!");

                        cmd.Parameters.Clear();
                    }
                }

                ts.Complete();
            }
        }


        public void ExecuteCommandOnUpdate(string commandText, bool updateControl, params SqlParameter[] parameters)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc();
            sonuc.Sorgu = commandText;
            sonuc.Parameters.AddRange(parameters);
            sonuc.UpdateKontrol = updateControl;
            SqlSorguParam.Add(sonuc);
        }

        #endregion Functions



        #region Sipariş ile ilgili Fonksiyonlar
        
        public KKPEvrakSiparis SiparisEvrakGetir(KKPSiparisTip sipTip, string evrakNo, int tarih, string hesapKodu)
        {
            KKPEvrakSiparis evrak = new KKPEvrakSiparis(sipTip);
            evrak.fromLoad = true;
            evrak.EvrakNo = evrakNo;
            evrak.Tarih = new DateTime(1900, 1, 1).AddDays(tarih - 2);
            evrak.HesapKodu = hesapKodu;

            using (KKPCon con = new KKPCon(ConStr))
            {
                con.CommandText = string.Format(
@"SELECT SPI.*, STK.MalAdi
, ISNULL(ST.TesisKodu,'') as TesisKodu
, ISNULL(ST.Kaydeden,'') as TalepEden
,ISNULL((select HesapAd from MUHASEBE6{0}.MUHASEBE6{0}.MMK(nolock) M where M.HesapKod = ST.TesisKodu ),'') as TesisAdi 
, ISNULL((select Unvan1+ ' ' + Unvan2 from FINSAT6{0}.FINSAT6{0}.CHK (nolock) CH where CH.HesapKodu = '{4}'),'') as Unvan 
, ISNULL(SPI.Kod1,'') as TalepNo
FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock) 
LEFT JOIN Kaynak.sta.Talep(NOLOCK) ST ON  SPI.KynkEvrakTip=63 AND SPI.EvrakNo=ST.SipEvrakNo AND SPI.Chk=ST.HesapKodu AND SPI.Kod1=ST.TalepNo AND SPI.MalKodu= ST.MalKodu
LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) ON STK.MalKodu=SPI.MalKodu
WHERE SPI.KynkEvrakTip={1} AND SPI.EvrakNo='{2}' AND SPI.Tarih={3} AND SPI.Chk='{4}'

SELECT * FROM FINSAT6{0}.FINSAT6{0}.FTD (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND HesapKodu='{4}'

SELECT * FROM FINSAT6{0}.FINSAT6{0}.MFK (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND EvrakTarih={3} AND HesapKod='{4}'
"
                    , SirketKodu
                    , (int)sipTip
                    , evrakNo
                    , tarih
                    , hesapKodu);

                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    KKP_SPI spi = new KKP_SPI();
                    Data.TabloDoldur(spi, dr);
                    evrak.listSPI.Add(spi);

                    spi.PropertyChanged += spi.SPI_PropertyChanged;
                }

                dr.NextResult();
                while (dr.Read())
                {
                    KKP_FTD ftd = new KKP_FTD();
                    Data.TabloDoldur(ftd, dr);
                    evrak.FTDList.Add(ftd);

                    ftd.PropertyChanged += ftd.FTD_PropertyChanged;
                }

                dr.NextResult();
                while (dr.Read())
                {
                    KKP_MFK mfk = new KKP_MFK();
                    Data.TabloDoldur(mfk, dr);
                    evrak.SetMfk(mfk);

                    mfk.PropertyChanged += mfk.MFK_PropertyChanged;
                }
            }

            if (evrak.listSPI.Count > 0)
            {
                KKP_SPI ilkSatir = evrak.listSPI[0];

                evrak.TeslimYeriKodu = ilkSatir.TeslimChk;
                evrak.TahTeslimTarihi = new DateTime(1900, 1, 1).AddDays(ilkSatir.TahTeslimTarih - 2);
                evrak.IslemTip = (KKPIslemTipSPI)ilkSatir.IslemTip;
                if (evrak.listSPI.Any(t => t.SiparisDurumu == 0))
                    evrak.SiparisDurumu = KKPSiparisDurumu.Açık;
                else
                    evrak.SiparisDurumu = KKPSiparisDurumu.Kapalı;

                if (evrak.FTDList.Count == 0)
                {
                    evrak.dvzTL = (KKPDvzTL)ilkSatir.DvzTL;
                    evrak.DovizCinsi = ilkSatir.DovizCinsi;
                    evrak.DovizKuru = ilkSatir.DovizKuru;
                }
                else
                {
                    evrak.dvzTL = (KKPDvzTL)evrak.FTDList[0].DvzTL;
                    evrak.DovizCinsi = evrak.FTDList[0].DovizCinsi;
                    evrak.DovizKuru = evrak.FTDList[0].DovizKuru;
                }

                evrak.GuncellemeModu = true;
                evrak.GuncellenecekEvrak.EvrakNo = ilkSatir.EvrakNo;
                evrak.GuncellenecekEvrak.HesapKodu = ilkSatir.Chk;
                evrak.GuncellenecekEvrak.Tarih = ilkSatir.Tarih;
                evrak.GuncellenecekEvrak.KynkEvrakTip = ilkSatir.KynkEvrakTip;

            }
            evrak.fromLoad = false;
            return evrak;
        }

        private KKPEvrakSiparis SiparisSilinecekEvrakBilgi(KKPSiparisTip sipTip, string evrakNo, int tarih, string hesapKodu)
        {
            KKPEvrakSiparis evrak = new KKPEvrakSiparis(sipTip);
            evrak.fromLoad = true;
            evrak.EvrakNo = evrakNo;
            evrak.Tarih = new DateTime(1900, 1, 1).AddDays(tarih - 2);
            evrak.HesapKodu = hesapKodu;
            using (KKPCon con = new KKPCon(ConStr))
            {
                con.CommandText = string.Format(
@"SELECT IslemTur, EvrakNo, Tarih, Chk, SiraNo, MalKodu, KynkEvrakTip, Miktar, Depo
, Kaydeden, KayitTarih, KayitSaat, KayitKaynak, KayitSurum, ROW_ID 
FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND Chk='{4}'"
                    , SirketKodu
                    , (int)sipTip
                    , evrakNo
                    , tarih
                    , hesapKodu);

                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    KKP_SPI spi = new KKP_SPI();
                    Data.TabloDoldur(spi, dr);
                    evrak.listSPI.Add(spi);
                }
            }
            evrak.fromLoad = false;
            return evrak;
        }

        List<KKPSorguSonuc> SiparisSil(KKPSiparisTip siptip, string evrakNo, int tarih, string hesapKodu)
        {
            Takvim.IzinFINSAT(tarih, false, TakvimeBagliKisit.MesajTip.ThrowException);

            KKPEvrakSiparis silevrak = SiparisSilinecekEvrakBilgi(siptip, evrakNo, tarih, hesapKodu);
            KKPSorguSonuc sonucSTK = STKUpdateFROMSPI(silevrak.listSPI, true);
            List<KKPSorguSonuc> sonucDSTList = DSTUpdateFROMSPI(silevrak.listSPI, true);

            KKPSorguSonuc sonucDelete = new KKPSorguSonuc() { TableName = "SPI" };

            sonucDelete.Sorgu = string.Format(
@"DELETE FROM FINSAT6{0}.FINSAT6{0}.SPI WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND Chk='{4}'
DELETE FROM FINSAT6{0}.FINSAT6{0}.FTD WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND HesapKodu='{4}'
DELETE FROM FINSAT6{0}.FINSAT6{0}.MFK WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND EvrakTarih={3} AND HesapKod='{4}'"
                     , SirketKodu
                    , (int)siptip
                    , evrakNo
                    , tarih
                    , hesapKodu);


            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();
            list.Add(sonucSTK);
            list.AddRange(sonucDSTList);
            list.Add(sonucDelete);

            return list;
        }

        List<KKPSorguSonuc> SiparisKayit(KKPEvrakSiparis evrak)
        {

            Takvim.IzinFINSAT(evrak.Tarih, !evrak.GuncellemeModu, TakvimeBagliKisit.MesajTip.ThrowException);

            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();
            KKPEvrakSiparis silevrak = null;
            if (evrak.GuncellemeModu)
            {
                silevrak = SiparisSilinecekEvrakBilgi(evrak.siparisTip, evrak.GuncellenecekEvrak.EvrakNo, evrak.GuncellenecekEvrak.Tarih, evrak.GuncellenecekEvrak.HesapKodu);
                list.AddRange(SiparisSil(evrak.siparisTip, evrak.GuncellenecekEvrak.EvrakNo, evrak.GuncellenecekEvrak.Tarih, evrak.GuncellenecekEvrak.HesapKodu));
            }

            list.Add(STKUpdateFROMSPI(evrak.listSPI, false));
            list.AddRange(DSTUpdateFROMSPI(evrak.listSPI, false));

            foreach (KKP_SPI spi in evrak.listSPI)
            {
                bool identity_insertON = false;
                if (silevrak != null)
                {
                    KKP_SPI silsatir = silevrak.listSPI.Find(t => t.SiraNo == spi.SiraNo && t.MalKodu == spi.MalKodu);
                    if (silsatir != null)
                    {
                        spi.Kaydeden = silsatir.Kaydeden;
                        spi.KayitTarih = silsatir.KayitTarih;
                        spi.KayitSaat = silsatir.KayitSaat;
                        spi.KayitKaynak = silsatir.KayitKaynak;
                        spi.KayitSurum = silsatir.KayitSurum;

                        spi.ROW_ID = silsatir.ROW_ID;
                        identity_insertON = true;
                    }
                }

                list.Add(Data.GetSqlInsert(spi, "SPI", SirketKodu, identity_insertON));
            }

            foreach (KKP_FTD ftd in evrak.FTDList)
            {
                list.Add(Data.GetSqlInsert(ftd, "FTD", SirketKodu, false));
            }
            list.Add(Data.GetSqlInsert(evrak.MFK, "MFK", SirketKodu, false));

            ///Güncelleme modunda evrak no INI güncellenemez
            if (evrak.GuncellemeModu == false && evrak.INIGuncellensin)
            {
                string yenievrakNo = MyHelper.EvrakNoArtir(evrak.EvrakNo);

                list.Add(INIUpdate(evrak.KynkEvrTip, yenievrakNo, evrak.EvrakSeri));
            }

            return list;
        }


        #endregion Sipariş ile ilgili Fonksiyonlar_son


        #region STok Giriş Çıkış ile ilgili Fonksiyonlar

        public KKPEvrakStokGirisCikis StokGirisCikisEvrakGetir(KKPStokGirisCikisTip tip, string evrakNo, int tarih)
        {
            KKPEvrakStokGirisCikis evrak = new KKPEvrakStokGirisCikis(tip);
            evrak.fromLoad = true;
            evrak.EvrakNo = evrakNo;
            evrak.Tarih = new DateTime(1900, 1, 1).AddDays(tarih - 2);

            using (KKPCon con = new KKPCon(ConStr))
            {
                con.CommandText = string.Format(@"SELECT * FROM FINSAT6{0}.FINSAT6{0}.STI (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3}"
                    , SirketKodu
                    , (short)evrak.KynkEvrTip
                    , evrakNo
                    , tarih);

                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    KKP_STI sti = new KKP_STI();
                    Data.TabloDoldur(sti, dr);
                    evrak.listSTI.Add(sti);

                    sti.PropertyChanged += sti.STI_PropertyChanged;
                }
            }
            if (evrak.listSTI.Count > 0)
            {
                KKP_STI ilksatir = evrak.listSTI[0];
                evrak.IslemTip = (KKPIslemTipSPI)ilksatir.IslemTip;

                //evrak.Kaydeden = ilksatir.Kaydeden;
                //evrak.KayitTarih = ilksatir.KayitTarih;
                //evrak.KayitSaat = ilksatir.KayitSaat;

                //evrak.Degistiren = ilksatir.Degistiren;
                //evrak.DegisTarih = ilksatir.DegisTarih;
                //evrak.DegisSaat = ilksatir.DegisSaat;

                evrak.GuncellemeModu = true;
                evrak.GuncellenecekEvrak.EvrakNo = ilksatir.EvrakNo;
                evrak.GuncellenecekEvrak.HesapKodu = ilksatir.Chk;
                evrak.GuncellenecekEvrak.Tarih = ilksatir.Tarih;
                evrak.GuncellenecekEvrak.KynkEvrakTip = ilksatir.KynkEvrakTip;
            }
            evrak.fromLoad = false;
            return evrak;
        }
        
        private KKPEvrakStokGirisCikis StokGirisCikisFisSilinecekEvrakBilgi(KKPStokGirisCikisTip tip, string evrakNo, int tarih)
        {
            KKPEvrakStokGirisCikis evrak = new KKPEvrakStokGirisCikis(tip);
            evrak.fromLoad = true;
            evrak.EvrakNo = evrakNo;
            evrak.Tarih = new DateTime(1900, 1, 1).AddDays(tarih - 2);
            using (KKPCon con = new KKPCon(ConStr))
            {
                con.CommandText = string.Format(
@"SELECT IslemTur, EvrakNo, Tarih, Chk, SiraNo, MalKodu, KynkEvrakTip, Miktar, Depo
, Kaydeden, KayitTarih, KayitSaat, KayitKaynak, KayitSurum, ROW_ID 
FROM FINSAT6{0}.FINSAT6{0}.STI (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3}"
                    , SirketKodu
                    , (int)tip
                    , evrakNo
                    , tarih);

                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    KKP_STI sti = new KKP_STI();
                    Data.TabloDoldur(sti, dr);
                    evrak.listSTI.Add(sti);
                }
            }
            evrak.fromLoad = false;
            return evrak;
        }

        List<KKPSorguSonuc> StokGirisCikisFisiSil(KKPStokGirisCikisTip tip, string evrakNo, int tarih, bool muhFisKaydiniSil)
        {
            Takvim.IzinFINSAT(tarih, false, TakvimeBagliKisit.MesajTip.ThrowException);

            KKPEvrakStokGirisCikis fis = StokGirisCikisFisSilinecekEvrakBilgi(tip, evrakNo, tarih);
            KKPSorguSonuc sonucSTK = STKUpdateFROMSTI(fis.listSTI, true);
            List<KKPSorguSonuc> sonucDSTList = DSTUpdateFROMSTI(fis.listSTI, true);

            KKPSorguSonuc sonucDelete = new KKPSorguSonuc() { TableName = "STI" };
            sonucDelete.Sorgu = string.Format(@"DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3}"
                , SirketKodu
                , (short)fis.KynkEvrTip
                , fis.EvrakNo
                , Convert.ToInt32(fis.Tarih.ToOADate()));

            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();
            list.Add(sonucSTK);
            list.AddRange(sonucDSTList);
            list.Add(sonucDelete);

            if (fis.KynkEvrTip == KKPKynkEvrakTip.StokÇıkışFişi && muhFisKaydiniSil)
            {
                KKPSorguSonuc deleteFis = new KKPSorguSonuc() { TableName = "MHI-ENT DELETE" };
                deleteFis.Sorgu = string.Format(
                @"DELETE FROM MUHASEBE6{0}.MUHASEBE6{0}.MHI WHERE FisTip=4 AND EvrakTip=59 AND KaynakEvrakNo='{1}'
                DELETE FROM MUHASEBE6{0}.MUHASEBE6{0}.ENT WHERE FisTip=4 AND KynkEvrakTip=59 AND EvrakNo='{1}'"
                    , SirketKodu, fis.EvrakNo);

                list.Add(deleteFis);
                
            }



            return list;
        }

        List<KKPSorguSonuc> StokGirisCikisFisiKayit(KKPEvrakStokGirisCikis fis)
        {
            Takvim.IzinFINSAT(fis.Tarih, !fis.GuncellemeModu, TakvimeBagliKisit.MesajTip.ThrowException);

            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();

            KKPEvrakStokGirisCikis silevrak = null;
            if (fis.GuncellemeModu)
            {
                silevrak = StokGirisCikisFisSilinecekEvrakBilgi(fis.GirisCikis, fis.GuncellenecekEvrak.EvrakNo, fis.GuncellenecekEvrak.Tarih);
                list.AddRange(StokGirisCikisFisiSil(fis.GirisCikis, fis.GuncellenecekEvrak.EvrakNo, fis.GuncellenecekEvrak.Tarih, fis.GuncellenecekEvrak.MuhasebeKaydiniSil));
            }

            list.Add(STKUpdateFROMSTI(fis.listSTI, false));
            list.AddRange(DSTUpdateFROMSTI(fis.listSTI, false));

            foreach (KKP_STI sti in fis.listSTI)
            {
                bool identity_insertON = false;
                if (silevrak != null)
                {
                    KKP_STI silsatir = silevrak.listSTI.Find(t => t.MalKodu == sti.MalKodu && t.SiraNo == sti.SiraNo);
                    if (silsatir != null)
                    {
                        sti.Kaydeden = silsatir.Kaydeden;
                        sti.KayitTarih = silsatir.KayitTarih;
                        sti.KayitSaat = silsatir.KayitSaat;
                        sti.KayitKaynak = silsatir.KayitKaynak;
                        sti.KayitSurum = silsatir.KayitSurum;

                        sti.ROW_ID = silsatir.ROW_ID;
                        identity_insertON = true;
                    }
                }
                list.Add(Data.GetSqlInsert(sti, "STI", SirketKodu, identity_insertON));

            }


            ///Güncelleme modunda evrak no INI güncellenemez
            if (fis.GuncellemeModu == false && fis.INIGuncellensin)
            {
                string yenievrakNo = MyHelper.EvrakNoArtir(fis.EvrakNo);

                list.Add(INIUpdate(fis.KynkEvrTip, yenievrakNo, fis.EvrakSeri));
            }

            return list;
        }

        #endregion STok Giriş Çıkış ile ilgili Fonksiyonlar_Son

        #region İrsaliye İle ilgili fonksiyonlar

        public KKPEvrakIrsaliye IrsaliyeEvrakGetir(KKPIrsaliyeTip irsTip, string evrakNo, int tarih, string hesapKodu, string sirketKodu)
        {
            KKPEvrakIrsaliye evrak = new KKPEvrakIrsaliye(irsTip);
            evrak.fromLoad = true;
            evrak.EvrakNo = evrakNo;
            evrak.Tarih = new DateTime(1900, 1, 1).AddDays(tarih - 2);
            evrak.HesapKodu = hesapKodu;

            using (KKPCon con = new KKPCon(ConStr))
            {
                con.CommandText = string.Format(
@"SELECT STI.*,'{0}' as SirketKodu , STK.MalAdi
, ISNULL(TP.TesisKodu,'') as TesisKodu
, ISNULL(TP.Kaydeden,'') as TalepEden
, ISNULL((select HesapAd from MUHASEBE6{0}.MUHASEBE6{0}.MMK(nolock) M where M.HesapKod = TP.TesisKodu ),'') as TesisAdi 
FROM FINSAT6{0}.FINSAT6{0}.STI (nolock) 
LEFT JOIN Kaynak.sta.Talep(NOLOCK) TP ON TP.MalKodu= STI.MalKodu and TP.TalepNo=STI.Kod1
LEFT JOIN FINSAT6{0}.FINSAT6{0}.STK (nolock) ON STK.MalKodu=STI.MalKodu
WHERE STI.KynkEvrakTip={1} AND STI.EvrakNo='{2}' AND STI.Tarih={3} AND STI.Chk='{4}'

SELECT * FROM FINSAT6{0}.FINSAT6{0}.FTD (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND HesapKodu='{4}'

SELECT * FROM FINSAT6{0}.FINSAT6{0}.MFK (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND EvrakTarih={3} AND HesapKod='{4}'
"
                    , sirketKodu
                    , (int)irsTip
                    , evrakNo
                    , tarih
                    , hesapKodu);

                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    
                    KKP_STI sti = new KKP_STI();
                    Data.TabloDoldur(sti, dr);
                    evrak.listSTI.Add(sti);

                    sti.PropertyChanged += sti.STI_PropertyChanged;
                }

                dr.NextResult();
                while (dr.Read())
                {
                    KKP_FTD ftd = new KKP_FTD();
                    Data.TabloDoldur(ftd, dr);
                    evrak.FTDList.Add(ftd);

                    ftd.PropertyChanged += ftd.FTD_PropertyChanged;
                }

                dr.NextResult();
                while (dr.Read())
                {
                    KKP_MFK mfk = new KKP_MFK();
                    Data.TabloDoldur(mfk, dr);
                    evrak.SetMfk(mfk);

                    mfk.PropertyChanged += mfk.MFK_PropertyChanged;
                }

                dr.Dispose();

                IrsaliyeSiparisIliskisi(con, evrak);

            }

            if (evrak.listSTI.Count > 0)
            {
                KKP_STI ilkSatir = evrak.listSTI[0];
                evrak.TeslimYeriKodu = ilkSatir.TeslimChk;
                //evrak.TahTeslimTarihi = new DateTime(1900, 1, 1);
                evrak.IslemTip = (KKPIslemTipSPI)ilkSatir.IslemTip;
                //evrak.SiparisDurumu = KKPSiparisDurumu.Açık;

                evrak.dvzTL = (KKPDvzTL)ilkSatir.DvzTL;
                evrak.DovizCinsi = ilkSatir.DovizCinsi;
                evrak.DovizKuru = ilkSatir.DovizKuru;


                evrak.GuncellemeModu = true;
                evrak.GuncellenecekEvrak.EvrakNo = ilkSatir.EvrakNo;
                evrak.GuncellenecekEvrak.HesapKodu = ilkSatir.Chk;
                evrak.GuncellenecekEvrak.Tarih = ilkSatir.Tarih;
                evrak.GuncellenecekEvrak.KynkEvrakTip = (short)irsTip;
            }
           


            evrak.fromLoad = false;
            return evrak;
        }
        
        private KKPEvrakIrsaliye IrsaliyeSilinecekEvrakBilgi(KKPIrsaliyeTip irsTip, string evrakNo, int tarih, string hesapKodu)
        {
            KKPEvrakIrsaliye evrak = new KKPEvrakIrsaliye(irsTip);
            evrak.fromLoad = true;
            evrak.EvrakNo = evrakNo;
            evrak.Tarih = new DateTime(1900, 1, 1).AddDays(tarih - 2);
            evrak.HesapKodu = hesapKodu;
            using (KKPCon con = new KKPCon(ConStr))
            {
                con.CommandText = string.Format(
@"SELECT IslemTur, EvrakNo, Tarih, Chk, SiraNo, MalKodu, KynkEvrakTip, Miktar, BirimMiktar, Depo, Birim
, Kaydeden, KayitTarih, KayitSaat, KayitKaynak, KayitSurum, ROW_ID
, KaynakSiparisNo, KaynakSiparisTarih, KaynakSiraNo, SiparisSiraNo 
FROM FINSAT6{0}.FINSAT6{0}.STI (nolock) 
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND Chk='{4}'"
                    , SirketKodu
                    , (int)irsTip
                    , evrakNo
                    , tarih
                    , hesapKodu);

                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    KKP_STI sti = new KKP_STI();
                    Data.TabloDoldur(sti, dr);
                    evrak.listSTI.Add(sti);
                }
                dr.Dispose();

                IrsaliyeSiparisIliskisi(con, evrak);
                /*
//                List<KKP_STI> mylist = evrak.listSTI.FindAll (t => t.KaynakSiparisNo.Trim() != "");

//                if (mylist.Count > 0)
//                {
//                    List<string> sipevraklist = mylist.Select(t => t.KaynakSiparisNo).Distinct().ToList();
//                    List<int> tarihlist = mylist.Select(t => t.KaynakSiparisTarih).Distinct().ToList();
//                    List<int> siralist = mylist.Select(t => Convert.ToInt32(t.KaynakSiraNo)).Distinct().ToList();
//                    List<string> mallist = mylist.Select(t => t.MalKodu).Distinct().ToList();

//                    con.CommandText = string.Format(@"SELECT IslemTur, EvrakNo, Tarih, Chk, SiraNo, MalKodu
//, KynkEvrakTip, Miktar, Birim, BirimMiktar, TeslimMiktar, KapatilanMiktar 
//FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock)
//WHERE KynkEvrakTip={1} AND EvrakNo IN ({2}) AND Tarih IN ({3}) AND Chk='{4}' AND MalKodu IN ({5}) AND SiraNo IN ({6})"
//                        , SirketKodu
//                        , irsTip == KKPIrsaliyeTip.AlimIrsaliyesi ? (short)KKPSiparisTip.AlimSiparisi : (short)KKPSiparisTip.SatisSiparisi
//                        , MyHelper.ArrayForSql(sipevraklist)
//                        , MyHelper.ArrayForSql(tarihlist)
//                        , hesapKodu
//                        , MyHelper.ArrayForSql(mallist)
//                        , MyHelper.ArrayForSql(siralist));


//                    dr = con.ExecuteReader();
//                    while (dr.Read())
//                    {
//                        KKP_SPI spi = new KKP_SPI(irsTip == KKPIrsaliyeTip.AlimIrsaliyesi ? KKPSiparisTip.AlimSiparisi : KKPSiparisTip.SatisSiparisi);
//                        Data.TabloDoldur(spi, dr);

//                        KKP_STI sti = evrak.listSTI.Find(t => t.KaynakSiparisNo == spi.EvrakNo && t.KaynakSiparisTarih == spi.Tarih && t.KaynakSiraNo == spi.SiraNo
//                            && t.Chk == spi.Chk && t.MalKodu == spi.MalKodu);

//                        sti.SPIInfo = spi;
//                    }
//                }
                */


            }
            evrak.fromLoad = false;
            return evrak;
        }

        void IrsaliyeSiparisIliskisi(KKPCon con, KKPEvrakIrsaliye evrak)
        {
            List<KKP_STI> mylist = evrak.listSTI.FindAll(t => t.KaynakSiparisNo.Trim() != "");

            if (mylist.Count > 0)
            {
                List<string> sipevraklist = mylist.Select(t => t.KaynakSiparisNo).Distinct().ToList();
                List<int> tarihlist = mylist.Select(t => t.KaynakSiparisTarih).Distinct().ToList();
                List<int> siralist = mylist.Select(t => Convert.ToInt32(t.SiparisSiraNo)).Distinct().ToList();
                List<string> mallist = mylist.Select(t => t.MalKodu).Distinct().ToList();
                KKPIrsaliyeTip irsTip = evrak.IrsaliyeTip;

                con.CommandText = string.Format(@"SELECT IslemTur, EvrakNo, Tarih, Chk, SiraNo, MalKodu
, KynkEvrakTip, IslemTip, Miktar, Birim, BirimMiktar, TeslimMiktar, KapatilanMiktar, Depo
FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo IN ({2}) AND Tarih IN ({3}) AND Chk='{4}' AND MalKodu IN ({5}) AND SiraNo IN ({6})"
                    , SirketKodu
                    , irsTip == KKPIrsaliyeTip.AlimIrsaliyesi ? (short)KKPSiparisTip.AlimSiparisi : (short)KKPSiparisTip.SatisSiparisi
                    , MyHelper.ArrayForSql(sipevraklist)
                    , MyHelper.ArrayForSql(tarihlist)
                    , evrak.HesapKodu
                    , MyHelper.ArrayForSql(mallist)
                    , MyHelper.ArrayForSql(siralist));


                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    KKP_SPI spi = new KKP_SPI(irsTip == KKPIrsaliyeTip.AlimIrsaliyesi ? KKPSiparisTip.AlimSiparisi : KKPSiparisTip.SatisSiparisi);
                    Data.TabloDoldur(spi, dr);

                    KKP_STI sti = evrak.listSTI.Find(t => t.KaynakSiparisNo == spi.EvrakNo && t.KaynakSiparisTarih == spi.Tarih && t.KaynakSiraNo == spi.SiraNo
                        && t.Chk == spi.Chk && t.MalKodu == spi.MalKodu);

                    if (sti != null)
                    {
                        sti.SPIInfo = spi;
                        evrak.IrsSatiriOlustur(sti);
                    }

                   
                }
            }
        }

        List<KKPSorguSonuc> IrsaliyeSil(KKPIrsaliyeTip irsTip, string evrakNo, int tarih, string hesapKodu)
        {
            Takvim.IzinFINSAT(tarih, false, TakvimeBagliKisit.MesajTip.ThrowException);

            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();
            KKPEvrakIrsaliye silevrak = IrsaliyeSilinecekEvrakBilgi(irsTip, evrakNo, tarih, hesapKodu);
            foreach (KKP_STI sti in silevrak.listSTI)
            {
                if (sti.SPIInfo != null)                
                    list.Add(SPIUpdate(sti, true));                
            }

            KKPSorguSonuc sonucSTK = STKUpdateFROMSTI(silevrak.listSTI, true);
            List<KKPSorguSonuc> sonucDSTList = DSTUpdateFROMSTI(silevrak.listSTI, true);

            KKPSorguSonuc sonucDelete = new KKPSorguSonuc() { TableName = "STI" };

            sonucDelete.Sorgu = string.Format(
@"DELETE FROM FINSAT6{0}.FINSAT6{0}.STI WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND Chk='{4}'
DELETE FROM FINSAT6{0}.FINSAT6{0}.FTD WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND HesapKodu='{4}'
DELETE FROM FINSAT6{0}.FINSAT6{0}.MFK WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND EvrakTarih={3} AND HesapKod='{4}'
DELETE FROM FINSAT6{0}.FINSAT6{0}.IRS WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3} AND Chk='{4}'"
            , SirketKodu
            , (int)irsTip
            , evrakNo
            , tarih
            , hesapKodu);

            
            list.Add(sonucSTK);
            list.AddRange(sonucDSTList);
            list.Add(sonucDelete);

            return list;

        }

        List<KKPSorguSonuc> IrsaliyeKayit(KKPEvrakIrsaliye evrak)
        {
            Takvim.IzinFINSAT(evrak.Tarih, !evrak.GuncellemeModu, TakvimeBagliKisit.MesajTip.ThrowException);

            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();
            KKPEvrakIrsaliye silevrak = null;
            if (evrak.GuncellemeModu)
            {
                silevrak = IrsaliyeSilinecekEvrakBilgi(evrak.IrsaliyeTip, evrak.GuncellenecekEvrak.EvrakNo, evrak.GuncellenecekEvrak.Tarih, evrak.GuncellenecekEvrak.HesapKodu);
                list.AddRange(IrsaliyeSil(evrak.IrsaliyeTip, evrak.GuncellenecekEvrak.EvrakNo, evrak.GuncellenecekEvrak.Tarih, evrak.GuncellenecekEvrak.HesapKodu));
            }

            list.Add(STKUpdateFROMSTI(evrak.listSTI, false));
            list.AddRange(DSTUpdateFROMSTI(evrak.listSTI, false));

            foreach (KKP_STI sti in evrak.listSTI)
            {
                bool identity_insertON = false;
                if (silevrak != null)
                {
                    KKP_STI silsatir = silevrak.listSTI.Find(t => t.SiraNo == sti.SiraNo && t.MalKodu == sti.MalKodu);
                    if (silsatir != null)
                    {
                        sti.Kaydeden = silsatir.Kaydeden;
                        sti.KayitTarih = silsatir.KayitTarih;
                        sti.KayitSaat = silsatir.KayitSaat;
                        sti.KayitKaynak = silsatir.KayitKaynak;
                        sti.KayitSurum = silsatir.KayitSurum;

                        sti.ROW_ID = silsatir.ROW_ID;
                        identity_insertON = true;
                    }
                }

                list.Add(Data.GetSqlInsert(sti, "STI", SirketKodu, identity_insertON));
                if (sti.IRSInfo != null)
                {
                    sti.IRSInfo.Miktar = sti.BirimMiktar;
                    sti.IRSInfo.SiraNo = sti.SiraNo;
                    list.Add(Data.GetSqlInsert(sti.IRSInfo, "IRS", SirketKodu, false));
                    list.Add(SPIUpdate(sti, false));
                }
            }

            foreach (KKP_FTD ftd in evrak.FTDList)
            {
                list.Add(Data.GetSqlInsert(ftd, "FTD", SirketKodu, false));
            }
            list.Add(Data.GetSqlInsert(evrak.MFK, "MFK", SirketKodu, false));

            ///Güncelleme modunda evrak no INI güncellenemez
            if (evrak.GuncellemeModu == false && evrak.INIGuncellensin)
            {
                string yenievrakNo = MyHelper.EvrakNoArtir(evrak.EvrakNo);

                list.Add(INIUpdate(evrak.KynkEvrTip, yenievrakNo, evrak.EvrakSeri));
            }


            return list;
        }

        #endregion

        #region Muhasebe ile ilgili fonksiyonlar
        
        List<KKPSorguSonuc> MuhFisKayit(KKPEvrakMuhFis fis)
        {
            Takvim.IzinMUHASEBE(fis.Tarih, !fis.GuncellemeModu, TakvimeBagliKisit.MesajTip.ThrowException);

            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();

            foreach (var item in fis.MHIList)
            {
                list.Add(Data.GetSqlInsert(item, "MHI", SirketKodu, false));
            }
            foreach (var item in fis.ENTList)
            {
                list.Add(Data.GetSqlInsert(item, "ENT", SirketKodu, false));
            }
            return list;

        }
        #endregion


        #region ORTAK SORGULAR

        KKPSorguSonuc STKUpdateFROMSPI(KKP_SPI spi, bool geriAl)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "STK" };
            if (spi.IslemTur == (short)KKPIslemTur.Giriş)
            {
                if (geriAl)
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok-@Miktar, AlimSiparis=AlimSiparis-@Miktar
WHERE MalKodu=@MalKodu", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok+@Miktar, AlimSiparis=AlimSiparis+@Miktar
WHERE MalKodu=@MalKodu", SirketKodu);
                }
            }
            else
            {
                if (geriAl)
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok+@Miktar, SatisSiparis=SatisSiparis-@Miktar
WHERE MalKodu=@MalKodu ", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok-@Miktar, SatisSiparis=SatisSiparis+@Miktar
WHERE MalKodu=@MalKodu", SirketKodu);
                }
            }

            sonuc.Parameters.Add(new SqlParameter("Miktar", SqlDbType.Decimal) { Value = spi.Miktar });
            sonuc.Parameters.Add(new SqlParameter("MalKodu", SqlDbType.VarChar) { Value = spi.MalKodu });

            return sonuc;
        }

        KKPSorguSonuc STKUpdateFROMSPI(List<KKP_SPI> listSPI, bool geriAl)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "STK" };
            int index = 0;

            foreach (KKP_SPI spi in listSPI)
            {
                index++;
                if (spi.IslemTur == (short)KKPIslemTur.Giriş)
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok-@Miktar{1}, AlimSiparis=AlimSiparis-@Miktar{1}
WHERE MalKodu=@MalKodu{1}", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok+@Miktar{1}, AlimSiparis=AlimSiparis+@Miktar{1}
WHERE MalKodu=@MalKodu{1}", SirketKodu, index);
                    }
                }
                else
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok+@Miktar{1}, SatisSiparis=SatisSiparis-@Miktar{1}
WHERE MalKodu=@MalKodu{1} ", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
TahminiStok=TahminiStok-@Miktar{1}, SatisSiparis=SatisSiparis+@Miktar{1}
WHERE MalKodu=@MalKodu{1}", SirketKodu, index);
                    }
                }

                sonuc.Sorgu += Environment.NewLine;

                sonuc.Parameters.Add(new SqlParameter("Miktar" + index.ToString(), SqlDbType.Decimal) { Value = spi.Miktar });
                sonuc.Parameters.Add(new SqlParameter("MalKodu" + index.ToString(), SqlDbType.VarChar) { Value = spi.MalKodu });
            }

            return sonuc;
        }

        KKPSorguSonuc DSTUpdateFROMSPI(KKP_SPI spi, bool geriAl)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "DST" };
            sonuc.Sorgu = string.Format("IF (SELECT COUNT(Row_ID) FROM FINSAT6{0}.FINSAT6{0}.DST (nolock) WHERE MalKodu=@MalKodu AND Depo=@Depo)=0", SirketKodu);
            sonuc.Sorgu += Environment.NewLine + "    ";

            KKP_DST dst = DSTTable.NewDST();
            Data.DefatulValueSetDST(dst);
            dst.MalKodu = spi.MalKodu;
            dst.Depo = spi.Depo;
            dst.KayitKaynak = (short)(GetKayitKaynak((KKPKynkEvrakTip)spi.KynkEvrakTip));
            dst.DegisKaynak = dst.KayitKaynak;

            KKPSorguSonuc sonucdst = Data.GetSqlInsert(dst, "DST", SirketKodu, false);
            sonuc.Sorgu += sonucdst.Sorgu;
            sonuc.Parameters.AddRange(sonucdst.Parameters);

            sonuc.Sorgu += Environment.NewLine;
            if (spi.IslemTur == (short)KKPIslemTur.Giriş)
            {
                if (geriAl)
                {
                    sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok-@Miktar, AlSiparis=AlSiparis-@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu += string.Format(@"    
UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok+@Miktar, AlSiparis=AlSiparis+@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo", SirketKodu);
                }
            }
            else
            {
                if (geriAl)
                {
                    sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok+@Miktar, SatSiparis=SatSiparis-@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo ", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu += string.Format(@"    
UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok-@Miktar, SatSiparis=SatSiparis+@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo", SirketKodu);
                }
            }

            /* DST kontrolü yapılırken bu parametreler atılmıştır.
           sonuc.Parameters.Add(new SqlParameter("Miktar", SqlDbType.Decimal) { Value = spi.Miktar });
           sonuc.Parameters.Add(new SqlParameter("MalKodu", SqlDbType.VarChar) { Value = spi.MalKodu });
           sonuc.Parameters.Add(new SqlParameter("Depo", SqlDbType.VarChar) { Value = spi.Depo });
           */

            return sonuc;
        }

        List<KKPSorguSonuc> DSTUpdateFROMSPI(List<KKP_SPI> listSPI, bool geriAl)
        {
            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();

            foreach (var grup in listSPI.GroupBy(t => new { t.MalKodu, t.Depo }))
            {
                KKPSorguSonuc dstCreate = new KKPSorguSonuc() { TableName = "DST" };
                dstCreate.Sorgu = string.Format("IF (SELECT COUNT(Row_ID) FROM FINSAT6{0}.FINSAT6{0}.DST (nolock) WHERE MalKodu=@MalKodu AND Depo=@Depo)=0", SirketKodu);
                dstCreate.Sorgu += Environment.NewLine + "    ";
                KKP_DST dst = DSTTable.NewDST();
                Data.DefatulValueSetDST(dst);
                dst.MalKodu = grup.Key.MalKodu;
                dst.Depo = grup.Key.Depo;
                dst.KayitKaynak = (short)GetKayitKaynak((KKPKynkEvrakTip)(grup.First().KynkEvrakTip));
                dst.DegisKaynak = dst.KayitKaynak;

                KKPSorguSonuc sonucdst = Data.GetSqlInsert(dst, "DST", SirketKodu, false);
                dstCreate.Sorgu += sonucdst.Sorgu;
                dstCreate.Parameters.AddRange(sonucdst.Parameters);
                list.Add(dstCreate);
            }

            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "DST" };
            int index = 0;
            foreach (KKP_SPI spi in listSPI)
            {
                index++;
                if (spi.IslemTur == (short)KKPIslemTur.Giriş)
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok-@Miktar{1}, AlSiparis=AlSiparis-@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1}", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu += string.Format(@"    
UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok+@Miktar{1}, AlSiparis=AlSiparis+@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1}", SirketKodu, index);
                    }
                }
                else
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok+@Miktar{1}, SatSiparis=SatSiparis-@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1} ", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu += string.Format(@"    
UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
TahminiStok=TahminiStok-@Miktar{1}, SatSiparis=SatSiparis+@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1}", SirketKodu, index);
                    }
                }
                sonuc.Parameters.Add(new SqlParameter("Miktar" + index.ToString(), SqlDbType.Decimal) { Value = spi.Miktar });
                sonuc.Parameters.Add(new SqlParameter("MalKodu" + index.ToString(), SqlDbType.VarChar) { Value = spi.MalKodu });
                sonuc.Parameters.Add(new SqlParameter("Depo" + index.ToString(), SqlDbType.VarChar) { Value = spi.Depo });
            }

            list.Add(sonuc);
            return list;
        }


        KKPSorguSonuc STKUpdateFROMSTI(KKP_STI sti, bool geriAl)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "STK" };
            if (sti.IslemTur == (short)KKPIslemTur.Giriş)
            {
                if (geriAl)
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
GirMiktar=GirMiktar-@Miktar, TahminiStok=TahminiStok-@Miktar
WHERE MalKodu=@MalKodu", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
GirMiktar=GirMiktar+@Miktar, TahminiStok=TahminiStok+@Miktar
WHERE MalKodu=@MalKodu", SirketKodu);
                }
            }
            else 
            {
                if (geriAl)
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
CikMiktar=CikMiktar-@Miktar, TahminiStok=TahminiStok+@Miktar
WHERE MalKodu=@MalKodu", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
CikMiktar=CikMiktar+@Miktar, TahminiStok=TahminiStok-@Miktar
WHERE MalKodu=@MalKodu", SirketKodu);
                }
            }
            sonuc.Parameters.Add(new SqlParameter("Miktar", SqlDbType.Decimal) { Value = sti.Miktar });
            sonuc.Parameters.Add(new SqlParameter("MalKodu", SqlDbType.VarChar) { Value = sti.MalKodu });

            return sonuc;
        }

        KKPSorguSonuc STKUpdateFROMSTI(List<KKP_STI> listSTI, bool geriAl)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "STK" };
            int index = 0;

            foreach (KKP_STI sti in listSTI)
            {
                index++;
                if (sti.IslemTur == (short)KKPIslemTur.Giriş)
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
GirMiktar=GirMiktar-@Miktar{1}, TahminiStok=TahminiStok-@Miktar{1}
WHERE MalKodu=@MalKodu{1}", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
GirMiktar=GirMiktar+@Miktar{1}, TahminiStok=TahminiStok+@Miktar{1}
WHERE MalKodu=@MalKodu{1}", SirketKodu, index);
                    }
                }
                else
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
CikMiktar=CikMiktar-@Miktar{1}, TahminiStok=TahminiStok+@Miktar{1}
WHERE MalKodu=@MalKodu{1}", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.STK SET
CikMiktar=CikMiktar+@Miktar{1}, TahminiStok=TahminiStok-@Miktar{1}
WHERE MalKodu=@MalKodu{1}", SirketKodu, index);
                    }
                }

                sonuc.Sorgu += Environment.NewLine;

                sonuc.Parameters.Add(new SqlParameter("Miktar" + index.ToString(), SqlDbType.Decimal) { Value = sti.Miktar });
                sonuc.Parameters.Add(new SqlParameter("MalKodu" + index.ToString(), SqlDbType.VarChar) { Value = sti.MalKodu });
            }

            return sonuc;

        }

        KKPSorguSonuc DSTUpdateFROMSTI(KKP_STI sti, bool geriAl)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "DST" };
            sonuc.Sorgu = string.Format("IF (SELECT COUNT(Row_ID) FROM FINSAT6{0}.FINSAT6{0}.DST (nolock) WHERE MalKodu=@MalKodu AND Depo=@Depo)=0", SirketKodu);
            sonuc.Sorgu += Environment.NewLine + "    ";

            KKP_DST dst = DSTTable.NewDST();
            Data.DefatulValueSetDST(dst);
            dst.MalKodu = sti.MalKodu;
            dst.Depo = sti.Depo;
            dst.KayitKaynak = (short)(GetKayitKaynak((KKPKynkEvrakTip)sti.KynkEvrakTip));
            dst.DegisKaynak = dst.KayitKaynak;

            KKPSorguSonuc sonucdst = Data.GetSqlInsert(dst, "DST", SirketKodu, false);
            sonuc.Sorgu += sonucdst.Sorgu;
            sonuc.Parameters.AddRange(sonucdst.Parameters);

            sonuc.Sorgu += Environment.NewLine;

            if (sti.IslemTur == (short)KKPIslemTur.Giriş)
            {
                if (geriAl)
                {
                    sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
GirMiktar=GirMiktar-@Miktar, TahminiStok=TahminiStok-@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo ", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
GirMiktar=GirMiktar+@Miktar, TahminiStok=TahminiStok+@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo ", SirketKodu);
                }
            }
            else
            {
                if (geriAl)
                {
                    sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
CikMiktar=CikMiktar-@Miktar, TahminiStok=TahminiStok+@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo ", SirketKodu);
                }
                else
                {
                    sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
CikMiktar=CikMiktar+@Miktar, TahminiStok=TahminiStok-@Miktar
WHERE MalKodu=@MalKodu and Depo=@Depo ", SirketKodu);
                }
            }

            /* DST kontrolü yapılırken bu parametreler atılmıştır.
            sonuc.Parameters.Add(new SqlParameter("Miktar", SqlDbType.Decimal) { Value = sti.Miktar });
            sonuc.Parameters.Add(new SqlParameter("MalKodu", SqlDbType.VarChar) { Value = sti.MalKodu });
            sonuc.Parameters.Add(new SqlParameter("Depo", SqlDbType.VarChar) { Value = sti.Depo });
            */

            return sonuc;
        }

        List<KKPSorguSonuc> DSTUpdateFROMSTI(List<KKP_STI> listSTI, bool geriAl)
        {
            List<KKPSorguSonuc> list = new List<KKPSorguSonuc>();
            
            foreach (var grup in listSTI.GroupBy(t => new { t.MalKodu, t.Depo }))
            {
                KKPSorguSonuc dstCreate = new KKPSorguSonuc() { TableName = "DST" };
                dstCreate.Sorgu = string.Format("IF (SELECT COUNT(Row_ID) FROM FINSAT6{0}.FINSAT6{0}.DST (nolock) WHERE MalKodu=@MalKodu AND Depo=@Depo)=0", SirketKodu);
                dstCreate.Sorgu += Environment.NewLine + "    ";
                KKP_DST dst = DSTTable.NewDST();
                Data.DefatulValueSetDST(dst);
                dst.MalKodu = grup.Key.MalKodu;
                dst.Depo = grup.Key.Depo;
                dst.KayitKaynak = (short)GetKayitKaynak((KKPKynkEvrakTip)(grup.First().KynkEvrakTip));
                dst.DegisKaynak = dst.KayitKaynak;

                KKPSorguSonuc sonucdst = Data.GetSqlInsert(dst, "DST", SirketKodu, false);
                dstCreate.Sorgu += sonucdst.Sorgu;
                dstCreate.Parameters.AddRange(sonucdst.Parameters);
                list.Add(dstCreate);
            }


            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "DST" };
            int index = 0;
            foreach (KKP_STI sti in listSTI)
            {
                index++;
                if (sti.IslemTur == (short)KKPIslemTur.Giriş)
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
GirMiktar=GirMiktar-@Miktar{1}, TahminiStok=TahminiStok-@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1} ", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
GirMiktar=GirMiktar+@Miktar{1}, TahminiStok=TahminiStok+@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1} ", SirketKodu, index);
                    }
                }
                else
                {
                    if (geriAl)
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
CikMiktar=CikMiktar-@Miktar{1}, TahminiStok=TahminiStok+@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1} ", SirketKodu, index);
                    }
                    else
                    {
                        sonuc.Sorgu += string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.DST SET
CikMiktar=CikMiktar+@Miktar{1}, TahminiStok=TahminiStok-@Miktar{1}
WHERE MalKodu=@MalKodu{1} and Depo=@Depo{1} ", SirketKodu, index);
                    }
                }
                sonuc.Parameters.Add(new SqlParameter("Miktar" + index.ToString(), SqlDbType.Decimal) { Value = sti.Miktar });
                sonuc.Parameters.Add(new SqlParameter("MalKodu" + index.ToString(), SqlDbType.VarChar) { Value = sti.MalKodu });
                sonuc.Parameters.Add(new SqlParameter("Depo" + index.ToString(), SqlDbType.VarChar) { Value = sti.Depo });
            }
            list.Add(sonuc);
            return list;
        }

        KKPSorguSonuc INIUpdate(KKPKynkEvrakTip evrakTip, string yeniEvrakNo, int seri)
        {
            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "INI" };

            sonuc.Sorgu = string.Format(@"UPDATE FINSAT6{0}.FINSAT6{0}.INI SET
MyValue='{1}' WHERE MySection=1 AND MyEntry={2}"
                            , SirketKodu
                            , yeniEvrakNo
                            , GetMyEntry(evrakTip) + seri - 1);
            return sonuc;
        }

        KKPSorguSonuc SPIUpdate(KKP_STI sti, bool geriAl)
        {
            if (sti == null || sti.SPIInfo == null)
                throw new ArgumentException("SPI UPDATE Parametreler null");

            KKPSorguSonuc sonuc = new KKPSorguSonuc() { TableName = "SPI UPDATE" };

            if (geriAl)
            {
                sonuc.TableName = "SPI UPDATE (Geri Al)";
                sonuc.Sorgu = string.Format(
@"UPDATE FINSAT6{0}.FINSAT6{0}.SPI SET
IrsaliyeNo='',
IrsaliyeTarih=0,
SonTeslimTarih=0,
TeslimMiktar=TeslimMiktar-{1},
DegisKaynak='{2}',
SiparisDurumu=(CASE WHEN BirimMiktar<=(TeslimMiktar-{3}+KapatilanMiktar) THEN 1 ELSE 0 END),
DegisSaat={3}
WHERE EvrakNo='{4}' AND MalKodu='{5}' AND SiraNo={6} AND KynkEvrakTip={7}"
                    , SirketKodu
                    , sti.BirimMiktar.ToString("F6").Replace(",", ".")
                    , sti.KynkEvrakTip
                    , Data.SaatForDB(DateTime.Now)
                    , sti.SPIInfo.EvrakNo
                    , sti.MalKodu
                    , sti.SPIInfo.SiraNo
                    , sti.SPIInfo.KynkEvrakTip);
            }
            else
            {
                sonuc.Sorgu = string.Format(
    @"UPDATE FINSAT6{0}.FINSAT6{0}.SPI SET
IrsaliyeNo='{1}',
IrsaliyeTarih={2},
SonTeslimTarih={2},
TeslimMiktar=TeslimMiktar+{3},
DegisKaynak='{4}',
SiparisDurumu=(CASE WHEN BirimMiktar<=(TeslimMiktar+{3}+KapatilanMiktar) THEN 1 ELSE 0 END),
DegisSaat={5}
WHERE EvrakNo='{6}' AND MalKodu='{7}' AND SiraNo={8} AND KynkEvrakTip={9}"
                    , SirketKodu
                    , sti.EvrakNo
                    , sti.Tarih
                    , sti.BirimMiktar.ToString("F6").Replace(",", ".")
                    , sti.KynkEvrakTip
                    , Data.SaatForDB(DateTime.Now)
                    , sti.SPIInfo.EvrakNo
                    , sti.MalKodu
                    , sti.SPIInfo.SiraNo
                    , sti.SPIInfo.KynkEvrakTip);
            }

            return sonuc;

        }

        #endregion ORTAK SORGULAR_SON

        KKPKayitKaynak GetKayitKaynak(KKPKynkEvrakTip tip)
        {
            KKPKayitKaynak kaynak = KKPKayitKaynak.AlimSiparisi;
            switch (tip)
            {
                case KKPKynkEvrakTip.Satışİrsaliyesi:
                    kaynak = KKPKayitKaynak.SatisIrsaliye;
                    break;
                case KKPKynkEvrakTip.SatışFaturası:
                    break;
                case KKPKynkEvrakTip.SatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.Alımİrsaliyesi:
                    kaynak = KKPKayitKaynak.AlimIrsaliye;
                    break;
                case KKPKynkEvrakTip.AlımFaturası:
                    break;
                case KKPKynkEvrakTip.AlımdanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.TahsilatMakbuzu:
                    break;
                case KKPKynkEvrakTip.TediyeMakbuzu:
                    break;
                case KKPKynkEvrakTip.AlacakDekontu:
                    break;
                case KKPKynkEvrakTip.BorçDekontu:
                    break;
                case KKPKynkEvrakTip.GelenHavale:
                    break;
                case KKPKynkEvrakTip.GönderilenHavale:
                    break;
                case KKPKynkEvrakTip.VirmanFişi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_SenetAlımBordrosu:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Protesto:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_KasadanTahsilat:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Ciro:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_TahsilataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_BankadanTahsilat:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Ödenmedi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Kayıp:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_İcrayaVerme:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_ÇekAlımBordrosu:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Karşılıksız:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_KasadanTahsilEdilen:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Ciro:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_TahsilataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_BankadanTahsilat:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Ödenmedi:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Kayıp:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_İcrayaVerme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_Ödeme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_KasadanÖdeme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_BankadanÖdeme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_Ödenmedi:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_Protesto:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_Ödeme:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_KasadanÖdeme:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_BankadanÖdeme:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_Karşılıksız:
                    break;
                case KKPKynkEvrakTip.İçKullanımFişi:
                    break;
                case KKPKynkEvrakTip.DepolarArasıTransferFişi:
                    break;
                case KKPKynkEvrakTip.AlınanNumune_HediyeFişi:
                    break;
                case KKPKynkEvrakTip.VerilenNumune_HediyeFişi:
                    break;
                case KKPKynkEvrakTip.FireFişi:
                    break;
                case KKPKynkEvrakTip.SayımFarkıFişi:
                    break;
                case KKPKynkEvrakTip.StokGirişFişi:
                    kaynak = KKPKayitKaynak.StokGirisFisi;
                    break;
                case KKPKynkEvrakTip.StokÇıkışFişi:
                    kaynak = KKPKayitKaynak.StokCikisFisi;
                    break;
                case KKPKynkEvrakTip.KasaTahsilFişi:
                    break;
                case KKPKynkEvrakTip.KasaTediyeFişi:
                    break;
                case KKPKynkEvrakTip.SatışSiparişi:
                    kaynak = KKPKayitKaynak.SatisSiparisi;
                    break;
                case KKPKynkEvrakTip.AlımSiparişi:
                    kaynak = KKPKayitKaynak.AlimSiparisi;
                    break;
                case KKPKynkEvrakTip.PerakendeSatışFaturası:
                    break;
                case KKPKynkEvrakTip.SiparişeGöreFaturalama:
                    break;
                case KKPKynkEvrakTip.AlımRezervasyonu:
                    break;
                case KKPKynkEvrakTip.SatışRezervasyonu:
                    break;
                case KKPKynkEvrakTip.Alımdanİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.Satıştanİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.Diğer:
                    break;
                case KKPKynkEvrakTip.FiyatListesiKartı:
                    break;
                case KKPKynkEvrakTip.MamülAğacıGirişi:
                    break;
                case KKPKynkEvrakTip.ÜretimEmirFişi:
                    break;
                case KKPKynkEvrakTip.Üretimİşlemleri:
                    break;
                case KKPKynkEvrakTip.AlacakÖdemePlanı:
                    break;
                case KKPKynkEvrakTip.BorçÖdemePlanı:
                    break;
                case KKPKynkEvrakTip.Bütçeİşlemleri:
                    break;
                case KKPKynkEvrakTip.BankaBorçDekontu:
                    break;
                case KKPKynkEvrakTip.BankaGelenHavale:
                    break;
                case KKPKynkEvrakTip.BankaAlacakDekontu:
                    break;
                case KKPKynkEvrakTip.BankaGönderilenHavale:
                    break;
                case KKPKynkEvrakTip.KarmaKoli:
                    break;
                case KKPKynkEvrakTip.AçılışFişiİşlemleri:
                    break;
                case KKPKynkEvrakTip.DönemselFişİşlemleri:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatışİşlemleri:
                    break;
                case KKPKynkEvrakTip.BankaTeminatMektubu:
                    break;
                case KKPKynkEvrakTip.SabitKıymetGiderİşlemleri:
                    break;
                case KKPKynkEvrakTip.MuhasebeAçılışFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeKapanışFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeTahsilFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeTediyeFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeMahsupFişi:
                    break;
                case KKPKynkEvrakTip.TeminatMektubu:
                    break;
                case KKPKynkEvrakTip.SayımSonuçFişi:
                    break;
                case KKPKynkEvrakTip.DepoSayımSonuçFişi:
                    break;
                case KKPKynkEvrakTip.CH_Devirİşlemi:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_TransferFişi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_TransferFişi:
                    break;
                case KKPKynkEvrakTip.GiderPusulası:
                    break;
                case KKPKynkEvrakTip.DepoSayımFarkıFişi:
                    break;
                case KKPKynkEvrakTip.ArbitrajFişi:
                    break;
                case KKPKynkEvrakTip.DepolarArasıAltTransferFişi:
                    break;
                case KKPKynkEvrakTip.KonsinyeVerilenMalİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.KonsinyeVerilenMalİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.KonsinyeAlınanMalİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.KonsinyeAlınanMalİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmesineGöreAçılışFişi:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmesineGöreDönemselFişi:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmeFişi:
                    break;
                case KKPKynkEvrakTip.FinansmanGiderFişi:
                    break;
                case KKPKynkEvrakTip.HisseSenetleriTablosu:
                    break;
                case KKPKynkEvrakTip.TarihiDeğerlerTablosu:
                    break;
                case KKPKynkEvrakTip.DönemSonuDüzeltilmişDeğerler:
                    break;
                case KKPKynkEvrakTip.AktifleşmedenÖnceYapılanHarcamalar:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmeTarsİşlemFişi:
                    break;
                case KKPKynkEvrakTip.SerbestMeslekMakbuzu:
                    break;
                case KKPKynkEvrakTip.PerakendeSatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.GiderÖdemeFişi:
                    break;
                case KKPKynkEvrakTip.BankaGiderÖdemeFişi:
                    break;
                case KKPKynkEvrakTip.PerakendeAlımFaturası:
                    break;
                case KKPKynkEvrakTip.GiderFaturası:
                    break;
                case KKPKynkEvrakTip.TeklifİstemeFişi:
                    break;
                case KKPKynkEvrakTip.SatışTeklifi:
                    break;
                case KKPKynkEvrakTip.SatınAlmaTeklifi:
                    break;
                case KKPKynkEvrakTip.HizmetSatışFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetSatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetAlımFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetAlımdanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetSatışSiparişi:
                    break;
                case KKPKynkEvrakTip.HizmetAlımSiparişi:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatışFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetAlımFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetAlımdanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatışSiparişi:
                    break;
                case KKPKynkEvrakTip.SabitKıymetAlımSiparişi:
                    break;
                case KKPKynkEvrakTip.MalzemeTalepFormu:
                    break;
                case KKPKynkEvrakTip.ÖnSatışTeklifi:
                    break;
                case KKPKynkEvrakTip.MasrafFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeSonuçAktarımFişi:
                    break;
                case KKPKynkEvrakTip.SatışlarİçinAlımFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.AlımlarİçinKesilenFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.SatışlarİçinKesilenFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.AlımFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.CariDevirFişi:
                    break;
                case KKPKynkEvrakTip.BankaTeminatMektubuİptal_İade:
                    break;
                case KKPKynkEvrakTip.MalzemeTeslimFişi:
                    break;
                default:
                    break;
            }
            return kaynak;
        }

        int GetMyEntry(KKPKynkEvrakTip evrakTip)
        {
            int sonuc = 0;
            switch (evrakTip)
            {
                case KKPKynkEvrakTip.Satışİrsaliyesi:
                    sonuc = 2100;
                    break;
                case KKPKynkEvrakTip.SatışFaturası:                    
                    break;
                case KKPKynkEvrakTip.SatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.Alımİrsaliyesi:
                    sonuc = 2000;
                    break;
                case KKPKynkEvrakTip.AlımFaturası:
                    break;
                case KKPKynkEvrakTip.AlımdanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.TahsilatMakbuzu:
                    break;
                case KKPKynkEvrakTip.TediyeMakbuzu:
                    break;
                case KKPKynkEvrakTip.AlacakDekontu:
                    break;
                case KKPKynkEvrakTip.BorçDekontu:
                    break;
                case KKPKynkEvrakTip.GelenHavale:
                    break;
                case KKPKynkEvrakTip.GönderilenHavale:
                    break;
                case KKPKynkEvrakTip.VirmanFişi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_SenetAlımBordrosu:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Protesto:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_KasadanTahsilat:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Ciro:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_TahsilataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_BankadanTahsilat:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Ödenmedi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_Kayıp:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_İcrayaVerme:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_ÇekAlımBordrosu:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Karşılıksız:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_KasadanTahsilEdilen:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Ciro:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_TahsilataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_BankadanTahsilat:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Ödenmedi:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_Kayıp:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_İcrayaVerme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_Ödeme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_KasadanÖdeme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_BankadanÖdeme:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_Ödenmedi:
                    break;
                case KKPKynkEvrakTip.BorçSenetleri_Protesto:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_Ödeme:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_İadeAlındı:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_İadeEdildi:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_KasadanÖdeme:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_TeminataÇıkış:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_BankadanÖdeme:
                    break;
                case KKPKynkEvrakTip.VerilenÇekler_Karşılıksız:
                    break;
                case KKPKynkEvrakTip.İçKullanımFişi:
                    break;
                case KKPKynkEvrakTip.DepolarArasıTransferFişi:
                    break;
                case KKPKynkEvrakTip.AlınanNumune_HediyeFişi:
                    break;
                case KKPKynkEvrakTip.VerilenNumune_HediyeFişi:
                    break;
                case KKPKynkEvrakTip.FireFişi:
                    break;
                case KKPKynkEvrakTip.SayımFarkıFişi:
                    break;
                case KKPKynkEvrakTip.StokGirişFişi:
                    sonuc = 3000;
                    break;
                case KKPKynkEvrakTip.StokÇıkışFişi:
                    sonuc = 3100;
                    break;
                case KKPKynkEvrakTip.KasaTahsilFişi:
                    break;
                case KKPKynkEvrakTip.KasaTediyeFişi:
                    break;
                case KKPKynkEvrakTip.SatışSiparişi:
                    sonuc = 7200;
                    break;
                case KKPKynkEvrakTip.AlımSiparişi:
                    sonuc = 3800;
                    break;
                case KKPKynkEvrakTip.PerakendeSatışFaturası:
                    break;
                case KKPKynkEvrakTip.SiparişeGöreFaturalama:
                    break;
                case KKPKynkEvrakTip.AlımRezervasyonu:
                    break;
                case KKPKynkEvrakTip.SatışRezervasyonu:
                    break;
                case KKPKynkEvrakTip.Alımdanİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.Satıştanİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.Diğer:
                    break;
                case KKPKynkEvrakTip.FiyatListesiKartı:
                    break;
                case KKPKynkEvrakTip.MamülAğacıGirişi:
                    break;
                case KKPKynkEvrakTip.ÜretimEmirFişi:
                    break;
                case KKPKynkEvrakTip.Üretimİşlemleri:
                    break;
                case KKPKynkEvrakTip.AlacakÖdemePlanı:
                    break;
                case KKPKynkEvrakTip.BorçÖdemePlanı:
                    break;
                case KKPKynkEvrakTip.Bütçeİşlemleri:
                    break;
                case KKPKynkEvrakTip.BankaBorçDekontu:
                    break;
                case KKPKynkEvrakTip.BankaGelenHavale:
                    break;
                case KKPKynkEvrakTip.BankaAlacakDekontu:
                    break;
                case KKPKynkEvrakTip.BankaGönderilenHavale:
                    break;
                case KKPKynkEvrakTip.KarmaKoli:
                    break;
                case KKPKynkEvrakTip.AçılışFişiİşlemleri:
                    break;
                case KKPKynkEvrakTip.DönemselFişİşlemleri:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatışİşlemleri:
                    break;
                case KKPKynkEvrakTip.BankaTeminatMektubu:
                    break;
                case KKPKynkEvrakTip.SabitKıymetGiderİşlemleri:
                    break;
                case KKPKynkEvrakTip.MuhasebeAçılışFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeKapanışFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeTahsilFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeTediyeFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeMahsupFişi:
                    break;
                case KKPKynkEvrakTip.TeminatMektubu:
                    break;
                case KKPKynkEvrakTip.SayımSonuçFişi:
                    break;
                case KKPKynkEvrakTip.DepoSayımSonuçFişi:
                    break;
                case KKPKynkEvrakTip.CH_Devirİşlemi:
                    break;
                case KKPKynkEvrakTip.AlınanÇekler_TransferFişi:
                    break;
                case KKPKynkEvrakTip.AlacakSenetleri_TransferFişi:
                    break;
                case KKPKynkEvrakTip.GiderPusulası:
                    break;
                case KKPKynkEvrakTip.DepoSayımFarkıFişi:
                    break;
                case KKPKynkEvrakTip.ArbitrajFişi:
                    break;
                case KKPKynkEvrakTip.DepolarArasıAltTransferFişi:
                    break;
                case KKPKynkEvrakTip.KonsinyeVerilenMalİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.KonsinyeVerilenMalİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.KonsinyeAlınanMalİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.KonsinyeAlınanMalİadeİrsaliyesi:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmesineGöreAçılışFişi:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmesineGöreDönemselFişi:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmeFişi:
                    break;
                case KKPKynkEvrakTip.FinansmanGiderFişi:
                    break;
                case KKPKynkEvrakTip.HisseSenetleriTablosu:
                    break;
                case KKPKynkEvrakTip.TarihiDeğerlerTablosu:
                    break;
                case KKPKynkEvrakTip.DönemSonuDüzeltilmişDeğerler:
                    break;
                case KKPKynkEvrakTip.AktifleşmedenÖnceYapılanHarcamalar:
                    break;
                case KKPKynkEvrakTip.EnflasyonDüzeltmeTarsİşlemFişi:
                    break;
                case KKPKynkEvrakTip.SerbestMeslekMakbuzu:
                    break;
                case KKPKynkEvrakTip.PerakendeSatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.GiderÖdemeFişi:
                    break;
                case KKPKynkEvrakTip.BankaGiderÖdemeFişi:
                    break;
                case KKPKynkEvrakTip.PerakendeAlımFaturası:
                    break;
                case KKPKynkEvrakTip.GiderFaturası:
                    break;
                case KKPKynkEvrakTip.TeklifİstemeFişi:
                    break;
                case KKPKynkEvrakTip.SatışTeklifi:
                    break;
                case KKPKynkEvrakTip.SatınAlmaTeklifi:
                    break;
                case KKPKynkEvrakTip.HizmetSatışFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetSatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetAlımFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetAlımdanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.HizmetSatışSiparişi:
                    break;
                case KKPKynkEvrakTip.HizmetAlımSiparişi:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatışFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatıştanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetAlımFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetAlımdanİadeFaturası:
                    break;
                case KKPKynkEvrakTip.SabitKıymetSatışSiparişi:
                    break;
                case KKPKynkEvrakTip.SabitKıymetAlımSiparişi:
                    break;
                case KKPKynkEvrakTip.MalzemeTalepFormu:
                    break;
                case KKPKynkEvrakTip.ÖnSatışTeklifi:
                    break;
                case KKPKynkEvrakTip.MasrafFişi:
                    break;
                case KKPKynkEvrakTip.MuhasebeSonuçAktarımFişi:
                    break;
                case KKPKynkEvrakTip.SatışlarİçinAlımFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.AlımlarİçinKesilenFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.SatışlarİçinKesilenFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.AlımFiyatFarkıFaturası:
                    break;
                case KKPKynkEvrakTip.CariDevirFişi:
                    break;
                case KKPKynkEvrakTip.BankaTeminatMektubuİptal_İade:
                    break;
                case KKPKynkEvrakTip.MalzemeTeslimFişi:
                    break;
                default:
                    break;
            }
            return sonuc;
        }

        public string YeniEvrakNo(KKPKynkEvrakTip evrakTip, int seri)
        {
            string evrakNo = "";

            using (KKPCon kkpcon = new KKPCon(ConStr))
            {
                kkpcon.CommandText = string.Format(
@"SELECT MyValue FROM FINSAT6{0}.FINSAT6{0}.INI (nolock)
WHERE MySection=1 AND MyEntry={1}"
, SirketKodu
, GetMyEntry(evrakTip) + seri - 1);

                object obj = kkpcon.ExecuteScalar();
                if (obj != null && obj != DBNull.Value)
                    evrakNo = obj.ToString();
            }

            return evrakNo;
        }

        public bool EvrakMevcut(KKPKynkEvrakTip evrakTip, string evrakNo, string chk, int tarih)
        {
            bool sonuc = false;
            
            using (KKPCon kkpcon = new KKPCon(ConStr))
            {
                int count = 0;
                if (evrakTip == KKPKynkEvrakTip.SatışSiparişi || evrakTip == KKPKynkEvrakTip.AlımSiparişi)
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Chk='{3}' AND Tarih={4}"
                            , SirketKodu
                            , (short)evrakTip
                            , evrakNo
                            , chk
                            , tarih);
                }
                else
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Chk='{3}' AND Tarih={4}"
                            , SirketKodu
                            , (short)evrakTip
                            , evrakNo
                            , chk
                            , tarih);
                }

                count = (int)kkpcon.ExecuteScalar();

                sonuc = count > 0;
            }

            return sonuc;
        }

        public bool EvrakMevcut(KKPKynkEvrakTip evrakTip, string evrakNo, int tarih)
        {
            bool sonuc = false;

            using (KKPCon kkpcon = new KKPCon(ConStr))
            {
                int count = 0;
                if (evrakTip == KKPKynkEvrakTip.SatışSiparişi || evrakTip == KKPKynkEvrakTip.AlımSiparişi)
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3}"
                            , SirketKodu
                            , (short)evrakTip
                            , evrakNo
                            , tarih);
                }
                else
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Tarih={3}"
                            , SirketKodu
                            , (short)evrakTip
                            , evrakNo
                            , tarih);
                }

                count = (int)kkpcon.ExecuteScalar();

                sonuc = count > 0;
            }

            return sonuc;
        }


        public bool EvrakMevcut(KKPKynkEvrakTip evrakTip, string evrakNo, string chk)
        {
            bool sonuc = false;

            using (KKPCon kkpcon = new KKPCon(ConStr))
            {
                int count = 0;
                if (evrakTip == KKPKynkEvrakTip.SatışSiparişi || evrakTip == KKPKynkEvrakTip.AlımSiparişi)
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Chk='{3}' "
                            , SirketKodu
                            , (short)evrakTip
                            , evrakNo
                            , chk);
                }
                else
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND Chk='{3}' "
                            , SirketKodu
                            , (short)evrakTip
                            , evrakNo
                            , chk);
                }

                count = (int)kkpcon.ExecuteScalar();

                sonuc = count > 0;
            }

            return sonuc;
        }

        public bool EvrakMevcut(string sirketKodu, KKPKynkEvrakTip evrakTip, string evrakNo)
        {
            bool sonuc = false;

            using (KKPCon kkpcon = new KKPCon(ConStr))
            {
                int count = 0;
                if (evrakTip == KKPKynkEvrakTip.SatışSiparişi || evrakTip == KKPKynkEvrakTip.AlımSiparişi)
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.SPI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' "
                            , sirketKodu
                            , (short)evrakTip
                            , evrakNo);
                }
                else
                {
                    kkpcon.CommandText = string.Format(
@"SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.STI (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' "
                            , sirketKodu
                            , (short)evrakTip
                            , evrakNo);
                }

                count = (int)kkpcon.ExecuteScalar();

                sonuc = count > 0;
            }

            return sonuc;
        }

        public int YeniMuhFisNo()
        {
            int fisNo = 1;
            using (KKPCon kkpcon = new KKPCon(ConStr))
            {
                kkpcon.CommandText = string.Format(@"SELECT MAX(FisNo) From MUHASEBE6{0}.MUHASEBE6{0}.MHI(NOLOCK)", SirketKodu);
                object sonuc = kkpcon.ExecuteScalar();
                if (sonuc != null && sonuc != DBNull.Value)
                    fisNo = Convert.ToInt32(sonuc) + 1;
            }
            return fisNo;
        }


        public KKP_MFK GetMFK(KKPKynkEvrakTip kynkEvrakTip, string hesapKod, string evrakNo, int evrakTarih)
        {
            KKP_MFK mfk = null;
            using (KKPCon con = new KKPCon(ConStr))
            {
                con.CommandText = string.Format(@"SELECT * FROM FINSAT6{0}.FINSAT6{0}.MFK (nolock)
WHERE KynkEvrakTip={1} AND EvrakNo='{2}' AND EvrakTarih={3} AND HesapKod='{4}'"
                        , SirketKodu
                        , (short)kynkEvrakTip
                        , evrakNo
                        , evrakTarih
                        , hesapKod);

                SqlDataReader dr = con.ExecuteReader();
                while (dr.Read())
                {
                    mfk = new KKP_MFK();
                    Data.TabloDoldur(mfk, dr);
                    mfk.PropertyChanged += mfk.MFK_PropertyChanged;
                }

            }
            return mfk;

            /*
            KKP_MFK mfk = null;
            using (System.Data.Linq.DataContext context = new System.Data.Linq.DataContext(ConStr))
            {
                string sorgu = string.Format(@"SELECT * FROM FINSAT6{0}.FINSAT6{0}.MFK (nolock)
WHERE KynkEvrakTip={{0}} AND EvrakNo={{1}} AND EvrakTarih={{2}} AND HesapKod={{3}}", SirketKodu);

                mfk = context.ExecuteQuery<KKP_MFK>(sorgu, (short)kynkEvrakTip, evrakNo, evrakTarih, hesapKod).FirstOrDefault();
            }
            return mfk;

            */
        }

        public void Dispose()
        {
            ClearEntities();

            if (MyCon != null)
                MyCon.Dispose();
        }
    }







    #region Table

    public class STKTable
    {
        public List<KKP_STK> InsertList { get; set; }
        /// <summary>
        /// Primary Key'e Göre İşlem Yapılır.
        /// </summary>
        public List<KKP_STK> UpdateList { get; set; }
        /// <summary>
        /// Primary Key'e Göre İşlem Yapılır.
        /// </summary>
        public List<KKP_STK> DeleteList { get; set; }
                

        internal STKTable()
        {
            InsertList = new List<KKP_STK>();
            UpdateList = new List<KKP_STK>();
            DeleteList = new List<KKP_STK>();            
        }



        #region static fonksiyonlar
        public static KKP_STK NewSTK()
        {
            KKP_STK stk = new KKP_STK();
            Data.DefaultValueSetSTK(stk);
            return stk;
        }
        #endregion  static fonksiyonlar

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }

    }

    public class CHKTable
    {
        public List<KKP_CHK> InsertList { get; set; }
        public List<KKP_CHK> UpdateList { get; set; }
        public List<KKP_CHK> DeleteList { get; set; }

        internal CHKTable()
        {
            InsertList = new List<KKP_CHK>();
            UpdateList = new List<KKP_CHK>();
            DeleteList = new List<KKP_CHK>();
        }

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }
    }

    public class DEPTable
    {
        public List<KKP_DEP> InsertList { get; set; }
        public List<KKP_DEP> UpdateList { get; set; }
        public List<KKP_DEP> DeleteList { get; set; }

        internal DEPTable()
        {
            InsertList = new List<KKP_DEP>();
            UpdateList = new List<KKP_DEP>();
            DeleteList = new List<KKP_DEP>();
        }

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }
    }

    public class FYTTable
    {
        public List<KKP_FYT> InsertList { get; set; }
        public List<KKP_FYT> UpdateList { get; set; }
        public List<KKP_FYT> DeleteList { get; set; }

        internal FYTTable()
        {
            InsertList = new List<KKP_FYT>();
            UpdateList = new List<KKP_FYT>();
            DeleteList = new List<KKP_FYT>();
        }

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }
    }

    public class SPITable
    {
        public List<KKP_SPI> InsertList { get; set; }
        public List<KKP_SPI> UpdateList { get; set; }
        public List<KKP_SPI> DeleteList { get; set; }


        
        internal SPITable()
        {
            InsertList = new List<KKP_SPI>();
            UpdateList = new List<KKP_SPI>();
            DeleteList = new List<KKP_SPI>();
        }

        #region static fonksiyonlar
        public static KKP_SPI NewSPI(KKPSiparisTip tip)
        {
            KKP_SPI spi = new KKP_SPI();
            Data.DefaultValueSetSPI(spi, tip);
            return spi;
        }
        #endregion static fonksiyonlar

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }
    }

    public class DSTTable
    {
        public List<KKP_DST> InsertList { get; set; }
        public List<KKP_DST> UpdateList { get; set; }
        public List<KKP_DST> DeleteList { get; set; }

        internal DSTTable()
        {
            InsertList = new List<KKP_DST>();
            UpdateList = new List<KKP_DST>();
            DeleteList = new List<KKP_DST>();
        }

        #region static fonksiyonlar
        public static KKP_DST NewDST()
        {
            KKP_DST dst = new KKP_DST();
            Data.DefatulValueSetDST(dst);
            return dst;
        }
        #endregion  static fonksiyonlar

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }
    }

    public class FTDTable
    {
        #region enums

        public enum SatirTip
        {
            MalBedeli = 0,
            KalemlerdeOranIskontosu1 = 15,
            KalemlerdeOranIskontosu2 = 18,
            KalemlerdeOranIskontosu3 = 19,
            KalemlerdeOranIskontosu4 = 20,
            KalemlerdeOranIskontosu5 = 21,
            AraToplam = 11,
            KDV = 9,
            Toplam = 12
        }

        /// <summary>
        ///  select * from FINSAT615.FINSAT615.COMBOITEM_NAME WHERE ITEMCBOID=72
        /// </summary>
        public enum IslemTip
        {
            Fatura = 0,
            İadeFaturası = 1,
            İrsaliye = 2,
            AlımSiparişi = 3,
            SatışSiparişi = 4,
            PerakendeSatışFaturası = 5,
            SiparişeGöreSatışFaturası = 6,
            AlımRezervasyonu = 7,
            SatışRezervasyonu = 8,
            İadeİrsaliyesi = 9,
            Konsinyeİrsaliyesi = 10,
            Konsinyeİadeİrsaliyesi = 11,
            SerbestMeslekMakbuzu = 12,
            PerakendeSatıştanİadeFaturası = 13,
            GiderFaturası = 14,
            SatışTeklifi = 15,
            SatınalmaTeklifi = 16,
            HizmetFaturası = 17,
            HizmetİadeFaturası = 18,
            HizmetAlımSiparişi = 19,
            HizmetSatışSiparişi = 20,
            SabitKıymetFaturası = 21,
            SabitKıymetİadeFaturası = 22,
            SabitKıymetAlımSiparişi = 23,
            SabitKıymetSatışSiparişi = 24,
            AlımFiyatFarkıFaturası = 25,
            SatışFiyatFarkıFaturası = 26,
            MüstahsilMakbuzu = 27,
        }

        #endregion enums

        public List<KKP_FTD> InsertList { get; set; }
        public List<KKP_FTD> UpdateList { get; set; }
        public List<KKP_FTD> DeleteList { get; set; }


        internal FTDTable()
        {
            InsertList = new List<KKP_FTD>();
            UpdateList = new List<KKP_FTD>();
            DeleteList = new List<KKP_FTD>();
        }


        #region static functions

        internal static string GetSatirTipName(SatirTip satirTip)
        {
            string sonuc = "";
            switch (satirTip)
            {
                case SatirTip.MalBedeli:
                    sonuc = "Mal Hizmet Bedeli";
                    break;
                case SatirTip.KalemlerdeOranIskontosu1:
                    sonuc = "Kalemlerde Oran Iskontosu-1";
                    break;
                case SatirTip.KalemlerdeOranIskontosu2:
                    sonuc = "Kalemlerde Oran Iskontosu-2";
                    break;
                case SatirTip.KalemlerdeOranIskontosu3:
                    sonuc = "Kalemlerde Oran Iskontosu-3";
                    break;
                case SatirTip.KalemlerdeOranIskontosu4:
                    sonuc = "Kalemlerde Oran Iskontosu-4";
                    break;
                case SatirTip.KalemlerdeOranIskontosu5:
                    sonuc = "Kalemlerde Oran Iskontosu-5";
                    break;
                case SatirTip.AraToplam:
                    sonuc = "Ara Toplam";
                    break;
                case SatirTip.KDV:
                    sonuc = "KDV";
                    break;
                case SatirTip.Toplam:
                    sonuc = "Toplam";
                    break;
                default:
                    sonuc = "";
                    break;
            }
            return sonuc;
        }




        public static KKP_FTD NewFTD(KKPKynkEvrakTip kynkEvrakTip, SatirTip satirTip)
        {
            KKP_FTD ftd = new KKP_FTD();
            Data.DefaultValueSetFTD(ftd, kynkEvrakTip, satirTip);
            return ftd;
        }

        #endregion static functions

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }
    }

    public class MFKTable
    {
        public List<KKP_MFK> InsertList { get; set; }
        public List<KKP_MFK> UpdateList { get; set; }
        public List<KKP_MFK> DeleteList { get; set; }


        internal MFKTable()
        {
            InsertList = new List<KKP_MFK>();
            UpdateList = new List<KKP_MFK>();
            DeleteList = new List<KKP_MFK>();
        }

        #region static functions

        public static KKP_MFK NewMFK(KKPKynkEvrakTip kynkEvrakTip)
        {
            KKP_MFK mfk = new KKP_MFK();
            Data.DefaultValueSetMFK(mfk, kynkEvrakTip);
            return mfk;
        }


        #endregion static functions

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }

    }

    public class MHITable
    {
        public List<KKP_MHI> InsertList { get; set; }
        public List<KKP_MHI> UpdateList { get; set; }
        public List<KKP_MHI> DeleteList { get; set; }

        internal MHITable()
        {

        }

        public void Clear()
        {
            InsertList.Clear();
            UpdateList.Clear();
            DeleteList.Clear();
        }

    }
    #endregion Table




    #region Evraklar

    interface IEvrak
    {
        string EvrakNo { get; set; }
        DateTime Tarih { get; set; }
        string HesapKodu { get; set; }
        string Unvan { get; set; }

        KKPKynkEvrakTip KynkEvrTip { get; }
        int EvrakSeri { get; set; }

        bool INIGuncellensin { get; set; }

        bool GuncellemeModu { get; set; }
        KKPEvrakOzet GuncellenecekEvrak { get; set; }

        //string Kaydeden { get; set; }
        string Kaydeden { get;  }
        Nullable<DateTime> KayitTarih { get; }
        Nullable<DateTime> KayitSaat { get; }
        string KayitKaynak { get; }
        string KayitSurum { get; }

        //string Degistiren { get; set; }
        string Degistiren { get; }
        Nullable<DateTime> DegisTarih { get; }
        Nullable<DateTime> DegisSaat { get; }
        string DegisKaynak { get; }
        string DegisSurum { get; }
    }


    public class KKPEvrakOzet
    {
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string HesapKodu { get; set; }


        public bool MuhasebeKaydiniSil { get; set; }
        public short KynkEvrakTip { get; set; }
        internal KKPEvrakOzet()
        {

        }
    }

    public class KKPEvrakSiparis : IEvrak
    {

        #region fields
        string hesapKodu;
        string teslimYeriKodu;
        string evrakNo;
        DateTime tarih;
        KKPIslemTipSPI _islemTip;
        KKPDvzTL _dvzTL;
        string _dovizCinsi;
        decimal dovizKuru;
        DateTime tahTeslimTarihi;
        internal List<KKP_SPI> listSPI;
        #endregion fields

        #region Properties

        public KKPSiparisTip siparisTip { get; private set; }
        public string HesapKodu
        {
            get { return hesapKodu; }
            set
            {
                hesapKodu = value;
                DuzeltHepsi();
            }
        }
        public string Unvan { get; set; }
        public string TeslimYeriKodu
        {
            get { return teslimYeriKodu; }
            set
            {
                teslimYeriKodu = value;
                DuzeltHepsi();
            }
        }
        public string TeslimYeriAdi { get; set; }
        public string EvrakNo
        {
            get { return evrakNo; }
            set
            {
                evrakNo = value;
                DuzeltHepsi();
            }
        }
        public DateTime Tarih
        {
            get { return tarih; }
            set
            {
                tarih = value;
                DuzeltHepsi();
            }
        }
        public DateTime TahTeslimTarihi 
        {
            get { return tahTeslimTarihi; }
            set
            {
                tahTeslimTarihi = value;
                DuzeltHepsi();
            }
        }
        public KKPIslemTipSPI IslemTip
        {
            get { return _islemTip; }
            set
            {
                _islemTip = value;
                DuzeltHepsi();
            }
        }

        public KKPSiparisDurumu SiparisDurumu { get; set; }

        public KKPDvzTL dvzTL 
        { 
            get { return _dvzTL; }
            set
            {
                _dvzTL = value;
                DuzeltHepsi();
            }
        }
        public string DovizCinsi
        {
            get { return _dovizCinsi; }
            set
            {
                _dovizCinsi = value;
                DuzeltHepsi();
            }
        }
        public decimal DovizKuru 
        {
            get { return dovizKuru; }
            set
            {
                dovizKuru = value;
                DuzeltHepsi();
            }
        }

        public KKPKynkEvrakTip KynkEvrTip
        { 
            get 
            { 
                return (KKPKynkEvrakTip)siparisTip; 
            } 
        }

        public bool INIGuncellensin { get; set; }
        public int EvrakSeri { get; set; }


        #region kaydeden kayittarih
        
        public string Kaydeden 
        { 
            get
            {
                if (listSPI.Count == 0)
                    return null;
                return listSPI[0].Kaydeden;
            }
        }

        public Nullable<DateTime> KayitTarih 
        {
            get 
            {
                if (listSPI.Count == 0)
                    return null;
                return new DateTime(1900, 1, 1).AddDays(listSPI[0].KayitTarih - 2);
            }
        }

        public Nullable<DateTime> KayitSaat
        {
            get
            {
                if (KayitTarih == null)
                    return null;

                return MyHelper.intToSaat((DateTime)KayitTarih, listSPI[0].KayitSaat);
            }
        }

        public string KayitKaynak
        {
            get
            {
                if (listSPI.Count == 0)
                    return null;
                return MyHelper.GetCaption(((KKPKayitKaynak)listSPI[0].KayitKaynak).ToString());
            }
        }

        public string KayitSurum 
        {
            get
            {
                if (listSPI.Count == 0)
                    return null;
                return listSPI[0].KayitSurum;
            }
        }


        public string Degistiren
        {
            get
            {
                if (listSPI.Count == 0)
                    return null;
                return listSPI[0].Degistiren;
            }
        }

        public Nullable<DateTime> DegisTarih
        {
            get
            {
                if (listSPI.Count == 0)
                    return null;
                return new DateTime(1900, 1, 1).AddDays(listSPI[0].DegisTarih - 2);
            }
        }

        public Nullable<DateTime> DegisSaat
        {
            get
            {
                if (DegisTarih == null)
                    return null;

                return MyHelper.intToSaat((DateTime)DegisTarih, listSPI[0].DegisSaat);
            }
        }

        public string DegisKaynak
        {
            get
            {
                if (listSPI.Count == 0)
                    return null;
                return MyHelper.GetCaption(((KKPKayitKaynak)listSPI[0].DegisKaynak).ToString());
            }
        }

        public string DegisSurum
        {
            get
            {
                if (listSPI.Count == 0)
                    return null;
                return listSPI[0].DegisSurum;
            }
        }

        #endregion kaydeden kayittarih_son

        #endregion Properties

        #region Bağlantı Properties

        public KKP_SPI[] Satirlar { get { return listSPI.ToArray(); } }
        public KKP_MFK MFK { get; private set; }
        public List<KKP_FTD> FTDList { get; private set; }


        #endregion Bağlantı Properties


        public bool GuncellemeModu { get; set; }
        public KKPEvrakOzet GuncellenecekEvrak { get; set; }

        internal bool fromLoad { get; set; }
        public KKPEvrakSiparis(KKPSiparisTip tip)
        {
            siparisTip = tip;
            listSPI = new List<KKP_SPI>();
            MFK = new KKP_MFK((KKPKynkEvrakTip)(int)tip);
            FTDList = new List<KKP_FTD>();
            EvrakSeri = 1;
            fromLoad = false;
            GuncellemeModu = false;
            GuncellenecekEvrak = new KKPEvrakOzet();

            _dovizCinsi = "";
            dovizKuru = 0;
            _dvzTL = KKPDvzTL.YerelPara;
        }

        public void Ekle(KKP_SPI spi)
        {
            if (spi == null)
                throw new ArgumentNullException("Null değer [Ekle(spi)]");
            Duzelt(spi);
            listSPI.Add(spi);
        }

        public void Kaldir(KKP_SPI spi)
        {
            listSPI.Remove(spi);
        }

        public void Kaldir(int index)
        {
            if (listSPI[index] == null)
                throw new ArgumentOutOfRangeException("index", "Hatalı index gönderimi [Kaldir(int index)]");
            listSPI.RemoveAt(index);
        }

        public void TumHesaplamalariYap()
        {
            //foreach (KKP_SPI item in listSPI)
            //{
            //    item.SatirHesapla();
            //}

            FTDHesapla();
        }

        public void FTDHesapla()
        {
            FTDList.Clear();

            foreach (KKP_SPI item in listSPI)
            {
                item.SatirHesapla();
            }

            short siraNo = 0;

            KKP_FTD ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.MalBedeli);
            decimal malBedeli = listSPI.Sum(t => t.Tutar);
            ftd.SiraNo = siraNo;
            siraNo++;
            ftd.Iskonto = malBedeli;
            ftd.DvzTL = (short)dvzTL;
            ftd.DovizCinsi = DovizCinsi;
            ftd.DovizKuru = DovizKuru;
            ftd.DovizTutar = listSPI.Sum(t => t.DovizTutar);
            FTDList.Add(ftd);

            decimal isk1Top = listSPI.Sum(t => t.Isk1Tutar);
            decimal isk2Top = listSPI.Sum(t => t.Isk2Tutar);
            decimal isk3Top = listSPI.Sum(t => t.Isk3Tutar);
            decimal isk4Top = listSPI.Sum(t => t.Isk4Tutar);
            decimal isk5Top = listSPI.Sum(t => t.Isk5Tutar);

            decimal isk1TopDvz = 0, isk2TopDvz = 0, isk3TopDvz = 0, isk4TopDvz = 0, isk5TopDvz = 0;
            if (DovizKuru > 0)
            {
                isk1TopDvz = listSPI.Sum(t => t.Isk1TutarDvz);
                isk2TopDvz = listSPI.Sum(t => t.Isk2TutarDvz);
                isk3TopDvz = listSPI.Sum(t => t.Isk3TutarDvz);
                isk4TopDvz = listSPI.Sum(t => t.Isk4TutarDvz);
                isk5TopDvz = listSPI.Sum(t => t.Isk5TutarDvz);
            }

            if (isk1Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu1);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk1Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk1TopDvz;
                FTDList.Add(ftd);
            }

            if (isk2Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu2);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk2Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk2TopDvz;
                FTDList.Add(ftd);
            }

            if (isk3Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu3);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk3Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk3TopDvz;
                FTDList.Add(ftd);
            }

            if (isk4Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu4);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk4Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk4TopDvz;
                FTDList.Add(ftd);
            }

            if (isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu5);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk5Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk5TopDvz;
                FTDList.Add(ftd);
            }

            if (isk1Top > 0 || isk2Top > 0 || isk3Top > 0 || isk4Top > 0 || isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.AraToplam);
                decimal araToplam = malBedeli - (isk1Top + isk2Top + isk3Top + isk4Top + isk5Top);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = araToplam;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                //if (dvzTL == DvzTL.Döviz)
                    ftd.DovizTutar = DovizKuru > 0 ? Math.Round(araToplam / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                FTDList.Add(ftd);
            }




            decimal kdv18Top = 0, kdv8Top = 0;
            if (listSPI.Any(t => t.KDV > 0))
            {
                kdv18Top = listSPI.Where(t => t.KDVOran == 18).Sum(t => t.KDV);
                if (kdv18Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 18;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DovizCinsi;
                    ftd.DovizKuru = DovizKuru;
                    ftd.Iskonto = kdv18Top;
                    //if (dvzTL == DvzTL.Döviz)
                        ftd.DovizTutar = DovizKuru > 0 ? Math.Round(kdv18Top / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
                kdv8Top = listSPI.Where(t => t.KDVOran == 8).Sum(t => t.KDV);
                if (kdv8Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 8;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DovizCinsi;
                    ftd.DovizKuru = DovizKuru;
                    ftd.Iskonto = kdv8Top;
                    //if (dvzTL == DvzTL.Döviz)
                        ftd.DovizTutar = DovizKuru > 0 ? Math.Round(kdv8Top / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
            }

            //if (FTDList.Count > 1) ///MalBedelinden başka iskonto ve kdv varsa Genel toplam da eklenecek. 
            //                       ///Yoksa MalBedeli tek başına da kalabilir
            //{
            decimal iskTop = isk1Top + isk2Top + isk3Top + isk4Top + isk5Top;
            ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.Toplam);
            ftd.SiraNo = siraNo;
            ftd.Iskonto = malBedeli - iskTop  + kdv8Top + kdv18Top;
            ftd.DvzTL = (short)dvzTL;
            ftd.DovizCinsi = DovizCinsi;
            ftd.DovizKuru = DovizKuru;
            ftd.DovizTutar = DovizKuru > 0 ? Math.Round(ftd.Iskonto / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
            FTDList.Add(ftd);
            //}

            foreach (var item in FTDList)
            {
                Duzelt(item);
            }
        }

        public void FTDHesapla(string DvzCinsi , decimal DvzKuru)
        {
            FTDList.Clear();

            short siraNo = 0;

            KKP_FTD ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.MalBedeli);
            decimal malBedeli = listSPI.Sum(t => t.Tutar);
            ftd.SiraNo = siraNo;
            siraNo++;
            ftd.Iskonto = malBedeli;
            ftd.DvzTL = (short)1;
            ftd.DovizCinsi = DvzCinsi;
            ftd.DovizKuru = DvzKuru;
            //ftd.DovizTutar = listSPI.Sum(t => t.DovizTutar);
            if (DvzKuru == Convert.ToDecimal(0))
            {
                ftd.DovizTutar = Convert.ToDecimal(0);
            }
            else
            {
                ftd.DovizTutar = malBedeli / DvzKuru;
            }
           
            FTDList.Add(ftd);

            decimal isk1Top = listSPI.Sum(t => t.Isk1Tutar);
            decimal isk2Top = listSPI.Sum(t => t.Isk2Tutar);
            decimal isk3Top = listSPI.Sum(t => t.Isk3Tutar);
            decimal isk4Top = listSPI.Sum(t => t.Isk4Tutar);
            decimal isk5Top = listSPI.Sum(t => t.Isk5Tutar);

            decimal isk1TopDvz = 0, isk2TopDvz = 0, isk3TopDvz = 0, isk4TopDvz = 0, isk5TopDvz = 0;
            if (DovizKuru > 0)
            {
                isk1TopDvz = listSPI.Sum(t => t.Isk1TutarDvz);
                isk2TopDvz = listSPI.Sum(t => t.Isk2TutarDvz);
                isk3TopDvz = listSPI.Sum(t => t.Isk3TutarDvz);
                isk4TopDvz = listSPI.Sum(t => t.Isk4TutarDvz);
                isk5TopDvz = listSPI.Sum(t => t.Isk5TutarDvz);
            }

            if (isk1Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu1);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk1Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DvzCinsi;
                ftd.DovizKuru = DvzKuru;
                ftd.DovizTutar = isk1TopDvz;
                FTDList.Add(ftd);
            }

            if (isk2Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu2);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk2Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DvzCinsi;
                ftd.DovizKuru = DvzKuru;
                ftd.DovizTutar = isk2TopDvz;
                FTDList.Add(ftd);
            }

            if (isk3Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu3);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk3Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DvzCinsi;
                ftd.DovizKuru = DvzKuru;
                ftd.DovizTutar = isk3TopDvz;
                FTDList.Add(ftd);
            }

            if (isk4Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu4);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk4Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DvzCinsi;
                ftd.DovizKuru = DvzKuru;
                ftd.DovizTutar = isk4TopDvz;
                FTDList.Add(ftd);
            }

            if (isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KalemlerdeOranIskontosu5);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk5Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DvzCinsi;
                ftd.DovizKuru = DvzKuru;
                ftd.DovizTutar = isk5TopDvz;
                FTDList.Add(ftd);
            }

            if (isk1Top > 0 || isk2Top > 0 || isk3Top > 0 || isk4Top > 0 || isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.AraToplam);
                decimal araToplam = malBedeli - (isk1Top + isk2Top + isk3Top + isk4Top + isk5Top);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = araToplam;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DvzCinsi;
                ftd.DovizKuru = DvzKuru;
                //if (dvzTL == DvzTL.Döviz)
                ftd.DovizTutar = DvzKuru > 0 ? Math.Round(araToplam / DvzKuru, 2, MidpointRounding.AwayFromZero) : 0;
                FTDList.Add(ftd);
            }




            decimal kdv18Top = 0, kdv8Top = 0;
            if (listSPI.Any(t => t.KDV > 0))
            {
                kdv18Top = listSPI.Where(t => t.KDVOran == 18).Sum(t => t.KDV);
                if (kdv18Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 18;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DvzCinsi;
                    ftd.DovizKuru = DvzKuru;
                    ftd.Iskonto = kdv18Top;
                    //if (dvzTL == DvzTL.Döviz)
                    ftd.DovizTutar = DvzKuru > 0 ? Math.Round(kdv18Top / DvzKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
                kdv8Top = listSPI.Where(t => t.KDVOran == 8).Sum(t => t.KDV);
                if (kdv8Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 8;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DvzCinsi;
                    ftd.DovizKuru = DvzKuru;
                    ftd.Iskonto = kdv8Top;
                    //if (dvzTL == DvzTL.Döviz)
                    ftd.DovizTutar = DvzKuru > 0 ? Math.Round(kdv8Top / DvzKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
            }

            //if (FTDList.Count > 1) ///MalBedelinden başka iskonto ve kdv varsa Genel toplam da eklenecek. 
            //                       ///Yoksa MalBedeli tek başına da kalabilir
            //{
            decimal iskTop = isk1Top + isk2Top + isk3Top + isk4Top + isk5Top;
            ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)siparisTip, FTDTable.SatirTip.Toplam);
            ftd.SiraNo = siraNo;
            ftd.Iskonto = malBedeli - iskTop + kdv8Top + kdv18Top;
            ftd.DvzTL = (short)dvzTL;
            ftd.DovizCinsi = DvzCinsi;
            ftd.DovizKuru = DvzKuru;
            ftd.DovizTutar = DvzKuru > 0 ? Math.Round(ftd.Iskonto / DvzKuru, 2, MidpointRounding.AwayFromZero) : 0;
            FTDList.Add(ftd);
            //}

            foreach (var item in FTDList)
            {
                Duzelt(item, DvzCinsi, DvzKuru);
            }
        }

        internal void SetMfk(KKP_MFK mfk)
        {
            MFK = mfk;
        }


        #region Private Functions

        private void DuzeltHepsi()
        {
            foreach (KKP_SPI item in listSPI)
            {
                Duzelt(item);
            }
            foreach (KKP_FTD item in FTDList)
            {
                Duzelt(item);
            }

            DuzeltMFK();
        }

        private void Duzelt(KKP_SPI spi)
        {
            if (fromLoad)
                return;

            spi.Chk = HesapKodu;
            spi.IslemTur = (short)(siparisTip == KKPSiparisTip.SatisSiparisi ? 1 : 0);
            spi.KynkEvrakTip = (short)siparisTip;
            spi.AnaEvrakTip = (short)siparisTip;
            spi.KayitKaynak = siparisTip == KKPSiparisTip.SatisSiparisi ? (short)KKPKayitKaynak.SatisSiparisi : (short)KKPKayitKaynak.AlimSiparisi;
            spi.DegisKaynak = spi.KayitKaynak;


            spi.EvrakNo = EvrakNo;
            spi.Tarih = Convert.ToInt32(Tarih.Date.ToOADate());
            spi.TahTeslimTarih = Convert.ToInt32(TahTeslimTarihi.ToOADate());
            if (spi.TahTeslimTarih < 0)
                spi.TahTeslimTarih = 0;
            spi.IslemTip = (short)IslemTip;
            spi.TeslimChk = TeslimYeriKodu;
            //spi.DvzTL = (short)dvzTL;

            if (dvzTL == KKPDvzTL.YerelPara)
            {
                spi.DovizCinsi = "";
                spi.DovizKuru = 0;
            }
            //else
            //{
            //    spi.DovizCinsi = DovizCinsi;
            //    spi.DovizKuru = DovizKuru;
            //}


        }

        private void Duzelt(KKP_FTD ftd)
        {
            if (fromLoad)
                return;

            ftd.HesapKodu = HesapKodu;
            ftd.TeslimChk = teslimYeriKodu;
            ftd.EvrakNo = EvrakNo;
            ftd.Tarih = Convert.ToInt32(Tarih.Date.ToOADate());
            ftd.FormBaBsTarih = ftd.Tarih;
            ftd.DvzTL = (short)dvzTL;
            if (dvzTL == KKPDvzTL.YerelPara)
            {
                ftd.DovizCinsi = "";
                ftd.DovizKuru = 0;
            }
            //else
            //{
            //    ftd.DovizCinsi = DovizCinsi;
            //    ftd.DovizKuru = DovizKuru;
            //}
        }

        private void Duzelt(KKP_FTD ftd, string DvzCinsi, decimal DvzKuru)
        {
            if (fromLoad)
                return;

            ftd.HesapKodu = HesapKodu;
            ftd.TeslimChk = teslimYeriKodu;
            ftd.EvrakNo = EvrakNo;
            ftd.Tarih = Convert.ToInt32(Tarih.Date.ToOADate());
            ftd.FormBaBsTarih = ftd.Tarih;
            ftd.DvzTL = (short)dvzTL;
            if (dvzTL == KKPDvzTL.YerelPara)
            {
                ftd.DovizCinsi = "";
                ftd.DovizKuru = 0;
            }
            else
            {
                ftd.DovizCinsi = DvzCinsi;
                ftd.DovizKuru = DvzKuru;
            }
        }

        private void DuzeltMFK()
        {
            if (fromLoad)
                return;

            MFK.EvrakNo = evrakNo;
            MFK.HesapKod = HesapKodu;
            MFK.EvrakTarih = Convert.ToInt32(Tarih.Date.ToOADate());
            MFK.Depo = "";
            if (listSPI.Count > 0)
                MFK.Depo = listSPI[0].Depo;
        }

        #endregion Private Functions
    }
    
    public class KKPEvrakStokGirisCikis : IEvrak
    {

        #region fields
        string evrakNo;
        DateTime tarih;
        KKPIslemTipSPI islemTip;
        KKPStokGirisCikisTip girisCikis;
        #endregion fields

        internal List<KKP_STI> listSTI;


        #region Properties
        #region kaydeden kayittarih

        public string Kaydeden
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].Kaydeden;
            }
        }

        public Nullable<DateTime> KayitTarih
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return new DateTime(1900, 1, 1).AddDays(listSTI[0].KayitTarih - 2);
            }
        }

        public Nullable<DateTime> KayitSaat
        {
            get
            {
                if (KayitTarih == null)
                    return null;

                return MyHelper.intToSaat((DateTime)KayitTarih, listSTI[0].KayitSaat);
            }
        }

        public string KayitKaynak
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return MyHelper.GetCaption(((KKPKayitKaynak)listSTI[0].KayitKaynak).ToString());
            }
        }

        public string KayitSurum
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].KayitSurum;
            }
        }


        public string Degistiren
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].Degistiren;
            }
        }

        public Nullable<DateTime> DegisTarih
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return new DateTime(1900, 1, 1).AddDays(listSTI[0].DegisTarih - 2);
            }
        }

        public Nullable<DateTime> DegisSaat
        {
            get
            {
                if (DegisTarih == null)
                    return null;

                return MyHelper.intToSaat((DateTime)DegisTarih, listSTI[0].DegisSaat);
            }
        }

        public string DegisKaynak 
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return MyHelper.GetCaption(((KKPKayitKaynak)listSTI[0].DegisKaynak).ToString());
            }
        }

        public string DegisSurum
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].DegisSurum;
            }
        }
        #endregion kaydeden kayittarih_son


        public string EvrakNo
        {
            get { return evrakNo; }
            set { evrakNo = value; DuzeltHepsi(); }
        }
        public DateTime Tarih
        {
            get { return tarih; }
            set { tarih = value; DuzeltHepsi(); }
        }
        public string HesapKodu { get; set; }
        public string Unvan { get; set; }

        public KKPIslemTipSPI IslemTip
        {
            get { return islemTip; }
            set { islemTip = value; DuzeltHepsi(); }
        }
        public KKPStokGirisCikisTip GirisCikis
        {
            get { return girisCikis; }
        }
        public KKPKynkEvrakTip KynkEvrTip
        {
            get { return (KKPKynkEvrakTip)GirisCikis; }
        }

        public KKP_STI[] Satirlar { get { return listSTI.ToArray(); } }

        public bool INIGuncellensin { get; set; }

        public int EvrakSeri { get; set; }

        

        #endregion Properties

        public bool GuncellemeModu { get; set; }
        public KKPEvrakOzet GuncellenecekEvrak { get; set; }

        internal bool fromLoad { get; set; }
        public KKPEvrakStokGirisCikis(KKPStokGirisCikisTip tip)
        {
            girisCikis = tip;
            EvrakSeri = 1;

            listSTI = new List<KKP_STI>();
            GuncellenecekEvrak = new KKPEvrakOzet();
        }

        #region FUNCTIONS
        
        public void Ekle(KKP_STI sti)
        {
            if (sti == null)
                throw new ArgumentNullException("Null değer [Ekle(sti)]");

            Duzelt(sti);
            listSTI.Add(sti);
        }

        public void Kaldir(KKP_STI sti)
        {
            listSTI.Remove(sti);
        }

        public void Kaldir(int index)
        {
            if (listSTI[index] == null)
                throw new ArgumentOutOfRangeException("index", "Hatalı index gönderimi [Kaldir(int index)]");
            listSTI.RemoveAt(index);
        }

        public void TumunuKaldir()
        {
            listSTI.Clear();
        }

        private void DuzeltHepsi()
        {
            foreach (KKP_STI item in listSTI)
            {
                Duzelt(item);
            }            
        }

        private void Duzelt(KKP_STI sti)
        {
            if (fromLoad)
                return;

            ///sti.Chk = "";  ///02.05.2016 tarihinde kaldırıldı. Odun Satınalma Stok Giriş Çıkış Fişinde Devir için Chk yı kullanıyoruz.
            sti.IslemTur = (short)(GirisCikis == KKPStokGirisCikisTip.Giris ? 0 : 1);
            sti.KynkEvrakTip = (short)GirisCikis;
            sti.AnaEvrakTip = (short)GirisCikis;
            sti.KayitKaynak = GirisCikis == KKPStokGirisCikisTip.Giris ? (short)KKPKayitKaynak.StokGirisFisi : (short)KKPKayitKaynak.StokCikisFisi;
            sti.DegisKaynak = sti.KayitKaynak;


            sti.EvrakNo = EvrakNo;
            sti.Tarih = Convert.ToInt32(Tarih.Date.ToOADate());
            sti.VadeTarih = sti.Tarih;
            sti.EvrakTarih = sti.Tarih;

            sti.IslemTip = (short)IslemTip;
            //sti.TeslimChk = "";  ///sti.Chk yorum yapılınca bu da yorum yapıldı.
            sti.DvzTL = (short)KKPDvzTL.YerelPara;

            //if (Kaydeden != null)
            //    sti.Kaydeden = Kaydeden;
            //if (KayitTarih > 0)
            //    sti.KayitTarih = KayitTarih;
            //if (KayitSaat > 0)
            //    sti.KayitSaat = KayitSaat;

            //if (Degistiren != null)
            //    sti.Degistiren = Degistiren;
            //if (degisTarih > 0)
            //    sti.DegisTarih = DegisTarih;
            //if (DegisSaat > 0)
            //    sti.DegisSaat = DegisSaat;

        }
        #endregion FUNCTIONS


        public KKPEvrakMuhFis MuhasebeFisSatirlariniOlustur()///List<KKP_STK> STKInfoList)
        {
            if (listSTI.Count == 0)
                return null;

            KKPEvrakMuhFis fis = new KKPEvrakMuhFis();
            fis.Tarih = this.Tarih;
            fis.FisTip = 4;
            fis.FisNo = -1;
            fis.MaddeNo = -1;
            
            fis.MrkHKod = this.listSTI[0].Chk;
            fis.KynkEvrakNo = this.EvrakNo;
            fis.KynkEvrakTarih = this.Tarih;


            short mhisiraNo = 0, entfisSiraNo = 0;
            foreach (KKP_STI sti in Satirlar)
            {
                ///KKP_STK stk = STKInfoList.Find(t => t.MalKodu == sti.MalKodu);

                #region ALIM MHI

                KKP_MHI alMHI = new KKP_MHI();
                Data.DefaultValueSet(alMHI);
                alMHI.HesapKod = sti.MhsKod; ///stk.AlimlarHesabi;
                alMHI.KebirKod = sti.MhsKod.Length >= 3 ? sti.MhsKod.Substring(0, 3) : sti.MhsKod; ///stk.AlimlarHesabi.Substring(0, 3);
                alMHI.Tarih = sti.Tarih;
                alMHI.Fistip = 4;
                alMHI.FisNo = -1;
                alMHI.SiraNo = mhisiraNo;
                alMHI.Aciklama = sti.EvrakNo + " Dış Depodan Saha Depoya Transfer";
                alMHI.BA = 1;
                alMHI.Tutar = sti.Tutar;
                alMHI.MaddeNo = -1;
                alMHI.Kod1 = sti.Kod1;
                alMHI.Kod2 = sti.Kod2;
                alMHI.Kod3 = sti.Kod3;
                alMHI.Kod4 = sti.Kod4;
                alMHI.Kod5 = sti.Kod5;
                alMHI.Kod6 = sti.Kod6;
                alMHI.Birim = sti.Birim;
                alMHI.Miktar = sti.Miktar;
                alMHI.EvrakTip = sti.KynkEvrakTip;
                alMHI.MrkHKod = sti.Chk;
                alMHI.Tarih2 = sti.Tarih;
                alMHI.AnaEvrakTip = sti.KynkEvrakTip;

                alMHI.Kaydeden = "12M";
                alMHI.KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                alMHI.KayitSaat = Data.SaatForDB(DateTime.Now);
                alMHI.KayitKaynak = 84; //dÖNEMSEL FİŞ İŞLEMLERİ
                alMHI.KayitSurum = KKP.LinkSurum;

                alMHI.Degistiren = "12M";
                alMHI.DegisTarih = alMHI.KayitTarih;
                alMHI.DegisSaat = alMHI.KayitSaat;
                alMHI.KayitKaynak = 84;
                alMHI.KayitSurum = KKP.LinkSurum;

                alMHI.CheckSum = 12;
                alMHI.Printed = 0;
                alMHI.YevmiyeEvrakTipi = -1;
                alMHI.KaynakEvrakNo = sti.EvrakNo;
                alMHI.KaynakEvrakTarih = sti.Tarih;

                fis.MHIList.Add(alMHI);
                #endregion ALIM MHI SON
                
                mhisiraNo++;

                #region satış MHI

                KKP_MHI satMHI = alMHI.Copy(); ///new KKP_MHI();
                //Data.DefaultValueSet(satMHI);
                satMHI.HesapKod = sti.MhsKarsiKod; ///stk.SatislarHesabi;
                satMHI.KebirKod = sti.MhsKarsiKod.Length >= 3 ? sti.MhsKarsiKod.Substring(0, 3) : sti.MhsKarsiKod; ///stk.SatislarHesabi.Substring(0, 3);
                satMHI.BA = 0;
                satMHI.SiraNo = mhisiraNo;                                                                                              
                //satMHI.Tarih = sti.Tarih;
                //satMHI.Fistip = 4;
                //satMHI.FisNo = -1;
                //satMHI.SiraNo = mhisiraNo;
                //satMHI.Aciklama = "Dış Depodan Saha Depoya Transfer";
                //satMHI.BA = 0;
                //satMHI.Tutar = sti.Tutar;
                //satMHI.MaddeNo = -1;
                //satMHI.Kod1 = sti.Kod1;
                //satMHI.Kod2 = sti.Kod2;
                //satMHI.Kod3 = sti.Kod3;
                //satMHI.Kod4 = sti.Kod4;
                //satMHI.Kod5 = sti.Kod5;
                //satMHI.Kod6 = sti.Kod6;
                //satMHI.Birim = sti.Birim;
                //satMHI.Miktar = sti.Miktar;
                //satMHI.EvrakTip = sti.KynkEvrakTip;
                //satMHI.MrkHKod = sti.Chk;
                //satMHI.Tarih2 = sti.Tarih;
                //satMHI.AnaEvrakTip = sti.KynkEvrakTip;

                //satMHI.Kaydeden = "12M";
                //satMHI.KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                //satMHI.KayitSaat = Data.SaatForDB(DateTime.Now);
                //satMHI.KayitKaynak = 84; //dÖNEMSEL FİŞ İŞLEMLERİ
                //satMHI.KayitSurum = KKP.LinkSurum;
                
                //satMHI.Degistiren = "12M";
                //satMHI.DegisTarih = satMHI.KayitTarih;
                //satMHI.DegisSaat = satMHI.KayitSaat;
                //satMHI.KayitKaynak = 84;
                //satMHI.KayitSurum = KKP.LinkSurum;

                //satMHI.CheckSum = 12;
                //satMHI.Printed = 0;
                //satMHI.KaynakEvrakNo = sti.EvrakNo;
                //satMHI.KaynakEvrakTarih = sti.Tarih;


                fis.MHIList.Add(satMHI);
                #endregion satış MHI SON

                
                mhisiraNo++;

                #region ENT AL
                
                KKP_ENT entAL = new KKP_ENT();
                Data.DefaultValueSet(entAL);
                entAL.IslemTip = 1;
                entAL.IslemTur = 1;
                entAL.EvrakNo = sti.EvrakNo;
                entAL.Tarih = sti.Tarih;
                entAL.HesapKodu = sti.Chk;
                entAL.KynkEvrakTip = sti.KynkEvrakTip;
                entAL.SiraNo = sti.SiraNo;
                entAL.TabloID = 12;
                entAL.BA = 1;
                entAL.FisTip = 4;
                entAL.FisTarih = sti.Tarih;
                entAL.FisNo = -1;
                entAL.FisMaddeNo = -1;
                entAL.FisSiraNo = entfisSiraNo;
                entAL.FisMhsKod = sti.MhsKod; ///stk.SatislarHesabi;
                entAL.EntYontem = 2;
                entAL.EntTabloNo = 9;
                entAL.EntTuru = 1;


                entAL.Kaydeden = "12M";
                entAL.KayitTarih = Convert.ToInt32(DateTime.Today.ToOADate());
                entAL.KayitSaat = Data.SaatForDB(DateTime.Now);
                entAL.KayitKaynak = 84; //dÖNEMSEL FİŞ İŞLEMLERİ
                entAL.KayitSurum = KKP.LinkSurum;

                entAL.Degistiren = "12M";
                entAL.DegisTarih = satMHI.KayitTarih;
                entAL.DegisSaat = satMHI.KayitSaat;
                entAL.KayitKaynak = 84;
                entAL.KayitSurum = KKP.LinkSurum;

                entAL.CheckSum = 12;
                entAL.EntSekli = 0;

                fis.ENTList.Add(entAL);

                #endregion ENT AL SON

                entfisSiraNo++;

                #region ENT SAT

                KKP_ENT entSAT = entAL.Copy();
                entSAT.BA = 0;
                entSAT.FisMhsKod = sti.MhsKarsiKod; ///stk.AlimlarHesabi;
                entSAT.FisSiraNo = entfisSiraNo;

                fis.ENTList.Add(entSAT);

                #endregion ENT SAT SON

                entfisSiraNo++;


            }


            return fis;
        }
    }

    public class KKPEvrakIrsaliye : IEvrak
    {
        #region fields
        string hesapKodu;
        string teslimYeriKodu;
        string evrakNo;
        DateTime tarih;
        KKPIslemTipSPI _islemTip;
        KKPDvzTL _dvzTL;
        string _dovizCinsi;
        decimal dovizKuru;

        internal List<KKP_STI> listSTI;
        internal List<KKP_STI> silinenSatirlar;
        //internal List<KKP_SPI> listKaynakSip;
        #endregion fields

        #region Properties

        public KKPIrsaliyeTip IrsaliyeTip { get; private set; }
        public string HesapKodu
        {
            get { return hesapKodu; }
            set
            {
                hesapKodu = value;
                DuzeltHepsi();
            }
        }
        public string Unvan { get; set; }
        public string TeslimYeriKodu
        {
            get { return teslimYeriKodu; }
            set
            {
                teslimYeriKodu = value;
                DuzeltHepsi();
            }
        }
        public string TeslimYeriAdi { get; set; }
        public string EvrakNo
        {
            get { return evrakNo; }
            set
            {
                evrakNo = value;
                DuzeltHepsi();
            }
        }
        public DateTime Tarih
        {
            get { return tarih; }
            set
            {
                tarih = value;
                DuzeltHepsi();
            }
        }
        //public DateTime TahTeslimTarihi { get; set; }
        public KKPIslemTipSPI IslemTip
        {
            get { return _islemTip; }
            set
            {
                _islemTip = value;
                DuzeltHepsi();
            }
        }

        //public KKPSiparisDurumu SiparisDurumu { get; set; }

        public KKPDvzTL dvzTL
        {
            get { return _dvzTL; }
            set
            {
                _dvzTL = value;
                DuzeltHepsi();
            }
        }
        public string DovizCinsi
        {
            get { return _dovizCinsi; }
            set
            {
                _dovizCinsi = value;
                DuzeltHepsi();
            }
        }
        public decimal DovizKuru
        {
            get { return dovizKuru; }
            set
            {
                dovizKuru = value;
                DuzeltHepsi();
            }
        }

        public KKPKynkEvrakTip KynkEvrTip
        {
            get
            {
                return (KKPKynkEvrakTip)IrsaliyeTip;
            }
        }

        public bool INIGuncellensin { get; set; }
        public int EvrakSeri { get; set; }


        #region kaydeden kayittarih

        public string Kaydeden
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].Kaydeden;
            }
        }

        public Nullable<DateTime> KayitTarih
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return new DateTime(1900, 1, 1).AddDays(listSTI[0].KayitTarih - 2);
            }
        }

        public Nullable<DateTime> KayitSaat
        {
            get
            {
                if (KayitTarih == null)
                    return null;

                return MyHelper.intToSaat((DateTime)KayitTarih, listSTI[0].KayitSaat);
            }
        }

        public string KayitKaynak
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return MyHelper.GetCaption(((KKPKayitKaynak)listSTI[0].KayitKaynak).ToString());
            }
        }

        public string KayitSurum
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].KayitSurum;
            }
        }


        public string Degistiren
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].Degistiren;
            }
        }

        public Nullable<DateTime> DegisTarih
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return new DateTime(1900, 1, 1).AddDays(listSTI[0].DegisTarih - 2);
            }
        }

        public Nullable<DateTime> DegisSaat
        {
            get
            {
                if (DegisTarih == null)
                    return null;

                return MyHelper.intToSaat((DateTime)DegisTarih, listSTI[0].DegisSaat);
            }
        }

        public string DegisKaynak
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return MyHelper.GetCaption(((KKPKayitKaynak)listSTI[0].DegisKaynak).ToString());
            }
        }

        public string DegisSurum
        {
            get
            {
                if (listSTI.Count == 0)
                    return null;
                return listSTI[0].DegisSurum;
            }
        }

        #endregion kaydeden kayittarih_son

        #endregion Properties

        #region Bağlantı Properties
        public KKP_STI[] Satirlar { get { return listSTI.ToArray(); } }
        public KKP_MFK MFK { get; private set; }
        public List<KKP_FTD> FTDList { get; private set; }
        public KKP_SPI[] SatirlarSPI 
        { 
            get 
            { 
                List<KKP_SPI> list = new List<KKP_SPI>();
                if(listSTI.Count == 0)
                    return list.ToArray();
                foreach (var item in Satirlar)
                {
                    if (item.SPIInfo != null)
                        list.Add(item.SPIInfo);
                }
                return list.ToArray();
            } 
        }
        public KKP_IRS[] SatirlarIRS 
        {
            get 
            {
                List<KKP_IRS> list = new List<KKP_IRS>();
                if (listSTI.Count == 0)
                    return list.ToArray();
                foreach (var item in Satirlar)
                {
                    if (item.IRSInfo != null)
                        list.Add(item.IRSInfo);
                }
                return list.ToArray();
            }
        }

        public KKP_STI[] SilinenSatirlar { get { return silinenSatirlar.ToArray(); } }
        #endregion


        public bool GuncellemeModu { get; set; }
        public KKPEvrakOzet GuncellenecekEvrak { get; set; }

        internal bool fromLoad { get; set; }
        public KKPEvrakIrsaliye(KKPIrsaliyeTip tip)
        {
            IrsaliyeTip = tip;
            listSTI = new List<KKP_STI>();
            MFK = new KKP_MFK((KKPKynkEvrakTip)(int)tip);
            FTDList = new List<KKP_FTD>();
            EvrakSeri = 1;
            fromLoad = false;
            GuncellemeModu = false;
            GuncellenecekEvrak = new KKPEvrakOzet();

            _dovizCinsi = "";
            dovizKuru = 0;
            _dvzTL = KKPDvzTL.YerelPara;
            silinenSatirlar = new List<KKP_STI>();


           
        }

        public void Ekle(KKP_STI sti)
        {
            if (sti == null)
                throw new ArgumentNullException("Null değer [Ekle(spi)]");
            Duzelt(sti);
            listSTI.Add(sti);
        }

        public void Kaldir(KKP_STI sti)
        {
            listSTI.Remove(sti);
            silinenSatirlar.Add(sti);
        }

        public void Kaldir(int index)
        {
            if (listSTI[index] == null)
                throw new ArgumentOutOfRangeException("index", "Hatalı index gönderimi [Kaldir(int index)]");

            silinenSatirlar.Add(listSTI[index]);
            listSTI.RemoveAt(index);            
        }

        public KKP_STI SiparisSatiriEkle(KKP_SPI spi)
        {
            if (spi == null || string.IsNullOrEmpty(spi.EvrakNo) || spi.Tarih <= 0 || string.IsNullOrEmpty(spi.MalKodu)
                || (spi.KynkEvrakTip != 62 && spi.KynkEvrakTip != 63) 
                || string.IsNullOrEmpty(spi.Birim))
                throw new ArgumentException("SPI nesnesi null veya geçersiz bilgilere sahip");

            if (spi.KynkEvrakTip == 63 && this.IrsaliyeTip != KKPIrsaliyeTip.AlimIrsaliyesi)
                throw new ArgumentException("SPI Kaydının KynkEvrakTipi hatalı!!");

            if(spi.KynkEvrakTip == 62 && this.IrsaliyeTip != KKPIrsaliyeTip.SatisIrsaliyesi)
                throw new ArgumentException("SPI Kaydının KynkEvrakTipi hatalı!!");

            if (spi.SiparisDurumu == 1)
                throw new ArgumentException("Sipariş Satırının Durumu kapalı!!"); ;

            decimal miktar = spi.BirimMiktar - spi.TeslimMiktar - spi.KapatilanMiktar;
            if (miktar <= 0)
                throw new ArgumentException("Sipariş Satırı kapatılmış");

            if (!string.IsNullOrEmpty(this.HesapKodu))
                if (this.HesapKodu != spi.Chk)
                    throw new ArgumentException("Sipariş Hesap Kodu uyumlu değil!!");
                else
                    this.HesapKodu = spi.Chk;

            if (string.IsNullOrEmpty(this.TeslimYeriKodu))
                this.TeslimYeriKodu = spi.TeslimChk;

            if (string.IsNullOrEmpty(this.DovizCinsi))
                this.DovizCinsi = spi.DovizCinsi;

            if (this.DovizKuru == 0 && spi.DovizKuru > 0)
                this.DovizKuru = spi.DovizKuru;

            


            KKP_STI sti = new KKP_STI(this.KynkEvrTip);
            sti.KaynakSiparisNo = spi.EvrakNo;
            sti.KaynakSiparisTarih = spi.Tarih;
            sti.KaynakSiraNo = spi.SiraNo;
            sti.SiparisSiraNo = spi.SiraNo;

            sti.VadeTarih = spi.VadeTarih;
            sti.Kredi_Donem_VadeTarih = spi.VadeTarih;
            sti.EvrakTarih = spi.Tarih;
            

            //0 İrsaliye; 1 Fatura; 2 Faturalanan İrsaliye; 3 İade Edilen Fatura; 
            //4 İade Edilen İrsaliye; 5 Faturalanan İade İrsaliyesi; 6 Sevk Edilen Sipariş; 7 Faturalanan Sipariş
            sti.IrsFat2 = 6; 
            

            sti.MalKodu = spi.MalKodu;
            sti.MalAdi = spi.MalAdi; 

            sti.BirimMiktar = miktar;
            sti.Birim = spi.Birim;
            sti.Depo = spi.Depo;
            sti.BirimFiyat = spi.BirimFiyat;
            sti.Fiyat = spi.Fiyat;
            sti.DvzBirimFiyat = spi.DvzBirimFiyat;

            sti.IskontoOran = spi.IskontoOran;
            sti.IskontoOran1 = spi.IskontoOran1;
            sti.IskontoOran2 = spi.IskontoOran2;
            sti.IskontoOran3 = spi.IskontoOran3;
            sti.IskontoOran4 = spi.IskontoOran4;
            sti.IskontoOran5 = spi.IskontoOran5;

            sti.DvzKurCinsi = spi.DvzKurCinsi;

            sti.Katsayi = spi.Katsayi;
            sti.Operator = spi.Operator;

            sti.FytListeNo = spi.FytListeNo;

            sti.KDVOran = spi.KDVOran;

            sti.Kod1 = spi.Kod1;
            sti.Kod2 = spi.Kod2;
            sti.Kod3 = spi.Kod3;
            sti.Kod4 = spi.Kod4;
            sti.Kod5 = spi.Kod5;
            sti.Kod6 = spi.Kod6;
            sti.Kod7 = spi.Kod7;
            sti.Kod8 = spi.Kod8;
            sti.Kod9 = spi.Kod9;
            sti.Kod10 = spi.Kod10;

            sti.TalepEden = spi.TalepEden;
            sti.TesisKodu = spi.TesisKodu;
            sti.TesisAdi = spi.TesisAdi;
            //sti.SatınAlmaci = spi.SatınAlmaci;

            sti.DvzTL = spi.DvzTL;
            sti.DovizCinsi = spi.DovizCinsi;
            sti.DovizKuru = spi.DovizKuru;

            sti.TalepNo = spi.TalepNo;
            sti.Unvan = spi.Unvan;
            
            sti.SPIInfo = spi;

            if (listSTI.Count > 0)
                sti.SiraNo = (short)(listSTI.Max(t => t.SiraNo) + (short)1);

            Ekle(sti);

            #region IRS Tablosu

            IrsSatiriOlustur(sti);

            //KKP_IRS irs = new KKP_IRS();
            //Data.DefaultValueSetIRS(irs, this.KynkEvrTip);

            //irs.IslemTur = sti.IslemTur;
            //irs.EvrakNo = sti.EvrakNo;
            //irs.Tarih = sti.Tarih;
            //irs.Chk = sti.Chk;
            //irs.KynkEvrakTip = sti.KynkEvrakTip;
            //irs.SiraNo = sti.SiraNo;
            //irs.IslemTip = sti.IslemTip;
            //irs.MalKodu = sti.MalKodu;
            //irs.Miktar = sti.BirimMiktar;


            //irs.IslemTur2 = spi.IslemTur;
            //irs.EvrakNo2 = spi.EvrakNo;
            //irs.Tarih2 = spi.Tarih;
            //irs.Chk2 = spi.Chk;
            //irs.KynkEvrakTip2 = spi.KynkEvrakTip;
            //irs.SiraNo2 = spi.SiraNo;
            //irs.IslemTip2 = spi.IslemTip;

            //irs.Depo = sti.Depo;
            //irs.Depo2 = spi.Depo;

            //sti.IRSInfo = irs;

            //irs.SiraNo = sti.SiraNo;
            #endregion

            return sti;
        }

        internal KKP_IRS IrsSatiriOlustur(KKP_STI sti)
        {
            KKP_IRS irs = new KKP_IRS();
            Data.DefaultValueSetIRS(irs, this.KynkEvrTip);

            irs.IslemTur = sti.IslemTur;
            irs.EvrakNo = sti.EvrakNo;
            irs.Tarih = sti.Tarih;
            irs.Chk = sti.Chk;
            irs.KynkEvrakTip = sti.KynkEvrakTip;
            irs.SiraNo = sti.SiraNo;
            irs.IslemTip = sti.IslemTip;
            irs.MalKodu = sti.MalKodu;
            irs.Miktar = sti.BirimMiktar;


            irs.IslemTur2 = sti.SPIInfo.IslemTur;
            irs.EvrakNo2 = sti.SPIInfo.EvrakNo;
            irs.Tarih2 = sti.SPIInfo.Tarih;
            irs.Chk2 = sti.SPIInfo.Chk;
            irs.KynkEvrakTip2 = sti.SPIInfo.KynkEvrakTip;
            irs.SiraNo2 = sti.SPIInfo.SiraNo;
            irs.IslemTip2 = sti.SPIInfo.IslemTip;

            irs.Depo = sti.Depo;
            irs.Depo2 = sti.SPIInfo.Depo;

            sti.IRSInfo = irs;

            irs.SiraNo = sti.SiraNo;

            return irs;
        }

        public void SiparisSatirlariEkle(List<KKP_SPI> spiList)
        {
            if (spiList == null)
                throw new ArgumentNullException("Liste Null");
            foreach (KKP_SPI spi in spiList)
            {
                SiparisSatiriEkle(spi);
            }
        }

        public bool SatirlarGuncellenmisMi()
        {
            return listSTI.Any(t => t.ChangedList.Count() > 0);
        }

        public void TumHesaplamalariYap()
        {
            FTDHesapla();
        }

        public void FTDHesapla()
        {
            FTDList.Clear();

            foreach (KKP_STI item in listSTI)
            {
                item.SatirHesapla();
            }

            short siraNo = 0;

            KKP_FTD ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.MalBedeli);
            decimal malBedeli = listSTI.Sum(t => t.Tutar);
            ftd.SiraNo = siraNo;
            siraNo++;
            ftd.Iskonto = malBedeli;
            ftd.DvzTL = (short)dvzTL;
            ftd.DovizCinsi = DovizCinsi;
            ftd.DovizKuru = DovizKuru;
            ftd.DovizTutar = listSTI.Sum(t => t.DovizTutar);
            FTDList.Add(ftd);

            decimal isk1Top = listSTI.Sum(t => t.Isk1Tutar);
            decimal isk2Top = listSTI.Sum(t => t.Isk2Tutar);
            decimal isk3Top = listSTI.Sum(t => t.Isk3Tutar);
            decimal isk4Top = listSTI.Sum(t => t.Isk4Tutar);
            decimal isk5Top = listSTI.Sum(t => t.Isk5Tutar);

            decimal isk1TopDvz = 0, isk2TopDvz = 0, isk3TopDvz = 0, isk4TopDvz = 0, isk5TopDvz = 0;
            if (DovizKuru > 0)
            {
                isk1TopDvz = listSTI.Sum(t => t.Isk1TutarDvz);
                isk2TopDvz = listSTI.Sum(t => t.Isk2TutarDvz);
                isk3TopDvz = listSTI.Sum(t => t.Isk3TutarDvz);
                isk4TopDvz = listSTI.Sum(t => t.Isk4TutarDvz);
                isk5TopDvz = listSTI.Sum(t => t.Isk5TutarDvz);
            }

            if (isk1Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu1);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk1Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk1TopDvz;
                FTDList.Add(ftd);
            }

            if (isk2Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu2);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk2Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk2TopDvz;
                FTDList.Add(ftd);
            }

            if (isk3Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu3);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk3Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk3TopDvz;
                FTDList.Add(ftd);
            }

            if (isk4Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu4);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk4Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk4TopDvz;
                FTDList.Add(ftd);
            }

            if (isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu5);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk5Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk5TopDvz;
                FTDList.Add(ftd);
            }

            if (isk1Top > 0 || isk2Top > 0 || isk3Top > 0 || isk4Top > 0 || isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.AraToplam);
                decimal araToplam = malBedeli - (isk1Top + isk2Top + isk3Top + isk4Top + isk5Top);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = araToplam;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                //if (dvzTL == DvzTL.Döviz)
                ftd.DovizTutar = DovizKuru > 0 ? Math.Round(araToplam / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                FTDList.Add(ftd);
            }




            decimal kdv18Top = 0, kdv8Top = 0;
            if (listSTI.Any(t => t.KDV > 0))
            {
                kdv18Top = listSTI.Where(t => t.KDVOran == 18).Sum(t => t.KDV);
                if (kdv18Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 18;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DovizCinsi;
                    ftd.DovizKuru = DovizKuru;
                    ftd.Iskonto = kdv18Top;
                    //if (dvzTL == DvzTL.Döviz)
                    ftd.DovizTutar = DovizKuru > 0 ? Math.Round(kdv18Top / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
                kdv8Top = listSTI.Where(t => t.KDVOran == 8).Sum(t => t.KDV);
                if (kdv8Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 8;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DovizCinsi;
                    ftd.DovizKuru = DovizKuru;
                    ftd.Iskonto = kdv8Top;
                    //if (dvzTL == DvzTL.Döviz)
                    ftd.DovizTutar = DovizKuru > 0 ? Math.Round(kdv8Top / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
            }

            //if (FTDList.Count > 1) ///MalBedelinden başka iskonto ve kdv varsa Genel toplam da eklenecek. 
            ///Yoksa MalBedeli tek başına da kalabilir
            //{
            decimal iskTop = isk1Top + isk2Top + isk3Top + isk4Top + isk5Top;
            ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.Toplam);
            ftd.SiraNo = siraNo;
            ftd.Iskonto = malBedeli - iskTop + kdv8Top + kdv18Top;
            ftd.DvzTL = (short)dvzTL;
            ftd.DovizCinsi = DovizCinsi;
            ftd.DovizKuru = DovizKuru;
            ftd.DovizTutar = DovizKuru > 0 ? Math.Round(ftd.Iskonto / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
            FTDList.Add(ftd);
            //}

            foreach (var item in FTDList)
            {
                Duzelt(item);
            }
        }

        public void FTDHesapla(bool tip)
        {
            FTDList.Clear();

            foreach (KKP_STI item in listSTI)
            {
                item.SatirHesapla();
            }

            short siraNo = 0;

            KKP_FTD ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.MalBedeli);
            decimal malBedeli = listSTI.Sum(t => t.Tutar);
            ftd.SiraNo = siraNo;
            siraNo++;
            ftd.Iskonto = malBedeli;
            ftd.DvzTL = (short)dvzTL;
            ftd.DovizCinsi = DovizCinsi;
            ftd.DovizKuru = DovizKuru;
            //ftd.DovizTutar = listSTI.Sum(t => t.DovizTutar);
            ftd.DovizTutar = DovizKuru>0 ? (malBedeli / DovizKuru):0;
            FTDList.Add(ftd);

            decimal isk1Top = listSTI.Sum(t => t.Isk1Tutar);
            decimal isk2Top = listSTI.Sum(t => t.Isk2Tutar);
            decimal isk3Top = listSTI.Sum(t => t.Isk3Tutar);
            decimal isk4Top = listSTI.Sum(t => t.Isk4Tutar);
            decimal isk5Top = listSTI.Sum(t => t.Isk5Tutar);

            decimal isk1TopDvz = 0, isk2TopDvz = 0, isk3TopDvz = 0, isk4TopDvz = 0, isk5TopDvz = 0;
            if (DovizKuru > 0)
            {
                isk1TopDvz = listSTI.Sum(t => t.Isk1TutarDvz);
                isk2TopDvz = listSTI.Sum(t => t.Isk2TutarDvz);
                isk3TopDvz = listSTI.Sum(t => t.Isk3TutarDvz);
                isk4TopDvz = listSTI.Sum(t => t.Isk4TutarDvz);
                isk5TopDvz = listSTI.Sum(t => t.Isk5TutarDvz);
            }

            if (isk1Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu1);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk1Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk1TopDvz;
                FTDList.Add(ftd);
            }

            if (isk2Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu2);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk2Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk2TopDvz;
                FTDList.Add(ftd);
            }

            if (isk3Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu3);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk3Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk3TopDvz;
                FTDList.Add(ftd);
            }

            if (isk4Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu4);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk4Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk4TopDvz;
                FTDList.Add(ftd);
            }

            if (isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KalemlerdeOranIskontosu5);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = isk5Top;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                ftd.DovizTutar = isk5TopDvz;
                FTDList.Add(ftd);
            }

            if (isk1Top > 0 || isk2Top > 0 || isk3Top > 0 || isk4Top > 0 || isk5Top > 0)
            {
                ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.AraToplam);
                decimal araToplam = malBedeli - (isk1Top + isk2Top + isk3Top + isk4Top + isk5Top);
                ftd.SiraNo = siraNo;
                siraNo++;
                ftd.Iskonto = araToplam;
                ftd.DvzTL = (short)dvzTL;
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
                //if (dvzTL == DvzTL.Döviz)
                ftd.DovizTutar = DovizKuru > 0 ? Math.Round(araToplam / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                FTDList.Add(ftd);
            }




            decimal kdv18Top = 0, kdv8Top = 0;
            if (listSTI.Any(t => t.KDV > 0))
            {
                kdv18Top = listSTI.Where(t => t.KDVOran == 18).Sum(t => t.KDV);
                if (kdv18Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 18;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DovizCinsi;
                    ftd.DovizKuru = DovizKuru;
                    ftd.Iskonto = kdv18Top;
                    //if (dvzTL == DvzTL.Döviz)
                    ftd.DovizTutar = DovizKuru > 0 ? Math.Round(kdv18Top / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
                kdv8Top = listSTI.Where(t => t.KDVOran == 8).Sum(t => t.KDV);
                if (kdv8Top > 0)
                {
                    ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.KDV);
                    ftd.SiraNo = siraNo;
                    siraNo++;
                    ftd.IskontoOran = 8;
                    ftd.DvzTL = (short)dvzTL;
                    ftd.DovizCinsi = DovizCinsi;
                    ftd.DovizKuru = DovizKuru;
                    ftd.Iskonto = kdv8Top;
                    //if (dvzTL == DvzTL.Döviz)
                    ftd.DovizTutar = DovizKuru > 0 ? Math.Round(kdv8Top / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
                    FTDList.Add(ftd);
                }
            }

            //if (FTDList.Count > 1) ///MalBedelinden başka iskonto ve kdv varsa Genel toplam da eklenecek. 
            ///Yoksa MalBedeli tek başına da kalabilir
            //{
            decimal iskTop = isk1Top + isk2Top + isk3Top + isk4Top + isk5Top;
            ftd = FTDTable.NewFTD((KKPKynkEvrakTip)(short)IrsaliyeTip, FTDTable.SatirTip.Toplam);
            ftd.SiraNo = siraNo;
            ftd.Iskonto = malBedeli - iskTop + kdv8Top + kdv18Top;
            ftd.DvzTL = (short)dvzTL;
            ftd.DovizCinsi = DovizCinsi;
            ftd.DovizKuru = DovizKuru;
            ftd.DovizTutar = DovizKuru > 0 ? Math.Round(ftd.Iskonto / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0;
            FTDList.Add(ftd);
            //}

            foreach (var item in FTDList)
            {
                Duzelt(item);
            }
        }

        internal void SetMfk(KKP_MFK mfk)
        {
            MFK = mfk;
        }

        #region Private Functions
        private void DuzeltHepsi()
        {
            foreach (KKP_STI item in listSTI)
            {
                Duzelt(item);
                if (item.IRSInfo != null)
                    Duzelt(item.IRSInfo);
            }
            foreach (KKP_FTD item in FTDList)
            {
                Duzelt(item);
            }

            DuzeltMFK();
        }

        private void Duzelt(KKP_STI sti)
        {
            if (fromLoad)
                return;

            sti.Chk = HesapKodu;
            sti.IslemTur = (short)(IrsaliyeTip == KKPIrsaliyeTip.SatisIrsaliyesi ? 1 : 0);
            sti.KynkEvrakTip = (short)IrsaliyeTip;
            sti.AnaEvrakTip = (short)IrsaliyeTip;
            sti.KayitKaynak = IrsaliyeTip == KKPIrsaliyeTip.SatisIrsaliyesi ? (short)KKPKayitKaynak.SatisIrsaliye : (short)KKPKayitKaynak.AlimIrsaliye;
            sti.DegisKaynak = sti.KayitKaynak;


            sti.EvrakNo = EvrakNo;
            sti.Tarih = Convert.ToInt32(Tarih.Date.ToOADate());
            sti.IslemTip = (short)IslemTip;
            sti.TeslimChk = TeslimYeriKodu;
            //sti.DvzTL = (short)dvzTL;

            if (dvzTL == KKPDvzTL.YerelPara)
            {
                sti.DovizCinsi = "";
                sti.DovizKuru = 0;
            }
            //else
            //{
            //    sti.DovizCinsi = DovizCinsi;
            //    sti.DovizKuru = DovizKuru;
            //}


        }

        private void Duzelt(KKP_FTD ftd)
        {
            if (fromLoad)
                return;

            ftd.HesapKodu = HesapKodu;
            ftd.TeslimChk = teslimYeriKodu;
            ftd.EvrakNo = EvrakNo;
            ftd.Tarih = Convert.ToInt32(Tarih.Date.ToOADate());
            ftd.FormBaBsTarih = ftd.Tarih;
            ftd.DvzTL = (short)dvzTL;
            if (dvzTL == KKPDvzTL.YerelPara)
            {
                ftd.DovizCinsi = "";
                ftd.DovizKuru = 0;
            }
            else
            {
                ftd.DovizCinsi = DovizCinsi;
                ftd.DovizKuru = DovizKuru;
            }
        }

        private void Duzelt(KKP_IRS irs)
        {
            irs.EvrakNo = EvrakNo;
            irs.Tarih = Convert.ToInt32(Tarih.Date.ToOADate());
            irs.Chk = HesapKodu;
            irs.IslemTip = (short)IslemTip;
        }

        private void DuzeltMFK()
        {
            if (fromLoad)
                return;

            MFK.EvrakNo = evrakNo;
            MFK.HesapKod = HesapKodu;
            MFK.EvrakTarih = Convert.ToInt32(Tarih.Date.ToOADate());
            MFK.Depo = "";
            if (listSTI.Count > 0)
                MFK.Depo = listSTI[0].Depo;
        }
        #endregion
    }

    public class KKPEvrakMuhFis
    {
        #region fields
        
        DateTime tarih;
        short fisTip;
        decimal fisNo;
        int maddeNo;
        string mrkHKod;
        string kynkEvrakNo;
        DateTime kynkEvrakTarih;

        
        
        #endregion fields son

        #region properties
        
        public DateTime Tarih
        {
            get { return tarih; }
            set { tarih = value; DuzeltHepsi(); }
        }

        public short FisTip
        {
            get { return fisTip; }
            set { fisTip = value; DuzeltHepsi(); }
        }
      
        public decimal FisNo
        {
            get { return fisNo; }
            set { fisNo = value; DuzeltHepsi(); }
        }

        public int MaddeNo
        {
            get { return maddeNo; }
            set { maddeNo = value; DuzeltHepsi(); }
        }

        public string MrkHKod
        {
            get { return mrkHKod; }
            set { mrkHKod = value; DuzeltHepsi(); }
        }

        public string KynkEvrakNo
        {
            get { return kynkEvrakNo; }
            set { kynkEvrakNo = value; DuzeltHepsi(); }
        }

        public DateTime KynkEvrakTarih
        {
            get { return kynkEvrakTarih; }
            set { kynkEvrakTarih = value; DuzeltHepsi(); }
        }
        
        #endregion properties son

        public bool GuncellemeModu { get; set; }

        public List<KKP_MHI> MHIList { get; set; }
        public List<KKP_ENT> ENTList { get; set; }

        public KKPEvrakMuhFis()
        {
            MHIList = new List<KKP_MHI>();
            ENTList = new List<KKP_ENT>();
        }

        void DuzeltHepsi()
        {
            foreach (var item in MHIList)
            {
                item.Tarih = Convert.ToInt32(Tarih.ToOADate());
                item.Tarih2 = Convert.ToInt32(Tarih.ToOADate());

                item.Fistip = FisTip;
                item.FisNo = FisNo;
                item.MaddeNo = MaddeNo;
                item.MrkHKod = MrkHKod;
                item.KaynakEvrakNo = KynkEvrakNo;
                item.KaynakEvrakTarih = Convert.ToInt32(KynkEvrakTarih.ToOADate());
            }

            foreach (var item in ENTList)
            {
                item.EvrakNo = KynkEvrakNo;
                item.Tarih = Convert.ToInt32(Tarih.ToOADate());                
                item.FisTip = FisTip;
                item.FisNo = FisNo;
                item.FisMaddeNo = MaddeNo;

            }

        }

    }


    #endregion Evraklar

}
