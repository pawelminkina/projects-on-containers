using System.Collections;
using System.Collections.Generic;
using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.GetTypesForTypeInGroup
{
    public class GetTypesOfIssuesForTypeInGroupOfIssuesQuery : IRequest<IEnumerable<TypeOfIssue>>
    {

        public GetTypesOfIssuesForTypeInGroupOfIssuesQuery(string typeOfGroupOfIssuesId, string organizationId)
        {
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            OrganizationId = organizationId;
        }

        public string TypeOfGroupOfIssuesId { get; }
        public string OrganizationId { get; }
    }
}