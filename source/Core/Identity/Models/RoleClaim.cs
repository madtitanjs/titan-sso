using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Identity.Models
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public virtual Role Role { get; set; }
    }
}
