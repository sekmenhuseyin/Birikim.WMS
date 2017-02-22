namespace Wms12m.Configuration
{
    public class Enm
    {
        /// <summary>
        /// list status
        /// </summary>
        public enum GetListStatus
        {            
            Refresh=0,
            Close=1
        }
        public enum ComboNames
        {
            MalKabul = 1,
            RafaKaldır = 2,
            SiparişTopla = 3,
            KabloSiparişi = 4,
            İrsaliyeKes = 5,
            Paketle = 6,
            SevkiyatıGerçekleştir = 7,
            KontrolSayım = 9,
            Açık = 10,
            Durdurulan = 11,
            Tamamlanan = 12,
        }
        public enum ComboItemNames
        {
            GorevTipleri = 1,
            GorevDurum = 2
        }
    }
}
