using System.Collections.Generic;
using System.Threading.Tasks;
using CoinsViewer.API.CoinCap.Model;
using CoinsViewer.API.CoinCap;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Activation;
using System;

namespace CoinsViewer
{
    public class PagingController
    {
        public uint CurrentPage { get; private set; }

        public uint Pages { get; private set; }

        public uint AssetsPerPage { get; set; }

        public List<Asset> Assets { get; private set; }

        private short _maxAssets;
        private string _searchQuery;
        private readonly short _maxPerPage;
        private readonly CoinCapApiService _coinCapService;
        

        public PagingController(short maxAssets = 2000)
        {
            _coinCapService = new CoinCapApiService();
            _maxAssets = maxAssets;
            AssetsPerPage = 10;
            CurrentPage = 1;
            Pages = 200;
            _searchQuery = string.Empty;
            _maxPerPage = 200;
        }

        public async Task SetAssetsPerPage()
        {
            CountPages();
            await SetAssets(AssetsPerPage);
        }

        public async Task FirstPage()
        {
            CurrentPage = 1;
            await GetPage();
        }

        public async Task LastPage()
        {
            CurrentPage = Pages;
            await GetPage();
        }

        public async Task NextPage()
        {
            CurrentPage = CurrentPage >= Pages ? Pages : ++CurrentPage;
            await GetPage();
        }

        public async Task PreviousPage()
        {
            CurrentPage = CurrentPage <= 1 ? 1 : --CurrentPage;
            await GetPage();
        }

        public async Task GetPage()
        {
            uint skip = (CurrentPage - 1) * AssetsPerPage;
            uint count = _maxAssets - skip >= AssetsPerPage ? AssetsPerPage : (uint)(_maxAssets - skip);
            await SetAssets(count, skip);
        }

        public async Task Search(string search)
        {
            _searchQuery = search;
            _maxAssets = await TotalAssets();
            CurrentPage = 1;
            CountPages();
            await GetPage();
        }

        private async Task<short> TotalAssets()
        {
            return (short)(await _coinCapService.GetAssets(2000, 0, _searchQuery)).Count;
        }

        private void CountPages()
        {
            uint pages = (uint)(_maxAssets / AssetsPerPage);
            uint remainder = (uint)(_maxAssets % AssetsPerPage);
            Pages = remainder == 0 ? pages : ++pages;
            Pages = Pages == 0 ? 1 : Pages;
        }

        public async Task SetAssets(uint count = 10, uint skip = 0)
        {
            Assets = await _coinCapService.GetAssets(count, skip, _searchQuery);
        }

        public void ValidateNumericTextBox(TextBox textBox)
        {
            if (AssetsPerPage == 0)
            {
                textBox.Text = string.Empty;
            }
            if (AssetsPerPage > _maxPerPage)
            {
                textBox.Text = _maxPerPage.ToString();
            }
        }
    }
}
