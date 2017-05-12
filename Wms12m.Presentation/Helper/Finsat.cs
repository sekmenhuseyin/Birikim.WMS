using System;
using System.Web;
using Wms12m.Security;

namespace Wms12m
{
    public class Finsat
    {
        private string ConStr { get; set; }
        private string SirketKodu { get; set; }
        public Finsat(string conStr, string sirketKodu)
        {
            ConStr = conStr;
            SirketKodu = sirketKodu;
        }
    }
}