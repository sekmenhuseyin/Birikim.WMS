using System.Collections.Generic;

namespace Wms12m.Repository
{
    public interface IRepository<T> where T : class
    {
        int Add(T entity, string Query);
        IList<T> All(string TableName);
        IList<T> QueryAll(string TableName);
        int Delete(string Query);
        T Find(string Query);
        int Update(T entity, string Query);
    }
}
