namespace WebBff.Aggregator.Models.User;

public class UserForCreationDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string OrganizationId { get; set; }
    public string Fullname { get; set; }
}