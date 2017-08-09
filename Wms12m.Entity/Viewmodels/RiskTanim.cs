using KurumsalKaynakPlanlaması12M;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    public class GenelAyarVeParams
    {
        public int ID { get; set; }
        public short Tip { get; set; }
        public short Tip2 { get; set; }
        public string TalebiGiren { get; set; }
        public string Talep2KademeOnaylayan { get; set; }
        public string Talep1KademeOnaylayan { get; set; }
        public string SiparisSorumlu { get; set; }
        public byte[] SiparisSorumluImza { get; set; }
        public decimal? SiparisGMYOnayLimit { get; set; }
        public string Satinalmaci { get; set; }
        public bool? SatinalmaciAdmin { get; set; }
        public string MailExp { get; set; }
        public string MailTo { get; set; }
        public string MailCc { get; set; }
        public int? OngoruStokAsgariFaktor { get; set; }
        public int? OngoruStokAzamiFaktor { get; set; }
        public string OzelParca { get; set; }
        public string DosyaTipi { get; set; }
        public string DosyaYolu { get; set; }
        public string Kaydeden { get; set; }
        public DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public DateTime DegisTarih { get; set; }
        public string TipAck { get; set; }
        public string Tip2Ack { get; set; }
        public int pk_ID { get; set; }
    }

    #region RiskTanim Class 
    public class RiskTanimToplu
    {
        public bool? Onay { get; set; }

        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SahsiCekLimiti { get; set; }

        public decimal MusteriCekLimiti { get; set; }

        public decimal? YeniSahsiCekLimiti { get; set; }

        public decimal? YeniMusteriCekLimiti { get; set; }
    }
    #region RiskTanimE Enum 
    public enum RiskTanimE
    {
        ID,
        HesapKodu,
        Unvan,
        SahsiCekLimiti,
        MusteriCekLimiti,
        SMOnay,
        SMOnaylayan,
        SMOnayTarih,
        SPGMYOnay,
        SPGMYOnaylayan,
        SPGMYOnayTarih,
        MIGMYOnay,
        MIGMYOnaylayan,
        MIGMYOnayTarih,
        GMOnay,
        GMOnaylayan,
        GMOnayTarih,
        OnayTip,
        Durum

    }
    #endregion /// RiskTanimE Enum   

    public class RiskTanim : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private int _ID;
        private string _HesapKodu;
        private string _Unvan;
        private decimal _SahsiCekLimiti;
        private decimal _MusteriCekLimiti;
        private bool? _SMOnay;
        private string _SMOnaylayan;
        private DateTime? _SMOnayTarih;
        private bool? _SPGMYOnay;
        private string _SPGMYOnaylayan;
        private DateTime? _SPGMYOnayTarih;
        private bool? _MIGMYOnay;
        private string _MIGMYOnaylayan;
        private DateTime? _MIGMYOnayTarih;
        private bool? _GMOnay;
        private string _GMOnaylayan;
        private DateTime? _GMOnayTarih;
        private short? _OnayTip;
        private bool? _Durum;
        private int _pk_ID;
        #endregion /// Fields


        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int ID
        {
            get { return this._ID; }
            set
            {
                this._ID = value;
                //OnPropertyChanged("HesapKodu");
            }

        }

        /// <summary> VARCHAR (30) * </summary>
        public string HesapKodu
        {
            get { return this._HesapKodu; }
            set
            {
                this._HesapKodu = value;
                OnPropertyChanged("HesapKodu");
            }
        }

        /// <summary> VARCHAR (100) Allow Null </summary>
        public string Unvan
        {
            get { return this._Unvan; }
            set
            {
                this._Unvan = value;
                OnPropertyChanged("Unvan");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal SahsiCekLimiti
        {
            get { return this._SahsiCekLimiti; }
            set
            {
                this._SahsiCekLimiti = value;
                OnPropertyChanged("SahsiCekLimiti");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal MusteriCekLimiti
        {
            get { return this._MusteriCekLimiti; }
            set
            {
                this._MusteriCekLimiti = value;
                OnPropertyChanged("MusteriCekLimiti");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? SMOnay
        {
            get { return this._SMOnay; }
            set
            {
                this._SMOnay = value;
                OnPropertyChanged("SMOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string SMOnaylayan
        {
            get { return this._SMOnaylayan; }
            set
            {
                this._SMOnaylayan = value;
                OnPropertyChanged("SMOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? SMOnayTarih
        {
            get { return this._SMOnayTarih; }
            set
            {
                this._SMOnayTarih = value;
                OnPropertyChanged("SMOnayTarih");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? SPGMYOnay
        {
            get { return this._SPGMYOnay; }
            set
            {
                this._SPGMYOnay = value;
                OnPropertyChanged("SPGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string SPGMYOnaylayan
        {
            get { return this._SPGMYOnaylayan; }
            set
            {
                this._SPGMYOnaylayan = value;
                OnPropertyChanged("SPGMYOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? SPGMYOnayTarih
        {
            get { return this._SPGMYOnayTarih; }
            set
            {
                this._SPGMYOnayTarih = value;
                OnPropertyChanged("SPGMYOnayTarih");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? MIGMYOnay
        {
            get { return this._MIGMYOnay; }
            set
            {
                this._MIGMYOnay = value;
                OnPropertyChanged("MIGMYOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string MIGMYOnaylayan
        {
            get { return this._MIGMYOnaylayan; }
            set
            {
                this._MIGMYOnaylayan = value;
                OnPropertyChanged("MIGMYOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? MIGMYOnayTarih
        {
            get { return this._MIGMYOnayTarih; }
            set
            {
                this._MIGMYOnayTarih = value;
                OnPropertyChanged("MIGMYOnayTarih");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? GMOnay
        {
            get { return this._GMOnay; }
            set
            {
                this._GMOnay = value;
                OnPropertyChanged("GMOnay");
            }
        }

        /// <summary> VARCHAR (50) Allow Null </summary>
        public string GMOnaylayan
        {
            get { return this._GMOnaylayan; }
            set
            {
                this._GMOnaylayan = value;
                OnPropertyChanged("GMOnaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? GMOnayTarih
        {
            get { return this._GMOnayTarih; }
            set
            {
                this._GMOnayTarih = value;
                OnPropertyChanged("GMOnayTarih");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? OnayTip
        {
            get { return this._OnayTip; }
            set
            {
                this._OnayTip = value;
                OnPropertyChanged("OnayTip");
            }
        }

        /// <summary> BIT (1) Allow Null </summary>
        public bool? Durum
        {
            get { return this._Durum; }
            set
            {
                this._Durum = value;
                OnPropertyChanged("Durum");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_ID
        {
            private get { return this._pk_ID; }
            set
            {
                this._pk_ID = value;
                OnPropertyChanged("pk_ID");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.RiskTanim";
        private string[] info_PrimaryKeys = { "pk_ID" };
        private string[] info_IdentityKeys = { "ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public RiskTanim()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += RiskTanim_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void RiskTanim_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    #endregion /// RiskTanim Class


    #region Teminat Class 

    #region TeminatE Enum 
    public enum TeminatE
    {
        ID,
        HesapKodu,
        Unvan,
        AltBayi,
        Cins,
        Tutar,
        SureliSuresiz,
        Tarih,
        VadeTarih,
        Onay,
        Onaylayan,
        OnayTarih

    }
    #endregion /// TeminatE Enum           

    public class Teminat : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private int _ID;
        private string _HesapKodu;
        private string _Unvan;
        private string _AltBayi;
        private string _Cins;
        private decimal _Tutar;
        private bool _SureliSuresiz;
        private DateTime _Tarih;
        private DateTime? _VadeTarih;
        private bool _Onay;
        private string _Onaylayan;
        private DateTime? _OnayTarih;
        private int _pk_ID;
        #endregion /// Fields


        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int ID
        {
            get { return this._ID; }
            set
            {
                this._ID = value;
                OnPropertyChanged("ID");
            }
        }

        /// <summary> VARCHAR (30) * </summary>
        public string HesapKodu
        {
            get { return this._HesapKodu; }
            set
            {
                this._HesapKodu = value;
                OnPropertyChanged("HesapKodu");
            }
        }

        /// <summary> VARCHAR (100) * </summary>
        public string Unvan
        {
            get { return this._Unvan; }
            set
            {
                this._Unvan = value;
                OnPropertyChanged("Unvan");
            }
        }

        /// <summary> VARCHAR (-1) * </summary>
        public string AltBayi
        {
            get { return this._AltBayi; }
            set
            {
                this._AltBayi = value;
                OnPropertyChanged("AltBayi");
            }
        }

        /// <summary> VARCHAR (50) * </summary>
        public string Cins
        {
            get { return this._Cins; }
            set
            {
                this._Cins = value;
                OnPropertyChanged("Cins");
            }
        }

        /// <summary> NUMERIC (13) * </summary>
        public decimal Tutar
        {
            get { return this._Tutar; }
            set
            {
                this._Tutar = value;
                OnPropertyChanged("Tutar");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool SureliSuresiz
        {
            get { return this._SureliSuresiz; }
            set
            {
                this._SureliSuresiz = value;
                OnPropertyChanged("SureliSuresiz");
            }
        }

        /// <summary> SMALLDATETIME (4) * </summary>
        public DateTime Tarih
        {
            get { return this._Tarih; }
            set
            {
                this._Tarih = value;
                OnPropertyChanged("Tarih");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? VadeTarih
        {
            get { return this._VadeTarih; }
            set
            {
                this._VadeTarih = value;
                OnPropertyChanged("VadeTarih");
            }
        }

        /// <summary> BIT (1) * </summary>
        public bool Onay
        {
            get { return this._Onay; }
            set
            {
                this._Onay = value;
                OnPropertyChanged("Onay");
            }
        }

        /// <summary> VARCHAR (5) Allow Null </summary>
        public string Onaylayan
        {
            get { return this._Onaylayan; }
            set
            {
                this._Onaylayan = value;
                OnPropertyChanged("Onaylayan");
            }
        }

        /// <summary> SMALLDATETIME (4) Allow Null </summary>
        public DateTime? OnayTarih
        {
            get { return this._OnayTarih; }
            set
            {
                this._OnayTarih = value;
                OnPropertyChanged("OnayTarih");
            }
        }

        /// <summary> INT (4) PRIMARY KEY * </summary>
        public int pk_ID
        {
            private get { return this._pk_ID; }
            set
            {
                this._pk_ID = value;
                OnPropertyChanged("pk_ID");
            }
        }
        #endregion /// Properties       

        #region Tablo Bilgileri & Metodlar

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "FINSAT6{0}.FINSAT6{0}.Teminat";
        private string[] info_PrimaryKeys = { "pk_ID" };
        private string[] info_IdentityKeys = { "ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public Teminat()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += Teminat_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void Teminat_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
    #endregion /// Teminat Class

    #region Fiyat
    public class ListeNoSelect
    {
        public string FiyatListNum { get; set; }

        public string FiyatListName { get; set; }
    }

    public class FiyatUrunGrupSelect
    {
        public string MalKodu { get; set; }

        public string MalAdi { get; set; }

        public string Birim1 { get; set; }

        public string Birim2 { get; set; }

        public string Birim3 { get; set; }

        public string GrupKod { get; set; }
    }

    public class FiyatListSelect
    {
        public System.Nullable<bool> Onay { get; set; }

        public int ROW_ID { get; set; }

        public string MalKodu { get; set; }

        public string MalAdi { get; set; }

        public string GrupKod { get; set; }

        public string MusteriKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SatisFiyat1 { get; set; }

        public string SF1Birim { get; set; }

        public string DovizSF1Birim { get; set; }

        public decimal DvzSatisFiyat1 { get; set; }

        public string SF1DovizCinsi { get; set; }

        public string Birim1 { get; set; }

        public string Birim2 { get; set; }

        public string Birim3 { get; set; }
    }
    public class BekleyenFiyatListesi
    {
        public int ID { get; set; }

        public string FiyatListNum { get; set; }

        public string MalKodu { get; set; }

        public string MalAdi { get; set; }

        public string HesapKodu { get; set; }

        public string Unvan { get; set; }

        public decimal SatisFiyat1 { get; set; }

        public string SatisFiyat1Birim { get; set; }

        public int SatisFiyat1BirimInt { get; set; }

        public decimal DovizSatisFiyat1 { get; set; }

        public string DovizSF1Birim { get; set; }

        public int DovizSF1BirimInt { get; set; }

        public string DovizCinsi { get; set; }

        public int ROW_ID { get; set; }

        public bool Onay { get; set; }

        public string Durum { get; set; }

        public string HangiOnayda { get; set; }
    }
    public class SozlesmeCariBilgileri
    {
        /// <summary> Decimal(36,6) (Allow Null) </summary>
        public decimal? Bakiye { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? ToplamBakiye { get; set; }
        /// <summary> Decimal(25,6) (Not Null) </summary>
        public decimal KrediLimiti { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string AlinanBordroOrtalamaVade { get; set; }
        /// <summary> VarChar(15) (Allow Null) </summary>
        public string OrtalamaVade { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? BekleyenSiparisTutar { get; set; }
        /// <summary> Decimal(38,6) (Allow Null) </summary>
        public decimal? AlinanBordro { get; set; }
    }

    #endregion

    #region Sozlesmeler
    public class SozlesmeListesi
    {
        public string ListeNo { get; set; }

        public string MusteriKod { get; set; }

        public string Unvan { get; set; }

        public string OnayMerci { get; set; }
    }

    public class UrunGrup
    {
        public string Kod { get; set; }

        public string Aciklama { get; set; }
    }
    #endregion

    #region Satınalma
    public class SatinAlmaGMOnayList
    {
        public int SipTalepNo { get; set; }

        public string HesapKodu { get; set; }

        public string Unvan { get; set; }
    }
    public class SatTalep
    {

        public string TalepNo { get; set; }
        public string MalKodu { get; set; }
        public DateTime Tarih { get; set; }
        public short Tip { get; set; }
        public string Birim { get; set; }
        public decimal BirimMiktar { get; set; }
        public double? Miktar { get; set; }
        public int? Operator { get; set; }
        public double? Katsayi { get; set; }
        public DateTime IstenenTarih { get; set; }
        public string Aciklama { get; set; }
        public string Aciklama2 { get; set; }
        public string Aciklama3 { get; set; }
        public short Durum { get; set; }
        public string EkDosya { get; set; }
        public string Kademe1Onaylayan { get; set; }
        public DateTime? Kademe1OnayTarih { get; set; }
        public string Kademe2Onaylayan { get; set; }
        public DateTime? Kademe2OnayTarih { get; set; }
        public string Satinalmaci { get; set; }
        public int? TeklifNo { get; set; }
        public string HesapKodu { get; set; }
        public decimal? BirimFiyat { get; set; }
        public short? DvzTL { get; set; }
        public string DvzCinsi { get; set; }
        public decimal? DvzKuru { get; set; }
        public Single KDVOran { get; set; }
        public int? SipTalepNo { get; set; }
        public short? SipIslemTip { get; set; }
        public string GMYMaliOnaylayan { get; set; }
        public DateTime? GMYMaliOnayTarih { get; set; }
        public string Kaydeden { get; set; }
        public string TesisKodu { get; set; }
        public string MalAdi { get; set; }
        public short? TeklifVade { get; set; }
        public string TeklifAciklamasi { get; set; }
        public short? FTDDovizTL { get; set; }
        public string FTDDovizCinsi { get; set; }
        public decimal? FTDDovizKuru { get; set; }


        public int ID { get; set; }
        public Nullable<decimal> TeslimMiktar { get; set; }
        public Nullable<decimal> KapatilanMiktar { get; set; }
        public Nullable<decimal> KapatilanMiktar_Onceki { get; set; }

        public string GMOnaylayan { get; set; }
        public Nullable<DateTime> GMOnayTarih { get; set; }
        public string SipEvrakNo { get; set; }
        public Nullable<DateTime> SipTerminTarih { get; set; }
        public string SirketKodu { get; set; }

        public string Unvan { get; set; }

        public DateTime KayitTarih { get; set; }
        public string Degistiren { get; set; }
        public DateTime DegisTarih { get; set; }
        public bool Gizle { get; set; }

        public string TesisAdi { get; set; }

        public string TipStr
        {
            get
            {
                if (Tip == 0)
                    return "MTO";
                else if (Tip == 1)
                    return "MTS";
                return "";
            }
            private set { }
        }
        public string DvzTLStr
        {
            get
            {
                if (DvzTL == 0)
                    return "Yerel Para";
                else if (DvzTL == 1)
                    return "Döviz";
                return "";
            }
            private set { }
        }
        public string SipIslemTipStr
        {
            get
            {
                if (SipIslemTip == (short)KKPIslemTipSPI.İçPiyasa)
                    return "İç Piyasa";
                else if (SipIslemTip == (short)KKPIslemTipSPI.DışPiyasa)
                    return "Dış Piyasa";
                return "";
            }
            private set { }
        }


        public Nullable<decimal> DvzTutar
        {
            get
            {
                if (BirimFiyat == null)
                    return null;

                if (DvzTL == 1)
                    return BirimMiktar * (decimal)BirimFiyat;
                return null;
            }
            private set { }
        }

        public Nullable<decimal> Tutar
        {
            get
            {
                if (BirimFiyat == null)
                    return null;

                if (DvzTL == 0)
                    return BirimMiktar * (decimal)BirimFiyat;
                else if (DvzTL == 1 && DvzKuru != null && DvzKuru > 0 && DvzTutar != null)
                    return (decimal)DvzTutar * (decimal)DvzKuru;
                return null;
            }
            private set { }
        }

    }

    #region TeklifDurum Enum
    enum TeklifDurum
    {
        Giris = 0,
        Fiyat = 1,
        TedarikciSecimiYapildi = 2,
        GMYOrmanElenen = 3,
        GMYOrmanOnayli = 4,
        GMYMaliElenen = 5,
        GMYMaliOnayli = 6
    }
    #endregion

    public class SatTeklif
    {
        public int ID { get; set; }

        public int TeklifNo { get; set; }

        public System.DateTime Tarih { get; set; }

        public string HesapKodu { get; set; }

        public string MalKodu { get; set; }

        public string Birim { get; set; }

        public decimal BirimFiyat { get; set; }

        public decimal TeklifMiktar { get; set; }

        public short Durum { get; set; }

        public short DvzTL { get; set; }

        public string DvzCinsi { get; set; }

        public int TerminSure { get; set; }

        public decimal MinSipMiktar { get; set; }

        public System.Nullable<System.DateTime> TeklifBasTarih { get; set; }

        public System.Nullable<System.DateTime> TeklifBitTarih { get; set; }

        public System.Nullable<short> Vade { get; set; }

        public string TeslimYeri { get; set; }

        public string TeklifDosya { get; set; }

        public bool OneriDurum { get; set; }

        public string Aciklama { get; set; }

        public string Aciklama2 { get; set; }

        public string Aciklama3 { get; set; }

        public string Satinalmaci { get; set; }

        public string Kademe2Onaylayan { get; set; }

        public System.Nullable<System.DateTime> Kademe2OnayTarih { get; set; }

        public string Kademe1Onaylayan { get; set; }

        public System.Nullable<System.DateTime> Kademe1OnayTarih { get; set; }

        public short Durum2 { get; set; }

        public string KynkTalepNo { get; set; }

        public string KynkTalepEkDosya { get; set; }

        public string Kaydeden { get; set; }

        public System.DateTime KayitTarih { get; set; }

        public string KayitSirKodu { get; set; }

        public string Degistiren { get; set; }

        public System.DateTime DegisTarih { get; set; }

        public string DegisSirKodu { get; set; }

        public string TeklifAciklamasi { get; set; }

        public string Unvan { get; set; }

        public string MalAdi { get; set; }

        public string DvzTLStr
        {
            get
            {
                if (DvzTL == 0)
                    return "Yerel Para";
                else if (DvzTL == 1)
                    return "Döviz";
                return "";
            }
            set { }
        }
        public string DurumStr
        {
            get
            {
                if (Durum == (short)TeklifDurum.Giris)
                    return "Teklif Giriş";
                else if (Durum == (short)TeklifDurum.Fiyat)
                    return "Teklif Fiyat";
                else if (Durum == (short)TeklifDurum.TedarikciSecimiYapildi)
                    return "Tedarikçi Seçimi Yapıldı";
                else if (Durum == (short)TeklifDurum.GMYOrmanElenen)
                    return "GMY Tedarikçi Elenen";
                else if (Durum == (short)TeklifDurum.GMYOrmanOnayli)
                    return "GMY Tedarikçi Onay";
                else if (Durum == (short)TeklifDurum.GMYMaliElenen)
                    return "GMY Mali Elenen";
                else if (Durum == (short)TeklifDurum.GMYMaliOnayli)
                    return "Onaylı";
                return "";
            }
            set { }
        }
    }

    public class SatOnayliTed
    {
        public int ID { get; set; }

        public int TeklifNo { get; set; }

        public string HesapKodu { get; set; }

        public string MalKodu { get; set; }

        public short SiraNo { get; set; }

        public string Kaydeden { get; set; }

        public System.DateTime KayitTarih { get; set; }

        public string KayitSirKodu { get; set; }

        public string Degistiren { get; set; }

        public System.DateTime DegisTarih { get; set; }

        public string DegisSirKodu { get; set; }

        //public SatTeklif  TeklifInfo { get; set; }



        public string Unvan1 { get; set; }
        public string Birim { get; set; }
        public decimal BirimFiyat { get; set; }
        public short DvzTL { get; set; }
        public string DvzCinsi { get; set; }
        public int TerminSure { get; set; }
        public decimal MinSipMiktar { get; set; }
        public System.Nullable<System.DateTime> TeklifBasTarih { get; set; }
        public System.Nullable<System.DateTime> TeklifBitTarih { get; set; }
        public string MalAdi { get; set; }
    }

    public class SonAlimListesi
    {
        public string EvrakNo { get; set; }

        public int Tarih { get; set; }

        public string Chk { get; set; }

        public string MalKodu { get; set; }

        public string Birim { get; set; }

        public decimal BirimMiktar { get; set; }

        public decimal BirimFiyat { get; set; }

        public decimal Tutar { get; set; }

        public decimal DvzBirimFiyat { get; set; }

        public decimal DovizKuru { get; set; }

        public decimal DovizTutar { get; set; }

        public string Unvan { get; set; }

        public string MalAdi { get; set; }

        public DateTime Tarih_D
        {
            get { return new DateTime(1900, 1, 1).AddDays(Tarih - 2); }
            set { }
        }
    }

    public class MMK
    {
        public string HesapKod { get; set; }
        public string HesapAd { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", HesapKod, HesapAd);
        }
    }
    public class SPI
    {
        public short IslemTur { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string Chk { get; set; }
        public short SiraNo { get; set; }
        public short IslemTip { get; set; }
        public string MalKodu { get; set; }
        public short KynkEvrakTip { get; set; }
        public decimal Miktar { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar { get; set; }
        public string DovizCinsi { get; set; }
        public decimal DovizKuru { get; set; }
        public decimal DovizTutar { get; set; }
        public decimal DvzBirimFiyat { get; set; }
        public string Birim { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal BirimMiktar { get; set; }
        public decimal Iskonto { get; set; }
        public double IskontoOran { get; set; }
        public decimal ToplamIskonto { get; set; }
        public decimal KDV { get; set; }
        public double KDVOran { get; set; }
        public short KDVDahilHaric { get; set; }
        public string Aciklama { get; set; }
        public string Kod1 { get; set; }
        public string Kod2 { get; set; }
        public string Kod3 { get; set; }
        public string Kod4 { get; set; }
        public string Kod5 { get; set; }
        public string Kod6 { get; set; }
        public string Kod7 { get; set; }
        public string Kod8 { get; set; }
        public string Kod9 { get; set; }
        public string Kod10 { get; set; }
        public short Kod11 { get; set; }
        public short Kod12 { get; set; }
        public decimal Kod13 { get; set; }
        public decimal Kod14 { get; set; }
        public int EvrakTarih { get; set; }
        public decimal Miktar2 { get; set; }
        public decimal Tutar2 { get; set; }
        public int Tarih2 { get; set; }
        public int VadeTarih { get; set; }
        public string Depo { get; set; }
        public string Vasita { get; set; }
        public string SeriNo { get; set; }
        public string IrsaliyeNo { get; set; }
        public int IrsaliyeTarih { get; set; }
        public decimal PromosyonMiktar { get; set; }
        public string Aciklama2 { get; set; }
        public string AsilEvrakNo { get; set; }
        public decimal Masraf { get; set; }
        public decimal TeslimMiktar { get; set; }
        public int TahTeslimTarih { get; set; }
        public int SonTeslimTarih { get; set; }
        public short SiparisDurumu { get; set; }
        public string RezervasyonEvrakNo { get; set; }
        public int RezervasyonTarihi { get; set; }
        public decimal KapatilanMiktar { get; set; }
        public double IskontoOran1 { get; set; }
        public short IskOran1Net { get; set; }
        public double IskontoOran2 { get; set; }
        public short IskOran2Net { get; set; }
        public double IskontoOran3 { get; set; }
        public short IskOran3Net { get; set; }
        public double IskontoOran4 { get; set; }
        public short IskOran4Net { get; set; }
        public double IskontoOran5 { get; set; }
        public short IskOran5Net { get; set; }
        public decimal KlmTutarIsk { get; set; }
        public short KlmTutarIskNet { get; set; }
        public string TeslimChk { get; set; }
        public string ButceKod { get; set; }
        public string FytListeNo { get; set; }
        public string MasrafMerkez { get; set; }
        public short DvzTL { get; set; }
        public string RenkBedenKod1 { get; set; }
        public string RenkBedenKod2 { get; set; }
        public string RenkBedenKod3 { get; set; }
        public string RenkBedenKod4 { get; set; }
        public string BarkodNo { get; set; }
        public double Katsayi { get; set; }
        public short Operator { get; set; }
        public short ValorGun { get; set; }
        public short KayitTuru { get; set; }
        public string Nesne1 { get; set; }
        public string Nesne2 { get; set; }
        public string Nesne3 { get; set; }
        public string TesTemMalKod { get; set; }
        public decimal Miktar3 { get; set; }
        public decimal Tutar3 { get; set; }
        public short SiraNo2 { get; set; }
        public decimal BlkMiktar { get; set; }
        public int BlkTarih { get; set; }
        public short BlkDurumu { get; set; }
        public int KurTarihi { get; set; }
        public short AnaEvrakTip { get; set; }
        public string GuvenlikKod { get; set; }
        public string Kaydeden { get; set; }
        public int KayitTarih { get; set; }
        public int KayitSaat { get; set; }
        public short KayitKaynak { get; set; }
        public string KayitSurum { get; set; }
        public string Degistiren { get; set; }
        public int DegisTarih { get; set; }
        public int DegisSaat { get; set; }
        public short DegisKaynak { get; set; }
        public string DegisSurum { get; set; }
        public short CheckSum { get; set; }
        public string TeklifEvrakNo { get; set; }
        public int TeklifTarihi { get; set; }
        public decimal OnayMiktar { get; set; }
        public int SonKullanimTarihi { get; set; }
        public short DvzKurCinsi { get; set; }
        public string TevfikatOran { get; set; }
        public decimal TevfikatTutar { get; set; }
        public int Tarih3 { get; set; }
        public int Tarih4 { get; set; }
        public int Tarih5 { get; set; }
        public int Tarih6 { get; set; }
        public string TevfikatOranKod { get; set; }
        public string ProjeKodu { get; set; }
        public decimal IskontoTutar { get; set; }
        public decimal IskontoTutar1 { get; set; }
        public decimal IskontoTutar2 { get; set; }
        public decimal IskontoTutar3 { get; set; }
        public decimal IskontoTutar4 { get; set; }
        public decimal IskontoTutar5 { get; set; }
        public string Not1 { get; set; }
        public string Not2 { get; set; }
        public string Not3 { get; set; }
        public string Not4 { get; set; }
        public string Not5 { get; set; }
        public string TeklifAciklamasi { get; set; }
        public int ROW_ID { get; set; }
    }
    #endregion

    #region Cek
    public class CekOnayDetay
    {
        public string EvrakNo { get; set; }

        public System.Nullable<System.DateTime> Tarih { get; set; }

        public string Veren { get; set; }

        public string VerenUnvan { get; set; }

        public string Borclu { get; set; }

        public string BorcluUnvan { get; set; }

        public decimal Tutar { get; set; }

        public System.Nullable<System.DateTime> VadeTarih { get; set; }

        public string SonPozisyon { get; set; }
    }
    #endregion
}
