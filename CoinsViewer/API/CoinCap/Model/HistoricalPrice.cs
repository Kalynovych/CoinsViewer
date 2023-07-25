using System;
using Newtonsoft.Json;

namespace CoinsViewer.API.CoinCap.Model
{
    public class HistoricalPrice
    {
        [JsonProperty("priceUsd")]
        public double Price { get; set; }

        [JsonProperty("time")]
        public long Timestamp { get; set; }

        public DateTime DateAndTime { 
            get
            {
                return GetDateTime();
            }
        }

        private DateTime GetDateTime()
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime;
        }
    }
}
