using Birikim.Enums;
using Birikim.Helpers;
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
        int cAR002_Row_ID;
        string cAR002_HesapKodu;
        string cAR002_Unvan1;
        string cAR002_Unvan2;
        string cAR002_Adres1;
        string cAR002_Adres2;
        string cAR002_Adres3;
        string cAR002_VergiDairesi;
        string cAR002_VergiHesapNo;
        string cAR002_Telefon1;
        int? cAR002_BolgeKodu;
        string cAR002_GrupKodu;
        string cAR002_MuhasebeKodu;
        decimal? cAR002_IskontoOrani;
        int? cAR002_OpsiyonGunu;
        int? cAR002_DevirTarihi;
        decimal? cAR002_DevirBorc;
        decimal? cAR002_DevirAlacak;
        decimal? cAR002_DigerBorc;
        decimal? cAR002_DigerAlacak;
        decimal? cAR002_OdemeBorc;
        decimal? cAR002_OdemeAlacak;
        decimal? cAR002_CiroBorc;
        decimal? cAR002_CiroAlacak;
        decimal? cAR002_IadeBorc;
        decimal? cAR002_IadeAlacak;
        decimal? cAR002_KDVBorc;
        decimal? cAR002_KDVAlacak;
        decimal? cAR002_otvborc;
        decimal? cAR002_otvalacak;
        string cAR002_GirenKaynak;
        int? cAR002_GirenTarih;
        string cAR002_GirenSaat;
        string cAR002_GirenKodu;
        string cAR002_GirenSurum;
        string cAR002_DegistirenKaynak;
        int? cAR002_DegistirenTarih;
        string cAR002_DegistirenSaat;
        string cAR002_DegistirenKodu;
        string cAR002_DegistirenSurum;
        string cAR002_Telefon2;
        string cAR002_Telefon3;
        string cAR002_Telefon4;
        string cAR002_Fax;
        string cAR002_EMailAdresi;
        string cAR002_InternetAdresi;
        string cAR002_OzelKodu;
        string cAR002_TipKodu;
        string cAR002_Kod1;
        string cAR002_Kod2;
        string cAR002_Kod3;
        string cAR002_Kod4;
        string cAR002_Kod5;
        string cAR002_Kod6;
        string cAR002_Kod7;
        decimal? cAR002_Kod8;
        decimal? cAR002_Kod9;
        short? cAR002_UygulanacakFiyat;
        short? cAR002_UygulanacakBankaKodu;
        byte? cAR002_UygulanacakFiyatTipi;
        short? cAR002_UygulananFiyat;
        short? cAR002_UygulananBankaKodu;
        byte? cAR002_UygulananFiyatTipi;
        string cAR002_Yetkili1;
        string cAR002_Yetkili1Gorevi;
        string cAR002_Yetkili1DahiliNo;
        string cAR002_Yetkili1EMail;
        string cAR002_Yetkili2;
        string cAR002_Yetkili2Gorevi;
        string cAR002_Yetkili2DahiliNo;
        string cAR002_Yetkili2EMail;
        string cAR002_BankaHesapKodu;
        string cAR002_BankaAdi;
        string cAR002_BankaSubeKodu;
        string cAR002_BankaHesapNo;
        string cAR002_KrediKartNo;
        byte? cAR002_OdemeGunu;
        string cAR002_ParaBirimi;
        int? cAR002_MutabakatTarihi;
        decimal? cAR002_MutabakatBakiyesi;
        string cAR002_TeslimYeri1;
        string cAR002_TeslimYeri2;
        string cAR002_TeslimAdresi1;
        string cAR002_TeslimAdresi2;
        decimal? cAR002_KrediLimiti;
        decimal? cAR002_DevirSenetRiskiBorc;
        decimal? cAR002_DevirSenetRiskiAlacak;
        decimal? cAR002_DevirCekRiskiBorc;
        decimal? cAR002_DevirCekRiskiAlacak;
        decimal? cAR002_DevirTeminat1Borc;
        decimal? cAR002_DevirTeminat1Alacak;
        decimal? cAR002_DevirTeminat2Borc;
        decimal? cAR002_DevirTeminat2Alacak;
        decimal? cAR002_DevirProtestoSenet;
        decimal? cAR002_DevirKarsiliksizCek;
        decimal? cAR002_SenetRiskiBorc;
        decimal? cAR002_SenetRiskiAlacak;
        decimal? cAR002_CekRiskiBorc;
        decimal? cAR002_CekRiskiAlacak;
        decimal? cAR002_Teminat1Borc;
        decimal? cAR002_Teminat1Alacak;
        decimal? cAR002_Teminat2Borc;
        decimal? cAR002_Teminat2Alacak;
        decimal? cAR002_ProtestoSenet;
        decimal? cAR002_KarsiliksizCek;
        int? cAR002_SonBorcTarihi;
        int? cAR002_SonAlacakTarihi;
        int? cAR002_SonRiskBorcTarihi;
        int? cAR002_SonRiskAlacakTarihi;
        string cAR002_Notlar1;
        string cAR002_Notlar2;
        string cAR002_Notlar3;
        string cAR002_Notlar4;
        string cAR002_Notlar5;
        string cAR002_Notlar6;
        string cAR002_Notlar7;
        string cAR002_MasrafMerkezi;
        string cAR002_Sifre;
        string cAR002_Resim;
        decimal? cAR002_YaslandirmaBakiye;
        int? cAR002_YaslandirmaTarihi;
        int? cAR002_YaslandirmaGunu;
        int? cAR002_OdemeTarihi;
        decimal? cAR002_DovizMutabakatBakiyesi;
        decimal? cAR002_DovizDevirBorc;
        decimal? cAR002_DovizDevirAlacak;
        decimal? cAR002_DovizDigerBorc;
        decimal? cAR002_DovizDigerAlacak;
        decimal? cAR002_DovizOdemeBorc;
        decimal? cAR002_DovizOdemeAlacak;
        decimal? cAR002_DovizCiroBorc;
        decimal? cAR002_DovizCiroAlacak;
        decimal? cAR002_DovizIadeBorc;
        decimal? cAR002_DovizIadeAlacak;
        decimal? cAR002_DovizKDVBorc;
        decimal? cAR002_DovizKDVAlacak;
        decimal? cAR002_Dovizotvborc;
        decimal? cAR002_Dovizotvalacak;
        decimal? cAR002_DovizKrediLimiti;
        decimal? cAR002_DovizDevirSenetRiskiBorc;
        decimal? cAR002_DovizDevirSenetRiskiAlacak;
        decimal? cAR002_DovizDevirCekRiskiBorc;
        decimal? cAR002_DovizDevirCekRiskiAlacak;
        decimal? cAR002_DovizDevirTeminat1Borc;
        decimal? cAR002_DovizDevirTeminat1Alacak;
        decimal? cAR002_DovizDevirTeminat2Borc;
        decimal? cAR002_DovizDevirTeminat2Alacak;
        decimal? cAR002_DovizDevirProtestoSenet;
        decimal? cAR002_DovizDevirKarsiliksizCek;
        decimal? cAR002_DovizSenetRiskiBorc;
        decimal? cAR002_DovizSenetRiskiAlacak;
        decimal? cAR002_DovizCekRiskiBorc;
        decimal? cAR002_DovizCekRiskiAlacak;
        decimal? cAR002_DovizTeminat1Borc;
        decimal? cAR002_DovizTeminat1Alacak;
        decimal? cAR002_DovizTeminat2Borc;
        decimal? cAR002_DovizTeminat2Alacak;
        decimal? cAR002_DovizProtestoSenet;
        decimal? cAR002_DovizKarsiliksizCek;
        int? cAR002_Ulke;
        byte? cAR002_CariFormB;
        byte? cAR002_FormBUnvanFlag;
        int? cAR002_VergiDairesiKodu;
        string cAR002_BankaIBANNo;
        byte? cAR002_AktifFlag;
        short? cAR002_HesapTipi;
        string cAR002_YetkiliCep;
        string cAR002_YetkiliCep2;
        string cAR002_TeslimAdresi3;
        string cAR002_Dokuman1;
        string cAR002_Dokuman2;
        string cAR002_Dokuman3;
        int? cAR002_BankaKodu1;
        string cAR002_BankaAdi1;
        int? cAR002_SubeKodu1;
        string cAR002_IBAN1;
        int? cAR002_BankaKodu2;
        string cAR002_BankaAdi2;
        int? cAR002_SubeKodu2;
        string cAR002_IBAN2;
        int? cAR002_BankaKodu3;
        string cAR002_BankaAdi3;
        int? cAR002_SubeKodu3;
        string cAR002_IBAN3;
        int? cAR002_BankaKodu4;
        string cAR002_BankaAdi4;
        int? cAR002_SubeKodu4;
        string cAR002_IBAN4;
        int? cAR002_FiyatListeNoAlis;
        int? cAR002_FiyatListeNoSatis;
        byte? cAR002_IrsaliyeFormNo;
        byte? cAR002_FaturaFormNo;
        int? cAR002_ILKodu;
        short? cAR002_ILCEKodu;
        string cAR002_PostaKodu;
        string cAR002_IrsaliyeRGNFormName;
        string cAR002_FaturaRGNFormName;
        string cAR002_GMEnlem;
        string cAR002_GMBoylam;
        string cAR002_DovizCinsi1;
        string cAR002_DovizCinsi2;
        string cAR002_DovizCinsi3;
        string cAR002_DovizCinsi4;
        byte? cAR002_KDVTevkTabi;
        string cAR002_EFaturaKullanici;
        byte? cAR002_EFaturaSenaryo;
        string cAR002_EFaturaCadde;
        string cAR002_EFaturaBinaAdi;
        string cAR002_EFaturaDisKapiNo;
        string cAR002_EFaturaIcKapiNo;
        string cAR002_EFaturaKasabaKoy;
        string cAR002_EFaturaIlce;
        string cAR002_EFaturaSehir;
        string cAR002_EFaturaEtiketNo;
        string cAR002_Adi;
        string cAR002_Soyadi;
        string cAR002_EFaturaMusterBirimSubeTanim;
        string cAR002_EFaturaMusterBirimSubeTanimDeger;
        string cAR002_EFaturaCHKayitKodTanim;
        string cAR002_EFaturaCHKayitKod;
        short? cAR002_IskNedenTabloNo;
        byte? cAR002_KDVdenMuaf;
        byte? cAR002_EArsivFaturaTeslimSekli;
        string cAR002_EArsivTeslimEMailAdresi1;
        string cAR002_EArsivTeslimEMailAdresi2;
        string cAR002_EArsivTeslimEMailAdresi3;
        string cAR002_EArsivGoruntulemeDosyasi;
        byte? cAR002_EArsivUnvanAdresFaturadan;
        int? cAR002_YOKCBankaBkmID;

        string _pk_CAR002_HesapKodu;
        #endregion /// Fields

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int CAR002_Row_ID => cAR002_Row_ID;

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR002_HesapKodu
        {
            get { return cAR002_HesapKodu; }
            set
            {
                cAR002_HesapKodu = value;
                OnPropertyChanged("CAR002_HesapKodu");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Unvan1
        {
            get { return cAR002_Unvan1; }
            set
            {
                cAR002_Unvan1 = value;
                OnPropertyChanged("CAR002_Unvan1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Unvan2
        {
            get { return cAR002_Unvan2; }
            set
            {
                cAR002_Unvan2 = value;
                OnPropertyChanged("CAR002_Unvan2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adres1
        {
            get { return cAR002_Adres1; }
            set
            {
                cAR002_Adres1 = value;
                OnPropertyChanged("CAR002_Adres1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adres2
        {
            get { return cAR002_Adres2; }
            set
            {
                cAR002_Adres2 = value;
                OnPropertyChanged("CAR002_Adres2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adres3
        {
            get { return cAR002_Adres3; }
            set
            {
                cAR002_Adres3 = value;
                OnPropertyChanged("CAR002_Adres3");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string CAR002_VergiDairesi
        {
            get { return cAR002_VergiDairesi; }
            set
            {
                cAR002_VergiDairesi = value;
                OnPropertyChanged("CAR002_VergiDairesi");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_VergiHesapNo
        {
            get { return cAR002_VergiHesapNo; }
            set
            {
                cAR002_VergiHesapNo = value;
                OnPropertyChanged("CAR002_VergiHesapNo");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon1
        {
            get { return cAR002_Telefon1; }
            set
            {
                cAR002_Telefon1 = value;
                OnPropertyChanged("CAR002_Telefon1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BolgeKodu
        {
            get { return cAR002_BolgeKodu; }
            set
            {
                cAR002_BolgeKodu = value;
                OnPropertyChanged("CAR002_BolgeKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_GrupKodu
        {
            get { return cAR002_GrupKodu; }
            set
            {
                cAR002_GrupKodu = value;
                OnPropertyChanged("CAR002_GrupKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_MuhasebeKodu
        {
            get { return cAR002_MuhasebeKodu; }
            set
            {
                cAR002_MuhasebeKodu = value;
                OnPropertyChanged("CAR002_MuhasebeKodu");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? CAR002_IskontoOrani
        {
            get { return cAR002_IskontoOrani; }
            set
            {
                cAR002_IskontoOrani = value;
                OnPropertyChanged("CAR002_IskontoOrani");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_OpsiyonGunu
        {
            get { return cAR002_OpsiyonGunu; }
            set
            {
                cAR002_OpsiyonGunu = value;
                OnPropertyChanged("CAR002_OpsiyonGunu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_DevirTarihi
        {
            get { return cAR002_DevirTarihi; }
            set
            {
                cAR002_DevirTarihi = value;
                OnPropertyChanged("CAR002_DevirTarihi");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirBorc
        {
            get { return cAR002_DevirBorc; }
            set
            {
                cAR002_DevirBorc = value;
                OnPropertyChanged("CAR002_DevirBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirAlacak
        {
            get { return cAR002_DevirAlacak; }
            set
            {
                cAR002_DevirAlacak = value;
                OnPropertyChanged("CAR002_DevirAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DigerBorc
        {
            get { return cAR002_DigerBorc; }
            set
            {
                cAR002_DigerBorc = value;
                OnPropertyChanged("CAR002_DigerBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DigerAlacak
        {
            get { return cAR002_DigerAlacak; }
            set
            {
                cAR002_DigerAlacak = value;
                OnPropertyChanged("CAR002_DigerAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_OdemeBorc
        {
            get { return cAR002_OdemeBorc; }
            set
            {
                cAR002_OdemeBorc = value;
                OnPropertyChanged("CAR002_OdemeBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_OdemeAlacak
        {
            get { return cAR002_OdemeAlacak; }
            set
            {
                cAR002_OdemeAlacak = value;
                OnPropertyChanged("CAR002_OdemeAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CiroBorc
        {
            get { return cAR002_CiroBorc; }
            set
            {
                cAR002_CiroBorc = value;
                OnPropertyChanged("CAR002_CiroBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CiroAlacak
        {
            get { return cAR002_CiroAlacak; }
            set
            {
                cAR002_CiroAlacak = value;
                OnPropertyChanged("CAR002_CiroAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_IadeBorc
        {
            get { return cAR002_IadeBorc; }
            set
            {
                cAR002_IadeBorc = value;
                OnPropertyChanged("CAR002_IadeBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_IadeAlacak
        {
            get { return cAR002_IadeAlacak; }
            set
            {
                cAR002_IadeAlacak = value;
                OnPropertyChanged("CAR002_IadeAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KDVBorc
        {
            get { return cAR002_KDVBorc; }
            set
            {
                cAR002_KDVBorc = value;
                OnPropertyChanged("CAR002_KDVBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KDVAlacak
        {
            get { return cAR002_KDVAlacak; }
            set
            {
                cAR002_KDVAlacak = value;
                OnPropertyChanged("CAR002_KDVAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_otvborc
        {
            get { return cAR002_otvborc; }
            set
            {
                cAR002_otvborc = value;
                OnPropertyChanged("CAR002_otvborc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_otvalacak
        {
            get { return cAR002_otvalacak; }
            set
            {
                cAR002_otvalacak = value;
                OnPropertyChanged("CAR002_otvalacak");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_GirenKaynak
        {
            get { return cAR002_GirenKaynak; }
            set
            {
                cAR002_GirenKaynak = value;
                OnPropertyChanged("CAR002_GirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_GirenTarih
        {
            get { return cAR002_GirenTarih; }
            set
            {
                cAR002_GirenTarih = value;
                OnPropertyChanged("CAR002_GirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_GirenSaat
        {
            get { return cAR002_GirenSaat; }
            set
            {
                cAR002_GirenSaat = value;
                OnPropertyChanged("CAR002_GirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_GirenKodu
        {
            get { return cAR002_GirenKodu; }
            set
            {
                cAR002_GirenKodu = value;
                OnPropertyChanged("CAR002_GirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_GirenSurum
        {
            get { return cAR002_GirenSurum; }
            set
            {
                cAR002_GirenSurum = value;
                OnPropertyChanged("CAR002_GirenSurum");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_DegistirenKaynak
        {
            get { return cAR002_DegistirenKaynak; }
            set
            {
                cAR002_DegistirenKaynak = value;
                OnPropertyChanged("CAR002_DegistirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_DegistirenTarih
        {
            get { return cAR002_DegistirenTarih; }
            set
            {
                cAR002_DegistirenTarih = value;
                OnPropertyChanged("CAR002_DegistirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_DegistirenSaat
        {
            get { return cAR002_DegistirenSaat; }
            set
            {
                cAR002_DegistirenSaat = value;
                OnPropertyChanged("CAR002_DegistirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_DegistirenKodu
        {
            get { return cAR002_DegistirenKodu; }
            set
            {
                cAR002_DegistirenKodu = value;
                OnPropertyChanged("CAR002_DegistirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_DegistirenSurum
        {
            get { return cAR002_DegistirenSurum; }
            set
            {
                cAR002_DegistirenSurum = value;
                OnPropertyChanged("CAR002_DegistirenSurum");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon2
        {
            get { return cAR002_Telefon2; }
            set
            {
                cAR002_Telefon2 = value;
                OnPropertyChanged("CAR002_Telefon2");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon3
        {
            get { return cAR002_Telefon3; }
            set
            {
                cAR002_Telefon3 = value;
                OnPropertyChanged("CAR002_Telefon3");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Telefon4
        {
            get { return cAR002_Telefon4; }
            set
            {
                cAR002_Telefon4 = value;
                OnPropertyChanged("CAR002_Telefon4");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_Fax
        {
            get { return cAR002_Fax; }
            set
            {
                cAR002_Fax = value;
                OnPropertyChanged("CAR002_Fax");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EMailAdresi
        {
            get { return cAR002_EMailAdresi; }
            set
            {
                cAR002_EMailAdresi = value;
                OnPropertyChanged("CAR002_EMailAdresi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_InternetAdresi
        {
            get { return cAR002_InternetAdresi; }
            set
            {
                cAR002_InternetAdresi = value;
                OnPropertyChanged("CAR002_InternetAdresi");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_OzelKodu
        {
            get { return cAR002_OzelKodu; }
            set
            {
                cAR002_OzelKodu = value;
                OnPropertyChanged("CAR002_OzelKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_TipKodu
        {
            get { return cAR002_TipKodu; }
            set
            {
                cAR002_TipKodu = value;
                OnPropertyChanged("CAR002_TipKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod1
        {
            get { return cAR002_Kod1; }
            set
            {
                cAR002_Kod1 = value;
                OnPropertyChanged("CAR002_Kod1");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod2
        {
            get { return cAR002_Kod2; }
            set
            {
                cAR002_Kod2 = value;
                OnPropertyChanged("CAR002_Kod2");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod3
        {
            get { return cAR002_Kod3; }
            set
            {
                cAR002_Kod3 = value;
                OnPropertyChanged("CAR002_Kod3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod4
        {
            get { return cAR002_Kod4; }
            set
            {
                cAR002_Kod4 = value;
                OnPropertyChanged("CAR002_Kod4");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod5
        {
            get { return cAR002_Kod5; }
            set
            {
                cAR002_Kod5 = value;
                OnPropertyChanged("CAR002_Kod5");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod6
        {
            get { return cAR002_Kod6; }
            set
            {
                cAR002_Kod6 = value;
                OnPropertyChanged("CAR002_Kod6");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_Kod7
        {
            get { return cAR002_Kod7; }
            set
            {
                cAR002_Kod7 = value;
                OnPropertyChanged("CAR002_Kod7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Kod8
        {
            get { return cAR002_Kod8; }
            set
            {
                cAR002_Kod8 = value;
                OnPropertyChanged("CAR002_Kod8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Kod9
        {
            get { return cAR002_Kod9; }
            set
            {
                cAR002_Kod9 = value;
                OnPropertyChanged("CAR002_Kod9");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulanacakFiyat
        {
            get { return cAR002_UygulanacakFiyat; }
            set
            {
                cAR002_UygulanacakFiyat = value;
                OnPropertyChanged("CAR002_UygulanacakFiyat");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulanacakBankaKodu
        {
            get { return cAR002_UygulanacakBankaKodu; }
            set
            {
                cAR002_UygulanacakBankaKodu = value;
                OnPropertyChanged("CAR002_UygulanacakBankaKodu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_UygulanacakFiyatTipi
        {
            get { return cAR002_UygulanacakFiyatTipi; }
            set
            {
                cAR002_UygulanacakFiyatTipi = value;
                OnPropertyChanged("CAR002_UygulanacakFiyatTipi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulananFiyat
        {
            get { return cAR002_UygulananFiyat; }
            set
            {
                cAR002_UygulananFiyat = value;
                OnPropertyChanged("CAR002_UygulananFiyat");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_UygulananBankaKodu
        {
            get { return cAR002_UygulananBankaKodu; }
            set
            {
                cAR002_UygulananBankaKodu = value;
                OnPropertyChanged("CAR002_UygulananBankaKodu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_UygulananFiyatTipi
        {
            get { return cAR002_UygulananFiyatTipi; }
            set
            {
                cAR002_UygulananFiyatTipi = value;
                OnPropertyChanged("CAR002_UygulananFiyatTipi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Yetkili1
        {
            get { return cAR002_Yetkili1; }
            set
            {
                cAR002_Yetkili1 = value;
                OnPropertyChanged("CAR002_Yetkili1");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string CAR002_Yetkili1Gorevi
        {
            get { return cAR002_Yetkili1Gorevi; }
            set
            {
                cAR002_Yetkili1Gorevi = value;
                OnPropertyChanged("CAR002_Yetkili1Gorevi");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_Yetkili1DahiliNo
        {
            get { return cAR002_Yetkili1DahiliNo; }
            set
            {
                cAR002_Yetkili1DahiliNo = value;
                OnPropertyChanged("CAR002_Yetkili1DahiliNo");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_Yetkili1EMail
        {
            get { return cAR002_Yetkili1EMail; }
            set
            {
                cAR002_Yetkili1EMail = value;
                OnPropertyChanged("CAR002_Yetkili1EMail");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Yetkili2
        {
            get { return cAR002_Yetkili2; }
            set
            {
                cAR002_Yetkili2 = value;
                OnPropertyChanged("CAR002_Yetkili2");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string CAR002_Yetkili2Gorevi
        {
            get { return cAR002_Yetkili2Gorevi; }
            set
            {
                cAR002_Yetkili2Gorevi = value;
                OnPropertyChanged("CAR002_Yetkili2Gorevi");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_Yetkili2DahiliNo
        {
            get { return cAR002_Yetkili2DahiliNo; }
            set
            {
                cAR002_Yetkili2DahiliNo = value;
                OnPropertyChanged("CAR002_Yetkili2DahiliNo");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_Yetkili2EMail
        {
            get { return cAR002_Yetkili2EMail; }
            set
            {
                cAR002_Yetkili2EMail = value;
                OnPropertyChanged("CAR002_Yetkili2EMail");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string CAR002_BankaHesapKodu
        {
            get { return cAR002_BankaHesapKodu; }
            set
            {
                cAR002_BankaHesapKodu = value;
                OnPropertyChanged("CAR002_BankaHesapKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_BankaAdi
        {
            get { return cAR002_BankaAdi; }
            set
            {
                cAR002_BankaAdi = value;
                OnPropertyChanged("CAR002_BankaAdi");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_BankaSubeKodu
        {
            get { return cAR002_BankaSubeKodu; }
            set
            {
                cAR002_BankaSubeKodu = value;
                OnPropertyChanged("CAR002_BankaSubeKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_BankaHesapNo
        {
            get { return cAR002_BankaHesapNo; }
            set
            {
                cAR002_BankaHesapNo = value;
                OnPropertyChanged("CAR002_BankaHesapNo");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_KrediKartNo
        {
            get { return cAR002_KrediKartNo; }
            set
            {
                cAR002_KrediKartNo = value;
                OnPropertyChanged("CAR002_KrediKartNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_OdemeGunu
        {
            get { return cAR002_OdemeGunu; }
            set
            {
                cAR002_OdemeGunu = value;
                OnPropertyChanged("CAR002_OdemeGunu");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_ParaBirimi
        {
            get { return cAR002_ParaBirimi; }
            set
            {
                cAR002_ParaBirimi = value;
                OnPropertyChanged("CAR002_ParaBirimi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_MutabakatTarihi
        {
            get { return cAR002_MutabakatTarihi; }
            set
            {
                cAR002_MutabakatTarihi = value;
                OnPropertyChanged("CAR002_MutabakatTarihi");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_MutabakatBakiyesi
        {
            get { return cAR002_MutabakatBakiyesi; }
            set
            {
                cAR002_MutabakatBakiyesi = value;
                OnPropertyChanged("CAR002_MutabakatBakiyesi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimYeri1
        {
            get { return cAR002_TeslimYeri1; }
            set
            {
                cAR002_TeslimYeri1 = value;
                OnPropertyChanged("CAR002_TeslimYeri1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimYeri2
        {
            get { return cAR002_TeslimYeri2; }
            set
            {
                cAR002_TeslimYeri2 = value;
                OnPropertyChanged("CAR002_TeslimYeri2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimAdresi1
        {
            get { return cAR002_TeslimAdresi1; }
            set
            {
                cAR002_TeslimAdresi1 = value;
                OnPropertyChanged("CAR002_TeslimAdresi1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimAdresi2
        {
            get { return cAR002_TeslimAdresi2; }
            set
            {
                cAR002_TeslimAdresi2 = value;
                OnPropertyChanged("CAR002_TeslimAdresi2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KrediLimiti
        {
            get { return cAR002_KrediLimiti; }
            set
            {
                cAR002_KrediLimiti = value;
                OnPropertyChanged("CAR002_KrediLimiti");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirSenetRiskiBorc
        {
            get { return cAR002_DevirSenetRiskiBorc; }
            set
            {
                cAR002_DevirSenetRiskiBorc = value;
                OnPropertyChanged("CAR002_DevirSenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirSenetRiskiAlacak
        {
            get { return cAR002_DevirSenetRiskiAlacak; }
            set
            {
                cAR002_DevirSenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_DevirSenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirCekRiskiBorc
        {
            get { return cAR002_DevirCekRiskiBorc; }
            set
            {
                cAR002_DevirCekRiskiBorc = value;
                OnPropertyChanged("CAR002_DevirCekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirCekRiskiAlacak
        {
            get { return cAR002_DevirCekRiskiAlacak; }
            set
            {
                cAR002_DevirCekRiskiAlacak = value;
                OnPropertyChanged("CAR002_DevirCekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat1Borc
        {
            get { return cAR002_DevirTeminat1Borc; }
            set
            {
                cAR002_DevirTeminat1Borc = value;
                OnPropertyChanged("CAR002_DevirTeminat1Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat1Alacak
        {
            get { return cAR002_DevirTeminat1Alacak; }
            set
            {
                cAR002_DevirTeminat1Alacak = value;
                OnPropertyChanged("CAR002_DevirTeminat1Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat2Borc
        {
            get { return cAR002_DevirTeminat2Borc; }
            set
            {
                cAR002_DevirTeminat2Borc = value;
                OnPropertyChanged("CAR002_DevirTeminat2Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirTeminat2Alacak
        {
            get { return cAR002_DevirTeminat2Alacak; }
            set
            {
                cAR002_DevirTeminat2Alacak = value;
                OnPropertyChanged("CAR002_DevirTeminat2Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirProtestoSenet
        {
            get { return cAR002_DevirProtestoSenet; }
            set
            {
                cAR002_DevirProtestoSenet = value;
                OnPropertyChanged("CAR002_DevirProtestoSenet");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_DevirKarsiliksizCek
        {
            get { return cAR002_DevirKarsiliksizCek; }
            set
            {
                cAR002_DevirKarsiliksizCek = value;
                OnPropertyChanged("CAR002_DevirKarsiliksizCek");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_SenetRiskiBorc
        {
            get { return cAR002_SenetRiskiBorc; }
            set
            {
                cAR002_SenetRiskiBorc = value;
                OnPropertyChanged("CAR002_SenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_SenetRiskiAlacak
        {
            get { return cAR002_SenetRiskiAlacak; }
            set
            {
                cAR002_SenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_SenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CekRiskiBorc
        {
            get { return cAR002_CekRiskiBorc; }
            set
            {
                cAR002_CekRiskiBorc = value;
                OnPropertyChanged("CAR002_CekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_CekRiskiAlacak
        {
            get { return cAR002_CekRiskiAlacak; }
            set
            {
                cAR002_CekRiskiAlacak = value;
                OnPropertyChanged("CAR002_CekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat1Borc
        {
            get { return cAR002_Teminat1Borc; }
            set
            {
                cAR002_Teminat1Borc = value;
                OnPropertyChanged("CAR002_Teminat1Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat1Alacak
        {
            get { return cAR002_Teminat1Alacak; }
            set
            {
                cAR002_Teminat1Alacak = value;
                OnPropertyChanged("CAR002_Teminat1Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat2Borc
        {
            get { return cAR002_Teminat2Borc; }
            set
            {
                cAR002_Teminat2Borc = value;
                OnPropertyChanged("CAR002_Teminat2Borc");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_Teminat2Alacak
        {
            get { return cAR002_Teminat2Alacak; }
            set
            {
                cAR002_Teminat2Alacak = value;
                OnPropertyChanged("CAR002_Teminat2Alacak");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_ProtestoSenet
        {
            get { return cAR002_ProtestoSenet; }
            set
            {
                cAR002_ProtestoSenet = value;
                OnPropertyChanged("CAR002_ProtestoSenet");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_KarsiliksizCek
        {
            get { return cAR002_KarsiliksizCek; }
            set
            {
                cAR002_KarsiliksizCek = value;
                OnPropertyChanged("CAR002_KarsiliksizCek");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonBorcTarihi
        {
            get { return cAR002_SonBorcTarihi; }
            set
            {
                cAR002_SonBorcTarihi = value;
                OnPropertyChanged("CAR002_SonBorcTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonAlacakTarihi
        {
            get { return cAR002_SonAlacakTarihi; }
            set
            {
                cAR002_SonAlacakTarihi = value;
                OnPropertyChanged("CAR002_SonAlacakTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonRiskBorcTarihi
        {
            get { return cAR002_SonRiskBorcTarihi; }
            set
            {
                cAR002_SonRiskBorcTarihi = value;
                OnPropertyChanged("CAR002_SonRiskBorcTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SonRiskAlacakTarihi
        {
            get { return cAR002_SonRiskAlacakTarihi; }
            set
            {
                cAR002_SonRiskAlacakTarihi = value;
                OnPropertyChanged("CAR002_SonRiskAlacakTarihi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar1
        {
            get { return cAR002_Notlar1; }
            set
            {
                cAR002_Notlar1 = value;
                OnPropertyChanged("CAR002_Notlar1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar2
        {
            get { return cAR002_Notlar2; }
            set
            {
                cAR002_Notlar2 = value;
                OnPropertyChanged("CAR002_Notlar2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar3
        {
            get { return cAR002_Notlar3; }
            set
            {
                cAR002_Notlar3 = value;
                OnPropertyChanged("CAR002_Notlar3");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar4
        {
            get { return cAR002_Notlar4; }
            set
            {
                cAR002_Notlar4 = value;
                OnPropertyChanged("CAR002_Notlar4");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar5
        {
            get { return cAR002_Notlar5; }
            set
            {
                cAR002_Notlar5 = value;
                OnPropertyChanged("CAR002_Notlar5");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar6
        {
            get { return cAR002_Notlar6; }
            set
            {
                cAR002_Notlar6 = value;
                OnPropertyChanged("CAR002_Notlar6");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Notlar7
        {
            get { return cAR002_Notlar7; }
            set
            {
                cAR002_Notlar7 = value;
                OnPropertyChanged("CAR002_Notlar7");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_MasrafMerkezi
        {
            get { return cAR002_MasrafMerkezi; }
            set
            {
                cAR002_MasrafMerkezi = value;
                OnPropertyChanged("CAR002_MasrafMerkezi");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string CAR002_Sifre
        {
            get { return cAR002_Sifre; }
            set
            {
                cAR002_Sifre = value;
                OnPropertyChanged("CAR002_Sifre");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Resim
        {
            get { return cAR002_Resim; }
            set
            {
                cAR002_Resim = value;
                OnPropertyChanged("CAR002_Resim");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? CAR002_YaslandirmaBakiye
        {
            get { return cAR002_YaslandirmaBakiye; }
            set
            {
                cAR002_YaslandirmaBakiye = value;
                OnPropertyChanged("CAR002_YaslandirmaBakiye");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_YaslandirmaTarihi
        {
            get { return cAR002_YaslandirmaTarihi; }
            set
            {
                cAR002_YaslandirmaTarihi = value;
                OnPropertyChanged("CAR002_YaslandirmaTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_YaslandirmaGunu
        {
            get { return cAR002_YaslandirmaGunu; }
            set
            {
                cAR002_YaslandirmaGunu = value;
                OnPropertyChanged("CAR002_YaslandirmaGunu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_OdemeTarihi
        {
            get { return cAR002_OdemeTarihi; }
            set
            {
                cAR002_OdemeTarihi = value;
                OnPropertyChanged("CAR002_OdemeTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizMutabakatBakiyesi
        {
            get { return cAR002_DovizMutabakatBakiyesi; }
            set
            {
                cAR002_DovizMutabakatBakiyesi = value;
                OnPropertyChanged("CAR002_DovizMutabakatBakiyesi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirBorc
        {
            get { return cAR002_DovizDevirBorc; }
            set
            {
                cAR002_DovizDevirBorc = value;
                OnPropertyChanged("CAR002_DovizDevirBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirAlacak
        {
            get { return cAR002_DovizDevirAlacak; }
            set
            {
                cAR002_DovizDevirAlacak = value;
                OnPropertyChanged("CAR002_DovizDevirAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDigerBorc
        {
            get { return cAR002_DovizDigerBorc; }
            set
            {
                cAR002_DovizDigerBorc = value;
                OnPropertyChanged("CAR002_DovizDigerBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDigerAlacak
        {
            get { return cAR002_DovizDigerAlacak; }
            set
            {
                cAR002_DovizDigerAlacak = value;
                OnPropertyChanged("CAR002_DovizDigerAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizOdemeBorc
        {
            get { return cAR002_DovizOdemeBorc; }
            set
            {
                cAR002_DovizOdemeBorc = value;
                OnPropertyChanged("CAR002_DovizOdemeBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizOdemeAlacak
        {
            get { return cAR002_DovizOdemeAlacak; }
            set
            {
                cAR002_DovizOdemeAlacak = value;
                OnPropertyChanged("CAR002_DovizOdemeAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCiroBorc
        {
            get { return cAR002_DovizCiroBorc; }
            set
            {
                cAR002_DovizCiroBorc = value;
                OnPropertyChanged("CAR002_DovizCiroBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCiroAlacak
        {
            get { return cAR002_DovizCiroAlacak; }
            set
            {
                cAR002_DovizCiroAlacak = value;
                OnPropertyChanged("CAR002_DovizCiroAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizIadeBorc
        {
            get { return cAR002_DovizIadeBorc; }
            set
            {
                cAR002_DovizIadeBorc = value;
                OnPropertyChanged("CAR002_DovizIadeBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizIadeAlacak
        {
            get { return cAR002_DovizIadeAlacak; }
            set
            {
                cAR002_DovizIadeAlacak = value;
                OnPropertyChanged("CAR002_DovizIadeAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKDVBorc
        {
            get { return cAR002_DovizKDVBorc; }
            set
            {
                cAR002_DovizKDVBorc = value;
                OnPropertyChanged("CAR002_DovizKDVBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKDVAlacak
        {
            get { return cAR002_DovizKDVAlacak; }
            set
            {
                cAR002_DovizKDVAlacak = value;
                OnPropertyChanged("CAR002_DovizKDVAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_Dovizotvborc
        {
            get { return cAR002_Dovizotvborc; }
            set
            {
                cAR002_Dovizotvborc = value;
                OnPropertyChanged("CAR002_Dovizotvborc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_Dovizotvalacak
        {
            get { return cAR002_Dovizotvalacak; }
            set
            {
                cAR002_Dovizotvalacak = value;
                OnPropertyChanged("CAR002_Dovizotvalacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKrediLimiti
        {
            get { return cAR002_DovizKrediLimiti; }
            set
            {
                cAR002_DovizKrediLimiti = value;
                OnPropertyChanged("CAR002_DovizKrediLimiti");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirSenetRiskiBorc
        {
            get { return cAR002_DovizDevirSenetRiskiBorc; }
            set
            {
                cAR002_DovizDevirSenetRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizDevirSenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirSenetRiskiAlacak
        {
            get { return cAR002_DovizDevirSenetRiskiAlacak; }
            set
            {
                cAR002_DovizDevirSenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizDevirSenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirCekRiskiBorc
        {
            get { return cAR002_DovizDevirCekRiskiBorc; }
            set
            {
                cAR002_DovizDevirCekRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizDevirCekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirCekRiskiAlacak
        {
            get { return cAR002_DovizDevirCekRiskiAlacak; }
            set
            {
                cAR002_DovizDevirCekRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizDevirCekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat1Borc
        {
            get { return cAR002_DovizDevirTeminat1Borc; }
            set
            {
                cAR002_DovizDevirTeminat1Borc = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat1Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat1Alacak
        {
            get { return cAR002_DovizDevirTeminat1Alacak; }
            set
            {
                cAR002_DovizDevirTeminat1Alacak = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat1Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat2Borc
        {
            get { return cAR002_DovizDevirTeminat2Borc; }
            set
            {
                cAR002_DovizDevirTeminat2Borc = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat2Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirTeminat2Alacak
        {
            get { return cAR002_DovizDevirTeminat2Alacak; }
            set
            {
                cAR002_DovizDevirTeminat2Alacak = value;
                OnPropertyChanged("CAR002_DovizDevirTeminat2Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirProtestoSenet
        {
            get { return cAR002_DovizDevirProtestoSenet; }
            set
            {
                cAR002_DovizDevirProtestoSenet = value;
                OnPropertyChanged("CAR002_DovizDevirProtestoSenet");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizDevirKarsiliksizCek
        {
            get { return cAR002_DovizDevirKarsiliksizCek; }
            set
            {
                cAR002_DovizDevirKarsiliksizCek = value;
                OnPropertyChanged("CAR002_DovizDevirKarsiliksizCek");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizSenetRiskiBorc
        {
            get { return cAR002_DovizSenetRiskiBorc; }
            set
            {
                cAR002_DovizSenetRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizSenetRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizSenetRiskiAlacak
        {
            get { return cAR002_DovizSenetRiskiAlacak; }
            set
            {
                cAR002_DovizSenetRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizSenetRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCekRiskiBorc
        {
            get { return cAR002_DovizCekRiskiBorc; }
            set
            {
                cAR002_DovizCekRiskiBorc = value;
                OnPropertyChanged("CAR002_DovizCekRiskiBorc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizCekRiskiAlacak
        {
            get { return cAR002_DovizCekRiskiAlacak; }
            set
            {
                cAR002_DovizCekRiskiAlacak = value;
                OnPropertyChanged("CAR002_DovizCekRiskiAlacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat1Borc
        {
            get { return cAR002_DovizTeminat1Borc; }
            set
            {
                cAR002_DovizTeminat1Borc = value;
                OnPropertyChanged("CAR002_DovizTeminat1Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat1Alacak
        {
            get { return cAR002_DovizTeminat1Alacak; }
            set
            {
                cAR002_DovizTeminat1Alacak = value;
                OnPropertyChanged("CAR002_DovizTeminat1Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat2Borc
        {
            get { return cAR002_DovizTeminat2Borc; }
            set
            {
                cAR002_DovizTeminat2Borc = value;
                OnPropertyChanged("CAR002_DovizTeminat2Borc");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizTeminat2Alacak
        {
            get { return cAR002_DovizTeminat2Alacak; }
            set
            {
                cAR002_DovizTeminat2Alacak = value;
                OnPropertyChanged("CAR002_DovizTeminat2Alacak");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizProtestoSenet
        {
            get { return cAR002_DovizProtestoSenet; }
            set
            {
                cAR002_DovizProtestoSenet = value;
                OnPropertyChanged("CAR002_DovizProtestoSenet");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? CAR002_DovizKarsiliksizCek
        {
            get { return cAR002_DovizKarsiliksizCek; }
            set
            {
                cAR002_DovizKarsiliksizCek = value;
                OnPropertyChanged("CAR002_DovizKarsiliksizCek");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_Ulke
        {
            get { return cAR002_Ulke; }
            set
            {
                cAR002_Ulke = value;
                OnPropertyChanged("CAR002_Ulke");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_CariFormB
        {
            get { return cAR002_CariFormB; }
            set
            {
                cAR002_CariFormB = value;
                OnPropertyChanged("CAR002_CariFormB");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_FormBUnvanFlag
        {
            get { return cAR002_FormBUnvanFlag; }
            set
            {
                cAR002_FormBUnvanFlag = value;
                OnPropertyChanged("CAR002_FormBUnvanFlag");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_VergiDairesiKodu
        {
            get { return cAR002_VergiDairesiKodu; }
            set
            {
                cAR002_VergiDairesiKodu = value;
                OnPropertyChanged("CAR002_VergiDairesiKodu");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_BankaIBANNo
        {
            get { return cAR002_BankaIBANNo; }
            set
            {
                cAR002_BankaIBANNo = value;
                OnPropertyChanged("CAR002_BankaIBANNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_AktifFlag
        {
            get { return cAR002_AktifFlag; }
            set
            {
                cAR002_AktifFlag = value;
                OnPropertyChanged("CAR002_AktifFlag");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_HesapTipi
        {
            get { return cAR002_HesapTipi; }
            set
            {
                cAR002_HesapTipi = value;
                OnPropertyChanged("CAR002_HesapTipi");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_YetkiliCep
        {
            get { return cAR002_YetkiliCep; }
            set
            {
                cAR002_YetkiliCep = value;
                OnPropertyChanged("CAR002_YetkiliCep");
            }
        }

        /// <summary> NVARCHAR (24) Allow Null </summary>
        public string CAR002_YetkiliCep2
        {
            get { return cAR002_YetkiliCep2; }
            set
            {
                cAR002_YetkiliCep2 = value;
                OnPropertyChanged("CAR002_YetkiliCep2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_TeslimAdresi3
        {
            get { return cAR002_TeslimAdresi3; }
            set
            {
                cAR002_TeslimAdresi3 = value;
                OnPropertyChanged("CAR002_TeslimAdresi3");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Dokuman1
        {
            get { return cAR002_Dokuman1; }
            set
            {
                cAR002_Dokuman1 = value;
                OnPropertyChanged("CAR002_Dokuman1");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Dokuman2
        {
            get { return cAR002_Dokuman2; }
            set
            {
                cAR002_Dokuman2 = value;
                OnPropertyChanged("CAR002_Dokuman2");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_Dokuman3
        {
            get { return cAR002_Dokuman3; }
            set
            {
                cAR002_Dokuman3 = value;
                OnPropertyChanged("CAR002_Dokuman3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu1
        {
            get { return cAR002_BankaKodu1; }
            set
            {
                cAR002_BankaKodu1 = value;
                OnPropertyChanged("CAR002_BankaKodu1");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi1
        {
            get { return cAR002_BankaAdi1; }
            set
            {
                cAR002_BankaAdi1 = value;
                OnPropertyChanged("CAR002_BankaAdi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu1
        {
            get { return cAR002_SubeKodu1; }
            set
            {
                cAR002_SubeKodu1 = value;
                OnPropertyChanged("CAR002_SubeKodu1");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN1
        {
            get { return cAR002_IBAN1; }
            set
            {
                cAR002_IBAN1 = value;
                OnPropertyChanged("CAR002_IBAN1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu2
        {
            get { return cAR002_BankaKodu2; }
            set
            {
                cAR002_BankaKodu2 = value;
                OnPropertyChanged("CAR002_BankaKodu2");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi2
        {
            get { return cAR002_BankaAdi2; }
            set
            {
                cAR002_BankaAdi2 = value;
                OnPropertyChanged("CAR002_BankaAdi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu2
        {
            get { return cAR002_SubeKodu2; }
            set
            {
                cAR002_SubeKodu2 = value;
                OnPropertyChanged("CAR002_SubeKodu2");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN2
        {
            get { return cAR002_IBAN2; }
            set
            {
                cAR002_IBAN2 = value;
                OnPropertyChanged("CAR002_IBAN2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu3
        {
            get { return cAR002_BankaKodu3; }
            set
            {
                cAR002_BankaKodu3 = value;
                OnPropertyChanged("CAR002_BankaKodu3");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi3
        {
            get { return cAR002_BankaAdi3; }
            set
            {
                cAR002_BankaAdi3 = value;
                OnPropertyChanged("CAR002_BankaAdi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu3
        {
            get { return cAR002_SubeKodu3; }
            set
            {
                cAR002_SubeKodu3 = value;
                OnPropertyChanged("CAR002_SubeKodu3");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN3
        {
            get { return cAR002_IBAN3; }
            set
            {
                cAR002_IBAN3 = value;
                OnPropertyChanged("CAR002_IBAN3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_BankaKodu4
        {
            get { return cAR002_BankaKodu4; }
            set
            {
                cAR002_BankaKodu4 = value;
                OnPropertyChanged("CAR002_BankaKodu4");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string CAR002_BankaAdi4
        {
            get { return cAR002_BankaAdi4; }
            set
            {
                cAR002_BankaAdi4 = value;
                OnPropertyChanged("CAR002_BankaAdi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_SubeKodu4
        {
            get { return cAR002_SubeKodu4; }
            set
            {
                cAR002_SubeKodu4 = value;
                OnPropertyChanged("CAR002_SubeKodu4");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_IBAN4
        {
            get { return cAR002_IBAN4; }
            set
            {
                cAR002_IBAN4 = value;
                OnPropertyChanged("CAR002_IBAN4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_FiyatListeNoAlis
        {
            get { return cAR002_FiyatListeNoAlis; }
            set
            {
                cAR002_FiyatListeNoAlis = value;
                OnPropertyChanged("CAR002_FiyatListeNoAlis");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_FiyatListeNoSatis
        {
            get { return cAR002_FiyatListeNoSatis; }
            set
            {
                cAR002_FiyatListeNoSatis = value;
                OnPropertyChanged("CAR002_FiyatListeNoSatis");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_IrsaliyeFormNo
        {
            get { return cAR002_IrsaliyeFormNo; }
            set
            {
                cAR002_IrsaliyeFormNo = value;
                OnPropertyChanged("CAR002_IrsaliyeFormNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_FaturaFormNo
        {
            get { return cAR002_FaturaFormNo; }
            set
            {
                cAR002_FaturaFormNo = value;
                OnPropertyChanged("CAR002_FaturaFormNo");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_ILKodu
        {
            get { return cAR002_ILKodu; }
            set
            {
                cAR002_ILKodu = value;
                OnPropertyChanged("CAR002_ILKodu");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_ILCEKodu
        {
            get { return cAR002_ILCEKodu; }
            set
            {
                cAR002_ILCEKodu = value;
                OnPropertyChanged("CAR002_ILCEKodu");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_PostaKodu
        {
            get { return cAR002_PostaKodu; }
            set
            {
                cAR002_PostaKodu = value;
                OnPropertyChanged("CAR002_PostaKodu");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_IrsaliyeRGNFormName
        {
            get { return cAR002_IrsaliyeRGNFormName; }
            set
            {
                cAR002_IrsaliyeRGNFormName = value;
                OnPropertyChanged("CAR002_IrsaliyeRGNFormName");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string CAR002_FaturaRGNFormName
        {
            get { return cAR002_FaturaRGNFormName; }
            set
            {
                cAR002_FaturaRGNFormName = value;
                OnPropertyChanged("CAR002_FaturaRGNFormName");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_GMEnlem
        {
            get { return cAR002_GMEnlem; }
            set
            {
                cAR002_GMEnlem = value;
                OnPropertyChanged("CAR002_GMEnlem");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_GMBoylam
        {
            get { return cAR002_GMBoylam; }
            set
            {
                cAR002_GMBoylam = value;
                OnPropertyChanged("CAR002_GMBoylam");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi1
        {
            get { return cAR002_DovizCinsi1; }
            set
            {
                cAR002_DovizCinsi1 = value;
                OnPropertyChanged("CAR002_DovizCinsi1");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi2
        {
            get { return cAR002_DovizCinsi2; }
            set
            {
                cAR002_DovizCinsi2 = value;
                OnPropertyChanged("CAR002_DovizCinsi2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi3
        {
            get { return cAR002_DovizCinsi3; }
            set
            {
                cAR002_DovizCinsi3 = value;
                OnPropertyChanged("CAR002_DovizCinsi3");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string CAR002_DovizCinsi4
        {
            get { return cAR002_DovizCinsi4; }
            set
            {
                cAR002_DovizCinsi4 = value;
                OnPropertyChanged("CAR002_DovizCinsi4");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_KDVTevkTabi
        {
            get { return cAR002_KDVTevkTabi; }
            set
            {
                cAR002_KDVTevkTabi = value;
                OnPropertyChanged("CAR002_KDVTevkTabi");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string CAR002_EFaturaKullanici
        {
            get { return cAR002_EFaturaKullanici; }
            set
            {
                cAR002_EFaturaKullanici = value;
                OnPropertyChanged("CAR002_EFaturaKullanici");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_EFaturaSenaryo
        {
            get { return cAR002_EFaturaSenaryo; }
            set
            {
                cAR002_EFaturaSenaryo = value;
                OnPropertyChanged("CAR002_EFaturaSenaryo");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaCadde
        {
            get { return cAR002_EFaturaCadde; }
            set
            {
                cAR002_EFaturaCadde = value;
                OnPropertyChanged("CAR002_EFaturaCadde");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaBinaAdi
        {
            get { return cAR002_EFaturaBinaAdi; }
            set
            {
                cAR002_EFaturaBinaAdi = value;
                OnPropertyChanged("CAR002_EFaturaBinaAdi");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_EFaturaDisKapiNo
        {
            get { return cAR002_EFaturaDisKapiNo; }
            set
            {
                cAR002_EFaturaDisKapiNo = value;
                OnPropertyChanged("CAR002_EFaturaDisKapiNo");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string CAR002_EFaturaIcKapiNo
        {
            get { return cAR002_EFaturaIcKapiNo; }
            set
            {
                cAR002_EFaturaIcKapiNo = value;
                OnPropertyChanged("CAR002_EFaturaIcKapiNo");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaKasabaKoy
        {
            get { return cAR002_EFaturaKasabaKoy; }
            set
            {
                cAR002_EFaturaKasabaKoy = value;
                OnPropertyChanged("CAR002_EFaturaKasabaKoy");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaIlce
        {
            get { return cAR002_EFaturaIlce; }
            set
            {
                cAR002_EFaturaIlce = value;
                OnPropertyChanged("CAR002_EFaturaIlce");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_EFaturaSehir
        {
            get { return cAR002_EFaturaSehir; }
            set
            {
                cAR002_EFaturaSehir = value;
                OnPropertyChanged("CAR002_EFaturaSehir");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string CAR002_EFaturaEtiketNo
        {
            get { return cAR002_EFaturaEtiketNo; }
            set
            {
                cAR002_EFaturaEtiketNo = value;
                OnPropertyChanged("CAR002_EFaturaEtiketNo");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Adi
        {
            get { return cAR002_Adi; }
            set
            {
                cAR002_Adi = value;
                OnPropertyChanged("CAR002_Adi");
            }
        }

        /// <summary> NVARCHAR (80) Allow Null </summary>
        public string CAR002_Soyadi
        {
            get { return cAR002_Soyadi; }
            set
            {
                cAR002_Soyadi = value;
                OnPropertyChanged("CAR002_Soyadi");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaMusterBirimSubeTanim
        {
            get { return cAR002_EFaturaMusterBirimSubeTanim; }
            set
            {
                cAR002_EFaturaMusterBirimSubeTanim = value;
                OnPropertyChanged("CAR002_EFaturaMusterBirimSubeTanim");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaMusterBirimSubeTanimDeger
        {
            get { return cAR002_EFaturaMusterBirimSubeTanimDeger; }
            set
            {
                cAR002_EFaturaMusterBirimSubeTanimDeger = value;
                OnPropertyChanged("CAR002_EFaturaMusterBirimSubeTanimDeger");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaCHKayitKodTanim
        {
            get { return cAR002_EFaturaCHKayitKodTanim; }
            set
            {
                cAR002_EFaturaCHKayitKodTanim = value;
                OnPropertyChanged("CAR002_EFaturaCHKayitKodTanim");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string CAR002_EFaturaCHKayitKod
        {
            get { return cAR002_EFaturaCHKayitKod; }
            set
            {
                cAR002_EFaturaCHKayitKod = value;
                OnPropertyChanged("CAR002_EFaturaCHKayitKod");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? CAR002_IskNedenTabloNo
        {
            get { return cAR002_IskNedenTabloNo; }
            set
            {
                cAR002_IskNedenTabloNo = value;
                OnPropertyChanged("CAR002_IskNedenTabloNo");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_KDVdenMuaf
        {
            get { return cAR002_KDVdenMuaf; }
            set
            {
                cAR002_KDVdenMuaf = value;
                OnPropertyChanged("CAR002_KDVdenMuaf");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_EArsivFaturaTeslimSekli
        {
            get { return cAR002_EArsivFaturaTeslimSekli; }
            set
            {
                cAR002_EArsivFaturaTeslimSekli = value;
                OnPropertyChanged("CAR002_EArsivFaturaTeslimSekli");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EArsivTeslimEMailAdresi1
        {
            get { return cAR002_EArsivTeslimEMailAdresi1; }
            set
            {
                cAR002_EArsivTeslimEMailAdresi1 = value;
                OnPropertyChanged("CAR002_EArsivTeslimEMailAdresi1");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EArsivTeslimEMailAdresi2
        {
            get { return cAR002_EArsivTeslimEMailAdresi2; }
            set
            {
                cAR002_EArsivTeslimEMailAdresi2 = value;
                OnPropertyChanged("CAR002_EArsivTeslimEMailAdresi2");
            }
        }

        /// <summary> NVARCHAR (128) Allow Null </summary>
        public string CAR002_EArsivTeslimEMailAdresi3
        {
            get { return cAR002_EArsivTeslimEMailAdresi3; }
            set
            {
                cAR002_EArsivTeslimEMailAdresi3 = value;
                OnPropertyChanged("CAR002_EArsivTeslimEMailAdresi3");
            }
        }

        /// <summary> NVARCHAR (512) Allow Null </summary>
        public string CAR002_EArsivGoruntulemeDosyasi
        {
            get { return cAR002_EArsivGoruntulemeDosyasi; }
            set
            {
                cAR002_EArsivGoruntulemeDosyasi = value;
                OnPropertyChanged("CAR002_EArsivGoruntulemeDosyasi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? CAR002_EArsivUnvanAdresFaturadan
        {
            get { return cAR002_EArsivUnvanAdresFaturadan; }
            set
            {
                cAR002_EArsivUnvanAdresFaturadan = value;
                OnPropertyChanged("CAR002_EArsivUnvanAdresFaturadan");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? CAR002_YOKCBankaBkmID
        {
            get { return cAR002_YOKCBankaBkmID; }
            set
            {
                cAR002_YOKCBankaBkmID = value;
                OnPropertyChanged("CAR002_YOKCBankaBkmID");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string pk_CAR002_HesapKodu
        {
            get { return _pk_CAR002_HesapKodu; }
            set
            {
                _pk_CAR002_HesapKodu = value;
                OnPropertyChanged("pk_CAR002_HesapKodu");
            }
        }

        #endregion /// Properties       
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(CAR002E Property, object deger, Operand and_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(CAR002E), Property), deger, and_Or));
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

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "YNS{0}.YNS{0}.CAR002";
        string[] info_PrimaryKeys = { "pk_CAR002_HesapKodu" };
        string[] info_IdentityKeys = { "CAR002_Row_ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public CAR002()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += CAR002_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void CAR002_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!ChangedProperties.Contains(e.PropertyName))
            {
                ChangedProperties.Add(e.PropertyName);
            }
        }

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion  /// Tablo Bilgileri & Metodlar
    }
}