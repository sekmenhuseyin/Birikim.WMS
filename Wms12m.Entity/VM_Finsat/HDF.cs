using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Entity
{
    public class HDF
    {
        public int ID { get; set; }
        public short TIP { get; set; }
        public string BOLGE { get; set; }
        public string TEMSILCI { get; set; }
        public string URUNGRUP { get; set; }
        public string HEDEF { get; set; } //decimal
        public int TARIH { get; set; }
        public string AYYIL { get; set; }
        public static string SelectSorgu = @"
                                            SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,CONVERT(NVARCHAR,H1.HEDEF) AS HEDEF,H1.TARIH,H1.AYYIL 
                                            FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) WHERE H1.TIP={1}
                                            ";
        public static string TInsertSorgu = @"
                    DECLARE @MAXDURUM INT,@HDFT1 NUMERIC(25,6),@HDFT0 NUMERIC(25,6)
                    SET @MAXDURUM = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) WHERE H1.BOLGE='{1}' AND H1.TIP=0 
                    AND H1.AYYIL=REPLACE(RIGHT(CONVERT(VARCHAR(12),DATEADD(DD,{5},'12-30-1899'),104),7),'.','')
                    )
                    IF(@MAXDURUM > 0)
                    BEGIN
                    SET @HDFT1 = (SELECT ISNULL(SUM(H2.HEDEF),0) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H2 WITH (NOLOCK) WHERE H2.BOLGE='{1}' AND H2.TIP=1 
                    AND H2.AYYIL=REPLACE(RIGHT(CONVERT(VARCHAR(12),DATEADD(DD,{5},'12-30-1899'),104),7),'.',''))
                    SET @HDFT0 = (SELECT H0.HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS H0 WITH (NOLOCK) WHERE H0.BOLGE='{1}' AND H0.TIP=0 
                    AND H0.AYYIL=REPLACE(RIGHT(CONVERT(VARCHAR(12),DATEADD(DD,{5},'12-30-1899'),104),7),'.',''))
                    IF(@HDFT1 < @HDFT0)
                    BEGIN
                    INSERT INTO FINSAT6{0}.FINSAT6{0}.HDF (TIP,BOLGE,TEMSILCI,URUNGRUP,HEDEF,TARIH,AYYIL) 
                    VALUES (1,'{1}','{2}','{3}','{4}',{5},'{6}')
                    SELECT 1
                    END
                    ELSE
                    BEGIN
                    SELECT 10
                    END
                    END
                    ELSE
                    BEGIN
                    SELECT 5
                    END";
        public static string TUpdateSorgu = @"
                    DECLARE @MAXDURUM INT,@HDFT1 NUMERIC(25,6),@HDFT0 NUMERIC(25,6),@VAL NUMERIC(25,6)
                    SET @MAXDURUM = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) WHERE H1.BOLGE='{1}' AND H1.TIP=1 
                    AND H1.AYYIL='{6}'
                    )
                    IF(@MAXDURUM>0)
                    BEGIN
                    SET @HDFT1= (SELECT SUM(H1.HEDEF) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                    WHERE H1.BOLGE='{1}' AND H1.TIP=1 AND H1.AYYIL='{6}')
                    SET @HDFT0= (SELECT H0.HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS H0 WITH (NOLOCK)
                    WHERE H0.BOLGE='{1}' AND H0.TIP=0 AND H0.AYYIL='{6}')
                    SET @VAL = (SELECT H2.HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS H2 WITH (NOLOCK) WHERE H2.ID={5} AND H2.TIP=1)
                    IF({4}<=(@HDFT0-@HDFT1)+@VAL)
                    BEGIN
                    UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE='{1}',TEMSILCI='{2}',URUNGRUP='{3}',HEDEF='{4}' WHERE ID={5} AND TIP=1
                    SELECT 1
                    END
                    ELSE
                    BEGIN
                    SELECT 10
                    END
                    END
                    ELSE
                    BEGIN
                    SELECT 5
                    END";
        public static string BInsertSorgu = @"
                    DECLARE @MAXDURUM INT SET @MAXDURUM=(SELECT COUNT(*)
                    FROM FINSAT6{0}.FINSAT6{0}.HDF AS H0 WITH (NOLOCK) WHERE H0.TIP=0 AND H0.BOLGE='{1}' AND H0.AYYIL='{4}')
                    IF(@MAXDURUM = 0)
                    BEGIN
                    INSERT INTO FINSAT6{0}.FINSAT6{0}.HDF (TIP,BOLGE,HEDEF,TARIH,AYYIL) VALUES (0,'{1}','{2}',{3},'{4}')
                    SELECT 1
                    END
                    ELSE
                    BEGIN
                    SELECT 10
                    END";
        public static string BUpdateSorgu = @"
                    DECLARE @MAXDURUM INT,@HDF01 NUMERIC(25,6),@TK INT
                    SET @MAXDURUM = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H0 WITH (NOLOCK) 
                    WHERE H0.TIP=0 AND H0.BOLGE='{1}' AND H0.AYYIL='{4}')
                    IF(@MAXDURUM = 1)
                    BEGIN
                    SET @HDF01 = (SELECT SUM(H1.HEDEF) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                    WHERE H1.TIP=1 AND H1.BOLGE='{1}' AND H1.AYYIL='{4}')
                    IF({2} > @HDF01)
                    BEGIN
                    UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE='{1}',HEDEF='{2}' WHERE ID={3} AND TIP = 0
                    SELECT 1
                    END
                    ELSE
                    BEGIN
                    SELECT 7
                    END
                    END
                    ELSE
                    BEGIN
                    SET @TK = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H3 WITH (NOLOCK)
                    WHERE H3.TIP=1 AND H3.BOLGE='{1}' AND H3.AYYIL='{4}')
                    IF(@TK = 0)
                    BEGIN
                    UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE='{1}',HEDEF='{2}' WHERE ID={3} AND TIP=0
                    SELECT 1
                    END
                    ELSE
                    BEGIN
                    SELECT 5
                    END
                    END";
        public static string BDeleteSorgu = @"
            DECLARE @MAXDURUM INT SET @MAXDURUM=(
            SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
            LEFT JOIN (
            SELECT IC0.BOLGE,IC0.AYYIL FROM FINSAT6{0}.FINSAT6{0}.HDF AS IC0 WITH (NOLOCK)
            WHERE IC0.TIP=0 AND IC0.ID={1}
            ) AS D ON H1.BOLGE=D.BOLGE AND H1.AYYIL=D.AYYIL
            WHERE H1.AYYIL=D.AYYIL AND H1.BOLGE=D.BOLGE AND H1.TIP=1)
            IF(@MAXDURUM=0)
            BEGIN
            DELETE FROM [FINSAT6{0}].[FINSAT6{0}].HDF WHERE ID={1} AND TIP=0
            SELECT 1
            END
            ELSE
            BEGIN
            SELECT 5
            END";
    }
}
