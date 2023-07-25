using Windows.UI.Xaml.Controls;

namespace CoinsViewer
{
    public class NavigationManager
    {
        public void NavigateToPage(Frame frame, string pageName)
        {
            switch (pageName)
            {
                case "mainPage":
                    frame.Navigate(typeof(MainPage));
                    break;
                case "searchPage":
                    frame.Navigate(typeof(SearchPage));
                    break;
                default:
                    break;
            }
        }
    }
}
