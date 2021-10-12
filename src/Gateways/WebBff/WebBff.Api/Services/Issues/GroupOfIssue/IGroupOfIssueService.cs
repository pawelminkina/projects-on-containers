using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBff.Api.Models.Issuses.GroupOfIssue;

namespace WebBff.Api.Services.Issues.GroupOfIssue
{
    public interface IGroupOfIssueService
    {
        Task<IEnumerable<GroupOfIssueDto>> GetGroupsOfIssuesAsync();
        Task<GroupOfIssueDto> GetGroupOfIssueAsync(string id);
        Task<string> CreateGroupOfIssueAsync(CreateGroupOfIssuesRequest request);
        Task RenameGroupOfIssueAsync(string id, string newName);
        Task DeleteGroupOfIssueAsync(string id);
    }
}