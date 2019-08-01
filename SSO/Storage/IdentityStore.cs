using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Context;
using SSO.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSO.Storage
{
    public class IdentityStore : BaseStore, IIdentityStore
    {
        public UserManager<User> UserManager { get; private set; }
        public RoleManager<Role> RoleManager { get; private set; }


        public IdentityStore(UserManager<User> userManager, RoleManager<Role> roleManager, SSODbContext context)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Context = context;
        }

        public DbSet<User> Users { get { return Context.Set<User>(); } }
        public DbSet<Role> Roles { get { return Context.Set<Role>(); } }
        public DbSet<UserClaim> UserClaims { get { return Context.Set<UserClaim>(); } }
        public DbSet<UserToken> UserTokens { get { return Context.Set<UserToken>(); } }
        public DbSet<UserLogin> UserLogins { get { return Context.Set<UserLogin>(); } }
        public DbSet<RoleClaim> RoleClaims { get { return Context.Set<RoleClaim>(); } }
        
    }
}
