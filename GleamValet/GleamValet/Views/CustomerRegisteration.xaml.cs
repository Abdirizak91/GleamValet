using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using GleamValet.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using GleamValet.Helper;
using Xamarin.Essentials;
using FirebaseAdmin.Auth;




namespace GleamValet.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerRegisteration : ContentPage
    {
        private EmailPassFireAuth.IEmailPassFireAuth Register;

        FirebaseHelper firebaseHelper = new FirebaseHelper();


        public CustomerRegisteration()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Register = DependencyService.Get<EmailPassFireAuth.IEmailPassFireAuth>();
        }

        // this method checks if the user is entering something in the fields 
        private bool CheckValidations()
        {
            if (string.IsNullOrEmpty(EntryEmail.Text))
            {
                DisplayAlert("Alert", "Enter email", "ok");
                return false;
            }
            if (string.IsNullOrEmpty(EntryPassword.Text))
            {
                DisplayAlert("Alert", "Enter password", "ok");
                return false;
            }
            return true;
        }


        private async void Register_Button(object sender, EventArgs e)
        {
            string Token = await Register.RegisterWithEmailPassword(EntryEmail.Text, EntryPassword.Text);

            //string id =  Register.getCurrentUser();

            //Code below saves user on database. 

            await firebaseHelper.AddCustomer(Register.getCurrentUser(), EntryEmail.Text, true);
            await DisplayAlert("Success", "You have Registered As A Customer", "OK");
            var allPersons = await firebaseHelper.PassCustomer();

            if (CheckValidations())
            {
                await Navigation.PushAsync(new CustomerLogin());
            }
        }
    }
}
