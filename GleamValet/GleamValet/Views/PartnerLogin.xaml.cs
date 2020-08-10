using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.IO;
using GleamValet.Data;
using GleamValet.Views;
using GleamValet.Helper;
using Firebase.Database;
using Xamarin.Essentials;
using FirebaseAdmin.Auth;
using GleamValet.Helper;

namespace GleamValet
{
    public partial class PartnerLogin : ContentPage
    {
        public static string LoggedPartner;
        private FirebaseAuth firebaeadmin;

        //The code below is to get data from the realtime database
        FirebaseHelper firebaseHelpers = new FirebaseHelper();
        private EmailPassFireAuth.IEmailPassFireAuth Login;

        public PartnerLogin()
        {
            InitializeComponent();
            Login = DependencyService.Get<EmailPassFireAuth.IEmailPassFireAuth>();

        }


        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NavigationPage(new PartnerRegisteration()));
        }


        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (PartnerEmail.Text == "" || PartnerEmail.Text == null)
            {
                await DisplayAlert("ERROR", "Provide Email", "Ok");
            }
            if (EntryPassword.Text == "" || EntryPassword.Text == null)
            {
                await DisplayAlert("ERROR", "Provide Password", "Ok");
            }

            var Token = Login.LoginWithEmailPassword(PartnerEmail.Text, EntryPassword.Text);

           string CurrentUserID = Login.getCurrentUser();

           if (Token != null)
           {

               var employees = await firebaseHelpers.GetPartner(PartnerEmail.Text);

               if (employees != null)
               {
                   if (employees.IsPartner == true)
                   {
                       await Navigation.PushAsync(new NavigationPage(new PartnerUI()));
                   }

                   else
                   {
                       await DisplayAlert("ERROR", "Incorrect Password", "Ok");
                   }
               }

               else
               {
                   await DisplayAlert("ALERT", "Email does not exist or you are not a partner", "Ok");
               }

               LoggedPartner = PartnerEmail.Text;

           }
        }
    }
}
