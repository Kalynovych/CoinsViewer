using Newtonsoft.Json;

namespace CoinsViewer.API.CoinCap.Model
{
    public class AssetsRequest
    {
        [JsonProperty("search")]
        public string Search { get; set; } = string.Empty;

        [JsonProperty("ids")]
        public string MultipleIds { get; set; } = string.Empty;

        [JsonProperty("offset")]
        public uint Skip { get; set; }

        [JsonProperty("limit")]
        public uint Count { get; set; }
    }
}
