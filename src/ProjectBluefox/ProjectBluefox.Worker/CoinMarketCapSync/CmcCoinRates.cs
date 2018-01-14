using Newtonsoft.Json;
using ProjectBluefox.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBluefox.Worker.CoinMarketCapSync
{
    internal sealed class CmcCoinRates
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "price_usd")]
        public decimal PriceUsd { get; set; }

        [JsonProperty(PropertyName = "24h_volume_usd")]
        public decimal VolumeUsd24h { get; set; }

        [JsonProperty(PropertyName = "market_cap_usd")]
        public decimal MarketCapUsd { get; set; }

        [JsonProperty(PropertyName = "available_supply")]
        public decimal AvailableSupply { get; set; }

        [JsonProperty(PropertyName = "total_supply")]
        public decimal TotalSupply { get; set; }

        [JsonProperty(PropertyName = "max_supply")]
        public decimal? MaxSupply { get; set; }

        [JsonProperty(PropertyName = "percent_change_1h")]
        public double? PercentChange1h { get; set; }

        [JsonProperty(PropertyName = "percent_change_24h")]
        public double? PercentChange24h { get; set; }

        [JsonProperty(PropertyName = "last_updated")]
        public double LastUpdated { get; set; }

        [JsonProperty(PropertyName = "price_eur")]
        public decimal PriceEur { get; set; }

        [JsonProperty(PropertyName = "24h_volume_eur")]
        public decimal VolumeEur24h { get; set; }

        [JsonProperty(PropertyName = "market_cap_eur")]
        public decimal MarketCapEur { get; set; }

        public CurrencyValueRates ToModel()
        {
            return new CurrencyValueRates()
            {
                Symbol = Symbol.ToUpper(),
                Rank = Rank,
                PriceUsd = PriceUsd,
                VolumeUsd24h = VolumeUsd24h,
                MarketCapUsd = MarketCapUsd,
                AvailableSupply = AvailableSupply,
                TotalSupply = TotalSupply,
                MaxSupply = MaxSupply,
                PercentChange1h = PercentChange1h,
                PercentChange24h = PercentChange24h,
                LastUpdated = UnixTimeStampToDateTime(LastUpdated),
                PriceEur = PriceEur,
                VolumeEur24h = VolumeEur24h,
                MarketCapEur = MarketCapEur,
            };
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}