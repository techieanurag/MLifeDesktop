using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
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
    public partial class MyCity : Page
    {
        //Global Vars
        private BackgroundWorker backroungWorker;
        SyndicationItem item = new SyndicationItem();
        static string loadUri = "";        
        string myCity = "Mumbai";
        double zoomLevel = 11;
        string ItemTitle = "";
        string twshareText = "";
        string fbshareText = "";       
        int selectcount = 0;

        //Constructor

        public MyCity()
        {
            
            //Initializer
            InitializeComponent();
            StartLoadingData();
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

       public void setCenter(double lat, double lon, string cityName)
        {
            Action action9 = () =>
            {
                Location loc = new Location(lat, lon);
                myMap.Center = loc;
                myMap.ZoomLevel = zoomLevel;
                Pushpin pin = new Pushpin();
                pin.Location = loc;
                myMap.Children.Add(pin);
                bdTitle.Text = cityName + " TimeLine";
                                
            };
            Dispatcher.BeginInvoke(action9); 
        }


       private void StartLoadingData()
       {
           backroungWorker = new BackgroundWorker();
           backroungWorker.DoWork += new DoWorkEventHandler(backroungWorker_DoWork);
           backroungWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backroungWorker_RunWorkerCompleted);
           backroungWorker.RunWorkerAsync();
       }

       void backroungWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
       {

       }

       void backroungWorker_DoWork(object sender, DoWorkEventArgs e)
       {
           //Thread for loading the listbox
           Action action4 = () =>
           {
               try
               {
                   WebClient wc = new WebClient();
                   wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
                   wc.DownloadStringAsync(new Uri("http://techiesin.com/dogood/feed/?s=" + loadUri));
               }
               catch (Exception internetEx)
               {
                   internetEx.ToString();
               }
           };
           Dispatcher.BeginInvoke(action4);
       }

       public void LoadNow(string LoadCity)
       {
           loadUri = LoadCity;
           loadForCity();
       }

       private void CitySelect(object sender, SelectionChangedEventArgs e)
       {
           myCity = (CityBox.SelectedItem as ComboBoxItem).Content.ToString();
           if (myCity == "Select City")
           {
               myCity = "";
           }
           else
           {
               MessageBox.Show("You selected " + myCity);
           }
           
           
           if (myCity == "Mumbai")
           {
               setCenter(19.09132, 72.862587, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Delhi")
           {
               setCenter(28.661671, 77.192459, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Pune")
           {
               setCenter(18.518679, 73.859596, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Bangalore")
           {
               setCenter(12.969766, 77.587967, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Bhopal")
           {
               setCenter(23.24387, 77.41848, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Hyderabad")
           {
               setCenter(17.380784, 78.486214, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Ahmedabad")
           {
               setCenter(23.026502, 72.579432, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Jaipur")
           {
               setCenter(26.910437, 75.79731, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Kolkata")
           {
               setCenter(22.560757, 88.373566, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Patna")
           {
               setCenter(25.609023, 85.142612, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Ranchi")
           {
               setCenter(23.345408, 85.30941, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Srinagar")
           {
               setCenter(34.085649, 74.798241, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Chandigarh")
           {
               setCenter(30.727376, 76.778584, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Amritsar")
           {
               setCenter(31.633799, 74.877262, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Lucknow")
           {
               setCenter(26.850416, 80.949898, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Kanpur")
           {
               setCenter(26.451517, 80.316868, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Raipur")
           {
               setCenter(21.252262, 81.631794, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Panaji")
           {
               setCenter(15.492062, 73.819857, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Trivendrum")
           {
               setCenter(8.483918, 76.944695, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Chennai")
           {
               setCenter(13.056737, 80.253639, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Bhubaneshwar")
           {
               setCenter(20.293113, 85.826454, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Guwahati")
           {
               setCenter(26.150199, 91.744366, myCity);
               LoadNow(myCity);
           }
           else if (myCity == "Shimla")
           {
               setCenter(31.099394, 77.175808, myCity);
               LoadNow(myCity);
           }

           else if (myCity == "Dehradun")
           {
               setCenter(30.31747, 78.033428, myCity);
               LoadNow(myCity);
           }

           
       }


        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Action action3 = () =>
            {
                try
                {
                    XmlReader reader = XmlReader.Create(new StringReader(e.Result));
                    SyndicationFeed downloadFeed = SyndicationFeed.Load(reader);
                    odonorBox.ItemsSource = downloadFeed.Items;
                    DBProgressBar.Visibility = Visibility.Collapsed;
                    LoadingText.Visibility = Visibility.Collapsed;
                    bdTitle.Visibility = Visibility.Visible;
                }
                catch (Exception loaderEx)
                {
                    loaderEx.ToString();
                    DBProgressBar.Visibility = Visibility.Collapsed;
                    LoadingText.Text = "Please check your Internet Connection";
                    bdTitle.Visibility = Visibility.Collapsed;
                }
            };
            Dispatcher.BeginInvoke(action3);
        }

        private void DonateOrganBoxTL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectcount++;
            if (selectcount > 1)
            {
                FBSBtn.IsHitTestVisible = true;
                TWSBtn.IsHitTestVisible = true;
                FBSBtn.Opacity = 1;
                TWSBtn.Opacity = 1;
                ListBox listBox = sender as ListBox;
                if (listBox != null && listBox.SelectedItem != null)
                {
                    SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;
                    if (sItem.Links.Count > 0)
                    {
                        ItemTitle = sItem.Summary.Text;
                        twshareText = " " + sItem.Title.Text + " please share: " + sItem.Id.ToString();
                        fbshareText = "https://www.facebook.com/sharer/sharer.php?s=100&p[url]=" + sItem.Id.ToString() + "&p[images][0]=http://cdn.marketplaceimages.windowsphone.com/v8/images/237c2f34-a880-42ca-b6cd-0cc5c6021040&p[title]=" + sItem.Title.Text + "&p[summary]=" + "Please share the requests from application @meNeedLife. This App is can be used to save lives of others as well as to help someone you know. Search for meNeedLife on your Phones Application store for downloading the app.";
                        //MessageBox.Show(fbshareText);                       
                    }
                }
            }
        }

        
        private void DownloadStringCompletedRefresh(object sender, DownloadStringCompletedEventArgs e)
        {
            Action action7 = () =>
            {
                try
                {
                    XmlReader reader = XmlReader.Create(new StringReader(e.Result));
                    SyndicationFeed downloadFeed = SyndicationFeed.Load(reader);
                    odonorBox.Visibility = Visibility.Visible;
                    odonorBox.ItemsSource = downloadFeed.Items;
                    DBProgressBar.Visibility = Visibility.Collapsed;
                    LoadingText.Visibility = Visibility.Collapsed;
                    bdTitle.Visibility = Visibility.Visible;
                }
                catch (Exception loaderEx)
                {
                    loaderEx.ToString();
                    DBProgressBar.Visibility = Visibility.Collapsed;
                    LoadingText.Text = "Please check your Internet Connection";
                    bdTitle.Visibility = Visibility.Collapsed;
                }
            };
            Dispatcher.BeginInvoke(action7);
        }

        public void loadForCity()
        {
            Action action8 = () =>
            {
                try
                {
                    odonorBox.Visibility = Visibility.Collapsed;
                    DBProgressBar.Visibility = Visibility.Visible;
                    LoadingText.Visibility = Visibility.Visible;
                    bdTitle.Visibility = Visibility.Collapsed;

                    Random rnd = new Random();
                    int uriRand = rnd.Next();

                    WebClient wc = new WebClient();
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompletedRefresh);
                    wc.DownloadStringAsync(new Uri("http://www.techiesin.com/dogood/feed/?s="+loadUri+"&rand="+uriRand));
                }
                catch (Exception internetEx)
                {
                    internetEx.ToString();
                }
            };
            Dispatcher.BeginInvoke(action8);
        }
        
        
        
        public void refreshButtonClickMethod()
        {
            Action action5 = () =>
            {
                try
                {
                    odonorBox.Visibility = Visibility.Collapsed;
                    DBProgressBar.Visibility = Visibility.Visible;
                    LoadingText.Visibility = Visibility.Visible;
                    bdTitle.Visibility = Visibility.Collapsed;

                    Random rnd = new Random();
                    int uriRand = rnd.Next();

                    WebClient wc = new WebClient();
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompletedRefresh);
                    wc.DownloadStringAsync(new Uri("http://www.techiesin.com/dogood/feed/?s=" + loadUri + "&rand=" + uriRand));
                }
                catch (Exception internetEx)
                {
                    internetEx.ToString();
                }
            };
            Dispatcher.BeginInvoke(action5);
        }

        //Soft Button Handlings

        private void FBS_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo(fbshareText));
        }

        private void FBS_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo(fbshareText));
        }

        private void TWS_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://twitter.com/share?text=@meneedlife" + twshareText));
        }

        private void TWS_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://twitter.com/share?text=@meneedlife" + twshareText));
        }

        private void backimg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new Search());
        }
        private void backimg_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _myCityFrame.Navigate(new Search());
        }

        private void refresh_StylusUp(object sender, StylusButtonEventArgs e)
        {
            refreshButtonClickMethod();
        }

        private void refresh_MouseUp(object sender, MouseButtonEventArgs e)
        {
            refreshButtonClickMethod();
        }

        private void rbName_GotFocus(object sender, RoutedEventArgs e)
        {
            keyBoardPop();
        }
        private void rbMob_GotFocus(object sender, RoutedEventArgs e)
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

    }
}
