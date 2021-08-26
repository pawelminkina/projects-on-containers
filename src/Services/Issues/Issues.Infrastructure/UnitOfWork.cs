using System.Threading;
using System.Threading.Tasks;
using Architecture.DDD.Repositories;
using Issues.Infrastructure.Database;
using Issues.Infrastructure.Processing;

namespace Issues.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IssuesServiceDbContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(IssuesServiceDbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher)
        {
            _dbContext = dbContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}