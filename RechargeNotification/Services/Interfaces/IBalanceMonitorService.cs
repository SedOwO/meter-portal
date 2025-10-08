using RechargeNotification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechargeNotification.Services.Interfaces
{
    public interface IBalanceMonitorService
    {
        Task<List<ConsumerBalance>> GetLowBalanceConsumersAsync();
    }
}
