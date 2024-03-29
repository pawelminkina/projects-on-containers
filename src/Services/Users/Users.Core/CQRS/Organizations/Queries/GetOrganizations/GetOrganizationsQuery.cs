﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Users.Core.Domain;
using Users.DAL;

namespace Users.Core.CQRS.Organizations.Queries.GetOrganizations
{
    public sealed class GetOrganizationsQuery : IRequest<IEnumerable<Organization>>
    {

    }

    public sealed class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IEnumerable<Organization>>
    {
        private readonly UserServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetOrganizationsQueryHandler(UserServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Organization>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.Organizations.Select(_mapper.Map<Organization>);
        }
    }
}
