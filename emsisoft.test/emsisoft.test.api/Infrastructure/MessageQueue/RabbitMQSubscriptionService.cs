namespace emsisoft.test.api.Infrastructure.MessageQueue
{
    public class RabbitMQSubscriptionService : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly TaskCompletionSource taskCompletionSource = new();

        public RabbitMQSubscriptionService(IServiceProvider serviceProvider, IHostApplicationLifetime lifetime)
        {
            this.serviceProvider = serviceProvider;
            lifetime.ApplicationStarted.Register(() => taskCompletionSource.SetResult());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await taskCompletionSource.Task;
                using var scope = serviceProvider.CreateScope();
                var rabbitMqSubscriptionBinder = scope.ServiceProvider.GetRequiredService<IRabbitMQSubscriptionBinder>();
                
                await rabbitMqSubscriptionBinder.DoWork(stoppingToken);
            }
            catch (Exception ex)
            {
                // log error
                throw;
            }
        }
    }
}
