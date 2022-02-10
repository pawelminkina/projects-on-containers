using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auth.Core.Models;
using Microsoft.Extensions.Logging;
using Users.API.Protos;

namespace Auth.Core.Services
{
    public class GrpcLoginService : ILoginService<POCUser>, IUserService<POCUser>
    {
        private readonly UserService.UserServiceClient _userServiceClient;
        private readonly ILogger<GrpcLoginService> _logger;

        public GrpcLoginService(UserService.UserServiceClient userServiceClient, ILogger<GrpcLoginService> logger)
        {
            _userServiceClient = userServiceClient;
            _logger = logger;
        }

        public async Task<bool> ValidateCredentialsAsync(POCUser user, string password)
        {
            var validationResponse = await _userServiceClient.CheckIdAndPasswordMatchesAsync(new CheckIdAndPasswordMatchesRequest()
            {
                UserId = user.Id,
                Password = password
            });

            return validationResponse.PasswordMatches;
        }

        public async Task<POCUser> FindByUsernameAsync(string username)
        {
            var userResponse = await _userServiceClient.GetUserByUsernameAsync(new GetUserByUsernameRequest() { Username = username });

            if (userResponse is null)
                return null;

            return new POCUser(userResponse.User.Id, userResponse.User.OrganizationId, userResponse.User.Username, userResponse.User.CreatedDate.ToDateTimeOffset())
            {
                Id = userResponse.User.Id,
                Username = userResponse.User.Username,
                OrganizationId = userResponse.User.OrganizationId
            };
        }

        public async Task<POCUser> GetUserByIdAsync(string id)
        {
            var userResponse = await _userServiceClient.GetUserByIdAsync(new GetUserByIdRequest() { UserId = id });

            if (userResponse is null)
                return null;

            return new POCUser(userResponse.User.Id, userResponse.User.OrganizationId, userResponse.User.Username, userResponse.User.CreatedDate.ToDateTimeOffset())
            {
                Id = userResponse.User.Id,
                Username = userResponse.User.Username,
                OrganizationId = userResponse.User.OrganizationId
            };
        }
    }
}
