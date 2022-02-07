using Microsoft.AspNetCore.Identity;
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace Users.API.Infrastructure.Database
{
    public class DefaultUserServiceDbSeeder
    {
        private readonly UserServiceDbContext _dbContext;
        private readonly IUserSeedItemService _seedItemService;
        private readonly ILogger<DefaultUserServiceDbSeeder> _logger;

        public DefaultUserServiceDbSeeder(UserServiceDbContext dbContext, IUserSeedItemService seedItemService, ILogger<DefaultUserServiceDbSeeder> logger)
        {
            _dbContext = dbContext;
            _seedItemService = seedItemService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            if (_dbContext.Organizations.Any())
            {
                _logger.LogInformation($"There is organization it database. Seeder {this.GetType().Name} was not applied.");
            }
            else
            {
                _logger.LogInformation($"Seeding database with {this.GetType().Name} seeder.");
                _dbContext.Organizations.AddRange(_seedItemService.GetOrganizationsFromSeed());
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
