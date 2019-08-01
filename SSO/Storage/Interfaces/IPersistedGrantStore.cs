using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace SSO.Storage
{
    public interface IPersistedGrantStore : IBaseStore, IDisposable
    {
        DbSet<DeviceFlowCodes> DeviceFlowCodes { get; }
        DbSet<PersistedGrant> PersistedGrants { get; }
    }
}