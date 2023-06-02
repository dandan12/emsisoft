using emsisoft.test.core.Repository.Hash;
using emsisoft.test.core.Repository.RabbitMQ;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emsisoft.test.worker.queue
{
    public interface IWorkerExecution
    {
        Task DoWork(CancellationToken cancellationToken);
    }
    public class WorkerExecution : IWorkerExecution
    {
        private readonly IRabbitMQRepository rabbitMQRepository;

        public WorkerExecution(IRabbitMQRepository rabbitMQRepository)
        {
            this.rabbitMQRepository = rabbitMQRepository;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            await rabbitMQRepository.ConsumeMessages();
        }
    }
}
