using System.Collections.Generic;

namespace Wms12m.Business
{
    public class CacheControl<T> : abstractCache<T>
    {
        public override List<T> CacheGet(string CacheKey)
        {
            return ((List<T>)CacheManager.Current.Get(CacheKey));
        }

        public override void CacheUpdate(List<T> Update,string CacheKey)
        {
            CacheManager.Current.Remove(CacheKey);
            CacheManager.Current.Add(CacheKey, Update);
        }
    }
}
