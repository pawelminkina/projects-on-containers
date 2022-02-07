using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Database
{
    public class DefaultUserServiceDbSeeder
    {
        private UserServiceDbContext _dbContext;
        private ILogger<DefaultUserServiceDbSeeder> _logger;
        public async Task SeedAsync(UserServiceDbContext dbContext, IUserSeedItemService seedItemService, ILogger<DefaultUserServiceDbSeeder> logger, bool clearDatabaseFirst = false)
        {
            _dbContext = dbContext;
            _logger = logger;

            var policy = CreatePolicy( nameof(UserServiceDbContext));
            await policy.ExecuteAsync(async () =>
            {
                using (_dbContext)
                {
                    _dbContext.Database.Migrate();

                    if (clearDatabaseFirst)
                    {
                        _logger.LogInformation("Removing items from Db");
                        ClearDb();
                    }

                    if (_dbContext.Organizations.Any())
                    {
                        _logger.LogInformation($"There is organization it database. Seeder {this.GetType().Name} was not applied.");
                    }
                    else
                    {
                        _logger.LogInformation($"Seeding database with {this.GetType().Name} seeder.");
                        _dbContext.Organizations.AddRange(seedItemService.GetOrganizationsFromSeed());
                        await _dbContext.SaveChangesAsync();
                    }
                }
            });
        }

        private void ClearDb()
        {
            _dbContext.Organizations.RemoveRange(_dbContext.Organizations);
            _dbContext.UserClaims.RemoveRange(_dbContext.UserClaims);
            _dbContext.UserLogins.RemoveRange(_dbContext.UserLogins);
            _dbContext.UserRoles.RemoveRange(_dbContext.UserRoles);
            _dbContext.UserTokens.RemoveRange(_dbContext.UserTokens);
            _dbContext.Users.RemoveRange(_dbContext.Users);
            _dbContext.RoleClaims.RemoveRange(_dbContext.RoleClaims);
            _dbContext.Roles.RemoveRange(_dbContext.Roles);

            _dbContext.SaveChanges();
        }

        private AsyncRetryPolicy CreatePolicy(string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        _logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
    }
}
