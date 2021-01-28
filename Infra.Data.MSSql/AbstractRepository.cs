using Aplicativo.Net.Shared.Repositories;
using Dapper;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Infra.Data.MSSql
{
    public abstract class AbstractRepository : Notifiable
    {
        public string ConnectionString { get; private set; }
        protected AbstractRepository(string connectionString) => ConnectionString = connectionString;
        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
        protected IEnumerable<T> Query<T>(string query, object parameters)
        {
            using (var connection = CreateConnection())
            {
                return connection.Query<T>(query, parameters);
            }
        }
        protected void Execute(string command, object parameters)
        {
            using (var connection = CreateConnection())
            {
                connection.Execute(command, parameters);
            }
        }
    }
}
