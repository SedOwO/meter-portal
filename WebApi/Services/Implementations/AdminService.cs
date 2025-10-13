using WebApi.Models.DB;
using WebApi.Models.Misc;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IConsumerRepository _consumerRepository;

        public AdminService(IComplaintRepository complaintRepository, IConsumerRepository consumerRepository)
        {
            _complaintRepository = complaintRepository;
            _consumerRepository = consumerRepository;
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaints()
        {
            var complaints = await _complaintRepository.GetAllComplaintsAsync();

            return complaints;
        }

        public async Task<PagedList<Complaint>> GetAllComplaintsPaginated(int page, int pageSize)
        {
            try
            {
                var complaints = await _complaintRepository.GetAllComplaintsPaginatedAsync(page, pageSize);

                return complaints;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
