namespace emsisoft.test.core.Repository.RabbitMQ
{
    public interface IRabbitMQRepository
    {
        Task ConsumeMessages();
    }
}
