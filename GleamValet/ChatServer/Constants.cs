using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ChatServer
{
    public static class Constants
        {
    

            // Used to differentiate message types sent via SignalR. This
            // sample only uses a single message type.
             public static string MessageName { get; set; } = "newMessage";


        }
}
