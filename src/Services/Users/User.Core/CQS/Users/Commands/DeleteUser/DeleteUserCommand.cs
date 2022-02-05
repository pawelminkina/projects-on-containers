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
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace User.Core.CQS.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public DeleteUserCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly UserManager<UserDAO> _userManager;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(UserManager<UserDAO> userManager, UserServiceDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToRemove = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(request.UserId), cancellationToken);

            if (userToRemove is null)
                throw new NotFoundException($"Not found user id: {request.UserId}");

            await _userManager.DeleteAsync(_mapper.Map<UserDAO>(userToRemove));

            return Unit.Value;
        }
    }
}
