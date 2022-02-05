using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.DAL.DataAccessObjects;

namespace User.Core.CQS.Users.Queries.CheckIdAndPasswordMatches
{
    public sealed class CheckIdAndPasswordMatchesQuery : IRequest<bool>
    {
        public CheckIdAndPasswordMatchesQuery(string userId, string password)
        {
            UserId = userId;
            Password = password;
        }

        public string UserId { get; }

        public string Password { get; }
    }

    public class CheckIdAndPasswordMatchesQueryHandler : IRequestHandler<CheckIdAndPasswordMatchesQuery, bool>
    {
        private readonly UserManager<UserDAO> _userManager;

        public CheckIdAndPasswordMatchesQueryHandler(UserManager<UserDAO> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(CheckIdAndPasswordMatchesQuery request, CancellationToken cancellationToken)
        {
            var userDao = await _userManager.FindByIdAsync(request.UserId);
            return await _userManager.CheckPasswordAsync(userDao, request.Password);
        }
    }
}
