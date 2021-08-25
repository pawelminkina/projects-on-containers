using MediatR;

namespace Issues.Application.TypeOfGroupOfIssues.CreateType
{
    public class CreateTypeOfGroupOfIssuesCommand : IRequest<string>
    {
        public CreateTypeOfGroupOfIssuesCommand(string name, string organizationId)
        {
            Name = name;
            OrganizationId = organizationId;
        }

        public string Name { get; }
        public string OrganizationId { get; }
    }
}