using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBluefox.Database.Tables
{    
    [Table(Name = "[Currency.ValueRates]")]
    public class CurrencyValueRatesTable
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public int Currency { get; set; }

        [Column]
        public int Rank { get; set; }

        [Column]
        public decimal PriceUsd { get; set; }

        [Column]
        public decimal VolumeUsd24h { get; set; }

        [Column]
        public decimal MarketCapUsd { get; set; }

        [Column]
        public decimal AvailableSupply { get; set; }

        [Column]
        public decimal TotalSupply { get; set; }

        [Column]
        public decimal? MaxSupply { get; set; }

        [Column]
        public double? PercentChange1h { get; set; }

        [Column]
        public double? PercentChange24h { get; set; }

        [Column]
        public DateTime LastUpdated { get; set; }

        [Column]
        public decimal PriceEur { get; set; }

        [Column]
        public decimal VolumeEur24h { get; set; }

        [Column]
        public decimal MarketCapEur { get; set; }
    }
}
