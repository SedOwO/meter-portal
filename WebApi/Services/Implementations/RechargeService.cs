using System.Runtime.InteropServices;
using WebApi.Messages;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Interfaces;

namespace WebApi.Services.Implementations
{
    public class RechargeService : IRechargeService
    {
        private readonly IRechargeRepository _rechargeRepository;
        private readonly IConsumerRepository _consumerRepository;
        private readonly ISmartMeterRepository _smartMeterRepository;
        private readonly IRabbitMqPublisher _rabbitMqPublisher;

        public RechargeService(IRechargeRepository rechargeRepository, IConsumerRepository consumerRepository, ISmartMeterRepository smartMeterRepository, IRabbitMqPublisher rabbitMqPublisher)
        {
            _rechargeRepository = rechargeRepository;
            _consumerRepository = consumerRepository;
            _smartMeterRepository = smartMeterRepository;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public async Task<RechargeRespone> RechargeSmartMeterAsync(int userId, RechargeRequest recharge)
        {
            if (recharge.Amount <= 0)
                throw new ArgumentException("Recharge amount must be greater than zero.");

            if (recharge.Amount > 99999999.99m)
                throw new ArgumentException("Recharge amount exceeds maximum limit");

            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var meter = await _smartMeterRepository.GetMeterByIdAsync(recharge.MeterId);

            if (meter == null)
                throw new InvalidOperationException("No smart meter found for this consumer. Please contact support.");

            if (meter.Status != "active")
                throw new InvalidOperationException($"Smart meter is currently {meter.Status}. Cannot process recharge.");

            var rechargeSuccess = await _smartMeterRepository.AddRechargeAsync(meter.MeterId, recharge.Amount);

            if (!rechargeSuccess)
                throw new InvalidOperationException("Failed to process recharge. Please try again.");

            var updatedMeter = await _smartMeterRepository.GetMeterByIdAsync(meter.MeterId);

            var response = new RechargeRespone
            {
                MeterId = meter.MeterId,
                Amount = recharge.Amount
            };

            var message = $"New Recharge: {recharge.Amount}";
            await _rabbitMqPublisher.PublishMessage(message);

            return response;
        }
    }
}
