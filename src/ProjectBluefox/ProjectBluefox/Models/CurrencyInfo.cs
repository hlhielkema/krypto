using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBluefox.Models
{
    public class CurrencyInfo
    {        
        public Guid Id { get; set; }
        
        public string DisplayName { get; set; }
        
        public string ShortCode { get; set; }

        public DateTime DateCreated { get; set; }

        public string CreatedBy { get; set; }

        public int Score { get; set; }

        public int RecentComments { get; set; }

        public int TotalComments { get; set; }

        public string FormattedDateCreated
            => DateCreated.ToString("dd-MM-yyyy hh:mm");
    }
}