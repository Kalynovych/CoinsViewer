using System.Collections.Generic;
using Newtonsoft.Json;

namespace CoinsViewer.API.CoinCap.Model
{
    public class ResponseArray<T>
    {
        [JsonProperty("data")]
        public List<T> Array { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}
