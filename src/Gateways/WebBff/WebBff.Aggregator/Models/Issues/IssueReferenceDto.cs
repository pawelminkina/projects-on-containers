namespace WebBff.Aggregator.Models.Issues
{
    public class IssueReferenceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CreatingUserId { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }
        public string GroupId { get; set; }
        public bool IsDeleted { get; set; }
        public string StatusName { get; set; }
    }
}
