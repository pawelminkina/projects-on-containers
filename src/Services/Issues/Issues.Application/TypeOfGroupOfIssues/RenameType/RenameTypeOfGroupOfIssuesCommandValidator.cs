using FluentValidation;

namespace Issues.Application.TypeOfGroupOfIssues.RenameType
{
    public class RenameTypeOfGroupOfIssuesCommandValidator : AbstractValidator<RenameTypeOfGroupOfIssuesCommand>
    {
        public RenameTypeOfGroupOfIssuesCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();

            RuleFor(command => command.NewName).NotEmpty();
            
            RuleFor(command => command.OrganizationId).NotEmpty();
        }
    }
}