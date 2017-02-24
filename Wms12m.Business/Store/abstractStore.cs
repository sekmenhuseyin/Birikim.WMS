using System.Collections.Generic;
using Wms12m.Entity;

namespace Wms12m.Business
{
    public abstract class abstractStore<T>
    {
        public abstract T Detail(int Id);
        public abstract List<T> GetList();
        public abstract List<T> GetList(int ParentId);
        public abstract List<T> SubList(int Id);
        public abstract Result Delete(int Id);      
        public abstract Result Update(T tbl);
        public abstract Result Insert(T tbl);
    }
}
