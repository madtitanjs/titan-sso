using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.Mapper
{
    public class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            CreateMap<Client, ClientDTO>(MemberList.Destination)
                .ReverseMap();

            CreateMap<ClientGrantType, string>()
                .ConstructUsing(src => src.GrantType)
                .ReverseMap()
                .ForMember(dest => dest.GrantType, opt => opt.MapFrom(src => src));

            CreateMap<ClientRedirectUri, string>()
                .ConstructUsing(src => src.RedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.RedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientPostLogoutRedirectUri, string>()
                .ConstructUsing(src => src.PostLogoutRedirectUri)
                .ReverseMap()
                .ForMember(dest => dest.PostLogoutRedirectUri, opt => opt.MapFrom(src => src));

            CreateMap<ClientScope, string>()
                .ConstructUsing(src => src.Scope)
                .ReverseMap()
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src));

            CreateMap<ClientSecret, ClientSecretDTO>(MemberList.Destination)
                .ForMember(dest => dest.Type, opt => opt.Condition(srs => srs != null))
                .ReverseMap();

            CreateMap<ClientClaim, ClaimDTO>(MemberList.None)
                .ConstructUsing(src => new ClaimDTO { Type = src.Type, Value = src.Value })
                .ReverseMap();

            CreateMap<ClientCorsOrigin, string>()
                  .ConstructUsing(src => src.Origin)
                  .ReverseMap()
                  .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src));
        }
    }
}
