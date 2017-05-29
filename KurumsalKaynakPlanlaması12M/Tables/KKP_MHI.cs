using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
    [DbTablo("MUHASEBE6", "MUHASEBE6", "MHI")]
    public class KKP_MHI : INotifyPropertyChanged
    {

        #region Fields
        private string _HesapKod;
        private string _KebirKod;
        private int _Tarih;
        private short _Fistip;
        private decimal _FisNo;
        private short _SiraNo;
        private string _Aciklama;
        private short _BA;
        private decimal _Tutar;
        private int _MaddeNo;
        private string _Referans;
        private string _FisKod;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private string _Kod4;
        private string _Kod5;
        private string _Kod6;
        private decimal _Kod7;
        private decimal _Kod8;
        private string _DovizCinsi;
        private string _Birim;
        private string _Aciklama2;
        private decimal _Bakiye;
        private decimal _DovizKuru;
        private string _EvrakNo;
        private string _MasrafMerkez;
        private decimal _Miktar;
        private int _EvrakTarih;
        private short _EvrakTip;
        private decimal _DovizTutar;
        private string _MrkHKod;
        private string _ButceKod;
        private int _Tarih2;
        private short _DvzTL;
        private int _FinMal_EsasTarih;
        private int _FinMal_Vade_Tarihi;
        private decimal _FinMal_VadeFarki_Tutari;
        private decimal _Kredi_Borc_Tutari;
        private decimal _Aktif_FinMal;
        private int _Finmal_Dnm_BasTarih;
        private int _Finmal_Dnm_BitTarih;
        private decimal _ReelOlmayan_Finmal;
        private short _FinMal_Arindirma_Sekli;
        private short _FinMal_Gider_Turu;
        private int _FinMal_Yil;
        private short _FinMal_Donem;
        private string _Ilgili_Evrak_No;
        private int _Ilgili_Evrak_Tarihi;
        private decimal _Duzeltilmis_Deger;
        private short _KayitTuru;
        private short _AnaEvrakTip;
        private string _KartDovizCinsi;
        private decimal _KartDovizKuru;
        private string _MMKKartDovizCinsi;
        private decimal _MMKKartDovizKuru;
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
        private short _DvzKurCinsi;
        private short _Printed;
        private decimal _YevmiyeSiraNo;
        private string _OdemeTipi;
        private string _EvrakTipAciklama;
        private short _YevmiyeEvrakTipi;
        private string _KaynakEvrakNo;
        private int _KaynakEvrakTarih;
        private string _IthalatDosyaNo;
        private string _ProjeKodu;
        private int _Row_ID;

        #endregion Fields



        #region table Properties

        [DbAlan("HesapKod", SqlDbType.VarChar, 50)]
        public string HesapKod
        {
            get { return _HesapKod; }
            set
            {
                if (_HesapKod != value)
                {
                    OnPropertyChanged("HesapKod");
                    _HesapKod = value;
                }
            }
        }

        [DbAlan("KebirKod", SqlDbType.VarChar, 50)]
        public string KebirKod
        {
            get { return _KebirKod; }
            set
            {
                if (_KebirKod != value)
                {
                    OnPropertyChanged("KebirKod");
                    _KebirKod = value;
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

        [DbAlan("Fistip", SqlDbType.SmallInt, 2, false, true, false)]
        public short Fistip
        {
            get { return _Fistip; }
            set
            {
                if (_Fistip != value)
                {
                    OnPropertyChanged("Fistip");
                    _Fistip = value;
                }
            }
        }

        [DbAlan("FisNo", SqlDbType.Decimal, 9, false, true, false)]
        public decimal FisNo
        {
            get { return _FisNo; }
            set
            {
                if (_FisNo != value)
                {
                    OnPropertyChanged("FisNo");
                    _FisNo = value;
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

        [DbAlan("BA", SqlDbType.SmallInt, 2)]
        public short BA
        {
            get { return _BA; }
            set
            {
                if (_BA != value)
                {
                    OnPropertyChanged("BA");
                    _BA = value;
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

        [DbAlan("MaddeNo", SqlDbType.Int, 4, false, true, false)]
        public int MaddeNo
        {
            get { return _MaddeNo; }
            set
            {
                if (_MaddeNo != value)
                {
                    OnPropertyChanged("MaddeNo");
                    _MaddeNo = value;
                }
            }
        }

        [DbAlan("Referans", SqlDbType.VarChar, 8)]
        public string Referans
        {
            get { return _Referans; }
            set
            {
                if (_Referans != value)
                {
                    OnPropertyChanged("Referans");
                    _Referans = value;
                }
            }
        }

        [DbAlan("FisKod", SqlDbType.VarChar, 10)]
        public string FisKod
        {
            get { return _FisKod; }
            set
            {
                if (_FisKod != value)
                {
                    OnPropertyChanged("FisKod");
                    _FisKod = value;
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

        [DbAlan("Kod6", SqlDbType.VarChar, 20)]
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

        [DbAlan("Kod7", SqlDbType.Decimal, 13)]
        public decimal Kod7
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

        [DbAlan("Kod8", SqlDbType.Decimal, 13)]
        public decimal Kod8
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

        [DbAlan("Bakiye", SqlDbType.Decimal, 13)]
        public decimal Bakiye
        {
            get { return _Bakiye; }
            set
            {
                if (_Bakiye != value)
                {
                    OnPropertyChanged("Bakiye");
                    _Bakiye = value;
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

        [DbAlan("EvrakNo", SqlDbType.VarChar, 32)]
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

        [DbAlan("EvrakTip", SqlDbType.SmallInt, 2)]
        public short EvrakTip
        {
            get { return _EvrakTip; }
            set
            {
                if (_EvrakTip != value)
                {
                    OnPropertyChanged("EvrakTip");
                    _EvrakTip = value;
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

        [DbAlan("MrkHKod", SqlDbType.VarChar, 50)]
        public string MrkHKod
        {
            get { return _MrkHKod; }
            set
            {
                if (_MrkHKod != value)
                {
                    OnPropertyChanged("MrkHKod");
                    _MrkHKod = value;
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

        [DbAlan("FinMal_EsasTarih", SqlDbType.Int, 4)]
        public int FinMal_EsasTarih
        {
            get { return _FinMal_EsasTarih; }
            set
            {
                if (_FinMal_EsasTarih != value)
                {
                    OnPropertyChanged("FinMal_EsasTarih");
                    _FinMal_EsasTarih = value;
                }
            }
        }

        [DbAlan("FinMal_Vade_Tarihi", SqlDbType.Int, 4)]
        public int FinMal_Vade_Tarihi
        {
            get { return _FinMal_Vade_Tarihi; }
            set
            {
                if (_FinMal_Vade_Tarihi != value)
                {
                    OnPropertyChanged("FinMal_Vade_Tarihi");
                    _FinMal_Vade_Tarihi = value;
                }
            }
        }

        [DbAlan("FinMal_VadeFarki_Tutari", SqlDbType.Decimal, 13)]
        public decimal FinMal_VadeFarki_Tutari
        {
            get { return _FinMal_VadeFarki_Tutari; }
            set
            {
                if (_FinMal_VadeFarki_Tutari != value)
                {
                    OnPropertyChanged("FinMal_VadeFarki_Tutari");
                    _FinMal_VadeFarki_Tutari = value;
                }
            }
        }

        [DbAlan("Kredi_Borc_Tutari", SqlDbType.Decimal, 13)]
        public decimal Kredi_Borc_Tutari
        {
            get { return _Kredi_Borc_Tutari; }
            set
            {
                if (_Kredi_Borc_Tutari != value)
                {
                    OnPropertyChanged("Kredi_Borc_Tutari");
                    _Kredi_Borc_Tutari = value;
                }
            }
        }

        [DbAlan("Aktif_FinMal", SqlDbType.Decimal, 13)]
        public decimal Aktif_FinMal
        {
            get { return _Aktif_FinMal; }
            set
            {
                if (_Aktif_FinMal != value)
                {
                    OnPropertyChanged("Aktif_FinMal");
                    _Aktif_FinMal = value;
                }
            }
        }

        [DbAlan("Finmal_Dnm_BasTarih", SqlDbType.Int, 4)]
        public int Finmal_Dnm_BasTarih
        {
            get { return _Finmal_Dnm_BasTarih; }
            set
            {
                if (_Finmal_Dnm_BasTarih != value)
                {
                    OnPropertyChanged("Finmal_Dnm_BasTarih");
                    _Finmal_Dnm_BasTarih = value;
                }
            }
        }

        [DbAlan("Finmal_Dnm_BitTarih", SqlDbType.Int, 4)]
        public int Finmal_Dnm_BitTarih
        {
            get { return _Finmal_Dnm_BitTarih; }
            set
            {
                if (_Finmal_Dnm_BitTarih != value)
                {
                    OnPropertyChanged("Finmal_Dnm_BitTarih");
                    _Finmal_Dnm_BitTarih = value;
                }
            }
        }

        [DbAlan("ReelOlmayan_Finmal", SqlDbType.Decimal, 13)]
        public decimal ReelOlmayan_Finmal
        {
            get { return _ReelOlmayan_Finmal; }
            set
            {
                if (_ReelOlmayan_Finmal != value)
                {
                    OnPropertyChanged("ReelOlmayan_Finmal");
                    _ReelOlmayan_Finmal = value;
                }
            }
        }

        [DbAlan("FinMal_Arindirma_Sekli", SqlDbType.SmallInt, 2)]
        public short FinMal_Arindirma_Sekli
        {
            get { return _FinMal_Arindirma_Sekli; }
            set
            {
                if (_FinMal_Arindirma_Sekli != value)
                {
                    OnPropertyChanged("FinMal_Arindirma_Sekli");
                    _FinMal_Arindirma_Sekli = value;
                }
            }
        }

        [DbAlan("FinMal_Gider_Turu", SqlDbType.SmallInt, 2)]
        public short FinMal_Gider_Turu
        {
            get { return _FinMal_Gider_Turu; }
            set
            {
                if (_FinMal_Gider_Turu != value)
                {
                    OnPropertyChanged("FinMal_Gider_Turu");
                    _FinMal_Gider_Turu = value;
                }
            }
        }

        [DbAlan("FinMal_Yil", SqlDbType.Int, 4)]
        public int FinMal_Yil
        {
            get { return _FinMal_Yil; }
            set
            {
                if (_FinMal_Yil != value)
                {
                    OnPropertyChanged("FinMal_Yil");
                    _FinMal_Yil = value;
                }
            }
        }

        [DbAlan("FinMal_Donem", SqlDbType.SmallInt, 2)]
        public short FinMal_Donem
        {
            get { return _FinMal_Donem; }
            set
            {
                if (_FinMal_Donem != value)
                {
                    OnPropertyChanged("FinMal_Donem");
                    _FinMal_Donem = value;
                }
            }
        }

        [DbAlan("Ilgili_Evrak_No", SqlDbType.VarChar, 16)]
        public string Ilgili_Evrak_No
        {
            get { return _Ilgili_Evrak_No; }
            set
            {
                if (_Ilgili_Evrak_No != value)
                {
                    OnPropertyChanged("Ilgili_Evrak_No");
                    _Ilgili_Evrak_No = value;
                }
            }
        }

        [DbAlan("Ilgili_Evrak_Tarihi", SqlDbType.Int, 4)]
        public int Ilgili_Evrak_Tarihi
        {
            get { return _Ilgili_Evrak_Tarihi; }
            set
            {
                if (_Ilgili_Evrak_Tarihi != value)
                {
                    OnPropertyChanged("Ilgili_Evrak_Tarihi");
                    _Ilgili_Evrak_Tarihi = value;
                }
            }
        }

        [DbAlan("Duzeltilmis_Deger", SqlDbType.Decimal, 13)]
        public decimal Duzeltilmis_Deger
        {
            get { return _Duzeltilmis_Deger; }
            set
            {
                if (_Duzeltilmis_Deger != value)
                {
                    OnPropertyChanged("Duzeltilmis_Deger");
                    _Duzeltilmis_Deger = value;
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

        [DbAlan("MMKKartDovizCinsi", SqlDbType.VarChar, 3)]
        public string MMKKartDovizCinsi
        {
            get { return _MMKKartDovizCinsi; }
            set
            {
                if (_MMKKartDovizCinsi != value)
                {
                    OnPropertyChanged("MMKKartDovizCinsi");
                    _MMKKartDovizCinsi = value;
                }
            }
        }

        [DbAlan("MMKKartDovizKuru", SqlDbType.Decimal, 13)]
        public decimal MMKKartDovizKuru
        {
            get { return _MMKKartDovizKuru; }
            set
            {
                if (_MMKKartDovizKuru != value)
                {
                    OnPropertyChanged("MMKKartDovizKuru");
                    _MMKKartDovizKuru = value;
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

        [DbAlan("Printed", SqlDbType.SmallInt, 2)]
        public short Printed
        {
            get { return _Printed; }
            set
            {
                if (_Printed != value)
                {
                    OnPropertyChanged("Printed");
                    _Printed = value;
                }
            }
        }

        [DbAlan("YevmiyeSiraNo", SqlDbType.Decimal, 9)]
        public decimal YevmiyeSiraNo
        {
            get { return _YevmiyeSiraNo; }
            set
            {
                if (_YevmiyeSiraNo != value)
                {
                    OnPropertyChanged("YevmiyeSiraNo");
                    _YevmiyeSiraNo = value;
                }
            }
        }

        [DbAlan("OdemeTipi", SqlDbType.VarChar, 20)]
        public string OdemeTipi
        {
            get { return _OdemeTipi; }
            set
            {
                if (_OdemeTipi != value)
                {
                    OnPropertyChanged("OdemeTipi");
                    _OdemeTipi = value;
                }
            }
        }

        [DbAlan("EvrakTipAciklama", SqlDbType.VarChar, 64)]
        public string EvrakTipAciklama
        {
            get { return _EvrakTipAciklama; }
            set
            {
                if (_EvrakTipAciklama != value)
                {
                    OnPropertyChanged("EvrakTipAciklama");
                    _EvrakTipAciklama = value;
                }
            }
        }

        [DbAlan("YevmiyeEvrakTipi", SqlDbType.SmallInt, 2)]
        public short YevmiyeEvrakTipi
        {
            get { return _YevmiyeEvrakTipi; }
            set
            {
                if (_YevmiyeEvrakTipi != value)
                {
                    OnPropertyChanged("YevmiyeEvrakTipi");
                    _YevmiyeEvrakTipi = value;
                }
            }
        }

        [DbAlan("KaynakEvrakNo", SqlDbType.VarChar, 16)]
        public string KaynakEvrakNo
        {
            get { return _KaynakEvrakNo; }
            set
            {
                if (_KaynakEvrakNo != value)
                {
                    OnPropertyChanged("KaynakEvrakNo");
                    _KaynakEvrakNo = value;
                }
            }
        }

        [DbAlan("KaynakEvrakTarih", SqlDbType.Int, 4)]
        public int KaynakEvrakTarih
        {
            get { return _KaynakEvrakTarih; }
            set
            {
                if (_KaynakEvrakTarih != value)
                {
                    OnPropertyChanged("KaynakEvrakTarih");
                    _KaynakEvrakTarih = value;
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



        internal void MHI_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

            //PropertyValue val = _ChangedList.Find(t => t.PropertiName == propertyName);
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

        public KKP_MHI Copy()
        {
            return (KKP_MHI)this.MemberwiseClone();
        }

        #endregion property changes events functions

        public KKP_MHI()
        {

        }

    }
}
