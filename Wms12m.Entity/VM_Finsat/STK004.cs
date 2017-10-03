using DevHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        private int _STK004_Row_ID;
        private string _STK004_MalKodu;
        private string _STK004_Aciklama;
        private string _STK004_GrupKodu;
        private decimal? _STK004_Iskonto;
        private byte? _STK004_KDV;
        private string _STK004_Birim1;
        private string _STK004_Birim2;
        private string _STK004_Birim3;
        private string _STK004_BorcluHesap;
        private string _STK004_AlacakliHesap;
        private decimal? _STK004_AlisFiyati1;
        private byte? _STK004_AlisKDV1;
        private byte? _STK004_AlisBirim1;
        private decimal? _STK004_AlisFiyati2;
        private byte? _STK004_AlisKDV2;
        private byte? _STK004_AlisBirim2;
        private decimal? _STK004_AlisFiyati3;
        private byte? _STK004_AlisKDV3;
        private byte? _STK004_AlisBirim3;
        private decimal? _STK004_SatisFiyati1;
        private byte? _STK004_SatisKDV1;
        private byte? _STK004_SatisBirim1;
        private decimal? _STK004_SatisFiyati2;
        private byte? _STK004_SatisKDV2;
        private byte? _STK004_SatisBirim2;
        private decimal? _STK004_SatisFiyati3;
        private byte? _STK004_SatisKDV3;
        private byte? _STK004_SatisBirim3;
        private decimal? _STK004_DevirMiktari;
        private decimal? _STK004_DevirTutari;
        private decimal? _STK004_GirisMiktari;
        private decimal? _STK004_GirisTutari;
        private decimal? _STK004_GirisIskonto;
        private decimal? _STK004_CikisMiktari;
        private decimal? _STK004_CikisTutari;
        private decimal? _STK004_CikisIskonto;
        private decimal? _STK004_KritikSeviye;
        private int? _STK004_DevirTarihi;
        private decimal? _STK004_KafileBuyuklugu;
        private string _STK004_GirenKaynak;
        private int? _STK004_GirenTarih;
        private string _STK004_GirenSaat;
        private string _STK004_GirenKodu;
        private string _STK004_GirenSurum;
        private string _STK004_DegistirenKaynak;
        private int? _STK004_DegistirenTarih;
        private string _STK004_DegistirenSaat;
        private string _STK004_DegistirenKodu;
        private string _STK004_DegistirenSurum;
        private string _STK004_OzelKodu;
        private string _STK004_TipKodu;
        private string _STK004_Kod4;
        private string _STK004_Kod5;
        private string _STK004_Kod6;
        private string _STK004_Kod7;
        private string _STK004_Kod8;
        private string _STK004_Kod9;
        private string _STK004_Kod10;
        private decimal? _STK004_Kod11;
        private decimal? _STK004_Kod12;
        private string _STK004_UreticiKodu1;
        private string _STK004_UreticiKodu2;
        private string _STK004_UreticiKodu3;
        private string _STK004_MusterekMalKodu;
        private byte? _STK004_MaliyetSekli;
        private decimal? _STK004_FireOrani;
        private string _STK004_TeminYeri;
        private short? _STK004_TeminSuresi;
        private string _STK004_Mensei;
        private string _STK004_GTIPN;
        private decimal? _STK004_GumrukOrani;
        private decimal? _STK004_Fon;
        private decimal? _STK004_DovizAlis1;
        private string _STK004_DovizAlisCinsi1;
        private decimal? _STK004_DovizAlis2;
        private string _STK004_DovizAlisCinsi2;
        private decimal? _STK004_DovizAlis3;
        private string _STK004_DovizAlisCinsi3;
        private decimal? _STK004_DovizSatis1;
        private string _STK004_DovizSatisCinsi1;
        private decimal? _STK004_DovizSatis2;
        private string _STK004_DovizSatisCinsi2;
        private decimal? _STK004_DovizSatis3;
        private string _STK004_DovizSatisCinsi3;
        private decimal? _STK004_AlisSiparisi;
        private decimal? _STK004_SatisSiparisi;
        private decimal? _STK004_AzamiSeviye;
        private int? _STK004_SonGirisTarihi;
        private int? _STK004_SonCikisTarihi;
        private decimal? _STK004_BirimAgirligi;
        private int? _STK004_ValorGun;
        private string _STK004_Barkod1;
        private string _STK004_Barkod2;
        private string _STK004_Barkod3;
        private string _STK004_Aciklama2;
        private string _STK004_Aciklama3;
        private short? _STK004_UygMaliyetTipi;
        private decimal? _STK004_SonMaliyetBirimFiyati;
        private int? _STK004_SonMaliyetTarihi;
        private short? _STK004_SonMaliyetTipi;
        private byte? _STK004_SonMaliyetParaBirimi;
        private decimal? _STK004_Rezervasyon;
        private string _STK004_AlimdanIade;
        private string _STK004_SatistanIade;
        private string _STK004_MalKodu2;
        private string _STK004_DovizCinsi;
        private int? _STK004_SonAlimFaturasiTarihi;
        private string _STK004_SonAlimFaturasiNo;
        private decimal? _STK004_SonAlimFaturasiBirimFiyati;
        private string _STK004_SonAlimFaturasiCariHesapKodu;
        private decimal? _STK004_SonAlimFaturasiDovizBirimFiyati;
        private int? _STK004_SonSatisFaturasiTarihi;
        private string _STK004_SonSatisFaturasiNo;
        private decimal? _STK004_SonSatisFaturasiBirimFiyati;
        private string _STK004_SonSatisFaturasiCariHesapKodu;
        private decimal? _STK004_SonSatisFaturasiDovizBirimFiyati;
        private string _STK004_MasrafMerkezi;
        private string _STK004_ResimDosyasi;
        private byte? _STK004_FiyatListesindeCikart;
        private int? _STK004_SatisFiyati1ValorGun;
        private int? _STK004_SatisFiyati2ValorGun;
        private int? _STK004_SatisFiyati3ValorGun;
        private int? _STK004_DovizSatisFiyati1ValorGun;
        private int? _STK004_DovizSatisFiyati2ValorGun;
        private int? _STK004_DovizSatisFiyati3ValorGun;
        private decimal? _STK004_DovizDevirTutari;
        private decimal? _STK004_DovizGirisTutari;
        private decimal? _STK004_DovizGirisIskontoTutari;
        private decimal? _STK004_DovizCikisTutari;
        private decimal? _STK004_DovizCikisIskontoTutari;
        private string _STK004_BarkodBirim1;
        private string _STK004_BarkodBirim2;
        private string _STK004_BarkodBirim3;
        private decimal? _STK004_BarkodCarpan1;
        private decimal? _STK004_BarkodCarpan2;
        private decimal? _STK004_BarkodCarpan3;
        private decimal? _STK004_DevirMiktari2;
        private decimal? _STK004_GirisMiktari2;
        private decimal? _STK004_GirisTutari2;
        private decimal? _STK004_CikisMiktari2;
        private decimal? _STK004_CikisTutari2;
        private string _STK004_DepoKodu1;
        private decimal? _STK004_DepoDevirMiktari21;
        private decimal? _STK004_DepoDevirTutari21;
        private decimal? _STK004_DepoGirisMiktari1;
        private decimal? _STK004_DepoGirisTutari1;
        private decimal? _STK004_DepoGirisIskonto1;
        private decimal? _STK004_DepoCikisMiktari1;
        private decimal? _STK004_DepoCikisTutari1;
        private decimal? _STK004_DepoCikisIskonto1;
        private decimal? _STK004_DepoSonMaliyetBf1;
        private short? _STK004_DepoSonMaliyetTipi1;
        private int? _STK004_DepoSonMaliyetTarihi1;
        private byte? _STK004_DepoSonMaliyetPrBr1;
        private int? _STK004_DepoSonDevirTarihi1;
        private int? _STK004_DepoSonGirisTarihi1;
        private int? _STK004_DepoSonCikisTarihi1;
        private string _STK004_DepoKodu2;
        private decimal? _STK004_DepoDevirMiktari22;
        private decimal? _STK004_DepoDevirTutari22;
        private decimal? _STK004_DepoGirisMiktari2;
        private decimal? _STK004_DepoGirisTutari2;
        private decimal? _STK004_DepoGirisIskonto2;
        private decimal? _STK004_DepoCikisMiktari2;
        private decimal? _STK004_DepoCikisTutari2;
        private decimal? _STK004_DepoCikisIskonto2;
        private decimal? _STK004_DepoSonMaliyetBf2;
        private short? _STK004_DepoSonMaliyetTipi2;
        private int? _STK004_DepoSonMaliyetTarihi2;
        private byte? _STK004_DepoSonMaliyetPrBr2;
        private int? _STK004_DepoSonDevirTarihi2;
        private int? _STK004_DepoSonGirisTarihi2;
        private int? _STK004_DepoSonCikisTarihi2;
        private string _STK004_DepoKodu3;
        private decimal? _STK004_DepoDevirMiktari23;
        private decimal? _STK004_DepoDevirTutari23;
        private decimal? _STK004_DepoGirisMiktari3;
        private decimal? _STK004_DepoGirisTutari3;
        private decimal? _STK004_DepoGirisIskonto3;
        private decimal? _STK004_DepoCikisMiktari3;
        private decimal? _STK004_DepoCikisTutari3;
        private decimal? _STK004_DepoCikisIskonto3;
        private decimal? _STK004_DepoSonMaliyetBf3;
        private short? _STK004_DepoSonMaliyetTipi3;
        private int? _STK004_DepoSonMaliyetTarihi3;
        private byte? _STK004_DepoSonMaliyetPrBr3;
        private int? _STK004_DepoSonDevirTarihi3;
        private int? _STK004_DepoSonGirisTarihi3;
        private int? _STK004_DepoSonCikisTarihi3;
        private string _STK004_DepoKodu4;
        private decimal? _STK004_DepoDevirMiktari24;
        private decimal? _STK004_DepoDevirTutari24;
        private decimal? _STK004_DepoGirisMiktari4;
        private decimal? _STK004_DepoGirisTutari4;
        private decimal? _STK004_DepoGirisIskonto4;
        private decimal? _STK004_DepoCikisMiktari4;
        private decimal? _STK004_DepoCikisTutari4;
        private decimal? _STK004_DepoCikisIskonto4;
        private decimal? _STK004_DepoSonMaliyetBf4;
        private short? _STK004_DepoSonMaliyetTipi4;
        private int? _STK004_DepoSonMaliyetTarihi4;
        private byte? _STK004_DepoSonMaliyetPrBr4;
        private int? _STK004_DepoSonDevirTarihi4;
        private int? _STK004_DepoSonGirisTarihi4;
        private int? _STK004_DepoSonCikisTarihi4;
        private string _STK004_DepoKodu5;
        private decimal? _STK004_DepoDevirMiktari25;
        private decimal? _STK004_DepoDevirTutari25;
        private decimal? _STK004_DepoGirisMiktari5;
        private decimal? _STK004_DepoGirisTutari5;
        private decimal? _STK004_DepoGirisIskonto5;
        private decimal? _STK004_DepoCikisMiktari5;
        private decimal? _STK004_DepoCikisTutari5;
        private decimal? _STK004_DepoCikisIskonto5;
        private decimal? _STK004_DepoSonMaliyetBf5;
        private short? _STK004_DepoSonMaliyetTipi5;
        private int? _STK004_DepoSonMaliyetTarihi5;
        private byte? _STK004_DepoSonMaliyetPrBr5;
        private int? _STK004_DepoSonDevirTarihi5;
        private int? _STK004_DepoSonGirisTarihi5;
        private int? _STK004_DepoSonCikisTarihi5;
        private string _STK004_DepoKodu6;
        private decimal? _STK004_DepoDevirMiktari26;
        private decimal? _STK004_DepoDevirTutari26;
        private decimal? _STK004_DepoGirisMiktari6;
        private decimal? _STK004_DepoGirisTutari6;
        private decimal? _STK004_DepoGirisIskonto6;
        private decimal? _STK004_DepoCikisMiktari6;
        private decimal? _STK004_DepoCikisTutari6;
        private decimal? _STK004_DepoCikisIskonto6;
        private decimal? _STK004_DepoSonMaliyetBf6;
        private short? _STK004_DepoSonMaliyetTipi6;
        private int? _STK004_DepoSonMaliyetTarihi6;
        private byte? _STK004_DepoSonMaliyetPrBr6;
        private int? _STK004_DepoSonDevirTarihi6;
        private int? _STK004_DepoSonGirisTarihi6;
        private int? _STK004_DepoSonCikisTarihi6;
        private string _STK004_DepoKodu7;
        private decimal? _STK004_DepoDevirMiktari27;
        private decimal? _STK004_DepoDevirTutari27;
        private decimal? _STK004_DepoGirisMiktari7;
        private decimal? _STK004_DepoGirisTutari7;
        private decimal? _STK004_DepoGirisIskonto7;
        private decimal? _STK004_DepoCikisMiktari7;
        private decimal? _STK004_DepoCikisTutari7;
        private decimal? _STK004_DepoCikisIskonto7;
        private decimal? _STK004_DepoSonMaliyetBf7;
        private short? _STK004_DepoSonMaliyetTipi7;
        private int? _STK004_DepoSonMaliyetTarihi7;
        private byte? _STK004_DepoSonMaliyetPrBr7;
        private int? _STK004_DepoSonDevirTarihi7;
        private int? _STK004_DepoSonGirisTarihi7;
        private int? _STK004_DepoSonCikisTarihi7;
        private string _STK004_DepoKodu8;
        private decimal? _STK004_DepoDevirMiktari28;
        private decimal? _STK004_DepoDevirTutari28;
        private decimal? _STK004_DepoGirisMiktari8;
        private decimal? _STK004_DepoGirisTutari8;
        private decimal? _STK004_DepoGirisIskonto8;
        private decimal? _STK004_DepoCikisMiktari8;
        private decimal? _STK004_DepoCikisTutari8;
        private decimal? _STK004_DepoCikisIskonto8;
        private decimal? _STK004_DepoSonMaliyetBf8;
        private short? _STK004_DepoSonMaliyetTipi8;
        private int? _STK004_DepoSonMaliyetTarihi8;
        private byte? _STK004_DepoSonMaliyetPrBr8;
        private int? _STK004_DepoSonDevirTarihi8;
        private int? _STK004_DepoSonGirisTarihi8;
        private int? _STK004_DepoSonCikisTarihi8;
        private string _STK004_DepoKodu9;
        private decimal? _STK004_DepoDevirMiktari29;
        private decimal? _STK004_DepoDevirTutari29;
        private decimal? _STK004_DepoGirisMiktari9;
        private decimal? _STK004_DepoGirisTutari9;
        private decimal? _STK004_DepoGirisIskonto9;
        private decimal? _STK004_DepoCikisMiktari9;
        private decimal? _STK004_DepoCikisTutari9;
        private decimal? _STK004_DepoCikisIskonto9;
        private decimal? _STK004_DepoSonMaliyetBf9;
        private short? _STK004_DepoSonMaliyetTipi9;
        private int? _STK004_DepoSonMaliyetTarihi9;
        private byte? _STK004_DepoSonMaliyetPrBr9;
        private int? _STK004_DepoSonDevirTarihi9;
        private int? _STK004_DepoSonGirisTarihi9;
        private int? _STK004_DepoSonCikisTarihi9;
        private string _STK004_DepoKodu10;
        private decimal? _STK004_DepoDevirMiktari210;
        private decimal? _STK004_DepoDevirTutari210;
        private decimal? _STK004_DepoGirisMiktari10;
        private decimal? _STK004_DepoGirisTutari10;
        private decimal? _STK004_DepoGirisIskonto10;
        private decimal? _STK004_DepoCikisMiktari10;
        private decimal? _STK004_DepoCikisTutari10;
        private decimal? _STK004_DepoCikisIskonto10;
        private decimal? _STK004_DepoSonMaliyetBf10;
        private short? _STK004_DepoSonMaliyetTipi10;
        private int? _STK004_DepoSonMaliyetTarihi10;
        private byte? _STK004_DepoSonMaliyetPrBr10;
        private int? _STK004_DepoSonDevirTarihi10;
        private int? _STK004_DepoSonGirisTarihi10;
        private int? _STK004_DepoSonCikisTarihi10;
        private string _STK004_StokMiktar2Br;
        private byte? _STK004_StokMCH1;
        private string _STK004_MOP1;
        private byte? _STK004_StokMCH2;
        private string _STK004_MOP2;
        private byte? _STK004_StokMCH3;
        private string _STK004_MOP3;
        private byte? _STK004_StokMCH4;
        private string _STK004_MOP4;
        private byte? _STK004_StokMCH5;
        private string _STK004_MOP5;
        private byte? _STK004_StokMSOP;
        private decimal? _STK004_StokDevirTutar2;
        private string _STK004_Not1;
        private string _STK004_Not2;
        private string _STK004_Not3;
        private string _STK004_Not4;
        private string _STK004_Not5;
        private string _STK004_Not6;
        private string _STK004_Not7;
        private decimal? _STK004_StokDBMiktari;
        private decimal? _STK004_StokDBTutari;
        private decimal? _STK004_StokDBDuzTutari;
        private int? _STK004_StokDBDuzDate;
        private int? _STK004_StokDBDuzYil;
        private byte? _STK004_StokDBDuzDonem;
        private decimal? _STK004_StokDBRofm;
        private string _STK004_StokDBEntHesapKodu;
        private decimal? _STK004_YASatisFiati1;
        private byte? _STK004_YSatisKDV1;
        private byte? _STK004_YSatisBirim1;
        private decimal? _STK004_YASatisFiati2;
        private byte? _STK004_YSatisKDV2;
        private byte? _STK004_YSatisBirim2;
        private decimal? _STK004_YASatisFiati3;
        private byte? _STK004_YSatisKDV3;
        private byte? _STK004_YSatisBirim3;
        private int? _STK004_YSatisFiyati1ValorGun;
        private int? _STK004_YSatisFiyati2ValorGun;
        private int? _STK004_YSatisFiyati3ValorGun;
        private byte? _STK004_AktifFlag;
        private int? _STK004_SayimTarihi;
        private decimal? _STK004_SayimMiktari;
        private string _STK004_Dokuman1;
        private string _STK004_Dokuman2;
        private string _STK004_Dokuman3;
        private string _STK004_SMMHesapKodu;
        private int? _STK004_KDVTevkIslemTuruAlim;
        private string _STK004_Barkod4;
        private string _STK004_Barkod5;
        private string _STK004_BarkodBirim4;
        private string _STK004_BarkodBirim5;
        private decimal? _STK004_BarkodCarpan4;
        private decimal? _STK004_BarkodCarpan5;
        private int? _STK004_KDVTevkIslemTuruSatis;
        private string _STK004_KDVTevkOraniAlim;
        private string _STK004_KDVTevkOraniSatis;
        private byte? _STK004_KismiKDVMuafMevcut;
        private string _STK004_KDVMuafAciklama;
        private string _STK004_BoyutBirimi;
        private decimal? _STK004_BoyutEn;
        private decimal? _STK004_BoyutBoy;
        private decimal? _STK004_BoyutGenisilik;
        private string _STK004_AgirlikBirimi;
        private decimal? _STK004_AgirlikBrut;
        private decimal? _STK004_AgirlikNet;
        private string _STK004_HacimBirimi;
        private decimal? _STK004_HacimBrut;
        private decimal? _STK004_HacimNet;
        private byte? _STK004_YOKCPLUGonder;
        private int? _STK004_YOKCPLUID;
        private int? _STK004_YOKCDepartmanID;
        //private int _pk_STK004_Row_ID;

        private string _pk_STK004_MalKodu;
        #endregion /// Fields


        /// <summary> INT (4) PrimaryKey IdentityKey * </summary>
        public int STK004_Row_ID
        {
            get { return this._STK004_Row_ID; }
        }


        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_MalKodu
        {
            get { return this._STK004_MalKodu; }
            set
            {
                this._STK004_MalKodu = value;
                OnPropertyChanged("STK004_MalKodu");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string STK004_Aciklama
        {
            get { return this._STK004_Aciklama; }
            set
            {
                this._STK004_Aciklama = value;
                OnPropertyChanged("STK004_Aciklama");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_GrupKodu
        {
            get { return this._STK004_GrupKodu; }
            set
            {
                this._STK004_GrupKodu = value;
                OnPropertyChanged("STK004_GrupKodu");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_Iskonto
        {
            get { return this._STK004_Iskonto; }
            set
            {
                this._STK004_Iskonto = value;
                OnPropertyChanged("STK004_Iskonto");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_KDV
        {
            get { return this._STK004_KDV; }
            set
            {
                this._STK004_KDV = value;
                OnPropertyChanged("STK004_KDV");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_Birim1
        {
            get { return this._STK004_Birim1; }
            set
            {
                this._STK004_Birim1 = value;
                OnPropertyChanged("STK004_Birim1");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_Birim2
        {
            get { return this._STK004_Birim2; }
            set
            {
                this._STK004_Birim2 = value;
                OnPropertyChanged("STK004_Birim2");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_Birim3
        {
            get { return this._STK004_Birim3; }
            set
            {
                this._STK004_Birim3 = value;
                OnPropertyChanged("STK004_Birim3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_BorcluHesap
        {
            get { return this._STK004_BorcluHesap; }
            set
            {
                this._STK004_BorcluHesap = value;
                OnPropertyChanged("STK004_BorcluHesap");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_AlacakliHesap
        {
            get { return this._STK004_AlacakliHesap; }
            set
            {
                this._STK004_AlacakliHesap = value;
                OnPropertyChanged("STK004_AlacakliHesap");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_AlisFiyati1
        {
            get { return this._STK004_AlisFiyati1; }
            set
            {
                this._STK004_AlisFiyati1 = value;
                OnPropertyChanged("STK004_AlisFiyati1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisKDV1
        {
            get { return this._STK004_AlisKDV1; }
            set
            {
                this._STK004_AlisKDV1 = value;
                OnPropertyChanged("STK004_AlisKDV1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisBirim1
        {
            get { return this._STK004_AlisBirim1; }
            set
            {
                this._STK004_AlisBirim1 = value;
                OnPropertyChanged("STK004_AlisBirim1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_AlisFiyati2
        {
            get { return this._STK004_AlisFiyati2; }
            set
            {
                this._STK004_AlisFiyati2 = value;
                OnPropertyChanged("STK004_AlisFiyati2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisKDV2
        {
            get { return this._STK004_AlisKDV2; }
            set
            {
                this._STK004_AlisKDV2 = value;
                OnPropertyChanged("STK004_AlisKDV2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisBirim2
        {
            get { return this._STK004_AlisBirim2; }
            set
            {
                this._STK004_AlisBirim2 = value;
                OnPropertyChanged("STK004_AlisBirim2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_AlisFiyati3
        {
            get { return this._STK004_AlisFiyati3; }
            set
            {
                this._STK004_AlisFiyati3 = value;
                OnPropertyChanged("STK004_AlisFiyati3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisKDV3
        {
            get { return this._STK004_AlisKDV3; }
            set
            {
                this._STK004_AlisKDV3 = value;
                OnPropertyChanged("STK004_AlisKDV3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AlisBirim3
        {
            get { return this._STK004_AlisBirim3; }
            set
            {
                this._STK004_AlisBirim3 = value;
                OnPropertyChanged("STK004_AlisBirim3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SatisFiyati1
        {
            get { return this._STK004_SatisFiyati1; }
            set
            {
                this._STK004_SatisFiyati1 = value;
                OnPropertyChanged("STK004_SatisFiyati1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisKDV1
        {
            get { return this._STK004_SatisKDV1; }
            set
            {
                this._STK004_SatisKDV1 = value;
                OnPropertyChanged("STK004_SatisKDV1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisBirim1
        {
            get { return this._STK004_SatisBirim1; }
            set
            {
                this._STK004_SatisBirim1 = value;
                OnPropertyChanged("STK004_SatisBirim1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SatisFiyati2
        {
            get { return this._STK004_SatisFiyati2; }
            set
            {
                this._STK004_SatisFiyati2 = value;
                OnPropertyChanged("STK004_SatisFiyati2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisKDV2
        {
            get { return this._STK004_SatisKDV2; }
            set
            {
                this._STK004_SatisKDV2 = value;
                OnPropertyChanged("STK004_SatisKDV2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisBirim2
        {
            get { return this._STK004_SatisBirim2; }
            set
            {
                this._STK004_SatisBirim2 = value;
                OnPropertyChanged("STK004_SatisBirim2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SatisFiyati3
        {
            get { return this._STK004_SatisFiyati3; }
            set
            {
                this._STK004_SatisFiyati3 = value;
                OnPropertyChanged("STK004_SatisFiyati3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisKDV3
        {
            get { return this._STK004_SatisKDV3; }
            set
            {
                this._STK004_SatisKDV3 = value;
                OnPropertyChanged("STK004_SatisKDV3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SatisBirim3
        {
            get { return this._STK004_SatisBirim3; }
            set
            {
                this._STK004_SatisBirim3 = value;
                OnPropertyChanged("STK004_SatisBirim3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DevirMiktari
        {
            get { return this._STK004_DevirMiktari; }
            set
            {
                this._STK004_DevirMiktari = value;
                OnPropertyChanged("STK004_DevirMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DevirTutari
        {
            get { return this._STK004_DevirTutari; }
            set
            {
                this._STK004_DevirTutari = value;
                OnPropertyChanged("STK004_DevirTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_GirisMiktari
        {
            get { return this._STK004_GirisMiktari; }
            set
            {
                this._STK004_GirisMiktari = value;
                OnPropertyChanged("STK004_GirisMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_GirisTutari
        {
            get { return this._STK004_GirisTutari; }
            set
            {
                this._STK004_GirisTutari = value;
                OnPropertyChanged("STK004_GirisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_GirisIskonto
        {
            get { return this._STK004_GirisIskonto; }
            set
            {
                this._STK004_GirisIskonto = value;
                OnPropertyChanged("STK004_GirisIskonto");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_CikisMiktari
        {
            get { return this._STK004_CikisMiktari; }
            set
            {
                this._STK004_CikisMiktari = value;
                OnPropertyChanged("STK004_CikisMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_CikisTutari
        {
            get { return this._STK004_CikisTutari; }
            set
            {
                this._STK004_CikisTutari = value;
                OnPropertyChanged("STK004_CikisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_CikisIskonto
        {
            get { return this._STK004_CikisIskonto; }
            set
            {
                this._STK004_CikisIskonto = value;
                OnPropertyChanged("STK004_CikisIskonto");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_KritikSeviye
        {
            get { return this._STK004_KritikSeviye; }
            set
            {
                this._STK004_KritikSeviye = value;
                OnPropertyChanged("STK004_KritikSeviye");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DevirTarihi
        {
            get { return this._STK004_DevirTarihi; }
            set
            {
                this._STK004_DevirTarihi = value;
                OnPropertyChanged("STK004_DevirTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_KafileBuyuklugu
        {
            get { return this._STK004_KafileBuyuklugu; }
            set
            {
                this._STK004_KafileBuyuklugu = value;
                OnPropertyChanged("STK004_KafileBuyuklugu");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_GirenKaynak
        {
            get { return this._STK004_GirenKaynak; }
            set
            {
                this._STK004_GirenKaynak = value;
                OnPropertyChanged("STK004_GirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_GirenTarih
        {
            get { return this._STK004_GirenTarih; }
            set
            {
                this._STK004_GirenTarih = value;
                OnPropertyChanged("STK004_GirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_GirenSaat
        {
            get { return this._STK004_GirenSaat; }
            set
            {
                this._STK004_GirenSaat = value;
                OnPropertyChanged("STK004_GirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_GirenKodu
        {
            get { return this._STK004_GirenKodu; }
            set
            {
                this._STK004_GirenKodu = value;
                OnPropertyChanged("STK004_GirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_GirenSurum
        {
            get { return this._STK004_GirenSurum; }
            set
            {
                this._STK004_GirenSurum = value;
                OnPropertyChanged("STK004_GirenSurum");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_DegistirenKaynak
        {
            get { return this._STK004_DegistirenKaynak; }
            set
            {
                this._STK004_DegistirenKaynak = value;
                OnPropertyChanged("STK004_DegistirenKaynak");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DegistirenTarih
        {
            get { return this._STK004_DegistirenTarih; }
            set
            {
                this._STK004_DegistirenTarih = value;
                OnPropertyChanged("STK004_DegistirenTarih");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_DegistirenSaat
        {
            get { return this._STK004_DegistirenSaat; }
            set
            {
                this._STK004_DegistirenSaat = value;
                OnPropertyChanged("STK004_DegistirenSaat");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DegistirenKodu
        {
            get { return this._STK004_DegistirenKodu; }
            set
            {
                this._STK004_DegistirenKodu = value;
                OnPropertyChanged("STK004_DegistirenKodu");
            }
        }

        /// <summary> NVARCHAR (16) Allow Null </summary>
        public string STK004_DegistirenSurum
        {
            get { return this._STK004_DegistirenSurum; }
            set
            {
                this._STK004_DegistirenSurum = value;
                OnPropertyChanged("STK004_DegistirenSurum");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_OzelKodu
        {
            get { return this._STK004_OzelKodu; }
            set
            {
                this._STK004_OzelKodu = value;
                OnPropertyChanged("STK004_OzelKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_TipKodu
        {
            get { return this._STK004_TipKodu; }
            set
            {
                this._STK004_TipKodu = value;
                OnPropertyChanged("STK004_TipKodu");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod4
        {
            get { return this._STK004_Kod4; }
            set
            {
                this._STK004_Kod4 = value;
                OnPropertyChanged("STK004_Kod4");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod5
        {
            get { return this._STK004_Kod5; }
            set
            {
                this._STK004_Kod5 = value;
                OnPropertyChanged("STK004_Kod5");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod6
        {
            get { return this._STK004_Kod6; }
            set
            {
                this._STK004_Kod6 = value;
                OnPropertyChanged("STK004_Kod6");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod7
        {
            get { return this._STK004_Kod7; }
            set
            {
                this._STK004_Kod7 = value;
                OnPropertyChanged("STK004_Kod7");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod8
        {
            get { return this._STK004_Kod8; }
            set
            {
                this._STK004_Kod8 = value;
                OnPropertyChanged("STK004_Kod8");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod9
        {
            get { return this._STK004_Kod9; }
            set
            {
                this._STK004_Kod9 = value;
                OnPropertyChanged("STK004_Kod9");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_Kod10
        {
            get { return this._STK004_Kod10; }
            set
            {
                this._STK004_Kod10 = value;
                OnPropertyChanged("STK004_Kod10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Kod11
        {
            get { return this._STK004_Kod11; }
            set
            {
                this._STK004_Kod11 = value;
                OnPropertyChanged("STK004_Kod11");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Kod12
        {
            get { return this._STK004_Kod12; }
            set
            {
                this._STK004_Kod12 = value;
                OnPropertyChanged("STK004_Kod12");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_UreticiKodu1
        {
            get { return this._STK004_UreticiKodu1; }
            set
            {
                this._STK004_UreticiKodu1 = value;
                OnPropertyChanged("STK004_UreticiKodu1");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_UreticiKodu2
        {
            get { return this._STK004_UreticiKodu2; }
            set
            {
                this._STK004_UreticiKodu2 = value;
                OnPropertyChanged("STK004_UreticiKodu2");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_UreticiKodu3
        {
            get { return this._STK004_UreticiKodu3; }
            set
            {
                this._STK004_UreticiKodu3 = value;
                OnPropertyChanged("STK004_UreticiKodu3");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_MusterekMalKodu
        {
            get { return this._STK004_MusterekMalKodu; }
            set
            {
                this._STK004_MusterekMalKodu = value;
                OnPropertyChanged("STK004_MusterekMalKodu");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_MaliyetSekli
        {
            get { return this._STK004_MaliyetSekli; }
            set
            {
                this._STK004_MaliyetSekli = value;
                OnPropertyChanged("STK004_MaliyetSekli");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_FireOrani
        {
            get { return this._STK004_FireOrani; }
            set
            {
                this._STK004_FireOrani = value;
                OnPropertyChanged("STK004_FireOrani");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_TeminYeri
        {
            get { return this._STK004_TeminYeri; }
            set
            {
                this._STK004_TeminYeri = value;
                OnPropertyChanged("STK004_TeminYeri");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_TeminSuresi
        {
            get { return this._STK004_TeminSuresi; }
            set
            {
                this._STK004_TeminSuresi = value;
                OnPropertyChanged("STK004_TeminSuresi");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_Mensei
        {
            get { return this._STK004_Mensei; }
            set
            {
                this._STK004_Mensei = value;
                OnPropertyChanged("STK004_Mensei");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_GTIPN
        {
            get { return this._STK004_GTIPN; }
            set
            {
                this._STK004_GTIPN = value;
                OnPropertyChanged("STK004_GTIPN");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_GumrukOrani
        {
            get { return this._STK004_GumrukOrani; }
            set
            {
                this._STK004_GumrukOrani = value;
                OnPropertyChanged("STK004_GumrukOrani");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Fon
        {
            get { return this._STK004_Fon; }
            set
            {
                this._STK004_Fon = value;
                OnPropertyChanged("STK004_Fon");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizAlis1
        {
            get { return this._STK004_DovizAlis1; }
            set
            {
                this._STK004_DovizAlis1 = value;
                OnPropertyChanged("STK004_DovizAlis1");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizAlisCinsi1
        {
            get { return this._STK004_DovizAlisCinsi1; }
            set
            {
                this._STK004_DovizAlisCinsi1 = value;
                OnPropertyChanged("STK004_DovizAlisCinsi1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizAlis2
        {
            get { return this._STK004_DovizAlis2; }
            set
            {
                this._STK004_DovizAlis2 = value;
                OnPropertyChanged("STK004_DovizAlis2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizAlisCinsi2
        {
            get { return this._STK004_DovizAlisCinsi2; }
            set
            {
                this._STK004_DovizAlisCinsi2 = value;
                OnPropertyChanged("STK004_DovizAlisCinsi2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizAlis3
        {
            get { return this._STK004_DovizAlis3; }
            set
            {
                this._STK004_DovizAlis3 = value;
                OnPropertyChanged("STK004_DovizAlis3");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizAlisCinsi3
        {
            get { return this._STK004_DovizAlisCinsi3; }
            set
            {
                this._STK004_DovizAlisCinsi3 = value;
                OnPropertyChanged("STK004_DovizAlisCinsi3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizSatis1
        {
            get { return this._STK004_DovizSatis1; }
            set
            {
                this._STK004_DovizSatis1 = value;
                OnPropertyChanged("STK004_DovizSatis1");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizSatisCinsi1
        {
            get { return this._STK004_DovizSatisCinsi1; }
            set
            {
                this._STK004_DovizSatisCinsi1 = value;
                OnPropertyChanged("STK004_DovizSatisCinsi1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizSatis2
        {
            get { return this._STK004_DovizSatis2; }
            set
            {
                this._STK004_DovizSatis2 = value;
                OnPropertyChanged("STK004_DovizSatis2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizSatisCinsi2
        {
            get { return this._STK004_DovizSatisCinsi2; }
            set
            {
                this._STK004_DovizSatisCinsi2 = value;
                OnPropertyChanged("STK004_DovizSatisCinsi2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizSatis3
        {
            get { return this._STK004_DovizSatis3; }
            set
            {
                this._STK004_DovizSatis3 = value;
                OnPropertyChanged("STK004_DovizSatis3");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizSatisCinsi3
        {
            get { return this._STK004_DovizSatisCinsi3; }
            set
            {
                this._STK004_DovizSatisCinsi3 = value;
                OnPropertyChanged("STK004_DovizSatisCinsi3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AlisSiparisi
        {
            get { return this._STK004_AlisSiparisi; }
            set
            {
                this._STK004_AlisSiparisi = value;
                OnPropertyChanged("STK004_AlisSiparisi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SatisSiparisi
        {
            get { return this._STK004_SatisSiparisi; }
            set
            {
                this._STK004_SatisSiparisi = value;
                OnPropertyChanged("STK004_SatisSiparisi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AzamiSeviye
        {
            get { return this._STK004_AzamiSeviye; }
            set
            {
                this._STK004_AzamiSeviye = value;
                OnPropertyChanged("STK004_AzamiSeviye");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonGirisTarihi
        {
            get { return this._STK004_SonGirisTarihi; }
            set
            {
                this._STK004_SonGirisTarihi = value;
                OnPropertyChanged("STK004_SonGirisTarihi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonCikisTarihi
        {
            get { return this._STK004_SonCikisTarihi; }
            set
            {
                this._STK004_SonCikisTarihi = value;
                OnPropertyChanged("STK004_SonCikisTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BirimAgirligi
        {
            get { return this._STK004_BirimAgirligi; }
            set
            {
                this._STK004_BirimAgirligi = value;
                OnPropertyChanged("STK004_BirimAgirligi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_ValorGun
        {
            get { return this._STK004_ValorGun; }
            set
            {
                this._STK004_ValorGun = value;
                OnPropertyChanged("STK004_ValorGun");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod1
        {
            get { return this._STK004_Barkod1; }
            set
            {
                this._STK004_Barkod1 = value;
                OnPropertyChanged("STK004_Barkod1");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod2
        {
            get { return this._STK004_Barkod2; }
            set
            {
                this._STK004_Barkod2 = value;
                OnPropertyChanged("STK004_Barkod2");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod3
        {
            get { return this._STK004_Barkod3; }
            set
            {
                this._STK004_Barkod3 = value;
                OnPropertyChanged("STK004_Barkod3");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string STK004_Aciklama2
        {
            get { return this._STK004_Aciklama2; }
            set
            {
                this._STK004_Aciklama2 = value;
                OnPropertyChanged("STK004_Aciklama2");
            }
        }

        /// <summary> NVARCHAR (100) Allow Null </summary>
        public string STK004_Aciklama3
        {
            get { return this._STK004_Aciklama3; }
            set
            {
                this._STK004_Aciklama3 = value;
                OnPropertyChanged("STK004_Aciklama3");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_UygMaliyetTipi
        {
            get { return this._STK004_UygMaliyetTipi; }
            set
            {
                this._STK004_UygMaliyetTipi = value;
                OnPropertyChanged("STK004_UygMaliyetTipi");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SonMaliyetBirimFiyati
        {
            get { return this._STK004_SonMaliyetBirimFiyati; }
            set
            {
                this._STK004_SonMaliyetBirimFiyati = value;
                OnPropertyChanged("STK004_SonMaliyetBirimFiyati");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonMaliyetTarihi
        {
            get { return this._STK004_SonMaliyetTarihi; }
            set
            {
                this._STK004_SonMaliyetTarihi = value;
                OnPropertyChanged("STK004_SonMaliyetTarihi");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_SonMaliyetTipi
        {
            get { return this._STK004_SonMaliyetTipi; }
            set
            {
                this._STK004_SonMaliyetTipi = value;
                OnPropertyChanged("STK004_SonMaliyetTipi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_SonMaliyetParaBirimi
        {
            get { return this._STK004_SonMaliyetParaBirimi; }
            set
            {
                this._STK004_SonMaliyetParaBirimi = value;
                OnPropertyChanged("STK004_SonMaliyetParaBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_Rezervasyon
        {
            get { return this._STK004_Rezervasyon; }
            set
            {
                this._STK004_Rezervasyon = value;
                OnPropertyChanged("STK004_Rezervasyon");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_AlimdanIade
        {
            get { return this._STK004_AlimdanIade; }
            set
            {
                this._STK004_AlimdanIade = value;
                OnPropertyChanged("STK004_AlimdanIade");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_SatistanIade
        {
            get { return this._STK004_SatistanIade; }
            set
            {
                this._STK004_SatistanIade = value;
                OnPropertyChanged("STK004_SatistanIade");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_MalKodu2
        {
            get { return this._STK004_MalKodu2; }
            set
            {
                this._STK004_MalKodu2 = value;
                OnPropertyChanged("STK004_MalKodu2");
            }
        }

        /// <summary> NVARCHAR (6) Allow Null </summary>
        public string STK004_DovizCinsi
        {
            get { return this._STK004_DovizCinsi; }
            set
            {
                this._STK004_DovizCinsi = value;
                OnPropertyChanged("STK004_DovizCinsi");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonAlimFaturasiTarihi
        {
            get { return this._STK004_SonAlimFaturasiTarihi; }
            set
            {
                this._STK004_SonAlimFaturasiTarihi = value;
                OnPropertyChanged("STK004_SonAlimFaturasiTarihi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonAlimFaturasiNo
        {
            get { return this._STK004_SonAlimFaturasiNo; }
            set
            {
                this._STK004_SonAlimFaturasiNo = value;
                OnPropertyChanged("STK004_SonAlimFaturasiNo");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SonAlimFaturasiBirimFiyati
        {
            get { return this._STK004_SonAlimFaturasiBirimFiyati; }
            set
            {
                this._STK004_SonAlimFaturasiBirimFiyati = value;
                OnPropertyChanged("STK004_SonAlimFaturasiBirimFiyati");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonAlimFaturasiCariHesapKodu
        {
            get { return this._STK004_SonAlimFaturasiCariHesapKodu; }
            set
            {
                this._STK004_SonAlimFaturasiCariHesapKodu = value;
                OnPropertyChanged("STK004_SonAlimFaturasiCariHesapKodu");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SonAlimFaturasiDovizBirimFiyati
        {
            get { return this._STK004_SonAlimFaturasiDovizBirimFiyati; }
            set
            {
                this._STK004_SonAlimFaturasiDovizBirimFiyati = value;
                OnPropertyChanged("STK004_SonAlimFaturasiDovizBirimFiyati");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SonSatisFaturasiTarihi
        {
            get { return this._STK004_SonSatisFaturasiTarihi; }
            set
            {
                this._STK004_SonSatisFaturasiTarihi = value;
                OnPropertyChanged("STK004_SonSatisFaturasiTarihi");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonSatisFaturasiNo
        {
            get { return this._STK004_SonSatisFaturasiNo; }
            set
            {
                this._STK004_SonSatisFaturasiNo = value;
                OnPropertyChanged("STK004_SonSatisFaturasiNo");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_SonSatisFaturasiBirimFiyati
        {
            get { return this._STK004_SonSatisFaturasiBirimFiyati; }
            set
            {
                this._STK004_SonSatisFaturasiBirimFiyati = value;
                OnPropertyChanged("STK004_SonSatisFaturasiBirimFiyati");
            }
        }

        /// <summary> NVARCHAR (32) Allow Null </summary>
        public string STK004_SonSatisFaturasiCariHesapKodu
        {
            get { return this._STK004_SonSatisFaturasiCariHesapKodu; }
            set
            {
                this._STK004_SonSatisFaturasiCariHesapKodu = value;
                OnPropertyChanged("STK004_SonSatisFaturasiCariHesapKodu");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SonSatisFaturasiDovizBirimFiyati
        {
            get { return this._STK004_SonSatisFaturasiDovizBirimFiyati; }
            set
            {
                this._STK004_SonSatisFaturasiDovizBirimFiyati = value;
                OnPropertyChanged("STK004_SonSatisFaturasiDovizBirimFiyati");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_MasrafMerkezi
        {
            get { return this._STK004_MasrafMerkezi; }
            set
            {
                this._STK004_MasrafMerkezi = value;
                OnPropertyChanged("STK004_MasrafMerkezi");
            }
        }

        /// <summary> NVARCHAR (256) Allow Null </summary>
        public string STK004_ResimDosyasi
        {
            get { return this._STK004_ResimDosyasi; }
            set
            {
                this._STK004_ResimDosyasi = value;
                OnPropertyChanged("STK004_ResimDosyasi");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_FiyatListesindeCikart
        {
            get { return this._STK004_FiyatListesindeCikart; }
            set
            {
                this._STK004_FiyatListesindeCikart = value;
                OnPropertyChanged("STK004_FiyatListesindeCikart");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SatisFiyati1ValorGun
        {
            get { return this._STK004_SatisFiyati1ValorGun; }
            set
            {
                this._STK004_SatisFiyati1ValorGun = value;
                OnPropertyChanged("STK004_SatisFiyati1ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SatisFiyati2ValorGun
        {
            get { return this._STK004_SatisFiyati2ValorGun; }
            set
            {
                this._STK004_SatisFiyati2ValorGun = value;
                OnPropertyChanged("STK004_SatisFiyati2ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SatisFiyati3ValorGun
        {
            get { return this._STK004_SatisFiyati3ValorGun; }
            set
            {
                this._STK004_SatisFiyati3ValorGun = value;
                OnPropertyChanged("STK004_SatisFiyati3ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DovizSatisFiyati1ValorGun
        {
            get { return this._STK004_DovizSatisFiyati1ValorGun; }
            set
            {
                this._STK004_DovizSatisFiyati1ValorGun = value;
                OnPropertyChanged("STK004_DovizSatisFiyati1ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DovizSatisFiyati2ValorGun
        {
            get { return this._STK004_DovizSatisFiyati2ValorGun; }
            set
            {
                this._STK004_DovizSatisFiyati2ValorGun = value;
                OnPropertyChanged("STK004_DovizSatisFiyati2ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DovizSatisFiyati3ValorGun
        {
            get { return this._STK004_DovizSatisFiyati3ValorGun; }
            set
            {
                this._STK004_DovizSatisFiyati3ValorGun = value;
                OnPropertyChanged("STK004_DovizSatisFiyati3ValorGun");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizDevirTutari
        {
            get { return this._STK004_DovizDevirTutari; }
            set
            {
                this._STK004_DovizDevirTutari = value;
                OnPropertyChanged("STK004_DovizDevirTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizGirisTutari
        {
            get { return this._STK004_DovizGirisTutari; }
            set
            {
                this._STK004_DovizGirisTutari = value;
                OnPropertyChanged("STK004_DovizGirisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizGirisIskontoTutari
        {
            get { return this._STK004_DovizGirisIskontoTutari; }
            set
            {
                this._STK004_DovizGirisIskontoTutari = value;
                OnPropertyChanged("STK004_DovizGirisIskontoTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizCikisTutari
        {
            get { return this._STK004_DovizCikisTutari; }
            set
            {
                this._STK004_DovizCikisTutari = value;
                OnPropertyChanged("STK004_DovizCikisTutari");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DovizCikisIskontoTutari
        {
            get { return this._STK004_DovizCikisIskontoTutari; }
            set
            {
                this._STK004_DovizCikisIskontoTutari = value;
                OnPropertyChanged("STK004_DovizCikisIskontoTutari");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim1
        {
            get { return this._STK004_BarkodBirim1; }
            set
            {
                this._STK004_BarkodBirim1 = value;
                OnPropertyChanged("STK004_BarkodBirim1");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim2
        {
            get { return this._STK004_BarkodBirim2; }
            set
            {
                this._STK004_BarkodBirim2 = value;
                OnPropertyChanged("STK004_BarkodBirim2");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim3
        {
            get { return this._STK004_BarkodBirim3; }
            set
            {
                this._STK004_BarkodBirim3 = value;
                OnPropertyChanged("STK004_BarkodBirim3");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan1
        {
            get { return this._STK004_BarkodCarpan1; }
            set
            {
                this._STK004_BarkodCarpan1 = value;
                OnPropertyChanged("STK004_BarkodCarpan1");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan2
        {
            get { return this._STK004_BarkodCarpan2; }
            set
            {
                this._STK004_BarkodCarpan2 = value;
                OnPropertyChanged("STK004_BarkodCarpan2");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan3
        {
            get { return this._STK004_BarkodCarpan3; }
            set
            {
                this._STK004_BarkodCarpan3 = value;
                OnPropertyChanged("STK004_BarkodCarpan3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DevirMiktari2
        {
            get { return this._STK004_DevirMiktari2; }
            set
            {
                this._STK004_DevirMiktari2 = value;
                OnPropertyChanged("STK004_DevirMiktari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_GirisMiktari2
        {
            get { return this._STK004_GirisMiktari2; }
            set
            {
                this._STK004_GirisMiktari2 = value;
                OnPropertyChanged("STK004_GirisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_GirisTutari2
        {
            get { return this._STK004_GirisTutari2; }
            set
            {
                this._STK004_GirisTutari2 = value;
                OnPropertyChanged("STK004_GirisTutari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_CikisMiktari2
        {
            get { return this._STK004_CikisMiktari2; }
            set
            {
                this._STK004_CikisMiktari2 = value;
                OnPropertyChanged("STK004_CikisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_CikisTutari2
        {
            get { return this._STK004_CikisTutari2; }
            set
            {
                this._STK004_CikisTutari2 = value;
                OnPropertyChanged("STK004_CikisTutari2");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu1
        {
            get { return this._STK004_DepoKodu1; }
            set
            {
                this._STK004_DepoKodu1 = value;
                OnPropertyChanged("STK004_DepoKodu1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari21
        {
            get { return this._STK004_DepoDevirMiktari21; }
            set
            {
                this._STK004_DepoDevirMiktari21 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari21");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari21
        {
            get { return this._STK004_DepoDevirTutari21; }
            set
            {
                this._STK004_DepoDevirTutari21 = value;
                OnPropertyChanged("STK004_DepoDevirTutari21");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari1
        {
            get { return this._STK004_DepoGirisMiktari1; }
            set
            {
                this._STK004_DepoGirisMiktari1 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari1
        {
            get { return this._STK004_DepoGirisTutari1; }
            set
            {
                this._STK004_DepoGirisTutari1 = value;
                OnPropertyChanged("STK004_DepoGirisTutari1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto1
        {
            get { return this._STK004_DepoGirisIskonto1; }
            set
            {
                this._STK004_DepoGirisIskonto1 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari1
        {
            get { return this._STK004_DepoCikisMiktari1; }
            set
            {
                this._STK004_DepoCikisMiktari1 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari1
        {
            get { return this._STK004_DepoCikisTutari1; }
            set
            {
                this._STK004_DepoCikisTutari1 = value;
                OnPropertyChanged("STK004_DepoCikisTutari1");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto1
        {
            get { return this._STK004_DepoCikisIskonto1; }
            set
            {
                this._STK004_DepoCikisIskonto1 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf1
        {
            get { return this._STK004_DepoSonMaliyetBf1; }
            set
            {
                this._STK004_DepoSonMaliyetBf1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf1");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi1
        {
            get { return this._STK004_DepoSonMaliyetTipi1; }
            set
            {
                this._STK004_DepoSonMaliyetTipi1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi1
        {
            get { return this._STK004_DepoSonMaliyetTarihi1; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr1
        {
            get { return this._STK004_DepoSonMaliyetPrBr1; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr1 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi1
        {
            get { return this._STK004_DepoSonDevirTarihi1; }
            set
            {
                this._STK004_DepoSonDevirTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi1
        {
            get { return this._STK004_DepoSonGirisTarihi1; }
            set
            {
                this._STK004_DepoSonGirisTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi1");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi1
        {
            get { return this._STK004_DepoSonCikisTarihi1; }
            set
            {
                this._STK004_DepoSonCikisTarihi1 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi1");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu2
        {
            get { return this._STK004_DepoKodu2; }
            set
            {
                this._STK004_DepoKodu2 = value;
                OnPropertyChanged("STK004_DepoKodu2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari22
        {
            get { return this._STK004_DepoDevirMiktari22; }
            set
            {
                this._STK004_DepoDevirMiktari22 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari22");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari22
        {
            get { return this._STK004_DepoDevirTutari22; }
            set
            {
                this._STK004_DepoDevirTutari22 = value;
                OnPropertyChanged("STK004_DepoDevirTutari22");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari2
        {
            get { return this._STK004_DepoGirisMiktari2; }
            set
            {
                this._STK004_DepoGirisMiktari2 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari2
        {
            get { return this._STK004_DepoGirisTutari2; }
            set
            {
                this._STK004_DepoGirisTutari2 = value;
                OnPropertyChanged("STK004_DepoGirisTutari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto2
        {
            get { return this._STK004_DepoGirisIskonto2; }
            set
            {
                this._STK004_DepoGirisIskonto2 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari2
        {
            get { return this._STK004_DepoCikisMiktari2; }
            set
            {
                this._STK004_DepoCikisMiktari2 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari2
        {
            get { return this._STK004_DepoCikisTutari2; }
            set
            {
                this._STK004_DepoCikisTutari2 = value;
                OnPropertyChanged("STK004_DepoCikisTutari2");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto2
        {
            get { return this._STK004_DepoCikisIskonto2; }
            set
            {
                this._STK004_DepoCikisIskonto2 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf2
        {
            get { return this._STK004_DepoSonMaliyetBf2; }
            set
            {
                this._STK004_DepoSonMaliyetBf2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf2");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi2
        {
            get { return this._STK004_DepoSonMaliyetTipi2; }
            set
            {
                this._STK004_DepoSonMaliyetTipi2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi2
        {
            get { return this._STK004_DepoSonMaliyetTarihi2; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr2
        {
            get { return this._STK004_DepoSonMaliyetPrBr2; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr2 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi2
        {
            get { return this._STK004_DepoSonDevirTarihi2; }
            set
            {
                this._STK004_DepoSonDevirTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi2
        {
            get { return this._STK004_DepoSonGirisTarihi2; }
            set
            {
                this._STK004_DepoSonGirisTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi2");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi2
        {
            get { return this._STK004_DepoSonCikisTarihi2; }
            set
            {
                this._STK004_DepoSonCikisTarihi2 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi2");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu3
        {
            get { return this._STK004_DepoKodu3; }
            set
            {
                this._STK004_DepoKodu3 = value;
                OnPropertyChanged("STK004_DepoKodu3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari23
        {
            get { return this._STK004_DepoDevirMiktari23; }
            set
            {
                this._STK004_DepoDevirMiktari23 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari23");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari23
        {
            get { return this._STK004_DepoDevirTutari23; }
            set
            {
                this._STK004_DepoDevirTutari23 = value;
                OnPropertyChanged("STK004_DepoDevirTutari23");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari3
        {
            get { return this._STK004_DepoGirisMiktari3; }
            set
            {
                this._STK004_DepoGirisMiktari3 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari3
        {
            get { return this._STK004_DepoGirisTutari3; }
            set
            {
                this._STK004_DepoGirisTutari3 = value;
                OnPropertyChanged("STK004_DepoGirisTutari3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto3
        {
            get { return this._STK004_DepoGirisIskonto3; }
            set
            {
                this._STK004_DepoGirisIskonto3 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari3
        {
            get { return this._STK004_DepoCikisMiktari3; }
            set
            {
                this._STK004_DepoCikisMiktari3 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari3
        {
            get { return this._STK004_DepoCikisTutari3; }
            set
            {
                this._STK004_DepoCikisTutari3 = value;
                OnPropertyChanged("STK004_DepoCikisTutari3");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto3
        {
            get { return this._STK004_DepoCikisIskonto3; }
            set
            {
                this._STK004_DepoCikisIskonto3 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto3");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf3
        {
            get { return this._STK004_DepoSonMaliyetBf3; }
            set
            {
                this._STK004_DepoSonMaliyetBf3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf3");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi3
        {
            get { return this._STK004_DepoSonMaliyetTipi3; }
            set
            {
                this._STK004_DepoSonMaliyetTipi3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi3
        {
            get { return this._STK004_DepoSonMaliyetTarihi3; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr3
        {
            get { return this._STK004_DepoSonMaliyetPrBr3; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr3 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi3
        {
            get { return this._STK004_DepoSonDevirTarihi3; }
            set
            {
                this._STK004_DepoSonDevirTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi3
        {
            get { return this._STK004_DepoSonGirisTarihi3; }
            set
            {
                this._STK004_DepoSonGirisTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi3
        {
            get { return this._STK004_DepoSonCikisTarihi3; }
            set
            {
                this._STK004_DepoSonCikisTarihi3 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi3");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu4
        {
            get { return this._STK004_DepoKodu4; }
            set
            {
                this._STK004_DepoKodu4 = value;
                OnPropertyChanged("STK004_DepoKodu4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari24
        {
            get { return this._STK004_DepoDevirMiktari24; }
            set
            {
                this._STK004_DepoDevirMiktari24 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari24");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari24
        {
            get { return this._STK004_DepoDevirTutari24; }
            set
            {
                this._STK004_DepoDevirTutari24 = value;
                OnPropertyChanged("STK004_DepoDevirTutari24");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari4
        {
            get { return this._STK004_DepoGirisMiktari4; }
            set
            {
                this._STK004_DepoGirisMiktari4 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari4");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari4
        {
            get { return this._STK004_DepoGirisTutari4; }
            set
            {
                this._STK004_DepoGirisTutari4 = value;
                OnPropertyChanged("STK004_DepoGirisTutari4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto4
        {
            get { return this._STK004_DepoGirisIskonto4; }
            set
            {
                this._STK004_DepoGirisIskonto4 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari4
        {
            get { return this._STK004_DepoCikisMiktari4; }
            set
            {
                this._STK004_DepoCikisMiktari4 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari4");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari4
        {
            get { return this._STK004_DepoCikisTutari4; }
            set
            {
                this._STK004_DepoCikisTutari4 = value;
                OnPropertyChanged("STK004_DepoCikisTutari4");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto4
        {
            get { return this._STK004_DepoCikisIskonto4; }
            set
            {
                this._STK004_DepoCikisIskonto4 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto4");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf4
        {
            get { return this._STK004_DepoSonMaliyetBf4; }
            set
            {
                this._STK004_DepoSonMaliyetBf4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf4");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi4
        {
            get { return this._STK004_DepoSonMaliyetTipi4; }
            set
            {
                this._STK004_DepoSonMaliyetTipi4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi4
        {
            get { return this._STK004_DepoSonMaliyetTarihi4; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi4");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr4
        {
            get { return this._STK004_DepoSonMaliyetPrBr4; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr4 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi4
        {
            get { return this._STK004_DepoSonDevirTarihi4; }
            set
            {
                this._STK004_DepoSonDevirTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi4
        {
            get { return this._STK004_DepoSonGirisTarihi4; }
            set
            {
                this._STK004_DepoSonGirisTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi4");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi4
        {
            get { return this._STK004_DepoSonCikisTarihi4; }
            set
            {
                this._STK004_DepoSonCikisTarihi4 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi4");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu5
        {
            get { return this._STK004_DepoKodu5; }
            set
            {
                this._STK004_DepoKodu5 = value;
                OnPropertyChanged("STK004_DepoKodu5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari25
        {
            get { return this._STK004_DepoDevirMiktari25; }
            set
            {
                this._STK004_DepoDevirMiktari25 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari25");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari25
        {
            get { return this._STK004_DepoDevirTutari25; }
            set
            {
                this._STK004_DepoDevirTutari25 = value;
                OnPropertyChanged("STK004_DepoDevirTutari25");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari5
        {
            get { return this._STK004_DepoGirisMiktari5; }
            set
            {
                this._STK004_DepoGirisMiktari5 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari5");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari5
        {
            get { return this._STK004_DepoGirisTutari5; }
            set
            {
                this._STK004_DepoGirisTutari5 = value;
                OnPropertyChanged("STK004_DepoGirisTutari5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto5
        {
            get { return this._STK004_DepoGirisIskonto5; }
            set
            {
                this._STK004_DepoGirisIskonto5 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari5
        {
            get { return this._STK004_DepoCikisMiktari5; }
            set
            {
                this._STK004_DepoCikisMiktari5 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari5");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari5
        {
            get { return this._STK004_DepoCikisTutari5; }
            set
            {
                this._STK004_DepoCikisTutari5 = value;
                OnPropertyChanged("STK004_DepoCikisTutari5");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto5
        {
            get { return this._STK004_DepoCikisIskonto5; }
            set
            {
                this._STK004_DepoCikisIskonto5 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto5");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf5
        {
            get { return this._STK004_DepoSonMaliyetBf5; }
            set
            {
                this._STK004_DepoSonMaliyetBf5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf5");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi5
        {
            get { return this._STK004_DepoSonMaliyetTipi5; }
            set
            {
                this._STK004_DepoSonMaliyetTipi5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi5
        {
            get { return this._STK004_DepoSonMaliyetTarihi5; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi5");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr5
        {
            get { return this._STK004_DepoSonMaliyetPrBr5; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr5 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi5
        {
            get { return this._STK004_DepoSonDevirTarihi5; }
            set
            {
                this._STK004_DepoSonDevirTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi5
        {
            get { return this._STK004_DepoSonGirisTarihi5; }
            set
            {
                this._STK004_DepoSonGirisTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi5
        {
            get { return this._STK004_DepoSonCikisTarihi5; }
            set
            {
                this._STK004_DepoSonCikisTarihi5 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi5");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu6
        {
            get { return this._STK004_DepoKodu6; }
            set
            {
                this._STK004_DepoKodu6 = value;
                OnPropertyChanged("STK004_DepoKodu6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari26
        {
            get { return this._STK004_DepoDevirMiktari26; }
            set
            {
                this._STK004_DepoDevirMiktari26 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari26");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari26
        {
            get { return this._STK004_DepoDevirTutari26; }
            set
            {
                this._STK004_DepoDevirTutari26 = value;
                OnPropertyChanged("STK004_DepoDevirTutari26");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari6
        {
            get { return this._STK004_DepoGirisMiktari6; }
            set
            {
                this._STK004_DepoGirisMiktari6 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari6");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari6
        {
            get { return this._STK004_DepoGirisTutari6; }
            set
            {
                this._STK004_DepoGirisTutari6 = value;
                OnPropertyChanged("STK004_DepoGirisTutari6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto6
        {
            get { return this._STK004_DepoGirisIskonto6; }
            set
            {
                this._STK004_DepoGirisIskonto6 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari6
        {
            get { return this._STK004_DepoCikisMiktari6; }
            set
            {
                this._STK004_DepoCikisMiktari6 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari6");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari6
        {
            get { return this._STK004_DepoCikisTutari6; }
            set
            {
                this._STK004_DepoCikisTutari6 = value;
                OnPropertyChanged("STK004_DepoCikisTutari6");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto6
        {
            get { return this._STK004_DepoCikisIskonto6; }
            set
            {
                this._STK004_DepoCikisIskonto6 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto6");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf6
        {
            get { return this._STK004_DepoSonMaliyetBf6; }
            set
            {
                this._STK004_DepoSonMaliyetBf6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf6");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi6
        {
            get { return this._STK004_DepoSonMaliyetTipi6; }
            set
            {
                this._STK004_DepoSonMaliyetTipi6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi6
        {
            get { return this._STK004_DepoSonMaliyetTarihi6; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi6");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr6
        {
            get { return this._STK004_DepoSonMaliyetPrBr6; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr6 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi6
        {
            get { return this._STK004_DepoSonDevirTarihi6; }
            set
            {
                this._STK004_DepoSonDevirTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi6
        {
            get { return this._STK004_DepoSonGirisTarihi6; }
            set
            {
                this._STK004_DepoSonGirisTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi6");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi6
        {
            get { return this._STK004_DepoSonCikisTarihi6; }
            set
            {
                this._STK004_DepoSonCikisTarihi6 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi6");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu7
        {
            get { return this._STK004_DepoKodu7; }
            set
            {
                this._STK004_DepoKodu7 = value;
                OnPropertyChanged("STK004_DepoKodu7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari27
        {
            get { return this._STK004_DepoDevirMiktari27; }
            set
            {
                this._STK004_DepoDevirMiktari27 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari27");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari27
        {
            get { return this._STK004_DepoDevirTutari27; }
            set
            {
                this._STK004_DepoDevirTutari27 = value;
                OnPropertyChanged("STK004_DepoDevirTutari27");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari7
        {
            get { return this._STK004_DepoGirisMiktari7; }
            set
            {
                this._STK004_DepoGirisMiktari7 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari7
        {
            get { return this._STK004_DepoGirisTutari7; }
            set
            {
                this._STK004_DepoGirisTutari7 = value;
                OnPropertyChanged("STK004_DepoGirisTutari7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto7
        {
            get { return this._STK004_DepoGirisIskonto7; }
            set
            {
                this._STK004_DepoGirisIskonto7 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari7
        {
            get { return this._STK004_DepoCikisMiktari7; }
            set
            {
                this._STK004_DepoCikisMiktari7 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari7
        {
            get { return this._STK004_DepoCikisTutari7; }
            set
            {
                this._STK004_DepoCikisTutari7 = value;
                OnPropertyChanged("STK004_DepoCikisTutari7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto7
        {
            get { return this._STK004_DepoCikisIskonto7; }
            set
            {
                this._STK004_DepoCikisIskonto7 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto7");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf7
        {
            get { return this._STK004_DepoSonMaliyetBf7; }
            set
            {
                this._STK004_DepoSonMaliyetBf7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf7");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi7
        {
            get { return this._STK004_DepoSonMaliyetTipi7; }
            set
            {
                this._STK004_DepoSonMaliyetTipi7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi7
        {
            get { return this._STK004_DepoSonMaliyetTarihi7; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi7");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr7
        {
            get { return this._STK004_DepoSonMaliyetPrBr7; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr7 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi7
        {
            get { return this._STK004_DepoSonDevirTarihi7; }
            set
            {
                this._STK004_DepoSonDevirTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi7
        {
            get { return this._STK004_DepoSonGirisTarihi7; }
            set
            {
                this._STK004_DepoSonGirisTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi7");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi7
        {
            get { return this._STK004_DepoSonCikisTarihi7; }
            set
            {
                this._STK004_DepoSonCikisTarihi7 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi7");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu8
        {
            get { return this._STK004_DepoKodu8; }
            set
            {
                this._STK004_DepoKodu8 = value;
                OnPropertyChanged("STK004_DepoKodu8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari28
        {
            get { return this._STK004_DepoDevirMiktari28; }
            set
            {
                this._STK004_DepoDevirMiktari28 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari28");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari28
        {
            get { return this._STK004_DepoDevirTutari28; }
            set
            {
                this._STK004_DepoDevirTutari28 = value;
                OnPropertyChanged("STK004_DepoDevirTutari28");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari8
        {
            get { return this._STK004_DepoGirisMiktari8; }
            set
            {
                this._STK004_DepoGirisMiktari8 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari8
        {
            get { return this._STK004_DepoGirisTutari8; }
            set
            {
                this._STK004_DepoGirisTutari8 = value;
                OnPropertyChanged("STK004_DepoGirisTutari8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto8
        {
            get { return this._STK004_DepoGirisIskonto8; }
            set
            {
                this._STK004_DepoGirisIskonto8 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari8
        {
            get { return this._STK004_DepoCikisMiktari8; }
            set
            {
                this._STK004_DepoCikisMiktari8 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari8
        {
            get { return this._STK004_DepoCikisTutari8; }
            set
            {
                this._STK004_DepoCikisTutari8 = value;
                OnPropertyChanged("STK004_DepoCikisTutari8");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto8
        {
            get { return this._STK004_DepoCikisIskonto8; }
            set
            {
                this._STK004_DepoCikisIskonto8 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto8");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf8
        {
            get { return this._STK004_DepoSonMaliyetBf8; }
            set
            {
                this._STK004_DepoSonMaliyetBf8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf8");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi8
        {
            get { return this._STK004_DepoSonMaliyetTipi8; }
            set
            {
                this._STK004_DepoSonMaliyetTipi8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi8
        {
            get { return this._STK004_DepoSonMaliyetTarihi8; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi8");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr8
        {
            get { return this._STK004_DepoSonMaliyetPrBr8; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr8 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi8
        {
            get { return this._STK004_DepoSonDevirTarihi8; }
            set
            {
                this._STK004_DepoSonDevirTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi8
        {
            get { return this._STK004_DepoSonGirisTarihi8; }
            set
            {
                this._STK004_DepoSonGirisTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi8");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi8
        {
            get { return this._STK004_DepoSonCikisTarihi8; }
            set
            {
                this._STK004_DepoSonCikisTarihi8 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi8");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu9
        {
            get { return this._STK004_DepoKodu9; }
            set
            {
                this._STK004_DepoKodu9 = value;
                OnPropertyChanged("STK004_DepoKodu9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari29
        {
            get { return this._STK004_DepoDevirMiktari29; }
            set
            {
                this._STK004_DepoDevirMiktari29 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari29");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari29
        {
            get { return this._STK004_DepoDevirTutari29; }
            set
            {
                this._STK004_DepoDevirTutari29 = value;
                OnPropertyChanged("STK004_DepoDevirTutari29");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari9
        {
            get { return this._STK004_DepoGirisMiktari9; }
            set
            {
                this._STK004_DepoGirisMiktari9 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari9");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari9
        {
            get { return this._STK004_DepoGirisTutari9; }
            set
            {
                this._STK004_DepoGirisTutari9 = value;
                OnPropertyChanged("STK004_DepoGirisTutari9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto9
        {
            get { return this._STK004_DepoGirisIskonto9; }
            set
            {
                this._STK004_DepoGirisIskonto9 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari9
        {
            get { return this._STK004_DepoCikisMiktari9; }
            set
            {
                this._STK004_DepoCikisMiktari9 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari9");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari9
        {
            get { return this._STK004_DepoCikisTutari9; }
            set
            {
                this._STK004_DepoCikisTutari9 = value;
                OnPropertyChanged("STK004_DepoCikisTutari9");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto9
        {
            get { return this._STK004_DepoCikisIskonto9; }
            set
            {
                this._STK004_DepoCikisIskonto9 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto9");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf9
        {
            get { return this._STK004_DepoSonMaliyetBf9; }
            set
            {
                this._STK004_DepoSonMaliyetBf9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf9");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi9
        {
            get { return this._STK004_DepoSonMaliyetTipi9; }
            set
            {
                this._STK004_DepoSonMaliyetTipi9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi9
        {
            get { return this._STK004_DepoSonMaliyetTarihi9; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi9");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr9
        {
            get { return this._STK004_DepoSonMaliyetPrBr9; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr9 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi9
        {
            get { return this._STK004_DepoSonDevirTarihi9; }
            set
            {
                this._STK004_DepoSonDevirTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi9
        {
            get { return this._STK004_DepoSonGirisTarihi9; }
            set
            {
                this._STK004_DepoSonGirisTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi9");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi9
        {
            get { return this._STK004_DepoSonCikisTarihi9; }
            set
            {
                this._STK004_DepoSonCikisTarihi9 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi9");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_DepoKodu10
        {
            get { return this._STK004_DepoKodu10; }
            set
            {
                this._STK004_DepoKodu10 = value;
                OnPropertyChanged("STK004_DepoKodu10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoDevirMiktari210
        {
            get { return this._STK004_DepoDevirMiktari210; }
            set
            {
                this._STK004_DepoDevirMiktari210 = value;
                OnPropertyChanged("STK004_DepoDevirMiktari210");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoDevirTutari210
        {
            get { return this._STK004_DepoDevirTutari210; }
            set
            {
                this._STK004_DepoDevirTutari210 = value;
                OnPropertyChanged("STK004_DepoDevirTutari210");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisMiktari10
        {
            get { return this._STK004_DepoGirisMiktari10; }
            set
            {
                this._STK004_DepoGirisMiktari10 = value;
                OnPropertyChanged("STK004_DepoGirisMiktari10");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoGirisTutari10
        {
            get { return this._STK004_DepoGirisTutari10; }
            set
            {
                this._STK004_DepoGirisTutari10 = value;
                OnPropertyChanged("STK004_DepoGirisTutari10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoGirisIskonto10
        {
            get { return this._STK004_DepoGirisIskonto10; }
            set
            {
                this._STK004_DepoGirisIskonto10 = value;
                OnPropertyChanged("STK004_DepoGirisIskonto10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisMiktari10
        {
            get { return this._STK004_DepoCikisMiktari10; }
            set
            {
                this._STK004_DepoCikisMiktari10 = value;
                OnPropertyChanged("STK004_DepoCikisMiktari10");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoCikisTutari10
        {
            get { return this._STK004_DepoCikisTutari10; }
            set
            {
                this._STK004_DepoCikisTutari10 = value;
                OnPropertyChanged("STK004_DepoCikisTutari10");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_DepoCikisIskonto10
        {
            get { return this._STK004_DepoCikisIskonto10; }
            set
            {
                this._STK004_DepoCikisIskonto10 = value;
                OnPropertyChanged("STK004_DepoCikisIskonto10");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_DepoSonMaliyetBf10
        {
            get { return this._STK004_DepoSonMaliyetBf10; }
            set
            {
                this._STK004_DepoSonMaliyetBf10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetBf10");
            }
        }

        /// <summary> SMALLINT (2) Allow Null </summary>
        public short? STK004_DepoSonMaliyetTipi10
        {
            get { return this._STK004_DepoSonMaliyetTipi10; }
            set
            {
                this._STK004_DepoSonMaliyetTipi10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTipi10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonMaliyetTarihi10
        {
            get { return this._STK004_DepoSonMaliyetTarihi10; }
            set
            {
                this._STK004_DepoSonMaliyetTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetTarihi10");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_DepoSonMaliyetPrBr10
        {
            get { return this._STK004_DepoSonMaliyetPrBr10; }
            set
            {
                this._STK004_DepoSonMaliyetPrBr10 = value;
                OnPropertyChanged("STK004_DepoSonMaliyetPrBr10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonDevirTarihi10
        {
            get { return this._STK004_DepoSonDevirTarihi10; }
            set
            {
                this._STK004_DepoSonDevirTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonDevirTarihi10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonGirisTarihi10
        {
            get { return this._STK004_DepoSonGirisTarihi10; }
            set
            {
                this._STK004_DepoSonGirisTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonGirisTarihi10");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_DepoSonCikisTarihi10
        {
            get { return this._STK004_DepoSonCikisTarihi10; }
            set
            {
                this._STK004_DepoSonCikisTarihi10 = value;
                OnPropertyChanged("STK004_DepoSonCikisTarihi10");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_StokMiktar2Br
        {
            get { return this._STK004_StokMiktar2Br; }
            set
            {
                this._STK004_StokMiktar2Br = value;
                OnPropertyChanged("STK004_StokMiktar2Br");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH1
        {
            get { return this._STK004_StokMCH1; }
            set
            {
                this._STK004_StokMCH1 = value;
                OnPropertyChanged("STK004_StokMCH1");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP1
        {
            get { return this._STK004_MOP1; }
            set
            {
                this._STK004_MOP1 = value;
                OnPropertyChanged("STK004_MOP1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH2
        {
            get { return this._STK004_StokMCH2; }
            set
            {
                this._STK004_StokMCH2 = value;
                OnPropertyChanged("STK004_StokMCH2");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP2
        {
            get { return this._STK004_MOP2; }
            set
            {
                this._STK004_MOP2 = value;
                OnPropertyChanged("STK004_MOP2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH3
        {
            get { return this._STK004_StokMCH3; }
            set
            {
                this._STK004_StokMCH3 = value;
                OnPropertyChanged("STK004_StokMCH3");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP3
        {
            get { return this._STK004_MOP3; }
            set
            {
                this._STK004_MOP3 = value;
                OnPropertyChanged("STK004_MOP3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH4
        {
            get { return this._STK004_StokMCH4; }
            set
            {
                this._STK004_StokMCH4 = value;
                OnPropertyChanged("STK004_StokMCH4");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP4
        {
            get { return this._STK004_MOP4; }
            set
            {
                this._STK004_MOP4 = value;
                OnPropertyChanged("STK004_MOP4");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMCH5
        {
            get { return this._STK004_StokMCH5; }
            set
            {
                this._STK004_StokMCH5 = value;
                OnPropertyChanged("STK004_StokMCH5");
            }
        }

        /// <summary> NVARCHAR (2) Allow Null </summary>
        public string STK004_MOP5
        {
            get { return this._STK004_MOP5; }
            set
            {
                this._STK004_MOP5 = value;
                OnPropertyChanged("STK004_MOP5");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokMSOP
        {
            get { return this._STK004_StokMSOP; }
            set
            {
                this._STK004_StokMSOP = value;
                OnPropertyChanged("STK004_StokMSOP");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_StokDevirTutar2
        {
            get { return this._STK004_StokDevirTutar2; }
            set
            {
                this._STK004_StokDevirTutar2 = value;
                OnPropertyChanged("STK004_StokDevirTutar2");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not1
        {
            get { return this._STK004_Not1; }
            set
            {
                this._STK004_Not1 = value;
                OnPropertyChanged("STK004_Not1");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not2
        {
            get { return this._STK004_Not2; }
            set
            {
                this._STK004_Not2 = value;
                OnPropertyChanged("STK004_Not2");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not3
        {
            get { return this._STK004_Not3; }
            set
            {
                this._STK004_Not3 = value;
                OnPropertyChanged("STK004_Not3");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not4
        {
            get { return this._STK004_Not4; }
            set
            {
                this._STK004_Not4 = value;
                OnPropertyChanged("STK004_Not4");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not5
        {
            get { return this._STK004_Not5; }
            set
            {
                this._STK004_Not5 = value;
                OnPropertyChanged("STK004_Not5");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not6
        {
            get { return this._STK004_Not6; }
            set
            {
                this._STK004_Not6 = value;
                OnPropertyChanged("STK004_Not6");
            }
        }

        /// <summary> NVARCHAR (200) Allow Null </summary>
        public string STK004_Not7
        {
            get { return this._STK004_Not7; }
            set
            {
                this._STK004_Not7 = value;
                OnPropertyChanged("STK004_Not7");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_StokDBMiktari
        {
            get { return this._STK004_StokDBMiktari; }
            set
            {
                this._STK004_StokDBMiktari = value;
                OnPropertyChanged("STK004_StokDBMiktari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_StokDBTutari
        {
            get { return this._STK004_StokDBTutari; }
            set
            {
                this._STK004_StokDBTutari = value;
                OnPropertyChanged("STK004_StokDBTutari");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_StokDBDuzTutari
        {
            get { return this._STK004_StokDBDuzTutari; }
            set
            {
                this._STK004_StokDBDuzTutari = value;
                OnPropertyChanged("STK004_StokDBDuzTutari");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_StokDBDuzDate
        {
            get { return this._STK004_StokDBDuzDate; }
            set
            {
                this._STK004_StokDBDuzDate = value;
                OnPropertyChanged("STK004_StokDBDuzDate");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_StokDBDuzYil
        {
            get { return this._STK004_StokDBDuzYil; }
            set
            {
                this._STK004_StokDBDuzYil = value;
                OnPropertyChanged("STK004_StokDBDuzYil");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_StokDBDuzDonem
        {
            get { return this._STK004_StokDBDuzDonem; }
            set
            {
                this._STK004_StokDBDuzDonem = value;
                OnPropertyChanged("STK004_StokDBDuzDonem");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_StokDBRofm
        {
            get { return this._STK004_StokDBRofm; }
            set
            {
                this._STK004_StokDBRofm = value;
                OnPropertyChanged("STK004_StokDBRofm");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_StokDBEntHesapKodu
        {
            get { return this._STK004_StokDBEntHesapKodu; }
            set
            {
                this._STK004_StokDBEntHesapKodu = value;
                OnPropertyChanged("STK004_StokDBEntHesapKodu");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_YASatisFiati1
        {
            get { return this._STK004_YASatisFiati1; }
            set
            {
                this._STK004_YASatisFiati1 = value;
                OnPropertyChanged("STK004_YASatisFiati1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisKDV1
        {
            get { return this._STK004_YSatisKDV1; }
            set
            {
                this._STK004_YSatisKDV1 = value;
                OnPropertyChanged("STK004_YSatisKDV1");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisBirim1
        {
            get { return this._STK004_YSatisBirim1; }
            set
            {
                this._STK004_YSatisBirim1 = value;
                OnPropertyChanged("STK004_YSatisBirim1");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_YASatisFiati2
        {
            get { return this._STK004_YASatisFiati2; }
            set
            {
                this._STK004_YASatisFiati2 = value;
                OnPropertyChanged("STK004_YASatisFiati2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisKDV2
        {
            get { return this._STK004_YSatisKDV2; }
            set
            {
                this._STK004_YSatisKDV2 = value;
                OnPropertyChanged("STK004_YSatisKDV2");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisBirim2
        {
            get { return this._STK004_YSatisBirim2; }
            set
            {
                this._STK004_YSatisBirim2 = value;
                OnPropertyChanged("STK004_YSatisBirim2");
            }
        }

        /// <summary> NUMERIC (13) Allow Null </summary>
        public decimal? STK004_YASatisFiati3
        {
            get { return this._STK004_YASatisFiati3; }
            set
            {
                this._STK004_YASatisFiati3 = value;
                OnPropertyChanged("STK004_YASatisFiati3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisKDV3
        {
            get { return this._STK004_YSatisKDV3; }
            set
            {
                this._STK004_YSatisKDV3 = value;
                OnPropertyChanged("STK004_YSatisKDV3");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YSatisBirim3
        {
            get { return this._STK004_YSatisBirim3; }
            set
            {
                this._STK004_YSatisBirim3 = value;
                OnPropertyChanged("STK004_YSatisBirim3");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YSatisFiyati1ValorGun
        {
            get { return this._STK004_YSatisFiyati1ValorGun; }
            set
            {
                this._STK004_YSatisFiyati1ValorGun = value;
                OnPropertyChanged("STK004_YSatisFiyati1ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YSatisFiyati2ValorGun
        {
            get { return this._STK004_YSatisFiyati2ValorGun; }
            set
            {
                this._STK004_YSatisFiyati2ValorGun = value;
                OnPropertyChanged("STK004_YSatisFiyati2ValorGun");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YSatisFiyati3ValorGun
        {
            get { return this._STK004_YSatisFiyati3ValorGun; }
            set
            {
                this._STK004_YSatisFiyati3ValorGun = value;
                OnPropertyChanged("STK004_YSatisFiyati3ValorGun");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_AktifFlag
        {
            get { return this._STK004_AktifFlag; }
            set
            {
                this._STK004_AktifFlag = value;
                OnPropertyChanged("STK004_AktifFlag");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_SayimTarihi
        {
            get { return this._STK004_SayimTarihi; }
            set
            {
                this._STK004_SayimTarihi = value;
                OnPropertyChanged("STK004_SayimTarihi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_SayimMiktari
        {
            get { return this._STK004_SayimMiktari; }
            set
            {
                this._STK004_SayimMiktari = value;
                OnPropertyChanged("STK004_SayimMiktari");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string STK004_Dokuman1
        {
            get { return this._STK004_Dokuman1; }
            set
            {
                this._STK004_Dokuman1 = value;
                OnPropertyChanged("STK004_Dokuman1");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string STK004_Dokuman2
        {
            get { return this._STK004_Dokuman2; }
            set
            {
                this._STK004_Dokuman2 = value;
                OnPropertyChanged("STK004_Dokuman2");
            }
        }

        /// <summary> NVARCHAR (64) Allow Null </summary>
        public string STK004_Dokuman3
        {
            get { return this._STK004_Dokuman3; }
            set
            {
                this._STK004_Dokuman3 = value;
                OnPropertyChanged("STK004_Dokuman3");
            }
        }

        /// <summary> NVARCHAR (40) Allow Null </summary>
        public string STK004_SMMHesapKodu
        {
            get { return this._STK004_SMMHesapKodu; }
            set
            {
                this._STK004_SMMHesapKodu = value;
                OnPropertyChanged("STK004_SMMHesapKodu");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_KDVTevkIslemTuruAlim
        {
            get { return this._STK004_KDVTevkIslemTuruAlim; }
            set
            {
                this._STK004_KDVTevkIslemTuruAlim = value;
                OnPropertyChanged("STK004_KDVTevkIslemTuruAlim");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod4
        {
            get { return this._STK004_Barkod4; }
            set
            {
                this._STK004_Barkod4 = value;
                OnPropertyChanged("STK004_Barkod4");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string STK004_Barkod5
        {
            get { return this._STK004_Barkod5; }
            set
            {
                this._STK004_Barkod5 = value;
                OnPropertyChanged("STK004_Barkod5");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim4
        {
            get { return this._STK004_BarkodBirim4; }
            set
            {
                this._STK004_BarkodBirim4 = value;
                OnPropertyChanged("STK004_BarkodBirim4");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BarkodBirim5
        {
            get { return this._STK004_BarkodBirim5; }
            set
            {
                this._STK004_BarkodBirim5 = value;
                OnPropertyChanged("STK004_BarkodBirim5");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan4
        {
            get { return this._STK004_BarkodCarpan4; }
            set
            {
                this._STK004_BarkodCarpan4 = value;
                OnPropertyChanged("STK004_BarkodCarpan4");
            }
        }

        /// <summary> NUMERIC (5) Allow Null </summary>
        public decimal? STK004_BarkodCarpan5
        {
            get { return this._STK004_BarkodCarpan5; }
            set
            {
                this._STK004_BarkodCarpan5 = value;
                OnPropertyChanged("STK004_BarkodCarpan5");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_KDVTevkIslemTuruSatis
        {
            get { return this._STK004_KDVTevkIslemTuruSatis; }
            set
            {
                this._STK004_KDVTevkIslemTuruSatis = value;
                OnPropertyChanged("STK004_KDVTevkIslemTuruSatis");
            }
        }

        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string STK004_KDVTevkOraniAlim
        {
            get { return this._STK004_KDVTevkOraniAlim; }
            set
            {
                this._STK004_KDVTevkOraniAlim = value;
                OnPropertyChanged("STK004_KDVTevkOraniAlim");
            }
        }

        /// <summary> NVARCHAR (14) Allow Null </summary>
        public string STK004_KDVTevkOraniSatis
        {
            get { return this._STK004_KDVTevkOraniSatis; }
            set
            {
                this._STK004_KDVTevkOraniSatis = value;
                OnPropertyChanged("STK004_KDVTevkOraniSatis");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_KismiKDVMuafMevcut
        {
            get { return this._STK004_KismiKDVMuafMevcut; }
            set
            {
                this._STK004_KismiKDVMuafMevcut = value;
                OnPropertyChanged("STK004_KismiKDVMuafMevcut");
            }
        }

        /// <summary> NVARCHAR (20) Allow Null </summary>
        public string STK004_KDVMuafAciklama
        {
            get { return this._STK004_KDVMuafAciklama; }
            set
            {
                this._STK004_KDVMuafAciklama = value;
                OnPropertyChanged("STK004_KDVMuafAciklama");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_BoyutBirimi
        {
            get { return this._STK004_BoyutBirimi; }
            set
            {
                this._STK004_BoyutBirimi = value;
                OnPropertyChanged("STK004_BoyutBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BoyutEn
        {
            get { return this._STK004_BoyutEn; }
            set
            {
                this._STK004_BoyutEn = value;
                OnPropertyChanged("STK004_BoyutEn");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BoyutBoy
        {
            get { return this._STK004_BoyutBoy; }
            set
            {
                this._STK004_BoyutBoy = value;
                OnPropertyChanged("STK004_BoyutBoy");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_BoyutGenisilik
        {
            get { return this._STK004_BoyutGenisilik; }
            set
            {
                this._STK004_BoyutGenisilik = value;
                OnPropertyChanged("STK004_BoyutGenisilik");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_AgirlikBirimi
        {
            get { return this._STK004_AgirlikBirimi; }
            set
            {
                this._STK004_AgirlikBirimi = value;
                OnPropertyChanged("STK004_AgirlikBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AgirlikBrut
        {
            get { return this._STK004_AgirlikBrut; }
            set
            {
                this._STK004_AgirlikBrut = value;
                OnPropertyChanged("STK004_AgirlikBrut");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_AgirlikNet
        {
            get { return this._STK004_AgirlikNet; }
            set
            {
                this._STK004_AgirlikNet = value;
                OnPropertyChanged("STK004_AgirlikNet");
            }
        }

        /// <summary> NVARCHAR (10) Allow Null </summary>
        public string STK004_HacimBirimi
        {
            get { return this._STK004_HacimBirimi; }
            set
            {
                this._STK004_HacimBirimi = value;
                OnPropertyChanged("STK004_HacimBirimi");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_HacimBrut
        {
            get { return this._STK004_HacimBrut; }
            set
            {
                this._STK004_HacimBrut = value;
                OnPropertyChanged("STK004_HacimBrut");
            }
        }

        /// <summary> NUMERIC (9) Allow Null </summary>
        public decimal? STK004_HacimNet
        {
            get { return this._STK004_HacimNet; }
            set
            {
                this._STK004_HacimNet = value;
                OnPropertyChanged("STK004_HacimNet");
            }
        }

        /// <summary> TINYINT (1) Allow Null </summary>
        public byte? STK004_YOKCPLUGonder
        {
            get { return this._STK004_YOKCPLUGonder; }
            set
            {
                this._STK004_YOKCPLUGonder = value;
                OnPropertyChanged("STK004_YOKCPLUGonder");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YOKCPLUID
        {
            get { return this._STK004_YOKCPLUID; }
            set
            {
                this._STK004_YOKCPLUID = value;
                OnPropertyChanged("STK004_YOKCPLUID");
            }
        }

        /// <summary> INT (4) Allow Null </summary>
        public int? STK004_YOKCDepartmanID
        {
            get { return this._STK004_YOKCDepartmanID; }
            set
            {
                this._STK004_YOKCDepartmanID = value;
                OnPropertyChanged("STK004_YOKCDepartmanID");
            }
        }

        /// <summary> NVARCHAR (60) Allow Null </summary>
        public string pk_STK004_MalKodu
        {
            get { return this._pk_STK004_MalKodu; }
            set
            {
                this._pk_STK004_MalKodu = value;
                OnPropertyChanged("pk_STK004_MalKodu");
            }
        }


        #endregion /// Properties       
        #region Tablo Bilgileri & Metodlar

        public void WhereAdd(STK004E Property, object Deger, Operand And_Or = Operand.AND)
        {
            WhereList.Add(SqlExperOperatorIslem.WhereAdd(Enum.GetName(typeof(STK004E), Property), Deger, And_Or));
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

        private List<string> WhereList = new List<string>();
        private List<string> SetList = new List<string>();
        private string info_FullTableName = "YNS{0}.YNS{0}.STK004";
        private string[] info_PrimaryKeys = { "pk_STK004_MalKodu" };
        private string[] info_IdentityKeys = { "STK004_Row_ID" };

        private List<string> ChangedProperties = new List<string>();
        public event PropertyChangedEventHandler PropertyChanged;

        public STK004()
        {
            ChangedProperties = new List<string>();
            this.PropertyChanged += STK004_PropertyChanged;
        }

        public void AcceptChanges()
        {
            ChangedProperties.Clear();
        }

        void STK004_PropertyChanged(object sender, PropertyChangedEventArgs e)
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
