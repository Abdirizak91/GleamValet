using System;
using Firebase.Database;
using Firebase.Database.Query;
using GleamValet.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GleamValet.Helper
{

    public class FirebaseHelper
    {
        // Section 1 : Getting the data from the application and passing it on to the database


        // initializing the firebase database and its connection 
        FirebaseClient firebase = new FirebaseClient("https://gleamvalet-2c82b.firebaseio.com/");


        // create a list of customers which will be passed on to the firebase database 
        public async Task<List<CustomerTable>> PassCustomer()
        {
            // the method passes the data which will be stored in "Customers" under the firebase realtime db 
            // the objects or customers are being created from CustomerTable class 
            return (await firebase.Child("Customers").OnceAsync<CustomerTable>()).Select(item => new CustomerTable
            {   
                // The objects properties will be saved as such
                //firebase object's ID
                ID = item.Object.ID,

                Email = item.Object.Email,
                
                IsCustomer = item.Object.IsCustomer
            }).ToList();

        }
        
        // method to add the customer details
        public async Task AddCustomer(string id, string EML, bool Iscust)
        {

            await firebase
                .Child("Customers")
                .PostAsync(new { CustID = id, Email = EML, custo = Iscust });
        }


        // method to get the data from the firebase database - CUSTOMERS ONE 
        public async Task<CustomerTable> GetCustomer(string email)
        {

            var allCustomers = await PassCustomer();

            await firebase
                .Child("Customers")
                .OnceAsync<CustomerTable>();


            return allCustomers.Where(a => a.Email == email).FirstOrDefault();
        }





        //................................................................................................


        // same as above but this one is for the partners/valeters

        public async Task<List<PartnerRegTable>> PassPartners()
        {

            return (await firebase.Child("Partners").OnceAsync<PartnerRegTable>()).Select(item => new PartnerRegTable
            {
                ID = item.Object.ID,
                PEmail = item.Object.PEmail,
                IsPartner = item.Object.IsPartner
               
            }).ToList();
        }


        // same as above but this is for the partners/valeters
        public async Task AddPartner(string id,  string PEML,  bool ispartner)
        {
            await firebase
                .Child("Partners")
                .PostAsync(new { PartnerID = id,  PEmail = PEML, Ispartner = ispartner });
        }


        // method to get the data from the firebase database - PARTNERS ONE 
        public async Task<PartnerRegTable> GetPartner(string email)
        {
            // using GetAllCustomers method which added the customers objects to the firebase, to retrieve a customer based on email 
            var allPartners = await PassPartners();
            // from the firebase's real time database children indented sections called "customers"
            await firebase
                .Child("Partners")
                .OnceAsync<PartnerRegTable>();

            // get the customers based on matching emails
            return allPartners.Where(b => b.PEmail == email).FirstOrDefault();
        }

        // Section below is for Job 

        // modelling the job object from the jobs class 
        public async Task<List<Jobs>> PassJobs()
        {

            return (await firebase.Child("Jobs").OnceAsync<Jobs>()).Select(item => new Jobs
            {
                SendersEmail = item.Object.SendersEmail,
                JobType = item.Object.JobType,
                Price = item.Object.Price,
                Longitude = item.Object.Longitude,
                Latitude = item.Object.Latitude,
                Postcode = item.Object.Postcode

            }).ToList();
        }



        // Task takes the information and stores it as such to post it to firebase
        public async Task AddJobs(string EmailSender, string Types, string Prices, double longi, double lati, string Postcode)
        {
            await firebase
                .Child("Jobs")
                .PostAsync(new { SenderEmail = EmailSender, Type = Types, Price = Prices, longitude = longi, latitude = lati,  postcode = Postcode });
        }

        // Task below retrieves the jobs from firebase 
        public async Task<Jobs> GetJobs()
        {
            
            var allJobs = await PassJobs();
            
            await firebase
                .Child("Jobs")
                .OnceAsync<Jobs>();

            return allJobs.FirstOrDefault();
        }



        public async Task DeleteJob(string Postcode)
        {
            var toDeletePerson = (await firebase
                .Child("Jobs")
                .OnceAsync<Jobs>()).Where(a => a.Object.Postcode == Postcode).FirstOrDefault();
            await firebase.Child("Jobs").Child(toDeletePerson.Key).DeleteAsync();

        }










    }
}
