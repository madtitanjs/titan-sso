using AutoMapper;
using SSO.Core.DTO;
using SSO.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSO.Core.Mapper
{
    public class IdentityMapperProfile : Profile
    {
        public IdentityMapperProfile()
        {
            CreateMap<User, UserDTO>(MemberList.Destination)
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role.Name)))
            .ReverseMap();


            CreateMap<UserClaim, KeyValuePair<string, string>>(MemberList.Destination)
                .ConstructUsing(x => new KeyValuePair<string, string>(x.ClaimType, x.ClaimValue))
                .ReverseMap()
                .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.Value));
            //.ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.ClaimType))
            //.ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClaimValue))
            //.ReverseMap()
            //.ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Key))
            //.

            CreateMap<UserRole, string>()
                .ConvertUsing(r => r.Role.Name);

            CreateMap<string, UserRole>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new Role { Name = src, NormalizedName = src.ToUpper() }));

            CreateMap<Role, RoleDTO>(MemberList.Destination)
                .ReverseMap();
        }
    }
}
