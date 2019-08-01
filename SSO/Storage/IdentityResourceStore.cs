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
    public class IdentityResourceStore : BaseStore, IIdentityResourceStore
    {
        public IdentityResourceStore(SSOConfigDbContext context)
        {
            Context = context;
        }

        public DbSet<IdentityResource> IdentityResources { get { return Context.Set<IdentityResource>(); } }
        public DbSet<IdentityResourceProperty> IdentityResourceProperties { get { return Context.Set<IdentityResourceProperty>(); } }
        public DbSet<IdentityClaim> IdentityClaims { get { return Context.Set<IdentityClaim>(); } }
        
    }
}
