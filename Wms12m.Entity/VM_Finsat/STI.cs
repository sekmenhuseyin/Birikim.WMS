using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    #region STI Class 

    public class STI : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        short islemTur;
        string evrakNo;
        int tarih;
        string chk;
        short kynkEvrakTip;
        short siraNo;
        short irsFat;
        short islemTip;
        string malKodu;
        decimal miktar;
        decimal fiyat;
        decimal tutar;
        string dovizCinsi;
        decimal dovizKuru;
        decimal dovizTutar;
        decimal dvzBirimFiyat;
        string birim;
        decimal birimFiyat;
        decimal birimMiktar;
        decimal iskonto;
        float iskontoOran;
        decimal toplamIskonto;
        decimal kDV;
        float kDVOran;
        short kDVDahilHaric;
        short otvDahilHaric;
        decimal otvTutar;
        string gtkListeNo;
        string aciklama;
        string kod1;
        string kod2;
        string kod3;
        string kod4;
        string kod5;
        string kod6;
        string kod7;
        string kod8;
        string kod9;
        string kod10;
        short kod11;
        short kod12;
        decimal kod13;
        decimal kod14;
        string depo;
        string vasita;
        string seriNo;
        int sevkTarih;
        decimal promosyonMiktar;
        decimal miktar2;
        decimal tutar2;
        int tarih2;
        int vadeTarih;
        decimal masraf;
        decimal maliyet;
        short mlyYontem;
        string mhsKod;
        string mhsKarsiKod;
        string masrafMerkezi;
        short mhsDurum;
        short mlyMhs;
        short mhsTabloNo;
        int evrakTarih;
        short siparisSiraNo;
        float iskontoOran1;
        short iskOran1Net;
        float iskontoOran2;
        short iskOran2Net;
        float iskontoOran3;
        short iskOran3Net;
        float iskontoOran4;
        short iskOran4Net;
        float iskontoOran5;
        short iskOran5Net;
        decimal klmTutarIsk;
        short klmTutarIskNet;
        string teslimChk;
        string butceKod;
        string fytListeNo;
        decimal fatMiktar;
        string tesTemMalKod;
        short dvzTL;
        string barkodNo;
        double katsayi;
        short _Operator;
        short valorGun;
        string kaynakIrsEvrakNo;
        int kaynakIrsTarih;
        string kaynakIIFEvrakNo;
        int kaynakIIFTarih;
        string kaynakSiparisNo;
        int kaynakSiparisTarih;
        string erekIFEvrakNo;
        short erekIFKEvrakTip;
        decimal erekIFMiktar;
        string erekIIFEvrakNo;
        short erekIIFKEvrakTip;
        decimal erekIIFMiktar;
        string renkBedenKod1;
        string renkBedenKod2;
        string renkBedenKod3;
        string renkBedenKod4;
        short kayitTuru;
        string nesne1;
        string nesne2;
        string nesne3;
        short irsFat2;
        decimal miktar3;
        decimal tutar3;
        short siraNo2;
        int kurTarihi;
        decimal krediBorcTutar;
        decimal aktiflesenKrediFaizi;
        int kredi_Donem_BaslangicTarih;
        int kredi_Donem_BitisTarih;
        int kredi_Donem_VadeTarih;
        decimal kredi_Donem_VadeFarkiTutar;
        decimal reelOlmayanFinansmanMaliyet;
        short krediArindirmaSekli;
        short finansmanGiderTuru;
        int duz_Yapilan_Yil;
        short duz_Yapilan_Donem;
        short duz_Yontemi;
        string duz_Mhs_Hesap_Kodu;
        short duz_Mhs_Durumu;
        float duz_Stok_Devir_Hizi;
        float duz_Katsayisi;
        decimal duz_Esas_Tutar;
        decimal duz_Tutar;
        short duz_Mly_Yontemi;
        decimal duz_Mly_Tarihi_Mly_Tutar;
        decimal duz_Mly_Satilan_Mal_Mly_Tutar;
        string duz_Mly_Mhs_Hesap_Kodu;
        short duz_Mly_Mhs_Durumu;
        short anaEvrakTip;
        string kartDovizCinsi;
        decimal kartDovizKuru;
        string guvenlikKod;
        string kaydeden;
        int kayitTarih;
        int kayitSaat;
        short kayitKaynak;
        string kayitSurum;
        string degistiren;
        int degisTarih;
        int degisSaat;
        short degisKaynak;
        string degisSurum;
        short checkSum;
        string fFEvrakNo;
        int fFTarih;
        short kaynakSiraNo;
        int sonKullanimTarihi;
        short dvzKurCinsi;
        decimal depoIadeEdilenMiktar;
        string tevfikatOran;
        decimal tevfikatTutar;
        decimal masraf1;
        decimal masraf2;
        string ithalatDosyaNo;
        short ithalatMDagDurum;
        int tarih3;
        int tarih4;
        int tarih5;
        int tarih6;
        short eFatTip;
        short eFatDurum;
        decimal toplamKlmIskonto;
        short ithalatMDagYontem;
        string kDVMuafAck;
        string karsiMalKodu;
        string ureticiMalKodu;
        float stopajOran;
        decimal stopajTutar;
        short oTVTevkifatVarYok;
        string aciklama2;
        string _eFatIrsReferansNo;
        string ihracatDosyaNo;
        string tevfikatOranKod;
        string ozelMatKDVKod;
        string projeKodu;
        short eArsivTeslimSekli;
        short eArsivFaturaTipi;
        short eArsivFaturaDurum;
        decimal iskontoTutar;
        decimal iskontoTutar1;
        decimal iskontoTutar2;
        decimal iskontoTutar3;
        decimal iskontoTutar4;
        decimal iskontoTutar5;
        string not1;
        string not2;
        string not3;
        string not4;
        string not5;
        decimal brutAgirlik;
        decimal netAgirlik;
        decimal daraAgirlik;
        string birimAgirlik;
        decimal brutHacim;
        decimal netHacim;
        string birimHacim;
        string kapTipi;
        decimal kapAdet;
        int formBaBsTarih;
        int yOKCZRaporNo;
        float kDVDahilKalemIskontoOran;
        decimal kDVDahilKalemOranIskontosu;
        decimal kDVDahilKalemTutarIskontosu;
        decimal kDVDahilIskonto;
        decimal kDVIstisnaTutar;
        string ureticiChk;
        decimal ihracatMiktar_Dagitilan;
        string faturaID;
        int rOW_ID;
        byte[] _timestamp;
        short _pk_IslemTur;
        string _pk_EvrakNo;
        int _pk_Tarih;
        string _pk_Chk;
        short _pk_KynkEvrakTip;
        short _pk_SiraNo;
        #endregion /// Fields

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short IslemTur
        {
            get { return islemTur; }
            set
            {
                islemTur = value;
                OnPropertyChanged("IslemTur");
            }
        }

        /// <summary> VARCHAR (16) PrimaryKey * </summary>
        public string EvrakNo
        {
            get { return evrakNo; }
            set
            {
                evrakNo = value;
                OnPropertyChanged("EvrakNo");
            }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
        public int Tarih
        {
            get { return tarih; }
            set
            {
                tarih = value;
                OnPropertyChanged("Tarih");
            }
        }

        /// <summary> VARCHAR (20) PrimaryKey * </summary>
        public string Chk
        {
            get { return chk; }
            set
            {
                chk = value;
                OnPropertyChanged("Chk");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short KynkEvrakTip
        {
            get { return kynkEvrakTip; }
            set
            {
                kynkEvrakTip = value;
                OnPropertyChanged("KynkEvrakTip");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short SiraNo
        {
            get { return siraNo; }
            set
            {
                siraNo = value;
                OnPropertyChanged("SiraNo");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IrsFat
        {
            get { return irsFat; }
            set
            {
                irsFat = value;
                OnPropertyChanged("IrsFat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IslemTip
        {
            get { return islemTip; }
            set
            {
                islemTip = value;
                OnPropertyChanged("IslemTip");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string MalKodu
        {
            get { return malKodu; }
            set
            {
                malKodu = value;
                OnPropertyChanged("MalKodu");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Miktar
        {
            get { return miktar; }
            set
            {
                miktar = value;
                OnPropertyChanged("Miktar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Fiyat
        {
            get { return fiyat; }
            set
            {
                fiyat = value;
                OnPropertyChanged("Fiyat");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Tutar
        {
            get { return tutar; }
            set
            {
                tutar = value;
                OnPropertyChanged("Tutar");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string DovizCinsi
        {
            get { return dovizCinsi; }
            set
            {
                dovizCinsi = value;
                OnPropertyChanged("DovizCinsi");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DovizKuru
        {
            get { return dovizKuru; }
            set
            {
                dovizKuru = value;
                OnPropertyChanged("DovizKuru");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DovizTutar
        {
            get { return dovizTutar; }
            set
            {
                dovizTutar = value;
                OnPropertyChanged("DovizTutar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzBirimFiyat
        {
            get { return dvzBirimFiyat; }
            set
            {
                dvzBirimFiyat = value;
                OnPropertyChanged("DvzBirimFiyat");
            }
        }

        /// <summary> VARCHAR (4) * </summary>
        public string Birim
        {
            get { return birim; }
            set
            {
                birim = value;
                OnPropertyChanged("Birim");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal BirimFiyat
        {
            get { return birimFiyat; }
            set
            {
                birimFiyat = value;
                OnPropertyChanged("BirimFiyat");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal BirimMiktar
        {
            get { return birimMiktar; }
            set
            {
                birimMiktar = value;
                OnPropertyChanged("BirimMiktar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Iskonto
        {
            get { return iskonto; }
            set
            {
                iskonto = value;
                OnPropertyChanged("Iskonto");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float IskontoOran
        {
            get { return iskontoOran; }
            set
            {
                iskontoOran = value;
                OnPropertyChanged("IskontoOran");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal ToplamIskonto
        {
            get { return toplamIskonto; }
            set
            {
                toplamIskonto = value;
                OnPropertyChanged("ToplamIskonto");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KDV
        {
            get { return kDV; }
            set
            {
                kDV = value;
                OnPropertyChanged("KDV");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float KDVOran
        {
            get { return kDVOran; }
            set
            {
                kDVOran = value;
                OnPropertyChanged("KDVOran");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KDVDahilHaric
        {
            get { return kDVDahilHaric; }
            set
            {
                kDVDahilHaric = value;
                OnPropertyChanged("KDVDahilHaric");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short OtvDahilHaric
        {
            get { return otvDahilHaric; }
            set
            {
                otvDahilHaric = value;
                OnPropertyChanged("OtvDahilHaric");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal OtvTutar
        {
            get { return otvTutar; }
            set
            {
                otvTutar = value;
                OnPropertyChanged("OtvTutar");
            }
        }

        /// <summary> VARCHAR (2) * </summary>
        public string GtkListeNo
        {
            get { return gtkListeNo; }
            set
            {
                gtkListeNo = value;
                OnPropertyChanged("GtkListeNo");
            }
        }

        /// <summary> VARCHAR (64) * </summary>
        public string Aciklama
        {
            get { return aciklama; }
            set
            {
                aciklama = value;
                OnPropertyChanged("Aciklama");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod1
        {
            get { return kod1; }
            set
            {
                kod1 = value;
                OnPropertyChanged("Kod1");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod2
        {
            get { return kod2; }
            set
            {
                kod2 = value;
                OnPropertyChanged("Kod2");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod3
        {
            get { return kod3; }
            set
            {
                kod3 = value;
                OnPropertyChanged("Kod3");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod4
        {
            get { return kod4; }
            set
            {
                kod4 = value;
                OnPropertyChanged("Kod4");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod5
        {
            get { return kod5; }
            set
            {
                kod5 = value;
                OnPropertyChanged("Kod5");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod6
        {
            get { return kod6; }
            set
            {
                kod6 = value;
                OnPropertyChanged("Kod6");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod7
        {
            get { return kod7; }
            set
            {
                kod7 = value;
                OnPropertyChanged("Kod7");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod8
        {
            get { return kod8; }
            set
            {
                kod8 = value;
                OnPropertyChanged("Kod8");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod9
        {
            get { return kod9; }
            set
            {
                kod9 = value;
                OnPropertyChanged("Kod9");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod10
        {
            get { return kod10; }
            set
            {
                kod10 = value;
                OnPropertyChanged("Kod10");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Kod11
        {
            get { return kod11; }
            set
            {
                kod11 = value;
                OnPropertyChanged("Kod11");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Kod12
        {
            get { return kod12; }
            set
            {
                kod12 = value;
                OnPropertyChanged("Kod12");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod13
        {
            get { return kod13; }
            set
            {
                kod13 = value;
                OnPropertyChanged("Kod13");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod14
        {
            get { return kod14; }
            set
            {
                kod14 = value;
                OnPropertyChanged("Kod14");
            }
        }

        /// <summary> VARCHAR (10) * </summary>
        public string Depo
        {
            get { return depo; }
            set
            {
                depo = value;
                OnPropertyChanged("Depo");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string Vasita
        {
            get { return vasita; }
            set
            {
                vasita = value;
                OnPropertyChanged("Vasita");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string SeriNo
        {
            get { return seriNo; }
            set
            {
                seriNo = value;
                OnPropertyChanged("SeriNo");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SevkTarih
        {
            get { return sevkTarih; }
            set
            {
                sevkTarih = value;
                OnPropertyChanged("SevkTarih");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal PromosyonMiktar
        {
            get { return promosyonMiktar; }
            set
            {
                promosyonMiktar = value;
                OnPropertyChanged("PromosyonMiktar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Miktar2
        {
            get { return miktar2; }
            set
            {
                miktar2 = value;
                OnPropertyChanged("Miktar2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Tutar2
        {
            get { return tutar2; }
            set
            {
                tutar2 = value;
                OnPropertyChanged("Tutar2");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Tarih2
        {
            get { return tarih2; }
            set
            {
                tarih2 = value;
                OnPropertyChanged("Tarih2");
            }
        }

        /// <summary> INT (4) * </summary>
        public int VadeTarih
        {
            get { return vadeTarih; }
            set
            {
                vadeTarih = value;
                OnPropertyChanged("VadeTarih");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Masraf
        {
            get { return masraf; }
            set
            {
                masraf = value;
                OnPropertyChanged("Masraf");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Maliyet
        {
            get { return maliyet; }
            set
            {
                maliyet = value;
                OnPropertyChanged("Maliyet");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MlyYontem
        {
            get { return mlyYontem; }
            set
            {
                mlyYontem = value;
                OnPropertyChanged("MlyYontem");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string MhsKod
        {
            get { return mhsKod; }
            set
            {
                mhsKod = value;
                OnPropertyChanged("MhsKod");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string MhsKarsiKod
        {
            get { return mhsKarsiKod; }
            set
            {
                mhsKarsiKod = value;
                OnPropertyChanged("MhsKarsiKod");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string MasrafMerkezi
        {
            get { return masrafMerkezi; }
            set
            {
                masrafMerkezi = value;
                OnPropertyChanged("MasrafMerkezi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MhsDurum
        {
            get { return mhsDurum; }
            set
            {
                mhsDurum = value;
                OnPropertyChanged("MhsDurum");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MlyMhs
        {
            get { return mlyMhs; }
            set
            {
                mlyMhs = value;
                OnPropertyChanged("MlyMhs");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short MhsTabloNo
        {
            get { return mhsTabloNo; }
            set
            {
                mhsTabloNo = value;
                OnPropertyChanged("MhsTabloNo");
            }
        }

        /// <summary> INT (4) * </summary>
        public int EvrakTarih
        {
            get { return evrakTarih; }
            set
            {
                evrakTarih = value;
                OnPropertyChanged("EvrakTarih");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SiparisSiraNo
        {
            get { return siparisSiraNo; }
            set
            {
                siparisSiraNo = value;
                OnPropertyChanged("SiparisSiraNo");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float IskontoOran1
        {
            get { return iskontoOran1; }
            set
            {
                iskontoOran1 = value;
                OnPropertyChanged("IskontoOran1");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IskOran1Net
        {
            get { return iskOran1Net; }
            set
            {
                iskOran1Net = value;
                OnPropertyChanged("IskOran1Net");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float IskontoOran2
        {
            get { return iskontoOran2; }
            set
            {
                iskontoOran2 = value;
                OnPropertyChanged("IskontoOran2");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IskOran2Net
        {
            get { return iskOran2Net; }
            set
            {
                iskOran2Net = value;
                OnPropertyChanged("IskOran2Net");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float IskontoOran3
        {
            get { return iskontoOran3; }
            set
            {
                iskontoOran3 = value;
                OnPropertyChanged("IskontoOran3");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IskOran3Net
        {
            get { return iskOran3Net; }
            set
            {
                iskOran3Net = value;
                OnPropertyChanged("IskOran3Net");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float IskontoOran4
        {
            get { return iskontoOran4; }
            set
            {
                iskontoOran4 = value;
                OnPropertyChanged("IskontoOran4");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IskOran4Net
        {
            get { return iskOran4Net; }
            set
            {
                iskOran4Net = value;
                OnPropertyChanged("IskOran4Net");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float IskontoOran5
        {
            get { return iskontoOran5; }
            set
            {
                iskontoOran5 = value;
                OnPropertyChanged("IskontoOran5");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IskOran5Net
        {
            get { return iskOran5Net; }
            set
            {
                iskOran5Net = value;
                OnPropertyChanged("IskOran5Net");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KlmTutarIsk
        {
            get { return klmTutarIsk; }
            set
            {
                klmTutarIsk = value;
                OnPropertyChanged("KlmTutarIsk");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KlmTutarIskNet
        {
            get { return klmTutarIskNet; }
            set
            {
                klmTutarIskNet = value;
                OnPropertyChanged("KlmTutarIskNet");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string TeslimChk
        {
            get { return teslimChk; }
            set
            {
                teslimChk = value;
                OnPropertyChanged("TeslimChk");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string ButceKod
        {
            get { return butceKod; }
            set
            {
                butceKod = value;
                OnPropertyChanged("ButceKod");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string FytListeNo
        {
            get { return fytListeNo; }
            set
            {
                fytListeNo = value;
                OnPropertyChanged("FytListeNo");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal FatMiktar
        {
            get { return fatMiktar; }
            set
            {
                fatMiktar = value;
                OnPropertyChanged("FatMiktar");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string TesTemMalKod
        {
            get { return tesTemMalKod; }
            set
            {
                tesTemMalKod = value;
                OnPropertyChanged("TesTemMalKod");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzTL
        {
            get { return dvzTL; }
            set
            {
                dvzTL = value;
                OnPropertyChanged("DvzTL");
            }
        }

        /// <summary> VARCHAR (52) * </summary>
        public string BarkodNo
        {
            get { return barkodNo; }
            set
            {
                barkodNo = value;
                OnPropertyChanged("BarkodNo");
            }
        }

        /// <summary> FLOAT (8) * </summary>
        public double Katsayi
        {
            get { return katsayi; }
            set
            {
                katsayi = value;
                OnPropertyChanged("Katsayi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Operator
        {
            get { return _Operator; }
            set
            {
                _Operator = value;
                OnPropertyChanged("Operator");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short ValorGun
        {
            get { return valorGun; }
            set
            {
                valorGun = value;
                OnPropertyChanged("ValorGun");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string KaynakIrsEvrakNo
        {
            get { return kaynakIrsEvrakNo; }
            set
            {
                kaynakIrsEvrakNo = value;
                OnPropertyChanged("KaynakIrsEvrakNo");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KaynakIrsTarih
        {
            get { return kaynakIrsTarih; }
            set
            {
                kaynakIrsTarih = value;
                OnPropertyChanged("KaynakIrsTarih");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string KaynakIIFEvrakNo
        {
            get { return kaynakIIFEvrakNo; }
            set
            {
                kaynakIIFEvrakNo = value;
                OnPropertyChanged("KaynakIIFEvrakNo");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KaynakIIFTarih
        {
            get { return kaynakIIFTarih; }
            set
            {
                kaynakIIFTarih = value;
                OnPropertyChanged("KaynakIIFTarih");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string KaynakSiparisNo
        {
            get { return kaynakSiparisNo; }
            set
            {
                kaynakSiparisNo = value;
                OnPropertyChanged("KaynakSiparisNo");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KaynakSiparisTarih
        {
            get { return kaynakSiparisTarih; }
            set
            {
                kaynakSiparisTarih = value;
                OnPropertyChanged("KaynakSiparisTarih");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string ErekIFEvrakNo
        {
            get { return erekIFEvrakNo; }
            set
            {
                erekIFEvrakNo = value;
                OnPropertyChanged("ErekIFEvrakNo");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short ErekIFKEvrakTip
        {
            get { return erekIFKEvrakTip; }
            set
            {
                erekIFKEvrakTip = value;
                OnPropertyChanged("ErekIFKEvrakTip");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal ErekIFMiktar
        {
            get { return erekIFMiktar; }
            set
            {
                erekIFMiktar = value;
                OnPropertyChanged("ErekIFMiktar");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string ErekIIFEvrakNo
        {
            get { return erekIIFEvrakNo; }
            set
            {
                erekIIFEvrakNo = value;
                OnPropertyChanged("ErekIIFEvrakNo");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short ErekIIFKEvrakTip
        {
            get { return erekIIFKEvrakTip; }
            set
            {
                erekIIFKEvrakTip = value;
                OnPropertyChanged("ErekIIFKEvrakTip");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal ErekIIFMiktar
        {
            get { return erekIIFMiktar; }
            set
            {
                erekIIFMiktar = value;
                OnPropertyChanged("ErekIIFMiktar");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod1
        {
            get { return renkBedenKod1; }
            set
            {
                renkBedenKod1 = value;
                OnPropertyChanged("RenkBedenKod1");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod2
        {
            get { return renkBedenKod2; }
            set
            {
                renkBedenKod2 = value;
                OnPropertyChanged("RenkBedenKod2");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod3
        {
            get { return renkBedenKod3; }
            set
            {
                renkBedenKod3 = value;
                OnPropertyChanged("RenkBedenKod3");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod4
        {
            get { return renkBedenKod4; }
            set
            {
                renkBedenKod4 = value;
                OnPropertyChanged("RenkBedenKod4");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitTuru
        {
            get { return kayitTuru; }
            set
            {
                kayitTuru = value;
                OnPropertyChanged("KayitTuru");
            }
        }

        /// <summary> VARCHAR (254) * </summary>
        public string Nesne1
        {
            get { return nesne1; }
            set
            {
                nesne1 = value;
                OnPropertyChanged("Nesne1");
            }
        }

        /// <summary> VARCHAR (254) * </summary>
        public string Nesne2
        {
            get { return nesne2; }
            set
            {
                nesne2 = value;
                OnPropertyChanged("Nesne2");
            }
        }

        /// <summary> VARCHAR (254) * </summary>
        public string Nesne3
        {
            get { return nesne3; }
            set
            {
                nesne3 = value;
                OnPropertyChanged("Nesne3");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IrsFat2
        {
            get { return irsFat2; }
            set
            {
                irsFat2 = value;
                OnPropertyChanged("IrsFat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Miktar3
        {
            get { return miktar3; }
            set
            {
                miktar3 = value;
                OnPropertyChanged("Miktar3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Tutar3
        {
            get { return tutar3; }
            set
            {
                tutar3 = value;
                OnPropertyChanged("Tutar3");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SiraNo2
        {
            get { return siraNo2; }
            set
            {
                siraNo2 = value;
                OnPropertyChanged("SiraNo2");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KurTarihi
        {
            get { return kurTarihi; }
            set
            {
                kurTarihi = value;
                OnPropertyChanged("KurTarihi");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KrediBorcTutar
        {
            get { return krediBorcTutar; }
            set
            {
                krediBorcTutar = value;
                OnPropertyChanged("KrediBorcTutar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal AktiflesenKrediFaizi
        {
            get { return aktiflesenKrediFaizi; }
            set
            {
                aktiflesenKrediFaizi = value;
                OnPropertyChanged("AktiflesenKrediFaizi");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Kredi_Donem_BaslangicTarih
        {
            get { return kredi_Donem_BaslangicTarih; }
            set
            {
                kredi_Donem_BaslangicTarih = value;
                OnPropertyChanged("Kredi_Donem_BaslangicTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Kredi_Donem_BitisTarih
        {
            get { return kredi_Donem_BitisTarih; }
            set
            {
                kredi_Donem_BitisTarih = value;
                OnPropertyChanged("Kredi_Donem_BitisTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Kredi_Donem_VadeTarih
        {
            get { return kredi_Donem_VadeTarih; }
            set
            {
                kredi_Donem_VadeTarih = value;
                OnPropertyChanged("Kredi_Donem_VadeTarih");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kredi_Donem_VadeFarkiTutar
        {
            get { return kredi_Donem_VadeFarkiTutar; }
            set
            {
                kredi_Donem_VadeFarkiTutar = value;
                OnPropertyChanged("Kredi_Donem_VadeFarkiTutar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal ReelOlmayanFinansmanMaliyet
        {
            get { return reelOlmayanFinansmanMaliyet; }
            set
            {
                reelOlmayanFinansmanMaliyet = value;
                OnPropertyChanged("ReelOlmayanFinansmanMaliyet");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KrediArindirmaSekli
        {
            get { return krediArindirmaSekli; }
            set
            {
                krediArindirmaSekli = value;
                OnPropertyChanged("KrediArindirmaSekli");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short FinansmanGiderTuru
        {
            get { return finansmanGiderTuru; }
            set
            {
                finansmanGiderTuru = value;
                OnPropertyChanged("FinansmanGiderTuru");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Duz_Yapilan_Yil
        {
            get { return duz_Yapilan_Yil; }
            set
            {
                duz_Yapilan_Yil = value;
                OnPropertyChanged("Duz_Yapilan_Yil");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Duz_Yapilan_Donem
        {
            get { return duz_Yapilan_Donem; }
            set
            {
                duz_Yapilan_Donem = value;
                OnPropertyChanged("Duz_Yapilan_Donem");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Duz_Yontemi
        {
            get { return duz_Yontemi; }
            set
            {
                duz_Yontemi = value;
                OnPropertyChanged("Duz_Yontemi");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string Duz_Mhs_Hesap_Kodu
        {
            get { return duz_Mhs_Hesap_Kodu; }
            set
            {
                duz_Mhs_Hesap_Kodu = value;
                OnPropertyChanged("Duz_Mhs_Hesap_Kodu");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Duz_Mhs_Durumu
        {
            get { return duz_Mhs_Durumu; }
            set
            {
                duz_Mhs_Durumu = value;
                OnPropertyChanged("Duz_Mhs_Durumu");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Duz_Stok_Devir_Hizi
        {
            get { return duz_Stok_Devir_Hizi; }
            set
            {
                duz_Stok_Devir_Hizi = value;
                OnPropertyChanged("Duz_Stok_Devir_Hizi");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float Duz_Katsayisi
        {
            get { return duz_Katsayisi; }
            set
            {
                duz_Katsayisi = value;
                OnPropertyChanged("Duz_Katsayisi");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Duz_Esas_Tutar
        {
            get { return duz_Esas_Tutar; }
            set
            {
                duz_Esas_Tutar = value;
                OnPropertyChanged("Duz_Esas_Tutar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Duz_Tutar
        {
            get { return duz_Tutar; }
            set
            {
                duz_Tutar = value;
                OnPropertyChanged("Duz_Tutar");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Duz_Mly_Yontemi
        {
            get { return duz_Mly_Yontemi; }
            set
            {
                duz_Mly_Yontemi = value;
                OnPropertyChanged("Duz_Mly_Yontemi");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Duz_Mly_Tarihi_Mly_Tutar
        {
            get { return duz_Mly_Tarihi_Mly_Tutar; }
            set
            {
                duz_Mly_Tarihi_Mly_Tutar = value;
                OnPropertyChanged("Duz_Mly_Tarihi_Mly_Tutar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Duz_Mly_Satilan_Mal_Mly_Tutar
        {
            get { return duz_Mly_Satilan_Mal_Mly_Tutar; }
            set
            {
                duz_Mly_Satilan_Mal_Mly_Tutar = value;
                OnPropertyChanged("Duz_Mly_Satilan_Mal_Mly_Tutar");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string Duz_Mly_Mhs_Hesap_Kodu
        {
            get { return duz_Mly_Mhs_Hesap_Kodu; }
            set
            {
                duz_Mly_Mhs_Hesap_Kodu = value;
                OnPropertyChanged("Duz_Mly_Mhs_Hesap_Kodu");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short Duz_Mly_Mhs_Durumu
        {
            get { return duz_Mly_Mhs_Durumu; }
            set
            {
                duz_Mly_Mhs_Durumu = value;
                OnPropertyChanged("Duz_Mly_Mhs_Durumu");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AnaEvrakTip
        {
            get { return anaEvrakTip; }
            set
            {
                anaEvrakTip = value;
                OnPropertyChanged("AnaEvrakTip");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string KartDovizCinsi
        {
            get { return kartDovizCinsi; }
            set
            {
                kartDovizCinsi = value;
                OnPropertyChanged("KartDovizCinsi");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KartDovizKuru
        {
            get { return kartDovizKuru; }
            set
            {
                kartDovizKuru = value;
                OnPropertyChanged("KartDovizKuru");
            }
        }

        /// <summary> VARCHAR (2) * </summary>
        public string GuvenlikKod
        {
            get { return guvenlikKod; }
            set
            {
                guvenlikKod = value;
                OnPropertyChanged("GuvenlikKod");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Kaydeden
        {
            get { return kaydeden; }
            set
            {
                kaydeden = value;
                OnPropertyChanged("Kaydeden");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitTarih
        {
            get { return kayitTarih; }
            set
            {
                kayitTarih = value;
                OnPropertyChanged("KayitTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitSaat
        {
            get { return kayitSaat; }
            set
            {
                kayitSaat = value;
                OnPropertyChanged("KayitSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitKaynak
        {
            get { return kayitKaynak; }
            set
            {
                kayitKaynak = value;
                OnPropertyChanged("KayitKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string KayitSurum
        {
            get { return kayitSurum; }
            set
            {
                kayitSurum = value;
                OnPropertyChanged("KayitSurum");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Degistiren
        {
            get { return degistiren; }
            set
            {
                degistiren = value;
                OnPropertyChanged("Degistiren");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisTarih
        {
            get { return degisTarih; }
            set
            {
                degisTarih = value;
                OnPropertyChanged("DegisTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisSaat
        {
            get { return degisSaat; }
            set
            {
                degisSaat = value;
                OnPropertyChanged("DegisSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DegisKaynak
        {
            get { return degisKaynak; }
            set
            {
                degisKaynak = value;
                OnPropertyChanged("DegisKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string DegisSurum
        {
            get { return degisSurum; }
            set
            {
                degisSurum = value;
                OnPropertyChanged("DegisSurum");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short CheckSum
        {
            get { return checkSum; }
            set
            {
                checkSum = value;
                OnPropertyChanged("CheckSum");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string FFEvrakNo
        {
            get { return fFEvrakNo; }
            set
            {
                fFEvrakNo = value;
                OnPropertyChanged("FFEvrakNo");
            }
        }

        /// <summary> INT (4) * </summary>
        public int FFTarih
        {
            get { return fFTarih; }
            set
            {
                fFTarih = value;
                OnPropertyChanged("FFTarih");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KaynakSiraNo
        {
            get { return kaynakSiraNo; }
            set
            {
                kaynakSiraNo = value;
                OnPropertyChanged("KaynakSiraNo");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SonKullanimTarihi
        {
            get { return sonKullanimTarihi; }
            set
            {
                sonKullanimTarihi = value;
                OnPropertyChanged("SonKullanimTarihi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzKurCinsi
        {
            get { return dvzKurCinsi; }
            set
            {
                dvzKurCinsi = value;
                OnPropertyChanged("DvzKurCinsi");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DepoIadeEdilenMiktar
        {
            get { return depoIadeEdilenMiktar; }
            set
            {
                depoIadeEdilenMiktar = value;
                OnPropertyChanged("DepoIadeEdilenMiktar");
            }
        }

        /// <summary> VARCHAR (7) * </summary>
        public string TevfikatOran
        {
            get { return tevfikatOran; }
            set
            {
                tevfikatOran = value;
                OnPropertyChanged("TevfikatOran");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal TevfikatTutar
        {
            get { return tevfikatTutar; }
            set
            {
                tevfikatTutar = value;
                OnPropertyChanged("TevfikatTutar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Masraf1
        {
            get { return masraf1; }
            set
            {
                masraf1 = value;
                OnPropertyChanged("Masraf1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Masraf2
        {
            get { return masraf2; }
            set
            {
                masraf2 = value;
                OnPropertyChanged("Masraf2");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string IthalatDosyaNo
        {
            get { return ithalatDosyaNo; }
            set
            {
                ithalatDosyaNo = value;
                OnPropertyChanged("IthalatDosyaNo");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IthalatMDagDurum
        {
            get { return ithalatMDagDurum; }
            set
            {
                ithalatMDagDurum = value;
                OnPropertyChanged("IthalatMDagDurum");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Tarih3
        {
            get { return tarih3; }
            set
            {
                tarih3 = value;
                OnPropertyChanged("Tarih3");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Tarih4
        {
            get { return tarih4; }
            set
            {
                tarih4 = value;
                OnPropertyChanged("Tarih4");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Tarih5
        {
            get { return tarih5; }
            set
            {
                tarih5 = value;
                OnPropertyChanged("Tarih5");
            }
        }

        /// <summary> INT (4) * </summary>
        public int Tarih6
        {
            get { return tarih6; }
            set
            {
                tarih6 = value;
                OnPropertyChanged("Tarih6");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short EFatTip
        {
            get { return eFatTip; }
            set
            {
                eFatTip = value;
                OnPropertyChanged("EFatTip");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short EFatDurum
        {
            get { return eFatDurum; }
            set
            {
                eFatDurum = value;
                OnPropertyChanged("EFatDurum");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal ToplamKlmIskonto
        {
            get { return toplamKlmIskonto; }
            set
            {
                toplamKlmIskonto = value;
                OnPropertyChanged("ToplamKlmIskonto");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short IthalatMDagYontem
        {
            get { return ithalatMDagYontem; }
            set
            {
                ithalatMDagYontem = value;
                OnPropertyChanged("IthalatMDagYontem");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string KDVMuafAck
        {
            get { return kDVMuafAck; }
            set
            {
                kDVMuafAck = value;
                OnPropertyChanged("KDVMuafAck");
            }
        }

        /// <summary> VARCHAR (40) * </summary>
        public string KarsiMalKodu
        {
            get { return karsiMalKodu; }
            set
            {
                karsiMalKodu = value;
                OnPropertyChanged("KarsiMalKodu");
            }
        }

        /// <summary> VARCHAR (40) * </summary>
        public string UreticiMalKodu
        {
            get { return ureticiMalKodu; }
            set
            {
                ureticiMalKodu = value;
                OnPropertyChanged("UreticiMalKodu");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float StopajOran
        {
            get { return stopajOran; }
            set
            {
                stopajOran = value;
                OnPropertyChanged("StopajOran");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal StopajTutar
        {
            get { return stopajTutar; }
            set
            {
                stopajTutar = value;
                OnPropertyChanged("StopajTutar");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short OTVTevkifatVarYok
        {
            get { return oTVTevkifatVarYok; }
            set
            {
                oTVTevkifatVarYok = value;
                OnPropertyChanged("OTVTevkifatVarYok");
            }
        }

        /// <summary> VARCHAR (64) * </summary>
        public string Aciklama2
        {
            get { return aciklama2; }
            set
            {
                aciklama2 = value;
                OnPropertyChanged("Aciklama2");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string eFatIrsReferansNo
        {
            get { return _eFatIrsReferansNo; }
            set
            {
                _eFatIrsReferansNo = value;
                OnPropertyChanged("eFatIrsReferansNo");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string IhracatDosyaNo
        {
            get { return ihracatDosyaNo; }
            set
            {
                ihracatDosyaNo = value;
                OnPropertyChanged("IhracatDosyaNo");
            }
        }

        /// <summary> VARCHAR (7) * </summary>
        public string TevfikatOranKod
        {
            get { return tevfikatOranKod; }
            set
            {
                tevfikatOranKod = value;
                OnPropertyChanged("TevfikatOranKod");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string OzelMatKDVKod
        {
            get { return ozelMatKDVKod; }
            set
            {
                ozelMatKDVKod = value;
                OnPropertyChanged("OzelMatKDVKod");
            }
        }

        /// <summary> VARCHAR (40) * </summary>
        public string ProjeKodu
        {
            get { return projeKodu; }
            set
            {
                projeKodu = value;
                OnPropertyChanged("ProjeKodu");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short EArsivTeslimSekli
        {
            get { return eArsivTeslimSekli; }
            set
            {
                eArsivTeslimSekli = value;
                OnPropertyChanged("EArsivTeslimSekli");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short EArsivFaturaTipi
        {
            get { return eArsivFaturaTipi; }
            set
            {
                eArsivFaturaTipi = value;
                OnPropertyChanged("EArsivFaturaTipi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short EArsivFaturaDurum
        {
            get { return eArsivFaturaDurum; }
            set
            {
                eArsivFaturaDurum = value;
                OnPropertyChanged("EArsivFaturaDurum");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal IskontoTutar
        {
            get { return iskontoTutar; }
            set
            {
                iskontoTutar = value;
                OnPropertyChanged("IskontoTutar");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal IskontoTutar1
        {
            get { return iskontoTutar1; }
            set
            {
                iskontoTutar1 = value;
                OnPropertyChanged("IskontoTutar1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal IskontoTutar2
        {
            get { return iskontoTutar2; }
            set
            {
                iskontoTutar2 = value;
                OnPropertyChanged("IskontoTutar2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal IskontoTutar3
        {
            get { return iskontoTutar3; }
            set
            {
                iskontoTutar3 = value;
                OnPropertyChanged("IskontoTutar3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal IskontoTutar4
        {
            get { return iskontoTutar4; }
            set
            {
                iskontoTutar4 = value;
                OnPropertyChanged("IskontoTutar4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal IskontoTutar5
        {
            get { return iskontoTutar5; }
            set
            {
                iskontoTutar5 = value;
                OnPropertyChanged("IskontoTutar5");
            }
        }

        /// <summary> VARCHAR (128) * </summary>
        public string Not1
        {
            get { return not1; }
            set
            {
                not1 = value;
                OnPropertyChanged("Not1");
            }
        }

        /// <summary> VARCHAR (128) * </summary>
        public string Not2
        {
            get { return not2; }
            set
            {
                not2 = value;
                OnPropertyChanged("Not2");
            }
        }

        /// <summary> VARCHAR (128) * </summary>
        public string Not3
        {
            get { return not3; }
            set
            {
                not3 = value;
                OnPropertyChanged("Not3");
            }
        }

        /// <summary> VARCHAR (128) * </summary>
        public string Not4
        {
            get { return not4; }
            set
            {
                not4 = value;
                OnPropertyChanged("Not4");
            }
        }

        /// <summary> VARCHAR (128) * </summary>
        public string Not5
        {
            get { return not5; }
            set
            {
                not5 = value;
                OnPropertyChanged("Not5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal BrutAgirlik
        {
            get { return brutAgirlik; }
            set
            {
                brutAgirlik = value;
                OnPropertyChanged("BrutAgirlik");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal NetAgirlik
        {
            get { return netAgirlik; }
            set
            {
                netAgirlik = value;
                OnPropertyChanged("NetAgirlik");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DaraAgirlik
        {
            get { return daraAgirlik; }
            set
            {
                daraAgirlik = value;
                OnPropertyChanged("DaraAgirlik");
            }
        }

        /// <summary> VARCHAR (4) * </summary>
        public string BirimAgirlik
        {
            get { return birimAgirlik; }
            set
            {
                birimAgirlik = value;
                OnPropertyChanged("BirimAgirlik");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal BrutHacim
        {
            get { return brutHacim; }
            set
            {
                brutHacim = value;
                OnPropertyChanged("BrutHacim");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal NetHacim
        {
            get { return netHacim; }
            set
            {
                netHacim = value;
                OnPropertyChanged("NetHacim");
            }
        }

        /// <summary> VARCHAR (4) * </summary>
        public string BirimHacim
        {
            get { return birimHacim; }
            set
            {
                birimHacim = value;
                OnPropertyChanged("BirimHacim");
            }
        }

        /// <summary> VARCHAR (4) * </summary>
        public string KapTipi
        {
            get { return kapTipi; }
            set
            {
                kapTipi = value;
                OnPropertyChanged("KapTipi");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KapAdet
        {
            get { return kapAdet; }
            set
            {
                kapAdet = value;
                OnPropertyChanged("KapAdet");
            }
        }

        /// <summary> INT (4) * </summary>
        public int FormBaBsTarih
        {
            get { return formBaBsTarih; }
            set
            {
                formBaBsTarih = value;
                OnPropertyChanged("FormBaBsTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int YOKCZRaporNo
        {
            get { return yOKCZRaporNo; }
            set
            {
                yOKCZRaporNo = value;
                OnPropertyChanged("YOKCZRaporNo");
            }
        }

        /// <summary> REAL (4) * </summary>
        public float KDVDahilKalemIskontoOran
        {
            get { return kDVDahilKalemIskontoOran; }
            set
            {
                kDVDahilKalemIskontoOran = value;
                OnPropertyChanged("KDVDahilKalemIskontoOran");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KDVDahilKalemOranIskontosu
        {
            get { return kDVDahilKalemOranIskontosu; }
            set
            {
                kDVDahilKalemOranIskontosu = value;
                OnPropertyChanged("KDVDahilKalemOranIskontosu");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KDVDahilKalemTutarIskontosu
        {
            get { return kDVDahilKalemTutarIskontosu; }
            set
            {
                kDVDahilKalemTutarIskontosu = value;
                OnPropertyChanged("KDVDahilKalemTutarIskontosu");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KDVDahilIskonto
        {
            get { return kDVDahilIskonto; }
            set
            {
                kDVDahilIskonto = value;
                OnPropertyChanged("KDVDahilIskonto");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal KDVIstisnaTutar
        {
            get { return kDVIstisnaTutar; }
            set
            {
                kDVIstisnaTutar = value;
                OnPropertyChanged("KDVIstisnaTutar");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string UreticiChk
        {
            get { return ureticiChk; }
            set
            {
                ureticiChk = value;
                OnPropertyChanged("UreticiChk");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal IhracatMiktar_Dagitilan
        {
            get { return ihracatMiktar_Dagitilan; }
            set
            {
                ihracatMiktar_Dagitilan = value;
                OnPropertyChanged("IhracatMiktar_Dagitilan");
            }
        }

        /// <summary> VARCHAR (40) * </summary>
        public string FaturaID
        {
            get { return faturaID; }
            set
            {
                faturaID = value;
                OnPropertyChanged("FaturaID");
            }
        }

        /// <summary> INT (4) IdentityKey * </summary>
        public int ROW_ID => rOW_ID;

        /// <summary> TIMESTAMP (8) * </summary>
        public byte[] timestamp
        {
            get { return _timestamp; }
            set
            {
                _timestamp = value;
                OnPropertyChanged("timestamp");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_IslemTur
        {
            private get { return _pk_IslemTur; }
            set
            {
                _pk_IslemTur = value;
                OnPropertyChanged("pk_IslemTur");
            }
        }

        /// <summary> VARCHAR (16) PRIMARY KEY * </summary>
        public string pk_EvrakNo
        {
            private get { return _pk_EvrakNo; }
            set
            {
                _pk_EvrakNo = value;
                OnPropertyChanged("pk_EvrakNo");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_Tarih
        {
            private get { return _pk_Tarih; }
            set
            {
                _pk_Tarih = value;
                OnPropertyChanged("pk_Tarih");
            }
        }

        /// <summary> VARCHAR (20) PRIMARY KEY * </summary>
        public string pk_Chk
        {
            private get { return _pk_Chk; }
            set
            {
                _pk_Chk = value;
                OnPropertyChanged("pk_Chk");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_KynkEvrakTip
        {
            private get { return _pk_KynkEvrakTip; }
            set
            {
                _pk_KynkEvrakTip = value;
                OnPropertyChanged("pk_KynkEvrakTip");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_SiraNo
        {
            private get { return _pk_SiraNo; }
            set
            {
                _pk_SiraNo = value;
                OnPropertyChanged("pk_SiraNo");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.STI";
        string[] info_PrimaryKeys = { "pk_IslemTur", "pk_EvrakNo", "pk_Tarih", "pk_Chk", "pk_KynkEvrakTip", "pk_SiraNo" };
        string[] info_IdentityKeys = { "ROW_ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public STI()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += STI_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void STI_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!ChangedProperties.Contains(e.PropertyName))
            {
                ChangedProperties.Add(e.PropertyName);
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion  /// Tablo Bilgileri & Metodlar
    }
    #endregion /// STI Class
}