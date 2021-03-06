USE [master]
GO
/****** Object:  Database [KIRLIOGLUDOSYALAR]    Script Date: 05/04/2018 10:36:05 ******/
CREATE DATABASE [KIRLIOGLUDOSYALAR]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'KIRLIOGLUDOSYALAR', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\KIRLIOGLU.mdf' , SIZE = 315392KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'KIRLIOGLUDOSYALAR_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\KIRLIOGLU.ldf' , SIZE = 63424KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KIRLIOGLUDOSYALAR].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET ARITHABORT OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET  MULTI_USER 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET DB_CHAINING OFF 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET QUERY_STORE = OFF
GO
USE [KIRLIOGLUDOSYALAR]
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
USE [KIRLIOGLUDOSYALAR]
GO
/****** Object:  Schema [usr]    Script Date: 05/04/2018 10:36:05 ******/
CREATE SCHEMA [usr]
GO
/****** Object:  Schema [UYS]    Script Date: 05/04/2018 10:36:05 ******/
CREATE SCHEMA [UYS]
GO
/****** Object:  UserDefinedFunction [dbo].[EXCEL_TeslimAlmaRaporu]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION  [dbo].[EXCEL_TeslimAlmaRaporu]
(	
)
RETURNS @RtnValue table 
(
	SatinalmaReferansNo VARCHAR(50),
	SatinalmaReferansTarihi DATETIME,
	TeslimAlmaNo VARCHAR(50),
	TeslimAlmaTarihi DATETIME,
	MalzemeKodu VARCHAR(50),
	MalzemeAdi VARCHAR(50),
	Birim VARCHAR(5),
	TeslimAlmaBirimMiktar NUMERIC(24,6),
	SatinalmaReferansBirimMiktar NUMERIC(24,6)
) 
AS

BEGIN


INSERT INTO @RtnValue(SatinalmaReferansNo, SatinalmaReferansTarihi,TeslimAlmaNo, TeslimAlmaTarihi, MalzemeKodu, MalzemeAdi, Birim, TeslimAlmaBirimMiktar, SatinalmaReferansBirimMiktar )
SELECT 
	(CASE WHEN A.RowID=1 THEN A.SatinalmaReferansNo ELSE '' END) AS SatinalmaReferansNo ,
	A.SatinalmaReferansTarihi,
	(CASE WHEN A.RowID=1 THEN A.TeslimAlmaNo ELSE '' END) AS TeslimAlmaNo,
	A. TeslimAlmaTarihi,
	A.MalzemeKodu, A.MalzemeAdi, A.Birim, A.TeslimAlmaBirimMiktar, A.SatinalmaReferansBirimMiktar
FROM
(
	SELECT ROW_NUMBER() OVER(PARTITION BY TAG.EvrakNo ORDER By TAG.EvrakNo) AS RowID, TAG.EvrakNo AS TeslimAlmaNo, TAG.Tarih AS TeslimAlmaTarihi, 
		ISNULL(TAG.SatinalmaReferansNo,'') AS SatinalmaReferansNo, 
		ISNULL((SELECT TOP(1) TAL.Tarih FROM KIRLIOGLUDOSYALAR.UYS.TALEP(NOLOCK) TAL WHERE TAL.OnayDurum = 1 AND TAL.ReferansNo=TAG.SatinalmaReferansNo AND TAL.MalKodu=TAG.MalKodu),'') AS SatinalmaReferansTarihi,
		TAG.MalKodu AS MalzemeKodu, ISNULL(STK.MalAdi,'') AS MalzemeAdi,
		TAG.Birim AS Birim, TAG.BirimMiktar AS TeslimAlmaBirimMiktar,
		ISNULL((SELECT SUM(Miktar) FROM KIRLIOGLUDOSYALAR.UYS.TALEP(NOLOCK) TAL WHERE TAL.OnayDurum = 1 AND TAL.ReferansNo=TAG.SatinalmaReferansNo AND TAL.MalKodu=TAG.MalKodu),0) AS SatinalmaReferansBirimMiktar
	FROM KIRLIOGLUDOSYALAR.UYS.TAG (NOLOCK) TAG
		 LEFT JOIN FINSAT661.FINSAT661.STK(NOLOCK) STK ON STK.MalKodu=TAG.MalKodu
	WHERE EvrakTip=1 AND datediff(d,0,CONVERT(DATETIME,CONVERT(DATE, TAG.Tarih))+2) >=42736--01.01.2017
)A
ORDER BY A.TeslimAlmaNo


RETURN
END





GO
/****** Object:  UserDefinedFunction [dbo].[EXCEL_TeslimAlmaRaporu_2017]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION  [dbo].[EXCEL_TeslimAlmaRaporu_2017]
(	
)
RETURNS @RtnValue table 
(
	SatinalmaReferansNo VARCHAR(50),
	SatinalmaReferansTarihi DATETIME,
	TeslimAlmaNo VARCHAR(50),
	TeslimAlmaTarihi DATETIME,
	MalzemeKodu VARCHAR(50),
	MalzemeAdi VARCHAR(50),
	Birim VARCHAR(5),
	TeslimAlmaBirimMiktar NUMERIC(24,6),
	SatinalmaReferansBirimMiktar NUMERIC(24,6)
) 
AS

BEGIN


INSERT INTO @RtnValue(SatinalmaReferansNo, SatinalmaReferansTarihi,TeslimAlmaNo, TeslimAlmaTarihi, MalzemeKodu, MalzemeAdi, Birim, TeslimAlmaBirimMiktar, SatinalmaReferansBirimMiktar )
SELECT 
	(CASE WHEN A.RowID=1 THEN A.SatinalmaReferansNo ELSE '' END) AS SatinalmaReferansNo ,
	A.SatinalmaReferansTarihi,
	(CASE WHEN A.RowID=1 THEN A.TeslimAlmaNo ELSE '' END) AS TeslimAlmaNo,
	A. TeslimAlmaTarihi,
	A.MalzemeKodu, A.MalzemeAdi, A.Birim, A.TeslimAlmaBirimMiktar, A.SatinalmaReferansBirimMiktar
FROM
(
	SELECT ROW_NUMBER() OVER(PARTITION BY TAG.EvrakNo ORDER By TAG.EvrakNo) AS RowID, TAG.EvrakNo AS TeslimAlmaNo, TAG.Tarih AS TeslimAlmaTarihi, 
		ISNULL(TAG.SatinalmaReferansNo,'') AS SatinalmaReferansNo, 
		ISNULL((SELECT TOP(1) TAL.Tarih FROM KIRLIOGLUDOSYALAR.UYS.TALEP(NOLOCK) TAL WHERE TAL.OnayDurum = 1 AND TAL.ReferansNo=TAG.SatinalmaReferansNo AND TAL.MalKodu=TAG.MalKodu),'') AS SatinalmaReferansTarihi,
		TAG.MalKodu AS MalzemeKodu, ISNULL(STK.MalAdi,'') AS MalzemeAdi,
		TAG.Birim AS Birim, TAG.BirimMiktar AS TeslimAlmaBirimMiktar,
		ISNULL((SELECT SUM(Miktar) FROM KIRLIOGLUDOSYALAR.UYS.TALEP(NOLOCK) TAL WHERE TAL.OnayDurum = 1 AND TAL.ReferansNo=TAG.SatinalmaReferansNo AND TAL.MalKodu=TAG.MalKodu),0) AS SatinalmaReferansBirimMiktar
	FROM KIRLIOGLUDOSYALAR.UYS.TAG (NOLOCK) TAG
		 LEFT JOIN FINSAT661.FINSAT661.STK(NOLOCK) STK ON STK.MalKodu=TAG.MalKodu
	WHERE EvrakTip=1 AND datediff(d,0,CONVERT(DATETIME,CONVERT(DATE, TAG.Tarih))+2) >=42583--01.02.2016
)A
ORDER BY A.TeslimAlmaNo


RETURN
END





GO
/****** Object:  Table [dbo].[AdrSTK]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdrSTK](
	[MalKodu] [nvarchar](30) NOT NULL,
	[MalAdi] [nvarchar](150) NULL,
	[AdrGrup] [nvarchar](350) NULL,
	[AdrAdi] [nvarchar](350) NULL,
	[Kaydeden] [nvarchar](150) NULL,
	[KayitTarih] [int] NULL,
	[KayitSaat] [int] NULL,
	[Degistiren] [nvarchar](150) NULL,
	[DegisTarih] [int] NULL,
	[DegisSaat] [int] NULL,
 CONSTRAINT [PK_MalAdrKayıt] PRIMARY KEY CLUSTERED 
(
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdrTanimlari]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdrTanimlari](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AdrGrup] [varchar](200) NOT NULL,
	[AdrAdi] [varchar](200) NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [int] NULL,
	[KayitSaat] [int] NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [int] NULL,
	[DegisSaat] [int] NULL,
	[Birim] [varchar](20) NULL,
 CONSTRAINT [PK_AdrTanimlari_1] PRIMARY KEY CLUSTERED 
(
	[AdrGrup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Arac]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Arac](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Arac] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Arac] PRIMARY KEY CLUSTERED 
(
	[Arac] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Belgeler]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Belgeler](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[TabloAdi] [varchar](100) NOT NULL,
	[TabloId] [int] NOT NULL,
	[SiraNo] [int] NOT NULL,
	[Aciklama] [varchar](500) NULL,
	[DosyaAd] [varchar](max) NULL,
	[DosyaIcerik] [varbinary](max) NULL,
	[DosyaTip] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Belgeler] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BelgelerIzin]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BelgelerIzin](
	[UserID] [int] NOT NULL,
	[GuvenlikBelgesi1] [bit] NOT NULL,
	[GuvenlikBelgesi2] [bit] NOT NULL,
	[DepolamaIzinBelgesi] [bit] NOT NULL,
	[SatisIzinBelgesi] [bit] NOT NULL,
	[PatlayiciMaddeRuhsati] [bit] NOT NULL,
	[PatlayiciMaddeSigortasi] [bit] NOT NULL,
	[Arac1Ruhsati] [bit] NOT NULL,
	[Arac1KBelgesi] [bit] NOT NULL,
	[Arac1Sigortasi] [bit] NOT NULL,
	[Arac1PatlayiciMaddeSigortasi] [bit] NOT NULL,
	[Arac2Ruhsati] [bit] NOT NULL,
	[Arac2KBelgesi] [bit] NOT NULL,
	[Arac2Sigortasi] [bit] NOT NULL,
	[Arac2PatlayiciMaddeSigortasi] [bit] NOT NULL,
	[ProformaFatura] [bit] NOT NULL,
 CONSTRAINT [PK_BelgelerIzin] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 05/04/2018 10:36:05 ******/
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
/****** Object:  Table [dbo].[FYT_Temp]    Script Date: 05/04/2018 10:36:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FYT_Temp](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[Kod] [varchar](20) NOT NULL,
	[ParaBirimi] [varchar](10) NOT NULL,
	[Bayi] [numeric](24, 6) NOT NULL,
	[Toptan] [numeric](24, 6) NOT NULL,
	[Perakende] [numeric](24, 6) NOT NULL,
 CONSTRAINT [PK_FYT_Temp] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GorusmeDetaylari]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GorusmeDetaylari](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[KullaniciAdi] [varchar](50) NULL,
	[HesapKodu] [varchar](20) NULL,
	[Tarih] [datetime] NULL,
	[Detay] [varchar](250) NULL,
	[Hatirlatma] [bit] NULL,
	[HatirlatmaTarihi] [datetime] NULL,
	[GorusmeTarihi] [datetime] NULL,
	[Tip] [smallint] NULL,
	[Aciklama] [varchar](250) NULL,
 CONSTRAINT [PK_GorusmeDetaylari] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IZINBELGELERI]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IZINBELGELERI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SIRKETKODU] [varchar](2) NOT NULL,
	[HESAPKODU] [nvarchar](50) NOT NULL,
	[DOSYAYOL] [nvarchar](50) NOT NULL,
	[KISAAD] [nvarchar](50) NOT NULL,
	[ACIKLAMA] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_IZINBELGELERI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SIRKETKODU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Izinler]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Izinler](
	[SirketKodu] [varchar](2) NOT NULL,
	[IzinID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[MusteriIslemleri] [bit] NULL,
	[StokIslemleri] [bit] NULL,
	[TahsilatTakipIslemleri] [bit] NULL,
	[GosterilecekSirketler] [varchar](150) NULL,
	[TahsilatSorumlusu] [bit] NULL,
	[EkstreIslemleri] [bit] NULL,
	[OdenmemisFaturalar] [bit] NULL,
	[AyrFatListesi] [bit] NULL,
	[SatisAnalizRaporu] [bit] NULL,
	[VadeFarkiHesap] [bit] NULL,
	[MustIzinBelgeleri] [bit] NULL,
	[TekveSozlesmeler] [bit] NULL,
	[GorusmeDetayi] [bit] NULL,
	[GostCHKKodAlani] [varchar](50) NULL,
	[GostCHKDeger] [varchar](150) NULL,
	[RiskRaporu] [bit] NULL,
	[Personeller] [bit] NULL,
	[Ruhsat] [bit] NULL,
	[Siparis] [bit] NULL,
	[DepolarArasiTransfer] [bit] NULL,
	[Sevkiyat] [bit] NULL,
	[Notlar] [bit] NULL,
	[Seri] [int] NULL,
	[FinansIslemleri] [bit] NULL,
	[Icmal] [bit] NULL,
	[RaporTanimlari] [bit] NULL,
	[GosterilecekDepolar] [varchar](50) NULL,
	[FatIr] [bit] NULL,
	[FiyatIskonto] [bit] NULL,
	[FiyatIskontoOnay] [bit] NULL,
	[GostSTKKodAlani] [varchar](50) NULL,
	[GostSTKDeger] [varchar](150) NULL,
	[CariKartSiparisDurumuAc] [bit] NULL,
	[CariKartSiparisDurumuKapat] [bit] NULL,
	[GuvenlikBelgesi1] [bit] NULL,
	[GuvenlikBelgesi2] [bit] NULL,
	[DepolamaIzinBelgesi] [bit] NULL,
	[SatisIzinBelgesi] [bit] NULL,
	[PatlayiciMaddeRuhsati] [bit] NULL,
	[PatlayiciMaddeSigortasi] [bit] NULL,
	[Arac1Ruhsati] [bit] NULL,
	[Arac1KBelgesi] [bit] NULL,
	[Arac1Sigortasi] [bit] NULL,
	[Arac1PatlayiciMaddeSigortasi] [bit] NULL,
	[Arac2Ruhsati] [bit] NULL,
	[Arac2KBelgesi] [bit] NULL,
	[Arac2Sigortasi] [bit] NULL,
	[Arac2PatlayiciMaddeSigortasi] [bit] NULL,
	[ProformaFatura] [bit] NULL,
 CONSTRAINT [PK_Izinler] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KayitKontrol]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KayitKontrol](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tip] [int] NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[KullaniciAdi] [varchar](50) NOT NULL,
	[Tarih] [datetime] NOT NULL,
	[HesapKodu] [varchar](30) NULL,
	[Unvan] [varchar](85) NULL,
	[TahsilatSorumlusu] [int] NULL,
	[FaturaAdediLimit] [int] NULL,
	[FaturaAdediGercek] [int] NULL,
	[KrediLimiti] [numeric](25, 6) NULL,
	[Bakiye] [numeric](25, 6) NULL,
	[KritikGunLimit] [int] NULL,
	[KritikGunGercek] [int] NULL,
	[SenetCek] [int] NULL,
	[KaynakEvrakTip] [int] NULL,
 CONSTRAINT [PK_KayitKontrol] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[Tip] ASC,
	[SirketKodu] ASC,
	[KullaniciAdi] ASC,
	[Tarih] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KullanimYerleri]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KullanimYerleri](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RuhsatID] [int] NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[KlnmYeri] [varchar](40) NOT NULL,
	[SahaNo] [varchar](40) NULL,
	[ErisimNo] [varchar](15) NULL,
	[Turu] [varchar](40) NOT NULL,
	[Guz1] [varchar](100) NOT NULL,
	[Guz2] [varchar](100) NOT NULL,
	[Guz3] [varchar](100) NOT NULL,
	[Guz4] [varchar](100) NOT NULL,
	[SBas] [smalldatetime] NOT NULL,
	[SBit] [smalldatetime] NOT NULL,
	[DinamitToplamMiktar] [decimal](18, 6) NOT NULL,
	[DinamitKritikLimit] [decimal](18, 6) NOT NULL,
	[DinamitTekSefer] [decimal](18, 6) NOT NULL,
	[AnfoToplamMiktar] [decimal](18, 6) NOT NULL,
	[AnfoKritikLimit] [decimal](18, 6) NOT NULL,
	[AnfoTekSefer] [decimal](18, 6) NOT NULL,
	[KapsulToplamMiktar] [decimal](18, 6) NOT NULL,
	[KapsulKritikLimit] [decimal](18, 6) NOT NULL,
	[KapsulTekSefer] [decimal](18, 6) NOT NULL,
	[KapsulTekSeferElektrikli] [decimal](18, 6) NULL,
	[KapsulTekSeferElektriksiz] [decimal](18, 6) NULL,
	[FitilToplamMiktar] [decimal](18, 6) NOT NULL,
	[FitilKritikLimit] [decimal](18, 6) NOT NULL,
	[FitilTekSefer] [decimal](18, 6) NOT NULL,
	[PatlamaYilda] [int] NOT NULL,
	[PatlamaAyda] [int] NOT NULL,
	[PatlamaHaftada] [int] NOT NULL,
	[MuvafakatMiktari] [decimal](18, 6) NOT NULL,
	[Ilce] [varchar](50) NULL,
	[DinamitHafta] [decimal](18, 2) NULL,
	[DinamitAy] [decimal](18, 2) NULL,
	[DinamitYil] [decimal](18, 2) NULL,
	[AnfoHafta] [decimal](18, 2) NULL,
	[AnfoAy] [decimal](18, 2) NULL,
	[AnfoYil] [decimal](18, 2) NULL,
	[KapsulHafta] [decimal](18, 2) NULL,
	[KapsulAy] [decimal](18, 2) NULL,
	[KapsulYil] [decimal](18, 2) NULL,
	[FitilHafta] [decimal](18, 2) NULL,
	[FitilAy] [decimal](18, 2) NULL,
	[FitilYil] [decimal](18, 2) NULL,
	[KonaklamaYeri] [varchar](50) NULL,
	[Kaydeden] [varchar](50) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](50) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_KullanimYerleri] PRIMARY KEY CLUSTERED 
(
	[RuhsatID] ASC,
	[SiraNo] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MacAdresleri]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MacAdresleri](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Adres] [varchar](17) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
 CONSTRAINT [PK_MacAdresleri] PRIMARY KEY CLUSTERED 
(
	[Adres] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailLog]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MailAdi] [varchar](50) NOT NULL,
	[Tarih] [datetime] NULL,
	[Durum] [bit] NULL,
	[Aciklama] [varchar](max) NULL,
 CONSTRAINT [PK_MailLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[MailAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MutabakatMektubu]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MutabakatMektubu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [nchar](2) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[Unvan] [varchar](100) NOT NULL,
	[Bakiye] [decimal](18, 2) NOT NULL,
	[GonderimTarihi] [int] NOT NULL,
	[TarihAraligi1] [int] NULL,
	[TarihAraligi2] [int] NULL,
	[Turu] [varchar](10) NULL,
	[DegisimTarihi] [int] NOT NULL,
	[Durum] [smallint] NULL,
	[Degistiren] [varchar](20) NOT NULL,
	[AdSoyad] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
 CONSTRAINT [PK_MutabakatMektubu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personel]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[Ad] [varchar](20) NOT NULL,
	[Soyad] [varchar](20) NOT NULL,
	[KimlikNo] [varchar](11) NOT NULL,
	[Tel] [varchar](12) NULL,
	[GuvenlikBelgeBasTarih] [smalldatetime] NULL,
	[GuvenlikBelgeBitTarih] [smalldatetime] NULL,
	[AteslemeBelgeBasTarih] [smalldatetime] NULL,
	[AteslemeBelgeBitTarih] [smalldatetime] NULL,
	[Atesleme] [bit] NOT NULL,
	[Guvenlik] [bit] NOT NULL,
	[Sofor] [bit] NOT NULL,
	[GuvenlikBelgeNo] [varchar](20) NULL,
 CONSTRAINT [PK_Personel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Personel] UNIQUE NONCLUSTERED 
(
	[KimlikNo] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique2_Personel] UNIQUE NONCLUSTERED 
(
	[Ad] ASC,
	[Soyad] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personel2]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personel2](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[Ad] [varchar](20) NOT NULL,
	[Soyad] [varchar](20) NOT NULL,
	[KimlikNo] [varchar](11) NOT NULL,
	[Tel] [varchar](12) NULL,
	[GuvenlikBelgeBasTarih] [smalldatetime] NULL,
	[GuvenlikBelgeBitTarih] [smalldatetime] NULL,
	[AteslemeBelgeBasTarih] [smalldatetime] NULL,
	[AteslemeBelgeBitTarih] [smalldatetime] NULL,
	[Atesleme] [bit] NOT NULL,
	[Guvenlik] [bit] NOT NULL,
	[Sofor] [bit] NOT NULL,
	[GuvenlikBelgeNo] [varchar](20) NULL,
 CONSTRAINT [PK_Personel2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Personel2] UNIQUE NONCLUSTERED 
(
	[KimlikNo] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique2_Personel2] UNIQUE NONCLUSTERED 
(
	[Ad] ASC,
	[Soyad] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRGVersiyon]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRGVersiyon](
	[Program] [varchar](50) NOT NULL,
	[Versiyon] [varchar](15) NULL,
	[Aciklama] [varchar](500) NULL,
	[Guncelleyen] [varchar](20) NULL,
 CONSTRAINT [PK_PRGVersiyon] PRIMARY KEY CLUSTERED 
(
	[Program] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RaporTanimlari]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RaporTanimlari](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[Depo] [varchar](50) NOT NULL,
	[Baslik1] [varchar](500) NULL,
	[Baslik2] [varchar](500) NULL,
	[Baslik3] [varchar](500) NULL,
	[EmniyetMudurlugu] [varchar](100) NULL,
	[DepoAdresi] [varchar](100) NULL,
	[UnvanGorunsun] [bit] NULL,
	[IzinDilekcesiBaslik] [varchar](100) NULL,
	[TelGorunsun] [bit] NULL,
	[TasimaIzinBelgesi] [varchar](500) NULL,
	[GorevliAdSoyad] [varchar](50) NULL,
	[Unvan1] [varchar](500) NULL,
	[Unvan2] [varchar](500) NULL,
 CONSTRAINT [PK_RaporTanimlari] PRIMARY KEY CLUSTERED 
(
	[Depo] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Referans]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Referans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](20) NOT NULL,
	[Aciklama] [varchar](50) NULL,
	[Marka] [varchar](64) NULL,
	[Renk] [varchar](64) NULL,
	[Kapasite] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Referans] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[Kod] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Referans2]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Referans2](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](20) NOT NULL,
	[Aciklama] [varchar](50) NULL,
	[Marka] [varchar](64) NULL,
	[Renk] [varchar](64) NULL,
 CONSTRAINT [PK_Referans2] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[Kod] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RiskTanimlari]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RiskTanimlari](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[KullaniciAdi] [varchar](50) NOT NULL,
	[TanimAdi] [varchar](50) NOT NULL,
	[ChkSecimi] [bit] NULL,
	[HesapKodu1] [varchar](50) NULL,
	[HesapKodu2] [varchar](50) NULL,
	[ChkKodAlani1Secimi] [bit] NULL,
	[ChkKodAlani1] [varchar](50) NULL,
	[ChkKodAlani1Deger] [varchar](50) NULL,
	[ChkKodAlani2Secimi] [bit] NULL,
	[ChkKodAlani2] [varchar](50) NULL,
	[ChkKodAlani2Deger] [varchar](50) NULL,
	[TahsilatSrmSecimi] [bit] NULL,
	[TahsilatSorumlulari] [varchar](500) NULL,
	[FaturaSayisiSecimi] [bit] NULL,
	[FaturaSayisiDeger] [int] NULL,
	[CekSenetSayisiSecimi] [bit] NULL,
	[CekSenetSayisiDeger] [int] NULL,
	[FaturaVadesiSecimi] [bit] NULL,
	[FaturaVadesiDeger] [int] NULL,
	[SabitKrediLimiti] [bit] NULL,
	[SabitKrediLimitiSecimi] [bit] NULL,
	[SabitKrediLimitiDeger] [numeric](25, 6) NULL,
	[DegiskenKrediLimiti] [bit] NULL,
	[Tarih1] [varchar](10) NULL,
	[Tarih2] [varchar](10) NULL,
	[Oran] [int] NULL,
	[TarihSecimi] [bit] NULL,
	[BaslangicTarihi] [varchar](10) NULL,
	[BitisTarihi] [varchar](10) NULL,
 CONSTRAINT [PK_RiskTanimlari] PRIMARY KEY CLUSTERED 
(
	[SirketKodu] ASC,
	[KullaniciAdi] ASC,
	[TanimAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ruhsat]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ruhsat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RuhsatNo] [varchar](20) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[VerilisTarihi] [smalldatetime] NOT NULL,
	[BitisTarihi] [smalldatetime] NOT NULL,
	[R_Il] [varchar](40) NOT NULL,
	[R_Ilce] [varchar](40) NOT NULL,
	[R_Mahalle] [varchar](40) NOT NULL,
	[R_Koy] [varchar](40) NOT NULL,
	[R_Mevki] [varchar](40) NOT NULL,
	[RuhsatSahibi] [varchar](20) NOT NULL,
	[MusteriDepoKapasite] [decimal](18, 6) NOT NULL,
	[DinamitDevirTarih] [smalldatetime] NULL,
	[DinamitDevir] [decimal](18, 6) NULL,
	[AnfoDevirTarih] [smalldatetime] NULL,
	[AnfoDevir] [decimal](18, 6) NULL,
	[KapsulDevirTarih] [smalldatetime] NULL,
	[KapsulDevir] [decimal](18, 6) NULL,
	[FitilDevirTarih] [smalldatetime] NULL,
	[FitilDevir] [decimal](18, 6) NULL,
	[YildaPatDevirTarih] [smalldatetime] NOT NULL,
	[YildaPatDevir] [int] NOT NULL,
	[AydaPatDevirTarih] [smalldatetime] NOT NULL,
	[AydaPatDevir] [int] NOT NULL,
	[HaftadaPatDevirTarih] [smalldatetime] NOT NULL,
	[HaftadaPatDevir] [int] NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[ErisimNo] [varchar](64) NULL,
	[KiralananFirma] [varchar](30) NULL,
	[SiparisDurumu] [smallint] NULL,
	[SiparisDurumuTip] [smallint] NULL,
	[Aciklama] [varchar](254) NULL,
	[Kullanici] [varchar](5) NULL,
	[IslemTarihi] [int] NULL,
 CONSTRAINT [PK_Ruhsat_1] PRIMARY KEY CLUSTERED 
(
	[RuhsatNo] ASC,
	[VerilisTarihi] ASC,
	[RuhsatSahibi] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RuhsatDevir]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RuhsatDevir](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[RuhsatID] [int] NOT NULL,
	[PatlayiciTipi] [smallint] NOT NULL,
	[Tarih] [smalldatetime] NOT NULL,
	[Miktar] [decimal](18, 6) NOT NULL,
	[Type] [smallint] NOT NULL,
 CONSTRAINT [PK_RuhsatDevir] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RuhsatPersonel]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RuhsatPersonel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RuhsatID] [int] NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[PersonelID] [int] NULL,
	[AteslemePersMi] [bit] NOT NULL,
	[AdSoyad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RuhsatPersonel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sevkiyat]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sevkiyat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[SiparisNo] [varchar](8) NOT NULL,
	[GidecegiYer] [varchar](50) NULL,
	[Sofor1] [varchar](50) NULL,
	[Arac1] [varchar](50) NULL,
	[Sofor2] [varchar](50) NULL,
	[Arac2] [varchar](50) NULL,
	[Arac1Guzergah] [varchar](100) NULL,
	[Arac2Guzergah] [varchar](100) NULL,
	[Guvenlik1] [varchar](50) NULL,
	[Guvenlik2] [varchar](50) NULL,
	[NakilTarihSaat] [smalldatetime] NULL,
	[CiltNo] [varchar](10) NULL,
	[SayfaNo] [varchar](10) NULL,
	[TeslimTarih] [smalldatetime] NULL,
	[AteslemePersoneli] [varchar](50) NULL,
	[VerildigiTarih] [datetime] NULL,
	[VerildigiSaat] [varchar](5) NULL,
	[KonaklamaYeri] [varchar](50) NULL,
	[Durumu] [varchar](20) NULL,
	[Kaydeden] [varchar](50) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](50) NULL,
	[DegisTarih] [smalldatetime] NULL,
	[A1Dinamit] [bit] NULL,
	[A1Anfo] [bit] NULL,
	[A1Kapsul] [bit] NULL,
	[A1Fitil] [bit] NULL,
	[A2Dinamit] [bit] NULL,
	[A2Anfo] [bit] NULL,
	[A2Kapsul] [bit] NULL,
	[A2Fitil] [bit] NULL,
	[Arac1Miktar] [numeric](18, 2) NULL,
	[Arac2Miktar] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Sevkiyat] PRIMARY KEY CLUSTERED 
(
	[SiparisNo] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sorgular]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sorgular](
	[Tip] [varchar](50) NULL,
	[Sorgu] [varchar](max) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_SQL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sorumlular]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sorumlular](
	[SorumluID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[SorumluKodu] [varchar](10) NOT NULL,
	[AdiSoyadi] [varchar](50) NULL,
	[Telefon] [varchar](14) NULL,
	[Email] [varchar](max) NULL,
 CONSTRAINT [PK_Sorumlular] PRIMARY KEY CLUSTERED 
(
	[SorumluID] ASC,
	[SorumluKodu] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TahsilatTakip]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TahsilatTakip](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[KullaniciAdi] [varchar](50) NOT NULL,
	[Tarih] [datetime] NOT NULL,
	[HesapKodu] [varchar](30) NULL,
	[Unvan] [varchar](85) NULL,
	[TahsilatSorumlusu] [int] NULL,
	[FaturaAdediLimit] [int] NULL,
	[FaturaAdediGercek] [int] NULL,
	[KrediLimiti] [numeric](25, 6) NULL,
	[Bakiye] [numeric](25, 6) NULL,
	[KritikGunLimit] [int] NULL,
	[KritikGunGercek] [int] NULL,
	[SenetCek] [int] NULL,
 CONSTRAINT [PK_TahsilatTakip] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SirketKodu] ASC,
	[KullaniciAdi] ASC,
	[Tarih] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tanimlar]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tanimlar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[MailServer] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[CHKSorumluKodu] [varchar](50) NULL,
 CONSTRAINT [PK_Tanimlar] PRIMARY KEY CLUSTERED 
(
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TEKLIFSOZLESMELER]    Script Date: 05/04/2018 10:36:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TEKLIFSOZLESMELER](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SIRKETKODU] [varchar](2) NOT NULL,
	[HESAPKODU] [nvarchar](50) NOT NULL,
	[DOSYAYOL] [nvarchar](200) NOT NULL,
	[KISAAD] [nvarchar](50) NOT NULL,
	[ACIKLAMA] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_TEKLIFSOZLESMELER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[SIRKETKODU] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempSPI]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempSPI](
	[SirketKodu] [varchar](2) NOT NULL,
	[EvrakNo] [varchar](16) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Arac1] [varchar](50) NULL,
	[Arac1Miktar] [numeric](25, 6) NULL,
	[Arac2] [varchar](50) NULL,
	[Arac2Miktar] [numeric](25, 6) NULL,
	[Kaydeden] [varchar](50) NULL,
	[KayitTarih] [int] NULL,
	[Degistiren] [varchar](50) NULL,
	[DegisTarih] [int] NULL,
	[ROW_ID] [int] NOT NULL,
 CONSTRAINT [PK_TempSPI_1] PRIMARY KEY CLUSTERED 
(
	[SirketKodu] ASC,
	[EvrakNo] ASC,
	[HesapKodu] ASC,
	[SiraNo] ASC,
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[KullaniciAdi] [varchar](50) NOT NULL,
	[Sifre] [varchar](50) NULL,
	[AdiSoyadi] [varchar](50) NULL,
	[Grup] [varchar](5) NULL,
	[HataliGirisSayisi] [smallint] NULL,
	[Uretim] [smallint] NULL,
	[Email] [varchar](100) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[KullaniciAdi] ASC,
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[Izin]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Izin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Izin] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Izin] PRIMARY KEY CLUSTERED 
(
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[Kullanici]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Kullanici](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [nvarchar](10) NOT NULL,
	[Sifre] [nvarchar](50) NOT NULL,
	[AdSoyad] [nvarchar](100) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Rol] [nvarchar](100) NULL,
	[GosterilecekSirket] [nvarchar](100) NOT NULL,
	[GosterilecekDepo] [nvarchar](100) NOT NULL,
	[PasifStokKartlariGosterilsinmi] [bit] NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[KullaniciIzin]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[KullaniciIzin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [nvarchar](50) NOT NULL,
	[Izin] [nvarchar](100) NOT NULL,
	[Okuma] [bit] NOT NULL,
	[Yazma] [bit] NOT NULL,
 CONSTRAINT [PK_KullaniciIzin] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[Rol]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Rol](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[RolIzin]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[RolIzin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [nvarchar](100) NOT NULL,
	[Izin] [nvarchar](100) NOT NULL,
	[Okuma] [bit] NOT NULL,
	[Yazma] [bit] NOT NULL,
 CONSTRAINT [PK_RolIzin] PRIMARY KEY CLUSTERED 
(
	[Rol] ASC,
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[EVR]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[EVR](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Seri] [smallint] NOT NULL,
	[Value] [varchar](8) NOT NULL,
	[Aciklama] [varchar](50) NULL,
 CONSTRAINT [PK_EVR] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[GKPD]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[GKPD](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EvrakNo] [varchar](16) NULL,
	[ParametreKod] [varchar](10) NULL,
	[ParametreAciklama] [varchar](50) NULL,
	[Birim] [varchar](20) NULL,
	[TekrarSayisi] [smallint] NULL,
	[AlanTipi] [varchar](4) NULL,
	[AlanUzunluk] [int] NULL,
	[AlanDecimal] [int] NULL,
	[KontrolSekli] [varchar](20) NULL,
	[Aciklama] [varchar](100) NULL,
	[Deger] [decimal](18, 2) NULL,
	[ArtiEksi] [decimal](18, 2) NULL,
	[Maksimum] [varchar](20) NULL,
	[Minimum] [varchar](20) NULL,
	[SecimParametre] [varchar](20) NULL,
	[Kriter] [varchar](20) NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [int] NULL,
	[KayitSaat] [int] NULL,
 CONSTRAINT [PK_GKPD] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[GKPKOD]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[GKPKOD](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParametreKod] [varchar](10) NOT NULL,
	[ParametreAciklama] [varchar](100) NULL,
	[Aciklama] [varchar](50) NULL,
	[Aciklama2] [varchar](50) NULL,
	[Birim] [varchar](20) NULL,
	[TekrarSayisi] [smallint] NULL,
	[AlanTipi] [varchar](4) NULL,
	[AlanUzunluk] [int] NULL,
	[AlanDecimal] [int] NULL,
	[KontrolSekli] [varchar](20) NULL,
	[SecimParametre] [varchar](20) NULL,
	[Kriter] [varchar](20) NULL,
 CONSTRAINT [PK_GKPKOD] PRIMARY KEY CLUSTERED 
(
	[ParametreKod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[GKTP]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[GKTP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EvrakTip] [smallint] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[EvrakNo] [varchar](16) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[GrupKod] [varchar](20) NOT NULL,
	[ParametreTipi] [smallint] NULL,
	[SecimParametre] [varchar](20) NULL,
	[Degerler] [varchar](300) NULL,
	[KontrolSekli] [varchar](20) NULL,
	[Aciklama] [varchar](100) NULL,
	[Tarih] [int] NULL,
	[KaliteOnay] [smallint] NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [int] NULL,
	[KayitSaat] [int] NULL,
 CONSTRAINT [PK_GKTP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC,
	[EvrakTip] ASC,
	[MalKodu] ASC,
	[EvrakNo] ASC,
	[SiraNo] ASC,
	[GrupKod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[KTK01]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[KTK01](
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](20) NOT NULL,
	[Aciklama] [varchar](100) NOT NULL,
	[GrupKod] [varchar](10) NOT NULL,
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
	[Aciklama2] [varchar](100) NOT NULL,
	[Birim] [varchar](4) NULL,
	[IstenilenDeger] [varchar](50) NULL,
	[Row_ID] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [pkKTK01] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[MAIL]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[MAIL](
	[Tip] [smallint] NOT NULL,
	[Aciklama] [varchar](100) NULL,
	[GonderenMailAdres] [varchar](max) NOT NULL,
	[Sifre] [varchar](50) NOT NULL,
	[Domain] [varchar](100) NOT NULL,
	[Host] [varchar](100) NOT NULL,
	[Port] [int] NOT NULL,
	[SSL] [bit] NOT NULL,
	[GidecekMailAdres] [varchar](max) NOT NULL,
	[GidecekMailAdresCC] [varchar](max) NULL,
	[ROW_ID] [int] NOT NULL,
 CONSTRAINT [PK_MAIL] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [UYS].[PRS]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[PRS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Ad] [varchar](64) NOT NULL,
	[Soyad] [varchar](64) NOT NULL,
	[Tip] [smallint] NOT NULL,
 CONSTRAINT [PK_PRS] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Tip] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[PWD]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[PWD](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciAdi] [varchar](50) NOT NULL,
	[Onay] [bit] NULL,
	[Uretim] [bit] NULL,
	[Tanim] [bit] NULL,
	[Isletmen] [bit] NULL,
	[Rapor] [bit] NULL,
	[Admin] [bit] NULL,
	[UretimOnayi] [bit] NULL,
	[UretimGirisi] [bit] NULL,
	[ReceteTanimlari] [bit] NULL,
	[PersonelTanimlari] [bit] NULL,
	[OperatorTanimlari] [bit] NULL,
	[UretimMuhendisiTanimlari] [bit] NULL,
	[TesisMuduruTanimlari] [bit] NULL,
	[ServerAyarlari] [bit] NULL,
	[KullaniciIzinleri] [bit] NULL,
	[StokTakipRaporu] [bit] NULL,
 CONSTRAINT [PK_PWD] PRIMARY KEY CLUSTERED 
(
	[KullaniciAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[RCT]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[RCT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [smallint] NOT NULL,
	[MamulKodu] [varchar](30) NOT NULL,
	[MamulMiktar] [numeric](24, 6) NOT NULL,
	[Birim] [varchar](5) NOT NULL,
	[YariMamulKodu] [varchar](30) NOT NULL,
	[YariMamulBirim] [varchar](5) NOT NULL,
	[Miktar] [numeric](24, 6) NOT NULL,
	[YariMamulTip] [smallint] NOT NULL,
	[Depo] [varchar](20) NOT NULL,
	[ToplamMiktaraDahilEt] [bit] NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [int] NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [int] NULL,
 CONSTRAINT [PK_RCT] PRIMARY KEY CLUSTERED 
(
	[MamulKodu] ASC,
	[YariMamulKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[SPI]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[SPI](
	[EvrakNo] [varchar](8) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[ReferansNo] [varchar](8) NULL,
	[Tarih] [smalldatetime] NULL,
	[Chk] [varchar](20) NULL,
	[IslemTur] [smallint] NULL,
	[MalKodu] [varchar](30) NULL,
	[KaynakEvrakTip] [int] NULL,
	[SirketKodu] [int] NULL,
	[Miktar] [numeric](24, 6) NULL,
	[Fiyat] [numeric](18, 6) NULL,
	[Tutar] [numeric](18, 6) NULL,
	[Birim] [varchar](5) NULL,
	[Aciklama] [varchar](250) NULL,
	[VadeTarih] [smalldatetime] NULL,
	[Depo] [varchar](5) NULL,
	[SiparisDurum] [bit] NULL,
	[Opsiyon] [int] NULL,
	[Valor] [int] NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_SPI] PRIMARY KEY CLUSTERED 
(
	[EvrakNo] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[TAG]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[TAG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EvrakNo] [varchar](8) NOT NULL,
	[EvrakTip] [smallint] NOT NULL,
	[IslemTur] [smallint] NOT NULL,
	[HesapKodu] [varchar](30) NOT NULL,
	[Tarih] [smalldatetime] NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Birim] [varchar](5) NOT NULL,
	[BirimMiktar] [numeric](24, 6) NOT NULL,
	[Depo] [varchar](5) NOT NULL,
	[IrsaliyeNo] [varchar](8) NOT NULL,
	[IrsaliyeTarih] [smalldatetime] NOT NULL,
	[YururlulukTarihi] [smalldatetime] NOT NULL,
	[RevizyonNo] [varchar](8) NOT NULL,
	[RevizyonTarih] [smalldatetime] NOT NULL,
	[Aciklama] [varchar](50) NOT NULL,
	[GiderYeri] [varchar](20) NULL,
	[GKPDurum] [smallint] NULL,
	[GKPSiraNo] [smallint] NULL,
	[Onay] [bit] NULL,
	[SatinalmaReferansNo] [varchar](16) NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_TAG] PRIMARY KEY CLUSTERED 
(
	[EvrakNo] ASC,
	[EvrakTip] ASC,
	[IslemTur] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[TAGI]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[TAGI](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UretimNo] [varchar](20) NOT NULL,
	[MalKodu] [varchar](20) NOT NULL,
	[TeslimatNo] [varchar](20) NOT NULL,
	[KullanMiktar] [decimal](18, 0) NOT NULL,
	[IrsaliyeTarih] [smalldatetime] NOT NULL,
	[Miktar] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_TAGI] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[TALEP]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[TALEP](
	[ReferansNo] [varchar](8) NOT NULL,
	[EvrakNo] [varchar](8) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[Tarih] [smalldatetime] NULL,
	[TIFTarih] [smalldatetime] NULL,
	[MalKodu] [varchar](30) NULL,
	[Miktar] [numeric](24, 6) NULL,
	[Birim] [varchar](5) NULL,
	[TeminTarih] [smalldatetime] NULL,
	[TalepNeden] [varchar](250) NULL,
	[TalepEden] [varchar](30) NULL,
	[Onaylayan] [varchar](30) NULL,
	[OnayDurum] [bit] NULL,
	[Tedarikci1] [varchar](20) NULL,
	[Tedarikci2] [varchar](20) NULL,
	[Tedarikci3] [varchar](20) NULL,
	[OnayTarih] [smalldatetime] NULL,
	[TIFDurum] [bit] NULL,
	[TTODurum] [bit] NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_TALEP] PRIMARY KEY CLUSTERED 
(
	[ReferansNo] ASC,
	[EvrakNo] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[TIF]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[TIF](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EvrakNo] [varchar](8) NOT NULL,
	[ReferansNo] [varchar](8) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[Tarih] [smalldatetime] NULL,
	[MalKodu] [varchar](30) NULL,
	[Miktar] [numeric](24, 6) NULL,
	[Birim] [varchar](5) NULL,
	[TeminTarih] [smalldatetime] NULL,
	[TalepNeden] [varchar](250) NULL,
	[TalepEden] [varchar](30) NULL,
	[Onaylayan] [varchar](30) NULL,
	[OnayDurum] [bit] NULL,
	[OnayTarih] [smalldatetime] NULL,
	[Tedarikci1] [varchar](20) NULL,
	[Tedarikci2] [varchar](20) NULL,
	[Tedarikci3] [varchar](20) NULL,
	[TTDurum] [bit] NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_TIF] PRIMARY KEY CLUSTERED 
(
	[EvrakNo] ASC,
	[ReferansNo] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[TT]    Script Date: 05/04/2018 10:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[TT](
	[EvrakNo] [varchar](8) NOT NULL,
	[ReferansNo] [varchar](8) NULL,
	[SiraNo] [smallint] NOT NULL,
	[TTSiraNo] [smallint] NULL,
	[Tarih] [smalldatetime] NULL,
	[MalKodu] [varchar](30) NULL,
	[Miktar] [numeric](24, 6) NULL,
	[Birim] [varchar](5) NULL,
	[Tedarikci] [varchar](20) NULL,
	[Fiyat] [numeric](18, 6) NULL,
	[Valor] [int] NULL,
	[Vade] [smalldatetime] NULL,
	[Opsiyon] [int] NULL,
	[Aciklama] [varchar](250) NULL,
	[OnayDurum] [bit] NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [smalldatetime] NULL,
 CONSTRAINT [PK_TT_1] PRIMARY KEY CLUSTERED 
(
	[EvrakNo] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[URT]    Script Date: 05/04/2018 10:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[URT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UretimNo] [varchar](8) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[UretilenMiktar] [numeric](24, 6) NOT NULL,
	[Birim] [varchar](5) NOT NULL,
	[BaslangicTarih] [int] NOT NULL,
	[BitisTarih] [int] NOT NULL,
	[BaslangicSaat] [smalldatetime] NOT NULL,
	[BitisSaat] [smalldatetime] NOT NULL,
	[DurusNedeni] [varchar](64) NOT NULL,
	[DokumanKodu] [varchar](12) NOT NULL,
	[YururlulukTarihi] [int] NOT NULL,
	[RevizyonNo] [varchar](8) NOT NULL,
	[RevizyonTarihi] [int] NOT NULL,
	[SiraNo] [varchar](8) NOT NULL,
	[PartiNo] [varchar](12) NOT NULL,
	[Personel] [varchar](5) NOT NULL,
	[Operator] [varchar](5) NOT NULL,
	[UretimMuhendisi] [varchar](5) NOT NULL,
	[TesisMuduru] [varchar](5) NOT NULL,
	[Onay] [smallint] NOT NULL,
	[Depo1] [varchar](5) NULL,
	[Depo1Miktar] [numeric](24, 6) NULL,
	[Depo2] [varchar](5) NULL,
	[Depo2Miktar] [numeric](24, 6) NULL,
	[TestEdilecekMiktar] [numeric](24, 6) NULL,
	[EvrakSeriNo] [int] NULL,
 CONSTRAINT [PK_URT] PRIMARY KEY CLUSTERED 
(
	[UretimNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UYS].[YRM]    Script Date: 05/04/2018 10:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UYS].[YRM](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UretimNo] [varchar](8) NOT NULL,
	[MamulKodu] [varchar](30) NOT NULL,
	[Birim] [varchar](5) NOT NULL,
	[YariMamulKodu] [varchar](30) NOT NULL,
	[YariMamulBirim] [varchar](5) NOT NULL,
	[Miktar] [numeric](24, 6) NOT NULL,
	[YariMamulTip] [smallint] NOT NULL,
	[Depo] [varchar](20) NOT NULL,
	[GKPDurum] [smallint] NULL,
	[GKPSiraNo] [smallint] NULL,
 CONSTRAINT [PK_YRM] PRIMARY KEY CLUSTERED 
(
	[UretimNo] ASC,
	[MamulKodu] ASC,
	[YariMamulKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_SendMail]  DEFAULT ((0)) FOR [SendMail]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_Baslik1]  DEFAULT (' ') FOR [Baslik1]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_Baslik2]  DEFAULT (' ') FOR [Baslik2]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_Baslik3]  DEFAULT (' ') FOR [Baslik3]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_EmniyetMudurlugu]  DEFAULT (' ') FOR [EmniyetMudurlugu]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_DepoAdresi]  DEFAULT (' ') FOR [DepoAdresi]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_IzinDilekcesiBaslik]  DEFAULT (' ') FOR [IzinDilekcesiBaslik]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_TasimaIzinBelgesi]  DEFAULT (' ') FOR [TasimaIzinBelgesi]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_GorevliAdSoyad]  DEFAULT (' ') FOR [GorevliAdSoyad]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_Unvan1]  DEFAULT (' ') FOR [Unvan1]
GO
ALTER TABLE [dbo].[RaporTanimlari] ADD  CONSTRAINT [DF_RaporTanimlari_Unvan2]  DEFAULT (' ') FOR [Unvan2]
GO
ALTER TABLE [dbo].[Ruhsat] ADD  CONSTRAINT [DF_Ruhsat_SiparisDurumuTip]  DEFAULT ((0)) FOR [SiparisDurumuTip]
GO
ALTER TABLE [dbo].[Ruhsat] ADD  CONSTRAINT [DF_Ruhsat_Aciklama]  DEFAULT (' ') FOR [Aciklama]
GO
ALTER TABLE [dbo].[Ruhsat] ADD  CONSTRAINT [DF_Ruhsat_Kullanici]  DEFAULT (' ') FOR [Kullanici]
GO
ALTER TABLE [dbo].[Sevkiyat] ADD  CONSTRAINT [DF_Sevkiyat_VerildigiTarih]  DEFAULT ('') FOR [VerildigiTarih]
GO
ALTER TABLE [dbo].[Sevkiyat] ADD  CONSTRAINT [DF_Sevkiyat_VerildigiSaat]  DEFAULT ('') FOR [VerildigiSaat]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_HataliGirisSayisi]  DEFAULT ((0)) FOR [HataliGirisSayisi]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Uretim]  DEFAULT ((1)) FOR [Uretim]
GO
ALTER TABLE [usr].[Kullanici] ADD  CONSTRAINT [DF_Kullanici_GosterilecekSirket]  DEFAULT ('') FOR [GosterilecekSirket]
GO
ALTER TABLE [usr].[Kullanici] ADD  CONSTRAINT [DF_Kullanici_GosterilecekDepo]  DEFAULT ('') FOR [GosterilecekDepo]
GO
ALTER TABLE [usr].[Kullanici] ADD  CONSTRAINT [DF_Kullanici_PasifStokKartlariGosterilsinmi]  DEFAULT ((0)) FOR [PasifStokKartlariGosterilsinmi]
GO
ALTER TABLE [UYS].[GKTP] ADD  CONSTRAINT [DF_GKTP_ParametreTipi]  DEFAULT ((0)) FOR [ParametreTipi]
GO
ALTER TABLE [UYS].[RCT] ADD  CONSTRAINT [DF_RCT_MamulMiktar]  DEFAULT ((1)) FOR [MamulMiktar]
GO
ALTER TABLE [UYS].[RCT] ADD  CONSTRAINT [DF_RCT_ToplamMiktaraDahilEt]  DEFAULT ((1)) FOR [ToplamMiktaraDahilEt]
GO
ALTER TABLE [UYS].[SPI] ADD  CONSTRAINT [DF_SPI_IslemTur]  DEFAULT ((0)) FOR [IslemTur]
GO
ALTER TABLE [UYS].[TAG] ADD  CONSTRAINT [DF_TAG_Onay]  DEFAULT ((0)) FOR [Onay]
GO
ALTER TABLE [UYS].[URT] ADD  CONSTRAINT [DF_URT_Onay]  DEFAULT ((1)) FOR [Onay]
GO
ALTER TABLE [UYS].[URT] ADD  CONSTRAINT [DF_URT_Depo1]  DEFAULT ('') FOR [Depo1]
GO
ALTER TABLE [UYS].[URT] ADD  CONSTRAINT [DF_URT_Depo1Miktar]  DEFAULT ((0)) FOR [Depo1Miktar]
GO
ALTER TABLE [UYS].[URT] ADD  CONSTRAINT [DF_URT_Depo2]  DEFAULT ('') FOR [Depo2]
GO
ALTER TABLE [UYS].[URT] ADD  CONSTRAINT [DF_URT_Depo2Miktar]  DEFAULT ((0)) FOR [Depo2Miktar]
GO
ALTER TABLE [UYS].[URT] ADD  CONSTRAINT [DF_URT_TestEdilecekMiktar]  DEFAULT ((0)) FOR [TestEdilecekMiktar]
GO
ALTER TABLE [UYS].[URT] ADD  CONSTRAINT [DF_URT_EvrakSeriNo]  DEFAULT ((-1)) FOR [EvrakSeriNo]
GO
ALTER TABLE [usr].[RolIzin]  WITH CHECK ADD  CONSTRAINT [FK_RolIzin_Rol] FOREIGN KEY([Rol])
REFERENCES [usr].[Rol] ([Rol])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [usr].[RolIzin] CHECK CONSTRAINT [FK_RolIzin_Rol]
GO
/****** Object:  StoredProcedure [dbo].[TeslimAlmaGirisIslemleri]    Script Date: 05/04/2018 10:36:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
AfyonRapor'da TeslimAlmaGirisi_Islemleri içinde kullanılıyor.
EXEC [dbo].[TeslimAlmaGirisIslemleri] @URETIMNO='AN180091',@STOK04='76986',@MALKD='0150-230001',@TARIH=99999
401803M040000067
301211000000 --MAMULKODU
*/
CREATE PROC [dbo].[TeslimAlmaGirisIslemleri]
@URETIMNO NVARCHAR(20),
@STOK04 INT,
@MALKD NVARCHAR(30),
@TARIH INT
AS
DECLARE @MalKodu NVARCHAR(30),@EvrakNo NVARCHAR(8),@Tarih DATETIME,@Miktar NUMERIC(24,6)
IF(OBJECT_ID('tempdb..#TeslimAlmaGiris1231')) IS NOT NULL BEGIN DROP TABLE #TeslimAlmaGiris1231 END
CREATE TABLE #TeslimAlmaGiris1231(RowID INT IDENTITY(1,1),MalKodu NVARCHAR(30),EvrakNo NVARCHAR(8),Tarih DATETIME
,Miktar INT,BirimMktr INT)
DECLARE db_cursor CURSOR FOR 
SELECT D.MalKodu,D.EvrakNo,D.Tarih,D.Miktar FROM (
SELECT TAG.MalKodu,TAG.EvrakNo,TAG.IrsaliyeTarih AS Tarih, TAG.BirimMiktar AS Miktar
FROM KIRLIOGLUDOSYALAR.UYS.TAG AS TAG WITH (NOLOCK) WHERE TAG.EvrakTip=1 AND TAG.IslemTur=0 AND TAG.Onay=1
AND TAG.MalKodu=@MALKD AND DATEDIFF(DD,'1899-12-30',CONVERT(DATE,TAG.IrsaliyeTarih)) <= @TARIH
UNION ALL
SELECT URT.MalKodu,URT.UretimNo AS EvrakNo,DATEADD(DD,URT.BaslangicTarih,'1899-12-30') AS Tarih, URT.UretilenMiktar AS Miktar
FROM KIRLIOGLUDOSYALAR.UYS.URT AS URT WITH (NOLOCK) WHERE URT.Onay=0 AND URT.MalKodu=@MALKD AND URT.BaslangicTarih <= @TARIH
) AS D ORDER BY D.Tarih DESC

OPEN db_cursor  
FETCH NEXT FROM db_cursor INTO @MalKodu,@EvrakNo,@Tarih,@Miktar  
WHILE @@FETCH_STATUS = 0  
BEGIN
     INSERT INTO #TeslimAlmaGiris1231 SELECT @MalKodu,@EvrakNo,@Tarih,@Miktar,(CASE WHEN (@STOK04-@Miktar)>0 THEN @Miktar ELSE @STOK04 END)
	 SET @STOK04=@STOK04-@Miktar
	 IF(@STOK04<=0) BEGIN BREAK END
	 FETCH NEXT FROM db_cursor INTO @MalKodu,@EvrakNo,@Tarih,@Miktar  
END 
CLOSE db_cursor  
DEALLOCATE db_cursor
SELECT TE2.RowID,TE2.MalKodu,TE2.EvrakNo AS TeslimatNo,TE2.Tarih AS IrsaliyeTarih,TE2.BirimMktr AS BirimMiktar,
(CASE WHEN ISNULL(D.KullanMiktar,0)=0 THEN TE2.BirimMktr ELSE ABS(TE2.BirimMktr-ISNULL(D.KullanMiktar,0)) END) AS StkMiktar
,CONVERT(INT,ROUND(ISNULL(D2.KullanMiktar,0),0)) AS KullanilanMiktar
,0 AS FarkMiktar --> C# Kullanılıyor
,0 AS TKullan --> C# Kullanılıyor
FROM #TeslimAlmaGiris1231 AS TE2 WITH (NOLOCK) 
LEFT JOIN (
SELECT K.MalKodu,K.TeslimatNo,CONVERT(INT,SUM(K.KullanMiktar)) AS KullanMiktar
FROM KIRLIOGLUDOSYALAR.UYS.TAGI AS K WITH (NOLOCK) GROUP BY K.MalKodu,K.TeslimatNo
) AS D ON TE2.MalKodu=D.MalKodu AND TE2.EvrakNo=D.TeslimatNo
LEFT JOIN KIRLIOGLUDOSYALAR.UYS.TAGI AS D2 WITH (NOLOCK) ON TE2.MalKodu=D2.MalKodu AND TE2.EvrakNo=D2.TeslimatNo AND D2.UretimNo=@URETIMNO

/*Kullanılmış ve TAGI tablosuna kayıt atılmış kayitlar.Insert işlemindeyken boş gelecektir.
Sadece güncellemede ilgili üretimde olmayan evrak numaralarını getirmek için*/
UNION ALL
SELECT 
TAGI.ID AS RowID
,TAGI.MalKodu
,TAGI.TeslimatNo
,TAGI.IrsaliyeTarih
,0 AS BirimMiktar
,0 AS StkMiktar
,CONVERT(INT,TAGI.KullanMiktar) AS KullanilanMiktar
,0 AS FarkMiktar --> C# Kullanılıyor
,CONVERT(INT,TAGI.KullanMiktar) AS TKullan
FROM KIRLIOGLUDOSYALAR.UYS.TAGI AS TAGI WITH (NOLOCK)
WHERE TAGI.MalKodu=@MALKD AND TAGI.UretimNo=@URETIMNO AND TAGI.TeslimatNo NOT IN (
SELECT P.EvrakNo FROM #TeslimAlmaGiris1231 AS P WITH (NOLOCK) WHERE P.MalKodu=@MALKD)
ORDER BY IrsaliyeTarih DESC
IF(OBJECT_ID('tempdb..#TeslimAlmaGiris1231')) IS NOT NULL BEGIN DROP TABLE #TeslimAlmaGiris1231 END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Atesleme; 1 Güvenlik; 2 Şöför' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RuhsatPersonel', @level2type=N'COLUMN',@level2name=N'AteslemePersMi'
GO
USE [master]
GO
ALTER DATABASE [KIRLIOGLUDOSYALAR] SET  READ_WRITE 
GO
