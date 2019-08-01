using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Context;
using SSO.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSO.Storage
{
    public class PersistedGrantStore : BaseStore, IPersistedGrantStore
    {
        public PersistedGrantStore(SSOPersistDbContext context)
        {
            Context = context;
        }

        public DbSet<PersistedGrant> PersistedGrants { get { return Context.Set<PersistedGrant>(); } }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get { return Context.Set<DeviceFlowCodes>(); } }

    }
}
