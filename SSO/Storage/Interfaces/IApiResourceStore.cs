using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace SSO.Storage
{
    public interface IApiResourceStore : IBaseStore, IDisposable
    {
        DbSet<ApiResourceClaim> ApiClaims { get; }
        DbSet<ApiResourceProperty> ApiProperties { get; }
        DbSet<ApiResource> Apis { get; }
        DbSet<ApiScopeClaim> ApiScopeClaims { get; }
        DbSet<ApiScope> ApiScopes { get; }
        DbSet<ApiSecret> ApiSecrets { get; }
    }
}