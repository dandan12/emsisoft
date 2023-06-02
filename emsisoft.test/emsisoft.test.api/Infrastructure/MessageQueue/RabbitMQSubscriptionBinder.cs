using emsisoft.test.core.Abstractions;
using emsisoft.test.infra.rabbitmq.Configuration;

namespace emsisoft.test.api.Infrastructure.MessageQueue
{
    public interface IRabbitMQSubscriptionBinder
    {
        Task DoWork(CancellationToken cancellationToken);
    }

    public class RabbitMQSubscriptionBinder : IRabbitMQSubscriptionBinder
    {
        private readonly IMessegeQueuePublisher messegeQueuePublisher;
        private readonly RabbitMqConfig rabbitMqConfig;

        public RabbitMQSubscriptionBinder(IMessegeQueuePublisher messegeQueuePublisher,
            RabbitMqConfig rabbitMqConfig)
        {
            this.messegeQueuePublisher = messegeQueuePublisher;
            this.rabbitMqConfig = rabbitMqConfig;
        }

        public Task DoWork(CancellationToken cancellationToken)
        {
            foreach (var queueTopic in rabbitMqConfig.Queues)
            {
                if (queueTopic.Active)
                    messegeQueuePublisher.AddQueue(queueTopic.Name);
                else
                    messegeQueuePublisher.DeleteQueue(queueTopic.Name);
            }
            
            throw new NotImplementedException();
        }
    }
}
