using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechargeNotification.Models
{
    public class ConsumerBalance
    {
        public int ConsumerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int MeterId { get; set; }
        public string MeterNumber { get; set; } = string.Empty;
        public decimal BalanceAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
