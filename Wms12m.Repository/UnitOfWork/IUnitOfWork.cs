using System;

namespace Wms12m.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> Repository<T>() where T : class, new();

        void Commit();
    }
}
