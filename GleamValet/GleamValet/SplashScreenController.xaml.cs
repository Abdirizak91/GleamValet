using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GleamValet.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GleamValet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreenController : ContentPage
    {
        
        public SplashScreenController()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            

            base.OnAppearing();

            await splashimage.ScaleTo(1, 2000);
            await splashimage.ScaleTo(0.9, 1500, Easing.Linear);
            await splashimage.ScaleTo(150, 1200, Easing.Linear);
            Application.Current.MainPage = new NavigationPage(new PartnerOrCustomer());
        }
    }
}