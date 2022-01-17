﻿using System.Collections.Generic;
using System.Linq;
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
            var issues = await _issueRepository.GetIssueReferencesForUserAsync(request.UserId);
            return issues.Where(s => !s.IsDeleted && !s.GroupOfIssue.IsDeleted);
        }
    }
}