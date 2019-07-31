using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core;
using SSO.Core.Context;
using SSO.Core.DTO;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SSO.Storage
{
    public class ApiResourceStore : IDisposable, IApiResourceStore
    {
        public ApiResourceStore(SSOConfigDbContext context, IPersistedGrantStore store)
        {
            Context = context;
        }

        public async Task<ApiResource> GetApiAsync(int apiResourceId)
        {
            var result = await Apis
                        .Include(x => x.UserClaims)
                        .Include(o => o.Secrets)
                        .Include(o => o.Scopes)
                        .ThenInclude(s => s.UserClaims)
                        .AsNoTracking()
                        .Where(x => x.Id == apiResourceId)
                        .SingleOrDefaultAsync();

            return result;
        }

        public Task<IOrderedQueryable<ApiResource>> GetApisAsync(string filter, int start, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = from obj
                        in Apis
                        .Include(x => x.UserClaims)
                        .Include(o => o.Secrets)
                        .Include(o => o.Scopes)
                        .ThenInclude(s => s.UserClaims)
                        orderby obj.Name
                        select obj;

            return Task.FromResult(query);
        }

        public Task<IOrderedQueryable<ApiScope>> GetApiScopesAsync(int apiResourceId, int start, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = from obj
                        in ApiScopes
                        where obj.ApiResourceId == apiResourceId
                        orderby obj.Name
                        select obj;

            return Task.FromResult(query);
        }

        public Task<IOrderedQueryable<ApiSecret>> GetApiSecretsAsync(int apiResourceId, int start, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = from obj
                        in ApiSecrets
                        where obj.ApiResourceId == apiResourceId
                        orderby obj.Created
                        select obj;

            return Task.FromResult(query);
        }

        public async Task<ApiScope> GetApiScopeAsync(int apiResourceId, int apiScopeId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await ApiScopes
                .Include(x => x.UserClaims)
                .Include(x => x.ApiResource)
                .Where(x => x.Id == apiScopeId && x.ApiResource.Id == apiResourceId)
                .SingleOrDefaultAsync(cancellationToken);

            return (result);
        }

        public async Task<ApiSecret> GetApiSecretAsync(int apiResourceId, int apiSecretId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await ApiSecrets
                .Include(x => x.ApiResource)
                .Where(x => x.Id == apiSecretId && x.ApiResource.Id == apiResourceId)
                .SingleOrDefaultAsync(cancellationToken);

            return result;
        }

        public async Task<bool> AddApiSecretAsync(int apiResourceId, ApiSecret entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parent = await Apis.Where(x => x.Id == apiResourceId).SingleOrDefaultAsync(cancellationToken);
            entity.ApiResource = parent;

            await ApiSecrets.AddAsync(entity);
            await SaveChanges(cancellationToken);
            return true;
        }

        public async Task<bool> AddApiScopeAsync(int apiResourceId, ApiScope entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parent = await Apis.Where(x => x.Id == apiResourceId).SingleOrDefaultAsync(cancellationToken);
            entity.ApiResource = parent;

            await ApiScopes.AddAsync(entity);
            await SaveChanges(cancellationToken);
            return true;
        }

        public async Task<bool> AddApiScopeClaimAsync(int apiScopeClaimId, ApiScopeClaim entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parent = await ApiScopes.Where(x => x.Id == apiScopeClaimId).SingleOrDefaultAsync(cancellationToken);
            entity.ApiScope = parent;
            await ApiScopeClaims.AddAsync(entity);
            await SaveChanges(cancellationToken);
            return true;
        }

        public async Task<bool> AddApiPropertyAsync(int apiResourceId, ApiResourceProperty entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parent = await Apis.Where(x => x.Id == apiResourceId).SingleOrDefaultAsync(cancellationToken);
            entity.ApiResource = parent;

            await ApiProperties.AddAsync(entity);
            await SaveChanges(cancellationToken);
            return true;
        }



        /// <summary>
        /// User Claims that are bundled with the token when using this resource as a scope
        /// </summary>
        /// <param name="apiResourceId"></param>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> AddApiClaimsAsync(int apiResourceId, ApiResourceClaim entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var parent = await Apis.Where(x => x.Id == apiResourceId).SingleOrDefaultAsync(cancellationToken);
            entity.ApiResource = parent;

            await ApiClaims.AddAsync(entity);
            await SaveChanges(cancellationToken);
            return true;

        }
        public async Task<ApiScopeClaim> GetApiScopeClaimAsync(int scopeClaimId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = await ApiScopeClaims.FirstOrDefaultAsync(o => o.Id == scopeClaimId);
            return query;
        }

        public async Task<ApiResourceProperty> GetApiPropertyAsync(int apiResourcePropertyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = await ApiProperties.FirstOrDefaultAsync(o => o.Id == apiResourcePropertyId);
            return query;
        }

        public async Task<ApiResourceClaim> GetApiClaimAsync(int apiResourceClaimId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = await ApiClaims.FirstOrDefaultAsync(o => o.Id == apiResourceClaimId);
            return query;
        }

        public async Task<bool> RemoveApiSecretAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await ApiSecrets.FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
            if (entity == null)
                throw new Exception(Constants.Errors.NotFound);

            Context.Attach(entity);
            Context.Remove(entity);
            await SaveChanges(cancellationToken);

            return true;
        }

        public async Task<bool> RemoveApiClaimAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await ApiClaims.FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
            if (entity == null)
                throw new Exception(Constants.Errors.NotFound);

            Context.Attach(entity);
            Context.Remove(entity);
            await SaveChanges(cancellationToken);

            return true;
        }

        public async Task<bool> RemoveApiPropertyAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await ApiProperties.FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
            if (entity == null)
                throw new Exception(Constants.Errors.NotFound);

            Context.Attach(entity);
            Context.Remove(entity);
            await SaveChanges(cancellationToken);

            return true;
        }

        public async Task<bool> RemoveApiScopeAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await ApiScopes.FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
            if (entity == null)
                throw new Exception(Constants.Errors.NotFound);

            Context.Attach(entity);
            Context.Remove(entity);
            await SaveChanges(cancellationToken);

            return true;
        }

        public async Task<bool> RemoveApiScopeClaimAsync(int Id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await ApiScopeClaims.FirstOrDefaultAsync(o => o.Id == Id, cancellationToken);
            if (entity == null)
                throw new Exception(Constants.Errors.NotFound);

            Context.Attach(entity);
            Context.Remove(entity);
            await SaveChanges(cancellationToken);

            return true;
        }

        public async Task<ApiResource> CreateAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Apis.AddAsync(entity, cancellationToken);
            await SaveChanges(cancellationToken);
            return (entity);
        }

        public async Task<ApiResource> UpdateAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            Apis.Attach(entity);
            await RemoveApiResourceClaimsAsync(entity);
            Apis.Update(entity);
            await SaveChanges(cancellationToken);

            return (entity);
        }

        public async Task<bool> RemoveAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resource = await Apis.Where(x => x.Id == entity.Id).SingleOrDefaultAsync();
            Context.Remove(resource);
            await SaveChanges(cancellationToken);
            return true;
        }

        public async Task<bool> CanInsertApiResourceAsync(ApiResource apiResource)
        {
            if (apiResource.Id == 0)
            {
                var existsWithSameName = await Apis.Where(x => x.Name == apiResource.Name).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }
            else
            {
                var existsWithSameName = await Apis.Where(x => x.Name == apiResource.Name && x.Id != apiResource.Id).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }
        }

        public async Task<bool> CanInsertApiScopeAsync(ApiScope apiScope)
        {
            if (apiScope.Id == 0)
            {
                var existsWithSameName = await ApiScopes.Where(x => x.Name == apiScope.Name).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }
            else
            {
                var existsWithSameName = await ApiScopes.Where(x => x.Name == apiScope.Name && x.Id != apiScope.Id).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }
        }

        private async Task RemoveApiResourceClaimsAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Remove old identity claims
            var apiResourceClaims = await ApiClaims.Where(x => x.ApiResource.Id == entity.Id).ToListAsync();
            ApiClaims.RemoveRange(apiResourceClaims);

            //Remove old proprs
            var apiProps = await ApiProperties.Where(x => x.ApiResource.Id == entity.Id).ToListAsync();
            ApiProperties.RemoveRange(apiProps);

            //Remove old scopes
            var apiScopes = await ApiScopes.Where(x => x.ApiResource.Id == entity.Id).ToListAsync();
            ApiScopes.RemoveRange(apiScopes);

            //Remove old secrets
            var apiSecrets = await ApiSecrets.Where(x => x.ApiResource.Id == entity.Id).ToListAsync();
            ApiSecrets.RemoveRange(apiSecrets);

            await SaveChanges(cancellationToken);
        }

        private readonly SSOConfigDbContext Context;
        protected DbSet<ApiResource> Apis { get { return Context.Set<ApiResource>(); } }
        protected DbSet<ApiResourceClaim> ApiClaims { get { return Context.Set<ApiResourceClaim>(); } }
        protected DbSet<ApiResourceProperty> ApiProperties { get { return Context.Set<ApiResourceProperty>(); } }
        protected DbSet<ApiSecret> ApiSecrets { get { return Context.Set<ApiSecret>(); } }
        protected DbSet<ApiScope> ApiScopes { get { return Context.Set<ApiScope>(); } }
        protected DbSet<ApiScopeClaim> ApiScopeClaims { get { return Context.Set<ApiScopeClaim>(); } }
        private bool _disposed;


        private async Task SaveChanges(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Dispose the store
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }
    }
}
