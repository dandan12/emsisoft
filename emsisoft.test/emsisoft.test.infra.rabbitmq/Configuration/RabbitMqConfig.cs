using emsisoft.test.core.Application.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emsisoft.test.infra.rabbitmq.Configuration
{
    public class RabbitMqConfig
    {
        public string HostName { get; set; }
        public RabbitMQQueue[] Queues { get; set; }
    }

    public class RabbitMQQueue
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public QueueType Type { get; set; }
    }
}
