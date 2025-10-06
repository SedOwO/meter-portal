using System.Reflection.Metadata.Ecma335;
using WebApi.Models.DB;
using WebApi.Models.Request;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class SmartMeterService : ISmartMeterService
    {
        private readonly ISmartMeterRepository _smartMeterRepository;
        private readonly IConsumerRepository _consumerRepository;

        public SmartMeterService(ISmartMeterRepository smartMeterRepository, IConsumerRepository consumerRepository)
        {
            _smartMeterRepository = smartMeterRepository;
            _consumerRepository = consumerRepository;
        }

        public async Task<SmartMeter?> CreateSmartMeterAsync(int userId, SmartMeterRequest meter)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var newMeterId = await _smartMeterRepository.CreateMeterAsync(meter);

            var response = await _smartMeterRepository.GetMeterByIdAsync(newMeterId);

            if (response == null)
                throw new ArgumentException("cant create meter");

            return response;
        }

        public async Task<IEnumerable<SmartMeter>> GetAllSmartMeterByConsumerIdAsync(int userId, int consumerId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var response = await _smartMeterRepository.GetAllMetersByConsumerId(consumerId);

            return response;
        }

        public async Task<IEnumerable<SmartMeter>> GetAllSmartMetersAsync(int userId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var response = await _smartMeterRepository.GetAllMetersAsync();

            return response;
        }

        public async Task<SmartMeter?> GetSmartMeterByIdAsync(int userId, int meterId)
        {
            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var response = await _smartMeterRepository.GetMeterByIdAsync(meterId);

            if (response == null)
                throw new ArgumentException("meter not found");

            return response;
        }
    }
}
