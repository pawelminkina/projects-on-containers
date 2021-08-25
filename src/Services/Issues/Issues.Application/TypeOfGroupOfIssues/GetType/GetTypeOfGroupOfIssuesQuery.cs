using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.GetType
{
    public class GetTypeOfGroupOfIssuesQuery : IRequest<Domain.GroupsOfIssues.TypeOfGroupOfIssues>
    {
        public GetTypeOfGroupOfIssuesQuery(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }
        public string OrganizationId { get; }

    }
}