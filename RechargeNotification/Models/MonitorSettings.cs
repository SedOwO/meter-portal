using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechargeNotification.Models
{
    public class MonitorSettings
    {
        public decimal LowBalanceThreshold { get; set; } = 50.00m;
        public int CheckIntervalMinutes { get; set; } = 5;
    }
}
