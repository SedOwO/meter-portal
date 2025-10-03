using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerWorker.Messages
{
    internal interface IRabbitMqPublisher
    {
        Task PublishMessage(string message);
    }
}
