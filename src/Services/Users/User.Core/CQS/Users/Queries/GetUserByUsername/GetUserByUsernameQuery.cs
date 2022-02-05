using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using User.Core.Exceptions;
using Users.DAL.DataAccessObjects;

namespace User.Core.CQS.Users.Queries.GetUserByUsername
{
    public sealed class GetUserByUsernameQuery : IRequest<Domain.User>

    {
        public GetUserByUsernameQuery(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }

    public sealed class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, Domain.User>
    {
        private readonly UserManager<UserDAO> _userManager;
        private readonly IMapper _mapper;

        public GetUserByUsernameQueryHandler(UserManager<UserDAO> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Domain.User> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
        {
            var userDao = await _userManager.Users
                .Include(u => u.Organization)
                .FirstOrDefaultAsync(u => u.Email.ToUpper().Equals(request.Username.ToUpper()), cancellationToken);

            if (userDao is null)
                throw new NotFoundException($"User with username {request.Username} not found");

            return _mapper.Map<Domain.User>(userDao);
        }
    }
}
