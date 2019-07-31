using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using SSO.Core;
using SSO.Core.Context;
using SSO.Core.DTO;
using SSO.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSO.Storage
{
    public class ClientStore : IDisposable
    {
        private bool _disposed;
        public ClientStore(SSOConfigDbContext context)
        {
            Context = context;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
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

        #region STORE PROPS
        internal SSOConfigDbContext Context { get; private set; }
        public DbSet<Client> Clients { get { return Context.Set<Client>(); } }
        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get { return Context.Set<ClientCorsOrigin>(); } }
        public DbSet<ClientClaim> ClientClaims { get { return Context.Set<ClientClaim>(); } }
        public DbSet<ClientGrantType> ClientGrantTypes { get { return Context.Set<ClientGrantType>(); } }
        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get { return Context.Set<ClientIdPRestriction>(); } }
        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get { return Context.Set<ClientPostLogoutRedirectUri>(); } }
        public DbSet<ClientProperty> ClientProperties { get { return Context.Set<ClientProperty>(); } }
        public DbSet<ClientRedirectUri> ClientRedirectUris { get { return Context.Set<ClientRedirectUri>(); } }
        public DbSet<ClientScope> ClientScopes { get { return Context.Set<ClientScope>(); } }
        public DbSet<ClientSecret> ClientSecrets { get { return Context.Set<ClientSecret>(); } }
        #endregion
    }
}
