using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.ServiceModel.Syndication;

// Header Imports Finished

namespace MeNeedLife
{
    public partial class HowTo : Page
    {
       
        //Constructor
        public HowTo()
        {
            //Initializer
            InitializeComponent();

            //Display Setting Change Handler (Landscape to Potrait)
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


        private void backimg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Search());
        }
        private void backimg_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Search());
        }

        private void filter_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Filter());
        }

        private void filter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Filter());
        }

        private void rbName_GotFocus(object sender, RoutedEventArgs e)
        {
            keyBoardPop();
        }
        private void rbMob_GotFocus(object sender, RoutedEventArgs e)
        {
            keyBoardPop();
        }
        private void hos_GotFocus(object sender, RoutedEventArgs e)
        {
            keyBoardPop();
        }

        public void keyBoardPop()
        {
            try
            {
                Process.Start("TabTip.exe");
            }
            catch (Exception keyboardEx)
            {
                keyboardEx.ToString();
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            backimg.Opacity = 0.5;
        }
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            backimg.Opacity = 1;
        }

        private void filter_MouseLeave(object sender, MouseEventArgs e)
        {
            filter.Opacity = 1;
        }

        private void filter_MouseEnter(object sender, MouseEventArgs e)
        {
            filter.Opacity = 0.5;
        }
        private void home_MouseEnter(object sender, MouseEventArgs e)
        {
            home.Opacity = 0.5;
        }

        private void home_MouseLeave(object sender, MouseEventArgs e)
        {
            home.Opacity = 1;
        }

        private void about_MouseLeave(object sender, MouseEventArgs e)
        {
            about.Opacity = 1;
        }

        private void about_MouseEnter(object sender, MouseEventArgs e)
        {
            about.Opacity = 0.5;
        }
        private void mycity_MouseLeave(object sender, MouseEventArgs e)
        {
            mycity.Opacity = 1;
        }

        private void mycity_MouseEnter(object sender, MouseEventArgs e)
        {
            mycity.Opacity = 0.5;
        }

        private void howto_MouseEnter(object sender, MouseEventArgs e)
        {
            howto.Opacity = 0.5;
        }

        private void howto_MouseLeave(object sender, MouseEventArgs e)
        {
            howto.Opacity = 1;
        }
        private void home_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Main());
        }

        private void home_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Main());
        }

        private void about_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new About());
        }

        private void about_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new About());
        }

        private void mycity_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new TimeLine());
        }

        private void mycity_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new TimeLine());
        }

        private void howto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Contact());
        }

        private void howto_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _aboutFrame.Navigate(new Contact());
        }

       
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Visible;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void MycityBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Visible;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void FilterBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Visible;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void RBBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Visible;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void ROBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Visible;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void DOBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Visible;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void DBBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Visible;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void TimelineBtn_Click(object sender, RoutedEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Visible;
        }

        private void SearchBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Visible;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void MycityBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Visible;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void FilterBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Visible;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void RBBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Visible;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void ROBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Visible;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void DOBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Visible;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void DBBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Visible;
            TimelineText.Visibility = Visibility.Collapsed;
        }

        private void TimelineBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Image.Visibility = Visibility.Collapsed;
            SearchText.Visibility = Visibility.Collapsed;
            MyCityText.Visibility = Visibility.Collapsed;
            FilterText.Visibility = Visibility.Collapsed;
            RequestBloodText.Visibility = Visibility.Collapsed;
            RequestOrganText.Visibility = Visibility.Collapsed;
            DonateOrganText.Visibility = Visibility.Collapsed;
            DonateBloodText.Visibility = Visibility.Collapsed;
            TimelineText.Visibility = Visibility.Visible;
        }


    }
}
