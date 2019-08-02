using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.DTO;
using System.Linq;

namespace SSO.Core.Mapper
{
    public class ApiResourceMapperProfile : Profile
    {
        public ApiResourceMapperProfile()
        {
            // entity to model
            CreateMap<ApiResource, ApiResourceDTO>(MemberList.Destination)
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => x.Type)));

            CreateMap<ApiScope, ScopeDTO>(MemberList.Destination)
                .ForMember(x => x.UserClaims, opt => opt.MapFrom(src => src.UserClaims.Select(x => x.Type)));

            CreateMap<ApiSecret, SecretDTO>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
                .ReverseMap();

            // model to entity
            CreateMap<ApiResourceDTO, ApiResource>(MemberList.Source)
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new ApiResourceClaim { Type = x })));

            CreateMap<ScopeDTO, ApiScope>(MemberList.Source)
                .ForMember(x => x.UserClaims, opts => opts.MapFrom(src => src.UserClaims.Select(x => new ApiScopeClaim { Type = x })));
        }
    }
}
