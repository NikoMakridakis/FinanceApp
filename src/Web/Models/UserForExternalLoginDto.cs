using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Web.Models
{
    public class UserForExternalLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}
