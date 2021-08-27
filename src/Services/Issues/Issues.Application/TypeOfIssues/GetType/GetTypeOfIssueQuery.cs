using Issues.Domain.TypesOfIssues;
using MediatR;

namespace Issues.Application.TypeOfIssues.GetType
{
    public class GetTypeOfIssueQuery : IRequest<TypeOfIssue>
    {
        public GetTypeOfIssueQuery(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }
        public string OrganizationId { get; }
    }
}