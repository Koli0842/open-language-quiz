using OpenQuiz.View;
using Windows.UI.Xaml.Controls;

namespace OpenQuiz
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                MainFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                switch (args.InvokedItem)
                {
                    case "Dictionary":
                        MainFrame.Navigate(typeof(DictionarySelectionPage));
                        break;
                    case "Statistics":
                        MainFrame.Navigate(typeof(StatisticsPage));
                        break;
                }
            }
        }
    }
}
