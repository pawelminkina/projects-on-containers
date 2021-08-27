using System.Collections.Generic;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.GetTypes
{
    public class GetTypesOfIssuesQuery : IRequest<IEnumerable<TypeOfIssue>>
    {
        public GetTypesOfIssuesQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }
}