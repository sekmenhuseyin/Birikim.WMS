using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Wms12m.Business
{
    public static class QueryAnalysis<T>
    {
        public static string Update(T table, string tableName)
        {
            string a = "update " + tableName + " set  ";
            var prop = table.GetType().GetProperties();
            string qery = "";
            string Where = "  Where ";
            foreach (var props in prop)
            {
                var Value = props.GetValue(table, null);
                object[] attrs = props.GetCustomAttributes(true);
                if (attrs.Length == 1)
                {
                    if (attrs[0].ToString().Contains("Key"))
                    {
                        Where += props.Name + "=@" + props.Name;
                    }
                }
                else
                {
                    if (Value != null)
                    {
                        var Colunm = props.Name;
                        qery += props.Name + "=@" + props.Name + ",";
                    }
                }
            }
            return a + qery.Substring(0, qery.Length - 1) + Where;
        }
        public static string Insert(T table, string tableName)
        {
            string a = "insert into " + tableName;
            string Param = "(";
            string Values = "(";
            var prop = table.GetType().GetProperties();
            foreach (var props in prop)
            {
                var Value = props.GetValue(table, null);
                object[] attrs = props.GetCustomAttributes(true);
                if (attrs.Length > 0)
                {
                    if (Value != null)
                    {
                        if (!attrs[0].ToString().Contains("Key"))
                        {
                            var Colunm = props.Name;
                            Param += props.Name + ",";
                            Values += "@" + props.Name + ",";
                        }
                    }
                }
            }
            return a + " " + Param.Substring(0, Param.Length - 1) + ")values" + Values.Substring(0, Values.Length - 1) + ")";
        }
        public static List<string> WordCrop(string Word, int Length)
        {
            List<string> _Lst = new List<string>();
            int _Count = Word.Length % Length > 0 ? (Word.Length / Length) + 1 : (Word.Length / Length);
            string _totalWord = "";
            try
            {
                if (Word.Length > Length)
                {
                    for (int i = 0; i < _Count; i++)
                    {

                        int _KesilecekUzunluk = 0;
                        int kesilenKisim = _totalWord == "" ? 0 : _totalWord.Length;
                        int _TotalKelime = Word.Length;
                        _KesilecekUzunluk = kesilenKisim == 0 ? Length : ((_TotalKelime - kesilenKisim) > Length ? Length : (_TotalKelime - kesilenKisim));
                        string _newWord = Word.Substring((_totalWord == "" ? 0 : _totalWord.Length), _KesilecekUzunluk);
                        _Lst.Add(_newWord);
                        _totalWord += _newWord;
                    }
                }
                else
                {
                    _Lst.Add(Word);
                }
            }
            catch (Exception)
            {
                _Lst = null;
            }
            return _Lst;
        }
        public static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            text = System.Net.WebUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
        public static string InnerJoin(object[] List)
        {
            string Return = "";
            try
            {
          
                return Return;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
