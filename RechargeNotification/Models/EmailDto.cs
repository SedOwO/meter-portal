using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechargeNotification.Models
{
    public class EmailDto
    {
        public string ConsumerName { get; set; }
        public string Email { get; set; }
        public string MeterNumber { get; set; }
        public decimal BalanceAmount { get; set; }
    }
}
