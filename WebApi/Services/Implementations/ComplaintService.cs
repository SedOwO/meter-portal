using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using WebApi.Models.DB;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        private readonly IConsumerRepository _consumerRepository;

        public ComplaintService(IComplaintRepository complaintRepository, IConsumerRepository consumerRepository)
        {
            _complaintRepository = complaintRepository;
            _consumerRepository = consumerRepository;
        }

        public async Task<Complaint?> GetComplaintsByIdAsync(int userId, int complaintId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("User profile not found");

            var complaint = await _complaintRepository.GetComplaintByIdAsync(complaintId);

            return complaint;
        }
    }
}
