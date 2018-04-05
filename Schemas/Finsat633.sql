USE [FINSAT633]
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetMalAdi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 20.12.2017
-- Modify date: 20.12.2017
-- Description:	mal koduna göre mal adını getirir
-- =============================================
CREATE FUNCTION [dbo].[fnGetMalAdi]
(
	@MalKodu varchar(30)
)
RETURNS varchar(150)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(150)

	-- Add the T-SQL statements to compute the return value here
	SELECT        @Result = MalAdi
	FROM            FINSAT633.FINSAT633.STK WITH (NOLOCK)
	WHERE        (MalKodu = @MalKodu)

	-- Return the result of the function
	RETURN @Result

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetKod3OrtGun]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetKod3OrtGun] (@HesapKodu varchar(20),@CHKKod2 varchar(20)) RETURNS DECIMAL(24, 6)
AS
BEGIN
DECLARE @Kod3OrtGun DECIMAL(24, 6) = 0
DECLARE @Gun_FaturaTutar DECIMAL(24, 6) = 0
DECLARE @FaturaTop DECIMAL(24, 6) = 0
--DECLARE @Gun DECIMAL(24, 6) = 0
DECLARE @Gun INT = 0
DECLARE @FaturaTutar DECIMAL(24, 6) = 0
DECLARE @Bakiye DECIMAL(24, 6) = 0
DECLARE @Alacak DECIMAL(24, 6) = 0
DECLARE @OrtGunFaturaTutar DECIMAL(24, 6) = 0
DECLARE @OrtGunFaturaTop DECIMAL(24, 6) = 0
DECLARE @Alacak2 DECIMAL(24, 6) = 0
DECLARE @Gun_OrtGun_FaturaTutar DECIMAL(24, 6) = 0
DECLARE @ChkIlkKarakter VARCHAR(1)=''
DECLARE @BakilacakGun INT=0
DECLARE @ChkIlk INT

IF @HesapKodu IS NULL
	RETURN  (@Kod3OrtGun)

SET @ChkIlkKarakter = SUBSTRING(@HesapKodu,1,1)

IF ISNUMERIC(@ChkIlkKarakter)=1
SET @ChkIlk = CONVERT(INT, @ChkIlkKarakter)

IF (@ChkIlk < 5 AND @ChkIlk > 0)
     SET @BakilacakGun = 20
ELSE IF (@ChkIlk = 5 OR @ChkIlk = 6)
     SET @BakilacakGun = 24
ELSE IF (@ChkIlk = 9)
     SET @BakilacakGun = 20

IF  CHARINDEX('1',@CHKKod2) > 0
     SET @BakilacakGun = 59


DECLARE CRS_PERSONEL CURSOR FOR
select  
tarih,
Borc,
Alacak,
Tutar,
DATEDIFF(dd,0,GETDATE()+2) - tarih AS Gun
from 
(   select	
	tarih,
	Borc,
	Alacak,
	Tutar,
	SRT,EvrakNo
	from 
	(   select 
		A.*,  
		Case A.BA when B.BorA Then 0 Else 1 End  as st 
		from 
		(   select 
			1 as AyYil,
			A.hesapkodu, 
			A.hesapkodu+'                              1' as SRT,
			ISNULL(B.tarih,0 )  as Tarih ,
			ISNULL(B.EvrakNo,' ' )  as EvrakNo,
			ISNULL(B.BA,0 )  as BA,
			ISNULL(B.Aciklama,0 )  as Aciklama,
			ISNULL(B.VadeTarih,0 )  as VadeTarih,
			ISNULL(A.Unvan1,' ' )  as Unvan,
			Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
			Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
			ISNULL(B.Tutar,0 )  as Tutar, 
			Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
			Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
			ISNULL(B.TLTutar,0 )  as TLTutar,  
			Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
			Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
			ISNULL(B.YTLTutar,0 )  as YTLTutar, 
			ISNULL(A.OzelKod,' ' )  as OzelKod,
			ISNULL(A.TipKod,' ' )  as TipKod,
			ISNULL(A.GrupKod,' ' )  as grupkod,
			ISNULL(A.BolgeKod,' ' )  as BolgeKod,
			ISNULL(A.KartTip,0 )  as Karttip,
			ISNULL(B.DovizKuru,0 )  as DovizKuru, 
			ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
			DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
			ISNULL(B.Islemtip,0 )  as Islemtip, 
			(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
			opsiyonGunu,
			A.Kod1,
			A.Kod2,
			A.Kod3,
			A.Kod4,
			A.Kod5,
			A.Kod6,
			A.Kod7,
			A.Kod8,
			A.Kod9,
			A.Kod10,
			A.Kod11,
			A.Kod12,
			A.Kod13,
			A.Notlar,
			MhsKod 
			from 
			(	select 
				A.hesapkodu,
				tarih,
				A.EvrakNo,
				min(A.BA) as BA,
				min(A.Aciklama) as Aciklama,
				VadeTarih,
				Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip					when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
				Sum(A.DovizTutar) as DovizTutar,
				min(A.DovizKuru) as DovizKuru,
				min(A.DovizCinsi) as DovizCinsi,
				Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case							B.Islemtip when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
				Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
				min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
				from 
				(   select 
					* 
					from 
					(	select 
						hesapkodu,
						tarih,
						EvrakNo,
						BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where   BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
						and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
						and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 

						UNION ALL 

						select 
						hesapkodu,
						tarih,
						EvrakNo,
						BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
						And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
						and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
						
						UNION ALL 
						
						select 
						KarsiHesapKodu as HesapKodu,
						tarih,
						EvrakNo,
						1 as BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
						and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu 
						and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
						UNION ALL 
						
						select 
						KarsiHesapKodu as HesapKodu,
						tarih,
						EvrakNo,
						0 as BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu and						KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
					) A 
					where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 )						And  IslemTip in 
					(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
				) A 
				left join 
				(	select 
					hesapkodu,
					BA,
					sum(A.Tutar) as Tutar,
					KynkEvraktip,
					Evrakno, 
					DovizCinsi,  
					Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
					sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
					sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 
					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				
					UNION ALL 

					select 
					hesapkodu,
					BA,
					sum(A.Tutar) as Tutar,
					KynkEvraktip,
					Evrakno, 
					DovizCinsi,  
					Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
					sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
					sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1 
					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi =							   B.DovizCinsi  
				group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
			) B ,
			FINSAT633.FINSAT633.CHK A (NoLock)  
			where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA +			DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and A.BolgeKod<='ZZZZ'			and A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and A.OzelKod<='ZZZZZ'  and			A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and				A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and			A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'  and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and			A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
		) A 
		, 
		(	select 
			hesapkodu, 
			Case Sign(SUM(YTLborc)-sum(YTLalacak)) when 1 then 0 when -1 then 1 when 0 then -2 else -3 End  as BorA 
			from 
			(	select 
				1 as AyYil,
				A.hesapkodu, 
				A.hesapkodu+'                              1' as SRT,
				ISNULL(B.tarih,0 )  as Tarih ,
				ISNULL(B.EvrakNo,' ' )  as EvrakNo,
				ISNULL(B.BA,0 )  as BA,
				ISNULL(B.Aciklama,0 )  as Aciklama,
				ISNULL(B.VadeTarih,0 )  as VadeTarih,
				ISNULL(A.Unvan1,' ' )  as Unvan,
				Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
				Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
				ISNULL(B.Tutar,0 )  as Tutar, 
				Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
				Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
				ISNULL(B.TLTutar,0 )  as TLTutar, 
				Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
				Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
				ISNULL(B.YTLTutar,0 )  as YTLTutar, 
				ISNULL(A.OzelKod,' ' )  as OzelKod,
				ISNULL(A.TipKod,' ' )  as TipKod,
				ISNULL(A.GrupKod,' ' )  as grupkod,
				ISNULL(A.BolgeKod,' ' )  as BolgeKod,
				ISNULL(A.KartTip,0 )  as Karttip,
				ISNULL(B.DovizKuru,0 )  as DovizKuru, 
				ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
				DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
				ISNULL(B.Islemtip,0 )  as Islemtip, 
				(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
				opsiyonGunu,
				A.Kod1,
				A.Kod2,
				A.Kod3,
				A.Kod4,
				A.Kod5,
				A.Kod6,
				A.Kod7,
				A.Kod8,
				A.Kod9,
				A.Kod10,
				A.Kod11,
				A.Kod12,
				A.Kod13,
				A.Notlar,
				MhsKod 
				from 
				(	select 
					A.hesapkodu,
					tarih,
					A.EvrakNo,
					min(A.BA) as BA,
					min(A.Aciklama) as Aciklama,
					VadeTarih,
					Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case								B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
					Sum(A.DovizTutar) as DovizTutar,
					min(A.DovizKuru) as DovizKuru,
					min(A.DovizCinsi) as DovizCinsi,
					Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case							B.Islemtip when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
					Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
					min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
					from 
					(	select 
						* 
						from 
						(	select 
							hesapkodu,
							tarih,
							EvrakNo,
							BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where   BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
							and  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
							and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 

							UNION ALL 
							
							select 
							hesapkodu,
							tarih,
							EvrakNo,
							BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip, 
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
							And  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
							and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
							
							UNION ALL 
							
							select 
							KarsiHesapKodu as HesapKodu,
							tarih,EvrakNo,
							1 as BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null and								KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu 
							and  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
							UNION ALL 
							
							select 
							KarsiHesapKodu as HesapKodu,
							tarih,
							EvrakNo,
							0 as BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu							and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
						) A 
						where (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 )
						And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
					) A 
					left join 
					(   select 
						hesapkodu,
						BA,
						sum(A.Tutar) as Tutar,
						KynkEvraktip,
						Evrakno, 
						DovizCinsi,  
						Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
						sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
						sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 
						group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
						
						UNION ALL 
						
						select hesapkodu,
						BA,
						sum(A.Tutar) as Tutar,
						KynkEvraktip,
						Evrakno, 
						DovizCinsi,  
						Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
						sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
						sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1 
						group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End
					) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi =								B.DovizCinsi  
					group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
				) B ,
				FINSAT633.FINSAT633.CHK A (NoLock) 
			    where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA +					KDVA + DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and						A.BolgeKod<='ZZZZ'  and A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and				A.OzelKod<='ZZZZZ'  and A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and							A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and						A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'				and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
			) A  
			group by hesapkodu 
		) B 
		where A.hesapkodu=B.hesapkodu 
	) A 
	where st=0 
	
	UNION 
	
	select 
	0 as tarih,
	Sum(A.Borc) as Borc,
	Sum(A.Alacak) as Alacak, 
	sum(A.Tutar) as Tutar,
	A.hesapkodu+'                              0' as SRT,
	' ' as Evrakno
	from 
	(	select 
		1 as AyYil,
		A.hesapkodu, 
		A.hesapkodu+'                              1' as SRT,
		ISNULL(B.tarih,0 )  as Tarih ,
		ISNULL(B.EvrakNo,' ' )  as EvrakNo,
		ISNULL(B.BA,0 )  as BA,
		ISNULL(B.Aciklama,0 )  as Aciklama,
		ISNULL(B.VadeTarih,0 )  as VadeTarih,
		ISNULL(A.Unvan1,' ' )  as Unvan,
		Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
		Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
		ISNULL(B.Tutar,0 )  as Tutar, Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
		Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
		ISNULL(B.TLTutar,0 )  as TLTutar, 
		Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
		Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
		ISNULL(B.YTLTutar,0 )  as YTLTutar, 
		ISNULL(A.OzelKod,' ' )  as OzelKod,
		ISNULL(A.TipKod,' ' )  as TipKod,
		ISNULL(A.GrupKod,' ' )  as grupkod,
		ISNULL(A.BolgeKod,' ' )  as BolgeKod,
		ISNULL(A.KartTip,0 )  as Karttip,
		ISNULL(B.DovizKuru,0 )  as DovizKuru, 
		ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
		DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
		ISNULL(B.Islemtip,0 )  as Islemtip, 
		(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
		opsiyonGunu,
		A.Kod1,
		A.Kod2,
		A.Kod3,
		A.Kod4,
		A.Kod5,
		A.Kod6,
		A.Kod7,
		A.Kod8,
		A.Kod9,
		A.Kod10,
		A.Kod11,
		A.Kod12,
		A.Kod13,
		A.Notlar,
		MhsKod 
		from 
		(	select 
			A.hesapkodu,
			tarih,
			A.EvrakNo,
			min(A.BA) as BA,
			min(A.Aciklama) as Aciklama,
			VadeTarih,
			Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip					when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
			Sum(A.DovizTutar) as DovizTutar,
			min(A.DovizKuru) as DovizKuru,
			min(A.DovizCinsi) as DovizCinsi,
			Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case B.Islemtip				when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
			Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
			min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
			from 
			(	select 
				* 
				from 
				(	select 
					hesapkodu,
					tarih,
					EvrakNo,
					BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip,
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where   BA=0    and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and								A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ'  
					and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
					
					UNION ALL 
					
					select 
					hesapkodu,
					tarih,
					EvrakNo,
					BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock)  
					where  BA=1   and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ'  
					And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
					
					UNION ALL 
					
					select 
					KarsiHesapKodu as HesapKodu,
					tarih,
					EvrakNo,
					1 as BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar  
					from FINSAT633.FINSAT633.CHI A  (NoLock)  
					where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'					and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and A.Kod6<='ZZZZZ'  and							A.Kod7>=' '  and A.Kod7<='ZZZZZ'  and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null  and KarsiHesapKodu >=@HesapKodu						and KarsiHesapKodu <=@HesapKodu 
					and  IslemTip in																																	(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)

					UNION ALL 
					
					select 
					KarsiHesapKodu as HesapKodu,
					tarih,
					EvrakNo,
					0 as BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar  
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where  BA=1  and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'					and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and A.Kod6<='ZZZZZ'  and							A.Kod7>=' '  and A.Kod7<='ZZZZZ'  and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu and KarsiHesapKodu <> ' '						and KarsiHesapKodu is not null 
				) A 
				where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) 
				And  IslemTip in																																	(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
			) A 
			left join 
			(	select 
				hesapkodu,
				BA,
				sum(A.Tutar) as Tutar,
				KynkEvraktip,
				Evrakno, 
				DovizCinsi,  
				Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
				sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
				sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
				from FINSAT633.FINSAT633.CHI A  (NoLock)  
				where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0  
				group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
				
				UNION ALL  
				
				select 
				hesapkodu,
				BA,
				sum(A.Tutar) as Tutar,
				KynkEvraktip,
				Evrakno, 
				DovizCinsi, 
			    Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
				sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
				sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar  
				from FINSAT633.FINSAT633.CHI A  (NoLock)  where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
			) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi			group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
		) B ,
		FINSAT633.FINSAT633.CHK A (NoLock)  
		where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA +			DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and A.BolgeKod<='ZZZZ'  and		A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and A.OzelKod<='ZZZZZ'  and				A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and				A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and			A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'  and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and			A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
	) A  
	group by A.hesapkodu 
) A     
order by SRT,Tarih desc,EvrakNo desc

DECLARE @ALCK DECIMAL(24, 6) = 0
DECLARE @BRC DECIMAL(24, 6) = 0
DECLARE @TTR DECIMAL(24, 6) = 0
--DECLARE @TRH DECIMAL(24, 6) = 0
DECLARE @TRH INT = 0

DECLARE @aa INT=0


OPEN CRS_PERSONEL

FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@BRC,@ALCK,@TTR,@GUN

SET @Bakiye = @BRC- @ALCK
SET @Alacak = @Bakiye
SET @Alacak2 = @Bakiye

FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@ALCK,@BRC,@TTR, @GUN


WHILE @@FETCH_STATUS =0
	BEGIN
	
		SET @Gun = DATEDIFF(dd,0,GETDATE()+2) - @TRH
        SET @FaturaTutar = @TTR
        SET @OrtGunFaturaTutar = @TTR

		IF (@Alacak > 0)
        BEGIN   
                 
            IF (@Alacak > @FaturaTutar OR @Alacak = @FaturaTutar)
            BEGIN
           
                SET @Alacak = @Alacak-@FaturaTutar

                IF (@Gun > @BakilacakGun)
                BEGIN
                 SET @aa =124
                    SET @FaturaTop = @FaturaTop + @FaturaTutar
                    SET @Gun_FaturaTutar = @Gun_FaturaTutar + (@Gun * @FaturaTutar)
                END
            END
            ELSE
            BEGIN
                IF (@Gun > @BakilacakGun)
                BEGIN
                
                    SET @FaturaTop = @FaturaTop + @Alacak
                    SET @Gun_FaturaTutar =@Gun_FaturaTutar + (@Gun * (@Alacak))
                END
                SET @Alacak = 0;
            END
        END



		IF (@Alacak2 > 0)
        BEGIN
            IF (@Alacak2 > @OrtGunFaturaTutar OR @Alacak2 = @OrtGunFaturaTutar)
            BEGIN
                SET @Alacak2 = @Alacak2 - @OrtGunFaturaTutar

                SET @OrtGunFaturaTop = @OrtGunFaturaTop + @OrtGunFaturaTutar
                SET @Gun_OrtGun_FaturaTutar = @Gun_OrtGun_FaturaTutar + (@Gun * @OrtGunFaturaTutar)
            END
            ELSE
            BEGIN

                SET @OrtGunFaturaTop = @OrtGunFaturaTop + @Alacak2
                SET @Gun_OrtGun_FaturaTutar = @Gun_OrtGun_FaturaTutar + (@Gun * (@Alacak2))
                SET @Alacak2 = 0
            END
        END

		FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@ALCK,@BRC,@TTR, @GUN
 
	END

CLOSE CRS_PERSONEL

DEALLOCATE CRS_PERSONEL

IF (@Gun_FaturaTutar > 0)
SET @Kod3OrtGun = @Kod3OrtGun + (@Gun_FaturaTutar / @FaturaTop)

RETURN  (@Kod3OrtGun)
END







GO
/****** Object:  UserDefinedFunction [dbo].[GetOrtGunKod3OrtBakiyeGun-ESKİ]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetOrtGunKod3OrtBakiyeGun-ESKİ] (@HesapKodu varchar(20),@CHKKod2 varchar(20))
RETURNS @RtnValue table 
(
	Kod3OrtBakiye DECIMAL(24, 6),
	Kod3OrtGun DECIMAL(24, 6),
	OrtalamaGun DECIMAL(24, 6)
) 
AS
BEGIN
DECLARE @OrtalamaGun DECIMAL(24, 6) = 0
DECLARE @Kod3OrtGun DECIMAL(24, 6) = 0
DECLARE @Kod3OrtBakiye DECIMAL(24, 6) = 0
DECLARE @Gun_FaturaTutar DECIMAL(24, 6) = 0
DECLARE @FaturaTop DECIMAL(24, 6) = 0
--DECLARE @Gun DECIMAL(24, 6) = 0
DECLARE @Gun INT = 0
DECLARE @FaturaTutar DECIMAL(24, 6) = 0
DECLARE @Bakiye DECIMAL(24, 6) = 0
DECLARE @Alacak DECIMAL(24, 6) = 0
DECLARE @OrtGunFaturaTutar DECIMAL(24, 6) = 0
DECLARE @OrtGunFaturaTop DECIMAL(24, 6) = 0
DECLARE @Alacak2 DECIMAL(24, 6) = 0
DECLARE @Gun_OrtGun_FaturaTutar DECIMAL(24, 6) = 0
DECLARE @ChkIlkKarakter VARCHAR(1)=''
DECLARE @BakilacakGun INT=0
DECLARE @ChkIlk INT

IF @HesapKodu IS NULL
BEGIN
	INSERT INTO @RtnValue(Kod3OrtBakiye, Kod3OrtGun, OrtalamaGun) VALUES(0, 0, 0)
	RETURN
END

SET @ChkIlkKarakter = SUBSTRING(@HesapKodu,1,1)

IF ISNUMERIC(@ChkIlkKarakter)=1
SET @ChkIlk = CONVERT(INT, @ChkIlkKarakter)

IF (@ChkIlk < 5 AND @ChkIlk > 0)
     SET @BakilacakGun = 20
ELSE IF (@ChkIlk = 5 OR @ChkIlk = 6)
     SET @BakilacakGun = 24
ELSE IF (@ChkIlk = 9)
     SET @BakilacakGun = 20

IF  CHARINDEX('1',@CHKKod2) > 0
     SET @BakilacakGun = 59


DECLARE CRS_PERSONEL CURSOR FOR
select  
tarih,
Borc,
Alacak,
Tutar,
DATEDIFF(dd,0,GETDATE()+2) - tarih AS Gun
from 
(   select	
	tarih,
	Borc,
	Alacak,
	Tutar,
	SRT,EvrakNo
	from 
	(   select 
		A.*,  
		Case A.BA when B.BorA Then 0 Else 1 End  as st 
		from 
		(   select 
			1 as AyYil,
			A.hesapkodu, 
			A.hesapkodu+'                              1' as SRT,
			ISNULL(B.tarih,0 )  as Tarih ,
			ISNULL(B.EvrakNo,' ' )  as EvrakNo,
			ISNULL(B.BA,0 )  as BA,
			ISNULL(B.Aciklama,0 )  as Aciklama,
			ISNULL(B.VadeTarih,0 )  as VadeTarih,
			ISNULL(A.Unvan1,' ' )  as Unvan,
			Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
			Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
			ISNULL(B.Tutar,0 )  as Tutar, 
			Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
			Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
			ISNULL(B.TLTutar,0 )  as TLTutar,  
			Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
			Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
			ISNULL(B.YTLTutar,0 )  as YTLTutar, 
			ISNULL(A.OzelKod,' ' )  as OzelKod,
			ISNULL(A.TipKod,' ' )  as TipKod,
			ISNULL(A.GrupKod,' ' )  as grupkod,
			ISNULL(A.BolgeKod,' ' )  as BolgeKod,
			ISNULL(A.KartTip,0 )  as Karttip,
			ISNULL(B.DovizKuru,0 )  as DovizKuru, 
			ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
			DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
			ISNULL(B.Islemtip,0 )  as Islemtip, 
			(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
			opsiyonGunu,
			A.Kod1,
			A.Kod2,
			A.Kod3,
			A.Kod4,
			A.Kod5,
			A.Kod6,
			A.Kod7,
			A.Kod8,
			A.Kod9,
			A.Kod10,
			A.Kod11,
			A.Kod12,
			A.Kod13,
			A.Notlar,
			MhsKod 
			from 
			(	select 
				A.hesapkodu,
				tarih,
				A.EvrakNo,
				min(A.BA) as BA,
				min(A.Aciklama) as Aciklama,
				VadeTarih,
				Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip					when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
				Sum(A.DovizTutar) as DovizTutar,
				min(A.DovizKuru) as DovizKuru,
				min(A.DovizCinsi) as DovizCinsi,
				Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case							B.Islemtip when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
				Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
				min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
				from 
				(   select 
					* 
					from 
					(	select 
						hesapkodu,
						tarih,
						EvrakNo,
						BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where   BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
						and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
						and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 

						UNION ALL 

						select 
						hesapkodu,
						tarih,
						EvrakNo,
						BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
						And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
						and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
						
						UNION ALL 
						
						select 
						KarsiHesapKodu as HesapKodu,
						tarih,
						EvrakNo,
						1 as BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
						and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu 
						and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
						UNION ALL 
						
						select 
						KarsiHesapKodu as HesapKodu,
						tarih,
						EvrakNo,
						0 as BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu and						KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
					) A 
					where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 )						And  IslemTip in 
					(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
				) A 
				left join 
				(	select 
					hesapkodu,
					BA,
					sum(A.Tutar) as Tutar,
					KynkEvraktip,
					Evrakno, 
					DovizCinsi,  
					Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
					sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
					sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 
					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				
					UNION ALL 

					select 
					hesapkodu,
					BA,
					sum(A.Tutar) as Tutar,
					KynkEvraktip,
					Evrakno, 
					DovizCinsi,  
					Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
					sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
					sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1 
					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi =							   B.DovizCinsi  
				group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
			) B ,
			FINSAT633.FINSAT633.CHK A (NoLock)  
			where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA +			DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and A.BolgeKod<='ZZZZ'			and A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and A.OzelKod<='ZZZZZ'  and			A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and				A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and			A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'  and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and			A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
		) A 
		, 
		(	select 
			hesapkodu, 
			Case Sign(SUM(YTLborc)-sum(YTLalacak)) when 1 then 0 when -1 then 1 when 0 then -2 else -3 End  as BorA 
			from 
			(	select 
				1 as AyYil,
				A.hesapkodu, 
				A.hesapkodu+'                              1' as SRT,
				ISNULL(B.tarih,0 )  as Tarih ,
				ISNULL(B.EvrakNo,' ' )  as EvrakNo,
				ISNULL(B.BA,0 )  as BA,
				ISNULL(B.Aciklama,0 )  as Aciklama,
				ISNULL(B.VadeTarih,0 )  as VadeTarih,
				ISNULL(A.Unvan1,' ' )  as Unvan,
				Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
				Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
				ISNULL(B.Tutar,0 )  as Tutar, 
				Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
				Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
				ISNULL(B.TLTutar,0 )  as TLTutar, 
				Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
				Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
				ISNULL(B.YTLTutar,0 )  as YTLTutar, 
				ISNULL(A.OzelKod,' ' )  as OzelKod,
				ISNULL(A.TipKod,' ' )  as TipKod,
				ISNULL(A.GrupKod,' ' )  as grupkod,
				ISNULL(A.BolgeKod,' ' )  as BolgeKod,
				ISNULL(A.KartTip,0 )  as Karttip,
				ISNULL(B.DovizKuru,0 )  as DovizKuru, 
				ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
				DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
				ISNULL(B.Islemtip,0 )  as Islemtip, 
				(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
				opsiyonGunu,
				A.Kod1,
				A.Kod2,
				A.Kod3,
				A.Kod4,
				A.Kod5,
				A.Kod6,
				A.Kod7,
				A.Kod8,
				A.Kod9,
				A.Kod10,
				A.Kod11,
				A.Kod12,
				A.Kod13,
				A.Notlar,
				MhsKod 
				from 
				(	select 
					A.hesapkodu,
					tarih,
					A.EvrakNo,
					min(A.BA) as BA,
					min(A.Aciklama) as Aciklama,
					VadeTarih,
					Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case								B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
					Sum(A.DovizTutar) as DovizTutar,
					min(A.DovizKuru) as DovizKuru,
					min(A.DovizCinsi) as DovizCinsi,
					Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case							B.Islemtip when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
					Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
					min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
					from 
					(	select 
						* 
						from 
						(	select 
							hesapkodu,
							tarih,
							EvrakNo,
							BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where   BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
							and  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
							and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 

							UNION ALL 
							
							select 
							hesapkodu,
							tarih,
							EvrakNo,
							BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip, 
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
							And  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
							and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
							
							UNION ALL 
							
							select 
							KarsiHesapKodu as HesapKodu,
							tarih,EvrakNo,
							1 as BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null and								KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu 
							and  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
							UNION ALL 
							
							select 
							KarsiHesapKodu as HesapKodu,
							tarih,
							EvrakNo,
							0 as BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu							and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
						) A 
						where (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 )
						And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
					) A 
					left join 
					(   select 
						hesapkodu,
						BA,
						sum(A.Tutar) as Tutar,
						KynkEvraktip,
						Evrakno, 
						DovizCinsi,  
						Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
						sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
						sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 
						group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
						
						UNION ALL 
						
						select hesapkodu,
						BA,
						sum(A.Tutar) as Tutar,
						KynkEvraktip,
						Evrakno, 
						DovizCinsi,  
						Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
						sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
						sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1 
						group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End
					) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi =								B.DovizCinsi  
					group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
				) B ,
				FINSAT633.FINSAT633.CHK A (NoLock) 
			    where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA +					KDVA + DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and						A.BolgeKod<='ZZZZ'  and A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and				A.OzelKod<='ZZZZZ'  and A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and							A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and						A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'				and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
			) A  
			group by hesapkodu 
		) B 
		where A.hesapkodu=B.hesapkodu 
	) A 
	where st=0 
	
	UNION 
	
	select 
	0 as tarih,
	Sum(A.Borc) as Borc,
	Sum(A.Alacak) as Alacak, 
	sum(A.Tutar) as Tutar,
	A.hesapkodu+'                              0' as SRT,
	' ' as Evrakno
	from 
	(	select 
		1 as AyYil,
		A.hesapkodu, 
		A.hesapkodu+'                              1' as SRT,
		ISNULL(B.tarih,0 )  as Tarih ,
		ISNULL(B.EvrakNo,' ' )  as EvrakNo,
		ISNULL(B.BA,0 )  as BA,
		ISNULL(B.Aciklama,0 )  as Aciklama,
		ISNULL(B.VadeTarih,0 )  as VadeTarih,
		ISNULL(A.Unvan1,' ' )  as Unvan,
		Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
		Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
		ISNULL(B.Tutar,0 )  as Tutar, Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
		Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
		ISNULL(B.TLTutar,0 )  as TLTutar, 
		Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
		Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
		ISNULL(B.YTLTutar,0 )  as YTLTutar, 
		ISNULL(A.OzelKod,' ' )  as OzelKod,
		ISNULL(A.TipKod,' ' )  as TipKod,
		ISNULL(A.GrupKod,' ' )  as grupkod,
		ISNULL(A.BolgeKod,' ' )  as BolgeKod,
		ISNULL(A.KartTip,0 )  as Karttip,
		ISNULL(B.DovizKuru,0 )  as DovizKuru, 
		ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
		DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
		ISNULL(B.Islemtip,0 )  as Islemtip, 
		(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
		opsiyonGunu,
		A.Kod1,
		A.Kod2,
		A.Kod3,
		A.Kod4,
		A.Kod5,
		A.Kod6,
		A.Kod7,
		A.Kod8,
		A.Kod9,
		A.Kod10,
		A.Kod11,
		A.Kod12,
		A.Kod13,
		A.Notlar,
		MhsKod 
		from 
		(	select 
			A.hesapkodu,
			tarih,
			A.EvrakNo,
			min(A.BA) as BA,
			min(A.Aciklama) as Aciklama,
			VadeTarih,
			Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip					when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
			Sum(A.DovizTutar) as DovizTutar,
			min(A.DovizKuru) as DovizKuru,
			min(A.DovizCinsi) as DovizCinsi,
			Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case B.Islemtip				when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
			Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
			min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
			from 
			(	select 
				* 
				from 
				(	select 
					hesapkodu,
					tarih,
					EvrakNo,
					BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip,
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where   BA=0    and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and								A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ'  
					and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
					
					UNION ALL 
					
					select 
					hesapkodu,
					tarih,
					EvrakNo,
					BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock)  
					where  BA=1   and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ'  
					And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
					
					UNION ALL 
					
					select 
					KarsiHesapKodu as HesapKodu,
					tarih,
					EvrakNo,
					1 as BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar  
					from FINSAT633.FINSAT633.CHI A  (NoLock)  
					where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'					and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and A.Kod6<='ZZZZZ'  and							A.Kod7>=' '  and A.Kod7<='ZZZZZ'  and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null  and KarsiHesapKodu >=@HesapKodu						and KarsiHesapKodu <=@HesapKodu 
					and  IslemTip in																																	(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)

					UNION ALL 
					
					select 
					KarsiHesapKodu as HesapKodu,
					tarih,
					EvrakNo,
					0 as BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar  
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where  BA=1  and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'					and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and A.Kod6<='ZZZZZ'  and							A.Kod7>=' '  and A.Kod7<='ZZZZZ'  and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu and KarsiHesapKodu <> ' '						and KarsiHesapKodu is not null 
				) A 
				where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) 
				And  IslemTip in																																	(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
			) A 
			left join 
			(	select 
				hesapkodu,
				BA,
				sum(A.Tutar) as Tutar,
				KynkEvraktip,
				Evrakno, 
				DovizCinsi,  
				Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
				sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
				sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
				from FINSAT633.FINSAT633.CHI A  (NoLock)  
				where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0  
				group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
				
				UNION ALL  
				
				select 
				hesapkodu,
				BA,
				sum(A.Tutar) as Tutar,
				KynkEvraktip,
				Evrakno, 
				DovizCinsi, 
			    Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
				sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
				sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar  
				from FINSAT633.FINSAT633.CHI A  (NoLock)  where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
			) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi			group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
		) B ,
		FINSAT633.FINSAT633.CHK A (NoLock)  
		where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA +			DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and A.BolgeKod<='ZZZZ'  and		A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and A.OzelKod<='ZZZZZ'  and				A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and				A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and			A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'  and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and			A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
	) A  
	group by A.hesapkodu 
) A     
order by SRT,Tarih desc,EvrakNo desc

DECLARE @ALCK DECIMAL(24, 6) = 0
DECLARE @BRC DECIMAL(24, 6) = 0
DECLARE @TTR DECIMAL(24, 6) = 0
--DECLARE @TRH DECIMAL(24, 6) = 0
DECLARE @TRH INT = 0

DECLARE @aa INT=0


OPEN CRS_PERSONEL

FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@BRC,@ALCK,@TTR,@GUN

SET @Bakiye = @BRC- @ALCK
SET @Alacak = @Bakiye
SET @Alacak2 = @Bakiye

FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@ALCK,@BRC,@TTR, @GUN


WHILE @@FETCH_STATUS =0
	BEGIN
	
		SET @Gun = DATEDIFF(dd,0,GETDATE()+2) - @TRH
        SET @FaturaTutar = @TTR
        SET @OrtGunFaturaTutar = @TTR

		IF (@Alacak > 0)
        BEGIN   
                 
            IF (@Alacak > @FaturaTutar OR @Alacak = @FaturaTutar)
            BEGIN
           
                SET @Alacak = @Alacak-@FaturaTutar

                IF (@Gun > @BakilacakGun)
                BEGIN
                 SET @aa =124
                    SET @FaturaTop = @FaturaTop + @FaturaTutar
                    SET @Gun_FaturaTutar = @Gun_FaturaTutar + (@Gun * @FaturaTutar)
                END
            END
            ELSE
            BEGIN
                IF (@Gun > @BakilacakGun)
                BEGIN
                
                    SET @FaturaTop = @FaturaTop + @Alacak
                    SET @Gun_FaturaTutar =@Gun_FaturaTutar + (@Gun * (@Alacak))
                END
                SET @Alacak = 0;
            END
        END



		IF (@Alacak2 > 0)
        BEGIN
            IF (@Alacak2 > @OrtGunFaturaTutar OR @Alacak2 = @OrtGunFaturaTutar)
            BEGIN
                SET @Alacak2 = @Alacak2 - @OrtGunFaturaTutar

                SET @OrtGunFaturaTop = @OrtGunFaturaTop + @OrtGunFaturaTutar
                SET @Gun_OrtGun_FaturaTutar = @Gun_OrtGun_FaturaTutar + (@Gun * @OrtGunFaturaTutar)
            END
            ELSE
            BEGIN

                SET @OrtGunFaturaTop = @OrtGunFaturaTop + @Alacak2
                SET @Gun_OrtGun_FaturaTutar = @Gun_OrtGun_FaturaTutar + (@Gun * (@Alacak2))
                SET @Alacak2 = 0
            END
        END

		FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@ALCK,@BRC,@TTR, @GUN
 
	END

CLOSE CRS_PERSONEL

DEALLOCATE CRS_PERSONEL




IF (@Bakiye> 0)
SET @Kod3OrtBakiye = @Kod3OrtBakiye +  @FaturaTop

IF(@FaturaTop=0)
SET @FaturaTop=1

IF(@OrtGunFaturaTop=0)
SET @OrtGunFaturaTop=1

IF (@Gun_FaturaTutar > 0)
SET @Kod3OrtGun = @Kod3OrtGun + (@Gun_FaturaTutar / @FaturaTop)

IF (@Gun_OrtGun_FaturaTutar> 0)
SET @OrtalamaGun = @OrtalamaGun +  (@Gun_OrtGun_FaturaTutar / @OrtGunFaturaTop)

INSERT INTO @RtnValue(Kod3OrtBakiye, Kod3OrtGun, OrtalamaGun) VALUES(@Kod3OrtBakiye, @Kod3OrtGun, @OrtalamaGun)
RETURN
END








GO
/****** Object:  UserDefinedFunction [dbo].[GetSCekTCekPRBakiye-ESKİ]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetSCekTCekPRBakiye-ESKİ] (@HesapKodu varchar(20))
RETURNS @RtnValue table 
(
	SCek DECIMAL(24, 6),
	TCek DECIMAL(24, 6),
	PRBakiye DECIMAL(24, 6)
) 
AS
BEGIN
DECLARE @SahsiCek DECIMAL(24, 6) = 0
DECLARE @PRBakiye DECIMAL(24, 6) = 0 
DECLARE @ToplamCek DECIMAL(24, 6) = 0 
DECLARE @KrediLimiti DECIMAL(24, 6) = 0 
DECLARE @MCek DECIMAL(24, 6) = 0 
IF @HesapKodu IS NULL
BEGIN
	INSERT INTO @RtnValue(SCek, TCek, PRBakiye) VALUES(0, 0, 0)
	RETURN
END

SELECT top(1) 
@KrediLimiti= isnull(max([Kredi Limiti]),0), 
@PRBakiye = isnull(max([PRBakiye]),0),
@SahsiCek= isnull(max([Şahsi Çek]),0),
@MCek = isnull(max([Müşteri Çek]),0) 
FROM 
(	SELECT 
	alsana.[Ünvan],
	SUM(alsana.[Kredi Limiti]) as [Kredi Limiti] ,
	SUM(alsana.Bakiye) as [Bakiye] ,
	SUM(alsana.PRBakiye) as [PRBakiye] ,
	SUM(alsana.[Şahsi Çek]) as [Şahsi Çek] ,
	SUM(alsana.[Müşteri Çek]) as [Müşteri Çek] 
	FROM 
	(	
		SELECT 
		c.unvan as [Ünvan],
		SUM(c.kredilimiti) as [Kredi Limiti],
		SUM(c.bakiye) as Bakiye,
		SUM(c.PRBakiye)as PRBakiye,
		
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1='Ş' AND h.SonIslemTip NOT IN ('5','12','13') AND h.vadetarih>=getdate()-10 THEN h.tutar ELSE 0 END),0) 
		FROM  FINSAT633.FINSAT633.ACK h (nolock) 
		where h.veren=c.hesapkodu AND c.hesapkodu not like '%PR'
		)
		-
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1='Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10AND CHI.Islemtip=21 AND CHI.BA=1 THEN			CHI.tutar ELSE 0 END),0)  
		FROM  FINSAT633.FINSAT633.ACK h (nolock),
		FINSAT633.FINSAT633.CHI CHI (NOLOCK) 
		where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) AND h.veren=c.hesapkodu AND c.hesapkodu not like '%PR'AND								H.EVRAKNO=CHI.EVRAKNO
		)   as[Şahsi Çek] ,
		
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1<>'Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 THEN h.tutar ELSE 0 END),0)  
		FROM  FINSAT633.FINSAT633.ACK h (nolock) 
		where h.veren=c.hesapkodu AND c.hesapkodu not like '%PR'
		)
		-
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1<>'Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 AND CHI.Islemtip=21 AND CHI.BA=1				THEN CHI.tutar ELSE 0 END),0)  
		FROM  FINSAT633.FINSAT633.ACK h (nolock),
		FINSAT633.FINSAT633.CHI CHI (NOLOCK) 
		where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) AND h.veren=c.hesapkodu AND c.hesapkodu not like '%PR' AND								H.EVRAKNO=CHI.EVRAKNO
		) as[Müşteri Çek] 
		
		FROM 
		(	SELECT 
			d.HesapKodu as hesapkodu,
			d.unvan,
			ISNULL(CASE WHEN d.hesapkodu=@HesapKodu+'PR' THEN 0 ELSE ISNULL(SUM(d.kredilimiti),0) END ,0) as kredilimiti,
			SUM(d.bakiye) as bakiye,
			SUM(d.PRBakiye) as PRbakiye 
			FROM 
			(	SELECT 
				b.hesapkodu,
				b.unvan1+' '+b.unvan2 as unvan,  
				b.kredilimiti ,
				CASE WHEN b.hesapkodu=@HesapKodu+'PR' THEN 0 ELSE SUM(CASE WHEN a.BA = 0 THEN ISNULL(a.Tutar,0) ELSE ISNULL(-a.Tutar,0) END) END					as Bakiye,
				SUM(CASE WHEN a.BA = 0 THEN ISNULL(a.[PR Tutar],0) ELSE ISNULL(-a.[PR Tutar],0) END) as PRBakiye 
				FROM  
				(	SELECT  
					S.KarsiHesapKodu as HesapKodu,
					f.ozelkod,
					f.hesapno, 
					ISNULL((CASE WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar END),0) as [PR Tutar],
					ISNULL((CASE WHEN S.IslemTip IN ('5','9')  THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar END),0) as Tutar,						ISNULL((CASE WHEN S.BA = 0 THEN 1 ELSE 0 END),'-1') as BA 
					FROM  FINSAT633.FINSAT633.CHI S (nolock)  
					RIGHT JOIN  FINSAT633.FINSAT633.CHK F  (NOLOCK) on S.KarsiHesapKodu=F.HesapKodu 
					where S.KarsiHesapKodu is not null AND S.KarsiHesapKodu <> '' AND S.IslemTip NOT IN (16,21,27,32,36,37,41,42)  
					
					UNION ALL  
					
					SELECT 
					S.HesapKodu as hesapkodu,
					f.ozelkod ,
					f.hesapno,
					ISNULL((CASE WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN -S.Tutar WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar END),0) as [PR Tutar],  
					ISNULL((CASE WHEN S.IslemTip IN ('5','9') THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar END),0) as Tutar  ,
					ISNULL(S.BA,'-1') as BA 
					FROM  FINSAT633.FINSAT633.CHI S (nolock) 
					RIGHT JOIN  FINSAT633.FINSAT633.CHK F (NOLOCK) on  S.HesapKodu=F.HesapKodu 
					where S.IslemTip not in (16,21,27,32,36,37,41,42) 
				) a   
				RIGHT JOIN   FINSAT633.FINSAT633.CHK b (nolock) ON b.hesapkodu=a.hesapkodu 
				where  (b.HesapKodu LIKE @HesapKodu+'%' AND  ( b.HesapKodu BETWEEN '1' AND '7' and SUBSTRING(b.HesapKodu,1,1)<> '7' 
				or b.HesapKodu between '9' AND '99' ))
				 GROUP BY b.hesapkodu,(b.unvan1+' '+b.unvan2),b.kredilimiti 
			) d GROUP BY d.unvan,d.hesapkodu 
		) c GROUP BY c.unvan,c.hesapkodu 
	) alsana 
	GROUP BY [Ünvan] 
) AS A

SET @ToplamCek= @SahsiCek + @MCek 

INSERT INTO @RtnValue(SCek, TCek, PRBakiye) VALUES(@SahsiCek, @ToplamCek, @PRBakiye)
RETURN
END








GO
/****** Object:  UserDefinedFunction [dbo].[OrtalamaBeklemeSuresi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[OrtalamaBeklemeSuresi] (@FIRMA_ID varchar(20),@Buguntarih int) RETURNS int
AS
BEGIN

DECLARE @Bakiye float
DECLARE @xBA numeric(1)
DECLARE CarıOZET_Cursor CURSOR FOR
select  (isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0)) as Bakiye from ( select  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, (case when BA = 0 then 1 else 0 end) as BA From FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42)  Union All select  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, BA From FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu  
WHERE (b.Hesapkodu = @FIRMA_ID) 
group by b.HesapKodu

OPEN CarıOZET_Cursor
FETCH NEXT FROM CarıOZET_Cursor
INTO @Bakiye
CLOSE CarıOZET_Cursor
DEALLOCATE CarıOZET_Cursor
IF @Bakiye >= 0 SET @xBA = 0
ELSE SET @xBA = 1

DECLARE @SAdat Float, @SBakiye Float, @Flag int
DECLARE @RefDate int,@VADETAR int, @BA numeric(1),@tutar float, @Tarih int
DECLARE CarıEkstre_Cursor CURSOR FOR

 select Tarih, VadeTarih,A.BA as BA, Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar			 from 					( 					select * from ( 					select hesapkodu,tarih,EvrakNo,Aciklama, BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 from FINSAT633.FINSAT633.CHI A 					 where   hesapkodu = @FIRMA_ID					 UNION ALL 					select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,Aciklama, case BA when 0 then 1 else 0 end as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 from FINSAT633.FINSAT633.CHI A 					 where  karsihesapkodu =@FIRMA_ID				and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 					) A 					where (Islemtip<>5 and Islemtip<>9 and Islemtip<>16 and Islemtip<>21 and Islemtip<>27 and Islemtip<>32 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41) 					)  A 					left join 					(select hesapkodu,BA,sum(A.Tutar) as Tutar,Evrakno, IslemTip, KynkEvraktip 					 from FINSAT633.FINSAT633.CHI A 					 where (Islemtip=5 or Islemtip=9) 					 group by hesapkodu,BA,KynkEvraktip,Evrakno, IslemTip 					) B 					on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA WHERE A.BA =@xBA  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih  order by Tarih DESC



OPEN CarıEkstre_Cursor
FETCH NEXT FROM CarıEkstre_Cursor
INTO @Tarih, @VADETAR,  @BA, @TUTAR

SET @SAdat=0
SET @SBakiye = 0

SET @FLAG = @@FETCH_STATUS
WHILE (@FLAG = 0 )
BEGIN
IF @xBA = 0
BEGIN
If @SBakiye + @TUTAR > @Bakiye
BEGIN
SET @SAdat=@SAdat + ((@Bakiye-@SBakiye) * (@Buguntarih-@Tarih))
SET @SBakiye = @Bakiye 
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = @SBakiye + @TUTAR
SET @SAdat= @SAdat + (@tutar * (@Buguntarih-@Tarih))
END
END
ELSE
BEGIN
If @SBakiye + @TUTAR > ABS(@Bakiye)
BEGIN
SET @SAdat=@SAdat + CONVERT(FLOAT,((ABS(@Bakiye))-@SBakiye) *(@Buguntarih-@Tarih))
SET @SBakiye = (@Bakiye)
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = ((@SBakiye)) + @TUTAR
SET @SAdat=@SAdat + CONVERT(FLOAT,(@TUTAR * (@Buguntarih-@Tarih)))
END
END
FETCH NEXT FROM CarıEkstre_Cursor
INTO @Tarih, @VADETAR, @BA, @tutar
IF @@FETCH_STATUS <> 0 SET @Flag=1
END
CLOSE CarıEkstre_Cursor
DEALLOCATE CarıEkstre_Cursor
RETURN  (round(@SAdat / ABS(@Bakiye),0))
END





GO
/****** Object:  UserDefinedFunction [dbo].[OrtalamaBeklemeSuresiTao]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[OrtalamaBeklemeSuresiTao] (@FIRMA_ID varchar(20),@Buguntarih int,@Bakiye float) RETURNS int
AS
BEGIN

DECLARE @xBA numeric(1)
declare @ortbeklememe float
--DECLARE CarıOZET_Cursor CURSOR FOR
--select  (isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0)) as Bakiye from ( select  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, (case when BA = 0 then 1 else 0 end) as BA From FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42)  Union All select  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, BA From FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu  
--WHERE (b.Hesapkodu = @FIRMA_ID) 
--group by b.HesapKodu

--OPEN CarıOZET_Cursor
--FETCH NEXT FROM CarıOZET_Cursor
--INTO @Bakiye
--CLOSE CarıOZET_Cursor
--DEALLOCATE CarıOZET_Cursor
IF @Bakiye >= 0 SET @xBA = 0
ELSE SET @xBA = 1

DECLARE @SAdat Float, @SBakiye Float, @Flag int
DECLARE @RefDate int,@VADETAR int, @BA numeric(1),@tutar float, @Tarih int
DECLARE CarıEkstre_Cursor CURSOR FOR

select Tarih, VadeTarih,A.BA as BA, Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar			 
from 					
( 	select * from ( 					
select hesapkodu,tarih,EvrakNo,Aciklama, BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A (nolock) 					 
where   hesapkodu = @FIRMA_ID					 
UNION ALL 					
select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,Aciklama, case BA when 0 then 1 else 0 end as BA, 
Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
Tutar,DovizTutar,Islemtip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A (nolock) 					 
where  karsihesapkodu =@FIRMA_ID and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null ) A 					
where (Islemtip<>5 and Islemtip<>9 and Islemtip<>16 and Islemtip<>21 and Islemtip<>27 and Islemtip<>32 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41)) A 					
left join 					
(select hesapkodu,BA,sum(A.Tutar) as Tutar,Evrakno, IslemTip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A  (nolock)					 
where (Islemtip=5 or Islemtip=9) 					 
group by hesapkodu,BA,KynkEvraktip,Evrakno, IslemTip) B 					
on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA 
WHERE A.BA =@xBA  
group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih  
order by Tarih DESC



OPEN CarıEkstre_Cursor
FETCH NEXT FROM CarıEkstre_Cursor
INTO @Tarih, @VADETAR,  @BA, @TUTAR

SET @SAdat=0
SET @SBakiye = 0

SET @FLAG = @@FETCH_STATUS
WHILE (@FLAG = 0 )
BEGIN
IF @xBA = 0
BEGIN
If @SBakiye + @TUTAR > @Bakiye
BEGIN
SET @SAdat=@SAdat + ((@Bakiye-@SBakiye) * (@Buguntarih-@Tarih))
SET @SBakiye = @Bakiye 
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = @SBakiye + @TUTAR
SET @SAdat= @SAdat + (@tutar * (@Buguntarih-@Tarih))
END
END
ELSE
BEGIN
If @SBakiye + @TUTAR > ABS(@Bakiye)
BEGIN
SET @SAdat=@SAdat + CONVERT(FLOAT,((ABS(@Bakiye))-@SBakiye) *(@Buguntarih-@Tarih))
SET @SBakiye = (@Bakiye)
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = ((@SBakiye)) + @TUTAR
SET @SAdat=@SAdat + CONVERT(FLOAT,(@TUTAR * (@Buguntarih-@Tarih)))
END
END
FETCH NEXT FROM CarıEkstre_Cursor
INTO @Tarih, @VADETAR, @BA, @tutar
IF @@FETCH_STATUS <> 0 SET @Flag=1
END
CLOSE CarıEkstre_Cursor
DEALLOCATE CarıEkstre_Cursor
IF @Bakiye<>0
BEGIN
SET @ortbeklememe=(round(@SAdat / ABS(@Bakiye),0))
END
ELSE
BEGIN
SET @ortbeklememe=0
END
RETURN  @ortbeklememe
END







GO
/****** Object:  UserDefinedFunction [dbo].[OrtalamaBeklemeSuresiTaoSaticiKarti]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [dbo].[OrtalamaBeklemeSuresiTaoSaticiKarti] (@FIRMA_ID varchar(20),@Buguntarih int,@Bakiye float) RETURNS int
AS
BEGIN

DECLARE @xBA numeric(1)
declare @ortbeklememe float
--DECLARE CarıOZET_Cursor CURSOR FOR
--select  (isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0)) as Bakiye from ( select  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, (case when BA = 0 then 1 else 0 end) as BA From FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42)  Union All select  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, BA From FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu  
--WHERE (b.Hesapkodu = @FIRMA_ID) 
--group by b.HesapKodu

--OPEN CarıOZET_Cursor
--FETCH NEXT FROM CarıOZET_Cursor
--INTO @Bakiye
--CLOSE CarıOZET_Cursor
--DEALLOCATE CarıOZET_Cursor
IF @Bakiye >= 0 SET @xBA = 1
ELSE SET @xBA = 0

DECLARE @SAdat Float, @SBakiye Float, @Flag int
DECLARE @RefDate int,@VADETAR int, @BA numeric(1),@tutar float, @Tarih int
DECLARE CarıEkstre_Cursor CURSOR FOR

select Tarih, VadeTarih,A.BA as BA, Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar			 
from 					
(select * from 
(select hesapkodu,tarih,EvrakNo,Aciklama, BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A 	(nolock)				 
where   hesapkodu = @FIRMA_ID					 
UNION ALL 					
select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,Aciklama, case BA when 0 then 1 else 0 end as BA, 
Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A (nolock)					 
where  karsihesapkodu =@FIRMA_ID and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null ) A 					
where (Islemtip<>5 and Islemtip<>9 and Islemtip<>16 and Islemtip<>21 and Islemtip<>27 and Islemtip<>32 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41))  A 
left join 					
(select hesapkodu,BA,sum(A.Tutar) as Tutar,Evrakno, IslemTip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A (nolock)					 
where (Islemtip=5 or Islemtip=9) 					 
group by hesapkodu,BA,KynkEvraktip,Evrakno, IslemTip) B 		
on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA 
WHERE A.BA =@xBA  
group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih  
order by Tarih DESC



OPEN CarıEkstre_Cursor
FETCH NEXT FROM CarıEkstre_Cursor
INTO @Tarih, @VADETAR,  @BA, @TUTAR

SET @SAdat=0
SET @SBakiye = 0

SET @FLAG = @@FETCH_STATUS
WHILE (@FLAG = 0 )
BEGIN
IF @xBA = 1
BEGIN
If @SBakiye + @TUTAR > @Bakiye
BEGIN
SET @SAdat=@SAdat + ((@Bakiye-@SBakiye) * (@Buguntarih-@Tarih))
SET @SBakiye = @Bakiye 
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = @SBakiye + @TUTAR
SET @SAdat= @SAdat + (@tutar * (@Buguntarih-@Tarih))
END
END
ELSE
BEGIN
If @SBakiye + @TUTAR > ABS(@Bakiye)
BEGIN
SET @SAdat=@SAdat + CONVERT(FLOAT,((ABS(@Bakiye))-@SBakiye) *(@Buguntarih-@Tarih))
SET @SBakiye = (@Bakiye)
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = ((@SBakiye)) + @TUTAR
SET @SAdat=@SAdat + CONVERT(FLOAT,(@TUTAR * (@Buguntarih-@Tarih)))
END
END
FETCH NEXT FROM CarıEkstre_Cursor
INTO @Tarih, @VADETAR, @BA, @tutar
IF @@FETCH_STATUS <> 0 SET @Flag=1
END
CLOSE CarıEkstre_Cursor
DEALLOCATE CarıEkstre_Cursor
IF @Bakiye<>0
BEGIN
SET @ortbeklememe=(round(@SAdat / ABS(@Bakiye),0))
END
ELSE
BEGIN
SET @ortbeklememe=0
END
RETURN  @ortbeklememe
END







GO
/****** Object:  UserDefinedFunction [dbo].[ORTVADE1]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE FUNCTION [dbo].[ORTVADE1] (@FIRMA_ID varchar(20)) RETURNS int
AS
BEGIN

DECLARE @Bakiye float
DECLARE @xBA numeric(1)
DECLARE CarıOZET_Cursor CURSOR FOR
select  (isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0)) as Bakiye from ( select  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, (case when BA = 0 then 1 else 0 end) as BA From FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42)  Union All select  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, BA From FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu  
WHERE (b.Hesapkodu = @FIRMA_ID) 
group by b.HesapKodu

OPEN CarıOZET_Cursor
FETCH NEXT FROM CarıOZET_Cursor
INTO @Bakiye
CLOSE CarıOZET_Cursor
DEALLOCATE CarıOZET_Cursor
IF @Bakiye >= 0 SET @xBA = 0
ELSE SET @xBA = 1

DECLARE @SAdat Float, @SBakiye Float, @Flag int
DECLARE @RefDate int,@VADETAR int, @BA numeric(1),@tutar float
DECLARE CarıEkstre_Cursor CURSOR FOR

 select VadeTarih,A.BA as BA, 					 Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar			 from 					( 					select * from ( 					select hesapkodu,tarih,EvrakNo,Aciklama, BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 from FINSAT633.FINSAT633.CHI A 					 where   hesapkodu = @FIRMA_ID					 UNION ALL 					select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,Aciklama, case BA when 0 then 1 else 0 end as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 from FINSAT633.FINSAT633.CHI A 					 where  karsihesapkodu =@FIRMA_ID				and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 					) A 					where (Islemtip<>5 and Islemtip<>9 and Islemtip<>16 and Islemtip<>21 and Islemtip<>27 and Islemtip<>32 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41) 					)  A 					left join 					(select hesapkodu,BA,sum(A.Tutar) as Tutar,Evrakno, IslemTip, KynkEvraktip 					 from FINSAT633.FINSAT633.CHI A 					 where (Islemtip=5 or Islemtip=9) 					 group by hesapkodu,BA,KynkEvraktip,Evrakno, IslemTip 					) B 					on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA WHERE A.BA =@xBA  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih  order by Tarih DESC



OPEN CarıEkstre_Cursor
FETCH NEXT FROM CarıEkstre_Cursor
INTO @VADETAR,  @BA,@TUTAR
SET @SAdat=0
SET @SBakiye = 0
SET @FLAG = @@FETCH_STATUS
WHILE (@FLAG = 0 )
BEGIN
IF @xBA = 0
BEGIN
If @SBakiye + @TUTAR > @Bakiye
BEGIN
SET @SAdat=@SAdat + ((@Bakiye-@SBakiye) * @VADETAR)
SET @SBakiye = @Bakiye 
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = @SBakiye + @TUTAR
SET @SAdat= @SAdat + (@tutar * @VADETAR)
END
END
ELSE
BEGIN
If @SBakiye + @TUTAR > ABS(@Bakiye)
BEGIN
SET @SAdat=@SAdat + CONVERT(FLOAT,((ABS(@Bakiye))-@SBakiye) *@VADETAR)
SET @SBakiye = (@Bakiye)
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = ((@SBakiye)) + @TUTAR
SET @SAdat=@SAdat + CONVERT(FLOAT,(@TUTAR * @VADETAR))
END
END
FETCH NEXT FROM CarıEkstre_Cursor
INTO @VADETAR, @BA,@tutar
IF @@FETCH_STATUS <> 0 SET @Flag=1
END
CLOSE CarıEkstre_Cursor
DEALLOCATE CarıEkstre_Cursor
RETURN  (round(@SAdat / ABS(@Bakiye),0))
END





GO
/****** Object:  UserDefinedFunction [dbo].[ORTVADE1Tao]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE FUNCTION [dbo].[ORTVADE1Tao] (@FIRMA_ID varchar(20),@Bakiye float) RETURNS int
AS
BEGIN

DECLARE @xBA numeric(1)
declare @ortvade float
--DECLARE CarıOZET_Cursor CURSOR FOR
--select  (isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0)) as Bakiye from ( select  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, (case when BA = 0 then 1 else 0 end) as BA From FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42)  Union All select  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, BA From FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu  
--WHERE (b.Hesapkodu = @FIRMA_ID) 
--group by b.HesapKodu

--OPEN CarıOZET_Cursor
--FETCH NEXT FROM CarıOZET_Cursor
--INTO @Bakiye
--CLOSE CarıOZET_Cursor
--DEALLOCATE CarıOZET_Cursor
IF @Bakiye >= 0 SET @xBA = 0
ELSE SET @xBA = 1

DECLARE @SAdat Float, @SBakiye Float, @Flag int
DECLARE @RefDate int,@VADETAR int, @BA numeric(1),@tutar float
DECLARE CarıEkstre_Cursor CURSOR FOR

select VadeTarih,A.BA as BA,Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar			 
from (select * from 
(select hesapkodu,tarih,EvrakNo,Aciklama, BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A (nolock)				 
where   hesapkodu = @FIRMA_ID					 
UNION ALL 					
select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,Aciklama, case BA when 0 then 1 else 0 end as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 			from FINSAT633.FINSAT633.CHI A (nolock) 					 
where  karsihesapkodu =@FIRMA_ID and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null) A 					
where (Islemtip<>5 and Islemtip<>9 and Islemtip<>16 and Islemtip<>21 and Islemtip<>27 and Islemtip<>32 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41))  A 					
left join 					
(select hesapkodu,BA,sum(A.Tutar) as Tutar,Evrakno, IslemTip, KynkEvraktip 					 
from FINSAT633.FINSAT633.CHI A  (nolock)					 
where (Islemtip=5 or Islemtip=9) 					 
group by hesapkodu,BA,KynkEvraktip,Evrakno, IslemTip) B 					
on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA 
WHERE A.BA =@xBA  
group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih  
order by Tarih DESC



OPEN CarıEkstre_Cursor
FETCH NEXT FROM CarıEkstre_Cursor
INTO @VADETAR,  @BA,@TUTAR
SET @SAdat=0
SET @SBakiye = 0
SET @FLAG = @@FETCH_STATUS
WHILE (@FLAG = 0 )
BEGIN
IF @xBA = 0
BEGIN
If @SBakiye + @TUTAR > @Bakiye
BEGIN
SET @SAdat=@SAdat + ((@Bakiye-@SBakiye) * @VADETAR)
SET @SBakiye = @Bakiye 
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = @SBakiye + @TUTAR
SET @SAdat= @SAdat + (@tutar * @VADETAR)
END
END
ELSE
BEGIN
If @SBakiye + @TUTAR > ABS(@Bakiye)
BEGIN
SET @SAdat=@SAdat + CONVERT(FLOAT,((ABS(@Bakiye))-@SBakiye) *@VADETAR)
SET @SBakiye = (@Bakiye)
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = ((@SBakiye)) + @TUTAR
SET @SAdat=@SAdat + CONVERT(FLOAT,(@TUTAR * @VADETAR))
END
END
FETCH NEXT FROM CarıEkstre_Cursor
INTO @VADETAR, @BA,@tutar
IF @@FETCH_STATUS <> 0 SET @Flag=1
END
CLOSE CarıEkstre_Cursor
DEALLOCATE CarıEkstre_Cursor
IF @Bakiye<>0
BEGIN
 set @ortvade= (round(@SAdat / ABS(@Bakiye),0))
END
ELSE
BEGIN
set @ortvade=0
END
RETURN @ortvade
END







GO
/****** Object:  UserDefinedFunction [dbo].[ORTVADE1TaoSaticiKarti]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE FUNCTION [dbo].[ORTVADE1TaoSaticiKarti] (@FIRMA_ID varchar(20),@Bakiye float) RETURNS int
AS
BEGIN

DECLARE @xBA numeric(1)
declare @ortvade float
--DECLARE CarıOZET_Cursor CURSOR FOR
--select  (isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0)) as Bakiye from ( select  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, (case when BA = 0 then 1 else 0 end) as BA From FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42)  Union All select  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, BA From FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu  
--WHERE (b.Hesapkodu = @FIRMA_ID) 
--group by b.HesapKodu

--OPEN CarıOZET_Cursor
--FETCH NEXT FROM CarıOZET_Cursor
--INTO @Bakiye
--CLOSE CarıOZET_Cursor
--DEALLOCATE CarıOZET_Cursor
IF @Bakiye >= 0 SET @xBA =1
ELSE SET @xBA = 0

DECLARE @SAdat Float, @SBakiye Float, @Flag int
DECLARE @RefDate int,@VADETAR int, @BA numeric(1),@tutar float
DECLARE CarıEkstre_Cursor CURSOR FOR

 select VadeTarih,A.BA as BA,Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar			 
 from(select * from ( 					
 select hesapkodu,tarih,EvrakNo,Aciklama, BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 					 
 from FINSAT633.FINSAT633.CHI A (nolock)					 
 where   hesapkodu = @FIRMA_ID					 
 UNION ALL 					
 select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,Aciklama, case BA when 0 then 1 else 0 end as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,DovizTutar,Islemtip, KynkEvraktip 			from FINSAT633.FINSAT633.CHI A 	(nolock)				 
 where  karsihesapkodu =@FIRMA_ID and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null ) A 					
 where (Islemtip<>5 and Islemtip<>9 and Islemtip<>16 and Islemtip<>21 and Islemtip<>27 and Islemtip<>32 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41))  A 					
 left join 					
 (select hesapkodu,BA,sum(A.Tutar) as Tutar,Evrakno, IslemTip, KynkEvraktip 					 
 from FINSAT633.FINSAT633.CHI A (nolock)					 
 where (Islemtip=5 or Islemtip=9) 					 
 group by hesapkodu,BA,KynkEvraktip,Evrakno, IslemTip) B 					
 on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA 
 WHERE A.BA =@xBA  
 group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih  
 order by Tarih DESC



OPEN CarıEkstre_Cursor
FETCH NEXT FROM CarıEkstre_Cursor
INTO @VADETAR,  @BA,@TUTAR
SET @SAdat=0
SET @SBakiye = 0
SET @FLAG = @@FETCH_STATUS
WHILE (@FLAG = 0 )
BEGIN
IF @xBA = 1
BEGIN
If @SBakiye + @TUTAR > @Bakiye
BEGIN
SET @SAdat=@SAdat + ((@Bakiye-@SBakiye) * @VADETAR)
SET @SBakiye = @Bakiye 
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = @SBakiye + @TUTAR
SET @SAdat= @SAdat + (@tutar * @VADETAR)
END
END
ELSE
BEGIN
If @SBakiye + @TUTAR > ABS(@Bakiye)
BEGIN
SET @SAdat=@SAdat + CONVERT(FLOAT,((ABS(@Bakiye))-@SBakiye) *@VADETAR)
SET @SBakiye = (@Bakiye)
SET @Flag=1
END
ELSE
BEGIN
SET @SBakiye = ((@SBakiye)) + @TUTAR
SET @SAdat=@SAdat + CONVERT(FLOAT,(@TUTAR * @VADETAR))
END
END
FETCH NEXT FROM CarıEkstre_Cursor
INTO @VADETAR, @BA,@tutar
IF @@FETCH_STATUS <> 0 SET @Flag=1
END
CLOSE CarıEkstre_Cursor
DEALLOCATE CarıEkstre_Cursor
IF @Bakiye<>0
BEGIN
 set @ortvade= (round(@SAdat / ABS(@Bakiye),0))
END
ELSE
BEGIN
set @ortvade=0
END
RETURN @ortvade
END







GO
/****** Object:  UserDefinedFunction [dbo].[PrimOrani]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[PrimOrani](@STITarih INT, @CHKHesapKodu VARCHAR(20), @STIMalKodu VARCHAR(30), @STKGrupKod VARCHAR(20), @STKTipKod VARCHAR(20) ) RETURNS DECIMAL(24,6)

AS
BEGIN
DECLARE @PrimOran DECIMAL(24,6),
		@Prim DECIMAL(24,6),
		@PrimKodu INT,
		@Say INT

SET @PrimOran=0

	SELECT @Say=COUNT(*) FROM(
	SELECT P.PrimKodu FROM FINSAT633.FINSAT633.Primler(NOLOCK) P 
	WHERE 
	--P.FirmaKodu=@CHKHesapKodu AND 
	@STIMalKodu >= P.BaslangicMalKodu AND @STIMalKodu<=P.BitisMalKodu
	AND P.GrupKodlari LIKE '%'+@STKGrupKod+'%'
	AND P.TipKodlari LIKE '%'+@STKTipKod+'%'
	GROUP BY P.PrimKodu
	)A

if(@Say<>0)
	begin

	DECLARE CURPRIMKODU CURSOR FOR

	SELECT P.PrimKodu FROM FINSAT633.FINSAT633.Primler(NOLOCK) P 
	WHERE 
	--P.FirmaKodu=@CHKHesapKodu AND 
	@STIMalKodu >= P.BaslangicMalKodu AND @STIMalKodu<=P.BitisMalKodu
	AND P.GrupKodlari LIKE '%'+@STKGrupKod+'%'
	AND P.TipKodlari LIKE '%'+@STKTipKod+'%'


		OPEN CURPRIMKODU
		FETCH NEXT FROM CURPRIMKODU INTO @PrimKodu
		WHILE @@FETCH_STATUS =0
		BEGIN

			DECLARE CURPRIM CURSOR FOR

Select
			(
			CASE Donemi WHEN 'Yıllık' THEN 
			(CASE YillikOran WHEN '' THEN 0 ELSE CAST(YillikOran AS DECIMAL(24,6)) END)
			WHEN '3 Aylık' THEN (
			CASE month(@STITarih-2)  
			WHEN 1 THEN  (CASE UcAylikDonem1Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem1Oran AS DECIMAL(24,6)) END) 
			WHEN 2 THEN  (CASE UcAylikDonem1Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem1Oran AS DECIMAL(24,6)) END) 
			WHEN 3 THEN  (CASE UcAylikDonem1Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem1Oran AS DECIMAL(24,6)) END) 
			WHEN 4 THEN  (CASE UcAylikDonem2Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem2Oran AS DECIMAL(24,6)) END) 
			WHEN 5 THEN  (CASE UcAylikDonem2Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem2Oran AS DECIMAL(24,6)) END)  
			WHEN 6 THEN  (CASE UcAylikDonem2Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem2Oran AS DECIMAL(24,6)) END)  
			WHEN 7 THEN  (CASE UcAylikDonem3Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem3Oran AS DECIMAL(24,6)) END) 
			WHEN 8 THEN  (CASE UcAylikDonem3Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem3Oran AS DECIMAL(24,6)) END) 
			WHEN 9 THEN  (CASE UcAylikDonem3Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem3Oran AS DECIMAL(24,6)) END) 
			WHEN 10 THEN (CASE UcAylikDonem4Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem4Oran AS DECIMAL(24,6)) END) 
			WHEN 11 THEN (CASE UcAylikDonem4Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem4Oran AS DECIMAL(24,6)) END) 
			WHEN 12 THEN (CASE UcAylikDonem4Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem4Oran AS DECIMAL(24,6)) END)
			END
			)
			WHEN 'Aylık' THEN(
			CASE month(@STITarih-2)  
			WHEN 1 THEN  (CASE OcakOran    WHEN '' THEN 0.0 ELSE CAST(OcakOran    AS DECIMAL(24,6))  END) 
			WHEN 2 THEN  (CASE SubatOran   WHEN '' THEN 0.0 ELSE CAST(SubatOran   AS DECIMAL(24,6))  END) 
			WHEN 3 THEN  (CASE MartOran    WHEN '' THEN 0.0 ELSE CAST(MartOran    AS DECIMAL(24,6))  END) 
			WHEN 4 THEN  (CASE NisanOran   WHEN '' THEN 0.0 ELSE CAST(NisanOran   AS DECIMAL(24,6))  END)
			WHEN 5 THEN  (CASE MayisOran   WHEN '' THEN 0.0 ELSE CAST(MayisOran   AS DECIMAL(24,6))  END) 
			WHEN 6 THEN  (CASE HaziranOran WHEN '' THEN 0.0 ELSE CAST(HaziranOran AS DECIMAL(24,6))  END) 
			WHEN 7 THEN  (CASE TemmuzOran  WHEN '' THEN 0.0 ELSE CAST(TemmuzOran  AS DECIMAL(24,6))  END) 
			WHEN 8 THEN  (CASE AgustosOran WHEN '' THEN 0.0 ELSE CAST(AgustosOran AS DECIMAL(24,6))  END) 
			WHEN 9 THEN  (CASE EylulOran   WHEN '' THEN 0.0 ELSE CAST(EylulOran   AS DECIMAL(24,6))  END) 
			WHEN 10 THEN (CASE EkimOran    WHEN '' THEN 0.0 ELSE CAST(EkimOran    AS DECIMAL(24,6))  END) 
			WHEN 11 THEN (CASE KasimOran   WHEN '' THEN 0.0 ELSE CAST(KasimOran   AS DECIMAL(24,6))  END) 
			WHEN 12 THEN (CASE AralikOran  WHEN '' THEN 0.0 ELSE CAST(AralikOran  AS DECIMAL(24,6))  END) END
			)
			ELSE
			0.0
			END)AS Prim
			FROM FINSAT633.FINSAT633.Primler (NOLOCK) PR WHERE PR.PrimKodu in(@PrimKodu) 

			--Select
			--(CASE WHEN Donemi='Yıllık' THEN 
			--(CASE YillikOran WHEN '' THEN 0 ELSE CAST(YillikOran AS DECIMAL(24,6)) END)
			--ELSE 
			--(CASE WHEN Donemi='3 Aylık' THEN 
			--(CASE month(@STITarih-2)  
			--WHEN 1 THEN  (CASE UcAylikDonem1Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem1Oran AS DECIMAL(24,6)) END) 
			--WHEN 2 THEN  (CASE UcAylikDonem1Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem1Oran AS DECIMAL(24,6)) END) 
			--WHEN 3 THEN  (CASE UcAylikDonem1Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem1Oran AS DECIMAL(24,6)) END) 
			--WHEN 4 THEN  (CASE UcAylikDonem2Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem2Oran AS DECIMAL(24,6)) END) 
			--WHEN 5 THEN  (CASE UcAylikDonem2Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem2Oran AS DECIMAL(24,6)) END)  
			--WHEN 6 THEN  (CASE UcAylikDonem2Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem2Oran AS DECIMAL(24,6)) END)  
			--WHEN 7 THEN  (CASE UcAylikDonem3Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem3Oran AS DECIMAL(24,6)) END) 
			--WHEN 8 THEN  (CASE UcAylikDonem3Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem3Oran AS DECIMAL(24,6)) END) 
			--WHEN 9 THEN  (CASE UcAylikDonem3Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem3Oran AS DECIMAL(24,6)) END) 
			--WHEN 10 THEN (CASE UcAylikDonem4Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem4Oran AS DECIMAL(24,6)) END) 
			--WHEN 11 THEN (CASE UcAylikDonem4Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem4Oran AS DECIMAL(24,6)) END) 
			--WHEN 12 THEN (CASE UcAylikDonem4Oran WHEN '' THEN 0.0 ELSE CAST(UcAylikDonem4Oran AS DECIMAL(24,6)) END)
			--ELSE 
			--(CASE month(@STITarih-2)  
			--WHEN 1 THEN  (CASE OcakOran    WHEN '' THEN 0.0 ELSE CAST(OcakOran    AS DECIMAL(24,6))  END) 
			--WHEN 2 THEN  (CASE SubatOran   WHEN '' THEN 0.0 ELSE CAST(SubatOran   AS DECIMAL(24,6))  END) 
			--WHEN 3 THEN  (CASE MartOran    WHEN '' THEN 0.0 ELSE CAST(MartOran    AS DECIMAL(24,6))  END) 
			--WHEN 4 THEN  (CASE NisanOran   WHEN '' THEN 0.0 ELSE CAST(NisanOran   AS DECIMAL(24,6))  END)
			--WHEN 5 THEN  (CASE MayisOran   WHEN '' THEN 0.0 ELSE CAST(MayisOran   AS DECIMAL(24,6))  END) 
			--WHEN 6 THEN  (CASE HaziranOran WHEN '' THEN 0.0 ELSE CAST(HaziranOran AS DECIMAL(24,6))  END) 
			--WHEN 7 THEN  (CASE TemmuzOran  WHEN '' THEN 0.0 ELSE CAST(TemmuzOran  AS DECIMAL(24,6))  END) 
			--WHEN 8 THEN  (CASE AgustosOran WHEN '' THEN 0.0 ELSE CAST(AgustosOran AS DECIMAL(24,6))  END) 
			--WHEN 9 THEN  (CASE EylulOran   WHEN '' THEN 0.0 ELSE CAST(EylulOran   AS DECIMAL(24,6))  END) 
			--WHEN 10 THEN (CASE EkimOran    WHEN '' THEN 0.0 ELSE CAST(EkimOran    AS DECIMAL(24,6))  END) 
			--WHEN 11 THEN (CASE KasimOran   WHEN '' THEN 0.0 ELSE CAST(KasimOran   AS DECIMAL(24,6))  END) 
			--WHEN 12 THEN (CASE AralikOran  WHEN '' THEN 0.0 ELSE CAST(AralikOran  AS DECIMAL(24,6))  END) 
			--ELSE 0.0 END )
			--END) 
			--ELSE 0.0 END)
			--END
			--) AS Prim
			--FROM FINSAT633.FINSAT633.Primler (NOLOCK) PR WHERE PR.PrimKodu in(@PrimKodu)

			OPEN CURPRIM
			FETCH NEXT FROM CURPRIM INTO @Prim
			WHILE @@FETCH_STATUS =0
				BEGIN
				Set @PrimOran +=@Prim
				FETCH NEXT FROM CURPRIM INTO @Prim
				END

			CLOSE CURPRIM
			DEALLOCATE CURPRIM 
		FETCH NEXT FROM CURPRIMKODU INTO @PrimKodu
		END

		CLOSE CURPRIMKODU
		DEALLOCATE CURPRIMKODU
	END

RETURN  (@PrimOran)
END

GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[Split]
(
	@RowData nvarchar(2000),
	@SplitOn nvarchar(5)
)  
RETURNS @RtnValue table 
(
	Id int identity(1,1),
	Data nvarchar(100)
) 
AS  
BEGIN 
	Declare @Cnt int
	Set @Cnt = 1

	While (Charindex(@SplitOn,@RowData)>0)
	Begin
		Insert Into @RtnValue (data)
		Select 
			Data = ltrim(rtrim(Substring(@RowData,1,Charindex(@SplitOn,@RowData)-1)))

		Set @RowData = Substring(@RowData,Charindex(@SplitOn,@RowData)+1,len(@RowData))
		Set @Cnt = @Cnt + 1
	End
	
	Insert Into @RtnValue (data)
	Select Data = ltrim(rtrim(@RowData))

	Return
END
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 05/04/2018 10:33:59 ******/
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
/****** Object:  UserDefinedFunction [FINSAT633].[fncMiktarToBirimMiktar]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE FUNCTION [FINSAT633].[fncMiktarToBirimMiktar]
(
	@Miktar DECIMAL(24, 6),
	@Birim VARCHAR(MAX),
	@Birim1 VARCHAR(MAX),
	@Birim2 VARCHAR(MAX),
	@Birim3 VARCHAR(MAX),
	@Operator2 SMALLINT,
	@Operator3 SMALLINT,
	@Katsayi2 DECIMAL(24, 6),
	@Katsayi3 DECIMAL(24, 6)
)
RETURNS DECIMAL(24, 6)
AS
BEGIN
	
	DECLARE @Result DECIMAL(24, 6)

	--Birim Miktar 0 mı Kontrolü
	IF( @Miktar = 0 )
	BEGIN
		SET @Result = @Miktar
		RETURN @Result
	END

	--Gönderilen Birim, Birim1 e Eşitse
	IF( @Birim = @Birim1 )
	BEGIN
		SET @Result = @Miktar
		RETURN @Result
	END

	--Gönderilen Birim, Birim2 ye Eşitse
	IF( @Birim = @Birim2 )
	BEGIN
		
		IF( @Operator2 = 0 )
		BEGIN
			SET @Result = @Miktar * @Katsayi2
			RETURN @Result
		END
		ELSE IF( @Operator2 = 1 )
		BEGIN
			SET @Result = @Miktar / CASE @Katsayi2 WHEN 0 THEN 1 ELSE @Katsayi2 END
			RETURN @Result
		END

	END

	--Gönderilen Birim, Birim3 e Eşitse
	IF( @Birim = @Birim3 )
	BEGIN
		
		IF( @Operator3 = 0 )
		BEGIN
			SET @Result = @Miktar * @Katsayi3
			RETURN @Result
		END
		ELSE IF( @Operator3 = 1 )
		BEGIN
			SET @Result = @Miktar / CASE @Katsayi3 WHEN 0 THEN 1 ELSE @Katsayi3 END
			RETURN @Result
		END

	END

	RETURN @Result

END







GO
/****** Object:  UserDefinedFunction [wms].[fnGetBarkods]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Derviş Mehmet Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 02.10.2017
-- Modify date: 20.12.2017
-- Description:	mal koduna göre Barkod Numaralarını getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetBarkods]
(
	@MalKodu varchar(30)
)
RETURNS varchar(150)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(150)

	-- Add the T-SQL statements to compute the return value here
	SELECT        @Result = ';' +  Barkod1 + ';' + Barkod2  + ';' + Barkod3  + ';'
	FROM            FINSAT633.FINSAT633.STK WITH (NOLOCK)
	WHERE        (MalKodu = @MalKodu)

	-- Return the result of the function
	RETURN @Result

END

GO
/****** Object:  UserDefinedFunction [wms].[fnGetMalAdi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 20.12.2017
-- Modify date: 20.12.2017
-- Description:	mal koduna göre mal adını getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetMalAdi]
(
	@MalKodu varchar(30)
)
RETURNS varchar(150)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(150)

	-- Add the T-SQL statements to compute the return value here
	SELECT        @Result = MalAdi
	FROM            FINSAT633.FINSAT633.STK WITH (NOLOCK)
	WHERE        (MalKodu = @MalKodu)

	-- Return the result of the function
	RETURN @Result

END

GO
/****** Object:  UserDefinedFunction [wms].[fnGetSatislarHesabi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Derviş Mehmet Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 02.10.2017
-- Modify date: 20.12.2017
-- Description:	mal koduna göre SatislarHesabi getirir
-- =============================================
CREATE FUNCTION [wms].[fnGetSatislarHesabi]
(
	@MalKodu varchar(30)
)
RETURNS varchar(150)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result varchar(150)

	-- Add the T-SQL statements to compute the return value here
	SELECT        @Result = SatislarHesabi
	FROM            FINSAT633.FINSAT633.STK WITH (NOLOCK)
	WHERE        (MalKodu = @MalKodu)

	-- Return the result of the function
	RETURN @Result

END

GO
/****** Object:  UserDefinedFunction [wms].[GetOrtGunKod3OrtBakiyeGun]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [wms].[GetOrtGunKod3OrtBakiyeGun] (@HesapKodu varchar(20),@CHKKod2 varchar(20))
RETURNS @RtnValue table 
(
	Kod3OrtBakiye DECIMAL(24, 6),
	Kod3OrtGun DECIMAL(24, 6),
	OrtalamaGun DECIMAL(24, 6)
) 
AS
BEGIN
DECLARE @OrtalamaGun DECIMAL(24, 6) = 0
DECLARE @Kod3OrtGun DECIMAL(24, 6) = 0
DECLARE @Kod3OrtBakiye DECIMAL(24, 6) = 0
DECLARE @Gun_FaturaTutar DECIMAL(24, 6) = 0
DECLARE @FaturaTop DECIMAL(24, 6) = 0
--DECLARE @Gun DECIMAL(24, 6) = 0
DECLARE @Gun INT = 0
DECLARE @FaturaTutar DECIMAL(24, 6) = 0
DECLARE @Bakiye DECIMAL(24, 6) = 0
DECLARE @Alacak DECIMAL(24, 6) = 0
DECLARE @OrtGunFaturaTutar DECIMAL(24, 6) = 0
DECLARE @OrtGunFaturaTop DECIMAL(24, 6) = 0
DECLARE @Alacak2 DECIMAL(24, 6) = 0
DECLARE @Gun_OrtGun_FaturaTutar DECIMAL(24, 6) = 0
DECLARE @ChkIlkKarakter VARCHAR(1)=''
DECLARE @BakilacakGun INT=0
DECLARE @ChkIlk INT

IF @HesapKodu IS NULL
BEGIN
	INSERT INTO @RtnValue(Kod3OrtBakiye, Kod3OrtGun, OrtalamaGun) VALUES(0, 0, 0)
	RETURN
END

SET @ChkIlkKarakter = SUBSTRING(@HesapKodu,1,1)

IF ISNUMERIC(@ChkIlkKarakter)=1
SET @ChkIlk = CONVERT(INT, @ChkIlkKarakter)

IF (@ChkIlk < 5 AND @ChkIlk > 0)
     SET @BakilacakGun = 20
ELSE IF (@ChkIlk = 5 OR @ChkIlk = 6)
     SET @BakilacakGun = 24
ELSE IF (@ChkIlk = 9)
     SET @BakilacakGun = 20

IF  CHARINDEX('1',@CHKKod2) > 0
     SET @BakilacakGun = 59


DECLARE CRS_PERSONEL CURSOR FOR
select  
tarih,
Borc,
Alacak,
Tutar,
DATEDIFF(dd,0,GETDATE()+2) - tarih AS Gun
from 
(   select	
	tarih,
	Borc,
	Alacak,
	Tutar,
	SRT,EvrakNo
	from 
	(   select 
		A.*,  
		Case A.BA when B.BorA Then 0 Else 1 End  as st 
		from 
		(   select 
			1 as AyYil,
			A.hesapkodu, 
			A.hesapkodu+'                              1' as SRT,
			ISNULL(B.tarih,0 )  as Tarih ,
			ISNULL(B.EvrakNo,' ' )  as EvrakNo,
			ISNULL(B.BA,0 )  as BA,
			ISNULL(B.Aciklama,0 )  as Aciklama,
			ISNULL(B.VadeTarih,0 )  as VadeTarih,
			ISNULL(A.Unvan1,' ' )  as Unvan,
			Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
			Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
			ISNULL(B.Tutar,0 )  as Tutar, 
			Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
			Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
			ISNULL(B.TLTutar,0 )  as TLTutar,  
			Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
			Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
			ISNULL(B.YTLTutar,0 )  as YTLTutar, 
			ISNULL(A.OzelKod,' ' )  as OzelKod,
			ISNULL(A.TipKod,' ' )  as TipKod,
			ISNULL(A.GrupKod,' ' )  as grupkod,
			ISNULL(A.BolgeKod,' ' )  as BolgeKod,
			ISNULL(A.KartTip,0 )  as Karttip,
			ISNULL(B.DovizKuru,0 )  as DovizKuru, 
			ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
			DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
			ISNULL(B.Islemtip,0 )  as Islemtip, 
			(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
			opsiyonGunu,
			A.Kod1,
			A.Kod2,
			A.Kod3,
			A.Kod4,
			A.Kod5,
			A.Kod6,
			A.Kod7,
			A.Kod8,
			A.Kod9,
			A.Kod10,
			A.Kod11,
			A.Kod12,
			A.Kod13,
			A.Notlar,
			MhsKod 
			from 
			(	select 
				A.hesapkodu,
				tarih,
				A.EvrakNo,
				min(A.BA) as BA,
				min(A.Aciklama) as Aciklama,
				VadeTarih,
				Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip					when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
				Sum(A.DovizTutar) as DovizTutar,
				min(A.DovizKuru) as DovizKuru,
				min(A.DovizCinsi) as DovizCinsi,
				Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case							B.Islemtip when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
				Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
				min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
				from 
				(   select 
					* 
					from 
					(	select 
						hesapkodu,
						tarih,
						EvrakNo,
						BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where   BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
						and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
						and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 

						UNION ALL 

						select 
						hesapkodu,
						tarih,
						EvrakNo,
						BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
						And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
						and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
						
						UNION ALL 
						
						select 
						KarsiHesapKodu as HesapKodu,
						tarih,
						EvrakNo,
						1 as BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
						and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu 
						and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
						UNION ALL 
						
						select 
						KarsiHesapKodu as HesapKodu,
						tarih,
						EvrakNo,
						0 as BA,
						Aciklama, 
						Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
						Tutar,
						DovizTutar,
						DovizKuru,
						A.DovizCinsi,
						Islemtip,
						KynkEvraktip,
						round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
						round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu and						KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
					) A 
					where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 )						And  IslemTip in 
					(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
				) A 
				left join 
				(	select 
					hesapkodu,
					BA,
					sum(A.Tutar) as Tutar,
					KynkEvraktip,
					Evrakno, 
					DovizCinsi,  
					Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
					sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
					sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 
					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				
					UNION ALL 

					select 
					hesapkodu,
					BA,
					sum(A.Tutar) as Tutar,
					KynkEvraktip,
					Evrakno, 
					DovizCinsi,  
					Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
					sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
					sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1 
					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi =							   B.DovizCinsi  
				group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
			) B ,
			FINSAT633.FINSAT633.CHK A (NoLock)  
			where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA +			DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and A.BolgeKod<='ZZZZ'			and A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and A.OzelKod<='ZZZZZ'  and			A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and				A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and			A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'  and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and			A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
		) A 
		, 
		(	select 
			hesapkodu, 
			Case Sign(SUM(YTLborc)-sum(YTLalacak)) when 1 then 0 when -1 then 1 when 0 then -2 else -3 End  as BorA 
			from 
			(	select 
				1 as AyYil,
				A.hesapkodu, 
				A.hesapkodu+'                              1' as SRT,
				ISNULL(B.tarih,0 )  as Tarih ,
				ISNULL(B.EvrakNo,' ' )  as EvrakNo,
				ISNULL(B.BA,0 )  as BA,
				ISNULL(B.Aciklama,0 )  as Aciklama,
				ISNULL(B.VadeTarih,0 )  as VadeTarih,
				ISNULL(A.Unvan1,' ' )  as Unvan,
				Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
				Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
				ISNULL(B.Tutar,0 )  as Tutar, 
				Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
				Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
				ISNULL(B.TLTutar,0 )  as TLTutar, 
				Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
				Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
				ISNULL(B.YTLTutar,0 )  as YTLTutar, 
				ISNULL(A.OzelKod,' ' )  as OzelKod,
				ISNULL(A.TipKod,' ' )  as TipKod,
				ISNULL(A.GrupKod,' ' )  as grupkod,
				ISNULL(A.BolgeKod,' ' )  as BolgeKod,
				ISNULL(A.KartTip,0 )  as Karttip,
				ISNULL(B.DovizKuru,0 )  as DovizKuru, 
				ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
				DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
				ISNULL(B.Islemtip,0 )  as Islemtip, 
				(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
				opsiyonGunu,
				A.Kod1,
				A.Kod2,
				A.Kod3,
				A.Kod4,
				A.Kod5,
				A.Kod6,
				A.Kod7,
				A.Kod8,
				A.Kod9,
				A.Kod10,
				A.Kod11,
				A.Kod12,
				A.Kod13,
				A.Notlar,
				MhsKod 
				from 
				(	select 
					A.hesapkodu,
					tarih,
					A.EvrakNo,
					min(A.BA) as BA,
					min(A.Aciklama) as Aciklama,
					VadeTarih,
					Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case								B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
					Sum(A.DovizTutar) as DovizTutar,
					min(A.DovizKuru) as DovizKuru,
					min(A.DovizCinsi) as DovizCinsi,
					Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case							B.Islemtip when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
					Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
					min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
					from 
					(	select 
						* 
						from 
						(	select 
							hesapkodu,
							tarih,
							EvrakNo,
							BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where   BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
							and  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
							and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 

							UNION ALL 
							
							select 
							hesapkodu,
							tarih,
							EvrakNo,
							BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip, 
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' 
							And  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
							and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
							
							UNION ALL 
							
							select 
							KarsiHesapKodu as HesapKodu,
							tarih,EvrakNo,
							1 as BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null and								KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu 
							and  IslemTip in																															(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
							UNION ALL 
							
							select 
							KarsiHesapKodu as HesapKodu,
							tarih,
							EvrakNo,
							0 as BA,
							Aciklama, 
							Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as										vadetarih ,
							Tutar,
							DovizTutar,
							DovizKuru,
							A.DovizCinsi,
							Islemtip,
							KynkEvraktip,
							round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
							round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
							from FINSAT633.FINSAT633.CHI A  (NoLock) 
							where  BA=1 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ' and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu							and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null 
						) A 
						where (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 )
						And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
					) A 
					left join 
					(   select 
						hesapkodu,
						BA,
						sum(A.Tutar) as Tutar,
						KynkEvraktip,
						Evrakno, 
						DovizCinsi,  
						Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
						sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
						sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 
						group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
						
						UNION ALL 
						
						select hesapkodu,
						BA,
						sum(A.Tutar) as Tutar,
						KynkEvraktip,
						Evrakno, 
						DovizCinsi,  
						Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
						sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
						sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
						from FINSAT633.FINSAT633.CHI A  (NoLock) 
						where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1 
						group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End
					) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi =								B.DovizCinsi  
					group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
				) B ,
				FINSAT633.FINSAT633.CHK A (NoLock) 
			    where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA +					KDVA + DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and						A.BolgeKod<='ZZZZ'  and A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and				A.OzelKod<='ZZZZZ'  and A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and							A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and						A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'				and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
			) A  
			group by hesapkodu 
		) B 
		where A.hesapkodu=B.hesapkodu 
	) A 
	where st=0 
	
	UNION 
	
	select 
	0 as tarih,
	Sum(A.Borc) as Borc,
	Sum(A.Alacak) as Alacak, 
	sum(A.Tutar) as Tutar,
	A.hesapkodu+'                              0' as SRT,
	' ' as Evrakno
	from 
	(	select 
		1 as AyYil,
		A.hesapkodu, 
		A.hesapkodu+'                              1' as SRT,
		ISNULL(B.tarih,0 )  as Tarih ,
		ISNULL(B.EvrakNo,' ' )  as EvrakNo,
		ISNULL(B.BA,0 )  as BA,
		ISNULL(B.Aciklama,0 )  as Aciklama,
		ISNULL(B.VadeTarih,0 )  as VadeTarih,
		ISNULL(A.Unvan1,' ' )  as Unvan,
		Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,
		Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,
		ISNULL(B.Tutar,0 )  as Tutar, Case B.BA when 0 Then B.TLTutar Else 0 End  as TLBorc,
		Case B.BA when 1 Then B.TLTutar Else 0 End  as TLAlacak,
		ISNULL(B.TLTutar,0 )  as TLTutar, 
		Case B.BA when 0 Then B.YTLTutar Else 0 End  as YTLBorc,
		Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak,
		ISNULL(B.YTLTutar,0 )  as YTLTutar, 
		ISNULL(A.OzelKod,' ' )  as OzelKod,
		ISNULL(A.TipKod,' ' )  as TipKod,
		ISNULL(A.GrupKod,' ' )  as grupkod,
		ISNULL(A.BolgeKod,' ' )  as BolgeKod,
		ISNULL(A.KartTip,0 )  as Karttip,
		ISNULL(B.DovizKuru,0 )  as DovizKuru, 
		ISNULL(B.DovizCinsi,' ' )  as DovizCinsi , 
		DATEDIFF(dd,0,GETDATE()+2)-B.Tarih as Gun ,
		ISNULL(B.Islemtip,0 )  as Islemtip, 
		(DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA) as Bakiye,
		opsiyonGunu,
		A.Kod1,
		A.Kod2,
		A.Kod3,
		A.Kod4,
		A.Kod5,
		A.Kod6,
		A.Kod7,
		A.Kod8,
		A.Kod9,
		A.Kod10,
		A.Kod11,
		A.Kod12,
		A.Kod13,
		A.Notlar,
		MhsKod 
		from 
		(	select 
			A.hesapkodu,
			tarih,
			A.EvrakNo,
			min(A.BA) as BA,
			min(A.Aciklama) as Aciklama,
			VadeTarih,
			Sum(A.Tutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip					when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar,
			Sum(A.DovizTutar) as DovizTutar,
			min(A.DovizKuru) as DovizKuru,
			min(A.DovizCinsi) as DovizCinsi,
			Sum(A.TLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.TLtutar,0 )  Else 0 End  when 8 then  Case B.Islemtip				when 9 Then ISNULL(B.TLtutar,0 )  Else 0 End  else 0 End ) as TLTutar,
			Sum(A.YTLTutar- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case						B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar,
			min(case when A.KynkEvrakTip in (118,119,139) and A.IslemTip = 6 then 48 else A.Islemtip end) as Islemtip  
			from 
			(	select 
				* 
				from 
				(	select 
					hesapkodu,
					tarih,
					EvrakNo,
					BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip,
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where   BA=0    and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and								A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ'  
					and  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
					
					UNION ALL 
					
					select 
					hesapkodu,
					tarih,
					EvrakNo,
					BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar 
					from FINSAT633.FINSAT633.CHI A  (NoLock)  
					where  BA=1   and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and									A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and								A.Kod6<='ZZZZZ'  and A.Kod7>=' '  and A.Kod7<='ZZZZZ'  
					And  IslemTip in																																(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					and Hesapkodu >=@HesapKodu and Hesapkodu <=@HesapKodu 
					
					UNION ALL 
					
					select 
					KarsiHesapKodu as HesapKodu,
					tarih,
					EvrakNo,
					1 as BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar  
					from FINSAT633.FINSAT633.CHI A  (NoLock)  
					where  BA=0 and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'					and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and A.Kod6<='ZZZZZ'  and							A.Kod7>=' '  and A.Kod7<='ZZZZZ'  and KarsiHesapKodu <> ' ' and KarsiHesapKodu is not null  and KarsiHesapKodu >=@HesapKodu						and KarsiHesapKodu <=@HesapKodu 
					and  IslemTip in																																	(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)

					UNION ALL 
					
					select 
					KarsiHesapKodu as HesapKodu,
					tarih,
					EvrakNo,
					0 as BA,
					Aciklama, 
					Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,
					Tutar,
					DovizTutar,
					DovizKuru,
					A.DovizCinsi,
					Islemtip,
					KynkEvraktip, 
					round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2) as TLTutar, 
					round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar  
					from FINSAT633.FINSAT633.CHI A  (NoLock) 
					where  BA=1  and  A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'					and A.Kod4>=' '  and A.Kod4<='ZZZZZ'  and A.Kod5>=' '  and A.Kod5<='ZZZZZ'  and A.Kod6>=' '  and A.Kod6<='ZZZZZ'  and							A.Kod7>=' '  and A.Kod7<='ZZZZZ'  and KarsiHesapKodu >=@HesapKodu and KarsiHesapKodu <=@HesapKodu and KarsiHesapKodu <> ' '						and KarsiHesapKodu is not null 
				) A 
				where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) 
				And  IslemTip in																																	(0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
			) A 
			left join 
			(	select 
				hesapkodu,
				BA,
				sum(A.Tutar) as Tutar,
				KynkEvraktip,
				Evrakno, 
				DovizCinsi,  
				Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
				sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
				sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar 
				from FINSAT633.FINSAT633.CHI A  (NoLock)  
				where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0  
				group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
				
				UNION ALL  
				
				select 
				hesapkodu,
				BA,
				sum(A.Tutar) as Tutar,
				KynkEvraktip,
				Evrakno, 
				DovizCinsi, 
			    Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,
				sum( round(Case when Tarih>=38353 then Tutar*1000000 else Tutar End, 2)) as TLTutar,
				sum( round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2) ) as YTLTutar  
				from FINSAT633.FINSAT633.CHI A  (NoLock)  where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  and Tarih>=-1					group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
			) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi			group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
		) B ,
		FINSAT633.FINSAT633.CHK A (NoLock)  
		where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA +			DigerA))<=999999999999999999 and  A.hesapkodu>=@HesapKodu  and A.hesapkodu<=@HesapKodu   and A.BolgeKod>=' '  and A.BolgeKod<='ZZZZ'  and		A.GrupKod>=' '  and A.GrupKod<='ZZZZZ'  and A.TipKod>=' '  and A.TipKod<='ZZZZZ'  and A.OzelKod>=' '  and A.OzelKod<='ZZZZZ'  and				A.Kod1>=' '  and A.Kod1<='ZZZZZ'  and A.Kod2>=' '  and A.Kod2<='ZZZZZ'  and A.Kod3>=' '  and A.Kod3<='ZZZZZ'  and A.Kod4>=' '  and				A.Kod4<='ZZZZZ'  and A.Kod5<=999999999999999999 and A.Kod6<=999999999999999999 and A.Kod7<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod8>=' '  and			A.Kod8<='ZZZZZZZZZZZZZZZZZZ'  and A.Kod9>=' '  and A.Kod9<='ZZZZZZZZZZZZZZZZZZ'  and A.MhsKod>=' ' and A.MhsKod<='ZZZZZZZZZZZZZZZ' and			A.Unvan1>=' ' and A.Unvan1<='ZZZZZZZZZZZZZZZ'
	) A  
	group by A.hesapkodu 
) A     
order by SRT,Tarih desc,EvrakNo desc

DECLARE @ALCK DECIMAL(24, 6) = 0
DECLARE @BRC DECIMAL(24, 6) = 0
DECLARE @TTR DECIMAL(24, 6) = 0
--DECLARE @TRH DECIMAL(24, 6) = 0
DECLARE @TRH INT = 0

DECLARE @aa INT=0


OPEN CRS_PERSONEL

FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@BRC,@ALCK,@TTR,@GUN

SET @Bakiye = @BRC- @ALCK
SET @Alacak = @Bakiye
SET @Alacak2 = @Bakiye

FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@ALCK,@BRC,@TTR, @GUN


WHILE @@FETCH_STATUS =0
	BEGIN
	
		SET @Gun = DATEDIFF(dd,0,GETDATE()+2) - @TRH
        SET @FaturaTutar = @TTR
        SET @OrtGunFaturaTutar = @TTR

		IF (@Alacak > 0)
        BEGIN   
                 
            IF (@Alacak > @FaturaTutar OR @Alacak = @FaturaTutar)
            BEGIN
           
                SET @Alacak = @Alacak-@FaturaTutar

                IF (@Gun > @BakilacakGun)
                BEGIN
                 SET @aa =124
                    SET @FaturaTop = @FaturaTop + @FaturaTutar
                    SET @Gun_FaturaTutar = @Gun_FaturaTutar + (@Gun * @FaturaTutar)
                END
            END
            ELSE
            BEGIN
                IF (@Gun > @BakilacakGun)
                BEGIN
                
                    SET @FaturaTop = @FaturaTop + @Alacak
                    SET @Gun_FaturaTutar =@Gun_FaturaTutar + (@Gun * (@Alacak))
                END
                SET @Alacak = 0;
            END
        END



		IF (@Alacak2 > 0)
        BEGIN
            IF (@Alacak2 > @OrtGunFaturaTutar OR @Alacak2 = @OrtGunFaturaTutar)
            BEGIN
                SET @Alacak2 = @Alacak2 - @OrtGunFaturaTutar

                SET @OrtGunFaturaTop = @OrtGunFaturaTop + @OrtGunFaturaTutar
                SET @Gun_OrtGun_FaturaTutar = @Gun_OrtGun_FaturaTutar + (@Gun * @OrtGunFaturaTutar)
            END
            ELSE
            BEGIN

                SET @OrtGunFaturaTop = @OrtGunFaturaTop + @Alacak2
                SET @Gun_OrtGun_FaturaTutar = @Gun_OrtGun_FaturaTutar + (@Gun * (@Alacak2))
                SET @Alacak2 = 0
            END
        END

		FETCH NEXT FROM CRS_PERSONEL INTO @TRH,@ALCK,@BRC,@TTR, @GUN
 
	END

CLOSE CRS_PERSONEL

DEALLOCATE CRS_PERSONEL




IF (@Bakiye> 0)
SET @Kod3OrtBakiye = @Kod3OrtBakiye +  @FaturaTop

IF(@FaturaTop=0)
SET @FaturaTop=1

IF(@OrtGunFaturaTop=0)
SET @OrtGunFaturaTop=1

IF (@Gun_FaturaTutar > 0)
SET @Kod3OrtGun = @Kod3OrtGun + (@Gun_FaturaTutar / @FaturaTop)

IF (@Gun_OrtGun_FaturaTutar> 0)
SET @OrtalamaGun = @OrtalamaGun +  (@Gun_OrtGun_FaturaTutar / @OrtGunFaturaTop)

INSERT INTO @RtnValue(Kod3OrtBakiye, Kod3OrtGun, OrtalamaGun) VALUES(@Kod3OrtBakiye, @Kod3OrtGun, @OrtalamaGun)
RETURN
END










GO
/****** Object:  UserDefinedFunction [wms].[GetSCekTCekPRBakiye]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE FUNCTION [wms].[GetSCekTCekPRBakiye] (@HesapKodu varchar(20))
RETURNS @RtnValue table 
(
	SCek DECIMAL(24, 6),
	TCek DECIMAL(24, 6),
	PRBakiye DECIMAL(24, 6)
) 
AS
BEGIN
DECLARE @SahsiCek DECIMAL(24, 6) = 0
DECLARE @PRBakiye DECIMAL(24, 6) = 0 
DECLARE @ToplamCek DECIMAL(24, 6) = 0 
DECLARE @KrediLimiti DECIMAL(24, 6) = 0 
DECLARE @MCek DECIMAL(24, 6) = 0 
IF @HesapKodu IS NULL
BEGIN
	INSERT INTO @RtnValue(SCek, TCek, PRBakiye) VALUES(0, 0, 0)
	RETURN
END

SELECT top(1) 
@KrediLimiti= isnull(max([Kredi Limiti]),0), 
@PRBakiye = isnull(max([PRBakiye]),0),
@SahsiCek= isnull(max([Şahsi Çek]),0),
@MCek = isnull(max([Müşteri Çek]),0) 
FROM 
(	SELECT 
	alsana.[Ünvan],
	SUM(alsana.[Kredi Limiti]) as [Kredi Limiti] ,
	SUM(alsana.Bakiye) as [Bakiye] ,
	SUM(alsana.PRBakiye) as [PRBakiye] ,
	SUM(alsana.[Şahsi Çek]) as [Şahsi Çek] ,
	SUM(alsana.[Müşteri Çek]) as [Müşteri Çek] 
	FROM 
	(	
		SELECT 
		c.unvan as [Ünvan],
		SUM(c.kredilimiti) as [Kredi Limiti],
		SUM(c.bakiye) as Bakiye,
		SUM(c.PRBakiye)as PRBakiye,
		
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1='Ş' AND h.SonIslemTip NOT IN ('5','12','13') AND h.vadetarih>=getdate()-10 THEN h.tutar ELSE 0 END),0) 
		FROM  FINSAT633.FINSAT633.ACK h (nolock) 
		where h.veren=c.hesapkodu AND c.hesapkodu not like '%PR'
		)
		-
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1='Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10AND CHI.Islemtip=21 AND CHI.BA=1 THEN			CHI.tutar ELSE 0 END),0)  
		FROM  FINSAT633.FINSAT633.ACK h (nolock),
		FINSAT633.FINSAT633.CHI CHI (NOLOCK) 
		where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) AND h.veren=c.hesapkodu AND c.hesapkodu not like '%PR'AND								H.EVRAKNO=CHI.EVRAKNO
		)   as[Şahsi Çek] ,
		
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1<>'Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 THEN h.tutar ELSE 0 END),0)  
		FROM  FINSAT633.FINSAT633.ACK h (nolock) 
		where h.veren=c.hesapkodu AND c.hesapkodu not like '%PR'
		)
		-
		(SELECT 
		ISNULL(SUM(CASE WHEN h.kod1<>'Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 AND CHI.Islemtip=21 AND CHI.BA=1				THEN CHI.tutar ELSE 0 END),0)  
		FROM  FINSAT633.FINSAT633.ACK h (nolock),
		FINSAT633.FINSAT633.CHI CHI (NOLOCK) 
		where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) AND h.veren=c.hesapkodu AND c.hesapkodu not like '%PR' AND								H.EVRAKNO=CHI.EVRAKNO
		) as[Müşteri Çek] 
		
		FROM 
		(	SELECT 
			d.HesapKodu as hesapkodu,
			d.unvan,
			ISNULL(CASE WHEN d.hesapkodu=@HesapKodu+'PR' THEN 0 ELSE ISNULL(SUM(d.kredilimiti),0) END ,0) as kredilimiti,
			SUM(d.bakiye) as bakiye,
			SUM(d.PRBakiye) as PRbakiye 
			FROM 
			(	SELECT 
				b.hesapkodu,
				b.unvan1+' '+b.unvan2 as unvan,  
				b.kredilimiti ,
				CASE WHEN b.hesapkodu=@HesapKodu+'PR' THEN 0 ELSE SUM(CASE WHEN a.BA = 0 THEN ISNULL(a.Tutar,0) ELSE ISNULL(-a.Tutar,0) END) END					as Bakiye,
				SUM(CASE WHEN a.BA = 0 THEN ISNULL(a.[PR Tutar],0) ELSE ISNULL(-a.[PR Tutar],0) END) as PRBakiye 
				FROM  
				(	SELECT  
					S.KarsiHesapKodu as HesapKodu,
					f.ozelkod,
					f.hesapno, 
					ISNULL((CASE WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar END),0) as [PR Tutar],
					ISNULL((CASE WHEN S.IslemTip IN ('5','9')  THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar END),0) as Tutar,						ISNULL((CASE WHEN S.BA = 0 THEN 1 ELSE 0 END),'-1') as BA 
					FROM  FINSAT633.FINSAT633.CHI S (nolock)  
					RIGHT JOIN  FINSAT633.FINSAT633.CHK F  (NOLOCK) on S.KarsiHesapKodu=F.HesapKodu 
					where S.KarsiHesapKodu is not null AND S.KarsiHesapKodu <> '' AND S.IslemTip NOT IN (16,21,27,32,36,37,41,42)  
					
					UNION ALL  
					
					SELECT 
					S.HesapKodu as hesapkodu,
					f.ozelkod ,
					f.hesapno,
					ISNULL((CASE WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN -S.Tutar WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar END),0) as [PR Tutar],  
					ISNULL((CASE WHEN S.IslemTip IN ('5','9') THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar END),0) as Tutar  ,
					ISNULL(S.BA,'-1') as BA 
					FROM  FINSAT633.FINSAT633.CHI S (nolock) 
					RIGHT JOIN  FINSAT633.FINSAT633.CHK F (NOLOCK) on  S.HesapKodu=F.HesapKodu 
					where S.IslemTip not in (16,21,27,32,36,37,41,42) 
				) a   
				RIGHT JOIN   FINSAT633.FINSAT633.CHK b (nolock) ON b.hesapkodu=a.hesapkodu 
				where  (b.HesapKodu LIKE @HesapKodu+'%' AND  ( b.HesapKodu BETWEEN '1' AND '7' and SUBSTRING(b.HesapKodu,1,1)<> '7' 
				or b.HesapKodu between '9' AND '99' ))
				 GROUP BY b.hesapkodu,(b.unvan1+' '+b.unvan2),b.kredilimiti 
			) d GROUP BY d.unvan,d.hesapkodu 
		) c GROUP BY c.unvan,c.hesapkodu 
	) alsana 
	GROUP BY [Ünvan] 
) AS A

SET @ToplamCek= @SahsiCek + @MCek 

INSERT INTO @RtnValue(SCek, TCek, PRBakiye) VALUES(@SahsiCek, @ToplamCek, @PRBakiye)
RETURN
END










GO
/****** Object:  UserDefinedFunction [wms].[getStockByDepo]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 2017.09.12
-- Modify date: 2017.10.25
-- Description:	depo bazlı malın stoğunu getirir
-- =============================================
CREATE FUNCTION [wms].[getStockByDepo]
(
	@MalKodu varchar(30),
	@DepoKodu varchar(5)
)
RETURNS numeric(25, 6)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result numeric(25, 6)

	-- Add the T-SQL statements to compute the return value here
	SELECT        @Result = (ISNULL(FINSAT633.DST.DvrMiktar, 0) + ISNULL(FINSAT633.DST.GirMiktar, 0) - ISNULL(FINSAT633.DST.CikMiktar, 0))
	FROM            FINSAT633.STK WITH (NOLOCK)  LEFT OUTER JOIN
							 FINSAT633.DST WITH (NOLOCK) ON FINSAT633.STK.MalKodu = FINSAT633.DST.MalKodu AND FINSAT633.DST.Depo = @DepoKodu
	WHERE        (FINSAT633.STK.MalKodu = @MalKodu)

	-- Return the result of the function
	RETURN @Result

END

GO
/****** Object:  UserDefinedFunction [wms].[splitstring]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [wms].[splitstring] ( @stringToSplit VARCHAR(MAX) )
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
/****** Object:  UserDefinedFunction [wms].[fnSiparisList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 23.11.2017
-- Modify date: 23.11.2017
-- Description:	kablo veya genel sipariş
-- =============================================
CREATE FUNCTION [wms].[fnSiparisList]
(	
	@SirketKodu varchar(30),
	@DepoKodu varchar(5),
	@isKable bit,
	@BasTarih int,
	@BitTarih int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT        @SirketKodu AS SirketID, FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.Chk, FINSAT633.FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.FINSAT633.CHK.GrupKod, 
							 FINSAT633.FINSAT633.CHK.FaturaAdres3 AS FaturaAdres, FINSAT633.FINSAT633.MFK.Aciklama, COUNT(FINSAT633.FINSAT633.SPI.MalKodu) AS Çeşit, 
							 SUM(FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar) AS Miktar, MIN(FINSAT633.FINSAT633.SPI.KayitSaat) AS Saat
	FROM            FINSAT633.FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.MalKodu = FINSAT633.FINSAT633.STK.MalKodu INNER JOIN
							 FINSAT633.FINSAT633.MFK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.EvrakNo = FINSAT633.FINSAT633.MFK.EvrakNo AND FINSAT633.FINSAT633.SPI.Tarih = FINSAT633.FINSAT633.MFK.EvrakTarih AND 
							 FINSAT633.FINSAT633.SPI.Chk = FINSAT633.FINSAT633.MFK.HesapKod AND FINSAT633.FINSAT633.SPI.KynkEvrakTip = FINSAT633.FINSAT633.MFK.KynkEvrakTip INNER JOIN
							 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.Chk = FINSAT633.FINSAT633.CHK.HesapKodu
	WHERE        (FINSAT633.FINSAT633.SPI.Depo = @DepoKodu) AND (FINSAT633.FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND 
							(FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar) > 0 AND
							case when @isKable=1 then 
								case when (FINSAT633.FINSAT633.STK.Kod1 = 'KKABLO') then 1 else 0 end 
							else 
								case when (FINSAT633.FINSAT633.STK.Kod1 <> 'KKABLO') then 1 else 0 end 
							end = 1 AND 
							case when @BasTarih > 0 then case when (FINSAT633.FINSAT633.SPI.Tarih >= @BasTarih) then 1 else 0 end else 1 end = 1 AND 
							case when @BasTarih > 0 then case when (FINSAT633.FINSAT633.SPI.Tarih <= @BitTarih) then 1 else 0 end else 1 end = 1 AND
							FINSAT633.FINSAT633.SPI.ROW_ID NOT IN (SELECT        BIRIKIM.wms.IRS_Detay.KynkSiparisID
																	FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
																							 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN
																							 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID
																	WHERE        (BIRIKIM.wms.Gorev.DurumID = 9) AND (NOT (BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL))
																	GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID)
	GROUP BY FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.Chk, FINSAT633.FINSAT633.CHK.Unvan1, FINSAT633.FINSAT633.CHK.GrupKod, FINSAT633.FINSAT633.CHK.FaturaAdres3, FINSAT633.FINSAT633.MFK.Aciklama
)
GO
/****** Object:  StoredProcedure [FINSAT633].[sp_BakiyeHesap]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [FINSAT633].[sp_BakiyeHesap]
(
	@HesapKodu VARCHAR(20),
	@OKrediLimiti NUMERIC(25,6) OUTPUT,
	@OBakiye NUMERIC(25,6) OUTPUT,
	@OSahsiCek NUMERIC(25,6) OUTPUT,
	@OMusteriCek NUMERIC(25,6) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT top(1) @OKrediLimiti=max([Kredi Limiti]), @OBakiye=max([Bakiye]),@OSahsiCek=max([Şahsi Çek]),@OMusteriCek=max([Müşteri Çek]) FROM 
	(
		SELECT alsana.[Ünvan],SUM(alsana.[Kredi Limiti]) as [Kredi Limiti] ,SUM(alsana.Bakiye) as [Bakiye] ,SUM(alsana.PRBakiye) as [PRBakiye] ,SUM(alsana.[Şahsi Çek]) as [Şahsi Çek] ,SUM(alsana.[Müşteri Çek]) as [Müşteri Çek] FROM 
		(
			SELECT c.unvan as [Ünvan],SUM(c.kredilimiti) as [Kredi Limiti],SUM(c.bakiye) as Bakiye,SUM(c.PRBakiye)as PRBakiye,(SELECT ISNULL(SUM(CASE WHEN h.kod1='Ş' AND h.SonIslemTip NOT IN ('5','12','13') AND h.vadetarih>=getdate()-10 THEN h.tutar ELSE 0 END),0) FROM 
			FINSAT633.FINSAT633.ACK h (nolock) where h.veren=c.hesapkodu AND c.hesapkodu not like '%PR')-(SELECT ISNULL(SUM(CASE WHEN h.kod1='Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10AND CHI.Islemtip=21 AND CHI.BA=1 THEN CHI.tutar ELSE 0 END),0)  FROM 
			FINSAT633.FINSAT633.ACK h (nolock),FINSAT633.FINSAT633.CHI CHI (NOLOCK) where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) AND h.veren=c.hesapkodu AND c.hesapkodu not like '%PR'AND H.EVRAKNO=CHI.EVRAKNO)   as[Şahsi Çek] ,(SELECT ISNULL(SUM(CASE WHEN h.kod1<>'Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 THEN h.tutar ELSE 0 END),0)  FROM 
			FINSAT633.FINSAT633.ACK h (nolock) where h.veren=c.hesapkodu AND c.hesapkodu not like '%PR')-(SELECT ISNULL(SUM(CASE WHEN h.kod1<>'Ş' AND h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 AND CHI.Islemtip=21 AND CHI.BA=1 THEN CHI.tutar ELSE 0 END),0)  FROM 
			FINSAT633.FINSAT633.ACK h (nolock),FINSAT633.FINSAT633.CHI CHI (NOLOCK) where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) AND h.veren=c.hesapkodu AND c.hesapkodu not like '%PR' AND H.EVRAKNO=CHI.EVRAKNO) as[Müşteri Çek] FROM
			(
				SELECT d.HesapKodu as hesapkodu,d.unvan,ISNULL(CASE WHEN d.hesapkodu=@HesapKodu+'PR' THEN 0 ELSE ISNULL(SUM(d.kredilimiti),0) END ,0) as kredilimiti,SUM(d.bakiye) as bakiye,SUM(d.PRBakiye) as PRbakiye FROM
				(
					SELECT b.hesapkodu,b.unvan1+' '+b.unvan2 as unvan,  b.kredilimiti ,CASE WHEN b.hesapkodu=@HesapKodu+'PR' THEN 0 ELSE SUM(CASE WHEN a.BA = 0 THEN ISNULL(a.Tutar,0) ELSE ISNULL(-a.Tutar,0) END) END as Bakiye,SUM(CASE WHEN a.BA = 0 THEN ISNULL(a.[PR Tutar],0) ELSE ISNULL(-a.[PR Tutar],0) END) as PRBakiye FROM 
					(
						SELECT  S.KarsiHesapKodu as HesapKodu,f.ozelkod,f.hesapno, ISNULL((CASE WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar END),0) as [PR Tutar],ISNULL((CASE WHEN S.IslemTip IN ('5','9')  THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar END),0) as Tutar,  ISNULL((CASE WHEN S.BA = 0 THEN 1 ELSE 0 END),'-1') as BA FROM 
						FINSAT633.FINSAT633.CHI S (nolock) 
						RIGHT JOIN 
						FINSAT633.FINSAT633.CHK F  (NOLOCK) on S.KarsiHesapKodu=F.HesapKodu where S.KarsiHesapKodu is not null AND S.KarsiHesapKodu <> '' AND S.IslemTip NOT IN (16,21,27,32,36,37,41,42) 
						UNION ALL  
						SELECT S.HesapKodu as hesapkodu,f.ozelkod ,f.hesapno,ISNULL((CASE WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN -S.Tutar WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar END),0) as [PR Tutar],  ISNULL((CASE WHEN S.IslemTip IN ('5','9') THEN -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar END),0) as Tutar  ,ISNULL(S.BA,'-1') as BA FROM 
						FINSAT633.FINSAT633.CHI S (nolock) 
						RIGHT JOIN 
						FINSAT633.FINSAT633.CHK F (NOLOCK) on  S.HesapKodu=F.HesapKodu where S.IslemTip not in (16,21,27,32,36,37,41,42)
					) a  
					RIGHT JOIN  

					FINSAT633.FINSAT633.CHK b (nolock) ON b.hesapkodu=a.hesapkodu where 
					(b.HesapKodu LIKE @HesapKodu+'%' AND 
						( b.HesapKodu BETWEEN '1' AND '7'
						AND SUBSTRING(b.HesapKodu,1,1)<> '7' 
						or b.HesapKodu between '9' AND '99' --tüm için
						)
					) GROUP BY b.hesapkodu,(b.unvan1+' '+b.unvan2),b.kredilimiti 
				) d GROUP BY d.unvan,d.hesapkodu
			) c GROUP BY c.unvan,c.hesapkodu
		) alsana GROUP BY [Ünvan]
	) AS A
END



GO
/****** Object:  StoredProcedure [FINSAT633].[sp_Kod3OrtGun]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [FINSAT633].[sp_Kod3OrtGun]
(
	@HesapKodu VARCHAR(20),
	@ChkKod2 VARCHAR(10),
	@OKod3OrtGun NUMERIC(25,6) OUTPUT,
	@OKod3OrtBakiye NUMERIC(25,6) OUTPUT,
	@OOrtalamaGun NUMERIC(25,6) OUTPUT,
	@OBakilacakGun INT OUTPUT 
)
AS
BEGIN
	SET NOCOUNT ON;
	SET @OBakilacakGun = 0 SET @OKod3OrtGun = 0 SET @OKod3OrtBakiye = 0 SET @OOrtalamaGun = 0
	DECLARE @Tarih INT,@Borc NUMERIC(25,6),@VAlacak NUMERIC(25,6),@Tutar NUMERIC(25,6)
	DECLARE @BakiyeOrt NUMERIC(25,6),@Alacak NUMERIC(25,6),@Alacak2 NUMERIC(25,6),@FaturaTutar NUMERIC(25,6),@Gun INT,@FaturaTop NUMERIC(25,6),@GunFaturaTutar NUMERIC(25,6),@OrtGunFaturaTutar NUMERIC(25,6),@OrtGunFaturaTop NUMERIC(25,6),@GunOrtGunFaturaTutar NUMERIC(25,6)
	
	SET @BakiyeOrt = 0 SET @Alacak = 0 SET @Alacak2 = 0 SET @FaturaTutar = 0 SET @Gun = 0 SET @FaturaTop = 0 
	SET @GunFaturaTutar = 0 SET @OrtGunFaturaTutar = 0 SET @OrtGunFaturaTop = 0 SET @GunOrtGunFaturaTutar=0

	DECLARE CURT CURSOR FOR
			select  tarih,Borc,Alacak,Tutar from 
(
	select hesapkodu,SRT,tarih,EvrakNo,Borc,Alacak,Tutar from 
	(
		select A.*,  Case A.BA when B.BorA Then 0 Else 1 End  as st from 
		(
			select A.hesapkodu, A.hesapkodu+'                              1' as SRT,ISNULL(B.tarih,0 )  as Tarih ,ISNULL(B.EvrakNo,' ' )  as EvrakNo,ISNULL(B.BA,0 )  as BA,Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,ISNULL(B.Tutar,0 )  as Tutar from 
			(
				select A.hesapkodu,tarih,A.EvrakNo,min(A.BA) as BA,Sum(ISNULL(A.Tutar,0)- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar from 
				(
					select * from 
					(
						select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where   BA=0 and    IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu 
						UNION ALL 
						select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
						UNION ALL 
						select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,1 as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=0 and KarsiHesapKodu =@HesapKodu and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
						UNION ALL 
						select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,0 as BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and  KarsiHesapKodu =@HesapKodu 
					) A where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
				) A 
				left join 
				(
					select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
					UNION ALL 
					select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1 group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
			) B ,FINSAT633.FINSAT633.CHK A (NoLock)  where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA))<=999999999999999999 and  A.hesapkodu=@HesapKodu  
		) A , 
		(
			select hesapkodu, Case Sign(SUM(ISNULL(YTLborc,0))-sum(ISNULL(YTLalacak,0))) when 1 then 0 when -1 then 1 when 0 then -2 else -3 End  as BorA from 
			(
				select A.hesapkodu,Case B.BA when 0 Then ISNULL(B.YTLTutar,0) Else 0 End  as YTLBorc,Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak from 
				(
					select A.hesapkodu,min(A.BA) as BA,Sum(ISNULL(A.YTLTutar,0)- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar from 
					(
						select * from 
						(
							select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip, round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where   BA=0 and IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
							UNION ALL 
							select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip,round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu 
							UNION ALL 
							select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,1 as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip,round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=0 and KarsiHesapKodu =@HesapKodu and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
							UNION ALL 
							select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,0 as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip,round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and  KarsiHesapKodu =@HesapKodu 
						) A where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
					) A 
					left join 
					( 
						select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,sum( round(Case when Tarih>=38353 then ISNULL(Tutar,0)*1000000 else ISNULL(Tutar,0) End, 2)) as TLTutar,sum( round(Case when Tarih<38353 then ISNULL(Tutar,0)/1000000 else ISNULL(Tutar,0) End,2) ) as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
						UNION ALL 
						select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,sum( round(Case when Tarih>=38353 then ISNULL(Tutar,0)*1000000 else ISNULL(Tutar,0) End, 2)) as TLTutar,sum( round(Case when Tarih<38353 then ISNULL(Tutar,0)/1000000 else ISNULL(Tutar,0) End,2) ) as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End
					) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
				) B ,FINSAT633.FINSAT633.CHK A (NoLock)  where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA))<=999999999999999999 and  A.hesapkodu=@HesapKodu   
			) A  group by hesapkodu 
		) B where A.hesapkodu=B.hesapkodu 
	) A where st=0 
	UNION 
	select hesapkodu,A.hesapkodu+'                              0' as SRT,0 as tarih,' ' as Evrakno,Sum(ISNULL(A.Borc,0)) as Borc,Sum(ISNULL(A.Alacak,0)) as Alacak, sum(ISNULL(A.Tutar,0)) as Tutar from 
	(
		select A.hesapkodu,Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,ISNULL(B.Tutar,0 )  as Tutar from 
		(
			select A.hesapkodu,min(A.BA) as BA,Sum(ISNULL(A.Tutar,0)- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar from 
			(
				select * from 
				(
					select hesapkodu,tarih,EvrakNo,BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where   BA=0    and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
					UNION ALL
					select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock)  where  BA=1  And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
					UNION ALL 
					select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,1 as BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock)  where  BA=0 and KarsiHesapKodu =@HesapKodu and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					UNION ALL 
					select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,0 as BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and KarsiHesapKodu =@HesapKodu 
				) A where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
			) A 
			left join 
			(
				select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock)  where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0  group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
				UNION ALL 
				select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock)  where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
			) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
		) B ,FINSAT633.FINSAT633.CHK A (NoLock)  where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA))<=999999999999999999 and  A.hesapkodu=@HesapKodu
	) A group by A.hesapkodu 
) A order by SRT,Tarih desc,EvrakNo desc
			
			OPEN CURT
			FETCH NEXT FROM CURT INTO @Tarih,@Borc,@VAlacak,@Tutar 			
			IF @Tarih IS NOT NULL
			BEGIN
				SET @BakiyeOrt = @Borc - @VAlacak
				SET @Alacak = @BakiyeOrt
				SET @Alacak2 = @BakiyeOrt

				DECLARE @IlkKarakter varchar(1)
				SET @IlkKarakter = LEFT(@HesapKodu,1)
				IF ISNUMERIC(@IlkKarakter) = 1
				BEGIN
					DECLARE @Ilk INT
					SET @Ilk=CAST(@IlkKarakter AS INT)
					IF (@Ilk > 0 AND @Ilk < 5)
						SET @OBakilacakGun = 21
					ELSE IF (@Ilk = 5 OR @Ilk = 6)
						SET @OBakilacakGun = 25
					ELSE IF (@Ilk = 9)
						SET @OBakilacakGun = 21
				END
				IF PATINDEX('%1%',@ChkKod2) > 0
					SET @OBakilacakGun = 60
			END

			FETCH NEXT FROM CURT INTO @Tarih,@Borc,@VAlacak,@Tutar --WHİLE IN DIŞINDA BİR KERE DAHA DOLDURARAK 1.SATIRI ATLIYORUZ
			WHILE (@@FETCH_STATUS = 0)
			BEGIN
				SET @Gun = CAST(GETDATE() + 2 AS INT) - @Tarih
				SET @FaturaTutar = @Tutar
				SET @OrtGunFaturaTutar = @Tutar
				IF @Alacak > 0
				BEGIN
					IF @Alacak >= @FaturaTutar
					BEGIN 
						SET @Alacak = @Alacak - @FaturaTutar
						IF @Gun > @OBakilacakGun
						BEGIN
							SET @FaturaTop = @FaturaTop + @FaturaTutar
							SET @GunFaturaTutar = @GunFaturaTutar + (@Gun * @FaturaTutar)
						END
					END
					ELSE
					BEGIN
						IF @Gun > @OBakilacakGun
						BEGIN
							SET @FaturaTop = @FaturaTop + @Alacak
							SET @GunFaturaTutar = @GunFaturaTutar + (@Gun * @Alacak)
						END
						SET @Alacak = 0
					END
				END
				IF @Alacak2 > 0
				BEGIN
					IF @Alacak2 >= @OrtGunFaturaTutar
					BEGIN
						SET @Alacak2 = @Alacak2 - @OrtGunFaturaTutar
						SET	@OrtGunFaturaTop = @OrtGunFaturaTop + @OrtGunFaturaTutar
						SET @GunOrtGunFaturaTutar = @GunOrtGunFaturaTutar + (@Gun * @OrtGunFaturaTutar)
					END
					ELSE
					BEGIN
						SET @OrtGunFaturaTop = @OrtGunFaturaTop + @Alacak2
						SET @GunOrtGunFaturaTutar = @GunOrtGunFaturaTutar + (@Gun * @Alacak2)
						SET @Alacak2 = 0
					END		
				END
				ELSE
					BREAK
				FETCH NEXT FROM CURT INTO @Tarih,@Borc,@VAlacak,@Tutar
			END
			CLOSE CURT
			DEALLOCATE CURT
			IF @GunFaturaTutar > 0
				SET @OKod3OrtGun = (@GunFaturaTutar / @FaturaTop) --KOD3 ORTALAMA GÜN
			ELSE
				SET @OKod3OrtGun = 0
			IF @BakiyeOrt > 0
				SET @OKod3OrtBakiye =  @FaturaTop --KOD3 ORTALAMA BAKİYE
			ELSE
				SET @OKod3OrtBakiye =   0
			IF @GunOrtGunFaturaTutar > 0
				SET @OOrtalamaGun = (@GunOrtGunFaturaTutar /@OrtGunFaturaTop) --ORTALAMA GÜN
			ELSE
				 SET @OOrtalamaGun = 0
END



GO
/****** Object:  StoredProcedure [FINSAT633].[sp_SiparisKontrol]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [FINSAT633].[sp_SiparisKontrol]
(
	@SiparisEvrakNo VARCHAR(8)
)
AS
BEGIN
	SET NOCOUNT ON;
		
	
	DECLARE @SiparisBak NUMERIC(25,6),@KontrolEdilecekMiktar NUMERIC(25,6),@DepoStokMiktari NUMERIC(25,6),@ToplamSiparisBakiye NUMERIC(25,6)
	DECLARE @KrediLimiti NUMERIC(25,6),@Bakiye NUMERIC(25,6), @Bakiye_Gecici NUMERIC(25,6),@SahsiCek NUMERIC(25,6),@MusteriCek NUMERIC(25,6),@Sonuc VARCHAR(30),@Kod3OrtGun NUMERIC(25,6),@Kod3OrtBakiye NUMERIC(25,6),@OrtalamaGun NUMERIC(25,6),@SiparisBakiyesi NUMERIC(25,6),@ChkUpdateDegeri VARCHAR(20)
	SET @Kod3OrtGun = 0 SET @Kod3OrtBakiye = 0 SET @OrtalamaGun = 0 SET @SiparisBakiyesi = 0 SET @ChkUpdateDegeri='' SET @Sonuc=''
	SET @KrediLimiti=0
	DECLARE @BakilacakGun INT,@Index INT,@Tarih INT,@Borc NUMERIC(25,6),@VAlacak NUMERIC(25,6),@Tutar NUMERIC(25,6),
	@Alacak NUMERIC(25,6),@Alacak2 NUMERIC(25,6),@FaturaTutar NUMERIC(25,6),@Gun INT,@FaturaTop NUMERIC(25,6),@GunFaturaTutar NUMERIC(25,6),@OrtGunFaturaTutar NUMERIC(25,6),@OrtGunFaturaTop NUMERIC(25,6),@GunOrtGunFaturaTutar NUMERIC(25,6),@BakiyeOrt NUMERIC(25,6), @GunBakiye VARCHAR(20), @RiskYuzdesi NUMERIC(25,6)
	SET @BakilacakGun = 0 SET @Index = 0 SET @Alacak = 0 SET @Alacak2 = 0 SET @FaturaTutar = 0 SET @Gun = 0 SET @FaturaTop = 0 
	SET @GunFaturaTutar = 0 SET @OrtGunFaturaTutar = 0 SET @OrtGunFaturaTop = 0 SET @BakiyeOrt = 0 SET @GunOrtGunFaturaTutar=0

	DECLARE @IslemTur SMALLINT,@IslemTip SMALLINT, @EvrakNo VARCHAR(8),@Depo VARCHAR(5),@MalKodu VARCHAR(30),@Miktar NUMERIC(25,6),@BirimFiyat NUMERIC(25,6), @ToplamIskonto NUMERIC(25,6),@KDVOran REAL,@TeslimMiktar NUMERIC(25,6),@KapatilanMiktar NUMERIC(25,6),@Chk VARCHAR(20), @SiraNo SMALLINT,@SiparisDurumu SMALLINT,@Kod4 VARCHAR(10),@Kod10 VARCHAR(10),@Kod13 NUMERIC(25,6),@OzelKod VARCHAR(10),@HesapKodu VARCHAR(20),@ChkKod2 VARCHAR(10),@Kod3 VARCHAR(10),@Kod14 NUMERIC(25,6), @KynkEvrakTip SMALLINT,@CheckSum SMALLINT,@TeslimMiktarD numeric(25,6),@Kod13D numeric(25,6),@MiktarD numeric(25,6),@BirimFiyatD numeric(25,6),@DepoD VARCHAR(5),@Kod12 SMALLINT,@Kod12D SMALLINT


	SET @Kod3OrtGun = 0 SET @Kod3OrtBakiye = 0 SET @OrtalamaGun = 0 SET @SiparisBakiyesi = 0 SET @ChkUpdateDegeri='' SET @Sonuc=''
	

	
	SET @BakilacakGun = 0 SET @Index = 0 


	DECLARE @BakiyeHesapFlag BIT,@Kod3OrtGunFlag BIT
	SET @BakiyeHesapFlag = 0 SET @Kod3OrtGunFlag = 0

	
	DECLARE @C1 CURSOR 
	SET @C1 = CURSOR LOCAL SCROLL FOR  
	SELECT IslemTur,IslemTip,EvrakNo,Depo,MalKodu,Miktar,BirimFiyat,ToplamIskonto,KDVOran,TeslimMiktar,KapatilanMiktar,Chk,SiraNo,SiparisDurumu,Kod4,Kod10,Kod13,KynkEvrakTip,[CheckSum] FROM FINSAT633.FINSAT633.SPI WHERE EvrakNo=@SiparisEvrakNo
	
	OPEN @C1
	FETCH NEXT FROM @C1 INTO @IslemTur,@IslemTip,@EvrakNo,@Depo,@MalKodu,@Miktar,@BirimFiyat,@ToplamIskonto,@KDVOran,@TeslimMiktar,@KapatilanMiktar,@Chk,@SiraNo,@SiparisDurumu,@Kod4,@Kod10,@Kod13,@KynkEvrakTip,@CheckSum
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		--19102017 de değiştirildi
		--IF 	@KynkEvrakTip = 62 AND @CheckSum <> 13 AND @Kod10 <> 'Reddedildi' AND @SiparisDurumu=0 AND @TeslimMiktar+@KapatilanMiktar<@Miktar AND @IslemTur=1 AND @IslemTip=1 AND @Kod4<>'1'

		IF 	@KynkEvrakTip = 62 AND @CheckSum <> 13 AND @Kod10 <> 'Reddedildi' AND  @SiparisDurumu=0 AND @TeslimMiktar+@KapatilanMiktar<@Miktar AND @IslemTur=1 AND @IslemTip=1 AND @Kod4<>'1'
		-- and (@TeslimMiktar<> @TeslimMiktarD or @Miktar<>@MiktarD or @BirimFiyat<>@BirimFiyatD or @DepoD<>@Depo or @Kod12D<>@Kod12)
		BEGIN


			SET @SiparisBak = ((((@Miktar * @BirimFiyat)-@ToplamIskonto) * @KDVOran) / 100) + ((@Miktar * @BirimFiyat) - @ToplamIskonto)
		SET @KontrolEdilecekMiktar = @Miktar - (@KapatilanMiktar + @TeslimMiktar)
		SELECT @DepoStokMiktari = (DvrMiktar+(GirMiktar-CikMiktar)) FROM FINSAT633.FINSAT633.DST WHERE MalKodu=@MalKodu AND Depo=@Depo
		IF @KontrolEdilecekMiktar > @DepoStokMiktari
			SET @ToplamSiparisBakiye = (@DepoStokMiktari * ((@Miktar * @BirimFiyat) -@ToplamIskonto) / @Miktar) +(@DepoStokMiktari * (((@Miktar * @BirimFiyat)- @ToplamIskonto) / @Miktar) * @KDVOran / 100);
			ELSE
				SET @ToplamSiparisBakiye = (@KontrolEdilecekMiktar * ((@Miktar * @BirimFiyat)-@ToplamIskonto)) + ((@KontrolEdilecekMiktar *(((@Miktar * @BirimFiyat) - @ToplamIskonto) / @Miktar )) * @Miktar / 100)
			
			SELECT @OzelKod=OzelKod,@HesapKodu=HesapKodu,@ChkKod2=Kod2 FROM FINSAT633.FINSAT633.CHK WHERE HesapKodu=@Chk			
			IF @ChkKod2='8'
			BEGIN
				SET @Sonuc ='Beklemede'
			END
			ELSE IF @OzelKod='2'
		BEGIN
			SELECT @KrediLimiti=[Kredi Limiti], @Bakiye=[Bakiye],@Bakiye_Gecici=[Bakiye],@SahsiCek=[Şahsi Çek],@MusteriCek=[Müşteri Çek] FROM (
			select alsana.[Ünvan],sum(alsana.[Kredi Limiti]) as [Kredi Limiti] ,sum(alsana.Bakiye) as [Bakiye] ,sum(alsana.PRBakiye) as [PRBakiye] ,sum(alsana.[Şahsi Çek]) as [Şahsi Çek] ,sum(alsana.[Müşteri Çek]) as [Müşteri Çek] from (select c.unvan as [Ünvan],sum(c.kredilimiti) as [Kredi Limiti],sum(c.bakiye) as Bakiye,sum(c.PRBakiye)as PRBakiye,(select isnull(sum(case when h.kod1='Ş' and h.SonIslemTip NOT IN ('5','12','13') and h.vadetarih>=getdate()-10 then h.tutar else 0 end),0)  from FINSAT633.FINSAT633.ACK h (nolock) where h.veren=c.hesapkodu and c.hesapkodu not like '%PR')-(select isnull(sum(case when h.kod1='Ş' and h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10AND CHI.Islemtip=21 and CHI.BA=1 then CHI.tutar else 0 end),0)  from FINSAT633.FINSAT633.ACK h (nolock),FINSAT633.FINSAT633.CHI CHI (NOLOCK) where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) and h.veren=c.hesapkodu and c.hesapkodu not like '%PR'AND H.EVRAKNO=CHI.EVRAKNO)   as[Şahsi Çek] ,(select isnull(sum(case when h.kod1<>'Ş' and h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 then h.tutar else 0 end),0)  from FINSAT633.FINSAT633.ACK h (nolock) where h.veren=c.hesapkodu and c.hesapkodu not like '%PR')-(select isnull(sum(case when h.kod1<>'Ş' and h.SonIslemTip NOT IN ('5','12','13')and h.vadetarih>=getdate()-10 AND CHI.Islemtip=21 and CHI.BA=1 then CHI.tutar else 0 end),0)  from FINSAT633.FINSAT633.ACK h (nolock),FINSAT633.FINSAT633.CHI CHI (NOLOCK) where (CHI.Hesapkodu=H.VEREN or CHI.Karsihesapkodu=H.VEREN) and h.veren=c.hesapkodu and c.hesapkodu not like '%PR' AND H.EVRAKNO=CHI.EVRAKNO) as[Müşteri Çek] from(select d.HesapKodu as hesapkodu,d.unvan,isnull(case when d.hesapkodu=@HesapKodu+'PR' then 0 else isnull(sum(d.kredilimiti),0) end ,0) as kredilimiti,sum(d.bakiye) as bakiye,sum(d.PRBakiye) as PRbakiye from( select b.hesapkodu,b.unvan1+' '+b.unvan2 as unvan,  b.kredilimiti ,case when b.hesapkodu='1110900'+'PR' then 0 else sum(case when a.BA = 0 then isnull(a.Tutar,0) else isnull(-a.Tutar,0) end) end as Bakiye,sum(case when a.BA = 0 then isnull(a.[PR Tutar],0) else isnull(-a.[PR Tutar],0) end) as PRBakiye from (select  S.KarsiHesapKodu as HesapKodu,f.ozelkod,f.hesapno, isnull((case WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' then -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar end),0) as [PR Tutar],isnull((case WHEN S.IslemTip IN ('5','9')  then -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar end),0) as Tutar,  isnull((case when S.BA = 0 then 1 else 0 end),'-1') as BA From FINSAT633.FINSAT633.CHI S (nolock) right join FINSAT633.FINSAT633.CHK F  (NOLOCK) on S.KarsiHesapKodu=F.HesapKodu where S.KarsiHesapKodu is not null and S.KarsiHesapKodu <> '' and S.IslemTip not in (16,21,27,32,36,37,41,42) Union ALL  select S.HesapKodu as hesapkodu,f.ozelkod ,f.hesapno,isnull((case WHEN S.IslemTip IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' then -S.Tutar WHEN S.IslemTip NOT IN ('5','9') AND F.HesapKodu=@HesapKodu+'PR' THEN S.Tutar end),0) as [PR Tutar],  isnull((case WHEN S.IslemTip IN ('5','9') then -S.Tutar  WHEN S.IslemTip NOT IN ('5','9') THEN S.Tutar end),0) as Tutar  ,isnull(S.BA,'-1') as BA From FINSAT633.FINSAT633.CHI S (nolock) right join FINSAT633.FINSAT633.CHK F (NOLOCK) on  S.HesapKodu=F.HesapKodu where S.IslemTip not in (16,21,27,32,36,37,41,42)) a  right join  

 FINSAT633.FINSAT633.CHK b (nolock) ON b.hesapkodu=a.hesapkodu where 
(b.HesapKodu like @HesapKodu+'%' and 
( b.HesapKodu between '1' and '7'
 and substring(b.HesapKodu,1,1)<> '7' 
--or b.HesapKodu between '9' and '10' --tüm için
)) group by b.hesapkodu,(b.unvan1+' '+b.unvan2),b.kredilimiti ) d group by d.unvan,d.hesapkodu) c group by c.unvan,c.hesapkodu) alsana group by [Ünvan]) AS A
			--select @KrediLimiti = null,@Bakiye=1000,@SahsiCek=null,@MusteriCek=0
			IF @KrediLimiti=0
			BEGIN
			
				SET @Sonuc = 'Beklemede'  
				--Barış Bey bekleyene düşmesini istedi 20.12.2017 
				--SET @Sonuc = 'Beklemede'
	
			END
				DECLARE @CURT CURSOR 
				SET @CURT = CURSOR LOCAL SCROLL FOR  
			select  tarih,Borc,Alacak,Tutar from 
(
	select hesapkodu,SRT,tarih,EvrakNo,Borc,Alacak,Tutar from 
	(
		select A.*,  Case A.BA when B.BorA Then 0 Else 1 End  as st from 
		(
			select A.hesapkodu, A.hesapkodu+'                              1' as SRT,ISNULL(B.tarih,0 )  as Tarih ,ISNULL(B.EvrakNo,' ' )  as EvrakNo,ISNULL(B.BA,0 )  as BA,Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,ISNULL(B.Tutar,0 )  as Tutar from 
			(
				select A.hesapkodu,tarih,A.EvrakNo,min(A.BA) as BA,Sum(ISNULL(A.Tutar,0)- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar from 
				(
					select * from 
					(
						select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where   BA=0 and    IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu 
						UNION ALL 
						select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
						UNION ALL 
						select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,1 as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=0 and KarsiHesapKodu =@HesapKodu and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
						UNION ALL 
						select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,0 as BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and  KarsiHesapKodu =@HesapKodu 
					) A where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
				) A 
				left join 
				(
					select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
					UNION ALL 
					select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1 group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
				) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
			) B ,FINSAT633.FINSAT633.CHK A (NoLock)  where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA))<=999999999999999999 and  A.hesapkodu=@HesapKodu  
		) A , 
		(
			select hesapkodu, Case Sign(SUM(ISNULL(YTLborc,0))-sum(ISNULL(YTLalacak,0))) when 1 then 0 when -1 then 1 when 0 then -2 else -3 End  as BorA from 
			(
				select A.hesapkodu,Case B.BA when 0 Then ISNULL(B.YTLTutar,0) Else 0 End  as YTLBorc,Case B.BA when 1 Then B.YTLTutar Else 0 End  as YTLAlacak from 
				(
					select A.hesapkodu,min(A.BA) as BA,Sum(ISNULL(A.YTLTutar,0)- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.YTLtutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.YTLtutar,0 )  Else 0 End  else 0 End ) as YTLTutar from 
					(
						select * from 
						(
							select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip, round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where   BA=0 and IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
							UNION ALL 
							select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip,round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu 
							UNION ALL 
							select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,1 as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip,round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=0 and KarsiHesapKodu =@HesapKodu and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
							UNION ALL 
							select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,0 as BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,DovizCinsi,Islemtip,KynkEvraktip,round(Case when Tarih<38353 then Tutar/1000000 else Tutar End,2)  as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and  KarsiHesapKodu =@HesapKodu 
						) A where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
					) A 
					left join 
					( 
						select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,sum( round(Case when Tarih>=38353 then ISNULL(Tutar,0)*1000000 else ISNULL(Tutar,0) End, 2)) as TLTutar,sum( round(Case when Tarih<38353 then ISNULL(Tutar,0)/1000000 else ISNULL(Tutar,0) End,2) ) as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0 group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
						UNION ALL 
						select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip,sum( round(Case when Tarih>=38353 then ISNULL(Tutar,0)*1000000 else ISNULL(Tutar,0) End, 2)) as TLTutar,sum( round(Case when Tarih<38353 then ISNULL(Tutar,0)/1000000 else ISNULL(Tutar,0) End,2) ) as YTLTutar from FINSAT633.FINSAT633.CHI A  (NoLock) where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End
					) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
				) B ,FINSAT633.FINSAT633.CHK A (NoLock)  where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA))<=999999999999999999 and  A.hesapkodu=@HesapKodu   
			) A  group by hesapkodu 
		) B where A.hesapkodu=B.hesapkodu 
	) A where st=0 
	UNION 
	select hesapkodu,A.hesapkodu+'                              0' as SRT,0 as tarih,' ' as Evrakno,Sum(ISNULL(A.Borc,0)) as Borc,Sum(ISNULL(A.Alacak,0)) as Alacak, sum(ISNULL(A.Tutar,0)) as Tutar from 
	(
		select A.hesapkodu,Case B.BA when 0 Then B.Tutar Else 0 End  as Borc,Case B.BA when 1 Then B.Tutar Else 0 End  as Alacak,ISNULL(B.Tutar,0 )  as Tutar from 
		(
			select A.hesapkodu,min(A.BA) as BA,Sum(ISNULL(A.Tutar,0)- Case A.Islemtip when 4 then  Case B.Islemtip when 5 Then ISNULL(B.tutar,0 )  Else 0 End  when 8 then  Case B.Islemtip when 9 Then ISNULL(B.tutar,0 )  Else 0 End  else 0 End ) as Tutar from 
			(
				select * from 
				(
					select hesapkodu,tarih,EvrakNo,BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where   BA=0    and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
					UNION ALL
					select hesapkodu,tarih,EvrakNo,BA, Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock)  where  BA=1  And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) and Hesapkodu =@HesapKodu
					UNION ALL 
					select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,1 as BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock)  where  BA=0 and KarsiHesapKodu =@HesapKodu and  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51) 
					UNION ALL 
					select KarsiHesapKodu as HesapKodu,tarih,EvrakNo,0 as BA,Case IslemTip when 0 Then  Case A.VadeTarih when 0 Then Tarih Else A.VadeTarih End  Else A.VadeTarih End  as vadetarih ,Tutar,A.DovizCinsi,Islemtip,KynkEvraktip from FINSAT633.FINSAT633.CHI A  (NoLock) where  BA=1 and KarsiHesapKodu =@HesapKodu 
				) A where  (Islemtip<>5 and Islemtip<>9 and Islemtip<>51 and Islemtip<>36 and Islemtip<>37 and Islemtip<>41 and IslemTip <>52 ) And  IslemTip in (0,1,2,3,4,6,7,8,10,11,12,13,14,15,17,18,19,20,22,23,24,25,26,28,29,30,31,33,34,35,38,39,40,42,43,44,45,46,47,48,49,50,51)
			) A 
			left join 
			(
				select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock)  where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=0  group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  
				UNION ALL 
				select hesapkodu,BA,sum(ISNULL(A.Tutar,0)) as Tutar,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End  as IslemTip from FINSAT633.FINSAT633.CHI A  (NoLock)  where (Islemtip=5 or Islemtip=9 or IslemTip=51) and BA=1  group by hesapkodu,BA,KynkEvraktip,Evrakno, DovizCinsi,  Case IslemTip when 51 Then 5 Else IslemTip End 
			) B on A.hesapkodu=B.hesapkodu and A.KynkEvrakTip=B.KynkEvrakTip and A.EvrakNo=B.EvrakNo and A.BA=B.BA and A.DovizCinsi = B.DovizCinsi  group by A.Hesapkodu,A.BA,A.Evrakno,A.KynkEvrakTip,Tarih,VadeTarih, A.DovizCinsi 
		) B ,FINSAT633.FINSAT633.CHK A (NoLock)  where A.hesapkodu=B.hesapkodu   and ((DvrB + OdemeB + CiroB + IadeFatB + KDVB + DigerB ) - (DvrA + OdemeA + CiroA + IadeFatA + KDVA + DigerA))<=999999999999999999 and  A.hesapkodu=@HesapKodu
	) A group by A.hesapkodu 
) A order by SRT,Tarih desc,EvrakNo desc
			
			OPEN @CURT
			FETCH NEXT FROM @CURT INTO @Tarih,@Borc,@VAlacak,@Tutar 			
			IF @Tarih IS NOT NULL
			BEGIN
				SET @BakiyeOrt = @Borc - @VAlacak
				SET @Alacak = @BakiyeOrt
				SET @Alacak2 = @BakiyeOrt

				DECLARE @IlkKarakter varchar(1)
				SET @IlkKarakter = LEFT(@HesapKodu,1)
				IF ISNUMERIC(@IlkKarakter) = 1
				BEGIN
					DECLARE @Ilk INT
					SET @Ilk=CAST(@IlkKarakter AS INT)
					IF (@Ilk > 0 AND @Ilk < 5)
						SET @BakilacakGun = 21
					ELSE IF (@Ilk = 5 OR @Ilk = 6)
						SET @BakilacakGun = 25
				END
				IF PATINDEX('%1%',@ChkKod2) > 0
					SET @BakilacakGun = 60
			END

			FETCH NEXT FROM @CURT INTO @Tarih,@Borc,@VAlacak,@Tutar --WHİLE IN DIŞINDA BİR KERE DAHA DOLDURARAK 1.SATIRI ATLIYORUZ
			WHILE (@@FETCH_STATUS = 0)
			BEGIN
				SET @Gun = CAST(GETDATE() + 2 AS INT) - @Tarih
				SET @FaturaTutar = @Tutar
				SET @OrtGunFaturaTutar = @Tutar
				IF @Alacak > 0
				BEGIN
					IF @Alacak >= @FaturaTutar
					BEGIN 
						SET @Alacak = @Alacak - @FaturaTutar
						IF @Gun > @BakilacakGun
						BEGIN
							SET @FaturaTop = @FaturaTop + @FaturaTutar
							SET @GunFaturaTutar = @GunFaturaTutar + (@Gun * @FaturaTutar)
						END
					END
					ELSE
					BEGIN
						IF @Gun > @BakilacakGun
						BEGIN
							SET @FaturaTop = @FaturaTop + @Alacak
							SET @GunFaturaTutar = @GunFaturaTutar + (@Gun * @Alacak)
						END
						SET @Alacak = 0
					END
				END
				IF @Alacak2 > 0
				BEGIN
					IF @Alacak2 >= @OrtGunFaturaTutar
					BEGIN
						SET @Alacak2 = @Alacak2 - @OrtGunFaturaTutar
						SET	@OrtGunFaturaTop = @OrtGunFaturaTop + @OrtGunFaturaTutar
						SET @GunOrtGunFaturaTutar = @GunOrtGunFaturaTutar + (@Gun * @OrtGunFaturaTutar)
					END
					ELSE
					BEGIN
						SET @OrtGunFaturaTop = @OrtGunFaturaTop + @Alacak2
						SET @GunOrtGunFaturaTutar = @GunOrtGunFaturaTutar + (@Gun * @Alacak2)
						SET @Alacak2 = 0
					END		
				END
				ELSE
					BREAK
				FETCH NEXT FROM @CURT INTO @Tarih,@Borc,@VAlacak,@Tutar
			END
			CLOSE @CURT
			DEALLOCATE @CURT
			IF @GunFaturaTutar > 0
				SET @Kod3OrtGun = (@GunFaturaTutar / @FaturaTop) --KOD3 ORTALAMA GÜN
			ELSE
				SET @Kod3OrtGun = 0
			IF @BakiyeOrt > 0
				SET @Kod3OrtBakiye =  @FaturaTop --KOD3 ORTALAMA BAKİYE
			ELSE
				SET @Kod3OrtBakiye =   0
			IF @GunOrtGunFaturaTutar > 0
				SET @OrtalamaGun = (@GunOrtGunFaturaTutar /@OrtGunFaturaTop) --ORTALAMA GÜN
			ELSE
				 SET @OrtalamaGun = 0

			
			IF @Sonuc <> 'Terminal'	--if (sonuc.contains('terminal') return sonuc
			BEGIN
				select @SiparisBakiyesi=isnull(SUM(t.Kod14),0) from FINSAT633.FINSAT633.SPI (nolock)t  where t.Chk=@HesapKodu and t.SiparisDurumu=0 and t.Kod10<>'Reddedildi'
				SET  @SiparisBakiyesi = @SiparisBakiyesi + @SiparisBak
				
					--Kod3OrtGun>60 ve ve Fatura Toplamının 2500 den büyük olduğu durumda Kod2 nin guncellenmesi.
				IF @Kod3OrtGun >60 AND @FaturaTop>2500
				BEGIN
					SET @ChkUpdateDegeri = '8'
					--SET @Sonuc = @Sonuc + 'Beklemede'
					SET @Sonuc ='Beklemede'
				
						


				END


				IF @KrediLimiti < (@Bakiye + @SahsiCek + (@MusteriCek / 5) + @SiparisBakiyesi)
				BEGIN
					SET @ChkUpdateDegeri = @ChkUpdateDegeri +'5'
					--SET @Sonuc = @Sonuc + 'Beklemede'
					SET @Sonuc ='Beklemede'
					
				END
				
				
				

				IF @OrtalamaGun > @BakilacakGun
				BEGIN
					IF @Bakiye >= 1000
					BEGIN
						IF @Bakiye >= (@KrediLimiti / 10)
						BEGIN
							SET @ChkUpdateDegeri = @ChkUpdateDegeri + '3'
							IF PATINDEX('%Beklemede%',@Sonuc) = 0
								--SET @Sonuc = @Sonuc + 'Beklemede'
								SET @Sonuc ='Beklemede'
							
						END
					END
				END
				ELSE
				BEGIN
					IF @Kod3OrtBakiye >= 1000 AND @Kod3OrtGun > @BakilacakGun
					BEGIN
						IF @Kod3OrtBakiye >= (@KrediLimiti / 10)
						BEGIN
							SET @ChkUpdateDegeri = @ChkUpdateDegeri + '3'
							IF PATINDEX('%Beklemede%',@Sonuc) = 0
								--SET @Sonuc = @Sonuc + 'Beklemede'
									SET @Sonuc ='Beklemede'
						END
					END
				END
				DECLARE @Sart3Mu BIT SET @Sart3Mu = 0
				IF PATINDEX('%9%',@ChkKod2) > 0 OR PATINDEX('%8%',@ChkKod2) > 0 OR PATINDEX('%7%',@ChkKod2) > 0 OR PATINDEX('%6%',@ChkKod2) > 0 OR PATINDEX('%4%',@ChkKod2) > 0 OR PATINDEX('%2%',@ChkKod2) > 0
				BEGIN
					IF PATINDEX('%Beklemede%',@Sonuc) = 0
						SET @Sonuc = @Sonuc + 'Beklemede'
								
					SET @Sart3Mu = 1
				END
				IF @ChkUpdateDegeri <> ''
				BEGIN
					DECLARE @TopDeger VARCHAR(20) SET @TopDeger = @ChkUpdateDegeri + @ChkKod2
					DECLARE @UpdateEdilecekDeger VARCHAR(20)
					SET @UpdateEdilecekDeger = ''
					IF @ChkKod2 = '' OR @ChkKod2 IS NULL
						SET @UpdateEdilecekDeger = @ChkUpdateDegeri
					ELSE
					BEGIN
						SET @Index = 1
						DECLARE @V INT
						CREATE TABLE #T (C INT)
						WHILE @Index < LEN(@TopDeger)
						BEGIN
							INSERT INTO #T VALUES(CAST(SUBSTRING(@TopDeger,@Index,1) AS INT))
							SET @Index = @Index + 1;
						END
						DECLARE @CUR2 CURSOR 
						SET @CUR2 = CURSOR LOCAL SCROLL FOR   SELECT C FROM #T ORDER BY C DESC
						OPEN @CUR2
						FETCH NEXT FROM @CUR2 INTO @V
						WHILE @@FETCH_STATUS = 0
						BEGIN
							IF PATINDEX('%'+CAST(@V AS VARCHAR(1))+'%',@UpdateEdilecekDeger) =  0
								SET @UpdateEdilecekDeger = @UpdateEdilecekDeger + CAST(@V AS VARCHAR(1))
							FETCH NEXT FROM @CUR2 INTO @V
						END
						CLOSE @CUR2
						DEALLOCATE @CUR2
						DROP TABLE #T
						
					END
					Update FINSAT633.FINSAT633.CHK SET Kod2=@UpdateEdilecekDeger where HesapKodu=@HesapKodu
				END
				IF @ChkUpdateDegeri = '' AND @Sart3Mu = 0
				BEGIN
					IF PATINDEX('%Beklemede%',@Sonuc) = 0
					SET @Sonuc ='Terminal'					
				END
			END
		END
		ELSE BEGIN
			IF PATINDEX('%Beklemede%',@Sonuc) = 0
			SET @Sonuc ='Terminal'
		END
			SET @Kod10 = @Sonuc
			SET @Kod3='Tüm'
			
			
			IF @DepoStokMiktari > 0
			BEGIN
				IF @KontrolEdilecekMiktar > @DepoStokMiktari
				BEGIN
					SET @Kod13 = @DepoStokMiktari
					SET @Kod14 = (@DepoStokMiktari * (((@Miktar * @BirimFiyat)-@ToplamIskonto) / @Miktar)) + ((@DepoStokMiktari * (((@Miktar * @BirimFiyat)-@ToplamIskonto)/@Miktar))) * @KDVOran / 100
				END
				ELSE BEGIN
					SET @Kod13 = @KontrolEdilecekMiktar
					SET @Kod14 = (@KontrolEdilecekMiktar * (((@Miktar * @BirimFiyat)-@ToplamIskonto)/@Miktar)) + ((@KontrolEdilecekMiktar * (((@Miktar * @BirimFiyat)-@ToplamIskonto) / @Miktar)) * @KDVOran / 100)		

				END
			END
			ELSE BEGIN
				SET @Kod13 = 0
				SET @Kod14 = 0
			END			
	
			UPDATE FINSAT633.FINSAT633.SPI SET Kod10=@Kod10,Kod3=@Kod3,Kod14=@Kod14,Kod13=@Kod13 WHERE EvrakNo=@EvrakNo and Chk=@HesapKodu AND MalKodu=@MalKodu and SiraNo=@SiraNo
		
		END
		FETCH NEXT FROM @C1 INTO @IslemTur,@IslemTip,@EvrakNo,@Depo,@MalKodu,@Miktar,@BirimFiyat,@ToplamIskonto,@KDVOran,@TeslimMiktar,@KapatilanMiktar,@Chk,@SiraNo,@SiparisDurumu,@Kod4,@Kod10,@Kod13,@KynkEvrakTip,@CheckSum
	END
	CLOSE @C1
	DEALLOCATE @C1
	
	if ((select count(Kod10) from FINSAT633.FINSAT633.SPI (nolock)t  where t.Chk=@HesapKodu and t.SiparisDurumu=0 and t.Kod10='Beklemede' and t.EvrakNo=@EvrakNo) > 0)
	begin	
	
	UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Beklemede' where EvrakNo=@EvrakNo and Chk=@HesapKodu


	end
END












GO
/****** Object:  StoredProcedure [FINSAT633].[sp_SiparisKontrol_20102017_RENAME]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [FINSAT633].[sp_SiparisKontrol_20102017_RENAME]
(
	@SiparisEvrakNo VARCHAR(8)
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @SiparisBak NUMERIC(25,6),@KontrolEdilecekMiktar NUMERIC(25,6),@DepoStokMiktari NUMERIC(25,6),@ToplamSiparisBakiye NUMERIC(25,6)
	
	DECLARE @KrediLimiti NUMERIC(25,6),@Bakiye NUMERIC(25,6),@SahsiCek NUMERIC(25,6),@MusteriCek NUMERIC(25,6),@Sonuc VARCHAR(30),@Kod3OrtGun NUMERIC(25,6),@Kod3OrtBakiye NUMERIC(25,6),@OrtalamaGun NUMERIC(25,6),@SiparisBakiyesi NUMERIC(25,6),@ChkUpdateDegeri VARCHAR(20)
	SET @Kod3OrtGun = 0 SET @Kod3OrtBakiye = 0 SET @OrtalamaGun = 0 SET @SiparisBakiyesi = 0 SET @ChkUpdateDegeri='' SET @Sonuc=''
	
	DECLARE @BakilacakGun INT,@Index INT
	
	SET @BakilacakGun = 0 SET @Index = 0 
	
	DECLARE @IslemTur SMALLINT,@IslemTip SMALLINT, @EvrakNo VARCHAR(8),@Depo VARCHAR(5),@MalKodu VARCHAR(30),@Miktar NUMERIC(25,6),@BirimFiyat NUMERIC(25,6), @ToplamIskonto NUMERIC(25,6),@KDVOran REAL,@TeslimMiktar NUMERIC(25,6),@KapatilanMiktar NUMERIC(25,6),@Chk VARCHAR(20), @SiraNo SMALLINT,@SiparisDurumu SMALLINT,@Kod4 VARCHAR(10),@Kod10 VARCHAR(10),@Kod13 NUMERIC(25,6),@OzelKod VARCHAR(10),@HesapKodu VARCHAR(20),@ChkKod2 VARCHAR(10),@Kod3 VARCHAR(10),@Kod14 NUMERIC(25,6), @KynkEvrakTip SMALLINT,@CheckSum SMALLINT

	DECLARE @BakiyeHesapFlag BIT,@Kod3OrtGunFlag BIT
	SET @BakiyeHesapFlag = 0 SET @Kod3OrtGunFlag = 0

	
	DECLARE C1 CURSOR FOR
	SELECT IslemTur,IslemTip,EvrakNo,Depo,MalKodu,Miktar,BirimFiyat,ToplamIskonto,KDVOran,TeslimMiktar,KapatilanMiktar,Chk,SiraNo,SiparisDurumu,Kod4,Kod10,Kod13,KynkEvrakTip,[CheckSum] FROM FINSAT633.FINSAT633.SPI WHERE EvrakNo=@SiparisEvrakNo
	
	OPEN C1
	FETCH NEXT FROM C1 INTO @IslemTur,@IslemTip,@EvrakNo,@Depo,@MalKodu,@Miktar,@BirimFiyat,@ToplamIskonto,@KDVOran,@TeslimMiktar,@KapatilanMiktar,@Chk,@SiraNo,@SiparisDurumu,@Kod4,@Kod10,@Kod13,@KynkEvrakTip,@CheckSum
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		IF 	@KynkEvrakTip = 62 AND @CheckSum <> 13 AND @Kod10 <> 'Reddedildi' AND @SiparisDurumu=0 AND @TeslimMiktar+@KapatilanMiktar<@Miktar AND @IslemTur=1 AND @IslemTip=1 AND @Kod4<>'1'
		BEGIN
			SET @SiparisBak = ((((@Miktar * @BirimFiyat)-@ToplamIskonto) * @KDVOran) / 100) + ((@Miktar * @BirimFiyat) - @ToplamIskonto)
			SET @KontrolEdilecekMiktar = @Miktar - (@KapatilanMiktar + @TeslimMiktar)
			SELECT @DepoStokMiktari = (DvrMiktar+(GirMiktar-CikMiktar)) FROM FINSAT633.FINSAT633.DST WHERE MalKodu=@MalKodu AND Depo=@Depo
			IF @KontrolEdilecekMiktar > @DepoStokMiktari
				SET @ToplamSiparisBakiye = (@DepoStokMiktari * ((@Miktar * @BirimFiyat) -@ToplamIskonto) / @Miktar) +(@DepoStokMiktari * (((@Miktar * @BirimFiyat)- @ToplamIskonto) / @Miktar) * @KDVOran / 100);
			ELSE
			SET @ToplamSiparisBakiye = (@KontrolEdilecekMiktar * ((@Miktar * @BirimFiyat)-@ToplamIskonto)) + ((@KontrolEdilecekMiktar *(((@Miktar * @BirimFiyat) - @ToplamIskonto) / @Miktar )) * @Miktar / 100)
			
			SELECT @OzelKod=OzelKod,@HesapKodu=HesapKodu,@ChkKod2=Kod2 FROM FINSAT633.FINSAT633.CHK WHERE HesapKodu=@Chk
			IF @ChkKod2='8'
			BEGIN
				SET @Sonuc = 'Beklemede'
			END
			ELSE IF @OzelKod='2'
			BEGIN
				IF @BakiyeHesapFlag = 0
				BEGIN
					EXECUTE FINSAT633.sp_BakiyeHesap @HesapKodu,@OKrediLimiti=@KrediLimiti OUTPUT ,@OBakiye=@Bakiye OUTPUT ,@OSahsiCek=@SahsiCek OUTPUT ,@OMusteriCek=@MusteriCek OUTPUT
					SET @BakiyeHesapFlag = 1
				END
				IF @KrediLimiti IS NULL
					SET @Sonuc = 'Terminal'
				IF @Kod3OrtGunFlag = 0
				BEGIN
					EXECUTE FINSAT633.sp_Kod3OrtGun @HesapKodu,@ChkKod2,@OKod3OrtGun=@Kod3OrtGun OUTPUT, @OKod3OrtBakiye=@Kod3OrtBakiye OUTPUT, @OOrtalamaGun=@OrtalamaGun OUTPUT, @OBakilacakGun=@BakilacakGun OUTPUT
					SET @Kod3OrtGunFlag = 1
				END								
							
				
				--IF @Sonuc <> 'Terminal'	--if (sonuc.contains('terminal') return sonuc
				--BEGIN
					select @SiparisBakiyesi=isnull(SUM(t.Kod14),0) from FINSAT633.FINSAT633.SPI (nolock)t  where t.Chk=@HesapKodu and t.SiparisDurumu=0 and t.Kod10<>'Reddedildi'
					SET  @SiparisBakiyesi = @SiparisBakiyesi + @SiparisBak													
					if (@Bakiye >= 1000)
					begin
						IF @Bakiye >= (@KrediLimiti / 10)
						BEGIN							
							IF @OrtalamaGun > @BakilacakGun
							BEGIN								
								SET @ChkUpdateDegeri = @ChkUpdateDegeri + '3'
								set @Sonuc = 'Beklemede'
							end
							else begin
								IF (@Kod3OrtBakiye >= (@KrediLimiti / 10)) and @Kod3OrtGun > @BakilacakGun
								BEGIN
									SET @ChkUpdateDegeri = @ChkUpdateDegeri + '3'
									SET @Sonuc = 'Beklemede'
								END
								else begin
									set @Sonuc='Terminal'
								end
							end
						END
						else begin
							set @Sonuc='Terminal'
						end
					end
					else begin
						set @Sonuc = 'Terminal'
					end										
											
					IF @KrediLimiti < (@Bakiye + @SahsiCek + (@MusteriCek / 4) + @SiparisBakiyesi)
					BEGIN
						SET @ChkUpdateDegeri =@ChkUpdateDegeri + '5'
						set @Sonuc = 'Beklemede'
					END
					
					DECLARE @Sart3Mu BIT SET @Sart3Mu = 0
					IF PATINDEX('%9%',@ChkKod2) > 0 OR PATINDEX('%8%',@ChkKod2) > 0 OR PATINDEX('%7%',@ChkKod2) > 0 OR PATINDEX('%6%',@ChkKod2) > 0 OR PATINDEX('%4%',@ChkKod2) > 0 OR PATINDEX('%2%',@ChkKod2) > 0
					BEGIN
						SET @Sonuc = 'Beklemede'
						SET @Sart3Mu = 1
					END
					IF @ChkUpdateDegeri <> ''
					BEGIN
						DECLARE @TopDeger VARCHAR(20) SET @TopDeger = @ChkUpdateDegeri + @ChkKod2
						DECLARE @UpdateEdilecekDeger VARCHAR(20)
						SET @UpdateEdilecekDeger = ''
						IF @ChkKod2 = '' OR @ChkKod2 IS NULL
							SET @UpdateEdilecekDeger = @ChkUpdateDegeri
						ELSE
						BEGIN
							SET @Index = 1
							DECLARE @V INT
							CREATE TABLE #T (C INT)
							WHILE @Index < LEN(@TopDeger)
							BEGIN
								INSERT INTO #T VALUES(CAST(SUBSTRING(@TopDeger,@Index,1) AS INT))
								SET @Index = @Index + 1;
							END
							DECLARE CUR2 CURSOR FOR SELECT C FROM #T ORDER BY C DESC
							OPEN CUR2
							FETCH NEXT FROM CUR2 INTO @V
							WHILE @@FETCH_STATUS = 0
							BEGIN
								IF PATINDEX('%'+CAST(@V AS VARCHAR(1))+'%',@UpdateEdilecekDeger) =  0
									SET @UpdateEdilecekDeger = @UpdateEdilecekDeger + CAST(@V AS VARCHAR(1))
								FETCH NEXT FROM CUR2 INTO @V
							END
							CLOSE CUR2
							DEALLOCATE CUR2
							DROP TABLE #T							
						END
						Update FINSAT633.FINSAT633.CHK SET Kod2=@UpdateEdilecekDeger where HesapKodu=@HesapKodu
					END
					IF @ChkUpdateDegeri = '' AND @Sart3Mu = 0
					BEGIN
						SET @Sonuc = 'Terminal'
					END
				--END				
			END
			ELSE
				SET @Sonuc = 'Terminal'
			
			SET @Kod10 = @Sonuc
			SET @Kod3='Tüm'
			
			
			IF @DepoStokMiktari > 0
			BEGIN
				IF @KontrolEdilecekMiktar > @DepoStokMiktari
				BEGIN
					SET @Kod13 = @DepoStokMiktari
					SET @Kod14 = (@DepoStokMiktari * (((@Miktar * @BirimFiyat)-@ToplamIskonto) / @Miktar)) + ((@DepoStokMiktari * (((@Miktar * @BirimFiyat)-@ToplamIskonto)/@Miktar))) * @KDVOran / 100
				END
				ELSE BEGIN
					SET @Kod13 = @KontrolEdilecekMiktar
					SET @Kod14 = (@KontrolEdilecekMiktar * (((@Miktar * @BirimFiyat)-@ToplamIskonto)/@Miktar)) + ((@KontrolEdilecekMiktar * (((@Miktar * @BirimFiyat)-@ToplamIskonto) / @Miktar)) * @KDVOran / 100)		

				END
			END
			ELSE BEGIN
				SET @Kod13 = 0
				SET @Kod14 = 0
			END			
			UPDATE FINSAT633.FINSAT633.SPI SET Kod10=@Kod10,Kod3=@Kod3,Kod14=@Kod14,Kod13=@Kod13 WHERE EvrakNo=@EvrakNo and Chk=@HesapKodu AND MalKodu=@MalKodu and SiraNo=@SiraNo
		END
		FETCH NEXT FROM C1 INTO @IslemTur,@IslemTip,@EvrakNo,@Depo,@MalKodu,@Miktar,@BirimFiyat,@ToplamIskonto,@KDVOran,@TeslimMiktar,@KapatilanMiktar,@Chk,@SiraNo,@SiparisDurumu,@Kod4,@Kod10,@Kod13,@KynkEvrakTip,@CheckSum
	END
	CLOSE C1
	DEALLOCATE C1
	
	if ((select count(Kod10) from FINSAT633.FINSAT633.SPI (nolock)t  where t.Chk=@HesapKodu and t.SiparisDurumu=0 and t.Kod10='Beklemede' and t.EvrakNo=@EvrakNo) > 0)
	begin	
	UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Beklemede' where EvrakNo=@EvrakNo and Chk=@HesapKodu
	end
END








GO
/****** Object:  StoredProcedure [FINSAT633].[spBakiyeOlusturanHareketlerFatura]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [FINSAT633].[spBakiyeOlusturanHareketlerFatura]
@HesapKodu VARCHAR(MAX),
@Bakiye DECIMAL(24, 6)
AS
BEGIN
	
	IF OBJECT_ID('tempdb..#LocalTempTablo') IS NOT NULL DROP TABLE #LocalTempTablo
	CREATE TABLE #LocalTempTablo
	(
		Hareket int,
		Borc2 DECIMAL(24, 6),
		Alacak2 DECIMAL(24, 6),
		EvrakNo VARCHAR(MAX),
		Tarih VARCHAR(MAX),
		Tarih2 int,
		Alacak DECIMAL(24, 6),
		Borc DECIMAL(24, 6),
		VadeTarih VARCHAR(MAX),
		VadeTarih2 int
	)
	
	DECLARE @BorcAlacak BIT
	
	IF( @Bakiye > 0)
	BEGIN
		
		SET @BorcAlacak = 0

	END
	ELSE
	BEGIN

		SET @BorcAlacak = 1
		SET @Bakiye = @Bakiye * -1

	END

	--DECLARE @HesapKodu VARCHAR(MAX)
	--SET @HesapKodu = '1387100'

	--DECLARE @Bakiye DECIMAL(24, 6)
	--SET @Bakiye = 25943

	DECLARE @Hareket int
	DECLARE @Borc2 DECIMAL(24, 6)
	DECLARE @Alacak2 DECIMAL(24, 6)
	DECLARE @EvrakNo VARCHAR(MAX)
	DECLARE @Tarih VARCHAR(MAX)
	DECLARE @Tarih2 int
	DECLARE @Alacak DECIMAL(24, 6)
	DECLARE @Borc DECIMAL(24, 6)
	DECLARE @VadeTarih VARCHAR(MAX)
	DECLARE @VadeTarih2 int

	DECLARE CRS CURSOR FOR

		SELECT * FROM
		(
			SELECT  CAST(-1 AS INT ) AS Hareket,
			sum((CASE WHEN KarsiHesapKodu LIKE @HesapKodu THEN BA*Tutar ELSE abs(BA - 1)*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc2, 
			sum((CASE WHEN KarsiHesapKodu LIKE @HesapKodu  THEN abs(BA - 1)*Tutar ELSE BA*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak2, 
			EvrakNo,
			MIN(convert(varchar(15),convert(datetime,Tarih-2),104)) AS Tarih, MIN(Tarih) as Tarih2,
			SUM((CASE CHI.IslemTip WHEN 5 THEN - CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * BA) AS Alacak, 
			SUM(((CASE CHI.IslemTip WHEN 5 THEN - CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * (BA - 1)) * (BA - 1)) AS Borc, 
			MIN(convert(varchar(15),convert(datetime,VadeTarih-2),104)) AS VadeTarih, MIN(VadeTarih) as VadeTarih2
			FROM FINSAT633.FINSAT633.CHI (NOLOCK) CHI 
			WHERE (IslemTip <> 16) and (IslemTip <> 20) and (HesapKodu like @HesapKodu ) AND (IslemTip <> 21) OR (IslemTip <> 21) AND (KarsiHesapKodu like @HesapKodu )  
			--and CHI.VadeTarih > convert(int,convert(datetime,convert(nvarchar(10),cast(getdate() as datetime),104),104))+2
			GROUP BY EvrakNo 

			union all 

			SELECT CAST(-1 AS INT ) AS Hareket,
			sum((CASE WHEN KarsiHesapKodu LIKE @HesapKodu  THEN BA*Tutar ELSE abs(BA - 1)*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc2, 
			sum((CASE WHEN KarsiHesapKodu LIKE @HesapKodu  THEN abs(BA - 1)*Tutar ELSE BA*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak2, 
			EvrakNo, 
			MIN(convert(varchar(15),convert(datetime,Tarih-2),104)) AS Tarih, 
			MIN(Tarih) as Tarih2,SUM((CASE CHI.IslemTip WHEN 5 THEN - CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * BA) AS Alacak, 
			SUM(((CASE CHI.IslemTip WHEN 5 THEN - CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * (BA - 1)) * (BA - 1)) AS Borc, 
			MIN(convert(varchar(15),convert(datetime,VadeTarih-2),104)) AS VadeTarih , MIN(VadeTarih) as VadeTarih2
			FROM FINSAT633.FINSAT633.CHI (NOLOCK) CHI 
			WHERE (IslemTip = 20) and (HesapKodu like @HesapKodu ) 
			--and CHI.VadeTarih > convert(int,convert(datetime,convert(nvarchar(10),cast(getdate() as datetime),104),104))+2 
			GROUP BY EvrakNo 

			union all 

			SELECT CAST(-1 AS INT ) AS Hareket,
			sum((CASE WHEN KarsiHesapKodu LIKE @HesapKodu  THEN abs(BA - 1)*Tutar ELSE BA*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc2, 
			sum((CASE WHEN KarsiHesapKodu LIKE @HesapKodu  THEN BA*Tutar ELSE abs(BA - 1)*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak2, 
			EvrakNo,MIN(convert(varchar(15),convert(datetime,Tarih-2),104)) AS Tarih,MIN(Tarih) as Tarih2,
			SUM(((CASE CHI.IslemTip WHEN 5 THEN -CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * (BA - 1)) * (BA - 1)) AS Alacak, 
			SUM((CASE CHI.IslemTip WHEN 5 THEN - CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * BA) AS Borc, 
			MIN(convert(varchar(15),convert(datetime,VadeTarih-2),104)) AS VadeTarih  , MIN(VadeTarih) as VadeTarih2
			FROM FINSAT633.FINSAT633.CHI (NOLOCK) CHI 
			WHERE (KarsiHesapKodu like @HesapKodu ) and (IslemTip = 3) and (KarsiHesapKodu <> HesapKodu) and (left(HesapKodu,7)=left(KarsiHesapKodu,7)) 
			--and CHI.VadeTarih > convert(int,convert(datetime,convert(nvarchar(10),cast(getdate() as datetime),104),104))+2 
			GROUP BY EvrakNo 
		) A
		--WHERE A.Borc2 > 0
		ORDER BY A.Tarih2 desc, A.EvrakNo desc
	
	OPEN CRS

	FETCH NEXT FROM CRS INTO @Hareket, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2

	WHILE @@FETCH_STATUS =0
		BEGIN
			
				IF( @BorcAlacak = 0)
				BEGIN
						
					IF( @Borc2 <= 0)
					FETCH NEXT FROM CRS INTO @Hareket, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2

					IF( @Bakiye > @Borc2)
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, Borc, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2)

						SET @Bakiye = @Bakiye - @Borc2

					END
					ELSE 
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, Borc, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @Bakiye, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2)

						SET @Bakiye = 0

					END
				END
				ELSE
				BEGIN
					
					IF( @Alacak2 <= 0)
					FETCH NEXT FROM CRS INTO @Hareket, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2

					IF( @Bakiye > @Alacak2)
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, Borc, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2)

						SET @Bakiye = @Bakiye - @Alacak2

					END
					ELSE 
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, Borc, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @Borc2, @Bakiye, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2)

						SET @Bakiye = 0

					END
				END

				IF( @Bakiye <= 0)
					BREAK


				FETCH NEXT FROM CRS INTO @Hareket, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @Borc, @VadeTarih, @VadeTarih2

		END
	
	CLOSE CRS

	DEALLOCATE CRS


	SELECT * FROM #LocalTempTablo

END



GO
/****** Object:  StoredProcedure [FINSAT633].[spBakiyeOlusturanHareketlerPOS]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [FINSAT633].[spBakiyeOlusturanHareketlerPOS]
@HesapKodu VARCHAR(MAX),
@Bakiye DECIMAL(24, 6)
AS
BEGIN
	
	IF OBJECT_ID('tempdb..#LocalTempTablo') IS NOT NULL DROP TABLE #LocalTempTablo
	CREATE TABLE #LocalTempTablo
	(
		Hareket int,
		Hesap VARCHAR(MAX),
		Borc DECIMAL(24, 6),
		VadeTarih int,
		VadeGun VARCHAR(MAX)
	)

	--DECLARE @HesapKodu VARCHAR(MAX)
	--SET @HesapKodu = '1387100'

	--DECLARE @Bakiye DECIMAL(24, 6)
	--SET @Bakiye = 25943

	DECLARE @Hareket int
	DECLARE @Hesap VARCHAR(MAX)
	DECLARE @Borc DECIMAL(24, 6)
	DECLARE @VadeTarih int
	DECLARE @VadeGun VARCHAR(MAX)

	DECLARE CRS CURSOR FOR

		select '' AS Hareket,CHK.HesapKodu+' - '+CHK.Unvan1 as Hesap ,(isnull(sum(case when CHI.BA = 0 then CHI.Tutar else -CHI.Tutar end),0)) as [Borc] , 
		VadeTarih, convert(datetime,convert(nvarchar(10),cast(VadeTarih-2 as datetime),104),104) as 'VadeGun'
		from 
		( 
		select  KarsiHesapKodu as HesapKodu, 
		(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, 
		(case when BA = 0 then 1 else 0 end) as BA, VadeTarih,Tarih
		From FINSAT633.FINSAT633.CHI (nolock) where 
		KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42)  

		Union All 
		Select  HesapKodu, 
		(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar, 
		BA,VadeTarih,Tarih
		From FINSAT633.FINSAT633.CHI (nolock) where 
		IslemTip not in (16,21,27,32,36,37,41,42)
		) CHI 
		right join 
		FINSAT633.FINSAT633.CHK CHK (nolock) ON CHK.HesapKodu = CHI.HesapKodu  
		where 
		CHK.HesapKodu=@HesapKodu and BA=0
		group by CHK.Kod9,CHK.HesapKodu,CHK.Unvan1,VadeTarih,Tarih 
		having  (isnull(sum(case when CHI.BA = 0 then CHI.Tutar else -CHI.Tutar end),0)) > 0  or   (isnull(sum(case when CHI.BA = 0 then CHI.Tutar else -CHI.Tutar end),0)) < 0
		order by Tarih desc
	
	OPEN CRS

	FETCH NEXT FROM CRS INTO @Hareket, @Hesap, @Borc, @VadeTarih, @VadeGun

	WHILE @@FETCH_STATUS =0
		BEGIN
			
				IF( @Bakiye > @Borc)
				BEGIN

					INSERT INTO #LocalTempTablo
					(Hareket, Hesap, Borc, VadeTarih, VadeGun) 
					VALUES 
					(@Hareket, @Hesap, @Borc, @VadeTarih, @VadeGun)

					SET @Bakiye = @Bakiye - @Borc

				END
				ELSE 
				BEGIN

					INSERT INTO #LocalTempTablo
					(Hareket, Hesap, Borc, VadeTarih, VadeGun) 
					VALUES 
					(@Hareket, @Hesap, @Bakiye, @VadeTarih, @VadeGun)

					SET @Bakiye = 0

				END

				IF( @Bakiye <= 0)
					BREAK


				FETCH NEXT FROM CRS INTO @Hareket, @Hesap, @Borc, @VadeTarih, @VadeGun

		END
	
	CLOSE CRS

	DEALLOCATE CRS


	SELECT * FROM #LocalTempTablo

END



GO
/****** Object:  StoredProcedure [FINSAT633].[spBakiyeOlusturanHareketlerTedarikci]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [FINSAT633].[spBakiyeOlusturanHareketlerTedarikci]
@HesapKodu VARCHAR(MAX),
@Bakiye DECIMAL(24, 6)
AS
BEGIN
	
	IF OBJECT_ID('tempdb..#LocalTempTablo') IS NOT NULL DROP TABLE #LocalTempTablo
	CREATE TABLE #LocalTempTablo
	(
		Hareket int,
		HesapKodu VARCHAR(MAX),
		Borc2 DECIMAL(24, 6),
		Alacak2 DECIMAL(24, 6),
		EvrakNo VARCHAR(MAX),
		Tarih VARCHAR(MAX),
		Tarih2 int,
		Alacak DECIMAL(24, 6),
		VadeTarih VARCHAR(MAX),
		VadeTarih2 int
	)

	DECLARE @BorcAlacak BIT
	
	IF( @Bakiye > 0)
	BEGIN
		
		SET @BorcAlacak = 0

	END
	ELSE
	BEGIN

		SET @BorcAlacak = 1
		SET @Bakiye = @Bakiye * -1

	END

	--DECLARE @HesapKodu VARCHAR(MAX)
	--SET @HesapKodu = '1387100'

	--DECLARE @Bakiye DECIMAL(24, 6)
	--SET @Bakiye = 25943

	DECLARE @Hareket int
	DECLARE @HesapKodu2 VARCHAR(MAX)
	DECLARE @Borc2 DECIMAL(24, 6)
	DECLARE @Alacak2 DECIMAL(24, 6)
	DECLARE @EvrakNo VARCHAR(MAX)
	DECLARE @Tarih VARCHAR(MAX)
	DECLARE @Tarih2 int
	DECLARE @Alacak DECIMAL(24, 6)
	DECLARE @VadeTarih VARCHAR(MAX)
	DECLARE @VadeTarih2 int

	DECLARE CRS CURSOR FOR

			select * from (
			SELECT '' AS Hareket, HesapKodu, SUM((abs(BA-1)*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) Borc2, SUM((BA*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) Alacak2, EvrakNo,MIN(convert(varchar(15),convert(datetime,Tarih-2),104)) AS Tarih, MIN(Tarih) as Tarih2, SUM(((CASE CHI.IslemTip WHEN 5 THEN - CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * (BA - 1)) * (BA - 1)) AS Alacak, MIN(convert(varchar(15),convert(datetime,VadeTarih-2),104)) AS VadeTarih,MIN(VadeTarih) as VadeTarih2
			FROM FINSAT633.FINSAT633.CHI (NOLOCK)
			WHERE ((IslemTip <> 16) and (IslemTip <> 20) and  (HesapKodu like @HesapKodu ) AND (IslemTip <> 21) )
			--WHERE (IslemTip <> 16) and (IslemTip <> 20) and (HesapKodu like @HesapKodu ) AND (IslemTip <> 21) OR (IslemTip <> 21) AND (KarsiHesapKodu like @HesapKodu ) 
			GROUP BY EvrakNo, [HesapKodu] 
			union all 
			SELECT '' AS Hareket, KarsiHesapKodu HesapKodu, SUM((BA*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) Borc2, SUM((abs(BA-1)*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) Alacak2, EvrakNo,MIN(convert(varchar(15),convert(datetime,Tarih-2),104))  AS Tarih, MIN(Tarih) as Tarih2, SUM(((CASE CHI.IslemTip WHEN 5 THEN - CHI.Tutar WHEN 9 THEN - CHI.Tutar ELSE CHI.Tutar END) * (BA - 1)) * (BA - 1)) AS Alacak, MIN(convert(varchar(15),convert(datetime,VadeTarih-2),104)) AS VadeTarih,MIN(VadeTarih) as VadeTarih2 
			FROM FINSAT633.FINSAT633.CHI (NOLOCK)
			WHERE ((IslemTip <> 21) AND (KarsiHesapKodu like @HesapKodu ))
			GROUP BY EvrakNo, KarsiHesapKodu
			union all
			SELECT '' as Hareket, HesapKodu, sum((abs(BA - 1)*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc2,  sum((BA*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak2, EvrakNo, MIN(convert(varchar(15),convert(datetime,Tarih-2),104)) AS Tarih, MIN(Tarih) as Tarih2, SUM(((CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) * (BA - 1)) * (BA - 1)) AS Alacak, MIN(convert(varchar(15),convert(datetime,VadeTarih-2),104)) AS VadeTarih,MIN(VadeTarih) as VadeTarih2 FROM FINSAT633.FINSAT633.CHI (NOLOCK) WHERE (IslemTip = 20) and (HesapKodu like @HesapKodu ) GROUP BY EvrakNo,HesapKodu 
			union all
			SELECT '' as Hareket, KarsiHesapKodu as [HesapKodu] ,sum((abs(BA - 1)*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc2, sum((abs(BA - 1)*Tutar)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak2, EvrakNo,MIN(convert(varchar(15),convert(datetime,Tarih-2),104)) AS Tarih,MIN(Tarih) as Tarih2,SUM((CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) * BA) AS Alacak, MIN(convert(varchar(15),convert(datetime,VadeTarih-2),104)) AS VadeTarih,MIN(VadeTarih) as VadeTarih2  FROM FINSAT633.FINSAT633.CHI (NOLOCK) WHERE (KarsiHesapKodu like @HesapKodu ) and (IslemTip = 3) and (KarsiHesapKodu <> HesapKodu) and (left(HesapKodu,7)=left(KarsiHesapKodu,7))  GROUP BY EvrakNo, KarsiHesapKodu
			) a  GROUP BY a.Hareket,a.HesapKodu,a.Alacak2,a.Borc2,a.EvrakNo,a.Tarih,a.Tarih2,a.Alacak,a.VadeTarih,a.VadeTarih2 ORDER BY HesapKodu, Tarih2 desc, EvrakNo desc
	
	OPEN CRS

	FETCH NEXT FROM CRS INTO @Hareket, @HesapKodu2, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2

	WHILE @@FETCH_STATUS =0
		BEGIN
			
				
				IF( @BorcAlacak = 0)
				BEGIN

					IF( @Borc2 <= 0)
						FETCH NEXT FROM CRS INTO @Hareket, @HesapKodu2, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2

					IF( @Bakiye > @Borc2)
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, HesapKodu, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @HesapKodu2, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2)

						SET @Bakiye = @Bakiye - @Borc2

					END
					ELSE 
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, HesapKodu, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @HesapKodu2, @Bakiye, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2)

						SET @Bakiye = 0

					END

				END
				ELSE
				BEGIN
					
					IF( @Alacak2 <= 0)
						FETCH NEXT FROM CRS INTO @Hareket, @HesapKodu2, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2

					IF( @Bakiye > @Alacak2)
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, HesapKodu, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @HesapKodu2, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2)

						SET @Bakiye = @Bakiye - @Alacak2

					END
					ELSE 
					BEGIN

						INSERT INTO #LocalTempTablo
						(Hareket, HesapKodu, Borc2, Alacak2, EvrakNo, Tarih, Tarih2, Alacak, VadeTarih, VadeTarih2) 
						VALUES 
						(@Hareket, @HesapKodu2, @Borc2, @Bakiye, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2)

						SET @Bakiye = 0

					END

				END

				IF( @Bakiye <= 0)
					BREAK


				FETCH NEXT FROM CRS INTO @Hareket, @HesapKodu2, @Borc2, @Alacak2, @EvrakNo, @Tarih, @Tarih2, @Alacak, @VadeTarih, @VadeTarih2

		END
	
	CLOSE CRS

	DEALLOCATE CRS


	SELECT * FROM #LocalTempTablo

END



GO
/****** Object:  StoredProcedure [wms].[AlimIptalDetail]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 03.10.2017
-- Modify date: 03.10.2017
-- Description:	Alımdan iade evrak ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[AlimIptalDetail]
	@EvrakNo varchar(16),
	@DepoKodu varchar(5),
	@CHK varchar(20)

AS
BEGIN
	
	SELECT 
'33' as SirketID, 
STI.EvrakNo, 
STI.Chk, 
CHK.Unvan1 AS Unvan, 
STI.MalKodu, 
STK.MalAdi, 
STK.Birim1 AS Birim,
SUM(STI.Miktar-STI.ErekIIFMiktar) AS Miktar, 
STI.Depo,
wms.getStockByDepo(STI.MalKodu,@DepoKodu) as Stok,
BIRIKIM.wms.fnGetStock(STI.Depo, STI.MalKodu, STK.Birim1,0) AS WmsStok,
BIRIKIM.wms.fnGetRezervStock(@DepoKodu,STI.MalKodu,STK.Birim1) AS WmsRezerv 
FROM FINSAT633.FINSAT633.STI WITH(NOLOCK) 
INNER JOIN FINSAT633.FINSAT633.STK WITH(NOLOCK) ON STI.MalKodu = STK.MalKodu 
INNER JOIN FINSAT633.FINSAT633.MFK WITH(NOLOCK) ON STI.EvrakNo = MFK.EvrakNo  AND STI.Chk = MFK.HesapKod AND STI.KynkEvrakTip = MFK.KynkEvrakTip 
INNER JOIN FINSAT633.FINSAT633.CHK WITH(NOLOCK) ON STI.Chk = CHK.HesapKodu 
WHERE (STI.Depo = @DepoKodu) AND (STI.KynkEvrakTip = 4) AND (STI.Chk=@CHK) AND (STI.EvrakNo=@EvrakNo)
GROUP BY STI.EvrakNo,STI.MalKodu, STI.Chk, CHK.Unvan1,STI.Depo,STK.MalAdi, STK.Birim1

END




GO
/****** Object:  StoredProcedure [wms].[AlimIptalSecimList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 03.10.2017
-- Modify date: 03.10.2017
-- Description:	Alımdan iade evrak ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[AlimIptalSecimList]
	@EvrakNo varchar(16),
	@DepoKodu varchar(5),
	@CHK varchar(20)

AS
BEGIN
	
SELECT 
'33' AS SirketID, 
STI.ROW_ID AS ID, 
STI.EvrakNo, 
STI.Tarih, 
STI.MalKodu, 
STK.MalAdi, 
STI.Miktar-STI.ErekIIFMiktar AS Miktar, 
STI.Birim, 
wms.getStockByDepo(STI.MalKodu,@DepoKodu) as GunesStok,
BIRIKIM.wms.fnGetStock(STI.Depo, STI.MalKodu, STK.Birim1,0) AS WmsStok, 
BIRIKIM.wms.fnGetRezervStock(@DepoKodu,STI.MalKodu,STK.Birim1) AS WmsRezerv 
FROM FINSAT633.FINSAT633.STI WITH(NOLOCK) 
INNER JOIN FINSAT633.FINSAT633.STK WITH(NOLOCK) ON STI.MalKodu = STK.MalKodu 
INNER JOIN FINSAT633.FINSAT633.MFK WITH(NOLOCK) ON STI.EvrakNo = MFK.EvrakNo  AND STI.Chk = MFK.HesapKod AND STI.KynkEvrakTip = MFK.KynkEvrakTip 
INNER JOIN FINSAT633.FINSAT633.CHK WITH(NOLOCK) ON STI.Chk = CHK.HesapKodu 
WHERE (STI.Depo = @DepoKodu) AND (STI.KynkEvrakTip = 4) AND (STI.Chk=@CHK) AND (STI.EvrakNo=@EvrakNo)

END




GO
/****** Object:  StoredProcedure [wms].[AlimIptalSiparisList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 03.10.2017
-- Modify date: 03.10.2017
-- Description:	Alımdan iade evrak ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[AlimIptalSiparisList]
	@DepoKodu varchar(5),
	@CHK varchar(20),
	@BasTarih int,
	@BitTarih int

AS
BEGIN
	
SELECT 
'33' as SirketID, 
STI.EvrakNo, 
STI.Tarih, 
STI.Chk, 
CHK.Unvan1 AS Unvan, 
CHK.GrupKod, 
CHK.FaturaAdres3 AS FaturaAdres, 
MFK.Aciklama, 
COUNT(STI.MalKodu) AS Çeşit, 
SUM(STI.Miktar-STI.ErekIIFMiktar) AS Miktar,
MIN(STI.KayitSaat) as Saat 
FROM FINSAT633.FINSAT633.STI WITH(NOLOCK) 
INNER JOIN FINSAT633.FINSAT633.STK WITH(NOLOCK) ON STI.MalKodu = STK.MalKodu 
INNER JOIN FINSAT633.FINSAT633.MFK WITH(NOLOCK) ON STI.EvrakNo = MFK.EvrakNo AND STI.Chk = MFK.HesapKod AND STI.KynkEvrakTip = MFK.KynkEvrakTip 
INNER JOIN FINSAT633.FINSAT633.CHK WITH(NOLOCK) ON STI.Chk = CHK.HesapKodu 
WHERE (STI.Tarih >= @BasTarih) AND (STI.Tarih <= @BitTarih) AND (STI.Depo = @DepoKodu) AND (STI.KynkEvrakTip = 4) AND (STI.Chk=@CHK)
GROUP BY STI.EvrakNo, STI.Tarih, STI.Chk, CHK.Unvan1, CHK.GrupKod, CHK.FaturaAdres3, MFK.Aciklama

END





GO
/****** Object:  StoredProcedure [wms].[BaglantiDetaySelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[BaglantiDetaySelect]
@ListeNo VARCHAR(30)
AS
BEGIN
	
	SELECT Kod10, (CASE MalKodGrup WHEN 0 THEN 'Mal Kodu' WHEN 1 THEN 'Grup Kodu' 
	       WHEN 2 THEN 'Tip Kodu' WHEN 3 THEN 'Özel Kod' WHEN 4 THEN 'Kod1' WHEN 5 THEN 'Kod2' 
		   WHEN 6 THEN 'Kod3' WHEN 7 THEN 'Kod4' END) AS MalKodGrup, 
		   MalKod,(SELECT MalAdi FROM FINSAT633.FINSAT633.STK(NOLOCK) WHERE MalKodu=MalKod) as MalAdi, Oran1, Oran2, Oran3, Oran4, Oran5 FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) 
    WHERE ListeNo=@ListeNo

END









GO
/****** Object:  StoredProcedure [wms].[BaglantiDetaySelect1]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[BaglantiDetaySelect1]
@ListeNo VARCHAR(30)
AS
BEGIN
	
	SELECT Kod10, (CASE MalKodGrup WHEN 0 THEN 'Mal Kodu' WHEN 1 THEN 'Grup Kodu' 
	       WHEN 2 THEN 'Tip Kodu' WHEN 3 THEN 'Özel Kod' WHEN 4 THEN 'Kod1' WHEN 5 THEN 'Kod2' 
		   WHEN 6 THEN 'Kod3' WHEN 7 THEN 'Kod4' END) AS MalKodGrup, 
		   MalKod,(SELECT MalAdi FROM FINSAT633.FINSAT633.STK(NOLOCK) WHERE MalKodu=MalKod) as MalAdi,Kod4 as Kalite, Oran1, Oran2, Oran3, Oran4, Oran5 FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) 
    WHERE ListeNo=@ListeNo

END






GO
/****** Object:  StoredProcedure [wms].[BakiyeRaporu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[BakiyeRaporu]
@BasTarih INT,
@BitTarih INT,
@VadeBaslangic INT,
@VadeBitis INT, 
@BasHesapKodu VARCHAR(30),
@BitHesapKodu VARCHAR(30),
@Bakiye DECIMAL
AS
BEGIN
	
	SELECT * FROM (
select a.HesapKodu, isnull(max(b.Unvan1+' '+b.unvan2),'') as Unvan, 
cast(isnull(sum(case when a.BA = 0 then a.Tutar else 0 end),0) as numeric(36,2)) as Borc,
cast(isnull(sum(case when a.BA = 1 then a.Tutar else 0 end),0) as numeric(36,2)) as Alacak,
cast(isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0)as numeric(36,2)) as Bakiye
from (
select  KarsiHesapKodu as HesapKodu, 
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA
From FINSAT633.FINSAT633.CHI (nolock)  
where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42,60) 
and (Tarih Between @BasTarih and @BitTarih 
and VadeTarih Between @VadeBaslangic and @VadeBitis
)
Union All
select  HesapKodu, 
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA
From FINSAT633.FINSAT633.CHI (nolock) 
where IslemTip not in (16,21,27,32,36,37,41,42,60) 
and (Tarih Between @BasTarih and @BitTarih 
and VadeTarih Between @VadeBaslangic and @VadeBitis
)) a
inner join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu
where (b.hesapkodu Between @BasHesapKodu and @BitHesapKodu )

group by a.HesapKodu 
) B
WHERE B.Bakiye > (CASE @Bakiye WHEN 0 THEN -999999999 ELSE @Bakiye END)
AND (HesapKodu BETWEEN @BasHesapKodu AND @BitHesapKodu )
order by B.hesapkodu

END
















GO
/****** Object:  StoredProcedure [wms].[BekleyenSiparisMusteriKriterSelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[BekleyenSiparisMusteriKriterSelect]
AS
BEGIN

	SELECT        'TÜMÜ' AS Kriter, 'a' AS Flag
	UNION
	SELECT DISTINCT GrupKod AS Kriter, 'b' AS Flag
	FROM            FINSAT633.STK WITH (NOLOCK)
	WHERE        (GrupKod IS NOT NULL) AND (GrupKod <> '')
	ORDER BY Flag, Kriter

END

GO
/****** Object:  StoredProcedure [wms].[BekleyenSiparisRaporu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[BekleyenSiparisRaporu]
@BasTarih int,
@BitTarih int,
@BasTeslimTarih int,
@BitTeslimTarih int
AS
BEGIN

SELECT B.Chk, B.Unvan, B.Tarih, B.EvrakNo, B.BaglantiNo, B.GrupKod, B.MalKodu,
       B.MalAdi, B.BirimMiktar AS Miktar, B.Birim, B.Miktar AS AnaMiktar, B.AnaBirim, B.SevkedilenMiktar,
	   (CASE WHEN B.KalanBirim='ADET' THEN CAST(B.KalanMiktar AS INT) ELSE B.KalanMiktar END) AS KalanMiktar,
	   B.KalanBirim, (CASE WHEN B.KalanAnaBirim='ADET' THEN CAST(B.KalanAnaMiktar AS INT) ELSE B.KalanAnaMiktar END) 
	   AS KalanAnaMiktar, B.KalanAnaBirim, B.BaglantiTipi, B.FiyatListeNo, B.FYTFiyat,
	   B.FYTDovizCinsi, (CAST(B.IskOran1 as varchar)+' + '+CAST(B.IskOran2 as varchar)+' + '+  
	   CAST(B.IskOran3 as varchar)+' + '+CAST(B.IskOran4 as varchar)+' + '+
	   CAST(B.IskOran5 as varchar))  AS SozlesmeSartlari,
	   FINSAT633.FINSAT633.[Iskontolu_Fiyat_Hesapla](B.FYTFiyat,B.IskOran1,B.IskOran2,
	   B.IskOran3,B.IskOran4,B.IskOran5) AS NetFiyat, (B.Tutar-B.ToplamIskonto)/B.BirimMiktar AS NetFiyat2,
	    B.Tutar, B.ToplamIskonto, B.KDV, B.KalanBirimMiktar,
	   (B.Tutar-B.ToplamIskonto) AS KDVSizTutar,  (B.Tutar-B.ToplamIskonto+B.KDV) AS KDVliTutar,
	   (FINSAT633.FINSAT633.[Iskontolu_Fiyat_Hesapla](B.FYTFiyat,B.IskOran1,B.IskOran2,
	   B.IskOran3,B.IskOran4,B.IskOran5)*(B.KalanBirimMiktar)) AS KDVSizKalanTutar,
	   ((FINSAT633.FINSAT633.[Iskontolu_Fiyat_Hesapla](B.FYTFiyat,B.IskOran1,B.IskOran2,
	   B.IskOran3,B.IskOran4,B.IskOran5)+(B.KDV/B.BirimMiktar))*B.KalanBirimMiktar) AS KDVliKalanTutar,
	   (CASE WHEN B.StokBirim='ADET' THEN CAST(B.StokMiktar AS INT) ELSE B.StokMiktar END) AS StokMiktar, 
	   B.StokBirim, 
	   (CASE WHEN B.StokAnaBirim='ADET' THEN CAST(B.StokAnaMiktar AS INT) ELSE B.StokAnaMiktar END) 
	   AS StokAnaMiktar,  B.StokAnaBirim,
	   B.TeslimTarih, B.TeslimTarihDurum, B.SatisTemsilcisi
FROM
(  
   SELECT  SPI.Chk, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan,
	CONVERT(DATETIME,SPI.Tarih-2) AS Tarih, SPI.EvrakNo,
	SPI.Kod5 AS BaglantiNo, STK.GrupKod, SPI.Malkodu, STK.MalAdi, 
	SPI.BirimMiktar,  SPI.Birim,  SPI.Miktar ,  STK.Birim1 AS AnaBirim, 
	SPI.TeslimMiktar AS SevkedilenMiktar, 

	(SPI.BirimMiktar - SPI.TeslimMiktar - SPI.KapatilanMiktar) AS KalanMiktar,
	SPI.Birim AS KalanBirim,

	(CASE WHEN SPI.Birim=STK.Birim1 THEN (SPI.BirimMiktar - SPI.TeslimMiktar - SPI.KapatilanMiktar)
	 WHEN SPI.Birim=STK.Birim2 AND STK.Operator2 = 1 
	 THEN (SPI.BirimMiktar - SPI.TeslimMiktar - SPI.KapatilanMiktar)* STK.Katsayi2
	 WHEN SPI.Birim=STK.Birim2 AND STK.Operator2 = 0 
	 THEN (SPI.BirimMiktar - SPI.TeslimMiktar - SPI.KapatilanMiktar)/(CASE WHEN STK.Katsayi2>0 THEN STK.Katsayi2 ELSE 1 END)
	 END) AS KalanAnaMiktar,

	 STK.Birim1 AS KalanAnaBirim,

	(SELECT TOP 1 ISST.Kod1  
	  FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
	  WHERE ISST.ListeNo=SPI.Kod5 AND ISST.MusteriKod=SPI.Chk AND 
	  ISST.MalKod=(CASE ISST.MalKodGrup WHEN 0 THEN STK.MalKodu WHEN 1 THEN STK.GrupKod ELSE ISST.MalKod END)
	) AS BaglantiTipi,
     
    (SELECT TOP 1 ISST.Kod10  
	  FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
	  WHERE ISST.ListeNo=SPI.Kod5 AND ISST.MusteriKod=SPI.Chk AND 
	  ISST.MalKod=(CASE ISST.MalKodGrup WHEN 0 THEN STK.MalKodu WHEN 1 THEN STK.GrupKod ELSE ISST.MalKod END)
	) AS FiyatListeNo,
	 
	(SELECT TOP 1 CASE WHEN SatisFiyat1 <= 0 THEN A.DovizSatisFiyat1 ELSE A.SatisFiyat1 END FROM
		(
			SELECT SatisFiyat1, DvzSatisFiyat1 as DovizSatisFiyat1, SF1DovizCinsi
			FROM FINSAT633.FINSAT633.FYT (NOLOCK) FYT 
			INNER JOIN FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST ON ISST.Kod10=FYT.FiyatListNum
			WHERE FYT.MalKodu = SPI.MalKodu AND MusteriKodu = SPI.Chk AND SPI.Kod5 = ISST.ListeNo
			UNION ALL 
			SELECT SatisFiyat1, DvzSatisFiyat1, SF1DovizCinsi
			FROM FINSAT633.FINSAT633.FYT (NOLOCK) FYT 
			INNER JOIN FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST ON ISST.Kod10=FYT.FiyatListNum
			WHERE FYT.MalKodu = SPI.MalKodu AND MusteriKodu = '' AND SPI.Kod5 = ISST.ListeNo
		) A
	) AS FYTFiyat,
	 
	(SELECT TOP 1 CASE WHEN SatisFiyat1 <= 0 THEN A.SF1DovizCinsi ELSE '' END FROM
		(
			SELECT SatisFiyat1, DvzSatisFiyat1, SF1DovizCinsi
			FROM FINSAT633.FINSAT633.FYT (NOLOCK) FYT 
			INNER JOIN FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST ON ISST.Kod10=FYT.FiyatListNum
			WHERE FYT.MalKodu = SPI.MalKodu AND MusteriKodu = SPI.Chk 
			UNION ALL 
			SELECT SatisFiyat1, DvzSatisFiyat1, SF1DovizCinsi
			FROM FINSAT633.FINSAT633.FYT (NOLOCK) FYT 
			INNER JOIN FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST ON ISST.Kod10=FYT.FiyatListNum
			WHERE FYT.MalKodu = SPI.MalKodu AND MusteriKodu = ''
		) A
	) AS FYTDovizCinsi,
	
	(SELECT TOP 1 Oran1 
	  FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
	  WHERE ISST.ListeNo=SPI.Kod5 AND ISST.MusteriKod=SPI.Chk AND 
	  ISST.MalKod=(CASE ISST.MalKodGrup WHEN 0 THEN STK.MalKodu WHEN 1 
	               THEN STK.GrupKod when 7 then STK.Kod4 ELSE ISST.MalKod END)
	) AS IskOran1,
	
	(SELECT TOP 1 Oran2 
	  FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
	  WHERE ISST.ListeNo=SPI.Kod5 AND ISST.MusteriKod=SPI.Chk AND 
	  ISST.MalKod=(CASE ISST.MalKodGrup WHEN 0 THEN STK.MalKodu WHEN 1 
	               THEN STK.GrupKod when 7 then STK.Kod4 ELSE ISST.MalKod END)
	) AS IskOran2,

	(SELECT TOP 1 Oran3 
	  FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
	  WHERE ISST.ListeNo=SPI.Kod5 AND ISST.MusteriKod=SPI.Chk AND 
	  ISST.MalKod=(CASE ISST.MalKodGrup WHEN 0 THEN STK.MalKodu WHEN 1 
	               THEN STK.GrupKod when 7 then STK.Kod4 ELSE ISST.MalKod END)
	) AS IskOran3,

	(SELECT TOP 1 Oran4 
	  FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
	  WHERE ISST.ListeNo=SPI.Kod5 AND ISST.MusteriKod=SPI.Chk AND 
	  ISST.MalKod=(CASE ISST.MalKodGrup WHEN 0 THEN STK.MalKodu WHEN 1 
	               THEN STK.GrupKod when 7 then STK.Kod4 ELSE ISST.MalKod END)
	) AS IskOran4,

	(SELECT TOP 1 Oran5 
	  FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
	  WHERE ISST.ListeNo=SPI.Kod5 AND ISST.MusteriKod=SPI.Chk AND 
	  ISST.MalKod=(CASE ISST.MalKodGrup WHEN 0 THEN STK.MalKodu WHEN 1 
	               THEN STK.GrupKod  when 7 then STK.Kod4 ELSE ISST.MalKod END)
	) AS IskOran5,

    (SPI.BirimMiktar - SPI.TeslimMiktar - SPI.KapatilanMiktar) AS KalanBirimMiktar, 

    FINSAT633.FINSAT633.fncMiktarToBirimMiktar((STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar),
	SPI.Birim, STK.Birim1, STK.Birim2, STK.Birim3, STK.Operator2, STK.Operator3, STK.Katsayi2, 
	STK.Katsayi3) AS StokBirimMiktar, 
 
    SPI.Birim AS StokBirimMiktarBirim, SPI.Tutar, SPI.ToplamIskonto, SPI.KDV,

	((SPI.BirimMiktar - SPI.KapatilanMiktar - SPI.TeslimMiktar) * SPI.BirimFiyat)
	 AS KalanTutar, 

	(STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar) AS StokAnaMiktar, STK.Birim1 AS StokAnaBirim,

	(CASE WHEN STK.Birim2='' THEN (STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar) 
	WHEN STK.Birim1=STK.Birim2 THEN (STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar) 
	WHEN STK.Operator2 = 1 THEN 
	(STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar)/
	(CASE WHEN STK.KatSayi2>0 THEN STK.KatSayi2 ELSE 1 END) 
	WHEN STK.Operator2 = 0 THEN (STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar)*STK.KatSayi2 END) 
	AS StokMiktar,
   
   (CASE WHEN STK.Birim2='' THEN STK.Birim1 ELSE STK.Birim2 END) AS StokBirim, SPI.BirimFiyat, 

   (CASE SPI.CheckSum WHEN 12 THEN CONVERT(DATETIME,SPI.TahTeslimTarih-2) ELSE null END) 
    AS TeslimTarih,

	(CASE WHEN
	   (CASE SPI.CheckSum WHEN 12 THEN CONVERT(DATETIME,SPI.TahTeslimTarih-2) ELSE null END) = null
	THEN '' WHEN GETDATE() > (CASE SPI.CheckSum WHEN 12 THEN CONVERT(DATETIME,SPI.TahTeslimTarih-2) 
	ELSE null END) THEN 'Teslim Tarihi Geçmiş'  ELSE  ''
	END) AS TeslimTarihDurum,

	(SELECT Aciklama
		FROM SOLAR6.DBO.KTK (NOLOCK)
		WHERE Tip = 8 AND Kod = CHK.OzelKod
	) AS SatisTemsilcisi


FROM FINSAT633.FINSAT633.SPI (NOLOCK) SPI 
INNER JOIN FINSAT633.FINSAT633.CHK (NOLOCK) CHK ON CHK.HesapKodu=SPI.Chk 
INNER JOIN FINSAT633.FINSAT633.STK (NOLOCK) STK ON STK.Malkodu=SPI.Malkodu
WHERE SPI.Kynkevraktip=62 AND SPI.SiparisDurumu=0
AND SPI.BirimMiktar-SPI.TeslimMiktar > 0
AND SPI.Tarih BETWEEN @BasTarih AND @BitTarih
AND SPI.TahTeslimTarih BETWEEN @BasTeslimTarih AND @BitTeslimTarih
AND SPI.Kod10 IN('Onaylandı','Sevk Edilebilir')
) B

ORDER BY B.Tarih DESC


END










GO
/****** Object:  StoredProcedure [wms].[BolgeBazliSatisAnaliziKriterSelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[BolgeBazliSatisAnaliziKriterSelect]
AS
BEGIN
	
	SELECT 'TÜMÜ' as Kriter,'a' as Flag
	UNION 
	SELECT DISTINCT GrupKod as Kriter,'b' as Flag FROM [FINSAT633].[FINSAT633].CHK(NOLOCK)
	WHERE GrupKod IS NOT NULL AND GrupKod<>''
	Order BY Flag,Kriter

END


GO
/****** Object:  StoredProcedure [wms].[BTB_RP_CariEkstreDetay_Cek]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure  [wms].[BTB_RP_CariEkstreDetay_Cek]
@Tip VARCHAR(20),
@HesapKodu NVARCHAR(30),
@EvrakNo VARCHAR(16)
as
begin
	SELECT BorcluUnvan1 AS [BorcluUnvan],
	replace(replace(replace(convert(varchar,CAST(Tutar as money),1),'.','x'),',','.'),'x',',') as Tutar, 
	CONVERT(varchar,CAST(VadeTarih - 2 AS datetime),104) AS VadeTarihi, 
	Sehir+' - '+UIK.Adi AS [Sehir], 
	Banka+' - '+UBK.Adi as Banka, 
	Sube+' - '+USK.Adi AS Sube, 
	CekNo AS CekNo 
	FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
	LEFT JOIN SOLAR6.dbo.UIK (NOLOCK) UIK ON UIK.UlkeNumKodu=ACK.Ulke AND UIK.Kodu=ACK.Sehir
	LEFT JOIN SOLAR6.dbo.UBK(NOLOCK) UBK ON UBK.UlkeNumKodu=ACK.Ulke AND UBK.Kodu= ACK.Banka
	LEFT JOIN SOLAR6.dbo.USK(NOLOCK) USK ON USK.UlkeNumKodu=ACK.Ulke AND USK.BankaKodu=ACK.Banka AND USK.IlKodu=ACK.Sehir AND USK.Kodu=ACK.Sube
	where (EvrakNo=@EvrakNo) and (Veren like @HesapKodu+'%')
	end












GO
/****** Object:  StoredProcedure [wms].[BTB_RP_CariEkstreDetay_Diger]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE procedure  [wms].[BTB_RP_CariEkstreDetay_Diger]
@HesapKodu NVARCHAR(30),
@EvrakNo VARCHAR(16)
as
begin

SELECT 
CHI.HesapKodu, 
CHK.Unvan1+' '+ CHK.Unvan2 as Unvan,
EvrakNo,
KarsiHesapKodu,
KarsiEvrakNo,
Aciklama,
replace(replace(replace(convert(varchar,CAST(Tutar as money),1),'.','x'),',','.'),'x',',') as Tutar, 
CONVERT(varchar,CAST(VadeTarih - 2 AS datetime),104) AS VadeTarihi
FROM FINSAT633.FINSAT633.CHI
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK 
ON CHK.HesapKodu=CHI.HesapKodu  
WHERE (CHI.EvrakNo = @EvrakNo) AND ((CHI.HesapKodu like @HesapKodu+'%') OR (CHI.KarsiHesapKodu like @HesapKodu+'%')  )

end 









GO
/****** Object:  StoredProcedure [wms].[BTB_RP_CariEkstreDetay_Fatura]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE procedure  [wms].[BTB_RP_CariEkstreDetay_Fatura]
@Tip VARCHAR(20),
@HesapKodu NVARCHAR(30),
@EvrakNo VARCHAR(16)
as
begin
	--SELECT STI.MalKodu AS MalKodu,
	--STK.MalAdi AS MalAdi, 
	--CAST(STI.Miktar AS decimal(8,2)) AS Miktar,
	--STI.Birim as Birim 
	--FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	--INNER JOIN 
	--FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu = STK.MalKodu 
	SELECT EvrakNo,
	STI.MalKodu AS MalKodu,
	STK.MalAdi AS MalAdi, 
	replace(replace(replace(convert(varchar,CAST(STI.Miktar as money),1),'.','x'),',','.'),'x',',') AS Miktar,
	STI.Birim as Birim,
	replace(replace(replace(convert(varchar,CAST(STI.BirimFiyat as money),1),'.','x'),',','.'),'x',',') AS BirimFiyat,
	replace(replace(replace(convert(varchar,CAST(STI.Tutar as money),1),'.','x'),',','.'),'x',',') AS Tutar,
	'% '+CAST(STI.IskontoOran1 AS NVARCHAR(5)) AS IskontoOran1,
	'% '+CAST(STI.IskontoOran2 AS NVARCHAR(5)) AS IskontoOran2,
	'% '+CAST(STI.IskontoOran3 AS NVARCHAR(5)) AS IskontoOran3,
	'% '+CAST(STI.IskontoOran4 AS NVARCHAR(5)) AS IskontoOran4,
	'% '+CAST(STI.IskontoOran5 AS NVARCHAR(5)) AS IskontoOran5,
	replace(replace(replace(convert(varchar,CAST(STI.ToplamIskonto as money),1),'.','x'),',','.'),'x',',') AS ToplamIskonto,
	replace(replace(replace(convert(varchar,CAST(STI.Tutar-STI.ToplamIskonto as money),1),'.','x'),',','.'),'x',',') AS NetTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN 
	FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu = STK.MalKodu 
	WHERE (STI.EvrakNo = @EvrakNo) AND (STI.Chk like @HesapKodu+'%')

end 












GO
/****** Object:  StoredProcedure [wms].[CekListesiRaporu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE[wms].[CekListesiRaporu]
@Ay INT, 
@IslemTip INT,
@Yil INT
AS
BEGIN
	
SELECT SCI.EvrakNo, CONVERT(DATETIME, SCI.Tarih-2) AS  Tarih, SCI.Chk,CHK.Unvan1, SCI.Tutar,  CONVERT(DATETIME, SCI.VadeTarih-2) AS VadeTarih, CONVERT(DATETIME, SCI.KayitTarih-2) AS KayitTarih,
(select ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME (NOLOCK)COMBOITEM_NAME WHERE COMBOITEM_NAME.ITEMCBOID=13 AND COMBOITEM_NAME.ID=SCI.IslemTip) AS Pozisyon, ACK.BorcluUnvan1, (select ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME (NOLOCK)COMBOITEM_NAME WHERE COMBOITEM_NAME.ITEMCBOID=13 AND COMBOITEM_NAME.ID=ACK.SonIslemTip) AS SonPozisyon,
ISNULL((select top 1 S.CHK FROM FINSAT633.FINSAT633.SCI (NOLOCK) S WHERE S.Sirano=(SELECT max(Sirano) FROM FINSAT633.FINSAT633.SCI (NOLOCK) SC WHERE SC.Islemtip=2 and SC.Evrakno=SCI.Evrakno ) and s.evrakno=SCI.Evrakno),'') as VerildigiYer,
ISNULL((select top 1 CHK.Unvan1+' '+chk.Unvan2   FROM FINSAT633.FINSAT633.SCI (NOLOCK) S INNER JOIN FINSAT633.FINSAT633.CHK (NOLOCK) CHK ON CHK.HesapKodu=S.CHK WHERE S.Sirano=(SELECT max(Sirano) FROM FINSAT633.FINSAT633.SCI (NOLOCK) SC WHERE SC.Islemtip=2 and SC.Evrakno=SCI.Evrakno ) and s.evrakno=SCI.Evrakno),'') as VerildigiYerUnvan


FROM FINSAT633.FINSAT633.SCI (nolock) SCI 
INNER JOIN FINSAT633.FINSAT633.CHK (NOLOCK) CHK ON Chk.HesapKodu=SCI.Chk
INNER JOIN FINSAT633.FINSAT633.ACK(NOLOCK) ACK ON SCI.EvrakNo=ACK.EvrakNo
WHERE (SCI.ScTip=0) AND Tarih>=41275 AND (MONTH(Tarih-2)=@Ay)  AND (SCI.IslemTip=@IslemTip) AND (YEAR(Tarih-2)=@Yil)

END










GO
/****** Object:  StoredProcedure [wms].[CekOnayDetay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[CekOnayDetay]
@Unvan VARCHAR(MAX)
AS
BEGIN
	
	SELECT EvrakNo
, CONVERT(DATETIME, AlimTarih-2) AS Tarih
, Veren
, ISNULL((CHK.Unvan1 +' '+ CHK.Unvan2),'') AS VerenUnvan 
, Borclu
, BorcluUnvan1 +' '+ BorcluUnvan2 As BorcluUnvan
, Tutar
, CONVERT(DATETIME, VadeTarih) AS VadeTarih
, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME (NOLOCK)COMBOITEM_NAME WHERE COMBOITEM_NAME.ITEMCBOID=13 AND COMBOITEM_NAME.ID=ACK.SonIslemTip) AS SonPozisyon
FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK 
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON ACK.Veren=CHK.HesapKodu
WHERE BorcluUnvan1 LIKE '%'+@Unvan+'%'

END








GO
/****** Object:  StoredProcedure [wms].[CekOnayGM]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[CekOnayGM]

AS
BEGIN

	--SELECT * FROM FINSAT633.FINSAT633.CEK 
	--WHERE 
	--SPGMYOnay =1 AND MIGMYOnay = 1 AND GMOnay IS NULL  
 --   AND Durum = 0

declare @ortvade datetime
select @ortvade=(select convert(datetime,avg(VadeTarih) - 2)  as BaglantininOrtalamaVadesi from	(SELECT x.VadeTarih from FINSAT633.FINSAT633.ACK x
    join FINSAT633.FINSAT633.SCI y on x.EvrakNo= y.EvrakNo
 where y.Kod2 = (SELECT Kod2 FROM FINSAT633.FINSAT633.SCI WHERE ScTip = 0 AND EvrakNo = 
(SELECT  TOP 1  EvrakNo FROM FINSAT633.FINSAT633.CEK WHERE	SPGMYOnay =1 AND MIGMYOnay = 1 AND GMOnay IS NULL AND Durum = 0))) t)

SELECT C.EvrakNo,C.Veren,C.Unvan,C.Borclu,C.BorcluUnvan,C.VadeTarihi,C.Tutar,x.VadeTarih,
	ISNULL((SELECT Kod11 from FINSAT633.FINSAT633.ISS_Temp xx where xx.ListeNo = y.Kod2),0)as BaglantiTutar, @ortvade AS BaglantininOrtalamaVadesi
FROM FINSAT633.FINSAT633.ACK x
join  FINSAT633.FINSAT633.SCI y on x.EvrakNo = y.EvrakNo 
LEFT JOIN FINSAT633.FINSAT633.CEK C ON y.EvrakNo = C.EvrakNo 
WHERE y.Kod2 = (SELECT Kod2 FROM FINSAT633.FINSAT633.SCI WHERE ScTip = 0 AND EvrakNo = 
(SELECT  TOP 1  EvrakNo FROM FINSAT633.FINSAT633.CEK WHERE	SPGMYOnay =1 AND MIGMYOnay = 1 AND GMOnay IS NULL AND Durum = 0)) 
	AND y.IslemTip = 0  
	AND C.SPGMYOnay =1 AND C.MIGMYOnay = 1 AND C.GMOnay IS NULL  
    AND C.Durum = 0
END





GO
/****** Object:  StoredProcedure [wms].[CekOnayMIGMY]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[CekOnayMIGMY]

AS
BEGIN

	--SELECT * FROM FINSAT633.FINSAT633.CEK 
	--WHERE 
	--SPGMYOnay =1 AND MIGMYOnay IS NULL AND GMOnay IS NULL  
 --   AND Durum = 0
 declare @ortvade datetime
select @ortvade=(select convert(datetime,avg(VadeTarih) - 2)  as BaglantininOrtalamaVadesi from	(SELECT x.VadeTarih from FINSAT633.FINSAT633.ACK x
    join FINSAT633.FINSAT633.SCI y on x.EvrakNo= y.EvrakNo
 where y.Kod2 = (SELECT Kod2 FROM FINSAT633.FINSAT633.SCI WHERE ScTip = 0 AND EvrakNo = 
(SELECT  TOP 1  EvrakNo FROM FINSAT633.FINSAT633.CEK WHERE	SPGMYOnay =1 AND MIGMYOnay = 1 AND GMOnay IS NULL AND Durum = 0))) t)

SELECT C.EvrakNo,C.Veren,C.Unvan,C.Borclu,C.BorcluUnvan,C.VadeTarihi,C.Tutar,x.VadeTarih,
	ISNULL((SELECT Kod11 from FINSAT633.FINSAT633.ISS_Temp xx where xx.ListeNo = y.Kod2),0)as BaglantiTutar, @ortvade AS BaglantininOrtalamaVadesi
FROM FINSAT633.FINSAT633.ACK x
join  FINSAT633.FINSAT633.SCI y on x.EvrakNo = y.EvrakNo 
LEFT JOIN FINSAT633.FINSAT633.CEK C ON y.EvrakNo = C.EvrakNo 
WHERE y.Kod2 = (SELECT Kod2 FROM FINSAT633.FINSAT633.SCI WHERE ScTip = 0 AND EvrakNo = 
(SELECT  TOP 1  EvrakNo FROM FINSAT633.FINSAT633.CEK WHERE	SPGMYOnay =1 AND MIGMYOnay = 1 AND GMOnay IS NULL AND Durum = 0)) 
	AND y.IslemTip = 0  
	AND C.SPGMYOnay =1 AND C.MIGMYOnay IS NULL AND C.GMOnay IS NULL  
    AND C.Durum = 0

END





GO
/****** Object:  StoredProcedure [wms].[CekOnaySPGMY]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[CekOnaySPGMY]

AS
BEGIN

	--SELECT * FROM FINSAT633.FINSAT633.CEK 
	--WHERE 
	--SPGMYOnay IS NULL AND MIGMYOnay IS NULL AND GMOnay IS NULL AND 
	--Durum = 0


	 declare @ortvade datetime
select @ortvade=(select convert(datetime,avg(VadeTarih) - 2)  as BaglantininOrtalamaVadesi from	(SELECT x.VadeTarih from FINSAT633.FINSAT633.ACK x
    join FINSAT633.FINSAT633.SCI y on x.EvrakNo= y.EvrakNo
 where y.Kod2 = (SELECT Kod2 FROM FINSAT633.FINSAT633.SCI WHERE ScTip = 0 AND EvrakNo = 
(SELECT  TOP 1  EvrakNo FROM FINSAT633.FINSAT633.CEK WHERE	SPGMYOnay =1 AND MIGMYOnay = 1 AND GMOnay IS NULL AND Durum = 0))) t)

SELECT C.EvrakNo,C.Veren,C.Unvan,C.Borclu,C.BorcluUnvan,C.VadeTarihi,C.Tutar,x.VadeTarih,
	ISNULL((SELECT Kod11 from FINSAT633.FINSAT633.ISS_Temp xx where xx.ListeNo = y.Kod2),0)as BaglantiTutar, @ortvade AS BaglantininOrtalamaVadesi
FROM FINSAT633.FINSAT633.ACK x
join  FINSAT633.FINSAT633.SCI y on x.EvrakNo = y.EvrakNo 
LEFT JOIN FINSAT633.FINSAT633.CEK C ON y.EvrakNo = C.EvrakNo 
WHERE y.Kod2 = (SELECT Kod2 FROM FINSAT633.FINSAT633.SCI WHERE ScTip = 0 AND EvrakNo = 
(SELECT  TOP 1  EvrakNo FROM FINSAT633.FINSAT633.CEK WHERE	SPGMYOnay =1 AND MIGMYOnay = 1 AND GMOnay IS NULL AND Durum = 0)) 
	AND y.IslemTip = 0  
	AND C.SPGMYOnay IS NULL AND C.MIGMYOnay IS NULL AND C.GMOnay IS NULL  
    AND C.Durum = 0
END





GO
/****** Object:  StoredProcedure [wms].[CekPortfoyListesi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[CekPortfoyListesi]
@BasTarih INT,
@BitTarih INT,
@BasVadeTarih INT,
@BitVadeTarih INT
AS
BEGIN
	
SELECT 
CEK.Evrakno,CEK.Tarih,CEK.CHK,CEK.Unvan,CEK.CekiDuzenleyen,
cast(CEK.Tutar as numeric(36,2))as Tutar, --replace(replace(replace(convert(varchar,CAST(CEK.Tutar as money),1),'.','x'),',','.'),'x',',') as Tutar,
CEK.VadeTarih,CEK.SonPozisyon,CEK.VerildigiYer,CEK.VerildigiYerUnvan 
FROM
(
SELECT 
SCI.IslemTip,SCI.EvrakNo,
CONVERT(VARCHAR(15),(CONVERT(DATETIME, SCI.Tarih-2)),104) AS Tarih, 
ACK.Veren as CHK, (CHK.Unvan1+' '+CHK.Unvan2 )AS Unvan, 
(ACK.BorcluUnvan1 + ' '+ ACK.BorcluUnvan2) AS CekiDuzenleyen, SCI.Tutar as Tutar,
   CONVERT(VARCHAR(15),CONVERT(DATETIME, ACK.VadeTarih-2),104) AS VadeTarih,
(select ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME (NOLOCK)COMBOITEM_NAME WHERE COMBOITEM_NAME.ITEMCBOID=13 AND COMBOITEM_NAME.ID=ACK.SonIslemTip)
 AS SonPozisyon,

ISNULL((select top 1 S.CHK FROM FINSAT633.FINSAT633.SCI (NOLOCK) S WHERE S.Sirano=(SELECT max(Sirano) FROM FINSAT633.FINSAT633.SCI
 (NOLOCK) SC WHERE SC.Islemtip=2 and SC.Evrakno=SCI.Evrakno
and SC.Tarih between @BasTarih AND @BitTarih
 ) and s.evrakno=SCI.Evrakno),'') as VerildigiYer,
 ISNULL((select top 1(CHK.Unvan1+' '+chk.Unvan2)   FROM FINSAT633.FINSAT633.SCI (NOLOCK) S INNER JOIN FINSAT633.FINSAT633.CHK (NOLOCK) CHK 
 ON CHK.HesapKodu=S.CHK WHERE S.Sirano=(SELECT max(Sirano) FROM FINSAT633.FINSAT633.SCI (NOLOCK) SC WHERE SC.Islemtip=2 and SC.Evrakno=SCI.Evrakno
  and SC.Tarih between @BasTarih AND @BitTarih ) and s.evrakno=SCI.Evrakno),'') as VerildigiYerUnvan



 FROM FINSAT633.FINSAT633.SCI (NOLOCK) SCI

RIGHT JOIN

(SELECT MAX (SCI.EvrakNo)as EvrakNo,MAX (SCI.Sirano) as Sirano 
FROM FINSAT633.FINSAT633.SCI (NOLOCK) SCI

WHERE (SCI.Tarih BETWEEN @BasTarih AND @BitTarih)
--and SCI.Evrakno='VP001655' 
GROUP BY SCI.EvrakNo
) SIRA ON SIRA.Evrakno=SCI.EvrakNo and SIRA.Sirano=SCI.SiraNo
RIGHT JOIN FINSAT633.FINSAT633.ACK (NOLOCK) ACK ON SCI.EvrakNo=ACK.EvrakNo
RIGHT JOIN FINSAT633.FINSAT633.CHK (NOLOCK) CHK ON ACK.Veren=CHK.HesapKodu
WHERE  ACK.VadeTarih BETWEEN @BasVadeTarih and @BitVadeTarih 
--55153
) CEK
WHERE 
--(CEK.VerildigiYer in('0002TMP','0002GOP','0002KD','0002ADN','0002ANK','0002BUR','0002GBZ','0002PANO','0002IZM')
--and CEK.IslemTip=2
--) 
--or
CEK.IslemTip NOT in (6,10)
ORDER BY CEK.VadeTarih,CEK.Evrakno

END




















GO
/****** Object:  StoredProcedure [wms].[ChkKampanyaDetay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[ChkKampanyaDetay]
@CHK NVARCHAR(30) ,
@BasTarih INT,
@BitTarih INT
AS
BEGIN
SELECT 
* ,
'PALET' as ComfortBirim,
'PALET' as ExculusiveBirim,
'PALET' as PeliNeroFloorBirim,
'PALET' as GoldenBirim,
'PALET' as LoftBirim,
'PALET' as VintageBirim,
'PALET' as WoodBirim,
'PAKET' as SupurgelikBirim,
(C.ComfortMiktar+ C.ExculusiveMiktar) as HakKazanilan6cmSup,
(C.PeliNeroFloorMiktar+ C.GoldenMiktar+C.LoftMiktar+ C.VintageMiktar+ C.WoodMiktar) as HakKazanilan8cmSup

FROM (
SELECT 
B.Chk,
B.EvrakNo,
SUM(B.PeliNeroFloorMiktar) as PeliNeroFloorMiktar,
SUM(B.PeliNeroFloorTeslimMiktar) as PeliNeroFloorTeslimMiktar,
SUM(B.PeliNeroFloorTutar) as PeliNeroFloorTutar,
SUM(B.ComfortMiktar) as ComfortMiktar,
SUM(B.ComfortTeslimMiktar) as ComfortTeslimMiktar,
SUM(B.ComfortTutar) as ComfortTutar,
SUM(B.ExculusiveMiktar) as ExculusiveMiktar,
SUM(B.ExculusiveTeslimMiktar) as ExculusiveTeslimMiktar,
SUM(B.ExculusiveTutar) as ExculusiveTutar,
SUM(B.GoldenMiktar) as GoldenMiktar,
SUM(B.GoldenTeslimMiktar) as GoldenTeslimMiktar,
SUM(B.GoldenTutar) as GoldenTutar,
SUM(B.LoftMiktar) as LoftMiktar,
SUM(B.LoftTeslimMiktar) as LoftTeslimMiktar,
SUM(B.LoftTutar) as LoftTutar,
SUM(B.VintageMiktar) as VintageMiktar,
SUM(B.VintageTeslimMiktar) as VintageTeslimMiktar,
SUM(B.VintageTutar) as VintageTutar,
SUM(B.WoodMiktar) as WoodMiktar,
SUM(B.WoodTeslimMiktar) as WoodTeslimMiktar,
SUM(B.WoodTutar) as WoodTutar,
SUM(B.Bedelsiz6CmSupMiktar) as Bedelsiz6CmSupMiktar,
SUM(B.Bedelsiz6CmSupTeslimMiktar) as Bedelsiz6CmSupTeslimMiktar,
SUM(B.Bedelsiz6CmSupTutar) as Bedelsiz6CmSupTutar,
SUM(B.Bedelsiz8CmSupMiktar) as Bedelsiz8CmSupMiktar,
SUM(B.Bedelsiz8CmSupTeslimMiktar) as Bedelsiz8CmSupTeslimMiktar,
SUM(B.Bedelsiz8CmSupTutar) as Bedelsiz8CmSupTutar,
SUM(B.Bedelli6CmSupMiktar) as Bedelli6CmSupMiktar,
SUM(B.Bedelli6CmSupTeslimMiktar) as Bedelli6CmSupTeslimMiktar,
SUM(B.Bedelli6CmSupTutar) as Bedelli6CmSupTutar,
SUM(B.Bedelli8CmSupMiktar) as Bedelli8CmSupMiktar,
SUM(B.Bedelli8CmSupTeslimMiktar) as Bedelli8CmSupTeslimMiktar,
SUM(B.Bedelli8CmSupTutar) as Bedelli8CmSupTutar
FROM(
	SELECT 
	A.CHK,
	A.EvrakNo,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='PELİ NEROFLOOR' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS PeliNeroFloorMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='PELİ NEROFLOOR' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS PeliNeroFloorTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='PELİ NEROFLOOR' THEN SUM(A.Tutar) END),0)  AS PeliNeroFloorTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='COMFORT' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS ComfortMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='COMFORT' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS ComfortTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='COMFORT' THEN SUM(A.Tutar) END),0)  AS ComfortTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='EXCLUSIVE' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS ExculusiveMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='EXCLUSIVE' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS ExculusiveTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='EXCLUSIVE' THEN SUM(A.Tutar) END),0)  AS ExculusiveTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='GOLDEN' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS GoldenMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='GOLDEN' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS GoldenTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='GOLDEN' THEN SUM(A.Tutar) END),0)  AS GoldenTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='LOFT' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS LoftMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='LOFT' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS LoftTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='LOFT' THEN SUM(A.Tutar)END),0)  AS LoftTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='VİNTAGE' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS VintageMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='VİNTAGE' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS VintageTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='VİNTAGE' THEN SUM(A.Tutar) END),0)  AS VintageTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='WOOD' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS WoodMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='WOOD' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS WoodTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='WOOD' THEN SUM(A.Tutar) END),0)  AS WoodTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelsiz6CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelsiz6CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN SUM(A.Tutar) END),0)  AS Bedelsiz6CmSupTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelsiz8CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelsiz8CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN SUM(A.Tutar) END),0)  AS Bedelsiz8CmSupTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelli6CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelli6CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.Tutar)/(78.4 ),0) END),0)  AS Bedelli6CmSupTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelli8CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelli8CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN SUM(A.Tutar) END),0)  AS Bedelli8CmSupTutar
	FROM(
		SELECT 
			SPI.Chk,
			SPI.EvrakNo,
			ROUND(SUM(SPI.Tutar),0) as Tutar,
			ROUND(SUM(SPI.Miktar),0) as Miktar,
			ROUND(SUM(SPI.TeslimMiktar),0) as TeslimMiktar,
			SPI.Nesne1 as Durum,
			STK.GrupKod ,
			STK.Kod4 AS Koleksiyon,
			(CASE WHEN SPI.MalKodu LIKE '89850978%' THEN '6CM' WHEN SPI.MalKodu LIKE '8985095%' THEN '8CM' ELSE '' END) AS SUPTIP
		FROM FINSAT633.FINSAT633.SPI (NOLOCK) 
		LEFT JOIN FINSAT633.FINSAT633.STK(NOLOCK) ON SPI.MalKodu= STK.MalKodu
		WHERE SPI.CHK = @CHK AND KynkEvrakTip = 62 AND SPI.Tarih between @BasTarih AND @BitTarih
		GROUP BY SPI.CHK,SPI.EvrakNo,SPI.Nesne1,STK.GrupKod,STK.Kod4,(CASE WHEN SPI.MalKodu LIKE '89850978%' THEN '6CM' WHEN SPI.MalKodu LIKE '8985095%'			THEN '8CM' ELSE '' END)
	) A
GROUP BY A.CHK,A.GrupKod,A.Koleksiyon,A.Durum,A.SUPTIP,A.EvrakNo
) B
GROUP BY B.CHK,B.EvrakNo
) C
END












GO
/****** Object:  StoredProcedure [wms].[CHKSearch]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 14.07.2017
-- Modify date: 14.07.2017
-- Description:	istenenen cari hesap kartlarını getirir
-- =============================================
CREATE PROCEDURE [wms].[CHKSearch]
	@HesapKodu varchar(20),
	@Unvan varchar(80),
	@top int

AS
BEGIN
	
	SELECT      top (@top)  HesapKodu as id, Unvan1 + ' ' + Unvan2 AS [value], Unvan1 + ' ' + Unvan2 AS label
	FROM            FINSAT633.CHK WITH (NOLOCK)
	WHERE        (KartTip IN (0, 1, 4)) 
					AND case when @HesapKodu<>'' then case when HesapKodu like @HesapKodu + '%' then 1 else 0 end else 1 end = 1
					AND case when @Unvan<>'' then case when Unvan1 like '%' + @Unvan + '%' or Unvan2 like '%' + @Unvan + '%' then 1 else 0 end else 1 end = 1
	ORDER BY HesapKodu

END




GO
/****** Object:  StoredProcedure [wms].[CHKSearch4]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 03.10.2017
-- Modify date: 03.10.2017
-- Description:	istenenen cari hesap kartlarını getirir
-- =============================================
CREATE PROCEDURE [wms].[CHKSearch4]
	@HesapKodu varchar(20),
	@Unvan varchar(80),
	@top int

AS
BEGIN
	
		SELECT      top (@top)  HesapKodu as id, HesapKodu + ' ' + Unvan1 + ' ' + Unvan2 AS [value], HesapKodu + ' ' + Unvan1 + ' ' + Unvan2 AS label
	FROM            FINSAT633.CHK WITH (NOLOCK)
	WHERE        (KartTip IN (0, 1, 4)) 
					AND case when @HesapKodu<>'' then case when HesapKodu like @HesapKodu + '%' then 1 else 0 end else 1 end = 1
					AND case when @Unvan<>'' then case when HesapKodu like '%' + @Unvan + '%' or Unvan1 like '%' + @Unvan + '%' or Unvan2 like '%' + @Unvan + '%' then 1 else 0 end else 1 end = 1
	ORDER BY HesapKodu

END






GO
/****** Object:  StoredProcedure [wms].[CHKSelect1]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[CHKSelect1]
AS
BEGIN
	
	SELECT HesapKodu, (Unvan1+' '+Unvan2) AS Unvan 
	FROM FINSAT633.FINSAT633.CHK(NOLOCK) 
	WHERE HesapKodu Not Like '%-T%'	
	      --AND HesapKodu Like '120%' AND KartTip in (0,1,4) 
	ORDER BY HesapKodu

END


GO
/****** Object:  StoredProcedure [wms].[CHKSelect2]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[CHKSelect2]
AS
BEGIN
	
	SELECT CAST(0 AS BIT) AS Onay, HesapKodu, (Unvan1+' '+Unvan2) AS Unvan, Kod12 AS SahsiCekLimiti, Kod13 AS MusteriCekLimiti, CAST(0 AS DECIMAL) AS YeniSahsiCekLimiti, CAST(0 AS DECIMAL) AS YeniMusteriCekLimiti FROM FINSAT633.FINSAT633.CHK(NOLOCK) WHERE KartTip in (0)

END




GO
/****** Object:  StoredProcedure [wms].[CHKSelect3]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[CHKSelect3]
AS
BEGIN	
	SELECT HesapKodu, (Unvan1+' '+Unvan2) AS Unvan FROM FINSAT633.CHK(NOLOCK) 
	WHERE HesapKodu Like '120%' AND KartTip in (0,1,4) 
	      AND HesapKodu Not Like '%-T%'		
	ORDER BY Unvan
END





GO
/****** Object:  StoredProcedure [wms].[CHKSelectKartTip]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[CHKSelectKartTip]
AS
BEGIN
	
	SELECT HesapKodu, (Unvan1+' '+Unvan2) AS Unvan 
	FROM FINSAT633.FINSAT633.CHK(NOLOCK) 
	WHERE KartTip in(0, 4) 
	ORDER BY HesapKodu

END









GO
/****** Object:  StoredProcedure [wms].[DB_Aylik_SatisAnalizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[DB_Aylik_SatisAnalizi]
AS
BEGIN

    DECLARE @BasOcak2015 INT, @BitOcak2015 INT,
	        @BasSubat2015 INT, @BitSubat2015 INT,
			@BasMart2015 INT, @BitMart2015 INT,
			@BasNisan2015 INT, @BitNisan2015 INT,
			@BasMayis2015 INT, @BitMayis2015 INT,
			@BasHaziran2015 INT, @BitHaziran2015 INT,
			@BasTemmuz2015 INT, @BitTemmuz2015 INT,
			@BasAgustos2015 INT, @BitAgustos2015 INT,
			@BasEylul2015 INT, @BitEylul2015 INT,
			@BasEkim2015 INT, @BitEkim2015 INT,
			@BasKasim2015 INT, @BitKasim2015 INT,
			@BasAralik2015 INT, @BitAralik2015 INT,
	        @BasOcak2016 INT, @BitOcak2016 INT,
	        @BasSubat2016 INT, @BitSubat2016 INT,
			@BasMart2016 INT, @BitMart2016 INT,
			@BasNisan2016 INT, @BitNisan2016 INT,
			@BasMayis2016 INT, @BitMayis2016 INT,
			@BasHaziran2016 INT, @BitHaziran2016 INT,
			@BasTemmuz2016 INT, @BitTemmuz2016 INT,
			@BasAgustos2016 INT, @BitAgustos2016 INT,
			@BasEylul2016 INT, @BitEylul2016 INT,
			@BasEkim2016 INT, @BitEkim2016 INT,
			@BasKasim2016 INT, @BitKasim2016 INT,
			@BasAralik2016 INT, @BitAralik2016 INT,
			@BasOcak2017 INT, @BitOcak2017 INT,
	        @BasSubat2017 INT, @BitSubat2017 INT,
			@BasMart2017 INT, @BitMart2017 INT,
			@BasNisan2017 INT, @BitNisan2017 INT,
			@BasMayis2017 INT, @BitMayis2017 INT,
			@BasHaziran2017 INT, @BitHaziran2017 INT,
			@BasTemmuz2017 INT, @BitTemmuz2017 INT,
			@BasAgustos2017 INT, @BitAgustos2017 INT,
			@BasEylul2017 INT, @BitEylul2017 INT,
			@BasEkim2017 INT, @BitEkim2017 INT,
			@BasKasim2017 INT, @BitKasim2017 INT,
			@BasAralik2017 INT, @BitAralik2017 INT
    
	Set @BasOcak2015=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)	    
	Set @BitOcak2015=CAST(CONVERT(SMALLDATETIME, '2016-01-31')+2 AS INT)
	Set @BasSubat2015=CAST(CONVERT(SMALLDATETIME, '2016-02-01')+2 AS INT) 	
	Set @BitSubat2015=CAST(CONVERT(SMALLDATETIME, '2016-02-29')+2 AS INT)
	Set @BasMart2015=CAST(CONVERT(SMALLDATETIME, '2016-03-01')+2 AS INT)
    Set @BitMart2015=CAST(CONVERT(SMALLDATETIME, '2016-03-31')+2 AS INT)
	Set @BasNisan2015=CAST(CONVERT(SMALLDATETIME, '2016-04-01')+2 AS INT)	    
	Set @BitNisan2015=CAST(CONVERT(SMALLDATETIME, '2016-04-30')+2 AS INT)
	Set @BasMayis2015=CAST(CONVERT(SMALLDATETIME, '2016-05-01')+2 AS INT)	    
	Set @BitMayis2015=CAST(CONVERT(SMALLDATETIME, '2016-05-31')+2 AS INT)
	Set @BasHaziran2015=CAST(CONVERT(SMALLDATETIME, '2016-06-01')+2 AS INT)	
	Set @BitHaziran2015=CAST(CONVERT(SMALLDATETIME, '2016-06-30')+2 AS INT)
	Set @BasTemmuz2015=CAST(CONVERT(SMALLDATETIME, '2016-07-01')+2 AS INT)	
	Set @BitTemmuz2015=CAST(CONVERT(SMALLDATETIME, '2016-07-31')+2 AS INT)
	Set @BasAgustos2015=CAST(CONVERT(SMALLDATETIME, '2016-08-01')+2 AS INT)	
	Set @BitAgustos2015=CAST(CONVERT(SMALLDATETIME, '2016-08-31')+2 AS INT)
	Set @BasEylul2015=CAST(CONVERT(SMALLDATETIME, '2016-09-01')+2 AS INT) 	
	Set @BitEylul2015=CAST(CONVERT(SMALLDATETIME, '2016-09-30')+2 AS INT)
	Set @BasEkim2015=CAST(CONVERT(SMALLDATETIME, '2016-10-01')+2 AS INT)  	
	Set @BitEkim2015=CAST(CONVERT(SMALLDATETIME, '2016-10-31')+2 AS INT)
	Set @BasKasim2015=CAST(CONVERT(SMALLDATETIME, '2016-11-01')+2 AS INT) 	
	Set @BitKasim2015=CAST(CONVERT(SMALLDATETIME, '2016-11-30')+2 AS INT)
	Set @BasAralik2015=CAST(CONVERT(SMALLDATETIME, '2016-12-01')+2 AS INT)	
	Set @BitAralik2015=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)

	Set @BasOcak2016=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)	    
	Set @BitOcak2016=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	Set @BasSubat2016=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT) 	
	Set @BitSubat2016=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	Set @BasMart2016=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
    Set @BitMart2016=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	Set @BasNisan2016=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)	    
	Set @BitNisan2016=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	Set @BasMayis2016=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)	    
	Set @BitMayis2016=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	Set @BasHaziran2016=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)	
	Set @BitHaziran2016=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	Set @BasTemmuz2016=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)	
	Set @BitTemmuz2016=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	Set @BasAgustos2016=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)	
	Set @BitAgustos2016=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	Set @BasEylul2016=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT) 	
	Set @BitEylul2016=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	Set @BasEkim2016=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)  	
	Set @BitEkim2016=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	Set @BasKasim2016=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT) 	
	Set @BitKasim2016=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	Set @BasAralik2016=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)	
	Set @BitAralik2016=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)

	Set @BasOcak2017=CAST(CONVERT(SMALLDATETIME, '2018-01-01')+2 AS INT)	    
	Set @BitOcak2017=CAST(CONVERT(SMALLDATETIME, '2018-01-31')+2 AS INT)
	Set @BasSubat2017=CAST(CONVERT(SMALLDATETIME, '2018-02-01')+2 AS INT) 	
	Set @BitSubat2017=CAST(CONVERT(SMALLDATETIME, '2018-02-28')+2 AS INT)
	Set @BasMart2017=CAST(CONVERT(SMALLDATETIME, '2018-03-01')+2 AS INT)
    Set @BitMart2017=CAST(CONVERT(SMALLDATETIME, '2018-03-31')+2 AS INT)
	Set @BasNisan2017=CAST(CONVERT(SMALLDATETIME, '2018-04-01')+2 AS INT)	    
	Set @BitNisan2017=CAST(CONVERT(SMALLDATETIME, '2018-04-30')+2 AS INT)
	Set @BasMayis2017=CAST(CONVERT(SMALLDATETIME, '2018-05-01')+2 AS INT)	    
	Set @BitMayis2017=CAST(CONVERT(SMALLDATETIME, '2018-05-31')+2 AS INT)
	Set @BasHaziran2017=CAST(CONVERT(SMALLDATETIME, '2018-06-01')+2 AS INT)	
	Set @BitHaziran2017=CAST(CONVERT(SMALLDATETIME, '2018-06-30')+2 AS INT)
	Set @BasTemmuz2017=CAST(CONVERT(SMALLDATETIME, '2018-07-01')+2 AS INT)	
	Set @BitTemmuz2017=CAST(CONVERT(SMALLDATETIME, '2018-07-31')+2 AS INT)
	Set @BasAgustos2017=CAST(CONVERT(SMALLDATETIME, '2018-08-01')+2 AS INT)	
	Set @BitAgustos2017=CAST(CONVERT(SMALLDATETIME, '2018-08-31')+2 AS INT)
	Set @BasEylul2017=CAST(CONVERT(SMALLDATETIME, '2018-09-01')+2 AS INT) 	
	Set @BitEylul2017=CAST(CONVERT(SMALLDATETIME, '2018-09-30')+2 AS INT)
	Set @BasEkim2017=CAST(CONVERT(SMALLDATETIME, '2018-10-01')+2 AS INT)  	
	Set @BitEkim2017=CAST(CONVERT(SMALLDATETIME, '2018-10-31')+2 AS INT)
	Set @BasKasim2017=CAST(CONVERT(SMALLDATETIME, '2018-11-01')+2 AS INT) 	
	Set @BitKasim2017=CAST(CONVERT(SMALLDATETIME, '2018-11-30')+2 AS INT)
	Set @BasAralik2017=CAST(CONVERT(SMALLDATETIME, '2018-12-01')+2 AS INT)	
	Set @BitAralik2017=CAST(CONVERT(SMALLDATETIME, '2018-12-31')+2 AS INT)

	-------------------- Ocak ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Ocak' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017

		 ) AS Yil2016

		 UNION ALL

	-------------------- Şubat ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Şubat' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017

		 ) AS Yil2018
	UNION ALL
	-------------------- Mart ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Mart' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017

		 ) AS Yil2018

		 UNION ALL

	-------------------- Nisan ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Nisan' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017

		 ) AS Yil2018
	UNION ALL
	-------------------- Mayıs ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Mayıs' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasMayis2015 AND @BitMayis2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasMayis2016 AND @BitMayis2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasMayis2017 AND @BitMayis2017

		 ) AS Yil2018

		 UNION ALL

	-------------------- Haziran ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Haziran' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017

		 ) AS Yil2018
	UNION ALL
	-------------------- Temmuz ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Temmuz' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017

		 ) AS Yil2018

		 UNION ALL

	-------------------- Agustos ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Agustos' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasAgustos2015 AND @BitAgustos2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasAgustos2016 AND @BitAgustos2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasAgustos2017 AND @BitAgustos2017

		 ) AS Yil2018
	UNION ALL
	-------------------- Eylul ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Eylul' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasEylul2015 AND @BitEylul2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasEylul2016 AND @BitEylul2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasEylul2017 AND @BitEylul2017

		 ) AS Yil2018

		 UNION ALL

	-------------------- Ekim ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Ekim' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017

		 ) AS Yil2018
	UNION ALL
	-------------------- Kasım ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Kasım' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017

		 ) AS Yil2018

		 UNION ALL

	-------------------- Aralık ------------------------------------------------------------------
	SELECT '33' as Sirket, 'Aralık' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016

		 ) AS Yil2017, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in (1,163) AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017

		 ) AS Yil2018
	


END

GO
/****** Object:  StoredProcedure [wms].[DB_Aylik_SatisAnalizi_CHK]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[DB_Aylik_SatisAnalizi_CHK]
@chk varchar(20)
AS
BEGIN

    DECLARE @BasOcak2015 INT, @BitOcak2015 INT,
	        @BasSubat2015 INT, @BitSubat2015 INT,
			@BasMart2015 INT, @BitMart2015 INT,
			@BasNisan2015 INT, @BitNisan2015 INT,
			@BasMayis2015 INT, @BitMayis2015 INT,
			@BasHaziran2015 INT, @BitHaziran2015 INT,
			@BasTemmuz2015 INT, @BitTemmuz2015 INT,
			@BasAgustos2015 INT, @BitAgustos2015 INT,
			@BasEylul2015 INT, @BitEylul2015 INT,
			@BasEkim2015 INT, @BitEkim2015 INT,
			@BasKasim2015 INT, @BitKasim2015 INT,
			@BasAralik2015 INT, @BitAralik2015 INT,
	        @BasOcak2016 INT, @BitOcak2016 INT,
	        @BasSubat2016 INT, @BitSubat2016 INT,
			@BasMart2016 INT, @BitMart2016 INT,
			@BasNisan2016 INT, @BitNisan2016 INT,
			@BasMayis2016 INT, @BitMayis2016 INT,
			@BasHaziran2016 INT, @BitHaziran2016 INT,
			@BasTemmuz2016 INT, @BitTemmuz2016 INT,
			@BasAgustos2016 INT, @BitAgustos2016 INT,
			@BasEylul2016 INT, @BitEylul2016 INT,
			@BasEkim2016 INT, @BitEkim2016 INT,
			@BasKasim2016 INT, @BitKasim2016 INT,
			@BasAralik2016 INT, @BitAralik2016 INT,
			@BasOcak2017 INT, @BitOcak2017 INT,
	        @BasSubat2017 INT, @BitSubat2017 INT,
			@BasMart2017 INT, @BitMart2017 INT,
			@BasNisan2017 INT, @BitNisan2017 INT,
			@BasMayis2017 INT, @BitMayis2017 INT,
			@BasHaziran2017 INT, @BitHaziran2017 INT,
			@BasTemmuz2017 INT, @BitTemmuz2017 INT,
			@BasAgustos2017 INT, @BitAgustos2017 INT,
			@BasEylul2017 INT, @BitEylul2017 INT,
			@BasEkim2017 INT, @BitEkim2017 INT,
			@BasKasim2017 INT, @BitKasim2017 INT,
			@BasAralik2017 INT, @BitAralik2017 INT
    
	Set @BasOcak2015=CAST(CONVERT(SMALLDATETIME, '2015-01-01')+2 AS INT)	    
	Set @BitOcak2015=CAST(CONVERT(SMALLDATETIME, '2015-01-31')+2 AS INT)
	Set @BasSubat2015=CAST(CONVERT(SMALLDATETIME, '2015-02-01')+2 AS INT) 	
	Set @BitSubat2015=CAST(CONVERT(SMALLDATETIME, '2015-02-28')+2 AS INT)
	Set @BasMart2015=CAST(CONVERT(SMALLDATETIME, '2015-03-01')+2 AS INT)
    Set @BitMart2015=CAST(CONVERT(SMALLDATETIME, '2015-03-31')+2 AS INT)
	Set @BasNisan2015=CAST(CONVERT(SMALLDATETIME, '2015-04-01')+2 AS INT)	    
	Set @BitNisan2015=CAST(CONVERT(SMALLDATETIME, '2015-04-30')+2 AS INT)
	Set @BasMayis2015=CAST(CONVERT(SMALLDATETIME, '2015-05-01')+2 AS INT)	    
	Set @BitMayis2015=CAST(CONVERT(SMALLDATETIME, '2015-05-31')+2 AS INT)
	Set @BasHaziran2015=CAST(CONVERT(SMALLDATETIME, '2015-06-01')+2 AS INT)	
	Set @BitHaziran2015=CAST(CONVERT(SMALLDATETIME, '2015-06-30')+2 AS INT)
	Set @BasTemmuz2015=CAST(CONVERT(SMALLDATETIME, '2015-07-01')+2 AS INT)	
	Set @BitTemmuz2015=CAST(CONVERT(SMALLDATETIME, '2015-07-31')+2 AS INT)
	Set @BasAgustos2015=CAST(CONVERT(SMALLDATETIME, '2015-08-01')+2 AS INT)	
	Set @BitAgustos2015=CAST(CONVERT(SMALLDATETIME, '2015-08-31')+2 AS INT)
	Set @BasEylul2015=CAST(CONVERT(SMALLDATETIME, '2015-09-01')+2 AS INT) 	
	Set @BitEylul2015=CAST(CONVERT(SMALLDATETIME, '2015-09-30')+2 AS INT)
	Set @BasEkim2015=CAST(CONVERT(SMALLDATETIME, '2015-10-01')+2 AS INT)  	
	Set @BitEkim2015=CAST(CONVERT(SMALLDATETIME, '2015-10-31')+2 AS INT)
	Set @BasKasim2015=CAST(CONVERT(SMALLDATETIME, '2015-11-01')+2 AS INT) 	
	Set @BitKasim2015=CAST(CONVERT(SMALLDATETIME, '2015-11-30')+2 AS INT)
	Set @BasAralik2015=CAST(CONVERT(SMALLDATETIME, '2015-12-01')+2 AS INT)	
	Set @BitAralik2015=CAST(CONVERT(SMALLDATETIME, '2015-12-31')+2 AS INT)

	Set @BasOcak2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)	    
	Set @BitOcak2016=CAST(CONVERT(SMALLDATETIME, '2016-01-31')+2 AS INT)
	Set @BasSubat2016=CAST(CONVERT(SMALLDATETIME, '2016-02-01')+2 AS INT) 	
	Set @BitSubat2016=CAST(CONVERT(SMALLDATETIME, '2016-02-29')+2 AS INT)
	Set @BasMart2016=CAST(CONVERT(SMALLDATETIME, '2016-03-01')+2 AS INT)
    Set @BitMart2016=CAST(CONVERT(SMALLDATETIME, '2016-03-31')+2 AS INT)
	Set @BasNisan2016=CAST(CONVERT(SMALLDATETIME, '2016-04-01')+2 AS INT)	    
	Set @BitNisan2016=CAST(CONVERT(SMALLDATETIME, '2016-04-30')+2 AS INT)
	Set @BasMayis2016=CAST(CONVERT(SMALLDATETIME, '2016-05-01')+2 AS INT)	    
	Set @BitMayis2016=CAST(CONVERT(SMALLDATETIME, '2016-05-31')+2 AS INT)
	Set @BasHaziran2016=CAST(CONVERT(SMALLDATETIME, '2016-06-01')+2 AS INT)	
	Set @BitHaziran2016=CAST(CONVERT(SMALLDATETIME, '2016-06-30')+2 AS INT)
	Set @BasTemmuz2016=CAST(CONVERT(SMALLDATETIME, '2016-07-01')+2 AS INT)	
	Set @BitTemmuz2016=CAST(CONVERT(SMALLDATETIME, '2016-07-31')+2 AS INT)
	Set @BasAgustos2016=CAST(CONVERT(SMALLDATETIME, '2016-08-01')+2 AS INT)	
	Set @BitAgustos2016=CAST(CONVERT(SMALLDATETIME, '2016-08-31')+2 AS INT)
	Set @BasEylul2016=CAST(CONVERT(SMALLDATETIME, '2016-09-01')+2 AS INT) 	
	Set @BitEylul2016=CAST(CONVERT(SMALLDATETIME, '2016-09-30')+2 AS INT)
	Set @BasEkim2016=CAST(CONVERT(SMALLDATETIME, '2016-10-01')+2 AS INT)  	
	Set @BitEkim2016=CAST(CONVERT(SMALLDATETIME, '2016-10-31')+2 AS INT)
	Set @BasKasim2016=CAST(CONVERT(SMALLDATETIME, '2016-11-01')+2 AS INT) 	
	Set @BitKasim2016=CAST(CONVERT(SMALLDATETIME, '2016-11-30')+2 AS INT)
	Set @BasAralik2016=CAST(CONVERT(SMALLDATETIME, '2016-12-01')+2 AS INT)	
	Set @BitAralik2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)

	Set @BasOcak2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)	    
	Set @BitOcak2017=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	Set @BasSubat2017=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT) 	
	Set @BitSubat2017=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	Set @BasMart2017=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
    Set @BitMart2017=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	Set @BasNisan2017=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)	    
	Set @BitNisan2017=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	Set @BasMayis2017=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)	    
	Set @BitMayis2017=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	Set @BasHaziran2017=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)	
	Set @BitHaziran2017=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	Set @BasTemmuz2017=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)	
	Set @BitTemmuz2017=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	Set @BasAgustos2017=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)	
	Set @BitAgustos2017=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	Set @BasEylul2017=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT) 	
	Set @BitEylul2017=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	Set @BasEkim2017=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)  	
	Set @BitEkim2017=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	Set @BasKasim2017=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT) 	
	Set @BitKasim2017=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	Set @BasAralik2017=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)	
	Set @BitAralik2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)

	-------------------- 2017 ------------------------------------------------------------------
	SELECT 'Ocak' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017 AND STI.Chk=@chk

		 ) AS Yil2017

		 UNION ALL

	SELECT 'Şubat' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017 AND STI.Chk=@chk

		 ) AS Yil2017
	UNION ALL
	SELECT 'Mart' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017 AND STI.Chk=@chk

		 ) AS Yil2017

		 UNION ALL

		 		  SELECT 'Nisan' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017 AND STI.Chk=@chk

		 ) AS Yil2017
	UNION ALL
	SELECT 'Mayıs' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasMayis2015 AND @BasMayis2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasMayis2016 AND @BasMayis2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasMayis2017 AND @BasMayis2017 AND STI.Chk=@chk

		 ) AS Yil2017

		 UNION ALL

		 		  SELECT 'Haziran' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017 AND STI.Chk=@chk

		 ) AS Yil2017
	UNION ALL
	SELECT 'Temmuz' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017 AND STI.Chk=@chk

		 ) AS Yil2017

		 UNION ALL

		 		  SELECT 'Agustos' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasAgustos2015 AND @BitAgustos2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasAgustos2016 AND @BitAgustos2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasAgustos2017 AND @BitAgustos2017 AND STI.Chk=@chk

		 ) AS Yil2017
	UNION ALL
	SELECT 'Eylul' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasEylul2015 AND @BitEylul2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasEylul2016 AND @BitEylul2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasEylul2017 AND @BitEylul2017 AND STI.Chk=@chk

		 ) AS Yil2017

		 UNION ALL

		 		  SELECT 'Ekim' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017 AND STI.Chk=@chk

		 ) AS Yil2017
	UNION ALL
	SELECT 'Kasım' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017 AND STI.Chk=@chk

		 ) AS Yil2017

		 UNION ALL

		 		  SELECT 'Aralık' Ay , ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015 AND STI.Chk=@chk

		 ) AS Yil2015, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016 AND STI.Chk=@chk

		 ) AS Yil2016, ( SELECT ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar

	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017 AND STI.Chk=@chk

		 ) AS Yil2017
	


END












GO
/****** Object:  StoredProcedure [wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz]
@Grup VARCHAR(10),
@Kriter VARCHAR(10),
@IslemTip SMALLINT
AS
BEGIN

    DECLARE @BasOcak2015 INT, @BitOcak2015 INT,
	        @BasSubat2015 INT, @BitSubat2015 INT,
			@BasMart2015 INT, @BitMart2015 INT,
			@BasNisan2015 INT, @BitNisan2015 INT,
			@BasMayis2015 INT, @BitMayis2015 INT,
			@BasHaziran2015 INT, @BitHaziran2015 INT,
			@BasTemmuz2015 INT, @BitTemmuz2015 INT,
			@BasAğustos2015 INT, @BitAğustos2015 INT,
			@BasEylül2015 INT, @BitEylül2015 INT,
			@BasEkim2015 INT, @BitEkim2015 INT,
			@BasKasim2015 INT, @BitKasim2015 INT,
			@BasAralik2015 INT, @BitAralik2015 INT,
	        @BasOcak2016 INT, @BitOcak2016 INT,
	        @BasSubat2016 INT, @BitSubat2016 INT,
			@BasMart2016 INT, @BitMart2016 INT,
			@BasNisan2016 INT, @BitNisan2016 INT,
			@BasMayis2016 INT, @BitMayis2016 INT,
			@BasHaziran2016 INT, @BitHaziran2016 INT,
			@BasTemmuz2016 INT, @BitTemmuz2016 INT,
			@BasAğustos2016 INT, @BitAğustos2016 INT,
			@BasEylül2016 INT, @BitEylül2016 INT,
			@BasEkim2016 INT, @BitEkim2016 INT,
			@BasKasim2016 INT, @BitKasim2016 INT,
			@BasAralik2016 INT, @BitAralik2016 INT,
			@BasOcak2017 INT, @BitOcak2017 INT,
	        @BasSubat2017 INT, @BitSubat2017 INT,
			@BasMart2017 INT, @BitMart2017 INT,
			@BasNisan2017 INT, @BitNisan2017 INT,
			@BasMayis2017 INT, @BitMayis2017 INT,
			@BasHaziran2017 INT, @BitHaziran2017 INT,
			@BasTemmuz2017 INT, @BitTemmuz2017 INT,
			@BasAğustos2017 INT, @BitAğustos2017 INT,
			@BasEylül2017 INT, @BitEylül2017 INT,
			@BasEkim2017 INT, @BitEkim2017 INT,
			@BasKasim2017 INT, @BitKasim2017 INT,
			@BasAralik2017 INT, @BitAralik2017 INT
    
	Set @BasOcak2015=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)	    
	Set @BitOcak2015=CAST(CONVERT(SMALLDATETIME, '2016-01-31')+2 AS INT)
	Set @BasSubat2015=CAST(CONVERT(SMALLDATETIME, '2016-02-01')+2 AS INT) 	
	Set @BitSubat2015=CAST(CONVERT(SMALLDATETIME, '2016-02-29')+2 AS INT)
	Set @BasMart2015=CAST(CONVERT(SMALLDATETIME, '2016-03-01')+2 AS INT)
    Set @BitMart2015=CAST(CONVERT(SMALLDATETIME, '2016-03-31')+2 AS INT)
	Set @BasNisan2015=CAST(CONVERT(SMALLDATETIME, '2016-04-01')+2 AS INT)	    
	Set @BitNisan2015=CAST(CONVERT(SMALLDATETIME, '2016-04-30')+2 AS INT)
	Set @BasMayis2015=CAST(CONVERT(SMALLDATETIME, '2016-05-01')+2 AS INT)	    
	Set @BitMayis2015=CAST(CONVERT(SMALLDATETIME, '2016-05-31')+2 AS INT)
	Set @BasHaziran2015=CAST(CONVERT(SMALLDATETIME, '2016-06-01')+2 AS INT)	
	Set @BitHaziran2015=CAST(CONVERT(SMALLDATETIME, '2016-06-30')+2 AS INT)
	Set @BasTemmuz2015=CAST(CONVERT(SMALLDATETIME, '2016-07-01')+2 AS INT)	
	Set @BitTemmuz2015=CAST(CONVERT(SMALLDATETIME, '2016-07-31')+2 AS INT)
	Set @BasAğustos2015=CAST(CONVERT(SMALLDATETIME, '2016-08-01')+2 AS INT)	
	Set @BitAğustos2015=CAST(CONVERT(SMALLDATETIME, '2016-08-31')+2 AS INT)
	Set @BasEylül2015=CAST(CONVERT(SMALLDATETIME, '2016-09-01')+2 AS INT) 	
	Set @BitEylül2015=CAST(CONVERT(SMALLDATETIME, '2016-09-30')+2 AS INT)
	Set @BasEkim2015=CAST(CONVERT(SMALLDATETIME, '2016-10-01')+2 AS INT)  	
	Set @BitEkim2015=CAST(CONVERT(SMALLDATETIME, '2016-10-31')+2 AS INT)
	Set @BasKasim2015=CAST(CONVERT(SMALLDATETIME, '2016-11-01')+2 AS INT) 	
	Set @BitKasim2015=CAST(CONVERT(SMALLDATETIME, '2016-11-30')+2 AS INT)
	Set @BasAralik2015=CAST(CONVERT(SMALLDATETIME, '2016-12-01')+2 AS INT)	
	Set @BitAralik2015=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)

	Set @BasOcak2016=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)	    
	Set @BitOcak2016=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	Set @BasSubat2016=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT) 	
	Set @BitSubat2016=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	Set @BasMart2016=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
    Set @BitMart2016=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	Set @BasNisan2016=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)	    
	Set @BitNisan2016=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	Set @BasMayis2016=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)	    
	Set @BitMayis2016=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	Set @BasHaziran2016=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)	
	Set @BitHaziran2016=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	Set @BasTemmuz2016=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)	
	Set @BitTemmuz2016=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	Set @BasAğustos2016=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)	
	Set @BitAğustos2016=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	Set @BasEylül2016=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT) 	
	Set @BitEylül2016=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	Set @BasEkim2016=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)  	
	Set @BitEkim2016=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	Set @BasKasim2016=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT) 	
	Set @BitKasim2016=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	Set @BasAralik2016=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)	
	Set @BitAralik2016=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)

	Set @BasOcak2017=CAST(CONVERT(SMALLDATETIME, '2018-01-01')+2 AS INT)	    
	Set @BitOcak2017=CAST(CONVERT(SMALLDATETIME, '2018-01-31')+2 AS INT)
	Set @BasSubat2017=CAST(CONVERT(SMALLDATETIME, '2018-02-01')+2 AS INT) 	
	Set @BitSubat2017=CAST(CONVERT(SMALLDATETIME, '2018-02-28')+2 AS INT)
	Set @BasMart2017=CAST(CONVERT(SMALLDATETIME, '2018-03-01')+2 AS INT)
    Set @BitMart2017=CAST(CONVERT(SMALLDATETIME, '2018-03-31')+2 AS INT)
	Set @BasNisan2017=CAST(CONVERT(SMALLDATETIME, '2018-04-01')+2 AS INT)	    
	Set @BitNisan2017=CAST(CONVERT(SMALLDATETIME, '2018-04-30')+2 AS INT)
	Set @BasMayis2017=CAST(CONVERT(SMALLDATETIME, '2018-05-01')+2 AS INT)	    
	Set @BitMayis2017=CAST(CONVERT(SMALLDATETIME, '2018-05-31')+2 AS INT)
	Set @BasHaziran2017=CAST(CONVERT(SMALLDATETIME, '2018-06-01')+2 AS INT)	
	Set @BitHaziran2017=CAST(CONVERT(SMALLDATETIME, '2018-06-30')+2 AS INT)
	Set @BasTemmuz2017=CAST(CONVERT(SMALLDATETIME, '2018-07-01')+2 AS INT)	
	Set @BitTemmuz2017=CAST(CONVERT(SMALLDATETIME, '2018-07-31')+2 AS INT)
	Set @BasAğustos2017=CAST(CONVERT(SMALLDATETIME, '2018-08-01')+2 AS INT)	
	Set @BitAğustos2017=CAST(CONVERT(SMALLDATETIME, '2018-08-31')+2 AS INT)
	Set @BasEylül2017=CAST(CONVERT(SMALLDATETIME, '2018-09-01')+2 AS INT) 	
	Set @BitEylül2017=CAST(CONVERT(SMALLDATETIME, '2018-09-30')+2 AS INT)
	Set @BasEkim2017=CAST(CONVERT(SMALLDATETIME, '2018-10-01')+2 AS INT)  	
	Set @BitEkim2017=CAST(CONVERT(SMALLDATETIME, '2018-10-31')+2 AS INT)
	Set @BasKasim2017=CAST(CONVERT(SMALLDATETIME, '2018-11-01')+2 AS INT) 	
	Set @BitKasim2017=CAST(CONVERT(SMALLDATETIME, '2018-11-30')+2 AS INT)
	Set @BasAralik2017=CAST(CONVERT(SMALLDATETIME, '2018-12-01')+2 AS INT)	
	Set @BitAralik2017=CAST(CONVERT(SMALLDATETIME, '2018-12-31')+2 AS INT)

	 IF(@Grup='TÜMÜ')
	 BEGIN
			IF(@Kriter='TL')
			BEGIN
				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ocak' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END))  
						 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 

							) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Şubat' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					  ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE')
					   
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	  
					  ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					  ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017
				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mart' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	  
					  ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTuta
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					  ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	  
					  ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Nisan' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mayıs' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasMayis2015 AND @BitMayis2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasMayis2016 AND @BitMayis2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasMayis2017 AND @BitMayis2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Haziran' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Temmuz' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ağustos' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasAğustos2015 AND @BitAğustos2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasAğustos2016 AND @BitAğustos2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasAğustos2017 AND @BitAğustos2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Eylül' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasEylül2015 AND @BitEylül2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasEylül2016 AND @BitEylül2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasEylül2017 AND @BitEylül2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ekim' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Kasım' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 	
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Aralık' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,163) AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--and (STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) 
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017
			END
			ELSE IF(@Kriter='Döviz')
			BEGIN
				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ocak' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
						 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				 INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
							) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
	
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Şubat' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017
				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mart' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Nisan' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mayıs' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2015 AND @BitMayis2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2016 AND @BitMayis2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2017 AND @BitMayis2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Haziran' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Temmuz' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				 INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ağustos' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2015 AND @BitAğustos2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2016 AND @BitAğustos2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2017 AND @BitAğustos2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Eylül' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2015 AND @BitEylül2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2016 AND @BitEylül2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2017 AND @BitEylül2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ekim' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Kasım' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
			( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Aralık' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017
			END
			ELSE
			BEGIN
				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ocak' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
						 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				--STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
							) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
	
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Şubat' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017
				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mart' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Nisan' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mayıs' Ay ,
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2015 AND @BitMayis2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2016 AND @BitMayis2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2017 AND @BitMayis2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Haziran' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Temmuz' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ağustos' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2015 AND @BitAğustos2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2016 AND @BitAğustos2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2017 AND @BitAğustos2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Eylül' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2015 AND @BitEylül2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2016 AND @BitEylül2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2017 AND @BitEylül2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ekim' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Kasım' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Aralık' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					 ----STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
					 ) AS Yil2017
		    END
	 END
	 ELSE 
	 BEGIN
	        IF(@Kriter='TL')
			BEGIN
				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ocak' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				 AND STK.MalAdi4=@Grup
						 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				 AND STK.MalAdi4=@Grup
							) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Şubat' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2017
				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mart' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTuta
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Nisan' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mayıs' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2015 AND @BitMayis2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2016 AND @BitMayis2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2017 AND @BitMayis2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Haziran' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Temmuz' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ağustos' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2015 AND @BitAğustos2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2016 AND @BitAğustos2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2017 AND @BitAğustos2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Eylül' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2015 AND @BitEylül2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2016 AND @BitEylül2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2017 AND @BitEylül2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ekim' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Kasım' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Aralık' Ay , 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017
			END
			ELSE IF(@Kriter='Döviz')
			BEGIN
				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ocak' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR' 
				 
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				 AND STK.MalAdi4=@Grup
						 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				 AND STK.MalAdi4=@Grup 
							) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Şubat' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup 
					 ) AS Yil2017
				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mart' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
			( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Nisan' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
			( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Mayıs' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2015 AND @BitMayis2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
			( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2016 AND @BitMayis2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2017 AND @BitMayis2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Haziran' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Temmuz' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ağustos' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2015 AND @BitAğustos2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2016 AND @BitAğustos2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
			( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2017 AND @BitAğustos2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Eylül' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2015 AND @BitEylül2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2016 AND @BitEylül2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2017 AND @BitEylül2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Ekim' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Kasım' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0)AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip, 'Aralık' Ay , 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
			( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,25,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)/DVZ.KUR00
	ELSE
	((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar + STI.KDV)*-1) /DVZ.Kur00 END
	),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'  
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017
			END
			ELSE
			BEGIN
				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Ocak' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2015 AND @BitOcak2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				 AND STK.MalAdi4=@Grup
						 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2016 AND @BitOcak2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
				 AND STK.MalAdi4=@Grup
							) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasOcak2017 AND @BitOcak2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Şubat' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2015 AND @BitSubat2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2016 AND @BitSubat2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasSubat2017 AND @BitSubat2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2017
				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Mart' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2015 AND @BitMart2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2016 AND @BitMart2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMart2017 AND @BitMart2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Nisan' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2015 AND @BitNisan2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2016 AND @BitNisan2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasNisan2017 AND @BitNisan2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Mayıs' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2015 AND @BitMayis2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2016 AND @BitMayis2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasMayis2017 AND @BitMayis2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Haziran' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2015 AND @BitHaziran2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2016 AND @BitHaziran2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasHaziran2017 AND @BitHaziran2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Temmuz' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2015 AND @BitTemmuz2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2016 AND @BitTemmuz2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasTemmuz2017 AND @BitTemmuz2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Ağustos' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2015 AND @BitAğustos2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2016 AND @BitAğustos2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAğustos2017 AND @BitAğustos2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Eylül' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2015 AND @BitEylül2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2016 AND @BitEylül2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEylül2017 AND @BitEylül2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Ekim' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2015 AND @BitEkim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2016 AND @BitEkim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasEkim2017 AND @BitEkim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup 
					 ) AS Yil2017

				UNION ALL

				SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Kasım' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2015 AND @BitKasim2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2016 AND @BitKasim2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasKasim2017 AND @BitKasim2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2017

				UNION ALL

		 		SELECT '33' as DB, @Grup as Grup, @Kriter as Kriter, @IslemTip as IslemTip,'Aralık' Ay , 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2015 AND @BitAralik2015 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2015, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2016 AND @BitAralik2016 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					   AND STK.MalAdi4=@Grup
					 ) AS Yil2016, 
				( SELECT ISNULL(SUM(STI.BirimMiktar),0) AS GenelTutar
				FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
				INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
				WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @BasAralik2017 AND @BitAralik2017 AND STI.IslemTip=(CASE WHEN @IslemTip=0 THEN STI.IslemTip ELSE @IslemTip END)
					  AND STK.MalAdi4=@Grup
					 ) AS Yil2017
		    END
	 END

END













GO
/****** Object:  StoredProcedure [wms].[DB_BaglantiLogGetir]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[DB_BaglantiLogGetir]
AS
BEGIN 

	SELECT GETDATE() Tarih,0 as KalanTutar 

END








GO
/****** Object:  StoredProcedure [wms].[DB_BakiyeRiskAnalizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[DB_BakiyeRiskAnalizi]

AS
BEGIN

	DECLARE @BasTarih INT, @BitTarih INT, @VadeBaslangic INT, @VadeBitis INT
	SET @BasTarih=36526
	SET @BitTarih=45200
	SET @VadeBaslangic=36526
	SET @VadeBitis=45200
		
	SELECT        TOP (10) '33' as DB, HesapKodu, Unvan, Borc, Alacak, Bakiye
	FROM            (SELECT        a.HesapKodu, SUBSTRING(ISNULL(MAX(b.Unvan1 + ' ' + b.Unvan2), ''), 1, 20) AS Unvan, ISNULL(SUM(CASE WHEN a.BA = 0 THEN a.Tutar ELSE 0 END), 0) AS Borc, 
														ISNULL(SUM(CASE WHEN a.BA = 1 THEN a.Tutar ELSE 0 END), 0) AS Alacak, ISNULL(SUM(CASE WHEN a.BA = 0 THEN a.Tutar ELSE - a.Tutar END), 0) AS Bakiye
							  FROM            (SELECT        KarsiHesapKodu AS HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, (CASE WHEN BA = 0 THEN 1 ELSE 0 END) AS BA
														FROM            FINSAT633.CHI WITH (nolock)
														WHERE        (KarsiHesapKodu IS NOT NULL) AND (KarsiHesapKodu <> '') AND (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60)) AND (Tarih BETWEEN @BasTarih AND @BitTarih) AND (VadeTarih BETWEEN 
																				  @VadeBaslangic AND @VadeBitis)
														UNION ALL
														SELECT        HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, BA
														FROM            FINSAT633.CHI WITH (nolock)
														WHERE        (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60)) AND (Tarih BETWEEN @BasTarih AND @BitTarih) AND (VadeTarih BETWEEN @VadeBaslangic AND @VadeBitis)) AS a INNER JOIN
														FINSAT633.CHK AS b WITH (nolock) ON b.HesapKodu = a.HesapKodu
							  WHERE        (b.MhsKod LIKE '120%') AND (b.KartTip IN (0, 1, 4))
							  GROUP BY a.HesapKodu, b.MhsKod) AS B
	WHERE        (Bakiye > 0) AND (HesapKodu LIKE '120%')
	ORDER BY Bakiye DESC

END

GO
/****** Object:  StoredProcedure [wms].[DB_BekleyenOnaylar]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [wms].[DB_BekleyenOnaylar]
AS
BEGIN

DECLARE @BekleyenSiparisAdet_SM INT, @BekleyenSiparisAdet_SPGMY INT, 
        @BekleyenSiparisAdet_GM INT, @OnayBekleyenStokAdet INT, --Stok Tek Onaylı
		@BekleyenSozlesmeAdet_SM INT, @BekleyenSozlesmeAdet_SPGMY INT, 
        @BekleyenSozlesmeAdet_GM INT, @BekleyenRiskLimitAdet_SM INT, 
		@BekleyenRiskLimitAdet_SPGMY INT, @BekleyenRiskLimitAdet_MIGMY INT, 
        @BekleyenRiskLimitAdet_GM INT,  @OnayBekleyenTeminatAdet INT, --Teminat Tek Onaylı
		@BekleyenFiyatAdet_SM INT,  @BekleyenFiyatAdet_SPGMY INT, 
        @BekleyenFiyatAdet_GM INT,  @BekleyenCekAdet_SPGMY INT, --Çeklerde SM yok
		@BekleyenCekAdet_MIGMY INT, @BekleyenCekAdet_GM INT,

		@KurumKartlari INT, @GorusmeNotlari INT, @TeklifAnalizi INT, -- Onaylama Olmayanlar
		@KurumKartlariBuHafta INT, @GorusmeNotlariBuHafta INT, @TeklifAnaliziBuHafta INT,
	    @KurumKartlariBugun INT, @GorusmeNotlariBugun INT, @TeklifAnaliziBugun INT,

		@YeniIhaleAdet INT, @YeniTahsisAdet INT,
		@NakliyeFiyatAdet INT,
		@SatinalmaSipTalepGMAdet INT
		

--- 1-) BEKLEYEN SİPARİŞ ONAYLARI 
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'SiparisOnay'))
	BEGIN

	  SET @BekleyenSiparisAdet_SM = (SELECT COUNT(EvrakNo) AS ADET 
		FROM	
		(	
			SELECT SPI.EvrakNo
			FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
			INNER JOIN FINSAT633.FINSAT633.SiparisOnay (NOLOCK) SO 
			ON SPI.EvrakNo = SO.EvrakNo
			WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 
			AND SO.Durum IN (1)
			AND 
			(
				(SO.OnayTip = 1 AND SO.SMOnay = 0) OR
				(SO.OnayTip = 2 AND SO.SMOnay = 0 AND SO.SPGMYOnay = 0) OR
				(SO.OnayTip = 3 AND SO.SMOnay = 0 AND SO.SPGMYOnay = 0 AND SO.GMOnay = 0)
				OR (SO.OnayTip = 3 AND SO.SMOnay = 0 )  
			)
			GROUP BY SPI.EvrakNo 
		) Siparis)

		  SET @BekleyenSiparisAdet_SPGMY = (SELECT COUNT(EvrakNo) AS ADET 
			FROM	
			(	
				SELECT SPI.EvrakNo
				FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
				INNER JOIN FINSAT633.FINSAT633.SiparisOnay (NOLOCK) SO 
				ON SPI.EvrakNo = SO.EvrakNo
				WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 
				AND SO.Durum IN (1)
				AND 
				(
					(SO.OnayTip = 2 AND SO.SMOnay = 1 AND SO.SPGMYOnay = 0) OR
					(SO.OnayTip = 3 AND SO.SMOnay = 1 AND SO.SPGMYOnay = 0 AND SO.GMOnay = 0)	
				)
				GROUP BY SPI.EvrakNo 
			) Siparis)

		  SET @BekleyenSiparisAdet_GM = (SELECT COUNT(EvrakNo) AS ADET 
			FROM	
			(	
				SELECT SPI.EvrakNo
				FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
				INNER JOIN FINSAT633.FINSAT633.SiparisOnay (NOLOCK) SO 
				ON SPI.EvrakNo = SO.EvrakNo
				WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 
				AND SO.Durum IN (1)
				AND 
				(
					(SO.OnayTip = 3 AND SO.SMOnay = 1 AND SO.SPGMYOnay = 1 AND SO.GMOnay = 0)
				)
				GROUP BY SPI.EvrakNo 
			) Siparis)
	END
	else
	begin
		SET @BekleyenSiparisAdet_SM = 0
		SET @BekleyenSiparisAdet_SPGMY = 0
		SET @BekleyenSiparisAdet_GM = 0
	end

---  2-) BEKLEYEN STOK ONAYLARI        
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'STK_Temp'))
		  SET @OnayBekleyenStokAdet = (SELECT COUNT(MalKodu) FROM FINSAT633.FINSAT633.STK_Temp(NOLOCK))
	else
		  SET @OnayBekleyenStokAdet = 0

---  3-) BEKLEYEN SÖZLEŞME ONAYLARI      
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'ISS_Temp'))
	begin
	   SET @BekleyenSozlesmeAdet_SM = (SELECT COUNT(ListeNo) 
		FROM
		(
  			SELECT ListeNo 
			FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
			WHERE (OnayTip=0 AND SatisMuduruOnay=0) OR
			(OnayTip=1 AND SatisMuduruOnay=0 AND FinansmanMuduruOnay = 0) OR
			(OnayTip=2 AND SatisMuduruOnay=0 AND FinansmanMuduruOnay=0 AND GenelMudurOnay=0) 
			 OR (OnayTip=2 AND SatisMuduruOnay=0 AND GenelMudurOnay = 0)           
			GROUP BY ListeNo
		) Sozlesme)

	   SET @BekleyenSozlesmeAdet_SPGMY = (SELECT COUNT(ListeNo) 
		FROM
		(
  			SELECT ListeNo 
			FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
			WHERE  (OnayTip=1 AND SatisMuduruOnay=1 AND FinansmanMuduruOnay = 0) OR
			(OnayTip=2 AND SatisMuduruOnay=1 AND FinansmanMuduruOnay=0 AND GenelMudurOnay=0)          
			GROUP BY ListeNo
		) Sozlesme)

	   SET @BekleyenSozlesmeAdet_GM = (SELECT COUNT(ListeNo) 
		FROM
		(
  			SELECT ListeNo 
			FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
			WHERE (OnayTip=2 AND SatisMuduruOnay=1 AND FinansmanMuduruOnay=1 AND GenelMudurOnay=0)          
			GROUP BY ListeNo
		) Sozlesme)
	END
	else
	begin
		SET @BekleyenSozlesmeAdet_SM = 0
		SET @BekleyenSozlesmeAdet_SPGMY = 0
		SET @BekleyenSozlesmeAdet_GM = 0
	end

---  4-) BEKLEYEN RİSK LİMİT ONAYLARI   
--Risk Limit onayları için procedureler yok burası Silver projesi altındaki selectlerden baz alınarak hazırlanmıştır.
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'RiskTanim'))
	begin
		   SET @BekleyenRiskLimitAdet_SM = (SELECT COUNT(ID) FROM FINSAT633.FINSAT633.RiskTanim(NOLOCK) 
											WHERE OnayTip=0 AND SMOnay=0 AND Durum=0)

		   SET @BekleyenRiskLimitAdet_SPGMY = (SELECT COUNT(ID) FROM FINSAT633.FINSAT633.RiskTanim(NOLOCK) 
										WHERE (OnayTip=1 AND SPGMYOnay=0) OR (OnayTip=2 AND SPGMYOnay=0) 
											  OR (OnayTip=3 AND SPGMYOnay=0) AND Durum=0)

		   SET @BekleyenRiskLimitAdet_MIGMY = (SELECT COUNT(ID) FROM FINSAT633.FINSAT633.RiskTanim(NOLOCK) 
										WHERE (OnayTip=2 AND SPGMYOnay=1 AND MIGMYOnay=0) OR
											  (OnayTip=3 AND SPGMYOnay=1 AND MIGMYOnay=0) AND Durum=0)

		   SET @BekleyenRiskLimitAdet_GM = (SELECT COUNT(ID) FROM FINSAT633.FINSAT633.RiskTanim(NOLOCK) 
										WHERE (OnayTip=3 AND SPGMYOnay=1 AND MIGMYOnay=1 AND GMOnay=0) OR
											  (OnayTip=4 AND GMOnay=0) AND Durum=0)
	END
	else
	begin
		SET @BekleyenRiskLimitAdet_SM = 0
		SET @BekleyenRiskLimitAdet_SPGMY = 0
		SET @BekleyenRiskLimitAdet_MIGMY = 0
		SET @BekleyenRiskLimitAdet_GM = 0
	end

---  5-) BEKLEYEN TEMİNAT ONAYLARI
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'Teminat'))
	   SET @OnayBekleyenTeminatAdet = (SELECT COUNT(ID)	FROM FINSAT633.FINSAT633.Teminat(NOLOCK) WHERE Onay=0)
	else
		SET @OnayBekleyenTeminatAdet = 0

---  6-) BEKLEYEN FİYAT ONAYLARI
	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'Fiyat'))
	begin
	   SET @BekleyenFiyatAdet_SM = (SELECT COUNT(STK.MalKodu)
								   FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) Fiyat
								   INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK 
								   ON Fiyat.MalKodu=STK.MalKodu
								   WHERE Onay=0 AND SPGMYOnay=0 AND GMOnay=0)

	   SET @BekleyenFiyatAdet_SPGMY = (SELECT COUNT(STK.MalKodu)
								   FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) Fiyat
								   INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK 
								   ON Fiyat.MalKodu=STK.MalKodu
								   WHERE Onay=1 and SPGMYOnay=0 and GMOnay=0)

	   SET @BekleyenFiyatAdet_GM = (SELECT COUNT(STK.MalKodu)
								   FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) Fiyat
								   INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK 
								   ON Fiyat.MalKodu=STK.MalKodu
								   WHERE Onay=1 and SPGMYOnay=1 and GMOnay=0)
	END
	else
	begin
		SET @BekleyenFiyatAdet_SM = 0
		SET @BekleyenFiyatAdet_SPGMY = 0
		SET @BekleyenFiyatAdet_GM = 0
	end


---  7-) BEKLEYEN ÇEK ONAYLARI
 	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'CEK'))
	begin
		  SET @BekleyenCekAdet_SPGMY = (SELECT COUNT(ID) FROM FINSAT633.FINSAT633.CEK(NOLOCK)
										 WHERE ISNULL(SPGMYOnay,0)=0 AND ISNULL(MIGMYOnay,0)=0 AND
										 ISNULL(GMOnay,0)=0 AND Durum=0)

		   SET @BekleyenCekAdet_MIGMY = (SELECT COUNT(ID) FROM FINSAT633.FINSAT633.CEK(NOLOCK)
										 WHERE ISNULL(SPGMYOnay,0)=1 AND ISNULL(MIGMYOnay,0)=0 AND
										 ISNULL(GMOnay,0)=0 AND Durum=0)

		   SET @BekleyenCekAdet_GM = (SELECT COUNT(ID) FROM FINSAT633.FINSAT633.CEK(NOLOCK)
										 WHERE ISNULL(SPGMYOnay,0)=1 AND ISNULL(MIGMYOnay,0)=1 AND
										 ISNULL(GMOnay,0)=0 AND Durum=0)
	END
	else
	begin
		SET @BekleyenCekAdet_SPGMY = 0
		SET @BekleyenCekAdet_MIGMY = 0
		SET @BekleyenCekAdet_GM = 0
	end



---  8-) CRM ÖZET BİLGİLERİ

   SET @KurumKartlari =0
   SET @GorusmeNotlari = 0
   SET @TeklifAnalizi = 0

   SET @KurumKartlariBuHafta = 0

   SET @KurumKartlariBugun = 0

   SET @GorusmeNotlariBuHafta = 0

	SET @GorusmeNotlariBugun =0

	 SET @TeklifAnaliziBuHafta = 0

	 SET @TeklifAnaliziBugun = 0



 	IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_SCHEMA = 'FINSAT633'  AND  TABLE_NAME = 'IHLTAH'))
	begin
		SET @YeniIhaleAdet = (SELECT COUNT(*) FROM FINSAT633.FINSAT633.IHLTAH (nolock) WHERE Tip=0 AND Onay=0)

		SET @YeniTahsisAdet= (SELECT COUNT(*) FROM FINSAT633.FINSAT633.IHLTAH (nolock) WHERE Tip=2 AND Onay=0)
	END
	else
	begin
		SET @YeniIhaleAdet = 0
		SET @YeniTahsisAdet = 0
	end

	SET @NakliyeFiyatAdet = (SELECT COUNT(*) FROM FINSAT633.FINSAT633.DEP (nolock) WHERE Depo LIKE 'H%' AND Kod2<>'')


	SET @SatinalmaSipTalepGMAdet = 0



----- SONUÇLAR GÖNDERİLİYOR -------------
  
     SELECT @BekleyenSiparisAdet_SM AS SiparisOnay_SM, 
	        @BekleyenSiparisAdet_SPGMY AS SiparisOnay_SPGMY, 
			@BekleyenSiparisAdet_GM AS SiparisOnay_GM, 
			@OnayBekleyenStokAdet AS StokOnay, --Stok Tek Onaylı
			@BekleyenSozlesmeAdet_SM AS SozlesmeOnay_SM, 
			@BekleyenSozlesmeAdet_SPGMY AS SozlesmeOnay_SPGMY, 
			@BekleyenSozlesmeAdet_GM AS SozlesmeOnay_GM, 
			@BekleyenRiskLimitAdet_SM AS RiskLimitOnay_SM, 
			@BekleyenRiskLimitAdet_SPGMY AS RiskLimitOnay_SPGMY, 
			@BekleyenRiskLimitAdet_MIGMY AS RiskLimitOnay_MIGMY, 
			@BekleyenRiskLimitAdet_GM AS RiskLimitOnay_GM,  
			@OnayBekleyenTeminatAdet AS TeminatOnay, --Teminat Tek Onaylı
			@BekleyenFiyatAdet_SM AS FiyatOnay_SM,  
			@BekleyenFiyatAdet_SPGMY AS FiyatOnay_SPGMY, 
			@BekleyenFiyatAdet_GM AS FiyatOnay_GM,  
			@BekleyenCekAdet_SPGMY AS CekOnay_SPGMY, --Çeklerde SM yok
			@BekleyenCekAdet_MIGMY AS CekOnay_MIGMY, 
			@BekleyenCekAdet_GM AS CekOnay_GM,
			@KurumKartlari AS KurumKartlari,     --CRM Bilgileri
			@KurumKartlariBuHafta AS KurumKartlariBuHafta,
			@KurumKartlariBugun AS KurumKartlariBugun,
			@GorusmeNotlari AS GorusmeNotlari,
			@GorusmeNotlariBuHafta AS GorusmeNotlariBuHafta,
			@GorusmeNotlariBugun AS GorusmeNotlariBugun,
			@TeklifAnalizi AS TeklifAnalizi,
			@TeklifAnaliziBuHafta AS TeklifAnaliziBuHafta,
			@TeklifAnaliziBugun AS TeklifAnaliziBugun,
			@YeniIhaleAdet as YeniIhaleAdet,
			@YeniTahsisAdet as YeniTahsisAdet,
			@NakliyeFiyatAdet as NakliyeFiyatAdet,
			@SatinalmaSipTalepGMAdet as SatinalmaSipTalepGMAdet

END




GO
/****** Object:  StoredProcedure [wms].[DB_BekleyenSiparis_Musteri_Analizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









CREATE PROCEDURE [wms].[DB_BekleyenSiparis_Musteri_Analizi]
@Kod VARCHAR(10),
@Kriter VARCHAR(10)
AS
BEGIN
	IF (@Kod='TÜMÜ')
	BEGIN
		IF(@Kriter='TL')
		BEGIN
			SELECT * FROM
			(     
			SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar FROM					FINSAT633.FINSAT633.SPI(NOLOCK) 
 				WHERE KynkEvrakTip=62  AND SiparisDurumu=0 
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod
			ORDER BY KalanMiktar DESC
			) B
	
			UNION ALL

			SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
			(
 
			SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar FROM					FINSAT633.FINSAT633.SPI(NOLOCK) 
				WHERE KynkEvrakTip=62  AND SiparisDurumu=0 
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod

			) A
			WHERE RowID BETWEEN 26 AND 100000
		END
		ELSE IF(@Kriter='Döviz')
		BEGIN
			SELECT * FROM
			(     
			SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar/						DVZ.Kur00 as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
 				WHERE KynkEvrakTip=62  AND SiparisDurumu=0 
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod
			ORDER BY KalanMiktar DESC
			) B
	
			UNION ALL

			SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
			(
 
			SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar/						DVZ.Kur00 as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
				WHERE KynkEvrakTip=62  AND SiparisDurumu=0 
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod

			) A
			WHERE RowID BETWEEN 26 AND 100000
		END
		ELSE
		BEGIN
			SELECT * FROM
			(     
			SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, BirimMiktar					as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
 				WHERE KynkEvrakTip=62  AND SiparisDurumu=0 
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod
			ORDER BY KalanMiktar DESC
			) B
	
			UNION ALL

			SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
			(
 
			SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, BirimMiktar					as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
				WHERE KynkEvrakTip=62  AND SiparisDurumu=0 
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod

			) A
			WHERE RowID BETWEEN 26 AND 100000
		END
	END

	ELSE
	BEGIN
		IF(@Kriter='TL')
		BEGIN
			SELECT * FROM
			(     
			SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar FROM					FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON SPI.MalKodu=STK.MalKodu
 				WHERE KynkEvrakTip=62  AND SiparisDurumu=0  AND STK.GrupKod=@Kod
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod
			ORDER BY KalanMiktar DESC
			) B
	
			UNION ALL

			SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
			(
 
			SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar FROM					FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON SPI.MalKodu=STK.MalKodu
				WHERE KynkEvrakTip=62  AND SiparisDurumu=0  AND STK.GrupKod=@Kod
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod

			) A
			WHERE RowID BETWEEN 26 AND 100000
		END
		ELSE IF(@Kriter='Döviz')
		BEGIN
			SELECT * FROM
			(     
			SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar/						DVZ.Kur00 as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON SPI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
 				WHERE KynkEvrakTip=62  AND SiparisDurumu=0  AND STK.GrupKod=@Kod
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod
			ORDER BY KalanMiktar DESC
			) B
	
			UNION ALL

			SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
			(
 
			SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar/						DVZ.Kur00 as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON SPI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
				WHERE KynkEvrakTip=62  AND SiparisDurumu=0  AND STK.GrupKod=@Kod
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod

			) A
			WHERE RowID BETWEEN 26 AND 100000
		END
		ELSE
		BEGIN
			SELECT * FROM
			(     
			SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, BirimMiktar					as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON SPI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
 				WHERE KynkEvrakTip=62  AND SiparisDurumu=0  AND STK.GrupKod=@Kod
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod
			ORDER BY KalanMiktar DESC
			) B
	
			UNION ALL

			SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
			(
 
			SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
				(
				SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, BirimMiktar					as Tutar FROM	FINSAT633.FINSAT633.SPI(NOLOCK) 
				INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON SPI.MalKodu=STK.MalKodu
				INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
				WHERE KynkEvrakTip=62  AND SiparisDurumu=0 AND STK.GrupKod=@Kod
				) A 
			WHERE A.Tutar<>0
			GROUP BY A.GrupKod

			) A
			WHERE RowID BETWEEN 26 AND 100000
		END
	END



END


















GO
/****** Object:  StoredProcedure [wms].[DB_BekleyenSiparis_UrunGrubu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [wms].[DB_BekleyenSiparis_UrunGrubu]
@BasTarih INT,
@BitTarih INT
AS
BEGIN

SELECT SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Miktar-TeslimMiktar-KapatilanMiktar) AS KalanMiktar FROM
(
SELECT
(SELECT MalAdi4 FROM FINSAT633.FINSAT633.STK (NOLOCK) WHERE MalKodu= SPI.MalKodu)AS GrupKod, 
Miktar,TeslimMiktar,KapatilanMiktar FROM FINSAT633.FINSAT633.SPI (NOLOCK) WHERE KynkEvrakTip=62 AND SiparisDurumu=0 AND
 Tarih BETWEEN @BasTarih AND @BitTarih
) A 
WHERE A.Miktar-A.TeslimMiktar-A.KapatilanMiktar<>0
GROUP BY A.GrupKod

END
















GO
/****** Object:  StoredProcedure [wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat]
AS
BEGIN

	SELECT        '33' as DB, GrupKod, KalanMiktar
	FROM            (SELECT        TOP (15) SUBSTRING(GrupKod, 1, 20) AS GrupKod, ISNULL(SUM(Tutar), 0) AS KalanMiktar
							  FROM            (SELECT        STK.GrupKod, SPI.Tutar - SPI.ToplamIskonto AS Tutar
														FROM            FINSAT633.SPI AS SPI WITH (NOLOCK) INNER JOIN
																				  FINSAT633.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.Chk INNER JOIN
																				  FINSAT633.STK AS STK WITH (NOLOCK) ON STK.MalKodu = SPI.MalKodu
														WHERE        (SPI.KynkEvrakTip = 62) AND (SPI.SiparisDurumu = 0) AND (SPI.BirimMiktar - SPI.TeslimMiktar > 0) AND-- (STK.GrupKod IN ('PARKE', 'MDF', 'MDFLAM', 'SLAM', 'YLEVHA', 'SÜPÜRGELİK', 'ŞİLTE')) AND 
																				  (SPI.Kod10 IN ('Onaylandı', 'Sevk Edilebilir'))) AS A
							  WHERE        (Tutar <> 0)
							  GROUP BY GrupKod
							  ORDER BY KalanMiktar DESC) AS B

END

GO
/****** Object:  StoredProcedure [wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [wms].[DB_BekleyenSiparis_UrunGrubu_Fiyat_Kriter]
@Kriter VARCHAR(10)
AS
BEGIN

	IF(@Kriter='TL')
	BEGIN
	SELECT * FROM
(     
   SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
		(
		SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar FROM FINSAT633.FINSAT633.SPI(NOLOCK) WHERE KynkEvrakTip=62 AND Aciklama2<> 'SEVK EDİLDİ' AND SiparisDurumu=0 
	) A 
	WHERE A.Tutar<>0
	GROUP BY A.GrupKod
	ORDER BY KalanMiktar DESC
) B
	
UNION ALL

 SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
 (
 
	SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
		(
		SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar FROM FINSAT633.FINSAT633.SPI(NOLOCK) WHERE KynkEvrakTip=62 AND Aciklama2<> 'SEVK EDİLDİ' AND SiparisDurumu=0 
	) A 
	WHERE A.Tutar<>0
	GROUP BY A.GrupKod

 ) A
 WHERE RowID BETWEEN 26 AND 100000
	END

	ELSE IF(@Kriter='Döviz')
	BEGIN
	SELECT * FROM
(     
   SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
		(
		SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar/DVZ.Kur00 as Tutar FROM FINSAT633.FINSAT633.SPI(NOLOCK) 
		INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
		WHERE KynkEvrakTip=62 AND Aciklama2<> 'SEVK EDİLDİ' AND SiparisDurumu=0 
	) A 
	WHERE A.Tutar<>0
	GROUP BY A.GrupKod
	ORDER BY KalanMiktar DESC
) B
	
UNION ALL

 SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
 (
 
	SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
		(
		SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, Tutar/DVZ.Kur00 as Tutar FROM FINSAT633.FINSAT633.SPI(NOLOCK)
		INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON SPI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
		 WHERE KynkEvrakTip=62 AND Aciklama2<> 'SEVK EDİLDİ' AND SiparisDurumu=0 
	) A 
	WHERE A.Tutar<>0
	GROUP BY A.GrupKod

 ) A
 WHERE RowID BETWEEN 26 AND 100000
	END

	ELSE
	BEGIN
	SELECT * FROM
(     
   SELECT TOP 25 SUBSTRING(A.GrupKod,1,20) AS GrupKod, ISNULL(SUM(Tutar),0) AS KalanMiktar FROM
		(
		SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, BirimMiktar as Tutar FROM FINSAT633.FINSAT633.SPI(NOLOCK) WHERE KynkEvrakTip=62 AND Aciklama2<> 'SEVK EDİLDİ' AND SiparisDurumu=0 
	) A 
	WHERE A.Tutar<>0
	GROUP BY A.GrupKod
	ORDER BY KalanMiktar DESC
) B
	
UNION ALL

 SELECT 'Diger' as GrupKod, ISNULL(SUM(KalanMiktar),0) as KalanMiktar FROM
 (
 
	SELECT ROW_NUMBER() OVER(ORDER BY Sum(Tutar) DESC) as RowID,SUBSTRING(A.GrupKod,1,20) AS GrupKod, SUM(Tutar) AS KalanMiktar FROM
		(
		SELECT ISNULL( (SELECT Unvan1 FROM FINSAT633.FINSAT633.CHK (NOLOCK) WHERE HesapKodu= SPI.Kod1),'ATANMAMIŞ') AS GrupKod, BirimMiktar as Tutar FROM FINSAT633.FINSAT633.SPI(NOLOCK) WHERE KynkEvrakTip=62 AND Aciklama2<> 'SEVK EDİLDİ' AND SiparisDurumu=0 
	) A 
	WHERE A.Tutar<>0
	GROUP BY A.GrupKod

 ) A
 WHERE RowID BETWEEN 26 AND 100000
	END



END

















GO
/****** Object:  StoredProcedure [wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[DB_BekleyenSiparis_UrunGrubu_Miktar]
AS
BEGIN

	SELECT        '33' as DB, GrupKod, KalanMiktar
	FROM            (SELECT        TOP (15) SUBSTRING(GrupKod, 1, 20) AS GrupKod, ISNULL(SUM(Miktar), 0) AS KalanMiktar
							  FROM            (SELECT        STK.GrupKod, SPI.Miktar
														FROM            FINSAT633.SPI AS SPI WITH (NOLOCK) INNER JOIN
																				  FINSAT633.CHK AS CHK WITH (NOLOCK) ON CHK.HesapKodu = SPI.Chk INNER JOIN
																				  FINSAT633.STK AS STK WITH (NOLOCK) ON STK.MalKodu = SPI.MalKodu
														WHERE        (SPI.KynkEvrakTip = 62) AND (SPI.SiparisDurumu = 0) AND (SPI.BirimMiktar - SPI.TeslimMiktar > 0) AND --(STK.GrupKod IN ('PARKE', 'MDF', 'MDFLAM', 'SLAM', 'YLEVHA', 'SÜPÜRGELİK', 'ŞİLTE')) AND 
																				  (SPI.Kod10 IN ('Onaylandı', 'Sevk Edilebilir'))) AS A
							  WHERE        (Miktar <> 0)
							  GROUP BY GrupKod
							  ORDER BY KalanMiktar DESC) AS B
						  	
END

GO
/****** Object:  StoredProcedure [wms].[DB_Bolge_Bazinda_SatisAnalizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[DB_Bolge_Bazinda_SatisAnalizi]
@Ay INT,
@Kriter VARCHAR(20)
AS
BEGIN

    DECLARE @AyBasi2016 INT, @AySonu2016 INT, @AyAdi VARCHAR(20)
    IF(@Ay=0)
	Begin
	  Set @AyBasi2016=42735   Set @AySonu2016=43100
	  Set @AyAdi='TÜMÜ'
	End
	ELSE IF(@Ay=1)
	Begin
	  Set @AyBasi2016=42735   Set @AySonu2016=42765
	  Set @AyAdi='OCAK'
	End
	ELSE IF(@Ay=2)
	Begin
	  Set @AyBasi2016=42766   Set @AySonu2016=42794
	  Set @AyAdi='ŞUBAT'
	End
	ELSE IF(@Ay=3)
	Begin
	  Set @AyBasi2016=42795   Set @AySonu2016=42825
	  Set @AyAdi='MART'
	End
	ELSE IF(@Ay=4)
	Begin
	  Set @AyBasi2016=42826   Set @AySonu2016=42855
	  Set @AyAdi='NİSAN'
	End
	ELSE IF(@Ay=5)
	Begin
	  Set @AyBasi2016=42856   Set @AySonu2016=42886
	  Set @AyAdi='MAYIS'
	End
	ELSE IF(@Ay=6)
	Begin
	  Set @AyBasi2016=42887   Set @AySonu2016=42916
	  Set @AyAdi='HAZİRAN'
	End
	ELSE IF(@Ay=7)
	Begin
	  Set @AyBasi2016=42917   Set @AySonu2016=42947
	  Set @AyAdi='TEMMUZ'
	End
	ELSE IF(@Ay=8)
	Begin
	  Set @AyBasi2016=42948   Set @AySonu2016=42978
	  Set @AyAdi='AĞUSTOS'
	End
	ELSE IF(@Ay=9)
	Begin
	  Set @AyBasi2016=42979   Set @AySonu2016=43008
	  Set @AyAdi='EYLÜL'
	End
	ELSE IF(@Ay=10)
	Begin
	  Set @AyBasi2016=43009   Set @AySonu2016=43039
	  Set @AyAdi='EKİM'
	End
	ELSE IF(@Ay=11)
	Begin
	  Set @AyBasi2016=43040   Set @AySonu2016=43079
	  Set @AyAdi='KASIM'
	End
	ELSE IF(@Ay=12)
	Begin
	  Set @AyBasi2016=43080   Set @AySonu2016=43110
	  Set @AyAdi='ARALIK'
	End

    IF(@Kriter='Tümü')
	BEGIN
		SELECT @AyAdi AS Ay, CHK.Kod15 AS Lokasyon, 
		       ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu		
		WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @AyBasi2016 AND @AySonu2016
			  AND STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE')
			  AND CHK.Kod15 IN(SELECT DISTINCT Kod15 
			  FROM FINSAT633.FINSAT633.CHK(NOLOCK) WHERE Kod15<>'')
		GROUP BY CHK.Kod15
	END
	ELSE
	BEGIN
		SELECT @AyAdi AS Ay, CHK.Kod15 AS Lokasyon, 
		       ISNULL(SUM((STI.Tutar-STI.ToplamIskonto) + STI.KDV),0) AS GenelTutar
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu	
		WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @AyBasi2016 AND @AySonu2016
			  AND STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE')
			  AND CHK.Kod15=@Kriter
		GROUP BY CHK.Kod15
	END
	

END











GO
/****** Object:  StoredProcedure [wms].[DB_CariEkstre]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[DB_CariEkstre]
 @HesapKodu NVARCHAR(30) 
AS 
BEGIN 

Declare @TempTablo TABLE(
    ID int IDENTITY(1,1) PRIMARY KEY, --ID alanını sıralama bozulmasın diye ekledim
    EvrakNo varchar(20),              --Cursorde Order By kullanınca TempTablo read only oluyor
	Tarih varchar(15), 
	IslemTip varchar(30),   
	VadeTarih varchar(15),
	Borc Numeric(25,6),
	Alacak Numeric(25,6),
	BorcBakiye Numeric(25,6),
	AlacakBakiye Numeric(25,6),
	Borc2 varchar(30),
	Alacak2 varchar(30),
	BorcBakiye2 varchar(30),
	AlacakBakiye2 varchar(30)
)

DECLARE @ToplamBorc NUMERIC(25,6), @ToplamAlacak NUMERIC(25,6),
        @BorcBakiye NUMERIC(25,6), @AlacakBakiye NUMERIC(25,6),
		@Borc NUMERIC(25,6), @Alacak NUMERIC(25,6)


INSERT @TempTablo 
SELECT A.EvrakNo, A.Tarih, A.IslemTip, A.VadeTarih, A.Borc, A.Alacak, 0, 0,'','','',''
FROM
(
	SELECT 
		SUM((CASE WHEN KarsiHesapKodu LIKE @HesapKodu THEN BA*Tutar ELSE abs(BA - 1)*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc, 
		SUM((CASE WHEN KarsiHesapKodu LIKE @HesapKodu THEN abs(BA - 1)*Tutar ELSE BA*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak, 
		(CASE MIN(FINSAT633.CHI.IslemTip) WHEN 2 THEN 'Havale' WHEN 3 THEN 'Virman' WHEN 4 THEN 'Fatura' WHEN 7 THEN 'Dekont' WHEN 8 THEN 'İade Faturası' 
		 WHEN 17 THEN 'Çek' WHEN 20 THEN 'Çek İade' WHEN 28 THEN 'Borç Çeki' WHEN 40 THEN 'Kredi Kartı' WHEN 1 THEN 'Nakit' WHEN 18 THEN 'Karşılıksız Çek'ELSE 'Diğer' END) AS IslemTip, 
		EvrakNo, CONVERT(VARCHAR(15),CONVERT(DATETIME,MIN(CAST(Tarih - 2 AS datetime))),104) AS Tarih, CONVERT(VARCHAR(15),CONVERT(DATETIME,MIN(CAST(VadeTarih - 2 AS datetime))),104) AS VadeTarih 
	FROM FINSAT633.CHI(NOLOCK)
	WHERE  (HesapKodu like @HesapKodu AND  IslemTip NOT IN(16,20,21,60)) OR (KarsiHesapKodu like @HesapKodu AND IslemTip NOT IN(21,60))
	GROUP BY EvrakNo,tarih
 
	UNION ALL

	SELECT 
		SUM((CASE WHEN KarsiHesapKodu LIKE @HesapKodu THEN BA*Tutar ELSE abs(BA - 1)*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc, 
		SUM((CASE WHEN KarsiHesapKodu LIKE @HesapKodu THEN abs(BA - 1)*Tutar ELSE BA*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak, 
		(CASE MIN(FINSAT633.CHI.IslemTip) WHEN 2 THEN 'Havale' WHEN 3 THEN 'Virman' WHEN 4 THEN 'Fatura' WHEN 7 THEN 'Dekont' WHEN 8 THEN 'İade Faturası' 
		WHEN 17 THEN 'Çek' WHEN 20 THEN 'Çek İade' WHEN 28 THEN 'Borç Çeki' WHEN 40 THEN 'Kredi Kartı' WHEN 1 THEN 'Nakit' ELSE 'Diğer' END) AS IslemTip, 
		EvrakNo, CONVERT(VARCHAR(15),CONVERT(DATETIME,MIN(CAST(Tarih - 2 AS datetime))),104) AS Tarih, CONVERT(VARCHAR(15),CONVERT(DATETIME,MIN(CAST(VadeTarih - 2 AS datetime))),104) AS VadeTarih 
	FROM FINSAT633.CHI(NOLOCK) 
	WHERE (IslemTip = 20) and (HesapKodu like @HesapKodu) 
	GROUP BY EvrakNo

	UNION ALL

	SELECT 
		SUM((CASE WHEN KarsiHesapKodu LIKE @HesapKodu THEN abs(BA - 1)*Tutar ELSE BA*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Borc, 
		SUM((CASE WHEN KarsiHesapKodu LIKE @HesapKodu THEN BA*Tutar ELSE abs(BA - 1)*Tutar END)*(CASE IslemTip WHEN 5 THEN -1 WHEN 9 THEN -1 ELSE 1 END)) AS Alacak, 
		(CASE MIN(FINSAT633.CHI.IslemTip) WHEN 2 THEN 'Havale' WHEN 3 THEN 'Virman' WHEN 4 THEN 'Fatura' WHEN 7 THEN 'Dekont' WHEN 8 THEN 'İade Faturası' 
		WHEN 17 THEN 'Çek' WHEN 20 THEN 'Çek İade' WHEN 28 THEN 'Borç Çeki' WHEN 40 THEN 'Kredi Kartı' WHEN 1 THEN 'Nakit' ELSE 'Diğer' END) AS IslemTip, 
		EvrakNo, CONVERT(VARCHAR(15),CONVERT(DATETIME,MIN(CAST(Tarih - 2 AS datetime))),104) AS Tarih, CONVERT(VARCHAR(15),CONVERT(DATETIME,MIN(CAST(VadeTarih - 2 AS datetime))),104) AS VadeTarih 
	FROM FINSAT633.CHI(NOLOCK)
	WHERE (KarsiHesapKodu like @HesapKodu) and (IslemTip = 3) and (KarsiHesapKodu <> HesapKodu) and (left(HesapKodu,30)=left(KarsiHesapKodu,30)) and Islemtip not in ('60')
	GROUP BY EvrakNo
) A


ORDER BY A.Tarih, A.EvrakNo
SET @ToplamBorc=0
SET @ToplamAlacak=0

DECLARE CRS CURSOR FOR
SELECT Borc, Alacak FROM @TempTablo 

OPEN CRS
FETCH NEXT FROM CRS INTO @Borc, @Alacak

WHILE @@FETCH_STATUS=0
BEGIN

    SET @ToplamBorc = @ToplamBorc + @Borc
	SET @ToplamAlacak = @ToplamAlacak + @Alacak

	IF(@ToplamBorc>@ToplamAlacak)
	Begin 
	    SET @BorcBakiye=@ToplamBorc-@ToplamAlacak
		UPDATE @TempTablo
		SET BorcBakiye=@BorcBakiye
		, AlacakBakiye=0
		, Borc2= cast(Borc as numeric(36,2)) --=replace(replace(replace(convert(varchar,CAST(Borc as money),1),'.','x'),',','.'),'x',',')
		, Alacak2=cast(Alacak as numeric(36,2)) --(replace(replace(replace(convert(varchar,CAST(Alacak as money),1),'.','x'),',','.'),'x',',')
		, BorcBakiye2=cast(BorcBakiye as numeric(36,2))--replace(replace(replace(convert(varchar,CAST(BorcBakiye as money),1),'.','x'),',','.'),'x',',')
		, AlacakBakiye2=cast(AlacakBakiye as numeric(36,2))--replace(replace(replace(convert(varchar,CAST(AlacakBakiye as money),1),'.','x'),',','.'),'x',',')
		WHERE CURRENT OF CRS
	End
	ELSE
	Begin
		SET @AlacakBakiye=@ToplamAlacak-@ToplamBorc
		UPDATE @TempTablo
		SET BorcBakiye=0
		, AlacakBakiye=@AlacakBakiye
		, Borc2= cast(Borc as numeric(36,2))--Borc2=replace(replace(replace(convert(varchar,CAST(Borc as money),1),'.','x'),',','.'),'x',',')
		, Alacak2=cast(Alacak as numeric(36,2)) --Alacak2=replace(replace(replace(convert(varchar,CAST(Alacak as money),1),'.','x'),',','.'),'x',',')
		, BorcBakiye2=cast(BorcBakiye as numeric(36,2))--BorcBakiye2=replace(replace(replace(convert(varchar,CAST(BorcBakiye as money),1),'.','x'),',','.'),'x',',')
		, AlacakBakiye2=cast(AlacakBakiye as numeric(36,2))--AlacakBakiye2=replace(replace(replace(convert(varchar,CAST(AlacakBakiye as money),1),'.','x'),',','.'),'x',',')
		WHERE CURRENT OF CRS
	End

	FETCH NEXT FROM CRS INTO @Borc, @Alacak
 
END
CLOSE CRS
DEALLOCATE CRS

SELECT * FROM @TempTablo
ORDER BY Tarih, EvrakNo

END

















GO
/****** Object:  StoredProcedure [wms].[DB_GetKod]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [wms].[DB_GetKod]
AS
BEGIN
--IF (@Tip=1)
--BEGIN
SELECT * FROM (
SELECT DISTINCT GrupKod as Kod, '1' as Tip FROM FINSAT633.FINSAT633.STK(NOLOCK)
UNION ALL
SELECT DISTINCT TipKod as Kod, '2' as Tip FROM FINSAT633.FINSAT633.STK(NOLOCK)
UNION ALL
SELECT DISTINCT OzelKod as Kod, '3' as Tip FROM FINSAT633.FINSAT633.STK(NOLOCK)
UNION ALL
SELECT DISTINCT Kod1 as Kod, '4' as Tip FROM FINSAT633.FINSAT633.STK(NOLOCK)
UNION ALL
SELECT DISTINCT Kod2 as Kod, '5' as Tip FROM FINSAT633.FINSAT633.STK(NOLOCK)
UNION ALL
SELECT DISTINCT Kod3 as Kod, '6' as Tip FROM FINSAT633.FINSAT633.STK(NOLOCK)
)  AS A
WHERE Kod<>''
GROUP BY Kod,Tip
ORDER BY Kod
--END

END





GO
/****** Object:  StoredProcedure [wms].[DB_GetTipKod]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Last Modify: 25.12.2017
-- Description:	tipkod getir
-- =============================================
CREATE PROCEDURE [wms].[DB_GetTipKod]
AS
BEGIN

	SELECT DISTINCT TipKod AS Kod, '2' AS Tip
	FROM            FINSAT633.CHK WITH (NOLOCK)
	WHERE        (TipKod <> '')
	GROUP BY TipKod

END
GO
/****** Object:  StoredProcedure [wms].[DB_GunlukSatisAnalizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [wms].[DB_GunlukSatisAnalizi]
@Tarih INT
AS
BEGIN


SELECT * FROM
(     
   SELECT TOP 25  STI.Chk, SUBSTRING(CHK.Unvan1+' '+CHK.Unvan2,1,20) AS Unvan, 
	        Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih=@Tarih 
	GROUP BY STI.Chk, CHK.Unvan1+' '+CHK.Unvan2
	ORDER BY GenelTutar DESC, Unvan
) B
	
UNION ALL

 SELECT 'Diger' as Chk, 'Diger' as Unvan, SUM(GenelTutar) as GenelTutar FROM
 (
    SELECT ROW_NUMBER() OVER(ORDER BY  Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV) DESC, SUBSTRING(CHK.Unvan1+' '+CHK.Unvan2,1,20)) as RowID,  
	       STI.Chk, SUBSTRING(CHK.Unvan1+' '+CHK.Unvan2,1,20) AS Unvan, 
	        Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip=1 AND STI.Tarih=@Tarih 
	GROUP BY STI.Chk, CHK.Unvan1+' '+CHK.Unvan2

 ) A
 WHERE RowID BETWEEN 26 AND 100000


	
 

END















GO
/****** Object:  StoredProcedure [wms].[DB_GunlukSatisAnaliziDoubleKriter]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[DB_GunlukSatisAnaliziDoubleKriter]
	@Tarih INT,
	@IslemTip SMALLINT,
	@Grup VARCHAR(10)
AS
BEGIN

	SELECT * FROM
	(     
		SELECT        TOP (10) STI.Chk, SUBSTRING(CHK.Unvan1 + ' ' + CHK.Unvan2, 1, 20) AS Unvan, SUM(STI.Tutar - STI.ToplamIskonto + STI.KDV) AS GenelTutar
		FROM            FINSAT633.STI AS STI WITH (NOLOCK) INNER JOIN
								 FINSAT633.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
								 FINSAT633.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu
		WHERE        (STI.KynkEvrakTip = 1) AND (STI.Tarih = @Tarih) AND 
								(STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) AND 
								(STI.IslemTip = (CASE WHEN @IslemTip = 0 THEN STI.IslemTip ELSE @IslemTip END))
		GROUP BY STI.Chk, CHK.Unvan1 + ' ' + CHK.Unvan2
		ORDER BY GenelTutar DESC, Unvan
	) B
	
	UNION ALL

	SELECT 'Diğer' as Chk, 'Diğer' as Unvan, SUM(GenelTutar) as GenelTutar FROM
	(
		SELECT        ROW_NUMBER() OVER(ORDER BY  Sum((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV) DESC, SUBSTRING(CHK.Unvan1+' '+CHK.Unvan2,1,20)) as RowID, 
					STI.Chk, SUBSTRING(CHK.Unvan1 + ' ' + CHK.Unvan2, 1, 20) AS Unvan, SUM(STI.Tutar - STI.ToplamIskonto + STI.KDV) AS GenelTutar
		FROM            FINSAT633.STI AS STI WITH (NOLOCK) INNER JOIN
								 FINSAT633.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
								 FINSAT633.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu
		WHERE        (STI.KynkEvrakTip = 1) AND (STI.Tarih = @Tarih) AND 
								(STK.MalAdi4= (CASE WHEN @Grup = 'TÜMÜ' THEN STK.MalAdi4 ELSE @Grup END)) AND 
								(STI.IslemTip = (CASE WHEN @IslemTip = 0 THEN STI.IslemTip ELSE @IslemTip END))
		GROUP BY STI.Chk, CHK.Unvan1 + ' ' + CHK.Unvan2
	) A
	WHERE RowID BETWEEN 11 AND 100000

END



GO
/****** Object:  StoredProcedure [wms].[DB_GunlukSatisAnaliziYearToDay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[DB_GunlukSatisAnaliziYearToDay]

AS
BEGIN

	declare @yilbasi int, @bugun int;
	set @yilbasi=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT);
	set @bugun=CAST(GETDATE()+2 AS INT);

	SELECT * FROM
	(     
		SELECT        TOP (15) '33' AS DB, STI.Chk, SUBSTRING(CHK.Unvan1 + ' ' + CHK.Unvan2, 1, 20) AS Unvan, SUM(CASE WHEN STI.KynkEvrakTip IN (1, 125, 64, 163) THEN (STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) 
								 + STI.KDV ELSE ((STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) + STI.KDV) * - 1 END) AS GenelTutar
		FROM            FINSAT633.STI AS STI WITH (NOLOCK) INNER JOIN
								 FINSAT633.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
								 FINSAT633.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu
		WHERE        (STI.KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163)) AND (STI.Tarih BETWEEN @yilbasi AND @bugun)
		GROUP BY STI.Chk, CHK.Unvan1 + ' ' + CHK.Unvan2
		ORDER BY GenelTutar DESC, Unvan
	) B
	
	UNION ALL

	SELECT '33' as DB, 'Diğer' as Chk, 'Diğer' as Unvan, ISNULL(SUM(GenelTutar),0) as GenelTutar FROM
	(
		SELECT ROW_NUMBER() OVER(ORDER BY Sum((STI.Tutar-STI.ToplamIskonto) + STI.KDV) DESC, SUBSTRING(CHK.Unvan1+' '+CHK.Unvan2,1,20)) as RowID,  
			STI.Chk, SUBSTRING(CHK.Unvan1+' '+CHK.Unvan2,1,20) AS Unvan, 
			Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END) AS GenelTutar
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
		WHERE  STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.Tarih BETWEEN @yilbasi AND @bugun
		GROUP BY STI.Chk, CHK.Unvan1+' '+CHK.Unvan2
	) A
	WHERE RowID BETWEEN 16 AND 100000


END


GO
/****** Object:  StoredProcedure [wms].[DB_GunlukSatisGetir]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 03.08.2017
-- Modify date: 03.08.2017
-- Description:	günlük satışı getirir
-- =============================================
CREATE PROCEDURE [wms].[DB_GunlukSatisGetir]
AS
BEGIN 

	SELECT       CAST(Tarih - 2 AS datetime) AS Tarih, SUM(CASE WHEN STI.KynkEvrakTip IN (1, 125, 64, 163) THEN (STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) 
							 + STI.KDV ELSE ((STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) + STI.KDV) * - 1 END) AS KalanTutar
	FROM            FINSAT633.FINSAT633.STI AS STI WITH (NOLOCK)
	WHERE        (KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163))
	GROUP BY CONVERT(VARCHAR(15), CAST(Tarih - 2 AS datetime), 104), STI.Tarih
	ORDER BY STI.Tarih

END



GO
/****** Object:  StoredProcedure [wms].[DB_LokasyonBazli_SatisAnalizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 01.01.2017
-- Modify date: 03.08.2017
-- Description:	Lokasyon Bazli Satis Analizi
-- =============================================
CREATE PROCEDURE [wms].[DB_LokasyonBazli_SatisAnalizi]
	@Ay SMALLINT

AS
BEGIN

    DECLARE @BasTarih2017 INT, @BitTarih2017 INT,@BasTarih2016 INT, @BitTarih2016 INT
	IF @Ay=0
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End
	ELSE IF @Ay=1
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-31')+2 AS INT)
	End
	ELSE IF @Ay=2
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-29')+2 AS INT)
	End
	ELSE IF @Ay=3
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-31')+2 AS INT)
	End
	ELSE IF @Ay=4
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-30')+2 AS INT)
	End
	ELSE IF @Ay=5
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-31')+2 AS INT)
	End
	ELSE IF @Ay=6
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-30')+2 AS INT)
	End
	ELSE IF @Ay=7
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-31')+2 AS INT)
	End
	ELSE IF @Ay=8
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-31')+2 AS INT)
	End
	ELSE IF @Ay=9
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-30')+2 AS INT)
	End
	ELSE IF @Ay=10
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-31')+2 AS INT)
	End
	ELSE IF @Ay=11
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-30')+2 AS INT)
	End
	ELSE IF @Ay=12
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End
	
	SELECT * FROM (

		SELECT TOP (15) '33' as DB, @Ay as Ay, A.GrupKod,A.Yil2016,A.Yil2017, A.Yil2016+A.Yil2017 As Toplam FROM ( 
			SELECT  ISNULL((SELECT Unvan1 FROM FINSAT616.FINSAT616.CHK(NOLOCK) WHERE HesapKodu=STII.TeslimChk)+' ('+STII.TeslimChk+')','') AS GrupKod,
					cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
									STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
									),0) AS GenelTutar
							FROM FINSAT616.FINSAT616.STI(NOLOCK) STI 
							INNER JOIN FINSAT616.FINSAT616.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
							INNER JOIN FINSAT616.FINSAT616.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
							WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
							GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2016 , 
					cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
									STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
									),0) AS GenelTutar
							FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
							INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
							INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
							WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
							GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2017
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
			WHERE STII.KynkEvrakTip=1
			GROUP BY STII.TeslimChk 
		) A
		WHERE A.Yil2016<>0 OR A.Yil2017<>0
		ORDER BY Toplam DESC
	) B

	UNION ALL

	SELECT '33' as DB, @Ay as Ay, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
	(
		SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM 
		( 
			SELECT  ISNULL((SELECT Unvan1 FROM FINSAT616.FINSAT616.CHK(NOLOCK) WHERE HesapKodu=STII.TeslimChk)+' ('+STII.TeslimChk+')','') AS GrupKod,
					cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
									STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
									),0) AS GenelTutar
							FROM FINSAT616.FINSAT616.STI(NOLOCK) STI 
							INNER JOIN FINSAT616.FINSAT616.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
							INNER JOIN FINSAT616.FINSAT616.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
							WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
							GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2016 , 
					cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
									STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
									),0) AS GenelTutar
					FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
					INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
					INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
					WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
					GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2017
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
			WHERE STII.KynkEvrakTip=1
			GROUP BY STII.TeslimChk 
		) A
		WHERE A.Yil2016<>0 OR A.Yil2017<>0
	 ) A
	 WHERE RowID BETWEEN 16 AND 100000

END
GO
/****** Object:  StoredProcedure [wms].[DB_LokasyonBazli_SatisAnalizi_Kriter]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 01.01.2017
-- Modify date: 03.08.2017
-- Description:	Lokasyon Bazli Satis Analizi Kriter
-- =============================================
CREATE PROCEDURE [wms].[DB_LokasyonBazli_SatisAnalizi_Kriter]
	@Ay SMALLINT,
	@Kriter VARCHAR(10)

AS
BEGIN

    DECLARE @BasTarih2017 INT, @BitTarih2017 INT,@BasTarih2016 INT, @BitTarih2016 INT
	IF @Ay=0
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End
	ELSE IF @Ay=1
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-31')+2 AS INT)
	End
	ELSE IF @Ay=2
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-29')+2 AS INT)
	End
	ELSE IF @Ay=3
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-31')+2 AS INT)
	End
	ELSE IF @Ay=4
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-30')+2 AS INT)
	End
	ELSE IF @Ay=5
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-31')+2 AS INT)
	End
	ELSE IF @Ay=6
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-30')+2 AS INT)
	End
	ELSE IF @Ay=7
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-31')+2 AS INT)
	End
	ELSE IF @Ay=8
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-31')+2 AS INT)
	End
	ELSE IF @Ay=9
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-30')+2 AS INT)
	End
	ELSE IF @Ay=10
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-31')+2 AS INT)
	End
	ELSE IF @Ay=11
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-30')+2 AS INT)
	End
	ELSE IF @Ay=12
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End

 	IF(@Kriter='TL')
	BEGIN
		SELECT * FROM (
			SELECT        TOP (15) '33' as DB, @Ay as Ay, @Kriter as Kriter, GrupKod, Yil2016, Yil2017, Yil2016 + Yil2017 AS Toplam
			FROM            (SELECT        ISNULL((SELECT        Unvan1
											FROM            FINSAT616.FINSAT616.CHK WITH (NOLOCK)
											WHERE        (HesapKodu = STII.TeslimChk)) + ' (' + STII.TeslimChk + ')', '') AS GrupKod, 
											cast(ISNULL((SELECT        ISNULL(SUM(CASE WHEN STI.KynkEvrakTip IN (1, 125, 64, 163) THEN (STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) + STI.KDV ELSE ((STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) + STI.KDV) 
																		* - 1 END), 0) AS GenelTutar
											FROM            FINSAT616.FINSAT616.STI AS STI WITH (NOLOCK) INNER JOIN
																		FINSAT616.FINSAT616.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
																		FINSAT616.FINSAT616.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu
											WHERE        (STI.KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163)) AND (STI.TeslimChk = STII.TeslimChk) AND (STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016)
											GROUP BY STI.TeslimChk), 0) as numeric(36,2)) AS Yil2016, 
											cast(ISNULL((SELECT        ISNULL(SUM(CASE WHEN STI.KynkEvrakTip IN (1, 125, 64, 163) THEN (STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) + STI.KDV ELSE ((STI.Tutar - STI.ToplamIskonto - STI.TevfikatTutar) + STI.KDV) 
																		* - 1 END), 0) AS GenelTutar
											FROM            FINSAT633.STI AS STI WITH (NOLOCK) INNER JOIN
																		FINSAT633.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
																		FINSAT633.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu
											WHERE        (STI.KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163)) AND (STI.TeslimChk = STII.TeslimChk) AND (STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017)
											GROUP BY STI.TeslimChk), 0) as numeric(36,2)) AS Yil2017
							FROM            FINSAT633.STI AS STII WITH (NOLOCK) INNER JOIN
													FINSAT633.CHK AS CHKK WITH (NOLOCK) ON STII.Chk = CHKK.HesapKodu INNER JOIN
													FINSAT633.STK AS STKK WITH (NOLOCK) ON STII.MalKodu = STKK.MalKodu
							WHERE        (STII.KynkEvrakTip = 1)
							GROUP BY STII.TeslimChk) AS A
			WHERE        (Yil2016 <> 0) OR
									 (Yil2017 <> 0)
			ORDER BY Toplam DESC
		) B
		UNION ALL
		SELECT '33' as DB, @Ay as Ay, @Kriter as Kriter, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
		(
			SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM ( 
			SELECT (
			ISNULL((SELECT Unvan1 FROM FINSAT616.FINSAT616.CHK(NOLOCK) WHERE HesapKodu=STII.TeslimChk)+' ('+STII.TeslimChk+')','')) AS GrupKod,
			cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
			STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
			),0) AS GenelTutar
			FROM FINSAT616.FINSAT616.STI(NOLOCK) STI 
			INNER JOIN FINSAT616.FINSAT616.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
			INNER JOIN FINSAT616.FINSAT616.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
			WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
			GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2016 , 
			cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
			STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
			),0) AS GenelTutar
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
			WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
			GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2017
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
			WHERE STII.KynkEvrakTip=1
			GROUP BY STII.TeslimChk ) A
			WHERE A.Yil2016<>0 OR A.Yil2017<>0
		) A
		WHERE RowID BETWEEN 16 AND 100000
	END
	ELSE IF(@Kriter='Döviz')
	BEGIN
		SELECT * FROM (
			SELECT        TOP (15) '33' as DB, @Ay as Ay, @Kriter as Kriter, GrupKod, Yil2016, Yil2017, Yil2016 + Yil2017 AS Toplam
			FROM            (SELECT        ISNULL((SELECT        Unvan1
										FROM            FINSAT616.FINSAT616.CHK WITH (NOLOCK)
										WHERE        (HesapKodu = STII.TeslimChk)) + ' (' + STII.TeslimChk + ')', '') AS GrupKod, 
										cast(ISNULL((SELECT        SUM((STI.Tutar - STI.ToplamIskonto + STI.KDV) / DVZ.Kur00) AS GenelTutar
										FROM            FINSAT616.FINSAT616.STI AS STI WITH (NOLOCK) INNER JOIN
																	FINSAT616.FINSAT616.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
																	FINSAT616.FINSAT616.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu INNER JOIN
																	solar6.dbo.DVZ AS DVZ WITH (NOLOCK) ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi = 'EUR'
										WHERE        (STI.KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163)) AND (STI.TeslimChk = STII.TeslimChk) AND (STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016)
										GROUP BY STI.TeslimChk), 0) as numeric(36,2)) AS Yil2016, 
										cast(ISNULL((SELECT        SUM((STI.Tutar - STI.ToplamIskonto + STI.KDV) / DVZ.Kur00) AS GenelTutar
										FROM            FINSAT633.STI AS STI WITH (NOLOCK) INNER JOIN
																	FINSAT633.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
																	FINSAT633.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu INNER JOIN
																	solar6.dbo.DVZ AS DVZ WITH (NOLOCK) ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi = 'EUR'
										WHERE        (STI.KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163)) AND (STI.TeslimChk = STII.TeslimChk) AND (STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017)
										GROUP BY STI.TeslimChk), 0) as numeric(36,2)) AS Yil2017
								FROM            FINSAT633.STI AS STII WITH (NOLOCK) INNER JOIN
														FINSAT633.CHK AS CHKK WITH (NOLOCK) ON STII.Chk = CHKK.HesapKodu INNER JOIN
														FINSAT633.STK AS STKK WITH (NOLOCK) ON STII.MalKodu = STKK.MalKodu
								WHERE        (STII.KynkEvrakTip = 1)
								GROUP BY STII.TeslimChk) AS A
			WHERE        (Yil2016 <> 0) OR
									 (Yil2017 <> 0)
			ORDER BY Toplam DESC
		) B
		UNION ALL
		SELECT '33' as DB, @Ay as Ay, @Kriter as Kriter, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
		(
			SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM ( 
			SELECT (
			ISNULL((SELECT Unvan1 FROM FINSAT616.FINSAT616.CHK(NOLOCK) WHERE HesapKodu=STII.TeslimChk)+' ('+STII.TeslimChk+')','')) AS GrupKod,
			cast(ISNULL((SELECT SUM(((STI.Tutar-STI.ToplamIskonto) + STI.KDV)/DVZ.Kur00) AS GenelTutar
			FROM FINSAT616.FINSAT616.STI(NOLOCK) STI 
			INNER JOIN FINSAT616.FINSAT616.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
			INNER JOIN FINSAT616.FINSAT616.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
			INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
			WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
			GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2016 , 
			cast(ISNULL((SELECT SUM(((STI.Tutar-STI.ToplamIskonto) + STI.KDV)/DVZ.Kur00) AS GenelTutar
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
			INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
			WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
			GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2017
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
			WHERE STII.KynkEvrakTip=1
			GROUP BY STII.TeslimChk ) A
			WHERE A.Yil2016<>0 OR A.Yil2017<>0
		) A
		WHERE RowID BETWEEN 16 AND 100000
	END
	ELSE
	BEGIN
		SELECT * FROM (
			SELECT        TOP (15) '33' as DB, @Ay as Ay, @Kriter as Kriter, GrupKod, Yil2016, Yil2017, Yil2016 + Yil2017 AS Toplam
			FROM            (SELECT        ISNULL((SELECT        Unvan1
									FROM            FINSAT616.FINSAT616.CHK WITH (NOLOCK)
									WHERE        (HesapKodu = STII.TeslimChk)) + ' (' + STII.TeslimChk + ')', '') AS GrupKod, 
									cast(ISNULL((SELECT        SUM(STI.BirimMiktar) AS GenelTutar
									FROM            FINSAT616.FINSAT616.STI AS STI WITH (NOLOCK) INNER JOIN
																FINSAT616.FINSAT616.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
																FINSAT616.FINSAT616.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu
									WHERE        (STI.KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163)) AND (STI.TeslimChk = STII.TeslimChk) AND (STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016)
									GROUP BY STI.TeslimChk), 0) as numeric(36,2)) AS Yil2016, 
									cast(ISNULL((SELECT        SUM(STI.BirimMiktar) AS GenelTutar
									FROM            FINSAT633.STI AS STI WITH (NOLOCK) INNER JOIN
																FINSAT633.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
																FINSAT633.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu
									WHERE        (STI.KynkEvrakTip IN (1, 2, 125, 126, 64, 117, 163)) AND (STI.TeslimChk = STII.TeslimChk) AND (STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017)
									GROUP BY STI.TeslimChk), 0) as numeric(36,2)) AS Yil2017
							FROM            FINSAT633.STI AS STII WITH (NOLOCK) INNER JOIN
													FINSAT633.CHK AS CHKK WITH (NOLOCK) ON STII.Chk = CHKK.HesapKodu INNER JOIN
													FINSAT633.STK AS STKK WITH (NOLOCK) ON STII.MalKodu = STKK.MalKodu
							WHERE        (STII.KynkEvrakTip = 1)
							GROUP BY STII.TeslimChk) AS A
			WHERE        (Yil2016 <> 0) OR
									 (Yil2017 <> 0)
			ORDER BY Toplam DESC
		) B
		UNION ALL
		SELECT '33' as DB, @Ay as Ay, @Kriter as Kriter, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
		(
			SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM ( 
			SELECT (
			ISNULL((SELECT Unvan1 FROM FINSAT616.FINSAT616.CHK(NOLOCK) WHERE HesapKodu=STII.TeslimChk)+' ('+STII.TeslimChk+')','')) AS GrupKod,
			cast(ISNULL((SELECT SUM(STI.BirimMiktar) AS GenelTutar
			FROM FINSAT616.FINSAT616.STI(NOLOCK) STI 
			INNER JOIN FINSAT616.FINSAT616.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
			INNER JOIN FINSAT616.FINSAT616.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
			WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
			GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2016 , 
			cast(ISNULL((SELECT SUM(STI.BirimMiktar) AS GenelTutar
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
			WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STI.TeslimChk=STII.TeslimChk AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
			GROUP BY STI.TeslimChk),0) as numeric(36,2)) as Yil2017
			FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
			INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
			INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
			WHERE STII.KynkEvrakTip=1
			GROUP BY STII.TeslimChk ) A
			WHERE A.Yil2016<>0 OR A.Yil2017<>0
		) A
		WHERE RowID BETWEEN 16 AND 100000
	END

END
GO
/****** Object:  StoredProcedure [wms].[DB_SatisBaglanti_UrunGrubu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[DB_SatisBaglanti_UrunGrubu]
AS
BEGIN

		SELECT        '33' as DB, '' as MalKod, cast(0 as decimal) AS KalanTutar

END


GO
/****** Object:  StoredProcedure [wms].[DB_UrunGrubu_SatisAnalizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 01.01.2017
-- Modify date: 03.08.2017
-- Description:	Urun Grubu_Satis Analizi
-- =============================================
CREATE PROCEDURE [wms].[DB_UrunGrubu_SatisAnalizi]
	 @Ay int

AS
BEGIN

    DECLARE @BasTarih2017 INT, @BitTarih2017 INT,@BasTarih2016 INT, @BitTarih2016 INT
	--SET @Ay= 1
	IF @Ay=0
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End
	ELSE IF @Ay=1
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-31')+2 AS INT)
	End
	ELSE IF @Ay=2
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-28')+2 AS INT)
	End
	ELSE IF @Ay=3
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-31')+2 AS INT)
	End
	ELSE IF @Ay=4
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-30')+2 AS INT)
	End
	ELSE IF @Ay=5
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-31')+2 AS INT)
	End
	ELSE IF @Ay=6
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-30')+2 AS INT)
	End
	ELSE IF @Ay=7
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-31')+2 AS INT)
	End
	ELSE IF @Ay=8
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-31')+2 AS INT)
	End
	ELSE IF @Ay=9
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-30')+2 AS INT)
	End
	ELSE IF @Ay=10
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-31')+2 AS INT)
	End
	ELSE IF @Ay=11
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-30')+2 AS INT)
	End
	ELSE IF @Ay=12
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End
	
	SELECT * FROM (
		SELECT TOP (15) '33' as DB, @Ay as Ay, A.GrupKod,A.Yil2016,A.Yil2017, A.Yil2016+A.Yil2017 As Toplam FROM ( 
		SELECT (CASE WHEN STKK.MalAdi4='' THEN 'BOŞ' ELSE STKK.MalAdi4 END) as GrupKod,
				cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
								STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
								),0) AS GenelTutar
						FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
						INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
						INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
						WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
						GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2016 , 
				cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
								STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
								),0) AS GenelTutar
						FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
						INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
						INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
						WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
						GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2017
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
		WHERE STII.KynkEvrakTip in (1,163)
		GROUP BY STKK.MalAdi4 ) A
		WHERE A.Yil2016<>0 OR A.Yil2017<>0
		ORDER BY Toplam DESC
	) B
	UNION ALL
	SELECT '33' as DB, @Ay as Ay, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
	(
		SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM ( 
		SELECT (CASE WHEN STKK.GrupKod='' THEN 'BOŞ' ELSE STKK.GrupKod END) as GrupKod,
				cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
								STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
								),0) AS GenelTutar
						FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
						INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
						INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
						WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.GrupKod=STKK.GrupKod AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
						GROUP BY STK.GrupKod),0) as numeric(36,2)) as Yil2016 , 
				cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
								STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
								),0) AS GenelTutar
						FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
						INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
						INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
						WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.GrupKod=STKK.GrupKod AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
						GROUP BY STK.GrupKod),0) as numeric(36,2)) as Yil2017
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
		WHERE STII.KynkEvrakTip=1
		GROUP BY STKK.GrupKod ) A
		WHERE A.Yil2016<>0 OR A.Yil2017<>0
	) A
	WHERE RowID BETWEEN 16 AND 100000

END
GO
/****** Object:  StoredProcedure [wms].[DB_UrunGrubu_SatisAnalizi_Kriter]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 01.01.2017
-- Modify date: 03.08.2017
-- Description:	Urun Grubu_Satis Analizi_Kriter
-- =============================================
CREATE PROCEDURE [wms].[DB_UrunGrubu_SatisAnalizi_Kriter]
	@Ay INT,
	@Kriter VARCHAR(10)

AS
BEGIN

    DECLARE @BasTarih2017 INT, @BitTarih2017 INT,@BasTarih2016 INT, @BitTarih2016 INT
	IF @Ay=0
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End
	ELSE IF @Ay=1
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-01-31')+2 AS INT)
	End
	ELSE IF @Ay=2
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-02-29')+2 AS INT)
	End
	ELSE IF @Ay=3
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-03-31')+2 AS INT)
	End
	ELSE IF @Ay=4
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-04-30')+2 AS INT)
	End
	ELSE IF @Ay=5
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-05-31')+2 AS INT)
	End
	ELSE IF @Ay=6
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-06-30')+2 AS INT)
	End
	ELSE IF @Ay=7
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-07-31')+2 AS INT)
	End
	ELSE IF @Ay=8
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-08-31')+2 AS INT)
	End
	ELSE IF @Ay=9
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-09-30')+2 AS INT)
	End
	ELSE IF @Ay=10
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-10-31')+2 AS INT)
	End
	ELSE IF @Ay=11
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-11-30')+2 AS INT)
	End
	ELSE IF @Ay=12
	Begin
	   Set @BasTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)
	   Set @BitTarih2017=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	   Set @BasTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-01')+2 AS INT)
	   Set @BitTarih2016=CAST(CONVERT(SMALLDATETIME, '2016-12-31')+2 AS INT)
	End
	

	IF(@Kriter='TL')
	BEGIN
	SELECT * FROM (

		SELECT TOP (15) '33' as DB, @Ay as Ay, @Kriter as Kriter, A.GrupKod,A.Yil2016,A.Yil2017, A.Yil2016+A.Yil2017 As Toplam FROM ( 
		SELECT (
		CASE WHEN STKK.MalAdi4='' THEN 'BOŞ' ELSE STKK.MalAdi4 END) as GrupKod,
		cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
				STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
				),0) AS GenelTutar
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
		WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
		GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2016 , 
		cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
			STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
			),0) AS GenelTutar
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
		WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
		GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2017
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
	WHERE STII.KynkEvrakTip in (1,163)
	GROUP BY STKK.MalAdi4 ) A
	WHERE A.Yil2016<>0 OR A.Yil2017<>0
	ORDER BY Toplam DESC
	) B

UNION ALL

 SELECT '33' as DB, @Ay as Ay, @Kriter as Kriter, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
 (
    SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM ( 
		SELECT (
		CASE WHEN STKK.MalAdi4='' THEN 'BOŞ' ELSE STKK.MalAdi4 END) as GrupKod,
		cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
				STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
				),0) AS GenelTutar
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
		WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
		GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2016 , 
		cast(ISNULL((SELECT ISNULL(Sum( CASE WHEN STI.KynkEvrakTip in (1,125,64,163) THEN (STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + 
				STI.KDV ELSE ((STI.Tutar-STI.ToplamIskonto-STI.TevfikatTutar) + STI.KDV)*-1 END
				),0) AS GenelTutar
		FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
		INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
		INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
		WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
		GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2017
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
	WHERE STII.KynkEvrakTip=1
	GROUP BY STKK.MalAdi4 ) A
	WHERE A.Yil2016<>0 OR A.Yil2017<>0
 ) A
 WHERE RowID BETWEEN 16 AND 100000
	END

	ELSE IF(@Kriter='Döviz')
	BEGIN
	SELECT * FROM (

	SELECT TOP (15) '33' as DB, @Ay as Ay, @Kriter as Kriter, A.GrupKod,A.Yil2016,A.Yil2017, A.Yil2016+A.Yil2017 As Toplam FROM ( 
	SELECT (
	CASE WHEN STKK.MalAdi4='' THEN 'BOŞ' ELSE STKK.MalAdi4 END) as GrupKod,
	cast(ISNULL((SELECT SUM(((STI.Tutar-STI.ToplamIskonto) + STI.KDV)/DVZ.Kur00) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
	WHERE STI.KynkEvrakTip=1 AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2016 , 
	cast(ISNULL((SELECT SUM(((STI.Tutar-STI.ToplamIskonto) + STI.KDV)/DVZ.Kur00) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
	WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2017
FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
WHERE STII.KynkEvrakTip=1
GROUP BY STKK.MalAdi4 ) A
WHERE A.Yil2016<>0 OR A.Yil2017<>0
ORDER BY Toplam DESC
) B

UNION ALL

 SELECT '33' as DB, @Ay as Ay, @Kriter as Kriter, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
 (
    SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM ( 
	SELECT (
	CASE WHEN STKK.MalAdi4='' THEN 'BOŞ' ELSE STKK.MalAdi4 END) as GrupKod,
	cast(ISNULL((SELECT SUM(((STI.Tutar-STI.ToplamIskonto) + STI.KDV)/DVZ.Kur00) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
	WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2016 , 
	cast(ISNULL((SELECT SUM(((STI.Tutar-STI.ToplamIskonto) + STI.KDV)/DVZ.Kur00) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	INNER JOIN SOLAR6.DBO.DVZ (NOLOCK) DVZ ON STI.Tarih = DVZ.Tarih AND DVZ.DovizCinsi='EUR'
	WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2017
FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
WHERE STII.KynkEvrakTip=1
GROUP BY STKK.MalAdi4 ) A
WHERE A.Yil2016<>0 OR A.Yil2017<>0
 ) A
 WHERE RowID BETWEEN 16 AND 100000
	END

	ELSE
	BEGIN
	SELECT * FROM (

	SELECT TOP (15) '33' as DB, @Ay as Ay, @Kriter as Kriter, A.GrupKod,A.Yil2016,A.Yil2017, A.Yil2016+A.Yil2017 As Toplam FROM ( 
	SELECT (
	CASE WHEN STKK.MalAdi4='' THEN 'BOŞ' ELSE STKK.MalAdi4 END) as GrupKod,
	cast(ISNULL((SELECT SUM(STI.BirimMiktar) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2016 , 
	cast(ISNULL((SELECT SUM(STI.BirimMiktar) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2017
FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
WHERE STII.KynkEvrakTip=1
GROUP BY STKK.MalAdi4 ) A
WHERE A.Yil2016<>0 OR A.Yil2017<>0
ORDER BY Toplam DESC
) B

UNION ALL

 SELECT '33' as DB, @Ay as Ay, @Kriter as Kriter, 'Diğer' as GrupKod, ISNULL(SUM(Y2016),0) as Yil2016,ISNULL(SUM(Y2017),0) as Yil2017,ISNULL(SUM(Toplam2),0) as Toplam FROM
 (
    SELECT ROW_NUMBER() OVER(ORDER BY A.Yil2016+A.Yil2017 DESC) as RowID, A.GrupKod,A.Yil2016 as Y2016,A.Yil2017 as Y2017, A.Yil2016+A.Yil2017 As Toplam2 FROM ( 
	SELECT (
	CASE WHEN STKK.MalAdi4='' THEN 'BOŞ' ELSE STKK.MalAdi4 END) as GrupKod,
	cast(ISNULL((SELECT SUM(STI.BirimMiktar) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2016 AND @BitTarih2016 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2016 , 
	cast(ISNULL((SELECT SUM(STI.BirimMiktar) AS GenelTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
	INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
	WHERE STI.KynkEvrakTip in(1,2,125,126,64,117,163) AND STK.MalAdi4=STKK.MalAdi4 AND STI.Tarih BETWEEN @BasTarih2017 AND @BitTarih2017 
    GROUP BY STK.MalAdi4),0) as numeric(36,2)) as Yil2017
FROM FINSAT633.FINSAT633.STI(NOLOCK) STII 
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHKK ON STII.Chk=CHKK.HesapKodu
INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STKK ON STII.MalKodu=STKK.MalKodu
WHERE STII.KynkEvrakTip=1
GROUP BY STKK.MalAdi4 ) A
WHERE A.Yil2016<>0 OR A.Yil2017<>0
 ) A
 WHERE RowID BETWEEN 16 AND 100000
	END

END
GO
/****** Object:  StoredProcedure [wms].[Fatura_Onay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[Fatura_Onay]
@onayTip int
AS
BEGIN
SET NOCOUNT ON;  

	SELECT CONVERT(VARCHAR(14),CONVERT(Datetime, STI.Tarih-2),104) AS Tarih, STI.EvrakNo, STI.Chk, STI.Not1,
CHK.Unvan1+ SPACE(2) + CHK.Unvan2 AS Unvan 
	FROM FINSAT633.STI  STI(NOLOCK) 
	INNER JOIN 
	FINSAT633.CHK (NOLOCK)  CHK ON STI.Chk= Chk.HesapKodu 
	--WHERE STI.KynkEvrakTip=1 AND STI.Kod10='Onay Bekliyor'
	WHERE STI.KynkEvrakTip IN(1,163) AND STI.Kod10= Case when (@onayTip=0) then
	 'Onay Bekliyor' else 'Reddedildi' end
	GROUP BY 
	STI.Tarih, STI.EvrakNo, STI.Chk, CHK.Unvan1, CHK.Unvan2, STI.Not1
END

GO
/****** Object:  StoredProcedure [wms].[Fatura_OnayDetay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Modify date: 17.07.2017
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE[wms].[Fatura_OnayDetay] (@EvrakNo varchar(16))
AS
BEGIN
SET NOCOUNT ON;
	SELECT TOP(1) STI.Chk, CHK.Unvan1+' '+CHK.Unvan2 AS Unvan, STI.TeslimChk AS TeslimYeriKodu,STI.Kaydeden,STI.Degistiren
,MFK.Aciklama,MFK.Aciklama2,MFK.Aciklama3,MFK.Aciklama4,MFK.Aciklama5,MFK.Aciklama6,
STI.EvrakNo, CONVERT(VARCHAR(10),CONVERT(Datetime, STI.Tarih-2),104) AS Tarih,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME(NOLOCK) WHERE ITEMCBOID='57' AND ID=STI.IslemTip) AS IslemTip , 
FTD.EFatNot,
KTK.Aciklama as TeslimSarti ,
(case
IFB.GonderimSekli 
 when 0 then 'Belirsiz'
when 1 then 'Denizyolu'
when 2 then 'Demiryolu'
when 3 then 'Karayolu'
when 4 then 'Havayolu'
when 5 then 'Posta'
when 6 then 'Çok araçı'
when 7 then 'Posta'
when 8 then 'Sabit taşıma tesisleri'
when 9 then 'İç su taşımacılığı'
end
) as GonderimSekli,
ISNULL(IFB.BrutKilo,0) as BrutKilo,
ISNULL(IFB.NetKilo,0) as NetKilo,
FOY.Notlar as OdemeNotu,
KTK2.Aciklama as OdemeSekli,
CONVERT(VARCHAR(10),CONVERT(Datetime, CHI.VadeTarih-2),104) AS VadeTarih,
(CASE WHEN FTD.DvzTL=0 THEN 'Yerel Para' ELSE 'Döviz' END) AS DvzTL,
FTD.DovizCinsi,
replace(replace(replace(convert(varchar,CAST(FTD.DovizKuru  as decimal(16,4)),1),'.','x'),',','.'),'x',',') as DovizKuru , 
ISNULL((SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME(NOLOCK) WHERE ITEMCBOID='223' AND ID=FTD.EFatTip),'İhracat') AS EFatTip
--(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME(NOLOCK) WHERE ITEMCBOID='224' AND ID=FTD.EFatDurum) AS EFatDurum
FROM FINSAT633.FINSAT633.STI(NOLOCK) STI
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
INNER JOIN FINSAT633.FINSAT633.MFK(NOLOCK) MFK ON CHK.HesapKodu=MFK.HesapKod AND MFK.EvrakNo=STI.EvrakNo AND MFK.KynkEvrakTip=STI.KynkEvrakTip
INNER JOIN FINSAT633.FINSAT633.CHI(NOLOCK) CHI ON CHI.IslemTip=4 AND STI.Chk=CHI.HesapKodu AND STI.EvrakNo=CHI.EvrakNo AND STI.KynkEvrakTip=CHI.KynkEvrakTip
INNER JOIN FINSAT633.FINSAT633.FTD(NOLOCK) FTD ON FTD.EvrakNo=STI.EvrakNo AND FTD.KynkEvrakTip=STI.KynkEvrakTip
LEFT JOIN FINSAT633.FINSAT633.IFB(NOLOCK) IFB ON STI.EvrakNo=IFB.FaturaNo AND STI.Tarih=IFB.Tarih AND STI.Chk=IFB.CHK
LEFT JOIN FINSAT633.FINSAT633.FOY(NOLOCK) FOY ON IFB.FaturaID=FOY.FaturaID
LEFT JOIN solar6.dbo.KTK(NOLOCK) KTK ON IFB.TeslimSarti=KTK.KOD AND KTK.Tip=415
LEFT JOIN solar6.dbo.KTK(NOLOCK) KTK2 ON FOY.Odemesekli=KTK2.KOD AND KTK2.Tip=417
WHERE STI.KynkEvrakTip IN (1,163) AND STI.EvrakNo=@EvrakNo
Order By STI.DegisTarih desc, STI.DegisSaat desc


SELECT STI.MalKodu,STK.MalAdi,STI.Birim,
replace(replace(replace(convert(varchar,CAST(STI.BirimMiktar as decimal(16,4)),1),'.','x'),',','.'),'x',',') as BirimMiktar ,
replace(replace(replace(convert(varchar,CAST(STI.BirimFiyat as decimal(16,4)),1),'.','x'),',','.'),'x',',') as BirimFiyat, 
replace(replace(replace(convert(varchar,CAST(STI.Tutar as money),1),'.','x'),',','.'),'x',',') as Tutar,
STI.Depo,STI.KdvOran,(SELECT K.Aciklama FROM SOLAR6.dbo.KTK(NOLOCK) K WHERE K.Tip=410 AND K.Kod=STI.KdvMuafAck) AS KdvMuafiyetNedeni,
(CASE WHEN STI.DvzTL=0 THEN 'Yerel Para' ELSE 'Döviz' END) AS DvzTL,STI.DovizCinsi,
replace(replace(replace(convert(varchar,CAST(STI.DovizKuru as decimal(16,4)),1),'.','x'),',','.'),'x',',') as DovizKuru,
replace(replace(replace(convert(varchar,CAST(STI.DovizTutar as money),1),'.','x'),',','.'),'x',',') as DovizTutar,
replace(replace(replace(convert(varchar,CAST(STI.IskontoOran1 as money),1),'.','x'),',','.'),'x',',') as IskontoOran1,
replace(replace(replace(convert(varchar,CAST(STI.IskontoOran2 as money),1),'.','x'),',','.'),'x',',') as IskontoOran2,
replace(replace(replace(convert(varchar,CAST(STI.IskontoOran3 as money),1),'.','x'),',','.'),'x',',') as IskontoOran3,
replace(replace(replace(convert(varchar,CAST(STI.IskontoOran4 as money),1),'.','x'),',','.'),'x',',') as IskontoOran4,
replace(replace(replace(convert(varchar,CAST(STI.IskontoOran5 as money),1),'.','x'),',','.'),'x',',') as IskontoOran5,
replace(replace(replace(convert(varchar,CAST((SELECT FINSAT633.Iskontolu_Fiyat_Hesapla (STI.Tutar,  STI.IskontoOran1,  STI.IskontoOran2,  STI.IskontoOran3, STI.IskontoOran4, STI.IskontoOran5)) as money),1),'.','x'),',','.'),'x',',') as NetTutar,STI.Nesne1,STI.CheckSum
FROM FINSAT633.FINSAT633.STI(NOLOCK) STI
LEFT JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STK.MalKodu=STI.MalKodu
WHERE STI.KynkEvrakTip IN (1,163) AND STI.EvrakNo=@EvrakNo


SELECT FTD.Aciklama AS Tip, FTD.Aciklama,
replace(replace(replace(convert(varchar,CAST(FTD.IskontoOran as money),1),'.','x'),',','.'),'x',',') as IskontoOran,
replace(replace(replace(convert(varchar,CAST(FTD.Iskonto as money),1),'.','x'),',','.'),'x',',') as Iskonto,
(CASE WHEN FTD.DvzTL=0 THEN 'Yerel Para' ELSE 'Döviz' END) AS DvzTL,FTD.DovizCinsi,
replace(replace(replace(convert(varchar,CAST(FTD.DovizKuru as decimal(16,4)),1),'.','x'),',','.'),'x',',') as DovizKuru,
replace(replace(replace(convert(varchar,CAST(FTD.DovizTutar as money),1),'.','x'),',','.'),'x',',') as DovizTutar,
FTD.KdvMuafAck AS KdvMuafiyetNedeni  FROM FINSAT633.FINSAT633.FTD(NOLOCK) FTD
WHERE FTD.KynkEvrakTip IN (1,163) AND FTD.EvrakNo=@EvrakNo
END








GO
/****** Object:  StoredProcedure [wms].[Fatura_OnayUpdate]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [wms].[Fatura_OnayUpdate] (@Tip Smallint, @EvrakNo varchar(16), @Chk varchar(20),@Tarih int, @RedNedeni varchar(250),@Degistiren varchar(5),@Degistarih int)

AS

BEGIN TRY

        BEGIN TRAN
			
			IF(@Tip=1) ------ ONAYLANDI
			BEGIN

				DECLARE @EFatDurum SMALLINT

				SELECT TOP(1) @EFatDurum=(CASE WHEN (EFatDurum=5 AND Kod10='Onay Bekliyor') THEN 0 ELSE EFatDurum END) FROM FINSAT633.FINSAT633.STI WHERE EvrakNo=@EvrakNo AND Chk=@Chk 
				AND Tarih= @Tarih AND KynkEvrakTip IN (1,163)


			 				UPDATE FINSAT633.FINSAT633.STI SET Kod10='Onaylandı', CheckSum=1, EFatDurum=@EFatDurum,degistiren=@Degistiren, degistarih=@Degistarih  WHERE EvrakNo=@EvrakNo AND Chk=@Chk 
				AND Tarih= @Tarih AND KynkEvrakTip IN (1,163) 

			

					UPDATE FINSAT633.FINSAT633.FTD SET  EFatDurum=0 ,CheckSum=1,degistiren=@Degistiren, degistarih=@Degistarih WHERE EvrakNo=@EvrakNo AND HesapKodu=@Chk 
				AND Tarih= @Tarih AND KynkEvrakTip IN (1,163) AND EFatTip<>0



			END

			ELSE IF(@Tip=0) ------ REDDEDİLDİ
			BEGIN
			   
			 	UPDATE FINSAT633.FINSAT633.STI SET Kod10='Reddedildi', CheckSum=1, Not1=@RedNedeni,degistiren=@Degistiren, degistarih=@Degistarih   WHERE EvrakNo=@EvrakNo AND Chk=@Chk 
				AND Tarih= @Tarih AND KynkEvrakTip IN (1,163)

				UPDATE FINSAT633.FINSAT633.FTD SET EFatDurum=(case satirtip when 0 then 0 else 5 end), CheckSum=1,degistiren=@Degistiren, degistarih=@Degistarih  WHERE EvrakNo=@EvrakNo AND HesapKodu=@Chk 
				AND Tarih= @Tarih AND KynkEvrakTip IN (1,163) AND EFatTip<>0
			
		    END

        COMMIT TRAN

			 SELECT 1 AS SONUC
END TRY
BEGIN CATCH
			 SELECT 0 AS SONUC
               ROLLBACK TRAN
END CATCH





GO
/****** Object:  StoredProcedure [wms].[FiyatOnayList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayList]
AS
BEGIN
	
	SELECT ID, FiyatListNum, fiyat.MalKodu, STK.MalAdi, HesapKodu, (CASE HesapKodu WHEN '' THEN '' ELSE (SELECT TOP 1 Unvan1+' '+Unvan2 FROM FINSAT633.FINSAT633.CHK(NOLOCK) WHERE HesapKodu=fiyat.HesapKodu) END) AS Unvan, fiyat.SatisFiyat1, SatisFiyat1Birim, SatisFiyat1BirimInt, fiyat.DovizSatisFiyat1, fiyat.DovizSF1Birim, DovizSF1BirimInt, fiyat.DovizCinsi, fiyat.ROW_ID, Onay, (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum, Onaylayan AS OzgurMu 
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) fiyat
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON fiyat.MalKodu=STK.MalKodu
	--Where Onay=0 AND GMOnay=0
	      --AND SPGMYOnay=0  --Bu satır 16.04.2015 Filtre kağıt SM ve GM'ye düşecek  
		                     --talebi üzerine iptal edildi.

END










GO
/****** Object:  StoredProcedure [wms].[FiyatOnayList_GrupEbatYuzey]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayList_GrupEbatYuzey]
AS
BEGIN
	
	SELECT  STK.GrupKod, STK.TipKod AS Kalite, STK.Kod2 AS En, STK.Kod3 AS Boy, 
	        STK.Kod1 AS Kalinlik, STK.Kod8 AS Yuzey,
	        Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, CAST(0 as Bit) Onay, 
	        (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 
			THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) 
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON Fiyat.MalKodu=STK.MalKodu
	WHERE STK.GrupKod<>'PARKE' AND Onay=0 AND GMOnay=0
	GROUP BY STK.GrupKod, STK.TipKod, STK.Kod2, STK.Kod3, STK.Kod1, STK.Kod8, 
	        Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, 
			(CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 
			THEN 'Güncellenecek Kayıt' ELSE '' END) 

 

END












GO
/****** Object:  StoredProcedure [wms].[FiyatOnayList_Koleksiyon]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayList_Koleksiyon]
AS
BEGIN
	
	SELECT  STK.Kod4, STK.TipKod, Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, CAST(0 as Bit) Onay, 
	        (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) 
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON Fiyat.MalKodu=STK.MalKodu
	WHERE STK.GrupKod='PARKE' AND Onay=0 AND GMOnay=0
	GROUP BY STK.Kod4, STK.TipKod, Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, 
			(CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) 

 

END











GO
/****** Object:  StoredProcedure [wms].[FiyatOnayListGM]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayListGM]
AS
BEGIN
	
	SELECT ID, FiyatListNum, fiyat.MalKodu, STK.MalAdi, HesapKodu, (CASE HesapKodu WHEN '' THEN '' ELSE (SELECT TOP 1 Unvan1+' '+Unvan2 FROM FINSAT633.FINSAT633.CHK(NOLOCK) WHERE HesapKodu=fiyat.HesapKodu) END) AS Unvan, fiyat.SatisFiyat1, SatisFiyat1Birim, SatisFiyat1BirimInt, fiyat.DovizSatisFiyat1, fiyat.DovizSF1Birim, DovizSF1BirimInt, fiyat.DovizCinsi, fiyat.ROW_ID, GMOnay as Onay, (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum 
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) fiyat
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON fiyat.MalKodu=STK.MalKodu
	where Onay=1 and SPGMYOnay=1 and GMOnay=0

END










GO
/****** Object:  StoredProcedure [wms].[FiyatOnayListGM_GrupEbatYuzey]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayListGM_GrupEbatYuzey]
AS
BEGIN
	
	SELECT  STK.GrupKod, STK.TipKod AS Kalite, STK.Kod2 AS En, STK.Kod3 AS Boy, 
	        STK.Kod1 AS Kalinlik, STK.Kod8 AS Yuzey,
	        Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, CAST(0 as Bit) Onay, 
	        (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 
			THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) 
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON Fiyat.MalKodu=STK.MalKodu
	WHERE STK.GrupKod<>'PARKE' AND Onay=1 AND SPGMYOnay=1 AND GMOnay=0
	GROUP BY STK.GrupKod, STK.TipKod, STK.Kod2, STK.Kod3, STK.Kod1, STK.Kod8, 
	        Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, 
			(CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 
			THEN 'Güncellenecek Kayıt' ELSE '' END) 

 

END












GO
/****** Object:  StoredProcedure [wms].[FiyatOnayListGM_Koleksiyon]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayListGM_Koleksiyon]
AS
BEGIN

    SELECT  STK.Kod4, STK.TipKod, Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, CAST(0 as Bit) Onay, 
	        (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) 
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON Fiyat.MalKodu=STK.MalKodu
	WHERE STK.GrupKod='PARKE' AND Onay=1 AND SPGMYOnay=1 AND GMOnay=0
	GROUP BY STK.Kod4, STK.TipKod, Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, 
			(CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) 

	
END











GO
/****** Object:  StoredProcedure [wms].[FiyatOnayListSPGMY]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayListSPGMY]
AS
BEGIN
	
	SELECT ID, FiyatListNum, fiyat.MalKodu, STK.MalAdi, HesapKodu, (CASE HesapKodu WHEN '' THEN '' ELSE (SELECT TOP 1 Unvan1+' '+Unvan2 FROM FINSAT633.FINSAT633.CHK(NOLOCK) WHERE HesapKodu=fiyat.HesapKodu) END) AS Unvan, fiyat.SatisFiyat1, SatisFiyat1Birim, SatisFiyat1BirimInt, fiyat.DovizSatisFiyat1, fiyat.DovizSF1Birim, DovizSF1BirimInt, fiyat.DovizCinsi, fiyat.ROW_ID, SPGMYOnay as Onay, (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum 
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) fiyat
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON fiyat.MalKodu=STK.MalKodu
	where Onay=1 --and SPGMYOnay=0 and GMOnay=0

END










GO
/****** Object:  StoredProcedure [wms].[FiyatOnayListSPGMY_GrupEbatYuzey]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayListSPGMY_GrupEbatYuzey]
AS
BEGIN
	
	SELECT  STK.GrupKod, STK.TipKod AS Kalite, STK.Kod2 AS En, STK.Kod3 AS Boy, 
	        STK.Kod1 AS Kalinlik, STK.Kod8 AS Yuzey,
	        Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, CAST(0 as Bit) Onay, 
	        (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 
			THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) 
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON Fiyat.MalKodu=STK.MalKodu
	WHERE STK.GrupKod<>'PARKE' AND Onay=1 AND SPGMYOnay=0 AND GMOnay=0
	GROUP BY STK.GrupKod, STK.TipKod, STK.Kod2, STK.Kod3, STK.Kod1, STK.Kod8, 
	        Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, 
			(CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 
			THEN 'Güncellenecek Kayıt' ELSE '' END) 

 

END












GO
/****** Object:  StoredProcedure [wms].[FiyatOnayListSPGMY_Koleksiyon]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayListSPGMY_Koleksiyon]
AS
BEGIN
	
	 SELECT  STK.Kod4, STK.TipKod, Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, CAST(0 as Bit) Onay, 
	        (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) 
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON Fiyat.MalKodu=STK.MalKodu
	WHERE STK.GrupKod='PARKE' AND Onay=1 AND SPGMYOnay=0 AND GMOnay=0
	GROUP BY STK.Kod4, STK.TipKod, Fiyat.FiyatListNum, Fiyat.SatisFiyat1, Fiyat.SatisFiyat1Birim, 
	        Fiyat.SatisFiyat1BirimInt, Fiyat.DovizSatisFiyat1, Fiyat.DovizSF1Birim, 
			Fiyat.DovizSF1BirimInt, Fiyat.DovizCinsi, 
			(CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) 


END











GO
/****** Object:  StoredProcedure [wms].[FiyatOnayListTumBekleyenler]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatOnayListTumBekleyenler]
AS
BEGIN
	
	SELECT ID, FiyatListNum, fiyat.MalKodu, STK.MalAdi, HesapKodu, (CASE HesapKodu WHEN '' THEN '' ELSE (SELECT TOP 1 Unvan1+' '+Unvan2 
	FROM FINSAT633.FINSAT633.CHK(NOLOCK) WHERE HesapKodu=fiyat.HesapKodu) END) AS Unvan, fiyat.SatisFiyat1, SatisFiyat1Birim, SatisFiyat1BirimInt, fiyat.DovizSatisFiyat1, fiyat.DovizSF1Birim, DovizSF1BirimInt, fiyat.DovizCinsi, fiyat.ROW_ID, Onay, (CASE Durum WHEN 1 THEN 'Yeni Kayıt' WHEN 2 THEN 'Silinecek Kayıt' WHEN 3 THEN 'Güncellenecek Kayıt' ELSE '' END) AS Durum, (CASE WHEN Onay=0  and GMOnay=0 THEN 'SM' WHEN Onay=1 and SPGMYOnay=0 and GMOnay=0 THEN 'SPGMY' WHEN Onay=1 and SPGMYOnay=1 and GMOnay=0 THEN 'GM' ELSE '' END) AS HangiOnayda
	FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) fiyat
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON fiyat.MalKodu=STK.MalKodu
	where Onay=0 or SPGMYOnay=0 or GMOnay=0

END










GO
/****** Object:  StoredProcedure [wms].[FiyatTanimVeOnay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[FiyatTanimVeOnay]
@ListeNo VARCHAR(30)
AS
BEGIN
	
SELECT FYT.MalKodu, MusteriKodu, FYT.SatisFiyat1, (CASE FYT.SF1Birim WHEN 1 THEN STK.Birim1 WHEN 2 THEN STK.Birim2 ELSE '' END) AS SF1Birim, (CASE FYT.DovizSF1Birim WHEN 1 THEN STK.Birim1 WHEN 2 THEN STK.Birim2 ELSE '' END) AS DovizSF1Birim, DvzSatisFiyat1, FYT.SF1DovizCinsi
FROM FINSAT633.FINSAT633.FYT(NOLOCK) FYT
INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON FYT.MalKodu=STK.MalKodu
WHERE FiyatListNum= @ListeNo

UNION ALL

SELECT fiyat.MalKodu, HesapKodu,fiyat.SatisFiyat1, SatisFiyat1Birim,fiyat.DovizSF1Birim,fiyat.DovizSatisFiyat1, fiyat.DovizCinsi
FROM FINSAT633.FINSAT633.Fiyat(NOLOCK) fiyat
INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON fiyat.MalKodu=STK.MalKodu
Where Onay=0 AND GMOnay=0 AND SPGMYOnay=0 AND FiyatListNum= @ListeNo

END








GO
/****** Object:  StoredProcedure [wms].[FYTSelect1]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [wms].[FYTSelect1]
@ListeNo VARCHAR(30)
AS
BEGIN
	
	SELECT CAST(0 AS BIT) AS Onay, FYT.ROW_ID, FYT.MalKodu, STK.MalAdi, STK.GrupKod, MusteriKodu, (CASE MusteriKodu WHEN '' THEN '' ELSE (SELECT TOP 1 Unvan1+' '+Unvan2 FROM FINSAT633.FINSAT633.CHK(NOLOCK) WHERE HesapKodu=MusteriKodu) END) AS Unvan, FYT.SatisFiyat1, (CASE FYT.SF1Birim WHEN 1 THEN STK.Birim1 WHEN 2 THEN STK.Birim2 ELSE '' END) AS SF1Birim, (CASE FYT.DovizSF1Birim WHEN 1 THEN STK.Birim1 WHEN 2 THEN STK.Birim2 ELSE '' END) AS DovizSF1Birim, DvzSatisFiyat1, FYT.SF1DovizCinsi, STK.Birim1, STK.Birim2, STK.Birim3
	FROM FINSAT633.FINSAT633.FYT(NOLOCK) FYT
	INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON FYT.MalKodu=STK.MalKodu
	WHERE FiyatListNum=@ListeNo
	ORDER BY FiyatListNum

END
GO
/****** Object:  StoredProcedure [wms].[FYTSelect2]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [wms].[FYTSelect2]
AS
BEGIN
	
	SELECT FiyatListNum, FiyatListName FROM FINSAT633.FINSAT633.FYT (NOLOCK) GROUP BY FiyatListNum, FiyatListName
	ORDER BY FiyatListNum
END
GO
/****** Object:  StoredProcedure [wms].[GerceklesenSevkiyatRaporu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[GerceklesenSevkiyatRaporu]
@BasTarih int,
@BitTarih int
AS
BEGIN

SELECT CONVERT(DATETIME,STI.Tarih-2) AS Tarih, STI.EvrakNo, STI.Chk, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan, 
(
	SELECT Aciklama
	FROM SOLAR6.DBO.KTK (NOLOCK)
	WHERE Tip = 8 AND Kod = CHK.OzelKod
) AS SatisTemsilcisi, STI.Malkodu, STK.MalAdi,  STI.Birimmiktar, STI.Birim, STK.DvrMiktar+STK.GirMiktar-STK.CikMiktar AS StokMiktar, STK.Birim1 AS StokBirim
FROM FINSAT633.FINSAT633.STI (NOLOCK) STI 
INNER JOIN FINSAT633.FINSAT633.CHK (NOLOCK) CHK ON CHK.HesapKodu=STI.Chk 
INNER JOIN FINSAT633.FINSAT633.STK (NOLOCK) STK ON STK.Malkodu=STI.Malkodu
WHERE STI.Kynkevraktip=0 
AND STI.Tarih BETWEEN @BasTarih AND @BitTarih
ORDER BY Tarih desc

END









GO
/****** Object:  StoredProcedure [wms].[getBarcodeDetails]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 01.07.2017
-- Modify date: 01.07.2017
-- Description:	paket barkodu için bilgi getir
-- =============================================
CREATE PROCEDURE [wms].[getBarcodeDetails]
	@SirketKodu varchar(10),
	@DepoKodu varchar(3),
	@EvrakNo varchar(16)

AS
BEGIN
	

	declare @GonderenUnvan varchar(40), @tel varchar(14), @fax varchar(14), @GonderenAdres varchar(MAX), @Unvan varchar(MAX), @FaturaAdres varchar(MAX), @TeslimAdres varchar(MAX)

	SELECT        @GonderenUnvan = solar6.dbo.SIR.Unvan1, @tel = solar6.dbo.SIR.Telefon1, @fax = solar6.dbo.SIR.Fax1
	FROM            solar6.dbo.SDK INNER JOIN
							 solar6.dbo.SIR ON solar6.dbo.SDK.SirketKod = solar6.dbo.SIR.Kod
	WHERE        (solar6.dbo.SDK.Kod = @SirketKodu)

	SELECT        @GonderenAdres = (Adres1 + ' ' + Adres2 + ' ' + Adres3)
							  FROM            FINSAT633.FINSAT633.DEP
							  WHERE        (Depo = @DepoKodu)

	SELECT        @Unvan = (CHK.Unvan1 + ' ' + CHK.Unvan2), 
							 @FaturaAdres = (CHK.FaturaAdres1 + ' ' + CHK.FaturaAdres2 + ' ' + CHK.FaturaAdres3), 
							 @TeslimAdres = CASE WHEN MFK.Aciklama = '' THEN CHK.TeslimAdres1 + ' ' + CHK.TeslimAdres2 + ' ' + CHK.TeslimAdres3 ELSE MFK.Aciklama END
	FROM            FINSAT633.FINSAT633.STI INNER JOIN
							 FINSAT633.FINSAT633.CHK ON STI.Chk = CHK.HesapKodu
							 INNER JOIN  FINSAT633.FINSAT633.MFK ON STI.CHK=MFK.HesapKod AND STI.EvrakNo=MFK.EvrakNo AND STI.KynkEvrakTip=MFK.KynkEvrakTip
	WHERE        (STI.EvrakNo = @EvrakNo)

SELECT        @GonderenUnvan AS GonderenUnvan, @tel AS tel, @fax AS fax, @GonderenAdres AS GonderenAdres, @Unvan AS Unvan, @FaturaAdres AS FaturaAdres, @TeslimAdres AS TeslimAdres
END

GO
/****** Object:  StoredProcedure [wms].[getBarcodeDetails-]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 01.07.2017
-- Modify date: 01.07.2017
-- Description:	paket barkodu için bilgi getir
-- =============================================
CREATE PROCEDURE [wms].[getBarcodeDetails-]
	@SirketKodu varchar(10),
	@DepoKodu varchar(3),
	@EvrakNo varchar(16)

AS
BEGIN
	
	declare @GonderenUnvan varchar(40), @tel varchar(14), @fax varchar(14), @GonderenAdres varchar(MAX), @Unvan varchar(MAX), @FaturaAdres varchar(MAX), @TeslimAdres varchar(MAX)

	SELECT        @GonderenUnvan = solar6.dbo.SIR.Unvan1, @tel = solar6.dbo.SIR.Telefon1, @fax = solar6.dbo.SIR.Fax1
	FROM            solar6.dbo.SDK INNER JOIN
							 solar6.dbo.SIR ON solar6.dbo.SDK.SirketKod = solar6.dbo.SIR.Kod
	WHERE        (solar6.dbo.SDK.Kod = @SirketKodu)

	SELECT        @GonderenAdres = (Adres1 + ' ' + Adres2 + ' ' + Adres3)
							  FROM            FINSAT633.DEP
							  WHERE        (Depo = @DepoKodu)

	SELECT        @Unvan = (FINSAT633.CHK.Unvan1 + ' ' + FINSAT633.CHK.Unvan2), 
							 @FaturaAdres = (FINSAT633.CHK.FaturaAdres1 + ' ' + FINSAT633.CHK.FaturaAdres2 + ' ' + FINSAT633.CHK.FaturaAdres3), 
							 @TeslimAdres = CASE WHEN FINSAT633.STI.Aciklama = '' THEN FINSAT633.CHK.TeslimAdres1 + ' ' + FINSAT633.CHK.TeslimAdres2 + ' ' + FINSAT633.CHK.TeslimAdres3 ELSE FINSAT633.STI.Aciklama END
	FROM            FINSAT633.STI INNER JOIN
							 FINSAT633.CHK ON FINSAT633.STI.Chk = FINSAT633.CHK.HesapKodu
	WHERE        (FINSAT633.STI.EvrakNo = @EvrakNo)

	SELECT        @GonderenUnvan AS GonderenUnvan, @tel AS tel, @fax AS fax, @GonderenAdres AS GonderenAdres, @Unvan AS Unvan, @FaturaAdres AS FaturaAdres, @TeslimAdres AS TeslimAdres

END


GO
/****** Object:  StoredProcedure [wms].[getBarcodeDetails-rename]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 01.07.2017
-- Modify date: 01.07.2017
-- Description:	paket barkodu için bilgi getir
-- =============================================
CREATE PROCEDURE [wms].[getBarcodeDetails-rename]
	@SirketKodu varchar(10),
	@DepoKodu varchar(3),
	@EvrakNo varchar(16)

AS
BEGIN
	
	declare @GonderenUnvan varchar(40), @tel varchar(14), @fax varchar(14), @GonderenAdres varchar(MAX), @Unvan varchar(MAX), @FaturaAdres varchar(MAX), @TeslimAdres varchar(MAX)

	SELECT        @GonderenUnvan = solar6.dbo.SIR.Unvan1, @tel = solar6.dbo.SIR.Telefon1, @fax = solar6.dbo.SIR.Fax1
	FROM            solar6.dbo.SDK INNER JOIN
							 solar6.dbo.SIR ON solar6.dbo.SDK.SirketKod = solar6.dbo.SIR.Kod
	WHERE        (solar6.dbo.SDK.Kod = @SirketKodu)

	SELECT        @GonderenAdres = (Adres1 + ' ' + Adres2 + ' ' + Adres3)
							  FROM            FINSAT633.DEP
							  WHERE        (Depo = @DepoKodu)

	SELECT        @Unvan = (FINSAT633.CHK.Unvan1 + ' ' + FINSAT633.CHK.Unvan2), 
							 @FaturaAdres = (FINSAT633.CHK.FaturaAdres1 + ' ' + FINSAT633.CHK.FaturaAdres2 + ' ' + FINSAT633.CHK.FaturaAdres3), 
							 @TeslimAdres = CASE WHEN FINSAT633.STI.Aciklama = '' THEN FINSAT633.CHK.TeslimAdres1 + ' ' + FINSAT633.CHK.TeslimAdres2 + ' ' + FINSAT633.CHK.TeslimAdres3 ELSE FINSAT633.STI.Aciklama END
	FROM            FINSAT633.STI INNER JOIN
							 FINSAT633.CHK ON FINSAT633.STI.Chk = FINSAT633.CHK.HesapKodu
	WHERE        (FINSAT633.STI.EvrakNo = @EvrakNo)

	SELECT        @GonderenUnvan AS GonderenUnvan, @tel AS tel, @fax AS fax, @GonderenAdres AS GonderenAdres, @Unvan AS Unvan, @FaturaAdres AS FaturaAdres, @TeslimAdres AS TeslimAdres

END
GO
/****** Object:  StoredProcedure [wms].[gethatalibarkodlar]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 16.06.2017
-- Modify date: 16.06.2017
-- Description:	tekrarlanan barkodları gösterir
-- =============================================
CREATE PROCEDURE [wms].[gethatalibarkodlar]

AS
BEGIN
	
	SELECT        Barkod, COUNT(MalKodu) AS Sayı
	FROM            (SELECT        BarKod1 AS barkod, MalKodu
					FROM            FINSAT633.FINSAT633.STK
					UNION
					SELECT        BarKod2 AS barkod, MalKodu
					FROM            FINSAT633.FINSAT633.STK AS STK_1) AS t
	GROUP BY barkod
	HAVING        (COUNT(MalKodu) > 1)

END
GO
/****** Object:  StoredProcedure [wms].[getIrsaliyeDetay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 17.01.2018
-- Last Modify: 17.01.2018
-- Description:	irsaliye detay listesi
-- =============================================
CREATE PROCEDURE [wms].[getIrsaliyeDetay]
	@IrsID int

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        IRS_Detay.MalKodu, STK.MalAdi, IRS_Detay.Birim, IRS_Detay.Miktar, BIRIKIM.wms.fnGetStockByID(IRS.DepoID, IRS_Detay.MalKodu, '') as Stok
	FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
							 BIRIKIM.wms.IRS ON IRS_Detay.IrsaliyeID = IRS.ID INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.IRS_Detay.MalKodu
	WHERE        (IRS_Detay.IrsaliyeID = @IrsID)

END
GO
/****** Object:  StoredProcedure [wms].[GetIrsDetayfromGorev]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 10.03.2017
-- Modify date: 22.12.2017
-- Description:	görev noyu verince ona ait irs detay tablosu gelir
-- =============================================
Create PROCEDURE [wms].[GetIrsDetayfromGorev]
	@GorevID int
AS
BEGIN
	SET NOCOUNT ON;

	--varsa transferi göster
	IF NOT EXISTS (SELECT IrsaliyeID FROM BIRIKIM.wms.GorevIRS WHERE (GorevID = @GorevID))
		SELECT        BIRIKIM.wms.Gorev.ID, BIRIKIM.wms.Transfer.SirketKod, BIRIKIM.wms.Gorev.GorevNo, BIRIKIM.wms.Transfer_Detay.MalKodu, STK.MalAdi, SUM(BIRIKIM.wms.Transfer_Detay.Miktar) AS Miktar, BIRIKIM.wms.Transfer_Detay.Birim, '' AS MakaraNo, 
								 0 AS Sira, '' AS HucreAd
		FROM            BIRIKIM.wms.Transfer INNER JOIN
								 BIRIKIM.wms.Transfer_Detay ON BIRIKIM.wms.Transfer.ID = BIRIKIM.wms.Transfer_Detay.TransferID INNER JOIN
								 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.Transfer_Detay.MalKodu INNER JOIN
								 BIRIKIM.wms.Gorev ON BIRIKIM.wms.Transfer.GorevID = BIRIKIM.wms.Gorev.ID
		WHERE        (BIRIKIM.wms.Gorev.ID = @GorevID)
		GROUP BY BIRIKIM.wms.Gorev.ID, BIRIKIM.wms.Transfer.SirketKod, BIRIKIM.wms.Gorev.GorevNo, BIRIKIM.wms.Transfer_Detay.MalKodu, STK.MalAdi, BIRIKIM.wms.Transfer_Detay.Birim
		ORDER BY BIRIKIM.wms.Transfer_Detay.MalKodu
	
	--varsa yer tablosunu göster
	ELSE IF EXISTS (SELECT ID FROM BIRIKIM.wms.GorevYer WHERE (GorevID = @GorevID))
		SELECT        BIRIKIM.wms.Gorev.ID, BIRIKIM.wms.IRS.SirketKod, BIRIKIM.wms.Gorev.GorevNo, BIRIKIM.wms.GorevYer.MalKodu, STK.MalAdi, SUM(BIRIKIM.wms.GorevYer.Miktar) AS Miktar, BIRIKIM.wms.GorevYer.Birim, '' AS MakaraNo, ISNULL(BIRIKIM.wms.GorevYer.Sira, 0) 
								 AS Sira, BIRIKIM.wms.Yer.HucreAd
		FROM            BIRIKIM.wms.GorevYer INNER JOIN
								 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevYer.GorevID = BIRIKIM.wms.Gorev.ID INNER JOIN
								 BIRIKIM.wms.IRS ON BIRIKIM.wms.Gorev.IrsaliyeID = BIRIKIM.wms.IRS.ID INNER JOIN
								 BIRIKIM.wms.Yer ON BIRIKIM.wms.GorevYer.YerID = BIRIKIM.wms.Yer.ID INNER JOIN
								 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.GorevYer.MalKodu
		WHERE        (BIRIKIM.wms.Gorev.ID = @GorevID)
		GROUP BY BIRIKIM.wms.Gorev.GorevNo, BIRIKIM.wms.Gorev.ID, BIRIKIM.wms.GorevYer.MalKodu, STK.MalAdi, BIRIKIM.wms.GorevYer.Birim, BIRIKIM.wms.IRS.SirketKod, BIRIKIM.wms.GorevYer.Sira, BIRIKIM.wms.Yer.HucreAd
		ORDER BY BIRIKIM.wms.GorevYer.MalKodu
	
	--normalde irs detayı göster
	ELSE
		SELECT        BIRIKIM.wms.Gorev.ID, BIRIKIM.wms.IRS.SirketKod, BIRIKIM.wms.Gorev.GorevNo, BIRIKIM.wms.IRS_Detay.MalKodu, STK.MalAdi, SUM(BIRIKIM.wms.IRS_Detay.Miktar) AS Miktar, BIRIKIM.wms.IRS_Detay.Birim, BIRIKIM.wms.IRS_Detay.MakaraNo, 0 AS Sira, 
								 '' AS HucreAd
		FROM            BIRIKIM.wms.Gorev INNER JOIN
								 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.Gorev.ID = BIRIKIM.wms.GorevIRS.GorevID INNER JOIN
								 BIRIKIM.wms.IRS ON BIRIKIM.wms.GorevIRS.IrsaliyeID = BIRIKIM.wms.IRS.ID INNER JOIN
								 BIRIKIM.wms.IRS_Detay ON BIRIKIM.wms.IRS.ID = BIRIKIM.wms.IRS_Detay.IrsaliyeID INNER JOIN
								 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.IRS_Detay.MalKodu
		WHERE        (BIRIKIM.wms.Gorev.ID = @GorevID)
		GROUP BY BIRIKIM.wms.Gorev.GorevNo, BIRIKIM.wms.Gorev.ID, BIRIKIM.wms.IRS_Detay.MalKodu, STK.MalAdi, BIRIKIM.wms.IRS_Detay.Birim, BIRIKIM.wms.IRS.SirketKod, BIRIKIM.wms.IRS_Detay.MakaraNo
		ORDER BY BIRIKIM.wms.IRS_Detay.MalKodu

END


GO
/****** Object:  StoredProcedure [wms].[getMalKabulAsnSiparisler]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 25.12.2017
-- Last Modify: 25.12.2017
-- Description:	satıcı malkodundan malkodunu getir
-- =============================================
CREATE PROCEDURE [wms].[getMalKabulAsnSiparisler]
	@EvrakNo varchar(50),
	@MalKodu varchar(50),
	@Depo varchar(50)

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        FINSAT633.SPI.ROW_ID, FINSAT633.SPI.EvrakNo, FINSAT633.SPI.Tarih, FINSAT633.SPI.SiraNo, FINSAT633.SPI.BirimMiktar, FINSAT633.STK.Birim1 AS Birim
	FROM            FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.SPI.MalKodu = FINSAT633.STK.MalKodu
	WHERE        (FINSAT633.SPI.IslemTur = 0) AND (FINSAT633.SPI.KynkEvrakTip = 63) AND (FINSAT633.SPI.EvrakNo = @EvrakNo) AND (FINSAT633.SPI.MalKodu = @MalKodu) AND (FINSAT633.SPI.Depo = @Depo)

END
GO
/****** Object:  StoredProcedure [wms].[getMalKabulIrsaliyeList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Last Modify: 25.12.2017
-- Description:	malkabul irsaliye listesi
-- =============================================
CREATE PROCEDURE [wms].[getMalKabulIrsaliyeList]
	@HesapKodu varchar(50),
	@DepoID varchar(50)

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        BIRIKIM.wms.IRS.ID, BIRIKIM.wms.IRS.DepoID, BIRIKIM.wms.IRS.EvrakNo, BIRIKIM.wms.IRS.HesapKodu, CHK.Unvan1 AS Unvan, BIRIKIM.wms.IRS.Tarih, BIRIKIM.wms.IRS.Onay
	FROM            BIRIKIM.wms.IRS INNER JOIN
							 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.FINSAT633.CHK.HesapKodu = BIRIKIM.wms.IRS.HesapKodu
	WHERE        (BIRIKIM.wms.IRS.IslemTur = 0) AND (BIRIKIM.wms.IRS.HesapKodu = @HesapKodu) AND (BIRIKIM.wms.IRS.DepoID = @DepoID)
	ORDER BY BIRIKIM.wms.IRS.ID DESC

END
GO
/****** Object:  StoredProcedure [wms].[getMalKabulSiparisDetail]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 25.12.2017
-- Last Modify: 28.12.2017
-- Description:	malkabul siparişten getirdeki açık sipariş listesi
-- =============================================
CREATE PROCEDURE [wms].[getMalKabulSiparisDetail]
	@ROW_ID int,
	@MalKodu varchar(50),
	@EvrakNo varchar(50)

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        FINSAT633.SPI.EvrakNo, FINSAT633.SPI.Tarih, FINSAT633.SPI.SiraNo, FINSAT633.SPI.MalKodu, FINSAT633.SPI.BirimMiktar, FINSAT633.SPI.BirimMiktar - FINSAT633.SPI.TeslimMiktar - FINSAT633.SPI.KapatilanMiktar AS Miktar, 
							 FINSAT633.SPI.Birim, FINSAT633.SPI.DegisSaat, FINSAT633.STK.Kod1
	FROM            FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.SPI.MalKodu = FINSAT633.STK.MalKodu
	WHERE        (FINSAT633.SPI.IslemTur = 0) AND (FINSAT633.SPI.KynkEvrakTip = 63) AND (FINSAT633.SPI.SiparisDurumu = 0) AND 
							(FINSAT633.SPI.ROW_ID = @ROW_ID) AND (FINSAT633.SPI.MalKodu = @MalKodu) AND (LTRIM(FINSAT633.SPI.EvrakNo) = @EvrakNo)

END
GO
/****** Object:  StoredProcedure [wms].[getMalKabulSiparisList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 25.12.2017
-- Last Modify: 28.12.2017
-- Description:	malkabul siparişten getirdeki açık sipariş listesi
-- =============================================
CREATE PROCEDURE [wms].[getMalKabulSiparisList]
	@HesapKodu varchar(50),
	@DepoID varchar(50)

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        FINSAT633.SPI.ROW_ID AS ID, FINSAT633.SPI.EvrakNo, FINSAT633.SPI.Tarih, FINSAT633.STK.MalAdi, FINSAT633.STK.MalKodu, 
							 FINSAT633.SPI.BirimMiktar - FINSAT633.SPI.TeslimMiktar - FINSAT633.SPI.KapatilanMiktar AS AçıkMiktar, FINSAT633.SPI.Birim
	FROM            FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.SPI.MalKodu = FINSAT633.STK.MalKodu INNER JOIN
							 FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.SPI.Chk = FINSAT633.CHK.HesapKodu INNER JOIN
							 BIRIKIM.wms.Depo ON FINSAT633.SPI.Depo = Depo.DepoKodu
	WHERE        (FINSAT633.SPI.IslemTur = 0) AND (FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.SPI.KynkEvrakTip = 63) AND (Depo.ID = @DepoID) AND (FINSAT633.SPI.Chk = @HesapKodu) AND 
							 (FINSAT633.SPI.ROW_ID NOT IN
								 (SELECT        BIRIKIM.wms.IRS_Detay.KynkSiparisID
								   FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
															 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN
															 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID
								   WHERE        (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 15) AND (NOT (BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL))
								   GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID))

END
GO
/****** Object:  StoredProcedure [wms].[getMalkoduFromSatMalKodu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 25.12.2017
-- Last Modify: 25.12.2017
-- Description:	satıcı malkodundan malkodunu getir
-- =============================================
CREATE PROCEDURE [wms].[getMalkoduFromSatMalKodu]
	@HesapKodu varchar(50),
	@SatMalKodu varchar(50)

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        FINSAT633.TTY.MalKodu, FINSAT633.STK.Birim1, FINSAT633.STK.Kod1
	FROM            FINSAT633.TTY WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.TTY.MalKodu = FINSAT633.STK.MalKodu
	WHERE        (FINSAT633.TTY.SatMalKodu = @SatMalKodu) AND (FINSAT633.TTY.Chk = @HesapKodu)

END
GO
/****** Object:  StoredProcedure [wms].[getMalzemeByCodeOrName]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 25.05.2017
-- Modify date: 14.06.2017
-- Description:	maladı veya malkoduna göre mazlemeyi getirir
-- =============================================
CREATE PROCEDURE [wms].[getMalzemeByCodeOrName]
	@MalKodu varchar(30),
	@MalAdi varchar(50)

AS
BEGIN
	
	if (@MalKodu <> '')
		SELECT        TOP (20) MalKodu AS id, MalKodu + ' ' + MalAdi AS value, MalKodu + ' ' + MalAdi AS label
		FROM            FINSAT633.STK WITH (NOLOCK)
		WHERE        (MalKodu LIKE @MalKodu + '%')
	Else 
		SELECT        TOP (20) MalKodu AS id, MalKodu + ' ' + MalAdi AS value, MalKodu + ' ' + MalAdi AS label
		FROM            FINSAT633.STK WITH (NOLOCK)
		WHERE        (MalAdi LIKE '%' + @MalAdi + '%')

END



GO
/****** Object:  StoredProcedure [wms].[getOlcuList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 29.12.2017
-- Last Modify: 29.12.2017
-- Description:	ölçü tablosunu getirir
-- =============================================
CREATE PROCEDURE [wms].[getOlcuList]

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        BIRIKIM.wms.Olcu.ID, BIRIKIM.wms.Olcu.MalKodu, BIRIKIM.wms.Olcu.Birim, BIRIKIM.wms.Olcu.En, BIRIKIM.wms.Olcu.Boy, BIRIKIM.wms.Olcu.Derinlik, BIRIKIM.wms.Olcu.Agirlik, BIRIKIM.wms.Olcu.Hacim, 
							 FINSAT633.STK.MalAdi AS Kaydeden
	FROM            BIRIKIM.wms.Olcu INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON BIRIKIM.wms.Olcu.MalKodu = FINSAT633.STK.MalKodu
						 
END
GO
/****** Object:  StoredProcedure [wms].[getSayimEksikList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 27.12.2017
-- Last Modify: 27.12.2017
-- Description:	kontrollü sayım eksik listesini getirir
-- =============================================
CREATE PROCEDURE [wms].[getSayimEksikList]
	@ID int

AS
BEGIN

	--depo kodu
	DECLARE @DepoKodu varchar(5)
	SELECT        @DepoKodu = Depo.DepoKodu
	FROM            BIRIKIM.wms.Gorev INNER JOIN
							 BIRIKIM.wms.Depo ON BIRIKIM.wms.Gorev.DepoID = BIRIKIM.wms.Depo.ID
	WHERE        (Gorev.ID = @ID)

	--liste
	SELECT        FINSAT633.FINSAT633.STK.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, FINSAT633.FINSAT633.STK.Birim1 AS Birim, CAST(0 AS DECIMAL) AS Miktar, 
							 ISNULL(FINSAT633.wms.getStockByDepo(FINSAT633.FINSAT633.STK.MalKodu, @DepoKodu), 0) AS GunesStok, ISNULL(BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.FINSAT633.STK.MalKodu, '', 0), 0) AS WmsStok
	FROM            FINSAT633.FINSAT633.STK WITH (NOLOCK) INNER JOIN
							 FINSAT633.FINSAT633.DST WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = FINSAT633.FINSAT633.DST.MalKodu
	WHERE        (FINSAT633.FINSAT633.DST.Depo = @DepoKodu) AND (FINSAT633.wms.getStockByDepo(FINSAT633.FINSAT633.STK.MalKodu, @DepoKodu) <> ISNULL(BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.FINSAT633.STK.MalKodu, '', 0), 0)) AND 
							 (FINSAT633.FINSAT633.STK.MalKodu NOT IN (SELECT MalKodu FROM BIRIKIM.wms.GorevYer WHERE (GorevID = @ID)))


	UNION


	SELECT        Yer.MalKodu, STK.MalAdi, Birim, 0 AS Miktar, ISNULL(FINSAT633.wms.getStockByDepo(Yer.MalKodu, @DepoKodu), 0) AS GunesStok, ISNULL(BIRIKIM.wms.fnGetStock(@DepoKodu, Yer.MalKodu, '', 0), 0) AS WmsStok
	FROM            BIRIKIM.wms.Yer INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.Yer.MalKodu
	GROUP BY Yer.MalKodu, STK.MalAdi, Birim, ISNULL(FINSAT633.wms.getStockByDepo(Yer.MalKodu, @DepoKodu), 0), ISNULL(BIRIKIM.wms.fnGetStock(@DepoKodu, Yer.MalKodu, '', 0), 0)
	HAVING        (NOT (Yer.MalKodu IN
								 (SELECT        MalKodu
								   FROM            BIRIKIM.wms.GorevYer
								   WHERE        (GorevID = @ID)))) AND (ISNULL(BIRIKIM.wms.fnGetStock(@DepoKodu, Yer.MalKodu, '', 0), 0) > 0)


	ORDER BY FINSAT633.FINSAT633.STK.MalKodu

END
GO
/****** Object:  StoredProcedure [wms].[getSayimFisiListesi1]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 27.12.2017
-- Last Modify: 27.12.2017
-- Description:	kontrollü sayım CountCreate mal listesini getirir
-- =============================================
CREATE PROCEDURE [wms].[getSayimFisiListesi1]
	@ID int

AS
BEGIN
	
	SELECT        BIRIKIM.wms.GorevYer.MalKodu, MalAdi, BIRIKIM.wms.GorevYer.Birim, SUM(BIRIKIM.wms.GorevYer.Miktar) AS Miktar,
							 FINSAT633.wms.getStockByDepo(BIRIKIM.wms.GorevYer.MalKodu, BIRIKIM.wms.Depo.DepoKodu) AS GunesStok, 
							 BIRIKIM.wms.fnGetStockByID(BIRIKIM.wms.Gorev.DepoID, BIRIKIM.wms.GorevYer.MalKodu, '') AS WmsStok
	FROM            BIRIKIM.wms.Gorev INNER JOIN
							 BIRIKIM.wms.GorevYer ON BIRIKIM.wms.Gorev.ID = BIRIKIM.wms.GorevYer.GorevID INNER JOIN
							 BIRIKIM.wms.Depo ON BIRIKIM.wms.Gorev.DepoID = BIRIKIM.wms.Depo.ID INNER JOIN
							FINSAT633.FINSAT633.STK WITH (NOLOCK) ON STK.MalKodu = BIRIKIM.wms.GorevYer.MalKodu
	WHERE        (BIRIKIM.wms.Gorev.ID = @ID)
	GROUP BY BIRIKIM.wms.Gorev.DepoID, BIRIKIM.wms.GorevYer.MalKodu, BIRIKIM.wms.GorevYer.Birim, BIRIKIM.wms.Depo.DepoKodu, MalAdi

END
GO
/****** Object:  StoredProcedure [wms].[getSayimFisiListesi2]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 27.12.2017
-- Last Modify: 27.12.2017
-- Description:	kontrollü sayım CountCreate mal listesini getirir
-- =============================================
CREATE PROCEDURE [wms].[getSayimFisiListesi2]
	@ID int,
	@DepoKodu varchar(5)

AS
BEGIN
	
	SELECT        FINSAT633.FINSAT633.STK.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, FINSAT633.FINSAT633.STK.Birim1 AS Birim, CAST(0 AS DECIMAL) AS Miktar, 
							FINSAT633.wms.getStockByDepo(FINSAT633.FINSAT633.STK.MalKodu, @DepoKodu) AS GunesStok, 
							ISNULL(BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.FINSAT633.STK.MalKodu, '', 0), 0) AS WmsStok
	FROM            FINSAT633.FINSAT633.STK INNER JOIN
							 FINSAT633.FINSAT633.DST ON FINSAT633.FINSAT633.STK.MalKodu = FINSAT633.FINSAT633.DST.MalKodu
	WHERE        (FINSAT633.FINSAT633.DST.Depo = @DepoKodu) AND (FINSAT633.wms.getStockByDepo(FINSAT633.FINSAT633.STK.MalKodu, @DepoKodu) <> ISNULL(BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.FINSAT633.STK.MalKodu, '', 0), 0)) AND 
							 (FINSAT633.FINSAT633.STK.MalKodu NOT IN (SELECT MalKodu FROM BIRIKIM.wms.GorevYer WHERE (GorevID = @ID)))

END
GO
/****** Object:  StoredProcedure [wms].[getSayimList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 27.12.2017
-- Last Modify: 27.12.2017
-- Description:	kontrollü sayım tüm ve fark listesini getirir
-- =============================================
CREATE PROCEDURE [wms].[getSayimList]
	@ID int,
	@FarkMi bit

AS
BEGIN
	
	SELECT        MalKodu, ISNULL(MalAdi, '') AS MalAdi, Birim, ISNULL(Miktar, 0) AS Miktar, ISNULL(WmsStok, 0) AS WmsStok, ISNULL(GunesStok, 0) AS GunesStok
	FROM            (SELECT        GorevYer.MalKodu, GorevYer.Birim, SUM(GorevYer.Miktar) AS Miktar, STK.MalAdi,
											FINSAT633.wms.getStockByDepo(GorevYer.MalKodu, Depo.DepoKodu) AS GunesStok, BIRIKIM.wms.fnGetStockByID(Gorev.DepoID, GorevYer.MalKodu, '') AS WmsStok
					FROM            BIRIKIM.wms.Gorev INNER JOIN
											BIRIKIM.wms.Depo ON BIRIKIM.wms.Gorev.DepoID = BIRIKIM.wms.Depo.ID INNER JOIN
											BIRIKIM.wms.GorevYer ON BIRIKIM.wms.Gorev.ID = BIRIKIM.wms.GorevYer.GorevID INNER JOIN
											FINSAT633.FINSAT633.STK WITH (NOLOCK) ON STK.MalKodu = BIRIKIM.wms.GorevYer.MalKodu
					WHERE        (Gorev.ID = @ID)
					GROUP BY Gorev.DepoID, GorevYer.MalKodu, GorevYer.Birim, STK.MalAdi, Depo.DepoKodu) AS t
	WHERE case when @FarkMi=1 then case when (WmsStok <> Miktar) then 1 else 0 end else 1 end = 1
	ORDER BY MalKodu

END
GO
/****** Object:  StoredProcedure [wms].[getSiparis2ListStep2]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 04.01.2018
-- Modify date: 04.01.2018
-- Description:	kablo sipariş step2
-- =============================================
CREATE PROCEDURE [wms].[getSiparis2ListStep2]
	@DepoKodu varchar(5),
	@EvrakNos varchar(MAX)

AS
BEGIN
	
	SELECT        FINSAT633.FINSAT633.SPI.ROW_ID AS ID, FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.KayitSaat AS Saat, 
							 FINSAT633.FINSAT633.SPI.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar AS Miktar, 
							 FINSAT633.FINSAT633.SPI.Birim, FINSAT633.wms.getStockByDepo(FINSAT633.FINSAT633.STK.MalKodu, @DepoKodu) AS GunesStok, BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.FINSAT633.SPI.MalKodu, '', 0) AS WmsStok, 
							 BIRIKIM.wms.fnGetRezervStock(@DepoKodu, FINSAT633.FINSAT633.SPI.MalKodu, '') AS WmsRezerv
	FROM            FINSAT633.FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.FINSAT633.CHK.HesapKodu = FINSAT633.FINSAT633.SPI.Chk INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.MalKodu = FINSAT633.FINSAT633.STK.MalKodu
	WHERE        (FINSAT633.FINSAT633.SPI.Depo = @DepoKodu) AND (FINSAT633.FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.FINSAT633.SPI.EvrakNo IN (SELECT * FROM dbo.splitstring(@EvrakNos))) AND 
							 (FINSAT633.FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND (FINSAT633.FINSAT633.SPI.ROW_ID NOT IN
								 (SELECT        BIRIKIM.wms.IRS_Detay.KynkSiparisID
								   FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
															 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN
															 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID
								   WHERE        (BIRIKIM.wms.Gorev.DurumID = 9) AND (NOT (BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL))
								   GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID))


END
GO
/****** Object:  StoredProcedure [wms].[getSiparis2ListStep3]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 04.01.2018
-- Modify date: 04.01.2018
-- Description:	kablo sipariş step3
-- =============================================
CREATE PROCEDURE [wms].[getSiparis2ListStep3]
	@DepoKodu varchar(5),
	@EvrakNos varchar(MAX),
	@IDs varchar(MAX)

AS
BEGIN
	
	SELECT        FINSAT633.FINSAT633.SPI.ROW_ID, FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.KayitSaat AS Saat, 
							 FINSAT633.FINSAT633.SPI.SiraNo, FINSAT633.FINSAT633.STK.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, FINSAT633.FINSAT633.STK.MalAdi4 AS Marka, FINSAT633.FINSAT633.STK.Nesne2 AS Cins, 
							 FINSAT633.FINSAT633.STK.Kod15 AS Kesit, FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar AS Miktar, BIRIKIM.wms.fnGetStock(@DepoKodu, 
							 FINSAT633.FINSAT633.SPI.MalKodu, FINSAT633.FINSAT633.SPI.Birim, 1) AS Stok
	FROM            FINSAT633.FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.FINSAT633.CHK.HesapKodu = FINSAT633.FINSAT633.SPI.Chk INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.MalKodu = FINSAT633.FINSAT633.STK.MalKodu
	WHERE        (FINSAT633.FINSAT633.SPI.Depo = @DepoKodu) AND (FINSAT633.FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.FINSAT633.SPI.EvrakNo IN (SELECT * FROM dbo.splitstring(@EvrakNos))) AND 
							 (FINSAT633.FINSAT633.SPI.ROW_ID IN (SELECT * FROM dbo.splitstring(@IDs))) AND (FINSAT633.FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND (FINSAT633.FINSAT633.SPI.ROW_ID NOT IN
								 (SELECT        BIRIKIM.wms.IRS_Detay.KynkSiparisID
								   FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
															 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN
															 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID
								   WHERE        (BIRIKIM.wms.Gorev.DurumID = 9) AND (NOT (BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL))
								   GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID))

END
GO
/****** Object:  StoredProcedure [wms].[getSiparis2ListStep4]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 04.01.2018
-- Modify date: 04.01.2018
-- Description:	kablo sipariş step4
-- =============================================
CREATE PROCEDURE [wms].[getSiparis2ListStep4]
	@DepoKodu varchar(5),
	@EvrakNos varchar(MAX),
	@IDs varchar(MAX)

AS
BEGIN
	
	SELECT        FINSAT633.FINSAT633.SPI.ROW_ID, FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.DegisSaat, FINSAT633.FINSAT633.SPI.SiraNo, FINSAT633.FINSAT633.STK.MalKodu, 
							 FINSAT633.FINSAT633.STK.MalAdi4, FINSAT633.FINSAT633.STK.Nesne2, FINSAT633.FINSAT633.STK.Kod15, FINSAT633.FINSAT633.SPI.Chk, FINSAT633.FINSAT633.SPI.Birim, 
							 FINSAT633.FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.FINSAT633.SPI.BirimMiktar, FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar AS Miktar, 
							 FINSAT633.FINSAT633.SPI.ValorGun, FINSAT633.FINSAT633.SPI.TeslimChk
	FROM            FINSAT633.FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.MalKodu = FINSAT633.FINSAT633.STK.MalKodu INNER JOIN
							 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.Chk = FINSAT633.FINSAT633.CHK.HesapKodu INNER JOIN
							 FINSAT633.FINSAT633.MFK WITH (NOLOCK) ON FINSAT633.FINSAT633.MFK.EvrakNo = FINSAT633.FINSAT633.SPI.EvrakNo AND FINSAT633.FINSAT633.MFK.KynkEvrakTip = FINSAT633.FINSAT633.SPI.KynkEvrakTip AND 
							 FINSAT633.FINSAT633.MFK.HesapKod = FINSAT633.FINSAT633.SPI.Chk
	WHERE        (FINSAT633.FINSAT633.SPI.Depo = @DepoKodu) AND (FINSAT633.FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.FINSAT633.SPI.EvrakNo IN (SELECT * FROM dbo.splitstring(@EvrakNos))) AND 
							 (FINSAT633.FINSAT633.SPI.ROW_ID IN (SELECT * FROM dbo.splitstring(@IDs))) AND (FINSAT633.FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı'))

END
GO
/****** Object:  StoredProcedure [wms].[getSiparisDetail]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 09.06.2017
-- Last Modify: 05.01.2018
-- Description:	genel sipariş ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[getSiparisDetail]
	@EvrakNo varchar(16),
	@Depo varchar(5)

AS
BEGIN
	
	SELECT        FINSAT633.SPI.MalKodu, FINSAT633.STK.MalAdi, FINSAT633.SPI.BirimMiktar - FINSAT633.SPI.KapatilanMiktar - FINSAT633.SPI.TeslimMiktar AS Miktar, FINSAT633.SPI.Birim,
					wms.getStockByDepo(FINSAT633.STK.MalKodu,FINSAT633.SPI.Depo) as Stok,
					BIRIKIM.wms.fnGetStock(FINSAT633.SPI.Depo, FINSAT633.STK.MalKodu, '', 0) AS WmsStok, 
					case when ((SELECT        TOP 1 BIRIKIM.wms.IRS_Detay.ID
					FROM            BIRIKIM.wms.Gorev INNER JOIN
											 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.Gorev.ID = BIRIKIM.wms.GorevIRS.GorevID INNER JOIN
											 BIRIKIM.wms.Depo ON BIRIKIM.wms.Gorev.DepoID = BIRIKIM.wms.Depo.ID INNER JOIN
											 BIRIKIM.wms.IRS_Detay ON BIRIKIM.wms.GorevIRS.IrsaliyeID = BIRIKIM.wms.IRS_Detay.IrsaliyeID
					WHERE        (BIRIKIM.wms.Depo.DepoKodu = FINSAT633.SPI.Depo) AND (BIRIKIM.wms.IRS_Detay.MalKodu = FINSAT633.SPI.MalKodu) AND (BIRIKIM.wms.IRS_Detay.KynkSiparisID = FINSAT633.SPI.ROW_ID) 
									AND (BIRIKIM.wms.Gorev.DurumID = 9 OR BIRIKIM.wms.Gorev.DurumID = 11))) IS NULL then '' else 'Planlanmış' end as HucreAd
	FROM            FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.SPI.MalKodu = FINSAT633.STK.MalKodu LEFT OUTER JOIN
							 FINSAT633.DST WITH (NOLOCK) ON FINSAT633.STK.MalKodu = FINSAT633.DST.MalKodu AND FINSAT633.DST.Depo = FINSAT633.SPI.Depo
	WHERE        (FINSAT633.SPI.EvrakNo = @EvrakNo) AND (FINSAT633.SPI.Depo = @Depo) AND (FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı'))

END
GO
/****** Object:  StoredProcedure [wms].[getSiparisList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 23.11.2017
-- Modify date: 21.12.2017
-- Description:	kablo veya genel sipariş
-- =============================================
CREATE PROCEDURE [wms].[getSiparisList]
	@DepoKodu varchar(5),
	@isKable bit,
	@BasTarih int,
	@BitTarih int

AS
BEGIN
	
	SELECT        FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.Chk, FINSAT633.FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.FINSAT633.CHK.GrupKod, 
							 FINSAT633.FINSAT633.CHK.FaturaAdres3 AS FaturaAdres, FINSAT633.FINSAT633.MFK.Aciklama, COUNT(FINSAT633.FINSAT633.SPI.MalKodu) AS Çeşit, 
							 SUM(FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar) AS Miktar, MIN(FINSAT633.FINSAT633.SPI.KayitSaat) AS Saat
	FROM            FINSAT633.FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.MalKodu = FINSAT633.FINSAT633.STK.MalKodu INNER JOIN
							 FINSAT633.FINSAT633.MFK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.EvrakNo = FINSAT633.FINSAT633.MFK.EvrakNo AND FINSAT633.FINSAT633.SPI.Tarih = FINSAT633.FINSAT633.MFK.EvrakTarih AND 
							 FINSAT633.FINSAT633.SPI.Chk = FINSAT633.FINSAT633.MFK.HesapKod AND FINSAT633.FINSAT633.SPI.KynkEvrakTip = FINSAT633.FINSAT633.MFK.KynkEvrakTip INNER JOIN
							 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.Chk = FINSAT633.FINSAT633.CHK.HesapKodu
	WHERE        (FINSAT633.FINSAT633.SPI.Depo = @DepoKodu) AND (FINSAT633.FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND 
							(FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar) > 0 AND
							case when @isKable=1 then 
								case when (FINSAT633.FINSAT633.STK.Kod1 = 'KKABLO') then 1 else 0 end 
							else 
								case when (FINSAT633.FINSAT633.STK.Kod1 <> 'KKABLO') then 1 else 0 end 
							end = 1 AND 
							case when @BasTarih > 0 then case when (FINSAT633.FINSAT633.SPI.Tarih >= @BasTarih) then 1 else 0 end else 1 end = 1 AND 
							case when @BasTarih > 0 then case when (FINSAT633.FINSAT633.SPI.Tarih <= @BitTarih) then 1 else 0 end else 1 end = 1 AND
							FINSAT633.FINSAT633.SPI.ROW_ID NOT IN (SELECT        BIRIKIM.wms.IRS_Detay.KynkSiparisID
																	FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
																							 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN
																							 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID
																	WHERE        (BIRIKIM.wms.Gorev.DurumID = 9) AND (NOT (BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL))
																	GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID)
	GROUP BY FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.Chk, FINSAT633.FINSAT633.CHK.Unvan1, FINSAT633.FINSAT633.CHK.GrupKod, FINSAT633.FINSAT633.CHK.FaturaAdres3, FINSAT633.FINSAT633.MFK.Aciklama

END

GO
/****** Object:  StoredProcedure [wms].[getSiparisListStep2]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Modify date: 22.12.2017
-- Description:	kablo veya genel sipariş step2
-- =============================================
CREATE PROCEDURE [wms].[getSiparisListStep2]
	@DepoKodu varchar(5),
	@EvrakNos varchar(MAX)

AS
BEGIN
	
	SELECT        FINSAT633.SPI.MalKodu, FINSAT633.STK.MalAdi, SUM(FINSAT633.SPI.BirimMiktar - FINSAT633.SPI.TeslimMiktar - FINSAT633.SPI.KapatilanMiktar) AS Miktar, FINSAT633.SPI.Birim, 
							 wms.getStockByDepo(FINSAT633.STK.MalKodu, @DepoKodu) AS Stok, BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.SPI.MalKodu, '', 0) AS WmsStok, BIRIKIM.wms.fnGetRezervStock(@DepoKodu, FINSAT633.SPI.MalKodu, '') AS WmsRezerv
	FROM            FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.SPI.MalKodu = FINSAT633.STK.MalKodu LEFT OUTER JOIN
							 FINSAT633.DST WITH (NOLOCK) ON FINSAT633.STK.MalKodu = FINSAT633.DST.MalKodu AND FINSAT633.DST.Depo = @DepoKodu
	WHERE        (FINSAT633.SPI.EvrakNo IN (SELECT * FROM dbo.splitstring(@EvrakNos))) AND (FINSAT633.SPI.Depo = @DepoKodu) AND (FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND (FINSAT633.SPI.ROW_ID NOT IN
								 (SELECT        BIRIKIM.wms.IRS_Detay.KynkSiparisID
								   FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
															 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN
															 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.Gorev.ID
								   WHERE        (BIRIKIM.wms.Gorev.DurumID = 9) AND (NOT (BIRIKIM.wms.IRS_Detay.KynkSiparisID IS NULL))
								   GROUP BY BIRIKIM.wms.IRS_Detay.KynkSiparisID))
	GROUP BY FINSAT633.SPI.MalKodu, FINSAT633.STK.MalAdi, FINSAT633.SPI.Birim, wms.getStockByDepo(FINSAT633.STK.MalKodu, @DepoKodu), BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.SPI.MalKodu, '', 
							 0), BIRIKIM.wms.fnGetRezervStock(@DepoKodu, FINSAT633.SPI.MalKodu, '')

END

GO
/****** Object:  StoredProcedure [wms].[getSiparisListStep3]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Modify date: 30.01.2018
-- Description:	kablo veya genel sipariş step3
-- =============================================
CREATE PROCEDURE [wms].[getSiparisListStep3]
	@DepoKodu varchar(5),
	@EvrakNos varchar(MAX),
	@MalKodus varchar(MAX)

AS
BEGIN
	
	SELECT        FINSAT633.FINSAT633.SPI.ROW_ID AS ID, FINSAT633.FINSAT633.SPI.EvrakNo, FINSAT633.FINSAT633.SPI.Tarih, FINSAT633.FINSAT633.SPI.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, 
							 FINSAT633.FINSAT633.SPI.BirimMiktar - FINSAT633.FINSAT633.SPI.TeslimMiktar - FINSAT633.FINSAT633.SPI.KapatilanMiktar AS Miktar, FINSAT633.FINSAT633.SPI.Birim, 
							 BIRIKIM.wms.fnGetStock(@DepoKodu, FINSAT633.FINSAT633.SPI.MalKodu, '', 1) AS WmsStok
	FROM            FINSAT633.FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.SPI.MalKodu = FINSAT633.FINSAT633.STK.MalKodu
	WHERE        (FINSAT633.FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND (FINSAT633.FINSAT633.SPI.Depo = @DepoKodu) AND 
							 (FINSAT633.FINSAT633.SPI.EvrakNo IN (SELECT * FROM dbo.splitstring(@EvrakNos))) AND (FINSAT633.FINSAT633.SPI.MalKodu IN (SELECT * FROM dbo.splitstring(@MalKodus))) AND
								 ((SELECT        SUM(Miktar) AS Expr1
									 FROM            BIRIKIM.wms.Yer AS Yer_2 WITH (NOLOCK)
									 WHERE        (MalKodu = FINSAT633.FINSAT633.SPI.MalKodu)) IS NOT NULL) AND (FINSAT633.FINSAT633.SPI.ROW_ID NOT IN
								 (SELECT        IRS_Detay.KynkSiparisID
								   FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
															 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = GorevIRS.IrsaliyeID INNER JOIN
															 BIRIKIM.wms.Gorev ON BIRIKIM.wms.GorevIRS.GorevID = Gorev.ID
								   WHERE        (Gorev.DurumID = 9) AND (NOT (IRS_Detay.KynkSiparisID IS NULL))
								   GROUP BY IRS_Detay.KynkSiparisID))
	ORDER BY FINSAT633.FINSAT633.SPI.MalKodu

END

GO
/****** Object:  StoredProcedure [wms].[getSiparisListStep4]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Modify date: 20.02.2018
-- Description:	kablo veya genel sipariş step4
-- =============================================
CREATE PROCEDURE [wms].[getSiparisListStep4]
	@DepoKodu varchar(5),
	@EvrakNos varchar(MAX),
	@MalKodus varchar(MAX),
	@IDs varchar(MAX)

AS
BEGIN
	
	SELECT        FINSAT633.MFK.Aciklama, FINSAT633.SPI.ROW_ID, FINSAT633.SPI.EvrakNo, FINSAT633.SPI.Tarih, FINSAT633.SPI.KayitSaat AS Saat, FINSAT633.SPI.DegisSaat, FINSAT633.SPI.SiraNo, FINSAT633.SPI.Chk, 
							 FINSAT633.SPI.MalKodu, FINSAT633.SPI.Birim, FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.SPI.BirimMiktar, FINSAT633.SPI.BirimMiktar - FINSAT633.SPI.TeslimMiktar - FINSAT633.SPI.KapatilanMiktar AS Miktar, 
							 FINSAT633.SPI.ValorGun, FINSAT633.SPI.TeslimChk
	FROM            FINSAT633.SPI WITH (NOLOCK) INNER JOIN
							 FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.SPI.Chk = FINSAT633.CHK.HesapKodu INNER JOIN
							 FINSAT633.MFK WITH (NOLOCK) ON FINSAT633.MFK.EvrakNo = FINSAT633.SPI.EvrakNo AND FINSAT633.MFK.KynkEvrakTip = FINSAT633.SPI.KynkEvrakTip
	WHERE        (FINSAT633.SPI.SiparisDurumu = 0) AND (FINSAT633.SPI.KynkEvrakTip = 62) AND (FINSAT633.SPI.Kod10 IN ('Terminal', 'Onaylandı')) AND 
							(FINSAT633.SPI.Depo = @DepoKodu) AND (FINSAT633.SPI.EvrakNo IN (SELECT * FROM dbo.splitstring(@EvrakNos))) AND 
							(FINSAT633.SPI.ROW_ID IN (SELECT * FROM dbo.splitstring(@IDs))) AND (FINSAT633.SPI.MalKodu IN (SELECT * FROM dbo.splitstring(@MalKodus)))
	GROUP BY FINSAT633.MFK.Aciklama, FINSAT633.SPI.ROW_ID, FINSAT633.SPI.EvrakNo, FINSAT633.SPI.Tarih, FINSAT633.SPI.KayitSaat, FINSAT633.SPI.DegisSaat, FINSAT633.SPI.SiraNo, FINSAT633.SPI.Chk, FINSAT633.SPI.MalKodu, 
		                     FINSAT633.SPI.Birim, FINSAT633.CHK.Unvan1, FINSAT633.SPI.BirimMiktar, FINSAT633.SPI.BirimMiktar - FINSAT633.SPI.TeslimMiktar - FINSAT633.SPI.KapatilanMiktar, FINSAT633.SPI.ValorGun, 
			                 FINSAT633.SPI.TeslimChk
	ORDER BY FINSAT633.SPI.Tarih, Saat

END
GO
/****** Object:  StoredProcedure [wms].[getSiparisListStep41]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Last Modify: 22.12.2017
-- Description:	satış step4 iç kodlar
-- =============================================
CREATE PROCEDURE [wms].[getSiparisListStep41]
	@GorevID int

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        IRS_Detay.MalKodu, STK.MalAdi, SUM(IRS_Detay.Miktar) AS Miktar, IRS_Detay.Birim
	FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
							 BIRIKIM.wms.GorevIRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.GorevIRS.IrsaliyeID INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.IRS_Detay.MalKodu
	WHERE        (GorevIRS.GorevID = @GorevID)
	GROUP BY IRS_Detay.MalKodu, STK.MalAdi, IRS_Detay.Birim

END
GO
/****** Object:  StoredProcedure [wms].[getSiparisListStep42]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Last Modify: 03.04.2018
-- Description:	satış step4 iç kodlar
-- =============================================
CREATE PROCEDURE [wms].[getSiparisListStep42]
	@GorevID int

AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT        Yer.HucreAd, GorevYer.MalKodu, STK.MalAdi, GorevYer.Miktar, GorevYer.Birim, GorevYer.MakaraNo, ISNULL(GorevYer.Sira, 0) as Sira, Yer.Miktar AS Stok
	FROM            BIRIKIM.wms.GorevYer INNER JOIN
							 BIRIKIM.wms.Yer ON BIRIKIM.wms.GorevYer.YerID = BIRIKIM.wms.Yer.ID INNER JOIN
							 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.GorevYer.MalKodu
	WHERE        (GorevYer.GorevID = @GorevID)
	ORDER BY GorevYer.Sira

END
GO
/****** Object:  StoredProcedure [wms].[getStkOzet]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 28.12.2017
-- Modify date: 05.04.2018
-- Description:	stkdan özet bilgi
-- =============================================
CREATE PROCEDURE [wms].[getStkOzet]
	@Malkodu varchar(50),
	@Barkod varchar(50)

AS
BEGIN
	
	SELECT        MalKodu, MalAdi, Birim1 AS Birim, Kod1, ';' + Barkod1 + ';' + Barkod2 + ';' + Barkod3 + ';' AS Barkod
	FROM            FINSAT633.STK
	WHERE        (MalAdi <> '') AND 
				case when @Malkodu <> '' then case when MalKodu = @Malkodu then 1 else 0 end else 1 end = 1 AND 
				case when @Barkod <> '' then case when (BarKod1 = @Barkod) OR (BarKod2 = @Barkod) OR (BarKod3 = @Barkod) then 1 else 0 end else 1 end = 1

END
GO
/****** Object:  StoredProcedure [wms].[GetStockSearchByCode]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 30.03.2018
-- Modify date: 30.03.2018
-- Description:	WMS/Stock sayfasında malzeme bazlı arama ilk sorgu
-- =============================================
CREATE PROCEDURE [wms].[GetStockSearchByCode]
	@MalKodu varchar(30)

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        wms.fnGetRezervStock(wms.Depo.DepoKodu, wms.Yer.MalKodu, '') AS WmsRezerv, wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, SUM(wms.Yer.Miktar) AS Miktar
	FROM            wms.Yer INNER JOIN
							 wms.Depo ON wms.Yer.DepoID = wms.Depo.ID
	WHERE        (wms.Yer.MalKodu = @MalKodu)
	GROUP BY wms.Depo.DepoAd, wms.Depo.ID, wms.Yer.MalKodu, wms.fnGetRezervStock(wms.Depo.DepoKodu, wms.Yer.MalKodu, '')

END
GO
/****** Object:  StoredProcedure [wms].[getStokDetay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 22.12.2017
-- Modify date: 04.01.2018
-- Description:	stok detay sayfası
-- =============================================
CREATE PROCEDURE [wms].[getStokDetay]
	@IrsaliyeNo int

AS
BEGIN
	
	IF EXISTS (SELECT IrsaliyeID FROM BIRIKIM.wms.IRS_Detay WHERE (IrsaliyeID = @IrsaliyeNo))
		SELECT        BIRIKIM.wms.IRS.SirketKod, BIRIKIM.wms.IRS.IslemTur, BIRIKIM.wms.IRS.EvrakNo, BIRIKIM.wms.IRS.HesapKodu, ISNULL(FINSAT633.FINSAT633.CHK.Unvan1, '') AS Unvan, BIRIKIM.wms.IRS.Tarih, BIRIKIM.wms.IRS_Detay.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, 
								 BIRIKIM.wms.IRS_Detay.Miktar, BIRIKIM.wms.IRS_Detay.Birim
		FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
								 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.IRS_Detay.MalKodu INNER JOIN
								 BIRIKIM.wms.IRS ON BIRIKIM.wms.IRS_Detay.IrsaliyeID = BIRIKIM.wms.IRS.ID LEFT OUTER JOIN
								 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON BIRIKIM.wms.IRS.HesapKodu = FINSAT633.FINSAT633.CHK.HesapKodu
		WHERE        (BIRIKIM.wms.IRS_Detay.IrsaliyeID = @IrsaliyeNo)
	ELSE
		SELECT        BIRIKIM.wms.IRS.SirketKod, BIRIKIM.wms.IRS.IslemTur, BIRIKIM.wms.IRS.EvrakNo, BIRIKIM.wms.IRS.HesapKodu, ISNULL(FINSAT633.FINSAT633.CHK.Unvan1, '') AS Unvan, BIRIKIM.wms.IRS.Tarih, BIRIKIM.wms.GorevYer.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, 
								 BIRIKIM.wms.GorevYer.Miktar, BIRIKIM.wms.GorevYer.Birim
		FROM            BIRIKIM.wms.GorevIRS INNER JOIN
								 BIRIKIM.wms.GorevYer ON BIRIKIM.wms.GorevIRS.GorevID = BIRIKIM.wms.GorevYer.GorevID INNER JOIN
								 FINSAT633.FINSAT633.STK WITH (NOLOCK) ON FINSAT633.FINSAT633.STK.MalKodu = BIRIKIM.wms.GorevYer.MalKodu INNER JOIN
								 BIRIKIM.wms.IRS ON BIRIKIM.wms.GorevIRS.IrsaliyeID = BIRIKIM.wms.IRS.ID LEFT OUTER JOIN
								 FINSAT633.FINSAT633.CHK WITH (NOLOCK) ON BIRIKIM.wms.IRS.HesapKodu = FINSAT633.FINSAT633.CHK.HesapKodu
		WHERE        (BIRIKIM.wms.GorevIRS.IrsaliyeID = @IrsaliyeNo)

END
GO
/****** Object:  StoredProcedure [wms].[GunlukSatisAnaliziKriterSelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[GunlukSatisAnaliziKriterSelect]
AS
BEGIN

	SELECT 'TÜMÜ' as Kriter,'a' as Flag
	UNION 
	SELECT DISTINCT MalAdi4 as Kriter,'b' as Flag FROM FINSAT633.FINSAT633.STK(NOLOCK)
	WHERE GrupKod IS NOT NULL --AND STK.GrupKod IN('PARKE','MDF','MDFLAM','SLAM','YLEVHA','SÜPÜRGELİK','ŞİLTE') 
	Order BY Flag,Kriter

END
GO
/****** Object:  StoredProcedure [wms].[GunlukSatisRaporu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[GunlukSatisRaporu]
@BasTarih INT,
@BitTarih INT
AS
BEGIN
	
SELECT STI.Chk, STI.Chk AS Chk2,
CHK.Unvan1+' '+CHK.Unvan2 AS Unvan, ISST.Kod1 AS BaglantiTipi, ISST.ListeNo AS BaglantiNo,  
(
	SELECT Aciklama
	FROM SOLAR6.DBO.KTK (NOLOCK)
	WHERE Tip = 8 AND Kod = CHK.OzelKod
) AS SatisTemsilcisi, STK.GrupKod, STI.MalKodu, STK.MalAdi, STK.TipKod AS Kalite,  
CONVERT(DATETIME, STI.Tarih-2) AS IslemTarihi, 
STI.EvrakNo, STI.Birim, STI.BirimMiktar, STK.Birim1 AS AnaBirim,
(CASE WHEN STI.Birim = STK.Birim2 THEN STI.BirimMiktar * STK.KatSayi2 ELSE STI.BirimMiktar END) AS AnaBirimMiktar, 
(CASE WHEN STI.Birim in ('ADET','AD') THEN STI.BirimMiktar ELSE 0 END) AS AdetCikisMiktar, 
(CASE WHEN STI.Birim in ('M2') THEN STI.BirimMiktar ELSE 0 END) AS M2CikisMiktar, 
(CASE WHEN STI.Birim in ('KĞ','KG') THEN STI.BirimMiktar ELSE 0 END) AS KGCikisMiktar, 
(CASE WHEN STK.Birim2 in ('M3') THEN CONVERT(NUMERIC(24,6), STI.BirimMiktar*STK.KatSayi2) ELSE 0 END) AS M3CikisMiktar, 
(CASE WHEN STI.Birim in ('MTR', 'METR', 'MT') THEN STI.BirimMiktar ELSE 0 END) AS MTCikisMiktar, 
(CASE WHEN STI.BirimMiktar = 0 THEN 0 ELSE ((STI.Tutar-STI.ToplamIskonto)/STI.BirimMiktar) END) AS NetBirimFiyat, 
(STI.Tutar-STI.ToplamIskonto) AS NetTutar, STI.KDV, (STI.Tutar-STI.ToplamIskonto) + STI.KDV AS GenelToplam
FROM FINSAT633.FINSAT633.STI(NOLOCK) STI 
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON STI.Chk=CHK.HesapKodu
INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STI.MalKodu=STK.MalKodu
LEFT JOIN
(SELECT Distinct Kod1, ListeNo FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK)) AS ISST ON STI.Kod5 = ISST.ListeNo 
WHERE STI.KynkEvrakTip=1 AND STI.Tarih BETWEEN @BasTarih AND @BitTarih
ORDER BY STI.Tarih

END








GO
/****** Object:  StoredProcedure [wms].[JobCacheSteps]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 19.01.2018
-- Last Modify: 19.01.2018
-- Description:	cache job steps
-- =============================================
CREATE PROCEDURE [wms].[JobCacheSteps]

AS
BEGIN
	SET NOCOUNT ON;

	--delete
	DELETE FROM BIRIKIM_LOG.dbo.DB_Aylik_SatisAnalizi WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_Aylik_SatisAnalizi_Tip_Kod_Doviz WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_GunlukSatisAnaliziYearToDay WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_BekleyenSiparis_UrunGrubu_Fiyat WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_BakiyeRiskAnalizi WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_LokasyonBazli_SatisAnalizi WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_LokasyonBazli_SatisAnalizi_Kriter WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_BekleyenSiparis_UrunGrubu_Miktar WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_UrunGrubu_SatisAnalizi WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_UrunGrubu_SatisAnalizi_Kriter WHERE DB = '33'
	DELETE FROM BIRIKIM_LOG.dbo.DB_SatisBaglanti_UrunGrubu WHERE DB = '33'
	-----------------------------------------------------------------------------------------------------
	-- DB_Aylik_SatisAnalizi
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_Aylik_SatisAnalizi (DB, Ay, Yil2015, Yil2016, Yil2017)
	EXEC [FINSAT633].[wms].[DB_Aylik_SatisAnalizi]
	-----------------------------------------------------------------------------------------------------
	-- DB_Aylik_SatisAnalizi_Tip_Kod_Doviz
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_Aylik_SatisAnalizi_Tip_Kod_Doviz (DB, Grup, Kriter, IslemTip, Ay, Yil2015, Yil2016, Yil2017)
	EXEC [FINSAT633].[wms].[DB_Aylik_SatisAnalizi_Tip_Kod_Doviz] @Grup = N'TÜMÜ', @Kriter = N'TL', @IslemTip = 0
	-----------------------------------------------------------------------------------------------------
	-- DB_GunlukSatisAnaliziYearToDay
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_GunlukSatisAnaliziYearToDay (DB, CHK, Unvan, GenelTutar)
	EXEC [FINSAT633].[wms].[DB_GunlukSatisAnaliziYearToDay]
	-----------------------------------------------------------------------------------------------------
	-- DB_BekleyenSiparis_UrunGrubu_Fiyat
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_BekleyenSiparis_UrunGrubu_Fiyat (DB, GrupKod, KalanMiktar)
	EXEC [FINSAT633].[wms].DB_BekleyenSiparis_UrunGrubu_Fiyat
	-----------------------------------------------------------------------------------------------------
	-- DB_BakiyeRiskAnalizi
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_BakiyeRiskAnalizi (DB, HesapKodu, Unvan, Borc, Alacak, Bakiye)
	EXEC [FINSAT633].[wms].DB_BakiyeRiskAnalizi
	-----------------------------------------------------------------------------------------------------
	-- DB_BekleyenSiparis_UrunGrubu_Miktar
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_BekleyenSiparis_UrunGrubu_Miktar (DB, GrupKod, KalanMiktar)
	EXEC [FINSAT633].[wms].DB_BekleyenSiparis_UrunGrubu_Miktar
	-----------------------------------------------------------------------------------------------------
	-- DB_UrunGrubu_SatisAnalizi
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_UrunGrubu_SatisAnalizi (DB, Ay, GrupKod, Yil2016, Yil2017, Toplam)
	EXEC [FINSAT633].[wms].[DB_UrunGrubu_SatisAnalizi] @Ay = 0
	-----------------------------------------------------------------------------------------------------
	-- DB_UrunGrubu_SatisAnalizi_Kriter
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_UrunGrubu_SatisAnalizi_Kriter (DB, Ay, Kriter, GrupKod, Yil2016, Yil2017, Toplam)
	EXEC [FINSAT633].[wms].[DB_UrunGrubu_SatisAnalizi_Kriter] @Ay = 0, @Kriter = N'TL'
	-----------------------------------------------------------------------------------------------------
	-- DB_UrunGrubu_SatisAnalizi_Kriter
	-----------------------------------------------------------------------------------------------------
	INSERT INTO BIRIKIM_LOG.dbo.DB_SatisBaglanti_UrunGrubu (DB, MalKod, KalanTutar)
	EXEC [FINSAT633].[wms].DB_SatisBaglanti_UrunGrubu
	-----------------------------------------------------------------------------------------------------

END
GO
/****** Object:  StoredProcedure [wms].[KampanyaliSatisRaporu]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[KampanyaliSatisRaporu]
@BasTarih INT,
@BitTarih INT
AS
BEGIN

SELECT 
* ,
'PALET' as ComfortBirim,
'PALET' as ExculusiveBirim,
'PALET' as PeliNeroFloorBirim,
'PALET' as GoldenBirim,
'PALET' as LoftBirim,
'PALET' as VintageBirim,
'PALET' as WoodBirim,
'PAKET' as SupurgelikBirim,
(C.ComfortMiktar+ C.ExculusiveMiktar) as HakKazanilan6cmSup,
(C.PeliNeroFloorMiktar+ C.GoldenMiktar+C.LoftMiktar+ C.VintageMiktar+ C.WoodMiktar) as HakKazanilan8cmSup

FROM (
SELECT 
B.Chk,
SUM(B.PeliNeroFloorMiktar) as PeliNeroFloorMiktar,
SUM(B.PeliNeroFloorTeslimMiktar) as PeliNeroFloorTeslimMiktar,
SUM(B.PeliNeroFloorTutar) as PeliNeroFloorTutar,
SUM(B.ComfortMiktar) as ComfortMiktar,
SUM(B.ComfortTeslimMiktar) as ComfortTeslimMiktar,
SUM(B.ComfortTutar) as ComfortTutar,
SUM(B.ExculusiveMiktar) as ExculusiveMiktar,
SUM(B.ExculusiveTeslimMiktar) as ExculusiveTeslimMiktar,
SUM(B.ExculusiveTutar) as ExculusiveTutar,
SUM(B.GoldenMiktar) as GoldenMiktar,
SUM(B.GoldenTeslimMiktar) as GoldenTeslimMiktar,
SUM(B.GoldenTutar) as GoldenTutar,
SUM(B.LoftMiktar) as LoftMiktar,
SUM(B.LoftTeslimMiktar) as LoftTeslimMiktar,
SUM(B.LoftTutar) as LoftTutar,
SUM(B.VintageMiktar) as VintageMiktar,
SUM(B.VintageTeslimMiktar) as VintageTeslimMiktar,
SUM(B.VintageTutar) as VintageTutar,
SUM(B.WoodMiktar) as WoodMiktar,
SUM(B.WoodTeslimMiktar) as WoodTeslimMiktar,
SUM(B.WoodTutar) as WoodTutar,
SUM(B.Bedelsiz6CmSupMiktar) as Bedelsiz6CmSupMiktar,
SUM(B.Bedelsiz6CmSupTeslimMiktar) as Bedelsiz6CmSupTeslimMiktar,
SUM(B.Bedelsiz6CmSupTutar) as Bedelsiz6CmSupTutar,
SUM(B.Bedelsiz8CmSupMiktar) as Bedelsiz8CmSupMiktar,
SUM(B.Bedelsiz8CmSupTeslimMiktar) as Bedelsiz8CmSupTeslimMiktar,
SUM(B.Bedelsiz8CmSupTutar) as Bedelsiz8CmSupTutar,
SUM(B.Bedelli6CmSupMiktar) as Bedelli6CmSupMiktar,
SUM(B.Bedelli6CmSupTeslimMiktar) as Bedelli6CmSupTeslimMiktar,
SUM(B.Bedelli6CmSupTutar) as Bedelli6CmSupTutar,
SUM(B.Bedelli8CmSupMiktar) as Bedelli8CmSupMiktar,
SUM(B.Bedelli8CmSupTeslimMiktar) as Bedelli8CmSupTeslimMiktar,
SUM(B.Bedelli8CmSupTutar) as Bedelli8CmSupTutar
FROM(
	SELECT 
	A.CHK,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='PELİ NEROFLOOR' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS PeliNeroFloorMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='PELİ NEROFLOOR' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS PeliNeroFloorTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='PELİ NEROFLOOR' THEN SUM(A.Tutar) END),0)  AS PeliNeroFloorTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='COMFORT' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS ComfortMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='COMFORT' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS ComfortTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='COMFORT' THEN SUM(A.Tutar) END),0)  AS ComfortTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='EXCLUSIVE' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS ExculusiveMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='EXCLUSIVE' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS ExculusiveTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='EXCLUSIVE' THEN SUM(A.Tutar) END),0)  AS ExculusiveTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='GOLDEN' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS GoldenMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='GOLDEN' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS GoldenTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='GOLDEN' THEN SUM(A.Tutar) END),0)  AS GoldenTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='LOFT' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS LoftMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='LOFT' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS LoftTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='LOFT' THEN SUM(A.Tutar)END),0)  AS LoftTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='VİNTAGE' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS VintageMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='VİNTAGE' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS VintageTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='VİNTAGE' THEN SUM(A.Tutar) END),0)  AS VintageTutar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='WOOD' THEN ROUND(SUM(A.Miktar)/(109.805),0) END),0)  AS WoodMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='WOOD' THEN ROUND(SUM(A.TeslimMiktar)/(109.805),0) END),0)  AS WoodTeslimMiktar,
	ISNULL((CASE WHEN (A.GrupKod='PARKE' OR A.GrupKod='TM') AND A.Koleksiyon='WOOD' THEN SUM(A.Tutar) END),0)  AS WoodTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelsiz6CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelsiz6CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN SUM(A.Tutar) END),0)  AS Bedelsiz6CmSupTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelsiz8CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelsiz8CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum='Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN SUM(A.Tutar) END),0)  AS Bedelsiz8CmSupTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelli6CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelli6CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='6CM' THEN ROUND(SUM(A.Tutar)/(78.4 ),0) END),0)  AS Bedelli6CmSupTutar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.Miktar)/(78.4 ),0) END),0)  AS Bedelli8CmSupMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN ROUND(SUM(A.TeslimMiktar)/(78.4 ),0) END),0)  AS Bedelli8CmSupTeslimMiktar,
	ISNULL((CASE WHEN A.GrupKod='SÜPÜRGELİK' AND A.Koleksiyon='SÜPÜRGELİK' AND A.Durum<>'Bedelsiz, Promosyon Ürünüdür' AND A.SUPTIP='8CM' THEN SUM(A.Tutar) END),0)  AS Bedelli8CmSupTutar
	FROM(
		SELECT 
			SPI.Chk,
			ROUND(SUM(SPI.Tutar),0) as Tutar,
			ROUND(SUM(SPI.Miktar),0) as Miktar,
			ROUND(SUM(SPI.TeslimMiktar),0) as TeslimMiktar,
			SPI.Nesne1 as Durum,
			STK.GrupKod ,
			STK.Kod4 AS Koleksiyon,
			(CASE WHEN SPI.MalKodu LIKE '89850978%' THEN '6CM' WHEN SPI.MalKodu LIKE '8985095%' THEN '8CM' ELSE '' END) AS SUPTIP
		FROM FINSAT633.FINSAT633.SPI (NOLOCK) 
		LEFT JOIN FINSAT633.FINSAT633.STK(NOLOCK) ON SPI.MalKodu= STK.MalKodu
		WHERE (SPI.CHK LIKE '%KP-5%' OR  SPI.CHK LIKE '%KP-6%') AND KynkEvrakTip = 62 AND SPI.Tarih between @BasTarih AND @BitTarih
		GROUP BY SPI.CHK,SPI.Nesne1,STK.GrupKod,STK.Kod4,(CASE WHEN SPI.MalKodu LIKE '89850978%' THEN '6CM' WHEN SPI.MalKodu LIKE '8985095%'			THEN '8CM' ELSE '' END)
	) A
GROUP BY A.CHK,A.GrupKod,A.Koleksiyon,A.Durum,A.SUPTIP
) B
GROUP BY B.CHK
) C
END











GO
/****** Object:  StoredProcedure [wms].[MDF_UretimRapor_Chart]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[MDF_UretimRapor_Chart]
@BasTarih INT,
@BitTarih INT,
@Tip SMALLINT
AS
BEGIN

    --DECLARE olarak kullanılan TempTable bellekte tutulmaz
	DECLARE @TempStokVeri TABLE 
	(
		GrupID int,
		Kalinlik varchar(20), 
		En varchar(20), 
		Boy varchar(20),
		MamulBirim varchar(4),
		MamulMiktar numeric(25,6),
		MalKodu varchar(30),
		MalAdi varchar(50),
		Birim1 varchar(4),
		Birim1Miktar numeric(25,6),
		Birim2 varchar(4),
		Birim2Miktar numeric(25,6)
	)


	DECLARE @Kalinlik VARCHAR(20), @En VARCHAR(20), @Boy VARCHAR(20), 
			@MamulBirim VARCHAR(4), @MamulMiktar NUMERIC(25,6), @ID INT,
			@SilKalinlik VARCHAR(20), @SilEn VARCHAR(20), @SilBoy VARCHAR(20),
			@Say INT
	SET @ID=1;

	DECLARE CUR1 CURSOR FOR  
			(SELECT 
					STK.Kod1 AS Kalinlik, STK.Kod2 AS En, STK.Kod3 AS Boy, 
					MAX(STK.Birim1) AnaBirim, SUM(STI.Miktar) Miktar 
			FROM FINSAT633.FINSAT633.STK(NOLOCK) STK
			INNER JOIN FINSAT633.FINSAT633.STI(NOLOCK) STI ON STK.MalKodu=STI.MalKodu
			WHERE STK.GrupKod='MDF' 
			AND STI.Tarih BETWEEN @BasTarih AND @BitTarih 
			AND STI.IslemTur=0 
			AND KynkEvrakTip=74
			AND STI.Depo='11 DP'
			GROUP BY STK.Kod1, STK.Kod2, STK.Kod3) 
	OPEN CUR1
	FETCH NEXT FROM CUR1 INTO @Kalinlik, @En, @Boy,  @MamulBirim, @MamulMiktar
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO @TempStokVeri (GrupID, Kalinlik, En, Boy, MamulBirim, MamulMiktar, 
									MalKodu, MalAdi, Birim1, Birim1Miktar, Birim2, Birim2Miktar)
		SELECT @ID, @Kalinlik, @En, @Boy, @MamulBirim, @MamulMiktar, A.MalKodu, STK.MalAdi, STK.Birim1, 
		        A.Miktar Birim1Miktar, STK.Birim2, CASE WHEN STK.Birim2<>'' AND STK.Operator2=0 
				THEN  A.Miktar*STK.KatSayi2 WHEN STK.Birim2<>'' AND STK.Operator2=1 THEN A.Miktar/STK.KatSayi2 
				ELSE 0.0 END AS Birim2Miktar
				FROM
				(
				    --SARFİYATLAR ALINIYOR..
					SELECT '' as Kod1, '' as Kod2, '' as Kod3, STI.MalKodu, SUM(Miktar) Miktar, 
						   0 as MamulMiktar, MAX(STK.Birim1) AS Birim 
					FROM FINSAT633.FINSAT633.STI(NOLOCK)
					INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STK.MalKodu=STI.MalKodu
					WHERE EvrakNo IN(SELECT DISTINCT EvrakNo FROM FINSAT633.FINSAT633.STI(NOLOCK)
									WHERE MalKodu IN(SELECT MalKodu FROM FINSAT633.FINSAT633.STK(NOLOCK)
														WHERE GrupKod='MDF' AND Kod1=@Kalinlik AND Kod2=@En AND Kod3=@Boy)
											AND IslemTur=0 AND KynkEvrakTip=74 AND STI.Tarih BETWEEN @BasTarih AND @BitTarih) 
						 AND IslemTur = 1 AND KynkEvrakTip=74 
						
					GROUP BY STI.MalKodu

					UNION ALL

			        --MAMÜLLER ALINIYOR..
					SELECT STK.Kod1, STK.Kod2, STK.Kod3, '' as MalKodu, 0 as Miktar, 
					       SUM(Miktar) MamulMiktar, MAX(STK.Birim1) AS Birim
					FROM FINSAT633.FINSAT633.STI(NOLOCK)
					INNER JOIN FINSAT633.FINSAT633.STK(NOLOCK) STK ON STK.MalKodu=STI.MalKodu
					WHERE EvrakNo IN(SELECT DISTINCT EvrakNo FROM FINSAT633.FINSAT633.STI(NOLOCK)
									WHERE MalKodu IN(SELECT MalKodu FROM FINSAT633.FINSAT633.STK(NOLOCK)
														WHERE GrupKod='MDF' AND Kod1=@Kalinlik AND Kod2=@En AND Kod3=@Boy)
											AND IslemTur=0 AND KynkEvrakTip=74 AND STI.Tarih BETWEEN @BasTarih AND @BitTarih) 
						  AND IslemTur = 0 AND KynkEvrakTip=74 
						  AND Depo='11 DP'
					GROUP BY STK.Kod1, STK.Kod2, STK.Kod3 
				) 
				A
				LEFT JOIN 
				FINSAT633.FINSAT633.STK(NOLOCK) STK ON STK.MalKodu=A.MalKodu

			SET @ID=@ID+1;

	FETCH NEXT FROM CUR1 INTO @Kalinlik, @En, @Boy, @MamulBirim, @MamulMiktar
	END

	CLOSE CUR1
	DEALLOCATE CUR1

	--Sarfiyatı olan Mamüllerin çiftleyen satırlarını siliyoruz..
	DECLARE CUR2 CURSOR FOR  
	        (Select Kalinlik, En, Boy, COUNT(GrupID) as Say From @TempStokVeri 
			Group By Kalinlik, En, Boy
			Having COUNT(GrupID)>1)
	OPEN CUR2
	FETCH NEXT FROM CUR2 INTO @SilKalinlik, @SilEn, @SilBoy,  @Say
	WHILE @@FETCH_STATUS = 0
	BEGIN
	    
		DELETE FROM @TempStokVeri
		WHERE Kalinlik = @SilKalinlik AND En = @SilEn AND 
		        Boy = @SilBoy AND  MalKodu ='' 
			 
	    
	    FETCH NEXT FROM CUR2 INTO @SilKalinlik, @SilEn, @SilBoy,  @Say
	END
	CLOSE CUR2
	DEALLOCATE CUR2

	--Deposu YZMDF olmayan fakat sarflarda gelen MDF silinemediği için
	DELETE  @TempStokVeri WHERE Birim1='M3' AND Birim2='ADET' 

	
	--DELETE FROM @TempStokVeri
	--WHERE(
	--     Kalinlik= (SELECT A.Kalinlik FROM( 	
	--		Select Kalinlik, En, Boy, COUNT(GrupID) as Say From @TempStokVeri 
	--		Group By Kalinlik, En, Boy
	--		Having COUNT(GrupID)>1)A) AND 
	--	  En= (SELECT A.En FROM( 	
	--		Select Kalinlik, En, Boy, COUNT(GrupID) as Say From @TempStokVeri 
	--		Group By Kalinlik, En, Boy
	--		Having COUNT(GrupID)>1)A) AND
	--	  Boy=(SELECT A.Boy FROM( 	
	--		Select Kalinlik, En, Boy, COUNT(GrupID) as Say From @TempStokVeri 
	--		Group By Kalinlik, En, Boy
	--		Having COUNT(GrupID)>1)A) AND
	--	  MalKodu='' 
 --    )

	
	 --Pivot Gridde Sarfiyatı olmayanlar için ayrı kolonlar açtığı için bunu önlemek için 
	 --boş alanları mevcut diğer alanlarla dolduruyırum...
	 IF(SELECT COUNT(GrupID) FROM @TempStokVeri WHERE MalAdi IS NULL) > 0  AND 
	   (SELECT COUNT(GrupID) FROM @TempStokVeri WHERE MalAdi IS NOT NULL) > 0
     BEGIN
	    UPDATE @TempStokVeri 
		       SET MalKodu=(SELECT TOP 1 MalKodu FROM @TempStokVeri WHERE MalAdi IS NOT NULL),
			       MalAdi=(SELECT TOP 1 MalAdi FROM @TempStokVeri WHERE MalAdi IS NOT NULL),
				   Birim1=(SELECT TOP 1 Birim1 FROM @TempStokVeri WHERE MalAdi IS NOT NULL),
				   Birim2=(SELECT TOP 1 Birim2 FROM @TempStokVeri WHERE MalAdi IS NOT NULL)
		WHERE MalAdi IS NULL 
	END
	--SELECT *  FROM @TempStokVeri


	IF(@Tip=0)   ---Tip=0 ise Rapor
	BEGIN
		--Değerler gönderiliyor...
		SELECT  GrupID, '['+En+' - '+Boy+' - '+Kalinlik+']  =>  '+CAST(MamulMiktar AS varchar(20))+' '+MamulBirim AS Mamul,
				MalKodu+' - '+MalAdi+'  =>  ['+Birim1+' - '+Birim2+']'  AS MalKodu, Birim1Miktar, Birim2Miktar 
		FROM @TempStokVeri
	END
	ELSE IF(@Tip=1)  ---Tip=1 ise Chart
	BEGIN
		--Değerler gönderiliyor...
		SELECT GrupID, MAX('['+En+' - '+Boy+' - '+Kalinlik+'] '+MamulBirim) AS Mamul, '' AS MalKodu, 
				MAX(MamulMiktar) AS Birim1Miktar,  0.0 AS Birim2Miktar 
		FROM @TempStokVeri
		Group By GrupID
	END

	

END












GO
/****** Object:  StoredProcedure [wms].[MevcudBanka]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 02.08.2017
-- Modify date: 02.08.2017
-- Description:	bankadaki mevcut parayı getirir
-- =============================================
CREATE PROCEDURE [wms].[MevcudBanka]
AS
BEGIN
	
	SELECT        ISNULL(SUM(CASE WHEN a.BA = 0 THEN a.Tutar ELSE - a.Tutar END) ,0) AS Bakiye
	FROM            (SELECT        KarsiHesapKodu AS HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, (CASE WHEN BA = 0 THEN 1 ELSE 0 END) AS BA
							  FROM            FINSAT633.CHI WITH (nolock)
							  WHERE        (KarsiHesapKodu IS NOT NULL) AND (KarsiHesapKodu <> '') AND (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60))
							  UNION ALL
							  SELECT        HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, BA
							  FROM            FINSAT633.CHI AS CHI_1 WITH (nolock)
							  WHERE        (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60))) AS a INNER JOIN
							 FINSAT633.CHK AS b WITH (nolock) ON b.HesapKodu = a.HesapKodu
	WHERE        (b.HesapKodu LIKE 'T%')

END

GO
/****** Object:  StoredProcedure [wms].[MevcudCek]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 02.08.2017
-- Modify date: 02.08.2017
-- Description:	çeklerdeki mevcut parayı getirir
-- =============================================
CREATE PROCEDURE [wms].[MevcudCek]

AS
BEGIN
	
	SELECT        ISNULL(SUM(Tutar) ,0) AS Tutar
	FROM            (SELECT        SCI.IslemTip, SCI.Tutar, ISNULL
															((SELECT        TOP (1) Chk
																FROM            FINSAT633.SCI AS S WITH (NOLOCK)
																WHERE        (SiraNo =
																							 (SELECT        MAX(SiraNo) AS Expr1
																							   FROM            FINSAT633.SCI AS SC WITH (NOLOCK)
																							   WHERE        (IslemTip = 2) AND (EvrakNo = SCI.EvrakNo))) AND (EvrakNo = SCI.EvrakNo)), '') AS VerildigiYer
							  FROM            FINSAT633.SCI AS SCI WITH (NOLOCK) RIGHT OUTER JOIN
															(SELECT        MAX(EvrakNo) AS EvrakNo, MAX(SiraNo) AS Sirano
															  FROM            FINSAT633.SCI AS SCI WITH (NOLOCK)
															  GROUP BY EvrakNo) AS SIRA ON SIRA.EvrakNo = SCI.EvrakNo AND SIRA.Sirano = SCI.SiraNo RIGHT OUTER JOIN
														FINSAT633.ACK AS ACK WITH (NOLOCK) ON SCI.EvrakNo = ACK.EvrakNo RIGHT OUTER JOIN
														FINSAT633.CHK AS CHK WITH (NOLOCK) ON ACK.Veren = CHK.HesapKodu) AS CEK
	WHERE        (VerildigiYer = '101 01 01 00002') AND (IslemTip = 2) OR (IslemTip IN (0, 10, 16))

END

GO
/****** Object:  StoredProcedure [wms].[MevcudKasa]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 02.08.2017
-- Modify date: 02.08.2017
-- Description:	kasadaki mevcut parayı getirir
-- =============================================
CREATE PROCEDURE [wms].[MevcudKasa]
AS
BEGIN

	SELECT        ISNULL(SUM(CASE WHEN a.BA = 0 THEN a.Tutar ELSE - a.Tutar END) ,0) AS Bakiye
	FROM            (SELECT        KarsiHesapKodu AS HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, (CASE WHEN BA = 0 THEN 1 ELSE 0 END) AS BA
							  FROM            FINSAT633.CHI WITH (nolock)
							  WHERE        (KarsiHesapKodu IS NOT NULL) AND (KarsiHesapKodu <> '') AND (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60))
							  UNION ALL
							  SELECT        HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, BA
							  FROM            FINSAT633.CHI AS CHI_1 WITH (nolock)
							  WHERE        (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60))) AS a INNER JOIN
							 FINSAT633.CHK AS b WITH (nolock) ON b.HesapKodu = a.HesapKodu
	WHERE        (b.HesapKodu LIKE '0001%')

END

GO
/****** Object:  StoredProcedure [wms].[MevcudPOS]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Create date: 02.08.2017
-- Modify date: 02.08.2017
-- Description:	kredikartındaki mevcut parayı getirir
-- =============================================
CREATE PROCEDURE [wms].[MevcudPOS]
AS
BEGIN
	
	SELECT        ISNULL(SUM(CASE WHEN a.BA = 0 THEN a.Tutar ELSE - a.Tutar END) ,0) AS Bakiye
	FROM            (SELECT        KarsiHesapKodu AS HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, (CASE WHEN BA = 0 THEN 1 ELSE 0 END) AS BA
							  FROM            FINSAT633.CHI WITH (nolock)
							  WHERE        (KarsiHesapKodu IS NOT NULL) AND (KarsiHesapKodu <> '') AND (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60))
							  UNION ALL
							  SELECT        HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, BA
							  FROM            FINSAT633.CHI AS CHI_1 WITH (nolock)
							  WHERE        (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42, 60))) AS a INNER JOIN
							 FINSAT633.CHK AS b WITH (nolock) ON b.HesapKodu = a.HesapKodu
	WHERE        (b.HesapKodu LIKE '108%')

END

GO
/****** Object:  StoredProcedure [wms].[OdemeYapmayanMusteriler]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[OdemeYapmayanMusteriler]
@Number INT
AS
BEGIN
	
	SELECT CHK.HesapKodu, CHK.Unvan1,
	cast (CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadefatB+CHK.KdvB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadefatA-CHK.KdvA-CHK.DigerA as numeric(36,2)) AS Bakiye,
--	replace(replace(replace(convert(varchar,CAST((CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadefatB+CHK.KdvB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadefatA-CHK.KdvA-CHK.DigerA) as decimal(16,4)),1),'.','x'),',','.'),'x',',') as Bakiye ,
	cast(isnull((select top(1) Tutar from FINSAT633.FINSAT633.CHI(NOLOCK) CHI where CHI.KynkEvrakTip in (6,10,12,25,60,79,148) and (CHI.HesapKodu=CHK.HesapKodu or CHI.KarsiHesapKodu=CHK.HesapKodu) order by CHI.Tarih desc,CHI.EvrakNo desc, CHI.SiraNo desc),'0')as numeric(36,2)) AS SonTahsilatTutari,
--  replace(replace(replace(convert(varchar,CAST(isnull((select top(1) Tutar from FINSAT633.FINSAT633.CHI(NOLOCK) CHI where CHI.KynkEvrakTip in (6,10,12,25,60,79,148) and (CHI.HesapKodu=CHK.HesapKodu or CHI.KarsiHesapKodu=CHK.HesapKodu) order by CHI.Tarih desc,CHI.EvrakNo desc, CHI.SiraNo desc),'0') as money),1),'.','x'),',','.'),'x',',') as SonTahsilatTutari,
(CASE WHEN isnull((select top(1) Tarih from FINSAT633.FINSAT633.CHI(NOLOCK) CHI where CHI.KynkEvrakTip in (6,10,12,25,60,79,148) and (CHI.HesapKodu=CHK.HesapKodu or CHI.KarsiHesapKodu=CHK.HesapKodu) order by CHI.Tarih desc,CHI.EvrakNo desc, CHI.SiraNo desc),'') = 0 THEN '' ELSE CONVERT(VARCHAR(15), CONVERT(DATETIME,isnull((select top(1) Tarih-2 from FINSAT633.FINSAT633.CHI(NOLOCK) CHI where CHI.KynkEvrakTip in (6,10,12,25,60,79,148) and (CHI.HesapKodu=CHK.HesapKodu or CHI.KarsiHesapKodu=CHK.HesapKodu) order by CHI.Tarih desc,CHI.EvrakNo desc, CHI.SiraNo desc),'')),104) END) as SonTahsilatTarihi

FROM FINSAT633.FINSAT633.CHK CHK (NOLOCK)

where CHK.HesapKodu not in 
(
select HesapKodu from FINSAT633.FINSAT633.CHI(NOLOCK) CHI where CHI.KynkEvrakTip in (6,10,12,25,60,79,148) and CHI.Tarih between datediff(dd,0,getdate()+2)-@Number and datediff(dd,0,getdate()+2) 
union all
select KarsiHesapKodu from FINSAT633.FINSAT633.CHI(NOLOCK) CHI where CHI.KynkEvrakTip in (6,10,12,25,60,79,148) and CHI.Tarih between datediff(dd,0,getdate()+2)-@Number and datediff(dd,0,getdate()+2) 

)
and CHK.Karttip =0 and (CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadefatB+CHK.KdvB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadefatA-CHK.KdvA-CHK.DigerA) > 0
order by CHK.HesapKodu

END















GO
/****** Object:  StoredProcedure [wms].[PozisyonCekSenet]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[PozisyonCekSenet]
AS
BEGIN
	
	select ID,ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME(NOLOCK) COMBOITEM_NAME WHERE COMBOITEM_NAME.ITEMCBOID=13 

END







GO
/****** Object:  StoredProcedure [wms].[SatilmayanUrunler]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SatilmayanUrunler]
@Number INT
AS
BEGIN
	
	select STK.MalKodu as [MalKodu],STK.MalAdi as [MalAdi],STK.Birim1 as Birim, STK.GirMiktar-STK.CikMiktar+STK.DvrMiktar as [StokMiktar], STK.KritikStok as [KritikStok], A.EvrakNo SonFaturaNo, CONVERT(VARCHAR(15),CONVERT(DATETIME, A.Tarih-2),104) AS SonFaturaTarihi
,ISNULL(CONVERT(VARCHAR(15),(SELECT TOP 1 U.Tarih FROM BirikimERP.UYM.Uretim(NOLOCK) U WHERE U.MalKodu=STK.MalKodu ORDER BY U.Tarih DESC),104),'') AS  SonUretimTarihi
,A.Miktar SonSatisMiktari
,A.Birim SonSatisMiktariBirim
from FINSAT633.FINSAT633.STK (NOLOCK) STK INNER JOIN (SELECT  A.MalKodu, S.Tarih, S.EvrakNo, S.Miktar, S.Birim FROM FINSAT633.FINSAT633.STI (NOLOCK) S INNER JOIN (SELECT MAX(Row_ID) Row_ID, MalKodu FROM FINSAT633.FINSAT633.STI (NOLOCK) WHERE KynkEvrakTip IN (1,64) GROUP BY MalKodu) A ON A.Row_ID=S.Row_ID) A ON STK.MalKodu=A.MalKodu
where (STK.GirMiktar-STK.CikMiktar+STK.DvrMiktar)>0 AND STK.KartTip in (0,1) 
AND STK.MalKodu not in  (select MalKodu from FINSAT633.FINSAT633.STI(NOLOCK) STI where STI.KynkEvrakTip in (1,64) and STI.Tarih between DATEDIFF(d,@Number,GETDATE()+2) and DATEDIFF(d,0,GETDATE()+2) )  
order by STK.MalKodu

END








GO
/****** Object:  StoredProcedure [wms].[SatinAlmaGMOnayList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[SatinAlmaGMOnayList]
AS
BEGIN
	
	SELECT DISTINCT ST.SipTalepNo, ST.HesapKodu, CHK.Unvan1 as Unvan
FROM [Kaynak].[sta].[Talep] as ST (nolock)
LEFT JOIN [FINSAT633].[FINSAT633].[CHK] (nolock) on ST.HesapKodu=CHK.HesapKodu
WHERE ST.Durum=11 AND ST.SipTalepNo IS NOT NULL AND ST.HesapKodu IS NOT NULL AND ST.TeklifNo IS NOT NULL

END











GO
/****** Object:  StoredProcedure [wms].[SatinAlmaGMOnayListData]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Modify date: 13.12.2033
-- Description:	/Approvals/Purchase/GM_Onay listesi
-- =============================================
CREATE PROCEDURE [wms].[SatinAlmaGMOnayListData]
	@HesapKodu VARCHAR(20),
	@SipTalepNo INT
AS
BEGIN

	SELECT        ST.ID, ST.TalepNo, ST.MalKodu, ST.Tarih, ST.Tip, ST.Birim, ST.BirimMiktar, (CASE WHEN ST.Birim = STK.Birim1 THEN ST.BirimMiktar WHEN ST.Birim = STK.Birim2 AND 
							 STK.Operator2 = 0 THEN ST.BirimMiktar / STK.Katsayi2 WHEN ST.Birim = STK.Birim2 AND STK.Operator2 = 1 THEN ST.BirimMiktar * STK.Katsayi2 WHEN ST.Birim = STK.Birim3 AND 
							 STK.Operator3 = 0 THEN ST.BirimMiktar / STK.Katsayi3 WHEN ST.Birim = STK.Birim3 AND STK.Operator3 = 1 THEN ST.BirimMiktar * STK.Katsayi3 WHEN ST.Birim = STK.Birim4 AND 
							 STK.Operator4 = 0 THEN ST.BirimMiktar / STK.Katsayi4 WHEN ST.Birim = STK.Birim4 AND STK.Operator4 = 1 THEN ST.BirimMiktar * STK.Katsayi4 END) AS Miktar, 
							 (CASE WHEN ST.Birim = STK.Birim1 THEN 0 WHEN ST.Birim = STK.Birim2 THEN STK.Operator2 WHEN ST.Birim = STK.Birim3 THEN STK.Operator3 WHEN ST.Birim = STK.Birim4 THEN STK.Operator4 END) AS Operator, 
							 (CASE WHEN ST.Birim = STK.Birim1 THEN 1 WHEN ST.Birim = STK.Birim2 THEN STK.Katsayi2 WHEN ST.Birim = STK.Birim3 THEN STK.Katsayi3 WHEN ST.Birim = STK.Birim4 THEN STK.Katsayi4 END) AS Katsayi, 
							 ST.IstenenTarih, ST.Aciklama, ST.Aciklama2, ST.Aciklama3, ST.Durum, ST.EkDosya, ST.Kademe1Onaylayan, ST.Kademe1OnayTarih, ST.Kademe2Onaylayan, ST.Kademe2OnayTarih, ST.Satinalmaci, ST.TeklifNo, ST.HesapKodu, 
							 ST.BirimFiyat, ST.DvzTL, ST.DvzCinsi,
								ISNULL((SELECT CASE WHEN DovizCinsi='JPY' then KUR01/100 else KUR01 end AS DvzKuru FROM SOLAR6.dbo.DVZ (NOLOCK) WHERE DovizCinsi=ST.DvzCinsi AND Tarih=CAST( DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))+2 AS INT)),0) as DvzKuru,
							 ST.KDVOran, ST.SipTalepNo, ST.SipIslemTip, ST.GMYMaliOnaylayan, ST.GMYMaliOnayTarih, 
							 ST.Kaydeden, ST.TesisKodu, FINSAT633.STK.MalAdi, TK.Vade AS TeklifVade, TK.TeklifAciklamasi, ST.FTDDovizTL, ST.FTDDovizCinsi,
								ISNULL((SELECT CASE WHEN DovizCinsi='JPY' then KUR01/100 else KUR01 end AS DvzKuru FROM SOLAR6.dbo.DVZ (NOLOCK) WHERE DovizCinsi=ST.FTDDovizCinsi AND Tarih=CAST( DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))+2 AS INT)),0) as FTDDovizKuru
	FROM            Kaynak.sta.Talep AS ST WITH (nolock) INNER JOIN
							 Kaynak.sta.Teklif AS TK WITH (nolock) ON TK.TeklifNo = ST.TeklifNo AND TK.HesapKodu = ST.HesapKodu AND TK.MalKodu = ST.MalKodu LEFT OUTER JOIN
							 FINSAT633.STK WITH (nolock) ON ST.MalKodu = FINSAT633.STK.MalKodu
	WHERE        (ST.Durum = 11) AND (ST.SipTalepNo = @SipTalepNo) AND (ST.HesapKodu = @HesapKodu)

END
GO
/****** Object:  StoredProcedure [wms].[SatisBaglantiHareketleri]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[SatisBaglantiHareketleri]
@SozlesmeNo VARCHAR(20)
AS
BEGIN


	SELECT
		STI.Kod5 AS SozlesmeNo, STI.EvrakNo, 
		CONVERT(VARCHAR(12),CONVERT(datetime,STI.Tarih-2),104) AS Tarih, STI.KaynakSiparisNo, 
		CONVERT(VARCHAR(12),CONVERT(datetime,STI.KaynakSiparisTarih-2),104) AS KaynakSiparisTarih,
		 		  (select FytListeNo FROM FINSAT633.FINSAT633.SPI (NOLOCK) SPI WHERE SPI.Evrakno=STI.KaynakSiparisNo and SPI.SiraNo=STI.SiparisSiraNo and SPI.CHK=STI.CHK AND SPI.kod5=STI.Kod5 and SPI.Kynkevraktip=62) as FytListeNo, STI.Depo, STI.MalKodu, STI.Miktar, STI.Birim, STI.BirimFiyat, STI.Tutar, 
		STI.ToplamIskonto, STI.KDVOran, STI.KDV, 
		(STI.Tutar-STI.ToplamIskonto+STI.KDV) AS SevkedilenTutar
	FROM FINSAT633.FINSAT633.STI(NOLOCK) STI
	WHERE STI.KOD5=@SozlesmeNo AND  STI.KynkEvrakTip=1  AND
	      STI.Chk=(SELECT TOP 1 MusteriKod FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) 
		           WHERE ListeNo=@SozlesmeNo)
	Order By STI.ROW_ID 

END










GO
/****** Object:  StoredProcedure [wms].[SatisIadeKayitList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 10.10.2017
-- Modify date: 29.11.2017
-- Description:	Alımdan iade evrak ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[SatisIadeKayitList]
	@IrsaliyeID int
AS
BEGIN
	
SELECT        BIRIKIM.wms.IRS_Detay.IrsaliyeID, BIRIKIM.wms.IRS_Detay.MalKodu, SUM(BIRIKIM.wms.IRS_Detay.Miktar) AS Miktar, BIRIKIM.wms.IRS_Detay.Birim, ISNULL(ISNULL(FINSAT633.STI.ErekIIFMiktar, 0) 
                         + SUM(BIRIKIM.wms.IRS_Detay.Miktar), 0) AS ErekIIFMiktar, ISNULL(SUM(BIRIKIM.wms.IRS_Detay.OkutulanMiktar), 0) AS OkutulanMiktar, BIRIKIM.wms.Depo.DepoKodu, BIRIKIM.wms.IRS.HesapKodu, 
                         BIRIKIM.wms.IRS.Tarih, FINSAT633.STI.ROW_ID,
                             (SELECT        MalAdi
                               FROM            FINSAT633.STK WITH (NOLOCK)
                               WHERE        (MalKodu = BIRIKIM.wms.IRS_Detay.MalKodu)) AS MalAdi, ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisNo, '') AS SiparisNo, ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisNo, '') 
                         AS KaynakIIFEvrakNo, ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisSiraNo, 0) AS KynkSiparisSiraNo, ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisTarih, 0) AS KynkSiparisTarih, 
                         ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisMiktar, 0) AS KynkSiparisMiktar, FINSAT633.STI.BirimFiyat AS Fiyat, FINSAT633.STI.KDVOran, FINSAT633.STI.SiraNo, FINSAT633.STI.KaynakSiraNo, 
                         FINSAT633.STI.IskontoOran1, FINSAT633.STI.IskontoOran2, FINSAT633.STI.IskontoOran3, FINSAT633.STI.IskontoOran4, FINSAT633.STI.IskontoOran5, FINSAT633.STI.Kod1, FINSAT633.STI.Kod2, 
                         FINSAT633.STI.Kod3, FINSAT633.STI.Kod4, FINSAT633.STI.Kod5, FINSAT633.STI.Kod6, FINSAT633.STI.Kod7, FINSAT633.STI.Kod8, FINSAT633.STI.Kod9, FINSAT633.STI.Kod10, FINSAT633.STI.Kod11, 
                         FINSAT633.STI.Kod12, FINSAT633.STI.Kod13, FINSAT633.STI.Kod14, FINSAT633.STI.ValorGun, FINSAT633.STI.Operator, FINSAT633.STI.KaynakIrsEvrakNo, FINSAT633.STI.KaynakIrsTarih, 
                         FINSAT633.STI.KaynakIIFTarih, FINSAT633.STI.KaynakSiparisNo, FINSAT633.STI.KaynakSiparisTarih, FINSAT633.STI.Kredi_Donem_VadeTarih, FINSAT633.MFK.Tarih AS MFKTarih, 
                         FINSAT633.MFK.Aciklama AS MFKAciklama, FINSAT633.STI.ToplamKlmIskonto, FINSAT633.STI.IskontoTutar1, FINSAT633.STI.IslemTip AS SipIslemTip, FINSAT633.STI.Nesne1, FINSAT633.STI.Nesne2, 
                         FINSAT633.STI.Nesne3, FINSAT633.STI.SevkTarih, FINSAT633.STI.SiparisSiraNo, FINSAT633.STI.EvrakTarih, FINSAT633.CHK.EFatSenaryo, FINSAT633.CHK.EArsivTeslimSekli, 
                         FINSAT633.CHK.EFatEtiket AS EFatEtiketPK, ISNULL
                             ((SELECT        solar6.dbo.SIR.EfatEtiketGB
                                 FROM            solar6.dbo.SIR WITH (NOLOCK) INNER JOIN
                                                          solar6.dbo.SDK WITH (NOLOCK) ON solar6.dbo.SIR.Kod = solar6.dbo.SDK.SirketKod
                                 WHERE        (solar6.dbo.SDK.Kod = '33') AND (solar6.dbo.SDK.Tip = 0)), '') AS EfatEtiketGB

FROM            BIRIKIM.wms.IRS WITH (NOLOCK) INNER JOIN
                         BIRIKIM.wms.Depo WITH (NOLOCK) ON BIRIKIM.wms.IRS.DepoID = BIRIKIM.wms.Depo.ID INNER JOIN
                         FINSAT633.STI WITH (NOLOCK) INNER JOIN
                         FINSAT633.MFK WITH (NOLOCK) ON FINSAT633.STI.EvrakNo = FINSAT633.MFK.EvrakNo AND FINSAT633.STI.KynkEvrakTip = FINSAT633.MFK.KynkEvrakTip INNER JOIN
                         FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.STI.Chk = FINSAT633.CHK.HesapKodu ON BIRIKIM.wms.IRS.HesapKodu = FINSAT633.STI.Chk INNER JOIN
                         BIRIKIM.wms.IRS_Detay WITH (NOLOCK) ON BIRIKIM.wms.IRS.ID = BIRIKIM.wms.IRS_Detay.IrsaliyeID AND FINSAT633.STI.EvrakNo = BIRIKIM.wms.IRS_Detay.KynkSiparisNo AND 
                         FINSAT633.STI.ROW_ID = BIRIKIM.wms.IRS_Detay.KynkSiparisID

WHERE        (FINSAT633.STI.KynkEvrakTip = 1) AND (BIRIKIM.wms.IRS_Detay.IrsaliyeID = @IrsaliyeID) AND (BIRIKIM.wms.IRS_Detay.OkutulanMiktar IS NOT NULL) AND (BIRIKIM.wms.IRS_Detay.OkutulanMiktar > 0)
GROUP BY BIRIKIM.wms.IRS.EvrakNo, BIRIKIM.wms.IRS_Detay.IrsaliyeID, FINSAT633.STI.KynkEvrakTip, BIRIKIM.wms.IRS_Detay.MalKodu, FINSAT633.STI.ErekIIFMiktar, BIRIKIM.wms.IRS_Detay.Birim, 
                         BIRIKIM.wms.Depo.DepoKodu, BIRIKIM.wms.IRS.HesapKodu, BIRIKIM.wms.IRS.Tarih, ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisNo, ''), ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisSiraNo, 0), 
                         ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisTarih, 0), ISNULL(BIRIKIM.wms.IRS_Detay.KynkSiparisMiktar, 0), FINSAT633.STI.BirimFiyat, FINSAT633.STI.KDVOran, FINSAT633.STI.IskontoOran1, 
                         FINSAT633.STI.IskontoOran2, FINSAT633.STI.ROW_ID, FINSAT633.STI.IskontoOran3, FINSAT633.STI.IskontoOran4, FINSAT633.STI.IskontoOran5, FINSAT633.STI.Kod1, FINSAT633.STI.Kod3, 
                         FINSAT633.STI.ValorGun, FINSAT633.STI.Operator, FINSAT633.STI.Kod2, FINSAT633.STI.Kod4, FINSAT633.STI.Kod5, FINSAT633.STI.Kod6, FINSAT633.STI.Kod7, FINSAT633.STI.Kod8, FINSAT633.STI.Kod9, 
                         FINSAT633.STI.Kod10, FINSAT633.STI.Kod11, FINSAT633.STI.Kod12, FINSAT633.STI.Kod13, FINSAT633.STI.Kod14, FINSAT633.STI.KaynakIrsEvrakNo, FINSAT633.STI.KaynakIrsTarih, 
                         FINSAT633.STI.KaynakIIFTarih, FINSAT633.STI.KaynakSiparisNo, FINSAT633.STI.Kredi_Donem_VadeTarih, FINSAT633.MFK.Tarih, FINSAT633.MFK.Aciklama, FINSAT633.STI.KaynakSiparisTarih, 
                         FINSAT633.STI.ToplamKlmIskonto, FINSAT633.STI.IskontoTutar1, FINSAT633.STI.Nesne1, FINSAT633.STI.Nesne2, FINSAT633.STI.Nesne3, FINSAT633.STI.SevkTarih, FINSAT633.STI.SiparisSiraNo, 
                         FINSAT633.STI.EvrakTarih, FINSAT633.STI.SiraNo, FINSAT633.STI.KaynakSiraNo, FINSAT633.CHK.EFatSenaryo, FINSAT633.CHK.EArsivTeslimSekli, FINSAT633.CHK.EFatEtiket, FINSAT633.STI.IslemTip

END
GO
/****** Object:  StoredProcedure [wms].[SatisIptalDetail]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 03.10.2017
-- Modify date: 29.11.2017
-- Description:	Alımdan iade evrak ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[SatisIptalDetail]
	@EvrakNo varchar(16),
	@DepoKodu varchar(5),
	@CHK varchar(20)

AS
BEGIN
	
	SELECT        '33' AS SirketID, FINSAT633.STI.EvrakNo, FINSAT633.STI.Chk, FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.STI.MalKodu, FINSAT633.STK.MalAdi, FINSAT633.STK.Birim1 AS Birim, 
							 SUM(FINSAT633.STI.Miktar - FINSAT633.STI.ErekIIFMiktar) AS Miktar, FINSAT633.STI.Depo, wms.getStockByDepo(FINSAT633.STI.MalKodu, @DepoKodu) AS Stok, BIRIKIM.wms.fnGetStock(FINSAT633.STI.Depo, 
							 FINSAT633.STI.MalKodu, FINSAT633.STK.Birim1, 0) AS WmsStok, BIRIKIM.wms.fnGetRezervStock(@DepoKodu, FINSAT633.STI.MalKodu, FINSAT633.STK.Birim1) AS WmsRezerv
	FROM            FINSAT633.STI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.STI.MalKodu = FINSAT633.STK.MalKodu INNER JOIN
							 --FINSAT633.MFK WITH (NOLOCK) ON FINSAT633.STI.EvrakNo = FINSAT633.MFK.EvrakNo AND FINSAT633.STI.Chk = FINSAT633.MFK.HesapKod AND FINSAT633.STI.KynkEvrakTip = FINSAT633.MFK.KynkEvrakTip INNER JOIN
							 FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.STI.Chk = FINSAT633.CHK.HesapKodu
	WHERE        (FINSAT633.STI.KynkEvrakTip = 1) AND (FINSAT633.STI.Chk = @CHK) AND (FINSAT633.STI.EvrakNo = @EvrakNo)
				 AND case when @DepoKodu <> '' then case when (FINSAT633.STI.Depo = @DepoKodu) then 1 else 0 end else 1 end = 1
	GROUP BY FINSAT633.STI.EvrakNo, FINSAT633.STI.MalKodu, FINSAT633.STI.Chk, FINSAT633.CHK.Unvan1, FINSAT633.STI.Depo, FINSAT633.STK.MalAdi, FINSAT633.STK.Birim1

END
GO
/****** Object:  StoredProcedure [wms].[SatisIptalSecimList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 03.10.2017
-- Modify date: 29.11.2017
-- Description:	Alımdan iade evrak ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[SatisIptalSecimList]
	@EvrakNo varchar(16),
	@DepoKodu varchar(5),
	@CHK varchar(20)

AS
BEGIN
	
	SELECT        '33' AS SirketID, FINSAT633.STI.ROW_ID AS ID, FINSAT633.STI.EvrakNo, FINSAT633.STI.Tarih, FINSAT633.STI.MalKodu, FINSAT633.STK.MalAdi, FINSAT633.STI.Miktar - FINSAT633.STI.ErekIIFMiktar AS Miktar, 
							 FINSAT633.STI.Birim, wms.getStockByDepo(FINSAT633.STI.MalKodu, @DepoKodu) AS GunesStok, BIRIKIM.wms.fnGetStock(FINSAT633.STI.Depo, FINSAT633.STI.MalKodu, FINSAT633.STK.Birim1, 0) AS WmsStok, 
							 BIRIKIM.wms.fnGetRezervStock(@DepoKodu, FINSAT633.STI.MalKodu, FINSAT633.STK.Birim1) AS WmsRezerv
	FROM            FINSAT633.STI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.STI.MalKodu = FINSAT633.STK.MalKodu INNER JOIN
							 --FINSAT633.MFK WITH (NOLOCK) ON FINSAT633.STI.EvrakNo = FINSAT633.MFK.EvrakNo AND FINSAT633.STI.Chk = FINSAT633.MFK.HesapKod AND FINSAT633.STI.KynkEvrakTip = FINSAT633.MFK.KynkEvrakTip INNER JOIN
							 FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.STI.Chk = FINSAT633.CHK.HesapKodu
	WHERE        (FINSAT633.STI.KynkEvrakTip = 1) AND (FINSAT633.STI.Chk IN (Select * from BIRIKIM.dbo.splitstring(@CHK))) AND (FINSAT633.STI.EvrakNo in (Select * from BIRIKIM.dbo.splitstring(@EvrakNo)))
					AND case when @DepoKodu <> '' then case when (FINSAT633.STI.Depo = @DepoKodu) then 1 else 0 end else 1 end = 1

END
GO
/****** Object:  StoredProcedure [wms].[SatisIptalSiparisList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 03.10.2017
-- Modify date: 16.11.2017
-- Description:	Alımdan iade evrak ayrıntıları
-- =============================================
CREATE PROCEDURE [wms].[SatisIptalSiparisList]
	@DepoKodu varchar(5),
	@CHK varchar(20),
	@BasTarih int,
	@BitTarih int

AS
BEGIN
	
	SELECT        '33' AS SirketID, FINSAT633.STI.EvrakNo, FINSAT633.STI.Depo, FINSAT633.STI.Tarih, FINSAT633.STI.Chk, FINSAT633.CHK.Unvan1 AS Unvan, FINSAT633.CHK.GrupKod, FINSAT633.CHK.FaturaAdres3 AS FaturaAdres, COUNT(FINSAT633.STI.MalKodu) 
							 AS Çeşit, SUM(FINSAT633.STI.Miktar - FINSAT633.STI.ErekIIFMiktar) AS Miktar, MIN(FINSAT633.STI.KayitSaat) AS Saat
	FROM            FINSAT633.STI WITH (NOLOCK) INNER JOIN
							 FINSAT633.STK WITH (NOLOCK) ON FINSAT633.STI.MalKodu = FINSAT633.STK.MalKodu INNER JOIN
							 FINSAT633.CHK WITH (NOLOCK) ON FINSAT633.STI.Chk = FINSAT633.CHK.HesapKodu
	WHERE        (FINSAT633.STI.KynkEvrakTip = 1) AND (FINSAT633.STI.Chk = @CHK) AND (FINSAT633.STI.Tarih >= @BasTarih) AND (FINSAT633.STI.Tarih <= @BitTarih)
				 AND case when @DepoKodu <> '' then case when (FINSAT633.STI.Depo = @DepoKodu) then 1 else 0 end else 1 end = 1
	GROUP BY FINSAT633.STI.EvrakNo, FINSAT633.STI.Depo, FINSAT633.STI.Tarih, FINSAT633.STI.Chk, FINSAT633.CHK.Unvan1, FINSAT633.CHK.GrupKod, FINSAT633.CHK.FaturaAdres3

END
GO
/****** Object:  StoredProcedure [wms].[SatisTemsilcisi_AylikSatisAnalizi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Create date: 01.01.2017
-- Modify date: 03.08.2017
-- Description:	satış temsilcilerinini aylık satış analizi
-- =============================================
CREATE PROCEDURE [wms].[SatisTemsilcisi_AylikSatisAnalizi]
	@Ay SMALLINT,
	@Kriter VARCHAR(10)

AS
BEGIN

    DECLARE @BasTarih INT, @BitTarih INT

	IF @Ay=0
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	End
	ELSE IF @Ay=1
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-01-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-01-31')+2 AS INT)
	End
	ELSE IF @Ay=2
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-02-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-02-28')+2 AS INT)
	End
	ELSE IF @Ay=3
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-03-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-03-31')+2 AS INT)
	End
	ELSE IF @Ay=4
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-04-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-04-30')+2 AS INT)
	End
	ELSE IF @Ay=5
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-05-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-05-31')+2 AS INT)
	End
	ELSE IF @Ay=6
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-06-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-06-30')+2 AS INT)
	End
	ELSE IF @Ay=7
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-07-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-07-31')+2 AS INT)
	End
	ELSE IF @Ay=8
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-08-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-08-31')+2 AS INT)
	End
	ELSE IF @Ay=9
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-09-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-09-30')+2 AS INT)
	End
	ELSE IF @Ay=10
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-10-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-10-31')+2 AS INT)
	End
	ELSE IF @Ay=11
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-11-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-11-30')+2 AS INT)
	End
	ELSE IF @Ay=12
	Begin
	   Set @BasTarih=CAST(CONVERT(SMALLDATETIME, '2017-12-01')+2 AS INT)
	   Set @BitTarih=CAST(CONVERT(SMALLDATETIME, '2017-12-31')+2 AS INT)
	End

	SELECT        STI.Kod7, MAX(FINSAT633.KTK.Aciklama) AS Aciklama, ISNULL(SUM(STI.Tutar - STI.ToplamIskonto), 0) AS GenelTutar
	FROM            FINSAT633.STI AS STI WITH (NOLOCK) INNER JOIN
							 FINSAT633.CHK AS CHK WITH (NOLOCK) ON STI.Chk = CHK.HesapKodu INNER JOIN
							 FINSAT633.STK AS STK WITH (NOLOCK) ON STI.MalKodu = STK.MalKodu LEFT OUTER JOIN
							 FINSAT633.KTK WITH (NOLOCK) ON FINSAT633.KTK.Tip = 105 AND FINSAT633.KTK.Kod = STI.Kod7
	WHERE        (STI.KynkEvrakTip = 1) AND (STI.Tarih BETWEEN @BasTarih AND @BitTarih) AND (STK.TipKod NOT IN ('271', '250')) AND 
							 (CASE WHEN @Kriter = 'TÜMÜ' THEN 1 ELSE CASE WHEN STK.GrupKod = @Kriter THEN 1 ELSE 0 END END = 1) AND 
							 (MONTH(CAST(STI.Tarih - 2 AS smalldatetime)) = @Ay) AND (STI.Kod7 <> '')
	GROUP BY STI.Kod7
	ORDER BY GenelTutar DESC

END
GO
/****** Object:  StoredProcedure [wms].[SetSozlesmeOnayTip]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SetSozlesmeOnayTip]
@HesapKodu VARCHAR(30),
@ListeNo VARCHAR(30),
@BaglantiTutari decimal(24,6)
AS
BEGIN
	--declare @HesapKodu varchar(50)
	--set @HesapKodu='120  A012'
	declare @Tutar decimal(24,6)
	set @Tutar=0

	SELECT  @Tutar=ISNULL(SUM(AA.KalanTutar),0) 
	FROM 
	(
		SELECT DISTINCT 
		(ISS.kod11) 
		as KalanTutar 
		FROM FINSAT633.FINSAT633.ISS(NOLOCK) ISS
		where ISS.MusteriKod=@HesapKodu

		union all 

		select CAST(0 AS DECIMAL) AS KalanTutar
	) AA
--select @tutar
Set @Tutar=ISNULL(@Tutar,0)+@BaglantiTutari




    DECLARE @MalKodGrup SMALLINT,@MalKod VARCHAR(30), @BagParaCinsi VARCHAR(4)

	SELECT @MalKodGrup=MalKodGrup, @MalKod=MalKod, @BagParaCinsi=BaglantiParaCinsi FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) 
	WHERE ListeNo=@ListeNo

	IF ISNULL(@BagParaCinsi,'TL')='TL'
	BEGIN


		IF ((@MalKodGrup=0 AND @MalKod Like '2800%') OR (@MalKodGrup=1 AND @MalKod='FKAĞIT') 
			 OR (@MalKod='M001001000017051' OR @MalKod='M001001000022051'))
		BEGIN
		   Update FINSAT633.FINSAT633.ISS_Temp set OnayTip=2, SatisMuduruOnay=0, FinansmanMuduruOnay=1 
		   Where ListeNo=@ListeNo
		END
		ELSE
		BEGIN
			IF(ISNULL(@Tutar,0)<50000)
			BEGIN

				Update FINSAT633.FINSAT633.ISS_Temp set OnayTip=0 Where ListeNo=@ListeNo
				--update FINSAT633.FINSAT633.ISS_Temp set MikYuzde1=1

			END
			ELSE IF(ISNULL(@Tutar,0)<200000)
			BEGIN

				Update FINSAT633.FINSAT633.ISS_Temp set OnayTip=1 Where ListeNo=@ListeNo
				--update FINSAT633.FINSAT633.ISS_Temp set MikYuzde1=1

			END
			ELSE
			BEGIN

				Update FINSAT633.FINSAT633.ISS_Temp set OnayTip=2 Where ListeNo=@ListeNo
				--update FINSAT633.FINSAT633.ISS_Temp set MikYuzde1=1
			END
		END

	END
	ELSE BEGIN  --USD VE EUR İSE
		Update FINSAT633.FINSAT633.ISS_Temp set OnayTip=2 Where ListeNo=@ListeNo
	END


END








GO
/****** Object:  StoredProcedure [wms].[SiparisKampanyaDetay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[SiparisKampanyaDetay]
@CHK VARCHAR(30),
@EvrakNo VARCHAR(30),
@BasTarih INT,
@BitTarih INT
AS
BEGIN
	
SELECT 
	SPI.EvrakNo,
	CONVERT(DATETIME, SPI.Tarih-2) AS  Tarih,
	SPI.Chk,
	SUM(SPI.Tutar) as Tutar,
	(CASE WHEN STK.GrupKod ='PARKE' THEN ROUND(SUM(SPI.Miktar)/(109.805),0) WHEN STK.GrupKod ='SÜPÜRGELİK' THEN ROUND(SUM(SPI.Miktar)/(78.4),0) ELSE ROUND	(SUM(SPI.Miktar),0) END) AS Miktar,
	(CASE WHEN STK.GrupKod ='PARKE' THEN ROUND(SUM(SPI.TeslimMiktar)/(109.805),0) WHEN STK.GrupKod ='SÜPÜRGELİK' THEN ROUND(SUM(SPI.TeslimMiktar)/(78.4),0)	 ELSE ROUND(SUM(SPI.TeslimMiktar),0) END) AS TeslimMiktar,
	SPI.TeslimChk,
	SPI.Nesne1 as Durum,
	STK.GrupKod ,
	STK.Kod4 AS Koleksiyon,
	(CASE WHEN STK.GrupKod ='PARKE' THEN 'PALET' WHEN STK.GrupKod ='SÜPÜRGELİK' THEN 'PAKET' ELSE SPI.Birim END) AS Birim,
	(CASE WHEN SPI.MalKodu LIKE '89850978%' THEN '6CM' WHEN SPI.MalKodu LIKE '8985095%' THEN '8CM' ELSE '' END) AS SupTip
FROM FINSAT633.FINSAT633.SPI (NOLOCK) 
LEFT JOIN FINSAT633.FINSAT633.STK(NOLOCK) ON SPI.MalKodu= STK.MalKodu
WHERE (SPI.CHK = @CHK) AND KynkEvrakTip = 62 AND SPI.EvrakNo=@EvrakNo AND SPI.Tarih between @BasTarih AND @BitTarih
GROUP BY SPI.EvrakNo,SPI.Tarih,SPI.CHK,SPI.TeslimChk,SPI.Nesne1,GrupKod,STK.Kod4,(CASE WHEN SPI.MalKodu LIKE '89850978%' THEN '6CM' WHEN SPI.MalKodu LIKE '8985095%' THEN '8CM' ELSE '' END),(CASE WHEN STK.GrupKod ='PARKE' THEN 'PALET' WHEN STK.GrupKod ='SÜPÜRGELİK' THEN 'PAKET' ELSE SPI.Birim END)

--select * from FINSAT633.FINSAT633.STK(NOLOCK) where GrupKod='PARKE'


END









GO
/****** Object:  StoredProcedure [wms].[SiparisOnayla]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Last Modify: 25.12.2017
-- Description:	sipariş onay onayla
-- =============================================
CREATE PROCEDURE [wms].[SiparisOnayla]
	@OncekiDurum VARCHAR(20),
	@Kullanici VARCHAR(5),
	@EvrakNo VARCHAR(16)

AS
BEGIN

	DECLARE @Tarih INT
	SET @Tarih = DATEDIFF(dd,0,GETDATE()+2)

	UPDATE       FINSAT633.SPI
	SET                Kod10 = 'Onaylandı', Kod2 = SUBSTRING(@Kullanici, 0, 9), Kod1 = 'sicak', CheckSum = 13, Degistiren = @Kullanici, DegisTarih = @Tarih
	WHERE        (EvrakNo = @EvrakNo) AND (Kod10 = @OncekiDurum)

END
GO
/****** Object:  StoredProcedure [wms].[SiparisOnayList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Last Modify: 25.12.2017
-- Description:	sipariş onay listesi
-- =============================================
CREATE PROCEDURE [wms].[SiparisOnayList]
	@OnayDurm VARCHAR(20),
	@Secim SMALLINT,
	@BasTarih INT,
	@BitTarih INT,
	@ChkAralik VARCHAR(100),
	@TipKodlari VARCHAR(500),
	@Kod3Aralik VARCHAR(100),
	@RiskAralik VARCHAR(500),
	@Kod3Aralik2 VARCHAR(100),
	@RiskAralik2 VARCHAR(500),
	@Grup VARCHAR(50)
AS
BEGIN
DECLARE 
@Tarih INT,
@CHK1 VARCHAR(20),
@CHK2 VARCHAR(20),
@Kod31 VARCHAR(50),
@Kod32 VARCHAR(50),
@Risk1 VARCHAR(20),
@Risk2 VARCHAR(20),
@Kod312 VARCHAR(50),
@Kod322 VARCHAR(50),
@Risk12 VARCHAR(20),
@Risk22 VARCHAR(20)
SET @Tarih = DATEDIFF(dd,0,GETDATE()+2)
 IF PATINDEX('%;%', @ChkAralik) > 0
BEGIN
    SET @CHK1 = SUBSTRING(@ChkAralik,1,PATINDEX('%;%', @ChkAralik)-1)

    SET @CHK2 =  SUBSTRING(@ChkAralik,PATINDEX('%;%', @ChkAralik)+1,50)
END
ELSE
BEGIN
    SET @CHK1 = ''
    SET @CHK2 = 'ZZZZZZZZZZ'
END

IF PATINDEX('%;%', @Kod3Aralik) > 0
BEGIN
	SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
    IF @Kod31 = '' SET @Kod31 = '0'
	ELSE SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    IF @Kod32 = '' SET @Kod32 = '9999999999999999'
	ELSE SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
END
ELSE
BEGIN
    SET @Kod31 = '0'
    SET @Kod32 = '9999999999999999'
END

IF PATINDEX('%;%', @Kod3Aralik2) > 0
BEGIN
	SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
    IF @Kod312 = '' SET @Kod312 = '0'
	ELSE SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    IF @Kod322 = '' SET @Kod322 = '0'
	ELSE SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
END
ELSE
BEGIN
    SET @Kod312 = '0'
    SET @Kod322 = '0'
END

IF PATINDEX('%;%', @RiskAralik) > 0
BEGIN
	SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
	IF @Risk1 = '' SET @Risk1 = '0'
	ELSE SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    IF @Risk2 = '' SET @Risk2 = '9999999999999999'
	ELSE SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
END
ELSE
BEGIN
    SET @Risk1 = '0'
    SET @Risk2 = '9999999999999999'
END

IF PATINDEX('%;%', @RiskAralik2) > 0
BEGIN
	SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
	IF @Risk12 = '' SET @Risk12 = '0'
	ELSE SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    IF @Risk22 = '' SET @Risk22 = '0'
	ELSE SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
END
ELSE
BEGIN
    SET @Risk12 = '0'
    SET @Risk22 = '0'
END

SELECT 
*,
(AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) as RiskBakiyesi,
(((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) as Risk
FROM
(
SELECT 
CONVERT(bit, @Secim) AS Secim,
a.Chk AS HesapKodu,
MAX(d.Unvan1 + ' ' + d.Unvan2) AS Unvan,
d.TipKod AS TipKodu,
CASE WHEN d.KrediLimiti=0 THEN 1 ELSE d.KrediLimiti END AS KrediLimiti,
(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA) AS Bakiye,
AT.SCek AS SCek,
AT.TCek AS TCek,
AT.PRBakiye AS PRTBakiye,
d.Kod2 AS Kod2,
ST.OrtalamaGun AS OrtGun,
ST.Kod3OrtGun AS Kod3OrtGun,
ST.Kod3OrtBakiye AS Kod3OrtBakiye,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.EvrakNo = a.EvrakNo and t.Tarih = @Tarih and t.SiparisDurumu = 0 and t.Kod10 = @OnayDurm), 0) AS SicakSiparis,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Tarih < @Tarih and t.EvrakNo = a.EvrakNo and t.Kod10 = @OnayDurm), 0) AS SogukSiparis ,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Kod10 <> 'Reddedildi' and t.Tarih < @Tarih),0) AS GunIciSiparis,
CASE a.Tarih WHEN @Tarih THEN 'SICAK' ELSE 'SOĞUK' END AS SiparisTuru,
a.EvrakNo AS EvrakNo,
a.Kod10 AS OnayDurumu,
CONVERT(nvarchar(10), CAST(a.Tarih as datetime) - 2, 104) AS Tarih,
a.Kod3 AS Firma,
a.Kod2 AS Onaylayan
FROM FINSAT633.FINSAT633.SPI (NOLOCK)a
inner join FINSAT633.FINSAT633.CHK(NOLOCK) d ON d.HesapKodu = a.Chk
CROSS APPLY FINSAT633.wms.GetOrtGunKod3OrtBakiyeGun(a.Chk,d.Kod2) AS ST
CROSS APPLY FINSAT633.wms.GetSCekTCekPRBakiye(a.Chk) AS AT
WHERE a.Kod10=@OnayDurm  
and a.CHK between @CHK1 AND @CHK2 
and a.Tarih between @BasTarih and @BitTarih
--and a.SiparisDurumu=0 
and case when @OnayDurm='BEKLEMEDE' then case when (a.SiparisDurumu=0 ) then 1 else 0 end end = 1 
and d.TipKod in (select * from dbo.[splitstring](replace(@TipKodlari,';',',')))
and a.TeslimMiktar+a.KapatilanMiktar<Miktar 
and a.Kod13>0 
and (SELECT sum(v.GirMiktar-v.CikMiktar+v.DvrMiktar) FROM FINSAT633.FINSAT633.DST(NOLOCK) v WHERE v.MalKodu=a.MalKodu and v.Depo=a.Depo)>=(Miktar-(a.TeslimMiktar+a.KapatilanMiktar))
--a.Miktar

GROUP BY a.Chk,d.TipKod,d.KrediLimiti,(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA),d.Kod2,a.Tarih,a.EvrakNo,a.Kod10,a.Kod3,a.Kod2,ST.OrtalamaGun,ST.Kod3OrtGun,ST.Kod3OrtBakiye,AT.SCek,AT.TCek,AT.PRBakiye
) AA
WHERE (

	  (
	  (((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk1 and @Risk2) 
	  OR ((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) <0) 
	  )
	  AND
	  (((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod31 and @Kod32)
	  OR ((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) <0)
	  )
	  )
	  
	  OR 

	  (
	  (((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk12 and @Risk22) 
	  OR ((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)<0) 
	  )
	  AND
	  (((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod312 and @Kod322)
	  OR ((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) <0)
	  )
	  )

	  )
	  AND
	  not  (
			(Kod2=8 AND (@Grup<>'Koordinatör'  AND  @Grup<>'Admin')) 
			OR 
			(
				(@Grup='Koordinatör' OR @Grup='Bölge Müdürü' OR @Grup='Finans') 
				AND 
				(
					(AA.Kod3OrtGun>=60 AND @Grup<>'Finans') 
					OR 
					(
						(AA.Kod3OrtGun<60 )
						AND 
						(  
							((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)>=100 AND @Grup<>'Koordinatör') 
							OR 
								(
									((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)<100) 
									AND 
									(	
										(AA.Kod3OrtGun<60 AND AA.Kod3OrtGun>=30 AND @Grup<>'Koordinatör') 
													OR 
										(AA.Kod3OrtGun>=60 AND AA.Kod3OrtGun<30 AND @Grup<>'Bölge Müdürü' AND Kod2<>8)
									)
								)
						)
					)
				)
			)
		)
END






GO
/****** Object:  StoredProcedure [wms].[SiparisOnayList-]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Last Modify: 25.12.2017
-- Description:	sipariş onay listesi
-- =============================================
CREATE PROCEDURE [wms].[SiparisOnayList-]
	@OnayDurm VARCHAR(20),
	@Secim SMALLINT,
	@BasTarih INT,
	@BitTarih INT,
	@ChkAralik VARCHAR(100),
	@TipKodlari VARCHAR(500),
	@Kod3Aralik VARCHAR(100),
	@RiskAralik VARCHAR(500),
	@Kod3Aralik2 VARCHAR(100),
	@RiskAralik2 VARCHAR(500),
	@Grup VARCHAR(50)
AS
BEGIN
DECLARE 
@Tarih INT,
@CHK1 VARCHAR(20),
@CHK2 VARCHAR(20),
@Kod31 VARCHAR(50),
@Kod32 VARCHAR(50),
@Risk1 VARCHAR(20),
@Risk2 VARCHAR(20),
@Kod312 VARCHAR(50),
@Kod322 VARCHAR(50),
@Risk12 VARCHAR(20),
@Risk22 VARCHAR(20)
SET @Tarih = DATEDIFF(dd,0,GETDATE()+2)
 IF PATINDEX('%;%', @ChkAralik) > 0
BEGIN
    SET @CHK1 = SUBSTRING(@ChkAralik,1,PATINDEX('%;%', @ChkAralik)-1)

    SET @CHK2 =  SUBSTRING(@ChkAralik,PATINDEX('%;%', @ChkAralik)+1,50)
END
ELSE
BEGIN
    SET @CHK1 = ''
    SET @CHK2 = 'ZZZZZZZZZZ'
END

IF PATINDEX('%;%', @Kod3Aralik) > 0
BEGIN
	SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
    IF @Kod31 = '' SET @Kod31 = '0'
	ELSE SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    IF @Kod32 = '' SET @Kod32 = '9999999999999999'
	ELSE SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
END
ELSE
BEGIN
    SET @Kod31 = '0'
    SET @Kod32 = '9999999999999999'
END

IF PATINDEX('%;%', @Kod3Aralik2) > 0
BEGIN
	SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
    IF @Kod312 = '' SET @Kod312 = '0'
	ELSE SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    IF @Kod322 = '' SET @Kod322 = '0'
	ELSE SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
END
ELSE
BEGIN
    SET @Kod312 = '0'
    SET @Kod322 = '0'
END

IF PATINDEX('%;%', @RiskAralik) > 0
BEGIN
	SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
	IF @Risk1 = '' SET @Risk1 = '0'
	ELSE SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    IF @Risk2 = '' SET @Risk2 = '9999999999999999'
	ELSE SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
END
ELSE
BEGIN
    SET @Risk1 = '0'
    SET @Risk2 = '9999999999999999'
END

IF PATINDEX('%;%', @RiskAralik2) > 0
BEGIN
	SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
	IF @Risk12 = '' SET @Risk12 = '0'
	ELSE SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    IF @Risk22 = '' SET @Risk22 = '0'
	ELSE SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
END
ELSE
BEGIN
    SET @Risk12 = '0'
    SET @Risk22 = '0'
END

SELECT 
*,
(AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) as RiskBakiyesi,
(((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) as Risk
FROM
(
SELECT 
CONVERT(bit, @Secim) AS Secim,
a.Chk AS HesapKodu,
MAX(d.Unvan1 + ' ' + d.Unvan2) AS Unvan,
d.TipKod AS TipKodu,
CASE WHEN d.KrediLimiti=0 THEN 1 ELSE d.KrediLimiti END AS KrediLimiti,
(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA) AS Bakiye,
AT.SCek AS SCek,
AT.TCek AS TCek,
AT.PRBakiye AS PRTBakiye,
d.Kod2 AS Kod2,
ST.OrtalamaGun AS OrtGun,
ST.Kod3OrtGun AS Kod3OrtGun,
ST.Kod3OrtBakiye AS Kod3OrtBakiye,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.EvrakNo = a.EvrakNo and t.Tarih = @Tarih and t.SiparisDurumu = 0 and t.Kod10 = @OnayDurm), 0) AS SicakSiparis,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Tarih < @Tarih and t.EvrakNo = a.EvrakNo and t.Kod10 = @OnayDurm), 0) AS SogukSiparis ,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Kod10 <> 'Reddedildi' and t.Tarih < @Tarih),0) AS GunIciSiparis,
CASE a.Tarih WHEN @Tarih THEN 'SICAK' ELSE 'SOĞUK' END AS SiparisTuru,
a.EvrakNo AS EvrakNo,
a.Kod10 AS OnayDurumu,
CONVERT(nvarchar(10), CAST(a.Tarih as datetime) - 2, 104) AS Tarih,
a.Kod3 AS Firma,
a.Kod2 AS Onaylayan
FROM FINSAT633.FINSAT633.SPI (NOLOCK)a
inner join FINSAT633.FINSAT633.CHK(NOLOCK) d ON d.HesapKodu = a.Chk
CROSS APPLY FINSAT633.wms.GetOrtGunKod3OrtBakiyeGun(a.Chk,d.Kod2) AS ST
CROSS APPLY FINSAT633.wms.GetSCekTCekPRBakiye(a.Chk) AS AT
WHERE a.Kod10=@OnayDurm  
and a.CHK between @CHK1 AND @CHK2 
and a.Tarih between @BasTarih and @BitTarih
--and a.SiparisDurumu=0 
and case when @OnayDurm='BEKLEMEDE' then case when (a.SiparisDurumu=0 ) then 1 else 0 end end = 1 
and d.TipKod in (select * from dbo.[splitstring](replace(@TipKodlari,';',',')))
and a.TeslimMiktar+a.KapatilanMiktar<Miktar 
and a.Kod13>0 
and (SELECT sum(v.GirMiktar-v.CikMiktar+v.DvrMiktar) FROM FINSAT633.FINSAT633.DST(NOLOCK) v WHERE v.MalKodu=a.MalKodu and v.Depo=a.Depo)>=a.Miktar

GROUP BY a.Chk,d.TipKod,d.KrediLimiti,(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA),d.Kod2,a.Tarih,a.EvrakNo,a.Kod10,a.Kod3,a.Kod2,ST.OrtalamaGun,ST.Kod3OrtGun,ST.Kod3OrtBakiye,AT.SCek,AT.TCek,AT.PRBakiye
) AA
WHERE (

	  (
	  ((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk1 and @Risk2) 
	  AND
	  ((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod31 and @Kod32)
	  )
	  
	  OR 

	  (
	  ((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk12 and @Risk22) 
	  AND
	  ((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod312 and @Kod322)
	  )

	  )
	  AND
	  not  (
			(Kod2=8 AND (@Grup<>'Satış Müdürü'  AND  @Grup<>'Admin')) 
			OR 
			(
				(@Grup='Satış Müdürü' OR @Grup='Bölge Sorumlusu' OR @Grup='Finans') 
				AND 
				(
					(AA.Kod3OrtGun>=60 AND @Grup<>'Finans') 
					OR 
					(
						(AA.Kod3OrtGun<60 )
						AND 
						(  
							((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)>=100 AND @Grup<>'Satış Müdürü') 
							OR 
								(
									((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)<100) 
									AND 
									(	
										(AA.Kod3OrtGun<60 AND AA.Kod3OrtGun>=30 AND @Grup<>'Satış Müdürü') 
													OR 
										(AA.Kod3OrtGun>=60 AND AA.Kod3OrtGun<30 AND @Grup<>'Bölge Sorumlusu' AND Kod2<>8)
									)
								)
						)
					)
				)
			)
		)
END






GO
/****** Object:  StoredProcedure [wms].[SiparisOnayList----]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [wms].[SiparisOnayList----]
	@OnayDurm VARCHAR(20),
	@Secim SMALLINT,
	@BasTarih INT,
	@BitTarih INT,
	@ChkAralik VARCHAR(100),
	@Sirketler VARCHAR(100),
	@TipKodlari VARCHAR(500),
	@Kod3Aralik VARCHAR(100),
	@RiskAralik VARCHAR(500),
	@Kod3Aralik2 VARCHAR(100),
	@RiskAralik2 VARCHAR(500),
	@Grup VARCHAR(50)
AS
BEGIN
DECLARE 
@Tarih INT,
@CHK1 VARCHAR(20),
@CHK2 VARCHAR(20),
@Kod31 VARCHAR(50),
@Kod32 VARCHAR(50),
@Risk1 VARCHAR(20),
@Risk2 VARCHAR(20),
@Kod312 VARCHAR(50),
@Kod322 VARCHAR(50),
@Risk12 VARCHAR(20),
@Risk22 VARCHAR(20)
SET @Tarih = DATEDIFF(dd,0,GETDATE()+2)
 IF PATINDEX('%;%', @ChkAralik) > 0
BEGIN
    SET @CHK1 = SUBSTRING(@ChkAralik,1,PATINDEX('%;%', @ChkAralik)-1)

    SET @CHK2 =  SUBSTRING(@ChkAralik,PATINDEX('%;%', @ChkAralik)+1,50)
END
ELSE
BEGIN
    SET @CHK1 = ''
    SET @CHK2 = 'ZZZZZZZZZZ'
END

IF PATINDEX('%;%', @Kod3Aralik) > 0
BEGIN
	SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
    IF @Kod31 = '' SET @Kod31 = '0'
	ELSE SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    IF @Kod32 = '' SET @Kod32 = '9999999999999999'
	ELSE SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
END
ELSE
BEGIN
    SET @Kod31 = '0'
    SET @Kod32 = '9999999999999999'
END

IF PATINDEX('%;%', @Kod3Aralik2) > 0
BEGIN
	SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
    IF @Kod312 = '' SET @Kod312 = '0'
	ELSE SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    IF @Kod322 = '' SET @Kod322 = '9999999999999999'
	ELSE SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
END
ELSE
BEGIN
    SET @Kod312 = '0'
    SET @Kod322 = '9999999999999999'
END

IF PATINDEX('%;%', @RiskAralik) > 0
BEGIN
	SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
	IF @Risk1 = '' SET @Risk1 = '0'
	ELSE SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    IF @Risk2 = '' SET @Risk2 = '9999999999999999'
	ELSE SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
END
ELSE
BEGIN
    SET @Risk1 = '0'
    SET @Risk2 = '9999999999999999'
END

IF PATINDEX('%;%', @RiskAralik2) > 0
BEGIN
	SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
	IF @Risk12 = '' SET @Risk12 = '0'
	ELSE SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    IF @Risk22 = '' SET @Risk22 = '9999999999999999'
	ELSE SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
END
ELSE
BEGIN
    SET @Risk12 = '0'
    SET @Risk22 = '9999999999999999'
END

SELECT 
		*,
		(AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) as RiskBakiyesi,
		(((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) as Risk
FROM
(
	SELECT 
		CONVERT(bit, @Secim) AS Secim,
		a.Chk AS HesapKodu,
		MAX(d.Unvan1 + ' ' + d.Unvan2) AS Unvan,
		d.TipKod AS TipKodu,
		CASE WHEN d.KrediLimiti=0 THEN 1 ELSE d.KrediLimiti END AS KrediLimiti,
		(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA) AS Bakiye,
		AT.SCek AS SCek,
		AT.TCek AS TCek,
		AT.PRBakiye AS PRTBakiye,
		d.Kod2 AS Kod2,
		ST.OrtalamaGun AS OrtGun,
		ST.Kod3OrtGun AS Kod3OrtGun,
		ST.Kod3OrtBakiye AS Kod3OrtBakiye,
		ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.EvrakNo = a.EvrakNo and t.Tarih = @Tarih and t.SiparisDurumu = 0 and t.Kod10 = @OnayDurm), 0) AS SicakSiparis,
		ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Tarih < @Tarih and t.EvrakNo = a.EvrakNo and t.Kod10 = @OnayDurm), 0) AS SogukSiparis ,
		ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Kod10 <> 'Reddedildi' and t.Tarih < @Tarih),0) AS GunIciSiparis,
		CASE a.Tarih WHEN @Tarih THEN 'SICAK' ELSE 'SOĞUK' END AS SiparisTuru,
		a.EvrakNo AS EvrakNo,
		a.Kod10 AS OnayDurumu,
		CONVERT(nvarchar(10), CAST(a.Tarih as datetime) - 2, 104) AS Tarih,
		a.Kod3 AS Firma,
		a.Kod2 AS Onaylayan
	FROM FINSAT633.FINSAT633.SPI (NOLOCK)a
		inner join FINSAT633.FINSAT633.CHK(NOLOCK) d ON d.HesapKodu = a.Chk
		CROSS APPLY FINSAT633.wms.GetOrtGunKod3OrtBakiyeGun(a.Chk,d.Kod2) AS ST
		CROSS APPLY FINSAT633.wms.GetSCekTCekPRBakiye(a.Chk) AS AT
	WHERE a.Kod10=@OnayDurm  
		and a.CHK between @CHK1 AND @CHK2 
		and a.Tarih between @BasTarih and @BitTarih
		and case when @OnayDurm='BEKLEMEDE' then case when (a.SiparisDurumu=0 ) then 1 else 0 end else 1 end  = 1 
		and d.TipKod in (select * from wms.[splitstring](replace(@TipKodlari,';',',')))
		and a.TeslimMiktar+a.KapatilanMiktar<Miktar 
		and a.Kod13>0 
		and a.Kod3 in (select * from wms.[splitstring](replace(@Sirketler,';',',')))  
		and (SELECT sum(v.GirMiktar-v.CikMiktar+v.DvrMiktar) FROM FINSAT633.FINSAT633.DST(NOLOCK) v WHERE v.MalKodu=a.MalKodu and v.Depo=a.Depo)>=a.Miktar

	GROUP BY a.Chk,d.TipKod,d.KrediLimiti,(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA),d.Kod2,a.Tarih,a.EvrakNo,a.Kod10,a.Kod3,a.Kod2,ST.OrtalamaGun,ST.Kod3OrtGun,ST.Kod3OrtBakiye,AT.SCek,AT.TCek,AT.PRBakiye
) AA
WHERE 	 
(
	(@Grup='Finans' AND Kod2 like '%8%') OR
	(@Grup<>'Finans' AND Kod2 not like '%8%') or 
	(@Grup='Bölge Sorumlusu' AND Kod2=9)
)
and 
(
	(
		@Grup='Sipariş Masası' OR  
		@Grup='Admin' OR 
		@Grup='Depo Sorumlusu' OR 
		(@Grup='GM' and   
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk1 and @Risk2) 
			AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod31 and @Kod32)
			)        
			OR 
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk12 and @Risk22) 
			AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod312 and @Kod322)
			)
		)
	)
	OR
	(
		(
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk1 and @Risk2) 
				AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod31 and @Kod32)
			)	  
			OR 
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk12 and @Risk22) 
				AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod312 and @Kod322)
			)
		)
		AND	 
		(
			(
				Kod2 LIKE '%8%'AND (@Grup='Finans'  or  @Grup='Admin')
			) 
			OR 
			(
				(
					@Grup<>'Satış Müdürü' OR 
					@Grup<>'Bölge Sorumlusu' OR 
					@Grup<>'Finans'
				) 
				AND 
				(
					(AA.Kod3OrtGun>=60 AND @Grup='Finans') 
					OR 
					(
						(AA.Kod3OrtGun<60 )
						AND 
						(  
							((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)>=125 AND @Grup='Satış Müdürü') 
							OR 
							(
								((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)<125) 
								AND 		
								(	
									(AA.Kod3OrtGun<60 AND AA.Kod3OrtGun>=30 AND @Grup='Satış Müdürü') 
									OR 
									(AA.Kod3OrtGun>=0 AND AA.Kod3OrtGun<30 AND @Grup='Bölge Sorumlusu' )										
								)
							)
						)
					)
				)
			)
		)
	)
)
END


GO
/****** Object:  StoredProcedure [wms].[SiparisOnayListGM]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [wms].[SiparisOnayListGM]
AS
BEGIN
	
SELECT S1.Onay, S1.EvrakNo, S1.Tarih, S1.HesapNo, S1.Chk, S1.Unvan, S1.TeslimYeriKodu,
       S1.TeslimYeriUnvan, S1.SiparisTutari, ISNULL(S3.Bakiye,S1.Bakiye) AS Bakiye, 
	   ISNULL(S3.RiskBakiyesi,S1.RiskBakiyesi) AS RiskBakiyesi,
       ISNULL(S3.ToplamBakiye,S1.ToplamBakiye) AS ToplamBakiye, 
	   ISNULL(S3.SahsiCekLimiti,S1.SahsiCekLimiti) AS SahsiCekLimiti, 
	   ISNULL(S3.SahsiCekRiski,S1.SahsiCekRiski) AS SahsiCekRiski, 
	   ISNULL(S3.MusteriCekLimiti,S1.MusteriCekLimiti) AS MusteriCekLimiti, 
	   ISNULL(S3.MusteriCekRiski,S1.MusteriCekRiski) AS MusteriCekRiski,
	   ISNULL(S3.BekleyenSiparisTutari,S1.BekleyenSiparisTutari) AS BekleyenSiparisTutari ,
	   S1.Teminat, S1.TeminatAltBayi 
FROM
(
SELECT
       CAST(0 AS BIT) AS Onay,	Siparis.EvrakNo, Siparis.Tarih, CHK.HesapNo,
       Siparis.Chk, (CHK.Unvan1 +''+ CHK.Unvan2) AS Unvan, 
Siparis.TeslimChk AS TeslimYeriKodu,
(CHK2.Unvan1 +''+ CHK2.Unvan2) AS TeslimYeriUnvan, 
Siparis.Tutar AS SiparisTutari,

(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) AS Bakiye,
 
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=Siparis.Chk AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=Siparis.Chk AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS RiskBakiyesi,



(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) +
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=Siparis.Chk AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=Siparis.Chk AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS ToplamBakiye,


CHK.Kod12 AS SahsiCekLimiti, 

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE (ACK.Veren=ACK.Borclu AND ACK.Veren=CHK.HesapKodu OR ACK.BorcluUnvan1 LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%') 
	AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS SahsiCekRiski,


CHK.Kod13 AS MusteriCekLimiti,

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE ACK.Veren <> ACK.Borclu AND ACK.BorcluUnvan1 NOT LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%' AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS MusteriCekRiski,


ISNULL((
	SELECT SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV)  
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.SiparisDurumu=0 AND SPI.Chk=Siparis.Chk and SPI.Kod10 in ('Sevk Edilebilir','Onaylandı')
	GROUP BY SPI.Chk
),0) AS BekleyenSiparisTutari,


ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.Teminat(NOLOCK) 
	WHERE HesapKodu=LEFT(Siparis.Chk,15) AND Onay=1 AND ( SureliSuresiz=0 OR (SureliSuresiz=1 AND VadeTarih>GETDATE()))
),0) AS Teminat, 
ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.Teminat(NOLOCK) 
	WHERE HesapKodu=Siparis.TeslimChk AND Onay=1 AND ( SureliSuresiz=0 OR (SureliSuresiz=1 AND VadeTarih=GETDATE()))
),0) AS TeminatAltBayi
FROM 
(
	SELECT SPI.EvrakNo, 
	CONVERT(VARCHAR(15), CONVERT(DATETIME, SPI.Tarih-2),104) AS Tarih, 
	SPI.Chk,
	SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV) AS Tutar,
	SPI.TeslimChk, 
	SPI.Kod10, 
	SO.OnayTip AS Kod11
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	INNER JOIN FINSAT633.FINSAT633.SiparisOnay (NOLOCK) SO ON SPI.EvrakNo = SO.EvrakNo
	WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 
	AND SO.Durum IN (1)
	AND 
	(
		(SO.OnayTip = 3 AND SO.SMOnay = 1 AND SO.SPGMYOnay = 1 AND SO.GMOnay = 0)
	)
	GROUP BY SPI.EvrakNo, SPI.Tarih, SPI.Chk, SPI.TeslimChk, SPI.Kod10, SO.OnayTip
) Siparis
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON Siparis.Chk=CHK.HesapKodu
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK2 ON Siparis.TeslimChk=CHK2.HesapKodu
)  S1


LEFT JOIN
(SELECT S2.HesapNo, SUM(S2.Bakiye) AS Bakiye, SUM(S2.RiskBakiyesi) AS RiskBakiyesi,
        SUM(S2.ToplamBakiye) AS ToplamBakiye, SUM(S2.SahsiCekLimiti) AS SahsiCekLimiti, 
		SUM(S2.SahsiCekRiski) AS SahsiCekRiski, SUM(S2.MusteriCekLimiti) AS MusteriCekLimiti, 
	    SUM(S2.MusteriCekRiski) AS MusteriCekRiski, SUM(S2.BekleyenSiparisTutari) AS BekleyenSiparisTutari 		
FROM
(
SELECT

CHK.HesapNo,

 
(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) AS Bakiye,

ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=CHK.HesapKodu AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS RiskBakiyesi,



(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) +
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=CHK.HesapKodu AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS ToplamBakiye,


CHK.Kod12 AS SahsiCekLimiti, 

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE (ACK.Veren=ACK.Borclu AND ACK.Veren=CHK.HesapKodu OR ACK.BorcluUnvan1 LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%') 
	AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS SahsiCekRiski,


CHK.Kod13 AS MusteriCekLimiti,

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE ACK.Veren <> ACK.Borclu AND ACK.BorcluUnvan1 NOT LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%' AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS MusteriCekRiski,


ISNULL((
	SELECT SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV)  
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.SiparisDurumu=0 AND SPI.Chk=CHK.HesapKodu and SPI.Kod10 in ('Sevk Edilebilir','Onaylandı')
	GROUP BY SPI.Chk
),0) AS BekleyenSiparisTutari
FROM 
 FINSAT633.FINSAT633.CHK(NOLOCK) 
 WHERE CHK.Hesapkodu not like '340%'
) S2
WHERE S2.HesapNo<>''
GROUP BY S2.HesapNo
) S3 ON S1.HesapNo = S3.HesapNo


END
GO
/****** Object:  StoredProcedure [wms].[SiparisOnayListSM]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SiparisOnayListSM]
AS
BEGIN
	
SELECT S1.Onay, S1.EvrakNo, S1.Tarih, 
       S1.HesapNo, 
	   S1.Chk, S1.Unvan, S1.TeslimYeriKodu,
       S1.TeslimYeriUnvan, S1.SiparisTutari, ISNULL(S3.Bakiye,S1.Bakiye) AS Bakiye, 
	   ISNULL(S3.RiskBakiyesi,S1.RiskBakiyesi) AS RiskBakiyesi,
       ISNULL(S3.ToplamBakiye,S1.ToplamBakiye) AS ToplamBakiye, 
	   ISNULL(S3.SahsiCekLimiti,S1.SahsiCekLimiti) AS SahsiCekLimiti, 
	   ISNULL(S3.SahsiCekRiski,S1.SahsiCekRiski) AS SahsiCekRiski, 
	   ISNULL(S3.MusteriCekLimiti,S1.MusteriCekLimiti) AS MusteriCekLimiti, 
	   ISNULL(S3.MusteriCekRiski,S1.MusteriCekRiski) AS MusteriCekRiski,
	   ISNULL(S3.BekleyenSiparisTutari,S1.BekleyenSiparisTutari) AS BekleyenSiparisTutari , 
	   S1.Teminat, S1.TeminatAltBayi 
FROM
(
SELECT
       CAST(0 AS BIT) AS Onay,	Siparis.EvrakNo, Siparis.Tarih, CHK.HesapNo,
       Siparis.Chk, (CHK.Unvan1 +''+ CHK.Unvan2) AS Unvan, 
Siparis.TeslimChk AS TeslimYeriKodu,
(CHK2.Unvan1 +''+ CHK2.Unvan2) AS TeslimYeriUnvan, 
Siparis.Tutar AS SiparisTutari,

(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) AS Bakiye,
 
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=Siparis.Chk AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=Siparis.Chk AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS RiskBakiyesi,



(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) +
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=Siparis.Chk AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=Siparis.Chk AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS ToplamBakiye,


CHK.Kod12 AS SahsiCekLimiti, 

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE (ACK.Veren=ACK.Borclu AND ACK.Veren=CHK.HesapKodu OR ACK.BorcluUnvan1 LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%') 
	AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS SahsiCekRiski,


CHK.Kod13 AS MusteriCekLimiti,

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE ACK.Veren <> ACK.Borclu AND ACK.BorcluUnvan1 NOT LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%' AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS MusteriCekRiski,


ISNULL((
	SELECT SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV)  
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.SiparisDurumu=0 AND SPI.Chk=Siparis.Chk and SPI.Kod10 in ('Sevk Edilebilir','Onaylandı')
	GROUP BY SPI.Chk
),0) AS BekleyenSiparisTutari,


ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.Teminat(NOLOCK) 
	WHERE HesapKodu=LEFT(Siparis.Chk,15) AND Onay=1 AND ( SureliSuresiz=0 OR (SureliSuresiz=1 AND VadeTarih>GETDATE()))
),0) AS Teminat, 
ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.Teminat(NOLOCK) 
	WHERE HesapKodu=Siparis.TeslimChk AND Onay=1 AND ( SureliSuresiz=0 OR (SureliSuresiz=1 AND VadeTarih>=GETDATE()))
),0) AS TeminatAltBayi
FROM 
(
	SELECT SPI.EvrakNo, 
	CONVERT(VARCHAR(15), CONVERT(DATETIME, SPI.Tarih-2),104) AS Tarih, 
	SPI.Chk,
	SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV) AS Tutar,
	SPI.TeslimChk, 
	SPI.Kod10, 
	SO.OnayTip AS Kod11
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	INNER JOIN FINSAT633.FINSAT633.SiparisOnay (NOLOCK) SO ON SPI.EvrakNo = SO.EvrakNo
	WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 
	AND SO.Durum IN (1)
	AND 
	(
	    (SO.OnayTip = 1 AND SO.SMOnay = 0) OR
		(SO.OnayTip = 2 AND SO.SMOnay = 0 AND SO.SPGMYOnay = 0) OR
		(SO.OnayTip = 3 AND SO.SMOnay = 0 AND SO.SPGMYOnay = 0 AND SO.GMOnay = 0)
		 OR (SO.OnayTip = 3 AND SO.SMOnay = 0 ) --16.04.2015 te bu satır eklendi 
	)
	GROUP BY SPI.EvrakNo, SPI.Tarih, SPI.Chk, SPI.TeslimChk, SPI.Kod10, SO.OnayTip
) Siparis
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON Siparis.Chk=CHK.HesapKodu
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK2 ON Siparis.TeslimChk=CHK2.HesapKodu
)  S1


LEFT JOIN
(SELECT S2.HesapNo, SUM(S2.Bakiye) AS Bakiye, SUM(S2.RiskBakiyesi) AS RiskBakiyesi,
        SUM(S2.ToplamBakiye) AS ToplamBakiye, SUM(S2.SahsiCekLimiti) AS SahsiCekLimiti, 
		SUM(S2.SahsiCekRiski) AS SahsiCekRiski, SUM(S2.MusteriCekLimiti) AS MusteriCekLimiti, 
	    SUM(S2.MusteriCekRiski) AS MusteriCekRiski, SUM(S2.BekleyenSiparisTutari) AS BekleyenSiparisTutari 		
FROM
(
SELECT

CHK.HesapNo,

 
(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) AS Bakiye,

ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=CHK.HesapKodu AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS RiskBakiyesi,



(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) +
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=CHK.HesapKodu AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS ToplamBakiye,


CHK.Kod12 AS SahsiCekLimiti, 

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE (ACK.Veren=ACK.Borclu AND ACK.Veren=CHK.HesapKodu OR ACK.BorcluUnvan1 LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%') 
	AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS SahsiCekRiski,


CHK.Kod13 AS MusteriCekLimiti,

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE ACK.Veren <> ACK.Borclu AND ACK.BorcluUnvan1 NOT LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%' AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS MusteriCekRiski,


ISNULL((
	SELECT SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV)  
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.SiparisDurumu=0 AND SPI.Chk=CHK.HesapKodu and SPI.Kod10 in ('Sevk Edilebilir','Onaylandı')
	GROUP BY SPI.Chk
),0) AS BekleyenSiparisTutari
FROM 
 FINSAT633.FINSAT633.CHK(NOLOCK) 
  WHERE CHK.Hesapkodu not like '340%'
) S2
WHERE S2.HesapNo<>''
GROUP BY S2.HesapNo
) S3 ON S1.HesapNo = S3.HesapNo


END
GO
/****** Object:  StoredProcedure [wms].[SiparisOnayListSPGMY]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SiparisOnayListSPGMY]
AS
BEGIN
	
SELECT S1.Onay, S1.EvrakNo, S1.Tarih, S1.HesapNo, S1.Chk, S1.Unvan, S1.TeslimYeriKodu,
       S1.TeslimYeriUnvan, S1.SiparisTutari, ISNULL(S3.Bakiye,S1.Bakiye) AS Bakiye, 
	   ISNULL(S3.RiskBakiyesi,S1.RiskBakiyesi) AS RiskBakiyesi,
       ISNULL(S3.ToplamBakiye,S1.ToplamBakiye) AS ToplamBakiye, 
	   ISNULL(S3.SahsiCekLimiti,S1.SahsiCekLimiti) AS SahsiCekLimiti, 
	   ISNULL(S3.SahsiCekRiski,S1.SahsiCekRiski) AS SahsiCekRiski, 
	   ISNULL(S3.MusteriCekLimiti,S1.MusteriCekLimiti) AS MusteriCekLimiti, 
	   ISNULL(S3.MusteriCekRiski,S1.MusteriCekRiski) AS MusteriCekRiski,
	   ISNULL(S3.BekleyenSiparisTutari,S1.BekleyenSiparisTutari) AS BekleyenSiparisTutari ,
	    S1.Teminat, S1.TeminatAltBayi 
FROM
(
SELECT
       CAST(0 AS BIT) AS Onay,	Siparis.EvrakNo, Siparis.Tarih, CHK.HesapNo,
       Siparis.Chk, (CHK.Unvan1 +''+ CHK.Unvan2) AS Unvan, 
Siparis.TeslimChk AS TeslimYeriKodu,
(CHK2.Unvan1 +''+ CHK2.Unvan2) AS TeslimYeriUnvan, 
Siparis.Tutar AS SiparisTutari,

(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) AS Bakiye,
 
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=Siparis.Chk AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=Siparis.Chk AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS RiskBakiyesi,



(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) +
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=Siparis.Chk AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=Siparis.Chk AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS ToplamBakiye,


CHK.Kod12 AS SahsiCekLimiti, 

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE (ACK.Veren=ACK.Borclu AND ACK.Veren=CHK.HesapKodu OR ACK.BorcluUnvan1 LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%') 
	AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS SahsiCekRiski,


CHK.Kod13 AS MusteriCekLimiti,

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE ACK.Veren <> ACK.Borclu AND ACK.BorcluUnvan1 NOT LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%' AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS MusteriCekRiski,


ISNULL((
	SELECT SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV)  
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.SiparisDurumu=0 AND SPI.Chk=Siparis.Chk and SPI.Kod10 in ('Sevk Edilebilir','Onaylandı')
	GROUP BY SPI.Chk
),0) AS BekleyenSiparisTutari,


ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.Teminat(NOLOCK) 
	WHERE HesapKodu=LEFT(Siparis.Chk,15) AND Onay=1 AND ( SureliSuresiz=0 OR (SureliSuresiz=1 AND VadeTarih>GETDATE()))
),0) AS Teminat, 
ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.Teminat(NOLOCK) 
	WHERE HesapKodu=Siparis.TeslimChk AND Onay=1 AND ( SureliSuresiz=0 OR (SureliSuresiz=1 AND VadeTarih>GETDATE()))
),0) AS TeminatAltBayi
FROM 
(
	SELECT SPI.EvrakNo, 
	CONVERT(VARCHAR(15), CONVERT(DATETIME, SPI.Tarih-2),104) AS Tarih, 
	SPI.Chk,
	SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV) AS Tutar,
	SPI.TeslimChk, 
	SPI.Kod10, 
	SO.OnayTip AS Kod11
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	INNER JOIN FINSAT633.FINSAT633.SiparisOnay (NOLOCK) SO ON SPI.EvrakNo = SO.EvrakNo
	WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 
	AND SO.Durum IN (1)
	AND 
	(
	   (SO.OnayTip = 2 AND SO.SMOnay = 1 AND SO.SPGMYOnay = 0) OR
	   (SO.OnayTip = 3 AND SO.SMOnay = 1 AND SO.SPGMYOnay = 0 AND SO.GMOnay = 0)
	)
	GROUP BY SPI.EvrakNo, SPI.Tarih, SPI.Chk, SPI.TeslimChk, SPI.Kod10, SO.OnayTip
) Siparis
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON Siparis.Chk=CHK.HesapKodu
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK2 ON Siparis.TeslimChk=CHK2.HesapKodu
)  S1


LEFT JOIN
(SELECT S2.HesapNo, SUM(S2.Bakiye) AS Bakiye, SUM(S2.RiskBakiyesi) AS RiskBakiyesi,
        SUM(S2.ToplamBakiye) AS ToplamBakiye, SUM(S2.SahsiCekLimiti) AS SahsiCekLimiti, 
		SUM(S2.SahsiCekRiski) AS SahsiCekRiski, SUM(S2.MusteriCekLimiti) AS MusteriCekLimiti, 
	    SUM(S2.MusteriCekRiski) AS MusteriCekRiski, SUM(S2.BekleyenSiparisTutari) AS BekleyenSiparisTutari 		
FROM
(
SELECT

CHK.HesapNo,

 
(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) AS Bakiye,

ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=CHK.HesapKodu AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS RiskBakiyesi,



(CHK.DvrB+CHK.OdemeB+CHK.CiroB+CHK.IadeFatB+CHK.KDVB+CHK.DigerB-CHK.DvrA-CHK.OdemeA-CHK.CiroA-CHK.IadeFatA-CHK.KDVA-CHK.DigerA) +
ISNULL((
SELECT SUM(Tutar) FROM (
SELECT 'ÇEK' AS Tip, CekNo , ACK.EvrakNo, ACK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ACK.VadeTarih-2),104) VadeTarih, ACK.Tutar, ISNULL(ACK.BorcluUnvan1+' '+ACK.BorcluUnvan2,'') AS BorcluUnvan,
(SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ACK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ACK (NOLOCK) ACK
WHERE ACK.SonIslemTip NOT IN (5,12,13) AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
UNION ALL
SELECT 'SENET' AS Tip, '' AS CekNo, ASK.EvrakNo, ASK.Veren, CONVERT(VARCHAR(15),CONVERT(DATETIME,ASK.VadeTarih-2),104) VadeTarih, ASK.Tutar, ISNULL(ASK.BorcluUnvan1+' '+ASK.BorcluUnvan2,'') AS BorcluUnvan, (SELECT ITEMNAME FROM FINSAT633.FINSAT633.COMBOITEM_NAME WHERE ITEMCBOID=13 AND ID=ASK.SonIslemTip) AS Durum
FROM FINSAT633.FINSAT633.ASK (NOLOCK) ASK 
WHERE ASK.SonIslemTip NOT IN (5,12,13) AND ASK.Veren=CHK.HesapKodu AND ASK.VadeTarih>DATEDIFF(DD,0,GETDATE()+2)
) AAA),0) AS ToplamBakiye,


CHK.Kod12 AS SahsiCekLimiti, 

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE (ACK.Veren=ACK.Borclu AND ACK.Veren=CHK.HesapKodu OR ACK.BorcluUnvan1 LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%') 
	AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS SahsiCekRiski,


CHK.Kod13 AS MusteriCekLimiti,

ISNULL((
	SELECT SUM(Tutar)
	FROM FINSAT633.FINSAT633.ACK(NOLOCK) ACK
	WHERE ACK.Veren <> ACK.Borclu AND ACK.BorcluUnvan1 NOT LIKE '%'+SUBSTRING(CHK.Unvan1, 0, 10)+'%' AND ACK.Veren=CHK.HesapKodu AND ACK.VadeTarih >= DATEDIFF(dd,0,GETDATE()+2) AND ACK.SonIslemTip NOT IN (5, 12, 13)
	GROUP BY ACK.Veren
),0) AS MusteriCekRiski,


ISNULL((
	SELECT SUM(SPI.Tutar-SPI.ToplamIskonto+SPI.KDV)  
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.SiparisDurumu=0 AND SPI.Chk=CHK.HesapKodu and SPI.Kod10 in ('Sevk Edilebilir','Onaylandı')
	GROUP BY SPI.Chk
),0) AS BekleyenSiparisTutari
FROM 
 FINSAT633.FINSAT633.CHK(NOLOCK) 
  WHERE CHK.Hesapkodu not like '340%'
) S2
WHERE S2.HesapNo<>''
GROUP BY S2.HesapNo
) S3 ON S1.HesapNo = S3.HesapNo


END
GO
/****** Object:  StoredProcedure [wms].[SiparisOnayList-yedek]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Last Modify: 25.12.2017
-- Description:	sipariş onay listesi
-- =============================================
CREATE PROCEDURE [wms].[SiparisOnayList-yedek]
	@OnayDurm VARCHAR(20),
	@Secim SMALLINT,
	@BasTarih INT,
	@BitTarih INT,
	@ChkAralik VARCHAR(100),
	@TipKodlari VARCHAR(500),
	@Kod3Aralik VARCHAR(100),
	@RiskAralik VARCHAR(500),
	@Kod3Aralik2 VARCHAR(100),
	@RiskAralik2 VARCHAR(500),
	@Grup VARCHAR(50)
AS
BEGIN
DECLARE 
@Tarih INT,
@CHK1 VARCHAR(20),
@CHK2 VARCHAR(20),
@Kod31 VARCHAR(50),
@Kod32 VARCHAR(50),
@Risk1 VARCHAR(20),
@Risk2 VARCHAR(20),
@Kod312 VARCHAR(50),
@Kod322 VARCHAR(50),
@Risk12 VARCHAR(20),
@Risk22 VARCHAR(20)
SET @Tarih = DATEDIFF(dd,0,GETDATE()+2)
 IF PATINDEX('%;%', @ChkAralik) > 0
BEGIN
    SET @CHK1 = SUBSTRING(@ChkAralik,1,PATINDEX('%;%', @ChkAralik)-1)

    SET @CHK2 =  SUBSTRING(@ChkAralik,PATINDEX('%;%', @ChkAralik)+1,50)
END
ELSE
BEGIN
    SET @CHK1 = ''
    SET @CHK2 = 'ZZZZZZZZZZ'
END

IF PATINDEX('%;%', @Kod3Aralik) > 0
BEGIN
	SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
    IF @Kod31 = '' SET @Kod31 = '0'
	ELSE SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    IF @Kod32 = '' SET @Kod32 = '9999999999999999'
	ELSE SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
END
ELSE
BEGIN
    SET @Kod31 = '0'
    SET @Kod32 = '9999999999999999'
END

IF PATINDEX('%;%', @Kod3Aralik2) > 0
BEGIN
	SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
    IF @Kod312 = '' SET @Kod312 = '0'
	ELSE SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    IF @Kod322 = '' SET @Kod322 = '0'
	ELSE SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
END
ELSE
BEGIN
    SET @Kod312 = '0'
    SET @Kod322 = '0'
END

IF PATINDEX('%;%', @RiskAralik) > 0
BEGIN
	SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
	IF @Risk1 = '' SET @Risk1 = '0'
	ELSE SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    IF @Risk2 = '' SET @Risk2 = '9999999999999999'
	ELSE SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
END
ELSE
BEGIN
    SET @Risk1 = '0'
    SET @Risk2 = '9999999999999999'
END

IF PATINDEX('%;%', @RiskAralik2) > 0
BEGIN
	SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
	IF @Risk12 = '' SET @Risk12 = '0'
	ELSE SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    IF @Risk22 = '' SET @Risk22 = '0'
	ELSE SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
END
ELSE
BEGIN
    SET @Risk12 = '0'
    SET @Risk22 = '0'
END

SELECT 
*,
(AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) as RiskBakiyesi,
(((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) as Risk
FROM
(
SELECT 
CONVERT(bit, @Secim) AS Secim,
a.Chk AS HesapKodu,
MAX(d.Unvan1 + ' ' + d.Unvan2) AS Unvan,
d.TipKod AS TipKodu,
CASE WHEN d.KrediLimiti=0 THEN 1 ELSE d.KrediLimiti END AS KrediLimiti,
(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA) AS Bakiye,
AT.SCek AS SCek,
AT.TCek AS TCek,
AT.PRBakiye AS PRTBakiye,
d.Kod2 AS Kod2,
ST.OrtalamaGun AS OrtGun,
ST.Kod3OrtGun AS Kod3OrtGun,
ST.Kod3OrtBakiye AS Kod3OrtBakiye,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.EvrakNo = a.EvrakNo and t.Tarih = @Tarih and t.SiparisDurumu = 0 and t.Kod10 = @OnayDurm), 0) AS SicakSiparis,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Tarih < @Tarih and t.EvrakNo = a.EvrakNo and t.Kod10 = @OnayDurm), 0) AS SogukSiparis ,
ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Kod10 <> 'Reddedildi' and t.Tarih < @Tarih),0) AS GunIciSiparis,
CASE a.Tarih WHEN @Tarih THEN 'SICAK' ELSE 'SOĞUK' END AS SiparisTuru,
a.EvrakNo AS EvrakNo,
a.Kod10 AS OnayDurumu,
CONVERT(nvarchar(10), CAST(a.Tarih as datetime) - 2, 104) AS Tarih,
a.Kod3 AS Firma,
a.Kod2 AS Onaylayan
FROM FINSAT633.FINSAT633.SPI (NOLOCK)a
inner join FINSAT633.FINSAT633.CHK(NOLOCK) d ON d.HesapKodu = a.Chk
CROSS APPLY FINSAT633.wms.GetOrtGunKod3OrtBakiyeGun(a.Chk,d.Kod2) AS ST
CROSS APPLY FINSAT633.wms.GetSCekTCekPRBakiye(a.Chk) AS AT
WHERE a.Kod10=@OnayDurm  
and a.CHK between @CHK1 AND @CHK2 
and a.Tarih between @BasTarih and @BitTarih
--and a.SiparisDurumu=0 
and case when @OnayDurm='BEKLEMEDE' then case when (a.SiparisDurumu=0 ) then 1 else 0 end end = 1 
and d.TipKod in (select * from dbo.[splitstring](replace(@TipKodlari,';',',')))
and a.TeslimMiktar+a.KapatilanMiktar<Miktar 
and a.Kod13>0 
and (SELECT sum(v.GirMiktar-v.CikMiktar+v.DvrMiktar) FROM FINSAT633.FINSAT633.DST(NOLOCK) v WHERE v.MalKodu=a.MalKodu and v.Depo=a.Depo)>=(Miktar-(a.TeslimMiktar+a.KapatilanMiktar))
--a.Miktar

GROUP BY a.Chk,d.TipKod,d.KrediLimiti,(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA),d.Kod2,a.Tarih,a.EvrakNo,a.Kod10,a.Kod3,a.Kod2,ST.OrtalamaGun,ST.Kod3OrtGun,ST.Kod3OrtBakiye,AT.SCek,AT.TCek,AT.PRBakiye
) AA
WHERE (

	  (
	  (((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk1 and @Risk2) 
	  OR ((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) <0) 
	  )
	  AND
	  (((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod31 and @Kod32)
	  OR ((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) <0)
	  )
	  )
	  
	  OR 

	  (
	  (((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk12 and @Risk22) 
	  OR ((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)<0) 
	  )
	  AND
	  (((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod312 and @Kod322)
	  OR ((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) <0)
	  )
	  )

	  )
	  AND
	  not  (
			(Kod2=8 AND (@Grup<>'Koordinatör'  AND  @Grup<>'Admin')) 
			OR 
			(
				(@Grup='Koordinatör' OR @Grup='Bölge Müdürü' OR @Grup='Finans') 
				AND 
				(
					(AA.Kod3OrtGun>=60 AND @Grup<>'Finans') 
					OR 
					(
						(AA.Kod3OrtGun<60 )
						AND 
						(  
							((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)>=100 AND @Grup<>'Koordinatör') 
							OR 
								(
									((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)<100) 
									AND 
									(	
										(AA.Kod3OrtGun<60 AND AA.Kod3OrtGun>=30 AND @Grup<>'Koordinatör') 
													OR 
										(AA.Kod3OrtGun>=60 AND AA.Kod3OrtGun<30 AND @Grup<>'Bölge Müdürü' AND Kod2<>8)
									)
								)
						)
					)
				)
			)
		)
END






GO
/****** Object:  StoredProcedure [wms].[SiparisOnayUpdate]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [wms].[SiparisOnayUpdate]
@EvrakNo VARCHAR(30),
@Text VARCHAR(30)
AS
BEGIN
	
	UPDATE FINSAT633.FINSAT633.SPI SET Kod10=@Text WHERE EvrakNo=@EvrakNo AND KynkEvrakTip = 62

END












GO
/****** Object:  StoredProcedure [wms].[SiparisReddet]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Derviş Akdeniz
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: -
-- Last Modify: 25.12.2017
-- Description:	sipariş onay reddet
-- =============================================
CREATE PROCEDURE [wms].[SiparisReddet]
	@OncekiDurum VARCHAR(20),
	@Kullanici VARCHAR(5),
	@EvrakNo VARCHAR(16)

AS
BEGIN
DECLARE 

	@Tarih INT
	SET @Tarih = DATEDIFF(dd,0,GETDATE()+2)

	UPDATE       FINSAT633.SPI
	SET                Kod10 = 'Reddedildi', Kod2 = SUBSTRING(@Kullanici, 0, 9), Degistiren = @Kullanici, DegisTarih = @Tarih
	WHERE        (EvrakNo = @EvrakNo) AND (Kod10 = @OncekiDurum)

END
GO
/****** Object:  StoredProcedure [wms].[SozlesmeCariBilgileri]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [wms].[SozlesmeCariBilgileri]
@HesapKodu VARCHAR(30),
@ListeNo VARCHAR(30)
AS
BEGIN
	declare @aaa int 
	declare @bbb int 
	set @aaa = FINSAT633.dbo.AlinanBordroORTVADE1( @HesapKodu, @ListeNo ) 
	set @bbb = FINSAT633.dbo.ORTVADE1( @HesapKodu) 
	SELECT 
	(DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB-DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA) AS Bakiye,
	(DvrCekRiskA+DvrSntRiskA+CekRiskA+SntRiskA+DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB -DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA-SntRiskB-CekRiskB-DvrSntRiskB-DvrCekRiskB) AS ToplamBakiye,
	KrediLimiti, 
	(select case when @aaa is null then '' else CONVERT(VARCHAR(15), CONVERT(DATETIME,@aaa-2),104) end) AS AlinanBordroOrtalamaVade,
	(select case when @bbb is null then '' else CONVERT(VARCHAR(15), CONVERT(DATETIME,@bbb-2),104) end) AS OrtalamaVade,
	(SELECT SUM(SPI.Tutar-SPI.ToplamIskonto) FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	   WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 AND SPI.Chk=@HesapKodu) as BekleyenSiparisTutar,
	(SELECT ISNULL(SUM(Tutar),0) FROM FINSAT633.FINSAT633.CHI(NOLOCK)
		WHERE KynkEvrakTip=25 AND IslemTip=17 AND BA=1 AND HesapKodu=@HesapKodu AND Kod2=@ListeNo) as AlinanBordro

	
FROM FINSAT633.FINSAT633.CHK(NOLOCK)
WHERE HesapKodu=@HesapKodu

END










GO
/****** Object:  StoredProcedure [wms].[SozlesmeFinansmanMuduruOnay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SozlesmeFinansmanMuduruOnay]
@SozlesmeNo VARCHAR(30),
@Kullanici VARCHAR(30)
AS
BEGIN
	
	UPDATE FINSAT633.FINSAT633.ISS_Temp SET OnaylayanFinansmanMuduru=@Kullanici, OnayTarihFinansmanMuduru = GETDATE(), FinansmanMuduruOnay=1 WHERE ListeNo=@SozlesmeNo

END








GO
/****** Object:  StoredProcedure [wms].[SozlesmeGenelMudurOnay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SozlesmeGenelMudurOnay]
@SozlesmeNo VARCHAR(30),
@Kullanici VARCHAR(30)
AS
BEGIN
	
	UPDATE FINSAT633.FINSAT633.ISS_Temp SET OnaylayanGenelMudur=@Kullanici, OnayTarihGenelMudur = GETDATE(), GenelMudurOnay=1 WHERE ListeNo=@SozlesmeNo

END








GO
/****** Object:  StoredProcedure [wms].[SozlesmeOnaylanmamisList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SozlesmeOnaylanmamisList]
AS
BEGIN
	
	SELECT ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan
	, (CASE 
	WHEN SatisMuduruOnay = 0 THEN 'SM' 
	WHEN (OnayTip in (1, 2) AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 0) THEN 'SPGMY' 
	WHEN (OnayTip = 2 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 1 AND GenelMudurOnay = 0) THEN 'GM' 
	ELSE '' END) AS OnayMerci
FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON ISST.MusteriKod=CHK.HesapKodu
--WHERE SatisMuduruOnay=0 or FinansmanMuduruOnay=0 or GenelMudurOnay=0
WHERE
(OnayTip = 0 AND SatisMuduruOnay = 0) OR
(OnayTip = 1 AND (SatisMuduruOnay = 0 OR FinansmanMuduruOnay = 0)) OR
(OnayTip = 2 AND (SatisMuduruOnay = 0 OR FinansmanMuduruOnay = 0 OR GenelMudurOnay = 0))
--(OnayTip = 0 AND SatisMuduruOnay = 0) OR
--(OnayTip = 1 AND SatisMuduruOnay = 0) OR
--(OnayTip = 2 AND SatisMuduruOnay = 0) OR
--(OnayTip = 1 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 0) OR
--(OnayTip = 2 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 0) OR
--(OnayTip = 2 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 1 AND GenelMudurOnay = 0) 
GROUP BY ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2), SatisMuduruOnay, FinansmanMuduruOnay, GenelMudurOnay, OnayTip

END








GO
/****** Object:  StoredProcedure [wms].[SozlesmeOnaylanmisList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SozlesmeOnaylanmisList]
AS
BEGIN
	
	SELECT ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan
	, '' AS OnayMerci
FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON ISST.MusteriKod=CHK.HesapKodu
--WHERE SatisMuduruOnay=1 and FinansmanMuduruOnay=1 and GenelMudurOnay=1
WHERE 
(OnayTip = 0 AND SatisMuduruOnay = 1) OR
(OnayTip = 1 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 1) OR
(OnayTip = 2 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 1 AND GenelMudurOnay = 1)
GROUP BY ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2), SatisMuduruOnay, FinansmanMuduruOnay, GenelMudurOnay, OnayTip

END








GO
/****** Object:  StoredProcedure [wms].[SozlesmeSatisMuduruOnay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SozlesmeSatisMuduruOnay]
@SozlesmeNo VARCHAR(30),
@Kullanici VARCHAR(30)
AS
BEGIN
	
	UPDATE FINSAT633.FINSAT633.ISS_Temp SET OnaylayanSatisMuduru=@Kullanici, OnayTarihSatisMuduru = GETDATE(), SatisMuduruOnay=1 WHERE ListeNo=@SozlesmeNo

END








GO
/****** Object:  StoredProcedure [wms].[SozlesmeSiraNoSelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[SozlesmeSiraNoSelect]
AS
BEGIN
	
	DECLARE @Num INT
	SET @Num = (SELECT Top 1 CAST(REPLACE(Kod2,'SÖZ','') AS INT)+1 
				FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) 
				Order By Row_ID Desc) 

	WHILE (SELECT COUNT(Row_ID) 
		FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) 
		WHERE Kod2='SÖZ '+CAST(@Num AS Varchar))>0
	BEGIN
	   SET @Num=@Num+1
	END

	SELECT @Num 

END







GO
/****** Object:  StoredProcedure [wms].[SP_SiparisOnay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[SP_SiparisOnay]
@EvrakNo VARCHAR(50),
@Kullanici VARCHAR(50),
@OnayTip INT,
@OnaylandiMi BIT
AS
BEGIN

	IF (@OnayTip=3)
	BEGIN
		IF(@OnaylandiMi=1)
		BEGIN
		UPDATE FINSAT633.FINSAT633.SiparisOnay SET Durum = 3, GMOnay = 1,GMOnaylayan = @Kullanici,GMOnayTarih=GETDATE() WHERE EvrakNo=@EvrakNo
		UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Onaylandı' WHERE EvrakNo=@EvrakNo AND KynkEvrakTip = 62
		END
		ELSE
		BEGIN
		UPDATE FINSAT633.FINSAT633.SiparisOnay SET Durum = 3, GMOnay = 0,GMOnaylayan = @Kullanici,GMOnayTarih=GETDATE() WHERE EvrakNo=@EvrakNo
		UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Reddedildi' WHERE EvrakNo=@EvrakNo AND KynkEvrakTip = 62
		END
	
	END
	ELSE IF (@OnayTip=2)
	BEGIN
		IF(@OnaylandiMi=1)
		BEGIN
		UPDATE FINSAT633.FINSAT633.SiparisOnay SET Durum = 2, SPGMYOnay = 1,SPGMYOnaylayan = @Kullanici,GMOnayTarih=GETDATE() WHERE EvrakNo=@EvrakNo
		UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Onaylandı' WHERE EvrakNo=@EvrakNo AND KynkEvrakTip = 62
		END
		ELSE
		BEGIN
		UPDATE FINSAT633.FINSAT633.SiparisOnay SET Durum = 2, SPGMYOnay = 0,SMOnaylayan = @Kullanici,GMOnayTarih=GETDATE() WHERE EvrakNo=@EvrakNo
		UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Reddedildi' WHERE EvrakNo=@EvrakNo AND KynkEvrakTip = 62
		END
	END
	ELSE
	BEGIN
		IF(@OnaylandiMi=1)
		BEGIN
		UPDATE FINSAT633.FINSAT633.SiparisOnay SET Durum = 1, SMOnay = 1,GMOnaylayan = @Kullanici,GMOnayTarih=GETDATE() WHERE EvrakNo=@EvrakNo
		UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Onaylandı' WHERE EvrakNo=@EvrakNo AND KynkEvrakTip = 62
		END
		ELSE
		BEGIN
		UPDATE FINSAT633.FINSAT633.SiparisOnay SET Durum = 1, SMOnay = 0,GMOnaylayan = @Kullanici,GMOnayTarih=GETDATE() WHERE EvrakNo=@EvrakNo
		UPDATE FINSAT633.FINSAT633.SPI SET Kod10='Reddedildi' WHERE EvrakNo=@EvrakNo AND KynkEvrakTip = 62
		END
	END

-------------------GM ONAY -----------------------------------------------------



END






GO
/****** Object:  StoredProcedure [wms].[SP_SozlesmeOnay]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[SP_SozlesmeOnay]

AS
BEGIN
	--------------


	SELECT ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan
	, (CASE 
	WHEN SatisMuduruOnay = 0 THEN 'SM' 
	WHEN (OnayTip in (1, 2) AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 0) THEN 'SPGMY' 
	WHEN (OnayTip = 2 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 1 AND GenelMudurOnay = 0) THEN 'GM' 
	ELSE '' END) AS OnayMerci,

	(SELECT (DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB-DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA) AS Bakiye
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu = MusteriKod ) as Bakiye,

	(SELECT (DvrCekRiskA+DvrSntRiskA+CekRiskA+SntRiskA+DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB -DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA-SntRiskB-CekRiskB-DvrSntRiskB-DvrCekRiskB) AS ToplamBakiye
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu=MusteriKod) as ToplamBakiye,

	(SELECT KrediLimiti
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu=MusteriKod) as KrediLimiti,

	

	--(SELECT (SELECT CASE WHEN  FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo) IS NULL THEN  '' ELSE CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo ))-2),104) END)) as AlinanBordroOrtalamaVade,
	ISNULL(CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo ))-2),104),'') as AlinanBordroOrtalamaVade,

	(SELECT ISNULL(SUM(Tutar),0) AS Bakiye
	FROM FINSAT633.FINSAT633.CHI(NOLOCK)
	WHERE KynkEvrakTip=25 AND IslemTip=17 AND BA=1 AND HesapKodu=MusteriKod AND Kod2=ListeNo) as AlinanBordro,

	--(SELECT (SELECT CASE WHEN  FINSAT633.dbo.ORTVADE1 (MusteriKod) IS NULL THEN   '' ELSE CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.ORTVADE1 (MusteriKod)-2)),104) END)) as OrtalamaVade,
	ISNULL(CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.ORTVADE1 (MusteriKod))-2),104),'') as OrtalamaVade,

 	ISNULL((SELECT SUM(SPI.Tutar-SPI.ToplamIskonto) AS Tutar
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 AND SPI.Chk=MusteriKod),0) as BekleyenSiparisTutar,

	ISST.Kod1 As BaglantiKodu,
	ISST.Kod11 As BaglantiTutar,
	ISST.Kod2 As SiraNo,
	ISST.BasTarih,
	ISST.BitTarih,
	ISST.Aciklama,
	ISST.CekTutari,
	ISST.CekOrtalamaVadeTarih,
	ISST.NakitTutari
	


 

FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON ISST.MusteriKod=CHK.HesapKodu
WHERE
(OnayTip = 0 AND SatisMuduruOnay = 0) OR
(OnayTip = 1 AND (SatisMuduruOnay = 0 OR FinansmanMuduruOnay = 0)) OR
(OnayTip = 2 AND (SatisMuduruOnay = 0 OR FinansmanMuduruOnay = 0 OR GenelMudurOnay = 0))
GROUP BY ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2), SatisMuduruOnay, FinansmanMuduruOnay, GenelMudurOnay, OnayTip,
 ISST.Kod1,ISST.Kod11,ISST.Kod2,ISST.BasTarih,ISST.BitTarih,ISST.Aciklama,ISST.CekTutari,ISST.CekOrtalamaVadeTarih,ISST.NakitTutari
END





GO
/****** Object:  StoredProcedure [wms].[SP_SozlesmeOnaySM]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[SP_SozlesmeOnaySM]

AS
BEGIN
	--------------


	SELECT ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan
	, (CASE 
	WHEN SatisMuduruOnay = 0 THEN 'SM' 
	WHEN (OnayTip in (1, 2) AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 0) THEN 'SPGMY' 
	WHEN (OnayTip = 2 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 1 AND GenelMudurOnay = 0) THEN 'GM' 
	ELSE '' END) AS OnayMerci,

	(SELECT (DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB-DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA) AS Bakiye
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu = MusteriKod ) as Bakiye,

	(SELECT (DvrCekRiskA+DvrSntRiskA+CekRiskA+SntRiskA+DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB -DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA-SntRiskB-CekRiskB-DvrSntRiskB-DvrCekRiskB) AS ToplamBakiye
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu=MusteriKod) as ToplamBakiye,

	(SELECT KrediLimiti
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu=MusteriKod) as KrediLimiti,

	

	--(SELECT (SELECT CASE WHEN  FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo) IS NULL THEN  '' ELSE CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo ))-2),104) END)) as AlinanBordroOrtalamaVade,
	ISNULL(CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo ))-2),104),'') as AlinanBordroOrtalamaVade,

	(SELECT ISNULL(SUM(Tutar),0) AS Bakiye
	FROM FINSAT633.FINSAT633.CHI(NOLOCK)
	WHERE KynkEvrakTip=25 AND IslemTip=17 AND BA=1 AND HesapKodu=MusteriKod AND Kod2=ListeNo) as AlinanBordro,

	--(SELECT (SELECT CASE WHEN  FINSAT633.dbo.ORTVADE1 (MusteriKod) IS NULL THEN   '' ELSE CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.ORTVADE1 (MusteriKod)-2)),104) END)) as OrtalamaVade,
	ISNULL(CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.ORTVADE1 (MusteriKod))-2),104),'') as OrtalamaVade,

 	ISNULL((SELECT SUM(SPI.Tutar-SPI.ToplamIskonto) AS Tutar
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 AND SPI.Chk=MusteriKod),0) as BekleyenSiparisTutar,

	ISST.Kod1 As BaglantiKodu,
	ISST.Kod11 As BaglantiTutar,
	ISST.Kod2 As SiraNo,
	ISST.BasTarih,
	ISST.BitTarih,
	ISST.Aciklama,
	ISST.CekTutari,
	ISST.CekOrtalamaVadeTarih,
	ISST.NakitTutari
	


 

FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON ISST.MusteriKod=CHK.HesapKodu
WHERE
(OnayTip=0 AND SatisMuduruOnay=0) OR
(OnayTip=1 AND SatisMuduruOnay=0 AND FinansmanMuduruOnay = 0) OR
(OnayTip=2 AND SatisMuduruOnay=0 AND FinansmanMuduruOnay = 0 AND GenelMudurOnay = 0) OR 
(OnayTip=2 AND SatisMuduruOnay=0 AND GenelMudurOnay = 0)
GROUP BY ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2), SatisMuduruOnay, FinansmanMuduruOnay, GenelMudurOnay, OnayTip,
 ISST.Kod1,ISST.Kod11,ISST.Kod2,ISST.BasTarih,ISST.BitTarih,ISST.Aciklama,ISST.CekTutari,ISST.CekOrtalamaVadeTarih,ISST.NakitTutari
END





GO
/****** Object:  StoredProcedure [wms].[SP_SozlesmeOnaySPGMY]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[SP_SozlesmeOnaySPGMY]

AS
BEGIN
	--------------


	SELECT ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan
	, (CASE 
	WHEN SatisMuduruOnay = 0 THEN 'SM' 
	WHEN (OnayTip in (1, 2) AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 0) THEN 'SPGMY' 
	WHEN (OnayTip = 2 AND SatisMuduruOnay = 1 AND FinansmanMuduruOnay = 1 AND GenelMudurOnay = 0) THEN 'GM' 
	ELSE '' END) AS OnayMerci,

	(SELECT (DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB-DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA) AS Bakiye
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu = MusteriKod ) as Bakiye,

	(SELECT (DvrCekRiskA+DvrSntRiskA+CekRiskA+SntRiskA+DvrB+OdemeB+CiroB+IadeFatB+KDVB+DigerB -DvrA-OdemeA-CiroA-IadeFatA-KDVA-DigerA-SntRiskB-CekRiskB-DvrSntRiskB-DvrCekRiskB) AS ToplamBakiye
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu=MusteriKod) as ToplamBakiye,

	(SELECT KrediLimiti
	FROM FINSAT633.FINSAT633.CHK(NOLOCK)
	WHERE HesapKodu=MusteriKod) as KrediLimiti,

	

	--(SELECT (SELECT CASE WHEN  FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo) IS NULL THEN  '' ELSE CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo ))-2),104) END)) as AlinanBordroOrtalamaVade,
	ISNULL(CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.AlinanBordroORTVADE1 (MusteriKod, ListeNo ))-2),104),'') as AlinanBordroOrtalamaVade,

	(SELECT ISNULL(SUM(Tutar),0) AS Bakiye
	FROM FINSAT633.FINSAT633.CHI(NOLOCK)
	WHERE KynkEvrakTip=25 AND IslemTip=17 AND BA=1 AND HesapKodu=MusteriKod AND Kod2=ListeNo) as AlinanBordro,

	--(SELECT (SELECT CASE WHEN  FINSAT633.dbo.ORTVADE1 (MusteriKod) IS NULL THEN   '' ELSE CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.ORTVADE1 (MusteriKod)-2)),104) END)) as OrtalamaVade,
	ISNULL(CONVERT(VARCHAR(15), CONVERT(DATETIME,(FINSAT633.dbo.ORTVADE1 (MusteriKod))-2),104),'') as OrtalamaVade,

 	ISNULL((SELECT SUM(SPI.Tutar-SPI.ToplamIskonto) AS Tutar
	FROM FINSAT633.FINSAT633.SPI(NOLOCK) SPI
	WHERE SPI.KynkEvrakTip=62 AND SPI.SiparisDurumu=0 AND SPI.Chk=MusteriKod),0) as BekleyenSiparisTutar,

	ISST.Kod1 As BaglantiKodu,
	ISST.Kod11 As BaglantiTutar,
	ISST.Kod2 As SiraNo,
	ISST.BasTarih,
	ISST.BitTarih,
	ISST.Aciklama,
	ISST.CekTutari,
	ISST.CekOrtalamaVadeTarih,
	ISST.NakitTutari
	


 

FROM FINSAT633.FINSAT633.ISS_Temp(NOLOCK) ISST
LEFT JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON ISST.MusteriKod=CHK.HesapKodu
WHERE
(OnayTip=1 AND SatisMuduruOnay=1 AND FinansmanMuduruOnay = 0) OR
(OnayTip=2 AND SatisMuduruOnay=1 AND FinansmanMuduruOnay = 0 AND GenelMudurOnay = 0) 
GROUP BY ListeNo, MusteriKod, (CHK.Unvan1+' '+CHK.Unvan2), SatisMuduruOnay, FinansmanMuduruOnay, GenelMudurOnay, OnayTip,
 ISST.Kod1,ISST.Kod11,ISST.Kod2,ISST.BasTarih,ISST.BitTarih,ISST.Aciklama,ISST.CekTutari,ISST.CekOrtalamaVadeTarih,ISST.NakitTutari
END





GO
/****** Object:  StoredProcedure [wms].[STKSelect1]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [wms].[STKSelect1]
AS
BEGIN
	
	SELECT MalKodu, MalAdi, Birim1, Birim2, Birim3, GrupKod FROM FINSAT633.FINSAT633.STK(NOLOCK) WHERE AktifPasif=0 AND KartTip in (0,1,2,8)

END
GO
/****** Object:  StoredProcedure [wms].[STKSelect2]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [wms].[STKSelect2]
@INDEX INT
AS
BEGIN
	
	IF(@INDEX=0)
	SELECT DISTINCT MalKodu AS Kod, MalAdi AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

	ELSE IF(@INDEX=1)
	SELECT DISTINCT GrupKod AS Kod, '' AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

	ELSE IF(@INDEX=2)
	SELECT DISTINCT TipKod AS Kod, '' AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

	ELSE IF(@INDEX=3)
	SELECT DISTINCT OzelKod AS Kod, '' AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

	ELSE IF(@INDEX=4)
	SELECT DISTINCT Kod1 AS Kod, '' AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

	ELSE IF(@INDEX=5)
	SELECT DISTINCT Kod2 AS Kod, '' AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

	ELSE IF(@INDEX=6)
	SELECT DISTINCT Kod3 AS Kod, '' AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

	ELSE IF(@INDEX=7)
	SELECT DISTINCT Kod4 AS Kod, '' AS Aciklama FROM FINSAT633.FINSAT633.STK(NOLOCK)

END
GO
/****** Object:  StoredProcedure [wms].[StokOnaySelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[StokOnaySelect]
@AktifPasifTumu SMALLINT 
AS
BEGIN
	
	IF (@AktifPasifTumu=0)      --TÜMÜNÜ GETİR
	BEGIN
		SELECT CAST(0 AS Bit) Onay, MalKodu, MalAdi, MalAdi2, Birim1, Birim2, GrupKod, TipKod, 
		       OzelKod, Kod1, Kod2, Kod3, Kod4, Kod8, KatSayi2, KatSayi3, Operator2, Operator3,
			   Case When AktifPasif = 1 Then 0 When AktifPasif = 0 Then 1 End AS AktifPasif 
		FROM FINSAT633.FINSAT633.STK(NOLOCK)
    END
	ELSE IF(@AktifPasifTumu=1)  --ONAY BEKLEYEN
	BEGIN
	    SELECT CAST(0 AS Bit) Onay, MalKodu, MalAdi, MalAdi2, Birim1, Birim2, GrupKod, TipKod, 
		       OzelKod, Kod1, Kod2, Kod3, Kod4, Kod8, KatSayi2, KatSayi3, Operator2, Operator3,
			   Case When AktifPasif = 1 Then 0 When AktifPasif = 0 Then 1 End AS AktifPasif 
		FROM FINSAT633.FINSAT633.STK_Temp(NOLOCK)
	END
	ELSE IF(@AktifPasifTumu=2)  --PASIFLERİ GETİR
	BEGIN
		SELECT CAST(0 AS Bit) Onay, MalKodu, MalAdi, MalAdi2, Birim1, Birim2, GrupKod, TipKod, 
		       OzelKod, Kod1, Kod2, Kod3, Kod4, Kod8, KatSayi2, KatSayi3, Operator2, Operator3,
			   Case When AktifPasif = 1 Then 0 When AktifPasif = 0 Then 1 End AS AktifPasif 
		FROM FINSAT633.FINSAT633.STK(NOLOCK)
		WHERE AktifPasif = 1 AND [CheckSum] = 14 
	END
	ELSE IF(@AktifPasifTumu=3)  --AKTIFLERİ GETİR
	BEGIN
		SELECT CAST(0 AS Bit) Onay, MalKodu, MalAdi, MalAdi2, Birim1, Birim2, GrupKod, TipKod, 
		       OzelKod, Kod1, Kod2, Kod3, Kod4, Kod8, KatSayi2, KatSayi3, Operator2, Operator3,
			   Case When AktifPasif = 1 Then 0 When AktifPasif = 0 Then 1 End AS AktifPasif  	
		FROM FINSAT633.FINSAT633.STK(NOLOCK)
		WHERE AktifPasif = 0 AND [CheckSum] <> -1 
	END
	ELSE IF(@AktifPasifTumu=4)  --REDLERİ GETİR
	BEGIN
		SELECT CAST(0 AS Bit) Onay, MalKodu, MalAdi, MalAdi2, Birim1, Birim2, GrupKod, TipKod, 
		       OzelKod, Kod1, Kod2, Kod3, Kod4, Kod8, KatSayi2, KatSayi3, Operator2, Operator3,
			   Case When AktifPasif = 1 Then 0 When AktifPasif = 0 Then 1 End AS AktifPasif 
		FROM FINSAT633.FINSAT633.STK(NOLOCK)
		WHERE [CheckSum] = -1 
	END

END








GO
/****** Object:  StoredProcedure [wms].[StokRaporuKodCase]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [wms].[StokRaporuKodCase]
@Tarih INT,
@BasGrupKod VARCHAR(30),
@BitGrupKod VARCHAR(30),
@BasTipKod VARCHAR(30),
@BitTipKod VARCHAR(30), 
@BasOzelKod VARCHAR(30),
@BitOzelKod VARCHAR(30),
@BasKod1 VARCHAR(30),
@BitKod1 VARCHAR(30),
@BasKod2 VARCHAR(30),
@BitKod2 VARCHAR(30),
@BasKod3 VARCHAR(30),
@BitKod3 VARCHAR(30)
AS
BEGIN

SELECT A.MalKodu, A.MalAdi
, A.GrupKod, A.TipKod, A.OzelKod, A.Kod1, A.Kod2, A.Kod3
, cast(A.GirMiktar as numeric(36,2)) as GirMiktar, cast(A.CikMiktar as numeric(36,2)) as CikMiktar, cast(A.StokMiktar as numeric(36,2)) as StokMiktar, A.Birim1
, cast(CASE A.Birim2 when 'M2' THEN ROUND((CASE WHEN A.Birim2 <> '' THEN FINSAT633.FINSAT633.fncMiktarToBirimMiktar(A.StokMiktar , A.Birim2, A.Birim1, A.Birim2, A.Birim3, A.Operator2, A.Operator3, A.Katsayi2,  A.Katsayi3) ELSE 0 END),2) WHEN 'M3' THEN ROUND((CASE WHEN A.Birim2 <> '' THEN FINSAT633.FINSAT633.fncMiktarToBirimMiktar(A.StokMiktar , A.Birim2, A.Birim1, A.Birim2, A.Birim3, A.Operator2, A.Operator3, A.Katsayi2,  A.Katsayi3) ELSE 0 END),3) ELSE ROUND((CASE WHEN A.Birim2 <> '' THEN FINSAT633.FINSAT633.fncMiktarToBirimMiktar(A.StokMiktar , A.Birim2, A.Birim1, A.Birim2, A.Birim3, A.Operator2, A.Operator3, A.Katsayi2,  A.Katsayi3) ELSE 0 END),0) END  as numeric(36,2))AS StokMiktarBirim2
, A.Birim2
, cast((CASE WHEN A.Birim3 <> '' THEN FINSAT633.FINSAT633.fncMiktarToBirimMiktar(A.StokMiktar , A.Birim3, A.Birim1, A.Birim2, A.Birim3, A.Operator2, A.Operator3, A.Katsayi2,  A.Katsayi3) ELSE 0 END)as numeric(36,2))AS StokMiktarBirim3
, A.Birim3
FROM (
	SELECT Result.MalKodu, Result.MalAdi, Result.GrupKod, Result.TipKod, Result.OzelKod, Result.Kod1, Result.Kod2, Result.Kod3
	, GirMiktar = ISNULL(SUM(Result.DonemGiris),0.0)
	, CikMiktar = ISNULL(SUM(Result.DonemCikis),0.0)
	, StokMiktar = ISNULL(SUM(Result.DonemGiris - Result.DonemCikis), 0.0) 
	, Result.Birim1, Result.Birim2, Result.Birim3, Result.Operator2, Result.Operator3, Result.Katsayi2, Result.Katsayi3
	FROM 
	(
		SELECT STK.MalKodu, STK.MalAdi, STK.Birim1, STK.Birim2, STK.Birim3, STK.Operator2, STK.Operator3, STK.Katsayi2, STK.Katsayi3, STK.GrupKod, STK.TipKod, STK.OzelKod, STK.Kod1, STK.Kod2, STK.Kod3, Donemsel.DonemGiris, Donemsel.DonemCikis
		FROM FINSAT633.FINSAT633.STK  AS STK (NOLOCK)
		LEFT JOIN 
		(
			SELECT  STI.MalKodu,STI.Depo
			, DonemGiris = SUM(case when STI.KynkEvrakTip IN (141, 142, 143, 144) then 0 when STI.IslemTip IN (14, 17, 18, 21, 22) then 0 else (CASE when STI.IslemTur=0 AND STK.GrupKod='PARKE' AND STI.Depo IN ('01 DP', '13 DP')  THEN (STI.Miktar - STI.ErekIFMiktar) when STI.IslemTur=0 AND STK.GrupKod<>'PARKE' AND STI.DEPO IN ('01 DP','02 DP','03 DP','04 DP','05 DP','06 DP','07 DP','08 DP','09 DP', '10 DP','11 DP','12 DP','13 DP','14 DP','15 DP','16 DP','17 DP','18 DP','19 DP','20 DP','21 DP',
'22 DP','23 DP','24 DP','26 DP','27 DP','28 DP','29 DP','30 DP','31 DP')  then  
			(STI.Miktar - STI.ErekIFMiktar) ELSE 0 END) END)
			, DonemCikis = SUM(case when STI.KynkEvrakTip IN (141, 142, 143, 144) then 0 when STI.IslemTip IN (14, 17, 18, 21, 22) then 0 else (CASE when  STI.IslemTur= 1 and STK.GrupKod='PARKE'  AND STI.Depo IN ('01 DP', '13 DP')THEN (STI.Miktar - STI.ErekIFMiktar) WHEN STI.IslemTur= 1 and STK.GrupKod<>'PARKE' AND STI.DEPO IN ('01 DP','02 DP','03 DP','04 DP','05 DP','06 DP','07 DP','08 DP','09 DP', '10 DP','11 DP','12 DP','13 DP','14 DP','15 DP','16 DP','17 DP','18 DP','19 DP','20 DP','21 DP',
'22 DP','23 DP','24 DP','26 DP','27 DP','28 DP','29 DP','30 DP','31 DP') THEN (STI.Miktar - STI.ErekIFMiktar) ELSE 0 END) END)
			FROM FINSAT633.FINSAT633.STI AS STI (NOLOCK) INNER JOIN FINSAT633.FINSAT633.STK (NOLOCK) STK ON STK.Malkodu=STI.MalKodu
			WHERE  STI.Tarih BETWEEN 0 AND @Tarih
		
			AND STI.IslemTip NOT IN (14, 17, 18) 
			AND ((STI.IrsFat = 2 AND STI.BirimMiktar <> STI.ErekIFMiktar) OR STI.IrsFat <> 2) 
			AND NOT (KynkEvrakTip IN (68, 69) AND ErekIIFKEvrakTip IN (5, 2) AND IrsFat = 3)
			GROUP BY STI.MalKodu ,STI.Depo
		) Donemsel ON STK.MalKodu = Donemsel.MalKodu 
		WHERE  STK.KartTip IN (0,1,2,3,8) 	
		AND AktifPasif = 0
		AND (GrupKod BETWEEN @BasGrupKod AND @BitGrupKod) 
		AND (TipKod BETWEEN @BasTipKod AND @BitTipKod)
		AND (OzelKod BETWEEN @BasOzelKod AND @BitOzelKod)
		AND (Kod1 BETWEEN @BasKod1 AND @BitKod1)
		AND (Kod2 BETWEEN @BasKod2 AND @BitKod2)
		AND (Kod3 BETWEEN @BasKod3 AND @BitKod3)
			
	) Result
	GROUP BY Result.MalKodu, Result.MalAdi, Result.Birim1, Result.Birim2, Result.Birim3, Result.Operator2, Result.Operator3, Result.Katsayi2, Result.Katsayi3, Result.GrupKod, Result.TipKod, Result.OzelKod, Result.Kod1, Result.Kod2, Result.Kod3
) A
ORDER BY A.MalKodu

END














GO
/****** Object:  StoredProcedure [wms].[Tao_GrupKod_MusteriKart]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[Tao_GrupKod_MusteriKart]
@MaliYilBasTarih INT,
@MaliYilBitTarih INT,
@BugunTarih INT,
@GrupKodu VARCHAR

--Hesap Kod Yok Grup Kod Seçili Müşteri Kartı Seçili
AS
BEGIN
	
SELECT (case when  FINSAT633.dbo.OrtalamaBeklemeSuresiTao(HesapKodu,@BugunTarih,
q.[2012DevirBakiye]+q.[2013Bakiye])<>0 then FINSAT633.dbo.OrtalamaBeklemeSuresiTao(HesapKodu,@BugunTarih,
q.[2012DevirBakiye]+q.[2013Bakiye]) end) as OrtalamaBeklemeSuresi,
(case when FINSAT633.dbo.ORTVADE1Tao(HesapKodu,
q.[2012DevirBakiye]+q.[2013Bakiye]) <>0 then convert(varchar, (convert(datetime,FINSAT633.dbo.ORTVADE1Tao(HesapKodu,q.[2012DevirBakiye]+q.[2013Bakiye])-2) ), 104)end) as OrtalamaVade,
cast(0 as decimal) as VadeFarki,
cast(0 as decimal) as Gun,
'' as KapatanVade, '' as BakiyeninIlkIslemTarihi, q.GrupKod as GrupKod,
q.TipKodu as TipKod, q.Kod1 as Kod1, q.HesapKodu as HesapKodu, q.Unvan as Unvan,
q.[2012DevirBakiye] as DevirBakiye2012, q.[2013Borç] as Borc2013, q.[2013Alacak] as Alacak2013,
q.[2013Bakiye] as Bakiye2013, q.[2012DevirBakiye]+q.[2013Bakiye] as ToplamBakiye, q.NetCiro as NetCiro,
q.kod2 as Yasak, q.OpsiyonGun as OpsiyonGun 
FROM (SELECT b.HesapKodu, isnull(max(b.Unvan1),'') as Unvan,b.GrupKod as GrupKod, b.TipKod as TipKodu,
b.Kod1 as Kod1, b.Kod2 as Kod2, b.OpsiyonGunu as OpsiyonGun, 
isnull((SUM(a.[FATURA])+SUM(a.[İSKONTO]))-(SUM(a.[İADEFATURA])+SUM(a.[İADEİSKONTO])),0) AS [NetCiro],
isnull((SELECT isnull(sum(case when x.BA = 0 then x.Tutar else -x.Tutar end),0) FROM (SELECT  KarsiHesapKodu as HesapKodu, 
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock) 
where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih < @MaliYilBasTarih  )

Union All 

SELECT  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)
and (Tarih < @MaliYilBasTarih )) x where x.hesapkodu=b.hesapkodu group by x.HesapKodu ),0) as [2012DevirBakiye],isnull(sum(case when a.BA = 0 then a.Tutar else 0 end),0) as [2013Borç],
isnull(sum(case when a.BA = 1 then a.Tutar else 0 end),0) as [2013Alacak],
isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0) as [2013Bakiye] 
FROM (SELECT  KarsiHesapKodu as HesapKodu, 
Case When IslemTip = '4'  and BA='0' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='1' THEN Tutar Else 0 END AS [İADEFATURA],Case When IslemTip = '9'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI
(nolock)where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih  )

Union All 

SELECT  HesapKodu,Case When IslemTip = '4'  and BA='0' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='1' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42) 
and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih  )) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu where b.GrupKod in (@GrupKodu)  group by b.HesapKodu,b.GrupKod,
b.TipKod,b.Kod1,b.Kod2,b.OpsiyonGunu) q 
WHERE  ((q.[2012DevirBakiye] >=1 OR q.[2012DevirBakiye]<=0) or (q.[2013Borç]>=1 OR q.[2013Borç]<=0) or 
(q.[2013Alacak]>=1 OR q.[2013Alacak]<=0)) 
order by q.hesapkodu

END








GO
/****** Object:  StoredProcedure [wms].[Tao_HesapKod_MusteriKart]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[Tao_HesapKod_MusteriKart]
@MaliYilBasTarih INT,
@MaliYilBitTarih INT,
@BugunTarih INT,
@HesapKodu VARCHAR

--Hesap Kod Var Grup Kod Önemli Değil Müşteri Kartı Seçili

AS
BEGIN
	
SELECT (case when  FINSAT633.dbo.OrtalamaBeklemeSuresiTao(HesapKodu, @BugunTarih, 
q.[2012DevirBakiye]+q.[2013Bakiye]) <>0 then FINSAT633.dbo.OrtalamaBeklemeSuresiTao(HesapKodu,@BugunTarih,
q.[2012DevirBakiye]+q.[2013Bakiye]) end)
as OrtalamaBeklemeSuresi, 
(case when FINSAT633.dbo.ORTVADE1Tao(HesapKodu,q.[2012DevirBakiye]+q.[2013Bakiye]) <>0 
then convert(varchar, (convert(datetime,FINSAT633.dbo.ORTVADE1Tao(HesapKodu,
q.[2012DevirBakiye]+q.[2013Bakiye])-2) ), 104)end)  as OrtalamaVade,
cast(0 as decimal) as VadeFarki,
cast(0 as decimal) as Gun,
'' as KapatanVade,
'' as BakiyeninIlkIslemTarihi,
q.GrupKod as GrupKod,
q.TipKodu as TipKod,
q.Kod1 as Kod1,
q.HesapKodu as HesapKodu,
q.Unvan as Unvan,
q.[2012DevirBakiye] as DevirBakiye2012,
q.[2013Borç] as Borc2013,q.[2013Alacak]
as Alacak2013,q.[2013Bakiye] as Bakiye2013,
q.[2012DevirBakiye]+q.[2013Bakiye] as ToplamBakiye,
q.NetCiro as NetCiro,
q.kod2 as Yasak,
q.OpsiyonGun as OpsiyonGun 
FROM (SELECT b.HesapKodu, 
isnull(max(b.Unvan1),'') as Unvan,
b.GrupKod as GrupKod, 
b.TipKod as TipKodu,
b.Kod1 as Kod1,
b.Kod2 as Kod2,
b.OpsiyonGunu as OpsiyonGun, 
isnull((SUM(a.[FATURA])+SUM(a.[İSKONTO]))-(SUM(a.[İADEFATURA])+SUM(a.[İADEİSKONTO])),0) AS [NetCiro],
isnull((SELECT isnull(sum(case when x.BA = 0 then x.Tutar else -x.Tutar end),0) FROM (SELECT KarsiHesapKodu as HesapKodu,
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI
(nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih < @MaliYilBasTarih  )Union All SELECT  HesapKodu,
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42)
and (Tarih < @MaliYilBasTarih )) x where x.hesapkodu=b.hesapkodu group by x.HesapKodu ),0) as [2012DevirBakiye], isnull(sum(case when a.BA = 0 then a.Tutar else 0 end),0) as [2013Borç], 
isnull(sum(case when a.BA = 1 then a.Tutar else 0 end),0) as [2013Alacak], 
isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0) as [2013Bakiye] 
FROM (SELECT  KarsiHesapKodu as HesapKodu,
Case When IslemTip = '4'  and BA='0' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='1' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) 
and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih  ) 

Union All 

SELECT  HesapKodu,
Case When IslemTip = '4'  and BA='0' THEN Tutar ELSE 0 END AS [FATURA],Case When IslemTip = '5' and BA='0' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='1' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) 
where IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih  )) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu
where b.HesapKodu=@HesapKodu group by b.HesapKodu,b.GrupKod,b.TipKod,b.Kod1,b.Kod2,b.OpsiyonGunu) q
order by q.hesapkodu

END








GO
/****** Object:  StoredProcedure [wms].[Tao_HesapKod_SaticiKart]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[Tao_HesapKod_SaticiKart]
@MaliYilBasTarih INT,
@MaliYilBitTarih INT,
@BugunTarih INT,
@HesapKodu VARCHAR
AS
BEGIN
	
SELECT 
(case when  FINSAT633.dbo.OrtalamaBeklemeSuresiTaoSaticiKarti(HesapKodu,@BugunTarih,
(q.[2012DevirBakiye]*-1)+q.[2013Bakiye]*-1)<>0 then FINSAT633.dbo.OrtalamaBeklemeSuresiTaoSaticiKarti(HesapKodu,@BugunTarih,(q.[2012DevirBakiye]*-1)+q.[2013Bakiye]*-1) end) as OrtalamaBeklemeSuresi, 
(case when FINSAT633.dbo.ORTVADE1TaoSaticiKarti(HesapKodu,(q.[2012DevirBakiye]*-1)+q.[2013Bakiye]*-1) <>0 then convert(varchar, (convert(datetime,FINSAT633.dbo.ORTVADE1TaoSaticiKarti(HesapKodu,
(q.[2012DevirBakiye]*-1)+q.[2013Bakiye]*-1)-2) ), 104)end)  as OrtalamaVade,
cast(0 as decimal) as VadeFarki,cast(0 as decimal) as Gun,
'' as KapatanVade,
'' as BakiyeninIlkIslemTarihi,
q.GrupKod as GrupKod,
q.TipKodu as TipKod,
q.Kod1 as Kod1,
q.HesapKodu as HesapKodu,
q.Unvan as Unvan,
q.[2012DevirBakiye] as DevirBakiye2012,
q.[2013Borç] as Borc2013,
q.[2013Alacak] as Alacak2013,
(q.[2013Bakiye]*-1) as Bakiye2013,
(q.[2012DevirBakiye])+(q.[2013Bakiye])  as ToplamBakiye,
q.NetCiro as NetCiro,
q.kod2 as Yasak,
q.OpsiyonGun as OpsiyonGun 
FROM (SELECT b.HesapKodu, isnull(max(b.Unvan1),'') as Unvan,
b.GrupKod as GrupKod, 
b.TipKod as TipKodu,
b.Kod1 as Kod1,
b.Kod2 as Kod2,
b.OpsiyonGunu as OpsiyonGun, 
isnull((SUM((a.[FATURA]*-1))+SUM((a.[İSKONTO]*-1)))-(SUM((a.[İADEFATURA]*-1))+SUM((a.[İADEİSKONTO]*-1))),0) AS [NetCiro],
isnull((SELECT isnull(sum(case when x.BA = 0 then x.Tutar else -x.Tutar end),0) FROM (SELECT  KarsiHesapKodu as HesapKodu, 
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih < @MaliYilBasTarih  )

Union All 

SELECT  HesapKodu, 
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih < @MaliYilBasTarih )) x where x.hesapkodu=b.hesapkodu group by x.HesapKodu ),0) as [2012DevirBakiye],
isnull(sum(case when a.BA = 0 then a.Tutar else 0 end),0) as [2013Borç],
isnull(sum(case when a.BA = 1 then a.Tutar else 0 end),0) as [2013Alacak],
isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0) as [2013Bakiye] 
FROM (SELECT  KarsiHesapKodu as HesapKodu,
Case When IslemTip = '4'  and BA='1' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='0' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock)where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih )

Union All

SELECT  HesapKodu,
Case When IslemTip = '4'  and BA='1' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='0' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih  )) a right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu where b.HesapKodu=@HesapKodu 
group by b.HesapKodu,b.GrupKod,b.TipKod,b.Kod1,b.Kod2,b.OpsiyonGunu) q 
order by q.hesapkodu

END








GO
/****** Object:  StoredProcedure [wms].[Tao_HesapKodYok_SaticiKart]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[Tao_HesapKodYok_SaticiKart]
@MaliYilBasTarih INT,
@MaliYilBitTarih INT,
@BugunTarih INT
--Hesap Kod Yok Grup Kod Önemli Değil Satıcı Kartı Seçili
AS
BEGIN
	
SELECT (case when  FINSAT633.dbo.OrtalamaBeklemeSuresiTaoSaticiKarti(HesapKodu,@BugunTarih,
q.[2012DevirBakiye]*-1+q.[2013Bakiye]*-1)<>0 then FINSAT633.dbo.OrtalamaBeklemeSuresiTaoSaticiKarti(HesapKodu,@BugunTarih,q.[2012DevirBakiye]*-1+q.[2013Bakiye]*-1) end) as OrtalamaBeklemeSuresi,
(case when FINSAT633.dbo.ORTVADE1TaoSaticiKarti(HesapKodu,q.[2012DevirBakiye]*-1+q.[2013Bakiye]*-1) <>0 then convert(varchar, (convert(datetime,FINSAT633.dbo.ORTVADE1TaoSaticiKarti(HesapKodu,q.[2012DevirBakiye]*-1+q.[2013Bakiye]*-1)-2) ), 104)end)  as OrtalamaVade,
cast(0 as decimal) as VadeFarki,cast(0 as decimal) as Gun,
'' as KapatanVade,'' as BakiyeninIlkIslemTarihi,q.GrupKod as GrupKod,q.TipKodu as TipKod,
q.Kod1 as Kod1, q.HesapKodu as HesapKodu, q.Unvan as Unvan, q.[2012DevirBakiye] as DevirBakiye2012, 
q.[2013Borç] as Borc2013, q.[2013Alacak] as Alacak2013, (q.[2013Bakiye]*-1) as Bakiye2013,
(q.[2012DevirBakiye])+(q.[2013Bakiye]) as ToplamBakiye, q.NetCiro as NetCiro, q.kod2 as Yasak,
q.OpsiyonGun as OpsiyonGun FROM (SELECT b.HesapKodu, isnull(max(b.Unvan1),'') as Unvan, b.GrupKod as GrupKod, b.TipKod as TipKodu, b.Kod1 as Kod1, b.Kod2 as Kod2, b.OpsiyonGunu as OpsiyonGun, 
isnull((SUM((a.[FATURA]*-1))+SUM((a.[İSKONTO]*-1)))-(SUM((a.[İADEFATURA]*-1))+SUM((a.[İADEİSKONTO]*-1))),0) AS [NetCiro],
isnull((SELECT isnull(sum(case when x.BA = 0 then x.Tutar else -x.Tutar end),0) FROM (SELECT  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock) where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih < @MaliYilBasTarih  )

Union All

SELECT  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih < @MaliYilBasTarih )) x where x.hesapkodu=b.hesapkodu group by x.HesapKodu ),0) as [2012DevirBakiye],
isnull(sum(case when a.BA = 0 then a.Tutar else 0 end),0) as [2013Borç],
isnull(sum(case when a.BA = 1 then a.Tutar else 0 end),0) as [2013Alacak],
isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0) as [2013Bakiye] 
FROM (SELECT  KarsiHesapKodu as HesapKodu,
Case When IslemTip = '4'  and BA='1' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='0' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock)where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih  )

Union All

SELECT  HesapKodu,
Case When IslemTip = '4'  and BA='1' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='0' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih )) a 
right join
FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu  
group by b.HesapKodu,b.GrupKod,b.TipKod,b.Kod1,b.Kod2,b.OpsiyonGunu) q 
WHERE  (((q.[2012DevirBakiye]) >=1 and HesapKodu like '8%'  OR (q.[2012DevirBakiye])<=0) and HesapKodu like '8%'  or ((q.[2013Borç])>=1 and HesapKodu like '8%'  OR (q.[2013Borç])<=0) and HesapKodu like '8%'  or ((q.[2013Alacak])>=1 and HesapKodu like '8%'  OR (q.[2013Alacak])<=0 and HesapKodu like '8%' ))  
order by q.hesapkodu


END








GO
/****** Object:  StoredProcedure [wms].[Tao_TipKod_SaticiKart]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [wms].[Tao_TipKod_SaticiKart]
@MaliYilBasTarih INT,
@MaliYilBitTarih INT,
@BugunTarih INT,
@TipKod VARCHAR
--Hesap Kod Yok Tip Kod Seçili Satıcı Kartı Seçili
AS
BEGIN
	
SELECT (case when  FINSAT633.dbo.OrtalamaBeklemeSuresiTao(HesapKodu,@BugunTarih,
q.[2012DevirBakiye]+q.[2013Bakiye])<>0 then FINSAT633.dbo.OrtalamaBeklemeSuresiTao(HesapKodu,@BugunTarih,
q.[2012DevirBakiye]+q.[2013Bakiye]) end) as OrtalamaBeklemeSuresi, 
(case when FINSAT633.dbo.ORTVADE1Tao(HesapKodu,q.[2012DevirBakiye]+q.[2013Bakiye]) <>0
 then convert(varchar, (convert(datetime,FINSAT633.dbo.ORTVADE1Tao (HesapKodu,
 q.[2012DevirBakiye]+q.[2013Bakiye])-2) ), 104)end)  as OrtalamaVade,
 cast(0 as decimal) as VadeFarki,cast(0 as decimal) as Gun,'' as KapatanVade,'' as BakiyeninIlkIslemTarihi,
q.GrupKod as GrupKod,q.TipKodu as TipKod,q.Kod1 as Kod1,q.HesapKodu as HesapKodu,q.Unvan as Unvan,
q.[2012DevirBakiye] as DevirBakiye2012,q.[2013Borç] as Borc2013,
q.[2013Alacak] as Alacak2013,q.[2013Bakiye] as Bakiye2013,
q.[2012DevirBakiye]+q.[2013Bakiye] as ToplamBakiye,q.NetCiro as NetCiro,
q.kod2 as Yasak, q.OpsiyonGun as OpsiyonGun 
FROM (SELECT b.HesapKodu, isnull(max(b.Unvan1),'') as Unvan,b.GrupKod as GrupKod,
 b.TipKod as TipKodu,b.Kod1 as Kod1,b.Kod2 as Kod2,b.OpsiyonGunu as OpsiyonGun,
isnull((SUM(a.[FATURA])+SUM(a.[İSKONTO]))-(SUM(a.[İADEFATURA])+SUM(a.[İADEİSKONTO])),0) AS [NetCiro],
isnull((SELECT isnull(sum(case when x.BA = 0 then x.Tutar else -x.Tutar end),0) FROM (SELECT  KarsiHesapKodu as HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock) 
where KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih < @MaliYilBasTarih  )

Union All 

SELECT  HesapKodu, (case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42) and
(Tarih < @MaliYilBasTarih )) x where x.hesapkodu=b.hesapkodu group by x.HesapKodu ),0) as [2012DevirBakiye],
isnull(sum(case when a.BA = 0 then a.Tutar else 0 end),0) as [2013Borç],
isnull(sum(case when a.BA = 1 then a.Tutar else 0 end),0) as [2013Alacak],
isnull(sum(case when a.BA = 0 then a.Tutar else -a.Tutar end),0) as [2013Bakiye] 
FROM (SELECT KarsiHesapKodu as HesapKodu,Case When IslemTip = '4' and BA='0' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5'  and BA='0' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8'  and BA='1' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
(case when BA = 0 then 1 else 0 end) as BA FROM FINSAT633.FINSAT633.CHI (nolock) where 
KarsiHesapKodu is not null and KarsiHesapKodu <> '' and IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih and Tarih <= @MaliYilBitTarih  )Union All SELECT  HesapKodu,
Case When IslemTip = '4' and BA='0' THEN Tutar ELSE 0 END AS [FATURA],
Case When IslemTip = '5' and BA='0' THEN Tutar*(-1) Else 0 END AS [İSKONTO],
Case When IslemTip = '8' and BA='1' THEN Tutar Else 0 END AS [İADEFATURA],
Case When IslemTip = '9'  and BA='1' THEN Tutar*(-1) Else 0 END AS [İADEİSKONTO],
(case IslemTip when 5 then -Tutar when 9 then -Tutar else Tutar end) as Tutar,
BA FROM FINSAT633.FINSAT633.CHI (nolock) where IslemTip not in (16,21,27,32,36,37,41,42) and (Tarih >= @MaliYilBasTarih  and Tarih <= @MaliYilBitTarih )) a 
right join FINSAT633.FINSAT633.CHK b (nolock) ON b.HesapKodu = a.HesapKodu where b.TipKod in (@TipKod) 
group by b.HesapKodu,b.GrupKod,b.TipKod,b.Kod1,b.Kod2,b.OpsiyonGunu) q 
WHERE  ((q.[2012DevirBakiye] >=1 OR q.[2012DevirBakiye]<=0) or (q.[2013Borç]>=1 OR q.[2013Borç]<=0) or (q.[2013Alacak]>=1 OR q.[2013Alacak]<=0))  
order by q.hesapkodu

END








GO
/****** Object:  StoredProcedure [wms].[TeminatDurbunSelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[TeminatDurbunSelect]
AS
BEGIN
	

	SELECT TM.HesapKodu, (CHK.Unvan1+' '+CHK.Unvan2) AS Unvan FROM 
	(
	SELECT DISTINCT HesapKodu FROM FINSAT633.FINSAT633.Teminat(NOLOCK) WHERE	Onay=1
	) TM INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON TM.HesapKodu=CHK.HesapKodu

END


GO
/****** Object:  StoredProcedure [wms].[TeminatOnayList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[TeminatOnayList]
AS
BEGIN
	
	SELECT CAST(0 AS BIT) AS Onay, ID, HesapKodu, Unvan, AltBayi, Cins, Tutar, SureliSuresiz, Tarih, VadeTarih, ISNULL((SELECT SUM(Tutar) FROM FINSAT633.FINSAT633.Teminat(NOLOCK) TM WHERE TM.HesapKodu=T.HesapKodu AND TM.Onay=1),0) AS TeminatTutar FROM FINSAT633.FINSAT633.Teminat(NOLOCK) T -- where Onay=0
	
END











GO
/****** Object:  StoredProcedure [wms].[TeminatOnaySelect]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[TeminatOnaySelect]
@HesapKodu varchar(20)
AS
BEGIN
	

	SELECT * FROM FINSAT633.FINSAT633.Teminat WHERE HesapKodu=@HesapKodu AND Onay=1

END



GO
/****** Object:  StoredProcedure [wms].[TeminatOnayUpdate]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[TeminatOnayUpdate]
@ID INT,
@Kullanici VARCHAR(MAX)
AS
BEGIN
	
	UPDATE FINSAT633.FINSAT633.Teminat SET Onay=1, Onaylayan = @Kullanici, OnayTarih = GETDATE() WHERE ID=@ID

END











GO
/****** Object:  StoredProcedure [wms].[TeminatSil]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[TeminatSil]
@ID int
AS
BEGIN TRY
	
	DELETE FROM FINSAT633.FINSAT633.Teminat WHERE ID=@ID
	SELECT 1 AS SONUC

END TRY
BEGIN CATCH
	SELECT 0 AS SONUC
END CATCH



GO
/****** Object:  StoredProcedure [wms].[TerminalGetIrsaliye]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 28.12.2017
-- Last Modify: 16.01.2018
-- Description:	terminaldeki GetIrsaliye sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalGetIrsaliye]
	@GorevID int

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        MIN(IRS.ID) AS ID, Depo.DepoKodu AS DepoID, MIN(IRS.HesapKodu) as HesapKodu, BIRIKIM.dbo.fnFormatDateFromInt(IRS.Tarih) AS Tarih, MIN(Unvan1 + ' ' + Unvan2) AS Unvan, 
							(SELECT IRS.EvrakNo + '  ' FROM BIRIKIM.wms.IRS WITH(nolock) INNER JOIN BIRIKIM.wms.GorevIRS WITH(nolock) ON IRS.ID = GorevIRS.IrsaliyeID WHERE (GorevIRS.GorevID = @GorevID) FOR XML PATH('')) as EvrakNo 
	FROM            BIRIKIM.wms.IRS AS IRS WITH (nolock) INNER JOIN
							 BIRIKIM.wms.Depo WITH (nolock) ON IRS.DepoID = Depo.ID INNER JOIN
							 BIRIKIM.wms.GorevIRS WITH (nolock) ON IRS.ID = GorevIRS.IrsaliyeID LEFT OUTER JOIN
							 FINSAT633.FINSAT633.CHK ON IRS.HesapKodu = CHK.HesapKodu
	WHERE        (GorevIRS.GorevID = @GorevID)
	GROUP BY Depo.DepoKodu, BIRIKIM.dbo.fnFormatDateFromInt(IRS.Tarih)

END
GO
/****** Object:  StoredProcedure [wms].[TerminalGetMalzemeGenel]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 16.01.2018
-- Last Modify: 05.04.2018
-- Description:	terminaldeki GetMalzeme sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalGetMalzemeGenel]
	@GorevID int,
	@HepsiMi bit,
	@OkutulanMi bit,
	@YerVarMi bit

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        IRS_Detay.ID, IRS.ID AS irsID, IRS.EvrakNo as IrsaliyeNo, IRS_Detay.MalKodu, IRS_Detay.Miktar, IRS_Detay.Birim, IRS_Detay.MakaraNo, ISNULL(IRS_Detay.OkutulanMiktar, 0) AS OkutulanMiktar, 
							 Yer_Log.HucreAd AS Raf, ISNULL(SUM(Yer_Log.Miktar), 0) AS YerlestirmeMiktari, IRS_Detay.KynkSiparisNo, IRS_Detay.KynkSiparisSiraNo, 
								STK.MalAdi, ';' +  Barkod1 + ';' + Barkod2  + ';' + Barkod3  + ';' AS Barkod
	FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
							 FINSAT633.FINSAT633.STK ON IRS_Detay.MalKodu = STK.MalKodu INNER JOIN
							 BIRIKIM.wms.IRS ON IRS_Detay.IrsaliyeID = IRS.ID INNER JOIN
							 BIRIKIM.wms.GorevIRS ON IRS.ID = GorevIRS.IrsaliyeID LEFT OUTER JOIN
							 BIRIKIM.wms.Yer_Log ON IRS_Detay.ID = Yer_Log.IRSDetayID AND IRS_Detay.MalKodu = Yer_Log.MalKodu
	WHERE        (GorevIRS.GorevID = @GorevID) AND 
							(CASE WHEN @YerVarMi=1 then CASE WHEN (Yer_Log.GC = 1) THEN 1 ELSE 0 END ELSE CASE WHEN (Yer_Log.GC = 0 OR Yer_Log.GC IS NULL) THEN 1 ELSE 0 END END) = 1 AND 
							(CASE WHEN @HepsiMi = 1 THEN CASE WHEN (IRS_Detay.Miktar <> ISNULL(case when @OkutulanMi=1 then OkutulanMiktar else YerlestirmeMiktari end, 0)) THEN 1 ELSE 0 END ELSE 1 END = 1)
	GROUP BY IRS_Detay.ID, IRS.ID, IRS.EvrakNo, IRS_Detay.MalKodu, IRS_Detay.Miktar, IRS_Detay.Birim, IRS_Detay.MakaraNo, ISNULL(IRS_Detay.OkutulanMiktar, 0), ISNULL(IRS_Detay.YerlestirmeMiktari, 0), 
							 Yer_Log.HucreAd, STK.MalAdi,BarKod1,BarKod2,BarKod3, IRS_Detay.KynkSiparisNo, IRS_Detay.KynkSiparisSiraNo

END
GO
/****** Object:  StoredProcedure [wms].[TerminalGetMalzemeSiparis]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 16.01.2018
-- Last Modify: 05.04.2018
-- Description:	terminaldeki GetMalzeme sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalGetMalzemeSiparis]
	@GorevID int,
	@HepsiMi bit

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        GorevYer.ID, GorevYer.MalKodu, GorevYer.Miktar, GorevYer.Birim, GorevYer.MakaraNo, Yer.HucreAd AS Raf, ISNULL(GorevYer.YerlestirmeMiktari, 0) AS YerlestirmeMiktari, 
						'' as KynkSiparisNo, CAST(0 as smallint)  as KynkSiparisSiraNo, '' as IrsaliyeNo, 
						STK.MalAdi, ';' +  Barkod1 + ';' + Barkod2  + ';' + Barkod3  + ';' AS Barkod
	FROM            BIRIKIM.wms.GorevYer WITH (nolock) INNER JOIN
							BIRIKIM.wms.Yer WITH (nolock) ON GorevYer.YerID = Yer.ID LEFT OUTER JOIN
							FINSAT633.FINSAT633.STK ON GorevYer.MalKodu = STK.MalKodu
	WHERE        (GorevYer.GorevID = @GorevID) AND 
						(CASE WHEN @HepsiMi = 1 THEN CASE WHEN (GorevYer.Miktar <> ISNULL(GorevYer.YerlestirmeMiktari, 0) OR GorevYer.Miktar = 0) THEN 1 ELSE 0 END ELSE 1 END = 1)
	ORDER BY GorevYer.Sira

END
GO
/****** Object:  StoredProcedure [wms].[TerminalGetMalzemeTransfer]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 16.01.2018
-- Last Modify: 05.04.2018
-- Description:	terminaldeki GetMalzeme sayfası
-- =============================================
CREATE PROCEDURE [wms].[TerminalGetMalzemeTransfer]
	@GorevID int,
	@HepsiMi bit

AS
BEGIN
	SET NOCOUNT ON;

	SELECT        IRS_Detay.ID, IRS.ID AS irsID, IRS.EvrakNo as IrsaliyeNo, IRS_Detay.MalKodu, IRS_Detay.Miktar, IRS_Detay.Birim, IRS_Detay.MakaraNo, ISNULL(IRS_Detay.OkutulanMiktar, 0) AS OkutulanMiktar, 
						Yer_Log.HucreAd AS Raf, ISNULL(IRS_Detay.YerlestirmeMiktari, 0) AS YerlestirmeMiktari, IRS_Detay.KynkSiparisNo, IRS_Detay.KynkSiparisSiraNo, 
							STK.MalAdi, ';' +  Barkod1 + ';' + Barkod2  + ';' + Barkod3  + ';' AS Barkod
	FROM            BIRIKIM.wms.IRS_Detay INNER JOIN
							 BIRIKIM.wms.IRS ON IRS_Detay.IrsaliyeID = IRS.ID INNER JOIN
							 BIRIKIM.wms.GorevIRS ON IRS.ID = GorevIRS.IrsaliyeID LEFT OUTER JOIN
							 BIRIKIM.wms.Yer_Log ON IRS_Detay.KynkSiparisID = Yer_Log.KatID AND IRS_Detay.MalKodu = Yer_Log.MalKodu LEFT OUTER JOIN
							 FINSAT633.FINSAT633.STK ON IRS_Detay.MalKodu = STK.MalKodu
	WHERE        (GorevIRS.GorevID = @GorevID) AND (Yer_Log.GC = 0 OR Yer_Log.GC IS NULL) AND 
							(CASE WHEN @HepsiMi = 1 THEN CASE WHEN (IRS_Detay.Miktar <> ISNULL(YerlestirmeMiktari,0)) THEN 1 ELSE 0 END ELSE 1 END = 1)
	GROUP BY IRS_Detay.ID, IRS.ID, IRS.EvrakNo, IRS_Detay.MalKodu, IRS_Detay.Miktar, IRS_Detay.Birim, IRS_Detay.MakaraNo, ISNULL(IRS_Detay.OkutulanMiktar, 0), ISNULL(IRS_Detay.YerlestirmeMiktari, 0),
							 Yer_Log.HucreAd, IRS_Detay.KynkSiparisNo, IRS_Detay.KynkSiparisSiraNo, STK.MalAdi,BarKod1,BarKod2,BarKod3

END
GO
/****** Object:  StoredProcedure [wms].[ToplamRiskBakiyesi]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [wms].[ToplamRiskBakiyesi]
@BasTarih INT,
@BitTarih INT,
@VadeBaslangic INT,
@VadeBitis INT, 
@BasHesapKodu VARCHAR(30),
@BitHesapKodu VARCHAR(30)
AS
BEGIN
	
SELECT HesapKodu, 
Unvan AS Ünvan, 
Borc AS Borç, 
Alacak, 
Bakiye, 
Bakiye - ToplamBakiye AS ToplamBakiye
FROM 
(

	SELECT a.HesapKodu, ISNULL(MAX(b.Unvan1 + ' ' + b.Unvan2), '') AS Unvan, ISNULL(SUM(CASE WHEN a.BA = 0 THEN a.Tutar ELSE 0 END), 0) AS Borc, 
	ISNULL(SUM(CASE WHEN a.BA = 1 THEN a.Tutar ELSE 0 END), 0) AS Alacak, 
	ISNULL(SUM(CASE WHEN a.BA = 0 THEN a.Tutar ELSE - a.Tutar END), 0) AS Bakiye, 
	ISNULL(
	(SELECT SUM(Tutar) AS Expr1	
	FROM 
	(
		SELECT Tutar
		FROM FINSAT633.FINSAT633.ACK AS ACK WITH (NOLOCK) 
		WHERE (AlimTarih BETWEEN @BasTarih AND @BitTarih) AND (VadeTarih BETWEEN @VadeBaslangic AND @VadeBitis) AND (SonIslemTip IN (2)) AND (VadeTarih < DATEDIFF(DD, 0, GETDATE() + 2)) AND (Veren = a.HesapKodu) 
		
		UNION ALL
		
		SELECT Tutar
		FROM FINSAT633.FINSAT633.ASK AS ASK WITH (NOLOCK) 
		WHERE (AlimTarih BETWEEN @BasTarih AND @BitTarih) AND (VadeTarih BETWEEN @VadeBaslangic AND @VadeBitis) AND (SonIslemTip IN (2)) AND (VadeTarih < DATEDIFF(DD, 0, GETDATE() + 2)) AND (Veren = a.HesapKodu)
	) AS AAA
	), 0) AS ToplamBakiye 
	FROM 
	(
		SELECT KarsiHesapKodu AS HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, (CASE WHEN BA = 0 THEN 1 ELSE 0 END) AS BA
		FROM FINSAT633.FINSAT633.CHI WITH (nolock)
		WHERE (CHI.Tarih BETWEEN @BasTarih AND @BitTarih) AND (CHI.VadeTarih BETWEEN @VadeBaslangic AND @VadeBitis) AND (KarsiHesapKodu IS NOT NULL) AND (KarsiHesapKodu <> '') AND (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42,60))
		UNION ALL
		SELECT HesapKodu, (CASE IslemTip WHEN 5 THEN - Tutar WHEN 9 THEN - Tutar ELSE Tutar END) AS Tutar, BA
		FROM FINSAT633.FINSAT633.CHI AS CHI WITH (nolock)
		WHERE (CHI.Tarih BETWEEN @BasTarih AND @BitTarih) AND (CHI.VadeTarih BETWEEN @VadeBaslangic AND @VadeBitis) AND (IslemTip NOT IN (16, 21, 27, 32, 36, 37, 41, 42,60))
	) AS a LEFT OUTER JOIN FINSAT633.FINSAT633.CHK AS b WITH (nolock) ON b.HesapKodu = a.HesapKodu 
	
	WHERE (b.KartTip IN (0, 1)) AND (b.HesapKodu BETWEEN @BasHesapKodu AND @BitHesapKodu)  GROUP BY a.HesapKodu
	
) AS BB


END







GO
/****** Object:  StoredProcedure [wms].[TransferDetayList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 26.12.2017
-- Last Modify: 26.12.2017
-- Description:	Depolar Arası Transfer detay listesi
-- =============================================
CREATE PROCEDURE [wms].[TransferDetayList]
	@TransferID int

AS
BEGIN
	
	SELECT        BIRIKIM.wms.[Transfer].ID, BIRIKIM.wms.[Transfer].GorevID, BIRIKIM.wms.IRS.EvrakNo, Depo_1.DepoKodu AS CikisDepo, Depo_2.DepoKodu AS AraDepo, BIRIKIM.wms.Depo.DepoKodu AS GirisDepo, BIRIKIM.wms.Gorev.Olusturan, BIRIKIM.wms.[Transfer].Onaylayan, 
							 BIRIKIM.dbo.fnFormatDateFromInt(BIRIKIM.wms.Gorev.OlusturmaTarihi) AS Tarih, BIRIKIM.wms.Transfer_Detay.MalKodu, FINSAT633.FINSAT633.STK.MalAdi, BIRIKIM.wms.Transfer_Detay.Miktar, 
							 BIRIKIM.wms.Transfer_Detay.Birim
	FROM            BIRIKIM.wms.[Transfer] INNER JOIN
							 BIRIKIM.wms.Transfer_Detay ON BIRIKIM.wms.[Transfer].ID = BIRIKIM.wms.Transfer_Detay.TransferID INNER JOIN
							 BIRIKIM.wms.Gorev ON BIRIKIM.wms.[Transfer].GorevID = BIRIKIM.wms.Gorev.ID INNER JOIN
							 BIRIKIM.wms.IRS ON BIRIKIM.wms.Gorev.IrsaliyeID = BIRIKIM.wms.IRS.ID INNER JOIN
							 BIRIKIM.wms.Depo ON BIRIKIM.wms.[Transfer].GirisDepoID = BIRIKIM.wms.Depo.ID INNER JOIN
							 BIRIKIM.wms.Depo AS Depo_1 ON BIRIKIM.wms.[Transfer].CikisDepoID = Depo_1.ID INNER JOIN
							 BIRIKIM.wms.Depo AS Depo_2 ON BIRIKIM.wms.[Transfer].AraDepoID = Depo_2.ID LEFT OUTER JOIN
							 FINSAT633.FINSAT633.STK ON BIRIKIM.wms.Transfer_Detay.MalKodu = FINSAT633.FINSAT633.STK.MalKodu
	WHERE        (BIRIKIM.wms.[Transfer].ID = @TransferID)

END
GO
/****** Object:  StoredProcedure [wms].[TransferKontrol]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 26.12.2017
-- Last Modify: 26.12.2017
-- Description:	Depolar Arası Transfer özet sayfasındaki ilk kontrol
-- =============================================
CREATE PROCEDURE [wms].[TransferKontrol]
	@CikisDepo varchar(3),
	@MalKodus varchar(MAX)

AS
BEGIN
	
	SELECT        STK.MalKodu, BIRIKIM.wms.fnGetStock(@CikisDepo, STK.MalKodu, '', 0) AS Depo2WmsStok, ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0) AS Depo2GunesStok
	FROM            FINSAT633.FINSAT633.STK AS STK WITH (NOLOCK) LEFT OUTER JOIN
							 FINSAT633.FINSAT633.DST AS DST WITH (NOLOCK) ON STK.MalKodu = DST.MalKodu AND DST.Depo = @CikisDepo
	WHERE        (STK.MalKodu IN (SELECT * FROM dbo.splitstring(@MalKodus)))

END
GO
/****** Object:  StoredProcedure [wms].[TransferList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 26.12.2017
-- Last Modify: 26.12.2017
-- Description:	Depolar Arası Transfer listeleme
-- =============================================
CREATE PROCEDURE [wms].[TransferList]
	@CikisDepo varchar(3),
	@GirisDepo varchar(3),
	@GerekenKadar bit

AS
BEGIN
	
	SELECT        STK.MalKodu, STK.MalAdi, STK.Birim1 AS Birim, ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0) AS Depo1StokMiktar, ISNULL(DST.KritikStok, 0) AS Depo1KritikMiktar, ISNULL(DST.DvrMiktar, 0) 
							 + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0) - ISNULL(DST.KritikStok, 0) AS Depo1GerekenMiktar, ISNULL(DST.AlSiparis, 0) AS AlSiparis, ISNULL(DST.SatSiparis, 0) AS SatSiparis, 
							 BIRIKIM.wms.fnGetStock(@CikisDepo, STK.MalKodu, '', 0) AS Depo2WmsStok, ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0) AS Depo2GunesStok, ISNULL(DST2.KritikStok, 0) AS Depo2KritikMiktar, 
							 ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0) - ISNULL(DST2.KritikStok, 0) AS Depo2GerekenMiktar, CAST(0 AS DECIMAL) AS Depo2Miktar
	FROM            FINSAT633.STK AS STK WITH (NOLOCK) LEFT OUTER JOIN
							 FINSAT633.DST AS DST WITH (NOLOCK) ON STK.MalKodu = DST.MalKodu AND DST.Depo = @GirisDepo LEFT OUTER JOIN
							 FINSAT633.DST AS DST2 WITH (NOLOCK) ON STK.MalKodu = DST2.MalKodu AND DST2.Depo = @CikisDepo
	WHERE        (ISNULL(DST2.DvrMiktar, 0) + ISNULL(DST2.GirMiktar, 0) - ISNULL(DST2.CikMiktar, 0) > 0) AND 
							case when @GerekenKadar = 1 then case when (ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0) - ISNULL(DST.KritikStok, 0) < 0) then 1 else 0 end else 1 end = 1
	ORDER BY DST.MalKodu

END
GO
/****** Object:  StoredProcedure [wms].[TransferSummaryListKontrol]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hüseyin Sekmenoğlu
-- Modifier:	Hüseyin Sekmenoğlu
-- Create date: 26.12.2017
-- Last Modify: 26.12.2017
-- Description:	Depolar Arası Transfer özet sayfasındaki liste
-- =============================================
CREATE PROCEDURE [wms].[TransferSummaryListKontrol]
	@CikisDepo varchar(5),
	@MalKodu varchar(50)

AS
BEGIN
	
	SELECT        STK.MalKodu, BIRIKIM.wms.fnGetStock(@CikisDepo, STK.MalKodu, '', 0) AS Depo2WmsStok, ISNULL(DST.DvrMiktar, 0) + ISNULL(DST.GirMiktar, 0) - ISNULL(DST.CikMiktar, 0) AS Depo2GunesStok
	FROM            FINSAT633.FINSAT633.STK AS STK WITH (NOLOCK) LEFT OUTER JOIN
							 FINSAT633.FINSAT633.DST AS DST WITH (NOLOCK) ON STK.MalKodu = DST.MalKodu AND DST.Depo = @CikisDepo
	WHERE        (STK.MalKodu = @MalKodu)

END
GO
/****** Object:  StoredProcedure [wms].[TumSiparisOnayList]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[TumSiparisOnayList]
	@OnayDurm VARCHAR(20),
	@Secim SMALLINT,
	@BasTarih INT,
	@BitTarih INT,
	@ChkAralik VARCHAR(100),
	@Sirketler VARCHAR(100),
	@TipKodlari VARCHAR(500),
	@Kod3Aralik VARCHAR(100),
	@RiskAralik VARCHAR(500),
	@Kod3Aralik2 VARCHAR(100),
	@RiskAralik2 VARCHAR(500),
	@Grup VARCHAR(50)
AS
BEGIN
DECLARE 
@Tarih INT,
@CHK1 VARCHAR(20),
@CHK2 VARCHAR(20),
@Kod31 VARCHAR(50),
@Kod32 VARCHAR(50),
@Risk1 VARCHAR(20),
@Risk2 VARCHAR(20),
@Kod312 VARCHAR(50),
@Kod322 VARCHAR(50),
@Risk12 VARCHAR(20),
@Risk22 VARCHAR(20)
SET @Tarih = DATEDIFF(dd,0,GETDATE()+2)
 IF PATINDEX('%;%', @ChkAralik) > 0
BEGIN
    SET @CHK1 = SUBSTRING(@ChkAralik,1,PATINDEX('%;%', @ChkAralik)-1)

    SET @CHK2 =  SUBSTRING(@ChkAralik,PATINDEX('%;%', @ChkAralik)+1,50)
END
ELSE
BEGIN
    SET @CHK1 = ''
    SET @CHK2 = 'ZZZZZZZZZZ'
END

IF PATINDEX('%;%', @Kod3Aralik) > 0
BEGIN
	SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
    IF @Kod31 = '' SET @Kod31 = '0'
	ELSE SET @Kod31 = SUBSTRING(@Kod3Aralik,1,PATINDEX('%;%', @Kod3Aralik)-1)
    IF @Kod32 = '' SET @Kod32 = '9999999999999999'
	ELSE SET @Kod32 =  SUBSTRING(@Kod3Aralik,PATINDEX('%;%', @Kod3Aralik)+1,50)
END
ELSE
BEGIN
    SET @Kod31 = '0'
    SET @Kod32 = '9999999999999999'
END

IF PATINDEX('%;%', @Kod3Aralik2) > 0
BEGIN
	SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
    IF @Kod312 = '' SET @Kod312 = '0'
	ELSE SET @Kod312 = SUBSTRING(@Kod3Aralik2,1,PATINDEX('%;%', @Kod3Aralik2)-1)
    IF @Kod322 = '' SET @Kod322 = '9999999999999999'
	ELSE SET @Kod322 =  SUBSTRING(@Kod3Aralik2,PATINDEX('%;%', @Kod3Aralik2)+1,50)
END
ELSE
BEGIN
    SET @Kod312 = '0'
    SET @Kod322 = '9999999999999999'
END

IF PATINDEX('%;%', @RiskAralik) > 0
BEGIN
	SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
	IF @Risk1 = '' SET @Risk1 = '0'
	ELSE SET @Risk1 = SUBSTRING(@RiskAralik,1,PATINDEX('%;%', @RiskAralik)-1)
    IF @Risk2 = '' SET @Risk2 = '9999999999999999'
	ELSE SET @Risk2 =  SUBSTRING(@RiskAralik,PATINDEX('%;%', @RiskAralik)+1,50)
END
ELSE
BEGIN
    SET @Risk1 = '0'
    SET @Risk2 = '9999999999999999'
END

IF PATINDEX('%;%', @RiskAralik2) > 0
BEGIN
	SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
	IF @Risk12 = '' SET @Risk12 = '0'
	ELSE SET @Risk12 = SUBSTRING(@RiskAralik2,1,PATINDEX('%;%', @RiskAralik2)-1)
    IF @Risk22 = '' SET @Risk22 = '9999999999999999'
	ELSE SET @Risk22 =  SUBSTRING(@RiskAralik2,PATINDEX('%;%', @RiskAralik2)+1,50)
END
ELSE
BEGIN
    SET @Risk12 = '0'
    SET @Risk22 = '9999999999999999'
END

SELECT 
		*,
		(AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) as RiskBakiyesi,
		(((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) as Risk
FROM
(
	SELECT 
		CONVERT(bit, @Secim) AS Secim,
		a.Chk AS HesapKodu,
		MAX(d.Unvan1 + ' ' + d.Unvan2) AS Unvan,
		d.TipKod AS TipKodu,
		CASE WHEN d.KrediLimiti=0 THEN 1 ELSE d.KrediLimiti END AS KrediLimiti,
		(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA) AS Bakiye,
		AT.SCek AS SCek,
		AT.TCek AS TCek,
		AT.PRBakiye AS PRTBakiye,
		d.Kod2 AS Kod2,
		ST.OrtalamaGun AS OrtGun,
		ST.Kod3OrtGun AS Kod3OrtGun,
		ST.Kod3OrtBakiye AS Kod3OrtBakiye,
		ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.EvrakNo = a.EvrakNo and t.Tarih = @Tarih and t.SiparisDurumu = 0 and t.Kod10 = @OnayDurm), 0) AS SicakSiparis,
		ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Tarih < @Tarih and t.EvrakNo = a.EvrakNo and t.Kod10 = @OnayDurm), 0) AS SogukSiparis ,
		ISNULL((SELECT Sum(t.Kod14) FROM FINSAT633.FINSAT633.SPI (NOLOCK)t WHERE t.Chk = a.Chk and t.SiparisDurumu = 0 and t.Kod10 <> 'Reddedildi' and t.Tarih < @Tarih),0) AS GunIciSiparis,
		CASE a.Tarih WHEN @Tarih THEN 'SICAK' ELSE 'SOĞUK' END AS SiparisTuru,
		a.EvrakNo AS EvrakNo,
		a.Kod10 AS OnayDurumu,
		CONVERT(nvarchar(10), CAST(a.Tarih as datetime) - 2, 104) AS Tarih,
		a.Kod3 AS Firma,
		a.Kod2 AS Onaylayan
	FROM FINSAT633.FINSAT633.SPI (NOLOCK)a
		inner join FINSAT633.FINSAT633.CHK(NOLOCK) d ON d.HesapKodu = a.Chk
		CROSS APPLY FINSAT633.wms.GetOrtGunKod3OrtBakiyeGun(a.Chk,d.Kod2) AS ST
		CROSS APPLY FINSAT633.wms.GetSCekTCekPRBakiye(a.Chk) AS AT
	WHERE a.Kod10=@OnayDurm  
		and a.CHK between @CHK1 AND @CHK2 
		and a.Tarih between @BasTarih and @BitTarih
		and case when @OnayDurm='BEKLEMEDE' then case when (a.SiparisDurumu=0 ) then 1 else 0 end else 1 end  = 1 
		and d.TipKod in (select * from wms.[splitstring](replace(@TipKodlari,';',',')))
		and a.TeslimMiktar+a.KapatilanMiktar<Miktar 
		and a.Kod13>0 
		and a.Kod3 in (select * from wms.[splitstring](replace(@Sirketler,';',',')))  
		and (SELECT sum(v.GirMiktar-v.CikMiktar+v.DvrMiktar) FROM FINSAT633.FINSAT633.DST(NOLOCK) v WHERE v.MalKodu=a.MalKodu and v.Depo=a.Depo)>=a.Miktar

	GROUP BY a.Chk,d.TipKod,d.KrediLimiti,(d.DvrB + d.OdemeB + (d.CiroB + IadeFatB) + d.KDVB + d.DigerB) - (d.DvrA + d.OdemeA + (d.CiroA + d.IadeFatA) + d.KDVA + d.DigerA),d.Kod2,a.Tarih,a.EvrakNo,a.Kod10,a.Kod3,a.Kod2,ST.OrtalamaGun,ST.Kod3OrtGun,ST.Kod3OrtBakiye,AT.SCek,AT.TCek,AT.PRBakiye
) AA
WHERE 	 
(
	(@Grup='Finans' AND Kod2 like '%8%') OR
	(@Grup<>'Finans' AND Kod2 not like '%8%') or 
	(@Grup='Bölge Sorumlusu' AND Kod2=9)
)
and 
(
	(
		@Grup='Sipariş Masası' OR  
		@Grup='Admin' OR 
		@Grup='Depo Sorumlusu' OR 
		(@Grup='GM' and   
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk1 and @Risk2) 
			AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod31 and @Kod32)
			)        
			OR 
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk12 and @Risk22) 
			AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod312 and @Kod322)
			)
		)
	)
	OR
	(
		(
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk1 and @Risk2) 
				AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod31 and @Kod32)
			)	  
			OR 
			(
				((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100) between @Risk12 and @Risk22) 
				AND
				((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5)) between @Kod312 and @Kod322)
			)
		)
		AND	 
		(
			(
				Kod2 LIKE '%8%'AND (@Grup='Finans'  or  @Grup='Admin')
			) 
			OR 
			(
				(
					@Grup<>'Satış Müdürü' OR 
					@Grup<>'Bölge Sorumlusu' OR 
					@Grup<>'Finans'
				) 
				AND 
				(
					(AA.Kod3OrtGun>=60 AND @Grup='Finans') 
					OR 
					(
						(AA.Kod3OrtGun<60 )
						AND 
						(  
							((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)>=125 AND @Grup='Satış Müdürü') 
							OR 
							(
								((((AA.Bakiye+AA.SCek+AA.GunIciSiparis+((AA.TCek-AA.SCek)/5))/AA.KrediLimiti)*100)<125) 
								AND 		
								(	
									(AA.Kod3OrtGun<60 AND AA.Kod3OrtGun>=30 AND @Grup='Satış Müdürü') 
									OR 
									(AA.Kod3OrtGun>=0 AND AA.Kod3OrtGun<30 AND @Grup='Bölge Sorumlusu' )										
								)
							)
						)
					)
				)
			)
		)
	)
)
END


GO
/****** Object:  StoredProcedure [wms].[VadesiGelmemisCekler]    Script Date: 05/04/2018 10:33:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [wms].[VadesiGelmemisCekler]
@BasTarih INT,
@BitTarih INT
AS
BEGIN
	
	SELECT SCI.EvrakNo, SCI.CHK, CHK.Unvan1+' '+CHK.Unvan2 AS Unvan,
	cast(SCI.Tutar as numeric(36,2)) as Tutar,
	--replace(replace(replace(convert(varchar,CAST(SCI.Tutar as money),1),'.','x'),',','.'),'x',',') as Tutar,
	 CONVERT(VARCHAR(15),CONVERT(DATETIME, SCI.Tarih-2),104) AS Tarih, CONVERT(VARCHAR(15),CONVERT(DATETIME, ACK.VadeTarih-2),104) AS VadeTarih,
ISNULL((select top 1 S.CHK FROM FINSAT633.FINSAT633.SCI (NOLOCK) S WHERE S.Sirano = (SELECT max(Sirano) FROM FINSAT633.FINSAT633.SCI (NOLOCK) SC WHERE SC.Islemtip=2 and SC.Evrakno=SCI.Evrakno ) and s.evrakno=SCI.Evrakno),'') as VerildigiYer,
ISNULL((select top 1 (CHK.Unvan1+' '+chk.Unvan2)   FROM FINSAT633.FINSAT633.SCI (NOLOCK) S INNER JOIN FINSAT633.FINSAT633.CHK (NOLOCK) CHK ON CHK.HesapKodu=S.CHK WHERE S.Sirano = (SELECT max(Sirano) FROM FINSAT633.FINSAT633.SCI (NOLOCK) SC WHERE SC.Islemtip=2 and SC.Evrakno=SCI.Evrakno ) and s.evrakno=SCI.Evrakno),'') as VerildigiYerUnvan
, (ACK.BorcluUnvan1 + '  '+ ACK.BorcluUnvan2) AS CekiDuzenleyen
FROM FINSAT633.FINSAT633.SCI(NOLOCK) SCI 
INNER JOIN FINSAT633.FINSAT633.CHK(NOLOCK) CHK ON SCI.CHK=CHK.HesapKodu
INNER JOIN FINSAT633.FINSAT633.ACK (NOLOCK) ACK ON SCI.EvrakNo=ACK.EvrakNo 
WHERE SCI.IslemTip=0 AND SCI.ScTip=0 AND (ACK.VadeTarih BETWEEN @BasTarih AND @BitTarih) AND ACK.SonIslemTip NOT IN (5,12,13) 
ORDER BY ACK.VadeTarih

END















GO
