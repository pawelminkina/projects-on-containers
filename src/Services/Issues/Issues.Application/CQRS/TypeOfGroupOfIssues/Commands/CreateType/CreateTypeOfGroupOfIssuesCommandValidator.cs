using FluentValidation;

namespace Issues.Application.CQRS.TypeOfGroupOfIssues.Commands.CreateType
{
    public class CreateTypeOfGroupOfIssuesCommandValidator : AbstractValidator<CreateTypeOfGroupOfIssuesCommand>
    {
        public CreateTypeOfGroupOfIssuesCommandValidator()
        {
            RuleFor(command => command.OrganizationId).NotEmpty();

            RuleFor(command => command.Name).NotEmpty();
        }
    }
}