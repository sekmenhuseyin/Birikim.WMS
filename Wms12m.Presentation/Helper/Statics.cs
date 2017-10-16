using Wms12m.Entity.Models;

namespace Wms12m
{
    public class Statics
    {
        public static void PutUsersOffline()
        {
            using (var db = new WMSEntities())
            {
                foreach (var connection in db.Connections)
                    connection.IsOnline = false;
                db.SaveChanges();
            }
        }
    }
}