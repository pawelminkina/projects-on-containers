using System.IO;
using MediatR;

namespace Issues.Application.GroupOfIssues.CreateGroup
{
    public class CreateGroupOfIssuesCommand : IRequest<string>
    {

        public CreateGroupOfIssuesCommand(string typeOfGroupId, string statusFlowId, string name, string organizationId)
        {
            TypeOfGroupId = typeOfGroupId;
            StatusFlowId = statusFlowId;
            Name = name;
            OrganizationId = organizationId;
        }

        public string TypeOfGroupId { get; }
        public string StatusFlowId { get; }
        public string Name { get; }
        public string OrganizationId { get; }
    }
}