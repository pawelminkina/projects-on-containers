using System.Linq;
using System.Threading.Tasks;
using Architecture.DDD;
using Issues.Infrastructure.Database;
using MediatR;

namespace Issues.Infrastructure.Processing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IssuesServiceDbContext _dbContext;

        public DomainEventsDispatcher(IMediator mediator, IssuesServiceDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task DispatchEventsAsync()
        {
            //Gets all entities with uncommited events
            var domainEntities = _dbContext.ChangeTracker
                .Entries<EntityBase>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            //Lists all uncommited domain events
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            //Publish all domain events
            var tasks = domainEvents.Select(async domainEvent =>
            {
                await _mediator.Publish(domainEvent);
            });

            await Task.WhenAll(tasks);
        }
    }
}