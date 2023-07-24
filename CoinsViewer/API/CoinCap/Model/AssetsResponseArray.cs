using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoinsViewer.API.CoinCap.Model
{
    public class AssetsResponseArray
    {
        [JsonProperty("data")]
        public List<Asset> AssetsArray { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        public DateTime GetDateTime()
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime;
        }
    }
}
