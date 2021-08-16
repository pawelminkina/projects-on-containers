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
        public CreateIssueCommand(string name, string groupId, string textContent, string organizationId)
        {
            Name = name;
            GroupId = groupId;
            TextContent = textContent;
            OrganizationId = organizationId;
        }
        public string Name { get; }
        public string GroupId { get; }
        public string TextContent { get; }
        public string OrganizationId { get; }
    }
}
