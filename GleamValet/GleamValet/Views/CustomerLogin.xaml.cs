using GleamValet.Helper;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using Xamarin.Forms;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Essentials;
using System.Collections.Generic;
using System.Net.Http;

namespace GleamValet.Views
{

    public partial class CustomerLogin : ContentPage
    {

        public static string LoggedCustomer;

        //The code below is to get data from the realtime database
        FirebaseHelper firebaseHelpers = new FirebaseHelper();
        private EmailPassFireAuth.IEmailPassFireAuth SignIn;



        public CustomerLogin()
        {
            InitializeComponent();
            SignIn = DependencyService.Get<EmailPassFireAuth.IEmailPassFireAuth>();
        }




        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            // take the user to the customer registration page
            Navigation.PushAsync(new NavigationPage(new CustomerRegisteration()));

        }





        private async void LoginButton_Clicked(object sender, EventArgs e)
        {

            if (EntryEmail.Text == "" || EntryEmail.Text == null)
            {
                await DisplayAlert("ERROR", "Provide Email", "Ok");
            }
            if (EntryPassword.Text == "" || EntryPassword.Text == null)
            {
                await DisplayAlert("ERROR", "Provide Password", "Ok");
            }


            string Token = await SignIn.LoginWithEmailPassword(EntryEmail.Text, EntryPassword.Text);
            if (Token != null)
            {

                string Id = SignIn.getCurrentUser();

                var customers = await firebaseHelpers.GetCustomer(EntryEmail.Text);


                if (customers != null)
                {

                    if (customers.IsCustomer == true)
                    {
                        await Navigation.PushAsync(new CustomerUI());
                    }

                    else
                    {
                        await DisplayAlert("ERROR", "Incorrect Password", "Ok");
                    }
                }

                else
                {
                    await DisplayAlert("ALERT", "Email does not exist or you not a customer", "Ok");
                }


                LoggedCustomer = EntryEmail.Text;

            }
        }
    }
}