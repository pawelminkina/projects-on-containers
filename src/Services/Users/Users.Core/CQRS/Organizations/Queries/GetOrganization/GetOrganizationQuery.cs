using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Core.Domain;
using Users.Core.Exceptions;
using Users.DAL;

namespace Users.Core.CQRS.Organizations.Queries.GetOrganization
{
    public class GetOrganizationQuery : IRequest<Organization>
    {
        public string OrganizationId { get; }

        public GetOrganizationQuery(string organizationId)
        {
            OrganizationId = organizationId;
        }
    }

    public class GetOrganizationQueryHandler : IRequestHandler<GetOrganizationQuery, Organization>
    {
        private readonly UserServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrganizationQueryHandler(UserServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Organization> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
            var organization = await _dbContext.Organizations.FirstOrDefaultAsync(s=>s.Id == request.OrganizationId);
            if (organization is null)
                throw NotFoundException.RequestedResourceWithIdWasNotFound(request.OrganizationId);

            return _mapper.Map<Organization>(organization);

        }
    }
}
