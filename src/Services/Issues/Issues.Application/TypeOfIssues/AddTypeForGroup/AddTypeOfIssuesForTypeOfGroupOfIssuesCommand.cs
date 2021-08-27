using MediatR;

namespace Issues.Application.TypeOfIssues.AddTypeForGroup
{
    public class AddTypeOfIssuesForTypeOfGroupOfIssuesCommand : IRequest
    {
        public AddTypeOfIssuesForTypeOfGroupOfIssuesCommand(string typeOfIssuesId, string typeOfGroupOfIssuesId, string statusFlowId, string organizationId)
        {
            TypeOfIssuesId = typeOfIssuesId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            StatusFlowId = statusFlowId;
            OrganizationId = organizationId;
        }

        public string TypeOfIssuesId { get; }
        public string TypeOfGroupOfIssuesId { get; }
        public string StatusFlowId { get; }
        public string OrganizationId { get; }
    }
}