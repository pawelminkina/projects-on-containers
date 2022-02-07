using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Core.Domain;
using Users.DAL;

namespace Users.Core.CQS.Organizations.Queries.ListOrganizations
{
    public sealed class ListOrganizationsQuery : IRequest<IEnumerable<Organization>>
    {

    }

    public sealed class ListOrganizationsQueryHandler : IRequestHandler<ListOrganizationsQuery, IEnumerable<Organization>>
    {
        private readonly UserServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public ListOrganizationsQueryHandler(UserServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Organization>> Handle(ListOrganizationsQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Organizations.Select(_mapper.Map<Organization>);
        }
    }
}
