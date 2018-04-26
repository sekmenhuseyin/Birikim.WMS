using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    public class FYT : INotifyPropertyChanged
    {
        private string malKodu;
        private int basTarih;
        private int basSaat;
        private int bitTarih;
        private int bitSaat;
        private short siraNo;
        private string renkBedenKod1;
        private string renkBedenKod2;
        private string renkBedenKod3;
        private string renkBedenKod4;
        private string fiyatListNum;
        private short fiyatTuru;
        private string kod1;
        private string kod2;
        private string kod3;
        private short kodTipi;
        private string musteriKodu;
        private decimal alisFiyat1;
        private decimal alisFiyat2;
        private decimal alisFiyat3;
        private decimal dvzAlisFiyat1;
        private decimal dvzAlisFiyat2;
        private decimal dvzAlisFiyat3;
        private string aF1DovizCinsi;
        private string aF2DovizCinsi;
        private string aF3DovizCinsi;
        private short aF1KDV;
        private short aF2KDV;
        private short aF3KDV;
        private short dovizAF1KDV;
        private short dovizAF2KDV;
        private short dovizAF3KDV;
        private short aF1Birim;
        private short aF2Birim;
        private short aF3Birim;
        private short dovizAF1Birim;
        private short dovizAF2Birim;
        private short dovizAF3Birim;
        private decimal satisFiyat1;
        private decimal satisFiyat2;
        private decimal satisFiyat3;
        private decimal satisFiyat4;
        private decimal satisFiyat5;
        private decimal satisFiyat6;
        private decimal dvzSatisFiyat1;
        private decimal dvzSatisFiyat2;
        private decimal dvzSatisFiyat3;
        private string sF1DovizCinsi;
        private string sF2DovizCinsi;
        private string sF3DovizCinsi;
        private string sF4DovizCinsi;
        private string sF5DovizCinsi;
        private string sF6DovizCinsi;
        private short sF1KDV;
        private short sF2KDV;
        private short sF3KDV;
        private short sF4KDV;
        private short sF5KDV;
        private short sF6KDV;
        private short dovizSF1KDV;
        private short dovizSF2KDV;
        private short dovizSF3KDV;
        private short sF1Birim;
        private short sF2Birim;
        private short sF3Birim;
        private short sF4Birim;
        private short sF5Birim;
        private short sF6Birim;
        private short dovizSF1Birim;
        private short dovizSF2Birim;
        private short dovizSF3Birim;
        private string fiyatListName;
        private decimal satisFiyatAltLimit;
        private decimal satisFiyatUstLimit;
        private short sF1ValorGun;
        private short sF2ValorGun;
        private short sF3ValorGun;
        private short sF4ValorGun;
        private short sF5ValorGun;
        private short sF6ValorGun;
        private short dvzSF1ValorGun;
        private short dvzSF2ValorGun;
        private short dvzSF3ValorGun;
        private short kayitTuru;
        private string guvenlikKod;
        private string kaydeden;
        private int kayitTarih;
        private int kayitSaat;
        private short kayitKaynak;
        private string kayitSurum;
        private string degistiren;
        private int degisTarih;
        private int degisSaat;
        private short degisKaynak;
        private string degisSurum;
        private short checkSum;
        private string aciklama;
        private string kod4;
        private string kod5;
        private string kod6;
        private string kod7;
        private string kod8;
        private string kod9;
        private string kod10;
        private decimal kod11;
        private decimal kod12;
        private int sF1VadeTarih;
        private int sF2VadeTarih;
        private int sF3VadeTarih;
        private int sF4VadeTarih;
        private int sF5VadeTarih;
        private int sF6VadeTarih;
        private int dvzSF1VadeTarih;
        private int dvzSF2VadeTarih;
        private int dvzSF3VadeTarih;
        private short fiyatListeTipi;
        private int rOW_ID;
        private byte[] _timestamp;
        private string _pk_MalKodu;
        private int _pk_BasTarih;
        private int _pk_BasSaat;
        private short _pk_SiraNo;
        private string _pk_FiyatListNum;
        private string _pk_MusteriKodu;
        private short _pk_FiyatListeTipi;

        /// <summary> VARCHAR (30) PrimaryKey * </summary>
        public string MalKodu
        {
            get { return malKodu; }
            set
            {
                malKodu = value;
                OnPropertyChanged("MalKodu");
            }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
        public int BasTarih
        {
            get { return basTarih; }
            set
            {
                basTarih = value;
                OnPropertyChanged("BasTarih");
            }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
        public int BasSaat
        {
            get { return basSaat; }
            set
            {
                basSaat = value;
                OnPropertyChanged("BasSaat");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitTarih
        {
            get { return bitTarih; }
            set
            {
                bitTarih = value;
                OnPropertyChanged("BitTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int BitSaat
        {
            get { return bitSaat; }
            set
            {
                bitSaat = value;
                OnPropertyChanged("BitSaat");
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

        /// <summary> VARCHAR (16) PrimaryKey * </summary>
        public string FiyatListNum
        {
            get { return fiyatListNum; }
            set
            {
                fiyatListNum = value;
                OnPropertyChanged("FiyatListNum");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short FiyatTuru
        {
            get { return fiyatTuru; }
            set
            {
                fiyatTuru = value;
                OnPropertyChanged("FiyatTuru");
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

        /// <summary> SMALLINT (2) * </summary>
        public short KodTipi
        {
            get { return kodTipi; }
            set
            {
                kodTipi = value;
                OnPropertyChanged("KodTipi");
            }
        }

        /// <summary> VARCHAR (20) PrimaryKey * </summary>
        public string MusteriKodu
        {
            get { return musteriKodu; }
            set
            {
                musteriKodu = value;
                OnPropertyChanged("MusteriKodu");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal AlisFiyat1
        {
            get { return alisFiyat1; }
            set
            {
                alisFiyat1 = value;
                OnPropertyChanged("AlisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal AlisFiyat2
        {
            get { return alisFiyat2; }
            set
            {
                alisFiyat2 = value;
                OnPropertyChanged("AlisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal AlisFiyat3
        {
            get { return alisFiyat3; }
            set
            {
                alisFiyat3 = value;
                OnPropertyChanged("AlisFiyat3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzAlisFiyat1
        {
            get { return dvzAlisFiyat1; }
            set
            {
                dvzAlisFiyat1 = value;
                OnPropertyChanged("DvzAlisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzAlisFiyat2
        {
            get { return dvzAlisFiyat2; }
            set
            {
                dvzAlisFiyat2 = value;
                OnPropertyChanged("DvzAlisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzAlisFiyat3
        {
            get { return dvzAlisFiyat3; }
            set
            {
                dvzAlisFiyat3 = value;
                OnPropertyChanged("DvzAlisFiyat3");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string AF1DovizCinsi
        {
            get { return aF1DovizCinsi; }
            set
            {
                aF1DovizCinsi = value;
                OnPropertyChanged("AF1DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string AF2DovizCinsi
        {
            get { return aF2DovizCinsi; }
            set
            {
                aF2DovizCinsi = value;
                OnPropertyChanged("AF2DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string AF3DovizCinsi
        {
            get { return aF3DovizCinsi; }
            set
            {
                aF3DovizCinsi = value;
                OnPropertyChanged("AF3DovizCinsi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF1KDV
        {
            get { return aF1KDV; }
            set
            {
                aF1KDV = value;
                OnPropertyChanged("AF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF2KDV
        {
            get { return aF2KDV; }
            set
            {
                aF2KDV = value;
                OnPropertyChanged("AF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF3KDV
        {
            get { return aF3KDV; }
            set
            {
                aF3KDV = value;
                OnPropertyChanged("AF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF1KDV
        {
            get { return dovizAF1KDV; }
            set
            {
                dovizAF1KDV = value;
                OnPropertyChanged("DovizAF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF2KDV
        {
            get { return dovizAF2KDV; }
            set
            {
                dovizAF2KDV = value;
                OnPropertyChanged("DovizAF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF3KDV
        {
            get { return dovizAF3KDV; }
            set
            {
                dovizAF3KDV = value;
                OnPropertyChanged("DovizAF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF1Birim
        {
            get { return aF1Birim; }
            set
            {
                aF1Birim = value;
                OnPropertyChanged("AF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF2Birim
        {
            get { return aF2Birim; }
            set
            {
                aF2Birim = value;
                OnPropertyChanged("AF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short AF3Birim
        {
            get { return aF3Birim; }
            set
            {
                aF3Birim = value;
                OnPropertyChanged("AF3Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF1Birim
        {
            get { return dovizAF1Birim; }
            set
            {
                dovizAF1Birim = value;
                OnPropertyChanged("DovizAF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF2Birim
        {
            get { return dovizAF2Birim; }
            set
            {
                dovizAF2Birim = value;
                OnPropertyChanged("DovizAF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizAF3Birim
        {
            get { return dovizAF3Birim; }
            set
            {
                dovizAF3Birim = value;
                OnPropertyChanged("DovizAF3Birim");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat1
        {
            get { return satisFiyat1; }
            set
            {
                satisFiyat1 = value;
                OnPropertyChanged("SatisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat2
        {
            get { return satisFiyat2; }
            set
            {
                satisFiyat2 = value;
                OnPropertyChanged("SatisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat3
        {
            get { return satisFiyat3; }
            set
            {
                satisFiyat3 = value;
                OnPropertyChanged("SatisFiyat3");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat4
        {
            get { return satisFiyat4; }
            set
            {
                satisFiyat4 = value;
                OnPropertyChanged("SatisFiyat4");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat5
        {
            get { return satisFiyat5; }
            set
            {
                satisFiyat5 = value;
                OnPropertyChanged("SatisFiyat5");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyat6
        {
            get { return satisFiyat6; }
            set
            {
                satisFiyat6 = value;
                OnPropertyChanged("SatisFiyat6");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzSatisFiyat1
        {
            get { return dvzSatisFiyat1; }
            set
            {
                dvzSatisFiyat1 = value;
                OnPropertyChanged("DvzSatisFiyat1");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzSatisFiyat2
        {
            get { return dvzSatisFiyat2; }
            set
            {
                dvzSatisFiyat2 = value;
                OnPropertyChanged("DvzSatisFiyat2");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal DvzSatisFiyat3
        {
            get { return dvzSatisFiyat3; }
            set
            {
                dvzSatisFiyat3 = value;
                OnPropertyChanged("DvzSatisFiyat3");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF1DovizCinsi
        {
            get { return sF1DovizCinsi; }
            set
            {
                sF1DovizCinsi = value;
                OnPropertyChanged("SF1DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF2DovizCinsi
        {
            get { return sF2DovizCinsi; }
            set
            {
                sF2DovizCinsi = value;
                OnPropertyChanged("SF2DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF3DovizCinsi
        {
            get { return sF3DovizCinsi; }
            set
            {
                sF3DovizCinsi = value;
                OnPropertyChanged("SF3DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF4DovizCinsi
        {
            get { return sF4DovizCinsi; }
            set
            {
                sF4DovizCinsi = value;
                OnPropertyChanged("SF4DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF5DovizCinsi
        {
            get { return sF5DovizCinsi; }
            set
            {
                sF5DovizCinsi = value;
                OnPropertyChanged("SF5DovizCinsi");
            }
        }

        /// <summary> VARCHAR (3) * </summary>
        public string SF6DovizCinsi
        {
            get { return sF6DovizCinsi; }
            set
            {
                sF6DovizCinsi = value;
                OnPropertyChanged("SF6DovizCinsi");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF1KDV
        {
            get { return sF1KDV; }
            set
            {
                sF1KDV = value;
                OnPropertyChanged("SF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF2KDV
        {
            get { return sF2KDV; }
            set
            {
                sF2KDV = value;
                OnPropertyChanged("SF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF3KDV
        {
            get { return sF3KDV; }
            set
            {
                sF3KDV = value;
                OnPropertyChanged("SF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF4KDV
        {
            get { return sF4KDV; }
            set
            {
                sF4KDV = value;
                OnPropertyChanged("SF4KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF5KDV
        {
            get { return sF5KDV; }
            set
            {
                sF5KDV = value;
                OnPropertyChanged("SF5KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF6KDV
        {
            get { return sF6KDV; }
            set
            {
                sF6KDV = value;
                OnPropertyChanged("SF6KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF1KDV
        {
            get { return dovizSF1KDV; }
            set
            {
                dovizSF1KDV = value;
                OnPropertyChanged("DovizSF1KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF2KDV
        {
            get { return dovizSF2KDV; }
            set
            {
                dovizSF2KDV = value;
                OnPropertyChanged("DovizSF2KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF3KDV
        {
            get { return dovizSF3KDV; }
            set
            {
                dovizSF3KDV = value;
                OnPropertyChanged("DovizSF3KDV");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF1Birim
        {
            get { return sF1Birim; }
            set
            {
                sF1Birim = value;
                OnPropertyChanged("SF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF2Birim
        {
            get { return sF2Birim; }
            set
            {
                sF2Birim = value;
                OnPropertyChanged("SF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF3Birim
        {
            get { return sF3Birim; }
            set
            {
                sF3Birim = value;
                OnPropertyChanged("SF3Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF4Birim
        {
            get { return sF4Birim; }
            set
            {
                sF4Birim = value;
                OnPropertyChanged("SF4Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF5Birim
        {
            get { return sF5Birim; }
            set
            {
                sF5Birim = value;
                OnPropertyChanged("SF5Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF6Birim
        {
            get { return sF6Birim; }
            set
            {
                sF6Birim = value;
                OnPropertyChanged("SF6Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF1Birim
        {
            get { return dovizSF1Birim; }
            set
            {
                dovizSF1Birim = value;
                OnPropertyChanged("DovizSF1Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF2Birim
        {
            get { return dovizSF2Birim; }
            set
            {
                dovizSF2Birim = value;
                OnPropertyChanged("DovizSF2Birim");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DovizSF3Birim
        {
            get { return dovizSF3Birim; }
            set
            {
                dovizSF3Birim = value;
                OnPropertyChanged("DovizSF3Birim");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string FiyatListName
        {
            get { return fiyatListName; }
            set
            {
                fiyatListName = value;
                OnPropertyChanged("FiyatListName");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyatAltLimit
        {
            get { return satisFiyatAltLimit; }
            set
            {
                satisFiyatAltLimit = value;
                OnPropertyChanged("SatisFiyatAltLimit");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SatisFiyatUstLimit
        {
            get { return satisFiyatUstLimit; }
            set
            {
                satisFiyatUstLimit = value;
                OnPropertyChanged("SatisFiyatUstLimit");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF1ValorGun
        {
            get { return sF1ValorGun; }
            set
            {
                sF1ValorGun = value;
                OnPropertyChanged("SF1ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF2ValorGun
        {
            get { return sF2ValorGun; }
            set
            {
                sF2ValorGun = value;
                OnPropertyChanged("SF2ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF3ValorGun
        {
            get { return sF3ValorGun; }
            set
            {
                sF3ValorGun = value;
                OnPropertyChanged("SF3ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF4ValorGun
        {
            get { return sF4ValorGun; }
            set
            {
                sF4ValorGun = value;
                OnPropertyChanged("SF4ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF5ValorGun
        {
            get { return sF5ValorGun; }
            set
            {
                sF5ValorGun = value;
                OnPropertyChanged("SF5ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short SF6ValorGun
        {
            get { return sF6ValorGun; }
            set
            {
                sF6ValorGun = value;
                OnPropertyChanged("SF6ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzSF1ValorGun
        {
            get { return dvzSF1ValorGun; }
            set
            {
                dvzSF1ValorGun = value;
                OnPropertyChanged("DvzSF1ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzSF2ValorGun
        {
            get { return dvzSF2ValorGun; }
            set
            {
                dvzSF2ValorGun = value;
                OnPropertyChanged("DvzSF2ValorGun");
            }
        }

        /// <summary> SMALLINT (2) * </summary>
        public short DvzSF3ValorGun
        {
            get { return dvzSF3ValorGun; }
            set
            {
                dvzSF3ValorGun = value;
                OnPropertyChanged("DvzSF3ValorGun");
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

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod11
        {
            get { return kod11; }
            set
            {
                kod11 = value;
                OnPropertyChanged("Kod11");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Kod12
        {
            get { return kod12; }
            set
            {
                kod12 = value;
                OnPropertyChanged("Kod12");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF1VadeTarih
        {
            get { return sF1VadeTarih; }
            set
            {
                sF1VadeTarih = value;
                OnPropertyChanged("SF1VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF2VadeTarih
        {
            get { return sF2VadeTarih; }
            set
            {
                sF2VadeTarih = value;
                OnPropertyChanged("SF2VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF3VadeTarih
        {
            get { return sF3VadeTarih; }
            set
            {
                sF3VadeTarih = value;
                OnPropertyChanged("SF3VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF4VadeTarih
        {
            get { return sF4VadeTarih; }
            set
            {
                sF4VadeTarih = value;
                OnPropertyChanged("SF4VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF5VadeTarih
        {
            get { return sF5VadeTarih; }
            set
            {
                sF5VadeTarih = value;
                OnPropertyChanged("SF5VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int SF6VadeTarih
        {
            get { return sF6VadeTarih; }
            set
            {
                sF6VadeTarih = value;
                OnPropertyChanged("SF6VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DvzSF1VadeTarih
        {
            get { return dvzSF1VadeTarih; }
            set
            {
                dvzSF1VadeTarih = value;
                OnPropertyChanged("DvzSF1VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DvzSF2VadeTarih
        {
            get { return dvzSF2VadeTarih; }
            set
            {
                dvzSF2VadeTarih = value;
                OnPropertyChanged("DvzSF2VadeTarih");
            }
        }

        /// <summary> INT (4) * </summary>
        public int DvzSF3VadeTarih
        {
            get { return dvzSF3VadeTarih; }
            set
            {
                dvzSF3VadeTarih = value;
                OnPropertyChanged("DvzSF3VadeTarih");
            }
        }

        /// <summary> SMALLINT (2) PrimaryKey * </summary>
        public short FiyatListeTipi
        {
            get { return fiyatListeTipi; }
            set
            {
                fiyatListeTipi = value;
                OnPropertyChanged("FiyatListeTipi");
            }
        }

        /// <summary> INT (4) IdentityKey * </summary>
        public int ROW_ID
        {
            get { return rOW_ID; }
            set
            {
                rOW_ID = value;
                OnPropertyChanged("ROW_ID");
            }
        }

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

        /// <summary> VARCHAR (30) PRIMARY KEY * </summary>
        public string pk_MalKodu
        {
            private get { return _pk_MalKodu; }
            set
            {
                _pk_MalKodu = value;
                OnPropertyChanged("pk_MalKodu");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_BasTarih
        {
            private get { return _pk_BasTarih; }
            set
            {
                _pk_BasTarih = value;
                OnPropertyChanged("pk_BasTarih");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_BasSaat
        {
            private get { return _pk_BasSaat; }
            set
            {
                _pk_BasSaat = value;
                OnPropertyChanged("pk_BasSaat");
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

        /// <summary> VARCHAR (16) PRIMARY KEY * </summary>
        public string pk_FiyatListNum
        {
            private get { return _pk_FiyatListNum; }
            set
            {
                _pk_FiyatListNum = value;
                OnPropertyChanged("pk_FiyatListNum");
            }
        }

        /// <summary> VARCHAR (20) PRIMARY KEY * </summary>
        public string pk_MusteriKodu
        {
            private get { return _pk_MusteriKodu; }
            set
            {
                _pk_MusteriKodu = value;
                OnPropertyChanged("pk_MusteriKodu");
            }
        }

        /// <summary> SMALLINT (2) PRIMARY KEY * </summary>
        public short pk_FiyatListeTipi
        {
            private get { return _pk_FiyatListeTipi; }
            set
            {
                _pk_FiyatListeTipi = value;
                OnPropertyChanged("pk_FiyatListeTipi");
            }
        }

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
            PropertyChanged += FYT_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        private void FYT_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    }
}