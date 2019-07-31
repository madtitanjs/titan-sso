using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System;

namespace SSO.Core.Context
{
    public class SSOPersistDbContext : PersistedGrantDbContext<SSOPersistDbContext>
    {
        private readonly OperationalStoreOptions storeOptions;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public SSOPersistDbContext(DbContextOptions<SSOPersistDbContext> options, OperationalStoreOptions storeOptions)
            : base(options, storeOptions)
        {
            this.storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigurePersistedGrantContext(storeOptions);
            base.OnModelCreating(modelBuilder);
        }
    }
}
