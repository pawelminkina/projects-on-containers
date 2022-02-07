using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Users.Core.CQS.Users.Commands.CreateUser;
using Users.Core.CQS.Users.Commands.DeleteUser;
using Users.Core.CQS.Users.Queries.CheckIdAndPasswordMatches;
using Users.Core.CQS.Users.Queries.GetUserById;
using Users.Core.CQS.Users.Queries.GetUserByUsername;
using Users.Core.CQS.Users.Queries.GetUsersForOrganization;
using Users.API.Protos;
using Users.Core.CQS.Users.Queries.CheckEmailAvailability;

namespace Users.API.GrpcServices
{
    public class GrpcUserService : Protos.UserService.UserServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcUserService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<CheckEmailvAilabilityResponse> CheckEmailAvailability(CheckEmailAvailabilityRequest request, ServerCallContext context)
        {
            var isAvailable = await _mediator.Send(new CheckEmailAvailabilityQuery(request.Email));
            return new CheckEmailvAilabilityResponse() {IsAvailable = isAvailable};
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

        private UserResponse MapToUserResponse(Users.Core.Domain.User user)
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
