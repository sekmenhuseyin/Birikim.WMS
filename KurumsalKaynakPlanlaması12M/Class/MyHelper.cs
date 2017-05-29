
using System;
using System.Collections.Generic;
namespace KurumsalKaynakPlanlaması12M
{
    static class MyHelper
    {
        public static string EvrakNoArtir(string evrakNo)
        {
            if (string.IsNullOrEmpty(evrakNo))
                return null;
            int ind = evrakNo.Length;
            int seri = 0;
            bool arttırılabilir = false;
            for (int i = ind; i > 0; i--)
            {
                ind = i;
                if (evrakNo.Substring(evrakNo.Length - ind).Contains("-"))
                    continue;
                if (evrakNo.Substring(evrakNo.Length - ind).Contains(" "))
                    continue;
                if (int.TryParse(evrakNo.Substring(evrakNo.Length - ind), out seri))
                {
                    arttırılabilir = true;
                    break;
                }
            }
            if (arttırılabilir == false)
                return null;
            seri++;
            if (seri.ToString().Length > ind)
                return null;

            string sifir = "";
            while (sifir.Length + seri.ToString().Length != ind)
            {
                sifir += "0";
            }

            evrakNo = evrakNo.Substring(0, evrakNo.Length - ind) + sifir + seri.ToString();
            return evrakNo;
        }

        public static int SaatForDB(DateTime Saatim)
        {
            int saat = 0;
            if (Saatim.Hour >= 1)
            {
                saat += Saatim.Hour * 60 * 60;
            }
            if (Saatim.Minute >= 1)
            {
                saat += Saatim.Minute * 60;
            }
            if (Saatim.Second > 0)
            {
                saat += Saatim.Second;
            }
            return saat;
        }

        public static DateTime intToSaat(int saatint)
        {
            int saat = 0, dakika = 0, saniye = 0;

            saat = saatint / 3600;
            int mod = saatint % 3600;

            dakika = mod / 60;
            saniye = mod % 60;

            return DateTime.Today.AddHours(saat).AddMinutes(dakika).AddSeconds(saniye);
        }

        public static DateTime intToSaat(DateTime tarih, int saatint)
        {
            int saat = 0, dakika = 0, saniye = 0;

            saat = saatint / 3600;
            int mod = saatint % 3600;

            dakika = mod / 60;
            saniye = mod % 60;

            return tarih.Date.AddHours(saat).AddMinutes(dakika).AddSeconds(saniye);
        }

        public static string GetCaption(string str)
        {
            List<char> list = new List<char>();
            int index = 0;
            foreach (char c in str)
            {
                if (char.IsUpper(c))
                {
                    if (list.Count > 0)
                    {
                        bool oncesibuyuk = char.IsUpper(str[index - 1]);
                        bool sonrasibuyukveyaRakam = false;
                        if (index < (str.Length - 1))
                        {
                            sonrasibuyukveyaRakam = char.IsUpper(str[index + 1]);
                            if (!sonrasibuyukveyaRakam)
                                sonrasibuyukveyaRakam = char.IsNumber(str[index + 1]);
                        }

                        if (index == (str.Length - 1))
                            sonrasibuyukveyaRakam = true;

                        if (!oncesibuyuk || !sonrasibuyukveyaRakam)
                            list.Add(' ');
                    }
                    list.Add(c);
                }
                else
                {
                    list.Add(c);
                }
                index++;
            }

            string st = new string(list.ToArray());
            return st;
        }



        /// <summary>
        /// string bir liste gönderip sql cümlesinde kullanacağımız hale getirir. Örnek new List<string>{"10001.00, 10002.01, 10003.15"} dönüşür > "'10001.00','10002,01','10003.15'" 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ArrayForSql(List<string> list)
        {
            return ArrayForSql(list.ToArray());
        }

        /// <summary>
        /// string bir dizi gönderip sql cümlesinde kullanacağımız hale getirir. Örnek new string[]{"10001.00, 10002.01, 10003.15"}  dönüşür > "'10001.00','10002,01','10003.15'" 
        /// </summary>
        /// <param name="Array">Sql cümlesine hazırlayacağımız string dizi</param>
        /// <returns>string değer</returns>
        public static string ArrayForSql(string[] Array)
        {
            string str = "";
            foreach (string s in Array)
            {
                if (string.IsNullOrEmpty(s.Trim()))
                    continue;
                if (str == "")
                    str = string.Format("'{0}'", s.Trim());
                else
                    str += string.Format(",'{0}'", s.Trim());
            }
            return str;
        }


        /// <summary>
        /// integer bir diziyi sql cümlesinde kullanacağız hale getirir. Örnek new int[]{100, 99, 102} dönüşür > "100,99,102"
        /// </summary>
        /// <param name="list">integer Liste sql cümlesinde kullacağız.</param>
        /// <returns>string değer</returns>
        public static string ArrayForSql(List<int> list)
        {
            return ArrayForSql(list.ToArray());
        }

        /// <summary>
        /// integer bir diziyi sql cümlesinde kullanacağız hale getirir. Örnek new int[]{100, 99, 102} dönüşür > "100,99,102"
        /// </summary>
        /// <param name="Array">integer dizi sql cümlesinde kullacağız.</param>
        /// <returns>string değer</returns>
        public static string ArrayForSql(int[] Array)
        {
            string[] dizi = new string[Array.Length];
            for (int i = 0; i < Array.Length; i++)
            {
                dizi[i] = Array[i].ToString();
            }
            string str = ArrayForSql(dizi);
            str = str.Replace("'", "");
            return str;
        }



    }
}
