using System.ComponentModel.DataAnnotations;

namespace serverside.Models.DTOs.User
{
    public class LoginUserRequestDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

    }
}
