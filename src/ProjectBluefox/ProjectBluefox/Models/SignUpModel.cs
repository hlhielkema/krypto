using ProjectBluefox.Misc;
using System.ComponentModel.DataAnnotations;

namespace ProjectBluefox.Models
{
    public class SignUpModel
    {
        [Required]
        [StringLength(8, MinimumLength = 8)]
        public string Token { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "The username needs to be between 5 and 50 charachters long.")]
        [ValidUsername]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The password needs to be between 8 and 50 charachters long.")]
        public string Password { get; set; }
    }
}