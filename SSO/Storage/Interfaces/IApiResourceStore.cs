using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSO.Storage
{
    public interface IApiResourceStore : IBaseStore, IDisposable
    {
        Task<IEnumerable<ApiResourceDTO>> Search(SearchDTO search);
        DbSet<ApiResourceClaim> ApiClaims { get; }
        DbSet<ApiResourceProperty> ApiProperties { get; }
        DbSet<ApiResource> Apis { get; }
        DbSet<ApiScopeClaim> ApiScopeClaims { get; }
        DbSet<ApiScope> ApiScopes { get; }
        DbSet<ApiSecret> ApiSecrets { get; }
    }
}