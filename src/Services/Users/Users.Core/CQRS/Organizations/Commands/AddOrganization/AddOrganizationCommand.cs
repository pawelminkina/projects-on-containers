using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.Core.IntegrationEvents.Events;
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace Users.Core.CQRS.Organizations.Commands.AddOrganization
{
    public sealed class AddOrganizationCommand : IRequest<AddOrganizationResponse>
    {
        public AddOrganizationCommand(string organizationName)
        {
            OrganizationName = organizationName;
        }

        public string OrganizationName { get; }
    }

    public sealed class AddOrganizationCommandHandler : IRequestHandler<AddOrganizationCommand, AddOrganizationResponse>
    {
        private readonly IPasswordHasher<UserDAO> _passwordHasher;
        private readonly UserServiceDbContext _dbContext;
        private readonly IEventBus _eventBus;

        public AddOrganizationCommandHandler(IPasswordHasher<UserDAO> passwordHasher, UserServiceDbContext dbContext, IEventBus eventBus)
        {
            _passwordHasher = passwordHasher;
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        public async Task<AddOrganizationResponse> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organizationId = Guid.NewGuid().ToString();
            var defaultUserPassword = Guid.NewGuid().ToString();
            var defaultUser = GetDefaultUser(request.OrganizationName, defaultUserPassword);

            await _dbContext.Organizations.AddAsync(new OrganizationDAO()
            {
                Id = organizationId,
                Name = request.OrganizationName,
                TimeOfCreationUtc = DateTime.UtcNow,
                Users = new List<UserDAO>()
                {
                    defaultUser
                }
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _eventBus.Publish(new OrganizationCreatedIntegrationEvent(organizationId));


            return new AddOrganizationResponse()
            {
                DefaultUserName = defaultUser.UserName,
                DefaultUserPassword = defaultUserPassword,
                OrganizationId = organizationId
            };
        }

        private UserDAO GetDefaultUser(string organizationName, string userPassword)
        {
            var username = organizationName.Replace(" ", string.Empty);
            var user = new UserDAO()
            {
                Id = Guid.NewGuid().ToString(),
                Email = $"default@{username}.com",
                UserName = $"default@{username}.com",
                NormalizedEmail = $"default@{username}.com".ToUpper(),
                NormalizedUserName = $"default@{username}.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                TimeOfCreationUtc = DateTime.UtcNow,
                Fullname = "Default User",
                PasswordHash = userPassword
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, userPassword);
            return user;
        }

    }
}
