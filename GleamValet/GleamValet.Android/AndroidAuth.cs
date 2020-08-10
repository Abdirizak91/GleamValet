using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GleamValet.Helper;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Auth;
using GleamValet.Droid;
using Xamarin.Forms;
using FirebaseAdmin;


[assembly: Dependency(typeof(AndroidAuth))]
namespace GleamValet.Droid
{
    
    public class AndroidAuth : EmailPassFireAuth.IEmailPassFireAuth
    {
        public void signout()
        {
            FirebaseAuth.Instance.SignOut();

        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {

            var user = await FirebaseAuth.Instance.
                SignInWithEmailAndPasswordAsync(email, password);
            
            var token = await user.User.GetIdTokenAsync(false);
            return token.Token;
        }

        public async Task<string> RegisterWithEmailPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            
            var token = await user.User.GetIdTokenAsync(false);
            return token.Token;


            

        }



        public string getCurrentUser()
        {
            string id = Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
            return id;
        }


    }
}
