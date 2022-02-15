using System.Diagnostics;
using Users.API.Protos;
using WebBff.Aggregator.Models.User;

namespace WebBff.Aggregator.Services.User;

public class GrpcUsersService : IUsersService
{
    private readonly UserService.UserServiceClient _grpcClient;

    public GrpcUsersService(UserService.UserServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }
    public async Task<IEnumerable<UserDto>> GetUsers(string organizationId)
    {
        var response = await _grpcClient.GetUsersForOrganizationAsync(new GetUsersForOrganizationRequest() {OrganizationId = organizationId});
        return response.Users.Select(MapToDto);
    }

    public async Task<UserDto> GetUserById(string userId)
    {
        var response = await _grpcClient.GetUserByIdAsync(new GetUserByIdRequest() {UserId = userId});
        return MapToDto(response.User);
    }

    public async Task<UserDto> GetUserByUsername(string username)
    {
        var response = await _grpcClient.GetUserByUsernameAsync(new GetUserByUsernameRequest() {Username = username});
        return MapToDto(response.User);
    }

    public async Task<string> CreateUser(UserForCreationDto dto, string organizationId)
    {
        var response = await _grpcClient.CreateUserAsync(new CreateUserRequest() {Email = dto.Email, Fullname = dto.Fullname, OrganizationId = organizationId, Password = dto.Password});
        return response.UserId;
    }

    public async Task DeleteUser(string userId)
    {
        await _grpcClient.DeleteUserAsync(new DeleteUserRequest() {UserId = userId});
    }

    public async Task<bool> CheckEmailAvailability(string email)
    {
        var response = await _grpcClient.CheckEmailAvailabilityAsync(new CheckEmailAvailabilityRequest() {Email = email});
        return response.IsAvailable;
    }

    public async Task ChangePassword(ChangePasswordDto dto)
    {
        await _grpcClient.ChangePasswordAsync(new ChangePasswordRequest() {NewPassword = dto.NewPassword, OldPassword = dto.OldPassword, UserId = dto.UserId});
    }

    private UserDto MapToDto(global::Users.API.Protos.User user)
    {
        return new UserDto()
        {
            CreatedDate = user.CreatedDate.ToDateTimeOffset(),
            Fullname = user.Fullname,
            Id = user.Id,
            OrganizationId = user.OrganizationId,
            Username = user.Username
        };
    }
}