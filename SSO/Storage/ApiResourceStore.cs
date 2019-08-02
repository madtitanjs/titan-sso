using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core;
using SSO.Core.Context;
using SSO.Core.DTO;
using SSO.Core.Mapper;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ApiResourceDTO>> Search(SearchDTO search)
        {
            var query = from api in Apis
            .Include(x => x.UserClaims)
            .Include(o => o.Secrets)
            .Include(o => o.Scopes)
            .ThenInclude(s => s.UserClaims)
                        orderby api.Name
                        select api;

            if (!string.IsNullOrWhiteSpace(search.Search))
            {
                query = from obj in query
                        where obj.Name.Contains(search.Search)
                        orderby obj.Name
                        select obj;
            }

            return await query.Select(s => s.ToDTO()).ToArrayAsync();
        }
        public DbSet<ApiResource> Apis { get { return Context.Set<ApiResource>(); } }
        public DbSet<ApiResourceClaim> ApiClaims { get { return Context.Set<ApiResourceClaim>(); } }
        public DbSet<ApiResourceProperty> ApiProperties { get { return Context.Set<ApiResourceProperty>(); } }
        public DbSet<ApiSecret> ApiSecrets { get { return Context.Set<ApiSecret>(); } }
        public DbSet<ApiScope> ApiScopes { get { return Context.Set<ApiScope>(); } }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get { return Context.Set<ApiScopeClaim>(); } }
    }
}
