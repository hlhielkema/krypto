using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Models
{
    public class NavigationMenuButton
    {
        public string DisplayName { get; set; }

        public string Icon { get; set; } = "far fa-circle";

        public string Url { get; set; }
        

        public bool IsActive { get; set; }
    }
}