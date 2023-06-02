using emsisoft.test.core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace emsisoft.test.infra.sql
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterSqlInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EmsisoftTestConnectionString"));
            });

            services.AddScoped<IDbContext>((provider) => provider.GetService<DataContext>());
            return services;
        }
    }
}