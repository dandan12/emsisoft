namespace emsisoft.test.worker.queue
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider serviceProvider;
        private readonly IHostApplicationLifetime hostApplicationLifetime;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            this.serviceProvider = serviceProvider;
            this.hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    using var scope = serviceProvider.CreateScope();
                    var workerSerivce = scope.ServiceProvider.GetService<IWorkerExecution>();
                    
                    await workerSerivce.DoWork(stoppingToken);

                    await Task.Delay(1000, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                // log error
                hostApplicationLifetime.StopApplication();
            }
          
        }
    }
}