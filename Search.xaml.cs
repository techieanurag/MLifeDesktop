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
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Maps.MapControl.WPF.Design;
// Header Imports Finished

namespace MeNeedLife
{
    public partial class Search : Page
    {
        //Global Vars
        SyndicationItem item = new SyndicationItem();
        static string loadUri = "http://techiesin.com/dogood/maps/mumbai.php?lat=18.9750&lon=72.8258&city=Mumbai&img=4";
        string myCity = "Mumbai";
       //Constructor

        public Search()
        {
            //Initializer
            InitializeComponent();

            Action action = () =>
            {
                browser.Source = new Uri(loadUri);
            };
            Dispatcher.BeginInvoke(action);

            browser.Source = new Uri(loadUri);
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
  
        public void setCenter(string latitude, string longitude, string LoadCity)
        {
            
            Action action = () =>
            {
                if (LoadCity == "My City")
                {
                    
                }
                else
                {
                    Latitude.Text="";
                    Longitude.Text="";
                }
                
                Random random = new Random();
                string imgNo = random.Next(1, 5).ToString();
                DBProgressBar.Visibility = Visibility.Visible;
                browser.Navigate("http://techiesin.com/dogood/maps/index.php?lat=" + latitude + "&lon=" + longitude + "&city=" + LoadCity + "&img=" + imgNo);
            };
            Dispatcher.BeginInvoke(action);
                        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string getLat = Latitude.Text;
            string getLon = Longitude.Text;
            setCenter(getLat, getLon, "My City");

        }

        private void CitySelect(object sender, SelectionChangedEventArgs e)
        {
            myCity = (CityBox.SelectedItem as ComboBoxItem).Content.ToString();
                        
            if (myCity == "Mumbai")
            {
                setCenter("19.09132", "72.862587", myCity);
                MessageBox.Show("Results will be displayed for "+myCity);

            }
            else if (myCity == "Delhi")
            {
                setCenter("28.661671", "77.192459", myCity);

            }
            else if (myCity == "Pune")
            {
                setCenter("18.518679", "73.859596", myCity);

            }
            else if (myCity == "Bangalore")
            {
                setCenter("12.969766", "77.587967", myCity);

            }
            else if (myCity == "Bhopal")
            {
                setCenter("23.24387", "77.41848", myCity);

            }
            else if (myCity == "Hyderabad")
            {
                setCenter("17.380784", "78.486214", myCity);

            }
            else if (myCity == "Ahmedabad")
            {
                setCenter("23.026502", "72.579432", myCity);

            }
            else if (myCity == "Jaipur")
            {
                setCenter("26.910437", "75.79731", myCity);

            }
            else if (myCity == "Kolkata")
            {
                setCenter("22.560757", "88.373566", myCity);
            }
            else if (myCity == "Patna")
            {
                setCenter("25.609023", "85.142612", myCity);
            }
            else if (myCity == "Ranchi")
            {
                setCenter("23.345408", "85.30941", myCity);
            }
            else if (myCity == "Srinagar")
            {
                setCenter("34.085649", "74.798241", myCity);
            }
            else if (myCity == "Chandigarh")
            {
                setCenter("30.727376", "76.778584", myCity);
            }
            else if (myCity == "Amritsar")
            {
                setCenter("31.633799", "74.877262", myCity);
            }
            else if (myCity == "Lucknow")
            {
                setCenter("26.850416", "80.949898", myCity);
            }
            else if (myCity == "Kanpur")
            {
                setCenter("26.451517", "80.316868", myCity);
            }
            else if (myCity == "Raipur")
            {
                setCenter("21.252262", "81.631794", myCity);
            }
            else if (myCity == "Panaji")
            {
                setCenter("15.492062", "73.819857", myCity);
            }
            else if (myCity == "Trivendrum")
            {
                setCenter("8.483918", "76.944695", myCity);
            }
            else if (myCity == "Chennai")
            {
                setCenter("13.056737", "80.253639", myCity);
            }
            else if (myCity == "Bhubaneshwar")
            {
                setCenter("20.293113", " 85.826454", myCity);
            }
            else if (myCity == "Guwahati")
            {
                setCenter("26.150199", "91.744366", myCity);
            }
            else if (myCity == "Shimla")
            {
                setCenter("31.099394", "77.175808", myCity);
            }

            else if (myCity == "Dehradun")
            {
                setCenter("30.31747", "78.033428", myCity);
            }
            
            if (myCity == "Select City")
            {
                myCity = "";
            }
            else{
                MessageBox.Show("Results will be displayed for " + myCity);
            }
        }


        private void backimg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            keyBoardPop();
        }
        private void backimg_StylusUp(object sender, StylusButtonEventArgs e)
        {
            keyBoardPop();
        }

        private void refresh_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new Filter());
        }

        private void refresh_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new Filter());
        }

        public void keyBoardPop()
        {


            Action action1 = () =>
            {
                try
                {
                    Process.Start("TabTip.exe");
                }
                catch (Exception keyboardEx)
                {
                    keyboardEx.ToString();
                }
            };
            Dispatcher.BeginInvoke(action1);
            
            
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            backimg.Opacity = 0.5;
        }
        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            backimg.Opacity = 1;
        }

        private void refresh_MouseLeave(object sender, MouseEventArgs e)
        {
            refresh.Opacity = 1;
        }

        private void refresh_MouseEnter(object sender, MouseEventArgs e)
        {
            refresh.Opacity = 0.5;
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
            _myCityFrame.Navigate(new Main());
        }

        private void home_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new Main());
        }

        private void about_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new About());
        }

        private void about_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new About());
        }

        private void mycity_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new TimeLine());
        }

        private void mycity_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new TimeLine());
        }

        private void howto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new HowTo());
        }

        private void howto_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new HowTo());
        }

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            DBProgressBar.Visibility = Visibility.Collapsed;
        }

        private void Button_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            string getLat = Latitude.Text;
            string getLon = Longitude.Text;
            setCenter(getLat, getLon, "My City");
        }

        

    }
}
