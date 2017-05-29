using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{
    [DbTablo("FINSAT6","FINSAT6","STI")]
    public class KKP_STI : INotifyPropertyChanged
    {


        #region Fields
        private short _IslemTur;
        private string _EvrakNo;
        private int _Tarih;
        private string _Chk;
        private short _KynkEvrakTip;
        private short _SiraNo;
        private short _IrsFat;
        private short _IslemTip;
        private string _MalKodu;
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
        private short _OtvDahilHaric;
        private decimal _OtvTutar;
        private string _GtkListeNo;
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
        private string _Depo;
        private string _Vasita;
        private string _SeriNo;
        private int _SevkTarih;
        private decimal _PromosyonMiktar;
        private decimal _Miktar2;
        private decimal _Tutar2;
        private int _Tarih2;
        private int _VadeTarih;
        private decimal _Masraf;
        private decimal _Maliyet;
        private short _MlyYontem;
        private string _MhsKod;
        private string _MhsKarsiKod;
        private string _MasrafMerkezi;
        private short _MhsDurum;
        private short _MlyMhs;
        private short _MhsTabloNo;
        private int _EvrakTarih;
        private short _SiparisSiraNo;
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
        private decimal _FatMiktar;
        private string _TesTemMalKod;
        private short _DvzTL;
        private string _BarkodNo;
        private double _Katsayi;
        private short _Operator;
        private short _ValorGun;
        private string _KaynakIrsEvrakNo;
        private int _KaynakIrsTarih;
        private string _KaynakIIFEvrakNo;
        private int _KaynakIIFTarih;
        private string _KaynakSiparisNo;
        private int _KaynakSiparisTarih;
        private string _ErekIFEvrakNo;
        private short _ErekIFKEvrakTip;
        private decimal _ErekIFMiktar;
        private string _ErekIIFEvrakNo;
        private short _ErekIIFKEvrakTip;
        private decimal _ErekIIFMiktar;
        private string _RenkBedenKod1;
        private string _RenkBedenKod2;
        private string _RenkBedenKod3;
        private string _RenkBedenKod4;
        private short _KayitTuru;
        private string _Nesne1;
        private string _Nesne2;
        private string _Nesne3;
        private short _IrsFat2;
        private decimal _Miktar3;
        private decimal _Tutar3;
        private short _SiraNo2;
        private int _KurTarihi;
        private decimal _KrediBorcTutar;
        private decimal _AktiflesenKrediFaizi;
        private int _Kredi_Donem_BaslangicTarih;
        private int _Kredi_Donem_BitisTarih;
        private int _Kredi_Donem_VadeTarih;
        private decimal _Kredi_Donem_VadeFarkiTutar;
        private decimal _ReelOlmayanFinansmanMaliyet;
        private short _KrediArindirmaSekli;
        private short _FinansmanGiderTuru;
        private int _Duz_Yapilan_Yil;
        private short _Duz_Yapilan_Donem;
        private short _Duz_Yontemi;
        private string _Duz_Mhs_Hesap_Kodu;
        private short _Duz_Mhs_Durumu;
        private double _Duz_Stok_Devir_Hizi;
        private double _Duz_Katsayisi;
        private decimal _Duz_Esas_Tutar;
        private decimal _Duz_Tutar;
        private short _Duz_Mly_Yontemi;
        private decimal _Duz_Mly_Tarihi_Mly_Tutar;
        private decimal _Duz_Mly_Satilan_Mal_Mly_Tutar;
        private string _Duz_Mly_Mhs_Hesap_Kodu;
        private short _Duz_Mly_Mhs_Durumu;
        private short _AnaEvrakTip;
        private string _KartDovizCinsi;
        private decimal _KartDovizKuru;
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
        private string _FFEvrakNo;
        private int _FFTarih;
        private short _KaynakSiraNo;
        private int _SonKullanimTarihi;
        private short _DvzKurCinsi;
        private decimal _DepoIadeEdilenMiktar;
        private string _TevfikatOran;
        private decimal _TevfikatTutar;
        private decimal _Masraf1;
        private decimal _Masraf2;
        private string _IthalatDosyaNo;
        private short _IthalatMDagDurum;
        private int _Tarih3;
        private int _Tarih4;
        private int _Tarih5;
        private int _Tarih6;
        private short _EFatTip;
        private short _EFatDurum;
        private decimal _ToplamKlmIskonto;
        private short _IthalatMDagYontem;
        private string _KDVMuafAck;
        private string _KarsiMalKodu;
        private string _UreticiMalKodu;
        private double _StopajOran;
        private decimal _StopajTutar;
        private short _OTVTevkifatVarYok;
        private string _Aciklama2;
        private string _eFatIrsReferansNo;
        private string _IhracatDosyaNo;
        private string _TevfikatOranKod;
        private string _OzelMatKDVKod;
        private string _ProjeKodu;
        private short _EArsivTeslimSekli;
        private short _EArsivFaturaTipi;
        private short _EArsivFaturaDurum;
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
        private decimal _BrutAgirlik;
        private decimal _NetAgirlik;
        private decimal _DaraAgirlik;
        private string _BirimAgirlik;
        private decimal _BrutHacim;
        private decimal _NetHacim;
        private string _BirimHacim;
        private string _KapTipi;
        private decimal _KapAdet;
        private int _FormBaBsTarih;
        private int _YOKCZRaporNo;
        private double _KDVDahilKalemIskontoOran;
        private decimal _KDVDahilKalemOranIskontosu;
        private decimal _KDVDahilKalemTutarIskontosu;
        private decimal _KDVDahilIskonto;
        private decimal _KDVIstisnaTutar;
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

        [DbAlan("IrsFat", SqlDbType.SmallInt, 2)]
        public short IrsFat
        {
            get { return _IrsFat; }
            set
            {
                if (_IrsFat != value)
                {
                    OnPropertyChanged("IrsFat");
                    _IrsFat = value;
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

        [DbAlan("OtvDahilHaric", SqlDbType.SmallInt, 2)]
        public short OtvDahilHaric
        {
            get { return _OtvDahilHaric; }
            set
            {
                if (_OtvDahilHaric != value)
                {
                    OnPropertyChanged("OtvDahilHaric");
                    _OtvDahilHaric = value;
                }
            }
        }

        [DbAlan("OtvTutar", SqlDbType.Decimal, 13)]
        public decimal OtvTutar
        {
            get { return _OtvTutar; }
            set
            {
                if (_OtvTutar != value)
                {
                    OnPropertyChanged("OtvTutar");
                    _OtvTutar = value;
                }
            }
        }

        [DbAlan("GtkListeNo", SqlDbType.VarChar, 2)]
        public string GtkListeNo
        {
            get { return _GtkListeNo; }
            set
            {
                if (_GtkListeNo != value)
                {
                    OnPropertyChanged("GtkListeNo");
                    _GtkListeNo = value;
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

        [DbAlan("SevkTarih", SqlDbType.Int, 4)]
        public int SevkTarih
        {
            get { return _SevkTarih; }
            set
            {
                if (_SevkTarih != value)
                {
                    OnPropertyChanged("SevkTarih");
                    _SevkTarih = value;
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

        [DbAlan("Maliyet", SqlDbType.Decimal, 13)]
        public decimal Maliyet
        {
            get { return _Maliyet; }
            set
            {
                if (_Maliyet != value)
                {
                    OnPropertyChanged("Maliyet");
                    _Maliyet = value;
                }
            }
        }

        [DbAlan("MlyYontem", SqlDbType.SmallInt, 2)]
        public short MlyYontem
        {
            get { return _MlyYontem; }
            set
            {
                if (_MlyYontem != value)
                {
                    OnPropertyChanged("MlyYontem");
                    _MlyYontem = value;
                }
            }
        }

        [DbAlan("MhsKod", SqlDbType.VarChar, 50)]
        public string MhsKod
        {
            get { return _MhsKod; }
            set
            {
                if (_MhsKod != value)
                {
                    OnPropertyChanged("MhsKod");
                    _MhsKod = value;
                }
            }
        }

        [DbAlan("MhsKarsiKod", SqlDbType.VarChar, 50)]
        public string MhsKarsiKod
        {
            get { return _MhsKarsiKod; }
            set
            {
                if (_MhsKarsiKod != value)
                {
                    OnPropertyChanged("MhsKarsiKod");
                    _MhsKarsiKod = value;
                }
            }
        }

        [DbAlan("MasrafMerkezi", SqlDbType.VarChar, 20)]
        public string MasrafMerkezi
        {
            get { return _MasrafMerkezi; }
            set
            {
                if (_MasrafMerkezi != value)
                {
                    OnPropertyChanged("MasrafMerkezi");
                    _MasrafMerkezi = value;
                }
            }
        }

        [DbAlan("MhsDurum", SqlDbType.SmallInt, 2)]
        public short MhsDurum
        {
            get { return _MhsDurum; }
            set
            {
                if (_MhsDurum != value)
                {
                    OnPropertyChanged("MhsDurum");
                    _MhsDurum = value;
                }
            }
        }

        [DbAlan("MlyMhs", SqlDbType.SmallInt, 2)]
        public short MlyMhs
        {
            get { return _MlyMhs; }
            set
            {
                if (_MlyMhs != value)
                {
                    OnPropertyChanged("MlyMhs");
                    _MlyMhs = value;
                }
            }
        }

        [DbAlan("MhsTabloNo", SqlDbType.SmallInt, 2)]
        public short MhsTabloNo
        {
            get { return _MhsTabloNo; }
            set
            {
                if (_MhsTabloNo != value)
                {
                    OnPropertyChanged("MhsTabloNo");
                    _MhsTabloNo = value;
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

        [DbAlan("SiparisSiraNo", SqlDbType.SmallInt, 2)]
        public short SiparisSiraNo
        {
            get { return _SiparisSiraNo; }
            set
            {
                if (_SiparisSiraNo != value)
                {
                    OnPropertyChanged("SiparisSiraNo");
                    _SiparisSiraNo = value;
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

        [DbAlan("FatMiktar", SqlDbType.Decimal, 13)]
        public decimal FatMiktar
        {
            get { return _FatMiktar; }
            set
            {
                if (_FatMiktar != value)
                {
                    OnPropertyChanged("FatMiktar");
                    _FatMiktar = value;
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

        [DbAlan("KaynakIrsEvrakNo", SqlDbType.VarChar, 16)]
        public string KaynakIrsEvrakNo
        {
            get { return _KaynakIrsEvrakNo; }
            set
            {
                if (_KaynakIrsEvrakNo != value)
                {
                    OnPropertyChanged("KaynakIrsEvrakNo");
                    _KaynakIrsEvrakNo = value;
                }
            }
        }

        [DbAlan("KaynakIrsTarih", SqlDbType.Int, 4)]
        public int KaynakIrsTarih
        {
            get { return _KaynakIrsTarih; }
            set
            {
                if (_KaynakIrsTarih != value)
                {
                    OnPropertyChanged("KaynakIrsTarih");
                    _KaynakIrsTarih = value;
                }
            }
        }

        [DbAlan("KaynakIIFEvrakNo", SqlDbType.VarChar, 16)]
        public string KaynakIIFEvrakNo
        {
            get { return _KaynakIIFEvrakNo; }
            set
            {
                if (_KaynakIIFEvrakNo != value)
                {
                    OnPropertyChanged("KaynakIIFEvrakNo");
                    _KaynakIIFEvrakNo = value;
                }
            }
        }

        [DbAlan("KaynakIIFTarih", SqlDbType.Int, 4)]
        public int KaynakIIFTarih
        {
            get { return _KaynakIIFTarih; }
            set
            {
                if (_KaynakIIFTarih != value)
                {
                    OnPropertyChanged("KaynakIIFTarih");
                    _KaynakIIFTarih = value;
                }
            }
        }

        [DbAlan("KaynakSiparisNo", SqlDbType.VarChar, 16)]
        public string KaynakSiparisNo
        {
            get { return _KaynakSiparisNo; }
            set
            {
                if (_KaynakSiparisNo != value)
                {
                    OnPropertyChanged("KaynakSiparisNo");
                    _KaynakSiparisNo = value;
                }
            }
        }

        [DbAlan("KaynakSiparisTarih", SqlDbType.Int, 4)]
        public int KaynakSiparisTarih
        {
            get { return _KaynakSiparisTarih; }
            set
            {
                if (_KaynakSiparisTarih != value)
                {
                    OnPropertyChanged("KaynakSiparisTarih");
                    _KaynakSiparisTarih = value;
                }
            }
        }

        [DbAlan("ErekIFEvrakNo", SqlDbType.VarChar, 16)]
        public string ErekIFEvrakNo
        {
            get { return _ErekIFEvrakNo; }
            set
            {
                if (_ErekIFEvrakNo != value)
                {
                    OnPropertyChanged("ErekIFEvrakNo");
                    _ErekIFEvrakNo = value;
                }
            }
        }

        [DbAlan("ErekIFKEvrakTip", SqlDbType.SmallInt, 2)]
        public short ErekIFKEvrakTip
        {
            get { return _ErekIFKEvrakTip; }
            set
            {
                if (_ErekIFKEvrakTip != value)
                {
                    OnPropertyChanged("ErekIFKEvrakTip");
                    _ErekIFKEvrakTip = value;
                }
            }
        }

        [DbAlan("ErekIFMiktar", SqlDbType.Decimal, 13)]
        public decimal ErekIFMiktar
        {
            get { return _ErekIFMiktar; }
            set
            {
                if (_ErekIFMiktar != value)
                {
                    OnPropertyChanged("ErekIFMiktar");
                    _ErekIFMiktar = value;
                }
            }
        }

        [DbAlan("ErekIIFEvrakNo", SqlDbType.VarChar, 16)]
        public string ErekIIFEvrakNo
        {
            get { return _ErekIIFEvrakNo; }
            set
            {
                if (_ErekIIFEvrakNo != value)
                {
                    OnPropertyChanged("ErekIIFEvrakNo");
                    _ErekIIFEvrakNo = value;
                }
            }
        }

        [DbAlan("ErekIIFKEvrakTip", SqlDbType.SmallInt, 2)]
        public short ErekIIFKEvrakTip
        {
            get { return _ErekIIFKEvrakTip; }
            set
            {
                if (_ErekIIFKEvrakTip != value)
                {
                    OnPropertyChanged("ErekIIFKEvrakTip");
                    _ErekIIFKEvrakTip = value;
                }
            }
        }

        [DbAlan("ErekIIFMiktar", SqlDbType.Decimal, 13)]
        public decimal ErekIIFMiktar
        {
            get { return _ErekIIFMiktar; }
            set
            {
                if (_ErekIIFMiktar != value)
                {
                    OnPropertyChanged("ErekIIFMiktar");
                    _ErekIIFMiktar = value;
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

        [DbAlan("IrsFat2", SqlDbType.SmallInt, 2)]
        public short IrsFat2
        {
            get { return _IrsFat2; }
            set
            {
                if (_IrsFat2 != value)
                {
                    OnPropertyChanged("IrsFat2");
                    _IrsFat2 = value;
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

        [DbAlan("KrediBorcTutar", SqlDbType.Decimal, 13)]
        public decimal KrediBorcTutar
        {
            get { return _KrediBorcTutar; }
            set
            {
                if (_KrediBorcTutar != value)
                {
                    OnPropertyChanged("KrediBorcTutar");
                    _KrediBorcTutar = value;
                }
            }
        }

        [DbAlan("AktiflesenKrediFaizi", SqlDbType.Decimal, 13)]
        public decimal AktiflesenKrediFaizi
        {
            get { return _AktiflesenKrediFaizi; }
            set
            {
                if (_AktiflesenKrediFaizi != value)
                {
                    OnPropertyChanged("AktiflesenKrediFaizi");
                    _AktiflesenKrediFaizi = value;
                }
            }
        }

        [DbAlan("Kredi_Donem_BaslangicTarih", SqlDbType.Int, 4)]
        public int Kredi_Donem_BaslangicTarih
        {
            get { return _Kredi_Donem_BaslangicTarih; }
            set
            {
                if (_Kredi_Donem_BaslangicTarih != value)
                {
                    OnPropertyChanged("Kredi_Donem_BaslangicTarih");
                    _Kredi_Donem_BaslangicTarih = value;
                }
            }
        }

        [DbAlan("Kredi_Donem_BitisTarih", SqlDbType.Int, 4)]
        public int Kredi_Donem_BitisTarih
        {
            get { return _Kredi_Donem_BitisTarih; }
            set
            {
                if (_Kredi_Donem_BitisTarih != value)
                {
                    OnPropertyChanged("Kredi_Donem_BitisTarih");
                    _Kredi_Donem_BitisTarih = value;
                }
            }
        }

        [DbAlan("Kredi_Donem_VadeTarih", SqlDbType.Int, 4)]
        public int Kredi_Donem_VadeTarih
        {
            get { return _Kredi_Donem_VadeTarih; }
            set
            {
                if (_Kredi_Donem_VadeTarih != value)
                {
                    OnPropertyChanged("Kredi_Donem_VadeTarih");
                    _Kredi_Donem_VadeTarih = value;
                }
            }
        }

        [DbAlan("Kredi_Donem_VadeFarkiTutar", SqlDbType.Decimal, 13)]
        public decimal Kredi_Donem_VadeFarkiTutar
        {
            get { return _Kredi_Donem_VadeFarkiTutar; }
            set
            {
                if (_Kredi_Donem_VadeFarkiTutar != value)
                {
                    OnPropertyChanged("Kredi_Donem_VadeFarkiTutar");
                    _Kredi_Donem_VadeFarkiTutar = value;
                }
            }
        }

        [DbAlan("ReelOlmayanFinansmanMaliyet", SqlDbType.Decimal, 13)]
        public decimal ReelOlmayanFinansmanMaliyet
        {
            get { return _ReelOlmayanFinansmanMaliyet; }
            set
            {
                if (_ReelOlmayanFinansmanMaliyet != value)
                {
                    OnPropertyChanged("ReelOlmayanFinansmanMaliyet");
                    _ReelOlmayanFinansmanMaliyet = value;
                }
            }
        }

        [DbAlan("KrediArindirmaSekli", SqlDbType.SmallInt, 2)]
        public short KrediArindirmaSekli
        {
            get { return _KrediArindirmaSekli; }
            set
            {
                if (_KrediArindirmaSekli != value)
                {
                    OnPropertyChanged("KrediArindirmaSekli");
                    _KrediArindirmaSekli = value;
                }
            }
        }

        [DbAlan("FinansmanGiderTuru", SqlDbType.SmallInt, 2)]
        public short FinansmanGiderTuru
        {
            get { return _FinansmanGiderTuru; }
            set
            {
                if (_FinansmanGiderTuru != value)
                {
                    OnPropertyChanged("FinansmanGiderTuru");
                    _FinansmanGiderTuru = value;
                }
            }
        }

        [DbAlan("Duz_Yapilan_Yil", SqlDbType.Int, 4)]
        public int Duz_Yapilan_Yil
        {
            get { return _Duz_Yapilan_Yil; }
            set
            {
                if (_Duz_Yapilan_Yil != value)
                {
                    OnPropertyChanged("Duz_Yapilan_Yil");
                    _Duz_Yapilan_Yil = value;
                }
            }
        }

        [DbAlan("Duz_Yapilan_Donem", SqlDbType.SmallInt, 2)]
        public short Duz_Yapilan_Donem
        {
            get { return _Duz_Yapilan_Donem; }
            set
            {
                if (_Duz_Yapilan_Donem != value)
                {
                    OnPropertyChanged("Duz_Yapilan_Donem");
                    _Duz_Yapilan_Donem = value;
                }
            }
        }

        [DbAlan("Duz_Yontemi", SqlDbType.SmallInt, 2)]
        public short Duz_Yontemi
        {
            get { return _Duz_Yontemi; }
            set
            {
                if (_Duz_Yontemi != value)
                {
                    OnPropertyChanged("Duz_Yontemi");
                    _Duz_Yontemi = value;
                }
            }
        }

        [DbAlan("Duz_Mhs_Hesap_Kodu", SqlDbType.VarChar, 50)]
        public string Duz_Mhs_Hesap_Kodu
        {
            get { return _Duz_Mhs_Hesap_Kodu; }
            set
            {
                if (_Duz_Mhs_Hesap_Kodu != value)
                {
                    OnPropertyChanged("Duz_Mhs_Hesap_Kodu");
                    _Duz_Mhs_Hesap_Kodu = value;
                }
            }
        }

        [DbAlan("Duz_Mhs_Durumu", SqlDbType.SmallInt, 2)]
        public short Duz_Mhs_Durumu
        {
            get { return _Duz_Mhs_Durumu; }
            set
            {
                if (_Duz_Mhs_Durumu != value)
                {
                    OnPropertyChanged("Duz_Mhs_Durumu");
                    _Duz_Mhs_Durumu = value;
                }
            }
        }

        [DbAlan("Duz_Stok_Devir_Hizi", SqlDbType.Real, 4)]
        public double Duz_Stok_Devir_Hizi
        {
            get { return _Duz_Stok_Devir_Hizi; }
            set
            {
                if (_Duz_Stok_Devir_Hizi != value)
                {
                    OnPropertyChanged("Duz_Stok_Devir_Hizi");
                    _Duz_Stok_Devir_Hizi = value;
                }
            }
        }

        [DbAlan("Duz_Katsayisi", SqlDbType.Real, 4)]
        public double Duz_Katsayisi
        {
            get { return _Duz_Katsayisi; }
            set
            {
                if (_Duz_Katsayisi != value)
                {
                    OnPropertyChanged("Duz_Katsayisi");
                    _Duz_Katsayisi = value;
                }
            }
        }

        [DbAlan("Duz_Esas_Tutar", SqlDbType.Decimal, 13)]
        public decimal Duz_Esas_Tutar
        {
            get { return _Duz_Esas_Tutar; }
            set
            {
                if (_Duz_Esas_Tutar != value)
                {
                    OnPropertyChanged("Duz_Esas_Tutar");
                    _Duz_Esas_Tutar = value;
                }
            }
        }

        [DbAlan("Duz_Tutar", SqlDbType.Decimal, 13)]
        public decimal Duz_Tutar
        {
            get { return _Duz_Tutar; }
            set
            {
                if (_Duz_Tutar != value)
                {
                    OnPropertyChanged("Duz_Tutar");
                    _Duz_Tutar = value;
                }
            }
        }

        [DbAlan("Duz_Mly_Yontemi", SqlDbType.SmallInt, 2)]
        public short Duz_Mly_Yontemi
        {
            get { return _Duz_Mly_Yontemi; }
            set
            {
                if (_Duz_Mly_Yontemi != value)
                {
                    OnPropertyChanged("Duz_Mly_Yontemi");
                    _Duz_Mly_Yontemi = value;
                }
            }
        }

        [DbAlan("Duz_Mly_Tarihi_Mly_Tutar", SqlDbType.Decimal, 13)]
        public decimal Duz_Mly_Tarihi_Mly_Tutar
        {
            get { return _Duz_Mly_Tarihi_Mly_Tutar; }
            set
            {
                if (_Duz_Mly_Tarihi_Mly_Tutar != value)
                {
                    OnPropertyChanged("Duz_Mly_Tarihi_Mly_Tutar");
                    _Duz_Mly_Tarihi_Mly_Tutar = value;
                }
            }
        }

        [DbAlan("Duz_Mly_Satilan_Mal_Mly_Tutar", SqlDbType.Decimal, 13)]
        public decimal Duz_Mly_Satilan_Mal_Mly_Tutar
        {
            get { return _Duz_Mly_Satilan_Mal_Mly_Tutar; }
            set
            {
                if (_Duz_Mly_Satilan_Mal_Mly_Tutar != value)
                {
                    OnPropertyChanged("Duz_Mly_Satilan_Mal_Mly_Tutar");
                    _Duz_Mly_Satilan_Mal_Mly_Tutar = value;
                }
            }
        }

        [DbAlan("Duz_Mly_Mhs_Hesap_Kodu", SqlDbType.VarChar, 50)]
        public string Duz_Mly_Mhs_Hesap_Kodu
        {
            get { return _Duz_Mly_Mhs_Hesap_Kodu; }
            set
            {
                if (_Duz_Mly_Mhs_Hesap_Kodu != value)
                {
                    OnPropertyChanged("Duz_Mly_Mhs_Hesap_Kodu");
                    _Duz_Mly_Mhs_Hesap_Kodu = value;
                }
            }
        }

        [DbAlan("Duz_Mly_Mhs_Durumu", SqlDbType.SmallInt, 2)]
        public short Duz_Mly_Mhs_Durumu
        {
            get { return _Duz_Mly_Mhs_Durumu; }
            set
            {
                if (_Duz_Mly_Mhs_Durumu != value)
                {
                    OnPropertyChanged("Duz_Mly_Mhs_Durumu");
                    _Duz_Mly_Mhs_Durumu = value;
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

        [DbAlan("KartDovizCinsi", SqlDbType.VarChar, 3)]
        public string KartDovizCinsi
        {
            get { return _KartDovizCinsi; }
            set
            {
                if (_KartDovizCinsi != value)
                {
                    OnPropertyChanged("KartDovizCinsi");
                    _KartDovizCinsi = value;
                }
            }
        }

        [DbAlan("KartDovizKuru", SqlDbType.Decimal, 13)]
        public decimal KartDovizKuru
        {
            get { return _KartDovizKuru; }
            set
            {
                if (_KartDovizKuru != value)
                {
                    OnPropertyChanged("KartDovizKuru");
                    _KartDovizKuru = value;
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

        [DbAlan("FFEvrakNo", SqlDbType.VarChar, 16)]
        public string FFEvrakNo
        {
            get { return _FFEvrakNo; }
            set
            {
                if (_FFEvrakNo != value)
                {
                    OnPropertyChanged("FFEvrakNo");
                    _FFEvrakNo = value;
                }
            }
        }

        [DbAlan("FFTarih", SqlDbType.Int, 4)]
        public int FFTarih
        {
            get { return _FFTarih; }
            set
            {
                if (_FFTarih != value)
                {
                    OnPropertyChanged("FFTarih");
                    _FFTarih = value;
                }
            }
        }

        [DbAlan("KaynakSiraNo", SqlDbType.SmallInt, 2)]
        public short KaynakSiraNo
        {
            get { return _KaynakSiraNo; }
            set
            {
                if (_KaynakSiraNo != value)
                {
                    OnPropertyChanged("KaynakSiraNo");
                    _KaynakSiraNo = value;
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

        [DbAlan("DepoIadeEdilenMiktar", SqlDbType.Decimal, 13)]
        public decimal DepoIadeEdilenMiktar
        {
            get { return _DepoIadeEdilenMiktar; }
            set
            {
                if (_DepoIadeEdilenMiktar != value)
                {
                    OnPropertyChanged("DepoIadeEdilenMiktar");
                    _DepoIadeEdilenMiktar = value;
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

        [DbAlan("Masraf1", SqlDbType.Decimal, 13)]
        public decimal Masraf1
        {
            get { return _Masraf1; }
            set
            {
                if (_Masraf1 != value)
                {
                    OnPropertyChanged("Masraf1");
                    _Masraf1 = value;
                }
            }
        }

        [DbAlan("Masraf2", SqlDbType.Decimal, 13)]
        public decimal Masraf2
        {
            get { return _Masraf2; }
            set
            {
                if (_Masraf2 != value)
                {
                    OnPropertyChanged("Masraf2");
                    _Masraf2 = value;
                }
            }
        }

        [DbAlan("IthalatDosyaNo", SqlDbType.VarChar, 20)]
        public string IthalatDosyaNo
        {
            get { return _IthalatDosyaNo; }
            set
            {
                if (_IthalatDosyaNo != value)
                {
                    OnPropertyChanged("IthalatDosyaNo");
                    _IthalatDosyaNo = value;
                }
            }
        }

        [DbAlan("IthalatMDagDurum", SqlDbType.SmallInt, 2)]
        public short IthalatMDagDurum
        {
            get { return _IthalatMDagDurum; }
            set
            {
                if (_IthalatMDagDurum != value)
                {
                    OnPropertyChanged("IthalatMDagDurum");
                    _IthalatMDagDurum = value;
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

        /// <summary>
        /// 0 Onaylı Kağıt Fatura; 1 Temel eFatura; 2 Ticari eFatura; 3 e-Arşiv Faturası
        /// </summary>
        [DbAlan("EFatTip", SqlDbType.SmallInt, 2)]
        public short EFatTip
        {
            get { return _EFatTip; }
            set
            {
                if (_EFatTip != value)
                {
                    OnPropertyChanged("EFatTip");
                    _EFatTip = value;
                }
            }
        }

        /// <summary>
        /// 0 Taslak; 1 Gönderildi; 2 Kabul Edildi; 3 Rededildi; 4 İade Edildi; 5 Arsivlendi; 6 GIB Teslim Aldi; 7 GIB Kabul Etmedi; 8 Aliciya Teslim Edildi; 9 Alici Teslim Almadi; 10 Beklemede
        /// </summary>
        [DbAlan("EFatDurum", SqlDbType.SmallInt, 2)]
        public short EFatDurum
        {
            get { return _EFatDurum; }
            set
            {
                if (_EFatDurum != value)
                {
                    OnPropertyChanged("EFatDurum");
                    _EFatDurum = value;
                }
            }
        }

        [DbAlan("ToplamKlmIskonto", SqlDbType.Decimal, 13)]
        public decimal ToplamKlmIskonto
        {
            get { return _ToplamKlmIskonto; }
            set
            {
                if (_ToplamKlmIskonto != value)
                {
                    OnPropertyChanged("ToplamKlmIskonto");
                    _ToplamKlmIskonto = value;
                }
            }
        }

        [DbAlan("IthalatMDagYontem", SqlDbType.SmallInt, 2)]
        public short IthalatMDagYontem
        {
            get { return _IthalatMDagYontem; }
            set
            {
                if (_IthalatMDagYontem != value)
                {
                    OnPropertyChanged("IthalatMDagYontem");
                    _IthalatMDagYontem = value;
                }
            }
        }

        [DbAlan("KDVMuafAck", SqlDbType.VarChar, 20)]
        public string KDVMuafAck
        {
            get { return _KDVMuafAck; }
            set
            {
                if (_KDVMuafAck != value)
                {
                    OnPropertyChanged("KDVMuafAck");
                    _KDVMuafAck = value;
                }
            }
        }

        [DbAlan("KarsiMalKodu", SqlDbType.VarChar, 40)]
        public string KarsiMalKodu
        {
            get { return _KarsiMalKodu; }
            set
            {
                if (_KarsiMalKodu != value)
                {
                    OnPropertyChanged("KarsiMalKodu");
                    _KarsiMalKodu = value;
                }
            }
        }

        [DbAlan("UreticiMalKodu", SqlDbType.VarChar, 40)]
        public string UreticiMalKodu
        {
            get { return _UreticiMalKodu; }
            set
            {
                if (_UreticiMalKodu != value)
                {
                    OnPropertyChanged("UreticiMalKodu");
                    _UreticiMalKodu = value;
                }
            }
        }

        [DbAlan("StopajOran", SqlDbType.Real, 4)]
        public double StopajOran
        {
            get { return _StopajOran; }
            set
            {
                if (_StopajOran != value)
                {
                    OnPropertyChanged("StopajOran");
                    _StopajOran = value;
                }
            }
        }

        [DbAlan("StopajTutar", SqlDbType.Decimal, 13)]
        public decimal StopajTutar
        {
            get { return _StopajTutar; }
            set
            {
                if (_StopajTutar != value)
                {
                    OnPropertyChanged("StopajTutar");
                    _StopajTutar = value;
                }
            }
        }

        [DbAlan("OTVTevkifatVarYok", SqlDbType.SmallInt, 2)]
        public short OTVTevkifatVarYok
        {
            get { return _OTVTevkifatVarYok; }
            set
            {
                if (_OTVTevkifatVarYok != value)
                {
                    OnPropertyChanged("OTVTevkifatVarYok");
                    _OTVTevkifatVarYok = value;
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

        [DbAlan("eFatIrsReferansNo", SqlDbType.VarChar, 30)]
        public string eFatIrsReferansNo
        {
            get { return _eFatIrsReferansNo; }
            set
            {
                if (_eFatIrsReferansNo != value)
                {
                    OnPropertyChanged("eFatIrsReferansNo");
                    _eFatIrsReferansNo = value;
                }
            }
        }

        [DbAlan("IhracatDosyaNo", SqlDbType.VarChar, 20)]
        public string IhracatDosyaNo
        {
            get { return _IhracatDosyaNo; }
            set
            {
                if (_IhracatDosyaNo != value)
                {
                    OnPropertyChanged("IhracatDosyaNo");
                    _IhracatDosyaNo = value;
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

        [DbAlan("OzelMatKDVKod", SqlDbType.VarChar, 3)]
        public string OzelMatKDVKod
        {
            get { return _OzelMatKDVKod; }
            set
            {
                if (_OzelMatKDVKod != value)
                {
                    OnPropertyChanged("OzelMatKDVKod");
                    _OzelMatKDVKod = value;
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

        [DbAlan("EArsivTeslimSekli", SqlDbType.SmallInt, 2)]
        public short EArsivTeslimSekli
        {
            get { return _EArsivTeslimSekli; }
            set
            {
                if (_EArsivTeslimSekli != value)
                {
                    OnPropertyChanged("EArsivTeslimSekli");
                    _EArsivTeslimSekli = value;
                }
            }
        }

        [DbAlan("EArsivFaturaTipi", SqlDbType.SmallInt, 2)]
        public short EArsivFaturaTipi
        {
            get { return _EArsivFaturaTipi; }
            set
            {
                if (_EArsivFaturaTipi != value)
                {
                    OnPropertyChanged("EArsivFaturaTipi");
                    _EArsivFaturaTipi = value;
                }
            }
        }

        [DbAlan("EArsivFaturaDurum", SqlDbType.SmallInt, 2)]
        public short EArsivFaturaDurum
        {
            get { return _EArsivFaturaDurum; }
            set
            {
                if (_EArsivFaturaDurum != value)
                {
                    OnPropertyChanged("EArsivFaturaDurum");
                    _EArsivFaturaDurum = value;
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

        [DbAlan("BrutAgirlik", SqlDbType.Decimal, 13)]
        public decimal BrutAgirlik
        {
            get { return _BrutAgirlik; }
            set
            {
                if (_BrutAgirlik != value)
                {
                    OnPropertyChanged("BrutAgirlik");
                    _BrutAgirlik = value;
                }
            }
        }

        [DbAlan("NetAgirlik", SqlDbType.Decimal, 13)]
        public decimal NetAgirlik
        {
            get { return _NetAgirlik; }
            set
            {
                if (_NetAgirlik != value)
                {
                    OnPropertyChanged("NetAgirlik");
                    _NetAgirlik = value;
                }
            }
        }

        [DbAlan("DaraAgirlik", SqlDbType.Decimal, 13)]
        public decimal DaraAgirlik
        {
            get { return _DaraAgirlik; }
            set
            {
                if (_DaraAgirlik != value)
                {
                    OnPropertyChanged("DaraAgirlik");
                    _DaraAgirlik = value;
                }
            }
        }

        [DbAlan("BirimAgirlik", SqlDbType.VarChar, 4)]
        public string BirimAgirlik
        {
            get { return _BirimAgirlik; }
            set
            {
                if (_BirimAgirlik != value)
                {
                    OnPropertyChanged("BirimAgirlik");
                    _BirimAgirlik = value;
                }
            }
        }

        [DbAlan("BrutHacim", SqlDbType.Decimal, 13)]
        public decimal BrutHacim
        {
            get { return _BrutHacim; }
            set
            {
                if (_BrutHacim != value)
                {
                    OnPropertyChanged("BrutHacim");
                    _BrutHacim = value;
                }
            }
        }

        [DbAlan("NetHacim", SqlDbType.Decimal, 13)]
        public decimal NetHacim
        {
            get { return _NetHacim; }
            set
            {
                if (_NetHacim != value)
                {
                    OnPropertyChanged("NetHacim");
                    _NetHacim = value;
                }
            }
        }

        [DbAlan("BirimHacim", SqlDbType.VarChar, 4)]
        public string BirimHacim
        {
            get { return _BirimHacim; }
            set
            {
                if (_BirimHacim != value)
                {
                    OnPropertyChanged("BirimHacim");
                    _BirimHacim = value;
                }
            }
        }

        [DbAlan("KapTipi", SqlDbType.VarChar, 4)]
        public string KapTipi
        {
            get { return _KapTipi; }
            set
            {
                if (_KapTipi != value)
                {
                    OnPropertyChanged("KapTipi");
                    _KapTipi = value;
                }
            }
        }

        [DbAlan("KapAdet", SqlDbType.Decimal, 13)]
        public decimal KapAdet
        {
            get { return _KapAdet; }
            set
            {
                if (_KapAdet != value)
                {
                    OnPropertyChanged("KapAdet");
                    _KapAdet = value;
                }
            }
        }

        [DbAlan("FormBaBsTarih", SqlDbType.Int, 4)]
        public int FormBaBsTarih
        {
            get { return _FormBaBsTarih; }
            set
            {
                if (_FormBaBsTarih != value)
                {
                    OnPropertyChanged("FormBaBsTarih");
                    _FormBaBsTarih = value;
                }
            }
        }

        [DbAlan("YOKCZRaporNo", SqlDbType.Int, 4)]
        public int YOKCZRaporNo
        {
            get { return _YOKCZRaporNo; }
            set
            {
                if (_YOKCZRaporNo != value)
                {
                    OnPropertyChanged("YOKCZRaporNo");
                    _YOKCZRaporNo = value;
                }
            }
        }

        [DbAlan("KDVDahilKalemIskontoOran", SqlDbType.Real, 4)]
        public double KDVDahilKalemIskontoOran
        {
            get { return _KDVDahilKalemIskontoOran; }
            set
            {
                if (_KDVDahilKalemIskontoOran != value)
                {
                    OnPropertyChanged("KDVDahilKalemIskontoOran");
                    _KDVDahilKalemIskontoOran = value;
                }
            }
        }

        [DbAlan("KDVDahilKalemOranIskontosu", SqlDbType.Decimal, 13)]
        public decimal KDVDahilKalemOranIskontosu
        {
            get { return _KDVDahilKalemOranIskontosu; }
            set
            {
                if (_KDVDahilKalemOranIskontosu != value)
                {
                    OnPropertyChanged("KDVDahilKalemOranIskontosu");
                    _KDVDahilKalemOranIskontosu = value;
                }
            }
        }

        [DbAlan("KDVDahilKalemTutarIskontosu", SqlDbType.Decimal, 13)]
        public decimal KDVDahilKalemTutarIskontosu
        {
            get { return _KDVDahilKalemTutarIskontosu; }
            set
            {
                if (_KDVDahilKalemTutarIskontosu != value)
                {
                    OnPropertyChanged("KDVDahilKalemTutarIskontosu");
                    _KDVDahilKalemTutarIskontosu = value;
                }
            }
        }

        [DbAlan("KDVDahilIskonto", SqlDbType.Decimal, 13)]
        public decimal KDVDahilIskonto
        {
            get { return _KDVDahilIskonto; }
            set
            {
                if (_KDVDahilIskonto != value)
                {
                    OnPropertyChanged("KDVDahilIskonto");
                    _KDVDahilIskonto = value;
                }
            }
        }

        [DbAlan("KDVIstisnaTutar", SqlDbType.Decimal, 13)]
        public decimal KDVIstisnaTutar
        {
            get { return _KDVIstisnaTutar; }
            set
            {
                if (_KDVIstisnaTutar != value)
                {
                    OnPropertyChanged("KDVIstisnaTutar");
                    _KDVIstisnaTutar = value;
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



        internal void STI_PropertyChanged(object sender, PropertyChangedEventArgs e)
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


        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string MalAdi { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string Unvan { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TalepEden { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TesisKodu { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TesisAdi { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string SirketKodu { get; set; }

        [DbAlan(AlanAdi = "", DbTye = SqlDbType.VarChar)]
        public string TalepNo { get; set; }

        public DateTime KaynakSiparisTarih_D 
        {
            get { return new DateTime(1900, 1, 1).AddDays(KaynakSiparisTarih - 2); }
            set { KaynakSiparisTarih = Convert.ToInt32(value.ToOADate()); }
        }


        public KKP_SPI SPIInfo { get; set; }
        public KKP_IRS IRSInfo { get; set; }

        #endregion

        public KKP_STI()
        {
            Data.DefaultValueSetSTI(this, (KKPKynkEvrakTip)0);            
        }

        public KKP_STI(KKPKynkEvrakTip evrakTip)
        {
            Data.DefaultValueSetSTI(this, evrakTip);
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

            #region Miktar

            Miktar = BirimMiktar;
            if (Operator == 0 && Katsayi > 0)
                Miktar = BirimMiktar / (decimal)Katsayi;
            else if (Operator == 1)
                Miktar = BirimMiktar * (decimal)Katsayi;
            #endregion Miktar

            #region Fiyat
            if (Miktar == 0)
                Fiyat = 0;
            else
                Fiyat = Math.Round(Tutar / Miktar, 6, MidpointRounding.AwayFromZero);
            #endregion Fiyat

        }
    }
}
