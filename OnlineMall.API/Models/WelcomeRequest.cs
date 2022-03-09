using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMall.API.Models
{
    public class WelcomeRequest
    {
        public string TICKET { get; set; }
        public string UserName { get; set; }
        public string ToEmail { get; set; }
    }
}
