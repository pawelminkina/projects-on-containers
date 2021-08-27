using MediatR;

namespace Issues.Application.TypeOfIssues.CreateType
{
    public class CreateTypeOfIssuesCommand : IRequest<string>
    {
        public CreateTypeOfIssuesCommand(string name, string organizationId)
        {
            Name = name;
            OrganizationId = organizationId;
        }

        public string Name { get; }
        public string OrganizationId { get; }
    }
}