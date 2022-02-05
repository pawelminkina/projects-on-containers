using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.DAL;

namespace User.Core.CQS.Users.Queries.GetUsersForOrganization
{
    public class GetUsersForOrganizationQuery : IRequest<IEnumerable<Domain.User>>
    {
        public GetUsersForOrganizationQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; }
    }

    public class GetUsersForOrganizationQueryHandler : IRequestHandler<GetUsersForOrganizationQuery, IEnumerable<Domain.User>>
    {
        private readonly UserServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersForOrganizationQueryHandler(UserServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.User>> Handle(GetUsersForOrganizationQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Users.Include(s => s.Organization).Where(d => d.OrganizationId == request.OrganizationId).AsEnumerable().Select(_mapper.Map<Domain.User>);
        }
    }
}
