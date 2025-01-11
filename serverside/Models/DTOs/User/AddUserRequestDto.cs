using System.ComponentModel.DataAnnotations;

namespace serverside.Models.DTOs.User
{
    public class AddUserRequestDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Username must not exceed 100 characters.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Role must not exceed 50 characters.")]
        public string Role { get; set; }
        public int ReputationPoints { get; set; }

    }

}
