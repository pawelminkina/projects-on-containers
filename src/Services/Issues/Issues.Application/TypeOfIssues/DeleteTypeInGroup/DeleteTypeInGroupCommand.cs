using MediatR;

namespace Issues.Application.TypeOfIssues.DeleteTypeInGroup
{
    public class DeleteTypeInGroupCommand : IRequest
    {
        public DeleteTypeInGroupCommand(string typeOfIssueId, string typeOfGroupOfIssuesId, string organizationId)
        {
            TypeOfIssueId = typeOfIssueId;
            TypeOfGroupOfIssuesId = typeOfGroupOfIssuesId;
            OrganizationId = organizationId;
        }

        public string TypeOfIssueId { get; }
        public string TypeOfGroupOfIssuesId { get; }
        public string OrganizationId { get; }
    }
}