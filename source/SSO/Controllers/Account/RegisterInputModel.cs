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
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
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

        [Required(ErrorMessage = "Phone number is required")]
        [MaxLength(12, ErrorMessage ="Phone number must not exceed 12 digits. 09xxxxxxxxx or 639xxxxxxxxx")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        public int BirthdayDate { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        public int BirthdayMonth { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        public int BirthdayYear { get; set; }

        public string Address { get; set; }
    }
}
