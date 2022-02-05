using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace User.Core.CQS.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.OrganizationId).NotEmpty();

            RuleFor(command => command.Email).EmailAddress();

            RuleFor(command => command.Password)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$")
                .WithMessage("Password should contains 8 characters and consist of at least one upper case, one number and one special number");
        }
    }
}
