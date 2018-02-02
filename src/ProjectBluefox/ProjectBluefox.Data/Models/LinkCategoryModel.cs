using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Models
{
    public class LinkCategoryModel
    {
        public string Title { get; set; }

        public List<LinkModel> Items { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public string FormattedDateCreated
           => DateCreated.ToString("dd-MM-yyyy HH:mm");
    }
}