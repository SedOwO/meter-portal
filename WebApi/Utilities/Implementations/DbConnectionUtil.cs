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

        //public async Task<(List<T> items, int totalCount)> ExecutePagedQueryAsync<T>(string query, string countQuery, Func<NpgsqlDataReader, Task<T>> mapFunc, int page, int pageSize, Dictionary<string, object>? parameters = null)
        //{
        //    var items = new List<T>();
        //    int totalCount = 0;

        //    await using var conn = new NpgsqlConnection(_connectionString);
        //    await conn.OpenAsync();

        //    await using (var countCommand = new NpgsqlCommand(countQuery, conn))
        //    {
        //        if (parameters != null)
        //        {
        //            foreach (var parameter in parameters)
        //            {
        //                countCommand.Parameters.AddWithValue(parameter.Key, parameter.Value);
        //            }
        //        }
        //        totalCount = Convert.ToInt32(await countCommand.ExecuteScalarAsync() ?? 0);
        //    }

        //    var offset = (page - 1) * pageSize;
        //    var paginatedQuery = $"{query} LIMIT @PageSize OFFSET @Offset";
            
        //    await using (var dataCommand = new NpgsqlCommand(paginatedQuery, conn))
        //    {
        //        dataCommand.Parameters.AddWithValue("@PageSize", pageSize);
        //        dataCommand.Parameters.AddWithValue("@Offset", offset);

        //        if (parameters != null)
        //        {
        //            foreach (var param in parameters)
        //            {
        //                dataCommand.Parameters.AddWithValue(param.Key, param.Value);
        //            }
        //        }

        //        await using var reader = await dataCommand.ExecuteReaderAsync();
        //        while (await reader.ReadAsync())
        //        {
        //            items.Add(await mapFunc(reader));
        //        }
        //    }
        //    return (items,  totalCount);
        //}

        public async Task<NpgsqlDataReader> ExecuteReaderAsync(string query, Dictionary<string, object>? parameters = null)
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

        public async Task<T?> ExecuteScalarAsync<T>(string query, Dictionary<string, object>? parameters = null)
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
            return result != DBNull.Value && result != null ? (T)result : default;
        }
    }
}
