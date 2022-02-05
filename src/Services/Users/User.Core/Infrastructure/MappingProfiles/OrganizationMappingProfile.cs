using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using User.Core.Domain;
using Users.DAL.DataAccessObjects;

namespace User.Core.Infrastructure.MappingProfiles
{
    public class OrganizationMappingProfile : Profile
    {
        public OrganizationMappingProfile()
        {
            CreateMap<OrganizationDAO, Organization>()
                .ForMember(o => o.Id, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(o => o.Name, cfg => cfg.MapFrom(src => src.Name))
                .ForMember(o => o.TimeOfCreation, cfg => cfg.MapFrom(src => new DateTimeOffset(src.TimeOfCreationUtc)));
        }
    }
}
