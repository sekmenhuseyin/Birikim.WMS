using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Wms12m.Repository
{
    internal class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        public Repository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public virtual int Add(T entity, string Query)
        {
            try
            {
                return Connection.Execute(Query, entity, transaction: Transaction);
            }
            catch (Exception ex)
            {

                if (ex.Message.StartsWith("Violation of UNIQUE KEY"))
                {
                    return -2;
                }
                else
                {
                    return -1;
                }
                
            }
        }

        public virtual IList<T> All(string TableName)
        {
            return Connection.Query<T>(
                  ("SELECT * FROM " + TableName),
                  transaction: Transaction
              ).ToList();
        }

        public virtual int Delete(string Query)
        {
            return Connection.Execute(Query, transaction: Transaction);
        }

        public virtual T Find(string Query)
        {
            return Connection.Query<T>(Query, transaction: Transaction).SingleOrDefault();
        }

        public IList<T> QueryAll(string Query)
        {
            return Connection.Query<T>(
                  (Query),
                  transaction: Transaction
              ).ToList();
        }

        public virtual int Update(T entity, string Query)
        {
            try
            {
                return Connection.Execute(Query, entity, transaction: Transaction);
            }
            catch (System.Exception)
            {
                return -1;
            }
        }
    }
}
