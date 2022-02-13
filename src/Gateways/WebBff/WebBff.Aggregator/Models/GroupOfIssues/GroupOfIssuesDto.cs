namespace WebBff.Aggregator.Models.GroupOfIssues
{
    public class GroupOfIssuesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TypeOfGroupId { get; set; }
        public string ShortName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset? TimeOfDelete { get; set; }
    }
}
