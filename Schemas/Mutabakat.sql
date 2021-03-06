USE [master]
GO
/****** Object:  Database [OnlineMutabakat]    Script Date: 05.01.2018 12:50:20 ******/
CREATE DATABASE [OnlineMutabakat]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OnlineMutabakat', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\OnlineMutabakat.mdf' , SIZE = 267200KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OnlineMutabakat_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\OnlineMutabakat_1.ldf' , SIZE = 1250752KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OnlineMutabakat] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineMutabakat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineMutabakat] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineMutabakat] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineMutabakat] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OnlineMutabakat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineMutabakat] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnlineMutabakat] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineMutabakat] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineMutabakat] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineMutabakat] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineMutabakat] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [OnlineMutabakat] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineMutabakat] SET QUERY_STORE = ON
GO
ALTER DATABASE [OnlineMutabakat] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [OnlineMutabakat]
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
USE [OnlineMutabakat]
GO
/****** Object:  Table [dbo].[GenelAyar]    Script Date: 05.01.2018 12:50:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenelAyar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DisIP] [varchar](100) NULL,
	[Port] [varchar](20) NULL,
	[TekSeferdeMailGonAdet] [int] NOT NULL,
	[MailJobAdi] [varchar](50) NULL,
	[Kaydeden] [varchar](50) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](50) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Sirket_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kayıtlar]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kayıtlar](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MutabakatID] [int] NOT NULL,
	[Emails] [varchar](max) NOT NULL,
	[Gonderen] [varchar](50) NOT NULL,
	[Tarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Kayıtlar] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanici]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanici](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciKodu] [varchar](50) NOT NULL,
	[Sifre] [varchar](50) NOT NULL,
	[AdiSoyadi] [varchar](50) NOT NULL,
	[EMail] [varchar](100) NULL,
	[Admin] [bit] NULL,
	[Kaydeden] [varchar](50) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](50) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[KullaniciKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mutabakat]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mutabakat](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Tip] [smallint] NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[MaliYil] [int] NOT NULL,
	[HesapKodu] [varchar](30) NOT NULL,
	[Tarih] [int] NOT NULL,
	[MuhKodu] [varchar](2) NOT NULL,
	[Bakiye] [decimal](18, 2) NOT NULL,
	[BA] [smallint] NOT NULL,
	[FaturaAdedi] [smallint] NOT NULL,
	[MailTo] [varchar](300) NOT NULL,
	[MailCc] [varchar](300) NOT NULL,
	[Baslik] [varchar](300) NOT NULL,
	[Icerik] [varchar](max) NOT NULL,
	[MailID] [int] NOT NULL,
	[MailDurum] [smallint] NOT NULL,
	[MailKayitTarih] [smalldatetime] NULL,
	[MailGonderimTarih] [smalldatetime] NULL,
	[Cevap] [smallint] NOT NULL,
	[CevapTarih] [smalldatetime] NULL,
	[Cevaplayan] [varchar](100) NULL,
	[Aciklama] [varchar](1000) NULL,
	[GonderimSayisi] [smallint] NOT NULL,
	[Kaydeden] [varchar](50) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](50) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Mutabakat] PRIMARY KEY CLUSTERED 
(
	[Tip] ASC,
	[SirketKodu] ASC,
	[MaliYil] ASC,
	[HesapKodu] ASC,
	[Tarih] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MutabakatDosya]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MutabakatDosya](
	[MutabakatID] [int] NOT NULL,
	[CevapMi] [bit] NOT NULL,
	[Guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[DosyaAdi] [nvarchar](250) NOT NULL,
	[Boyut] [varchar](50) NOT NULL,
	[BoyutByte] [int] NOT NULL,
 CONSTRAINT [PK_MutabakatDosya] PRIMARY KEY CLUSTERED 
(
	[MutabakatID] ASC,
	[CevapMi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SirketMail]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SirketMail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SirketKodu] [varchar](2) NOT NULL,
	[HesapAdi] [varchar](50) NOT NULL,
	[GondericiAdi] [varchar](30) NOT NULL,
	[EMaili] [varchar](100) NOT NULL,
	[Sifre] [varchar](100) NOT NULL,
	[SMTPServer] [varchar](100) NOT NULL,
	[SMTPPort] [smallint] NOT NULL,
	[SSL] [bit] NOT NULL,
	[GondericiProfilAdi] [varchar](50) NOT NULL,
	[CHKMutabakatEMail] [varchar](50) NULL,
	[CHKBABSEMail] [varchar](50) NULL,
	[MutabakatEMail] [varchar](100) NULL,
	[BABSEMail] [varchar](100) NULL,
	[Kaydeden] [varchar](50) NOT NULL,
	[KayitTarih] [smalldatetime] NOT NULL,
	[Degistiren] [varchar](50) NOT NULL,
	[DegisTarih] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_SirketMail] PRIMARY KEY CLUSTERED 
(
	[SirketKodu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_SirketMail_ProfillAdi] UNIQUE NONCLUSTERED 
(
	[GondericiProfilAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [Ind_Mutabakat_Cevap]    Script Date: 05.01.2018 12:50:21 ******/
CREATE NONCLUSTERED INDEX [Ind_Mutabakat_Cevap] ON [dbo].[Mutabakat]
(
	[Cevap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Ind_Mutabakat_MailDurum]    Script Date: 05.01.2018 12:50:21 ******/
CREATE NONCLUSTERED INDEX [Ind_Mutabakat_MailDurum] ON [dbo].[Mutabakat]
(
	[MailDurum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [Unique_SirketMail_HesapAdi]    Script Date: 05.01.2018 12:50:21 ******/
CREATE NONCLUSTERED INDEX [Unique_SirketMail_HesapAdi] ON [dbo].[SirketMail]
(
	[HesapAdi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GenelAyar] ADD  CONSTRAINT [DF_Sirket_TekSeferdeMailGonAdet]  DEFAULT ((5)) FOR [TekSeferdeMailGonAdet]
GO
ALTER TABLE [dbo].[Kullanici] ADD  CONSTRAINT [DF_Kullanici_Admin]  DEFAULT ((0)) FOR [Admin]
GO
ALTER TABLE [dbo].[Mutabakat] ADD  CONSTRAINT [DF_Mutabakat_Bakiye]  DEFAULT ((0)) FOR [Bakiye]
GO
ALTER TABLE [dbo].[Mutabakat] ADD  CONSTRAINT [DF_Mutabakat_BA]  DEFAULT ((0)) FOR [BA]
GO
ALTER TABLE [dbo].[Mutabakat] ADD  CONSTRAINT [DF_Mutabakat_FaturaAdedi]  DEFAULT ((0)) FOR [FaturaAdedi]
GO
ALTER TABLE [dbo].[Mutabakat] ADD  CONSTRAINT [DF_Mutabakat_MailID]  DEFAULT ((0)) FOR [MailID]
GO
ALTER TABLE [dbo].[Mutabakat] ADD  CONSTRAINT [DF_Mutabakat_MailDurum]  DEFAULT ((-1)) FOR [MailDurum]
GO
ALTER TABLE [dbo].[Mutabakat] ADD  CONSTRAINT [DF_Mutabakat_Cevap]  DEFAULT ((0)) FOR [Cevap]
GO
ALTER TABLE [dbo].[Mutabakat] ADD  CONSTRAINT [DF_Mutabakat_GonderimSayisi]  DEFAULT ((0)) FOR [GonderimSayisi]
GO
ALTER TABLE [dbo].[MutabakatDosya] ADD  CONSTRAINT [DF_MutabakatDosya_Cevap]  DEFAULT ((0)) FOR [CevapMi]
GO
ALTER TABLE [dbo].[MutabakatDosya] ADD  CONSTRAINT [DF_MutabakatCevap_Guid]  DEFAULT (newid()) FOR [Guid]
GO
/****** Object:  StoredProcedure [dbo].[sp_MailGonder]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		12M
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Last Modify: 20.12.2017
-- Description:	mail gönderir
-- =============================================
CREATE PROCEDURE [dbo].[sp_MailGonder]

AS
BEGIN
	SET NOCOUNT ON;

	--DECLARES
	DECLARE @GonderimSay INT;
	SELECT @GonderimSay=TekSeferdeMailGonAdet FROM [dbo].[GenelAyar] (nolock)
	if ISNULL(@GonderimSay,0)=0
		SET @GonderimSay = 5;
	DECLARE @ID INT, @SirKod VARCHAR(2), @MailTo VARCHAR(500), @MailCc VARCHAR(500), @Icerik VARCHAR(MAX), @Baslik VARCHAR(500), @MailID INT, @MailDurum SMALLINT, @GonderSayi SMALLINT
	DECLARE @KontrolSirKod VARCHAR(2), @ProfilAdi VARCHAR(50), @MailToBcc VARCHAR(100), @SirketGondericiAdi VARCHAR(150), @Degistiren VARCHAR(50)
	SET @KontrolSirKod = ''
	--sayac
	DECLARE @sayac INT; SET @sayac = 0
	--loop
	WHILE (@sayac < @GonderimSay)
	BEGIN
		--sets
		SELECT TOP(1) @ID=[ID], @SirKod=[SirketKodu], @MailTo=[MailTo], @MailCc=[MailCc], @Baslik=[Baslik], @Icerik=[Icerik], @Degistiren = Degistiren FROM dbo.Mutabakat (nolock) WHERE (MailDurum = -1) OR ((CevapTarih IS NULL) AND (MailKayitTarih < GETDATE() - 3) AND (MailKayitTarih > GETDATE() - 20)) ORDER BY MailDurum
		--kontrol
		if @ID IS NULL
			BREAK;
		--SirKod Kontrol
		if @SirKod <> @KontrolSirKod begin
			SELECT @ProfilAdi=GondericiProfilAdi, @MailToBcc = MutabakatEMail, @SirketGondericiAdi = GondericiAdi+' <'+EMaili+'>' FROM dbo.SirketMail (nolock) WHERE SirketKodu=@SirKod
			SET @KontrolSirKod=@SirKod
		end
		--send
		if ISNULL(@ProfilAdi,'') <> ''
		begin
			--send
			EXEC msdb.dbo.sp_send_dbmail
						@profile_name = @ProfilAdi,
						@from_address = @SirketGondericiAdi,
						@recipients = @MailTo,
						@copy_recipients = @MailCc,
						@blind_copy_recipients = @MailToBcc,
						@subject = @Baslik,
						@body = @Icerik,
						@body_format = 'HTML',
						@importance = 'HIGH',
						@mailitem_id = @MailID OUTPUT
			--update satir
			UPDATE Mutabakat SET MailID = @MailID, MailDurum = 0, MailKayitTarih = GETDATE(), GonderimSayisi = GonderimSayisi + 1 WHERE (ID = @ID)
			--insert log
			INSERT INTO Kayıtlar (MutabakatID, Emails, Gonderen, Tarih) VALUES (@ID, @MailTo+';'+@MailCc, @Degistiren, GETDATE())
			--show result
			select @ProfilAdi as ProfilAdi, @SirketGondericiAdi as GondericiAdi, @MailTo as MailTo, @MailCc as MailCc, @MailToBcc as MailToBcc, @Baslik as Baslik
		end
		--dispose
		SET @ID=NULL;
		SET @sayac = @sayac + 1;
	END	
	--mail kontrol
	EXEC [sp_MailKontrol]

END
GO
/****** Object:  StoredProcedure [dbo].[sp_MailGonderGenel]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_MailGonderGenel]
	
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @ID INT, @SirKod VARCHAR(2), @MailTo VARCHAR(500), @MailCc VARCHAR(500), @Icerik VARCHAR(MAX),
			@Baslik VARCHAR(500), @MailID INT, @MailDurum SMALLINT

	DECLARE @KontrolSirKod VARCHAR(2), @ProfilAdi VARCHAR(50)
	SET @KontrolSirKod = ''

	


	DECLARE MAILCUR CURSOR FOR
	(
		SELECT TOP(5) ID, SirketKodu, MailTo, MailCc, Baslik, Icerik
		FROM dbo.Mutabakat (nolock) 
		WHERE MailDurum = -1
	)

	OPEN MAILCUR

	FETCH NEXT FROM MAILCUR INTO @ID, @SirKod, @MailTo, @MailCc, @Baslik, @Icerik
	WHILE @@FETCH_STATUS = 0
	BEGIN
		if @SirKod <> @KontrolSirKod begin
			SELECT @ProfilAdi=GondericiProfilAdi FROM dbo.SirketMail (nolock) WHERE SirketKodu=@SirKod
			SET @KontrolSirKod=@SirKod			
		end

		
		if ISNULL(@ProfilAdi,'') <> ''
		begin
			EXEC msdb.dbo.sp_send_dbmail
						@profile_name = @ProfilAdi,
						@recipients = @MailTo,
						@copy_recipients= @MailCc,
						@subject = @Baslik,
						@body=@Icerik,
						@body_format ='HTML',
						@importance ='HIGH',
						@mailitem_id = @MailID OUTPUT

			UPDATE dbo.Mutabakat SET [MailID] = @MailID, [MailDurum]=0, [MailKayitTarih]=GETDATE(), [GonderimSayisi]=GonderimSayisi+1 WHERE ID=@ID
		end
		
		FETCH NEXT FROM MAILCUR INTO @ID, @SirKod, @MailTo, @MailCc, @Baslik, @Icerik
	END
    
	CLOSE MAILCUR
	DEALLOCATE MAILCUR


	EXEC sp_MailKontrol

END

GO
/****** Object:  StoredProcedure [dbo].[sp_MailKontrol]    Script Date: 05.01.2018 12:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_MailKontrol]

AS
BEGIN
	SET NOCOUNT ON;

	Declare @ID INT, @MailID INT, @sent_status smallint, @sent_date smalldatetime

	DECLARE KONTROL_CUR CURSOR FOR
	(
		SELECT ID, MailID FROM dbo.Mutabakat (nolock) WHERE [MailDurum]=0 AND [MailID]>0
	)

	OPEN KONTROL_CUR

	FETCH NEXT FROM KONTROL_CUR INTO @ID, @MailID
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		select @sent_status=sent_status, @sent_date=sent_date from msdb.dbo.sysmail_mailitems 
		WHERE mailitem_id=@MailID
		
		if @sent_status is not null
			UPDATE [dbo].[Mutabakat] SET [MailDurum]=@sent_status, [MailGonderimTarih]=@sent_date WHERE ID=@ID
			

		FETCH NEXT FROM KONTROL_CUR INTO @ID, @MailID
	END	

	CLOSE KONTROL_CUR
	DEALLOCATE KONTROL_CUR

END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:BA-1:BS-2:CHK' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mutabakat', @level2type=N'COLUMN',@level2name=N'Tip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 Borç, 1 Alacak' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mutabakat', @level2type=N'COLUMN',@level2name=N'BA'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:Gönderilmedi-1:Gönderildi-2:Hatalı' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mutabakat', @level2type=N'COLUMN',@level2name=N'MailDurum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:Gönderilmedi-1:Beklemede-2:Mutabıkız-3:MutabıkDegiliz' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mutabakat', @level2type=N'COLUMN',@level2name=N'Cevap'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Mutabakat', @level2type=N'COLUMN',@level2name=N'Cevaplayan'
GO
USE [master]
GO
ALTER DATABASE [OnlineMutabakat] SET  READ_WRITE 
GO
