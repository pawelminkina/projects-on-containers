using WebBff.Aggregator.Models.Issues;

namespace WebBff.Aggregator.Services.Issues;

public interface IIssuesService
{
    Task<IEnumerable<IssueReferenceDto>> GetIssuesForUser(string userId);
    Task<IEnumerable<IssueReferenceDto>> GetIssuesForGroup(string groupId);
    Task<IssueWithContentDto> GetIssueWithContent(string id);
    Task<string> CreateIssue(IssueForCreationDto dto);
    Task RenameIssue(string id, string newName);
    Task UpdateTextContentOfIssue(string id, string textContent);
    Task DeleteIssue(string id);
    Task ChangeStatusOfIssue(string issueId, string newStatusInFlowId);
}