using System;
using Newtonsoft.Json;

namespace CoinsViewer.API.CoinCap.Model
{
    public class ExchangeMarket
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rank")]
        public uint Rank { get; set; }

        [JsonProperty("percentTotalVolume")]
        public double? PercentTotalVolume { get; set; }

        [JsonProperty("volumeUsd")]
        public double? VolumeUsd { get; set; }

        [JsonProperty("tradingPairs")]
        public uint TraidingPairs { get; set; }

        [JsonProperty("exchangeUrl")]
        public string ExchangeUrl { get; set; }

        [JsonProperty("updated")]
        public long Timespan { get; set; }

        public DateTime Updated
        {
            get
            {
                return GetDateTime();
            }
        }

        private DateTime GetDateTime()
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(Timespan).DateTime;
        }
    }
}
