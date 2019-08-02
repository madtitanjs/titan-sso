using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core;
using SSO.Core.Context;
using SSO.Core.DTO;
using SSO.Core.Mapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class IdentityResourceService : IIdentityResourceService
    {
        private readonly SSOConfigDbContext Context;

        public IdentityResourceService(SSOConfigDbContext context)
        {
            Context = context;
        }

        public async Task<PagedQuery<IdentityResourceDTO>> SearchAsync(SearchDTO search)
        {
            var query = from ir in IdentityResources
                        .Include(c => c.UserClaims)
                        orderby ir.Name
                        select ir;

            if (!string.IsNullOrWhiteSpace(search.Search))
            {
                query = from obj in query
                        where obj.Name.Contains(search.Search)
                        orderby obj.Name
                        select obj;
            }

            var items = await query.Select(s => s.ToDTO()).ToListAsync();

            var result = new PagedQuery<IdentityResourceDTO>
            {
                Count = search.Count,
                Items = items,
                Start = search.Start,
                Total = items.Count()
            };

            return result;
        }

        public async Task<IdentityResourceDTO> GetAsync(int id)
        {
            var query = from api in IdentityResources
                        .Include(c => c.UserClaims)
                        where api.Id == id
                        select api;

            var result = await query.FirstOrDefaultAsync();

            return result.ToDTO();
        }

        /// <summary>
        /// Add new identity resource
        /// </summary>
        /// <param name="identityResource"></param>
        /// <returns>This method return new identity resource id</returns>
        public async Task<IdentityResourceDTO> AddIdentityResourceAsync(IdentityResourceDTO dto)
        {
            var entity = dto.ToEntity();
            var canInsert = await CanInsertIdentityResourceAsync(entity);
            if (!canInsert)
            {
                throw new Exception(Constants.Errors.CantInsert);
            }
           
            IdentityResources.Add(entity);

            await Context.SaveChangesAsync();

            return entity.ToDTO();
        }

        /// <summary>
        /// Update identity resource
        /// </summary>
        /// <returns>This method return new identity resource id</returns>
        public async Task<IdentityResourceDTO> UpdateIdentityResourceAsync(IdentityResourceDTO dto)
        {
            var entity = dto.ToEntity();
            var canInsert = await CanInsertIdentityResourceAsync(entity);
            if (!canInsert)
            {
                throw new Exception(Constants.Errors.CantInsert);
            }

            IdentityResources.Attach(entity);
            await CleanupIdentityResourceAsync(entity);
            IdentityResources.Update(entity);
            await Context.SaveChangesAsync();
            return entity.ToDTO();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = IdentityResources.Find(id);
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                IdentityResources.Attach(entity);
            }
            IdentityResources.Remove(entity);
            await Context.SaveChangesAsync();
        }

        private async Task<bool> CanInsertIdentityResourceAsync(IdentityResource entity)
        {
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

        private async Task CleanupIdentityResourceAsync(IdentityResource entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var identityClaims = await IdentityClaims.Where(x => x.IdentityResource.Id == entity.Id).ToListAsync();
            IdentityClaims.RemoveRange(identityClaims);
        }

        public DbSet<IdentityResource> IdentityResources { get { return Context.Set<IdentityResource>(); } }
        public DbSet<IdentityResourceProperty> IdentityResourceProperties { get { return Context.Set<IdentityResourceProperty>(); } }
        public DbSet<IdentityClaim> IdentityClaims { get { return Context.Set<IdentityClaim>(); } }
    }
}
