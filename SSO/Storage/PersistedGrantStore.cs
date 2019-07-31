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
    public class PersistedGrantStore : IDisposable, IPersistedGrantStore
    {
        public PersistedGrantStore(SSOPersistDbContext context)
        {
            Context = context;
        }



        public Task<IOrderedQueryable<PersistedGrant>> GetPersistedGrantsByUser(string subjectId, int start, int count, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = from obj
                        in PersistedGrants
                        where obj.SubjectId == subjectId
                        orderby obj.CreationTime
                        select obj;


            return Task.FromResult(query);
        }

        public async Task<PersistedGrant> GetPersitedGrantAsync(string key, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await PersistedGrants.SingleOrDefaultAsync(x => x.Key == key, cancellationToken);
        }

        public async Task<bool> RemovePersistedGrantAsync(string key, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await PersistedGrants.SingleOrDefaultAsync(x => x.Key == key, cancellationToken);
            PersistedGrants.Remove(result);
            await SaveChanges(cancellationToken);
            return true;
        }

        public async Task<bool> ExistsPersistedGrantsAsync(string subjectId)
        {
            var exist = await PersistedGrants.AnyAsync(x => x.SubjectId == subjectId);
            return exist;
        }

        /// <summary>
        /// Remove All Persisted Grants Of A User
        /// </summary>
        /// <param name="subjectId">User ID</param>
        /// <param name="cancellationToken">Propagates notification that operations should be cancelled</param>
        /// <returns></returns>
        public async Task<bool> RemovePersistedGrantsAsync(string subjectId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = await PersistedGrants.Where(x => x.SubjectId == subjectId).ToListAsync(cancellationToken);
            PersistedGrants.RemoveRange(result);
            await SaveChanges(cancellationToken);
            return true;
        }


        private readonly SSOPersistDbContext Context;
        protected DbSet<PersistedGrant> PersistedGrants { get { return Context.Set<PersistedGrant>(); } }
        protected DbSet<DeviceFlowCodes> DeviceFlowCodes { get { return Context.Set<DeviceFlowCodes>(); } }
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
