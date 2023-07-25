using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            return await GetListOfData<Asset>(endPoint);
        }

        public async Task<List<ExchangeMarket>> GetExchange(int count)
        {
            Uri endPoint = new Uri(_baseEndpoint, "exchanges");
            return (await GetListOfData<ExchangeMarket>(endPoint)).Take(count).ToList();
        }

        public async Task<List<HistoricalPrice>> GetHistory(string id, string interval)
        {
            string pathWithQuery = $"assets/{id}/history?interval={interval}";
            Uri endPoint = new Uri(_baseEndpoint, pathWithQuery);

            return await GetListOfData<HistoricalPrice>(endPoint);
        }

        public async Task<List<HistoricalPrice>> GetHistory(string id, string interval, int period)
        {
            var startAndEnd = GetPeriod(period);
            string pathWithQuery = $"assets/{id}/history?interval={interval}&start={startAndEnd.Item1}&end={startAndEnd.Item2}";
            Uri endPoint = new Uri(_baseEndpoint, pathWithQuery);

            return await GetListOfData<HistoricalPrice>(endPoint);
        }

        private async Task<List<T>> GetListOfData<T>(Uri endPoint)
        {
            var response = await _client.GetAsync(endPoint);
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            ResponseArray<T> assetsArray = JsonConvert.DeserializeObject<ResponseArray<T>>(responseData);
            return assetsArray.Array;
        }

        private (string, string) GetPeriod(int period)
        {
            DateTime? startDate = null;
            DateTime endDate = DateTime.Now;

            switch (period)
            {
                case Intervals.year:
                    startDate = DateTime.Now.AddYears(-1);
                    break;
                case Intervals.month:
                    startDate = DateTime.Now.AddMonths(-1);
                    break;
                case Intervals.week:
                    startDate = DateTime.Now.AddDays(-8);
                    break;
                default:
                    return (string.Empty, string.Empty);
            }

            long start = ((DateTimeOffset)startDate).ToUnixTimeMilliseconds();
            long end = ((DateTimeOffset)endDate).ToUnixTimeMilliseconds();
            return (start.ToString(), end.ToString());
        }

    }

    public struct Intervals
    {
        public const string day = "d1";
        public const string hour = "h1";
        public const string minute = "m1";
        public const string halfAnHour = "m30";
        public const string halfADay = "h12";
        public const int week = (int)ExtendedIntervals.week;
        public const int month = (int)ExtendedIntervals.month;
        public const int year = (int)ExtendedIntervals.year;

        private enum ExtendedIntervals
        {
            week,
            month,
            year
        }
    }
}
