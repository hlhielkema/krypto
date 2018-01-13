using System;

namespace ProjectBluefox.Models
{
    public class CommentModel
    {        
        public Guid Id { get; set; }
                
        public string CreatedBy { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public int Vote { get; set; } // +1, 0 or -1
        
        public string Message { get; set; }

        public string FormattedDateCreated
            => DateCreated.ToString("dd-MM-yyyy hh:mm");
    }
}