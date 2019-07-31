using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Context;
using SSO.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSO.Storage
{
    public class IdentityResourceStore : IDisposable, IIdentityResourceStore
    {
        public IdentityResourceStore(SSOConfigDbContext context)
        {
            Context = context;
        }



        public Task<IQueryable<IdentityResource>> GetIdentityResourcesAsync(string filter, int start, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = from idr
                        in Context.IdentityResources
                        .Include(c => c.UserClaims)
                        select idr;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = from idr in query
                        where idr.Name.Contains(filter)
                        orderby idr
                        select idr;
            }

            return Task.FromResult(query);
        }

        public async Task<IdentityResource> GetIdentityResourceAsync(int identityResourceId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await IdentityResources
                .Include(x => x.UserClaims)
                .Where(x => x.Id == identityResourceId)
                .SingleOrDefaultAsync(cancellationToken);

            return result;
        }

        /// <summary>
        /// Add new identity resource
        /// </summary>
        /// <param name="identityResource"></param>
        /// <returns>This method return new identity resource id</returns>
        public async Task<IdentityResource> AddIdentityResourceAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            IdentityResources.Add(entity);

            await SaveChanges(cancellationToken);

            return entity;
        }

        public async Task<bool> AddIdentityClaim(int identityResourceId, IdentityClaim entity, CancellationToken cancellationToken = default(CancellationToken))
        {

            var query = await IdentityClaims.FirstOrDefaultAsync(o => o.Type == entity.Type);
            if (query == null)
            {
                var parent = await IdentityResources.Where(x => x.Id == identityResourceId).SingleOrDefaultAsync();
                entity.IdentityResource = parent;

                await IdentityClaims.AddAsync(entity);
                await SaveChanges(cancellationToken);
            }
            return true;

        }

        public async Task<bool> CanInsertIdentityResourceAsync(IdentityResource entity)
        {
            ThrowIfDisposed();
            if (entity.Id == 0)
            {
                var existsWithSameName = await IdentityResources.Where(x => x.Name == entity.Name).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }
            else
            {
                var existsWithSameName = await IdentityResources.Where(x => x.Name == entity.Name && x.Id != entity.Id).SingleOrDefaultAsync();
                return existsWithSameName == null;
            }


        }

        private async Task<bool> RemoveIdentityResourceClaimsAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            var identityClaims = await IdentityClaims.Where(x => x.IdentityResource.Id == entity.Id).ToListAsync();
            IdentityClaims.RemoveRange(identityClaims);
            return true;
        }

        public async Task<bool> DeleteIdentityResourceAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            var identityResourceToDelete = await IdentityResources.Where(x => x.Id == entity.Id).SingleOrDefaultAsync();

            IdentityResources.Remove(entity);

            await SaveChanges(cancellationToken);

            return true;
        }

        public async Task<IdentityResource> UpdateIdentityResourceAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfDisposed();
            IdentityResources.Attach(entity);
            //Remove old relations
            await RemoveIdentityResourceClaimsAsync(entity);

            //Update with new data
            IdentityResources.Update(entity);

            await SaveChanges(cancellationToken);

            return (entity);
        }


        private readonly SSOConfigDbContext Context;
        protected DbSet<IdentityResource> IdentityResources { get { return Context.Set<IdentityResource>(); } }
        protected DbSet<IdentityResourceProperty> IdentityResourceProperties { get { return Context.Set<IdentityResourceProperty>(); } }
        protected DbSet<IdentityClaim> IdentityClaims { get { return Context.Set<IdentityClaim>(); } }
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
