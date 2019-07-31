using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.DTO;

namespace SSO.Storage
{
    public interface IApiResourceStore
    {
        Task<bool> AddApiClaimsAsync(int apiResourceId, ApiResourceClaim entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AddApiPropertyAsync(int apiResourceId, ApiResourceProperty entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AddApiScopeAsync(int apiResourceId, ApiScope entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AddApiScopeClaimAsync(int apiScopeClaimId, ApiScopeClaim entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AddApiSecretAsync(int apiResourceId, ApiSecret entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CanInsertApiResourceAsync(ApiResource apiResource);
        Task<bool> CanInsertApiScopeAsync(ApiScope apiScope);
        Task<ApiResource> CreateAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken));
        void Dispose();
        Task<ApiResource> GetApiAsync(int apiResourceId);
        Task<ApiResourceClaim> GetApiClaimAsync(int apiResourceClaimId, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiResourceProperty> GetApiPropertyAsync(int apiResourcePropertyId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IOrderedQueryable<ApiResource>> GetApisAsync(string filter, int start, int count, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiScope> GetApiScopeAsync(int apiResourceId, int apiScopeId, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiScopeClaim> GetApiScopeClaimAsync(int scopeClaimId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IOrderedQueryable<ApiScope>> GetApiScopesAsync(int apiResourceId, int start, int count, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiSecret> GetApiSecretAsync(int apiResourceId, int apiSecretId, CancellationToken cancellationToken = default(CancellationToken));
        Task<IOrderedQueryable<ApiSecret>> GetApiSecretsAsync(int apiResourceId, int start, int count, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemoveApiClaimAsync(int Id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemoveApiPropertyAsync(int Id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemoveApiScopeAsync(int Id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemoveApiScopeClaimAsync(int Id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemoveApiSecretAsync(int Id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> RemoveAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken));
        Task<ApiResource> UpdateAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}