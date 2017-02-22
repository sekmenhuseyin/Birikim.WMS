using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms12m.Entity;

namespace Wms12m.Business
{
    public abstract class abstractMalKabul<T>
    {
        public abstract T Detail(int Id);
        public abstract List<T> GetList();
        public abstract List<T> SubList(int Id);
        public abstract Result Delete(int Id);
        public abstract Result Operation(T P);
    }
}
