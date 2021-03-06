USE [master]
GO
/****** Object:  Database [BIRIKIM]    Script Date: 05/04/2018 10:32:56 ******/
CREATE DATABASE [BIRIKIM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BIRIKIM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\BIRIKIM.mdf' , SIZE = 94208KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BIRIKIM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\BIRIKIM_log.ldf' , SIZE = 43264KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BIRIKIM] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BIRIKIM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BIRIKIM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BIRIKIM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BIRIKIM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BIRIKIM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BIRIKIM] SET ARITHABORT OFF 
GO
ALTER DATABASE [BIRIKIM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BIRIKIM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BIRIKIM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BIRIKIM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BIRIKIM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BIRIKIM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BIRIKIM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BIRIKIM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BIRIKIM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BIRIKIM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BIRIKIM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BIRIKIM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BIRIKIM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BIRIKIM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BIRIKIM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BIRIKIM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BIRIKIM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BIRIKIM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BIRIKIM] SET  MULTI_USER 
GO
ALTER DATABASE [BIRIKIM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BIRIKIM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BIRIKIM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BIRIKIM] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BIRIKIM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BIRIKIM] SET QUERY_STORE = ON
GO
ALTER DATABASE [BIRIKIM] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [BIRIKIM]
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
USE [BIRIKIM]
GO
/****** Object:  Schema [ong]    Script Date: 05/04/2018 10:32:56 ******/
CREATE SCHEMA [ong]
GO
/****** Object:  Schema [usr]    Script Date: 05/04/2018 10:32:56 ******/
CREATE SCHEMA [usr]
GO
/****** Object:  Schema [wms]    Script Date: 05/04/2018 10:32:56 ******/
CREATE SCHEMA [wms]
GO
/****** Object:  UserDefinedFunction [dbo].[fnFormatDateFromInt]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 17.05.2017
-- Modify date: 17.05.2017
-- Description:	int halindeki tarihi normal hale getirir
-- =============================================
CREATE FUNCTION [dbo].[fnFormatDateFromInt]
(
	@Tarih int
)
RETURNS VARCHAR(15)
AS
BEGIN

	RETURN CONVERT(VARCHAR(15), CAST(@Tarih - 2 AS datetime), 104);

END


GO
/****** Object:  UserDefinedFunction [dbo].[fnFormatTimeFromInt]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 17.05.2017
-- Modify date: 17.05.2017
-- Description:	int halindeki saati normal hale getirir
-- =============================================
CREATE FUNCTION [dbo].[fnFormatTimeFromInt]
(
	@Saat int
)
RETURNS VARCHAR(15)
AS
BEGIN

	RETURN CONVERT(char(5), DATEADD(second, @Saat, ''), 108)

END



GO
/****** Object:  UserDefinedFunction [dbo].[fnRoundUp]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		https://stackoverflow.com/questions/14059164/tsql-round-up-decimal-number
-- Create date: 04.07.2017
-- Description:	We can write a T-SQL function to round up for an arbitrary number of decimal places.
-- =============================================
CREATE FUNCTION [dbo].[fnRoundUp]
(
	-- Add the parameters for the function here
	@value float, @places int
)
RETURNS float
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar float

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar = CEILING(@value * POWER(10, @places)) / POWER(10, @places)

	-- Return the result of the function
	RETURN @ResultVar

END

GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit VARCHAR(MAX) )
RETURNS
	@returnList TABLE ([Name] [nvarchar] (500))

AS
BEGIN

	DECLARE @name NVARCHAR(255)
	DECLARE @pos INT

	WHILE CHARINDEX(',', @stringToSplit) > 0
	BEGIN
		SELECT @pos  = CHARINDEX(',', @stringToSplit)  
		SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

		INSERT INTO @returnList 
		SELECT @name

		SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
	END

	INSERT INTO @returnList
	SELECT @stringToSplit

	RETURN
END
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring2]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring2] ( @stringToSplit VARCHAR(MAX), @splitter varchar(1) )
RETURNS
	@returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

	DECLARE @name NVARCHAR(255)
	DECLARE @pos INT

	WHILE CHARINDEX(@splitter, @stringToSplit) > 0
	BEGIN
		SELECT @pos  = CHARINDEX(@splitter, @stringToSplit)  
		SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

		INSERT INTO @returnList 
		SELECT @name

		SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
	END

	INSERT INTO @returnList
	SELECT @stringToSplit

RETURN
END
GO
/****** Object:  UserDefinedFunction [wms].[fnGetDepoIDFromKatID]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 03.05.2017
-- Modify date: 03.05.2017
-- Description:	yer tablosunda kat id'den depo id'yi getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetDepoIDFromKatID]
(
	@KatID int
)
RETURNS int
AS
BEGIN

	--declares
	DECLARE @Return int
	--select
	SELECT        @Return = wms.Depo.ID
	FROM            wms.Depo INNER JOIN
							 wms.Koridor ON wms.Depo.ID = wms.Koridor.DepoID INNER JOIN
							 wms.Raf ON wms.Koridor.ID = wms.Raf.KoridorID INNER JOIN
							 wms.Bolum ON wms.Raf.ID = wms.Bolum.RafID INNER JOIN
							 wms.Kat ON wms.Bolum.ID = wms.Kat.BolumID
	WHERE wms.Kat.ID = @KatID
	-- Return the result of the function
	RETURN @Return;

END


GO
/****** Object:  UserDefinedFunction [wms].[fnGetHucreAdFromKatID]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 13.03.2017
-- Modify date: 16.06.2017
-- Description:	yer tablosunda kat id'den hücre adını getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetHucreAdFromKatID]
(
	@KatID int
)
RETURNS varchar(50)
AS
BEGIN

	--declares
	DECLARE @Return varchar(50)
	--select
	SELECT        @Return = wms.Koridor.KoridorAd + '-' + wms.Raf.RafAd + wms.Bolum.BolumAd + '-' + wms.Kat.KatAd
	FROM            wms.Depo INNER JOIN
							 wms.Koridor ON wms.Depo.ID = wms.Koridor.DepoID INNER JOIN
							 wms.Raf ON wms.Koridor.ID = wms.Raf.KoridorID INNER JOIN
							 wms.Bolum ON wms.Raf.ID = wms.Bolum.RafID INNER JOIN
							 wms.Kat ON wms.Bolum.ID = wms.Kat.BolumID
	WHERE wms.Kat.ID = @KatID
	-- Return the result of the function
	RETURN @Return;

END




GO
/****** Object:  UserDefinedFunction [wms].[fnGetKatIDFromHucreAd]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 10.03.2017
-- Modify date: 10.03.2017
-- Description:	yer_log tablosunda hücre adından kat id'yi getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetKatIDFromHucreAd]
(
	@Kod varchar(50)
)
RETURNS int
AS
BEGIN

	--declares
	DECLARE @name NVARCHAR(255), @pos INT, @tmp int
	DECLARE @Koridor varchar(50), @Raf varchar(50), @Bolum varchar(50), @Kat varchar(50)
	DECLARE @Return varchar(50)
	set @tmp=0
	--loop
	WHILE CHARINDEX('-', @Kod) > 0
	BEGIN
		SELECT @pos  = CHARINDEX('-', @Kod)  
		SELECT @name = SUBSTRING(@Kod, 1, @pos-1)
		--set names
		if(@tmp=0)
			set @Koridor=@name
		else if(@tmp=1)
			set @Raf=@name
		else 
			set @Kat=@name
		set @tmp=@tmp+1
		--remove
		SELECT @Kod = SUBSTRING(@Kod, @pos+1, LEN(@Kod)-@pos)
	END
	set @Kat=@Kod
	set @Bolum=SUBSTRING(@Raf, 2, 2)
	set @Raf=SUBSTRING(@Raf, 1, 1)

	--find id
	SELECT		@Return = wms.Kat.ID
	FROM		wms.Koridor INNER JOIN
							 wms.Raf ON wms.Koridor.ID = wms.Raf.KoridorID INNER JOIN
							 wms.Bolum ON wms.Raf.ID = wms.Bolum.RafID INNER JOIN
							 wms.Kat ON wms.Bolum.ID = wms.Kat.BolumID
	WHERE		(wms.Koridor.DepoID = 1) AND (wms.Koridor.KoridorAd = @Koridor) AND (wms.Raf.RafAd = @Raf) AND (wms.Bolum.BolumAd = @Bolum) AND (wms.Kat.KatAd = @Kat)
	-- Return the result of the function
	RETURN @Return;

END



GO
/****** Object:  UserDefinedFunction [wms].[fnGetKynkEvrakNosForGorev]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 30.05.2017
-- Modify date: 22.06.2017
-- Description:	görevdeki evrakları getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetKynkEvrakNosForGorev]
(
	@GorevID int
)
RETURNS VARCHAR(MAX)
AS
BEGIN

	declare @result varchar(max)
	set @result=(
		SELECT        wms.IRS_Detay.KynkSiparisNo + ', '
		FROM            wms.GorevIRS INNER JOIN
								 wms.IRS_Detay ON wms.GorevIRS.IrsaliyeID = wms.IRS_Detay.IrsaliyeID
		WHERE        (wms.GorevIRS.GorevID = @GorevID)
		GROUP BY	wms.IRS_Detay.KynkSiparisNo + ', '
		FOR XML PATH('')
	)
	if (@result = '')
	set @result=(
		SELECT        wms.IRS.EvrakNo+', '
		FROM            wms.GorevIRS INNER JOIN
								 wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID
		WHERE        (wms.GorevIRS.GorevID = @GorevID)
		GROUP BY	wms.IRS.EvrakNo+', '
		FOR XML PATH('')
	)
	RETURN @result

END
GO
/****** Object:  UserDefinedFunction [wms].[fnGetKynkTarihsForGorev]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 16.06.2017
-- Modify date: 20.12.2017
-- Description:	görevdeki kaynak sipariş tarihlerini getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetKynkTarihsForGorev]
(
	@GorevID int
)
RETURNS VARCHAR(MAX)
AS
BEGIN

	declare @result varchar(max)
	set @result=(
		SELECT        dbo.fnFormatDateFromInt(wms.IRS_Detay.KynkSiparisTarih) + ', '
		FROM            wms.GorevIRS INNER JOIN
								 wms.IRS_Detay ON wms.GorevIRS.IrsaliyeID = wms.IRS_Detay.IrsaliyeID INNER JOIN
								 wms.Gorev ON wms.GorevIRS.GorevID = wms.Gorev.ID
		WHERE        (wms.GorevIRS.GorevID = @GorevID) AND (wms.Gorev.GorevTipiID <> 19) AND (wms.Gorev.GorevTipiID <> 20)
		GROUP BY dbo.fnFormatDateFromInt(wms.IRS_Detay.KynkSiparisTarih) + ', '
		FOR XML PATH('')
	)
	if (@result = '')
	set @result=(
		SELECT        dbo.fnFormatDateFromInt(wms.IRS.Tarih) + ', '
		FROM            wms.GorevIRS INNER JOIN
								 wms.IRS ON wms.GorevIRS.IrsaliyeID = wms.IRS.ID
		WHERE        (wms.GorevIRS.GorevID = @GorevID)
		GROUP BY dbo.fnFormatDateFromInt(wms.IRS.Tarih) + ', '
		FOR XML PATH('')
	)
	RETURN @result

END
GO
/****** Object:  UserDefinedFunction [wms].[fnGetRezervStock]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 19.09.2017
-- Modify date: 20.12.2017
-- Description:	bir mal için rezerv stoğu getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetRezervStock]
(
	@DepoKodu varchar(5),
	@MalKodu varchar(30),
	@Birim varchar(4)
)
RETURNS numeric(25, 6)
AS
BEGIN
	-- Declare the return variable here
	declare @Rezerv int
	-- Add the T-SQL statements to compute the return value here
	SELECT        @Rezerv = SUM(wms.IRS_Detay.Miktar)
	FROM            wms.IRS INNER JOIN
								wms.IRS_Detay ON wms.IRS.ID = wms.IRS_Detay.IrsaliyeID INNER JOIN
								wms.GorevIRS ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN
								wms.Gorev ON wms.GorevIRS.GorevID = wms.Gorev.ID INNER JOIN
								wms.Depo ON wms.Gorev.DepoID = wms.Depo.ID
	WHERE        (wms.IRS_Detay.MalKodu = @MalKodu) AND (wms.Gorev.GorevTipiID = 3) AND (wms.Gorev.DurumID = 9) AND (wms.Depo.DepoKodu = @DepoKodu) AND 
				CASE WHEN @Birim = '' then 1 else case when (wms.IRS_Detay.Birim = @Birim) then 1 else 0 end end = 1
	-- Return the result of the function
	RETURN ISNULL(@Rezerv, 0)

END

GO
/****** Object:  UserDefinedFunction [wms].[fnGetStock]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 03.04.2017
-- Modify date: 20.12.2017
-- Description:	bir mal için geçerli stoğu getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetStock]
(
	@DepoKodu varchar(5),
	@MalKodu varchar(30),
	@Birim varchar(4),
	@includeRezerv bit
)
RETURNS numeric(25, 6)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int
	declare @Rezerv int

	-- Add the T-SQL statements to compute the return value here
	if (@includeRezerv=1)
		SELECT        @Rezerv = SUM(wms.IRS_Detay.Miktar)
		FROM            wms.IRS INNER JOIN
									wms.IRS_Detay ON wms.IRS.ID = wms.IRS_Detay.IrsaliyeID INNER JOIN
									wms.GorevIRS ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN
									wms.Gorev ON wms.GorevIRS.GorevID = wms.Gorev.ID INNER JOIN
									wms.Depo ON wms.Gorev.DepoID = wms.Depo.ID
		WHERE        (wms.IRS_Detay.MalKodu = @MalKodu) AND (wms.Gorev.GorevTipiID = 3) AND (wms.Gorev.DurumID = 9) AND (wms.Depo.DepoKodu = @DepoKodu) AND
					CASE WHEN @Birim = '' then 1 else case when (wms.IRS_Detay.Birim = @Birim) then 1 else 0 end end = 1
	--get return
	SELECT        @Result=(SUM(wms.Yer.Miktar) - ISNULL(@Rezerv,0))
	FROM            wms.Yer INNER JOIN
								wms.Kat ON wms.Yer.KatID = wms.Kat.ID AND wms.Yer.KatID = wms.Kat.ID INNER JOIN
								wms.Bolum ON wms.Kat.BolumID = wms.Bolum.ID AND wms.Kat.BolumID = wms.Bolum.ID INNER JOIN
								wms.Raf ON wms.Bolum.RafID = wms.Raf.ID AND wms.Bolum.RafID = wms.Raf.ID INNER JOIN
								wms.Koridor ON wms.Raf.KoridorID = wms.Koridor.ID AND wms.Raf.KoridorID = wms.Koridor.ID INNER JOIN
								wms.Depo ON wms.Koridor.DepoID = wms.Depo.ID
	WHERE        (wms.Yer.MalKodu = @MalKodu) AND (wms.Depo.DepoKodu = @DepoKodu) AND 
				CASE WHEN @Birim = '' then 1 else case when (wms.Yer.Birim = @Birim) then 1 else 0 end end = 1
	-- Return the result of the function
	RETURN ISNULL(@Result, 0)

END


GO
/****** Object:  UserDefinedFunction [wms].[fnGetStockByID]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 24.08.2017
-- Modify date: 20.12.2017
-- Description:	bir mal için geçerli stoğu getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetStockByID]
(
	@DepoID int,
	@MalKodu varchar(30),
	@Birim varchar(4)
)
RETURNS numeric(25, 6)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result int
	--get return
	SELECT        @Result=SUM(wms.Yer.Miktar)
	FROM            wms.Yer INNER JOIN
								wms.Kat ON wms.Yer.KatID = wms.Kat.ID AND wms.Yer.KatID = wms.Kat.ID INNER JOIN
								wms.Bolum ON wms.Kat.BolumID = wms.Bolum.ID AND wms.Kat.BolumID = wms.Bolum.ID INNER JOIN
								wms.Raf ON wms.Bolum.RafID = wms.Raf.ID AND wms.Bolum.RafID = wms.Raf.ID INNER JOIN
								wms.Koridor ON wms.Raf.KoridorID = wms.Koridor.ID AND wms.Raf.KoridorID = wms.Koridor.ID INNER JOIN
								wms.Depo ON wms.Koridor.DepoID = wms.Depo.ID
	WHERE        (wms.Yer.MalKodu = @MalKodu) AND (wms.Depo.ID = @DepoID) AND 
				CASE WHEN @Birim = '' then 1 else case when (wms.Yer.Birim = @Birim) then 1 else 0 end end = 1
	-- Return the result of the function
	RETURN ISNULL(@Result, 0)

END




GO
/****** Object:  Table [wms].[Kat]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Kat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BolumID] [int] NOT NULL,
	[KatAd] [nvarchar](100) NOT NULL,
	[Boy] [numeric](25, 6) NOT NULL,
	[Derinlik] [numeric](25, 6) NOT NULL,
	[En] [numeric](25, 6) NOT NULL,
	[AgirlikKapasite] [numeric](25, 6) NOT NULL,
	[OzellikID] [int] NOT NULL,
	[Aciklama] [nvarchar](250) NULL,
	[SiraNo] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_Store05] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Koridor]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Koridor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepoID] [int] NOT NULL,
	[KoridorAd] [nvarchar](100) NOT NULL,
	[SiraNo] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_Store02] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [StorId_Corridor_UniqueKey] UNIQUE NONCLUSTERED 
(
	[DepoID] ASC,
	[KoridorAd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Raf]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Raf](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KoridorID] [int] NOT NULL,
	[RafAd] [nvarchar](100) NOT NULL,
	[SiraNo] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_Store03] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Corridor-ShelfName_UniqueKey] UNIQUE NONCLUSTERED 
(
	[KoridorID] ASC,
	[RafAd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Yer]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Yer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KatID] [int] NOT NULL,
	[DepoID]  AS ([wms].[fnGetDepoIDFromKatID]([KatID])),
	[HucreAd]  AS ([wms].[fnGetHucreAdFromKatID]([KatID])),
	[MalKodu] [varchar](30) NOT NULL,
	[Miktar] [numeric](25, 6) NOT NULL,
	[Birim] [varchar](4) NOT NULL,
	[MakaraNo] [varchar](50) NULL,
	[MakaraDurum] [bit] NOT NULL,
 CONSTRAINT [PK_Yerlestirme] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Bolum]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Bolum](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RafID] [int] NOT NULL,
	[BolumAd] [nvarchar](100) NOT NULL,
	[SiraNo] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_Store04] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [ShelfId-SectionName-UniqueKey] UNIQUE NONCLUSTERED 
(
	[RafID] ASC,
	[BolumAd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Depo]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Depo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepoKodu] [varchar](5) NOT NULL,
	[DepoAd] [nvarchar](100) NOT NULL,
	[KabloDepoID] [int] NULL,
	[SiraNo] [int] NOT NULL,
	[Aktif] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_Store01] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [StoreCode_UniqueKey] UNIQUE NONCLUSTERED 
(
	[DepoKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [StoreName_UniqueKey] UNIQUE NONCLUSTERED 
(
	[DepoAd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [wms].[fnGetStockAll]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 18.01.2018
-- Modify date: 18.01.2018
-- Description:	tüm mallar için stoğu getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetStockAll]
(	
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT        wms.Yer.MalKodu, wms.Depo.DepoKodu, SUM(wms.Yer.Miktar) as Miktar
	FROM            wms.Yer INNER JOIN
								wms.Kat ON wms.Yer.KatID = wms.Kat.ID AND wms.Yer.KatID = wms.Kat.ID INNER JOIN
								wms.Bolum ON wms.Kat.BolumID = wms.Bolum.ID AND wms.Kat.BolumID = wms.Bolum.ID INNER JOIN
								wms.Raf ON wms.Bolum.RafID = wms.Raf.ID AND wms.Bolum.RafID = wms.Raf.ID INNER JOIN
								wms.Koridor ON wms.Raf.KoridorID = wms.Koridor.ID AND wms.Raf.KoridorID = wms.Koridor.ID INNER JOIN
								wms.Depo ON wms.Koridor.DepoID = wms.Depo.ID
	GROUP BY wms.Depo.DepoKodu, wms.Yer.MalKodu
)
GO
/****** Object:  View [dbo].[RowCount]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RowCount]
AS
SELECT        TOP (100) PERCENT t.name AS TableName, s.name AS SchemaName, p.rows AS RowCounts, SUM(a.total_pages) * 8 AS TotalSpaceKB, SUM(a.used_pages) 
                         * 8 AS UsedSpaceKB, (SUM(a.total_pages) - SUM(a.used_pages)) * 8 AS UnusedSpaceKB
FROM            sys.tables AS t INNER JOIN
                         sys.indexes AS i ON t.object_id = i.object_id INNER JOIN
                         sys.partitions AS p ON i.object_id = p.object_id AND i.index_id = p.index_id INNER JOIN
                         sys.allocation_units AS a ON p.partition_id = a.container_id LEFT OUTER JOIN
                         sys.schemas AS s ON t.schema_id = s.schema_id
WHERE        (t.name NOT LIKE 'dt%') AND (t.is_ms_shipped = 0) AND (i.object_id > 255)
GROUP BY t.name, s.name, p.rows
ORDER BY TableName





GO
/****** Object:  Table [dbo].[Combo_Name]    Script Date: 05/04/2018 10:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combo_Name](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ComboName] [varchar](50) NOT NULL,
	[Açıklama] [nvarchar](250) NULL,
 CONSTRAINT [PK_ComboName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Combo_Name] UNIQUE NONCLUSTERED 
(
	[ComboName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ComboItem_Name]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ComboItem_Name](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ComboID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Visible] [bit] NOT NULL,
 CONSTRAINT [PK_ComboItemName] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ComboItem_Name] UNIQUE NONCLUSTERED 
(
	[ComboID] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Connections]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Connections](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ConnectionId] [nvarchar](max) NOT NULL,
	[UserName] [varchar](5) NOT NULL,
	[IsOnline] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Connections] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Etkinlik]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Etkinlik](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TatilTipi] [int] NOT NULL,
	[Tarih] [date] NOT NULL,
	[Username] [varchar](5) NULL,
	[Aciklama] [nvarchar](250) NULL,
	[Sure] [int] NULL,
	[Tekrarlayan] [bit] NOT NULL,
	[Onay] [bit] NOT NULL,
 CONSTRAINT [PK_Tatil] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQ](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TopicTypeID] [int] NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MesajTipi] [int] NOT NULL,
	[Tarih] [smalldatetime] NOT NULL,
	[Kimden] [varchar](5) NOT NULL,
	[Kime] [varchar](5) NOT NULL,
	[Mesaj] [nvarchar](max) NOT NULL,
	[URL] [varchar](50) NULL,
	[Goruldu] [bit] NOT NULL,
	[Okundu] [bit] NOT NULL,
	[GonderenSildi] [bit] NOT NULL,
	[AliciSildi] [bit] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[ID] [int] NOT NULL,
	[SiteName] [nvarchar](50) NOT NULL,
	[LoginLogo] [varchar](250) NOT NULL,
	[TopLogo] [varchar](250) NOT NULL,
	[AllowNewUser] [bit] NOT NULL,
	[AllowForgotPass] [bit] NOT NULL,
	[SmtpEmail] [varchar](150) NULL,
	[SmtpPass] [varchar](150) NULL,
	[SmtpPort] [int] NULL,
	[SmtpHost] [varchar](150) NULL,
	[SmtpSSL] [bit] NOT NULL,
	[OnayStok] [bit] NOT NULL,
	[OnayTeminat] [bit] NOT NULL,
	[OnaySiparis] [bit] NOT NULL,
	[OnaySozlesme] [bit] NOT NULL,
	[OnayRisk] [bit] NOT NULL,
	[OnayFiyat] [bit] NOT NULL,
	[OnayCek] [bit] NOT NULL,
	[OnayTekno] [bit] NOT NULL,
	[CrmOzet] [bit] NOT NULL,
	[GorevProjesi] [bit] NOT NULL,
	[KabloSiparisMySql] [bit] NOT NULL,
	[SevkiyatVarmi] [bit] NOT NULL,
	[SiparisOnayParametre] [bit] NOT NULL,
	[GitServerAddress] [varchar](250) NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Simge]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Simge](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Ad] [varchar](50) NOT NULL,
	[Icon] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Simge] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TABLE_INFO]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TABLE_INFO](
	[schema] [varchar](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[Usage] [varchar](max) NULL,
 CONSTRAINT [PK_TableInfo] PRIMARY KEY CLUSTERED 
(
	[schema] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebMenu]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebMenu](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[SiteTipiID] [int] NOT NULL,
	[MenuYeriID] [int] NOT NULL,
	[Ad] [nvarchar](50) NOT NULL,
	[Sira] [tinyint] NOT NULL,
	[Url] [varchar](100) NULL,
	[SimgeID] [smallint] NULL,
	[UstMenuID] [smallint] NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_WebSite] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_WebMenu] UNIQUE NONCLUSTERED 
(
	[MenuYeriID] ASC,
	[SiteTipiID] ASC,
	[UstMenuID] ASC,
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebMenuRol]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebMenuRol](
	[MenuID] [smallint] NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
 CONSTRAINT [PK_WebMenuYetki] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC,
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ong].[Gorevler]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ong].[Gorevler](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjeFormID] [int] NOT NULL,
	[Sorumlu] [varchar](5) NOT NULL,
	[Sorumlu2] [varchar](5) NULL,
	[Sorumlu3] [varchar](5) NULL,
	[KontrolSorumlusu] [varchar](5) NULL,
	[Gorev] [nvarchar](250) NOT NULL,
	[Aciklama] [nvarchar](max) NOT NULL,
	[OncelikID] [int] NOT NULL,
	[DurumID] [int] NOT NULL,
	[GorevTipiID] [int] NOT NULL,
	[DepartmanID] [int] NOT NULL,
	[TahminiBaslama] [smalldatetime] NULL,
	[TahminiBitis] [smalldatetime] NULL,
	[TahminiSure] [int] NULL,
	[BitisTarih] [smalldatetime] NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Gorevler] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ong].[GorevlerCalisma]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ong].[GorevlerCalisma](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GorevID] [int] NOT NULL,
	[Tarih] [date] NOT NULL,
	[Sure] [int] NOT NULL,
	[Calisma] [nvarchar](max) NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_GorevlerCalisma] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ong].[GorevlerToDoList]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ong].[GorevlerToDoList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GorevID] [int] NOT NULL,
	[Aciklama] [nvarchar](max) NOT NULL,
	[Onay] [bit] NOT NULL,
	[KontrolOnay] [bit] NOT NULL,
	[AdminOnay] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[Onaylayan] [varchar](5) NULL,
	[KontrolEden] [varchar](5) NULL,
 CONSTRAINT [PK_GorevToDoList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ong].[Musteri]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ong].[Musteri](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Firma] [varchar](20) NOT NULL,
	[Unvan] [nvarchar](80) NOT NULL,
	[Aciklama] [nvarchar](300) NULL,
	[Email] [varchar](80) NULL,
	[Tel1] [varchar](15) NULL,
	[Tel2] [varchar](15) NULL,
	[MesaiKontrol] [bit] NOT NULL,
	[MesaiKota] [int] NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Musteri] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Firma] UNIQUE NONCLUSTERED 
(
	[Firma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [ong].[ProjeForm]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ong].[ProjeForm](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MusteriID] [int] NOT NULL,
	[Proje] [varchar](120) NOT NULL,
	[Form] [varchar](250) NOT NULL,
	[Sorumlu] [varchar](5) NULL,
	[KarsiSorumlu] [varchar](50) NULL,
	[Aciklama] [varchar](max) NULL,
	[MesaiKontrol] [bit] NOT NULL,
	[MesaiKota] [int] NULL,
	[PID] [int] NULL,
	[GitAddress] [varchar](max) NULL,
	[GitGuid] [varchar](max) NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
	[Aktif] [bit] NOT NULL,
 CONSTRAINT [PK_Proje] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Proje_Form] UNIQUE NONCLUSTERED 
(
	[MusteriID] ASC,
	[Proje] ASC,
	[Form] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [ong].[ProjeFormDosya]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ong].[ProjeFormDosya](
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[ProjeID] [int] NOT NULL,
	[DosyaAdi] [nvarchar](250) NOT NULL,
	[Boyut] [varchar](50) NOT NULL,
	[BoyutByte] [int] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[Tarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_ProjeFormDosya] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[Perm]    Script Date: 05/04/2018 10:32:57 ******/
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
 CONSTRAINT [PK_Perm] PRIMARY KEY CLUSTERED 
(
	[PermName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[RolePerm]    Script Date: 05/04/2018 10:32:57 ******/
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
/****** Object:  Table [usr].[Roles]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
	[Aciklama] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Roles] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[UserDetails]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[UserDetails](
	[UserID] [int] NOT NULL,
	[DepoID] [int] NULL,
	[SatisIrsaliyeSeri] [int] NULL,
	[SatisFaturaSeri] [int] NULL,
	[AlimdanIadeFaturaSeri] [int] NULL,
	[SatistanIadeIrsaliyeSeri] [int] NULL,
	[TransferInSeri] [int] NULL,
	[TransferOutSeri] [int] NULL,
	[SayimSeri] [int] NULL,
	[GosterilecekSirket] [nvarchar](100) NULL,
	[GosterilecekDepo] [nvarchar](100) NULL,
	[PasifKartlariGoster] [bit] NOT NULL,
	[GostCHKKodAlani] [varchar](100) NULL,
	[GostCHKDeger] [varchar](150) NULL,
	[GostSTKKodAlani] [varchar](50) NULL,
	[GostSTKDeger] [varchar](500) NULL,
	[GostRiskDeger] [varchar](150) NULL,
	[GostKod3OrtBakiye] [varchar](150) NULL,
	[EvrakSeri] [int] NULL,
	[UretimDurum] [bit] NOT NULL,
 CONSTRAINT [PK_Depo_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Depo_Users] UNIQUE NONCLUSTERED 
(
	[UserID] ASC,
	[DepoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[UserDevices]    Script Date: 05/04/2018 10:32:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[UserDevices](
	[UserID] [int] NOT NULL,
	[Device] [varchar](250) NOT NULL,
	[FirsLoginDate] [smalldatetime] NOT NULL,
	[LastLoginDate] [smalldatetime] NOT NULL,
	[LoginCount] [int] NOT NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_UserDevices] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[Device] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [usr].[UserPerm]    Script Date: 05/04/2018 10:32:57 ******/
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
/****** Object:  Table [usr].[Users]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [usr].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Sirket] [varchar](5) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[Kod] [varchar](5) NOT NULL,
	[Sifre] [varchar](128) NOT NULL,
	[AdSoyad] [nvarchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[RoleName] [varchar](20) NOT NULL,
	[Tema] [varchar](50) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Aktif] [bit] NOT NULL,
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
 CONSTRAINT [PK_USR01] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Guid] UNIQUE NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Key] UNIQUE NONCLUSTERED 
(
	[Sirket] ASC,
	[Tip] ASC,
	[Kod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Gorev]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Gorev](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DepoID] [int] NOT NULL,
	[GorevNo] [varchar](50) NOT NULL,
	[GorevTipiID] [int] NOT NULL,
	[DurumID] [int] NOT NULL,
	[Olusturan] [varchar](5) NOT NULL,
	[OlusturmaTarihi] [int] NOT NULL,
	[OlusturmaSaati] [int] NOT NULL,
	[Bilgi] [nvarchar](max) NULL,
	[Aciklama] [nvarchar](max) NULL,
	[IrsaliyeID] [int] NULL,
	[Gorevli] [varchar](5) NULL,
	[Atayan] [varchar](5) NULL,
	[AtamaTarihi] [int] NULL,
	[BitisTarihi] [int] NULL,
	[BitisSaati] [int] NULL,
 CONSTRAINT [PK_GorevListesi] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [wms].[GorevIRS]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[GorevIRS](
	[GorevID] [int] NOT NULL,
	[IrsaliyeID] [int] NOT NULL,
 CONSTRAINT [PK_GorevIRS] PRIMARY KEY CLUSTERED 
(
	[GorevID] ASC,
	[IrsaliyeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[GorevNo]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[GorevNo](
	[Tarih] [int] NOT NULL,
	[DepoID] [int] NOT NULL,
	[No] [int] NOT NULL,
 CONSTRAINT [PK_AyarlarEvrakNo] PRIMARY KEY CLUSTERED 
(
	[Tarih] ASC,
	[DepoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[GorevPaketler]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[GorevPaketler](
	[GorevID] [int] NOT NULL,
	[SevkiyatNo] [varchar](50) NOT NULL,
	[PaketNo] [varchar](50) NOT NULL,
	[Adet] [numeric](25, 6) NOT NULL,
	[PaketTipiID] [int] NOT NULL,
	[Agirlik] [numeric](25, 6) NOT NULL,
	[Printed] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_GorevPaketler] PRIMARY KEY CLUSTERED 
(
	[GorevID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[GorevPaketNo]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[GorevPaketNo](
	[DepoID] [int] NOT NULL,
	[SevkiyatNo] [int] NOT NULL,
	[PaketNo] [int] NOT NULL,
	[MakaraNo] [int] NOT NULL,
 CONSTRAINT [PK_Settings_1] PRIMARY KEY CLUSTERED 
(
	[DepoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[GorevUsers]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[GorevUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GorevID] [int] NOT NULL,
	[UserName] [varchar](5) NOT NULL,
	[BaslamaTarihi] [int] NOT NULL,
	[BitisTarihi] [int] NULL,
 CONSTRAINT [PK_GorevUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_GorevUsers] UNIQUE NONCLUSTERED 
(
	[GorevID] ASC,
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[GorevYer]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[GorevYer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GorevID] [int] NOT NULL,
	[YerID] [int] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Miktar] [numeric](25, 6) NOT NULL,
	[YerlestirmeMiktari] [numeric](25, 6) NULL,
	[MakaraNo] [varchar](50) NULL,
	[Birim] [varchar](4) NOT NULL,
	[GC] [bit] NOT NULL,
	[Sira] [int] NULL,
 CONSTRAINT [PK_GorevYer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[IRS]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[IRS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKod] [varchar](2) NOT NULL,
	[DepoID] [int] NOT NULL,
	[IslemTur] [bit] NOT NULL,
	[EvrakNo] [varchar](16) NOT NULL,
	[HesapKodu] [varchar](20) NOT NULL,
	[Tarih] [int] NOT NULL,
	[Onay] [bit] NOT NULL,
	[TeslimCHK] [varchar](20) NULL,
	[ValorGun] [smallint] NULL,
	[Aciklama] [nvarchar](120) NULL,
	[LinkEvrakNo] [varchar](16) NULL,
 CONSTRAINT [PK_Irsaliye] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[IRS_Detay]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[IRS_Detay](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IrsaliyeID] [int] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Miktar] [numeric](25, 6) NOT NULL,
	[Birim] [varchar](4) NOT NULL,
	[MakaraNo] [varchar](50) NULL,
	[KynkSiparisID] [int] NULL,
	[KynkSiparisNo] [varchar](16) NULL,
	[KynkSiparisSiraNo] [smallint] NULL,
	[KynkSiparisTarih] [int] NULL,
	[KynkSiparisMiktar] [numeric](25, 6) NULL,
	[KynkDegisSaat] [int] NULL,
	[OkutulanMiktar] [numeric](25, 6) NULL,
	[YerlestirmeMiktari] [numeric](25, 6) NULL,
 CONSTRAINT [PK_Malzeme] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Olcu]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Olcu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Birim] [varchar](4) NOT NULL,
	[En] [numeric](25, 6) NULL,
	[Boy] [numeric](25, 6) NULL,
	[Derinlik] [numeric](25, 6) NULL,
	[Agirlik] [numeric](25, 6) NULL,
	[Hacim]  AS (([En]*[Boy])*[Derinlik]) PERSISTED,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarih] [int] NOT NULL,
	[Degistiren] [varchar](5) NOT NULL,
	[DegisTarih] [int] NOT NULL,
 CONSTRAINT [PK_Olcu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [PK_MalKoduBirim] UNIQUE NONCLUSTERED 
(
	[MalKodu] ASC,
	[Birim] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Transfer]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Transfer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GorevID] [int] NOT NULL,
	[SirketKod] [varchar](2) NOT NULL,
	[GirisDepoID] [int] NOT NULL,
	[CikisDepoID] [int] NOT NULL,
	[AraDepoID] [int] NOT NULL,
	[Onay] [bit] NOT NULL,
	[Onaylayan] [varchar](5) NULL,
	[OnaylamaTarihi] [int] NULL,
	[OnaylamaSaati] [int] NULL,
 CONSTRAINT [PK_Transfer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Transfer_Detay]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Transfer_Detay](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransferID] [int] NOT NULL,
	[MalKodu] [varchar](30) NOT NULL,
	[Miktar] [numeric](25, 6) NOT NULL,
	[Birim] [varchar](4) NOT NULL,
 CONSTRAINT [PK_Transfer_Detay] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [wms].[Yer_Log]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [wms].[Yer_Log](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KatID] [int] NOT NULL,
	[DepoID]  AS ([wms].[fnGetDepoIDFromKatID]([KatID])),
	[HucreAd]  AS ([wms].[fnGetHucreAdFromKatID]([KatID])),
	[MalKodu] [varchar](30) NOT NULL,
	[Birim] [varchar](4) NOT NULL,
	[Miktar] [numeric](25, 6) NOT NULL,
	[GC] [bit] NOT NULL,
	[Kaydeden] [varchar](5) NOT NULL,
	[KayitTarihi] [int] NOT NULL,
	[KayitSaati] [int] NOT NULL,
	[IrsaliyeID] [int] NULL,
	[MakaraNo] [varchar](50) NULL,
	[IslemTipi] [varchar](150) NULL,
	[IRSDetayID] [int] NULL,
 CONSTRAINT [PK_Yerlestirme_Hareketler] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserName]    Script Date: 05/04/2018 10:32:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserName] ON [dbo].[Connections]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Tatil]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [UQ_Tatil] ON [dbo].[Etkinlik]
(
	[TatilTipi] ASC,
	[Username] ASC,
	[Tarih] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [GorevlerCalismaTarihKaydeden]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [GorevlerCalismaTarihKaydeden] ON [ong].[GorevlerCalisma]
(
	[Tarih] ASC,
	[Kaydeden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [GorevlerToDoListGorevIDAdminOnay]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [GorevlerToDoListGorevIDAdminOnay] ON [ong].[GorevlerToDoList]
(
	[GorevID] ASC,
	[AdminOnay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [GorevlerToDoListGorevIDOnay]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [GorevlerToDoListGorevIDOnay] ON [ong].[GorevlerToDoList]
(
	[GorevID] ASC,
	[Onay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Gorev]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [IX_Gorev] ON [wms].[Gorev]
(
	[DepoID] ASC,
	[GorevTipiID] ASC,
	[DurumID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_GorevYer]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [IX_GorevYer] ON [wms].[GorevYer]
(
	[GorevID] ASC,
	[YerID] ASC,
	[MalKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EvrakNoUniqueKey]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [EvrakNoUniqueKey] ON [wms].[IRS]
(
	[SirketKod] ASC,
	[IslemTur] ASC,
	[EvrakNo] ASC,
	[HesapKodu] ASC,
	[ValorGun] ASC,
	[TeslimCHK] ASC,
	[Aciklama] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [SectionId-FloorName-UniqueKey]    Script Date: 05/04/2018 10:32:58 ******/
CREATE NONCLUSTERED INDEX [SectionId-FloorName-UniqueKey] ON [wms].[Kat]
(
	[BolumID] ASC,
	[KatAd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ComboItem_Name] ADD  CONSTRAINT [DF_ComboItemName_ItemVisible]  DEFAULT ((1)) FOR [Visible]
GO
ALTER TABLE [dbo].[Etkinlik] ADD  CONSTRAINT [DF_Etkinlik_Tekrarlayan]  DEFAULT ((0)) FOR [Tekrarlayan]
GO
ALTER TABLE [dbo].[Etkinlik] ADD  CONSTRAINT [DF_Etkinlik_Onay]  DEFAULT ((1)) FOR [Onay]
GO
ALTER TABLE [dbo].[FAQ] ADD  CONSTRAINT [DF_FAQ_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_Goruldu]  DEFAULT ((0)) FOR [Goruldu]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_Read]  DEFAULT ((0)) FOR [Okundu]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_GonderenSildi]  DEFAULT ((0)) FOR [GonderenSildi]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_AliciSildi]  DEFAULT ((0)) FOR [AliciSildi]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_AllowNewUser]  DEFAULT ((0)) FOR [AllowNewUser]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_AllowForgotPass]  DEFAULT ((0)) FOR [AllowForgotPass]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_SmtpSSL]  DEFAULT ((0)) FOR [SmtpSSL]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayStok]  DEFAULT ((0)) FOR [OnayStok]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayStok1]  DEFAULT ((0)) FOR [OnayTeminat]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayStok2]  DEFAULT ((0)) FOR [OnaySiparis]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayStok3]  DEFAULT ((0)) FOR [OnaySozlesme]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayStok4]  DEFAULT ((0)) FOR [OnayRisk]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayStok5]  DEFAULT ((0)) FOR [OnayFiyat]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayStok6]  DEFAULT ((0)) FOR [OnayCek]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_OnayCek1]  DEFAULT ((0)) FOR [OnayTekno]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_CrmOzet]  DEFAULT ((0)) FOR [CrmOzet]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_GorevProjesi]  DEFAULT ((0)) FOR [GorevProjesi]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_KabloSiparisMySql]  DEFAULT ((0)) FOR [KabloSiparisMySql]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_SmtpSSL1]  DEFAULT ((0)) FOR [SevkiyatVarmi]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_SiparisOnayParametre]  DEFAULT ((0)) FOR [SiparisOnayParametre]
GO
ALTER TABLE [dbo].[Settings] ADD  CONSTRAINT [DF_Settings_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [dbo].[WebMenu] ADD  CONSTRAINT [DF_WebSiteMenu_Sira]  DEFAULT ((0)) FOR [Sira]
GO
ALTER TABLE [dbo].[WebMenu] ADD  CONSTRAINT [DF_WebSiteMenu_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [ong].[Gorevler] ADD  CONSTRAINT [DF_Gorev_Oncelik]  DEFAULT ((0)) FOR [OncelikID]
GO
ALTER TABLE [ong].[Gorevler] ADD  CONSTRAINT [DF_Gorevler_Durum]  DEFAULT ((0)) FOR [DurumID]
GO
ALTER TABLE [ong].[Gorevler] ADD  CONSTRAINT [DF_Gorevler_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [ong].[Gorevler] ADD  CONSTRAINT [DF_Gorevler_DegisTarih]  DEFAULT (getdate()) FOR [DegisTarih]
GO
ALTER TABLE [ong].[GorevlerCalisma] ADD  CONSTRAINT [DF_GorevCalisma_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [ong].[GorevlerCalisma] ADD  CONSTRAINT [DF_GorevCalisma_DegisTarih]  DEFAULT (getdate()) FOR [DegisTarih]
GO
ALTER TABLE [ong].[GorevlerToDoList] ADD  CONSTRAINT [DF_GorevToDoList_OnayDurum]  DEFAULT ((0)) FOR [Onay]
GO
ALTER TABLE [ong].[GorevlerToDoList] ADD  CONSTRAINT [DF_GorevToDoList_KontrolOnay]  DEFAULT ((0)) FOR [KontrolOnay]
GO
ALTER TABLE [ong].[GorevlerToDoList] ADD  CONSTRAINT [DF_GorevlerToDoList_AdminOnay]  DEFAULT ((0)) FOR [AdminOnay]
GO
ALTER TABLE [ong].[Musteri] ADD  CONSTRAINT [DF_Musteri_MesaiKontrol]  DEFAULT ((0)) FOR [MesaiKontrol]
GO
ALTER TABLE [ong].[Musteri] ADD  CONSTRAINT [DF_Musteri_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [ong].[Musteri] ADD  CONSTRAINT [DF_Musteri_DegisTarih]  DEFAULT (getdate()) FOR [DegisTarih]
GO
ALTER TABLE [ong].[ProjeForm] ADD  CONSTRAINT [DF_ProjeForm_MesaiKontrol]  DEFAULT ((0)) FOR [MesaiKontrol]
GO
ALTER TABLE [ong].[ProjeForm] ADD  CONSTRAINT [DF_ProjeForm_KayitTarih]  DEFAULT (getdate()) FOR [KayitTarih]
GO
ALTER TABLE [ong].[ProjeForm] ADD  CONSTRAINT [DF_ProjeForm_DegisTarih]  DEFAULT (getdate()) FOR [DegisTarih]
GO
ALTER TABLE [ong].[ProjeForm] ADD  CONSTRAINT [DF_ProjeForm_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [ong].[ProjeFormDosya] ADD  CONSTRAINT [DF_Dosya_Guid]  DEFAULT (newid()) FOR [Guid]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_Type_1]  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_Type]  DEFAULT ((0)) FOR [AppType]
GO
ALTER TABLE [usr].[Perm] ADD  CONSTRAINT [DF_Perm_RecordDate]  DEFAULT (getdate()) FOR [RecordDate]
GO
ALTER TABLE [usr].[UserDetails] ADD  CONSTRAINT [DF_UserDetails_PasifKartlariGoster]  DEFAULT ((0)) FOR [PasifKartlariGoster]
GO
ALTER TABLE [usr].[UserDetails] ADD  CONSTRAINT [DF_UserDetails_UretimDurum]  DEFAULT ((0)) FOR [UretimDurum]
GO
ALTER TABLE [usr].[UserDevices] ADD  CONSTRAINT [DF_UserDevices_LoginCount]  DEFAULT ((1)) FOR [LoginCount]
GO
ALTER TABLE [usr].[UserDevices] ADD  CONSTRAINT [DF_UserDevices_Approved]  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [usr].[Users] ADD  CONSTRAINT [DF_Users_Guid]  DEFAULT (newid()) FOR [Guid]
GO
ALTER TABLE [wms].[Bolum] ADD  CONSTRAINT [DF_TK_BOL_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [wms].[Depo] ADD  CONSTRAINT [DF_TK_DEP_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [wms].[Gorev] ADD  CONSTRAINT [DF_GorevListesi_DurumID]  DEFAULT ((9)) FOR [DurumID]
GO
ALTER TABLE [wms].[GorevNo] ADD  CONSTRAINT [DF_AyarlarGorev_GorevSayisi]  DEFAULT ((0)) FOR [No]
GO
ALTER TABLE [wms].[GorevPaketler] ADD  CONSTRAINT [DF_GorevPaketler_Agirlik]  DEFAULT ((0)) FOR [Agirlik]
GO
ALTER TABLE [wms].[GorevPaketler] ADD  CONSTRAINT [DF_GorevPaketler_Printed]  DEFAULT ((1)) FOR [Printed]
GO
ALTER TABLE [wms].[GorevPaketNo] ADD  CONSTRAINT [DF_GorevPaketNo_SevkiyatNo]  DEFAULT ((0)) FOR [SevkiyatNo]
GO
ALTER TABLE [wms].[GorevPaketNo] ADD  CONSTRAINT [DF_GorevPaketNo_PaketNo]  DEFAULT ((0)) FOR [PaketNo]
GO
ALTER TABLE [wms].[GorevPaketNo] ADD  CONSTRAINT [DF_GorevPaketNo_MakaraNo]  DEFAULT ((0)) FOR [MakaraNo]
GO
ALTER TABLE [wms].[GorevYer] ADD  CONSTRAINT [DF_GorevYer_YerlestirmeMiktari]  DEFAULT ((0)) FOR [YerlestirmeMiktari]
GO
ALTER TABLE [wms].[GorevYer] ADD  CONSTRAINT [DF_GorevYer_GC]  DEFAULT ((0)) FOR [GC]
GO
ALTER TABLE [wms].[IRS] ADD  CONSTRAINT [DF_IRS_IslemTur]  DEFAULT ((0)) FOR [IslemTur]
GO
ALTER TABLE [wms].[IRS] ADD  CONSTRAINT [DF_IRS_Onay]  DEFAULT ((0)) FOR [Onay]
GO
ALTER TABLE [wms].[IRS_Detay] ADD  CONSTRAINT [DF_IRS_Detay_YerlestirmeDurumu]  DEFAULT ((0)) FOR [YerlestirmeMiktari]
GO
ALTER TABLE [wms].[Kat] ADD  CONSTRAINT [DF_TK_KAT_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [wms].[Koridor] ADD  CONSTRAINT [DF_TK_KOR_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [wms].[Olcu] ADD  CONSTRAINT [DF_Olcu_En]  DEFAULT ((0)) FOR [En]
GO
ALTER TABLE [wms].[Olcu] ADD  CONSTRAINT [DF_Olcu_Boy]  DEFAULT ((0)) FOR [Boy]
GO
ALTER TABLE [wms].[Olcu] ADD  CONSTRAINT [DF_Olcu_Genislik]  DEFAULT ((0)) FOR [Derinlik]
GO
ALTER TABLE [wms].[Olcu] ADD  CONSTRAINT [DF_Olcu_Agirlik]  DEFAULT ((0)) FOR [Agirlik]
GO
ALTER TABLE [wms].[Raf] ADD  CONSTRAINT [DF_TK_RAF_Aktif]  DEFAULT ((1)) FOR [Aktif]
GO
ALTER TABLE [wms].[Transfer] ADD  CONSTRAINT [DF_Transfer_Onay]  DEFAULT ((0)) FOR [Onay]
GO
ALTER TABLE [wms].[Yer] ADD  CONSTRAINT [DF_Yer_MakaraDurum]  DEFAULT ((1)) FOR [MakaraDurum]
GO
ALTER TABLE [dbo].[ComboItem_Name]  WITH CHECK ADD  CONSTRAINT [FK_ComboItemName_ComboName] FOREIGN KEY([ComboID])
REFERENCES [dbo].[Combo_Name] ([ID])
GO
ALTER TABLE [dbo].[ComboItem_Name] CHECK CONSTRAINT [FK_ComboItemName_ComboName]
GO
ALTER TABLE [dbo].[Etkinlik]  WITH CHECK ADD  CONSTRAINT [FK_Tatil_ComboItem_Name] FOREIGN KEY([TatilTipi])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [dbo].[Etkinlik] CHECK CONSTRAINT [FK_Tatil_ComboItem_Name]
GO
ALTER TABLE [dbo].[FAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_ComboItem_Name] FOREIGN KEY([TopicTypeID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [dbo].[FAQ] CHECK CONSTRAINT [FK_FAQ_ComboItem_Name]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_ComboItem_Name] FOREIGN KEY([MesajTipi])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_ComboItem_Name]
GO
ALTER TABLE [dbo].[WebMenu]  WITH CHECK ADD  CONSTRAINT [FK_WebMenu_MenuYeri] FOREIGN KEY([MenuYeriID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [dbo].[WebMenu] CHECK CONSTRAINT [FK_WebMenu_MenuYeri]
GO
ALTER TABLE [dbo].[WebMenu]  WITH CHECK ADD  CONSTRAINT [FK_WebMenu_Simge] FOREIGN KEY([SimgeID])
REFERENCES [dbo].[Simge] ([ID])
GO
ALTER TABLE [dbo].[WebMenu] CHECK CONSTRAINT [FK_WebMenu_Simge]
GO
ALTER TABLE [dbo].[WebMenu]  WITH CHECK ADD  CONSTRAINT [FK_WebMenu_SiteTipi] FOREIGN KEY([SiteTipiID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [dbo].[WebMenu] CHECK CONSTRAINT [FK_WebMenu_SiteTipi]
GO
ALTER TABLE [dbo].[WebMenu]  WITH CHECK ADD  CONSTRAINT [FK_WebMenu_WebMenu] FOREIGN KEY([UstMenuID])
REFERENCES [dbo].[WebMenu] ([ID])
GO
ALTER TABLE [dbo].[WebMenu] CHECK CONSTRAINT [FK_WebMenu_WebMenu]
GO
ALTER TABLE [dbo].[WebMenuRol]  WITH CHECK ADD  CONSTRAINT [FK_WebMenuRol_Roles] FOREIGN KEY([RoleName])
REFERENCES [usr].[Roles] ([RoleName])
GO
ALTER TABLE [dbo].[WebMenuRol] CHECK CONSTRAINT [FK_WebMenuRol_Roles]
GO
ALTER TABLE [dbo].[WebMenuRol]  WITH CHECK ADD  CONSTRAINT [FK_WebMenuRol_WebMenu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[WebMenu] ([ID])
GO
ALTER TABLE [dbo].[WebMenuRol] CHECK CONSTRAINT [FK_WebMenuRol_WebMenu]
GO
ALTER TABLE [ong].[Gorevler]  WITH CHECK ADD  CONSTRAINT [FK_Gorevler_ComboItem_Name_Departman] FOREIGN KEY([DepartmanID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [ong].[Gorevler] CHECK CONSTRAINT [FK_Gorevler_ComboItem_Name_Departman]
GO
ALTER TABLE [ong].[Gorevler]  WITH CHECK ADD  CONSTRAINT [FK_Gorevler_ComboItem_Name_Durum] FOREIGN KEY([DurumID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [ong].[Gorevler] CHECK CONSTRAINT [FK_Gorevler_ComboItem_Name_Durum]
GO
ALTER TABLE [ong].[Gorevler]  WITH CHECK ADD  CONSTRAINT [FK_Gorevler_ComboItem_Name_GorevTipi] FOREIGN KEY([GorevTipiID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [ong].[Gorevler] CHECK CONSTRAINT [FK_Gorevler_ComboItem_Name_GorevTipi]
GO
ALTER TABLE [ong].[Gorevler]  WITH CHECK ADD  CONSTRAINT [FK_Gorevler_ProjeForm] FOREIGN KEY([ProjeFormID])
REFERENCES [ong].[ProjeForm] ([ID])
GO
ALTER TABLE [ong].[Gorevler] CHECK CONSTRAINT [FK_Gorevler_ProjeForm]
GO
ALTER TABLE [ong].[GorevlerCalisma]  WITH CHECK ADD  CONSTRAINT [FK_GorevlerCalisma_Gorevler] FOREIGN KEY([GorevID])
REFERENCES [ong].[Gorevler] ([ID])
GO
ALTER TABLE [ong].[GorevlerCalisma] CHECK CONSTRAINT [FK_GorevlerCalisma_Gorevler]
GO
ALTER TABLE [ong].[GorevlerToDoList]  WITH CHECK ADD  CONSTRAINT [FK_GorevlerToDoList_Gorevler] FOREIGN KEY([GorevID])
REFERENCES [ong].[Gorevler] ([ID])
GO
ALTER TABLE [ong].[GorevlerToDoList] CHECK CONSTRAINT [FK_GorevlerToDoList_Gorevler]
GO
ALTER TABLE [ong].[ProjeForm]  WITH CHECK ADD  CONSTRAINT [FK_Proje_Musteri] FOREIGN KEY([MusteriID])
REFERENCES [ong].[Musteri] ([ID])
GO
ALTER TABLE [ong].[ProjeForm] CHECK CONSTRAINT [FK_Proje_Musteri]
GO
ALTER TABLE [ong].[ProjeForm]  WITH CHECK ADD  CONSTRAINT [FK_ProjeForm_ProjeForm] FOREIGN KEY([PID])
REFERENCES [ong].[ProjeForm] ([ID])
GO
ALTER TABLE [ong].[ProjeForm] CHECK CONSTRAINT [FK_ProjeForm_ProjeForm]
GO
ALTER TABLE [ong].[ProjeFormDosya]  WITH CHECK ADD  CONSTRAINT [FK_ProjeFormDosya_ProjeForm] FOREIGN KEY([ProjeID])
REFERENCES [ong].[ProjeForm] ([ID])
GO
ALTER TABLE [ong].[ProjeFormDosya] CHECK CONSTRAINT [FK_ProjeFormDosya_ProjeForm]
GO
ALTER TABLE [usr].[RolePerm]  WITH CHECK ADD  CONSTRAINT [FK_RolePerm_Perm] FOREIGN KEY([PermName])
REFERENCES [usr].[Perm] ([PermName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [usr].[RolePerm] CHECK CONSTRAINT [FK_RolePerm_Perm]
GO
ALTER TABLE [usr].[RolePerm]  WITH CHECK ADD  CONSTRAINT [FK_RolePerm_Roles] FOREIGN KEY([RoleName])
REFERENCES [usr].[Roles] ([RoleName])
GO
ALTER TABLE [usr].[RolePerm] CHECK CONSTRAINT [FK_RolePerm_Roles]
GO
ALTER TABLE [usr].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_Depo_Users_Depo] FOREIGN KEY([DepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [usr].[UserDetails] CHECK CONSTRAINT [FK_Depo_Users_Depo]
GO
ALTER TABLE [usr].[UserDetails]  WITH CHECK ADD  CONSTRAINT [FK_Depo_Users_Users] FOREIGN KEY([UserID])
REFERENCES [usr].[Users] ([ID])
GO
ALTER TABLE [usr].[UserDetails] CHECK CONSTRAINT [FK_Depo_Users_Users]
GO
ALTER TABLE [usr].[UserDevices]  WITH CHECK ADD  CONSTRAINT [FK_UserDevices_Users] FOREIGN KEY([UserID])
REFERENCES [usr].[Users] ([ID])
GO
ALTER TABLE [usr].[UserDevices] CHECK CONSTRAINT [FK_UserDevices_Users]
GO
ALTER TABLE [usr].[UserPerm]  WITH CHECK ADD  CONSTRAINT [FK_Perm_UserPerm2] FOREIGN KEY([PermName])
REFERENCES [usr].[Perm] ([PermName])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [usr].[UserPerm] CHECK CONSTRAINT [FK_Perm_UserPerm2]
GO
ALTER TABLE [usr].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleName])
REFERENCES [usr].[Roles] ([RoleName])
GO
ALTER TABLE [usr].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [usr].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([ID])
REFERENCES [usr].[Users] ([ID])
GO
ALTER TABLE [usr].[Users] CHECK CONSTRAINT [FK_Users_Users]
GO
ALTER TABLE [wms].[Bolum]  WITH CHECK ADD  CONSTRAINT [FK_Bolum_Raf] FOREIGN KEY([RafID])
REFERENCES [wms].[Raf] ([ID])
GO
ALTER TABLE [wms].[Bolum] CHECK CONSTRAINT [FK_Bolum_Raf]
GO
ALTER TABLE [wms].[Gorev]  WITH CHECK ADD  CONSTRAINT [FK_Gorev_ComboItem_NameDurum] FOREIGN KEY([DurumID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [wms].[Gorev] CHECK CONSTRAINT [FK_Gorev_ComboItem_NameDurum]
GO
ALTER TABLE [wms].[Gorev]  WITH CHECK ADD  CONSTRAINT [FK_Gorev_ComboItem_NameOzellik] FOREIGN KEY([GorevTipiID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [wms].[Gorev] CHECK CONSTRAINT [FK_Gorev_ComboItem_NameOzellik]
GO
ALTER TABLE [wms].[Gorev]  WITH CHECK ADD  CONSTRAINT [FK_Gorev_Depo] FOREIGN KEY([DepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[Gorev] CHECK CONSTRAINT [FK_Gorev_Depo]
GO
ALTER TABLE [wms].[Gorev]  WITH CHECK ADD  CONSTRAINT [FK_GorevListesi_IRS] FOREIGN KEY([IrsaliyeID])
REFERENCES [wms].[IRS] ([ID])
GO
ALTER TABLE [wms].[Gorev] CHECK CONSTRAINT [FK_GorevListesi_IRS]
GO
ALTER TABLE [wms].[GorevIRS]  WITH CHECK ADD  CONSTRAINT [FK_GorevIRS_Gorev] FOREIGN KEY([GorevID])
REFERENCES [wms].[Gorev] ([ID])
GO
ALTER TABLE [wms].[GorevIRS] CHECK CONSTRAINT [FK_GorevIRS_Gorev]
GO
ALTER TABLE [wms].[GorevIRS]  WITH CHECK ADD  CONSTRAINT [FK_GorevIRS_IRS] FOREIGN KEY([IrsaliyeID])
REFERENCES [wms].[IRS] ([ID])
GO
ALTER TABLE [wms].[GorevIRS] CHECK CONSTRAINT [FK_GorevIRS_IRS]
GO
ALTER TABLE [wms].[GorevNo]  WITH CHECK ADD  CONSTRAINT [FK_GorevNo_Depo] FOREIGN KEY([DepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[GorevNo] CHECK CONSTRAINT [FK_GorevNo_Depo]
GO
ALTER TABLE [wms].[GorevPaketler]  WITH CHECK ADD  CONSTRAINT [FK_GorevPaketler_ComboItem_Name] FOREIGN KEY([PaketTipiID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [wms].[GorevPaketler] CHECK CONSTRAINT [FK_GorevPaketler_ComboItem_Name]
GO
ALTER TABLE [wms].[GorevPaketler]  WITH CHECK ADD  CONSTRAINT [FK_GorevPaketler_Gorev] FOREIGN KEY([GorevID])
REFERENCES [wms].[Gorev] ([ID])
GO
ALTER TABLE [wms].[GorevPaketler] CHECK CONSTRAINT [FK_GorevPaketler_Gorev]
GO
ALTER TABLE [wms].[GorevPaketNo]  WITH CHECK ADD  CONSTRAINT [FK_GorevPaketNo_Depo] FOREIGN KEY([DepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[GorevPaketNo] CHECK CONSTRAINT [FK_GorevPaketNo_Depo]
GO
ALTER TABLE [wms].[GorevUsers]  WITH CHECK ADD  CONSTRAINT [FK_GorevUsers_Gorev] FOREIGN KEY([GorevID])
REFERENCES [wms].[Gorev] ([ID])
GO
ALTER TABLE [wms].[GorevUsers] CHECK CONSTRAINT [FK_GorevUsers_Gorev]
GO
ALTER TABLE [wms].[GorevYer]  WITH CHECK ADD  CONSTRAINT [FK_GorevYer_Gorev] FOREIGN KEY([GorevID])
REFERENCES [wms].[Gorev] ([ID])
GO
ALTER TABLE [wms].[GorevYer] CHECK CONSTRAINT [FK_GorevYer_Gorev]
GO
ALTER TABLE [wms].[GorevYer]  WITH CHECK ADD  CONSTRAINT [FK_GorevYer_Yer] FOREIGN KEY([YerID])
REFERENCES [wms].[Yer] ([ID])
GO
ALTER TABLE [wms].[GorevYer] CHECK CONSTRAINT [FK_GorevYer_Yer]
GO
ALTER TABLE [wms].[IRS]  WITH CHECK ADD  CONSTRAINT [FK_IRS_Depo] FOREIGN KEY([DepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[IRS] CHECK CONSTRAINT [FK_IRS_Depo]
GO
ALTER TABLE [wms].[IRS_Detay]  WITH CHECK ADD  CONSTRAINT [FK_IRS_Detay_IRS] FOREIGN KEY([IrsaliyeID])
REFERENCES [wms].[IRS] ([ID])
GO
ALTER TABLE [wms].[IRS_Detay] CHECK CONSTRAINT [FK_IRS_Detay_IRS]
GO
ALTER TABLE [wms].[Kat]  WITH CHECK ADD  CONSTRAINT [FK_Kat_Bolum] FOREIGN KEY([BolumID])
REFERENCES [wms].[Bolum] ([ID])
GO
ALTER TABLE [wms].[Kat] CHECK CONSTRAINT [FK_Kat_Bolum]
GO
ALTER TABLE [wms].[Kat]  WITH CHECK ADD  CONSTRAINT [FK_Kat_ComboItem_Name] FOREIGN KEY([OzellikID])
REFERENCES [dbo].[ComboItem_Name] ([ID])
GO
ALTER TABLE [wms].[Kat] CHECK CONSTRAINT [FK_Kat_ComboItem_Name]
GO
ALTER TABLE [wms].[Koridor]  WITH CHECK ADD  CONSTRAINT [FK_Koridor_Depo] FOREIGN KEY([DepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[Koridor] CHECK CONSTRAINT [FK_Koridor_Depo]
GO
ALTER TABLE [wms].[Raf]  WITH CHECK ADD  CONSTRAINT [FK_Raf_Koridor] FOREIGN KEY([KoridorID])
REFERENCES [wms].[Koridor] ([ID])
GO
ALTER TABLE [wms].[Raf] CHECK CONSTRAINT [FK_Raf_Koridor]
GO
ALTER TABLE [wms].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Depo] FOREIGN KEY([GirisDepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[Transfer] CHECK CONSTRAINT [FK_Transfer_Depo]
GO
ALTER TABLE [wms].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Depo1] FOREIGN KEY([CikisDepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[Transfer] CHECK CONSTRAINT [FK_Transfer_Depo1]
GO
ALTER TABLE [wms].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Depo2] FOREIGN KEY([AraDepoID])
REFERENCES [wms].[Depo] ([ID])
GO
ALTER TABLE [wms].[Transfer] CHECK CONSTRAINT [FK_Transfer_Depo2]
GO
ALTER TABLE [wms].[Transfer]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Gorev] FOREIGN KEY([GorevID])
REFERENCES [wms].[Gorev] ([ID])
GO
ALTER TABLE [wms].[Transfer] CHECK CONSTRAINT [FK_Transfer_Gorev]
GO
ALTER TABLE [wms].[Transfer_Detay]  WITH CHECK ADD  CONSTRAINT [FK_Transfer_Detay_Transfer] FOREIGN KEY([TransferID])
REFERENCES [wms].[Transfer] ([ID])
GO
ALTER TABLE [wms].[Transfer_Detay] CHECK CONSTRAINT [FK_Transfer_Detay_Transfer]
GO
ALTER TABLE [dbo].[Settings]  WITH CHECK ADD  CONSTRAINT [CHK_Settings_singlerow] CHECK  (([ID]=(1)))
GO
ALTER TABLE [dbo].[Settings] CHECK CONSTRAINT [CHK_Settings_singlerow]
GO
/****** Object:  StoredProcedure [dbo].[_Sıfırla]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 22.02.2017
-- Last Modify: 10.11.2017
-- Description:	db sıfırla
-- =============================================
CREATE PROCEDURE [dbo].[_Sıfırla]
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM BIRIKIM_LOG.dbo.AppLog;DBCC CHECKIDENT ('BIRIKIM_LOG.dbo.AppLog', RESEED, 0);	
	DELETE FROM BIRIKIM_LOG.dbo.LoginLog;DBCC CHECKIDENT ('BIRIKIM_LOG.dbo.LoginLog', RESEED, 0);	
	DELETE FROM BIRIKIM_LOG.dbo.ErrorLog;DBCC CHECKIDENT ('BIRIKIM_LOG.dbo.ErrorLog', RESEED, 0);	
	DELETE FROM BIRIKIM_LOG.dbo.ELMAH_Error;DBCC CHECKIDENT ('BIRIKIM_LOG.dbo.ELMAH_Error', RESEED, 0);	

	DELETE FROM dbo.FAQ;DBCC CHECKIDENT ('dbo.[FAQ]', RESEED, 0);
	DELETE FROM dbo.Connections;DBCC CHECKIDENT ('dbo.[Connections]', RESEED, 0);
	DELETE FROM dbo.Etkinlik;DBCC CHECKIDENT ('dbo.[Etkinlik]', RESEED, 0);
	DELETE FROM dbo.[Messages];DBCC CHECKIDENT ('dbo.[Messages]', RESEED, 0);
	DELETE FROM dbo.WebMenuRol;
	DELETE FROM dbo.WebMenu;DBCC CHECKIDENT ('dbo.[WebMenu]', RESEED, 0);
	DELETE FROM usr.Roles where ID > 1;DBCC CHECKIDENT ('usr.[Roles]', RESEED, 0);
	DELETE FROM usr.RolePerm;DBCC CHECKIDENT ('usr.[RolePerm]', RESEED, 0);
	DELETE FROM usr.Perm;DBCC CHECKIDENT ('usr.[Perm]', RESEED, 0);
	DELETE FROM wms.Olcu;DBCC CHECKIDENT ('wms.[Olcu]', RESEED, 0);

	DELETE FROM wms.GorevNo;
	DELETE FROM wms.GorevIRS;
	DELETE FROM wms.GorevPaketler;
	DELETE FROM wms.GorevYer;DBCC CHECKIDENT ('wms.[GorevYer]', RESEED, 0);
	DELETE FROM wms.GorevUsers;DBCC CHECKIDENT ('wms.[GorevUsers]', RESEED, 0);

	DELETE FROM wms.Yer_Log;DBCC CHECKIDENT ('wms.[Yer_Log]', RESEED, 0);
	DELETE FROM wms.Yer;DBCC CHECKIDENT ('wms.[Yer]', RESEED, 0);
	DELETE FROM wms.Kat;DBCC CHECKIDENT ('wms.[Kat]', RESEED, 0);
	DELETE FROM wms.Bolum;DBCC CHECKIDENT ('wms.[Bolum]', RESEED, 0);
	DELETE FROM wms.Raf;DBCC CHECKIDENT ('wms.[Raf]', RESEED, 0);
	DELETE FROM wms.Koridor;DBCC CHECKIDENT ('wms.[Koridor]', RESEED, 0);

	DELETE FROM usr.UserDetails;
	DELETE FROM usr.UserDevices;
	DELETE FROM usr.Users where ID > 1;DBCC CHECKIDENT ('usr.[Users]', RESEED, 0);

	DELETE FROM wms.Transfer_Detay;DBCC CHECKIDENT ('wms.[Transfer_Detay]', RESEED, 0);
	DELETE FROM wms.Transfer;DBCC CHECKIDENT ('wms.[Transfer]', RESEED, 0);
	DELETE FROM wms.Gorev;DBCC CHECKIDENT ('wms.[Gorev]', RESEED, 0);
	DELETE FROM wms.IRS_Detay;DBCC CHECKIDENT ('wms.[IRS_Detay]', RESEED, 0);
	DELETE FROM wms.IRS;DBCC CHECKIDENT ('wms.[IRS]', RESEED, 0);	
	DELETE FROM wms.GorevPaketNo;
	DELETE FROM wms.Depo;DBCC CHECKIDENT ('wms.[Depo]', RESEED, 0);

	DELETE FROM ong.GorevlerCalisma;DBCC CHECKIDENT ('ong.[GorevlerCalisma]', RESEED, 0);
	DELETE FROM ong.GorevlerToDoList;DBCC CHECKIDENT ('ong.[GorevlerToDoList]', RESEED, 0);
	DELETE FROM ong.Gorevler;DBCC CHECKIDENT ('ong.[Gorevler]', RESEED, 0);
	DELETE FROM ong.ProjeForm;DBCC CHECKIDENT ('ong.[ProjeForm]', RESEED, 0);
	DELETE FROM ong.Musteri;DBCC CHECKIDENT ('ong.[Musteri]', RESEED, 0);

END

GO
/****** Object:  StoredProcedure [dbo].[CachedChartBakiyeRisk]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 26.07.2017
-- Last Modify: 26.07.2017
-- Description:	cache db'den DB_BakiyeRiskAnalizi getirir
-- =============================================
CREATE PROCEDURE [dbo].[CachedChartBakiyeRisk]
	@DB varchar(2)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT HesapKodu, Unvan, Borc, Alacak, Bakiye FROM BIRIKIM_LOG.dbo.DB_BakiyeRiskAnalizi WHERE DB = @DB ORDER BY ID

END


GO
/****** Object:  StoredProcedure [dbo].[CachedChartBekleyenUrunMiktarFiyat]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 26.07.2017
-- Last Modify: 26.07.2017
-- Description:	cache db'den DB_BekleyenSiparis_UrunGrubu Fiyat/Miktar getirir
-- =============================================
CREATE PROCEDURE [dbo].[CachedChartBekleyenUrunMiktarFiyat]
	@DB varchar(2),
	@Miktarmi bit

AS
BEGIN
	SET NOCOUNT ON;

	if (@Miktarmi = 0)
		SELECT GrupKod, KalanMiktar FROM BIRIKIM_LOG.dbo.DB_BekleyenSiparis_UrunGrubu_Fiyat WHERE DB = @DB ORDER BY ID
	else
		SELECT GrupKod, KalanMiktar FROM BIRIKIM_LOG.dbo.DB_BekleyenSiparis_UrunGrubu_Miktar WHERE DB = @DB ORDER BY ID

END

GO
/****** Object:  StoredProcedure [dbo].[CachedChartSatisBaglanti]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 27.07.2017
-- Last Modify: 27.07.2017
-- Description:	cache db'den DB_SatisBaglanti_UrunGrubu getirir
-- =============================================
CREATE PROCEDURE [dbo].[CachedChartSatisBaglanti]
	@DB varchar(2)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT MalKod, KalanTutar FROM BIRIKIM_LOG.dbo.DB_SatisBaglanti_UrunGrubu WHERE DB = @DB ORDER BY ID

END
GO
/****** Object:  StoredProcedure [dbo].[CRM_GorusmeNotlari]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.07.2017
-- Modify date: 31.07.2017
-- Description:	CRM_GorusmeNotlarini listeler
-- =============================================
CREATE PROCEDURE [dbo].[CRM_GorusmeNotlari]
	@Baslangic date,
	@Bitis date

AS
BEGIN
	SET NOCOUNT ON;
	
	IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '[CAMPUSLNK1]' OR name = '[CAMPUSLNK1]')))
		SELECT        GN.ID, KI.ADI + ' ' + KI.SOYADI AS GorusenKisi, KU.ADI AS GorusulenKurum, GN.GORUSULENKISI AS GorusulenKisi, GN.GORUSMETARIHI AS GorusmeTarihi, GN.YENIDENGORUSMETARIHI AS YenidenGorusmeTarihi, 
								 DF1.ACIKLAMA AS GorusmeBicimi, DF2.ACIKLAMA AS GorusmeNedeni, DF3.ACIKLAMA AS GorusmeNotuDurumu,
								 CAMPUSLNK1.dbo.TBLOPERATOR.KODU as Kaydeden, CAMPUSLNK1.dbo.TARIHCE.TARIH AS KayitTarih
		FROM            CAMPUSLNK1.dbo.TBLGORUSMENOTU AS GN WITH (NOLOCK) INNER JOIN
								CAMPUSLNK1.dbo.TARIHCE WITH (NOLOCK) ON CAMPUSLNK1.dbo.TARIHCE.TABLEROWID = GN.ID LEFT OUTER JOIN
								CAMPUSLNK1.dbo.TBLOPERATOR WITH (NOLOCK) ON CAMPUSLNK1.dbo.TARIHCE.KULLANICI = CAMPUSLNK1.dbo.TBLOPERATOR.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLKISI AS KI WITH (NOLOCK) ON GN.GORUSENKISI = KI.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLKURUM AS KU WITH (NOLOCK) ON GN.KURUM = KU.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DF1 WITH (NOLOCK) ON GN.GORUSMEBICIMI = DF1.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DF2 WITH (NOLOCK) ON GN.GORUSMENEDENI = DF2.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DF3 WITH (NOLOCK) ON GN.GORUSMENOTUDURUMU = DF3.ID
		WHERE			(CAMPUSLNK1.dbo.TARIHCE.TABLEID = 77) AND (CAMPUSLNK1.dbo.TARIHCE.ISLEMTURU = 8) AND 
						case when @Baslangic is not null then case when GN.GORUSMETARIHI>=@Baslangic then 1 else 0 end else 1 end = 1 AND
						case when @Bitis is not null then case when GN.GORUSMETARIHI<=@Bitis then 1 else 0 end else 1 end = 1

END

GO
/****** Object:  StoredProcedure [dbo].[CRM_GorusmeNotlariDetay]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 31.07.2017
-- Modify date: 31.07.2017
-- Description:	CRM_GorusmeNot datayı getirir
-- =============================================
CREATE PROCEDURE [dbo].[CRM_GorusmeNotlariDetay]
	@ID int

AS
BEGIN
	SET NOCOUNT ON;
	
	IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '[CAMPUSLNK1]' OR name = '[CAMPUSLNK1]')))
		SELECT        GN.GORUSMENOTU AS GorusmeNotu
		FROM            CAMPUSLNK1.dbo.TBLGORUSMENOTU AS GN WITH (NOLOCK)
		WHERE			GN.ID = @ID

END


GO
/****** Object:  StoredProcedure [dbo].[CRM_KurumKarti]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.07.2017
-- Modify date: 31.07.2017
-- Description:	CRM_KurumKartlarini listeler
-- =============================================
CREATE PROCEDURE [dbo].[CRM_KurumKarti]
	@Baslangic date,
	@Bitis date

AS
BEGIN
	SET NOCOUNT ON;
	
	IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '[CAMPUSLNK1]' OR name = '[CAMPUSLNK1]')))
		SELECT       KU.KODU AS Kodu, KU.ADI AS KisaAdi, KU.CARIHESAPKODU AS CariHesapKodu, KU.UNVAN1 + ' ' + KU.UNVAN2 AS Unvan, KU.WEBSAYFASI, KU.EMAIL1 AS EMAIL, DFKod3.ACIKLAMA AS Sektor, 
								 KU.ADRES1 + ' ' + KU.ADRES2 + ' ' + KU.ADRES3 AS Adres, KU.POSTAKODU AS PostaKodu, DFUlke.ACIKLAMA AS Ulke, DFIl.ACIKLAMA AS Il, DFIlce.ACIKLAMA AS Ilce, DFKod1.ACIKLAMA AS BayiTipi, 
								 DFKod2.ACIKLAMA AS Bolge, DFKod4.ACIKLAMA AS MusteriTipi, CAST(KU.CENTIK1 AS BIT) AS Parke, CAST(KU.CENTIK2 AS BIT) AS Plaka, CAST(KU.CENTIK3 AS BIT) AS Kontraplak, CAST(KU.CENTIK4 AS BIT) AS MutfakTezgahi, 
								 CAST(KU.CENTIK5 AS BIT) AS Kartela, CAST(KU.CENTIK6 AS BIT) AS Stand, CAST(KU.CENTIK7 AS BIT) AS FiltreKagit, CAST(KU.CENTIK8 AS BIT) AS EmprenyeliKagit, CAST(KU.CENTIK9 AS BIT) AS Tutkal, 
								 CAST(KU.CENTIK10 AS BIT) AS Palet, CAST(KU.CENTIK11 AS BIT) AS AhsapKiris, CAST(KU.CENTIK12 AS BIT) AS Kereste, CAST(KU.CENTIK13 AS BIT) AS TicariMal, CAST(KU.CENTIK14 AS BIT) AS Formaldehit,
								 CAMPUSLNK1.dbo.TBLOPERATOR.KODU as Kaydeden, CAMPUSLNK1.dbo.TARIHCE.TARIH AS KayitTarih
		FROM            CAMPUSLNK1.dbo.TBLKURUM AS KU WITH (NOLOCK) INNER JOIN
								CAMPUSLNK1.dbo.TARIHCE WITH (NOLOCK) ON CAMPUSLNK1.dbo.TARIHCE.TABLEROWID = KU.ID LEFT OUTER JOIN
								CAMPUSLNK1.dbo.TBLOPERATOR WITH (NOLOCK) ON CAMPUSLNK1.dbo.TARIHCE.KULLANICI = CAMPUSLNK1.dbo.TBLOPERATOR.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DFUlke WITH (NOLOCK) ON DFUlke.ID = KU.ULKE LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DFIl WITH (NOLOCK) ON DFIl.ID = KU.IL LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DFIlce WITH (NOLOCK) ON DFIlce.ID = KU.ILCE LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DFKod1 WITH (NOLOCK) ON DFKod1.ID = KU.KOD1 LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DFKod2 WITH (NOLOCK) ON DFKod2.ID = KU.KOD2 LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DFKod3 WITH (NOLOCK) ON DFKod3.ID = KU.KOD3 LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLDARREFERANS AS DFKod4 WITH (NOLOCK) ON DFKod4.ID = KU.KOD4
		WHERE			(CAMPUSLNK1.dbo.TARIHCE.TABLEID = 14) AND (CAMPUSLNK1.dbo.TARIHCE.ISLEMTURU = 8) AND 
						case when @Baslangic is not null then case when CAMPUSLNK1.dbo.TARIHCE.TARIH>=@Baslangic then 1 else 0 end else 1 end = 1 AND
						case when @Bitis is not null then case when CAMPUSLNK1.dbo.TARIHCE.TARIH<=@Bitis then 1 else 0 end else 1 end = 1

END

GO
/****** Object:  StoredProcedure [dbo].[CRM_TeklifAnaliz]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.07.2017
-- Modify date: 31.07.2017
-- Description:	CRM_TeklifAnalizlerini listeler
-- =============================================
CREATE PROCEDURE [dbo].[CRM_TeklifAnaliz]
	@Baslangic date,
	@Bitis date

AS
BEGIN
	SET NOCOUNT ON;
	
	IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '[CAMPUSLNK1]' OR name = '[CAMPUSLNK1]')))
		SELECT        TK.ID, OP.ADI AS TeklifSahibi, KU.ADI AS Musteri, TK.KONU, TK.TARIH, TK.GECERLILIKTARIHI, SUM(TD.TUTAR) AS ToplamTeklifTutari
		FROM            CAMPUSLNK1.dbo.TBLFIRSATTEKLIFLER AS TK WITH (NOLOCK) LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLKURUM AS KU WITH (NOLOCK) ON TK.MUSTERI = KU.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLOPERATOR AS OP WITH (NOLOCK) ON TK.SAHIBI = OP.ID LEFT OUTER JOIN
								 CAMPUSLNK1.dbo.TBLFIRSATTEKLIFDETAY AS TD WITH (NOLOCK) ON TD.TEKLIF = TK.ID
		WHERE 
						case when @Baslangic is not null then case when TK.TARIH>=@Baslangic then 1 else 0 end else 1 end = 1 AND
						case when @Bitis is not null then case when TK.TARIH<=@Bitis then 1 else 0 end else 1 end = 1
		GROUP BY TK.ID, OP.ADI, KU.ADI, TK.KONU, TK.TARIH, TK.GECERLILIKTARIHI

END

GO
/****** Object:  StoredProcedure [dbo].[CRM_TeklifAnaliz_Detay]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.07.2017
-- Modify date: 14.07.2017
-- Description:	CRM_TeklifAnaliz_Detayı getirir
-- =============================================
CREATE PROCEDURE [dbo].[CRM_TeklifAnaliz_Detay]
	@TeklifID INT

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        TS.STOKKODU, TS.ACIKLAMA, TD.BIRIMFIYAT, TD.ADET AS Miktar, TD.ISKORAN, TD.ISKORAN2, TD.KDVORANI, TD.TUTAR
	FROM            CAMPUSLNK1.dbo.TBLFIRSATTEKLIFDETAY AS TD WITH (NOLOCK) LEFT OUTER JOIN
							 CAMPUSLNK1.dbo.TBLSTOK AS TS WITH (NOLOCK) ON TD.URUNKODU = TS.ID
	WHERE        (TD.TEKLIF = @TeklifID)

END

GO
/****** Object:  StoredProcedure [dbo].[GetChats]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 12.01.2018
-- Last Modify: 12.01.2018
-- Description:	özel ve genel mesajları getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetChats]
	@MesajTipi int,
	@Kimden varchar(5),
	@Kime varchar(5)

AS
BEGIN
	SET NOCOUNT ON;

IF (@MesajTipi=84)
	UPDATE [Messages] Set Okundu = 1 WHERE [Messages].MesajTipi = 84 AND [Messages].Kimden = @Kimden AND [Messages].Kime = @Kime


	SELECT        TOP (100) usr.Users.AdSoyad, usr.Users.Kod, [Messages].Tarih, [Messages].Mesaj, usr.Users.[Guid] AS [ID]
	FROM            [Messages] INNER JOIN usr.Users ON [Messages].Kimden = usr.Users.Kod
	WHERE        ([Messages].MesajTipi = @MesajTipi) AND 
						case when @MesajTipi=84 then case when (([Messages].Kimden = @Kimden) AND ([Messages].Kime = @Kime) OR ([Messages].Kimden = @Kime) AND ([Messages].Kime = @Kimden)) then 1 else 0 end else 1 end = 1

	ORDER BY [Messages].Tarih

END

GO
/****** Object:  StoredProcedure [dbo].[GetHomeSummary]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 04.04.2017
-- Modify date: 11.12.2017
-- Description:	anasayfa özeti
-- =============================================
CREATE PROCEDURE [dbo].[GetHomeSummary]
	@UserName varchar(5),
	@UserID int,
	@YetkiliDepo int

AS
BEGIN
	SET NOCOUNT ON;

	--depo sayısı
	declare @depo int
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'wms'  AND  TABLE_NAME = 'Depo'))
		set @depo = (SELECT COUNT(ID) AS Expr1 FROM wms.Depo)
	ELSE
		set @depo = 0
	--kullanıcı sayısı
	declare @kull int
	set @kull = (SELECT COUNT(ID) AS Expr1 FROM usr.Users WHERE ID > 1)
	--görev sayısı
	declare @gorev int
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'wms'  AND  TABLE_NAME = 'Gorev'))
		set @gorev=(SELECT COUNT(ID) AS Expr1 FROM wms.Gorev WHERE (DurumID  =  9))
	ELSE
		set @gorev = 0
	--yetkiye sahip rapor sayısı
	declare @yetki varchar(MAX)
	if (@UserID = 1 and @UserName = 'super')
		set @yetki = (SELECT ''''+PermName+'''' FROM usr.[Perm] WHERE (Active = 1) AND (Type = 1) AND ([Group] = 'WMS') FOR XML PATH(''))
	else
		set @yetki = (select t+',' from (
						SELECT        ''''+usr.[Perm].PermName+'''' t
						FROM            usr.RolePerm INNER JOIN
												 usr.[Perm] ON usr.RolePerm.PermName = usr.[Perm].PermName INNER JOIN
												 usr.Roles INNER JOIN
												 usr.Users ON usr.Roles.RoleName = usr.Users.RoleName ON usr.RolePerm.RoleName = usr.Roles.RoleName
						WHERE        (usr.[Perm].Active = 1) AND usr.RolePerm.Reading = 1 AND (usr.[Perm].Type = 1) AND (usr.[Perm].[Group] = 'WMS') AND (usr.Users.ID = @UserID)
						UNION
						SELECT        ''''+usr.[Perm].PermName+'''' t
						FROM            usr.[Perm] INNER JOIN
												 usr.UserPerm ON usr.[Perm].PermName = usr.UserPerm.PermName
						WHERE        (usr.[Perm].Active = 1) AND usr.UserPerm.Reading = 1 AND (usr.[Perm].Type = 1) AND (usr.[Perm].[Group] = 'WMS') AND (usr.UserPerm.UserName = @UserName)
					) as t
					FOR XML PATH(''))
	--görev tipi sayıları
	declare @MalKabul int, @RafaKaldir int, @SiparisTopla int, @Paketle int, @Sevkiyat int, @Sayim int
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'wms'  AND  TABLE_NAME = 'Gorev'))
	BEGIN
		set @MalKabul = (SELECT ISNULL(COUNT(ID), 0) AS Expr1 FROM wms.Gorev WHERE (DurumID = 9) AND (GorevTipiID = 1) AND case when @YetkiliDepo <> null then case when DepoID = @YetkiliDepo then 1 else 0 end else 1 end = 1)
		set @RafaKaldir = (SELECT ISNULL(COUNT(ID), 0) AS Expr1 FROM wms.Gorev WHERE (DurumID = 9) AND (GorevTipiID = 2) AND case when @YetkiliDepo <> null then case when DepoID = @YetkiliDepo then 1 else 0 end else 1 end = 1)
		set @SiparisTopla = (SELECT ISNULL(COUNT(ID), 0) AS Expr1 FROM wms.Gorev WHERE (DurumID = 9) AND (GorevTipiID = 3) AND case when @YetkiliDepo <> null then case when DepoID = @YetkiliDepo then 1 else 0 end else 1 end = 1)
		set @Paketle = (SELECT ISNULL(COUNT(ID), 0) AS Expr1 FROM wms.Gorev WHERE (DurumID = 9) AND (GorevTipiID = 6) AND case when @YetkiliDepo <> null then case when DepoID = @YetkiliDepo then 1 else 0 end else 1 end = 1)
		set @Sevkiyat = (SELECT ISNULL(COUNT(ID), 0) AS Expr1 FROM wms.Gorev WHERE (DurumID = 9) AND (GorevTipiID = 7) AND case when @YetkiliDepo <> null then case when DepoID = @YetkiliDepo then 1 else 0 end else 1 end = 1)
		set @Sayim = (SELECT ISNULL(COUNT(ID), 0) AS Expr1 FROM wms.Gorev WHERE (DurumID = 9) AND (GorevTipiID = 8) AND case when @YetkiliDepo <> null then case when DepoID = @YetkiliDepo then 1 else 0 end else 1 end = 1)
	END
	ELSE
	BEGIN
		set @MalKabul = 0
		set @RafaKaldir = 0
		set @SiparisTopla = 0
		set @Paketle = 0
		set @Sevkiyat = 0
		set @Sayim = 0
	END
	--return
	select @depo as depo, @kull as kull, @gorev as gorev, @MalKabul as MalKabul, @RafaKaldir as RafaKaldir, @SiparisTopla as SiparisTopla, @Paketle as Paketle, @Sevkiyat as Sevkiyat, @Sayim as Sayim, ISNULL(@yetki,'') as yetki

END



GO
/****** Object:  StoredProcedure [dbo].[GetMenuRoleFor]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 11.05.2017
-- Last Modify: 02.10.2017
-- Description:	menu yetkilerini getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetMenuRoleFor]
	@RoleName varchar(20)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        WebMenu.ID, CASE WHEN WebMenu_2.Ad IS NOT NULL THEN WebMenu_2.Ad + ', ' ELSE '' END + CASE WHEN WebMenu_1.Ad IS NOT NULL THEN WebMenu_1.Ad + ', ' ELSE '' END + WebMenu.Ad AS Ad, 
							 CASE WHEN (SELECT RoleName FROM WebMenuRol WHERE (MenuID = WebMenu.ID) AND (RoleName = @RoleName OR RoleName IS NULL)) IS NULL then '' else ' checked' end  AS RoleName
							 --,WebMenu.Aktif,WebMenu_1.Aktif,WebMenu_2.Aktif,WebMenu_3.Aktif

	FROM            WebMenu AS WebMenu_3 RIGHT OUTER JOIN
							 WebMenu AS WebMenu_2 ON WebMenu_3.ID = WebMenu_2.UstMenuID RIGHT OUTER JOIN
							 WebMenu AS WebMenu_1 ON WebMenu_2.ID = WebMenu_1.UstMenuID RIGHT OUTER JOIN
							 WebMenu ON WebMenu_1.ID = WebMenu.UstMenuID
	WHERE        (WebMenu.Aktif = 1) AND (WebMenu_1.Aktif = 1 OR WebMenu_1.Aktif IS NULL) AND (WebMenu_2.Aktif = 1 OR WebMenu_2.Aktif IS NULL) AND (WebMenu_3.Aktif = 1 OR WebMenu_3.Aktif IS NULL)
	ORDER BY Ad

END
GO
/****** Object:  StoredProcedure [dbo].[GetNotificationsForActiveUsers]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 09.02.2018
-- Last Modify: 09.02.2018
-- Description:	ZigChatHub / SendNotifications: aktif kullanıcılar için bekleyen uyarı listesi
-- =============================================
CREATE PROCEDURE [dbo].[GetNotificationsForActiveUsers]

AS
BEGIN
	SET NOCOUNT ON;

	--show list
	SELECT        [Messages].ID, [Messages].Kime, ComboItem_Name.Name, [Messages].Mesaj, Connections.ConnectionId
	FROM            [Messages] INNER JOIN
							 Connections ON [Messages].Kime = Connections.UserName INNER JOIN
							 ComboItem_Name ON [Messages].MesajTipi = ComboItem_Name.ID
	WHERE        (Connections.IsOnline = 1) AND ([Messages].Goruldu = 0) AND ([Messages].MesajTipi = 85)

	--update as seen
	UPDATE [Messages] SET Goruldu = 1 WHERE ID IN (
		SELECT        [Messages].ID
		FROM            [Messages] INNER JOIN
								 Connections ON [Messages].Kime = Connections.UserName INNER JOIN
								 ComboItem_Name ON [Messages].MesajTipi = ComboItem_Name.ID
		WHERE        (Connections.IsOnline = 1) AND ([Messages].Goruldu = 0) AND ([Messages].MesajTipi = 85)
	)

END

GO
/****** Object:  StoredProcedure [dbo].[GetPermissionFor]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 26.04.2017
-- Last Modify: 27.04.2017
-- Description:	yetki getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetPermissionFor]
	@UserID int,
	@RoleName varchar(20),
	@PermName varchar(50),
	@Group varchar(50),
	@Perm varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	--declare
	DECLARE @Result bit
	set @Result=0
	--set for group
	SELECT       TOP(1) @Result = case 
						when @Perm='Reading' then usr.RolePerm.Reading 
						when @Perm='Writing' then usr.RolePerm.Writing 
						when @Perm='Updating' then usr.RolePerm.Updating 
						when @Perm='Deleting' then usr.RolePerm.Deleting
					end
	FROM            usr.[Perm] INNER JOIN
							 usr.RolePerm ON usr.[Perm].PermName = usr.RolePerm.PermName
	WHERE        (usr.[Perm].[Group] = @Group) AND (usr.RolePerm.RoleName = @RoleName) AND (usr.RolePerm.PermName = @PermName)
	--set for user
	if(@Result=0)
		SELECT        TOP(1) @Result = case 
							when @Perm='Reading' then usr.UserPerm.Reading 
							when @Perm='Writing' then usr.UserPerm.Writing 
							when @Perm='Updating' then usr.UserPerm.Updating 
							when @Perm='Deleting' then usr.UserPerm.Deleting
						end
		FROM            usr.Users INNER JOIN
								 usr.UserPerm ON usr.Users.Kod = usr.UserPerm.UserName
		WHERE        (usr.Users.ID = @UserID) AND (usr.UserPerm.PermName = @PermName)
	--super kontrol
	if (@UserID = 1)
		set @Result=1
	--result
	select ISNULL(@Result, 0) as Result

END

GO
/****** Object:  StoredProcedure [dbo].[GetRolePermsFor]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 09.05.2017
-- Last Modify: 25.05.2017
-- Description:	rol yetki getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetRolePermsFor]
	@RoleName varchar(20),
	@Group varchar(50)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        PermName, [Type], AppType,
					ISNULL((SELECT ID FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0) as ID,
					ISNULL((SELECT Reading FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0) as Reading,
					ISNULL((SELECT Writing FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0) as Writing,
					ISNULL((SELECT Updating FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0) as Updating,
					ISNULL((SELECT Deleting FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0) as Deleting
	FROM            usr.[Perm]
	WHERE		Active = 1 AND [Group] = @Group
	ORDER BY	[Type], PermName

END


GO
/****** Object:  StoredProcedure [dbo].[GetSirketMuhasebeKod]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 03.01.2017
-- Last Modify: 03.01.2017
-- Description:	şirketin aktif muhasebe şirketini getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetSirketMuhasebeKod]
	@SirketKodu varchar(5),
	@Tarih int

AS
BEGIN
	SET NOCOUNT ON;

		SELECT        ISNULL(
								(SELECT        TOP (1) SDK_1.Kod
                                 FROM            SOLAR6.dbo.SDK INNER JOIN
                                                          SOLAR6.dbo.SDK AS SDK_1 ON SOLAR6.dbo.SDK.SirketKod = SDK_1.SirketKod
                                 WHERE        (SOLAR6.dbo.SDK.Tip = 0) AND (SOLAR6.dbo.SDK.Kod = @SirketKodu) AND (SDK_1.Tip = 1) AND (SDK_1.Tarih <= @Tarih)
                                 ORDER BY SDK_1.Tarih DESC)
							, @SirketKodu) AS Kod

END

GO
/****** Object:  StoredProcedure [dbo].[GetSirketMuhasebeYear]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 03.01.2018
-- Last Modify: 07.02.2018
-- Description:	şirketin aktif muhasebe yılını getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetSirketMuhasebeYear]
	@SirketKodu varchar(5),
	@Tarih int

AS
BEGIN
	SET NOCOUNT ON;

		SELECT        ISNULL(
								(SELECT        TOP (1) YEAR(CAST(SDK_1.Tarih - 2 AS DATETIME)) AS Expr1
                                 FROM            SOLAR6.dbo.SDK INNER JOIN
                                                          SOLAR6.dbo.SDK AS SDK_1 ON SOLAR6.dbo.SDK.SirketKod = SDK_1.SirketKod
                                 WHERE        (SOLAR6.dbo.SDK.Tip = 0) AND (SOLAR6.dbo.SDK.Kod = @SirketKodu) AND (SDK_1.Tip = 1) AND (SDK_1.Tarih <= @Tarih)
                                 ORDER BY SDK_1.Tarih DESC)
							, YEAR(GETDATE())) AS Yil

END

GO
/****** Object:  StoredProcedure [dbo].[GetSirkets]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 20.02.2017
-- Last Modify: 31.02.2017
-- Description:	şirket listesini getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetSirkets]

AS
BEGIN
	SET NOCOUNT ON;

	SELECT '0test' as Kod, '0test' AS Ad,'0test' Adres1
	union 
	SELECT        solar6.dbo.SDK.Kod, solar6.dbo.SIR.KisaAdi + ' [' + solar6.dbo.SDK.Kod + ']' AS Ad, solar6.dbo.SIR.Adres1
	FROM            solar6.dbo.SIR INNER JOIN
							 solar6.dbo.SDK ON solar6.dbo.SIR.Kod = solar6.dbo.SDK.SirketKod
	WHERE        (solar6.dbo.SIR.AktifPasif = 0) AND (SDK.Tip = 0) AND (solar6.dbo.SDK.Kod IN ('17','18','33','71','99'))
	GROUP BY        solar6.dbo.SDK.Kod, solar6.dbo.SIR.KisaAdi + ' [' + solar6.dbo.SDK.Kod + ']', solar6.dbo.SIR.Adres1, solar6.dbo.SIR.KisaAdi
	ORDER BY Kod

END

GO
/****** Object:  StoredProcedure [dbo].[GetUserPermsFor]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 15.05.2017
-- Last Modify: 04.07.2017
-- Description:	kullanıcı yetki getirir
-- =============================================
CREATE PROCEDURE [dbo].[GetUserPermsFor]
	@UserID int

AS
BEGIN
	SET NOCOUNT ON;

	--get role name
	declare  @RoleName varchar(20)
	declare  @UserName varchar(5)
	select @RoleName = RoleName, @UserName = Kod from usr.Users where ID = @UserID
	--gets perms
	SELECT        PermName, 
					ISNULL((SELECT UserName FROM usr.UserPerm WHERE (PermName = usr.[Perm].PermName) AND (UserName = @UserName)), '') as UserName,
					ISNULL((SELECT Reading FROM usr.UserPerm WHERE (PermName = usr.[Perm].PermName) AND (UserName = @UserName)), ISNULL((SELECT Reading FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0)) as Reading,
					ISNULL((SELECT Writing FROM usr.UserPerm WHERE (PermName = usr.[Perm].PermName) AND (UserName = @UserName)), ISNULL((SELECT Writing FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0)) as Writing,
					ISNULL((SELECT Updating FROM usr.UserPerm WHERE (PermName = usr.[Perm].PermName) AND (UserName = @UserName)), ISNULL((SELECT Updating FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0)) as Updating,
					ISNULL((SELECT Deleting FROM usr.UserPerm WHERE (PermName = usr.[Perm].PermName) AND (UserName = @UserName)), ISNULL((SELECT Deleting FROM usr.RolePerm WHERE (PermName = usr.[Perm].PermName) AND (RoleName = @RoleName)), 0)) as Deleting
	FROM            usr.[Perm]
	WHERE		Active = 1 AND [Group] in( 'WMS','KRL')
	ORDER BY	[Type], PermName

END



GO
/****** Object:  StoredProcedure [dbo].[LogActions]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 12.06.2017
-- Last Modify: 15.06.2017
-- Description:	işlem kayıtları
-- =============================================
CREATE PROCEDURE [dbo].[LogActions]
    @Site varchar(50),
	@Area varchar(50),
	@Controller varchar(50),
	@Action varchar(50),
	@Type int,
	@SelectedID int,
	@Request varchar(MAX),
	@Details varchar(MAX),
	@Username varchar(5),
	@IpAddress varchar(50)

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO BIRIKIM_LOG.dbo.AppLog ([Tarih], [Site], [Area], [Controller], [Action], [Type], IpAddress, [SelectedID], [Request], [Details], [Username])
	VALUES										(GETDATE(), @Site, @Area, @Controller, @Action, @Type, @IpAddress, 
												case when @SelectedID = 0 then NULL else @SelectedID end, 
												case when @Request = '' then NULL else @Request end, 
												case when @Details = '' then NULL else @Details end, 
												case when @Username = '' then NULL else @Username end)

END
GO
/****** Object:  StoredProcedure [dbo].[Logger]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 02.03.2017
-- Last Modify: 29.03.2017
-- Description:	logger
-- =============================================
CREATE PROCEDURE [dbo].[Logger]
    @UserName varchar(50),
	@Machine varchar(50),
	@IpAddress varchar(50),
	@Description  varchar(max),
	@Message varchar(max),
	@Source varchar(50)

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO BIRIKIM_LOG.dbo.ErrorLog ([Description], [Message], [Source], [DateTime], UserName, Machine, IpAddress)
	VALUES										(@Description, @Message, @Source, GETDATE(), @UserName, @Machine, @IpAddress)

END
GO
/****** Object:  StoredProcedure [dbo].[LogLogins]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 31.05.2017
-- Last Modify: 31.05.2017
-- Description:	login logger
-- =============================================
CREATE PROCEDURE [dbo].[LogLogins]
    @UserName varchar(50),
	@IpAddress varchar(50),
	@LoggedIn bit,
	@Comment varchar(150)

AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO BIRIKIM_LOG.dbo.LoginLog (Username, IP, [DateTime], LoggedIn, Comment)
	VALUES										(@UserName, @IpAddress, GETDATE(), @LoggedIn, case when @Comment<> '' then @Comment else NULL end)

END

GO
/****** Object:  StoredProcedure [dbo].[MenuGetirici]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 15.03.2017
-- Modify date: 25.05.2017
-- Description:	menüyü getirir
-- =============================================
CREATE PROCEDURE [dbo].[MenuGetirici]
	@WebSiteTipiID int,
	@MenuYeriID int,
	@RoleName varchar(20),
	@UstMenuID smallint,
	@url varchar(100)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        w0.ID, w0.Ad, w0.[Url], Simge.Icon, @UstMenuID as UstMenuID,
					(SELECT        COUNT(wb.ID) AS sayi
					FROM            WebMenu as wb LEFT OUTER JOIN
												WebMenuRol ON wb.ID = WebMenuRol.MenuID
					WHERE        (wb.UstMenuID = w0.ID) AND (wb.MenuYeriID = @MenuYeriID) AND (wb.SiteTipiID = @WebSiteTipiID) AND (wb.Aktif = 1) AND
								CASE WHEN @RoleName <> '' then case when (WebMenuRol.RoleName = @RoleName) then 1 else 0 end else 1 end = 1
					) as AltmenuCount,
					case when EXISTS(
						SELECT ID FROM WebMenu as w1 WHERE ([Url] = @url) AND ID = w0.ID
						UNION
						SELECT w2.ID FROM WebMenu AS w1 INNER JOIN WebMenu AS w2 ON w2.UstMenuID = w1.ID WHERE (w1.ID = w0.ID) AND (w2.[Url] = @url)
						UNION
						SELECT w2.ID FROM WebMenu AS w1 INNER JOIN WebMenu AS w2 ON w2.UstMenuID = w1.ID INNER JOIN WebMenu AS w3 ON w3.UstMenuID = w2.ID WHERE (w1.ID = w0.ID) AND (w3.[Url] = @url)
					) then 1 else 0 end as Aktif

	FROM            WebMenuRol RIGHT OUTER JOIN
								WebMenu as w0 ON WebMenuRol.MenuID = w0.ID LEFT OUTER JOIN
								Simge ON w0.SimgeID = Simge.ID

	WHERE        (w0.MenuYeriID = @MenuYeriID) AND (w0.Aktif = 1) AND (w0.SiteTipiID = @WebSiteTipiID) AND 
					CASE WHEN @RoleName <> '' then case when (WebMenuRol.RoleName = @RoleName) then 1 else 0 end else 1 end = 1 AND 
					CASE WHEN @UstMenuID = 0 THEN  CASE WHEN (w0.UstMenuID IS NULL) THEN 1 ELSE 0 END  ELSE  CASE WHEN (w0.UstMenuID = @UstMenuID) THEN 1 ELSE 0 END  END = 1

	GROUP BY w0.ID, w0.Ad, w0.[Url], Simge.Icon, w0.Sira
	ORDER BY w0.Sira


END

GO
/****** Object:  StoredProcedure [dbo].[MenuRolEkle]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 13.07.2016
-- Modify date: 24.04.2017
-- Description:	menüye ait rolü ekler
-- =============================================
CREATE PROCEDURE [dbo].[MenuRolEkle]
	@MenuID smallint,
	@RolNo varchar(MAX)

AS
BEGIN
	SET NOCOUNT ON;

	-- önce menünün tüm yetkilerini siliyoruz
	DELETE FROM WebMenuRol
	WHERE        (MenuID = @MenuID)
	-- sonra yeni yetkileri ekliyoruz
	INSERT        INTO              WebMenuRol(MenuID, RoleName)
	SELECT @MenuID as mno,RoleName FROM usr.Roles where ID IN (Select * from dbo.splitstring(@RolNo))


END

GO
/****** Object:  StoredProcedure [dbo].[MenuRolGetir]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 13.07.2016
-- Modify date: 11.05.2017
-- Description:	menüye ait olan ve olmayan rolleri getirir
-- =============================================
CREATE PROCEDURE [dbo].[MenuRolGetir]
	@MenuID int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        ID, RoleName, (SELECT RoleName FROM WebMenuRol WHERE (MenuID = @MenuID) AND (RoleName = usr.Roles.RoleName)) as Perm
	FROM            usr.Roles
	WHERE		RoleName<>''

END

GO
/****** Object:  StoredProcedure [dbo].[MenuSiralayici]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 13.06.2016
-- Modify date: 24.04.2017
-- Description:	menüyü sıralar
-- =============================================
CREATE PROCEDURE [dbo].[MenuSiralayici]
	@WebSiteTipiNo int,
	@MenuYeriNo int,
	@UstMenuNo smallint

AS
BEGIN
	SET NOCOUNT ON;


	UPDATE WebMenu

	SET WebMenu.Sira = WebMenu.Sira2

	FROM (
		  SELECT Sira, ROW_NUMBER() OVER (ORDER BY Sira) AS Sira2
		  FROM WebMenu
		  WHERE        (SiteTipiID = @WebSiteTipiNo) AND (MenuYeriID = @MenuYeriNo) AND 
					 ((CASE WHEN @UstMenuNo = 0 THEN 
						CASE WHEN (WebMenu.UstMenuID IS NULL) THEN 1 ELSE 0 END 
					 ELSE 
						CASE WHEN (WebMenu.UstMenuID = @UstMenuNo) THEN 1 ELSE 0 END 
					 END) = 1)
		) WebMenu


END

GO
/****** Object:  StoredProcedure [dbo].[RolMenuEkle]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 12.07.2017
-- Modify date: 12.07.2017
-- Description:	menüye ait rolü ekler
-- =============================================
CREATE PROCEDURE [dbo].[RolMenuEkle]
	@RoleName varchar(20),
	@MenuIDs varchar(MAX)

AS
BEGIN
	SET NOCOUNT ON;

	-- önce menünün tüm yetkilerini siliyoruz
	DELETE FROM WebMenuRol
	WHERE        (RoleName = @RoleName)
	-- sonra yeni yetkileri ekliyoruz
	INSERT        INTO              WebMenuRol(MenuID, RoleName)
	SELECT ID, @RoleName as RoleName FROM WebMenu where ID IN (Select * from dbo.splitstring(@MenuIDs))


END


GO
/****** Object:  StoredProcedure [dbo].[UpdateUserDevice]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.06.2017
-- Last Modify: 14.06.2017
-- Description:	kullanıcı device tablosuna kayıt atar
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUserDevice]
	@UserID int,
	@Device varchar(250)

AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT * FROM usr.UserDevices WHERE (UserID = @UserID) AND (Device = @Device))
		INSERT INTO usr.UserDevices (UserID, Device, FirsLoginDate, LastLoginDate)
		VALUES						(@UserID, @Device, GETDATE(), GETDATE())
	ELSE
		UPDATE usr.UserDevices SET LastLoginDate = GETDATE(), LoginCount = LoginCount + 1 WHERE (UserID = @UserID) AND (Device = @Device)

END

GO
/****** Object:  StoredProcedure [ong].[GetCalismaSure]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 02.01.2018
-- Modify date: 02.01.2018
-- Description:	developerın toplam çalışma süresini getirir
-- =============================================
CREATE PROCEDURE [ong].[GetCalismaSure]
	@Username varchar(5),
	@Tarih Date

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        ISNULL(SUM(Sure), 0) AS Expr1
	FROM            (

		SELECT        ISNULL(SUM(Sure), 0) AS Sure
		FROM            ong.GorevlerCalisma
		WHERE        (Kaydeden = @Username) AND (Tarih = @Tarih)

		UNION ALL

		SELECT        ISNULL(SUM(ISNULL(Sure, 1000)), 0) AS Sure
		FROM            Etkinlik
		WHERE        (Tarih = @Tarih) AND (TatilTipi <> 78) AND (Username = @Username OR ISNULL(Username, '') = '')

	) AS A

END

GO
/****** Object:  StoredProcedure [ong].[GetProjeFormFromMusteri]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 11.01.2018
-- Modify date: 11.01.2018
-- Description: müşteriye ait formu getirir
-- =============================================
CREATE PROCEDURE [ong].[GetProjeFormFromMusteri]
	@MusteriID int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        CONVERT(varchar(10), ong.ProjeForm.ID) as [Value], ong.ProjeForm.Proje as [Text]
    FROM            ong.ProjeForm INNER JOIN
                                ong.ProjeForm AS ProjeForm_1 ON ong.ProjeForm.ID = ProjeForm_1.PID
    WHERE        (ong.ProjeForm.MusteriID = @MusteriID) AND (ong.ProjeForm.PID IS NULL) AND (ong.ProjeForm.Aktif = 1)
    GROUP BY ong.ProjeForm.ID, ong.ProjeForm.Proje
    ORDER BY ong.ProjeForm.Proje

END

GO
/****** Object:  StoredProcedure [wms].[DeleteIrsaliye]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 28.08.2017
-- Last Modify: 28.08.2017
-- Description:	delete irsaliye
-- =============================================
CREATE PROCEDURE [wms].[DeleteIrsaliye]
	@IrsaliyeID int
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM wms.IRS_Detay WHERE (IrsaliyeID = @IrsaliyeID)
	DELETE FROM wms.GorevIRS WHERE (IrsaliyeID = @IrsaliyeID)
	DELETE FROM wms.Gorev WHERE (IrsaliyeID = @IrsaliyeID)
	DELETE FROM wms.IRS WHERE (ID = @IrsaliyeID)

END

GO
/****** Object:  StoredProcedure [wms].[DeleteTransfer]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 28.08.2017
-- Last Modify: 06.09.2017
-- Description:	delete transfer
-- =============================================
CREATE PROCEDURE [wms].[DeleteTransfer]
	@GorevID int

AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM wms.IRS_Detay WHERE (IrsaliyeID IN (SELECT IrsaliyeID FROM wms.GorevIRS WHERE (GorevID = @GorevID)))
	DELETE FROM wms.GorevIRS WHERE (GorevID = @GorevID)
	DELETE FROM wms.Transfer_Detay WHERE (TransferID IN (SELECT ID FROM wms.[Transfer] WHERE (GorevID = @GorevID)))
	DELETE FROM wms.[Transfer] WHERE (GorevID = @GorevID)
	DELETE FROM wms.GorevYer WHERE (GorevID = @GorevID)
	DELETE FROM wms.GorevUsers WHERE (GorevID = @GorevID)
	DELETE FROM wms.Gorev WHERE (ID = @GorevID)
	DELETE FROM wms.IRS WHERE (ID IN (SELECT IrsaliyeID FROM wms.Gorev WHERE (ID = @GorevID)))

END

GO
/****** Object:  StoredProcedure [wms].[GetBolumSiralamaFromGorevId]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.03.2017
-- Last Modify: 14.03.2017
-- Description:	görev id ve koridor id'den yerleştirme veya toplama sirasinı getirir
-- =============================================
CREATE PROCEDURE [wms].[GetBolumSiralamaFromGorevId]
	@GorevID int,
	@KoridorID int,
	@desc bit

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        wms.GorevYer.ID
	FROM            wms.GorevYer INNER JOIN
							 wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID INNER JOIN
							 wms.Kat ON wms.Yer.KatID = wms.Kat.ID INNER JOIN
							 wms.Bolum ON wms.Kat.BolumID = wms.Bolum.ID INNER JOIN
							 wms.Raf ON wms.Bolum.RafID = wms.Raf.ID
	WHERE        (wms.GorevYer.GorevID = @GorevID) AND (wms.Raf.KoridorID = @KoridorID)
	GROUP BY wms.Bolum.BolumAd, wms.Kat.KatAd, wms.GorevYer.ID
	ORDER BY 
			  CASE WHEN @desc=0 THEN wms.Bolum.BolumAd ELSE '' END ASC,
			  CASE WHEN @desc=1 THEN wms.Bolum.BolumAd ELSE '' END DESC, wms.Kat.KatAd

END




GO
/****** Object:  StoredProcedure [wms].[GetGorevlis]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 02.05.2017
-- Modify date: 02.05.2017
-- Description:	bir depodaki tüm gorevli kişileri getirir
-- =============================================
CREATE PROCEDURE [wms].[GetGorevlis]
	@DepoID int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        usr.Users.ID, usr.Users.Kod
	FROM            wms.Gorev INNER JOIN
							 usr.Users ON wms.Gorev.Gorevli = usr.Users.Kod
	WHERE        (wms.Gorev.DepoID = @DepoID)
	GROUP BY usr.Users.ID, usr.Users.Kod

END

GO
/****** Object:  StoredProcedure [wms].[GetHucreAd]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 08.03.2017
-- Last Modify: 16.06.2017
-- Description:	hücre adlarını getirir
-- =============================================
CREATE PROCEDURE [wms].[GetHucreAd]
	@DepoID int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        wms.Kat.ID, wms.Koridor.KoridorAd + '-' + wms.Raf.RafAd + wms.Bolum.BolumAd + '-' + wms.Kat.KatAd AS Ad
	FROM            wms.Depo INNER JOIN
							 wms.Koridor ON wms.Depo.ID = wms.Koridor.DepoID INNER JOIN
							 wms.Raf ON wms.Koridor.ID = wms.Raf.KoridorID INNER JOIN
							 wms.Bolum ON wms.Raf.ID = wms.Bolum.RafID INNER JOIN
							 wms.Kat ON wms.Bolum.ID = wms.Kat.BolumID
	WHERE        (wms.Depo.ID = @DepoID)

END



GO
/****** Object:  StoredProcedure [wms].[GetHucreKatID]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 08.03.2017
-- Last Modify: 16.06.2017
-- Description:	hücre kodundan kat id bulur
-- =============================================
CREATE PROCEDURE [wms].[GetHucreKatID]
	@DepoID int,
	@Kod varchar(50)

AS
BEGIN
	SET NOCOUNT ON;

	--declares
	DECLARE @name NVARCHAR(255), @pos INT, @tmp int
	DECLARE @Koridor varchar(50), @Raf varchar(50), @Bolum varchar(50), @Kat varchar(50)
	set @tmp=0
	--loop
	WHILE CHARINDEX('-', @Kod) > 0
	BEGIN
		SELECT @pos  = CHARINDEX('-', @Kod)  
		SELECT @name = SUBSTRING(@Kod, 1, @pos-1)
		--set names
		if(@tmp=0)
			set @Koridor=@name
		else if(@tmp=1)
			set @Raf=@name
		else 
			set @Kat=@name
		set @tmp=@tmp+1
		--remove
		SELECT @Kod = SUBSTRING(@Kod, @pos+1, LEN(@Kod)-@pos)
	END
	set @Kat=@Kod
	set @Bolum=SUBSTRING(@Raf, 2, 2)
	set @Raf=SUBSTRING(@Raf, 1, 1)

	--select @DepoID, @Koridor, @Raf, @Bolum, @Kat
	--find id
	SELECT		TOP (1) wms.Kat.ID
	FROM		wms.Koridor INNER JOIN
							 wms.Raf ON wms.Koridor.ID = wms.Raf.KoridorID INNER JOIN
							 wms.Bolum ON wms.Raf.ID = wms.Bolum.RafID INNER JOIN
							 wms.Kat ON wms.Bolum.ID = wms.Kat.BolumID
	WHERE		(wms.Koridor.DepoID = @DepoID) AND (wms.Koridor.KoridorAd = @Koridor) AND (wms.Raf.RafAd = @Raf) AND (wms.Bolum.BolumAd = @Bolum) AND (wms.Kat.KatAd = @Kat)

END


GO
/****** Object:  StoredProcedure [wms].[GetKoridorIdFromGorevId]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.03.2017
-- Last Modify: 14.03.2017
-- Description:	görev id'den yerleştirme veya toplama sirasi için koridoru bulur
-- =============================================
CREATE PROCEDURE [wms].[GetKoridorIdFromGorevId]
	@GorevID int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        wms.Koridor.ID
	FROM            wms.GorevYer INNER JOIN
							 wms.Yer ON wms.GorevYer.YerID = wms.Yer.ID INNER JOIN
							 wms.Kat ON wms.Yer.KatID = wms.Kat.ID INNER JOIN
							 wms.Bolum ON wms.Kat.BolumID = wms.Bolum.ID INNER JOIN
							 wms.Raf ON wms.Bolum.RafID = wms.Raf.ID INNER JOIN
							 wms.Koridor ON wms.Raf.KoridorID = wms.Koridor.ID
	WHERE        (wms.GorevYer.GorevID = @GorevID)
	GROUP BY wms.Koridor.KoridorAd, wms.Koridor.ID
	ORDER BY wms.Koridor.KoridorAd

END




GO
/****** Object:  StoredProcedure [wms].[GetSayimFarki]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 16.01.2018
-- Last Modify: 30.03.2018
-- Description:	wms/tasks/CountCreateDiff
-- =============================================
CREATE PROCEDURE [wms].[GetSayimFarki]
	@GorevID int,
	@HepsiMi bit

AS
BEGIN
	SET NOCOUNT ON;
	
	IF (@HepsiMi = 1)
		SELECT        wms.Yer.KatID, wms.Yer.MalKodu, wms.Yer.Birim, wms.Yer.MakaraNo, wms.Yer.Miktar AS Stok, ISNULL
									 ((SELECT        SUM(Miktar) AS Expr1
										 FROM            wms.GorevYer
										 WHERE        (GorevID = @GorevID) AND (MalKodu = wms.Yer.MalKodu) AND (YerID = wms.Yer.ID)), 0) AS Miktar
		FROM            wms.Yer INNER JOIN
								 wms.Gorev ON wms.Yer.DepoID = wms.Gorev.DepoID
		WHERE        (wms.Gorev.ID = @GorevID)
	ELSE
		SELECT        wms.Yer.KatID, wms.Yer.MalKodu, wms.Yer.Birim, wms.Yer.MakaraNo, wms.Yer.Miktar AS Stok, ISNULL
									 ((SELECT        SUM(Miktar) AS Expr1
										 FROM            wms.GorevYer
										 WHERE        (GorevID = @GorevID) AND (MalKodu = wms.Yer.MalKodu) AND (YerID = wms.Yer.ID)), 0) AS Miktar
		FROM            wms.Yer INNER JOIN
								 wms.GorevYer AS GorevYer_1 ON wms.Yer.MalKodu = GorevYer_1.MalKodu INNER JOIN
								 wms.Gorev ON wms.Yer.DepoID = wms.Gorev.DepoID
		WHERE        (GorevYer_1.GorevID = @GorevID)
		GROUP BY wms.Yer.KatID, wms.Yer.MalKodu, wms.Yer.Birim, wms.Yer.MakaraNo, wms.Yer.Miktar, wms.Yer.ID

END
GO
/****** Object:  StoredProcedure [wms].[GetSTIList]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 02.10.2017
-- Last Modify: 07.12.2017
-- Description:	terminal liste
-- =============================================
CREATE PROCEDURE [wms].[GetSTIList]
	@devamMi bit ,
	@gorevTip varchar(50),
	@gorevID int,
	@transferCount int
AS
BEGIN
	DECLARE @Komut VARCHAR(MAX)
	DECLARE @KomutCase VARCHAR(MAX)
	DECLARE @KomutCase2 VARCHAR(MAX)
	SET @KomutCase=''
	SET @KomutCase2=''
	IF (@gorevTip = 'Sipariş Topla' OR @gorevTip = 'Transfer Çıkış' OR @gorevTip = 'Kontrollü Sayım'OR @gorevTip = 'Transfer Çıkış'OR @gorevTip = 'Alımdan İade')
	BEGIN
		IF (@devamMi=1)
		BEGIN
		SET @KomutCase = 'AND (wms.GorevYer.Miktar<>ISNULL(wms.GorevYer.YerlestirmeMiktari,0) OR wms.GorevYer.Miktar = 0)'
		END
		SET @Komut = 'SELECT wms.GorevYer.ID, wms.GorevYer.MalKodu, wms.GorevYer.Miktar, wms.GorevYer.Birim, wms.GorevYer.MakaraNo, wms.Yer.HucreAd AS Raf, ISNULL(wms.GorevYer.YerlestirmeMiktari,0) as YerlestirmeMiktari, ISNULL(BIRIKIM.wms.fnGetMalAdi(wms.GorevYer.MalKodu),'''') AS MalAdi, ISNULL(BIRIKIM.wms.fnGetBarkods(wms.GorevYer.MalKodu),'''') AS Barkod 
		FROM wms.GorevYer WITH(nolock) 
		INNER JOIN wms.Yer WITH(nolock) ON wms.GorevYer.YerID = wms.Yer.ID 
		WHERE (wms.GorevYer.GorevID = ' +  CONVERT(VARCHAR(10),@gorevID)  +')'

		SET @Komut = @Komut + ' ' + @KomutCase + ' ORDER BY wms.GorevYer.Sira'

	END
	ELSE IF(@gorevTip = 'Transfer Giriş' AND @transferCount=0)
	BEGIN	
			IF (@devamMi=1)
			BEGIN
			SET @KomutCase = ' HAVING (wms.IRS_Detay.Miktar <> ISNULL(wms.IRS_Detay.YerlestirmeMiktari,0))'
			END
			SET @Komut = 'SELECT wms.IRS_Detay.ID, wms.IRS.ID as irsID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, wms.IRS_Detay.MakaraNo, ISNULL(wms.IRS_Detay.OkutulanMiktar, 0) AS OkutulanMiktar, 
			ISNULL(wms.IRS_Detay.YerlestirmeMiktari, 0) AS YerMiktar,
			ISNULL(BIRIKIM.wms.fnGetHucreAdFromKatID(wms.Yer.KatID),'''') AS Raf,
			-- wms.Yer_Log.HucreAd AS Raf,
			 --SUM(wms.Yer_Log.Miktar) AS YerMiktar, 
			ISNULL(BIRIKIM.wms.fnGetMalAdi(wms.IRS_Detay.MalKodu),'''') AS MalAdi,
			ISNULL(BIRIKIM.wms.fnGetBarkods(wms.IRS_Detay.MalKodu),'''') AS Barkod 
			FROM wms.IRS_Detay WITH (nolock) INNER JOIN wms.IRS WITH (nolock) ON wms.IRS_Detay.IrsaliyeID = wms.IRS.ID 
			INNER JOIN wms.GorevIRS WITH (nolock) ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID 
			INNER JOIN wms.GorevYer WITH (nolock) ON wms.GorevIRS.GorevID = wms.Gorevyer.GorevID 
			INNER JOIN wms.Yer WITH (nolock) ON wms.Gorevyer.YerID = wms.Yer.ID 
			LEFT OUTER JOIN wms.Yer_Log WITH (nolock) ON wms.IRS_Detay.KynkSiparisID = wms.Yer_Log.KatID AND wms.IRS_Detay.MalKodu = wms.Yer_Log.MalKodu
			WHERE (wms.GorevIRS.GorevID =  ' +  CONVERT(VARCHAR(10),@gorevID)  +') AND (wms.Yer_Log.GC = 0 OR wms.Yer_Log.GC IS NULL)
			GROUP BY wms.IRS_Detay.ID, wms.IRS.ID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, wms.IRS_Detay.MakaraNo, 
			ISNULL(wms.IRS_Detay.OkutulanMiktar, 0), ISNULL(wms.IRS_Detay.YerlestirmeMiktari, 0), wms.Yer.KatID'

			SET @Komut = @Komut + ' ' + @KomutCase
	END 
	ELSE 
	BEGIN
			IF (@devamMi=1)
			BEGIN
				IF(@gorevTip = 'Paketle ' OR @gorevTip = 'Sevkiyat')
					SET @KomutCase = ' HAVING (wms.IRS_Detay.Miktar <> ISNULL(OkutulanMiktar,0))'
				ELSE IF(@gorevTip = 'Mal Kabul')
					SET @KomutCase2 = ' ((SELECT SUM(IRS_Detay_2.Miktar - ISNULL(IRS_Detay_2.OkutulanMiktar, 0)) AS Expr1
										FROM wms.IRS_Detay AS IRS_Detay_2 INNER JOIN wms.GorevIRS AS GorevIRS_2 ON IRS_Detay_2.IrsaliyeID = GorevIRS_2.IrsaliyeID
										WHERE (GorevIRS_2.GorevID = GorevIRS.GorevID) AND (IRS_Detay_2.MalKodu = IRS_Detay.MalKodu)) <> 0) AND'
				ELSE
					SET @KomutCase = ' HAVING (wms.IRS_Detay.Miktar <> ISNULL(YerlestirmeMiktari,0))'
			END

			IF(@gorevTip = 'Paketle ' OR @gorevTip = 'Sevkiyat' OR @gorevTip = 'Barkod Hazırla')
				SET @KomutCase2 = ' (wms.Yer_Log.GC = 1) '
			ELSE IF(@gorevTip = 'Mal Kabul')
				SET @KomutCase2 = @KomutCase2 + ' (wms.Yer_Log.GC = 0 OR wms.Yer_Log.GC IS NULL) '
			ELSE
				SET @KomutCase2 = ' (wms.Yer_Log.GC = 0 OR wms.Yer_Log.GC IS NULL) '

			SET @Komut = 'SELECT wms.IRS_Detay.ID, wms.IRS.ID as irsID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, wms.IRS_Detay.MakaraNo, ISNULL(wms.IRS_Detay.OkutulanMiktar, 0) AS OkutulanMiktar, wms.Yer_Log.HucreAd AS Raf,
								SUM(wms.Yer_Log.Miktar) AS YerMiktar,
								ISNULL(BIRIKIM.wms.fnGetMalAdi(wms.IRS_Detay.MalKodu),'''') AS MalAdi, 
								ISNULL(BIRIKIM.wms.fnGetBarkods(wms.IRS_Detay.MalKodu),'''') AS Barkod
						FROM wms.IRS_Detay WITH (nolock) INNER JOIN wms.IRS WITH (nolock) ON wms.IRS_Detay.IrsaliyeID = wms.IRS.ID INNER JOIN wms.GorevIRS WITH (nolock) ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID LEFT OUTER JOIN wms.Yer_Log WITH (nolock) ON wms.IRS_Detay.IrsaliyeID = wms.Yer_Log.IrsaliyeID AND wms.IRS_Detay.MalKodu = wms.Yer_Log.MalKodu AND  (wms.IRS_Detay.ID = wms.Yer_Log.IRSDetayID OR  ISNULL(wms.Yer_Log.IRSDetayID,'''')='''')
						WHERE (wms.GorevIRS.GorevID =  ' + CONVERT(VARCHAR(10),@gorevID) +') AND ' + 
								@KomutCase2 + 
						' GROUP BY wms.IRS_Detay.ID, wms.IRS.ID, wms.IRS_Detay.MalKodu, wms.IRS_Detay.Miktar, wms.IRS_Detay.Birim, wms.IRS_Detay.MakaraNo, ISNULL(wms.IRS_Detay.OkutulanMiktar, 0), ISNULL(wms.IRS_Detay.YerlestirmeMiktari, 0), wms.Yer_Log.HucreAd ' + @KomutCase
			
	END
	EXECUTE(@Komut)
END
GO
/****** Object:  StoredProcedure [wms].[GetStock]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 03.04.2017
-- Modify date: 03.04.2017
-- Description:	bir mal için geçerli stoğu getirir
-- =============================================
CREATE PROCEDURE [wms].[GetStock]
	@DepoID int,
	@MalKodu varchar(30),
	@Birim varchar(4),
	@includeRezerv bit

AS
BEGIN
	SET NOCOUNT ON;

	--rezerv sayılsın mı
	declare @Rezerv int
	if (@includeRezerv=1)
		set @Rezerv=(SELECT SUM(wms.IRS_Detay.Miktar) AS Expr1
					FROM wms.IRS INNER JOIN wms.IRS_Detay ON wms.IRS.ID = wms.IRS_Detay.IrsaliyeID INNER JOIN wms.GorevIRS ON wms.IRS.ID = wms.GorevIRS.IrsaliyeID INNER JOIN wms.Gorev ON wms.GorevIRS.GorevID = wms.Gorev.ID
					WHERE (wms.IRS.DepoID = @DepoID) AND (wms.IRS_Detay.MalKodu = @MalKodu) AND (wms.IRS_Detay.Birim = @Birim) AND (wms.Gorev.GorevTipiID = 3) AND (wms.Gorev.DurumID = 9))
	--toplam stoğu getir
	SELECT        (SUM(wms.Yer.Miktar) - ISNULL(@Rezerv,0)) AS Stok
	FROM            wms.Yer INNER JOIN
							 wms.Kat ON wms.Yer.KatID = wms.Kat.ID AND wms.Yer.KatID = wms.Kat.ID INNER JOIN
							 wms.Bolum ON wms.Kat.BolumID = wms.Bolum.ID AND wms.Kat.BolumID = wms.Bolum.ID INNER JOIN
							 wms.Raf ON wms.Bolum.RafID = wms.Raf.ID AND wms.Bolum.RafID = wms.Raf.ID INNER JOIN
							 wms.Koridor ON wms.Raf.KoridorID = wms.Koridor.ID AND wms.Raf.KoridorID = wms.Koridor.ID
	WHERE        (wms.Koridor.DepoID = @DepoID) AND (wms.Yer.MalKodu = @MalKodu) AND (wms.Yer.Birim = @Birim)

END



GO
/****** Object:  StoredProcedure [wms].[GetStockSearchByCode]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 30.03.2018
-- Modify date: 05.01.2018
-- Description:	WMS/Stock sayfasında malzeme bazlı arama ilk sorgu
-- =============================================
CREATE PROCEDURE [wms].[GetStockSearchByCode]
	@MalKodu varchar(30)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        wms.fnGetRezervStock(wms.Depo.DepoKodu, wms.Yer.MalKodu, '') AS WmsRezerv, wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, SUM(wms.Yer.Miktar) AS Miktar, wms.Yer.Birim
	FROM            wms.Yer INNER JOIN
							 wms.Depo ON wms.Yer.DepoID = wms.Depo.ID
	WHERE        (wms.Yer.MalKodu = @MalKodu)
	GROUP BY wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, wms.fnGetRezervStock(wms.Depo.DepoKodu, wms.Yer.MalKodu, ''), wms.Yer.Birim

END
GO
/****** Object:  StoredProcedure [wms].[GetTaskList]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Last Modify: 25.12.2017
-- Description:	wms/görev/liste
-- =============================================
CREATE PROCEDURE [wms].[GetTaskList]
	@DurumID int,
	@DepoID int,
	@Tarih int

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        wms.Gorev.ID, wms.Gorev.DurumID, wms.Gorev.GorevNo, wms.Gorev.Gorevli, wms.Depo.DepoAd, wms.fnGetKynkEvrakNosForGorev(wms.Gorev.ID) AS EvrakNo, ComboItem_Name.Name AS GorevTipi, 
							 wms.fnGetKynkTarihsForGorev(wms.Gorev.ID) AS SiparisTarihi, dbo.fnFormatDateFromInt(wms.Gorev.OlusturmaTarihi) AS OlusturmaTarihi, dbo.fnFormatDateFromInt(wms.Gorev.BitisTarihi) AS BitisTarihi, wms.Gorev.Bilgi
	FROM            wms.Gorev INNER JOIN
							 wms.Depo ON wms.Gorev.DepoID = wms.Depo.ID INNER JOIN
							 ComboItem_Name ON wms.Gorev.GorevTipiID = ComboItem_Name.ID AND wms.Gorev.GorevTipiID = ComboItem_Name.ID
	WHERE        (wms.Gorev.DurumID = @DurumID) AND 
						(CASE WHEN @DepoID = 0 THEN 1 ELSE CASE WHEN wms.Gorev.DepoID = @DepoID THEN 1 ELSE 0 END END = 1) AND 
						(CASE WHEN @Tarih = 0 THEN 1 ELSE CASE WHEN wms.Gorev.OlusturmaTarihi >= @Tarih THEN 1 ELSE 0 END END = 1)

END
GO
/****** Object:  StoredProcedure [wms].[InsertIadeIrsaliye]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 10.10.2017
-- Last Modify: 17.01.2018
-- Description:	İade irsaliyesi ve görevi oluşturur
-- =============================================
CREATE PROCEDURE [wms].[InsertIadeIrsaliye]
	@SirketKod varchar(2),
	@DepoID int,
	@GorevNo varchar(50),
	@IrsEvrakNo varchar(16),
	@IrsTarih int,
	@GorevBilgi nvarchar(max),
	@IrsIslemTur bit,
	@GorevTipiID int,
	@Olusturan varchar(5),
	@OlusturmaTarihi int,
	@OlusturmaSaati int,
	@HesapKodu varchar(20),
	@TeslimCHK varchar(20)='',
	@ValorGun smallint=0,
	@LinkEvrakNo varchar(16)	

AS
BEGIN
	SET NOCOUNT ON;
	
	declare @GorevID int
	declare @IrsaliyeID int
	declare @Onay bit
	--check if irs exists
	BEGIN
		--irsaliye ekle
		INSERT INTO wms.IRS (SirketKod, DepoID, IslemTur, EvrakNo, HesapKodu, TeslimCHK, ValorGun, Tarih)
		VALUES        (@SirketKod, @DepoID, @IrsIslemTur, @IrsEvrakNo, @HesapKodu, case when @TeslimCHK='' then NULL else @TeslimCHK end, case when @ValorGun=0 then NULL else @ValorGun end, @IrsTarih)
		set @IrsaliyeID = SCOPE_IDENTITY()
		Set @Onay = 0
		--check if görev exists
		BEGIN
			--görev ekle ama durumu başlamamış olsun, listelerde gözükmesin
			INSERT INTO wms.Gorev( DepoID, GorevNo, GorevTipiID, DurumID, Olusturan, OlusturmaTarihi, OlusturmaSaati, Bilgi, IrsaliyeID)
			VALUES				(@DepoID, @GorevNo, @GorevTipiID, 15, @Olusturan, @OlusturmaTarihi, @OlusturmaSaati, @GorevBilgi, @IrsaliyeID)
			set @GorevID = SCOPE_IDENTITY()
		END
		--gorevirs tablosu
		INSERT INTO wms.GorevIRS (GorevID, IrsaliyeID) VALUES (@GorevID, @IrsaliyeID)
	END
	
	if(@LinkEvrakNo<>'') UPDATE wms.IRS SET LinkEvrakNo=@LinkEvrakNo WHERE ID=@IrsaliyeID
	--return
	select @IrsaliyeID as IrsaliyeID, @GorevID as GorevID, @Onay as Onay

END


GO
/****** Object:  StoredProcedure [wms].[InsertIrsaliye]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 10.03.2017
-- Last Modify: 03.11.2017
-- Description:	irsaliyesi ve görevi oluşturur
-- =============================================
CREATE PROCEDURE [wms].[InsertIrsaliye]
	@SirketKod varchar(2),
	@DepoID int,
	@GorevNo varchar(50),
	@IrsEvrakNo varchar(16),
	@IrsTarih int,
	@GorevBilgi nvarchar(max),
	@IrsIslemTur bit,
	@GorevTipiID int,
	@Olusturan varchar(5),
	@OlusturmaTarihi int,
	@OlusturmaSaati int,
	@HesapKodu varchar(20),
	@TeslimCHK varchar(20)='',
	@ValorGun smallint=0,
	@LinkEvrakNo varchar(16),
	@Aciklama varchar(120)=''

AS
BEGIN
	SET NOCOUNT ON;
	
	declare @GorevID int
	declare @IrsaliyeID int
	declare @Onay bit
	--check if irs exists
	IF NOT EXISTS (SELECT ID FROM wms.IRS WHERE (EvrakNo = @IrsEvrakNo) AND (IslemTur = @IrsIslemTur) AND (SirketKod = @SirketKod) AND (HesapKodu = @HesapKodu) AND (ISNULL(ValorGun, 0) = @ValorGun) AND (ISNULL(TeslimCHK,'') = @TeslimCHK) AND (ISNULL(Aciklama, '') = @Aciklama))
	BEGIN
		--irsaliye ekle
		INSERT INTO wms.IRS (SirketKod, DepoID, IslemTur, EvrakNo, HesapKodu, TeslimCHK, ValorGun, Tarih,Aciklama)
		VALUES        (@SirketKod, @DepoID, @IrsIslemTur, @IrsEvrakNo, @HesapKodu, case when @TeslimCHK='' then NULL else @TeslimCHK end, case when @ValorGun=0 then NULL else @ValorGun end, @IrsTarih,case when @Aciklama='' then NULL else @Aciklama end)
		set @IrsaliyeID = SCOPE_IDENTITY()
		Set @Onay = 0
		--check if görev exists
		IF NOT EXISTS (SELECT ID FROM wms.Gorev WHERE (GorevNo = @GorevNo) AND (DepoID = @DepoID))
		BEGIN
			--görev ekle ama durumu başlamamış olsun, listelerde gözükmesin
			INSERT INTO wms.Gorev( DepoID, GorevNo, GorevTipiID, DurumID, Olusturan, OlusturmaTarihi, OlusturmaSaati, Bilgi, IrsaliyeID)
			VALUES				(@DepoID, @GorevNo, @GorevTipiID, 15, @Olusturan, @OlusturmaTarihi, @OlusturmaSaati, @GorevBilgi, @IrsaliyeID)
			set @GorevID = SCOPE_IDENTITY()
		END
		ELSE
			SELECT @GorevID = ID FROM wms.Gorev WHERE (GorevNo = @GorevNo)
		--gorevirs tablosu
		INSERT INTO wms.GorevIRS (GorevID, IrsaliyeID) VALUES (@GorevID, @IrsaliyeID)
	END
	ELSE
	BEGIN
		--get irs no
		SELECT @IrsaliyeID = ID, @Onay= Onay FROM wms.IRS WHERE (EvrakNo = @IrsEvrakNo)
		--check if görev exists
		IF NOT EXISTS (SELECT ID FROM wms.Gorev WHERE (GorevNo = @GorevNo) AND (DepoID = @DepoID))
		BEGIN
			--görev ekle
			INSERT INTO wms.Gorev( DepoID, GorevNo, GorevTipiID, DurumID, Olusturan, OlusturmaTarihi, OlusturmaSaati, Bilgi, IrsaliyeID)
			VALUES				(@DepoID, @GorevNo, @GorevTipiID, 9, @Olusturan, @OlusturmaTarihi, @OlusturmaSaati, @GorevBilgi, @IrsaliyeID)
			set @GorevID = SCOPE_IDENTITY()
		END
		ELSE
			SELECT @GorevID = ID FROM wms.Gorev WHERE (GorevNo = @GorevNo)
		--gorevirs tablosu
		IF NOT EXISTS (SELECT * FROM wms.GorevIRS WHERE (GorevID = @GorevID) AND (IrsaliyeID = @IrsaliyeID))
			INSERT INTO wms.GorevIRS (GorevID, IrsaliyeID) VALUES (@GorevID, @IrsaliyeID)
	END
	if(@LinkEvrakNo<>'') UPDATE wms.IRS SET LinkEvrakNo=@LinkEvrakNo WHERE ID=@IrsaliyeID
	--return
	select @IrsaliyeID as IrsaliyeID, @GorevID as GorevID, @Onay as Onay

END


GO
/****** Object:  StoredProcedure [wms].[MakaraNoKontrol]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Devis Akdeniz
-- Create date: 31.10.2017
-- Modify date: 31.10.2017
-- Description:	MakaraNo'nun kullanılıp kullanılmadığını kontrol eder.
-- =============================================
CREATE PROCEDURE [wms].[MakaraNoKontrol]
	@DepoID int,
	@MakaraNo varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT        MakaraNo
	FROM            wms.Yer
	WHERE        (DepoID = @DepoID) AND (MakaraNo = @MakaraNo)
	UNION
	SELECT        wms.IRS_Detay.MakaraNo
	FROM            wms.GorevIRS INNER JOIN
							 wms.Gorev ON wms.GorevIRS.GorevID = wms.Gorev.ID INNER JOIN
							 wms.IRS_Detay ON wms.GorevIRS.IrsaliyeID = wms.IRS_Detay.IrsaliyeID
	WHERE        (wms.Gorev.DepoID = @DepoID) AND (wms.IRS_Detay.MakaraNo = @MakaraNo) AND (wms.Gorev.GorevTipiID = 1) AND (wms.Gorev.DurumID <> 10)

END
GO
/****** Object:  StoredProcedure [wms].[SettingsGorevNo]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 01.03.2017
-- Last Modify: 08.05.2017
-- Description:	bugünkü görev listesi için görev noyu getirir
-- =============================================
CREATE PROCEDURE [wms].[SettingsGorevNo]
	@tarih int,
	@DepoID int

AS
BEGIN
	SET NOCOUNT ON;

	--declares
	declare @result varchar(50)
	declare @gs int
	--get current gorev no
	if exists(select [No] from wms.GorevNo where Tarih = @tarih and DepoID = @DepoID)
	begin
		update wms.GorevNo set [No]=[No]+1 where Tarih=@tarih and DepoID = @DepoID
		set @gs=(select [No] from wms.GorevNo where Tarih=@tarih and DepoID = @DepoID)
	end
	else
	begin
		insert wms.GorevNo (Tarih, DepoID, [No]) VALUES (@tarih, @DepoID, 1)
		set @gs=1
	end
	--result
	set @result=replace(CONVERT(VARCHAR(10),GETDATE(),2),'.','')
	set @result=@result+'-'+CONVERT(VARCHAR(10),@gs)
	select @result GorevNo

END
GO
/****** Object:  StoredProcedure [wms].[SettingsMakaraNo]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 01.08.2017
-- Last Modify: 01.08.2017
-- Description:	depoya ait en son makara noyu getirir, bir tane arttırır
-- =============================================
CREATE PROCEDURE [wms].[SettingsMakaraNo]
	@DepoID int

AS
BEGIN
	SET NOCOUNT ON;

	--if doesn't exists add row
	if not exists(select DepoID from wms.GorevPaketNo where DepoID = @DepoID)
		insert wms.GorevPaketNo (DepoID, SevkiyatNo, PaketNo, MakaraNo) VALUES (@DepoID, 1, 1, 1)
	--declares
	declare @MakaraNo int
	select @MakaraNo=MakaraNo from wms.GorevPaketNo where DepoID = @DepoID
	--update
	update wms.GorevPaketNo set MakaraNo=MakaraNo+1 where DepoID = @DepoID
	--result
	select @MakaraNo MakaraNo

END
GO
/****** Object:  StoredProcedure [wms].[SettingsPaketNo]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 30.06.2017
-- Last Modify: 01.08.2017
-- Description:	depo bazlı paket listesi oluşturur
-- =============================================
CREATE PROCEDURE [wms].[SettingsPaketNo]
	@DepoID int,
	@GorevID int,
	@Username varchar(5),
	@Tarih int

AS
BEGIN
	SET NOCOUNT ON;

	--declares
	declare @SevkiyatNo int, @PaketNo int, @SevkiyatNo2 varchar(20), @PaketNo2 varchar(20), @trh varchar(10)
	set @trh = replace(CONVERT(varchar(10), GetDate(),126),'-','')
	--if doesn't exists add row
	if not exists(select DepoID from wms.GorevPaketNo where DepoID = @DepoID)
		insert wms.GorevPaketNo (DepoID, SevkiyatNo, PaketNo, MakaraNo) VALUES (@DepoID, 1, 1, 1)
	--sets
	select @SevkiyatNo=SevkiyatNo, @PaketNo=PaketNo from wms.GorevPaketNo where DepoID = @DepoID
	set @SevkiyatNo2=@trh+'-'+RIGHT('0000'+CONVERT(varchar(5),@DepoID),2)+'-'+RIGHT('0000'+CONVERT(varchar(5),@SevkiyatNo),5)
	set @PaketNo2=@trh+'-'+RIGHT('0000'+CONVERT(varchar(5),@DepoID),2)+'-'+RIGHT('0000'+CONVERT(varchar(5),@PaketNo),5)
	--add gorevpaketler
	INSERT INTO wms.GorevPaketler (GorevID, SevkiyatNo, PaketNo, Adet, PaketTipiID, Kaydeden, KayitTarih, Degistiren, DegisTarih)
	VALUES							(@GorevID, @SevkiyatNo2, @PaketNo2, 1, 1, @Username, @Tarih, @Username, @Tarih)
	--update paket nos
	update wms.GorevPaketNo set PaketNo=PaketNo+1, SevkiyatNo=SevkiyatNo+1 where DepoID = @DepoID

END
GO
/****** Object:  StoredProcedure [wms].[TerminalFinishGorev]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 29.03.2017
-- Last Modify: 30.06.2017
-- Description:	terminaldeki görevi bitirip yeni görev açar
-- =============================================
CREATE PROCEDURE [wms].[TerminalFinishGorev]
	@GorevID int,
	@IrsaliyeID int,
	@YeniGorevNo varchar(50),
	@BitisTarihi int,
	@BitisSaati int,
	@Kullanici varchar(5),
	@LinkEvrakNo varchar(16),
	@GörevTipiID int,
	@YeniGorevTipiID int

AS
BEGIN
	SET NOCOUNT ON;

	--update görev table
	if(@GörevTipiID > 1)
	BEGIN
		UPDATE wms.Gorev SET DurumID=11, BitisTarihi=@BitisTarihi, BitisSaati=@BitisSaati WHERE ID=@GorevID	
		--paketleme bitince de sıfırla
		if(@GörevTipiID=6)
			UPDATE wms.IRS_Detay SET OkutulanMiktar=NULL WHERE (IrsaliyeID = @IrsaliyeID)
	END
	else--mal kabul ise okutulan miktarları miktar yap
	BEGIN
		UPDATE wms.IRS_Detay SET Miktar = ISNULL(OkutulanMiktar,0) WHERE (IrsaliyeID = @IrsaliyeID)
		DELETE FROM wms.IRS_Detay WHERE (IrsaliyeID = @IrsaliyeID) AND Miktar = 0
	END
	--update irs table
	UPDATE wms.IRS SET Onay=1 WHERE ID=@IrsaliyeID
	if(@LinkEvrakNo<>'') UPDATE wms.IRS SET LinkEvrakNo=@LinkEvrakNo WHERE ID=@IrsaliyeID
	--insert yeni görev
	if(@YeniGorevTipiID>0)
	begin
		--check if görev exists
		IF NOT EXISTS (SELECT ID FROM wms.Gorev WHERE (GorevNo = @YeniGorevNo) AND (GorevTipiID = @YeniGorevTipiID) AND (DurumID = 9))
		BEGIN
			INSERT INTO wms.Gorev (DepoID, GorevNo, GorevTipiID, DurumID, Olusturan, OlusturmaTarihi, OlusturmaSaati, Bilgi, Aciklama, IrsaliyeID)
			SELECT DepoID, @YeniGorevNo, @YeniGorevTipiID, 9, @Kullanici, @BitisTarihi, @BitisSaati, Bilgi, Aciklama, IrsaliyeID from wms.Gorev where ID=@GorevID
			set @GorevID=SCOPE_IDENTITY()
		END
		ELSE
			SELECT @GorevID = ID FROM wms.Gorev WHERE (GorevNo = @YeniGorevNo) AND (GorevTipiID = @YeniGorevTipiID) AND (DurumID = 9)
		--Insert GorevIRS
		INSERT INTO wms.GorevIRS (GorevID, IrsaliyeID)
		VALUES (@GorevID, @IrsaliyeID)
	end
	--return
	SELECT @GorevID as GorevID

END
GO
/****** Object:  StoredProcedure [wms].[TerminalGorevList]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 28.12.2017
-- Last Modify: 30.03.2018
-- Description:	terminaldeki GetGorevList sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalGorevList]
	@DurumID int,
	@DepoID int,
	@GorevTipiID int,
	@Gorevli varchar(5),
	@Username varchar(5)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        Gorev.ID, Gorev.IrsaliyeID, dbo.fnFormatDateFromInt(Gorev.OlusturmaTarihi) AS OlusturmaTarihi, Gorev.Bilgi, Gorev.GorevNo, IRS.EvrakNo, Gorev.Gorevli
	FROM            wms.Gorev WITH (nolock) INNER JOIN
							 wms.IRS WITH (nolock) ON Gorev.IrsaliyeID = IRS.ID LEFT OUTER JOIN
							 wms.GorevUsers WITH (nolock) ON Gorev.ID = GorevUsers.GorevID LEFT OUTER JOIN
							 usr.Users WITH (nolock) ON Gorev.Gorevli = usr.Users.Kod
	WHERE        DurumID = @DurumID AND GorevTipiID = @GorevTipiID AND
							case when @Gorevli <> '' then case when Gorevli = @Gorevli then 1 else 0 end else 1 end = 1 AND
							--satıştan iade ise tüm depolar
							case when Gorev.GorevTipiID <> 73 then case when (Gorev.DepoID = @DepoID) then 1 else 0 end else 1 end = 1 AND 
							--Görev durumu açık olsa da kendisi görevi bitirmişse göstermesin
							CASE WHEN @DurumID=9 then 
										case when (GorevID IN
												(SELECT        GorevID
												FROM            GorevUsers
												WHERE        (GorevUsers.UserName = @Username) AND (GorevUsers.BitisTarihi IS NOT NULL))) then 0 else 1 end 
							else 1 end = 1
	GROUP BY Gorev.ID, Gorev.IrsaliyeID, dbo.fnFormatDateFromInt(Gorev.OlusturmaTarihi), Gorev.Bilgi, Gorev.GorevNo, IRS.EvrakNo, Gorev.Gorevli

END
GO
/****** Object:  StoredProcedure [wms].[TerminalGorevOzet]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 27.12.2017
-- Last Modify: 16.01.2018
-- Description:	terminaldeki GetGorevOzet sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalGorevOzet]
	@DepoID int,
	@Username varchar(5)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        ComboItem_Name.ID, ComboItem_Name.Name AS Ad, COUNT(wms.Gorev.ID) AS Sayi
	FROM            wms.Gorev INNER JOIN
							 ComboItem_Name ON wms.Gorev.GorevTipiID = ComboItem_Name.ID
	WHERE        (wms.Gorev.DepoID = @DepoID) AND (wms.Gorev.DurumID = 9) AND (wms.Gorev.ID NOT IN
								 (SELECT        GorevID
								   FROM            wms.GorevUsers
								   WHERE        (GorevUsers.UserName = @Username) AND (GorevUsers.BitisTarihi IS NOT NULL)))
	GROUP BY ComboItem_Name.Name, ComboItem_Name.ID

END
GO
/****** Object:  StoredProcedure [wms].[TerminalMalKabul_GorevKontrol]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 28.12.2017
-- Last Modify: 28.12.2017
-- Description:	terminaldeki MalKabul_GorevKontrol sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalMalKabul_GorevKontrol]
	@GorevID int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        SUM(wms.IRS_Detay.Miktar - ISNULL(wms.IRS_Detay.OkutulanMiktar, 0)) AS Bitmeyen
	FROM            wms.GorevIRS INNER JOIN
							 wms.IRS_Detay ON wms.GorevIRS.IrsaliyeID = wms.IRS_Detay.IrsaliyeID
	WHERE        (wms.GorevIRS.GorevID = @GorevID)
	GROUP BY wms.IRS_Detay.MalKodu, wms.IRS_Detay.MakaraNo

END
GO
/****** Object:  StoredProcedure [wms].[TerminalPackageBarcodeDetails]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 28.12.2017
-- Last Modify: 28.12.2017
-- Description:	terminaldeki PackageBarcodeDetails sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalPackageBarcodeDetails]
	@GorevID int

AS
BEGIN
	SET NOCOUNT ON;

	--ölçüden ağırlık bul
	DECLARE @Agirlik decimal
	SELECT        @Agirlik = SUM(ISNULL(wms.Olcu.Agirlik * wms.IRS_Detay.Miktar, 0))
	FROM            wms.IRS_Detay INNER JOIN
							 wms.GorevIRS ON wms.IRS_Detay.IrsaliyeID = wms.GorevIRS.IrsaliyeID LEFT OUTER JOIN
							 wms.Olcu ON wms.IRS_Detay.Birim = wms.Olcu.Birim AND wms.IRS_Detay.MalKodu = wms.Olcu.MalKodu
	WHERE        (wms.GorevIRS.GorevID = @GorevID)
	--detect hepsivar column
	DECLARE @HepsiVar bit
	IF EXISTS (SELECT        TOP (1) wms.Olcu.Agirlik
				FROM            wms.IRS_Detay INNER JOIN
										 wms.GorevIRS ON wms.IRS_Detay.IrsaliyeID = wms.GorevIRS.IrsaliyeID LEFT OUTER JOIN
										 wms.Olcu ON wms.IRS_Detay.Birim = wms.Olcu.Birim AND wms.IRS_Detay.MalKodu = wms.Olcu.MalKodu
				WHERE        (wms.Olcu.Agirlik IS NULL OR wms.Olcu.Agirlik = 0) AND (wms.GorevIRS.GorevID = @GorevID))
		SET @HepsiVar = 0
	else
		SET @HepsiVar = 1
	--return
	SELECT       SevkiyatNo, PaketNo, Adet, PaketTipiID, Agirlik + @Agirlik as Agirlik, @HepsiVar AS HepsiVar
	FROM            wms.GorevPaketler
	WHERE        (wms.GorevPaketler.GorevID = @GorevID)


END
GO
/****** Object:  StoredProcedure [wms].[UpdateGorev]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 05.05.2017
-- Last Modify: 22.12.2017
-- Description:	görev aktifleştirir
-- =============================================
CREATE PROCEDURE [wms].[UpdateGorev]
	@IrsID int

AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE       wms.IRS
	SET                Onay = 1
	WHERE        (ID IN
						(SELECT        IrsaliyeID
						FROM            wms.GorevIRS
						WHERE        (GorevID IN
												(SELECT        GorevID
												FROM            wms.GorevIRS
												WHERE        (IrsaliyeID = @IrsID)))))


	UPDATE       wms.Gorev
	SET                DurumID = 9
	WHERE        (ID IN
						(SELECT        GorevID
						FROM            wms.GorevIRS
						WHERE        (IrsaliyeID = @IrsID)))

END

GO
/****** Object:  StoredProcedure [wms].[UpdateGorevDurum]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 20.06.2017
-- Last Modify: 20.06.2017
-- Description:	görevi açar
-- =============================================
CREATE PROCEDURE [wms].[UpdateGorevDurum]
	@Tarih int,
	@Saat int,
	@IrsID int

AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE       wms.Gorev
	SET                DurumID = 9, OlusturmaTarihi = @Tarih, OlusturmaSaati = @Saat
	WHERE        (ID IN
								 (SELECT        wms.Gorev.ID
								   FROM            wms.Gorev INNER JOIN
															 wms.GorevIRS ON wms.Gorev.ID = wms.GorevIRS.GorevID
								   WHERE        (wms.GorevIRS.IrsaliyeID = @IrsID) AND (wms.Gorev.DurumID = 15)))

END
GO
/****** Object:  StoredProcedure [wms].[UpdateIrsaliye]    Script Date: 05/04/2018 10:32:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 05.05.2017
-- Modify date: 23.10.2017
-- Description:	irsaliye evrak noyu değiştirir
-- =============================================
CREATE PROCEDURE [wms].[UpdateIrsaliye]
	@IrsID int,
	@EvrakNo varchar(16),
	@LinkEvrakNo varchar(16)

AS
BEGIN
	SET NOCOUNT ON;
	
	if (@EvrakNo <> '')
		UPDATE wms.IRS SET EvrakNo = @EvrakNo, Onay = 1 WHERE (ID = @IrsID)
	if (@LinkEvrakNo <> '')
		UPDATE wms.IRS SET LinkEvrakNo = @LinkEvrakNo WHERE (ID = @IrsID)

END


GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Buraya firmanın kısa kodu gelecek.' , @level0type=N'SCHEMA',@level0name=N'ong', @level1type=N'TABLE',@level1name=N'Musteri', @level2type=N'COLUMN',@level2name=N'Firma'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unvan alanında Sorumlu adı soyadı da tutuluyor.' , @level0type=N'SCHEMA',@level0name=N'ong', @level1type=N'TABLE',@level1name=N'Musteri', @level2type=N'COLUMN',@level2name=N'Unvan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Araya ; koyarak birden fazla mailde tanımlanabilir.' , @level0type=N'SCHEMA',@level0name=N'ong', @level1type=N'TABLE',@level1name=N'Musteri', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bu alan Kullanici tablosundaki KulKodu alanından gelecek.' , @level0type=N'SCHEMA',@level0name=N'ong', @level1type=N'TABLE',@level1name=N'ProjeForm', @level2type=N'COLUMN',@level2name=N'Sorumlu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bu alan Firma tablosundaki Sorumlu alanından gelecek.' , @level0type=N'SCHEMA',@level0name=N'ong', @level1type=N'TABLE',@level1name=N'ProjeForm', @level2type=N'COLUMN',@level2name=N'KarsiSorumlu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'PID=0 olanlar projedir.  Formlarda PID bağlı olduğu Projenin ID sini verir.(Aynı tabloda)' , @level0type=N'SCHEMA',@level0name=N'ong', @level1type=N'TABLE',@level1name=N'ProjeForm', @level2type=N'COLUMN',@level2name=N'PID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type -> 0 ise form bazlı 1-> ise kontrol bazlı' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Perm', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Farklı programlar varsa ayırt etmek için' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Perm', @level2type=N'COLUMN',@level2name=N'AppType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'burası rolün kodu olacak' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'rol Adı' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Roles', @level2type=N'COLUMN',@level2name=N'Aciklama'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tip=>0 ise ElTerminali  1 ise BackOffice ' , @level0type=N'SCHEMA',@level0name=N'usr', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'Tip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Girdi=0, Çıktı=1' , @level0type=N'SCHEMA',@level0name=N'wms', @level1type=N'TABLE',@level1name=N'GorevYer', @level2type=N'COLUMN',@level2name=N'GC'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Girdi=0, Çıktı=1' , @level0type=N'SCHEMA',@level0name=N'wms', @level1type=N'TABLE',@level1name=N'IRS', @level2type=N'COLUMN',@level2name=N'IslemTur'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'FINSTA6xx.STI.EvrakNo' , @level0type=N'SCHEMA',@level0name=N'wms', @level1type=N'TABLE',@level1name=N'IRS_Detay', @level2type=N'COLUMN',@level2name=N'KynkSiparisNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=kapalı, 0=açık' , @level0type=N'SCHEMA',@level0name=N'wms', @level1type=N'TABLE',@level1name=N'Yer', @level2type=N'COLUMN',@level2name=N'MakaraDurum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Girdi=0, Çıktı=1' , @level0type=N'SCHEMA',@level0name=N'wms', @level1type=N'TABLE',@level1name=N'Yer_Log', @level2type=N'COLUMN',@level2name=N'GC'
GO
USE [master]
GO
ALTER DATABASE [BIRIKIM] SET  READ_WRITE 
GO
