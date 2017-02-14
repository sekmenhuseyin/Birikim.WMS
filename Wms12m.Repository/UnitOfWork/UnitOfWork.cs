using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Wms12m.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private Dictionary<string, object> repositories = new Dictionary<string, object>();
        private bool _disposed;
        public UnitOfWork()
        {
            try
            {
                _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SettingConnectionString"].ConnectionString);
                _connection.Open();
                _transaction = _connection.BeginTransaction();
            }
            catch (Exception)
            {
                _transaction = null;
            }

        }

        public IRepository<T> Repository<T>() where T : class, new()
        {
            var type = typeof(T).Name;
            if (repositories.ContainsKey(type))
            {
                return (IRepository<T>)repositories[type];
            }
            var repositoryType = typeof(Repository<>);
            repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _transaction));
            return (IRepository<T>)repositories[type];
        }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            repositories = null;

        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
