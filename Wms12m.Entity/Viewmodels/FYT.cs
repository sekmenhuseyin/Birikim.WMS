using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity.Viewmodels
{

    #region FYT Class 

    #region FYTE Enum 
    public enum FYTE
    {
        MalKodu,
        BasTarih,
        BasSaat,
        BitTarih,
        BitSaat,
        SiraNo,
        RenkBedenKod1,
        RenkBedenKod2,
        RenkBedenKod3,
        RenkBedenKod4,
        FiyatListNum,
        FiyatTuru,
        Kod1,
        Kod2,
        Kod3,
        KodTipi,
        MusteriKodu,
        AlisFiyat1,
        AlisFiyat2,
        AlisFiyat3,
        DvzAlisFiyat1,
        DvzAlisFiyat2,
        DvzAlisFiyat3,
        AF1DovizCinsi,
        AF2DovizCinsi,
        AF3DovizCinsi,
        AF1KDV,
        AF2KDV,
        AF3KDV,
        DovizAF1KDV,
        DovizAF2KDV,
        DovizAF3KDV,
        AF1Birim,
        AF2Birim,
        AF3Birim,
        DovizAF1Birim,
        DovizAF2Birim,
        DovizAF3Birim,
        SatisFiyat1,
        SatisFiyat2,
        SatisFiyat3,
        SatisFiyat4,
        SatisFiyat5,
        SatisFiyat6,
        DvzSatisFiyat1,
        DvzSatisFiyat2,
        DvzSatisFiyat3,
        SF1DovizCinsi,
        SF2DovizCinsi,
        SF3DovizCinsi,
        SF4DovizCinsi,
        SF5DovizCinsi,
        SF6DovizCinsi,
        SF1KDV,
        SF2KDV,
        SF3KDV,
        SF4KDV,
        SF5KDV,
        SF6KDV,
        DovizSF1KDV,
        DovizSF2KDV,
        DovizSF3KDV,
        SF1Birim,
        SF2Birim,
        SF3Birim,
        SF4Birim,
        SF5Birim,
        SF6Birim,
        DovizSF1Birim,
        DovizSF2Birim,
        DovizSF3Birim,
        FiyatListName,
        SatisFiyatAltLimit,
        SatisFiyatUstLimit,
        SF1ValorGun,
        SF2ValorGun,
        SF3ValorGun,
        SF4ValorGun,
        SF5ValorGun,
        SF6ValorGun,
        DvzSF1ValorGun,
        DvzSF2ValorGun,
        DvzSF3ValorGun,
        KayitTuru,
        GuvenlikKod,
        Kaydeden,
        KayitTarih,
        KayitSaat,
        KayitKaynak,
        KayitSurum,
        Degistiren,
        DegisTarih,
        DegisSaat,
        DegisKaynak,
        DegisSurum,
        CheckSum,
        Aciklama,
        Kod4,
        Kod5,
        Kod6,
        Kod7,
        Kod8,
        Kod9,
        Kod10,
        Kod11,
        Kod12,
        SF1VadeTarih,
        SF2VadeTarih,
        SF3VadeTarih,
        SF4VadeTarih,
        SF5VadeTarih,
        SF6VadeTarih,
        DvzSF1VadeTarih,
        DvzSF2VadeTarih,
        DvzSF3VadeTarih,
        FiyatListeTipi,
        ROW_ID,
        timestamp

    }
    #endregion /// FYTE Enum           

    public class FYT : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private string _MalKodu;
        private int _BasTarih;
        private int _BasSaat;
        private int _BitTarih;
        private int _BitSaat;
        private short _SiraNo;
        private string _RenkBedenKod1;
        private string _RenkBedenKod2;
        private string _RenkBedenKod3;
        private string _RenkBedenKod4;
        private string _FiyatListNum;
        private short _FiyatTuru;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
        private short _KodTipi;
        private string _MusteriKodu;
        private decimal _AlisFiyat1;
        private decimal _AlisFiyat2;
        private decimal _AlisFiyat3;
        private decimal _DvzAlisFiyat1;
        private decimal _DvzAlisFiyat2;
        private decimal _DvzAlisFiyat3;
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
        private decimal _DvzSatisFiyat1;
        private decimal _DvzSatisFiyat2;
        private decimal _DvzSatisFiyat3;
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
        private string _FiyatListName;
        private decimal _SatisFiyatAltLimit;
        private decimal _SatisFiyatUstLimit;
        private short _SF1ValorGun;
        private short _SF2ValorGun;
        private short _SF3ValorGun;
        private short _SF4ValorGun;
        private short _SF5ValorGun;
        private short _SF6ValorGun;
        private short _DvzSF1ValorGun;
        private short _DvzSF2ValorGun;
        private short _DvzSF3ValorGun;
        private short _KayitTuru;
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
        private string _Aciklama;
        private string _Kod4;
        private string _Kod5;
        private string _Kod6;
        private string _Kod7;
        private string _Kod8;
        private string _Kod9;
        private string _Kod10;
        private decimal _Kod11;
        private decimal _Kod12;
        private int _SF1VadeTarih;
        private int _SF2VadeTarih;
        private int _SF3VadeTarih;
        private int _SF4VadeTarih;
        private int _SF5VadeTarih;
        private int _SF6VadeTarih;
        private int _DvzSF1VadeTarih;
        private int _DvzSF2VadeTarih;
        private int _DvzSF3VadeTarih;
        private short _FiyatListeTipi;
        private int _ROW_ID;
        private byte[] _timestamp;
        private string _pk_MalKodu;
        private int _pk_BasTarih;
        private int _pk_BasSaat;
        private short _pk_SiraNo;
        private string _pk_FiyatListNum;
        private string _pk_MusteriKodu;
        private short _pk_FiyatListeTipi;
        #endregion /// Fields


        /// <summary> VARCHAR (30) PrimaryKey * </summary>
        public string MalKodu
        {
            get { return this._MalKodu; }
            set
            {
                this._MalKodu = value;
                OnPropertyChanged("MalKodu");
            }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
        public int BasTarih
        {
            get { return this._BasTarih; }
            set
            {
                this._BasTarih = value;
                OnPropertyChanged("BasTarih");
            }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
        public int BasSaat
        {
            get { return this._BasSaat; }
            set
            {
                this._BasSaat = value;
                OnPropertyChanged("BasSaat");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitTarih
        {
            get { return this._BitTarih; }
            set
            {
                this._BitTarih = value;
                OnPropertyChanged("BitTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitSaat
        {
            get { return this._BitSaat; }
            set
            {
                this._BitSaat = value;
                OnPropertyChanged("BitSaat");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short SiraNo
        {
            get { return this._SiraNo; }
            set
            {
                this._SiraNo = value;
                OnPropertyChanged("SiraNo");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod1
        {
            get { return this._RenkBedenKod1; }
            set
            {
                this._RenkBedenKod1 = value;
                OnPropertyChanged("RenkBedenKod1");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod2
        {
            get { return this._RenkBedenKod2; }
            set
            {
                this._RenkBedenKod2 = value;
                OnPropertyChanged("RenkBedenKod2");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod3
        {
            get { return this._RenkBedenKod3; }
            set
            {
                this._RenkBedenKod3 = value;
                OnPropertyChanged("RenkBedenKod3");
            }
        }

        /// <summary> VARCHAR (16) * </summary>
        public string RenkBedenKod4
        {
            get { return this._RenkBedenKod4; }
            set
            {
                this._RenkBedenKod4 = value;
                OnPropertyChanged("RenkBedenKod4");
            }
        }

        /// <summary> VARCHAR (16) PrimaryKey * </summary>
        public string FiyatListNum
        {
            get { return this._FiyatListNum; }
            set
            {
                this._FiyatListNum = value;
                OnPropertyChanged("FiyatListNum");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short FiyatTuru
        {
            get { return this._FiyatTuru; }
            set
            {
                this._FiyatTuru = value;
                OnPropertyChanged("FiyatTuru");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod1
        {
            get { return this._Kod1; }
            set
            {
                this._Kod1 = value;
                OnPropertyChanged("Kod1");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod2
        {
            get { return this._Kod2; }
            set
            {
                this._Kod2 = value;
                OnPropertyChanged("Kod2");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod3
        {
            get { return this._Kod3; }
            set
            {
                this._Kod3 = value;
                OnPropertyChanged("Kod3");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KodTipi
        {
            get { return this._KodTipi; }
            set
            {
                this._KodTipi = value;
                OnPropertyChanged("KodTipi");
            }
        }

        /// <summary> VARCHAR (20) PrimaryKey * </summary>
        public string MusteriKodu
        {
            get { return this._MusteriKodu; }
            set
            {
                this._MusteriKodu = value;
                OnPropertyChanged("MusteriKodu");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal AlisFiyat1
        {
            get { return this._AlisFiyat1; }
            set
            {
                this._AlisFiyat1 = value;
                OnPropertyChanged("AlisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal AlisFiyat2
        {
            get { return this._AlisFiyat2; }
            set
            {
                this._AlisFiyat2 = value;
                OnPropertyChanged("AlisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal AlisFiyat3
        {
            get { return this._AlisFiyat3; }
            set
            {
                this._AlisFiyat3 = value;
                OnPropertyChanged("AlisFiyat3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzAlisFiyat1
        {
            get { return this._DvzAlisFiyat1; }
            set
            {
                this._DvzAlisFiyat1 = value;
                OnPropertyChanged("DvzAlisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzAlisFiyat2
        {
            get { return this._DvzAlisFiyat2; }
            set
            {
                this._DvzAlisFiyat2 = value;
                OnPropertyChanged("DvzAlisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzAlisFiyat3
        {
            get { return this._DvzAlisFiyat3; }
            set
            {
                this._DvzAlisFiyat3 = value;
                OnPropertyChanged("DvzAlisFiyat3");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string AF1DovizCinsi
        {
            get { return this._AF1DovizCinsi; }
            set
            {
                this._AF1DovizCinsi = value;
                OnPropertyChanged("AF1DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string AF2DovizCinsi
        {
            get { return this._AF2DovizCinsi; }
            set
            {
                this._AF2DovizCinsi = value;
                OnPropertyChanged("AF2DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string AF3DovizCinsi
        {
            get { return this._AF3DovizCinsi; }
            set
            {
                this._AF3DovizCinsi = value;
                OnPropertyChanged("AF3DovizCinsi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF1KDV
        {
            get { return this._AF1KDV; }
            set
            {
                this._AF1KDV = value;
                OnPropertyChanged("AF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF2KDV
        {
            get { return this._AF2KDV; }
            set
            {
                this._AF2KDV = value;
                OnPropertyChanged("AF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF3KDV
        {
            get { return this._AF3KDV; }
            set
            {
                this._AF3KDV = value;
                OnPropertyChanged("AF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF1KDV
        {
            get { return this._DovizAF1KDV; }
            set
            {
                this._DovizAF1KDV = value;
                OnPropertyChanged("DovizAF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF2KDV
        {
            get { return this._DovizAF2KDV; }
            set
            {
                this._DovizAF2KDV = value;
                OnPropertyChanged("DovizAF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF3KDV
        {
            get { return this._DovizAF3KDV; }
            set
            {
                this._DovizAF3KDV = value;
                OnPropertyChanged("DovizAF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF1Birim
        {
            get { return this._AF1Birim; }
            set
            {
                this._AF1Birim = value;
                OnPropertyChanged("AF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF2Birim
        {
            get { return this._AF2Birim; }
            set
            {
                this._AF2Birim = value;
                OnPropertyChanged("AF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF3Birim
        {
            get { return this._AF3Birim; }
            set
            {
                this._AF3Birim = value;
                OnPropertyChanged("AF3Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF1Birim
        {
            get { return this._DovizAF1Birim; }
            set
            {
                this._DovizAF1Birim = value;
                OnPropertyChanged("DovizAF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF2Birim
        {
            get { return this._DovizAF2Birim; }
            set
            {
                this._DovizAF2Birim = value;
                OnPropertyChanged("DovizAF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF3Birim
        {
            get { return this._DovizAF3Birim; }
            set
            {
                this._DovizAF3Birim = value;
                OnPropertyChanged("DovizAF3Birim");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat1
        {
            get { return this._SatisFiyat1; }
            set
            {
                this._SatisFiyat1 = value;
                OnPropertyChanged("SatisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat2
        {
            get { return this._SatisFiyat2; }
            set
            {
                this._SatisFiyat2 = value;
                OnPropertyChanged("SatisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat3
        {
            get { return this._SatisFiyat3; }
            set
            {
                this._SatisFiyat3 = value;
                OnPropertyChanged("SatisFiyat3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat4
        {
            get { return this._SatisFiyat4; }
            set
            {
                this._SatisFiyat4 = value;
                OnPropertyChanged("SatisFiyat4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat5
        {
            get { return this._SatisFiyat5; }
            set
            {
                this._SatisFiyat5 = value;
                OnPropertyChanged("SatisFiyat5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat6
        {
            get { return this._SatisFiyat6; }
            set
            {
                this._SatisFiyat6 = value;
                OnPropertyChanged("SatisFiyat6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzSatisFiyat1
        {
            get { return this._DvzSatisFiyat1; }
            set
            {
                this._DvzSatisFiyat1 = value;
                OnPropertyChanged("DvzSatisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzSatisFiyat2
        {
            get { return this._DvzSatisFiyat2; }
            set
            {
                this._DvzSatisFiyat2 = value;
                OnPropertyChanged("DvzSatisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzSatisFiyat3
        {
            get { return this._DvzSatisFiyat3; }
            set
            {
                this._DvzSatisFiyat3 = value;
                OnPropertyChanged("DvzSatisFiyat3");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF1DovizCinsi
        {
            get { return this._SF1DovizCinsi; }
            set
            {
                this._SF1DovizCinsi = value;
                OnPropertyChanged("SF1DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF2DovizCinsi
        {
            get { return this._SF2DovizCinsi; }
            set
            {
                this._SF2DovizCinsi = value;
                OnPropertyChanged("SF2DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF3DovizCinsi
        {
            get { return this._SF3DovizCinsi; }
            set
            {
                this._SF3DovizCinsi = value;
                OnPropertyChanged("SF3DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF4DovizCinsi
        {
            get { return this._SF4DovizCinsi; }
            set
            {
                this._SF4DovizCinsi = value;
                OnPropertyChanged("SF4DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF5DovizCinsi
        {
            get { return this._SF5DovizCinsi; }
            set
            {
                this._SF5DovizCinsi = value;
                OnPropertyChanged("SF5DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF6DovizCinsi
        {
            get { return this._SF6DovizCinsi; }
            set
            {
                this._SF6DovizCinsi = value;
                OnPropertyChanged("SF6DovizCinsi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF1KDV
        {
            get { return this._SF1KDV; }
            set
            {
                this._SF1KDV = value;
                OnPropertyChanged("SF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF2KDV
        {
            get { return this._SF2KDV; }
            set
            {
                this._SF2KDV = value;
                OnPropertyChanged("SF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF3KDV
        {
            get { return this._SF3KDV; }
            set
            {
                this._SF3KDV = value;
                OnPropertyChanged("SF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF4KDV
        {
            get { return this._SF4KDV; }
            set
            {
                this._SF4KDV = value;
                OnPropertyChanged("SF4KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF5KDV
        {
            get { return this._SF5KDV; }
            set
            {
                this._SF5KDV = value;
                OnPropertyChanged("SF5KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF6KDV
        {
            get { return this._SF6KDV; }
            set
            {
                this._SF6KDV = value;
                OnPropertyChanged("SF6KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF1KDV
        {
            get { return this._DovizSF1KDV; }
            set
            {
                this._DovizSF1KDV = value;
                OnPropertyChanged("DovizSF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF2KDV
        {
            get { return this._DovizSF2KDV; }
            set
            {
                this._DovizSF2KDV = value;
                OnPropertyChanged("DovizSF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF3KDV
        {
            get { return this._DovizSF3KDV; }
            set
            {
                this._DovizSF3KDV = value;
                OnPropertyChanged("DovizSF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF1Birim
        {
            get { return this._SF1Birim; }
            set
            {
                this._SF1Birim = value;
                OnPropertyChanged("SF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF2Birim
        {
            get { return this._SF2Birim; }
            set
            {
                this._SF2Birim = value;
                OnPropertyChanged("SF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF3Birim
        {
            get { return this._SF3Birim; }
            set
            {
                this._SF3Birim = value;
                OnPropertyChanged("SF3Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF4Birim
        {
            get { return this._SF4Birim; }
            set
            {
                this._SF4Birim = value;
                OnPropertyChanged("SF4Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF5Birim
        {
            get { return this._SF5Birim; }
            set
            {
                this._SF5Birim = value;
                OnPropertyChanged("SF5Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF6Birim
        {
            get { return this._SF6Birim; }
            set
            {
                this._SF6Birim = value;
                OnPropertyChanged("SF6Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF1Birim
        {
            get { return this._DovizSF1Birim; }
            set
            {
                this._DovizSF1Birim = value;
                OnPropertyChanged("DovizSF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF2Birim
        {
            get { return this._DovizSF2Birim; }
            set
            {
                this._DovizSF2Birim = value;
                OnPropertyChanged("DovizSF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF3Birim
        {
            get { return this._DovizSF3Birim; }
            set
            {
                this._DovizSF3Birim = value;
                OnPropertyChanged("DovizSF3Birim");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string FiyatListName
        {
            get { return this._FiyatListName; }
            set
            {
                this._FiyatListName = value;
                OnPropertyChanged("FiyatListName");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyatAltLimit
        {
            get { return this._SatisFiyatAltLimit; }
            set
            {
                this._SatisFiyatAltLimit = value;
                OnPropertyChanged("SatisFiyatAltLimit");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyatUstLimit
        {
            get { return this._SatisFiyatUstLimit; }
            set
            {
                this._SatisFiyatUstLimit = value;
                OnPropertyChanged("SatisFiyatUstLimit");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF1ValorGun
        {
            get { return this._SF1ValorGun; }
            set
            {
                this._SF1ValorGun = value;
                OnPropertyChanged("SF1ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF2ValorGun
        {
            get { return this._SF2ValorGun; }
            set
            {
                this._SF2ValorGun = value;
                OnPropertyChanged("SF2ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF3ValorGun
        {
            get { return this._SF3ValorGun; }
            set
            {
                this._SF3ValorGun = value;
                OnPropertyChanged("SF3ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF4ValorGun
        {
            get { return this._SF4ValorGun; }
            set
            {
                this._SF4ValorGun = value;
                OnPropertyChanged("SF4ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF5ValorGun
        {
            get { return this._SF5ValorGun; }
            set
            {
                this._SF5ValorGun = value;
                OnPropertyChanged("SF5ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF6ValorGun
        {
            get { return this._SF6ValorGun; }
            set
            {
                this._SF6ValorGun = value;
                OnPropertyChanged("SF6ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzSF1ValorGun
        {
            get { return this._DvzSF1ValorGun; }
            set
            {
                this._DvzSF1ValorGun = value;
                OnPropertyChanged("DvzSF1ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzSF2ValorGun
        {
            get { return this._DvzSF2ValorGun; }
            set
            {
                this._DvzSF2ValorGun = value;
                OnPropertyChanged("DvzSF2ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzSF3ValorGun
        {
            get { return this._DvzSF3ValorGun; }
            set
            {
                this._DvzSF3ValorGun = value;
                OnPropertyChanged("DvzSF3ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitTuru
        {
            get { return this._KayitTuru; }
            set
            {
                this._KayitTuru = value;
                OnPropertyChanged("KayitTuru");
            }
        }

        /// <summary> VARCHAR (2) * </summary>
        public string GuvenlikKod
        {
            get { return this._GuvenlikKod; }
            set
            {
                this._GuvenlikKod = value;
                OnPropertyChanged("GuvenlikKod");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Kaydeden
        {
            get { return this._Kaydeden; }
            set
            {
                this._Kaydeden = value;
                OnPropertyChanged("Kaydeden");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitTarih
        {
            get { return this._KayitTarih; }
            set
            {
                this._KayitTarih = value;
                OnPropertyChanged("KayitTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int KayitSaat
        {
            get { return this._KayitSaat; }
            set
            {
                this._KayitSaat = value;
                OnPropertyChanged("KayitSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short KayitKaynak
        {
            get { return this._KayitKaynak; }
            set
            {
                this._KayitKaynak = value;
                OnPropertyChanged("KayitKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string KayitSurum
        {
            get { return this._KayitSurum; }
            set
            {
                this._KayitSurum = value;
                OnPropertyChanged("KayitSurum");
            }
        }

        /// <summary> VARCHAR (5) * </summary>
        public string Degistiren
        {
            get { return this._Degistiren; }
            set
            {
                this._Degistiren = value;
                OnPropertyChanged("Degistiren");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisTarih
        {
            get { return this._DegisTarih; }
            set
            {
                this._DegisTarih = value;
                OnPropertyChanged("DegisTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DegisSaat
        {
            get { return this._DegisSaat; }
            set
            {
                this._DegisSaat = value;
                OnPropertyChanged("DegisSaat");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DegisKaynak
        {
            get { return this._DegisKaynak; }
            set
            {
                this._DegisKaynak = value;
                OnPropertyChanged("DegisKaynak");
            }
        }

        /// <summary> VARCHAR (12) * </summary>
        public string DegisSurum
        {
            get { return this._DegisSurum; }
            set
            {
                this._DegisSurum = value;
                OnPropertyChanged("DegisSurum");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short CheckSum
        {
            get { return this._CheckSum; }
            set
            {
                this._CheckSum = value;
                OnPropertyChanged("CheckSum");
            }
        }

        /// <summary> VARCHAR (64) * </summary>
        public string Aciklama
        {
            get { return this._Aciklama; }
            set
            {
                this._Aciklama = value;
                OnPropertyChanged("Aciklama");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod4
        {
            get { return this._Kod4; }
            set
            {
                this._Kod4 = value;
                OnPropertyChanged("Kod4");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod5
        {
            get { return this._Kod5; }
            set
            {
                this._Kod5 = value;
                OnPropertyChanged("Kod5");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod6
        {
            get { return this._Kod6; }
            set
            {
                this._Kod6 = value;
                OnPropertyChanged("Kod6");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod7
        {
            get { return this._Kod7; }
            set
            {
                this._Kod7 = value;
                OnPropertyChanged("Kod7");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod8
        {
            get { return this._Kod8; }
            set
            {
                this._Kod8 = value;
                OnPropertyChanged("Kod8");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod9
        {
            get { return this._Kod9; }
            set
            {
                this._Kod9 = value;
                OnPropertyChanged("Kod9");
            }
        }

        /// <summary> VARCHAR (20) * </summary>
        public string Kod10
        {
            get { return this._Kod10; }
            set
            {
                this._Kod10 = value;
                OnPropertyChanged("Kod10");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod11
        {
            get { return this._Kod11; }
            set
            {
                this._Kod11 = value;
                OnPropertyChanged("Kod11");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod12
        {
            get { return this._Kod12; }
            set
            {
                this._Kod12 = value;
                OnPropertyChanged("Kod12");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF1VadeTarih
        {
            get { return this._SF1VadeTarih; }
            set
            {
                this._SF1VadeTarih = value;
                OnPropertyChanged("SF1VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF2VadeTarih
        {
            get { return this._SF2VadeTarih; }
            set
            {
                this._SF2VadeTarih = value;
                OnPropertyChanged("SF2VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF3VadeTarih
        {
            get { return this._SF3VadeTarih; }
            set
            {
                this._SF3VadeTarih = value;
                OnPropertyChanged("SF3VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF4VadeTarih
        {
            get { return this._SF4VadeTarih; }
            set
            {
                this._SF4VadeTarih = value;
                OnPropertyChanged("SF4VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF5VadeTarih
        {
            get { return this._SF5VadeTarih; }
            set
            {
                this._SF5VadeTarih = value;
                OnPropertyChanged("SF5VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF6VadeTarih
        {
            get { return this._SF6VadeTarih; }
            set
            {
                this._SF6VadeTarih = value;
                OnPropertyChanged("SF6VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DvzSF1VadeTarih
        {
            get { return this._DvzSF1VadeTarih; }
            set
            {
                this._DvzSF1VadeTarih = value;
                OnPropertyChanged("DvzSF1VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DvzSF2VadeTarih
        {
            get { return this._DvzSF2VadeTarih; }
            set
            {
                this._DvzSF2VadeTarih = value;
                OnPropertyChanged("DvzSF2VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DvzSF3VadeTarih
        {
            get { return this._DvzSF3VadeTarih; }
            set
            {
                this._DvzSF3VadeTarih = value;
                OnPropertyChanged("DvzSF3VadeTarih");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short FiyatListeTipi
        {
            get { return this._FiyatListeTipi; }
            set
            {
                this._FiyatListeTipi = value;
                OnPropertyChanged("FiyatListeTipi");
            }
        }

        /// <summary> INT (4) IdentityKey * </summary>
        public int ROW_ID
        {
            get { return this._ROW_ID; }
        }

        /// <summary> TIMESTAMP (8) * </summary>
        public byte[] timestamp
        {
            get { return this._timestamp; }
            set
            {
                this._timestamp = value;
                OnPropertyChanged("timestamp");
            }
        }

        /// <summary> VARCHAR (30) PRIMARY KEY * </summary>
        public string pk_MalKodu
        {
            private get { return this._pk_MalKodu; }
            set
            {
                this._pk_MalKodu = value;
                OnPropertyChanged("pk_MalKodu");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_BasTarih
        {
            private get { return this._pk_BasTarih; }
            set
            {
                this._pk_BasTarih = value;
                OnPropertyChanged("pk_BasTarih");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_BasSaat
        {
            private get { return this._pk_BasSaat; }
            set
            {
                this._pk_BasSaat = value;
                OnPropertyChanged("pk_BasSaat");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_SiraNo
        {
            private get { return this._pk_SiraNo; }
            set
            {
                this._pk_SiraNo = value;
                OnPropertyChanged("pk_SiraNo");
            }
        }

        /// <summary> VARCHAR (16) PRIMARY KEY * </summary>
        public string pk_FiyatListNum
        {
            private get { return this._pk_FiyatListNum; }
            set
            {
                this._pk_FiyatListNum = value;
                OnPropertyChanged("pk_FiyatListNum");
            }
        }

        /// <summary> VARCHAR (20) PRIMARY KEY * </summary>
        public string pk_MusteriKodu
        {
            private get { return this._pk_MusteriKodu; }
            set
            {
                this._pk_MusteriKodu = value;
                OnPropertyChanged("pk_MusteriKodu");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_FiyatListeTipi
        {
            private get { return this._pk_FiyatListeTipi; }
            set
            {
                this._pk_FiyatListeTipi = value;
                OnPropertyChanged("pk_FiyatListeTipi");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar


        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.FYT";
        private string[] info_PrimaryKeys = { "pk_MalKodu", "pk_BasTarih", "pk_BasSaat", "pk_SiraNo", "pk_FiyatListNum", "pk_MusteriKodu", "pk_FiyatListeTipi" };
        private string[] info_IdentityKeys = { "ROW_ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public FYT()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += FYT_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void FYT_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!ChangedProperties.Contains(e.PropertyName))
            {
                ChangedProperties.Add(e.PropertyName);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion  /// Tablo Bilgileri & Metodlar

    }
    #endregion /// FYT Class

}
