using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms12m.Business
{
    public class Helper
    {
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
    }
}
