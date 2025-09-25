using Npgsql;
using WebApi.Utilities.Interfaces;

namespace WebApi.Utilities.Implementations
{
    public class DbConnectionUtil : IDbConnectionUtil
    {
        private readonly string _connectionString;

        public DbConnectionUtil(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<NpgsqlDataReader> ExecuteReaderAsync(string query, Dictionary<string, object> parameters = null)
        {
            var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(query, conn);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }

            return await cmd.ExecuteReaderAsync();
        }

        public async Task<T?> ExecuteScalarAsync<T>(string query, Dictionary<string, object> parameters = null)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(query, conn);

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                }
            }
            var result = await cmd.ExecuteScalarAsync();
            return result != DBNull.Value ? (T)result : default;
        }
    }
}
