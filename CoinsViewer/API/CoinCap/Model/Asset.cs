using Newtonsoft.Json;

namespace CoinsViewer.API.CoinCap.Model
{
    public class Asset
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("supply")]
        public double? Supply { get; set; }

        [JsonProperty("maxSupply")]
        public double? MaxSupply { get; set; }

        [JsonProperty("marketCapUsd")]
        public double? MarketCapUSD { get; set; }

        [JsonProperty("volumeUsd24Hr")]
        public double? Volume { get; set; }

        [JsonProperty("priceUsd")]
        public double? PriceUSD { get; set; }

        [JsonProperty("changePercent24Hr")]
        public double? ChangePercent { get; set; }

        [JsonProperty("vwap24Hr")]
        public double? VolumeWeightedAveragePrice { get; set; }

        [JsonProperty("explorer")]
        public string Explorer { get; set; }
    }
}
