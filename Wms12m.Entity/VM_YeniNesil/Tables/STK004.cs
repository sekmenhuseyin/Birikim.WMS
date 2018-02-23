using System;
using System.Collections.Generic;
using System.ComponentModel;
using Birikim.Helpers;

namespace Wms12m.Entity
{
    #region STK004E Enum 
    public enum STK004E
    {
        STK004_Row_ID,
        STK004_MalKodu,
        STK004_Aciklama,
        STK004_GrupKodu,
        STK004_Iskonto,
        STK004_KDV,
        STK004_Birim1,
        STK004_Birim2,
        STK004_Birim3,
        STK004_BorcluHesap,
        STK004_AlacakliHesap,
        STK004_AlisFiyati1,
        STK004_AlisKDV1,
        STK004_AlisBirim1,
        STK004_AlisFiyati2,
        STK004_AlisKDV2,
        STK004_AlisBirim2,
        STK004_AlisFiyati3,
        STK004_AlisKDV3,
        STK004_AlisBirim3,
        STK004_SatisFiyati1,
        STK004_SatisKDV1,
        STK004_SatisBirim1,
        STK004_SatisFiyati2,
        STK004_SatisKDV2,
        STK004_SatisBirim2,
        STK004_SatisFiyati3,
        STK004_SatisKDV3,
        STK004_SatisBirim3,
        STK004_DevirMiktari,
        STK004_DevirTutari,
        STK004_GirisMiktari,
        STK004_GirisTutari,
        STK004_GirisIskonto,
        STK004_CikisMiktari,
        STK004_CikisTutari,
        STK004_CikisIskonto,
        STK004_KritikSeviye,
        STK004_DevirTarihi,
        STK004_KafileBuyuklugu,
        STK004_GirenKaynak,
        STK004_GirenTarih,
        STK004_GirenSaat,
        STK004_GirenKodu,
        STK004_GirenSurum,
        STK004_DegistirenKaynak,
        STK004_DegistirenTarih,
        STK004_DegistirenSaat,
        STK004_DegistirenKodu,
        STK004_DegistirenSurum,
        STK004_OzelKodu,
        STK004_TipKodu,
        STK004_Kod4,
        STK004_Kod5,
        STK004_Kod6,
        STK004_Kod7,
        STK004_Kod8,
        STK004_Kod9,
        STK004_Kod10,
        STK004_Kod11,
        STK004_Kod12,
        STK004_UreticiKodu1,
        STK004_UreticiKodu2,
        STK004_UreticiKodu3,
        STK004_MusterekMalKodu,
        STK004_MaliyetSekli,
        STK004_FireOrani,
        STK004_TeminYeri,
        STK004_TeminSuresi,
        STK004_Mensei,
        STK004_GTIPN,
        STK004_GumrukOrani,
        STK004_Fon,
        STK004_DovizAlis1,
        STK004_DovizAlisCinsi1,
        STK004_DovizAlis2,
        STK004_DovizAlisCinsi2,
        STK004_DovizAlis3,
        STK004_DovizAlisCinsi3,
        STK004_DovizSatis1,
        STK004_DovizSatisCinsi1,
        STK004_DovizSatis2,
        STK004_DovizSatisCinsi2,
        STK004_DovizSatis3,
        STK004_DovizSatisCinsi3,
        STK004_AlisSiparisi,
        STK004_SatisSiparisi,
        STK004_AzamiSeviye,
        STK004_SonGirisTarihi,
        STK004_SonCikisTarihi,
        STK004_BirimAgirligi,
        STK004_ValorGun,
        STK004_Barkod1,
        STK004_Barkod2,
        STK004_Barkod3,
        STK004_Aciklama2,
        STK004_Aciklama3,
        STK004_UygMaliyetTipi,
        STK004_SonMaliyetBirimFiyati,
        STK004_SonMaliyetTarihi,
        STK004_SonMaliyetTipi,
        STK004_SonMaliyetParaBirimi,
        STK004_Rezervasyon,
        STK004_AlimdanIade,
        STK004_SatistanIade,
        STK004_MalKodu2,
        STK004_DovizCinsi,
        STK004_SonAlimFaturasiTarihi,
        STK004_SonAlimFaturasiNo,
        STK004_SonAlimFaturasiBirimFiyati,
        STK004_SonAlimFaturasiCariHesapKodu,
        STK004_SonAlimFaturasiDovizBirimFiyati,
        STK004_SonSatisFaturasiTarihi,
        STK004_SonSatisFaturasiNo,
        STK004_SonSatisFaturasiBirimFiyati,
        STK004_SonSatisFaturasiCariHesapKodu,
        STK004_SonSatisFaturasiDovizBirimFiyati,
        STK004_MasrafMerkezi,
        STK004_ResimDosyasi,
        STK004_FiyatListesindeCikart,
        STK004_SatisFiyati1ValorGun,
        STK004_SatisFiyati2ValorGun,
        STK004_SatisFiyati3ValorGun,
        STK004_DovizSatisFiyati1ValorGun,
        STK004_DovizSatisFiyati2ValorGun,
        STK004_DovizSatisFiyati3ValorGun,
        STK004_DovizDevirTutari,
        STK004_DovizGirisTutari,
        STK004_DovizGirisIskontoTutari,
        STK004_DovizCikisTutari,
        STK004_DovizCikisIskontoTutari,
        STK004_BarkodBirim1,
        STK004_BarkodBirim2,
        STK004_BarkodBirim3,
        STK004_BarkodCarpan1,
        STK004_BarkodCarpan2,
        STK004_BarkodCarpan3,
        STK004_DevirMiktari2,
        STK004_GirisMiktari2,
        STK004_GirisTutari2,
        STK004_CikisMiktari2,
        STK004_CikisTutari2,
        STK004_DepoKodu1,
        STK004_DepoDevirMiktari21,
        STK004_DepoDevirTutari21,
        STK004_DepoGirisMiktari1,
        STK004_DepoGirisTutari1,
        STK004_DepoGirisIskonto1,
        STK004_DepoCikisMiktari1,
        STK004_DepoCikisTutari1,
        STK004_DepoCikisIskonto1,
        STK004_DepoSonMaliyetBf1,
        STK004_DepoSonMaliyetTipi1,
        STK004_DepoSonMaliyetTarihi1,
        STK004_DepoSonMaliyetPrBr1,
        STK004_DepoSonDevirTarihi1,
        STK004_DepoSonGirisTarihi1,
        STK004_DepoSonCikisTarihi1,
        STK004_DepoKodu2,
        STK004_DepoDevirMiktari22,
        STK004_DepoDevirTutari22,
        STK004_DepoGirisMiktari2,
        STK004_DepoGirisTutari2,
        STK004_DepoGirisIskonto2,
        STK004_DepoCikisMiktari2,
        STK004_DepoCikisTutari2,
        STK004_DepoCikisIskonto2,
        STK004_DepoSonMaliyetBf2,
        STK004_DepoSonMaliyetTipi2,
        STK004_DepoSonMaliyetTarihi2,
        STK004_DepoSonMaliyetPrBr2,
        STK004_DepoSonDevirTarihi2,
        STK004_DepoSonGirisTarihi2,
        STK004_DepoSonCikisTarihi2,
        STK004_DepoKodu3,
        STK004_DepoDevirMiktari23,
        STK004_DepoDevirTutari23,
        STK004_DepoGirisMiktari3,
        STK004_DepoGirisTutari3,
        STK004_DepoGirisIskonto3,
        STK004_DepoCikisMiktari3,
        STK004_DepoCikisTutari3,
        STK004_DepoCikisIskonto3,
        STK004_DepoSonMaliyetBf3,
        STK004_DepoSonMaliyetTipi3,
        STK004_DepoSonMaliyetTarihi3,
        STK004_DepoSonMaliyetPrBr3,
        STK004_DepoSonDevirTarihi3,
        STK004_DepoSonGirisTarihi3,
        STK004_DepoSonCikisTarihi3,
        STK004_DepoKodu4,
        STK004_DepoDevirMiktari24,
        STK004_DepoDevirTutari24,
        STK004_DepoGirisMiktari4,
        STK004_DepoGirisTutari4,
        STK004_DepoGirisIskonto4,
        STK004_DepoCikisMiktari4,
        STK004_DepoCikisTutari4,
        STK004_DepoCikisIskonto4,
        STK004_DepoSonMaliyetBf4,
        STK004_DepoSonMaliyetTipi4,
        STK004_DepoSonMaliyetTarihi4,
        STK004_DepoSonMaliyetPrBr4,
        STK004_DepoSonDevirTarihi4,
        STK004_DepoSonGirisTarihi4,
        STK004_DepoSonCikisTarihi4,
        STK004_DepoKodu5,
        STK004_DepoDevirMiktari25,
        STK004_DepoDevirTutari25,
        STK004_DepoGirisMiktari5,
        STK004_DepoGirisTutari5,
        STK004_DepoGirisIskonto5,
        STK004_DepoCikisMiktari5,
        STK004_DepoCikisTutari5,
        STK004_DepoCikisIskonto5,
        STK004_DepoSonMaliyetBf5,
        STK004_DepoSonMaliyetTipi5,
        STK004_DepoSonMaliyetTarihi5,
        STK004_DepoSonMaliyetPrBr5,
        STK004_DepoSonDevirTarihi5,
        STK004_DepoSonGirisTarihi5,
        STK004_DepoSonCikisTarihi5,
        STK004_DepoKodu6,
        STK004_DepoDevirMiktari26,
        STK004_DepoDevirTutari26,
        STK004_DepoGirisMiktari6,
        STK004_DepoGirisTutari6,
        STK004_DepoGirisIskonto6,
        STK004_DepoCikisMiktari6,
        STK004_DepoCikisTutari6,
        STK004_DepoCikisIskonto6,
        STK004_DepoSonMaliyetBf6,
        STK004_DepoSonMaliyetTipi6,
        STK004_DepoSonMaliyetTarihi6,
        STK004_DepoSonMaliyetPrBr6,
        STK004_DepoSonDevirTarihi6,
        STK004_DepoSonGirisTarihi6,
        STK004_DepoSonCikisTarihi6,
        STK004_DepoKodu7,
        STK004_DepoDevirMiktari27,
        STK004_DepoDevirTutari27,
        STK004_DepoGirisMiktari7,
        STK004_DepoGirisTutari7,
        STK004_DepoGirisIskonto7,
        STK004_DepoCikisMiktari7,
        STK004_DepoCikisTutari7,
        STK004_DepoCikisIskonto7,
        STK004_DepoSonMaliyetBf7,
        STK004_DepoSonMaliyetTipi7,
        STK004_DepoSonMaliyetTarihi7,
        STK004_DepoSonMaliyetPrBr7,
        STK004_DepoSonDevirTarihi7,
        STK004_DepoSonGirisTarihi7,
        STK004_DepoSonCikisTarihi7,
        STK004_DepoKodu8,
        STK004_DepoDevirMiktari28,
        STK004_DepoDevirTutari28,
        STK004_DepoGirisMiktari8,
        STK004_DepoGirisTutari8,
        STK004_DepoGirisIskonto8,
        STK004_DepoCikisMiktari8,
        STK004_DepoCikisTutari8,
        STK004_DepoCikisIskonto8,
        STK004_DepoSonMaliyetBf8,
        STK004_DepoSonMaliyetTipi8,
        STK004_DepoSonMaliyetTarihi8,
        STK004_DepoSonMaliyetPrBr8,
        STK004_DepoSonDevirTarihi8,
        STK004_DepoSonGirisTarihi8,
        STK004_DepoSonCikisTarihi8,
        STK004_DepoKodu9,
        STK004_DepoDevirMiktari29,
        STK004_DepoDevirTutari29,
        STK004_DepoGirisMiktari9,
        STK004_DepoGirisTutari9,
        STK004_DepoGirisIskonto9,
        STK004_DepoCikisMiktari9,
        STK004_DepoCikisTutari9,
        STK004_DepoCikisIskonto9,
        STK004_DepoSonMaliyetBf9,
        STK004_DepoSonMaliyetTipi9,
        STK004_DepoSonMaliyetTarihi9,
        STK004_DepoSonMaliyetPrBr9,
        STK004_DepoSonDevirTarihi9,
        STK004_DepoSonGirisTarihi9,
        STK004_DepoSonCikisTarihi9,
        STK004_DepoKodu10,
        STK004_DepoDevirMiktari210,
        STK004_DepoDevirTutari210,
        STK004_DepoGirisMiktari10,
        STK004_DepoGirisTutari10,
        STK004_DepoGirisIskonto10,
        STK004_DepoCikisMiktari10,
        STK004_DepoCikisTutari10,
        STK004_DepoCikisIskonto10,
        STK004_DepoSonMaliyetBf10,
        STK004_DepoSonMaliyetTipi10,
        STK004_DepoSonMaliyetTarihi10,
        STK004_DepoSonMaliyetPrBr10,
        STK004_DepoSonDevirTarihi10,
        STK004_DepoSonGirisTarihi10,
        STK004_DepoSonCikisTarihi10,
        STK004_StokMiktar2Br,
        STK004_StokMCH1,
        STK004_MOP1,
        STK004_StokMCH2,
        STK004_MOP2,
        STK004_StokMCH3,
        STK004_MOP3,
        STK004_StokMCH4,
        STK004_MOP4,
        STK004_StokMCH5,
        STK004_MOP5,
        STK004_StokMSOP,
        STK004_StokDevirTutar2,
        STK004_Not1,
        STK004_Not2,
        STK004_Not3,
        STK004_Not4,
        STK004_Not5,
        STK004_Not6,
        STK004_Not7,
        STK004_StokDBMiktari,
        STK004_StokDBTutari,
        STK004_StokDBDuzTutari,
        STK004_StokDBDuzDate,
        STK004_StokDBDuzYil,
        STK004_StokDBDuzDonem,
        STK004_StokDBRofm,
        STK004_StokDBEntHesapKodu,
        STK004_YASatisFiati1,
        STK004_YSatisKDV1,
        STK004_YSatisBirim1,
        STK004_YASatisFiati2,
        STK004_YSatisKDV2,
        STK004_YSatisBirim2,
        STK004_YASatisFiati3,
        STK004_YSatisKDV3,
        STK004_YSatisBirim3,
        STK004_YSatisFiyati1ValorGun,
        STK004_YSatisFiyati2ValorGun,
        STK004_YSatisFiyati3ValorGun,
        STK004_AktifFlag,
        STK004_SayimTarihi,
        STK004_SayimMiktari,
        STK004_Dokuman1,
        STK004_Dokuman2,
        STK004_Dokuman3,
        STK004_SMMHesapKodu,
        STK004_KDVTevkIslemTuruAlim,
        STK004_Barkod4,
        STK004_Barkod5,
        STK004_BarkodBirim4,
        STK004_BarkodBirim5,
        STK004_BarkodCarpan4,
        STK004_BarkodCarpan5,
        STK004_KDVTevkIslemTuruSatis,
        STK004_KDVTevkOraniAlim,
        STK004_KDVTevkOraniSatis,
        STK004_KismiKDVMuafMevcut,
        STK004_KDVMuafAciklama,
        STK004_BoyutBirimi,
        STK004_BoyutEn,
        STK004_BoyutBoy,
        STK004_BoyutGenisilik,
        STK004_AgirlikBirimi,
        STK004_AgirlikBrut,
        STK004_AgirlikNet,
        STK004_HacimBirimi,
        STK004_HacimBrut,
        STK004_HacimNet,
        STK004_YOKCPLUGonder,
        STK004_YOKCPLUID,
        STK004_YOKCDepartmanID

    }
    #endregion /// STK004E Enum                
    public class STK004 : INotifyPropertyChanged
    {
        #region Properties
        #region Fields  
        int sTK004_Row_ID;
        string sTK004_MalKodu;
        string sTK004_Aciklama;
        string sTK004_GrupKodu;
        decimal? sTK004_Iskonto;
        byte? sTK004_KDV;
        string sTK004_Birim1;
        string sTK004_Birim2;
        string sTK004_Birim3;
        string sTK004_BorcluHesap;
        string sTK004_AlacakliHesap;
        decimal? sTK004_AlisFiyati1;
        byte? sTK004_AlisKDV1;
        byte? sTK004_AlisBirim1;
        decimal? sTK004_AlisFiyati2;
        byte? sTK004_AlisKDV2;
        byte? sTK004_AlisBirim2;
        decimal? sTK004_AlisFiyati3;
        byte? sTK004_AlisKDV3;
        byte? sTK004_AlisBirim3;
        decimal? sTK004_SatisFiyati1;
        byte? sTK004_SatisKDV1;
        byte? sTK004_SatisBirim1;
        decimal? sTK004_SatisFiyati2;
        byte? sTK004_SatisKDV2;
        byte? sTK004_SatisBirim2;
        decimal? sTK004_SatisFiyati3;
        byte? sTK004_SatisKDV3;
        byte? sTK004_SatisBirim3;
        decimal? sTK004_DevirMiktari;
        decimal? sTK004_DevirTutari;
        decimal? sTK004_GirisMiktari;
        decimal? sTK004_GirisTutari;
        decimal? sTK004_GirisIskonto;
        decimal? sTK004_CikisMiktari;
        decimal? sTK004_CikisTutari;
        decimal? sTK004_CikisIskonto;
        decimal? sTK004_KritikSeviye;
        int? sTK004_DevirTarihi;
        decimal? sTK004_KafileBuyuklugu;
        string sTK004_GirenKaynak;
        int? sTK004_GirenTarih;
        string sTK004_GirenSaat;
        string sTK004_GirenKodu;
        string sTK004_GirenSurum;
        string sTK004_DegistirenKaynak;
        int? sTK004_DegistirenTarih;
        string sTK004_DegistirenSaat;
        string sTK004_DegistirenKodu;
        string sTK004_DegistirenSurum;
        string sTK004_OzelKodu;
        string sTK004_TipKodu;
        string sTK004_Kod4;
        string sTK004_Kod5;
        string sTK004_Kod6;
        string sTK004_Kod7;
        string sTK004_Kod8;
        string sTK004_Kod9;
        string sTK004_Kod10;
        decimal? sTK004_Kod11;
        decimal? sTK004_Kod12;
        string sTK004_UreticiKodu1;
        string sTK004_UreticiKodu2;
        string sTK004_UreticiKodu3;
        string sTK004_MusterekMalKodu;
        byte? sTK004_MaliyetSekli;
        decimal? sTK004_FireOrani;
        string sTK004_TeminYeri;
        short? sTK004_TeminSuresi;
        string sTK004_Mensei;
        string sTK004_GTIPN;
        decimal? sTK004_GumrukOrani;
        decimal? sTK004_Fon;
        decimal? sTK004_DovizAlis1;
        string sTK004_DovizAlisCinsi1;
        decimal? sTK004_DovizAlis2;
        string sTK004_DovizAlisCinsi2;
        decimal? sTK004_DovizAlis3;
        string sTK004_DovizAlisCinsi3;
        decimal? sTK004_DovizSatis1;
        string sTK004_DovizSatisCinsi1;
        decimal? sTK004_DovizSatis2;
        string sTK004_DovizSatisCinsi2;
        decimal? sTK004_DovizSatis3;
        string sTK004_DovizSatisCinsi3;
        decimal? sTK004_AlisSiparisi;
        decimal? sTK004_SatisSiparisi;
        decimal? sTK004_AzamiSeviye;
        int? sTK004_SonGirisTarihi;
        int? sTK004_SonCikisTarihi;
        decimal? sTK004_BirimAgirligi;
        int? sTK004_ValorGun;
        string sTK004_Barkod1;
        string sTK004_Barkod2;
        string sTK004_Barkod3;
        string sTK004_Aciklama2;
        string sTK004_Aciklama3;
        short? sTK004_UygMaliyetTipi;
        decimal? sTK004_SonMaliyetBirimFiyati;
        int? sTK004_SonMaliyetTarihi;
        short? sTK004_SonMaliyetTipi;
        byte? sTK004_SonMaliyetParaBirimi;
        decimal? sTK004_Rezervasyon;
        string sTK004_AlimdanIade;
        string sTK004_SatistanIade;
        string sTK004_MalKodu2;
        string sTK004_DovizCinsi;
        int? sTK004_SonAlimFaturasiTarihi;
        string sTK004_SonAlimFaturasiNo;
        decimal? sTK004_SonAlimFaturasiBirimFiyati;
        string sTK004_SonAlimFaturasiCariHesapKodu;
        decimal? sTK004_SonAlimFaturasiDovizBirimFiyati;
        int? sTK004_SonSatisFaturasiTarihi;
        string sTK004_SonSatisFaturasiNo;
        decimal? sTK004_SonSatisFaturasiBirimFiyati;
        string sTK004_SonSatisFaturasiCariHesapKodu;
        decimal? sTK004_SonSatisFaturasiDovizBirimFiyati;
        string sTK004_MasrafMerkezi;
        string sTK004_ResimDosyasi;
        byte? sTK004_FiyatListesindeCikart;
        int? sTK004_SatisFiyati1ValorGun;
        int? sTK004_SatisFiyati2ValorGun;
        int? sTK004_SatisFiyati3ValorGun;
        int? sTK004_DovizSatisFiyati1ValorGun;
        int? sTK004_DovizSatisFiyati2ValorGun;
        int? sTK004_DovizSatisFiyati3ValorGun;
        decimal? sTK004_DovizDevirTutari;
        decimal? sTK004_DovizGirisTutari;
        decimal? sTK004_DovizGirisIskontoTutari;
        decimal? sTK004_DovizCikisTutari;
        decimal? sTK004_DovizCikisIskontoTutari;
        string sTK004_BarkodBirim1;
        string sTK004_BarkodBirim2;
        string sTK004_BarkodBirim3;
        decimal? sTK004_BarkodCarpan1;
        decimal? sTK004_BarkodCarpan2;
        decimal? sTK004_BarkodCarpan3;
        decimal? sTK004_DevirMiktari2;
        decimal? sTK004_GirisMiktari2;
        decimal? sTK004_GirisTutari2;
        decimal? sTK004_CikisMiktari2;
        decimal? sTK004_CikisTutari2;
        string sTK004_DepoKodu1;
        decimal? sTK004_DepoDevirMiktari21;
        decimal? sTK004_DepoDevirTutari21;
        decimal? sTK004_DepoGirisMiktari1;
        decimal? sTK004_DepoGirisTutari1;
        decimal? sTK004_DepoGirisIskonto1;
        decimal? sTK004_DepoCikisMiktari1;
        decimal? sTK004_DepoCikisTutari1;
        decimal? sTK004_DepoCikisIskonto1;
        decimal? sTK004_DepoSonMaliyetBf1;
        short? sTK004_DepoSonMaliyetTipi1;
        int? sTK004_DepoSonMaliyetTarihi1;
        byte? sTK004_DepoSonMaliyetPrBr1;
        int? sTK004_DepoSonDevirTarihi1;
        int? sTK004_DepoSonGirisTarihi1;
        int? sTK004_DepoSonCikisTarihi1;
        string sTK004_DepoKodu2;
        decimal? sTK004_DepoDevirMiktari22;
        decimal? sTK004_DepoDevirTutari22;
        decimal? sTK004_DepoGirisMiktari2;
        decimal? sTK004_DepoGirisTutari2;
        decimal? sTK004_DepoGirisIskonto2;
        decimal? sTK004_DepoCikisMiktari2;
        decimal? sTK004_DepoCikisTutari2;
        decimal? sTK004_DepoCikisIskonto2;
        decimal? sTK004_DepoSonMaliyetBf2;
        short? sTK004_DepoSonMaliyetTipi2;
        int? sTK004_DepoSonMaliyetTarihi2;
        byte? sTK004_DepoSonMaliyetPrBr2;
        int? sTK004_DepoSonDevirTarihi2;
        int? sTK004_DepoSonGirisTarihi2;
        int? sTK004_DepoSonCikisTarihi2;
        string sTK004_DepoKodu3;
        decimal? sTK004_DepoDevirMiktari23;
        decimal? sTK004_DepoDevirTutari23;
        decimal? sTK004_DepoGirisMiktari3;
        decimal? sTK004_DepoGirisTutari3;
        decimal? sTK004_DepoGirisIskonto3;
        decimal? sTK004_DepoCikisMiktari3;
        decimal? sTK004_DepoCikisTutari3;
        decimal? sTK004_DepoCikisIskonto3;
        decimal? sTK004_DepoSonMaliyetBf3;
        short? sTK004_DepoSonMaliyetTipi3;
        int? sTK004_DepoSonMaliyetTarihi3;
        byte? sTK004_DepoSonMaliyetPrBr3;
        int? sTK004_DepoSonDevirTarihi3;
        int? sTK004_DepoSonGirisTarihi3;
        int? sTK004_DepoSonCikisTarihi3;
        string sTK004_DepoKodu4;
        decimal? sTK004_DepoDevirMiktari24;
        decimal? sTK004_DepoDevirTutari24;
        decimal? sTK004_DepoGirisMiktari4;
        decimal? sTK004_DepoGirisTutari4;
        decimal? sTK004_DepoGirisIskonto4;
        decimal? sTK004_DepoCikisMiktari4;
        decimal? sTK004_DepoCikisTutari4;
        decimal? sTK004_DepoCikisIskonto4;
        decimal? sTK004_DepoSonMaliyetBf4;
        short? sTK004_DepoSonMaliyetTipi4;
        int? sTK004_DepoSonMaliyetTarihi4;
        byte? sTK004_DepoSonMaliyetPrBr4;
        int? sTK004_DepoSonDevirTarihi4;
        int? sTK004_DepoSonGirisTarihi4;
        int? sTK004_DepoSonCikisTarihi4;
        string sTK004_DepoKodu5;
        decimal? sTK004_DepoDevirMiktari25;
        decimal? sTK004_DepoDevirTutari25;
        decimal? sTK004_DepoGirisMiktari5;
        decimal? sTK004_DepoGirisTutari5;
        decimal? sTK004_DepoGirisIskonto5;
        decimal? sTK004_DepoCikisMiktari5;
        decimal? sTK004_DepoCikisTutari5;
        decimal? sTK004_DepoCikisIskonto5;
        decimal? sTK004_DepoSonMaliyetBf5;
        short? sTK004_DepoSonMaliyetTipi5;
        int? sTK004_DepoSonMaliyetTarihi5;
        byte? sTK004_DepoSonMaliyetPrBr5;
        int? sTK004_DepoSonDevirTarihi5;
        int? sTK004_DepoSonGirisTarihi5;
        int? sTK004_DepoSonCikisTarihi5;
        string sTK004_DepoKodu6;
        decimal? sTK004_DepoDevirMiktari26;
        decimal? sTK004_DepoDevirTutari26;
        decimal? sTK004_DepoGirisMiktari6;
        decimal? sTK004_DepoGirisTutari6;
        decimal? sTK004_DepoGirisIskonto6;
        decimal? sTK004_DepoCikisMiktari6;
        decimal? sTK004_DepoCikisTutari6;
        decimal? sTK004_DepoCikisIskonto6;
        decimal? sTK004_DepoSonMaliyetBf6;
        short? sTK004_DepoSonMaliyetTipi6;
        int? sTK004_DepoSonMaliyetTarihi6;
        byte? sTK004_DepoSonMaliyetPrBr6;
        int? sTK004_DepoSonDevirTarihi6;
        int? sTK004_DepoSonGirisTarihi6;
        int? sTK004_DepoSonCikisTarihi6;
        string sTK004_DepoKodu7;
        decimal? sTK004_DepoDevirMiktari27;
        decimal? sTK004_DepoDevirTutari27;
        decimal? sTK004_DepoGirisMiktari7;
        decimal? sTK004_DepoGirisTutari7;
        decimal? sTK004_DepoGirisIskonto7;
        decimal? sTK004_DepoCikisMiktari7;
        decimal? sTK004_DepoCikisTutari7;
        decimal? sTK004_DepoCikisIskonto7;
        decimal? sTK004_DepoSonMaliyetBf7;
        short? sTK004_DepoSonMaliyetTipi7;
        int? sTK004_DepoSonMaliyetTarihi7;
        byte? sTK004_DepoSonMaliyetPrBr7;
        int? sTK004_DepoSonDevirTarihi7;
        int? sTK004_DepoSonGirisTarihi7;
        int? sTK004_DepoSonCikisTarihi7;
        string sTK004_DepoKodu8;
        decimal? sTK004_DepoDevirMiktari28;
        decimal? sTK004_DepoDevirTutari28;
        decimal? sTK004_DepoGirisMiktari8;
        decimal? sTK004_DepoGirisTutari8;
        decimal? sTK004_DepoGirisIskonto8;
        decimal? sTK004_DepoCikisMiktari8;
        decimal? sTK004_DepoCikisTutari8;
        decimal? sTK004_DepoCikisIskonto8;
        decimal? sTK004_DepoSonMaliyetBf8;
        short? sTK004_DepoSonMaliyetTipi8;
        int? sTK004_DepoSonMaliyetTarihi8;
        byte? sTK004_DepoSonMaliyetPrBr8;
        int? sTK004_DepoSonDevirTarihi8;
        int? sTK004_DepoSonGirisTarihi8;
        int? sTK004_DepoSonCikisTarihi8;
        string sTK004_DepoKodu9;
        decimal? sTK004_DepoDevirMiktari29;
        decimal? sTK004_DepoDevirTutari29;
        decimal? sTK004_DepoGirisMiktari9;
        decimal? sTK004_DepoGirisTutari9;
        decimal? sTK004_DepoGirisIskonto9;
        decimal? sTK004_DepoCikisMiktari9;
        decimal? sTK004_DepoCikisTutari9;
        decimal? sTK004_DepoCikisIskonto9;
        decimal? sTK004_DepoSonMaliyetBf9;
        short? sTK004_DepoSonMaliyetTipi9;
        int? sTK004_DepoSonMaliyetTarihi9;
        byte? sTK004_DepoSonMaliyetPrBr9;
        int? sTK004_DepoSonDevirTarihi9;
        int? sTK004_DepoSonGirisTarihi9;
        int? sTK004_DepoSonCikisTarihi9;
        string sTK004_DepoKodu10;
        decimal? sTK004_DepoDevirMiktari210;
        decimal? sTK004_DepoDevirTutari210;
        decimal? sTK004_DepoGirisMiktari10;
        decimal? sTK004_DepoGirisTutari10;
        decimal? sTK004_DepoGirisIskonto10;
        decimal? sTK004_DepoCikisMiktari10;
        decimal? sTK004_DepoCikisTutari10;
        decimal? sTK004_DepoCikisIskonto10;
        decimal? sTK004_DepoSonMaliyetBf10;
        short? sTK004_DepoSonMaliyetTipi10;
        int? sTK004_DepoSonMaliyetTarihi10;
        byte? sTK004_DepoSonMaliyetPrBr10;
        int? sTK004_DepoSonDevirTarihi10;
        int? sTK004_DepoSonGirisTarihi10;
        int? sTK004_DepoSonCikisTarihi10;
        string sTK004_StokMiktar2Br;
        byte? sTK004_StokMCH1;
        string sTK004_MOP1;
        byte? sTK004_StokMCH2;
        string sTK004_MOP2;
        byte? sTK004_StokMCH3;
        string sTK004_MOP3;
        byte? sTK004_StokMCH4;
        string sTK004_MOP4;
        byte? sTK004_StokMCH5;
        string sTK004_MOP5;
        byte? sTK004_StokMSOP;
        decimal? sTK004_StokDevirTutar2;
        string sTK004_Not1;
        string sTK004_Not2;
        string sTK004_Not3;
        string sTK004_Not4;
        string sTK004_Not5;
        string sTK004_Not6;
        string sTK004_Not7;
        decimal? sTK004_StokDBMiktari;
        decimal? sTK004_StokDBTutari;
        decimal? sTK004_StokDBDuzTutari;
        int? sTK004_StokDBDuzDate;
        int? sTK004_StokDBDuzYil;
        byte? sTK004_StokDBDuzDonem;
        decimal? sTK004_StokDBRofm;
        string sTK004_StokDBEntHesapKodu;
        decimal? sTK004_YASatisFiati1;
        byte? sTK004_YSatisKDV1;
        byte? sTK004_YSatisBirim1;
        decimal? sTK004_YASatisFiati2;
        byte? sTK004_YSatisKDV2;
        byte? sTK004_YSatisBirim2;
        decimal? sTK004_YASatisFiati3;
        byte? sTK004_YSatisKDV3;
        byte? sTK004_YSatisBirim3;
        int? sTK004_YSatisFiyati1ValorGun;
        int? sTK004_YSatisFiyati2ValorGun;
        int? sTK004_YSatisFiyati3ValorGun;
        byte? sTK004_AktifFlag;
        int? sTK004_SayimTarihi;
        decimal? sTK004_SayimMiktari;
        string sTK004_Dokuman1;
        string sTK004_Dokuman2;
        string sTK004_Dokuman3;
        string sTK004_SMMHesapKodu;
        int? sTK004_KDVTevkIslemTuruAlim;
        string sTK004_Barkod4;
        string sTK004_Barkod5;
        string sTK004_BarkodBirim4;
        string sTK004_BarkodBirim5;
        decimal? sTK004_BarkodCarpan4;
        decimal? sTK004_BarkodCarpan5;
        int? sTK004_KDVTevkIslemTuruSatis;
        string sTK004_KDVTevkOraniAlim;
        string sTK004_KDVTevkOraniSatis;
        byte? sTK004_KismiKDVMuafMevcut;
        string sTK004_KDVMuafAciklama;
        string sTK004_BoyutBirimi;
        decimal? sTK004_BoyutEn;
        decimal? sTK004_BoyutBoy;
        decimal? sTK004_BoyutGenisilik;
        string sTK004_AgirlikBirimi;
        decimal? sTK004_AgirlikBrut;
        decimal? sTK004_AgirlikNet;
        string sTK004_HacimBirimi;
        decimal? sTK004_HacimBrut;
        decimal? sTK004_HacimNet;
        byte? sTK004_YOKCPLUGonder;
        int? sTK004_YOKCPLUID;
        int? sTK004_YOKCDepartmanID;
        // private int _pk_STK004_Row_ID;

        string _pk_STK004_MalKodu;
        #endregion /// Fields

        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int STK004_Row_ID => sTK004_Row_ID;

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_MalKodu
        {
            get { return sTK004_MalKodu; }
            set
            {
                sTK004_MalKodu = value;
                OnPropertyChanged("STK004_MalKodu");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string STK004_Aciklama
        {
            get { return sTK004_Aciklama; }
            set
            {
                sTK004_Aciklama = value;
                OnPropertyChanged("STK004_Aciklama");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_GrupKodu
        {
            get { return sTK004_GrupKodu; }
            set
            {
                sTK004_GrupKodu = value;
                OnPropertyChanged("STK004_GrupKodu");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_Iskonto
        {
            get { return sTK004_Iskonto; }
            set
            {
                sTK004_Iskonto = value;
                OnPropertyChanged("STK004_Iskonto");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_KDV
        {
            get { return sTK004_KDV; }
            set
            {
                sTK004_KDV = value;
                OnPropertyChanged("STK004_KDV");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_Birim1
        {
            get { return sTK004_Birim1; }
            set
            {
                sTK004_Birim1 = value;
                OnPropertyChanged("STK004_Birim1");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_Birim2
        {
            get { return sTK004_Birim2; }
            set
            {
                sTK004_Birim2 = value;
                OnPropertyChanged("STK004_Birim2");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_Birim3
        {
            get { return sTK004_Birim3; }
            set
            {
                sTK004_Birim3 = value;
                OnPropertyChanged("STK004_Birim3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_BorcluHesap
        {
            get { return sTK004_BorcluHesap; }
            set
            {
                sTK004_BorcluHesap = value;
                OnPropertyChanged("STK004_BorcluHesap");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_AlacakliHesap
        {
            get { return sTK004_AlacakliHesap; }
            set
            {
                sTK004_AlacakliHesap = value;
                OnPropertyChanged("STK004_AlacakliHesap");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_AlisFiyati1
        {
            get { return sTK004_AlisFiyati1; }
            set
            {
                sTK004_AlisFiyati1 = value;
                OnPropertyChanged("STK004_AlisFiyati1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisKDV1
        {
            get { return sTK004_AlisKDV1; }
            set
            {
                sTK004_AlisKDV1 = value;
                OnPropertyChanged("STK004_AlisKDV1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisBirim1
        {
            get { return sTK004_AlisBirim1; }
            set
            {
                sTK004_AlisBirim1 = value;
                OnPropertyChanged("STK004_AlisBirim1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_AlisFiyati2
        {
            get { return sTK004_AlisFiyati2; }
            set
            {
                sTK004_AlisFiyati2 = value;
                OnPropertyChanged("STK004_AlisFiyati2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisKDV2
        {
            get { return sTK004_AlisKDV2; }
            set
            {
                sTK004_AlisKDV2 = value;
                OnPropertyChanged("STK004_AlisKDV2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisBirim2
        {
            get { return sTK004_AlisBirim2; }
            set
            {
                sTK004_AlisBirim2 = value;
                OnPropertyChanged("STK004_AlisBirim2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_AlisFiyati3
        {
            get { return sTK004_AlisFiyati3; }
            set
            {
                sTK004_AlisFiyati3 = value;
                OnPropertyChanged("STK004_AlisFiyati3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisKDV3
        {
            get { return sTK004_AlisKDV3; }
            set
            {
                sTK004_AlisKDV3 = value;
                OnPropertyChanged("STK004_AlisKDV3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisBirim3
        {
            get { return sTK004_AlisBirim3; }
            set
            {
                sTK004_AlisBirim3 = value;
                OnPropertyChanged("STK004_AlisBirim3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SatisFiyati1
        {
            get { return sTK004_SatisFiyati1; }
            set
            {
                sTK004_SatisFiyati1 = value;
                OnPropertyChanged("STK004_SatisFiyati1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisKDV1
        {
            get { return sTK004_SatisKDV1; }
            set
            {
                sTK004_SatisKDV1 = value;
                OnPropertyChanged("STK004_SatisKDV1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisBirim1
        {
            get { return sTK004_SatisBirim1; }
            set
            {
                sTK004_SatisBirim1 = value;
                OnPropertyChanged("STK004_SatisBirim1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SatisFiyati2
        {
            get { return sTK004_SatisFiyati2; }
            set
            {
                sTK004_SatisFiyati2 = value;
                OnPropertyChanged("STK004_SatisFiyati2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisKDV2
        {
            get { return sTK004_SatisKDV2; }
            set
            {
                sTK004_SatisKDV2 = value;
                OnPropertyChanged("STK004_SatisKDV2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisBirim2
        {
            get { return sTK004_SatisBirim2; }
            set
            {
                sTK004_SatisBirim2 = value;
                OnPropertyChanged("STK004_SatisBirim2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SatisFiyati3
        {
            get { return sTK004_SatisFiyati3; }
            set
            {
                sTK004_SatisFiyati3 = value;
                OnPropertyChanged("STK004_SatisFiyati3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisKDV3
        {
            get { return sTK004_SatisKDV3; }
            set
            {
                sTK004_SatisKDV3 = value;
                OnPropertyChanged("STK004_SatisKDV3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisBirim3
        {
            get { return sTK004_SatisBirim3; }
            set
            {
                sTK004_SatisBirim3 = value;
                OnPropertyChanged("STK004_SatisBirim3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DevirMiktari
        {
            get { return sTK004_DevirMiktari; }
            set
            {
                sTK004_DevirMiktari = value;
                OnPropertyChanged("STK004_DevirMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DevirTutari
        {
            get { return sTK004_DevirTutari; }
            set
            {
                sTK004_DevirTutari = value;
                OnPropertyChanged("STK004_DevirTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_GirisMiktari
        {
            get { return sTK004_GirisMiktari; }
            set
            {
                sTK004_GirisMiktari = value;
                OnPropertyChanged("STK004_GirisMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_GirisTutari
        {
            get { return sTK004_GirisTutari; }
            set
            {
                sTK004_GirisTutari = value;
                OnPropertyChanged("STK004_GirisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_GirisIskonto
        {
            get { return sTK004_GirisIskonto; }
            set
            {
                sTK004_GirisIskonto = value;
                OnPropertyChanged("STK004_GirisIskonto");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_CikisMiktari
        {
            get { return sTK004_CikisMiktari; }
            set
            {
                sTK004_CikisMiktari = value;
                OnPropertyChanged("STK004_CikisMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_CikisTutari
        {
            get { return sTK004_CikisTutari; }
            set
            {
                sTK004_CikisTutari = value;
                OnPropertyChanged("STK004_CikisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_CikisIskonto
        {
            get { return sTK004_CikisIskonto; }
            set
            {
                sTK004_CikisIskonto = value;
                OnPropertyChanged("STK004_CikisIskonto");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_KritikSeviye
        {
            get { return sTK004_KritikSeviye; }
            set
            {
                sTK004_KritikSeviye = value;
                OnPropertyChanged("STK004_KritikSeviye");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DevirTarihi
        {
            get { return sTK004_DevirTarihi; }
            set
            {
                sTK004_DevirTarihi = value;
                OnPropertyChanged("STK004_DevirTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_KafileBuyuklugu
        {
            get { return sTK004_KafileBuyuklugu; }
            set
            {
                sTK004_KafileBuyuklugu = value;
                OnPropertyChanged("STK004_KafileBuyuklugu");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_GirenKaynak
        {
            get { return sTK004_GirenKaynak; }
            set
            {
                sTK004_GirenKaynak = value;
                OnPropertyChanged("STK004_GirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_GirenTarih
        {
            get { return sTK004_GirenTarih; }
            set
            {
                sTK004_GirenTarih = value;
                OnPropertyChanged("STK004_GirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_GirenSaat
        {
            get { return sTK004_GirenSaat; }
            set
            {
                sTK004_GirenSaat = value;
                OnPropertyChanged("STK004_GirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_GirenKodu
        {
            get { return sTK004_GirenKodu; }
            set
            {
                sTK004_GirenKodu = value;
                OnPropertyChanged("STK004_GirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_GirenSurum
        {
            get { return sTK004_GirenSurum; }
            set
            {
                sTK004_GirenSurum = value;
                OnPropertyChanged("STK004_GirenSurum");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_DegistirenKaynak
        {
            get { return sTK004_DegistirenKaynak; }
            set
            {
                sTK004_DegistirenKaynak = value;
                OnPropertyChanged("STK004_DegistirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DegistirenTarih
        {
            get { return sTK004_DegistirenTarih; }
            set
            {
                sTK004_DegistirenTarih = value;
                OnPropertyChanged("STK004_DegistirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_DegistirenSaat
        {
            get { return sTK004_DegistirenSaat; }
            set
            {
                sTK004_DegistirenSaat = value;
                OnPropertyChanged("STK004_DegistirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DegistirenKodu
        {
            get { return sTK004_DegistirenKodu; }
            set
            {
                sTK004_DegistirenKodu = value;
                OnPropertyChanged("STK004_DegistirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_DegistirenSurum
        {
            get { return sTK004_DegistirenSurum; }
            set
            {
                sTK004_DegistirenSurum = value;
                OnPropertyChanged("STK004_DegistirenSurum");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_OzelKodu
        {
            get { return sTK004_OzelKodu; }
            set
            {
                sTK004_OzelKodu = value;
                OnPropertyChanged("STK004_OzelKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_TipKodu
        {
            get { return sTK004_TipKodu; }
            set
            {
                sTK004_TipKodu = value;
                OnPropertyChanged("STK004_TipKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod4
        {
            get { return sTK004_Kod4; }
            set
            {
                sTK004_Kod4 = value;
                OnPropertyChanged("STK004_Kod4");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod5
        {
            get { return sTK004_Kod5; }
            set
            {
                sTK004_Kod5 = value;
                OnPropertyChanged("STK004_Kod5");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod6
        {
            get { return sTK004_Kod6; }
            set
            {
                sTK004_Kod6 = value;
                OnPropertyChanged("STK004_Kod6");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod7
        {
            get { return sTK004_Kod7; }
            set
            {
                sTK004_Kod7 = value;
                OnPropertyChanged("STK004_Kod7");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod8
        {
            get { return sTK004_Kod8; }
            set
            {
                sTK004_Kod8 = value;
                OnPropertyChanged("STK004_Kod8");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod9
        {
            get { return sTK004_Kod9; }
            set
            {
                sTK004_Kod9 = value;
                OnPropertyChanged("STK004_Kod9");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod10
        {
            get { return sTK004_Kod10; }
            set
            {
                sTK004_Kod10 = value;
                OnPropertyChanged("STK004_Kod10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Kod11
        {
            get { return sTK004_Kod11; }
            set
            {
                sTK004_Kod11 = value;
                OnPropertyChanged("STK004_Kod11");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Kod12
        {
            get { return sTK004_Kod12; }
            set
            {
                sTK004_Kod12 = value;
                OnPropertyChanged("STK004_Kod12");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_UreticiKodu1
        {
            get { return sTK004_UreticiKodu1; }
            set
            {
                sTK004_UreticiKodu1 = value;
                OnPropertyChanged("STK004_UreticiKodu1");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_UreticiKodu2
        {
            get { return sTK004_UreticiKodu2; }
            set
            {
                sTK004_UreticiKodu2 = value;
                OnPropertyChanged("STK004_UreticiKodu2");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_UreticiKodu3
        {
            get { return sTK004_UreticiKodu3; }
            set
            {
                sTK004_UreticiKodu3 = value;
                OnPropertyChanged("STK004_UreticiKodu3");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_MusterekMalKodu
        {
            get { return sTK004_MusterekMalKodu; }
            set
            {
                sTK004_MusterekMalKodu = value;
                OnPropertyChanged("STK004_MusterekMalKodu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_MaliyetSekli
        {
            get { return sTK004_MaliyetSekli; }
            set
            {
                sTK004_MaliyetSekli = value;
                OnPropertyChanged("STK004_MaliyetSekli");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_FireOrani
        {
            get { return sTK004_FireOrani; }
            set
            {
                sTK004_FireOrani = value;
                OnPropertyChanged("STK004_FireOrani");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_TeminYeri
        {
            get { return sTK004_TeminYeri; }
            set
            {
                sTK004_TeminYeri = value;
                OnPropertyChanged("STK004_TeminYeri");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_TeminSuresi
        {
            get { return sTK004_TeminSuresi; }
            set
            {
                sTK004_TeminSuresi = value;
                OnPropertyChanged("STK004_TeminSuresi");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_Mensei
        {
            get { return sTK004_Mensei; }
            set
            {
                sTK004_Mensei = value;
                OnPropertyChanged("STK004_Mensei");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_GTIPN
        {
            get { return sTK004_GTIPN; }
            set
            {
                sTK004_GTIPN = value;
                OnPropertyChanged("STK004_GTIPN");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_GumrukOrani
        {
            get { return sTK004_GumrukOrani; }
            set
            {
                sTK004_GumrukOrani = value;
                OnPropertyChanged("STK004_GumrukOrani");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Fon
        {
            get { return sTK004_Fon; }
            set
            {
                sTK004_Fon = value;
                OnPropertyChanged("STK004_Fon");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizAlis1
        {
            get { return sTK004_DovizAlis1; }
            set
            {
                sTK004_DovizAlis1 = value;
                OnPropertyChanged("STK004_DovizAlis1");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizAlisCinsi1
        {
            get { return sTK004_DovizAlisCinsi1; }
            set
            {
                sTK004_DovizAlisCinsi1 = value;
                OnPropertyChanged("STK004_DovizAlisCinsi1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizAlis2
        {
            get { return sTK004_DovizAlis2; }
            set
            {
                sTK004_DovizAlis2 = value;
                OnPropertyChanged("STK004_DovizAlis2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizAlisCinsi2
        {
            get { return sTK004_DovizAlisCinsi2; }
            set
            {
                sTK004_DovizAlisCinsi2 = value;
                OnPropertyChanged("STK004_DovizAlisCinsi2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizAlis3
        {
            get { return sTK004_DovizAlis3; }
            set
            {
                sTK004_DovizAlis3 = value;
                OnPropertyChanged("STK004_DovizAlis3");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizAlisCinsi3
        {
            get { return sTK004_DovizAlisCinsi3; }
            set
            {
                sTK004_DovizAlisCinsi3 = value;
                OnPropertyChanged("STK004_DovizAlisCinsi3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizSatis1
        {
            get { return sTK004_DovizSatis1; }
            set
            {
                sTK004_DovizSatis1 = value;
                OnPropertyChanged("STK004_DovizSatis1");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizSatisCinsi1
        {
            get { return sTK004_DovizSatisCinsi1; }
            set
            {
                sTK004_DovizSatisCinsi1 = value;
                OnPropertyChanged("STK004_DovizSatisCinsi1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizSatis2
        {
            get { return sTK004_DovizSatis2; }
            set
            {
                sTK004_DovizSatis2 = value;
                OnPropertyChanged("STK004_DovizSatis2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizSatisCinsi2
        {
            get { return sTK004_DovizSatisCinsi2; }
            set
            {
                sTK004_DovizSatisCinsi2 = value;
                OnPropertyChanged("STK004_DovizSatisCinsi2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizSatis3
        {
            get { return sTK004_DovizSatis3; }
            set
            {
                sTK004_DovizSatis3 = value;
                OnPropertyChanged("STK004_DovizSatis3");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizSatisCinsi3
        {
            get { return sTK004_DovizSatisCinsi3; }
            set
            {
                sTK004_DovizSatisCinsi3 = value;
                OnPropertyChanged("STK004_DovizSatisCinsi3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AlisSiparisi
        {
            get { return sTK004_AlisSiparisi; }
            set
            {
                sTK004_AlisSiparisi = value;
                OnPropertyChanged("STK004_AlisSiparisi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SatisSiparisi
        {
            get { return sTK004_SatisSiparisi; }
            set
            {
                sTK004_SatisSiparisi = value;
                OnPropertyChanged("STK004_SatisSiparisi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AzamiSeviye
        {
            get { return sTK004_AzamiSeviye; }
            set
            {
                sTK004_AzamiSeviye = value;
                OnPropertyChanged("STK004_AzamiSeviye");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonGirisTarihi
        {
            get { return sTK004_SonGirisTarihi; }
            set
            {
                sTK004_SonGirisTarihi = value;
                OnPropertyChanged("STK004_SonGirisTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonCikisTarihi
        {
            get { return sTK004_SonCikisTarihi; }
            set
            {
                sTK004_SonCikisTarihi = value;
                OnPropertyChanged("STK004_SonCikisTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BirimAgirligi
        {
            get { return sTK004_BirimAgirligi; }
            set
            {
                sTK004_BirimAgirligi = value;
                OnPropertyChanged("STK004_BirimAgirligi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_ValorGun
        {
            get { return sTK004_ValorGun; }
            set
            {
                sTK004_ValorGun = value;
                OnPropertyChanged("STK004_ValorGun");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod1
        {
            get { return sTK004_Barkod1; }
            set
            {
                sTK004_Barkod1 = value;
                OnPropertyChanged("STK004_Barkod1");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod2
        {
            get { return sTK004_Barkod2; }
            set
            {
                sTK004_Barkod2 = value;
                OnPropertyChanged("STK004_Barkod2");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod3
        {
            get { return sTK004_Barkod3; }
            set
            {
                sTK004_Barkod3 = value;
                OnPropertyChanged("STK004_Barkod3");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string STK004_Aciklama2
        {
            get { return sTK004_Aciklama2; }
            set
            {
                sTK004_Aciklama2 = value;
                OnPropertyChanged("STK004_Aciklama2");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string STK004_Aciklama3
        {
            get { return sTK004_Aciklama3; }
            set
            {
                sTK004_Aciklama3 = value;
                OnPropertyChanged("STK004_Aciklama3");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_UygMaliyetTipi
        {
            get { return sTK004_UygMaliyetTipi; }
            set
            {
                sTK004_UygMaliyetTipi = value;
                OnPropertyChanged("STK004_UygMaliyetTipi");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SonMaliyetBirimFiyati
        {
            get { return sTK004_SonMaliyetBirimFiyati; }
            set
            {
                sTK004_SonMaliyetBirimFiyati = value;
                OnPropertyChanged("STK004_SonMaliyetBirimFiyati");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonMaliyetTarihi
        {
            get { return sTK004_SonMaliyetTarihi; }
            set
            {
                sTK004_SonMaliyetTarihi = value;
                OnPropertyChanged("STK004_SonMaliyetTarihi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_SonMaliyetTipi
        {
            get { return sTK004_SonMaliyetTipi; }
            set
            {
                sTK004_SonMaliyetTipi = value;
                OnPropertyChanged("STK004_SonMaliyetTipi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SonMaliyetParaBirimi
        {
            get { return sTK004_SonMaliyetParaBirimi; }
            set
            {
                sTK004_SonMaliyetParaBirimi = value;
                OnPropertyChanged("STK004_SonMaliyetParaBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Rezervasyon
        {
            get { return sTK004_Rezervasyon; }
            set
            {
                sTK004_Rezervasyon = value;
                OnPropertyChanged("STK004_Rezervasyon");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_AlimdanIade
        {
            get { return sTK004_AlimdanIade; }
            set
            {
                sTK004_AlimdanIade = value;
                OnPropertyChanged("STK004_AlimdanIade");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_SatistanIade
        {
            get { return sTK004_SatistanIade; }
            set
            {
                sTK004_SatistanIade = value;
                OnPropertyChanged("STK004_SatistanIade");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_MalKodu2
        {
            get { return sTK004_MalKodu2; }
            set
            {
                sTK004_MalKodu2 = value;
                OnPropertyChanged("STK004_MalKodu2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizCinsi
        {
            get { return sTK004_DovizCinsi; }
            set
            {
                sTK004_DovizCinsi = value;
                OnPropertyChanged("STK004_DovizCinsi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonAlimFaturasiTarihi
        {
            get { return sTK004_SonAlimFaturasiTarihi; }
            set
            {
                sTK004_SonAlimFaturasiTarihi = value;
                OnPropertyChanged("STK004_SonAlimFaturasiTarihi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonAlimFaturasiNo
        {
            get { return sTK004_SonAlimFaturasiNo; }
            set
            {
                sTK004_SonAlimFaturasiNo = value;
                OnPropertyChanged("STK004_SonAlimFaturasiNo");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SonAlimFaturasiBirimFiyati
        {
            get { return sTK004_SonAlimFaturasiBirimFiyati; }
            set
            {
                sTK004_SonAlimFaturasiBirimFiyati = value;
                OnPropertyChanged("STK004_SonAlimFaturasiBirimFiyati");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonAlimFaturasiCariHesapKodu
        {
            get { return sTK004_SonAlimFaturasiCariHesapKodu; }
            set
            {
                sTK004_SonAlimFaturasiCariHesapKodu = value;
                OnPropertyChanged("STK004_SonAlimFaturasiCariHesapKodu");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SonAlimFaturasiDovizBirimFiyati
        {
            get { return sTK004_SonAlimFaturasiDovizBirimFiyati; }
            set
            {
                sTK004_SonAlimFaturasiDovizBirimFiyati = value;
                OnPropertyChanged("STK004_SonAlimFaturasiDovizBirimFiyati");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonSatisFaturasiTarihi
        {
            get { return sTK004_SonSatisFaturasiTarihi; }
            set
            {
                sTK004_SonSatisFaturasiTarihi = value;
                OnPropertyChanged("STK004_SonSatisFaturasiTarihi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonSatisFaturasiNo
        {
            get { return sTK004_SonSatisFaturasiNo; }
            set
            {
                sTK004_SonSatisFaturasiNo = value;
                OnPropertyChanged("STK004_SonSatisFaturasiNo");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SonSatisFaturasiBirimFiyati
        {
            get { return sTK004_SonSatisFaturasiBirimFiyati; }
            set
            {
                sTK004_SonSatisFaturasiBirimFiyati = value;
                OnPropertyChanged("STK004_SonSatisFaturasiBirimFiyati");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonSatisFaturasiCariHesapKodu
        {
            get { return sTK004_SonSatisFaturasiCariHesapKodu; }
            set
            {
                sTK004_SonSatisFaturasiCariHesapKodu = value;
                OnPropertyChanged("STK004_SonSatisFaturasiCariHesapKodu");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SonSatisFaturasiDovizBirimFiyati
        {
            get { return sTK004_SonSatisFaturasiDovizBirimFiyati; }
            set
            {
                sTK004_SonSatisFaturasiDovizBirimFiyati = value;
                OnPropertyChanged("STK004_SonSatisFaturasiDovizBirimFiyati");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_MasrafMerkezi
        {
            get { return sTK004_MasrafMerkezi; }
            set
            {
                sTK004_MasrafMerkezi = value;
                OnPropertyChanged("STK004_MasrafMerkezi");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK004_ResimDosyasi
        {
            get { return sTK004_ResimDosyasi; }
            set
            {
                sTK004_ResimDosyasi = value;
                OnPropertyChanged("STK004_ResimDosyasi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_FiyatListesindeCikart
        {
            get { return sTK004_FiyatListesindeCikart; }
            set
            {
                sTK004_FiyatListesindeCikart = value;
                OnPropertyChanged("STK004_FiyatListesindeCikart");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SatisFiyati1ValorGun
        {
            get { return sTK004_SatisFiyati1ValorGun; }
            set
            {
                sTK004_SatisFiyati1ValorGun = value;
                OnPropertyChanged("STK004_SatisFiyati1ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SatisFiyati2ValorGun
        {
            get { return sTK004_SatisFiyati2ValorGun; }
            set
            {
                sTK004_SatisFiyati2ValorGun = value;
                OnPropertyChanged("STK004_SatisFiyati2ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SatisFiyati3ValorGun
        {
            get { return sTK004_SatisFiyati3ValorGun; }
            set
            {
                sTK004_SatisFiyati3ValorGun = value;
                OnPropertyChanged("STK004_SatisFiyati3ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DovizSatisFiyati1ValorGun
        {
            get { return sTK004_DovizSatisFiyati1ValorGun; }
            set
            {
                sTK004_DovizSatisFiyati1ValorGun = value;
                OnPropertyChanged("STK004_DovizSatisFiyati1ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DovizSatisFiyati2ValorGun
        {
            get { return sTK004_DovizSatisFiyati2ValorGun; }
            set
            {
                sTK004_DovizSatisFiyati2ValorGun = value;
                OnPropertyChanged("STK004_DovizSatisFiyati2ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DovizSatisFiyati3ValorGun
        {
            get { return sTK004_DovizSatisFiyati3ValorGun; }
            set
            {
                sTK004_DovizSatisFiyati3ValorGun = value;
                OnPropertyChanged("STK004_DovizSatisFiyati3ValorGun");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizDevirTutari
        {
            get { return sTK004_DovizDevirTutari; }
            set
            {
                sTK004_DovizDevirTutari = value;
                OnPropertyChanged("STK004_DovizDevirTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizGirisTutari
        {
            get { return sTK004_DovizGirisTutari; }
            set
            {
                sTK004_DovizGirisTutari = value;
                OnPropertyChanged("STK004_DovizGirisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizGirisIskontoTutari
        {
            get { return sTK004_DovizGirisIskontoTutari; }
            set
            {
                sTK004_DovizGirisIskontoTutari = value;
                OnPropertyChanged("STK004_DovizGirisIskontoTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizCikisTutari
        {
            get { return sTK004_DovizCikisTutari; }
            set
            {
                sTK004_DovizCikisTutari = value;
                OnPropertyChanged("STK004_DovizCikisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizCikisIskontoTutari
        {
            get { return sTK004_DovizCikisIskontoTutari; }
            set
            {
                sTK004_DovizCikisIskontoTutari = value;
                OnPropertyChanged("STK004_DovizCikisIskontoTutari");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim1
        {
            get { return sTK004_BarkodBirim1; }
            set
            {
                sTK004_BarkodBirim1 = value;
                OnPropertyChanged("STK004_BarkodBirim1");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim2
        {
            get { return sTK004_BarkodBirim2; }
            set
            {
                sTK004_BarkodBirim2 = value;
                OnPropertyChanged("STK004_BarkodBirim2");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim3
        {
            get { return sTK004_BarkodBirim3; }
            set
            {
                sTK004_BarkodBirim3 = value;
                OnPropertyChanged("STK004_BarkodBirim3");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan1
        {
            get { return sTK004_BarkodCarpan1; }
            set
            {
                sTK004_BarkodCarpan1 = value;
                OnPropertyChanged("STK004_BarkodCarpan1");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan2
        {
            get { return sTK004_BarkodCarpan2; }
            set
            {
                sTK004_BarkodCarpan2 = value;
                OnPropertyChanged("STK004_BarkodCarpan2");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan3
        {
            get { return sTK004_BarkodCarpan3; }
            set
            {
                sTK004_BarkodCarpan3 = value;
                OnPropertyChanged("STK004_BarkodCarpan3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DevirMiktari2
        {
            get { return sTK004_DevirMiktari2; }
            set
            {
                sTK004_DevirMiktari2 = value;
                OnPropertyChanged("STK004_DevirMiktari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_GirisMiktari2
        {
            get { return sTK004_GirisMiktari2; }
            set
            {
                sTK004_GirisMiktari2 = value;
                OnPropertyChanged("STK004_GirisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_GirisTutari2
        {
            get { return sTK004_GirisTutari2; }
            set
            {
                sTK004_GirisTutari2 = value;
                OnPropertyChanged("STK004_GirisTutari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_CikisMiktari2
        {
            get { return sTK004_CikisMiktari2; }
            set
            {
                sTK004_CikisMiktari2 = value;
                OnPropertyChanged("STK004_CikisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_CikisTutari2
        {
            get { return sTK004_CikisTutari2; }
            set
            {
                sTK004_CikisTutari2 = value;
                OnPropertyChanged("STK004_CikisTutari2");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu1
        {
            get { return sTK004_DepoKodu1; }
            set
            {
                sTK004_DepoKodu1 = value;
                OnPropertyChanged("STK004_DepoKodu1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari21
        {
            get { return sTK004_DepoDevirMiktari21; }
            set
            {
                sTK004_DepoDevirMiktari21 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari21");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari21
        {
            get { return sTK004_DepoDevirTutari21; }
            set
            {
                sTK004_DepoDevirTutari21 = value;
                OnPropertyChanged("STK004_DepoDevirTutari21");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari1
        {
            get { return sTK004_DepoGirisMiktari1; }
            set
            {
                sTK004_DepoGirisMiktari1 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari1
        {
            get { return sTK004_DepoGirisTutari1; }
            set
            {
                sTK004_DepoGirisTutari1 = value;
                OnPropertyChanged("STK004_DepoGirisTutari1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto1
        {
            get { return sTK004_DepoGirisIskonto1; }
            set
            {
                sTK004_DepoGirisIskonto1 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari1
        {
            get { return sTK004_DepoCikisMiktari1; }
            set
            {
                sTK004_DepoCikisMiktari1 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari1
        {
            get { return sTK004_DepoCikisTutari1; }
            set
            {
                sTK004_DepoCikisTutari1 = value;
                OnPropertyChanged("STK004_DepoCikisTutari1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto1
        {
            get { return sTK004_DepoCikisIskonto1; }
            set
            {
                sTK004_DepoCikisIskonto1 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf1
        {
            get { return sTK004_DepoSonMaliyetBf1; }
            set
            {
                sTK004_DepoSonMaliyetBf1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf1");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi1
        {
            get { return sTK004_DepoSonMaliyetTipi1; }
            set
            {
                sTK004_DepoSonMaliyetTipi1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi1
        {
            get { return sTK004_DepoSonMaliyetTarihi1; }
            set
            {
                sTK004_DepoSonMaliyetTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr1
        {
            get { return sTK004_DepoSonMaliyetPrBr1; }
            set
            {
                sTK004_DepoSonMaliyetPrBr1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi1
        {
            get { return sTK004_DepoSonDevirTarihi1; }
            set
            {
                sTK004_DepoSonDevirTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi1
        {
            get { return sTK004_DepoSonGirisTarihi1; }
            set
            {
                sTK004_DepoSonGirisTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi1
        {
            get { return sTK004_DepoSonCikisTarihi1; }
            set
            {
                sTK004_DepoSonCikisTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi1");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu2
        {
            get { return sTK004_DepoKodu2; }
            set
            {
                sTK004_DepoKodu2 = value;
                OnPropertyChanged("STK004_DepoKodu2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari22
        {
            get { return sTK004_DepoDevirMiktari22; }
            set
            {
                sTK004_DepoDevirMiktari22 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari22");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari22
        {
            get { return sTK004_DepoDevirTutari22; }
            set
            {
                sTK004_DepoDevirTutari22 = value;
                OnPropertyChanged("STK004_DepoDevirTutari22");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari2
        {
            get { return sTK004_DepoGirisMiktari2; }
            set
            {
                sTK004_DepoGirisMiktari2 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari2
        {
            get { return sTK004_DepoGirisTutari2; }
            set
            {
                sTK004_DepoGirisTutari2 = value;
                OnPropertyChanged("STK004_DepoGirisTutari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto2
        {
            get { return sTK004_DepoGirisIskonto2; }
            set
            {
                sTK004_DepoGirisIskonto2 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari2
        {
            get { return sTK004_DepoCikisMiktari2; }
            set
            {
                sTK004_DepoCikisMiktari2 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari2
        {
            get { return sTK004_DepoCikisTutari2; }
            set
            {
                sTK004_DepoCikisTutari2 = value;
                OnPropertyChanged("STK004_DepoCikisTutari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto2
        {
            get { return sTK004_DepoCikisIskonto2; }
            set
            {
                sTK004_DepoCikisIskonto2 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf2
        {
            get { return sTK004_DepoSonMaliyetBf2; }
            set
            {
                sTK004_DepoSonMaliyetBf2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf2");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi2
        {
            get { return sTK004_DepoSonMaliyetTipi2; }
            set
            {
                sTK004_DepoSonMaliyetTipi2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi2
        {
            get { return sTK004_DepoSonMaliyetTarihi2; }
            set
            {
                sTK004_DepoSonMaliyetTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr2
        {
            get { return sTK004_DepoSonMaliyetPrBr2; }
            set
            {
                sTK004_DepoSonMaliyetPrBr2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi2
        {
            get { return sTK004_DepoSonDevirTarihi2; }
            set
            {
                sTK004_DepoSonDevirTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi2
        {
            get { return sTK004_DepoSonGirisTarihi2; }
            set
            {
                sTK004_DepoSonGirisTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi2
        {
            get { return sTK004_DepoSonCikisTarihi2; }
            set
            {
                sTK004_DepoSonCikisTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi2");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu3
        {
            get { return sTK004_DepoKodu3; }
            set
            {
                sTK004_DepoKodu3 = value;
                OnPropertyChanged("STK004_DepoKodu3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari23
        {
            get { return sTK004_DepoDevirMiktari23; }
            set
            {
                sTK004_DepoDevirMiktari23 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari23");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari23
        {
            get { return sTK004_DepoDevirTutari23; }
            set
            {
                sTK004_DepoDevirTutari23 = value;
                OnPropertyChanged("STK004_DepoDevirTutari23");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari3
        {
            get { return sTK004_DepoGirisMiktari3; }
            set
            {
                sTK004_DepoGirisMiktari3 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari3
        {
            get { return sTK004_DepoGirisTutari3; }
            set
            {
                sTK004_DepoGirisTutari3 = value;
                OnPropertyChanged("STK004_DepoGirisTutari3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto3
        {
            get { return sTK004_DepoGirisIskonto3; }
            set
            {
                sTK004_DepoGirisIskonto3 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari3
        {
            get { return sTK004_DepoCikisMiktari3; }
            set
            {
                sTK004_DepoCikisMiktari3 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari3
        {
            get { return sTK004_DepoCikisTutari3; }
            set
            {
                sTK004_DepoCikisTutari3 = value;
                OnPropertyChanged("STK004_DepoCikisTutari3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto3
        {
            get { return sTK004_DepoCikisIskonto3; }
            set
            {
                sTK004_DepoCikisIskonto3 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf3
        {
            get { return sTK004_DepoSonMaliyetBf3; }
            set
            {
                sTK004_DepoSonMaliyetBf3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf3");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi3
        {
            get { return sTK004_DepoSonMaliyetTipi3; }
            set
            {
                sTK004_DepoSonMaliyetTipi3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi3
        {
            get { return sTK004_DepoSonMaliyetTarihi3; }
            set
            {
                sTK004_DepoSonMaliyetTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr3
        {
            get { return sTK004_DepoSonMaliyetPrBr3; }
            set
            {
                sTK004_DepoSonMaliyetPrBr3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi3
        {
            get { return sTK004_DepoSonDevirTarihi3; }
            set
            {
                sTK004_DepoSonDevirTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi3
        {
            get { return sTK004_DepoSonGirisTarihi3; }
            set
            {
                sTK004_DepoSonGirisTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi3
        {
            get { return sTK004_DepoSonCikisTarihi3; }
            set
            {
                sTK004_DepoSonCikisTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi3");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu4
        {
            get { return sTK004_DepoKodu4; }
            set
            {
                sTK004_DepoKodu4 = value;
                OnPropertyChanged("STK004_DepoKodu4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari24
        {
            get { return sTK004_DepoDevirMiktari24; }
            set
            {
                sTK004_DepoDevirMiktari24 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari24");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari24
        {
            get { return sTK004_DepoDevirTutari24; }
            set
            {
                sTK004_DepoDevirTutari24 = value;
                OnPropertyChanged("STK004_DepoDevirTutari24");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari4
        {
            get { return sTK004_DepoGirisMiktari4; }
            set
            {
                sTK004_DepoGirisMiktari4 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari4");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari4
        {
            get { return sTK004_DepoGirisTutari4; }
            set
            {
                sTK004_DepoGirisTutari4 = value;
                OnPropertyChanged("STK004_DepoGirisTutari4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto4
        {
            get { return sTK004_DepoGirisIskonto4; }
            set
            {
                sTK004_DepoGirisIskonto4 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari4
        {
            get { return sTK004_DepoCikisMiktari4; }
            set
            {
                sTK004_DepoCikisMiktari4 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari4");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari4
        {
            get { return sTK004_DepoCikisTutari4; }
            set
            {
                sTK004_DepoCikisTutari4 = value;
                OnPropertyChanged("STK004_DepoCikisTutari4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto4
        {
            get { return sTK004_DepoCikisIskonto4; }
            set
            {
                sTK004_DepoCikisIskonto4 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto4");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf4
        {
            get { return sTK004_DepoSonMaliyetBf4; }
            set
            {
                sTK004_DepoSonMaliyetBf4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf4");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi4
        {
            get { return sTK004_DepoSonMaliyetTipi4; }
            set
            {
                sTK004_DepoSonMaliyetTipi4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi4
        {
            get { return sTK004_DepoSonMaliyetTarihi4; }
            set
            {
                sTK004_DepoSonMaliyetTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi4");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr4
        {
            get { return sTK004_DepoSonMaliyetPrBr4; }
            set
            {
                sTK004_DepoSonMaliyetPrBr4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi4
        {
            get { return sTK004_DepoSonDevirTarihi4; }
            set
            {
                sTK004_DepoSonDevirTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi4
        {
            get { return sTK004_DepoSonGirisTarihi4; }
            set
            {
                sTK004_DepoSonGirisTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi4
        {
            get { return sTK004_DepoSonCikisTarihi4; }
            set
            {
                sTK004_DepoSonCikisTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi4");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu5
        {
            get { return sTK004_DepoKodu5; }
            set
            {
                sTK004_DepoKodu5 = value;
                OnPropertyChanged("STK004_DepoKodu5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari25
        {
            get { return sTK004_DepoDevirMiktari25; }
            set
            {
                sTK004_DepoDevirMiktari25 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari25");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari25
        {
            get { return sTK004_DepoDevirTutari25; }
            set
            {
                sTK004_DepoDevirTutari25 = value;
                OnPropertyChanged("STK004_DepoDevirTutari25");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari5
        {
            get { return sTK004_DepoGirisMiktari5; }
            set
            {
                sTK004_DepoGirisMiktari5 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari5");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari5
        {
            get { return sTK004_DepoGirisTutari5; }
            set
            {
                sTK004_DepoGirisTutari5 = value;
                OnPropertyChanged("STK004_DepoGirisTutari5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto5
        {
            get { return sTK004_DepoGirisIskonto5; }
            set
            {
                sTK004_DepoGirisIskonto5 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari5
        {
            get { return sTK004_DepoCikisMiktari5; }
            set
            {
                sTK004_DepoCikisMiktari5 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari5");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari5
        {
            get { return sTK004_DepoCikisTutari5; }
            set
            {
                sTK004_DepoCikisTutari5 = value;
                OnPropertyChanged("STK004_DepoCikisTutari5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto5
        {
            get { return sTK004_DepoCikisIskonto5; }
            set
            {
                sTK004_DepoCikisIskonto5 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto5");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf5
        {
            get { return sTK004_DepoSonMaliyetBf5; }
            set
            {
                sTK004_DepoSonMaliyetBf5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf5");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi5
        {
            get { return sTK004_DepoSonMaliyetTipi5; }
            set
            {
                sTK004_DepoSonMaliyetTipi5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi5
        {
            get { return sTK004_DepoSonMaliyetTarihi5; }
            set
            {
                sTK004_DepoSonMaliyetTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi5");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr5
        {
            get { return sTK004_DepoSonMaliyetPrBr5; }
            set
            {
                sTK004_DepoSonMaliyetPrBr5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi5
        {
            get { return sTK004_DepoSonDevirTarihi5; }
            set
            {
                sTK004_DepoSonDevirTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi5
        {
            get { return sTK004_DepoSonGirisTarihi5; }
            set
            {
                sTK004_DepoSonGirisTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi5
        {
            get { return sTK004_DepoSonCikisTarihi5; }
            set
            {
                sTK004_DepoSonCikisTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi5");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu6
        {
            get { return sTK004_DepoKodu6; }
            set
            {
                sTK004_DepoKodu6 = value;
                OnPropertyChanged("STK004_DepoKodu6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari26
        {
            get { return sTK004_DepoDevirMiktari26; }
            set
            {
                sTK004_DepoDevirMiktari26 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari26");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari26
        {
            get { return sTK004_DepoDevirTutari26; }
            set
            {
                sTK004_DepoDevirTutari26 = value;
                OnPropertyChanged("STK004_DepoDevirTutari26");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari6
        {
            get { return sTK004_DepoGirisMiktari6; }
            set
            {
                sTK004_DepoGirisMiktari6 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari6");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari6
        {
            get { return sTK004_DepoGirisTutari6; }
            set
            {
                sTK004_DepoGirisTutari6 = value;
                OnPropertyChanged("STK004_DepoGirisTutari6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto6
        {
            get { return sTK004_DepoGirisIskonto6; }
            set
            {
                sTK004_DepoGirisIskonto6 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari6
        {
            get { return sTK004_DepoCikisMiktari6; }
            set
            {
                sTK004_DepoCikisMiktari6 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari6");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari6
        {
            get { return sTK004_DepoCikisTutari6; }
            set
            {
                sTK004_DepoCikisTutari6 = value;
                OnPropertyChanged("STK004_DepoCikisTutari6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto6
        {
            get { return sTK004_DepoCikisIskonto6; }
            set
            {
                sTK004_DepoCikisIskonto6 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto6");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf6
        {
            get { return sTK004_DepoSonMaliyetBf6; }
            set
            {
                sTK004_DepoSonMaliyetBf6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf6");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi6
        {
            get { return sTK004_DepoSonMaliyetTipi6; }
            set
            {
                sTK004_DepoSonMaliyetTipi6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi6
        {
            get { return sTK004_DepoSonMaliyetTarihi6; }
            set
            {
                sTK004_DepoSonMaliyetTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi6");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr6
        {
            get { return sTK004_DepoSonMaliyetPrBr6; }
            set
            {
                sTK004_DepoSonMaliyetPrBr6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi6
        {
            get { return sTK004_DepoSonDevirTarihi6; }
            set
            {
                sTK004_DepoSonDevirTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi6
        {
            get { return sTK004_DepoSonGirisTarihi6; }
            set
            {
                sTK004_DepoSonGirisTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi6
        {
            get { return sTK004_DepoSonCikisTarihi6; }
            set
            {
                sTK004_DepoSonCikisTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi6");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu7
        {
            get { return sTK004_DepoKodu7; }
            set
            {
                sTK004_DepoKodu7 = value;
                OnPropertyChanged("STK004_DepoKodu7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari27
        {
            get { return sTK004_DepoDevirMiktari27; }
            set
            {
                sTK004_DepoDevirMiktari27 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari27");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari27
        {
            get { return sTK004_DepoDevirTutari27; }
            set
            {
                sTK004_DepoDevirTutari27 = value;
                OnPropertyChanged("STK004_DepoDevirTutari27");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari7
        {
            get { return sTK004_DepoGirisMiktari7; }
            set
            {
                sTK004_DepoGirisMiktari7 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari7
        {
            get { return sTK004_DepoGirisTutari7; }
            set
            {
                sTK004_DepoGirisTutari7 = value;
                OnPropertyChanged("STK004_DepoGirisTutari7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto7
        {
            get { return sTK004_DepoGirisIskonto7; }
            set
            {
                sTK004_DepoGirisIskonto7 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari7
        {
            get { return sTK004_DepoCikisMiktari7; }
            set
            {
                sTK004_DepoCikisMiktari7 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari7
        {
            get { return sTK004_DepoCikisTutari7; }
            set
            {
                sTK004_DepoCikisTutari7 = value;
                OnPropertyChanged("STK004_DepoCikisTutari7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto7
        {
            get { return sTK004_DepoCikisIskonto7; }
            set
            {
                sTK004_DepoCikisIskonto7 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf7
        {
            get { return sTK004_DepoSonMaliyetBf7; }
            set
            {
                sTK004_DepoSonMaliyetBf7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf7");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi7
        {
            get { return sTK004_DepoSonMaliyetTipi7; }
            set
            {
                sTK004_DepoSonMaliyetTipi7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi7
        {
            get { return sTK004_DepoSonMaliyetTarihi7; }
            set
            {
                sTK004_DepoSonMaliyetTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi7");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr7
        {
            get { return sTK004_DepoSonMaliyetPrBr7; }
            set
            {
                sTK004_DepoSonMaliyetPrBr7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi7
        {
            get { return sTK004_DepoSonDevirTarihi7; }
            set
            {
                sTK004_DepoSonDevirTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi7
        {
            get { return sTK004_DepoSonGirisTarihi7; }
            set
            {
                sTK004_DepoSonGirisTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi7
        {
            get { return sTK004_DepoSonCikisTarihi7; }
            set
            {
                sTK004_DepoSonCikisTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi7");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu8
        {
            get { return sTK004_DepoKodu8; }
            set
            {
                sTK004_DepoKodu8 = value;
                OnPropertyChanged("STK004_DepoKodu8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari28
        {
            get { return sTK004_DepoDevirMiktari28; }
            set
            {
                sTK004_DepoDevirMiktari28 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari28");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari28
        {
            get { return sTK004_DepoDevirTutari28; }
            set
            {
                sTK004_DepoDevirTutari28 = value;
                OnPropertyChanged("STK004_DepoDevirTutari28");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari8
        {
            get { return sTK004_DepoGirisMiktari8; }
            set
            {
                sTK004_DepoGirisMiktari8 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari8
        {
            get { return sTK004_DepoGirisTutari8; }
            set
            {
                sTK004_DepoGirisTutari8 = value;
                OnPropertyChanged("STK004_DepoGirisTutari8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto8
        {
            get { return sTK004_DepoGirisIskonto8; }
            set
            {
                sTK004_DepoGirisIskonto8 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari8
        {
            get { return sTK004_DepoCikisMiktari8; }
            set
            {
                sTK004_DepoCikisMiktari8 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari8
        {
            get { return sTK004_DepoCikisTutari8; }
            set
            {
                sTK004_DepoCikisTutari8 = value;
                OnPropertyChanged("STK004_DepoCikisTutari8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto8
        {
            get { return sTK004_DepoCikisIskonto8; }
            set
            {
                sTK004_DepoCikisIskonto8 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf8
        {
            get { return sTK004_DepoSonMaliyetBf8; }
            set
            {
                sTK004_DepoSonMaliyetBf8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf8");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi8
        {
            get { return sTK004_DepoSonMaliyetTipi8; }
            set
            {
                sTK004_DepoSonMaliyetTipi8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi8
        {
            get { return sTK004_DepoSonMaliyetTarihi8; }
            set
            {
                sTK004_DepoSonMaliyetTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi8");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr8
        {
            get { return sTK004_DepoSonMaliyetPrBr8; }
            set
            {
                sTK004_DepoSonMaliyetPrBr8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi8
        {
            get { return sTK004_DepoSonDevirTarihi8; }
            set
            {
                sTK004_DepoSonDevirTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi8
        {
            get { return sTK004_DepoSonGirisTarihi8; }
            set
            {
                sTK004_DepoSonGirisTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi8
        {
            get { return sTK004_DepoSonCikisTarihi8; }
            set
            {
                sTK004_DepoSonCikisTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi8");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu9
        {
            get { return sTK004_DepoKodu9; }
            set
            {
                sTK004_DepoKodu9 = value;
                OnPropertyChanged("STK004_DepoKodu9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari29
        {
            get { return sTK004_DepoDevirMiktari29; }
            set
            {
                sTK004_DepoDevirMiktari29 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari29");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari29
        {
            get { return sTK004_DepoDevirTutari29; }
            set
            {
                sTK004_DepoDevirTutari29 = value;
                OnPropertyChanged("STK004_DepoDevirTutari29");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari9
        {
            get { return sTK004_DepoGirisMiktari9; }
            set
            {
                sTK004_DepoGirisMiktari9 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari9");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari9
        {
            get { return sTK004_DepoGirisTutari9; }
            set
            {
                sTK004_DepoGirisTutari9 = value;
                OnPropertyChanged("STK004_DepoGirisTutari9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto9
        {
            get { return sTK004_DepoGirisIskonto9; }
            set
            {
                sTK004_DepoGirisIskonto9 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari9
        {
            get { return sTK004_DepoCikisMiktari9; }
            set
            {
                sTK004_DepoCikisMiktari9 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari9");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari9
        {
            get { return sTK004_DepoCikisTutari9; }
            set
            {
                sTK004_DepoCikisTutari9 = value;
                OnPropertyChanged("STK004_DepoCikisTutari9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto9
        {
            get { return sTK004_DepoCikisIskonto9; }
            set
            {
                sTK004_DepoCikisIskonto9 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto9");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf9
        {
            get { return sTK004_DepoSonMaliyetBf9; }
            set
            {
                sTK004_DepoSonMaliyetBf9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf9");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi9
        {
            get { return sTK004_DepoSonMaliyetTipi9; }
            set
            {
                sTK004_DepoSonMaliyetTipi9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi9
        {
            get { return sTK004_DepoSonMaliyetTarihi9; }
            set
            {
                sTK004_DepoSonMaliyetTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi9");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr9
        {
            get { return sTK004_DepoSonMaliyetPrBr9; }
            set
            {
                sTK004_DepoSonMaliyetPrBr9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi9
        {
            get { return sTK004_DepoSonDevirTarihi9; }
            set
            {
                sTK004_DepoSonDevirTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi9
        {
            get { return sTK004_DepoSonGirisTarihi9; }
            set
            {
                sTK004_DepoSonGirisTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi9
        {
            get { return sTK004_DepoSonCikisTarihi9; }
            set
            {
                sTK004_DepoSonCikisTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi9");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu10
        {
            get { return sTK004_DepoKodu10; }
            set
            {
                sTK004_DepoKodu10 = value;
                OnPropertyChanged("STK004_DepoKodu10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari210
        {
            get { return sTK004_DepoDevirMiktari210; }
            set
            {
                sTK004_DepoDevirMiktari210 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari210");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari210
        {
            get { return sTK004_DepoDevirTutari210; }
            set
            {
                sTK004_DepoDevirTutari210 = value;
                OnPropertyChanged("STK004_DepoDevirTutari210");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari10
        {
            get { return sTK004_DepoGirisMiktari10; }
            set
            {
                sTK004_DepoGirisMiktari10 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari10");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari10
        {
            get { return sTK004_DepoGirisTutari10; }
            set
            {
                sTK004_DepoGirisTutari10 = value;
                OnPropertyChanged("STK004_DepoGirisTutari10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto10
        {
            get { return sTK004_DepoGirisIskonto10; }
            set
            {
                sTK004_DepoGirisIskonto10 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari10
        {
            get { return sTK004_DepoCikisMiktari10; }
            set
            {
                sTK004_DepoCikisMiktari10 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari10");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari10
        {
            get { return sTK004_DepoCikisTutari10; }
            set
            {
                sTK004_DepoCikisTutari10 = value;
                OnPropertyChanged("STK004_DepoCikisTutari10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto10
        {
            get { return sTK004_DepoCikisIskonto10; }
            set
            {
                sTK004_DepoCikisIskonto10 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto10");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf10
        {
            get { return sTK004_DepoSonMaliyetBf10; }
            set
            {
                sTK004_DepoSonMaliyetBf10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf10");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi10
        {
            get { return sTK004_DepoSonMaliyetTipi10; }
            set
            {
                sTK004_DepoSonMaliyetTipi10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi10
        {
            get { return sTK004_DepoSonMaliyetTarihi10; }
            set
            {
                sTK004_DepoSonMaliyetTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi10");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr10
        {
            get { return sTK004_DepoSonMaliyetPrBr10; }
            set
            {
                sTK004_DepoSonMaliyetPrBr10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi10
        {
            get { return sTK004_DepoSonDevirTarihi10; }
            set
            {
                sTK004_DepoSonDevirTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi10
        {
            get { return sTK004_DepoSonGirisTarihi10; }
            set
            {
                sTK004_DepoSonGirisTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi10
        {
            get { return sTK004_DepoSonCikisTarihi10; }
            set
            {
                sTK004_DepoSonCikisTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi10");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_StokMiktar2Br
        {
            get { return sTK004_StokMiktar2Br; }
            set
            {
                sTK004_StokMiktar2Br = value;
                OnPropertyChanged("STK004_StokMiktar2Br");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH1
        {
            get { return sTK004_StokMCH1; }
            set
            {
                sTK004_StokMCH1 = value;
                OnPropertyChanged("STK004_StokMCH1");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP1
        {
            get { return sTK004_MOP1; }
            set
            {
                sTK004_MOP1 = value;
                OnPropertyChanged("STK004_MOP1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH2
        {
            get { return sTK004_StokMCH2; }
            set
            {
                sTK004_StokMCH2 = value;
                OnPropertyChanged("STK004_StokMCH2");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP2
        {
            get { return sTK004_MOP2; }
            set
            {
                sTK004_MOP2 = value;
                OnPropertyChanged("STK004_MOP2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH3
        {
            get { return sTK004_StokMCH3; }
            set
            {
                sTK004_StokMCH3 = value;
                OnPropertyChanged("STK004_StokMCH3");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP3
        {
            get { return sTK004_MOP3; }
            set
            {
                sTK004_MOP3 = value;
                OnPropertyChanged("STK004_MOP3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH4
        {
            get { return sTK004_StokMCH4; }
            set
            {
                sTK004_StokMCH4 = value;
                OnPropertyChanged("STK004_StokMCH4");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP4
        {
            get { return sTK004_MOP4; }
            set
            {
                sTK004_MOP4 = value;
                OnPropertyChanged("STK004_MOP4");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH5
        {
            get { return sTK004_StokMCH5; }
            set
            {
                sTK004_StokMCH5 = value;
                OnPropertyChanged("STK004_StokMCH5");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP5
        {
            get { return sTK004_MOP5; }
            set
            {
                sTK004_MOP5 = value;
                OnPropertyChanged("STK004_MOP5");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMSOP
        {
            get { return sTK004_StokMSOP; }
            set
            {
                sTK004_StokMSOP = value;
                OnPropertyChanged("STK004_StokMSOP");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_StokDevirTutar2
        {
            get { return sTK004_StokDevirTutar2; }
            set
            {
                sTK004_StokDevirTutar2 = value;
                OnPropertyChanged("STK004_StokDevirTutar2");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not1
        {
            get { return sTK004_Not1; }
            set
            {
                sTK004_Not1 = value;
                OnPropertyChanged("STK004_Not1");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not2
        {
            get { return sTK004_Not2; }
            set
            {
                sTK004_Not2 = value;
                OnPropertyChanged("STK004_Not2");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not3
        {
            get { return sTK004_Not3; }
            set
            {
                sTK004_Not3 = value;
                OnPropertyChanged("STK004_Not3");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not4
        {
            get { return sTK004_Not4; }
            set
            {
                sTK004_Not4 = value;
                OnPropertyChanged("STK004_Not4");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not5
        {
            get { return sTK004_Not5; }
            set
            {
                sTK004_Not5 = value;
                OnPropertyChanged("STK004_Not5");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not6
        {
            get { return sTK004_Not6; }
            set
            {
                sTK004_Not6 = value;
                OnPropertyChanged("STK004_Not6");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not7
        {
            get { return sTK004_Not7; }
            set
            {
                sTK004_Not7 = value;
                OnPropertyChanged("STK004_Not7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_StokDBMiktari
        {
            get { return sTK004_StokDBMiktari; }
            set
            {
                sTK004_StokDBMiktari = value;
                OnPropertyChanged("STK004_StokDBMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_StokDBTutari
        {
            get { return sTK004_StokDBTutari; }
            set
            {
                sTK004_StokDBTutari = value;
                OnPropertyChanged("STK004_StokDBTutari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_StokDBDuzTutari
        {
            get { return sTK004_StokDBDuzTutari; }
            set
            {
                sTK004_StokDBDuzTutari = value;
                OnPropertyChanged("STK004_StokDBDuzTutari");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_StokDBDuzDate
        {
            get { return sTK004_StokDBDuzDate; }
            set
            {
                sTK004_StokDBDuzDate = value;
                OnPropertyChanged("STK004_StokDBDuzDate");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_StokDBDuzYil
        {
            get { return sTK004_StokDBDuzYil; }
            set
            {
                sTK004_StokDBDuzYil = value;
                OnPropertyChanged("STK004_StokDBDuzYil");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokDBDuzDonem
        {
            get { return sTK004_StokDBDuzDonem; }
            set
            {
                sTK004_StokDBDuzDonem = value;
                OnPropertyChanged("STK004_StokDBDuzDonem");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_StokDBRofm
        {
            get { return sTK004_StokDBRofm; }
            set
            {
                sTK004_StokDBRofm = value;
                OnPropertyChanged("STK004_StokDBRofm");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_StokDBEntHesapKodu
        {
            get { return sTK004_StokDBEntHesapKodu; }
            set
            {
                sTK004_StokDBEntHesapKodu = value;
                OnPropertyChanged("STK004_StokDBEntHesapKodu");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_YASatisFiati1
        {
            get { return sTK004_YASatisFiati1; }
            set
            {
                sTK004_YASatisFiati1 = value;
                OnPropertyChanged("STK004_YASatisFiati1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisKDV1
        {
            get { return sTK004_YSatisKDV1; }
            set
            {
                sTK004_YSatisKDV1 = value;
                OnPropertyChanged("STK004_YSatisKDV1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisBirim1
        {
            get { return sTK004_YSatisBirim1; }
            set
            {
                sTK004_YSatisBirim1 = value;
                OnPropertyChanged("STK004_YSatisBirim1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_YASatisFiati2
        {
            get { return sTK004_YASatisFiati2; }
            set
            {
                sTK004_YASatisFiati2 = value;
                OnPropertyChanged("STK004_YASatisFiati2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisKDV2
        {
            get { return sTK004_YSatisKDV2; }
            set
            {
                sTK004_YSatisKDV2 = value;
                OnPropertyChanged("STK004_YSatisKDV2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisBirim2
        {
            get { return sTK004_YSatisBirim2; }
            set
            {
                sTK004_YSatisBirim2 = value;
                OnPropertyChanged("STK004_YSatisBirim2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_YASatisFiati3
        {
            get { return sTK004_YASatisFiati3; }
            set
            {
                sTK004_YASatisFiati3 = value;
                OnPropertyChanged("STK004_YASatisFiati3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisKDV3
        {
            get { return sTK004_YSatisKDV3; }
            set
            {
                sTK004_YSatisKDV3 = value;
                OnPropertyChanged("STK004_YSatisKDV3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisBirim3
        {
            get { return sTK004_YSatisBirim3; }
            set
            {
                sTK004_YSatisBirim3 = value;
                OnPropertyChanged("STK004_YSatisBirim3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YSatisFiyati1ValorGun
        {
            get { return sTK004_YSatisFiyati1ValorGun; }
            set
            {
                sTK004_YSatisFiyati1ValorGun = value;
                OnPropertyChanged("STK004_YSatisFiyati1ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YSatisFiyati2ValorGun
        {
            get { return sTK004_YSatisFiyati2ValorGun; }
            set
            {
                sTK004_YSatisFiyati2ValorGun = value;
                OnPropertyChanged("STK004_YSatisFiyati2ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YSatisFiyati3ValorGun
        {
            get { return sTK004_YSatisFiyati3ValorGun; }
            set
            {
                sTK004_YSatisFiyati3ValorGun = value;
                OnPropertyChanged("STK004_YSatisFiyati3ValorGun");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AktifFlag
        {
            get { return sTK004_AktifFlag; }
            set
            {
                sTK004_AktifFlag = value;
                OnPropertyChanged("STK004_AktifFlag");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SayimTarihi
        {
            get { return sTK004_SayimTarihi; }
            set
            {
                sTK004_SayimTarihi = value;
                OnPropertyChanged("STK004_SayimTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SayimMiktari
        {
            get { return sTK004_SayimMiktari; }
            set
            {
                sTK004_SayimMiktari = value;
                OnPropertyChanged("STK004_SayimMiktari");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string STK004_Dokuman1
        {
            get { return sTK004_Dokuman1; }
            set
            {
                sTK004_Dokuman1 = value;
                OnPropertyChanged("STK004_Dokuman1");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string STK004_Dokuman2
        {
            get { return sTK004_Dokuman2; }
            set
            {
                sTK004_Dokuman2 = value;
                OnPropertyChanged("STK004_Dokuman2");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string STK004_Dokuman3
        {
            get { return sTK004_Dokuman3; }
            set
            {
                sTK004_Dokuman3 = value;
                OnPropertyChanged("STK004_Dokuman3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_SMMHesapKodu
        {
            get { return sTK004_SMMHesapKodu; }
            set
            {
                sTK004_SMMHesapKodu = value;
                OnPropertyChanged("STK004_SMMHesapKodu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_KDVTevkIslemTuruAlim
        {
            get { return sTK004_KDVTevkIslemTuruAlim; }
            set
            {
                sTK004_KDVTevkIslemTuruAlim = value;
                OnPropertyChanged("STK004_KDVTevkIslemTuruAlim");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod4
        {
            get { return sTK004_Barkod4; }
            set
            {
                sTK004_Barkod4 = value;
                OnPropertyChanged("STK004_Barkod4");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod5
        {
            get { return sTK004_Barkod5; }
            set
            {
                sTK004_Barkod5 = value;
                OnPropertyChanged("STK004_Barkod5");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim4
        {
            get { return sTK004_BarkodBirim4; }
            set
            {
                sTK004_BarkodBirim4 = value;
                OnPropertyChanged("STK004_BarkodBirim4");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim5
        {
            get { return sTK004_BarkodBirim5; }
            set
            {
                sTK004_BarkodBirim5 = value;
                OnPropertyChanged("STK004_BarkodBirim5");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan4
        {
            get { return sTK004_BarkodCarpan4; }
            set
            {
                sTK004_BarkodCarpan4 = value;
                OnPropertyChanged("STK004_BarkodCarpan4");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan5
        {
            get { return sTK004_BarkodCarpan5; }
            set
            {
                sTK004_BarkodCarpan5 = value;
                OnPropertyChanged("STK004_BarkodCarpan5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_KDVTevkIslemTuruSatis
        {
            get { return sTK004_KDVTevkIslemTuruSatis; }
            set
            {
                sTK004_KDVTevkIslemTuruSatis = value;
                OnPropertyChanged("STK004_KDVTevkIslemTuruSatis");
            }
        }

        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string STK004_KDVTevkOraniAlim
        {
            get { return sTK004_KDVTevkOraniAlim; }
            set
            {
                sTK004_KDVTevkOraniAlim = value;
                OnPropertyChanged("STK004_KDVTevkOraniAlim");
            }
        }

        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string STK004_KDVTevkOraniSatis
        {
            get { return sTK004_KDVTevkOraniSatis; }
            set
            {
                sTK004_KDVTevkOraniSatis = value;
                OnPropertyChanged("STK004_KDVTevkOraniSatis");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_KismiKDVMuafMevcut
        {
            get { return sTK004_KismiKDVMuafMevcut; }
            set
            {
                sTK004_KismiKDVMuafMevcut = value;
                OnPropertyChanged("STK004_KismiKDVMuafMevcut");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_KDVMuafAciklama
        {
            get { return sTK004_KDVMuafAciklama; }
            set
            {
                sTK004_KDVMuafAciklama = value;
                OnPropertyChanged("STK004_KDVMuafAciklama");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BoyutBirimi
        {
            get { return sTK004_BoyutBirimi; }
            set
            {
                sTK004_BoyutBirimi = value;
                OnPropertyChanged("STK004_BoyutBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BoyutEn
        {
            get { return sTK004_BoyutEn; }
            set
            {
                sTK004_BoyutEn = value;
                OnPropertyChanged("STK004_BoyutEn");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BoyutBoy
        {
            get { return sTK004_BoyutBoy; }
            set
            {
                sTK004_BoyutBoy = value;
                OnPropertyChanged("STK004_BoyutBoy");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BoyutGenisilik
        {
            get { return sTK004_BoyutGenisilik; }
            set
            {
                sTK004_BoyutGenisilik = value;
                OnPropertyChanged("STK004_BoyutGenisilik");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_AgirlikBirimi
        {
            get { return sTK004_AgirlikBirimi; }
            set
            {
                sTK004_AgirlikBirimi = value;
                OnPropertyChanged("STK004_AgirlikBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AgirlikBrut
        {
            get { return sTK004_AgirlikBrut; }
            set
            {
                sTK004_AgirlikBrut = value;
                OnPropertyChanged("STK004_AgirlikBrut");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AgirlikNet
        {
            get { return sTK004_AgirlikNet; }
            set
            {
                sTK004_AgirlikNet = value;
                OnPropertyChanged("STK004_AgirlikNet");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_HacimBirimi
        {
            get { return sTK004_HacimBirimi; }
            set
            {
                sTK004_HacimBirimi = value;
                OnPropertyChanged("STK004_HacimBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_HacimBrut
        {
            get { return sTK004_HacimBrut; }
            set
            {
                sTK004_HacimBrut = value;
                OnPropertyChanged("STK004_HacimBrut");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_HacimNet
        {
            get { return sTK004_HacimNet; }
            set
            {
                sTK004_HacimNet = value;
                OnPropertyChanged("STK004_HacimNet");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YOKCPLUGonder
        {
            get { return sTK004_YOKCPLUGonder; }
            set
            {
                sTK004_YOKCPLUGonder = value;
                OnPropertyChanged("STK004_YOKCPLUGonder");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YOKCPLUID
        {
            get { return sTK004_YOKCPLUID; }
            set
            {
                sTK004_YOKCPLUID = value;
                OnPropertyChanged("STK004_YOKCPLUID");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YOKCDepartmanID
        {
            get { return sTK004_YOKCDepartmanID; }
            set
            {
                sTK004_YOKCDepartmanID = value;
                OnPropertyChanged("STK004_YOKCDepartmanID");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string pk_STK004_MalKodu
        {
            get { return _pk_STK004_MalKodu; }
            set
            {
                _pk_STK004_MalKodu = value;
                OnPropertyChanged("pk_STK004_MalKodu");
            }
        }

        #endregion /// Properties       
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(STK004E Property, object deger, Operand and_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK004E), Property), deger, and_Or));
        }

        public void WhereAdd(STK004E Property, Islem islem, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK004E), Property), islem, Deger, And_Or));
        }

        public void WhereAdd(STK004E Property, Operand In_NotIn, params object[] Degerler_Parantez)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK004E), Property), In_NotIn, Degerler_Parantez));
        }

        public void WhereAdd(params object[] Degerler)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Degerler));
        }

        /// <summary> Set işleminde Property " = " eşit ile otomatik başlar. </summary>
        public void SetAdd(STK004E Property, params object[] Degerler)
        {
            SetList.Add(SqlExperOperatorIslem.SetAdd(Enum.GetName(typeof(STK004E), Property), Degerler));
        }

        List<string> WhereList = new List<string>();
        List<string> SetList = new List<string>();
        string info_FullTableName = "YNS{0}.YNS{0}.STK004";
        string[] info_PrimaryKeys = { "pk_STK004_MalKodu" };
        string[] info_IdentityKeys = { "STK004_Row_ID" };

        List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public STK004()
        {
            ChangedProperties = new List<string>();
            PropertyChanged += STK004_PropertyChanged;
        }

        public void AcceptChanges() => ChangedProperties.Clear();

        void STK004_PropertyChanged(object sender, PropertyChangedEventArgs e)
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