using emsisoft.test.core.Abstractions;
using emsisoft.test.core.Application.Hash.Services;
using emsisoft.test.core.Application.Queues.Models;
using MediatR;

namespace emsisoft.test.core.Application.Hash.Commands
{
    public class CreateHashCommandHandler : IRequestHandler<CreateHashCommand, Unit>
    {
        private readonly IHashService hashService;
        private readonly IMessegeQueuePublisher messegeQueuePublisher;

        public CreateHashCommandHandler(IHashService hashService,
            IMessegeQueuePublisher messegeQueuePublisher)
        {
            this.hashService = hashService;
            this.messegeQueuePublisher = messegeQueuePublisher;
        }

        public async Task<Unit> Handle(CreateHashCommand request, CancellationToken cancellationToken)
        {
            var hashes = hashService.GenerateRandomSHA1Hash(40_000);
            foreach (var hash in hashes)
                await messegeQueuePublisher.AddMessage(QueueType.Hash, hash);

            return Unit.Value;
        }
    }
}
