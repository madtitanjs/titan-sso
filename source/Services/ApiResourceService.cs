using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Context;
using SSO.Core.DTO;
using SSO.Core.Mapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class ApiResourceService : IApiResourceService
    {
        private const string SharedSecret = "SharedSecret";

        public ApiResourceService(SSOConfigDbContext context)
        {
            Context = context;
        }

        public async Task<PagedQuery<ApiResourceDTO>> SearchAsync(SearchDTO search)
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

            var items = await query.Select(s => s.ToDTO()).ToListAsync();

            var result = new PagedQuery<ApiResourceDTO>
            {
                Count = search.Count,
                Items = items,
                Start = search.Start,
                Total = items.Count()
            };

            return result;
        }

        public async Task<ApiResourceDTO> GetAsync(int id)
        {
            var query = from api in Apis
                        .Include(x => x.UserClaims)
                        .Include(o => o.Secrets)
                        .Include(o => o.Scopes)
                        .ThenInclude(s => s.UserClaims)
                        where api.Id == id
                        select api;

            var result = await query.FirstOrDefaultAsync();

            return result.ToDTO();
        }

        public async Task<ApiResourceDTO> AddAsync(ApiResourceDTO dto)
        {
            var entity = dto.ToEntity();
            await Apis.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity.ToDTO();
        }

        public async Task<ApiResourceDTO> UpdateAsync(ApiResourceDTO dto)
        {
            var entity = dto.ToEntity();
            Apis.Attach(entity);
            await CleanupApiResourceAsync(entity);
            Apis.Update(entity);
            await Context.SaveChangesAsync();
            return entity.ToDTO();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = Apis.Find(id);
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Apis.Attach(entity);
            }
            Apis.Remove(entity);
            await Context.SaveChangesAsync();
        }

        private async Task CleanupApiResourceAsync(ApiResource entity, CancellationToken cancellationToken = default(CancellationToken))
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
        }

        private readonly SSOConfigDbContext Context;
        public DbSet<ApiResource> Apis { get { return Context.Set<ApiResource>(); } }
        public DbSet<ApiResourceClaim> ApiClaims { get { return Context.Set<ApiResourceClaim>(); } }
        public DbSet<ApiResourceProperty> ApiProperties { get { return Context.Set<ApiResourceProperty>(); } }
        public DbSet<ApiSecret> ApiSecrets { get { return Context.Set<ApiSecret>(); } }
        public DbSet<ApiScope> ApiScopes { get { return Context.Set<ApiScope>(); } }
        public DbSet<ApiScopeClaim> ApiScopeClaims { get { return Context.Set<ApiScopeClaim>(); } }
    }
}
