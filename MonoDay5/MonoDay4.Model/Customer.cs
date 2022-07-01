using MonoDay4.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoDay4.Model
{
    public class Customer : ICustomer
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
        public Customer()
        {

        }
    }
}
