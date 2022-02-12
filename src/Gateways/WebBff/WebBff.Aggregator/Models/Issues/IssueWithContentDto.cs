namespace WebBff.Aggregator.Models.Issues
{
    public class IssueWithContentDto : IssueReferenceDto
    {
        public IssueWithContentDto(IssueReferenceDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            CreatingUserId = dto.CreatingUserId;
            TimeOfCreation = dto.TimeOfCreation;
            GroupId = dto.GroupId;
            IsDeleted = dto.IsDeleted;
            StatusName = dto.StatusName;
        }

        public IssueWithContentDto()
        {
            
        }

        public string TextContent { get; set; }
    }
}
