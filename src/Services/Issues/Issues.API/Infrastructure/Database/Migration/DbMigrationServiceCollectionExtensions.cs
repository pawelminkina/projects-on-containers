using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Issues.API.Infrastructure.Database.Migration
{
    public static class DbMigrationServiceCollectionExtensions
    {
        public static IServiceCollection AddDbMigration<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
            var dbContext = serviceProvider.GetRequiredService<TContext>();

            logger.LogInformation($"Migrating context associated with context {typeof(TContext).Name}");

            dbContext.Database.Migrate();

            return services;
        }
    }
}