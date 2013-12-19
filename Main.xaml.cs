using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeNeedLife
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
        }

        void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            if (SystemParameters.PrimaryScreenWidth > SystemParameters.PrimaryScreenHeight)
            {
                // Runs in landscape
            }
            else
            {
                MessageBox.Show("Please change the orientation to landscape Mode by Tilting your Tablet.");
            }
        }

        private void donateOrganClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new DonateOrgan());
        }

        private void donateBloodClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new DonateBlood());
        }

        private void requestOrganClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new RequestOrgan());
        }

        private void requestBloodClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new RequestBlood());
        }

        private void timeLineClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new TimeLine());
        }

        private void mycityClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new MyCity());
        }

        private void contactClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Contact());
        }

        private void searchClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Search());
        }

        private void supportClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Support());
        }

        private void howtoClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new HowTo());
        }

        private void aboutClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new About());
        }
        
        private void filterClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Filter());
        }

        private void whyClick(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Ads());
        }

        private void donateOrganStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new DonateOrgan());
        }

        private void donateBloodStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new DonateBlood());
        }

        private void requestOrganStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new RequestOrgan());
        }

        private void requestBloodStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new RequestBlood());
        }

        private void timeLineStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new TimeLine());
        }

        private void mycityStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new MyCity());
        }

        private void contactStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Contact());
        }

        private void searchStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Search());
        }

        private void supportStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Support());
        }

        private void howtoStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new HowTo());
        }

        private void aboutStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new About());
        }

        private void filterStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Filter());
        }

        private void whyStylus(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _mainFrame.Navigate(new Ads());
        }
        
    }
}
