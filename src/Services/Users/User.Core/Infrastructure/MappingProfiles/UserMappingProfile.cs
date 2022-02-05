using AutoMapper;
using Users.DAL.DataAccessObjects;

namespace User.Core.Infrastructure.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDAO, Domain.User>()
                .ForMember(u => u.Id, cfg => cfg.MapFrom(src => src.Id))
                .ForMember(u => u.UserName, cfg => cfg.MapFrom(src => src.UserName))
                .ForMember(u => u.Organization, cfg => cfg.MapFrom(src => src.Organization))
                .ForMember(u => u.TimeOfCreation, cfg => cfg.MapFrom(src => new DateTimeOffset(src.TimeOfCreationUtc)))
                .ForMember(u => u.Fullname, cfg => cfg.MapFrom(src => src.Fullname))

                .ReverseMap();
        }
    }
}
