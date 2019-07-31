using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;

namespace SSO.Storage
{
    public interface IIdentityResourceStore
    {
        Task<bool> AddIdentityClaim(int identityResourceId, IdentityClaim entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<IdentityResource> AddIdentityResourceAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CanInsertIdentityResourceAsync(IdentityResource entity);
        Task<bool> DeleteIdentityResourceAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken));
        void Dispose();
        Task<IdentityResource> GetIdentityResourceAsync(int identityResourceId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IQueryable<IdentityResource>> GetIdentityResourcesAsync(string filter, int start, int count, CancellationToken cancellationToken = default(CancellationToken));
        Task<IdentityResource> UpdateIdentityResourceAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}