using Microsoft.EntityFrameworkCore;
using SSO.Core.DTO;
using SSO.Storage;
using System.Threading.Tasks;
using System.Linq;
using SSO.Core.Mapper;
using SSO.Core;
using System;
using SSO.Core.IdentityServer;
using IdentityServer4.Models;

namespace Services
{
    public class ClientService
    {
        private readonly IClientStore store;

        public ClientService(IClientStore store)
        {
            this.store = store;
        }

        public async Task<PagedQuery<ClientDTO>> SearchClient(SearchDTO search)
        {
            var query = from client in store.Clients
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
            
            if(!string.IsNullOrWhiteSpace(search.Search))
            {
                query = from client in query
                        where client.ClientName.Contains(search.Search) || client.ClientId.Contains(search.Search)
                        orderby client.ClientName
                        select client;
            }

            int total = query.Count();
            var clients = await query.Skip(search.Start).Take(search.Count).ToArrayAsync();

            var result = new PagedQuery<ClientDTO>
            {
                Start = search.Start,
                Count = search.Count,
                Total = total,
                Items = clients.Select(i => i.ToDTO()),
                Search = search.Search
            };

            return result;
        }

        public async Task<ClientDTO> GetClientAsync(int id)
        {
            var query = from client in store.Clients
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

            await store.Clients.AddAsync(entity);
            await store.SaveChangesAsync();

            return entity.ToDTO();
        }

        private async Task<bool> CanInsertAsync(ClientDTO dto)
        {
            var query = await store.Clients.Where(x => x.ClientId == dto.ClientId && x.Id != dto.Id).SingleOrDefaultAsync();
            return query == null;
        }

        private void PrepareClientTypeForNewClient(ClientDTO client)
        {
            switch (client.ClientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.WebImplicit:
                    client.AllowedGrantTypes.AddRange(GrantTypes.Implicit);
                    client.AllowAccessTokensViaBrowser = true;
                    break;
                case ClientType.WebHybrid:
                    client.AllowedGrantTypes.AddRange(GrantTypes.Hybrid);
                    break;
                case ClientType.Spa:
                    client.AllowedGrantTypes.AddRange(GrantTypes.Implicit);
                    client.AllowAccessTokensViaBrowser = true;
                    break;
                case ClientType.Native:
                    client.AllowedGrantTypes.AddRange(GrantTypes.Hybrid);
                    break;
                case ClientType.Machine:
                    client.AllowedGrantTypes.AddRange(GrantTypes.ResourceOwnerPasswordAndClientCredentials);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
