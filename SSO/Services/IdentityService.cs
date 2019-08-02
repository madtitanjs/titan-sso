using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Context;
using SSO.Core.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class IdentityService
    {
        public UserManager<User> UserManager { get; private set; }
        public RoleManager<Role> RoleManager { get; private set; }

        private readonly SSODbContext Context;

        public IdentityService(UserManager<User> userManager, RoleManager<Role> roleManager, SSODbContext context)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            Context = context;
        }

        private DbSet<User> Users { get { return Context.Set<User>(); } }
        private DbSet<Role> Roles { get { return Context.Set<Role>(); } }
        private DbSet<UserClaim> UserClaims { get { return Context.Set<UserClaim>(); } }
        private DbSet<UserToken> UserTokens { get { return Context.Set<UserToken>(); } }
        private DbSet<UserLogin> UserLogins { get { return Context.Set<UserLogin>(); } }
        private DbSet<RoleClaim> RoleClaims { get { return Context.Set<RoleClaim>(); } }
    }
}
