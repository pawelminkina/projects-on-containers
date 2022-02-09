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
using Users.DAL;
using Users.DAL.DataAccessObjects;

namespace Users.Core.CQRS.Users.Commands.CreateUser
{
    public sealed class CreateUserCommand : IRequest<Domain.User>
    {
        public CreateUserCommand(string email, string organizationId, string password, string fullname)
        {
            Email = email;
            OrganizationId = organizationId;
            Password = password;
            Fullname = fullname;
        }

        public string Email { get; }

        public string OrganizationId { get; }

        public string Password { get; }
        public string Fullname { get; }
    }

    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Domain.User>
    {
        private readonly UserManager<UserDAO> _userManager;
        private readonly UserServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<UserDAO> userManager, UserServiceDbContext dbContext, IMapper mapper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Domain.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var organization = await _dbContext.Organizations.FirstAsync(o => o.Id.Equals(request.OrganizationId), cancellationToken);

            if (organization is null)
                throw new NotFoundException($"No organization found with id: {request.OrganizationId}");

            var identityResult = await _userManager.CreateAsync(new UserDAO()
            {
                UserName = request.Email,
                Email = request.Email,
                Organization = organization,
                TimeOfCreationUtc = DateTime.UtcNow,
                Fullname = request.Fullname
            }, request.Password);

            if (!identityResult.Succeeded)
                throw IdentityResultException.IdentityResultFailed(identityResult);
            
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(request.Email), cancellationToken);

            return _mapper.Map<Domain.User>(user);

        }
    }
}
