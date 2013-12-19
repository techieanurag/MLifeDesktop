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
    public partial class Contact : Page
    {
        //Global Vars

        SyndicationItem item = new SyndicationItem();
        string odCityName = "";
        int TxtBoxTxtlen = 0;
        string bdSendUrl = "";
        int nextFlag = 0;

        //Constructor

        public Contact()
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
        
        private void odCity(object sender, SelectionChangedEventArgs e)
        {
            odCityName = (brCityBox.SelectedItem as ComboBoxItem).Content.ToString();
            if (odCityName == "Select City")
            {
                odCityName = "";
            }
            else
            {
                MessageBox.Show("You selected you city as " + odCityName);
            }

        }

        private void rbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            int EmailgBoxTxtlen = rbEmail.Text.ToString().Length;
            

            if (EmailgBoxTxtlen > 100)
            {
               MessageBox.Show("Address is too long");
            }
        }

        
        private void rbMsg_TextChanged(object sender, TextChangedEventArgs e)
        {
            int MesgBoxTxtlen = rbMsg.Text.ToString().Length;
            

            if (MesgBoxTxtlen > 1000)
            {
                MessageBox.Show("Message is more than 1000 Characters, its too long to be message!!");
            }
        }
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int MesgBoxTxtlen = rbMsg.Text.ToString().Length;
            int EmailBoxTxtlen = rbEmail.Text.ToString().Length;
            string emailaddress = rbEmail.Text;
            
                if (!emailaddress.Contains("@") && !emailaddress.Contains("."))
                {
                    MessageBox.Show("Your email address seems to be incorrect!!");
                 }
                else if (MesgBoxTxtlen < 20)
                {
                    MessageBox.Show("Message is less than 20 Characters, its too short to be message!!");
                }

                else if (odCityName=="")
                {
                    MessageBox.Show("Please select your City First");
                }

                else if (rbName.Text == "")
                {
                    MessageBox.Show("Please enter your Name");
                }
                else if (TxtBoxTxtlen < 10)
                {
                    MessageBox.Show("Please check your Mobile Number");
                }

                else if (EmailBoxTxtlen < 6)
                {
                    MessageBox.Show("Really is that your email address!!! Its too short, try another");
                }

                else
                {

                    Action action = () =>
                    {
                        submitBtn.Content = "Please wait.....";
                        submitBtn.IsHitTestVisible = false;
                        notify.Text = "Connecting with server and transferring the data. If you see this message for too long please go to Home and Come Back again.";
                        bdSendUrl = "name=" + rbName.Text + "&mobile=" + rbMob.Text + "&email=" + emailaddress + "&city=" + odCityName + "&msg=" + rbMsg.Text;
                        string bdSiteUrl = "http://techiesin.com/dogood/aContact.php?" + bdSendUrl;
                        sendToServer(bdSiteUrl);
                    };
                    Dispatcher.BeginInvoke(action);
                    
                    
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
                    notify.Text = "We got your message, please await a reply via email or expect a call in 48 working hours.";
                    submitBtn.Content = "Message Sent!";
                    submitBtn.IsHitTestVisible = false;
                };

                Dispatcher.BeginInvoke(action1);
            }
        }

        
        private void backimg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new Search());
        }
        private void backimg_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new Search());
        }

        private void filter_StylusUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new Filter());
        }

        private void filter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new Filter());
        }

        private void rbName_GotFocus(object sender, RoutedEventArgs e)
        {
            keyBoardPop();
        }
        private void rbMob_GotFocus(object sender, RoutedEventArgs e)
        {
            keyBoardPop();
        }
        private void rbEmail_GotFocus(object sender, RoutedEventArgs e)
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
            _contactFrame.Navigate(new Main());
        }

        private void home_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new Main());
        }

        private void about_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new About());
        }

        private void about_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new About());
        }

        private void mycity_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new TimeLine());
        }

        private void mycity_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new TimeLine());
        }

        private void howto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new HowTo());
        }

        private void howto_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            _contactFrame.Navigate(new HowTo());
        }

        private void submitBtn_StylusButtonUp(object sender, StylusButtonEventArgs e)
        {
            int MesgBoxTxtlen = rbMsg.Text.ToString().Length;
            int EmailBoxTxtlen = rbEmail.Text.ToString().Length;
            string emailaddress = rbEmail.Text;

            if (!emailaddress.Contains("@") && !emailaddress.Contains("."))
            {
                MessageBox.Show("Your email address seems to be incorrect!!");
            }
            else if (MesgBoxTxtlen < 20)
            {
                MessageBox.Show("Message is less than 20 Characters, its too short to be message!!");
            }

            else if (odCityName == "")
            {
                MessageBox.Show("Please select your City First");
            }

            else if (rbName.Text == "")
            {
                MessageBox.Show("Please enter your Name");
            }
            else if (TxtBoxTxtlen < 10)
            {
                MessageBox.Show("Please check your Mobile Number");
            }

            else if (EmailBoxTxtlen < 6)
            {
                MessageBox.Show("Really is that your email address!!! Its too short, try another");
            }

            else
            {
                submitBtn.Content = "Please wait.....";
                submitBtn.IsHitTestVisible = false;
                notify.Text = "Connecting with server and transferring the data. If you see this message for too long please go to Home and Come Back again.";
                bdSendUrl = "name=" + rbName.Text + "&mobile=" + rbMob.Text + "&email=" + emailaddress + "&city=" + odCityName + "&msg=" + rbMsg.Text;
                string bdSiteUrl = "http://techiesin.com/dogood/aContact.php?" + bdSendUrl;
                sendToServer(bdSiteUrl);
            }      
        }       

    }
}
