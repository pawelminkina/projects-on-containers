using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Issues.Application.Issues.DeleteIssue
{
    public class DeleteIssueCommand : IRequest
    {
        public string IssueId { get; }
        public string OrganizationId { get; }

        public DeleteIssueCommand(string issueId, string organizationId)
        {
            IssueId = issueId;
            OrganizationId = organizationId;
        }
    }
}
