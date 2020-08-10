using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Google.Cloud.Storage.V1;

namespace GleamValet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartnerOrCustomer : ContentPage
    {
        public PartnerOrCustomer()
        {
            InitializeComponent();

     



            
        }

   


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PartnerLogin());
          
        }

        private async void Button_Customer(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomerLogin());
        }
    }
}
