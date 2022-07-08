using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoDay4.Database.Models
{
    public class Customer
    {
        public Guid CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Customer(Guid customerID, string firstName, string lastName)
        {
            this.CustomerID = customerID;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}