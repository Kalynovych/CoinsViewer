using Windows.UI.Xaml.Controls;

namespace CoinsViewer
{
    public class NavigationManager
    {
        public void NavigateToPage(Frame frame, string pageName, object parameter = null)
        {
            switch (pageName)
            {
                case "mainPage":
                    frame.Navigate(typeof(MainPage), parameter);
                    break;
                case "searchPage":
                    frame.Navigate(typeof(SearchPage), parameter);
                    break;
                case "detailedInfo":
                    frame.Navigate(typeof(DetailedInfo), parameter);
                    break;
                case "converterPage":
                    frame.Navigate(typeof(ConverterPage), parameter);
                    break;
                default:
                    break;
            }
        }
    }
}
