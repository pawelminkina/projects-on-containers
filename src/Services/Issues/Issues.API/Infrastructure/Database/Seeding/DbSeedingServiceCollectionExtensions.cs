using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Issues.API.Infrastructure.Database.Seeding
{
    public static class DbSeedingServiceCollectionExtensions
    {
        public static IServiceCollection AddDbSeeding<TContext, TContextSeeder>(this IServiceCollection services)
            where TContext : DbContext
            where TContextSeeder : class, IDbSeeder<TContext>
        {
            services.AddScoped<TContextSeeder>();

            var serviceProvider = services.BuildServiceProvider();

            var dbSeeder = serviceProvider.GetRequiredService<TContextSeeder>();
            dbSeeder.SeedAsync().Wait();

            return services;
        }
    }
}