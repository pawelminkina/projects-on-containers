using System;

namespace WebBff.Api.Models.Issuses.Issues
{
    public class IssueDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StatusId { get; set; }
        public string CreatingUserId { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }
        public string TypeOfIssueId { get; set; }
    }
    
}