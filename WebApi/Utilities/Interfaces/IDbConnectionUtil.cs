using Npgsql;

namespace WebApi.Utilities.Interfaces
{
    public interface IDbConnectionUtil
    {
        Task<NpgsqlDataReader> ExecuteReaderAsync(string query, Dictionary<string, object>? parameters = null);
        Task<T?> ExecuteScalarAsync<T>(string query, Dictionary<string, object>? parameters = null);


        //// Pagination Method
        //Task<(List<T> items, int totalCount)> ExecutePagedQueryAsync<T>(
        //    string query,
        //    string countQuery,
        //    Func<NpgsqlDataReader, Task<T>> mapFunc,
        //    int page,
        //    int pageSize,
        //    Dictionary<string, object>? parameters = null);
    }
}
