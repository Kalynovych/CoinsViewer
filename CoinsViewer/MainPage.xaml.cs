using System.Collections.Generic;
using System.Threading.Tasks;
using CoinsViewer.API.CoinCap;
using CoinsViewer.API.CoinCap.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CoinsViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Asset> _assets;
        private uint _currentPage;
        private uint _maxPage;
        private uint _assetsPerPage;
        private CoinCapApiService _coinCapService;
        private const short _maxAssets = 2000;

        public MainPage()
        {
            InitializeComponent();
            _coinCapService = new CoinCapApiService();
            _assetsPerPage = 10;
            _currentPage = 1;
            _maxPage = 200;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _assets = await _coinCapService.GetAssets();
            Bindings.Update();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            uint pages = (uint)(_maxAssets / _assetsPerPage);
            uint remainder = (uint)(_maxAssets % _assetsPerPage);
            _maxPage = remainder == 0 ? pages : ++pages;
            _assets = await _coinCapService.GetAssets(_assetsPerPage);
            Bindings.Update();
        }

        private async void FirstPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            await GetPage();
        }

        private async void LastPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _maxPage;
            await GetPage();
        }

        private async void NextPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _currentPage >= _maxPage ? _maxPage : ++_currentPage;
            await GetPage();
        }

        private async void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _currentPage <= 1 ? 1 : --_currentPage;
            await GetPage();
        }

        private async Task GetPage()
        {
            uint skip = (_currentPage - 1) * _assetsPerPage;
            uint count = _maxAssets - skip >= _assetsPerPage ? _assetsPerPage : (uint)(_maxAssets - skip);
            _assets = await _coinCapService.GetAssets(count, skip);
            Bindings.Update();
        }

        private void AssetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TODO: Asset selected
        }

        private void AssetsPerPage_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (args.NewText != string.Empty)
            {
                args.Cancel = !uint.TryParse(args.NewText, out _assetsPerPage);
            }
        }

        private void AssetsPerPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox assetsPerPageTextBox = (TextBox)sender;
            assetsPerPageTextBox.Text = _assetsPerPage.ToString();
            ValidateNumericTextBox(assetsPerPageTextBox);
        }

        private void ValidateNumericTextBox(TextBox textBox)
        {
            if (_assetsPerPage == 0)
            {
                textBox.Text = string.Empty;
            }
            if (_assetsPerPage > 200)
            {
                textBox.Text = "200";
            }
        }
    }
}
