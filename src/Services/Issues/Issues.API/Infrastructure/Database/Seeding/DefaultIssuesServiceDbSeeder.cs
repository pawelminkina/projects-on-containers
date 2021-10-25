using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using Issues.Domain.StatusesFlow;
using Issues.Domain.TypesOfIssues;
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
            if (_dbContext.TypesOfGroupsOfIssues.Any() || _dbContext.Issues.Any() || _dbContext.Statuses.Any() || _dbContext.TypesOfIssues.Any() || _dbContext.GroupsOfIssues.Any())
            {
                _logger.LogDebug($"Database has items. Seeder {this.GetType().Name} was not applied.");
            }
            else
            {
                _logger.LogInformation($"Seeding database with {this.GetType().Name} seeder.");
                await SeedIssuesDb();
                await _dbContext.SaveChangesAsync();
            }
        }

        private async Task SeedIssuesDb()
        {
            var typeOfIssue = new TypeOfIssue("MOCKEDORGANIZATION", "SOMENAME");
            var firstStatus = new Status("someStatusName", "MOCKEDORGANIZATION");
            var secondStatus = new Status("someStatusName", "MOCKEDORGANIZATION");
            var type = new TypeOfGroupOfIssues("MOCKEDORGANIZATION", "SOMENAME");
            var flow = new StatusFlow("SOMENAME", "MOCKEDORGANIZATION");
            type.SetIsDefaultToTrue();
            var firstGroup = type.AddNewGroupOfIssues("nameOfGroup", "SHN");
            var secondGroup = type.AddNewGroupOfIssues("nameOfGroupTwo", "SHN2");
            var firstIssue = firstGroup.AddIssue("firstIssue", "MOCKEDUSER", "someTextContent", typeOfIssue.Id, firstStatus.Id);
            var secondIssue = firstGroup.AddIssue("secondIssue", "MOCKEDUSER", "someTextContent2", typeOfIssue.Id, secondStatus.Id);
            var typeInGroup = typeOfIssue.AddNewTypeOfGroupToCollection(type.Id, flow.Id);
            _dbContext.TypesOfGroupsOfIssues.Add(type);
            _dbContext.TypesOfIssues.Add(typeOfIssue);
            _dbContext.Statuses.AddRange(new[]{ firstStatus, secondStatus});
            _dbContext.GroupsOfIssues.AddRange(new []{firstGroup, secondGroup});
            _dbContext.Issues.AddRange(new []{firstIssue, secondIssue});
            _dbContext.TypesOfIssueInTypeOfGroups.AddRange(new[] { typeInGroup });
        }
    }
}