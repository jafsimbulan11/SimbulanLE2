using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BlogDataLibrary.Database
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<List<T>> LoadData<T, U>(
            string sqlStatement,
            U parameters,
            string connectionStringName,
            bool isStoredProcedure)
        {
            var connectionString = _config.GetConnectionString(connectionStringName);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");
            }

            using IDbConnection connection = new SqlConnection(connectionString);

            var rows = await connection.QueryAsync<T>(
                sqlStatement,
                parameters,
                commandType: isStoredProcedure
                    ? CommandType.StoredProcedure
                    : CommandType.Text);

            return rows.ToList();
        }

        public async Task SaveData<T>(
            string sqlStatement,
            T parameters,
            string connectionStringName,
            bool isStoredProcedure)
        {
            var connectionString = _config.GetConnectionString(connectionStringName);
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException($"Connection string '{connectionStringName}' not found.");
            }

            using IDbConnection connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(
                sqlStatement,
                parameters,
                commandType: isStoredProcedure
                    ? CommandType.StoredProcedure
                    : CommandType.Text);
        }
    }
}
