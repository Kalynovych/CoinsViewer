using System.Collections.Generic;
using System.Linq;
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
    public sealed partial class ConverterPage : Page
    {
        private NavigationManager _navigationManager;
        private List<Rate> _rates;
        private CoinCapApiService _coinCapApiService;
        private double _amountOfCurrency = 1;

        public ConverterPage()
        {
            InitializeComponent();
            _navigationManager = new NavigationManager();
            _coinCapApiService = new CoinCapApiService();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _rates = await _coinCapApiService.GetRates();
            Bindings.Update();
        }

        private void Page_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            ListViewItem listItem = (ListViewItem)listView.SelectedItem;
            if (Frame != null)
            {
                _navigationManager.NavigateToPage(Frame, listItem.Name);
            }
        }

        private void ConvertionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
            switch (item.Content)
            {
                case "fiat to fiat":
                    convertFrom.ItemsSource = _rates.Where(r => r.Type == "fiat").ToList();
                    convertTo.ItemsSource = _rates.Where(r => r.Type == "fiat").ToList();
                    break;
                case "fiat to crypto":
                    convertFrom.ItemsSource = _rates.Where(r => r.Type == "fiat").ToList();
                    convertTo.ItemsSource = _rates.Where(r => r.Type == "crypto").ToList();
                    break;
                case "crypto to fiat":
                    convertFrom.ItemsSource = _rates.Where(r => r.Type == "crypto").ToList();
                    convertTo.ItemsSource = _rates.Where(r => r.Type == "fiat").ToList();
                    break;
                case "crypto to crypto":
                    convertFrom.ItemsSource = _rates.Where(r => r.Type == "crypto").ToList();
                    convertTo.ItemsSource = _rates.Where(r => r.Type == "crypto").ToList();
                    break;
                default:
                    break;
            }       
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            Rate fromRate = (Rate)convertFrom.SelectedItem;
            Rate toRate= (Rate)convertTo.SelectedItem;
            if (fromRate == null || toRate == null)
            {
                return;
            }

            double result = (_amountOfCurrency * fromRate.RateUsd) / toRate.RateUsd;
            convertionResult.Text = $"{_amountOfCurrency} {fromRate.Symbol} = {result.ToString("N5")} {toRate.Symbol}";
        }

        private void Swap_Click(object sender, RoutedEventArgs e)
        {
            Rate fromTemp = (Rate)convertFrom.SelectedItem;
            Rate toTemp = (Rate)convertTo.SelectedItem;
            if (fromTemp == null || toTemp == null)
            {
                return;
            }

            if (fromTemp.Type != toTemp.Type)
            {
                if (fromTemp.Type == "fiat")
                {
                    convertionType.SelectedItem = convertionType.Items.Where(c => ((ComboBoxItem)c).Content.ToString() == "crypto to fiat").First();
                }
                else
                {
                    convertionType.SelectedItem = convertionType.Items.Where(c => ((ComboBoxItem)c).Content.ToString() == "fiat to crypto").First();
                }
            }

            convertFrom.SelectedItem = toTemp;
            convertTo.SelectedItem = fromTemp;
        }

        private void Amount_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (args.NewText != string.Empty)
            {
                args.Cancel = !double.TryParse(args.NewText, out _amountOfCurrency);
            }
            
            args.Cancel = _amountOfCurrency <= 0;
        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox amountTextBox = (TextBox)sender;
            amountTextBox.Text = _amountOfCurrency.ToString();
        }
    }
}
