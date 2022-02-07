using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Core.Exceptions;
using Users.DAL.DataAccessObjects;

namespace Users.Core.CQS.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQuery : IRequest<Domain.User>
    {
        public GetUserByIdQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Domain.User>
    {
        private readonly UserManager<UserDAO> _userManager;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(UserManager<UserDAO> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Domain.User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var userDao = await _userManager.Users.Include(u => u.Organization).FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken);
            if (userDao is null)
                throw new NotFoundException($"Not found user with id {query.UserId}");

            return _mapper.Map<Domain.User>(userDao);
        }
    }
}
