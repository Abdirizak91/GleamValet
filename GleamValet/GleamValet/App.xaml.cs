using System;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GleamValet.Views;
using Google.Apis.Auth.OAuth2;


namespace GleamValet
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SplashScreenController());

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
