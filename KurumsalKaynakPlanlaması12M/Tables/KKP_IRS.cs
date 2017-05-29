using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
    [DbTablo("FINSAT6", "FINSAT6", "IRS")]
    public class KKP_IRS : INotifyPropertyChanged
    {

        #region Fields
        private short _IslemTur;
        private string _EvrakNo;
        private int _Tarih;
        private string _Chk;
        private short _KynkEvrakTip;
        private short _SiraNo;
        private short _IslemTip;
        private string _MalKodu;
        private decimal _Miktar;
        private short _IslemTur2;
        private string _EvrakNo2;
        private int _Tarih2;
        private string _Chk2;
        private short _KynkEvrakTip2;
        private short _SiraNo2;
        private short _IslemTip2;
        private short _IslemDurum;
        private short _KayitTuru;
        private short _BlkDurumu;
        private int _BlkTarih;
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
        private string _Depo;
        private string _Depo2;
        private int _Row_ID;

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

        [DbAlan("IslemTur2", SqlDbType.SmallInt, 2)]
        public short IslemTur2
        {
            get { return _IslemTur2; }
            set
            {
                if (_IslemTur2 != value)
                {
                    OnPropertyChanged("IslemTur2");
                    _IslemTur2 = value;
                }
            }
        }

        [DbAlan("EvrakNo2", SqlDbType.VarChar, 16, false, true, false)]
        public string EvrakNo2
        {
            get { return _EvrakNo2; }
            set
            {
                if (_EvrakNo2 != value)
                {
                    OnPropertyChanged("EvrakNo2");
                    _EvrakNo2 = value;
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

        [DbAlan("Chk2", SqlDbType.VarChar, 20)]
        public string Chk2
        {
            get { return _Chk2; }
            set
            {
                if (_Chk2 != value)
                {
                    OnPropertyChanged("Chk2");
                    _Chk2 = value;
                }
            }
        }

        [DbAlan("KynkEvrakTip2", SqlDbType.SmallInt, 2)]
        public short KynkEvrakTip2
        {
            get { return _KynkEvrakTip2; }
            set
            {
                if (_KynkEvrakTip2 != value)
                {
                    OnPropertyChanged("KynkEvrakTip2");
                    _KynkEvrakTip2 = value;
                }
            }
        }

        [DbAlan("SiraNo2", SqlDbType.SmallInt, 2, false, true, false)]
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

        [DbAlan("IslemTip2", SqlDbType.SmallInt, 2)]
        public short IslemTip2
        {
            get { return _IslemTip2; }
            set
            {
                if (_IslemTip2 != value)
                {
                    OnPropertyChanged("IslemTip2");
                    _IslemTip2 = value;
                }
            }
        }

        [DbAlan("IslemDurum", SqlDbType.SmallInt, 2)]
        public short IslemDurum
        {
            get { return _IslemDurum; }
            set
            {
                if (_IslemDurum != value)
                {
                    OnPropertyChanged("IslemDurum");
                    _IslemDurum = value;
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

        [DbAlan("Depo2", SqlDbType.VarChar, 5)]
        public string Depo2
        {
            get { return _Depo2; }
            set
            {
                if (_Depo2 != value)
                {
                    OnPropertyChanged("Depo2");
                    _Depo2 = value;
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



        internal void IRS_PropertyChanged(object sender, PropertyChangedEventArgs e)
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

        #endregion property changes events functions


        public KKP_IRS()
        {

        }
    }
}
