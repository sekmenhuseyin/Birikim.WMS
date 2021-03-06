USE [master]
GO
/****** Object:  Database [BIRIKIM_LOG]    Script Date: 30/03/2018 12:17:11 ******/
CREATE DATABASE [BIRIKIM_LOG]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BIRIKIM_LOG', FILENAME = N'E:\MsSqlServerData\BIRIKIM_LOG.mdf' , SIZE = 169728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BIRIKIM_LOG_log', FILENAME = N'E:\MsSqlServerData\BIRIKIM_LOG_log.ldf' , SIZE = 43264KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BIRIKIM_LOG] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BIRIKIM_LOG].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BIRIKIM_LOG] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET ARITHABORT OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BIRIKIM_LOG] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BIRIKIM_LOG] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BIRIKIM_LOG] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BIRIKIM_LOG] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BIRIKIM_LOG] SET  MULTI_USER 
GO
ALTER DATABASE [BIRIKIM_LOG] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BIRIKIM_LOG] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BIRIKIM_LOG] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BIRIKIM_LOG] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BIRIKIM_LOG] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BIRIKIM_LOG] SET QUERY_STORE = ON
GO
ALTER DATABASE [BIRIKIM_LOG] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [BIRIKIM_LOG]
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
USE [BIRIKIM_LOG]
GO
/****** Object:  UserDefinedFunction [dbo].[GetComboItemName]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 23.08.2017
-- Modify date: 23.08.2017
-- Description:	Comboitemname tablosundan idye göre name sütununu çeker
-- =============================================
CREATE FUNCTION [dbo].[GetComboItemName]
(
	@ID int
)
RETURNS varchar(50)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result varchar(50)

	-- Add the T-SQL statements to compute the return value here
	SELECT @result = [Name] FROM BIRIKIM.dbo.ComboItem_Name WHERE (ID = @ID)

	-- Return the result of the function
	RETURN @result

END
GO
/****** Object:  Table [dbo].[AppLog]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tarih] [smalldatetime] NOT NULL,
	[Site] [varchar](50) NOT NULL,
	[Area] [varchar](50) NOT NULL,
	[Controller] [varchar](50) NOT NULL,
	[Action] [varchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[SelectedID] [int] NULL,
	[Details] [varchar](max) NULL,
	[Request] [varchar](max) NULL,
	[Username] [varchar](5) NULL,
	[IpAddress] [varchar](50) NOT NULL,
	[TypeName]  AS ([dbo].[GetComboItemName]([Type])),
 CONSTRAINT [PK_AppLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_Aylik_SatisAnalizi]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_Aylik_SatisAnalizi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[Ay] [varchar](50) NOT NULL,
	[Yil2015] [decimal](25, 6) NOT NULL,
	[Yil2016] [decimal](25, 6) NOT NULL,
	[Yil2017] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_Aylik_SatisAnalizi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[Grup] [varchar](50) NOT NULL,
	[Kriter] [varchar](50) NOT NULL,
	[IslemTip] [int] NOT NULL,
	[Ay] [varchar](50) NOT NULL,
	[Yil2015] [decimal](25, 6) NOT NULL,
	[Yil2016] [decimal](25, 6) NOT NULL,
	[Yil2017] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_BakiyeRiskAnalizi]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_BakiyeRiskAnalizi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[HesapKodu] [varchar](50) NOT NULL,
	[Unvan] [nvarchar](250) NOT NULL,
	[Borc] [decimal](25, 6) NOT NULL,
	[Alacak] [decimal](25, 6) NOT NULL,
	[Bakiye] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_BakiyeRiskAnalizi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_BekleyenSiparis_UrunGrubu_Fiyat]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_BekleyenSiparis_UrunGrubu_Fiyat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[GrupKod] [varchar](150) NOT NULL,
	[KalanMiktar] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_BekleyenSiparis_UrunGrubu_Fiyat] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_BekleyenSiparis_UrunGrubu_Miktar]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_BekleyenSiparis_UrunGrubu_Miktar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[GrupKod] [varchar](150) NOT NULL,
	[KalanMiktar] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_BekleyenSiparis_UrunGrubu_Miktar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_GunlukSatisAnaliziYearToDay]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_GunlukSatisAnaliziYearToDay](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[CHK] [varchar](50) NOT NULL,
	[Unvan] [nvarchar](250) NOT NULL,
	[GenelTutar] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_GunlukSatisAnaliziYearToDay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_LokasyonBazli_SatisAnalizi]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_LokasyonBazli_SatisAnalizi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[Ay] [int] NOT NULL,
	[GrupKod] [nvarchar](250) NOT NULL,
	[Yil2016] [decimal](25, 6) NOT NULL,
	[Yil2017] [decimal](25, 6) NOT NULL,
	[Toplam] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_LokasyonBazli_SatisAnalizi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_LokasyonBazli_SatisAnalizi_Kriter]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_LokasyonBazli_SatisAnalizi_Kriter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[Ay] [int] NOT NULL,
	[Kriter] [varchar](50) NOT NULL,
	[GrupKod] [nvarchar](250) NOT NULL,
	[Yil2016] [decimal](25, 6) NOT NULL,
	[Yil2017] [decimal](25, 6) NOT NULL,
	[Toplam] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_LokasyonBazli_SatisAnalizi_Kriter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_SatisBaglanti_UrunGrubu]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_SatisBaglanti_UrunGrubu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[MalKod] [varchar](50) NOT NULL,
	[KalanTutar] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_SatisBaglanti_UrunGrubu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_UrunGrubu_SatisAnalizi]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_UrunGrubu_SatisAnalizi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[Ay] [int] NOT NULL,
	[GrupKod] [nvarchar](250) NOT NULL,
	[Yil2016] [decimal](25, 6) NOT NULL,
	[Yil2017] [decimal](25, 6) NOT NULL,
	[Toplam] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_UrunGrubu_SatisAnalizi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DB_UrunGrubu_SatisAnalizi_Kriter]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DB_UrunGrubu_SatisAnalizi_Kriter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DB] [varchar](5) NOT NULL,
	[Ay] [int] NOT NULL,
	[Kriter] [varchar](50) NOT NULL,
	[GrupKod] [nvarchar](250) NOT NULL,
	[Yil2016] [decimal](25, 6) NOT NULL,
	[Yil2017] [decimal](25, 6) NOT NULL,
	[Toplam] [decimal](25, 6) NOT NULL,
 CONSTRAINT [PK_DB_UrunGrubu_SatisAnalizi_Kriter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 30/03/2018 12:17:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL,
 CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 30/03/2018 12:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Source] [varchar](50) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Machine] [varchar](50) NULL,
	[IpAddress] [varchar](50) NULL,
	[UserName] [varchar](50) NULL,
 CONSTRAINT [PK_ApplicationLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginLog]    Script Date: 30/03/2018 12:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[IP] [varchar](50) NULL,
	[DateTime] [smalldatetime] NULL,
	[LoggedIn] [bit] NOT NULL,
	[Comment] [varchar](150) NULL,
 CONSTRAINT [PK_LoginLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TumSiparisOnayLog]    Script Date: 30/03/2018 12:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TumSiparisOnayLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[Unvan] [varchar](100) NOT NULL,
	[EvrakNo] [varchar](16) NOT NULL,
	[Firma] [varchar](100) NOT NULL,
	[SiparisTuru] [varchar](20) NOT NULL,
	[OnayDurumu] [varchar](20) NOT NULL,
	[TipKodu] [varchar](20) NOT NULL,
	[Risk] [decimal](18, 2) NOT NULL,
	[KrediLimiti] [decimal](18, 2) NOT NULL,
	[Bakiye] [decimal](18, 2) NOT NULL,
	[SCek] [decimal](18, 2) NOT NULL,
	[TCek] [decimal](18, 2) NOT NULL,
	[PRTBakiye] [decimal](18, 2) NOT NULL,
	[Kod2] [varchar](20) NOT NULL,
	[Kod3OrtBakiye] [decimal](18, 2) NOT NULL,
	[Kod3OrtGun] [decimal](18, 2) NOT NULL,
	[OrtGun] [decimal](18, 2) NOT NULL,
	[SicakSiparis] [decimal](18, 2) NOT NULL,
	[SogukSiparis] [decimal](18, 2) NOT NULL,
	[GunIciSiparis] [decimal](18, 2) NOT NULL,
	[CHKAralik] [varchar](100) NOT NULL,
	[RiskBakiyesi] [decimal](18, 2) NOT NULL,
	[TipKodlari] [varchar](500) NOT NULL,
	[RiskBakiyeAralik] [varchar](150) NOT NULL,
	[RiskAralik] [varchar](150) NOT NULL,
	[SirketAralik] [varchar](100) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_TumSiparisOnayLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ELMAH_Error_App_Time_Seq]    Script Date: 30/03/2018 12:17:12 ******/
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error]
(
	[Application] ASC,
	[TimeUtc] DESC,
	[Sequence] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
ALTER TABLE [dbo].[LoginLog] ADD  CONSTRAINT [DF_LoginLog_LoggedIn]  DEFAULT ((0)) FOR [LoggedIn]
GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorsXml]    Script Date: 30/03/2018 12:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO

GO
/****** Object:  StoredProcedure [dbo].[ELMAH_GetErrorXml]    Script Date: 30/03/2018 12:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application

GO
/****** Object:  StoredProcedure [dbo].[ELMAH_LogError]    Script Date: 30/03/2018 12:17:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )

GO
USE [master]
GO
ALTER DATABASE [BIRIKIM_LOG] SET  READ_WRITE 
GO
