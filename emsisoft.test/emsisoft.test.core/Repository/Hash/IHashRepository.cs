using emsisoft.test.core.Application.Hash.Models;

namespace emsisoft.test.core.Repository.Hash
{
    public interface IHashRepository
    {
        Task CreateHashes(params string[] hash);
        Task<HashAggregate[]> GetHashAggregate();
    }
}
