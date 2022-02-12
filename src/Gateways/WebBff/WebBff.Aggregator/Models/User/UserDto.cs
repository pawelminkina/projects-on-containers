namespace WebBff.Aggregator.Models.User;

public class UserDto
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Fullname { get; set; }
    public string OrganizationId { get; set; }
    public DateTimeOffset CreatedDate { get; set; }

}