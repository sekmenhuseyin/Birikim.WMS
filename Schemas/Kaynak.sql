USE [master]
GO
/****** Object:  Database [Kaynak]    Script Date: 05.01.2018 12:49:57 ******/
CREATE DATABASE [Kaynak]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Kaynak', FILENAME = N'E:\MsSqlServerData\Kaynak.mdf' , SIZE = 50368KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Kaynak_log', FILENAME = N'E:\MsSqlServerData\Kaynak.ldf' , SIZE = 4736KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Kaynak] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Kaynak].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Kaynak] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Kaynak] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Kaynak] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Kaynak] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Kaynak] SET ARITHABORT OFF 
GO
ALTER DATABASE [Kaynak] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Kaynak] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Kaynak] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Kaynak] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Kaynak] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Kaynak] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Kaynak] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Kaynak] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Kaynak] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Kaynak] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Kaynak] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Kaynak] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Kaynak] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Kaynak] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Kaynak] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Kaynak] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Kaynak] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Kaynak] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Kaynak] SET  MULTI_USER 
GO
ALTER DATABASE [Kaynak] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Kaynak] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Kaynak] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Kaynak] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Kaynak] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Kaynak] SET QUERY_STORE = ON
GO
ALTER DATABASE [Kaynak] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [Kaynak]
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
USE [Kaynak]
GO
/****** Object:  User [uretim]    Script Date: 05.01.2018 12:49:58 ******/
CREATE USER [uretim] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [uretim]
GO
/****** Object:  Schema [IYM]    Script Date: 05.01.2018 12:49:58 ******/
CREATE SCHEMA [IYM]
GO
/****** Object:  Schema [log]    Script Date: 05.01.2018 12:49:58 ******/
CREATE SCHEMA [log]
GO
/****** Object:  Schema [sta]    Script Date: 05.01.2018 12:49:58 ******/
CREATE SCHEMA [sta]
GO
/****** Object:  Schema [usr]    Script Date: 05.01.2018 12:49:58 ******/
CREATE SCHEMA [usr]
GO
/****** Object:  UserDefinedFunction [sta].[TalepIrsDonenMiktar]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [sta].[TalepIrsDonenMiktar]
(
	@TalepNo VARCHAR(10),
	@MalKodu VARCHAR(30)
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	
	DECLARE @Miktar DECIMAL(18,2)

	SELECT @Miktar=SUM(Miktar) FROM
	(
		SELECT SUM(BirimMiktar) as Miktar FROM FINSAT617.FINSAT617.STI (nolock)
		WHERE KynkEvrakTip=3 AND Kod1=@TalepNo AND MalKodu=@MalKodu
		--UNION ALL
		--SELECT SUM(BirimMiktar) as Miktar FROM FINSAT615.FINSAT615.STI (nolock)
		--WHERE KynkEvrakTip=3 AND Kod1=@TalepNo AND MalKodu=@MalKodu
	) AS A

	RETURN @Miktar

END

GO
/****** Object:  UserDefinedFunction [sta].[TedarikciMail]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [sta].[TedarikciMail]
(
	@HesapKodu VARCHAR(20)
)
RETURNS VARCHAR(2000)
AS
BEGIN
	
	declare @mail varchar(100)
	
	SELECT @mail=COALESCE(@mail + ';','')+Email FROM sta.CHK02 (nolock)
	WHERE HesapKodu=@HesapKodu

	return @mail;
	 
END

GO
/****** Object:  Table [dbo].[GridSchema]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GridSchema](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[GridName] [varchar](100) NOT NULL,
	[SchemaData] [varbinary](max) NOT NULL,
	[ModifiedDate] [smalldatetime] NOT NULL,
	[CreateDate] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_GridSchema] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC,
	[GridName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Izin]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Izin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Izin] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Izin] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KIzin]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KIzin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[Izin] [varchar](50) NOT NULL,
	[Okuma] [bit] NOT NULL,
	[Yazma] [bit] NOT NULL,
 CONSTRAINT [PK_KIzin] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[Kod] ASC,
	[Sirket] ASC,
	[Izin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Sifre] [varchar](50) NOT NULL,
	[Adi] [varchar](50) NOT NULL,
	[Admin] [bit] NOT NULL,
	[IYM] [bit] NOT NULL,
	[UYM] [bit] NOT NULL,
	[BTC] [bit] NOT NULL,
	[GosterilecekSirketler] [varchar](50) NOT NULL,
	[IymGrup] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UYMH]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UYMH](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[IsEmri] [bit] NULL,
	[UretimGirisi] [bit] NULL,
	[MaliyetSistemi] [bit] NULL,
	[ButceEkrani] [bit] NULL,
	[PlanlamaEkrani] [bit] NULL,
	[SonAlimFiyatTanimlama] [bit] NULL,
	[HizGirisi] [bit] NULL,
	[ReceteTanimlama] [bit] NULL,
	[HatTanimlama] [bit] NULL,
	[KalipTanimlama] [bit] NULL,
	[HatKalipKapasite] [bit] NULL,
	[RotaTanimlama] [bit] NULL,
	[PlanlamaRaporu] [bit] NULL,
	[AcikIsEmrUretimRap] [bit] NULL,
	[IsEmriMalAyrintiRap] [bit] NULL,
	[StokDevirRaporu] [bit] NULL,
	[AcikIsEmriRaporu] [bit] NULL,
	[TopluIsEmriKapatma] [bit] NULL,
	[StokMikHespDepolar] [varchar](1000) NULL,
	[IsEmriGunSayisi] [int] NULL,
	[Kod1] [varchar](20) NULL,
	[Kod2] [varchar](20) NULL,
	[Kod3] [varchar](20) NULL,
	[Kod4] [varchar](20) NULL,
	[Kod5] [varchar](20) NULL,
	[Kod6] [varchar](20) NULL,
	[Kod7] [varchar](20) NULL,
	[Kod8] [varchar](20) NULL,
	[Kod9] [varchar](20) NULL,
	[Kod10] [varchar](20) NULL,
	[VarsayilanHesKodu] [varchar](20) NULL,
	[SeriNo] [smallint] NULL,
	[AlimSiparisSeriNo] [smallint] NULL,
	[UretimSeriNo] [smallint] NULL,
	[MalTipleri] [varchar](300) NULL,
	[TopluHammaddeSarfiyati] [bit] NULL,
	[SatinAlmaSiparisi] [bit] NULL,
	[UretimSilme] [bit] NULL,
	[UretimGuncelleme] [bit] NULL,
	[GosterilecekHatlar] [varchar](300) NULL,
	[PlnBekleyenSipRapor] [bit] NULL,
 CONSTRAINT [PK_UYMH_1] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Sirket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmDiger]    Script Date: 05.01.2018 12:49:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmDiger](
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[kutuIcrkKntrlEdlsnmi] [bit] NOT NULL,
	[kutuIcrkKolon] [varchar](50) NOT NULL,
	[ihrctKytKntrlEdlsnmi] [bit] NOT NULL,
	[ihrctKytKntrlKolon] [varchar](50) NOT NULL,
	[ihrctKytKntrlDgr] [varchar](50) NOT NULL,
	[CHKeklnckSrktlr] [varchar](50) NOT NULL,
	[srkt1] [varchar](50) NOT NULL,
	[srkt2] [varchar](50) NOT NULL,
	[seri] [smallint] NOT NULL,
	[aktrlckPlstSrktlr1] [varchar](50) NOT NULL,
	[aktrlckPlstSrktlr2] [varchar](50) NOT NULL,
	[aktrlckElktrktkiPlstkHspKodu] [varchar](50) NOT NULL,
	[aktrlckPlstktkiElktrkHspKodu] [varchar](50) NOT NULL,
	[KarOrani] [smallint] NOT NULL,
	[MutlusanUrunleri] [varchar](50) NOT NULL,
	[dahilmi] [bit] NOT NULL,
	[aktrmaYplsnmi] [bit] NOT NULL,
	[gstrlmyckUrunler] [varchar](150) NOT NULL,
	[aktDepo1] [varchar](5) NOT NULL,
	[aktDepo2] [varchar](5) NOT NULL,
	[UrtEntDepoElektrikSirket] [varchar](2) NULL,
	[UrtEntDepoPlastikSirket] [varchar](2) NULL,
	[MutlusanUrunleriKodAlani] [varchar](20) NULL,
	[MutlusanUrunleriIstisna] [varchar](50) NULL,
	[Logo] [varbinary](max) NULL,
	[Kase] [varbinary](max) NULL,
 CONSTRAINT [PK_prmDiger] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Sirket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmDil]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmDil](
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[IngMalAdi] [varchar](50) NOT NULL,
	[IngBirim] [varchar](50) NOT NULL,
	[FrMalAdi] [varchar](50) NOT NULL,
	[FrBirim] [varchar](50) NOT NULL,
 CONSTRAINT [PK_prmDil] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Sirket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmDvz]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmDvz](
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[DvzliCalismami] [bit] NOT NULL,
	[DvzKurSisAlmi] [bit] NOT NULL,
	[KulKurCinsi] [varchar](50) NOT NULL,
	[DvzKurTarihi] [bit] NOT NULL,
 CONSTRAINT [PK_prmDvz] PRIMARY KEY CLUSTERED 
(
	[Sirket] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmFiyatIskonto]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmFiyatIskonto](
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[STKdanmi] [bit] NOT NULL,
	[satisFiyati] [varchar](50) NOT NULL,
	[fiyatListeNo] [varchar](50) NOT NULL,
	[iskontoCHKdanmi] [bit] NOT NULL,
	[iskontoListeNo] [varchar](50) NOT NULL,
	[NetFiyatUygulansinMi] [bit] NULL,
	[NetFiyatListeNo] [varchar](8) NULL,
	[NetFiyatSatisFiyati] [varchar](50) NULL,
 CONSTRAINT [PK_prmFiyatIskonto] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Sirket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmMail]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmMail](
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[gonderenAdres] [varchar](100) NOT NULL,
	[gorunenIsim] [varchar](70) NOT NULL,
	[oturumAdi] [varchar](70) NOT NULL,
	[oturumSifre] [varchar](50) NOT NULL,
	[mailServer] [varchar](50) NOT NULL,
	[ProformaKonu] [varchar](70) NOT NULL,
	[ProformaMesaj] [varchar](1500) NOT NULL,
	[CekiKonu] [varchar](70) NOT NULL,
	[CekiMesaj] [varchar](1500) NOT NULL,
	[KonsimentoKonu] [varchar](70) NOT NULL,
	[KonsimentoMesaj] [varchar](1500) NOT NULL,
	[RezTalKonu] [varchar](70) NOT NULL,
	[RezTalMesaj] [varchar](1500) NOT NULL,
	[SipTeklifKonu] [varchar](70) NOT NULL,
	[SipTeklifMesaj] [varchar](1500) NOT NULL,
 CONSTRAINT [PK_prmMail] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Sirket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmSistem]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmSistem](
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[kolonKK] [varchar](50) NOT NULL,
	[degerKK] [varchar](50) NOT NULL,
	[kolonKA] [varchar](50) NOT NULL,
	[degerKA] [varchar](50) NOT NULL,
	[kolonPB] [varchar](50) NOT NULL,
	[degerPB] [varchar](50) NOT NULL,
	[kolonPS] [varchar](50) NOT NULL,
	[degerPS] [varchar](50) NOT NULL,
	[kolMtBasinaAdet] [varchar](50) NOT NULL,
	[kolMalinCinsi] [varchar](254) NOT NULL,
	[kolGmrkTarifeKodu] [varchar](50) NOT NULL,
	[kolonTNKG] [varchar](50) NOT NULL,
	[birimTNKG] [smallint] NOT NULL,
	[islemTNKG] [smallint] NOT NULL,
	[birimTBKG] [smallint] NOT NULL,
	[islemTBKG] [smallint] NOT NULL,
	[kolonTBKG] [varchar](50) NOT NULL,
	[birimTHM3] [smallint] NOT NULL,
	[islemTHM3] [smallint] NOT NULL,
	[kolonTHM] [varchar](50) NOT NULL,
	[kolonAA] [varchar](50) NULL,
	[degerAA] [varchar](50) NULL,
	[kolonEP] [varchar](50) NULL,
	[degerEP] [varchar](50) NULL,
	[kolonAB] [varchar](50) NULL,
	[degerAB] [varchar](50) NULL,
	[kolonEA] [varchar](50) NULL,
	[degerEA] [varchar](50) NULL,
	[kolonPSB] [varchar](50) NULL,
	[degerPSB] [varchar](50) NULL,
	[kolonSGS] [varchar](50) NULL,
	[degerSGS] [varchar](50) NULL,
 CONSTRAINT [PK_prmSistem] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Sirket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmUrtEnt]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmUrtEnt](
	[Kod] [varchar](5) NOT NULL,
	[Sirket] [varchar](2) NOT NULL,
	[UrtEntDepoElektrikSirket] [varchar](2) NOT NULL,
	[UrtEntDepoPlastikSirket] [varchar](2) NOT NULL,
 CONSTRAINT [PK_prmUrtEnt] PRIMARY KEY CLUSTERED 
(
	[Kod] ASC,
	[Sirket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [IYM].[prmYonetici]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [IYM].[prmYonetici](
	[KullaniciKodu] [varchar](5) NOT NULL,
	[GosterilcekSirketler] [varchar](50) NULL,
 CONSTRAINT [PK_prmYonetici] PRIMARY KEY CLUSTERED 
(
	[KullaniciKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [log].[MTS02]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [log].[MTS02](
	[MTS02_ID] [int] NULL,
	[MalKodu] [varchar](30) NULL,
	[OngoruYontem] [int] NULL,
	[AsgariFaktor] [int] NULL,
	[AzamiFaktor] [int] NULL,
	[Faktor] [int] NULL,
	[ManuelKullanimGunluk] [decimal](18, 2) NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[KayitSirKodu] [varchar](2) NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [smalldatetime] NULL,
	[DegisSirKodu] [varchar](2) NULL,
	[OngoruYontemAck]  AS (case [OngoruYontem] when (0) then 'Son 1 Yıl' when (1) then 'Son Çeyrek' when (2) then 'Yılbaşından Bugüne' when (3) then 'Manuel'  end) PERSISTED,
	[Host] [varchar](50) NULL,
	[ProgramName] [varchar](50) NULL,
	[SqlStr] [varchar](4000) NULL,
	[SqlUser] [varchar](50) NULL,
	[LogDate] [datetime] NOT NULL,
	[LogType] [smallint] NOT NULL,
	[LogID] [uniqueidentifier] NOT NULL,
	[LogTypeAck]  AS (case [LogType] when (0) then 'Yeni Kayıt' when (1) then 'Güncelleme' when (2) then 'Silme'  end) PERSISTED,
 CONSTRAINT [PK_MTS02_1] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [log].[STK02]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [log].[STK02](
	[STK02_ID] [int] NULL,
	[MalKodu] [varchar](30) NULL,
	[HesapKodu] [varchar](20) NULL,
	[Birim] [varchar](4) NULL,
	[StokMiktar] [decimal](18, 2) NULL,
	[TeminSure] [int] NULL,
	[ToplamStokMiktar] [decimal](18, 2) NULL,
	[MaxTeminSure] [int] NULL,
	[MinTeminSure] [int] NULL,
	[Kaydeden] [varchar](5) NULL,
	[KayitTarih] [smalldatetime] NULL,
	[KayitSirKodu] [varchar](2) NULL,
	[Degistiren] [varchar](5) NULL,
	[DegisTarih] [smalldatetime] NULL,
	[DegisSirKodu] [varchar](2) NULL,
	[Host] [varchar](50) NULL,
	[ProgramName] [varchar](50) NULL,
	[SqlStr] [varchar](4000) NULL,
	[SqlUser] [varchar](50) NULL,
	[LogDate] [datetime] NOT NULL,
	[LogType] [smallint] NOT NULL,
	[LogID] [uniqueidentifier] NOT NULL,
	[LogTypeAck]  AS (case [LogType] when (0) then 'Yeni Kayıt' when (1) then 'Güncelleme' when (2) then 'Silme'  end) PERSISTED,
 CONSTRAINT [PK_STK02_1] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sta].[CHK02]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[CHK02](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[AdSoyad] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[KayitSirKodu] [varchar](2) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[DegisSirKodu] [varchar](2) NOT NULL,
 CONSTRAINT [PK_CHK02] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sta].[GenelAyarVeParams]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[GenelAyarVeParams](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Tip2] [smallint] NOT NULL,
	[TalebiGiren] [varchar](5) NULL,
	[Talep2KademeOnaylayan] [varchar](5) NULL,
	[Talep1KademeOnaylayan] [varchar](5) NULL,
	[SiparisSorumlu] [varchar](5) NULL,
	[SiparisSorumluImza] [image] NULL,
	[SiparisGMYOnayLimit] [decimal](18, 2) NULL,
	[Satinalmaci] [varchar](5) NULL,
	[SatinalmaciAdmin] [bit] NULL,
	[MailExp] [varchar](200) NULL,
	[AccountName] [varchar](100) NULL,
	[AccountPass] [varchar](50) NULL,
	[MailServer] [varchar](100) NULL,
	[EnableSSL] [bit] NULL,
	[SmtpPort] [int] NULL,
	[MailTo] [varchar](1000) NULL,
	[MailCc] [varchar](1000) NULL,
	[OngoruStokAsgariFaktor] [int] NULL,
	[OngoruStokAzamiFaktor] [int] NULL,
	[OzelParca] [varchar](30) NULL,
	[DosyaTipi] [varchar](50) NULL,
	[DosyaYolu] [varchar](500) NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[TipAck]  AS (case [Tip] when (0) then 'Talep Onay' when (1) then 'Sipariş Ayar' when (2) then 'Sipariş GM Onay Limit' when (3) then 'Satınalmacı' when (4) then 'Mail Ayarları' when (5) then 'Öngörü Stok Yöntemi' when (6) then 'Özel Parça' when (7) then 'Belge-Dosya Yolu'  end) PERSISTED,
	[Tip2Ack]  AS (case when [Tip]=(1) AND [Tip2]=(0) then 'Satınalma Uzmanı' when [Tip]=(1) AND [Tip2]=(1) then 'GMY' when [Tip]=(1) AND [Tip2]=(2) then 'GM'  end) PERSISTED,
 CONSTRAINT [PK_GenelAyarVeParams] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [sta].[MTS02]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[MTS02](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[OngoruYontem] [int] NOT NULL,
	[AsgariFaktor] [int] NOT NULL,
	[AzamiFaktor] [int] NOT NULL,
	[Faktor] [int] NOT NULL,
	[ManuelKullanimGunluk] [decimal](18, 2) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[KayitSirKodu] [varchar](2) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[DegisSirKodu] [varchar](2) NOT NULL,
	[OngoruYontemAck]  AS (case [OngoruYontem] when (0) then 'Son 1 Yıl' when (1) then 'Son Çeyrek' when (2) then 'Yılbaşından Bugüne' when (3) then 'Manuel'  end) PERSISTED,
 CONSTRAINT [PK_MTS02] PRIMARY KEY CLUSTERED 
(
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sta].[OnayliTed]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[OnayliTed](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeklifNo] [int] NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[SiraNo] [smallint] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[KayitSirKodu] [varchar](2) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[DegisSirKodu] [varchar](2) NOT NULL,
 CONSTRAINT [PK_OnayliTed_1] PRIMARY KEY CLUSTERED 
(
	[MalKodu] ASC,
	[SiraNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sta].[Sarfiyat]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[Sarfiyat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tarih] [int] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[SonCeyrek] [numeric](24, 2) NOT NULL,
	[Son1Yil] [numeric](24, 2) NOT NULL,
	[Yilbasindan] [numeric](24, 2) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Sarfiyat] PRIMARY KEY CLUSTERED 
(
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sta].[STK02]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[STK02](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[Birim] [varchar](4) NOT NULL,
	[StokMiktar] [decimal](18, 2) NOT NULL,
	[TeminSure] [int] NOT NULL,
	[ToplamStokMiktar] [decimal](18, 2) NOT NULL,
	[MaxTeminSure] [int] NOT NULL,
	[MinTeminSure] [int] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[KayitSirKodu] [varchar](2) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[DegisSirKodu] [varchar](2) NOT NULL,
 CONSTRAINT [PK_STK02] PRIMARY KEY CLUSTERED 
(
	[MalKodu] ASC,
	[HesapKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sta].[Talep]    Script Date: 05.01.2018 12:49:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[Talep](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TalepNo] [varchar](8) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Tarih] [date] NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Birim] [varchar](4) NOT NULL,
	[BirimMiktar] [decimal](18, 2) NOT NULL,
	[MTSHesaplananSipMiktar] [decimal](18, 2) NOT NULL,
	[IstenenTarih] [date] NOT NULL,
	[TesisKodu] [varchar](30) NULL,
	[Aciklama] [varchar](250) NULL,
	[Aciklama2] [varchar](250) NULL,
	[Aciklama3] [varchar](250) NULL,
	[Durum] [smallint] NOT NULL,
	[EkDosya] [varchar](100) NULL,
	[Kademe2Onaylayan] [varchar](5) NULL,
	[Kademe2OnayTarih] [smalldatetime] NULL,
	[Kademe1Onaylayan] [varchar](5) NULL,
	[Kademe1OnayTarih] [smalldatetime] NULL,
	[Satinalmaci] [varchar](20) NULL,
	[TeklifNo] [int] NULL,
	[HesapKodu] [varchar](20) NULL,
	[BirimFiyat] [decimal](18, 6) NULL,
	[DvzTL] [smallint] NULL,
	[DvzCinsi] [varchar](4) NULL,
	[DvzKuru] [decimal](18, 6) NULL,
	[KDVOran] [real] NOT NULL,
	[SipTalepNo] [int] NULL,
	[SipIslemTip] [smallint] NULL,
	[GMYMaliOnaylayan] [varchar](20) NULL,
	[GMYMaliOnayTarih] [smalldatetime] NULL,
	[GMOnaylayan] [varchar](20) NULL,
	[GMOnayTarih] [smalldatetime] NULL,
	[SipEvrakNo] [varchar](16) NULL,
	[SipTerminTarih] [date] NULL,
	[SirketKodu] [varchar](2) NULL,
	[MailGonder] [smallint] NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[KayitSirKodu] [varchar](2) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[DegisSirKodu] [varchar](2) NOT NULL,
	[DurumAck]  AS (case [Durum] when (0) then 'Ham Talep' when (1) then 'Talep Ön Onay' when (2) then 'Onaylı Talep' when (3) then 'Ön Talep Onay İptal' when (4) then 'Onay Talep İptal' when (5) then 'Teklif Bekleniyor' when (6) then 'Teklif Değerlendirme' when (7) then 'Teklif Onaylandı' when (8) then 'Sipariş Süreci' when (9) then 'Sipariş Ön Onay İptal' when (11) then 'Sipariş Ön Onay' when (13) then 'Sipariş Onay İptal' when (15) then 'Onaylı Sipariş' when (17) then 'Kısmen Kapanan Sipariş' when (19) then 'Kapanan Sipariş'  end) PERSISTED,
	[Gizle] [bit] NULL,
	[FTDDovizCinsi] [varchar](4) NULL,
	[FTDDovizTL] [smallint] NULL,
	[FTDDovizKuru] [decimal](18, 6) NULL,
 CONSTRAINT [PK_Talep] PRIMARY KEY CLUSTERED 
(
	[TalepNo] DESC,
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [sta].[Teklif]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sta].[Teklif](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TeklifNo] [int] NOT NULL,
	[Tarih] [date] NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Birim] [varchar](4) NOT NULL,
	[BirimFiyat] [decimal](18, 6) NOT NULL,
	[TeklifMiktar] [decimal](18, 2) NOT NULL,
	[Durum] [smallint] NOT NULL,
	[DvzTL] [smallint] NOT NULL,
	[DvzCinsi] [varchar](4) NOT NULL,
	[TerminSure] [int] NOT NULL,
	[MinSipMiktar] [decimal](18, 2) NOT NULL,
	[TeklifBasTarih] [date] NULL,
	[TeklifBitTarih] [date] NULL,
	[Vade] [smallint] NULL,
	[TeslimYeri] [varchar](50) NULL,
	[TeklifDosya] [varchar](100) NULL,
	[OneriDurum] [bit] NOT NULL,
	[Aciklama] [varchar](250) NULL,
	[Aciklama2] [varchar](250) NULL,
	[Aciklama3] [varchar](250) NULL,
	[Satinalmaci] [varchar](5) NULL,
	[Kademe2Onaylayan] [varchar](5) NULL,
	[Kademe2OnayTarih] [smalldatetime] NULL,
	[Kademe1Onaylayan] [varchar](5) NULL,
	[Kademe1OnayTarih] [smalldatetime] NULL,
	[Durum2] [smallint] NOT NULL,
	[KynkTalepNo] [varchar](8) NULL,
	[KynkTalepEkDosya] [varchar](100) NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[KayitSirKodu] [varchar](2) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[DegisSirKodu] [varchar](2) NOT NULL,
	[TeklifAciklamasi] [varchar](max) NULL,
 CONSTRAINT [PK_Teklif] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [usr].[Perm]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Perm](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PermName] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[Type] [smallint] NOT NULL,
	[AppType] [smallint] NOT NULL,
	[Group] [varchar](50) NULL,
	[RecordDate] [smalldatetime] NULL,
 CONSTRAINT [PK_Perm2] PRIMARY KEY CLUSTERED 
(
	[PermName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[RolePerm]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[RolePerm](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
	[PermName] [varchar](50) NOT NULL,
	[Reading] [bit] NOT NULL,
	[Writing] [bit] NOT NULL,
	[Updating] [bit] NOT NULL,
	[Deleting] [bit] NOT NULL,
	[RecordDate] [smalldatetime] NULL,
	[RecordUser] [varchar](20) NULL,
	[ModifiedDate] [smalldatetime] NULL,
	[ModifiedUser] [varchar](20) NULL,
 CONSTRAINT [PK_RolePerm2] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC,
	[PermName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[Roles]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Roles2] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[UserPerm]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[UserPerm](
	[UserName] [varchar](5) NOT NULL,
	[PermName] [varchar](50) NOT NULL,
	[Reading] [bit] NOT NULL,
	[Writing] [bit] NOT NULL,
	[Updating] [bit] NOT NULL,
	[Deleting] [bit] NOT NULL,
	[RecordDate] [smalldatetime] NULL,
	[RecordUser] [varchar](20) NULL,
	[ModifiedDate] [smalldatetime] NULL,
	[ModifiedUser] [varchar](20) NULL,
 CONSTRAINT [PK_UserPerm2] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC,
	[PermName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[Users]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](5) NOT NULL,
	[Password] [varchar](128) NOT NULL,
	[NameSurname] [varchar](30) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Aktif] [bit] NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[RoleName] [varchar](20) NULL,
	[SkinName] [varchar](50) NULL,
	[AppType] [smallint] NOT NULL,
	[RecordDate] [smalldatetime] NULL,
	[RecordUser] [varchar](20) NULL,
	[ModifiedDate] [smalldatetime] NULL,
	[ModifiedUser] [varchar](20) NULL,
 CONSTRAINT [PK_Users2] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Ind_CHK02]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Ind_CHK02] ON [sta].[CHK02]
(
	[HesapKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Ind1_STK02]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Ind1_STK02] ON [sta].[STK02]
(
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Ind2_STK02]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Ind2_STK02] ON [sta].[STK02]
(
	[HesapKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UniqInd_STK02]    Script Date: 05.01.2018 12:50:00 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UniqInd_STK02] ON [sta].[STK02]
(
	[MalKodu] ASC,
	[HesapKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Ind1_Talep]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Ind1_Talep] ON [sta].[Talep]
(
	[SipEvrakNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Ind2_Talep]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Ind2_Talep] ON [sta].[Talep]
(
	[SipTalepNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Ind3_Talep]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Ind3_Talep] ON [sta].[Talep]
(
	[Satinalmaci] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Ind4_Talep]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Ind4_Talep] ON [sta].[Talep]
(
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Index_Teklif]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Index_Teklif] ON [sta].[Teklif]
(
	[TeklifNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Index2_Teklif]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Index2_Teklif] ON [sta].[Teklif]
(
	[MalKodu] ASC,
	[Tarih] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Index3_Teklif]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Index3_Teklif] ON [sta].[Teklif]
(
	[TeklifNo] ASC,
	[HesapKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Index4_Teklif]    Script Date: 05.01.2018 12:50:00 ******/
CREATE NONCLUSTERED INDEX [Index4_Teklif] ON [sta].[Teklif]
(
	[HesapKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IndexUniq_Teklif]    Script Date: 05.01.2018 12:50:00 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IndexUniq_Teklif] ON [sta].[Teklif]
(
	[TeklifNo] ASC,
	[HesapKodu] ASC,
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GridSchema] ADD  CONSTRAINT [DF_GridSchema_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[GridSchema] ADD  CONSTRAINT [DF_GridSchema_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[UYMH] ADD  CONSTRAINT [DF_UYMH_GosterilecekHatlar]  DEFAULT ('') FOR [GosterilecekHatlar]
GO
ALTER TABLE [IYM].[prmDvz] ADD  CONSTRAINT [DF_Table_1_DovizliCalismami]  DEFAULT ((0)) FOR [DvzliCalismami]
GO
ALTER TABLE [IYM].[prmDvz] ADD  CONSTRAINT [DF_DvzParametreler_DvzKurSisAlmi]  DEFAULT ((0)) FOR [DvzKurSisAlmi]
GO
ALTER TABLE [IYM].[prmFiyatIskonto] ADD  CONSTRAINT [DF_prmFiyatIskonto_STKdanmi]  DEFAULT ((1)) FOR [STKdanmi]
GO
ALTER TABLE [IYM].[prmFiyatIskonto] ADD  CONSTRAINT [DF_prmFiyatIskonto_iskontoCHKdanmi]  DEFAULT ((1)) FOR [iskontoCHKdanmi]
GO
ALTER TABLE [IYM].[prmMail] ADD  CONSTRAINT [DF_prmMailler_gorunenIsim]  DEFAULT ('') FOR [gorunenIsim]
GO
ALTER TABLE [IYM].[prmMail] ADD  CONSTRAINT [DF_prmMailler_konu]  DEFAULT ('') FOR [ProformaKonu]
GO
ALTER TABLE [log].[MTS02] ADD  CONSTRAINT [DF_MTS02_LogDate]  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [log].[MTS02] ADD  CONSTRAINT [DF_MTS02_LogID]  DEFAULT (newid()) FOR [LogID]
GO
ALTER TABLE [log].[STK02] ADD  CONSTRAINT [DF_STK02_ToplamStokMiktar]  DEFAULT ((0)) FOR [ToplamStokMiktar]
GO
ALTER TABLE [log].[STK02] ADD  CONSTRAINT [DF_STK02_MaxTeminSure]  DEFAULT ((0)) FOR [MaxTeminSure]
GO
ALTER TABLE [log].[STK02] ADD  CONSTRAINT [DF_STK02_MinTeminSure]  DEFAULT ((0)) FOR [MinTeminSure]
GO
ALTER TABLE [log].[STK02] ADD  CONSTRAINT [DF_STK02_LogDate]  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [log].[STK02] ADD  CONSTRAINT [DF_STK02_LogID]  DEFAULT (newid()) FOR [LogID]
GO
ALTER TABLE [sta].[GenelAyarVeParams] ADD  CONSTRAINT [DF_GenelAyarVeParams_Tip2]  DEFAULT ((0)) FOR [Tip2]
GO
ALTER TABLE [sta].[STK02] ADD  CONSTRAINT [DF_STK02_TedarikciStokMiktar]  DEFAULT ((0)) FOR [ToplamStokMiktar]
GO
ALTER TABLE [sta].[STK02] ADD  CONSTRAINT [DF_STK02_TedarikciMaxTeminSure]  DEFAULT ((0)) FOR [MaxTeminSure]
GO
ALTER TABLE [sta].[STK02] ADD  CONSTRAINT [DF_STK02_TedarikciMinTeminSure]  DEFAULT ((0)) FOR [MinTeminSure]
GO
ALTER TABLE [sta].[Talep] ADD  CONSTRAINT [DF_SatTalep_Tip]  DEFAULT ((0)) FOR [Tip]
GO
ALTER TABLE [sta].[Talep] ADD  CONSTRAINT [DF_Talep_MTSHesaplananSipMiktar]  DEFAULT ((0)) FOR [MTSHesaplananSipMiktar]
GO
ALTER TABLE [sta].[Talep] ADD  CONSTRAINT [DF_SatTalep_Durum]  DEFAULT ((0)) FOR [Durum]
GO
ALTER TABLE [sta].[Talep] ADD  CONSTRAINT [DF_Talep_KDVOran]  DEFAULT ((0)) FOR [KDVOran]
GO
ALTER TABLE [sta].[Talep] ADD  CONSTRAINT [DF_Talep_Gizle]  DEFAULT ((0)) FOR [Gizle]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_SatTeklif_BirimFiyat]  DEFAULT ((0)) FOR [BirimFiyat]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_Teklif_TeklifMiktar]  DEFAULT ((0)) FOR [TeklifMiktar]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_SatTeklif_Durum]  DEFAULT ((0)) FOR [Durum]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_SatTeklif_DvzTL]  DEFAULT ((-1)) FOR [DvzTL]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_SatTeklif_DvzCinsi]  DEFAULT ('') FOR [DvzCinsi]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_SatTeklif_TerminSure]  DEFAULT ((0)) FOR [TerminSure]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_SatTeklif_MinSipMiktar]  DEFAULT ((0)) FOR [MinSipMiktar]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_SatTeklif_OneriDurum]  DEFAULT ((0)) FOR [OneriDurum]
GO
ALTER TABLE [sta].[Teklif] ADD  CONSTRAINT [DF_Teklif_Durum2]  DEFAULT ((0)) FOR [Durum2]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_Type_1]  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_Type]  DEFAULT ((0)) FOR [AppType]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [usr].[RolePerm] ADD  CONSTRAINT [DF_RolePerm_Updating]  DEFAULT ((0)) FOR [Updating]
GO
ALTER TABLE [usr].[UserPerm] ADD  CONSTRAINT [DF_UserPerm_Updating]  DEFAULT ((0)) FOR [Updating]
GO
ALTER TABLE [usr].[Users] ADD  CONSTRAINT [DF_Users_UserType]  DEFAULT ((0)) FOR [Admin]
GO
ALTER TABLE [usr].[Users] ADD  CONSTRAINT [DF_Users_Type]  DEFAULT ((0)) FOR [AppType]
GO
ALTER TABLE [dbo].[KIzin]  WITH CHECK ADD  CONSTRAINT [FK_KIzin_Izin] FOREIGN KEY([Tip], [Izin])
REFERENCES [dbo].[Izin] ([Tip], [Izin])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KIzin] CHECK CONSTRAINT [FK_KIzin_Izin]
GO
ALTER TABLE [dbo].[KIzin]  WITH CHECK ADD  CONSTRAINT [FK_KIzin_Users] FOREIGN KEY([Kod])
REFERENCES [dbo].[Users] ([Kod])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KIzin] CHECK CONSTRAINT [FK_KIzin_Users]
GO
ALTER TABLE [usr].[RolePerm]  WITH CHECK ADD  CONSTRAINT [FK_RolePerm_Perm] FOREIGN KEY([PermName])
REFERENCES [usr].[Perm] ([PermName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [usr].[RolePerm] CHECK CONSTRAINT [FK_RolePerm_Perm]
GO
ALTER TABLE [usr].[RolePerm]  WITH CHECK ADD  CONSTRAINT [FK_RolePerm_Roles2] FOREIGN KEY([RoleName])
REFERENCES [usr].[Roles] ([RoleName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [usr].[RolePerm] CHECK CONSTRAINT [FK_RolePerm_Roles2]
GO
ALTER TABLE [usr].[UserPerm]  WITH CHECK ADD  CONSTRAINT [FK_Perm_UserPerm2] FOREIGN KEY([PermName])
REFERENCES [usr].[Perm] ([PermName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [usr].[UserPerm] CHECK CONSTRAINT [FK_Perm_UserPerm2]
GO
ALTER TABLE [usr].[UserPerm]  WITH CHECK ADD  CONSTRAINT [FK_UserPerm_Users2] FOREIGN KEY([UserName])
REFERENCES [usr].[Users] ([UserName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [usr].[UserPerm] CHECK CONSTRAINT [FK_UserPerm_Users2]
GO
/****** Object:  StoredProcedure [sta].[sp_DashboardGenel]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [sta].[sp_DashboardGenel]

AS
BEGIN
	SET NOCOUNT ON;

		/* TALEP DURUMLARI
		HamTalep = 0,
        TalepOnOnay = 1,
        OnayliTalep = 2,
        OnOnayTalepIptal = 3,
        OnayTalepIptal = 4,
        TeklifBekleniyor = 5,

        TeklifDegerlendime = 6,

        TeklifOnaylandi = 7,

        
        
        SiparisSureci = 8,
        SiparisOnOnayIptal = 9,
        SiparisOnOnay = 11,
        SiparisOnayIptal = 13,
        SiparisOnayli = 15,

        KismenKapanan = 17,
        KapananTalep = 19
		*/

		/* TEKLİF DURUMLARI
		Giris = 0,
        Fiyat = 1,
        TedarikciSecimiYapildi = 2,
        GMYOrmanElenen = 3,
        GMYOrmanOnayli = 4,        
        GMYMaliElenen = 5,
        GMYMaliOnayli = 6
		*/


	DECLARE @SiparisSay INT, @KapananSipSay INT, @BakiyeliSipSay INT, @AcikSipSay INT, @GecikenSipSay INT
	, @OnaySurecSatSay INT, @GMOnaydaBekleyenSay INT

	DECLARE @SefOnayBekleyenTalepSay INT, @GMYOnayBekleyenTalepSay INT, @MTSOnayBekleyenTalepSay INT

	DECLARE @GMYMaliOnayBekleyenTeklifSay INT, @GMYTedarikciOnayBekleyenTeklifSay INT

	DECLARE @SatTeklifOnaySureciSay INT, @SatTeklifBekleyenTalepSay INT, @SatTeklifDegerlendirmeTalepSay INT, @SatOnayliTeklifSay INT

	--SATINALMA Sipariş Süreci
	
	SELECT @GMOnaydaBekleyenSay = COUNT(ST.SipTalepNo)
	FROM sta.[Talep] as ST (nolock)
	LEFT JOIN [FINSAT617].[FINSAT617].[CHK] (nolock) on ST.HesapKodu=CHK.HesapKodu
	WHERE ST.Durum=11 AND ST.SipTalepNo IS NOT NULL AND ST.HesapKodu IS NOT NULL AND ST.TeklifNo IS NOT NULL

	SELECT @SiparisSay=COUNT(ID), @KapananSipSay=COUNT(CASE Durum WHEN 19 THEN 1 END)
	, @BakiyeliSipSay=COUNT(CASE Durum WHEN 17 THEN 1 END)
	, @AcikSipSay=COUNT(CASE Durum WHEN 15 THEN 1 END)
	, @GecikenSipSay=COUNT(CASE WHEN Durum IN (15,17) AND SipTerminTarih IS NOT NULL AND SipTerminTarih<DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 END)
	FROM sta.Talep (nolock)
	WHERE Durum in (15,17,19)
	
	/*
	SELECT @SiparisSay=COUNT(DISTINCT SipEvrakNo)
	FROM sta.Talep (nolock)
	WHERE Durum>=15

	SELECT @KapananSipSay=COUNT(DISTINCT SipEvrakNo)
	FROM sta.Talep (nolock)
	WHERE Durum>=15
	GROUP BY SipEvrakNo
	HAVING COUNT(*)=COUNT(CASE Durum WHEN 19 THEN 1 END)

	SELECT @BakiyeliSipSay=COUNT(DISTINCT SipEvrakNo)
	FROM sta.Talep (nolock)
	WHERE Durum=17
	GROUP BY SipEvrakNo

	SELECT @AcikSipSay=COUNT(DISTINCT SipEvrakNo)
	FROM sta.Talep (nolock)
	WHERE Durum=15

	SELECT @GecikenSipSay=COUNT(DISTINCT SipEvrakNo) 
	FROM sta.Talep (nolock)
	WHERE Durum in (15,17) AND SipTerminTarih<DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))
	AND SipTerminTarih IS NOT NULL
	*/

	SELECT @OnaySurecSatSay=COUNT(ID)
	FROM sta.Talep (nolock)
	WHERE Durum IN (2,5,6,7,8,11)
	--SATINALMA Sipariş Süreci SON

	--SATINALMA Teklif Süreci

	SELECT @SatTeklifOnaySureciSay=COUNT(ID), @SatTeklifBekleyenTalepSay=COUNT(CASE Durum WHEN 2 THEN 1 END)
	, @SatTeklifDegerlendirmeTalepSay=COUNT(CASE WHEN Durum IN (5,6) THEN 1 END)
	, @SatOnayliTeklifSay=COUNT(CASE Durum WHEN 7 THEN 1 END)
	FROM sta.Talep (nolock)
	WHERE Durum IN (2,5,6,7)


	--SATINALMA Teklif Süreci SON



	--TALEP
	SELECT @SefOnayBekleyenTalepSay=COUNT(CASE WHEN Tip=0 AND Durum=0 THEN 1 END)
	, @GMYOnayBekleyenTalepSay= COUNT(CASE WHEN (Tip=0 AND Durum=1) THEN 1 END)
	, @MTSOnayBekleyenTalepSay= COUNT(CASE WHEN (Tip=1 AND Durum=0) THEN 1 END)
	FROM sta.Talep (nolock)
	WHERE Durum IN (0,1)
	--TALEP SON

	--TEKLİF
	--FARKLI TARİHLER DE AYNI MALKODUAN TEKLİF GİRİLDİYSE FARKLI TARİHLER OLDUĞU İÇİN BU İKİ SATIR OLARAK DÜŞÜNÜLÜYOR.
	SELECT @GMYMaliOnayBekleyenTeklifSay=COUNT(DISTINCT CASE WHEN Durum=4 THEN MalKodu+''+CONVERT(VARCHAR(15), Tarih, 104) END) 
	, @GMYTedarikciOnayBekleyenTeklifSay= COUNT(DISTINCT CASE WHEN Durum=2 THEN MalKodu+''+CONVERT(VARCHAR(15), Tarih, 104) END)   
	FROM sta.Teklif (nolock)
	WHERE Durum IN (2,4)

	




	SELECT @SiparisSay=ISNULL(@SiparisSay,0), @KapananSipSay=ISNULL(@KapananSipSay,0)
	, @BakiyeliSipSay=ISNULL(@BakiyeliSipSay,0), @AcikSipSay=ISNULL(@AcikSipSay,0)
	, @GecikenSipSay=ISNULL(@GecikenSipSay,0), @OnaySurecSatSay=ISNULL(@OnaySurecSatSay,0)
	, @GMOnaydaBekleyenSay=ISNULL(@GMOnaydaBekleyenSay,0)
	
	, @SefOnayBekleyenTalepSay=ISNULL(@SefOnayBekleyenTalepSay,0), @GMYOnayBekleyenTalepSay=ISNULL(@GMYOnayBekleyenTalepSay,0)
	, @MTSOnayBekleyenTalepSay=ISNULL(@MTSOnayBekleyenTalepSay,0)

	, @GMYMaliOnayBekleyenTeklifSay=ISNULL(@GMYMaliOnayBekleyenTeklifSay,0), @GMYTedarikciOnayBekleyenTeklifSay=ISNULL(@GMYTedarikciOnayBekleyenTeklifSay,0)

	, @SatTeklifOnaySureciSay=ISNULL(@SatTeklifOnaySureciSay,0), @SatTeklifBekleyenTalepSay=ISNULL(@SatTeklifBekleyenTalepSay,0)
	, @SatTeklifDegerlendirmeTalepSay=ISNULL(@SatTeklifDegerlendirmeTalepSay,0), @SatOnayliTeklifSay=ISNULL(@SatOnayliTeklifSay,0)





	DECLARE @Agac TABLE
	(
		ID INT NOT NULL PRIMARY KEY,
		Aciklama VARCHAR(250) NOT NULL,
		Adet INT NOT NULL,
		ParentID INT
	)

	 
	--INSERT INTO @Agac VALUES (4, CONCAT('Onaylı Satınalma Sipariş Sayısı (',@SiparisSay,')'), @SiparisSay, 1, 3)


	INSERT INTO @Agac VALUES (1, 'Talep', 0, NULL)
	INSERT INTO @Agac VALUES (2, 'Şef Onay Bekleyen Talepler', @SefOnayBekleyenTalepSay, 1)
	INSERT INTO @Agac VALUES (3, 'GMY Onay Bekleyen Talepler', @GMYOnayBekleyenTalepSay, 1)
	INSERT INTO @Agac VALUES (4, 'MTS Onay Bekleyen Talepler', @MTSOnayBekleyenTalepSay, 1)


	INSERT INTO @Agac VALUES (40, 'Teklif', 0, NULL)
	INSERT INTO @Agac VALUES (41, 'GMY Tedarikçi Onay Bekleyen Teklifler', @GMYTedarikciOnayBekleyenTeklifSay, 40); 
	INSERT INTO @Agac VALUES (42, 'GMY Mali Onay Bekleyen Teklifler',@GMYMaliOnayBekleyenTeklifSay,40)
	



	INSERT INTO @Agac VALUES(60, 'Satınalma Teklif Süreci', 0, NULL)
	INSERT INTO @Agac VALUES(61, 'Teklif Onay Süreci', @SatTeklifOnaySureciSay, 60)
	INSERT INTO @Agac VALUES(62, 'Teklif Bekleyen Talep Sayısı', @SatTeklifBekleyenTalepSay, 61)
	INSERT INTO @Agac VALUES(63, 'Teklif Değerlendirmedeki Talep Sayısı', @SatTeklifDegerlendirmeTalepSay, 61)
	INSERT INTO @Agac VALUES(64, 'Onaylı Teklif', @SatOnayliTeklifSay, 61)


	INSERT INTO @Agac VALUES (80, 'Satınalma Sipariş Süreci', 0, NULL)
	INSERT INTO @Agac VALUES (81, 'GM Onayında Bekleyenler', @GMOnaydaBekleyenSay ,80)
	INSERT INTO @Agac VALUES (82, 'Onay Sürecindeki Satınalma Sayısı', @OnaySurecSatSay, 80)
	INSERT INTO @Agac VALUES (83, 'Onaylı Satınalma Talep Sayısı', @SiparisSay, 80)
	INSERT INTO @Agac VALUES (84, 'Kapanan', @KapananSipSay, 83)
	INSERT INTO @Agac VALUES (85, 'Bakiyesi Olan', @BakiyeliSipSay, 83)
	INSERT INTO @Agac VALUES (86, 'Açık Olan', @AcikSipSay, 83)
	INSERT INTO @Agac VALUES (87, 'Termin Tarihi Geciken', @GecikenSipSay, 83)

	SELECT * FROM @Agac



END
GO
/****** Object:  StoredProcedure [sta].[sp_SonAlimListesi]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [sta].[sp_SonAlimListesi]
	@MalKodu VARCHAR(30),
	@TopRecord INT
AS
BEGIN
	SET NOCOUNT ON;

	IF @TopRecord IS NULL
		SET @TopRecord=10;
	
	SELECT TOP(@TopRecord) * FROM 
	(
		SELECT STI.EvrakNo, STI.Tarih, STI.Chk, STI.MalKodu, STI.Birim, STI.BirimMiktar, STI.BirimFiyat, STI.Tutar 
		, STI.DvzBirimFiyat, STI.DovizKuru, STI.DovizTutar
		, CHK.Unvan1 as Unvan, STK.MalAdi
		FROM FINSAT616.FINSAT616.STI (nolock)
		LEFT JOIN FINSAT616.FINSAT616.CHK (nolock) on CHK.HesapKodu=STI.Chk
		LEFT JOIN FINSAT616.FINSAT616.STK (nolock) on STK.MalKodu=STI.MalKodu
		WHERE STI.KynkEvrakTip=3 AND STI.MalKodu=@MalKodu

		union all

SELECT STI.EvrakNo, STI.Tarih, STI.Chk, STI.MalKodu, STI.Birim, STI.BirimMiktar, STI.BirimFiyat, STI.Tutar 
		, STI.DvzBirimFiyat, STI.DovizKuru, STI.DovizTutar
		, CHK.Unvan1 as Unvan, STK.MalAdi
		FROM FINSAT617.FINSAT617.STI (nolock)
		LEFT JOIN FINSAT617.FINSAT617.CHK (nolock) on CHK.HesapKodu=STI.Chk
		LEFT JOIN FINSAT617.FINSAT617.STK (nolock) on STK.MalKodu=STI.MalKodu
		WHERE STI.KynkEvrakTip=3 AND STI.MalKodu=@MalKodu
	) AS A
	ORDER BY A.Tarih DESC

END

GO
/****** Object:  StoredProcedure [sta].[sp_TalepIrsaliyeList]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [sta].[sp_TalepIrsaliyeList]
	@TalepNo VARCHAR(10),
	@MalKodu VARCHAR(30)
AS
BEGIN
	SET NOCOUNT ON;

	
	SELECT A.EvrakNo, A.Tarih
	, A.Chk as HesapKodu, CHK.Unvan1 as Unvan, A.MalKodu, STK.MalAdi
	, A.BirimMiktar, A.Birim, A.Kod1 as TalepNo 
	FROM 
	(

		SELECT EvrakNo, CAST(Tarih-2 as SMALLDATETIME) as Tarih, Chk, MalKodu, BirimMiktar, Birim, Kod1 FROM FINSAT616.FINSAT616.STI (nolock)
		WHERE KynkEvrakTip=3 AND Kod1=@TalepNo AND MalKodu=@MalKodu
		union all

		
		SELECT EvrakNo, CAST(Tarih-2 as SMALLDATETIME) as Tarih, Chk, MalKodu, BirimMiktar, Birim, Kod1 FROM FINSAT617.FINSAT617.STI (nolock)
		WHERE KynkEvrakTip=3 AND Kod1=@TalepNo AND MalKodu=@MalKodu
		--SELECT EvrakNo, CAST(Tarih-2 as SMALLDATETIME) as Tarih, Chk, MalKodu, BirimMiktar, Birim, Kod1 FROM FINSAT615.FINSAT615.STI (nolock)
		--WHERE KynkEvrakTip=3 AND Kod1=@TalepNo AND MalKodu=@MalKodu

	) as A
	LEFT JOIN FINSAT617.FINSAT617.STK (nolock) on STK.MalKodu=A.MalKodu
	LEFT JOIN FINSAT617.FINSAT617.CHK (nolock) on CHK.HesapKodu=A.Chk
	ORDER BY A.Tarih DESC

    
END

GO
/****** Object:  StoredProcedure [sta].[sp_TedarikciSevkPerformans]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [sta].[sp_TedarikciSevkPerformans]	
	@BasTarih INT,
	@BitTarih INT
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT SiparisNo, SipTarih, HesapKodu, Unvan, MalKodu, MalAdi, Birim, SipBirimMiktar, IstenenTarih, TahTeslimTarih 
	, IrsaliyeNo, IrsTarih, IrsBirimMiktar
	FROM
	(

		SELECT B.SiparisNo,B.SipTarih,B.HesapKodu,B.Unvan,B.Malkodu,B.MalAdi,b.Birim, b.SipBirimMiktar,b.IstenenTarih,b.TahTeslimTarih,b.IrsaliyeNo,b.IrsTarih,b.IrsBirimMiktar FROM 
(
		SELECT SPI.EvrakNo as SiparisNo, SPI.Tarih as SipTarih
		, SPI.Chk as HesapKodu, CHK.Unvan1 as Unvan, SPI.MalKodu, STK.MalAdi, SPI.Birim, ROUND(SPI.BirimMiktar,2) as SipBirimMiktar
		, TLP.IstenenTarih, SPI.TahTeslimTarih, STI.EvrakNo as IrsaliyeNo, STI.Tarih as IrsTarih, ROUND(STI.BirimMiktar,2) as IrsBirimMiktar

		FROM FINSAT616.FINSAT616.SPI (nolock)
		LEFT JOIN FINSAT616.FINSAT616.CHK (nolock) on SPI.Chk=CHK.HesapKodu
		LEFT JOIN FINSAT616.FINSAT616.STK (nolock) on STK.MalKodu=SPI.MalKodu
		LEFT JOIN FINSAT616.FINSAT616.STI (nolock) on STI.KaynakSiparisNo=SPI.EvrakNo AND STI.SiparisSiraNo=SPI.SiraNo AND STI.MalKodu=SPI.MalKodu
		INNER JOIN Kaynak.sta.Talep (nolock) as TLP on TLP.TalepNo=SPI.Kod1 AND TLP.MalKodu=SPI.MalKodu
		WHERE SPI.KynkEvrakTip=63 AND SPI.Tarih BETWEEN @BasTarih AND @BitTarih
		UNION ALL 



		SELECT SPI.EvrakNo as SiparisNo, SPI.Tarih as SipTarih
		, SPI.Chk as HesapKodu, CHK.Unvan1 as Unvan, SPI.MalKodu, STK.MalAdi, SPI.Birim, ROUND(SPI.BirimMiktar,2) as SipBirimMiktar
		, TLP.IstenenTarih, SPI.TahTeslimTarih, STI.EvrakNo as IrsaliyeNo, STI.Tarih as IrsTarih, ROUND(STI.BirimMiktar,2) as IrsBirimMiktar

		FROM FINSAT617.FINSAT617.SPI (nolock)
		LEFT JOIN FINSAT617.FINSAT617.CHK (nolock) on SPI.Chk=CHK.HesapKodu
		LEFT JOIN FINSAT617.FINSAT617.STK (nolock) on STK.MalKodu=SPI.MalKodu
		LEFT JOIN FINSAT617.FINSAT617.STI (nolock) on STI.KaynakSiparisNo=SPI.EvrakNo AND STI.SiparisSiraNo=SPI.SiraNo AND STI.MalKodu=SPI.MalKodu AND STI.KynkEvrakTip=3
		INNER JOIN Kaynak.sta.Talep (nolock) as TLP on TLP.TalepNo=SPI.Kod1 AND TLP.MalKodu=SPI.MalKodu
		WHERE SPI.KynkEvrakTip=63 AND SPI.Tarih BETWEEN @BasTarih AND @BitTarih

) AS B GROUP BY B.SiparisNo,B.SipTarih,B.HesapKodu,B.Unvan,B.Malkodu,B.MalAdi,b.Birim, b.SipBirimMiktar,b.IstenenTarih,b.TahTeslimTarih,b.IrsaliyeNo,b.IrsTarih,b.IrsBirimMiktar

	) AS A
		
END





GO
/****** Object:  StoredProcedure [sta].[sp_UretimSarfiyatList]    Script Date: 05.01.2018 12:50:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [sta].[sp_UretimSarfiyatList]

AS
BEGIN
	SET NOCOUNT ON;

    DECLARE @DunTarih INT = CAST( DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()-1)) AS INT)+2
	DECLARE @CeyrekTarih INT = @DunTarih-90+1
	DECLARE @Son1YilTarih INT = @DunTarih-360+1
	DECLARE @YilbasiTarih INT=  CAST( DATEADD(yy, DATEDIFF(yy,0,getdate()), 0) AS INT)+2

	--SELECT @DunTarih, @CeyrekTarih, @Son1YilTarih, @YilbasiTarih, (42565-42370)

	TRUNCATE TABLE [sta].[Sarfiyat]

	INSERT INTO [sta].[Sarfiyat] (Tarih, MalKodu, SonCeyrek, Son1Yil, Yilbasindan, KayitTarih)

	SELECT @DunTarih
	, MalKodu
	, ISNULL(SUM(CASE WHEN Tarih BETWEEN @CeyrekTarih AND @DunTarih THEN Miktar END)/90,0) as SonCeyrek
	, ISNULL(SUM(CASE WHEN Tarih BETWEEN @Son1YilTarih AND @DunTarih THEN Miktar END)/360,0) as Son1Yil
	, ISNULL(SUM(CASE WHEN Tarih BETWEEN @YilbasiTarih AND @DunTarih THEN Miktar END)/(@DunTarih-@YilbasiTarih),0) as Yilbasindan
	, GETDATE()
	FROM
	(
		SELECT Tarih, MalKodu, Miktar FROM FINSAT617.FINSAT617.STI (nolock)
		WHERE /*KynkEvrakTip=74 AND*/ IslemTur=1 AND KynkEvrakTip NOT IN (53,155) AND Irsfat not in (2,4)
		UNION ALL
		SELECT Tarih, MalKodu, Miktar FROM FINSAT616.FINSAT616.STI (nolock)
		WHERE /*KynkEvrakTip=74 AND*/ IslemTur=1 AND KynkEvrakTip NOT IN (53,155) AND Irsfat not in (2,4)

		--53 Depolar Arası Transfer Fişi; 155 Çoklu Depo Transfer Fişi 

	) as A
	GROUP BY MalKodu
END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Bütce; 1 İhracat Yönetim; 2: Üretim Yönetim' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Izin', @level2type=N'COLUMN',@level2name=N'Tip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-insert; 1-update, 2-delete' , @level0type=N'SCHEMA',@level0name=N'log', @level1type=N'TABLE',@level1name=N'MTS02', @level2type=N'COLUMN',@level2name=N'LogType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-insert; 1-update, 2-delete' , @level0type=N'SCHEMA',@level0name=N'log', @level1type=N'TABLE',@level1name=N'STK02', @level2type=N'COLUMN',@level2name=N'LogType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 İç Piyasa; 2 Dış Piyasa' , @level0type=N'SCHEMA',@level0name=N'sta', @level1type=N'TABLE',@level1name=N'Talep', @level2type=N'COLUMN',@level2name=N'SirketKodu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Yerel Para; 1 Döviz' , @level0type=N'SCHEMA',@level0name=N'sta', @level1type=N'TABLE',@level1name=N'Teklif', @level2type=N'COLUMN',@level2name=N'DvzTL'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1; GMY Mali tarafında Onaylanıp Geçenler; 0 olduğunda zaten onaylanmamış Durum da 5 demektir.' , @level0type=N'SCHEMA',@level0name=N'sta', @level1type=N'TABLE',@level1name=N'Teklif', @level2type=N'COLUMN',@level2name=N'Durum2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type -> 0 ise form bazlı 1-> ise kontrol bazlı' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Perm', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Farklı programlar varsa ayırt etmek için' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Perm', @level2type=N'COLUMN',@level2name=N'AppType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 -> User  1-> Admin  2-> SuperAdmin  gibi' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Admin'
GO
USE [master]
GO
ALTER DATABASE [Kaynak] SET  READ_WRITE 
GO
