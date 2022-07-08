using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoDay4.WebApi.Controllers
{
    public class RestCustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public RestCustomer(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}