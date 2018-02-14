using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Transactions;
using Wms12m.Entity;

namespace Wms12m
{
    public static class SqlExperExtension
    {
        internal static string ObjectSetFormat(this object Deger)
        {
            var text = "";
            if (Deger.GetType().IsEnum)
            {
                if (Deger.GetType().Name == "SetIslem")
                {
                    switch (Deger.ToString())
                    {
                        case "Arti":
                            text = "+";
                            break;

                        case "Eksi":
                            text = "-";
                            break;

                        case "Carpi":
                            text = "*";
                            break;

                        case "Bolu":
                            text = "/";
                            break;
                    }
                }
                else
                {
                    text = Deger.ToString();
                }
            }
            else
            {
                text = string.Format("'{0}'", Deger.ToString().Replace(',', '.'));
            }

            return text;
        }

        internal static string ObjectWhereFormat(this object Deger)
        {
            var text = "";
            if (Deger.GetType().IsEnum)
            {
                if (Deger.GetType().Name == "Islem")
                {
                    switch (Deger.ToString())
                    {
                        case "EsitDegil":
                            text = "<>";
                            break;

                        case "Esit":
                            text = "=";
                            break;

                        case "Buyuk":
                            text = ">";
                            break;

                        case "BuyukEsit":
                            text = ">=";
                            break;

                        case "Kucuk":
                            text = "<";
                            break;

                        case "KucukEsit":
                            text = "<=";
                            break;

                        case "Arti":
                            text = "+";
                            break;

                        case "Eksi":
                            text = "-";
                            break;

                        case "Carpi":
                            text = "*";
                            break;

                        case "Bolu":
                            text = "/";
                            break;

                        case "ileBaslayan":
                        case "ileBiten":
                        case "icindeGecen":
                            text = " Like ";
                            break;
                    }
                }
                else if (Deger.GetType().Name == "Operand")
                {
                    if (Deger.ToString() == "ParAc") text = "(";
                    else if (Deger.ToString() == "ParKapa") text = ")";
                    else if (Deger.ToString() == "OR")
                        text = " " + Deger.ToString();
                    else
                        text = Deger.ToString();
                }
                else
                {
                    text = Deger.ToString();
                }
            }
            else
            {
                text = string.Format("'{0}'", Deger.ToString().Replace(',', '.'));
            }

            return text;
        }
    }

    /// <summary>
    /// SqlExper Sürüm 2.0.2 <br />
    /// Son Güncelleme : 03.10.2016 10:21
    /// </summary>
    public class SqlExper
    {
        public short IndexValue = 0;
        private List<SqlExper> SqlExKomutDizisi = new List<SqlExper>();

        /// <summary> bool param=true sadece diğer yapıcı metotla çakışmasın diye verildi.</summary>
        public SqlExper(string conStr, string sirketKodu)
        {
            GC.Collect();
            ConStr = conStr;
            SirketKodu = sirketKodu;
            SqlExKomutDizisi.Clear();
        }

        private SqlExper(SQLType SqlType = SQLType.NONE)
        {
            Parametreler = new List<object>();

            switch (SqlType)
            {
                case SQLType.NONE:
                    SqlKomut = "";
                    break;

                case SQLType.SELECT:
                    SqlKomut = "SELECT {0} FROM {1}(NOLOCK) WHERE {2}";
                    break;

                case SQLType.INSERT:
                    SqlKomut = "INSERT INTO {0} ({1}) VALUES({2})";
                    break;

                case SQLType.UPDATE:
                    SqlKomut = "UPDATE {0} SET {1} WHERE {2}";
                    break;

                case SQLType.DELETE:
                    SqlKomut = "DELETE {0} WHERE {1}";
                    break;

                case SQLType.KAYITVARMI:
                    SqlKomut = @"IF EXISTS({0}) SELECT CAST(1 AS BIT) SONUC
                                 ELSE           SELECT CAST(0 AS BIT) SONUC";
                    break;

                case SQLType.KAYITADEDI:
                    SqlKomut = "SELECT COUNT(*) as SONUC FROM ({0}) A";
                    break;

                default:
                    SqlKomut = "";
                    break;
            }
        }

        private enum SQLType
        {
            NONE = 0, SELECT = 1, INSERT = 2, UPDATE = 3, DELETE = 4, KAYITVARMI = 5, KAYITADEDI = 6
        }

        private string ConStr { get; set; }
        private List<object> Parametreler { get; set; }
        private string SirketKodu { get; set; }
        private string SqlKomut { get; set; }

        public Result AcceptChanges()
        {
            try
            {
                using (TransactionScope Scope = new TransactionScope())
                {
                    using (DataContext Context = new DataContext(ConStr))
                    {
                        foreach (SqlExper item in SqlExKomutDizisi)
                            Context.ExecuteCommand(item.SqlKomut, item.Parametreler.ToArray());
                    }

                    Scope.Complete();
                    SqlExKomutDizisi.Clear();
                    return new Result(true);
                }
            }
            catch (Exception hata)
            {
                SqlExKomutDizisi.Clear();
                return new Result(false, hata.Message);
            }
        }

        /// <summary>
        /// <para>Where örneği : chk.WhereAdd(CHKE.HesapKodu, Islem.icindeGecen, "120");  >  HesapKodu Like '%120%' </para>
        /// </summary>
        public void Delete(object Nesne, string sirketKodu = null, bool PKeyZorunlu = false)
        {
            if (sirketKodu.IsNull()) sirketKodu = SirketKodu;

            var Wheres = string.Empty;
            var Sqlex = new SqlExper(SqlExper.SQLType.DELETE);
            var Tablo = Nesne.GetType().GetField("info_FullTableName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne).ToString2();
            Tablo = string.Format(Tablo, sirketKodu);
            List<string> ChangedProperties = (List<string>)Nesne.GetType().GetField("ChangedProperties", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            // List<string> WhereList = (List<string>)Nesne.GetType().GetField("WhereList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);

            var sayac = 0;
            foreach (var pi in Nesne.GetType().GetProperties())
            {
                if (pi.Name.Length > 2 && pi.Name.Substring(0, 3) == "pk_" && ChangedProperties.Contains(pi.Name))
                {
                    var orjPropName = "";
                    orjPropName = pi.Name;
                    orjPropName = orjPropName.Remove(0, 3);
                    if (pi.GetValue(Nesne, null).IsNull())
                    {
                        Wheres += orjPropName + "=null AND ";

                        if (PKeyZorunlu)
                            throw new Exception("Primary Key alanlarının tamamı girilmeli !");
                    }
                    else
                    {
                        Wheres += orjPropName + "={" + sayac + "} AND ";
                        Sqlex.Parametreler.Add(pi.GetValue(Nesne, null));
                        sayac++;
                    }
                }
            }

            // foreach (var item in WhereList)
            //    Wheres += " " + item;

            if (Wheres.Length < 2)
                throw new Exception("Delete sorgusu için where kısmında koşul belirtilmemiş !");
            Wheres = Wheres.Remove(Wheres.Length - 4, 4);

            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, Tablo, Wheres);
            SqlExKomutDizisi.Add(Sqlex);
        }

        public void Insert(object Nesne, List<Prop> FieldExtra = null, string sirketKodu = null, params string[] istisnalar)
        {
            if (sirketKodu.IsNull()) sirketKodu = SirketKodu;

            var Sqlex = new SqlExper(SqlExper.SQLType.INSERT);
            string Fields = "", Values = "";
            var Tablo = Nesne.GetType().GetField("info_FullTableName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne).ToString2();
            Tablo = string.Format(Tablo, sirketKodu);
            string[] iKeys = (string[])Nesne.GetType().GetField("info_IdentityKeys", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            List<string> Istisnalar = new List<string>(istisnalar);
            Istisnalar.AddRange(iKeys);

            var sayac = 0;
            foreach (var pi in Nesne.GetType().GetProperties())
            {
                if (Istisnalar.Contains(pi.Name)) continue;

                if (pi.Name.Length > 2 && pi.Name.Substring(0, 3) == "pk_")
                    continue;

                if (!pi.CanWrite) continue;

                if (pi.GetValue(Nesne, null).IsNull())
                {
                    Fields += pi.Name + ",";
                    Values += "null,";
                }
                else
                {
                    Fields += pi.Name + ",";
                    Values += "{" + sayac + "},";
                    Sqlex.Parametreler.Add(pi.GetValue(Nesne, null));
                    sayac++;
                }
            }

            if (Nesne.GetType().GetField("InsertEkList").IsNotNull())
            {
                Dictionary<string, object> InsertEkList = (Dictionary<string, object>)Nesne.GetType().GetField("InsertEkList").GetValue(Nesne);
                foreach (var item in InsertEkList)
                {
                    Fields += item.Key + ",";
                    Values += "{" + sayac + "},";
                    Sqlex.Parametreler.Add(item.Value);
                    sayac++;
                }
            }

            if (FieldExtra != null)
            {
                foreach (var fieldEx in FieldExtra)
                {
                    if (fieldEx.DefaultValue.GetType() == typeof(short[]))
                    {
                        Fields += fieldEx.FieldName + ",";
                        Values += ((short[])fieldEx.DefaultValue)[IndexValue] + ",";
                        IndexValue++;
                    }
                    else
                    {
                        Fields += fieldEx.FieldName + ",";
                        Values += fieldEx.DefaultValue + ",";
                    }
                }
            }

            Fields = Fields.Remove(Fields.Length - 1, 1);
            Values = Values.Remove(Values.Length - 1, 1);

            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, Tablo, Fields, Values);
            SqlExKomutDizisi.Add(Sqlex);
        }

        /// <summary>
        /// Her türlü select ifadesinin içinde joinde olabilir kayıt sayısını döner.
        /// <para>Kullanımı : Sqlex.KayitAdedi("Select * From FINSAT616.CHK WHERE KartTip=2")</para>
        /// </summary>
        /// <param name="Sorgu">Dikkat: Parametreleri çift paranteze alın. {{0}} olacak şekilde </param>
        public int KayitAdedi(string Sorgu, params object[] Parametreler)
        {
            Sorgu = string.Format(Sorgu, SirketKodu);
            var Sqlex = new SqlExper(SqlExper.SQLType.KAYITADEDI);
            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, Sorgu);
            var Adet = 0;
            using (DataContext Context = new DataContext(ConStr))
            {
                Adet = Context.ExecuteQuery<int>(Sqlex.SqlKomut, Parametreler).FirstOrDefault().ToInt32();
            }

            return Adet;
        }

        /// <summary>
        ///  Örnek Kullanım : Kullanici kul = new Kullanici();
        ///  kul.AdSoyad = "Ahmet Ak";
        ///  int adet = Sqlex.KayitAdedi(kul);
        ///  <para>Where koşulu için tüm alanlar ve WhereList kullanılabilir.</para>
        /// </summary>
        public int KayitAdedi(object Nesne, string sirketKodu = null)
        {
            if (sirketKodu.IsNull()) sirketKodu = SirketKodu;

            var Wheres = string.Empty;
            var Sqlex = new SqlExper(SqlExper.SQLType.SELECT);
            var Tablo = Nesne.GetType().GetField("info_FullTableName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne).ToString2();
            Tablo = string.Format(Tablo, sirketKodu);
            List<string> ChangedProperties = (List<string>)Nesne.GetType().GetField("ChangedProperties", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            List<string> WhereList = (List<string>)Nesne.GetType().GetField("WhereList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);

            var sayac = 0;
            foreach (var pi in Nesne.GetType().GetProperties())
            {
                var orjPropName = "";
                ///Primary key dahil değer girilmiş tüm alanlar
                if (ChangedProperties.Contains(pi.Name))
                {
                    orjPropName = pi.Name;
                    if (pi.Name.Length > 2 && pi.Name.Substring(0, 3) == "pk_")
                        orjPropName = orjPropName.Remove(0, 3);

                    if (pi.GetValue(Nesne, null).IsNull())
                    {
                        Wheres += orjPropName + "=null AND ";
                    }
                    else
                    {
                        Wheres += orjPropName + "={" + sayac + "} AND ";
                        Sqlex.Parametreler.Add(pi.GetValue(Nesne, null));
                        sayac++;
                    }
                }
            }

            foreach (var item in WhereList)
                Wheres += " " + item;

            if (Wheres.Length < 2)
                throw new Exception("Select sorgusu için where kısmında koşul belirtilmemiş !");
            Wheres = Wheres.Remove(Wheres.Length - 4, 4);
            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, "*", Tablo, Wheres);

            var SqlexKayitVarmi = new SqlExper(SqlExper.SQLType.KAYITADEDI);
            SqlexKayitVarmi.SqlKomut = string.Format(SqlexKayitVarmi.SqlKomut, Sqlex.SqlKomut);
            var Adet = 0;
            using (DataContext Context = new DataContext(ConStr))
            {
                Adet = Context.ExecuteQuery<int>(SqlexKayitVarmi.SqlKomut, Sqlex.Parametreler.ToArray()).FirstOrDefault().ToInt32();
            }

            return Adet;
        }

        /// <summary>
        /// Her türlü select ifadesini içinde joinde olabilir database kaydı varmı kontrol eder.
        /// <para>Kullanımı : Sqlex.KayitVarMi("Select * From FINSAT616.STI WHERE Row_ID={0}", 7050)</para>
        /// </summary>
        /// <param name="Sorgu">Dikkat: Parametreleri çift paranteze alın. {{0}} olacak şekilde </param>
        public bool KayitVarMi(string Sorgu, params object[] Parametreler)
        {
            Sorgu = string.Format(Sorgu, SirketKodu);
            var Sqlex = new SqlExper(SqlExper.SQLType.KAYITVARMI);
            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, Sorgu);
            var Varmi = false;
            using (DataContext Context = new DataContext(ConStr))
            {
                Varmi = Context.ExecuteQuery<bool>(Sqlex.SqlKomut, Parametreler).FirstOrDefault().ToBool();
            }

            return Varmi;
        }

        /// <summary>
        ///  Örnek Kullanım : Kullanici kul = new Kullanici();
        ///  kul.AdSoyad = "Ahmet Ak";
        ///  bool Varmi = Sqlex.KayitVarMi(kul);
        /// <para>Where koşulu için tüm alanlar ve WhereList kullanılabilir.</para>
        /// </summary>
        public bool KayitVarMi(object Nesne, string sirketKodu = null)
        {
            if (sirketKodu.IsNull()) sirketKodu = SirketKodu;

            var Wheres = string.Empty;
            var Sqlex = new SqlExper(SqlExper.SQLType.SELECT);
            var Tablo = Nesne.GetType().GetField("info_FullTableName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne).ToString2();
            Tablo = string.Format(Tablo, sirketKodu);
            List<string> ChangedProperties = (List<string>)Nesne.GetType().GetField("ChangedProperties", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            List<string> WhereList = (List<string>)Nesne.GetType().GetField("WhereList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);

            var sayac = 0;
            foreach (var pi in Nesne.GetType().GetProperties())
            {
                var orjPropName = "";
                ///Primary key dahil değer girilmiş tüm alanlar
                if (ChangedProperties.Contains(pi.Name))
                {
                    orjPropName = pi.Name;
                    if (pi.Name.Length > 2 && pi.Name.Substring(0, 3) == "pk_")
                        orjPropName = orjPropName.Remove(0, 3);

                    if (pi.GetValue(Nesne, null).IsNull())
                    {
                        Wheres += orjPropName + "=null AND ";
                    }
                    else
                    {
                        Wheres += orjPropName + "={" + sayac + "} AND ";
                        Sqlex.Parametreler.Add(pi.GetValue(Nesne, null));
                        sayac++;
                    }
                }
            }

            foreach (var item in WhereList)
                Wheres += " " + item;

            if (Wheres.Length < 2)
                throw new Exception("Select sorgusu için where kısmında koşul belirtilmemiş !");
            Wheres = Wheres.Remove(Wheres.Length - 4, 4);
            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, "*", Tablo, Wheres);

            var SqlexKayitVarmi = new SqlExper(SqlExper.SQLType.KAYITVARMI);
            SqlexKayitVarmi.SqlKomut = string.Format(SqlexKayitVarmi.SqlKomut, Sqlex.SqlKomut);
            var Varmi = false;
            using (DataContext Context = new DataContext(ConStr))
            {
                Varmi = Context.ExecuteQuery<bool>(SqlexKayitVarmi.SqlKomut, Sqlex.Parametreler.ToArray()).FirstOrDefault().ToBool();
            }

            return Varmi;
        }

        ///<summary>
        /// Nesne kullanmadan bağımsız komut yazmak için kullanılır. Örnek Kullanım :
        ///<para>SqlEx.Komut("Update Personel Set Adi={0} Where ID={1}", "Mehmet", 35);</para>
        ///</summary>
        public void Komut(string Komut, params object[] Parametreler)
        {
            var sqlex = new SqlExper(SQLType.NONE);
            Komut = string.Format(Komut, SirketKodu);
            sqlex.SqlKomut = Komut;
            sqlex.Parametreler = Parametreler.ToList();
            SqlExKomutDizisi.Add(sqlex);
        }

        public List<string> KomutListesi()
        {
            List<string> Liste = new List<string>();
            foreach (var item in SqlExKomutDizisi)
                Liste.Add(string.Format(item.SqlKomut, item.Parametreler.ToArray()));

            return Liste;
        }

        public void RollBack()
        {
            try
            {
                SqlExKomutDizisi.Clear();
            }
            catch { }
        }

        public T SelectFirst<T>(string Sorgu, params object[] Parametreler)
        {
            Sorgu = string.Format(Sorgu, SirketKodu);
            T nesne;
            using (DataContext Context = new DataContext(ConStr))
                nesne = Context.ExecuteQuery<T>(Sorgu, Parametreler).FirstOrDefault();

            return nesne;
        }

        /// <summary>
        /// En Hızlı List'e veri aktarım yöntemi.
        /// SqlDataAdapter'ın DataTable'a veri doldurması kadar hızlı çalışır.
        /// (100.000 satır STI ort: 9.4 sn 566 MB)
        /// </summary>
        /// <param name="Sorgu">Dikkat: Parametreleri çift paranteze alın. {{0}} olacak şekilde </param>
        public List<T> SelectList<T>(string Sorgu, params object[] Parametreler)
        {
            Sorgu = string.Format(Sorgu, SirketKodu);
            List<T> Liste = new List<T>();
            using (DataContext Context = new DataContext(ConStr))
                Liste = Context.ExecuteQuery<T>(Sorgu, Parametreler).ToList();

            return Liste;
        }

        public List<T> SelectList<T>(int tip = 0, string Fields = "*", string Wheres = "1=1")
        {
            var Sqlex = new SqlExper(SqlExper.SQLType.SELECT);
            var Tip = typeof(T);
            var veri = Activator.CreateInstance<T>();
            var Tablo = Tip.GetType().GetField("info_FullTableName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(veri).ToString2();
            Tablo = string.Format(Tablo, SirketKodu);

            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, Fields, Tablo, Wheres);

            List<T> Liste = new List<T>();
            using (DataContext Context = new DataContext(ConStr))
                Liste = Context.ExecuteQuery<T>(Sqlex.SqlKomut).ToList();

            return Liste;
        }

        /// <summary>
        /// 2. Hızlı DataTable'a veri aktarım yöntemi.
        /// DataContext ve ToDataTable() kullanılarak veri DataTable'a aktarılır.
        /// (100.000 satır STI ort: 18.2 sn 860 MB)
        /// </summary>
        /// <param name="sorgu">Dikkat: Parametreleri çift paranteze alın. {{0}} olacak şekilde </param>
        public DataTable SelectTable<T>(string sorgu, params object[] parametreler)
        {
            sorgu = string.Format(sorgu, SirketKodu);
            var dt = new DataTable();
            using (DataContext Context = new DataContext(ConStr))
            {
                dt = Context.ExecuteQuery<T>(sorgu, parametreler).ToList().ToDataTable();
            }

            return dt;
        }

        /// <summary>
        /// En Hızlı DataTable'a veri aktarım yöntemi.
        /// SqlDataAdapter kullanılarak veri DataTable'a aktarılır.
        /// (100.000 satır STI ort: 9.1 sn 554 MB)
        /// </summary>
        public DataTable SelectTable(string Sorgu)
        {
            Sorgu = string.Format(Sorgu, SirketKodu);
            var dt = new DataTable();
            using (SqlDataAdapter Adapter = new SqlDataAdapter(Sorgu, ConStr))
                Adapter.Fill(dt);

            return dt;
        }

        /// <summary>
        /// En Hızlı DataSet'e veri aktarım yöntemi.
        /// SqlDataAdapter kullanılarak veri DataSet'e aktarılır.
        /// </summary>
        public DataSet SelectTables(string Sorgular, params string[] TabloAdlari)
        {
            Sorgular = string.Format(Sorgular, SirketKodu);
            var Dataset = new DataSet();

            if (TabloAdlari.Length > 0)
            {
                using (SqlDataAdapter Adapter = new SqlDataAdapter(Sorgular, ConStr))
                {
                    Adapter.TableMappings.Add("Table", TabloAdlari[0]);
                    for (int i = 1; i < TabloAdlari.Length; i++)
                        Adapter.TableMappings.Add("Table" + i.ToString(), TabloAdlari[i]);

                    Adapter.Fill(Dataset);
                }
            }
            else
            {
                using (SqlDataAdapter Adapter = new SqlDataAdapter(Sorgular, ConStr))
                    Adapter.Fill(Dataset);
            }

            return Dataset;
        }

        /// <summary>
        /// <para>Set örneği   : stk.SetAdd(STKE.GirMiktar, STKE.GirMiktar, SetIslem.Arti, 20);  >  GirMiktar=GirMiktar+20  </para>
        /// <para>Where örneği : chk.WhereAdd(CHKE.HesapKodu, Islem.ileBaslayan, "120");  >  HesapKodu Like '%120' </para>
        /// </summary>
        public void Update(object Nesne, string SetsEk = null, string sirketKodu = null, bool PKeyZorunlu = false, params string[] istisnalar)
        {
            if (sirketKodu.IsNull()) sirketKodu = SirketKodu;

            string Sets = string.Empty, Wheres = string.Empty;
            var Sqlex = new SqlExper(SqlExper.SQLType.UPDATE);
            var Tablo = Nesne.GetType().GetField("info_FullTableName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne).ToString2();
            Tablo = string.Format(Tablo, sirketKodu);
            List<string> ChangedProperties = (List<string>)Nesne.GetType().GetField("ChangedProperties", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            List<string> WhereList = (List<string>)Nesne.GetType().GetField("WhereList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            List<string> SetList = (List<string>)Nesne.GetType().GetField("SetList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            string[] iKeys = (string[])Nesne.GetType().GetField("info_IdentityKeys", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            string[] pKeys = (string[])Nesne.GetType().GetField("info_PrimaryKeys", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Nesne);
            List<string> Istisnalar = new List<string>(istisnalar);
            Istisnalar.AddRange(iKeys);

            var sayac = 0;
            var orjPropName = "";
            foreach (var pi in Nesne.GetType().GetProperties())
            {
                if (Istisnalar.Contains(pi.Name)) continue;

                ///Primary keylerde identity key değilse update edilebilir
                if ((!pKeys.Contains(pi.Name)) && ChangedProperties.Contains(pi.Name))
                {
                    if (pi.GetValue(Nesne, null).IsNull())
                    {
                        Sets += pi.Name + "=null,";
                    }
                    else
                    {
                        Sets += pi.Name + "={" + sayac + "},";
                        Sqlex.Parametreler.Add(pi.GetValue(Nesne, null));
                        sayac++;
                    }
                }
                ///Burada primary keyleri where de kullanacağız
                else if (pi.Name.Length > 2 && pi.Name.Substring(0, 3) == "pk_" && ChangedProperties.Contains(pi.Name))
                {
                    orjPropName = pi.Name;
                    orjPropName = orjPropName.Remove(0, 3);
                    if (pi.GetValue(Nesne, null).IsNull())
                    {
                        Wheres += orjPropName + "=null AND ";

                        if (PKeyZorunlu)
                            throw new Exception("Primary Key alanlarının tamamı girilmeli !");
                    }
                    else
                    {
                        Wheres += orjPropName + "={" + sayac + "} AND ";
                        Sqlex.Parametreler.Add(pi.GetValue(Nesne, null));
                        sayac++;
                    }
                }
            }

            foreach (var item in SetList)
                Sets += " " + item;

            foreach (var item in WhereList)
                Wheres += " " + item;

            if (SetsEk != null) Sets += SetsEk;

            if (Sets.Length < 1)
                throw new Exception("Güncellenecek herhangi bir alan belirtilmemiş !");
            Sets = Sets.Remove(Sets.Length - 1, 1);
            if (Wheres.Length < 2)
                throw new Exception("Update sorgusu için where kısmında koşul belirtilmemiş !");
            Wheres = Wheres.Remove(Wheres.Length - 4, 4);

            Sqlex.SqlKomut = string.Format(Sqlex.SqlKomut, Tablo, Sets, Wheres);
            SqlExKomutDizisi.Add(Sqlex);
        }
    }

    public class SqlExperOperatorIslem
    {
        public static string SetAdd(string Property, params object[] Degerler)
        {
            var set = string.Empty;

            if (Degerler.Length == 0)
            {
                throw new Exception("SetAdd metodunda Degerler dizisine en az bir eleman girmek zorunludur !");
            }
            else if ((Degerler.Length % 2 == 0) || Degerler.Length > 7)
            {
                throw new Exception("SetAdd metodunda Degerler dizisi 2, 4, 6  gibi çift elemanlı ve 7 den büyük olamaz !");
            }
            else if (Degerler.Length == 1)
            {
                set = string.Format("{0}={1},", Property, Degerler[0].ObjectSetFormat());
            }
            else if (Degerler.Length == 3)
            {
                if (Degerler[1].GetType().Name == "SetIslem")
                {
                    set = string.Format("{0}={1}{2}{3},", Property, Degerler[0].ObjectSetFormat(),
                                                          Degerler[1].ObjectSetFormat(), Degerler[2].ObjectSetFormat());
                }
                else
                {
                    throw new Exception("SetAdd metodunda 3. parametre SetIslem tipinde enum olmak zorundadır !");
                }
            }
            else if (Degerler.Length == 5)
            {
                if (Degerler[1].GetType().Name == "SetIslem" && Degerler[3].GetType().Name == "SetIslem")
                {
                    set = string.Format("{0}={1}{2}{3}{4}{5},", Property, Degerler[0].ObjectSetFormat(), Degerler[1].ObjectSetFormat(),
                                                               Degerler[2].ObjectSetFormat(), Degerler[3].ObjectSetFormat(), Degerler[4].ObjectSetFormat());
                }
                else
                {
                    throw new Exception("SetAdd metodunda 3 ve 5. parametreler SetIslem tipinde enum olmak zorundadır !");
                }
            }
            else if (Degerler.Length == 7)
            {
                if (Degerler[1].GetType().Name == "SetIslem" && Degerler[3].GetType().Name == "SetIslem" && Degerler[5].GetType().Name == "SetIslem")
                {
                    set = string.Format("{0}={1}{2}{3}{4}{5}{6}{7},", Property, Degerler[0].ObjectSetFormat(), Degerler[1].ObjectSetFormat(),
                                                          Degerler[2].ObjectSetFormat(), Degerler[3].ObjectSetFormat(), Degerler[4].ObjectSetFormat(),
                                                          Degerler[5].ObjectSetFormat(), Degerler[6].ObjectSetFormat());
                }
                else
                {
                    throw new Exception("SetAdd metodunda 3, 5 ve 7 parametreler SetIslem tipinde enum olmak zorundadır !");
                }
            }

            return set;
        }

        public static string WhereAdd(params object[] Degerler)
        {
            var where = string.Empty;

            if (Degerler.Length < 3 || Degerler.Length > 9)
            {
                throw new Exception("WhereAdd metodunda en az 3 parametre en fazla 9 parametre girilebilir !");
            }
            else if (Degerler.Length == 3)
            {
                where = string.Format("{0}{1}{2} AND", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(), Degerler[2].ObjectWhereFormat());
            }
            else if (Degerler.Length == 4)
            {
                if (Degerler[3].ToString() == "AND" || Degerler[3].ToString() == "OR")
                    where = string.Format("{0}{1}{2} {3}", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(), Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat());
                else
                    where = string.Format(" {0}{1}{2}{3} AND", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(), Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat());
            }
            else if (Degerler.Length == 5)
            {
                if (Degerler[4].ToString() == "AND" || Degerler[4].ToString() == "OR")
                    where = string.Format("{0}{1}{2}{3} {4}", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(), Degerler[2].ObjectWhereFormat(),
                                                              Degerler[3].ObjectWhereFormat(), Degerler[4].ObjectWhereFormat());
                else
                    where = string.Format("{0}{1}{2}{3}{4} AND", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(), Degerler[2].ObjectWhereFormat(),
                                                                 Degerler[3].ObjectWhereFormat(), Degerler[4].ObjectWhereFormat());
            }
            else if (Degerler.Length == 6)
            {
                if (Degerler[5].ToString() == "AND" || Degerler[5].ToString() == "OR")
                    where = string.Format("{0}{1}{2}{3}{4} {5}", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                 Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(),
                                                                 Degerler[4].ObjectWhereFormat(), Degerler[5].ObjectWhereFormat());
                else
                    where = string.Format("{0}{1}{2}{3}{4}{5} AND", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                    Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(),
                                                                    Degerler[4].ObjectWhereFormat(), Degerler[5].ObjectWhereFormat());
            }
            else if (Degerler.Length == 7)
            {
                if (Degerler[6].ToString() == "AND" || Degerler[6].ToString() == "OR")
                    where = string.Format("{0}{1}{2}{3}{4}{5} {6}", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                    Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(), Degerler[4].ObjectWhereFormat(),
                                                                    Degerler[5].ObjectWhereFormat(), Degerler[6].ObjectWhereFormat());
                else
                    where = string.Format("{0}{1}{2}{3}{4}{5}{6} AND", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                       Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(),
                                                                       Degerler[4].ObjectWhereFormat(), Degerler[5].ObjectWhereFormat(), Degerler[6].ObjectWhereFormat());
            }
            else if (Degerler.Length == 8)
            {
                if (Degerler[7].ToString() == "AND" || Degerler[7].ToString() == "OR")
                    where = string.Format("{0}{1}{2}{3}{4}{5}{6} {7}", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                       Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(),
                                                                       Degerler[4].ObjectWhereFormat(), Degerler[5].ObjectWhereFormat(),
                                                                       Degerler[6].ObjectWhereFormat(), Degerler[7].ObjectWhereFormat());
                else
                    where = string.Format("{0}{1}{2}{3}{4}{5}{6}{7} AND", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                          Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(),
                                                                          Degerler[4].ObjectWhereFormat(), Degerler[5].ObjectWhereFormat(),
                                                                          Degerler[6].ObjectWhereFormat(), Degerler[7].ObjectWhereFormat());
            }
            else if (Degerler.Length == 9)
            {
                if (Degerler[8].ToString() == "AND" || Degerler[8].ToString() == "OR")
                    where = string.Format("{0}{1}{2}{3}{4}{5}{6}{7} {8}", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                          Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(),
                                                                          Degerler[4].ObjectWhereFormat(), Degerler[5].ObjectWhereFormat(),
                                                                          Degerler[6].ObjectWhereFormat(), Degerler[7].ObjectWhereFormat(), Degerler[8].ObjectWhereFormat());
                else
                    where = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8} AND", Degerler[0].ObjectWhereFormat(), Degerler[1].ObjectWhereFormat(),
                                                                             Degerler[2].ObjectWhereFormat(), Degerler[3].ObjectWhereFormat(),
                                                                             Degerler[4].ObjectWhereFormat(), Degerler[5].ObjectWhereFormat(),
                                                                             Degerler[6].ObjectWhereFormat(), Degerler[7].ObjectWhereFormat(), Degerler[8].ObjectWhereFormat());
            }

            return where;
        }

        public static string WhereAdd(string Property, object Deger, Operand And_Or = Operand.AND)
        {
            return WhereAdd(Property, Islem.Esit, Deger, And_Or);
        }

        public static string WhereAdd(string Property, Islem islem, object Deger, Operand And_Or = Operand.AND)
        {
            string where = string.Empty, islemString = string.Empty, AndOrString = string.Empty;
            switch (islem)
            {
                case Islem.EsitDegil:
                    islemString = "<>";
                    break;

                case Islem.Esit:
                    islemString = "=";
                    break;

                case Islem.Buyuk:
                    islemString = ">";
                    break;

                case Islem.BuyukEsit:
                    islemString = ">=";
                    break;

                case Islem.Kucuk:
                    islemString = "<";
                    break;

                case Islem.KucukEsit:
                    islemString = "<=";
                    break;

                case Islem.ileBaslayan:
                case Islem.ileBiten:
                case Islem.icindeGecen:
                    islemString = "Like";
                    break;

                default:
                    throw new Exception("Bu metodda işlem parametresi sadece karşılaştırma olabilir !");
            }

            switch (And_Or)
            {
                case Operand.AND:
                    AndOrString = "AND";
                    break;

                case Operand.OR:
                    AndOrString = " OR";
                    break;

                case Operand.IN:
                    AndOrString = "IN";
                    break;

                case Operand.NotIN:
                    AndOrString = "NOT IN";
                    break;

                case Operand.ParAc:
                    AndOrString = "(";
                    break;

                case Operand.ParKapa:
                    AndOrString = ")";
                    break;

                case Operand.NONE:
                    AndOrString = "";
                    break;
            }

            if (islem == Islem.ileBaslayan)
                where = string.Format("{0} {1} '%{2}' {3}", Property, islemString, Deger, AndOrString);
            else if (islem == Islem.ileBiten)
                where = string.Format("{0} {1} '{2}%' {3}", Property, islemString, Deger, AndOrString);
            else if (islem == Islem.ileBiten)
                where = string.Format("{0} {1} '%{2}%' {3}", Property, islemString, Deger, AndOrString);
            else
                where = string.Format("{0}{1}'{2}' {3}", Property, islemString, Deger, AndOrString);
            return where;
        }

        public static string WhereAdd(string Property, Operand In_NotIn, params object[] Degerler_AndOr)
        {
            string where = string.Empty, InNotInString = string.Empty;
            switch (In_NotIn)
            {
                case Operand.IN:
                    InNotInString = "IN";
                    break;

                case Operand.NotIN:
                    InNotInString = "NOT IN";
                    break;

                default:
                    throw new Exception("Bu metod sadece In ve Not In operatörleri için kullanılabilir !");
            }

            var degerler = string.Empty;
            var sonOperand = "AND";
            foreach (var item in Degerler_AndOr)
            {
                if (!item.GetType().IsEnum)
                    degerler += string.Format("'{0}',", item);
                else
                {
                    if (item.ToString2() == "AND" || item.ToString2() == "OR")
                    {
                        sonOperand = item.ToString2();
                    }
                    else
                    {
                        throw new Exception("Bu metotta yazılacak son ifade enum ise AND veya OR olmak zorunda ! ");
                    }
                }
            }

            if (sonOperand == "OR")
                sonOperand = " " + sonOperand;

            degerler = degerler.Remove(degerler.Length - 1, 1);
            where = string.Format("{0} {1}({2}) {3}", Property, InNotInString, degerler, sonOperand);
            return where;
        }
    }
}