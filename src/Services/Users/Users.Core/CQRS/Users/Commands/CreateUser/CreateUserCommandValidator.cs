using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Users.Core.Exceptions;

namespace Users.Core.CQRS.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.OrganizationId).NotEmpty();

            RuleFor(command => command.Fullname).NotEmpty();

            RuleFor(command => command.Email).EmailAddress();

            RuleFor(command => command.Password)
                .Matches(Constants.Regex.PasswordRule)
                .WithMessage(ApplicationErrorMessages.PasswordIsNotPassingRequiredCriteria);
        }
    }
}
