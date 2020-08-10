using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using GleamValet.Helper;
using UIKit;
using System.Threading.Tasks;
using Firebase.Auth;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using GleamValet.iOS;
using Xamarin.Forms;


[assembly: Dependency(typeof(iOSAuth))]
namespace GleamValet.iOS
{
    public class iOSAuth: EmailPassFireAuth.IEmailPassFireAuth
    {
        
        public void signout()
        {
            NSError Signedout = new NSError();
            Auth.DefaultInstance.SignOut(out Signedout);
        }

        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            var authDataResult = await Auth.DefaultInstance.SignInWithPasswordAsync(
                email,
                password);

            return await authDataResult.User.GetIdTokenAsync();


        }

        public async Task<string> RegisterWithEmailPassword(string email, string password)
        {
            var authDataResult = await Auth.DefaultInstance.CreateUserAsync(
                email,
                password);

            return await authDataResult.User.GetIdTokenAsync();
            
        }

        public string getCurrentUser()
        {
            string id = Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid;

            return id;
        }


    }
}