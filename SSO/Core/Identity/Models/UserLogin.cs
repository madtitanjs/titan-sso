using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSO.Core.Identity.Models
{
    [Table("IdentityUserLogin")]
    public class UserLogin : IdentityUserLogin<Guid>
    {
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
