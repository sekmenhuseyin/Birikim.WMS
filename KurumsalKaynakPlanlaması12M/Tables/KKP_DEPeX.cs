using System.Data;

namespace KurumsalKaynakPlanlaması12M
{
   public class KKP_DEP
    {
        #region DEP Column Names


        [DbAlan("Depo", SqlDbType.VarChar, 5, false, true, false)]
        public string Depo { get; set; }

        [DbAlan("DepoAdi", SqlDbType.VarChar, 40)]
        public string DepoAdi { get; set; }

        [DbAlan("Adres1", SqlDbType.VarChar, 30)]
        public string Adres1 { get; set; }

        [DbAlan("Adres2", SqlDbType.VarChar, 30)]
        public string Adres2 { get; set; }

        [DbAlan("Adres3", SqlDbType.VarChar, 30)]
        public string Adres3 { get; set; }

        [DbAlan("Telefon1", SqlDbType.VarChar, 14)]
        public string Telefon1 { get; set; }

        [DbAlan("Telefon2", SqlDbType.VarChar, 14)]
        public string Telefon2 { get; set; }

        [DbAlan("Telefon3", SqlDbType.VarChar, 14)]
        public string Telefon3 { get; set; }

        [DbAlan("Fax", SqlDbType.VarChar, 14)]
        public string Fax { get; set; }

        [DbAlan("BolgeKod", SqlDbType.VarChar, 4)]
        public string BolgeKod { get; set; }

        [DbAlan("GrupKod", SqlDbType.VarChar, 10)]
        public string GrupKod { get; set; }

        [DbAlan("TipKod", SqlDbType.VarChar, 10)]
        public string TipKod { get; set; }

        [DbAlan("Kod1", SqlDbType.VarChar, 10)]
        public string Kod1 { get; set; }

        [DbAlan("Kod2", SqlDbType.VarChar, 10)]
        public string Kod2 { get; set; }

        [DbAlan("Kod3", SqlDbType.VarChar, 10)]
        public string Kod3 { get; set; }

        [DbAlan("Yetkili1", SqlDbType.VarChar, 30)]
        public string Yetkili1 { get; set; }

        [DbAlan("Yetkili2", SqlDbType.VarChar, 30)]
        public string Yetkili2 { get; set; }

        [DbAlan("Yetkili1EMail", SqlDbType.VarChar, 50)]
        public string Yetkili1EMail { get; set; }

        [DbAlan("Yetkili2EMail", SqlDbType.VarChar, 50)]
        public string Yetkili2EMail { get; set; }

        [DbAlan("SonSayimSonuc", SqlDbType.Decimal, 13)]
        public decimal SonSayimSonuc { get; set; }

        [DbAlan("MerkezDepo", SqlDbType.SmallInt, 2)]
        public short MerkezDepo { get; set; }

        [DbAlan("GuvenlikKod", SqlDbType.VarChar, 2)]
        public string GuvenlikKod { get; set; }

        [DbAlan("Kaydeden", SqlDbType.VarChar, 5)]
        public string Kaydeden { get; set; }

        [DbAlan("KayitTarih", SqlDbType.Int, 4)]
        public int KayitTarih { get; set; }

        [DbAlan("KayitSaat", SqlDbType.Int, 4)]
        public int KayitSaat { get; set; }

        [DbAlan("KayitKaynak", SqlDbType.SmallInt, 2)]
        public short KayitKaynak { get; set; }

        [DbAlan("KayitSurum", SqlDbType.VarChar, 12)]
        public string KayitSurum { get; set; }

        [DbAlan("Degistiren", SqlDbType.VarChar, 5)]
        public string Degistiren { get; set; }

        [DbAlan("DegisTarih", SqlDbType.Int, 4)]
        public int DegisTarih { get; set; }

        [DbAlan("DegisSaat", SqlDbType.Int, 4)]
        public int DegisSaat { get; set; }

        [DbAlan("DegisKaynak", SqlDbType.SmallInt, 2)]
        public short DegisKaynak { get; set; }

        [DbAlan("DegisSurum", SqlDbType.VarChar, 12)]
        public string DegisSurum { get; set; }

        [DbAlan("CheckSum", SqlDbType.SmallInt, 2)]
        public short CheckSum { get; set; }

        [DbAlan("GirisMhsKod", SqlDbType.VarChar, 20)]
        public string GirisMhsKod { get; set; }

        [DbAlan("CikisMhsKod", SqlDbType.VarChar, 20)]
        public string CikisMhsKod { get; set; }

        [DbAlan("TuketimMhsKod", SqlDbType.VarChar, 20)]
        public string TuketimMhsKod { get; set; }

        [DbAlan("DepoTipi", SqlDbType.SmallInt, 2)]
        public short DepoTipi { get; set; }

        [DbAlan("Row_ID", SqlDbType.Int, 4, true, false, false)]
        public int Row_ID { get; set; }
        

       #endregion

        internal KKP_DEP()
        {

        }
    }
}
