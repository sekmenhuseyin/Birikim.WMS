using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
    public class KKPYNS_CAR005
    {

        #region Fields
        private int _CAR005_Row_ID;
        private Nullable<short> _CAR005_Secenek;
        private Nullable<int> _CAR005_FaturaTarihi;
        private string _CAR005_FaturaNo;
        private string _CAR005_BA;
        private Nullable<short> _CAR005_CariIslemTipi;
        private string _CAR005_SatirTipi;
        private Nullable<short> _CAR005_SatirNo;
        private string _CAR005_SatirKodu;
        private string _CAR005_Filler;
        private string _CAR005_SatirAciklama;
        private string _CAR005_CHKodu;
        private Nullable<decimal> _CAR005_Tutar;
        private Nullable<decimal> _CAR005_Oran;
        private Nullable<int> _CAR005_TevkIslemTuru;
        private string _CAR005_TevkOrani;
        private Nullable<decimal> _CAR005_TevkKDVOrani;
        private string _CAR005_EkBilgi1;
        private string _CAR005_EFaturaOTVListeNo;
        private Nullable<byte> _CAR005_EFaturaTipi;
        private Nullable<short> _CAR005_EFaturaDurumu;
        private Nullable<int> _CAR005_EFaturaDonemBas;
        private Nullable<int> _CAR005_EFaturaDonemBit;
        private Nullable<int> _CAR005_EFaturaSure;
        private Nullable<byte> _CAR005_EFaturaSureBirimi;
        private string _CAR005_EFaturaDonemAciklama;
        private string _CAR005_EFaturaNot;
        private string _CAR005_EFaturaReferansNo;
        private Nullable<byte> _CAR005_ParaBirimi;
        private string _CAR005_DovizCinsi;
        private Nullable<decimal> _CAR005_DovizKuru;
        private Nullable<decimal> _CAR005_DovizTutari;
        private string _CAR005_TeslimCHKodu;
        private Nullable<byte> _CAR005_TutarAlanDegeri;
        private string _CAR005_KDVMuafAciklama;
        private string _CAR005_EFaturaGonBirimEtiketi;
        private string _CAR005_EFaturaAliciEtiketi;
        private string _CAR005_AliciSiparisNo;
        private Nullable<int> _CAR005_AliciSiparisTarihi;
        private Nullable<byte> _CAR005_IptalDurumu;
        private Nullable<byte> _CAR005_EArsivFaturaTipi;
        private Nullable<byte> _CAR005_EArsivFaturaTeslimSekli;
        private Nullable<short> _CAR005_EArsivFaturaDurumu;

        #endregion Fields



        #region table Properties

        [DbAlan("CAR005_Row_ID", SqlDbType.Int, 4, true, true, false)]
        public int CAR005_Row_ID
        {
            get { return _CAR005_Row_ID; }
            set
            {
                if (_CAR005_Row_ID != value)
                {
                    OnPropertyChanged("CAR005_Row_ID");
                    _CAR005_Row_ID = value;
                }
            }
        }

        [DbAlan("CAR005_Secenek", SqlDbType.SmallInt, 2)]
        public Nullable<short> CAR005_Secenek
        {
            get { return _CAR005_Secenek; }
            set
            {
                if (_CAR005_Secenek != value)
                {
                    OnPropertyChanged("CAR005_Secenek");
                    _CAR005_Secenek = value;
                }
            }
        }

        [DbAlan("CAR005_FaturaTarihi", SqlDbType.Int, 4)]
        public Nullable<int> CAR005_FaturaTarihi
        {
            get { return _CAR005_FaturaTarihi; }
            set
            {
                if (_CAR005_FaturaTarihi != value)
                {
                    OnPropertyChanged("CAR005_FaturaTarihi");
                    _CAR005_FaturaTarihi = value;
                }
            }
        }

        [DbAlan("CAR005_FaturaNo", SqlDbType.NVarChar, 32)]
        public string CAR005_FaturaNo
        {
            get { return _CAR005_FaturaNo; }
            set
            {
                if (_CAR005_FaturaNo != value)
                {
                    OnPropertyChanged("CAR005_FaturaNo");
                    _CAR005_FaturaNo = value;
                }
            }
        }

        [DbAlan("CAR005_BA", SqlDbType.NVarChar, 2)]
        public string CAR005_BA
        {
            get { return _CAR005_BA; }
            set
            {
                if (_CAR005_BA != value)
                {
                    OnPropertyChanged("CAR005_BA");
                    _CAR005_BA = value;
                }
            }
        }

        [DbAlan("CAR005_CariIslemTipi", SqlDbType.SmallInt, 2)]
        public Nullable<short> CAR005_CariIslemTipi
        {
            get { return _CAR005_CariIslemTipi; }
            set
            {
                if (_CAR005_CariIslemTipi != value)
                {
                    OnPropertyChanged("CAR005_CariIslemTipi");
                    _CAR005_CariIslemTipi = value;
                }
            }
        }

        [DbAlan("CAR005_SatirTipi", SqlDbType.NVarChar, 2)]
        public string CAR005_SatirTipi
        {
            get { return _CAR005_SatirTipi; }
            set
            {
                if (_CAR005_SatirTipi != value)
                {
                    OnPropertyChanged("CAR005_SatirTipi");
                    _CAR005_SatirTipi = value;
                }
            }
        }

        [DbAlan("CAR005_SatirNo", SqlDbType.SmallInt, 2)]
        public Nullable<short> CAR005_SatirNo
        {
            get { return _CAR005_SatirNo; }
            set
            {
                if (_CAR005_SatirNo != value)
                {
                    OnPropertyChanged("CAR005_SatirNo");
                    _CAR005_SatirNo = value;
                }
            }
        }

        [DbAlan("CAR005_SatirKodu", SqlDbType.NVarChar, 40)]
        public string CAR005_SatirKodu
        {
            get { return _CAR005_SatirKodu; }
            set
            {
                if (_CAR005_SatirKodu != value)
                {
                    OnPropertyChanged("CAR005_SatirKodu");
                    _CAR005_SatirKodu = value;
                }
            }
        }

        [DbAlan("CAR005_Filler", SqlDbType.NVarChar, 2)]
        public string CAR005_Filler
        {
            get { return _CAR005_Filler; }
            set
            {
                if (_CAR005_Filler != value)
                {
                    OnPropertyChanged("CAR005_Filler");
                    _CAR005_Filler = value;
                }
            }
        }

        [DbAlan("CAR005_SatirAciklama", SqlDbType.NVarChar, 120)]
        public string CAR005_SatirAciklama
        {
            get { return _CAR005_SatirAciklama; }
            set
            {
                if (_CAR005_SatirAciklama != value)
                {
                    OnPropertyChanged("CAR005_SatirAciklama");
                    _CAR005_SatirAciklama = value;
                }
            }
        }

        [DbAlan("CAR005_CHKodu", SqlDbType.NVarChar, 32)]
        public string CAR005_CHKodu
        {
            get { return _CAR005_CHKodu; }
            set
            {
                if (_CAR005_CHKodu != value)
                {
                    OnPropertyChanged("CAR005_CHKodu");
                    _CAR005_CHKodu = value;
                }
            }
        }

        [DbAlan("CAR005_Tutar", SqlDbType.Decimal, 13)]
        public Nullable<decimal> CAR005_Tutar
        {
            get { return _CAR005_Tutar; }
            set
            {
                if (_CAR005_Tutar != value)
                {
                    OnPropertyChanged("CAR005_Tutar");
                    _CAR005_Tutar = value;
                }
            }
        }

        [DbAlan("CAR005_Oran", SqlDbType.Decimal, 5)]
        public Nullable<decimal> CAR005_Oran
        {
            get { return _CAR005_Oran; }
            set
            {
                if (_CAR005_Oran != value)
                {
                    OnPropertyChanged("CAR005_Oran");
                    _CAR005_Oran = value;
                }
            }
        }

        [DbAlan("CAR005_TevkIslemTuru", SqlDbType.Int, 4)]
        public Nullable<int> CAR005_TevkIslemTuru
        {
            get { return _CAR005_TevkIslemTuru; }
            set
            {
                if (_CAR005_TevkIslemTuru != value)
                {
                    OnPropertyChanged("CAR005_TevkIslemTuru");
                    _CAR005_TevkIslemTuru = value;
                }
            }
        }

        [DbAlan("CAR005_TevkOrani", SqlDbType.NVarChar, 14)]
        public string CAR005_TevkOrani
        {
            get { return _CAR005_TevkOrani; }
            set
            {
                if (_CAR005_TevkOrani != value)
                {
                    OnPropertyChanged("CAR005_TevkOrani");
                    _CAR005_TevkOrani = value;
                }
            }
        }

        [DbAlan("CAR005_TevkKDVOrani", SqlDbType.Decimal, 5)]
        public Nullable<decimal> CAR005_TevkKDVOrani
        {
            get { return _CAR005_TevkKDVOrani; }
            set
            {
                if (_CAR005_TevkKDVOrani != value)
                {
                    OnPropertyChanged("CAR005_TevkKDVOrani");
                    _CAR005_TevkKDVOrani = value;
                }
            }
        }

        [DbAlan("CAR005_EkBilgi1", SqlDbType.NVarChar, 20)]
        public string CAR005_EkBilgi1
        {
            get { return _CAR005_EkBilgi1; }
            set
            {
                if (_CAR005_EkBilgi1 != value)
                {
                    OnPropertyChanged("CAR005_EkBilgi1");
                    _CAR005_EkBilgi1 = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaOTVListeNo", SqlDbType.NVarChar, 4)]
        public string CAR005_EFaturaOTVListeNo
        {
            get { return _CAR005_EFaturaOTVListeNo; }
            set
            {
                if (_CAR005_EFaturaOTVListeNo != value)
                {
                    OnPropertyChanged("CAR005_EFaturaOTVListeNo");
                    _CAR005_EFaturaOTVListeNo = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaTipi", SqlDbType.TinyInt, 1)]
        public Nullable<byte> CAR005_EFaturaTipi
        {
            get { return _CAR005_EFaturaTipi; }
            set
            {
                if (_CAR005_EFaturaTipi != value)
                {
                    OnPropertyChanged("CAR005_EFaturaTipi");
                    _CAR005_EFaturaTipi = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaDurumu", SqlDbType.SmallInt, 2)]
        public Nullable<short> CAR005_EFaturaDurumu
        {
            get { return _CAR005_EFaturaDurumu; }
            set
            {
                if (_CAR005_EFaturaDurumu != value)
                {
                    OnPropertyChanged("CAR005_EFaturaDurumu");
                    _CAR005_EFaturaDurumu = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaDonemBas", SqlDbType.Int, 4)]
        public Nullable<int> CAR005_EFaturaDonemBas
        {
            get { return _CAR005_EFaturaDonemBas; }
            set
            {
                if (_CAR005_EFaturaDonemBas != value)
                {
                    OnPropertyChanged("CAR005_EFaturaDonemBas");
                    _CAR005_EFaturaDonemBas = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaDonemBit", SqlDbType.Int, 4)]
        public Nullable<int> CAR005_EFaturaDonemBit
        {
            get { return _CAR005_EFaturaDonemBit; }
            set
            {
                if (_CAR005_EFaturaDonemBit != value)
                {
                    OnPropertyChanged("CAR005_EFaturaDonemBit");
                    _CAR005_EFaturaDonemBit = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaSure", SqlDbType.Int, 4)]
        public Nullable<int> CAR005_EFaturaSure
        {
            get { return _CAR005_EFaturaSure; }
            set
            {
                if (_CAR005_EFaturaSure != value)
                {
                    OnPropertyChanged("CAR005_EFaturaSure");
                    _CAR005_EFaturaSure = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaSureBirimi", SqlDbType.TinyInt, 1)]
        public Nullable<byte> CAR005_EFaturaSureBirimi
        {
            get { return _CAR005_EFaturaSureBirimi; }
            set
            {
                if (_CAR005_EFaturaSureBirimi != value)
                {
                    OnPropertyChanged("CAR005_EFaturaSureBirimi");
                    _CAR005_EFaturaSureBirimi = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaDonemAciklama", SqlDbType.NVarChar, 128)]
        public string CAR005_EFaturaDonemAciklama
        {
            get { return _CAR005_EFaturaDonemAciklama; }
            set
            {
                if (_CAR005_EFaturaDonemAciklama != value)
                {
                    OnPropertyChanged("CAR005_EFaturaDonemAciklama");
                    _CAR005_EFaturaDonemAciklama = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaNot", SqlDbType.NVarChar, 512)]
        public string CAR005_EFaturaNot
        {
            get { return _CAR005_EFaturaNot; }
            set
            {
                if (_CAR005_EFaturaNot != value)
                {
                    OnPropertyChanged("CAR005_EFaturaNot");
                    _CAR005_EFaturaNot = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaReferansNo", SqlDbType.NVarChar, 32)]
        public string CAR005_EFaturaReferansNo
        {
            get { return _CAR005_EFaturaReferansNo; }
            set
            {
                if (_CAR005_EFaturaReferansNo != value)
                {
                    OnPropertyChanged("CAR005_EFaturaReferansNo");
                    _CAR005_EFaturaReferansNo = value;
                }
            }
        }

        [DbAlan("CAR005_ParaBirimi", SqlDbType.TinyInt, 1)]
        public Nullable<byte> CAR005_ParaBirimi
        {
            get { return _CAR005_ParaBirimi; }
            set
            {
                if (_CAR005_ParaBirimi != value)
                {
                    OnPropertyChanged("CAR005_ParaBirimi");
                    _CAR005_ParaBirimi = value;
                }
            }
        }

        [DbAlan("CAR005_DovizCinsi", SqlDbType.NVarChar, 6)]
        public string CAR005_DovizCinsi
        {
            get { return _CAR005_DovizCinsi; }
            set
            {
                if (_CAR005_DovizCinsi != value)
                {
                    OnPropertyChanged("CAR005_DovizCinsi");
                    _CAR005_DovizCinsi = value;
                }
            }
        }

        [DbAlan("CAR005_DovizKuru", SqlDbType.Decimal, 9)]
        public Nullable<decimal> CAR005_DovizKuru
        {
            get { return _CAR005_DovizKuru; }
            set
            {
                if (_CAR005_DovizKuru != value)
                {
                    OnPropertyChanged("CAR005_DovizKuru");
                    _CAR005_DovizKuru = value;
                }
            }
        }

        [DbAlan("CAR005_DovizTutari", SqlDbType.Decimal, 9)]
        public Nullable<decimal> CAR005_DovizTutari
        {
            get { return _CAR005_DovizTutari; }
            set
            {
                if (_CAR005_DovizTutari != value)
                {
                    OnPropertyChanged("CAR005_DovizTutari");
                    _CAR005_DovizTutari = value;
                }
            }
        }

        [DbAlan("CAR005_TeslimCHKodu", SqlDbType.NVarChar, 32)]
        public string CAR005_TeslimCHKodu
        {
            get { return _CAR005_TeslimCHKodu; }
            set
            {
                if (_CAR005_TeslimCHKodu != value)
                {
                    OnPropertyChanged("CAR005_TeslimCHKodu");
                    _CAR005_TeslimCHKodu = value;
                }
            }
        }

        [DbAlan("CAR005_TutarAlanDegeri", SqlDbType.TinyInt, 1)]
        public Nullable<byte> CAR005_TutarAlanDegeri
        {
            get { return _CAR005_TutarAlanDegeri; }
            set
            {
                if (_CAR005_TutarAlanDegeri != value)
                {
                    OnPropertyChanged("CAR005_TutarAlanDegeri");
                    _CAR005_TutarAlanDegeri = value;
                }
            }
        }

        [DbAlan("CAR005_KDVMuafAciklama", SqlDbType.NVarChar, 20)]
        public string CAR005_KDVMuafAciklama
        {
            get { return _CAR005_KDVMuafAciklama; }
            set
            {
                if (_CAR005_KDVMuafAciklama != value)
                {
                    OnPropertyChanged("CAR005_KDVMuafAciklama");
                    _CAR005_KDVMuafAciklama = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaGonBirimEtiketi", SqlDbType.NVarChar, 256)]
        public string CAR005_EFaturaGonBirimEtiketi
        {
            get { return _CAR005_EFaturaGonBirimEtiketi; }
            set
            {
                if (_CAR005_EFaturaGonBirimEtiketi != value)
                {
                    OnPropertyChanged("CAR005_EFaturaGonBirimEtiketi");
                    _CAR005_EFaturaGonBirimEtiketi = value;
                }
            }
        }

        [DbAlan("CAR005_EFaturaAliciEtiketi", SqlDbType.NVarChar, 256)]
        public string CAR005_EFaturaAliciEtiketi
        {
            get { return _CAR005_EFaturaAliciEtiketi; }
            set
            {
                if (_CAR005_EFaturaAliciEtiketi != value)
                {
                    OnPropertyChanged("CAR005_EFaturaAliciEtiketi");
                    _CAR005_EFaturaAliciEtiketi = value;
                }
            }
        }

        [DbAlan("CAR005_AliciSiparisNo", SqlDbType.NVarChar, 64)]
        public string CAR005_AliciSiparisNo
        {
            get { return _CAR005_AliciSiparisNo; }
            set
            {
                if (_CAR005_AliciSiparisNo != value)
                {
                    OnPropertyChanged("CAR005_AliciSiparisNo");
                    _CAR005_AliciSiparisNo = value;
                }
            }
        }

        [DbAlan("CAR005_AliciSiparisTarihi", SqlDbType.Int, 4)]
        public Nullable<int> CAR005_AliciSiparisTarihi
        {
            get { return _CAR005_AliciSiparisTarihi; }
            set
            {
                if (_CAR005_AliciSiparisTarihi != value)
                {
                    OnPropertyChanged("CAR005_AliciSiparisTarihi");
                    _CAR005_AliciSiparisTarihi = value;
                }
            }
        }

        [DbAlan("CAR005_IptalDurumu", SqlDbType.TinyInt, 1)]
        public Nullable<byte> CAR005_IptalDurumu
        {
            get { return _CAR005_IptalDurumu; }
            set
            {
                if (_CAR005_IptalDurumu != value)
                {
                    OnPropertyChanged("CAR005_IptalDurumu");
                    _CAR005_IptalDurumu = value;
                }
            }
        }

        [DbAlan("CAR005_EArsivFaturaTipi", SqlDbType.TinyInt, 1)]
        public Nullable<byte> CAR005_EArsivFaturaTipi
        {
            get { return _CAR005_EArsivFaturaTipi; }
            set
            {
                if (_CAR005_EArsivFaturaTipi != value)
                {
                    OnPropertyChanged("CAR005_EArsivFaturaTipi");
                    _CAR005_EArsivFaturaTipi = value;
                }
            }
        }

        [DbAlan("CAR005_EArsivFaturaTeslimSekli", SqlDbType.TinyInt, 1)]
        public Nullable<byte> CAR005_EArsivFaturaTeslimSekli
        {
            get { return _CAR005_EArsivFaturaTeslimSekli; }
            set
            {
                if (_CAR005_EArsivFaturaTeslimSekli != value)
                {
                    OnPropertyChanged("CAR005_EArsivFaturaTeslimSekli");
                    _CAR005_EArsivFaturaTeslimSekli = value;
                }
            }
        }

        [DbAlan("CAR005_EArsivFaturaDurumu", SqlDbType.SmallInt, 2)]
        public Nullable<short> CAR005_EArsivFaturaDurumu
        {
            get { return _CAR005_EArsivFaturaDurumu; }
            set
            {
                if (_CAR005_EArsivFaturaDurumu != value)
                {
                    OnPropertyChanged("CAR005_EArsivFaturaDurumu");
                    _CAR005_EArsivFaturaDurumu = value;
                }
            }
        }

        #endregion table Properties



        #region property changes events functions

        public event PropertyChangedEventHandler PropertyChanged;
        private List<PropertyValue> _ChangedList = new List<PropertyValue>();
        public PropertyValue[] ChangedList { get { return _ChangedList.ToArray(); } }



        internal void CAR005_PropertyChanged(object sender, PropertyChangedEventArgs e)
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


    }
}
