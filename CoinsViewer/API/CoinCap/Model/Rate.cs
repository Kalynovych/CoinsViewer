using Newtonsoft.Json;

namespace CoinsViewer.API.CoinCap.Model
{
    public class Rate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("currencySymbol")]
        public string CurrencySymbol { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rateUsd")]
        public double RateUsd { get; set; }
    }
}
