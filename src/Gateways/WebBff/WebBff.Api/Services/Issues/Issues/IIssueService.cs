using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBff.Api.Models.Issuses.Issues;

namespace WebBff.Api.Services.Issues.Issues
{
    public interface IIssueService
    {
        Task<string> CreateIssueAsync(IssueDto issue);
        Task DeleteIssueAsync(string id);
        Task<IEnumerable<IssueDto>> GetIssuesForGroupAsync(string groupId);
        Task<IEnumerable<IssueDto>> GetIssuesForUserAsync(string userId);
        Task RenameIssueAsync(string userId);
        Task<IssueDto> GetIssueWithContentAsync(string issueId); //TODO can be issueDTo with content
        Task UpdateIssueContentAsync(string textContent, string issueId);
        Task UpdateIssueStatusAsync(string issueId, string statusId);
    }
}