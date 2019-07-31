using Microsoft.AspNetCore.Identity;
using SSO.Core.Context;
using SSO.Core.Identity.Models;

namespace SSO.Storage
{
    public interface IIdentityStore
    {
        SSODbContext Context { get; }
        RoleManager<Role> RoleManager { get; }
        UserManager<User> UserManager { get; }

        void Dispose();
    }
}