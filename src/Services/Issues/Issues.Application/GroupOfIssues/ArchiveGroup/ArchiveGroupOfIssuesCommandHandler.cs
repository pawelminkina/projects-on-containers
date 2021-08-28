using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.GroupsOfIssues;
using MediatR;

namespace Issues.Application.GroupOfIssues.ArchiveGroup
{
    public class ArchiveGroupOfIssuesCommandHandler : IRequestHandler<ArchiveGroupOfIssuesCommand>
    {
        private readonly IGroupOfIssuesRepository _repository;

        public ArchiveGroupOfIssuesCommandHandler(IGroupOfIssuesRepository repository)
        {
            _repository = repository;
        }
        public async Task<Unit> Handle(ArchiveGroupOfIssuesCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}