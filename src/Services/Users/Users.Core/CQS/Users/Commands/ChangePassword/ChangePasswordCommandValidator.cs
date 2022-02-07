using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Users.Core.Exceptions;

namespace Users.Core.CQS.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();

            RuleFor(command => command.OldPassword).NotEmpty();

            RuleFor(command => command.NewPassword)
                .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$")
                .WithMessage(ApplicationErrorMessages.PasswordIsNotPassingRequiredCriteria);
        }
    }
}
