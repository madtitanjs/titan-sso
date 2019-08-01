using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SSO.Core.Identity.Models;
using System;

namespace SSO.Storage
{
    public interface IIdentityStore : IBaseStore, IDisposable
    {
        DbSet<RoleClaim> RoleClaims { get; }
        RoleManager<Role> RoleManager { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserClaim> UserClaims { get; }
        DbSet<UserLogin> UserLogins { get; }
        UserManager<User> UserManager { get; }
        DbSet<User> Users { get; }
        DbSet<UserToken> UserTokens { get; }
    }
}