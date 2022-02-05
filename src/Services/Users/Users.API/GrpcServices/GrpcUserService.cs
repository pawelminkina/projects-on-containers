using Grpc.Core;
using Users.API.Protos;

namespace Users.API.GrpcServices
{
    public class GrpcUserService : Protos.UserService.UserServiceBase
    {
        public override async Task<CheckEmailvAilabilityResponse> CheckEmailAvailability(CheckEmailAvailabilityRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<CheckIdAndPasswordMatchesResponse> CheckIdAndPasswordMatches(CheckIdAndPasswordMatchesRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<UserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<UserResponse> GetUserByUsername(GetUserByUsernameRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<GetUsersForOrganizationResponse> GetUsersForOrganization(GetUsersForOrganizationRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }
    }
}
