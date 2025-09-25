using RabbitMQ.Client.Events;
using System.Xml;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class ConsumerData : IConsumerData
    {
        private readonly IDbConnectionUtil _db;

        public ConsumerData(IDbConnectionUtil db)
        {
            _db = db;
        }


        public async Task<int> CreateConsumerAsync(ConsumerDetailRequest consumer)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "userid", consumer.UserId },
                    { "firstname", consumer.FirstName },
                    { "lastname", consumer.LastName },
                    { "email", consumer.Email },
                    { "phone", consumer.Phone },
                    { "address", consumer.Address }
                };

                var result = await _db.ExecuteScalarAsync<int>("query", parameters);
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ConsumerDetail?> GetConsumerByUserIdAsync(int userId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "userid", userId }
                };

                using var reader = await _db.ExecuteReaderAsync("query", parameters);

                if (await reader.ReadAsync())
                {
                    return new ConsumerDetail
                    {
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        UserId = reader.GetInt32(reader.GetOrdinal("userid")),
                        FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                        LastName = reader.GetString(reader.GetOrdinal("lastname")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? null : reader.GetString(reader.GetOrdinal("phone")),
                        Address = reader.IsDBNull(reader.GetOrdinal("address")) ? null : reader.GetString(reader.GetOrdinal("address"))
                    };
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
