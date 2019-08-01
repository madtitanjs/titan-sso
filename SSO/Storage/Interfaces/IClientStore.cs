using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace SSO.Storage
{
    public interface IClientStore : IBaseStore, IDisposable
    {
        DbSet<ClientClaim> ClientClaims { get; }
        DbSet<ClientCorsOrigin> ClientCorsOrigins { get; }
        DbSet<ClientGrantType> ClientGrantTypes { get; }
        DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; }
        DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; }
        DbSet<ClientProperty> ClientProperties { get; }
        DbSet<ClientRedirectUri> ClientRedirectUris { get; }
        DbSet<Client> Clients { get; }
        DbSet<ClientScope> ClientScopes { get; }
        DbSet<ClientSecret> ClientSecrets { get; }
    }
}