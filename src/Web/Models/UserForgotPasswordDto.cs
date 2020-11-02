using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class UserForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
