using WebBff.Aggregator.Models.Issues;

namespace WebBff.Aggregator.Models.GroupOfIssues;

public class GroupOfIssuesWithIssuesDto : GroupOfIssuesDto
{
    public IEnumerable<IssueDto> Issues { get; set; }
}