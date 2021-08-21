using System.Threading.Tasks;
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
        public Task SeedAsync()
        {
            //TODO There should be seeding
            return Task.CompletedTask;;
        }
    }
}