namespace Wms12m.Entity
{
    public class HDF
    {
        public int ID { get; set; }
        public short TIP { get; set; } //TIP=0->BOLGE,TIP=1->TEMSILCI,TIP=2 ->URUNGRUP
        public string BOLGE { get; set; }
        public string TEMSILCI { get; set; }
        public string URUNGRUP { get; set; }
        public string HEDEF { get; set; }
        public int TARIH { get; set; }
        public string AYYIL { get; set; }
        public static string SelectSorgu = @"
                                            SELECT H1.ID,H1.TIP,H1.BOLGE,H1.TEMSILCI,H1.URUNGRUP,REPLACE(CONVERT(DECIMAL(25,2),H1.HEDEF),'.',',') AS HEDEF,H1.TARIH,
                                            (CASE 
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='01' THEN CONCAT('Ocak',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='02' THEN CONCAT('Şubat',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='03' THEN CONCAT('Mart',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='04' THEN CONCAT('Nisan',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='05' THEN CONCAT('Mayıs',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='06' THEN CONCAT('Haziran',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='07' THEN CONCAT('Temmuz',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='08' THEN CONCAT('Ağustos',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='09' THEN CONCAT('Eylül',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='10' THEN CONCAT('Ekim',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            WHEN SUBSTRING(H1.AYYIL,1,2)='11' THEN CONCAT('Kasım',SPACE(1),SUBSTRING(H1.AYYIL,3,4))
                                            ELSE CONCAT('Aralık',SPACE(1),SUBSTRING(H1.AYYIL,3,4)) END) AS AYYIL
                                            FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) WHERE H1.TIP={1}
                                            ";
        public static string HdfGrupTanimInsert = @"
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
                    END
                    ";
        public static string HdfGrupTanimUpdate = @"
                    DECLARE @MAXDURUM INT,@HDF01 NUMERIC(25,6),@TK INT
                    SET @MAXDURUM = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H0 WITH (NOLOCK) 
                    WHERE H0.TIP=0 AND H0.BOLGE='{1}' AND H0.AYYIL='{4}')
                    IF(@MAXDURUM = 1)
                    BEGIN
                    SET @HDF01 = (SELECT ISNULL(SUM(H1.HEDEF),0) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                    WHERE H1.TIP=1 AND H1.BOLGE='{1}' AND H1.AYYIL='{4}')
                    IF({2} >= @HDF01)
                    BEGIN
                    UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE='{1}',HEDEF='{2}' WHERE ID = {3} AND TIP = 0
                    SELECT 1
                    END
                    ELSE
                    BEGIN
                    SELECT 7
                    END
                    END
                    ELSE
                    BEGIN
                    SET @TK = (SELECT COUNT(*) FROM FINSAT671.FINSAT671.HDF AS H3 WITH (NOLOCK)
                    WHERE H3.TIP=1 AND H3.BOLGE='{1}' AND H3.AYYIL='{4}')
                    IF(@TK = 0)
                    BEGIN
                    UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE='{1}',HEDEF='{2}' WHERE ID = {3} AND TIP = 0
                    SELECT 1
                    END
                    ELSE
                    BEGIN
                    SELECT 5
                    END
                    END
                    ";
        public static string HdfGrupTanimDelete = @"
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
            END
            ";
        public static string TemsilciGrupTanimInsert = @"
                    DECLARE @MAXDURUM INT,@TK INT,@HDFT1 NUMERIC(25,6),@HDFT0 NUMERIC(25,6)
                    SET @MAXDURUM = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) WHERE H1.BOLGE='{1}' AND H1.TIP=0 AND H1.AYYIL='{5}')
                    SET @TK = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H3 WITH (NOLOCK) WHERE
                    H3.BOLGE='{1}' AND H3.TIP = 1 AND H3.AYYIL = '{5}' AND H3.TEMSILCI='{2}')
                    
                    INSERT INTO FINSAT6{0}.FINSAT6{0}.HDF (TIP,BOLGE,TEMSILCI,HEDEF,TARIH,AYYIL) 
                    VALUES (1,'{1}','{2}','{3}',{4},'{5}')
                    IF(@TK = 0)
                    BEGIN                    
                    IF(@MAXDURUM > 0)
                    BEGIN
                    SET @HDFT1 = (SELECT ISNULL(SUM(H2.HEDEF),0) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H2 WITH (NOLOCK) WHERE H2.BOLGE='{1}' AND H2.TIP=1 AND H2.AYYIL='{5}')
                    SET @HDFT0 = (SELECT H0.HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS H0 WITH (NOLOCK) WHERE H0.BOLGE='{1}' AND H0.TIP=0 AND H0.AYYIL='{5}')
                    IF(@HDFT1 + {3} <= @HDFT0)
                    BEGIN
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
                    END
                    END
                    ELSE
                    BEGIN
                    SELECT 15
                    END

                    ";
        public static string TemsilciGrupTanimUpdate = @"
                    DECLARE @MAXDURUM INT,@HDFT1 NUMERIC(25,6),@HDFT0 NUMERIC(25,6),@VAL NUMERIC(25,6)
                    SET @MAXDURUM = (SELECT COUNT(*) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK) WHERE H1.BOLGE='{1}' AND H1.TIP=1 AND H1.AYYIL='{5}')
                    IF(@MAXDURUM > 0)
                    BEGIN
                    SET @HDFT1= (SELECT SUM(H1.HEDEF) FROM FINSAT6{0}.FINSAT6{0}.HDF AS H1 WITH (NOLOCK)
                    WHERE H1.BOLGE='{1}' AND H1.TIP=1 AND H1.AYYIL='{5}')
                    SET @HDFT0= (SELECT H0.HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS H0 WITH (NOLOCK)
                    WHERE H0.BOLGE='{1}' AND H0.TIP=0 AND H0.AYYIL='{5}')
                    SET @VAL = (SELECT H2.HEDEF FROM FINSAT6{0}.FINSAT6{0}.HDF AS H2 WITH (NOLOCK) WHERE H2.ID={4} AND H2.TIP = 1)

                    UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET BOLGE='{1}',TEMSILCI='{2}',HEDEF='{3}' WHERE ID={4} AND TIP = 1

                    IF( {3} <= (@HDFT0-@HDFT1)+@VAL )
                    BEGIN
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
                    END
                    ";
        public static string TemsilciGrupTanimDelete = @"
                    DELETE FROM [FINSAT6{0}].[FINSAT6{0}].HDF WHERE ID = {1} AND TIP = 1
                    ";
        public static string UrunGrupTanimInsert = @"INSERT INTO FINSAT6{0}.FINSAT6{0}.HDF (TIP,BOLGE,TEMSILCI,URUNGRUP,HEDEF,TARIH,AYYIL) VALUES (2,'{1}','{2}','{3}','{4}',{5},'{6}')";
        public static string UrunGrupTanimUpdate = @"UPDATE FINSAT6{0}.FINSAT6{0}.HDF SET HEDEF='{1}' WHERE ID = {2} AND TIP = 2";
        public static string UrunGrupTanimDelete = @"DELETE FROM FINSAT{0}.FINSAT{0}.HDF WHERE ID={1} AND TIP = 2";
    }
}
