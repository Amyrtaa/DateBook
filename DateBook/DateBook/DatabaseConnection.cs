using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DateBook
{
    public static class DatabaseConnection
    {
        private static NpgsqlConnection? _connection;

        private static string GetConnectionString()
        {
            return @"Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=code_624";
        }

        public static NpgsqlConnection GetSqlConnection()
        {
            if (_connection is null)
            {
                _connection = new NpgsqlConnection(GetConnectionString());
                _connection.Open();
            }

            return _connection;
        }
    }
}
