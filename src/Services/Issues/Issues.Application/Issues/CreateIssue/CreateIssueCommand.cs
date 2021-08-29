using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Issues.Application.Issues.CreateIssue
{
    public class CreateIssueCommand : IRequest<string>
    {
        public CreateIssueCommand(string name, string groupId, string textContent, string userId, string organizationId, string typeOfIssueId)
        {
            Name = name;
            GroupId = groupId;
            TextContent = textContent;
            UserId = userId;
            OrganizationId = organizationId;
            TypeOfIssueId = typeOfIssueId;
        }
        public string Name { get; }
        public string GroupId { get; }
        public string TextContent { get; }
        public string UserId { get; }
        public string OrganizationId { get; }
        public string TypeOfIssueId { get; }
    }
}
