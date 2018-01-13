using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Models
{
    public class LinkModel
    {
        public string Title { get; set; }

        public string Url { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime DateCreated { get; set; }

        public string FormattedDateCreated
           => DateCreated.ToString("dd-MM-yyyy hh:mm");
    }
}