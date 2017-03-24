using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Wms12m
{
    public static class Extensions
    {
        /// <summary>
        /// sembolleri sil
        /// </summary>
        public static string ClearSymbols(this string value)
        {
            return value.Replace(",", "").Replace(".", "").Replace("-", "").Replace(" ", "");
        }
        /// <summary>
        /// türkçe karakterleri sil
        /// </summary>
        public static string ClearNonAscii(this string value)
        {
            value = value.Replace("Ç", "C").Replace("Ş", "S").Replace("Ö", "O").Replace("Ü", "U").Replace("İ", "I").Replace("Ğ", "G");
            value = value.Replace("ç", "c").Replace("ş", "s").Replace("ö", "o").Replace("ü", "u").Replace("ı", "i").Replace("ğ", "g");
            return value;
        }
        /// <summary>
        /// Returns characters from right of specified length
        /// </summary>
        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }
        /// <summary>
        /// Returns characters from left of specified length
        /// </summary>
        public static string Left(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(0, length) : value;
        }
        /// <summary>
        /// Returns characters from left of specified length
        /// </summary>
        public static string Mid(this string value, int start, int length)
        {
            if (value.Length < (length + start)) length = value.Length - start;
            if (length < 0) return value;
            return value != null ? value.Substring(start, length) : value;
        }
        /// <summary>
        /// Removes last n chars from a string
        /// </summary>
        public static string RemoveLastCharacter(this String instr, int number = 1)
        {
            return instr.Substring(0, instr.Length - number);
        }
        /// <summary>
        /// Removes first n chars from a string
        /// </summary>
        public static string RemoveFirstCharacter(this String instr, int number = 1)
        {
            return instr.Substring(number);
        }
        /// <summary>
        /// Formats the string according to the specified mask
        /// </summary>
        public static string FormatWithMask(this string input, string mask)
        {
            if (input == null) return input;
            var output = string.Empty;
            var index = 0;
            foreach (var m in mask)
            {
                if (m == '#')
                {
                    if (index < input.Length)
                    {
                        output += input[index];
                        index++;
                    }
                }
                else
                    output += m;
            }
            return output;
        }
        /// <summary>
        /// Checks if a string value is numeric according to you system culture.
        /// </summary>
        public static bool IsNumeric(this string theValue)
        {
            long retNum;
            return long.TryParse(theValue, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }
        /// <summary>
        /// Checks whether the type is Boolean
        /// </summary>
        public static bool IsBoolean(this Type type)
        {
            return type.Equals(typeof(Boolean));
        }
        /// <summary>
        /// Check wheter a string is an valid e-mail address
        /// </summary>
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
        /// <summary>
        /// Shortcut for foreach
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
        /// <summary>
        /// Wraps DateTime.TryParse() and all the other kinds of code you need to determine if a given string holds a value that can be converted into a DateTime object.
        /// </summary>
        public static bool IsDate(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                DateTime dt;
                return (DateTime.TryParse(input, out dt));
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// clean html codes
        /// </summary>
        public static string CleanHtmlCodes(this string s)
        {
            s = s.Replace("<", "&amp;lt;");
            s = s.Replace(">", "&amp;gt;");
            s = s.Replace("script", "scr_ipt");
            s = s.Replace("'", "'");
            s = s.Replace("\"", "'");
            s = s.Replace("&amp;", "-");

            s = s.Trim();
            return s;
        }
        /// <summary>
        /// clean url
        /// </summary>
        public static string ToURL(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = s.Trim();
            if (s.Length > 80)
                s = s.Substring(0, 80);
            s = s.Replace("ş", "s");
            s = s.Replace("Ş", "S");
            s = s.Replace("ğ", "g");
            s = s.Replace("Ğ", "G");
            s = s.Replace("İ", "I");
            s = s.Replace("ı", "i");
            s = s.Replace("ç", "c");
            s = s.Replace("Ç", "C");
            s = s.Replace("ö", "o");
            s = s.Replace("Ö", "O");
            s = s.Replace("ü", "u");
            s = s.Replace("Ü", "U");
            s = s.Replace("'", "");
            s = s.Replace("\"", "");
            s = s.Replace("-", "");
            s = s.Replace("'", "");
            Regex r = new Regex("[^a-zA-Z0-9_-]");

            s = r.Replace(s, "-");
            if (!string.IsNullOrEmpty(s))
                while (s.IndexOf("--") > -1)
                    s = s.Replace("--", "-");
            if (s.StartsWith("-")) s = s.Substring(1);
            if (s.EndsWith("-")) s = s.Substring(0, s.Length - 1);
            return s;
        }
        /// <summary>
        /// <para>Gelen değeri Int32 türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static int ToInt32(this object Deger, int defaultValue = 0)
        {
            try { return Convert.ToInt32(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen tarihi Int32 türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static int ToOADateInt(this DateTime Deger, int defaultValue = 0)
        {
            try { return Convert.ToInt32(Deger.ToOADate()); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// Nicely formatted file size. This method will return file size with bytes, KB, MB and GB in it. You can use this alongside the Extension method named FileSize.
        /// </summary>
        public static string FormatFileSize(this long fileSize)
        {
            string[] suffix = { "bytes", "KB", "MB", "GB" };
            long j = 0;

            while (fileSize > 1024 && j < 4)
            {
                fileSize = fileSize / 1024;
                j++;
            }
            return (fileSize + " " + suffix[j]);
        }
        /// <summary>
        /// Get the file size of a given filename.
        /// </summary>
        public static long FileSize(this string filePath)
        {
            long bytes = 0;

            try
            {
                System.IO.FileInfo oFileInfo = new System.IO.FileInfo(filePath);
                bytes = oFileInfo.Length;
            }
            catch { }
            return bytes;
        }
        /// <summary>
        /// <para>Gelen Int formatındaki tarihi normal formata dönüştürür.</para>
        /// </summary>
        public static string FromOADateInt(this int Deger)
        {
            try { return DateTime.FromOADate(Deger).ToShortDateString(); }
            catch { return ""; }
        }
        /// <summary>
        /// <para>Gelen değeri Short (Int16) türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static short ToShort(this object Deger, short defaultValue = 0)
        {
            try { return Convert.ToInt16(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen değeri Long (Int64) türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static long ToLong(this object Deger, long defaultValue = 0)
        {
            try { return Convert.ToInt64(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen değeri Double türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static double ToDouble(this object Deger, double defaultValue = 0.0)
        {
            try { return Convert.ToDouble(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen değeri Float türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static float ToFloat(this object Deger, float defaultValue = 0.0f)
        {
            try { return Convert.ToSingle(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen değeri Decimal türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static decimal ToDecimal(this object Deger, decimal defaultValue = 0.0M)
        {
            try { return Convert.ToDecimal(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen değeri Char türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static char ToChar(this object Deger, char defaultValue = ' ')
        {
            try { return Convert.ToChar(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen değeri DateTime türüne dönüştürür.</para>
        /// <para> Hata olursa "01.01.1970" değeri döner.</para>
        /// format değerini 1 gönderirseniz saat kısmını o anki saat olarak set eder.
        /// </summary>
        public static DateTime ToDatetime(this object Deger, int format = 0)
        {
            DateTime mDeger;
            try
            {
                if (format == 1)
                {
                    mDeger = DateTime.Parse(Deger.ToString());
                    mDeger = new DateTime(mDeger.Year, mDeger.Month, mDeger.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                }
                else
                {
                    mDeger = DateTime.Parse(Deger.ToString());
                }
            }
            catch
            {
                mDeger = new DateTime(1970, 1, 1);
            }
            return mDeger;
        }
        /// <summary>
        /// <para>Gelen değeri DateTime türüne dönüştürür.</para>
        /// <para> Hata olursa NULL değeri döner.</para>
        /// format değerini 1 gönderirseniz saat kısmını o anki saat olarak set eder.
        /// </summary>
        public static DateTime? ToDatetimeNull(this object Deger, int format = 0)
        {
            DateTime? mDeger = null;
            try
            {
                if (format == 1)
                {
                    mDeger = DateTime.Parse(Deger.ToString());
                    mDeger = new DateTime(mDeger.Value.Year, mDeger.Value.Month, mDeger.Value.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                }
                else
                {
                    mDeger = DateTime.Parse(Deger.ToString());
                }
            }
            catch
            {
                mDeger = null;
            }
            return mDeger;
        }
        /// <summary>
        /// <para>Gelen değeri Bool türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static bool ToBool(this object Deger, bool defaultValue = false)
        {
            try { return Convert.ToBoolean(Deger); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// <para>Gelen değeri String türüne dönüştürür.</para>
        /// Trimle boşluklar atılır. Hata olursa defaultValue döner.
        /// </summary>
        public static string ToString2(this object Deger, string defaultValue = "")
        {
            try { return Convert.ToString(Deger).Trim(); }
            catch { return defaultValue; }
        }
        /// <summary>
        /// Int tipindeki değerleri DateTime tipine dönüştürür.
        /// </summary>
        public static DateTime IntToDate(this int Deger)
        {
            DateTime date = new DateTime(1899, 12, 30);
            date = date.AddDays(Deger);
            return date;
        }
        /// <summary>
        /// DateTime tipindeki saat kısmını alıp int olarak saat değeri üretir.
        /// </summary>
        public static int SaatiAl(this DateTime Deger)
        {
            return Deger.Hour * 60 * 60 + Deger.Minute * 60 + Deger.Second;
        }
        public static string SaatiGetir(this int Deger)
        {
            try
            {
                var now = new DateTime(2000, 1, 1);
                return now.AddSeconds(Deger).TimeOfDay.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// iki nesnenin birbiryle aynı olup olmadığını kontrol ediyor
        /// </summary>
        public static bool IsDifferent(this object Nesne, object Nesne2)
        {
            foreach (PropertyInfo pthis in Nesne.GetType().GetProperties())
            {
                if (pthis.GetValue(Nesne, null).ToString2() != Nesne2.GetType().GetProperty(pthis.Name).GetValue(Nesne2, null).ToString2())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// IEnumerable tipleri Datatable'a hızlı dönüştürür.
        /// </summary>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
            return tb;
        }
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        /// <summary>
        /// IEnumerable tipleri DataTable'a dönüştürür.  Örnek List<Class> => DataTable
        /// </summary>
        public static DataTable ToDataTableV2<T>(this IEnumerable<T> collection)
        {
            DataTable newDataTable = new DataTable();
            Type impliedType = typeof(T);
            PropertyInfo[] _propInfo = impliedType.GetProperties();
            foreach (PropertyInfo pi in _propInfo)
            {
                if (pi.Name.Substring(0, 1) == "_")
                    continue;

                if (!pi.PropertyType.IsGenericType)
                    newDataTable.Columns.Add(pi.Name, pi.PropertyType);
                else
                    newDataTable.Columns.Add(pi.Name, pi.PropertyType.GetGenericArguments()[0]);
            }

            foreach (T item in collection)
            {
                DataRow newDataRow = newDataTable.NewRow();
                newDataRow.BeginEdit();
                foreach (PropertyInfo pi in _propInfo)
                {
                    if (pi.Name.Substring(0, 1) == "_")
                        continue;

                    try
                    {
                        if (pi.GetValue(item, null).IsNotNull())
                            newDataRow[pi.Name] = pi.GetValue(item, null);
                        else
                            newDataRow[pi.Name] = DBNull.Value;
                    }
                    catch
                    { }
                }
                newDataRow.EndEdit();
                newDataTable.Rows.Add(newDataRow);
            }

            newDataTable.AcceptChanges();
            return newDataTable;
        }
        public static DataTable ToDataTableV3<T>(this IEnumerable<T> source)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            DataTable output = new DataTable();

            foreach (var prop in properties)
            {
                output.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in source)
            {
                DataRow row = output.NewRow();

                foreach (var prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item, null);
                }

                output.Rows.Add(row);
            }

            return output;
        }
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> Coll)
        {
            return new ObservableCollection<T>(Coll);
        }
        /// <summary>
        /// Null ve DBNULL kontrolü yapar 
        /// </summary>
        public static bool IsNull(this object value)
        {
            if (value == DBNull.Value || value == null)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Null ve DBNULL değilse true döner
        /// </summary>
        public static bool IsNotNull(this object value)
        {
            if (value == DBNull.Value || value == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Null ve DBNULL değilse true döner
        /// </summary>
        public static bool IsNullEmpty(this object value)
        {
            if (value == null) return true;
            if (value == DBNull.Value) return true;
            if (value.ToString().Trim() == string.Empty) return true;
            return false;
        }
        public static bool IsNotNullEmpty(this object value)
        {
            if (value == null) return false;
            if (value == DBNull.Value) return false;
            if (value.ToString().Trim() == string.Empty) return false;
            return true;
        }
        /// <summary>
        /// <para>Gelen değeri string olarak alır ve ifadeyi ters çevirir.</para>
        /// Hata olursa defaultValue döner.
        /// </summary>
        public static string ToReverse(this string Deger, string defaultValue = "?")
        {
            try
            {
                char[] dizi = Deger.ToCharArray();
                Array.Reverse(dizi);
                return new string(dizi);
            }
            catch { return defaultValue; }
        }
        /// <summary>
        /// Gelen değer NULL ise boşluk değilse aynı değeri döndürür
        /// </summary>
        public static object IfNullGetValue(this object Deger)
        {
            if (Deger == DBNull.Value || Deger == null)
            {
                return "";
            }
            else
                return Deger;
        }
        /// <summary>
        /// ? operatöründe kullanılan karşılaştırmayı yapar. 
        /// İlk parametre hangi değerle karşılaştırıldığını gösterir.
        /// Eğer sonuc true ise ikinci parametre değilse üçüncü parametre döner.
        /// </summary>
        public static string IfElse(this object Deger, object EsitKosulu, string Esitse, string Degilse)
        {
            if (Deger.Equals(EsitKosulu))
                return Esitse;
            else
                return Degilse;
        }
        /// <summary>
        /// Propertisi olan nesnelerin propertisini default değerler verir.
        /// </summary>
        /// <param name="Istisnalar">Default değeri set edilmeyecek propertyleri belirtmek gerekiyor.</param>
        public static void DefaultValueSet(this object Deger, params string[] Istisnalar)
        {

            foreach (var pi in Deger.GetType().GetProperties())
            {
                try
                {
                    if (Istisnalar.Contains(pi.Name))
                        continue;

                    if (!pi.CanWrite)
                        continue;

                    if (pi.PropertyType.Namespace == "System.Collections.Generic")
                        continue;

                    if (!pi.PropertyType.IsGenericType)
                    {
                        if (pi.PropertyType == typeof(string))
                            pi.SetValue(Deger, "", null);
                        else if (pi.PropertyType == typeof(decimal))
                            pi.SetValue(Deger, 0.0m, null);
                        else if (pi.PropertyType == typeof(int) ||
                                 pi.PropertyType == typeof(short) ||
                                 pi.PropertyType == typeof(Single) ||
                                 pi.PropertyType == typeof(double) ||
                                 pi.PropertyType == typeof(byte))
                            pi.SetValue(Deger, (short)0, null);
                    }
                    else
                    {
                        if (pi.PropertyType.GetGenericArguments()[0] == typeof(string))
                            pi.SetValue(Deger, "", null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(decimal))
                            pi.SetValue(Deger, 0.0m, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(int) ||
                                 pi.PropertyType.GetGenericArguments()[0] == typeof(double))
                            pi.SetValue(Deger, 0, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(Single))
                            pi.SetValue(Deger, 0.0f, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(short))
                            pi.SetValue(Deger, (short)0, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(byte))
                            pi.SetValue(Deger, (byte)0, null);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
