using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.DTO;

namespace SSO.Core.Mapper
{
    public static class IdentityResourceMapper
    {
        internal static IMapper Mapper { get; }
        static IdentityResourceMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<IdentityResourceMapperProfile>())
                .CreateMapper();
        }

        public static IdentityResourceDTO ToDTO(this IdentityResource entity)
        {
            return entity == null ? null : Mapper.Map<IdentityResourceDTO>(entity);
        }

        public static IdentityResource ToEntity(this IdentityResourceDTO dto)
        {
            return dto == null ? null : Mapper.Map<IdentityResource>(dto);
        }
    }
}
