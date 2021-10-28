using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.Domain.Issues;
using Issues.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Issues.Infrastructure.Repositories
{
    public class SqlIssueRepository : IIssueRepository
    {
        private readonly IssuesServiceDbContext _dbContext;

        public SqlIssueRepository(IssuesServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Issue> GetIssueByIdAsync(string id)
        {
            return await _dbContext.Issues.Include(s => s.Content).Include(s => s.GroupOfIssue).ThenInclude(d=>d.TypeOfGroup).Include(s => s.TypeOfIssue).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Issue>> GetIssueReferencesForUserAsync(string userId)
        {
            return _dbContext.Issues.Where(s => s.CreatingUserId == userId).Include(d=>d.TypeOfIssue);
        }
    }
}