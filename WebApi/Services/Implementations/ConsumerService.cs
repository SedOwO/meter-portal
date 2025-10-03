using System.Runtime.CompilerServices;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class ConsumerService : IConsumerService
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly IComplaintRepository _complaintRepository;

        public ConsumerService(IConsumerRepository consumerRepository, IComplaintRepository complaintRepository)
        {
            _consumerRepository = consumerRepository;
            _complaintRepository = complaintRepository;
        }

        public async Task<Complaint?> CreateComplaintAsync(int userId, CreateComplaintRequest complaint)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            if (string.IsNullOrWhiteSpace(complaint.Title))
                throw new ArgumentException("Title is Required");

            if (string.IsNullOrWhiteSpace(complaint.Description))
                throw new ArgumentException("Description is Required");

            var newComplaint = new ComplaintRequest
            {
                ConsumerId = consumer.ConsumerId,
                Title = complaint.Title,
                Description = complaint.Description,
                Status = "open"
            };

            var complaintId = await _complaintRepository.CreateComplaintAsync(newComplaint);

            var response = await _complaintRepository.GetComplaintByIdAsync(complaintId);

            return response;
        }

        public async Task<Complaint?> GetComplaintByIdAsync(int userId, int complaintId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var complaint = await _complaintRepository.GetComplaintByIdAsync(complaintId);

            if (complaint == null || complaint.ConsumerId != consumer.ConsumerId)
                return null;

            return complaint;
        }

        public async Task<IEnumerable<Complaint>> GetUserComplaintAsync(int userId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var complaints = await _complaintRepository.GetAllComplaintsByConsumerIdAsync(consumer.ConsumerId);

            return complaints;
        }


        // recharge

    }
}
