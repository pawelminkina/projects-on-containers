using WebBff.Aggregator.Models.Issues;

namespace WebBff.Aggregator.Models.GroupOfIssues;

public class GroupOfIssuesWithIssuesDto : GroupOfIssuesDto
{
    public GroupOfIssuesWithIssuesDto(GroupOfIssuesDto dto)
    {
        Id = dto.Id;
        Name= dto.Name;
        TypeOfGroupId = dto.TypeOfGroupId;
        ShortName = dto.ShortName;
        IsInTrash = dto.IsInTrash;
        TimeOfDelete = dto.TimeOfDelete;
    }

    public GroupOfIssuesWithIssuesDto()
    {
        
    }
    public IEnumerable<IssueReferenceDto> Issues { get; set; }
}