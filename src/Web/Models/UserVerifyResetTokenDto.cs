using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class UserVerifyResetTokenDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string ResetToken { get; set; }
    }
}
