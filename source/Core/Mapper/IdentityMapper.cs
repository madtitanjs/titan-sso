using AutoMapper;
using SSO.Core.DTO;
using SSO.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSO.Core.Mapper
{
    public static class IdentityMapper
    {
        internal static IMapper Mapper { get; }
        static IdentityMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityMapperProfile>())
                .CreateMapper();
        }

        public static UserDTO ToDTO(this User entity)
        {
            return entity == null ? null : Mapper.Map<UserDTO>(entity);
        }

        public static User ToEntity(this UserDTO dto)
        {
            return dto == null ? null : Mapper.Map<User>(dto);
        }

        public static RoleDTO ToDTO(this Role entity)
        {
            return entity == null ? null : Mapper.Map<RoleDTO>(entity);
        }

        public static Role ToEntity(this RoleDTO dto)
        {
            return dto == null ? null : Mapper.Map<Role>(dto);
        }

    }
}
