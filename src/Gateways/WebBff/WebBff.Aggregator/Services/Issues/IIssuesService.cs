using WebBff.Aggregator.Models.Issues;

namespace WebBff.Aggregator.Services.Issues;

public interface IIssuesService
{
    Task<IEnumerable<IssueDto>> GetIssuesForUser(string userId);
    Task<IssueDto> GetIssueWithContent(string id);
    Task<string> CreateIssue(IssueForCreationDto dto);
    Task RenameIssue(RenameIssueDto dto);
    Task UpdateTextContentOfIssue(UpdateTextContentOfIssueDto dto);
    Task DeleteIssue(string id);
}