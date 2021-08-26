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
            return new List<TypeOfGroupOfIssues>()
            {
                new TypeOfGroupOfIssues()
                {
                    Id = "51e07f6e-9ac2-4765-8d18-0e4b5c87b475", IsArchived = false, Name = "First group",
                    OrganizationId = "pocOrgan-70cf-4a4d-bcd8-1241e3bcce0b"
                },
                new TypeOfGroupOfIssues()
                {
                    Id = "1864d3f4-db57-4c01-a20b-769c9d52cb87", IsArchived = false, Name = "Second group",
                    OrganizationId = "pocOrgan-70cf-4a4d-bcd8-1241e3bcce0b"
                },
                new TypeOfGroupOfIssues()
                {
                    Id = "120a6b25-19b1-42a9-ab3e-f0955a510a66", IsArchived = false, Name = "Third group",
                    OrganizationId = "pocOrgan-70cf-4a4d-bcd8-1241e3bcce0b"
                }
            };
        }
    }
}