using emsisoft.test.core.Abstractions;
using emsisoft.test.infra.rabbitmq.Configuration;
using emsisoft.test.infra.rabbitmq.Consumers;
using emsisoft.test.infra.rabbitmq.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emsisoft.test.infra.rabbitmq
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterRabbitMQInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqConfig = configuration.GetSection("RabbitMQConfig").Get<RabbitMqConfig>();

            services.AddSingleton(rabbitMqConfig);
            foreach (var queue in rabbitMqConfig.Queues) 
                services.AddSingleton(new QueueDefinition(queue.Type, queue.Name));

            services.AddScoped<IBasicConsumer, HashQueueConsumer>();

            services.AddScoped<IConnectionFactory>(x => new ConnectionFactory() { HostName = rabbitMqConfig.HostName });
            services.AddScoped<IMessegeQueuePublisher, RabbitMqPublisher>();

            return services;
        }
    }
}
