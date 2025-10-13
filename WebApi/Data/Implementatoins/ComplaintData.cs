using System.Data;
using System.Diagnostics.Metrics;
using System.Threading;
using WebApi.Data.Interfaces;
using WebApi.Models.DB;
using WebApi.Models.Misc;
using WebApi.Models.Request;
using WebApi.Utilities.Interfaces;

namespace WebApi.Data.Implementatoins
{
    public class ComplaintData : IComplaintData
    {
        private readonly IDbConnectionUtil _db;

        public ComplaintData(IDbConnectionUtil db)
        {
            _db = db;
        }

        public async Task<int> CreateComplaintAsync(ComplaintRequest complaint)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "consumerid", complaint.ConsumerId},
                    { "title", complaint.Title},
                    { "description", complaint.Description}
                };

                var result = await _db.ExecuteScalarAsync<int>
                    ("SELECT * FROM public.createcomplaint(@consumerid, @title, @description);", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteComplaintAsync(int complaintId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "complaintid", complaintId}
                };
                var result = await _db.ExecuteScalarAsync<bool>(
                    "SELECT * FROM public.deletecomplaint(@complaintid);", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsAsync()
        {
            try
            {
                var complaints = new List<Complaint>();

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallcomplaints();");
                while (await reader.ReadAsync())
                {
                    complaints.Add(new Complaint
                    {
                        ComplaintId = reader.GetInt32(reader.GetOrdinal("complaintid")),
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        Status = reader.GetString(reader.GetOrdinal("status")),
                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    });
                }

                return complaints;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaintsByConsumerIdAsync(int consumerId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "consumerid", consumerId}
                };

                var complaints = new List<Complaint>();

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallcomplaintsbyconsumerid(@consumerid);", parameters);
                while (await reader.ReadAsync())
                {
                    complaints.Add(new Complaint
                    {
                        ComplaintId = reader.GetInt32(reader.GetOrdinal("complaintid")),
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        Status = reader.GetString(reader.GetOrdinal("status")),
                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    });
                }

                return complaints;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedList<Complaint>> GetAllComplaintsPaginatedAsync(int page, int pageSize)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@p_page", page },
                    { "@p_pageSize", pageSize }
                };

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getallcomplaintspaginated(@p_page, @p_pageSize)", parameters);

                var complaints = new List<Complaint>();
                int totalCount = 0;

                while (await reader.ReadAsync())
                {
                    complaints.Add(new Complaint
                    {
                        ComplaintId = reader.GetInt32(0),
                        ConsumerId = reader.GetInt32(1),
                        Title = reader.GetString(2),
                        Description = reader.GetString(3),
                        Status = reader.GetString(4),
                        UpdatedAt = reader.GetDateTime(5)
                    });

                    if (totalCount == 0)
                        totalCount = Convert.ToInt32(reader.GetInt64(6));
                }

                return PagedList<Complaint>.Create(complaints, page, pageSize, totalCount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Complaint?> GetComplaintByIdAsync(int complaintId)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "complaintid", complaintId }
                };

                using var reader = await _db.ExecuteReaderAsync("SELECT * FROM public.getcomplaintbyid(@complaintid);", parameters);

                if (await reader.ReadAsync())
                {
                    return new Complaint
                    {
                        ComplaintId = complaintId,
                        ConsumerId = reader.GetInt32(reader.GetOrdinal("consumerid")),
                        Title = reader.GetString(reader.GetOrdinal("title")),
                        Description = reader.GetString(reader.GetOrdinal("description")),
                        Status = reader.GetString(reader.GetOrdinal("status")),
                        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    };
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateComplaintAsync(int complaintId, ComplaintRequest complaint)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "complaintid", complaintId},
                    { "status", complaint.Status}
                };

                var result = await _db.ExecuteScalarAsync<bool>(
                    "SELECT * FROM public.updatecomplaint(@complaintid, @status);", parameters);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
