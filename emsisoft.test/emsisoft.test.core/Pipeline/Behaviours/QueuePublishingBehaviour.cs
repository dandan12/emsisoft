using emsisoft.test.core.Abstractions;
using MediatR;

namespace emsisoft.test.core.Pipeline.Behaviours
{
    public class QueuePublishingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDbContext dbContext;
        private readonly IMessegeQueuePublisher messegeQueuePublisher;

        public QueuePublishingBehaviour(IDbContext dbContext,
            IMessegeQueuePublisher messegeQueuePublisher)
        {
            this.dbContext = dbContext;
            this.messegeQueuePublisher = messegeQueuePublisher;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var result = await next();
            await dbContext.SaveChangesAsync(cancellationToken);
            await messegeQueuePublisher.TryPublishMessages();

            return result;
        }
    }
}
