using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WMSMobil
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
        public static string RemoveLastCharacter(this String instr, int number)
        {
            return instr.Substring(0, instr.Length - number);
        }
        /// <summary>
        /// Removes first n chars from a string
        /// </summary>
        public static string RemoveFirstCharacter(this String instr, int number)
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
        /// Hata olursa 0 döner.
        /// </summary>
        public static int ToInt32(this object Deger)
        {
            try { return Convert.ToInt32(Deger); }
            catch { return 0; }
        }


        /// <summary>
        /// <para>Gelen değeri Short (Int16) türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static short ToShort(this object Deger)
        {
            try { return Convert.ToInt16(Deger); }
            catch { return 0; }
        }


        /// <summary>
        /// <para>Gelen değeri Long (Int64) türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static long ToLong(this object Deger)
        {
            try { return Convert.ToInt64(Deger); }
            catch { return 0; }
        }

        /// <summary>
        /// <para>Gelen değeri Double türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static double ToDouble(this object Deger)
        {
            try { return Convert.ToDouble(Deger); }
            catch { return 0.0; }
        }

        /// <summary>
        /// <para>Gelen değeri Float türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static float ToFloat(this object Deger)
        {
            try { return Convert.ToSingle(Deger); }
            catch { return 0.0f; }
        }
        /// <summary>
        /// <para>Gelen değeri Decimal türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static decimal ToDecimal(this object Deger)
        {
            try { return Convert.ToDecimal(Deger); }
            catch { return 0.0M; }
        }
        /// <summary>
        /// <para>Gelen değeri Char türüne dönüştürür.</para>
        /// Hata olursa defaultValue parametresi döner.
        /// </summary>
        public static char ToChar(this object Deger)
        {
            try { return Convert.ToChar(Deger); }
            catch { return ' '; }
        }
        /// <summary>
        /// <para>Gelen değeri DateTime türüne dönüştürür.</para>
        /// <para> Hata olursa "01.01.1970" değeri döner.</para>
        /// format değerini 1 gönderirseniz saat kısmını o anki saat olarak set eder.
        /// </summary>
        public static DateTime ToDatetime(this object Deger)
        {
            DateTime mDeger;
            try
            {

                mDeger = DateTime.Parse(Deger.ToString());

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
        public static DateTime? ToDatetimeNull(this object Deger)
        {
            DateTime? mDeger = null;
            try
            {
                mDeger = DateTime.Parse(Deger.ToString());
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
        public static bool ToBool(this object Deger)
        {
            try { return Convert.ToBoolean(Deger); }
            catch { return false; }
        }
        /// <summary>
        /// <para>Gelen değeri String türüne dönüştürür.</para>
        /// Trimle boşluklar atılır. Hata olursa defaultValue döner.
        /// </summary>
        public static string ToString2(this object Deger)
        {
            try { return Convert.ToString(Deger).Trim(); }
            catch { return ""; }
        }
        /// <summary>
        /// <para>Gelen değeri string olarak alır ve ifadeyi ters çevirir.</para>
        /// Hata olursa defaultValue döner.
        /// </summary>
        public static string ToReverse(this string Deger)
        {
            try
            {
                char[] dizi = Deger.ToCharArray();
                Array.Reverse(dizi);
                return new string(dizi);
            }
            catch { return "?"; }
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
        /// Int tipindeki değerleri DateTime tipine dönüştürür.
        /// </summary>
        public static DateTime IntToDateTime(this int Deger)
        {
            DateTime date = new DateTime(1899, 12, 30);
            date = date.AddDays(Deger);
            return date;
        }
        /// <summary>
        /// DateTime tipindeki saat kısmını alıp int olarak saat değeri üretir.
        /// </summary>
        public static int TimeInt(this DateTime DateSaat)
        {
            int saat = 0;
            if (DateSaat.Hour >= 1)
            {
                saat += DateSaat.Hour * 60 * 60;
            }
            if (DateSaat.Minute >= 1)
            {
                saat += DateSaat.Minute * 60;
            }
            if (DateSaat.Second > 0)
            {
                saat += DateSaat.Second;
            }
            return saat;
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
        /// Genellikle decimal veya float tiplerde database tarafına kayıt
        /// atarken ondalık kısmı "," olarak gördüğünden hata verir. 
        /// Bunu önlemek için ToDot() extension metodu kullanılır.
        /// </summary>
        public static object ToDot(this object value)
        {
            return value.ToString().Replace(',', '.');
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
        /// <summary>
        /// Aynı tipteki nesnenin değerlerini kendine set eder 
        /// <para>Kullanım şekli : nesne1.Set(nesne2);  nesne2 nin değerleri nesne1'e atandı. </para>
        /// </summary>
        public static void Set(this object obj, object deger, params string[] istisnalar)
        {

            foreach (PropertyInfo pi in deger.GetType().GetProperties())
            {
                if (istisnalar != null)
                {
                    if (istisnalar.Contains(pi.Name))
                        continue;
                }
                try
                {
                    deger.GetType().GetProperty(pi.Name).SetValue(obj, pi.GetValue(deger, null), null);
                }
                catch { }
            }
        }
    }
}
