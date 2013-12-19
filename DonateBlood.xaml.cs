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
    public partial class DonateBlood : Page
    {
        //Global Vars

        SyndicationItem item = new SyndicationItem();
        string oBloodGroup = "";
        string odCityName = "";
        string odDisease = "";
        bool checkflag = false;
        string odPrivacy = "No";        
        int TxtBoxTxtlen = 0;
        string bdSendUrl = "";
        int nextFlag = 0;

        //Constructor

        public DonateBlood()
        {
            
            //Initializer
            InitializeComponent();

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
        //Encoding because + and other symbols are not validated as post through HTTP
        private void OrganDonateBlood(object sender, SelectionChangedEventArgs e)
        {
            oBloodGroup = (rBloodBox.SelectedItem as ComboBoxItem).Content.ToString();

            if (oBloodGroup == "Select Blood Group")
            {
                oBloodGroup = "";
            }
            else
            {
                MessageBox.Show("You selected " + oBloodGroup);
            }

            if (oBloodGroup == "A+")
            {
                oBloodGroup = "Ap";
            }
            else if (oBloodGroup == "B+")
            {
                oBloodGroup = "Bp";
            }
            else if (oBloodGroup == "AB+")
            {
                oBloodGroup = "ABp";
            }
            else if (oBloodGroup == "O+")
            {
                oBloodGroup = "Op";
            }
            else if (oBloodGroup == "A-")
            {
                oBloodGroup = "An";
            }
            else if (oBloodGroup == "B-")
            {
                oBloodGroup = "Bn";
            }
            else if (oBloodGroup == "AB-")
            {
                oBloodGroup = "ABn";
            }
            else
            {
                oBloodGroup = "";
            }
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

        public bool checker()
        {

            if (oBloodGroup=="")
            {

                MessageBox.Show("Please select your Blood Group");
                checkflag = false;
            }
            else if (odCityName == "")
            {
                MessageBox.Show("Please select nearest city.");
                checkflag = false;
            }
            else if (rbName.Text == "")
            {
                MessageBox.Show("Please enter a name in the field.");
                checkflag = false;
            }
            else if (odDisease == "")
            {
                MessageBox.Show("Please select yes or no in Disease History");
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
            if (btnY.IsChecked == true)
            {
                odDisease = "Yes";
                

            }
            if (btnN.IsChecked == true)
            {
                odDisease = "No";
                
            }
            
            if (privacy.IsChecked == true)
            {
                odPrivacy = "Yes";
            }

            bool isFlag = checker();

            if (isFlag == true)
            {
                submitBtn.Content = "Please wait.....";
                submitBtn.IsHitTestVisible = false;
                notify.Text = "Connecting with server and transferring the data. If you see this message for too long please go to Home and Come Back again.";   
                bdSendUrl = "name=" + rbName.Text + "&mobile=" + rbMob.Text + "&bg=" + oBloodGroup + "&disease=" + odDisease + "&city=" + odCityName + "&privacy=" + odPrivacy;
                string bdSiteUrl = "http://techiesin.com/dogood/donateBlood.php?" + bdSendUrl;
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
                notify.Text = "Please check your internet connectivity. We were unable to get a working Connection.";
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
                    notify.Text = "Problem in request service! Please inform meNeedLife team at meneedlifeapp@live.com";
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

        
        //Soft Button Handlings

       
        private void backimg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new Search());
        }
        private void backimg_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new Search());
        }

        private void filter_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new Filter());
        }

        private void filter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new Filter());
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
            _donateBloodFrame.Navigate(new Main());
        }

        private void home_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new Main());
        }

        private void about_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new About());
        }

        private void about_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new About());
        }

        private void mycity_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new TimeLine());
        }

        private void mycity_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new TimeLine());
        }

        private void howto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new HowTo());
        }

        private void howto_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _donateBloodFrame.Navigate(new HowTo());
        }

        private void submitBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            if (btnY.IsChecked == true)
            {
                odDisease = "Yes";


            }
            if (btnN.IsChecked == true)
            {
                odDisease = "No";

            }

            if (privacy.IsChecked == true)
            {
                odPrivacy = "Yes";
            }

            bool isFlag = checker();

            if (isFlag == true)
            {
                submitBtn.Content = "Please wait.....";
                submitBtn.IsHitTestVisible = false;
                notify.Text = "Connecting with server and transferring the data. If you see this message for too long please go to Home and Come Back again.";
                bdSendUrl = "name=" + rbName.Text + "&mobile=" + rbMob.Text + "&bg=" + oBloodGroup + "&disease=" + odDisease + "&city=" + odCityName + "&privacy=" + odPrivacy;
                string bdSiteUrl = "http://techiesin.com/dogood/donateBlood.php?" + bdSendUrl;
                sendToServer(bdSiteUrl);
            }
        }

    }
}
