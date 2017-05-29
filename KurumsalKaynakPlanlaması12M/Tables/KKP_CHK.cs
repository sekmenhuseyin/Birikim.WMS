using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{
     [DbTablo("FINSAT6", "FINSAT6", "CHK")]
    public class KKP_CHK : INotifyPropertyChanged
    {

        #region Fields
        private short _KartTip;
        private string _HesapKodu;
        private string _Unvan1;
        private string _Unvan2;
        private string _Yetkili1;
        private string _Yetkili2;
        private string _FaturaAdres1;
        private string _FaturaAdres2;
        private string _FaturaAdres3;
        private string _TeslimAdres1;
        private string _TeslimAdres2;
        private string _TeslimAdres3;
        private string _VergiDairesi;
        private string _HesapNo;
        private string _Telefon1;
        private string _Telefon2;
        private string _Telefon3;
        private string _Telefon4;
        private string _Fax1;
        private string _Fax2;
        private string _MusterekHesap;
        private int _MutabakatTarih;
        private double _IskontoOran;
        private short _OpsiyonGunu;
        private short _OdemeGunu;
        private string _DovizCinsi;
        private string _BolgeKod;
        private string _OzelKod;
        private string _GrupKod;
        private string _TipKod;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private string _Kod4;
        private decimal _Kod5;
        private decimal _Kod6;
        private string _Kod7;
        private string _KOD8;
        private string _Kod9;
        private short _Kod10;
        private short _Kod11;
        private decimal _Kod12;
        private decimal _Kod13;
        private string _MhsKod;
        private string _MasrafMerkezi;
        private short _KulSatisFiyat;
        private int _DvrTarih;
        private int _BorcTarih;
        private int _AlacakTarih;
        private int _RiskBorcTarih;
        private int _RiskAlacakTarih;
        private decimal _DvrSntRiskB;
        private decimal _DvrSntRiskA;
        private decimal _DvrCekRiskB;
        private decimal _DvrCekRiskA;
        private decimal _SntRiskB;
        private decimal _SntRiskA;
        private decimal _CekRiskB;
        private decimal _CekRiskA;
        private decimal _DvrTeminat1B;
        private decimal _DvrTeminat1A;
        private decimal _Teminat1B;
        private decimal _Teminat1A;
        private decimal _DvrTeminat2B;
        private decimal _DvrTeminat2A;
        private decimal _Teminat2B;
        private decimal _Teminat2A;
        private decimal _DvrB;
        private decimal _DvrA;
        private decimal _OdemeB;
        private decimal _OdemeA;
        private decimal _CiroB;
        private decimal _CiroA;
        private decimal _IadeFatB;
        private decimal _IadeFatA;
        private decimal _KDVB;
        private decimal _KDVA;
        private decimal _DigerB;
        private decimal _DigerA;
        private decimal _DvrProtSnt;
        private decimal _ProtSnt;
        private decimal _DvrKarsCek;
        private decimal _KarsCek;
        private decimal _KrediLimiti;
        private decimal _DvzKrediLimiti;
        private decimal _MtbkBak;
        private decimal _DvzMtbkBak;
        private decimal _DvzDvrB;
        private decimal _DvzDvrA;
        private decimal _DvzDigerB;
        private decimal _DvzDigerA;
        private decimal _DvzOdemeB;
        private decimal _DvzOdemeA;
        private decimal _DvzCiroB;
        private decimal _DvzCiroA;
        private decimal _DvzIadeB;
        private decimal _DvzIadeA;
        private decimal _DvzKDVB;
        private decimal _DvzKDVA;
        private decimal _DvrDvzSntRiskB;
        private decimal _DvrDvzSntRiskA;
        private decimal _DvrDvzCekRiskB;
        private decimal _DvrDvzCekRiskA;
        private decimal _DvrDvzTeminatB;
        private decimal _DvrDvzTeminatA;
        private decimal _DvrDvzTeminat2B;
        private decimal _DvrDvzTeminat2A;
        private decimal _DvzTeminatB;
        private decimal _DvzTeminatA;
        private decimal _DvzTeminat2B;
        private decimal _DvzTeminat2A;
        private decimal _DvzSntRiskB;
        private decimal _DvzSntRiskiA;
        private decimal _DvzCekRiskB;
        private decimal _DvzCekRiskA;
        private decimal _DvzProtSnt;
        private decimal _DvrDvzProtSenet;
        private decimal _DvzKarsCek;
        private decimal _DvrDvzKarsCek;
        private decimal _YasBakiye;
        private int _OrtalamaTarih;
        private short _OrtalamaGun;
        private decimal _OtvBorc;
        private decimal _OtvAlacak;
        private decimal _OtvDvzBorc;
        private decimal _OtvDvzAlacak;
        private string _Nesne1;
        private string _Nesne2;
        private string _Nesne3;
        private string _Notlar;
        private string _UygFiyatListeNo;
        private decimal _FatIrsDenDoganBorc;
        private decimal _FatIrsDenDoganAlacak;
        private decimal _BekleyenSatisSip;
        private decimal _BekleyenAlimSip;
        private decimal _FatIrsdenDoganDovizBorc;
        private decimal _FatIrsdenDoganDovizAlacak;
        private decimal _BekleyenSatSipDoviz;
        private decimal _BekleyenAlimSipDoviz;
        private string _YetkiliEmail1;
        private string _YetkiliEmail2;
        private string _HavaleEdilecekBanka;
        private string _HavaleEdilecekBankaSubesi;
        private string _HavaleEdilecekHesapNo;
        private string _DovizIslemBanka;
        private short _DovizIslemKurCins;
        private string _ButceKod;
        private string _FaturaChk;
        private string _FaturaAdres1Sehir;
        private string _FaturaAdres1Ulke;
        private string _FaturaAdres1PKod;
        private string _TeslimAdres1Sehir;
        private string _TeslimAdres1Ulke;
        private string _TeslimAdres1PKod;
        private string _Telefon1Dahili;
        private string _Telefon1BolgeKod;
        private string _Telefon1UlkeKod;
        private string _Telefon2Dahili;
        private string _Telefon2BolgeKod;
        private string _Telefon2UlkeKod;
        private string _Telefon3Dahili;
        private string _Telefon3BolgeKod;
        private string _Telefon3UlkeKod;
        private string _Telefon4Dahili;
        private string _Telefon4BolgeKod;
        private string _Telefon4UlkeKod;
        private string _Fax1BolgeKod;
        private string _Fax1UlkeKodu;
        private string _Fax2BolgeKod;
        private string _Fax2UlkeKodu;
        private string _BankaKod1;
        private string _BankaIlKod1;
        private string _BankaSubeKod1;
        private string _BankaHesap1;
        private string _BankaKod2;
        private string _BankaIlKod2;
        private string _BankaSubeKod2;
        private string _BankaHesap2;
        private string _BankaKod3;
        private string _BankaIlKod3;
        private string _BankaSubeKod3;
        private string _BankaHesap3;
        private string _BankaKod4;
        private string _BankaIlKod4;
        private string _BankaSubeKod4;
        private string _BankaHesap4;
        private string _SirketEMail;
        private string _SirketWebAdres;
        private short _TeslimatSekli;
        private short _SatisKurCinsi;
        private short _AlisKurCinsi;
        private short _FaturaMalAdi;
        private short _DvzTL;
        private int _OdemePlani;
        private string _SatIslemEMail;
        private string _SatAlmaIslemEMail;
        private string _FinIslemEMail;
        private string _CHKodu;
        private string _UlkeNumKod;
        private short _FormBaBsUnvan;
        private decimal _OivBorc;
        private decimal _OivAlacak;
        private decimal _OivDvzBorc;
        private decimal _OivDvzAlacak;
        private string _BankaUlkeNumKod1;
        private string _BankaUlkeNumKod2;
        private string _BankaUlkeNumKod3;
        private string _BankaUlkeNumKod4;
        private short _IslemTipi;
        private short _BakGostSekli;
        private short _HesapTuru;
        private string _VergiDairesi2;
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
        private decimal _BekleyenSatisTkf;
        private decimal _BekleyenAlimTkf;
        private decimal _BekleyenSatTkfDoviz;
        private decimal _BekleyenAlimTkfDoviz;
        private string _BankaIBAN1;
        private string _BankaIBAN2;
        private string _BankaIBAN3;
        private string _BankaIBAN4;
        private string _BankaDvzCinsi1;
        private string _BankaDvzCinsi2;
        private string _BankaDvzCinsi3;
        private string _BankaDvzCinsi4;
        private short _AktifPasif;
        private string _IrsaliyeRaporAdi;
        private string _FaturaRaporAdi;
        private short _KDVTevfikatUygula;
        private string _Kod14;
        private string _Kod15;
        private string _Kod16;
        private string _Kod17;
        private string _Kod18;
        private string _EFatAdresCadde;
        private string _EFatAdresBinaAdi;
        private string _EFatAdresDisKapiNo;
        private string _EFatAdresIcKapiNo;
        private string _EFatAdresKasabaKoy;
        private string _EFatAdresIlce;
        private short _EFatKullanici;
        private short _EFatSenaryo;
        private string _EFatEtiket;
        private string _Ad;
        private string _SoyAd;
        private string _EFatMusBrmSubeTnm;
        private string _EFatMusBrmSubeTnmDeger;
        private string _EFatCHKoduTnm;
        private short _KDVMuaf;
        private string _KDVMuafAck;
        private int _ROW_ID;

        #endregion Fields



        #region table Properties
        /// <summary>
        /// 0-Müşteri, 1-Satıcı, 2-Banka, 3-Kasa, 4-Diğer
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

        [DbAlan("HesapKodu", SqlDbType.VarChar, 20, false, true, false)]
        public string HesapKodu
        {
            get { return _HesapKodu; }
            set
            {
                if (_HesapKodu != value)
                {
                    OnPropertyChanged("HesapKodu");
                    _HesapKodu = value;
                }
            }
        }

        [DbAlan("Unvan1", SqlDbType.VarChar, 40)]
        public string Unvan1
        {
            get { return _Unvan1; }
            set
            {
                if (_Unvan1 != value)
                {
                    OnPropertyChanged("Unvan1");
                    _Unvan1 = value;
                }
            }
        }

        [DbAlan("Unvan2", SqlDbType.VarChar, 40)]
        public string Unvan2
        {
            get { return _Unvan2; }
            set
            {
                if (_Unvan2 != value)
                {
                    OnPropertyChanged("Unvan2");
                    _Unvan2 = value;
                }
            }
        }

        [DbAlan("Yetkili1", SqlDbType.VarChar, 30)]
        public string Yetkili1
        {
            get { return _Yetkili1; }
            set
            {
                if (_Yetkili1 != value)
                {
                    OnPropertyChanged("Yetkili1");
                    _Yetkili1 = value;
                }
            }
        }

        [DbAlan("Yetkili2", SqlDbType.VarChar, 30)]
        public string Yetkili2
        {
            get { return _Yetkili2; }
            set
            {
                if (_Yetkili2 != value)
                {
                    OnPropertyChanged("Yetkili2");
                    _Yetkili2 = value;
                }
            }
        }

        [DbAlan("FaturaAdres1", SqlDbType.VarChar, 40)]
        public string FaturaAdres1
        {
            get { return _FaturaAdres1; }
            set
            {
                if (_FaturaAdres1 != value)
                {
                    OnPropertyChanged("FaturaAdres1");
                    _FaturaAdres1 = value;
                }
            }
        }

        [DbAlan("FaturaAdres2", SqlDbType.VarChar, 40)]
        public string FaturaAdres2
        {
            get { return _FaturaAdres2; }
            set
            {
                if (_FaturaAdres2 != value)
                {
                    OnPropertyChanged("FaturaAdres2");
                    _FaturaAdres2 = value;
                }
            }
        }

        [DbAlan("FaturaAdres3", SqlDbType.VarChar, 40)]
        public string FaturaAdres3
        {
            get { return _FaturaAdres3; }
            set
            {
                if (_FaturaAdres3 != value)
                {
                    OnPropertyChanged("FaturaAdres3");
                    _FaturaAdres3 = value;
                }
            }
        }

        [DbAlan("TeslimAdres1", SqlDbType.VarChar, 40)]
        public string TeslimAdres1
        {
            get { return _TeslimAdres1; }
            set
            {
                if (_TeslimAdres1 != value)
                {
                    OnPropertyChanged("TeslimAdres1");
                    _TeslimAdres1 = value;
                }
            }
        }

        [DbAlan("TeslimAdres2", SqlDbType.VarChar, 40)]
        public string TeslimAdres2
        {
            get { return _TeslimAdres2; }
            set
            {
                if (_TeslimAdres2 != value)
                {
                    OnPropertyChanged("TeslimAdres2");
                    _TeslimAdres2 = value;
                }
            }
        }

        [DbAlan("TeslimAdres3", SqlDbType.VarChar, 40)]
        public string TeslimAdres3
        {
            get { return _TeslimAdres3; }
            set
            {
                if (_TeslimAdres3 != value)
                {
                    OnPropertyChanged("TeslimAdres3");
                    _TeslimAdres3 = value;
                }
            }
        }

        [DbAlan("VergiDairesi", SqlDbType.VarChar, 6)]
        public string VergiDairesi
        {
            get { return _VergiDairesi; }
            set
            {
                if (_VergiDairesi != value)
                {
                    OnPropertyChanged("VergiDairesi");
                    _VergiDairesi = value;
                }
            }
        }

        [DbAlan("HesapNo", SqlDbType.VarChar, 20)]
        public string HesapNo
        {
            get { return _HesapNo; }
            set
            {
                if (_HesapNo != value)
                {
                    OnPropertyChanged("HesapNo");
                    _HesapNo = value;
                }
            }
        }

        [DbAlan("Telefon1", SqlDbType.VarChar, 7)]
        public string Telefon1
        {
            get { return _Telefon1; }
            set
            {
                if (_Telefon1 != value)
                {
                    OnPropertyChanged("Telefon1");
                    _Telefon1 = value;
                }
            }
        }

        [DbAlan("Telefon2", SqlDbType.VarChar, 7)]
        public string Telefon2
        {
            get { return _Telefon2; }
            set
            {
                if (_Telefon2 != value)
                {
                    OnPropertyChanged("Telefon2");
                    _Telefon2 = value;
                }
            }
        }

        [DbAlan("Telefon3", SqlDbType.VarChar, 7)]
        public string Telefon3
        {
            get { return _Telefon3; }
            set
            {
                if (_Telefon3 != value)
                {
                    OnPropertyChanged("Telefon3");
                    _Telefon3 = value;
                }
            }
        }

        [DbAlan("Telefon4", SqlDbType.VarChar, 7)]
        public string Telefon4
        {
            get { return _Telefon4; }
            set
            {
                if (_Telefon4 != value)
                {
                    OnPropertyChanged("Telefon4");
                    _Telefon4 = value;
                }
            }
        }

        [DbAlan("Fax1", SqlDbType.VarChar, 7)]
        public string Fax1
        {
            get { return _Fax1; }
            set
            {
                if (_Fax1 != value)
                {
                    OnPropertyChanged("Fax1");
                    _Fax1 = value;
                }
            }
        }

        [DbAlan("Fax2", SqlDbType.VarChar, 7)]
        public string Fax2
        {
            get { return _Fax2; }
            set
            {
                if (_Fax2 != value)
                {
                    OnPropertyChanged("Fax2");
                    _Fax2 = value;
                }
            }
        }

        [DbAlan("MusterekHesap", SqlDbType.VarChar, 20)]
        public string MusterekHesap
        {
            get { return _MusterekHesap; }
            set
            {
                if (_MusterekHesap != value)
                {
                    OnPropertyChanged("MusterekHesap");
                    _MusterekHesap = value;
                }
            }
        }

        [DbAlan("MutabakatTarih", SqlDbType.Int, 4)]
        public int MutabakatTarih
        {
            get { return _MutabakatTarih; }
            set
            {
                if (_MutabakatTarih != value)
                {
                    OnPropertyChanged("MutabakatTarih");
                    _MutabakatTarih = value;
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

        [DbAlan("OpsiyonGunu", SqlDbType.SmallInt, 2)]
        public short OpsiyonGunu
        {
            get { return _OpsiyonGunu; }
            set
            {
                if (_OpsiyonGunu != value)
                {
                    OnPropertyChanged("OpsiyonGunu");
                    _OpsiyonGunu = value;
                }
            }
        }

        [DbAlan("OdemeGunu", SqlDbType.SmallInt, 2)]
        public short OdemeGunu
        {
            get { return _OdemeGunu; }
            set
            {
                if (_OdemeGunu != value)
                {
                    OnPropertyChanged("OdemeGunu");
                    _OdemeGunu = value;
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

        [DbAlan("BolgeKod", SqlDbType.VarChar, 4)]
        public string BolgeKod
        {
            get { return _BolgeKod; }
            set
            {
                if (_BolgeKod != value)
                {
                    OnPropertyChanged("BolgeKod");
                    _BolgeKod = value;
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

        [DbAlan("KOD8", SqlDbType.VarChar, 20)]
        public string KOD8
        {
            get { return _KOD8; }
            set
            {
                if (_KOD8 != value)
                {
                    OnPropertyChanged("KOD8");
                    _KOD8 = value;
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

        [DbAlan("KulSatisFiyat", SqlDbType.SmallInt, 2)]
        public short KulSatisFiyat
        {
            get { return _KulSatisFiyat; }
            set
            {
                if (_KulSatisFiyat != value)
                {
                    OnPropertyChanged("KulSatisFiyat");
                    _KulSatisFiyat = value;
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

        [DbAlan("BorcTarih", SqlDbType.Int, 4)]
        public int BorcTarih
        {
            get { return _BorcTarih; }
            set
            {
                if (_BorcTarih != value)
                {
                    OnPropertyChanged("BorcTarih");
                    _BorcTarih = value;
                }
            }
        }

        [DbAlan("AlacakTarih", SqlDbType.Int, 4)]
        public int AlacakTarih
        {
            get { return _AlacakTarih; }
            set
            {
                if (_AlacakTarih != value)
                {
                    OnPropertyChanged("AlacakTarih");
                    _AlacakTarih = value;
                }
            }
        }

        [DbAlan("RiskBorcTarih", SqlDbType.Int, 4)]
        public int RiskBorcTarih
        {
            get { return _RiskBorcTarih; }
            set
            {
                if (_RiskBorcTarih != value)
                {
                    OnPropertyChanged("RiskBorcTarih");
                    _RiskBorcTarih = value;
                }
            }
        }

        [DbAlan("RiskAlacakTarih", SqlDbType.Int, 4)]
        public int RiskAlacakTarih
        {
            get { return _RiskAlacakTarih; }
            set
            {
                if (_RiskAlacakTarih != value)
                {
                    OnPropertyChanged("RiskAlacakTarih");
                    _RiskAlacakTarih = value;
                }
            }
        }

        [DbAlan("DvrSntRiskB", SqlDbType.Decimal, 13)]
        public decimal DvrSntRiskB
        {
            get { return _DvrSntRiskB; }
            set
            {
                if (_DvrSntRiskB != value)
                {
                    OnPropertyChanged("DvrSntRiskB");
                    _DvrSntRiskB = value;
                }
            }
        }

        [DbAlan("DvrSntRiskA", SqlDbType.Decimal, 13)]
        public decimal DvrSntRiskA
        {
            get { return _DvrSntRiskA; }
            set
            {
                if (_DvrSntRiskA != value)
                {
                    OnPropertyChanged("DvrSntRiskA");
                    _DvrSntRiskA = value;
                }
            }
        }

        [DbAlan("DvrCekRiskB", SqlDbType.Decimal, 13)]
        public decimal DvrCekRiskB
        {
            get { return _DvrCekRiskB; }
            set
            {
                if (_DvrCekRiskB != value)
                {
                    OnPropertyChanged("DvrCekRiskB");
                    _DvrCekRiskB = value;
                }
            }
        }

        [DbAlan("DvrCekRiskA", SqlDbType.Decimal, 13)]
        public decimal DvrCekRiskA
        {
            get { return _DvrCekRiskA; }
            set
            {
                if (_DvrCekRiskA != value)
                {
                    OnPropertyChanged("DvrCekRiskA");
                    _DvrCekRiskA = value;
                }
            }
        }

        [DbAlan("SntRiskB", SqlDbType.Decimal, 13)]
        public decimal SntRiskB
        {
            get { return _SntRiskB; }
            set
            {
                if (_SntRiskB != value)
                {
                    OnPropertyChanged("SntRiskB");
                    _SntRiskB = value;
                }
            }
        }

        [DbAlan("SntRiskA", SqlDbType.Decimal, 13)]
        public decimal SntRiskA
        {
            get { return _SntRiskA; }
            set
            {
                if (_SntRiskA != value)
                {
                    OnPropertyChanged("SntRiskA");
                    _SntRiskA = value;
                }
            }
        }

        [DbAlan("CekRiskB", SqlDbType.Decimal, 13)]
        public decimal CekRiskB
        {
            get { return _CekRiskB; }
            set
            {
                if (_CekRiskB != value)
                {
                    OnPropertyChanged("CekRiskB");
                    _CekRiskB = value;
                }
            }
        }

        [DbAlan("CekRiskA", SqlDbType.Decimal, 13)]
        public decimal CekRiskA
        {
            get { return _CekRiskA; }
            set
            {
                if (_CekRiskA != value)
                {
                    OnPropertyChanged("CekRiskA");
                    _CekRiskA = value;
                }
            }
        }

        [DbAlan("DvrTeminat1B", SqlDbType.Decimal, 13)]
        public decimal DvrTeminat1B
        {
            get { return _DvrTeminat1B; }
            set
            {
                if (_DvrTeminat1B != value)
                {
                    OnPropertyChanged("DvrTeminat1B");
                    _DvrTeminat1B = value;
                }
            }
        }

        [DbAlan("DvrTeminat1A", SqlDbType.Decimal, 13)]
        public decimal DvrTeminat1A
        {
            get { return _DvrTeminat1A; }
            set
            {
                if (_DvrTeminat1A != value)
                {
                    OnPropertyChanged("DvrTeminat1A");
                    _DvrTeminat1A = value;
                }
            }
        }

        [DbAlan("Teminat1B", SqlDbType.Decimal, 13)]
        public decimal Teminat1B
        {
            get { return _Teminat1B; }
            set
            {
                if (_Teminat1B != value)
                {
                    OnPropertyChanged("Teminat1B");
                    _Teminat1B = value;
                }
            }
        }

        [DbAlan("Teminat1A", SqlDbType.Decimal, 13)]
        public decimal Teminat1A
        {
            get { return _Teminat1A; }
            set
            {
                if (_Teminat1A != value)
                {
                    OnPropertyChanged("Teminat1A");
                    _Teminat1A = value;
                }
            }
        }

        [DbAlan("DvrTeminat2B", SqlDbType.Decimal, 13)]
        public decimal DvrTeminat2B
        {
            get { return _DvrTeminat2B; }
            set
            {
                if (_DvrTeminat2B != value)
                {
                    OnPropertyChanged("DvrTeminat2B");
                    _DvrTeminat2B = value;
                }
            }
        }

        [DbAlan("DvrTeminat2A", SqlDbType.Decimal, 13)]
        public decimal DvrTeminat2A
        {
            get { return _DvrTeminat2A; }
            set
            {
                if (_DvrTeminat2A != value)
                {
                    OnPropertyChanged("DvrTeminat2A");
                    _DvrTeminat2A = value;
                }
            }
        }

        [DbAlan("Teminat2B", SqlDbType.Decimal, 13)]
        public decimal Teminat2B
        {
            get { return _Teminat2B; }
            set
            {
                if (_Teminat2B != value)
                {
                    OnPropertyChanged("Teminat2B");
                    _Teminat2B = value;
                }
            }
        }

        [DbAlan("Teminat2A", SqlDbType.Decimal, 13)]
        public decimal Teminat2A
        {
            get { return _Teminat2A; }
            set
            {
                if (_Teminat2A != value)
                {
                    OnPropertyChanged("Teminat2A");
                    _Teminat2A = value;
                }
            }
        }

        [DbAlan("DvrB", SqlDbType.Decimal, 13)]
        public decimal DvrB
        {
            get { return _DvrB; }
            set
            {
                if (_DvrB != value)
                {
                    OnPropertyChanged("DvrB");
                    _DvrB = value;
                }
            }
        }

        [DbAlan("DvrA", SqlDbType.Decimal, 13)]
        public decimal DvrA
        {
            get { return _DvrA; }
            set
            {
                if (_DvrA != value)
                {
                    OnPropertyChanged("DvrA");
                    _DvrA = value;
                }
            }
        }

        [DbAlan("OdemeB", SqlDbType.Decimal, 13)]
        public decimal OdemeB
        {
            get { return _OdemeB; }
            set
            {
                if (_OdemeB != value)
                {
                    OnPropertyChanged("OdemeB");
                    _OdemeB = value;
                }
            }
        }

        [DbAlan("OdemeA", SqlDbType.Decimal, 13)]
        public decimal OdemeA
        {
            get { return _OdemeA; }
            set
            {
                if (_OdemeA != value)
                {
                    OnPropertyChanged("OdemeA");
                    _OdemeA = value;
                }
            }
        }

        [DbAlan("CiroB", SqlDbType.Decimal, 13)]
        public decimal CiroB
        {
            get { return _CiroB; }
            set
            {
                if (_CiroB != value)
                {
                    OnPropertyChanged("CiroB");
                    _CiroB = value;
                }
            }
        }

        [DbAlan("CiroA", SqlDbType.Decimal, 13)]
        public decimal CiroA
        {
            get { return _CiroA; }
            set
            {
                if (_CiroA != value)
                {
                    OnPropertyChanged("CiroA");
                    _CiroA = value;
                }
            }
        }

        [DbAlan("IadeFatB", SqlDbType.Decimal, 13)]
        public decimal IadeFatB
        {
            get { return _IadeFatB; }
            set
            {
                if (_IadeFatB != value)
                {
                    OnPropertyChanged("IadeFatB");
                    _IadeFatB = value;
                }
            }
        }

        [DbAlan("IadeFatA", SqlDbType.Decimal, 13)]
        public decimal IadeFatA
        {
            get { return _IadeFatA; }
            set
            {
                if (_IadeFatA != value)
                {
                    OnPropertyChanged("IadeFatA");
                    _IadeFatA = value;
                }
            }
        }

        [DbAlan("KDVB", SqlDbType.Decimal, 13)]
        public decimal KDVB
        {
            get { return _KDVB; }
            set
            {
                if (_KDVB != value)
                {
                    OnPropertyChanged("KDVB");
                    _KDVB = value;
                }
            }
        }

        [DbAlan("KDVA", SqlDbType.Decimal, 13)]
        public decimal KDVA
        {
            get { return _KDVA; }
            set
            {
                if (_KDVA != value)
                {
                    OnPropertyChanged("KDVA");
                    _KDVA = value;
                }
            }
        }

        [DbAlan("DigerB", SqlDbType.Decimal, 13)]
        public decimal DigerB
        {
            get { return _DigerB; }
            set
            {
                if (_DigerB != value)
                {
                    OnPropertyChanged("DigerB");
                    _DigerB = value;
                }
            }
        }

        [DbAlan("DigerA", SqlDbType.Decimal, 13)]
        public decimal DigerA
        {
            get { return _DigerA; }
            set
            {
                if (_DigerA != value)
                {
                    OnPropertyChanged("DigerA");
                    _DigerA = value;
                }
            }
        }

        [DbAlan("DvrProtSnt", SqlDbType.Decimal, 13)]
        public decimal DvrProtSnt
        {
            get { return _DvrProtSnt; }
            set
            {
                if (_DvrProtSnt != value)
                {
                    OnPropertyChanged("DvrProtSnt");
                    _DvrProtSnt = value;
                }
            }
        }

        [DbAlan("ProtSnt", SqlDbType.Decimal, 13)]
        public decimal ProtSnt
        {
            get { return _ProtSnt; }
            set
            {
                if (_ProtSnt != value)
                {
                    OnPropertyChanged("ProtSnt");
                    _ProtSnt = value;
                }
            }
        }

        [DbAlan("DvrKarsCek", SqlDbType.Decimal, 13)]
        public decimal DvrKarsCek
        {
            get { return _DvrKarsCek; }
            set
            {
                if (_DvrKarsCek != value)
                {
                    OnPropertyChanged("DvrKarsCek");
                    _DvrKarsCek = value;
                }
            }
        }

        [DbAlan("KarsCek", SqlDbType.Decimal, 13)]
        public decimal KarsCek
        {
            get { return _KarsCek; }
            set
            {
                if (_KarsCek != value)
                {
                    OnPropertyChanged("KarsCek");
                    _KarsCek = value;
                }
            }
        }

        [DbAlan("KrediLimiti", SqlDbType.Decimal, 13)]
        public decimal KrediLimiti
        {
            get { return _KrediLimiti; }
            set
            {
                if (_KrediLimiti != value)
                {
                    OnPropertyChanged("KrediLimiti");
                    _KrediLimiti = value;
                }
            }
        }

        [DbAlan("DvzKrediLimiti", SqlDbType.Decimal, 13)]
        public decimal DvzKrediLimiti
        {
            get { return _DvzKrediLimiti; }
            set
            {
                if (_DvzKrediLimiti != value)
                {
                    OnPropertyChanged("DvzKrediLimiti");
                    _DvzKrediLimiti = value;
                }
            }
        }

        [DbAlan("MtbkBak", SqlDbType.Decimal, 13)]
        public decimal MtbkBak
        {
            get { return _MtbkBak; }
            set
            {
                if (_MtbkBak != value)
                {
                    OnPropertyChanged("MtbkBak");
                    _MtbkBak = value;
                }
            }
        }

        [DbAlan("DvzMtbkBak", SqlDbType.Decimal, 13)]
        public decimal DvzMtbkBak
        {
            get { return _DvzMtbkBak; }
            set
            {
                if (_DvzMtbkBak != value)
                {
                    OnPropertyChanged("DvzMtbkBak");
                    _DvzMtbkBak = value;
                }
            }
        }

        [DbAlan("DvzDvrB", SqlDbType.Decimal, 13)]
        public decimal DvzDvrB
        {
            get { return _DvzDvrB; }
            set
            {
                if (_DvzDvrB != value)
                {
                    OnPropertyChanged("DvzDvrB");
                    _DvzDvrB = value;
                }
            }
        }

        [DbAlan("DvzDvrA", SqlDbType.Decimal, 13)]
        public decimal DvzDvrA
        {
            get { return _DvzDvrA; }
            set
            {
                if (_DvzDvrA != value)
                {
                    OnPropertyChanged("DvzDvrA");
                    _DvzDvrA = value;
                }
            }
        }

        [DbAlan("DvzDigerB", SqlDbType.Decimal, 13)]
        public decimal DvzDigerB
        {
            get { return _DvzDigerB; }
            set
            {
                if (_DvzDigerB != value)
                {
                    OnPropertyChanged("DvzDigerB");
                    _DvzDigerB = value;
                }
            }
        }

        [DbAlan("DvzDigerA", SqlDbType.Decimal, 13)]
        public decimal DvzDigerA
        {
            get { return _DvzDigerA; }
            set
            {
                if (_DvzDigerA != value)
                {
                    OnPropertyChanged("DvzDigerA");
                    _DvzDigerA = value;
                }
            }
        }

        [DbAlan("DvzOdemeB", SqlDbType.Decimal, 13)]
        public decimal DvzOdemeB
        {
            get { return _DvzOdemeB; }
            set
            {
                if (_DvzOdemeB != value)
                {
                    OnPropertyChanged("DvzOdemeB");
                    _DvzOdemeB = value;
                }
            }
        }

        [DbAlan("DvzOdemeA", SqlDbType.Decimal, 13)]
        public decimal DvzOdemeA
        {
            get { return _DvzOdemeA; }
            set
            {
                if (_DvzOdemeA != value)
                {
                    OnPropertyChanged("DvzOdemeA");
                    _DvzOdemeA = value;
                }
            }
        }

        [DbAlan("DvzCiroB", SqlDbType.Decimal, 13)]
        public decimal DvzCiroB
        {
            get { return _DvzCiroB; }
            set
            {
                if (_DvzCiroB != value)
                {
                    OnPropertyChanged("DvzCiroB");
                    _DvzCiroB = value;
                }
            }
        }

        [DbAlan("DvzCiroA", SqlDbType.Decimal, 13)]
        public decimal DvzCiroA
        {
            get { return _DvzCiroA; }
            set
            {
                if (_DvzCiroA != value)
                {
                    OnPropertyChanged("DvzCiroA");
                    _DvzCiroA = value;
                }
            }
        }

        [DbAlan("DvzIadeB", SqlDbType.Decimal, 13)]
        public decimal DvzIadeB
        {
            get { return _DvzIadeB; }
            set
            {
                if (_DvzIadeB != value)
                {
                    OnPropertyChanged("DvzIadeB");
                    _DvzIadeB = value;
                }
            }
        }

        [DbAlan("DvzIadeA", SqlDbType.Decimal, 13)]
        public decimal DvzIadeA
        {
            get { return _DvzIadeA; }
            set
            {
                if (_DvzIadeA != value)
                {
                    OnPropertyChanged("DvzIadeA");
                    _DvzIadeA = value;
                }
            }
        }

        [DbAlan("DvzKDVB", SqlDbType.Decimal, 13)]
        public decimal DvzKDVB
        {
            get { return _DvzKDVB; }
            set
            {
                if (_DvzKDVB != value)
                {
                    OnPropertyChanged("DvzKDVB");
                    _DvzKDVB = value;
                }
            }
        }

        [DbAlan("DvzKDVA", SqlDbType.Decimal, 13)]
        public decimal DvzKDVA
        {
            get { return _DvzKDVA; }
            set
            {
                if (_DvzKDVA != value)
                {
                    OnPropertyChanged("DvzKDVA");
                    _DvzKDVA = value;
                }
            }
        }

        [DbAlan("DvrDvzSntRiskB", SqlDbType.Decimal, 13)]
        public decimal DvrDvzSntRiskB
        {
            get { return _DvrDvzSntRiskB; }
            set
            {
                if (_DvrDvzSntRiskB != value)
                {
                    OnPropertyChanged("DvrDvzSntRiskB");
                    _DvrDvzSntRiskB = value;
                }
            }
        }

        [DbAlan("DvrDvzSntRiskA", SqlDbType.Decimal, 13)]
        public decimal DvrDvzSntRiskA
        {
            get { return _DvrDvzSntRiskA; }
            set
            {
                if (_DvrDvzSntRiskA != value)
                {
                    OnPropertyChanged("DvrDvzSntRiskA");
                    _DvrDvzSntRiskA = value;
                }
            }
        }

        [DbAlan("DvrDvzCekRiskB", SqlDbType.Decimal, 13)]
        public decimal DvrDvzCekRiskB
        {
            get { return _DvrDvzCekRiskB; }
            set
            {
                if (_DvrDvzCekRiskB != value)
                {
                    OnPropertyChanged("DvrDvzCekRiskB");
                    _DvrDvzCekRiskB = value;
                }
            }
        }

        [DbAlan("DvrDvzCekRiskA", SqlDbType.Decimal, 13)]
        public decimal DvrDvzCekRiskA
        {
            get { return _DvrDvzCekRiskA; }
            set
            {
                if (_DvrDvzCekRiskA != value)
                {
                    OnPropertyChanged("DvrDvzCekRiskA");
                    _DvrDvzCekRiskA = value;
                }
            }
        }

        [DbAlan("DvrDvzTeminatB", SqlDbType.Decimal, 13)]
        public decimal DvrDvzTeminatB
        {
            get { return _DvrDvzTeminatB; }
            set
            {
                if (_DvrDvzTeminatB != value)
                {
                    OnPropertyChanged("DvrDvzTeminatB");
                    _DvrDvzTeminatB = value;
                }
            }
        }

        [DbAlan("DvrDvzTeminatA", SqlDbType.Decimal, 13)]
        public decimal DvrDvzTeminatA
        {
            get { return _DvrDvzTeminatA; }
            set
            {
                if (_DvrDvzTeminatA != value)
                {
                    OnPropertyChanged("DvrDvzTeminatA");
                    _DvrDvzTeminatA = value;
                }
            }
        }

        [DbAlan("DvrDvzTeminat2B", SqlDbType.Decimal, 13)]
        public decimal DvrDvzTeminat2B
        {
            get { return _DvrDvzTeminat2B; }
            set
            {
                if (_DvrDvzTeminat2B != value)
                {
                    OnPropertyChanged("DvrDvzTeminat2B");
                    _DvrDvzTeminat2B = value;
                }
            }
        }

        [DbAlan("DvrDvzTeminat2A", SqlDbType.Decimal, 13)]
        public decimal DvrDvzTeminat2A
        {
            get { return _DvrDvzTeminat2A; }
            set
            {
                if (_DvrDvzTeminat2A != value)
                {
                    OnPropertyChanged("DvrDvzTeminat2A");
                    _DvrDvzTeminat2A = value;
                }
            }
        }

        [DbAlan("DvzTeminatB", SqlDbType.Decimal, 13)]
        public decimal DvzTeminatB
        {
            get { return _DvzTeminatB; }
            set
            {
                if (_DvzTeminatB != value)
                {
                    OnPropertyChanged("DvzTeminatB");
                    _DvzTeminatB = value;
                }
            }
        }

        [DbAlan("DvzTeminatA", SqlDbType.Decimal, 13)]
        public decimal DvzTeminatA
        {
            get { return _DvzTeminatA; }
            set
            {
                if (_DvzTeminatA != value)
                {
                    OnPropertyChanged("DvzTeminatA");
                    _DvzTeminatA = value;
                }
            }
        }

        [DbAlan("DvzTeminat2B", SqlDbType.Decimal, 13)]
        public decimal DvzTeminat2B
        {
            get { return _DvzTeminat2B; }
            set
            {
                if (_DvzTeminat2B != value)
                {
                    OnPropertyChanged("DvzTeminat2B");
                    _DvzTeminat2B = value;
                }
            }
        }

        [DbAlan("DvzTeminat2A", SqlDbType.Decimal, 13)]
        public decimal DvzTeminat2A
        {
            get { return _DvzTeminat2A; }
            set
            {
                if (_DvzTeminat2A != value)
                {
                    OnPropertyChanged("DvzTeminat2A");
                    _DvzTeminat2A = value;
                }
            }
        }

        [DbAlan("DvzSntRiskB", SqlDbType.Decimal, 13)]
        public decimal DvzSntRiskB
        {
            get { return _DvzSntRiskB; }
            set
            {
                if (_DvzSntRiskB != value)
                {
                    OnPropertyChanged("DvzSntRiskB");
                    _DvzSntRiskB = value;
                }
            }
        }

        [DbAlan("DvzSntRiskiA", SqlDbType.Decimal, 13)]
        public decimal DvzSntRiskiA
        {
            get { return _DvzSntRiskiA; }
            set
            {
                if (_DvzSntRiskiA != value)
                {
                    OnPropertyChanged("DvzSntRiskiA");
                    _DvzSntRiskiA = value;
                }
            }
        }

        [DbAlan("DvzCekRiskB", SqlDbType.Decimal, 13)]
        public decimal DvzCekRiskB
        {
            get { return _DvzCekRiskB; }
            set
            {
                if (_DvzCekRiskB != value)
                {
                    OnPropertyChanged("DvzCekRiskB");
                    _DvzCekRiskB = value;
                }
            }
        }

        [DbAlan("DvzCekRiskA", SqlDbType.Decimal, 13)]
        public decimal DvzCekRiskA
        {
            get { return _DvzCekRiskA; }
            set
            {
                if (_DvzCekRiskA != value)
                {
                    OnPropertyChanged("DvzCekRiskA");
                    _DvzCekRiskA = value;
                }
            }
        }

        [DbAlan("DvzProtSnt", SqlDbType.Decimal, 13)]
        public decimal DvzProtSnt
        {
            get { return _DvzProtSnt; }
            set
            {
                if (_DvzProtSnt != value)
                {
                    OnPropertyChanged("DvzProtSnt");
                    _DvzProtSnt = value;
                }
            }
        }

        [DbAlan("DvrDvzProtSenet", SqlDbType.Decimal, 13)]
        public decimal DvrDvzProtSenet
        {
            get { return _DvrDvzProtSenet; }
            set
            {
                if (_DvrDvzProtSenet != value)
                {
                    OnPropertyChanged("DvrDvzProtSenet");
                    _DvrDvzProtSenet = value;
                }
            }
        }

        [DbAlan("DvzKarsCek", SqlDbType.Decimal, 13)]
        public decimal DvzKarsCek
        {
            get { return _DvzKarsCek; }
            set
            {
                if (_DvzKarsCek != value)
                {
                    OnPropertyChanged("DvzKarsCek");
                    _DvzKarsCek = value;
                }
            }
        }

        [DbAlan("DvrDvzKarsCek", SqlDbType.Decimal, 13)]
        public decimal DvrDvzKarsCek
        {
            get { return _DvrDvzKarsCek; }
            set
            {
                if (_DvrDvzKarsCek != value)
                {
                    OnPropertyChanged("DvrDvzKarsCek");
                    _DvrDvzKarsCek = value;
                }
            }
        }

        [DbAlan("YasBakiye", SqlDbType.Decimal, 13)]
        public decimal YasBakiye
        {
            get { return _YasBakiye; }
            set
            {
                if (_YasBakiye != value)
                {
                    OnPropertyChanged("YasBakiye");
                    _YasBakiye = value;
                }
            }
        }

        [DbAlan("OrtalamaTarih", SqlDbType.Int, 4)]
        public int OrtalamaTarih
        {
            get { return _OrtalamaTarih; }
            set
            {
                if (_OrtalamaTarih != value)
                {
                    OnPropertyChanged("OrtalamaTarih");
                    _OrtalamaTarih = value;
                }
            }
        }

        [DbAlan("OrtalamaGun", SqlDbType.SmallInt, 2)]
        public short OrtalamaGun
        {
            get { return _OrtalamaGun; }
            set
            {
                if (_OrtalamaGun != value)
                {
                    OnPropertyChanged("OrtalamaGun");
                    _OrtalamaGun = value;
                }
            }
        }

        [DbAlan("OtvBorc", SqlDbType.Decimal, 13)]
        public decimal OtvBorc
        {
            get { return _OtvBorc; }
            set
            {
                if (_OtvBorc != value)
                {
                    OnPropertyChanged("OtvBorc");
                    _OtvBorc = value;
                }
            }
        }

        [DbAlan("OtvAlacak", SqlDbType.Decimal, 13)]
        public decimal OtvAlacak
        {
            get { return _OtvAlacak; }
            set
            {
                if (_OtvAlacak != value)
                {
                    OnPropertyChanged("OtvAlacak");
                    _OtvAlacak = value;
                }
            }
        }

        [DbAlan("OtvDvzBorc", SqlDbType.Decimal, 13)]
        public decimal OtvDvzBorc
        {
            get { return _OtvDvzBorc; }
            set
            {
                if (_OtvDvzBorc != value)
                {
                    OnPropertyChanged("OtvDvzBorc");
                    _OtvDvzBorc = value;
                }
            }
        }

        [DbAlan("OtvDvzAlacak", SqlDbType.Decimal, 13)]
        public decimal OtvDvzAlacak
        {
            get { return _OtvDvzAlacak; }
            set
            {
                if (_OtvDvzAlacak != value)
                {
                    OnPropertyChanged("OtvDvzAlacak");
                    _OtvDvzAlacak = value;
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

        [DbAlan("Notlar", SqlDbType.VarChar, 50)]
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

        [DbAlan("UygFiyatListeNo", SqlDbType.VarChar, 16)]
        public string UygFiyatListeNo
        {
            get { return _UygFiyatListeNo; }
            set
            {
                if (_UygFiyatListeNo != value)
                {
                    OnPropertyChanged("UygFiyatListeNo");
                    _UygFiyatListeNo = value;
                }
            }
        }

        [DbAlan("FatIrsDenDoganBorc", SqlDbType.Decimal, 13)]
        public decimal FatIrsDenDoganBorc
        {
            get { return _FatIrsDenDoganBorc; }
            set
            {
                if (_FatIrsDenDoganBorc != value)
                {
                    OnPropertyChanged("FatIrsDenDoganBorc");
                    _FatIrsDenDoganBorc = value;
                }
            }
        }

        [DbAlan("FatIrsDenDoganAlacak", SqlDbType.Decimal, 13)]
        public decimal FatIrsDenDoganAlacak
        {
            get { return _FatIrsDenDoganAlacak; }
            set
            {
                if (_FatIrsDenDoganAlacak != value)
                {
                    OnPropertyChanged("FatIrsDenDoganAlacak");
                    _FatIrsDenDoganAlacak = value;
                }
            }
        }

        [DbAlan("BekleyenSatisSip", SqlDbType.Decimal, 13)]
        public decimal BekleyenSatisSip
        {
            get { return _BekleyenSatisSip; }
            set
            {
                if (_BekleyenSatisSip != value)
                {
                    OnPropertyChanged("BekleyenSatisSip");
                    _BekleyenSatisSip = value;
                }
            }
        }

        [DbAlan("BekleyenAlimSip", SqlDbType.Decimal, 13)]
        public decimal BekleyenAlimSip
        {
            get { return _BekleyenAlimSip; }
            set
            {
                if (_BekleyenAlimSip != value)
                {
                    OnPropertyChanged("BekleyenAlimSip");
                    _BekleyenAlimSip = value;
                }
            }
        }

        [DbAlan("FatIrsdenDoganDovizBorc", SqlDbType.Decimal, 13)]
        public decimal FatIrsdenDoganDovizBorc
        {
            get { return _FatIrsdenDoganDovizBorc; }
            set
            {
                if (_FatIrsdenDoganDovizBorc != value)
                {
                    OnPropertyChanged("FatIrsdenDoganDovizBorc");
                    _FatIrsdenDoganDovizBorc = value;
                }
            }
        }

        [DbAlan("FatIrsdenDoganDovizAlacak", SqlDbType.Decimal, 13)]
        public decimal FatIrsdenDoganDovizAlacak
        {
            get { return _FatIrsdenDoganDovizAlacak; }
            set
            {
                if (_FatIrsdenDoganDovizAlacak != value)
                {
                    OnPropertyChanged("FatIrsdenDoganDovizAlacak");
                    _FatIrsdenDoganDovizAlacak = value;
                }
            }
        }

        [DbAlan("BekleyenSatSipDoviz", SqlDbType.Decimal, 13)]
        public decimal BekleyenSatSipDoviz
        {
            get { return _BekleyenSatSipDoviz; }
            set
            {
                if (_BekleyenSatSipDoviz != value)
                {
                    OnPropertyChanged("BekleyenSatSipDoviz");
                    _BekleyenSatSipDoviz = value;
                }
            }
        }

        [DbAlan("BekleyenAlimSipDoviz", SqlDbType.Decimal, 13)]
        public decimal BekleyenAlimSipDoviz
        {
            get { return _BekleyenAlimSipDoviz; }
            set
            {
                if (_BekleyenAlimSipDoviz != value)
                {
                    OnPropertyChanged("BekleyenAlimSipDoviz");
                    _BekleyenAlimSipDoviz = value;
                }
            }
        }

        [DbAlan("YetkiliEmail1", SqlDbType.VarChar, 50)]
        public string YetkiliEmail1
        {
            get { return _YetkiliEmail1; }
            set
            {
                if (_YetkiliEmail1 != value)
                {
                    OnPropertyChanged("YetkiliEmail1");
                    _YetkiliEmail1 = value;
                }
            }
        }

        [DbAlan("YetkiliEmail2", SqlDbType.VarChar, 50)]
        public string YetkiliEmail2
        {
            get { return _YetkiliEmail2; }
            set
            {
                if (_YetkiliEmail2 != value)
                {
                    OnPropertyChanged("YetkiliEmail2");
                    _YetkiliEmail2 = value;
                }
            }
        }

        [DbAlan("HavaleEdilecekBanka", SqlDbType.VarChar, 4)]
        public string HavaleEdilecekBanka
        {
            get { return _HavaleEdilecekBanka; }
            set
            {
                if (_HavaleEdilecekBanka != value)
                {
                    OnPropertyChanged("HavaleEdilecekBanka");
                    _HavaleEdilecekBanka = value;
                }
            }
        }

        [DbAlan("HavaleEdilecekBankaSubesi", SqlDbType.VarChar, 5)]
        public string HavaleEdilecekBankaSubesi
        {
            get { return _HavaleEdilecekBankaSubesi; }
            set
            {
                if (_HavaleEdilecekBankaSubesi != value)
                {
                    OnPropertyChanged("HavaleEdilecekBankaSubesi");
                    _HavaleEdilecekBankaSubesi = value;
                }
            }
        }

        [DbAlan("HavaleEdilecekHesapNo", SqlDbType.VarChar, 20)]
        public string HavaleEdilecekHesapNo
        {
            get { return _HavaleEdilecekHesapNo; }
            set
            {
                if (_HavaleEdilecekHesapNo != value)
                {
                    OnPropertyChanged("HavaleEdilecekHesapNo");
                    _HavaleEdilecekHesapNo = value;
                }
            }
        }

        [DbAlan("DovizIslemBanka", SqlDbType.VarChar, 4)]
        public string DovizIslemBanka
        {
            get { return _DovizIslemBanka; }
            set
            {
                if (_DovizIslemBanka != value)
                {
                    OnPropertyChanged("DovizIslemBanka");
                    _DovizIslemBanka = value;
                }
            }
        }

        [DbAlan("DovizIslemKurCins", SqlDbType.SmallInt, 2)]
        public short DovizIslemKurCins
        {
            get { return _DovizIslemKurCins; }
            set
            {
                if (_DovizIslemKurCins != value)
                {
                    OnPropertyChanged("DovizIslemKurCins");
                    _DovizIslemKurCins = value;
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

        [DbAlan("FaturaChk", SqlDbType.VarChar, 20)]
        public string FaturaChk
        {
            get { return _FaturaChk; }
            set
            {
                if (_FaturaChk != value)
                {
                    OnPropertyChanged("FaturaChk");
                    _FaturaChk = value;
                }
            }
        }

        [DbAlan("FaturaAdres1Sehir", SqlDbType.VarChar, 40)]
        public string FaturaAdres1Sehir
        {
            get { return _FaturaAdres1Sehir; }
            set
            {
                if (_FaturaAdres1Sehir != value)
                {
                    OnPropertyChanged("FaturaAdres1Sehir");
                    _FaturaAdres1Sehir = value;
                }
            }
        }

        [DbAlan("FaturaAdres1Ulke", SqlDbType.VarChar, 20)]
        public string FaturaAdres1Ulke
        {
            get { return _FaturaAdres1Ulke; }
            set
            {
                if (_FaturaAdres1Ulke != value)
                {
                    OnPropertyChanged("FaturaAdres1Ulke");
                    _FaturaAdres1Ulke = value;
                }
            }
        }

        [DbAlan("FaturaAdres1PKod", SqlDbType.VarChar, 7)]
        public string FaturaAdres1PKod
        {
            get { return _FaturaAdres1PKod; }
            set
            {
                if (_FaturaAdres1PKod != value)
                {
                    OnPropertyChanged("FaturaAdres1PKod");
                    _FaturaAdres1PKod = value;
                }
            }
        }

        [DbAlan("TeslimAdres1Sehir", SqlDbType.VarChar, 40)]
        public string TeslimAdres1Sehir
        {
            get { return _TeslimAdres1Sehir; }
            set
            {
                if (_TeslimAdres1Sehir != value)
                {
                    OnPropertyChanged("TeslimAdres1Sehir");
                    _TeslimAdres1Sehir = value;
                }
            }
        }

        [DbAlan("TeslimAdres1Ulke", SqlDbType.VarChar, 20)]
        public string TeslimAdres1Ulke
        {
            get { return _TeslimAdres1Ulke; }
            set
            {
                if (_TeslimAdres1Ulke != value)
                {
                    OnPropertyChanged("TeslimAdres1Ulke");
                    _TeslimAdres1Ulke = value;
                }
            }
        }

        [DbAlan("TeslimAdres1PKod", SqlDbType.VarChar, 7)]
        public string TeslimAdres1PKod
        {
            get { return _TeslimAdres1PKod; }
            set
            {
                if (_TeslimAdres1PKod != value)
                {
                    OnPropertyChanged("TeslimAdres1PKod");
                    _TeslimAdres1PKod = value;
                }
            }
        }

        [DbAlan("Telefon1Dahili", SqlDbType.VarChar, 4)]
        public string Telefon1Dahili
        {
            get { return _Telefon1Dahili; }
            set
            {
                if (_Telefon1Dahili != value)
                {
                    OnPropertyChanged("Telefon1Dahili");
                    _Telefon1Dahili = value;
                }
            }
        }

        [DbAlan("Telefon1BolgeKod", SqlDbType.VarChar, 4)]
        public string Telefon1BolgeKod
        {
            get { return _Telefon1BolgeKod; }
            set
            {
                if (_Telefon1BolgeKod != value)
                {
                    OnPropertyChanged("Telefon1BolgeKod");
                    _Telefon1BolgeKod = value;
                }
            }
        }

        [DbAlan("Telefon1UlkeKod", SqlDbType.VarChar, 4)]
        public string Telefon1UlkeKod
        {
            get { return _Telefon1UlkeKod; }
            set
            {
                if (_Telefon1UlkeKod != value)
                {
                    OnPropertyChanged("Telefon1UlkeKod");
                    _Telefon1UlkeKod = value;
                }
            }
        }

        [DbAlan("Telefon2Dahili", SqlDbType.VarChar, 4)]
        public string Telefon2Dahili
        {
            get { return _Telefon2Dahili; }
            set
            {
                if (_Telefon2Dahili != value)
                {
                    OnPropertyChanged("Telefon2Dahili");
                    _Telefon2Dahili = value;
                }
            }
        }

        [DbAlan("Telefon2BolgeKod", SqlDbType.VarChar, 4)]
        public string Telefon2BolgeKod
        {
            get { return _Telefon2BolgeKod; }
            set
            {
                if (_Telefon2BolgeKod != value)
                {
                    OnPropertyChanged("Telefon2BolgeKod");
                    _Telefon2BolgeKod = value;
                }
            }
        }

        [DbAlan("Telefon2UlkeKod", SqlDbType.VarChar, 4)]
        public string Telefon2UlkeKod
        {
            get { return _Telefon2UlkeKod; }
            set
            {
                if (_Telefon2UlkeKod != value)
                {
                    OnPropertyChanged("Telefon2UlkeKod");
                    _Telefon2UlkeKod = value;
                }
            }
        }

        [DbAlan("Telefon3Dahili", SqlDbType.VarChar, 4)]
        public string Telefon3Dahili
        {
            get { return _Telefon3Dahili; }
            set
            {
                if (_Telefon3Dahili != value)
                {
                    OnPropertyChanged("Telefon3Dahili");
                    _Telefon3Dahili = value;
                }
            }
        }

        [DbAlan("Telefon3BolgeKod", SqlDbType.VarChar, 4)]
        public string Telefon3BolgeKod
        {
            get { return _Telefon3BolgeKod; }
            set
            {
                if (_Telefon3BolgeKod != value)
                {
                    OnPropertyChanged("Telefon3BolgeKod");
                    _Telefon3BolgeKod = value;
                }
            }
        }

        [DbAlan("Telefon3UlkeKod", SqlDbType.VarChar, 4)]
        public string Telefon3UlkeKod
        {
            get { return _Telefon3UlkeKod; }
            set
            {
                if (_Telefon3UlkeKod != value)
                {
                    OnPropertyChanged("Telefon3UlkeKod");
                    _Telefon3UlkeKod = value;
                }
            }
        }

        [DbAlan("Telefon4Dahili", SqlDbType.VarChar, 4)]
        public string Telefon4Dahili
        {
            get { return _Telefon4Dahili; }
            set
            {
                if (_Telefon4Dahili != value)
                {
                    OnPropertyChanged("Telefon4Dahili");
                    _Telefon4Dahili = value;
                }
            }
        }

        [DbAlan("Telefon4BolgeKod", SqlDbType.VarChar, 4)]
        public string Telefon4BolgeKod
        {
            get { return _Telefon4BolgeKod; }
            set
            {
                if (_Telefon4BolgeKod != value)
                {
                    OnPropertyChanged("Telefon4BolgeKod");
                    _Telefon4BolgeKod = value;
                }
            }
        }

        [DbAlan("Telefon4UlkeKod", SqlDbType.VarChar, 4)]
        public string Telefon4UlkeKod
        {
            get { return _Telefon4UlkeKod; }
            set
            {
                if (_Telefon4UlkeKod != value)
                {
                    OnPropertyChanged("Telefon4UlkeKod");
                    _Telefon4UlkeKod = value;
                }
            }
        }

        [DbAlan("Fax1BolgeKod", SqlDbType.VarChar, 4)]
        public string Fax1BolgeKod
        {
            get { return _Fax1BolgeKod; }
            set
            {
                if (_Fax1BolgeKod != value)
                {
                    OnPropertyChanged("Fax1BolgeKod");
                    _Fax1BolgeKod = value;
                }
            }
        }

        [DbAlan("Fax1UlkeKodu", SqlDbType.VarChar, 4)]
        public string Fax1UlkeKodu
        {
            get { return _Fax1UlkeKodu; }
            set
            {
                if (_Fax1UlkeKodu != value)
                {
                    OnPropertyChanged("Fax1UlkeKodu");
                    _Fax1UlkeKodu = value;
                }
            }
        }

        [DbAlan("Fax2BolgeKod", SqlDbType.VarChar, 4)]
        public string Fax2BolgeKod
        {
            get { return _Fax2BolgeKod; }
            set
            {
                if (_Fax2BolgeKod != value)
                {
                    OnPropertyChanged("Fax2BolgeKod");
                    _Fax2BolgeKod = value;
                }
            }
        }

        [DbAlan("Fax2UlkeKodu", SqlDbType.VarChar, 4)]
        public string Fax2UlkeKodu
        {
            get { return _Fax2UlkeKodu; }
            set
            {
                if (_Fax2UlkeKodu != value)
                {
                    OnPropertyChanged("Fax2UlkeKodu");
                    _Fax2UlkeKodu = value;
                }
            }
        }

        [DbAlan("BankaKod1", SqlDbType.VarChar, 3)]
        public string BankaKod1
        {
            get { return _BankaKod1; }
            set
            {
                if (_BankaKod1 != value)
                {
                    OnPropertyChanged("BankaKod1");
                    _BankaKod1 = value;
                }
            }
        }

        [DbAlan("BankaIlKod1", SqlDbType.VarChar, 3)]
        public string BankaIlKod1
        {
            get { return _BankaIlKod1; }
            set
            {
                if (_BankaIlKod1 != value)
                {
                    OnPropertyChanged("BankaIlKod1");
                    _BankaIlKod1 = value;
                }
            }
        }

        [DbAlan("BankaSubeKod1", SqlDbType.VarChar, 5)]
        public string BankaSubeKod1
        {
            get { return _BankaSubeKod1; }
            set
            {
                if (_BankaSubeKod1 != value)
                {
                    OnPropertyChanged("BankaSubeKod1");
                    _BankaSubeKod1 = value;
                }
            }
        }

        [DbAlan("BankaHesap1", SqlDbType.VarChar, 20)]
        public string BankaHesap1
        {
            get { return _BankaHesap1; }
            set
            {
                if (_BankaHesap1 != value)
                {
                    OnPropertyChanged("BankaHesap1");
                    _BankaHesap1 = value;
                }
            }
        }

        [DbAlan("BankaKod2", SqlDbType.VarChar, 3)]
        public string BankaKod2
        {
            get { return _BankaKod2; }
            set
            {
                if (_BankaKod2 != value)
                {
                    OnPropertyChanged("BankaKod2");
                    _BankaKod2 = value;
                }
            }
        }

        [DbAlan("BankaIlKod2", SqlDbType.VarChar, 3)]
        public string BankaIlKod2
        {
            get { return _BankaIlKod2; }
            set
            {
                if (_BankaIlKod2 != value)
                {
                    OnPropertyChanged("BankaIlKod2");
                    _BankaIlKod2 = value;
                }
            }
        }

        [DbAlan("BankaSubeKod2", SqlDbType.VarChar, 5)]
        public string BankaSubeKod2
        {
            get { return _BankaSubeKod2; }
            set
            {
                if (_BankaSubeKod2 != value)
                {
                    OnPropertyChanged("BankaSubeKod2");
                    _BankaSubeKod2 = value;
                }
            }
        }

        [DbAlan("BankaHesap2", SqlDbType.VarChar, 20)]
        public string BankaHesap2
        {
            get { return _BankaHesap2; }
            set
            {
                if (_BankaHesap2 != value)
                {
                    OnPropertyChanged("BankaHesap2");
                    _BankaHesap2 = value;
                }
            }
        }

        [DbAlan("BankaKod3", SqlDbType.VarChar, 3)]
        public string BankaKod3
        {
            get { return _BankaKod3; }
            set
            {
                if (_BankaKod3 != value)
                {
                    OnPropertyChanged("BankaKod3");
                    _BankaKod3 = value;
                }
            }
        }

        [DbAlan("BankaIlKod3", SqlDbType.VarChar, 3)]
        public string BankaIlKod3
        {
            get { return _BankaIlKod3; }
            set
            {
                if (_BankaIlKod3 != value)
                {
                    OnPropertyChanged("BankaIlKod3");
                    _BankaIlKod3 = value;
                }
            }
        }

        [DbAlan("BankaSubeKod3", SqlDbType.VarChar, 5)]
        public string BankaSubeKod3
        {
            get { return _BankaSubeKod3; }
            set
            {
                if (_BankaSubeKod3 != value)
                {
                    OnPropertyChanged("BankaSubeKod3");
                    _BankaSubeKod3 = value;
                }
            }
        }

        [DbAlan("BankaHesap3", SqlDbType.VarChar, 20)]
        public string BankaHesap3
        {
            get { return _BankaHesap3; }
            set
            {
                if (_BankaHesap3 != value)
                {
                    OnPropertyChanged("BankaHesap3");
                    _BankaHesap3 = value;
                }
            }
        }

        [DbAlan("BankaKod4", SqlDbType.VarChar, 3)]
        public string BankaKod4
        {
            get { return _BankaKod4; }
            set
            {
                if (_BankaKod4 != value)
                {
                    OnPropertyChanged("BankaKod4");
                    _BankaKod4 = value;
                }
            }
        }

        [DbAlan("BankaIlKod4", SqlDbType.VarChar, 3)]
        public string BankaIlKod4
        {
            get { return _BankaIlKod4; }
            set
            {
                if (_BankaIlKod4 != value)
                {
                    OnPropertyChanged("BankaIlKod4");
                    _BankaIlKod4 = value;
                }
            }
        }

        [DbAlan("BankaSubeKod4", SqlDbType.VarChar, 5)]
        public string BankaSubeKod4
        {
            get { return _BankaSubeKod4; }
            set
            {
                if (_BankaSubeKod4 != value)
                {
                    OnPropertyChanged("BankaSubeKod4");
                    _BankaSubeKod4 = value;
                }
            }
        }

        [DbAlan("BankaHesap4", SqlDbType.VarChar, 20)]
        public string BankaHesap4
        {
            get { return _BankaHesap4; }
            set
            {
                if (_BankaHesap4 != value)
                {
                    OnPropertyChanged("BankaHesap4");
                    _BankaHesap4 = value;
                }
            }
        }

        [DbAlan("SirketEMail", SqlDbType.VarChar, 50)]
        public string SirketEMail
        {
            get { return _SirketEMail; }
            set
            {
                if (_SirketEMail != value)
                {
                    OnPropertyChanged("SirketEMail");
                    _SirketEMail = value;
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

        [DbAlan("TeslimatSekli", SqlDbType.SmallInt, 2)]
        public short TeslimatSekli
        {
            get { return _TeslimatSekli; }
            set
            {
                if (_TeslimatSekli != value)
                {
                    OnPropertyChanged("TeslimatSekli");
                    _TeslimatSekli = value;
                }
            }
        }

        [DbAlan("SatisKurCinsi", SqlDbType.SmallInt, 2)]
        public short SatisKurCinsi
        {
            get { return _SatisKurCinsi; }
            set
            {
                if (_SatisKurCinsi != value)
                {
                    OnPropertyChanged("SatisKurCinsi");
                    _SatisKurCinsi = value;
                }
            }
        }

        [DbAlan("AlisKurCinsi", SqlDbType.SmallInt, 2)]
        public short AlisKurCinsi
        {
            get { return _AlisKurCinsi; }
            set
            {
                if (_AlisKurCinsi != value)
                {
                    OnPropertyChanged("AlisKurCinsi");
                    _AlisKurCinsi = value;
                }
            }
        }

        [DbAlan("FaturaMalAdi", SqlDbType.SmallInt, 2)]
        public short FaturaMalAdi
        {
            get { return _FaturaMalAdi; }
            set
            {
                if (_FaturaMalAdi != value)
                {
                    OnPropertyChanged("FaturaMalAdi");
                    _FaturaMalAdi = value;
                }
            }
        }

        /// <summary>
        /// 0-YerelPara, 1-Döviz, 2-YerelPara/Döviz
        /// </summary>
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

        [DbAlan("OdemePlani", SqlDbType.Int, 4)]
        public int OdemePlani
        {
            get { return _OdemePlani; }
            set
            {
                if (_OdemePlani != value)
                {
                    OnPropertyChanged("OdemePlani");
                    _OdemePlani = value;
                }
            }
        }

        [DbAlan("SatIslemEMail", SqlDbType.VarChar, 50)]
        public string SatIslemEMail
        {
            get { return _SatIslemEMail; }
            set
            {
                if (_SatIslemEMail != value)
                {
                    OnPropertyChanged("SatIslemEMail");
                    _SatIslemEMail = value;
                }
            }
        }

        [DbAlan("SatAlmaIslemEMail", SqlDbType.VarChar, 50)]
        public string SatAlmaIslemEMail
        {
            get { return _SatAlmaIslemEMail; }
            set
            {
                if (_SatAlmaIslemEMail != value)
                {
                    OnPropertyChanged("SatAlmaIslemEMail");
                    _SatAlmaIslemEMail = value;
                }
            }
        }

        [DbAlan("FinIslemEMail", SqlDbType.VarChar, 50)]
        public string FinIslemEMail
        {
            get { return _FinIslemEMail; }
            set
            {
                if (_FinIslemEMail != value)
                {
                    OnPropertyChanged("FinIslemEMail");
                    _FinIslemEMail = value;
                }
            }
        }

        [DbAlan("CHKodu", SqlDbType.VarChar, 20)]
        public string CHKodu
        {
            get { return _CHKodu; }
            set
            {
                if (_CHKodu != value)
                {
                    OnPropertyChanged("CHKodu");
                    _CHKodu = value;
                }
            }
        }

        [DbAlan("UlkeNumKod", SqlDbType.VarChar, 3)]
        public string UlkeNumKod
        {
            get { return _UlkeNumKod; }
            set
            {
                if (_UlkeNumKod != value)
                {
                    OnPropertyChanged("UlkeNumKod");
                    _UlkeNumKod = value;
                }
            }
        }

        [DbAlan("FormBaBsUnvan", SqlDbType.SmallInt, 2)]
        public short FormBaBsUnvan
        {
            get { return _FormBaBsUnvan; }
            set
            {
                if (_FormBaBsUnvan != value)
                {
                    OnPropertyChanged("FormBaBsUnvan");
                    _FormBaBsUnvan = value;
                }
            }
        }

        [DbAlan("OivBorc", SqlDbType.Decimal, 13)]
        public decimal OivBorc
        {
            get { return _OivBorc; }
            set
            {
                if (_OivBorc != value)
                {
                    OnPropertyChanged("OivBorc");
                    _OivBorc = value;
                }
            }
        }

        [DbAlan("OivAlacak", SqlDbType.Decimal, 13)]
        public decimal OivAlacak
        {
            get { return _OivAlacak; }
            set
            {
                if (_OivAlacak != value)
                {
                    OnPropertyChanged("OivAlacak");
                    _OivAlacak = value;
                }
            }
        }

        [DbAlan("OivDvzBorc", SqlDbType.Decimal, 13)]
        public decimal OivDvzBorc
        {
            get { return _OivDvzBorc; }
            set
            {
                if (_OivDvzBorc != value)
                {
                    OnPropertyChanged("OivDvzBorc");
                    _OivDvzBorc = value;
                }
            }
        }

        [DbAlan("OivDvzAlacak", SqlDbType.Decimal, 13)]
        public decimal OivDvzAlacak
        {
            get { return _OivDvzAlacak; }
            set
            {
                if (_OivDvzAlacak != value)
                {
                    OnPropertyChanged("OivDvzAlacak");
                    _OivDvzAlacak = value;
                }
            }
        }

        [DbAlan("BankaUlkeNumKod1", SqlDbType.VarChar, 3)]
        public string BankaUlkeNumKod1
        {
            get { return _BankaUlkeNumKod1; }
            set
            {
                if (_BankaUlkeNumKod1 != value)
                {
                    OnPropertyChanged("BankaUlkeNumKod1");
                    _BankaUlkeNumKod1 = value;
                }
            }
        }

        [DbAlan("BankaUlkeNumKod2", SqlDbType.VarChar, 3)]
        public string BankaUlkeNumKod2
        {
            get { return _BankaUlkeNumKod2; }
            set
            {
                if (_BankaUlkeNumKod2 != value)
                {
                    OnPropertyChanged("BankaUlkeNumKod2");
                    _BankaUlkeNumKod2 = value;
                }
            }
        }

        [DbAlan("BankaUlkeNumKod3", SqlDbType.VarChar, 3)]
        public string BankaUlkeNumKod3
        {
            get { return _BankaUlkeNumKod3; }
            set
            {
                if (_BankaUlkeNumKod3 != value)
                {
                    OnPropertyChanged("BankaUlkeNumKod3");
                    _BankaUlkeNumKod3 = value;
                }
            }
        }

        [DbAlan("BankaUlkeNumKod4", SqlDbType.VarChar, 3)]
        public string BankaUlkeNumKod4
        {
            get { return _BankaUlkeNumKod4; }
            set
            {
                if (_BankaUlkeNumKod4 != value)
                {
                    OnPropertyChanged("BankaUlkeNumKod4");
                    _BankaUlkeNumKod4 = value;
                }
            }
        }

        [DbAlan("IslemTipi", SqlDbType.SmallInt, 2)]
        public short IslemTipi
        {
            get { return _IslemTipi; }
            set
            {
                if (_IslemTipi != value)
                {
                    OnPropertyChanged("IslemTipi");
                    _IslemTipi = value;
                }
            }
        }

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

        [DbAlan("HesapTuru", SqlDbType.SmallInt, 2)]
        public short HesapTuru
        {
            get { return _HesapTuru; }
            set
            {
                if (_HesapTuru != value)
                {
                    OnPropertyChanged("HesapTuru");
                    _HesapTuru = value;
                }
            }
        }

        [DbAlan("VergiDairesi2", SqlDbType.VarChar, 12)]
        public string VergiDairesi2
        {
            get { return _VergiDairesi2; }
            set
            {
                if (_VergiDairesi2 != value)
                {
                    OnPropertyChanged("VergiDairesi2");
                    _VergiDairesi2 = value;
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

        [DbAlan("BekleyenSatisTkf", SqlDbType.Decimal, 13)]
        public decimal BekleyenSatisTkf
        {
            get { return _BekleyenSatisTkf; }
            set
            {
                if (_BekleyenSatisTkf != value)
                {
                    OnPropertyChanged("BekleyenSatisTkf");
                    _BekleyenSatisTkf = value;
                }
            }
        }

        [DbAlan("BekleyenAlimTkf", SqlDbType.Decimal, 13)]
        public decimal BekleyenAlimTkf
        {
            get { return _BekleyenAlimTkf; }
            set
            {
                if (_BekleyenAlimTkf != value)
                {
                    OnPropertyChanged("BekleyenAlimTkf");
                    _BekleyenAlimTkf = value;
                }
            }
        }

        [DbAlan("BekleyenSatTkfDoviz", SqlDbType.Decimal, 13)]
        public decimal BekleyenSatTkfDoviz
        {
            get { return _BekleyenSatTkfDoviz; }
            set
            {
                if (_BekleyenSatTkfDoviz != value)
                {
                    OnPropertyChanged("BekleyenSatTkfDoviz");
                    _BekleyenSatTkfDoviz = value;
                }
            }
        }

        [DbAlan("BekleyenAlimTkfDoviz", SqlDbType.Decimal, 13)]
        public decimal BekleyenAlimTkfDoviz
        {
            get { return _BekleyenAlimTkfDoviz; }
            set
            {
                if (_BekleyenAlimTkfDoviz != value)
                {
                    OnPropertyChanged("BekleyenAlimTkfDoviz");
                    _BekleyenAlimTkfDoviz = value;
                }
            }
        }

        [DbAlan("BankaIBAN1", SqlDbType.VarChar, 40)]
        public string BankaIBAN1
        {
            get { return _BankaIBAN1; }
            set
            {
                if (_BankaIBAN1 != value)
                {
                    OnPropertyChanged("BankaIBAN1");
                    _BankaIBAN1 = value;
                }
            }
        }

        [DbAlan("BankaIBAN2", SqlDbType.VarChar, 40)]
        public string BankaIBAN2
        {
            get { return _BankaIBAN2; }
            set
            {
                if (_BankaIBAN2 != value)
                {
                    OnPropertyChanged("BankaIBAN2");
                    _BankaIBAN2 = value;
                }
            }
        }

        [DbAlan("BankaIBAN3", SqlDbType.VarChar, 40)]
        public string BankaIBAN3
        {
            get { return _BankaIBAN3; }
            set
            {
                if (_BankaIBAN3 != value)
                {
                    OnPropertyChanged("BankaIBAN3");
                    _BankaIBAN3 = value;
                }
            }
        }

        [DbAlan("BankaIBAN4", SqlDbType.VarChar, 40)]
        public string BankaIBAN4
        {
            get { return _BankaIBAN4; }
            set
            {
                if (_BankaIBAN4 != value)
                {
                    OnPropertyChanged("BankaIBAN4");
                    _BankaIBAN4 = value;
                }
            }
        }

        [DbAlan("BankaDvzCinsi1", SqlDbType.VarChar, 3)]
        public string BankaDvzCinsi1
        {
            get { return _BankaDvzCinsi1; }
            set
            {
                if (_BankaDvzCinsi1 != value)
                {
                    OnPropertyChanged("BankaDvzCinsi1");
                    _BankaDvzCinsi1 = value;
                }
            }
        }

        [DbAlan("BankaDvzCinsi2", SqlDbType.VarChar, 3)]
        public string BankaDvzCinsi2
        {
            get { return _BankaDvzCinsi2; }
            set
            {
                if (_BankaDvzCinsi2 != value)
                {
                    OnPropertyChanged("BankaDvzCinsi2");
                    _BankaDvzCinsi2 = value;
                }
            }
        }

        [DbAlan("BankaDvzCinsi3", SqlDbType.VarChar, 3)]
        public string BankaDvzCinsi3
        {
            get { return _BankaDvzCinsi3; }
            set
            {
                if (_BankaDvzCinsi3 != value)
                {
                    OnPropertyChanged("BankaDvzCinsi3");
                    _BankaDvzCinsi3 = value;
                }
            }
        }

        [DbAlan("BankaDvzCinsi4", SqlDbType.VarChar, 3)]
        public string BankaDvzCinsi4
        {
            get { return _BankaDvzCinsi4; }
            set
            {
                if (_BankaDvzCinsi4 != value)
                {
                    OnPropertyChanged("BankaDvzCinsi4");
                    _BankaDvzCinsi4 = value;
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

        [DbAlan("IrsaliyeRaporAdi", SqlDbType.VarChar, 254)]
        public string IrsaliyeRaporAdi
        {
            get { return _IrsaliyeRaporAdi; }
            set
            {
                if (_IrsaliyeRaporAdi != value)
                {
                    OnPropertyChanged("IrsaliyeRaporAdi");
                    _IrsaliyeRaporAdi = value;
                }
            }
        }

        [DbAlan("FaturaRaporAdi", SqlDbType.VarChar, 254)]
        public string FaturaRaporAdi
        {
            get { return _FaturaRaporAdi; }
            set
            {
                if (_FaturaRaporAdi != value)
                {
                    OnPropertyChanged("FaturaRaporAdi");
                    _FaturaRaporAdi = value;
                }
            }
        }

        [DbAlan("KDVTevfikatUygula", SqlDbType.SmallInt, 2)]
        public short KDVTevfikatUygula
        {
            get { return _KDVTevfikatUygula; }
            set
            {
                if (_KDVTevfikatUygula != value)
                {
                    OnPropertyChanged("KDVTevfikatUygula");
                    _KDVTevfikatUygula = value;
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

        [DbAlan("EFatAdresCadde", SqlDbType.VarChar, 40)]
        public string EFatAdresCadde
        {
            get { return _EFatAdresCadde; }
            set
            {
                if (_EFatAdresCadde != value)
                {
                    OnPropertyChanged("EFatAdresCadde");
                    _EFatAdresCadde = value;
                }
            }
        }

        [DbAlan("EFatAdresBinaAdi", SqlDbType.VarChar, 40)]
        public string EFatAdresBinaAdi
        {
            get { return _EFatAdresBinaAdi; }
            set
            {
                if (_EFatAdresBinaAdi != value)
                {
                    OnPropertyChanged("EFatAdresBinaAdi");
                    _EFatAdresBinaAdi = value;
                }
            }
        }

        [DbAlan("EFatAdresDisKapiNo", SqlDbType.VarChar, 10)]
        public string EFatAdresDisKapiNo
        {
            get { return _EFatAdresDisKapiNo; }
            set
            {
                if (_EFatAdresDisKapiNo != value)
                {
                    OnPropertyChanged("EFatAdresDisKapiNo");
                    _EFatAdresDisKapiNo = value;
                }
            }
        }

        [DbAlan("EFatAdresIcKapiNo", SqlDbType.VarChar, 10)]
        public string EFatAdresIcKapiNo
        {
            get { return _EFatAdresIcKapiNo; }
            set
            {
                if (_EFatAdresIcKapiNo != value)
                {
                    OnPropertyChanged("EFatAdresIcKapiNo");
                    _EFatAdresIcKapiNo = value;
                }
            }
        }

        [DbAlan("EFatAdresKasabaKoy", SqlDbType.VarChar, 40)]
        public string EFatAdresKasabaKoy
        {
            get { return _EFatAdresKasabaKoy; }
            set
            {
                if (_EFatAdresKasabaKoy != value)
                {
                    OnPropertyChanged("EFatAdresKasabaKoy");
                    _EFatAdresKasabaKoy = value;
                }
            }
        }

        [DbAlan("EFatAdresIlce", SqlDbType.VarChar, 40)]
        public string EFatAdresIlce
        {
            get { return _EFatAdresIlce; }
            set
            {
                if (_EFatAdresIlce != value)
                {
                    OnPropertyChanged("EFatAdresIlce");
                    _EFatAdresIlce = value;
                }
            }
        }

        [DbAlan("EFatKullanici", SqlDbType.SmallInt, 2)]
        public short EFatKullanici
        {
            get { return _EFatKullanici; }
            set
            {
                if (_EFatKullanici != value)
                {
                    OnPropertyChanged("EFatKullanici");
                    _EFatKullanici = value;
                }
            }
        }

        [DbAlan("EFatSenaryo", SqlDbType.SmallInt, 2)]
        public short EFatSenaryo
        {
            get { return _EFatSenaryo; }
            set
            {
                if (_EFatSenaryo != value)
                {
                    OnPropertyChanged("EFatSenaryo");
                    _EFatSenaryo = value;
                }
            }
        }

        [DbAlan("EFatEtiket", SqlDbType.VarChar, 128)]
        public string EFatEtiket
        {
            get { return _EFatEtiket; }
            set
            {
                if (_EFatEtiket != value)
                {
                    OnPropertyChanged("EFatEtiket");
                    _EFatEtiket = value;
                }
            }
        }

        [DbAlan("Ad", SqlDbType.VarChar, 40)]
        public string Ad
        {
            get { return _Ad; }
            set
            {
                if (_Ad != value)
                {
                    OnPropertyChanged("Ad");
                    _Ad = value;
                }
            }
        }

        [DbAlan("SoyAd", SqlDbType.VarChar, 40)]
        public string SoyAd
        {
            get { return _SoyAd; }
            set
            {
                if (_SoyAd != value)
                {
                    OnPropertyChanged("SoyAd");
                    _SoyAd = value;
                }
            }
        }

        [DbAlan("EFatMusBrmSubeTnm", SqlDbType.VarChar, 20)]
        public string EFatMusBrmSubeTnm
        {
            get { return _EFatMusBrmSubeTnm; }
            set
            {
                if (_EFatMusBrmSubeTnm != value)
                {
                    OnPropertyChanged("EFatMusBrmSubeTnm");
                    _EFatMusBrmSubeTnm = value;
                }
            }
        }

        [DbAlan("EFatMusBrmSubeTnmDeger", SqlDbType.VarChar, 20)]
        public string EFatMusBrmSubeTnmDeger
        {
            get { return _EFatMusBrmSubeTnmDeger; }
            set
            {
                if (_EFatMusBrmSubeTnmDeger != value)
                {
                    OnPropertyChanged("EFatMusBrmSubeTnmDeger");
                    _EFatMusBrmSubeTnmDeger = value;
                }
            }
        }

        [DbAlan("EFatCHKoduTnm", SqlDbType.VarChar, 20)]
        public string EFatCHKoduTnm
        {
            get { return _EFatCHKoduTnm; }
            set
            {
                if (_EFatCHKoduTnm != value)
                {
                    OnPropertyChanged("EFatCHKoduTnm");
                    _EFatCHKoduTnm = value;
                }
            }
        }

        [DbAlan("KDVMuaf", SqlDbType.SmallInt, 2)]
        public short KDVMuaf
        {
            get { return _KDVMuaf; }
            set
            {
                if (_KDVMuaf != value)
                {
                    OnPropertyChanged("KDVMuaf");
                    _KDVMuaf = value;
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



        internal void CHK_PropertyChanged(object sender, PropertyChangedEventArgs e)
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


        public KKP_CHK()
        {
            ///this.PropertyChanged = this.CHK_PropertyChanged;
        }

    }
}
