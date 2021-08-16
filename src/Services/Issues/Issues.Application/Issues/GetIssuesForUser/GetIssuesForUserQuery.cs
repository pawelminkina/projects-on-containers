using System.Collections;
using System.Collections.Generic;
using Issues.Domain.Issues;
using MediatR;

namespace Issues.Application.Issues.GetIssuesForUser
{
    public class GetIssuesForUserQuery : IRequest<IEnumerable<Issue>>
    {
        public GetIssuesForUserQuery(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; }
    }
}