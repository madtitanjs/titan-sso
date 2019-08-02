using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PersistedGrantService
    {
        private readonly SSOPersistDbContext Context;

        public PersistedGrantService(SSOPersistDbContext context)
        {
            Context = context;
        }

        public DbSet<PersistedGrant> PersistedGrants { get { return Context.Set<PersistedGrant>(); } }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get { return Context.Set<DeviceFlowCodes>(); } }
    }
}
