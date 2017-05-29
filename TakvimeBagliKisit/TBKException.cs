using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TakvimeBagliKisit
{
    public class TBKException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="donemTipi">0: FINSAT; 1 MUHASEBE; 2 DEMİRBAŞ; 3 ÜRETİM</param>
        public TBKException(short donemTipi)
            : base(TBKIslem.HataMesaji(donemTipi))
        {

        }  
    }
}
