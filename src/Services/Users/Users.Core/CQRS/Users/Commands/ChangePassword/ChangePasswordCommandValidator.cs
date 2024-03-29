﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Users.Core.Exceptions;

namespace Users.Core.CQRS.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();

            RuleFor(command => command.OldPassword).NotEmpty();

            RuleFor(command => command.NewPassword)
                .Matches(Constants.Regex.PasswordRule)
                .WithMessage(ApplicationErrorMessages.PasswordIsNotPassingRequiredCriteria);
        }
    }
}
