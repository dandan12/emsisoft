using emsisoft.test.core.Application.Hash.Models;
using emsisoft.test.core.Repository.Hash;
using MediatR;

namespace emsisoft.test.core.Application.Hash.Queries
{
    public class GetHashAggregateQueryHandler : IRequestHandler<GetHashAggregateQuery, HashAggregate[]>
    {
        private readonly IHashRepository hashRepository;

        public GetHashAggregateQueryHandler(IHashRepository hashRepository)
        {
            this.hashRepository = hashRepository;
        }

        public Task<HashAggregate[]> Handle(GetHashAggregateQuery request, CancellationToken cancellationToken)
        {
            return hashRepository.GetHashAggregate();
        }
    }
}
