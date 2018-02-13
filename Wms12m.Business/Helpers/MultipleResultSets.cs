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
            public List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>> _resultSets;
            private readonly DbContext db;
            private readonly string storedProcedure;

            public MultipleResultSetWrapper(DbContext db, string storedProcedure)
            {
                this.db = db;
                this.storedProcedure = storedProcedure;
                _resultSets = new List<Func<IObjectContextAdapter, DbDataReader, IEnumerable>>();
            }

            public List<IEnumerable> Execute()
            {
                var results = new List<IEnumerable>();

                var connection = db.Database.Connection;
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "EXEC " + storedProcedure;

                using (var reader = command.ExecuteReader())
                {
                    var adapter = ((IObjectContextAdapter)db);
                    foreach (var resultSet in _resultSets)
                    {
                        results.Add(resultSet(adapter, reader));
                        reader.NextResult();
                    }
                }

                return results;
            }

            public MultipleResultSetWrapper With<TResult>()
            {
                _resultSets.Add((adapter, reader) => adapter
                    .ObjectContext
                    .Translate<TResult>(reader)
                    .ToList());

                return this;
            }
        }
    }
}