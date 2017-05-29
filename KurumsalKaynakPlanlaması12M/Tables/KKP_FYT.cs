using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{
     [DbTablo("FINSAT6", "FINSAT6", "FYT")]
   public class KKP_FYT : INotifyPropertyChanged
   {

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

       [DbAlan("BasTarih", SqlDbType.Int, 4, false, true, false)]
       public int BasTarih
       {
           get { return _BasTarih; }
           set
           {
               if (_BasTarih != value)
               {
                   OnPropertyChanged("BasTarih");
                   _BasTarih = value;
               }
           }
       }

       [DbAlan("BasSaat", SqlDbType.Int, 4, false, true, false)]
       public int BasSaat
       {
           get { return _BasSaat; }
           set
           {
               if (_BasSaat != value)
               {
                   OnPropertyChanged("BasSaat");
                   _BasSaat = value;
               }
           }
       }

       [DbAlan("BitTarih", SqlDbType.Int, 4)]
       public int BitTarih
       {
           get { return _BitTarih; }
           set
           {
               if (_BitTarih != value)
               {
                   OnPropertyChanged("BitTarih");
                   _BitTarih = value;
               }
           }
       }

       [DbAlan("BitSaat", SqlDbType.Int, 4)]
       public int BitSaat
       {
           get { return _BitSaat; }
           set
           {
               if (_BitSaat != value)
               {
                   OnPropertyChanged("BitSaat");
                   _BitSaat = value;
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

       [DbAlan("FiyatListNum", SqlDbType.VarChar, 16, false, true, false)]
       public string FiyatListNum
       {
           get { return _FiyatListNum; }
           set
           {
               if (_FiyatListNum != value)
               {
                   OnPropertyChanged("FiyatListNum");
                   _FiyatListNum = value;
               }
           }
       }

       /// <summary>
       ///0-Kodu Belirtilen Müşterilere Uygulansın, 1-Tüm Müşterilere Uygulansın
       /// </summary>
       [DbAlan("FiyatTuru", SqlDbType.SmallInt, 2)]
       public short FiyatTuru
       {
           get { return _FiyatTuru; }
           set
           {
               if (_FiyatTuru != value)
               {
                   OnPropertyChanged("FiyatTuru");
                   _FiyatTuru = value;
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

       /// <summary>
       ///  0-CHKodu, 1-GrupKodu, 2-ÖzelKod, 3-TipKodu, 4-BölgeKodu, 5-GüvenlikKodu, 6-Kod1, 7-Kod2, 8-Kod3, 9-Kod4, 10-Kod7, 11-Kod8, 12-Kod9, 13-MuhasebeKodu, 14-FaturaCHK 
       /// </summary>
       [DbAlan("KodTipi", SqlDbType.SmallInt, 2)]
       public short KodTipi
       {
           get { return _KodTipi; }
           set
           {
               if (_KodTipi != value)
               {
                   OnPropertyChanged("KodTipi");
                   _KodTipi = value;
               }
           }
       }

       [DbAlan("MusteriKodu", SqlDbType.VarChar, 20, false, true, false)]
       public string MusteriKodu
       {
           get { return _MusteriKodu; }
           set
           {
               if (_MusteriKodu != value)
               {
                   OnPropertyChanged("MusteriKodu");
                   _MusteriKodu = value;
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

       [DbAlan("DvzAlisFiyat1", SqlDbType.Decimal, 13)]
       public decimal DvzAlisFiyat1
       {
           get { return _DvzAlisFiyat1; }
           set
           {
               if (_DvzAlisFiyat1 != value)
               {
                   OnPropertyChanged("DvzAlisFiyat1");
                   _DvzAlisFiyat1 = value;
               }
           }
       }

       [DbAlan("DvzAlisFiyat2", SqlDbType.Decimal, 13)]
       public decimal DvzAlisFiyat2
       {
           get { return _DvzAlisFiyat2; }
           set
           {
               if (_DvzAlisFiyat2 != value)
               {
                   OnPropertyChanged("DvzAlisFiyat2");
                   _DvzAlisFiyat2 = value;
               }
           }
       }

       [DbAlan("DvzAlisFiyat3", SqlDbType.Decimal, 13)]
       public decimal DvzAlisFiyat3
       {
           get { return _DvzAlisFiyat3; }
           set
           {
               if (_DvzAlisFiyat3 != value)
               {
                   OnPropertyChanged("DvzAlisFiyat3");
                   _DvzAlisFiyat3 = value;
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

       [DbAlan("DvzSatisFiyat1", SqlDbType.Decimal, 13)]
       public decimal DvzSatisFiyat1
       {
           get { return _DvzSatisFiyat1; }
           set
           {
               if (_DvzSatisFiyat1 != value)
               {
                   OnPropertyChanged("DvzSatisFiyat1");
                   _DvzSatisFiyat1 = value;
               }
           }
       }

       [DbAlan("DvzSatisFiyat2", SqlDbType.Decimal, 13)]
       public decimal DvzSatisFiyat2
       {
           get { return _DvzSatisFiyat2; }
           set
           {
               if (_DvzSatisFiyat2 != value)
               {
                   OnPropertyChanged("DvzSatisFiyat2");
                   _DvzSatisFiyat2 = value;
               }
           }
       }

       [DbAlan("DvzSatisFiyat3", SqlDbType.Decimal, 13)]
       public decimal DvzSatisFiyat3
       {
           get { return _DvzSatisFiyat3; }
           set
           {
               if (_DvzSatisFiyat3 != value)
               {
                   OnPropertyChanged("DvzSatisFiyat3");
                   _DvzSatisFiyat3 = value;
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

       [DbAlan("FiyatListName", SqlDbType.VarChar, 30)]
       public string FiyatListName
       {
           get { return _FiyatListName; }
           set
           {
               if (_FiyatListName != value)
               {
                   OnPropertyChanged("FiyatListName");
                   _FiyatListName = value;
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

       [DbAlan("DvzSF1ValorGun", SqlDbType.SmallInt, 2)]
       public short DvzSF1ValorGun
       {
           get { return _DvzSF1ValorGun; }
           set
           {
               if (_DvzSF1ValorGun != value)
               {
                   OnPropertyChanged("DvzSF1ValorGun");
                   _DvzSF1ValorGun = value;
               }
           }
       }

       [DbAlan("DvzSF2ValorGun", SqlDbType.SmallInt, 2)]
       public short DvzSF2ValorGun
       {
           get { return _DvzSF2ValorGun; }
           set
           {
               if (_DvzSF2ValorGun != value)
               {
                   OnPropertyChanged("DvzSF2ValorGun");
                   _DvzSF2ValorGun = value;
               }
           }
       }

       [DbAlan("DvzSF3ValorGun", SqlDbType.SmallInt, 2)]
       public short DvzSF3ValorGun
       {
           get { return _DvzSF3ValorGun; }
           set
           {
               if (_DvzSF3ValorGun != value)
               {
                   OnPropertyChanged("DvzSF3ValorGun");
                   _DvzSF3ValorGun = value;
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

       [DbAlan("Kod5", SqlDbType.VarChar, 10)]
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

       [DbAlan("Kod11", SqlDbType.Decimal, 13)]
       public decimal Kod11
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



       internal void FYT_PropertyChanged(object sender, PropertyChangedEventArgs e)
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



       public KKP_FYT()
       {
           ///this.PropertyChanged += this.FYT_PropertyChanged;
       }
   }
}
