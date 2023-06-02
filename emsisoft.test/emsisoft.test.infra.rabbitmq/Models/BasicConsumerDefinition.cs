using emsisoft.test.core.Application.Queues.Models;
using RabbitMQ.Client;

namespace emsisoft.test.infra.rabbitmq.Models
{
    public record BasicConsumerDefinition(QueueType QueueType, IBasicConsumer BasicConsumer);
}
