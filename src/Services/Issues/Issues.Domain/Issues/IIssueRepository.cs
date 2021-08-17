using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Domain.Issues
{
    public interface IIssueRepository
    {
        Task<Issue> GetIssueByIdAsync(string id);
        Task<IEnumerable<Issue>> GetIssuesForUserAsync(string userId);
    }
}
