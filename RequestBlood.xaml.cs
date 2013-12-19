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

// Header Imports Finished

namespace MeNeedLife
{
    public partial class RequestBlood : Page
    {
        //Global Vars
        private BackgroundWorker backroungWorker;
        SyndicationItem item = new SyndicationItem();
        string rbBloodGroup = "";
        string odCityName = "";
        string ItemTitle = "";
        string twshareText = "";
        string fbshareText = "";
        int TxtBoxTxtlen = 0;
        string bdSendUrl = "";
        int selectcount = 0;
        int nextFlag = 0;
        bool checkflag = false;

        //Constructor

        public RequestBlood()
        {
            //Thread for loading the listbox
            
            //Initializer
            InitializeComponent();

            StartLoadingData();

            //Display Setting Change Handler (Landscape to Potrait)
            Microsoft.Win32.SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);

            //Code for TextBox Mobile Validation
            int iMinLength = 0;
            rbMob.MaxLength = 10;
            string iLengthBuf = iMinLength + "/" + rbMob.MaxLength;
            countCheck.Text = iLengthBuf;
            rbMob.TextChanged += new TextChangedEventHandler(textCountChanged);
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

        private void textCountChanged(object sender, TextChangedEventArgs e)
        {
            TxtBoxTxtlen = rbMob.Text.ToString().Length;
            countCheck.Text = TxtBoxTxtlen.ToString() + "/" + rbMob.MaxLength;  // Display the text char count as : entered/max value
        }

        private void odCity(object sender, SelectionChangedEventArgs e)
        {
            odCityName = (brCityBox.SelectedItem as ComboBoxItem).Content.ToString();
            if (odCityName == "Select Nearest City")
            {
                odCityName = "";
            }
            else
            {
                MessageBox.Show("You selected you city as " + odCityName);
            }

        }

        private void MyRequestBlood(object sender, SelectionChangedEventArgs e)
        {
            rbBloodGroup = (rBloodBox.SelectedItem as ComboBoxItem).Content.ToString();
            if (rbBloodGroup == "Select Blood Group")
            {
                rbBloodGroup = "";
            }
            else
            {
                MessageBox.Show("You selected " + rbBloodGroup);
            }
            
            if (rbBloodGroup == "A+")
            {
                rbBloodGroup = "Ap";
            }
            else if (rbBloodGroup == "B+")
            {
                rbBloodGroup = "Bp";
            }
            else if (rbBloodGroup == "AB+")
            {
                rbBloodGroup = "ABp";
            }
            else if (rbBloodGroup == "O+")
            {
                rbBloodGroup = "Op";
            }
            else if (rbBloodGroup == "A-")
            {
                rbBloodGroup = "An";
            }
            else if (rbBloodGroup == "B-")
            {
                rbBloodGroup = "Bn";
            }
            else if (rbBloodGroup == "AB-")
            {
                rbBloodGroup = "ABn";
            }
            else
            {
                rbBloodGroup = "";
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
                    wc.DownloadStringAsync(new Uri("http://techiesin.com/dogood/feed/?s=Blood"));
                }
                catch (Exception internetEx)
                {
                    internetEx.ToString();
                }
            };
            Dispatcher.BeginInvoke(action4);
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
                        twshareText = " " + sItem.Title.Text + " please share or retweet this quickly: " + sItem.Id.ToString();
                        fbshareText = "https://www.facebook.com/sharer/sharer.php?s=100&p[url]=" + sItem.Id.ToString() + "&p[images][0]=http://cdn.marketplaceimages.windowsphone.com/v8/images/237c2f34-a880-42ca-b6cd-0cc5c6021040&p[title]=" + sItem.Title.Text + "&p[summary]=" + "Please share the requests from application @meNeedLife. This App is can be used to save lives of others as well as to help someone you know. Search for meNeedLife on your Phones Application store for downloading the app.";
                        //MessageBox.Show(fbshareText);                       
                    }
                }
            }
        }

        public bool checker()
        {
            if (rbBloodGroup == "")
            {
                MessageBox.Show("Please select a Blood Group.");
                checkflag = false;
            }
            
            else if (odCityName == "")
            {
                MessageBox.Show("Please select nearest city.");
                checkflag = false;
            }
            else if (rbName.Text == "")
            {
                MessageBox.Show("Please provide your name.");
                checkflag = false;
            }

            else if (rbHos.Text == "")
            {
                MessageBox.Show("Please provide Doctor/Hospital.");
                checkflag = false;
            }
            else if (TxtBoxTxtlen < 10)
            {
                MessageBox.Show("Please check your mobile number.");
                checkflag = false;
            }

            else
            {
                checkflag = true;
            }
            return checkflag;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            bool isFlag = checker();

            if (isFlag == true)
            {
                submitBtn.Content = "Please wait.....";
                submitBtn.IsHitTestVisible = false;
                notify.Text = "Connecting with server and transferring the data. If you see this message for too long please go to Home and Come Back again.";
                    
                bdSendUrl = "name=" + rbName.Text + "&mobile=" + rbMob.Text + "&bg=" + rbBloodGroup + "&hos=" + rbHos.Text + "&city=" + odCityName;
                Debug.WriteLine(bdSendUrl);
                string bdSiteUrl = "http://techiesin.com/dogood/aRequestBlood.php?" + bdSendUrl;
                sendToServer(bdSiteUrl);
            }
        }

        void sendToServer(string postUri)
        {
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(postUri);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);
        }

        void GetRequestStreamCallback(IAsyncResult callbackResult)
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)callbackResult.AsyncState;
                // End the stream request operation
                Stream postStream = myRequest.EndGetRequestStream(callbackResult);
                // Create the post data
                string postData = bdSendUrl;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Add the post data to the web request
                postStream.Write(byteArray, 0, byteArray.Length);
                // Start the web request
                myRequest.BeginGetResponse(new AsyncCallback(request_CallBack), myRequest);
            }
            catch (Exception netEx)
            {
                notify.Text = "Please Check your Internet Connectivity. We were unable to get a working Connection.";
                submitBtn.Content = "Try Again";
                submitBtn.IsHitTestVisible = true;
                nextFlag = 10;
                netEx.ToString();
            }
        }


        void request_CallBack(IAsyncResult result)
        {
            try
            {
                var myRequest = result.AsyncState as HttpWebRequest;
                var response = (HttpWebResponse)myRequest.EndGetResponse(result);
                var baseStream = response.GetResponseStream();
                MemoryStream tempStream = new MemoryStream();
                response.GetResponseStream().CopyTo(tempStream);
                // if you want to read string response
                using (var reader = new StreamReader(baseStream))
                {
                    //Anything you want here
                }
            }
            catch (Exception newex)
            {
                newex.ToString();
                nextFlag = 10;
            }
            checkifSuccess();
        }

        public void checkifSuccess()
        {
            if (nextFlag > 1)
            {

                Action action = () =>
                {
                    notify.Text = "Problem in Request Service! Please Inform meNeedLife Team at meneedlifeapp@live.com";
                    submitBtn.Content = "Try Again";
                    submitBtn.IsHitTestVisible = true;
                };
                Dispatcher.BeginInvoke(action);

            }

            else
            {
                Action action1 = () =>
                {
                    notify.Text = "Your request successfully logged. We will shortly retweet and post it on Facebook.";
                    submitBtn.Content = "Request Logged!";
                    submitBtn.IsHitTestVisible = false;
                };

                Dispatcher.BeginInvoke(action1);
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

        public void refreshButtonClickMethod()
        {
            Action action5 = () =>
            {
                try
                {
                    DBProgressBar.Visibility = Visibility.Visible;
                    LoadingText.Visibility = Visibility.Visible;
                    bdTitle.Visibility = Visibility.Collapsed;

                    Random rnd = new Random();
                    int uriRand = rnd.Next();

                    WebClient wc = new WebClient();
                    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompletedRefresh);
                    wc.DownloadStringAsync(new Uri("http://techiesin.com/dogood/feed/?s=Blood&random=" + uriRand));
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
            _requestBloodFrame.Navigate(new Search());
        }
        private void backimg_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new Search());
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
            _requestBloodFrame.Navigate(new Main());
        }

        private void home_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new Main());
        }

        private void about_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new About());
        }

        private void about_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new About());
        }

        private void mycity_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new TimeLine());
        }

        private void mycity_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new TimeLine());
        }

        private void howto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new HowTo());
        }

        private void howto_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _requestBloodFrame.Navigate(new HowTo());
        }

        private void submitBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            bool isFlag = checker();

            if (isFlag == true)
            {
                submitBtn.Content = "Please wait.....";
                bdSendUrl = "name=" + rbName.Text + "&mobile=" + rbMob.Text + "&bg=" + rbBloodGroup + "&hos=" + rbHos.Text + "&city=" + odCityName;
                Debug.WriteLine(bdSendUrl);
                string bdSiteUrl = "http://techiesin.com/dogood/aRequestBlood.php?" + bdSendUrl;
                sendToServer(bdSiteUrl);
            }
        }

    }
}
