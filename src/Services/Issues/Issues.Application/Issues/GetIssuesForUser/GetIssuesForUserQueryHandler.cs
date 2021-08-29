using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.GetIssuesForUser
{
    public class GetIssuesForUserQueryHandler : IRequestHandler<GetIssuesForUserQuery, IEnumerable<Issue>>
    {
        private readonly IIssueRepository _issueRepository;

        public GetIssuesForUserQueryHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }
        public async Task<IEnumerable<Issue>> Handle(GetIssuesForUserQuery request, CancellationToken cancellationToken)
        {
            return await _issueRepository.GetIssueReferencesForUserAsync(request.UserId);
        }
    }
}