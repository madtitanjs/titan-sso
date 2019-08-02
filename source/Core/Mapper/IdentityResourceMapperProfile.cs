using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.DTO;
using System.Linq;

namespace SSO.Core.Mapper
{
    public class IdentityResourceMapperProfile : Profile
    {
        public IdentityResourceMapperProfile()
        {
            CreateMap<IdentityResource, IdentityResourceDTO>(MemberList.Destination)
                .ForMember(x => x.UserClaims, o => o.MapFrom(src => src.UserClaims.Select(s => s.Type)));

            CreateMap<IdentityResourceDTO, IdentityResource>(MemberList.Source)
                .ForMember(x => x.UserClaims, o => o.MapFrom(src => src.UserClaims.Select(s => new IdentityClaim { Type = s })));
        }
    }
}
