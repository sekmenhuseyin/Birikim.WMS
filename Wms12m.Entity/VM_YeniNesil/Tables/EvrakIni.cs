using DevHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    #region EvrakIni Class

    #region EvrakIniE Enum
    public enum EvrakIniE
    {
        ID,
        SeriNo,
        EvrakNo

    }
    #endregion /// EvrakIniE Enum

    public class EvrakIni : INotifyPropertyChanged
    {
        #region Properties
        #region Fields
        private int _ID;
        private int _SeriNo;
        private string _EvrakNo;
        private int _pk_SeriNo;
        #endregion /// Fields


        /// <summary> INT (4) IdentityKey * </summary>
        public int ID
        {
            get { return this._ID; }
        }

        /// <summary> INT (4) PrimaryKey * </summary>
        public int SeriNo
        {
            get { return this._SeriNo; }
            set
            {
                this._SeriNo = value;
                OnPropertyChanged("SeriNo");
            }
        }

        /// <summary> VARCHAR (20) PrimaryKey * </summary>
        public string EvrakNo
        {
            get { return this._EvrakNo; }
            set
            {
                this._EvrakNo = value;
                OnPropertyChanged("EvrakNo");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_SeriNo
        {
            private get { return this._pk_SeriNo; }
            set
            {
                this._pk_SeriNo = value;
                OnPropertyChanged("pk_SeriNo");
            }
        }


        #endregion /// Properties

        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(EvrakIniE Property, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(EvrakIniE), Property), Deger, And_Or));
        }

        public void WhereAdd(EvrakIniE Property, Islem islem, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(EvrakIniE), Property), islem, Deger, And_Or));
        }

        public void WhereAdd(EvrakIniE Property, Operand In_NotIn, params object[] Degerler_Parantez)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(EvrakIniE), Property), In_NotIn, Degerler_Parantez));
        }

        public void WhereAdd(params object[] Degerler)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Degerler));
        }

        /// <summary> Set işleminde Property " = " eşit ile otomatik başlar. </summary>
        public void SetAdd(EvrakIniE Property, params object[] Degerler)
        {
            SetList.Add(SqlExperOperatorIslem.SetAdd(Enum.GetName(typeof(EvrakIniE), Property), Degerler));
        }

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "YNS{0}.YNS{0}.EvrakIni";
        private string[] info_PrimaryKeys = { "pk_SeriNo" };
        private string[] info_IdentityKeys = { "ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public EvrakIni()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += EvrakIni_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void EvrakIni_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    #endregion /// EvrakIni Class  

}
