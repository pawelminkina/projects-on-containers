using MediatR;

namespace Issues.Application.Status.GetStatus
{
    public class GetStatusQuery : IRequest<Domain.StatusesFlow.Status>
    {
        public GetStatusQuery(string statusId, string organizationId)
        {
            StatusId = statusId;
            OrganizationId = organizationId;
        }

        public string StatusId { get; }
        public string OrganizationId { get; }
    }
}