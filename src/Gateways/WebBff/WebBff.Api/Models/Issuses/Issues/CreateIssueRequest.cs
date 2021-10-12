namespace WebBff.Api.Models.Issuses.Issues
{
    public class CreateIssueRequest
    {
        public string Name { get; set; }
        public string GroupId { get; set; }
        public string TextContent { get; set; }
        public string TypeOfIssueId { get; set; }
    }
}