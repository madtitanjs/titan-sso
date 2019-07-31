using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SSO.Core.Identity.Models
{
    [Table("IdentityRole")]
    public class Role : IdentityRole<Guid>
    {
        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
        public Guid ApiResourceID { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
