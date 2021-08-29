using MediatR;

namespace Issues.Application.GroupOfIssues.GetGroup
{
    public class GetGroupOfIssuesQuery : IRequest<Domain.GroupsOfIssues.GroupOfIssues>
    {
        public GetGroupOfIssuesQuery(string id, string organizationId)
        {
            Id = id;
            OrganizationId = organizationId;
        }

        public string Id { get; }
        public string OrganizationId { get; }
    }
}