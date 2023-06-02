using emsisoft.test.core.Application.Queues.Models;
using emsisoft.test.infra.rabbitmq.Configuration;
using emsisoft.test.infra.rabbitmq.Models;
using RabbitMQ.Client;

namespace emsisoft.test.infra.rabbitmq.Repositories
{
    public class RabbitMQRepository
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly RabbitMqConfig rabbitMqConfig;
        private Dictionary<QueueType, IBasicConsumer> basicConsumers = new();
        public RabbitMQRepository(IConnectionFactory connectionFactory,
            RabbitMqConfig rabbitMqConfig,
            IEnumerable<BasicConsumerDefinition> basicConsumerDefinitions)
        {
            this.connectionFactory = connectionFactory;
            this.rabbitMqConfig = rabbitMqConfig;
            basicConsumers = basicConsumerDefinitions.ToDictionary(x => x.QueueType, x => x.BasicConsumer);
        }

        public async Task ConsumeMessages()
        {
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            foreach (var queue in rabbitMqConfig.Queues)
            {
                channel.BasicConsume(queue.Name, true, basicConsumers[queue.Type]);
            }
        }
    }
}
