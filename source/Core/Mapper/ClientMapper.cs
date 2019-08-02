using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.DTO;

namespace SSO.Core.Mapper
{
    public static class ClientMapper
    {
        internal static IMapper Mapper { get; }
        static ClientMapper()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClientMapperProfile>())
                .CreateMapper();
        }

        public static ClientDTO ToDTO(this Client entity)
        {
            return entity == null ? null : Mapper.Map<ClientDTO>(entity);
        }

        public static Client ToEntity(this ClientDTO dto)
        {
            return dto == null ? null : Mapper.Map<Client>(dto);
        }

        public static ClaimDTO ToDTO(this ClientClaim entity)
        {
            return entity == null ? null : Mapper.Map<ClaimDTO>(entity);
        }

        public static ClientClaim ToEntity(this ClaimDTO dto)
        {
            return dto == null ? null : Mapper.Map<ClientClaim>(dto);
        }

        public static SecretDTO ToDTO(this ClientSecret entity)
        {
            return entity == null ? null : Mapper.Map<SecretDTO>(entity);
        }

        public static ClientSecret ToEntity(this SecretDTO dto)
        {
            return dto == null ? null : Mapper.Map<ClientSecret>(dto);
        }
    }
}
