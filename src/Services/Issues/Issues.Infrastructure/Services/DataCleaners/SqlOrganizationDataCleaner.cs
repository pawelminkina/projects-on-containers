using System.Linq;
using System.Threading.Tasks;
using Issues.Application.Common.Services.DataCleaners;
using Issues.Infrastructure.Database;

namespace Issues.Infrastructure.Services.DataCleaners;

public class SqlOrganizationDataCleaner : IOrganizationDataCleaner
{
    private readonly IssuesServiceDbContext _dbContext;

    public SqlOrganizationDataCleaner(IssuesServiceDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CleanAsync(string organizationId)
    {
        var typesToRemove = _dbContext.TypesOfGroupsOfIssues.Where(s => s.OrganizationId == organizationId);
        _dbContext.TypesOfGroupsOfIssues.RemoveRange(typesToRemove);

        var statusFlowsToRemove = _dbContext.StatusFlows.Where(s => s.OrganizationId == organizationId);
        _dbContext.StatusFlows.RemoveRange(statusFlowsToRemove);
    }
}