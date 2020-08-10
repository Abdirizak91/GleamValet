using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace GleamValet.Helper
{
    public class EmailPassFireAuth
    { 
        public interface IEmailPassFireAuth
        {

            void signout();
            Task<string> LoginWithEmailPassword(string email, string password);
            Task<string> RegisterWithEmailPassword(string email, string password);

            string getCurrentUser();

        }
    }
}
