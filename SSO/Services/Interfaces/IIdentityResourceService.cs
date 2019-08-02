using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core.DTO;

namespace Services
{
    public interface IIdentityResourceService
    {
        DbSet<IdentityClaim> IdentityClaims { get; }
        DbSet<IdentityResourceProperty> IdentityResourceProperties { get; }
        DbSet<IdentityResource> IdentityResources { get; }

        Task<IdentityResourceDTO> AddIdentityResourceAsync(IdentityResourceDTO dto);
        Task DeleteAsync(int id);
        Task<IdentityResourceDTO> GetAsync(int id);
        Task<PagedQuery<IdentityResourceDTO>> SearchAsync(SearchDTO search);
        Task<IdentityResourceDTO> UpdateIdentityResourceAsync(IdentityResourceDTO dto);
    }
}