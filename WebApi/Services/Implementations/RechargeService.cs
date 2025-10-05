using System.Runtime.InteropServices;
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

        public RechargeService(IRechargeRepository rechargeRepository, IConsumerRepository consumerRepository, ISmartMeterRepository smartMeterRepository)
        {
            _rechargeRepository = rechargeRepository;
            _consumerRepository = consumerRepository;
            _smartMeterRepository = smartMeterRepository;
        }

        public async Task<RechargeRespone> RechargeSmartMeterAsync(int userId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Recharge amount must be greater than zero.");

            if (amount > 99999999.99m)
                throw new ArgumentException("Recharge amount exceeds maximum limit");

            var consumer = await _consumerRepository.GetConsumerByUserIdAsync(userId);
            if (consumer == null)
                throw new InvalidOperationException("Consumer profile not found");

            var meters = await _smartMeterRepository.GetAllMetersByConsumerId(consumer.ConsumerId);
            var meter = meters.FirstOrDefault();

            if (meter == null)
                throw new InvalidOperationException("No smart meter found for this consumer. Please contact support.");

            if (meter.Status != "active")
                throw new InvalidOperationException($"Smart meter is currently {meter.Status}. Cannot process recharge.");

            var rechargeSuccess = await _smartMeterRepository.AddRechargeAsync(meter.MeterId, amount);

            if (!rechargeSuccess)
                throw new InvalidOperationException("Failed to process recharge. Please try again.");

            var updatedMeter = await _smartMeterRepository.GetMeterByIdAsync(meter.MeterId);

            return new RechargeRespone
            {
                MeterId = meter.MeterId,
                Amount = amount
            };
        }
    }
}
