﻿using System;
using System.IO;
using System.Xml;
using System.Net;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Threading;
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
    public partial class TimeLine : Page
    {
        //Global Vars
        private BackgroundWorker backroungWorker;
        SyndicationItem item = new SyndicationItem();
        string ItemTitle = "";
        string twshareText = "";
        string fbshareText = "";
        int selectcount = 0;

        //Constructor

        public TimeLine()
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
                    
                }
                catch (Exception loaderEx)
                {
                    loaderEx.ToString();
                    DBProgressBar.Visibility = Visibility.Collapsed;
                    LoadingText.Text = "Please check your Internet Connection";
                   
                }
            };
            Dispatcher.BeginInvoke(action3);
        }

        private void DonateOrganBoxTL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectcount++;
            if (selectcount > 0)
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
                    wc.DownloadStringAsync(new Uri("http://techiesin.com/dogood/feed/?s="));
                }
                catch (Exception internetEx)
                {
                    internetEx.ToString();
                }
            };
            Dispatcher.BeginInvoke(action4);
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
                    
                }
                catch (Exception loaderEx)
                {
                    loaderEx.ToString();
                    DBProgressBar.Visibility = Visibility.Collapsed;
                    LoadingText.Text = "Please check your Internet Connection";
                    
                }
            };
            Dispatcher.BeginInvoke(action7);
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
                   
                    Random rnd = new Random();
                    int uriRand = rnd.Next();

                    WebClient wc = new WebClient();
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompletedRefresh);
                    wc.DownloadStringAsync(new Uri("http://www.techiesin.com/dogood/feed/?random=" + uriRand));
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
            _timelineFrame.Navigate(new Search());
            
        }
        private void backimg_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new Search());
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
            _timelineFrame.Navigate(new Main());
        }

        private void home_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new Main());
        }

        private void about_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new About());
        }

        private void about_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new About());
        }

        private void mycity_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new MyCity());
        }

        private void mycity_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new MyCity());
        }

        private void howto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new HowTo());
        }

        private void howto_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _timelineFrame.Navigate(new HowTo());
        }

    }
}
