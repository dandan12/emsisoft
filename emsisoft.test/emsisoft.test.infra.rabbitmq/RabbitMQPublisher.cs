using emsisoft.test.core.Abstractions;
using emsisoft.test.core.Application.Queues.Models;
using emsisoft.test.infra.rabbitmq.Models;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using System.Text;

namespace emsisoft.test.infra.rabbitmq
{
    public class RabbitMqPublisher : IMessegeQueuePublisher
    {
        private ConcurrentQueue<QueueData> queues = new();
        private readonly IConnectionFactory connectionFactory;
        private Dictionary<QueueType, string> queueTypes = new();
        public RabbitMqPublisher(IConnectionFactory connectionFactory,
            IEnumerable<QueueDefinition> queueDefinitions)
        {
            this.connectionFactory = connectionFactory;
            queueTypes = queueDefinitions.ToDictionary(x => x.QueueType, x => x.QueueName);
        }

        public Task AddMessage(QueueType queueType, string data)
        {
            queues.Enqueue(new(queueTypes[queueType], data));
            return Task.CompletedTask;
        }

        public Task TryPublishMessages()
        {
            if (queues.Count <= 0) return Task.CompletedTask;

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            try
            {
                while (queues.TryDequeue(out var queue))
                {
                    var body = Encoding.UTF8.GetBytes(queue.Data);
                    channel.BasicPublish("", queue.QueueName, null, body);
                }
            }
            catch (Exception ex)
            {
                // log when dequeueing fails
            }

            return Task.CompletedTask;
        }

        public Task AddQueue(string queueName)
        {
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName);
            
            return Task.CompletedTask;
        }

        public Task DeleteQueue(string queueName)
        {
            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDelete(queueName);

            return Task.CompletedTask;
        }
    }
}
