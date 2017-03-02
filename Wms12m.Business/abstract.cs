using Wms12m.Entity;
using System.Collections.Generic;

namespace Wms12m.Business
{
    public abstract class abstractTables<T>
    {
        public abstract T Detail(int Id);
        public abstract List<T> GetList();
        public abstract List<T> GetList(int ParentId);
        public abstract Result Delete(int Id);      
        public abstract Result Operation(T tbl);
    }
}
