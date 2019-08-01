using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace SSO.Storage
{
    public interface IIdentityResourceStore : IBaseStore, IDisposable
    {
        DbSet<IdentityClaim> IdentityClaims { get; }
        DbSet<IdentityResourceProperty> IdentityResourceProperties { get; }
        DbSet<IdentityResource> IdentityResources { get; }
    }
}