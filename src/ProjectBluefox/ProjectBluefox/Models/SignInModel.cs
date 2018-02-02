using System.ComponentModel.DataAnnotations;

namespace ProjectBluefox.Models
{
    public class SignInModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}