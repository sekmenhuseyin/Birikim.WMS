using System.Collections.Generic;

namespace Wms12m.Business
{
    public abstract class abstractCache<T>
    {
        public abstract void CacheUpdate(List<T> Update, string CacheKey);
        public abstract List<T> CacheGet(string CacheKey);

    }
}
