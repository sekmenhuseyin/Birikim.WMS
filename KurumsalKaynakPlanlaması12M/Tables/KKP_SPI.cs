using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{

    /*SPI BİLGİ
     * Tutar, DovizTutar, ToplamIskonto, KDV alanları Private SET olarak ayarlanmıştır. 
     * Bu alanlar Hesaplanarak bulunacak alanlardır.
     * 
     * 
     * 
     * 
     */


     [DbTablo("FINSAT6", "FINSAT6", "SPI")]
    public class KKP_SPI : INotifyPropertyChanged
    {


        #region Fields
        private short _IslemTur;
        private string _EvrakNo;
        private int _Tarih;
        private string _Chk;
        private short _SiraNo;
        private short _IslemTip;
        private string _MalKodu;
        private short _KynkEvrakTip;
        private decimal _Miktar;
        private decimal _Fiyat;
        private decimal _Tutar;
        private string _DovizCinsi;
        private decimal _DovizKuru;
        private decimal _DovizTutar;
        private decimal _DvzBirimFiyat;
        private string _Birim;
        private decimal _BirimFiyat;
        private decimal _BirimMiktar;
        private decimal _Iskonto;
        private double _IskontoOran;
        private decimal _ToplamIskonto;
        private decimal _KDV;
        private double _KDVOran;
        private short _KDVDahilHaric;
        private string _Aciklama;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private string _Kod4;
        private string _Kod5;
        private string _Kod6;
        private string _Kod7;
        private string _Kod8;
        private string _Kod9;
        private string _Kod10;
        private short _Kod11;
        private short _Kod12;
        private decimal _Kod13;
        private decimal _Kod14;
        private int _EvrakTarih;
        private decimal _Miktar2;
        private decimal _Tutar2;
        private int _Tarih2;
        private int _VadeTarih;
        private string _Depo;
        private string _Vasita;
        private string _SeriNo;
        private string _IrsaliyeNo;
        private int _IrsaliyeTarih;
        private decimal _PromosyonMiktar;
        private string _Aciklama2;
        private string _AsilEvrakNo;
        private decimal _Masraf;
        private decimal _TeslimMiktar;
        private int _TahTeslimTarih;
        private int _SonTeslimTarih;
        private short _SiparisDurumu;
        private string _RezervasyonEvrakNo;
        private int _RezervasyonTarihi;
        private decimal _KapatilanMiktar;
        private double _IskontoOran1;
        private short _IskOran1Net;
        private double _IskontoOran2;
        private short _IskOran2Net;
        private double _IskontoOran3;
        private short _IskOran3Net;
        private double _IskontoOran4;
        private short _IskOran4Net;
        private double _IskontoOran5;
        private short _IskOran5Net;
        private decimal _KlmTutarIsk;
        private short _KlmTutarIskNet;
        private string _TeslimChk;
        private string _ButceKod;
        private string _FytListeNo;
        private string _MasrafMerkez;
        private short _DvzTL;
        private string _RenkBedenKod1;
        private string _RenkBedenKod2;
        private string _RenkBedenKod3;
        private string _RenkBedenKod4;
        private string _BarkodNo;
        private double _Katsayi;
        private short _Operator;
        private short _ValorGun;
        private short _KayitTuru;
        private string _Nesne1;
        private string _Nesne2;
        private string _Nesne3;
        private string _TesTemMalKod;
        private decimal _Miktar3;
        private decimal _Tutar3;
        private short _SiraNo2;
        private decimal _BlkMiktar;
        private int _BlkTarih;
        private short _BlkDurumu;
        private int _KurTarihi;
        private short _AnaEvrakTip;
        private string _GuvenlikKod;
        private string _Kaydeden;
        private int _KayitTarih;
        private int _KayitSaat;
        private short _KayitKaynak;
        private string _KayitSurum;
        private string _Degistiren;
        private int _DegisTarih;
        private int _DegisSaat;
        private short _DegisKaynak;
        private string _DegisSurum;
        private short _CheckSum;
        private string _TeklifEvrakNo;
        private int _TeklifTarihi;
        private decimal _OnayMiktar;
        private int _SonKullanimTarihi;
        private short _DvzKurCinsi;
        private string _TevfikatOran;
        private decimal _TevfikatTutar;
        private int _Tarih3;
        private int _Tarih4;
        private int _Tarih5;
        private int _Tarih6;
        private string _TevfikatOranKod;
        private string _ProjeKodu;
        private decimal _IskontoTutar;
        private decimal _IskontoTutar1;
        private decimal _IskontoTutar2;
        private decimal _IskontoTutar3;
        private decimal _IskontoTutar4;
        private decimal _IskontoTutar5;
        private string _Not1;
        private string _Not2;
        private string _Not3;
        private string _Not4;
        private string _Not5;
        private string _TeklifAciklamasi;
        private int _ROW_ID;

        #endregion Fields



        #region table Properties

        [DbAlan("IslemTur", SqlDbType.SmallInt, 2, false, true, false)]
        public short IslemTur
        {
            get { return _IslemTur; }
            set
            {
                if (_IslemTur != value)
                {
                    OnPropertyChanged("IslemTur");
                    _IslemTur = value;
                }
            }
        }

        [DbAlan("EvrakNo", SqlDbType.VarChar, 16, false, true, false)]
        public string EvrakNo
        {
            get { return _EvrakNo; }
            set
            {
                if (_EvrakNo != value)
                {
                    OnPropertyChanged("EvrakNo");
                    _EvrakNo = value;
                }
            }
        }

        [DbAlan("Tarih", SqlDbType.Int, 4, false, true, false)]
        public int Tarih
        {
            get { return _Tarih; }
            set
            {
                if (_Tarih != value)
                {
                    OnPropertyChanged("Tarih");
                    _Tarih = value;
                }
            }
        }

        [DbAlan("Chk", SqlDbType.VarChar, 20, false, true, false)]
        public string Chk
        {
            get { return _Chk; }
            set
            {
                if (_Chk != value)
                {
                    OnPropertyChanged("Chk");
                    _Chk = value;
                }
            }
        }

        [DbAlan("SiraNo", SqlDbType.SmallInt, 2, false, true, false)]
        public short SiraNo
        {
            get { return _SiraNo; }
            set
            {
                if (_SiraNo != value)
                {
                    OnPropertyChanged("SiraNo");
                    _SiraNo = value;
                }
            }
        }

        [DbAlan("IslemTip", SqlDbType.SmallInt, 2)]
        public short IslemTip
        {
            get { return _IslemTip; }
            set
            {
                if (_IslemTip != value)
                {
                    OnPropertyChanged("IslemTip");
                    _IslemTip = value;
                }
            }
        }

        [DbAlan("MalKodu", SqlDbType.VarChar, 30)]
        public string MalKodu
        {
            get { return _MalKodu; }
            set
            {
                if (_MalKodu != value)
                {
                    OnPropertyChanged("MalKodu");
                    _MalKodu = value;
                }
            }
        }

        [DbAlan("KynkEvrakTip", SqlDbType.SmallInt, 2, false, true, false)]
        public short KynkEvrakTip
        {
            get { return _KynkEvrakTip; }
            set
            {
                if (_KynkEvrakTip != value)
                {
                    OnPropertyChanged("KynkEvrakTip");
                    _KynkEvrakTip = value;
                }
            }
        }

        [DbAlan("Miktar", SqlDbType.Decimal, 13)]
        public decimal Miktar
        {
            get { return _Miktar; }
            set
            {
                if (_Miktar != value)
                {
                    OnPropertyChanged("Miktar");
                    _Miktar = value;
                }
            }
        }

        [DbAlan("Fiyat", SqlDbType.Decimal, 13)]
        public decimal Fiyat
        {
            get { return _Fiyat; }
            set
            {
                if (_Fiyat != value)
                {
                    OnPropertyChanged("Fiyat");
                    _Fiyat = value;
                }
            }
        }

        [DbAlan("Tutar", SqlDbType.Decimal, 13)]
        public decimal Tutar
        {
            get { return _Tutar; }
            set
            {
                if (_Tutar != value)
                {
                    OnPropertyChanged("Tutar");
                    _Tutar = value;
                }
            }
        }

        [DbAlan("DovizCinsi", SqlDbType.VarChar, 3)]
        public string DovizCinsi
        {
            get { return _DovizCinsi; }
            set
            {
                if (_DovizCinsi != value)
                {
                    OnPropertyChanged("DovizCinsi");
                    _DovizCinsi = value;
                }
            }
        }

        [DbAlan("DovizKuru", SqlDbType.Decimal, 13)]
        public decimal DovizKuru
        {
            get { return _DovizKuru; }
            set
            {
                if (_DovizKuru != value)
                {
                    OnPropertyChanged("DovizKuru");
                    _DovizKuru = value;
                }
            }
        }

        [DbAlan("DovizTutar", SqlDbType.Decimal, 13)]
        public decimal DovizTutar
        {
            get { return _DovizTutar; }
            set
            {
                if (_DovizTutar != value)
                {
                    OnPropertyChanged("DovizTutar");
                    _DovizTutar = value;
                }
            }
        }

        [DbAlan("DvzBirimFiyat", SqlDbType.Decimal, 13)]
        public decimal DvzBirimFiyat
        {
            get { return _DvzBirimFiyat; }
            set
            {
                if (_DvzBirimFiyat != value)
                {
                    OnPropertyChanged("DvzBirimFiyat");
                    _DvzBirimFiyat = value;
                }
            }
        }

        [DbAlan("Birim", SqlDbType.VarChar, 4)]
        public string Birim
        {
            get { return _Birim; }
            set
            {
                if (_Birim != value)
                {
                    OnPropertyChanged("Birim");
                    _Birim = value;
                }
            }
        }

        [DbAlan("BirimFiyat", SqlDbType.Decimal, 13)]
        public decimal BirimFiyat
        {
            get { return _BirimFiyat; }
            set
            {
                if (_BirimFiyat != value)
                {
                    OnPropertyChanged("BirimFiyat");
                    _BirimFiyat = value;
                }
            }
        }

        [DbAlan("BirimMiktar", SqlDbType.Decimal, 13)]
        public decimal BirimMiktar
        {
            get { return _BirimMiktar; }
            set
            {
                if (_BirimMiktar != value)
                {
                    OnPropertyChanged("BirimMiktar");
                    _BirimMiktar = value;
                }
            }
        }

        [DbAlan("Iskonto", SqlDbType.Decimal, 13)]
        public decimal Iskonto
        {
            get { return _Iskonto; }
            set
            {
                if (_Iskonto != value)
                {
                    OnPropertyChanged("Iskonto");
                    _Iskonto = value;
                }
            }
        }

        [DbAlan("IskontoOran", SqlDbType.Real, 4)]
        public double IskontoOran
        {
            get { return _IskontoOran; }
            set
            {
                if (_IskontoOran != value)
                {
                    OnPropertyChanged("IskontoOran");
                    _IskontoOran = value;
                }
            }
        }

        [DbAlan("ToplamIskonto", SqlDbType.Decimal, 13)]
        public decimal ToplamIskonto
        {
            get { return _ToplamIskonto; }
            set
            {
                if (_ToplamIskonto != value)
                {
                    OnPropertyChanged("ToplamIskonto");
                    _ToplamIskonto = value;
                }
            }
        }

        [DbAlan("KDV", SqlDbType.Decimal, 13)]
        public decimal KDV
        {
            get { return _KDV; }
            set
            {
                if (_KDV != value)
                {
                    OnPropertyChanged("KDV");
                    _KDV = value;
                }
            }
        }

        [DbAlan("KDVOran", SqlDbType.Real, 4)]
        public double KDVOran
        {
            get { return _KDVOran; }
            set
            {
                if (_KDVOran != value)
                {
                    OnPropertyChanged("KDVOran");
                    _KDVOran = value;
                }
            }
        }

        [DbAlan("KDVDahilHaric", SqlDbType.SmallInt, 2)]
        public short KDVDahilHaric
        {
            get { return _KDVDahilHaric; }
            set
            {
                if (_KDVDahilHaric != value)
                {
                    OnPropertyChanged("KDVDahilHaric");
                    _KDVDahilHaric = value;
                }
            }
        }

        [DbAlan("Aciklama", SqlDbType.VarChar, 64)]
        public string Aciklama
        {
            get { return _Aciklama; }
            set
            {
                if (_Aciklama != value)
                {
                    OnPropertyChanged("Aciklama");
                    _Aciklama = value;
                }
            }
        }

        [DbAlan("Kod1", SqlDbType.VarChar, 10)]
        public string Kod1
        {
            get { return _Kod1; }
            set
            {
                if (_Kod1 != value)
                {
                    OnPropertyChanged("Kod1");
                    _Kod1 = value;
                }
            }
        }

        [DbAlan("Kod2", SqlDbType.VarChar, 10)]
        public string Kod2
        {
            get { return _Kod2; }
            set
            {
                if (_Kod2 != value)
                {
                    OnPropertyChanged("Kod2");
                    _Kod2 = value;
                }
            }
        }

        [DbAlan("Kod3", SqlDbType.VarChar, 10)]
        public string Kod3
        {
            get { return _Kod3; }
            set
            {
                if (_Kod3 != value)
                {
                    OnPropertyChanged("Kod3");
                    _Kod3 = value;
                }
            }
        }

        [DbAlan("Kod4", SqlDbType.VarChar, 10)]
        public string Kod4
        {
            get { return _Kod4; }
            set
            {
                if (_Kod4 != value)
                {
                    OnPropertyChanged("Kod4");
                    _Kod4 = value;
                }
            }
        }

        [DbAlan("Kod5", SqlDbType.VarChar, 20)]
        public string Kod5
        {
            get { return _Kod5; }
            set
            {
                if (_Kod5 != value)
                {
                    OnPropertyChanged("Kod5");
                    _Kod5 = value;
                }
            }
        }

        [DbAlan("Kod6", SqlDbType.VarChar, 10)]
        public string Kod6
        {
            get { return _Kod6; }
            set
            {
                if (_Kod6 != value)
                {
                    OnPropertyChanged("Kod6");
                    _Kod6 = value;
                }
            }
        }

        [DbAlan("Kod7", SqlDbType.VarChar, 10)]
        public string Kod7
        {
            get { return _Kod7; }
            set
            {
                if (_Kod7 != value)
                {
                    OnPropertyChanged("Kod7");
                    _Kod7 = value;
                }
            }
        }

        [DbAlan("Kod8", SqlDbType.VarChar, 20)]
        public string Kod8
        {
            get { return _Kod8; }
            set
            {
                if (_Kod8 != value)
                {
                    OnPropertyChanged("Kod8");
                    _Kod8 = value;
                }
            }
        }

        [DbAlan("Kod9", SqlDbType.VarChar, 20)]
        public string Kod9
        {
            get { return _Kod9; }
            set
            {
                if (_Kod9 != value)
                {
                    OnPropertyChanged("Kod9");
                    _Kod9 = value;
                }
            }
        }

        [DbAlan("Kod10", SqlDbType.VarChar, 20)]
        public string Kod10
        {
            get { return _Kod10; }
            set
            {
                if (_Kod10 != value)
                {
                    OnPropertyChanged("Kod10");
                    _Kod10 = value;
                }
            }
        }

        [DbAlan("Kod11", SqlDbType.SmallInt, 2)]
        public short Kod11
        {
            get { return _Kod11; }
            set
            {
                if (_Kod11 != value)
                {
                    OnPropertyChanged("Kod11");
                    _Kod11 = value;
                }
            }
        }

        [DbAlan("Kod12", SqlDbType.SmallInt, 2)]
        public short Kod12
        {
            get { return _Kod12; }
            set
            {
                if (_Kod12 != value)
                {
                    OnPropertyChanged("Kod12");
                    _Kod12 = value;
                }
            }
        }

        [DbAlan("Kod13", SqlDbType.Decimal, 13)]
        public decimal Kod13
        {
            get { return _Kod13; }
            set
            {
                if (_Kod13 != value)
                {
                    OnPropertyChanged("Kod13");
                    _Kod13 = value;
                }
            }
        }

        [DbAlan("Kod14", SqlDbType.Decimal, 13)]
        public decimal Kod14
        {
            get { return _Kod14; }
            set
            {
                if (_Kod14 != value)
                {
                    OnPropertyChanged("Kod14");
                    _Kod14 = value;
                }
            }
        }

        [DbAlan("EvrakTarih", SqlDbType.Int, 4)]
        public int EvrakTarih
        {
            get { return _EvrakTarih; }
            set
            {
                if (_EvrakTarih != value)
                {
                    OnPropertyChanged("EvrakTarih");
                    _EvrakTarih = value;
                }
            }
        }

        [DbAlan("Miktar2", SqlDbType.Decimal, 13)]
        public decimal Miktar2
        {
            get { return _Miktar2; }
            set
            {
                if (_Miktar2 != value)
                {
                    OnPropertyChanged("Miktar2");
                    _Miktar2 = value;
                }
            }
        }

        [DbAlan("Tutar2", SqlDbType.Decimal, 13)]
        public decimal Tutar2
        {
            get { return _Tutar2; }
            set
            {
                if (_Tutar2 != value)
                {
                    OnPropertyChanged("Tutar2");
                    _Tutar2 = value;
                }
            }
        }

        [DbAlan("Tarih2", SqlDbType.Int, 4)]
        public int Tarih2
        {
            get { return _Tarih2; }
            set
            {
                if (_Tarih2 != value)
                {
                    OnPropertyChanged("Tarih2");
                    _Tarih2 = value;
                }
            }
        }

        [DbAlan("VadeTarih", SqlDbType.Int, 4)]
        public int VadeTarih
        {
            get { return _VadeTarih; }
            set
            {
                if (_VadeTarih != value)
                {
                    OnPropertyChanged("VadeTarih");
                    _VadeTarih = value;
                }
            }
        }

        [DbAlan("Depo", SqlDbType.VarChar, 5)]
        public string Depo
        {
            get { return _Depo; }
            set
            {
                if (_Depo != value)
                {
                    OnPropertyChanged("Depo");
                    _Depo = value;
                }
            }
        }

        [DbAlan("Vasita", SqlDbType.VarChar, 12)]
        public string Vasita
        {
            get { return _Vasita; }
            set
            {
                if (_Vasita != value)
                {
                    OnPropertyChanged("Vasita");
                    _Vasita = value;
                }
            }
        }

        [DbAlan("SeriNo", SqlDbType.VarChar, 30)]
        public string SeriNo
        {
            get { return _SeriNo; }
            set
            {
                if (_SeriNo != value)
                {
                    OnPropertyChanged("SeriNo");
                    _SeriNo = value;
                }
            }
        }

        [DbAlan("IrsaliyeNo", SqlDbType.VarChar, 16)]
        public string IrsaliyeNo
        {
            get { return _IrsaliyeNo; }
            set
            {
                if (_IrsaliyeNo != value)
                {
                    OnPropertyChanged("IrsaliyeNo");
                    _IrsaliyeNo = value;
                }
            }
        }

        [DbAlan("IrsaliyeTarih", SqlDbType.Int, 4)]
        public int IrsaliyeTarih
        {
            get { return _IrsaliyeTarih; }
            set
            {
                if (_IrsaliyeTarih != value)
                {
                    OnPropertyChanged("IrsaliyeTarih");
                    _IrsaliyeTarih = value;
                }
            }
        }

        [DbAlan("PromosyonMiktar", SqlDbType.Decimal, 13)]
        public decimal PromosyonMiktar
        {
            get { return _PromosyonMiktar; }
            set
            {
                if (_PromosyonMiktar != value)
                {
                    OnPropertyChanged("PromosyonMiktar");
                    _PromosyonMiktar = value;
                }
            }
        }

        [DbAlan("Aciklama2", SqlDbType.VarChar, 64)]
        public string Aciklama2
        {
            get { return _Aciklama2; }
            set
            {
                if (_Aciklama2 != value)
                {
                    OnPropertyChanged("Aciklama2");
                    _Aciklama2 = value;
                }
            }
        }

        [DbAlan("AsilEvrakNo", SqlDbType.VarChar, 16)]
        public string AsilEvrakNo
        {
            get { return _AsilEvrakNo; }
            set
            {
                if (_AsilEvrakNo != value)
                {
                    OnPropertyChanged("AsilEvrakNo");
                    _AsilEvrakNo = value;
                }
            }
        }

        [DbAlan("Masraf", SqlDbType.Decimal, 13)]
        public decimal Masraf
        {
            get { return _Masraf; }
            set
            {
                if (_Masraf != value)
                {
                    OnPropertyChanged("Masraf");
                    _Masraf = value;
                }
            }
        }

        [DbAlan("TeslimMiktar", SqlDbType.Decimal, 13)]
        public decimal TeslimMiktar
        {
            get { return _TeslimMiktar; }
            set
            {
                if (_TeslimMiktar != value)
                {
                    OnPropertyChanged("TeslimMiktar");
                    _TeslimMiktar = value;
                }
            }
        }

        [DbAlan("TahTeslimTarih", SqlDbType.Int, 4)]
        public int TahTeslimTarih
        {
            get { return _TahTeslimTarih; }
            set
            {
                if (_TahTeslimTarih != value)
                {
                    OnPropertyChanged("TahTeslimTarih");
                    _TahTeslimTarih = value;
                }
            }
        }

        [DbAlan("SonTeslimTarih", SqlDbType.Int, 4)]
        public int SonTeslimTarih
        {
            get { return _SonTeslimTarih; }
            set
            {
                if (_SonTeslimTarih != value)
                {
                    OnPropertyChanged("SonTeslimTarih");
                    _SonTeslimTarih = value;
                }
            }
        }

        [DbAlan("SiparisDurumu", SqlDbType.SmallInt, 2)]
        public short SiparisDurumu
        {
            get { return _SiparisDurumu; }
            set
            {
                if (_SiparisDurumu != value)
                {
                    OnPropertyChanged("SiparisDurumu");
                    _SiparisDurumu = value;
                }
            }
        }

        [DbAlan("RezervasyonEvrakNo", SqlDbType.VarChar, 16)]
        public string RezervasyonEvrakNo
        {
            get { return _RezervasyonEvrakNo; }
            set
            {
                if (_RezervasyonEvrakNo != value)
                {
                    OnPropertyChanged("RezervasyonEvrakNo");
                    _RezervasyonEvrakNo = value;
                }
            }
        }

        [DbAlan("RezervasyonTarihi", SqlDbType.Int, 4)]
        public int RezervasyonTarihi
        {
            get { return _RezervasyonTarihi; }
            set
            {
                if (_RezervasyonTarihi != value)
                {
                    OnPropertyChanged("RezervasyonTarihi");
                    _RezervasyonTarihi = value;
                }
            }
        }

        [DbAlan("KapatilanMiktar", SqlDbType.Decimal, 13)]
        public decimal KapatilanMiktar
        {
            get { return _KapatilanMiktar; }
            set
            {
                if (_KapatilanMiktar != value)
                {
                    OnPropertyChanged("KapatilanMiktar");
                    _KapatilanMiktar = value;
                }
            }
        }

        [DbAlan("IskontoOran1", SqlDbType.Real, 4)]
        public double IskontoOran1
        {
            get { return _IskontoOran1; }
            set
            {
                if (_IskontoOran1 != value)
                {
                    OnPropertyChanged("IskontoOran1");
                    _IskontoOran1 = value;
                }
            }
        }

        [DbAlan("IskOran1Net", SqlDbType.SmallInt, 2)]
        public short IskOran1Net
        {
            get { return _IskOran1Net; }
            set
            {
                if (_IskOran1Net != value)
                {
                    OnPropertyChanged("IskOran1Net");
                    _IskOran1Net = value;
                }
            }
        }

        [DbAlan("IskontoOran2", SqlDbType.Real, 4)]
        public double IskontoOran2
        {
            get { return _IskontoOran2; }
            set
            {
                if (_IskontoOran2 != value)
                {
                    OnPropertyChanged("IskontoOran2");
                    _IskontoOran2 = value;
                }
            }
        }

        [DbAlan("IskOran2Net", SqlDbType.SmallInt, 2)]
        public short IskOran2Net
        {
            get { return _IskOran2Net; }
            set
            {
                if (_IskOran2Net != value)
                {
                    OnPropertyChanged("IskOran2Net");
                    _IskOran2Net = value;
                }
            }
        }

        [DbAlan("IskontoOran3", SqlDbType.Real, 4)]
        public double IskontoOran3
        {
            get { return _IskontoOran3; }
            set
            {
                if (_IskontoOran3 != value)
                {
                    OnPropertyChanged("IskontoOran3");
                    _IskontoOran3 = value;
                }
            }
        }

        [DbAlan("IskOran3Net", SqlDbType.SmallInt, 2)]
        public short IskOran3Net
        {
            get { return _IskOran3Net; }
            set
            {
                if (_IskOran3Net != value)
                {
                    OnPropertyChanged("IskOran3Net");
                    _IskOran3Net = value;
                }
            }
        }

        [DbAlan("IskontoOran4", SqlDbType.Real, 4)]
        public double IskontoOran4
        {
            get { return _IskontoOran4; }
            set
            {
                if (_IskontoOran4 != value)
                {
                    OnPropertyChanged("IskontoOran4");
                    _IskontoOran4 = value;
                }
            }
        }

        [DbAlan("IskOran4Net", SqlDbType.SmallInt, 2)]
        public short IskOran4Net
        {
            get { return _IskOran4Net; }
            set
            {
                if (_IskOran4Net != value)
                {
                    OnPropertyChanged("IskOran4Net");
                    _IskOran4Net = value;
                }
            }
        }

        [DbAlan("IskontoOran5", SqlDbType.Real, 4)]
        public double IskontoOran5
        {
            get { return _IskontoOran5; }
            set
            {
                if (_IskontoOran5 != value)
                {
                    OnPropertyChanged("IskontoOran5");
                    _IskontoOran5 = value;
                }
            }
        }

        [DbAlan("IskOran5Net", SqlDbType.SmallInt, 2)]
        public short IskOran5Net
        {
            get { return _IskOran5Net; }
            set
            {
                if (_IskOran5Net != value)
                {
                    OnPropertyChanged("IskOran5Net");
                    _IskOran5Net = value;
                }
            }
        }

        [DbAlan("KlmTutarIsk", SqlDbType.Decimal, 13)]
        public decimal KlmTutarIsk
        {
            get { return _KlmTutarIsk; }
            set
            {
                if (_KlmTutarIsk != value)
                {
                    OnPropertyChanged("KlmTutarIsk");
                    _KlmTutarIsk = value;
                }
            }
        }

        [DbAlan("KlmTutarIskNet", SqlDbType.SmallInt, 2)]
        public short KlmTutarIskNet
        {
            get { return _KlmTutarIskNet; }
            set
            {
                if (_KlmTutarIskNet != value)
                {
                    OnPropertyChanged("KlmTutarIskNet");
                    _KlmTutarIskNet = value;
                }
            }
        }

        [DbAlan("TeslimChk", SqlDbType.VarChar, 20)]
        public string TeslimChk
        {
            get { return _TeslimChk; }
            set
            {
                if (_TeslimChk != value)
                {
                    OnPropertyChanged("TeslimChk");
                    _TeslimChk = value;
                }
            }
        }

        [DbAlan("ButceKod", SqlDbType.VarChar, 50)]
        public string ButceKod
        {
            get { return _ButceKod; }
            set
            {
                if (_ButceKod != value)
                {
                    OnPropertyChanged("ButceKod");
                    _ButceKod = value;
                }
            }
        }

        [DbAlan("FytListeNo", SqlDbType.VarChar, 16)]
        public string FytListeNo
        {
            get { return _FytListeNo; }
            set
            {
                if (_FytListeNo != value)
                {
                    OnPropertyChanged("FytListeNo");
                    _FytListeNo = value;
                }
            }
        }

        [DbAlan("MasrafMerkez", SqlDbType.VarChar, 20)]
        public string MasrafMerkez
        {
            get { return _MasrafMerkez; }
            set
            {
                if (_MasrafMerkez != value)
                {
                    OnPropertyChanged("MasrafMerkez");
                    _MasrafMerkez = value;
                }
            }
        }

        [DbAlan("DvzTL", SqlDbType.SmallInt, 2)]
        public short DvzTL
        {
            get { return _DvzTL; }
            set
            {
                if (_DvzTL != value)
                {
                    OnPropertyChanged("DvzTL");
                    _DvzTL = value;
                }
            }
        }

        [DbAlan("RenkBedenKod1", SqlDbType.VarChar, 16)]
        public string RenkBedenKod1
        {
            get { return _RenkBedenKod1; }
            set
            {
                if (_RenkBedenKod1 != value)
                {
                    OnPropertyChanged("RenkBedenKod1");
                    _RenkBedenKod1 = value;
                }
            }
        }

        [DbAlan("RenkBedenKod2", SqlDbType.VarChar, 16)]
        public string RenkBedenKod2
        {
            get { return _RenkBedenKod2; }
            set
            {
                if (_RenkBedenKod2 != value)
                {
                    OnPropertyChanged("RenkBedenKod2");
                    _RenkBedenKod2 = value;
                }
            }
        }

        [DbAlan("RenkBedenKod3", SqlDbType.VarChar, 16)]
        public string RenkBedenKod3
        {
            get { return _RenkBedenKod3; }
            set
            {
                if (_RenkBedenKod3 != value)
                {
                    OnPropertyChanged("RenkBedenKod3");
                    _RenkBedenKod3 = value;
                }
            }
        }

        [DbAlan("RenkBedenKod4", SqlDbType.VarChar, 16)]
        public string RenkBedenKod4
        {
            get { return _RenkBedenKod4; }
            set
            {
                if (_RenkBedenKod4 != value)
                {
                    OnPropertyChanged("RenkBedenKod4");
                    _RenkBedenKod4 = value;
                }
            }
        }

        [DbAlan("BarkodNo", SqlDbType.VarChar, 52)]
        public string BarkodNo
        {
            get { return _BarkodNo; }
            set
            {
                if (_BarkodNo != value)
                {
                    OnPropertyChanged("BarkodNo");
                    _BarkodNo = value;
                }
            }
        }

        [DbAlan("Katsayi", SqlDbType.Float, 8)]
        public double Katsayi
        {
            get { return _Katsayi; }
            set
            {
                if (_Katsayi != value)
                {
                    OnPropertyChanged("Katsayi");
                    _Katsayi = value;
                }
            }
        }

        [DbAlan("Operator", SqlDbType.SmallInt, 2)]
        public short Operator
        {
            get { return _Operator; }
            set
            {
                if (_Operator != value)
                {
                    OnPropertyChanged("Operator");
                    _Operator = value;
                }
            }
        }

        [DbAlan("ValorGun", SqlDbType.SmallInt, 2)]
        public short ValorGun
        {
            get { return _ValorGun; }
            set
            {
                if (_ValorGun != value)
                {
                    OnPropertyChanged("ValorGun");
                    _ValorGun = value;
                }
            }
        }

        [DbAlan("KayitTuru", SqlDbType.SmallInt, 2)]
        public short KayitTuru
        {
            get { return _KayitTuru; }
            set
            {
                if (_KayitTuru != value)
                {
                    OnPropertyChanged("KayitTuru");
                    _KayitTuru = value;
                }
            }
        }

        [DbAlan("Nesne1", SqlDbType.VarChar, 254)]
        public string Nesne1
        {
            get { return _Nesne1; }
            set
            {
                if (_Nesne1 != value)
                {
                    OnPropertyChanged("Nesne1");
                    _Nesne1 = value;
                }
            }
        }

        [DbAlan("Nesne2", SqlDbType.VarChar, 254)]
        public string Nesne2
        {
            get { return _Nesne2; }
            set
            {
                if (_Nesne2 != value)
                {
                    OnPropertyChanged("Nesne2");
                    _Nesne2 = value;
                }
            }
        }

        [DbAlan("Nesne3", SqlDbType.VarChar, 254)]
        public string Nesne3
        {
            get { return _Nesne3; }
            set
            {
                if (_Nesne3 != value)
                {
                    OnPropertyChanged("Nesne3");
                    _Nesne3 = value;
                }
            }
        }

        [DbAlan("TesTemMalKod", SqlDbType.VarChar, 30)]
        public string TesTemMalKod
        {
            get { return _TesTemMalKod; }
            set
            {
                if (_TesTemMalKod != value)
                {
                    OnPropertyChanged("TesTemMalKod");
                    _TesTemMalKod = value;
                }
            }
        }

        [DbAlan("Miktar3", SqlDbType.Decimal, 13)]
        public decimal Miktar3
        {
            get { return _Miktar3; }
            set
            {
                if (_Miktar3 != value)
                {
                    OnPropertyChanged("Miktar3");
                    _Miktar3 = value;
                }
            }
        }

        [DbAlan("Tutar3", SqlDbType.Decimal, 13)]
        public decimal Tutar3
        {
            get { return _Tutar3; }
            set
            {
                if (_Tutar3 != value)
                {
                    OnPropertyChanged("Tutar3");
                    _Tutar3 = value;
                }
            }
        }

        [DbAlan("SiraNo2", SqlDbType.SmallInt, 2)]
        public short SiraNo2
        {
            get { return _SiraNo2; }
            set
            {
                if (_SiraNo2 != value)
                {
                    OnPropertyChanged("SiraNo2");
                    _SiraNo2 = value;
                }
            }
        }

        [DbAlan("BlkMiktar", SqlDbType.Decimal, 13)]
        public decimal BlkMiktar
        {
            get { return _BlkMiktar; }
            set
            {
                if (_BlkMiktar != value)
                {
                    OnPropertyChanged("BlkMiktar");
                    _BlkMiktar = value;
                }
            }
        }

        [DbAlan("BlkTarih", SqlDbType.Int, 4)]
        public int BlkTarih
        {
            get { return _BlkTarih; }
            set
            {
                if (_BlkTarih != value)
                {
                    OnPropertyChanged("BlkTarih");
                    _BlkTarih = value;
                }
            }
        }

        [DbAlan("BlkDurumu", SqlDbType.SmallInt, 2)]
        public short BlkDurumu
        {
            get { return _BlkDurumu; }
            set
            {
                if (_BlkDurumu != value)
                {
                    OnPropertyChanged("BlkDurumu");
                    _BlkDurumu = value;
                }
            }
        }

        [DbAlan("KurTarihi", SqlDbType.Int, 4)]
        public int KurTarihi
        {
            get { return _KurTarihi; }
            set
            {
                if (_KurTarihi != value)
                {
                    OnPropertyChanged("KurTarihi");
                    _KurTarihi = value;
                }
            }
        }

        [DbAlan("AnaEvrakTip", SqlDbType.SmallInt, 2)]
        public short AnaEvrakTip
        {
            get { return _AnaEvrakTip; }
            set
            {
                if (_AnaEvrakTip != value)
                {
                    OnPropertyChanged("AnaEvrakTip");
                    _AnaEvrakTip = value;
                }
            }
        }

        [DbAlan("GuvenlikKod", SqlDbType.VarChar, 2)]
        public string GuvenlikKod
        {
            get { return _GuvenlikKod; }
            set
            {
                if (_GuvenlikKod != value)
                {
                    OnPropertyChanged("GuvenlikKod");
                    _GuvenlikKod = value;
                }
            }
        }

        [DbAlan("Kaydeden", SqlDbType.VarChar, 5)]
        public string Kaydeden
        {
            get { return _Kaydeden; }
            set
            {
                if (_Kaydeden != value)
                {
                    OnPropertyChanged("Kaydeden");
                    _Kaydeden = value;
                }
            }
        }

        [DbAlan("KayitTarih", SqlDbType.Int, 4)]
        public int KayitTarih
        {
            get { return _KayitTarih; }
            set
            {
                if (_KayitTarih != value)
                {
                    OnPropertyChanged("KayitTarih");
                    _KayitTarih = value;
                }
            }
        }

        [DbAlan("KayitSaat", SqlDbType.Int, 4)]
        public int KayitSaat
        {
            get { return _KayitSaat; }
            set
            {
                if (_KayitSaat != value)
                {
                    OnPropertyChanged("KayitSaat");
                    _KayitSaat = value;
                }
            }
        }

        [DbAlan("KayitKaynak", SqlDbType.SmallInt, 2)]
        public short KayitKaynak
        {
            get { return _KayitKaynak; }
            set
            {
                if (_KayitKaynak != value)
                {
                    OnPropertyChanged("KayitKaynak");
                    _KayitKaynak = value;
                }
            }
        }

        [DbAlan("KayitSurum", SqlDbType.VarChar, 12)]
        public string KayitSurum
        {
            get { return _KayitSurum; }
            set
            {
                if (_KayitSurum != value)
                {
                    OnPropertyChanged("KayitSurum");
                    _KayitSurum = value;
                }
            }
        }

        [DbAlan("Degistiren", SqlDbType.VarChar, 5)]
        public string Degistiren
        {
            get { return _Degistiren; }
            set
            {
                if (_Degistiren != value)
                {
                    OnPropertyChanged("Degistiren");
                    _Degistiren = value;
                }
            }
        }

        [DbAlan("DegisTarih", SqlDbType.Int, 4)]
        public int DegisTarih
        {
            get { return _DegisTarih; }
            set
            {
                if (_DegisTarih != value)
                {
                    OnPropertyChanged("DegisTarih");
                    _DegisTarih = value;
                }
            }
        }

        [DbAlan("DegisSaat", SqlDbType.Int, 4)]
        public int DegisSaat
        {
            get { return _DegisSaat; }
            set
            {
                if (_DegisSaat != value)
                {
                    OnPropertyChanged("DegisSaat");
                    _DegisSaat = value;
                }
            }
        }

        [DbAlan("DegisKaynak", SqlDbType.SmallInt, 2)]
        public short DegisKaynak
        {
            get { return _DegisKaynak; }
            set
            {
                if (_DegisKaynak != value)
                {
                    OnPropertyChanged("DegisKaynak");
                    _DegisKaynak = value;
                }
            }
        }

        [DbAlan("DegisSurum", SqlDbType.VarChar, 12)]
        public string DegisSurum
        {
            get { return _DegisSurum; }
            set
            {
                if (_DegisSurum != value)
                {
                    OnPropertyChanged("DegisSurum");
                    _DegisSurum = value;
                }
            }
        }

        [DbAlan("CheckSum", SqlDbType.SmallInt, 2)]
        public short CheckSum
        {
            get { return _CheckSum; }
            set
            {
                if (_CheckSum != value)
                {
                    OnPropertyChanged("CheckSum");
                    _CheckSum = value;
                }
            }
        }

        [DbAlan("TeklifEvrakNo", SqlDbType.VarChar, 16)]
        public string TeklifEvrakNo
        {
            get { return _TeklifEvrakNo; }
            set
            {
                if (_TeklifEvrakNo != value)
                {
                    OnPropertyChanged("TeklifEvrakNo");
                    _TeklifEvrakNo = value;
                }
            }
        }

        [DbAlan("TeklifTarihi", SqlDbType.Int, 4)]
        public int TeklifTarihi
        {
            get { return _TeklifTarihi; }
            set
            {
                if (_TeklifTarihi != value)
                {
                    OnPropertyChanged("TeklifTarihi");
                    _TeklifTarihi = value;
                }
            }
        }

        [DbAlan("OnayMiktar", SqlDbType.Decimal, 13)]
        public decimal OnayMiktar
        {
            get { return _OnayMiktar; }
            set
            {
                if (_OnayMiktar != value)
                {
                    OnPropertyChanged("OnayMiktar");
                    _OnayMiktar = value;
                }
            }
        }

        [DbAlan("SonKullanimTarihi", SqlDbType.Int, 4)]
        public int SonKullanimTarihi
        {
            get { return _SonKullanimTarihi; }
            set
            {
                if (_SonKullanimTarihi != value)
                {
                    OnPropertyChanged("SonKullanimTarihi");
                    _SonKullanimTarihi = value;
                }
            }
        }

        [DbAlan("DvzKurCinsi", SqlDbType.SmallInt, 2)]
        public short DvzKurCinsi
        {
            get { return _DvzKurCinsi; }
            set
            {
                if (_DvzKurCinsi != value)
                {
                    OnPropertyChanged("DvzKurCinsi");
                    _DvzKurCinsi = value;
                }
            }
        }

        [DbAlan("TevfikatOran", SqlDbType.VarChar, 7)]
        public string TevfikatOran
        {
            get { return _TevfikatOran; }
            set
            {
                if (_TevfikatOran != value)
                {
                    OnPropertyChanged("TevfikatOran");
                    _TevfikatOran = value;
                }
            }
        }

        [DbAlan("TevfikatTutar", SqlDbType.Decimal, 13)]
        public decimal TevfikatTutar
        {
            get { return _TevfikatTutar; }
            set
            {
                if (_TevfikatTutar != value)
                {
                    OnPropertyChanged("TevfikatTutar");
                    _TevfikatTutar = value;
                }
            }
        }

        [DbAlan("Tarih3", SqlDbType.Int, 4)]
        public int Tarih3
        {
            get { return _Tarih3; }
            set
            {
                if (_Tarih3 != value)
                {
                    OnPropertyChanged("Tarih3");
                    _Tarih3 = value;
                }
            }
        }

        [DbAlan("Tarih4", SqlDbType.Int, 4)]
        public int Tarih4
        {
            get { return _Tarih4; }
            set
            {
                if (_Tarih4 != value)
                {
                    OnPropertyChanged("Tarih4");
                    _Tarih4 = value;
                }
            }
        }

        [DbAlan("Tarih5", SqlDbType.Int, 4)]
        public int Tarih5
        {
            get { return _Tarih5; }
            set
            {
                if (_Tarih5 != value)
                {
                    OnPropertyChanged("Tarih5");
                    _Tarih5 = value;
                }
            }
        }

        [DbAlan("Tarih6", SqlDbType.Int, 4)]
        public int Tarih6
        {
            get { return _Tarih6; }
            set
            {
                if (_Tarih6 != value)
                {
                    OnPropertyChanged("Tarih6");
                    _Tarih6 = value;
                }
            }
        }

        [DbAlan("TevfikatOranKod", SqlDbType.VarChar, 7)]
        public string TevfikatOranKod
        {
            get { return _TevfikatOranKod; }
            set
            {
                if (_TevfikatOranKod != value)
                {
                    OnPropertyChanged("TevfikatOranKod");
                    _TevfikatOranKod = value;
                }
            }
        }

        [DbAlan("ProjeKodu", SqlDbType.VarChar, 40)]
        public string ProjeKodu
        {
            get { return _ProjeKodu; }
            set
            {
                if (_ProjeKodu != value)
                {
                    OnPropertyChanged("ProjeKodu");
                    _ProjeKodu = value;
                }
            }
        }

        [DbAlan("IskontoTutar", SqlDbType.Decimal, 13)]
        public decimal IskontoTutar
        {
            get { return _IskontoTutar; }
            set
            {
                if (_IskontoTutar != value)
                {
                    OnPropertyChanged("IskontoTutar");
                    _IskontoTutar = value;
                }
            }
        }

        [DbAlan("IskontoTutar1", SqlDbType.Decimal, 13)]
        public decimal IskontoTutar1
        {
            get { return _IskontoTutar1; }
            set
            {
                if (_IskontoTutar1 != value)
                {
                    OnPropertyChanged("IskontoTutar1");
                    _IskontoTutar1 = value;
                }
            }
        }

        [DbAlan("IskontoTutar2", SqlDbType.Decimal, 13)]
        public decimal IskontoTutar2
        {
            get { return _IskontoTutar2; }
            set
            {
                if (_IskontoTutar2 != value)
                {
                    OnPropertyChanged("IskontoTutar2");
                    _IskontoTutar2 = value;
                }
            }
        }

        [DbAlan("IskontoTutar3", SqlDbType.Decimal, 13)]
        public decimal IskontoTutar3
        {
            get { return _IskontoTutar3; }
            set
            {
                if (_IskontoTutar3 != value)
                {
                    OnPropertyChanged("IskontoTutar3");
                    _IskontoTutar3 = value;
                }
            }
        }

        [DbAlan("IskontoTutar4", SqlDbType.Decimal, 13)]
        public decimal IskontoTutar4
        {
            get { return _IskontoTutar4; }
            set
            {
                if (_IskontoTutar4 != value)
                {
                    OnPropertyChanged("IskontoTutar4");
                    _IskontoTutar4 = value;
                }
            }
        }

        [DbAlan("IskontoTutar5", SqlDbType.Decimal, 13)]
        public decimal IskontoTutar5
        {
            get { return _IskontoTutar5; }
            set
            {
                if (_IskontoTutar5 != value)
                {
                    OnPropertyChanged("IskontoTutar5");
                    _IskontoTutar5 = value;
                }
            }
        }

        [DbAlan("Not1", SqlDbType.VarChar, 128)]
        public string Not1
        {
            get { return _Not1; }
            set
            {
                if (_Not1 != value)
                {
                    OnPropertyChanged("Not1");
                    _Not1 = value;
                }
            }
        }

        [DbAlan("Not2", SqlDbType.VarChar, 128)]
        public string Not2
        {
            get { return _Not2; }
            set
            {
                if (_Not2 != value)
                {
                    OnPropertyChanged("Not2");
                    _Not2 = value;
                }
            }
        }

        [DbAlan("Not3", SqlDbType.VarChar, 128)]
        public string Not3
        {
            get { return _Not3; }
            set
            {
                if (_Not3 != value)
                {
                    OnPropertyChanged("Not3");
                    _Not3 = value;
                }
            }
        }

        [DbAlan("Not4", SqlDbType.VarChar, 128)]
        public string Not4
        {
            get { return _Not4; }
            set
            {
                if (_Not4 != value)
                {
                    OnPropertyChanged("Not4");
                    _Not4 = value;
                }
            }
        }

        [DbAlan("Not5", SqlDbType.VarChar, 128)]
        public string Not5
        {
            get { return _Not5; }
            set
            {
                if (_Not5 != value)
                {
                    OnPropertyChanged("Not5");
                    _Not5 = value;
                }
            }
        }
        [DbAlan("", SqlDbType.VarChar, 1000)]
        public string TeklifAciklamasi
        {
            get { return _TeklifAciklamasi; }
            set
            {
                if (_TeklifAciklamasi != value)
                {
                    OnPropertyChanged("TeklifAciklamasi");
                    _TeklifAciklamasi = value;
                }
            }
        }
        [DbAlan("ROW_ID", SqlDbType.Int, 4, true, false, false)]
        public int ROW_ID
        {
            get { return _ROW_ID; }
            set
            {
                if (_ROW_ID != value)
                {
                    OnPropertyChanged("ROW_ID");
                    _ROW_ID = value;
                }
            }
        }

        #endregion table Properties




        #region property changes events functions

        public event PropertyChangedEventHandler PropertyChanged;
        private List<PropertyValue> _ChangedList = new List<PropertyValue>();
        public PropertyValue[] ChangedList { get { return _ChangedList.ToArray(); } }



        internal void SPI_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyValue propval = _ChangedList.Find(t => t.PropertiName == e.PropertyName);
            if (propval == null)
            {
                propval = new PropertyValue();
                propval.PropertiName = e.PropertyName;
                propval.OldValue = this.GetType().GetProperty(e.PropertyName).GetValue(this, null);
                _ChangedList.Add(propval);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public object GetOldValue(string propertyName)
        {
            PropertyValue val = _ChangedList.Find(t => t.PropertiName == propertyName);
            if (val == null)
                return null;
            return val.OldValue;
            //if (val == null)
            //{
            //    PropertyInfo info = this.GetType().GetProperty(propertyName);
            //    if (info == null)
            //        throw new Exception(string.Format("{0} alan adı bulunamadı!! (GetOldValue())", propertyName));
            //    return info.GetValue(this, null);
            //}
            //else
            //    return val.OldValue;
        }


        public void UpdateChanges()
        {
            _ChangedList.Clear();
        }

        #endregion property changes events functions



        #region kendi alanlarımız

        #region Hesaplanan Kendi Alanlarımız

        public decimal Isk1Tutar { get; private set; }
        public decimal Isk2Tutar { get; private set; }
        public decimal Isk3Tutar { get; private set; }
        public decimal Isk4Tutar { get; private set; }
        public decimal Isk5Tutar { get; private set; }

        public decimal Isk1TutarDvz { get { return DovizKuru > 0 ? Math.Round(Isk1Tutar / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0; } }
        public decimal Isk2TutarDvz { get { return DovizKuru > 0 ? Math.Round(Isk2Tutar / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0; } }
        public decimal Isk3TutarDvz { get { return DovizKuru > 0 ? Math.Round(Isk3Tutar / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0; } }
        public decimal Isk4TutarDvz { get { return DovizKuru > 0 ? Math.Round(Isk4Tutar / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0; } }
        public decimal Isk5TutarDvz { get { return DovizKuru > 0 ? Math.Round(Isk5Tutar / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0; } }

        public decimal ToplamIskontoDvz { get { return DovizKuru > 0 ? Math.Round(ToplamIskonto / DovizKuru, 2, MidpointRounding.AwayFromZero) : 0; } }

        #endregion Hesaplanan Kendi Alanlarımız



        public DateTime TahTeslimTarih_D
        {
            get { return new DateTime(1900, 1, 1).AddDays(TahTeslimTarih - 2); }
            set { TahTeslimTarih = Convert.ToInt32(value.ToOADate()); }
        }


        [DbAlan(AlanAdi="", DbTye=SqlDbType.VarChar)]
        public string MalAdi { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TalepEden { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TesisKodu { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TesisAdi { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string SatinAlmaci { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TalepNo { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string Unvan { get; set; }

        #endregion

        public KKP_SPI()
        {

        }

        public KKP_SPI(KKPSiparisTip sipTip)
        {
            Data.DefaultValueSetSPI(this, sipTip);
        }


        public KKP_SPI Copy()
        {
            return (KKP_SPI)this.MemberwiseClone();          
        }


        public void SatirHesapla()
        {
            #region TUTAR
            if (DvzTL == 1)
            {
                DovizTutar = Math.Round(BirimMiktar * BirimFiyat, 2, MidpointRounding.AwayFromZero);
                Tutar = Math.Round(DovizTutar * DovizKuru, 2, MidpointRounding.AwayFromZero);
                DvzBirimFiyat = BirimFiyat;
            }
            else
            {
                Tutar = Math.Round(BirimMiktar * BirimFiyat, 2, MidpointRounding.AwayFromZero);
                DvzBirimFiyat = 0;
                if (DovizKuru > 0)
                {
                    DovizTutar = Math.Round(Tutar / DovizKuru, 2, MidpointRounding.AwayFromZero);
                    DvzBirimFiyat = Math.Round(DovizTutar / BirimMiktar, 6, MidpointRounding.AwayFromZero);
                }
            }
            #endregion TUTAR

            #region IskontoToplamları

            Isk1Tutar = Math.Round((Tutar * (decimal)IskontoOran1) / 100, 2, MidpointRounding.AwayFromZero);

            Isk2Tutar = Math.Round(
                ((Tutar - Isk1Tutar) * (decimal)IskontoOran2) / 100
                , 2, MidpointRounding.AwayFromZero);

            Isk3Tutar = Math.Round(
                ((Tutar - Isk1Tutar - Isk2Tutar)
                * (decimal)IskontoOran3) / 100
                , 2, MidpointRounding.AwayFromZero);

            Isk4Tutar = Math.Round(
                ((Tutar - Isk1Tutar - Isk2Tutar - Isk3Tutar)
                * (decimal)IskontoOran4) / 100
                , 2, MidpointRounding.AwayFromZero);

            Isk5Tutar = Math.Round(
                ((Tutar - Isk1Tutar - Isk2Tutar - Isk3Tutar - Isk4Tutar)
                * (decimal)IskontoOran5) / 100
                , 2, MidpointRounding.AwayFromZero);




            
            ToplamIskonto = Isk1Tutar + Isk2Tutar + Isk3Tutar + Isk4Tutar + Isk5Tutar;
            IskontoTutar = ToplamIskonto;

            IskontoTutar1 = Isk1Tutar;
            IskontoTutar2 = Isk2Tutar;
            IskontoTutar3 = Isk3Tutar;
            IskontoTutar4 = Isk4Tutar;
            IskontoTutar5 = Isk5Tutar;

            #endregion

            #region KDV
            KDV = 0;
            if (KDVOran > 0)
            {
                KDV = Math.Round(
                    ((Tutar - ToplamIskonto) * (decimal)KDVOran) / 100
                    , 2, MidpointRounding.AwayFromZero);
            }
            #endregion KDV

            //#region Miktar

            //Miktar = BirimMiktar;
            //if (Operator == 0 && Katsayi > 0)
            //    Miktar = BirimMiktar / (decimal)Katsayi;
            //else if (Operator == 1)
            //    Miktar = BirimMiktar * (decimal)Katsayi;
            //#endregion Miktar

            #region Fiyat
            if (Miktar == 0)
                Fiyat = 0;
            else
                Fiyat = Math.Round(Tutar / Miktar, 6, MidpointRounding.AwayFromZero);
            #endregion Fiyat

        }
    }
}
