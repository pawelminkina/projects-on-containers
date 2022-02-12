using WebBff.Aggregator.Models.GroupOfIssues;

namespace WebBff.Aggregator.Services.GroupOfIssues;

public interface IGroupOfIssuesService
{
    Task<IEnumerable<GroupOfIssuesDto>> GetGroupsOfIssues();
    Task<GroupOfIssuesDto> GetGroupOfIssues(string id);
    Task<string> CreateGroupOfIssues(GroupOfIssuesForCreationDto dto);
    Task RenameGroupOfIssues(RenameGroupOfIssuesDto dto);
    Task ChangeShortNameForGroupOfIssues(ChangeShortNameForGroupOfIssuesDto dto);
    Task DeleteGroupOfIssues(string id);
}