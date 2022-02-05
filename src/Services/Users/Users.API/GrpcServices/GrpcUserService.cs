using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using User.Core.CQS.Users.Commands.CreateUser;
using User.Core.CQS.Users.Commands.DeleteUser;
using User.Core.CQS.Users.Queries.CheckIdAndPasswordMatches;
using User.Core.CQS.Users.Queries.GetUserById;
using User.Core.CQS.Users.Queries.GetUserByUsername;
using User.Core.CQS.Users.Queries.GetUsersForOrganization;
using Users.API.Protos;

namespace Users.API.GrpcServices
{
    [Authorize]
    public class GrpcUserService : Protos.UserService.UserServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcUserService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<CheckEmailvAilabilityResponse> CheckEmailAvailability(CheckEmailAvailabilityRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, ""));
        }

        public override async Task<CheckIdAndPasswordMatchesResponse> CheckIdAndPasswordMatches(CheckIdAndPasswordMatchesRequest request, ServerCallContext context)
        {
            var passwordMatches = await _mediator.Send(new CheckIdAndPasswordMatchesQuery(request.UserId, request.Password));
            return new CheckIdAndPasswordMatchesResponse()
            {
                PasswordMatches = passwordMatches
            };
        }

        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var user = await _mediator.Send(new CreateUserCommand(request.Email, request.OrganizationId, request.Password));
            return new CreateUserResponse() {UserId = user.Id};
        }

        public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteUserCommand(request.UserId));
            return new DeleteUserResponse();
        }

        public override async Task<UserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId));
            return MapToUserResponse(user);
        }

        public override async Task<UserResponse> GetUserByUsername(GetUserByUsernameRequest request, ServerCallContext context)
        {
            var user = await _mediator.Send(new GetUserByUsernameQuery(request.Username));
            return MapToUserResponse(user);
        }

        public override async Task<GetUsersForOrganizationResponse> GetUsersForOrganization(GetUsersForOrganizationRequest request, ServerCallContext context)
        {
            var users = await _mediator.Send(new GetUsersForOrganizationQuery(request.OrganizationId));
            return new GetUsersForOrganizationResponse()
            {
                Users = { users.Select(MapToUserResponse) }
            };
        }

        private UserResponse MapToUserResponse(User.Core.Domain.User user)
        {
            return new UserResponse()
            {
                Id = user.Id,
                Username = user.UserName,
                Fullname = user.Fullname,
                OrganizationId = user.Organization.Id,
                CreatedDate = Timestamp.FromDateTimeOffset(user.TimeOfCreation)
            };
        }
    }
}
