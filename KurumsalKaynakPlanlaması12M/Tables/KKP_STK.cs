using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{
    [DbTablo("FINSAT6","FINSAT6","STK")]
    public class KKP_STK : INotifyPropertyChanged
    {


        #region Fields
        private string _MalKodu;
        private string _MalAdi;
        private string _MalAdi2;
        private string _MalAdi3;
        private string _MalAdi4;
        private string _MalAdi5;
        private string _GrupKod;
        private string _SirketWebAdres;
        private string _OzelKod;
        private string _TipKod;
        private string _BarKod1;
        private string _BarKod2;
        private string _BarKod3;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private string _Kod4;
        private decimal _Kod5;
        private decimal _Kod6;
        private string _Kod7;
        private string _Kod8;
        private string _Kod9;
        private short _Kod10;
        private short _Kod11;
        private decimal _Kod12;
        private decimal _Kod13;
        private string _DovizCinsi;
        private string _MalKodu2;
        private string _Birim1;
        private string _Birim2;
        private string _Birim3;
        private short _Operator2;
        private short _Operator3;
        private double _KatSayi2;
        private double _KatSayi3;
        private string _UreticiKodu;
        private string _FireliMalKodu;
        private short _MlySekli;
        private short _MKDS;
        private short _ValorGun;
        private double _IskontoOran;
        private double _KDVOran;
        private double _Fire;
        private string _TeminYeri;
        private short _TeminSuresi;
        private decimal _KritikStok;
        private decimal _AzamiStok;
        private decimal _TahminiStok;
        private string _SatislarHesabi;
        private string _AlimlarHesabi;
        private string _SatistanIade;
        private string _AlimdanIade;
        private string _MasrafMerkezi;
        private decimal _AlisFiyat1;
        private decimal _AlisFiyat2;
        private decimal _AlisFiyat3;
        private decimal _DovizAlisFiyat1;
        private decimal _DovizAlisFiyat2;
        private decimal _DovizAlisFiyat3;
        private string _AF1DovizCinsi;
        private string _AF2DovizCinsi;
        private string _AF3DovizCinsi;
        private short _AF1KDV;
        private short _AF2KDV;
        private short _AF3KDV;
        private short _DovizAF1KDV;
        private short _DovizAF2KDV;
        private short _DovizAF3KDV;
        private short _AF1Birim;
        private short _AF2Birim;
        private short _AF3Birim;
        private short _DovizAF1Birim;
        private short _DovizAF2Birim;
        private short _DovizAF3Birim;
        private decimal _SatisFiyat1;
        private decimal _SatisFiyat2;
        private decimal _SatisFiyat3;
        private decimal _SatisFiyat4;
        private decimal _SatisFiyat5;
        private decimal _SatisFiyat6;
        private decimal _DovizSatisFiyat1;
        private decimal _DovizSatisFiyat2;
        private decimal _DovizSatisFiyat3;
        private string _SF1DovizCinsi;
        private string _SF2DovizCinsi;
        private string _SF3DovizCinsi;
        private string _SF4DovizCinsi;
        private string _SF5DovizCinsi;
        private string _SF6DovizCinsi;
        private short _SF1KDV;
        private short _SF2KDV;
        private short _SF3KDV;
        private short _SF4KDV;
        private short _SF5KDV;
        private short _SF6KDV;
        private short _DovizSF1KDV;
        private short _DovizSF2KDV;
        private short _DovizSF3KDV;
        private short _SF1Birim;
        private short _SF2Birim;
        private short _SF3Birim;
        private short _SF4Birim;
        private short _SF5Birim;
        private short _SF6Birim;
        private short _DovizSF1Birim;
        private short _DovizSF2Birim;
        private short _DovizSF3Birim;
        private int _DvrTarih;
        private decimal _DvrMiktar;
        private decimal _DvrTutar;
        private decimal _GirMiktar;
        private decimal _GirTutar;
        private decimal _GirIskonto;
        private int _GirTarih;
        private decimal _CikMiktar;
        private decimal _CikTutar;
        private decimal _CikIskonto;
        private int _CikTarih;
        private decimal _DvzDvrTutar;
        private decimal _DvzGirTutar;
        private decimal _DvzGirIskTutar;
        private decimal _DvzCikTutar;
        private decimal _DvzCikIskTutar;
        private short _SonMlySekli;
        private decimal _SonMlyBirimFiyat;
        private int _SonMlyTarih;
        private int _SonAlimFatTarih;
        private string _SonAlimEvrakNo;
        private decimal _SonAlimBF;
        private string _SonAlimCHK;
        private decimal _AlimSiparis;
        private decimal _SatisSiparis;
        private decimal _GumrukFon;
        private string _GumrukGTIPN;
        private double _GumrukVergi;
        private decimal _GirRezervasyon;
        private decimal _CikRezervasyon;
        private decimal _GirKonsinye;
        private decimal _CikKonsinye;
        private string _Nesne1;
        private string _Nesne2;
        private string _Nesne3;
        private string _ButceKodu;
        private short _KartTuru;
        private short _UseSatRezervasyon;
        private short _UseSatSiparis;
        private short _UseSatFatIrs;
        private short _UseCikisIslem;
        private short _UseSetUrun;
        private short _UseAlimRezervasyon;
        private short _UseAlimSiparis;
        private short _UseAlimIrsFat;
        private short _UseGirisIslem;
        private short _SF1ValorGun;
        private short _SF2ValorGun;
        private short _SF3ValorGun;
        private short _SF4ValorGun;
        private short _SF5ValorGun;
        private short _SF6ValorGun;
        private short _SF1DvzValorGun;
        private short _SF2DvzValorGun;
        private short _SF3DvzValorGun;
        private short _SatisFiyatTip;
        private decimal _SatisFiyatAltLimit;
        private decimal _SatisFiyatUstLimit;
        private int _SonSayimTarih;
        private decimal _SonSayimSonuc;
        private decimal _SonSayimFark;
        private string _Notlar;
        private decimal _BlkMiktar;
        private short _ElekTicSitList;
        private short _WebMagSitList;
        private short _DagZinSitList;
        private short _SirIciSipSitList;
        private short _ElekTicSitListAdi;
        private short _WebMagSitListAdi;
        private short _DagZinSitListAdi;
        private short _SirIciSipSitListAdi;
        private string _DemirbasKodu;
        private short _MiktarTakibi;
        private short _BakGostSekli;
        private short _KartTip;
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
        private decimal _AlimTeklif;
        private decimal _SatisTeklif;
        private string _SatMalMaliyetHesap;
        private short _AktifPasif;
        private short _TevfikatOran;
        private decimal _SonAlimNetBF;
        private decimal _SonAlimDvzBF;
        private decimal _SonAlimDvzNetBF;
        private string _YDAlimlarHesabi;
        private string _TevkifatAlis;
        private string _TevkifatSatis;
        private string _TevkifatAlisTam;
        private string _Kod14;
        private string _Kod15;
        private string _Kod16;
        private string _Kod17;
        private string _Kod18;
        private int _Row_ID;

        #endregion Fields



        #region table Properties

        [DbAlan("MalKodu", SqlDbType.VarChar, 30, false, true, false)]
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

        [DbAlan("MalAdi", SqlDbType.VarChar, 50)]
        public string MalAdi
        {
            get { return _MalAdi; }
            set
            {
                if (_MalAdi != value)
                {
                    OnPropertyChanged("MalAdi");
                    _MalAdi = value;
                }
            }
        }

        [DbAlan("MalAdi2", SqlDbType.VarChar, 50)]
        public string MalAdi2
        {
            get { return _MalAdi2; }
            set
            {
                if (_MalAdi2 != value)
                {
                    OnPropertyChanged("MalAdi2");
                    _MalAdi2 = value;
                }
            }
        }

        [DbAlan("MalAdi3", SqlDbType.VarChar, 50)]
        public string MalAdi3
        {
            get { return _MalAdi3; }
            set
            {
                if (_MalAdi3 != value)
                {
                    OnPropertyChanged("MalAdi3");
                    _MalAdi3 = value;
                }
            }
        }

        [DbAlan("MalAdi4", SqlDbType.VarChar, 50)]
        public string MalAdi4
        {
            get { return _MalAdi4; }
            set
            {
                if (_MalAdi4 != value)
                {
                    OnPropertyChanged("MalAdi4");
                    _MalAdi4 = value;
                }
            }
        }

        [DbAlan("MalAdi5", SqlDbType.VarChar, 50)]
        public string MalAdi5
        {
            get { return _MalAdi5; }
            set
            {
                if (_MalAdi5 != value)
                {
                    OnPropertyChanged("MalAdi5");
                    _MalAdi5 = value;
                }
            }
        }

        [DbAlan("GrupKod", SqlDbType.VarChar, 20)]
        public string GrupKod
        {
            get { return _GrupKod; }
            set
            {
                if (_GrupKod != value)
                {
                    OnPropertyChanged("GrupKod");
                    _GrupKod = value;
                }
            }
        }

        [DbAlan("SirketWebAdres", SqlDbType.VarChar, 50)]
        public string SirketWebAdres
        {
            get { return _SirketWebAdres; }
            set
            {
                if (_SirketWebAdres != value)
                {
                    OnPropertyChanged("SirketWebAdres");
                    _SirketWebAdres = value;
                }
            }
        }

        [DbAlan("OzelKod", SqlDbType.VarChar, 20)]
        public string OzelKod
        {
            get { return _OzelKod; }
            set
            {
                if (_OzelKod != value)
                {
                    OnPropertyChanged("OzelKod");
                    _OzelKod = value;
                }
            }
        }

        [DbAlan("TipKod", SqlDbType.VarChar, 20)]
        public string TipKod
        {
            get { return _TipKod; }
            set
            {
                if (_TipKod != value)
                {
                    OnPropertyChanged("TipKod");
                    _TipKod = value;
                }
            }
        }

        [DbAlan("BarKod1", SqlDbType.VarChar, 52)]
        public string BarKod1
        {
            get { return _BarKod1; }
            set
            {
                if (_BarKod1 != value)
                {
                    OnPropertyChanged("BarKod1");
                    _BarKod1 = value;
                }
            }
        }

        [DbAlan("BarKod2", SqlDbType.VarChar, 52)]
        public string BarKod2
        {
            get { return _BarKod2; }
            set
            {
                if (_BarKod2 != value)
                {
                    OnPropertyChanged("BarKod2");
                    _BarKod2 = value;
                }
            }
        }

        [DbAlan("BarKod3", SqlDbType.VarChar, 52)]
        public string BarKod3
        {
            get { return _BarKod3; }
            set
            {
                if (_BarKod3 != value)
                {
                    OnPropertyChanged("BarKod3");
                    _BarKod3 = value;
                }
            }
        }

        [DbAlan("Kod1", SqlDbType.VarChar, 20)]
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

        [DbAlan("Kod2", SqlDbType.VarChar, 20)]
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

        [DbAlan("Kod3", SqlDbType.VarChar, 20)]
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

        [DbAlan("Kod4", SqlDbType.VarChar, 20)]
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

        [DbAlan("Kod5", SqlDbType.Decimal, 13)]
        public decimal Kod5
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

        [DbAlan("Kod6", SqlDbType.Decimal, 13)]
        public decimal Kod6
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

        [DbAlan("Kod7", SqlDbType.VarChar, 20)]
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

        [DbAlan("Kod10", SqlDbType.SmallInt, 2)]
        public short Kod10
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

        [DbAlan("Kod12", SqlDbType.Decimal, 13)]
        public decimal Kod12
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

        [DbAlan("MalKodu2", SqlDbType.VarChar, 30)]
        public string MalKodu2
        {
            get { return _MalKodu2; }
            set
            {
                if (_MalKodu2 != value)
                {
                    OnPropertyChanged("MalKodu2");
                    _MalKodu2 = value;
                }
            }
        }

        [DbAlan("Birim1", SqlDbType.VarChar, 4)]
        public string Birim1
        {
            get { return _Birim1; }
            set
            {
                if (_Birim1 != value)
                {
                    OnPropertyChanged("Birim1");
                    _Birim1 = value;
                }
            }
        }

        [DbAlan("Birim2", SqlDbType.VarChar, 4)]
        public string Birim2
        {
            get { return _Birim2; }
            set
            {
                if (_Birim2 != value)
                {
                    OnPropertyChanged("Birim2");
                    _Birim2 = value;
                }
            }
        }

        [DbAlan("Birim3", SqlDbType.VarChar, 4)]
        public string Birim3
        {
            get { return _Birim3; }
            set
            {
                if (_Birim3 != value)
                {
                    OnPropertyChanged("Birim3");
                    _Birim3 = value;
                }
            }
        }

        [DbAlan("Operator2", SqlDbType.SmallInt, 2)]
        public short Operator2
        {
            get { return _Operator2; }
            set
            {
                if (_Operator2 != value)
                {
                    OnPropertyChanged("Operator2");
                    _Operator2 = value;
                }
            }
        }

        [DbAlan("Operator3", SqlDbType.SmallInt, 2)]
        public short Operator3
        {
            get { return _Operator3; }
            set
            {
                if (_Operator3 != value)
                {
                    OnPropertyChanged("Operator3");
                    _Operator3 = value;
                }
            }
        }

        [DbAlan("KatSayi2", SqlDbType.Float, 8)]
        public double KatSayi2
        {
            get { return _KatSayi2; }
            set
            {
                if (_KatSayi2 != value)
                {
                    OnPropertyChanged("KatSayi2");
                    _KatSayi2 = value;
                }
            }
        }

        [DbAlan("KatSayi3", SqlDbType.Float, 8)]
        public double KatSayi3
        {
            get { return _KatSayi3; }
            set
            {
                if (_KatSayi3 != value)
                {
                    OnPropertyChanged("KatSayi3");
                    _KatSayi3 = value;
                }
            }
        }

        [DbAlan("UreticiKodu", SqlDbType.VarChar, 30)]
        public string UreticiKodu
        {
            get { return _UreticiKodu; }
            set
            {
                if (_UreticiKodu != value)
                {
                    OnPropertyChanged("UreticiKodu");
                    _UreticiKodu = value;
                }
            }
        }

        [DbAlan("FireliMalKodu", SqlDbType.VarChar, 30)]
        public string FireliMalKodu
        {
            get { return _FireliMalKodu; }
            set
            {
                if (_FireliMalKodu != value)
                {
                    OnPropertyChanged("FireliMalKodu");
                    _FireliMalKodu = value;
                }
            }
        }

        [DbAlan("MlySekli", SqlDbType.SmallInt, 2)]
        public short MlySekli
        {
            get { return _MlySekli; }
            set
            {
                if (_MlySekli != value)
                {
                    OnPropertyChanged("MlySekli");
                    _MlySekli = value;
                }
            }
        }

        [DbAlan("MKDS", SqlDbType.SmallInt, 2)]
        public short MKDS
        {
            get { return _MKDS; }
            set
            {
                if (_MKDS != value)
                {
                    OnPropertyChanged("MKDS");
                    _MKDS = value;
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

        [DbAlan("Fire", SqlDbType.Real, 4)]
        public double Fire
        {
            get { return _Fire; }
            set
            {
                if (_Fire != value)
                {
                    OnPropertyChanged("Fire");
                    _Fire = value;
                }
            }
        }

        [DbAlan("TeminYeri", SqlDbType.VarChar, 20)]
        public string TeminYeri
        {
            get { return _TeminYeri; }
            set
            {
                if (_TeminYeri != value)
                {
                    OnPropertyChanged("TeminYeri");
                    _TeminYeri = value;
                }
            }
        }

        [DbAlan("TeminSuresi", SqlDbType.SmallInt, 2)]
        public short TeminSuresi
        {
            get { return _TeminSuresi; }
            set
            {
                if (_TeminSuresi != value)
                {
                    OnPropertyChanged("TeminSuresi");
                    _TeminSuresi = value;
                }
            }
        }

        [DbAlan("KritikStok", SqlDbType.Decimal, 13)]
        public decimal KritikStok
        {
            get { return _KritikStok; }
            set
            {
                if (_KritikStok != value)
                {
                    OnPropertyChanged("KritikStok");
                    _KritikStok = value;
                }
            }
        }

        [DbAlan("AzamiStok", SqlDbType.Decimal, 13)]
        public decimal AzamiStok
        {
            get { return _AzamiStok; }
            set
            {
                if (_AzamiStok != value)
                {
                    OnPropertyChanged("AzamiStok");
                    _AzamiStok = value;
                }
            }
        }

        [DbAlan("TahminiStok", SqlDbType.Decimal, 13)]
        public decimal TahminiStok
        {
            get { return _TahminiStok; }
            set
            {
                if (_TahminiStok != value)
                {
                    OnPropertyChanged("TahminiStok");
                    _TahminiStok = value;
                }
            }
        }

        [DbAlan("SatislarHesabi", SqlDbType.VarChar, 50)]
        public string SatislarHesabi
        {
            get { return _SatislarHesabi; }
            set
            {
                if (_SatislarHesabi != value)
                {
                    OnPropertyChanged("SatislarHesabi");
                    _SatislarHesabi = value;
                }
            }
        }

        [DbAlan("AlimlarHesabi", SqlDbType.VarChar, 50)]
        public string AlimlarHesabi
        {
            get { return _AlimlarHesabi; }
            set
            {
                if (_AlimlarHesabi != value)
                {
                    OnPropertyChanged("AlimlarHesabi");
                    _AlimlarHesabi = value;
                }
            }
        }

        [DbAlan("SatistanIade", SqlDbType.VarChar, 50)]
        public string SatistanIade
        {
            get { return _SatistanIade; }
            set
            {
                if (_SatistanIade != value)
                {
                    OnPropertyChanged("SatistanIade");
                    _SatistanIade = value;
                }
            }
        }

        [DbAlan("AlimdanIade", SqlDbType.VarChar, 50)]
        public string AlimdanIade
        {
            get { return _AlimdanIade; }
            set
            {
                if (_AlimdanIade != value)
                {
                    OnPropertyChanged("AlimdanIade");
                    _AlimdanIade = value;
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

        [DbAlan("AlisFiyat1", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat1
        {
            get { return _AlisFiyat1; }
            set
            {
                if (_AlisFiyat1 != value)
                {
                    OnPropertyChanged("AlisFiyat1");
                    _AlisFiyat1 = value;
                }
            }
        }

        [DbAlan("AlisFiyat2", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat2
        {
            get { return _AlisFiyat2; }
            set
            {
                if (_AlisFiyat2 != value)
                {
                    OnPropertyChanged("AlisFiyat2");
                    _AlisFiyat2 = value;
                }
            }
        }

        [DbAlan("AlisFiyat3", SqlDbType.Decimal, 13)]
        public decimal AlisFiyat3
        {
            get { return _AlisFiyat3; }
            set
            {
                if (_AlisFiyat3 != value)
                {
                    OnPropertyChanged("AlisFiyat3");
                    _AlisFiyat3 = value;
                }
            }
        }

        [DbAlan("DovizAlisFiyat1", SqlDbType.Decimal, 13)]
        public decimal DovizAlisFiyat1
        {
            get { return _DovizAlisFiyat1; }
            set
            {
                if (_DovizAlisFiyat1 != value)
                {
                    OnPropertyChanged("DovizAlisFiyat1");
                    _DovizAlisFiyat1 = value;
                }
            }
        }

        [DbAlan("DovizAlisFiyat2", SqlDbType.Decimal, 13)]
        public decimal DovizAlisFiyat2
        {
            get { return _DovizAlisFiyat2; }
            set
            {
                if (_DovizAlisFiyat2 != value)
                {
                    OnPropertyChanged("DovizAlisFiyat2");
                    _DovizAlisFiyat2 = value;
                }
            }
        }

        [DbAlan("DovizAlisFiyat3", SqlDbType.Decimal, 13)]
        public decimal DovizAlisFiyat3
        {
            get { return _DovizAlisFiyat3; }
            set
            {
                if (_DovizAlisFiyat3 != value)
                {
                    OnPropertyChanged("DovizAlisFiyat3");
                    _DovizAlisFiyat3 = value;
                }
            }
        }

        [DbAlan("AF1DovizCinsi", SqlDbType.VarChar, 3)]
        public string AF1DovizCinsi
        {
            get { return _AF1DovizCinsi; }
            set
            {
                if (_AF1DovizCinsi != value)
                {
                    OnPropertyChanged("AF1DovizCinsi");
                    _AF1DovizCinsi = value;
                }
            }
        }

        [DbAlan("AF2DovizCinsi", SqlDbType.VarChar, 3)]
        public string AF2DovizCinsi
        {
            get { return _AF2DovizCinsi; }
            set
            {
                if (_AF2DovizCinsi != value)
                {
                    OnPropertyChanged("AF2DovizCinsi");
                    _AF2DovizCinsi = value;
                }
            }
        }

        [DbAlan("AF3DovizCinsi", SqlDbType.VarChar, 3)]
        public string AF3DovizCinsi
        {
            get { return _AF3DovizCinsi; }
            set
            {
                if (_AF3DovizCinsi != value)
                {
                    OnPropertyChanged("AF3DovizCinsi");
                    _AF3DovizCinsi = value;
                }
            }
        }

        [DbAlan("AF1KDV", SqlDbType.SmallInt, 2)]
        public short AF1KDV
        {
            get { return _AF1KDV; }
            set
            {
                if (_AF1KDV != value)
                {
                    OnPropertyChanged("AF1KDV");
                    _AF1KDV = value;
                }
            }
        }

        [DbAlan("AF2KDV", SqlDbType.SmallInt, 2)]
        public short AF2KDV
        {
            get { return _AF2KDV; }
            set
            {
                if (_AF2KDV != value)
                {
                    OnPropertyChanged("AF2KDV");
                    _AF2KDV = value;
                }
            }
        }

        [DbAlan("AF3KDV", SqlDbType.SmallInt, 2)]
        public short AF3KDV
        {
            get { return _AF3KDV; }
            set
            {
                if (_AF3KDV != value)
                {
                    OnPropertyChanged("AF3KDV");
                    _AF3KDV = value;
                }
            }
        }

        [DbAlan("DovizAF1KDV", SqlDbType.SmallInt, 2)]
        public short DovizAF1KDV
        {
            get { return _DovizAF1KDV; }
            set
            {
                if (_DovizAF1KDV != value)
                {
                    OnPropertyChanged("DovizAF1KDV");
                    _DovizAF1KDV = value;
                }
            }
        }

        [DbAlan("DovizAF2KDV", SqlDbType.SmallInt, 2)]
        public short DovizAF2KDV
        {
            get { return _DovizAF2KDV; }
            set
            {
                if (_DovizAF2KDV != value)
                {
                    OnPropertyChanged("DovizAF2KDV");
                    _DovizAF2KDV = value;
                }
            }
        }

        [DbAlan("DovizAF3KDV", SqlDbType.SmallInt, 2)]
        public short DovizAF3KDV
        {
            get { return _DovizAF3KDV; }
            set
            {
                if (_DovizAF3KDV != value)
                {
                    OnPropertyChanged("DovizAF3KDV");
                    _DovizAF3KDV = value;
                }
            }
        }

        [DbAlan("AF1Birim", SqlDbType.SmallInt, 2)]
        public short AF1Birim
        {
            get { return _AF1Birim; }
            set
            {
                if (_AF1Birim != value)
                {
                    OnPropertyChanged("AF1Birim");
                    _AF1Birim = value;
                }
            }
        }

        [DbAlan("AF2Birim", SqlDbType.SmallInt, 2)]
        public short AF2Birim
        {
            get { return _AF2Birim; }
            set
            {
                if (_AF2Birim != value)
                {
                    OnPropertyChanged("AF2Birim");
                    _AF2Birim = value;
                }
            }
        }

        [DbAlan("AF3Birim", SqlDbType.SmallInt, 2)]
        public short AF3Birim
        {
            get { return _AF3Birim; }
            set
            {
                if (_AF3Birim != value)
                {
                    OnPropertyChanged("AF3Birim");
                    _AF3Birim = value;
                }
            }
        }

        [DbAlan("DovizAF1Birim", SqlDbType.SmallInt, 2)]
        public short DovizAF1Birim
        {
            get { return _DovizAF1Birim; }
            set
            {
                if (_DovizAF1Birim != value)
                {
                    OnPropertyChanged("DovizAF1Birim");
                    _DovizAF1Birim = value;
                }
            }
        }

        [DbAlan("DovizAF2Birim", SqlDbType.SmallInt, 2)]
        public short DovizAF2Birim
        {
            get { return _DovizAF2Birim; }
            set
            {
                if (_DovizAF2Birim != value)
                {
                    OnPropertyChanged("DovizAF2Birim");
                    _DovizAF2Birim = value;
                }
            }
        }

        [DbAlan("DovizAF3Birim", SqlDbType.SmallInt, 2)]
        public short DovizAF3Birim
        {
            get { return _DovizAF3Birim; }
            set
            {
                if (_DovizAF3Birim != value)
                {
                    OnPropertyChanged("DovizAF3Birim");
                    _DovizAF3Birim = value;
                }
            }
        }

        [DbAlan("SatisFiyat1", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat1
        {
            get { return _SatisFiyat1; }
            set
            {
                if (_SatisFiyat1 != value)
                {
                    OnPropertyChanged("SatisFiyat1");
                    _SatisFiyat1 = value;
                }
            }
        }

        [DbAlan("SatisFiyat2", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat2
        {
            get { return _SatisFiyat2; }
            set
            {
                if (_SatisFiyat2 != value)
                {
                    OnPropertyChanged("SatisFiyat2");
                    _SatisFiyat2 = value;
                }
            }
        }

        [DbAlan("SatisFiyat3", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat3
        {
            get { return _SatisFiyat3; }
            set
            {
                if (_SatisFiyat3 != value)
                {
                    OnPropertyChanged("SatisFiyat3");
                    _SatisFiyat3 = value;
                }
            }
        }

        [DbAlan("SatisFiyat4", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat4
        {
            get { return _SatisFiyat4; }
            set
            {
                if (_SatisFiyat4 != value)
                {
                    OnPropertyChanged("SatisFiyat4");
                    _SatisFiyat4 = value;
                }
            }
        }

        [DbAlan("SatisFiyat5", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat5
        {
            get { return _SatisFiyat5; }
            set
            {
                if (_SatisFiyat5 != value)
                {
                    OnPropertyChanged("SatisFiyat5");
                    _SatisFiyat5 = value;
                }
            }
        }

        [DbAlan("SatisFiyat6", SqlDbType.Decimal, 13)]
        public decimal SatisFiyat6
        {
            get { return _SatisFiyat6; }
            set
            {
                if (_SatisFiyat6 != value)
                {
                    OnPropertyChanged("SatisFiyat6");
                    _SatisFiyat6 = value;
                }
            }
        }

        [DbAlan("DovizSatisFiyat1", SqlDbType.Decimal, 13)]
        public decimal DovizSatisFiyat1
        {
            get { return _DovizSatisFiyat1; }
            set
            {
                if (_DovizSatisFiyat1 != value)
                {
                    OnPropertyChanged("DovizSatisFiyat1");
                    _DovizSatisFiyat1 = value;
                }
            }
        }

        [DbAlan("DovizSatisFiyat2", SqlDbType.Decimal, 13)]
        public decimal DovizSatisFiyat2
        {
            get { return _DovizSatisFiyat2; }
            set
            {
                if (_DovizSatisFiyat2 != value)
                {
                    OnPropertyChanged("DovizSatisFiyat2");
                    _DovizSatisFiyat2 = value;
                }
            }
        }

        [DbAlan("DovizSatisFiyat3", SqlDbType.Decimal, 13)]
        public decimal DovizSatisFiyat3
        {
            get { return _DovizSatisFiyat3; }
            set
            {
                if (_DovizSatisFiyat3 != value)
                {
                    OnPropertyChanged("DovizSatisFiyat3");
                    _DovizSatisFiyat3 = value;
                }
            }
        }

        [DbAlan("SF1DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF1DovizCinsi
        {
            get { return _SF1DovizCinsi; }
            set
            {
                if (_SF1DovizCinsi != value)
                {
                    OnPropertyChanged("SF1DovizCinsi");
                    _SF1DovizCinsi = value;
                }
            }
        }

        [DbAlan("SF2DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF2DovizCinsi
        {
            get { return _SF2DovizCinsi; }
            set
            {
                if (_SF2DovizCinsi != value)
                {
                    OnPropertyChanged("SF2DovizCinsi");
                    _SF2DovizCinsi = value;
                }
            }
        }

        [DbAlan("SF3DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF3DovizCinsi
        {
            get { return _SF3DovizCinsi; }
            set
            {
                if (_SF3DovizCinsi != value)
                {
                    OnPropertyChanged("SF3DovizCinsi");
                    _SF3DovizCinsi = value;
                }
            }
        }

        [DbAlan("SF4DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF4DovizCinsi
        {
            get { return _SF4DovizCinsi; }
            set
            {
                if (_SF4DovizCinsi != value)
                {
                    OnPropertyChanged("SF4DovizCinsi");
                    _SF4DovizCinsi = value;
                }
            }
        }

        [DbAlan("SF5DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF5DovizCinsi
        {
            get { return _SF5DovizCinsi; }
            set
            {
                if (_SF5DovizCinsi != value)
                {
                    OnPropertyChanged("SF5DovizCinsi");
                    _SF5DovizCinsi = value;
                }
            }
        }

        [DbAlan("SF6DovizCinsi", SqlDbType.VarChar, 3)]
        public string SF6DovizCinsi
        {
            get { return _SF6DovizCinsi; }
            set
            {
                if (_SF6DovizCinsi != value)
                {
                    OnPropertyChanged("SF6DovizCinsi");
                    _SF6DovizCinsi = value;
                }
            }
        }

        [DbAlan("SF1KDV", SqlDbType.SmallInt, 2)]
        public short SF1KDV
        {
            get { return _SF1KDV; }
            set
            {
                if (_SF1KDV != value)
                {
                    OnPropertyChanged("SF1KDV");
                    _SF1KDV = value;
                }
            }
        }

        [DbAlan("SF2KDV", SqlDbType.SmallInt, 2)]
        public short SF2KDV
        {
            get { return _SF2KDV; }
            set
            {
                if (_SF2KDV != value)
                {
                    OnPropertyChanged("SF2KDV");
                    _SF2KDV = value;
                }
            }
        }

        [DbAlan("SF3KDV", SqlDbType.SmallInt, 2)]
        public short SF3KDV
        {
            get { return _SF3KDV; }
            set
            {
                if (_SF3KDV != value)
                {
                    OnPropertyChanged("SF3KDV");
                    _SF3KDV = value;
                }
            }
        }

        [DbAlan("SF4KDV", SqlDbType.SmallInt, 2)]
        public short SF4KDV
        {
            get { return _SF4KDV; }
            set
            {
                if (_SF4KDV != value)
                {
                    OnPropertyChanged("SF4KDV");
                    _SF4KDV = value;
                }
            }
        }

        [DbAlan("SF5KDV", SqlDbType.SmallInt, 2)]
        public short SF5KDV
        {
            get { return _SF5KDV; }
            set
            {
                if (_SF5KDV != value)
                {
                    OnPropertyChanged("SF5KDV");
                    _SF5KDV = value;
                }
            }
        }

        [DbAlan("SF6KDV", SqlDbType.SmallInt, 2)]
        public short SF6KDV
        {
            get { return _SF6KDV; }
            set
            {
                if (_SF6KDV != value)
                {
                    OnPropertyChanged("SF6KDV");
                    _SF6KDV = value;
                }
            }
        }

        [DbAlan("DovizSF1KDV", SqlDbType.SmallInt, 2)]
        public short DovizSF1KDV
        {
            get { return _DovizSF1KDV; }
            set
            {
                if (_DovizSF1KDV != value)
                {
                    OnPropertyChanged("DovizSF1KDV");
                    _DovizSF1KDV = value;
                }
            }
        }

        [DbAlan("DovizSF2KDV", SqlDbType.SmallInt, 2)]
        public short DovizSF2KDV
        {
            get { return _DovizSF2KDV; }
            set
            {
                if (_DovizSF2KDV != value)
                {
                    OnPropertyChanged("DovizSF2KDV");
                    _DovizSF2KDV = value;
                }
            }
        }

        [DbAlan("DovizSF3KDV", SqlDbType.SmallInt, 2)]
        public short DovizSF3KDV
        {
            get { return _DovizSF3KDV; }
            set
            {
                if (_DovizSF3KDV != value)
                {
                    OnPropertyChanged("DovizSF3KDV");
                    _DovizSF3KDV = value;
                }
            }
        }

        [DbAlan("SF1Birim", SqlDbType.SmallInt, 2)]
        public short SF1Birim
        {
            get { return _SF1Birim; }
            set
            {
                if (_SF1Birim != value)
                {
                    OnPropertyChanged("SF1Birim");
                    _SF1Birim = value;
                }
            }
        }

        [DbAlan("SF2Birim", SqlDbType.SmallInt, 2)]
        public short SF2Birim
        {
            get { return _SF2Birim; }
            set
            {
                if (_SF2Birim != value)
                {
                    OnPropertyChanged("SF2Birim");
                    _SF2Birim = value;
                }
            }
        }

        [DbAlan("SF3Birim", SqlDbType.SmallInt, 2)]
        public short SF3Birim
        {
            get { return _SF3Birim; }
            set
            {
                if (_SF3Birim != value)
                {
                    OnPropertyChanged("SF3Birim");
                    _SF3Birim = value;
                }
            }
        }

        [DbAlan("SF4Birim", SqlDbType.SmallInt, 2)]
        public short SF4Birim
        {
            get { return _SF4Birim; }
            set
            {
                if (_SF4Birim != value)
                {
                    OnPropertyChanged("SF4Birim");
                    _SF4Birim = value;
                }
            }
        }

        [DbAlan("SF5Birim", SqlDbType.SmallInt, 2)]
        public short SF5Birim
        {
            get { return _SF5Birim; }
            set
            {
                if (_SF5Birim != value)
                {
                    OnPropertyChanged("SF5Birim");
                    _SF5Birim = value;
                }
            }
        }

        [DbAlan("SF6Birim", SqlDbType.SmallInt, 2)]
        public short SF6Birim
        {
            get { return _SF6Birim; }
            set
            {
                if (_SF6Birim != value)
                {
                    OnPropertyChanged("SF6Birim");
                    _SF6Birim = value;
                }
            }
        }

        [DbAlan("DovizSF1Birim", SqlDbType.SmallInt, 2)]
        public short DovizSF1Birim
        {
            get { return _DovizSF1Birim; }
            set
            {
                if (_DovizSF1Birim != value)
                {
                    OnPropertyChanged("DovizSF1Birim");
                    _DovizSF1Birim = value;
                }
            }
        }

        [DbAlan("DovizSF2Birim", SqlDbType.SmallInt, 2)]
        public short DovizSF2Birim
        {
            get { return _DovizSF2Birim; }
            set
            {
                if (_DovizSF2Birim != value)
                {
                    OnPropertyChanged("DovizSF2Birim");
                    _DovizSF2Birim = value;
                }
            }
        }

        [DbAlan("DovizSF3Birim", SqlDbType.SmallInt, 2)]
        public short DovizSF3Birim
        {
            get { return _DovizSF3Birim; }
            set
            {
                if (_DovizSF3Birim != value)
                {
                    OnPropertyChanged("DovizSF3Birim");
                    _DovizSF3Birim = value;
                }
            }
        }

        [DbAlan("DvrTarih", SqlDbType.Int, 4)]
        public int DvrTarih
        {
            get { return _DvrTarih; }
            set
            {
                if (_DvrTarih != value)
                {
                    OnPropertyChanged("DvrTarih");
                    _DvrTarih = value;
                }
            }
        }

        [DbAlan("DvrMiktar", SqlDbType.Decimal, 13)]
        public decimal DvrMiktar
        {
            get { return _DvrMiktar; }
            set
            {
                if (_DvrMiktar != value)
                {
                    OnPropertyChanged("DvrMiktar");
                    _DvrMiktar = value;
                }
            }
        }

        [DbAlan("DvrTutar", SqlDbType.Decimal, 13)]
        public decimal DvrTutar
        {
            get { return _DvrTutar; }
            set
            {
                if (_DvrTutar != value)
                {
                    OnPropertyChanged("DvrTutar");
                    _DvrTutar = value;
                }
            }
        }

        [DbAlan("GirMiktar", SqlDbType.Decimal, 13)]
        public decimal GirMiktar
        {
            get { return _GirMiktar; }
            set
            {
                if (_GirMiktar != value)
                {
                    OnPropertyChanged("GirMiktar");
                    _GirMiktar = value;
                }
            }
        }

        [DbAlan("GirTutar", SqlDbType.Decimal, 13)]
        public decimal GirTutar
        {
            get { return _GirTutar; }
            set
            {
                if (_GirTutar != value)
                {
                    OnPropertyChanged("GirTutar");
                    _GirTutar = value;
                }
            }
        }

        [DbAlan("GirIskonto", SqlDbType.Decimal, 13)]
        public decimal GirIskonto
        {
            get { return _GirIskonto; }
            set
            {
                if (_GirIskonto != value)
                {
                    OnPropertyChanged("GirIskonto");
                    _GirIskonto = value;
                }
            }
        }

        [DbAlan("GirTarih", SqlDbType.Int, 4)]
        public int GirTarih
        {
            get { return _GirTarih; }
            set
            {
                if (_GirTarih != value)
                {
                    OnPropertyChanged("GirTarih");
                    _GirTarih = value;
                }
            }
        }

        [DbAlan("CikMiktar", SqlDbType.Decimal, 13)]
        public decimal CikMiktar
        {
            get { return _CikMiktar; }
            set
            {
                if (_CikMiktar != value)
                {
                    OnPropertyChanged("CikMiktar");
                    _CikMiktar = value;
                }
            }
        }

        [DbAlan("CikTutar", SqlDbType.Decimal, 13)]
        public decimal CikTutar
        {
            get { return _CikTutar; }
            set
            {
                if (_CikTutar != value)
                {
                    OnPropertyChanged("CikTutar");
                    _CikTutar = value;
                }
            }
        }

        [DbAlan("CikIskonto", SqlDbType.Decimal, 13)]
        public decimal CikIskonto
        {
            get { return _CikIskonto; }
            set
            {
                if (_CikIskonto != value)
                {
                    OnPropertyChanged("CikIskonto");
                    _CikIskonto = value;
                }
            }
        }

        [DbAlan("CikTarih", SqlDbType.Int, 4)]
        public int CikTarih
        {
            get { return _CikTarih; }
            set
            {
                if (_CikTarih != value)
                {
                    OnPropertyChanged("CikTarih");
                    _CikTarih = value;
                }
            }
        }

        [DbAlan("DvzDvrTutar", SqlDbType.Decimal, 13)]
        public decimal DvzDvrTutar
        {
            get { return _DvzDvrTutar; }
            set
            {
                if (_DvzDvrTutar != value)
                {
                    OnPropertyChanged("DvzDvrTutar");
                    _DvzDvrTutar = value;
                }
            }
        }

        [DbAlan("DvzGirTutar", SqlDbType.Decimal, 13)]
        public decimal DvzGirTutar
        {
            get { return _DvzGirTutar; }
            set
            {
                if (_DvzGirTutar != value)
                {
                    OnPropertyChanged("DvzGirTutar");
                    _DvzGirTutar = value;
                }
            }
        }

        [DbAlan("DvzGirIskTutar", SqlDbType.Decimal, 13)]
        public decimal DvzGirIskTutar
        {
            get { return _DvzGirIskTutar; }
            set
            {
                if (_DvzGirIskTutar != value)
                {
                    OnPropertyChanged("DvzGirIskTutar");
                    _DvzGirIskTutar = value;
                }
            }
        }

        [DbAlan("DvzCikTutar", SqlDbType.Decimal, 13)]
        public decimal DvzCikTutar
        {
            get { return _DvzCikTutar; }
            set
            {
                if (_DvzCikTutar != value)
                {
                    OnPropertyChanged("DvzCikTutar");
                    _DvzCikTutar = value;
                }
            }
        }

        [DbAlan("DvzCikIskTutar", SqlDbType.Decimal, 13)]
        public decimal DvzCikIskTutar
        {
            get { return _DvzCikIskTutar; }
            set
            {
                if (_DvzCikIskTutar != value)
                {
                    OnPropertyChanged("DvzCikIskTutar");
                    _DvzCikIskTutar = value;
                }
            }
        }

        [DbAlan("SonMlySekli", SqlDbType.SmallInt, 2)]
        public short SonMlySekli
        {
            get { return _SonMlySekli; }
            set
            {
                if (_SonMlySekli != value)
                {
                    OnPropertyChanged("SonMlySekli");
                    _SonMlySekli = value;
                }
            }
        }

        [DbAlan("SonMlyBirimFiyat", SqlDbType.Decimal, 13)]
        public decimal SonMlyBirimFiyat
        {
            get { return _SonMlyBirimFiyat; }
            set
            {
                if (_SonMlyBirimFiyat != value)
                {
                    OnPropertyChanged("SonMlyBirimFiyat");
                    _SonMlyBirimFiyat = value;
                }
            }
        }

        [DbAlan("SonMlyTarih", SqlDbType.Int, 4)]
        public int SonMlyTarih
        {
            get { return _SonMlyTarih; }
            set
            {
                if (_SonMlyTarih != value)
                {
                    OnPropertyChanged("SonMlyTarih");
                    _SonMlyTarih = value;
                }
            }
        }

        [DbAlan("SonAlimFatTarih", SqlDbType.Int, 4)]
        public int SonAlimFatTarih
        {
            get { return _SonAlimFatTarih; }
            set
            {
                if (_SonAlimFatTarih != value)
                {
                    OnPropertyChanged("SonAlimFatTarih");
                    _SonAlimFatTarih = value;
                }
            }
        }

        [DbAlan("SonAlimEvrakNo", SqlDbType.VarChar, 16)]
        public string SonAlimEvrakNo
        {
            get { return _SonAlimEvrakNo; }
            set
            {
                if (_SonAlimEvrakNo != value)
                {
                    OnPropertyChanged("SonAlimEvrakNo");
                    _SonAlimEvrakNo = value;
                }
            }
        }

        [DbAlan("SonAlimBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimBF
        {
            get { return _SonAlimBF; }
            set
            {
                if (_SonAlimBF != value)
                {
                    OnPropertyChanged("SonAlimBF");
                    _SonAlimBF = value;
                }
            }
        }

        [DbAlan("SonAlimCHK", SqlDbType.VarChar, 20)]
        public string SonAlimCHK
        {
            get { return _SonAlimCHK; }
            set
            {
                if (_SonAlimCHK != value)
                {
                    OnPropertyChanged("SonAlimCHK");
                    _SonAlimCHK = value;
                }
            }
        }

        [DbAlan("AlimSiparis", SqlDbType.Decimal, 13)]
        public decimal AlimSiparis
        {
            get { return _AlimSiparis; }
            set
            {
                if (_AlimSiparis != value)
                {
                    OnPropertyChanged("AlimSiparis");
                    _AlimSiparis = value;
                }
            }
        }

        [DbAlan("SatisSiparis", SqlDbType.Decimal, 13)]
        public decimal SatisSiparis
        {
            get { return _SatisSiparis; }
            set
            {
                if (_SatisSiparis != value)
                {
                    OnPropertyChanged("SatisSiparis");
                    _SatisSiparis = value;
                }
            }
        }

        [DbAlan("GumrukFon", SqlDbType.Decimal, 13)]
        public decimal GumrukFon
        {
            get { return _GumrukFon; }
            set
            {
                if (_GumrukFon != value)
                {
                    OnPropertyChanged("GumrukFon");
                    _GumrukFon = value;
                }
            }
        }

        [DbAlan("GumrukGTIPN", SqlDbType.VarChar, 30)]
        public string GumrukGTIPN
        {
            get { return _GumrukGTIPN; }
            set
            {
                if (_GumrukGTIPN != value)
                {
                    OnPropertyChanged("GumrukGTIPN");
                    _GumrukGTIPN = value;
                }
            }
        }

        [DbAlan("GumrukVergi", SqlDbType.Real, 4)]
        public double GumrukVergi
        {
            get { return _GumrukVergi; }
            set
            {
                if (_GumrukVergi != value)
                {
                    OnPropertyChanged("GumrukVergi");
                    _GumrukVergi = value;
                }
            }
        }

        [DbAlan("GirRezervasyon", SqlDbType.Decimal, 13)]
        public decimal GirRezervasyon
        {
            get { return _GirRezervasyon; }
            set
            {
                if (_GirRezervasyon != value)
                {
                    OnPropertyChanged("GirRezervasyon");
                    _GirRezervasyon = value;
                }
            }
        }

        [DbAlan("CikRezervasyon", SqlDbType.Decimal, 13)]
        public decimal CikRezervasyon
        {
            get { return _CikRezervasyon; }
            set
            {
                if (_CikRezervasyon != value)
                {
                    OnPropertyChanged("CikRezervasyon");
                    _CikRezervasyon = value;
                }
            }
        }

        [DbAlan("GirKonsinye", SqlDbType.Decimal, 13)]
        public decimal GirKonsinye
        {
            get { return _GirKonsinye; }
            set
            {
                if (_GirKonsinye != value)
                {
                    OnPropertyChanged("GirKonsinye");
                    _GirKonsinye = value;
                }
            }
        }

        [DbAlan("CikKonsinye", SqlDbType.Decimal, 13)]
        public decimal CikKonsinye
        {
            get { return _CikKonsinye; }
            set
            {
                if (_CikKonsinye != value)
                {
                    OnPropertyChanged("CikKonsinye");
                    _CikKonsinye = value;
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

        [DbAlan("ButceKodu", SqlDbType.VarChar, 50)]
        public string ButceKodu
        {
            get { return _ButceKodu; }
            set
            {
                if (_ButceKodu != value)
                {
                    OnPropertyChanged("ButceKodu");
                    _ButceKodu = value;
                }
            }
        }

        /// <summary>
        ///   0-StokKartı, 1-KarmaKoli
        /// </summary>
        [DbAlan("KartTuru", SqlDbType.SmallInt, 2)]
        public short KartTuru
        {
            get { return _KartTuru; }
            set
            {
                if (_KartTuru != value)
                {
                    OnPropertyChanged("KartTuru");
                    _KartTuru = value;
                }
            }
        }

        [DbAlan("UseSatRezervasyon", SqlDbType.SmallInt, 2)]
        public short UseSatRezervasyon
        {
            get { return _UseSatRezervasyon; }
            set
            {
                if (_UseSatRezervasyon != value)
                {
                    OnPropertyChanged("UseSatRezervasyon");
                    _UseSatRezervasyon = value;
                }
            }
        }

        [DbAlan("UseSatSiparis", SqlDbType.SmallInt, 2)]
        public short UseSatSiparis
        {
            get { return _UseSatSiparis; }
            set
            {
                if (_UseSatSiparis != value)
                {
                    OnPropertyChanged("UseSatSiparis");
                    _UseSatSiparis = value;
                }
            }
        }

        [DbAlan("UseSatFatIrs", SqlDbType.SmallInt, 2)]
        public short UseSatFatIrs
        {
            get { return _UseSatFatIrs; }
            set
            {
                if (_UseSatFatIrs != value)
                {
                    OnPropertyChanged("UseSatFatIrs");
                    _UseSatFatIrs = value;
                }
            }
        }

        [DbAlan("UseCikisIslem", SqlDbType.SmallInt, 2)]
        public short UseCikisIslem
        {
            get { return _UseCikisIslem; }
            set
            {
                if (_UseCikisIslem != value)
                {
                    OnPropertyChanged("UseCikisIslem");
                    _UseCikisIslem = value;
                }
            }
        }

        [DbAlan("UseSetUrun", SqlDbType.SmallInt, 2)]
        public short UseSetUrun
        {
            get { return _UseSetUrun; }
            set
            {
                if (_UseSetUrun != value)
                {
                    OnPropertyChanged("UseSetUrun");
                    _UseSetUrun = value;
                }
            }
        }

        [DbAlan("UseAlimRezervasyon", SqlDbType.SmallInt, 2)]
        public short UseAlimRezervasyon
        {
            get { return _UseAlimRezervasyon; }
            set
            {
                if (_UseAlimRezervasyon != value)
                {
                    OnPropertyChanged("UseAlimRezervasyon");
                    _UseAlimRezervasyon = value;
                }
            }
        }

        [DbAlan("UseAlimSiparis", SqlDbType.SmallInt, 2)]
        public short UseAlimSiparis
        {
            get { return _UseAlimSiparis; }
            set
            {
                if (_UseAlimSiparis != value)
                {
                    OnPropertyChanged("UseAlimSiparis");
                    _UseAlimSiparis = value;
                }
            }
        }

        [DbAlan("UseAlimIrsFat", SqlDbType.SmallInt, 2)]
        public short UseAlimIrsFat
        {
            get { return _UseAlimIrsFat; }
            set
            {
                if (_UseAlimIrsFat != value)
                {
                    OnPropertyChanged("UseAlimIrsFat");
                    _UseAlimIrsFat = value;
                }
            }
        }

        [DbAlan("UseGirisIslem", SqlDbType.SmallInt, 2)]
        public short UseGirisIslem
        {
            get { return _UseGirisIslem; }
            set
            {
                if (_UseGirisIslem != value)
                {
                    OnPropertyChanged("UseGirisIslem");
                    _UseGirisIslem = value;
                }
            }
        }

        [DbAlan("SF1ValorGun", SqlDbType.SmallInt, 2)]
        public short SF1ValorGun
        {
            get { return _SF1ValorGun; }
            set
            {
                if (_SF1ValorGun != value)
                {
                    OnPropertyChanged("SF1ValorGun");
                    _SF1ValorGun = value;
                }
            }
        }

        [DbAlan("SF2ValorGun", SqlDbType.SmallInt, 2)]
        public short SF2ValorGun
        {
            get { return _SF2ValorGun; }
            set
            {
                if (_SF2ValorGun != value)
                {
                    OnPropertyChanged("SF2ValorGun");
                    _SF2ValorGun = value;
                }
            }
        }

        [DbAlan("SF3ValorGun", SqlDbType.SmallInt, 2)]
        public short SF3ValorGun
        {
            get { return _SF3ValorGun; }
            set
            {
                if (_SF3ValorGun != value)
                {
                    OnPropertyChanged("SF3ValorGun");
                    _SF3ValorGun = value;
                }
            }
        }

        [DbAlan("SF4ValorGun", SqlDbType.SmallInt, 2)]
        public short SF4ValorGun
        {
            get { return _SF4ValorGun; }
            set
            {
                if (_SF4ValorGun != value)
                {
                    OnPropertyChanged("SF4ValorGun");
                    _SF4ValorGun = value;
                }
            }
        }

        [DbAlan("SF5ValorGun", SqlDbType.SmallInt, 2)]
        public short SF5ValorGun
        {
            get { return _SF5ValorGun; }
            set
            {
                if (_SF5ValorGun != value)
                {
                    OnPropertyChanged("SF5ValorGun");
                    _SF5ValorGun = value;
                }
            }
        }

        [DbAlan("SF6ValorGun", SqlDbType.SmallInt, 2)]
        public short SF6ValorGun
        {
            get { return _SF6ValorGun; }
            set
            {
                if (_SF6ValorGun != value)
                {
                    OnPropertyChanged("SF6ValorGun");
                    _SF6ValorGun = value;
                }
            }
        }

        [DbAlan("SF1DvzValorGun", SqlDbType.SmallInt, 2)]
        public short SF1DvzValorGun
        {
            get { return _SF1DvzValorGun; }
            set
            {
                if (_SF1DvzValorGun != value)
                {
                    OnPropertyChanged("SF1DvzValorGun");
                    _SF1DvzValorGun = value;
                }
            }
        }

        [DbAlan("SF2DvzValorGun", SqlDbType.SmallInt, 2)]
        public short SF2DvzValorGun
        {
            get { return _SF2DvzValorGun; }
            set
            {
                if (_SF2DvzValorGun != value)
                {
                    OnPropertyChanged("SF2DvzValorGun");
                    _SF2DvzValorGun = value;
                }
            }
        }

        [DbAlan("SF3DvzValorGun", SqlDbType.SmallInt, 2)]
        public short SF3DvzValorGun
        {
            get { return _SF3DvzValorGun; }
            set
            {
                if (_SF3DvzValorGun != value)
                {
                    OnPropertyChanged("SF3DvzValorGun");
                    _SF3DvzValorGun = value;
                }
            }
        }

        [DbAlan("SatisFiyatTip", SqlDbType.SmallInt, 2)]
        public short SatisFiyatTip
        {
            get { return _SatisFiyatTip; }
            set
            {
                if (_SatisFiyatTip != value)
                {
                    OnPropertyChanged("SatisFiyatTip");
                    _SatisFiyatTip = value;
                }
            }
        }

        [DbAlan("SatisFiyatAltLimit", SqlDbType.Decimal, 13)]
        public decimal SatisFiyatAltLimit
        {
            get { return _SatisFiyatAltLimit; }
            set
            {
                if (_SatisFiyatAltLimit != value)
                {
                    OnPropertyChanged("SatisFiyatAltLimit");
                    _SatisFiyatAltLimit = value;
                }
            }
        }

        [DbAlan("SatisFiyatUstLimit", SqlDbType.Decimal, 13)]
        public decimal SatisFiyatUstLimit
        {
            get { return _SatisFiyatUstLimit; }
            set
            {
                if (_SatisFiyatUstLimit != value)
                {
                    OnPropertyChanged("SatisFiyatUstLimit");
                    _SatisFiyatUstLimit = value;
                }
            }
        }

        [DbAlan("SonSayimTarih", SqlDbType.Int, 4)]
        public int SonSayimTarih
        {
            get { return _SonSayimTarih; }
            set
            {
                if (_SonSayimTarih != value)
                {
                    OnPropertyChanged("SonSayimTarih");
                    _SonSayimTarih = value;
                }
            }
        }

        [DbAlan("SonSayimSonuc", SqlDbType.Decimal, 13)]
        public decimal SonSayimSonuc
        {
            get { return _SonSayimSonuc; }
            set
            {
                if (_SonSayimSonuc != value)
                {
                    OnPropertyChanged("SonSayimSonuc");
                    _SonSayimSonuc = value;
                }
            }
        }

        [DbAlan("SonSayimFark", SqlDbType.Decimal, 13)]
        public decimal SonSayimFark
        {
            get { return _SonSayimFark; }
            set
            {
                if (_SonSayimFark != value)
                {
                    OnPropertyChanged("SonSayimFark");
                    _SonSayimFark = value;
                }
            }
        }

        [DbAlan("Notlar", SqlDbType.VarChar, 64)]
        public string Notlar
        {
            get { return _Notlar; }
            set
            {
                if (_Notlar != value)
                {
                    OnPropertyChanged("Notlar");
                    _Notlar = value;
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

        [DbAlan("ElekTicSitList", SqlDbType.SmallInt, 2)]
        public short ElekTicSitList
        {
            get { return _ElekTicSitList; }
            set
            {
                if (_ElekTicSitList != value)
                {
                    OnPropertyChanged("ElekTicSitList");
                    _ElekTicSitList = value;
                }
            }
        }

        [DbAlan("WebMagSitList", SqlDbType.SmallInt, 2)]
        public short WebMagSitList
        {
            get { return _WebMagSitList; }
            set
            {
                if (_WebMagSitList != value)
                {
                    OnPropertyChanged("WebMagSitList");
                    _WebMagSitList = value;
                }
            }
        }

        [DbAlan("DagZinSitList", SqlDbType.SmallInt, 2)]
        public short DagZinSitList
        {
            get { return _DagZinSitList; }
            set
            {
                if (_DagZinSitList != value)
                {
                    OnPropertyChanged("DagZinSitList");
                    _DagZinSitList = value;
                }
            }
        }

        [DbAlan("SirIciSipSitList", SqlDbType.SmallInt, 2)]
        public short SirIciSipSitList
        {
            get { return _SirIciSipSitList; }
            set
            {
                if (_SirIciSipSitList != value)
                {
                    OnPropertyChanged("SirIciSipSitList");
                    _SirIciSipSitList = value;
                }
            }
        }

        [DbAlan("ElekTicSitListAdi", SqlDbType.SmallInt, 2)]
        public short ElekTicSitListAdi
        {
            get { return _ElekTicSitListAdi; }
            set
            {
                if (_ElekTicSitListAdi != value)
                {
                    OnPropertyChanged("ElekTicSitListAdi");
                    _ElekTicSitListAdi = value;
                }
            }
        }

        [DbAlan("WebMagSitListAdi", SqlDbType.SmallInt, 2)]
        public short WebMagSitListAdi
        {
            get { return _WebMagSitListAdi; }
            set
            {
                if (_WebMagSitListAdi != value)
                {
                    OnPropertyChanged("WebMagSitListAdi");
                    _WebMagSitListAdi = value;
                }
            }
        }

        [DbAlan("DagZinSitListAdi", SqlDbType.SmallInt, 2)]
        public short DagZinSitListAdi
        {
            get { return _DagZinSitListAdi; }
            set
            {
                if (_DagZinSitListAdi != value)
                {
                    OnPropertyChanged("DagZinSitListAdi");
                    _DagZinSitListAdi = value;
                }
            }
        }

        [DbAlan("SirIciSipSitListAdi", SqlDbType.SmallInt, 2)]
        public short SirIciSipSitListAdi
        {
            get { return _SirIciSipSitListAdi; }
            set
            {
                if (_SirIciSipSitListAdi != value)
                {
                    OnPropertyChanged("SirIciSipSitListAdi");
                    _SirIciSipSitListAdi = value;
                }
            }
        }

        [DbAlan("DemirbasKodu", SqlDbType.VarChar, 20)]
        public string DemirbasKodu
        {
            get { return _DemirbasKodu; }
            set
            {
                if (_DemirbasKodu != value)
                {
                    OnPropertyChanged("DemirbasKodu");
                    _DemirbasKodu = value;
                }
            }
        }

        /// <summary>
        ///    0-Miktarsız, 1-Miktarlı
        /// </summary>
        [DbAlan("MiktarTakibi", SqlDbType.SmallInt, 2)]
        public short MiktarTakibi
        {
            get { return _MiktarTakibi; }
            set
            {
                if (_MiktarTakibi != value)
                {
                    OnPropertyChanged("MiktarTakibi");
                    _MiktarTakibi = value;
                }
            }
        }

        /// <summary>
        ///        0-YTL/TL, 1-Döviz, 2-2005 Öncesi TL
        /// </summary>
        [DbAlan("BakGostSekli", SqlDbType.SmallInt, 2)]
        public short BakGostSekli
        {
            get { return _BakGostSekli; }
            set
            {
                if (_BakGostSekli != value)
                {
                    OnPropertyChanged("BakGostSekli");
                    _BakGostSekli = value;
                }
            }
        }

        /// <summary>
        /// 0-Ticari Mal, 1-Mamül, 2-Yarı Mamül, 3-Sarf Malzemesi, 4-Sabit Kıymet,
        /// 5-Hizmet, 6-Masraf, 7-Vade Farkı/ Kur Farkı, 8-Diğer
        /// </summary>
        [DbAlan("KartTip", SqlDbType.SmallInt, 2)]
        public short KartTip
        {
            get { return _KartTip; }
            set
            {
                if (_KartTip != value)
                {
                    OnPropertyChanged("KartTip");
                    _KartTip = value;
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

        [DbAlan("AlimTeklif", SqlDbType.Decimal, 13)]
        public decimal AlimTeklif
        {
            get { return _AlimTeklif; }
            set
            {
                if (_AlimTeklif != value)
                {
                    OnPropertyChanged("AlimTeklif");
                    _AlimTeklif = value;
                }
            }
        }

        [DbAlan("SatisTeklif", SqlDbType.Decimal, 13)]
        public decimal SatisTeklif
        {
            get { return _SatisTeklif; }
            set
            {
                if (_SatisTeklif != value)
                {
                    OnPropertyChanged("SatisTeklif");
                    _SatisTeklif = value;
                }
            }
        }

        [DbAlan("SatMalMaliyetHesap", SqlDbType.VarChar, 50)]
        public string SatMalMaliyetHesap
        {
            get { return _SatMalMaliyetHesap; }
            set
            {
                if (_SatMalMaliyetHesap != value)
                {
                    OnPropertyChanged("SatMalMaliyetHesap");
                    _SatMalMaliyetHesap = value;
                }
            }
        }

        /// <summary>
        /// 0-Pasif, 1-Aktif
        /// </summary>
        [DbAlan("AktifPasif", SqlDbType.SmallInt, 2)]
        public short AktifPasif
        {
            get { return _AktifPasif; }
            set
            {
                if (_AktifPasif != value)
                {
                    OnPropertyChanged("AktifPasif");
                    _AktifPasif = value;
                }
            }
        }

        [DbAlan("TevfikatOran", SqlDbType.SmallInt, 2)]
        public short TevfikatOran
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

        [DbAlan("SonAlimNetBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimNetBF
        {
            get { return _SonAlimNetBF; }
            set
            {
                if (_SonAlimNetBF != value)
                {
                    OnPropertyChanged("SonAlimNetBF");
                    _SonAlimNetBF = value;
                }
            }
        }

        [DbAlan("SonAlimDvzBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimDvzBF
        {
            get { return _SonAlimDvzBF; }
            set
            {
                if (_SonAlimDvzBF != value)
                {
                    OnPropertyChanged("SonAlimDvzBF");
                    _SonAlimDvzBF = value;
                }
            }
        }

        [DbAlan("SonAlimDvzNetBF", SqlDbType.Decimal, 13)]
        public decimal SonAlimDvzNetBF
        {
            get { return _SonAlimDvzNetBF; }
            set
            {
                if (_SonAlimDvzNetBF != value)
                {
                    OnPropertyChanged("SonAlimDvzNetBF");
                    _SonAlimDvzNetBF = value;
                }
            }
        }

        [DbAlan("YDAlimlarHesabi", SqlDbType.VarChar, 50)]
        public string YDAlimlarHesabi
        {
            get { return _YDAlimlarHesabi; }
            set
            {
                if (_YDAlimlarHesabi != value)
                {
                    OnPropertyChanged("YDAlimlarHesabi");
                    _YDAlimlarHesabi = value;
                }
            }
        }

        [DbAlan("TevkifatAlis", SqlDbType.VarChar, 7)]
        public string TevkifatAlis
        {
            get { return _TevkifatAlis; }
            set
            {
                if (_TevkifatAlis != value)
                {
                    OnPropertyChanged("TevkifatAlis");
                    _TevkifatAlis = value;
                }
            }
        }

        [DbAlan("TevkifatSatis", SqlDbType.VarChar, 7)]
        public string TevkifatSatis
        {
            get { return _TevkifatSatis; }
            set
            {
                if (_TevkifatSatis != value)
                {
                    OnPropertyChanged("TevkifatSatis");
                    _TevkifatSatis = value;
                }
            }
        }

        [DbAlan("TevkifatAlisTam", SqlDbType.VarChar, 5)]
        public string TevkifatAlisTam
        {
            get { return _TevkifatAlisTam; }
            set
            {
                if (_TevkifatAlisTam != value)
                {
                    OnPropertyChanged("TevkifatAlisTam");
                    _TevkifatAlisTam = value;
                }
            }
        }

        [DbAlan("Kod14", SqlDbType.VarChar, 20)]
        public string Kod14
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

        [DbAlan("Kod15", SqlDbType.VarChar, 20)]
        public string Kod15
        {
            get { return _Kod15; }
            set
            {
                if (_Kod15 != value)
                {
                    OnPropertyChanged("Kod15");
                    _Kod15 = value;
                }
            }
        }

        [DbAlan("Kod16", SqlDbType.VarChar, 20)]
        public string Kod16
        {
            get { return _Kod16; }
            set
            {
                if (_Kod16 != value)
                {
                    OnPropertyChanged("Kod16");
                    _Kod16 = value;
                }
            }
        }

        [DbAlan("Kod17", SqlDbType.VarChar, 20)]
        public string Kod17
        {
            get { return _Kod17; }
            set
            {
                if (_Kod17 != value)
                {
                    OnPropertyChanged("Kod17");
                    _Kod17 = value;
                }
            }
        }

        [DbAlan("Kod18", SqlDbType.VarChar, 20)]
        public string Kod18
        {
            get { return _Kod18; }
            set
            {
                if (_Kod18 != value)
                {
                    OnPropertyChanged("Kod18");
                    _Kod18 = value;
                }
            }
        }

        [DbAlan("Row_ID", SqlDbType.Int, 4, true, false, false)]
        public int Row_ID
        {
            get { return _Row_ID; }
            set
            {
                if (_Row_ID != value)
                {
                    OnPropertyChanged("Row_ID");
                    _Row_ID = value;
                }
            }
        }

        #endregion table Properties



        #region property changes events functions

        public event PropertyChangedEventHandler PropertyChanged;
        private List<PropertyValue> _ChangedList = new List<PropertyValue>();
        public PropertyValue[] ChangedList { get { return _ChangedList.ToArray(); } }



        internal void STK_PropertyChanged(object sender, PropertyChangedEventArgs e)
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



        public KKP_STK()
        {
            ///this.PropertyChanged += this.STK_PropertyChanged;
        }

    }
}
