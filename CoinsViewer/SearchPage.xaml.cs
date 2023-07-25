using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CoinsViewer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        private PagingController _pagingController;
        private NavigationManager _navigationManager;
        private uint _assetsPerPage;

        public SearchPage()
        {
            InitializeComponent();
            _pagingController = new PagingController();
            _navigationManager = new NavigationManager();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await _pagingController.SetAssets();
            Bindings.Update();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            await _pagingController.SetAssetsPerPage();
            Bindings.Update();
        }

        private async void FirstPage_Click(object sender, RoutedEventArgs e)
        {
            await _pagingController.FirstPage();
            Bindings.Update();
        }

        private async void LastPage_Click(object sender, RoutedEventArgs e)
        {
            await _pagingController.LastPage();
            Bindings.Update();
        }

        private async void NextPage_Click(object sender, RoutedEventArgs e)
        {
            await _pagingController.NextPage();
            Bindings.Update();
        }

        private async void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            await _pagingController.PreviousPage();
            Bindings.Update();
        }

        private void AssetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            _navigationManager.NavigateToPage(Frame, "detailedInfo", listView.SelectedItem);
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

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            await _pagingController.Search(searchTextBox.Text);
            Bindings.Update();
        }

        private void AssetsPerPage_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            if (args.NewText != string.Empty)
            {
                args.Cancel = !uint.TryParse(args.NewText, out _assetsPerPage);
            }

            _pagingController.AssetsPerPage = _assetsPerPage;
        }

        private void AssetsPerPage_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox assetsPerPageTextBox = (TextBox)sender;
            assetsPerPageTextBox.Text = _pagingController.AssetsPerPage.ToString();
            _pagingController.ValidateNumericTextBox(assetsPerPageTextBox);
        }
    }
}
