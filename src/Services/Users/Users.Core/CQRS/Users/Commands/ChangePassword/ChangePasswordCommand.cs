using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.Core.Exceptions;
using Users.DAL.DataAccessObjects;

namespace Users.Core.CQRS.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest
    {
        public ChangePasswordCommand(string userId, string oldPassword, string newPassword)
        {
            UserId = userId;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string UserId { get; }

        public string OldPassword { get; }

        public string NewPassword { get; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly UserManager<UserDAO> _userManager;

        public ChangePasswordCommandHandler(UserManager<UserDAO> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user is null)
                throw new NotFoundException($"User with id: {request.UserId} not found");

            var identityResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (identityResult.Succeeded == false)
                throw IdentityResultException.IdentityResultFailed(identityResult);

            return Unit.Value;
        }
    }
}
