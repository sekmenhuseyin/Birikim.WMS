using System.Collections.Generic;
using Wms12m.Entity;

namespace Wms12m.Business
{
    public abstract class AbstractPersons
    {
        public abstract Result Login(USER01 P);
        public abstract USER01 Detail(int Id);
        public abstract List<USER01> PersonList();
     }
}
