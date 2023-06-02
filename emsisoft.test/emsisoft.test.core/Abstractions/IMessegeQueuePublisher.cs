using emsisoft.test.core.Application.Queues.Models;

namespace emsisoft.test.core.Abstractions
{
    public interface IMessegeQueuePublisher
    {
        Task AddMessage(QueueType queueType, string data);
        Task TryPublishMessages();
        Task AddQueue(string queueName);
        Task DeleteQueue(string queueName);
    }
}
