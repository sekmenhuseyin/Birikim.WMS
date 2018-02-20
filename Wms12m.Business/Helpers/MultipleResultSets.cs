using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Wms12m
{
    public static class MultipleResultSets
    {
        public static MultipleResultSetWrapper MultipleResults(this DbContext db, string storedProcedure)
        {
            return new MultipleResultSetWrapper(db, storedProcedure);
        }

        public class MultipleResultSetWrapper
        {
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> ResultSets;
            private readonly DbContext _db;
            private readonly string _storedProcedure;

            public MultipleResultSetWrapper(DbContext db, string storedProcedure)
            {
                _db = db;
                _storedProcedure = storedProcedure;
                ResultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public List<IEnumerable> Execute()
            {
                var results = new List<IEnumerable>();

                var connection = _db.Database.Connection;
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC " + _storedProcedure;

                using (var reader = command.ExecuteReader())
                {
                    var adapter = ((IObjectContextAdapter)_db);
                    foreach (var resultSet in ResultSets)
                    {
                        results.Add(resultSet(adapter, reader));
                        reader.NextResult();
                    }
                }

                return results;
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                ResultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }
        }
    }
}