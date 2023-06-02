using emsisoft.test.infra.rabbitmq;
using emsisoft.test.infra.sql;
using emsisoft.test.worker.queue;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;

        services
            .RegisterRabbitMQInfrastructure(config)
            .RegisterSqlInfrastructure(config);

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
