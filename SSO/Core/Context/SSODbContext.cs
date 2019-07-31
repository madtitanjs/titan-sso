using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Identity.Models;
using System;

namespace SSO.Core.Context
{
    public class SSODbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public SSODbContext(DbContextOptions<SSODbContext> options)
            : base(options)
        {
        }
    }
}
