using Microsoft.EntityFrameworkCore;
using SSO.Core.DTO;
using System.Threading.Tasks;
using System.Linq;
using SSO.Core.Mapper;
using SSO.Core;
using System;
using SSO.Core.IdentityServer;
using IdentityServer4.EntityFramework.Entities;
using SSO.Core.Context;

namespace Services
{
    public class ClientService : IClientService
    {
        private readonly SSOConfigDbContext Context;

        public ClientService(SSOConfigDbContext context)
        {
            Context = context;
        }

        public async Task<PagedQuery<ClientDTO>> SearchClient(SearchDTO searchDTO)
        {
            var query = from client in Clients
                        .Include(c => c.ClientSecrets)
                        .Include(c => c.AllowedCorsOrigins)
                        .Include(c => c.AllowedGrantTypes)
                        .Include(c => c.AllowedScopes)
                        .Include(c => c.Claims)
                        .Include(c => c.PostLogoutRedirectUris)
                        .Include(c => c.RedirectUris)
                        .Include(c => c.IdentityProviderRestrictions)
                        orderby client.ClientName
                        select client;
            
            if(!string.IsNullOrWhiteSpace(searchDTO.Search))
            {
                query = from client in query
                        where client.ClientName.Contains(searchDTO.Search) || client.ClientId.Contains(searchDTO.Search)
                        orderby client.ClientName
                        select client;
            }

            int total = query.Count();
            var clients = await query.Skip(searchDTO.Start).Take(searchDTO.Count).ToArrayAsync();

            var result = new PagedQuery<ClientDTO>
            {
                Start = searchDTO.Start,
                Count = searchDTO.Count,
                Total = total,
                Items = clients.Select(i => i.ToDTO()),
                Search = searchDTO.Search
            };

            return result;
        }

        public async Task<ClientDTO> GetClientAsync(int id)
        {
            var query = from client in Clients
                        .Include(c => c.ClientSecrets)
                        .Include(c => c.AllowedCorsOrigins)
                        .Include(c => c.AllowedGrantTypes)
                        .Include(c => c.AllowedScopes)
                        .Include(c => c.Claims)
                        .Include(c => c.PostLogoutRedirectUris)
                        .Include(c => c.RedirectUris)
                        .Include(c => c.IdentityProviderRestrictions)
                        where client.Id == id
                        select client;

            var result = await query.FirstOrDefaultAsync();

            return result.ToDTO();
        }

        public async Task<ClientDTO> AddClientAsync(ClientDTO dto)
        {
            if (!await CanInsertAsync(dto)) throw new Exception(Constants.Errors.CantInsert);

            PrepareClientTypeForNewClient(dto);
            var entity = dto.ToEntity();
            
            if(entity.AllowedGrantTypes != null)
            {
                var grantTypes = Constants.Client.GrantTypes;
                var notInScope = entity.AllowedGrantTypes
                                    .Select(a => a.GrantType)
                                    .Except(grantTypes)
                                    .Any();
                if (notInScope)
                    throw new Exception(Constants.Errors.InvalidGrantType);
            }

            await Clients.AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity.ToDTO();
        }

        public async Task<ClientDTO> UpdateClientAsync(ClientDTO dto)
        {
            if (!await CanInsertAsync(dto)) throw new Exception(Constants.Errors.CantInsert);

            var entity = dto.ToEntity();
            if (entity.AllowedGrantTypes != null)
            {
                var grantTypes = Constants.Client.GrantTypes;
                var notInScope = entity.AllowedGrantTypes
                                    .Select(a => a.GrantType)
                                    .Except(grantTypes)
                                    .Any();
                if (notInScope)
                    throw new Exception(Constants.Errors.InvalidGrantType);
            }

            await RemoveClientRelationsAsync(dto.Id);
            Clients.Update(entity);
            await Context.SaveChangesAsync();

            return entity.ToDTO();
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var entity = Clients.Find(clientId);
            if(Context.Entry(entity).State == EntityState.Detached)
            {
                Clients.Attach(entity);
            }
            Clients.Remove(entity);
            await Context.SaveChangesAsync();
        }

        private async Task RemoveClientRelationsAsync(int clientId)
        {
            //Remove old claims
            var clientClaims = await ClientClaims.Where(x => x.Client.Id == clientId).ToListAsync();
            ClientClaims.RemoveRange(clientClaims);

            //Remove old allowed scopes
            var clientScopes = await ClientScopes.Where(x => x.Client.Id == clientId).ToListAsync();
            ClientScopes.RemoveRange(clientScopes);

            //Remove old grant types
            var clientGrantTypes = await ClientGrantTypes.Where(x => x.Client.Id == clientId).ToListAsync();
            ClientGrantTypes.RemoveRange(clientGrantTypes);

            //Remove old redirect uri
            var clientRedirectUris = await ClientRedirectUris.Where(x => x.Client.Id == clientId).ToListAsync();
            ClientRedirectUris.RemoveRange(clientRedirectUris);

            //Remove old client cors
            var clientCorsOrigins = await ClientCorsOrigins.Where(x => x.Client.Id == clientId).ToListAsync();
            ClientCorsOrigins.RemoveRange(clientCorsOrigins);

            //Remove old client id restrictions
            var clientIdPRestrictions = await ClientIdPRestrictions.Where(x => x.Client.Id == clientId).ToListAsync();
            ClientIdPRestrictions.RemoveRange(clientIdPRestrictions);

            //Remove old client post logout redirect
            var clientPostLogoutRedirectUris = await ClientPostLogoutRedirectUris.Where(x => x.Client.Id == clientId).ToListAsync();
            ClientPostLogoutRedirectUris.RemoveRange(clientPostLogoutRedirectUris);
        }

        private async Task<bool> CanInsertAsync(ClientDTO dto)
        {
            var query = await Clients.Where(x => x.ClientId == dto.ClientId && x.Id != dto.Id).SingleOrDefaultAsync();
            return query == null;
        }

        private void PrepareClientTypeForNewClient(ClientDTO client)
        {
            switch (client.ClientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.WebImplicit:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Implicit);
                    client.AllowAccessTokensViaBrowser = true;
                    break;
                case ClientType.WebHybrid:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Hybrid);
                    break;
                case ClientType.Spa:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Implicit);
                    client.AllowAccessTokensViaBrowser = true;
                    break;
                case ClientType.Native:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Hybrid);
                    break;
                case ClientType.Machine:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.ResourceOwnerPasswordAndClientCredentials);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        #region STORE PROPS
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
