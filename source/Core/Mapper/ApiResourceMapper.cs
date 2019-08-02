using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.DTO;

namespace SSO.Core.Mapper
{
    public static class ApiResourceMapper
    {
        internal static IMapper Mapper { get; }
        static ApiResourceMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ApiResourceMapperProfile>())
                .CreateMapper();
        }

        public static ApiResourceDTO ToDTO(this ApiResource entity)
        {
            return entity == null ? null : Mapper.Map<ApiResourceDTO>(entity);
        }

        public static ApiResource ToEntity(this ApiResourceDTO dto)
        {
            return dto == null ? null : Mapper.Map<ApiResource>(dto);
        }

        public static ScopeDTO ToDTO(this ApiScope entity)
        {
            return entity == null ? null : Mapper.Map<ScopeDTO>(entity);
        }

        public static ApiScope ToEntity(this ScopeDTO dto)
        {
            return dto == null ? null : Mapper.Map<ApiScope>(dto);
        }

        public static SecretDTO ToDTO(this ApiSecret entity)
        {
            return entity == null ? null : Mapper.Map<SecretDTO>(entity);
        }

        public static ApiSecret ToEntity(this SecretDTO dto)
        {
            return dto == null ? null : Mapper.Map<ApiSecret>(dto);
        }
    }
}
