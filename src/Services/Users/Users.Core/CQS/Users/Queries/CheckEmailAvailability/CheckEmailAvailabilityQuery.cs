using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.DAL.DataAccessObjects;

namespace Users.Core.CQS.Users.Queries.CheckEmailAvailability
{
    public class CheckEmailAvailabilityQuery : IRequest<bool>
    {
        public string Email { get; }

        public CheckEmailAvailabilityQuery(string email)
        {
            Email = email;
        }
    }

    public class CheckEmailAvailabilityQueryHandler : IRequestHandler<CheckEmailAvailabilityQuery, bool>
    {
        private readonly UserManager<UserDAO> _userManager;

        public CheckEmailAvailabilityQueryHandler(UserManager<UserDAO> userManager)
        {
            _userManager = userManager;

        }
        public async Task<bool> Handle(CheckEmailAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var userDao = await _userManager.FindByIdAsync(request.Email);

            return userDao is null;
        }
    }
}
