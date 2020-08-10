using System;
using System.Collections.Generic;
using System.Text;

namespace GleamValet.Data
{
    public class Jobs
    { 
        public string SendersEmail { get; set; }
        public string JobType { get; set; }
        public string Price { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; } 
        public string Postcode { get; set; }
    }
}
