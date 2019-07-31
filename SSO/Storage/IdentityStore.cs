using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Context;
using SSO.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSO.Storage
{
    public class IdentityStore : IDisposable, IIdentityStore
    {
        public UserManager<User> UserManager { get; private set; }
        public RoleManager<Role> RoleManager { get; private set; }


        public IdentityStore(UserManager<User> userManager, RoleManager<Role> roleManager, SSODbContext context)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Context = context;
        }

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

        #region STORE PROPS
        public SSODbContext Context { get; private set; }
        protected DbSet<User> Users { get { return Context.Set<User>(); } }
        protected DbSet<Role> Roles { get { return Context.Set<Role>(); } }
        protected DbSet<UserClaim> UserClaims { get { return Context.Set<UserClaim>(); } }
        protected DbSet<UserToken> UserTokens { get { return Context.Set<UserToken>(); } }
        protected DbSet<UserLogin> UserLogins { get { return Context.Set<UserLogin>(); } }
        protected DbSet<RoleClaim> RoleClaims { get { return Context.Set<RoleClaim>(); } }

        private bool _disposed;
        #endregion
    }
}
