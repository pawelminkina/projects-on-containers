using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Users.Core.CQRS.Users.Commands.CreateUser;
using Users.Core.CQRS.Users.Commands.DeleteUser;
using Users.Core.CQRS.Users.Queries.CheckIdAndPasswordMatches;
using Users.Core.CQRS.Users.Queries.GetUserById;
using Users.Core.CQRS.Users.Queries.GetUserByUsername;
using Users.Core.CQRS.Users.Queries.GetUsersForOrganization;
using Users.API.Protos;
using Users.Core.CQRS.Users.Commands.ChangePassword;
using Users.Core.CQRS.Users.Queries.CheckEmailAvailability;

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
            var user = await _mediator.Send(new CreateUserCommand(request.Email, request.OrganizationId, request.Password, request.Fullname));
            return new CreateUserResponse() {UserId = user.Id};
        }

        public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteUserCommand(request.UserId));
            return new DeleteUserResponse();
        }

        public override async Task<GetUserResponse> GetUserById(GetUserByIdRequest request, ServerCallContext context)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(request.UserId));
            return new GetUserResponse() {User = MapToUser(user)};
        }

        public override async Task<GetUserResponse> GetUserByUsername(GetUserByUsernameRequest request, ServerCallContext context)
        {
            var user = await _mediator.Send(new GetUserByUsernameQuery(request.Username));
            return new GetUserResponse() { User = MapToUser(user) };
        }

        public override async Task<GetUsersForOrganizationResponse> GetUsersForOrganization(GetUsersForOrganizationRequest request, ServerCallContext context)
        {
            var users = await _mediator.Send(new GetUsersForOrganizationQuery(request.OrganizationId));
            return new GetUsersForOrganizationResponse()
            {
                Users = { users.Select(MapToUser) }
            };
        }

        public override async Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ChangePasswordCommand(request.UserId, request.OldPassword, request.NewPassword));
            return new ChangePasswordResponse();
        }

        private User MapToUser(Users.Core.Domain.User user)
        {
            return new User()
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
