using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GleamValet.Data;
using RestSharp;
using Newtonsoft.Json;
using GleamValet.Helper;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;
using FirebaseAdmin.Auth;

namespace GleamValet.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerUI : TabbedPage
    {

        public static double Longtitude;
        public static double Latitude;
        public static string ComboPrice;
        public static string InteriorPrice;
        public static string ExteriorPrice;
        public static string Postcode;

        private EmailPassFireAuth.IEmailPassFireAuth Signout;

        private bool Clicked;

        public CustomerUI()
        {
            InitializeComponent();
            Signout = DependencyService.Get<EmailPassFireAuth.IEmailPassFireAuth>();
        }

      
        

        private void CustomerLogout_Clicked(object sender, EventArgs e)
        {
            Signout.signout();
            Navigation.PushAsync(new CustomerLogin());
        }

        private void ComboImage_OnClicked(object sender, EventArgs e)
        {

            if (Location.Text == null && Clicked == true)
            {
                
                DisplayAlert("STOP", "Please wait for your address to appear above", "Ok");
            }
            else if (Location.Text == null && Clicked == false)
            {
                DisplayAlert("STOP", "Please click 'FIND ME' button and wait for your address to appear ", "Ok");
            }
            else
            {
                Navigation.PushAsync(new ComboSendJob());
            }

            ComboPrice = ComboPrices.Text;
        }
    

        private void ExteriorImage_OnClicked(object sender, EventArgs e)
        {
            if (Location.Text == null && Clicked == true)
            {
                DisplayAlert("STOP", "Please wait for your address to appear above", "Ok");
            }
            else if (Location.Text == null && Clicked == false)
            {
                DisplayAlert("STOP", "Please click 'FIND ME' button and wait for your address to appear ", "Ok");
            }
            else
            {
                Navigation.PushAsync(new ExteriorSendJob());
            }

            ExteriorPrice = ExteriorPrices.Text;
        }

        private void InteriorImage_OnClicked(object sender, EventArgs e)
        {
            if (Location.Text == null && Clicked == true)
            {
                DisplayAlert("STOP", "Please wait for your address to appear above", "Ok");
            }
            else if(Location.Text == null && Clicked == false)
            {
                DisplayAlert("STOP", "Please click 'FIND ME' button and wait for your address to appear ", "Ok");
            }
            else
            {
                Navigation.PushAsync(new InteriorSendJob());
            }

            InteriorPrice = InteriorPrices.Text;
        }

        private  async void GetLocation_OnClicked(object sender, EventArgs e)
        {
            Clicked = true;
           
                var location = await Geolocation.GetLocationAsync();

                 Longtitude = location.Longitude;
                 Latitude = location.Latitude;

                var placemarks = await Geocoding.GetPlacemarksAsync(Latitude, Longtitude);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    
                    string geocodeAddressed =

                        $"{placemark.SubThoroughfare}, " +
                        $"{placemark.Thoroughfare}, " +
                        $"{placemark.PostalCode}";
                    
                    Postcode = placemark.PostalCode;

                    Location.Text = geocodeAddressed;

                    // geoCodeAddress is global variable to be used by other methods.
                    
                }
        }

        private void GoToChat_Clicked(object sender, EventArgs e)
        {

            Navigation.PushModalAsync(new CustomerChatPage());

        }
    }
}