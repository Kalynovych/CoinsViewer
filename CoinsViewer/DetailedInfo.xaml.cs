using System;
using System.Collections.Generic;
using CoinsViewer.API.CoinCap;
using CoinsViewer.API.CoinCap.Model;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace CoinsViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailedInfo : Page
    {
        private Asset _asset;
        private CoinCapApiService _coinCapApiService;
        private List<ExchangeMarket> _exchangeMarket;

        public DetailedInfo()
        {
            InitializeComponent();
            _coinCapApiService = new CoinCapApiService();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _asset = (Asset)e.Parameter;
            _exchangeMarket = await _coinCapApiService.GetExchange(2);
            Bindings.Update();
        }

        private async void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void ExchangeMarket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            listView.SelectedIndex = -1;
        }
    }
}
