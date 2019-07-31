using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SSO.Core.Identity.Models
{
    [Table("IdentityUser")]
    public class User : IdentityUser<Guid>
    {
        public bool HasPassword { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserLogin> Logins { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserClaim> Claims { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserToken> Tokens { get; set; }
    }
}
