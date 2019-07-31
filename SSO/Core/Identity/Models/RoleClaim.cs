using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Identity.Models
{
    [Table("IdentityRoleClaim")]
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
