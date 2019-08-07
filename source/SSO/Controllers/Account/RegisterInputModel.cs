using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SSO.Idsrv
{
    [Serializable]
    public class BasicRegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string LoginProvider { get; set; }
        public string ProviderName { get; set; }
        public string ProviderKey { get; set; }

        public IEnumerable<Claim> claims { get; set; }
    }

    public class RegisterInputModel : BasicRegisterViewModel
    {
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public int BirthdayDate { get; set; }

        public int BirthdayMonth { get; set; }
       
        public int BirthdayYear { get; set; }

        public string Address { get; set; }
    }
}
