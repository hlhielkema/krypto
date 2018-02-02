using System.ComponentModel.DataAnnotations;

namespace ProjectBluefox.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The password needs to be between 8 and 50 charachters long.")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The password needs to be between 8 and 50 charachters long.")]
        public string NewPassword { get; set; }
    }
}