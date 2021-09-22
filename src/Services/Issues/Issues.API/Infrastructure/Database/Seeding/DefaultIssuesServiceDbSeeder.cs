using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Infrastructure.Database;
using Microsoft.Extensions.Logging;

namespace Issues.API.Infrastructure.Database.Seeding
{
    public class DefaultIssuesServiceDbSeeder : IDbSeeder<IssuesServiceDbContext>
    {
        private readonly IssuesServiceDbContext _dbContext;
        private readonly ILogger<DefaultIssuesServiceDbSeeder> _logger;

        public DefaultIssuesServiceDbSeeder(IssuesServiceDbContext dbContext, ILogger<DefaultIssuesServiceDbSeeder> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            if (_dbContext.TypesOfGroupsOfIssues.Any())
            {
                _logger.LogDebug($"There is a type of group of issues in database. Seeder {this.GetType().Name} was not applied.");
            }
            else
            {
                _logger.LogInformation($"Seeding database with {this.GetType().Name} seeder.");

                _dbContext.TypesOfGroupsOfIssues.AddRange(GetTypesOfGroupsOfIssues());

                await _dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<TypeOfGroupOfIssues> GetTypesOfGroupsOfIssues()
        {
            return new List<TypeOfGroupOfIssues>();
        }
    }
}