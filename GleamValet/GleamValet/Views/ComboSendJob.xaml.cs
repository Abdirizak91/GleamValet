using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GleamValet.Data;
using GleamValet.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GleamValet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ComboSendJob : ContentPage
    {
        FirebaseHelper firebaseHelpers = new FirebaseHelper();
        private EmailPassFireAuth.IEmailPassFireAuth ForSendingCombo;
        public ComboSendJob()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            firebaseHelpers.AddJobs(CustomerLogin.LoggedCustomer, "Combo", CustomerUI.ComboPrice, CustomerUI.Longtitude , CustomerUI.Latitude , CustomerUI.Postcode);
            Navigation.PushAsync(new CustomerUI());
        }
    }
}