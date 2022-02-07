using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Abstraction;
using MediatR;
using Users.Core.IntegrationEvents.Events;
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace Users.Core.CQS.Organizations.Commands.AddOrganization
{
    public sealed class AddOrganizationCommand : IRequest<string>
    {
        public AddOrganizationCommand(string organizationName)
        {
            OrganizationName = organizationName;
        }

        public string OrganizationName { get; }
    }

    public sealed class AddOrganizationCommandHandler : IRequestHandler<AddOrganizationCommand, string>
    {
        private readonly UserServiceDbContext _dbContext;
        private readonly IEventBus _eventBus;

        public AddOrganizationCommandHandler(UserServiceDbContext dbContext, IEventBus eventBus)
        {
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        public async Task<string> Handle(AddOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organizationId = Guid.NewGuid().ToString();

            await _dbContext.Organizations.AddAsync(new OrganizationDAO()
            {
                Id = organizationId,
                Enabled = true,
                Name = request.OrganizationName,
                TimeOfCreationUtc = DateTime.UtcNow
            }, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _eventBus.Publish(new OrganizationCreatedIntegrationEvent(organizationId));

            return organizationId;
        }
    }
}
