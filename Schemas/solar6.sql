USE [master]
GO
/****** Object:  Database [SOLAR6]    Script Date: 11/04/2018 11:19:56 ******/
CREATE DATABASE [SOLAR6]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Solar6_Data', FILENAME = N'E:\GUNDATA\Solar6_Data.MDF' , SIZE = 133120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 5120KB )
 LOG ON 
( NAME = N'Solar6_Log', FILENAME = N'E:\GUNDATA\Solar6_Log.LDF' , SIZE = 92160KB , MAXSIZE = 2048GB , FILEGROWTH = 5120KB )
GO
ALTER DATABASE [SOLAR6] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SOLAR6].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SOLAR6] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SOLAR6] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SOLAR6] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SOLAR6] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SOLAR6] SET ARITHABORT OFF 
GO
ALTER DATABASE [SOLAR6] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SOLAR6] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SOLAR6] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SOLAR6] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SOLAR6] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SOLAR6] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SOLAR6] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SOLAR6] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SOLAR6] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SOLAR6] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SOLAR6] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SOLAR6] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SOLAR6] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SOLAR6] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SOLAR6] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SOLAR6] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SOLAR6] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SOLAR6] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SOLAR6] SET  MULTI_USER 
GO
ALTER DATABASE [SOLAR6] SET PAGE_VERIFY TORN_PAGE_DETECTION  
GO
ALTER DATABASE [SOLAR6] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SOLAR6] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SOLAR6] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SOLAR6] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SOLAR6] SET QUERY_STORE = ON
GO
ALTER DATABASE [SOLAR6] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [SOLAR6]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [SOLAR6]
GO
/****** Object:  User [MUHASEBE618]    Script Date: 11/04/2018 11:19:56 ******/
CREATE USER [MUHASEBE618] FOR LOGIN [MUHASEBE618] WITH DEFAULT_SCHEMA=[MUHASEBE618]
GO
/****** Object:  User [MUHASEBE617]    Script Date: 11/04/2018 11:19:56 ******/
CREATE USER [MUHASEBE617] FOR LOGIN [MUHASEBE617] WITH DEFAULT_SCHEMA=[MUHASEBE617]
GO
/****** Object:  User [FINSAT671]    Script Date: 11/04/2018 11:19:56 ******/
CREATE USER [FINSAT671] FOR LOGIN [FINSAT671] WITH DEFAULT_SCHEMA=[FINSAT671]
GO
/****** Object:  User [FINSAT617]    Script Date: 11/04/2018 11:19:56 ******/
CREATE USER [FINSAT617] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[FINSAT617]
GO
/****** Object:  Schema [FINSAT617]    Script Date: 11/04/2018 11:19:56 ******/
CREATE SCHEMA [FINSAT617]
GO
/****** Object:  Schema [FINSAT671]    Script Date: 11/04/2018 11:19:56 ******/
CREATE SCHEMA [FINSAT671]
GO
/****** Object:  Schema [MUHASEBE617]    Script Date: 11/04/2018 11:19:56 ******/
CREATE SCHEMA [MUHASEBE617]
GO
/****** Object:  Schema [MUHASEBE618]    Script Date: 11/04/2018 11:19:56 ******/
CREATE SCHEMA [MUHASEBE618]
GO
/****** Object:  View [dbo].[ALINAN_CEKLER_LISTESI]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ALINAN_CEKLER_LISTESI] AS SELECT Veren, EvrakNo as DefterKayitNo, BorcluUnvan1 as Unvan, AlimTarih as Tarih,AlimTarih as VadeTarih, Tutar, Nesne1 as IslemTip, veren as VerildigiYer, Cekno as AraToplamTarih, Kod1, Kod2, Kod3, Kod4, Kod5, Kod6, Kod7, Kod8, Kod9, Kod10, Sehir, Notlar,DovizCinsi, Kod13 as DovizKuru, Kod13 as DovizTutar, Degistiren as VAy, Degistiren as VYil, Nesne1 as ACKSonIslemTip,Kod11, Kod12, Kod13, Kod14,Kod1 as SciKod1,Kod2 as SciKod2,Kod3 as SciKod3,Kod4 as SciKod4,Kod5 as SciKod5, Kod6 as SciKod6,Kod7 as SciKod7,Kod8 as SciKod8,Kod9 as SciKod9,Kod10 as SciKod10,Kod11 as SciKod11, Kod12 as SciKod12,Kod13 as SciKod13,Kod14 as SciKod14,DovizCinsi as SciDovizCinsi, BankaHesapNo, BorcluUnvan1 as VerildigiYerUnvan, CekNo, Banka, Nesne1 as BankaAciklama, Sube, Nesne1 as SubeAciklama, Borclu as CekinBorclusu, BorcluUnvan1 as CekinBorclusuUnvan, Nesne1 as SehirAciklama, BankaIBAN, AlimTarih, Degistiren as AAy, Degistiren as AYil, Degistiren as VGun, CekNo as BaslangicTarih, CekNo as BitisTarih, CekTip as Toplam_Para_Birimi from FINSAT600.FINSAT600.ACK where 1 = 0 
GO
/****** Object:  View [dbo].[AMORTISMAN_LISTESI]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AMORTISMAN_LISTESI] AS SELECT Kod as DemirbasKodu, Adi as DemirbasAdi, TipKod as AmortismanTipi, A_Orani as AmortismanOrani,AlimTarih,AlimMiktar,AlimTutar,AlimTutar as DOKasaDegeri, AlimTutar as DOBirikmisAmortisman, AlimTutar as DOToplamBirikmisAmortisman,AlimTutar as DOKalanDeger,AlimTutar as DSKasaDegeri, AlimTutar as DSBirikmisAmortisman, AlimTutar as CariDonemAmortisman,AlimTutar as CariYilAmortisman, AlimTutar as DSToplamBirikmisAmortisman, AlimTutar as DSKalanDegeri, AlimTutar as AktifDegerEnfDuzFarki, AlimTutar as BirikmisAmortEnfDuzFarki, AlimTutar as DSYeniBirikmisAmortisman, AlimTarih as Yil, AlimSekli as Donem, Kod1 as AyAdi, IsyeriKodu, BolumKodu, TipKod, GrupKod, OzelKod, Aciklama as IsyeriAdi, Aciklama as BolumAdi, Aciklama as TipAdi, Aciklama as GrupAdi, Aciklama as OzelKodAdi, Aciklama, Degerle as SatirTuru, AlimTutar as MaliYilBasiAktifDeger, AlimTutar as MaliYilBasiBirikmisAmortisman, SabitKiymetHesabi as SabitKiymetMhsKod, Aciklama as SabitKiymetMhsAd from DEMIRBAS600.DEMIRBAS600.DMR where 1 = 0 
GO
/****** Object:  View [dbo].[BANKA_TALIMAT_FISI]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BANKA_TALIMAT_FISI] AS SELECT EvrakNo, Kod1 as Tarih, HesapKodu, DvzTL, KarsiHesapKodu, Aciklama, HataAciklama, Tutar, DovizCinsi, DovizKuru, DovizTutar, IBAN as CH_IBAN, ID, Aciklama as OdemeTuru, DosyaAd, Kod1 as TalimatDurum, CHGecti, SorguNo, Aciklama as CHIIslemTip, Kod1 as CHIBA, CHIEvrakNo, Kod1 as CHITarih, CHIKynkEvrakTip, Kod1, Kod2, Kod3, Kod4, Kod5, Kod6, Kod7, Kod8,Kod9, Kod10, Kod11, Kod12, Kod13, Kod14, Kaydeden, KayitTarih, Aciklama as HesapAdi, Aciklama as KarsiHesapAdi, Aciklama as BankaIBAN1, Aciklama as BankaHesap1, Aciklama as BankaAdi, Aciklama as BankaSubeAdi, Aciklama as CH_Banka_Adi, Aciklama as CH_BankaSube_Adi, Aciklama as CH_Banka_HesapNo, Tarih as TarihNmr from FINSAT600.FINSAT600.BTI where 1 = 0 
GO
/****** Object:  View [dbo].[DVZ_AYLIK_ANALIZ]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DVZ_AYLIK_ANALIZ] AS SELECT MUHASEBE600.MUHASEBE600.MHI.HesapKod, HesapAd as HesapAdi, MUHASEBE600.MUHASEBE600.MHI.Kod1 as MHKKod1, MUHASEBE600.MUHASEBE600.MHI.Kod2 as MHKKod2, MUHASEBE600.MUHASEBE600.MHI.Kod3 as MHKKod3, Tutar as OcakBakiye ,Tutar as SubatBakiye ,Tutar as MartBakiye ,Tutar as NisanBakiye ,Tutar as MayisBakiye ,Tutar as HaziranBakiye,Tutar as TemmuzBakiye,Tutar as AgustosBakiye,Tutar as EylulBakiye,Tutar as EkimBakiye,Tutar as KasimBakiye,Tutar as AralikBakiye ,Tutar as ToplamBakiye, MUHASEBE600.MUHASEBE600.MHI.DovizCinsi as EDovizC, Tarih as BaslangicTarih, Tarih as BitisTarih  FROM MUHASEBE600.MUHASEBE600.MHI, MUHASEBE600.MUHASEBE600.MHK where 1 = 0 
GO
/****** Object:  View [dbo].[DVZ_GUNLUK_MIZAN]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DVZ_GUNLUK_MIZAN] AS SELECT HesapKod, HesapAd, KartTip,HesapKod2,HesapAd2,Borc1 AS Borc, Alacak1 AS Alacak,girmiktar1 as GirisMiktar,cikmiktar1 as CikisMiktar, dvzBorc1 AS DovizBorc, dvzalacak1 AS DovizAlacak, borc1 as Borcbakiye ,Borc1 as AlacakBakiye,Girmiktar1 as GirisMiktarBakiye,Cikmiktar1 as CikisMiktarBakiye,borc1 as DovizBorcBakiye,Borc1 as DovizAlacakBakiye,DovizCinsi, Birim, CikMiktar1 as SatirTuru, DovizCinsi as EDovizCinsi, Kod1 as MHKKod1, Kod2 as MHKKod2, Kod3 as MHKKod3, Kod4 as MHKKod4, Kod5 as MHKKod5, SonBorcTarih as BaslangicTarih, SonBorcTarih as BitisTarih, Bakiyetip as AzamiHesapBoyu FROM MUHASEBE600.MUHASEBE600.MHK where 1 = 0 
GO
/****** Object:  View [dbo].[DVZ_GUNLUK_MIZAN_TOPLAM]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DVZ_GUNLUK_MIZAN_TOPLAM] AS select borc1 as BorcBakiye, borc1 as AlacakBakiye ,borc1 as Borc,borc1 as Alacak,girmiktar1 as GirisMiktarBakiye,cikmiktar1 as CikisMiktarBakiye,borc1 as DovizBorcBakiye,borc1 as DovizAlacakBakiye,borc1 as DovizBorc,borc1 as DovizAlacak,girmiktar1 as GirisMiktar,CikMiktar1 as CikisMiktar  FROM MUHASEBE600.MUHASEBE600.MHK where 1 = 0 
GO
/****** Object:  View [dbo].[DVZ_MUAVIN_DEFTER]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DVZ_MUAVIN_DEFTER] AS Select MUHASEBE600.MUHASEBE600.MHI.Hesapkod, MUHASEBE600.MUHASEBE600.MHK.HesapAd,Hesapkod2,Hesapad2,Karttip, Tarih, FisNo, FisTip,girmiktar1 as  GirisMiktar,cikmiktar1 as CikisMiktar,MaddeNo, Aciklama, Aciklama2,MUHASEBE600.MUHASEBE600.MHI.DovizCinsi, DovizKuru, FisKod, KebirKod, MUHASEBE600.MUHASEBE600.MHI.Kod1, MUHASEBE600.MUHASEBE600.MHI.Kod2, MUHASEBE600.MUHASEBE600.MHI.Kod3, MUHASEBE600.MUHASEBE600.MHI.MasrafMerkez, Referans, EvrakNo, Tutar AS Borc, Tutar AS Alacak,Tutar AS DovizBorc, Tutar AS DovizAlacak, Tutar, BA, SiraNo,MUHASEBE600.MUHASEBE600.MHK.Kod1 as MhkKod1,MUHASEBE600.MUHASEBE600.MHK.Kod2 AS MhkKod2,MUHASEBE600.MUHASEBE600.MHK.Kod3  as MhkKod3,MUHASEBE600.MUHASEBE600.MHI.Birim,MUHASEBE600.MUHASEBE600.MHI.ButceKod,MUHASEBE600.MUHASEBE600.MMK.HesapAd as MasrafMerkezAdi,MUHASEBE600.MUHASEBE600.MHI.DovizCinsi as EDovizC,MUHASEBE600.MUHASEBE600.MHK.HesapAd as DHGormeyenHListelensinMi,MUHASEBE600.MUHASEBE600.MHI.Kod4 ,MUHASEBE600.MUHASEBE600.MHI.Kod5,MUHASEBE600.MUHASEBE600.MHK.Kod6,MUHASEBE600.MUHASEBE600.MHI.Kod7,MUHASEBE600.MUHASEBE600.MHI.Kod8,MUHASEBE600.MUHASEBE600.MHI.Kod1 as BaslangicTarih, MUHASEBE600.MUHASEBE600.MHI.Kod1 as BitisTarih, Tarih2 , MUHASEBE600.MUHASEBE600.MHI.Kod1 as OrtakOzellik from MUHASEBE600.MUHASEBE600.MHI, MUHASEBE600.MUHASEBE600.MHK, MUHASEBE600.MUHASEBE600.MMK where 1 = 0 
GO
/****** Object:  View [dbo].[DVZ_MUAVIN_DEFTER_DEVIR]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DVZ_MUAVIN_DEFTER_DEVIR] AS SELECT HesapKod, Tutar AS Borc, Tutar AS Alacak, Tutar AS DovizBorc, Tutar AS DovizAlacak,Miktar as GirisMiktar,Miktar as CikisMiktar, MUHASEBE600.MUHASEBE600.MHI.Kod1 as OrtakOzellik FROM MUHASEBE600.MUHASEBE600.MHI where 1 = 0 
GO
/****** Object:  View [dbo].[DVZ_STK_EKSTRE]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DVZ_STK_EKSTRE] AS SELECT Nesne1 AS idt,SeriNo,Vasita,KynkEvrakTip,SiraNo,KayitTarih,Islemtur,IrsFat,Depo,tutar as NetTutar,Tutar as KDV, Iskonto as IskontoTutar,IskontoOran,Nesne1 as STKOzelKod,Nesne1 as STKTipKod, Nesne1 as STKGrupKod,Nesne1 as STKKod1,Nesne1 as STKKod2, Nesne1 as STKKod3,Malkodu, Nesne1 as MalAdi,CHK as CHKHesapkodu, EvrakNo as Birim1,Tarih,EvrakNo, Kod1, Kod2, Kod3, KDVOran as IslemTip, Miktar as GirisMiktar,Tutar  as GirisBirimFiyat,Tutar as GirisTutar ,Miktar as CikisMiktar,Tutar as CikisBirimFiyat, Tutar as CikisTutar,CHK as SatirTuru,Islemtip AS grup,Kod4, Kod5, Kod6,miktar as GirisMiktarToplam, Miktar as GirisTutarToplam,miktar as CikisMiktarToplam,Miktar as CikisTutarToplam,Tutar as NetTutarToplam,Tutar as KDVToplam, Tutar as IskontoToplami,KdvOran,Aciklama,  Nesne1 as STKKod4, DovizKuru as STKKod5, DovizKuru as STKKod6, SeriNo as STKKod7,  SeriNo as STKKod8, SeriNo as STKKod9,Kayitturu as STKKod10,Kayitturu as STKKod11, Dovizkuru as STKKod12, Dovizkuru as STKKod13, SeriNo as BarKod1,SeriNo as BarKod2,SeriNo as BarKod3,SeriNo as TeminYeri, TeslimCHK, TesTemMalKod, vadetarih,MasrafMerkezi, Masraf as GirisMasraf,Masraf as CikisMasraf, DovizCinsi,DovizKuru, DovizTutar as DvzGirisTutar,DovizTutar as DvzCikisTutar,Tutar as DvzGirisTutarTop, Tutar as DvzCikisTutarTop,Masraf as MasrafGToplam,Masraf as MasrafCToplam, DovizTutar as DvzGirisBirimFiyat,DovizTutar as DvzCikisBirimFiyat, Masraf as DvzGirisMasraf,Masraf as DvzCikisMasraf, Tutar as GirisNetTutar, Tutar as CikisNetTutar, DovizTutar as DvzGirisNetTutar, DovizTutar as DvzCikisNetTutar, Masraf as DvzMasrafGToplam,Masraf as DvzMasrafCToplam , Tutar as GirisNetToplam, Tutar as CikisNetToplam, Tutar as DvzGirisNetToplam, Tutar as DvzCikisNetToplam,OTVTutar,Tutar as DvzOTVTutar, Kod14, DovizCinsi as EDovizCinsi, Nesne1 as Unvan,  Tutar  as GirisNetBirimFiyat, Tutar as CikisNetBirimFiyat, Tutar as DvzGirisNetBirimFiyat, Tutar  as DvzCikisNetBirimFiyat, KaynakIIfEvrakno,  IslemTip as Toplam_Para_Birimi from FINSAT600.FINSAT600.STI where 1 = 0 
GO
/****** Object:  View [dbo].[DVZ_STK_EKSTRETOPLAM]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DVZ_STK_EKSTRETOPLAM] AS SELECT Miktar as GirisMiktar,Tutar as GirisTutar,Miktar as CikisMiktar,Tutar as CikisTutar,Tutar as NetTutarToplam,Tutar as KDVToplam,Tutar as IskontoToplam,Tutar as DvzGirisTutar,Tutar as DvzCikisTutar, masraf as GirisMasraf,masraf as CikisMasraf,masraf as DvzGirisMasraf,masraf as DvzCikisMasraf, DovizTutar as DvzGirisNetTutar, DovizTutar as DvzCikisNetTutar,OTVTutar from FINSAT600.FINSAT600.STI where 1 = 0 
GO
/****** Object:  View [dbo].[FATURA_LISTESI]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FATURA_LISTESI] AS SELECT Hesapkodu, Nesne1 as Unvan, Evrakno, Tarih, VadeTarih, Tutar, Tutar as Iskonto1,Tutar as Iskonto2,Tutar as Iskonto3,Tutar as NetTutar,Tutar as KDVTutar1, Tutar as KDVTutar2,Tutar as KDVTutar3,Tutar as FaturaTutari, DovizCinsi, DovizKuru,DovizTutar,DovizTutar as DovizIskonto1,DovizTutar as DovizIskonto2,DovizTutar as DovizIskonto3,DovizTutar as DovizNetTutar,DovizTutar as DovizKDVTutar1,DovizTutar as DovizKDVTutar2,DovizTutar as DovizKDVTutar3,DovizTutar as DovizFaturaTutar, IskontoTur as IskontoTur1, IskontoTur as IskontoTur2, IskontoTur as IskontoTur3, KDVOran as KDVOran1, KDVOran as KDVOran2, KDVOran as KDVOran3,KarsiEvrakNo as ListeTuru, Tarih as BaslangicTarih, Tarih as BitisTarih,Tutar as OTVTutar, Tutar as KDVMatrah,Aciklama, Kod1 as BolgeKod, Kod1 as OzelKod, Kod8 as GrupKod, Kod1 as TipKod, Kod1, Kod2, Kod3, Kod4, Kod13 as Kod5, Kod13 as Kod6, Kod8 as Kod7, Kod8, Kod9, Kod11 as Kod10, Kod11, Kod13 as Kod12, Kod13, Tutar as KDVTevkifati, Tutar as KDVToplami, Kod1 as KDVTevkifatOrani,Kod1 as HareketKod1, Kod2 as HareketKod2, Kod3 as HareketKod3, Kod4 as HareketKod4, TeslimCHK, Nesne1 as TeslimUnvan, DovizTutar as DovizKDVTevkifati, DovizTutar as DovizKDVToplami, DovizTutar as DovizKDVMatrahi, Tutar as BelgeSayisi, IslemTip as ListeSekli, Tutar as GelirVergisi, Tutar as FonPayi, Nesne1 as VergiDairesi, KayitSurum as VergiHesapNo, Kod1 as FisTarihi, BordroNo as MaddeNo, Tutar as OIVTutar, IskontoTur as IskontoTur4, IskontoTur as IskontoTur5, IskontoTur as IskontoTur6, Tutar as Iskonto4,Tutar as Iskonto5,Tutar as Iskonto6, DovizTutar as DovizIskonto4,DovizTutar as DovizIskonto5,DovizTutar as DovizIskonto6,Kod5 as HareketKod5, Kod6 as HareketKod6, Kod7 as HareketKod7, Kod8 as HareketKod8, Kod9 as HareketKod9, Kod10 as HareketKod10, EvrakTarih, Tutar as OzelMatrahKDVTutar , Tutar as KDVMatrah2, Tutar as KDVMatrah3, KDVOran as KDVOran4, Tutar as KDVTutar4 , DovizTutar as DovizKDVTutar4, Tutar as KDVMatrah4, Nesne1 as KaynakEvrakTip , DovizTutar as DovizKDVMatrahi2, DovizTutar as DovizKDVMatrahi3, DovizTutar as DovizKDVMatrahi4 , Hesapkodu as EFatTip, Hesapkodu as EFatDurum, Kaydeden, Tutar as KDVTevkifati2, Tutar as KDVTevkifati3, Tutar as KDVTevkifati4 , Kod1 as KDVTevkifatOrani2, Kod1 as KDVTevkifatOrani3, Kod1 as KDVTevkifatOrani4 , DovizTutar As DovizKDVTevkifati2, DovizTutar As DovizKDVTevkifati3, DovizTutar As DovizKDVTevkifati4,  Tutar as OTVTevkifati, Tutar as Stopaj,  DovizTutar as DovizOTVTevkifati, DovizTutar as DovizOTVTutar, IslemTip as Toplam_Para_Birimi, Tutar as TevkifatTutar, Kod1 as TevkifatOran, Tutar2, Aciklama as CHIAciklama, Aciklama2 as CHIAciklama2, Aciklama as STIAciklama, Aciklama2 as STIAciklama2, MasrafMerkezi as CHIMasrafMerkezi, MasrafMerkezi as STIMasrafMerkezi, Kod1 as KayitTarih, DovizTutar as DovizTevkifatTutar, Hesapkodu as EArsivFaturaDurum, Hesapkodu as EArsivFaturaTipi, Hesapkodu as EArsivTeslimSekli, Tutar as MeraFonu, Kod1 as BelgeTarihi, AsilEvrakNo as BelgeNumarasi, Kod1 as Depo, Nesne1 as AdiSoyadi, TCKN, IBAN, DovizCinsi as BankaKod, ProjeKodu as BankaAdi, BordroNo as FisNo, Nesne1 as HareketKod1Aciklama, ProjeKodu, Nesne1 as ProjeKoduAciklama,Tutar as Navlun, Tutar as Sigorta, Tutar as IhracatMasraf, DovizTutar as DovizNavlun, DovizTutar as DovizSigorta, DovizTutar as DovizIhracatMasraf from FINSAT600.FINSAT600.CHI where 1 = 0
GO
/****** Object:  View [dbo].[MUSTERI_BAZINDA_SIPARIS]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[MUSTERI_BAZINDA_SIPARIS] AS select Chk, Aciklama as Unvan, Malkodu,Nesne1 as MalAdi,Evrakno as SiparisNo, Tarih as SiparisTarih,Aciklama, Aciklama as Aciklama2, BirimMiktar,Miktar as TeslimMiktar,Miktar as KalanMiktar,miktar as GirisMiktar,miktar as CikisMiktar, miktar as StokMevcudu, BirimFiyat, Tutar as BirimTutar, IslemTip, ToplamIskonto as Iskonto, Kdv, Tutar as NetTutar,TeslimCHK as TeslimYeriKodu,Aciklama as SiparisTuru, Depo, Tarih as TahminiTeslimTarihi, Tarih as SonTeslimTarihi, Kod1 as SPIKod1, Kod2 as SPIKod2, Kod3 as SPIKod3, Kod4 as SPIKod4, Kod5 as SPIKod5, Kod6 as SPIKod6, Kod7 as SPIKod7, Kod8 as SPIKod8, Kod9 as SPIKod9, Kod10 as SPIKod10, Kod11 as SPIKod11, Kod12 as SPIKod12, Kod13 as SPIKod13, Kod14 as SPIKod14, MasrafMerkezi as SPIMasrafMerkezi, Miktar as KapatilanMiktar, RenkBedenKod1 as AltUrunKodu1, RenkBedenKod2 as AltUrunKodu2, RenkBedenKod3 as AltUrunKodu3, RenkBedenKod4 as AltUrunKodu4, DovizTutar, DvzBirimFiyat, DovizCinsi, Tutar as SiparisTutari, Tutar as FaturalasacakTutar, DvzTl as YerelParaDoviz, DovizTutar as DvzBirimTutar, DovizTutar as DvzKdv, DovizKuru as KartDvzKur, DovizTutar as DvzSiparisTutari, DovizTutar as DvzFaturalasacakTutar, DovizTutar as DvzNetTutar, DovizTutar as DvzIskonto, Kod1 as Tarih2, IslemTip as Toplam_Para_Birimi From FINSAT600.FINSAT600.STI where 1 = 0 
GO
/****** Object:  View [dbo].[ODM_GENEL_MIZAN]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ODM_GENEL_MIZAN] AS SELECT  KartTip, HesapKod, HesapAd,HesapKod2,HesapAd2,Birim,Borc1 AS Borc, Alacak1 AS Alacak, Borc1 AS DovizBorc, Alacak1 AS DovizAlacak, Girmiktar1 as GirisMiktar ,cikmiktar1 as CikisMiktar ,Borc1 as BorcBakiye,borc1 as AlacakBakiye,Borc1 as DovizBorcBakiye,Borc1 as DovizAlacakBakiye,girmiktar1 as GirisMiktarBakiye,girmiktar1 as CikisMiktarBakiye,DovizCinsi, HesapAd as MizanAy, HesapAd as MizanRaporTur, Bakiyetip as AzamiHesapBoyu, DovizCinsi as MizanAlinacakAy FROM MUHASEBE600.MUHASEBE600.MHK where 1 = 0 
GO
/****** Object:  View [dbo].[ODM_GENEL_MIZANTOPLAM]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ODM_GENEL_MIZANTOPLAM] AS select borc1 as Baglanti,borc1 as BorcBakiye, borc1 as AlacakBakiye ,borc1 as DovizBorcBakiye,borc1 as DovizAlacakBakiye, girmiktar1 as GirisMiktarBakiye ,girmiktar1 as CikisMiktarBakiye,girmiktar1 as GirisMiktar,girmiktar1 as CikisMiktar,borc1 as Borc,borc1 as Alacak,borc1 as DovizBorc,borc1 as DovizAlacak FROM MUHASEBE600.MUHASEBE600.MHK where 1 = 0 
GO
/****** Object:  View [dbo].[STOK_ENVANTER]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[STOK_ENVANTER] AS SELECT Malkodu, Maladi,Birim1 as Birim,GirMiktar as GirisMiktar, GirMiktar as CikisMiktar,GirMiktar as KalanMiktar,SonMlyBirimFiyat as Son_Maliyet_Birim_Fiyati, DvrTutar as Tutar,SonMlySekli as MaliyetHesapYontemi, GrupKod,AlisFiyat1,AlisFiyat2,AlisFiyat3,SatisFiyat1,SatisFiyat2,SatisFiyat3,TipKod,OzelKod,DvrTutar as GirisTutar, DvrTutar as CikisTutar, DVRTutar as KalanTutar, DvrTutar as GirisNetTutar, DvrTutar as CikisNetTutar, DvrTutar as KalanNetTutar,GirMiktar as DevirMiktar, DvrTutar as DevirTutar,Girrezervasyon as RezervasyonGirisM,Cikrezervasyon as RezervasyonCikisM, DvrTarih as BaslangicTarih, DvrTarih as BitisTarih, Kod1, Kod2, Kod3, Kod4, Kod5, Kod6, Kod7, Kod8, Kod9, Kod10, Kod11, Kod12, Kod13, Notlar, KritikStok, AzamiStok, SatisSiparis, AlimSiparis, DvrTutar as GirisMasrafTutar, SonAlimBF, MasrafMerkezi, Maladi as MasrafMerkeziAdi, DvrMiktar as SayimFarkiMiktari, DvrTutar as SayimFarkiTutari, DvrTutar as GirisOTVTutar, DvrTutar as CikisOTVTutar, DvrTutar as GirisMasrafTutar1, DvrTutar as GirisMasrafTutar2, GirMiktar as Birim2GirisMiktar, GirMiktar as Birim2CikisMiktar,GirMiktar as Birim2KalanMiktar, GirMiktar as Birim2DevirMiktar,Kod1 as BaslangicDepo, Kod1 as BitisDepo, Kod10 as Toplam_Para_Birimi from FINSAT600.FINSAT600.STK where 1 = 0
GO
/****** Object:  View [dbo].[STOK_FATURA_LISTESI]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[STOK_FATURA_LISTESI] AS select Malkodu, Nesne1 as Maladi, chk as HesapKodu, Nesne1 as Unvan, EvrakNo, Tarih, Tutar, Iskonto, Tutar as NetTutar, KDV, Tutar as FaturaTutar, IslemTur as ListeTuru, Miktar, Fiyat as BirimFiyat, Kod1, Kod2, Kod3, Kod4, Kod5, Kod6, Kod7, Kod8, Kod9, Kod10, EvrakTarih, OTVTutar, KDV as KDV2, KDV as KDV3, KDV as KDV4, DovizTutar as DovizKDV, DovizTutar as DovizKDV2, DovizTutar as DovizKDV3, DovizTutar as DovizKDV4, Tutar as KDVMatrah, Tutar as KDVMatrah2, Tutar as KDVMatrah3, Tutar as KDVMatrah4, DovizTutar as DovizKDVMatrah, DovizTutar as DovizKDVMatrah2, DovizTutar as DovizKDVMatrah3, DovizTutar as DovizKDVMatrah4, Malkodu as TevkifatOran, TevfikatTutar as TevkifatTutar, Malkodu as EFatTip, Malkodu as EFatDurum, Nesne1 as VergiDairesi, Kod1 as HesapNo,  Tutar as OTVTevkifati, StopajTutar, Aciklama, Aciklama2, Malkodu as EArsivFaturaDurum, Malkodu as EArsivFaturaTipi, Malkodu as EArsivTeslimSekli, StopajTutar as MeraFonu, Depo, MhsKod as FaturaAdres1, MhsKod as FaturaAdres2, MhsKod as FaturaAdres3, Nesne1 as FaturaAdresi, Tutar as Sigorta, Tutar as Navlun, Tutar as IhracatMasraf,Vasita as BaslangicTarih, Vasita as BitisTarih, IslemTip as Toplam_Para_Birimi From FINSAT600.FINSAT600. STI where 1 = 0 
GO
/****** Object:  View [dbo].[VERILEN_CEKLER_LISTESI]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VERILEN_CEKLER_LISTESI] AS SELECT Alacakli as VerildigiYer, EvrakNo as DefterKayitNo, Nesne1 as Unvan, VadeTarih as Tarih,VadeTarih, Tutar, Nesne1 as IslemTip, Cekno as AraToplamTarih, Kod1, Kod2, Kod3, Kod4, Kod5, Kod6, Kod7, Kod8, Kod9, Kod10, Sehir, Notlar,DovizCinsi, Kod13 as DovizKuru, Kod13 as DovizTutar, Degistiren as VAy, Degistiren as VYil, Banka, Sube, Vadetarih as SonPozisyonTarih,CekNo as BaslangicTarih, CekNo as BitisTarih, Nesne1 as VCKSonIslemTip,Kod11, Kod12, Kod13, Kod14,Kod1 as SciKod1,Kod2 as SciKod2,Kod3 as SciKod3,Kod4 as SciKod4,Kod5 as SciKod5, Kod6 as SciKod6,Kod7 as SciKod7,Kod8 as SciKod8,Kod9 as SciKod9,Kod10 as SciKod10,Kod11 as SciKod11, Kod12 as SciKod12,Kod13 as SciKod13,Kod14 as SciKod14,DovizCinsi as SciDovizCinsi, BankaHesapNo, BankaCHK, Nesne1 as BankaAciklama, Nesne1 as SehirAciklama, Nesne1 as SubeAciklama, BankaIBAN, CekNo, CekTip as Toplam_Para_Birimi from FINSAT600.FINSAT600.VCK where 1 = 0 
GO
/****** Object:  Table [dbo].[APPNAMES]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APPNAMES](
	[ID] [int] NOT NULL,
	[KOD] [varchar](25) NULL,
	[APP_PATTERN] [varchar](50) NULL,
	[ACIKLAMA] [varchar](50) NULL,
	[INSTALLATION_PATH] [varchar](500) NULL,
 CONSTRAINT [pkAPPNAMES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 11/04/2018 11:19:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Applications](
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [aspnet_Applications_Index]    Script Date: 11/04/2018 11:19:57 ******/
CREATE CLUSTERED INDEX [aspnet_Applications_Index] ON [dbo].[aspnet_Applications]
(
	[LoweredApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Membership](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [ntext] NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [aspnet_Membership_index]    Script Date: 11/04/2018 11:19:57 ******/
CREATE CLUSTERED INDEX [aspnet_Membership_index] ON [dbo].[aspnet_Membership]
(
	[ApplicationId] ASC,
	[LoweredEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Paths](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NOT NULL,
	[Path] [nvarchar](256) NOT NULL,
	[LoweredPath] [nvarchar](256) NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [aspnet_Paths_index]    Script Date: 11/04/2018 11:19:57 ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_Paths_index] ON [dbo].[aspnet_Paths]
(
	[ApplicationId] ASC,
	[LoweredPath] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationAllUsers](
	[PathId] [uniqueidentifier] NOT NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_PersonalizationPerUser](
	[Id] [uniqueidentifier] NOT NULL,
	[PathId] [uniqueidentifier] NULL,
	[UserId] [uniqueidentifier] NULL,
	[PageSettings] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [aspnet_PersonalizationPerUser_index1]    Script Date: 11/04/2018 11:19:57 ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_PersonalizationPerUser_index1] ON [dbo].[aspnet_PersonalizationPerUser]
(
	[PathId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Profile](
	[UserId] [uniqueidentifier] NOT NULL,
	[PropertyNames] [ntext] NOT NULL,
	[PropertyValuesString] [ntext] NOT NULL,
	[PropertyValuesBinary] [image] NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Roles](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [aspnet_Roles_index1]    Script Date: 11/04/2018 11:19:57 ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_Roles_index1] ON [dbo].[aspnet_Roles]
(
	[ApplicationId] ASC,
	[LoweredRoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_SchemaVersions](
	[Feature] [nvarchar](128) NOT NULL,
	[CompatibleSchemaVersion] [nvarchar](128) NOT NULL,
	[IsCurrentVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Feature] ASC,
	[CompatibleSchemaVersion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_Users](
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[MobileAlias] [nvarchar](16) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [aspnet_Users_Index]    Script Date: 11/04/2018 11:19:57 ******/
CREATE UNIQUE CLUSTERED INDEX [aspnet_Users_Index] ON [dbo].[aspnet_Users]
(
	[ApplicationId] ASC,
	[LoweredUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_UsersInRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[aspnet_WebEvent_Events](
	[EventId] [char](32) NOT NULL,
	[EventTimeUtc] [datetime] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[EventType] [nvarchar](256) NOT NULL,
	[EventSequence] [decimal](19, 0) NOT NULL,
	[EventOccurrence] [decimal](19, 0) NOT NULL,
	[EventCode] [int] NOT NULL,
	[EventDetailCode] [int] NOT NULL,
	[Message] [nvarchar](1024) NULL,
	[ApplicationPath] [nvarchar](256) NULL,
	[ApplicationVirtualPath] [nvarchar](256) NULL,
	[MachineName] [nvarchar](256) NOT NULL,
	[RequestUrl] [nvarchar](1024) NULL,
	[ExceptionType] [nvarchar](256) NULL,
	[Details] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthorizationRules]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorizationRules](
	[RULEID] [int] IDENTITY(1,1) NOT NULL,
	[RULESOURCE] [int] NOT NULL,
	[RULETYPE] [nchar](1) NOT NULL,
	[ROOT] [nvarchar](64) NOT NULL,
	[ENTITY] [nvarchar](64) NOT NULL,
	[MEMBER] [nvarchar](128) NULL,
	[EXPRESSION] [ntext] NOT NULL,
 CONSTRAINT [PK_AuthorizationRules] PRIMARY KEY CLUSTERED 
(
	[RULEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[B2B_User]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[B2B_User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[Parola] [varchar](40) NOT NULL,
	[YetkiliEMail] [varchar](50) NOT NULL,
 CONSTRAINT [PK_B2B_User_1] PRIMARY KEY CLUSTERED 
(
	[HesapKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BilFisAkt_Users]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BilFisAkt_Users](
	[KullaniciAdi] [nvarchar](5) NOT NULL,
	[Sifre] [nvarchar](50) NULL,
	[Grup] [nvarchar](5) NULL,
	[AdiSoyadi] [nvarchar](50) NULL,
 CONSTRAINT [PK_BilFisAkt_Users] PRIMARY KEY CLUSTERED 
(
	[KullaniciAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BTT]    Script Date: 11/04/2018 11:19:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BTT](
	[SirDKod] [varchar](2) NOT NULL,
	[Kullanici] [varchar](5) NOT NULL,
	[MsgID] [int] NOT NULL,
	[TanimAdi] [varchar](248) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[Tanim] [varchar](248) NOT NULL,
	[FiltreAdi] [varchar](248) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkBTT] PRIMARY KEY CLUSTERED 
(
	[SirDKod] ASC,
	[Kullanici] ASC,
	[MsgID] ASC,
	[TanimAdi] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BX_AdminPaneli]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BX_AdminPaneli](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciAdi] [varchar](5) NOT NULL,
	[MuhasebeAktarim] [bit] NULL,
	[CariKartEkleme] [bit] NULL,
	[MuhasebeKartiEkleme] [bit] NULL,
	[KartOnaylama] [bit] NULL,
	[OdemeEmriGirisi] [bit] NULL,
	[OdemeEmriOnaylama] [bit] NULL,
	[GuvenlikKodlari] [nvarchar](150) NULL,
	[OdemeEslemeKapatma] [bit] NULL,
	[OdemeEslemeAcma] [bit] NULL,
	[SilinenKayitlar] [bit] NULL,
	[GuncellenenKayitlar] [bit] NULL,
 CONSTRAINT [PK_BX_AdminPaneli] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[KullaniciAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COLUMN_NAME]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COLUMN_NAME](
	[ID] [int] NOT NULL,
	[CLMTABLEID] [int] NULL,
	[CLMNAME] [varchar](64) NOT NULL,
	[CLMALIAS] [varchar](100) NULL,
	[CLMALIAS_LNG1] [varchar](100) NULL,
	[CLMALIAS_LNG2] [varchar](100) NULL,
	[CLMVISIBLE] [smallint] NOT NULL,
	[CLMDATATYPE] [int] NULL,
	[CLMDATALENGTH] [int] NULL,
	[CLMDATAPRECISION] [int] NULL,
	[CLMDATASCALE] [int] NULL,
	[CLMALIGNMENT] [int] NOT NULL,
	[CLMFORMAT] [varchar](64) NULL,
	[CLMF9] [smallint] NOT NULL,
	[CLMF10] [smallint] NOT NULL,
	[CLMREF_TABLECLMID] [int] NULL,
	[CLMREF_TYPECLMID] [int] NULL,
	[CLMREF_TYPEVALUE] [int] NULL,
	[CLMALIASUSERDEFINED] [smallint] NOT NULL,
	[CLMCBOID] [int] NULL,
	[CLMREF_DISPCLMID] [int] NULL,
	[CLMDEFAULTVALUE] [varchar](100) NULL,
	[CLMREF_TABLEDATABASE] [varchar](100) NULL,
	[CLMCAPTION] [varchar](100) NULL,
	[CLMCAPTION_LNG1] [varchar](100) NULL,
	[CLMCAPTION_LNG2] [varchar](100) NULL,
	[CLMALLOWFILTER] [smallint] NULL,
	[CLMREF_FILTER] [varchar](255) NOT NULL,
	[CLMREF_DISPOTHER] [varchar](100) NULL,
	[CLMTYPE] [smallint] NOT NULL,
	[CLMSCRIPT] [varchar](100) NULL,
	[CLMINNERFIELDS] [varchar](100) NULL,
	[CLMREF_SEARCHCLMID] [int] NULL,
	[CLMREF_CHECKINTEGRITY] [smallint] NOT NULL,
 CONSTRAINT [pkCOLUMN_NAME] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMBO_NAME]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMBO_NAME](
	[ID] [int] NOT NULL,
	[CBOTYPECOLUMNVALUE] [varchar](100) NOT NULL,
	[CBOTYPECOLUMNID] [int] NOT NULL,
	[CBONAME] [varchar](100) NOT NULL,
	[CBONAME_LNG1] [varchar](100) NULL,
	[CBONAME_LNG2] [varchar](100) NULL,
	[CBOVISIBLE] [smallint] NOT NULL,
 CONSTRAINT [pkCOMBO_NAME] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[CBOTYPECOLUMNVALUE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMBOITEM_NAME]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMBOITEM_NAME](
	[ITEMCBOID] [int] NOT NULL,
	[ID] [int] NOT NULL,
	[ITEMNAME] [varchar](100) NOT NULL,
	[ITEMNAME_LNG1] [varchar](100) NULL,
	[ITEMNAME_LNG2] [varchar](100) NULL,
	[ITEMDATA] [int] NULL,
	[ITEMVISIBLE] [smallint] NOT NULL,
 CONSTRAINT [pkCOMBOITEM_NAME] PRIMARY KEY CLUSTERED 
(
	[ITEMCBOID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DRV]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DRV](
	[Kod] [varchar](2) NOT NULL,
	[DriverName] [varchar](60) NOT NULL,
	[Entry0] [varchar](40) NOT NULL,
	[Value0] [varchar](40) NOT NULL,
	[Entry1] [varchar](40) NOT NULL,
	[Value1] [varchar](40) NOT NULL,
	[Entry2] [varchar](40) NOT NULL,
	[Value2] [varchar](40) NOT NULL,
	[Entry3] [varchar](40) NOT NULL,
	[Value3] [varchar](40) NOT NULL,
	[Entry4] [varchar](40) NOT NULL,
	[Value4] [varchar](40) NOT NULL,
	[Entry5] [varchar](40) NOT NULL,
	[Value5] [varchar](40) NOT NULL,
	[Entry6] [varchar](40) NOT NULL,
	[Value6] [varchar](40) NOT NULL,
	[Entry7] [varchar](40) NOT NULL,
	[Value7] [varchar](40) NOT NULL,
	[Entry8] [varchar](40) NOT NULL,
	[Value8] [varchar](40) NOT NULL,
	[Entry9] [varchar](40) NOT NULL,
	[Value9] [varchar](40) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkDRV] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DTK]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DTK](
	[AppID] [smallint] NOT NULL,
	[SirDKod] [varchar](2) NOT NULL,
	[Kullanici] [varchar](5) NOT NULL,
	[MySection] [varchar](248) NOT NULL,
	[MyEntry] [varchar](248) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[MyValue] [varchar](248) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkDTK] PRIMARY KEY CLUSTERED 
(
	[AppID] ASC,
	[SirDKod] ASC,
	[Kullanici] ASC,
	[MySection] ASC,
	[MyEntry] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DVZ]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DVZ](
	[DovizCinsi] [varchar](3) NOT NULL,
	[Tarih] [int] NOT NULL,
	[Kur00] [numeric](25, 6) NOT NULL,
	[Kur01] [numeric](25, 6) NOT NULL,
	[Kur02] [numeric](25, 6) NOT NULL,
	[Kur03] [numeric](25, 6) NOT NULL,
	[Kur04] [numeric](25, 6) NOT NULL,
	[Kur05] [numeric](25, 6) NOT NULL,
	[Kur06] [numeric](25, 6) NOT NULL,
	[Kur07] [numeric](25, 6) NOT NULL,
	[Kur08] [numeric](25, 6) NOT NULL,
	[Kur09] [numeric](25, 6) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkDVZ] PRIMARY KEY CLUSTERED 
(
	[Tarih] ASC,
	[DovizCinsi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EAO]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EAO](
	[Tarih] [int] NOT NULL,
	[Oran] [numeric](25, 6) NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEAO] PRIMARY KEY CLUSTERED 
(
	[Tarih] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EAT]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EAT](
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](2) NOT NULL,
	[Arsiv] [image] NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEAT] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EBF]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EBF](
	[YilAy] [int] NOT NULL,
	[Oran] [numeric](25, 6) NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEBF] PRIMARY KEY CLUSTERED 
(
	[YilAy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EDA]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EDA](
	[SirketKod] [varchar](2) NOT NULL,
	[MaliYil] [int] NOT NULL,
	[MaliBasAy] [smallint] NOT NULL,
	[MhsSdkKod] [varchar](2) NOT NULL,
	[MhsSirKod] [varchar](2) NOT NULL,
	[FnstSdkKod] [varchar](2) NOT NULL,
	[FnstSirKod] [varchar](2) NOT NULL,
	[Stok_Duz_Yontem] [smallint] NOT NULL,
	[Stok_Ard_Yontem] [smallint] NOT NULL,
	[Gelir_Duz_Yontem] [smallint] NOT NULL,
	[Gelir_Ard_Yontem] [smallint] NOT NULL,
	[Kredi_Ard_Yontem] [smallint] NOT NULL,
	[Vadeli_Ard_Yontem] [smallint] NOT NULL,
	[KayitTuru] [smallint] NOT NULL,
	[SirketBasTarih] [int] NOT NULL,
	[EnfDuzYontem] [smallint] NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEDA] PRIMARY KEY CLUSTERED 
(
	[SirketKod] ASC,
	[MaliYil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EDK]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EDK](
	[Yil] [smallint] NOT NULL,
	[DovizCinsi] [varchar](3) NOT NULL,
	[Ocak] [numeric](25, 6) NOT NULL,
	[Subat] [numeric](25, 6) NOT NULL,
	[Mart] [numeric](25, 6) NOT NULL,
	[Nisan] [numeric](25, 6) NOT NULL,
	[Mayis] [numeric](25, 6) NOT NULL,
	[Haziran] [numeric](25, 6) NOT NULL,
	[Temmuz] [numeric](25, 6) NOT NULL,
	[Agustos] [numeric](25, 6) NOT NULL,
	[Eylul] [numeric](25, 6) NOT NULL,
	[Ekim] [numeric](25, 6) NOT NULL,
	[Kasim] [numeric](25, 6) NOT NULL,
	[Aralik] [numeric](25, 6) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEDK] PRIMARY KEY CLUSTERED 
(
	[Yil] ASC,
	[DovizCinsi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EKF]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EKF](
	[Yil] [smallint] NOT NULL,
	[Oran1] [numeric](25, 6) NOT NULL,
	[Oran2] [numeric](25, 6) NOT NULL,
	[Oran3] [numeric](25, 6) NOT NULL,
	[Oran4] [numeric](25, 6) NOT NULL,
	[Oran5] [numeric](25, 6) NOT NULL,
	[Oran6] [numeric](25, 6) NOT NULL,
	[Oran7] [numeric](25, 6) NOT NULL,
	[Oran8] [numeric](25, 6) NOT NULL,
	[Oran9] [numeric](25, 6) NOT NULL,
	[Oran10] [numeric](25, 6) NOT NULL,
	[Oran11] [numeric](25, 6) NOT NULL,
	[Oran12] [numeric](25, 6) NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEKF] PRIMARY KEY CLUSTERED 
(
	[Yil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EML]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EML](
	[Kullanici] [varchar](5) NOT NULL,
	[GonderenEPosta] [varchar](64) NOT NULL,
	[GonderenAdi] [varchar](64) NOT NULL,
	[Sunucu] [varchar](64) NOT NULL,
	[Port] [int] NOT NULL,
	[KullaniciAdi] [varchar](64) NOT NULL,
	[Sifre] [varchar](64) NOT NULL,
	[SSL] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEML] PRIMARY KEY CLUSTERED 
(
	[Kullanici] ASC,
	[GonderenEPosta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EPK]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EPK](
	[HesapKod] [varchar](50) NOT NULL,
	[KiymetTur] [smallint] NOT NULL,
	[OzelKiyTuruTan] [smallint] NOT NULL,
	[ReelOlmynFinMal] [smallint] NOT NULL,
	[EnfDuz_Secimi] [smallint] NOT NULL,
	[EnfDuz_EsasTarih_Secimi] [smallint] NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEPK] PRIMARY KEY CLUSTERED 
(
	[HesapKod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kullanici] [varchar](10) NOT NULL,
	[Tarih] [smalldatetime] NOT NULL,
	[Program] [varchar](50) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[Source] [varchar](max) NOT NULL,
	[ExceptionType] [varchar](max) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[StackTrace] [varchar](max) NOT NULL,
	[Image] [image] NOT NULL,
	[SendMail] [bit] NOT NULL,
 CONSTRAINT [PK_HataLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ETF]    Script Date: 11/04/2018 11:19:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ETF](
	[Yil] [smallint] NOT NULL,
	[Oran1] [numeric](25, 6) NOT NULL,
	[Oran2] [numeric](25, 6) NOT NULL,
	[Oran3] [numeric](25, 6) NOT NULL,
	[Oran4] [numeric](25, 6) NOT NULL,
	[Oran5] [numeric](25, 6) NOT NULL,
	[Oran6] [numeric](25, 6) NOT NULL,
	[Oran7] [numeric](25, 6) NOT NULL,
	[Oran8] [numeric](25, 6) NOT NULL,
	[Oran9] [numeric](25, 6) NOT NULL,
	[Oran10] [numeric](25, 6) NOT NULL,
	[Oran11] [numeric](25, 6) NOT NULL,
	[Oran12] [numeric](25, 6) NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkETF] PRIMARY KEY CLUSTERED 
(
	[Yil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EVT]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EVT](
	[Sirket] [varchar](2) NOT NULL,
	[Kullanici] [varchar](5) NOT NULL,
	[SablonAdi] [varchar](100) NOT NULL,
	[SablonTipi] [smallint] NOT NULL,
	[TableID] [smallint] NOT NULL,
	[FieldID] [smallint] NOT NULL,
	[EslemeTip] [smallint] NOT NULL,
	[KolonID] [varchar](100) NOT NULL,
	[OzelSecim] [smallint] NOT NULL,
	[OzelTip] [smallint] NOT NULL,
	[ExtDeger] [varchar](50) NOT NULL,
	[LogTut] [smallint] NOT NULL,
	[Guncelle] [smallint] NOT NULL,
	[ExlBasSatir] [int] NOT NULL,
	[ExlTplSatir] [int] NOT NULL,
	[Kod1] [varchar](20) NOT NULL,
	[Kod2] [varchar](20) NOT NULL,
	[Kod3] [varchar](20) NOT NULL,
	[Kod4] [varchar](20) NOT NULL,
	[Kod5] [varchar](20) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[ExtTableID] [int] NOT NULL,
	[ExtFieldID] [int] NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEVT] PRIMARY KEY CLUSTERED 
(
	[SablonAdi] ASC,
	[TableID] ASC,
	[SablonTipi] ASC,
	[Sirket] ASC,
	[Kullanici] ASC,
	[FieldID] ASC,
	[ExtTableID] ASC,
	[ExtFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EYK]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EYK](
	[Yil] [smallint] NOT NULL,
	[Oran1] [numeric](25, 6) NOT NULL,
	[Oran2] [numeric](25, 6) NOT NULL,
	[Oran3] [numeric](25, 6) NOT NULL,
	[Oran4] [numeric](25, 6) NOT NULL,
	[Oran5] [numeric](25, 6) NOT NULL,
	[Oran6] [numeric](25, 6) NOT NULL,
	[Oran7] [numeric](25, 6) NOT NULL,
	[Oran8] [numeric](25, 6) NOT NULL,
	[Oran9] [numeric](25, 6) NOT NULL,
	[Oran10] [numeric](25, 6) NOT NULL,
	[Oran11] [numeric](25, 6) NOT NULL,
	[Oran12] [numeric](25, 6) NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkEYK] PRIMARY KEY CLUSTERED 
(
	[Yil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAS]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAS](
	[YilAy] [int] NOT NULL,
	[FormTip] [smallint] NOT NULL,
	[DetayTip] [smallint] NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[Unvan] [varchar](80) NOT NULL,
	[UlkeNumKod] [varchar](3) NOT NULL,
	[VergiKod] [varchar](12) NOT NULL,
	[BelgeAdet] [int] NOT NULL,
	[KDVMatrah] [numeric](25, 6) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[UnvanOrj] [varchar](80) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkFAS] PRIMARY KEY CLUSTERED 
(
	[YilAy] ASC,
	[FormTip] ASC,
	[DetayTip] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FBH]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FBH](
	[YilAy] [int] NOT NULL,
	[FormTip] [smallint] NOT NULL,
	[KonsSirket1] [varchar](2) NOT NULL,
	[KonsSirket2] [varchar](2) NOT NULL,
	[KonsSirket3] [varchar](2) NOT NULL,
	[KonsSirket4] [varchar](2) NOT NULL,
	[KonsSirket5] [varchar](2) NOT NULL,
	[KonsSirket6] [varchar](2) NOT NULL,
	[KonsSirket7] [varchar](2) NOT NULL,
	[KonsSirket8] [varchar](2) NOT NULL,
	[KonsSirket9] [varchar](2) NOT NULL,
	[KonsSirket10] [varchar](2) NOT NULL,
	[SatirFlag] [smallint] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkFBH] PRIMARY KEY CLUSTERED 
(
	[YilAy] ASC,
	[FormTip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GenelIzinler]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenelIzinler](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciKodu] [nvarchar](5) NULL,
	[SirketKodu] [nvarchar](2) NULL,
	[GosterilecekSirketler] [nvarchar](50) NULL,
	[EkranGoruntusunuDegistirebilsin] [bit] NULL,
	[CokluSiparisAcmaKapatmaYapabilsin] [bit] NULL,
	[OnayModulunuGorebilsin] [bit] NULL,
	[OnModChkAraligi] [nvarchar](100) NULL,
	[OnModRiskYuzdeAraligi] [nvarchar](100) NULL,
	[OnModKod3OrtGunAraligi] [nvarchar](100) NULL,
	[OnModSirketTur] [nvarchar](100) NULL,
	[Aktar] [bit] NULL,
	[Sil] [bit] NULL,
	[Kayit] [bit] NULL,
	[HabercideGonderebilsin] [bit] NULL,
	[OnModTipKodu] [nvarchar](500) NULL,
 CONSTRAINT [PK__GenelIzinler__141DA9CB] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GIK]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GIK](
	[GumrukIdareKodu] [varchar](6) NOT NULL,
	[GumrukIdareAdi] [varchar](100) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkGIK] PRIMARY KEY CLUSTERED 
(
	[GumrukIdareKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GROUP_NAME]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GROUP_NAME](
	[ID] [int] NOT NULL,
	[GRPNAME] [varchar](100) NOT NULL,
	[GRPNAME_LNG1] [varchar](100) NULL,
	[GRPNAME_LNG2] [varchar](100) NULL,
 CONSTRAINT [pkGROUP_NAME] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GUNES_MALZEME_ENTEGRASYONU]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GUNES_MALZEME_ENTEGRASYONU](
	[SATIRNO] [int] IDENTITY(1,1) NOT NULL,
	[ISEMNO] [int] NOT NULL,
	[ISEMRIYIL] [int] NOT NULL,
	[ISEMTURKODU] [varchar](50) NOT NULL,
	[MALZEMEKODU] [varchar](50) NOT NULL,
	[MIKTAR] [float] NOT NULL,
	[BIRIMFIYAT] [float] NOT NULL,
	[BIRIM] [varchar](50) NOT NULL,
	[DEPO] [varchar](50) NOT NULL,
	[TARIH] [datetime] NOT NULL,
	[SAAT] [varchar](50) NOT NULL,
	[STATU] [int] NULL,
	[Tip] [smallint] NULL,
 CONSTRAINT [PK_GUNES_MALZEME_ENTEGRASYONU] PRIMARY KEY CLUSTERED 
(
	[SATIRNO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[INI]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INI](
	[MySection] [int] NOT NULL,
	[MyEntry] [int] NOT NULL,
	[MyValue] [varchar](248) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkINI] PRIMARY KEY CLUSTERED 
(
	[MySection] ASC,
	[MyEntry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IZR_Izin]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IZR_Izin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Izin] [nvarchar](100) NOT NULL,
	[Grup] [nvarchar](50) NULL,
	[Aktif] [bit] NULL,
	[Aciklama] [varchar](100) NULL,
	[KayitTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_IZR_Izin] PRIMARY KEY CLUSTERED 
(
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IZR_KullaniciIzin]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IZR_KullaniciIzin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Izin] [nvarchar](100) NOT NULL,
	[Okuma] [bit] NOT NULL,
	[Yazma] [bit] NOT NULL,
	[Kaydetme] [bit] NOT NULL,
	[Guncelleme] [bit] NOT NULL,
	[Silme] [bit] NOT NULL,
	[Onay] [bit] NOT NULL,
	[Kaydeden] [varchar](10) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](10) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_IZR_KullaniciIzin] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IZR_Rol]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IZR_Rol](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](10) NOT NULL,
	[Rol] [nvarchar](100) NOT NULL,
	[Kaydeden] [varchar](10) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](10) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_IZR_Rol] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IZR_RolIzin]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IZR_RolIzin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [nvarchar](100) NOT NULL,
	[Izin] [nvarchar](100) NOT NULL,
	[Okuma] [bit] NOT NULL,
	[Yazma] [bit] NOT NULL,
	[Kaydetme] [bit] NOT NULL,
	[Guncelleme] [bit] NOT NULL,
	[Silme] [bit] NOT NULL,
	[Onay] [bit] NOT NULL,
	[Kaydeden] [varchar](10) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](10) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_IZR_RolIzin] PRIMARY KEY CLUSTERED 
(
	[Rol] ASC,
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KEK]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KEK](
	[Tip] [smallint] NOT NULL,
	[TipAdi] [varchar](100) NOT NULL,
	[TipAdiEN] [varchar](100) NOT NULL,
	[RefTip] [smallint] NOT NULL,
	[KaynakKod] [varchar](20) NOT NULL,
	[KaynakKodAciklama] [varchar](100) NOT NULL,
	[ErekKod] [varchar](20) NOT NULL,
	[ErekKodAciklama] [varchar](100) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkKEK] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[KaynakKod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KPD]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KPD](
	[LoginID] [varchar](15) NOT NULL,
	[EskiSifre] [varchar](40) NOT NULL,
	[YeniSifre] [varchar](40) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkKPD] PRIMARY KEY CLUSTERED 
(
	[LoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KSD]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KSD](
	[KullaniciKodu] [varchar](5) NOT NULL,
	[KullaniciTipi] [smallint] NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[DonemKodu] [varchar](2) NOT NULL,
	[DonemTipi] [smallint] NOT NULL,
	[DonemTarihi] [int] NOT NULL,
	[SirketKisaAdi] [varchar](50) NOT NULL,
	[Driver] [varchar](2) NOT NULL,
	[Log] [smallint] NOT NULL,
	[LogFileName] [varchar](20) NOT NULL,
	[Directory] [varchar](60) NOT NULL,
	[AktifPasif] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkKSD] PRIMARY KEY CLUSTERED 
(
	[KullaniciKodu] ASC,
	[KullaniciTipi] ASC,
	[SirketKodu] ASC,
	[DonemKodu] ASC,
	[DonemTipi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KST]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KST](
	[KynkEvrakTip] [smallint] NOT NULL,
	[TabloNo] [smallint] NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[R001] [int] NOT NULL,
	[R002] [int] NOT NULL,
	[R003] [int] NOT NULL,
	[R004] [int] NOT NULL,
	[R005] [int] NOT NULL,
	[R006] [int] NOT NULL,
	[R007] [int] NOT NULL,
	[R008] [int] NOT NULL,
	[R009] [int] NOT NULL,
	[R010] [int] NOT NULL,
	[R011] [int] NOT NULL,
	[R012] [int] NOT NULL,
	[R013] [int] NOT NULL,
	[R014] [int] NOT NULL,
	[R015] [int] NOT NULL,
	[R016] [int] NOT NULL,
	[R017] [int] NOT NULL,
	[R018] [int] NOT NULL,
	[R019] [int] NOT NULL,
	[R020] [int] NOT NULL,
	[R021] [int] NOT NULL,
	[R022] [int] NOT NULL,
	[R023] [int] NOT NULL,
	[R024] [int] NOT NULL,
	[R025] [int] NOT NULL,
	[R026] [int] NOT NULL,
	[R027] [int] NOT NULL,
	[R028] [int] NOT NULL,
	[R029] [int] NOT NULL,
	[R030] [int] NOT NULL,
	[R031] [int] NOT NULL,
	[R032] [int] NOT NULL,
	[R033] [int] NOT NULL,
	[R034] [int] NOT NULL,
	[R035] [int] NOT NULL,
	[R036] [int] NOT NULL,
	[R037] [int] NOT NULL,
	[R038] [int] NOT NULL,
	[R039] [int] NOT NULL,
	[R040] [int] NOT NULL,
	[R041] [int] NOT NULL,
	[R042] [int] NOT NULL,
	[R043] [int] NOT NULL,
	[R044] [int] NOT NULL,
	[R045] [int] NOT NULL,
	[R046] [int] NOT NULL,
	[R047] [int] NOT NULL,
	[R048] [int] NOT NULL,
	[R049] [int] NOT NULL,
	[R050] [int] NOT NULL,
	[R051] [int] NOT NULL,
	[R052] [int] NOT NULL,
	[R053] [int] NOT NULL,
	[R054] [int] NOT NULL,
	[R055] [int] NOT NULL,
	[R056] [int] NOT NULL,
	[R057] [int] NOT NULL,
	[R058] [int] NOT NULL,
	[R059] [int] NOT NULL,
	[R060] [int] NOT NULL,
	[R061] [int] NOT NULL,
	[R062] [int] NOT NULL,
	[R063] [int] NOT NULL,
	[R064] [int] NOT NULL,
	[R065] [int] NOT NULL,
	[R066] [int] NOT NULL,
	[R067] [int] NOT NULL,
	[R068] [int] NOT NULL,
	[R069] [int] NOT NULL,
	[R070] [int] NOT NULL,
	[R071] [int] NOT NULL,
	[R072] [int] NOT NULL,
	[R073] [int] NOT NULL,
	[R074] [int] NOT NULL,
	[R075] [int] NOT NULL,
	[R076] [int] NOT NULL,
	[R077] [int] NOT NULL,
	[R078] [int] NOT NULL,
	[R079] [int] NOT NULL,
	[R080] [int] NOT NULL,
	[R081] [int] NOT NULL,
	[R082] [int] NOT NULL,
	[R083] [int] NOT NULL,
	[R084] [int] NOT NULL,
	[R085] [int] NOT NULL,
	[R086] [int] NOT NULL,
	[R087] [int] NOT NULL,
	[R088] [int] NOT NULL,
	[R089] [int] NOT NULL,
	[R090] [int] NOT NULL,
	[R091] [int] NOT NULL,
	[R092] [int] NOT NULL,
	[R093] [int] NOT NULL,
	[R094] [int] NOT NULL,
	[R095] [int] NOT NULL,
	[R096] [int] NOT NULL,
	[R097] [int] NOT NULL,
	[R098] [int] NOT NULL,
	[R099] [int] NOT NULL,
	[R100] [int] NOT NULL,
	[Sil] [smallint] NOT NULL,
	[Kod1] [smallint] NOT NULL,
	[Kod2] [smallint] NOT NULL,
	[Kod3] [smallint] NOT NULL,
	[Kod4] [smallint] NOT NULL,
	[Kod5] [smallint] NOT NULL,
	[Kod6] [varchar](32) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Kod7] [smallint] NOT NULL,
	[Kod8] [smallint] NOT NULL,
	[Kod9] [smallint] NOT NULL,
	[Kod10] [smallint] NOT NULL,
	[Kod11] [smallint] NOT NULL,
	[Kod12] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkKST] PRIMARY KEY CLUSTERED 
(
	[KynkEvrakTip] ASC,
	[TabloNo] ASC,
	[Tip] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KTK]    Script Date: 11/04/2018 11:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KTK](
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](20) NOT NULL,
	[Aciklama] [varchar](100) NOT NULL,
	[GrupKod] [varchar](20) NOT NULL,
	[SayKod] [float] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Aciklama2] [varchar](256) NOT NULL,
	[Tip2] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkKTK] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LAR]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LAR](
	[Tip] [smallint] NOT NULL,
	[Arsiv] [image] NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkLAR] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LCK]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LCK](
	[SeriKey] [int] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkLCK] PRIMARY KEY CLUSTERED 
(
	[SeriKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LRG]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LRG](
	[RKey] [varchar](248) NOT NULL,
	[REntry] [varchar](248) NOT NULL,
	[RValue] [varchar](248) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkLRG] PRIMARY KEY CLUSTERED 
(
	[RKey] ASC,
	[REntry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MTK]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MTK](
	[TanimTipi] [smallint] NOT NULL,
	[VeriTipi] [smallint] NOT NULL,
	[VergiKimlikNo] [varchar](10) NOT NULL,
	[TCKimlikNo] [varchar](11) NOT NULL,
	[Unvan1] [varchar](40) NOT NULL,
	[Unvan2] [varchar](40) NOT NULL,
	[TicSicilNo] [varchar](15) NOT NULL,
	[EPosta] [varchar](50) NOT NULL,
	[TelAlanKodu] [varchar](3) NOT NULL,
	[TelNumarasi] [varchar](7) NOT NULL,
	[VergiDairesi] [varchar](12) NOT NULL,
	[Sifat] [smallint] NOT NULL,
	[MersisNo] [varchar](16) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkMTK] PRIMARY KEY CLUSTERED 
(
	[TanimTipi] ASC,
	[VeriTipi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OAS]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OAS](
	[OnaySemaKod] [int] NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[GroupUser] [smallint] NOT NULL,
	[UserKod] [varchar](5) NOT NULL,
	[Aciklama] [varchar](64) NOT NULL,
	[Bilgi] [varchar](5) NOT NULL,
	[TimeOut] [int] NOT NULL,
	[TimeOutType] [smallint] NOT NULL,
	[StatusMsg] [smallint] NOT NULL,
	[Skipping] [smallint] NOT NULL,
	[UpdateOption] [smallint] NOT NULL,
	[GecerlilikSuresiBas] [int] NOT NULL,
	[GecerlilikSuresiBit] [int] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[TimeLimitType] [smallint] NOT NULL,
	[GecSuresiBas] [int] NOT NULL,
	[GecSuresiBit] [int] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkOAS] PRIMARY KEY CLUSTERED 
(
	[OnaySemaKod] ASC,
	[SiraNo] ASC,
	[GroupUser] ASC,
	[UserKod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ODI]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ODI](
	[PkEvrak] [varchar](60) NOT NULL,
	[OnaySemaKod] [int] NOT NULL,
	[IslemTip] [smallint] NOT NULL,
	[Tekrar] [smallint] NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[Onaylayan] [varchar](5) NOT NULL,
	[OnayStatus] [smallint] NOT NULL,
	[OnayTarih] [int] NOT NULL,
	[OnaySaat] [int] NOT NULL,
	[Aciklama] [varchar](255) NOT NULL,
	[Evrak] [image] NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFS]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFS](
	[SirketKod] [varchar](2) NOT NULL,
	[EvrakTip] [smallint] NOT NULL,
	[Adi] [varchar](255) NOT NULL,
	[IslemTur] [smallint] NOT NULL,
	[Filtre] [varchar](255) NOT NULL,
	[OnayKul] [varchar](255) NOT NULL,
	[BekSuresi1] [smallint] NOT NULL,
	[BekDilim1] [smallint] NOT NULL,
	[AltKul1] [varchar](255) NOT NULL,
	[BekSuresi2] [smallint] NOT NULL,
	[BekDilim2] [smallint] NOT NULL,
	[AltKul2] [varchar](255) NOT NULL,
	[KayitTuru] [smallint] NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkOFS] PRIMARY KEY CLUSTERED 
(
	[SirketKod] ASC,
	[EvrakTip] ASC,
	[IslemTur] ASC,
	[Filtre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Onikim_Terminal]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Onikim_Terminal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[SevkEvrakNo] [varchar](16) NOT NULL,
	[SiparisNo] [varchar](16) NOT NULL,
	[Donem] [varchar](16) NULL,
	[Chk] [varchar](20) NOT NULL,
	[SipSiraNo] [smallint] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[BarkodNo] [varchar](100) NOT NULL,
	[BarkodMiktar] [decimal](24, 6) NOT NULL,
	[Birim] [nchar](10) NULL,
	[Depo] [nchar](10) NULL,
	[IslemTip] [smallint] NOT NULL,
	[AktarimDurum] [smallint] NOT NULL,
	[Kaydeden] [varchar](10) NOT NULL,
	[KayitTarih] [datetime] NOT NULL,
	[KayitTarih2]  AS (CONVERT([int],dateadd(day,(0),datediff(day,(0),[KayitTarih]))+(2))) PERSISTED,
 CONSTRAINT [PK_Onikim_Terminal] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OPK]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OPK](
	[Kod] [varchar](5) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[AktifPasif] [smallint] NOT NULL,
	[Ad] [varchar](40) NOT NULL,
	[Soyad] [varchar](40) NOT NULL,
	[TCKimlikNo] [varchar](11) NOT NULL,
	[DogumTarih] [int] NOT NULL,
	[Cinsiyet] [smallint] NOT NULL,
	[MedeniHal] [smallint] NOT NULL,
	[EvlilikTarih] [int] NOT NULL,
	[ePosta] [varchar](50) NOT NULL,
	[Telefon1UlkeKod] [varchar](4) NOT NULL,
	[Telefon1BolgeKod] [varchar](4) NOT NULL,
	[Telefon1] [varchar](7) NOT NULL,
	[Telefon1Dahili] [varchar](6) NOT NULL,
	[Telefon2UlkeKod] [varchar](4) NOT NULL,
	[Telefon2BolgeKod] [varchar](4) NOT NULL,
	[Telefon2] [varchar](7) NOT NULL,
	[Telefon2Dahili] [varchar](6) NOT NULL,
	[Adres1] [varchar](40) NOT NULL,
	[Adres2] [varchar](40) NOT NULL,
	[Adres3] [varchar](40) NOT NULL,
	[PostaKod] [varchar](7) NOT NULL,
	[GorevKod] [varchar](10) NOT NULL,
	[Resim] [varchar](256) NOT NULL,
	[KayitTuru] [smallint] NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkOPK] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OPT]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OPT](
	[Kullanici] [varchar](5) NOT NULL,
	[ProgramID] [smallint] NOT NULL,
	[DosyaAdi] [varchar](248) NOT NULL,
	[DosyaDizini] [varchar](248) NOT NULL,
	[Parametre] [varchar](248) NOT NULL,
	[Ekstralar] [varchar](248) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkOPT] PRIMARY KEY CLUSTERED 
(
	[Kullanici] ASC,
	[ProgramID] ASC,
	[DosyaAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PWD]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PWD](
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Sifre] [varchar](40) NOT NULL,
	[Grup01] [varchar](5) NOT NULL,
	[Grup02] [varchar](5) NOT NULL,
	[Grup03] [varchar](5) NOT NULL,
	[Grup04] [varchar](5) NOT NULL,
	[Grup05] [varchar](5) NOT NULL,
	[Grup06] [varchar](5) NOT NULL,
	[Grup07] [varchar](5) NOT NULL,
	[Grup08] [varchar](5) NOT NULL,
	[Grup09] [varchar](5) NOT NULL,
	[Grup10] [varchar](5) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[EskiSifre1] [varchar](40) NOT NULL,
	[EskiSifre2] [varchar](40) NOT NULL,
	[EskiSifre3] [varchar](40) NOT NULL,
	[SDTarih] [int] NOT NULL,
	[BlkDurumu] [smallint] NOT NULL,
	[Kod1] [varchar](20) NOT NULL,
	[Kod2] [varchar](20) NOT NULL,
	[Kod3] [varchar](40) NOT NULL,
	[eDefterYetkiSeviye] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkPWD] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PWD01]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PWD01](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Ad] [varchar](20) NOT NULL,
	[Soyad] [varchar](20) NOT NULL,
	[Aciklama] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Sifre] [varchar](100) NOT NULL,
	[EskiSifre1] [varchar](100) NULL,
	[EskiSifre2] [varchar](100) NULL,
	[EskiSifre3] [varchar](100) NULL,
	[AktifPasif] [smallint] NOT NULL,
	[Tema] [varchar](50) NULL,
	[Rol] [varchar](100) NULL,
	[Grup] [varchar](20) NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitSurum] [varchar](20) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisSurum] [varchar](20) NOT NULL,
 CONSTRAINT [PK_PWD01_1] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RES]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RES](
	[ID] [int] NOT NULL,
	[RESTYPE] [smallint] NOT NULL,
	[RESKEY] [int] NOT NULL,
	[RESGROUP] [int] NOT NULL,
	[RESNAME] [varchar](255) NOT NULL,
	[RESNAME_LNG1] [varchar](255) NOT NULL,
	[RESNAME_LNG2] [varchar](255) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkRES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RGT]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RGT](
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[RightType] [smallint] NOT NULL,
	[Rights] [image] NULL,
	[Kod1] [smallint] NOT NULL,
	[Kod2] [varchar](254) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkRGT] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[Kod] ASC,
	[RightType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RPK]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RPK](
	[SirketKod] [varchar](2) NOT NULL,
	[Kullanici] [varchar](5) NOT NULL,
	[DosyaAdi] [varchar](256) NOT NULL,
	[MySection] [varchar](256) NOT NULL,
	[MyEntry] [varchar](256) NOT NULL,
	[MyValue] [varchar](256) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkRPK] PRIMARY KEY CLUSTERED 
(
	[SirketKod] ASC,
	[Kullanici] ASC,
	[DosyaAdi] ASC,
	[MySection] ASC,
	[MyEntry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RPR]    Script Date: 11/04/2018 11:20:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RPR](
	[DosyaAdi] [varchar](255) NOT NULL,
	[DosyaDizini] [varchar](255) NOT NULL,
	[MainTable] [smallint] NOT NULL,
	[RaporTuru] [smallint] NOT NULL,
	[Company] [smallint] NOT NULL,
	[RaporAdi] [varchar](255) NOT NULL,
	[Addon] [varchar](32) NOT NULL,
	[ParaBrm] [smallint] NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[CiktiTuru] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkRPR] PRIMARY KEY CLUSTERED 
(
	[DosyaDizini] ASC,
	[DosyaAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RTK]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RTK](
	[SSection] [varchar](248) NOT NULL,
	[SEntry] [varchar](248) NOT NULL,
	[SValue] [varchar](248) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkRTK] PRIMARY KEY CLUSTERED 
(
	[SSection] ASC,
	[SEntry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SDK]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SDK](
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](2) NOT NULL,
	[SirketKod] [varchar](2) NOT NULL,
	[Tarih] [int] NOT NULL,
	[Driver] [varchar](2) NOT NULL,
	[Log] [smallint] NOT NULL,
	[LogFileName] [varchar](20) NOT NULL,
	[Directory] [varchar](60) NOT NULL,
	[TLTip] [smallint] NOT NULL,
	[DataFolder] [varchar](248) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[LogPeriyod] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkSDK] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SGB]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SGB](
	[SirketKod] [varchar](2) NOT NULL,
	[Kod] [varchar](20) NOT NULL,
	[Unvan] [varchar](128) NOT NULL,
	[EfatEtiketGB] [varchar](128) NOT NULL,
	[EfatEtiketPK] [varchar](128) NOT NULL,
	[Adres1] [varchar](40) NOT NULL,
	[Adres2] [varchar](40) NOT NULL,
	[Adres3] [varchar](40) NOT NULL,
	[CaddeSokak] [varchar](40) NOT NULL,
	[BinaAdi] [varchar](40) NOT NULL,
	[BinaNo] [varchar](10) NOT NULL,
	[KapiNo] [varchar](10) NOT NULL,
	[KasabaKoy] [varchar](40) NOT NULL,
	[Ilce] [varchar](40) NOT NULL,
	[Sehir] [varchar](40) NOT NULL,
	[PostaKodu] [varchar](7) NOT NULL,
	[UlkeKodu] [varchar](3) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Logo] [varchar](256) NOT NULL,
	[FaturadaKullan] [smallint] NOT NULL,
	[TicSicilNo] [varchar](15) NOT NULL,
	[MersisNo] [varchar](16) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkSGB] PRIMARY KEY CLUSTERED 
(
	[SirketKod] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SIN]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SIN](
	[SSection] [varchar](248) NOT NULL,
	[SEntry] [varchar](248) NOT NULL,
	[SValue] [varchar](248) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkSIN] PRIMARY KEY CLUSTERED 
(
	[SSection] ASC,
	[SEntry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SIR]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SIR](
	[Kod] [varchar](2) NOT NULL,
	[KisaAdi] [varchar](50) NOT NULL,
	[Unvan1] [varchar](40) NOT NULL,
	[Unvan2] [varchar](40) NOT NULL,
	[Adres1] [varchar](40) NOT NULL,
	[Adres2] [varchar](40) NOT NULL,
	[Adres3] [varchar](40) NOT NULL,
	[VergiDairesi] [varchar](6) NOT NULL,
	[HesapNo] [varchar](12) NOT NULL,
	[Telefon1] [varchar](14) NOT NULL,
	[Telefon2] [varchar](14) NOT NULL,
	[Telefon3] [varchar](14) NOT NULL,
	[Telefon4] [varchar](14) NOT NULL,
	[Fax1] [varchar](14) NOT NULL,
	[Fax2] [varchar](14) NOT NULL,
	[BolgeKod] [varchar](4) NOT NULL,
	[TicSicilNo] [varchar](15) NOT NULL,
	[Sermaye] [numeric](25, 0) NOT NULL,
	[YMMSMM] [varchar](30) NOT NULL,
	[Oda] [varchar](30) NOT NULL,
	[OdaSicilNo] [varchar](30) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[AktifPasif] [smallint] NOT NULL,
	[Cadde] [varchar](40) NOT NULL,
	[BinaAdi] [varchar](40) NOT NULL,
	[DisKapiNo] [varchar](10) NOT NULL,
	[IcKapiNo] [varchar](10) NOT NULL,
	[KasabaKoy] [varchar](40) NOT NULL,
	[Ilce] [varchar](40) NOT NULL,
	[Sehir] [varchar](40) NOT NULL,
	[PostaKodu] [varchar](7) NOT NULL,
	[UlkeNumKod] [varchar](3) NOT NULL,
	[WebAdres] [varchar](50) NOT NULL,
	[EMail] [varchar](50) NOT NULL,
	[EfatEtiketGB] [varchar](128) NOT NULL,
	[EfatEtiketPK] [varchar](128) NOT NULL,
	[EfatEtiketTip] [smallint] NOT NULL,
	[IsletmeMerkezi] [varchar](32) NOT NULL,
	[Ad] [varchar](40) NOT NULL,
	[SoyAd] [varchar](40) NOT NULL,
	[MersisNo] [varchar](16) NOT NULL,
	[Logo] [varchar](256) NOT NULL,
	[FaturadaKullan] [smallint] NOT NULL,
	[MrkzimVar] [smallint] NOT NULL,
	[MrkzUnvan1] [varchar](40) NOT NULL,
	[MrkzUnvan2] [varchar](40) NOT NULL,
	[MrkzCadde] [varchar](40) NOT NULL,
	[MrkzBinaAdi] [varchar](40) NOT NULL,
	[MrkzDisKapiNo] [varchar](10) NOT NULL,
	[MrkzIcKapiNo] [varchar](10) NOT NULL,
	[MrkzKasabaKoy] [varchar](40) NOT NULL,
	[MrkzIlce] [varchar](40) NOT NULL,
	[MrkzSehir] [varchar](40) NOT NULL,
	[MrkzPostaKodu] [varchar](7) NOT NULL,
	[MrkzUlkeNumKod] [varchar](3) NOT NULL,
	[MrkzWebAdres] [varchar](50) NOT NULL,
	[MrkzEMail] [varchar](50) NOT NULL,
	[MrkzFax1] [varchar](14) NOT NULL,
	[MrkzTelefon1] [varchar](14) NOT NULL,
	[YerelParaBrm] [varchar](3) NOT NULL,
	[EArsivKullan] [smallint] NOT NULL,
	[IhracatEFatGecisTarih] [int] NOT NULL,
	[IhracatGeciciKagitFat] [smallint] NOT NULL,
	[YolcuBerEFatGecisTarih] [int] NOT NULL,
	[YolcuBerGeciciKagitFat] [smallint] NOT NULL,
	[KEPAdresi] [varchar](50) NOT NULL,
	[SGKFatura] [smallint] NOT NULL,
	[eIrsGecisTarih] [int] NOT NULL,
	[eIrsGeciciKagitIrsaliye] [smallint] NOT NULL,
	[ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkSIR] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiparisINI]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiparisINI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[MySection] [int] NOT NULL,
	[MyEntry] [int] NOT NULL,
	[GosterilecekDepo] [varchar](500) NOT NULL,
	[SevkEvrakNo] [varchar](20) NOT NULL,
 CONSTRAINT [PK_SiparisINI] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SKT]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SKT](
	[SirKod] [varchar](2) NOT NULL,
	[Kullanici] [varchar](5) NOT NULL,
	[ID] [int] NOT NULL,
	[Dizin] [varchar](255) NOT NULL,
	[KisayolAdi] [varchar](255) NOT NULL,
	[KisayolTipi] [smallint] NOT NULL,
	[ArrType] [smallint] NOT NULL,
	[SolarType] [smallint] NOT NULL,
	[GuvenlikKod] [varchar](1) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkSKT] PRIMARY KEY CLUSTERED 
(
	[SirKod] ASC,
	[Kullanici] ASC,
	[Dizin] ASC,
	[ID] ASC,
	[KisayolTipi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TABLE_NAME]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_NAME](
	[ID] [int] NOT NULL,
	[TBLNAME] [varchar](32) NOT NULL,
	[TBLALIAS] [varchar](100) NULL,
	[TBLALIAS_LNG1] [varchar](100) NULL,
	[TBLALIAS_LNG2] [varchar](100) NULL,
	[TBLVISIBLE] [smallint] NOT NULL,
	[TBLGROUPID] [int] NULL,
	[TBLKOD] [smallint] NOT NULL,
	[CHISTORY] [smallint] NULL,
	[RHISTORY] [smallint] NULL,
	[UHISTORY] [smallint] NULL,
	[DHISTORY] [smallint] NULL,
	[LOG] [smallint] NULL,
 CONSTRAINT [pkTABLE_NAME] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TBLNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBK]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBK](
	[KullaniciKodu] [varchar](6) NOT NULL,
	[EvrakTipi] [smallint] NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[DonemKodu] [varchar](2) NOT NULL,
	[DonemTipi] [smallint] NOT NULL,
	[DonemTarihi] [int] NOT NULL,
	[KisitTipi] [smallint] NOT NULL,
	[Gun] [int] NOT NULL,
	[BasTarih] [int] NOT NULL,
	[BitTarih] [int] NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkTBK] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[KullaniciKodu] ASC,
	[EvrakTipi] ASC,
	[SirketKodu] ASC,
	[DonemKodu] ASC,
	[DonemTipi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TFT]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TFT](
	[FormulAdi] [varchar](50) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[HesapKodu] [varchar](50) NOT NULL,
	[HesapSaha] [int] NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Operator] [smallint] NOT NULL,
	[TabloID] [smallint] NOT NULL,
	[Sabit] [numeric](25, 6) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkTFT] PRIMARY KEY CLUSTERED 
(
	[FormulAdi] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TTK]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TTK](
	[SirDKod] [varchar](2) NOT NULL,
	[Kullanici] [varchar](5) NOT NULL,
	[MySection] [int] NOT NULL,
	[MyEntry] [int] NOT NULL,
	[MyValue] [varchar](248) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkTTK] PRIMARY KEY CLUSTERED 
(
	[SirDKod] ASC,
	[Kullanici] ASC,
	[MySection] ASC,
	[MyEntry] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TUM_Users]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TUM_Users](
	[KullaniciID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciKodu] [nvarchar](5) NULL,
	[KullaniciAdi] [nvarchar](50) NULL,
	[Sifre] [nvarchar](50) NULL,
	[Grup] [nvarchar](50) NULL,
 CONSTRAINT [PK_TUM_Users] PRIMARY KEY CLUSTERED 
(
	[KullaniciID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TUM_Web_Users]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TUM_Web_Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciAdi] [varchar](5) NOT NULL,
	[Sifre] [varchar](10) NOT NULL,
	[AdSoyad] [varchar](30) NOT NULL,
	[TipKod] [varchar](1000) NOT NULL,
	[GrupKod] [varchar](1000) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Seri1] [int] NOT NULL,
	[Seri2] [int] NOT NULL,
 CONSTRAINT [PK_TUM_Web_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_TUM_Web_Users] UNIQUE NONCLUSTERED 
(
	[KullaniciAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TumMBI]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TumMBI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciAdi] [nvarchar](5) NULL,
	[gosterilecekSirketler] [nvarchar](max) NULL,
	[gosterilecekDepolar] [nvarchar](max) NULL,
	[miktariGecebilsinmi] [bit] NULL,
	[durum] [bit] NULL,
	[sifre] [nvarchar](50) NULL,
	[grup] [nvarchar](50) NULL,
	[kullanilacakSeri] [int] NULL,
	[DepolarArasiTransferFisiKullanilacakSeri] [int] NULL,
	[rafNoKullanimi] [bit] NULL,
	[SicakSatis] [bit] NULL,
	[SogukSatis] [bit] NULL,
	[MalKabul] [bit] NULL,
	[SatistanIadeIrsaliyesi] [bit] NULL,
	[DepoTransfer] [bit] NULL,
	[StokMikHespDepo] [nvarchar](50) NULL,
	[BarkodOkutmadanIslemYapabilsin] [bit] NULL,
	[IcTransfer] [bit] NULL,
	[KayitliEvraklariGor] [bit] NULL,
 CONSTRAINT [PK_TumMBI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UBK]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UBK](
	[UlkeKodu] [varchar](3) NOT NULL,
	[UlkeNumKodu] [varchar](3) NOT NULL,
	[Kodu] [varchar](3) NOT NULL,
	[Adi] [varchar](40) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[SwiftKod] [varchar](11) NOT NULL,
	[ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkUBK] PRIMARY KEY CLUSTERED 
(
	[UlkeNumKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UCK]    Script Date: 11/04/2018 11:20:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UCK](
	[UlkeKodu] [varchar](3) NOT NULL,
	[UlkeNumKodu] [varchar](3) NOT NULL,
	[IlKodu] [varchar](3) NOT NULL,
	[Kodu] [varchar](5) NOT NULL,
	[Adi] [varchar](32) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkUCK] PRIMARY KEY CLUSTERED 
(
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[IlKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UIK]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UIK](
	[UlkeKodu] [varchar](3) NOT NULL,
	[UlkeNumKodu] [varchar](3) NOT NULL,
	[Kodu] [varchar](3) NOT NULL,
	[Adi] [varchar](32) NOT NULL,
	[TelKodu] [varchar](4) NOT NULL,
	[TelKodu2] [varchar](4) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkUIK] PRIMARY KEY CLUSTERED 
(
	[UlkeNumKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USK]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USK](
	[UlkeKodu] [varchar](3) NOT NULL,
	[UlkeNumKodu] [varchar](3) NOT NULL,
	[BankaKodu] [varchar](3) NOT NULL,
	[Kodu] [varchar](5) NOT NULL,
	[IlKodu] [varchar](3) NOT NULL,
	[Adi] [varchar](40) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkUSK] PRIMARY KEY CLUSTERED 
(
	[UlkeNumKodu] ASC,
	[BankaKodu] ASC,
	[IlKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UUK]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UUK](
	[Kodu] [varchar](3) NOT NULL,
	[NumKodu] [varchar](3) NOT NULL,
	[Adi] [varchar](70) NOT NULL,
	[AdiEng] [varchar](70) NOT NULL,
	[TelKodu] [varchar](4) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkUUK] PRIMARY KEY CLUSTERED 
(
	[NumKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UVK]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UVK](
	[UlkeKodu] [varchar](3) NOT NULL,
	[UlkeNumKodu] [varchar](3) NOT NULL,
	[IlKodu] [varchar](3) NOT NULL,
	[Kodu] [varchar](6) NOT NULL,
	[Adi] [varchar](64) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkUVK] PRIMARY KEY CLUSTERED 
(
	[UlkeNumKodu] ASC,
	[IlKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VA_B2B_Sepet]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VA_B2B_Sepet](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[MalAdi] [varchar](50) NOT NULL,
	[UrunGrubu] [varchar](20) NOT NULL,
	[BaglantiNo] [varchar](20) NOT NULL,
	[FiyatListeNum] [varchar](20) NOT NULL,
	[BaglantiSartlari] [varchar](30) NULL,
	[Koleksiyon] [varchar](20) NULL,
	[Kalite] [varchar](20) NULL,
	[Kalinlik] [varchar](20) NULL,
	[En] [varchar](20) NULL,
	[Boy] [varchar](20) NULL,
	[Katsayi] [decimal](24, 6) NULL,
	[Katsayi2] [decimal](24, 6) NULL,
	[DepoKodu] [varchar](10) NULL,
	[PalettekiMiktar] [decimal](24, 6) NULL,
	[PaletAdedi] [decimal](24, 6) NULL,
	[BTB_SiparisMiktari] [decimal](24, 6) NULL,
	[BTB_Birim] [varchar](4) NULL,
	[BTB_BirimFiyat] [decimal](24, 6) NULL,
	[SPI_SiparisMiktari] [decimal](24, 6) NULL,
	[SPI_Birim] [varchar](4) NULL,
	[SPI_BirimFiyat] [decimal](24, 6) NULL,
	[MalBedeli] [decimal](24, 6) NULL,
	[Iskonto1] [decimal](24, 6) NULL,
	[Iskonto2] [decimal](24, 6) NULL,
	[Iskonto3] [decimal](24, 6) NULL,
	[Iskonto4] [decimal](24, 6) NULL,
	[Iskonto5] [decimal](24, 6) NULL,
	[IskontoToplam]  AS (((([Iskonto1]+[Iskonto2])+[Iskonto3])+[Iskonto4])+[Iskonto5]) PERSISTED,
	[AraToplam] [decimal](24, 6) NULL,
	[KDV] [decimal](24, 6) NULL,
	[Toplam] [decimal](24, 6) NULL,
	[Agirlik] [decimal](18, 6) NULL,
	[AltlikID] [int] NULL,
	[Kaydeden] [varchar](20) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](20) NULL,
	[DegisTarih] [smalldatetime] NULL,
	[StokDurum] [nvarchar](20) NULL,
	[TahTeslimTarihi] [int] NULL,
 CONSTRAINT [PK_Sepet] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VA_B2B_Sepet2]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VA_B2B_Sepet2](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[MalAdi] [varchar](50) NOT NULL,
	[UrunGrubu] [varchar](20) NOT NULL,
	[BaglantiNo] [varchar](20) NOT NULL,
	[FiyatListeNum] [varchar](20) NOT NULL,
	[BaglantiSartlari] [varchar](30) NULL,
	[Koleksiyon] [varchar](20) NULL,
	[Kalite] [varchar](20) NULL,
	[Kalinlik] [varchar](20) NULL,
	[En] [varchar](20) NULL,
	[Boy] [varchar](20) NULL,
	[Katsayi] [decimal](24, 6) NULL,
	[Katsayi2] [decimal](24, 6) NULL,
	[PalettekiMiktar] [decimal](24, 6) NULL,
	[PaletAdedi] [decimal](24, 6) NULL,
	[BTB_SiparisMiktari] [decimal](24, 6) NULL,
	[BTB_Birim] [varchar](4) NULL,
	[BTB_BirimFiyat] [decimal](24, 6) NULL,
	[SPI_SiparisMiktari] [decimal](24, 6) NULL,
	[SPI_Birim] [varchar](4) NULL,
	[SPI_BirimFiyat] [decimal](24, 6) NULL,
	[MalBedeli] [decimal](24, 6) NULL,
	[Iskonto1] [decimal](24, 6) NULL,
	[Iskonto2] [decimal](24, 6) NULL,
	[Iskonto3] [decimal](24, 6) NULL,
	[Iskonto4] [decimal](24, 6) NULL,
	[Iskonto5] [decimal](24, 6) NULL,
	[IskontoToplam]  AS (((([Iskonto1]+[Iskonto2])+[Iskonto3])+[Iskonto4])+[Iskonto5]) PERSISTED,
	[AraToplam] [decimal](24, 6) NULL,
	[KDV] [decimal](24, 6) NULL,
	[Toplam] [decimal](24, 6) NULL,
	[Agirlik] [decimal](18, 6) NULL,
	[AltlikID] [int] NULL,
	[Kaydeden] [varchar](20) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](20) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_Sepet2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VERSION_INFO]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VERSION_INFO](
	[TABLE_NAME] [varchar](50) NOT NULL,
	[VERSION_ID] [int] NOT NULL,
	[TARIH] [int] NOT NULL,
	[ACIKLAMA] [varchar](100) NULL,
 CONSTRAINT [pkVERSION_INFO] PRIMARY KEY CLUSTERED 
(
	[TABLE_NAME] ASC,
	[VERSION_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VeziragacWebCEOUser]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VeziragacWebCEOUser](
	[LID] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [varchar](20) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserSurname] [varchar](50) NOT NULL,
	[UserType] [smallint] NOT NULL,
	[UserPassword] [varchar](10) NOT NULL,
	[InsertionTime] [datetime] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VKWRUsers]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VKWRUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](5) NOT NULL,
	[Password] [varchar](10) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[SurName] [varchar](30) NOT NULL,
	[Type] [smallint] NOT NULL,
	[SatisBaglantiRaporu] [bit] NULL,
	[SatisCiroRaporu] [bit] NULL,
	[OdemeYapmayanMusteriler] [bit] NULL,
	[SatilmayanUrunler] [bit] NULL,
	[EnEskiTarihliStoklar] [bit] NULL,
	[CekListesiRaporu] [bit] NULL,
	[BakiyeRaporu] [bit] NULL,
	[GunlukSatisRaporu] [bit] NULL,
	[VadesiGelmemisCekler] [bit] NULL,
	[SozlesmeTanim] [bit] NULL,
	[SatisMuduruOnay] [bit] NULL,
	[FinansmanMuduruOnay] [bit] NULL,
	[AdminPaneli] [bit] NULL,
	[Izinler] [bit] NULL,
	[ToplamRiskBakiyesi] [bit] NULL,
 CONSTRAINT [PK_VKWRUsers] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebKullanici]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebKullanici](
	[Kullanici] [varchar](30) NULL,
	[Sifre] [varchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebPelitUsers]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebPelitUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Sifre] [varchar](10) NOT NULL,
	[Ad] [varchar](30) NOT NULL,
	[Soyad] [varchar](30) NOT NULL,
	[Tip] [int] NOT NULL,
	[PageRapor] [bit] NOT NULL,
	[RaporSatisBaglanti] [bit] NOT NULL,
	[RaporSatisCiro] [bit] NOT NULL,
	[RaporCariSatisCiro] [bit] NULL,
	[RaporOdemeYapmayanMusteriler] [bit] NOT NULL,
	[RaporSatilmayanUrunler] [bit] NOT NULL,
	[RaporEnEskiUretimTarihliStoklar] [bit] NOT NULL,
	[RaporCekListesi] [bit] NOT NULL,
	[RaporBakiye] [bit] NOT NULL,
	[RaporGunlukSatis] [bit] NOT NULL,
	[RaporVadesiGelmemisCekler] [bit] NOT NULL,
	[RaporToplamRiskBakiyesi] [bit] NOT NULL,
	[RaporToplamRiskAnalizi] [bit] NOT NULL,
	[RaporGerceklesenSevkiyatPlani] [bit] NOT NULL,
	[RaporBekleyenSiparis] [bit] NOT NULL,
	[RaporUretimAgaci] [bit] NOT NULL,
	[RaporGunlukUretim] [bit] NULL,
	[RaporOnayArsivi] [bit] NULL,
	[PageSiparis] [bit] NOT NULL,
	[SiparisOnaySM] [bit] NOT NULL,
	[SiparisOnayIM] [bit] NOT NULL,
	[SiparisOnayGM] [bit] NOT NULL,
	[PageStok] [bit] NULL,
	[PageSozlesme] [bit] NOT NULL,
	[SozlesmeTanim] [bit] NOT NULL,
	[SozlesmeMaliyetGirisi] [bit] NULL,
	[SozlesmeOnaySM] [bit] NOT NULL,
	[SozlesmeOnayIM] [bit] NOT NULL,
	[SozlesmeOnayGM] [bit] NOT NULL,
	[PageRisk] [bit] NOT NULL,
	[RiskLimitTanim] [bit] NOT NULL,
	[RiskOnaySM] [bit] NOT NULL,
	[RiskOnaySPGMY] [bit] NOT NULL,
	[RiskOnayMIGMY] [bit] NOT NULL,
	[RiskOnayGM] [bit] NOT NULL,
	[PageTeminat] [bit] NOT NULL,
	[TeminatTanim] [bit] NOT NULL,
	[TeminatOnay] [bit] NOT NULL,
	[PageFiyat] [bit] NOT NULL,
	[FiyatTanim] [bit] NOT NULL,
	[FiyatOnaySM] [bit] NOT NULL,
	[FiyatOnaySPGMY] [bit] NOT NULL,
	[FiyatOnayGM] [bit] NOT NULL,
	[PageCek] [bit] NOT NULL,
	[CekOnaySPGMY] [bit] NOT NULL,
	[CekOnayMIGMY] [bit] NOT NULL,
	[CekOnayGM] [bit] NOT NULL,
	[PageAdminPaneli] [bit] NOT NULL,
	[AdminPaneli] [bit] NOT NULL,
	[Izinler] [bit] NOT NULL,
	[DashboardPanel] [bit] NULL,
	[DashboardTileYetki] [varchar](100) NULL,
 CONSTRAINT [PK_WebPelitUsers] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebUsers]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Sifre] [varchar](10) NOT NULL,
	[Ad] [varchar](30) NOT NULL,
	[Soyad] [varchar](30) NOT NULL,
	[Tip] [int] NOT NULL,
	[PageRapor] [bit] NOT NULL,
	[RaporSatisBaglanti] [bit] NOT NULL,
	[RaporSatisCiro] [bit] NOT NULL,
	[RaporCariSatisCiro] [bit] NULL,
	[RaporOdemeYapmayanMusteriler] [bit] NOT NULL,
	[RaporSatilmayanUrunler] [bit] NOT NULL,
	[RaporEnEskiUretimTarihliStoklar] [bit] NOT NULL,
	[RaporCekListesi] [bit] NOT NULL,
	[RaporBakiye] [bit] NOT NULL,
	[RaporGunlukSatis] [bit] NOT NULL,
	[RaporVadesiGelmemisCekler] [bit] NOT NULL,
	[RaporToplamRiskBakiyesi] [bit] NOT NULL,
	[RaporToplamRiskAnalizi] [bit] NOT NULL,
	[RaporGerceklesenSevkiyatPlani] [bit] NOT NULL,
	[RaporBekleyenSiparis] [bit] NOT NULL,
	[RaporUretimAgaci] [bit] NOT NULL,
	[RaporGunlukUretim] [bit] NULL,
	[RaporOnayArsivi] [bit] NULL,
	[PageSiparis] [bit] NOT NULL,
	[SiparisOnaySM] [bit] NOT NULL,
	[SiparisOnaySPGMY] [bit] NOT NULL,
	[SiparisOnayGM] [bit] NOT NULL,
	[PageStok] [bit] NULL,
	[PageSozlesme] [bit] NOT NULL,
	[SozlesmeTanim] [bit] NOT NULL,
	[SozlesmeOnaySM] [bit] NOT NULL,
	[SozlesmeOnaySPGMY] [bit] NOT NULL,
	[SozlesmeOnayGM] [bit] NOT NULL,
	[PageRisk] [bit] NOT NULL,
	[RiskLimitTanim] [bit] NOT NULL,
	[RiskOnaySM] [bit] NOT NULL,
	[RiskOnaySPGMY] [bit] NOT NULL,
	[RiskOnayMIGMY] [bit] NOT NULL,
	[RiskOnayGM] [bit] NOT NULL,
	[PageTeminat] [bit] NOT NULL,
	[TeminatTanim] [bit] NOT NULL,
	[TeminatOnay] [bit] NOT NULL,
	[PageFiyat] [bit] NOT NULL,
	[FiyatTanim] [bit] NOT NULL,
	[FiyatOnaySM] [bit] NOT NULL,
	[FiyatOnaySPGMY] [bit] NOT NULL,
	[FiyatOnayGM] [bit] NOT NULL,
	[PageCek] [bit] NOT NULL,
	[CekOnaySPGMY] [bit] NOT NULL,
	[CekOnayMIGMY] [bit] NOT NULL,
	[CekOnayGM] [bit] NOT NULL,
	[PageAdminPaneli] [bit] NOT NULL,
	[AdminPaneli] [bit] NOT NULL,
	[Izinler] [bit] NOT NULL,
	[DashboardPanel] [bit] NULL,
	[DashboardTileYetki] [varchar](100) NULL,
	[SozlesmeAktiflestirme] [bit] NULL,
	[PageIhaleTahsis] [bit] NOT NULL,
	[TahsisliAlimOnay] [bit] NOT NULL,
	[TahsisliAlimList] [bit] NOT NULL,
	[TahsisliIsletmeKasa] [bit] NOT NULL,
	[IhaleliAlimOnay] [bit] NOT NULL,
	[IhaleliAlimList] [bit] NOT NULL,
	[IhaleliIsletmeKasa] [bit] NOT NULL,
	[NakliyeFiyatOnay] [bit] NOT NULL,
	[NakliyeFiyatList] [bit] NOT NULL,
	[DisDepoStokRapor] [bit] NOT NULL,
	[DisDepoStokYaslandirma] [bit] NOT NULL,
	[DisDepoStokMaliyet] [bit] NOT NULL,
	[PageSatinalma] [bit] NOT NULL,
	[SatinalmaSipTalepGMOnay] [bit] NOT NULL,
	[SatinalmaTeklifList] [bit] NOT NULL,
	[SatinalmaOnayliTedList] [bit] NOT NULL,
	[SatinalmaTedSevkPerf] [bit] NOT NULL,
	[PageSatis] [bit] NULL,
 CONSTRAINT [PK_WebUsers] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YCT]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YCT](
	[BlgKulAdi] [varchar](30) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[Marka] [smallint] NOT NULL,
	[Model] [varchar](50) NOT NULL,
	[SeriNo] [varchar](50) NOT NULL,
	[Sifre] [varchar](20) NOT NULL,
	[Aktif] [smallint] NOT NULL,
	[ZRaporTip] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkYCT] PRIMARY KEY CLUSTERED 
(
	[BlgKulAdi] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YKK]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YKK](
	[KullaniciKodu] [varchar](5) NOT NULL,
	[KasiyerKodu] [varchar](10) NOT NULL,
	[KasiyerSifre] [varchar](20) NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkYKK] PRIMARY KEY CLUSTERED 
(
	[KullaniciKodu] ASC,
	[KasiyerKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YTG]    Script Date: 11/04/2018 11:20:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YTG](
	[Yil] [int] NOT NULL,
	[Tarih] [int] NOT NULL,
	[Aciklama] [varchar](254) NOT NULL,
	[GuvenlikKod] [varchar](2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[KayitSaat] [int] NOT NULL,
	[KayitKaynak] [smallint] NOT NULL,
	[KayitSurum] [varchar](12) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
	[DegisSaat] [int] NOT NULL,
	[DegisKaynak] [smallint] NOT NULL,
	[DegisSurum] [varchar](12) NOT NULL,
	[CheckSum] [smallint] NOT NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkYTG] PRIMARY KEY CLUSTERED 
(
	[Yil] ASC,
	[Tarih] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [pkAPPNAMESd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkAPPNAMESd] ON [dbo].[APPNAMES]
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [aspnet_PersonalizationPerUser_ncindex2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [aspnet_PersonalizationPerUser_ncindex2] ON [dbo].[aspnet_PersonalizationPerUser]
(
	[UserId] ASC,
	[PathId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [aspnet_Users_Index2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [aspnet_Users_Index2] ON [dbo].[aspnet_Users]
(
	[ApplicationId] ASC,
	[LastActivityDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [aspnet_UsersInRoles_index]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [aspnet_UsersInRoles_index] ON [dbo].[aspnet_UsersInRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AuthorizationRules]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [IX_AuthorizationRules] ON [dbo].[AuthorizationRules]
(
	[RULESOURCE] ASC,
	[RULETYPE] ASC,
	[ROOT] ASC,
	[ENTITY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkBTTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkBTTd] ON [dbo].[BTT]
(
	[SirDKod] DESC,
	[Kullanici] DESC,
	[MsgID] DESC,
	[TanimAdi] DESC,
	[SiraNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkCOLUMN_NAMEd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkCOLUMN_NAMEd] ON [dbo].[COLUMN_NAME]
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkCOMBO_NAMEd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkCOMBO_NAMEd] ON [dbo].[COMBO_NAME]
(
	[ID] DESC,
	[CBOTYPECOLUMNVALUE] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nCOMBOITEM_NAMEs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nCOMBOITEM_NAMEs2] ON [dbo].[COMBOITEM_NAME]
(
	[ID] ASC,
	[ITEMCBOID] ASC,
	[ITEMNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nCOMBOITEM_NAMEs3]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nCOMBOITEM_NAMEs3] ON [dbo].[COMBOITEM_NAME]
(
	[ITEMNAME] ASC,
	[ITEMCBOID] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkCOMBOITEM_NAMEd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkCOMBOITEM_NAMEd] ON [dbo].[COMBOITEM_NAME]
(
	[ITEMCBOID] DESC,
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkDRVd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkDRVd] ON [dbo].[DRV]
(
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkDTKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkDTKd] ON [dbo].[DTK]
(
	[AppID] DESC,
	[SirDKod] DESC,
	[Kullanici] DESC,
	[MySection] DESC,
	[MyEntry] DESC,
	[SiraNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkDVZd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkDVZd] ON [dbo].[DVZ]
(
	[Tarih] DESC,
	[DovizCinsi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkEAOd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEAOd] ON [dbo].[EAO]
(
	[Tarih] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkEATd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEATd] ON [dbo].[EAT]
(
	[Tip] DESC,
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkEBFd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEBFd] ON [dbo].[EBF]
(
	[YilAy] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkEDAd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEDAd] ON [dbo].[EDA]
(
	[SirketKod] DESC,
	[MaliYil] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkEDKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEDKd] ON [dbo].[EDK]
(
	[Yil] DESC,
	[DovizCinsi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkEKFd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEKFd] ON [dbo].[EKF]
(
	[Yil] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkEMLd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEMLd] ON [dbo].[EML]
(
	[Kullanici] DESC,
	[GonderenEPosta] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkEPKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEPKd] ON [dbo].[EPK]
(
	[HesapKod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkETFd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkETFd] ON [dbo].[ETF]
(
	[Yil] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkEVTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEVTd] ON [dbo].[EVT]
(
	[SablonAdi] DESC,
	[TableID] DESC,
	[SablonTipi] DESC,
	[Sirket] DESC,
	[Kullanici] DESC,
	[FieldID] DESC,
	[ExtTableID] DESC,
	[ExtFieldID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkEYKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkEYKd] ON [dbo].[EYK]
(
	[Yil] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkFASd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkFASd] ON [dbo].[FAS]
(
	[YilAy] DESC,
	[FormTip] DESC,
	[DetayTip] DESC,
	[SiraNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkFBHd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkFBHd] ON [dbo].[FBH]
(
	[YilAy] DESC,
	[FormTip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nGIKs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nGIKs2] ON [dbo].[GIK]
(
	[GumrukIdareAdi] ASC,
	[GumrukIdareKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nGIKs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nGIKs2d] ON [dbo].[GIK]
(
	[GumrukIdareAdi] DESC,
	[GumrukIdareKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkGIKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkGIKd] ON [dbo].[GIK]
(
	[GumrukIdareKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkGROUP_NAMEd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkGROUP_NAMEd] ON [dbo].[GROUP_NAME]
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkINId]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkINId] ON [dbo].[INI]
(
	[MySection] DESC,
	[MyEntry] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkKEKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkKEKd] ON [dbo].[KEK]
(
	[Tip] DESC,
	[KaynakKod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkKPDd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkKPDd] ON [dbo].[KPD]
(
	[LoginID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkKSDd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkKSDd] ON [dbo].[KSD]
(
	[KullaniciKodu] DESC,
	[KullaniciTipi] DESC,
	[SirketKodu] DESC,
	[DonemKodu] DESC,
	[DonemTipi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKSTs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKSTs2] ON [dbo].[KST]
(
	[Kod] ASC,
	[Tip] ASC,
	[KynkEvrakTip] ASC,
	[TabloNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkKSTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkKSTd] ON [dbo].[KST]
(
	[KynkEvrakTip] DESC,
	[TabloNo] DESC,
	[Tip] DESC,
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs2] ON [dbo].[KTK]
(
	[Aciklama] ASC,
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs2d] ON [dbo].[KTK]
(
	[Aciklama] DESC,
	[Kod] DESC,
	[Tip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs3]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs3] ON [dbo].[KTK]
(
	[GrupKod] ASC,
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs3d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs3d] ON [dbo].[KTK]
(
	[GrupKod] DESC,
	[Kod] DESC,
	[Tip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs4]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs4] ON [dbo].[KTK]
(
	[SayKod] ASC,
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs4d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs4d] ON [dbo].[KTK]
(
	[SayKod] DESC,
	[Kod] DESC,
	[Tip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs5]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs5] ON [dbo].[KTK]
(
	[Aciklama2] ASC,
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nKTKs5d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nKTKs5d] ON [dbo].[KTK]
(
	[Aciklama2] DESC,
	[Kod] DESC,
	[Tip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkKTKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkKTKd] ON [dbo].[KTK]
(
	[Kod] DESC,
	[Tip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkLARd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkLARd] ON [dbo].[LAR]
(
	[Tip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkLCKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkLCKd] ON [dbo].[LCK]
(
	[SeriKey] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkLRGd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkLRGd] ON [dbo].[LRG]
(
	[RKey] DESC,
	[REntry] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkMTKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkMTKd] ON [dbo].[MTK]
(
	[TanimTipi] DESC,
	[VeriTipi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkOASd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkOASd] ON [dbo].[OAS]
(
	[OnaySemaKod] DESC,
	[SiraNo] DESC,
	[GroupUser] DESC,
	[UserKod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkOFSd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkOFSd] ON [dbo].[OFS]
(
	[SirketKod] DESC,
	[EvrakTip] DESC,
	[IslemTur] DESC,
	[Filtre] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nOPKs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nOPKs2] ON [dbo].[OPK]
(
	[Ad] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nOPKs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nOPKs2d] ON [dbo].[OPK]
(
	[Ad] DESC,
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkOPKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkOPKd] ON [dbo].[OPK]
(
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkOPTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkOPTd] ON [dbo].[OPT]
(
	[Kullanici] DESC,
	[ProgramID] DESC,
	[DosyaAdi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkPWDd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkPWDd] ON [dbo].[PWD]
(
	[Tip] DESC,
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkRESd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkRESd] ON [dbo].[RES]
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkRGTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkRGTd] ON [dbo].[RGT]
(
	[Tip] DESC,
	[Kod] DESC,
	[RightType] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkRPKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkRPKd] ON [dbo].[RPK]
(
	[SirketKod] DESC,
	[Kullanici] DESC,
	[DosyaAdi] DESC,
	[MySection] DESC,
	[MyEntry] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nRPRs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nRPRs2] ON [dbo].[RPR]
(
	[DosyaDizini] ASC,
	[RaporAdi] ASC,
	[DosyaAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nRPRs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nRPRs2d] ON [dbo].[RPR]
(
	[DosyaDizini] DESC,
	[RaporAdi] DESC,
	[DosyaAdi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkRPRd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkRPRd] ON [dbo].[RPR]
(
	[DosyaDizini] DESC,
	[DosyaAdi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkRTKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkRTKd] ON [dbo].[RTK]
(
	[SSection] DESC,
	[SEntry] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkSDKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkSDKd] ON [dbo].[SDK]
(
	[Kod] DESC,
	[Tip] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkSGBd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkSGBd] ON [dbo].[SGB]
(
	[SirketKod] DESC,
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkSINd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkSINd] ON [dbo].[SIN]
(
	[SSection] DESC,
	[SEntry] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkSIRd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkSIRd] ON [dbo].[SIR]
(
	[Kod] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkSKTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkSKTd] ON [dbo].[SKT]
(
	[SirKod] DESC,
	[Kullanici] DESC,
	[Dizin] DESC,
	[ID] DESC,
	[KisayolTipi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nTABLE_NAMEs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nTABLE_NAMEs2] ON [dbo].[TABLE_NAME]
(
	[TBLNAME] ASC,
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkTABLE_NAMEd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkTABLE_NAMEd] ON [dbo].[TABLE_NAME]
(
	[ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkTBKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkTBKd] ON [dbo].[TBK]
(
	[Tip] DESC,
	[KullaniciKodu] DESC,
	[EvrakTipi] DESC,
	[SirketKodu] DESC,
	[DonemKodu] DESC,
	[DonemTipi] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkTFTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkTFTd] ON [dbo].[TFT]
(
	[FormulAdi] DESC,
	[SiraNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkTTKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkTTKd] ON [dbo].[TTK]
(
	[SirDKod] DESC,
	[Kullanici] DESC,
	[MySection] DESC,
	[MyEntry] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUBKs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUBKs2] ON [dbo].[UBK]
(
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUBKs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUBKs2d] ON [dbo].[UBK]
(
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkUBKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkUBKd] ON [dbo].[UBK]
(
	[UlkeNumKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUCKs5]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUCKs5] ON [dbo].[UCK]
(
	[Adi] ASC,
	[Kodu] ASC,
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[IlKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUCKs5d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUCKs5d] ON [dbo].[UCK]
(
	[Adi] DESC,
	[Kodu] DESC,
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[IlKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkUCKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkUCKd] ON [dbo].[UCK]
(
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[IlKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUIKs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUIKs2] ON [dbo].[UIK]
(
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUIKs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUIKs2d] ON [dbo].[UIK]
(
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkUIKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkUIKd] ON [dbo].[UIK]
(
	[UlkeNumKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkUSKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkUSKd] ON [dbo].[USK]
(
	[UlkeNumKodu] DESC,
	[BankaKodu] DESC,
	[IlKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUUKs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUUKs2] ON [dbo].[UUK]
(
	[Kodu] ASC,
	[NumKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUUKs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUUKs2d] ON [dbo].[UUK]
(
	[Kodu] DESC,
	[NumKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkUUKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkUUKd] ON [dbo].[UUK]
(
	[NumKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs2]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUVKs2] ON [dbo].[UVK]
(
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[IlKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs2d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE NONCLUSTERED INDEX [nUVKs2d] ON [dbo].[UVK]
(
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[IlKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs3]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [nUVKs3] ON [dbo].[UVK]
(
	[IlKodu] ASC,
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[Kodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs3d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [nUVKs3d] ON [dbo].[UVK]
(
	[IlKodu] DESC,
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs4]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [nUVKs4] ON [dbo].[UVK]
(
	[Kodu] ASC,
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[IlKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs4d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [nUVKs4d] ON [dbo].[UVK]
(
	[Kodu] DESC,
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[IlKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs5]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [nUVKs5] ON [dbo].[UVK]
(
	[Adi] ASC,
	[Kodu] ASC,
	[UlkeKodu] ASC,
	[UlkeNumKodu] ASC,
	[IlKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [nUVKs5d]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [nUVKs5d] ON [dbo].[UVK]
(
	[Adi] DESC,
	[Kodu] DESC,
	[UlkeKodu] DESC,
	[UlkeNumKodu] DESC,
	[IlKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkUVKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkUVKd] ON [dbo].[UVK]
(
	[UlkeNumKodu] DESC,
	[IlKodu] DESC,
	[Kodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkVERSION_INFOd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkVERSION_INFOd] ON [dbo].[VERSION_INFO]
(
	[TABLE_NAME] DESC,
	[VERSION_ID] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkYCTd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkYCTd] ON [dbo].[YCT]
(
	[BlgKulAdi] DESC,
	[SiraNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [pkYKKd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkYKKd] ON [dbo].[YKK]
(
	[KullaniciKodu] DESC,
	[KasiyerKodu] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [pkYTGd]    Script Date: 11/04/2018 11:20:02 ******/
CREATE UNIQUE NONCLUSTERED INDEX [pkYTGd] ON [dbo].[YTG]
(
	[Yil] DESC,
	[Tarih] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[APPNAMES] ADD  DEFAULT ((0)) FOR [ID]
GO
ALTER TABLE [dbo].[APPNAMES] ADD  DEFAULT (' ') FOR [KOD]
GO
ALTER TABLE [dbo].[APPNAMES] ADD  DEFAULT (' ') FOR [APP_PATTERN]
GO
ALTER TABLE [dbo].[APPNAMES] ADD  DEFAULT (' ') FOR [ACIKLAMA]
GO
ALTER TABLE [dbo].[APPNAMES] ADD  DEFAULT (' ') FOR [INSTALLATION_PATH]
GO
ALTER TABLE [dbo].[aspnet_Applications] ADD  CONSTRAINT [DF_AppI]  DEFAULT (newid()) FOR [ApplicationId]
GO
ALTER TABLE [dbo].[aspnet_Membership] ADD  CONSTRAINT [DF_MemI]  DEFAULT ((0)) FOR [PasswordFormat]
GO
ALTER TABLE [dbo].[aspnet_Paths] ADD  CONSTRAINT [DF_PathI]  DEFAULT (newid()) FOR [PathId]
GO
ALTER TABLE [dbo].[aspnet_PersonalizationPerUser] ADD  CONSTRAINT [DF_PerUsI]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[aspnet_Roles] ADD  CONSTRAINT [DF_RoleI]  DEFAULT (newid()) FOR [RoleId]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF_UserI]  DEFAULT (newid()) FOR [UserId]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF_UserII]  DEFAULT (NULL) FOR [MobileAlias]
GO
ALTER TABLE [dbo].[aspnet_Users] ADD  CONSTRAINT [DF_UserIII]  DEFAULT ((0)) FOR [IsAnonymous]
GO
ALTER TABLE [dbo].[BilFisAkt_Users] ADD  CONSTRAINT [DF_BilFisAkt_Users_Grup]  DEFAULT ('User') FOR [Grup]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((-1)) FOR [SirDKod]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [Kullanici]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((0)) FOR [MsgID]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [TanimAdi]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((0)) FOR [SiraNo]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [Tanim]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [FiltreAdi]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[BTT] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT (' ') FOR [CLMNAME]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((1)) FOR [CLMVISIBLE]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((0)) FOR [CLMALIGNMENT]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((0)) FOR [CLMF9]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((0)) FOR [CLMF10]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((1)) FOR [CLMALIASUSERDEFINED]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((1)) FOR [CLMREF_FILTER]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((0)) FOR [CLMTYPE]
GO
ALTER TABLE [dbo].[COLUMN_NAME] ADD  DEFAULT ((1)) FOR [CLMREF_CHECKINTEGRITY]
GO
ALTER TABLE [dbo].[COMBO_NAME] ADD  DEFAULT ((0)) FOR [CBOTYPECOLUMNVALUE]
GO
ALTER TABLE [dbo].[COMBO_NAME] ADD  DEFAULT ((0)) FOR [CBOTYPECOLUMNID]
GO
ALTER TABLE [dbo].[COMBO_NAME] ADD  DEFAULT (' ') FOR [CBONAME]
GO
ALTER TABLE [dbo].[COMBO_NAME] ADD  DEFAULT ((1)) FOR [CBOVISIBLE]
GO
ALTER TABLE [dbo].[COMBOITEM_NAME] ADD  DEFAULT ((1)) FOR [ITEMNAME]
GO
ALTER TABLE [dbo].[COMBOITEM_NAME] ADD  DEFAULT ((1)) FOR [ITEMVISIBLE]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [DriverName]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry0]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value0]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry1]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value1]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry2]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value2]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry3]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value3]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry4]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value4]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry5]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value5]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry6]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value6]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry7]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value7]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry8]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value8]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Entry9]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Value9]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[DRV] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[DTK] ADD  DEFAULT ((0)) FOR [AppID]
GO
ALTER TABLE [dbo].[DTK] ADD  DEFAULT ((-1)) FOR [SirDKod]
GO
ALTER TABLE [dbo].[DTK] ADD  DEFAULT ((-1)) FOR [Kullanici]
GO
ALTER TABLE [dbo].[DTK] ADD  DEFAULT (' ') FOR [MySection]
GO
ALTER TABLE [dbo].[DTK] ADD  DEFAULT (' ') FOR [MyEntry]
GO
ALTER TABLE [dbo].[DTK] ADD  DEFAULT ((0)) FOR [SiraNo]
GO
ALTER TABLE [dbo].[DTK] ADD  DEFAULT (' ') FOR [MyValue]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT (' ') FOR [DovizCinsi]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Tarih]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur00]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur01]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur02]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur03]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur04]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur05]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur06]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur07]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur08]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [Kur09]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[DVZ] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((0)) FOR [Tarih]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((0)) FOR [Oran]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EAO] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EAT] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((0)) FOR [YilAy]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((0)) FOR [Oran]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EBF] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [SirketKod]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [MaliYil]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [MaliBasAy]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [MhsSdkKod]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [MhsSirKod]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [FnstSdkKod]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [FnstSirKod]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [Stok_Duz_Yontem]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [Stok_Ard_Yontem]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [Gelir_Duz_Yontem]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [Gelir_Ard_Yontem]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [Kredi_Ard_Yontem]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [Vadeli_Ard_Yontem]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [KayitTuru]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [SirketBasTarih]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [EnfDuzYontem]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EDA] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Yil]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT (' ') FOR [DovizCinsi]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Ocak]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Subat]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Mart]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Nisan]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Mayis]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Haziran]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Temmuz]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Agustos]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Eylul]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Ekim]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Kasim]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [Aralik]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EDK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Yil]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran1]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran2]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran3]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran4]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran5]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran6]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran7]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran8]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran9]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran10]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran11]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [Oran12]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EKF] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT (' ') FOR [Kullanici]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT (' ') FOR [GonderenEPosta]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT (' ') FOR [GonderenAdi]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT (' ') FOR [Sunucu]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT ((0)) FOR [Port]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT (' ') FOR [KullaniciAdi]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT (' ') FOR [Sifre]
GO
ALTER TABLE [dbo].[EML] ADD  DEFAULT ((0)) FOR [SSL]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT (' ') FOR [HesapKod]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((-1)) FOR [KiymetTur]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((-1)) FOR [OzelKiyTuruTan]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((-1)) FOR [ReelOlmynFinMal]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((-1)) FOR [EnfDuz_Secimi]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((-1)) FOR [EnfDuz_EsasTarih_Secimi]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EPK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_SendMail]  DEFAULT ((0)) FOR [SendMail]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Yil]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran1]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran2]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran3]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran4]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran5]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran6]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran7]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran8]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran9]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran10]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran11]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [Oran12]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[ETF] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Sirket]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Kullanici]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [SablonAdi]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [SablonTipi]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [TableID]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [FieldID]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [EslemeTip]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [KolonID]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [OzelSecim]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [OzelTip]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [ExtDeger]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [LogTut]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [Guncelle]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [ExlBasSatir]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [ExlTplSatir]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Kod1]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Kod2]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Kod3]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Kod4]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Kod5]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [ExtTableID]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT ((-1)) FOR [ExtFieldID]
GO
ALTER TABLE [dbo].[EVT] ADD  DEFAULT (' ') FOR [HesapKodu]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Yil]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran1]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran2]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran3]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran4]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran5]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran6]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran7]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran8]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran9]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran10]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran11]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [Oran12]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[EYK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [YilAy]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((-1)) FOR [FormTip]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((-1)) FOR [DetayTip]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [SiraNo]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [Unvan]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [UlkeNumKod]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [VergiKod]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [BelgeAdet]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [KDVMatrah]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[FAS] ADD  DEFAULT (' ') FOR [UnvanOrj]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((0)) FOR [YilAy]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((-1)) FOR [FormTip]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket1]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket2]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket3]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket4]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket5]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket6]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket7]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket8]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket9]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KonsSirket10]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((0)) FOR [SatirFlag]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[FBH] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT (' ') FOR [GumrukIdareKodu]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT (' ') FOR [GumrukIdareAdi]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[GIK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[GROUP_NAME] ADD  DEFAULT (' ') FOR [GRPNAME]
GO
ALTER TABLE [dbo].[INI] ADD  DEFAULT ((-1)) FOR [MySection]
GO
ALTER TABLE [dbo].[INI] ADD  DEFAULT ((-1)) FOR [MyEntry]
GO
ALTER TABLE [dbo].[INI] ADD  DEFAULT ((-1)) FOR [MyValue]
GO
ALTER TABLE [dbo].[IZR_Izin] ADD  CONSTRAINT [DF_IZR_Izin_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [dbo].[IZR_Izin] ADD  CONSTRAINT [DF_IZR_Izin_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] ADD  CONSTRAINT [DF_IZR_KullaniciIzin_Kaydetme]  DEFAULT ((0)) FOR [Kaydetme]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] ADD  CONSTRAINT [DF_IZR_KullaniciIzin_Guncelleme]  DEFAULT ((0)) FOR [Guncelleme]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] ADD  CONSTRAINT [DF_IZR_KullaniciIzin_Silme]  DEFAULT ((0)) FOR [Silme]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] ADD  CONSTRAINT [DF_IZR_KullaniciIzin_Onay]  DEFAULT ((0)) FOR [Onay]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] ADD  CONSTRAINT [DF_IZR_KullaniciIzin_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] ADD  CONSTRAINT [DF_IZR_KullaniciIzin_DegisTarih]  DEFAULT (getdate()) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[IZR_Rol] ADD  CONSTRAINT [DF_IZR_Rol_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[IZR_Rol] ADD  CONSTRAINT [DF_IZR_Rol_DegisTarih]  DEFAULT (getdate()) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[IZR_RolIzin] ADD  CONSTRAINT [DF_IZR_RolIzin_Kaydetme]  DEFAULT ((0)) FOR [Kaydetme]
GO
ALTER TABLE [dbo].[IZR_RolIzin] ADD  CONSTRAINT [DF_IZR_RolIzin_Guncelleme]  DEFAULT ((0)) FOR [Guncelleme]
GO
ALTER TABLE [dbo].[IZR_RolIzin] ADD  CONSTRAINT [DF_IZR_RolIzin_Silme]  DEFAULT ((0)) FOR [Silme]
GO
ALTER TABLE [dbo].[IZR_RolIzin] ADD  CONSTRAINT [DF_IZR_RolIzin_Onay]  DEFAULT ((0)) FOR [Onay]
GO
ALTER TABLE [dbo].[IZR_RolIzin] ADD  CONSTRAINT [DF_IZR_RolIzin_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[IZR_RolIzin] ADD  CONSTRAINT [DF_IZR_RolIzin_DegisTarih]  DEFAULT (getdate()) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT (' ') FOR [TipAdi]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT (' ') FOR [TipAdiEN]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT ((-1)) FOR [RefTip]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT (' ') FOR [KaynakKod]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT (' ') FOR [KaynakKodAciklama]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT (' ') FOR [ErekKod]
GO
ALTER TABLE [dbo].[KEK] ADD  DEFAULT (' ') FOR [ErekKodAciklama]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [LoginID]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [EskiSifre]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [YeniSifre]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[KPD] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT (' ') FOR [KullaniciKodu]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT ((-1)) FOR [KullaniciTipi]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT (' ') FOR [SirketKodu]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT (' ') FOR [DonemKodu]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT ((-1)) FOR [DonemTipi]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT ((0)) FOR [DonemTarihi]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT (' ') FOR [SirketKisaAdi]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT (' ') FOR [Driver]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT ((-1)) FOR [Log]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT (' ') FOR [LogFileName]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT (' ') FOR [Directory]
GO
ALTER TABLE [dbo].[KSD] ADD  DEFAULT ((-1)) FOR [AktifPasif]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((-1)) FOR [KynkEvrakTip]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [TabloNo]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R001]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R002]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R003]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R004]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R005]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R006]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R007]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R008]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R009]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R010]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R011]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R012]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R013]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R014]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R015]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R016]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R017]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R018]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R019]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R020]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R021]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R022]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R023]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R024]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R025]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R026]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R027]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R028]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R029]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R030]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R031]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R032]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R033]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R034]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R035]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R036]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R037]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R038]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R039]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R040]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R041]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R042]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R043]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R044]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R045]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R046]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R047]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R048]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R049]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R050]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R051]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R052]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R053]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R054]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R055]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R056]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R057]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R058]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R059]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R060]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R061]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R062]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R063]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R064]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R065]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R066]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R067]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R068]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R069]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R070]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R071]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R072]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R073]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R074]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R075]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R076]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R077]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R078]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R079]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R080]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R081]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R082]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R083]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R084]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R085]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R086]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R087]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R088]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R089]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R090]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R091]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R092]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R093]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R094]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R095]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R096]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R097]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R098]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R099]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [R100]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Sil]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod1]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod2]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod3]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod4]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod5]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT (' ') FOR [Kod6]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod7]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod8]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod9]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod10]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod11]
GO
ALTER TABLE [dbo].[KST] ADD  DEFAULT ((0)) FOR [Kod12]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [Aciklama]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [GrupKod]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((0)) FOR [SayKod]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT (' ') FOR [Aciklama2]
GO
ALTER TABLE [dbo].[KTK] ADD  DEFAULT ((-1)) FOR [Tip2]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[LAR] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[LCK] ADD  DEFAULT ((0)) FOR [SeriKey]
GO
ALTER TABLE [dbo].[LRG] ADD  DEFAULT ((-1)) FOR [RKey]
GO
ALTER TABLE [dbo].[LRG] ADD  DEFAULT ((-1)) FOR [REntry]
GO
ALTER TABLE [dbo].[LRG] ADD  DEFAULT ((-1)) FOR [RValue]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT ((-1)) FOR [TanimTipi]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT ((0)) FOR [VeriTipi]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [VergiKimlikNo]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [TCKimlikNo]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [Unvan1]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [Unvan2]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [TicSicilNo]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [EPosta]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [TelAlanKodu]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [TelNumarasi]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [VergiDairesi]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT ((0)) FOR [Sifat]
GO
ALTER TABLE [dbo].[MTK] ADD  DEFAULT (' ') FOR [MersisNo]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [OnaySemaKod]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [SiraNo]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [GroupUser]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT (' ') FOR [UserKod]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT (' ') FOR [Aciklama]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT (' ') FOR [Bilgi]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [TimeOut]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [TimeOutType]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [StatusMsg]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [Skipping]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [UpdateOption]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [GecerlilikSuresiBas]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [GecerlilikSuresiBit]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((-1)) FOR [TimeLimitType]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [GecSuresiBas]
GO
ALTER TABLE [dbo].[OAS] ADD  DEFAULT ((0)) FOR [GecSuresiBit]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [SirketKod]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((-1)) FOR [EvrakTip]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [Adi]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((-1)) FOR [IslemTur]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [Filtre]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [OnayKul]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((0)) FOR [BekSuresi1]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((-1)) FOR [BekDilim1]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [AltKul1]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((0)) FOR [BekSuresi2]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((-1)) FOR [BekDilim2]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [AltKul2]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((-1)) FOR [KayitTuru]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[OFS] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[Onikim_Terminal] ADD  CONSTRAINT [DF_Onikim_Terminal_IslemTip]  DEFAULT ((1)) FOR [IslemTip]
GO
ALTER TABLE [dbo].[Onikim_Terminal] ADD  CONSTRAINT [DF_Onikim_Terminal_AktarimDurum]  DEFAULT ((0)) FOR [AktarimDurum]
GO
ALTER TABLE [dbo].[Onikim_Terminal] ADD  CONSTRAINT [DF_Onikim_Terminal_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((-1)) FOR [AktifPasif]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Ad]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Soyad]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [TCKimlikNo]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((0)) FOR [DogumTarih]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((-1)) FOR [Cinsiyet]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((-1)) FOR [MedeniHal]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((0)) FOR [EvlilikTarih]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [ePosta]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon1UlkeKod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon1BolgeKod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon1]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon1Dahili]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon2UlkeKod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon2BolgeKod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon2]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Telefon2Dahili]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Adres1]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Adres2]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Adres3]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [PostaKod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [GorevKod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Resim]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((-1)) FOR [KayitTuru]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[OPK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [Kullanici]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((0)) FOR [ProgramID]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [DosyaAdi]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [DosyaDizini]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [Parametre]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [Ekstralar]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[OPT] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Sifre]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup01]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup02]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup03]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup04]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup05]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup06]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup07]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup08]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup09]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Grup10]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [EskiSifre1]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [EskiSifre2]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [EskiSifre3]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((0)) FOR [SDTarih]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((-1)) FOR [BlkDurumu]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Kod1]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Kod2]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT (' ') FOR [Kod3]
GO
ALTER TABLE [dbo].[PWD] ADD  DEFAULT ((0)) FOR [eDefterYetkiSeviye]
GO
ALTER TABLE [dbo].[PWD01] ADD  CONSTRAINT [DF_PWD01_Tip]  DEFAULT ((0)) FOR [Tip]
GO
ALTER TABLE [dbo].[PWD01] ADD  CONSTRAINT [DF_PWD01_Admin]  DEFAULT ((0)) FOR [Admin]
GO
ALTER TABLE [dbo].[PWD01] ADD  CONSTRAINT [DF_PWD01_Soyad]  DEFAULT (' ') FOR [Soyad]
GO
ALTER TABLE [dbo].[PWD01] ADD  CONSTRAINT [DF_PWD01_Aciklama]  DEFAULT (' ') FOR [Aciklama]
GO
ALTER TABLE [dbo].[PWD01] ADD  CONSTRAINT [DF_PWD01_Email]  DEFAULT (' ') FOR [Email]
GO
ALTER TABLE [dbo].[PWD01] ADD  CONSTRAINT [DF_PWD01_Sifre]  DEFAULT (' ') FOR [Sifre]
GO
ALTER TABLE [dbo].[PWD01] ADD  CONSTRAINT [DF_PWD01_AktifPasif]  DEFAULT ((0)) FOR [AktifPasif]
GO
ALTER TABLE [dbo].[RES] ADD  DEFAULT ((0)) FOR [ID]
GO
ALTER TABLE [dbo].[RES] ADD  DEFAULT ((0)) FOR [RESTYPE]
GO
ALTER TABLE [dbo].[RES] ADD  DEFAULT ((0)) FOR [RESKEY]
GO
ALTER TABLE [dbo].[RES] ADD  DEFAULT ((0)) FOR [RESGROUP]
GO
ALTER TABLE [dbo].[RES] ADD  DEFAULT (' ') FOR [RESNAME]
GO
ALTER TABLE [dbo].[RES] ADD  DEFAULT (' ') FOR [RESNAME_LNG1]
GO
ALTER TABLE [dbo].[RES] ADD  DEFAULT (' ') FOR [RESNAME_LNG2]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((0)) FOR [RightType]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((0)) FOR [Kod1]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT (' ') FOR [Kod2]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[RGT] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[RPK] ADD  DEFAULT ((-1)) FOR [SirketKod]
GO
ALTER TABLE [dbo].[RPK] ADD  DEFAULT ((-1)) FOR [Kullanici]
GO
ALTER TABLE [dbo].[RPK] ADD  DEFAULT (' ') FOR [DosyaAdi]
GO
ALTER TABLE [dbo].[RPK] ADD  DEFAULT (' ') FOR [MySection]
GO
ALTER TABLE [dbo].[RPK] ADD  DEFAULT (' ') FOR [MyEntry]
GO
ALTER TABLE [dbo].[RPK] ADD  DEFAULT ((-1)) FOR [MyValue]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [DosyaAdi]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [DosyaDizini]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((0)) FOR [MainTable]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((-1)) FOR [RaporTuru]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((0)) FOR [Company]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [RaporAdi]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [Addon]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((-1)) FOR [ParaBrm]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[RPR] ADD  DEFAULT ((-1)) FOR [CiktiTuru]
GO
ALTER TABLE [dbo].[RTK] ADD  DEFAULT ((-1)) FOR [SSection]
GO
ALTER TABLE [dbo].[RTK] ADD  DEFAULT (' ') FOR [SEntry]
GO
ALTER TABLE [dbo].[RTK] ADD  DEFAULT (' ') FOR [SValue]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [SirketKod]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((0)) FOR [Tarih]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [Driver]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((-1)) FOR [Log]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [LogFileName]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [Directory]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((-1)) FOR [TLTip]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [DataFolder]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[SDK] ADD  DEFAULT ((-1)) FOR [LogPeriyod]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [SirketKod]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Kod]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Unvan]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [EfatEtiketGB]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [EfatEtiketPK]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Adres1]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Adres2]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Adres3]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [CaddeSokak]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [BinaAdi]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [BinaNo]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [KapiNo]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [KasabaKoy]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Ilce]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Sehir]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [PostaKodu]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [UlkeKodu]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [Logo]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT ((-1)) FOR [FaturadaKullan]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [TicSicilNo]
GO
ALTER TABLE [dbo].[SGB] ADD  DEFAULT (' ') FOR [MersisNo]
GO
ALTER TABLE [dbo].[SIN] ADD  DEFAULT (' ') FOR [SSection]
GO
ALTER TABLE [dbo].[SIN] ADD  DEFAULT (' ') FOR [SEntry]
GO
ALTER TABLE [dbo].[SIN] ADD  DEFAULT (' ') FOR [SValue]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_AktifPasif]  DEFAULT ((-1)) FOR [AktifPasif]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_Cadde]  DEFAULT (' ') FOR [Cadde]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_BinaAdi]  DEFAULT (' ') FOR [BinaAdi]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_DisKapiNo]  DEFAULT (' ') FOR [DisKapiNo]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_IcKapiNo]  DEFAULT (' ') FOR [IcKapiNo]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_KasabaKoy]  DEFAULT (' ') FOR [KasabaKoy]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_Ilce]  DEFAULT (' ') FOR [Ilce]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_Sehir]  DEFAULT (' ') FOR [Sehir]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_PostaKodu]  DEFAULT (' ') FOR [PostaKodu]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_UlkeNumKod]  DEFAULT (' ') FOR [UlkeNumKod]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_WebAdres]  DEFAULT (' ') FOR [WebAdres]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_EMail]  DEFAULT (' ') FOR [EMail]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_EfatEtiketGB]  DEFAULT (' ') FOR [EfatEtiketGB]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_EfatEtiketPK]  DEFAULT (' ') FOR [EfatEtiketPK]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_EfatEtiketTip]  DEFAULT ((-1)) FOR [EfatEtiketTip]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_IsletmeMerkezi]  DEFAULT (' ') FOR [IsletmeMerkezi]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_Ad]  DEFAULT (' ') FOR [Ad]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_SoyAd]  DEFAULT (' ') FOR [SoyAd]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MersisNo]  DEFAULT (' ') FOR [MersisNo]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_Logo]  DEFAULT (' ') FOR [Logo]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_FaturadaKullan]  DEFAULT ((-1)) FOR [FaturadaKullan]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzimVar]  DEFAULT ((-1)) FOR [MrkzimVar]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzUnvan1]  DEFAULT (' ') FOR [MrkzUnvan1]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzUnvan2]  DEFAULT (' ') FOR [MrkzUnvan2]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzCadde]  DEFAULT (' ') FOR [MrkzCadde]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzBinaAdi]  DEFAULT (' ') FOR [MrkzBinaAdi]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzDisKapiNo]  DEFAULT (' ') FOR [MrkzDisKapiNo]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzIcKapiNo]  DEFAULT (' ') FOR [MrkzIcKapiNo]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzKasabaKoy]  DEFAULT (' ') FOR [MrkzKasabaKoy]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzIlce]  DEFAULT (' ') FOR [MrkzIlce]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzSehir]  DEFAULT (' ') FOR [MrkzSehir]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzPostaKodu]  DEFAULT (' ') FOR [MrkzPostaKodu]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzUlkeNumKod]  DEFAULT (' ') FOR [MrkzUlkeNumKod]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzWebAdres]  DEFAULT (' ') FOR [MrkzWebAdres]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzEMail]  DEFAULT (' ') FOR [MrkzEMail]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzFax1]  DEFAULT (' ') FOR [MrkzFax1]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_MrkzTelefon1]  DEFAULT (' ') FOR [MrkzTelefon1]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_YerelParaBrm]  DEFAULT (' ') FOR [YerelParaBrm]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_EArsivKullan]  DEFAULT ((0)) FOR [EArsivKullan]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_IhracatEFatGecisTarih]  DEFAULT ((0)) FOR [IhracatEFatGecisTarih]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_IhracatGeciciKagitFat]  DEFAULT ((0)) FOR [IhracatGeciciKagitFat]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_YolcuBerEFatGecisTarih]  DEFAULT ((0)) FOR [YolcuBerEFatGecisTarih]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_YolcuBerGeciciKagitFat]  DEFAULT ((0)) FOR [YolcuBerGeciciKagitFat]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_KEPAdresi]  DEFAULT (' ') FOR [KEPAdresi]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_SGKFatura]  DEFAULT ((0)) FOR [SGKFatura]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_eIrsGecisTarih]  DEFAULT ((0)) FOR [eIrsGecisTarih]
GO
ALTER TABLE [dbo].[SIR] ADD  CONSTRAINT [DF_SIR_eIrsGeciciKagitIrsaliye]  DEFAULT ((0)) FOR [eIrsGeciciKagitIrsaliye]
GO
ALTER TABLE [dbo].[SiparisINI] ADD  CONSTRAINT [DF_SiparisINI_GosterilecekDepo]  DEFAULT ('') FOR [GosterilecekDepo]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((-1)) FOR [SirKod]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [Kullanici]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [ID]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [Dizin]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [KisayolAdi]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((-1)) FOR [KisayolTipi]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [ArrType]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [SolarType]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[SKT] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[TABLE_NAME] ADD  DEFAULT (' ') FOR [TBLNAME]
GO
ALTER TABLE [dbo].[TABLE_NAME] ADD  DEFAULT ((1)) FOR [TBLVISIBLE]
GO
ALTER TABLE [dbo].[TABLE_NAME] ADD  DEFAULT ((0)) FOR [TBLKOD]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT (' ') FOR [KullaniciKodu]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((-1)) FOR [EvrakTipi]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT (' ') FOR [SirketKodu]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT (' ') FOR [DonemKodu]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((-1)) FOR [DonemTipi]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((0)) FOR [DonemTarihi]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((-1)) FOR [KisitTipi]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((0)) FOR [Gun]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((0)) FOR [BasTarih]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((0)) FOR [BitTarih]
GO
ALTER TABLE [dbo].[TBK] ADD  DEFAULT ((0)) FOR [Tip]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT (' ') FOR [FormulAdi]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT ((0)) FOR [SiraNo]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT (' ') FOR [HesapKodu]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT ((0)) FOR [HesapSaha]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT ((-1)) FOR [Tip]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT ((-1)) FOR [Operator]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT ((0)) FOR [TabloID]
GO
ALTER TABLE [dbo].[TFT] ADD  DEFAULT ((0)) FOR [Sabit]
GO
ALTER TABLE [dbo].[TTK] ADD  DEFAULT ((-1)) FOR [SirDKod]
GO
ALTER TABLE [dbo].[TTK] ADD  DEFAULT ((-1)) FOR [Kullanici]
GO
ALTER TABLE [dbo].[TTK] ADD  DEFAULT ((-1)) FOR [MySection]
GO
ALTER TABLE [dbo].[TTK] ADD  DEFAULT ((-1)) FOR [MyEntry]
GO
ALTER TABLE [dbo].[TTK] ADD  DEFAULT ((-1)) FOR [MyValue]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [UlkeKodu]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [UlkeNumKodu]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [Kodu]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [Adi]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[UBK] ADD  DEFAULT (' ') FOR [SwiftKod]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [UlkeKodu]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [UlkeNumKodu]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [IlKodu]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [Kodu]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [Adi]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[UCK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [UlkeKodu]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [UlkeNumKodu]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [Kodu]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [Adi]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [TelKodu]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [TelKodu2]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[UIK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [UlkeKodu]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [UlkeNumKodu]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [BankaKodu]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [Kodu]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [IlKodu]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [Adi]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[USK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [Kodu]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [NumKodu]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [Adi]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [AdiEng]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [TelKodu]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[UUK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [UlkeKodu]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [UlkeNumKodu]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [IlKodu]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [Kodu]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [Adi]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[UVK] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[VERSION_INFO] ADD  DEFAULT ((0)) FOR [TARIH]
GO
ALTER TABLE [dbo].[VeziragacWebCEOUser] ADD  CONSTRAINT [DF_VeziragacWebCEOUser_InsertionTime]  DEFAULT (getdate()) FOR [InsertionTime]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_SatisBaglantiRaporu]  DEFAULT ((0)) FOR [SatisBaglantiRaporu]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_SatisCiroRaporu]  DEFAULT ((0)) FOR [SatisCiroRaporu]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_OdemeYapmayanMusteriler]  DEFAULT ((0)) FOR [OdemeYapmayanMusteriler]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_SatilmayanUrunler]  DEFAULT ((0)) FOR [SatilmayanUrunler]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_EnEskiTarihliStoklar]  DEFAULT ((0)) FOR [EnEskiTarihliStoklar]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_CekListesiRaporu]  DEFAULT ((0)) FOR [CekListesiRaporu]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_BakiyeRaporu]  DEFAULT ((0)) FOR [BakiyeRaporu]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_GunlukSatisRaporu]  DEFAULT ((0)) FOR [GunlukSatisRaporu]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_VadesiGelmemisCekler]  DEFAULT ((0)) FOR [VadesiGelmemisCekler]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_SozlesmeTanim]  DEFAULT ((0)) FOR [SozlesmeTanim]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_SatisMuduruOnay]  DEFAULT ((0)) FOR [SatisMuduruOnay]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_FinansmanMuduruOnay]  DEFAULT ((0)) FOR [FinansmanMuduruOnay]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_AdminPaneli]  DEFAULT ((0)) FOR [AdminPaneli]
GO
ALTER TABLE [dbo].[VKWRUsers] ADD  CONSTRAINT [DF_VKWRUsers_Izinler]  DEFAULT ((0)) FOR [Izinler]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_Rapor]  DEFAULT ((0)) FOR [PageRapor]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SatisBaglantiRaporu]  DEFAULT ((0)) FOR [RaporSatisBaglanti]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SatisCiroRaporu]  DEFAULT ((0)) FOR [RaporSatisCiro]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_OdemeYapmayanMusteriler]  DEFAULT ((0)) FOR [RaporOdemeYapmayanMusteriler]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SatilmayanUrunler]  DEFAULT ((0)) FOR [RaporSatilmayanUrunler]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_EnEskiUretimTarihliStoklar]  DEFAULT ((0)) FOR [RaporEnEskiUretimTarihliStoklar]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_CekListesiRaporu]  DEFAULT ((0)) FOR [RaporCekListesi]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_BakiyeRaporu]  DEFAULT ((0)) FOR [RaporBakiye]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_GunlukSatisRaporu]  DEFAULT ((0)) FOR [RaporGunlukSatis]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_VadesiGelmemisCekler]  DEFAULT ((0)) FOR [RaporVadesiGelmemisCekler]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_ToplamRiskBakiyesi]  DEFAULT ((0)) FOR [RaporToplamRiskBakiyesi]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RaporToplamRiskAnalizi]  DEFAULT ((0)) FOR [RaporToplamRiskAnalizi]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_GerceklesenSevkiyatPlaniRaporu]  DEFAULT ((0)) FOR [RaporGerceklesenSevkiyatPlani]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_BekleyenSiparisRaporu]  DEFAULT ((0)) FOR [RaporBekleyenSiparis]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_UretimAgaciRaporu]  DEFAULT ((0)) FOR [RaporUretimAgaci]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RaporGunlukUretim]  DEFAULT ((0)) FOR [RaporGunlukUretim]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RaporOnayArsivi]  DEFAULT ((0)) FOR [RaporOnayArsivi]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SiparisOnay]  DEFAULT ((0)) FOR [PageSiparis]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SiparisOnayA]  DEFAULT ((0)) FOR [SiparisOnaySM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SiparisOnayFinansmanMuduruA]  DEFAULT ((0)) FOR [SiparisOnayIM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SiparisOnayUstYonetimA]  DEFAULT ((0)) FOR [SiparisOnayGM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SozlesmeTanim]  DEFAULT ((0)) FOR [PageSozlesme]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SozlesmeTanimA]  DEFAULT ((0)) FOR [SozlesmeTanim]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_SatisMuduruOnay]  DEFAULT ((0)) FOR [SozlesmeOnaySM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_MaliIslerGenelMudurYardimcisiOnay]  DEFAULT ((0)) FOR [SozlesmeOnayIM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_GenelMudurOnay]  DEFAULT ((0)) FOR [SozlesmeOnayGM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RiskTanim]  DEFAULT ((0)) FOR [PageRisk]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RiskLimitTanim]  DEFAULT ((0)) FOR [RiskLimitTanim]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RiskTanimOnaySM]  DEFAULT ((0)) FOR [RiskOnaySM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RiskTanimOnaySPGMY]  DEFAULT ((0)) FOR [RiskOnaySPGMY]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RiskTanimOnayMIGMY]  DEFAULT ((0)) FOR [RiskOnayMIGMY]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_RiskTanimOnayGM]  DEFAULT ((0)) FOR [RiskOnayGM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_TeminatTanim]  DEFAULT ((0)) FOR [PageTeminat]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_TeminatGirisi]  DEFAULT ((0)) FOR [TeminatTanim]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_TeminatOnay]  DEFAULT ((0)) FOR [TeminatOnay]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_FiyatListesiTanim]  DEFAULT ((0)) FOR [PageFiyat]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_FiyatTanim]  DEFAULT ((0)) FOR [FiyatTanim]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_FiyatTanimOnay]  DEFAULT ((0)) FOR [FiyatOnaySM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_FiyatTanimOnaySPGMY]  DEFAULT ((0)) FOR [FiyatOnaySPGMY]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_FiyatTanimOnayGM]  DEFAULT ((0)) FOR [FiyatOnayGM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_CekOnay]  DEFAULT ((0)) FOR [PageCek]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_CekOnaySPGMY]  DEFAULT ((0)) FOR [CekOnaySPGMY]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_CekOnayMIGMY]  DEFAULT ((0)) FOR [CekOnayMIGMY]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_CekOnayGM]  DEFAULT ((0)) FOR [CekOnayGM]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_AdminPaneli]  DEFAULT ((0)) FOR [PageAdminPaneli]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_AdminPaneliA]  DEFAULT ((0)) FOR [AdminPaneli]
GO
ALTER TABLE [dbo].[WebPelitUsers] ADD  CONSTRAINT [DF_WebPelitUsers_Izinler]  DEFAULT ((0)) FOR [Izinler]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_Rapor]  DEFAULT ((0)) FOR [PageRapor]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatisBaglantiRaporu]  DEFAULT ((0)) FOR [RaporSatisBaglanti]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatisCiroRaporu]  DEFAULT ((0)) FOR [RaporSatisCiro]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_OdemeYapmayanMusteriler]  DEFAULT ((0)) FOR [RaporOdemeYapmayanMusteriler]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatilmayanUrunler]  DEFAULT ((0)) FOR [RaporSatilmayanUrunler]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_EnEskiUretimTarihliStoklar]  DEFAULT ((0)) FOR [RaporEnEskiUretimTarihliStoklar]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_CekListesiRaporu]  DEFAULT ((0)) FOR [RaporCekListesi]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_BakiyeRaporu]  DEFAULT ((0)) FOR [RaporBakiye]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_GunlukSatisRaporu]  DEFAULT ((0)) FOR [RaporGunlukSatis]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_VadesiGelmemisCekler]  DEFAULT ((0)) FOR [RaporVadesiGelmemisCekler]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_ToplamRiskBakiyesi]  DEFAULT ((0)) FOR [RaporToplamRiskBakiyesi]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RaporToplamRiskAnalizi]  DEFAULT ((0)) FOR [RaporToplamRiskAnalizi]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_GerceklesenSevkiyatPlaniRaporu]  DEFAULT ((0)) FOR [RaporGerceklesenSevkiyatPlani]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_BekleyenSiparisRaporu]  DEFAULT ((0)) FOR [RaporBekleyenSiparis]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_UretimAgaciRaporu]  DEFAULT ((0)) FOR [RaporUretimAgaci]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RaporGunlukUretim]  DEFAULT ((0)) FOR [RaporGunlukUretim]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RaporOnayArsivi]  DEFAULT ((0)) FOR [RaporOnayArsivi]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SiparisOnay]  DEFAULT ((0)) FOR [PageSiparis]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SiparisOnayA]  DEFAULT ((0)) FOR [SiparisOnaySM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SiparisOnayFinansmanMuduruA]  DEFAULT ((0)) FOR [SiparisOnaySPGMY]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SiparisOnayUstYonetimA]  DEFAULT ((0)) FOR [SiparisOnayGM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SozlesmeTanim]  DEFAULT ((0)) FOR [PageSozlesme]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SozlesmeTanimA]  DEFAULT ((0)) FOR [SozlesmeTanim]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatisMuduruOnay]  DEFAULT ((0)) FOR [SozlesmeOnaySM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_MaliIslerGenelMudurYardimcisiOnay]  DEFAULT ((0)) FOR [SozlesmeOnaySPGMY]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_GenelMudurOnay]  DEFAULT ((0)) FOR [SozlesmeOnayGM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RiskTanim]  DEFAULT ((0)) FOR [PageRisk]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RiskLimitTanim]  DEFAULT ((0)) FOR [RiskLimitTanim]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RiskTanimOnaySM]  DEFAULT ((0)) FOR [RiskOnaySM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RiskTanimOnaySPGMY]  DEFAULT ((0)) FOR [RiskOnaySPGMY]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RiskTanimOnayMIGMY]  DEFAULT ((0)) FOR [RiskOnayMIGMY]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_RiskTanimOnayGM]  DEFAULT ((0)) FOR [RiskOnayGM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_TeminatTanim]  DEFAULT ((0)) FOR [PageTeminat]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_TeminatGirisi]  DEFAULT ((0)) FOR [TeminatTanim]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_TeminatOnay]  DEFAULT ((0)) FOR [TeminatOnay]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_FiyatListesiTanim]  DEFAULT ((0)) FOR [PageFiyat]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_FiyatTanim]  DEFAULT ((0)) FOR [FiyatTanim]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_FiyatTanimOnay]  DEFAULT ((0)) FOR [FiyatOnaySM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_FiyatTanimOnaySPGMY]  DEFAULT ((0)) FOR [FiyatOnaySPGMY]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_FiyatTanimOnayGM]  DEFAULT ((0)) FOR [FiyatOnayGM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_CekOnay]  DEFAULT ((0)) FOR [PageCek]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_CekOnaySPGMY]  DEFAULT ((0)) FOR [CekOnaySPGMY]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_CekOnayMIGMY]  DEFAULT ((0)) FOR [CekOnayMIGMY]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_CekOnayGM]  DEFAULT ((0)) FOR [CekOnayGM]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_AdminPaneli]  DEFAULT ((0)) FOR [PageAdminPaneli]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_AdminPaneliA]  DEFAULT ((0)) FOR [AdminPaneli]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_Izinler]  DEFAULT ((0)) FOR [Izinler]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_PageIhaleTahsis]  DEFAULT ((0)) FOR [PageIhaleTahsis]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_TahsisliAlimOnay]  DEFAULT ((0)) FOR [TahsisliAlimOnay]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_TahsisliAlimList]  DEFAULT ((0)) FOR [TahsisliAlimList]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_TahsisliIsletmeKasa]  DEFAULT ((0)) FOR [TahsisliIsletmeKasa]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_IhaleliAlimOnay]  DEFAULT ((0)) FOR [IhaleliAlimOnay]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_IhaleliAlimList]  DEFAULT ((0)) FOR [IhaleliAlimList]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_IhaleliIsletmeKasa]  DEFAULT ((0)) FOR [IhaleliIsletmeKasa]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_NakliyeFiyatOnay]  DEFAULT ((0)) FOR [NakliyeFiyatOnay]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_NakliyeFiyatList]  DEFAULT ((0)) FOR [NakliyeFiyatList]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_DisDepoStokRapor]  DEFAULT ((0)) FOR [DisDepoStokRapor]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_DisDepoStokYaslandirma]  DEFAULT ((0)) FOR [DisDepoStokYaslandirma]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_DisDepoStokMaliyet]  DEFAULT ((0)) FOR [DisDepoStokMaliyet]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_PageSatinalma]  DEFAULT ((0)) FOR [PageSatinalma]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatinalmaSipTalepGMOnay]  DEFAULT ((0)) FOR [SatinalmaSipTalepGMOnay]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatinalmaTeklifList]  DEFAULT ((0)) FOR [SatinalmaTeklifList]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatinalmaOnayliTedList]  DEFAULT ((0)) FOR [SatinalmaOnayliTedList]
GO
ALTER TABLE [dbo].[WebUsers] ADD  CONSTRAINT [DF_WebUsers_SatinalmaTedSevkPerf]  DEFAULT ((0)) FOR [SatinalmaTedSevkPerf]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT (' ') FOR [BlgKulAdi]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT ((0)) FOR [SiraNo]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT ((-1)) FOR [Marka]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT (' ') FOR [Model]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT (' ') FOR [SeriNo]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT (' ') FOR [Sifre]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT ((-1)) FOR [Aktif]
GO
ALTER TABLE [dbo].[YCT] ADD  DEFAULT ((-1)) FOR [ZRaporTip]
GO
ALTER TABLE [dbo].[YKK] ADD  DEFAULT (' ') FOR [KullaniciKodu]
GO
ALTER TABLE [dbo].[YKK] ADD  DEFAULT (' ') FOR [KasiyerKodu]
GO
ALTER TABLE [dbo].[YKK] ADD  DEFAULT (' ') FOR [KasiyerSifre]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((0)) FOR [Yil]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((0)) FOR [Tarih]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT (' ') FOR [Aciklama]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT (' ') FOR [GuvenlikKod]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT (' ') FOR [Kaydeden]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((0)) FOR [KayitTarih]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((0)) FOR [KayitSaat]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((-1)) FOR [KayitKaynak]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT (' ') FOR [KayitSurum]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT (' ') FOR [Degistiren]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((0)) FOR [DegisTarih]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((0)) FOR [DegisSaat]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((-1)) FOR [DegisKaynak]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT (' ') FOR [DegisSurum]
GO
ALTER TABLE [dbo].[YTG] ADD  DEFAULT ((0)) FOR [CheckSum]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin]  WITH CHECK ADD  CONSTRAINT [FK_IZR_KullaniciIzin_IZR_Izin] FOREIGN KEY([Izin])
REFERENCES [dbo].[IZR_Izin] ([Izin])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] CHECK CONSTRAINT [FK_IZR_KullaniciIzin_IZR_Izin]
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin]  WITH CHECK ADD  CONSTRAINT [FK_IZR_KullaniciIzin_IZR_Kullanici] FOREIGN KEY([Kod])
REFERENCES [dbo].[PWD01] ([Kod])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IZR_KullaniciIzin] CHECK CONSTRAINT [FK_IZR_KullaniciIzin_IZR_Kullanici]
GO
ALTER TABLE [dbo].[IZR_RolIzin]  WITH CHECK ADD  CONSTRAINT [FK_IZR_RolIzin_IZR_Izin] FOREIGN KEY([Izin])
REFERENCES [dbo].[IZR_Izin] ([Izin])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IZR_RolIzin] CHECK CONSTRAINT [FK_IZR_RolIzin_IZR_Izin]
GO
/****** Object:  StoredProcedure [dbo].[aspnet_AnyDataInTables]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_AnyDataInTables]
    @TablesToCheck int
AS
BEGIN
    -- Check Membership table if (@TablesToCheck & 1) is set
    IF ((@TablesToCheck & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM aspnet_Membership))
        BEGIN
            SELECT N'aspnet_Membership'
            RETURN
        END
    END

    -- Check aspnet_Roles table if (@TablesToCheck & 2) is set
    IF ((@TablesToCheck & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Roles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 RoleId FROM aspnet_Roles))
        BEGIN
            SELECT N'aspnet_Roles'
            RETURN
        END
    END

    -- Check aspnet_Profile table if (@TablesToCheck & 4) is set
    IF ((@TablesToCheck & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM aspnet_Profile))
        BEGIN
            SELECT N'aspnet_Profile'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 8) is set
    IF ((@TablesToCheck & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM aspnet_PersonalizationPerUser))
        BEGIN
            SELECT N'aspnet_PersonalizationPerUser'
            RETURN
        END
    END

    -- Check aspnet_PersonalizationPerUser table if (@TablesToCheck & 16) is set
    IF ((@TablesToCheck & 16) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'aspnet_WebEvent_LogEvent') AND (type = 'P'))) )
    BEGIN
        IF (EXISTS(SELECT TOP 1 * FROM aspnet_WebEvent_Events))
        BEGIN
            SELECT N'aspnet_WebEvent_Events'
            RETURN
        END
    END

    -- Check aspnet_Users table if (@TablesToCheck & 1,2,4 & 8) are all set
    IF ((@TablesToCheck & 1) <> 0 AND
        (@TablesToCheck & 2) <> 0 AND
        (@TablesToCheck & 4) <> 0 AND
        (@TablesToCheck & 8) <> 0 AND
        (@TablesToCheck & 32) <> 0 AND
        (@TablesToCheck & 128) <> 0 AND
        (@TablesToCheck & 256) <> 0 AND
        (@TablesToCheck & 512) <> 0 AND
        (@TablesToCheck & 1024) <> 0)
    BEGIN
        IF (EXISTS(SELECT TOP 1 UserId FROM aspnet_Users))
        BEGIN
            SELECT N'aspnet_Users'
            RETURN
        END
        IF (EXISTS(SELECT TOP 1 ApplicationId FROM aspnet_Applications))
        BEGIN
            SELECT N'aspnet_Applications'
            RETURN
        END
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Applications_CreateApplication]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Applications_CreateApplication]
    @ApplicationName      nvarchar(256),
    @ApplicationId        uniqueidentifier OUTPUT
AS
BEGIN
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName

    IF(@ApplicationId IS NULL)
    BEGIN
        DECLARE @TranStarted   bit
        SET @TranStarted = 0

        IF( @@TRANCOUNT = 0 )
        BEGIN
	        BEGIN TRANSACTION
	        SET @TranStarted = 1
        END
        ELSE
    	    SET @TranStarted = 0

        SELECT  @ApplicationId = ApplicationId
        FROM aspnet_Applications WITH (UPDLOCK, HOLDLOCK)
        WHERE LOWER(@ApplicationName) = LoweredApplicationName

        IF(@ApplicationId IS NULL)
        BEGIN
            SELECT  @ApplicationId = NEWID()
            INSERT  aspnet_Applications (ApplicationId, ApplicationName, LoweredApplicationName)
            VALUES  (@ApplicationId, @ApplicationName, LOWER(@ApplicationName))
        END


        IF( @TranStarted = 1 )
        BEGIN
            IF(@@ERROR = 0)
            BEGIN
	        SET @TranStarted = 0
	        COMMIT TRANSACTION
            END
            ELSE
            BEGIN
                SET @TranStarted = 0
                ROLLBACK TRANSACTION
            END
        END
    END
END

GO
/****** Object:  StoredProcedure [dbo].[aspnet_CheckSchemaVersion]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_CheckSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    IF (EXISTS( SELECT  *
                FROM    aspnet_SchemaVersions
                WHERE   Feature = LOWER( @Feature ) AND
                        CompatibleSchemaVersion = @CompatibleSchemaVersion ))
        RETURN 0

    RETURN 1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ChangePasswordQuestionAndAnswer]
    @ApplicationName       nvarchar(256),
    @UserName              nvarchar(256),
    @NewPasswordQuestion   nvarchar(256),
    @NewPasswordAnswer     nvarchar(128)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    aspnet_Membership m, aspnet_Users u, aspnet_Applications a
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId
    IF (@UserId IS NULL)
    BEGIN
        RETURN(1)
    END

    UPDATE aspnet_Membership
    SET    PasswordQuestion = @NewPasswordQuestion, PasswordAnswer = @NewPasswordAnswer
    WHERE  UserId=@UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_CreateUser]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_CreateUser]
    @ApplicationName                        nvarchar(256),
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),
    @PasswordSalt                           nvarchar(128),
    @Email                                  nvarchar(256),
    @PasswordQuestion                       nvarchar(256),
    @PasswordAnswer                         nvarchar(128),
    @IsApproved                             bit,
    @CurrentTimeUtc                         datetime,
    @CreateDate                             datetime = NULL,
    @UniqueEmail                            int      = 0,
    @PasswordFormat                         int      = 0,
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @NewUserId uniqueidentifier
    SELECT @NewUserId = NULL

    DECLARE @IsLockedOut bit
    SET @IsLockedOut = 0

    DECLARE @LastLockoutDate  datetime
    SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAttemptCount int
    SET @FailedPasswordAttemptCount = 0

    DECLARE @FailedPasswordAttemptWindowStart  datetime
    SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @FailedPasswordAnswerAttemptCount int
    SET @FailedPasswordAnswerAttemptCount = 0

    DECLARE @FailedPasswordAnswerAttemptWindowStart  datetime
    SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )

    DECLARE @NewUserCreated bit
    DECLARE @ReturnValue   int
    SET @ReturnValue = 0

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    SET @CreateDate = @CurrentTimeUtc

    SELECT  @NewUserId = UserId FROM aspnet_Users WHERE LOWER(@UserName) = LoweredUserName AND @ApplicationId = ApplicationId
    IF ( @NewUserId IS NULL )
    BEGIN
        SET @NewUserId = @UserId
        EXEC @ReturnValue = aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CreateDate, @NewUserId OUTPUT
        SET @NewUserCreated = 1
    END
    ELSE
    BEGIN
        SET @NewUserCreated = 0
        IF( @NewUserId <> @UserId AND @UserId IS NOT NULL )
        BEGIN
            SET @ErrorCode = 6
            GOTO Cleanup
        END
    END

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @ReturnValue = -1 )
    BEGIN
        SET @ErrorCode = 10
        GOTO Cleanup
    END

    IF ( EXISTS ( SELECT UserId
                  FROM   aspnet_Membership
                  WHERE  @NewUserId = UserId ) )
    BEGIN
        SET @ErrorCode = 6
        GOTO Cleanup
    END

    SET @UserId = @NewUserId

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  aspnet_Membership m WITH ( UPDLOCK, HOLDLOCK )
                    WHERE ApplicationId = @ApplicationId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            SET @ErrorCode = 7
            GOTO Cleanup
        END
    END

    IF (@NewUserCreated = 0)
    BEGIN
        UPDATE aspnet_Users
        SET    LastActivityDate = @CreateDate
        WHERE  @UserId = UserId
        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    INSERT INTO aspnet_Membership
                ( ApplicationId,
                  UserId,
                  Password,
                  PasswordSalt,
                  Email,
                  LoweredEmail,
                  PasswordQuestion,
                  PasswordAnswer,
                  PasswordFormat,
                  IsApproved,
                  IsLockedOut,
                  CreateDate,
                  LastLoginDate,
                  LastPasswordChangedDate,
                  LastLockoutDate,
                  FailedPasswordAttemptCount,
                  FailedPasswordAttemptWindowStart,
                  FailedPasswordAnswerAttemptCount,
                  FailedPasswordAnswerAttemptWindowStart )
         VALUES ( @ApplicationId,
                  @UserId,
                  @Password,
                  @PasswordSalt,
                  @Email,
                  LOWER(@Email),
                  @PasswordQuestion,
                  @PasswordAnswer,
                  @PasswordFormat,
                  @IsApproved,
                  @IsLockedOut,
                  @CreateDate,
                  @CreateDate,
                  @CreateDate,
                  @LastLockoutDate,
                  @FailedPasswordAttemptCount,
                  @FailedPasswordAttemptWindowStart,
                  @FailedPasswordAnswerAttemptCount,
                  @FailedPasswordAnswerAttemptWindowStart )

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByEmail]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByEmail]
    @ApplicationName       nvarchar(256),
    @EmailToMatch          nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    IF( @EmailToMatch IS NULL )
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   aspnet_Users u, aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.Email IS NULL
            ORDER BY m.LoweredEmail
    ELSE
        INSERT INTO #PageIndexForUsers (UserId)
            SELECT u.UserId
            FROM   aspnet_Users u, aspnet_Membership m
            WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND m.LoweredEmail LIKE LOWER(@EmailToMatch)
            ORDER BY m.LoweredEmail

    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   aspnet_Membership m, aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId = @PageLowerBound AND p.IndexId = @PageUpperBound
    ORDER BY m.LoweredEmail

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_FindUsersByName]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_FindUsersByName]
    @ApplicationName       nvarchar(256),
    @UserNameToMatch       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT u.UserId
        FROM   aspnet_Users u, aspnet_Membership m
        WHERE  u.ApplicationId = @ApplicationId AND m.UserId = u.UserId AND u.LoweredUserName LIKE LOWER(@UserNameToMatch)
        ORDER BY u.UserName


    SELECT  u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   aspnet_Membership m, aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName

    SELECT  @TotalRecords = COUNT(*)
    FROM    #PageIndexForUsers
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetAllUsers]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetAllUsers]
    @ApplicationName       nvarchar(256),
    @PageIndex             int,
    @PageSize              int
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN 0


    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
    SELECT u.UserId
    FROM   aspnet_Membership m, aspnet_Users u
    WHERE  u.ApplicationId = @ApplicationId AND u.UserId = m.UserId
    ORDER BY u.UserName

    SELECT @TotalRecords = @@ROWCOUNT

    SELECT u.UserName, m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate,
            m.LastLoginDate,
            u.LastActivityDate,
            m.LastPasswordChangedDate,
            u.UserId, m.IsLockedOut,
            m.LastLockoutDate
    FROM   aspnet_Membership m, aspnet_Users u, #PageIndexForUsers p
    WHERE  u.UserId = p.UserId AND u.UserId = m.UserId AND
           p.IndexId >= @PageLowerBound AND p.IndexId <= @PageUpperBound
    ORDER BY u.UserName
    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetNumberOfUsersOnline]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetNumberOfUsersOnline]
    @ApplicationName            nvarchar(256),
    @MinutesSinceLastInActive   int,
    @CurrentTimeUtc             datetime
AS
BEGIN
    DECLARE @DateActive datetime
    SELECT  @DateActive = DATEADD(minute,  -(@MinutesSinceLastInActive), @CurrentTimeUtc)

    DECLARE @NumOnline int
    SELECT  @NumOnline = COUNT(*)
    FROM    aspnet_Users u(NOLOCK),
            aspnet_Applications a(NOLOCK),
            aspnet_Membership m(NOLOCK)
    WHERE   u.ApplicationId = a.ApplicationId                  AND
            LastActivityDate > @DateActive                     AND
            a.LoweredApplicationName = LOWER(@ApplicationName) AND
            u.UserId = m.UserId
    RETURN(@NumOnline)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPassword]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPassword]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @PasswordAnswer                 nvarchar(128) = NULL
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @PasswordFormat                         int
    DECLARE @Password                               nvarchar(128)
    DECLARE @passAns                                nvarchar(128)
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @Password = m.Password,
            @passAns = m.PasswordAnswer,
            @PasswordFormat = m.PasswordFormat,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    aspnet_Applications a, aspnet_Users u, aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    IF ( NOT( @PasswordAnswer IS NULL ) )
    BEGIN
        IF( ( @passAns IS NULL ) OR ( LOWER( @passAns ) <> LOWER( @PasswordAnswer ) ) )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
        ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

        UPDATE aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    IF( @ErrorCode = 0 )
        SELECT @Password, @PasswordFormat

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetPasswordWithFormat]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetPasswordWithFormat]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @UpdateLastLoginActivityDate    bit,
    @CurrentTimeUtc                 datetime
AS
BEGIN
    DECLARE @IsLockedOut                        bit
    DECLARE @UserId                             uniqueidentifier
    DECLARE @Password                           nvarchar(128)
    DECLARE @PasswordSalt                       nvarchar(128)
    DECLARE @PasswordFormat                     int
    DECLARE @FailedPasswordAttemptCount         int
    DECLARE @FailedPasswordAnswerAttemptCount   int
    DECLARE @IsApproved                         bit
    DECLARE @LastActivityDate                   datetime
    DECLARE @LastLoginDate                      datetime

    SELECT  @UserId          = NULL

    SELECT  @UserId = u.UserId, @IsLockedOut = m.IsLockedOut, @Password=Password, @PasswordFormat=PasswordFormat,
            @PasswordSalt=PasswordSalt, @FailedPasswordAttemptCount=FailedPasswordAttemptCount,
		    @FailedPasswordAnswerAttemptCount=FailedPasswordAnswerAttemptCount, @IsApproved=IsApproved,
            @LastActivityDate = LastActivityDate, @LastLoginDate = LastLoginDate
    FROM    aspnet_Applications a, aspnet_Users u, aspnet_Membership m
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF (@UserId IS NULL)
        RETURN 1

    IF (@IsLockedOut = 1)
        RETURN 99

    SELECT   @Password, @PasswordFormat, @PasswordSalt, @FailedPasswordAttemptCount,
             @FailedPasswordAnswerAttemptCount, @IsApproved, @LastLoginDate, @LastActivityDate

    IF (@UpdateLastLoginActivityDate = 1 AND @IsApproved = 1)
    BEGIN
        UPDATE  aspnet_Membership
        SET     LastLoginDate = @CurrentTimeUtc
        WHERE   UserId = @UserId

        UPDATE  aspnet_Users
        SET     LastActivityDate = @CurrentTimeUtc
        WHERE   @UserId = UserId
    END


    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByEmail]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByEmail]
    @ApplicationName  nvarchar(256),
    @Email            nvarchar(256)
AS
BEGIN
    IF( @Email IS NULL )
        SELECT  u.UserName
        FROM    aspnet_Applications a, aspnet_Users u, aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                m.LoweredEmail IS NULL
    ELSE
        SELECT  u.UserName
        FROM    aspnet_Applications a, aspnet_Users u, aspnet_Membership m
        WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                u.UserId = m.UserId AND
                LOWER(@Email) = m.LoweredEmail

    IF (@@rowcount = 0)
        RETURN(1)
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByName]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByName]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier

    IF (@UpdateLastActivity = 1)
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, @CurrentTimeUtc, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    aspnet_Applications a, aspnet_Users u, aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1

        UPDATE   aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        WHERE    @UserId = UserId
    END
    ELSE
    BEGIN
        SELECT TOP 1 m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
                m.CreateDate, m.LastLoginDate, u.LastActivityDate, m.LastPasswordChangedDate,
                u.UserId, m.IsLockedOut,m.LastLockoutDate
        FROM    aspnet_Applications a, aspnet_Users u, aspnet_Membership m
        WHERE    LOWER(@ApplicationName) = a.LoweredApplicationName AND
                u.ApplicationId = a.ApplicationId    AND
                LOWER(@UserName) = u.LoweredUserName AND u.UserId = m.UserId

        IF (@@ROWCOUNT = 0) -- Username not found
            RETURN -1
    END

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_GetUserByUserId]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_GetUserByUserId]
    @UserId               uniqueidentifier,
    @CurrentTimeUtc       datetime,
    @UpdateLastActivity   bit = 0
AS
BEGIN
    IF ( @UpdateLastActivity = 1 )
    BEGIN
        UPDATE   aspnet_Users
        SET      LastActivityDate = @CurrentTimeUtc
        FROM     aspnet_Users
        WHERE    @UserId = UserId

        IF ( @@ROWCOUNT = 0 ) -- User ID not found
            RETURN -1
    END

    SELECT  m.Email, m.PasswordQuestion, m.Comment, m.IsApproved,
            m.CreateDate, m.LastLoginDate, u.LastActivityDate,
            m.LastPasswordChangedDate, u.UserName, m.IsLockedOut,
            m.LastLockoutDate
    FROM    aspnet_Users u, aspnet_Membership m
    WHERE   @UserId = u.UserId AND u.UserId = m.UserId

    IF ( @@ROWCOUNT = 0 ) -- User ID not found
       RETURN -1

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_ResetPassword]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_ResetPassword]
    @ApplicationName             nvarchar(256),
    @UserName                    nvarchar(256),
    @NewPassword                 nvarchar(128),
    @MaxInvalidPasswordAttempts  int,
    @PasswordAttemptWindow       int,
    @PasswordSalt                nvarchar(128),
    @CurrentTimeUtc              datetime,
    @PasswordFormat              int = 0,
    @PasswordAnswer              nvarchar(128) = NULL
AS
BEGIN
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @UserId                                 uniqueidentifier
    SET     @UserId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId
    FROM    aspnet_Users u, aspnet_Applications a, aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    SELECT @IsLockedOut = IsLockedOut,
           @LastLockoutDate = LastLockoutDate,
           @FailedPasswordAttemptCount = FailedPasswordAttemptCount,
           @FailedPasswordAttemptWindowStart = FailedPasswordAttemptWindowStart,
           @FailedPasswordAnswerAttemptCount = FailedPasswordAnswerAttemptCount,
           @FailedPasswordAnswerAttemptWindowStart = FailedPasswordAnswerAttemptWindowStart
    FROM aspnet_Membership WITH ( UPDLOCK )
    WHERE @UserId = UserId

    IF( @IsLockedOut = 1 )
    BEGIN
        SET @ErrorCode = 99
        GOTO Cleanup
    END

    UPDATE aspnet_Membership
    SET    Password = @NewPassword,
           LastPasswordChangedDate = @CurrentTimeUtc,
           PasswordFormat = @PasswordFormat,
           PasswordSalt = @PasswordSalt
    WHERE  @UserId = UserId AND
           ( ( @PasswordAnswer IS NULL ) OR ( LOWER( PasswordAnswer ) = LOWER( @PasswordAnswer ) ) )

    IF ( @@ROWCOUNT = 0 )
        BEGIN
            IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAnswerAttemptWindowStart ) )
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = 1
            END
            ELSE
            BEGIN
                SET @FailedPasswordAnswerAttemptWindowStart = @CurrentTimeUtc
                SET @FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount + 1
            END

            BEGIN
                IF( @FailedPasswordAnswerAttemptCount >= @MaxInvalidPasswordAttempts )
                BEGIN
                    SET @IsLockedOut = 1
                    SET @LastLockoutDate = @CurrentTimeUtc
                END
            END

            SET @ErrorCode = 3
        END
    ELSE
        BEGIN
            IF( @FailedPasswordAnswerAttemptCount > 0 )
            BEGIN
                SET @FailedPasswordAnswerAttemptCount = 0
                SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            END
        END

    IF( NOT ( @PasswordAnswer IS NULL ) )
    BEGIN
        UPDATE aspnet_Membership
        SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
            FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
            FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
            FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
        WHERE @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_SetPassword]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_SetPassword]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @NewPassword      nvarchar(128),
    @PasswordSalt     nvarchar(128),
    @CurrentTimeUtc   datetime,
    @PasswordFormat   int = 0
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    aspnet_Users u, aspnet_Applications a, aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    UPDATE aspnet_Membership
    SET Password = @NewPassword, PasswordFormat = @PasswordFormat, PasswordSalt = @PasswordSalt,
        LastPasswordChangedDate = @CurrentTimeUtc
    WHERE @UserId = UserId
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UnlockUser]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UnlockUser]
    @ApplicationName                         nvarchar(256),
    @UserName                                nvarchar(256)
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId
    FROM    aspnet_Users u, aspnet_Applications a, aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF ( @UserId IS NULL )
        RETURN 1

    UPDATE aspnet_Membership
    SET IsLockedOut = 0,
        FailedPasswordAttemptCount = 0,
        FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        FailedPasswordAnswerAttemptCount = 0,
        FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 ),
        LastLockoutDate = CONVERT( datetime, '17540101', 112 )
    WHERE @UserId = UserId

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUser]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUser]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @Email                nvarchar(256),
    @Comment              ntext,
    @IsApproved           bit,
    @LastLoginDate        datetime,
    @LastActivityDate     datetime,
    @UniqueEmail          int,
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @UserId uniqueidentifier
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @UserId = NULL
    SELECT  @UserId = u.UserId, @ApplicationId = a.ApplicationId
    FROM    aspnet_Users u, aspnet_Applications a, aspnet_Membership m
    WHERE   LoweredUserName = LOWER(@UserName) AND
            u.ApplicationId = a.ApplicationId  AND
            LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.UserId = m.UserId

    IF (@UserId IS NULL)
        RETURN(1)

    IF (@UniqueEmail = 1)
    BEGIN
        IF (EXISTS (SELECT *
                    FROM  aspnet_Membership WITH (UPDLOCK, HOLDLOCK)
                    WHERE ApplicationId = @ApplicationId  AND @UserId <> UserId AND LoweredEmail = LOWER(@Email)))
        BEGIN
            RETURN(7)
        END
    END

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    UPDATE aspnet_Users WITH (ROWLOCK)
    SET
         LastActivityDate = @LastActivityDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    UPDATE aspnet_Membership WITH (ROWLOCK)
    SET
         Email            = @Email,
         LoweredEmail     = LOWER(@Email),
         Comment          = @Comment,
         IsApproved       = @IsApproved,
         LastLoginDate    = @LastLoginDate
    WHERE
       @UserId = UserId

    IF( @@ERROR <> 0 )
        GOTO Cleanup

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN -1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Membership_UpdateUserInfo]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Membership_UpdateUserInfo]
    @ApplicationName                nvarchar(256),
    @UserName                       nvarchar(256),
    @IsPasswordCorrect              bit,
    @UpdateLastLoginActivityDate    bit,
    @MaxInvalidPasswordAttempts     int,
    @PasswordAttemptWindow          int,
    @CurrentTimeUtc                 datetime,
    @LastLoginDate                  datetime,
    @LastActivityDate               datetime
AS
BEGIN
    DECLARE @UserId                                 uniqueidentifier
    DECLARE @IsApproved                             bit
    DECLARE @IsLockedOut                            bit
    DECLARE @LastLockoutDate                        datetime
    DECLARE @FailedPasswordAttemptCount             int
    DECLARE @FailedPasswordAttemptWindowStart       datetime
    DECLARE @FailedPasswordAnswerAttemptCount       int
    DECLARE @FailedPasswordAnswerAttemptWindowStart datetime

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    SELECT  @UserId = u.UserId,
            @IsApproved = m.IsApproved,
            @IsLockedOut = m.IsLockedOut,
            @LastLockoutDate = m.LastLockoutDate,
            @FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
            @FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
            @FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
            @FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart
    FROM    aspnet_Applications a, aspnet_Users u, aspnet_Membership m WITH ( UPDLOCK )
    WHERE   LOWER(@ApplicationName) = a.LoweredApplicationName AND
            u.ApplicationId = a.ApplicationId    AND
            u.UserId = m.UserId AND
            LOWER(@UserName) = u.LoweredUserName

    IF ( @@rowcount = 0 )
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    IF( @IsLockedOut = 1 )
    BEGIN
        GOTO Cleanup
    END

    IF( @IsPasswordCorrect = 0 )
    BEGIN
        IF( @CurrentTimeUtc > DATEADD( minute, @PasswordAttemptWindow, @FailedPasswordAttemptWindowStart ) )
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = 1
        END
        ELSE
        BEGIN
            SET @FailedPasswordAttemptWindowStart = @CurrentTimeUtc
            SET @FailedPasswordAttemptCount = @FailedPasswordAttemptCount + 1
        END

        BEGIN
            IF( @FailedPasswordAttemptCount >= @MaxInvalidPasswordAttempts )
            BEGIN
                SET @IsLockedOut = 1
                SET @LastLockoutDate = @CurrentTimeUtc
            END
        END
    END
    ELSE
    BEGIN
        IF( @FailedPasswordAttemptCount > 0 OR @FailedPasswordAnswerAttemptCount > 0 )
        BEGIN
            SET @FailedPasswordAttemptCount = 0
            SET @FailedPasswordAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @FailedPasswordAnswerAttemptCount = 0
            SET @FailedPasswordAnswerAttemptWindowStart = CONVERT( datetime, '17540101', 112 )
            SET @LastLockoutDate = CONVERT( datetime, '17540101', 112 )
        END
    END

    IF( @UpdateLastLoginActivityDate = 1 )
    BEGIN
        UPDATE  aspnet_Users
        SET     LastActivityDate = @LastActivityDate
        WHERE   @UserId = UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END

        UPDATE  aspnet_Membership
        SET     LastLoginDate = @LastLoginDate
        WHERE   UserId = @UserId

        IF( @@ERROR <> 0 )
        BEGIN
            SET @ErrorCode = -1
            GOTO Cleanup
        END
    END


    UPDATE aspnet_Membership
    SET IsLockedOut = @IsLockedOut, LastLockoutDate = @LastLockoutDate,
        FailedPasswordAttemptCount = @FailedPasswordAttemptCount,
        FailedPasswordAttemptWindowStart = @FailedPasswordAttemptWindowStart,
        FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount,
        FailedPasswordAnswerAttemptWindowStart = @FailedPasswordAnswerAttemptWindowStart
    WHERE @UserId = UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
	SET @TranStarted = 0
	COMMIT TRANSACTION
    END

    RETURN @ErrorCode

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Paths_CreatePath]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Paths_CreatePath]
    @ApplicationId UNIQUEIDENTIFIER,
    @Path           NVARCHAR(256),
    @PathId         UNIQUEIDENTIFIER OUTPUT
AS
BEGIN
    BEGIN TRANSACTION
    IF (NOT EXISTS(SELECT * FROM aspnet_Paths WHERE LoweredPath = LOWER(@Path) AND ApplicationId = @ApplicationId))
    BEGIN
        INSERT aspnet_Paths (ApplicationId, Path, LoweredPath) VALUES (@ApplicationId, @Path, LOWER(@Path))
    END
    COMMIT TRANSACTION
    SELECT @PathId = PathId FROM aspnet_Paths WHERE LOWER(@Path) = LoweredPath AND ApplicationId = @ApplicationId
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Personalization_GetApplicationId]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Personalization_GetApplicationId] (
    @ApplicationName NVARCHAR(256),
    @ApplicationId UNIQUEIDENTIFIER OUT)
AS
BEGIN
    SELECT @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_DeleteAllState]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_DeleteAllState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Count int OUT)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        IF (@AllUsersScope = 1)
            DELETE FROM aspnet_PersonalizationAllUsers
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)
        ELSE
            DELETE FROM aspnet_PersonalizationPerUser
            WHERE PathId IN
               (SELECT Paths.PathId
                FROM aspnet_Paths Paths
                WHERE Paths.ApplicationId = @ApplicationId)

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_FindState]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_FindState] (
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @PageIndex              INT,
    @PageSize               INT,
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound INT
    DECLARE @PageUpperBound INT
    DECLARE @TotalRecords   INT
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table to store the selected results
    CREATE TABLE #PageIndex (
        IndexId int IDENTITY (0, 1) NOT NULL,
        ItemId UNIQUEIDENTIFIER
    )

    IF (@AllUsersScope = 1)
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT Paths.PathId
        FROM aspnet_Paths Paths,
             ((SELECT Paths.PathId
               FROM aspnet_PersonalizationAllUsers AllUsers, aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND AllUsers.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT DISTINCT Paths.PathId
               FROM aspnet_PersonalizationPerUser PerUser, aspnet_Paths Paths
               WHERE Paths.ApplicationId = @ApplicationId
                      AND PerUser.PathId = Paths.PathId
                      AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path,
               SharedDataPerPath.LastUpdatedDate,
               SharedDataPerPath.SharedDataLength,
               UserDataPerPath.UserDataLength,
               UserDataPerPath.UserCount
        FROM aspnet_Paths Paths,
             ((SELECT PageIndex.ItemId AS PathId,
                      AllUsers.LastUpdatedDate AS LastUpdatedDate,
                      DATALENGTH(AllUsers.PageSettings) AS SharedDataLength
               FROM aspnet_PersonalizationAllUsers AllUsers, #PageIndex PageIndex
               WHERE AllUsers.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
              ) AS SharedDataPerPath
              FULL OUTER JOIN
              (SELECT PageIndex.ItemId AS PathId,
                      SUM(DATALENGTH(PerUser.PageSettings)) AS UserDataLength,
                      COUNT(*) AS UserCount
               FROM aspnet_PersonalizationPerUser PerUser, #PageIndex PageIndex
               WHERE PerUser.PathId = PageIndex.ItemId
                     AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
               GROUP BY PageIndex.ItemId
              ) AS UserDataPerPath
              ON SharedDataPerPath.PathId = UserDataPerPath.PathId
             )
        WHERE Paths.PathId = SharedDataPerPath.PathId OR Paths.PathId = UserDataPerPath.PathId
        ORDER BY Paths.Path ASC
    END
    ELSE
    BEGIN
        -- Insert into our temp table
        INSERT INTO #PageIndex (ItemId)
        SELECT PerUser.Id
        FROM aspnet_PersonalizationPerUser PerUser, aspnet_Users Users, aspnet_Paths Paths
        WHERE Paths.ApplicationId = @ApplicationId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
              AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
              AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
        ORDER BY Paths.Path ASC, Users.UserName ASC

        SELECT @TotalRecords = @@ROWCOUNT

        SELECT Paths.Path, PerUser.LastUpdatedDate, DATALENGTH(PerUser.PageSettings), Users.UserName, Users.LastActivityDate
        FROM aspnet_PersonalizationPerUser PerUser, aspnet_Users Users, aspnet_Paths Paths, #PageIndex PageIndex
        WHERE PerUser.Id = PageIndex.ItemId
              AND PerUser.UserId = Users.UserId
              AND PerUser.PathId = Paths.PathId
              AND PageIndex.IndexId >= @PageLowerBound AND PageIndex.IndexId <= @PageUpperBound
        ORDER BY Paths.Path ASC, Users.UserName ASC
    END

    RETURN @TotalRecords
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_GetCountOfState]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_GetCountOfState] (
    @Count int OUT,
    @AllUsersScope bit,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256) = NULL,
    @UserName NVARCHAR(256) = NULL,
    @InactiveSinceDate DATETIME = NULL)
AS
BEGIN

    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
        IF (@AllUsersScope = 1)
            SELECT @Count = COUNT(*)
            FROM aspnet_PersonalizationAllUsers AllUsers, aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND AllUsers.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
        ELSE
            SELECT @Count = COUNT(*)
            FROM aspnet_PersonalizationPerUser PerUser, aspnet_Users Users, aspnet_Paths Paths
            WHERE Paths.ApplicationId = @ApplicationId
                  AND PerUser.UserId = Users.UserId
                  AND PerUser.PathId = Paths.PathId
                  AND (@Path IS NULL OR Paths.LoweredPath LIKE LOWER(@Path))
                  AND (@UserName IS NULL OR Users.LoweredUserName LIKE LOWER(@UserName))
                  AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetSharedState]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetSharedState] (
    @Count int OUT,
    @ApplicationName NVARCHAR(256),
    @Path NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM aspnet_PersonalizationAllUsers
        WHERE PathId IN
            (SELECT AllUsers.PathId
             FROM aspnet_PersonalizationAllUsers AllUsers, aspnet_Paths Paths
             WHERE Paths.ApplicationId = @ApplicationId
                   AND AllUsers.PathId = Paths.PathId
                   AND Paths.LoweredPath = LOWER(@Path))

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAdministration_ResetUserState]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAdministration_ResetUserState] (
    @Count                  int                 OUT,
    @ApplicationName        NVARCHAR(256),
    @InactiveSinceDate      DATETIME            = NULL,
    @UserName               NVARCHAR(256)       = NULL,
    @Path                   NVARCHAR(256)       = NULL)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
        SELECT @Count = 0
    ELSE
    BEGIN
        DELETE FROM aspnet_PersonalizationPerUser
        WHERE Id IN (SELECT PerUser.Id
                     FROM aspnet_PersonalizationPerUser PerUser, aspnet_Users Users, aspnet_Paths Paths
                     WHERE Paths.ApplicationId = @ApplicationId
                           AND PerUser.UserId = Users.UserId
                           AND PerUser.PathId = Paths.PathId
                           AND (@InactiveSinceDate IS NULL OR Users.LastActivityDate <= @InactiveSinceDate)
                           AND (@UserName IS NULL OR Users.LoweredUserName = LOWER(@UserName))
                           AND (@Path IS NULL OR Paths.LoweredPath = LOWER(@Path)))

        SELECT @Count = @@ROWCOUNT
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT p.PageSettings FROM aspnet_PersonalizationAllUsers p WHERE p.PathId = @PathId
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path              NVARCHAR(256))
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    DELETE FROM aspnet_PersonalizationAllUsers WHERE PathId = @PathId
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationAllUsers_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL

    EXEC aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    IF (EXISTS(SELECT PathId FROM aspnet_PersonalizationAllUsers WHERE PathId = @PathId))
        UPDATE aspnet_PersonalizationAllUsers SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE PathId = @PathId
    ELSE
        INSERT INTO aspnet_PersonalizationAllUsers(PathId, PageSettings, LastUpdatedDate) VALUES (@PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_GetPageSettings]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_GetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    SELECT p.PageSettings FROM aspnet_PersonalizationPerUser p WHERE p.PathId = @PathId AND p.UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_ResetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC aspnet_Personalization_GetApplicationId @ApplicationName, @ApplicationId OUTPUT
    IF (@ApplicationId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @PathId = u.PathId FROM aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        RETURN
    END

    SELECT @UserId = u.UserId FROM aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        RETURN
    END

    UPDATE   aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    DELETE FROM aspnet_PersonalizationPerUser WHERE PathId = @PathId AND UserId = @UserId
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_PersonalizationPerUser_SetPageSettings]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_PersonalizationPerUser_SetPageSettings] (
    @ApplicationName  NVARCHAR(256),
    @UserName         NVARCHAR(256),
    @Path             NVARCHAR(256),
    @PageSettings     IMAGE,
    @CurrentTimeUtc   DATETIME)
AS
BEGIN
    DECLARE @ApplicationId UNIQUEIDENTIFIER
    DECLARE @PathId UNIQUEIDENTIFIER
    DECLARE @UserId UNIQUEIDENTIFIER

    SELECT @ApplicationId = NULL
    SELECT @PathId = NULL
    SELECT @UserId = NULL

    EXEC aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    SELECT @PathId = u.PathId FROM aspnet_Paths u WHERE u.ApplicationId = @ApplicationId AND u.LoweredPath = LOWER(@Path)
    IF (@PathId IS NULL)
    BEGIN
        EXEC aspnet_Paths_CreatePath @ApplicationId, @Path, @PathId OUTPUT
    END

    SELECT @UserId = u.UserId FROM aspnet_Users u WHERE u.ApplicationId = @ApplicationId AND u.LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
    BEGIN
        EXEC aspnet_Users_CreateUser @ApplicationId, @UserName, 0, @CurrentTimeUtc, @UserId OUTPUT
    END

    UPDATE   aspnet_Users WITH (ROWLOCK)
    SET      LastActivityDate = @CurrentTimeUtc
    WHERE    UserId = @UserId
    IF (@@ROWCOUNT = 0) -- Username not found
        RETURN

    IF (EXISTS(SELECT PathId FROM aspnet_PersonalizationPerUser WHERE UserId = @UserId AND PathId = @PathId))
        UPDATE aspnet_PersonalizationPerUser SET PageSettings = @PageSettings, LastUpdatedDate = @CurrentTimeUtc WHERE UserId = @UserId AND PathId = @PathId
    ELSE
        INSERT INTO aspnet_PersonalizationPerUser(UserId, PathId, PageSettings, LastUpdatedDate) VALUES (@UserId, @PathId, @PageSettings, @CurrentTimeUtc)
    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteInactiveProfiles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT  0
        RETURN
    END

    DELETE
    FROM    aspnet_Profile
    WHERE   UserId IN
            (   SELECT  UserId
                FROM    aspnet_Users u
                WHERE   ApplicationId = @ApplicationId
                        AND (LastActivityDate <= @InactiveSinceDate)
                        AND (
                                (@ProfileAuthOptions = 2)
                             OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                             OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                            )
            )

    SELECT  @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_DeleteProfiles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_DeleteProfiles]
    @ApplicationName        nvarchar(256),
    @UserNames              nvarchar(4000)
AS
BEGIN
    DECLARE @UserName     nvarchar(256)
    DECLARE @CurrentPos   int
    DECLARE @NextPos      int
    DECLARE @NumDeleted   int
    DECLARE @DeletedUser  int
    DECLARE @TranStarted  bit
    DECLARE @ErrorCode    int

    SET @ErrorCode = 0
    SET @CurrentPos = 1
    SET @NumDeleted = 0
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    WHILE (@CurrentPos <= LEN(@UserNames))
    BEGIN
        SELECT @NextPos = CHARINDEX(N',', @UserNames,  @CurrentPos)
        IF (@NextPos = 0 OR @NextPos IS NULL)
            SELECT @NextPos = LEN(@UserNames) + 1

        SELECT @UserName = SUBSTRING(@UserNames, @CurrentPos, @NextPos - @CurrentPos)
        SELECT @CurrentPos = @NextPos+1

        IF (LEN(@UserName) > 0)
        BEGIN
            SELECT @DeletedUser = 0
            EXEC aspnet_Users_DeleteUser @ApplicationName, @UserName, 4, @DeletedUser OUTPUT
            IF( @@ERROR <> 0 )
            BEGIN
                SET @ErrorCode = -1
                GOTO Cleanup
            END
            IF (@DeletedUser <> 0)
                SELECT @NumDeleted = @NumDeleted + 1
        END
    END
    SELECT @NumDeleted
    IF (@TranStarted = 1)
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END
    SET @TranStarted = 0

    RETURN 0

Cleanup:
    IF (@TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END
    RETURN @ErrorCode
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_GetNumberOfInactiveProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @InactiveSinceDate      datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
    BEGIN
        SELECT 0
        RETURN
    END

    SELECT  COUNT(*)
    FROM    aspnet_Users u, aspnet_Profile p
    WHERE   ApplicationId = @ApplicationId
        AND u.UserId = p.UserId
        AND (LastActivityDate <= @InactiveSinceDate)
        AND (
                (@ProfileAuthOptions = 2)
                OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
            )
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProfiles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_GetProfiles]
    @ApplicationName        nvarchar(256),
    @ProfileAuthOptions     int,
    @PageIndex              int,
    @PageSize               int,
    @UserNameToMatch        nvarchar(256) = NULL,
    @InactiveSinceDate      datetime      = NULL
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    -- Set the page bounds
    DECLARE @PageLowerBound int
    DECLARE @PageUpperBound int
    DECLARE @TotalRecords   int
    SET @PageLowerBound = @PageSize * @PageIndex
    SET @PageUpperBound = @PageSize - 1 + @PageLowerBound

    -- Create a temp table TO store the select results
    CREATE TABLE #PageIndexForUsers
    (
        IndexId int IDENTITY (0, 1) NOT NULL,
        UserId uniqueidentifier
    )

    -- Insert into our temp table
    INSERT INTO #PageIndexForUsers (UserId)
        SELECT  u.UserId
        FROM    aspnet_Users u, aspnet_Profile p
        WHERE   ApplicationId = @ApplicationId
            AND u.UserId = p.UserId
            AND (@InactiveSinceDate IS NULL OR LastActivityDate <= @InactiveSinceDate)
            AND (     (@ProfileAuthOptions = 2)
                   OR (@ProfileAuthOptions = 0 AND IsAnonymous = 1)
                   OR (@ProfileAuthOptions = 1 AND IsAnonymous = 0)
                 )
            AND (@UserNameToMatch IS NULL OR LoweredUserName LIKE LOWER(@UserNameToMatch))
        ORDER BY UserName

    SELECT  u.UserName, u.IsAnonymous, u.LastActivityDate, p.LastUpdatedDate,
            DATALENGTH(p.PropertyNames) + DATALENGTH(p.PropertyValuesString) + DATALENGTH(p.PropertyValuesBinary)
    FROM    aspnet_Users u, aspnet_Profile p, #PageIndexForUsers i
    WHERE   u.UserId = p.UserId AND p.UserId = i.UserId AND i.IndexId >= @PageLowerBound AND i.IndexId <= @PageUpperBound

    SELECT COUNT(*)
    FROM   #PageIndexForUsers

    DROP TABLE #PageIndexForUsers
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_GetProperties]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_GetProperties]
    @ApplicationName      nvarchar(256),
    @UserName             nvarchar(256),
    @CurrentTimeUtc       datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN

    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT @UserId = UserId
    FROM   aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)

    IF (@UserId IS NULL)
        RETURN
    SELECT TOP 1 PropertyNames, PropertyValuesString, PropertyValuesBinary
    FROM         aspnet_Profile
    WHERE        UserId = @UserId

    IF (@@ROWCOUNT > 0)
    BEGIN
        UPDATE aspnet_Users
        SET    LastActivityDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    END
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Profile_SetProperties]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Profile_SetProperties]
    @ApplicationName        nvarchar(256),
    @PropertyNames          ntext,
    @PropertyValuesString   ntext,
    @PropertyValuesBinary   image,
    @UserName               nvarchar(256),
    @IsUserAnonymous        bit,
    @CurrentTimeUtc         datetime
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
       BEGIN TRANSACTION
       SET @TranStarted = 1
    END
    ELSE
    	SET @TranStarted = 0

    EXEC aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DECLARE @UserId uniqueidentifier
    DECLARE @LastActivityDate datetime
    SELECT  @UserId = NULL
    SELECT  @LastActivityDate = @CurrentTimeUtc

    SELECT @UserId = UserId
    FROM   aspnet_Users
    WHERE  ApplicationId = @ApplicationId AND LoweredUserName = LOWER(@UserName)
    IF (@UserId IS NULL)
        EXEC aspnet_Users_CreateUser @ApplicationId, @UserName, @IsUserAnonymous, @LastActivityDate, @UserId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    UPDATE aspnet_Users
    SET    LastActivityDate=@CurrentTimeUtc
    WHERE  UserId = @UserId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS( SELECT *
               FROM   aspnet_Profile
               WHERE  UserId = @UserId))
        UPDATE aspnet_Profile
        SET    PropertyNames=@PropertyNames, PropertyValuesString = @PropertyValuesString,
               PropertyValuesBinary = @PropertyValuesBinary, LastUpdatedDate=@CurrentTimeUtc
        WHERE  UserId = @UserId
    ELSE
        INSERT INTO aspnet_Profile(UserId, PropertyNames, PropertyValuesString, PropertyValuesBinary, LastUpdatedDate)
             VALUES (@UserId, @PropertyNames, @PropertyValuesString, @PropertyValuesBinary, @CurrentTimeUtc)

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
    	SET @TranStarted = 0
    	COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
    	ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_RegisterSchemaVersion]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_RegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128),
    @IsCurrentVersion          bit,
    @RemoveIncompatibleSchema  bit
AS
BEGIN
    IF( @RemoveIncompatibleSchema = 1 )
    BEGIN
        DELETE FROM aspnet_SchemaVersions WHERE Feature = LOWER( @Feature )
    END
    ELSE
    BEGIN
        IF( @IsCurrentVersion = 1 )
        BEGIN
            UPDATE aspnet_SchemaVersions
            SET IsCurrentVersion = 0
            WHERE Feature = LOWER( @Feature )
        END
    END

    INSERT  aspnet_SchemaVersions( Feature, CompatibleSchemaVersion, IsCurrentVersion )
    VALUES( LOWER( @Feature ), @CompatibleSchemaVersion, @IsCurrentVersion )
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_CreateRole]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_CreateRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    EXEC aspnet_Applications_CreateApplication @ApplicationName, @ApplicationId OUTPUT

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF (EXISTS(SELECT RoleId FROM aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId))
    BEGIN
        SET @ErrorCode = 1
        GOTO Cleanup
    END

    INSERT INTO aspnet_Roles
                (ApplicationId, RoleName, LoweredRoleName)
         VALUES (@ApplicationId, @RoleName, LOWER(@RoleName))

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_DeleteRole]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[aspnet_Roles_DeleteRole]
    @ApplicationName            nvarchar(256),
    @RoleName                   nvarchar(256),
    @DeleteOnlyIfRoleIsEmpty    bit
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)

    DECLARE @ErrorCode     int
    SET @ErrorCode = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
        BEGIN TRANSACTION
        SET @TranStarted = 1
    END
    ELSE
        SET @TranStarted = 0

    DECLARE @RoleId   uniqueidentifier
    SELECT  @RoleId = NULL
    SELECT  @RoleId = RoleId FROM aspnet_Roles WHERE LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
    BEGIN
        SELECT @ErrorCode = 1
        GOTO Cleanup
    END
    IF (@DeleteOnlyIfRoleIsEmpty <> 0)
    BEGIN
        IF (EXISTS (SELECT RoleId FROM aspnet_UsersInRoles  WHERE @RoleId = RoleId))
        BEGIN
            SELECT @ErrorCode = 2
            GOTO Cleanup
        END
    END


    DELETE FROM aspnet_UsersInRoles  WHERE @RoleId = RoleId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    DELETE FROM aspnet_Roles WHERE @RoleId = RoleId  AND ApplicationId = @ApplicationId

    IF( @@ERROR <> 0 )
    BEGIN
        SET @ErrorCode = -1
        GOTO Cleanup
    END

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        COMMIT TRANSACTION
    END

    RETURN(0)

Cleanup:

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
        ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_GetAllRoles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Roles_GetAllRoles] (
    @ApplicationName           nvarchar(256))
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN
    SELECT RoleName
    FROM   aspnet_Roles WHERE ApplicationId = @ApplicationId
    ORDER BY RoleName
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Roles_RoleExists]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Roles_RoleExists]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(0)
    IF (EXISTS (SELECT RoleName FROM aspnet_Roles WHERE LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId ))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RemoveAllRoleMembers]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Setup_RemoveAllRoleMembers]
    @name   sysname
AS
BEGIN
    CREATE TABLE #aspnet_RoleMembers
    (
        Group_name      sysname,
        Group_id        smallint,
        Users_in_group  sysname,
        User_id         smallint
    )

    INSERT INTO #aspnet_RoleMembers
    EXEC sp_helpuser @name

    DECLARE @user_id smallint
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT User_id FROM #aspnet_RoleMembers

    OPEN c1

    FETCH c1 INTO @user_id
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = 'EXEC sp_droprolemember ' + '''' + @name + ''', ''' + USER_NAME(@user_id) + ''''
        EXEC (@cmd)
        FETCH c1 INTO @user_id
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Setup_RestorePermissions]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Setup_RestorePermissions]
    @name   sysname
AS
BEGIN
    DECLARE @object sysname
    DECLARE @protectType char(10)
    DECLARE @action varchar(20)
    DECLARE @grantee sysname
    DECLARE @cmd nvarchar(500)
    DECLARE c1 cursor FORWARD_ONLY FOR
        SELECT Object, ProtectType, [Action], Grantee FROM #aspnet_Permissions where Object = @name

    OPEN c1

    FETCH c1 INTO @object, @protectType, @action, @grantee
    WHILE (@@fetch_status = 0)
    BEGIN
        SET @cmd = @protectType + ' ' + @action + ' on ' + @object + ' TO [' + @grantee + ']'
        EXEC (@cmd)
        FETCH c1 INTO @object, @protectType, @action, @grantee
    END

    CLOSE c1
    DEALLOCATE c1
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UnRegisterSchemaVersion]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_UnRegisterSchemaVersion]
    @Feature                   nvarchar(128),
    @CompatibleSchemaVersion   nvarchar(128)
AS
BEGIN
    DELETE FROM aspnet_SchemaVersions
        WHERE   Feature = LOWER(@Feature) AND @CompatibleSchemaVersion = CompatibleSchemaVersion
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_CreateUser]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_Users_CreateUser]
    @ApplicationId    uniqueidentifier,
    @UserName         nvarchar(256),
    @IsUserAnonymous  bit,
    @LastActivityDate DATETIME,
    @UserId           uniqueidentifier OUTPUT
AS
BEGIN
    IF( @UserId IS NULL )
        SELECT @UserId = NEWID()
    ELSE
    BEGIN
        IF( EXISTS( SELECT UserId FROM aspnet_Users
                    WHERE @UserId = UserId ) )
            RETURN -1
    END

    INSERT aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
    VALUES (@ApplicationId, @UserId, @UserName, LOWER(@UserName), @IsUserAnonymous, @LastActivityDate)

    RETURN 0
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_Users_DeleteUser]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_Users_DeleteUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @TablesToDeleteFrom int,
    @NumTablesDeletedFrom int OUTPUT
AS
BEGIN
    DECLARE @UserId               uniqueidentifier
    SELECT  @UserId               = NULL
    SELECT  @NumTablesDeletedFrom = 0

    DECLARE @TranStarted   bit
    SET @TranStarted = 0

    IF( @@TRANCOUNT = 0 )
    BEGIN
	    BEGIN TRANSACTION
	    SET @TranStarted = 1
    END
    ELSE
	SET @TranStarted = 0

    DECLARE @ErrorCode   int
    DECLARE @RowCount    int

    SET @ErrorCode = 0
    SET @RowCount  = 0

    SELECT  @UserId = u.UserId
    FROM    aspnet_Users u, aspnet_Applications a
    WHERE   u.LoweredUserName       = LOWER(@UserName)
        AND u.ApplicationId         = a.ApplicationId
        AND LOWER(@ApplicationName) = a.LoweredApplicationName

    IF (@UserId IS NULL)
    BEGIN
        GOTO Cleanup
    END

    -- Delete from Membership table if (@TablesToDeleteFrom & 1) is set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_MembershipUsers') AND (type = 'V'))))
    BEGIN
        DELETE FROM aspnet_Membership WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
               @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_UsersInRoles table if (@TablesToDeleteFrom & 2) is set
    IF ((@TablesToDeleteFrom & 2) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_UsersInRoles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM aspnet_UsersInRoles WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Profile table if (@TablesToDeleteFrom & 4) is set
    IF ((@TablesToDeleteFrom & 4) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_Profiles') AND (type = 'V'))) )
    BEGIN
        DELETE FROM aspnet_Profile WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_PersonalizationPerUser table if (@TablesToDeleteFrom & 8) is set
    IF ((@TablesToDeleteFrom & 8) <> 0  AND
        (EXISTS (SELECT name FROM sysobjects WHERE (name = N'vw_aspnet_WebPartState_User') AND (type = 'V'))) )
    BEGIN
        DELETE FROM aspnet_PersonalizationPerUser WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    -- Delete from aspnet_Users table if (@TablesToDeleteFrom & 1,2,4 & 8) are all set
    IF ((@TablesToDeleteFrom & 1) <> 0 AND
        (@TablesToDeleteFrom & 2) <> 0 AND
        (@TablesToDeleteFrom & 4) <> 0 AND
        (@TablesToDeleteFrom & 8) <> 0 AND
        (EXISTS (SELECT UserId FROM aspnet_Users WHERE @UserId = UserId)))
    BEGIN
        DELETE FROM aspnet_Users WHERE @UserId = UserId

        SELECT @ErrorCode = @@ERROR,
                @RowCount = @@ROWCOUNT

        IF( @ErrorCode <> 0 )
            GOTO Cleanup

        IF (@RowCount <> 0)
            SELECT  @NumTablesDeletedFrom = @NumTablesDeletedFrom + 1
    END

    IF( @TranStarted = 1 )
    BEGIN
	    SET @TranStarted = 0
	    COMMIT TRANSACTION
    END

    RETURN 0

Cleanup:
    SET @NumTablesDeletedFrom = 0

    IF( @TranStarted = 1 )
    BEGIN
        SET @TranStarted = 0
	    ROLLBACK TRANSACTION
    END

    RETURN @ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_AddUsersToRoles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_AddUsersToRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000),
	@CurrentTimeUtc   datetime
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)
	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames	table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles	table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers	table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num		int
	DECLARE @Pos		int
	DECLARE @NextPos	int
	DECLARE @Name		nvarchar(256)

	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) COLLATE DATABASE_DEFAULT = ar.LoweredRoleName  COLLATE DATABASE_DEFAULT  AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		SELECT TOP 1 Name
		FROM   @tbNames
		WHERE  LOWER(Name) COLLATE DATABASE_DEFAULT NOT IN (SELECT ar.LoweredRoleName COLLATE DATABASE_DEFAULT FROM aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)  
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END

	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1

	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) COLLATE DATABASE_DEFAULT   = ar.LoweredUserName  COLLATE DATABASE_DEFAULT  AND ar.ApplicationId = @AppId

	IF (@@ROWCOUNT <> @Num)
	BEGIN
		DELETE FROM @tbNames
		WHERE LOWER(Name) COLLATE DATABASE_DEFAULT IN (SELECT LoweredUserName FROM aspnet_Users au,  @tbUsers u WHERE au.UserId = u.UserId)

		INSERT aspnet_Users (ApplicationId, UserId, UserName, LoweredUserName, IsAnonymous, LastActivityDate)
		  SELECT @AppId, NEWID(), Name, LOWER(Name), 0, @CurrentTimeUtc
		  FROM   @tbNames

		INSERT INTO @tbUsers
		  SELECT  UserId
		  FROM	aspnet_Users au, @tbNames t
		  WHERE   LOWER(t.Name) COLLATE DATABASE_DEFAULT = au.LoweredUserName  COLLATE DATABASE_DEFAULT  AND au.ApplicationId = @AppId
	END

	IF (EXISTS (SELECT * FROM aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr WHERE tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId))
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM aspnet_UsersInRoles ur, @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE	u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND tu.UserId = ur.UserId AND tr.RoleId = ur.RoleId

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	INSERT INTO aspnet_UsersInRoles (UserId, RoleId)
	SELECT UserId, RoleId
	FROM @tbUsers, @tbRoles

	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_FindUsersInRole]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_FindUsersInRole]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256),
    @UserNameToMatch  nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   aspnet_Users u, aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId AND LoweredUserName LIKE LOWER(@UserNameToMatch)
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetRolesForUser]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetRolesForUser]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL

    SELECT  @UserId = UserId
    FROM    aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(1)

    SELECT r.RoleName
    FROM   aspnet_Roles r, aspnet_UsersInRoles ur
    WHERE  r.RoleId = ur.RoleId AND r.ApplicationId = @ApplicationId AND ur.UserId = @UserId
    ORDER BY r.RoleName
    RETURN (0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_GetUsersInRoles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_GetUsersInRoles]
    @ApplicationName  nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(1)
     DECLARE @RoleId uniqueidentifier
     SELECT  @RoleId = NULL

     SELECT  @RoleId = RoleId
     FROM    aspnet_Roles
     WHERE   LOWER(@RoleName) = LoweredRoleName AND ApplicationId = @ApplicationId

     IF (@RoleId IS NULL)
         RETURN(1)

    SELECT u.UserName
    FROM   aspnet_Users u, aspnet_UsersInRoles ur
    WHERE  u.UserId = ur.UserId AND @RoleId = ur.RoleId AND u.ApplicationId = @ApplicationId
    ORDER BY u.UserName
    RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_IsUserInRole]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_IsUserInRole]
    @ApplicationName  nvarchar(256),
    @UserName         nvarchar(256),
    @RoleName         nvarchar(256)
AS
BEGIN
    DECLARE @ApplicationId uniqueidentifier
    SELECT  @ApplicationId = NULL
    SELECT  @ApplicationId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
    IF (@ApplicationId IS NULL)
        RETURN(2)
    DECLARE @UserId uniqueidentifier
    SELECT  @UserId = NULL
    DECLARE @RoleId uniqueidentifier
    SELECT  @RoleId = NULL

    SELECT  @UserId = UserId
    FROM    aspnet_Users
    WHERE   LoweredUserName = LOWER(@UserName) AND ApplicationId = @ApplicationId

    IF (@UserId IS NULL)
        RETURN(2)

    SELECT  @RoleId = RoleId
    FROM    aspnet_Roles
    WHERE   LoweredRoleName = LOWER(@RoleName) AND ApplicationId = @ApplicationId

    IF (@RoleId IS NULL)
        RETURN(3)

    IF (EXISTS( SELECT * FROM aspnet_UsersInRoles WHERE  UserId = @UserId AND RoleId = @RoleId))
        RETURN(1)
    ELSE
        RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_UsersInRoles_RemoveUsersFromRoles]
	@ApplicationName  nvarchar(256),
	@UserNames		  nvarchar(4000),
	@RoleNames		  nvarchar(4000)
AS
BEGIN
	DECLARE @AppId uniqueidentifier
	SELECT  @AppId = NULL
	SELECT  @AppId = ApplicationId FROM aspnet_Applications WHERE LOWER(@ApplicationName) = LoweredApplicationName
	IF (@AppId IS NULL)
		RETURN(2)


	DECLARE @TranStarted   bit
	SET @TranStarted = 0

	IF( @@TRANCOUNT = 0 )
	BEGIN
		BEGIN TRANSACTION
		SET @TranStarted = 1
	END

	DECLARE @tbNames  table(Name nvarchar(256) NOT NULL PRIMARY KEY)
	DECLARE @tbRoles  table(RoleId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @tbUsers  table(UserId uniqueidentifier NOT NULL PRIMARY KEY)
	DECLARE @Num	  int
	DECLARE @Pos	  int
	DECLARE @NextPos  int
	DECLARE @Name	  nvarchar(256)
	DECLARE @CountAll int
	DECLARE @CountU	  int
	DECLARE @CountR	  int


	SET @Num = 0
	SET @Pos = 1
	WHILE(@Pos <= LEN(@RoleNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @RoleNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@RoleNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@RoleNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbRoles
	  SELECT RoleId
	  FROM   aspnet_Roles ar, @tbNames t
	  WHERE  LOWER(t.Name) COLLATE DATABASE_DEFAULT = ar.LoweredRoleName COLLATE DATABASE_DEFAULT AND ar.ApplicationId = @AppId
	SELECT @CountR = @@ROWCOUNT

	IF (@CountR <> @Num)
	BEGIN
		SELECT TOP 1 N'', Name
		FROM   @tbNames
		WHERE  LOWER(Name) COLLATE DATABASE_DEFAULT NOT IN (SELECT ar.LoweredRoleName FROM aspnet_Roles ar,  @tbRoles r WHERE r.RoleId = ar.RoleId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(2)
	END


	DELETE FROM @tbNames WHERE 1=1
	SET @Num = 0
	SET @Pos = 1


	WHILE(@Pos <= LEN(@UserNames))
	BEGIN
		SELECT @NextPos = CHARINDEX(N',', @UserNames,  @Pos)
		IF (@NextPos = 0 OR @NextPos IS NULL)
			SELECT @NextPos = LEN(@UserNames) + 1
		SELECT @Name = RTRIM(LTRIM(SUBSTRING(@UserNames, @Pos, @NextPos - @Pos)))
		SELECT @Pos = @NextPos+1

		INSERT INTO @tbNames VALUES (@Name)
		SET @Num = @Num + 1
	END

	INSERT INTO @tbUsers
	  SELECT UserId
	  FROM   aspnet_Users ar, @tbNames t
	  WHERE  LOWER(t.Name) COLLATE DATABASE_DEFAULT = ar.LoweredUserName COLLATE DATABASE_DEFAULT AND ar.ApplicationId = @AppId

	SELECT @CountU = @@ROWCOUNT
	IF (@CountU <> @Num)
	BEGIN
		SELECT TOP 1 Name, N''
		FROM   @tbNames
		WHERE  LOWER(Name) COLLATE DATABASE_DEFAULT NOT IN (SELECT au.LoweredUserName FROM aspnet_Users au,  @tbUsers u WHERE u.UserId = au.UserId)

		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(1)
	END

	SELECT  @CountAll = COUNT(*)
	FROM	aspnet_UsersInRoles ur, @tbUsers u, @tbRoles r
	WHERE   ur.UserId = u.UserId AND ur.RoleId = r.RoleId

	IF (@CountAll <> @CountU * @CountR)
	BEGIN
		SELECT TOP 1 UserName, RoleName
		FROM		 @tbUsers tu, @tbRoles tr, aspnet_Users u, aspnet_Roles r
		WHERE		 u.UserId = tu.UserId AND r.RoleId = tr.RoleId AND
					 tu.UserId NOT IN (SELECT ur.UserId FROM aspnet_UsersInRoles ur WHERE ur.RoleId = tr.RoleId) AND
					 tr.RoleId NOT IN (SELECT ur.RoleId FROM aspnet_UsersInRoles ur WHERE ur.UserId = tu.UserId)
		IF( @TranStarted = 1 )
			ROLLBACK TRANSACTION
		RETURN(3)
	END

	DELETE FROM aspnet_UsersInRoles
	WHERE UserId IN (SELECT UserId FROM @tbUsers)
	  AND RoleId IN (SELECT RoleId FROM @tbRoles)
	IF( @TranStarted = 1 )
		COMMIT TRANSACTION
	RETURN(0)
END
GO
/****** Object:  StoredProcedure [dbo].[aspnet_WebEvent_LogEvent]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[aspnet_WebEvent_LogEvent]
        @EventId         char(32),
        @EventTimeUtc    datetime,
        @EventTime       datetime,
        @EventType       nvarchar(256),
        @EventSequence   decimal(19,0),
        @EventOccurrence decimal(19,0),
        @EventCode       int,
        @EventDetailCode int,
        @Message         nvarchar(1024),
        @ApplicationPath nvarchar(256),
        @ApplicationVirtualPath nvarchar(256),
        @MachineName    nvarchar(256),
        @RequestUrl      nvarchar(1024),
        @ExceptionType   nvarchar(256),
        @Details         ntext
AS
BEGIN
    INSERT
        aspnet_WebEvent_Events
        (
            EventId,
            EventTimeUtc,
            EventTime,
            EventType,
            EventSequence,
            EventOccurrence,
            EventCode,
            EventDetailCode,
            Message,
            ApplicationPath,
            ApplicationVirtualPath,
            MachineName,
            RequestUrl,
            ExceptionType,
            Details
        )
    VALUES
    (
        @EventId,
        @EventTimeUtc,
        @EventTime,
        @EventType,
        @EventSequence,
        @EventOccurrence,
        @EventCode,
        @EventDetailCode,
        @Message,
        @ApplicationPath,
        @ApplicationVirtualPath,
        @MachineName,
        @RequestUrl,
        @ExceptionType,
        @Details
    )
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteRuleById]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteRuleById]  
	@id INT AS  
BEGIN  
	DELETE AuthorizationRules  
	WHERE RULEID = @id  
End
GO
/****** Object:  StoredProcedure [dbo].[GetAllRules]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllRules] AS  
SELECT RULEID, RULETYPE + '.' + ROOT +
	CASE WHEN  LEN(ENTITY)= 0 THEN '' ELSE '.' + ENTITY END +
	CASE WHEN  LEN(MEMBER)= 0 THEN '' ELSE '.' + MEMBER END As NAME,EXPRESSION  
FROM AuthorizationRules 
Return(0) 
GO
/****** Object:  StoredProcedure [dbo].[GetAllRulesEx]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllRulesEx]  
	@RULESOURCE int AS  
SELECT RULEID,RULESOURCE,RULETYPE,ROOT,ENTITY,MEMBER,EXPRESSION  
FROM AuthorizationRules 
WHERE RULESOURCE=@RULESOURCE  
RETURN(0) 
GO
/****** Object:  StoredProcedure [dbo].[GetRuleByName]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRuleByName]  
	@RULESOURCE int,@RULETYPE nchar(1),@ROOT  nvarchar(64),@ENTITY  nvarchar(64)='', @MEMBER nvarchar(128)='' AS  
SELECT RULEID, STR(RULESOURCE) + '.' +  RULETYPE + '.' + ROOT + 
	CASE WHEN  LEN(ENTITY)= 0 THEN '' ELSE '.' + ENTITY END  +	  
	CASE WHEN  LEN(MEMBER)= 0 THEN '' ELSE '.' + MEMBER END  As NAME, EXPRESSION  
FROM AuthorizationRules  
WHERE RULESOURCE = @RULESOURCE AND RULETYPE =@RULETYPE  AND   
	ROOT=@ROOT AND
	ENTITY =@ENTITY AND  MEMBER=@MEMBER  
RETURN(0)
GO
/****** Object:  StoredProcedure [dbo].[GetRuleFamily]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRuleFamily]  
	@RULESOURCE int, @RULETYPE nchar(1),@ROOT  nvarchar(64),@ENTITY  nvarchar(64)='', @MEMBER nvarchar(128)='' AS  
	IF LEN(@ENTITY)= 0 AND LEN(@MEMBER)=0 
	BEGIN	
		SELECT RULEID, RULESOURCE,RULETYPE,ROOT,ENTITY,MEMBER,STR(RULESOURCE) + '.' +  RULETYPE + '.' + ROOT + 
		CASE LEN(ENTITY) WHEN 0 THEN '' ELSE '.' + ENTITY END  +	  
		CASE LEN(MEMBER) WHEN  0 THEN '' ELSE '.' + MEMBER END  As NAME,EXPRESSION  
		FROM AuthorizationRules  
		WHERE RULESOURCE=@RULESOURCE AND RULETYPE=@RULETYPE  AND ROOT=@ROOT
	END 	
	ELSE IF LEN(@ENTITY)> 0 AND LEN(@MEMBER)=0
	BEGIN
		SELECT RULEID, RULESOURCE,RULETYPE,ROOT,ENTITY,MEMBER,STR(RULESOURCE) + '.' +  RULETYPE + '.' + ROOT + 
		CASE LEN(ENTITY) WHEN 0 THEN '' ELSE '.' + ENTITY END  +	  
		CASE LEN(MEMBER) WHEN  0 THEN '' ELSE '.' + MEMBER END  As NAME,EXPRESSION   
		FROM AuthorizationRules  
		WHERE RULESOURCE=@RULESOURCE AND RULETYPE =@RULETYPE  AND	ROOT=@ROOT AND ENTITY=@ENTITY
	END
	ELSE IF LEN(@ENTITY)> 0 AND LEN(@MEMBER)>0
	BEGIN
		SELECT RULEID, RULESOURCE,RULETYPE,ROOT,ENTITY,MEMBER,STR(RULESOURCE) + '.' +  RULETYPE + '.' + ROOT + 
		CASE LEN(ENTITY) WHEN 0 THEN '' ELSE '.' + ENTITY END  +	  
		CASE LEN(MEMBER) WHEN  0 THEN '' ELSE '.' + MEMBER END  As NAME,EXPRESSION  
		FROM AuthorizationRules  
		WHERE RULESOURCE=@RULESOURCE AND RULETYPE =@RULETYPE  AND	ROOT=@ROOT AND ENTITY=@ENTITY AND MEMBER LIKE @MEMBER + '%'
	END
RETURN(0)
GO
/****** Object:  StoredProcedure [dbo].[InsertRule]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertRule]  
	@RULESOURCE int,@RULETYPE nchar(1),
	@ROOT nvarchar(64),	@ENTITY nvarchar(64)='',@MEMBER nvarchar(128)='',  
	@EXPRESSION nvarchar(2000) AS  
BEGIN  
INSERT INTO AuthorizationRules  
	(RULESOURCE,RULETYPE, ROOT, ENTITY,MEMBER,EXPRESSION)  
VALUES
	(@RULESOURCE,@RULETYPE ,@ROOT,@ENTITY,@MEMBER,@EXPRESSION)  
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_Applications]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Applications] as
Begin
if exists (select * from sysobjects where id = object_id(N'[FK_App_Mem]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_Membership] DROP CONSTRAINT FK_App_Mem


if exists (select * from sysobjects where id = object_id(N'[FK_App_Pat]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_Paths] DROP CONSTRAINT FK_App_Pat


if exists (select * from sysobjects where id = object_id(N'[FK_App_Rol]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_Roles] DROP CONSTRAINT FK_App_Rol


if exists (select * from sysobjects where id = object_id(N'[FK_App_User]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_Users] DROP CONSTRAINT FK_App_User


if exists (select * from sysobjects where id = object_id(N'[aspnet_Applications]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_Applications]


CREATE TABLE [aspnet_Applications] (
	[ApplicationName] [nvarchar] (256)  NOT NULL ,
	[LoweredApplicationName] [nvarchar] (256) NOT NULL ,
	[ApplicationId] [uniqueidentifier] NOT NULL ,
	[Description] [nvarchar] (256)  NULL 
) ON [PRIMARY]


 CREATE  CLUSTERED  INDEX [aspnet_Applications_Index] ON [aspnet_Applications]([LoweredApplicationName]) ON [PRIMARY]


ALTER TABLE [aspnet_Applications] ADD 
	CONSTRAINT [DF_AppI] DEFAULT (newid()) FOR [ApplicationId],
	 PRIMARY KEY  NONCLUSTERED 
	(
		[ApplicationId]
	)  ON [PRIMARY] ,
	 UNIQUE  NONCLUSTERED 
	(
		[LoweredApplicationName]
	)  ON [PRIMARY] ,
	 UNIQUE  NONCLUSTERED 
	(
		[ApplicationName]
	)  ON [PRIMARY] 
End
GO
/****** Object:  StoredProcedure [dbo].[sp_AuthorizationRules]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AuthorizationRules] AS
BEGIN 
CREATE TABLE [AuthorizationRules] (
 [RULEID] [int] IDENTITY (1, 1) NOT NULL ,
 [RULESOURCE] [int] NOT NULL ,
 [RULETYPE] [nchar] (1)  NOT NULL ,
 [ROOT] [nvarchar] (64)  NOT NULL ,
 [ENTITY] [nvarchar] (64)  NOT NULL ,
 [MEMBER] [nvarchar] (128)  NULL  ,
 [EXPRESSION] [ntext]  NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
ALTER TABLE [AuthorizationRules] ADD 
 CONSTRAINT [PK_AuthorizationRules] PRIMARY KEY  CLUSTERED 
 (
  [RULEID]
 )  ON [PRIMARY] 

 CREATE INDEX IX_AuthorizationRules
    ON AuthorizationRules ([RULESOURCE], [RULETYPE],[ROOT],[ENTITY])
END
GO
/****** Object:  StoredProcedure [dbo].[sp_Membership]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Membership] as
Begin
if exists (select * from sysobjects where id = object_id(N'[aspnet_Membership]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_Membership]


CREATE TABLE [aspnet_Membership] (
	[ApplicationId] [uniqueidentifier] NOT NULL ,
	[UserId] [uniqueidentifier] NOT NULL ,
	[Password] [nvarchar] (128) NOT NULL ,
	[PasswordFormat] [int] NOT NULL ,
	[PasswordSalt] [nvarchar] (128) NOT NULL ,
	[MobilePIN] [nvarchar] (16)  NULL ,
	[Email] [nvarchar] (256)  NULL ,
	[LoweredEmail] [nvarchar] (256)  NULL ,
	[PasswordQuestion] [nvarchar] (256)  NULL ,
	[PasswordAnswer] [nvarchar] (128)  NULL ,
	[IsApproved] [bit] NOT NULL ,
	[IsLockedOut] [bit] NOT NULL ,
	[CreateDate] [datetime] NOT NULL ,
	[LastLoginDate] [datetime] NOT NULL ,
	[LastPasswordChangedDate] [datetime] NOT NULL ,
	[LastLockoutDate] [datetime] NOT NULL ,
	[FailedPasswordAttemptCount] [int] NOT NULL ,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL ,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL ,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL ,
	[Comment] [ntext]  NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


 CREATE  CLUSTERED  INDEX [aspnet_Membership_index] ON [aspnet_Membership]([ApplicationId], [LoweredEmail]) ON [PRIMARY]


ALTER TABLE [aspnet_Membership] ADD 
	CONSTRAINT [DF_MemI] DEFAULT (0) FOR [PasswordFormat],
	 PRIMARY KEY  NONCLUSTERED 
	(
		[UserId]
	)  ON [PRIMARY] 

/*
ALTER TABLE [aspnet_Membership] ADD 
	 FOREIGN KEY 
	(
		[ApplicationId]
	) REFERENCES [aspnet_Applications] (
		[ApplicationId]
	),
	 FOREIGN KEY 
	(
		[UserId]
	) REFERENCES [aspnet_Users] (
		[UserId]
	)
*/
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Paths]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Paths] as
Begin
if exists (select * from sysobjects where id = object_id(N'[FK_PathI]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_PersonalizationAllUsers] DROP CONSTRAINT FK_PathI


if exists (select * from sysobjects where id = object_id(N'[FK_PathII]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_PersonalizationPerUser] DROP CONSTRAINT FK_PathII


if exists (select * from sysobjects where id = object_id(N'[aspnet_Paths]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_Paths]


CREATE TABLE [aspnet_Paths] (
	[ApplicationId] [uniqueidentifier] NOT NULL ,
	[PathId] [uniqueidentifier] NOT NULL ,
	[Path] [nvarchar] (256) NOT NULL ,
	[LoweredPath] [nvarchar] (256) NOT NULL 
) ON [PRIMARY]


 CREATE  UNIQUE  CLUSTERED  INDEX [aspnet_Paths_index] ON [aspnet_Paths]([ApplicationId], [LoweredPath]) ON [PRIMARY]


ALTER TABLE [aspnet_Paths] ADD 
	CONSTRAINT [DF_PathI] DEFAULT (newid()) FOR [PathId],
	 PRIMARY KEY  NONCLUSTERED 
	(
		[PathId]
	)  ON [PRIMARY] 

/*
ALTER TABLE [aspnet_Paths] ADD 
	 FOREIGN KEY 
	(
		[ApplicationId]
	) REFERENCES [aspnet_Applications] (
		[ApplicationId]
	)
*/
end
GO
/****** Object:  StoredProcedure [dbo].[sp_PersonalizationAllUsers]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_PersonalizationAllUsers] as
Begin
if exists (select * from sysobjects where id = object_id(N'[aspnet_PersonalizationAllUsers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_PersonalizationAllUsers]


CREATE TABLE [aspnet_PersonalizationAllUsers] (
	[PathId] [uniqueidentifier] NOT NULL ,
	[PageSettings] [image] NOT NULL ,
	[LastUpdatedDate] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [aspnet_PersonalizationAllUsers] WITH NOCHECK ADD 
	 PRIMARY KEY  CLUSTERED 
	(
		[PathId]
	)  ON [PRIMARY] 

/*
ALTER TABLE [aspnet_PersonalizationAllUsers] ADD 
	 FOREIGN KEY 
	(
		[PathId]
	) REFERENCES [aspnet_Paths] (
		[PathId]
	)
*/
end
	
GO
/****** Object:  StoredProcedure [dbo].[sp_PersonalizationPerUser]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_PersonalizationPerUser] as
Begin
if exists (select * from sysobjects where id = object_id(N'[aspnet_PersonalizationPerUser]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_PersonalizationPerUser]


CREATE TABLE [aspnet_PersonalizationPerUser] (
	[Id] [uniqueidentifier] NOT NULL ,
	[PathId] [uniqueidentifier] NULL ,
	[UserId] [uniqueidentifier] NULL ,
	[PageSettings] [image] NOT NULL ,
	[LastUpdatedDate] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


 CREATE  UNIQUE  CLUSTERED  INDEX [aspnet_PersonalizationPerUser_index1] ON [aspnet_PersonalizationPerUser]([PathId], [UserId]) ON [PRIMARY]


ALTER TABLE [aspnet_PersonalizationPerUser] ADD 
	CONSTRAINT [DF_PerUsI] DEFAULT (newid()) FOR [Id],
	 PRIMARY KEY  NONCLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 


 CREATE  UNIQUE  INDEX [aspnet_PersonalizationPerUser_ncindex2] ON [aspnet_PersonalizationPerUser]([UserId], [PathId]) ON [PRIMARY]

/*
ALTER TABLE [aspnet_PersonalizationPerUser] ADD 
	 FOREIGN KEY 
	(
		[PathId]
	) REFERENCES [aspnet_Paths] (
		[PathId]
	),
	 FOREIGN KEY 
	(
		[UserId]
	) REFERENCES [aspnet_Users] (
		[UserId]
	)
*/
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Profile]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Profile] as
Begin
if exists (select * from sysobjects where id = object_id(N'[aspnet_Profile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_Profile]


CREATE TABLE [aspnet_Profile] (
	[UserId] [uniqueidentifier] NOT NULL ,
	[PropertyNames] [ntext] NOT NULL ,
	[PropertyValuesString] [ntext] NOT NULL ,
	[PropertyValuesBinary] [image] NOT NULL ,
	[LastUpdatedDate] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [aspnet_Profile] WITH NOCHECK ADD 
	 PRIMARY KEY  CLUSTERED 
	(
		[UserId]
	)  ON [PRIMARY] 

/*
ALTER TABLE [aspnet_Profile] ADD 
	 FOREIGN KEY 
	(
		[UserId]
	) REFERENCES [aspnet_Users] (
		[UserId]
	)
*/
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Roles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Roles] as
Begin
if exists (select * from sysobjects where id = object_id(N'[FK_RoleI]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_UsersInRoles] DROP CONSTRAINT FK_RoleI


if exists (select * from sysobjects where id = object_id(N'[aspnet_Roles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_Roles]


CREATE TABLE [aspnet_Roles] (
	[ApplicationId] [uniqueidentifier] NOT NULL ,
	[RoleId] [uniqueidentifier] NOT NULL ,
	[RoleName] [nvarchar] (256) NOT NULL ,
	[LoweredRoleName] [nvarchar] (256)  NOT NULL ,
	[Description] [nvarchar] (256)  NULL 
) ON [PRIMARY]


 CREATE  UNIQUE  CLUSTERED  INDEX [aspnet_Roles_index1] ON [aspnet_Roles]([ApplicationId], [LoweredRoleName]) ON [PRIMARY]


ALTER TABLE [aspnet_Roles] ADD 
	CONSTRAINT [DF_RoleI] DEFAULT (newid()) FOR [RoleId],
	 PRIMARY KEY  NONCLUSTERED 
	(
		[RoleId]
	)  ON [PRIMARY] 

/*
ALTER TABLE [aspnet_Roles] ADD 
	 FOREIGN KEY 
	(
		[ApplicationId]
	) REFERENCES [aspnet_Applications] (
		[ApplicationId]
	)
*/
end
GO
/****** Object:  StoredProcedure [dbo].[sp_SchemaVersions]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_SchemaVersions] as
Begin
if exists (select * from sysobjects where id = object_id(N'[aspnet_SchemaVersions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_SchemaVersions]


CREATE TABLE [aspnet_SchemaVersions] (
	[Feature] [nvarchar] (128) NOT NULL ,
	[CompatibleSchemaVersion] [nvarchar] (128) NOT NULL ,
	[IsCurrentVersion] [bit] NOT NULL 
) ON [PRIMARY]


ALTER TABLE [aspnet_SchemaVersions] WITH NOCHECK ADD 
	 PRIMARY KEY  CLUSTERED 
	(
		[Feature],
		[CompatibleSchemaVersion]
	)  ON [PRIMARY] 


INSERT INTO [aspnet_SchemaVersions] (  
                           [Feature],[CompatibleSchemaVersion],[IsCurrentVersion])  
                           SELECT  'common',1,1  
                           INSERT INTO [aspnet_SchemaVersions] (  
                           [Feature],	[CompatibleSchemaVersion],[IsCurrentVersion])  
                           SELECT 'health monitoring',1,1  
                           INSERT INTO [aspnet_SchemaVersions] (  
                           [Feature],[CompatibleSchemaVersion],[IsCurrentVersion])  
                           SELECT 'membership',1,1   
                           INSERT INTO [aspnet_SchemaVersions] (  
                           [Feature],[CompatibleSchemaVersion],[IsCurrentVersion])  
                           SELECT 'personalization',1,1  
                           INSERT INTO [aspnet_SchemaVersions] (  
                           [Feature],[CompatibleSchemaVersion],[IsCurrentVersion] ) 
                           SELECT 'profile',1,1  
                           INSERT INTO [aspnet_SchemaVersions] (  
                           [Feature], [CompatibleSchemaVersion], [IsCurrentVersion] )  
                           SELECT 'role manager',1,1  

end
GO
/****** Object:  StoredProcedure [dbo].[sp_Users]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_Users] as
Begin
if exists (select * from sysobjects where id = object_id(N'[FK_User_Mem]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_Membership] DROP CONSTRAINT FK_User_Mem


if exists (select * from sysobjects where id = object_id(N'[FK_User_Per]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_PersonalizationPerUser] DROP CONSTRAINT FK_User_Per


if exists (select * from sysobjects where id = object_id(N'[FK_User_Pro]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_Profile] DROP CONSTRAINT FK_User_Pro


if exists (select * from sysobjects where id = object_id(N'[FK_User_Us]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [aspnet_UsersInRoles] DROP CONSTRAINT FK_User_Us


if exists (select * from sysobjects where id = object_id(N'[aspnet_Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_Users]


CREATE TABLE [aspnet_Users] (
	[ApplicationId] [uniqueidentifier] NOT NULL ,
	[UserId] [uniqueidentifier] NOT NULL ,
	[UserName] [nvarchar] (256)  NOT NULL ,
	[LoweredUserName] [nvarchar] (256)  NOT NULL ,
	[MobileAlias] [nvarchar] (16)  NULL ,
	[IsAnonymous] [bit] NOT NULL ,
	[LastActivityDate] [datetime] NOT NULL 
) ON [PRIMARY]


 CREATE  UNIQUE  CLUSTERED  INDEX [aspnet_Users_Index] ON [aspnet_Users]([ApplicationId], [LoweredUserName]) ON [PRIMARY]


ALTER TABLE [aspnet_Users] ADD 
	CONSTRAINT [DF_UserI] DEFAULT (newid()) FOR [UserId],
	CONSTRAINT [DF_UserII] DEFAULT (null) FOR [MobileAlias],
	CONSTRAINT [DF_UserIII] DEFAULT (0) FOR [IsAnonymous],
	 PRIMARY KEY  NONCLUSTERED 
	(
		[UserId]
	)  ON [PRIMARY] 


 CREATE  INDEX [aspnet_Users_Index2] ON [aspnet_Users]([ApplicationId], [LastActivityDate]) ON [PRIMARY]


/*
ALTER TABLE [aspnet_Users] ADD 
	 FOREIGN KEY 
	(
		[ApplicationId]
	) REFERENCES [aspnet_Applications] (
		[ApplicationId]
	)
*/
end
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersInRoles]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_UsersInRoles] as
Begin
if exists (select * from sysobjects where id = object_id(N'[aspnet_UsersInRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_UsersInRoles]


CREATE TABLE [aspnet_UsersInRoles] (
	[UserId] [uniqueidentifier] NOT NULL ,
	[RoleId] [uniqueidentifier] NOT NULL 
) ON [PRIMARY]


ALTER TABLE [aspnet_UsersInRoles] WITH NOCHECK ADD 
	 PRIMARY KEY  CLUSTERED 
	(
		[UserId],
		[RoleId]
	)  ON [PRIMARY] 


 CREATE  INDEX [aspnet_UsersInRoles_index] ON [aspnet_UsersInRoles]([RoleId]) ON [PRIMARY]

/*
ALTER TABLE [aspnet_UsersInRoles] ADD 
	 FOREIGN KEY 
	(
		[RoleId]
	) REFERENCES [aspnet_Roles] (
		[RoleId]
	),
	 FOREIGN KEY 
	(
		[UserId]
	) REFERENCES [aspnet_Users] (
		[UserId]
	)
*/
end
GO
/****** Object:  StoredProcedure [dbo].[sp_WebEvent_Events]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[sp_WebEvent_Events] as
Begin
if exists (select * from sysobjects where id = object_id(N'[aspnet_WebEvent_Events]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [aspnet_WebEvent_Events]


CREATE TABLE [aspnet_WebEvent_Events] (
	[EventId] [char] (32)  NOT NULL ,
	[EventTimeUtc] [datetime] NOT NULL ,
	[EventTime] [datetime] NOT NULL ,
	[EventType] [nvarchar] (256)  NOT NULL ,
	[EventSequence] [decimal](19, 0) NOT NULL ,
	[EventOccurrence] [decimal](19, 0) NOT NULL ,
	[EventCode] [int] NOT NULL ,
	[EventDetailCode] [int] NOT NULL ,
	[Message] [nvarchar] (1024)  NULL ,
	[ApplicationPath] [nvarchar] (256)  NULL ,
	[ApplicationVirtualPath] [nvarchar] (256) NULL ,
	[MachineName] [nvarchar] (256)  NOT NULL ,
	[RequestUrl] [nvarchar] (1024) NULL ,
	[ExceptionType] [nvarchar] (256) NULL ,
	[Details] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [aspnet_WebEvent_Events] WITH NOCHECK ADD 
	 PRIMARY KEY  CLUSTERED 
	(
		[EventId]
	)  ON [PRIMARY] 
end 
GO
/****** Object:  StoredProcedure [dbo].[UpdateRuleById]    Script Date: 11/04/2018 11:20:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRuleById]  
	@id INT,   
	@RULESOURCE int,@RULETYPE nchar(1),@ROOT   
	nvarchar(64),@ENTITY nvarchar(64)='',
	@MEMBER nvarchar(128)='',  
	@EXPRESSION nvarchar(2000) AS  
BEGIN  
UPDATE AuthorizationRules  
SET  
	EXPRESSION= @EXPRESSION,  
	RULESOURCE=@RULESOURCE,RULETYPE=@RULETYPE ,  
	ROOT=@ROOT,
	ENTITY=@ENTITY, MEMBER=@MEMBER  
WHERE 
	RULEID= @id  
END 
GO
USE [master]
GO
ALTER DATABASE [SOLAR6] SET  READ_WRITE 
GO
