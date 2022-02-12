namespace WebBff.Aggregator.Models.User
{
    public class ChangePasswordDto
    {
        public string Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
