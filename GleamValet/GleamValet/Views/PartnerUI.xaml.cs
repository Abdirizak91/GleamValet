using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Diagnostics;
using Prism.Navigation;
using Shiny.Locations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using GleamValet.Helper;
using Xamarin.Essentials;
using NavigationMode = Xamarin.Essentials.NavigationMode;

namespace GleamValet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartnerUI : ContentPage
    {
        SignalRService signalR;

        FirebaseHelper firebaseHelp = new FirebaseHelper();
        private bool click = true;
        public PartnerUI()
        {
            InitializeComponent();
            signalR = new SignalRService();
            signalR.Connected += SignalR_ConnectionChanged;
            signalR.ConnectionFailed += SignalR_ConnectionChanged;
            signalR.NewMessageReceived += SignalR_NewMessageReceived;
            

        }

      
        private async void CheckJob_OnClicked(object sender, EventArgs e)
        {
            // seeks to get a job from Firebase Realtime Database
            var Jobs = await firebaseHelp.GetJobs();

            // if there is no jobs found
            if (Jobs == null)
            {
                await DisplayAlert("ALERT", "No Job Found", "Ok");
            }
            
            //if a job is found
            if (Jobs != null)
            {
                bool answer = await DisplayAlert("Job Found !", "Would you like to navigate to job ?", "Accept", "Reject");
                
                // if the user has selected Accept
                if (answer == true)
                {
                    // using Xamarin.Essentials 'Location'  here we set a new location.
                    var location = new Location(Jobs.Latitude, Jobs.Longitude);

                    // using Xamarin.Essentials 'MapLaunchOptions' here we set the navigation modes
                    var options = new MapLaunchOptions { NavigationMode = NavigationMode.None };

                    // using Xamarin.Essentials we launch the maps with the location and options defined above.
                    await Map.OpenAsync(location, options);

                    //We remove the job that was posted by the user. 
                    await firebaseHelp.DeleteJob(Jobs.Postcode);

                    //var allLocations = await firebaseHelp.GetJobs();

               
                }

                // if the user selects reject we display an alert for the user notifying them they have rejected
                if(answer == false)
                {
                    await DisplayAlert("", "You have rejected the job", "Ok");
                }
                
            }
        }





        /// new listing for SignalR is below 
        /// 

            // Add Message
        void AddMessage(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Label label = new Label
                {
                    Text = message,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Start
                };

                messageList.Children.Add(label);
            });
        }


        //receive a message
        void SignalR_NewMessageReceived(object sender, Model.Message message)
        {
            string msg = $"{message.Name} ({message.TimeReceived}) - {message.Text}";
            AddMessage(msg);
        }


        //The *Re-try* connection to signalR
        void SignalR_ConnectionChanged(object sender, bool success, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                connectButton.Text = "Connect";
                connectButton.IsEnabled = !success;
                sendButton.IsEnabled = success;
                AddMessage($"Server connection changed: {message}");
            });
        }

        // connect button, connect to SignalR
        async void ConnectButton_ClickedAsync(object sender, EventArgs e)
        {
            connectButton.Text = "Connecting...";
            connectButton.IsEnabled = false;
            await signalR.ConnectAsync();
        }

        //Send message
        async void SendButton_ClickedAsync(object sender, EventArgs e)
        {
            await signalR.SendMessageAsync(Constants.Username, messageEntry.Text);
            messageEntry.Text = "";
        }


    }
}