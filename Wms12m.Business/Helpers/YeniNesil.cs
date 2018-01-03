using DevHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Wms12m.Entity;

namespace Wms12m
{
    public class YeniNesil
    {
        string ConStr { get; set; }
        string SirketKodu { get; set; }
        List<STIEvrakBilgi> EvrakNoList;
        List<HesapItem> HesapList;
        List<STK005> FaturaSatirlari;
        /// <summary>
        /// yeni finsat
        /// </summary>
        public YeniNesil(string conStr, string sirketKodu)
        {
            ConStr = conStr;
            SirketKodu = sirketKodu;
        }

        public bool OndalikAyiracVirgulMu()
        {
            var x = 1.1;
            var sonuc = x.ToString().Replace("1", "");

            if (sonuc == ",") return true;
            else return false;
        }

        public void SatisIadeOnay(SatisIadeOnay SIOnay)
        {
            var Sqlexper = new SqlExper(ConStr, SirketKodu);
            var Sorgu = string.Empty;
            var saat = DateTime.Now.ToString("HHmmss");
            var tarih = DateTime.Today.ToOADateInt();
            if (SIOnay.Onay)
            {
                Sorgu = string.Format(@"
                UPDATE STI1 
                SET STI1.STK005_Kod10='Onaylandı', STI1.STK005_DegistirenKodu='{1}', STI1.STK005_DegistirenTarih={2}, STI1.STK005_DegistirenSaat='{3}',
                    STI1.STK005_CariHesapKodu=STI2.STK005_CariHesapKodu, STI1.STK005_BirimFiyati=STI2.STK005_BirimFiyati, 
	                STI1.STK005_Tutari=(STI1.STK005_Miktari * STI2.STK005_BirimFiyati), STI1.STK005_DovizCinsi=STI2.STK005_DovizCinsi
                FROM  YNS{0}.YNS{0}.STK005(NOLOCK) STI1
                INNER JOIN YNS{0}.YNS{0}.STK005(NOLOCK) STI2 ON STI1.STK005_Kod9=STI2.STK005_EvrakSeriNo AND 
                      CAST(STI1.STK005_Kod11 as INT)=STI2.STK005_IslemTarihi AND  STI2.STK005_EvrakTipi=11 AND STI1.STK005_MalKodu=STI2.STK005_MalKodu

                WHERE STI1.STK005_EvrakTipi=99 AND STI1.STK005_IslemTipi=2 AND STI1.STK005_GC=0 AND STI1.STK005_Kod11>0 AND STI1.STK005_Kod9<>'' AND
                      STI1.STK005_Kod10='Onay Bekliyor' AND SUBSTRING(STI1.STK005_Not5,1,8)='AndMobil' AND 
	                  STI1.STK005_EvrakSeriNo='{4}' AND STI1.STK005_IslemTarihi={5} ",
                SirketKodu, SIOnay.Kaydeden, tarih, saat, SIOnay.IadeNo, SIOnay.IadeTarih);
            }
            else
            {
                Sorgu = string.Format(@"
                UPDATE YNS{0}.YNS{0}.STK005 SET STK005_Kod10='Reddedildi', STK005_DegistirenKodu='{1}', STK005_DegistirenTarih={2}, STK005_DegistirenSaat='{3}'
                WHERE STK005_EvrakTipi=99 AND STK005_IslemTipi=2 AND STK005_GC=0 AND STK005_Kod11>0 AND STK005_Kod9<>'' AND
                      STK005_Kod10='Onay Bekliyor' AND SUBSTRING(STK005_Not5,1,8)='AndMobil' AND 
	                  STK005_EvrakSeriNo='{4}' AND STK005_IslemTarihi={5} ",
                SirketKodu, SIOnay.Kaydeden, tarih, saat, SIOnay.IadeNo, SIOnay.IadeTarih);
            }

            Sqlexper.Komut(Sorgu);
            Sqlexper.AcceptChanges();
        }

        /// <summary>
        /// fatura kaydetme işllemleri
        /// </summary>
        public Result FaturaKaydet(List<SepetUrun> pSepetUrunleri)
        {
            var OndalikAyiracVirgul = OndalikAyiracVirgulMu();

            FaturaSatirlari = new List<STK005>();

            EvrakNolari_YukleAyarla();
            HesapListesini_YukleAyarla(pSepetUrunleri[0].HesapKodu);

            var faturaSiraNo = EvrakNoList[0].SiraNo;
            var siparisSiraNo = EvrakNoList[1].SiraNo;
            var saat = DateTime.Now.ToString("HHmmss"); ///O anın saatini döner 154010 gibi
            var Sqlexper = new SqlExper(ConStr, SirketKodu);
            foreach (SepetUrun item in pSepetUrunleri)
            {
                if (OndalikAyiracVirgul)
                {
                    item.Miktar = item.Miktar.Replace(".", ",");
                    item.Fiyat = item.Fiyat.Replace(".", ",");
                }

                var hesapKodu = "";
                var hesap = HesapList.Find(x => x.ParaBirimi.ToUpper() == item.ParaCinsi.ToUpper());
                if (hesap.IsNullEmpty())
                    throw new Exception("Hesapla ilgili stoğun döviz cinsleri uyuşmamaktadır !");
                hesapKodu = hesap.HesapKodu;
                faturaSiraNo++;
                var fatura = new STK005();
                fatura.DefaultValueSet();
                fatura.STK005_MalKodu = item.UrunKodu;
                fatura.STK005_IslemTarihi = DateTime.Today.ToOADate().ToInt32();
                fatura.STK005_GC = 1;
                fatura.STK005_CariHesapKodu = hesapKodu;
                fatura.STK005_TeslimCHKodu = hesapKodu;
                fatura.STK005_EvrakSeriNo = EvrakNoList[0].EvrakNo;
                fatura.STK005_Miktari = item.Miktar.ToDecimal();
                fatura.STK005_BirimFiyati = item.Fiyat.ToDecimal();
                fatura.STK005_Tutari = item.Miktar.ToDecimal() * item.Fiyat.ToDecimal();
                fatura.STK005_IslemTipi = 2;
                fatura.STK005_EvrakTipi = 11;
                fatura.STK005_KDVOranFlag = 4;
                fatura.STK005_FaturaDurumu = 1;
                fatura.STK005_ParaBirimi = 1;
                fatura.STK005_SEQNo = faturaSiraNo;
                fatura.STK005_IptalDurumu = 1;
                fatura.STK005_AsilEvrakTarihi = fatura.STK005_IslemTarihi;
                fatura.STK005_SevkTarihi = fatura.STK005_IslemTarihi;
                fatura.STK005_VadeTarihi = fatura.STK005_IslemTarihi + 60;  ///Default 60 gün vade 
                fatura.STK005_DovizCinsi = item.ParaCinsi;
                fatura.STK005_Depo = item.Depo;
                fatura.STK005_RafKodu = item.Depo + "-A0";
                fatura.STK005_MaliyetMuhasebelesmeSekli = 1;
                fatura.STK005_Not5 = "WMSOnay - " + item.KullaniciKodu;   ///Log amaçlı bilgi

                fatura.STK005_GirenKaynak = "Y6018";
                fatura.STK005_GirenSurum = "7.1.00";
                fatura.STK005_GirenKodu = item.Kaydeden;
                fatura.STK005_GirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                fatura.STK005_GirenSaat = saat;
                fatura.STK005_DegistirenKaynak = "Y6018";
                fatura.STK005_DegistirenSurum = "7.1.00";
                fatura.STK005_DegistirenKodu = item.KullaniciKodu;
                fatura.STK005_DegistirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                fatura.STK005_DegistirenSaat = saat;

                fatura.Adres1 = item.Adres1;
                fatura.Adres2 = item.Adres2;
                fatura.Adres3 = item.Adres3;
                fatura.Aciklama1 = item.Aciklama1;
                fatura.Aciklama2 = item.Aciklama2;
                fatura.Aciklama3 = item.Aciklama3;

                FaturaSatirlari.Add(fatura);
            }

            foreach (var item in FaturaSatirlari)
            {
                Sqlexper.Insert(item);
                var stk = new STK004();
                stk.pk_STK004_MalKodu = item.STK005_MalKodu;
                stk.STK004_DegistirenKodu = item.STK005_DegistirenKodu;
                stk.STK004_DegistirenSaat = item.STK005_DegistirenSaat;
                stk.STK004_DegistirenTarih = item.STK005_DegistirenTarih;

                stk.SetAdd(STK004E.STK004_CikisMiktari, STK004E.STK004_CikisMiktari, SetIslem.Arti, item.STK005_Miktari.ToDot());
                stk.SetAdd(STK004E.STK004_CikisTutari, STK004E.STK004_CikisTutari, SetIslem.Arti, item.STK005_Tutari.ToDot());
                stk.STK004_SonCikisTarihi = item.STK005_DegistirenTarih;
                Sqlexper.Update(stk);
            }

            foreach (var grupItem in FaturaSatirlari.GroupBy(x => x.STK005_CariHesapKodu))
            {
                var firstItem = grupItem.First();

                #region  FTD INSERT

                var fMal = new CAR005();
                fMal.DefaultValueSet();
                fMal.CAR005_FaturaNo = firstItem.STK005_EvrakSeriNo;
                fMal.CAR005_FaturaTarihi = firstItem.STK005_IslemTarihi;
                fMal.CAR005_CHKodu = firstItem.STK005_CariHesapKodu;
                fMal.CAR005_TeslimCHKodu = firstItem.STK005_TeslimCHKodu;
                fMal.CAR005_ParaBirimi = firstItem.STK005_ParaBirimi;
                fMal.CAR005_TeslimCHKodu = firstItem.STK005_TeslimCHKodu;
                fMal.CAR005_IptalDurumu = firstItem.STK005_IptalDurumu;

                fMal.CAR005_SatirTipi = "G";
                fMal.CAR005_SatirNo = 2;

                fMal.CAR005_Tutar = grupItem.Sum(x => x.STK005_Tutari);
                fMal.CAR005_DovizCinsi = firstItem.STK005_DovizCinsi;

                fMal.CAR005_CariIslemTipi = 4;
                fMal.CAR005_Secenek = 1;
                fMal.CAR005_BA = "B";

                fMal.CAR005_TevkIslemTuru = null;
                fMal.CAR005_EFaturaDurumu = null;
                fMal.CAR005_EFaturaDonemBas = null;
                fMal.CAR005_EFaturaDonemBit = null;
                fMal.CAR005_EFaturaSure = null;
                fMal.CAR005_EFaturaSureBirimi = null;
                fMal.CAR005_TutarAlanDegeri = null;
                fMal.CAR005_AliciSiparisTarihi = null;
                fMal.CAR005_EArsivFaturaTipi = null;
                fMal.CAR005_EArsivFaturaDurumu = null;
                fMal.CAR005_EArsivFaturaTeslimSekli = null;

                Sqlexper.Insert(fMal);

                short satirNo = 3;
                var fSevkAciklama = new CAR005();
                fSevkAciklama.Set(fMal);
                fSevkAciklama.CAR005_SatirTipi = "A";
                fSevkAciklama.CAR005_Tutar = 0;
                fSevkAciklama.CAR005_Oran = 0;
                fSevkAciklama.CAR005_ParaBirimi = 0;
                fSevkAciklama.CAR005_DovizCinsi = "";
                fSevkAciklama.CAR005_Secenek = 5;

                if (firstItem.Adres1.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Adres1.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Adres1.Substring(1, firstItem.Adres1.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                if (firstItem.Adres2.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Adres2.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Adres2.Substring(1, firstItem.Adres2.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                if (firstItem.Adres3.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Adres3.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Adres3.Substring(1, firstItem.Adres3.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                if (firstItem.Aciklama1.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Aciklama1.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Aciklama1.Substring(1, firstItem.Aciklama1.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                if (firstItem.Aciklama2.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Aciklama2.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Aciklama2.Substring(1, firstItem.Aciklama2.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                if (firstItem.Aciklama3.IsNotNullEmpty())
                {
                    fSevkAciklama.CAR005_SatirNo = satirNo;
                    fSevkAciklama.CAR005_Filler = firstItem.Aciklama3.Substring(0, 1);
                    fSevkAciklama.CAR005_SatirAciklama = firstItem.Aciklama3.Substring(1, firstItem.Aciklama3.Length - 1);
                    Sqlexper.Insert(fSevkAciklama);
                    satirNo++;
                }

                #endregion   FTD INSERT END

                #region  CHI INSERT

                var siraNo = FaturaSatirlari[0].STK005_SEQNo.ToInt32();

                var chiMal = new CAR003();
                chiMal.DefaultValueSet();
                chiMal.CAR003_HesapKodu = firstItem.STK005_CariHesapKodu;
                chiMal.CAR003_Tarih = firstItem.STK005_IslemTarihi;
                chiMal.CAR003_EvrakSeriNo = firstItem.STK005_EvrakSeriNo;

                chiMal.CAR003_BA = (byte)0;
                chiMal.CAR003_IslemTipi = 4;
                chiMal.CAR003_EvrakTipi = 11;

                chiMal.CAR003_Tutar = grupItem.Sum(x => x.STK005_Tutari);
                chiMal.CAR003_KarsiEvrakSeriNo = firstItem.STK005_IadeFaturaNo;

                chiMal.CAR003_SEQNo = siraNo;
                chiMal.CAR003_VadeTarihi = firstItem.STK005_VadeTarihi;
                chiMal.CAR003_KDVOrani = 4;
                chiMal.CAR003_ParaBirimi = firstItem.STK005_ParaBirimi;
                chiMal.CAR003_IptalDurumu = 1;
                chiMal.CAR003_AsilEvrakTarihi = firstItem.STK005_AsilEvrakTarihi;
                chiMal.CAR003_MuhasebelesmeSekli = 1;
                chiMal.CAR003_Aciklama = "Faturamız";
                chiMal.CAR003_EvrakSayisi = 1;
                chiMal.CAR003_DovizCinsi = firstItem.STK005_DovizCinsi;

                chiMal.CAR003_YevmiyeEvrakTip = null;
                chiMal.CAR003_EFaturaDurumu = null;
                chiMal.CAR003_EArsivFaturaTipi = null;
                chiMal.CAR003_EArsivFaturaTeslimSekli = null;
                chiMal.CAR003_EArsivFaturaDurumu = null;
                chiMal.CAR003_YOKCZRaporuNo = null;
                chiMal.CAR003_YOKCBelgeTipi = null;
                chiMal.CAR003_YOKCBilgiFisiTipi = null;
                chiMal.CAR003_YOKCFisNo = null;
                chiMal.CAR003_YOKCFisSaat = null;
                chiMal.CAR003_YOKCDuzenlemeTip = null;

                chiMal.CAR003_GirenKaynak = "AN001";
                chiMal.CAR003_GirenSurum = "7.1.00";
                chiMal.CAR003_GirenKodu = firstItem.STK005_DegistirenKodu;
                chiMal.CAR003_GirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                chiMal.CAR003_GirenSaat = saat;
                chiMal.CAR003_DegistirenKaynak = "AN001";
                chiMal.CAR003_DegistirenSurum = "7.1.00";
                chiMal.CAR003_DegistirenKodu = firstItem.STK005_DegistirenKodu;
                chiMal.CAR003_DegistirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                chiMal.CAR003_DegistirenSaat = saat;

                Sqlexper.Insert(chiMal);

                #endregion  CHI INSERT END

                #region  CHK UPDATE

                var chk = new CAR002();
                chk.pk_CAR002_HesapKodu = firstItem.STK005_CariHesapKodu;
                chk.SetAdd(CAR002E.CAR002_CiroBorc, CAR002E.CAR002_CiroBorc, SetIslem.Arti, chiMal.CAR003_Tutar.ToDot());
                chk.CAR002_SonBorcTarihi = firstItem.STK005_IslemTarihi;
                Sqlexper.Update(chk);

                #endregion   CHK UPDATE END              
            }

            Sqlexper.AcceptChanges();
            return new Result();
        }

        /// <summary>
        /// Depo Transfer kayıt işlemleri
        /// </summary>

        public Result DepoTransferKaydet(List<DepoTran> TransferUrunleri)
        {
            var OndalikAyiracVirgul = OndalikAyiracVirgulMu();

            string evrakNo = "", yeniEvrakNo = "";
            var siraNo = 0;

            var evrakBilgi = new STIEvrakBilgi();
            var Sqlexper = new SqlExper(ConStr, SirketKodu);
            var veriList = Sqlexper.SelectList<STIEvrakBilgi>(string.Format(@"
            SELECT TOP 1 STK005_EvrakSeriNo AS EvrakNo,    
                    STK005_SEQNo as SiraNo  
            FROM YNS{0}.YNS{0}.STK005 (NOLOCK)
            WHERE STK005_EvrakTipi=51 AND STK005_EvrakSeriNo Like 'T%' 
            ORDER BY STK005_Row_ID DESC", SirketKodu)).ToList();

            if (veriList.IsNull() || veriList.Count == 0)
            {
                evrakNo = "T0000000";
                siraNo = 0;
            }
            else
            {
                evrakNo = veriList[0].EvrakNo;
                siraNo = veriList[0].SiraNo;
            }

            evrakNo = evrakNo.Remove(0, 1);
            var no = evrakNo.ToInt32();
            yeniEvrakNo = EvrakNoOlustur(8, "T", no);

            var saat = DateTime.Now.ToString("HHmmss"); ///O anın saatini döner 154010 gibi
            var tarih = DateTime.Today.ToOADateInt();

            foreach (DepoTran item in TransferUrunleri)
            {
                siraNo++;
                var stiItem = new STK005();
                stiItem.DefaultValueSet();
                stiItem.STK005_EvrakSeriNo = yeniEvrakNo;
                stiItem.STK005_MalKodu = item.MalKodu;
                stiItem.STK005_Depo = item.CikisDepo;
                stiItem.STK005_Birim = item.Birim;
                stiItem.STK005_Miktari = item.Miktar;
                stiItem.STK005_Tarih2 = item.MiatTarih;//miat tarihi
                stiItem.STK005_GC = 1;

                stiItem.STK005_ParaBirimi = 1;
                stiItem.STK005_SEQNo = siraNo;

                stiItem.STK005_IslemTipi = 7;
                stiItem.STK005_EvrakTipi = 51;
                stiItem.STK005_MuhasebelesmeDurumu = 0;
                stiItem.STK005_IptalDurumu = 1;
                stiItem.STK005_IslemTarihi = tarih;
                stiItem.STK005_AsilEvrakTarihi = tarih;

                stiItem.STK005_GirenKaynak = "Y6018";
                stiItem.STK005_GirenSurum = "7.1.00";
                stiItem.STK005_GirenKodu = item.KullaniciKodu;
                stiItem.STK005_GirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                stiItem.STK005_GirenSaat = DateTime.Now.ToString("HHmmss"); ///O anın saatini döner 154010 gibi
                stiItem.STK005_DegistirenKaynak = "Y6018";
                stiItem.STK005_DegistirenSurum = "7.1.00";
                stiItem.STK005_DegistirenKodu = item.KullaniciKodu;
                stiItem.STK005_DegistirenTarih = DateTime.Today.Date.ToOADate().ToInt32();
                stiItem.STK005_DegistirenSaat = DateTime.Now.ToString("HHmmss"); ///O anın saatini döner             
                Sqlexper.Insert(stiItem);

                siraNo++;
                stiItem.STK005_SEQNo = siraNo;
                stiItem.STK005_Depo = item.GirisDepo;
                stiItem.STK005_GC = 0;
                Sqlexper.Insert(stiItem);
            }

            Sqlexper.AcceptChanges();
            return new Result();
        }


        public Result TahsilatKaydet(frmOnayTahsilatList tahsilatItem, string user)
        {
            SqlExper Sqlexper = new SqlExper(ConStr, SirketKodu);

            int seriNo = 1;
            if (tahsilatItem.IslemTuru == "Tahsilat")
                seriNo = 1;
            else if (tahsilatItem.IslemTuru == "İskonto")
                seriNo = 3;


            List<string> EvrakBilgi = Sqlexper.SelectList<string>(string.Format(@"
            SELECT EvrakNo FROM YNS{{0}}.YNS{{0}}.EvrakIni(NOLOCK) WHERE SeriNo={0}
            UNION ALL
            SELECT CAST(MAX(CAR003_SEQNo) as VarChar) EvrakNo FROM YNS{{0}}.YNS{{0}}.CAR003(NOLOCK)",seriNo));

            string EvrakNo = EvrakBilgi[0];
            int SeqNo = EvrakBilgi[1].ToInt32();


            int tarih = tahsilatItem.Tarih.ToDatetime().ToOADateInt();
            DovizKurlari DovizKurlari = Sqlexper.SelectFirst<DovizKurlari>(string.Format(DovizKurlari.Sorgu, tarih, tarih - 1, tarih - 2));

            if(DovizKurlari.IsNull() || DovizKurlari.USD<=0 || DovizKurlari.EUR<=0)
            {
                throw new Exception("İlgili tarihle alakalı geçerli(0 dan büyük) döviz kuru bulunamadı ! \n DVZ002 tablosunu kontrol ediniz.");
            }

            string kayitSaat = DateTime.Now.ToString("HHmmss"); ///O anın saatini döner 


            if (tahsilatItem.IslemTuru == "Tahsilat")
            {
                if (tahsilatItem.KapatilanTL > 0)
                {
                    Sqlexper.Komut(string.Format("EXEC YNS{0}.dbo.FaturalariKapat 0,'{1}','{2}',{3},{4},{5},'{6}','{7}','{8}',{9},'{10}','{11}',{12}",
                                    SirketKodu, tahsilatItem.HesapKodu, EvrakNo, tahsilatItem.KapatilanTL.ToDot(), tahsilatItem.KapatilanTL.ToDot(),
                                    tarih, EvrakNo, tahsilatItem.OdemeTuru, "TL", SeqNo + 1, kayitSaat, user, 0.0));
                }

                if (tahsilatItem.KapatilanUSD > 0)
                {
                    Sqlexper.Komut(string.Format("EXEC YNS{0}.dbo.FaturalariKapat 0,'{1}','{2}',{3},{4},{5},'{6}','{7}','{8}',{9},'{10}','{11}',{12}",
                                    SirketKodu, tahsilatItem.HesapKodu + "$", EvrakNo, tahsilatItem.KapatilanUSD.ToDot(), (tahsilatItem.KapatilanUSD * DovizKurlari.USD).ToDot(),
                                    tarih, EvrakNo, tahsilatItem.OdemeTuru, "USD", SeqNo + 1, kayitSaat, user, DovizKurlari.USD.ToDot()));
                }

                if (tahsilatItem.KapatilanEUR > 0)
                {
                    Sqlexper.Komut(string.Format("EXEC YNS{0}.dbo.FaturalariKapat 0,'{1}','{2}',{3},{4},{5},'{6}','{7}','{8}',{9},'{10}','{11}',{12}",
                                    SirketKodu, tahsilatItem.HesapKodu + "EU", EvrakNo, tahsilatItem.KapatilanEUR.ToDot(), (tahsilatItem.KapatilanEUR * DovizKurlari.EUR).ToDot(),
                                    tarih, EvrakNo, tahsilatItem.OdemeTuru, "EUR", SeqNo + 1, kayitSaat, user, DovizKurlari.EUR.ToDot()));
                }

            }
            else if(tahsilatItem.IslemTuru == "İskonto")
            {
                string paraCinsi = "TL";
                decimal islemTutar = 0, islemTutarTL = 0, dovizKuru = 0;

                islemTutar = tahsilatItem.Tutar;
                islemTutarTL = islemTutar;
                if(tahsilatItem.DovizCinsi=="USD")
                {
                    paraCinsi = "USD";
                    islemTutarTL = islemTutar * DovizKurlari.USD;
                    dovizKuru = DovizKurlari.USD;
                }
                else if (tahsilatItem.DovizCinsi == "EUR")
                {
                    paraCinsi = "EUR";
                    islemTutarTL = islemTutar * DovizKurlari.EUR;
                    dovizKuru = DovizKurlari.EUR;
                }

                Sqlexper.Komut(string.Format("EXEC YNS{0}.dbo.FaturalariKapat 1,'{1}','{2}',{3},{4},{5},'{6}','{7}','{8}',{9},'{10}','{11}',{12}",
                                SirketKodu, tahsilatItem.HesapKodu, EvrakNo, islemTutar.ToDot(), islemTutarTL.ToDot(),
                                tarih, EvrakNo, tahsilatItem.OdemeTuru, paraCinsi, SeqNo + 1, kayitSaat, user, dovizKuru));

            }


            string seri = EvrakNo.Substring(0, 2);
            EvrakNo = EvrakNo.Remove(0, 2);
            EvrakNo = EvrakNoOlustur(8, seri, EvrakNo.ToInt32());
            EvrakIni evrakIni = new EvrakIni
            {
                pk_SeriNo = seriNo,
                EvrakNo = EvrakNo
            };
            Sqlexper.Update(evrakIni);



            Sqlexper.AcceptChanges();

            return new Result();
        }

        /// <summary>
        /// evrak no oluştur
        /// </summary>
        string EvrakNoOlustur(int evrakKarakterSayisi, string seri, int no)
        {
            if (no.ToString().Length > evrakKarakterSayisi - seri.Length)
                throw new Exception("Evrak numarasında taşma olmuştur !");
            var format = "";
            for (int i = 0; i < evrakKarakterSayisi - seri.Length; i++)
                format += "0";
            format = string.Format("1:{0}", format);
            format = "{0}{" + format + "}";
            format = string.Format(format, seri, no + 1);
            return format;
        }

        void EvrakNolari_YukleAyarla()
        {
            List<STIEvrakBilgi> evrakBilgiList = new List<STIEvrakBilgi>();
            var Exper = new SqlExper(ConStr, SirketKodu);

            evrakBilgiList = Exper.SelectList<STIEvrakBilgi>(string.Format(@"
            SELECT 0 as Tip, * FROM (
            SELECT TOP 1 STK005_EvrakSeriNo as EvrakNo, STK005_SEQNo as SiraNo  
            FROM YNS{0}.YNS{0}.STK005(NOLOCK)
            WHERE STK005_EvrakTipi=11
            ORDER BY STK005_Row_ID DESC) Fatura
            UNION ALL
            SELECT 1 as Tip, * FROM (
            SELECT TOP 1 STK002_EvrakSeriNo as EvrakNo, STK002_SEQNo as SiraNo  
            FROM YNS{0}.YNS{0}.STK002(NOLOCK)
            WHERE STK002_GC=1
            ORDER BY STK002_Row_ID DESC) Siparis
            UNION ALL
            SELECT 2 as Tip, * FROM (
            SELECT TOP 1 TeklifNo as EvrakNo, 0 as SiraNo  
            FROM YNS{0}.YNS{0}.Teklif(NOLOCK)
            WHERE IslemTur=1
            ORDER BY ID DESC) Teklif
            UNION ALL
            SELECT 3 as Tip, * FROM (
            SELECT TOP 1 EvrakNo, 0 as SiraNo  
            FROM YNS{0}.YNS{0}.TempFatura(NOLOCK)
            ORDER BY ID DESC) TempFatura", SirketKodu));

            if (evrakBilgiList.IsNull() || evrakBilgiList.Count != 4)
            {
                throw new Exception(@"Sipariş ve/veya fatura için son evrak numarası bulunamadı. Lütfen STK002, STK005, Teklif ve TempFatura tablolarını inceleyin");
            }

            List<string> tabloList = new List<string> { "STK005", "STK002", "Teklif", "Fatura" };

            EvrakNoList = new List<STIEvrakBilgi>();
            for (int i = 0; i < 4; i++)
            {
                var evrakNo = "";
                var seri = "";
                var siraNo = 0;
                evrakNo = evrakBilgiList[i].EvrakNo;
                siraNo = evrakBilgiList[i].SiraNo;

                if (evrakNo.Length < 4)
                    throw new Exception(string.Format(@"Son evrak numarası geçersiz lütfen {0} tablosundaki son evrak numarasını kontrol edin !", tabloList[i]));

                seri = evrakNo.Substring(0, 2);
                evrakNo = evrakNo.Remove(0, 2);
                var no = evrakNo.ToInt32();

                evrakNo = EvrakNoOlustur(8, seri, no);

                EvrakNoList.Add(new STIEvrakBilgi { EvrakNo = evrakNo, SiraNo = siraNo, Tip = evrakBilgiList[i].Tip });
            }
        }

        /// <summary>
        /// hesap listesi
        /// </summary>
        void HesapListesini_YukleAyarla(string HesapKodu)
        {
            HesapList = new List<HesapItem>();
            var Sqlexper = new SqlExper(ConStr, SirketKodu);
            HesapList = Sqlexper.SelectList<HesapItem>(string.Format(HesapItem.Sorgu, HesapKodu));
        }


        public class DovizKurlari
        {
            public decimal USD { get; set; }
            public decimal EUR { get; set; }

            public static string Sorgu = @"
            SELECT TOP 1 DVZ002_DvzEfektisSatis1 as USD, DVZ002_DvzEfektisSatis2 as EUR
            FROM   YNS{{0}}.YNS{{0}}.DVZ002(NOLOCK) 
            WHERE  DVZ002_DvzEfektisSatis1>0 AND DVZ002_DovizDate IN({0},{1},{2})
            ORDER BY DVZ002_DovizDate DESC";
        }
    }
}