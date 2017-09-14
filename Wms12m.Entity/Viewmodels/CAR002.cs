using DevHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Wms12m.Entity
{
    #region CAR002E Enum 
    public enum CAR002E
    {
        CAR002_Row_ID,
        CAR002_HesapKodu,
        CAR002_Unvan1,
        CAR002_Unvan2,
        CAR002_Adres1,
        CAR002_Adres2,
        CAR002_Adres3,
        CAR002_VergiDairesi,
        CAR002_VergiHesapNo,
        CAR002_Telefon1,
        CAR002_BolgeKodu,
        CAR002_GrupKodu,
        CAR002_MuhasebeKodu,
        CAR002_IskontoOrani,
        CAR002_OpsiyonGunu,
        CAR002_DevirTarihi,
        CAR002_DevirBorc,
        CAR002_DevirAlacak,
        CAR002_DigerBorc,
        CAR002_DigerAlacak,
        CAR002_OdemeBorc,
        CAR002_OdemeAlacak,
        CAR002_CiroBorc,
        CAR002_CiroAlacak,
        CAR002_IadeBorc,
        CAR002_IadeAlacak,
        CAR002_KDVBorc,
        CAR002_KDVAlacak,
        CAR002_otvborc,
        CAR002_otvalacak,
        CAR002_GirenKaynak,
        CAR002_GirenTarih,
        CAR002_GirenSaat,
        CAR002_GirenKodu,
        CAR002_GirenSurum,
        CAR002_DegistirenKaynak,
        CAR002_DegistirenTarih,
        CAR002_DegistirenSaat,
        CAR002_DegistirenKodu,
        CAR002_DegistirenSurum,
        CAR002_Telefon2,
        CAR002_Telefon3,
        CAR002_Telefon4,
        CAR002_Fax,
        CAR002_EMailAdresi,
        CAR002_InternetAdresi,
        CAR002_OzelKodu,
        CAR002_TipKodu,
        CAR002_Kod1,
        CAR002_Kod2,
        CAR002_Kod3,
        CAR002_Kod4,
        CAR002_Kod5,
        CAR002_Kod6,
        CAR002_Kod7,
        CAR002_Kod8,
        CAR002_Kod9,
        CAR002_UygulanacakFiyat,
        CAR002_UygulanacakBankaKodu,
        CAR002_UygulanacakFiyatTipi,
        CAR002_UygulananFiyat,
        CAR002_UygulananBankaKodu,
        CAR002_UygulananFiyatTipi,
        CAR002_Yetkili1,
        CAR002_Yetkili1Gorevi,
        CAR002_Yetkili1DahiliNo,
        CAR002_Yetkili1EMail,
        CAR002_Yetkili2,
        CAR002_Yetkili2Gorevi,
        CAR002_Yetkili2DahiliNo,
        CAR002_Yetkili2EMail,
        CAR002_BankaHesapKodu,
        CAR002_BankaAdi,
        CAR002_BankaSubeKodu,
        CAR002_BankaHesapNo,
        CAR002_KrediKartNo,
        CAR002_OdemeGunu,
        CAR002_ParaBirimi,
        CAR002_MutabakatTarihi,
        CAR002_MutabakatBakiyesi,
        CAR002_TeslimYeri1,
        CAR002_TeslimYeri2,
        CAR002_TeslimAdresi1,
        CAR002_TeslimAdresi2,
        CAR002_KrediLimiti,
        CAR002_DevirSenetRiskiBorc,
        CAR002_DevirSenetRiskiAlacak,
        CAR002_DevirCekRiskiBorc,
        CAR002_DevirCekRiskiAlacak,
        CAR002_DevirTeminat1Borc,
        CAR002_DevirTeminat1Alacak,
        CAR002_DevirTeminat2Borc,
        CAR002_DevirTeminat2Alacak,
        CAR002_DevirProtestoSenet,
        CAR002_DevirKarsiliksizCek,
        CAR002_SenetRiskiBorc,
        CAR002_SenetRiskiAlacak,
        CAR002_CekRiskiBorc,
        CAR002_CekRiskiAlacak,
        CAR002_Teminat1Borc,
        CAR002_Teminat1Alacak,
        CAR002_Teminat2Borc,
        CAR002_Teminat2Alacak,
        CAR002_ProtestoSenet,
        CAR002_KarsiliksizCek,
        CAR002_SonBorcTarihi,
        CAR002_SonAlacakTarihi,
        CAR002_SonRiskBorcTarihi,
        CAR002_SonRiskAlacakTarihi,
        CAR002_Notlar1,
        CAR002_Notlar2,
        CAR002_Notlar3,
        CAR002_Notlar4,
        CAR002_Notlar5,
        CAR002_Notlar6,
        CAR002_Notlar7,
        CAR002_MasrafMerkezi,
        CAR002_Sifre,
        CAR002_Resim,
        CAR002_YaslandirmaBakiye,
        CAR002_YaslandirmaTarihi,
        CAR002_YaslandirmaGunu,
        CAR002_OdemeTarihi,
        CAR002_DovizMutabakatBakiyesi,
        CAR002_DovizDevirBorc,
        CAR002_DovizDevirAlacak,
        CAR002_DovizDigerBorc,
        CAR002_DovizDigerAlacak,
        CAR002_DovizOdemeBorc,
        CAR002_DovizOdemeAlacak,
        CAR002_DovizCiroBorc,
        CAR002_DovizCiroAlacak,
        CAR002_DovizIadeBorc,
        CAR002_DovizIadeAlacak,
        CAR002_DovizKDVBorc,
        CAR002_DovizKDVAlacak,
        CAR002_Dovizotvborc,
        CAR002_Dovizotvalacak,
        CAR002_DovizKrediLimiti,
        CAR002_DovizDevirSenetRiskiBorc,
        CAR002_DovizDevirSenetRiskiAlacak,
        CAR002_DovizDevirCekRiskiBorc,
        CAR002_DovizDevirCekRiskiAlacak,
        CAR002_DovizDevirTeminat1Borc,
        CAR002_DovizDevirTeminat1Alacak,
        CAR002_DovizDevirTeminat2Borc,
        CAR002_DovizDevirTeminat2Alacak,
        CAR002_DovizDevirProtestoSenet,
        CAR002_DovizDevirKarsiliksizCek,
        CAR002_DovizSenetRiskiBorc,
        CAR002_DovizSenetRiskiAlacak,
        CAR002_DovizCekRiskiBorc,
        CAR002_DovizCekRiskiAlacak,
        CAR002_DovizTeminat1Borc,
        CAR002_DovizTeminat1Alacak,
        CAR002_DovizTeminat2Borc,
        CAR002_DovizTeminat2Alacak,
        CAR002_DovizProtestoSenet,
        CAR002_DovizKarsiliksizCek,
        CAR002_Ulke,
        CAR002_CariFormB,
        CAR002_FormBUnvanFlag,
        CAR002_VergiDairesiKodu,
        CAR002_BankaIBANNo,
        CAR002_AktifFlag,
        CAR002_HesapTipi,
        CAR002_YetkiliCep,
        CAR002_YetkiliCep2,
        CAR002_TeslimAdresi3,
        CAR002_Dokuman1,
        CAR002_Dokuman2,
        CAR002_Dokuman3,
        CAR002_BankaKodu1,
        CAR002_BankaAdi1,
        CAR002_SubeKodu1,
        CAR002_IBAN1,
        CAR002_BankaKodu2,
        CAR002_BankaAdi2,
        CAR002_SubeKodu2,
        CAR002_IBAN2,
        CAR002_BankaKodu3,
        CAR002_BankaAdi3,
        CAR002_SubeKodu3,
        CAR002_IBAN3,
        CAR002_BankaKodu4,
        CAR002_BankaAdi4,
        CAR002_SubeKodu4,
        CAR002_IBAN4,
        CAR002_FiyatListeNoAlis,
        CAR002_FiyatListeNoSatis,
        CAR002_IrsaliyeFormNo,
        CAR002_FaturaFormNo,
        CAR002_ILKodu,
        CAR002_ILCEKodu,
        CAR002_PostaKodu,
        CAR002_IrsaliyeRGNFormName,
        CAR002_FaturaRGNFormName,
        CAR002_GMEnlem,
        CAR002_GMBoylam,
        CAR002_DovizCinsi1,
        CAR002_DovizCinsi2,
        CAR002_DovizCinsi3,
        CAR002_DovizCinsi4,
        CAR002_KDVTevkTabi,
        CAR002_EFaturaKullanici,
        CAR002_EFaturaSenaryo,
        CAR002_EFaturaCadde,
        CAR002_EFaturaBinaAdi,
        CAR002_EFaturaDisKapiNo,
        CAR002_EFaturaIcKapiNo,
        CAR002_EFaturaKasabaKoy,
        CAR002_EFaturaIlce,
        CAR002_EFaturaSehir,
        CAR002_EFaturaEtiketNo,
        CAR002_Adi,
        CAR002_Soyadi,
        CAR002_EFaturaMusterBirimSubeTanim,
        CAR002_EFaturaMusterBirimSubeTanimDeger,
        CAR002_EFaturaCHKayitKodTanim,
        CAR002_EFaturaCHKayitKod,
        CAR002_IskNedenTabloNo,
        CAR002_KDVdenMuaf,
        CAR002_EArsivFaturaTeslimSekli,
        CAR002_EArsivTeslimEMailAdresi1,
        CAR002_EArsivTeslimEMailAdresi2,
        CAR002_EArsivTeslimEMailAdresi3,
        CAR002_EArsivGoruntulemeDosyasi,
        CAR002_EArsivUnvanAdresFaturadan,
        CAR002_YOKCBankaBkmID

    }
    #endregion /// CAR002E Enum                
    public class CAR002 : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        private int _CAR002_Row_ID;
        private string _CAR002_HesapKodu;
        private string _CAR002_Unvan1;
        private string _CAR002_Unvan2;
        private string _CAR002_Adres1;
        private string _CAR002_Adres2;
        private string _CAR002_Adres3;
        private string _CAR002_VergiDairesi;
        private string _CAR002_VergiHesapNo;
        private string _CAR002_Telefon1;
        private int? _CAR002_BolgeKodu;
        private string _CAR002_GrupKodu;
        private string _CAR002_MuhasebeKodu;
        private decimal? _CAR002_IskontoOrani;
        private int? _CAR002_OpsiyonGunu;
        private int? _CAR002_DevirTarihi;
        private decimal? _CAR002_DevirBorc;
        private decimal? _CAR002_DevirAlacak;
        private decimal? _CAR002_DigerBorc;
        private decimal? _CAR002_DigerAlacak;
        private decimal? _CAR002_OdemeBorc;
        private decimal? _CAR002_OdemeAlacak;
        private decimal? _CAR002_CiroBorc;
        private decimal? _CAR002_CiroAlacak;
        private decimal? _CAR002_IadeBorc;
        private decimal? _CAR002_IadeAlacak;
        private decimal? _CAR002_KDVBorc;
        private decimal? _CAR002_KDVAlacak;
        private decimal? _CAR002_otvborc;
        private decimal? _CAR002_otvalacak;
        private string _CAR002_GirenKaynak;
        private int? _CAR002_GirenTarih;
        private string _CAR002_GirenSaat;
        private string _CAR002_GirenKodu;
        private string _CAR002_GirenSurum;
        private string _CAR002_DegistirenKaynak;
        private int? _CAR002_DegistirenTarih;
        private string _CAR002_DegistirenSaat;
        private string _CAR002_DegistirenKodu;
        private string _CAR002_DegistirenSurum;
        private string _CAR002_Telefon2;
        private string _CAR002_Telefon3;
        private string _CAR002_Telefon4;
        private string _CAR002_Fax;
        private string _CAR002_EMailAdresi;
        private string _CAR002_InternetAdresi;
        private string _CAR002_OzelKodu;
        private string _CAR002_TipKodu;
        private string _CAR002_Kod1;
        private string _CAR002_Kod2;
        private string _CAR002_Kod3;
        private string _CAR002_Kod4;
        private string _CAR002_Kod5;
        private string _CAR002_Kod6;
        private string _CAR002_Kod7;
        private decimal? _CAR002_Kod8;
        private decimal? _CAR002_Kod9;
        private short? _CAR002_UygulanacakFiyat;
        private short? _CAR002_UygulanacakBankaKodu;
        private byte? _CAR002_UygulanacakFiyatTipi;
        private short? _CAR002_UygulananFiyat;
        private short? _CAR002_UygulananBankaKodu;
        private byte? _CAR002_UygulananFiyatTipi;
        private string _CAR002_Yetkili1;
        private string _CAR002_Yetkili1Gorevi;
        private string _CAR002_Yetkili1DahiliNo;
        private string _CAR002_Yetkili1EMail;
        private string _CAR002_Yetkili2;
        private string _CAR002_Yetkili2Gorevi;
        private string _CAR002_Yetkili2DahiliNo;
        private string _CAR002_Yetkili2EMail;
        private string _CAR002_BankaHesapKodu;
        private string _CAR002_BankaAdi;
        private string _CAR002_BankaSubeKodu;
        private string _CAR002_BankaHesapNo;
        private string _CAR002_KrediKartNo;
        private byte? _CAR002_OdemeGunu;
        private string _CAR002_ParaBirimi;
        private int? _CAR002_MutabakatTarihi;
        private decimal? _CAR002_MutabakatBakiyesi;
        private string _CAR002_TeslimYeri1;
        private string _CAR002_TeslimYeri2;
        private string _CAR002_TeslimAdresi1;
        private string _CAR002_TeslimAdresi2;
        private decimal? _CAR002_KrediLimiti;
        private decimal? _CAR002_DevirSenetRiskiBorc;
        private decimal? _CAR002_DevirSenetRiskiAlacak;
        private decimal? _CAR002_DevirCekRiskiBorc;
        private decimal? _CAR002_DevirCekRiskiAlacak;
        private decimal? _CAR002_DevirTeminat1Borc;
        private decimal? _CAR002_DevirTeminat1Alacak;
        private decimal? _CAR002_DevirTeminat2Borc;
        private decimal? _CAR002_DevirTeminat2Alacak;
        private decimal? _CAR002_DevirProtestoSenet;
        private decimal? _CAR002_DevirKarsiliksizCek;
        private decimal? _CAR002_SenetRiskiBorc;
        private decimal? _CAR002_SenetRiskiAlacak;
        private decimal? _CAR002_CekRiskiBorc;
        private decimal? _CAR002_CekRiskiAlacak;
        private decimal? _CAR002_Teminat1Borc;
        private decimal? _CAR002_Teminat1Alacak;
        private decimal? _CAR002_Teminat2Borc;
        private decimal? _CAR002_Teminat2Alacak;
        private decimal? _CAR002_ProtestoSenet;
        private decimal? _CAR002_KarsiliksizCek;
        private int? _CAR002_SonBorcTarihi;
        private int? _CAR002_SonAlacakTarihi;
        private int? _CAR002_SonRiskBorcTarihi;
        private int? _CAR002_SonRiskAlacakTarihi;
        private string _CAR002_Notlar1;
        private string _CAR002_Notlar2;
        private string _CAR002_Notlar3;
        private string _CAR002_Notlar4;
        private string _CAR002_Notlar5;
        private string _CAR002_Notlar6;
        private string _CAR002_Notlar7;
        private string _CAR002_MasrafMerkezi;
        private string _CAR002_Sifre;
        private string _CAR002_Resim;
        private decimal? _CAR002_YaslandirmaBakiye;
        private int? _CAR002_YaslandirmaTarihi;
        private int? _CAR002_YaslandirmaGunu;
        private int? _CAR002_OdemeTarihi;
        private decimal? _CAR002_DovizMutabakatBakiyesi;
        private decimal? _CAR002_DovizDevirBorc;
        private decimal? _CAR002_DovizDevirAlacak;
        private decimal? _CAR002_DovizDigerBorc;
        private decimal? _CAR002_DovizDigerAlacak;
        private decimal? _CAR002_DovizOdemeBorc;
        private decimal? _CAR002_DovizOdemeAlacak;
        private decimal? _CAR002_DovizCiroBorc;
        private decimal? _CAR002_DovizCiroAlacak;
        private decimal? _CAR002_DovizIadeBorc;
        private decimal? _CAR002_DovizIadeAlacak;
        private decimal? _CAR002_DovizKDVBorc;
        private decimal? _CAR002_DovizKDVAlacak;
        private decimal? _CAR002_Dovizotvborc;
        private decimal? _CAR002_Dovizotvalacak;
        private decimal? _CAR002_DovizKrediLimiti;
        private decimal? _CAR002_DovizDevirSenetRiskiBorc;
        private decimal? _CAR002_DovizDevirSenetRiskiAlacak;
        private decimal? _CAR002_DovizDevirCekRiskiBorc;
        private decimal? _CAR002_DovizDevirCekRiskiAlacak;
        private decimal? _CAR002_DovizDevirTeminat1Borc;
        private decimal? _CAR002_DovizDevirTeminat1Alacak;
        private decimal? _CAR002_DovizDevirTeminat2Borc;
        private decimal? _CAR002_DovizDevirTeminat2Alacak;
        private decimal? _CAR002_DovizDevirProtestoSenet;
        private decimal? _CAR002_DovizDevirKarsiliksizCek;
        private decimal? _CAR002_DovizSenetRiskiBorc;
        private decimal? _CAR002_DovizSenetRiskiAlacak;
        private decimal? _CAR002_DovizCekRiskiBorc;
        private decimal? _CAR002_DovizCekRiskiAlacak;
        private decimal? _CAR002_DovizTeminat1Borc;
        private decimal? _CAR002_DovizTeminat1Alacak;
        private decimal? _CAR002_DovizTeminat2Borc;
        private decimal? _CAR002_DovizTeminat2Alacak;
        private decimal? _CAR002_DovizProtestoSenet;
        private decimal? _CAR002_DovizKarsiliksizCek;
        private int? _CAR002_Ulke;
        private byte? _CAR002_CariFormB;
        private byte? _CAR002_FormBUnvanFlag;
        private int? _CAR002_VergiDairesiKodu;
        private string _CAR002_BankaIBANNo;
        private byte? _CAR002_AktifFlag;
        private short? _CAR002_HesapTipi;
        private string _CAR002_YetkiliCep;
        private string _CAR002_YetkiliCep2;
        private string _CAR002_TeslimAdresi3;
        private string _CAR002_Dokuman1;
        private string _CAR002_Dokuman2;
        private string _CAR002_Dokuman3;
        private int? _CAR002_BankaKodu1;
        private string _CAR002_BankaAdi1;
        private int? _CAR002_SubeKodu1;
        private string _CAR002_IBAN1;
        private int? _CAR002_BankaKodu2;
        private string _CAR002_BankaAdi2;
        private int? _CAR002_SubeKodu2;
        private string _CAR002_IBAN2;
        private int? _CAR002_BankaKodu3;
        private string _CAR002_BankaAdi3;
        private int? _CAR002_SubeKodu3;
        private string _CAR002_IBAN3;
        private int? _CAR002_BankaKodu4;
        private string _CAR002_BankaAdi4;
        private int? _CAR002_SubeKodu4;
        private string _CAR002_IBAN4;
        private int? _CAR002_FiyatListeNoAlis;
        private int? _CAR002_FiyatListeNoSatis;
        private byte? _CAR002_IrsaliyeFormNo;
        private byte? _CAR002_FaturaFormNo;
        private int? _CAR002_ILKodu;
        private short? _CAR002_ILCEKodu;
        private string _CAR002_PostaKodu;
        private string _CAR002_IrsaliyeRGNFormName;
        private string _CAR002_FaturaRGNFormName;
        private string _CAR002_GMEnlem;
        private string _CAR002_GMBoylam;
        private string _CAR002_DovizCinsi1;
        private string _CAR002_DovizCinsi2;
        private string _CAR002_DovizCinsi3;
        private string _CAR002_DovizCinsi4;
        private byte? _CAR002_KDVTevkTabi;
        private string _CAR002_EFaturaKullanici;
        private byte? _CAR002_EFaturaSenaryo;
        private string _CAR002_EFaturaCadde;
        private string _CAR002_EFaturaBinaAdi;
        private string _CAR002_EFaturaDisKapiNo;
        private string _CAR002_EFaturaIcKapiNo;
        private string _CAR002_EFaturaKasabaKoy;
        private string _CAR002_EFaturaIlce;
        private string _CAR002_EFaturaSehir;
        private string _CAR002_EFaturaEtiketNo;
        private string _CAR002_Adi;
        private string _CAR002_Soyadi;
        private string _CAR002_EFaturaMusterBirimSubeTanim;
        private string _CAR002_EFaturaMusterBirimSubeTanimDeger;
        private string _CAR002_EFaturaCHKayitKodTanim;
        private string _CAR002_EFaturaCHKayitKod;
        private short? _CAR002_IskNedenTabloNo;
        private byte? _CAR002_KDVdenMuaf;
        private byte? _CAR002_EArsivFaturaTeslimSekli;
        private string _CAR002_EArsivTeslimEMailAdresi1;
        private string _CAR002_EArsivTeslimEMailAdresi2;
        private string _CAR002_EArsivTeslimEMailAdresi3;
        private string _CAR002_EArsivGoruntulemeDosyasi;
        private byte? _CAR002_EArsivUnvanAdresFaturadan;
        private int? _CAR002_YOKCBankaBkmID;

        private string _pk_CAR002_HesapKodu;
        #endregion /// Fields


        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int CAR002_Row_ID
        {
            get { return this._CAR002_Row_ID; }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR002_HesapKodu
        {
            get { return this._CAR002_HesapKodu; }
            set
            {
                this._CAR002_HesapKodu = value;
                OnPropertyChanged("CAR002_HesapKodu");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Unvan1
        {
            get { return this._CAR002_Unvan1; }
            set
            {
                this._CAR002_Unvan1 = value;
                OnPropertyChanged("CAR002_Unvan1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Unvan2
        {
            get { return this._CAR002_Unvan2; }
            set
            {
                this._CAR002_Unvan2 = value;
                OnPropertyChanged("CAR002_Unvan2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adres1
        {
            get { return this._CAR002_Adres1; }
            set
            {
                this._CAR002_Adres1 = value;
                OnPropertyChanged("CAR002_Adres1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adres2
        {
            get { return this._CAR002_Adres2; }
            set
            {
                this._CAR002_Adres2 = value;
                OnPropertyChanged("CAR002_Adres2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adres3
        {
            get { return this._CAR002_Adres3; }
            set
            {
                this._CAR002_Adres3 = value;
                OnPropertyChanged("CAR002_Adres3");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string CAR002_VergiDairesi
        {
            get { return this._CAR002_VergiDairesi; }
            set
            {
                this._CAR002_VergiDairesi = value;
                OnPropertyChanged("CAR002_VergiDairesi");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_VergiHesapNo
        {
            get { return this._CAR002_VergiHesapNo; }
            set
            {
                this._CAR002_VergiHesapNo = value;
                OnPropertyChanged("CAR002_VergiHesapNo");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon1
        {
            get { return this._CAR002_Telefon1; }
            set
            {
                this._CAR002_Telefon1 = value;
                OnPropertyChanged("CAR002_Telefon1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BolgeKodu
        {
            get { return this._CAR002_BolgeKodu; }
            set
            {
                this._CAR002_BolgeKodu = value;
                OnPropertyChanged("CAR002_BolgeKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_GrupKodu
        {
            get { return this._CAR002_GrupKodu; }
            set
            {
                this._CAR002_GrupKodu = value;
                OnPropertyChanged("CAR002_GrupKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_MuhasebeKodu
        {
            get { return this._CAR002_MuhasebeKodu; }
            set
            {
                this._CAR002_MuhasebeKodu = value;
                OnPropertyChanged("CAR002_MuhasebeKodu");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR002_IskontoOrani
        {
            get { return this._CAR002_IskontoOrani; }
            set
            {
                this._CAR002_IskontoOrani = value;
                OnPropertyChanged("CAR002_IskontoOrani");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_OpsiyonGunu
        {
            get { return this._CAR002_OpsiyonGunu; }
            set
            {
                this._CAR002_OpsiyonGunu = value;
                OnPropertyChanged("CAR002_OpsiyonGunu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_DevirTarihi
        {
            get { return this._CAR002_DevirTarihi; }
            set
            {
                this._CAR002_DevirTarihi = value;
                OnPropertyChanged("CAR002_DevirTarihi");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirBorc
        {
            get { return this._CAR002_DevirBorc; }
            set
            {
                this._CAR002_DevirBorc = value;
                OnPropertyChanged("CAR002_DevirBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirAlacak
        {
            get { return this._CAR002_DevirAlacak; }
            set
            {
                this._CAR002_DevirAlacak = value;
                OnPropertyChanged("CAR002_DevirAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DigerBorc
        {
            get { return this._CAR002_DigerBorc; }
            set
            {
                this._CAR002_DigerBorc = value;
                OnPropertyChanged("CAR002_DigerBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DigerAlacak
        {
            get { return this._CAR002_DigerAlacak; }
            set
            {
                this._CAR002_DigerAlacak = value;
                OnPropertyChanged("CAR002_DigerAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_OdemeBorc
        {
            get { return this._CAR002_OdemeBorc; }
            set
            {
                this._CAR002_OdemeBorc = value;
                OnPropertyChanged("CAR002_OdemeBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_OdemeAlacak
        {
            get { return this._CAR002_OdemeAlacak; }
            set
            {
                this._CAR002_OdemeAlacak = value;
                OnPropertyChanged("CAR002_OdemeAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CiroBorc
        {
            get { return this._CAR002_CiroBorc; }
            set
            {
                this._CAR002_CiroBorc = value;
                OnPropertyChanged("CAR002_CiroBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CiroAlacak
        {
            get { return this._CAR002_CiroAlacak; }
            set
            {
                this._CAR002_CiroAlacak = value;
                OnPropertyChanged("CAR002_CiroAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_IadeBorc
        {
            get { return this._CAR002_IadeBorc; }
            set
            {
                this._CAR002_IadeBorc = value;
                OnPropertyChanged("CAR002_IadeBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_IadeAlacak
        {
            get { return this._CAR002_IadeAlacak; }
            set
            {
                this._CAR002_IadeAlacak = value;
                OnPropertyChanged("CAR002_IadeAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KDVBorc
        {
            get { return this._CAR002_KDVBorc; }
            set
            {
                this._CAR002_KDVBorc = value;
                OnPropertyChanged("CAR002_KDVBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KDVAlacak
        {
            get { return this._CAR002_KDVAlacak; }
            set
            {
                this._CAR002_KDVAlacak = value;
                OnPropertyChanged("CAR002_KDVAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_otvborc
        {
            get { return this._CAR002_otvborc; }
            set
            {
                this._CAR002_otvborc = value;
                OnPropertyChanged("CAR002_otvborc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_otvalacak
        {
            get { return this._CAR002_otvalacak; }
            set
            {
                this._CAR002_otvalacak = value;
                OnPropertyChanged("CAR002_otvalacak");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_GirenKaynak
        {
            get { return this._CAR002_GirenKaynak; }
            set
            {
                this._CAR002_GirenKaynak = value;
                OnPropertyChanged("CAR002_GirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_GirenTarih
        {
            get { return this._CAR002_GirenTarih; }
            set
            {
                this._CAR002_GirenTarih = value;
                OnPropertyChanged("CAR002_GirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_GirenSaat
        {
            get { return this._CAR002_GirenSaat; }
            set
            {
                this._CAR002_GirenSaat = value;
                OnPropertyChanged("CAR002_GirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_GirenKodu
        {
            get { return this._CAR002_GirenKodu; }
            set
            {
                this._CAR002_GirenKodu = value;
                OnPropertyChanged("CAR002_GirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_GirenSurum
        {
            get { return this._CAR002_GirenSurum; }
            set
            {
                this._CAR002_GirenSurum = value;
                OnPropertyChanged("CAR002_GirenSurum");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_DegistirenKaynak
        {
            get { return this._CAR002_DegistirenKaynak; }
            set
            {
                this._CAR002_DegistirenKaynak = value;
                OnPropertyChanged("CAR002_DegistirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_DegistirenTarih
        {
            get { return this._CAR002_DegistirenTarih; }
            set
            {
                this._CAR002_DegistirenTarih = value;
                OnPropertyChanged("CAR002_DegistirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_DegistirenSaat
        {
            get { return this._CAR002_DegistirenSaat; }
            set
            {
                this._CAR002_DegistirenSaat = value;
                OnPropertyChanged("CAR002_DegistirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_DegistirenKodu
        {
            get { return this._CAR002_DegistirenKodu; }
            set
            {
                this._CAR002_DegistirenKodu = value;
                OnPropertyChanged("CAR002_DegistirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_DegistirenSurum
        {
            get { return this._CAR002_DegistirenSurum; }
            set
            {
                this._CAR002_DegistirenSurum = value;
                OnPropertyChanged("CAR002_DegistirenSurum");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon2
        {
            get { return this._CAR002_Telefon2; }
            set
            {
                this._CAR002_Telefon2 = value;
                OnPropertyChanged("CAR002_Telefon2");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon3
        {
            get { return this._CAR002_Telefon3; }
            set
            {
                this._CAR002_Telefon3 = value;
                OnPropertyChanged("CAR002_Telefon3");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon4
        {
            get { return this._CAR002_Telefon4; }
            set
            {
                this._CAR002_Telefon4 = value;
                OnPropertyChanged("CAR002_Telefon4");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Fax
        {
            get { return this._CAR002_Fax; }
            set
            {
                this._CAR002_Fax = value;
                OnPropertyChanged("CAR002_Fax");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EMailAdresi
        {
            get { return this._CAR002_EMailAdresi; }
            set
            {
                this._CAR002_EMailAdresi = value;
                OnPropertyChanged("CAR002_EMailAdresi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_InternetAdresi
        {
            get { return this._CAR002_InternetAdresi; }
            set
            {
                this._CAR002_InternetAdresi = value;
                OnPropertyChanged("CAR002_InternetAdresi");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_OzelKodu
        {
            get { return this._CAR002_OzelKodu; }
            set
            {
                this._CAR002_OzelKodu = value;
                OnPropertyChanged("CAR002_OzelKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_TipKodu
        {
            get { return this._CAR002_TipKodu; }
            set
            {
                this._CAR002_TipKodu = value;
                OnPropertyChanged("CAR002_TipKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod1
        {
            get { return this._CAR002_Kod1; }
            set
            {
                this._CAR002_Kod1 = value;
                OnPropertyChanged("CAR002_Kod1");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod2
        {
            get { return this._CAR002_Kod2; }
            set
            {
                this._CAR002_Kod2 = value;
                OnPropertyChanged("CAR002_Kod2");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod3
        {
            get { return this._CAR002_Kod3; }
            set
            {
                this._CAR002_Kod3 = value;
                OnPropertyChanged("CAR002_Kod3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod4
        {
            get { return this._CAR002_Kod4; }
            set
            {
                this._CAR002_Kod4 = value;
                OnPropertyChanged("CAR002_Kod4");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod5
        {
            get { return this._CAR002_Kod5; }
            set
            {
                this._CAR002_Kod5 = value;
                OnPropertyChanged("CAR002_Kod5");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod6
        {
            get { return this._CAR002_Kod6; }
            set
            {
                this._CAR002_Kod6 = value;
                OnPropertyChanged("CAR002_Kod6");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod7
        {
            get { return this._CAR002_Kod7; }
            set
            {
                this._CAR002_Kod7 = value;
                OnPropertyChanged("CAR002_Kod7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Kod8
        {
            get { return this._CAR002_Kod8; }
            set
            {
                this._CAR002_Kod8 = value;
                OnPropertyChanged("CAR002_Kod8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Kod9
        {
            get { return this._CAR002_Kod9; }
            set
            {
                this._CAR002_Kod9 = value;
                OnPropertyChanged("CAR002_Kod9");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulanacakFiyat
        {
            get { return this._CAR002_UygulanacakFiyat; }
            set
            {
                this._CAR002_UygulanacakFiyat = value;
                OnPropertyChanged("CAR002_UygulanacakFiyat");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulanacakBankaKodu
        {
            get { return this._CAR002_UygulanacakBankaKodu; }
            set
            {
                this._CAR002_UygulanacakBankaKodu = value;
                OnPropertyChanged("CAR002_UygulanacakBankaKodu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_UygulanacakFiyatTipi
        {
            get { return this._CAR002_UygulanacakFiyatTipi; }
            set
            {
                this._CAR002_UygulanacakFiyatTipi = value;
                OnPropertyChanged("CAR002_UygulanacakFiyatTipi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulananFiyat
        {
            get { return this._CAR002_UygulananFiyat; }
            set
            {
                this._CAR002_UygulananFiyat = value;
                OnPropertyChanged("CAR002_UygulananFiyat");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulananBankaKodu
        {
            get { return this._CAR002_UygulananBankaKodu; }
            set
            {
                this._CAR002_UygulananBankaKodu = value;
                OnPropertyChanged("CAR002_UygulananBankaKodu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_UygulananFiyatTipi
        {
            get { return this._CAR002_UygulananFiyatTipi; }
            set
            {
                this._CAR002_UygulananFiyatTipi = value;
                OnPropertyChanged("CAR002_UygulananFiyatTipi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Yetkili1
        {
            get { return this._CAR002_Yetkili1; }
            set
            {
                this._CAR002_Yetkili1 = value;
                OnPropertyChanged("CAR002_Yetkili1");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string CAR002_Yetkili1Gorevi
        {
            get { return this._CAR002_Yetkili1Gorevi; }
            set
            {
                this._CAR002_Yetkili1Gorevi = value;
                OnPropertyChanged("CAR002_Yetkili1Gorevi");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_Yetkili1DahiliNo
        {
            get { return this._CAR002_Yetkili1DahiliNo; }
            set
            {
                this._CAR002_Yetkili1DahiliNo = value;
                OnPropertyChanged("CAR002_Yetkili1DahiliNo");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_Yetkili1EMail
        {
            get { return this._CAR002_Yetkili1EMail; }
            set
            {
                this._CAR002_Yetkili1EMail = value;
                OnPropertyChanged("CAR002_Yetkili1EMail");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Yetkili2
        {
            get { return this._CAR002_Yetkili2; }
            set
            {
                this._CAR002_Yetkili2 = value;
                OnPropertyChanged("CAR002_Yetkili2");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string CAR002_Yetkili2Gorevi
        {
            get { return this._CAR002_Yetkili2Gorevi; }
            set
            {
                this._CAR002_Yetkili2Gorevi = value;
                OnPropertyChanged("CAR002_Yetkili2Gorevi");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_Yetkili2DahiliNo
        {
            get { return this._CAR002_Yetkili2DahiliNo; }
            set
            {
                this._CAR002_Yetkili2DahiliNo = value;
                OnPropertyChanged("CAR002_Yetkili2DahiliNo");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_Yetkili2EMail
        {
            get { return this._CAR002_Yetkili2EMail; }
            set
            {
                this._CAR002_Yetkili2EMail = value;
                OnPropertyChanged("CAR002_Yetkili2EMail");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR002_BankaHesapKodu
        {
            get { return this._CAR002_BankaHesapKodu; }
            set
            {
                this._CAR002_BankaHesapKodu = value;
                OnPropertyChanged("CAR002_BankaHesapKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_BankaAdi
        {
            get { return this._CAR002_BankaAdi; }
            set
            {
                this._CAR002_BankaAdi = value;
                OnPropertyChanged("CAR002_BankaAdi");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_BankaSubeKodu
        {
            get { return this._CAR002_BankaSubeKodu; }
            set
            {
                this._CAR002_BankaSubeKodu = value;
                OnPropertyChanged("CAR002_BankaSubeKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_BankaHesapNo
        {
            get { return this._CAR002_BankaHesapNo; }
            set
            {
                this._CAR002_BankaHesapNo = value;
                OnPropertyChanged("CAR002_BankaHesapNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_KrediKartNo
        {
            get { return this._CAR002_KrediKartNo; }
            set
            {
                this._CAR002_KrediKartNo = value;
                OnPropertyChanged("CAR002_KrediKartNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_OdemeGunu
        {
            get { return this._CAR002_OdemeGunu; }
            set
            {
                this._CAR002_OdemeGunu = value;
                OnPropertyChanged("CAR002_OdemeGunu");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_ParaBirimi
        {
            get { return this._CAR002_ParaBirimi; }
            set
            {
                this._CAR002_ParaBirimi = value;
                OnPropertyChanged("CAR002_ParaBirimi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_MutabakatTarihi
        {
            get { return this._CAR002_MutabakatTarihi; }
            set
            {
                this._CAR002_MutabakatTarihi = value;
                OnPropertyChanged("CAR002_MutabakatTarihi");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_MutabakatBakiyesi
        {
            get { return this._CAR002_MutabakatBakiyesi; }
            set
            {
                this._CAR002_MutabakatBakiyesi = value;
                OnPropertyChanged("CAR002_MutabakatBakiyesi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimYeri1
        {
            get { return this._CAR002_TeslimYeri1; }
            set
            {
                this._CAR002_TeslimYeri1 = value;
                OnPropertyChanged("CAR002_TeslimYeri1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimYeri2
        {
            get { return this._CAR002_TeslimYeri2; }
            set
            {
                this._CAR002_TeslimYeri2 = value;
                OnPropertyChanged("CAR002_TeslimYeri2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimAdresi1
        {
            get { return this._CAR002_TeslimAdresi1; }
            set
            {
                this._CAR002_TeslimAdresi1 = value;
                OnPropertyChanged("CAR002_TeslimAdresi1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimAdresi2
        {
            get { return this._CAR002_TeslimAdresi2; }
            set
            {
                this._CAR002_TeslimAdresi2 = value;
                OnPropertyChanged("CAR002_TeslimAdresi2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KrediLimiti
        {
            get { return this._CAR002_KrediLimiti; }
            set
            {
                this._CAR002_KrediLimiti = value;
                OnPropertyChanged("CAR002_KrediLimiti");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirSenetRiskiBorc
        {
            get { return this._CAR002_DevirSenetRiskiBorc; }
            set
            {
                this._CAR002_DevirSenetRiskiBorc = value;
                OnPropertyChanged("CAR002_DevirSenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirSenetRiskiAlacak
        {
            get { return this._CAR002_DevirSenetRiskiAlacak; }
            set
            {
                this._CAR002_DevirSenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_DevirSenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirCekRiskiBorc
        {
            get { return this._CAR002_DevirCekRiskiBorc; }
            set
            {
                this._CAR002_DevirCekRiskiBorc = value;
                OnPropertyChanged("CAR002_DevirCekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirCekRiskiAlacak
        {
            get { return this._CAR002_DevirCekRiskiAlacak; }
            set
            {
                this._CAR002_DevirCekRiskiAlacak = value;
                OnPropertyChanged("CAR002_DevirCekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat1Borc
        {
            get { return this._CAR002_DevirTeminat1Borc; }
            set
            {
                this._CAR002_DevirTeminat1Borc = value;
                OnPropertyChanged("CAR002_DevirTeminat1Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat1Alacak
        {
            get { return this._CAR002_DevirTeminat1Alacak; }
            set
            {
                this._CAR002_DevirTeminat1Alacak = value;
                OnPropertyChanged("CAR002_DevirTeminat1Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat2Borc
        {
            get { return this._CAR002_DevirTeminat2Borc; }
            set
            {
                this._CAR002_DevirTeminat2Borc = value;
                OnPropertyChanged("CAR002_DevirTeminat2Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat2Alacak
        {
            get { return this._CAR002_DevirTeminat2Alacak; }
            set
            {
                this._CAR002_DevirTeminat2Alacak = value;
                OnPropertyChanged("CAR002_DevirTeminat2Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirProtestoSenet
        {
            get { return this._CAR002_DevirProtestoSenet; }
            set
            {
                this._CAR002_DevirProtestoSenet = value;
                OnPropertyChanged("CAR002_DevirProtestoSenet");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirKarsiliksizCek
        {
            get { return this._CAR002_DevirKarsiliksizCek; }
            set
            {
                this._CAR002_DevirKarsiliksizCek = value;
                OnPropertyChanged("CAR002_DevirKarsiliksizCek");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_SenetRiskiBorc
        {
            get { return this._CAR002_SenetRiskiBorc; }
            set
            {
                this._CAR002_SenetRiskiBorc = value;
                OnPropertyChanged("CAR002_SenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_SenetRiskiAlacak
        {
            get { return this._CAR002_SenetRiskiAlacak; }
            set
            {
                this._CAR002_SenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_SenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CekRiskiBorc
        {
            get { return this._CAR002_CekRiskiBorc; }
            set
            {
                this._CAR002_CekRiskiBorc = value;
                OnPropertyChanged("CAR002_CekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CekRiskiAlacak
        {
            get { return this._CAR002_CekRiskiAlacak; }
            set
            {
                this._CAR002_CekRiskiAlacak = value;
                OnPropertyChanged("CAR002_CekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat1Borc
        {
            get { return this._CAR002_Teminat1Borc; }
            set
            {
                this._CAR002_Teminat1Borc = value;
                OnPropertyChanged("CAR002_Teminat1Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat1Alacak
        {
            get { return this._CAR002_Teminat1Alacak; }
            set
            {
                this._CAR002_Teminat1Alacak = value;
                OnPropertyChanged("CAR002_Teminat1Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat2Borc
        {
            get { return this._CAR002_Teminat2Borc; }
            set
            {
                this._CAR002_Teminat2Borc = value;
                OnPropertyChanged("CAR002_Teminat2Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat2Alacak
        {
            get { return this._CAR002_Teminat2Alacak; }
            set
            {
                this._CAR002_Teminat2Alacak = value;
                OnPropertyChanged("CAR002_Teminat2Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_ProtestoSenet
        {
            get { return this._CAR002_ProtestoSenet; }
            set
            {
                this._CAR002_ProtestoSenet = value;
                OnPropertyChanged("CAR002_ProtestoSenet");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KarsiliksizCek
        {
            get { return this._CAR002_KarsiliksizCek; }
            set
            {
                this._CAR002_KarsiliksizCek = value;
                OnPropertyChanged("CAR002_KarsiliksizCek");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonBorcTarihi
        {
            get { return this._CAR002_SonBorcTarihi; }
            set
            {
                this._CAR002_SonBorcTarihi = value;
                OnPropertyChanged("CAR002_SonBorcTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonAlacakTarihi
        {
            get { return this._CAR002_SonAlacakTarihi; }
            set
            {
                this._CAR002_SonAlacakTarihi = value;
                OnPropertyChanged("CAR002_SonAlacakTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonRiskBorcTarihi
        {
            get { return this._CAR002_SonRiskBorcTarihi; }
            set
            {
                this._CAR002_SonRiskBorcTarihi = value;
                OnPropertyChanged("CAR002_SonRiskBorcTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonRiskAlacakTarihi
        {
            get { return this._CAR002_SonRiskAlacakTarihi; }
            set
            {
                this._CAR002_SonRiskAlacakTarihi = value;
                OnPropertyChanged("CAR002_SonRiskAlacakTarihi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar1
        {
            get { return this._CAR002_Notlar1; }
            set
            {
                this._CAR002_Notlar1 = value;
                OnPropertyChanged("CAR002_Notlar1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar2
        {
            get { return this._CAR002_Notlar2; }
            set
            {
                this._CAR002_Notlar2 = value;
                OnPropertyChanged("CAR002_Notlar2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar3
        {
            get { return this._CAR002_Notlar3; }
            set
            {
                this._CAR002_Notlar3 = value;
                OnPropertyChanged("CAR002_Notlar3");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar4
        {
            get { return this._CAR002_Notlar4; }
            set
            {
                this._CAR002_Notlar4 = value;
                OnPropertyChanged("CAR002_Notlar4");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar5
        {
            get { return this._CAR002_Notlar5; }
            set
            {
                this._CAR002_Notlar5 = value;
                OnPropertyChanged("CAR002_Notlar5");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar6
        {
            get { return this._CAR002_Notlar6; }
            set
            {
                this._CAR002_Notlar6 = value;
                OnPropertyChanged("CAR002_Notlar6");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar7
        {
            get { return this._CAR002_Notlar7; }
            set
            {
                this._CAR002_Notlar7 = value;
                OnPropertyChanged("CAR002_Notlar7");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_MasrafMerkezi
        {
            get { return this._CAR002_MasrafMerkezi; }
            set
            {
                this._CAR002_MasrafMerkezi = value;
                OnPropertyChanged("CAR002_MasrafMerkezi");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_Sifre
        {
            get { return this._CAR002_Sifre; }
            set
            {
                this._CAR002_Sifre = value;
                OnPropertyChanged("CAR002_Sifre");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Resim
        {
            get { return this._CAR002_Resim; }
            set
            {
                this._CAR002_Resim = value;
                OnPropertyChanged("CAR002_Resim");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_YaslandirmaBakiye
        {
            get { return this._CAR002_YaslandirmaBakiye; }
            set
            {
                this._CAR002_YaslandirmaBakiye = value;
                OnPropertyChanged("CAR002_YaslandirmaBakiye");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_YaslandirmaTarihi
        {
            get { return this._CAR002_YaslandirmaTarihi; }
            set
            {
                this._CAR002_YaslandirmaTarihi = value;
                OnPropertyChanged("CAR002_YaslandirmaTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_YaslandirmaGunu
        {
            get { return this._CAR002_YaslandirmaGunu; }
            set
            {
                this._CAR002_YaslandirmaGunu = value;
                OnPropertyChanged("CAR002_YaslandirmaGunu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_OdemeTarihi
        {
            get { return this._CAR002_OdemeTarihi; }
            set
            {
                this._CAR002_OdemeTarihi = value;
                OnPropertyChanged("CAR002_OdemeTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizMutabakatBakiyesi
        {
            get { return this._CAR002_DovizMutabakatBakiyesi; }
            set
            {
                this._CAR002_DovizMutabakatBakiyesi = value;
                OnPropertyChanged("CAR002_DovizMutabakatBakiyesi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirBorc
        {
            get { return this._CAR002_DovizDevirBorc; }
            set
            {
                this._CAR002_DovizDevirBorc = value;
                OnPropertyChanged("CAR002_DovizDevirBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirAlacak
        {
            get { return this._CAR002_DovizDevirAlacak; }
            set
            {
                this._CAR002_DovizDevirAlacak = value;
                OnPropertyChanged("CAR002_DovizDevirAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDigerBorc
        {
            get { return this._CAR002_DovizDigerBorc; }
            set
            {
                this._CAR002_DovizDigerBorc = value;
                OnPropertyChanged("CAR002_DovizDigerBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDigerAlacak
        {
            get { return this._CAR002_DovizDigerAlacak; }
            set
            {
                this._CAR002_DovizDigerAlacak = value;
                OnPropertyChanged("CAR002_DovizDigerAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizOdemeBorc
        {
            get { return this._CAR002_DovizOdemeBorc; }
            set
            {
                this._CAR002_DovizOdemeBorc = value;
                OnPropertyChanged("CAR002_DovizOdemeBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizOdemeAlacak
        {
            get { return this._CAR002_DovizOdemeAlacak; }
            set
            {
                this._CAR002_DovizOdemeAlacak = value;
                OnPropertyChanged("CAR002_DovizOdemeAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCiroBorc
        {
            get { return this._CAR002_DovizCiroBorc; }
            set
            {
                this._CAR002_DovizCiroBorc = value;
                OnPropertyChanged("CAR002_DovizCiroBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCiroAlacak
        {
            get { return this._CAR002_DovizCiroAlacak; }
            set
            {
                this._CAR002_DovizCiroAlacak = value;
                OnPropertyChanged("CAR002_DovizCiroAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizIadeBorc
        {
            get { return this._CAR002_DovizIadeBorc; }
            set
            {
                this._CAR002_DovizIadeBorc = value;
                OnPropertyChanged("CAR002_DovizIadeBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizIadeAlacak
        {
            get { return this._CAR002_DovizIadeAlacak; }
            set
            {
                this._CAR002_DovizIadeAlacak = value;
                OnPropertyChanged("CAR002_DovizIadeAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKDVBorc
        {
            get { return this._CAR002_DovizKDVBorc; }
            set
            {
                this._CAR002_DovizKDVBorc = value;
                OnPropertyChanged("CAR002_DovizKDVBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKDVAlacak
        {
            get { return this._CAR002_DovizKDVAlacak; }
            set
            {
                this._CAR002_DovizKDVAlacak = value;
                OnPropertyChanged("CAR002_DovizKDVAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_Dovizotvborc
        {
            get { return this._CAR002_Dovizotvborc; }
            set
            {
                this._CAR002_Dovizotvborc = value;
                OnPropertyChanged("CAR002_Dovizotvborc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_Dovizotvalacak
        {
            get { return this._CAR002_Dovizotvalacak; }
            set
            {
                this._CAR002_Dovizotvalacak = value;
                OnPropertyChanged("CAR002_Dovizotvalacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKrediLimiti
        {
            get { return this._CAR002_DovizKrediLimiti; }
            set
            {
                this._CAR002_DovizKrediLimiti = value;
                OnPropertyChanged("CAR002_DovizKrediLimiti");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirSenetRiskiBorc
        {
            get { return this._CAR002_DovizDevirSenetRiskiBorc; }
            set
            {
                this._CAR002_DovizDevirSenetRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizDevirSenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirSenetRiskiAlacak
        {
            get { return this._CAR002_DovizDevirSenetRiskiAlacak; }
            set
            {
                this._CAR002_DovizDevirSenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizDevirSenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirCekRiskiBorc
        {
            get { return this._CAR002_DovizDevirCekRiskiBorc; }
            set
            {
                this._CAR002_DovizDevirCekRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizDevirCekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirCekRiskiAlacak
        {
            get { return this._CAR002_DovizDevirCekRiskiAlacak; }
            set
            {
                this._CAR002_DovizDevirCekRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizDevirCekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat1Borc
        {
            get { return this._CAR002_DovizDevirTeminat1Borc; }
            set
            {
                this._CAR002_DovizDevirTeminat1Borc = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat1Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat1Alacak
        {
            get { return this._CAR002_DovizDevirTeminat1Alacak; }
            set
            {
                this._CAR002_DovizDevirTeminat1Alacak = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat1Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat2Borc
        {
            get { return this._CAR002_DovizDevirTeminat2Borc; }
            set
            {
                this._CAR002_DovizDevirTeminat2Borc = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat2Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat2Alacak
        {
            get { return this._CAR002_DovizDevirTeminat2Alacak; }
            set
            {
                this._CAR002_DovizDevirTeminat2Alacak = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat2Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirProtestoSenet
        {
            get { return this._CAR002_DovizDevirProtestoSenet; }
            set
            {
                this._CAR002_DovizDevirProtestoSenet = value;
                OnPropertyChanged("CAR002_DovizDevirProtestoSenet");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirKarsiliksizCek
        {
            get { return this._CAR002_DovizDevirKarsiliksizCek; }
            set
            {
                this._CAR002_DovizDevirKarsiliksizCek = value;
                OnPropertyChanged("CAR002_DovizDevirKarsiliksizCek");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizSenetRiskiBorc
        {
            get { return this._CAR002_DovizSenetRiskiBorc; }
            set
            {
                this._CAR002_DovizSenetRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizSenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizSenetRiskiAlacak
        {
            get { return this._CAR002_DovizSenetRiskiAlacak; }
            set
            {
                this._CAR002_DovizSenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizSenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCekRiskiBorc
        {
            get { return this._CAR002_DovizCekRiskiBorc; }
            set
            {
                this._CAR002_DovizCekRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizCekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCekRiskiAlacak
        {
            get { return this._CAR002_DovizCekRiskiAlacak; }
            set
            {
                this._CAR002_DovizCekRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizCekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat1Borc
        {
            get { return this._CAR002_DovizTeminat1Borc; }
            set
            {
                this._CAR002_DovizTeminat1Borc = value;
                OnPropertyChanged("CAR002_DovizTeminat1Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat1Alacak
        {
            get { return this._CAR002_DovizTeminat1Alacak; }
            set
            {
                this._CAR002_DovizTeminat1Alacak = value;
                OnPropertyChanged("CAR002_DovizTeminat1Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat2Borc
        {
            get { return this._CAR002_DovizTeminat2Borc; }
            set
            {
                this._CAR002_DovizTeminat2Borc = value;
                OnPropertyChanged("CAR002_DovizTeminat2Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat2Alacak
        {
            get { return this._CAR002_DovizTeminat2Alacak; }
            set
            {
                this._CAR002_DovizTeminat2Alacak = value;
                OnPropertyChanged("CAR002_DovizTeminat2Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizProtestoSenet
        {
            get { return this._CAR002_DovizProtestoSenet; }
            set
            {
                this._CAR002_DovizProtestoSenet = value;
                OnPropertyChanged("CAR002_DovizProtestoSenet");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKarsiliksizCek
        {
            get { return this._CAR002_DovizKarsiliksizCek; }
            set
            {
                this._CAR002_DovizKarsiliksizCek = value;
                OnPropertyChanged("CAR002_DovizKarsiliksizCek");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_Ulke
        {
            get { return this._CAR002_Ulke; }
            set
            {
                this._CAR002_Ulke = value;
                OnPropertyChanged("CAR002_Ulke");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_CariFormB
        {
            get { return this._CAR002_CariFormB; }
            set
            {
                this._CAR002_CariFormB = value;
                OnPropertyChanged("CAR002_CariFormB");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_FormBUnvanFlag
        {
            get { return this._CAR002_FormBUnvanFlag; }
            set
            {
                this._CAR002_FormBUnvanFlag = value;
                OnPropertyChanged("CAR002_FormBUnvanFlag");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_VergiDairesiKodu
        {
            get { return this._CAR002_VergiDairesiKodu; }
            set
            {
                this._CAR002_VergiDairesiKodu = value;
                OnPropertyChanged("CAR002_VergiDairesiKodu");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_BankaIBANNo
        {
            get { return this._CAR002_BankaIBANNo; }
            set
            {
                this._CAR002_BankaIBANNo = value;
                OnPropertyChanged("CAR002_BankaIBANNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_AktifFlag
        {
            get { return this._CAR002_AktifFlag; }
            set
            {
                this._CAR002_AktifFlag = value;
                OnPropertyChanged("CAR002_AktifFlag");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_HesapTipi
        {
            get { return this._CAR002_HesapTipi; }
            set
            {
                this._CAR002_HesapTipi = value;
                OnPropertyChanged("CAR002_HesapTipi");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_YetkiliCep
        {
            get { return this._CAR002_YetkiliCep; }
            set
            {
                this._CAR002_YetkiliCep = value;
                OnPropertyChanged("CAR002_YetkiliCep");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_YetkiliCep2
        {
            get { return this._CAR002_YetkiliCep2; }
            set
            {
                this._CAR002_YetkiliCep2 = value;
                OnPropertyChanged("CAR002_YetkiliCep2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimAdresi3
        {
            get { return this._CAR002_TeslimAdresi3; }
            set
            {
                this._CAR002_TeslimAdresi3 = value;
                OnPropertyChanged("CAR002_TeslimAdresi3");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Dokuman1
        {
            get { return this._CAR002_Dokuman1; }
            set
            {
                this._CAR002_Dokuman1 = value;
                OnPropertyChanged("CAR002_Dokuman1");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Dokuman2
        {
            get { return this._CAR002_Dokuman2; }
            set
            {
                this._CAR002_Dokuman2 = value;
                OnPropertyChanged("CAR002_Dokuman2");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Dokuman3
        {
            get { return this._CAR002_Dokuman3; }
            set
            {
                this._CAR002_Dokuman3 = value;
                OnPropertyChanged("CAR002_Dokuman3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu1
        {
            get { return this._CAR002_BankaKodu1; }
            set
            {
                this._CAR002_BankaKodu1 = value;
                OnPropertyChanged("CAR002_BankaKodu1");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi1
        {
            get { return this._CAR002_BankaAdi1; }
            set
            {
                this._CAR002_BankaAdi1 = value;
                OnPropertyChanged("CAR002_BankaAdi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu1
        {
            get { return this._CAR002_SubeKodu1; }
            set
            {
                this._CAR002_SubeKodu1 = value;
                OnPropertyChanged("CAR002_SubeKodu1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN1
        {
            get { return this._CAR002_IBAN1; }
            set
            {
                this._CAR002_IBAN1 = value;
                OnPropertyChanged("CAR002_IBAN1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu2
        {
            get { return this._CAR002_BankaKodu2; }
            set
            {
                this._CAR002_BankaKodu2 = value;
                OnPropertyChanged("CAR002_BankaKodu2");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi2
        {
            get { return this._CAR002_BankaAdi2; }
            set
            {
                this._CAR002_BankaAdi2 = value;
                OnPropertyChanged("CAR002_BankaAdi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu2
        {
            get { return this._CAR002_SubeKodu2; }
            set
            {
                this._CAR002_SubeKodu2 = value;
                OnPropertyChanged("CAR002_SubeKodu2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN2
        {
            get { return this._CAR002_IBAN2; }
            set
            {
                this._CAR002_IBAN2 = value;
                OnPropertyChanged("CAR002_IBAN2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu3
        {
            get { return this._CAR002_BankaKodu3; }
            set
            {
                this._CAR002_BankaKodu3 = value;
                OnPropertyChanged("CAR002_BankaKodu3");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi3
        {
            get { return this._CAR002_BankaAdi3; }
            set
            {
                this._CAR002_BankaAdi3 = value;
                OnPropertyChanged("CAR002_BankaAdi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu3
        {
            get { return this._CAR002_SubeKodu3; }
            set
            {
                this._CAR002_SubeKodu3 = value;
                OnPropertyChanged("CAR002_SubeKodu3");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN3
        {
            get { return this._CAR002_IBAN3; }
            set
            {
                this._CAR002_IBAN3 = value;
                OnPropertyChanged("CAR002_IBAN3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu4
        {
            get { return this._CAR002_BankaKodu4; }
            set
            {
                this._CAR002_BankaKodu4 = value;
                OnPropertyChanged("CAR002_BankaKodu4");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi4
        {
            get { return this._CAR002_BankaAdi4; }
            set
            {
                this._CAR002_BankaAdi4 = value;
                OnPropertyChanged("CAR002_BankaAdi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu4
        {
            get { return this._CAR002_SubeKodu4; }
            set
            {
                this._CAR002_SubeKodu4 = value;
                OnPropertyChanged("CAR002_SubeKodu4");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN4
        {
            get { return this._CAR002_IBAN4; }
            set
            {
                this._CAR002_IBAN4 = value;
                OnPropertyChanged("CAR002_IBAN4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_FiyatListeNoAlis
        {
            get { return this._CAR002_FiyatListeNoAlis; }
            set
            {
                this._CAR002_FiyatListeNoAlis = value;
                OnPropertyChanged("CAR002_FiyatListeNoAlis");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_FiyatListeNoSatis
        {
            get { return this._CAR002_FiyatListeNoSatis; }
            set
            {
                this._CAR002_FiyatListeNoSatis = value;
                OnPropertyChanged("CAR002_FiyatListeNoSatis");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_IrsaliyeFormNo
        {
            get { return this._CAR002_IrsaliyeFormNo; }
            set
            {
                this._CAR002_IrsaliyeFormNo = value;
                OnPropertyChanged("CAR002_IrsaliyeFormNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_FaturaFormNo
        {
            get { return this._CAR002_FaturaFormNo; }
            set
            {
                this._CAR002_FaturaFormNo = value;
                OnPropertyChanged("CAR002_FaturaFormNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_ILKodu
        {
            get { return this._CAR002_ILKodu; }
            set
            {
                this._CAR002_ILKodu = value;
                OnPropertyChanged("CAR002_ILKodu");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_ILCEKodu
        {
            get { return this._CAR002_ILCEKodu; }
            set
            {
                this._CAR002_ILCEKodu = value;
                OnPropertyChanged("CAR002_ILCEKodu");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_PostaKodu
        {
            get { return this._CAR002_PostaKodu; }
            set
            {
                this._CAR002_PostaKodu = value;
                OnPropertyChanged("CAR002_PostaKodu");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_IrsaliyeRGNFormName
        {
            get { return this._CAR002_IrsaliyeRGNFormName; }
            set
            {
                this._CAR002_IrsaliyeRGNFormName = value;
                OnPropertyChanged("CAR002_IrsaliyeRGNFormName");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_FaturaRGNFormName
        {
            get { return this._CAR002_FaturaRGNFormName; }
            set
            {
                this._CAR002_FaturaRGNFormName = value;
                OnPropertyChanged("CAR002_FaturaRGNFormName");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_GMEnlem
        {
            get { return this._CAR002_GMEnlem; }
            set
            {
                this._CAR002_GMEnlem = value;
                OnPropertyChanged("CAR002_GMEnlem");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_GMBoylam
        {
            get { return this._CAR002_GMBoylam; }
            set
            {
                this._CAR002_GMBoylam = value;
                OnPropertyChanged("CAR002_GMBoylam");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi1
        {
            get { return this._CAR002_DovizCinsi1; }
            set
            {
                this._CAR002_DovizCinsi1 = value;
                OnPropertyChanged("CAR002_DovizCinsi1");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi2
        {
            get { return this._CAR002_DovizCinsi2; }
            set
            {
                this._CAR002_DovizCinsi2 = value;
                OnPropertyChanged("CAR002_DovizCinsi2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi3
        {
            get { return this._CAR002_DovizCinsi3; }
            set
            {
                this._CAR002_DovizCinsi3 = value;
                OnPropertyChanged("CAR002_DovizCinsi3");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi4
        {
            get { return this._CAR002_DovizCinsi4; }
            set
            {
                this._CAR002_DovizCinsi4 = value;
                OnPropertyChanged("CAR002_DovizCinsi4");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_KDVTevkTabi
        {
            get { return this._CAR002_KDVTevkTabi; }
            set
            {
                this._CAR002_KDVTevkTabi = value;
                OnPropertyChanged("CAR002_KDVTevkTabi");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR002_EFaturaKullanici
        {
            get { return this._CAR002_EFaturaKullanici; }
            set
            {
                this._CAR002_EFaturaKullanici = value;
                OnPropertyChanged("CAR002_EFaturaKullanici");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_EFaturaSenaryo
        {
            get { return this._CAR002_EFaturaSenaryo; }
            set
            {
                this._CAR002_EFaturaSenaryo = value;
                OnPropertyChanged("CAR002_EFaturaSenaryo");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaCadde
        {
            get { return this._CAR002_EFaturaCadde; }
            set
            {
                this._CAR002_EFaturaCadde = value;
                OnPropertyChanged("CAR002_EFaturaCadde");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaBinaAdi
        {
            get { return this._CAR002_EFaturaBinaAdi; }
            set
            {
                this._CAR002_EFaturaBinaAdi = value;
                OnPropertyChanged("CAR002_EFaturaBinaAdi");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_EFaturaDisKapiNo
        {
            get { return this._CAR002_EFaturaDisKapiNo; }
            set
            {
                this._CAR002_EFaturaDisKapiNo = value;
                OnPropertyChanged("CAR002_EFaturaDisKapiNo");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_EFaturaIcKapiNo
        {
            get { return this._CAR002_EFaturaIcKapiNo; }
            set
            {
                this._CAR002_EFaturaIcKapiNo = value;
                OnPropertyChanged("CAR002_EFaturaIcKapiNo");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaKasabaKoy
        {
            get { return this._CAR002_EFaturaKasabaKoy; }
            set
            {
                this._CAR002_EFaturaKasabaKoy = value;
                OnPropertyChanged("CAR002_EFaturaKasabaKoy");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaIlce
        {
            get { return this._CAR002_EFaturaIlce; }
            set
            {
                this._CAR002_EFaturaIlce = value;
                OnPropertyChanged("CAR002_EFaturaIlce");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaSehir
        {
            get { return this._CAR002_EFaturaSehir; }
            set
            {
                this._CAR002_EFaturaSehir = value;
                OnPropertyChanged("CAR002_EFaturaSehir");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR002_EFaturaEtiketNo
        {
            get { return this._CAR002_EFaturaEtiketNo; }
            set
            {
                this._CAR002_EFaturaEtiketNo = value;
                OnPropertyChanged("CAR002_EFaturaEtiketNo");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adi
        {
            get { return this._CAR002_Adi; }
            set
            {
                this._CAR002_Adi = value;
                OnPropertyChanged("CAR002_Adi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Soyadi
        {
            get { return this._CAR002_Soyadi; }
            set
            {
                this._CAR002_Soyadi = value;
                OnPropertyChanged("CAR002_Soyadi");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaMusterBirimSubeTanim
        {
            get { return this._CAR002_EFaturaMusterBirimSubeTanim; }
            set
            {
                this._CAR002_EFaturaMusterBirimSubeTanim = value;
                OnPropertyChanged("CAR002_EFaturaMusterBirimSubeTanim");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaMusterBirimSubeTanimDeger
        {
            get { return this._CAR002_EFaturaMusterBirimSubeTanimDeger; }
            set
            {
                this._CAR002_EFaturaMusterBirimSubeTanimDeger = value;
                OnPropertyChanged("CAR002_EFaturaMusterBirimSubeTanimDeger");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaCHKayitKodTanim
        {
            get { return this._CAR002_EFaturaCHKayitKodTanim; }
            set
            {
                this._CAR002_EFaturaCHKayitKodTanim = value;
                OnPropertyChanged("CAR002_EFaturaCHKayitKodTanim");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaCHKayitKod
        {
            get { return this._CAR002_EFaturaCHKayitKod; }
            set
            {
                this._CAR002_EFaturaCHKayitKod = value;
                OnPropertyChanged("CAR002_EFaturaCHKayitKod");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_IskNedenTabloNo
        {
            get { return this._CAR002_IskNedenTabloNo; }
            set
            {
                this._CAR002_IskNedenTabloNo = value;
                OnPropertyChanged("CAR002_IskNedenTabloNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_KDVdenMuaf
        {
            get { return this._CAR002_KDVdenMuaf; }
            set
            {
                this._CAR002_KDVdenMuaf = value;
                OnPropertyChanged("CAR002_KDVdenMuaf");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_EArsivFaturaTeslimSekli
        {
            get { return this._CAR002_EArsivFaturaTeslimSekli; }
            set
            {
                this._CAR002_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("CAR002_EArsivFaturaTeslimSekli");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EArsivTeslimEMailAdresi1
        {
            get { return this._CAR002_EArsivTeslimEMailAdresi1; }
            set
            {
                this._CAR002_EArsivTeslimEMailAdresi1 = value;
                OnPropertyChanged("CAR002_EArsivTeslimEMailAdresi1");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EArsivTeslimEMailAdresi2
        {
            get { return this._CAR002_EArsivTeslimEMailAdresi2; }
            set
            {
                this._CAR002_EArsivTeslimEMailAdresi2 = value;
                OnPropertyChanged("CAR002_EArsivTeslimEMailAdresi2");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EArsivTeslimEMailAdresi3
        {
            get { return this._CAR002_EArsivTeslimEMailAdresi3; }
            set
            {
                this._CAR002_EArsivTeslimEMailAdresi3 = value;
                OnPropertyChanged("CAR002_EArsivTeslimEMailAdresi3");
            }
        }

        /// <summary> NVARCHAR (512) Allow Null </summary>
        public string CAR002_EArsivGoruntulemeDosyasi
        {
            get { return this._CAR002_EArsivGoruntulemeDosyasi; }
            set
            {
                this._CAR002_EArsivGoruntulemeDosyasi = value;
                OnPropertyChanged("CAR002_EArsivGoruntulemeDosyasi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_EArsivUnvanAdresFaturadan
        {
            get { return this._CAR002_EArsivUnvanAdresFaturadan; }
            set
            {
                this._CAR002_EArsivUnvanAdresFaturadan = value;
                OnPropertyChanged("CAR002_EArsivUnvanAdresFaturadan");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_YOKCBankaBkmID
        {
            get { return this._CAR002_YOKCBankaBkmID; }
            set
            {
                this._CAR002_YOKCBankaBkmID = value;
                OnPropertyChanged("CAR002_YOKCBankaBkmID");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string pk_CAR002_HesapKodu
        {
            get { return this._pk_CAR002_HesapKodu; }
            set
            {
                this._pk_CAR002_HesapKodu = value;
                OnPropertyChanged("pk_CAR002_HesapKodu");
            }
        }

        #endregion /// Properties       
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(CAR002E Property, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR002E), Property), Deger, And_Or));
        }

        public void WhereAdd(CAR002E Property, Islem islem, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR002E), Property), islem, Deger, And_Or));
        }

        public void WhereAdd(CAR002E Property, Operand In_NotIn, params object[] Degerler_Parantez)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR002E), Property), In_NotIn, Degerler_Parantez));
        }

        public void WhereAdd(params object[] Degerler)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Degerler));
        }

        /// <summary> Set işleminde Property " = " eşit ile otomatik başlar. </summary>
        public void SetAdd(CAR002E Property, params object[] Degerler)
        {
            SetList.Add(SqlExperOperatorIslem.SetAdd(Enum.GetName(typeof(CAR002E), Property), Degerler));
        }

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "YNS{0}.YNS{0}.CAR002";
        private string[] info_PrimaryKeys = { "pk_CAR002_HesapKodu" };
        private string[] info_IdentityKeys = { "CAR002_Row_ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public CAR002()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += CAR002_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void CAR002_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
}
