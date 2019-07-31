using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Identity.Models
{
    [Table("IdentityUserToken")]
    public class UserToken : IdentityUserToken<Guid>
    {
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
