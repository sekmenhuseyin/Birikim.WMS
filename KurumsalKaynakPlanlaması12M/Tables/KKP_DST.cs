using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace KurumsalKaynakPlanlaması12M
{
     [DbTablo("FINSAT6", "FINSAT6", "DST")]
    public class KKP_DST : INotifyPropertyChanged
    {

        #region Fields
        private string _MalKodu;
        private string _Depo;
        private decimal _KritikStok;
        private decimal _AzamiStok;
        private decimal _DvrMiktar;
        private decimal _DvrTutar;
        private int _DvrTarih;
        private decimal _GirMiktar;
        private decimal _GirTutar;
        private decimal _GirIskonto;
        private decimal _DvzGirTutar;
        private decimal _DvzGirIskonto;
        private int _SonGirTarih;
        private decimal _CikMiktar;
        private decimal _CikTutar;
        private decimal _CikIskonto;
        private decimal _DvzCikTutar;
        private decimal _DvzCikIskonto;
        private int _SonCikTarih;
        private int _SonSayimTarih;
        private decimal _SonSayimFarki;
        private decimal _SatSiparis;
        private decimal _AlSiparis;
        private decimal _SatRezervasyon;
        private decimal _AlRezervasyon;
        private decimal _KonsGirMiktar;
        private decimal _KonsCikMiktar;
        private short _SonMlySekli;
        private int _SonMlyTarih;
        private decimal _SonMlyBirimFiyat;
        private decimal _Miktar2;
        private decimal _Tutar2;
        private decimal _BlkMiktar;
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
        private short _BakGostSekli;
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
        private decimal _TahminiStok;
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

        [DbAlan("Depo", SqlDbType.VarChar, 5, false, true, false)]
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

        [DbAlan("DvzGirIskonto", SqlDbType.Decimal, 13)]
        public decimal DvzGirIskonto
        {
            get { return _DvzGirIskonto; }
            set
            {
                if (_DvzGirIskonto != value)
                {
                    OnPropertyChanged("DvzGirIskonto");
                    _DvzGirIskonto = value;
                }
            }
        }

        [DbAlan("SonGirTarih", SqlDbType.Int, 4)]
        public int SonGirTarih
        {
            get { return _SonGirTarih; }
            set
            {
                if (_SonGirTarih != value)
                {
                    OnPropertyChanged("SonGirTarih");
                    _SonGirTarih = value;
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

        [DbAlan("DvzCikIskonto", SqlDbType.Decimal, 13)]
        public decimal DvzCikIskonto
        {
            get { return _DvzCikIskonto; }
            set
            {
                if (_DvzCikIskonto != value)
                {
                    OnPropertyChanged("DvzCikIskonto");
                    _DvzCikIskonto = value;
                }
            }
        }

        [DbAlan("SonCikTarih", SqlDbType.Int, 4)]
        public int SonCikTarih
        {
            get { return _SonCikTarih; }
            set
            {
                if (_SonCikTarih != value)
                {
                    OnPropertyChanged("SonCikTarih");
                    _SonCikTarih = value;
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

        [DbAlan("SonSayimFarki", SqlDbType.Decimal, 13)]
        public decimal SonSayimFarki
        {
            get { return _SonSayimFarki; }
            set
            {
                if (_SonSayimFarki != value)
                {
                    OnPropertyChanged("SonSayimFarki");
                    _SonSayimFarki = value;
                }
            }
        }

        [DbAlan("SatSiparis", SqlDbType.Decimal, 13)]
        public decimal SatSiparis
        {
            get { return _SatSiparis; }
            set
            {
                if (_SatSiparis != value)
                {
                    OnPropertyChanged("SatSiparis");
                    _SatSiparis = value;
                }
            }
        }

        [DbAlan("AlSiparis", SqlDbType.Decimal, 13)]
        public decimal AlSiparis
        {
            get { return _AlSiparis; }
            set
            {
                if (_AlSiparis != value)
                {
                    OnPropertyChanged("AlSiparis");
                    _AlSiparis = value;
                }
            }
        }

        [DbAlan("SatRezervasyon", SqlDbType.Decimal, 13)]
        public decimal SatRezervasyon
        {
            get { return _SatRezervasyon; }
            set
            {
                if (_SatRezervasyon != value)
                {
                    OnPropertyChanged("SatRezervasyon");
                    _SatRezervasyon = value;
                }
            }
        }

        [DbAlan("AlRezervasyon", SqlDbType.Decimal, 13)]
        public decimal AlRezervasyon
        {
            get { return _AlRezervasyon; }
            set
            {
                if (_AlRezervasyon != value)
                {
                    OnPropertyChanged("AlRezervasyon");
                    _AlRezervasyon = value;
                }
            }
        }

        [DbAlan("KonsGirMiktar", SqlDbType.Decimal, 13)]
        public decimal KonsGirMiktar
        {
            get { return _KonsGirMiktar; }
            set
            {
                if (_KonsGirMiktar != value)
                {
                    OnPropertyChanged("KonsGirMiktar");
                    _KonsGirMiktar = value;
                }
            }
        }

        [DbAlan("KonsCikMiktar", SqlDbType.Decimal, 13)]
        public decimal KonsCikMiktar
        {
            get { return _KonsCikMiktar; }
            set
            {
                if (_KonsCikMiktar != value)
                {
                    OnPropertyChanged("KonsCikMiktar");
                    _KonsCikMiktar = value;
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



        internal void DST_PropertyChanged(object sender, PropertyChangedEventArgs e)
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


        public KKP_DST()
        {
            ///this.PropertyChanged += this.DST_PropertyChanged;
        }
    }
}
