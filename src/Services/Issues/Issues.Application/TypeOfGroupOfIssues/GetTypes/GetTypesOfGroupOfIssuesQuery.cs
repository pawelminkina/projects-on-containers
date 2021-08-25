using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.GetTypes
{
    public class GetTypesOfGroupOfIssuesQuery : IRequest<IEnumerable<Domain.GroupsOfIssues.TypeOfGroupOfIssues>>
    {
        public GetTypesOfGroupOfIssuesQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }

    }
}
