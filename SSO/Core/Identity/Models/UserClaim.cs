using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SSO.Core.Identity.Models
{
    [Table("IdentityUserClaim")]
    public class UserClaim : IdentityUserClaim<Guid>
    {
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
