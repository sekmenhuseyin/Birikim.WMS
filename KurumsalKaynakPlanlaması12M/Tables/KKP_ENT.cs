using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
    [DbTablo("MUHASEBE6","MUHASEBE6","ENT")]
    public class KKP_ENT
    {
        #region Fields
        private short _IslemTip;
        private short _IslemTur;
        private string _EvrakNo;
        private int _Tarih;
        private string _HesapKodu;
        private short _KynkEvrakTip;
        private short _SiraNo;
        private short _TabloID;
        private string _Bordro;
        private short _BA;
        private short _FisTip;
        private int _FisTarih;
        private decimal _FisNo;
        private int _FisMaddeNo;
        private short _FisSiraNo;
        private string _FisMhsKod;
        private string _FisMmkKod;
        private short _EntYontem;
        private short _EntTabloNo;
        private short _EntTuru;
        private short _EntBHesap;
        private string _Kod1;
        private string _Kod2;
        private string _Kod3;
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
        private short _EntSekli;
        private short _YevmiyeEvrakTipi;
        private string _EvrakTipAciklama;
        private string _OdemeTipi;
        private string _BelgeNo;
        private int _BelgeTarih;
        private int _Row_ID;

        #endregion Fields



        #region table Properties

        [DbAlan("IslemTip", SqlDbType.SmallInt, 2, false, true, false)]
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

        [DbAlan("TabloID", SqlDbType.SmallInt, 2, false, true, false)]
        public short TabloID
        {
            get { return _TabloID; }
            set
            {
                if (_TabloID != value)
                {
                    OnPropertyChanged("TabloID");
                    _TabloID = value;
                }
            }
        }

        [DbAlan("Bordro", SqlDbType.VarChar, 16)]
        public string Bordro
        {
            get { return _Bordro; }
            set
            {
                if (_Bordro != value)
                {
                    OnPropertyChanged("Bordro");
                    _Bordro = value;
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

        [DbAlan("FisTip", SqlDbType.SmallInt, 2, false, true, false)]
        public short FisTip
        {
            get { return _FisTip; }
            set
            {
                if (_FisTip != value)
                {
                    OnPropertyChanged("FisTip");
                    _FisTip = value;
                }
            }
        }

        [DbAlan("FisTarih", SqlDbType.Int, 4, false, true, false)]
        public int FisTarih
        {
            get { return _FisTarih; }
            set
            {
                if (_FisTarih != value)
                {
                    OnPropertyChanged("FisTarih");
                    _FisTarih = value;
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

        [DbAlan("FisMaddeNo", SqlDbType.Int, 4, false, true, false)]
        public int FisMaddeNo
        {
            get { return _FisMaddeNo; }
            set
            {
                if (_FisMaddeNo != value)
                {
                    OnPropertyChanged("FisMaddeNo");
                    _FisMaddeNo = value;
                }
            }
        }

        [DbAlan("FisSiraNo", SqlDbType.SmallInt, 2, false, true, false)]
        public short FisSiraNo
        {
            get { return _FisSiraNo; }
            set
            {
                if (_FisSiraNo != value)
                {
                    OnPropertyChanged("FisSiraNo");
                    _FisSiraNo = value;
                }
            }
        }

        [DbAlan("FisMhsKod", SqlDbType.VarChar, 50)]
        public string FisMhsKod
        {
            get { return _FisMhsKod; }
            set
            {
                if (_FisMhsKod != value)
                {
                    OnPropertyChanged("FisMhsKod");
                    _FisMhsKod = value;
                }
            }
        }

        [DbAlan("FisMmkKod", SqlDbType.VarChar, 20)]
        public string FisMmkKod
        {
            get { return _FisMmkKod; }
            set
            {
                if (_FisMmkKod != value)
                {
                    OnPropertyChanged("FisMmkKod");
                    _FisMmkKod = value;
                }
            }
        }

        [DbAlan("EntYontem", SqlDbType.SmallInt, 2)]
        public short EntYontem
        {
            get { return _EntYontem; }
            set
            {
                if (_EntYontem != value)
                {
                    OnPropertyChanged("EntYontem");
                    _EntYontem = value;
                }
            }
        }

        [DbAlan("EntTabloNo", SqlDbType.SmallInt, 2)]
        public short EntTabloNo
        {
            get { return _EntTabloNo; }
            set
            {
                if (_EntTabloNo != value)
                {
                    OnPropertyChanged("EntTabloNo");
                    _EntTabloNo = value;
                }
            }
        }

        [DbAlan("EntTuru", SqlDbType.SmallInt, 2)]
        public short EntTuru
        {
            get { return _EntTuru; }
            set
            {
                if (_EntTuru != value)
                {
                    OnPropertyChanged("EntTuru");
                    _EntTuru = value;
                }
            }
        }

        [DbAlan("EntBHesap", SqlDbType.SmallInt, 2)]
        public short EntBHesap
        {
            get { return _EntBHesap; }
            set
            {
                if (_EntBHesap != value)
                {
                    OnPropertyChanged("EntBHesap");
                    _EntBHesap = value;
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

        [DbAlan("EntSekli", SqlDbType.SmallInt, 2, false, true, false)]
        public short EntSekli
        {
            get { return _EntSekli; }
            set
            {
                if (_EntSekli != value)
                {
                    OnPropertyChanged("EntSekli");
                    _EntSekli = value;
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

        [DbAlan("BelgeNo", SqlDbType.VarChar, 32)]
        public string BelgeNo
        {
            get { return _BelgeNo; }
            set
            {
                if (_BelgeNo != value)
                {
                    OnPropertyChanged("BelgeNo");
                    _BelgeNo = value;
                }
            }
        }

        [DbAlan("BelgeTarih", SqlDbType.Int, 4)]
        public int BelgeTarih
        {
            get { return _BelgeTarih; }
            set
            {
                if (_BelgeTarih != value)
                {
                    OnPropertyChanged("BelgeTarih");
                    _BelgeTarih = value;
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



        internal void ENT_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        public KKP_ENT Copy()
        {
            return (KKP_ENT)this.MemberwiseClone();
        }

        #endregion property changes events functions

        public KKP_ENT()
        {

        }

    }
}
