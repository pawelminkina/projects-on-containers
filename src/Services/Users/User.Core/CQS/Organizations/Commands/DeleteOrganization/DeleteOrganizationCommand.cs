using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBus.Abstraction;
using MediatR;
using User.Core.IntegrationEvents.Events;
using Users.DAL;

namespace User.Core.CQS.Organizations.Commands.DeleteOrganization
{
    public class DeleteOrganizationCommand : IRequest
    {
        public DeleteOrganizationCommand(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }

    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand>
    {
        private readonly UserServiceDbContext _userServiceDbContext;
        private readonly IEventBus _eventBus;

        public DeleteOrganizationCommandHandler(UserServiceDbContext userServiceDbContext, IEventBus eventBus)
        {
            _userServiceDbContext = userServiceDbContext;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organizationToRemove = await _userServiceDbContext.Organizations.FindAsync(request.OrganizationId);
            _userServiceDbContext.Organizations.Remove(organizationToRemove);
            await _userServiceDbContext.SaveChangesAsync(cancellationToken);

            _eventBus.Publish(new OrganizationDeletedIntegrationEvent(request.OrganizationId));

            return Unit.Value;
        }
    }
}
