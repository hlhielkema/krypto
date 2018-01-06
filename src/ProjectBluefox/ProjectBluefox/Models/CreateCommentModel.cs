using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectBluefox.Models
{
    public class CreateCommentModel
    {
        [Required]
        public Guid Currency { get; set; }
     
        [Range(-1, 1)]
        public int Vote { get; set; }

        [Required]
        [MaxLength(2000)]        
        public string Message { get; set; }
    }
}