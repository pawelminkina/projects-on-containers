using WebBff.Aggregator.Models.User;

namespace WebBff.Aggregator.Services.User
{
    public interface IUsersService
    {
        Task<IEnumerable<UserDto>> GetUsers(string organizationId);
        Task<UserDto> GetUserById(string userId);
        Task<UserDto> GetUserByUsername(string username);
        Task<string> CreateUser(UserForCreationDto dto);
        Task DeleteUser(string userId);
        Task<bool> CheckEmailAvailability(string email);
        Task ChangePassword(ChangePasswordDto dto);
    }
}
