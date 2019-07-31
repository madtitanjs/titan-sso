using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System;

namespace SSO.Core.Context
{
    public class SSOConfigDbContext : ConfigurationDbContext<SSOConfigDbContext>
    {
        private readonly ConfigurationStoreOptions storeOptions;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="storeOptions">The store options.</param>
        /// <exception cref="ArgumentNullException">storeOptions</exception>
        public SSOConfigDbContext(DbContextOptions<SSOConfigDbContext> options, ConfigurationStoreOptions storeOptions)
            : base(options, storeOptions)
        {
            this.storeOptions = storeOptions ?? throw new ArgumentNullException(nameof(storeOptions));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureClientContext(storeOptions);
            modelBuilder.ConfigureResourcesContext(storeOptions);
            base.OnModelCreating(modelBuilder);
        }
    }
}
