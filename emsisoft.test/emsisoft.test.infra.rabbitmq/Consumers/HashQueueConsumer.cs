using emsisoft.test.core.Repository.Hash;
using RabbitMQ.Client;
using System.Text;

namespace emsisoft.test.infra.rabbitmq.Consumers
{
    public class HashQueueConsumer : DefaultBasicConsumer
    {
        private readonly IHashRepository hashRepository;

        public HashQueueConsumer(IHashRepository hashRepository)
        {
            this.hashRepository = hashRepository;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            base.HandleBasicDeliver(consumerTag, deliveryTag, redelivered, exchange, routingKey, properties, body);

            var bodyArray = body.ToArray();
            var message =  Encoding.UTF8.GetString(bodyArray);

            hashRepository.CreateHashes(message);
        }
    }
}
