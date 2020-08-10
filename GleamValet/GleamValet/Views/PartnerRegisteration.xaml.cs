using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using GleamValet.Data;
using Xamarin.Forms.Xaml;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using GleamValet.Helper;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace GleamValet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartnerRegisteration : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private EmailPassFireAuth.IEmailPassFireAuth Register;

        public PartnerRegisteration()
        {
            InitializeComponent();
            Register = DependencyService.Get<EmailPassFireAuth.IEmailPassFireAuth>();
        }

         protected async override void OnAppearing()
        {
         base.OnAppearing();
         var allPartners = await firebaseHelper.PassPartners();

        }



        private async void Button_Clicked(object sender, EventArgs e)
        {
            Register.RegisterWithEmailPassword(EntryEmail.Text, EntryPassword.Text);

           await firebaseHelper.AddPartner(Register.getCurrentUser(),  EntryEmail.Text, true);
           var allPersons = await firebaseHelper.PassPartners();
           await DisplayAlert("Success", "Partners Added Successfully", "OK");

           Navigation.PushAsync(new PartnerLogin());



        }
    }
}