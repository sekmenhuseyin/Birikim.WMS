using System;
using Wms12m.Entity;
using Wms12m.Security;
using Wms12m.Entity.Models;

namespace Wms12m.Business
{
    public sealed class Logger
    {
        public void Writer(LogCs Parametre)
        {
            string username = Identity.Current.User.UserName;
            string machine = Identity.Current.Action.Machine;
            string ipAddress = Identity.Current.Action.IpAddress;
            string url = Identity.Current.Action.Url;
            using (WMSEntities db = new WMSEntities())
            {
                var s = db.Logger(username, machine, ipAddress, Parametre.Description, Parametre.Method, url);
            }
        }
    }
}
