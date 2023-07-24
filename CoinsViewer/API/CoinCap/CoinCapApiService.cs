using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinsViewer.API.CoinCap.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Windows.Web.Http;

namespace CoinsViewer.API.CoinCap
{
    public class CoinCapApiService
    {
        private readonly Uri _baseEndpoint;
        private readonly HttpClient _client;

        public CoinCapApiService()
        {
            _client = new HttpClient();
            _baseEndpoint = new Uri("https://api.coincap.io/v2/");
        }

        public async Task<List<Asset>> GetAssets(uint count = 10, uint skip = 0, string search = "")
        {
            string pathWithQuery = $"assets?limit={count}&offset={skip}&search={search}";
            Uri endPoint = new Uri(_baseEndpoint, pathWithQuery);

            var response = await _client.GetAsync(endPoint);
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            AssetsResponseArray assetsArray = JsonConvert.DeserializeObject<AssetsResponseArray>(responseData);
            return assetsArray.AssetsArray;
        }
    }
}
