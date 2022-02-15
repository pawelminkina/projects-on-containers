using WebBff.Aggregator.Models.GroupOfIssues;

namespace WebBff.Aggregator.Services.GroupOfIssues;

public interface IGroupOfIssuesService
{
    Task<IEnumerable<GroupOfIssuesDto>> GetGroupsOfIssues();
    Task<GroupOfIssuesDto> GetGroupOfIssues(string id);
    Task<string> CreateGroupOfIssues(GroupOfIssuesForCreationDto dto);
    Task RenameGroupOfIssues(string id, string newName);
    Task ChangeShortNameForGroupOfIssues(string id, string newShortName);
    Task DeleteGroupOfIssues(string id);
}