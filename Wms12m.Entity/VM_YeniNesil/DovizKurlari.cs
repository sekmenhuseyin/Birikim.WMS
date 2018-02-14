namespace Wms12m.Entity
{
    public class DovizKurlari
    {
        public static string Sorgu = @"
            SELECT TOP 1 DVZ002_DvzEfektisSatis1 as USD, DVZ002_DvzEfektisSatis2 as EUR
            FROM   YNS{{0}}.YNS{{0}}.DVZ002(NOLOCK)
            WHERE  DVZ002_DvzEfektisSatis1>0 AND DVZ002_DovizDate IN({0},{1},{2})
            ORDER BY DVZ002_DovizDate DESC";

        public decimal EUR { get; set; }
        public decimal USD { get; set; }
    }
}