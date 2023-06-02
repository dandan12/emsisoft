using emsisoft.test.core.Application.Hash.Models;
using emsisoft.test.core.Repository.Hash;
using emsisoft.test.infra.sql.Entities.Hash;
using Microsoft.EntityFrameworkCore;

namespace emsisoft.test.infra.sql.Repositories
{
    public class HashRepository : IHashRepository
    {
        private readonly DataContext context;

        public HashRepository(DataContext context)
        {
            this.context = context;
        }

        public Task<HashAggregate[]> GetHashAggregate()
        {
            return context.Hashes
                .GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month, x.CreatedDate.Day })
                .Select(x => new HashAggregate
                {
                    Date = new(x.Key.Year, x.Key.Month, x.Key.Day),
                    Count = x.Count()
                })
                .ToArrayAsync();
        }

        public async Task CreateHashes(params string[] hash)
        {
            var entities = hash.Select(x => new HashEntity
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Hash = x
            }).ToArray();

            await context.Hashes.AddRangeAsync(entities);
        }
    }
}
