using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GleamValet.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GleamValet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExteriorSendJob : ContentPage
    {
        FirebaseHelper firebaseHelpers = new FirebaseHelper();
        public ExteriorSendJob()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            firebaseHelpers.AddJobs(CustomerLogin.LoggedCustomer, "Exterior", CustomerUI.ExteriorPrice, CustomerUI.Longtitude, CustomerUI.Latitude, CustomerUI.Postcode);
            Navigation.PushAsync(new CustomerUI());
        }
    }
}