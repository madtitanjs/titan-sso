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
    public class ApiResourceStore : BaseStore, IApiResourceStore
    {
        public ApiResourceStore(SSOConfigDbContext context)
        {
            Context = context;
        }
        public DbSet<ApiResource> Apis { get { return Context.Set<ApiResource>(); } }
        public DbSet<ApiResourceClaim> ApiClaims { get { return Context.Set<ApiResourceClaim>(); } }
        public DbSet<ApiResourceProperty> ApiProperties { get { return Context.Set<ApiResourceProperty>(); } }
        public DbSet<ApiSecret> ApiSecrets { get { return Context.Set<ApiSecret>(); } }
        public DbSet<ApiScope> ApiScopes { get { return Context.Set<ApiScope>(); } }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get { return Context.Set<ApiScopeClaim>(); } }
    }
}
