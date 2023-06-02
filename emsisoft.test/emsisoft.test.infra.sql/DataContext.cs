using emsisoft.test.core.Abstractions;
using emsisoft.test.infra.sql.Entities.Hash;
using Microsoft.EntityFrameworkCore;

namespace emsisoft.test.infra.sql
{
    public class DataContext : DbContext, IDbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<HashEntity> Hashes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HashEntity>()
                .HasKey(x => x.Hash);

            modelBuilder.Entity<HashEntity>()
                .HasIndex(x => x.Hash);
        }
    }
}
