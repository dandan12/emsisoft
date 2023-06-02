using emsisoft.test.core.Application.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emsisoft.test.infra.rabbitmq.Models
{
    public record QueueDefinition(QueueType QueueType, string QueueName);
}
