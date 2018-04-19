using Birikim.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Wms12m
{
    public static class Extensions
    {
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
        /// türkçe karakterleri sil
        /// </summary>
        public static string ClearNonAscii(this string value)
        {
            value = value.Replace("Ç", "C").Replace("Ş", "S").Replace("Ö", "O").Replace("Ü", "U").Replace("İ", "I").Replace("Ğ", "G");
            value = value.Replace("ç", "c").Replace("ş", "s").Replace("ö", "o").Replace("ü", "u").Replace("ı", "i").Replace("ğ", "g");
            return value;
        }

        /// <summary>
        /// sembolleri sil
        /// </summary>
        public static string ClearSymbols(this string value)
        {
            return value.Replace(",", "").Replace(".", "").Replace("-", "").Replace(" ", "");
        }

        /// <summary>
        /// CryptographyExtension.Cozumle
        /// </summary>
        public static string Cozumle(this object value)
        {
            if (value == null)
                return "";
            return CryptographyExtension.Cozumle(value.ToString());
        }

        /// <summary>
        /// Propertisi olan nesnelerin propertisini default değerler verir.
        /// </summary>
        /// <param name="istisnalar">Default değeri set edilmeyecek propertyleri belirtmek gerekiyor.</param>
        public static void DefaultValueSet(this object value, params string[] istisnalar)
        {
            foreach (var pi in value.GetType().GetProperties())
            {
                try
                {
                    if (istisnalar.Contains(pi.Name)) continue;

                    if (!pi.CanWrite) continue;

                    if (pi.PropertyType.Namespace == "System.Collections.Generic")
                        continue;

                    if (!pi.PropertyType.IsGenericType)
                    {
                        if (pi.PropertyType == typeof(string))
                            pi.SetValue(value, "", null);
                        else if (pi.PropertyType == typeof(decimal))
                            pi.SetValue(value, 0.0m, null);
                        else if (pi.PropertyType == typeof(int) ||
                                 pi.PropertyType == typeof(short) ||
                                 pi.PropertyType == typeof(float) ||
                                 pi.PropertyType == typeof(double) ||
                                 pi.PropertyType == typeof(byte))
                            pi.SetValue(value, (short)0, null);
                    }
                    else
                    {
                        if (pi.PropertyType.GetGenericArguments()[0] == typeof(string))
                            pi.SetValue(value, "", null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(decimal))
                            pi.SetValue(value, 0.0m, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(int) ||
                                 pi.PropertyType.GetGenericArguments()[0] == typeof(double))
                            pi.SetValue(value, 0, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(float))
                            pi.SetValue(value, 0.0f, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(short))
                            pi.SetValue(value, (short)0, null);
                        else if (pi.PropertyType.GetGenericArguments()[0] == typeof(byte))
                            pi.SetValue(value, (byte)0, null);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary>
        /// Checks file exists
        /// </summary>
        public static bool FileExists(this string filePathName) => File.Exists(filePathName);

        /// <summary>
        /// Get the file size of a given filename.
        /// </summary>
        public static long FileSize(this string filePath)
        {
            long bytes = 0;

            try
            {
                var oFileInfo = new FileInfo(filePath);
                bytes = oFileInfo.Length;
            }
            catch { }
            return bytes;
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
        /// Nicely formatted file size. This method will return file size with bytes, KB, MB and GB in it. You can use this alongside the Extension method named FileSize.
        /// </summary>
        public static string FormatFileSize(this int fileSize)
        {
            //declarations
            string[] suffix = { "B", "KB", "MB", "GB", "TB" };
            var j = 0;

            //loop and divide
            while (fileSize > 1024 && j < (suffix.Length - 1))
            {
                fileSize = fileSize / 1024;
                j++;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return string.Format("{0:0} {1}", fileSize, suffix[j]);
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
        /// <para>Gelen Int formatındaki tarihi normal formata dönüştürür.</para>
        /// </summary>
        public static DateTime FromOaDate(this int value)
        {
            try
            {
                return DateTime.FromOADate(value).Date;
            }
            catch (Exception)
            {
                return DateTime.Today.Date;
            }
        }

        public static string FromOADateInt(this int value)
        {
            try { return DateTime.FromOADate(value).ToString("dd.MM.yyyy"); }
            catch { return ""; }
        }

        public static string FromOaTime(this int value)
        {
            try
            {
                var now = new DateTime(2000, 1, 1);
                return now.AddSeconds(value).TimeOfDay.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType) return t;
                else return Nullable.GetUnderlyingType(t);
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// Gelen değer NULL ise boşluk değilse aynı değeri döndürür
        /// </summary>
        public static object IfNullGetValue(this object value)
        {
            if (value == DBNull.Value || value == null)
            {
                return "";
            }
            else
                return value;
        }

        /// <summary>
        /// Int tipindeki değerleri DateTime tipine dönüştürür.
        /// </summary>
        public static DateTime IntToDate(this int value)
        {
            var date = new DateTime(1899, 12, 30);
            date = date.AddDays(value);
            return date;
        }

        /// <summary>
        /// Checks whether the type is Boolean
        /// </summary>
        public static bool IsBoolean(this Type type) => type.Equals(typeof(bool));

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
        /// iki nesnenin birbiryle aynı olup olmadığını kontrol ediyor
        /// </summary>
        public static bool IsDifferent(this object nesne, object nesne2)
        {
            foreach (PropertyInfo pthis in nesne.GetType().GetProperties())
            {
                if (pthis.GetValue(nesne, null).ToString2() != nesne2.GetType().GetProperty(pthis.Name)?.GetValue(nesne2, null).ToString2())
                {
                    return true;
                }
            }

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

        public static bool IsNotNullEmpty(this object value)
        {
            if (value == null) return false;
            if (value == DBNull.Value) return false;
            if (value.ToString().Trim() == string.Empty) return false;
            return true;
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

        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
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

        /// <summary>
        /// Checks if a string value is numeric according to you system culture.
        /// </summary>
        public static bool IsNumeric(this string theValue)
        {
            return long.TryParse(theValue, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out _);
        }

        /// <summary>
        /// Check wheter a string is an valid e-mail address
        /// </summary>
        public static bool IsValidEmailAddress(this string s)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
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
            return value.Substring(start, length);
        }

        /// <summary>
        /// Removes first n chars from a string
        /// </summary>
        public static string RemoveFromStart(this string instr, int number = 1)
        {
            return instr.Substring(number);
        }

        /// <summary>
        /// Removes last n chars from a string
        /// </summary>
        public static string RemoveFromEnd(this string instr, int number = 1)
        {
            return instr.Substring(0, instr.Length - number);
        }

        /// <summary>
        /// Returns characters from right of specified length
        /// </summary>
        public static string Right(this string value, int length)
        {
            return value != null && value.Length > length ? value.Substring(value.Length - length) : value;
        }

        /// <summary>
        /// CryptographyExtension.Sifrele
        /// </summary>
        public static string Sifrele(this object value)
        {
            return CryptographyExtension.Sifrele(value.ToString());
        }

        /// <summary>
        /// <para>Gelen değeri Bool türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static bool ToBool(this object value, bool defaultValue = false)
        {
            try { return Convert.ToBoolean(value); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// <para>Gelen değeri Char türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static char ToChar(this object value, char defaultValue = ' ')
        {
            try { return Convert.ToChar(value); }
            catch { return defaultValue; }
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
                var t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// IEnumerable tipleri DataTable'a dönüştürür.  Örnek List<Class> => DataTable
        /// </summary>
        public static DataTable ToDataTableV2<T>(this IEnumerable<T> collection)
        {
            var newDataTable = new DataTable();
            var impliedType = typeof(T);
            PropertyInfo[] propInfo = impliedType.GetProperties();
            foreach (PropertyInfo pi in propInfo)
            {
                if (pi.Name.Substring(0, 1) == "_") continue;

                if (!pi.PropertyType.IsGenericType)
                    newDataTable.Columns.Add(pi.Name, pi.PropertyType);
                else
                    newDataTable.Columns.Add(pi.Name, pi.PropertyType.GetGenericArguments()[0]);
            }

            foreach (T item in collection)
            {
                var newDataRow = newDataTable.NewRow();
                newDataRow.BeginEdit();
                foreach (PropertyInfo pi in propInfo)
                {
                    if (pi.Name.Substring(0, 1) == "_") continue;

                    try
                    {
                        if (pi.GetValue(item, null).IsNotNull())
                            newDataRow[pi.Name] = pi.GetValue(item, null);
                        else
                            newDataRow[pi.Name] = DBNull.Value;
                    }
                    catch
                    {
                        // ignored
                    }
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

            var output = new DataTable();

            foreach (var prop in properties)
                output.Columns.Add(prop.Name, prop.PropertyType);

            foreach (var item in source)
            {
                var row = output.NewRow();

                foreach (var prop in properties)
                    row[prop.Name] = prop.GetValue(item, null);

                output.Rows.Add(row);
            }

            return output;
        }

        /// <summary>
        /// <para>Gelen değeri DateTime türüne dönüştürür.</para>
        /// <para> Hata olursa "01.01.1970" değeri döner.</para>
        /// format değerini 1 gönderirseniz saat kısmını o anki saat olarak set eder.
        /// </summary>
        public static DateTime ToDatetime(this object value, int format = 0)
        {
            DateTime mDeger;
            try
            {
                if (format == 1)
                {
                    mDeger = DateTime.Parse(value.ToString());
                    mDeger = new DateTime(mDeger.Year, mDeger.Month, mDeger.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                }
                else
                {
                    mDeger = DateTime.Parse(value.ToString());
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
        public static DateTime? ToDatetimeNull(this object value, int format = 0)
        {
            DateTime? mDeger;
            try
            {
                if (format == 1)
                {
                    mDeger = DateTime.Parse(value.ToString());
                    mDeger = new DateTime(mDeger.Value.Year, mDeger.Value.Month, mDeger.Value.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                }
                else
                {
                    mDeger = DateTime.Parse(value.ToString());
                }
            }
            catch
            {
                mDeger = null;
            }

            return mDeger;
        }

        /// <summary>
        /// <para>Gelen değeri Decimal türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static decimal ToDecimal(this object value, decimal defaultValue = 0.0M)
        {
            try { return Convert.ToDecimal(value); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// Genellikle decimal veya float tiplerde database tarafına kayıt
        /// atarken ondalık kısmı "," olarak gördüğünden hata verir.
        /// Bunu önlemek için ToDot() extension metodu kullanılır.
        /// </summary>
        public static string ToDot(this object value) => value.ToString().Replace(',', '.');

        /// <summary>
        /// <para>Gelen değeri Double türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static double ToDouble(this object value, double defaultValue = 0.0)
        {
            try { return Convert.ToDouble(value); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// <para>Gelen değeri Float türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static float ToFloat(this object value, float defaultValue = 0.0f)
        {
            try { return Convert.ToSingle(value); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// <para>Gelen değeri Int32 türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static int ToInt32(this object value, int defaultValue = 0)
        {
            try { return Convert.ToInt32(value); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// <para>Gelen değeri Long (Int64) türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static long ToLong(this object value, long defaultValue = 0)
        {
            try { return Convert.ToInt64(value); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// <para>Gelen tarihi Int32 türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static int ToOADateInt(this DateTime value, int defaultValue = 0)
        {
            try { return Convert.ToInt32(value.ToOADate()); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// DateTime tipindeki saat kısmını alıp int olarak saat değeri üretir.
        /// </summary>
        public static int ToOaTime(this DateTime value)
        {
            return ((value.Hour * 60 * 60) + (value.Minute * 60) + value.Second);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            return new ObservableCollection<T>(coll);
        }

        /// <summary>
        /// custom ondalık format
        /// </summary>
        public static string ToOnFormat(this decimal value)
        {
            string[] dizi = value.ToString().Split(',');
            string sdeger;

            if (dizi.Length > 1)
                sdeger = string.Format(string.Format("{{0:n{0}}}", dizi[1].Length), value);
            else
                sdeger = string.Format("{0:n0}", value);

            return sdeger;
        }

        /// <summary>
        /// <para>Gelen değeri string olarak alır ve ifadeyi ters çevirir.</para>
        /// Hata olursa defaultValue döner.
        /// </summary>
        public static string ToReverse(this string value, string defaultValue = "?")
        {
            try
            {
                char[] dizi = value.ToCharArray();
                Array.Reverse(dizi);
                return new string(dizi);
            }
            catch { return defaultValue; }
        }

        /// <summary>
        /// <para>Gelen değeri Short (Int16) türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static short ToShort(this object value, short defaultValue = 0)
        {
            try { return Convert.ToInt16(value); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// <para>Gelen değeri String türüne dönüştürür.</para>
        /// Trimle boşluklar atılır. Hata olursa defaultValue döner.
        /// </summary>
        public static string ToString2(this object value, string defaultValue = "")
        {
            try { return Convert.ToString(value).Trim(); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// clean url
        /// </summary>
        public static string ToURL(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            s = s.Trim();
            if (s.Length > 80) s = s.Substring(0, 80);
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
            var r = new Regex("[^a-zA-Z0-9_-]");

            s = r.Replace(s, "-");
            if (!string.IsNullOrEmpty(s))
                while (s.IndexOf("--") > -1)
                    s = s.Replace("--", "-");
            if (s.StartsWith("-")) s = s.Substring(1);
            if (s.EndsWith("-")) s = s.Substring(0, s.Length - 1);
            return s;
        }
        /// <summary>
        /// Binlik ayracı
        /// </summary>
        public static string ToBinlikAyrac(this decimal value, string defaultvalue = "0,00")
        {
            try { return value.ToString("N", CultureInfo.GetCultureInfo("tr-TR")); }
            catch { return defaultvalue; }
        }
    }
}