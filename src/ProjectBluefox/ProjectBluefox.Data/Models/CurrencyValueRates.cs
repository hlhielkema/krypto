using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBluefox.Data.Models
{
    public sealed class CurrencyValueRates
    {   
        public string Symbol { get; set; }

        public int Rank { get; set; }
        
        public decimal PriceUsd { get; set; }
        
        public decimal VolumeUsd24h { get; set; }
        
        public decimal MarketCapUsd { get; set; }
        
        public decimal AvailableSupply { get; set; }
        
        public decimal TotalSupply { get; set; }
        
        public decimal? MaxSupply { get; set; }
        
        public double? PercentChange1h { get; set; }
        
        public double? PercentChange24h { get; set; }
        
        public DateTime LastUpdated { get; set; }
        
        public decimal PriceEur { get; set; }
        
        public decimal VolumeEur24h { get; set; }
        
        public decimal MarketCapEur { get; set; }

        public override string ToString()
            => Symbol;

        public string FormattedLastUpdated
         => LastUpdated.ToString("dd-MM-yyyy HH:mm");
    }
}
