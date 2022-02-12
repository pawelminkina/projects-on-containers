using WebBff.Aggregator.Models.GroupOfIssues;

namespace WebBff.Aggregator.Models.TypeOfGroupOfIssues
{
    public class TypeOfGroupOfIssuesWithGroupsDto : TypeOfGroupOfIssuesDto
    {
        public IEnumerable<GroupOfIssuesDto> GroupsOfIssues { get; set; }
    }
}
